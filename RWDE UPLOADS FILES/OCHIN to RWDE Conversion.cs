using DocumentFormat.OpenXml.ExtendedProperties;
using Rwde;
using RWDE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RWDE_UPLOADS_FILES
{
    public partial class OCHIN_to_RWDE_Conversion : Form
    {
        private readonly string connectionString;
        private readonly DBHelper dbHelper;
        private List<int> removedBatchIDs = new List<int>();
        public Panel PanelToReplace
        {

            get
            {
                return pnlOCHINConversion;
            }
        }
        public OCHIN_to_RWDE_Conversion()
        {
            InitializeComponent();
            dbHelper = new DBHelper();
            connectionString = dbHelper.GetConnectionString();
            this.ControlBox = false;
            PopulateDataGridView();
            PopulateDataGridViewHCC();
            this.WindowState = FormWindowState.Maximized;
            List<string> batchTypes = dbHelper.GetAllBatchTypesHCC();
            cbBatchType.Items.Clear();  // Clear existing items
            foreach (string batchType in batchTypes)
            {
                cbBatchType.Items.Add(batchType);
            }
            dataGridView.CellFormatting += dataGridView_CellFormatting;
            dataGridViewHCC.CellFormatting += dataGridView_CellFormatting;
            dataGridView.DataBindingComplete += DataGridView_DataBindingComplete;
            dtpStartDate.Value = DateTime.Now.AddYears(-1);
            dtpStartDate.CustomFormat = "MM-dd-yyyy";
            dtpEndDate.CustomFormat = "MM-dd-yyyy";
            // Assuming you have another DateTimePicker for the End Date
            dtpEndDate.Value = DateTime.Now;
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
                if (control is System.Windows.Forms.Button || control is CheckBox || control is DateTimePicker ||control is ComboBox)
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
                if (dataGridViewHCC.Columns[e.ColumnIndex].Name == Constants.StatusHeader)
                {
                    string statusValue = e.Value?.ToString();
                    if (!string.IsNullOrEmpty(statusValue))
                    {
                        string valueSelectQuery = "listconversion";
                        using (SqlConnection sql = new SqlConnection(connectionString))
                        {
                            using (SqlCommand com = new SqlCommand(valueSelectQuery, sql))
                            {
                                com.CommandType = CommandType.StoredProcedure;
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
                if (dataGridViewHCC.Columns.Contains("BatchID"))
                {
                    dataGridViewHCC.Columns["BatchID"].Width = 205;
                    // Set the width to 200 pixels
                }
                if (dataGridViewHCC.Columns.Contains("Type"))
                {
                    dataGridViewHCC.Columns["Type"].Width = 160; // Set the width to 200 pixels
                }
                if (dataGridViewHCC.Columns.Contains("FileName"))
                {
                    dataGridViewHCC.Columns["FileName"].Width = 205; // Set the width to 200 pixels
                }
                if (dataGridViewHCC.Columns.Contains("Description"))
                {
                    dataGridViewHCC.Columns["Description"].Width = 205; // Set the width to 200 pixels
                }
                if (dataGridViewHCC.Columns.Contains("UploadStartedAt"))
                {
                    dataGridViewHCC.Columns["UploadStartedAt"].Width = 205; // Set the width to 200 pixels
                }
                if (dataGridViewHCC.Columns.Contains("UploadEndedAt"))
                {
                    dataGridViewHCC.Columns["UploadEndedAt"].Width = 205; // Set the width to 200 pixels
                }
                if (dataGridViewHCC.Columns.Contains("ConversionStartedAt"))
                {
                    dataGridViewHCC.Columns["ConversionStartedAt"].Width = 250; // Set the width to 200 pixels
                }
                if (dataGridViewHCC.Columns.Contains("ConversionEndedAt"))
                {
                    dataGridViewHCC.Columns["ConversionEndedAt"].Width = 250; // Set the width to 200 pixels
                }

                if (dataGridViewHCC.Columns.Contains("Status"))
                {
                    dataGridViewHCC.Columns["Status"].Width = 205; // Set the width to 200 pixels
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        private void btncloseHCC_Click_1(object sender, EventArgs e)
        {
            try
            {
                int batchId = dbHelper.GetNextBatchID();
               
                if (btncloseHCC.Text == Constants.abort)
                {
                    DialogResult result = MessageBox.Show(Constants.Areyousureyouwanttoabort, "Abort Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {                           // Prompt the user with a confirmation message

                        progressbarHcc.Value = 0;
                        progressBarServices.Value = 0;
                        txtProgresshcc.Text = "0%";
                        txtProgressServices.Text = "0%";
                        foreach (DataGridViewRow row in dataGridView.Rows)
                        {
                            row.Cells["Status"].Value = Constants.xmlabort;
                            break; // Abort after updating the first row
                        }
                        DialogResult resultmsg = MessageBox.Show(Constants.Abortedsuccessfully, Constants.GenerateXML);

                        // Show a message box indicating successful abort
                        MessageBox.Show(Constants.Abortedsuccessfully);

                        // Update the status of the selected batch to Status "19" (Abort)
                        UpdateBatchStatusabort(batchId, 19);
                        this.Close();
                    }
                    this.Close();
                    

                }

                this.Close();
                System.Windows.Forms.Application.Restart();
            }
           
  
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            }

                private void UpdateBatchStatusabort(int batchId, int status)//for abort status 
                {
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            // Construct the SQL UPDATE statement
                            string query = "countxmlrows";
                    dbHelper.DeleteHCCABORTED(batchId);
                    // Create and execute the SqlCommand
                    using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                // Add parameters to the command
                                command.Parameters.AddWithValue("@Status", status);
                                command.Parameters.AddWithValue("@BatchID", batchId);

                                // Execute the update query
                                int rowsAffected = command.ExecuteNonQuery();

                                // Check if any rows were affected (optional)
                                if (rowsAffected > 0)
                                {
                                    Console.WriteLine(Constants.Batchstatusupdatedsuccessfully);
                                }
                                else
                                {
                                    Console.WriteLine(Constants.NobatchwasfoundwiththegivenID);
                                }
                            }
                        }

                      // Delete XML files in the specified directory
                              
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(Constants.Errorupdatingbatchstatus, ex.Message);
                        // Log or handle the exception appropriately
                    }
                }


                // System.Windows.Forms.Application.Restart();

            
          


        public void PopulateDataGridView(DataTable dt)///Populate values in the database
        {
            try
            {
                //string query = "Conversion";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("Conversionochin", connection);
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
                    // Now, remove the rows corresponding to removed batch IDs from the DataGridView

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(Constants.Error, ex.Message);
            }
        }
        private void AddDateTime(string name, string value, DataGridView dataGridView)//format datetime 
        {
            try { 
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn
            {
                Name = name,
                DataPropertyName = name,
                HeaderText = value,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "MM-dd-yyyy HH:mm:ss" }
            };
            dataGridView.Columns.Add(column);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
        }
        private void UpdateBatchStatus(int batchId, int status, DateTime timestamp)//update date information
        {
            try { 
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                using (SqlCommand command = new SqlCommand("updatestatus", connection))//UPDATE STATUS
                {
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@Timestamp", timestamp);
                    command.Parameters.AddWithValue("@BatchID", batchId);
                    command.ExecuteNonQuery();
                    ClearTables(selectedBatchID);//to clear data
                }
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
               
            }
        }
        public Panel GetPanelToReplace()
        {
            // Return the panel you want to replace
            return pnlOCHINConversion;//
        }
        private async void btncthcc_Click_1(object sender, EventArgs e)//Insertion of Client and Eligibility into HCC tables
        {
            try {

            //    if (dataGridView.Rows.Count > 0)
            //    {
            //        MessageBox.Show("The generation process for HCC data is still in progress. Please wait until it's completed.",
            //                        Constants.ochintorwdeconversion,
            //                        MessageBoxButtons.OK,
            //                        MessageBoxIcon.Warning);

            //        return;
            //    }

                if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show(Constants.PleaseselectarowwithaBatchIDtoproceed,Constants.ochintorwdeconversion, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method early if no row is selected
            }
            int selectedRowIndex = dataGridView.SelectedRows[0].Index;
            int selectedBatchID = Convert.ToInt32(dataGridView.Rows[selectedRowIndex].Cells["BatchID"].Value.ToString());
            string fileName = dataGridView.Rows[selectedRowIndex].Cells["fileName"].Value.ToString();

            if (fileName.Contains("Client"))
            {
                
                _ = GetclientssAsync(selectedBatchID);//to get client data mapping
            }

            if (fileName.Contains("Service"))
            {
                _ = GetservicesAsync(selectedBatchID);//to get service data mapping

            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
               
            }
        }
        public async Task GetclientssAsync(int selectedBatchID)//Insertion of Client and Eligibility into HCC tables
        {

            progressBarServices.Value = 0; 
            var batchDetails = await dbHelper.GetBatchDetailsFromSPAsyncclients(selectedBatchID);//to check whether the conversion completed or not

            if (batchDetails == null)
            {
                Console.WriteLine(Constants.Batchnotfound);
                return;
            }

            // Check if ConversionStartedAt and ConversionEndedAt are not null
            if (batchDetails.ConversionStartedAt != null && batchDetails.ConversionEndedAt != null)
            {
                MessageBox.Show(Constants.Conversionhasalreadybeencompletedforthisbatch,Constants.ochintorwdeconversion, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProgresshcc.Text = "0%";
                txtProgressServices.Text = "0%";
                txtBatchid.Text = null;
                txtUploadStarted.Text = null;
                txtUploadEnded.Text = null;
                txtTotaltime.Text = null;
                   
                return;
            }
            btncloseHCC.Text = Constants.abort;
            btnochintorwde.Enabled = false;
            int batchid = dbHelper.GetNextBatchID();
            RefreshValues();
            try
            {
                int selectedRowCount = dataGridView.SelectedRows.Count;
                // Check if no batch is selected
                if (selectedRowCount == 0)
                {
                    MessageBox.Show(Constants.Pleaseselectabatchbeforestartingtheconversion, Constants.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btncloseHCC.Text = Constants.close;
                    btnochintorwde.Enabled = true;
                    return; // Exit the method early
                }

                // Check if more than one batch is selected
                if (selectedRowCount != 1)
                {
                    MessageBox.Show(Constants.Pleaseselectonlyonebatchatatime, Constants.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Exit the method earl iy
                }
                int selectedRowIndex = dataGridView.SelectedRows[0].Index;
               // int selectedBatchID = Convert.ToInt32(dataGridView.Rows[selectedRowIndex].Cells["BatchID"].Value.ToString());
                bool batchExists = false;
                DateTime? conversionStartedAt = null;
                DateTime? conversionEndedAt = null;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SELECT ConversionStartedAt, ConversionEndedAt FROM Batch WHERE BatchID = @BatchID", connection))
                    {
                        command.Parameters.AddWithValue("@BatchID", selectedBatchID);

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
                    DialogResult result = MessageBox.Show($"Batch ID {selectedBatchID} has already completed the conversion.", "OCHIN To HCC Conversion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnochintorwde.Enabled = true;
                    //btncloseHCC.Text = "Close";
                    return;
                }
                UpdateGridStatus(selectedBatchID, Constants.HCCSTARTCON);//update the Status label in Status Column
                dataGridView.Refresh();
                string baseFilename = Constants.ServiceCTTOHCC;
                dbHelper.Log(Constants.ConverttoHCCforbatchIDStarted, Constants.ClientTrack, baseFilename, Constants.uploadct);

                DateTime startTime = DateTime.Now;
                dataGridView.Rows[selectedRowIndex].Cells["Status"].Value = 17;
                dataGridView.Rows[selectedRowIndex].Cells["ConversionStartedAt"].Value = startTime;
              
                txtBatchid.Text = selectedBatchID.ToString();
                txtUploadStarted.Text = startTime.ToString("MM/dd/yyyy HH:mm:ss");// Record the start time

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("MapCMSClients", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Pass the selected BatchID to the stored procedure
                        command.Parameters.AddWithValue("@BatchID", selectedBatchID);

                        // Get the total number of rows to be inserted
                        int totalRows = GetTotalRowsForBatch(selectedBatchID);

                        // Initialize progress variables
                        int insertedRows = 0;
                        //string baseFilename = Path.GetFileNameWithout
                        int batchId = dbHelper.GetNextBatchID();


                        // Update progress textbox with initial progress information
                        await UpdateProgressAsync(insertedRows, totalRows);

                        // Set up progress bar
                        progressbarHcc.Maximum = totalRows;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (insertedRows < totalRows)
                            {
                                // Process each row
                                insertedRows++;

                                // Update progress bar and text box
                                await UpdateProgressAsync(insertedRows, totalRows);

                                // You may want to do additional processing here if needed
                            }
                        }
                        DateTime endedTime = DateTime.Now;
                        TimeSpan TotalTime = endedTime - startTime;
                        string ETime = endedTime.ToString("MM/dd/yyyy HH:mm:ss");
                        double totalSeconds = TotalTime.TotalSeconds;
                        txtUploadEnded.Text = ETime;
                        txtTotaltime.Text = $"{totalSeconds:F2} Seconds";
                        
                        UpdateGridStatus(selectedBatchID, Constants.HCCENDCON);//Update Status label in Status Column 
                        dataGridView.Refresh();
                       
                        dbHelper.UpdateBatchclient(selectedBatchID, startTime, endedTime, totalRows);//to insert batch client
                        
                        PopulateDataGridView();//populate data
                        dbHelper.Log(Constants.ConverttoHCCforbatchIDStarted, Constants.ClientTrack, baseFilename, Constants.uploadct);
                        btncloseHCC.Text = Constants.close;
                        btnochintorwde.Enabled = true;

                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

            }
        }
        public OCHIN_to_RWDE_Conversion(string message, int displayDuration)//Automation process 
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
        private async Task GetservicesAsync(int selectedBatchID)//Mapping from CTServices to HCCServices
        {
            //lblclose.Text = "Abort";
            try
            {
                //progressbarHcc.Visible = false;
                progressbarHcc.Value = 0;

                // Call the stored procedure to get the ConversionStartedAt and ConversionEndedAt values
                var batchDetails = await dbHelper.GetBatchDetailsFromSPAsync(selectedBatchID);//to check whether the conversion completed or not

                if (batchDetails == null)
                    {
                        Console.WriteLine(Constants.Batchnotfound);
                        return;
                    }

                    // Check if ConversionStartedAt and ConversionEndedAt are not null
                    if (batchDetails.ConversionStartedAt!= null && batchDetails.ConversionEndedAt != null)
                    {
                       MessageBox.Show(Constants.Conversionhasalreadybeencompletedforthisbatch,Constants.ochintorwdeconversion,MessageBoxButtons.OK,MessageBoxIcon.Information);
                    txtProgresshcc.Text = "0%";
                    txtProgressServices.Text = "0%";
                    txtBatchid.Text = null;
                    txtUploadStarted.Text = null;
                    txtUploadEnded.Text = null;
                    txtTotaltime.Text = null;
                    return;
                    }
                   
                    btncloseHCC.Text = Constants.abort;
                    txtBatchid.Text = selectedBatchID.ToString();
                    DateTime starttime = DateTime.Now;

                    txtUploadStarted.Text = starttime.ToString("MM/dd/yyyy HH:mm:ss");
                    // Check if a batch has been selected
                    if (selectedBatchID >= 0)
                    {
                        int selectedRowIndex = dataGridView.SelectedRows[0].Index;
                        selectedBatchID = Convert.ToInt32(dataGridView.Rows[selectedRowIndex].Cells["BatchID"].Value.ToString());
                        //int AllTotalRows = GetTotalForBatch(selectedBatchID);
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            using (SqlCommand command = new SqlCommand("MapCMSServicesToHCCServices", connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;

                                // Pass the selected BatchID to the stored procedure
                                command.Parameters.AddWithValue("@BatchID", selectedBatchID);

                                // Get the total number of rows to be inserted
                                int totalRows = GetTotalRowsForBatchservices(selectedBatchID);//to get total rows

                                // Initialize progress variables
                                int insertedRows = 0;

                                string baseFilename = Constants.CTClients;
                                dbHelper.Log($"Convert to HCC  for batch ID Started.", Constants.HCC, baseFilename, Constants.uploadhcc);

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
                                UpdateGridStatus(selectedBatchID, Constants.HCCENDCON);//Update the Status label in batch table

                                dbHelper.Log($"Convert to HCC format completed successfully.", Constants.ClientTrack, baseFilename, Constants.uploadct);
                                //Record the end time
                                TimeSpan totalTime = endTime - startTime; // Calculate total time taken
                                Console.WriteLine("Mapping completed successfully for BatchID: " + selectedBatchID);
                                RemoveSelectedRow(selectedBatchID, "");
                                // int batchId = dbHelper.GetNextBatchID();//Getting next batchid from Batch table
                                dbHelper.UpdateBatchServices(selectedBatchID, startTime, endTime, totalRows);
                                dataGridView.Rows[selectedRowIndex].Cells["Status"].Value = 18;
                                dataGridView.Rows[selectedRowIndex].Cells["ConversionEndedAt"].Value = endTime;
                                DateTime endedTime = DateTime.Now;
                                TimeSpan TotalTime = endedTime - startTime;
                                string ETime = endedTime.ToString("MM/dd/yyyy HH:mm:ss");
                                double totalSeconds = TotalTime.TotalSeconds;
                                txtUploadEnded.Text = ETime;
                                txtTotaltime.Text = $"{totalSeconds:F2} Seconds";
                                btncloseHCC.Text = Constants.close;
                                PopulateDataGridView();
                                dataGridView.Refresh();
                                btnochintorwde.Enabled = true;
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
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            
        private void PopulateDataGridView()//load data in the grid
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("Conversionochin", connection);
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

                    // Now, remove the rows corresponding to removed batch IDs from the DataGridView
                    foreach (int batchID in removedBatchIDs)
                    {
                        DataGridViewRow row = dataGridView.Rows
                            .Cast<DataGridViewRow>()
                            .Where(r => r.Cells["BatchID"].Value != null && (int)r.Cells["BatchID"].Value == batchID)
                            .FirstOrDefault();
                        if (row != null)
                        {
                            dataGridView.Rows.Remove(row);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(Constants.Error, ex.Message);
            }
        }

        private void PopulateDataGridViewHCC()//load data in the grid
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("ConversionHCC", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);

                    //Bind the DataTable to the DataGridView
                    dataGridViewHCC.AutoGenerateColumns = false; // Prevent auto-generation of columns
                    dataGridViewHCC.Columns.Clear(); // Clear existing columns

                    // Define DataGridView columns and map them to DataTable columns
                    dataGridViewHCC.Columns.Add("BatchID", "Batch ID");
                    dataGridViewHCC.Columns["BatchID"].DataPropertyName = "BatchID";
                    dataGridViewHCC.Columns.Add("Type", "Batch Type");
                    dataGridViewHCC.Columns["Type"].DataPropertyName = "Type";
                    dataGridViewHCC.Columns.Add("Description", "Description");
                    dataGridViewHCC.Columns["Description"].DataPropertyName = "Description";
                    dataGridViewHCC.Columns.Add("FileName", "File Name");
                    dataGridViewHCC.Columns["FileName"].DataPropertyName = "FileName";
                    AddDateTime("UploadStartedAt", "Upload Started At", dataGridViewHCC);
                    AddDateTime("UploadEndedAt", "Upload Ended At", dataGridViewHCC);
                    AddDateTime("ConversionStartedAt", "Conversion Started At", dataGridViewHCC);
                    AddDateTime("ConversionEndedAt", "Conversion Ended At", dataGridViewHCC);
                    dataGridViewHCC.Columns.Add("Status", "Status");
                    dataGridViewHCC.Columns["Status"].DataPropertyName = "Status";
                    // Bind the DataTable to the DataGridView
                    dataGridViewHCC.DataSource = dataTable;

                    // Now, remove the rows corresponding to removed batch IDs from the DataGridView
                    foreach (int batchID in removedBatchIDs)
                    {
                        DataGridViewRow row = dataGridViewHCC.Rows
                            .Cast<DataGridViewRow>()
                            .Where(r => r.Cells["BatchID"].Value != null && (int)r.Cells["BatchID"].Value == batchID)
                            .FirstOrDefault();
                        if (row != null)
                        {
                            dataGridViewHCC.Rows.Remove(row);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(Constants.Error, ex.Message);
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
        private int GetTotalRowsForBatchservices(int batchID)//Getting total rows from required table
        {
            int totalRows = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("countcmsservices", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BatchID", batchID);
                        totalRows = (int)command.ExecuteScalar();
                        int successfulRows = totalRows; // Assuming all rows are successfully processed
                        int batchId = dbHelper.GetNextBatchID();//Getting next batchid from Batch table                        
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return totalRows;
        }
        private void RemoveSelectedRow(int batchID, string status) // Method to remove a selected row and store the removed batch ID in the database table
        {
            try
            {
                // Find and remove the row corresponding to the selected batch ID in the DataGridView
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.Cells["BatchID"].Value != null && Convert.ToInt32(row.Cells["BatchID"].Value) == batchID)
                    {
                        dataGridView.Rows.Remove(row);
                        btncloseHCC.Text = status;
                        break;
                    }
                }
                // Add the removed batch ID to the database table
                AddRemovedBatchIDToDatabase(batchID);//remove selected data
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        private void AddRemovedBatchIDToDatabase(int batchID)// // Method to add a removed batch ID to the database table
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO RemovedBatchIDs (BatchID) VALUES (@BatchID)", conn);
                    cmd.Parameters.AddWithValue("@BatchID", batchID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding removed batch ID to database: {ex.Message}");
            }
        }     
        private void UpdateGridStatus(int batchID, int status)// Updates the status label on the form based on a given status code retrieved from the database.
        {
            try
            {
                string valueSelectQuery = "updategrid";

                using (SqlConnection sql = new SqlConnection(connectionString))
                {
                    using (SqlCommand com = new SqlCommand(valueSelectQuery, sql))
                    {
                        com.Parameters.AddWithValue("@ListsID", status);

                        sql.Open();

                        var result = com.ExecuteScalar();

                        if (result != null)
                        {
                            //btncloseHCC.Text = result.ToString();
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
                progressbarHcc.Value = insertedRows;

                // Introduce a delay to slow down the progress display
                await Task.Delay(1); // Adjust the delay time as needed
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

        }
        private int GetTotalRowsForBatch(int batchID)//getting total rows from particular tables
        {
            int totalRows = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("COUNTCMSCLIENTS", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BatchID", batchID);
                        totalRows = (int)command.ExecuteScalar();
                        int successfulRows = totalRows; // Assuming all rows are successfully processed

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return totalRows;
        }
        private int selectedBatchID = 0;
        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)//to select particular batch
        {
            try
            {
                // Check if the click is on a valid row
                if (e.RowIndex >= 0)
                {
                    // Get the BatchID of the selected row
                    selectedBatchID = Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells["BatchID"].Value);
                    Console.WriteLine("Selected BatchID: " + selectedBatchID); // Check the selected BatchID value

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        private void btncloseHCC_Click(object sender, EventArgs e)//Closing form
        {

            int batchId = dbHelper.GetNextBatchID();


            try
            {


                if (btncloseHCC.Text == "Abort")
                {
                    DialogResult result = MessageBox.Show(Constants.Areyousureyouwanttoabort,Constants.Ochin, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    // Check if the user clicked "Yes"

                    // Show a message box indicating successful abort
                    MessageBox.Show(Constants.Abortedsuccessfully);
                    foreach (DataGridViewRow row in dataGridView.Rows)
                    {
                        row.Cells["Status"].Value = Constants.HCCABORT;

                        break;
                    }
                    // Update the status of the selected batch to Status "19" (Abort)
                    UpdateBatchStatus(batchId, Constants.HCCABORT);
                    // Application.Exit();
                }

                this.Close();
                //System.Windows.Forms.Application.Restart();


            }
            catch (Exception ex)
            {
                // Display or log the exception message
                MessageBox.Show(Constants.Error, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateBatchStatus(int selectedBatchID, int status)//Updating Batch Status calling batchid here 
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    ClearTables(selectedBatchID);//to clear the data in table
                    // Construct the SQL UPDATE statement
                    string query = "Updatebatch";
                    // Create and execute the SqlCommand
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to the command
                        command.Parameters.AddWithValue("@Status", status);
                        command.Parameters.AddWithValue("@BatchID", selectedBatchID);

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
        private void ClearTables(int batchId)//clear tables
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
                Console.WriteLine(ex.Message);
                // Log or handle the exception appropriately
                throw; // Re-throw if you want to handle it in the calling method
            }
        }
        private void RefreshValues()//constant values
        {
            txtBatchid.Clear();
            txtTotaltime.Clear();
            txtUploadStarted.Clear();
            txtUploadEnded.Clear();
            txtProgresshcc.Text = "0%";
            progressbarHcc.Value = 0;
            txtProgressServices.Text = "0%";
            progressBarServices.Value = 0;

        }

        private void btnSubmit_Click(object sender, EventArgs e)//to load data insertion into rwde
        {
            try
            {
                string batchType = cbBatchType.Text;
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

        private void btnClear_Click(object sender, EventArgs e)//to clear existing items
        {
            try
            {
                cbBatchType.Items.Clear();
                PopulateDataGridView();
                List<string> batchTypes = dbHelper.GetAllBatchTypesHCC();
                cbBatchType.Items.Clear();  // Clear existing items
                foreach (string batchType in batchTypes)
                {
                    cbBatchType.Items.Add(batchType);
                }
            }
            catch (Exception ex)
            {

            }
        }    
        private void btnSubmit_Click_1(object sender, EventArgs e)//to insert data
        {
            try
            {
                string batchType = cbBatchType.Text;
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
                DataTable result = dbHelper.GetParticularnGenerationDatasconversion(batchType, fromDate, endDate);
                dataGridView.DataSource = result;
                DataTable resultHCC = dbHelper.GetParticularnGenerationDatasHCC(batchType, fromDate, endDate);
                dataGridViewHCC.DataSource = resultHCC;

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
        private void bnClear_Click(object sender, EventArgs e)//to clear existing data
        {
            try
            {
                cbBatchType.Items.Clear();
                PopulateDataGridView();
                //PopulateDataGridViewHCC();
               dtpEndDate.CustomFormat = " ";
            dtpEndDate.Format = DateTimePickerFormat.Custom;
            // Do the same for dtpStartDate or any other DateTimePicker if needed
            dtpStartDate.CustomFormat = " ";
            dtpStartDate.Format = DateTimePickerFormat.Custom;
                List<string> batchTypes = dbHelper.GetAllBatchTypesHCC();
                cbBatchType.Items.Clear();  // Clear existing items
                foreach (string batchType in batchTypes)
                {
                    cbBatchType.Items.Add(batchType);
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void dtpStartDate_ValueChanged_1(object sender, EventArgs e)//to format dateY
        {
            try { 
            dtpStartDate.CustomFormat = "MM-dd-yyyy";
            dtpStartDate.Format = DateTimePickerFormat.Custom;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
              
            }
        }

        private void dtpEndDate_ValueChanged_1(object sender, EventArgs e)//to format date
        {
            try { 
            dtpEndDate.CustomFormat = "MM-dd-yyyy";
            dtpEndDate.Format = DateTimePickerFormat.Custom;
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
             
            }
        }

        private void btnNext_Click_1(object sender, EventArgs e)//navigate to next page for next process
        {
            try { 
            frmMain mainForm = System.Windows.Forms.Application.OpenForms.OfType<frmMain>().FirstOrDefault();

            if (mainForm != null)
            {
                mainForm.ShowOCHINToHCCScreenGENERATE();//to navigate to next page
            }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
              
            }
        }
    }
}