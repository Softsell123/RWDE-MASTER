namespace RWDE_UPLOADS_FILES
{
    partial class frmConnectionSetup
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtDataSource;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblInfo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtDataSource = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtDataSource
            // 
            this.txtDataSource.Location = new System.Drawing.Point(12, 47);
            this.txtDataSource.Name = "txtDataSource";
            this.txtDataSource.Size = new System.Drawing.Size(400, 22);
            this.txtDataSource.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 82);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(130, 82);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(12, 10);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(356, 32);
            this.lblInfo.TabIndex = 3;
            this.lblInfo.Text = "Enter the SQL Server Address for the database connection \nand ensure that RWDE Da" +
    "tabase is Already Created.";
            // 
            // frmConnectionSetup
            // 
            this.ClientSize = new System.Drawing.Size(434, 123);
            this.Controls.Add(this.txtDataSource);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblInfo);
            this.Name = "frmConnectionSetup";
            this.Text = "Data Source Setup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}