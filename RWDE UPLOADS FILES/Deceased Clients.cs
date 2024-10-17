using RWDE;
using System;
using System.Collections.Generic;
using ClosedXML.Excel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Bibliography;
using System.Text.RegularExpressions;
using Rwde;

namespace RWDE_UPLOADS_FILES
{
    public partial class Deceased_Clients : Form
    {
        private readonly string connectionString;
        private readonly DBHelper dbHelper;

        public Deceased_Clients()//initialize data
        {
            dbHelper = new DBHelper(); // Initialize the dbHelper object
            connectionString = dbHelper.GetConnectionString(); // Now this will work because dbHelper is initialized
            InitializeComponent();
            this.ControlBox = false;
             this.WindowState = FormWindowState.Maximized;
            //PopulateDataGridView();
            dtpStartDate.Value = DateTime.Now.AddYears(-1);
            dtpStartDate.CustomFormat = "MM-dd-yyyy";
            dtpEndDate.CustomFormat = "MM-dd-yyyy";
           // Assuming you have another DateTimePicker for the End Date
            dtpEndDate.Value = DateTime.Now;
        }
        public void PopulateDataGridView()//populate data
        {
            try { 
            // Clear existing columns
            dataGridView.AutoGenerateColumns = false;
            this.dataGridView.Columns.Clear();
            // Add columns to the DataGridView
            this.dataGridView.Columns.Add("HCCID", "HCC ID");
            this.dataGridView.Columns.Add("ClientName", "Client Name");
            this.dataGridView.Columns.Add("Status", "Status");
            this.dataGridView.Columns.Add("DateOfDeath", "Date of Death");
            this.dataGridView.Columns.Add("LastServiceDate", "Last Service Date");     
            this.dataGridView.Columns.Add("DownloadDate", "Download Date");
            this.dataGridView.Columns.Add("Extracted", "Extracted Y/N");
            this.dataGridView.Columns.Add("ExtractionDate", "Extraction Date");
            this.dataGridView.Columns.Add("CMSMatch", "CMS Match");
            this.dataGridView.Columns.Add("CMSMatchDate", "CMS Match Date");
            this.dataGridView.Columns.Add("ServiceCountAfterDeath", "ServiceCountAfterDeath");
            this.dataGridView.Columns.Add("CreatedOn", "Created On");
            // Set column widths (adjust as needed)
            this.dataGridView.Columns["HCCID"].Width = 100;
            this.dataGridView.Columns["ClientName"].Width = 200;
            this.dataGridView.Columns["Status"].Width = 120;
            this.dataGridView.Columns["DateOfDeath"].Width = 120;
            this.dataGridView.Columns["LastServiceDate"].Width = 120;         
            this.dataGridView.Columns["DownloadDate"].Width = 120;
            this.dataGridView.Columns["Extracted"].Width = 100;
            this.dataGridView.Columns["ExtractionDate"].Width = 120;
            this.dataGridView.Columns["CMSMatch"].Width = 100;
            this.dataGridView.Columns["CMSMatchDate"].Width = 120;
            this.dataGridView.Columns["ServiceCountAfterDeath"].Width = 120;

            this.dataGridView.Columns["CreatedOn"].Width = 120;
           
            // Set row height
            this.dataGridView.RowTemplate.Height = 40;

            // Set default cell style
            this.dataGridView.ForeColor = Color.Black;
            this.dataGridView.DefaultCellStyle.ForeColor = Color.Black; // Text color
            this.dataGridView.DefaultCellStyle.Font = new Font("Calibre", 14, FontStyle.Regular); // Font size 14 and regular
            DataTable dataTable = dbHelper.GetDataFromDatabase();
            // Set header style
            foreach (DataGridViewColumn column in this.dataGridView.Columns)
            {
                column.HeaderCell.Style.ForeColor = Color.Black; // Set header text color to black
            }

            // Clear existing rows
            this.dataGridView.Rows.Clear();

            // Populate DataGridView with data
            foreach (DataRow row in dataTable.Rows)
            {
                this.dataGridView.Rows.Add(
                    row["HCC ID"],
                    row["Client Name"],
                    row["Status"],
                    row["Date of Death"],
                    row["Last Service Date"],
                    row["Download Date"],
                    row["Extracted Y/N"],
                    row["Extraction Date"],
                    row["CMS Match"],
                    row["CMS Match Date"],
                    row["ServiceCountAfterDeath"],
                    row["Created On"]
                  
                );
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void PopulateDataGridView(DataTable dataTable)//populate data
        {
          try{  // Clear existing columns
            dataGridView.AutoGenerateColumns = false;
            this.dataGridView.Columns.Clear();

            // Add columns to the DataGridView
            this.dataGridView.Columns.Add("HCCID", "HCC ID");
            this.dataGridView.Columns.Add("ClientName", "Client Name");
            this.dataGridView.Columns.Add("Status", "Status");
            this.dataGridView.Columns.Add("DateOfDeath", "Date of Death");
            this.dataGridView.Columns.Add("LastServiceDate", "Last Service Date");
            this.dataGridView.Columns.Add("DownloadDate", "Download Date");
            this.dataGridView.Columns.Add("Extracted", "Extracted Y/N");
            this.dataGridView.Columns.Add("ExtractionDate", "Extraction Date");
            this.dataGridView.Columns.Add("CMSMatch", "CMS Match");
            this.dataGridView.Columns.Add("CMSMatchDate", "CMS Match Date");
            this.dataGridView.Columns.Add("Service Count After Death", "Service Count After Death");

            this.dataGridView.Columns.Add("CreatedOn", "Created On");
          
            // Set column widths (adjust as needed)
            this.dataGridView.Columns["HCCID"].Width = 100;
            this.dataGridView.Columns["ClientName"].Width = 200;
            this.dataGridView.Columns["Status"].Width = 120;
            this.dataGridView.Columns["DateOfDeath"].Width = 120;
            this.dataGridView.Columns["LastServiceDate"].Width = 120;
            this.dataGridView.Columns["DownloadDate"].Width = 120;
            this.dataGridView.Columns["Extracted"].Width = 100;
            this.dataGridView.Columns["ExtractionDate"].Width = 120;
            this.dataGridView.Columns["CMSMatch"].Width = 100;
            this.dataGridView.Columns["CMSMatchDate"].Width = 120;
            this.dataGridView.Columns["Service Count After Death"].Width = 120;

            this.dataGridView.Columns["CreatedOn"].Width = 120;
         
            // Set row height
            this.dataGridView.RowTemplate.Height = 40;

            // Set default cell style
            this.dataGridView.ForeColor = Color.Black;
            this.dataGridView.DefaultCellStyle.ForeColor = Color.Black; // Text color
            this.dataGridView.DefaultCellStyle.Font = new Font("Calibre", 14, FontStyle.Regular); // Font size 14 and regular

            // Set header style
            foreach (DataGridViewColumn column in this.dataGridView.Columns)
            {
                column.HeaderCell.Style.ForeColor = Color.Black; // Set header text color to black
            }

            // Clear existing rows
            this.dataGridView.Rows.Clear();

            // Populate DataGridView with data
            foreach (DataRow row in dataTable.Rows)
            {
                this.dataGridView.Rows.Add(
                    row["HCC ID"],
                    row["Client Name"],
                    row["Status"],
                    row["Date of Death"],
                    row["Last Service Date"],
                    row["Download Date"],
                    row["Extracted Y/N"],
                    row["Extraction Date"],
                    row["CMS Match"],
                    row["CMS Match Date"],                 
                     row["Service Count After Death"],
                      row["Created On"]
                );
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)//to close the form
        {
            try{// Close the current form (dispose it)
            this.Close();
            Application.Restart();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnDownload_Click(object sender, EventArgs e)//to show the filtered data in the grid
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
                    folderBrowserDialog.Description =Constants.selecrthefoldertosave;

                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Base file name and directory
                        string baseFileName = Constants.Deceased_Clients;
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
                        MessageBox.Show($"{Constants.datasuccessfullysaved}{Path.GetFileName(filePath)}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReport_Click(object sender, EventArgs e)//to show data in the grid
        {
            try { 
            // Assuming you have DateTimePicker controls named dtpStartDate and dtpEndDate
            DateTime startDate = dtpStartDate.Value.Date;
            DateTime endDate = dtpEndDate.Value.Date;
            if (startDate > endDate)
            {
                // Show an error message if the start date is later than the end date
                MessageBox.Show(Constants.StartdatemustbelessthanEnddate, "Deceased Clients", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Exit the method to prevent further processing
            }

            // Fetch filtered data from the database
            DataTable dataTable = dbHelper.GetFilteredDataFromDatabase(startDate, endDate);

            // Populate DataGridView with the fetched data
            PopulateDataGridView(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnClr_Click(object sender, EventArgs e)//to clear data
        {
            try { 
            dtpStartDate.Value = DateTime.Now.AddYears(-1);
            dtpStartDate.CustomFormat = "MM-dd-yyyy";
            dtpEndDate.CustomFormat = "MM-dd-yyyy";
            dtpEndDate.Value = DateTime.Now;
            // Clear only the rows in the DataGridView
            dataGridView.Rows.Clear();

            // Optionally, reset the DataGridView's current selection or focus
            dataGridView.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
       