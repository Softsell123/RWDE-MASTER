using System;
using ClosedXML.Excel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace RWDE
{
    public partial class DeceasedClients : Form
    {
        private readonly DbHelper dbHelper;

        public DeceasedClients() //initialize data
        {
            dbHelper = new DbHelper(); // Initialize the dbHelper object
            InitializeComponent();
            ControlBox = false;
            WindowState = FormWindowState.Maximized;
            dtpStartDate.Value = DateTime.Now.AddYears(-1);
            dtpStartDate.CustomFormat = Constants.DateFormatMMddyyyy;
            dtpEndDate.CustomFormat = Constants.DateFormatMMddyyyy;
            // Assuming you have another DateTimePicker for the End Date
            dtpEndDate.Value = DateTime.Now;
            //Assigning events to all Controls
            RegisterEvents(this); //Assigning events to all Controls
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
                    if (control is Button || control is CheckBox || control is DateTimePicker ||
                        control is ScrollBar)
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

        public void PopulateDataGridView(DataTable dataTable) //populate data
        {
            try
            {
                // Clear existing columns
                dataGridView.AutoGenerateColumns = false;
                dataGridView.Columns.Clear();

                // Add columns to the DataGridView
                dataGridView.Columns.Add(Constants.HccId, Constants.HccIdsp);
                dataGridView.Columns.Add(Constants.ClientName, Constants.ClientNamesp);
                dataGridView.Columns.Add(Constants.Status, Constants.Status);
                dataGridView.Columns.Add(Constants.DateOfDeath, Constants.DateOfDeathsp);
                dataGridView.Columns.Add(Constants.LastServiceDate, Constants.LastServiceDatesp);
                dataGridView.Columns.Add(Constants.DownloadDate, Constants.DownloadDatesp);
                dataGridView.Columns.Add(Constants.Extracted, Constants.Extractedsp);
                dataGridView.Columns.Add(Constants.ExtractionDate, Constants.ExtractionDatesp);
                dataGridView.Columns.Add(Constants.CmsMatch, Constants.CmsMatchsp);
                dataGridView.Columns.Add(Constants.CmsMatchDate, Constants.CmsMatchDatesp);
                dataGridView.Columns.Add(Constants.ServiceCountAfterDeath, Constants.ServiceCountAfterDeath);

                dataGridView.Columns.Add(Constants.CreatedOn, Constants.CreatedOnsp);

                // Set column widths (adjust as needed)
                dataGridView.Columns[Constants.HccId].Width = 100;
                dataGridView.Columns[Constants.ClientName].Width = 200;
                dataGridView.Columns[Constants.Status].Width = 120;
                dataGridView.Columns[Constants.DateOfDeath].Width = 120;
                dataGridView.Columns[Constants.LastServiceDate].Width = 120;
                dataGridView.Columns[Constants.DownloadDate].Width = 120;
                dataGridView.Columns[Constants.Extracted].Width = 100;
                dataGridView.Columns[Constants.ExtractionDate].Width = 120;
                dataGridView.Columns[Constants.CmsMatch].Width = 100;
                dataGridView.Columns[Constants.CmsMatchDate].Width = 120;
                dataGridView.Columns[Constants.ServiceCountAfterDeath].Width = 120;
                dataGridView.Columns[Constants.CreatedOn].Width = 120;

                // Set row height
                dataGridView.RowTemplate.Height = 40;

                // Set default cell style
                dataGridView.ForeColor = Color.Black;
                dataGridView.DefaultCellStyle.ForeColor = Color.Black; // Text color
                dataGridView.DefaultCellStyle.Font =
                    new Font(Constants.FntfmlyCalibre, 14, FontStyle.Regular); // Font size 14 and regular

                // Set header style
                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    column.HeaderCell.Style.ForeColor = Color.Black; // Set header text color to black
                }

                // Clear existing rows
                dataGridView.Rows.Clear();

                // Populate DataGridView with data
                foreach (DataRow row in dataTable.Rows)
                {
                    dataGridView.Rows.Add(
                        row[Constants.HccIdsp],
                        row[Constants.ClientNamesp],
                        row[Constants.Status],
                        row[Constants.DateOfDeathsp],
                        row[Constants.LastServiceDatesp],
                        row[Constants.DownloadDatesp],
                        row[Constants.Extractedsp],
                        row[Constants.ExtractionDatesp],
                        row[Constants.CmsMatchsp],
                        row[Constants.CmsMatchDatesp],
                        row[Constants.ServiceCountAfterDeath],
                        row[Constants.CreatedOnsp]
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e) //to close the form
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

        private void btnDownload_Click(object sender, EventArgs e) //to show the filtered data in the grid
        {
            try
            {
                if (dataGridView.Rows.Count == 0 || (dataGridView.Rows.Count == 1 && dataGridView.Rows[0].IsNewRow))
                {
                    MessageBox.Show(Constants.NoDataAvailableToDownload, Constants.Warning, MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

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
                        folderBrowserDialog.Description = Constants.Selecrthefoldertosave;

                        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                        {
                            // Base file name and directory
                            string baseFileName = Constants.DeceasedClients;
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
                            MessageBox.Show($@"{Constants.Datasuccessfullysaved}{Path.GetFileName(filePath)}", Constants.Success, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReport_Click(object sender, EventArgs e) //to show data in the grid
        {
            try
            {
                // Assuming you have DateTimePicker controls named dtpStartDate and dtpEndDate
                DateTime startDate = dtpStartDate.Value.Date;
                DateTime endDate = dtpEndDate.Value.Date;
                if (startDate > endDate)
                {
                    // Show an error message if the start date is later than the end date
                    MessageBox.Show(Constants.StartdatemustbeearlierthanEnddate, Constants.DeceasedClientsp,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Exit the method to prevent further processing
                }

                // Fetch filtered data from the database
                DataTable dataTable = dbHelper.GetFilteredDataFromDatabase(startDate, endDate);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }

                // Populate DataGridView with the fetched data
                PopulateDataGridView(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClr_Click(object sender, EventArgs e) //to clear data
        {
            try
            {
                dtpStartDate.Value = DateTime.Now.AddYears(-1);
                dtpStartDate.CustomFormat = Constants.DateFormatMMddyyyy;
                dtpEndDate.CustomFormat = Constants.DateFormatMMddyyyy;
                dtpEndDate.Value = DateTime.Now;
                // Clear only the rows in the DataGridView
                dataGridView.Rows.Clear();
                //reset the DataGridView's current selection or focus
                dataGridView.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
       