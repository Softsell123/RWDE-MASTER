using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RWDE//
{
    public partial class FrmUploadOchinCsv : Form
    {
        private readonly string connectionString;
        private readonly DbHelper dbHelper;
        bool isUploading = false;
        public string Path;
        private int lastProgress = -1;
        public FrmUploadOchinCsv()//to initialize data
        {
            InitializeComponent();
            dbHelper = new DbHelper();
            connectionString = dbHelper.GetConnectionString();
            this.ControlBox = false;
            this.WindowState = FormWindowState.Maximized;
            Descriptionformat();
            RegisterEvents(this);
            if (File.Exists(Constants.LastFolderPathhcc))
            {
                string lastFolderPathhcc = File.ReadAllText(Constants.LastFolderPathhcc).Trim();  // Trim to remove any extra spaces or newlines
                // Check if the file content is not empty and the directory exists
                if (!string.IsNullOrEmpty(lastFolderPathhcc) && Directory.Exists(lastFolderPathhcc))
                {
                    txtPath.Text = lastFolderPathhcc;
                }
                else
                {
                    txtPath.Clear();  // Ensure the path is cleared if the file is empty or invalid
                }
            }
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
                    if (btnClose.Text == "Close")
                    {
                        this.Close();
                        Application.Restart();
                        return;
                    }
                    if (btnClose.Text == Constants.Abort)
                    {
                        DialogResult result = MessageBox.Show(Constants.Areyousureyouwanttoabort, "UPLOAD OCHIN CSV", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            int batchId = Convert.ToInt32(txtBatchid.Text);
                            //int currentBatchId = dbHelper.GetNextBatchIdabort(connection); // Get the current batch ID from the database

                            string fileName = txtFileName.Text;
                        
                            // Increment the batch ID
                            //int nextBatchId = currentBatchId + 1;

                            // Show confirmation message
                            MessageBox.Show(Constants.Abortedsuccessfully, "UPLOAD OCHIN CSV");

                                UpdateBatch(batchId, fileName, Path);
                                this.Close();
                                dbHelper.DeleteBatchochin(batchId.ToString());
                                Application.Restart();
                        }

                        
                    }
                    // Restart the application
                   
                }
                catch (Exception ex)
                {
                    // Handle any exceptions by showing the error message
                    MessageBox.Show(Constants.ErrorCode + ex.Message, Constants.ErrorCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void UpdateBatch(int batchid, string filename, string path)//to Update the data information 
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string description = Constants.Abortedfile;
                    DateTime startTime = DateTime.Now;
                   
                    dbHelper.InsertBatch(batchid, filename, path, Constants.OchinCode, description, startTime, 0, 0, Constants.Fileaborted);
                    
                //ClearTables(batchid);//to clear data in tables
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private async void btnUpload_Click(object sender, EventArgs e)//to upload data into table in database
        {
            string[] files = Directory.GetFiles(txtPath.Text);
            bool allFilesAreCsv = files.All(file => System.IO.Path.GetExtension(file).Equals(".csv", StringComparison.OrdinalIgnoreCase));

            if (!allFilesAreCsv)
            {
                MessageBox.Show(Constants.ThefoldercontainsnonCsVfilesUploadisallowedonlyforCsVfiles, Constants.Ochin, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            btnClose.Text = Constants.Abort;
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
                            ResetUi();
                            return;
                        }

                        string[] csvFiles = Directory.GetFiles(folderPath, "*.csv");
                        int totalCsvFiles = csvFiles.Length;
                        int currentCsvFileIndex = 0;
                        int totalRowsInserted = 0;
                        progressBarLines.Value = 0;
                        progressBarfile.Value = 0;
                        progressBarfile.Maximum = totalCsvFiles;

                        DateTime startTime = DateTime.Now;

                        int batchid = dbHelper.GetNextBatchId(); // Initialize the first batch ID to 1

                        foreach (string csvFilePath in csvFiles)
                        {
                            string baseFilename = System.IO.Path.GetFileNameWithoutExtension(csvFilePath).Split(new[] { " (" }, StringSplitOptions.RemoveEmptyEntries)[0];
                            string fileName = System.IO.Path.GetFileName(csvFilePath);
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
                            string formattime = startTime.ToString("MM/dd/yyyy HH:mm:ss");
                            txtUploadStarted.Text = formattime;
                            Path = csvFilePath;
                            if (isUploading)
                            {
                                // Await the async method call
                                var result = await InsertCsvDataIntoTable(csvFilePath, batchid, connection);

                                totalRowsInserted += result.Item1;
                                totalRowsInCurrentFile += result.Item1;
                                progressBarfile.Value = currentCsvFileIndex;

                                // Log successful insertion
                                fileName = System.IO.Path.GetFileName(csvFilePath);
                                string description = txtDesc.Text;
                                int successfulRows = result.Item1;
                                DateTime endtime = DateTime.Now;
                                if (isUploading)
                                {
                                    dbHelper.InsertBatch(batchid, fileName, csvFilePath, Constants.OchinCode, description, startTime, totalRowsInCurrentFile, successfulRows, Constants.StatusCode);
                                    UpdateFileProgress(currentCsvFileIndex, totalCsvFiles, totalRowsInserted);//no of files insertion
                                }
                            }

                            if (baseFilename.Contains(Constants.Clients))
                            {
                                //btnClose.Text = Constants.close;
                                TimeSpan elapsedTime = DateTime.Now - startTime;
                                string eTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                            }

                            if (baseFilename.Contains(Constants.Services))
                            {
                                //btnClose.Text = Constants.close;
                                TimeSpan elapsedTime = DateTime.Now - startTime;
                                string eTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                                double totalSeconds = elapsedTime.TotalSeconds;
                                txtUploadEnded.Text = eTime;
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
                        btnClose.Text = "Close";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Constants.ErrorMessage, ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ResetUi();
                }
            }
        }
        private void ResetUi()//to reset button
        {
            btnUpload.Enabled = true;
            btnClose.Text = "Close";
            btnBrowse.Enabled = true;
        }
        private async void UpdateFileProgress(int currentCsvFileIndex, int totalCsvFiles, int totalRowsInserted)//to show the progress of lines inserted
        {
            try
            {
                progressBarLines.Minimum = 0;
                progressBarLines.Maximum = totalCsvFiles;
                int progressPercentage = (int)((double)currentCsvFileIndex / totalCsvFiles * 100);
                // txtProgressbar.Text = $"{currentCsvFileIndex}/{totalCsvFiles} ({progressPercentage}%)";
                progressBarLines.Value = currentCsvFileIndex;
                txtProgressfile.Text = $"{currentCsvFileIndex}/{totalCsvFiles} ({progressPercentage}%)";

                await Task.Delay(50); // Slow down progress bar update
            }
            catch (Exception ex)
            {
                // Log error if needed
                dbHelper.Log($"Error updating file progress: {ex.Message}", Constants.Error, "ProgressUpdate", Constants.Uploadochin);
            }
        }
        public async Task<Tuple<int, bool>> InsertCsvDataIntoTable(string csvFilePath, int batchId, SqlConnection connection)//functionality to insert particular data 
        {
            int rowsInserted = 0;
            int totalRows = 0;
            string filename = System.IO.Path.GetFileNameWithoutExtension(csvFilePath);//to get file name
            string[] filenameParts = filename.Split(new[] { " (" }, StringSplitOptions.RemoveEmptyEntries);
            string baseFilename = filenameParts[0];

            List<string[]> clientData = new List<string[]>();
            List<string[]> serviceData = new List<string[]>();

            try
            {
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
                                dbHelper.Log(Constants.UploadforOchincsVstarted, Constants.OchinCode, baseFilename + ".csv", Constants.Uploadochin);
                                hasLoggedClient = true;
                            }
                        }
                        else if (baseFilename.Contains(Constants.Services))
                        {
                            serviceData.Add(data);

                            if (!hasLoggedService)
                            {
                                dbHelper.Log(Constants.UploadforOchincsVstarted, Constants.OchinCode, baseFilename + ".csv", Constants.Uploadochin);
                                hasLoggedService = true;
                            }
                        }
                    }
                }
               
                // Insert all client data first
                foreach (var data in clientData)
                {
                    if (chckPHI.Checked == true && chckURN.Checked == false)
                    {
                        dbHelper.InsertClientInformationPhi(connection, data, batchId);//insertion of the client file with PHI DATA MASKING CONDITION
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
                if (serviceData.Count == 0&& baseFilename.Contains(Constants.Services))
                {
                    MessageBox.Show(Constants.UploadingEmptyFile+baseFilename, Constants.Ochin, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                // Insert all service data next
                foreach (var data in serviceData)
                {
                    if (chckPHI.Checked == true)//InsertClientServiceDataPHI
                    {
                        dbHelper.InsertClientServiceData(connection, data, batchId);//insertion of the services file with PHI DATA MASKING CONDITION
                    }
                    else
                    {
                        dbHelper.InsertClientServiceData(connection, data, batchId);//insertion of the services file with PHI DATA MASKING CONDITION
                    }
                    rowsInserted++;
                    await UpdateProgress(rowsInserted, totalRows); // Await the progress update
                }
                if (isUploading)
                {
                    dbHelper.Log(Constants.UploadforOchincsVcompletedsuccessfull, Constants.OchinCode, baseFilename + ".csv", Constants.Uploadochin);
                }
                return Tuple.Create(rowsInserted, true);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("another process"))
                {
                    MessageBox.Show(Constants.TheFileisbeingUsedinanotherprocessClosethefileandTryagain+baseFilename, Constants.Ochin, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                dbHelper.Log($"Error inserting CSV data from file '{csvFilePath}' into the table: {ex.Message}", Constants.Error, baseFilename, Constants.Uploadochin);
                return Tuple.Create(0, false);
            }
        }
        // Track the last progress value
        private async Task UpdateProgress(int currentRowIndex, int totalRowsInCurrentFile)//to update no of lines inserted in progress bar 
        {
            try
            {
                progressBarLines.Minimum = 0;
                progressBarLines.Maximum = totalRowsInCurrentFile;

                if (currentRowIndex != lastProgress)
                {
                    int progressPercentage = (int)((double)currentRowIndex / totalRowsInCurrentFile * 100);
                    txtProgressLines.Text = $"{currentRowIndex}/{totalRowsInCurrentFile} ({progressPercentage}%)";
                    progressBarLines.Value = currentRowIndex;
                    lastProgress = currentRowIndex;

                    // Slow down the progress update
                    await Task.Delay(50); // Adding asynchronous delay of 100 milliseconds
                }
            }
            catch (Exception ex)
            {
                // Log the error instead of showing a message box
                dbHelper.Log($"Error updating progress: {ex.Message}", Constants.Error, "ProgressUpdate", Constants.Uploadochin);
            }
        }
        private void btnBrowse_Click(object sender, EventArgs e)
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
                        bool allFilesAreCsv = files.All(file => System.IO.Path.GetExtension(file).Equals(".csv", StringComparison.OrdinalIgnoreCase));

                        if (!allFilesAreCsv)
                        {
                            MessageBox.Show(Constants.ThefoldercontainsnonCsVfilesUploadisallowedonlyforCsVfiles,
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
        private void Descriptionformat()//load description data
        {
            try
            {
                DateTime currentTime = DateTime.Now;
                string datePart = currentTime.ToString("MM/dd/yyyy"); // Format the date as MM/DD/YYYY
                string timePart = currentTime.ToString("HH:mm:ss"); // Format the time as HH:mm:ss
                txtDesc.Text = $"OCHIN CSV Upload on {datePart} at {timePart}"; // Combine date and time
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnNext_Click(object sender, EventArgs e)// Handles the click event for the 'Next' button to navigate to the next page in the process.
        {
            try
            {
                FrmMain mainForm = Application.OpenForms.OfType<FrmMain>().FirstOrDefault();

                if (mainForm != null)
                    mainForm.ShowOchinToHccScreenHcc();//to navigate to required page
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

