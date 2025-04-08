using System;
using System.Windows.Forms;

namespace RWDE
{
    sealed partial class ServiceCodeSetup
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblHeading = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblTilte = new System.Windows.Forms.Panel();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.ServiceCodeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServiceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaptoHcc = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ContractName = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.PrimaryServices = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SecondaryServices = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Subservice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitofMesurement = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.lblTilte.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            //
            //btnSave
            //
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnSave.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(1458, 838);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(200, 50);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            //
            //lblHeading
            //
            this.lblHeading.AutoSize = true;
            this.lblHeading.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.ForeColor = System.Drawing.Color.Black;
            this.lblHeading.Location = new System.Drawing.Point(111, 154);
            this.lblHeading.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(444, 33);
            this.lblHeading.TabIndex = 1;
            this.lblHeading.Text = "All ServiceCodes  are displayed below.";
            //btnAdd
            //
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnAdd.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(1555, 141);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(332, 46);
            this.btnAdd.TabIndex = 14;
            this.btnAdd.Text = "Add New Service";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            //
            //btnClose
            //
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnClose.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(1709, 838);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(200, 50);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = Constants.Close;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            //
            //lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Location = new System.Drawing.Point(865, 68);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(239, 35);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Service Code SetUp";
            //
            //lblTilte
            //
            this.lblTilte.BackColor = System.Drawing.Color.White;
            this.lblTilte.Controls.Add(this.dataGridView);
            this.lblTilte.Controls.Add(this.btnSave);
            this.lblTilte.Controls.Add(this.btnClose);
            this.lblTilte.Controls.Add(this.btnAdd);
            this.lblTilte.Controls.Add(this.lblHeading);
            this.lblTilte.Controls.Add(this.lblTitle);
            this.lblTilte.ForeColor = System.Drawing.Color.Black;
            this.lblTilte.Location = new System.Drawing.Point(2, 0);
            this.lblTilte.Margin = new System.Windows.Forms.Padding(4);
            this.lblTilte.Name = "lblTilte";
            this.lblTilte.Size = new System.Drawing.Size(2900, 1365);
            this.lblTilte.TabIndex = 12;
            //
            //dataGridView
            //
            this.dataGridView.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.ColumnHeadersHeight = 60;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ServiceCodeID,
            this.ServiceName,
            this.MaptoHcc,
            this.ContractName,
            this.PrimaryServices,
            this.SecondaryServices,
            this.Subservice,
            this.UnitofMesurement,
            this.Rate,
            this.Status});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView.GridColor = System.Drawing.Color.Black;
            this.dataGridView.Location = new System.Drawing.Point(117, 211);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(5);
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView.RowHeadersWidth = 40;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            this.dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView.Size = new System.Drawing.Size(1792, 603);
            this.dataGridView.TabIndex = 3;
            this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentClick);
            //
            //ServiceCodeID
            //
            this.ServiceCodeID.HeaderText = "ServiceCode ID";
            this.ServiceCodeID.MinimumWidth = 6;
            this.ServiceCodeID.Name = Constants.ServiceCodeId;
            this.ServiceCodeID.ReadOnly = true;
            this.ServiceCodeID.Width = 150;
            //
            //ServiceName
            //
            this.ServiceName.HeaderText = "Service Name";
            this.ServiceName.MinimumWidth = 6;
            this.ServiceName.Name = "ServiceName";
            this.ServiceName.ReadOnly = true;
            this.ServiceName.Width = 150;
            //
            //MaptoHcc
            //
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.MaptoHcc.DefaultCellStyle = dataGridViewCellStyle2;
            this.MaptoHcc.HeaderText = "Map to HCC ";
            this.MaptoHcc.MinimumWidth = 6;
            this.MaptoHcc.Name = "MaptoHcc";
            this.MaptoHcc.ReadOnly = true;
            this.MaptoHcc.Width = 150;
            //
            //ContractName
            //
            this.ContractName.DataPropertyName = "drop";
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            this.ContractName.DefaultCellStyle = dataGridViewCellStyle3;
            this.ContractName.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.ContractName.HeaderText = "Contract Name";
            this.ContractName.MinimumWidth = 6;
            this.ContractName.Name = Constants.ContractName;
            this.ContractName.ReadOnly = true;
            this.ContractName.Width = 180;
            //
            //PrimaryServices
            //
            this.PrimaryServices.HeaderText = "Primary Services";
            this.PrimaryServices.MinimumWidth = 6;
            this.PrimaryServices.Name = "PrimaryServices";
            this.PrimaryServices.ReadOnly = true;
            this.PrimaryServices.Width = 180;
            //
            //SecondaryServices
            //
            this.SecondaryServices.HeaderText = Constants.SecondaryService;
            this.SecondaryServices.MinimumWidth = 6;
            this.SecondaryServices.Name = "SecondaryServices";
            this.SecondaryServices.ReadOnly = true;
            this.SecondaryServices.Width = 210;
            //
            //Subservice
            //
            this.Subservice.HeaderText = "SubService";
            this.Subservice.MinimumWidth = 6;
            this.Subservice.Name = "Subservice";
            this.Subservice.ReadOnly = true;
            this.Subservice.Width = 120;
            //
            //UnitofMesurement
            //
            this.UnitofMesurement.HeaderText = "Unit of Mesurement";
            this.UnitofMesurement.MinimumWidth = 6;
            this.UnitofMesurement.Name = "UnitofMesurement";
            this.UnitofMesurement.ReadOnly = true;
            this.UnitofMesurement.Width = 210;
            //
            //Rate
            //
            this.Rate.HeaderText = "Rate";
            this.Rate.MinimumWidth = 6;
            this.Rate.Name = "Rate";
            this.Rate.ReadOnly = true;
            this.Rate.Width = 120;
            //
            //Status
            //
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.Status.DefaultCellStyle = dataGridViewCellStyle4;
            this.Status.HeaderText = Constants.Status;
            this.Status.MinimumWidth = 6;
            this.Status.Name = Constants.Status;
            this.Status.ReadOnly = true;
            this.Status.Width = 120;
            //
            //ServiceCodeSetup
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1924, 1055);
            this.Controls.Add(this.lblTilte);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ServiceCodeSetup";
            this.RightToLeftLayout = true;
            this.Text = "ServiceCode_Setup";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ServiceCodeSetup_Load);
            this.lblTilte.ResumeLayout(false);
            this.lblTilte.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }
        private void ServiceCodeSetup_Load(object sender, EventArgs e)
        {
            //Set the form to be maximized on load
            this.WindowState = FormWindowState.Maximized;

            //Optional: Adjust the form to the screen size
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            //Optional: Center the form on the screen
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel lblTilte;
        private DataGridView dataGridView;
        private DataGridViewTextBoxColumn ServiceCodeID;
        private DataGridViewTextBoxColumn ServiceName;
        private DataGridViewComboBoxColumn MaptoHcc;
        private DataGridViewComboBoxColumn ContractName;
        private DataGridViewTextBoxColumn PrimaryServices;
        private DataGridViewTextBoxColumn SecondaryServices;
        private DataGridViewTextBoxColumn Subservice;
        private DataGridViewTextBoxColumn UnitofMesurement;
        private DataGridViewTextBoxColumn Rate;
        private DataGridViewComboBoxColumn Status;

    }
}