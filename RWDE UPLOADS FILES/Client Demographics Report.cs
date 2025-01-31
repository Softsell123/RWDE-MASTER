using System;
using ClosedXML.Excel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace RWDE
{
    public partial class ClientDemographicsReport : Form
    {
        public ClientDemographicsReport()//initialize data
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            dtpStartDate.Value = DateTime.Now.AddYears(-1);
            dtpStartDate.CustomFormat = Constants.DateFormatMMddyyyy;
            dtpEndDate.CustomFormat = Constants.DateFormatMMddyyyy;
            ControlBox = false;
            WindowState = FormWindowState.Maximized;
            dtpEndDate.Value = DateTime.Now;
            RegisterEvents(this); //Assigning events to all Controls
        }
        private void Control_MouseHover(object sender, EventArgs e)//Changing Cursor as Hand on hover
        {
            Cursor = Cursors.Hand;
        }
        private void Control_MouseLeave(object sender, EventArgs e)//Changing back default Cursor on Leave
        {
            Cursor = Cursors.Default;
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
                        //Assigning events to Child Controls
                        RegisterEvents(control);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BtnClose_Click(object sender, EventArgs e)//to close the form
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
        private void BtnReport_Click(object sender, EventArgs e)//to get filtered data in the grid
        {
            try
            {
                using (DbHelper dbHelper = new DbHelper()) // Ensures proper disposal
                {
                    dataGridView.AutoGenerateColumns = true;
                    dataGridView.Columns.Clear();
                    // Ensure the date pickers are properly set
                    DateTime startDate = dtpStartDate.Value;
                    DateTime endDate = dtpEndDate.Value;
                    if (startDate >= endDate)
                    {
                        MessageBox.Show($@"{Constants.StartdatemustbeearlierthanEnddate}");
                    }
                    // Call the LoadData method to fetch the data
                    dataGridView.ForeColor = Color.Black;

                    //to get details of clients applied for services
                    DataTable result = dbHelper.LoadConfigurationfilter(startDate, endDate);//to get data in the grid
                    if (dbHelper.ErrorOccurred)
                    {
                        MessageBox.Show(Constants.ErrorOccurred);
                        return;
                    }

                    //to bind the data in DataGridView
                    dataGridView.DataSource = result;
                }
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
                dtpStartDate.Value = DateTime.Now.AddYears(-1);
                dtpStartDate.CustomFormat = Constants.DateFormatMMddyyyy;
                dtpEndDate.Value = DateTime.Now;
                dtpEndDate.CustomFormat = Constants.DateFormatMMddyyyy;

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
        private void BtnDownload_Click(object sender, EventArgs e)//to export the report to selected folder
        {
            try
            {
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
                            string baseFileName = ContractIdList.ClientDemographicsReport;
                            string directoryPath = folderBrowserDialog.SelectedPath;
                            string fileExtension = Constants.XlsxExtention;

                            // Construct the initial file path
                            string filePath = Path.Combine(directoryPath, baseFileName + fileExtension);

                            // Check if the file already exists, and if so, append a suffix
                            int fileSuffix = 1;
                            while ( File.Exists(filePath))
                            {
                                fileSuffix++;
                                filePath = Path.Combine(directoryPath, $"{baseFileName}_{fileSuffix}{fileExtension}");
                            }

                            // Save the workbook to the file path
                            workbook.SaveAs(filePath);
                            MessageBox.Show($@"{Constants.Datasuccessfullysaved} {Path.GetFileName(filePath)}", ContractIdList.ClientDemographicsReport, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}