using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RWDE
{
    public partial class FrmUploadHccCsv : Form
    {
        private readonly DbHelper dbHelper;

        public FrmUploadHccCsv()//initializing of data
        {
            InitializeComponent();
            dbHelper = new DbHelper();

            ControlBox = false;
            WindowState = FormWindowState.Maximized;
            DateTime currenttime = DateTime.Now;
            string date = currenttime.ToString(Constants.MMddyyyybkslash);
            string time = currenttime.ToString(Constants.HHmmss);
            txtDesc.Text = $@"{Constants.HccCsvUploadonAt.Replace("{date}", date).Replace("{time}", time)}";
            string pathFile = Constants.LastFolderPathOchin;
            RegisterEvents(this); //Assigning events to all Controls
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
        private void RegisterEvents(Control parent)//Assigning events to all Controls
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
        private void btnUpload_Click(object sender, EventArgs e)//Uploading CSV files
        {
            try
            {
                if (string.IsNullOrEmpty(txtpath.Text))
                {
                    MessageBox.Show(Constants.TheFolderPathCannotBeEmpty, Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                bool allFilesAreCsv = files.All(file => Path.GetExtension(file).Equals(Constants.CsvExtention, StringComparison.OrdinalIgnoreCase));

                if (!allFilesAreCsv)
                {
                    MessageBox.Show(Constants.ThefoldercontainsnonCsVfilesUploadisallowedonlyforCsVfiles, Constants.Hcc, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                btnClose.Text = Constants.Abort;
                btnUpload.Enabled = false;
                bool isUploading = true;
                SqlConnection connection=dbHelper.GetConnection();
                    string fileName = "";

                    try
                    {
                        string folderPath = txtpath.Text;

                        if (!string.IsNullOrEmpty(folderPath))
                        {
                            string existingBatchId = txtBatchid.Text;

                            // Check if the batch ID already exists and matches the previous batch ID
                            if (!string.IsNullOrEmpty(existingBatchId))
                            {
                                MessageBox.Show($@"{Constants.BatchIdAlreadyExists.Replace("{existingBatchId}", existingBatchId)}", Constants.Hcc, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                btnUpload.Enabled = false;
                                btnClose.Text = Constants.Close;
                                btnBrowse.Enabled = false;
                                return;
                            }
                            if (string.IsNullOrEmpty(txtBatchid.Text) && string.IsNullOrEmpty(txtUploadStarted.Text))
                            {
                                btnUpload.Enabled = false;
                                string[] csvFiles = Directory.GetFiles(folderPath, Constants.AllCsvExtention);
                                int totalCsvFiles = csvFiles.Length;
                                int currentCsvFileIndex = 0;
                                int totalRowsInserted = 0;
                                progressBarLines.Value = 0;
                                DateTime startTime = DateTime.Now;
                                progressBarfile.Value = 0;
                                progressBarfile.Maximum = totalCsvFiles;

                                foreach (string csvFilePath in csvFiles)
                                {
                                    UpdateFileProgress(currentFileIndex, totalCsvFiles, isUploading);//to update lines of insertion
                                    if (!isUploading)
                                        break;
                                    currentFileIndex++;
                                    string[] lines = File.ReadAllLines(csvFilePath);
                                    int totalRowsInCurrentFile = 0; // Initialize within the loop
                                    string baseFilename = Path.GetFileNameWithoutExtension(csvFilePath).Split(new[] { " (" }, StringSplitOptions.RemoveEmptyEntries)[0];
                                    fileName = Path.GetFileName(csvFilePath);
                                    txtFileName.Text = fileName;
                                    //bool batchId = false; 
                                    int batchid;
                                    batchid = dbHelper.GetNextBatchId(false);//to get next batch id
                                    if (dbHelper.ErrorOccurred)
                                    {
                                        MessageBox.Show(Constants.ErrorOccurred);
                                        return;
                                    }

                                    txtBatchid.Text = batchid.ToString();
                                    string date = startTime.ToString(Constants.MMddyyyybkslash);
                                    string time = startTime.ToString(Constants.HHmmss);
                                    txtDesc.Text = $@"{Constants.HccCsvUploadonAt.Replace("{date}", date).Replace("{time}", time)}";
                                    string formattime = startTime.ToString(Constants.MMddyyyyHHmmssbkslash);
                                    txtUploadStarted.Text = formattime;

                                    if (isUploading)
                                    {
                                        var result = InsertCsvDataIntoTable(csvFilePath, batchid, connection, isUploading); // Pass connection and transaction objects
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
                                            dbHelper.InsertBatch(batchid, fileName, baseFilename, Constants.HccCode, description, DateTime.Now, totalRowsInCurrentFile, successfulRows, Constants.StatusCode);
                                            if (dbHelper.ErrorOccurred)
                                            {
                                                MessageBox.Show(Constants.ErrorOccurred);
                                                return;
                                            }
                                            //progress of total files insertion 
                                            UpdateFileProgress(currentCsvFileIndex, totalCsvFiles, isUploading);
                                        }
                                    }
                                }
                                DateTime endTime = DateTime.Now;
                                TimeSpan totalTime = endTime - startTime;
                                string eTime = endTime.ToString(Constants.MMddyyyyHHmmssbkslash);
                                double totalSeconds = totalTime.TotalSeconds;
                                txtUploadEnded.Text = eTime;
                                txtTotaltime.Text = $@"{totalSeconds:F2} {Constants.Seconds}";
                                btnClose.Text = Constants.Close;
                                btnUpload.Enabled = true;
                            }
                            else
                            {
                                MessageBox.Show(Constants.ThesefileshavealreadybeenuploadedCloseandreopentouploadnewfiles, Constants.Hcc, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                btnUpload.Enabled = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show(Constants.NoFolderSelectedMessage, Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        var st = new StackTrace(ex, true);
                        var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                        int lineNumber = frame?.GetFileLineNumber() ?? 0;
                        dbHelper.LogError(ex.Message, ex.StackTrace, nameof(btnUpload_Click), fileName, lineNumber,Constants.HccCode);
                        MessageBox.Show(string.Format(Constants.ErrorMessagedynamic, ex.Message), Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (dbHelper.ErrorOccurred)
                        {
                            return;
                        }
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public  Tuple<int, bool> InsertCsvDataIntoTable(string csvFilePath, int batchid, SqlConnection connection, Boolean isUploading)//Insertion of CSV DATA into tables
        {
            int rowsInserted = 0;
            int totalRows = 0;
            string filename = Path.GetFileNameWithoutExtension(csvFilePath);
            string fileNameWithExtension = Path.GetFileName(csvFilePath);
            string[] filenameParts = filename.Split(new[] { " (" }, StringSplitOptions.RemoveEmptyEntries);
            string baseFilename = filenameParts[0];
            try
            {
                dbHelper.Log($"{Constants.UploadForBaseFileNameHasStarted.Replace("{baseFilename}", baseFilename)}", Constants.HccCode, baseFilename + Constants.CsvExtention, Constants.Uploadhcc);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return Tuple.Create(0, false);
                }
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

                        string[] data = line.Replace("\"", "").Replace("/","-").Split(',');

                        if (baseFilename == Constants.AriesClients)
                        {
                            if (chckPHI.Checked == true)
                            {
                                dbHelper.InsertClientDataPhi(connection, data, batchid, fileNameWithExtension);//insertion of aries clients
                                if (dbHelper.ErrorOccurred)
                                {
                                    MessageBox.Show(Constants.ErrorOccurred);
                                    break;
                                }
                            }
                            else
                            {
                                dbHelper.InsertClientData(connection, data, batchid, fileNameWithExtension);//insertion of aries clients
                                if (dbHelper.ErrorOccurred)
                                {
                                    MessageBox.Show(Constants.ErrorOccurred);
                                    break;
                                }
                            }
                            rowsInserted++;
                        }
                        if (baseFilename == Constants.AriesDeceased)//insertion of AriesDeceased
                        {
                            dbHelper.InsertdeceasedData(connection, data, batchid);//insertion of deceased clients
                            if (dbHelper.ErrorOccurred)
                            {
                                MessageBox.Show(Constants.ErrorOccurred);
                                break  ;
                            }
                            rowsInserted++;
                        }
                        if (baseFilename == Constants.AriesConsent)//insertion of AriesConsent
                        {
                            dbHelper.InsertConsentData(connection, data, batchid);//insertion of InsertConsentData
                            if (dbHelper.ErrorOccurred)
                            {
                                MessageBox.Show(Constants.ErrorOccurred);
                                break;
                            }
                            rowsInserted++;
                        }
                        if (baseFilename == Constants.AriesEligibility)//insertion of AriesEligibility
                        {
                            dbHelper.InsertDlEligibility(connection, data, batchid);
                            if (dbHelper.ErrorOccurred)
                            {
                                MessageBox.Show(Constants.ErrorOccurred);
                                break;
                            }
                            rowsInserted++;
                        }
                        if (baseFilename == Constants.AriesServices)//insertion of AriesServices
                        {
                            if (chckPHI.Checked == true)
                            {
                                int row = rowsInserted;
                                dbHelper.InsertDlServicesPhi(connection, data, batchid, fileNameWithExtension, row);
                                if (dbHelper.ErrorOccurred)
                                {
                                    MessageBox.Show(Constants.ErrorOccurred);
                                    break;
                                }
                                rowsInserted++;//insertion of aries clients
                            }
                            else
                            {
                                int row = rowsInserted;
                                dbHelper.InsertDlServices(connection, data, batchid, fileNameWithExtension, row);
                                if (dbHelper.ErrorOccurred)
                                {
                                    MessageBox.Show(Constants.ErrorOccurred);
                                    break;
                                }
                                rowsInserted++;//insertion of AriesServices
                            }
                        }
                        if (baseFilename == Constants.AriesFinancial)//insertion of AriesFinancial
                        {
                            dbHelper.InsertDlFinancials(connection, data, batchid);//insertion of AriesFinancial

                            if (dbHelper.ErrorOccurred)
                            {
                                MessageBox.Show(Constants.ErrorOccurred);
                                break;
                            }
                            rowsInserted++;
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
                    dbHelper.Log($"{Constants.UploadForBaseFileNameHasCompleted.Replace("{baseFilename}", baseFilename)}", Constants.OchinCode, baseFilename + Constants.CsvExtention, Constants.Uploadhcc);
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
                var st = new StackTrace(ex, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                //int lineNumber = frame != null ? frame.GetFileLineNumber() : 0;
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                dbHelper.LogError(ex.Message, ex.StackTrace, nameof(InsertCsvDataIntoTable), filename, lineNumber, Constants.HccCode);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                }
                return Tuple.Create(0, false);
            }
        }
        private void UpdateProgress(int currentRowIndex, int totalRowsInCurrentFile)//progress of total rows insertion of csv files 
        {
            try
            {
                if (currentRowIndex < totalRowsInCurrentFile)
                {
                    currentRowIndex++;
                    double percentage = ((double)currentRowIndex / totalRowsInCurrentFile) * 100;
                    int progress = (int)Math.Round(percentage); // Round the percentage to the nearest integer
                    txtProgressLines.Text = $@"{currentRowIndex}/{totalRowsInCurrentFile} ({progress}%)";
                    progressBarLines.Value = progress;
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateFileProgress(int currentFilesIndex, int totalFiles, Boolean isUploading)//progress of total files insertion 
        {
            try
            {
                // Update the progress bar value directly with the current file index
                progressBarfile.Value = isUploading ? currentFilesIndex : 0;

                int progressPercentage = (int)((double)currentFilesIndex / totalFiles * 100);
                // Update the progress text with current file progress
                txtProgressfile.Text = $@"{currentFilesIndex}/{totalFiles} ({progressPercentage}%)";

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
                        DialogResult result = MessageBox.Show(Constants.Areyousureyouwanttoabort, Constants.UploadHccCsv, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            int batchId = Convert.ToInt32(txtBatchid.Text);

                            string fileName = txtFileName.Text;

                            // Show confirmation message
                            MessageBox.Show(Constants.AbortedSuccessfully, Constants.UploadHccCsv);
                            string path = txtpath.Text;
                            UpdateBatch(batchId, fileName, path);
                            Close();
                            dbHelper.DeleteBatchochin(batchId.ToString());
                            if (dbHelper.ErrorOccurred)
                            {
                                MessageBox.Show(Constants.ErrorOccurred);
                                return;
                            }
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
        private void UpdateBatch(int batchId, string fileName, string path)//getting information of the entired data
        {
            try
            {
                DateTime currentTime = DateTime.Now;
                int totalRows = 0; // Assuming no rows were successfully processed
                int successfulRows = 0;
                string description = Constants.Abortedfile;
                dbHelper.InsertBatch(batchId, fileName, path, Constants.HccCode, description, currentTime, totalRows, successfulRows, 12);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"{Constants.ErrorLoadingData} {ex.Message}");
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
                        bool allFilesAreXml = files.All(file => Path.GetExtension(file).Equals(Constants.CsvExtention, StringComparison.OrdinalIgnoreCase));

                        if (!allFilesAreXml)
                        {
                            MessageBox.Show(Constants.ThefoldercontainsnonCsVfilesUploadisallowedonlyforCsVfiles, Constants.Hcc, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    mainForm.ShowOchinToHccScreen();//to show upload OCHIN CSV Screen
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
  
