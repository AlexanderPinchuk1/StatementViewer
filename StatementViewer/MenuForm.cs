using StatementViewer.Domain;
using StatementViewer.Foundation.Interfaces;
using StatementViewer.Models;

namespace StatementViewer
{
    public partial class MenuForm : Form
    {
        private readonly IDatabaseService _databaseService;
        private readonly IStatementParserService _statementParserService;



        public MenuForm(IDatabaseService documentService, IStatementParserService statementParserService)
        {
            InitializeComponent();

            _databaseService = documentService;
            _statementParserService = statementParserService;
        }



        private async void MenuForm_Load(object sender, EventArgs e)
        {
            await UpdateDocumentList();
        }

        private async void OpenStatementButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            OpenStatementButton.Enabled = false;

            var statement = await _statementParserService.ParseAsync(openFileDialog.FileName);

            MessageBox.Show(
              "Document added.",
              "Message",
              MessageBoxButtons.OK,
              MessageBoxIcon.Information,
              MessageBoxDefaultButton.Button1,
              MessageBoxOptions.DefaultDesktopOnly);

            OpenStatementButton.Enabled = true;

            await UpdateDocumentList();
            ViewStatement(statement);
        }

        private async void StatementListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            StatementListView.Enabled = false;
            if (StatementListView.SelectedItems.Count == 0)
            {
                return;
            };

            var statement = await _databaseService.GetDocumentByIdOrReturnNullAsync(GetSelectedDoucumentId());
            if (statement == null)
            {
                StatementListView.Enabled = true;

                return;
            }

            ViewStatement(statement);

            StatementDataGridView.ClearSelection();

