using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Diagnostics;
using ComboBox = System.Windows.Forms.ComboBox;
using ScrollBar = System.Windows.Forms.ScrollBar;
using System.Text;
using System.Runtime.InteropServices;

namespace RWDE
{
    public partial class FrmGeneratorXml : Form
    {
        private readonly DbHelper dbHelper;
        //// Import Mouse Click Functions
        [DllImport("user32.dll", SetLastError = true)]
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, IntPtr dwExtraInfo);

        private const uint MOUSEEVENTFLEFTDOWN = 0x02;
        private const uint MOUSEEVENTFLEFTUP = 0x04;
        public FrmGeneratorXml()
        {
            InitializeComponent();
            dbHelper = new DbHelper();
            LoadBatchStatus();// load data
            PopulateDataGridView();// Handle the Grid View 
            ControlBox = false;
            WindowState = FormWindowState.Maximized;
            dataGridView.CellFormatting += dataGridView_CellFormatting;
            dataGridView.DataBindingComplete += DataGridView_DataBindingComplete;
            // Handle BatchType Values
            List<string> batchTypes = dbHelper.GetAllBatchTypesHcc();
            if (dbHelper.ErrorOccurred)
            {
                MessageBox.Show(Constants.ErrorOccurred);
                return;
            }

            dtpStartDate.Value = DateTime.Now.AddYears(-1);// 
            dtpStartDate.CustomFormat = Constants.DateFormatMMddyyyy;
            dtpEndDate.CustomFormat = Constants.DateFormatMMddyyyy;
            // Assuming you have another DateTimePicker for the End Date
            dtpEndDate.Value = DateTime.Now;
            cbBatchType.Items.Clear();
            RegisterEvents(this); // Assigning events to all Controls
            // Clear existing items
            foreach (string batchType in batchTypes)
            {
                cbBatchType.Items.Add(batchType);
            }
            string pathFile = Constants.LastFolderPathxml;

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
        private void dataGridView_Scroll(object sender, ScrollEventArgs e)// Changing Cursor as Hand on hover
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
        private void RegisterEvents(Control parent)// Assigning events to all Controls
        {
            try
            {
                foreach (Control control in parent.Controls)
                {
                    if (control is Button || control is CheckBox || control is DateTimePicker || control is ComboBox || control is ScrollBar)
                    {
                        control.MouseHover += Control_MouseHover;
                        control.MouseLeave += Control_MouseLeave;
                    }
                    // Check for child controls in containers
                    if (control.HasChildren)
                    {
                        // Assigning events to child Controls
                        RegisterEvents(control);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void LoadBatchStatus()// load data
        {
            try
            {
                PopulateDataGridViewstartedstatus();// populate data in Grid
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void PopulateDataGridViewstartedstatus()// populate data
        {
            try
            {
                string query = Constants.GenerationStarted;
                
                DataTable dataTable = dbHelper.FillTheGrid(query);// to fill the Gird 
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }

                dataGridView.AutoGenerateColumns = false;
                    dataGridView.Columns.Clear();

                    dataGridView.Columns.Add(Constants.BatchId, Constants.BatchIdHeader);
                    dataGridView.Columns[Constants.BatchId].DataPropertyName = Constants.BatchId;
                    dataGridView.Columns.Add(Constants.Type, Constants.BatchTypeHeader);
                    dataGridView.Columns[Constants.Type].DataPropertyName = Constants.Type;
                    dataGridView.Columns.Add(Constants.Description, Constants.BatchDescriptionSp);
                    dataGridView.Columns[Constants.Description].DataPropertyName = Constants.Description;
                    dataGridView.Columns.Add(Constants.FileName, Constants.FileNamesp);
                    dataGridView.Columns[Constants.FileName].DataPropertyName = Constants.FileName;
                    dataGridView.Columns.Add(Constants.ConversionStartedAt, Constants.ConversionStartedAtHeader);
                    dataGridView.Columns[Constants.ConversionStartedAt].DataPropertyName = Constants.ConversionStartedAt;
                    dataGridView.Columns.Add(Constants.ConversionEndedAt, Constants.ConversionEndedAtHeader);
                    dataGridView.Columns[Constants.ConversionEndedAt].DataPropertyName = Constants.ConversionEndedAt;
                    dataGridView.Columns.Add(Constants.GenerationStartedAt, Constants.GenerationStartedAtHeader);
                    dataGridView.Columns[Constants.GenerationStartedAt].DataPropertyName = Constants.GenerationStartedAt;
                    dataGridView.Columns.Add(Constants.GenerationEndedAt, Constants.GenerationEndedAtHeader);
                    dataGridView.Columns[Constants.GenerationEndedAt].DataPropertyName = Constants.GenerationEndedAt;
                    dataGridView.Columns.Add(Constants.Status, Constants.Status);
                    dataGridView.Columns[Constants.Status].DataPropertyName = Constants.Status;
                    dataGridView.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)// Incresing of Width of Grid
        {
            try
            {
                // Check if the Constants.BatchId column exists before setting its width
                if (dataGridView.Columns.Contains(Constants.BatchId))
                {
                    dataGridView.Columns[Constants.BatchId].Width = 205; // Set the width to 200 pixels
                }
                if (dataGridView.Columns.Contains(Constants.Type))
                {
                    dataGridView.Columns[Constants.Type].Width = 205; // Set the width to 200 pixels
                }
                if (dataGridView.Columns.Contains(Constants.Type))
                {
                    dataGridView.Columns[Constants.Type].Width = 205; // Set the width to 200 pixels
                }
                if (dataGridView.Columns.Contains(Constants.Description))
                {
                    dataGridView.Columns[Constants.Description].Width = 205; // Set the width to 200 pixels
                }
                if (dataGridView.Columns.Contains(Constants.FileName))
                {
                    dataGridView.Columns[Constants.FileName].Width = 205; // Set the width to 200 pixels
                }
                if (dataGridView.Columns.Contains(Constants.ConversionStartedAt))
                {
                    dataGridView.Columns[Constants.ConversionStartedAt].Width = 250; // Set the width to 200 pixels
                }
                if (dataGridView.Columns.Contains(Constants.ConversionEndedAt))
                {
                    dataGridView.Columns[Constants.ConversionEndedAt].Width = 250; // Set the width to 200 pixels
                }
                if (dataGridView.Columns.Contains(Constants.GenerationStartedAt))
                {
                    dataGridView.Columns[Constants.GenerationStartedAt].Width = 250; // Set the width to 200 pixels
                }
                if (dataGridView.Columns.Contains(Constants.GenerationEndedAt))
                {
                    dataGridView.Columns[Constants.GenerationEndedAt].Width = 250; // Set the width to 200 pixels
                }

                if (dataGridView.Columns.Contains(Constants.Status))
                {
                    dataGridView.Columns[Constants.Status].Width = 205; // Set the width to 200 pixels
                }
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;

                dbHelper.LogError(ex.Message, ex.StackTrace, nameof(DataGridView_DataBindingComplete), Constants.ServiceCttohcc, lineNumber,Constants.HccCode);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
            }
        }
        public void PopulateDataGridView(DataTable dt)// Populating GRID from table
        {
            try
            {
                string query = Constants.GenerationOchin;
                DataTable dataTable = dbHelper.FillTheGrid(query);// to fill the Gird 
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }

                dataGridView.AutoGenerateColumns = false;
                dataGridView.Columns.Clear();

                dataGridView.Columns.Add(Constants.BatchId, Constants.BatchIdHeader);
                dataGridView.Columns[Constants.BatchId].DataPropertyName = Constants.BatchId;
                dataGridView.Columns.Add(Constants.Type, Constants.BatchTypeHeader);
                dataGridView.Columns[Constants.Type].DataPropertyName = Constants.Type;
                dataGridView.Columns.Add(Constants.Description, Constants.BatchDescriptionSp);
                dataGridView.Columns[Constants.Description].DataPropertyName = Constants.Description;
                dataGridView.Columns.Add(Constants.FileName, Constants.FileNamesp);
                dataGridView.Columns[Constants.FileName].DataPropertyName = Constants.FileName;
                AddDateTime(Constants.ConversionStartedAt, Constants.ConversionStartedAtHeader, dataGridView);
                AddDateTime(Constants.ConversionEndedAt, Constants.ConversionEndedAtHeader, dataGridView);
                AddDateTime(Constants.GenerationStartedAt, Constants.GenerationStartedAtHeader, dataGridView);
                AddDateTime(Constants.GenerationEndedAt, Constants.GenerationEndedAtHeader, dataGridView);
                dataGridView.Columns.Add(Constants.Status, Constants.Status);
                dataGridView.Columns[Constants.Status].DataPropertyName = Constants.Status;
                dataGridView.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                dbHelper.LogError(ex.Message, ex.StackTrace, nameof(PopulateDataGridView), Constants.ServiceCttohcc, lineNumber, Constants.HccCode);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
            }
        }

        private async void btnGeneration_Click(object sender, EventArgs e)// handle the Generation Process
        {
            try
            {
                if (dataGridView.SelectedRows.Count == 0)
                {
                    MessageBox.Show(Constants.PleaseselectarowwithaBatchIDtoproceed, Constants.Ochintorwdeconversion, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method early if no row is selected
                }

                int selectedRowIndex = dataGridView.SelectedRows[0].Index;
                int selectedBatchId = Convert.ToInt32(dataGridView.Rows[selectedRowIndex].Cells[Constants.BatchId].Value.ToString());
                int selectedRowCount = dataGridView.SelectedRows.Count;
                if (selectedRowCount != 1)
                {
                    MessageBox.Show(Constants.Pleaseselectonlyonebatchatatime, Constants.ErrorCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Exit the method early
                }

                if (selectedBatchId == 0)
                {
                    MessageBox.Show(Constants.PleaseselectabatchtogenerateXml, Constants.ErrorCode, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (txtPath.Text == "")
                {
                    MessageBox.Show(Constants.Pleaseselectthefolder, Constants.ErrorCode, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                btnClose.Text = Constants.Abort;
                btnGeneration.Enabled = false;
                btnNext.Enabled = false;

                // Calling Client Xml Method
                string fileName = dataGridView.Rows[selectedRowIndex].Cells[Constants.SmallFileName].Value.ToString();

                if (fileName.Contains(Constants.Client))
                {
                    await GenerateXmlForClientsAsync(selectedBatchId); // generate xml clients
                }

                if (fileName.Contains(Constants.Service))
                {
                    await GenerateXmlForServicesAsync(selectedBatchId); // generate xml services
                }

                btnClose.Text = Constants.Close;
                btnGeneration.Enabled = true;
                btnNext.Enabled = true;
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                dbHelper.LogError(ex.Message, ex.StackTrace, nameof(btnGeneration_Click), Constants.ServiceCttohcc, lineNumber, Constants.HccCode);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
            }
        }

        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) // Description of Status values
        {
            try
            {
                if (dataGridView.Columns[e.ColumnIndex].Name == Constants.Status)
                {
                    string statusValue = e.Value?.ToString();

                    // to get value of the ListId
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
        public async Task GenerateXmlForServicesAsync(int batchId)// Handle the Service Xml File Process
        {
            try
            {
                // progressClient.Value = 0;
                var batchDetails = await dbHelper.GetBatchDetailsFromSpAgenearationservices(batchId);// to check whether the generation completed or not
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
                if (batchDetails.GenerationStartedAt != null && batchDetails.GenerationEndedAt != null)
                {
                    MessageBox.Show(Constants.Generationhasalreadybeencompletedforthisbatch, Constants.GenerateXml, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtProgressBar.Text = Constants.ZeroPercent;
                    txtClient.Text = Constants.ZeroPercent;
                    txtBatchid.Text = null;
                    txtUploadStarted.Text = null;// 
                    txtUploadEnded.Text = null;
                    txtTotaltime.Text = null;
                    return;
                }
                btnClose.Text = Constants.Abort;
                txtBatchid.Text = batchId.ToString();
                DateTime startTime = DateTime.Now;
                string time = startTime.ToString(Constants.MMddyyyyHHmmssbkslash);
                txtUploadStarted.Text = time;
                txtUploadEnded.Text = null;
                txtTotaltime.Text = null;

                int selectedRowIndex = dataGridView.SelectedRows[0].Index;
                int selectedBatchId = Convert.ToInt32(dataGridView.Rows[selectedRowIndex].Cells[Constants.BatchId].Value.ToString());

                
                // Getting total rows from particular table
                int totalRows = dbHelper.GetTotalRowsForBatch(selectedBatchId);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }

                List<Dictionary<string, string>> data;

                // Update status of service and fetch error data
                data = chkError.Checked ? dbHelper.GetServiceserror(selectedBatchId) :

                    // Update status of service and fetch standard service data
                    dbHelper.GetServices(selectedBatchId);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }

                // calling XmlStructure Function
                List<Dictionary<string, string>> xmlStructure = dbHelper.GetXmlStructure();// get xml Structure
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }

                string baseFilename = Constants.ServiceCttohcc;
                dbHelper.Log(Constants.GeneratetoHcCforbatchIdStarted, Constants.ClientTrackCode, baseFilename, Constants.Uploadct);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }

                // calling Service xml file generation Method
                XElement xml = await GenerateXmlService(data, xmlStructure);// generate services Xml
                string folderPath = txtPath.Text;
                Directory.CreateDirectory(folderPath); // Create folder if it doesn't exist
                string baseFileName = $"{Constants.ServiceXmlHeader}{DateTime.Now.ToString(Constants.DdMMyyyy)}{Constants.XmlFooter}";
                string servicesFilePath = Path.Combine(folderPath, baseFileName);

                // Check if file exists and rename accordingly
                int fileCount = 1;
                string fileExtension = Path.GetExtension(baseFileName);
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(baseFileName);

                // Save the XML to a file with automatic numbering if the file exists
                // Loop to increment the filename if the file already exists
                while (File.Exists(servicesFilePath))
                {
                    servicesFilePath = Path.Combine(folderPath, $"{fileNameWithoutExtension}_{fileCount}{fileExtension}");
                    fileCount++;
                }

                // Now save the file
                xml.Save(servicesFilePath);

                DateTime endTime = DateTime.Now;
                TimeSpan totalTime = endTime - startTime;
                string eTime = endTime.ToString(Constants.MMddyyyyHHmmssbkslash);
                double totalSeconds = totalTime.TotalSeconds;
                txtUploadEnded.Text = eTime;
                txtTotaltime.Text = $@"{totalSeconds:F2} {Constants.Seconds}";
                btnClose.Text = Constants.Close;
                // update services status
                UpdateStatusColumnServices(selectedBatchId, Constants.Hccxmlstatusf, startTime, endTime);
                // Populating GRID from table
                PopulateDataGridView(new DataTable());// populate data
                dbHelper.Log(Constants.GeneratetoHcCformatcompletedsuccessfully, Constants.ClientTrackCode, baseFilename, Constants.Uploadct);
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
                dbHelper.LogError(ex.Message, ex.StackTrace, nameof(GenerateXmlForServicesAsync), Constants.ServiceCttohcc, lineNumber, Constants.HccCode);
                MessageBox.Show($@"{Constants.AnErrorOccurred}{ex.Message}", Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (dbHelper.ErrorOccurred)
                {
                    return;
                }
            }
        }
        private async Task<XElement> GenerateXmlService(List<Dictionary<string, string>> data, List<Dictionary<string, string>> xmlStructure)// Service Xml Generation
        {
            try
            {
                string rootTag = XmlConstants.TagNumberFive;
                XElement root = CreateRootElement(xmlStructure, rootTag);

                // Create a Source system for client and services xml file
                XElement sourceSystemElement = CreateSourceSystemElement(xmlStructure, XmlConstants.TagNumberTen);

                // Adding the Source System Element for Client and Service Xml File
                AddDynamicElements(sourceSystemElement, xmlStructure, rootTag);

                // Adding the Client Profile Tag for Client File
                string contractServiceTagName = GetContractServiceTagName(xmlStructure, XmlConstants.ContractServicesTagNumber);

                // Adding the Client Profile Title Attribute For Client file
                string contractServicesTitle = GetContractServicesTitle(xmlStructure, XmlConstants.ContractServicesTagNumber);
                int totalRows = data.Count;
                int processedRows = 0;

                foreach (var dataRow in data)
                {
                    processedRows++;
                    await Updateprogress(processedRows, totalRows);// progress of lines inserted

                    XElement contractServicesElement = new XElement(contractServiceTagName);

                    if (!string.IsNullOrEmpty(contractServicesTitle))
                    {
                        contractServicesElement.SetAttributeValue(XmlConstants.ContractServicesTitleIdentifier, contractServicesTitle);
                    }

                    foreach (var structureRow in xmlStructure.Where(x => int.Parse(x[XmlConstants.TagNumber]) > 30 && int.Parse(x[XmlConstants.TagNumber]) <= 110))
                    {
                        (string tagNumber, string tagName, string fieldName, string presetValue, string defaultValue) = GetXmlStructureValues(structureRow);// to get the Structure Values

                        string value = null;

                        if (dataRow.ContainsKey(fieldName) && !string.IsNullOrEmpty(dataRow[fieldName]))
                        {
                            value = dataRow[fieldName] ?? defaultValue;
                        }
                        else if (!string.IsNullOrEmpty(presetValue))
                        {
                            value = presetValue;
                        }
                        else
                        {
                            value = defaultValue;
                        }

                        contractServicesElement.Add(new XElement(tagName, value));
                    }

                    sourceSystemElement.Add(contractServicesElement);

                    await Updateprogress(processedRows, totalRows);// progress of lines inserted
                    progressServices.Minimum = 0;
                    progressServices.Maximum = totalRows;

                }
                root.Add(sourceSystemElement);
                return root;
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrames().FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                
                dbHelper.LogError(ex.Message, ex.StackTrace, nameof(GenerateXmlService), Constants.ServiceCttohcc, lineNumber, Constants.HccCode);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                }
                throw;
            }
        }
        private async Task<XElement> GenerateXmlClient(List<Dictionary<string, string>> data, List<Dictionary<string, string>> xmlStructure, int batchId)// Client Xml Generation
        {
            try
            {
                // Create the root element
                string rootTag = XmlConstants.TagNumberFive;
                XElement root = CreateRootElement(xmlStructure, rootTag);

                // Create the source system element
                XElement sourceSystemElement = CreateSourceSystemElement(xmlStructure, XmlConstants.TagNumberTen);

                // Adding the Source System Element for Client and Service Xml File
                AddDynamicElements(sourceSystemElement, xmlStructure, rootTag);

                // Get the tag name and title for client profile
                string contractServiceTagName = GetContractServiceTagName(xmlStructure, XmlConstants.ContractServicesTagNumber);

                // Get the tag name and title for Service profile
                string contractServicesTitle = GetContractServicesTitle(xmlStructure, XmlConstants.ContractServicesTagNumber);

                int totalRows = data.Count;
                int processedRows = 0;
                await Updateprogressclient(processedRows, totalRows);// progress of lines inserted
                progressClient.Minimum = 0;
                progressClient.Maximum = totalRows;

                var tasks = data.Select(async (dataRow, index) =>
                {
                    processedRows++;
                    await Updateprogressclient(processedRows, totalRows);// progress of lines inserted
                    XElement clientProfileElement = new XElement(contractServiceTagName);

                    // Set title attribute if present
                    if (!string.IsNullOrEmpty(contractServicesTitle))
                    {
                        clientProfileElement.SetAttributeValue(XmlConstants.ContractServicesTitleIdentifier, contractServicesTitle);
                    }

                    XElement currentDemoElement = null;
                    XElement childElement = null;
                    XElement medicalChild = null;
                    XElement medical = null;
                    XElement live = null;
                    XElement livingChild = null;
                    XElement medicalSubChild = null;
                    string currentDemoTitle = null;
                    XElement subChild = null;

                    foreach (var structureRow in xmlStructure.Where(x => int.Parse(x[XmlConstants.TagNumber]) > 30 && int.Parse(x[XmlConstants.TagNumber]) <= 1955))
                    {
                        // Retrieve values using the GetXmlStructureValues method
                        (string tagNumber, string tagName, string fieldName, string presetValue, string defaultValue) = GetXmlStructureValues(structureRow);

                        bool empty = bool.Parse(structureRow[XmlConstants.Empty]);
                        bool hasChild = bool.Parse(structureRow[XmlConstants.HasChild]);
                        string delimi = structureRow[XmlConstants.DelimiterxmlGeneratorId];

                        if (int.TryParse(tagNumber, out int parsedTagNumber))
                        {
                            if (parsedTagNumber >= 85 && parsedTagNumber <= 1895)
                            {
                                if (Enum.IsDefined(typeof(TagNumbers), parsedTagNumber))
                                {
                                    // Add the current demo element to client profile
                                    clientProfileElement.Add(currentDemoElement);

                                    var clientDemoRow = xmlStructure.FirstOrDefault(x => x[XmlConstants.TagNumber] == tagNumber);
                                    if (clientDemoRow != null)
                                    {
                                        string clientDemoTagName = clientDemoRow[XmlConstants.Tag].Split(' ')[0]; currentDemoTitle = clientDemoRow[XmlConstants.Tag].Contains(XmlConstants.ContractServicesTitleIdentifier) ? clientDemoRow[XmlConstants.Tag].Split(Constants.EqualTo)[1].Trim(' ', '"') : null;
                                        currentDemoElement = new XElement(clientDemoTagName);

                                        if (!string.IsNullOrEmpty(currentDemoTitle))
                                        {
                                            currentDemoElement.SetAttributeValue(XmlConstants.ContractServicesTitleIdentifier, currentDemoTitle);
                                        }
                                    }
                                }
                                if (Enum.IsDefined(typeof(ChildTagNumbers), parsedTagNumber))
                                {
                                    // Add child element to current demo element
                                    childElement = new XElement(tagName);
                                    currentDemoElement?.Add(childElement);
                                }
                                else if (!empty && !hasChild && (parsedTagNumber >= 275 && parsedTagNumber <= 385))
                                {
                                    // Add XML element to the child element
                                    AddXmlElement(dataRow, fieldName, presetValue, defaultValue, tagName, childElement);
                                }

                                // Handle specific parsed tag numbers
                                if (parsedTagNumber == 525)
                                {
                                    // to handle race values
                                    HandleRaceValues(dataRow, batchId, xmlStructure, currentDemoElement, childElement);
                                }
                                else if (parsedTagNumber >= 605 && parsedTagNumber <= 1775)
                                {
                                    // Handle the MedicalValues
                                    // this method is an exception for passing parameters as reference variable
                                    HandleMedicalValues(parsedTagNumber, dataRow, batchId, xmlStructure, clientProfileElement, ref medical, ref medicalChild, ref medicalSubChild, tagName, fieldName, presetValue, defaultValue, empty, hasChild);
                                }
                                else if (parsedTagNumber > 1775 && parsedTagNumber <= 1925)
                                {
                                    // Handle the living situation Values
                                    // this method is an exception for passing parameters as reference variable
                                    HandleLivingValues(parsedTagNumber, xmlStructure, clientProfileElement, ref live, ref livingChild, ref subChild, tagName, dataRow, fieldName, presetValue, defaultValue, empty, hasChild);
                                }

                                // Add XML element to the current demo element
                                if (!empty && !hasChild && (parsedTagNumber <= 255 || (parsedTagNumber >= 395 && parsedTagNumber <= 510) || (parsedTagNumber >= 585 && parsedTagNumber <= 595)))
                                {
                                    AddXmlElement(dataRow, fieldName, presetValue, defaultValue, tagName, currentDemoElement);
                                }
                            }
                            else
                            {
                                // Handle the root information section
                                HandleRootInformationSection(empty, hasChild, tagName, dataRow, fieldName, presetValue, defaultValue, clientProfileElement);
                            }
                        }
                    }

                    // Add current demo element to client profile element if it exists and has content
                    if (currentDemoElement != null && currentDemoElement.HasElements)
                    {
                        clientProfileElement.Add(currentDemoElement);
                    }

                    // Add the client profile element to the source system element
                    sourceSystemElement.Add(clientProfileElement);

                    // Making values as Array totalRows to synchronous the execution
                }).ToArray();
                await Task.WhenAll(tasks);

                root.Add(sourceSystemElement);
                return root;
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                dbHelper.LogError (ex.Message, ex.StackTrace, nameof(GenerateXmlClient), Constants.ServiceCttohcc, lineNumber, Constants.HccCode);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                }
                throw;
            }
        }

        private void HandleRaceValues(Dictionary<string, string> dataRow, int batchId, List<Dictionary<string, string>> xmlStructure, XElement currentDemoElement, XElement childElement)// method to handle race values
        {
            try
            {
                string clientId = dataRow.ContainsKey(Constants.Clntid) ? dataRow[Constants.Clntid] : null;
                if (!string.IsNullOrEmpty(clientId))
                {
                    // Retrieve the Rece values using FetchSubClientFromRace Method
                    List<Dictionary<string, string>> subClientData = dbHelper.FetchSubClientValuesFromRace(clientId, batchId);
                    if (dbHelper.ErrorOccurred)
                    {
                        MessageBox.Show(Constants.ErrorOccurred);
                        return;
                    }

                    foreach (var subDataRow in subClientData)
                    {
                        foreach (var subStructureRow in xmlStructure.Where(x => int.TryParse(x[XmlConstants.TagNumber], out int subTagNumber) && subTagNumber >= 525 && subTagNumber <= 540))
                        {
                            (string subTagNumberStr, string subTagName, string subFieldName, string subPresetValue, string subDefaultValue) = GetXmlStructureValues(subStructureRow);
                            if (Enum.IsDefined(typeof(MedicalSubChildDynamic), int.Parse(subTagNumberStr)))
                            {
                                childElement = new XElement(subTagName);
                                currentDemoElement?.Add(childElement);
                            }
                            if (!bool.Parse(subStructureRow[XmlConstants.Empty]) && !bool.Parse(subStructureRow[XmlConstants.HasChild]))
                            {
                                XElement element = new XElement(subTagName);

                                string value = subDataRow.ContainsKey(subFieldName) && !string.IsNullOrEmpty(subDataRow[subFieldName]) ? subDataRow[subFieldName] : !string.IsNullOrEmpty(subPresetValue) ? subPresetValue : subDefaultValue;

                                element.Value = value;
                                childElement?.Add(element);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrames().FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                dbHelper.LogError(ex.Message, ex.StackTrace, nameof(HandleRaceValues), Constants.ServiceCttohcc, lineNumber, Constants.HccCode);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
            }
        }
        // method to handle medical values
        // this method is an exception for passing parameters as reference variable
        private void HandleMedicalValues(int parsedTagNumber, Dictionary<string, string> dataRow, int batchId, List<Dictionary<string, string>> xmlStructure, XElement clientProfileElement, ref XElement medical, ref XElement medicalChild, ref XElement medicalSubChild, string tagName, string fieldName, string presetValue, string defaultValue, bool empty, bool hasChild)
        {
            try
            {
                if (parsedTagNumber == 605)
                {
                    medical = new XElement(tagName);
                    clientProfileElement?.Add(medical);
                }
                if (Enum.IsDefined(typeof(MedicalChild), parsedTagNumber))
                {
                    if (medicalChild != null && medicalChild.HasElements)
                    {
                        medical.Add(medicalChild);
                    }
                    var clientDemoRow = xmlStructure.FirstOrDefault(x => x[XmlConstants.TagNumber] == parsedTagNumber.ToString());
                    if (clientDemoRow != null)
                    {
                        string clientDemoTag = clientDemoRow[XmlConstants.Tag].Split(' ')[0];
                        string currentDemo = clientDemoRow[XmlConstants.Tag].Contains(XmlConstants.ContractServicesTitleIdentifier) ? clientDemoRow[XmlConstants.Tag].Split(Constants.EqualTo)[1].Trim(' ', '"') : null;
                        medicalChild = new XElement(clientDemoTag);

                        if (!string.IsNullOrEmpty(currentDemo))
                        {
                            medicalChild.SetAttributeValue(XmlConstants.ContractServicesTitleIdentifier, currentDemo);
                        }
                    }
                }
                if (parsedTagNumber == 615)
                {
                    // to Adding Element
                    AddXmlElement(dataRow, fieldName, presetValue, defaultValue, tagName, medicalChild);
                }
                else if (Enum.IsDefined(typeof(MedicalSubChild), parsedTagNumber))
                {
                    medicalSubChild = new XElement(tagName);
                    medicalChild?.Add(medicalSubChild);
                }
                // medical Child values
                if (parsedTagNumber == 710 || parsedTagNumber == 825 || parsedTagNumber == 940 || parsedTagNumber == 1010)
                {
                    string clientId = dataRow.ContainsKey(Constants.Clntid) ? dataRow[Constants.Clntid] : null;
                    // This method to handle Medical child values
                    HandleMedicalChildValues(parsedTagNumber, clientId, batchId, xmlStructure, medicalChild, medicalSubChild, dataRow);
                }
                else if (!empty && !hasChild && !(parsedTagNumber >= 1655 && parsedTagNumber <= 1660) && !(parsedTagNumber >= 1510 && parsedTagNumber <= 1540) && !(parsedTagNumber > 710 && parsedTagNumber <= 810) && !(parsedTagNumber > 825 && parsedTagNumber <= 850) && !(parsedTagNumber > 940 && parsedTagNumber <= 960) && !(parsedTagNumber > 1010 && parsedTagNumber <= 1050))
                {
                    // This method to handle Medical Values
                    AddXmlElement(dataRow, fieldName, presetValue, defaultValue, tagName, medicalSubChild);
                }
                else if ((parsedTagNumber >= 1510 && parsedTagNumber <= 1540) || (parsedTagNumber >= 1655 && parsedTagNumber <= 1660))
                {
                    // This method to Add Medical Values
                    AddXmlElement(dataRow, fieldName, presetValue, defaultValue, tagName, medicalChild);
                }
                if (parsedTagNumber == 1700)
                {
                    if (medicalChild != null && medicalChild.HasElements)
                    {
                        medical.Add(medicalChild);
                    }
                }
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrames().FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                dbHelper.LogError(ex.Message, ex.StackTrace, nameof(HandleMedicalValues), Constants.ServiceCttohcc, lineNumber, Constants.HccCode);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
            }
        }
        // method to handle living values
        // this method is an exception for passing parameters as reference variable
        private void HandleLivingValues(int parsedTagNumber, List<Dictionary<string, string>> xmlStructure, XElement clientProfileElement, ref XElement live, ref XElement livingChild, ref XElement subChild, string tagName, Dictionary<string, string> dataRow, string fieldName, string presetValue, string defaultValue, bool empty, bool hasChild)
        {
            try
            {
                if (parsedTagNumber == 1780)
                {
                    live = new XElement(tagName);
                }
                else if (parsedTagNumber == 1785 || parsedTagNumber == 1865)
                {
                    // Ensure livingChild is not null and has elements before adding to live
                    if (livingChild != null && livingChild.HasElements)
                    {
                        live.Add(livingChild);
                    }
                    var clientDemoRow = xmlStructure.FirstOrDefault(x => x[XmlConstants.TagNumber] == parsedTagNumber.ToString());
                    if (clientDemoRow != null)
                    {
                        string clientDemoTag = clientDemoRow[XmlConstants.Tag].Split(' ')[0];
                        string currentDemo = clientDemoRow[XmlConstants.Tag].Contains(XmlConstants.ContractServicesTitleIdentifier) ? clientDemoRow[XmlConstants.Tag].Split(Constants.EqualTo)[1].Trim(' ', '"') : null;

                        livingChild = new XElement(clientDemoTag);

                        if (!string.IsNullOrEmpty(currentDemo))
                        {
                            livingChild.SetAttributeValue(XmlConstants.ContractServicesTitleIdentifier, currentDemo);
                        }
                    }
                }
                else if (Enum.IsDefined(typeof(LiveSubChild), parsedTagNumber))
                {
                    subChild = new XElement(tagName);
                    livingChild?.Add(subChild);
                }
                else if (!empty && !hasChild)
                {
                    // Add an XML element to the SubChild element for Living situation
                    XElement element = new XElement(tagName);
                    string value = dataRow.ContainsKey(fieldName) && !string.IsNullOrEmpty(dataRow[fieldName]) ? dataRow[fieldName] : !string.IsNullOrEmpty(presetValue) ? presetValue : defaultValue;

                    element.Value = value;
                    subChild?.Add(element);
                }
                // After all elements have been processed, ensure livingChild is added to live
                if (parsedTagNumber == 1895)
                {
                    if (livingChild != null && livingChild.HasElements)
                    {
                        live.Add(livingChild);
                    }

                    // Ensure that live is added to clientProfileElement if it has elements
                    if (live != null && live.HasElements)
                    {
                        clientProfileElement.Add(live);
                    }
                }
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                dbHelper.LogError(ex.Message, ex.StackTrace, nameof(HandleLivingValues), Constants.ServiceCttohcc, lineNumber, Constants.HccCode);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
            }
        }
        // method to handle Medical child values
        private void HandleMedicalChildValues(int parsedTagNumber, string clientId, int batchId, List<Dictionary<string, string>> xmlStructure, XElement medicalChild, XElement medicalSubChild, Dictionary<string, string> dataRow)
        {
            try
            {
                if (string.IsNullOrEmpty(clientId))
                    return;

                List<Dictionary<string, string>> subClientData = null;
                int min = 0, max = 0;
                Func<string, int, List<Dictionary<string, string>>> fetchMethod = null;

                switch (parsedTagNumber)
                {
                    case 710:
                        fetchMethod = dbHelper.FetchSubClientValuesFromMedC4;// fetch particular client Medical values
                        if (dbHelper.ErrorOccurred)
                        {
                            MessageBox.Show(Constants.ErrorOccurred);
                            return;
                        }
                        min = 710;
                        max = 735;
                        break;
                    case 825:
                        fetchMethod = dbHelper.FetchSubClientValuesFromMedVl;// fetch particular client Medical values
                        if (dbHelper.ErrorOccurred)
                        {
                            MessageBox.Show(Constants.ErrorOccurred);
                            return;
                        }
                        min = 825;
                        max = 850;
                        break;
                    case 940:
                        fetchMethod = dbHelper.FetchSubClientValuesFromHivTest;// fetch particular client HIV test values
                        if (dbHelper.ErrorOccurred)
                        {
                            MessageBox.Show(Constants.ErrorOccurred);
                            return;
                        }
                        min = 940;
                        max = 960;
                        break;
                    case 1010:
                        fetchMethod = dbHelper.FetchSubClientValuesFromInsur;// fetch particular client Insurance values
                        if (dbHelper.ErrorOccurred)
                        {
                            MessageBox.Show(Constants.ErrorOccurred);
                            return;
                        }
                        min = 1010;
                        max = 1050;
                        break;
                }

                if (fetchMethod != null)
                {
                    subClientData = fetchMethod(clientId, batchId);
                    var subClientDataList = subClientData.Count > 0 ? subClientData : new List<Dictionary<string, string>> { new Dictionary<string, string>() };

                    foreach (var subDataRow in subClientDataList)
                    {
                        foreach (var subStructureRow in xmlStructure.Where(x => int.TryParse(x[XmlConstants.TagNumber], out int subTagNumber) && subTagNumber >= min && subTagNumber <= max))
                        {
                            (string subTagNumberStr, string subTagName, string subFieldName, string subPresetValue, string subDefaultValue) = GetXmlStructureValues(subStructureRow);

                            if (Enum.IsDefined(typeof(MedicalSubChildDynamic), int.Parse(subTagNumberStr)))
                            {
                                medicalSubChild = new XElement(subTagName);
                                medicalChild?.Add(medicalSubChild);
                            }

                            if (!bool.Parse(subStructureRow[XmlConstants.Empty]) && !bool.Parse(subStructureRow[XmlConstants.HasChild]))
                            {
                                XElement subElement = new XElement(subTagName);
                                // string value = Convert.ToInt32(subDataRow.ContainsKey(subFieldName) && !string.IsNullOrEmpty(subDataRow[subFieldName]) ? subDataRow[subFieldName] : !string.IsNullOrEmpty(subPresetValue) ? subPresetValue : subDefaultValue).ToString();

                                string value = subDataRow.ContainsKey(subFieldName) && !string.IsNullOrEmpty(subDataRow[subFieldName]) ? subDataRow[subFieldName] : !string.IsNullOrEmpty(subPresetValue) ? subPresetValue : subDefaultValue;

                                subElement.Value = value;
                                medicalSubChild?.Add(subElement);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrames().FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                dbHelper.LogError(ex.Message, ex.StackTrace, nameof(HandleMedicalChildValues), Constants.ServiceCttohcc, lineNumber, Constants.HccCode);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
            }

        }
        // to handle the Adding Element
        private void AddXmlElement(Dictionary<string, string> dataRow, string fieldName, string presetValue, string defaultValue, string tagName, XElement parentElement)
        {
            try
            {
                XElement element = new XElement(tagName);
                string value = dataRow.ContainsKey(fieldName) && !string.IsNullOrEmpty(dataRow[fieldName]) ? dataRow[fieldName] : !string.IsNullOrEmpty(presetValue) ? presetValue : defaultValue;

                element.Value = value;
                parentElement?.Add(element);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // This Method handle the root information Section
        private void HandleRootInformationSection(bool empty, bool hasChild, string tagName, Dictionary<string, string> dataRow, string fieldName, string presetValue, string defaultValue, XElement clientProfileElement)
        {
            try
            {
                if (!empty && !hasChild)
                {
                    XElement elementToAdd = new XElement(tagName);
                    string value;

                    if (dataRow.ContainsKey(fieldName) && !string.IsNullOrEmpty(dataRow[fieldName]))
                    {
                        value = dataRow[fieldName];
                    }
                    else if (!string.IsNullOrEmpty(presetValue))
                    {
                        value = presetValue;
                    }
                    else
                    {
                        value = defaultValue;
                    }

                    elementToAdd.Value = value;
                    clientProfileElement.Add(elementToAdd);
                }
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrames().FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                dbHelper.LogError(ex.Message, ex.StackTrace, nameof(HandleRootInformationSection), Constants.ServiceCttohcc, lineNumber, Constants.HccCode);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
            }
        }
        // create the rootElemt of client and serives xml file
        public XElement CreateRootElement(List<Dictionary<string, string>> xmlStructure, string rootTag)
        {
            try
            {
                string tag = xmlStructure.FirstOrDefault(x => x[XmlConstants.TagNumber] == rootTag)?[XmlConstants.Tag];

                return new XElement(tag);
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                dbHelper.LogError(ex.Message, ex.StackTrace, nameof(CreateRootElement), Constants.ServiceCttohcc, lineNumber, Constants.HccCode);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                }
                throw;
            }
        }
        // Create a Source system for client and services xml file
        public XElement CreateSourceSystemElement(List<Dictionary<string, string>> xmlStructure, string tagNumber)
        {
            try
            {
                string sourceSystemTag = xmlStructure.FirstOrDefault(x => x[XmlConstants.TagNumber] == tagNumber)?[XmlConstants.Tag];
                return new XElement(sourceSystemTag ?? string.Empty);
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                dbHelper.LogError(ex.Message, ex.StackTrace, nameof(CreateSourceSystemElement), Constants.ServiceCttohcc, lineNumber, Constants.HccCode);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                }
                throw;
            }
        }
        // Adding the Source System Element for Client and Service Xml File
        public void AddDynamicElements(XElement parentElement, List<Dictionary<string, string>> xmlStructure, string rootTag)
        {
            try
            {
                foreach (var structureRow in xmlStructure.Where(x => int.TryParse(x[XmlConstants.TagNumber], out int tagNumber) && tagNumber >= 15 && tagNumber < 30 && x[XmlConstants.Tag] != rootTag))
                {
                    string tagName = structureRow[XmlConstants.Tag];
                    string presetValue = structureRow[XmlConstants.PresetValue];
                    string defaultValue = structureRow[XmlConstants.Default];
                    string value = !string.IsNullOrEmpty(presetValue) ? presetValue : defaultValue;

                    parentElement.Add(new XElement(tagName, value));
                }
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                dbHelper.LogError(ex.Message, ex.StackTrace, nameof(AddDynamicElements), Constants.ServiceCttohcc, lineNumber, Constants.HccCode);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
            }
        }
        public static string GetContractServiceTagName(List<Dictionary<string, string>> xmlStructure, string tagNumber) // Adding the Client Profile Tag for Client File
        {
            try
            {
                string contractServicesTagName = xmlStructure.FirstOrDefault(x => x[XmlConstants.TagNumber] == tagNumber)?[XmlConstants.Tag].Split(' ')[0];
                return contractServicesTagName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }
        public static string GetContractServicesTitle(List<Dictionary<string, string>> xmlStructure, string tagNumber) // Adding the Client Profile Title Attribute For Client file
        {
            try
            {
                var contractServicesRow = xmlStructure.FirstOrDefault(x => x[XmlConstants.TagNumber] == tagNumber && x[XmlConstants.Tag].StartsWith(GetContractServiceTagName(xmlStructure, tagNumber)));
                return contractServicesRow != null && contractServicesRow[XmlConstants.Tag].Contains(XmlConstants.ContractServicesTitleIdentifier) ? contractServicesRow[XmlConstants.Tag].Split(Constants.EqualTo)[1].Trim(' ', '"') : null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }

        }
        public static (string tagNumber, string tagName, string fieldName, string presetValue, string defaultValue) GetXmlStructureValues(Dictionary<string, string> structureRow)// Return the Structure Values
        {
            try
            {
                string tagNumber = structureRow[XmlConstants.TagNumber];
                string tagName = structureRow[XmlConstants.Tag];
                string fieldName = structureRow[XmlConstants.Field];
                string presetValue = structureRow[XmlConstants.PresetValue];
                string defaultValue = structureRow[XmlConstants.Default];

                return (tagNumber, tagName, fieldName, presetValue, defaultValue);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return (XmlConstants.TagNumber, XmlConstants.TagNumber, XmlConstants.TagNumber, XmlConstants.TagNumber, XmlConstants.TagNumber);
            }
        }

        private async Task Updateprogress(int insertedRows, int totalRows) // Handle the Progress Values
        {
            try
            {
                int progressPercentage = (int)((double)insertedRows / totalRows * 100);
                string progressInfo = $"{insertedRows}/{totalRows} ({progressPercentage}%)";

                txtProgressBar.Text = progressInfo;
                progressServices.Value = insertedRows;

                await Task.Delay(1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PopulateDataGridView() // Handle the Grid View
        {
            try
            {
                string query = Constants.Generation;
                DataTable dataTable = dbHelper.FillTheGrid(query); // to fill the Gird
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }

                dataGridView.AutoGenerateColumns = false;
                dataGridView.Columns.Clear();

                dataGridView.Columns.Add(Constants.BatchId, Constants.BatchIdHeader);
                dataGridView.Columns[Constants.BatchId].DataPropertyName = Constants.BatchId;
                dataGridView.Columns.Add(Constants.Type, Constants.BatchTypeHeader);
                dataGridView.Columns[Constants.Type].DataPropertyName = Constants.Type;
                dataGridView.Columns.Add(Constants.Description, Constants.BatchDescriptionSp);
                dataGridView.Columns[Constants.Description].DataPropertyName = Constants.Description;
                dataGridView.Columns.Add(Constants.FileName, Constants.FileNamesp);
                dataGridView.Columns[Constants.FileName].DataPropertyName = Constants.FileName;
                AddDateTime(Constants.ConversionStartedAt, Constants.ConversionStartedAtHeader, dataGridView);
                AddDateTime(Constants.ConversionEndedAt, Constants.ConversionEndedAtHeader, dataGridView);
                AddDateTime(Constants.GenerationStartedAt, Constants.GenerationStartedAtHeader, dataGridView);
                AddDateTime(Constants.GenerationEndedAt, Constants.GenerationEndedAtHeader, dataGridView);
                dataGridView.Columns.Add(Constants.Status, Constants.Status);
                dataGridView.Columns[Constants.Status].DataPropertyName = Constants.Status;
                dataGridView.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddDateTime(string name, string value, DataGridView dataGridViews) // Adding the Date Format to the Column
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

        private void UpdateStatusColumn(int batchId, int listId, DateTime startTime, DateTime endTime)// update status
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.Cells[Constants.BatchId].Value.ToString() == batchId.ToString())
                    {
                        row.Cells[Constants.Status].Value = listId; // Store the ListID as the status value

                        // to update status in batch table
                        dbHelper.UpdateStatus(batchId,listId, startTime, endTime);
                        if (dbHelper.ErrorOccurred)
                        {
                            MessageBox.Show(Constants.ErrorOccurred);
                            return;
                        }

                        // Refresh the dataGridView outside the loop
                        PopulateDataGridView(new DataTable());
                        dataGridView.Refresh();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UpdateStatusColumnServices(int batchId, int listId, DateTime startTime, DateTime endTime)// update services status
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.Cells[Constants.BatchId].Value.ToString() == batchId.ToString())
                    {
                        row.Cells[Constants.Status].Value = listId; // Store the ListID as the status value

                        // to update the status of service file
                        dbHelper.UpdateServiceStatus(batchId, listId, startTime, endTime);
                        if (dbHelper.ErrorOccurred)
                        {
                            MessageBox.Show(Constants.ErrorOccurred);
                            return;
                        }

                        // Refresh the dataGridView outside the loop
                        PopulateDataGridView(new DataTable());
                        dataGridView.Refresh();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public FrmGeneratorXml(string message, int displayDuration)// Automation process
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

        public async Task GenerateXmlForClientsAsync(int batchId)// Clients xml generation
        {
            try
            {
                // progressServices.Value = 0;
                var batchDetails = await dbHelper.GetBatchDetailsFromSpAgenearationlients(batchId);// to check whether the generation completed or not
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
                if (batchDetails.GenerationStartedAt != null && batchDetails.GenerationEndedAt != null)
                {
                    MessageBox.Show(Constants.Generationhasalreadybeencompletedforthisbatch, Constants.GenerateXml, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtProgressBar.Text = Constants.ZeroPercent;
                    txtClient.Text = Constants.ZeroPercent;
                    txtBatchid.Text = null;
                    txtUploadStarted.Text = null;
                    txtUploadEnded.Text = null;
                    txtTotaltime.Text = null;
                    return;
                }
                btnClose.Text = Constants.Abort;
                txtUploadEnded.Text = null;
                txtTotaltime.Text = null;
                int selectedRowIndex = dataGridView.SelectedRows[0].Index;
                int selectedBatchId = Convert.ToInt32(dataGridView.Rows[selectedRowIndex].Cells[Constants.BatchId].Value.ToString());

                int totalRows = dbHelper.GetTotalRowsForBatchclient(batchId);// Getting total rows from particular table
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }

                DateTime startTime = DateTime.Now;

                txtBatchid.Text = batchId.ToString();
                string time = startTime.ToString(Constants.MMddyyyyHHmmssbkslash);
                txtUploadStarted.Text = time;

                // calling Store Procedure Function
                List<Dictionary<string, string>> data = dbHelper.GetClients(selectedBatchId);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }

                // calling XmlStructure Function
                List<Dictionary<string, string>> xmlStructure = dbHelper.GetClientFileXmlStructure();
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }

                // calling Service xml file generation Method
                XElement xml = await GenerateXmlClient(data, xmlStructure, selectedBatchId);

                // Save the XML to a file with automatic numbering if the file exists
                string folderPath = txtPath.Text;
                Directory.CreateDirectory(folderPath); // Create folder if it doesn't exist

                // Initial filename with the current date in the desired format (ddMMyyyy)
                string baseFileName = $"{Constants.ClientXmlHeader}{DateTime.Now.ToString(Constants.DdMMyyyy)}{Constants.XmlFooter}";
                string servicesFilePath = Path.Combine(folderPath, baseFileName);

                // Check if file exists and rename accordingly
                int fileCount = 1;
                string fileExtension = Path.GetExtension(baseFileName);
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(baseFileName);

                // Loop to increment the filename if the file already exists
                while (File.Exists(servicesFilePath))
                {
                    // Append number to the filename (e.g., ClientDetails_0246_0689_ddmmyyyy_143100_2.xml)
                    servicesFilePath = Path.Combine(folderPath, $"{fileNameWithoutExtension}_{fileCount}{fileExtension}");
                    fileCount++;
                }

                // Now save the file
                xml.Save(servicesFilePath);

                DateTime endedTime = DateTime.Now;
                TimeSpan totalTime = endedTime - startTime;
                string eTime = endedTime.ToString(Constants.MMddyyyyHHmmssbkslash);
                double totalSeconds = totalTime.TotalSeconds;
                txtUploadEnded.Text = eTime;
                txtTotaltime.Text = $@"{totalSeconds:F2} {Constants.Seconds}";
                DateTime endtime = DateTime.Now;

                // update status
                UpdateStatusColumn(selectedBatchId, Constants.Hccxmlstatusf, startTime, endtime);

                // Handle the Grid View 
                PopulateDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"{Constants.AnErrorOccurred}{ex.Message}", Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task Updateprogressclient(int insertedRows, int totalRows)// Progress of rows Insertion
        {
            try
            {
                int progressPercentage = (int)((double)insertedRows / totalRows * 100);
                string progressInfo = $"{insertedRows}/{totalRows} ({progressPercentage}%)";

                txtClient.Text = progressInfo;
                progressClient.Value = insertedRows;

                await Task.Delay(1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)// close main form
        {
            try
            {
                if (btnClose.Text == Constants.Close)
                {
                    Close();
                    Application.Restart();
                    return;
                }
                // int batchId = dbHelper.GetNextBatchID();
                int selectedRowIndex = dataGridView.SelectedRows[0].Index;
                int batchId = Convert.ToInt32(dataGridView.Rows[selectedRowIndex].Cells[Constants.BatchId].Value.ToString());
                String filename = Convert.ToString(dataGridView.Rows[selectedRowIndex].Cells[Constants.FileName].Value.ToString());
                if (btnClose.Text == Constants.Abort)
                {
                    // Prompt the user with a confirmation message
                    DialogResult result = MessageBox.Show(Constants.Areyousureyouwanttoabort, Constants.GenerateXml,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow row in dataGridView.Rows)
                        {
                            row.Cells[Constants.Status].Value = Constants.Xmlabort;
                            break;
                        } // Abort after updating the first row

                        string selectedFolderPath = txtPath.Text;

                        // Update the status of the selected batch to Status "19" (Abort)
                        dbHelper.UpdateBatchStatusabort(batchId, Constants.Xmlabort, filename);
                        if (dbHelper.ErrorOccurred)
                        {
                            MessageBox.Show(Constants.ErrorOccurred);
                            return;
                        }

                        // Delete XML files in the specified directory
                        DeleteXmlFiles(selectedFolderPath);

                        // Show a message box indicating successful abort
                        MessageBox.Show(Constants.AbortedSuccessfully, Constants.GenerateXml);
                        Application.Restart();
                    }
                    if (result == DialogResult.No)
                    {
                        // User clicked "No" or closed the message box, do nothing
                    }
                }
            }
            catch (Exception ex)
            {
                // Display or log the exception message
                MessageBox.Show(Constants.Errorsp + ex.Message, Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteXmlFiles(string directoryPath) // Deleting the xml after abort
        {
            try
            {
                if (Directory.Exists(directoryPath))
                {
                    // Get all XML files in the directory
                    string[] xmlFiles = Directory.GetFiles(directoryPath, Constants.AllXmlExtention);

                    // Delete each XML file
                    foreach (string file in xmlFiles)
                    {
                        File.Delete(file);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"{Constants.ErrorDeletingXmlFiles} {ex.Message}");
                // Log or handle the exception appropriately
                throw; // Re-throw if you want to handle it in the calling method
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)// selecting path to save xml file
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
                        bool allFilesAreXml = files.All(file => Path.GetExtension(file).Equals(Constants.XmlExtention, StringComparison.OrdinalIgnoreCase));

                        // Save the path to the file
                        File.WriteAllText(Constants.LastFolderPathxml, selectedFolderPath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnSubmit_Click(object sender, EventArgs e)// Handle the filtered data
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

                // Retrieve and bind data
                DataTable result = dbHelper.GetParticularnGenerationDatas(batchType, fromDate, endDate);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
                dataGridView.DataSource = result;

                if (result == null || result.Rows.Count == 0)
                {
                    MessageBox.Show(Constants.NoFilterDatasHcc, Constants.FilterTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                // Log the exception or display an error message
                MessageBox.Show($@"{Constants.AnErrorOccurred}{ex.Message}", Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void bnClear_Click(object sender, EventArgs e)// Handle to clear the filter datas
        {
            try
            {
                cbBatchType.Items.Clear();

                // Handle the Grid View 
                PopulateDataGridView();
                dtpEndDate.CustomFormat = Constants.Space;
                dtpEndDate.Format = DateTimePickerFormat.Custom;
                // Do the same for dtpStartDate or any other DateTimePicker if needed
                dtpStartDate.CustomFormat = Constants.Space;
                dtpStartDate.Format = DateTimePickerFormat.Custom;

                // get all batches type except Client Track
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

                txtUploadStarted.Text = "";
                txtUploadEnded.Text = "";
                txtBatchid.Text = "";
                txtTotaltime.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dtpStartDate_ValueChanged(object sender, EventArgs e)// format date
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

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)// format date
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

        private void btnNext_Click(object sender, EventArgs e)// navigate to next page
        {
            try
            {
                FrmMain mainForm = Application.OpenForms.OfType<FrmMain>().FirstOrDefault();

                if (mainForm != null)
                {
                    mainForm.ShowOchinToHccScreenMain();// to restart the Application
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pnlProgress_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView_KeyUp(object sender, KeyEventArgs e) // to Read the Key events
        {
            if (e.KeyCode == Keys.S) // Ctrl + Space to select row
            {
                SelectCurrentRow(); // to select the current row
            }
        }

        private void SelectCurrentRow()
        {
            if (dataGridView.CurrentCell != null) // Ensure a cell is selected
            {
                int rowIndex = dataGridView.CurrentCell.RowIndex; // Get current row index
                dataGridView.ClearSelection(); // Clear previous selection

                // Simulate user clicking the row header
                dataGridView.Rows[rowIndex].Selected = true;
                dataGridView.CurrentCell = dataGridView.Rows[rowIndex].Cells[0]; // Move focus to first cell of the row

                // Optional: Simulate a Mouse Click on Row Header (-1 Column)
                Rectangle headerCellRect = dataGridView.GetCellDisplayRectangle(-1, rowIndex, true);
                Point clickPoint = dataGridView.PointToScreen(new Point(headerCellRect.X + 5, headerCellRect.Y + 5));
                Cursor.Position = clickPoint;
                mouse_event(MOUSEEVENTFLEFTDOWN | MOUSEEVENTFLEFTUP, 0, 0, 0, IntPtr.Zero);
            }
        }
    }

    // Client Profile Tag Numbers
    public enum TagNumbers
    {
        Tag85 = 85,
        Tag135 = 135,
        Tag210 = 210,
        Tag265 = 265,
        Tag395 = 395,
        Tag425 = 425,
        Tag520 = 520,
        Tag580 = 580,
        Tag605 = 605
    }
    // Client Profile Chile Tag numbers
    public enum ChildTagNumbers
    {
        Tag270 = 270,
        Tag330 = 330,
        Tag550 = 550
    }
    // Medical Chile Tag Numbers
    public enum MedicalChild
    {
        Tag610 = 610,
        Tag705 = 705,
        Tag820 = 820,
        Tag935 = 935,
        Tag1005 = 1005,
        Tag1115 = 1115,
        Tag1245 = 1245,
        Tag1355 = 1355,
        Tag1425 = 1425,
        Tag1505 = 1505,
        Tag1550 = 1550,
        Tag1650 = 1650,
        Tag1670 = 1670,
    }
    // Medical Sub Child Tag Numbers
    public enum MedicalSubChild
    {
        Tag620 = 620,
        Tag655 = 655,
        Tag860 = 860,
        Tag895 = 895,
        Tag970 = 970,
        Tag1060 = 1060,
        Tag1120 = 1120,
        Tag1180 = 1180,
        Tag1250 = 1250,
        Tag1300 = 1300,
        Tag1360 = 1360,
        Tag1390 = 1390,
        Tag1430 = 1430,
        Tag1465 = 1465,
        Tag1555 = 1555,
        Tag1585 = 1585,
        Tag1615 = 1615,
        Tag1675 = 1675,
        Tag1700 = 1705,
        Tag1735 = 1735
    }
    // Medical MedicalSubChildDynamic
    public enum MedicalSubChildDynamic
    {
        Tag525 = 525,
        Tag710 = 710,
        Tag745 = 745,
        Tag780 = 780,
        Tag825 = 825,
        Tag940 = 940,
        Tag1010 = 1010

    }
    // public living_situation Child Tag Numbers
    public enum LiveSubChild
    {
        Tag1790 = 1790,
        Tag1825 = 1825,
        Tag1870 = 1870,
        Tag1900 = 1900,
    }

}

