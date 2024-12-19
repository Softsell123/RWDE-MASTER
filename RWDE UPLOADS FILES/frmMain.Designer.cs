using System;
using System.Windows.Forms;

namespace RWDE
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allBatchesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadHCCCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadCSVToOCHINToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xMLFileUploadsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oCHINToHCCConversionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oCHINToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateHCCXmlFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadHCCErrorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cSVFILESToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serviceReconciliationReportDotNotUseToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deceasedClientsReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadDashboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hCCRECONToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientDemographicsReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.errorLogReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jjToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.printPreviewToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnochincCsv = new System.Windows.Forms.ToolStripButton();
            this.btnXml = new System.Windows.Forms.ToolStripButton();
            this.btnHccCsv = new System.Windows.Forms.ToolStripButton();
            this.btnOCHINHCCConversion = new System.Windows.Forms.ToolStripButton();
            this.btnConversion = new System.Windows.Forms.ToolStripButton();
            this.btnGenerator = new System.Windows.Forms.ToolStripButton();
            this.manualUploadReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("menuStrip.BackgroundImage")));
            this.menuStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.editMenu,
            this.viewMenu,
            this.toolsMenu,
            this.reportsToolStripMenuItem,
            this.jjToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(1766, 28);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            this.menuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip_ItemClicked);
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator3,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator4,
            this.printToolStripMenuItem,
            this.printPreviewToolStripMenuItem,
            this.printSetupToolStripMenuItem,
            this.toolStripSeparator5,
            this.exitToolStripMenuItem});
            this.fileMenu.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(46, 24);
            this.fileMenu.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.ShowNewForm);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenFile);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(178, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.saveToolStripMenuItem.Text = "&Save";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.saveAsToolStripMenuItem.Text = "Save &As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(178, 6);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripMenuItem.Image")));
            this.printToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.printToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.printToolStripMenuItem.Text = "&Print";
            // 
            // printPreviewToolStripMenuItem
            // 
            this.printPreviewToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printPreviewToolStripMenuItem.Image")));
            this.printPreviewToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.printPreviewToolStripMenuItem.Text = "Print Pre&view";
            // 
            // printSetupToolStripMenuItem
            // 
            this.printSetupToolStripMenuItem.Name = "printSetupToolStripMenuItem";
            this.printSetupToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.printSetupToolStripMenuItem.Text = "Print Setup";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(178, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolsStripMenuItem_Click);
            // 
            // editMenu
            // 
            this.editMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator6,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator7,
            this.selectAllToolStripMenuItem});
            this.editMenu.Name = "editMenu";
            this.editMenu.Size = new System.Drawing.Size(49, 24);
            this.editMenu.Text = "&Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("undoToolStripMenuItem.Image")));
            this.undoToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(206, 26);
            this.undoToolStripMenuItem.Text = "&Undo";
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("redoToolStripMenuItem.Image")));
            this.redoToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(206, 26);
            this.redoToolStripMenuItem.Text = "&Redo";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(203, 6);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripMenuItem.Image")));
            this.cutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(206, 26);
            this.cutToolStripMenuItem.Text = "Cu&t";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.CutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
            this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(206, 26);
            this.copyToolStripMenuItem.Text = "&Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripMenuItem.Image")));
            this.pasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(206, 26);
            this.pasteToolStripMenuItem.Text = "&Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.PasteToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(203, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(206, 26);
            this.selectAllToolStripMenuItem.Text = "Select &All";
            // 
            // viewMenu
            // 
            this.viewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBarToolStripMenuItem,
            this.statusBarToolStripMenuItem,
            this.allBatchesToolStripMenuItem});
            this.viewMenu.Name = "viewMenu";
            this.viewMenu.Size = new System.Drawing.Size(97, 24);
            this.viewMenu.Text = "Data Setup";
            // 
            // toolBarToolStripMenuItem
            // 
            this.toolBarToolStripMenuItem.Checked = true;
            this.toolBarToolStripMenuItem.CheckOnClick = true;
            this.toolBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolBarToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("toolBarToolStripMenuItem.Image")));
            this.toolBarToolStripMenuItem.Name = "toolBarToolStripMenuItem";
            this.toolBarToolStripMenuItem.Size = new System.Drawing.Size(220, 26);
            this.toolBarToolStripMenuItem.Text = "&Contract Setup";
            this.toolBarToolStripMenuItem.Click += new System.EventHandler(this.ToolBarToolStripMenuItem_Click);
            // 
            // statusBarToolStripMenuItem
            // 
            this.statusBarToolStripMenuItem.Checked = true;
            this.statusBarToolStripMenuItem.CheckOnClick = true;
            this.statusBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.statusBarToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("statusBarToolStripMenuItem.Image")));
            this.statusBarToolStripMenuItem.Name = "statusBarToolStripMenuItem";
            this.statusBarToolStripMenuItem.Size = new System.Drawing.Size(220, 26);
            this.statusBarToolStripMenuItem.Text = "&Service Code Setup";
            this.statusBarToolStripMenuItem.Click += new System.EventHandler(this.StatusBarToolStripMenuItem_Click);
            // 
            // allBatchesToolStripMenuItem
            // 
            this.allBatchesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("allBatchesToolStripMenuItem.Image")));
            this.allBatchesToolStripMenuItem.Name = "allBatchesToolStripMenuItem";
            this.allBatchesToolStripMenuItem.Size = new System.Drawing.Size(220, 26);
            this.allBatchesToolStripMenuItem.Text = "Manage Batches";
            this.allBatchesToolStripMenuItem.Click += new System.EventHandler(this.allBatchesToolStripMenuItem_Click);
            // 
            // toolsMenu
            // 
            this.toolsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uploadHCCCSVToolStripMenuItem,
            this.uploadCSVToOCHINToolStripMenuItem,
            this.xMLFileUploadsToolStripMenuItem,
            this.oCHINToHCCConversionToolStripMenuItem,
            this.oCHINToolStripMenuItem,
            this.generateHCCXmlFilesToolStripMenuItem,
            this.downloadHCCErrorsToolStripMenuItem,
            this.cSVFILESToolStripMenuItem});
            this.toolsMenu.Name = "toolsMenu";
            this.toolsMenu.Size = new System.Drawing.Size(147, 24);
            this.toolsMenu.Text = "Data Management";
            this.toolsMenu.Click += new System.EventHandler(this.toolsMenu_Click);
            // 
            // uploadHCCCSVToolStripMenuItem
            // 
            this.uploadHCCCSVToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("uploadHCCCSVToolStripMenuItem.Image")));
            this.uploadHCCCSVToolStripMenuItem.Name = "uploadHCCCSVToolStripMenuItem";
            this.uploadHCCCSVToolStripMenuItem.Size = new System.Drawing.Size(279, 26);
            this.uploadHCCCSVToolStripMenuItem.Text = "Upload OCHIN CSV";
            this.uploadHCCCSVToolStripMenuItem.Click += new System.EventHandler(this.uploadHCCCSVToolStripMenuItem_Click);
            // 
            // uploadCSVToOCHINToolStripMenuItem
            // 
            this.uploadCSVToOCHINToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("uploadCSVToOCHINToolStripMenuItem.Image")));
            this.uploadCSVToOCHINToolStripMenuItem.Name = "uploadCSVToOCHINToolStripMenuItem";
            this.uploadCSVToOCHINToolStripMenuItem.Size = new System.Drawing.Size(279, 26);
            this.uploadCSVToOCHINToolStripMenuItem.Text = "Upload HCC CSV";
            this.uploadCSVToOCHINToolStripMenuItem.Click += new System.EventHandler(this.uploadCSVToOCHINToolStripMenuItem_Click);
            // 
            // xMLFileUploadsToolStripMenuItem
            // 
            this.xMLFileUploadsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("xMLFileUploadsToolStripMenuItem.Image")));
            this.xMLFileUploadsToolStripMenuItem.Name = "xMLFileUploadsToolStripMenuItem";
            this.xMLFileUploadsToolStripMenuItem.Size = new System.Drawing.Size(279, 26);
            this.xMLFileUploadsToolStripMenuItem.Text = "XML File Uploads";
            this.xMLFileUploadsToolStripMenuItem.Click += new System.EventHandler(this.xMLFileUploadsToolStripMenuItem_Click);
            // 
            // oCHINToHCCConversionToolStripMenuItem
            // 
            this.oCHINToHCCConversionToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("oCHINToHCCConversionToolStripMenuItem.Image")));
            this.oCHINToHCCConversionToolStripMenuItem.Name = "oCHINToHCCConversionToolStripMenuItem";
            this.oCHINToHCCConversionToolStripMenuItem.Size = new System.Drawing.Size(279, 26);
            this.oCHINToHCCConversionToolStripMenuItem.Text = "CT to HCC Conversion";
            this.oCHINToHCCConversionToolStripMenuItem.Click += new System.EventHandler(this.oCHINToHCCConversionToolStripMenuItem_Click);
            // 
            // oCHINToolStripMenuItem
            // 
            this.oCHINToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("oCHINToolStripMenuItem.Image")));
            this.oCHINToolStripMenuItem.Name = "oCHINToolStripMenuItem";
            this.oCHINToolStripMenuItem.Size = new System.Drawing.Size(279, 26);
            this.oCHINToolStripMenuItem.Text = "OCHIN to RWDE Conversion";
            this.oCHINToolStripMenuItem.Click += new System.EventHandler(this.oCHINToolStripMenuItem_Click);
            // 
            // generateHCCXmlFilesToolStripMenuItem
            // 
            this.generateHCCXmlFilesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("generateHCCXmlFilesToolStripMenuItem.Image")));
            this.generateHCCXmlFilesToolStripMenuItem.Name = "generateHCCXmlFilesToolStripMenuItem";
            this.generateHCCXmlFilesToolStripMenuItem.Size = new System.Drawing.Size(279, 26);
            this.generateHCCXmlFilesToolStripMenuItem.Text = "Generate HCC xml files";
            this.generateHCCXmlFilesToolStripMenuItem.Click += new System.EventHandler(this.generateHCCXmlFilesToolStripMenuItem_Click);
            // 
            // downloadHCCErrorsToolStripMenuItem
            // 
            this.downloadHCCErrorsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("downloadHCCErrorsToolStripMenuItem.Image")));
            this.downloadHCCErrorsToolStripMenuItem.Name = "downloadHCCErrorsToolStripMenuItem";
            this.downloadHCCErrorsToolStripMenuItem.Size = new System.Drawing.Size(279, 26);
            this.downloadHCCErrorsToolStripMenuItem.Text = "Download HCC Errors";
            this.downloadHCCErrorsToolStripMenuItem.Click += new System.EventHandler(this.downloadHCCErrorsToolStripMenuItem_Click);
            // 
            // cSVFILESToolStripMenuItem
            // 
            this.cSVFILESToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cSVFILESToolStripMenuItem.Image")));
            this.cSVFILESToolStripMenuItem.Name = "cSVFILESToolStripMenuItem";
            this.cSVFILESToolStripMenuItem.Size = new System.Drawing.Size(279, 26);
            this.cSVFILESToolStripMenuItem.Text = "CSV FILES";
            this.cSVFILESToolStripMenuItem.Click += new System.EventHandler(this.cSVFILESToolStripMenuItem_Click);
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serviceReconciliationReportDotNotUseToolStripMenuItem1,
            this.deceasedClientsReportToolStripMenuItem,
            this.uploadDashboardToolStripMenuItem,
            this.hCCRECONToolStripMenuItem,
            this.clientDemographicsReportToolStripMenuItem,
            this.errorLogReportToolStripMenuItem,
            this.manualUploadReportToolStripMenuItem});
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
            this.reportsToolStripMenuItem.Text = "Reports";
            this.reportsToolStripMenuItem.Click += new System.EventHandler(this.reportsToolStripMenuItem_Click);
            // 
            // serviceReconciliationReportDotNotUseToolStripMenuItem1
            // 
            this.serviceReconciliationReportDotNotUseToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("serviceReconciliationReportDotNotUseToolStripMenuItem1.Image")));
            this.serviceReconciliationReportDotNotUseToolStripMenuItem1.Name = "serviceReconciliationReportDotNotUseToolStripMenuItem1";
            this.serviceReconciliationReportDotNotUseToolStripMenuItem1.Size = new System.Drawing.Size(290, 26);
            this.serviceReconciliationReportDotNotUseToolStripMenuItem1.Text = "Service Reconciliation Report ";
            this.serviceReconciliationReportDotNotUseToolStripMenuItem1.Click += new System.EventHandler(this.serviceReconciliationReportDotNotUseToolStripMenuItem1_Click);
            // 
            // deceasedClientsReportToolStripMenuItem
            // 
            this.deceasedClientsReportToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deceasedClientsReportToolStripMenuItem.Image")));
            this.deceasedClientsReportToolStripMenuItem.Name = "deceasedClientsReportToolStripMenuItem";
            this.deceasedClientsReportToolStripMenuItem.Size = new System.Drawing.Size(290, 26);
            this.deceasedClientsReportToolStripMenuItem.Text = "Deceased Clients Report";
            this.deceasedClientsReportToolStripMenuItem.Click += new System.EventHandler(this.deceasedClientsReportToolStripMenuItem_Click);
            // 
            // uploadDashboardToolStripMenuItem
            // 
            this.uploadDashboardToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("uploadDashboardToolStripMenuItem.Image")));
            this.uploadDashboardToolStripMenuItem.Name = "uploadDashboardToolStripMenuItem";
            this.uploadDashboardToolStripMenuItem.Size = new System.Drawing.Size(290, 26);
            this.uploadDashboardToolStripMenuItem.Text = "Monthly Reports";
            this.uploadDashboardToolStripMenuItem.Click += new System.EventHandler(this.uploadDashboardToolStripMenuItem_Click);
            // 
            // hCCRECONToolStripMenuItem
            // 
            this.hCCRECONToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("hCCRECONToolStripMenuItem.Image")));
            this.hCCRECONToolStripMenuItem.Name = "hCCRECONToolStripMenuItem";
            this.hCCRECONToolStripMenuItem.Size = new System.Drawing.Size(290, 26);
            this.hCCRECONToolStripMenuItem.Text = "HCC Reconciliation Report";
            this.hCCRECONToolStripMenuItem.Click += new System.EventHandler(this.hCCRECONToolStripMenuItem_Click);
            // 
            // clientDemographicsReportToolStripMenuItem
            // 
            this.clientDemographicsReportToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("clientDemographicsReportToolStripMenuItem.Image")));
            this.clientDemographicsReportToolStripMenuItem.Name = "clientDemographicsReportToolStripMenuItem";
            this.clientDemographicsReportToolStripMenuItem.Size = new System.Drawing.Size(290, 26);
            this.clientDemographicsReportToolStripMenuItem.Text = "Client Demographics Report";
            this.clientDemographicsReportToolStripMenuItem.Click += new System.EventHandler(this.clientDemographicsReportToolStripMenuItem_Click);
            // 
            // errorLogReportToolStripMenuItem
            // 
            this.errorLogReportToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("errorLogReportToolStripMenuItem.Image")));
            this.errorLogReportToolStripMenuItem.Name = "errorLogReportToolStripMenuItem";
            this.errorLogReportToolStripMenuItem.Size = new System.Drawing.Size(290, 26);
            this.errorLogReportToolStripMenuItem.Text = "Error Log Report";
            this.errorLogReportToolStripMenuItem.Click += new System.EventHandler(this.errorLogReportToolStripMenuItem_Click);
            // 
            // manualUploadReportToolStripMenuItem
            // 
            this.manualUploadReportToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("errorLogReportToolStripMenuItem.Image")));
            this.manualUploadReportToolStripMenuItem.Name = "manualUploadReportToolStripMenuItem";
            this.manualUploadReportToolStripMenuItem.Size = new System.Drawing.Size(290, 26);
            this.manualUploadReportToolStripMenuItem.Text = "Manual Upload Report";
            this.manualUploadReportToolStripMenuItem.Click += new System.EventHandler(this.manualUploadReportToolStripMenuItem_Click);
            // 
            // jjToolStripMenuItem
            // 
            this.jjToolStripMenuItem.Name = "jjToolStripMenuItem";
            this.jjToolStripMenuItem.Size = new System.Drawing.Size(14, 24);
            // 
            // toolStrip
            // 
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator1,
            this.printToolStripButton,
            this.helpToolStripButton,
            this.printPreviewToolStripButton,
            this.toolStripSeparator2,
            this.btnochincCsv,
            this.btnXml,
            this.btnHccCsv,
            this.btnOCHINHCCConversion,
            this.btnConversion,
            this.btnGenerator});
            this.toolStrip.Location = new System.Drawing.Point(0, 28);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1766, 27);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "ToolStrip";
            this.toolStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip_ItemClicked);
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(29, 24);
            this.newToolStripButton.Text = "New";
            this.newToolStripButton.Click += new System.EventHandler(this.ShowNewForm);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(29, 24);
            this.openToolStripButton.Text = "Open";
            this.openToolStripButton.Click += new System.EventHandler(this.OpenFile);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(29, 24);
            this.saveToolStripButton.Text = "Save";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // printToolStripButton
            // 
            this.printToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.printToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripButton.Image")));
            this.printToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.printToolStripButton.Name = "printToolStripButton";
            this.printToolStripButton.Size = new System.Drawing.Size(29, 24);
            this.printToolStripButton.Text = "Print";
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripButton.Image")));
            this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Size = new System.Drawing.Size(29, 24);
            this.helpToolStripButton.Text = "Help";
            // 
            // printPreviewToolStripButton
            // 
            this.printPreviewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.printPreviewToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printPreviewToolStripButton.Image")));
            this.printPreviewToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.printPreviewToolStripButton.Name = "printPreviewToolStripButton";
            this.printPreviewToolStripButton.Size = new System.Drawing.Size(29, 24);
            this.printPreviewToolStripButton.Text = "Print Preview";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // btnochincCsv
            // 
            this.btnochincCsv.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnochincCsv.Image = ((System.Drawing.Image)(resources.GetObject("btnochincCsv.Image")));
            this.btnochincCsv.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnochincCsv.Name = "btnochincCsv";
            this.btnochincCsv.Size = new System.Drawing.Size(29, 24);
            this.btnochincCsv.Text = "Upload OCHIN CSV";
            this.btnochincCsv.Click += new System.EventHandler(this.btnochincCsv_Click);
            // 
            // btnXml
            // 
            this.btnXml.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnXml.Image = ((System.Drawing.Image)(resources.GetObject("btnXml.Image")));
            this.btnXml.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnXml.Name = "btnXml";
            this.btnXml.Size = new System.Drawing.Size(29, 24);
            this.btnXml.Text = "Upload HCC CSV";
            this.btnXml.Click += new System.EventHandler(this.btnXml_Click);
            // 
            // btnHccCsv
            // 
            this.btnHccCsv.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnHccCsv.Image = ((System.Drawing.Image)(resources.GetObject("btnHccCsv.Image")));
            this.btnHccCsv.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHccCsv.Name = "btnHccCsv";
            this.btnHccCsv.Size = new System.Drawing.Size(29, 24);
            this.btnHccCsv.Text = "Upload XML";
            this.btnHccCsv.Click += new System.EventHandler(this.btnHccCsv_Click_1);
            // 
            // btnOCHINHCCConversion
            // 
            this.btnOCHINHCCConversion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOCHINHCCConversion.Image = ((System.Drawing.Image)(resources.GetObject("btnOCHINHCCConversion.Image")));
            this.btnOCHINHCCConversion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOCHINHCCConversion.Name = "btnOCHINHCCConversion";
            this.btnOCHINHCCConversion.Size = new System.Drawing.Size(29, 24);
            this.btnOCHINHCCConversion.Text = "OCHIN to RWDE Conversion";
            this.btnOCHINHCCConversion.Click += new System.EventHandler(this.btnOCHINHCCConversion_Click);
            // 
            // btnConversion
            // 
            this.btnConversion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnConversion.Image = ((System.Drawing.Image)(resources.GetObject("btnConversion.Image")));
            this.btnConversion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnConversion.Name = "btnConversion";
            this.btnConversion.Size = new System.Drawing.Size(29, 24);
            this.btnConversion.Text = "OCHIN tO HCC Conversion";
            this.btnConversion.Click += new System.EventHandler(this.btnHccConversion_Click);
            // 
            // btnGenerator
            // 
            this.btnGenerator.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGenerator.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerator.Image")));
            this.btnGenerator.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGenerator.Name = "btnGenerator";
            this.btnGenerator.Size = new System.Drawing.Size(29, 24);
            this.btnGenerator.Text = "Generate HCC xml files";
            this.btnGenerator.Click += new System.EventHandler(this.btnGenerator_Click);
           
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1766, 1055);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMain";
            this.RightToLeftLayout = true;
            this.Text = "Ryan White Data Exchange";
            this.TransparencyKey = System.Drawing.Color.IndianRed;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        

        private void toolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
           
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
            {
                this.ActiveMdiChild.Close();
            }

            ServiceCodeSetup ServiceCodeSetup = new ServiceCodeSetup();
            ServiceCodeSetup.MdiParent = this;
           ServiceCodeSetup.Show();
          
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            frmMain frmMain = new frmMain();
            frmMain.BackColor = System.Drawing.Color.Blue;
        }


        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem printSetupToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editMenu;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewMenu;
        private System.Windows.Forms.ToolStripMenuItem toolBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statusBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsMenu;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripButton printToolStripButton;
        private System.Windows.Forms.ToolStripButton printPreviewToolStripButton;
        private System.Windows.Forms.ToolStripButton helpToolStripButton;
        private System.Windows.Forms.ToolStripButton btnXml;
        private System.Windows.Forms.ToolStripButton btnHccCsv;
        private System.Windows.Forms.ToolStripButton btnGenerator;
        private System.Windows.Forms.ToolStripButton btnConversion;
        private System.Windows.Forms.ToolStripMenuItem uploadCSVToOCHINToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jjToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oCHINToHCCConversionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateHCCXmlFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allBatchesToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnochincCsv;
        private System.Windows.Forms.ToolStripMenuItem uploadHCCCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xMLFileUploadsToolStripMenuItem;
        private ToolStripButton btnOCHINHCCConversion;
        private ToolStripMenuItem oCHINToolStripMenuItem;
        private ToolStripMenuItem reportsToolStripMenuItem;
        private ToolStripMenuItem serviceReconciliationReportDotNotUseToolStripMenuItem1;
        private ToolStripMenuItem deceasedClientsReportToolStripMenuItem;
        private ToolStripMenuItem uploadDashboardToolStripMenuItem;
        private ToolStripMenuItem hCCRECONToolStripMenuItem;
        private ToolStripMenuItem clientDemographicsReportToolStripMenuItem;
        private ToolStripMenuItem errorLogReportToolStripMenuItem;
        private ToolStripMenuItem downloadHCCErrorsToolStripMenuItem;
        private ToolStripMenuItem cSVFILESToolStripMenuItem;
        private ToolStripMenuItem manualUploadReportToolStripMenuItem;
    }
}



