using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace RWDE
{
    partial class FrmUploadOchinCsv
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
        private void progressBarfile_Click(object sender, EventArgs e)
        {

        }

        private void txtProgressfile_TextChanged(object sender, EventArgs e)
        {

        }

        private void pnlProgress_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblFname_Click(object sender, EventArgs e)
        {

        }

        private void txtFileName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTotaltime_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUploadEnded_TextChanged(object sender, EventArgs e)
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

        private void lblUploadStarts_Click(object sender, EventArgs e)
        {

        }

        private void lblBatch_Click(object sender, EventArgs e)
        {

        }

        private void lblFileInformation_Click(object sender, EventArgs e)
        {

        }

        private void lblProgress_Click(object sender, EventArgs e)
        {

        }

        private void txtProgressbar_TextChanged(object sender, EventArgs e)//
        {

        }

        private void lblfiles_Click(object sender, EventArgs e)
        {

        }

        private void lblLines_Click(object sender, EventArgs e)
        {

        }

        private void txtProgressfile_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void pnl_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblType_Click(object sender, EventArgs e)
        {

        }

        private void txtType_TextChanged(object sender, EventArgs e)
        {

        }

     

        private void lblPath_Click(object sender, EventArgs e)
        {

        }

        private void txtPath_TextChanged(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void chckPHI_CheckedChanged(object sender, EventArgs e)
        {

        }


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUploadOchinCsv));
            this.Next = new System.Windows.Forms.Panel();
            this.btnNext = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlProgress = new System.Windows.Forms.Panel();
            this.lblFilename = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.txtTotaltime = new System.Windows.Forms.TextBox();
            this.txtUploadEnded = new System.Windows.Forms.TextBox();
            this.txtUploadStarted = new System.Windows.Forms.TextBox();
            this.progressBarfile = new System.Windows.Forms.ProgressBar();
            this.txtBatchid = new System.Windows.Forms.TextBox();
            this.lblTotaltime = new System.Windows.Forms.Label();
            this.lblUploadEnded = new System.Windows.Forms.Label();
            this.lblUploadStarts = new System.Windows.Forms.Label();
            this.lblBatch = new System.Windows.Forms.Label();
            this.lblFileInformation = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.txtProgressLines = new System.Windows.Forms.TextBox();
            this.lblfiles = new System.Windows.Forms.Label();
            this.lblLines = new System.Windows.Forms.Label();
            this.txtProgressfile = new System.Windows.Forms.TextBox();
            this.progressBarLines = new System.Windows.Forms.ProgressBar();
            this.btnUpload = new System.Windows.Forms.Button();
            this.pnl = new System.Windows.Forms.Panel();
            this.chckURN = new System.Windows.Forms.CheckBox();
            this.chckPHI = new System.Windows.Forms.CheckBox();
            this.lblType = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.TextBox();
            this.lblHeading = new System.Windows.Forms.Label();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.lblDesc = new System.Windows.Forms.Label();
            this.lblPath = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Next.SuspendLayout();
            this.pnlProgress.SuspendLayout();
            this.pnl.SuspendLayout();
            this.SuspendLayout();
            // 
            // Next
            // 
            this.Next.BackColor = System.Drawing.Color.White;
            this.Next.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Next.Controls.Add(this.btnNext);
            this.Next.Controls.Add(this.lblTitle);
            this.Next.Controls.Add(this.btnClose);
            this.Next.Controls.Add(this.pnlProgress);
            this.Next.Controls.Add(this.btnUpload);
            this.Next.Controls.Add(this.pnl);
            this.Next.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Next.ForeColor = System.Drawing.Color.Black;
            this.Next.Location = new System.Drawing.Point(-2, 1);
            this.Next.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Next.Name = "Next";
            this.Next.Size = new System.Drawing.Size(5004, 993);
            this.Next.TabIndex = 0;
            this.Next.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNext.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(1798, 783);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(124, 54);
            this.btnNext.TabIndex = 8;
            this.btnNext.Text = "NEXT";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Location = new System.Drawing.Point(915, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(233, 35);
            this.lblTitle.TabIndex = 9;
            this.lblTitle.Text = "Upload OCHIN CSV";
            this.lblTitle.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnClose
            // 
            this.btnClose.AutoSize = true;
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnClose.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(1682, 714);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(240, 45);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pnlProgress
            // 
            this.pnlProgress.AutoSize = true;
            this.pnlProgress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlProgress.Controls.Add(this.lblFilename);
            this.pnlProgress.Controls.Add(this.txtFileName);
            this.pnlProgress.Controls.Add(this.txtTotaltime);
            this.pnlProgress.Controls.Add(this.txtUploadEnded);
            this.pnlProgress.Controls.Add(this.txtUploadStarted);
            this.pnlProgress.Controls.Add(this.progressBarfile);
            this.pnlProgress.Controls.Add(this.txtBatchid);
            this.pnlProgress.Controls.Add(this.lblTotaltime);
            this.pnlProgress.Controls.Add(this.lblUploadEnded);
            this.pnlProgress.Controls.Add(this.lblUploadStarts);
            this.pnlProgress.Controls.Add(this.lblBatch);
            this.pnlProgress.Controls.Add(this.lblFileInformation);
            this.pnlProgress.Controls.Add(this.lblProgress);
            this.pnlProgress.Controls.Add(this.txtProgressLines);
            this.pnlProgress.Controls.Add(this.lblfiles);
            this.pnlProgress.Controls.Add(this.lblLines);
            this.pnlProgress.Controls.Add(this.txtProgressfile);
            this.pnlProgress.Controls.Add(this.progressBarLines);
            this.pnlProgress.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlProgress.Location = new System.Drawing.Point(97, 425);
            this.pnlProgress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlProgress.Name = "pnlProgress";
            this.pnlProgress.Size = new System.Drawing.Size(1825, 275);
            this.pnlProgress.TabIndex = 7;
            this.pnlProgress.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlProgress_Paint);
            // 
            // lblFilename
            // 
            this.lblFilename.AutoSize = true;
            this.lblFilename.Font = new System.Drawing.Font("Calibri", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilename.ForeColor = System.Drawing.Color.Black;
            this.lblFilename.Location = new System.Drawing.Point(122, 207);
            this.lblFilename.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFilename.Name = "lblFilename";
            this.lblFilename.Size = new System.Drawing.Size(148, 36);
            this.lblFilename.TabIndex = 42;
            this.lblFilename.Text = "File Name :";
            this.lblFilename.Click += new System.EventHandler(this.lblFname_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.BackColor = System.Drawing.Color.White;
            this.txtFileName.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFileName.Location = new System.Drawing.Point(287, 207);
            this.txtFileName.Margin = new System.Windows.Forms.Padding(4);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(488, 40);
            this.txtFileName.TabIndex = 41;
            this.txtFileName.TabStop = false;
            this.txtFileName.TextChanged += new System.EventHandler(this.txtFileName_TextChanged);
            // 
            // txtTotaltime
            // 
            this.txtTotaltime.BackColor = System.Drawing.Color.White;
            this.txtTotaltime.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotaltime.Location = new System.Drawing.Point(1193, 229);
            this.txtTotaltime.Margin = new System.Windows.Forms.Padding(4);
            this.txtTotaltime.Name = "txtTotaltime";
            this.txtTotaltime.ReadOnly = true;
            this.txtTotaltime.Size = new System.Drawing.Size(348, 40);
            this.txtTotaltime.TabIndex = 40;
            this.txtTotaltime.TabStop = false;
            this.txtTotaltime.TextChanged += new System.EventHandler(this.txtTotaltime_TextChanged);
            // 
            // txtUploadEnded
            // 
            this.txtUploadEnded.BackColor = System.Drawing.Color.White;
            this.txtUploadEnded.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUploadEnded.Location = new System.Drawing.Point(1193, 172);
            this.txtUploadEnded.Margin = new System.Windows.Forms.Padding(4);
            this.txtUploadEnded.Name = "txtUploadEnded";
            this.txtUploadEnded.ReadOnly = true;
            this.txtUploadEnded.Size = new System.Drawing.Size(348, 40);
            this.txtUploadEnded.TabIndex = 39;
            this.txtUploadEnded.TabStop = false;
            this.txtUploadEnded.TextChanged += new System.EventHandler(this.txtUploadEnded_TextChanged);
            // 
            // txtUploadStarted
            // 
            this.txtUploadStarted.BackColor = System.Drawing.Color.White;
            this.txtUploadStarted.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUploadStarted.Location = new System.Drawing.Point(1193, 119);
            this.txtUploadStarted.Margin = new System.Windows.Forms.Padding(4);
            this.txtUploadStarted.Name = "txtUploadStarted";
            this.txtUploadStarted.ReadOnly = true;
            this.txtUploadStarted.Size = new System.Drawing.Size(348, 40);
            this.txtUploadStarted.TabIndex = 0;
            this.txtUploadStarted.TabStop = false;
            this.txtUploadStarted.TextChanged += new System.EventHandler(this.txtUploadStarted_TextChanged);
            // 
            // progressBarfile
            // 
            this.progressBarfile.Location = new System.Drawing.Point(287, 145);
            this.progressBarfile.Margin = new System.Windows.Forms.Padding(4);
            this.progressBarfile.Name = "progressBarfile";
            this.progressBarfile.Size = new System.Drawing.Size(240, 39);
            this.progressBarfile.TabIndex = 3;
            this.progressBarfile.Click += new System.EventHandler(this.progressBarfile_Click);
            // 
            // txtBatchid
            // 
            this.txtBatchid.BackColor = System.Drawing.Color.White;
            this.txtBatchid.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtBatchid.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBatchid.Location = new System.Drawing.Point(1193, 65);
            this.txtBatchid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBatchid.Name = "txtBatchid";
            this.txtBatchid.ReadOnly = true;
            this.txtBatchid.Size = new System.Drawing.Size(348, 40);
            this.txtBatchid.TabIndex = 33;
            this.txtBatchid.TabStop = false;
            this.txtBatchid.TextChanged += new System.EventHandler(this.txtBatchid_TextChanged);
            // 
            // lblTotaltime
            // 
            this.lblTotaltime.AutoSize = true;
            this.lblTotaltime.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotaltime.ForeColor = System.Drawing.Color.Black;
            this.lblTotaltime.Location = new System.Drawing.Point(951, 229);
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
            this.lblUploadEnded.Location = new System.Drawing.Point(950, 175);
            this.lblUploadEnded.Name = "lblUploadEnded";
            this.lblUploadEnded.Size = new System.Drawing.Size(214, 35);
            this.lblUploadEnded.TabIndex = 31;
            this.lblUploadEnded.Text = "Upload Ended At:";
            this.lblUploadEnded.Click += new System.EventHandler(this.lblUploadEnded_Click);
            // 
            // lblUploadStarts
            // 
            this.lblUploadStarts.AutoSize = true;
            this.lblUploadStarts.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUploadStarts.ForeColor = System.Drawing.Color.Black;
            this.lblUploadStarts.Location = new System.Drawing.Point(941, 122);
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
            this.lblBatch.Location = new System.Drawing.Point(1049, 69);
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
            this.lblFileInformation.Location = new System.Drawing.Point(895, 0);
            this.lblFileInformation.Name = "lblFileInformation";
            this.lblFileInformation.Size = new System.Drawing.Size(244, 35);
            this.lblFileInformation.TabIndex = 28;
            this.lblFileInformation.Text = "Upload Information";
            this.lblFileInformation.Click += new System.EventHandler(this.lblFileInformation_Click);
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgress.ForeColor = System.Drawing.Color.Black;
            this.lblProgress.Location = new System.Drawing.Point(29, 0);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(114, 35);
            this.lblProgress.TabIndex = 0;
            this.lblProgress.Text = "Progress";
            this.lblProgress.Click += new System.EventHandler(this.lblProgress_Click);
            // 
            // txtProgressLines
            // 
            this.txtProgressLines.BackColor = System.Drawing.Color.White;
            this.txtProgressLines.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProgressLines.Location = new System.Drawing.Point(535, 79);
            this.txtProgressLines.Margin = new System.Windows.Forms.Padding(4);
            this.txtProgressLines.Name = "txtProgressLines";
            this.txtProgressLines.ReadOnly = true;
            this.txtProgressLines.Size = new System.Drawing.Size(240, 40);
            this.txtProgressLines.TabIndex = 0;
            this.txtProgressLines.TabStop = false;
            this.txtProgressLines.Text = Constants.ZeroPercent;
            this.txtProgressLines.TextChanged += new System.EventHandler(this.txtProgressbar_TextChanged);
            // 
            // lblfiles
            // 
            this.lblfiles.AutoSize = true;
            this.lblfiles.Font = new System.Drawing.Font("Calibri", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblfiles.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblfiles.Location = new System.Drawing.Point(173, 145);
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
            this.lblLines.Location = new System.Drawing.Point(166, 81);
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
            this.txtProgressfile.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProgressfile.Location = new System.Drawing.Point(535, 143);
            this.txtProgressfile.Margin = new System.Windows.Forms.Padding(4);
            this.txtProgressfile.Name = "txtProgressfile";
            this.txtProgressfile.ReadOnly = true;
            this.txtProgressfile.Size = new System.Drawing.Size(240, 40);
            this.txtProgressfile.TabIndex = 0;
            this.txtProgressfile.TabStop = false;
            this.txtProgressfile.Text = "0/0(0%)";
            this.txtProgressfile.TextChanged += new System.EventHandler(this.txtProgressfile_TextChanged_1);
            // 
            // progressBarLines
            // 
            this.progressBarLines.Location = new System.Drawing.Point(287, 79);
            this.progressBarLines.Margin = new System.Windows.Forms.Padding(4);
            this.progressBarLines.Name = "progressBarLines";
            this.progressBarLines.Size = new System.Drawing.Size(240, 39);
            this.progressBarLines.TabIndex = 6;
            this.progressBarLines.Click += new System.EventHandler(this.progressBar_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.AutoSize = true;
            this.btnUpload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnUpload.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpload.ForeColor = System.Drawing.Color.Black;
            this.btnUpload.Location = new System.Drawing.Point(1421, 715);
            this.btnUpload.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(240, 45);
            this.btnUpload.TabIndex = 9;
            this.btnUpload.Text = "Upload CSV ";
            this.btnUpload.UseVisualStyleBackColor = false;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // pnl
            // 
            this.pnl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnl.AutoSize = true;
            this.pnl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl.Controls.Add(this.chckURN);
            this.pnl.Controls.Add(this.chckPHI);
            this.pnl.Controls.Add(this.lblType);
            this.pnl.Controls.Add(this.txtType);
            this.pnl.Controls.Add(this.lblHeading);
            this.pnl.Controls.Add(this.txtDesc);
            this.pnl.Controls.Add(this.lblDesc);
            this.pnl.Controls.Add(this.lblPath);
            this.pnl.Controls.Add(this.txtPath);
            this.pnl.Controls.Add(this.btnBrowse);
            this.pnl.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl.ForeColor = System.Drawing.Color.Black;
            this.pnl.Location = new System.Drawing.Point(97, 67);
            this.pnl.Margin = new System.Windows.Forms.Padding(4);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(1735, 336);
            this.pnl.TabIndex = 8;
            this.pnl.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_Paint);
            // 
            // chckURN
            // 
            this.chckURN.AutoSize = true;
            this.chckURN.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chckURN.Location = new System.Drawing.Point(1342, 143);
            this.chckURN.Name = "chckURN";
            this.chckURN.Size = new System.Drawing.Size(260, 39);
            this.chckURN.TabIndex = 8;
            this.chckURN.Text = "PHI With URN Data";
            this.chckURN.UseVisualStyleBackColor = true;
            // 
            // chckPHI
            // 
            this.chckPHI.AutoSize = true;
            this.chckPHI.Checked = true;
            this.chckPHI.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chckPHI.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chckPHI.Location = new System.Drawing.Point(1092, 143);
            this.chckPHI.Name = "chckPHI";
            this.chckPHI.Size = new System.Drawing.Size(242, 39);
            this.chckPHI.TabIndex = 7;
            this.chckPHI.Text = "PHI Data Masking";
            this.chckPHI.UseVisualStyleBackColor = true;
            this.chckPHI.CheckedChanged += new System.EventHandler(this.chckPHI_CheckedChanged);
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType.Location = new System.Drawing.Point(141, 240);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(151, 35);
            this.lblType.TabIndex = 0;
            this.lblType.Text = "Batch Type:";
            this.lblType.Click += new System.EventHandler(this.lblType_Click);
            // 
            // txtType
            // 
            this.txtType.BackColor = System.Drawing.Color.White;
            this.txtType.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtType.Location = new System.Drawing.Point(317, 238);
            this.txtType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtType.Name = "txtType";
            this.txtType.ReadOnly = true;
            this.txtType.Size = new System.Drawing.Size(240, 40);
            this.txtType.TabIndex = 4;
            this.txtType.Text = "OCHIN";
            this.txtType.TextChanged += new System.EventHandler(this.txtType_TextChanged);
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.BackColor = System.Drawing.Color.White;
            this.lblHeading.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.ForeColor = System.Drawing.Color.Black;
            this.lblHeading.Location = new System.Drawing.Point(4, 0);
            this.lblHeading.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(1725, 70);
            this.lblHeading.TabIndex = 4;
            this.lblHeading.Text = resources.GetString("lblHeading.Text");
            // 
            // txtDesc
            // 
            this.txtDesc.BackColor = System.Drawing.Color.White;
            this.txtDesc.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDesc.Location = new System.Drawing.Point(904, 240);
            this.txtDesc.Margin = new System.Windows.Forms.Padding(4);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(593, 40);
            this.txtDesc.TabIndex = 5;
            this.txtDesc.TextChanged += new System.EventHandler(this.txtDesc_TextChanged);
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesc.Location = new System.Drawing.Point(659, 244);
            this.lblDesc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(229, 35);
            this.lblDesc.TabIndex = 5;
            this.lblDesc.Text = "Batch Description:";
            this.lblDesc.Click += new System.EventHandler(this.lblDesc_Click);
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPath.Location = new System.Drawing.Point(171, 143);
            this.lblPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(123, 35);
            this.lblPath.TabIndex = 6;
            this.lblPath.Text = "File Path:";
            this.lblPath.Click += new System.EventHandler(this.lblPath_Click);
            // 
            // txtPath
            // 
            this.txtPath.BackColor = System.Drawing.Color.White;
            this.txtPath.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPath.Location = new System.Drawing.Point(313, 140);
            this.txtPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(593, 40);
            this.txtPath.TabIndex = 2;
            this.txtPath.TextChanged += new System.EventHandler(this.txtPath_TextChanged);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowse.Image")));
            this.btnBrowse.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnBrowse.Location = new System.Drawing.Point(912, 139);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(4);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnBrowse.Size = new System.Drawing.Size(149, 43);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // FrmUploadOchinCsv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1829, 935);
            this.Controls.Add(this.Next);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmUploadOchinCsv";
            this.Text = "Upload OCHIN CSV";
            this.Next.ResumeLayout(false);
            this.Next.PerformLayout();
            this.pnlProgress.ResumeLayout(false);
            this.pnlProgress.PerformLayout();
            this.pnl.ResumeLayout(false);
            this.pnl.PerformLayout();
            this.ResumeLayout(false);

        }

        private void lblDesc_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void progressBar_Click(object sender, EventArgs e)
        {
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }
        private void txtDesc_TextChanged(object sender, EventArgs e)//to dynamically insert uploded time when user runs the code for description
        {

        }
        #endregion
        public int currentFileIndex { get; private set; }
        private System.Windows.Forms.Panel Next;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlProgress;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.TextBox txtProgressLines;
        private System.Windows.Forms.Label lblfiles;
        private System.Windows.Forms.ProgressBar progressBarfile;
        private System.Windows.Forms.Label lblLines;
        private System.Windows.Forms.TextBox txtProgressfile;
        private System.Windows.Forms.ProgressBar progressBarLines;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Panel pnl;
        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnBrowse;
        private Label lblType;
        private TextBox txtType;
        private ContextMenuStrip contextMenuStrip1;
        private Label lblTitle;
        private TextBox txtBatchid;
        private Label lblTotaltime;
        private Label lblUploadEnded;
        private Label lblUploadStarts;
        private Label lblBatch;
        private Label lblFileInformation;
        private TextBox txtUploadEnded;
        private TextBox txtUploadStarted;
        private TextBox txtTotaltime;
        private Label lblFilename;
        private TextBox txtFileName;
        private CheckBox chckPHI;
        private Button btnNext;
        public CheckBox chckURN;
    }
}