using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using RWDE_UPLOADS_FILES;
using Rwde;
using RWDE;
using System.Xml;
using System.Data.SqlTypes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace RWDE_UPLOADS_FILES
{
    public partial class btnCT : Form
    {
        private readonly string connectionString;
        private readonly DBHelper dbHelper;
        private readonly DataGridView dataGridView;
        public Panel PanelToReplace
        {
            get
            {
                return pnlCsvXml;//
            }
        }
        public btnCT()
        {
            try
            {
                InitializeComponent();
                LoadDefaultPath();
                dbHelper = new DBHelper();
                connectionString = dbHelper.GetConnectionString();
               // this.Text = String.Empty;
                this.ControlBox = false;
                //this.DoubleBuffered = true;
                //this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
                this.WindowState = FormWindowState.Maximized;
                DateTime currenttime = DateTime.Now;
                string date = currenttime.ToString("MM/dd/yyyy");
                string time = currenttime.ToString("HH:mm:ss");
                txtDesc.Text = $"ClientTrack Upload on {date} at {time}";
                txtProgressbar.Text = "0%";
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

        public string fileName;
        public string path;
        public int batchId;
        public int totalRows;

        private async void btnUploadXML_Click(object sender, EventArgs e)
        {
            if (txtPath.Text == "" || txtPath == null)
            {
                MessageBox.Show(Constants.ThefolderisemptyPleaseuploadfiles,Constants.xmlfileuploads, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            string[] files = Directory.GetFiles(txtPath.Text);

            bool allFilesAreXml = files.All(file => Path.GetExtension(file).Equals(".xml", StringComparison.OrdinalIgnoreCase));

            if (files == null || files.Length == 0)
            {
                MessageBox.Show(Constants.ThefolderisemptyPleaseuploadfiles, Constants.xmlfileuploads, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!allFilesAreXml)
            {
                MessageBox.Show(Constants.ThefoldercontainsnonXMLfilesorfolderisemptyUploadisallowedonlyforXMLfiles, Constants.xmlfileuploads, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnClose.Text = Constants.abort;
     
            if (string.IsNullOrWhiteSpace(txtPath.Text))
            {
                MessageBox.Show("Select a file before uploading.");
                return; // Exit the method if the path is empty
            }

         
            // Check if all files have the .xml extension
           
            // Continue with the upload process if all files are XML



            btnUploadXML.Enabled = false;

            string folderPath = txtPath.Text.Trim();
            var values = cbMask.Checked ? true : false;
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

                        int batchId = dbHelper.GetNextBatchID();
                        path = xmlFilePath;
                        fileName = Path.GetFileName(xmlFilePath);
                        txtFName.Text = fileName;
                        txtBatchid.Text = batchId.ToString();
                        string date = startTime.ToString("MM/dd/yyyy");
                        string time = startTime.ToString("HH:mm:ss");
                        txtDesc.Text = $"ClientTrack Upload on {date} At {time}";
                        string Time = startTime.ToString("MM/dd/yyyy HH:mm:ss");
                        txtUploadStarted.Text = Time;

                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();

                            totalRows = await GetTotalRowsInXml(xmlDoc);
                            DateTime uploadStartedAt = DateTime.Now;
                            await InsertXmlDataIntoTable(xmlDoc, batchId, xmlFilePath, totalRows, conn, values);

                            int successfulRows = totalRows;
                            string description = $"Batch {batchId} - {fileName}";
                            dbHelper.InsertBatch(batchId, fileName, xmlFilePath, Constants.ClientTrack, description, uploadStartedAt, totalRows, successfulRows, Constants.Status);

                            processedXmlFiles++;
                            UpdateFileProgressTotal(processedXmlFiles, totalXmlFiles);

                        }

                        DateTime endTime = DateTime.Now;
                        TimeSpan totalTime = endTime - startTime;

                        double totalSeconds = totalTime.TotalSeconds;
                        string ETime = endTime.ToString("MM/dd/yyyy HH:mm:ss");
                        txtUploadEnded.Text = ETime;
                        txtTotaltime.Text = $"{totalSeconds:F2} Seconds";
                        btnClose.Text = Constants.close;
                    }

                    btnUploadXML.Enabled = true;
                    btnClose.Text = Constants.close;
                }
                else
                {
                    MessageBox.Show($"This {fileName} File is already uploaded.click On Browse to Choose another file to upload.", "XML File Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            string baseFilename = Path.GetFileNameWithoutExtension(xmlFilePath);
            try
            {

                dbHelper.Log($"Upload for {baseFilename} has started", Constants.ClientTrack, baseFilename, Constants.uploadct);// Log the start of the upload process for the given base filename

                int insertedClients = dbHelper.InsertClients(xmlDoc, batchId, conn, fileName, value);
                totalInsertedRows += insertedClients;

                int insertedEligibilityDocs = dbHelper.InsertEligibilityDocuments(xmlDoc, batchId, conn, fileName);
                totalInsertedRows += insertedEligibilityDocs;

                int insertedServiceLineItems = dbHelper.InsertServiceLineItems(xmlDoc, batchId, conn, fileName);
                totalInsertedRows += insertedServiceLineItems;

                //trans.Commit();
                await UpdateProgressBar(totalRows);/// Update the progress bar to reflect the total number of rows being processed.
                dbHelper.Log($"Upload for {baseFilename} has completed Successfully", Constants.ClientTrack, baseFilename, Constants.uploadct);// Log the end of the upload process for the given base filename
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrames().FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame != null ? frame.GetFileLineNumber() : 0;
                dbHelper.LogError(ex.Message, GetCurrentFilePath(), ex.StackTrace, nameof(InsertXmlDataIntoTable), fileName, lineNumber);
                throw;
            }

        }
        private async Task Updatetotal(int totalRows)// This method updates the total count of rows being processed asynchronously.
        {
            try
            {
                int totalInsertedRows = 0;

                while (totalInsertedRows <= totalRows)
                {
                    int progressPercentage = (int)((double)totalInsertedRows / totalRows * 100);
                    string progressInfo = $"{totalInsertedRows}/{totalRows} ({progressPercentage}%)";
                    txtProgressbar.Text = progressInfo;
                    progressBarfile.Value = totalInsertedRows;

                    totalInsertedRows++;
                    await Task.Delay(1);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid");
            }
        }

        private async Task UpdateProgressBar(int totalCount)
        {
            progressBar.Minimum = 0;
            progressBar.Maximum = totalCount;
            txtProgressbar.Text = "0%";
            progressBar.Value = 0; // Immediate feedback
            int currentCount = 0;
            int updateInterval = Math.Max(1, totalCount / 100); // Update every 1% or more frequently

            // Start showing progress immediately
            while (currentCount <= totalCount)
            {
                if (currentCount % updateInterval == 0 || currentCount == totalCount)
                {
                    int progressPercentage = (int)((double)currentCount / totalCount * 100);
                    string progressInfo = $"{currentCount}/{totalCount} ({progressPercentage}%)";
                    txtProgressbar.Text = progressInfo;
                    progressBar.Value = currentCount;
                }

                currentCount++;

                // Minimal delay to allow the UI to update, no need for a fraction like 0.25
                await Task.Delay(1);
            }
        }


        //private async Task UpdateProgressBar(int totalCount)//This method updates the progress bar to reflect the progress of the operation asynchronously.
        //{
        //    progressBar.Minimum = 0;
        //    progressBar.Maximum = totalCount;
        //    txtProgressbar.Text = "0%";
        //    int currentCount = 0;
        //    while (currentCount <= totalCount)
        //    {
        //        int progressPercentage = (int)((double)currentCount / totalCount * 100);
        //        string progressInfo = $"{currentCount}/{totalCount} ({progressPercentage}%)";
        //        txtProgressbar.Text = progressInfo;
        //        progressBar.Value = currentCount;

        //        await Task.Delay(1);
        //        currentCount++;
        //    }
        //}
        private void UpdateFileProgressTotal(int currentFileIndex, int totalFiles)// This method updates the file progress to reflect the current file index out of the total number of files.
        {
            if (progressBarfile.InvokeRequired)
            {
                progressBarfile.Invoke((MethodInvoker)delegate
                {
                    int FileProgressPercentage = (int)((double)currentFileIndex / totalFiles * 100);
                    progressBarfile.Value = currentFileIndex;
                    txtProgressfile.Text = $"{currentFileIndex}/{totalFiles} ({FileProgressPercentage}%)";
                });
            }
            else
            {
                int FileProgressPercentage = (int)((double)currentFileIndex / totalFiles * 100);
                progressBarfile.Value = currentFileIndex;
                txtProgressfile.Text = $"{currentFileIndex}/{totalFiles} ({FileProgressPercentage}%)";
            }
        }
        private int CountNodes(XmlDocument xmlDoc, string xpath)// This method counts the number of nodes in the XmlDocument that match the specified XPath expression.
        {
            XmlNodeList nodes = xmlDoc.SelectNodes(xpath);
            return nodes.Count;
        }
        private void btnGeneratexml_Click(object sender, EventArgs e)
        {
            pnlCsvXml.Visible = false;
        }
        // Define a class-level variable to store the default path.
        private string defaultPath = null;

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
                            bool allFilesAreXml = files.All(file => Path.GetExtension(file).Equals(".xml", StringComparison.OrdinalIgnoreCase));

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
            
            

        // Method to save the default path to application settings or configuration.
        private void SaveDefaultPathToSettings(string path)
        {
            // Example of saving to application settings (adjust according to your actual settings mechanism).
            //Properties.Settings.Default.DefaultPath = path;
            Properties.Settings.Default.Save();
        }

        // Method to load the default path when the form is initialized.
        private void LoadDefaultPath()
        {
            // Load the default path from settings if it exists.
            //defaultPath = Properties.Settings.Default.DefaultPath;

            // Optionally, set the TextBox to the default path.
            txtPath.Text = defaultPath;
        }

        // Call LoadDefaultPath in your form's constructor or Load event handler.
         private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                int batchId = dbHelper.GetNextBatchID();
                if (btnClose.Text == "Abort")
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to abort?","XML File Upload", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    // Check if the user clicked "Yes"
                    UpdateBatch(batchId, fileName, path);
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
                dbHelper.InsertBatch(batchId+1, fileName, path, Constants.ClientTrack, null, currentTime, totalRows, successfulRows, Constants.fileaborted);
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
        private void RefreshFormControls()// This method refreshes or updates all form controls to reflect the latest data or state changes.
        {
            txtBatchid.Clear();
            txtUploadStarted.Clear();
            txtUploadEnded.Clear();
            txtTotaltime.Clear();
            txtProgressbar.Text = "0%";
            txtProgressfile.Text = "0/0 (0%)";
            progressBarfile.Value = 0;
            progressBar.Value = 0;
            txtFName.Text = string.Empty;
            btnUploadXML.Visible = true;
            DateTime currenttime = DateTime.Now;
            string date = currenttime.ToString("MM/dd/yyyy");
            string time = currenttime.ToString("HH:mm:ss");

            txtDesc.Text = $"ClientTrack Upload on {date} At {time}";
        }
        private void CustomCheckBox_Paint(object sender, PaintEventArgs e)// Handles the custom painting of the CheckBox control to allow for custom visual appearance.
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox != null)
            {
                // Define the size of the checkbox part
                int checkBoxSize = 40;
                // Calculate positions
                int checkBoxTop = (checkBox.Height - checkBoxSize) / 2;
                Rectangle rect = new Rectangle(0, checkBoxTop, checkBoxSize, checkBoxSize);

                // Draw the checkbox manually
                ControlPaint.DrawCheckBox(e.Graphics, rect, checkBox.Checked ? ButtonState.Checked : ButtonState.Normal);
            }
        }
        // Returns the file path of the source file where the method is called, using the CallerFilePath attribute.
        private string GetCurrentFilePath([CallerFilePath] string filePath = "") => filePath;
        // Returns the line number of the source code where the method is called, using the CallerLineNumber attribute.
        private int GetCurrentLineNumber([CallerLineNumber] int lineNumber = 0) => lineNumber;

        // Returns the name of the calling member (method or property) using the CallerMemberName attribute.
        private string GetCurrentMemberName([CallerMemberName] string memberName = "") => memberName;

        private void cbMask_CheckedChanged_2(object sender, EventArgs e)
        {

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
    }
}