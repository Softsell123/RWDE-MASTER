using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace RWDE
{
    partial class FrmUploadXmlFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUploadXmlFile));
            this.btnClose = new System.Windows.Forms.Button();
            this.btnUploadXML = new System.Windows.Forms.Button();
            this.pnl = new System.Windows.Forms.Panel();
            this.chkPHI = new System.Windows.Forms.CheckBox();
            this.lblBatchtype = new System.Windows.Forms.Label();
            this.txtBatchType = new System.Windows.Forms.TextBox();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.lblHeading = new System.Windows.Forms.Label();
            this.lblDesc = new System.Windows.Forms.Label();
            this.lblPath = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlProggress = new System.Windows.Forms.Panel();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.lblFilename = new System.Windows.Forms.Label();
            this.txtTotaltime = new System.Windows.Forms.TextBox();
            this.txtUploadEnded = new System.Windows.Forms.TextBox();
            this.txtUploadStarted = new System.Windows.Forms.TextBox();
            this.txtBatchid = new System.Windows.Forms.TextBox();
            this.lblTotaltime = new System.Windows.Forms.Label();
            this.lblUploadEnded = new System.Windows.Forms.Label();
            this.lblUploadStarts = new System.Windows.Forms.Label();
            this.lblBatch = new System.Windows.Forms.Label();
            this.lblFileInformation = new System.Windows.Forms.Label();
            this.lblMsg = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lbl = new System.Windows.Forms.Label();
            this.lblprogress = new System.Windows.Forms.Label();
            this.lblLines = new System.Windows.Forms.Label();
            this.progressBarLines = new System.Windows.Forms.ProgressBar();
            this.txtProgressLines = new System.Windows.Forms.TextBox();
            this.txtProgressfile = new System.Windows.Forms.TextBox();
            this.lblFiles = new System.Windows.Forms.Label();
            this.progressBarfile = new System.Windows.Forms.ProgressBar();
            this.pnlCsvXml = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnl.SuspendLayout();
            this.pnlProggress.SuspendLayout();
            this.pnlCsvXml.SuspendLayout();
            this.SuspendLayout();
            //
            //btnClose
            //
            this.btnClose.AutoSize = true;
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnClose.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(1634, 822);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(240, 45);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = Constants.Close;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            //
            //btnUploadXML
            //
            this.btnUploadXML.AutoSize = true;
            this.btnUploadXML.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnUploadXML.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUploadXML.ForeColor = System.Drawing.Color.Black;
            this.btnUploadXML.Location = new System.Drawing.Point(1372, 822);
            this.btnUploadXML.Margin = new System.Windows.Forms.Padding(4);
            this.btnUploadXML.Name = "btnUploadXML";
            this.btnUploadXML.Size = new System.Drawing.Size(240, 45);
            this.btnUploadXML.TabIndex = 9;
            this.btnUploadXML.Text = "Upload XML";
            this.btnUploadXML.UseVisualStyleBackColor = false;
            this.btnUploadXML.Click += new System.EventHandler(this.btnUploadXML_Click);
            //
            //pnl
            //
            this.pnl.AutoSize = true;
            this.pnl.BackColor = System.Drawing.Color.White;
            this.pnl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl.Controls.Add(this.chkPHI);
            this.pnl.Controls.Add(this.lblBatchtype);
            this.pnl.Controls.Add(this.txtBatchType);
            this.pnl.Controls.Add(this.txtDesc);
            this.pnl.Controls.Add(this.lblHeading);
            this.pnl.Controls.Add(this.lblDesc);
            this.pnl.Controls.Add(this.lblPath);
            this.pnl.Controls.Add(this.txtPath);
            this.pnl.Controls.Add(this.btnBrowse);
            this.pnl.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnl.Location = new System.Drawing.Point(148, 94);
            this.pnl.Margin = new System.Windows.Forms.Padding(4);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(1722, 353);
            this.pnl.TabIndex = 16;
            //
            //chkPHI
            //
            this.chkPHI.AutoSize = true;
            this.chkPHI.Checked = true;
            this.chkPHI.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPHI.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPHI.Location = new System.Drawing.Point(1256, 134);
            this.chkPHI.Name = "chkPHI";
            this.chkPHI.Size = new System.Drawing.Size(242, 39);
            this.chkPHI.TabIndex = 21;
            this.chkPHI.Text = "PHI Data Masking";
            this.chkPHI.UseVisualStyleBackColor = true;
            this.chkPHI.MouseLeave += new System.EventHandler(this.cbMask_MouseLeave);
            this.chkPHI.MouseHover += new System.EventHandler(this.cbMask_MouseHover);
            //
            //lblBatchtype
            //
            this.lblBatchtype.AutoSize = true;
            this.lblBatchtype.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBatchtype.ForeColor = System.Drawing.Color.Black;
            this.lblBatchtype.Location = new System.Drawing.Point(140, 247);
            this.lblBatchtype.Name = "lblBatchtype";
            this.lblBatchtype.Size = new System.Drawing.Size(148, 35);
            this.lblBatchtype.TabIndex = 19;
            this.lblBatchtype.Text = "Batch Type:";
            //
            //txtBatchType
            //
            this.txtBatchType.BackColor = System.Drawing.Color.White;
            this.txtBatchType.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBatchType.ForeColor = System.Drawing.Color.Black;
            this.txtBatchType.Location = new System.Drawing.Point(310, 244);
            this.txtBatchType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBatchType.Name = "txtBatchType";
            this.txtBatchType.ReadOnly = true;
            this.txtBatchType.Size = new System.Drawing.Size(240, 40);
            this.txtBatchType.TabIndex = 5;
            this.txtBatchType.Text = "ClientTrack";
            //
            //txtDesc
            //
            this.txtDesc.BackColor = System.Drawing.Color.White;
            this.txtDesc.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDesc.ForeColor = System.Drawing.Color.Black;
            this.txtDesc.Location = new System.Drawing.Point(908, 244);
            this.txtDesc.Margin = new System.Windows.Forms.Padding(4);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(609, 40);
            this.txtDesc.TabIndex = 6;
            //
            //lblHeading
            //
            this.lblHeading.AutoSize = true;
            this.lblHeading.BackColor = System.Drawing.Color.White;
            this.lblHeading.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.ForeColor = System.Drawing.Color.Black;
            this.lblHeading.Location = new System.Drawing.Point(4, 0);
            this.lblHeading.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(1580, 70);
            this.lblHeading.TabIndex = 17;
            this.lblHeading.Text = "Enter the path below to download XML file. Ensure clients and services files are " +
    "placed in the same folder.A default folder has been set \r\nand this can be change" +
    "d by clicking on \"Browse\".";
            //
            //lblDesc
            //
            this.lblDesc.AutoSize = true;
            this.lblDesc.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesc.ForeColor = System.Drawing.Color.Black;
            this.lblDesc.Location = new System.Drawing.Point(666, 249);
            this.lblDesc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(224, 35);
            this.lblDesc.TabIndex = 10;
            this.lblDesc.Text = "Batch Description:";
            //
            //lblPath
            //
            this.lblPath.AutoSize = true;
            this.lblPath.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPath.ForeColor = System.Drawing.Color.Black;
            this.lblPath.Location = new System.Drawing.Point(172, 140);
            this.lblPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(118, 35);
            this.lblPath.TabIndex = 9;
            this.lblPath.Text = "File Path:";
            //
            //txtPath
            //
            this.txtPath.BackColor = System.Drawing.Color.White;
            this.txtPath.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPath.ForeColor = System.Drawing.Color.Black;
            this.txtPath.Location = new System.Drawing.Point(310, 135);
            this.txtPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(735, 40);
            this.txtPath.TabIndex = 3;
            //
            //btnBrowse
            //
            this.btnBrowse.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.ForeColor = System.Drawing.Color.Black;
            this.btnBrowse.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowse.Image")));
            this.btnBrowse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBrowse.Location = new System.Drawing.Point(1064, 134);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(4);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(163, 43);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            //
            //lblStatus
            //
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(245, 631);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 20);
            this.lblStatus.TabIndex = 18;
            //
            //pnlProggress
            //
            this.pnlProggress.AutoSize = true;
            this.pnlProggress.BackColor = System.Drawing.Color.White;
            this.pnlProggress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlProggress.Controls.Add(this.txtFileName);
            this.pnlProggress.Controls.Add(this.lblFilename);
            this.pnlProggress.Controls.Add(this.txtTotaltime);
            this.pnlProggress.Controls.Add(this.txtUploadEnded);
            this.pnlProggress.Controls.Add(this.txtUploadStarted);
            this.pnlProggress.Controls.Add(this.txtBatchid);
            this.pnlProggress.Controls.Add(this.lblTotaltime);
            this.pnlProggress.Controls.Add(this.lblUploadEnded);
            this.pnlProggress.Controls.Add(this.lblUploadStarts);
            this.pnlProggress.Controls.Add(this.lblBatch);
            this.pnlProggress.Controls.Add(this.lblFileInformation);
            this.pnlProggress.Controls.Add(this.lblMsg);
            this.pnlProggress.Controls.Add(this.lblMessage);
            this.pnlProggress.Controls.Add(this.lbl);
            this.pnlProggress.Controls.Add(this.lblprogress);
            this.pnlProggress.Controls.Add(this.lblLines);
            this.pnlProggress.Controls.Add(this.progressBarLines);
            this.pnlProggress.Controls.Add(this.txtProgressLines);
            this.pnlProggress.Controls.Add(this.txtProgressfile);
            this.pnlProggress.Controls.Add(this.lblFiles);
            this.pnlProggress.Controls.Add(this.progressBarfile);
            this.pnlProggress.Location = new System.Drawing.Point(148, 461);
            this.pnlProggress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlProggress.Name = "pnlProggress";
            this.pnlProggress.Size = new System.Drawing.Size(1722, 353);
            this.pnlProggress.TabIndex = 19;
            //
            //txtFileName
            //
            this.txtFileName.BackColor = System.Drawing.Color.White;
            this.txtFileName.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFileName.Location = new System.Drawing.Point(309, 209);
            this.txtFileName.Margin = new System.Windows.Forms.Padding(4);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(488, 40);
            this.txtFileName.TabIndex = 30;
            this.txtFileName.TabStop = false;
            //
            //lblFilename
            //
            this.lblFilename.AutoEllipsis = true;
            this.lblFilename.AutoSize = true;
            this.lblFilename.Font = new System.Drawing.Font("Calibri", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilename.ForeColor = System.Drawing.Color.Black;
            this.lblFilename.Location = new System.Drawing.Point(139, 209);
            this.lblFilename.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFilename.Name = "lblFilename";
            this.lblFilename.Size = new System.Drawing.Size(148, 36);
            this.lblFilename.TabIndex = 29;
            this.lblFilename.Text = "File Name :";
            //
            //txtTotaltime
            //
            this.txtTotaltime.BackColor = System.Drawing.Color.White;
            this.txtTotaltime.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotaltime.Location = new System.Drawing.Point(1173, 207);
            this.txtTotaltime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTotaltime.Name = "txtTotaltime";
            this.txtTotaltime.ReadOnly = true;
            this.txtTotaltime.Size = new System.Drawing.Size(348, 40);
            this.txtTotaltime.TabIndex = 27;
            this.txtTotaltime.TabStop = false;
            //
            //txtUploadEnded
            //
            this.txtUploadEnded.BackColor = System.Drawing.Color.White;
            this.txtUploadEnded.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUploadEnded.Location = new System.Drawing.Point(1173, 151);
            this.txtUploadEnded.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtUploadEnded.Name = "txtUploadEnded";
            this.txtUploadEnded.ReadOnly = true;
            this.txtUploadEnded.Size = new System.Drawing.Size(348, 40);
            this.txtUploadEnded.TabIndex = 26;
            this.txtUploadEnded.TabStop = false;
            //
            //txtUploadStarted
            //
            this.txtUploadStarted.BackColor = System.Drawing.Color.White;
            this.txtUploadStarted.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUploadStarted.Location = new System.Drawing.Point(1173, 101);
            this.txtUploadStarted.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtUploadStarted.Name = "txtUploadStarted";
            this.txtUploadStarted.ReadOnly = true;
            this.txtUploadStarted.Size = new System.Drawing.Size(348, 40);
            this.txtUploadStarted.TabIndex = 25;
            this.txtUploadStarted.TabStop = false;
            //
            //txtBatchid
            //
            this.txtBatchid.BackColor = System.Drawing.Color.White;
            this.txtBatchid.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBatchid.Location = new System.Drawing.Point(1173, 53);
            this.txtBatchid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBatchid.Name = "txtBatchid";
            this.txtBatchid.ReadOnly = true;
            this.txtBatchid.Size = new System.Drawing.Size(348, 40);
            this.txtBatchid.TabIndex = 24;
            this.txtBatchid.TabStop = false;
            //
            //lblTotaltime
            //
            this.lblTotaltime.AutoSize = true;
            this.lblTotaltime.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotaltime.ForeColor = System.Drawing.Color.Black;
            this.lblTotaltime.Location = new System.Drawing.Point(944, 212);
            this.lblTotaltime.Name = "lblTotaltime";
            this.lblTotaltime.Size = new System.Drawing.Size(212, 35);
            this.lblTotaltime.TabIndex = 23;
            this.lblTotaltime.Text = "Total Time Taken:";
            //
            //lblUploadEnded
            //
            this.lblUploadEnded.AutoSize = true;
            this.lblUploadEnded.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUploadEnded.ForeColor = System.Drawing.Color.Black;
            this.lblUploadEnded.Location = new System.Drawing.Point(943, 160);
            this.lblUploadEnded.Name = "lblUploadEnded";
            this.lblUploadEnded.Size = new System.Drawing.Size(214, 35);
            this.lblUploadEnded.TabIndex = 22;
            this.lblUploadEnded.Text = "Upload Ended At:";
            //
            //lblUploadStarts
            //
            this.lblUploadStarts.AutoSize = true;
            this.lblUploadStarts.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUploadStarts.ForeColor = System.Drawing.Color.Black;
            this.lblUploadStarts.Location = new System.Drawing.Point(931, 105);
            this.lblUploadStarts.Name = "lblUploadStarts";
            this.lblUploadStarts.Size = new System.Drawing.Size(224, 35);
            this.lblUploadStarts.TabIndex = 21;
            this.lblUploadStarts.Text = "Upload Started At:";
            //
            //lblBatch
            //
            this.lblBatch.AutoSize = true;
            this.lblBatch.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBatch.ForeColor = System.Drawing.Color.Black;
            this.lblBatch.Location = new System.Drawing.Point(1043, 57);
            this.lblBatch.Name = "lblBatch";
            this.lblBatch.Size = new System.Drawing.Size(117, 35);
            this.lblBatch.TabIndex = 20;
            this.lblBatch.Text = "Batch ID:";
            //
            //lblFileInformation
            //
            this.lblFileInformation.AutoSize = true;
            this.lblFileInformation.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileInformation.ForeColor = System.Drawing.Color.Black;
            this.lblFileInformation.Location = new System.Drawing.Point(912, 0);
            this.lblFileInformation.Name = "lblFileInformation";
            this.lblFileInformation.Size = new System.Drawing.Size(244, 35);
            this.lblFileInformation.TabIndex = 19;
            this.lblFileInformation.Text = "Upload Information";
            //
            //lblMsg
            //
            this.lblMsg.AutoSize = true;
            this.lblMsg.ForeColor = System.Drawing.Color.Black;
            this.lblMsg.Location = new System.Drawing.Point(1000, 119);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(10, 16);
            this.lblMsg.TabIndex = 18;
            this.lblMsg.Text = ".";
            //
            //lblMessage
            //
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(1165, 82);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 16);
            this.lblMessage.TabIndex = 17;
            //
            //lbl
            //
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(1045, 119);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(0, 16);
            this.lbl.TabIndex = 16;
            //
            //lblprogress
            //
            this.lblprogress.AutoSize = true;
            this.lblprogress.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblprogress.ForeColor = System.Drawing.Color.Black;
            this.lblprogress.Location = new System.Drawing.Point(32, 0);
            this.lblprogress.Name = "lblprogress";
            this.lblprogress.Size = new System.Drawing.Size(114, 35);
            this.lblprogress.TabIndex = 14;
            this.lblprogress.Text = "Progress";
            //
            //lblLines
            //
            this.lblLines.AutoSize = true;
            this.lblLines.Font = new System.Drawing.Font("Calibri", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLines.ForeColor = System.Drawing.Color.Black;
            this.lblLines.Location = new System.Drawing.Point(187, 70);
            this.lblLines.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLines.Name = "lblLines";
            this.lblLines.Size = new System.Drawing.Size(103, 36);
            this.lblLines.TabIndex = 8;
            this.lblLines.Text = "# Lines:";
            //
            //progressBarLines
            //
            this.progressBarLines.Location = new System.Drawing.Point(309, 69);
            this.progressBarLines.Margin = new System.Windows.Forms.Padding(4);
            this.progressBarLines.Name = "progressBarLines";
            this.progressBarLines.Size = new System.Drawing.Size(240, 39);
            this.progressBarLines.TabIndex = 7;
            //
            //txtProgressLines
            //
            this.txtProgressLines.BackColor = System.Drawing.Color.White;
            this.txtProgressLines.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProgressLines.Location = new System.Drawing.Point(557, 68);
            this.txtProgressLines.Margin = new System.Windows.Forms.Padding(4);
            this.txtProgressLines.Name = "txtProgressLines";
            this.txtProgressLines.ReadOnly = true;
            this.txtProgressLines.Size = new System.Drawing.Size(240, 40);
            this.txtProgressLines.TabIndex = 10;
            this.txtProgressLines.TabStop = false;
            this.txtProgressLines.Text = "0";
            //
            //txtProgressfile
            //
            this.txtProgressfile.BackColor = System.Drawing.Color.White;
            this.txtProgressfile.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProgressfile.Location = new System.Drawing.Point(557, 142);
            this.txtProgressfile.Margin = new System.Windows.Forms.Padding(4);
            this.txtProgressfile.Name = "txtProgressfile";
            this.txtProgressfile.ReadOnly = true;
            this.txtProgressfile.Size = new System.Drawing.Size(240, 40);
            this.txtProgressfile.TabIndex = 13;
            this.txtProgressfile.TabStop = false;
            this.txtProgressfile.Text = "0/0";
            //
            //lblFiles
            //
            this.lblFiles.AutoSize = true;
            this.lblFiles.Font = new System.Drawing.Font("Calibri", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFiles.ForeColor = System.Drawing.Color.Black;
            this.lblFiles.Location = new System.Drawing.Point(195, 142);
            this.lblFiles.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFiles.Name = "lblFiles";
            this.lblFiles.Size = new System.Drawing.Size(96, 36);
            this.lblFiles.TabIndex = 12;
            this.lblFiles.Text = "# Files:";
            //
            //progressBarfile
            //
            this.progressBarfile.Location = new System.Drawing.Point(309, 144);
            this.progressBarfile.Margin = new System.Windows.Forms.Padding(4);
            this.progressBarfile.Name = "progressBarfile";
            this.progressBarfile.Size = new System.Drawing.Size(240, 39);
            this.progressBarfile.TabIndex = 11;
            //
            //pnlCsvXml
            //
            this.pnlCsvXml.BackColor = System.Drawing.Color.White;
            this.pnlCsvXml.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCsvXml.Controls.Add(this.lblTitle);
            this.pnlCsvXml.Controls.Add(this.pnlProggress);
            this.pnlCsvXml.Controls.Add(this.lblStatus);
            this.pnlCsvXml.Controls.Add(this.pnl);
            this.pnlCsvXml.Controls.Add(this.btnUploadXML);
            this.pnlCsvXml.Controls.Add(this.btnClose);
            this.pnlCsvXml.ForeColor = System.Drawing.Color.Red;
            this.pnlCsvXml.Location = new System.Drawing.Point(-1, -1);
            this.pnlCsvXml.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlCsvXml.Name = "pnlCsvXml";
            this.pnlCsvXml.Size = new System.Drawing.Size(5000, 1071);
            this.pnlCsvXml.TabIndex = 19;
            //
            //lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Location = new System.Drawing.Point(928, 37);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(209, 35);
            this.lblTitle.TabIndex = 20;
            this.lblTitle.Text = "Upload XML File ";
            //
            //FrmUploadXmlFile
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1907, 935);
            this.Controls.Add(this.pnlCsvXml);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmUploadXmlFile";
            this.Text = "XML File Uploads";
            this.pnl.ResumeLayout(false);
            this.pnl.PerformLayout();
            this.pnlProggress.ResumeLayout(false);
            this.pnlProggress.PerformLayout();
            this.pnlCsvXml.ResumeLayout(false);
            this.pnlCsvXml.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnClose;
        private Button btnUploadXML;
        private Panel pnl;
        private Label lblBatchtype;
        private TextBox txtBatchType;
        private TextBox txtDesc;
        private Label lblHeading;
        private Label lblDesc;
        private Label lblPath;
        private TextBox txtPath;
        private Button btnBrowse;
        private Label lblStatus;
        private Panel pnlProggress;
        private Label lblMsg;
        private Label lblMessage;
        private Label lbl;
        private Label lblprogress;
        private Label lblLines;
        private ProgressBar progressBarLines;
        private TextBox txtProgressLines;
        private TextBox txtProgressfile;
        private Label lblFiles;
        private ProgressBar progressBarfile;
        private Panel pnlCsvXml;
        private Label lblTitle;
        private TextBox txtTotaltime;
        private TextBox txtUploadEnded;
        private TextBox txtUploadStarted;
        private TextBox txtBatchid;
        private Label lblTotaltime;
        private Label lblUploadEnded;
        private Label lblUploadStarts;
        private Label lblBatch;
        private Label lblFileInformation;
        private TextBox txtFileName;
        private Label lblFilename;
        private CheckBox chkPHI;
    }
}