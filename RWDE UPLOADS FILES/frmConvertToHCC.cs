using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScrollBar = System.Windows.Forms.ScrollBar;


namespace RWDE
{
    public partial class FrmConvertToHcc : Form
    {
        private readonly string connectionString;
        private readonly DbHelper dbHelper;
        public Panel PanelToReplace
        {
            get
            {
                return pnlHCCConversion;
            }
        }
        public FrmConvertToHcc()
        {
            InitializeComponent();
            dbHelper = new DbHelper();
            connectionString = dbHelper.GetConnectionString();
            this.ControlBox = false;
            this.WindowState = FormWindowState.Maximized;
            //Handle BatchType Values
            //List<string> batchTypes = dbHelper.GetAllBatchTypes();
            dataGridView.CellFormatting += dataGridView_CellFormatting;
            dataGridView.DataBindingComplete += DataGridView_DataBindingComplete;
            dtpEndDate.CustomFormat = " ";
            dtpEndDate.Format = DateTimePickerFormat.Custom;

            // Do the same for dtpStartDate or any other DateTimePicker if needed
            dtpStartDate.CustomFormat = " ";
            dtpStartDate.Format = DateTimePickerFormat.Custom;
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
        private void dataGridView_Scroll(object sender, ScrollEventArgs e)
        {
            Cursor = Cursors.Hand;
        }
        private void RegisterEvents(Control parent)
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
                    RegisterEvents(control);
                }
            }
        }
        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)// Formats the cell value for the "Status" column based on the corresponding value in the database.
        {
            try
            {
                if (dataGridView.Columns[e.ColumnIndex].Name == "Status")
                {
                    string statusValue = e.Value?.ToString();
                    if (!string.IsNullOrEmpty(statusValue))
                    {
                        string valueSelectQuery = "select Value from List where ListsID = @ListsID";
                        using (SqlConnection sql = new SqlConnection(connectionString))
                        {
                            using (SqlCommand com = new SqlCommand(valueSelectQuery, sql))
                            {
                                com.Parameters.AddWithValue("@ListsID", statusValue);
                                sql.Open();
                                var result = com.ExecuteScalar();
                                sql.Close();

                                if (result != null)
                                {
                                    e.Value = result.ToString();
                                    e.FormattingApplied = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)// Sets the width of specific columns in the DataGridView after data binding is complete.
        {
            try
            {            // Check if the "BatchID" column exists before setting its width
                if (dataGridView.Columns.Contains("BatchID"))
                {
                    dataGridView.Columns["BatchID"].Width = 205;
                    // Set the width to 200 pixels
                }
                if (dataGridView.Columns.Contains("Type"))
                {
                    dataGridView.Columns["Type"].Width = 160; // Set the width to 200 pixels
                }
                if (dataGridView.Columns.Contains("FileName"))
                {
                    dataGridView.Columns["FileName"].Width = 205; // Set the width to 200 pixels
                }
                if (dataGridView.Columns.Contains("Description"))
                {
                    dataGridView.Columns["Description"].Width = 205; // Set the width to 200 pixels
                }
                if (dataGridView.Columns.Contains("UploadStartedAt"))
                {
                    dataGridView.Columns["UploadStartedAt"].Width = 205; // Set the width to 200 pixels
                }
                if (dataGridView.Columns.Contains("UploadEndedAt"))
                {
                    dataGridView.Columns["UploadEndedAt"].Width = 205; // Set the width to 200 pixels
                }
                if (dataGridView.Columns.Contains("ConversionStartedAt"))
                {
                    dataGridView.Columns["ConversionStartedAt"].Width = 250; // Set the width to 200 pixels
                }
                if (dataGridView.Columns.Contains("ConversionEndedAt"))
                {
                    dataGridView.Columns["ConversionEndedAt"].Width = 250; // Set the width to 200 pixels
                }

                if (dataGridView.Columns.Contains("Status"))
                {
                    dataGridView.Columns["Status"].Width = 205; // Set the width to 200 pixels
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        private void AddDateTime(string name, string value, DataGridView dataGridView)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn
            {
                Name = name,
                DataPropertyName = name,
                HeaderText = value,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "MM-dd-yyyy HH:mm:ss" }
            };
            dataGridView.Columns.Add(column);
        }
        private void UpdateBatchStatus(int batchId, int status, DateTime timestamp)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                using (SqlCommand command = new SqlCommand("UPDATE Batch SET Status = @Status, ConversionStartedAt = @Timestamp WHERE BatchID = @BatchID", connection))
                {
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@Timestamp", timestamp);
                    command.Parameters.AddWithValue("@BatchID", batchId);
                    command.ExecuteNonQuery();
                    ClearTables(batchId);
                }
            }
        }
        public Panel GetPanelToReplace()
        {
            // Return the panel you want to replace
            return pnlHCCConversion;
        }
        private async void btnCTtoHCC_Click(object sender, EventArgs e)//Insertion of Client and Eligibility into HCC tables
        {
            btncthcc.Enabled = false;
            int batchid = dbHelper.GetNextBatchId();
            RefreshValues();
            try
            {
                int selectedRowCount = dataGridView.SelectedRows.Count;
                // Check if no batch is selected
                if (selectedRowCount == 0)
                {
                    MessageBox.Show(Constants.Pleaseselectabatchbeforestartingtheconversion, Constants.ErrorCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btncloseHCC.Text = Constants.Close;
                    btncthcc.Enabled = true;
                    return; // Exit the method early
                }
                // Check if more than one batch is selected
                if (selectedRowCount != 1)
                {
                    MessageBox.Show(Constants.Pleaseselectonlyonebatchatatime, Constants.ErrorCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Exit the method early
                }
                btncloseHCC.Text = Constants.Abort;
                int selectedRowIndex = dataGridView.SelectedRows[0].Index;
                int selectedBatchId = Convert.ToInt32(dataGridView.Rows[selectedRowIndex].Cells["BatchID"].Value.ToString());
                bool batchExists = false;
                DateTime? conversionStartedAt = null;
                DateTime? conversionEndedAt = null;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SELECT ConversionStartedAt, ConversionEndedAt FROM Batch WHERE BatchID = @BatchID", connection))
                    {
                        command.Parameters.AddWithValue("@BatchID", selectedBatchId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                conversionStartedAt = reader.IsDBNull(0) ? null : (DateTime?)reader.GetDateTime(0);
                                conversionEndedAt = reader.IsDBNull(1) ? null : (DateTime?)reader.GetDateTime(1);
                                batchExists = true;
                            }
                        }
                    }
                }
                if (conversionStartedAt != null && conversionEndedAt != null)
                {
                    DialogResult result = MessageBox.Show($"Batch ID {selectedBatchId} has already completed the conversion.", "OCHIN To HCC Conversion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btncthcc.Enabled = true;
                    btncloseHCC.Text = "Close";
                    return;
                }
                UpdateGridStatus(selectedBatchId, Constants.Hccstartcon);//update the Status label in Status Column
                dataGridView.Refresh();
                string baseFilename = Constants.ServiceCttohcc;
                dbHelper.Log(Constants.ConverttoHcCforbatchIdStarted, Constants.ClientTrack, baseFilename, Constants.Uploadct);

                DateTime startTime = DateTime.Now;
                dataGridView.Rows[selectedRowIndex].Cells["Status"].Value = 17;
                dataGridView.Rows[selectedRowIndex].Cells["ConversionStartedAt"].Value = startTime;
                UpdateBatchStatus(selectedBatchId, 17, startTime);
                txtBatchid.Text = selectedBatchId.ToString();
                txtUploadStarted.Text = startTime.ToString("MM/dd/yyyy HH:mm:ss");// Record the start time

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("MapCMSClientstest", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Pass the selected BatchID to the stored procedure
                        command.Parameters.AddWithValue("@BatchID", selectedBatchId);

                        // Get the total number of rows to be inserted
                        int totalRows = GetTotalRowsForBatch(selectedBatchId);

                        // Initialize progress variables
                        int insertedRows = 0;
                        //string baseFilename = Path.GetFileNameWithout
                        int batchId = dbHelper.GetNextBatchId();


                        // Update progress textbox with initial progress information
                        await UpdateProgressAsync(insertedRows, totalRows);

                        // Set up progress bar
                        progressBarClients.Maximum = totalRows;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (insertedRows < totalRows)
                            {
                                // Process each row
                                insertedRows++;

                                // Update progress bar and text box
                                await UpdateProgressAsync(insertedRows, totalRows);
                            }
                        }

                        DateTime endTime = DateTime.Now;
                        UpdateGridStatus(selectedBatchId, Constants.Hccendcon);//Update Status label in Status Column 
                        dataGridView.Refresh();

                        dbHelper.Log(Constants.ConverttoHcCforbatchIdStarted, Constants.ClientTrack, baseFilename, Constants.Uploadct);
                        _ = GetservicesAsync(selectedBatchId);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        public FrmConvertToHcc(string message, int displayDuration)//Automation process 
        {
            // Set up form properties
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowInTaskbar = false;
            this.Size = new Size(500, 200);
            this.Text = "";
            this.BackColor = Color.White;

            // Create and configure message label
            Label label = new Label();
            label.Text = message;
            label.AutoSize = false;
            label.Size = new Size(this.ClientSize.Width - 10, this.ClientSize.Height - 10); // Adjust size for padding
            label.Location = new Point(5, 5); // Adjust location for padding
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Font = new Font("Arial", 12, FontStyle.Regular); // Set font and size

            // Add label to form
            this.Controls.Add(label);

            // Set up timer to close the form after displayDuration milliseconds
            Timer timer = new Timer();
            timer.Interval = displayDuration;
            timer.Tick += (sender, e) => this.Close();
            timer.Start();
        }
        private async Task GetservicesAsync(int selectedBatchId)//Mapping from CTServices to HCCServices
        {
            lblStatus.Text = "Abort";
            try
            {
                // Check if a batch has been selected
                if (selectedBatchId >= 0)
                {
                    int selectedRowIndex = dataGridView.SelectedRows[0].Index;
                    selectedBatchId = Convert.ToInt32(dataGridView.Rows[selectedRowIndex].Cells["BatchID"].Value.ToString());
                    int allTotalRows = GetTotalForBatch(selectedBatchId);
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand("MapDlServicesToHCCServices", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            // Pass the selected BatchID to the stored procedure
                            command.Parameters.AddWithValue("@BatchID", selectedBatchId);

                            // Get the total number of rows to be inserted
                            int totalRows = GetTotalRowsForBatchservices(selectedBatchId);

                            // Initialize progress variables
                            int insertedRows = 0;

                            string baseFilename = Constants.CtClients;
                            dbHelper.Log($"Convert to HCC  for batch ID Started.", Constants.Hcc, baseFilename, Constants.Uploadhcc);

                            DateTime startTime = DateTime.Now;
                            txtUploadStarted.Text = startTime.ToString("MM/dd/yyyy HH:mm:ss");
                            // Update progress textbox with initial progress information
                            await UpdateProgressAsyncservices(insertedRows, totalRows);

                            // Set up progress bar
                            progressBarServices.Maximum = totalRows;

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (insertedRows < totalRows)
                                {
                                    // Process each row
                                    insertedRows++;

                                    // Update progress bar and text box
                                    await UpdateProgressAsyncservices(insertedRows, totalRows);

                                    // You may want to do additional processing here if needed
                                }
                            }
                            DateTime endTime = DateTime.Now;
                            UpdateGridStatus(selectedBatchId, Constants.Hccendcon);//Update the Status label in batch table

                            dbHelper.Log($"Convert to HCC format completed successfully.", Constants.ClientTrack, baseFilename, Constants.Uploadct);

                            Console.WriteLine("Mapping completed successfully for BatchID: " + selectedBatchId);
                            RemoveSelectedRow(selectedBatchId, "");
                            int batchId = dbHelper.GetNextBatchId();//Getting next batchid from Batch table
                            UpdateBatch(batchId, startTime, endTime, allTotalRows);
                            dataGridView.Rows[selectedRowIndex].Cells["Status"].Value = 18;
                            dataGridView.Rows[selectedRowIndex].Cells["ConversionEndedAt"].Value = endTime;
                            DateTime endedTime = DateTime.Now;
                            TimeSpan totalTime = endTime - startTime; // Calculate total time taken
                            string eTime = endedTime.ToString("MM/dd/yyyy HH:mm:ss");
                            double totalSeconds = totalTime.TotalSeconds;
                            txtUploadEnded.Text = eTime;
                            txtTotaltime.Text = $"{totalSeconds:F2} Seconds";
                            btncloseHCC.Text = Constants.Close;
                            PopulateDataGridView();
                            dataGridView.Refresh();
                            btncthcc.Enabled = true;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Please select a batch before starting the conversion.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        private int GetTotalForBatch(int batchId)//getting total rows from particular tables
        {
            int totalRows = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(
                        @"SELECT 
                    
                    (SELECT COUNT(*) FROM RWDE.dbo.CTClients WHERE BatchID = @BatchID) +
                    (SELECT COUNT(*) FROM RWDE.dbo.CTClientsEligibilityDoc WHERE BatchID = @BatchID) +
                    (SELECT COUNT(*) FROM RWDE.dbo.CTServices WHERE BatchID = @BatchID)AS TotalCount", connection))
                    {
                        command.Parameters.AddWithValue("@BatchID", batchId);
                        totalRows = (int)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return totalRows;
        }
        private void PopulateDataGridView()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("Conversion", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);

                    //Bind the DataTable to the DataGridView
                    dataGridView.AutoGenerateColumns = false; // Prevent auto-generation of columns
                    dataGridView.Columns.Clear(); // Clear existing columns

                    // Define DataGridView columns and map them to DataTable columns
                    dataGridView.Columns.Add("BatchID", "Batch ID");
                    dataGridView.Columns["BatchID"].DataPropertyName = "BatchID";
                    dataGridView.Columns.Add("Type", "Batch Type");
                    dataGridView.Columns["Type"].DataPropertyName = "Type";
                    dataGridView.Columns.Add("Description", "Description");
                    dataGridView.Columns["Description"].DataPropertyName = "Description";
                    dataGridView.Columns.Add("FileName", "File Name");
                    dataGridView.Columns["FileName"].DataPropertyName = "FileName";
                    AddDateTime("UploadStartedAt", "Upload Started At", dataGridView);
                    AddDateTime("UploadEndedAt", "Upload Ended At", dataGridView);
                    AddDateTime("ConversionStartedAt", "Conversion Started At", dataGridView);
                    AddDateTime("ConversionEndedAt", "Conversion Ended At", dataGridView);
                    dataGridView.Columns.Add("Status", "Status");
                    dataGridView.Columns["Status"].DataPropertyName = "Status";
                    // Bind the DataTable to the DataGridView
                    dataGridView.DataSource = dataTable;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(Constants.ErrorCode, ex.Message);
            }
        }
        private void UpdateBatch(int batchId, DateTime startTime, DateTime endTime, int allTotalRows)
        {//Updating status and Time on Batch Table     
            try
            {
                allTotalRows++;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // Define the SQL query to update the ConversionStartedAt and ConversionEndedAt columns
                    string updateQuery = @"
                UPDATE [RWDE].[dbo].[Batch]
                SET [ConversionStartedAt] = @ConversionStartedAt,
                    [ConversionEndedAt] = @ConversionEndedAt,
                    [SuccessfulRows] = @AllTotalRows,
                    [Status] = '18'
                WHERE [BatchID] = @BatchID AND [Status] = 11 OR [Status]=17 OR [Status] = 19";

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        // Set the parameters
                        command.Parameters.AddWithValue("@BatchID", batchId);
                        command.Parameters.AddWithValue("@ConversionStartedAt", startTime);
                        command.Parameters.AddWithValue("@ConversionEndedAt", endTime);
                        command.Parameters.AddWithValue("@AllTotalRows", allTotalRows-1);

                        // Execute the SQL update command
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating batch: {ex.Message}");
                // Log or handle the exception appropriately
            }
        }
        private async Task UpdateProgressAsyncservices(int insertedRows, int totalRows)//Services Progress bar
        {
            try
            {
                // Calculate progress percentage
                int progressPercentage = (int)((double)insertedRows / totalRows * 100);

                // Construct the progress information string in the desired format
                string progressInfo = $"{insertedRows}/{totalRows} ({progressPercentage}%)";

                // Update the progress information in the textbox without appending new line
                txtProgressServices.Text = progressInfo;

                // Update the progress bar
                progressBarServices.Value = insertedRows;

                // Introduce a delay to slow down the progress display
                await Task.Delay(1); // Adjust the delay time as needed
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        private int GetTotalRowsForBatchservices(int batchId)//Getting total rows from required table
        {
            int totalRows = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM RWDE.dbo.CTServices WHERE BatchID = @BatchID", connection))
                    {
                        command.Parameters.AddWithValue("@BatchID", batchId);
                        totalRows = (int)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return totalRows;
        }
        private void RemoveSelectedRow(int batchId, string status)// // Method to remove a selected row and store the removed batch ID in the database table
        {
            try
            {
                // Find and remove the row corresponding to the selected batch ID in the DataGridView
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.Cells["BatchID"].Value != null && Convert.ToInt32(row.Cells["BatchID"].Value) == batchId)
                    {
                        dataGridView.Rows.Remove(row);
                        lblStatus.Text = status;
                        break;
                    }
                }
                // Add the removed batch ID to the database table
                AddRemovedBatchIdToDatabase(batchId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        private void AddRemovedBatchIdToDatabase(int batchId)// // Method to add a removed batch ID to the database table
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO RemovedBatchIDs (BatchID) VALUES (@BatchID)", conn);
                    cmd.Parameters.AddWithValue("@BatchID", batchId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding removed batch ID to database: {ex.Message}");
            }
        }
        private void UpdateGridStatus(int batchId, int status)// Updates the status label on the form based on a given status code retrieved from the database.
        {
            try
            {
                string valueSelectQuery = "SELECT Value FROM List WHERE ListsID = @ListsID";
                using (SqlConnection sql = new SqlConnection(connectionString))
                {
                    using (SqlCommand com = new SqlCommand(valueSelectQuery, sql))
                    {
                        com.Parameters.AddWithValue("@ListsID", status);

                        sql.Open();

                        var result = com.ExecuteScalar();

                        if (result != null)
                        {
                            lblStatus.Text = result.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                Console.WriteLine($"Error updating grid status: {ex.Message}");
            }
        }
        private async Task UpdateProgressAsync(int insertedRows, int totalRows)//progress of rows
        {
            try
            {
                // Calculate progress percentage
                int progressPercentage = (int)((double)insertedRows / totalRows * 100);

                // Construct the progress information string in the desired format
                string progressInfo = $"{insertedRows}/{totalRows} ({progressPercentage}%)";

                // Update the progress information in the textbox without appending new lines
                txtProgresshcc.Text = progressInfo;

                // Update the progress bar
                progressBarClients.Value = insertedRows;

                // Introduce a delay to slow down the progress display
                await Task.Delay(1); // Adjust the delay time as needed
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        private int GetTotalRowsForBatch(int batchId)//getting total rows from particular tables
        {
            int totalRows = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(
                        @"SELECT 
                    
                    (SELECT COUNT(*) FROM RWDE.dbo.CMSClients WHERE BatchID = @BatchID) AS TotalCount",
                        connection))
                    {
                        command.Parameters.AddWithValue("@BatchID", batchId);
                        totalRows = (int)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return totalRows;
        }
        private void btncloseHCC_Click(object sender, EventArgs e)//Closing form
        {
            int batchId = dbHelper.GetNextBatchId();
            try
            {
                if (btncloseHCC.Text=="Close")
                {
                    this.Close();
                    Application.Restart();
                    return;
                }
                if (btncloseHCC.Text == "Abort")
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to abort?", "Abort Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    // Check if the user clicked "Yes"

                    // Show a message box indicating successful abort
                    MessageBox.Show(Constants.Abortedsuccessfully);
                    foreach (DataGridViewRow row in dataGridView.Rows)
                    {
                        row.Cells["Status"].Value = Constants.Hccabort;

                        break;
                    }
                    // Update the status of the selected batch to Status "19" (Abort)
                    UpdateBatchStatus(batchId, Constants.Hccabort);
                    // Application.Exit();
                }
                this.Close();
                Application.Restart();
            }
            catch (Exception ex)
            {
                // Display or log the exception message
                MessageBox.Show(Constants.ErrorCode, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateBatchStatus(int selectedBatchId, int status)//Updating Batch Status
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    ClearTables(selectedBatchId);
                    // Construct the SQL UPDATE statement
                    string query = "UPDATE [RWDE].[dbo].[Batch] " +
                            "SET [Status] = @Status, " +
                            "[SuccessfulRows] = 0 " +
                            "WHERE [BatchID] = @BatchID";
                    // Create and execute the SqlCommand
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to the command
                        command.Parameters.AddWithValue("@Status", status);
                        command.Parameters.AddWithValue("@BatchID", selectedBatchId);

                        // Execute the update query
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if any rows were affected
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(Constants.Errorupdatingbatchstatus, ex.Message);
            }
        }
        private void ClearTables(int batchId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("abortconversiondelete", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BatchId", batchId);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error clearing tables: {ex.Message}");
                // Log or handle the exception appropriately
                throw; // Re-throw if you want to handle it in the calling method
            }
        }

        private void RefreshValues()
        {
            txtBatchid.Clear();
            txtTotaltime.Clear();
            txtUploadStarted.Clear();
            txtUploadEnded.Clear();
            txtProgresshcc.Text = "0%";
            progressBarClients.Value = 0;
            txtProgressServices.Text = "0%";
            progressBarServices.Value = 0;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string batchType = txtBatchtype.Text;
                DateTime fromDate = dtpStartDate.Value.Date;
                DateTime endDate = dtpEndDate.Value.Date;

                // Validate inputs
                if (string.IsNullOrEmpty(batchType))
                {
                    MessageBox.Show(Constants.EmptyvalueMessage, Constants.FilterTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (endDate < fromDate)
                {
                    MessageBox.Show(Constants.DateShouldBeGreaterThen, Constants.FilterTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Retrieve and bind data
                DataTable result = dbHelper.GetParticularConversionDatas(batchType, fromDate, endDate);
                dataGridView.DataSource = result;

                if (result == null || result.Rows.Count == 0)
                {
                    MessageBox.Show(Constants.NoFilterDatas, Constants.FilterTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                // Log the exception or display an error message
                MessageBox.Show($"An error occurred: {ex.Message}", Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bnClear_Click(object sender, EventArgs e)
        {
            try
            {
                //txtBatchtype.Items.Clear();
                PopulateDataGridView();
                List<string> batchTypes = dbHelper.GetAllBatchTypes();
               // txtBatchtype.Items.Clear();  // Clear existing items
                foreach (string batchType in batchTypes)
                {
                    //txtBatchtype.Items.Add(batchType);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            dtpStartDate.CustomFormat = "MM-dd-yyyy";
            dtpStartDate.Format = DateTimePickerFormat.Custom;
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            dtpEndDate.CustomFormat = "MM-dd-yyyy";
            dtpEndDate.Format = DateTimePickerFormat.Custom;
        }
    }
}