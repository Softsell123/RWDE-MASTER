using System;
using System.Windows.Forms;

namespace RWDE_UPLOADS_FILES
{
    partial class ServiceIDLists
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
            this.SuspendLayout();
            // 
            // ServiceIDLists
            // 
            this.ClientSize = new System.Drawing.Size(960, 253);
            this.Name = "ServiceIDLists";
            this.ResumeLayout(false);

        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        #endregion

        private System.Windows.Forms.Panel lblTilte;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.DataGridView dataGridView;
        private Button btnClose;
        private Button btnAdd;
        private Button btnSave;
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
        private FlowLayoutPanel flowLayoutPanel1;
    }
}