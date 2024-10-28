using ExcelDataReader;
using Rwde;
using RWDE;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Media.Animation;
using static Spire.Pdf.General.Render.Decode.Jpeg2000.j2k.codestream.HeaderInfo;

namespace RWDE_UPLOADS_FILES
{
    public partial class LOAD : Form
    {
        private readonly string connectionString;
        private readonly DBHelper dbHelper;

        public LOAD()
        {
            InitializeComponent();
            this.BackColor = Color.White;
            this.WindowState = FormWindowState.Maximized;
            dbHelper = new DBHelper();


            connectionString = dbHelper.GetConnectionString();

            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            dataGridView.AutoGenerateColumns = false;
            //dataGridView.Columns.Clear();
           // DataGridViewTextBoxColumn SourceFileName = new DataGridViewTextBoxColumn();
           // //SourceFileName.Name = "Status";
           // //SourceFileName.HeaderText = "HCC Table";
           // SourceFileName.Width = 500;
           // SourceFileName.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
           // dataGridView.Columns.Add(SourceFileName);
           // DataGridViewTextBoxColumn clientid = new DataGridViewTextBoxColumn();
           // //clientid.Name = "Message";
           // //clientid.HeaderText = "Error Message";
           // clientid.Width = 900;
           // clientid.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
           // dataGridView.Columns.Add(clientid);
           // // Create "Status" column
           // DataGridViewTextBoxColumn statusColumn = new DataGridViewTextBoxColumn();
           //// statusColumn.Name = "SourceId";
           // //statusColumn.HeaderText = "SourceId";
           // statusColumn.Width = 200;
           // statusColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
           // dataGridView.Columns.Add(statusColumn);

           // // Create "Message" column
           // DataGridViewTextBoxColumn messageColumn = new DataGridViewTextBoxColumn();
           // //messageColumn.Name = "SourceFileName";
           // //messageColumn.HeaderText = "SourceFileName";
           // messageColumn.Width = 900;
           // messageColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
           // dataGridView.Columns.Add(messageColumn);

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
        private void ProcessExcelData(string sourceFileName)
        {
            DataTable excelData = ReadExcelFile(sourceFileName);

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
                            string SourceFileName = row["SourceFileName"].ToString();
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
                            InsertIntoDatabase(conn, transaction, hccTable, errorMessage, clientId, SourceFileName);
                            AddRowToGrid(errorMessage, hccTable, clientId, SourceFileName);
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
        private void ProcessExcelDataFILTER(string FILENAME)
        {
            DataTable excelData = ReadExcelFile(FILENAME);

            // Ensure the required columns exist
            if (!excelData.Columns.Contains("HccTable") || !excelData.Columns.Contains("ErrorMessage"))
            {
                MessageBox.Show("The required columns 'HccTable' or 'ErrorMessage' are missing in the Excel sheet.", "Download HCC Errors");
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
                            string sourceFileName = row["SourceFileName"]?.ToString();
                            string hccTable = row["HccTable"]?.ToString();
                            string errorMessage = row["ErrorMessage"]?.ToString();
                            string clientId = row["SourceId"]?.ToString();

                            // Check for missing or invalid values
                            if (string.IsNullOrWhiteSpace(hccTable))
                            {
                                MessageBox.Show("HccTable value is missing or invalid.", "Invalid Data");
                                continue; // Skip this row
                            }

                            if (string.IsNullOrWhiteSpace(errorMessage))
                            {
                                MessageBox.Show("ErrorMessage value is missing or invalid.", "Invalid Data");
                                continue; // Skip this row
                            }

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
                        MessageBox.Show("Data processed and inserted into the database successfully.", "Success");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Error inserting data into the database: " + ex.Message, "Error");
                    }
                }
            }
        }

