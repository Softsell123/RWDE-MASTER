﻿using System;
using System.Windows.Forms;

namespace RWDE_UPLOADS_FILES
{
    partial class OCHIN_to_RWDE_Conversion
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.batchBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.rWDEDataSet = new RWDE_UPLOADS_FILES.RWDEDataSet();
            this.batchTableAdapter = new RWDE_UPLOADS_FILES.RWDEDataSetTableAdapters.BatchTableAdapter();
            this.pnl = new System.Windows.Forms.Panel();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConversionStartedAt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbBatchType = new System.Windows.Forms.ComboBox();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartFrom = new System.Windows.Forms.Label();
            this.bnClear = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblEndTo = new System.Windows.Forms.Label();
            this.lblBatchType = new System.Windows.Forms.Label();
            this.dataGridViewHCC = new System.Windows.Forms.DataGridView();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblHcc = new System.Windows.Forms.Label();
            this.lblOchin = new System.Windows.Forms.Label();
            this.lblHeading = new System.Windows.Forms.Label();
            this.lblOchinHcc = new System.Windows.Forms.Label();
            this.btnochintorwde = new System.Windows.Forms.Button();
            this.pnlDATA = new System.Windows.Forms.Panel();
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
            this.btncloseHCC = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.pnlOCHINConversion = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.batchBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rWDEDataSet)).BeginInit();
            this.pnl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHCC)).BeginInit();
            this.pnlDATA.SuspendLayout();
            this.pnlOCHINConversion.SuspendLayout();
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
            // pnl
            // 
            this.pnl.AutoSize = true;
            this.pnl.BackColor = System.Drawing.Color.White;
            this.pnl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl.Controls.Add(this.dataGridView);
            this.pnl.Controls.Add(this.cbBatchType);
            this.pnl.Controls.Add(this.dtpStartDate);
            this.pnl.Controls.Add(this.lblStartFrom);
            this.pnl.Controls.Add(this.bnClear);
            this.pnl.Controls.Add(this.btnSubmit);
            this.pnl.Controls.Add(this.dtpEndDate);
            this.pnl.Controls.Add(this.lblEndTo);
            this.pnl.Controls.Add(this.lblBatchType);
            this.pnl.Controls.Add(this.dataGridViewHCC);
            this.pnl.Controls.Add(this.lblHcc);
            this.pnl.Controls.Add(this.lblOchin);
            this.pnl.Controls.Add(this.lblHeading);
            this.pnl.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl.ForeColor = System.Drawing.Color.Black;
            this.pnl.Location = new System.Drawing.Point(134, 40);
            this.pnl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(1785, 519);
            this.pnl.TabIndex = 34;
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
            this.Column1,
            this.type,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.ConversionStartedAt});
            this.dataGridView.GridColor = System.Drawing.Color.Black;
            this.dataGridView.Location = new System.Drawing.Point(35, 154);
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
            this.dataGridView.Size = new System.Drawing.Size(1684, 174);
            this.dataGridView.TabIndex = 1;
            this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentClick);
            // 
            // Column1
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column1.HeaderText = "Batch ID";
            this.Column1.MaxInputLength = 8;
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 200;
            // 
            // type
            // 
            this.type.HeaderText = "BatchType";
            this.type.MinimumWidth = 6;
            this.type.Name = "type";
            this.type.ReadOnly = true;
            this.type.Width = 125;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Description";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 200;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Start Time";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 200;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "End Time";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 200;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Status";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 200;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "Batch ID";
            this.Column6.HeaderText = "Select";
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 200;
            // 
            // ConversionStartedAt
            // 
            this.ConversionStartedAt.HeaderText = "Conversion Started At";
            this.ConversionStartedAt.MinimumWidth = 6;
            this.ConversionStartedAt.Name = "ConversionStartedAt";
            this.ConversionStartedAt.ReadOnly = true;
            this.ConversionStartedAt.Width = 300;
            // 
            // cbBatchType
            // 
            this.cbBatchType.BackColor = System.Drawing.Color.White;
            this.cbBatchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBatchType.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBatchType.FormattingEnabled = true;
            this.cbBatchType.Items.AddRange(new object[] {
            "OCHIN",
            "HCC"});
            this.cbBatchType.Location = new System.Drawing.Point(180, 58);
            this.cbBatchType.Margin = new System.Windows.Forms.Padding(4);
            this.cbBatchType.Name = "cbBatchType";
            this.cbBatchType.Size = new System.Drawing.Size(247, 37);
            this.cbBatchType.TabIndex = 35;
            this.cbBatchType.SelectedIndexChanged += new System.EventHandler(this.cbBatchType_SelectedIndexChanged_1);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(634, 56);
            this.dtpStartDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(204, 36);
            this.dtpStartDate.TabIndex = 34;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged_1);
            // 
            // lblStartFrom
            // 
            this.lblStartFrom.AutoSize = true;
            this.lblStartFrom.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartFrom.ForeColor = System.Drawing.Color.Black;
            this.lblStartFrom.Location = new System.Drawing.Point(469, 59);
            this.lblStartFrom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStartFrom.Name = "lblStartFrom";
            this.lblStartFrom.Size = new System.Drawing.Size(130, 29);
            this.lblStartFrom.TabIndex = 33;
            this.lblStartFrom.Text = "From Date :";
            // 
            // bnClear
            // 
            this.bnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.bnClear.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnClear.ForeColor = System.Drawing.Color.Black;
            this.bnClear.Location = new System.Drawing.Point(1503, 52);
            this.bnClear.Margin = new System.Windows.Forms.Padding(4);
            this.bnClear.Name = "bnClear";
            this.bnClear.Size = new System.Drawing.Size(227, 46);
            this.bnClear.TabIndex = 32;
            this.bnClear.Text = "Clear";
            this.bnClear.UseVisualStyleBackColor = false;
            this.bnClear.Click += new System.EventHandler(this.bnClear_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnSubmit.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.ForeColor = System.Drawing.Color.Black;
            this.btnSubmit.Location = new System.Drawing.Point(1252, 52);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(4);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(227, 46);
            this.btnSubmit.TabIndex = 1;
            this.btnSubmit.Text = "Filter";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click_1);
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(997, 56);
            this.dtpEndDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(211, 36);
            this.dtpEndDate.TabIndex = 30;
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.dtpEndDate_ValueChanged_1);
            // 
            // lblEndTo
            // 
            this.lblEndTo.AutoSize = true;
            this.lblEndTo.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndTo.ForeColor = System.Drawing.Color.Black;
            this.lblEndTo.Location = new System.Drawing.Point(872, 59);
            this.lblEndTo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEndTo.Name = "lblEndTo";
            this.lblEndTo.Size = new System.Drawing.Size(100, 29);
            this.lblEndTo.TabIndex = 29;
            this.lblEndTo.Text = "To Date :";
            this.lblEndTo.Click += new System.EventHandler(this.lblEndTo_Click_1);
            // 
            // lblBatchType
            // 
            this.lblBatchType.AutoSize = true;
            this.lblBatchType.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBatchType.ForeColor = System.Drawing.Color.Black;
            this.lblBatchType.Location = new System.Drawing.Point(39, 61);
            this.lblBatchType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBatchType.Name = "lblBatchType";
            this.lblBatchType.Size = new System.Drawing.Size(133, 29);
            this.lblBatchType.TabIndex = 28;
            this.lblBatchType.Text = "Batch Type :";
            // 
            // dataGridViewHCC
            // 
            this.dataGridViewHCC.AutoGenerateColumns = false;
            this.dataGridViewHCC.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewHCC.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewHCC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewHCC.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column7,
            this.Column8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12});
            this.dataGridViewHCC.DataSource = this.batchBindingSource;
            this.dataGridViewHCC.GridColor = System.Drawing.Color.Black;
            this.dataGridViewHCC.Location = new System.Drawing.Point(34, 373);
            this.dataGridViewHCC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridViewHCC.Name = "dataGridViewHCC";
            this.dataGridViewHCC.ReadOnly = true;
            this.dataGridViewHCC.RowHeadersWidth = 51;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            this.dataGridViewHCC.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewHCC.RowTemplate.Height = 24;
            this.dataGridViewHCC.Size = new System.Drawing.Size(1685, 137);
            this.dataGridViewHCC.TabIndex = 9;
            this.dataGridViewHCC.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewHCC_CellContentClick);
            // 
            // Column7
            // 
            this.Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column7.DataPropertyName = "BatchID";
            this.Column7.HeaderText = "Batch ID";
            this.Column7.MinimumWidth = 6;
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 140;
            // 
            // Column8
            // 
            this.Column8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column8.DataPropertyName = "BatchID";
            this.Column8.HeaderText = "File Name";
            this.Column8.MinimumWidth = 6;
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 140;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "Description";
            this.dataGridViewTextBoxColumn9.HeaderText = "Description";
            this.dataGridViewTextBoxColumn9.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Width = 200;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "StartedAt";
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dataGridViewTextBoxColumn10.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn10.HeaderText = "Conversion Started At";
            this.dataGridViewTextBoxColumn10.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Width = 250;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "EndedAt";
            this.dataGridViewTextBoxColumn11.HeaderText = "Conversion Ended At";
            this.dataGridViewTextBoxColumn11.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.Width = 240;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "Status";
            this.dataGridViewTextBoxColumn12.HeaderText = "Status";
            this.dataGridViewTextBoxColumn12.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.Width = 205;
            // 
            // lblHcc
            // 
            this.lblHcc.AutoSize = true;
            this.lblHcc.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHcc.ForeColor = System.Drawing.Color.Black;
            this.lblHcc.Location = new System.Drawing.Point(29, 336);
            this.lblHcc.Name = "lblHcc";
            this.lblHcc.Size = new System.Drawing.Size(136, 35);
            this.lblHcc.TabIndex = 7;
            this.lblHcc.Text = "HCC Batch";
            this.lblHcc.Click += new System.EventHandler(this.lblHcc_Click);
            // 
            // lblOchin
            // 
            this.lblOchin.AutoSize = true;
            this.lblOchin.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOchin.ForeColor = System.Drawing.Color.Black;
            this.lblOchin.Location = new System.Drawing.Point(38, 108);
            this.lblOchin.Name = "lblOchin";
            this.lblOchin.Size = new System.Drawing.Size(165, 35);
            this.lblOchin.TabIndex = 6;
            this.lblOchin.Text = "OCHIN Batch";
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.BackColor = System.Drawing.Color.White;
            this.lblHeading.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblHeading.Location = new System.Drawing.Point(38, 9);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(933, 35);
            this.lblHeading.TabIndex = 4;
            this.lblHeading.Text = "Choose a batch from the grids below to convert from OCHIN and HCC to RWDE.";
            // 
            // lblOchinHcc
            // 
            this.lblOchinHcc.AutoSize = true;
            this.lblOchinHcc.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOchinHcc.ForeColor = System.Drawing.Color.Black;
            this.lblOchinHcc.Location = new System.Drawing.Point(855, 3);
            this.lblOchinHcc.Name = "lblOchinHcc";
            this.lblOchinHcc.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblOchinHcc.Size = new System.Drawing.Size(338, 35);
            this.lblOchinHcc.TabIndex = 35;
            this.lblOchinHcc.Text = "OCHIN to RWDE Conversion";
            // 
            // btnochintorwde
            // 
            this.btnochintorwde.AutoSize = true;
            this.btnochintorwde.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnochintorwde.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnochintorwde.ForeColor = System.Drawing.Color.Black;
            this.btnochintorwde.Location = new System.Drawing.Point(1434, 817);
            this.btnochintorwde.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnochintorwde.Name = "btnochintorwde";
            this.btnochintorwde.Size = new System.Drawing.Size(240, 45);
            this.btnochintorwde.TabIndex = 37;
            this.btnochintorwde.TabStop = false;
            this.btnochintorwde.Text = "Start Conversion";
            this.btnochintorwde.UseVisualStyleBackColor = false;
            this.btnochintorwde.Click += new System.EventHandler(this.btncthcc_Click_1);
            // 
            // pnlDATA
            // 
            this.pnlDATA.AutoSize = true;
            this.pnlDATA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDATA.Controls.Add(this.txtTotaltime);
            this.pnlDATA.Controls.Add(this.txtUploadEnded);
            this.pnlDATA.Controls.Add(this.txtUploadStarted);
            this.pnlDATA.Controls.Add(this.txtBatchid);
            this.pnlDATA.Controls.Add(this.lblTotaltime);
            this.pnlDATA.Controls.Add(this.lblUploadEnded);
            this.pnlDATA.Controls.Add(this.lblUploadStarts);
            this.pnlDATA.Controls.Add(this.lblBatch);
            this.pnlDATA.Controls.Add(this.lblFileInformation);
            this.pnlDATA.Controls.Add(this.lblServices);
            this.pnlDATA.Controls.Add(this.progressBarServices);
            this.pnlDATA.Controls.Add(this.txtProgressServices);
            this.pnlDATA.Controls.Add(this.prsHeading);
            this.pnlDATA.Controls.Add(this.lblClient);
            this.pnlDATA.Controls.Add(this.progressbarHcc);
            this.pnlDATA.Controls.Add(this.txtProgresshcc);
            this.pnlDATA.Location = new System.Drawing.Point(134, 576);
            this.pnlDATA.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlDATA.Name = "pnlDATA";
            this.pnlDATA.Size = new System.Drawing.Size(1785, 240);
            this.pnlDATA.TabIndex = 26;
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
            this.txtTotaltime.TabStop = false;
            // 
            // txtUploadEnded
            // 
            this.txtUploadEnded.BackColor = System.Drawing.Color.White;
            this.txtUploadEnded.Font = new System.Drawing.Font("Calibri", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUploadEnded.ForeColor = System.Drawing.Color.Black;
            this.txtUploadEnded.Location = new System.Drawing.Point(1271, 147);
            this.txtUploadEnded.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtUploadEnded.Name = "txtUploadEnded";
            this.txtUploadEnded.ReadOnly = true;
            this.txtUploadEnded.Size = new System.Drawing.Size(348, 43);
            this.txtUploadEnded.TabIndex = 35;
            this.txtUploadEnded.TabStop = false;
            // 
            // txtUploadStarted
            // 
            this.txtUploadStarted.BackColor = System.Drawing.Color.White;
            this.txtUploadStarted.Font = new System.Drawing.Font("Calibri", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUploadStarted.ForeColor = System.Drawing.Color.Black;
            this.txtUploadStarted.Location = new System.Drawing.Point(1271, 97);
            this.txtUploadStarted.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtUploadStarted.Name = "txtUploadStarted";
            this.txtUploadStarted.ReadOnly = true;
            this.txtUploadStarted.Size = new System.Drawing.Size(348, 43);
            this.txtUploadStarted.TabIndex = 34;
            this.txtUploadStarted.TabStop = false;
            // 
            // txtBatchid
            // 
            this.txtBatchid.BackColor = System.Drawing.Color.White;
            this.txtBatchid.Font = new System.Drawing.Font("Calibri", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBatchid.ForeColor = System.Drawing.Color.Black;
            this.txtBatchid.Location = new System.Drawing.Point(1271, 46);
            this.txtBatchid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBatchid.Name = "txtBatchid";
            this.txtBatchid.ReadOnly = true;
            this.txtBatchid.Size = new System.Drawing.Size(348, 43);
            this.txtBatchid.TabIndex = 33;
            this.txtBatchid.TabStop = false;
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
            // 
            // progressBarServices
            // 
            this.progressBarServices.Location = new System.Drawing.Point(240, 139);
            this.progressBarServices.Margin = new System.Windows.Forms.Padding(4);
            this.progressBarServices.Name = "progressBarServices";
            this.progressBarServices.Size = new System.Drawing.Size(240, 39);
            this.progressBarServices.TabIndex = 24;
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
            this.txtProgressServices.TabStop = false;
            this.txtProgressServices.Text = "0%";
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
            // 
            // progressbarHcc
            // 
            this.progressbarHcc.Location = new System.Drawing.Point(240, 65);
            this.progressbarHcc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progressbarHcc.Name = "progressbarHcc";
            this.progressbarHcc.Size = new System.Drawing.Size(240, 39);
            this.progressbarHcc.TabIndex = 2;
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
            this.txtProgresshcc.TabStop = false;
            this.txtProgresshcc.Text = "0%";
            // 
            // btncloseHCC
            // 
            this.btncloseHCC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btncloseHCC.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncloseHCC.ForeColor = System.Drawing.Color.Black;
            this.btncloseHCC.Location = new System.Drawing.Point(1680, 816);
            this.btncloseHCC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btncloseHCC.Name = "btncloseHCC";
            this.btncloseHCC.Size = new System.Drawing.Size(239, 45);
            this.btncloseHCC.TabIndex = 38;
            this.btncloseHCC.TabStop = false;
            this.btncloseHCC.Text = "Close";
            this.btncloseHCC.UseVisualStyleBackColor = false;
            this.btncloseHCC.Click += new System.EventHandler(this.btncloseHCC_Click_1);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNext.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.ForeColor = System.Drawing.Color.Black;
            this.btnNext.Location = new System.Drawing.Point(1786, 861);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(124, 54);
            this.btnNext.TabIndex = 42;
            this.btnNext.TabStop = false;
            this.btnNext.Text = "NEXT";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click_1);
            // 
            // pnlOCHINConversion
            // 
            this.pnlOCHINConversion.BackColor = System.Drawing.Color.White;
            this.pnlOCHINConversion.Controls.Add(this.btnochintorwde);
            this.pnlOCHINConversion.Controls.Add(this.btnNext);
            this.pnlOCHINConversion.Controls.Add(this.btncloseHCC);
            this.pnlOCHINConversion.Controls.Add(this.pnlDATA);
            this.pnlOCHINConversion.Controls.Add(this.lblOchinHcc);
            this.pnlOCHINConversion.Controls.Add(this.pnl);
            this.pnlOCHINConversion.Location = new System.Drawing.Point(2, 0);
            this.pnlOCHINConversion.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlOCHINConversion.Name = "pnlOCHINConversion";
            this.pnlOCHINConversion.Size = new System.Drawing.Size(2942, 1006);
            this.pnlOCHINConversion.TabIndex = 6;
            this.pnlOCHINConversion.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // OCHIN_to_RWDE_Conversion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1007);
            this.Controls.Add(this.pnlOCHINConversion);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "OCHIN_to_RWDE_Conversion";
            this.Text = "OCHIN_to_RWDE_Conversion";
            this.Load += new System.EventHandler(this.OCHIN_to_RWDE_Conversion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.batchBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rWDEDataSet)).EndInit();
            this.pnl.ResumeLayout(false);
            this.pnl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHCC)).EndInit();
            this.pnlDATA.ResumeLayout(false);
            this.pnlDATA.PerformLayout();
            this.pnlOCHINConversion.ResumeLayout(false);
            this.pnlOCHINConversion.PerformLayout();
            this.ResumeLayout(false);

        }
        private void lblFileInformation_Click(object sender, EventArgs e)
        {

        }

        private void lblUploadStarts_Click(object sender, EventArgs e)
        {

        }

        private void txtUploadEnded_TextChanged(object sender, EventArgs e)
        {

        }
        private void cbBatchType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void lblStartFrom_Click(object sender, EventArgs e)
        {

        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void lblEndTo_Click(object sender, EventArgs e)
        {

        }

        private void lblBatchType_Click(object sender, EventArgs e)
        {

        }

        private void txtTotaltime_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUploadStarted_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBatchid_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblTotaltime_Click(object sender, EventArgs e)
        {

        }

        private void lblUploadEnded_Click(object sender, EventArgs e)
        {

        }

        private void lblBatch_Click(object sender, EventArgs e)
        {

        }

        private void lblServices_Click(object sender, EventArgs e)
        {

        }

        private void txtProgressServices_TextChanged(object sender, EventArgs e)
        {

        }

        private void prsHeading_Click(object sender, EventArgs e)
        {

        }

        private void progressbarHcc_Click_1(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }



        private void lblEndTo_Click_1(object sender, EventArgs e)
        {


        }

        private void cbBatchType_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
        private void OCHIN_to_RWDE_Conversion_Load(object sender, EventArgs e)
        {

        }
        private void dataGridViewHCC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView.ScrollBars = ScrollBars.Both;

        }

        private void lblHcc_Click(object sender, EventArgs e)
        {
          
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
           
        }

        #endregion
        private RWDEDataSet rWDEDataSet;
        private BindingSource batchBindingSource;
        private RWDEDataSetTableAdapters.BatchTableAdapter batchTableAdapter;
        private Panel pnl;
        private DataGridView dataGridView;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn type;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn ConversionStartedAt;
        private ComboBox cbBatchType;
        private DateTimePicker dtpStartDate;
        private Label lblStartFrom;
        private Button bnClear;
        private Button btnSubmit;
        private DateTimePicker dtpEndDate;
        private Label lblEndTo;
        private Label lblBatchType;
        private DataGridView dataGridViewHCC;
        private Label lblHcc;
        private Label lblOchin;
        private Label lblHeading;
        private Label lblOchinHcc;
        private Button btnochintorwde;
        private Panel pnlDATA;
        private TextBox txtTotaltime;
        private TextBox txtUploadEnded;
        private TextBox txtUploadStarted;
        private TextBox txtBatchid;
        private Label lblTotaltime;
        private Label lblUploadEnded;
        private Label lblUploadStarts;
        private Label lblBatch;
        private Label lblFileInformation;
        private Label lblServices;
        private ProgressBar progressBarServices;
        private TextBox txtProgressServices;
        private Label prsHeading;
        private Label lblClient;
        private ProgressBar progressbarHcc;
        private TextBox txtProgresshcc;
        private Button btncloseHCC;
        private Button btnNext;
        private Panel pnlOCHINConversion;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
    }
   

}
