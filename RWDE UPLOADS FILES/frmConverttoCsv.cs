using System;
using System.IO;
using System.Windows.Forms;

namespace RWDE
{
    public partial class CsvFileConversion : Form
    {
        private readonly DbHelper dbHelper;

        public CsvFileConversion()// to initialize data
        {
            InitializeComponent();
            dbHelper = new DbHelper();

            ControlBox = false;
            WindowState = FormWindowState.Maximized;
            RegisterEvents(this); // Assigning events to all Controls

            if (File.Exists(Constants.LastFolderPathhcc))
            {
                string lastFolderPathhcc = File.ReadAllText(Constants.LastFolderPathhcc).Trim();  // Trim to remove any extra spaces or newlines
                // Check if the file content is not empty and the directory exists
                if (!string.IsNullOrEmpty(lastFolderPathhcc) && Directory.Exists(lastFolderPathhcc))
                {
                    txtPath.Text = lastFolderPathhcc;
                }
                else
                {
                    txtPath.Clear();  // Ensure the path is cleared if the file is empty or invalid
                }
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
        private void btnReport_Click(object sender, EventArgs e)// to generate and save the Csv files
        {
            string ClientfilePath = Path.Combine(txtPath.Text, $"{Constants.Clients}{DateTime.Now.ToString(Constants.YyyyMMdd)}{Constants.CsvExtention}"); // Ensure the full file path includes a filename
            string ServicesfilePath = Path.Combine(txtPath.Text, $"{Constants.ServiceSample}{DateTime.Now.ToString(Constants.YyyyMMdd)}{Constants.CsvExtention}"); // Ensure the full file path includes a filename

            try
            {
                if (!Directory.Exists(txtPath.Text))
                {
                    MessageBox.Show(Constants.Theselectedfolderdoesnotexist, Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // to Write the CSV data of Clients
                dbHelper.WriteClientCsvData(ClientfilePath);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }

                // to Write the CSV data of Services
                dbHelper.WriteServicesCsvData(ServicesfilePath);
                if (dbHelper.ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }

            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show(Constants.Accessdeniedtothefolder, Constants.PermissionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Constants.Errorsp + ex.Message);
            }
        }
        private void btnBrowse_Click(object sender, EventArgs e)// to select the folder to save the csv files
        {
            try
            {
                using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
                {
                    folderDialog.Description = Constants.Selectafoldertosavethefile;
                    folderDialog.ShowNewFolderButton = true;

                    if (folderDialog.ShowDialog() == DialogResult.OK)
                    {
                        string selectedPath = "";
                        try
                        {
                            selectedPath = folderDialog.SelectedPath;
                            txtPath.Text = selectedPath;

                            // Test writing permission by creating a temporary file
                            string testFilePath = Path.Combine(selectedPath, Constants.Testfiletxt);
                            File.WriteAllText(testFilePath, Constants.Testingpermissions);
                            File.Delete(testFilePath); // Clean up after test

                            MessageBox.Show(Constants.Selectedfolder + selectedPath);
                        }
                        catch (UnauthorizedAccessException ex)
                        {
                            MessageBox.Show(Constants.Accessdeniedtothefolder + ex.Message, Constants.PermissionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        // Save the folder path (you only need to save it once)
                        File.WriteAllText(Constants.LastFolderPathhcc, selectedPath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