        private void CallFilterSourceFileNameSP(string sourceFileName)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("FILTERSOURCEFILENAME", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SourceFileName", sourceFileName);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Assuming you want to read the data and do something with it
                        while (reader.Read())
                        {
                            // Process the results as needed
                            // For example, you might display them in a DataGridView
                            Console.WriteLine(reader["ColumnName"]); // Replace "ColumnName" with the actual column name
                        }
                    }
                }
            }
        }

        private void AddRowToGrid(string errorMessage, string hccTable, string clientId,string SourceFileName)
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
            dataGridView.Rows.Add(hccTable, errorMessage, clientId, SourceFileName);
        }
        private void InsertIntoDatabase(SqlConnection conn, SqlTransaction transaction, string hccTable, string errorMessage, string clientId, string SourceFileName)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO HCC_ErrorLog (HccTable, ErrorMessage, ClientId, SourceFileName) VALUES (@HccTable, @ErrorMessage, @ClientId, @SourceFileName)", conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@HccTable", hccTable);
                    cmd.Parameters.AddWithValue("@ErrorMessage", errorMessage);

                    // Check if clientId starts with "246_" and remove it if it does
                    string modifiedClientId = clientId.StartsWith("246_") ? clientId.Substring(4) : clientId;

                    cmd.Parameters.AddWithValue("@ClientId", modifiedClientId);
                    cmd.Parameters.AddWithValue("@SourceFileName", SourceFileName);

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

        private void AddRowToGrid(string errorMessage, string status)
        {
            // Add a new row to the DataGridView with error message and status
            dataGridView.Rows.Add(status, errorMessage);
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle any additional logic on cell click if needed
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
           
            Application.Restart();
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            // Handle date change logic if needed
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (txtPath.Text == "") { 
                MessageBox.Show(Constants.nodatatoinsertintotable, "Download HCC Errors");
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
                MessageBox.Show(Constants.theservicefilenamecannotbenull, "Download HCC Errors");
                return;
            }
            if (!txtFileName.Text.ToUpper().Contains("SERVICE") &&
    !txtFileName.Text.ToUpper().Contains("CLIENT"))
            {
                MessageBox.Show(Constants.nodataavailableforthissourcefilename, "Download HCC Errors");
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
               
            }



            //string filePath = txtPath.Text; // Text box for file path

            // Validate file path
            //if (!string.IsNullOrWhiteSpace(filePath) && File.Exists(filePath))
            //{
            //    try
            //    {
            //        // Process the Excel data using the valid file path
            //        ProcessExcelDataFILTER(sourceFileName);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("An error occurred while processing the file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Please provide a valid file path.", "Invalid Path", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }

        // Updated ProcessExcelDataFILTER method
        private void ProcessExcelDataFILTER(string sourceFileName, string serviceColumnValue)
        {
            DataTable excelData = ReadExcelFile(sourceFileName);

            // Ensure the required columns exist
            if (!excelData.Columns.Contains("HccTable") || !excelData.Columns.Contains("ErrorMessage"))
            {
                MessageBox.Show("The required columns 'HccTable' or 'ErrorMessage' are missing in the Excel sheet.", "Download HCC Errors");
                return;
            }

            // Check if the column "Service" exists for filtering
            if (!excelData.Columns.Contains("serviceColumnValue"))
            {
                MessageBox.Show("The column 'Service' is missing in the Excel sheet.", "Missing Column", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
            }

            // Filter the rows based on the user-provided Service column value
            DataRow[] filteredRows = excelData.Select($"Service = '{serviceColumnValue}'");

            if (filteredRows.Length == 0)
            {
                MessageBox.Show($"No records found for the specified service: '{serviceColumnValue}'", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Prepare to insert into the database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (DataRow row in filteredRows)
                        {
                            string sourceFile = row["SourceFileName"].ToString();
                            string hccTable = row["HccTable"].ToString();
                            string errorMessage = row["ErrorMessage"].ToString();
                            string clientId = row["SourceId"].ToString();

                            // Replace values in HccTable based on specific cases
                            hccTable = DetermineHccTable(hccTable);

                            // Insert into database and add the row to the grid
                            InsertIntoDatabase(conn, transaction, hccTable, errorMessage, clientId, sourceFile);
                            AddRowToGrid(errorMessage, hccTable, clientId, sourceFile);
                        }

                        transaction.Commit();
                        MessageBox.Show("Filtered data processed and inserted into the database successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Error inserting filtered data into the database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Method to determine the appropriate HCC table name based on input
        private string DetermineHccTable(string hccTable)
        {
            // Map input HccTable names to their corresponding database tables
            switch (hccTable)
            {
                case "T_CLNT_DEMO":
                case "T_CLNT_ETHN_DTL":
                    return "HCCCLIENTS"; // Mapped to HCCCLIENTS
                case "T_CLNT_HIV_INFO":
                    return "HCCClientMedCD4"; // Mapped to HCCClientMedCD4
                case "T_CLNT_HIV_TEST":
                    return "HCCClientHIVTest"; // Mapped to HCCClientHIVTest
                case "T_CLNT_LVNG_STTN":
                    return "HCCLvngSttn"; // Mapped to HCCLvngSttn
                case "T_CLNT_RACE_DTL":
                    return "HCCClientRace"; // Mapped to HCCClientRace
                case "T_CLNT_SITE":
                    return "HCCClientAddr"; // Mapped to HCCClientAddr
                default:
                    // Check for any additional conditions or unexpected cases
                    if (hccTable.Contains("T_SITE"))
                    {
                        return "HCCServices"; // Handle site-related tables
                    }
                    else
                    {
                        // If none match, return an empty string or a default value
                        return ""; // Can be adjusted based on needs
                    }
            }
        }


        //private void ProcessExcelData(string filePath, string sourceFileName)
        //{
        //    DataTable excelData = ReadExcelFile(filePath);

        //    // Ensure the required columns exist
        //    if (!excelData.Columns.Contains("HccTable") || !excelData.Columns.Contains("ErrorMessage") || !excelData.Columns.Contains("SourceFileName"))
        //    {
        //        MessageBox.Show("The required columns 'HCCTABLE', 'ErrorMessage', or 'SourceFileName' are missing in the Excel sheet.");
        //        return;
        //    }

        //    // Filter rows where the SourceFileName matches the given sourceFileName
        //    var matchingRows = excelData.AsEnumerable()
        //        .Where(row => row.Field<string>("SourceFileName").Equals(sourceFileName, StringComparison.OrdinalIgnoreCase));

        //    if (!matchingRows.Any())
        //    {
        //        MessageBox.Show($"No matching data found for SourceFileName: {sourceFileName}");
        //        return;
        //    }

        //    // Process each matching row
        //    foreach (DataRow row in matchingRows)
        //    {
        //        string hccTable = row["HccTable"].ToString();
        //        string errorMessage = row["ErrorMessage"].ToString();

        //        // Replace values in HccTable based on specific cases
        //        switch (hccTable.ToUpper())
        //        {
        //            case "T_CLNT_DEMO":
        //            case "T_CLNT_ETHN_DTL":
        //                hccTable = "HCCCLIENTS";
        //                break;
        //            case "T_CLNT_HIV_INFO":
        //                hccTable = "HCCClientMedCD4";
        //                break;
        //            case "T_CLNT_HIV_TEST":
        //                hccTable = "HCCClientHIVTest";
        //                break;
        //            case "T_CLNT_LVNG_STTN":
        //                hccTable = "HCCLvngSttn";
        //                break;
        //            case "T_CLNT_RACE_DTL":
        //                hccTable = "HCCClientRace";
        //                break;
        //            case "T_CLNT_SITE":
        //                hccTable = "HCCClientAddr";
        //                break;
        //            default:
        //                hccTable = "Error";
        //                errorMessage = string.IsNullOrWhiteSpace(errorMessage) ? "Invalid HCCTABLE value." : errorMessage;
        //                break;
        //        }

        //        // Add row to DataGridView with the modified table name and error message
        //        AddRowToGrid(errorMessage, hccTable);
        //    }
        //}


        // Method to add a row to the DataGridView
        private void AddRowToGriddata(string errorMessage, string hccTable)
        {
            // Assuming your DataGridView is named 'dataGridView'
            dataGridView.Rows.Add(hccTable, errorMessage);
        }

        // Method to read data from an Excel file (placeholder, assuming you already have this method implemented)
        private DataTable ReadExcelFiledata(string filePath)
        {
            // Your implementation for reading the Excel file into a DataTable
            return new DataTable();
        }
        private void ProcessExcelData(string filePath, string sourceFileName)
        {
            DataTable excelData = ReadExcelFile(filePath);

            // Ensure the required columns exist
            if (!excelData.Columns.Contains("HccTable") || !excelData.Columns.Contains("ErrorMessage") || !excelData.Columns.Contains("SourceFileName"))
            {
                MessageBox.Show("The required columns 'HCCTABLE', 'ErrorMessage', or 'SourceFileName' are missing in the Excel sheet.");
                return;
            }

            // Filter rows where the SourceFileName column contains the word "services" (case-insensitive)
            var matchingRows = excelData.AsEnumerable()
                .Where(row => row.Field<string>("SourceFileName")?.IndexOf("Service", StringComparison.OrdinalIgnoreCase) >= 0);

            // If no rows match, show a message
            if (!matchingRows.Any())
            {
                MessageBox.Show($"No matching data found for rows containing 'Service' in SourceFileName", "Download HCC Errors");
                return;
            }

            // Process each matching row
            foreach (DataRow row in matchingRows)
            {
                string hccTable = row["HccTable"].ToString();
                string errorMessage = row["ErrorMessage"].ToString();
                string clientid = row["SourceId"].ToString();
                string SourceFileName = row["SourceFileName"].ToString();

                // Replace values in HccTable based on specific cases
                switch (hccTable.ToUpper())
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
                    default:
                        hccTable = "Error";
                        errorMessage = string.IsNullOrWhiteSpace(errorMessage) ? "Invalid HCCTABLE value." : errorMessage;
                        break;
                }

                // Add row to DataGridView with the modified table name and error message
                AddRowToGrid(errorMessage, hccTable);
            }
        }

        private void btnClr_Click(object sender, EventArgs e)
        {
            InitializeDataGridView();
           dataGridView.Rows.Clear();
            
            txtPath.Text = "";
            txtFileName.Text = "";

        }

       
    }
}
