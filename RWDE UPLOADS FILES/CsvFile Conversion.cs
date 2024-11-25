using Rwde;
using RWDE;
using RWDE_UPLOADS_FILES.RWDEDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RWDE_UPLOADS_FILES
{

    public partial class CsvFile_Conversion : Form
    {
        private readonly string connectionString;
        private readonly DBHelper dbHelper;

        public CsvFile_Conversion()//to initialize data
        {
            InitializeComponent();
            dbHelper = new DBHelper();
            connectionString = dbHelper.GetConnectionString();
            this.ControlBox = false;
            this.WindowState = FormWindowState.Maximized;
            
            if (File.Exists(Constants.LastFolderPathhcc))
            {
                string LastFolderPathhcc = File.ReadAllText(Constants.LastFolderPathhcc).Trim();  // Trim to remove any extra spaces or newlines
                // Check if the file content is not empty and the directory exists
                if (!string.IsNullOrEmpty(LastFolderPathhcc) && Directory.Exists(LastFolderPathhcc))
                {
                    txtPath.Text = LastFolderPathhcc;
                }
                else
                {
                    txtPath.Clear();  // Ensure the path is cleared if the file is empty or invalid
                }
            }
        }

        private void lblStartDate_Click(object sender, EventArgs e)
        {

        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(txtPath.Text, "CTClientsData.csv"); // Ensure the full file path includes a filename

            try
            {
                if (!Directory.Exists(txtPath.Text))
                {
                    MessageBox.Show("The selected folder does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // SQL query to execute the stored procedure
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    int batchid = dbHelper.GetNextBatchID();
                    // Call the stored procedure
                    using (SqlCommand cmd = new SqlCommand("ctclientsmapping", conn))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Batchid", batchid);
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
                                GetServicedataCSV(batchid);
                            }

                        }
                    }
                    MessageBox.Show("CSV file has been created successfully at " + filePath);//
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Access to the path is denied. Please choose a different folder or run the application as an administrator.", "Permission Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        public void GetServicedataCSV(int batchid)
        {
            
            string filePath = Path.Combine(txtPath.Text, "CTServicesData.csv"); // Ensure the full file path includes a filename

            try
            {
                if (!Directory.Exists(txtPath.Text))
                {
                    MessageBox.Show("The selected folder does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // SQL query to execute the stored procedure
                using (SqlConnection conn = new SqlConnection(connectionString))//
                {
                    conn.Open();

                    // Call the stored procedure
                    using (SqlCommand cmd = new SqlCommand("GetCTServicesForCSV", conn))
                    {
                      
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Batchid", batchid);
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

                    MessageBox.Show("CSV file has been created successfully at " + filePath);

                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Access to the path is denied. Please choose a different folder or run the application as an administrator.", "Permission Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void GetService(int batchid)
        {
            throw new NotImplementedException();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select a folder to save the file";
                folderDialog.ShowNewFolderButton = true;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string selectedPath = folderDialog.SelectedPath;
                        txtPath.Text = selectedPath;

                        // Test writing permission by creating a temporary file
                        string testFilePath = Path.Combine(selectedPath, "testfile.txt");
                        File.WriteAllText(testFilePath, "Testing permissions.");
                        File.Delete(testFilePath); // Clean up after test

                        MessageBox.Show("Selected folder: " + selectedPath);
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        MessageBox.Show("Access to the selected folder is denied. Please choose a different folder.", "Permission Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

    }
}
