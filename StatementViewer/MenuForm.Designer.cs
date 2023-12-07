namespace StatementViewer
{
    partial class MenuForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            StatementListView = new ListView();
            OpenStatementButton = new Button();
            openFileDialog = new OpenFileDialog();
            StatementDataGridView = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewTextBoxColumn();
            Column7 = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)StatementDataGridView).BeginInit();
            SuspendLayout();
            // 
            // StatementListView
            // 
            StatementListView.Alignment = ListViewAlignment.Left;
            StatementListView.Location = new Point(1221, 12);
            StatementListView.MultiSelect = false;
            StatementListView.Name = "StatementListView";
            StatementListView.Size = new Size(266, 701);
            StatementListView.TabIndex = 1;
            StatementListView.UseCompatibleStateImageBehavior = false;
            StatementListView.View = View.List;
            StatementListView.SelectedIndexChanged += StatementListView_SelectedIndexChanged;
            // 
            // OpenStatementButton
            // 
            OpenStatementButton.Location = new Point(1221, 722);
            OpenStatementButton.Name = "OpenStatementButton";
            OpenStatementButton.Size = new Size(266, 35);
            OpenStatementButton.TabIndex = 2;
            OpenStatementButton.Text = "Open";
            OpenStatementButton.UseVisualStyleBackColor = true;
            OpenStatementButton.Click += OpenStatementButton_Click;
            // 
            // openFileDialog
            // 
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            // 
            // StatementDataGridView
            // 
            StatementDataGridView.AllowUserToAddRows = false;
            StatementDataGridView.AllowUserToDeleteRows = false;
            StatementDataGridView.BackgroundColor = SystemColors.Control;
            StatementDataGridView.ColumnHeadersHeight = 29;
            StatementDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            StatementDataGridView.ColumnHeadersVisible = false;
            StatementDataGridView.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3, Column4, Column5, Column6, Column7 });
            StatementDataGridView.Location = new Point(12, 12);
            StatementDataGridView.MultiSelect = false;
            StatementDataGridView.Name = "StatementDataGridView";
            StatementDataGridView.ReadOnly = true;
            StatementDataGridView.RowHeadersVisible = false;
            StatementDataGridView.RowHeadersWidth = 51;
            StatementDataGridView.RowTemplate.Height = 20;
            StatementDataGridView.RowTemplate.ReadOnly = true;
            StatementDataGridView.RowTemplate.Resizable = DataGridViewTriState.False;
            StatementDataGridView.SelectionMode = DataGridViewSelectionMode.CellSelect;
            StatementDataGridView.Size = new Size(1203, 745);
            StatementDataGridView.TabIndex = 0;
            // 
            // Column1
            // 
            Column1.HeaderText = "Column1";
            Column1.MinimumWidth = 148;
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Resizable = DataGridViewTriState.False;
            Column1.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column1.Width = 148;
            // 
            // Column2
            // 
            Column2.HeaderText = "Column2";
            Column2.MinimumWidth = 148;
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            Column2.Resizable = DataGridViewTriState.False;
            Column2.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column2.Width = 148;
            // 
            // Column3
            // 
            Column3.HeaderText = "Column3";
            Column3.MinimumWidth = 148;
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            Column3.Resizable = DataGridViewTriState.False;
            Column3.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column3.Width = 148;
            // 
            // Column4
            // 
            Column4.HeaderText = "Column4";
            Column4.MinimumWidth = 148;
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            Column4.Resizable = DataGridViewTriState.False;
            Column4.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column4.Width = 148;
            // 
            // Column5
            // 
            Column5.HeaderText = "Column5";
            Column5.MinimumWidth = 148;
            Column5.Name = "Column5";
            Column5.ReadOnly = true;
            Column5.Resizable = DataGridViewTriState.False;
            Column5.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column5.Width = 148;
            // 
            // Column6
            // 
            Column6.HeaderText = "Column6";
            Column6.MinimumWidth = 148;
            Column6.Name = "Column6";
            Column6.ReadOnly = true;
            Column6.Resizable = DataGridViewTriState.False;
            Column6.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column6.Width = 148;
            // 
            // Column7
            // 
            Column7.HeaderText = "Column7";
            Column7.MinimumWidth = 135;
            Column7.Name = "Column7";
            Column7.ReadOnly = true;
            Column7.Resizable = DataGridViewTriState.False;
            Column7.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column7.Width = 135;
            // 
            // MenuForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1499, 773);
            Controls.Add(StatementDataGridView);
            Controls.Add(OpenStatementButton);
            Controls.Add(StatementListView);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MenuForm";
            Load += MenuForm_Load;
            ((System.ComponentModel.ISupportInitialize)StatementDataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private ListView StatementListView;
        private Button OpenStatementButton;
        private OpenFileDialog openFileDialog;
        private DataGridView StatementDataGridView;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
    }
}
