using System;
using System.Windows.Forms;

namespace RWDE
{
    partial class ViewAllBatchesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hWnd);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.batchBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.rWDEDataSet = new RWDE.RWDEDataSet();
            this.batchTableAdapter = new RWDE.RWDEDataSetTableAdapters.BatchTableAdapter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnl = new System.Windows.Forms.Panel();
            this.pnl2 = new System.Windows.Forms.Panel();
            this.cbBatchType = new System.Windows.Forms.ComboBox();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartFrom = new System.Windows.Forms.Label();
            this.bnClear = new System.Windows.Forms.Button();
            this.lblHeading = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblEndTo = new System.Windows.Forms.Label();
            this.lblBatchType = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.BatchID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalRows = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SuccessfulRows = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.batchIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fileNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pathDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startedAtDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.endedAtDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalRowsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.successfulRowsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.failedRowsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.messageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commentsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createdByDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createdOnDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Batchtype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartedAt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndedAt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonDelete = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnClose = new System.Windows.Forms.Button();
            this.batchTableAdapter1 = new RWDE.RWDEDataSet1TableAdapters.BatchTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.batchBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rWDEDataSet)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnl.SuspendLayout();
            this.pnl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // batchBindingSource
            // 
            this.batchBindingSource.DataMember = "Batch";
            this.batchBindingSource.DataSource = this.rWDEDataSet;
            // 
            // rWDEDataSet
            // 
            this.rWDEDataSet.DataSetName = "RWDEDataSet";
            this.rWDEDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // batchTableAdapter
            // 
            this.batchTableAdapter.ClearBeforeFill = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.pnl);
            this.panel1.Location = new System.Drawing.Point(3, 1);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(5000, 900);
            this.panel1.TabIndex = 1;
            // 
            // pnl
            // 
            this.pnl.AutoSize = true;
            this.pnl.Controls.Add(this.pnl2);
            this.pnl.Controls.Add(this.lblTitle);
            this.pnl.Controls.Add(this.dataGridView);
            this.pnl.Controls.Add(this.btnClose);
            this.pnl.Location = new System.Drawing.Point(0, 0);
            this.pnl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(1993, 950);
            this.pnl.TabIndex = 3;
            this.pnl.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_Paint);
            // 
            // pnl2
            // 
            this.pnl2.AutoSize = true;
            this.pnl2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl2.Controls.Add(this.cbBatchType);
            this.pnl2.Controls.Add(this.dtpStartDate);
            this.pnl2.Controls.Add(this.lblStartFrom);
            this.pnl2.Controls.Add(this.bnClear);
            this.pnl2.Controls.Add(this.lblHeading);
            this.pnl2.Controls.Add(this.btnSubmit);
            this.pnl2.Controls.Add(this.dtpEndDate);
            this.pnl2.Controls.Add(this.lblEndTo);
            this.pnl2.Controls.Add(this.lblBatchType);
            this.pnl2.Location = new System.Drawing.Point(120, 91);
            this.pnl2.Margin = new System.Windows.Forms.Padding(4);
            this.pnl2.Name = "pnl2";
            this.pnl2.Size = new System.Drawing.Size(1783, 103);
            this.pnl2.TabIndex = 4;
            // 
            // cbBatchType
            // 
            this.cbBatchType.BackColor = System.Drawing.Color.White;
            this.cbBatchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBatchType.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBatchType.FormattingEnabled = true;
            this.cbBatchType.Items.AddRange(new object[] {
            "HCC",
            "OCHIN",
            "Client track"});
            this.cbBatchType.Location = new System.Drawing.Point(159, 49);
            this.cbBatchType.Margin = new System.Windows.Forms.Padding(4);
            this.cbBatchType.Name = "cbBatchType";
            this.cbBatchType.Size = new System.Drawing.Size(247, 37);
            this.cbBatchType.TabIndex = 17;
            this.cbBatchType.SelectedIndexChanged += new System.EventHandler(this.cbBatchType_SelectedIndexChanged);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CalendarForeColor = System.Drawing.Color.Black;
            this.dtpStartDate.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(595, 52);
            this.dtpStartDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dtpStartDate.Size = new System.Drawing.Size(204, 36);
            this.dtpStartDate.TabIndex = 16;
            this.dtpStartDate.Value = new System.DateTime(2024, 6, 21, 14, 13, 9, 0);
            // 
            // lblStartFrom
            // 
            this.lblStartFrom.AutoSize = true;
            this.lblStartFrom.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartFrom.ForeColor = System.Drawing.Color.Black;
            this.lblStartFrom.Location = new System.Drawing.Point(435, 53);
            this.lblStartFrom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStartFrom.Name = "lblStartFrom";
            this.lblStartFrom.Size = new System.Drawing.Size(130, 29);
            this.lblStartFrom.TabIndex = 15;
            this.lblStartFrom.Text = "From Date :";
            // 
            // bnClear
            // 
            this.bnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.bnClear.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnClear.ForeColor = System.Drawing.Color.Black;
            this.bnClear.Location = new System.Drawing.Point(1468, 49);
            this.bnClear.Margin = new System.Windows.Forms.Padding(4);
            this.bnClear.Name = "bnClear";
            this.bnClear.Size = new System.Drawing.Size(227, 46);
            this.bnClear.TabIndex = 14;
            this.bnClear.Text = "Clear";
            this.bnClear.UseVisualStyleBackColor = false;
            this.bnClear.Click += new System.EventHandler(this.bnClear_Click);
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.BackColor = System.Drawing.Color.White;
            this.lblHeading.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblHeading.Location = new System.Drawing.Point(-7, -1);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(499, 35);
            this.lblHeading.TabIndex = 1;
            this.lblHeading.Text = " All batches in RWDE are displayed below.\r\n";
            this.lblHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnSubmit.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.ForeColor = System.Drawing.Color.Black;
            this.btnSubmit.Location = new System.Drawing.Point(1217, 49);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(4);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(227, 46);
            this.btnSubmit.TabIndex = 13;
            this.btnSubmit.Text = "Filter";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(961, 52);
            this.dtpEndDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(211, 36);
            this.dtpEndDate.TabIndex = 12;
            // 
            // lblEndTo
            // 
            this.lblEndTo.AutoSize = true;
            this.lblEndTo.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndTo.ForeColor = System.Drawing.Color.Black;
            this.lblEndTo.Location = new System.Drawing.Point(836, 55);
            this.lblEndTo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEndTo.Name = "lblEndTo";
            this.lblEndTo.Size = new System.Drawing.Size(100, 29);
            this.lblEndTo.TabIndex = 11;
            this.lblEndTo.Text = "To Date :";
            // 
            // lblBatchType
            // 
            this.lblBatchType.AutoSize = true;
            this.lblBatchType.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBatchType.ForeColor = System.Drawing.Color.Black;
            this.lblBatchType.Location = new System.Drawing.Point(3, 52);
            this.lblBatchType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBatchType.Name = "lblBatchType";
            this.lblBatchType.Size = new System.Drawing.Size(133, 29);
            this.lblBatchType.TabIndex = 9;
            this.lblBatchType.Text = "Batch Type :";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Location = new System.Drawing.Point(904, 52);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(207, 35);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "Manage Batches";
            // 
            // dataGridView
            // 
            this.dataGridView.AutoGenerateColumns = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BatchID,
            this.Description,
            this.TotalRows,
            this.SuccessfulRows,
            this.Status,
            this.batchIDDataGridViewTextBoxColumn,
            this.fileNameDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn,
            this.pathDataGridViewTextBoxColumn,
            this.startedAtDataGridViewTextBoxColumn,
            this.endedAtDataGridViewTextBoxColumn,
            this.totalRowsDataGridViewTextBoxColumn,
            this.successfulRowsDataGridViewTextBoxColumn,
            this.failedRowsDataGridViewTextBoxColumn,
            this.statusDataGridViewTextBoxColumn,
            this.messageDataGridViewTextBoxColumn,
            this.commentsDataGridViewTextBoxColumn,
            this.createdByDataGridViewTextBoxColumn,
            this.createdOnDataGridViewTextBoxColumn,
            this.Batchtype,
            this.StartedAt,
            this.EndedAt,
            this.buttonDelete});
            this.dataGridView.DataSource = this.batchBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(156)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView.EnableHeadersVisualStyles = false;
            this.dataGridView.GridColor = System.Drawing.Color.Black;
            this.dataGridView.Location = new System.Drawing.Point(120, 215);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView.RowHeadersWidth = 51;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView.RowTemplate.Height = 30;
            this.dataGridView.Size = new System.Drawing.Size(1783, 557);
            this.dataGridView.TabIndex = 0;
            // 
            // BatchID
            // 
            this.BatchID.DataPropertyName = Constants.BatchId;
            this.BatchID.HeaderText = Constants.BatchId;
            this.BatchID.MinimumWidth = 6;
            this.BatchID.Name = Constants.BatchId;
            this.BatchID.ReadOnly = true;
            this.BatchID.Width = 119;
            // 
            // Description
            // 
            this.Description.DataPropertyName = Constants.Description;
            this.Description.HeaderText = Constants.Description;
            this.Description.MinimumWidth = 6;
            this.Description.Name = Constants.Description;
            this.Description.ReadOnly = true;
            this.Description.Width = 157;
            // 
            // TotalRows
            // 
            this.TotalRows.DataPropertyName = "TotalRows";
            this.TotalRows.HeaderText = "TotalRows";
            this.TotalRows.MinimumWidth = 6;
            this.TotalRows.Name = "TotalRows";
            this.TotalRows.ReadOnly = true;
            this.TotalRows.Width = 146;
            // 
            // SuccessfulRows
            // 
            this.SuccessfulRows.DataPropertyName = "SuccessfulRows";
            this.SuccessfulRows.HeaderText = "SuccessfulRows";
            this.SuccessfulRows.MinimumWidth = 6;
            this.SuccessfulRows.Name = "SuccessfulRows";
            this.SuccessfulRows.ReadOnly = true;
            this.SuccessfulRows.Width = 200;
            // 
            // Status
            // 
            this.Status.DataPropertyName = Constants.Status;
            this.Status.HeaderText = Constants.Status;
            this.Status.MinimumWidth = 6;
            this.Status.Name = Constants.Status;
            this.Status.ReadOnly = true;
            this.Status.Width = 104;
            // 
            // batchIDDataGridViewTextBoxColumn
            // 
            this.batchIDDataGridViewTextBoxColumn.DataPropertyName = Constants.BatchId;
            this.batchIDDataGridViewTextBoxColumn.HeaderText = Constants.BatchId;
            this.batchIDDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.batchIDDataGridViewTextBoxColumn.Name = "batchIDDataGridViewTextBoxColumn";
            this.batchIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.batchIDDataGridViewTextBoxColumn.Width = 119;
            // 
            // fileNameDataGridViewTextBoxColumn
            // 
            this.fileNameDataGridViewTextBoxColumn.DataPropertyName = Constants.FileName;
            this.fileNameDataGridViewTextBoxColumn.HeaderText = Constants.FileName;
            this.fileNameDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.fileNameDataGridViewTextBoxColumn.Name = "fileNameDataGridViewTextBoxColumn";
            this.fileNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.fileNameDataGridViewTextBoxColumn.Width = 137;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = Constants.Description;
            this.descriptionDataGridViewTextBoxColumn.HeaderText = Constants.Description;
            this.descriptionDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            this.descriptionDataGridViewTextBoxColumn.ReadOnly = true;
            this.descriptionDataGridViewTextBoxColumn.Width = 157;
            // 
            // pathDataGridViewTextBoxColumn
            // 
            this.pathDataGridViewTextBoxColumn.DataPropertyName = "Path";
            this.pathDataGridViewTextBoxColumn.HeaderText = "Path";
            this.pathDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.pathDataGridViewTextBoxColumn.Name = "pathDataGridViewTextBoxColumn";
            this.pathDataGridViewTextBoxColumn.ReadOnly = true;
            this.pathDataGridViewTextBoxColumn.Width = 88;
            // 
            // startedAtDataGridViewTextBoxColumn
            // 
            this.startedAtDataGridViewTextBoxColumn.DataPropertyName = "StartedAt";
            this.startedAtDataGridViewTextBoxColumn.HeaderText = "StartedAt";
            this.startedAtDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.startedAtDataGridViewTextBoxColumn.Name = "startedAtDataGridViewTextBoxColumn";
            this.startedAtDataGridViewTextBoxColumn.ReadOnly = true;
            this.startedAtDataGridViewTextBoxColumn.Width = 137;
            // 
            // endedAtDataGridViewTextBoxColumn
            // 
            this.endedAtDataGridViewTextBoxColumn.DataPropertyName = "EndedAt";
            this.endedAtDataGridViewTextBoxColumn.HeaderText = "EndedAt";
            this.endedAtDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.endedAtDataGridViewTextBoxColumn.Name = "endedAtDataGridViewTextBoxColumn";
            this.endedAtDataGridViewTextBoxColumn.ReadOnly = true;
            this.endedAtDataGridViewTextBoxColumn.Width = 127;
            // 
            // totalRowsDataGridViewTextBoxColumn
            // 
            this.totalRowsDataGridViewTextBoxColumn.DataPropertyName = "TotalRows";
            this.totalRowsDataGridViewTextBoxColumn.HeaderText = "TotalRows";
            this.totalRowsDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.totalRowsDataGridViewTextBoxColumn.Name = "totalRowsDataGridViewTextBoxColumn";
            this.totalRowsDataGridViewTextBoxColumn.ReadOnly = true;
            this.totalRowsDataGridViewTextBoxColumn.Width = 146;
            // 
            // successfulRowsDataGridViewTextBoxColumn
            // 
            this.successfulRowsDataGridViewTextBoxColumn.DataPropertyName = "SuccessfulRows";
            this.successfulRowsDataGridViewTextBoxColumn.HeaderText = "SuccessfulRows";
            this.successfulRowsDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.successfulRowsDataGridViewTextBoxColumn.Name = "successfulRowsDataGridViewTextBoxColumn";
            this.successfulRowsDataGridViewTextBoxColumn.ReadOnly = true;
            this.successfulRowsDataGridViewTextBoxColumn.Width = 200;
            // 
            // failedRowsDataGridViewTextBoxColumn
            // 
            this.failedRowsDataGridViewTextBoxColumn.DataPropertyName = "FailedRows";
            this.failedRowsDataGridViewTextBoxColumn.HeaderText = "FailedRows";
            this.failedRowsDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.failedRowsDataGridViewTextBoxColumn.Name = "failedRowsDataGridViewTextBoxColumn";
            this.failedRowsDataGridViewTextBoxColumn.ReadOnly = true;
            this.failedRowsDataGridViewTextBoxColumn.Width = 156;
            // 
            // statusDataGridViewTextBoxColumn
            // 
            this.statusDataGridViewTextBoxColumn.DataPropertyName = Constants.Status;
            this.statusDataGridViewTextBoxColumn.HeaderText = Constants.Status;
            this.statusDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.statusDataGridViewTextBoxColumn.Name = "statusDataGridViewTextBoxColumn";
            this.statusDataGridViewTextBoxColumn.ReadOnly = true;
            this.statusDataGridViewTextBoxColumn.Width = 104;
            // 
            // messageDataGridViewTextBoxColumn
            // 
            this.messageDataGridViewTextBoxColumn.DataPropertyName = "Message";
            this.messageDataGridViewTextBoxColumn.HeaderText = "Message";
            this.messageDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.messageDataGridViewTextBoxColumn.Name = "messageDataGridViewTextBoxColumn";
            this.messageDataGridViewTextBoxColumn.ReadOnly = true;
            this.messageDataGridViewTextBoxColumn.Width = 130;
            // 
            // commentsDataGridViewTextBoxColumn
            // 
            this.commentsDataGridViewTextBoxColumn.DataPropertyName = "Comments";
            this.commentsDataGridViewTextBoxColumn.HeaderText = "Comments";
            this.commentsDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.commentsDataGridViewTextBoxColumn.Name = "commentsDataGridViewTextBoxColumn";
            this.commentsDataGridViewTextBoxColumn.ReadOnly = true;
            this.commentsDataGridViewTextBoxColumn.Width = 151;
            // 
            // createdByDataGridViewTextBoxColumn
            // 
            this.createdByDataGridViewTextBoxColumn.DataPropertyName = "CreatedBy";
            this.createdByDataGridViewTextBoxColumn.HeaderText = "CreatedBy";
            this.createdByDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.createdByDataGridViewTextBoxColumn.Name = "createdByDataGridViewTextBoxColumn";
            this.createdByDataGridViewTextBoxColumn.ReadOnly = true;
            this.createdByDataGridViewTextBoxColumn.Width = 145;
            // 
            // createdOnDataGridViewTextBoxColumn
            // 
            this.createdOnDataGridViewTextBoxColumn.DataPropertyName = "CreatedOn";
            this.createdOnDataGridViewTextBoxColumn.HeaderText = "CreatedOn";
            this.createdOnDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.createdOnDataGridViewTextBoxColumn.Name = "createdOnDataGridViewTextBoxColumn";
            this.createdOnDataGridViewTextBoxColumn.ReadOnly = true;
            this.createdOnDataGridViewTextBoxColumn.Width = 150;
            // 
            // Batchtype
            // 
            this.Batchtype.HeaderText = Constants.BatchTypeHeader;
            this.Batchtype.MinimumWidth = 6;
            this.Batchtype.Name = "Batchtype";
            this.Batchtype.ReadOnly = true;
            this.Batchtype.Width = 137;
            // 
            // StartedAt
            // 
            this.StartedAt.DataPropertyName = Constants.UploadStartedAt;
            this.StartedAt.HeaderText = "Upload StartedAt";
            this.StartedAt.MinimumWidth = 6;
            this.StartedAt.Name = "StartedAt";
            this.StartedAt.ReadOnly = true;
            this.StartedAt.Width = 196;
            // 
            // EndedAt
            // 
            this.EndedAt.DataPropertyName = Constants.UploadEndedAt;
            this.EndedAt.HeaderText = "Upload EndedAt";
            this.EndedAt.MinimumWidth = 6;
            this.EndedAt.Name = "EndedAt";
            this.EndedAt.ReadOnly = true;
            this.EndedAt.Width = 187;
            // 
            // buttonDelete
            // 
            this.buttonDelete.HeaderText = Constants.DeleteColumnName;
            this.buttonDelete.MinimumWidth = 6;
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.ReadOnly = true;
            this.buttonDelete.Width = 107;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(1665, 778);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(240, 48);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = Constants.Close;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // batchTableAdapter1
            // 
            this.batchTableAdapter1.ClearBeforeFill = true;
            // 
            // ViewAllBatchesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 884);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ViewAllBatchesForm";
            this.Text = "Manage Batches";
            ((System.ComponentModel.ISupportInitialize)(this.batchBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rWDEDataSet)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnl.ResumeLayout(false);
            this.pnl.PerformLayout();
            this.pnl2.ResumeLayout(false);
            this.pnl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }
        private void pnl_Paint(object sender, PaintEventArgs e)
        {
            IntPtr hDC = GetDC(this.Handle);
            ReleaseDC(this.Handle, hDC);
        }

        #endregion
        private RWDEDataSet rWDEDataSet;
        private System.Windows.Forms.BindingSource batchBindingSource;
        private RWDEDataSetTableAdapters.BatchTableAdapter batchTableAdapter;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClose;
        private Panel pnl;
        private Label lblHeading;
        private DataGridView dataGridView;
        private DataGridViewTextBoxColumn BatchID;
        private DataGridViewTextBoxColumn Description;
        private DataGridViewTextBoxColumn Batchtype;
        private DataGridViewTextBoxColumn StartedAt;
        private DataGridViewTextBoxColumn EndedAt;
        private DataGridViewTextBoxColumn TotalRows;
        private DataGridViewTextBoxColumn SuccessfulRows;
        private DataGridViewTextBoxColumn Status;
        private DataGridViewTextBoxColumn buttonDelete;
        private DataGridViewTextBoxColumn batchIDDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn fileNameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn pathDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn startedAtDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn endedAtDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn totalRowsDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn successfulRowsDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn failedRowsDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn statusDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn messageDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn commentsDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn createdByDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn createdOnDataGridViewTextBoxColumn;
        private Label lblTitle;
        private Panel pnl2;
        private Button btnSubmit;
        private DateTimePicker dtpEndDate;
        private Label lblEndTo;
        private Label lblBatchType;
        private Button bnClear;
        private Label lblStartFrom;
        private ComboBox cbBatchType;
        private RWDEDataSet1TableAdapters.BatchTableAdapter batchTableAdapter1;
        private DateTimePicker dtpStartDate;
        private void cbBatchType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
