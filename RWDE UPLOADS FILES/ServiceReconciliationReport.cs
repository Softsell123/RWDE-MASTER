using ClosedXML.Excel;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace RWDE
{
    public sealed partial class ServiceReconciliationReport : Form
    {
        private readonly DbHelper dbHelper = new DbHelper();
        public ServiceReconciliationReport()//initialization of data
        {
            InitializeComponent();
            DateTime startTime = DateTime.MinValue; // Or a specific default date
            DateTime endTime = DateTime.MaxValue; // Or a specific default date
                                                  // Fetch data from the database
            DataTable clientIds = dbHelper.GetClientIDs(startTime, endTime);//to load data of clients
            DataTable serviceIds = dbHelper.GetServiceIDs(startTime, endTime);//to load data of services
            DataTable hccServices = dbHelper.GetHccServices();//to load data of services
            DataTable hccClients = dbHelper.GetHccClients();// Similarly get and populate hccClients
            dtpStartDate.Value = DateTime.Now.AddYears(-1);
            dtpStartDate.CustomFormat = Constants.DateFormatMMddyyyy;
            dtpEndDate.Value = DateTime.Now;
            dtpEndDate.CustomFormat = Constants.DateFormatMMddyyyy;
            ControlBox = false;
            DoubleBuffered = true;
            MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;
            WindowState = FormWindowState.Maximized;
            txtBatchID.Text = "";
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
                    if (control is Button || control is CheckBox || control is DateTimePicker || control is ComboBox || control is ScrollBar)
                    {
                        control.MouseHover += Control_MouseHover;
                        control.MouseLeave += Control_MouseLeave;
                    }

                    // Check for child controls in containers
                    if (control.HasChildren)
                    {
                        //Assigning events to all child Controls
                        RegisterEvents(control);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DisplayHeadersOnly()//to display the empty GridView
        {
            try
            {
                dataGridView.DataSource = null;
                dataGridView.Columns.Clear();
                dataGridView.Columns.Add(Constants.SlNo, Constants.SlNo);
                dataGridView.Columns.Add(Constants.BatchId, Constants.BatchIdHeader);
                dataGridView.Columns.Add(Constants.Staff, Constants.Staff);
                dataGridView.Columns.Add(Constants.HccId, Constants.HccIdsp);
                dataGridView.Columns.Add(Constants.HccConsentExpiryDate, Constants.HccConsentExpiryDatesp);
                dataGridView.Columns.Add(Constants.RwEligibilityExpiryDate, Constants.RwEligibilityExpiryDatesp);
                dataGridView.Columns.Add(Constants.Service, Constants.Service);
                dataGridView.Columns.Add(Constants.ServiceCodeId, Constants.ServiceCodeIdsp);
                dataGridView.Columns.Add(Constants.HccContractId, Constants.HccContractIdSp);
                dataGridView.Columns.Add(Constants.UnitsOfServices, Constants.UnitsOfServicesSp);
                dataGridView.Columns.Add(Constants.ActualMinutesSpent, Constants.ActualMinutesSpentSp);
                dataGridView.Columns.Add(Constants.ServiceId, Constants.ServiceIdSp);
                dataGridView.Columns.Add(Constants.ServiceExportedToHcc, Constants.ServiceExportedToHccSp);
                dataGridView.Columns.Add(Constants.ServiceDate, Constants.ServiceDateSp);
                dataGridView.Columns.Add(Constants.EntryDate, Constants.EntryDateSp);
                dataGridView.Columns.Add(Constants.Lag, Constants.Lag);
                dataGridView.Columns.Add(Constants.LagStatus, Constants.LagStatusSp);
                dataGridView.Columns.Add(Constants.HccExportFailureReason, Constants.HccExportFailureReasonSp);
                dataGridView.AutoGenerateColumns = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnReport_Click(object sender, EventArgs e)//to display the data as per the given inputs
        {
            try
            {
                // Ensure the date pickers are properly set
                DateTime startDate = dtpStartDate.Value;
                DateTime endDate = dtpEndDate.Value;

                // Validate that the end date is greater than the start date
                if (endDate <= startDate)
                {
                    MessageBox.Show(Constants.StartdatemustbeearlierthanEnddate);
                    return;
                }

                // Create instance of DBHelper
                //DbHelper dbHelper = new DbHelper();
                dataGridView.Columns.Clear();
                dataGridView.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView_CellFormatting);

                // Determine filter type
                string filterType = string.Empty;
                if (!string.IsNullOrWhiteSpace(txtBatchID.Text) && int.TryParse(txtBatchID.Text, out int batchid) || (!string.IsNullOrWhiteSpace(txtBatchID.Text) && txtBatchID.Text.Contains(",")) || (!string.IsNullOrWhiteSpace(txtBatchID.Text)))
                {
                    filterType = Constants.BatchId;
                }
                else if (dtpDateFilter.SelectedItem != null)
                {
                    switch (dtpDateFilter.SelectedItem.ToString())
                    {
                        case Constants.Servicedate:
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
                    //to display the empty GridView
                    DisplayHeadersOnly();
                    return;
                }

                // Fetch data based on selected filter type
                DataTable result = null;
                int[] batchids = null;
                if (filterType == Constants.BatchId)
                {
                    // Get and validate batch IDs
                    string batchIdText = txtBatchID.Text;
                    batchids = txtBatchID.Text.Split(',').Select(int.Parse).Distinct().ToArray();
                    if (batchids.Length > 0)
                    {
                        //to display the data based on BatchId
                        result = dbHelper.LoadDatafilterServiceReconbatchid(batchids); //

                        if (result.Rows.Count == 0)
                        {
                            //to display the empty GridView
                            DisplayHeadersOnly();
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show(Constants.PleaseEnterValidBatchIds, Constants.InvalidInput);
                        //to display the empty GridView
                        DisplayHeadersOnly();
                        return;
                    }
                }
                else
                {
                    //to load the service-recon for created and service date filter
                    result = dbHelper.LoadDatafilterServiceRecon(startDate, endDate, filterType);
                }

                // Add a 'Sl No' column for serial number
                DataGridViewTextBoxColumn slNoColumn = new DataGridViewTextBoxColumn();

                // Insert at the first position

                // Add serial numbers (Sl No) to the DataTable before binding
                int serialNumber = 1;
                result.Columns[Constants.SlNo].ReadOnly = false;
                foreach (DataRow row in result.Rows)
                {
                    row[Constants.SlNo] = serialNumber++; // Assign the serial number
                }

                // Bind the data to the DataGridView
                dataGridView.AutoGenerateColumns = true;
                dataGridView.DataSource = result;
            }
            catch (Exception ex)
            {
                //to display the empty GridView
                DisplayHeadersOnly();
                MessageBox.Show($@"{Constants.AnErrorOccurred}{ex.Message}");
            }
        }
        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)//to set all Cell's text Color as Black
        {
            try
            {
                // Check if the cell value is not null
                if (e.Value != null)
                {
                    // Example condition to change color
                    e.CellStyle.ForeColor = e.CellStyle.ForeColor == Color.Black ? Color.Blue : Color.Black; // Default color for other cases
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnDownload_Click(object sender, EventArgs e) // to export data to selected folder
        {
            try
            {
                // Check if there are any rows in the DataGridView
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
                        folderBrowserDialog.Description = Constants.Selecrthefoldertosave;

                        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                        {
                            // Base file name and directory
                            string baseFileName = Constants.ServiceReconciliationReportFilename;
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
                            MessageBox.Show($@"{Constants.Datasuccessfullysaved} {Path.GetFileName(filePath)}", Constants.Success, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)//to close the form
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
        private void btnClr_Click(object sender, EventArgs e)//to clear data and filter date to default one
        {
            try
            {
                // Reset DateTimePickers to one year back from the current date
                dtpStartDate.Value = DateTime.Now.AddYears(-1);
                dtpStartDate.CustomFormat = Constants.DateFormatMMddyyyy;
                dtpEndDate.Value = DateTime.Now;
                dtpEndDate.CustomFormat = Constants.DateFormatMMddyyyy;
                dtpDateFilter.Text = Constants.CreatedDate;
                txtBatchID.Text = null;
                dtpDateFilter.Text = null;
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