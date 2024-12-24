using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace RWDE
{
    public partial class FrmMain : Form
    {
        private readonly string connectionString;
        private readonly DbHelper dbHelper;
        private Form currentForm; // Declare the current form variable at class level
        private Panel panelFrom;
        private Panel pnlForm;
        public FrmMain()//initialize data
        {
            InitializeComponent();
           
            dbHelper = new DbHelper();
            connectionString = dbHelper.GetConnectionString();
            btnHccCsv.Visible = false;
            this.uploadCSVToOCHINToolStripMenuItem = new ToolStripMenuItem();
            this.BackColor = Color.White;
            this.Load += MainForm_Load;
            this.WindowState = FormWindowState.Maximized;
            btnConversion.Visible = false;
            this.IsMdiContainer = true;
            RegisterEvents(this);
        }
        private void MainForm_Load(object sender, EventArgs e)//to load main form
        {
            this.BackColor = Color.White;
        }
        private void ShowNewForm(object sender, EventArgs e)//to show new form
        {
            // Check if any other dropdown or specific control is focused/active
            if (IsAnotherControlActive())
            {
                // Prevent the new form from showing
                return;
            }
            ServiceReconciliationReport service = new ServiceReconciliationReport();
            service.MdiParent = this;
            service.Show();
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
                if (control is Button || control is CheckBox || control is DateTimePicker)
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
        private bool IsAnotherControlActive()//to control the files
        {
            // Check if there are any open dropdowns or specific active controls
            foreach (Control control in this.Controls)
            {
                if (control is ComboBox comboBox && comboBox.DroppedDown)
                {
                    return true; // Another dropdown is open
                }
            }
            // Add more conditions if necessary to cover all cases
            return false;
        }
        private void OpenFile(object sender, EventArgs e)//to extract the files
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
            }
        }
         private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)//to extract from folder
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;
            }
        }
        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)//to close
        {
            this.Close();
        }
        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)//to open the prescribed form
        {
            ContractIdLists contractIdLists = new ContractIdLists();
            contractIdLists.MdiParent = this;
            contractIdLists.Show();
        }
        private void btnGenerator_Click(object sender, EventArgs e)//xml generation process 
        {
            if (this.ActiveMdiChild != null)
            {
                this.ActiveMdiChild.Close();
            }

            FrmGeneratorXml frmGeneratorXml = new FrmGeneratorXml();
            frmGeneratorXml.MdiParent = this;
            frmGeneratorXml.Show();
        }
        private void btnOCHINHCCConversion_Click(object sender, EventArgs e)//OCHIN to RWDE insertion
        {
            OchinToRwdeConversion ochinToRwdeConversion=new OchinToRwdeConversion();
            ochinToRwdeConversion.MdiParent=this;
            ochinToRwdeConversion.Show();
        }
        private void btnHccConversion_Click(object sender, EventArgs e)//CT to HCC Conversion
        {
            try
            {
                if (panelFrom != null)
                {
                    panelFrom.Visible = false;
                }
                if (pnlForm != null)
                {
                    pnlForm.Visible = false;
                }

                FrmConvertToHcc frmConvertToHcc = new FrmConvertToHcc();
                frmConvertToHcc.MdiParent = this;
                frmConvertToHcc.Show();

                // Add the panel from frmConvertToHCC to frmMain if it's not already added
                if (panelFrom == null)
                {
                    panelFrom = frmConvertToHcc.PanelToReplace;
                    this.Controls.Add(panelFrom);
                }

                // Store the current form
                currentForm = frmConvertToHcc;

                // Fetch data from the Batch table
                string query = "SELECT [BatchID], [FileName], [Description], [Path], [StartedAt], [EndedAt], [TotalRows], [SuccessfulRows], [FailedRows], [Status], [Message], [Comments], [CreatedBy], [CreatedOn] FROM [RWDE].[dbo].[Batch]";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);

                    // Bind the DataTable to the DataGridView in frmConvertToHCC
                    //frmConvertToHCC.PopulateDataGridView(dataTable);//The code fetches the grid and values from the frmConvertToHCC class
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnHccCsv_Click(object sender, EventArgs e)//Csv uploads
        {
            try
            {// Show the stored form (if any)

                // Hide panelFrom if it's visible
                if (panelFrom != null)
                {
                    panelFrom.Visible = false;
                }
                // Hide panelFrom if it's visible
                if (pnlForm != null)
                {
                    pnlForm.Visible = false;
                }

                // Create an instance of btnCT and show it
                FrmUploadOchinCsv frmUploadXMLFile = new FrmUploadOchinCsv();
                frmUploadXMLFile.MdiParent = this;
                frmUploadXMLFile.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnXml_Click(object sender, EventArgs e)//Xml uploads
        {
            // Show the stored form (if any)
            try
            {
                 // Hide panelFrom if it's visible
                if (panelFrom != null)
                {
                    panelFrom.Visible = false;
                }
                if (pnlForm != null)
                {
                    pnlForm.Visible = false;
                }
                FrmConvertToHcc frmConvertToHcc = new FrmConvertToHcc();
                frmConvertToHcc.Visible = false;

                // Create an instance of frmUploadCsv and show it
                FrmUploadHccCsv frmUploadHccCsv = new FrmUploadHccCsv();
                frmUploadHccCsv.MdiParent = this;
                frmUploadHccCsv.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void ShowOchinToHccScreenHcc()//function to navigate to next page
        {
            try { 
          
                OchinToRwdeConversion ochinToRwdeConversion = new OchinToRwdeConversion();
                // Show the OCHIN to HCC screen (assuming 'ochinToHCCControl' is a user control or panel for the OCHIN to HCC screen)
                ochinToRwdeConversion.MdiParent = this;
                ochinToRwdeConversion.Show(); // Make the OCHIN to HCC control visible
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void ShowOchinToHccScreen()//function to navigate to next page       
        {
            try 
            {
                FrmUploadOchinCsv frmUploadOchinCsv = new FrmUploadOchinCsv();
                frmUploadOchinCsv.MdiParent = this;
                frmUploadOchinCsv.Show(); // Make the OCHIN to HCC control visible
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void ShowOchinToHccScreenGenerate()////function to navigate to next page
        {
            try {
                // Hide the tool strip menu item or any other controls you want to hide
                FrmGeneratorXml frmGeneratorXml = new FrmGeneratorXml();
                // Show the OCHIN to HCC screen (assuming 'ochinToHCCControl' is a user control or panel for the OCHIN to HCC screen)
                frmGeneratorXml.MdiParent = this;
                frmGeneratorXml.Show(); // Make the OCHIN to HCC control visible
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void ShowOchinToHccScreenMain()//function to navigate to next page
        {
            Application.Restart(); // Make the OCHIN to HCC control visible
        }
        private void uploadCSVToOCHINToolStripMenuItem_Click(object sender, EventArgs e)//csv uploads 
        {
            try { 
                if (panelFrom != null)
                {
                    panelFrom.Visible = false;
                }
                if (pnlForm != null)
                {
                    pnlForm.Visible = false;
                }
                // Create an instance of frmUploadCsv and show it
                FrmUploadHccCsv frmUploadHccCsv = new FrmUploadHccCsv();
                frmUploadHccCsv.MdiParent = this;
                frmUploadHccCsv.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void oCHINToHCCConversionToolStripMenuItem_Click(object sender, EventArgs e)//ochin to hcc conversion
        {
            if (this.ActiveMdiChild != null)
            {
                this.ActiveMdiChild.Close();
            }
            FrmConvertToHcc frmConvertToHcc = new FrmConvertToHcc();
            frmConvertToHcc.MdiParent = this;
            frmConvertToHcc.Show();
        }
        private void generateHCCXmlFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
            {
                this.ActiveMdiChild.Close();
            }

            FrmGeneratorXml frmGeneratorXml = new FrmGeneratorXml();
            frmGeneratorXml.MdiParent = this;
            frmGeneratorXml.Show();
        }
        private void allBatchesToolStripMenuItem_Click(object sender, EventArgs e)//all batches updates
        {
            try { 
                ViewAllBatchesForm viewAllBatchesForm = new ViewAllBatchesForm();
                viewAllBatchesForm.MdiParent = this;
                viewAllBatchesForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BtnochincCsv_Click(object sender, EventArgs e)//ochin csv uploads
        {
            try {
                FrmUploadOchinCsv uploadOchinCsv = new FrmUploadOchinCsv();
                uploadOchinCsv.MdiParent=this;
                uploadOchinCsv.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void uploadOchinCSVToolStripMenuItem_Click(object sender, EventArgs e)//hcc csv uploads
        {
            if (this.ActiveMdiChild != null)
            {
                this.ActiveMdiChild.Close();
            }

            FrmUploadOchinCsv uploadOchinCsv = new FrmUploadOchinCsv();
            uploadOchinCsv.MdiParent = this;
            uploadOchinCsv.Show();
        }
        private void xMLFileUploadsToolStripMenuItem_Click(object sender, EventArgs e)//xml file insertion
        {
            try { 
                FrmUploadXmlFile frmUploadXmlFile = new FrmUploadXmlFile();
                frmUploadXmlFile.MdiParent=this;
                frmUploadXmlFile.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void oCHINToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
            {
                this.ActiveMdiChild.Close();
            }

            OchinToRwdeConversion ochinToRwdeConversion = new OchinToRwdeConversion();
            ochinToRwdeConversion.MdiParent = this;
            ochinToRwdeConversion.Show();
        }
        private void serviceReconciliationReportDotNotUseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try { 
                ServiceReconciliationReport service = new ServiceReconciliationReport();
                service.MdiParent = this;
                service.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void deceasedClientsReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { 
                DeceasedClients clients = new DeceasedClients();
                clients.MdiParent = this;
                clients.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void uploadDashboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try {
                MonthlyReport monthlyReport = new MonthlyReport();
                monthlyReport.MdiParent = this;
                monthlyReport.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void hCCRECONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { 
                HccReconciliation hccReconciliation = new HccReconciliation();
                hccReconciliation.MdiParent = this;
                hccReconciliation.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void clientDemographicsReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { 
                ClientDemographicsReport clientDemographicsReport = new ClientDemographicsReport();
                clientDemographicsReport.MdiParent = this;
                clientDemographicsReport.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void errorLogReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { 
                ErrorLogReport errorLogReport = new ErrorLogReport();
                errorLogReport.MdiParent = this;
                errorLogReport.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void downloadHCCErrorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
            {
                this.ActiveMdiChild.Close();
            }
            FrmDownloadHccErrors errorReport = new FrmDownloadHccErrors();
            errorReport.MdiParent = this;
            errorReport.Show();
        }
        private void cSVFILESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CsvFileConversion csvFileConversion = new CsvFileConversion();
            csvFileConversion.MdiParent = this;
            csvFileConversion.Show();
        }
        private void manualUploadReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmManualUpload frmManualUpload = new FrmManualUpload();
                frmManualUpload.MdiParent = this;
                frmManualUpload.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
          
