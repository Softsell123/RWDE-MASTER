using ExcelDataReader;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace RWDE
{
    public partial class FrmDownloadHccErrors : Form
    {
        private readonly string connectionString;

        public FrmDownloadHccErrors()
        {
            InitializeComponent();
            this.BackColor = Color.White;
            this.WindowState = FormWindowState.Maximized;
            DbHelper dbHelper = new DbHelper();
            connectionString = dbHelper.GetConnectionString();
            InitializeDataGridView();
            RegisterEvents(this);
        }
        private void Control_MouseHover(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }
        private void Control_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }
        private void RegisterEvents(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is Button || control is CheckBox || control is DateTimePicker || control is ScrollBar)
                {
                    control.MouseHover += Control_MouseHover;
                    control.MouseLeave += Control_MouseLeave;
                }
                // Check for child controls in containers
                if (control.HasChildren)
                {
                    RegisterEvents(control);
                }
            }
        }
        private void InitializeDataGridView()
        {
            dataGridView.AutoGenerateColumns = false;
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = Constants.ExcelFilesXlsx;
                    openFileDialog.Title = Constants.SelectAnExcelFile;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string selectedFilePath = openFileDialog.FileName;
                        txtPath.Text = selectedFilePath;
                        // Validate the selected file is an Excel
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool IsAllowedFileType(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            return extension == Constants.XlsxExtention;
        }
        private void ProcessExcelData(string SourceFileName)
        {
            DataTable excelData = ReadExcelFile(SourceFileName);
            
            // Ensure the required columns exist
            if (!excelData.Columns.Contains(Constants.HccTable) || !excelData.Columns.Contains(Constants.ErrorMessage))
            {
                MessageBox.Show(Constants.TheRequiredColumnsAreMissing,Constants.DownloadHccErrors);
                return;
            }

            // Prepare to insert into database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (DataRow row in excelData.Rows)
                        {
                            string sourceFileName = row[Constants.SourceFileName].ToString();
                            string hccTable = row[Constants.HccTable].ToString();
                            string errorMessage = row[Constants.ErrorMessage].ToString();
                            string clientId = row[Constants.SourceId].ToString();
                            
                            // Replace values in HccTable based on specific cases
                            switch (hccTable)
                            {
                                case Constants.T_CLNT_DEMO:
                                case Constants.T_CLNT_ETHN_DTL:
                                    hccTable = Constants.HCCCLIENTS;
                                    break;
                                case Constants.T_CLNT_HIV_INFO:
                                    hccTable = Constants.HCCClientMedCD4;
                                    break;
                                case Constants.T_CLNT_HIV_TEST:
                                    hccTable = Constants.HCCClientHIVTest;
                                    break;
                                case Constants.T_CLNT_LVNG_STTN:
                                    hccTable =Constants.HCCLvngSttn;
                                    break;
                                case Constants.T_CLNT_RACE_DTL:
                                    hccTable = Constants.HCCClientRace;
                                    break;
                                case Constants.T_CLNT_SITE:
                                    hccTable = Constants.HCCClientAddr;
                                    break;
                                case Constants.T_CLNT_HSNG_ASSTNC:
                                    hccTable = Constants.HCCLvngSttn;
                                    break;
                                case Constants.T_CLNT_HSHLD_INCOME:
                                    hccTable = Constants.HccClients;
                                    break;

                                default:
                                    if (hccTable.Contains(Constants.T_SITE))
                                    {
                                        hccTable = Constants.HccServices;
                                    }
                                    else
                                    {
                                        // Handle unexpected cases or set a default value
                                        hccTable = "";
                                    }
                                    break;
                            }
                            // Insert into database
                            InsertIntoDatabase(conn, transaction, hccTable, errorMessage, clientId, sourceFileName);
                            AddRowToGrid(errorMessage, hccTable, clientId, sourceFileName);
                        }
                        transaction.Commit();
                        MessageBox.Show(Constants.Dataprocessedandinsertedintothedatabasesuccessfully, Constants.DownloadHccErrors);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show(Constants.Errorinsertingdataintothedatabase + ex.Message, Constants.DownloadHccErrors);
                    }
                }
            }
        }
        private void AddRowToGrid(string errorMessage, string hccTable, string clientId, string sourceFileName)
        {
            // Ensure DataGridView is already created and columns are defined
            if (dataGridView.Columns.Count == 0)
            {
                // Create columns if they don't exist
                dataGridView.Columns.Add(Constants.HccTable, Constants.HccTableSp);
                dataGridView.Columns.Add(Constants.ErrorMessage, Constants.ErrorMessageSp);
                dataGridView.Columns.Add(Constants.SourceId, Constants.ClientIdSp);
                dataGridView.Columns.Add(Constants.SourceFileName, Constants.SourceFileName);
            }
            // Add a new row to the DataGridView
            dataGridView.Rows.Add(hccTable, errorMessage, clientId, sourceFileName);
        }
        private void InsertIntoDatabase(SqlConnection conn, SqlTransaction transaction, string hccTable, string errorMessage, string clientId, string sourceFileName)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(Constants.InsertIntoDatabaseQuery, conn, transaction))
                {
                    cmd.Parameters.AddWithValue(Constants.AtHccTable, hccTable);
                    cmd.Parameters.AddWithValue(Constants.AtErrorMessage, errorMessage);

                    // Check if clientId starts with "246_" and remove it if it does
                    string modifiedClientId = clientId.StartsWith(Constants.AgencyCode) ? clientId.Substring(4) : clientId;

                    cmd.Parameters.AddWithValue(Constants.AtClientId, modifiedClientId);
                    cmd.Parameters.AddWithValue(Constants.AtSourceFileName, sourceFileName);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions
                Console.WriteLine($@"{Constants.SqlError}{ex.Message}");
                transaction.Rollback(); // Rollback transaction if needed
            }
            catch (Exception ex)
            {
                // Handle any other exceptions
                Console.WriteLine($@"{Constants.Errorsp}{ex.Message}");
                transaction.Rollback(); // Rollback transaction if needed
            }
        }

        private DataTable ReadExcelFile(string filePath)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                    });

                    // Returning the first sheet as a DataTable
                    return result.Tables[0];
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                // Close the current form (dispose it)
                this.Close();
                Application.Restart();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (txtPath.Text == "")
            {
                MessageBox.Show(Constants.Nodatatoinsertintotable, Constants.DownloadHccErrors);
                return;
            }
            string selectedFilePath = txtPath.Text;
            // Validate the selected file is an Excel file
            if (IsAllowedFileType(selectedFilePath))
            {
                // Process the selected Excel file
                ProcessExcelData(selectedFilePath);
            }
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtFileName.Text == "")
            {
                MessageBox.Show(Constants.Theservicefilenamecannotbenull, Constants.DownloadHccErrors);
                return;
            }
            if (!txtFileName.Text.ToUpper().Contains(Constants.UpperService) && !txtFileName.Text.ToUpper().Contains(Constants.UpperClient))
            {
                MessageBox.Show(Constants.Nodataavailableforthissourcefilename, Constants.DownloadHccErrors);
                return;
            }
            dataGridView.AutoGenerateColumns = false;
            dataGridView.Columns.Clear();
            string sourceFileName = txtFileName.Text; // Text box for SourceFileName
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(Constants.Filtersourcefilename, conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(Constants.AtSourceFileName, sourceFileName);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            try
                            {
                                // Open the connection
                                conn.Open();
                                // Fill the DataTable with the result of the stored procedure
                                adapter.Fill(dt);
                                dataGridView.DataSource = dt;
                                dataGridView.AutoGenerateColumns = true;
                                dataGridView.AllowUserToAddRows = false;
                            }
                            catch (Exception ex)
                            {
                                // Handle exceptions (e.g., logging)
                                Console.WriteLine(Constants.Errorsp + ex.Message);
                            }
                            command.ExecuteNonQuery();
                            // Execute the SQL command to insert client data
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnClr_Click(object sender, EventArgs e)
        {
            InitializeDataGridView();
            dataGridView.Rows.Clear();
            dataGridView.Rows.Clear();

            txtPath.Text = "";
            txtFileName.Text = "";
        }
    }
}