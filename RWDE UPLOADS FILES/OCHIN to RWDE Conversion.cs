using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;

namespace RWDE
{
    public partial class OchinToRwdeConversion : Form
    {
        private readonly DbHelper dbHelper;
        
        public OchinToRwdeConversion()//to initialize the data
        {
            InitializeComponent();
            dbHelper = new DbHelper();
            ControlBox = false;

            //load data in the grid
            PopulateDataGridView();
            PopulateDataGridViewHcc();

            WindowState = FormWindowState.Maximized;

            //get all batches type except Client Track
            List<string> batchTypes = dbHelper.GetAllBatchTypesHcc();
            if (dbHelper.ErrorOccurred)
            {
                MessageBox.Show(Constants.ErrorOccurred);
                return;
            }

            cbBatchType.Items.Clear();  // Clear existing items
            foreach (string batchType in batchTypes)
            {
                cbBatchType.Items.Add(batchType);
            }
            dataGridView.CellFormatting += dataGridView_CellFormatting;
            dataGridViewHCC.CellFormatting += dataGridView_CellFormatting;
            dataGridView.DataBindingComplete += DataGridView_DataBindingComplete;
            dtpStartDate.Value = DateTime.Now.AddYears(-1);
            dtpStartDate.CustomFormat = Constants.DateFormatMMddyyyy;
            dtpEndDate.CustomFormat = Constants.DateFormatMMddyyyy;
            // Assuming you have another DateTimePicker for the End Date
            dtpEndDate.Value = DateTime.Now;
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
        private void dataGridViewHCC_Scroll(object sender, ScrollEventArgs e)//Changing Cursor as Hand on hover
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
                if (control is Button || control is CheckBox || control is DateTimePicker ||control is ComboBox || control is ScrollBar || control is ScrollBar)
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
        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)// Formats the cell value for the Constants.Status column based on the corresponding value in the database.
        {
            try
            {
                if (dataGridViewHCC.Columns[e.ColumnIndex].Name == Constants.StatusHeader)
                {
                    string statusValue = e.Value?.ToString();

                    //to get the value of the status
                    var result = dbHelper.GetListValue(statusValue);
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
                if (dataGridViewHCC.Columns.Contains(Constants.BatchId))
                {
                    dataGridViewHCC.Columns[Constants.BatchId].Width = 205;
                    // Set the width to 200 pixels
                }
                if (dataGridViewHCC.Columns.Contains(Constants.Type))
                {
                    dataGridViewHCC.Columns[Constants.Type].Width = 160; // Set the width to 200 pixels
                }
                if (dataGridViewHCC.Columns.Contains(Constants.FileName))
                {
                    dataGridViewHCC.Columns[Constants.FileName].Width = 205; // Set the width to 200 pixels
                }
                if (dataGridViewHCC.Columns.Contains(Constants.Description))
                {
                    dataGridViewHCC.Columns[Constants.Description].Width = 205; // Set the width to 200 pixels
                }
                if (dataGridViewHCC.Columns.Contains(Constants.UploadStartedAt))
                {
                    dataGridViewHCC.Columns[Constants.UploadStartedAt].Width = 205; // Set the width to 200 pixels
                }
                if (dataGridViewHCC.Columns.Contains(Constants.UploadEndedAt))
                {
                    dataGridViewHCC.Columns[Constants.UploadEndedAt].Width = 205; // Set the width to 200 pixels
                }
                if (dataGridViewHCC.Columns.Contains(Constants.ConversionStartedAt))
                {
                    dataGridViewHCC.Columns[Constants.ConversionStartedAt].Width = 250; // Set the width to 200 pixels
                }
                if (dataGridViewHCC.Columns.Contains(Constants.ConversionEndedAt))
                {
                    dataGridViewHCC.Columns[Constants.ConversionEndedAt].Width = 250; // Set the width to 200 pixels
                }
                if (dataGridViewHCC.Columns.Contains(Constants.Status))
                {
                    dataGridViewHCC.Columns[Constants.Status].Width = 205; // Set the width to 200 pixels
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(Constants.Errorsp + ex.Message);
            }
        }
        private void AddDateTime(string name, string value, DataGridView dataGridViews)//format datetime 
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
                dataGridViews.Columns.Add(column);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btncthcc_Click(object sender, EventArgs e)//Insertion of Client and Eligibility into HCC tables
        {
            try
            {
                if (dataGridView.SelectedRows.Count == 0)
                {
                    MessageBox.Show(Constants.PleaseselectarowwithaBatchIDtoproceed, Constants.Ochintorwdeconversion, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method early if no row is selected
                }
                int selectedRowCount = dataGridView.SelectedRows.Count;
                int hccselectedRowCount = dataGridViewHCC.SelectedRows.Count;
                if (selectedRowCount != 1 || (hccselectedRowCount > 0 && selectedRowCount > 0))
                {
                    MessageBox.Show(Constants.Pleaseselectonlyonebatchatatime, Constants.ErrorCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Exit the method early
                }
                int selectedRowIndex = dataGridView.SelectedRows[0].Index;
                if (dataGridView.Rows[selectedRowIndex].Cells[Constants.BatchId].Value == null || !int.TryParse(dataGridView.Rows[selectedRowIndex].Cells[Constants.BatchId].Value.ToString(), out int batchId) || batchId == 0)
                {
                    MessageBox.Show(Constants.Pleaseselectavalidrowtoproceed, Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int selectedBatchId = Convert.ToInt32(dataGridView.Rows[selectedRowIndex].Cells[Constants.BatchId].Value.ToString());
                string fileName = dataGridView.Rows[selectedRowIndex].Cells[Constants.SmallFileName].Value.ToString();

                if (fileName.Contains(Constants.Client))
                {
                    _ = GetclientssAsync(selectedBatchId);//to get client data mapping
                }

                if (fileName.Contains(Constants.Service))
                {
                    _ = GetservicesAsync(selectedBatchId);//to get service data mapping
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public async Task GetclientssAsync(int selectedBatchId)//Insertion o Client and Eligibility into HCC tables
        {
            try
            {
                progressBarServices.Value = 0;
                var batchDetails = await dbHelper.GetBatchDetailsFromSpAsyncclients(selectedBatchId);//to check whether the conversion completed or not
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
                if (batchDetails == null)
                {
                    MessageBox.Show(Constants.BatchNotfound);
                    return;
                }
                // Check if ConversionStartedAt and ConversionEndedAt are not null
                if (batchDetails.ConversionStartedAt != null && batchDetails.ConversionEndedAt != null)
                {
                    MessageBox.Show(Constants.Conversionhasalreadybeencompletedforthisbatch, Constants.Ochintorwdeconversion, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtProgressClients.Text = Constants.ZeroPercent;
                    txtProgressServices.Text = Constants.ZeroPercent;
                    txtBatchid.Text = null;
                    txtUploadStarted.Text = null;
                    txtUploadEnded.Text = null;
                    txtTotaltime.Text = null;
                    return;
                }
                btnClose.Text = Constants.Abort;
                btnConversion.Enabled = false;
                btnNext.Enabled = false;
                txtUploadEnded.Text = null;
                txtTotaltime.Text = null;
                RefreshValues();
                try
                {
                    int selectedRowCount = dataGridView.SelectedRows.Count;
                    // Check if no batch is selected
                    if (selectedRowCount == 0)
                    {
                        MessageBox.Show(Constants.PleaseSelectABatchBeforeStartingTheConversion, Constants.ErrorCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnClose.Text = Constants.Close;
                        btnConversion.Enabled = true;
                        return; // Exit the method early
                    }
                    // Check if more than one batch is selected
                    if (selectedRowCount != 1)
                    {
                        MessageBox.Show(Constants.Pleaseselectonlyonebatchatatime, Constants.ErrorCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Exit the method earl iy
                    }
                    int selectedRowIndex = dataGridView.SelectedRows[0].Index;

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
                        DialogResult result = MessageBox.Show($@"{Constants.BatchIdHeader} {selectedBatchId} {Constants.Hasalreadycompletedtheconversion}", Constants.OchinToHccConversion, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnConversion.Enabled = true;
                        btnNext.Enabled = true;
                        return;
                    }
                    
                    dbHelper.UpdateGridStatus(selectedBatchId, Constants.Hccstartcon);//update the Status label in Status Column
                    if (dbHelper.ErrorOccurred)
                    {
                        MessageBox.Show(Constants.ErrorOccurred);
                        return;
                    }

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

                    txtBatchid.Text = selectedBatchId.ToString();
                    txtUploadStarted.Text = startTime.ToString(Constants.MMddyyyyHHmmssbkslash);// Record the start time

                    // Get the total number of rows to be inserted
                    int totalRows = dbHelper.GetTotalRowsOfCmsClients(selectedBatchId);
                    if (dbHelper.ErrorOccurred)
                    {
                        MessageBox.Show(Constants.ErrorOccurred);
                        return;
                    }

                    // Set up progress bar
                    progressbarClients.Maximum = totalRows;

                    //to Map the Cms  clients to HCC tables
                    dbHelper.MapCmsClients(selectedBatchId);
                    if (dbHelper.ErrorOccurred)
                    {
                        MessageBox.Show(Constants.ErrorOccurred);
                        return;
                    }


                    int i =0;
                    while (i < totalRows)
                    {
                        // Process each row
                        i++;
                        // Update progress bar and text box
                        await UpdateProgressAsync(i, totalRows);
                    }


                    DateTime endedTime = DateTime.Now;
                    TimeSpan totalTime = endedTime - startTime;
                    string eTime = endedTime.ToString(Constants.MMddyyyyHHmmssbkslash);
                    double totalSeconds = totalTime.TotalSeconds;
                    txtUploadEnded.Text = eTime;
                    txtTotaltime.Text = $@"{totalSeconds:F2} {Constants.Seconds}";

                    dbHelper.UpdateGridStatus(selectedBatchId, Constants.Hccendcon);//Update Status label in Status Column 
                    if (dbHelper.ErrorOccurred)
                    {
                        MessageBox.Show(Constants.ErrorOccurred);
                        return;
                    }

                    dataGridView.Refresh();

                    dbHelper.UpdateBatchclient(selectedBatchId, startTime, endedTime, totalRows);//to insert batch client
                    if (dbHelper.ErrorOccurred)
                    {
                        MessageBox.Show(Constants.ErrorOccurred);
                        return;
                    }
                    PopulateDataGridView();//populate data
                    dbHelper.Log(Constants.ConverttoHcCforbatchIdStarted, Constants.ClientTrackCode, baseFilename, Constants.Uploadct);
                    if (dbHelper.ErrorOccurred)
                    {
                        MessageBox.Show(Constants.ErrorOccurred);
                        return;
                    }

                    btnClose.Text = Constants.Close;
                    btnConversion.Enabled = true;
                    btnNext.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Constants.Errorsp + ex.Message);
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public OchinToRwdeConversion(string message, int displayDuration)//Automation process 
        {
            // Set up form properties
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            ShowInTaskbar = false;
            Size = new Size(500, 200);
            Text = "";
            BackColor = Color.White;

            // Create and configure message label
            Label label = new Label
            {
                Text = message,
                AutoSize = false,
                Size = new Size(ClientSize.Width - 10, ClientSize.Height - 10), // Adjust size for padding
                Location = new Point(5, 5), // Adjust location for padding
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(Constants.FntfmlyArial, 12, FontStyle.Regular) // Set font and size
            };

            // Add label to form
            Controls.Add(label);

            // Set up timer to close the form after displayDuration milliseconds
            using (Timer timer = new Timer())
            {
                timer.Interval = displayDuration;

                timer.Tick += (sender, e) => Close();
                timer.Start();
            }
        }

        public sealed override Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        private async Task GetservicesAsync(int selectedBatchId)//Mapping from CTServices to HCCServices
        {
            //btncloseHCC.Text = Constants.Abort;
            try
            {
                //progressbarHcc.Visible = false;
                progressbarClients.Value = 0;

                // to get the ConversionStartedAt and ConversionEndedAt values
                var batchDetails = await dbHelper.GetBatchDetailsFromSpAsync(selectedBatchId);//to check whether the conversion completed or not
                if (dbHelper.ErrorOccurred)
                {
                    return;
                }
                if (batchDetails == null)
                {
                        MessageBox.Show(Constants.BatchNotfound);
                        return;
                }
                    // Check if ConversionStartedAt and ConversionEndedAt are not null
                    if (batchDetails.ConversionStartedAt!= null && batchDetails.ConversionEndedAt != null)
                    {
                       MessageBox.Show(Constants.Conversionhasalreadybeencompletedforthisbatch,Constants.Ochintorwdeconversion,MessageBoxButtons.OK,MessageBoxIcon.Information);
                        txtProgressClients.Text = Constants.ZeroPercent;
                        txtProgressServices.Text = Constants.ZeroPercent;
                        txtBatchid.Text = null;
                        txtUploadStarted.Text = null;
                        txtUploadEnded.Text = null;
                        txtTotaltime.Text = null;
                        return;
                    }
                    btnClose.Text = Constants.Abort;
                    txtBatchid.Text = selectedBatchId.ToString();
                    DateTime starttime = DateTime.Now;
                    txtUploadEnded.Text = null;
                    txtTotaltime.Text = null;
                    btnConversion.Enabled = false;
                    txtUploadStarted.Text = starttime.ToString(Constants.MMddyyyyHHmmssbkslash);


                // Get the total number of rows to be inserted
                int totalRows = dbHelper.GetTotalRowsForBatchservicesOchin(selectedBatchId);//to get total rows
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }


                // Initialize progress variables
                int insertedRows = 0;

                string baseFilename = Constants.CtServices;
                dbHelper.Log(Constants.ConverttoHcCforbatchIdStarted, Constants.Hcc, baseFilename, Constants.Uploadhcc);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }

                DateTime startTime = DateTime.Now;
                txtUploadStarted.Text = startTime.ToString(Constants.MMddyyyyHHmmssbkslash);
                // Update progress textbox with initial progress information
                if (totalRows != 0)
                {
                    await UpdateProgressAsyncservices(insertedRows, totalRows);//to update the progress Bar for Services
                }
                else
                {
                    MessageBox.Show(Constants.Nodataexistsforthisbatchid, Constants.Ochintorwdeconversion, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Check if a batch has been selected
                if (selectedBatchId >= 0)
                {
                    int selectedRowIndex = dataGridView.SelectedRows[0].Index;
                    selectedBatchId = Convert.ToInt32(dataGridView.Rows[selectedRowIndex].Cells[Constants.BatchId].Value.ToString());

                    //to map Cms Service To Hcc Services
                    dbHelper.MapCmsServicesToHccServices(selectedBatchId);
                    if (dbHelper.ErrorOccurred)
                    {
                        MessageBox.Show(Constants.ErrorOccurred);
                        return;
                    }


                    for (int i = 1; i <= totalRows; i++)
                    {
                        // Update progress bar and text box
                        await UpdateProgressAsyncservices(i, totalRows);
                    }

                    DateTime endTime = DateTime.Now;
                    dbHelper.UpdateGridStatus(selectedBatchId, Constants.Hccendcon);//Update the Status label in batch table
                    if (dbHelper.ErrorOccurred)
                    {
                        MessageBox.Show(Constants.ErrorOccurred);
                        return;
                    }


                    //insertion into log table
                    dbHelper.Log(Constants.ConverttoHcCformatcompletedsuccessfully, Constants.ClientTrackCode, baseFilename, Constants.Uploadct);
                    if (dbHelper.ErrorOccurred)
                    {
                        MessageBox.Show(Constants.ErrorOccurred);
                        return;
                    }

                    MessageBox.Show(Constants.MappingcompletedsuccessfullyforBatchId + selectedBatchId);

                    //update batch Services data
                    dbHelper.UpdateBatchServices(selectedBatchId, startTime, endTime, totalRows);
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
                    btnClose.Text = Constants.Close;

                    //load data in the grid
                    PopulateDataGridView();
                    dataGridView.Refresh();
                    btnConversion.Enabled = true;

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
        private void PopulateDataGridView()//load data in the grid
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(dbHelper.GetConnectionString()))
                {
                    SqlCommand command = new SqlCommand(Constants.ConversionOchin, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
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
                MessageBox.Show(Constants.ErrorCode, ex.Message);
            }
        }
        private void PopulateDataGridViewHcc()//load data in the grid
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(dbHelper.GetConnectionString()))
                {
                    SqlCommand command = new SqlCommand(Constants.ConversionHcc, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);

                    //Bind the DataTable to the DataGridView
                    dataGridViewHCC.AutoGenerateColumns = false; // Prevent auto-generation of columns
                    dataGridViewHCC.Columns.Clear(); // Clear existing columns

                    // Define DataGridView columns and map them to DataTable columns
                    dataGridViewHCC.Columns.Add(Constants.BatchId, Constants.BatchIdHeader);
                    dataGridViewHCC.Columns[Constants.BatchId].DataPropertyName = Constants.BatchId;
                    dataGridViewHCC.Columns.Add(Constants.Type, Constants.BatchTypeHeader);
                    dataGridViewHCC.Columns[Constants.Type].DataPropertyName = Constants.Type;
                    dataGridViewHCC.Columns.Add(Constants.Description, Constants.Description);
                    dataGridViewHCC.Columns[Constants.Description].DataPropertyName = Constants.Description;
                    dataGridViewHCC.Columns.Add(Constants.FileName, Constants.FileNamesp);
                    dataGridViewHCC.Columns[Constants.FileName].DataPropertyName = Constants.FileName;
                    AddDateTime(Constants.UploadStartedAt, Constants.UploadStartedAtHeader, dataGridViewHCC);
                    AddDateTime(Constants.UploadEndedAt, Constants.UploadEndedAtHeader, dataGridViewHCC);
                    AddDateTime(Constants.ConversionStartedAt, Constants.ConversionStartedAtHeader, dataGridViewHCC);
                    AddDateTime(Constants.ConversionEndedAt, Constants.ConversionEndedAtHeader, dataGridViewHCC);
                    dataGridViewHCC.Columns.Add(Constants.Status, Constants.Status);
                    dataGridViewHCC.Columns[Constants.Status].DataPropertyName = Constants.Status;
                    // Bind the DataTable to the DataGridView
                    dataGridViewHCC.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Constants.ErrorCode, ex.Message);
            }
        }
        //to update the progress bar of services
        public async Task UpdateProgressAsyncservices(int insertedRows, int totalRows)
        {
            try
            {
                // Calculate progress percentage
                int progressPercentage = (int)((double)insertedRows / totalRows * 100);

                // Update UI controls directly (assume running on the UI thread)
                txtProgressServices.Text = $"{insertedRows}/{totalRows} ({progressPercentage}%)";
                progressBarServices.Value = progressPercentage;
                await Task.Delay(20);
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message);
            }
            return;
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
                txtProgressClients.Text = progressInfo;

                // Update the progress bar
                progressbarClients.Value = insertedRows;
                await Task.Delay(20);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Constants.Errorsp + ex.Message);
            }
        }
        
        private void btncloseHCC_Click(object sender, EventArgs e)//Closing form
        {
            try
            {
                int batchId = dbHelper.GetNextBatchId();
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
                if (btnClose.Text == Constants.Abort)
                {
                    DialogResult result = MessageBox.Show(Constants.Areyousureyouwanttoabort,Constants.Ochin, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                   
                    // Check if the user clicked "Yes"
                    if (result == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow row in dataGridView.Rows)
                        {
                            row.Cells[Constants.Status].Value = Constants.Hccabort;
                            break;
                        }
                        // Update the status of the selected batch to Status "19" (Abort)
                        dbHelper.UpdateBatchStatusOchin(batchId, Constants.Hccabort);
                        if (dbHelper.ErrorOccurred)
                        {
                            MessageBox.Show(Constants.ErrorOccurred);
                            return;
                        }

                        // Show a message box indicating successful abort
                        MessageBox.Show(Constants.AbortedSuccessfully);
                        Close();
                        Application.Restart();
                    }
                }
                else
                {
                    Close();
                    Application.Restart();
                }
                
            }
            catch (Exception ex)
            {
                // Display or log the exception message
                MessageBox.Show(Constants.ErrorCode, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void RefreshValues()//constant values
        {
            try
            {
                txtBatchid.Clear();
            txtTotaltime.Clear();
            txtUploadStarted.Clear();
            txtUploadEnded.Clear();
            txtProgressClients.Text = Constants.ZeroPercent;
            progressbarClients.Value = 0;
            txtProgressServices.Text = Constants.ZeroPercent;
            progressBarServices.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }    
        private void btnSubmit_Click(object sender, EventArgs e)//to insert data
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

                //get generation data according to start and end time
                DataTable result = dbHelper.GetParticularnGenerationDatasconversion(batchType, fromDate, endDate);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
                dataGridView.DataSource = result;

                //get generation data of HCC according to start and end time
                DataTable resultHcc = dbHelper.GetParticularGenerationDatasHcc(batchType, fromDate, endDate);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
                dataGridViewHCC.DataSource = resultHcc;

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
        private void btnClear_Click(object sender, EventArgs e)//to clear existing data
        {
            try
            {
                cbBatchType.Items.Clear();
                //load data in the grid
                PopulateDataGridView();
                dtpEndDate.CustomFormat = Constants.Space;
                dtpEndDate.Format = DateTimePickerFormat.Custom;
                // Do the same for dtpStartDate or any other DateTimePicker if needed
                dtpStartDate.CustomFormat = Constants.Space;
                dtpStartDate.Format = DateTimePickerFormat.Custom;
                List<string> batchTypes = dbHelper.GetAllBatchTypesHcc();
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }

                cbBatchType.Items.Clear();  // Clear existing items
                foreach (string batchType in batchTypes)
                {
                    cbBatchType.Items.Add(batchType);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"{Constants.AnErrorOccurred}{ex.Message}", Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dtpStartDate_ValueChanged_1(object sender, EventArgs e)//to format date when the Start date is changed
        {
            try { 
                dtpStartDate.CustomFormat = Constants.DateFormatMMddyyyy;
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
                dtpEndDate.CustomFormat = Constants.DateFormatMMddyyyy;
                dtpEndDate.Format = DateTimePickerFormat.Custom;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)//navigate to next page for next process
        {
            try {
                FrmMain mainForm = Application.OpenForms.OfType<FrmMain>().FirstOrDefault();

                if (mainForm != null)
                {
                    mainForm.ShowOchinToHccScreenGenerate();//to navigate to Xml Generation Page
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}