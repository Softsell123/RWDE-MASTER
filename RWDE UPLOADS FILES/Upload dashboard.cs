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
    public partial class Upload_dashboard : Form
    {
        private readonly string connectionString;
        private readonly DBHelper dbHelper;
        public Upload_dashboard()//initialize data
        {
            InitializeComponent();
            this.ControlBox = false;
            this.WindowState = FormWindowState.Maximized;
            cbDateFilter.Items.AddRange(new string[]
               {
        "Previous Week", // Add "Previous Week" item
        "Current Week",
        "Previous Month",
        "Current Month",
        "Since One Month Ago",
        "First Quarter",
        "Second Quarter",
        "Third Quarter",
        "Fourth Quarter",
        "Previous Quarter",
        "Current Quarter",
        "This Year",
        "Last Year",
        "Since This Date Last Year"
              });

            // Attach SelectedIndexChanged event handler
            cbDateFilter.SelectedIndexChanged += cbDateFilter_SelectedIndexChanged;//filter data accordingly
            dtpStartDate.Value = DateTime.Now.AddYears(-1);
            dtpStartDate.CustomFormat = "MM-dd-yyyy";
            dtpEndDate.CustomFormat = "MM-dd-yyyy";
            // Assuming you have another DateTimePicker for the End Date
            dtpEndDate.Value = DateTime.Now;
            // Make sure to instantiate the DBHelper class
            DBHelper dbHelper = new DBHelper();
            dataGridView.AutoGenerateColumns = true;

            // Ensure the date pickers are properly set
            DateTime startDate = dtpStartDate.Value;
            DateTime endDate = dtpEndDate.Value;

            try
            {

                dataGridView.ForeColor = Color.Black;
            }
            catch (Exception ex)
            {
                // Handle exceptions, such as logging the error
                MessageBox.Show(ex.Message);
            }
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
                if (control is System.Windows.Forms.Button || control is CheckBox || control is DateTimePicker || control is ComboBox)
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
        private void SetPreviousWeekDates(out DateTime startDate, out DateTime endDate)//date filter
        {
            // Get the current date
            DateTime today = DateTime.Today;

            // Calculate the number of days to subtract to get the previous week's start date (last Monday)
            int daysToSubtract = (int)today.DayOfWeek + 7; // If today is Monday, we go back to last Monday
            startDate = today.AddDays(-daysToSubtract).Date; // Previous week's Monday

            // End date is the last day of the previous week (Sunday)
            endDate = startDate.AddDays(6); // Previous week's Sunday
        }
        private void SetDataGridViewFontSize(DataGridView dataGridView, float fontSize)//date filter for weeks
        {
            // Set the font size of the DataGridView
            dataGridView.Font = new Font(dataGridView.Font.FontFamily, fontSize);
        }        // Usage example
        public void ConfigureDataGridView()//date filter styles
        {
            try
            {// Assuming you have a DataGridView named dataGridView
             // Set font size to 16
                SetDataGridViewFontSize(dataGridView, 16);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        
        }
        private void PopulateMonthYearGrid(DateTime startDate, DateTime endDate)//populate data
        {
            try {// Create a DataTable to hold the month-year data
                DataTable monthYearTable = new DataTable();
                monthYearTable.Columns.Add("Monthyear", typeof(string));

                // Define the date range for months
                DateTime current = startDate;
                while (current <= endDate)
                {
                    // Format the date as Month-Year
                    string formattedMonthYear = current.ToString("MMM-yyyy");
                    monthYearTable.Rows.Add(formattedMonthYear);

                    // Move to the next month
                    current = current.AddMonths(1);
                }

                // Bind the DataTable to the DataGridView
                dataGridView.DataSource = monthYearTable;
                dataGridView.ForeColor = Color.Black;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnDownload_Click(object sender, EventArgs e)//to export report to selected folder
        {
            try
            {
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
                            string baseFileName = ContractIDList.Monthly_Reports;
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
                            MessageBox.Show($"{Constants.datasuccessfullysaved}: {Path.GetFileName(filePath)}", ContractIDList.Monthly_Reports, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
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


                try
                {
                    if (endDate <= startDate)
                    {
                        MessageBox.Show(Constants.StartdatemustbeearlierthanEnddate);
                    }

                    // Call the LoadData method to fetch the data

                    dataGridView.ForeColor = Color.Black;


                    DataTable result = dbHelper.LoadDatafilter(startDate, endDate);//to load filtered data for monthly reports

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
        private void btnClr_Click(object sender, EventArgs e)//to clear data in the grid
        {
            try { 
            // Reset DateTimePickers to one year back from the current date
            dtpStartDate.Value = DateTime.Now.AddYears(-1);
            dtpStartDate.CustomFormat = "MM-dd-yyyy";
            dtpEndDate.Value = DateTime.Now;
            dtpEndDate.CustomFormat = "MM-dd-yyyy";
                cbDateFilter.Text = "";
            // Clear the DataTable bound to the DataGridView
            if (dataGridView.DataSource is DataTable dt)
            {
                dt.Clear();  // Clears all rows from the DataTable
            }

                // Optionally reset any other controls or fields if necessary
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }     
        private void cbDateFilter_SelectedIndexChanged(object sender, EventArgs e)//to filter date accordingly
        {
            if (cbDateFilter.SelectedItem == null)
                return;

            string selectedFilter = cbDateFilter.SelectedItem.ToString();
            DateTime startDate, endDate;

            switch (selectedFilter)
            {
                case "Previous Week":
                    SetPreviousWeekDates(out startDate, out endDate);//filter weekly
                    break;

                case "Current Week":
                    SetCurrentWeekDates(out startDate, out endDate);//filter weekly
                    break;

                case "Previous Month":
                    SetPreviousMonthDates(out startDate, out endDate);//filter weekly
                    break;

                case "Current Month":
                    SetCurrentMonthDates(out startDate, out endDate);//filter weekly
                    break;

                case "Since One Month Ago":
                    SetSinceOneMonthAgoDates(out startDate, out endDate);//filter monthly
                    break;

                case "First Quarter":
                    SetQuarterDates(1, out startDate, out endDate);//filter quaterly
                    break;

                case "Second Quarter":
                    SetQuarterDates(2, out startDate, out endDate);//filter secomd quaterly
                    break;

                case "Third Quarter":
                    SetQuarterDates(3, out startDate, out endDate);//filter quaterly
                    break;

                case "Fourth Quarter":
                    SetQuarterDates(4, out startDate, out endDate);//filter quaterly
                    break;

                case "Previous Quarter":
                    SetPreviousQuarterDates(out startDate, out endDate);//filter quaterly
                    break;

                case "Current Quarter":
                    SetCurrentQuarterDates(out startDate, out endDate);//filter quaterly
                    break;

                case "This Year":
                    SetThisYearDates(out startDate, out endDate);//filter yearly
                    break;

                case "Last Year":
                    SetLastYearDates(out startDate, out endDate);//filter yearly
                    break;

                case "Since This Date Last Year":
                    SetSinceThisDateLastYear(out startDate, out endDate);//filter yearly
                    break;

                default:
                    return; // Handle invalid or other cases
            }

            // Set DateTimePicker values
            dtpStartDate.Value = startDate;
            dtpEndDate.Value = endDate;

        }
        private void SetCurrentWeekDates(out DateTime startDate, out DateTime endDate)//filter data on weekly basis
        {

            DateTime today = DateTime.Today;
            int daysToSubtract = (int)today.DayOfWeek;
            startDate = today.AddDays(-daysToSubtract).Date;
            endDate = startDate.AddDays(6);
        }
        private void SetPreviousMonthDates(out DateTime startDate, out DateTime endDate)//filter data on monthly basis
        {
            DateTime today = DateTime.Today;
            DateTime firstDayOfCurrentMonth = new DateTime(today.Year, today.Month, 1);
            startDate = firstDayOfCurrentMonth.AddMonths(-1);
            endDate = firstDayOfCurrentMonth.AddDays(-1);
        }

        private void SetCurrentMonthDates(out DateTime startDate, out DateTime endDate)//filter data on current month 
        {
            DateTime today = DateTime.Today;
            startDate = new DateTime(today.Year, today.Month, 1);
            endDate = startDate.AddMonths(1).AddDays(-1);
        }

        private void SetSinceOneMonthAgoDates(out DateTime startDate, out DateTime endDate)//filter data on one month ago
        {
            endDate = DateTime.Today;
            startDate = endDate.AddMonths(-1).AddDays(1);
        }
        private void SetQuarterDates(int quarter, out DateTime startDate, out DateTime endDate)//to modify start and end date on particular selection
        {
            int currentYear = DateTime.Today.Year;
            switch (quarter)
            {
                case 1:
                    startDate = new DateTime(currentYear, 1, 1);
                    endDate = new DateTime(currentYear, 3, 31);
                    break;
                case 2:
                    startDate = new DateTime(currentYear, 4, 1);
                    endDate = new DateTime(currentYear, 6, 30);
                    break;
                case 3:
                    startDate = new DateTime(currentYear, 7, 1);
                    endDate = new DateTime(currentYear, 9, 30);
                    break;
                case 4:
                    startDate = new DateTime(currentYear, 10, 1);
                    endDate = new DateTime(currentYear, 12, 31);
                    break;
                default:
                    startDate = endDate = DateTime.MinValue;
                    break;
            }
        }

        private void SetPreviousQuarterDates(out DateTime startDate, out DateTime endDate)//filter data on Quaterly basis
        {
            DateTime today = DateTime.Today;
            int currentQuarter = (today.Month - 1) / 3 + 1;
            int previousQuarter = currentQuarter == 1 ? 4 : currentQuarter - 1;
            int year = currentQuarter == 1 ? today.Year - 1 : today.Year;
            SetQuarterDates(previousQuarter, out startDate, out endDate);
            startDate = new DateTime(year, startDate.Month, 1);
            endDate = new DateTime(year, endDate.Month, DateTime.DaysInMonth(year, endDate.Month));
        }

        private void SetCurrentQuarterDates(out DateTime startDate, out DateTime endDate)//filter data on Quaterly basis
        {
            {
                DateTime today = DateTime.Today;
                int currentQuarter = (today.Month - 1) / 3 + 1;
                SetQuarterDates(currentQuarter, out startDate, out endDate);
            }
        }
        private void SetThisYearDates(out DateTime startDate, out DateTime endDate)//filter data on yearly basis
        {
            {
                int currentYear = DateTime.Today.Year;
                startDate = new DateTime(currentYear, 1, 1);
                endDate = new DateTime(currentYear, 12, 31);
            }
        }
        private void SetLastYearDates(out DateTime startDate, out DateTime endDate)//filter data on yearly basis
        {
            {
                int lastYear = DateTime.Today.Year - 1;
                startDate = new DateTime(lastYear, 1, 1);
                endDate = new DateTime(lastYear, 12, 31);
            }
        }
        private void SetSinceThisDateLastYear(out DateTime startDate, out DateTime endDate)//filter data on yearly basis
        {
            {
                endDate = DateTime.Today;
                startDate = endDate.AddYears(-1).AddDays(1);
            }

        }
    }
}