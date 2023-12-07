using Aspose.Cells;
using StatementViewer.Foundation.Interfaces;
using StatementViewer.Models;
using System.Text.RegularExpressions;

namespace StatementViewer.Foundation
{
    /// <summary>
    /// Сервис для парсинна содержимого exсel файла 
    /// </summary>
    public partial class StatementParserService(IDatabaseService statementService) : IStatementParserService
    {
        private const int BANK_NAME_COLUMN_NUMBER = 0;
        private const int BANK_NAME_ROW_NUMBER = 0;
        private const int DATE_PERIOD_COLUMN_NUMBER = 0;
        private const int DATE_PERIOD_ROW_NUMBER = 2;
        private const int DATE_GENERATION_COLUMN_NUMBER = 0;
        private const int DATE_GENERATION_ROW_NUMBER = 5;
        private const int CURRENCY_COLUMN_NUMBER = 6;
        private const int CURRENCY_ROW_NUMBER = 5;
        private const int ASSET_COLUMN_NUMBER = 1;
        private const int PASSIVE_COLUMN_NUMBER = 2;
        private const int DEBIT_COLUMN_NUMBER = 3;
        private const int CREDIT_COLUMN_NUMBER = 4;
        private const int ACCOUNT_INFO_ROW_START_NUMBER = 8;

        private readonly IDatabaseService _statementService = statementService;



        public async Task<Statement> ParseAsync(string filePath)
        {
            var workBook = new Workbook(filePath);
            var worksheet = workBook.Worksheets[0];

            var statement = GetInfoFromStatementHeader(worksheet, filePath);
            statement = GetAccountsInfoFromStatement(worksheet, statement, worksheet.Cells.Rows.Count);

            await _statementService.SaveStatement(statement);
            
            return statement;
        }
        private Statement GetInfoFromStatementHeader(Worksheet worksheet, string filePath)
        {
            var periodInfo = Convert.ToString(worksheet.Cells[DATE_PERIOD_ROW_NUMBER, DATE_PERIOD_COLUMN_NUMBER].Value)?.Split(" ");
            var generationDate = Convert.ToDateTime(worksheet.Cells[DATE_GENERATION_ROW_NUMBER, DATE_GENERATION_COLUMN_NUMBER].Value);

            return new Statement()
            {
                Id = Guid.NewGuid(),
                StartOfPeriod = periodInfo != null && periodInfo.Length >= 4 ? Convert.ToDateTime(periodInfo[3]) : null,
                EndOfPeriod = periodInfo != null && periodInfo.Length >= 6 ? Convert.ToDateTime(periodInfo[5]) : null,
                GenerationDate = generationDate == DateTime.MinValue ? null : generationDate,
                BankName = Convert.ToString(worksheet.Cells[BANK_NAME_ROW_NUMBER, BANK_NAME_COLUMN_NUMBER].Value),
                Сurrency = Convert.ToString(worksheet.Cells[CURRENCY_ROW_NUMBER, CURRENCY_COLUMN_NUMBER].Value),
                FileName = Path.GetFileNameWithoutExtension(filePath),
                AccountUnits = []
            };
        }

        private static Statement GetAccountsInfoFromStatement(Worksheet worksheet, Statement statement, int rowCount)
        {
            AccountUnit? currAccountClass = null;
            for (var i = ACCOUNT_INFO_ROW_START_NUMBER; i < rowCount; i++)
            {
                if (!IsRowValid(worksheet, i))
                {
                    continue;
                }

                if (IsRowContainClassInfo(worksheet, i))
                {
                    var newAccountUnit = GetAccountUnitInfoFromRowOrReturnNull(worksheet, i, statement.Id);
                    if (newAccountUnit != null)
                    {
                        statement.AccountUnits.Add(newAccountUnit);
                        currAccountClass = newAccountUnit;
                    }
                }
                else
                {
                    currAccountClass?.AccountsInfo?.Add(GetAccountInfoFromRow(worksheet, i, currAccountClass.Id));
                }
            }

            return statement;
        }

        private static AccountUnit? GetAccountUnitInfoFromRowOrReturnNull(Worksheet worksheet, int rowIndex, Guid statementId)
        {
            var classInfo = Convert.ToString(worksheet.Cells[rowIndex, 0].Value)?.Split(" ").ToList();
            if (classInfo == null)
            {
                return null;
            }

            classInfo.RemoveAll(str => str == "");

            return new AccountUnit()
            {
                Id = Guid.NewGuid(),
                Number = classInfo[1],
                Description = string.Join(" ", classInfo.GetRange(2, classInfo.Count - 2)),
                StatementId = statementId,
                AccountsInfo = []
            };
        }

        private static AccountInfo GetAccountInfoFromRow(Worksheet worksheet, int rowIndex, Guid? accountUnitId)
        {
            return new AccountInfo()
            {
                Id = Guid.NewGuid(),
                Number = Convert.ToString(worksheet.Cells[rowIndex, 0].Value),
                Asset = Convert.ToDecimal(worksheet.Cells[rowIndex, ASSET_COLUMN_NUMBER].Value),
                Passive = Convert.ToDecimal(worksheet.Cells[rowIndex, PASSIVE_COLUMN_NUMBER].Value),
                Debit = Convert.ToDecimal(worksheet.Cells[rowIndex, DEBIT_COLUMN_NUMBER].Value),
                Credit = Convert.ToDecimal(worksheet.Cells[rowIndex, CREDIT_COLUMN_NUMBER].Value),
                AccountUnitId = accountUnitId
            };
        }

        private static bool IsRowContainClassInfo(Worksheet worksheet, int rowIndex)
        {
            return worksheet.Cells[rowIndex, 0].IsMerged;
        }

        private static bool IsRowValid(Worksheet worksheet, int rowIndex)
        {
            var value = Convert.ToString(worksheet.Cells[rowIndex, 0].Value);
            var regex = RusAlphabet();
            if (value == null || (!worksheet.Cells[rowIndex, 0].IsMerged && (value.Length <= 2 || regex.IsMatch(value))))
            {
                return false;
            }

            return true;
        }

        [GeneratedRegex(@"^[А-Яа-яеЁ]+")]
        private static partial Regex RusAlphabet();
    }
}
