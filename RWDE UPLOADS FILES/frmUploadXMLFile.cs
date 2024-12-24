using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace RWDE
{
    public partial class FrmUploadXmlFile : Form
    {
        private readonly string connectionString;
        private readonly DbHelper dbHelper;
        private readonly DataGridView dataGridView;
        public string FileName;
        public string Path;
        public int TotalRows;

        // Define a class-level variable to store the default path.
        private string defaultPath = null;

        // Returns the file path of the source file where the method is called, using the CallerFilePath attribute.
        private string GetCurrentFilePath([CallerFilePath] string filePath = "") => filePath;
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
                connectionString = dbHelper.GetConnectionString();
                this.ControlBox = false;
                this.WindowState = FormWindowState.Maximized;
                DateTime currenttime = DateTime.Now;
                string date = currenttime.ToString("MM/dd/yyyy");
                string time = currenttime.ToString("HH:mm:ss");
                txtDesc.Text = $"ClientTrack Upload on {date} at {time}";
                txtProgressLines.Text = "0%";
                txtProgressfile.Text = "0/0 (0%)";
                string pathFile = "LastFolderPath.txt";
                RegisterEvents(this);

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
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbMask_MouseHover(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }
        private void cbMask_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
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
        private async void btnUploadXML_Click(object sender, EventArgs e)
        {
            if (txtPath.Text == "" || txtPath == null)
            {
                MessageBox.Show(Constants.ThefolderisemptyPleaseuploadfiles,Constants.Xmlfileuploads, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            string[] files = Directory.GetFiles(txtPath.Text);

            bool allFilesAreXml = files.All(file => System.IO.Path.GetExtension(file).Equals(".xml", StringComparison.OrdinalIgnoreCase));

            if (files == null || files.Length == 0)
            {
                MessageBox.Show(Constants.ThefolderisemptyPleaseuploadfiles, Constants.Xmlfileuploads, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (files.Length >1)
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
                MessageBox.Show("Select a file before uploading.");
                return; // Exit the method if the path is empty
            }

            btnUploadXML.Enabled = false;

            string folderPath = txtPath.Text.Trim();
            var values = chkPHI.Checked ? true : false;
            if (string.IsNullOrEmpty(folderPath))
            {
                MessageBox.Show("Please select a folder to upload XML files.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnUploadXML.Enabled = true;
                return;
            }
            try
            {
                if (string.IsNullOrEmpty(txtBatchid.Text) && string.IsNullOrEmpty(txtUploadStarted.Text))
                {
                    btnClose.Text = "Abort";
                    string[] xmlFiles = Directory.GetFiles(folderPath, "*.xml");
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

                        int batchId = dbHelper.GetNextBatchId();
                        Path = xmlFilePath;
                        FileName = System.IO.Path.GetFileName(xmlFilePath);
                        txtFileName.Text = FileName;
                        txtBatchid.Text = batchId.ToString();
                        string date = startTime.ToString("MM/dd/yyyy");
                        string time = startTime.ToString("HH:mm:ss");
                        txtDesc.Text = $"ClientTrack Upload on {date} At {time}";
                        string formattime = startTime.ToString("MM/dd/yyyy HH:mm:ss");
                        txtUploadStarted.Text = formattime;

                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();

                            TotalRows = await GetTotalRowsInXml(xmlDoc);
                            DateTime uploadStartedAt = DateTime.Now;
                            await InsertXmlDataIntoTable(xmlDoc, batchId, xmlFilePath, TotalRows, conn, values);

                            int successfulRows = TotalRows;
                            string description = $"Batch {batchId} - {FileName}";
                            dbHelper.InsertBatch(batchId, FileName, xmlFilePath, Constants.ClientTrack, description, uploadStartedAt, TotalRows, successfulRows, Constants.StatusCode);

                            processedXmlFiles++;
                            UpdateFileProgressTotal(processedXmlFiles, totalXmlFiles);
                        }
                        DateTime endTime = DateTime.Now;
                        TimeSpan totalTime = endTime - startTime;
                        double totalSeconds = totalTime.TotalSeconds;
                        string eTime = endTime.ToString("MM/dd/yyyy HH:mm:ss");
                        txtUploadEnded.Text = eTime;
                        txtTotaltime.Text = $"{totalSeconds:F2} Seconds";
                        btnClose.Text = Constants.Close;
                    }
                    btnUploadXML.Enabled = true;
                    btnClose.Text = Constants.Close;
                }
                else
                {
                    MessageBox.Show($"This {FileName} File is already uploaded.click On Browse to Choose another file to upload.", "XML File Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnUploadXML.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnUploadXML.Enabled = true;
                btnClose.Text = "Close";
            }
        }

        private async Task<int> GetTotalRowsInXml(XmlDocument xmlDoc)// This method asynchronously returns the total number of rows in the given XmlDocument.
        {
            int totalClients = CountNodes(xmlDoc, "//Client");
            int totalEligibilityDocs = CountNodes(xmlDoc, "//EligibilityDocument");
            int totalServiceLineItems = CountNodes(xmlDoc, "//ServiceLineItem");

            return totalClients + totalEligibilityDocs + totalServiceLineItems;
        }
        private async Task InsertXmlDataIntoTable(XmlDocument xmlDoc, int batchId, string xmlFilePath, int totalRows, SqlConnection conn, bool value)// This method inserts data from an XmlDocument into a database table asynchronously.
        {
            int totalInsertedRows = 0;
            string baseFilename = System.IO.Path.GetFileNameWithoutExtension(xmlFilePath);
            try
            {
                dbHelper.Log($"Upload for {baseFilename} has started", Constants.ClientTrack, baseFilename, Constants.Uploadct);// Log the start of the upload process for the given base filename

                int insertedClients = dbHelper.InsertClients(xmlDoc, batchId, conn, FileName, value);
                totalInsertedRows += insertedClients;

                int insertedEligibilityDocs = dbHelper.InsertEligibilityDocuments(xmlDoc, batchId, conn, FileName);
                totalInsertedRows += insertedEligibilityDocs;

                int insertedServiceLineItems = dbHelper.InsertServiceLineItems(xmlDoc, batchId, conn, FileName);
                totalInsertedRows += insertedServiceLineItems;

                await UpdateProgressBar(totalRows);/// Update the progress bar to reflect the total number of rows being processed.
                dbHelper.Log($"Upload for {baseFilename} has completed Successfully", Constants.ClientTrack, baseFilename, Constants.Uploadct);// Log the end of the upload process for the given base filename
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrames().FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame != null ? frame.GetFileLineNumber() : 0;
                dbHelper.LogError(ex.Message, GetCurrentFilePath(), ex.StackTrace, nameof(InsertXmlDataIntoTable), FileName, lineNumber);
                throw;
            }

        }
        private async Task UpdateProgressBar(int totalCount)
        {
            progressBarLines.Minimum = 0;
            progressBarLines.Maximum = totalCount;
            txtProgressLines.Text = "0%";
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
        private void UpdateFileProgressTotal(int currentFileIndex, int totalFiles)// This method updates the file progress to reflect the current file index out of the total number of files.
        {
            if (progressBarfile.InvokeRequired)
            {
                progressBarfile.Invoke((MethodInvoker)delegate
                {
                    int fileProgressPercentage = (int)((double)currentFileIndex / totalFiles * 100);
                    progressBarfile.Value = currentFileIndex;
                    txtProgressfile.Text = $"{currentFileIndex}/{totalFiles} ({fileProgressPercentage}%)";
                });
            }
            else
            {
                int fileProgressPercentage = (int)((double)currentFileIndex / totalFiles * 100);
                progressBarfile.Value = currentFileIndex;
                txtProgressfile.Text = $"{currentFileIndex}/{totalFiles} ({fileProgressPercentage}%)";
            }
        }
        private int CountNodes(XmlDocument xmlDoc, string xpath)// This method counts the number of nodes in the XmlDocument that match the specified XPath expression.
        {
            XmlNodeList nodes = xmlDoc.SelectNodes(xpath);
            return nodes.Count;
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
                            bool allFilesAreXml = files.All(file => System.IO.Path.GetExtension(file).Equals(".xml", StringComparison.OrdinalIgnoreCase));

                            if (!allFilesAreXml)
                            {
                                MessageBox.Show("The folder contains non-XML files or folder is empty. Upload is allowed only for XML files.", "Invalid File Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            // Save the path to the file
                            File.WriteAllText("LastFolderPath.txt", selectedFolderPath);
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
            // Optionally, set the TextBox to the default path.
            txtPath.Text = defaultPath;
        }

        // Call LoadDefaultPath in your form's constructor or Load event handler.
        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnClose.Text == Constants.Close)
                {
                    this.Close();
                    Application.Restart();
                    return;
                }
                int batchId = Convert.ToInt32(txtBatchid.Text);
                if (btnClose.Text == Constants.Abort)
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to abort?","XML File Upload", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    // Check if the user clicked "Yes"
                    UpdateBatch(batchId, FileName, Path);
                    DialogResult result2 = MessageBox.Show("Aborted Successfully","XML File Upload");
                }
                this.WindowState = FormWindowState.Maximized;
                Application.Restart();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UpdateBatch(int batchId, string fileName, string path)// This method updates the batch record in the database with the specified batch ID, file name, and path.
        {
            try
            {

                DateTime currentTime = DateTime.Now;
                int successfulRows = 0;
                // Insert batch details into the database, including batch ID, file name, path, timestamps, row counts, and status.
                dbHelper.InsertBatch(batchId+1, fileName, path, Constants.ClientTrack, null, currentTime, TotalRows, successfulRows, Constants.Fileaborted);
                ClearTables(batchId+1);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating batch: {ex.Message}");
            }
        }
        private void ClearTables(int batchId)// This method clears or resets tables associated with the specified batch ID.
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("abortdelete", connection))
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
                throw; // Re-throw if you want to handle it in the calling method
            }
        }
    }
}