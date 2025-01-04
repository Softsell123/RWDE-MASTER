using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace RWDE
{
    public partial class CsvFileConversion : Form
    {
        private readonly string connectionString;
        private readonly DbHelper dbHelper;

        public CsvFileConversion()//to initialize data
        {
            InitializeComponent();
            dbHelper = new DbHelper();
            //get connection string
            connectionString = dbHelper.GetConnectionString();
            ControlBox = false;
            WindowState = FormWindowState.Maximized;
            RegisterEvents(this); //Assigning events to all Controls

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
        private void Control_MouseHover(object sender, EventArgs e)//Changing Cursor as Hand on hover
        {
            Cursor = Cursors.Hand;
        }
        private void Control_MouseLeave(object sender, EventArgs e)//Changing back default Cursor on Leave
        {
            Cursor = Cursors.Default;
        }
        private void RegisterEvents(Control parent)//Assigning events to all Controls
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
                    //Assigning events to child Controls
                    RegisterEvents(control);
                }
            }
        }
        private void btnReport_Click(object sender, EventArgs e)//to generate and save the Csv files
        {
            string filePath = Path.Combine(txtPath.Text, $"{Constants.Clients}{DateTime.Now.ToString(Constants.YyyyMMdd)}{Constants.CsvExtention}"); // Ensure the full file path includes a filename

            try
            {
                if (!Directory.Exists(txtPath.Text))
                {
                    MessageBox.Show(Constants.Theselectedfolderdoesnotexist, Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // SQL query to execute the stored procedure
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    //Getting BatchId for particular file to generate CSV
                    int batchid = dbHelper.GetMaxXmlBatchId();
                    // Call the stored procedure
                    using (SqlCommand cmd = new SqlCommand(Constants.Ctclientsmapping, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue(Constants.AtBatchid, batchid);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Create a StreamWriter to write to the CSV file
                            using (StreamWriter writer = new StreamWriter(filePath))

                            {
                                // Write the header (column names)
                                var columnNames = Enumerable.Range(0, reader.FieldCount)
                                                            .Select(reader.GetName)
                                                            .ToArray();
                                writer.WriteLine(string.Join("|", columnNames));  // Use pipe (|) as the separator

                                // Write each row
                                while (reader.Read())
                                {
                                    var rowValues = new string[reader.FieldCount];
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        rowValues[i] = reader[i]?.ToString();
                                    }
                                    writer.WriteLine(string.Join("|", rowValues));  // Use pipe (|) as the separator
                                }
                                //to get the services of the last uploaded Xml file
                                GetServicedataCsv(batchid);
                            }
                        }
                    }
                    MessageBox.Show(Constants.CsVfilehasbeencreatedsuccessfullyat + filePath);//
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
        public void GetServicedataCsv(int batchid)//to get the services of the last uploaded Xml file
        {
            string filePath = Path.Combine(txtPath.Text, $"{Constants.ServiceSample}{DateTime.Now.ToString(Constants.YyyyMMdd)}{Constants.CsvExtention}"); // Ensure the full file path includes a filename
            try
            {
                if (!Directory.Exists(txtPath.Text))
                {
                    MessageBox.Show(Constants.Theselectedfolderdoesnotexist, Constants.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // SQL query to execute the stored procedure
                using (SqlConnection conn = new SqlConnection(connectionString))//
                {
                    conn.Open();
                    // Call the stored procedure
                    using (SqlCommand cmd = new SqlCommand(Constants.GetCtServicesForCsv, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue(Constants.AtBatchid, batchid);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Create a StreamWriter to write to the CSV file
                            using (StreamWriter writer = new StreamWriter(filePath))
                            {
                                // Write the header (column names)
                                var columnNames = Enumerable.Range(0, reader.FieldCount)
                                                            .Select(reader.GetName)
                                                            .ToArray();
                                writer.WriteLine(string.Join("|", columnNames));  // Use pipe (|) as the separator

                                // Write each row
                                while (reader.Read())
                                {
                                    var rowValues = new string[reader.FieldCount];
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        rowValues[i] = reader[i]?.ToString();
                                    }
                                    writer.WriteLine(string.Join("|", rowValues));  // Use pipe (|) as the separator
                                }
                            }
                        }
                    }
                    MessageBox.Show(Constants.CsVfilehasbeencreatedsuccessfullyat + filePath);
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
        private void btnBrowse_Click(object sender, EventArgs e)//to select the folder to save the csv files
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = Constants.Selectafoldertosavethefile;
                folderDialog.ShowNewFolderButton = true;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath="";
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
    }
}
