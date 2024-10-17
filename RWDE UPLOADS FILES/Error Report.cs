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
            dataGridView.Columns.Clear();

            // Create "Status" column
            DataGridViewTextBoxColumn statusColumn = new DataGridViewTextBoxColumn();
            statusColumn.Name = "Status";
            statusColumn.HeaderText = "Error Status";
            statusColumn.Width = 200;
            statusColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns.Add(statusColumn);

            // Create "Message" column
            DataGridViewTextBoxColumn messageColumn = new DataGridViewTextBoxColumn();
            messageColumn.Name = "Message";
            messageColumn.HeaderText = "Error Message";
            messageColumn.Width = 900;
            messageColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns.Add(messageColumn);
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
        private void ProcessExcelData(string filePath)
        {
            DataTable excelData = ReadExcelFile(filePath);

            // Ensure the required columns exist
            if (!excelData.Columns.Contains("HccTable") || !excelData.Columns.Contains("ErrorMessage"))
            {
                MessageBox.Show("The required columns 'HCCTABLE' or 'ErrorMessage' are missing in the Excel sheet.","Download HCC Errors");
                return;
            }

            foreach (DataRow row in excelData.Rows)
            {
                string hccTable = row["HccTable"].ToString();
                string errorMessage = row["ErrorMessage"].ToString();

                // Replace values in HccTable based on specific cases
                switch (hccTable)
                {
                    case "T_CLNT_DEMO":
                        hccTable = "HCCCLIENTS";
                        break;
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
                    // Add other cases as needed
                    default:
                        hccTable = "Error";
                        errorMessage = string.IsNullOrWhiteSpace(errorMessage) ? "Invalid HCCTABLE value." : errorMessage;
                        break;
                }

                // Add row to DataGridView with the modified table name and error message
                AddRowToGrid(errorMessage, hccTable);
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
            this.Close();
            Application.Restart();
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            // Handle date change logic if needed
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            string selectedFilePath = txtPath.Text;
                       
                        // Validate the selected file is an Excel file
                        if (IsAllowedFileType(selectedFilePath))
                        {
                            // Process the selected Excel file
                            ProcessExcelData(selectedFilePath);
                        }
                        else
                        {
                            MessageBox.Show("Please select a valid Excel (.xlsx) file.", "Download HCC Errors");
                        }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string sourceFileName = txtFileName.Text; // Text box for SourceFileName
            string filePath = txtPath.Text; // Text box for file path

            // Validate file path
            if (!string.IsNullOrWhiteSpace(filePath) && File.Exists(filePath))
            {
                ProcessExcelData(filePath, sourceFileName);
            }
            else
            {
                MessageBox.Show("Please provide a valid file path.");
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

            // Filter rows where the SourceFileName matches the given sourceFileName (case-insensitive)
            var matchingRows = excelData.AsEnumerable()
                .Where(row => row.Field<string>("SourceFileName").Equals(sourceFileName, StringComparison.OrdinalIgnoreCase));

            // If no rows match, show a message
            if (!matchingRows.Any())
            {
                MessageBox.Show($"No matching data found for SourceFileName", "Download HCC Errors");
                return;
            }

            // Process each matching row
            foreach (DataRow row in matchingRows)
            {
                string hccTable = row["HccTable"].ToString();
                string errorMessage = row["ErrorMessage"].ToString();

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
           
            txtPath.Text = "";
            txtFileName.Text = "";

        }
    }
}
