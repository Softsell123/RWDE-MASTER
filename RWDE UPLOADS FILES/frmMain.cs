using System;
using System.Data; 
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace RWDE
{
    public partial class FrmMain : Form
    {
        // private readonly string connectionString;
        private DbHelper dbHelper;
        private Panel panelFrom;
        private readonly Panel pnlForm;
        public FrmMain()// initialize data
        {
            this.pnlForm = null;
            InitializeComponent();

            dbHelper = new DbHelper();
            btnHccCsv.Visible = false;
            uploadCSVToOCHINToolStripMenuItem = new ToolStripMenuItem();
            BackColor = Color.White;
            // Load += MainForm_Load;
            WindowState = FormWindowState.Maximized;
            btnConversion.Visible = false;
            IsMdiContainer = true;
            // to set StartDate as First date of the Current Month
            loadStratDate();
           
            RegisterEvents(this); // Assigning events to all Controls

        }

        public sealed override Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        private void ShowNewForm(object sender, EventArgs e)// to show new form
        {
            try
            {
                // Check if any other dropdown or specific control is focused/active
                if (IsAnotherControlActive())
                {
                    // Prevent the new form from showing
                    return;
                }
                pnlLeftImage.Visible = false;
                pnlRightChart.Visible = false;
                ServiceReconciliationReport service = new ServiceReconciliationReport
                {
                    MdiParent = this
                };
                service.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
        private bool IsAnotherControlActive()// to control the files
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
        private void OpenFile(object sender, EventArgs e)// to extract the files
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    openFileDialog.Filter = Constants.AllExtention;
                    
                    if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        string fileName = openFileDialog.FileName;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)// to extract from folder
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    saveFileDialog.Filter = Constants.AllExtention;

                    if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        string fileName = saveFileDialog.FileName;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)// to close
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
        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)// to open the prescribed form
        {
            try
            {
                pnlLeftImage.Visible = false;
                pnlRightChart.Visible = false;

                ContractIdLists contractIdLists = new ContractIdLists
                {
                    MdiParent = this
                };
                contractIdLists.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnGenerator_Click(object sender, EventArgs e)// xml generation process 
        {
            try
            {
                if (ActiveMdiChild != null)
                {
                    ActiveMdiChild.Close();
                }
                pnlLeftImage.Visible = false;
                pnlRightChart.Visible = false;

                FrmGeneratorXml frmGeneratorXml = new FrmGeneratorXml
                {
                    MdiParent = this
                };
                frmGeneratorXml.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnOCHINHCCConversion_Click(object sender, EventArgs e)// OCHIN to RWDE insertion
        {
            try
            {
                pnlLeftImage.Visible = false;
                pnlRightChart.Visible = false;
                OchinToRwdeConversion ochinToRwdeConversion = new OchinToRwdeConversion
                {
                    MdiParent = this
                };
                ochinToRwdeConversion.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnHccConversion_Click(object sender, EventArgs e)// CT to HCC Conversion
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
                pnlLeftImage.Visible = false;
                pnlRightChart.Visible = false;
                FrmConvertToHcc frmConvertToHcc = new FrmConvertToHcc
                {
                    MdiParent = this
                };
                frmConvertToHcc.Show();

                // Add the panel from frmConvertToHCC to frmMain if it's not already added
                if (panelFrom == null)
                {
                    panelFrom = frmConvertToHcc.PanelToReplace;
                    Controls.Add(panelFrom);
                }
                // Fetch data from the Batch table
                string query = Constants.BatchTableQuery;
                DataTable dataTable = dbHelper.FillTheGridQuery(query);// to fill the Gird using Query
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnHccCsv_Click(object sender, EventArgs e)// Csv uploads
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
                pnlLeftImage.Visible = false;
                pnlRightChart.Visible = false;

                // Create an instance of btnCT and show it
                FrmUploadHccCsv frmUploadXmlFile = new FrmUploadHccCsv
                {
                    MdiParent = this
                };
                frmUploadXmlFile.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnXml_Click(object sender, EventArgs e)// Xml uploads
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
                pnlLeftImage.Visible = false;
                pnlRightChart.Visible = false;
                FrmConvertToHcc frmConvertToHcc = new FrmConvertToHcc
                {
                    Visible = false
                };

                // Create an instance of frmUploadCsv and show it
                FrmUploadHccCsv frmUploadHccCsv = new FrmUploadHccCsv
                {
                    MdiParent = this
                };
                frmUploadHccCsv.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void ShowOchinToHccScreenHcc()// function to navigate to next page
        {
            try
            {
                pnlLeftImage.Visible = false;
                pnlRightChart.Visible = false;
                OchinToRwdeConversion ochinToRwdeConversion = new OchinToRwdeConversion
                {
                    // Show the OCHIN to HCC screen (assuming 'ochinToHCCControl' is a user control or panel for the OCHIN to HCC screen)
                    MdiParent = this
                };
                ochinToRwdeConversion.Show(); // Make the OCHIN to HCC control visible
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void ShowOchinToHccScreen()// function to navigate to next page       
        {
            try
            {
                pnlLeftImage.Visible = false;
                pnlRightChart.Visible = false;

                FrmUploadOchinCsv frmUploadOchinCsv = new FrmUploadOchinCsv
                {
                    MdiParent = this
                };
                frmUploadOchinCsv.Show(); // Make the OCHIN to HCC control visible
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void ShowOchinToHccScreenGenerate()// // function to navigate to next page
        {
            try
            {
                pnlLeftImage.Visible = false;
                pnlRightChart.Visible = false;

                // Hide the tool strip menu item or any other controls you want to hide
                FrmGeneratorXml frmGeneratorXml = new FrmGeneratorXml
                {
                    // Show the OCHIN to HCC screen (assuming 'ochinToHCCControl' is a user control or panel for the OCHIN to HCC screen)
                    MdiParent = this
                };
                frmGeneratorXml.Show(); // Make the OCHIN to HCC control visible
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void ShowOchinToHccScreenMain()// function to navigate to next page
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
        private void uploadCSVToOCHINToolStripMenuItem_Click(object sender, EventArgs e)// csv uploads 
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
                pnlLeftImage.Visible = false;
                pnlRightChart.Visible = false;

                // Create an instance of frmUploadCsv and show it
                FrmUploadHccCsv frmUploadHccCsv = new FrmUploadHccCsv
                {
                    MdiParent = this
                };
                frmUploadHccCsv.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void oCHINToHCCConversionToolStripMenuItem_Click(object sender, EventArgs e)// ochin to hcc conversion
        {
            try
            {
                if (ActiveMdiChild != null)
                {
                    ActiveMdiChild.Close();
                }
                pnlLeftImage.Visible = false;
                pnlRightChart.Visible = false;

                FrmConvertToHcc frmConvertToHcc = new FrmConvertToHcc
                {
                    MdiParent = this
                };
                frmConvertToHcc.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void generateHCCXmlFilesToolStripMenuItem_Click(object sender, EventArgs e)// Xml Generator
        {
            try
            {
                if (ActiveMdiChild != null)
                {
                    ActiveMdiChild.Close();
                }
                pnlLeftImage.Visible = false;
                pnlRightChart.Visible = false;

                FrmGeneratorXml frmGeneratorXml = new FrmGeneratorXml
                {
                    MdiParent = this
                };
                frmGeneratorXml.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void allBatchesToolStripMenuItem_Click(object sender, EventArgs e)// all batches updates
        {
            try
            {
                pnlLeftImage.Visible = false;
                pnlRightChart.Visible = false;

                ViewAllBatchesForm viewAllBatchesForm = new ViewAllBatchesForm
                {
                    MdiParent = this
                };
                viewAllBatchesForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BtnochincCsv_Click(object sender, EventArgs e)// ochin csv uploads
        {
            try
            {
                pnlLeftImage.Visible = false;
                pnlRightChart.Visible = false;

                FrmUploadOchinCsv uploadOchinCsv = new FrmUploadOchinCsv
                {
                    MdiParent = this
                };
                uploadOchinCsv.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void uploadOchinCSVToolStripMenuItem_Click(object sender, EventArgs e)// hcc csv uploads
        {
            try
            {
                if (ActiveMdiChild != null)
                {
                    ActiveMdiChild.Close();
                }
                pnlLeftImage.Visible = false;
                pnlRightChart.Visible = false;
                FrmUploadOchinCsv uploadOchinCsv = new FrmUploadOchinCsv
                {
                    MdiParent = this
                };
                uploadOchinCsv.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void xMLFileUploadsToolStripMenuItem_Click(object sender, EventArgs e)// xml file insertion
        {
            try
            {
                pnlLeftImage.Visible = false;
                pnlRightChart.Visible = false;

                FrmUploadXmlFile frmUploadXmlFile = new FrmUploadXmlFile
                {
                    MdiParent = this
                };
                frmUploadXmlFile.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void oCHINToolStripMenuItem_Click(object sender, EventArgs e)// Ochin to RWDE
        {
            try
            {
                if (ActiveMdiChild != null)
                {
                    ActiveMdiChild.Close();
                }
                pnlLeftImage.Visible = false;
                pnlRightChart.Visible = false;

                OchinToRwdeConversion ochinToRwdeConversion = new OchinToRwdeConversion
                {
                    MdiParent = this
                };
                ochinToRwdeConversion.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void serviceReconciliationReportDotNotUseToolStripMenuItem1_Click(object sender, EventArgs e)// to display ServiceRecon Report
        {
            try
            {
                pnlLeftImage.Visible = false;
                pnlRightChart.Visible = false;

                ServiceReconciliationReport service = new ServiceReconciliationReport
                {
                    MdiParent = this
                };
                service.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void deceasedClientsReportToolStripMenuItem_Click(object sender, EventArgs e)// to display DeceasedClient Report
        {
            try
            {
                pnlLeftImage.Visible = false;
                pnlRightChart.Visible = false;

                DeceasedClients clients = new DeceasedClients
                {
                    MdiParent = this
                };
                clients.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void uploadDashboardToolStripMenuItem_Click(object sender, EventArgs e)// to display Monthly Report
        {
            try
            {
                pnlLeftImage.Visible = false;
                pnlRightChart.Visible = false;

                MonthlyReport monthlyReport = new MonthlyReport
                {
                    MdiParent = this
                };
                monthlyReport.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void hCCRECONToolStripMenuItem_Click(object sender, EventArgs e)// to display HCCRecon Report
        {
            try
            {
                pnlLeftImage.Visible = false;
                pnlRightChart.Visible = false;

                HccReconciliation hccReconciliation = new HccReconciliation
                {
                    MdiParent = this
                };
                hccReconciliation.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void clientDemographicsReportToolStripMenuItem_Click(object sender, EventArgs e)// to display ClientDemographics Report
        {
            try
            {
                pnlLeftImage.Visible = false;
                pnlRightChart.Visible = false;

                ClientDemographicsReport clientDemographicsReport = new ClientDemographicsReport
                {
                    MdiParent = this
                };
                clientDemographicsReport.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void errorLogReportToolStripMenuItem_Click(object sender, EventArgs e)// to display ErrorLog Report
        {
            try
            {
                pnlLeftImage.Visible = false;
                pnlRightChart.Visible = false;

                ErrorLogReport errorLogReport = new ErrorLogReport
                {
                    MdiParent = this
                };
                errorLogReport.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void downloadHCCErrorsToolStripMenuItem_Click(object sender, EventArgs e)// to display DownloadHcc Errors
        {
            try
            {
                if (ActiveMdiChild != null)
                {
                    ActiveMdiChild.Close();
                }
                pnlLeftImage.Visible = false;
                pnlRightChart.Visible = false;

                FrmDownloadHccErrors errorReport = new FrmDownloadHccErrors
                {
                    MdiParent = this
                };
                errorReport.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void cSVFILESToolStripMenuItem_Click(object sender, EventArgs e)// to display Csv Generation Screen
        {
            try
            {
                pnlLeftImage.Visible = false;
                pnlRightChart.Visible = false;

                CsvFileConversion csvFileConversion = new CsvFileConversion
                {
                    MdiParent = this
                };
                csvFileConversion.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void manualUploadReportToolStripMenuItem_Click(object sender, EventArgs e)// to display ManualUpload Report
        {
            try
            {
                pnlLeftImage.Visible = false;
                pnlRightChart.Visible = false;

                FrmManualUpload frmManualUpload = new FrmManualUpload
                {
                    MdiParent = this
                };
                frmManualUpload.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void loadStratDate() // to set StartDate as First date of the Current Month
        {
            try
            {
                DateTime Today = DateTime.Now;
                DateTime firstDayOfMonth = new DateTime(Today.Year, Today.Month, 1);
                dtpStartDate.Value = firstDayOfMonth;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private async void fillChartServices()// to fill the data in Pie Chart
        {
            try
            {
                if (!Validatedate())// to Validate the Selected Dates  
                {
                    return;
                }
                DbHelper dbHelper = new DbHelper();
                DateTime Today = DateTime.Now;
                DateTime StartDate = dtpStartDate.Value.Date;
                DateTime EndDate = dtpEndDate.Value.Date.AddDays(1).AddTicks(-1);

                // to get the data of the PieChart
                DataTable dt = dbHelper.GetPieChartData(StartDate, EndDate);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show(Constants.Nodatafoundbetweenselecteddates);
                    return;
                }

                chartServices.Series.Clear();

                // Add a new series to the chart for Pie chart
                var series = chartServices.Series.Add("Service Data");
                series.ChartType = SeriesChartType.Pie;

                foreach (DataRow row in dt.Rows)
                {
                    // Loop through the columns, starting from the third column (index 2)
                    for (int colIndex = 2; colIndex < dt.Columns.Count; colIndex++)
                    {
                        string columnName = dt.Columns[colIndex].ColumnName;
                        int targetValue = Convert.ToInt32(row[colIndex]);

                        if (columnName.Length > 25)  // Check if the column name is too long
                        {
                            // columnName = InsertLineBreaks(columnName);// to split the header
                        }
                        // Add an initial point with value 0
                        var pointIndex = series.Points.AddXY(columnName, 0); // Add the point and get its index
                        var point = series.Points[pointIndex];             // Retrieve the actual DataPoint object

                        // customizing the chart appearance (labels, colors,)
                        series.IsValueShownAsLabel = true;
                        series["PieLabelStyle"] = "Outside";
                        series["LabelDistance"] = "4";

                        await Task.Delay(300);
                        for (int value = 0; value <= targetValue; value++) // Gradual increments
                        {
                            point.YValues[0] =value;
                            chartServices.Refresh(); // Redraw the chart
                            if(targetValue-value<50) await Task.Delay(25);    // Control the animation speed
                            if (value+50 <targetValue) value += 49;
                        }
                        foreach (var points in series.Points)
                        {
                            points.ToolTip = $"{points.AxisLabel}: {points.YValues[0]}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // to insert line breaks into the header text
        private string InsertLineBreaks(string header)
        {
            try
            {
                int maxLength = 23;  // Set the maximum number of characters per line
                if (header.Length <= maxLength)
                    return header;

                // Split the header text into two lines at the space closest to the maxLength
                int breakPoint = header.LastIndexOf(' ', maxLength);
                if (breakPoint == -1) breakPoint = maxLength;  // No space found, split directly

                string firstLine = header.Substring(0, breakPoint);
                string secondLine = header.Substring(breakPoint).Trim();
                string thirdLine = "";
                if (secondLine.Length > maxLength)
                {
                    thirdLine = InsertLineBreaks(secondLine); // to insert line breaks into the header text
                }
                if (thirdLine.Length > 1)
                {
                    return firstLine + "\n" + thirdLine;
                }
                else
                {
                    return firstLine + "\n" + secondLine;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return header;
            }
        }
        private bool Validatedate()// to Validate the Selected Dates  
        {
            try
            {
                DateTime StartDate = dtpStartDate.Value.Date;
                DateTime EndDate = dtpEndDate.Value.Date;

                if (StartDate > DateTime.Now.Date)
                {
                    MessageBox.Show(Constants.SelectedFutureDate);
                    return false;
                }
                else if (EndDate > DateTime.Now.Date)
                {
                    MessageBox.Show(Constants.SelectedFutureDate);
                    return false;
                }
                else if (EndDate < StartDate)
                {
                    MessageBox.Show(Constants.FromDateMustBeEarlierThanToDate);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Validatedate())// to Validate the Selected Dates  
                {
                    fillChartServices();// to fill the data in Pie Chart
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Validatedate())// to Validate the Selected Dates  
                {
                    fillChartServices();// to fill the data in Pie Chart
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            if (this.Width < 1200 || this.Height < 800) // Adjust based on minimum size
            {
                this.AutoScroll = true;
                this.IsMdiContainer = true;
            }
            else
            {
                this.AutoScroll = false;
                this.IsMdiContainer = true;
            }
        }

        private void activityLoggerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                pnlLeftImage.Visible = false;
                pnlRightChart.Visible = false;

                frmActivityLogger frmActivityLogger = new frmActivityLogger
                {
                    MdiParent = this
                };
                frmActivityLogger.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}