using System;
using System.Windows.Forms;

namespace RWDE
{
    partial class frmConvertToHCC
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHCCConversion = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnl = new System.Windows.Forms.Panel();
            this.txtBatchtype = new System.Windows.Forms.TextBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartFrom = new System.Windows.Forms.Label();
            this.bnClear = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblEndTo = new System.Windows.Forms.Label();
            this.lblBatchType = new System.Windows.Forms.Label();
            this.lblCaption = new System.Windows.Forms.Label();
            this.pnlProgress = new System.Windows.Forms.Panel();
            this.txtTotaltime = new System.Windows.Forms.TextBox();
            this.txtUploadEnded = new System.Windows.Forms.TextBox();
            this.txtUploadStarted = new System.Windows.Forms.TextBox();
            this.txtBatchid = new System.Windows.Forms.TextBox();
            this.lblTotaltime = new System.Windows.Forms.Label();
            this.lblUploadEnded = new System.Windows.Forms.Label();
            this.lblUploadStarts = new System.Windows.Forms.Label();
            this.lblBatch = new System.Windows.Forms.Label();
            this.lblFileInformation = new System.Windows.Forms.Label();
            this.lblServices = new System.Windows.Forms.Label();
            this.progressBarServices = new System.Windows.Forms.ProgressBar();
            this.txtProgressServices = new System.Windows.Forms.TextBox();
            this.prsHeading = new System.Windows.Forms.Label();
            this.lblClient = new System.Windows.Forms.Label();
            this.progressbarHcc = new System.Windows.Forms.ProgressBar();
            this.txtProgresshcc = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btncloseHCC = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btncthcc = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.imageList3 = new System.Windows.Forms.ImageList(this.components);
            this.dummyServiceLineItemsTableAdapter1 = new RWDE.RWDEDataSetTableAdapters.dummyServiceLineItemsTableAdapter();
            this.rwdeDataSet1 = new RWDE.RWDEDataSet();
            this.pnlHCCConversion.SuspendLayout();
            this.pnl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.pnlProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rwdeDataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHCCConversion
            // 
            this.pnlHCCConversion.BackColor = System.Drawing.Color.White;
            this.pnlHCCConversion.Controls.Add(this.label1);
            this.pnlHCCConversion.Controls.Add(this.pnl);
            this.pnlHCCConversion.Controls.Add(this.pnlProgress);
            this.pnlHCCConversion.Controls.Add(this.lblStatus);
            this.pnlHCCConversion.Controls.Add(this.btncloseHCC);
            this.pnlHCCConversion.Controls.Add(this.button4);
            this.pnlHCCConversion.Controls.Add(this.btncthcc);
            this.pnlHCCConversion.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlHCCConversion.ForeColor = System.Drawing.Color.Black;
            this.pnlHCCConversion.Location = new System.Drawing.Point(1, 1);
            this.pnlHCCConversion.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlHCCConversion.Name = "pnlHCCConversion";
            this.pnlHCCConversion.Size = new System.Drawing.Size(5004, 1059);
            this.pnlHCCConversion.TabIndex = 20;
            this.pnlHCCConversion.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlHCCConversion_Paint_1);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(872, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(345, 34);
            this.label1.TabIndex = 0;
            this.label1.Text = "CT to HCC Conversion";
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // pnl
            // 
            this.pnl.BackColor = System.Drawing.Color.White;
            this.pnl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl.Controls.Add(this.txtBatchtype);
            this.pnl.Controls.Add(this.dataGridView);
            this.pnl.Controls.Add(this.dtpStartDate);
            this.pnl.Controls.Add(this.lblStartFrom);
            this.pnl.Controls.Add(this.bnClear);
            this.pnl.Controls.Add(this.btnSubmit);
            this.pnl.Controls.Add(this.dtpEndDate);
            this.pnl.Controls.Add(this.lblEndTo);
            this.pnl.Controls.Add(this.lblBatchType);
            this.pnl.Controls.Add(this.lblCaption);
            this.pnl.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl.Location = new System.Drawing.Point(158, 72);
            this.pnl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(1720, 462);
            this.pnl.TabIndex = 26;
            this.pnl.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_Paint);
            // 
            // txtBatchtype
            // 
            this.txtBatchtype.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBatchtype.ForeColor = System.Drawing.Color.Black;
            this.txtBatchtype.Location = new System.Drawing.Point(185, 86);
            this.txtBatchtype.Name = "txtBatchtype";
            this.txtBatchtype.Size = new System.Drawing.Size(249, 40);
            this.txtBatchtype.TabIndex = 27;
            this.txtBatchtype.Text = "Client Track";
            // 
            // dataGridView
            // 
            this.dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.ColumnHeadersHeight = 51;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
            this.dataGridView.GridColor = System.Drawing.Color.Black;
            this.dataGridView.Location = new System.Drawing.Point(21, 157);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Blue;
            this.dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(1681, 287);
            this.dataGridView.TabIndex = 21;
            this.dataGridView.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataGridView_Scroll);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn1.HeaderText = "Batch ID";
            this.dataGridViewTextBoxColumn1.MaxInputLength = 8;
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 150;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn2.HeaderText = "Batch Type";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 150;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn3.HeaderText = "Description";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 120;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn4.HeaderText = "Start Time";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 120;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn5.HeaderText = "End Time";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 120;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn6.HeaderText = "Status";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 120;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Batch ID";
            this.dataGridViewTextBoxColumn7.HeaderText = "Select";
            this.dataGridViewTextBoxColumn7.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 120;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn8.HeaderText = "Conversion Started At";
            this.dataGridViewTextBoxColumn8.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 220;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(601, 86);
            this.dtpStartDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(204, 36);
            this.dtpStartDate.TabIndex = 24;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged);
            // 
            // lblStartFrom
            // 
            this.lblStartFrom.AutoSize = true;
            this.lblStartFrom.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartFrom.ForeColor = System.Drawing.Color.Black;
            this.lblStartFrom.Location = new System.Drawing.Point(441, 86);
            this.lblStartFrom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStartFrom.Name = "lblStartFrom";
            this.lblStartFrom.Size = new System.Drawing.Size(152, 35);
            this.lblStartFrom.TabIndex = 23;
            this.lblStartFrom.Text = "From Date :";
            this.lblStartFrom.Click += new System.EventHandler(this.lblStartFrom_Click);
            // 
            // bnClear
            // 
            this.bnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.bnClear.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnClear.ForeColor = System.Drawing.Color.Black;
            this.bnClear.Location = new System.Drawing.Point(1475, 83);
            this.bnClear.Margin = new System.Windows.Forms.Padding(4);
            this.bnClear.Name = "bnClear";
            this.bnClear.Size = new System.Drawing.Size(227, 46);
            this.bnClear.TabIndex = 22;
            this.bnClear.Text = "Clear";
            this.bnClear.UseVisualStyleBackColor = false;
            this.bnClear.Click += new System.EventHandler(this.bnClear_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnSubmit.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.ForeColor = System.Drawing.Color.Black;
            this.btnSubmit.Location = new System.Drawing.Point(1224, 83);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(4);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(227, 46);
            this.btnSubmit.TabIndex = 21;
            this.btnSubmit.Text = "Filter";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(968, 85);
            this.dtpEndDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(211, 36);
            this.dtpEndDate.TabIndex = 20;
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.dtpEndDate_ValueChanged);
            // 
            // lblEndTo
            // 
            this.lblEndTo.AutoSize = true;
            this.lblEndTo.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndTo.ForeColor = System.Drawing.Color.Black;
            this.lblEndTo.Location = new System.Drawing.Point(843, 86);
            this.lblEndTo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEndTo.Name = "lblEndTo";
            this.lblEndTo.Size = new System.Drawing.Size(118, 35);
            this.lblEndTo.TabIndex = 19;
            this.lblEndTo.Text = "To Date :";
            this.lblEndTo.Click += new System.EventHandler(this.lblEndTo_Click);
            // 
            // lblBatchType
            // 
            this.lblBatchType.AutoSize = true;
            this.lblBatchType.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBatchType.ForeColor = System.Drawing.Color.Black;
            this.lblBatchType.Location = new System.Drawing.Point(21, 87);
            this.lblBatchType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBatchType.Name = "lblBatchType";
            this.lblBatchType.Size = new System.Drawing.Size(157, 35);
            this.lblBatchType.TabIndex = 18;
            this.lblBatchType.Text = "Batch Type :";
            this.lblBatchType.Click += new System.EventHandler(this.lblBatchType_Click);
            // 
            // lblCaption
            // 
            this.lblCaption.AutoSize = true;
            this.lblCaption.BackColor = System.Drawing.Color.White;
            this.lblCaption.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaption.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCaption.Location = new System.Drawing.Point(15, 12);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(754, 35);
            this.lblCaption.TabIndex = 4;
            this.lblCaption.Text = "Choose a batch from the grid to convert from CT to HCC format.";
            this.lblCaption.Click += new System.EventHandler(this.lblCaption_Click);
            // 
            // pnlProgress
            // 
            this.pnlProgress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlProgress.Controls.Add(this.txtTotaltime);
            this.pnlProgress.Controls.Add(this.txtUploadEnded);
            this.pnlProgress.Controls.Add(this.txtUploadStarted);
            this.pnlProgress.Controls.Add(this.txtBatchid);
            this.pnlProgress.Controls.Add(this.lblTotaltime);
            this.pnlProgress.Controls.Add(this.lblUploadEnded);
            this.pnlProgress.Controls.Add(this.lblUploadStarts);
            this.pnlProgress.Controls.Add(this.lblBatch);
            this.pnlProgress.Controls.Add(this.lblFileInformation);
            this.pnlProgress.Controls.Add(this.lblServices);
            this.pnlProgress.Controls.Add(this.progressBarServices);
            this.pnlProgress.Controls.Add(this.txtProgressServices);
            this.pnlProgress.Controls.Add(this.prsHeading);
            this.pnlProgress.Controls.Add(this.lblClient);
            this.pnlProgress.Controls.Add(this.progressbarHcc);
            this.pnlProgress.Controls.Add(this.txtProgresshcc);
            this.pnlProgress.Location = new System.Drawing.Point(158, 545);
            this.pnlProgress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlProgress.Name = "pnlProgress";
            this.pnlProgress.Size = new System.Drawing.Size(1720, 267);
            this.pnlProgress.TabIndex = 25;
            this.pnlProgress.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlProgress_Paint);
            // 
            // txtTotaltime
            // 
            this.txtTotaltime.BackColor = System.Drawing.Color.White;
            this.txtTotaltime.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotaltime.ForeColor = System.Drawing.Color.Black;
            this.txtTotaltime.Location = new System.Drawing.Point(1271, 196);
            this.txtTotaltime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTotaltime.Name = "txtTotaltime";
            this.txtTotaltime.ReadOnly = true;
            this.txtTotaltime.Size = new System.Drawing.Size(348, 40);
            this.txtTotaltime.TabIndex = 36;
            this.txtTotaltime.TextChanged += new System.EventHandler(this.txtTotaltime_TextChanged);
            // 
            // txtUploadEnded
            // 
            this.txtUploadEnded.BackColor = System.Drawing.Color.White;
            this.txtUploadEnded.Font = new System.Drawing.Font("Calibri", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUploadEnded.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtUploadEnded.Location = new System.Drawing.Point(1271, 147);
            this.txtUploadEnded.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtUploadEnded.Name = "txtUploadEnded";
            this.txtUploadEnded.ReadOnly = true;
            this.txtUploadEnded.Size = new System.Drawing.Size(348, 43);
            this.txtUploadEnded.TabIndex = 35;
            this.txtUploadEnded.TextChanged += new System.EventHandler(this.txtUploadEnded_TextChanged);
            // 
            // txtUploadStarted
            // 
            this.txtUploadStarted.BackColor = System.Drawing.Color.White;
            this.txtUploadStarted.Font = new System.Drawing.Font("Calibri", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUploadStarted.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtUploadStarted.Location = new System.Drawing.Point(1271, 97);
            this.txtUploadStarted.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtUploadStarted.Name = "txtUploadStarted";
            this.txtUploadStarted.ReadOnly = true;
            this.txtUploadStarted.Size = new System.Drawing.Size(348, 43);
            this.txtUploadStarted.TabIndex = 34;
            this.txtUploadStarted.TextChanged += new System.EventHandler(this.txtUploadStarted_TextChanged);
            // 
            // txtBatchid
            // 
            this.txtBatchid.BackColor = System.Drawing.Color.White;
            this.txtBatchid.Font = new System.Drawing.Font("Calibri", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBatchid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtBatchid.Location = new System.Drawing.Point(1271, 46);
            this.txtBatchid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBatchid.Name = "txtBatchid";
            this.txtBatchid.ReadOnly = true;
            this.txtBatchid.Size = new System.Drawing.Size(348, 43);
            this.txtBatchid.TabIndex = 33;
            this.txtBatchid.TextChanged += new System.EventHandler(this.txtBatchid_TextChanged);
            // 
            // lblTotaltime
            // 
            this.lblTotaltime.AutoSize = true;
            this.lblTotaltime.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotaltime.ForeColor = System.Drawing.Color.Black;
            this.lblTotaltime.Location = new System.Drawing.Point(1015, 200);
            this.lblTotaltime.Name = "lblTotaltime";
            this.lblTotaltime.Size = new System.Drawing.Size(212, 35);
            this.lblTotaltime.TabIndex = 32;
            this.lblTotaltime.Text = "Total Time Taken:";
            this.lblTotaltime.Click += new System.EventHandler(this.lblTotaltime_Click);
            // 
            // lblUploadEnded
            // 
            this.lblUploadEnded.AutoSize = true;
            this.lblUploadEnded.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUploadEnded.ForeColor = System.Drawing.Color.Black;
            this.lblUploadEnded.Location = new System.Drawing.Point(963, 151);
            this.lblUploadEnded.Name = "lblUploadEnded";
            this.lblUploadEnded.Size = new System.Drawing.Size(261, 35);
            this.lblUploadEnded.TabIndex = 31;
            this.lblUploadEnded.Text = "Conversion Ended At:";
            this.lblUploadEnded.Click += new System.EventHandler(this.lblUploadEnded_Click);
            // 
            // lblUploadStarts
            // 
            this.lblUploadStarts.AutoSize = true;
            this.lblUploadStarts.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUploadStarts.ForeColor = System.Drawing.Color.Black;
            this.lblUploadStarts.Location = new System.Drawing.Point(952, 100);
            this.lblUploadStarts.Name = "lblUploadStarts";
            this.lblUploadStarts.Size = new System.Drawing.Size(271, 35);
            this.lblUploadStarts.TabIndex = 30;
            this.lblUploadStarts.Text = "Conversion Started At:";
            this.lblUploadStarts.Click += new System.EventHandler(this.lblUploadStarts_Click);
            // 
            // lblBatch
            // 
            this.lblBatch.AutoSize = true;
            this.lblBatch.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBatch.ForeColor = System.Drawing.Color.Black;
            this.lblBatch.Location = new System.Drawing.Point(1105, 50);
            this.lblBatch.Name = "lblBatch";
            this.lblBatch.Size = new System.Drawing.Size(117, 35);
            this.lblBatch.TabIndex = 29;
            this.lblBatch.Text = "Batch ID:";
            this.lblBatch.Click += new System.EventHandler(this.lblBatch_Click);
            // 
            // lblFileInformation
            // 
            this.lblFileInformation.AutoSize = true;
            this.lblFileInformation.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileInformation.ForeColor = System.Drawing.Color.Black;
            this.lblFileInformation.Location = new System.Drawing.Point(905, 0);
            this.lblFileInformation.Name = "lblFileInformation";
            this.lblFileInformation.Size = new System.Drawing.Size(290, 35);
            this.lblFileInformation.TabIndex = 28;
            this.lblFileInformation.Text = "Conversion Information";
            this.lblFileInformation.Click += new System.EventHandler(this.lblFileInformation_Click);
            // 
            // lblServices
            // 
            this.lblServices.AutoSize = true;
            this.lblServices.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServices.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblServices.Location = new System.Drawing.Point(85, 139);
            this.lblServices.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblServices.Name = "lblServices";
            this.lblServices.Size = new System.Drawing.Size(130, 35);
            this.lblServices.TabIndex = 25;
            this.lblServices.Text = "#Services:";
            this.lblServices.Click += new System.EventHandler(this.lblServices_Click);
            // 
            // progressBarServices
            // 
            this.progressBarServices.Location = new System.Drawing.Point(240, 139);
            this.progressBarServices.Margin = new System.Windows.Forms.Padding(4);
            this.progressBarServices.Name = "progressBarServices";
            this.progressBarServices.Size = new System.Drawing.Size(240, 39);
            this.progressBarServices.TabIndex = 24;
            this.progressBarServices.Click += new System.EventHandler(this.progressBarServices_Click);
            // 
            // txtProgressServices
            // 
            this.txtProgressServices.BackColor = System.Drawing.Color.White;
            this.txtProgressServices.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProgressServices.Location = new System.Drawing.Point(500, 139);
            this.txtProgressServices.Margin = new System.Windows.Forms.Padding(4);
            this.txtProgressServices.Name = "txtProgressServices";
            this.txtProgressServices.ReadOnly = true;
            this.txtProgressServices.Size = new System.Drawing.Size(240, 40);
            this.txtProgressServices.TabIndex = 23;
            this.txtProgressServices.Text = "0%";
            this.txtProgressServices.TextChanged += new System.EventHandler(this.txtProgressServices_TextChanged);
            // 
            // prsHeading
            // 
            this.prsHeading.AutoSize = true;
            this.prsHeading.BackColor = System.Drawing.Color.White;
            this.prsHeading.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prsHeading.ForeColor = System.Drawing.Color.Black;
            this.prsHeading.Location = new System.Drawing.Point(3, 0);
            this.prsHeading.Name = "prsHeading";
            this.prsHeading.Size = new System.Drawing.Size(114, 35);
            this.prsHeading.TabIndex = 15;
            this.prsHeading.Text = "Progress";
            this.prsHeading.Click += new System.EventHandler(this.prsHeading_Click);
            // 
            // lblClient
            // 
            this.lblClient.AutoSize = true;
            this.lblClient.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClient.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblClient.Location = new System.Drawing.Point(113, 70);
            this.lblClient.Name = "lblClient";
            this.lblClient.Size = new System.Drawing.Size(102, 35);
            this.lblClient.TabIndex = 3;
            this.lblClient.Text = "#Client:";
            this.lblClient.Click += new System.EventHandler(this.label4_Click);
            // 
            // progressbarHcc
            // 
            this.progressbarHcc.Location = new System.Drawing.Point(240, 65);
            this.progressbarHcc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progressbarHcc.Name = "progressbarHcc";
            this.progressbarHcc.Size = new System.Drawing.Size(240, 39);
            this.progressbarHcc.TabIndex = 2;
            this.progressbarHcc.Click += new System.EventHandler(this.progressbarHcc_Click_1);
            // 
            // txtProgresshcc
            // 
            this.txtProgresshcc.BackColor = System.Drawing.Color.White;
            this.txtProgresshcc.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProgresshcc.Location = new System.Drawing.Point(500, 63);
            this.txtProgresshcc.Margin = new System.Windows.Forms.Padding(4);
            this.txtProgresshcc.Name = "txtProgresshcc";
            this.txtProgresshcc.ReadOnly = true;
            this.txtProgresshcc.Size = new System.Drawing.Size(240, 40);
            this.txtProgresshcc.TabIndex = 14;
            this.txtProgresshcc.Text = "0%";
            this.txtProgresshcc.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(148, 640);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 35);
            this.lblStatus.TabIndex = 23;
            this.lblStatus.Click += new System.EventHandler(this.label1_Click);
            // 
            // btncloseHCC
            // 
            this.btncloseHCC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btncloseHCC.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncloseHCC.ForeColor = System.Drawing.Color.Black;
            this.btncloseHCC.Location = new System.Drawing.Point(1639, 823);
            this.btncloseHCC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btncloseHCC.Name = "btncloseHCC";
            this.btncloseHCC.Size = new System.Drawing.Size(239, 43);
            this.btncloseHCC.TabIndex = 22;
            this.btncloseHCC.Text = "Close";
            this.btncloseHCC.UseVisualStyleBackColor = false;
            this.btncloseHCC.Click += new System.EventHandler(this.btncloseHCC_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(639, 558);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(0, 0);
            this.button4.TabIndex = 21;
            this.button4.Text = "Closed";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btncthcc
            // 
            this.btncthcc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btncthcc.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncthcc.ForeColor = System.Drawing.Color.Black;
            this.btncthcc.Location = new System.Drawing.Point(1382, 823);
            this.btncthcc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btncthcc.Name = "btncthcc";
            this.btncthcc.Size = new System.Drawing.Size(240, 43);
            this.btncthcc.TabIndex = 20;
            this.btncthcc.Text = "Start Conversion";
            this.btncthcc.UseVisualStyleBackColor = false;
            this.btncthcc.Click += new System.EventHandler(this.btnCTtoHCC_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageList2
            // 
            this.imageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList2.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageList3
            // 
            this.imageList3.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList3.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList3.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // dummyServiceLineItemsTableAdapter1
            // 
            this.dummyServiceLineItemsTableAdapter1.ClearBeforeFill = true;
            // 
            // rwdeDataSet1
            // 
            this.rwdeDataSet1.DataSetName = "RWDEDataSet";
            this.rwdeDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // frmConvertToHCC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1055);
            this.Controls.Add(this.pnlHCCConversion);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmConvertToHCC";
            this.Text = "CT to HCC Conversion";
            this.Load += new System.EventHandler(this.frmConvertToHCC_Load);
            this.pnlHCCConversion.ResumeLayout(false);
            this.pnlHCCConversion.PerformLayout();
            this.pnl.ResumeLayout(false);
            this.pnl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.pnlProgress.ResumeLayout(false);
            this.pnlProgress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rwdeDataSet1)).EndInit();
            this.ResumeLayout(false);

        }

        private void progressBarServices_Click(object sender, EventArgs e)
        {
            
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void pnlProgress_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void pnl_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void frmConvertToHCC_Load(object sender, EventArgs e)
        {
            
        }

        private void label4_Click(object sender, EventArgs e)
        {
            
        }

        private void lblCaption_Click(object sender, EventArgs e)
        {
        }

        private void pnlHCCConversion_Paint_1(object sender, PaintEventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
          
        }
        private void pnlHCCConversion_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void progressbarHcc_Click(object sender, EventArgs e)
        {

        }

        #endregion
        private System.Windows.Forms.Panel pnlHCCConversion;
        private System.Windows.Forms.Button btncloseHCC;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btncthcc;
        private System.Windows.Forms.TextBox txtProgresshcc;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.Label lblClient;
        private System.Windows.Forms.ProgressBar progressbarHcc;
        private Label lblStatus;
        private Panel pnlProgress;
        private Label prsHeading;
        private ImageList imageList1;
        private ImageList imageList2;
        private ImageList imageList3;
        private RWDEDataSetTableAdapters.dummyServiceLineItemsTableAdapter dummyServiceLineItemsTableAdapter1;
        private Panel pnl;
        private Label lblServices;
        private ProgressBar progressBarServices;
        private TextBox txtProgressServices;
        private Label label1;
        private TextBox txtTotaltime;
        private TextBox txtUploadEnded;
        private TextBox txtUploadStarted;
        private TextBox txtBatchid;
        private Label lblTotaltime;
        private Label lblUploadEnded;
        private Label lblUploadStarts;
        private Label lblBatch;
        private Label lblFileInformation;
        private DateTimePicker dtpStartDate;
        private Label lblStartFrom;
        private Button bnClear;
        private Button btnSubmit;
        private DateTimePicker dtpEndDate;
        private Label lblEndTo;
        private Label lblBatchType;
        private RWDEDataSet rwdeDataSet1;
        private DataGridView dataGridView;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private TextBox txtBatchtype;
    }
}