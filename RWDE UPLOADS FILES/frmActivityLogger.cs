using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;

namespace RWDE
{
    public partial class frmActivityLogger : Form
    {
        public frmActivityLogger()// initialize data
        {
            InitializeComponent();
            dataGridView.AutoGenerateColumns = false;
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
                            MessageBox.Show($@"{Constants.Datasuccessfullysaved} {Path.GetFileName(filePath)}", Constants.HccReconciliation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //private void btnReport_Click(object sender, EventArgs e)
        //{

        //    try
        //    {
        //        string filterType = string.Empty;
        //        string batchIds = txtbatchs.Text.Trim();

        //        if (string.IsNullOrEmpty(batchIds))
        //        {
        //            filterType = Constants.CreatedDate;
        //        }
        //        else
        //        {
        //            filterType = Constants.BatchId;
        //        }

        //        DateTime startDate = dtpStartDate.Value;
        //        DateTime endDate = dtpEndDate.Value;
        //        DataTable dt = new DataTable();


        //        if (endDate <= startDate)
        //        {
        //            MessageBox.Show(Constants.StartdatemustbeearlierthanEnddate);
        //            return;
        //        }


        //        // Get connection string from App.config
        //        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;


        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            using (SqlCommand cmd = new SqlCommand("sp_ActivityLogger", conn))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@StartDate", startDate);
        //                cmd.Parameters.AddWithValue("@EndDate", endDate);
        //                cmd.Parameters.AddWithValue("@BatchId", batchIds);
        //                cmd.Parameters.AddWithValue("@Type", filterType);

        //                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        //                {
        //                    da.Fill(dt);
        //                }
        //            }
        //        }

        //        if (dt == null || dt.Rows.Count < 1)
        //        {
        //            MessageBox.Show(Constants.Nodatafoundbetweenselecteddates);

        //        }
        //        dataGridView.DataSource = null;
        //        dataGridView.Rows.Clear();
        //        dataGridView.Columns.Clear(); // Clear any existing column definitions
        //        dataGridView.AutoGenerateColumns = true; // Ensure columns are created from DataTable

        //        dataGridView.DataSource = dt;
        //        dataGridView.AllowUserToAddRows = false;
        //        dataGridView.ForeColor = Color.Black;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($@"{Constants.AnErrorOccurred}{ex.Message}");
        //    }
        //}


        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                string filterType = string.Empty;
                string batchIdsInput = txtbatchs.Text.Trim();
                List<int> validBatchIds = new List<int>();
                List<int> missingBatchIds = new List<int>();
                DataTable finalDataTable = new DataTable();

                DateTime startDate = dtpStartDate.Value;
                DateTime endDate = dtpEndDate.Value;

                if (endDate <= startDate)
                {
                    MessageBox.Show(Constants.StartdatemustbeearlierthanEnddate);
                    return;
                }

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

                if (string.IsNullOrEmpty(batchIdsInput))
                {
                    // Filter by Date
                    filterType = Constants.CreatedDate;

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_ActivityLogger", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@StartDate", startDate);
                            cmd.Parameters.AddWithValue("@EndDate", endDate);
                            cmd.Parameters.AddWithValue("@BatchId", ""); // no batch id
                            cmd.Parameters.AddWithValue("@Type", filterType);

                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(finalDataTable);
                            }
                        }
                    }
                }
                else
                {
                    // Filter by Batch ID
                    filterType = Constants.BatchId;

                    string[] batchIdParts = batchIdsInput.Split(',');
                    foreach (string batchIdStr in batchIdParts)
                    {
                        if (int.TryParse(batchIdStr.Trim(), out int batchId))
                        {
                            DataTable dt = new DataTable();

                            using (SqlConnection conn = new SqlConnection(connectionString))
                            {
                                using (SqlCommand cmd = new SqlCommand("sp_ActivityLogger", conn))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                                    cmd.Parameters.AddWithValue("@EndDate", endDate);
                                    cmd.Parameters.AddWithValue("@BatchId", batchId.ToString());
                                    cmd.Parameters.AddWithValue("@Type", filterType);

                                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                                    {
                                        da.Fill(dt);
                                    }
                                }
                            }

                            if (dt.Rows.Count > 0)
                            {
                                finalDataTable.Merge(dt); // Accumulate data
                                validBatchIds.Add(batchId);
                            }
                            else
                            {
                                missingBatchIds.Add(batchId); // Log missing
                            }
                        }
                    }

                    if (missingBatchIds.Count > 0)
                    {
                        MessageBox.Show($"No data found for the following batch ID(s): {string.Join(", ", missingBatchIds)}", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                // Display the final data
                if (finalDataTable == null || finalDataTable.Rows.Count < 1)
                {
                    MessageBox.Show(Constants.Nodatafoundbetweenselecteddates);
                    return;
                }

                dataGridView.DataSource = null;
                dataGridView.Rows.Clear();
                dataGridView.Columns.Clear();
                dataGridView.AutoGenerateColumns = true;
                dataGridView.DataSource = finalDataTable;
                dataGridView.AllowUserToAddRows = false;
                dataGridView.ForeColor = Color.Black;
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"{Constants.AnErrorOccurred}{ex.Message}");
            }
        }


        private void btnClr_Click(object sender, EventArgs e)// clear the data in the grid
        {
            try
            {
                // Reset DateTimePickers to one year back from the current date
                dtpStartDate.Value = DateTime.Now.AddYears(-1);
                dtpStartDate.CustomFormat = Constants.DateFormatMMddyyyy;
                dtpEndDate.Value = DateTime.Now;
                dtpEndDate.CustomFormat = Constants.DateFormatMMddyyyy;
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

        //private DataTable GetDataFromStoredProcedure(DateTime startDate, DateTime endDate, string batchIds)
        //{
        //    DataTable dt = new DataTable();
        //    // Use your actual connection string here, or use DbHelper.GetConnection() if available
        //    string connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;


        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    using (SqlCommand cmd = new SqlCommand("sp_ActivityLogger", conn))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        // Add parameters as required by your stored procedure
        //        cmd.Parameters.AddWithValue("@StartDate", startDate);
        //        cmd.Parameters.AddWithValue("@EndDate", endDate);
        //        cmd.Parameters.AddWithValue("@BatchIds", batchIds); // Example: comma-separated batch IDs

        //        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        //        {
        //            da.Fill(dt);
        //        }
        //    }
        //    return dt;
        //}


        private void lblTitle_Click(object sender, EventArgs e)
        {

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

    }
}
