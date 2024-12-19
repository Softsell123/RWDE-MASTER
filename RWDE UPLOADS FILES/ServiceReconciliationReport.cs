using ClosedXML.Excel;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace RWDE
{
    public partial class ServiceReconciliationReport : Form
    {
        private DBHelper DBHelper;
        private DataTable dataTable;
        private readonly DBHelper dbHelper = new DBHelper();
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
            dtpStartDate.CustomFormat = "MM-dd-yyyy";
            dtpEndDate.Value = DateTime.Now;
            dtpEndDate.CustomFormat = "MM-dd-yyyy";
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;
            txtBatchID.Text = "";
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
        public void PopulateDataGridView(DataTable hccServices, DataTable hccClients)//populate data
        {
            try
            {
                this.dataGridView.RowTemplate.Height = 40;

                // Set default cell style
                this.dataGridView.ForeColor = Color.Black;
                this.dataGridView.DefaultCellStyle.ForeColor = Color.Black; // Text color
                this.dataGridView.DefaultCellStyle.Font = new Font("Calibre", 14, FontStyle.Regular);// Clear existing columns and rows
                dataGridView.Columns.Clear();
                dataGridView.Rows.Clear();

                // Add columns to the DataGridView
                dataGridView.Columns.Add("User", "User");
                dataGridView.Columns["User"].DataPropertyName = "Staff_id";

                dataGridView.Columns.Add("ClientID", "Client ID");
                dataGridView.Columns["ClientID"].DataPropertyName = "Agncy_client_1";

                dataGridView.Columns.Add("HCCID", "HCC ID");
                dataGridView.Columns["HCCID"].DataPropertyName = "Clnt_id";

                dataGridView.Columns.Add("Program", "Program");
                dataGridView.Columns.Add("Classification", "Classification");
                dataGridView.Columns.Add("Status", "Status");
                dataGridView.Columns.Add("HCCConsentExpiryDate", "HCC Consent Expiry Date");
                dataGridView.Columns.Add("RWEligibilityExpiryDate", "RW Eligibility Expiry Date");
                dataGridView.Columns.Add("CaseManager", "Case Manager");
                dataGridView.Columns.Add("ServiceGroup", "Service Group");
                dataGridView.Columns.Add("HCCContractID", "HCC Contract ID");
                dataGridView.Columns["HCCContractID"].DataPropertyName = "Contract_name";
                dataGridView.Columns.Add("UnitsOfServices", "Units of Services");
                dataGridView.Columns["UnitsOfServices"].DataPropertyName = "Quantity_served";
                dataGridView.Columns.Add("ActualMinutesSpent", "Actual Minutes Spent");
                dataGridView.Columns["ActualMinutesSpent"].DataPropertyName = "Actual_minutes_spent";
                dataGridView.Columns.Add("ServiceCodeMappedToHCC", "Service Code Mapped to HCC");
                dataGridView.Columns["ServiceCodeMappedToHCC"].DataPropertyName = "MappedToHCC";
                dataGridView.Columns.Add("ServiceID", "Service ID");
                dataGridView.Columns["ServiceID"].DataPropertyName = "ServiceID";
                dataGridView.Columns.Add("ServiceExportedToHCC", "Service Exported to HCC");
                dataGridView.Columns["ServiceExportedToHCC"].DataPropertyName = "MappedToHCC";
                dataGridView.Columns.Add("ServiceDate", "Service Date");
                dataGridView.Columns["ServiceDate"].DataPropertyName = "Service_date";
                dataGridView.Columns.Add("EntryDate", "Entry Date");
                dataGridView.Columns["EntryDate"].DataPropertyName = "CreatedOn";
                dataGridView.Columns.Add("Lag", "Lag");
                dataGridView.Columns.Add("Grade", "Grade");
                dataGridView.Columns.Add("HCCExportFailureReason", "HCC Export Failure Reason");
                dataGridView.Columns.Add("Status", "Status");
                dataGridView.Columns.Add("ErrorMessage", "ErrorMessage");

                // Set primary key for hccClients if it's not already set
                if (hccClients.PrimaryKey.Length == 0)
                {
                    hccClients.PrimaryKey = new DataColumn[] { hccClients.Columns["Clnt_id"] };
                }

                // Populate rows
                foreach (DataRow serviceRow in hccServices.Rows)
                {
                    DataRow clientRow = hccClients.Rows.Find(serviceRow["Clnt_id"]);

                    DateTime serviceDate = serviceRow["Service_date"] != DBNull.Value ? Convert.ToDateTime(serviceRow["Service_date"]) : DateTime.MinValue;
                    DateTime createdOnDate = serviceRow["CreatedOn"] != DBNull.Value ? Convert.ToDateTime(serviceRow["CreatedOn"]).AddDays(-1) : DateTime.MinValue; // Subtract 1 day from CreatedOn
                    TimeSpan lag = createdOnDate - serviceDate;

                    string grade = lag.Days < 1 ? "Early" :
                                   lag.Days >= 0 && lag.Days <= 5 ? "On Time" : "Late";
                    string hccExportFailureReason = string.Empty;

                    int quantityServed = 0;
                    // Check if Contract_id is null

                    // Check if Map
                    dataGridView.Rows.Add(
                     serviceRow["Staff_id"],
                     clientRow != null ? clientRow["Agency_client_1"] : DBNull.Value,
                     serviceRow["Clnt_id"],
                     DBNull.Value,
                     DBNull.Value,
                     DBNull.Value,
                     DBNull.Value,
                     DBNull.Value,
                     DBNull.Value,
                     DBNull.Value,
                     serviceRow["Contract_id"],
                     serviceRow["Quantity_served"],
                     serviceRow["Actual_minutes_spent"],
                     serviceRow["MappedToHCC"],
                     serviceRow["ServiceID"],
                     serviceRow["MappedToHCC"] != DBNull.Value && Convert.ToBoolean(serviceRow["MappedToHCC"]) ? "Yes" : "", // Convert MappedToHCC to Yes/No
                     serviceRow["Service_date"],
                     serviceRow["Status"],
                     serviceRow["ErrorMessage"],
                     createdOnDate,
                     lag.Days,
                     grade,
                     hccExportFailureReason);
                }
                // Auto-size columns to fit content
                dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void cbDateFilter_SelectedIndexChanged(object sender, EventArgs e)//to filter data
        {
            try
            {
                // Store the selected filter type
                //selectedFilterType = dtpDateFilter.SelectedItem.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string selectedFilterType;

        //private void btnReport_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // Ensure the date pickers are properly set
        //        DateTime startDate = dtpStartDate.Value;
        //        DateTime endDate = dtpEndDate.Value;

        //        // Validate that the end date is greater than the start date
        //        if (endDate <= startDate)
        //        {
        //            MessageBox.Show(Constants.StartdatemustbeearlierthanEnddate);
        //            return;
        //        }
        //        if (dtpDateFilter.SelectedItem ==Constants.createddate)
        //        {
        //            MessageBox.Show("Please select a service date from the dropdown.", "Date Selection Required");
        //            return;
        //        }
        //        // Validate BatchID input
        //        //if (!int.TryParse(txtBatchID.Text, out int BatchID))
        //        //{
        //        //    MessageBox.Show("Please enter a valid Batch ID.","ServiceReconciliationReport");
        //        //    return;
        //        //}
        //        // Set up the DataGridView
        //        dataGridView.AutoGenerateColumns = true;
        //        dataGridView.Columns.Clear();
        //        dataGridView.ForeColor = Color.Black;
        //        // Fetch data using DBHelper
        //        DBHelper dbHelper = new DBHelper();
        //        try
        //        {
        //            // Call the LoadData method to fetch the data
        //            DataTable result = dbHelper.LoadDatafilterServiceRecon(startDate, endDate, batchid);//batchid removed
        //            // Bind the result to the DataGridView
        //            dataGridView.DataSource = result;
        //        }
        //        catch (Exception ex)
        //        {
        //            // Handle exceptions, such as logging the error
        //            MessageBox.Show($"An error occurred while loading data: {ex.Message}");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exceptions related to DateTimePicker values or other issues
        //        MessageBox.Show($"An error occurred: {ex.Message}");
        //    }
        //}
        private void DisplayHeadersOnly()
        {
            dataGridView.DataSource = null;
            dataGridView.Columns.Clear();
            dataGridView.Columns.Add("Sl No", "Sl No");
            dataGridView.Columns.Add("BatchID", "Batch ID");
            dataGridView.Columns.Add("Staff", "Staff");
            //dataGridView.Columns.Add("ClientID", "Client ID");
            dataGridView.Columns.Add("HCCID", "HCC ID");
            dataGridView.Columns.Add("HCCConsentExpiryDate", "HCC Consent Expiry Date");
            dataGridView.Columns.Add("RWEligibilityExpiryDate", "RW Eligibility Expiry Date");
            dataGridView.Columns.Add("Service", "Service");
            dataGridView.Columns.Add("ServiceCodeID", "Service Code ID");
            dataGridView.Columns.Add("HCCContractID", "HCC Contract ID");
            dataGridView.Columns.Add("UnitsOfServices", "Units of Services");
            dataGridView.Columns.Add("ActualMinutesSpent", "Actual Minutes Spent");
            dataGridView.Columns.Add("ServiceID", "Service ID");
            dataGridView.Columns.Add("ServiceExportedToHCC", "Service Exported to HCC");
            dataGridView.Columns.Add("ServiceDate", "Service Date");
            dataGridView.Columns.Add("EntryDate", "Entry Date");
            dataGridView.Columns.Add("Lag", "Lag");
            dataGridView.Columns.Add("Lag Status", "Lag Status");
            dataGridView.Columns.Add("HCCExportFailureReason", "HCC Export Failure Reason");
            dataGridView.AutoGenerateColumns = false;
        }
        private void btnReport_Click(object sender, EventArgs e)
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
                DBHelper dbHelper = new DBHelper();
                dataGridView.Columns.Clear();
                dataGridView.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dataGridView_CellFormatting);

                // Determine filter type
                string filterType = string.Empty;
                if (!string.IsNullOrWhiteSpace(txtBatchID.Text) && int.TryParse(txtBatchID.Text, out int batchid) || (!string.IsNullOrWhiteSpace(txtBatchID.Text) && txtBatchID.Text.Contains(",")) || (!string.IsNullOrWhiteSpace(txtBatchID.Text)))
                {
                    filterType = "BatchID";
                }
                else if (dtpDateFilter.SelectedItem != null)
                {
                    switch (dtpDateFilter.SelectedItem.ToString())
                    {
                        case Constants.servicedate:
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
                    DisplayHeadersOnly();
                    return;
                }

                // Fetch data based on selected filter type
                DataTable result = null;
                int[] Batchids = null;
                if (filterType == "BatchID")
                {
                    // Get and validate batch IDs
                    string batchIdText = txtBatchID.Text;
                    Batchids = txtBatchID.Text.Split(',').Select(int.Parse).Distinct().ToArray();
                    if (Batchids.Length > 0)
                    {
                        result = dbHelper.LoadDatafilterServiceReconbatchid(Batchids); //

                        if (result.Rows.Count == 0)
                        {
                            DisplayHeadersOnly();
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter valid Batch IDs.", "Invalid Input");
                        DisplayHeadersOnly();
                        return;
                    }
                }
                else
                {
                    result = dbHelper.LoadDatafilterServiceRecon(startDate, endDate, filterType);
                }

                // Add a 'Sl No' column for serial number
                DataGridViewTextBoxColumn slNoColumn = new DataGridViewTextBoxColumn();

                // Insert at the first position

                // Add serial numbers (Sl No) to the DataTable before binding
                int serialNumber = 1;
                result.Columns["Sl No"].ReadOnly = false;
                foreach (DataRow row in result.Rows)
                {
                    row["Sl No"] = serialNumber++; // Assign the serial number
                }

                // Bind the data to the DataGridView
                dataGridView.AutoGenerateColumns = true;
                dataGridView.DataSource = result;
            }
            catch (Exception ex)
            {
                DisplayHeadersOnly();
                MessageBox.Show($"An error occurred: {ex.Message}");

            }
        }
        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Check if the cell value is not null
            if (e.Value != null)
            {
                // Example condition to change color
                if (e.Value.ToString() == "YourCondition") // Replace with your actual condition
                {
                    e.CellStyle.ForeColor = Color.Red; // Set desired color for the condition
                }
                else
                {
                    e.CellStyle.ForeColor = Color.Black; // Default color for other cases
                }
            }
        }
        private void btnDownload_Click(object sender, EventArgs e) // to export data to selected folder
        {
            try
            {
                // Check if there are any rows in the DataGridView
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
                            string baseFileName = Constants.Service_ReconciliationReport;
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
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void btnClr_Click(object sender, EventArgs e)//to clear data and filter date to default one
        {
            try
            {
                // Reset DateTimePickers to one year back from the current date
                dtpStartDate.Value = DateTime.Now.AddYears(-1);
                dtpStartDate.CustomFormat = "MM-dd-yyyy";
                dtpEndDate.Value = DateTime.Now;
                dtpEndDate.CustomFormat = "MM-dd-yyyy";
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

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}