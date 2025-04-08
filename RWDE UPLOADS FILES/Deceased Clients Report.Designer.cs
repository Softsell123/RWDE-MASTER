using System;
using System.Windows.Forms;

namespace RWDE
{
    partial class DeceasedClients
    {
        ///<summary>
        ///Required designer variable.
        ///</summary>
        private System.ComponentModel.IContainer components = null;

        ///<summary>
        ///Clean up any resources being used.
        ///</summary>
        ///<param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        ///<summary>
        ///Required method for Designer support - do not modify
        ///the contents of this method with the code editor.
        ///</summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnDownload = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.pnl = new System.Windows.Forms.Panel();
            this.btnClr = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.lblHeader = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.SINo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HCCID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClientId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AriesId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Program = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Classification = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DownloadDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Extracted = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExtractionDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMSMatch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMSMatchDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreatedOn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.pnl.SuspendLayout();
            this.SuspendLayout();
            //
            //btnDownload
            //
            this.btnDownload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnDownload.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.ForeColor = System.Drawing.Color.Black;
            this.btnDownload.Location = new System.Drawing.Point(1372, 721);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(244, 50);
            this.btnDownload.TabIndex = 10;
            this.btnDownload.Text = "Export";
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            //
            //lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Location = new System.Drawing.Point(849, 52);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(298, 35);
            this.lblTitle.TabIndex = 9;
            this.lblTitle.Text = "Deceased Clients Report";
            //
            //btnClose
            //
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnClose.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(1632, 721);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(244, 50);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            //
            //dataGridView
            //
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.ColumnHeadersHeight = 51;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SINo,
            this.HCCID,
            this.ClientId,
            this.AriesId,
            this.Program,
            this.Classification,
            this.DownloadDate,
            this.Extracted,
            this.ExtractionDate,
            this.CMSMatch,
            this.CMSMatchDate,
            this.Count,
            this.CreatedOn});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView.EnableHeadersVisualStyles = false;
            this.dataGridView.GridColor = System.Drawing.Color.Black;
            this.dataGridView.Location = new System.Drawing.Point(153, 294);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.Size = new System.Drawing.Size(1723, 410);
            this.dataGridView.TabIndex = 7;
            //
            //pnl
            //
            this.pnl.AutoSize = true;
            this.pnl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl.Controls.Add(this.btnClr);
            this.pnl.Controls.Add(this.btnReport);
            this.pnl.Controls.Add(this.lblHeader);
            this.pnl.Controls.Add(this.dtpEndDate);
            this.pnl.Controls.Add(this.lblEndDate);
            this.pnl.Controls.Add(this.dtpStartDate);
            this.pnl.Controls.Add(this.lblStartDate);
            this.pnl.Location = new System.Drawing.Point(151, 106);
            this.pnl.Margin = new System.Windows.Forms.Padding(4);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(1725, 157);
            this.pnl.TabIndex = 6;
            //
            //btnClr
            //
            this.btnClr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnClr.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClr.ForeColor = System.Drawing.Color.Black;
            this.btnClr.Location = new System.Drawing.Point(902, 91);
            this.btnClr.Name = "btnClr";
            this.btnClr.Size = new System.Drawing.Size(168, 48);
            this.btnClr.TabIndex = 7;
            this.btnClr.Text = "Clear";
            this.btnClr.UseVisualStyleBackColor = false;
            this.btnClr.Click += new System.EventHandler(this.btnClr_Click);
            //
            //btnReport
            //
            this.btnReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnReport.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.ForeColor = System.Drawing.Color.Black;
            this.btnReport.Location = new System.Drawing.Point(701, 91);
            this.btnReport.Margin = new System.Windows.Forms.Padding(4);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(168, 48);
            this.btnReport.TabIndex = 6;
            this.btnReport.Text = "Submit";
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            //
            //lblHeader
            //
            this.lblHeader.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.Black;
            this.lblHeader.Location = new System.Drawing.Point(43, 20);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(686, 35);
            this.lblHeader.TabIndex = 4;
            this.lblHeader.Text = "This report displays all clients confirmed deceased in HCC.";
            //
            //dtpEndDate
            //
            this.dtpEndDate.CalendarFont = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEndDate.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(501, 95);
            this.dtpEndDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(172, 40);
            this.dtpEndDate.TabIndex = 1;
            //
            //lblEndDate
            //
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndDate.ForeColor = System.Drawing.Color.Black;
            this.lblEndDate.Location = new System.Drawing.Point(371, 97);
            this.lblEndDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(129, 35);
            this.lblEndDate.TabIndex = 3;
            this.lblEndDate.Text = "End Date:";
            //
            //dtpStartDate
            //
            this.dtpStartDate.CustomFormat = "";
            this.dtpStartDate.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(184, 97);
            this.dtpStartDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(172, 40);
            this.dtpStartDate.TabIndex = 0;
            this.dtpStartDate.Value = new System.DateTime(2024, 6, 5, 15, 21, 59, 0);
            //
            //lblStartDate
            //
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartDate.ForeColor = System.Drawing.Color.Black;
            this.lblStartDate.Location = new System.Drawing.Point(37, 98);
            this.lblStartDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(142, 35);
            this.lblStartDate.TabIndex = 1;
            this.lblStartDate.Text = "Start Date:";
            //
            //SINo
            //
            this.SINo.HeaderText = "SI No";
            this.SINo.MinimumWidth = 6;
            this.SINo.Name = "SINo";
            this.SINo.ReadOnly = true;
            this.SINo.Width = 103;
            //
            //HCCID
            //
            this.HCCID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.HCCID.DefaultCellStyle = dataGridViewCellStyle2;
            this.HCCID.HeaderText = "HCC ID";
            this.HCCID.MinimumWidth = 6;
            this.HCCID.Name = "HCCID";
            this.HCCID.ReadOnly = true;
            this.HCCID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.HCCID.Width = 200;
            //
            //ClientId
            //
            this.ClientId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ClientId.HeaderText = "Client Last First Name";
            this.ClientId.MinimumWidth = 6;
            this.ClientId.Name = "ClientId";
            this.ClientId.ReadOnly = true;
            this.ClientId.Width = 200;
            //
            //AriesId
            //
            this.AriesId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.AriesId.HeaderText = "Status";
            this.AriesId.MinimumWidth = 6;
            this.AriesId.Name = "AriesId";
            this.AriesId.ReadOnly = true;
            this.AriesId.Width = 200;
            //
            //Program
            //
            this.Program.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Program.HeaderText = "Date of Death";
            this.Program.MinimumWidth = 6;
            this.Program.Name = "Program";
            this.Program.ReadOnly = true;
            this.Program.Width = 200;
            //
            //Classification
            //
            this.Classification.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Classification.HeaderText = "Download Date";
            this.Classification.MinimumWidth = 6;
            this.Classification.Name = "Classification";
            this.Classification.ReadOnly = true;
            this.Classification.Width = 200;
            //
            //DownloadDate
            //
            this.DownloadDate.HeaderText = "Last service Date";
            this.DownloadDate.MinimumWidth = 6;
            this.DownloadDate.Name = "DownloadDate";
            this.DownloadDate.ReadOnly = true;
            this.DownloadDate.Width = 240;
            //
            //Extracted
            //
            this.Extracted.HeaderText = "Extracted Y/N";
            this.Extracted.MinimumWidth = 6;
            this.Extracted.Name = "Extracted";
            this.Extracted.ReadOnly = true;
            this.Extracted.Width = 204;
            //
            //ExtractionDate
            //
            this.ExtractionDate.HeaderText = "Extraction Date";
            this.ExtractionDate.MinimumWidth = 6;
            this.ExtractionDate.Name = "ExtractionDate";
            this.ExtractionDate.ReadOnly = true;
            this.ExtractionDate.Width = 224;
            //
            //CMSMatch
            //
            this.CMSMatch.HeaderText = "CMS Match";
            this.CMSMatch.MinimumWidth = 6;
            this.CMSMatch.Name = "CMSMatch";
            this.CMSMatch.ReadOnly = true;
            this.CMSMatch.Width = 177;
            //
            //CMSMatchDate
            //
            this.CMSMatchDate.HeaderText = "CMS Match Date";
            this.CMSMatchDate.MinimumWidth = 6;
            this.CMSMatchDate.Name = "CMSMatchDate";
            this.CMSMatchDate.ReadOnly = true;
            this.CMSMatchDate.Width = 239;
            //
            //Count
            //
            this.Count.HeaderText = "Service Count After Death ";
            this.Count.MinimumWidth = 6;
            this.Count.Name = "Count";
            this.Count.ReadOnly = true;
            this.Count.Width = 351;
            //
            //CreatedOn
            //
            this.CreatedOn.HeaderText = "Created On";
            this.CreatedOn.MinimumWidth = 6;
            this.CreatedOn.Name = "CreatedOn";
            this.CreatedOn.ReadOnly = true;
            this.CreatedOn.Width = 176;
            //
            //DeceasedClients
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1924, 1055);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.pnl);
            this.Name = "DeceasedClients";
            this.Text = "Deceased_Clients Report";
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
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblStartDate;
        private Button btnClr;
        private DataGridViewTextBoxColumn SINo;
        private DataGridViewTextBoxColumn HCCID;
        private DataGridViewTextBoxColumn ClientId;
        private DataGridViewTextBoxColumn AriesId;
        private DataGridViewTextBoxColumn Program;
        private DataGridViewTextBoxColumn Classification;
        private DataGridViewTextBoxColumn DownloadDate;
        private DataGridViewTextBoxColumn Extracted;
        private DataGridViewTextBoxColumn ExtractionDate;
        private DataGridViewTextBoxColumn CMSMatch;
        private DataGridViewTextBoxColumn CMSMatchDate;
        private DataGridViewTextBoxColumn Count;
        private DataGridViewTextBoxColumn CreatedOn;
    }
}