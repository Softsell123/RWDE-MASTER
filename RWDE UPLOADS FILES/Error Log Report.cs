using ClosedXML.Excel;
using OfficeOpenXml;
using Rwde;
using RWDE;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;


namespace RWDE_UPLOADS_FILES
{
    public partial class Error_Log_Report : Form
    {
        public Error_Log_Report()//initialize data
        {
            InitializeComponent();
            this.ControlBox = false;
            this.WindowState = FormWindowState.Maximized;
            dtpStartDate.Value = DateTime.Now.AddYears(-1);
            dtpStartDate.CustomFormat = "MM-dd-yyyy";
            dtpEndDate.CustomFormat = "MM-dd-yyyy";
            dtpEndDate.Value = DateTime.Now;
        }


        private void btnReport_Click(object sender, EventArgs e)//to get the filtered data
        {
            try
            {
                DBHelper dbHelper = new DBHelper();
                dataGridView.AutoGenerateColumns = true;
                dataGridView.Columns.Clear();
                // Ensure the date pickers are properly set
                DateTime startDate = dtpStartDate.Value;
                DateTime endDate = dtpEndDate.Value;
                if (endDate <= startDate)
                {
                    MessageBox.Show(Constants.StartdatemustbeearlierthanEnddate);
                }
                try
                {
                    // Call the LoadData method to fetch the data

                    dataGridView.ForeColor = Color.Black;


                    DataTable result = dbHelper.LoadERRORLOG(startDate, endDate);

                    // Now you can use the result, e.g., bind it to a DataGridView or process it
                    dataGridView.DataSource = result;
                    //PopulateMonthYearGrid(startDate, endDate);
                }
                catch (Exception ex)
                {
                    // Handle exceptions, such as logging the error
                    MessageBox.Show(ex.Message);

                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show(ex.Message);
            }
                       
            }    
        private void btnClose_Click(object sender, EventArgs e)//to close the form
        {
            try { 
            // Close the current form (dispose it)
            this.Close();
            Application.Restart();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
      private void btnDownload_Click(object sender, EventArgs e)//to export the report to selected folder
        {
            try {
                if (dataGridView.Rows.Count == 0 || (dataGridView.Rows.Count == 1 && dataGridView.Rows[0].IsNewRow))
                {
                    MessageBox.Show("No data available to download.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method if there is no data
                }
                DataTable dataTable = new DataTable();

            // Add columns to the DataTable
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                dataTable.Columns.Add(column.HeaderText);
            }

            // Add rows to the DataTable
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (!row.IsNewRow)
                {
                    DataRow dataRow = dataTable.NewRow();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dataRow[cell.ColumnIndex] = cell.Value ?? DBNull.Value;
                    }
                    dataTable.Rows.Add(dataRow);
                }
            }

            // Create a new Excel workbook and worksheet
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Sheet1");

                // Load the DataTable into the worksheet
                worksheet.Cell(1, 1).InsertTable(dataTable);

                // Prompt the user to select a folder to save the file
                using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
                {
                    folderBrowserDialog.Description = Constants.selecrthefoldertosave;

                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Base file name and directory
                        string baseFileName = ContractIDList.Error_Log_Report;
                        string directoryPath = folderBrowserDialog.SelectedPath;
                        string fileExtension = ".xlsx";

                        // Construct the initial file path
                        string filePath = Path.Combine(directoryPath, baseFileName + fileExtension);

                        // Check if the file already exists, and if so, append a suffix
                        int fileSuffix = 1;
                        while (File.Exists(filePath))
                        {
                            fileSuffix++;
                            filePath = Path.Combine(directoryPath, $"{baseFileName}_{fileSuffix}{fileExtension}");
                        }

                        // Save the workbook to the file path
                        workbook.SaveAs(filePath);
                        MessageBox.Show($"{Constants.datasuccessfullysaved} {Path.GetFileName(filePath)}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnClr_Click(object sender, EventArgs e)//to clear data in the grid
        {
            try{// Reset DateTimePickers to one year back from the current date
            dtpStartDate.Value = DateTime.Now.AddYears(-1);
            dtpStartDate.CustomFormat = "MM-dd-yyyy";
            dtpEndDate.Value = DateTime.Now;
            dtpEndDate.CustomFormat = "MM-dd-yyyy";

            // Clear the DataTable bound to the DataGridView
            if (dataGridView.DataSource is DataTable dt)
            {
                dt.Clear();  // Clears all rows from the DataTable
            }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
    
}
