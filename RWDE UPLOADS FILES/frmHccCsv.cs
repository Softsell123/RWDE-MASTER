using Rwde;
using RWDE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RWDE_UPLOADS_FILES//
{
    public partial class frmHccCsv : Form
    {
        private readonly string connectionString;
        private readonly DBHelper dbHelper;
        private object error;
        public frmHccCsv()//to initialize data
        {
            InitializeComponent();
            dbHelper = new DBHelper();
            connectionString = dbHelper.GetConnectionString();
            this.ControlBox = false;
            this.WindowState = FormWindowState.Maximized;
            descriptionformat();
            RegisterEvents(this);
            if (File.Exists(Constants.LastFolderPathhcc))
            {
                string LastFolderPathhcc = File.ReadAllText(Constants.LastFolderPathhcc).Trim();  // Trim to remove any extra spaces or newlines
                // Check if the file content is not empty and the directory exists
                if (!string.IsNullOrEmpty(LastFolderPathhcc) && Directory.Exists(LastFolderPathhcc))
                {
                    txtPath.Text = LastFolderPathhcc;
                }
                else
                {
                    txtPath.Clear();  // Ensure the path is cleared if the file is empty or invalid
                }
            }
        }
        public string fileName;
        public string baseFilename;
        public int batchid;
        bool isUploading = false;
        bool batchId = false;    
        public string path;
        private int totalCsvFiles;
        private int currentCsvFileIndex;
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
                if (control is System.Windows.Forms.Button || control is CheckBox || control is DateTimePicker)
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    if (btnClose.Text == Constants.abort)
                    {
                      
                            DialogResult result = MessageBox.Show(Constants.Areyousureyouwanttoabort, "UPLOAD OCHIN CSV", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            int currentBatchId = dbHelper.GetNextBatchIdabort(connection); // Get the current batch ID from the database

                            string fileName = txtFileName.Text;
                            
                            // Increment the batch ID
                            int nextBatchId = currentBatchId + 1;
                            dbHelper.DeleteBatchochin(nextBatchId.ToString());

                            // Update the batch status with the new batch ID

                            // Show confirmation message
                            MessageBox.Show(Constants.Abortedsuccessfully, "UPLOAD OCHIN CSV");

                                UpdateBatch(nextBatchId, fileName, path);
                                this.Close();

                            
                        }
                    }

                    // Restart the application
                    Application.Restart();
                }
                catch (Exception ex)
                {
                    // Handle any exceptions by showing the error message
                    MessageBox.Show(Constants.Error + ex.Message, Constants.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ClearTables(int batchId)//to clear data in tables
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("abortochindelete", connection))
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


        private void UpdateBatch(int batchid, string filename, string path)//to Update the data information 
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string description = Constants.abortedfile;
                    DateTime startTime = DateTime.Now;
                    
                    dbHelper.InsertBatch(batchid, filename, path, Constants.OCHIN, description, startTime, 0, 0, Constants.fileaborted);
                    ClearTables(batchid);//to clear data in tables


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private async void btnUpload_Click_1(object sender, EventArgs e)//to upload data into table in database
        {
            if (string.IsNullOrEmpty(txtPath.Text))
            {
                MessageBox.Show("The folder path cannot be empty. Please select a valid folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } // Get all files in the selected folder
            string[] filesempty = Directory.GetFiles(txtPath.Text);

            // Check if the folder is empty

            if (txtPath.Text == "" || txtPath == null || filesempty.Length == 0)
            {
                MessageBox.Show(Constants.ThefolderisemptyPleaseuploadfiles, Constants.Ochin, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
           
            if (string.IsNullOrWhiteSpace(txtPath.Text))
            {
                MessageBox.Show(Constants.Selectafilebeforeuploading);
                return; // Exit the method if the path is empty
            }
            string[] files = Directory.GetFiles(txtPath.Text);
            bool allFilesAreCsv = files.All(file => Path.GetExtension(file).Equals(".csv", StringComparison.OrdinalIgnoreCase));

            bool allFilesAreXml = files.All(file => Path.GetExtension(file).Equals(".csv", StringComparison.OrdinalIgnoreCase));

            if (!allFilesAreXml)
            {
                MessageBox.Show(Constants.ThefoldercontainsnonCSVfilesUploadisallowedonlyforCSVfiles, Constants.Ochin, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!allFilesAreCsv)
            {
                MessageBox.Show(Constants.Areyousureyouwanttoabort, Constants.Ochin, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            btnClose.Text = Constants.abort;
            btnUpload.Enabled = false;
            isUploading = true;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string folderPath = txtPath.Text;

                    if (!string.IsNullOrEmpty(folderPath))
                    {
                        string existingBatchId = txtBatchid.Text;

                        // Check if the batch ID already exists
                        if (!string.IsNullOrEmpty(existingBatchId))
                        {
                            MessageBox.Show($"Batch ID {existingBatchId} already exists. Close and reopen to upload new files.", "Batch ID Exists", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            ResetUI();
                            return;
                        }

                        string[] csvFiles = Directory.GetFiles(folderPath, "*.csv");
                        int totalCsvFiles = csvFiles.Length;
                        int currentCsvFileIndex = 0;
                        int totalRowsInserted = 0;
                        progressBar.Value = 0;
                        progressBarfile.Value = 0;
                        progressBarfile.Maximum = totalCsvFiles;

                        DateTime startTime = DateTime.Now;

                        int batchid = GetNextBatchId(connection);// Initialize the first batch ID to 1

                        foreach (string csvFilePath in csvFiles)
                        {
                            string baseFilename = Path.GetFileNameWithoutExtension(csvFilePath).Split(new[] { " (" }, StringSplitOptions.RemoveEmptyEntries)[0];
                            string fileName = Path.GetFileName(csvFilePath);
                            txtFileName.Text = fileName;
                            if (!isUploading)
                                break;
                            UpdateFileProgress(currentCsvFileIndex, totalCsvFiles, totalRowsInserted);//NO OF ROWS INSERTED
                            currentCsvFileIndex++;

                            int totalRowsInCurrentFile = 0; // Initialize within the loop

                            txtBatchid.Text = batchid.ToString();
                            string date = startTime.ToString("MM/dd/yyyy");
                            string time = startTime.ToString("HH:mm:ss");
                            txtDesc.Text = $"OCHIN CSV Upload on {date} at {time}";
                            string Time = startTime.ToString("MM/dd/yyyy HH:mm:ss");
                            txtUploadStarted.Text = Time;
                            path = csvFilePath;
                            if (isUploading)
                            {
                                // Await the async method call
                                var result = await InsertCsvDataIntoTable(csvFilePath, batchid, connection);

                                totalRowsInserted += result.Item1;
                                totalRowsInCurrentFile += result.Item1;
                                progressBarfile.Value = currentCsvFileIndex;

                                // Log successful insertion
                                fileName = Path.GetFileName(csvFilePath);
                                string description = txtDesc.Text;
                                int successfulRows = result.Item1;
                                DateTime endtime = DateTime.Now;
                                if (isUploading)
                                {
                                    dbHelper.InsertBatch(batchid, fileName, csvFilePath, Constants.OCHIN, description, startTime, totalRowsInCurrentFile, successfulRows, Constants.Status);
                                    UpdateFileProgress(currentCsvFileIndex, totalCsvFiles, totalRowsInserted);//no of files insertion
                                }
                            }

                            if (baseFilename.Contains(Constants.Clients))
                            {
                                //btnClose.Text = Constants.close;
                                TimeSpan elapsedTime = DateTime.Now - startTime;
                                string ETime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

                                // Increment batch ID for the next file if it is a service
                                // batchid++;
                            }

                            if (baseFilename.Contains(Constants.Services))
                            {
                                //btnClose.Text = Constants.close;
                                TimeSpan elapsedTime = DateTime.Now - startTime;
                                string ETime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                                double totalSeconds = elapsedTime.TotalSeconds;
                                txtUploadEnded.Text = ETime;
                                txtTotaltime.Text = $"{totalSeconds:F2} Seconds";
                                btnUpload.Enabled = true;
                                btnClose.Text = "Close";
                            }
                            else
                            {
                                btnUpload.Enabled = true;
                            }
                        }
                    }
                    else
                    {
                        btnUpload.Enabled = true;
                        //btnClose.Text = "Close";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Constants.ErrorMessage, ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ResetUI();
                }
            }
        }
        private int GetNextBatchId(SqlConnection connection)//to get next batch id
        {
            try
            {
                // Query to get the maximum batch ID from the Batch table and increment by 1
                using (SqlCommand command = new SqlCommand("SELECT ISNULL(MAX(BatchID), 0) + 1 FROM Batch", connection))
                {
                    return (int)command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }
        private void ResetUI()//to reset button
        {
            btnUpload.Enabled = true;
            btnClose.Text = "Close";
            btnBrowse.Enabled = true;
        }


       
        private async void UpdateFileProgress(int currentCsvFileIndex, int totalCsvFiles, int totalRowsInserted)//to show the progress of lines inserted
        {
            try
            {
                progressBar.Minimum = 0;
                progressBar.Maximum = totalCsvFiles;
                int progressPercentage = (int)((double)currentCsvFileIndex / totalCsvFiles * 100);
               // txtProgressbar.Text = $"{currentCsvFileIndex}/{totalCsvFiles} ({progressPercentage}%)";
                progressBar.Value = currentCsvFileIndex;
                txtProgressfile.Text = $"{currentCsvFileIndex}/{totalCsvFiles} ({progressPercentage}%)";

                await Task.Delay(100); // Slow down progress bar update
            }
            catch (Exception ex)
            {
                // Log error if needed
                dbHelper.Log($"Error updating file progress: {ex.Message}", Constants.ERROR, "ProgressUpdate", Constants.uploadochin);
            }
        }

        public async Task<Tuple<int, bool>> InsertCsvDataIntoTable(string csvFilePath, int batchId, SqlConnection connection)//functionality to insert particular data 
        {
            int rowsInserted = 0;
            int totalRows = 0;
            string filename = Path.GetFileNameWithoutExtension(csvFilePath);//to get file name
            string[] filenameParts = filename.Split(new[] { " (" }, StringSplitOptions.RemoveEmptyEntries);
            string baseFilename = filenameParts[0];

            List<string[]> clientData = new List<string[]>();
            List<string[]> serviceData = new List<string[]>();

            try
            {
                error = null;


                using (StreamReader reader = new StreamReader(csvFilePath))
                {
                    string headerLine = reader.ReadLine();
                    totalRows = File.ReadLines(csvFilePath).Count() - 1;

                    bool hasLoggedClient = false;
                    bool hasLoggedService = false;

                    while (!reader.EndOfStream)
                    {
                        if (!isUploading)
                            break;
                        string line = reader.ReadLine();

                        if (string.IsNullOrEmpty(line))
                            continue;

                        string[] data = line.Split('|');

                        if (baseFilename.Contains(Constants.Clients))
                        {
                            clientData.Add(data);

                            if (!hasLoggedClient)
                            {
                                dbHelper.Log(Constants.UploadforOCHINCSVstarted, Constants.OCHIN, baseFilename + ".csv", Constants.uploadochin);
                                hasLoggedClient = true;
                            }
                        }
                        else if (baseFilename.Contains(Constants.Services))
                        {
                            serviceData.Add(data);

                            if (!hasLoggedService)
                            {
                                dbHelper.Log(Constants.UploadforOCHINCSVstarted, Constants.OCHIN, baseFilename + ".csv", Constants.uploadochin);
                                hasLoggedService = true;
                            }
                        }
                    }
                }
                // Loop until a unique batchId is found
                while (BatchExists(connection, batchId))
                {
                    batchId++;
                }

                // Insert all client data first
                foreach (var data in clientData)
                {
                    if (chckPHI.Checked == true && chckURN.Checked == false)
                    {
                        dbHelper.InsertClientInformationPHI(connection, data, batchId);//insertion of the client file with PHI DATA MASKING CONDITION
                    }
                    else if (chckURN.Checked == true)
                    {
                        dbHelper.InsertClientInformationphiurn(connection, data, batchId);//insertion of the client file without PHI DATA MASKING CONDITION
                    }
                    else
                    {
                        dbHelper.InsertClientInformation(connection, data, batchId);//insertion of the client file without PHI DATA MASKING CONDITION
                    }
                    
                    rowsInserted++;
                    if (isUploading)
                    {
                        await UpdateProgress(rowsInserted, totalRows); // Await the progress update
                        //await Task.Delay(500); // Adding delay of 500 milliseconds
                    }
                }

                // Insert all service data next
                foreach (var data in serviceData)
                {
                    int batchid = batchId - 1;
                    if (chckPHI.Checked == true)//InsertClientServiceDataPHI
                    {
                        dbHelper.InsertClientServiceData(connection, data, batchid);//insertion of the services file with PHI DATA MASKING CONDITION
                    }
                    else
                    {
                        dbHelper.InsertClientServiceData(connection, data, batchid);//insertion of the services file with PHI DATA MASKING CONDITION
                    }

                    rowsInserted++;
                    await UpdateProgress(rowsInserted, totalRows); // Await the progress update
                    //await Task.Delay(500); // Adding delay of 500 milliseconds
                }
                if (isUploading)
                {
                    dbHelper.Log(Constants.UploadforOCHINCSVcompletedsuccessfull, Constants.OCHIN, baseFilename + ".csv", Constants.uploadochin);
                }
                return Tuple.Create(rowsInserted, true);
            }
            catch (Exception ex)
            {
                dbHelper.Log($"Error inserting CSV data from file '{csvFilePath}' into the table: {ex.Message}", Constants.ERROR, baseFilename, Constants.uploadochin);
                return Tuple.Create(0, false);
            }
        }

        private bool BatchExists(SqlConnection connection, int batchId)//BATCH DATA
        {
            try
            {
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Batch WHERE BatchID = @BatchID", connection))
                {
                    command.Parameters.AddWithValue("@BatchID", batchId);
                    return (int)command.ExecuteScalar() > 0;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return false;
            }
        }

        // Track the last progress value
        private int lastProgress = -1;
        private async Task UpdateProgress(int currentRowIndex, int totalRowsInCurrentFile)//to update no of lines inserted in progress bar 
        {
            try
            {
                progressBar.Minimum = 0;
                progressBar.Maximum = totalRowsInCurrentFile;

                if (currentRowIndex != lastProgress)
                {
                    int progressPercentage = (int)((double)currentRowIndex / totalRowsInCurrentFile * 100);
                    txtProgressbar.Text = $"{currentRowIndex}/{totalRowsInCurrentFile} ({progressPercentage}%)";
                    progressBar.Value = currentRowIndex;
                    lastProgress = currentRowIndex;

                    // Slow down the progress update
                    await Task.Delay(100); // Adding asynchronous delay of 100 milliseconds
                }
            }
            catch (Exception ex)
            {
                // Log the error instead of showing a message box
                dbHelper.Log($"Error updating progress: {ex.Message}", Constants.ERROR, "ProgressUpdate", Constants.uploadochin);
            }
        }
        private void btnBrowse_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
                {
                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    {
                        string selectedFolderPath = folderBrowserDialog.SelectedPath;
                        txtPath.Text = selectedFolderPath;

                        // Get all files in the selected folder
                        string[] files = Directory.GetFiles(selectedFolderPath);

                        // Check if the folder is empty
                        if (files.Length == 0)
                        {
                            MessageBox.Show("The selected folder is empty. Please select a folder containing .csv files.",
                                            "Error",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);
                            return;
                        }

                        // Check if all files in the folder have the .csv extension
                        bool allFilesAreCsv = files.All(file => Path.GetExtension(file).Equals(".csv", StringComparison.OrdinalIgnoreCase));

                        if (!allFilesAreCsv)
                        {
                            MessageBox.Show(Constants.ThefoldercontainsnonCSVfilesUploadisallowedonlyforCSVfiles,
                                            Constants.Ochin,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);
                            return;
                        }

                        // Save the folder path (you only need to save it once)
                        File.WriteAllText(Constants.LastFolderPathhcc, selectedFolderPath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Method to clear the file path
        private void btnClearPath_Click(object sender, EventArgs e)//to clear the pat
        {
            txtPath.Clear();
            // Save an empty string to the file when the path is cleared
            File.WriteAllText(Constants.LastFolderPathhcc, string.Empty);
        }
        private void descriptionformat()//load description data
        {
            try
            {
                DateTime currenttime = DateTime.Now;
                string datePart = currenttime.ToString("MM/dd/yyyy"); // Format the date as MM/DD/YYYY
                string timePart = currenttime.ToString("HH:mm:ss"); // Format the time as HH:mm:ss
                txtDesc.Text = $"OCHIN CSV Upload on {datePart} at {timePart}"; // Combine date and time
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnNxt_Click(object sender, EventArgs e)//Next FUNction
        {
            OCHIN_to_RWDE_Conversion OCHIN_to_RWDE_Conversion = new OCHIN_to_RWDE_Conversion();
            OCHIN_to_RWDE_Conversion.Show();
        }
        private void btnNext_Click(object sender, EventArgs e)// Handles the click event for the 'Next' button to navigate to the next page in the process.
        {
            try
            {
                frmMain mainForm = Application.OpenForms.OfType<frmMain>().FirstOrDefault();

                if (mainForm != null)

                    mainForm.ShowOCHINToHCCScreenHCC();//to navigate to required page
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
     
}