            StatementListView.Enabled = true;
        }

        private Guid GetSelectedDoucumentId()
        {
            var id = StatementListView.SelectedItems[0].Tag as Guid?;

            return id == null ? Guid.Empty : (Guid)id;
        }

        private void ClearPreviousStatementInfo()
        {
            StatementDataGridView.Rows.Clear();
        }

        private static string ConvertDateTimeToShortString(DateTime? date)
        {
            return date.GetValueOrDefault() == DateTime.MinValue ? "" : date.GetValueOrDefault().ToShortDateString();
        }
        private static string ConvertDateTimeToString(DateTime? date)
        {
            return date.GetValueOrDefault() == DateTime.MinValue ? "" : date.GetValueOrDefault().ToString();
        }
        private void AddStatementHeaderIntoTable(Statement statement)
        {
            StatementDataGridView.Rows.Add(7);
            MergeCellsInTable(statement.BankName ?? "", StringAlignment.Near, 0, 0, 1);
            MergeCellsInTable(" ", StringAlignment.Center, 0, 2, 6);
            MergeCellsInTable("Оборотная ведомость по балансовым счетам", StringAlignment.Center, 1, 0, 6);
            MergeCellsInTable("за период с " + ConvertDateTimeToShortString(statement.StartOfPeriod) + 
                " по " + ConvertDateTimeToShortString(statement.EndOfPeriod), StringAlignment.Center, 2, 0, 6);
            MergeCellsInTable("по банку", StringAlignment.Center, 3, 0, 6);
            MergeCellsInTable(ConvertDateTimeToString(statement.GenerationDate), StringAlignment.Near, 4, 0, 0);
            MergeCellsInTable(" ", StringAlignment.Center, 4, 1, 5);
            MergeCellsInTable("в руб.", StringAlignment.Far, 4, 6, 6);
            MergeCellsInTable(" ", StringAlignment.Center, 5, 0, 0);
            MergeCellsInTable("Б/сч", StringAlignment.Near, 6, 0, 0);
            MergeCellsInTable("ВХОДЯЩЕЕ САЛЬДО", StringAlignment.Center, 5, 1, 2);
            MergeCellsInTable("ОБОРОТЫ", StringAlignment.Center, 5, 3, 4);
            MergeCellsInTable("ИСХОДЯЩЕЕ САЛЬДО", StringAlignment.Center, 5, 5, 6);
            MergeCellsInTable("АКТИВ", StringAlignment.Center, 6, 1, 1);
            MergeCellsInTable("АКТИВ", StringAlignment.Center, 6, 5, 5);
            MergeCellsInTable("ПАССИВ", StringAlignment.Center, 6, 2, 2);
            MergeCellsInTable("ПАССИВ", StringAlignment.Center, 6, 6, 6);
            MergeCellsInTable("ДЕБЕТ", StringAlignment.Center, 6, 3, 3);
            MergeCellsInTable("КРЕДИТ", StringAlignment.Center, 6, 4, 4);
        }

        private void ViewStatement(Statement statement)
        {
            ClearPreviousStatementInfo();
            AddStatementHeaderIntoTable(statement);
            AddAccountsInfoIntoTable(statement);
        }

        private static decimal RoundNumber(decimal number)
        {
            return number == 0 ? Math.Round(number) : Math.Round(number, 2);
        }

        private void AddRowWithAccountInfoIntoTable(AccountInfo accountInfo)
        {
            StatementDataGridView.Rows.Add(
                accountInfo.Number,
                RoundNumber(accountInfo.Asset),
                RoundNumber(accountInfo.Passive),
                RoundNumber(accountInfo.Debit),
                RoundNumber(accountInfo.Credit),
                RoundNumber(accountInfo.Asset == 0 ? 0 : accountInfo.Asset + accountInfo.Debit - accountInfo.Credit),
                RoundNumber(accountInfo.Passive == 0 ? 0 : accountInfo.Passive + accountInfo.Credit - accountInfo.Debit));
        }

        private void AddRowWithAccountInfoSummaryIntoTable(string description, List<AccountInfo> accountsInfo)
        {
            StatementDataGridView.Rows.Add(
                description,
                RoundNumber(accountsInfo.Select(accountInGroup => accountInGroup.Asset).Sum()),
                RoundNumber(accountsInfo.Select(accountInGroup => accountInGroup.Passive).Sum()),
                RoundNumber(accountsInfo.Select(accountInGroup => accountInGroup.Debit).Sum()),
                RoundNumber(accountsInfo.Select(accountInGroup => accountInGroup.Credit).Sum()),
                RoundNumber(accountsInfo.Select(accountInGroup => accountInGroup.Asset == 0
                    ? 0 : accountInGroup.Asset + accountInGroup.Debit - accountInGroup.Credit).Sum()),
                RoundNumber(accountsInfo.Select(accountInGroup => accountInGroup.Passive == 0
                    ? 0 : accountInGroup.Passive + accountInGroup.Credit - accountInGroup.Debit).Sum()));

            StatementDataGridView.Rows[^1].DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
        }

        private void MergeCellsInTable(string text, StringAlignment alignment, int rowIndex, int leftColumnIndex, int rightColumnIndex)
        {
            for (var i = leftColumnIndex; i <= rightColumnIndex; i++)
            {
                StatementDataGridView.Rows[rowIndex].Cells[i] = 
                    new MergedCell(alignment, leftColumnIndex, rightColumnIndex)
                    {
                        Value = text,
                    };
            }
        }

        private void AddAccountsInfoIntoTable(Statement statement)
        {
            foreach (var accountUnit in statement.AccountUnits.OrderBy(accountUnit => accountUnit.Number))
            {
                StatementDataGridView.Rows.Add();
                MergeCellsInTable("КЛАСС " + accountUnit.Number + " " + accountUnit.Description, StringAlignment.Center, StatementDataGridView.Rows.Count - 1, 0, 6);
                foreach (var accountsInfoInGroup in accountUnit.AccountsInfo.GroupBy(account => account?.Number?[..2]).OrderBy(group => group.Key))
                {
                    foreach (var accountInfo in accountsInfoInGroup.OrderBy(account => account.Number))
                    {
                        AddRowWithAccountInfoIntoTable(accountInfo);
                    }
                    AddRowWithAccountInfoSummaryIntoTable(accountsInfoInGroup.Key ?? "", [.. accountsInfoInGroup]);
                }
                AddRowWithAccountInfoSummaryIntoTable("ПО КЛАССУ", accountUnit.AccountsInfo);
            }
            AddRowWithAccountInfoSummaryIntoTable("БАЛАНС", statement.AccountUnits.SelectMany(accountUnit => accountUnit.AccountsInfo).ToList());
        }

        private async Task UpdateDocumentList()
        {
            StatementListView.Items.Clear();
            foreach (var document in await _databaseService.GetStatements())
            {
                StatementListView.Items.Add(new ListViewItem()
                {
                    Text = document.Name,
                    Tag = document.Id
                });
            }
        }
    }
}
