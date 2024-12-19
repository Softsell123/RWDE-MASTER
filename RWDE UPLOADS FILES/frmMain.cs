using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace RWDE
{
    public partial class frmMain : Form
    {
        private readonly string connectionString;
        private readonly DBHelper dbHelper;
        public frmMain()//initialize data
        {
            InitializeComponent();
           
            dbHelper = new DBHelper();
            connectionString = dbHelper.GetConnectionString();
            btnHccCsv.Visible = false;
            this.uploadCSVToOCHINToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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

        private bool IsAnotherControlActive()//to control the files
        {
            // Check if there are any open dropdowns or specific active controls
            foreach (Control control in this.Controls)
            {
                if (control is ComboBox comboBox && comboBox.DroppedDown)
                {
                    return true; // Another dropdown is open
                }

                // Add more checks if needed for other controls
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
                string FileName = openFileDialog.FileName;
            }
        }
         private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)//to extract from folder
        {
            
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }
        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)//to close
        {
            this.Close();
        }
        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)//to open the prescribed form
        {
            ContractIDLists contractIDLists = new ContractIDLists();
            contractIDLists.MdiParent = this;
            contractIDLists.Show();
        }
        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)//This method is triggered when the user clicks the "Cascade" option from a ToolStripMenuItem.
        {
            LayoutMdi(MdiLayout.Cascade);
        } 
        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)// This method is triggered when the user clicks the "Cascade" option from a ToolStripMenuItem.
        {
            LayoutMdi(MdiLayout.TileVertical);
        }
       private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)// This method is triggered when the user clicks the "Cascade" option from a ToolStripMenuItem.
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }
        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)// This method is triggered when the user clicks the "Cascade" option from a ToolStripMenuItem.
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }
       private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)// This method is triggered when the user clicks the "Cascade" option from a ToolStripMenuItem.
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }
        private Panel panelFrom;
        private Panel pnlForm;
        private void btnGenerator_Click(object sender, EventArgs e)//xml generation process 
        {

            if (this.ActiveMdiChild != null)
            {
                this.ActiveMdiChild.Close();
            }

            frmXMLGenerator frmXMLGenerator = new frmXMLGenerator();
            frmXMLGenerator.MdiParent = this;
            frmXMLGenerator.Show();
        }
        private void btnOCHINHCCConversion_Click(object sender, EventArgs e)//OCHIN to RWDE insertion
        {
            OCHIN_to_RWDE_Conversion OCHIN_to_RWDE_Conversion=new OCHIN_to_RWDE_Conversion();
            OCHIN_to_RWDE_Conversion.MdiParent=this;
            OCHIN_to_RWDE_Conversion.Show();
        }

        private Form currentForm; // Declare the current form variable at class level
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

                frmConvertToHCC frmConvertToHCC = new frmConvertToHCC();
                frmConvertToHCC.MdiParent = this;

                frmConvertToHCC.Show();


                // Add the panel from frmConvertToHCC to frmMain if it's not already added
                if (panelFrom == null)
                {
                    panelFrom = frmConvertToHCC.PanelToReplace;
                    this.Controls.Add(panelFrom);
                }

                // Store the current form
                currentForm = frmConvertToHCC;

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
      private void btnHccCsv_Click_1(object sender, EventArgs e)//Csv uploads
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
                btnCT btnCT = new btnCT();
                btnCT.MdiParent = this;
                btnCT.Show();
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
                frmConvertToHCC frmConvertToHCC = new frmConvertToHCC();
                frmConvertToHCC.Visible = false;

                // Create an instance of frmUploadCsv and show it
                frmUploadCsv frmUploadCsv = new frmUploadCsv();
                frmUploadCsv.MdiParent = this;
                frmUploadCsv.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void ShowOCHINToHCCScreenHCC()//function to navigate to next page
        {
            try { 
          
            OCHIN_to_RWDE_Conversion OCHIN_to_RWDE_Conversion = new OCHIN_to_RWDE_Conversion();
            // Show the OCHIN to HCC screen (assuming 'ochinToHCCControl' is a user control or panel for the OCHIN to HCC screen)
            OCHIN_to_RWDE_Conversion.MdiParent = this;
            OCHIN_to_RWDE_Conversion.Show(); // Make the OCHIN to HCC control visible
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void ShowOCHINToHCCScreen()//function to navigate to next page       
            {
            try { 
           
            frmHccCsv frmHccCsv = new frmHccCsv();
            frmHccCsv.MdiParent = this;
            frmHccCsv.Show(); // Make the OCHIN to HCC control visible

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void ShowOCHINToHCCScreenGENERATE()////function to navigate to next page
            {
            try { 
            // Hide the tool strip menu item or any other controls you want to hide
            //uploadHCCCSVToolStripMenuItem.Visible = false; // Adjust as necessary
            frmXMLGenerator frmXMLGenerator = new frmXMLGenerator();
            // Show the OCHIN to HCC screen (assuming 'ochinToHCCControl' is a user control or panel for the OCHIN to HCC screen)
            frmXMLGenerator.MdiParent = this;
            frmXMLGenerator.Show(); // Make the OCHIN to HCC control visible

                // Optionally, you might want to hide other controls or panels if necessary
                // Hide other controls or panels
                // otherControlOrPanel.Visible = false; // Hide other controls/panels if necessary
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void ShowOCHINToHCCScreenMain()//function to navigate to next page
            {

            Application.Restart(); // Make the OCHIN to HCC control visible
        }
       private Image ResizeImage(Image image, int width, int height)
        {

            Bitmap resizedImage = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.DrawImage(image, 0, 0, width, height);
            }
            return resizedImage;
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
            frmUploadCsv frmUploadCsv = new frmUploadCsv();
            frmUploadCsv.MdiParent = this;
            frmUploadCsv.Show();
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

            frmConvertToHCC frmConvertToHCC = new frmConvertToHCC();
            frmConvertToHCC.MdiParent = this;
            frmConvertToHCC.Show();
           // try
           // { 
           //     if (panelFrom != null)
           // {
           //     panelFrom.Visible = false;


           // }
           // if (pnlForm != null)
           // {
           //     pnlForm.Visible = false;


           // }

           // frmConvertToHCC frmConvertToHCC = new frmConvertToHCC();
           // frmConvertToHCC.MdiParent = this;

           // frmConvertToHCC.Show();


           // // Add the panel from frmConvertToHCC to frmMain if it's not already added
           // if (panelFrom == null)
           // {
           //     panelFrom = frmConvertToHCC.PanelToReplace;
           //     this.Controls.Add(panelFrom);
           // }
           //// Store the current form
           // currentForm = frmConvertToHCC;

           // // Fetch data from the Batch table
           //string query = "SELECT [BatchID], [FileName], [Description], [Path], [UploadStartedAt],[UploadEndedAt],[ConversionStartedAt],[ConversionEndedAt],[GenerationStartedAt]  ,[GenerationEndedAt] , [TotalRows], [SuccessfulRows], [FailedRows], [Status], [Message], [Comments], [CreatedBy], [CreatedOn] FROM [RWDE].[dbo].[Batch]";

           // using (SqlConnection connection = new SqlConnection(connectionString))
           // {
           //     SqlCommand command = new SqlCommand(query, connection);
           //     SqlDataAdapter adapter = new SqlDataAdapter(command);
           //     DataTable dataTable = new DataTable();

           //     adapter.Fill(dataTable);

           //     // Bind the DataTable to the DataGridView in frmConvertToHCC
           //    // frmConvertToHCC.PopulateDataGridView(dataTable);
           // }
           // }
           // catch (Exception ex)
           // {
           //     MessageBox.Show(ex.Message);
           // }
        }
        private void generateHCCXmlFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
            {
                this.ActiveMdiChild.Close();
            }

            frmXMLGenerator frmXMLGenerator = new frmXMLGenerator();
            frmXMLGenerator.MdiParent = this;
            frmXMLGenerator.Show();

            //try
            //{ // Create an instance of frmUploadCsv and show it
            //frmXMLGenerator frmXMLGenerate = new frmXMLGenerator();
            //frmXMLGenerate.MdiParent = this;

            //frmXMLGenerate.Show();


            //// Add the panel from frmConvertToHCC to frmMain if it's not already added
            //if (panelFrom == null)
            //{
            //    panelFrom = frmXMLGenerate.PanelToReplace;
            //    this.Controls.Add(panelFrom);
            //}

            //string query = "SELECT [BatchID], [FileName], [Description], [Path], [UploadStartedAt], [UploadEndedAt], [TotalRows], [SuccessfulRows], [FailedRows], [Status], [Message], [Comments], [CreatedBy], [CreatedOn] FROM [RWDE].[dbo].[Batch]";

            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    SqlCommand command = new SqlCommand(query, connection);
            //    SqlDataAdapter adapter = new SqlDataAdapter(command);
            //    DataTable dataTable = new DataTable();

            //    adapter.Fill(dataTable);

            //    // Bind the DataTable to the DataGridView in frmConvertToHCC
            //    frmXMLGenerate.PopulateDataGridView(dataTable);
            //}
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void allBatchesToolStripMenuItem_Click(object sender, EventArgs e)//all batches updates
        {
            try { 
            ViewAllBatchesForm ViewAllBatchesForm = new ViewAllBatchesForm();
            ViewAllBatchesForm.MdiParent = this;
            ViewAllBatchesForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnochincCsv_Click(object sender, EventArgs e)//ochin csv uploads
        {
            try { 
            frmHccCsv frmHcccsv= new frmHccCsv();
           frmHcccsv.MdiParent=this;
            frmHcccsv.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void uploadHCCCSVToolStripMenuItem_Click(object sender, EventArgs e)//hcc csv uploads
        {
            if (this.ActiveMdiChild != null)
            {
                this.ActiveMdiChild.Close();
            }

            frmHccCsv frmHcccsv = new frmHccCsv();
            frmHcccsv.MdiParent = this;
            frmHcccsv.Show();
        }
        private void xMLFileUploadsToolStripMenuItem_Click(object sender, EventArgs e)//xml file insertion
        {
            try { 
            btnCT btnCT= new btnCT();
            btnCT.MdiParent=this;   
            btnCT.Show();
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

            OCHIN_to_RWDE_Conversion OCHIN_to_RWDE_Conversion = new OCHIN_to_RWDE_Conversion();
            OCHIN_to_RWDE_Conversion.MdiParent = this;
            OCHIN_to_RWDE_Conversion.Show();
            //try { 
            //// Hide any existing panels/forms
            //if (panelFrom != null)
            //{
            //    panelFrom.Visible = false;
            //}

            //if (pnlForm != null)
            //{
            //    pnlForm.Visible = false;
            //}

            //// Create a new instance of the conversion form
            //OCHIN_to_RWDE_Conversion ochinConversionForm = new OCHIN_to_RWDE_Conversion
            //{
            //    MdiParent = this
            //};

            //// Show the form and focus
            //ochinConversionForm.Show();
            //ochinConversionForm.Focus();  // Ensure the form gets focus

            //// Add the panel from OCHIN_to_RWDE_Conversion if it's not already added
            //if (panelFrom == null)
            //{
            //    panelFrom = ochinConversionForm.PanelToReplace;
            //    if (!this.Controls.Contains(panelFrom))
            //    {
            //        this.Controls.Add(panelFrom);
            //    }
            //}


            //// Store the current form reference
            //currentForm = ochinConversionForm;

            //// Fetch data from the Batch table
            //string query = "SELECT [BatchID], [FileName], [Description], [Path], [UploadStartedAt], [UploadEndedAt], [ConversionStartedAt], [ConversionEndedAt], [GenerationStartedAt], [GenerationEndedAt], [TotalRows], [SuccessfulRows], [FailedRows], [Status], [Message], [Comments], [CreatedBy], [CreatedOn] FROM [RWDE].[dbo].[Batch]";

            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    SqlCommand command = new SqlCommand(query, connection);
            //    SqlDataAdapter adapter = new SqlDataAdapter(command);
            //    DataTable dataTable = new DataTable();

            //    // Open connection and fetch the data
            //    connection.Open();
            //    adapter.Fill(dataTable);

            //    // Populate the DataGridView on the form
            //    ochinConversionForm.PopulateDataGridView(dataTable);
            //}
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void serviceReconciliationReportDotNotUseToolStripMenuItem_Click(object sender, EventArgs e)
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
        private void contractIDListDotNotUseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { 
            ContractIDLists contractIDLists = new ContractIDLists();
            contractIDLists.MdiParent = this;
            contractIDLists.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void serviceIDListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { 
            ServiceCodeSetup serviceIDLists = new ServiceCodeSetup();
            serviceIDLists.MdiParent = this;
            serviceIDLists.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            Deceased_Clients clients = new Deceased_Clients();
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
            Upload_dashboard Upload_dashboard = new Upload_dashboard();
            Upload_dashboard.MdiParent = this;
            Upload_dashboard.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void hCCRECONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { 
            HCC_Reconciliation HCC_Reconciliation = new HCC_Reconciliation();
            HCC_Reconciliation.MdiParent = this;
            HCC_Reconciliation.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clientDemographicsReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { 
            Client_Demographics_Report Client_Demographics_Report = new Client_Demographics_Report();
            Client_Demographics_Report.MdiParent = this;
            Client_Demographics_Report.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void errorLogReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { 
            Error_Log_Report Error_Log_Report = new Error_Log_Report();
            Error_Log_Report.MdiParent = this;
            Error_Log_Report.Show();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolsMenu_Click(object sender, EventArgs e)
        {

        }

        private void errorReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
            {
                this.ActiveMdiChild.Close();
            }

            LOAD Error_Report =new LOAD();
            Error_Report.MdiParent = this;
            Error_Report.Show();

        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void downloadHCCErrorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
            {
                this.ActiveMdiChild.Close();
            }

            LOAD Error_Report = new LOAD();
            Error_Report.MdiParent = this;
            Error_Report.Show();
        }

        private void cSVFILESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CsvFile_Conversion CsvFile_Conversion = new CsvFile_Conversion();
            CsvFile_Conversion.MdiParent = this;
            CsvFile_Conversion.Show();
        }

        private void manualUploadReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmManualUpload frmManualUpload = new frmManualUpload();
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
          
