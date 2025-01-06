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
            BackColor = Color.White;
            WindowState = FormWindowState.Maximized;
            DbHelper dbHelper = new DbHelper();
            connectionString = dbHelper.GetConnectionString();
            InitializeDataGridView();
            RegisterEvents(this); //Assigning events to all Controls
        }

        public sealed override Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        private void Control_MouseHover(object sender, EventArgs e)//Changing Cursor as Hand on hover
        {
            try
            {
                Cursor = Cursors.Hand;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Control_MouseLeave(object sender, EventArgs e)//Changing back default Cursor on Leave
        {
            try
            {
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void RegisterEvents(Control parent)//Assigning events to all Controls
        {
            try
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
                        //Assigning events to child Controls
                        RegisterEvents(control);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void InitializeDataGridView()//to load the default gridView
        {
            try
            {
                dataGridView.AutoGenerateColumns = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)//to select the Error file
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
        private bool IsAllowedFileType(string filePath)//to check whether the selected filetype  is Excel or not
        {
            try
            {
                string extension = Path.GetExtension(filePath).ToLowerInvariant();
                return extension == Constants.XlsxExtention;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private void ProcessExcelData(string sourceFileName)//to read  and store excel data in Database
        {
            try
            {
                DataTable excelData = ReadExcelFile(sourceFileName);//to read the excel

                // Ensure the required columns exist
                if (!excelData.Columns.Contains(Constants.HccTable) || !excelData.Columns.Contains(Constants.ErrorMessage))
                {
                    MessageBox.Show(Constants.TheRequiredColumnsAreMissing, Constants.DownloadHccErrors);
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
                                string sourceFileNameStr = row[Constants.SourceFileName].ToString();
                                string hccTable = row[Constants.HccTable].ToString();
                                string errorMessage = row[Constants.ErrorMessage].ToString();
                                string clientId = row[Constants.SourceId].ToString();

                                // Replace values in HccTable based on specific cases
                                switch (hccTable)
                                {
                                    case Constants.ClntDemo:
                                    case Constants.ClntEthnDtl:
                                        hccTable = Constants.Hccclients;
                                        break;
                                    case Constants.ClntHivInfo:
                                        hccTable = Constants.HccClientMedCd4;
                                        break;
                                    case Constants.ClntHivTest:
                                        hccTable = Constants.HccClientHivTest;
                                        break;
                                    case Constants.ClntLvngSttn:
                                        hccTable = Constants.HccLvngSttn;
                                        break;
                                    case Constants.ClntRaceDtl:
                                        hccTable = Constants.HccClientRace;
                                        break;
                                    case Constants.ClntSite:
                                        hccTable = Constants.HccClientAddr;
                                        break;
                                    case Constants.ClntHsngAsstnc:
                                        hccTable = Constants.HccLvngSttn;
                                        break;
                                    case Constants.ClntHshldIncome:
                                        hccTable = Constants.HccClients;
                                        break;

                                    default:
                                        if (hccTable.Contains(Constants.Site))
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
                                InsertIntoDatabase(conn, transaction, hccTable, errorMessage, clientId, sourceFileNameStr);
                                AddRowToGrid(errorMessage, hccTable, clientId, sourceFileNameStr);
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AddRowToGrid(string errorMessage, string hccTable, string clientId, string sourceFileName)//to add every row in excel to grid
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void InsertIntoDatabase(SqlConnection conn, SqlTransaction transaction, string hccTable, string errorMessage, string clientId, string sourceFileName)//to insert the data into the database
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

        private DataTable ReadExcelFile(string filePath)//to read the excel file with header
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)//to close the form
        {
            try
            {
                // Close the current form (dispose it)
                Close();
                Application.Restart();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnUpload_Click(object sender, EventArgs e)//to read  and store excel data in Database
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnSubmit_Click(object sender, EventArgs e)//to fetch the data of the given Source file 
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnClr_Click(object sender, EventArgs e)//to set all default values
        {
            try
            {
                InitializeDataGridView();
                dataGridView.Rows.Clear();
                dataGridView.Rows.Clear();

                txtPath.Text = "";
                txtFileName.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}