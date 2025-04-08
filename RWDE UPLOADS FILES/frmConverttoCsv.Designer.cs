namespace RWDE
{
    partial class CsvFileConversion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CsvFileConversion));
            this.btnReport = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblCtCsvFiles = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            //btnReport
            //
            this.btnReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnReport.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.ForeColor = System.Drawing.Color.Black;
            this.btnReport.Location = new System.Drawing.Point(692, 203);
            this.btnReport.Margin = new System.Windows.Forms.Padding(4);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(168, 48);
            this.btnReport.TabIndex = 7;
            this.btnReport.Text = "Submit";
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            //
            //lblName
            //
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.Color.Black;
            this.lblName.Location = new System.Drawing.Point(294, 114);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(115, 35);
            this.lblName.TabIndex = 8;
            this.lblName.Text = "CSV File:";
            //
            //txtPath
            //
            this.txtPath.BackColor = System.Drawing.Color.White;
            this.txtPath.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPath.Location = new System.Drawing.Point(448, 112);
            this.txtPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(594, 40);
            this.txtPath.TabIndex = 9;
            //
            //btnBrowse
            //
            this.btnBrowse.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowse.Image")));
            this.btnBrowse.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnBrowse.Location = new System.Drawing.Point(1049, 109);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(4);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnBrowse.Size = new System.Drawing.Size(149, 43);
            this.btnBrowse.TabIndex = 10;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            //
            //lblCtCsvFiles
            //
            this.lblCtCsvFiles.AutoSize = true;
            this.lblCtCsvFiles.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCtCsvFiles.ForeColor = System.Drawing.Color.Black;
            this.lblCtCsvFiles.Location = new System.Drawing.Point(488, 210);
            this.lblCtCsvFiles.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCtCsvFiles.Name = "lblCtCsvFiles";
            this.lblCtCsvFiles.Size = new System.Drawing.Size(161, 35);
            this.lblCtCsvFiles.TabIndex = 11;
            this.lblCtCsvFiles.Text = "CT CSV Files:";
            //
            //CsvFile_Conversion
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1438, 620);
            this.Controls.Add(this.lblCtCsvFiles);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.btnReport);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "CsvFileConversion";
            this.Text = "CsvFile_Conversion";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblCtCsvFiles;
    }
}