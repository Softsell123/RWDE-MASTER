using ClosedXML.Excel;
using OfficeOpenXml;
using Rwde;
using RWDE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RWDE_UPLOADS_FILES
{
    public partial class HCC_Reconciliation : Form
    {
        private DBHelper DBHelper;
        private DataTable dataTable;
        private readonly DBHelper dbHelper = new DBHelper();
        public HCC_Reconciliation()//initialize data
        {
            InitializeComponent();
            dtpStartDate.Value = DateTime.Now.AddYears(-1);
            dtpStartDate.CustomFormat = "MM-dd-yyyy";
            dtpEndDate.CustomFormat = "MM-dd-yyyy";
            dtpEndDate.Value = DateTime.Now;
            this.ControlBox = false;
            this.WindowState = FormWindowState.Maximized;
        }
        public void PopulateDataGridView()//populate data
        {
            try {
                DataTable dt = new DataTable();

                // Add columns to the DataTable
                dt.Columns.Add("Month", typeof(string));
                dt.Columns.Add("TotalServiceEntries", typeof(int));
                dt.Columns.Add("ServiceEntriesNotMappedToHCC", typeof(int));
                dt.Columns.Add("ServiceEntriesSuccessfullyExportedToHCC", typeof(int));
                dt.Columns.Add("ServiceEntriesNotExportedToHCC", typeof(int));
                dt.Columns.Add("ServiceEntriesForMHServicesOnlyClients", typeof(int));
                dt.Columns.Add("ServiceEntriesPostTimeboxPeriod", typeof(int));
                dt.Columns.Add("ServiceEntriesForExpiredMissingHCCConsent", typeof(int));
                dt.Columns.Add("ServiceEntriesForHCCIDMissing", typeof(int));
                dt.Columns.Add("ServiceEntriesNotEnrolledInProgram", typeof(int));
                dt.Columns.Add("ServiceEntriesForPreRegClients", typeof(int));
                dt.Columns.Add("ServiceEntriesForRWEligibilityExpired", typeof(int));
                dt.Columns.Add("ServiceEntriesForMissingHCCStaffLogin", typeof(int));
                dt.Columns.Add("ServiceEntriesWithZeroUnitOfService", typeof(int));
                dt.Columns.Add("ServiceEntriesForWaiver", typeof(int));
                dt.Columns.Add("ServiceEntriesFor3DayDelayInHCCUpload", typeof(int));
                dt.Columns.Add("ServiceEntriesForITDrops", typeof(int));
                dt.Columns.Add("%Drop", typeof(decimal));  // Percentage fields are often decimal

                // Fetch data from the database
                DataRow dr = dt.NewRow();
                dr["Month"] = DateTime.Now.ToString("MMMM");

                dr["TotalServiceEntries"] = dbHelper.GetTotalServiceEntries();
                dr["ServiceEntriesNotMappedToHCC"] = dbHelper.GetServiceEntriesNotMappedToHCC();
                dr["ServiceEntriesSuccessfullyExportedToHCC"] = dbHelper.GetServiceEntriesSuccessfullyExportedToHCC();
                dr["ServiceEntriesNotExportedToHCC"] = dbHelper.GetServiceEntriesNotExportedToHCC();
                dr["ServiceEntriesForMHServicesOnlyClients"] = dbHelper.GetServiceEntriesForMHServicesOnlyClients();
                // dr["ServiceEntriesPostTimeboxPeriod"] = dbHelper.GetServiceEntriesPostTimeboxPeriod();
                //dr["ServiceEntriesForExpiredMissingHCCConsent"] = dbHelper.GetServiceEntriesForExpiredMissingHCCConsent();
                dr["ServiceEntriesForHCCIDMissing"] = dbHelper.GetServiceEntriesForHCCIDMissing();
                dr["ServiceEntriesNotEnrolledInProgram"] = dbHelper.GetServiceEntriesNotEnrolledInProgram();
                //dr["ServiceEntriesForPreRegClients"] = dbHelper.GetServiceEntriesForPreRegClients();
                dr["ServiceEntriesForRWEligibilityExpired"] = dbHelper.GetServiceEntriesForRWEligibilityExpired();
                dr["ServiceEntriesForMissingHCCStaffLogin"] = dbHelper.GetServiceEntriesForMissingHCCStaffLogin();
                // dr["ServiceEntriesWithZeroUnitOfService"] = dbHelper.GetServiceEntriesWithZeroUnitOfService();
                dr["ServiceEntriesForWaiver"] = dbHelper.GetServiceEntriesForWaiver();
                //dr["ServiceEntriesFor//3DayDelayInHCCUpload"] = dbHelper.GetServiceEntriesFor3DayDelayInHCCUpload();
                dr["ServiceEntriesForITDrops"] = dbHelper.GetServiceEntriesForITDrops();

                // Calculate % Drop
                int expiredMissingHCCConsent = dbHelper.GetServiceEntriesForExpiredMissingHCCConsent();
                int totalNotMappedToHCC = dbHelper.GetTotalServiceEntries() - dbHelper.GetServiceEntriesNotMappedToHCC();
                dr["%Drop"] = totalNotMappedToHCC == 0 ? 0 : (decimal)expiredMissingHCCConsent / totalNotMappedToHCC;

                dt.Rows.Add(dr);

                // Clear existing columns and rows


                dataGridView.DataSource = dt;

                // Set additional properties like row height, style, etc.
                this.dataGridView.RowTemplate.Height = 40;
                this.dataGridView.ForeColor = Color.Black;
                this.dataGridView.DefaultCellStyle.ForeColor = Color.Black;
                this.dataGridView.DefaultCellStyle.Font = new Font("Calibre", 14, FontStyle.Regular);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Define methods to fetch data from the database
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
                        string baseFileName = Constants.HCC_Reconciliation;
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
                        MessageBox.Show($"{Constants.datasuccessfullysaved} {Path.GetFileName(filePath)}",Constants.HCC_Reconciliation, MessageBoxButtons.OK, MessageBoxIcon.Information);                   
                }
            }
            }
        } catch (Exception ex)
 {
     MessageBox.Show(ex.Message);
 }
}
      private void btnClose_Click(object sender, EventArgs e)//to close the form
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
        private void btnReport_Click(object sender, EventArgs e)//to get the filtered data in the grid
        {
            try
            {
                // Ensure the date pickers are properly set
                DateTime startDate = dtpStartDate.Value;
                DateTime endDate = dtpEndDate.Value;
                dataGridView.Columns.Clear();
                // Validate that the end date is greater than the start date
                if (endDate <= startDate)
                {
                    MessageBox.Show(Constants.StartdatemustbelessthanEnddate);
                    return;
                }

                // Create instance of DBHelper
                DBHelper dbHelper = new DBHelper();
                dataGridView.ForeColor = Color.Black;

                DataTable result = dbHelper.LoadDatafilterhccrecon(startDate, endDate);//to get filtered data 

                // Now you can use the result, e.g., bind it to a DataGridView or process it
                dataGridView.AutoGenerateColumns = true;
                dataGridView.DataSource = result;
                if (result.Rows.Count < 1) 
                {
                    MessageBox.Show(Constants.Nodatafoundbetweenselecteddates);
                    return;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions related to DateTimePicker values or other issues
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
        private void btnClr_Click(object sender, EventArgs e)//clear the data in the grid
        {
            try { 

            // Reset DateTimePickers to one year back from the current date
            dtpStartDate.Value = DateTime.Now.AddYears(-1);
            dtpStartDate.CustomFormat = "MM-dd-yyyy";
            dtpEndDate.Value = DateTime.Now;
            dtpEndDate.CustomFormat = "MM-dd-yyyy";
            dtpDateFilter.Text = Constants.CreatedDate;

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

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    }
