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
        }
        public void PopulateDataGridView(DataTable hccServices, DataTable hccClients)//populate data
        {
            try { 
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
     createdOnDate,
     lag.Days,
     grade,
     hccExportFailureReason
 );
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
                selectedFilterType = dtpDateFilter.SelectedItem.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string selectedFilterType;

        private void btnReport_Click(object sender, EventArgs e)//to load data in the grid
        {
            // Ensure that the DateTimePickers have valid dates
            try
            {
               
                DBHelper dbHelper = new DBHelper();
                dataGridView.AutoGenerateColumns = true;
                dataGridView.Columns.Clear();
                // Ensure the date pickers are properly set
                DateTime startDate = dtpStartDate.Value;
                DateTime endDate = dtpEndDate.Value;

                try
                {
                    // Call the LoadData method to fetch the data
                    if (endDate <= startDate)
                    {
                        MessageBox.Show(Constants.StartdatemustbelessthanEnddate);


                    }
                 
                    dataGridView.ForeColor = Color.Black;


                    DataTable result = dbHelper.LoadDatafilterServiceRecon(startDate, endDate);//load the filtered data

                    // Now you can use the result, e.g., bind it to a DataGridView or process it
                    dataGridView.DataSource = result;
                    // PopulateMonthYearGrid(startDate, endDate);
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
