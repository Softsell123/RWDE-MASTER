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
        private readonly DbHelper dbHelper;
        public HccReconciliation()//initialize data
        {
            InitializeComponent();
            dbHelper = new DbHelper();
            dtpStartDate.Value = DateTime.Now.AddYears(-1);
            dtpStartDate.CustomFormat = "MM-dd-yyyy";
            dtpEndDate.CustomFormat = "MM-dd-yyyy";
            dtpEndDate.Value = DateTime.Now;
            this.ControlBox = false;
            this.WindowState = FormWindowState.Maximized;
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
                if (control is System.Windows.Forms.Button || control is CheckBox || control is DateTimePicker || control is ComboBox || control is ScrollBar)
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
                        folderBrowserDialog.Description = Constants.Selecrthefoldertosave;

                        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                        {
                            // Base file name and directory
                            string baseFileName = Constants.HccReconciliation;
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
                            MessageBox.Show($"{Constants.Datasuccessfullysaved} {Path.GetFileName(filePath)}",Constants.HccReconciliation, MessageBoxButtons.OK, MessageBoxIcon.Information);                   
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
                    filterType = "BatchID";
                    batchids= txtbatchs.Text.Split(',').Select(int.Parse).Distinct().ToArray();
                }
                else if (dtpDateFilter.SelectedItem != null)
                {
                    switch (dtpDateFilter.SelectedItem.ToString())
                    {
                        case Constants.Servicedate:
                            filterType = "ServiceDate";
                            break;
                        case Constants.CreatedDate:
                            filterType = "CreatedDate";
                            break;
                        default:
                            MessageBox.Show("Please select a valid filter type from the dropdown.", "Filter Selection Required");
                            return;
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid Batch ID or select a filter type.", "Input Error");
                    return;
                }
                DataTable result = null;
                try
                {
                    if (filterType == "BatchID")
                    {
                        List<DataTable> allidtables;
                        allidtables = dbHelper.LoadDatafilterhccreconBatchid(startDate, endDate, batchids, filterType);
                        result = dbHelper.CombineAllResults(allidtables);
                    }
                    else
                    {
                        result = dbHelper.LoadDatafilterhccrecon(startDate, endDate, filterType);
                    }

                    // Bind data to the DataGridView
                    dataGridView.AutoGenerateColumns = true;
                    dataGridView.DataSource = result;
                }
                catch (Exception ex)
                {
                    // Handle exceptions related to DateTimePicker values or other issues
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
                if(filterType == "BatchID")
                {
                    return;
                }
                else if (result.Rows.Count < 1) 
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
