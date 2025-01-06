using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.AccessControl;
using System.Windows.Forms;

namespace RWDE
{
    public partial class FrmConnectionSetup : Form
    {
        public FrmConnectionSetup()
        {
            InitializeComponent();
            LoadCurrentDataSource();
        }

        // Load the current Data Source into the textbox
        private void LoadCurrentDataSource()
        {
            try
            {
                string currentConnectionString = ConfigurationManager.ConnectionStrings[Constants.MyConnection].ConnectionString;
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(currentConnectionString);
                txtDataSource.Text = builder.DataSource; // Load current Data Source
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)//To Save the Given DataSource
        {
            try
            {
                string newDataSource = txtDataSource.Text.Trim();

                if (string.IsNullOrEmpty(newDataSource))
                {
                    MessageBox.Show(Constants.DataSourcecannotbeempty, Constants.ValidationError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // Validate if the Data Source is accessible
                if (ValidateDataSource(newDataSource))
                {
                    // Update only the Data Source in the connection string
                    UpdateConnectionString(newDataSource);//to update the new Data Source in connectionString
                    MessageBox.Show(Constants.DataSourceupdatedsuccessfully, Constants.Success, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show(Constants.UnabletoconnectPleasecheckandtryagain, Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)// to close the Application
        {
            try
            {
                Application.Exit(); // Exit if no Data Source is set
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Validate if the Data Source is accessible
        private bool ValidateDataSource(string dataSource)
        {
            try
            {
                string currentConnectionString = ConfigurationManager.ConnectionStrings[Constants.MyConnection].ConnectionString;
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
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var connectionStringSettings = config.ConnectionStrings.ConnectionStrings[Constants.MyConnection];
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionStringSettings.ConnectionString)
                {
                    DataSource = newDataSource
                };
                connectionStringSettings.ConnectionString = builder.ToString();
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(Constants.ConnectionStrings);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
