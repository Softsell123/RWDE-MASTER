using DocumentFormat.OpenXml.Drawing.Diagrams;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RWDE 
{
    public partial class FrmUploadOchinCsv : Form
    {
        private readonly DbHelper dbHelper;
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public FrmUploadOchinCsv() // to initialize data
        {
            InitializeComponent();
            dbHelper = new DbHelper();

            ControlBox = false;
            WindowState = FormWindowState.Maximized;

            // load description data
            Descriptionformat();
            RegisterEvents(this); // Assigning events to all Controls
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
                if (control is Button || control is CheckBox || control is DateTimePicker)
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

        private void btnClose_Click(object sender, EventArgs e)// To Close the form and Restart the Application
        {
            try
            {
                if (btnClose.Text == Constants.Close)
                {
                    Close();
                    Application.Restart();
                    return;
                }
                if (btnClose.Text == Constants.Abort)
                {
                    DialogResult result = MessageBox.Show(Constants.Areyousureyouwanttoabort, Constants.UploadOchinCsv, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        _cancellationTokenSource.Cancel();
                        string folderPath = txtPath.Text;
                        string[] csvFiles = Directory.GetFiles(folderPath, Constants.AllCsvExtention);

                        int batchId = Convert.ToInt32(txtBatchid.Text);// Get the current batch ID

                        foreach (string csvFilePath in csvFiles)
                        {
                            string fileName = Path.GetFileName(csvFilePath);
                            if(txtFileName.Text== fileName)
                            {
                                // to Update the Aborted data information 
                                UpdateBatch(batchId, fileName, csvFilePath);
                            }
                        }
                        // Delete All Values form all Ochin tables 
                        dbHelper.DeleteBatchochin(batchId.ToString());
                        if (dbHelper.ErrorOccurred)
                        {
                            MessageBox.Show(Constants.ErrorOccurred);
                            return;
                        }
                        // Show confirmation message
                        _cancellationTokenSource.Cancel(); // Signal cancellation
                        MessageBox.Show(Constants.AbortedSuccessfully, Constants.UploadOchinCsv);
                        Close();
                        Application.Restart();
                    }
                }
                // Restart the application
            }
            catch (Exception ex)
            {
                // Handle any exceptions by showing the error message
                MessageBox.Show(Constants.ErrorCode + ex.Message, Constants.ErrorCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Restart();
            }

        }
        private void UpdateBatch(int batchid, string filename, string path)// to Update the Aborted data information 
        {
            try
            {
                string description = Constants.Abortedfile;
                DateTime startTime = DateTime.Now;

                // update batch status in database
                dbHelper.InsertBatch(batchid, filename, path, Constants.OchinCode, description, startTime, 0, 0, Constants.Fileaborted);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnUpload_Click(object sender, EventArgs e) // to upload data into table in database
        {
            try
            {
                if (ConfigurationManager.ConnectionStrings[Constants.MyConnection].ConnectionString.Contains(Constants.Dev))
                {
                    DialogResult result = MessageBox.Show(Constants.DevEnvironment, Constants.UploadOchinCsv, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.No)
                    {
                        return;
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show(Constants.ProductionEnvironment, Constants.UploadOchinCsv, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.No)
                    {
                        return;
                    }
                }

                if (string.IsNullOrEmpty(txtPath.Text))
                {
                    MessageBox.Show(Constants.TheFolderPathCannotBeEmpty, Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get all files in the selected folder
                string[] filesempty = Directory.GetFiles(txtPath.Text);

                // Check if the folder is empty
                if (txtPath.Text == "" || txtPath == null || filesempty.Length == 0)
                {
                    MessageBox.Show(Constants.ThefolderisemptyPleaseuploadfiles, Constants.Ochin, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (filesempty.Length > 2)
                {
                    MessageBox.Show(Constants.Thefolderhasmorethantwofileorduplicatefiles, Constants.Ochin, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPath.Text))
                {
                    MessageBox.Show(Constants.Selectafilebeforeuploading);
                    return; // Exit the method if the path is empty
                }
                string[] files = Directory.GetFiles(txtPath.Text);
                bool allFilesAreCsv = files.All(file => System.IO.Path.GetExtension(file).Equals(Constants.CsvExtention, StringComparison.OrdinalIgnoreCase));

                if (!allFilesAreCsv)
                {
                    MessageBox.Show(Constants.ThefoldercontainsnonCsVfilesUploadisallowedonlyforCsVfiles, Constants.Ochin, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                btnClose.Text = Constants.Abort;
                btnUpload.Enabled = false;
                btnNext.Enabled = false;
                bool isUploading = true;

                SqlConnection connection = dbHelper.GetConnection();
                try
                {
                    string folderPath = txtPath.Text;

                    if (!string.IsNullOrEmpty(folderPath))
                    {
                        string existingBatchId = txtBatchid.Text;

                        // Check if the batch ID already exists
                        if (!string.IsNullOrEmpty(existingBatchId))
                        {
                            MessageBox.Show($@"{Constants.BatchIdAlreadyExistsCloseAndReopen.Replace("{existingBatchId}", existingBatchId)}", Constants.BatchIdExists, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            ResetUi();
                            return;
                        }

                        string[] csvFiles = Directory.GetFiles(folderPath, Constants.AllCsvExtention);
                        int totalCsvFiles = csvFiles.Length;
                        int currentCsvFileIndex = 0;
                        int totalRowsInserted = 0;
                        progressBarLines.Value = 0;
                        progressBarfile.Value = 0;
                        progressBarfile.Maximum = totalCsvFiles;

                        DateTime startTime = DateTime.Now;

                        int batchid = dbHelper.GetNextBatchId(); // to get the Next Batch ID
                        if (dbHelper.ErrorOccurred)
                        {
                            MessageBox.Show(Constants.ErrorOccurred);
                            return;
                        }

                        foreach (string csvFilePath in csvFiles)
                        {
                            if (_cancellationTokenSource.Token.IsCancellationRequested)
                            {
                                break; // Exit the loop if cancellation is requested
                            }

                            string baseFilename = Path.GetFileNameWithoutExtension(csvFilePath).Split(new[] { " (" }, StringSplitOptions.RemoveEmptyEntries)[0];
                            string fileName = Path.GetFileName(csvFilePath);
                            txtFileName.Text = fileName;
                            if (!isUploading)
                            {
                                break;
                            }

                            UpdateFileProgress(currentCsvFileIndex, totalCsvFiles, totalRowsInserted);// to update the file Progress
                            currentCsvFileIndex++;

                            int totalRowsInCurrentFile = 0; // Initialize within the loop

                            txtBatchid.Text = batchid.ToString();
                            string date = startTime.ToString(Constants.MMddyyyybkslash);
                            string time = startTime.ToString(Constants.HHmmss);
                            txtDesc.Text = $@"{Constants.OchinCsvUploadonAt.Replace("{date}", date).Replace("{time}", time)}";
                            string formattime = startTime.ToString(Constants.MMddyyyyHHmmssbkslash);
                            txtUploadStarted.Text = formattime;

                            if (isUploading)
                            {
                                // Await the async method call
                                var result = await InsertCsvDataIntoTable(csvFilePath, batchid, connection, _cancellationTokenSource.Token);

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
                                    if (_cancellationTokenSource.Token.IsCancellationRequested)
                                    {
                                        break; // Exit the loop if cancellation is requested
                                    }

                                    // update batch status in database
                                    dbHelper.InsertBatch(batchid, fileName, csvFilePath, Constants.OchinCode, description, startTime, totalRowsInCurrentFile, successfulRows, Constants.StatusCode);
                                    if (dbHelper.ErrorOccurred)
                                    {
                                        MessageBox.Show(Constants.ErrorOccurred);
                                        return;
                                    }

                                    UpdateFileProgress(currentCsvFileIndex, totalCsvFiles, totalRowsInserted); // no of files insertion progress
                                }
                            }

                            if (baseFilename.Contains(Constants.Clients))
                            {
                                // btnClose.Text = Constants.close;
                                TimeSpan elapsedTime = DateTime.Now - startTime;
                                string eTime = DateTime.Now.ToString(Constants.MMddyyyyHHmmssbkslash);
                            }

                            if (baseFilename.Contains(Constants.Services))
                            {
                                // btnClose.Text = Constants.close;
                                TimeSpan elapsedTime = DateTime.Now - startTime;
                                string eTime = DateTime.Now.ToString(Constants.MMddyyyyHHmmssbkslash);
                                double totalSeconds = elapsedTime.TotalSeconds;
                                txtUploadEnded.Text = eTime;
                                txtTotaltime.Text = $@"{totalSeconds:F2} {Constants.Seconds}";
                                btnUpload.Enabled = true;
                                btnNext.Enabled = true;
                                btnClose.Text = Constants.Close;
                            }
                            else
                            {
                                btnUpload.Enabled = true;
                                btnNext.Enabled = true;
                            }
                        }
                    }
                    else
                    {
                        btnUpload.Enabled = true;
                        btnNext.Enabled = true;
                        btnClose.Text = Constants.Close;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Constants.ErrorMessagedynamic, ex.Message), Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ResetUi();
                }
            }
            catch (Exception)
            {
                throw; // TODO handle exception
            }
            finally
            {
                btnUpload.Enabled = true;
                btnNext.Enabled = true;
                btnClose.Text = Constants.Close;
            }
        }

        private void ResetUi() // to reset button
        {
            try
            {
                btnUpload.Enabled = true;
                btnNext.Enabled = true;
                btnClose.Text = Constants.Close;
                btnBrowse.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void UpdateFileProgress(int currentCsvFileIndex, int totalCsvFiles, int totalRowsInserted) // to show the progress of lines inserted
        {
            try
            {
                progressBarLines.Minimum = 0;
                progressBarLines.Maximum = totalCsvFiles;
                int progressPercentage = (int)((double)currentCsvFileIndex / totalCsvFiles * 100);
                progressBarLines.Value = currentCsvFileIndex;
                txtProgressfile.Text = $@"{currentCsvFileIndex}/{totalCsvFiles} ({progressPercentage}%)";

                await Task.Delay(15); // Slow down progress bar update
            }
            catch (Exception ex)
            {
                // Log error if needed
                dbHelper.Log($"{Constants.ErrorUpdatingFileProgress} {ex.Message}", Constants.Error, Constants.ProgressUpdate, Constants.Uploadochin);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
            }
        }

        public async Task<Tuple<int, bool>> InsertCsvDataIntoTable(string csvFilePath, int batchId, SqlConnection connection, CancellationToken cancellationToken) // functionality to insert particular data 
        {
            int rowsInserted = 0;
            int totalRows = 0;
            string filename = System.IO.Path.GetFileNameWithoutExtension(csvFilePath); // to get file name
            string[] filenameParts = filename.Split(new[] { " (" }, StringSplitOptions.RemoveEmptyEntries);
            string baseFilename = filenameParts[0];

            List<string[]> clientData = new List<string[]>();
            List<string[]> serviceData = new List<string[]>();

            bool isUploading = true;
            bool isOchinFile = false;
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
                        if (cancellationToken.IsCancellationRequested)
                        {
                            break;
                            // Exit the loop if cancellation is requested
                        }

                        if (!isUploading)
                        {
                            break;
                        }

                        string line = reader.ReadLine();

                        if (string.IsNullOrEmpty(line))
                        {
                            continue;
                        }

                        line = line.Trim('"');
                        string[] data = line.Split('|');

                        if (baseFilename.Contains(Constants.Clients))
                        {
                            clientData.Add(data);

                            if (!hasLoggedClient)
                            {
                                if (baseFilename.Contains(Constants.Hcc))
                                {
                                    dbHelper.Log(Constants.UploadforOchincsVstarted, Constants.OchinCode, baseFilename + Constants.CsvExtention, Constants.Uploadochin);
                                    isOchinFile = true;
                                }
                                else
                                {
                                    dbHelper.Log(Constants.UploadforClientTrackcsvstarted, Constants.ClientTrackCode, baseFilename + Constants.CsvExtention, Constants.UploadCT);
                                }

                                if (dbHelper.ErrorOccurred)
                                {
                                    MessageBox.Show(Constants.ErrorOccurred);
                                    break;
                                }

                                hasLoggedClient = true;
                            }
                        }
                        else if (baseFilename.Contains(Constants.Services))
                        {
                            serviceData.Add(data);

                            if (!hasLoggedService)
                            {
                                if (baseFilename.Contains(Constants.Hcc))
                                {
                                    dbHelper.Log(Constants.UploadforOchincsVstarted, Constants.OchinCode, baseFilename + Constants.CsvExtention, Constants.Uploadochin);
                                    isOchinFile = true;
                                }
                                else
                                {
                                    dbHelper.Log(Constants.UploadforClientTrackcsvstarted, Constants.ClientTrackCode, baseFilename + Constants.CsvExtention, Constants.UploadCT);
                                }

                                if (dbHelper.ErrorOccurred)
                                {
                                    MessageBox.Show(Constants.ErrorOccurred);
                                    break;
                                }

                                hasLoggedService = true;
                            }
                        }
                    }
                }

                // Insert all client data first
                foreach (var data in clientData)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return Tuple.Create(0, false);

                        // break; Exit the loop if cancellation is requested
                    }

                    if (chckPHI.Checked == true && chckURN.Checked == false)
                    {
                        dbHelper.InsertClientInformationPhi(connection, data, batchId, baseFilename); // insertion of the client file with PHI DATA MASKING CONDITION
                        if (dbHelper.ErrorOccurred)
                        {
                            MessageBox.Show(Constants.ErrorOccurred);
                            break;
                        }
                    }
                    else if (chckURN.Checked == true)
                    {
                        dbHelper.InsertClientInformationphiurn(connection, data, batchId, baseFilename); // insertion of the client file without PHI DATA MASKING CONDITION
                        if (dbHelper.ErrorOccurred)
                        {
                            MessageBox.Show(Constants.ErrorOccurred);
                            break;
                        }
                    }
                    else
                    {
                        if (isOchinFile)
                        {
                            dbHelper.InsertClientInformationOchin(connection, data, batchId, baseFilename); // insertion of the client file without PHI DATA MASKING CONDITION
                        }
                        else
                        {
                            dbHelper.InsertClientInformationCt(connection, data, batchId, baseFilename); // insertion of the client file without PHI DATA MASKING CONDITION
                        }

                        if (dbHelper.ErrorOccurred)
                        {
                            MessageBox.Show(Constants.ErrorOccurred);
                            break;
                        }
                    }

                    rowsInserted++;
                    if (isUploading)
                    {
                        await UpdateProgress(rowsInserted, totalRows); // Await the progress update
                    }
                }

                if (serviceData.Count == 0 && baseFilename.Contains(Constants.Services))
                {
                    MessageBox.Show(Constants.UploadingEmptyFile+baseFilename, Constants.Ochin, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // Insert all service data next
                foreach (var data in serviceData)
                {
                    if (chckPHI.Checked == true) // InsertClientServiceDataPHI
                    {
                        dbHelper.InsertClientServiceDataPhi(connection, data, batchId, baseFilename); // insertion of the services file with PHI DATA MASKING CONDITION
                        if (dbHelper.ErrorOccurred)
                        {
                            MessageBox.Show(Constants.ErrorOccurred);
                            break;
                        }
                    }
                    else
                    {
                        if (isOchinFile)
                        {
                            dbHelper.InsertClientServiceDataOchin(connection, data, batchId, baseFilename); // insertion of the services file with PHI DATA MASKING CONDITION
                        }
                        else
                        {
                            dbHelper.InsertClientServiceDataCT(connection, data, batchId, baseFilename); // insertion of the services file with PHI DATA MASKING CONDITION                        }
                        }

                        if (dbHelper.ErrorOccurred)
                        {
                            MessageBox.Show(Constants.ErrorOccurred);
                            break;
                        }
                    }

                    rowsInserted++;
                    await UpdateProgress(rowsInserted, totalRows); // Await the progress update
                }
                if (isUploading)
                {
                    dbHelper.Log(Constants.UploadforOchincsVcompletedsuccessfull, Constants.OchinCode, baseFilename + Constants.CsvExtention, Constants.Uploadochin);
                    if (dbHelper.ErrorOccurred)
                    {
                        MessageBox.Show(Constants.ErrorOccurred);
                        return Tuple.Create(0, false);
                    }
                }
                return Tuple.Create(rowsInserted, true);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(Constants.AnotherProcess))
                {
                    MessageBox.Show(Constants.TheFileisbeingUsedinanotherprocessClosethefileandTryagain+baseFilename, Constants.Ochin, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                dbHelper.Log($"{Constants.ErrorInsertingCsvDataFromFileIntoTheTable.Replace("{csvFilePath}", csvFilePath)}", Constants.Error, baseFilename, Constants.Uploadochin);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred); 
                    return Tuple.Create(0, false);
                }

                return Tuple.Create(0, false);
            }
        }

        // Track the last progress value
        private async Task UpdateProgress(int currentRowIndex, int totalRowsInCurrentFile) // to update no of lines inserted in progress bar 
        {
            try
            {
                int lastProgress = -1;
                progressBarLines.Minimum = 0;
                progressBarLines.Maximum = totalRowsInCurrentFile;

                if (currentRowIndex != lastProgress)
                {
                    int progressPercentage = (int)((double)currentRowIndex / totalRowsInCurrentFile * 100);
                    txtProgressLines.Text = $@"{currentRowIndex}/{totalRowsInCurrentFile} ({progressPercentage}%)";
                    progressBarLines.Value = currentRowIndex;
                    lastProgress = currentRowIndex;

                    // Slow down the progress update
                    await Task.Delay(15); // Adding asynchronous delay of 0.015 milliseconds
                }
            }
            catch (Exception ex)
            {
                // Log the error instead of showing a message box
                dbHelper.Log($"{Constants.ErrorUpdatingFileProgress}{ex.Message}", Constants.Error, Constants.ProgressUpdate, Constants.Uploadochin);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e) // to select the folder containing Ochin Csv file
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
                            MessageBox.Show( Constants.TheSelectedFolderIsEmpty, Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Check if all files in the folder have the .csv extension
                        bool allFilesAreCsv = files.All(file => System.IO.Path.GetExtension(file).Equals(Constants.CsvExtention, StringComparison.OrdinalIgnoreCase));

                        if (!allFilesAreCsv)
                        {
                            MessageBox.Show(Constants.ThefoldercontainsnonCsVfilesUploadisallowedonlyforCsVfiles, Constants.Ochin,  MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void Descriptionformat() // load description data
        {
            try
            {
                DateTime currentTime = DateTime.Now;
                string date = currentTime.ToString(Constants.MMddyyyybkslash); // Format the date as MM/DD/YYYY
                string time = currentTime.ToString(Constants.HHmmss); // Format the time as HH:mm:ss
                txtDesc.Text = $@"{Constants.OchinCsvUploadonAt.Replace("{date}", date).Replace("{time}", time)}"; // Combine date and time
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNext_Click(object sender, EventArgs e) // Handles the click event for the 'Next' button to navigate to the next page in the process.
        {
            try
            {
                FrmMain mainForm = Application.OpenForms.OfType<FrmMain>().FirstOrDefault();

                if (mainForm != null)
                {
                    mainForm.ShowOchinToHccScreenHcc(); // to navigate to Ochin to Rwde Conversion Page
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}