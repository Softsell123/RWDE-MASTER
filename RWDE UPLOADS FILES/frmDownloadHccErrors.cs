using ExcelDataReader;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace RWDE
{
    public partial class FrmDownloadHccErrors : Form
    {
        private readonly string connectionString;
        private readonly DbHelper dbHelper;

        public FrmDownloadHccErrors()
        {
            InitializeComponent();
            this.BackColor = Color.White;
            this.WindowState = FormWindowState.Maximized;
            dbHelper = new DbHelper();
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
                if (control is System.Windows.Forms.Button || control is CheckBox || control is DateTimePicker || control is ScrollBar)
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
                    openFileDialog.Filter = "Excel Files|*.xlsx";
                    openFileDialog.Title = "Select an Excel File";

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
            return extension == ".xlsx";
        }
        private void ProcessExcelData(string SourceFileName)
        {
            DataTable excelData = ReadExcelFile(SourceFileName);

            // Ensure the required columns exist
            if (!excelData.Columns.Contains("HccTable") || !excelData.Columns.Contains("ErrorMessage"))
            {
                MessageBox.Show("The required columns 'HCCTABLE' or 'ErrorMessage' are missing in the Excel sheet.", "Download HCC Errors");
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
                            string sourceFileName = row["SourceFileName"].ToString();
                            string hccTable = row["HccTable"].ToString();
                            string errorMessage = row["ErrorMessage"].ToString();
                            string clientId = row["SourceId"].ToString();
                            
                            // Replace values in HccTable based on specific cases
                            switch (hccTable)
                            {
                                case "T_CLNT_DEMO":
                                case "T_CLNT_ETHN_DTL":
                                    hccTable = "HCCCLIENTS";
                                    break;
                                case "T_CLNT_HIV_INFO":
                                    hccTable = "HCCClientMedCD4";
                                    break;
                                case "T_CLNT_HIV_TEST":
                                    hccTable = "HCCClientHIVTest";
                                    break;
                                case "T_CLNT_LVNG_STTN":
                                    hccTable = "HCCLvngSttn";
                                    break;
                                case "T_CLNT_RACE_DTL":
                                    hccTable = "HCCClientRace";
                                    break;
                                case "T_CLNT_SITE":
                                    hccTable = "HCCClientAddr";
                                    break;
                                case "T_CLNT_HSNG_ASSTNC":
                                    hccTable = "HCCLvngSttn";
                                    break;
                                case "T_CLNT_HSHLD_INCOME":
                                    hccTable = "HCCClients";
                                    break;

                                default:
                                    if (hccTable.Contains("T_SITE"))
                                    {
                                        hccTable = "HCCServices";
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
                        MessageBox.Show("Data processed and inserted into the database successfully.", "Download HCC Errors");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Error inserting data into the database: " + ex.Message, "Download HCC Errors");
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
                dataGridView.Columns.Add("HccTable", "HCC Table");
                dataGridView.Columns.Add("ErrorMessage", "Error Message");
                dataGridView.Columns.Add("SourceId", "Client ID");
                dataGridView.Columns.Add("SourceFileName", "SourceFileName");
            }
            // Add a new row to the DataGridView
            dataGridView.Rows.Add(hccTable, errorMessage, clientId, sourceFileName);
        }
        private void InsertIntoDatabase(SqlConnection conn, SqlTransaction transaction, string hccTable, string errorMessage, string clientId, string sourceFileName)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO HCC_ErrorLog (HccTable, ErrorMessage, SourceId, SourceFileName) VALUES (@HccTable, @ErrorMessage, @ClientId, @SourceFileName)", conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@HccTable", hccTable);
                    cmd.Parameters.AddWithValue("@ErrorMessage", errorMessage);

                    // Check if clientId starts with "246_" and remove it if it does
                    string modifiedClientId = clientId.StartsWith("246_") ? clientId.Substring(4) : clientId;

                    cmd.Parameters.AddWithValue("@ClientId", modifiedClientId);
                    cmd.Parameters.AddWithValue("@SourceFileName", sourceFileName);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions
                Console.WriteLine($"SQL Error: {ex.Message}");
                transaction.Rollback(); // Rollback transaction if needed
            }
            catch (Exception ex)
            {
                // Handle any other exceptions
                Console.WriteLine($"Error: {ex.Message}");
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
                MessageBox.Show(Constants.Nodatatoinsertintotable, "Download HCC Errors");
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
                MessageBox.Show(Constants.Theservicefilenamecannotbenull, "Download HCC Errors");
                return;
            }
            if (!txtFileName.Text.ToUpper().Contains("SERVICE") && !txtFileName.Text.ToUpper().Contains("CLIENT"))
            {
                MessageBox.Show(Constants.Nodataavailableforthissourcefilename, "Download HCC Errors");
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
                    using (SqlCommand command = new SqlCommand("FILTERSOURCEFILENAME", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@SourceFileName", sourceFileName);
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
                                Console.WriteLine("Error: " + ex.Message);
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