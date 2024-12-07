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
    public partial class frmManualUpload : Form
    {
        public frmManualUpload()//initialize data
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            dtpStartDate.Value = DateTime.Now.AddMonths(-1);
            dtpStartDate.CustomFormat = ManualUploadConstants.DateCustomFormat;
            dtpEndDate.CustomFormat = ManualUploadConstants.DateCustomFormat;
            this.ControlBox = false;
            this.WindowState = FormWindowState.Maximized;
            dtpEndDate.Value = DateTime.Now;
            RegisterEvents(this);
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
        private void btnReport_Click(object sender, EventArgs e)//to get filtered data in the grid
        {
            try
            {
                DBHelper dbHelper = new DBHelper();
                dataGridView.AutoGenerateColumns = true;
                dataGridView.Columns.Clear();
                // Ensure the date pickers are properly set
                DateTime startDate = dtpStartDate.Value;
                DateTime endDate = dtpEndDate.Value;
                if (startDate >=endDate)
                {
                    MessageBox.Show($"{Constants.StartdatemustbeearlierthanEnddate}");
                }
                // Call the LoadData method to fetch the data
                dataGridView.ForeColor = Color.Black;
                DataTable result = dbHelper.LoadManualUploadReport(startDate, endDate);//to get data in the grid
                // Now you can use the result, e.g., bind it to a DataGridView or process it
                if (result.Rows.Count == 0)
                {
                    MessageBox.Show(ManualUploadConstants.NoManualUploadsbetweenselecteddates, ManualUploadConstants.Information, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                dataGridView.DataSource = result;
            }
            catch (Exception ex)
            {
                // Handle exceptions, such as logging the error
                MessageBox.Show(ex.Message);
            }
        }
        private void Clear_Click(object sender, EventArgs e)//to clear data in the grid
        {
            try 
            { 
                dtpStartDate.Value = DateTime.Now.AddMonths(-1);
                dtpStartDate.CustomFormat = ManualUploadConstants.DateCustomFormat;
                dtpEndDate.Value = DateTime.Now;
                dtpEndDate.CustomFormat = ManualUploadConstants.DateCustomFormat;

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
        private void btnDownload_Click(object sender, EventArgs e)//to export the report to selected folder
        {
            try
            {
                if (dataGridView.Rows.Count == 0 || (dataGridView.Rows.Count == 1 && dataGridView.Rows[0].IsNewRow))
                {
                    MessageBox.Show(ManualUploadConstants.Nodataavailabletodownload, ManualUploadConstants.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    var worksheet = workbook.Worksheets.Add(ManualUploadConstants.Sheet1);

                    // Load the DataTable into the worksheet
                    worksheet.Cell(1, 1).InsertTable(dataTable);

                    // Prompt the user to select a folder to save the file
                    using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
                    {
                        folderBrowserDialog.Description = Constants.selecrthefoldertosave;

                        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                        {
                            // Base file name and directory
                            string baseFileName = ManualUploadConstants.ManualUploadClientsReport;
                            string directoryPath = folderBrowserDialog.SelectedPath;
                            string fileExtension = ManualUploadConstants.xlsx_extention;

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
                            MessageBox.Show($"{Constants.datasuccessfullysaved} {Path.GetFileName(filePath)}", ManualUploadConstants.ManualUploadClientsReport, MessageBoxButtons.OK,MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                if (control is System.Windows.Forms.Button || control is CheckBox || control is DateTimePicker )
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
    }
}