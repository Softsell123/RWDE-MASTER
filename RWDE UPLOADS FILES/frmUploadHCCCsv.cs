using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace RWDE
{
    public partial class FrmUploadHccCsv : Form
    {
        private readonly string connectionString;
        private readonly DbHelper dbHelper;
        public string FileName;
        public int Batchid;
        bool isUploading;
        bool batchId = false;
        private string GetCurrentFilePath([CallerFilePath] string filePath = "") => filePath;//to get file path
        public FrmUploadHccCsv()//initializing of data
        {
            InitializeComponent();           
            dbHelper = new DbHelper();
            connectionString = dbHelper.GetConnectionString();//get connection string
            this.ControlBox = false;
            this.WindowState = FormWindowState.Maximized;
            DateTime currenttime = DateTime.Now;
            string date = currenttime.ToString("MM/dd/yyyy");
            string time = currenttime.ToString("HH:mm:ss");
            txtDesc.Text = $"HCC CSV Upload on {date} at {time}";
            string pathFile = Constants.LastFolderPathOchin;
            RegisterEvents(this);
            // Check if the file exists
            if (File.Exists(pathFile))
            {
                string lastFolderPathhcc = File.ReadAllText(pathFile).Trim();  // Trim to remove extra spaces or newlines
                // Check if the file content is not empty and the directory exists
                if (!string.IsNullOrEmpty(lastFolderPathhcc) && Directory.Exists(lastFolderPathhcc))
                {
                    txtpath.Text = lastFolderPathhcc;
                }
                else
                {
                    txtpath.Clear();  // Ensure the path is cleared if the file is empty or invalid
                }
            }
            else
            {
                txtpath.Clear();  // Clear the text box if the file doesn't exist
            }
        }
        public Panel PanelToReplace
        {
            get
            {
                return pnlCsvXml;
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
        private void btnUpload_Click(object sender, EventArgs e)//Uploding CSV files
        {
            try {
                if (string.IsNullOrEmpty(txtpath.Text))
                {
                    MessageBox.Show("The folder path cannot be empty. Please select a valid folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string[] filesempty = Directory.GetFiles(txtpath.Text);

                // Check if the folder is empty
                
                if (txtpath.Text == "" || txtpath == null || filesempty.Length == 0)
                {
                    MessageBox.Show(Constants.ThefolderisemptyPleaseuploadfiles, Constants.Ochin, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtpath.Text))
                {
                    MessageBox.Show(Constants.Selectafilebeforeuploading);
                    return; // Exit the method if the path is empty
                }
                string[] files = Directory.GetFiles(txtpath.Text);
                bool allFilesAreCsv = files.All(file => Path.GetExtension(file).Equals(".csv", StringComparison.OrdinalIgnoreCase));

                if (!allFilesAreCsv)
                {
                    MessageBox.Show(Constants.ThefoldercontainsnonCsVfilesUploadisallowedonlyforCsVfiles,Constants.Hccdata, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                btnClose.Text = Constants.Abort;
                btnUpload.Enabled = false;
                isUploading = true;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //SqlTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        string folderPath = txtpath.Text;

                        if (!string.IsNullOrEmpty(folderPath))
                        {
                            string existingBatchId = txtBatchid.Text;

                            // Check if the batch ID already exists and matches the previous batch ID
                            if (!string.IsNullOrEmpty(existingBatchId))
                            {
                                MessageBox.Show($"Batch ID {existingBatchId} already exists", Constants.Hccdata, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                btnUpload.Enabled = false;
                                btnClose.Text = Constants.Close;
                                btnBrowse.Enabled = false;
                                return;
                            }
                            if (string.IsNullOrEmpty(txtBatchid.Text) && string.IsNullOrEmpty(txtUploadStarted.Text))
                            {
                                btnUpload.Enabled = false;
                                string[] csvFiles = Directory.GetFiles(folderPath, "*.csv");
                                int totalCsvFiles = csvFiles.Length;
                                int currentCsvFileIndex = 0;
                                int totalRowsInserted = 0;
                                progressBarLines.Value = 0;
                                DateTime startTime = DateTime.Now;
                                progressBarfile.Value = 0;
                                progressBarfile.Maximum = totalCsvFiles;

                                foreach (string csvFilePath in csvFiles)
                                {
                                    UpdateFileProgress(currentFileIndex, totalCsvFiles, totalRowsInserted);//to update lines of insertion
                                    if (!isUploading)
                                        break;
                                    currentFileIndex++;
                                    string[] lines = File.ReadAllLines(csvFilePath);
                                    int totalRowsInCurrentFile = 0; // Initialize within the loop
                                    string baseFilename = Path.GetFileNameWithoutExtension(csvFilePath).Split(new[] { " (" }, StringSplitOptions.RemoveEmptyEntries)[0];
                                    string fileName = Path.GetFileName(csvFilePath);
                                    txtFileName.Text = fileName;

                                    Batchid = dbHelper.GetNextBatchId(batchId);//to get next batch id
                                    txtBatchid.Text = Batchid.ToString();
                                    string date = startTime.ToString("MM/dd/yyyy");
                                    string time = startTime.ToString("HH:mm:ss");
                                    txtDesc.Text = $"HCC CSV Upload on {date} at {time}";
                                    string formattime = startTime.ToString("MM/dd/yyyy HH:mm:ss");
                                    txtUploadStarted.Text = formattime;

                                    if (isUploading)
                                    {
                                        var result = InsertCsvDataIntoTable(csvFilePath, Batchid, connection); // Pass connection and transaction objects
                                        totalRowsInserted += result.Item1;

                                        // Increment current file index
                                        currentCsvFileIndex++;

                                        totalRowsInCurrentFile += result.Item1;
                                        progressBarfile.Value = currentCsvFileIndex;

                                        // Log successful insertion
                                        baseFilename = Path.GetFileNameWithoutExtension(csvFilePath).Split(new[] { " (" }, StringSplitOptions.RemoveEmptyEntries)[0];
                                        fileName = Path.GetFileName(csvFilePath);

                                        string description = txtDesc.Text;
                                        int successfulRows = result.Item1;

                                        if (isUploading)
                                        {
                                            dbHelper.InsertBatch(Batchid, fileName, baseFilename, Constants.Hcc, description, DateTime.Now, totalRowsInCurrentFile, successfulRows, Constants.StatusCode);
                                            UpdateFileProgress(currentCsvFileIndex, totalCsvFiles, totalRowsInserted);
                                        }
                                    }
                                }
                                DateTime endTime = DateTime.Now;
                                TimeSpan totalTime = endTime - startTime;
                                string eTime = endTime.ToString("MM/dd/yyyy HH:mm:ss");
                                double totalSeconds = totalTime.TotalSeconds;
                                txtUploadEnded.Text = eTime;
                                txtTotaltime.Text = $"{totalSeconds:F2} Seconds";
                                btnClose.Text = Constants.Close;
                                btnUpload.Enabled = true;
                            }
                            else
                            {
                                MessageBox.Show(Constants.ThesefileshavealreadybeenuploadedCloseandreopentouploadnewfiles,Constants.Hccdata, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                btnUpload.Enabled = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show(Constants.NoFolderSelectedMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        var st = new StackTrace(ex, true);
                        var frame = st.GetFrames().FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                        int lineNumber = frame != null ? frame.GetFileLineNumber() : 0;
                        dbHelper.LogError(ex.Message, GetCurrentFilePath(), ex.StackTrace, nameof(btnUpload_Click), FileName, lineNumber);
                        MessageBox.Show(string.Format(Constants.ErrorMessage, ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public Tuple<int, bool> InsertCsvDataIntoTable(string csvFilePath, int batchid, SqlConnection connection)//Insertion of CSV DATA into tables
        {
            int rowsInserted = 0;
            int totalRows = 0;
            string filename = Path.GetFileNameWithoutExtension(csvFilePath);
            string fileNameWithExtension = Path.GetFileName(csvFilePath);
            string[] filenameParts = filename.Split(new[] { " (" }, StringSplitOptions.RemoveEmptyEntries);
            string baseFilename = filenameParts[0];
            try
            {
                dbHelper.Log($"Upload for {baseFilename} has started", Constants.Hcc, baseFilename+".csv", Constants.Uploadhcc);

                // No need to open the connection here as it's already open

                using (StreamReader reader = new StreamReader(csvFilePath))
                {
                    string headerLine = reader.ReadLine();
                    totalRows = File.ReadLines(csvFilePath).Count() - 1;

                    while (!reader.EndOfStream)
                    {
                        if (!isUploading)
                            break;
                        string line = reader.ReadLine();

                        if (string.IsNullOrEmpty(line))
                            continue;

                        string[] data = line.Split(',');

                        if (baseFilename == Constants.AriesClients)
                        {
                            if (chckPHI.Checked == true)
                            {
                                dbHelper.InsertClientDataPhi(connection, data, batchid, fileNameWithExtension);//insertion of aries clients
                            }
                            else
                            {
                                dbHelper.InsertClientData(connection, data, batchid, fileNameWithExtension);//insertion of aries clients
                            }
                            rowsInserted++;
                        }
                        if (baseFilename == Constants.AriesDeceased)//insertion of AriesDeceased
                        {
                            dbHelper.InsertdeceasedData(connection, data, batchid, fileNameWithExtension);//insertion of deceased clients
                            rowsInserted++;
                        }
                        if (baseFilename == Constants.AriesConsent)//insertion of AriesConsent
                        {
                            dbHelper.InsertConsentData(connection, data, batchid, fileNameWithExtension);//insertion of InsertConsentData
                            rowsInserted++;
                        }
                        if (baseFilename == Constants.AriesEligibility)//insertion of AriesEligibility
                        {
                            dbHelper.InsertDlEligibility(connection, data, batchid, fileNameWithExtension);
                            rowsInserted++;
                        }
                        if (baseFilename == Constants.AriesServices)//insertion of AriesServices
                        {
                            if (chckPHI.Checked == true)
                            {
                                int row = rowsInserted;
                                dbHelper.InsertDlServicesPhi(connection, data, batchid, fileNameWithExtension, row);
                                rowsInserted++;//insertion of aries clients
                            }
                            else
                            {
                                int row = rowsInserted;
                                dbHelper.InsertDlServices(connection, data, batchid, fileNameWithExtension, row);
                                rowsInserted++;//insertion of AriesServices
                            }
                            // rowsInserted++;
                            //rowsInserted++;
                        }
                        if (baseFilename == Constants.AriesFinanacial)//insertion of AriesFinanacial
                        {
                            dbHelper.InsertDlFinancials(connection, data, batchid, fileNameWithExtension);//insertion of AriesFinanacial
                            rowsInserted++;//
                            //rowsInserted++;
                        }
                        if (isUploading)
                        {
                            UpdateProgress(rowsInserted, totalRows);//progress or rows inserted
                        }
                    }
                }
                // No need to close the connection here as it will be handled externally
                if (isUploading)
                {
                    dbHelper.Log($"Upload for {baseFilename} has completed", Constants.OchinCode, baseFilename+".csv", Constants.Uploadhcc);
                }
                return Tuple.Create(rowsInserted, true);
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrames().FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame != null ? frame.GetFileLineNumber() : 0;
                dbHelper.LogError(ex.Message, GetCurrentFilePath(), ex.StackTrace, nameof(InsertCsvDataIntoTable), FileName, lineNumber);
                return Tuple.Create(0, false);
            }
        }
        private async void UpdateProgress(int currentRowIndex, int totalRowsInCurrentFile)//progress of total rows insertion of csv files 
        {
            try
            {
                if (currentRowIndex < totalRowsInCurrentFile)
                {
                    currentRowIndex++;
                    double percentage = ((double)currentRowIndex / totalRowsInCurrentFile) * 100;
                    int progress = (int)Math.Round(percentage); // Round the percentage to the nearest integer
                    txtProgressLines.Text = $"{currentRowIndex}/{totalRowsInCurrentFile} ({progress}%)";
                    progressBarLines.Value = progress;
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateFileProgress(int currentFileIndex, int totalFiles, int totalRowsInserted)//progress of total files insertion 
        {
            try
            {
                // Update the progress bar value directly with the current file index
                progressBarfile.Value = isUploading ? currentFileIndex : 0;

                int progressPercentage = (int)((double)currentFileIndex / totalFiles * 100);
                // Update the progress text with current file progress
                txtProgressfile.Text = $"{currentFileIndex}/{totalFiles} ({progressPercentage}%)";

                // Refresh the UI
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e) //close the window
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
                        DialogResult result = MessageBox.Show(Constants.Areyousureyouwanttoabort, "UPLOAD HCC CSV", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            int batchId = Convert.ToInt32(txtBatchid.Text);

                            string fileName = txtFileName.Text;

                            // Show confirmation message
                            MessageBox.Show(Constants.Abortedsuccessfully, "UPLOAD HCC CSV");
                            String path = txtpath.Text;
                            UpdateBatch(batchId, fileName,path);
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
        private void UpdateBatch(int batchId, string fileName, string path)//getting information of the entired data
        {
            try
            {
                DateTime currentTime = DateTime.Now;
                int totalRows = 0; // Assuming no rows were successfully processed
                int successfulRows = 0;
                dbHelper.InsertBatch(batchId, fileName, path, Constants.Hcc, null, currentTime, totalRows, successfulRows, 12);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{Constants.ErrorLoadingData} {ex.Message}");
                // Log or handle the exception appropriately
            }
        }
        private void btnBrowse_Click(object sender, EventArgs e)//selecting csv files 
        {
            try
            {
                using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
                {
                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    {
                        string selectedFolderPath = folderBrowserDialog.SelectedPath;
                        txtpath.Text = selectedFolderPath;

                        // Check if the folder contains only XML files
                        string[] files = Directory.GetFiles(selectedFolderPath);

                        // Check if all files have the .xml extension
                        bool allFilesAreXml = files.All(file => Path.GetExtension(file).Equals(".csv", StringComparison.OrdinalIgnoreCase));

                        if (!allFilesAreXml)
                        {
                            MessageBox.Show(Constants.ThefoldercontainsnonCsVfilesUploadisallowedonlyforCsVfiles,Constants.Hccdata, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        // Save the path to the file
                        File.WriteAllText(Constants.LastFolderPathOchin, selectedFolderPath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)//to navigate to next page
        {
            try
            {
                FrmMain mainForm = Application.OpenForms.OfType<FrmMain>().FirstOrDefault();
                if (mainForm != null)
                {
                    mainForm.ShowOchinToHccScreen();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
  
