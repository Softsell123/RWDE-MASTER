using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RWDE_UPLOADS_FILES
{
    public partial class frmConnectionSetup : Form
    {
        public frmConnectionSetup()
        {
            InitializeComponent();
            LoadCurrentDataSource();
        }

        // Load the current Data Source into the textbox
        private void LoadCurrentDataSource()
        {
            string currentConnectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(currentConnectionString);
            txtDataSource.Text = builder.DataSource; // Load current Data Source
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string newDataSource = txtDataSource.Text.Trim();

            if (string.IsNullOrEmpty(newDataSource))
            {
                MessageBox.Show("Data Source cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ValidateDataSource(newDataSource))
            {
                UpdateConnectionString(newDataSource);
                MessageBox.Show("Data Source updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Unable to connect to the specified Data Source. Please check and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Exit if no Data Source is set
        }

        // Validate if the Data Source is accessible
        private bool ValidateDataSource(string dataSource)
        {
            try
            {
                string currentConnectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(currentConnectionString)
                {
                    DataSource = dataSource
                };

                using (SqlConnection connection = new SqlConnection(builder.ToString()))
                {
                    connection.Open(); // Test the connection
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Update only the Data Source in the connection string
        private void UpdateConnectionString(string newDataSource)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringSettings = config.ConnectionStrings.ConnectionStrings["MyConnection"];
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionStringSettings.ConnectionString)
            {
                DataSource = newDataSource
            };

            connectionStringSettings.ConnectionString = builder.ToString();
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("connectionStrings");
        }
    }
}
