using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
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
        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)// Formats the cell value for the Constants.Status column based on the corresponding value in the database.
        {
            try
            {
                if (dataGridView.Columns[e.ColumnIndex].Name == Constants.Status)
                {
                    string statusValue = e.Value?.ToString();
                    if (!string.IsNullOrEmpty(statusValue))
                    {
                        string valueSelectQuery = Constants.SelectValuefromListwhereListsId;
                        using (SqlConnection sql = new SqlConnection(connectionString))
                        {
                            using (SqlCommand com = new SqlCommand(valueSelectQuery, sql))
                            {
                                com.Parameters.AddWithValue(Constants.AtListsId, statusValue);
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
            {            // Check if the Constants.BatchId column exists before setting its width
                if (dataGridView.Columns.Contains(Constants.BatchId))
                {
                    dataGridView.Columns[Constants.BatchId].Width = 205;
                    // Set the width to 200 pixels
                }
                if (dataGridView.Columns.Contains(Constants.Type))
                {
                    dataGridView.Columns[Constants.Type].Width = 160; // Set the width to 200 pixels
                }
                if (dataGridView.Columns.Contains(Constants.FileName))
                {
                    dataGridView.Columns[Constants.FileName].Width = 205; // Set the width to 200 pixels
                }
                if (dataGridView.Columns.Contains(Constants.Description))
                {
                    dataGridView.Columns[Constants.Description].Width = 205; // Set the width to 200 pixels
                }
                if (dataGridView.Columns.Contains(Constants.UploadStartedAt))
                {
                    dataGridView.Columns[Constants.UploadStartedAt].Width = 205; // Set the width to 200 pixels
                }
                if (dataGridView.Columns.Contains(Constants.UploadEndedAt))
                {
                    dataGridView.Columns[Constants.UploadEndedAt].Width = 205; // Set the width to 200 pixels
                }
                if (dataGridView.Columns.Contains(Constants.ConversionStartedAt))
                {
                    dataGridView.Columns[Constants.ConversionStartedAt].Width = 250; // Set the width to 200 pixels
                }
                if (dataGridView.Columns.Contains(Constants.ConversionEndedAt))
                {
                    dataGridView.Columns[Constants.ConversionEndedAt].Width = 250; // Set the width to 200 pixels
                }

                if (dataGridView.Columns.Contains(Constants.Status))
                {
                    dataGridView.Columns[Constants.Status].Width = 205; // Set the width to 200 pixels
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(Constants.Errorsp + ex.Message);
            }
        }
        private void AddDateTime(string name, string value, DataGridView dataGridView)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn
            {
                Name = name,
                DataPropertyName = name,
                HeaderText = value,
                DefaultCellStyle = new DataGridViewCellStyle { Format = Constants.MMddyyyyHHmmss }
            };
            dataGridView.Columns.Add(column);
        }
        private void UpdateBatchStatus(int batchId, int status, DateTime timestamp)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                using (SqlCommand command = new SqlCommand(Constants.UpdateBatchStatusQuery, connection))
                {
                    command.Parameters.AddWithValue(Constants.AtStatus, status);
                    command.Parameters.AddWithValue(Constants.AtTimestamp, timestamp);
                    command.Parameters.AddWithValue(Constants.AtBatchid, batchId);
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
                int selectedBatchId = Convert.ToInt32(dataGridView.Rows[selectedRowIndex].Cells[Constants.BatchId].Value.ToString());
                bool batchExists = false;
                DateTime? conversionStartedAt = null;
                DateTime? conversionEndedAt = null;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(Constants.GetConversionTimeQuery, connection))
                    {
                        command.Parameters.AddWithValue(Constants.AtBatchid, selectedBatchId);

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
                    MessageBox.Show($@"{Constants.BatchIdHeader} {selectedBatchId} {Constants.Hasalreadycompletedtheconversion}", Constants.OchinToHccConversion, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btncthcc.Enabled = true;
                    btncloseHCC.Text = Constants.Close;
                    return;
                }
                UpdateGridStatus(selectedBatchId, Constants.Hccstartcon);//update the Status label in Status Column
                dataGridView.Refresh();
                string baseFilename = Constants.ServiceCttohcc;
                dbHelper.Log(Constants.ConverttoHcCforbatchIdStarted, Constants.ClientTrack, baseFilename, Constants.Uploadct);

                DateTime startTime = DateTime.Now;
                dataGridView.Rows[selectedRowIndex].Cells[Constants.Status].Value = 17;
                dataGridView.Rows[selectedRowIndex].Cells[Constants.ConversionStartedAt].Value = startTime;
                UpdateBatchStatus(selectedBatchId, 17, startTime);
                txtBatchid.Text = selectedBatchId.ToString();
                txtUploadStarted.Text = startTime.ToString(Constants.MMddyyyyHHmmssbkslash);// Record the start time

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(Constants.MapCmsClientstest, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Pass the selected BatchID to the stored procedure
                        command.Parameters.AddWithValue(Constants.AtBatchid, selectedBatchId);

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
                Console.WriteLine(Constants.Errorsp + ex.Message);
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
            label.Font = new Font(Constants.FntfmlyArial, 12, FontStyle.Regular); // Set font and size

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
            lblStatus.Text = Constants.Abort;
            try
            {
                // Check if a batch has been selected
                if (selectedBatchId >= 0)
                {
                    int selectedRowIndex = dataGridView.SelectedRows[0].Index;
                    selectedBatchId = Convert.ToInt32(dataGridView.Rows[selectedRowIndex].Cells[Constants.BatchId].Value.ToString());
                    int allTotalRows = GetTotalForBatch(selectedBatchId);
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand(Constants.MapDlServicesToHccServices, connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            // Pass the selected BatchID to the stored procedure
                            command.Parameters.AddWithValue(Constants.AtBatchid, selectedBatchId);

                            // Get the total number of rows to be inserted
                            int totalRows = GetTotalRowsForBatchservices(selectedBatchId);

                            // Initialize progress variables
                            int insertedRows = 0;

                            string baseFilename = Constants.CtClients;
                            dbHelper.Log(Constants.ConverttoHcCforbatchIdStarted, Constants.Hcc, baseFilename, Constants.Uploadhcc);

                            DateTime startTime = DateTime.Now;
                            txtUploadStarted.Text = startTime.ToString(Constants.MMddyyyyHHmmssbkslash);
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

                            dbHelper.Log(Constants.ConverttoHcCformatcompletedsuccessfully, Constants.ClientTrack, baseFilename, Constants.Uploadct);

                            Console.WriteLine( Constants.MappingcompletedsuccessfullyforBatchId + selectedBatchId);
                            RemoveSelectedRow(selectedBatchId, "");
                            int batchId = dbHelper.GetNextBatchId();//Getting next batchid from Batch table
                            UpdateBatch(batchId, startTime, endTime, allTotalRows);
                            dataGridView.Rows[selectedRowIndex].Cells[Constants.Status].Value = 18;
                            dataGridView.Rows[selectedRowIndex].Cells[Constants.ConversionEndedAt].Value = endTime;
                            DateTime endedTime = DateTime.Now;
                            TimeSpan totalTime = endTime - startTime; // Calculate total time taken
                            string eTime = endedTime.ToString(Constants.MMddyyyyHHmmssbkslash);
                            double totalSeconds = totalTime.TotalSeconds;
                            txtUploadEnded.Text = eTime;
                            txtTotaltime.Text = $@"{totalSeconds:F2} {Constants.Seconds}";
                            btncloseHCC.Text = Constants.Close;
                            PopulateDataGridView();
                            dataGridView.Refresh();
                            btncthcc.Enabled = true;
                        }
                    }
                }
                else
                {
                    Console.WriteLine(Constants.Pleaseselectabatchbeforestartingtheconversion);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(Constants.Errorsp + ex.Message);
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
                    using (SqlCommand command = new SqlCommand(Constants.GetTotalRowsQuery
                       , connection))
                    {
                        command.Parameters.AddWithValue(Constants.AtBatchid, batchId);
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
                    SqlCommand command = new SqlCommand(Constants.Conversion, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);

                    //Bind the DataTable to the DataGridView
                    dataGridView.AutoGenerateColumns = false; // Prevent auto-generation of columns
                    dataGridView.Columns.Clear(); // Clear existing columns

                    // Define DataGridView columns and map them to DataTable columns
                    dataGridView.Columns.Add(Constants.BatchId, Constants.BatchIdHeader);
                    dataGridView.Columns[Constants.BatchId].DataPropertyName = Constants.BatchId;
                    dataGridView.Columns.Add(Constants.Type, Constants.BatchTypeHeader);
                    dataGridView.Columns[Constants.Type].DataPropertyName = Constants.Type;
                    dataGridView.Columns.Add(Constants.Description, Constants.Description);
                    dataGridView.Columns[Constants.Description].DataPropertyName = Constants.Description;
                    dataGridView.Columns.Add(Constants.FileName, Constants.FileNamesp);
                    dataGridView.Columns[Constants.FileName].DataPropertyName = Constants.FileName;
                    AddDateTime(Constants.UploadStartedAt, Constants.UploadStartedAtHeader, dataGridView);
                    AddDateTime(Constants.UploadEndedAt, Constants.UploadEndedAtHeader, dataGridView);
                    AddDateTime(Constants.ConversionStartedAt, Constants.ConversionStartedAtHeader, dataGridView);
                    AddDateTime(Constants.ConversionEndedAt, Constants.ConversionEndedAtHeader, dataGridView);
                    dataGridView.Columns.Add(Constants.Status, Constants.Status);
                    dataGridView.Columns[Constants.Status].DataPropertyName = Constants.Status;
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
                    string updateQuery = $"@{ Constants.UpdateBatchConversionTimeQuery}";

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        // Set the parameters
                        command.Parameters.AddWithValue(Constants.AtBatchid, batchId);
                        command.Parameters.AddWithValue(Constants.AtConversionStartedAt, startTime);
                        command.Parameters.AddWithValue(Constants.AtConversionEndedAt, endTime);
                        command.Parameters.AddWithValue(Constants.AtAllTotalRows, allTotalRows-1);

                        // Execute the SQL update command
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($@"{Constants.Errorupdatingbatch}{ex.Message}");
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
                Console.WriteLine(Constants.Errorsp + ex.Message);
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

                    using (SqlCommand command = new SqlCommand(Constants.GetTotalRowsForBatchservicesQuery, connection))
                    {
                        command.Parameters.AddWithValue(Constants.AtBatchid, batchId);
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
                    if (row.Cells[Constants.BatchId].Value != null && Convert.ToInt32(row.Cells[Constants.BatchId].Value) == batchId)
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
                Console.WriteLine(Constants.Errorsp + ex.Message);
            }
        }
        private void AddRemovedBatchIdToDatabase(int batchId)// // Method to add a removed batch ID to the database table
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(Constants.AddRemovedBatchIdToDatabaseQuery, conn);
                    cmd.Parameters.AddWithValue(Constants.AtBatchid, batchId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($@"{Constants.ErroraddingremovedbatchIDtodatabase} {ex.Message}");
            }
        }
        private void UpdateGridStatus(int batchId, int status)// Updates the status label on the form based on a given status code retrieved from the database.
        {
            try
            {
                string valueSelectQuery = Constants.UpdateGridStatusQuery;
                using (SqlConnection sql = new SqlConnection(connectionString))
                {
                    using (SqlCommand com = new SqlCommand(valueSelectQuery, sql))
                    {
                        com.Parameters.AddWithValue(Constants.AtListsId, status);

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
                Console.WriteLine($@"{Constants.ErrorUpdatingGridStatus}{ex.Message}");
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
                Console.WriteLine(Constants.Errorsp + ex.Message);
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
                    using (SqlCommand command = new SqlCommand($"@{Constants.GetTotalRowsForBatchQuery}", connection))
                    {
                        command.Parameters.AddWithValue(Constants.AtBatchid, batchId);
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
                if (btncloseHCC.Text==Constants.Close)
                {
                    this.Close();
                    Application.Restart();
                    return;
                }
                if (btncloseHCC.Text == Constants.Abort)
                {
                    DialogResult result = MessageBox.Show(Constants.Areyousureyouwanttoabort, Constants.AbortConfirmation, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    // Check if the user clicked "Yes"

                    // Show a message box indicating successful abort
                    MessageBox.Show(Constants.Abortedsuccessfully);
                    foreach (DataGridViewRow row in dataGridView.Rows)
                    {
                        row.Cells[Constants.Status].Value = Constants.Hccabort;

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
                    string query =Constants.UpdateBatchStatus;
                    // Create and execute the SqlCommand
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to the command
                        command.Parameters.AddWithValue(Constants.AtStatus, status);
                        command.Parameters.AddWithValue(Constants.AtBatchid, selectedBatchId);

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
                    using (SqlCommand command = new SqlCommand(Constants.Abortconversiondelete, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(Constants.AtBatchid, batchId);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($@"{Constants.ErrorClearingtables}{ex.Message}");
                throw; // Re-throw if you want to handle it in the calling method
            }
        }

        private void RefreshValues()
        {
            txtBatchid.Clear();
            txtTotaltime.Clear();
            txtUploadStarted.Clear();
            txtUploadEnded.Clear();
            txtProgresshcc.Text = Constants.ZeroPercent;
            progressBarClients.Value = 0;
            txtProgressServices.Text = Constants.ZeroPercent;
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
                MessageBox.Show($@"{Constants.AnErrorOccurred}{ex.Message}", Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show($@"{Constants.AnErrorOccurred}{ex.Message}", Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            dtpStartDate.CustomFormat = Constants.DateFormatMMddyyyy;
            dtpStartDate.Format = DateTimePickerFormat.Custom;
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            dtpEndDate.CustomFormat = Constants.DateFormatMMddyyyy;
            dtpEndDate.Format = DateTimePickerFormat.Custom;
        }
    }
}