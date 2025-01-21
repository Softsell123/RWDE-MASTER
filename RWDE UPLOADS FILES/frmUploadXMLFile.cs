using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml;
using System.Diagnostics;


namespace RWDE
{
    public partial class FrmUploadXmlFile : Form
    {
        private readonly DbHelper dbHelper;
       
        // Define a class-level variable to store the default path.
        private readonly string defaultPath = null;

        // Returns the file path of the source file where the method is called, using the CallerFilePath attribute.
       
        public Panel PanelToReplace
        {
            get
            {
                return pnlCsvXml;//
            }
        }
        public FrmUploadXmlFile()
        {
            try
            {
                InitializeComponent();
                LoadDefaultPath();
                dbHelper = new DbHelper();

                this.ControlBox = false;
                this.WindowState = FormWindowState.Maximized;
                DateTime currenttime = DateTime.Now;
                string date = currenttime.ToString(Constants.MMddyyyybkslash);
                string time = currenttime.ToString(Constants.HHmmss);
                txtDesc.Text = $@"{Constants.ClientTrackUploadOnAt.Replace("{date}", date).Replace("{time}", time)}";
                txtProgressLines.Text = Constants.ZeroPercent;
                txtProgressfile.Text = Constants.InitialProgress;
                string pathFile = Constants.LastFolderPathTxt;
                RegisterEvents(this); //Assigning events to all Controls

                // Check if the file exists
                if (File.Exists(pathFile))
                {
                    string lastFolderPath = File.ReadAllText(pathFile).Trim();  // Trim to remove extra spaces or newlines

                    // Check if the file content is not empty and the directory exists
                    if (!string.IsNullOrEmpty(lastFolderPath) && Directory.Exists(lastFolderPath))
                    {
                        txtPath.Text = lastFolderPath;
                    }
                    else
                    {
                        txtPath.Clear();  // Ensure the path is cleared if the file is empty or invalid
                    }
                }
                else
                {
                    txtPath.Clear();  // Clear the text box if the file doesn't exist
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"{Constants.AnErrorOccurred}{ex.Message}", Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbMask_MouseHover(object sender, EventArgs e)//Changing Cursor as Hand on hover
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
        private void cbMask_MouseLeave(object sender, EventArgs e)//Changing back default Cursor on Leave
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
                    if (control is System.Windows.Forms.Button || control is CheckBox || control is DateTimePicker)
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
        private async void btnUploadXML_Click(object sender, EventArgs e)//to read and Store the Xml File
        {
            try
            {
                if (txtPath.Text == "" || txtPath == null)
                {
                    MessageBox.Show(Constants.ThefolderisemptyPleaseuploadfiles, Constants.Xmlfileuploads, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }
                string[] files = Directory.GetFiles(txtPath.Text);

                bool allFilesAreXml = files.All(file => System.IO.Path.GetExtension(file).Equals(Constants.XmlExtention, StringComparison.OrdinalIgnoreCase));

                if (files == null || files.Length == 0)
                {
                    MessageBox.Show(Constants.ThefolderisemptyPleaseuploadfiles, Constants.Xmlfileuploads, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (files.Length > 1)
                {
                    MessageBox.Show(Constants.Thefolderhasmorethanonefileorduplicatefiles, Constants.Xmlfileuploads, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // Check if all files have the .xml extension
                if (!allFilesAreXml)
                {
                    MessageBox.Show(Constants.ThefoldercontainsnonXmLfilesorfolderisemptyUploadisallowedonlyforXmLfiles, Constants.Xmlfileuploads, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Continue with the upload process if all files are XML
                btnClose.Text = Constants.Abort;

                if (string.IsNullOrWhiteSpace(txtPath.Text))
                {
                    MessageBox.Show(Constants.SelectAFolderBeforeUploading);
                    return; // Exit the method if the path is empty
                }

                btnUploadXML.Enabled = false;

                string folderPath = txtPath.Text.Trim();
                var values = chkPHI.Checked ? true : false;
                if (string.IsNullOrEmpty(folderPath))
                {
                    MessageBox.Show(Constants.PleaseSelectAFolderToUploadXmlFiles, Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnUploadXML.Enabled = true;
                    return;
                }
                try
                {
                    if (string.IsNullOrEmpty(txtBatchid.Text) && string.IsNullOrEmpty(txtUploadStarted.Text))
                    {
                        btnClose.Text = Constants.Abort;
                        string[] xmlFiles = Directory.GetFiles(folderPath, Constants.AllXmlExtention);
                        int totalXmlFiles = xmlFiles.Length;
                        int processedXmlFiles = 0;
                        DateTime startTime = DateTime.Now;

                        progressBarfile.Minimum = 0;
                        progressBarfile.Maximum = totalXmlFiles;
                        progressBarfile.Value = 0;

                        foreach (string xmlFilePath in xmlFiles)
                        {
                            // Update the progress display to show the current processed XML file index out of the total number of XML files.
                            UpdateFileProgressTotal(processedXmlFiles, totalXmlFiles);

                            XmlDocument xmlDoc = new XmlDocument();
                            xmlDoc.Load(xmlFilePath);

                            int batchId = dbHelper.GetNextBatchId();//to get next BatchId for Insertion
                            if (dbHelper.ErrorOccurred)
                            {
                                MessageBox.Show(Constants.ErrorOccurred);
                                return;
                            }
                            string FileName = Path.GetFileName(xmlFilePath);
                            txtFileName.Text = FileName;
                            txtBatchid.Text = batchId.ToString();
                            string date = startTime.ToString(Constants.MMddyyyybkslash);
                            string time = startTime.ToString(Constants.HHmmss);
                            txtDesc.Text = Constants.ClientTrackUploadOnAt.Replace("{date}", date).Replace("{time}", time);
                            string formattime = startTime.ToString(Constants.MMddyyyyHHmmssbkslash);
                            txtUploadStarted.Text = formattime;

                            using (SqlConnection conn = new SqlConnection(dbHelper.GetConnectionString()))

                            {
                                conn.Open();

                                // to get the total number of rows in the given XmlDocument.
                                int TotalRows = await GetTotalRowsInXml(xmlDoc);
                                DateTime uploadStartedAt = DateTime.Now;

                                // to insert data from an XmlDocument into a database table asynchronously.
                                await InsertXmlDataIntoTable(xmlDoc, batchId, xmlFilePath, TotalRows, conn, values, FileName);

                                int successfulRows = TotalRows;
                                string description = $"{Constants.Batch} {batchId} - {FileName}";

                                //update batch status in database
                                dbHelper.InsertBatch(batchId, FileName, xmlFilePath, Constants.ClientTrackCode, description, uploadStartedAt, TotalRows, successfulRows, Constants.StatusCode);
                                if (dbHelper.ErrorOccurred)
                                {
                                    MessageBox.Show(Constants.ErrorOccurred);
                                    return;
                                }
                                processedXmlFiles++;
                                UpdateFileProgressTotal(processedXmlFiles, totalXmlFiles);//to update the file progress
                            }
                            DateTime endTime = DateTime.Now;
                            TimeSpan totalTime = endTime - startTime;
                            double totalSeconds = totalTime.TotalSeconds;
                            string eTime = endTime.ToString(Constants.MMddyyyyHHmmssbkslash);
                            txtUploadEnded.Text = eTime;
                            txtTotaltime.Text = $@"{totalSeconds:F2} {Constants.Seconds}";
                            btnClose.Text = Constants.Close;
                        }
                        btnUploadXML.Enabled = true;
                        btnClose.Text = Constants.Close;
                    }
                    else
                    {
                        MessageBox.Show($@"{Constants.TheFileIsAlreadyUploaded}", Constants.XmlFileUpload, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnUploadXML.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($@"{Constants.AnErrorOccurred}{ex.Message}", Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnUploadXML.Enabled = true;
                    btnClose.Text = Constants.Close;
                }
                finally
                {
                    btnUploadXML.Enabled = true;
                    btnClose.Text = Constants.Close;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task<int> GetTotalRowsInXml(XmlDocument xmlDoc)// This method asynchronously returns the total number of rows in the given XmlDocument.
        {
            try
            {
                // to count the number of nodes in the XmlDocument that match the specified XPath expression.
                int totalClients = CountNodes(xmlDoc, Constants.BkslhClient);
                int totalEligibilityDocs = CountNodes(xmlDoc, Constants.BkslhEligibilityDocument);
                int totalServiceLineItems = CountNodes(xmlDoc, Constants.BkslhServiceLineItem);

                return totalClients + totalEligibilityDocs + totalServiceLineItems;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }
        private async Task InsertXmlDataIntoTable(XmlDocument xmlDoc, int batchId, string xmlFilePath, int totalRows, SqlConnection conn, bool value,string fileName)// This method inserts data from an XmlDocument into a database table asynchronously.
        {
            int totalInsertedRows = 0;
            string baseFilename = Path.GetFileNameWithoutExtension(xmlFilePath);
            try
            {
                //insertion into log table
                dbHelper.Log($"{Constants.UploadForBaseFileNameHasStarted.Replace("{baseFilename}", baseFilename)}", Constants.ClientTrackCode, baseFilename, Constants.Uploadct);// Log the start of the upload process for the given base filename
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
                //insert clients into database from Xml
                int insertedClients = dbHelper.InsertClients(xmlDoc, batchId, conn, fileName, value);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
                totalInsertedRows += insertedClients;

                //insertion of eligibility document from xml file
                int insertedEligibilityDocs = dbHelper.InsertEligibilityDocuments(xmlDoc, batchId, conn, fileName);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
                totalInsertedRows += insertedEligibilityDocs;

                //insertion of service table from xml file
                int insertedServiceLineItems = dbHelper.InsertServiceLineItems(xmlDoc, batchId, conn, fileName);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
                totalInsertedRows += insertedServiceLineItems;

                await UpdateProgressBar(totalRows);// Update the progress bar to reflect the total number of rows being processed.
                dbHelper.Log($"{Constants.UploadForBaseFileNameHasCompleted.Replace("{baseFilename}", baseFilename)}", Constants.ClientTrackCode, baseFilename, Constants.Uploadct);// Log the end of the upload process for the given base filename
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                dbHelper.LogError(ex.Message, ex.StackTrace, nameof(InsertXmlDataIntoTable), fileName, lineNumber);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
                throw;
            }
        }
        private async Task UpdateProgressBar(int totalCount)
        {
            try
            {
                progressBarLines.Minimum = 0;
                progressBarLines.Maximum = totalCount;
                txtProgressLines.Text = Constants.ZeroPercent;
                progressBarLines.Value = 0; // Immediate feedback
                int currentCount = 0;
                int updateInterval = Math.Max(1, totalCount / 100); // Update every 1% or more frequently

                // Start showing progress immediately
                while (currentCount <= totalCount)
                {
                    if (currentCount % updateInterval == 0 || currentCount == totalCount)
                    {
                        int progressPercentage = (int)((double)currentCount / totalCount * 100);
                        string progressInfo = $"{currentCount}/{totalCount} ({progressPercentage}%)";
                        txtProgressLines.Text = progressInfo;
                        progressBarLines.Value = currentCount;
                    }

                    currentCount++;
                    // Minimal delay to allow the UI to update, no need for a fraction like 0.25
                    await Task.Delay(1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UpdateFileProgressTotal(int currentFilesIndex, int totalFiles)// This method updates the file progress to reflect the current file index out of the total number of files.
        {
            try
            {
                if (progressBarfile.InvokeRequired)
                {
                    progressBarfile.Invoke((MethodInvoker)delegate
                    {
                        int fileProgressPercentage = (int)((double)currentFilesIndex / totalFiles * 100);
                        progressBarfile.Value = currentFilesIndex;
                        txtProgressfile.Text = $@"{currentFilesIndex}/{totalFiles} ({fileProgressPercentage}%)";
                    });
                }
                else
                {
                    int fileProgressPercentage = (int)((double)currentFilesIndex / totalFiles * 100);
                    progressBarfile.Value = currentFilesIndex;
                    txtProgressfile.Text = $@"{currentFilesIndex}/{totalFiles} ({fileProgressPercentage}%)";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private int CountNodes(XmlDocument xmlDoc, string xpath)// This method counts the number of nodes in the XmlDocument that match the specified XPath expression.
        {
            try
            {
                XmlNodeList nodes = xmlDoc.SelectNodes(xpath);
                return nodes.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }
        // Event handler for the Browse button click event.
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

                        // Check if the folder contains only XML files
                        string[] files = Directory.GetFiles(selectedFolderPath);

                        // Check if all files have the .xml extension
                        bool allFilesAreXml = files.All(file => System.IO.Path.GetExtension(file).Equals(Constants.XmlExtention, StringComparison.OrdinalIgnoreCase));

                        if (!allFilesAreXml)
                        {
                            MessageBox.Show(Constants.TheFolderContainsNonXmlFiles, Constants.InvalidFileType, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        // Save the path to the file
                        File.WriteAllText(Constants.LastFolderPathTxt, selectedFolderPath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Method to load the default path when the form is initialized.
        private void LoadDefaultPath()
        {
            try
            {
                // Optionally, set the TextBox to the default path.
                txtPath.Text = defaultPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnClose.Text == Constants.Close)
                {
                    Close();
                    Application.Restart();
                    return;
                }
                int batchId = Convert.ToInt32(txtBatchid.Text);
                if (btnClose.Text == Constants.Abort)
                {
                    DialogResult result = MessageBox.Show(Constants.Areyousureyouwanttoabort, Constants.XmlFileUpload, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes) // Check if the user clicked "Yes"
                    {
                        string folderPath = txtPath.Text.Trim();
                        string[] xmlFiles = Directory.GetFiles(folderPath, Constants.AllXmlExtention);
                        string FileName = txtFileName.Text;

                            foreach (string xmlFilePath in xmlFiles)
                        {
                            XmlDocument xmlDoc = new XmlDocument();
                            xmlDoc.Load(xmlFilePath);
                            int TotalRows =await GetTotalRowsInXml(xmlDoc);
                            // to update the batch record in the database
                            UpdateBatch(batchId, FileName, xmlFilePath, TotalRows);
                        }
                        MessageBox.Show(Constants.AbortedSuccessfully, Constants.XmlFileUpload);
                    }
                    else
                    {
                        return;
                    }
                }
                WindowState = FormWindowState.Maximized;
                Application.Restart();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UpdateBatch(int batchId, string fileName, string path,int TotalRows)// This method updates the batch record in the database with the specified batch ID, file name, and path.
        {
            try
            {
                DateTime currentTime = DateTime.Now;
                int successfulRows = 0;
                string description = Constants.Abortedfile;
                // Insert batch details into the database, including batch ID, file name, path, timestamps, row counts, and status.
                dbHelper.InsertBatch(batchId, fileName, path, Constants.ClientTrackCode, description, currentTime, TotalRows, successfulRows, Constants.Fileaborted);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
                //to clear the tables associated with the specified batch ID.
                dbHelper.ClearTables(batchId);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($@"{Constants.Errorupdatingbatch}{ex.Message}");
            }
        }
    }
}