﻿using System;
using System.Windows.Forms;

namespace RWDE
{
    sealed partial class ServiceReconciliationReport
    {
        ///<summary>
        ///Required designer variable.
        ///</summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DateTimePicker startDatePicker;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.rwdeDataSet1 = new RWDE.RWDEDataSet();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnDownload = new System.Windows.Forms.Button();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblDateFilter = new System.Windows.Forms.Label();
            this.dtpDateFilter = new System.Windows.Forms.ComboBox();
            this.btnReport = new System.Windows.Forms.Button();
            this.pnl = new System.Windows.Forms.Panel();
            this.txtBatchID = new System.Windows.Forms.TextBox();
            this.lblBatch = new System.Windows.Forms.Label();
            this.btnClr = new System.Windows.Forms.Button();
            this.lblHeader = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.SerialNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServiceIDdata = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServiceId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Agency_client_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AriesConsentExprireDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CaseManager = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServiceGroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServiceCodeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AriesContractId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitsOfService = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ActualMinutesSpent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServiceExportedToAries = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Service_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EntryDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lagstatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AriesExportFailureReason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rwdeDataSet2 = new RWDE.RWDEDataSet();
            ((System.ComponentModel.ISupportInitialize)(this.rwdeDataSet1)).BeginInit();
            this.pnl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rwdeDataSet2)).BeginInit();
            this.SuspendLayout();
            //
            //rwdeDataSet1
            //
            this.rwdeDataSet1.DataSetName = "RWDEDataSet";
            this.rwdeDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            //
            //btnClose
            //
            this.btnClose.AutoSize = true;
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnClose.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(1633, 838);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(244, 50);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = Constants.Close;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            //
            //lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Location = new System.Drawing.Point(802, 53);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(355, 35);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "Service Reconciliation Report";
            //
            //btnDownload
            //
            this.btnDownload.AutoSize = true;
            this.btnDownload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnDownload.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.ForeColor = System.Drawing.Color.Black;
            this.btnDownload.Location = new System.Drawing.Point(1382, 838);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(244, 50);
            this.btnDownload.TabIndex = 5;
            this.btnDownload.Text = "Export";
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
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
            this.lblStartDate.Click += new System.EventHandler(this.lblStartDate_Click);
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
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged);
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
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.dtpEndDate_ValueChanged);
            //
            //lblDateFilter
            //
            this.lblDateFilter.AutoSize = true;
            this.lblDateFilter.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateFilter.ForeColor = System.Drawing.Color.Black;
            this.lblDateFilter.Location = new System.Drawing.Point(684, 95);
            this.lblDateFilter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDateFilter.Name = "lblDateFilter";
            this.lblDateFilter.Size = new System.Drawing.Size(152, 35);
            this.lblDateFilter.TabIndex = 4;
            this.lblDateFilter.Text = "Date Filter :";
            //
            //dtpDateFilter
            //
            this.dtpDateFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dtpDateFilter.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateFilter.FormattingEnabled = true;
            this.dtpDateFilter.Items.AddRange(new object[] {
            "Created Date",
            "Service Date"});
            this.dtpDateFilter.Location = new System.Drawing.Point(839, 94);
            this.dtpDateFilter.Margin = new System.Windows.Forms.Padding(4);
            this.dtpDateFilter.Name = "dtpDateFilter";
            this.dtpDateFilter.Size = new System.Drawing.Size(247, 41);
            this.dtpDateFilter.TabIndex = 5;
            this.dtpDateFilter.Text = "Created Date";
            //
            //btnReport
            //
            this.btnReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnReport.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.ForeColor = System.Drawing.Color.Black;
            this.btnReport.Location = new System.Drawing.Point(1365, 92);
            this.btnReport.Margin = new System.Windows.Forms.Padding(4);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(168, 48);
            this.btnReport.TabIndex = 6;
            this.btnReport.Text = "Submit";
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            //
            //pnl
            //
            this.pnl.AutoSize = true;
            this.pnl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl.Controls.Add(this.txtBatchID);
            this.pnl.Controls.Add(this.lblBatch);
            this.pnl.Controls.Add(this.btnClr);
            this.pnl.Controls.Add(this.btnReport);
            this.pnl.Controls.Add(this.lblHeader);
            this.pnl.Controls.Add(this.dtpDateFilter);
            this.pnl.Controls.Add(this.lblDateFilter);
            this.pnl.Controls.Add(this.dtpEndDate);
            this.pnl.Controls.Add(this.lblEndDate);
            this.pnl.Controls.Add(this.dtpStartDate);
            this.pnl.Controls.Add(this.lblStartDate);
            this.pnl.Location = new System.Drawing.Point(139, 101);
            this.pnl.Margin = new System.Windows.Forms.Padding(4);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(1738, 157);
            this.pnl.TabIndex = 0;
            //
            //txtBatchID
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
            this.txtBatchID.Location = new System.Drawing.Point(1211, 95);
            this.txtBatchID.Name = "txtBatchID";
            this.txtBatchID.Size = new System.Drawing.Size(147, 40);
            this.txtBatchID.TabIndex = 9;
            //
            //lblBatch
            //
            this.lblBatch.AutoSize = true;
            this.lblBatch.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBatch.ForeColor = System.Drawing.Color.Black;
            this.lblBatch.Location = new System.Drawing.Point(1098, 98);
            this.lblBatch.Name = "lblBatch";
            this.lblBatch.Size = new System.Drawing.Size(107, 35);
            this.lblBatch.TabIndex = 8;
            this.lblBatch.Text = Constants.BatchId;
            //
            //btnClr
            //
            this.btnClr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(125)))), ((int)(((byte)(255)))));
            this.btnClr.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClr.ForeColor = System.Drawing.Color.Black;
            this.btnClr.Location = new System.Drawing.Point(1540, 93);
            this.btnClr.Name = "btnClr";
            this.btnClr.Size = new System.Drawing.Size(168, 48);
            this.btnClr.TabIndex = 7;
            this.btnClr.Text = "Clear";
            this.btnClr.UseVisualStyleBackColor = false;
            this.btnClr.Click += new System.EventHandler(this.btnClr_Click);
            //
            //lblHeader
            //
            this.lblHeader.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.Black;
            this.lblHeader.Location = new System.Drawing.Point(37, 38);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1106, 35);
            this.lblHeader.TabIndex = 4;
            this.lblHeader.Text = "This report displays all services for a specified period, filtered by Created Dat" +
    "e or Service Date.";
            //
            //dataGridView
            //
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.ColumnHeadersHeight = 70;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SerialNo,
            this.BatchId,
            this.ServiceIDdata,
            this.ServiceId,
            this.Agency_client_2,
            this.Status,
            this.AriesConsentExprireDate,
            this.CaseManager,
            this.ServiceGroup,
            this.ServiceCodeID,
            this.AriesContractId,
            this.UnitsOfService,
            this.ActualMinutesSpent,
            this.ServiceExportedToAries,
            this.Service_date,
            this.EntryDate,
            this.Lag,
            this.Lagstatus,
            this.AriesExportFailureReason});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView.EnableHeadersVisualStyles = false;
            this.dataGridView.GridColor = System.Drawing.Color.Black;
            this.dataGridView.Location = new System.Drawing.Point(143, 269);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.Size = new System.Drawing.Size(1734, 562);
            this.dataGridView.TabIndex = 1;
            //
            //SerialNo
            //
            this.SerialNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SerialNo.HeaderText = Constants.SlNo;
            this.SerialNo.MinimumWidth = 6;
            this.SerialNo.Name = "SerialNo";
            this.SerialNo.ReadOnly = true;
            this.SerialNo.Width = 90;
            //
            //BatchId
            //
            this.BatchId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.BatchId.HeaderText = Constants.BatchId;
            this.BatchId.MinimumWidth = 6;
            this.BatchId.Name = "BatchId";
            this.BatchId.ReadOnly = true;
            this.BatchId.Width = 110;
            //
            //ServiceIDdata
            //
            this.ServiceIDdata.HeaderText = "ServiceID";
            this.ServiceIDdata.MinimumWidth = 6;
            this.ServiceIDdata.Name = "ServiceIDdata";
            this.ServiceIDdata.ReadOnly = true;
            this.ServiceIDdata.Width = 132;
            //
            //ServiceId
            //
            this.ServiceId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ServiceId.HeaderText = "Staff ";
            this.ServiceId.MinimumWidth = 6;
            this.ServiceId.Name = "ServiceId";
            this.ServiceId.ReadOnly = true;
            this.ServiceId.Width = 80;
            //
            //Agency_client_2
            //
            this.Agency_client_2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Agency_client_2.HeaderText = Constants.HccIdsp;
            this.Agency_client_2.MinimumWidth = 6;
            this.Agency_client_2.Name = "Agency_client_2";
            this.Agency_client_2.ReadOnly = true;
            this.Agency_client_2.Width = 110;
            //
            //Status
            //
            this.Status.HeaderText = Constants.Status;
            this.Status.MinimumWidth = 6;
            this.Status.Name = Constants.Status;
            this.Status.ReadOnly = true;
            //
            //AriesConsentExprireDate
            //
            this.AriesConsentExprireDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.AriesConsentExprireDate.HeaderText = Constants.HccConsentExpiryDatesp;
            this.AriesConsentExprireDate.MinimumWidth = 6;
            this.AriesConsentExprireDate.Name = "AriesConsentExprireDate";
            this.AriesConsentExprireDate.ReadOnly = true;
            this.AriesConsentExprireDate.Width = 200;
            //
            //CaseManager
            //
            this.CaseManager.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CaseManager.HeaderText = "RWEligibility ExpiryDate";
            this.CaseManager.MinimumWidth = 6;
            this.CaseManager.Name = "CaseManager";
            this.CaseManager.ReadOnly = true;
            this.CaseManager.Width = 200;
            //
            //ServiceGroup
            //
            this.ServiceGroup.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ServiceGroup.HeaderText = "Service ";
            this.ServiceGroup.MinimumWidth = 6;
            this.ServiceGroup.Name = "ServiceGroup";
            this.ServiceGroup.ReadOnly = true;
            this.ServiceGroup.Width = 125;
            //
            //ServiceCodeID
            //
            this.ServiceCodeID.HeaderText = Constants.ServiceCodeId;
            this.ServiceCodeID.MinimumWidth = 6;
            this.ServiceCodeID.Name = Constants.ServiceCodeId;
            this.ServiceCodeID.ReadOnly = true;
            this.ServiceCodeID.Width = 180;
            //
            //AriesContractId
            //
            this.AriesContractId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.AriesContractId.HeaderText = "HCC Contract Name";
            this.AriesContractId.MinimumWidth = 6;
            this.AriesContractId.Name = "AriesContractId";
            this.AriesContractId.ReadOnly = true;
            this.AriesContractId.Width = 180;
            //
            //UnitsOfService
            //
            this.UnitsOfService.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.UnitsOfService.HeaderText = "Units Of Service";
            this.UnitsOfService.MinimumWidth = 6;
            this.UnitsOfService.Name = "UnitsOfService";
            this.UnitsOfService.ReadOnly = true;
            this.UnitsOfService.Width = 200;
            //
            //ActualMinutesSpent
            //
            this.ActualMinutesSpent.HeaderText = "Actual Minutes Spent";
            this.ActualMinutesSpent.MinimumWidth = 6;
            this.ActualMinutesSpent.Name = "ActualMinutesSpent";
            this.ActualMinutesSpent.ReadOnly = true;
            this.ActualMinutesSpent.Width = 224;
            //
            //ServiceExportedToAries
            //
            this.ServiceExportedToAries.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ServiceExportedToAries.HeaderText = "Service Exported to HCC";
            this.ServiceExportedToAries.MinimumWidth = 6;
            this.ServiceExportedToAries.Name = "ServiceExportedToAries";
            this.ServiceExportedToAries.ReadOnly = true;
            this.ServiceExportedToAries.Width = 200;
            //
            //Service_date
            //
            this.Service_date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Service_date.HeaderText = "Service Date";
            this.Service_date.MinimumWidth = 6;
            this.Service_date.Name = "Service_date";
            this.Service_date.ReadOnly = true;
            this.Service_date.Width = 140;
            //
            //EntryDate
            //
            this.EntryDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.EntryDate.HeaderText = "Entry Date";
            this.EntryDate.MinimumWidth = 6;
            this.EntryDate.Name = "EntryDate";
            this.EntryDate.ReadOnly = true;
            this.EntryDate.Width = 135;
            //
            //Lag
            //
            this.Lag.HeaderText = "Lag";
            this.Lag.MinimumWidth = 6;
            this.Lag.Name = "Lag";
            this.Lag.ReadOnly = true;
            this.Lag.Width = 73;
            //
            //Lagstatus
            //
            this.Lagstatus.HeaderText = "Lag Status";
            this.Lagstatus.MinimumWidth = 6;
            this.Lagstatus.Name = "Lagstatus";
            this.Lagstatus.ReadOnly = true;
            this.Lagstatus.Width = 126;
            //
            //AriesExportFailureReason
            //
            this.AriesExportFailureReason.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.AriesExportFailureReason.HeaderText = "HCC Export Failure Reason";
            this.AriesExportFailureReason.MinimumWidth = 6;
            this.AriesExportFailureReason.Name = "AriesExportFailureReason";
            this.AriesExportFailureReason.ReadOnly = true;
            this.AriesExportFailureReason.Width = 200;
            //
            //rwdeDataSet2
            //
            this.rwdeDataSet2.DataSetName = "RWDEDataSet";
            this.rwdeDataSet2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            //
            //ServiceReconciliationReport
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
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ServiceReconciliationReport";
            this.Text = "Service Reconciliation Report";
            this.Load += new System.EventHandler(this.ServiceReconciliationReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rwdeDataSet1)).EndInit();
            this.pnl.ResumeLayout(false);
            this.pnl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rwdeDataSet2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private RWDEDataSet rwdeDataSet1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnDownload;
        private Label lblStartDate;
        private DateTimePicker dtpStartDate;
        private Label lblEndDate;
        private DateTimePicker dtpEndDate;
        private Label lblDateFilter;
        private ComboBox dtpDateFilter;
        private Button btnReport;
        private Panel pnl;
        private Label lblHeader;
        private Button btnClr;
        private DataGridView dataGridView;
        private void lblStartDate_Click(object sender, EventArgs e)
        {

        }
        private void ServiceReconciliationReport_Load(object sender, EventArgs e)
        {
            //Initialization code if needed
        }

        private void btnReports_Click(object sender, EventArgs e)
        {

        }
        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {

        }
        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {

        }
        private TextBox txtBatchID;
        private Label lblBatch;
        private RWDEDataSet rwdeDataSet2;
        private DataGridViewTextBoxColumn SerialNo;
        private DataGridViewTextBoxColumn BatchId;
        private DataGridViewTextBoxColumn ServiceIDdata;
        private DataGridViewTextBoxColumn ServiceId;
        private DataGridViewTextBoxColumn Agency_client_2;
        private DataGridViewTextBoxColumn Status;
        private DataGridViewTextBoxColumn AriesConsentExprireDate;
        private DataGridViewTextBoxColumn CaseManager;
        private DataGridViewTextBoxColumn ServiceGroup;
        private DataGridViewTextBoxColumn ServiceCodeID;
        private DataGridViewTextBoxColumn AriesContractId;
        private DataGridViewTextBoxColumn UnitsOfService;
        private DataGridViewTextBoxColumn ActualMinutesSpent;
        private DataGridViewTextBoxColumn ServiceExportedToAries;
        private DataGridViewTextBoxColumn Service_date;
        private DataGridViewTextBoxColumn EntryDate;
        private DataGridViewTextBoxColumn Lag;
        private DataGridViewTextBoxColumn Lagstatus;
        private DataGridViewTextBoxColumn AriesExportFailureReason;
    }
    }