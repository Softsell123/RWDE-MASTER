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
        private Panel panelFrom;
        private readonly Panel pnlForm;
        public FrmMain()//initialize data
        {
            this.pnlForm = null;
            InitializeComponent();
           
            DbHelper dbHelper1 = new DbHelper();
            connectionString = dbHelper1.GetConnectionString();
            btnHccCsv.Visible = false;
            uploadCSVToOCHINToolStripMenuItem = new ToolStripMenuItem();
            BackColor = Color.White;
            Load += MainForm_Load;
            WindowState = FormWindowState.Maximized;
            btnConversion.Visible = false;
            IsMdiContainer = true;
            RegisterEvents(this); //Assigning events to all Controls
        }

        public sealed override Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        private void MainForm_Load(object sender, EventArgs e)//to load main form
        {
            try
            {
                BackColor = Color.White;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ShowNewForm(object sender, EventArgs e)//to show new form
        {
            try
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
                if (control is Button || control is CheckBox || control is DateTimePicker)
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
        private bool IsAnotherControlActive()//to control the files
        {
            try
            {
                // Check if there are any open dropdowns or specific active controls
                foreach (Control control in Controls)
            {
                if (control is ComboBox comboBox && comboBox.DroppedDown)
                {
                    return true; // Another dropdown is open
                }
            }
            // Add more conditions if necessary to cover all cases
            return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private void OpenFile(object sender, EventArgs e)//to extract the files
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = Constants.AllExtention;
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
         private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)//to extract from folder
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = Constants.AllExtention;
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)//to close
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)//to open the prescribed form
        {
            try
            {
                ContractIdLists contractIdLists = new ContractIdLists();
            contractIdLists.MdiParent = this;
            contractIdLists.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnGenerator_Click(object sender, EventArgs e)//xml generation process 
        {
            try
            {
                if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            FrmGeneratorXml frmGeneratorXml = new FrmGeneratorXml();
            frmGeneratorXml.MdiParent = this;
            frmGeneratorXml.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnOCHINHCCConversion_Click(object sender, EventArgs e)//OCHIN to RWDE insertion
        {
            try
            {
                OchinToRwdeConversion ochinToRwdeConversion=new OchinToRwdeConversion();
            ochinToRwdeConversion.MdiParent=this;
            ochinToRwdeConversion.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                    Controls.Add(panelFrom);
                }
                // Fetch data from the Batch table
                string query = Constants.BatchTableQuery;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);
                    // Bind the DataTable to the DataGridView in frmConvertToHCC
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
                FrmUploadOchinCsv frmUploadXmlFile = new FrmUploadOchinCsv();
                frmUploadXmlFile.MdiParent = this;
                frmUploadXmlFile.Show();
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
            try
            {
                Application.Restart(); // Make the OCHIN to HCC control visible
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            try
            {
                if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }
            FrmConvertToHcc frmConvertToHcc = new FrmConvertToHcc();
            frmConvertToHcc.MdiParent = this;
            frmConvertToHcc.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void generateHCCXmlFilesToolStripMenuItem_Click(object sender, EventArgs e)//Xml Generator
        {
            try
            {
                if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            FrmGeneratorXml frmGeneratorXml = new FrmGeneratorXml();
            frmGeneratorXml.MdiParent = this;
            frmGeneratorXml.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            try
            {
                if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }
            FrmUploadOchinCsv uploadOchinCsv = new FrmUploadOchinCsv();
            uploadOchinCsv.MdiParent = this;
            uploadOchinCsv.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
        private void oCHINToolStripMenuItem_Click(object sender, EventArgs e)//Ochin to RWDE
        {
            try
            {
                if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            OchinToRwdeConversion ochinToRwdeConversion = new OchinToRwdeConversion();
            ochinToRwdeConversion.MdiParent = this;
            ochinToRwdeConversion.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void serviceReconciliationReportDotNotUseToolStripMenuItem1_Click(object sender, EventArgs e)//to display ServiceRecon Report
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
        private void deceasedClientsReportToolStripMenuItem_Click(object sender, EventArgs e)//to display DeceasedClient Report
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
        private void uploadDashboardToolStripMenuItem_Click(object sender, EventArgs e)//to display Monthly Report
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
        
        private void hCCRECONToolStripMenuItem_Click(object sender, EventArgs e)//to display HCCRecon Report
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
        private void clientDemographicsReportToolStripMenuItem_Click(object sender, EventArgs e)//to display ClientDemographics Report
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
        private void errorLogReportToolStripMenuItem_Click(object sender, EventArgs e)//to display ErrorLog Report
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
        private void downloadHCCErrorsToolStripMenuItem_Click(object sender, EventArgs e)//to display DownloadHcc Errors
        {
            try
            {
                if (ActiveMdiChild != null)
                {
                    ActiveMdiChild.Close();
                }
                FrmDownloadHccErrors errorReport = new FrmDownloadHccErrors();
                errorReport.MdiParent = this;
                errorReport.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void cSVFILESToolStripMenuItem_Click(object sender, EventArgs e)//to display Csv Generation Screen
        {
            try
            {
                CsvFileConversion csvFileConversion = new CsvFileConversion();
            csvFileConversion.MdiParent = this;
            csvFileConversion.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void manualUploadReportToolStripMenuItem_Click(object sender, EventArgs e)//to display ManualUpload Report
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
          
