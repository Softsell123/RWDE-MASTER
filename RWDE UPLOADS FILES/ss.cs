using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace RWDE
{
    public partial class HccReconciliation : Form
    {
        public HccReconciliation()// initialize data
        {
            InitializeComponent();
            dtpStartDate.Value = DateTime.Now.AddYears(-1);
            dtpStartDate.CustomFormat = Constants.DateFormatMMddyyyy;
            dtpEndDate.CustomFormat = Constants.DateFormatMMddyyyy;
            dtpEndDate.Value = DateTime.Now;
            ControlBox = false;
            WindowState = FormWindowState.Maximized;
            RegisterEvents(this); // Assigning events to all Controls
        }
        private void Control_MouseHover(object sender, EventArgs e)// Changing Cursor as Hand on hover
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
        private void Control_MouseLeave(object sender, EventArgs e)// Changing back default Cursor on Leave
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
        private void RegisterEvents(Control parent)// Assigning events to all Controls
        {
            try
            {
                foreach (Control control in parent.Controls)
            {
                if (control is Button || control is CheckBox || control is DateTimePicker || control is ComboBox || control is ScrollBar)
                {
                    control.MouseHover += Control_MouseHover;
                    control.MouseLeave += Control_MouseLeave;
                }

                // Check for child controls in containers
                if (control.HasChildren)
                {
                    // Assigning events to all child Controls
                    RegisterEvents(control);
                }
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Define methods to fetch data from the database
        private void btnDownload_Click(object sender, EventArgs e)// to export the report to selected folder
        {
            try {
                if (dataGridView.Rows.Count == 0 || (dataGridView.Rows.Count == 1 && dataGridView.Rows[0].IsNewRow))
                {
                    MessageBox.Show(Constants.NoDataAvailableToDownload, Constants.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    var worksheet = workbook.Worksheets.Add(Constants.Sheet1);

                    // Load the DataTable into the worksheet
                    worksheet.Cell(1, 1).InsertTable(dataTable);

                    // Prompt the user to select a folder to save the file
                    using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
                    {
                        folderBrowserDialog.Description = Constants.Selectthefoldertosave;

                        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                        {
                            // Base file name and directory
                            string baseFileName = Constants.HccReconciliation;
                            string directoryPath = folderBrowserDialog.SelectedPath;
                            string fileExtension = Constants.XlsxExtention;

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
                            MessageBox.Show($@"{Constants.Datasuccessfullysaved} {Path.GetFileName(filePath)}",Constants.HccReconciliation, MessageBoxButtons.OK, MessageBoxIcon.Information);                   
                        }
                    }
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)// to close the form
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
        private void btnReport_Click(object sender, EventArgs e)// to get the filtered data in the grid
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
                    MessageBox.Show(Constants.StartdatemustbeearlierthanEnddate);
                    return;
                }

                // Create instance of DBHelper
                DbHelper dbHelper = new DbHelper();
                dataGridView.ForeColor = Color.Black;
                string filterType = string.Empty;
                int[] batchids = null;

                if ((!string.IsNullOrWhiteSpace(txtbatchs.Text) && int.TryParse(txtbatchs.Text, out int batchid))||(!string.IsNullOrWhiteSpace(txtbatchs.Text) && txtbatchs.Text.Contains(","))||(!string.IsNullOrWhiteSpace(txtbatchs.Text)))
                {
                    filterType = Constants.BatchId;
                    batchids= txtbatchs.Text.Split(',').Select(int.Parse).Distinct().ToArray();
                }
                else if (dtpDateFilter.SelectedItem != null)
                {
                    switch (dtpDateFilter.SelectedItem.ToString())
                    {
                        case Constants.ServiceDateSp:
                            filterType = Constants.ServiceDate;
                            break;
                        case Constants.CreatedDatesp:
                            filterType = Constants.CreatedDate;
                            break;
                        default:
                            MessageBox.Show(Constants.PleaseSelectAValidFilterTypeFromTheDropdown, Constants.FilterSelectionRequired);
                            return;
                    }
                }
                else
                {
                    MessageBox.Show(Constants.PleaseEnterAValidBatchIdOrSelectAFilterType, Constants.InputError);
                    return;
                }
                DataTable result = null;
                try
                {
                    if (filterType == Constants.BatchId)
                    {
                        List<DataTable> allidtables;
                        // to load the HCCRecon for Batch ID filter
                        allidtables = dbHelper.LoadDatafilterhccreconBatchid(startDate, endDate, batchids, filterType);
                        if (dbHelper.ErrorOccurred)
                        {
                            MessageBox.Show(Constants.ErrorOccurred);
                            return;
                        }

                        // to combine all the data for different BatchID
                        result = dbHelper.CombineAllResults(allidtables);
                        
                    }
                    else
                    {
                        // to load the HCCRecon for created and service date filter
                        result = dbHelper.LoadDatafilterhccrecon(startDate, endDate, filterType);
                        if (dbHelper.ErrorOccurred)
                        {
                            MessageBox.Show(Constants.ErrorOccurred);
                            return;
                        }

                    }

                    // Bind data to the DataGridView
                    dataGridView.AutoGenerateColumns = true;
                    dataGridView.DataSource = result;
                }
                catch (Exception ex)
                {
                    // Handle exceptions related to DateTimePicker values or other issues
                    MessageBox.Show($@"{Constants.AnErrorOccurred}{ex.Message}");
                }
                if(filterType == Constants.BatchId)
                {
                    return;
                }
                else if (result != null && result.Rows.Count < 1) 
                {
                    MessageBox.Show(Constants.Nodatafoundbetweenselecteddates);
                    return;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions related to DateTimePicker values or other issues
                MessageBox.Show($@"{Constants.AnErrorOccurred}{ex.Message}");
            }
        }
        private void btnClr_Click(object sender, EventArgs e)// clear the data in the grid
        {
            try { 
                // Reset DateTimePickers to one year back from the current date
                dtpStartDate.Value = DateTime.Now.AddYears(-1);
                dtpStartDate.CustomFormat = Constants.DateFormatMMddyyyy;
                dtpEndDate.Value = DateTime.Now;
                dtpEndDate.CustomFormat = Constants.DateFormatMMddyyyy;
                dtpDateFilter.Text = Constants.CreatedDate;
                txtbatchs.Text = null;

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
