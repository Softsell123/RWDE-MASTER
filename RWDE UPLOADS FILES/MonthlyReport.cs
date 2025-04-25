using ClosedXML.Excel;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace RWDE
{
    public partial class MonthlyReport : Form
    {
        public MonthlyReport()// initialize data
        {
            InitializeComponent();
            ControlBox = false;
            WindowState = FormWindowState.Maximized;
            
            cbDateFilter.Items.AddRange(new object[]
               {
                   Constants.PreviousWeek, // Add "Previous Week" item
                   Constants.CurrentWeek,
                   Constants.PreviousMonth,
                   Constants.CurrentMonth,
                   Constants.SinceOneMonthAgo,
                   Constants.FirstQuarter,
                   Constants.SecondQuarter,
                   Constants.ThirdQuarter,
                   Constants.FourthQuarter,
                   Constants.PreviousQuarter,
                   Constants.CurrentQuarter,
                   Constants.ThisYear,
                   Constants.LastYear,
                   Constants.SinceThisDateLastYear
              });

            // Attach SelectedIndexChanged event handler
            cbDateFilter.SelectedIndexChanged += cbDateFilter_SelectedIndexChanged;// filter data accordingly
            dtpStartDate.Value = DateTime.Now.AddYears(-1);
            dtpStartDate.CustomFormat = Constants.DateFormatMMddyyyy;
            dtpEndDate.CustomFormat = Constants.DateFormatMMddyyyy;
            // Assuming you have another DateTimePicker for the End Date
            dtpEndDate.Value = DateTime.Now;
            // Make sure to instantiate the DBHelper class
            dataGridView.AutoGenerateColumns = true;
            try
            {

                dataGridView.ForeColor = Color.Black;
            }
            catch (Exception ex)
            {
                // Handle exceptions, such as logging the error
                MessageBox.Show(ex.Message);
            }
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
        private void SetPreviousWeekDates(out DateTime startDate, out DateTime endDate)// date filter
        {
            try
            {
                // Get the current date
                DateTime today = DateTime.Today;

            // Calculate the number of days to subtract to get the previous week's start date (last Monday)

            int daysToSubtract = (int)today.DayOfWeek + 7; // If today is Monday, we go back to last Monday
            startDate = today.AddDays(-daysToSubtract).Date; // Previous week's Monday

            // End date is the last day of the previous week (Sunday)
            endDate = startDate.AddDays(6); // Previous week's Sunday
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                startDate = DateTime.MinValue;
                endDate = DateTime.MinValue;
            }
        }
        private void btnDownload_Click(object sender, EventArgs e)// to export report to selected folder
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
                            string baseFileName = ContractIdList.MonthlyReports;
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
                            MessageBox.Show($@"{Constants.Datasuccessfullysaved}: {Path.GetFileName(filePath)}", ContractIdList.MonthlyReports, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
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
        private void btnReport_Click(object sender, EventArgs e)// to get filtered data in the grid
        {
            try
            {
                DbHelper dbHelper = new DbHelper();
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

                    DataTable result = dbHelper.LoadDatafilter(startDate, endDate);// to load filtered data for monthly reports
                    if (dbHelper.ErrorOccurred)
                    {
                        MessageBox.Show(Constants.ErrorOccurred);
                        return;
                    }

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
        private void btnClr_Click(object sender, EventArgs e)// to clear data in the grid
        {
            try { 
                 // Reset DateTimePickers to one year back from the current date
            dtpStartDate.Value = DateTime.Now.AddYears(-1);
            dtpStartDate.CustomFormat = Constants.DateFormatMMddyyyy;
            dtpEndDate.Value = DateTime.Now;
            dtpEndDate.CustomFormat = Constants.DateFormatMMddyyyy;
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
        private void cbDateFilter_SelectedIndexChanged(object sender, EventArgs e)// to filter date accordingly
        {
            try
            {
                if (cbDateFilter.SelectedItem == null)
                return;

            string selectedFilter = cbDateFilter.SelectedItem.ToString();
            DateTime startDate, endDate;

            switch (selectedFilter)
            {
                case Constants.PreviousWeek:
                    SetPreviousWeekDates(out startDate, out endDate);// filter weekly
                    break;

                case Constants.CurrentWeek:
                    SetCurrentWeekDates(out startDate, out endDate);// filter weekly
                    break;

                case Constants.PreviousMonth:
                    SetPreviousMonthDates(out startDate, out endDate);// filter weekly
                    break;

                case Constants.CurrentMonth:
                    SetCurrentMonthDates(out startDate, out endDate);// filter weekly
                    break;

                case Constants.SinceOneMonthAgo:
                    SetSinceOneMonthAgoDates(out startDate, out endDate);// filter monthly
                    break;

                case Constants.FirstQuarter:
                    SetQuarterDates(1, out startDate, out endDate);// filter quarterly
                    break;

                case Constants.SecondQuarter:
                    SetQuarterDates(2, out startDate, out endDate);// filter second quarterly
                    break;

                case Constants.ThirdQuarter:
                    SetQuarterDates(3, out startDate, out endDate);// filter quarterly
                    break;

                case Constants.FourthQuarter:
                    SetQuarterDates(4, out startDate, out endDate);// filter quarterly
                    break;

                case Constants.PreviousQuarter:
                    SetPreviousQuarterDates(out startDate, out endDate);// filter quarterly
                    break;

                case Constants.CurrentQuarter:
                    SetCurrentQuarterDates(out startDate, out endDate);// filter quarterly
                    break;

                case Constants.ThisYear:
                    SetThisYearDates(out startDate, out endDate);// filter yearly
                    break;

                case Constants.LastYear:
                    SetLastYearDates(out startDate, out endDate);// filter yearly
                    break;

                case Constants.SinceThisDateLastYear:
                    SetSinceThisDateLastYear(out startDate, out endDate);// filter yearly
                    break;

                default:
                    return; // Handle invalid or other cases
            }
            // Set DateTimePicker values
            dtpStartDate.Value = startDate;
            dtpEndDate.Value = endDate;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SetCurrentWeekDates(out DateTime startDate, out DateTime endDate)// filter data on weekly basis
        {
            try
            {
                DateTime today = DateTime.Today;
            int daysToSubtract = (int)today.DayOfWeek;
            startDate = today.AddDays(-daysToSubtract).Date;
            endDate = startDate.AddDays(6);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                startDate = DateTime.MinValue;
                endDate = DateTime.MinValue;
            }
        }
        private void SetPreviousMonthDates(out DateTime startDate, out DateTime endDate)// filter data on monthly basis
        {
            try
            {
                DateTime today = DateTime.Today;
            DateTime firstDayOfCurrentMonth = new DateTime(today.Year, today.Month, 1);
            startDate = firstDayOfCurrentMonth.AddMonths(-1);
            endDate = firstDayOfCurrentMonth.AddDays(-1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                startDate = DateTime.MinValue;
                endDate = DateTime.MinValue;
            }
        }
        private void SetCurrentMonthDates(out DateTime startDate, out DateTime endDate)// filter data on current month 
        {
            try
            {
                DateTime today = DateTime.Today;
            startDate = new DateTime(today.Year, today.Month, 1);
            endDate = startDate.AddMonths(1).AddDays(-1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                startDate = DateTime.MinValue;
                endDate = DateTime.MinValue;
            }
        }
        private void SetSinceOneMonthAgoDates(out DateTime startDate, out DateTime endDate)// filter data on one month ago
        {
            try
            {
                endDate = DateTime.Today;
            startDate = endDate.AddMonths(-1).AddDays(1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                startDate = DateTime.MinValue;
                endDate = DateTime.MinValue;
            }
        }
        private void SetQuarterDates(int quarter, out DateTime startDate, out DateTime endDate)// to modify start and end date on particular selection
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                startDate = DateTime.MinValue;
                endDate = DateTime.MinValue;
            }
        }
        private void SetPreviousQuarterDates(out DateTime startDate, out DateTime endDate)// filter data on Quarterly basis
        {
            try
            {
                DateTime today = DateTime.Today;
            int currentQuarter = (today.Month - 1) / 3 + 1;
            int previousQuarter = currentQuarter == 1 ? 4 : currentQuarter - 1;
            int year = currentQuarter == 1 ? today.Year - 1 : today.Year;
            SetQuarterDates(previousQuarter, out startDate, out endDate);
            startDate = new DateTime(year, startDate.Month, 1);
            endDate = new DateTime(year, endDate.Month, DateTime.DaysInMonth(year, endDate.Month));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                startDate = DateTime.MinValue;
                endDate = DateTime.MinValue;
            }
        }
        private void SetCurrentQuarterDates(out DateTime startDate, out DateTime endDate)// filter data on Quarterly basis
        {
            try
            {
                DateTime today = DateTime.Today;
                int currentQuarter = (today.Month - 1) / 3 + 1;
                SetQuarterDates(currentQuarter, out startDate, out endDate);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                startDate = DateTime.MinValue;
                endDate = DateTime.MinValue;
            }
        }
        private void SetThisYearDates(out DateTime startDate, out DateTime endDate)// filter data on yearly basis
        {
            try
            {
                int currentYear = DateTime.Today.Year;
                startDate = new DateTime(currentYear, 1, 1);
                endDate = new DateTime(currentYear, 12, 31);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                startDate = DateTime.MinValue;
                endDate = DateTime.MinValue;
            }
        }
        private void SetLastYearDates(out DateTime startDate, out DateTime endDate)// filter data on yearly basis
        {
            try
            {
                int lastYear = DateTime.Today.Year - 1;
                startDate = new DateTime(lastYear, 1, 1);
                endDate = new DateTime(lastYear, 12, 31);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                startDate = DateTime.MinValue;
                endDate = DateTime.MinValue;
            }

        }
        private void SetSinceThisDateLastYear(out DateTime startDate, out DateTime endDate)// filter data on yearly basis
        {
            try
            {
                endDate = DateTime.Today;
                startDate = endDate.AddYears(-1).AddDays(1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                startDate = DateTime.MinValue;
                endDate = DateTime.MinValue;
            }

        }
    }
}