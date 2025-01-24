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

            ControlBox = false;
            WindowState = FormWindowState.Maximized;

            //Handle BatchType Values
            List<string> batchTypes = dbHelper.GetAllBatchTypes();
            if (dbHelper.ErrorOccurred)
            {
                MessageBox.Show(Constants.ErrorOccurred);
                return;
            }

            dataGridView.CellFormatting += dataGridView_CellFormatting;
            dataGridView.DataBindingComplete += DataGridView_DataBindingComplete;
            dtpEndDate.CustomFormat = Constants.Space;
            dtpEndDate.Format = DateTimePickerFormat.Custom;
            dtpStartDate.CustomFormat = Constants.Space;
            dtpStartDate.Format = DateTimePickerFormat.Custom;
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
        private void dataGridView_Scroll(object sender, ScrollEventArgs e)//Changing Cursor as Hand on hover
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
        private void RegisterEvents(Control parent)//Assigning events to all Controls
        {
            try
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
        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)// Formats the cell value for the Constants.Status column based on the corresponding value in the database.
        {
            try
            {
                if (dataGridView.Columns[e.ColumnIndex].Name == Constants.Status)
                {
                    string statusValue = e.Value?.ToString();
                    var result = dbHelper.FormatStatus(statusValue);
                    if (dbHelper.ErrorOccurred)
                    {
                        MessageBox.Show(Constants.ErrorOccurred);
                        return;
                    }

                    if (result != null)
                    {
                        e.Value = result.ToString();
                        e.FormattingApplied = true;
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
            {   // Check if the Constants.BatchId column exists before setting its width
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
                MessageBox.Show(Constants.Errorsp + ex.Message);
            }
        }
        private void AddDateTime(string name, string value, DataGridView dataGridView)//to add the DateTime Column
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private async void btnCTtoHCC_Click(object sender, EventArgs e)//Insertion of Client and Eligibility into HCC tables
        {
            btncthcc.Enabled = false;
            int batchid = dbHelper.GetNextBatchId();
            if (dbHelper.ErrorOccurred)
            {
                MessageBox.Show(Constants.ErrorOccurred);
                return;
            }
            RefreshValues();
            try
            {
                int selectedRowCount = dataGridView.SelectedRows.Count;
                // Check if no batch is selected
                if (selectedRowCount == 0)
                {
                    MessageBox.Show(Constants.PleaseSelectABatchBeforeStartingTheConversion, Constants.ErrorCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                DateTime? conversionStartedAt = null;
                DateTime? conversionEndedAt = null;

                //to get the conversion time of the Batch
                (conversionStartedAt, conversionEndedAt) = dbHelper.GetCoversionTime(selectedBatchId);

                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
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
                dbHelper.Log(Constants.ConverttoHcCforbatchIdStarted, Constants.ClientTrackCode, baseFilename, Constants.Uploadct);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
                DateTime startTime = DateTime.Now;
                dataGridView.Rows[selectedRowIndex].Cells[Constants.Status].Value = 17;
                dataGridView.Rows[selectedRowIndex].Cells[Constants.ConversionStartedAt].Value = startTime;

                //to upadte the status of the Batch
                dbHelper.UpdateBatchStatusTime(selectedBatchId, 17, startTime);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }

                txtBatchid.Text = selectedBatchId.ToString();
                txtUploadStarted.Text = startTime.ToString(Constants.MMddyyyyHHmmssbkslash);// Record the start time


                // Get the total number of rows to be inserted
                int totalRows = dbHelper.GetTotalRows(selectedBatchId);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }

                // Set up progress bar
                progressBarClients.Maximum = totalRows;
                //to map the Cms Clients to Hcc Tables
                _ = dbHelper.MapCmsClientsAsync(selectedBatchId);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
                // Initialize progress variables
                int insertedRows = 0;
                // Update progress textbox with initial progress information
                await UpdateProgressAsync(insertedRows, totalRows);

                while (insertedRows < totalRows)
                {
                    // Process each row
                    insertedRows++;

                    // Update progress bar and text box
                    await UpdateProgressAsync(insertedRows, totalRows);
                }

                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }

                DateTime endTime = DateTime.Now;
                UpdateGridStatus(selectedBatchId, Constants.Hccendcon);//Update Status label in Status Column 
                dataGridView.Refresh();

                dbHelper.Log(Constants.ConverttoHcCforbatchIdStarted, Constants.ClientTrackCode, baseFilename, Constants.Uploadct);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
                //Mapping from CTServices to HCCServices
                _ = GetservicesAsync(selectedBatchId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Constants.Errorsp + ex.Message);
            }
        }


        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
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
                    int allTotalRows = dbHelper.GetTotalForBatch(selectedBatchId);//getting total rows from particular tables

                    // Get the total number of rows to be inserted
                    int totalRows = dbHelper.GetTotalRowsForBatchservices(selectedBatchId);

                    if (dbHelper.ErrorOccurred)
                    {
                        MessageBox.Show(Constants.ErrorOccurred);
                        return;
                    }

                    // Set up progress bar
                    progressBarServices.Maximum = totalRows;

                    //to Map the DlServices to Hcc Tables
                    _ = dbHelper.MapDlServicesAsync(selectedBatchId);
                    if (dbHelper.ErrorOccurred)
                    {
                        MessageBox.Show(Constants.ErrorOccurred);
                        return;
                    }

                    // Initialize progress variables
                    int insertedRows = 0;
                    // Update progress textbox with initial progress information
                    await UpdateProgressAsyncservices(insertedRows, totalRows);

                    while (insertedRows < totalRows)
                    {
                        // Process each row
                        insertedRows++;

                        // Update progress bar and text box
                        await UpdateProgressAsyncservices(insertedRows, totalRows);
                    }
                    if (dbHelper.ErrorOccurred)
                    {
                        MessageBox.Show(Constants.ErrorOccurred);
                        return;
                    }

                    string baseFilename = Constants.CtClients;
                    dbHelper.Log(Constants.ConverttoHcCforbatchIdStarted, Constants.Hcc, baseFilename, Constants.Uploadhcc);
                    if (dbHelper.ErrorOccurred)
                    {
                        MessageBox.Show(Constants.ErrorOccurred);
                        return;
                    }

                    DateTime startTime = DateTime.Now;
                    txtUploadStarted.Text = startTime.ToString(Constants.MMddyyyyHHmmssbkslash);

                    DateTime endTime = DateTime.Now;
                    UpdateGridStatus(selectedBatchId, Constants.Hccendcon);//Update the Status label in batch table

                    dbHelper.Log(Constants.ConverttoHcCformatcompletedsuccessfully, Constants.ClientTrackCode, baseFilename, Constants.Uploadct);
                    if (dbHelper.ErrorOccurred)
                    {
                        MessageBox.Show(Constants.ErrorOccurred);
                        return;
                    }

                    MessageBox.Show(Constants.MappingcompletedsuccessfullyforBatchId + selectedBatchId);

                    // Method to remove a selected row and store the removed batch ID in the database table
                    RemoveSelectedRow(selectedBatchId, "");
                    int batchId = dbHelper.GetNextBatchId();//Getting next batchid from Batch table
                    if (dbHelper.ErrorOccurred)
                    {
                        MessageBox.Show(Constants.ErrorOccurred);
                        return;
                    }
                    //Updating status and Time on Batch Table
                    dbHelper.UpdateBatch(batchId, startTime, endTime, allTotalRows);
                    if (dbHelper.ErrorOccurred)
                    {
                        MessageBox.Show(Constants.ErrorOccurred);
                        return;
                    }

                    dataGridView.Rows[selectedRowIndex].Cells[Constants.Status].Value = 18;
                    dataGridView.Rows[selectedRowIndex].Cells[Constants.ConversionEndedAt].Value = endTime;
                    DateTime endedTime = DateTime.Now;
                    TimeSpan totalTime = endTime - startTime; // Calculate total time taken
                    string eTime = endedTime.ToString(Constants.MMddyyyyHHmmssbkslash);
                    double totalSeconds = totalTime.TotalSeconds;
                    txtUploadEnded.Text = eTime;
                    txtTotaltime.Text = $@"{totalSeconds:F2} {Constants.Seconds}";
                    btncloseHCC.Text = Constants.Close;

                    //to display the default Grid
                    PopulateDataGridView();
                    dataGridView.Refresh();
                    btncthcc.Enabled = true;
                }
                else
                {
                    MessageBox.Show(Constants.PleaseSelectABatchBeforeStartingTheConversion);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Constants.Errorsp + ex.Message);
            }
        }
        private void PopulateDataGridView()//to display the default Grid
        {
            try
            {
                string query = Constants.Conversion;
                DataTable dataTable = dbHelper.FillTheGrid(query);//to fill the Gird 
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }

                //Bind the DataTable to the DataGridView
                dataGridView.AutoGenerateColumns = false;
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
            catch (Exception ex)
            {
                MessageBox.Show(Constants.ErrorCode, ex.Message);
            }
        }
        public async Task UpdateProgressAsyncservices(int insertedRows, int totalRows)//Services Progress bar
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
                MessageBox.Show(Constants.Errorsp + ex.Message);
            }
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
                dbHelper.AddRemovedBatchIdToDatabase(batchId);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(Constants.Errorsp + ex.Message);
            }
        }
        private void UpdateGridStatus(int batchId, int status)// Updates the status label on the form based on a given status code retrieved from the database.
        {
            try
            {
                //to upadte the Grid status 
                var result = dbHelper.UpdateGridStatus(status);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }

                if (result != null)
                {
                    lblStatus.Text = result.ToString();
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                MessageBox.Show($@"{Constants.ErrorUpdatingGridStatus}{ex.Message}");
            }
        }
        public async Task UpdateProgressAsync(int insertedRows, int totalRows)//progress of rows
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
                MessageBox.Show(Constants.Errorsp + ex.Message);
            }
        }
        private void btncloseHCC_Click(object sender, EventArgs e)//Closing form
        {
            int batchId = dbHelper.GetNextBatchId();
            if (dbHelper.ErrorOccurred)
            {
                MessageBox.Show(Constants.ErrorOccurred);
                return;
            }
            try
            {
                if (btncloseHCC.Text == Constants.Close)
                {
                    Close();
                    Application.Restart();
                    return;
                }
                if (btncloseHCC.Text == Constants.Abort)
                {
                    DialogResult result = MessageBox.Show(Constants.Areyousureyouwanttoabort, Constants.AbortConfirmation, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    // Check if the user clicked "Yes"

                    // Show a message box indicating successful abort
                    MessageBox.Show(Constants.AbortedSuccessfully);
                    foreach (DataGridViewRow row in dataGridView.Rows)
                    {
                        row.Cells[Constants.Status].Value = Constants.Hccabort;
                        break;
                    }
                    // Update the status of the selected batch to Status "19" (Abort)
                    dbHelper.UpdateBatchStatus(batchId, Constants.Hccabort);

                    if (dbHelper.ErrorOccurred)
                    {
                        MessageBox.Show(Constants.ErrorOccurred);
                        return;
                    }

                    //to delete the Aborted Batch data
                    dbHelper.ClearAbortedTables(batchId);
                    if (dbHelper.ErrorOccurred)
                    {
                        MessageBox.Show(Constants.ErrorOccurred);
                        return;
                    }

                    // Application.Exit();
                }
                Close();
                Application.Restart();
            }
            catch (Exception ex)
            {
                // Display or log the exception message
                MessageBox.Show(Constants.ErrorCode, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshValues()//to set all values as default
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)//to convert the data to HCC
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
                // Retrieve and bind data //retrieve data for according to the start and end date
                DataTable result = dbHelper.GetParticularConversionDatas(batchType, fromDate, endDate);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }

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

        private void bnClear_Click(object sender, EventArgs e)//to clear all loaded data
        {
            try
            {
                //txtBatchtype.Items.Clear();
                PopulateDataGridView();
                List<string> batchTypes = dbHelper.GetAllBatchTypes();
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }

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
        private void dtpStartDate_ValueChanged(object sender, EventArgs e)//to get the startdate
        {
            try
            {
                dtpStartDate.CustomFormat = Constants.DateFormatMMddyyyy;
                dtpStartDate.Format = DateTimePickerFormat.Custom;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)//to get the Enddate
        {
            try
            {
                dtpEndDate.CustomFormat = Constants.DateFormatMMddyyyy;
                dtpEndDate.Format = DateTimePickerFormat.Custom;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}


