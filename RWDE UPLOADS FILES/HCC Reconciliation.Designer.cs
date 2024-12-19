namespace RWDE
{
    partial class HCC_Reconciliation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnDownload = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.pnl = new System.Windows.Forms.Panel();
            this.txtbatchs = new System.Windows.Forms.TextBox();
            this.lblbatchid = new System.Windows.Forms.Label();
            this.btnClr = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.lblHeader = new System.Windows.Forms.Label();
            this.dtpDateFilter = new System.Windows.Forms.ComboBox();
            this.lblDateFilter = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.txtBatchID = new System.Windows.Forms.TextBox();
            this.lblBatch = new System.Windows.Forms.Label();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Month = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServiceEntriesSuccessfullyExportedToHCC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServiceEntriesNotExportedToHCC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServiceEntriesPostTimeboxPeriod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServiceEntriesForHCCIDMissing = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Drop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.pnl.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDownload
            // 
            this.btnDownload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnDownload.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.ForeColor = System.Drawing.Color.Black;
            this.btnDownload.Location = new System.Drawing.Point(1418, 769);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(244, 50);
            this.btnDownload.TabIndex = 10;
            this.btnDownload.Text = "Export";
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Location = new System.Drawing.Point(802, 39);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(320, 35);
            this.lblTitle.TabIndex = 9;
            this.lblTitle.Text = "HCC Reconciliation Report";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnClose.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(1669, 769);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(244, 50);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.ColumnHeadersHeight = 50;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Month,
            this.ServiceEntriesSuccessfullyExportedToHCC,
            this.ServiceEntriesNotExportedToHCC,
            this.ServiceEntriesPostTimeboxPeriod,
            this.ServiceEntriesForHCCIDMissing,
            this.Drop});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView.EnableHeadersVisualStyles = false;
            this.dataGridView.GridColor = System.Drawing.Color.Black;
            this.dataGridView.Location = new System.Drawing.Point(143, 251);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.Size = new System.Drawing.Size(1770, 500);
            this.dataGridView.TabIndex = 7;
            this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentClick);
            // 
            // pnl
            // 
            this.pnl.AutoSize = true;
            this.pnl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl.Controls.Add(this.txtbatchs);
            this.pnl.Controls.Add(this.lblbatchid);
            this.pnl.Controls.Add(this.btnClr);
            this.pnl.Controls.Add(this.btnReport);
            this.pnl.Controls.Add(this.lblHeader);
            this.pnl.Controls.Add(this.dtpDateFilter);
            this.pnl.Controls.Add(this.lblDateFilter);
            this.pnl.Controls.Add(this.dtpEndDate);
            this.pnl.Controls.Add(this.lblEndDate);
            this.pnl.Controls.Add(this.dtpStartDate);
            this.pnl.Controls.Add(this.lblStartDate);
            this.pnl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnl.Location = new System.Drawing.Point(141, 84);
            this.pnl.Margin = new System.Windows.Forms.Padding(4);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(1770, 157);
            this.pnl.TabIndex = 6;
            // 
            // txtbatchs
            // 
            this.txtbatchs.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbatchs.Location = new System.Drawing.Point(1176, 97);
            this.txtbatchs.Name = "txtbatchs";
            this.txtbatchs.Size = new System.Drawing.Size(189, 40);
            this.txtbatchs.TabIndex = 11;
            // 
            // lblbatchid
            // 
            this.lblbatchid.AutoSize = true;
            this.lblbatchid.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblbatchid.ForeColor = System.Drawing.Color.Black;
            this.lblbatchid.Location = new System.Drawing.Point(1034, 98);
            this.lblbatchid.Name = "lblbatchid";
            this.lblbatchid.Size = new System.Drawing.Size(127, 35);
            this.lblbatchid.TabIndex = 10;
            this.lblbatchid.Text = "Batch ID :";
            this.lblbatchid.Click += new System.EventHandler(this.label3_Click);
            // 
            // btnClr
            // 
            this.btnClr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(125)))), ((int)(((byte)(255)))));
            this.btnClr.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClr.ForeColor = System.Drawing.Color.Black;
            this.btnClr.Location = new System.Drawing.Point(1558, 95);
            this.btnClr.Name = "btnClr";
            this.btnClr.Size = new System.Drawing.Size(168, 48);
            this.btnClr.TabIndex = 8;
            this.btnClr.Text = "Clear";
            this.btnClr.UseVisualStyleBackColor = false;
            this.btnClr.Click += new System.EventHandler(this.btnClr_Click);
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnReport.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.ForeColor = System.Drawing.Color.Black;
            this.btnReport.Location = new System.Drawing.Point(1383, 96);
            this.btnReport.Margin = new System.Windows.Forms.Padding(4);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(168, 48);
            this.btnReport.TabIndex = 6;
            this.btnReport.Text = "Submit";
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // lblHeader
            // 
            this.lblHeader.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.Black;
            this.lblHeader.Location = new System.Drawing.Point(37, 40);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1106, 35);
            this.lblHeader.TabIndex = 4;
            this.lblHeader.Text = "This report displays all services for a specified period, filtered by Created Dat" +
    "e or Service Date.";
            // 
            // dtpDateFilter
            // 
            this.dtpDateFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpDateFilter.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateFilter.FormattingEnabled = true;
            this.dtpDateFilter.Items.AddRange(new object[] {
            "Created Date",
            "Service Date"});
            this.dtpDateFilter.Location = new System.Drawing.Point(817, 96);
            this.dtpDateFilter.Margin = new System.Windows.Forms.Padding(4);
            this.dtpDateFilter.Name = "dtpDateFilter";
            this.dtpDateFilter.Size = new System.Drawing.Size(210, 41);
            this.dtpDateFilter.TabIndex = 5;
            this.dtpDateFilter.Text = "Created Date";
            // 
            // lblDateFilter
            // 
            this.lblDateFilter.AutoSize = true;
            this.lblDateFilter.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateFilter.ForeColor = System.Drawing.Color.Black;
            this.lblDateFilter.Location = new System.Drawing.Point(657, 97);
            this.lblDateFilter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDateFilter.Name = "lblDateFilter";
            this.lblDateFilter.Size = new System.Drawing.Size(152, 35);
            this.lblDateFilter.TabIndex = 4;
            this.lblDateFilter.Text = "Date Filter :";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CalendarFont = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEndDate.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(477, 98);
            this.dtpEndDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(172, 40);
            this.dtpEndDate.TabIndex = 1;
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndDate.ForeColor = System.Drawing.Color.Black;
            this.lblEndDate.Location = new System.Drawing.Point(340, 99);
            this.lblEndDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(129, 35);
            this.lblEndDate.TabIndex = 3;
            this.lblEndDate.Text = "End Date:";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CustomFormat = "";
            this.dtpStartDate.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(160, 96);
            this.dtpStartDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(172, 40);
            this.dtpStartDate.TabIndex = 0;
            this.dtpStartDate.Value = new System.DateTime(2024, 6, 5, 15, 21, 59, 0);
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged);
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartDate.ForeColor = System.Drawing.Color.Black;
            this.lblStartDate.Location = new System.Drawing.Point(10, 98);
            this.lblStartDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(142, 35);
            this.lblStartDate.TabIndex = 1;
            this.lblStartDate.Text = "Start Date:";
            // 
            // txtBatchID
            // 
            this.txtBatchID.AutoCompleteCustomSource.AddRange(new string[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.txtBatchID.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBatchID.Location = new System.Drawing.Point(1205, 93);
            this.txtBatchID.Name = "txtBatchID";
            this.txtBatchID.Size = new System.Drawing.Size(147, 40);
            this.txtBatchID.TabIndex = 11;
            // 
            // lblBatch
            // 
            this.lblBatch.AutoSize = true;
            this.lblBatch.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBatch.ForeColor = System.Drawing.Color.Black;
            this.lblBatch.Location = new System.Drawing.Point(1092, 96);
            this.lblBatch.Name = "lblBatch";
            this.lblBatch.Size = new System.Drawing.Size(107, 35);
            this.lblBatch.TabIndex = 10;
            this.lblBatch.Text = "BatchID";
            // 
            // Column1
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column1.HeaderText = "MMM-YYYY";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 184;
            // 
            // Month
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            this.Month.DefaultCellStyle = dataGridViewCellStyle3;
            this.Month.HeaderText = "Total Service Entries";
            this.Month.MinimumWidth = 6;
            this.Month.Name = "Month";
            this.Month.ReadOnly = true;
            this.Month.Width = 278;
            // 
            // ServiceEntriesSuccessfullyExportedToHCC
            // 
            this.ServiceEntriesSuccessfullyExportedToHCC.HeaderText = "Service Entries Successfully Exported To HCC";
            this.ServiceEntriesSuccessfullyExportedToHCC.MinimumWidth = 6;
            this.ServiceEntriesSuccessfullyExportedToHCC.Name = "ServiceEntriesSuccessfullyExportedToHCC";
            this.ServiceEntriesSuccessfullyExportedToHCC.ReadOnly = true;
            this.ServiceEntriesSuccessfullyExportedToHCC.Width = 558;
            // 
            // ServiceEntriesNotExportedToHCC
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.ServiceEntriesNotExportedToHCC.DefaultCellStyle = dataGridViewCellStyle4;
            this.ServiceEntriesNotExportedToHCC.HeaderText = "Service Entries Not Exported To HCC";
            this.ServiceEntriesNotExportedToHCC.MinimumWidth = 6;
            this.ServiceEntriesNotExportedToHCC.Name = "ServiceEntriesNotExportedToHCC";
            this.ServiceEntriesNotExportedToHCC.ReadOnly = true;
            this.ServiceEntriesNotExportedToHCC.Width = 462;
            // 
            // ServiceEntriesPostTimeboxPeriod
            // 
            this.ServiceEntriesPostTimeboxPeriod.HeaderText = "Service Entries Post Timebox Period";
            this.ServiceEntriesPostTimeboxPeriod.MinimumWidth = 6;
            this.ServiceEntriesPostTimeboxPeriod.Name = "ServiceEntriesPostTimeboxPeriod";
            this.ServiceEntriesPostTimeboxPeriod.ReadOnly = true;
            this.ServiceEntriesPostTimeboxPeriod.Width = 459;
            // 
            // ServiceEntriesForHCCIDMissing
            // 
            this.ServiceEntriesForHCCIDMissing.HeaderText = "Service Entries For HCCID Missing";
            this.ServiceEntriesForHCCIDMissing.MinimumWidth = 6;
            this.ServiceEntriesForHCCIDMissing.Name = "ServiceEntriesForHCCIDMissing";
            this.ServiceEntriesForHCCIDMissing.ReadOnly = true;
            this.ServiceEntriesForHCCIDMissing.Width = 431;
            // 
            // Drop
            // 
            this.Drop.HeaderText = "%Drop";
            this.Drop.MinimumWidth = 6;
            this.Drop.Name = "Drop";
            this.Drop.ReadOnly = true;
            this.Drop.Width = 122;
            // 
            // HCC_Reconciliation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1838, 875);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.pnl);
            this.Name = "HCC_Reconciliation";
            this.Text = "HCC_Reconciliation Report";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.pnl.ResumeLayout(false);
            this.pnl.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Panel pnl;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.ComboBox dtpDateFilter;
        private System.Windows.Forms.Label lblDateFilter;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Button btnClr;
        private System.Windows.Forms.TextBox txtBatchID;
        private System.Windows.Forms.Label lblBatch;
        private System.Windows.Forms.Label lblbatchid;
        private System.Windows.Forms.TextBox txtbatchs;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Month;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServiceEntriesSuccessfullyExportedToHCC;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServiceEntriesNotExportedToHCC;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServiceEntriesPostTimeboxPeriod;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServiceEntriesForHCCIDMissing;
        private System.Windows.Forms.DataGridViewTextBoxColumn Drop;
    }
}