using System;
using System.Windows.Forms;

namespace RWDE
{
    partial class FrmUploadHccCsv
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
        public int currentFileIndex { get; private set; }
        private void progressBar_Click(object sender, EventArgs e)
        {

        }
        private void pnlProgress_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblLabelType_Click(object sender, EventArgs e)
        {

        }

        private void lblPath_Click(object sender, EventArgs e)
        {

        }
        private void txtBatchId_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblUploadStarts_Click(object sender, EventArgs e)
        {

        }

        private void txtDesc_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void progressBarfile_Click(object sender, EventArgs e)
        {

        }

        private void lblLines_Click(object sender, EventArgs e)
        {

        }

        private void lblfiles_Click(object sender, EventArgs e)
        {

        }
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUploadHccCsv));
            this.pnlCsvXml = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.lblUploadCsv = new System.Windows.Forms.Label();
            this.pnlProgress = new System.Windows.Forms.Panel();
            this.txtFName = new System.Windows.Forms.TextBox();
            this.lblFileName = new System.Windows.Forms.Label();
            this.txtBatchid = new System.Windows.Forms.TextBox();
            this.txtTotaltime = new System.Windows.Forms.TextBox();
            this.txtUploadEnded = new System.Windows.Forms.TextBox();
            this.progressBarfile = new System.Windows.Forms.ProgressBar();
            this.txtUploadStarted = new System.Windows.Forms.TextBox();
            this.lblTotaltime = new System.Windows.Forms.Label();
            this.lblUploadEnded = new System.Windows.Forms.Label();
            this.lblUploadStarts = new System.Windows.Forms.Label();
            this.lblBatch = new System.Windows.Forms.Label();
            this.lblFileInformation = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.txtProgressbar = new System.Windows.Forms.TextBox();
            this.lblfiles = new System.Windows.Forms.Label();
            this.lblLines = new System.Windows.Forms.Label();
            this.txtProgressfile = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.pnl = new System.Windows.Forms.Panel();
            this.chckPHI = new System.Windows.Forms.CheckBox();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.lblLabelType = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.TextBox();
            this.lblHeading = new System.Windows.Forms.Label();
            this.lblDesc = new System.Windows.Forms.Label();
            this.lblPath = new System.Windows.Forms.Label();
            this.txtpath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.pnlCsvXml.SuspendLayout();
            this.pnlProgress.SuspendLayout();
            this.pnl.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCsvXml
            // 
            this.pnlCsvXml.BackColor = System.Drawing.Color.White;
            this.pnlCsvXml.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCsvXml.Controls.Add(this.label2);
            this.pnlCsvXml.Controls.Add(this.button2);
            this.pnlCsvXml.Controls.Add(this.button1);
            this.pnlCsvXml.Controls.Add(this.btnNext);
            this.pnlCsvXml.Controls.Add(this.lblUploadCsv);
            this.pnlCsvXml.Controls.Add(this.pnlProgress);
            this.pnlCsvXml.Controls.Add(this.pnl);
            this.pnlCsvXml.Controls.Add(this.btnClose);
            this.pnlCsvXml.Controls.Add(this.btnUpload);
            this.pnlCsvXml.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlCsvXml.Location = new System.Drawing.Point(1, 2);
            this.pnlCsvXml.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlCsvXml.Name = "pnlCsvXml";
            this.pnlCsvXml.Size = new System.Drawing.Size(4999, 919);
            this.pnlCsvXml.TabIndex = 0;
            this.pnlCsvXml.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlCsvXml_Paint);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label2.Font = new System.Drawing.Font("Calibri", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(2453, 438);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 40);
            this.label2.TabIndex = 44;
            this.label2.Text = "Next:";
            // 
            // button2
            // 
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.Location = new System.Drawing.Point(2480, 442);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(52, 48);
            this.button2.TabIndex = 43;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Location = new System.Drawing.Point(2472, 434);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(52, 48);
            this.button1.TabIndex = 42;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNext.ForeColor = System.Drawing.Color.Black;
            this.btnNext.Location = new System.Drawing.Point(1730, 802);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(124, 54);
            this.btnNext.TabIndex = 40;
            this.btnNext.Text = "NEXT";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lblUploadCsv
            // 
            this.lblUploadCsv.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblUploadCsv.AutoSize = true;
            this.lblUploadCsv.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUploadCsv.ForeColor = System.Drawing.Color.Black;
            this.lblUploadCsv.Location = new System.Drawing.Point(902, 26);
            this.lblUploadCsv.Name = "lblUploadCsv";
            this.lblUploadCsv.Size = new System.Drawing.Size(204, 35);
            this.lblUploadCsv.TabIndex = 0;
            this.lblUploadCsv.Text = "Upload HCC CSV";
            // 
            // pnlProgress
            // 
            this.pnlProgress.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlProgress.AutoSize = true;
            this.pnlProgress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlProgress.Controls.Add(this.txtFName);
            this.pnlProgress.Controls.Add(this.lblFileName);
            this.pnlProgress.Controls.Add(this.txtBatchid);
            this.pnlProgress.Controls.Add(this.txtTotaltime);
            this.pnlProgress.Controls.Add(this.txtUploadEnded);
            this.pnlProgress.Controls.Add(this.progressBarfile);
            this.pnlProgress.Controls.Add(this.txtUploadStarted);
            this.pnlProgress.Controls.Add(this.lblTotaltime);
            this.pnlProgress.Controls.Add(this.lblUploadEnded);
            this.pnlProgress.Controls.Add(this.lblUploadStarts);
            this.pnlProgress.Controls.Add(this.lblBatch);
            this.pnlProgress.Controls.Add(this.lblFileInformation);
            this.pnlProgress.Controls.Add(this.lblProgress);
            this.pnlProgress.Controls.Add(this.txtProgressbar);
            this.pnlProgress.Controls.Add(this.lblfiles);
            this.pnlProgress.Controls.Add(this.lblLines);
            this.pnlProgress.Controls.Add(this.txtProgressfile);
            this.pnlProgress.Controls.Add(this.progressBar);
            this.pnlProgress.Location = new System.Drawing.Point(159, 473);
            this.pnlProgress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlProgress.Name = "pnlProgress";
            this.pnlProgress.Size = new System.Drawing.Size(1693, 252);
            this.pnlProgress.TabIndex = 1;
            // 
            // txtFName
            // 
            this.txtFName.BackColor = System.Drawing.Color.White;
            this.txtFName.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFName.Location = new System.Drawing.Point(292, 206);
            this.txtFName.Margin = new System.Windows.Forms.Padding(4);
            this.txtFName.Name = "txtFName";
            this.txtFName.ReadOnly = true;
            this.txtFName.Size = new System.Drawing.Size(488, 40);
            this.txtFName.TabIndex = 39;
            this.txtFName.TabStop = false;
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Font = new System.Drawing.Font("Calibri", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileName.ForeColor = System.Drawing.Color.Black;
            this.lblFileName.Location = new System.Drawing.Point(131, 207);
            this.lblFileName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(148, 36);
            this.lblFileName.TabIndex = 38;
            this.lblFileName.Text = "File Name :";
            // 
            // txtBatchid
            // 
            this.txtBatchid.BackColor = System.Drawing.Color.White;
            this.txtBatchid.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBatchid.Location = new System.Drawing.Point(1187, 48);
            this.txtBatchid.Margin = new System.Windows.Forms.Padding(4);
            this.txtBatchid.Name = "txtBatchid";
            this.txtBatchid.ReadOnly = true;
            this.txtBatchid.Size = new System.Drawing.Size(348, 40);
            this.txtBatchid.TabIndex = 37;
            this.txtBatchid.TabStop = false;
            // 
            // txtTotaltime
            // 
            this.txtTotaltime.BackColor = System.Drawing.Color.White;
            this.txtTotaltime.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotaltime.Location = new System.Drawing.Point(1187, 203);
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
            this.txtUploadEnded.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUploadEnded.Location = new System.Drawing.Point(1187, 150);
            this.txtUploadEnded.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtUploadEnded.Name = "txtUploadEnded";
            this.txtUploadEnded.ReadOnly = true;
            this.txtUploadEnded.Size = new System.Drawing.Size(348, 40);
            this.txtUploadEnded.TabIndex = 35;
            this.txtUploadEnded.TabStop = false;
            // 
            // progressBarfile
            // 
            this.progressBarfile.Location = new System.Drawing.Point(292, 146);
            this.progressBarfile.Margin = new System.Windows.Forms.Padding(4);
            this.progressBarfile.Name = "progressBarfile";
            this.progressBarfile.Size = new System.Drawing.Size(240, 39);
            this.progressBarfile.TabIndex = 3;
            this.progressBarfile.Click += new System.EventHandler(this.progressBarfile_Click);
            // 
            // txtUploadStarted
            // 
            this.txtUploadStarted.BackColor = System.Drawing.Color.White;
            this.txtUploadStarted.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUploadStarted.Location = new System.Drawing.Point(1187, 96);
            this.txtUploadStarted.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtUploadStarted.Name = "txtUploadStarted";
            this.txtUploadStarted.ReadOnly = true;
            this.txtUploadStarted.Size = new System.Drawing.Size(348, 40);
            this.txtUploadStarted.TabIndex = 34;
            this.txtUploadStarted.TabStop = false;
            // 
            // lblTotaltime
            // 
            this.lblTotaltime.AutoSize = true;
            this.lblTotaltime.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotaltime.ForeColor = System.Drawing.Color.Black;
            this.lblTotaltime.Location = new System.Drawing.Point(947, 206);
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
            this.lblUploadEnded.Location = new System.Drawing.Point(944, 150);
            this.lblUploadEnded.Name = "lblUploadEnded";
            this.lblUploadEnded.Size = new System.Drawing.Size(214, 35);
            this.lblUploadEnded.TabIndex = 31;
            this.lblUploadEnded.Text = "Upload Ended At:";
            // 
            // lblUploadStarts
            // 
            this.lblUploadStarts.AutoSize = true;
            this.lblUploadStarts.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUploadStarts.ForeColor = System.Drawing.Color.Black;
            this.lblUploadStarts.Location = new System.Drawing.Point(933, 100);
            this.lblUploadStarts.Name = "lblUploadStarts";
            this.lblUploadStarts.Size = new System.Drawing.Size(224, 35);
            this.lblUploadStarts.TabIndex = 30;
            this.lblUploadStarts.Text = "Upload Started At:";
            this.lblUploadStarts.Click += new System.EventHandler(this.lblUploadStarts_Click);
            // 
            // lblBatch
            // 
            this.lblBatch.AutoSize = true;
            this.lblBatch.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBatch.ForeColor = System.Drawing.Color.Black;
            this.lblBatch.Location = new System.Drawing.Point(1047, 50);
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
            this.lblFileInformation.Location = new System.Drawing.Point(933, 2);
            this.lblFileInformation.Name = "lblFileInformation";
            this.lblFileInformation.Size = new System.Drawing.Size(244, 35);
            this.lblFileInformation.TabIndex = 28;
            this.lblFileInformation.Text = "Upload Information";
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgress.ForeColor = System.Drawing.Color.Black;
            this.lblProgress.Location = new System.Drawing.Point(29, 2);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(114, 35);
            this.lblProgress.TabIndex = 0;
            this.lblProgress.Text = "Progress";
            this.lblProgress.Click += new System.EventHandler(this.label4_Click);
            // 
            // txtProgressbar
            // 
            this.txtProgressbar.BackColor = System.Drawing.Color.White;
            this.txtProgressbar.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProgressbar.Location = new System.Drawing.Point(540, 75);
            this.txtProgressbar.Margin = new System.Windows.Forms.Padding(4);
            this.txtProgressbar.Name = "txtProgressbar";
            this.txtProgressbar.ReadOnly = true;
            this.txtProgressbar.Size = new System.Drawing.Size(240, 40);
            this.txtProgressbar.TabIndex = 1;
            this.txtProgressbar.TabStop = false;
            this.txtProgressbar.Text = "0%";
            this.txtProgressbar.TextChanged += new System.EventHandler(this.txtProgressbar_TextChanged);
            // 
            // lblfiles
            // 
            this.lblfiles.AutoSize = true;
            this.lblfiles.Font = new System.Drawing.Font("Calibri", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblfiles.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblfiles.Location = new System.Drawing.Point(184, 146);
            this.lblfiles.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblfiles.Name = "lblfiles";
            this.lblfiles.Size = new System.Drawing.Size(96, 36);
            this.lblfiles.TabIndex = 2;
            this.lblfiles.Text = "# Files:";
            this.lblfiles.Click += new System.EventHandler(this.lblfiles_Click);
            // 
            // lblLines
            // 
            this.lblLines.AutoSize = true;
            this.lblLines.Font = new System.Drawing.Font("Calibri", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLines.ForeColor = System.Drawing.Color.Black;
            this.lblLines.Location = new System.Drawing.Point(176, 79);
            this.lblLines.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLines.Name = "lblLines";
            this.lblLines.Size = new System.Drawing.Size(103, 36);
            this.lblLines.TabIndex = 4;
            this.lblLines.Text = "# Lines:";
            this.lblLines.Click += new System.EventHandler(this.lblLines_Click);
            // 
            // txtProgressfile
            // 
            this.txtProgressfile.BackColor = System.Drawing.Color.White;
            this.txtProgressfile.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProgressfile.Location = new System.Drawing.Point(540, 146);
            this.txtProgressfile.Margin = new System.Windows.Forms.Padding(4);
            this.txtProgressfile.Name = "txtProgressfile";
            this.txtProgressfile.ReadOnly = true;
            this.txtProgressfile.Size = new System.Drawing.Size(240, 40);
            this.txtProgressfile.TabIndex = 5;
            this.txtProgressfile.TabStop = false;
            this.txtProgressfile.Text = "0/0 (0%)";
            this.txtProgressfile.TextChanged += new System.EventHandler(this.txtProgressfile_TextChanged);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(292, 76);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(240, 39);
            this.progressBar.TabIndex = 6;
            this.progressBar.Click += new System.EventHandler(this.progressBar_Click);
            // 
            // pnl
            // 
            this.pnl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnl.AutoSize = true;
            this.pnl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl.Controls.Add(this.chckPHI);
            this.pnl.Controls.Add(this.txtDesc);
            this.pnl.Controls.Add(this.lblLabelType);
            this.pnl.Controls.Add(this.txtType);
            this.pnl.Controls.Add(this.lblHeading);
            this.pnl.Controls.Add(this.lblDesc);
            this.pnl.Controls.Add(this.lblPath);
            this.pnl.Controls.Add(this.txtpath);
            this.pnl.Controls.Add(this.btnBrowse);
            this.pnl.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl.ForeColor = System.Drawing.Color.Black;
            this.pnl.Location = new System.Drawing.Point(159, 86);
            this.pnl.Margin = new System.Windows.Forms.Padding(4);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(1693, 381);
            this.pnl.TabIndex = 2;
            this.pnl.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // chckPHI
            // 
            this.chckPHI.AutoSize = true;
            this.chckPHI.Checked = true;
            this.chckPHI.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chckPHI.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chckPHI.Location = new System.Drawing.Point(1293, 172);
            this.chckPHI.Name = "chckPHI";
            this.chckPHI.Size = new System.Drawing.Size(242, 39);
            this.chckPHI.TabIndex = 8;
            this.chckPHI.Text = "PHI Data Masking";
            this.chckPHI.UseVisualStyleBackColor = true;
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(903, 268);
            this.txtDesc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(632, 40);
            this.txtDesc.TabIndex = 5;
            this.txtDesc.TextChanged += new System.EventHandler(this.txtDesc_TextChanged);
            // 
            // lblLabelType
            // 
            this.lblLabelType.AutoSize = true;
            this.lblLabelType.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLabelType.Location = new System.Drawing.Point(131, 268);
            this.lblLabelType.Name = "lblLabelType";
            this.lblLabelType.Size = new System.Drawing.Size(151, 35);
            this.lblLabelType.TabIndex = 1;
            this.lblLabelType.Text = "Batch Type:";
            // 
            // txtType
            // 
            this.txtType.BackColor = System.Drawing.Color.White;
            this.txtType.Location = new System.Drawing.Point(292, 265);
            this.txtType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtType.Name = "txtType";
            this.txtType.ReadOnly = true;
            this.txtType.Size = new System.Drawing.Size(240, 40);
            this.txtType.TabIndex = 5;
            this.txtType.Text = "HCC";
            this.txtType.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.BackColor = System.Drawing.Color.White;
            this.lblHeading.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.ForeColor = System.Drawing.Color.Black;
            this.lblHeading.Location = new System.Drawing.Point(18, 18);
            this.lblHeading.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(1459, 70);
            this.lblHeading.TabIndex = 3;
            this.lblHeading.Text = resources.GetString("lblHeading.Text");
            this.lblHeading.Click += new System.EventHandler(this.lblHeading_Click);
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesc.Location = new System.Drawing.Point(659, 273);
            this.lblDesc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(229, 35);
            this.lblDesc.TabIndex = 4;
            this.lblDesc.Text = "Batch Description:";
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPath.Location = new System.Drawing.Point(157, 172);
            this.lblPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(123, 35);
            this.lblPath.TabIndex = 5;
            this.lblPath.Text = "File Path:";
            // 
            // txtpath
            // 
            this.txtpath.BackColor = System.Drawing.Color.White;
            this.txtpath.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpath.Location = new System.Drawing.Point(292, 169);
            this.txtpath.Margin = new System.Windows.Forms.Padding(4);
            this.txtpath.Name = "txtpath";
            this.txtpath.ReadOnly = true;
            this.txtpath.ShortcutsEnabled = false;
            this.txtpath.Size = new System.Drawing.Size(828, 40);
            this.txtpath.TabIndex = 3;
            this.txtpath.TextChanged += new System.EventHandler(this.textPath_TextChanged);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowse.Image")));
            this.btnBrowse.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnBrowse.Location = new System.Drawing.Point(1129, 169);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(4);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnBrowse.Size = new System.Drawing.Size(149, 43);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click_1);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClose.AutoSize = true;
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnClose.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(1615, 731);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(240, 45);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click_1);
            // 
            // btnUpload
            // 
            this.btnUpload.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnUpload.AutoSize = true;
            this.btnUpload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnUpload.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpload.ForeColor = System.Drawing.Color.Black;
            this.btnUpload.Location = new System.Drawing.Point(1347, 731);
            this.btnUpload.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(240, 45);
            this.btnUpload.TabIndex = 7;
            this.btnUpload.Text = "Upload CSV ";
            this.btnUpload.UseVisualStyleBackColor = false;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // frmUploadCsv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 894);
            this.Controls.Add(this.pnlCsvXml);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmUploadCsv";
            this.Text = "Upload HCC CSV";
            this.pnlCsvXml.ResumeLayout(false);
            this.pnlCsvXml.PerformLayout();
            this.pnlProgress.ResumeLayout(false);
            this.pnlProgress.PerformLayout();
            this.pnl.ResumeLayout(false);
            this.pnl.PerformLayout();
            this.ResumeLayout(false);

        }

        private void textPath_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void lblHeading_Click(object sender, EventArgs e)
        {
            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void txtProgressfile_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtProgressbar_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label4_Click(object sender, EventArgs e)
        {
           
        }

        private void pnlCsvXml_Paint(object sender, PaintEventArgs e)
        {
            
        }

        #endregion

        private System.Windows.Forms.Panel pnlCsvXml;
        private System.Windows.Forms.Panel pnl;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.TextBox txtpath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnUpload;
        private Panel pnlProgress;
        private TextBox txtProgressbar;
        private Label lblfiles;
        private ProgressBar progressBarfile;
        private Label lblLines;
        private TextBox txtProgressfile;
        private ProgressBar progressBar;
        private Label lblProgress;
        private Label lblHeading;
        private Label lblLabelType;
        private TextBox txtType;
        private TextBox txtDesc;
        private Label lblUploadCsv;
        private TextBox txtTotaltime;
        private TextBox txtUploadEnded;
        private TextBox txtUploadStarted;
        private Label lblTotaltime;
        private Label lblUploadEnded;
        private Label lblUploadStarts;
        private Label lblBatch;
        private Label lblFileInformation;
        private TextBox txtFName;
        private Label lblFileName;
        private Button button2;
        private Button button1;
        private Button btnNext;
        private Label label2;
        private TextBox txtBatchid;
        private CheckBox chckPHI;
    }
}