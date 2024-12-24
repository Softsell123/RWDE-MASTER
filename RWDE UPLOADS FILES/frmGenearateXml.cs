using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using ComboBox = System.Windows.Forms.ComboBox;
using ScrollBar = System.Windows.Forms.ScrollBar;

namespace RWDE
{
    public partial class FrmGeneratorXml : Form
    {
        private readonly string connectionString;
        private readonly DbHelper dbHelper;
        private string GetCurrentFilePath([CallerFilePath] string filePath = "") => filePath;
        public FrmGeneratorXml()
        {
            InitializeComponent();
            dbHelper = new DbHelper();
            connectionString = dbHelper.GetConnectionString();
            LoadBatchStatus();
            PopulateDataGridView();
            this.ControlBox = false;
            this.WindowState = FormWindowState.Maximized;
            dataGridView.CellFormatting += dataGridView_CellFormatting;
            dataGridView.DataBindingComplete += DataGridView_DataBindingComplete;
            //Handle BatchType Values
            List<string> batchTypes = dbHelper.GetAllBatchTypesHcc();
            dtpStartDate.Value = DateTime.Now.AddYears(-1);//
            dtpStartDate.CustomFormat = "MM-dd-yyyy";
            dtpEndDate.CustomFormat = "MM-dd-yyyy";
            // Assuming you have another DateTimePicker for the End Date
            dtpEndDate.Value = DateTime.Now;
            cbBatchType.Items.Clear();
            RegisterEvents(this);
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
        private void Control_MouseHover(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }
        private void Control_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }
        private void dataGridView_Scroll(object sender, ScrollEventArgs e)
        {
            Cursor = Cursors.Hand;
        }
        private void RegisterEvents(Control parent)
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
                    RegisterEvents(control);
                }
            }
        }
        private void LoadBatchStatus()//load data
        {
            PopulateDataGridViewstartedstatus();
        }
        private void PopulateDataGridViewstartedstatus()//populate data
        {
            try
            {
                string query = "Generationstarted";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);

                    dataGridView.AutoGenerateColumns = false;
                    dataGridView.Columns.Clear();

                    dataGridView.Columns.Add("BatchID", "Batch ID");
                    dataGridView.Columns["BatchID"].DataPropertyName = "BatchID";
                    dataGridView.Columns.Add("Type", "Batch Type");
                    dataGridView.Columns["Type"].DataPropertyName = "Type";
                    dataGridView.Columns.Add("Description", "Batch Description");
                    dataGridView.Columns["Description"].DataPropertyName = "Description";
                    dataGridView.Columns.Add("FileName", "File Name");
                    dataGridView.Columns["FileName"].DataPropertyName = "FileName";
                    dataGridView.Columns.Add("ConversionStartedAt", "Conversion Started At");
                    dataGridView.Columns["ConversionStartedAt"].DataPropertyName = "ConversionStartedAt";
                    dataGridView.Columns.Add("ConversionEndedAt", "Conversion Ended At");
                    dataGridView.Columns["ConversionEndedAt"].DataPropertyName = "ConversionEndedAt";
                    dataGridView.Columns.Add("GenerationStartedAt", "Generation Started At");
                    dataGridView.Columns["GenerationStartedAt"].DataPropertyName = "GenerationStartedAt";
                    dataGridView.Columns.Add("GenerationEndedAt", "Generation Ended At");
                    dataGridView.Columns["GenerationEndedAt"].DataPropertyName = "GenerationEndedAt";
                    dataGridView.Columns.Add("Status", "Status");
                    dataGridView.Columns["Status"].DataPropertyName = "Status";
                    dataGridView.DataSource = dataTable;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)//Incresing of Width of Grid
        {
            try
            {
                // Check if the "BatchID" column exists before setting its width
                if (dataGridView.Columns.Contains("BatchID"))
                {
                    dataGridView.Columns["BatchID"].Width = 205; // Set the width to 200 pixels
                }
                if (dataGridView.Columns.Contains("Type"))
                {
                    dataGridView.Columns["Type"].Width = 205; // Set the width to 200 pixels
                }
                if (dataGridView.Columns.Contains("Type"))
                {
                    dataGridView.Columns["Type"].Width = 205; // Set the width to 200 pixels
                }
                if (dataGridView.Columns.Contains("Description"))
                {
                    dataGridView.Columns["Description"].Width = 205; // Set the width to 200 pixels
                }
                if (dataGridView.Columns.Contains("FileName"))
                {
                    dataGridView.Columns["FileName"].Width = 205; // Set the width to 200 pixels
                }
                if (dataGridView.Columns.Contains("ConversionStartedAt"))
                {
                    dataGridView.Columns["ConversionStartedAt"].Width = 250; // Set the width to 200 pixels
                }
                if (dataGridView.Columns.Contains("ConversionEndedAt"))
                {
                    dataGridView.Columns["ConversionEndedAt"].Width = 250; // Set the width to 200 pixels
                }
                if (dataGridView.Columns.Contains("GenerationStartedAt"))
                {
                    dataGridView.Columns["GenerationStartedAt"].Width = 250; // Set the width to 200 pixels
                }
                if (dataGridView.Columns.Contains("GenerationEndedAt"))
                {
                    dataGridView.Columns["GenerationEndedAt"].Width = 250; // Set the width to 200 pixels
                }

                if (dataGridView.Columns.Contains("Status"))
                {
                    dataGridView.Columns["Status"].Width = 205; // Set the width to 200 pixels
                }
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrames().FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame != null ? frame.GetFileLineNumber() : 0;
                dbHelper.LogError(ex.Message, GetCurrentFilePath(), ex.StackTrace, nameof(DataGridView_DataBindingComplete), Constants.ServiceCttohcc, lineNumber);
            }
        }
        public Panel PanelToReplace
        {
            get
            {
                return panel1;
            }
        }
        // private const string RemovedBatchIDsFilePath = "removedBatchIDs.txt";
        public void PopulateDataGridView(DataTable dt)//Populating GRID from table
        {
            try
            {
                string query = "Generationochin";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);

                    dataGridView.AutoGenerateColumns = false;
                    dataGridView.Columns.Clear();

                    dataGridView.Columns.Add("BatchID", "Batch ID");
                    dataGridView.Columns["BatchID"].DataPropertyName = "BatchID";
                    dataGridView.Columns.Add("Type", "Batch Type");
                    dataGridView.Columns["Type"].DataPropertyName = "Type";
                    dataGridView.Columns.Add("Description", "Batch Description");
                    dataGridView.Columns["Description"].DataPropertyName = "Description";
                    dataGridView.Columns.Add("FileName", "File Name");
                    dataGridView.Columns["FileName"].DataPropertyName = "FileName";
                    AddDateTime("ConversionStartedAt", "Conversion Started At", dataGridView);
                    AddDateTime("ConversionEndedAt", "Conversion Ended At", dataGridView);
                    AddDateTime("GenerationStartedAt", "Generation Started At", dataGridView);
                    AddDateTime("GenerationEndedAt", "Generation Ended At", dataGridView);
                    dataGridView.Columns.Add("Status", "Status");
                    dataGridView.Columns["Status"].DataPropertyName = "Status";
                    dataGridView.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrames().FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame != null ? frame.GetFileLineNumber() : 0;
                dbHelper.LogError(ex.Message, GetCurrentFilePath(), ex.StackTrace, nameof(PopulateDataGridView), Constants.ServiceCttohcc, lineNumber);
            }
        }
        private async void btnGeneration_Click(object sender, EventArgs e)//handle the Generation Process
        {
            try
            {
                if (dataGridView.SelectedRows.Count == 0)
                {
                    MessageBox.Show(Constants.PleaseselectarowwithaBatchIDtoproceed, Constants.Ochintorwdeconversion, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method early if no row is selected
                }
                int selectedRowIndex = dataGridView.SelectedRows[0].Index;
                int selectedBatchId = Convert.ToInt32(dataGridView.Rows[selectedRowIndex].Cells["BatchID"].Value.ToString());
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
                //Calling Client Xml Method
             
                string fileName = dataGridView.Rows[selectedRowIndex].Cells["fileName"].Value.ToString();

                if (fileName.Contains("Client"))
                {
                    await GenerateXmlForClientsAsync(selectedBatchId);//generate xml clients
                }

                if (fileName.Contains("Service"))
                {
                    await GenerateXmlForServicesAsync(selectedBatchId);//generate xml services

                }
                btnClose.Text =Constants.Close;
                btnGeneration.Enabled = true;
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrames().FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame != null ? frame.GetFileLineNumber() : 0;
                dbHelper.LogError(ex.Message, GetCurrentFilePath(), ex.StackTrace, nameof(btnGeneration_Click), Constants.ServiceCttohcc, lineNumber);
            }
        }
        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)//Description of Status values
        {
            try
            {
                if (dataGridView.Columns[e.ColumnIndex].Name == "Status")
                {
                    string statusValue = e.Value?.ToString();
                    if (!string.IsNullOrEmpty(statusValue))
                    {
                        string valueSelectQuery = "listvaluexml";

                        using (SqlConnection sql = new SqlConnection(connectionString))
                        {
                            using (SqlCommand com = new SqlCommand(valueSelectQuery, sql))
                            {
                                com.CommandType = CommandType.StoredProcedure;
                                com.Parameters.AddWithValue("@ListsID", statusValue);
                                sql.Open();
                                var result = com.ExecuteScalar();
                                sql.Close();

                                if (result != null)
                                {
                                    e.Value = result.ToString();
                                    e.FormattingApplied = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //private int selectedBatchID = 0;
        public async Task GenerateXmlForServicesAsync(int batchId)//Handle the Service Xml File Process
        {
            try
            {
                //progressClient.Value = 0;
                var batchDetails = await dbHelper.GetBatchDetailsFromSpAgenearationservices(batchId);//to check whether the generation completed or not

                if (batchDetails == null)
                {
                    Console.WriteLine(Constants.BatchNotfound);
                    return;
                }

                // Check if ConversionStartedAt and ConversionEndedAt are not null
                if (batchDetails.GenerationStartedAt != null && batchDetails.GenerationEndedAt != null)
                {
                    MessageBox.Show(Constants.Generationhasalreadybeencompletedforthisbatch, Constants.GenerateXml, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtProgressBar.Text = "0%";
                    txtClient.Text = "0%";
                    txtBatchid.Text = null;
                    txtUploadStarted.Text = null;//
                    txtUploadEnded.Text = null;
                    txtTotaltime.Text = null;
                    return;
                }
                btnClose.Text = "Abort";
                txtBatchid.Text = batchId.ToString();
                DateTime startTime = DateTime.Now;
                string time = startTime.ToString("MM/dd/yyyy HH:mm:ss");
                txtUploadStarted.Text = time;
                txtUploadEnded.Text = null;
                txtTotaltime.Text = null;

                int selectedRowIndex = dataGridView.SelectedRows[0].Index;
                int selectedBatchId = Convert.ToInt32(dataGridView.Rows[selectedRowIndex].Cells["BatchID"].Value.ToString());

                bool batchExists = false;
                DateTime? generationStartedAt = null;
                DateTime? generationEndedAt = null;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("updatexmlBATCH", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BatchID", selectedBatchId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                generationStartedAt = reader.IsDBNull(0) ? null : (DateTime?)reader.GetDateTime(0);
                                generationEndedAt = reader.IsDBNull(1) ? null : (DateTime?)reader.GetDateTime(1);
                                batchExists = true;
                            }
                        }
                    }
                }
                // Update the DataGridView
                int totalRows = GetTotalRowsForBatch(selectedBatchId);
                List<Dictionary<string, string>> data;
                if (chkError.Checked)
                {
                    // Update status of service and fetch error data
                    data = dbHelper.GetServiceserror(selectedBatchId);//
                    //calling XmlStructure Function
                    List<Dictionary<string, string>> xmlStructure = dbHelper.GetXmlStructure();//generate xml

                    string baseFilename = Constants.ServiceCttohcc;
                    dbHelper.Log(Constants.GeneratetoHcCforbatchIdStarted, Constants.ClientTrack, baseFilename, Constants.Uploadct);

                    //calling Service xml file generation Method
                    XElement xml = await GenerateXmlService(data, xmlStructure);//generate xml
                    string folderPath = txtPath.Text;
                    Directory.CreateDirectory(folderPath); // Create folder if it doesn't exist
                    string baseFileName = $"ServiceDetails_0246_0422_{DateTime.Now.ToString("ddMMyyyy")}_143100.xml";
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
                    string eTime = endTime.ToString("MM/dd/yyyy HH:mm:ss");
                    double totalSeconds = totalTime.TotalSeconds;
                    txtUploadEnded.Text = eTime;
                    txtTotaltime.Text = $"{totalSeconds:F2} Seconds";
                    btnClose.Text = Constants.Close;
                    UpdateStatusColumnServices(selectedBatchId, Constants.Hccxmlstatusf, startTime, endTime);
                    PopulateDataGridView(new DataTable());//populate data
                    dbHelper.Log(Constants.GeneratetoHcCformatcompletedsuccessfully, Constants.ClientTrack, baseFilename, Constants.Uploadct);
                }
                else
                {
                    // Update status of service and fetch standard service data
                    data = dbHelper.GetServices(selectedBatchId);

                    //calling XmlStructure Function
                    List<Dictionary<string, string>> xmlStructure = dbHelper.GetXmlStructure();//generate xml

                    string baseFilename = Constants.ServiceCttohcc;
                    dbHelper.Log(Constants.GeneratetoHcCforbatchIdStarted, Constants.ClientTrack, baseFilename, Constants.Uploadct);

                    //calling Service xml file generation Method
                    XElement xml = await GenerateXmlService(data, xmlStructure);//generate xml
                                                                                
                    string folderPath = txtPath.Text;
                    Directory.CreateDirectory(folderPath); // Create folder if it doesn't exist
                    string baseFileName = $"ServiceDetails_0246_0422_{DateTime.Now.ToString("ddMMyyyy")}_143100.xml";
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
                    string eTime = endTime.ToString("MM/dd/yyyy HH:mm:ss");
                    double totalSeconds = totalTime.TotalSeconds;
                    txtUploadEnded.Text = eTime;
                    txtTotaltime.Text = $"{totalSeconds:F2} Seconds";
                    btnClose.Text = Constants.Close;
                    UpdateStatusColumnServices(selectedBatchId, Constants.Hccxmlstatusf, startTime, endTime);
                    PopulateDataGridView(new DataTable());//populate data
                    dbHelper.Log(Constants.GeneratetoHcCformatcompletedsuccessfully, Constants.ClientTrack, baseFilename, Constants.Uploadct);
                }
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrames().FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame != null ? frame.GetFileLineNumber() : 0;
                dbHelper.LogError(ex.Message, GetCurrentFilePath(), ex.StackTrace, nameof(GenerateXmlForServicesAsync), Constants.ServiceCttohcc, lineNumber);
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task<XElement> GenerateXmlService(List<Dictionary<string, string>> data, List<Dictionary<string, string>> xmlStructure)
        {
            try
            {
                string rootTag = XmlConstants.TagNumberFive;
                XElement root = CreateRootElement(xmlStructure, rootTag);

                XElement sourceSystemElement = CreateSourceSystemElement(xmlStructure, XmlConstants.TagNumberTen);

                AddDynamicElements(sourceSystemElement, xmlStructure, rootTag);

                string contractServiceTagName = GetContractServiceTagName(xmlStructure, XmlConstants.ContractServicesTagNumber);
                string contractServicesTitle = GetContractServicesTitle(xmlStructure, XmlConstants.ContractServicesTagNumber);
                int totalRows = data.Count;
                int processedRows = 0;

                foreach (var dataRow in data)
                {
                    processedRows++;
                    await Updateprogress(processedRows, totalRows);//progress of lines inserted

                    XElement contractServicesElement = new XElement(contractServiceTagName);

                    if (!string.IsNullOrEmpty(contractServicesTitle))
                    {
                        contractServicesElement.SetAttributeValue(XmlConstants.ContractServicesTitleIdentifier, contractServicesTitle);
                    }

                    foreach (var structureRow in xmlStructure.Where(x => int.Parse(x[XmlConstants.TagNumber]) > 30 && int.Parse(x[XmlConstants.TagNumber]) <= 110))
                    {
                        (string tagNumber, string tagName, string fieldName, string presetValue, string defaultValue) = GetXmlStructureValues(structureRow);

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

                    await Updateprogress(processedRows, totalRows);//progress of lines inserted
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
                int lineNumber = frame != null ? frame.GetFileLineNumber() : 0;
                dbHelper.LogError(ex.Message, GetCurrentFilePath(), ex.StackTrace, nameof(GenerateXmlService), Constants.ServiceCttohcc, lineNumber);
                throw;
            }
        }
        private async Task<XElement> GenerateXmlClient(List<Dictionary<string, string>> data, List<Dictionary<string, string>> xmlStructure, int batchId)//Client Xml Generation
        {
            try
            {
                // Create the root element
                string rootTag = XmlConstants.TagNumberFive;
                XElement root = CreateRootElement(xmlStructure, rootTag);

                // Create the source system element
                XElement sourceSystemElement = CreateSourceSystemElement(xmlStructure, XmlConstants.TagNumberTen);
                AddDynamicElements(sourceSystemElement, xmlStructure, rootTag);

                // Get the tag name and title for client profile
                string contractServiceTagName = GetContractServiceTagName(xmlStructure, XmlConstants.ContractServicesTagNumber);
                string contractServicesTitle = GetContractServicesTitle(xmlStructure, XmlConstants.ContractServicesTagNumber);

                int totalRows = data.Count;
                int processedRows = 0;
                await Updateprogressclient(processedRows, totalRows);
                progressClient.Minimum = 0;
                progressClient.Maximum = totalRows;

                var tasks = data.Select(async (dataRow, index) =>
                {
                    processedRows++;
                    await Updateprogressclient(processedRows, totalRows);
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
                        string value = null;

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
                                        string clientDemoTagName = clientDemoRow[XmlConstants.Tag].Split(' ')[0]; currentDemoTitle = clientDemoRow[XmlConstants.Tag].Contains(XmlConstants.ContractServicesTitleIdentifier) ? clientDemoRow[XmlConstants.Tag].Split('=')[1].Trim(' ', '"') : null;
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
                                    HandleRaceValues(dataRow, batchId, xmlStructure, currentDemoElement, childElement);
                                }
                                else if (parsedTagNumber >= 605 && parsedTagNumber <= 1775)
                                {
                                    //Handle the MedicalValues
                                    HandleMedicalValues(parsedTagNumber, dataRow, batchId, xmlStructure, clientProfileElement, ref medical, ref medicalChild, ref medicalSubChild, tagName, fieldName, presetValue, defaultValue, empty, hasChild);
                                }
                                else if (parsedTagNumber > 1775 && parsedTagNumber <= 1925)
                                {
                                    //Handle the living situation Values
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

                //Making values as Array totalRows to synchronous the ececution
                }).ToArray();
                await Task.WhenAll(tasks);

                root.Add(sourceSystemElement);
                return root;
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrames().FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame != null ? frame.GetFileLineNumber() : 0;
                dbHelper.LogError(ex.Message, GetCurrentFilePath(), ex.StackTrace, nameof(GenerateXmlClient), Constants.ServiceCttohcc, lineNumber);
                throw;
            }
        }
        //  method to handle race values
        private void HandleRaceValues(Dictionary<string, string> dataRow, int batchId, List<Dictionary<string, string>> xmlStructure, XElement currentDemoElement, XElement childElement)
        {
            try
            {
                string clientId = dataRow.ContainsKey(XmlConstants.Clntid) ? dataRow[XmlConstants.Clntid] : null;
                if (!string.IsNullOrEmpty(clientId))
                {
                    // Retrieve the Rece values using FetchSubClientFromRace Method
                    List<Dictionary<string, string>> subClientData = dbHelper.FetchSubClientValuesFromRace(clientId, batchId);
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
                int lineNumber = frame != null ? frame.GetFileLineNumber() : 0;
                dbHelper.LogError(ex.Message, GetCurrentFilePath(), ex.StackTrace, nameof(HandleRaceValues), Constants.ServiceCttohcc, lineNumber);
            }
        }
        //  method to handle medical values
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
                        string currentDemo = clientDemoRow[XmlConstants.Tag].Contains(XmlConstants.ContractServicesTitleIdentifier) ? clientDemoRow[XmlConstants.Tag].Split('=')[1].Trim(' ', '"') : null;
                        medicalChild = new XElement(clientDemoTag);

                        if (!string.IsNullOrEmpty(currentDemo))
                        {
                            medicalChild.SetAttributeValue(XmlConstants.ContractServicesTitleIdentifier, currentDemo);
                        }
                    }
                }
                if (parsedTagNumber == 615)
                {
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
                    string clientId = dataRow.ContainsKey(XmlConstants.Clntid) ? dataRow[XmlConstants.Clntid] : null;
                    //This method to handle Medical child values
                    HandleMedicalChildValues(parsedTagNumber, clientId, batchId, xmlStructure, medicalChild, medicalSubChild, dataRow);
                }
                else if (!empty && !hasChild && !(parsedTagNumber >= 1655 && parsedTagNumber <= 1660) && !(parsedTagNumber >= 1510 && parsedTagNumber <= 1540) && !(parsedTagNumber > 710 && parsedTagNumber <= 810) && !(parsedTagNumber > 825 && parsedTagNumber <= 850) && !(parsedTagNumber > 940 && parsedTagNumber <= 960) && !(parsedTagNumber > 1010 && parsedTagNumber <= 1050))
                {
                    //This method to handle Medical Values
                    AddXmlElement(dataRow, fieldName, presetValue, defaultValue, tagName, medicalSubChild);
                }
                else if ((parsedTagNumber >= 1510 && parsedTagNumber <= 1540) || (parsedTagNumber >= 1655 && parsedTagNumber <= 1660))
                {
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
                int lineNumber = frame != null ? frame.GetFileLineNumber() : 0;
                dbHelper.LogError(ex.Message, GetCurrentFilePath(), ex.StackTrace, nameof(HandleMedicalValues), Constants.ServiceCttohcc, lineNumber);
            }
        }
        // method to handle living values
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
                        string currentDemo = clientDemoRow[XmlConstants.Tag].Contains(XmlConstants.ContractServicesTitleIdentifier) ? clientDemoRow[XmlConstants.Tag].Split('=')[1].Trim(' ', '"') : null;

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
                var frame = st.GetFrames().FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame != null ? frame.GetFileLineNumber() : 0;
                dbHelper.LogError(ex.Message, GetCurrentFilePath(), ex.StackTrace, nameof(HandleLivingValues), Constants.ServiceCttohcc, lineNumber);
            }
        }
        //method to handle Medical child values
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
                        fetchMethod = dbHelper.FetchSubClientValuesFromMedC4;
                        min = 710;
                        max = 735;
                        break;
                    case 825:
                        fetchMethod = dbHelper.FetchSubClientValuesFromMedVl;
                        min = 825;
                        max = 850;
                        break;
                    case 940:
                        fetchMethod = dbHelper.FetchSubClientValuesFromHivTest;
                        min = 940;
                        max = 960;
                        break;
                    case 1010:
                        fetchMethod = dbHelper.FetchSubClientValuesFromInsur;
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
                                //string value = Convert.ToInt32(subDataRow.ContainsKey(subFieldName) && !string.IsNullOrEmpty(subDataRow[subFieldName]) ? subDataRow[subFieldName] : !string.IsNullOrEmpty(subPresetValue) ? subPresetValue : subDefaultValue).ToString();

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
                int lineNumber = frame != null ? frame.GetFileLineNumber() : 0;
                dbHelper.LogError(ex.Message, GetCurrentFilePath(), ex.StackTrace, nameof(HandleMedicalChildValues), Constants.ServiceCttohcc, lineNumber);
            }

        }
        //method to handle the Adding Element
        private void AddXmlElement(Dictionary<string, string> dataRow, string fieldName, string presetValue, string defaultValue, string tagName, XElement parentElement)
        {
            try { 
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
                int lineNumber = frame != null ? frame.GetFileLineNumber() : 0;
                dbHelper.LogError(ex.Message, GetCurrentFilePath(), ex.StackTrace, nameof(HandleRootInformationSection), Constants.ServiceCttohcc, lineNumber);
            }
        }
        public XElement CreateRootElement(List<Dictionary<string, string>> xmlStructure, string rootTag) //create the rootElemt of client and serives xml file
        {
            try
            {
                string tag = xmlStructure.FirstOrDefault(x => x[XmlConstants.TagNumber] == rootTag)?[XmlConstants.Tag];
                return new XElement(tag);
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrames().FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame != null ? frame.GetFileLineNumber() : 0;
                dbHelper.LogError(ex.Message, GetCurrentFilePath(), ex.StackTrace, nameof(CreateRootElement), Constants.ServiceCttohcc, lineNumber);
                throw;
            }
        }
        public XElement CreateSourceSystemElement(List<Dictionary<string, string>> xmlStructure, string tagNumber) //Create a Source system for client and services xml file
        {
            try
            {
                string sourceSystemTag = xmlStructure.FirstOrDefault(x => x[XmlConstants.TagNumber] == tagNumber)?[XmlConstants.Tag];
                return new XElement(sourceSystemTag);
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrames().FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame != null ? frame.GetFileLineNumber() : 0;
                dbHelper.LogError(ex.Message, GetCurrentFilePath(), ex.StackTrace, nameof(CreateSourceSystemElement), Constants.ServiceCttohcc, lineNumber);
                throw;
            }
        }
        public void AddDynamicElements(XElement parentElement, List<Dictionary<string, string>> xmlStructure, string rootTag) //Adding the Source System Element for Client and Service Xml File
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
                var frame = st.GetFrames().FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame != null ? frame.GetFileLineNumber() : 0;
                dbHelper.LogError(ex.Message, GetCurrentFilePath(), ex.StackTrace, nameof(AddDynamicElements), Constants.ServiceCttohcc, lineNumber);
            }
        }
        public static string GetContractServiceTagName(List<Dictionary<string, string>> xmlStructure, string tagNumber) //Adding the Client Profile Tag for Client File
        {
            try
            {
                string contractServicesTagName = xmlStructure.FirstOrDefault(x => x[XmlConstants.TagNumber] == tagNumber)?[XmlConstants.Tag].Split(' ')[0];
                return contractServicesTagName;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static string GetContractServicesTitle(List<Dictionary<string, string>> xmlStructure, string tagNumber) //Adding the Client Profile Title Attribute For Client file
        {
            try
            {
                var contractServicesRow = xmlStructure.FirstOrDefault(x => x[XmlConstants.TagNumber] == tagNumber && x[XmlConstants.Tag].StartsWith(GetContractServiceTagName(xmlStructure, tagNumber)));
                return contractServicesRow[XmlConstants.Tag].Contains(XmlConstants.ContractServicesTitleIdentifier) ? contractServicesRow[XmlConstants.Tag].Split('=')[1].Trim(' ', '"') : null;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public static (string tagNumber, string tagName, string fieldName, string presetValue, string defaultValue) GetXmlStructureValues(Dictionary<string, string> structureRow)// Return the Structure Values
        {
            string tagNumber = structureRow[XmlConstants.TagNumber];
            string tagName = structureRow[XmlConstants.Tag];
            string fieldName = structureRow[XmlConstants.Field];
            string presetValue = structureRow[XmlConstants.PresetValue];
            string defaultValue = structureRow[XmlConstants.Default];

            return (tagNumber, tagName, fieldName, presetValue, defaultValue);
        }
        private async Task Updateprogress(int insertedRows, int totalRows)// Handle the Progress Values
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
        private void PopulateDataGridView()//Handle the Grid View 
        {
            try
            {
                string query = "Generation";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);

                    dataGridView.AutoGenerateColumns = false;
                    dataGridView.Columns.Clear();

                    dataGridView.Columns.Add("BatchID", "Batch ID");
                    dataGridView.Columns["BatchID"].DataPropertyName = "BatchID";
                    dataGridView.Columns.Add("Type", "Batch Type");
                    dataGridView.Columns["Type"].DataPropertyName = "Type";
                    dataGridView.Columns.Add("Description", "Batch Description");
                    dataGridView.Columns["Description"].DataPropertyName = "Description";
                    dataGridView.Columns.Add("FileName", "File Name");
                    dataGridView.Columns["FileName"].DataPropertyName = "FileName";
                    AddDateTime("ConversionStartedAt", "Conversion Started At", dataGridView);
                    AddDateTime("ConversionEndedAt", "Conversion Ended At", dataGridView);
                    AddDateTime("GenerationStartedAt", "Generation Started At", dataGridView);
                    AddDateTime("GenerationEndedAt", "Generation Ended At", dataGridView);
                    dataGridView.Columns.Add("Status", "Status");
                    dataGridView.Columns["Status"].DataPropertyName = "Status";
                    dataGridView.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AddDateTime(string name, string value, DataGridView dataGridView)//Adding the Date Formate to the Cloumn
        {
            try { 
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn
                {
                    Name = name,
                    DataPropertyName = name,
                    HeaderText = value,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "MM-dd-yyyy HH:mm:ss" }
                };
                dataGridView.Columns.Add(column);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void UpdateStatusColumn(int batchId, int listId, DateTime startTime, DateTime endTime)//update status
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.Cells["BatchID"].Value.ToString() == batchId.ToString())
                    {
                        row.Cells["Status"].Value = listId; // Store the ListID as the status value

                        if (listId == 20)
                        {
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                connection.Open();

                                // Define the SQL query to update the GenerationStartedAt column
                                string updateQuery = "updatexmlCLIENT";

                                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                                {
                                    command.CommandType = CommandType.StoredProcedure;
                                    // Set the parameters
                                    command.Parameters.AddWithValue("@BatchID", batchId);
                                    command.Parameters.AddWithValue("@GenerationStartedAt", startTime);

                                    command.Parameters.AddWithValue("@GenerationEndedAt", endTime);

                                    // Execute the SQL update command
                                    command.ExecuteNonQuery();
                                }
                            }
                        }
                        else
                        {
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                connection.Open();

                                // Define the SQL query to update the GenerationEndedAt column
                                string updateQuery = @"UPDATE [RWDE].[dbo].[Batch] 
                                           SET [GenerationEndedAt] = @GenerationEndedAt, [Status] = '21' 
                                           WHERE [BatchID] = @BatchID AND [FileName] LIKE '%Client%'";

                                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                                {
                                    // Set the parameters
                                    command.Parameters.AddWithValue("@BatchID", batchId);
                                    command.Parameters.AddWithValue("@GenerationEndedAt", endTime);

                                    // Execute the SQL update command
                                    command.ExecuteNonQuery();
                                }
                            }
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
        private void UpdateStatusColumnServices(int batchId, int listId, DateTime startTime, DateTime endTime)//update services status
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.Cells["BatchID"].Value.ToString() == batchId.ToString())
                    {
                        row.Cells["Status"].Value = listId; // Store the ListID as the status value

                        if (listId == 20)
                        {
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                connection.Open();

                                // Define the SQL query to update the GenerationStartedAt column"
                                string updateQuery ="updatexml";

                                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                                {
                                    command.CommandType = CommandType.StoredProcedure;

                                    // Set the parameters
                                    command.Parameters.AddWithValue("@BatchID", batchId);
                                    command.Parameters.AddWithValue("@GenerationStartedAt", startTime);

                                    command.Parameters.AddWithValue("@GenerationEndedAt", endTime);

                                    // Execute the SQL update command
                                    command.ExecuteNonQuery();
                                }
                            }
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
        public FrmGeneratorXml(string message, int displayDuration)//Automation process
        {
            // Set up form properties
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowInTaskbar = false;
            this.Size = new Size(500, 200);
            this.Text = "";
            this.BackColor = Color.White;

            // Create and configure message label
            Label label = new Label();
            label.Text = message;
            label.AutoSize = false;
            label.Size = new Size(this.ClientSize.Width - 10, this.ClientSize.Height - 10); // Adjust size for padding
            label.Location = new Point(5, 5); // Adjust location for padding
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Font = new Font("Arial", 12, FontStyle.Regular); // Set font and size

            // Add label to form
            this.Controls.Add(label);

            // Set up timer to close the form after displayDuration milliseconds
            Timer timer = new Timer();
            timer.Interval = displayDuration;
            timer.Tick += (sender, e) => this.Close();
            timer.Start();
        }
        public async Task GenerateXmlForClientsAsync(int batchId)//Clients xml generation
        {
            try
            {
               // progressServices.Value = 0;
                 var batchDetails = await dbHelper.GetBatchDetailsFromSpAgenearationlients(batchId);//to check whether the generation completed or not

                if (batchDetails == null)
                {
                    Console.WriteLine(Constants.BatchNotfound);
                    return;
                }

                // Check if ConversionStartedAt and ConversionEndedAt are not null
                if (batchDetails.GenerationStartedAt != null && batchDetails.GenerationEndedAt != null)
                {
                    MessageBox.Show(Constants.Generationhasalreadybeencompletedforthisbatch,Constants.GenerateXml,MessageBoxButtons.OK,MessageBoxIcon.Information);
                    txtProgressBar.Text = "0%";
                    txtClient.Text= "0%";
                    txtBatchid.Text = null;
                    txtUploadStarted.Text = null;
                    txtUploadEnded.Text = null;
                    txtTotaltime.Text = null;
                    return;
                }
                btnClose.Text = "Abort";
                txtUploadEnded.Text = null;
                txtTotaltime.Text = null;
                int selectedRowIndex = dataGridView.SelectedRows[0].Index;
                int selectedBatchId = Convert.ToInt32(dataGridView.Rows[selectedRowIndex].Cells["BatchID"].Value.ToString());

                int totalRows = GetTotalRowsForBatchclient(batchId);
                DateTime startTime = DateTime.Now;

                txtBatchid.Text = batchId.ToString();
                string time = startTime.ToString("MM/dd/yyyy HH:mm:ss");
                txtUploadStarted.Text = time;

                bool batchExists = false;
                DateTime? generationStartedAt = null;
                DateTime? generationEndedAt = null;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT GenerationStartedAt, GenerationEndedAt FROM Batch WHERE BatchID = @BatchID", connection))
                    {
                        command.Parameters.AddWithValue("@BatchID", selectedBatchId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                generationStartedAt = reader.IsDBNull(0) ? null : (DateTime?)reader.GetDateTime(0);
                                generationEndedAt = reader.IsDBNull(1) ? null : (DateTime?)reader.GetDateTime(1);
                                batchExists = true;
                            }
                        }
                    }
                }

                //calling Store Procedure Function
                List<Dictionary<string, string>> data = dbHelper.GetClients(selectedBatchId);

                //calling XmlStructure Function
                List<Dictionary<string, string>> xmlStructure = dbHelper.GetClientFileXmlStructure();

                //calling Service xml file generation Method               
                XElement xml = await GenerateXmlClient(data, xmlStructure, selectedBatchId);

                // Save the XML to a file with automatic numbering if the file exists
                string folderPath = txtPath.Text;
                Directory.CreateDirectory(folderPath); // Create folder if it doesn't exist

                // Initial filename with the current date in the desired format (ddMMyyyy)
                string baseFileName = $"ClientDetails_0246_0689_{DateTime.Now.ToString("ddMMyyyy")}_143100.xml";
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
                string eTime = endedTime.ToString("MM/dd/yyyy HH:mm:ss");
                double totalSeconds = totalTime.TotalSeconds;
                txtUploadEnded.Text = eTime;
                txtTotaltime.Text = $"{totalSeconds:F2} Seconds";
                DateTime endtime = DateTime.Now;
                UpdateStatusColumn(selectedBatchId, Constants.Hccxmlstatusf, startTime, endtime);
                PopulateDataGridView();
                //await GenerateXMLForServicesAsync(selectedBatchID);
            }
            // Save XML 
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task Updateprogressclient(int insertedRows, int totalRows)//Progress of rows Insertion
        {
            try { 
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
        private int GetTotalRowsForBatch(int selectedBatchId)//Getting total rows from particular table
        {
            int totalRows = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("countxml", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BatchID", selectedBatchId);
                        totalRows = (int)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting total rows: " + ex.Message);
            }

            return totalRows;
        }
        private int GetTotalRowsForBatchclient(int selectedBatchId)//Getting total rows from particular table
        {
            int totalRows = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("countxmlservices", connection))
                    {
                        command.CommandType= CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@BatchID", selectedBatchId);
                        totalRows = (int)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting total rows: " + ex.Message);
            }

            return totalRows;
        }
        private void btnClose_Click(object sender, EventArgs e)//close main form
        {
            try
            {
                if (btnClose.Text == "Close")
                {
                    this.Close();
                    Application.Restart();
                    return;
                }
                // int batchId = dbHelper.GetNextBatchID();
                int selectedRowIndex = dataGridView.SelectedRows[0].Index;
                int batchId = Convert.ToInt32(dataGridView.Rows[selectedRowIndex].Cells["BatchID"].Value.ToString());
                String filename = Convert.ToString(dataGridView.Rows[selectedRowIndex].Cells["FileName"].Value.ToString());
                if (btnClose.Text == Constants.Abort)
                {
                    // Prompt the user with a confirmation message
                    DialogResult result = MessageBox.Show("Are you sure you want to abort?", Constants.GenerateXml,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow row in dataGridView.Rows)
                        {
                            row.Cells["Status"].Value = Constants.Xmlabort;
                            break;
                        } // Abort after updating the first row

                        string selectedFolderPath = txtPath.Text;

                        // Update the status of the selected batch to Status "19" (Abort)
                        UpdateBatchStatusabort(batchId, Constants.Xmlabort, selectedFolderPath, filename);

                        //Show a message box indicating successful abort
                        MessageBox.Show(Constants.Abortedsuccessfully, Constants.GenerateXml);
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
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateBatchStatusabort(int batchId, int status, string xmlDirectoryPath,String filename)//for abort status 
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Construct the SQL UPDATE statement
                    string query = "countxmlrows";

                    // Create and execute the SqlCommand
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        // Add parameters to the command
                        command.Parameters.AddWithValue("@Status", status);
                        command.Parameters.AddWithValue("@BatchID", batchId);
                        command.Parameters.AddWithValue("@Filename", filename);

                        // Execute the update query
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if any rows were affected (optional)
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine(Constants.Batchstatusupdatedsuccessfully);
                        }
                        else
                        {
                            Console.WriteLine(Constants.NobatchwasfoundwiththegivenId);
                        }
                    }
                }
                // Delete XML files in the specified directory
                DeleteXmlFiles(xmlDirectoryPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(Constants.Errorupdatingbatchstatus, ex.Message);
                // Log or handle the exception appropriately
            }
        }
        private void DeleteXmlFiles(string directoryPath)//Deleting the xml after abort
        {
            try
            {
                if (Directory.Exists(directoryPath))
                {
                    // Get all XML files in the directory
                    string[] xmlFiles = Directory.GetFiles(directoryPath, "*.xml");

                    // Delete each XML file
                    foreach (string file in xmlFiles)
                    {
                        File.Delete(file);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting XML files: {ex.Message}");
                // Log or handle the exception appropriately
                throw; // Re-throw if you want to handle it in the calling method
            }
        }
        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)//to select particular row
        {
            try { 
                if (e.RowIndex >= 0)
                {
                    int  selectedBatchId = Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells["BatchID"].Value);
                    Console.WriteLine("Selected BatchID: " + selectedBatchId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnBrowse_Click(object sender, EventArgs e)//selecting path to save xml file
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
        private void btnSubmit_Click(object sender, EventArgs e)//Handle the filtered datas
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
                dataGridView.DataSource = result;

                if (result == null || result.Rows.Count == 0)
                {
                    MessageBox.Show(Constants.NoFilterDatasHcc, Constants.FilterTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                // Log the exception or display an error message
                MessageBox.Show($"An error occurred: {ex.Message}", Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void bnClear_Click(object sender, EventArgs e)//Handle to clear the filter datas
        {
            try
            {
                cbBatchType.Items.Clear();
                PopulateDataGridView();
                dtpEndDate.CustomFormat = " ";
                dtpEndDate.Format = DateTimePickerFormat.Custom;
                // Do the same for dtpStartDate or any other DateTimePicker if needed
                dtpStartDate.CustomFormat = " ";
                dtpStartDate.Format = DateTimePickerFormat.Custom;
                List<string> batchTypes = dbHelper.GetAllBatchTypesHcc();
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
        private void dtpStartDate_ValueChanged(object sender, EventArgs e)//format date
        {
            dtpStartDate.CustomFormat = "MM-dd-yyyy";
            dtpStartDate.Format = DateTimePickerFormat.Custom;
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)//format date
        {
            try { 
                dtpEndDate.CustomFormat = "MM-dd-yyyy";
                dtpEndDate.Format = DateTimePickerFormat.Custom;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)//navigate to next page
        {
            try
            {
                FrmMain mainForm = Application.OpenForms.OfType<FrmMain>().FirstOrDefault();

                if (mainForm != null)
                {
                    mainForm.ShowOchinToHccScreenMain();
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
    }
    //Client Profile Tag Numbers
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
    //Client Profile Chile Tag numbers
    public enum ChildTagNumbers
    {
        Tag270 = 270,
        Tag330 = 330,
        Tag550 = 550
    }
    //Medical Chile Tag Numbers
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
    //Medical Sub Child Tag Numbers
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
    //Medical MedicalSubChildDyamic
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
    //public living_situation Child Tag Numbers
    public enum LiveSubChild
    {
        Tag1790 = 1790,
        Tag1825 = 1825,
        Tag1870 = 1870,
        Tag1900 = 1900,
    }

}




