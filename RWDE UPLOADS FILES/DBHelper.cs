using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Globalization;// Add appropriate using directive
using System.Xml;
using System.Data.SqlTypes;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using ClosedXML.Report.Utils;

namespace RWDE
{
    public class DbHelper : IDisposable
    {
        private readonly string connectionString; // Stores the connection string for the database
        private string error;  // Stores any error messages encountered during database operations
        private bool batchIdIncremented;
        private bool errorLogged;
        private bool _disposed = false;
        private bool errorOccurred = false;
        readonly SqlConnection connection;

        // Constructor to initialize the DBHelper class with a connection string
        public DbHelper()
        {
            // Define the connection string within the DBHelper class
            connectionString = ConfigurationManager.ConnectionStrings[Constants.MyConnection].ConnectionString;
            connection = new SqlConnection(connectionString);

        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // Prevents finalization as resources are already cleaned up
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
            }
        }
        // Finalizer
        ~DbHelper()
        {
            Dispose(false); // Only unmanaged resources
        }

        public SqlConnection GetConnection() // get connection string
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            return connection;
        }

        public bool ErrorOccurred  // getter to get the errorOccurred
        {
            get { return errorOccurred; }
        }

        // Method to check if a message is already logged

        public async Task<BatchDetails> GetBatchDetailsFromSpAsync(int batchId) // to check whether the conversion completed or not
        {
            try
            {
                SqlConnection connection = GetConnection();
                connection.Close();
                await connection.OpenAsync();
                errorOccurred = false;
                using (SqlCommand cmd = new SqlCommand(Constants.ConversionCompletion, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(Constants.AtBatchid, batchId);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            return new BatchDetails
                            {
                                ConversionStartedAt = reader[Constants.ConversionStartedAt] as DateTime?,
                                ConversionEndedAt = reader[Constants.ConversionEndedAt] as DateTime?
                            };
                        }
                    }
                }
                connection.Close();
                connection.Open();
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                errorOccurred = true;
                connection.Close();
                connection.Open();
                return null;
            }
        }

        public async Task<BatchDetails> GetBatchDetailsFromSpAsyncclients(int batchId) // to check whether the generation completed or not
        {
            try
            {
                SqlConnection connection = GetConnection();
                connection.Close();
                await connection.OpenAsync();
                errorOccurred = false;

                using (SqlCommand cmd = new SqlCommand(Constants.ClientConversionCompletion, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(Constants.AtBatchid, batchId);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            return new BatchDetails
                            {
                                ConversionStartedAt = reader[Constants.ConversionStartedAt] as DateTime?,
                                ConversionEndedAt = reader[Constants.ConversionEndedAt] as DateTime?
                            };
                        }
                    }
                }
                connection.Close();
                connection.Open();
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                errorOccurred = true;
                connection.Close();
                connection.Open();
                return null;
            }
        }

        public async Task<BatchDetailsgeneration> GetBatchDetailsFromSpAgenearationlients(int batchId) // to check whether the generation completed or not
        {
            try
            {
                SqlConnection connection = GetConnection();
                connection.Close();
                await connection.OpenAsync();
                errorOccurred = false;
                using (SqlCommand cmd = new SqlCommand(Constants.ClientGenerationCompletion, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(Constants.AtBatchid, batchId);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            return new BatchDetailsgeneration
                            {
                                GenerationStartedAt = reader[Constants.GenerationStartedAt] as DateTime?,
                                GenerationEndedAt = reader[Constants.GenerationEndedAt] as DateTime?
                            };
                        }
                    }
                }
                connection.Close();
                connection.Open();
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                errorOccurred = true;
                connection.Close();
                connection.Open();
                return null;
            }
        }

        public async Task<BatchDetailsgeneration> GetBatchDetailsFromSpAgenearationservices(int batchId) // to check whether the generation completed or not
        {
            try
            {
                SqlConnection connection = GetConnection();
                connection.Close();
                await connection.OpenAsync();
                errorOccurred = false;
                using (SqlCommand cmd = new SqlCommand(Constants.ServiceGenerationCompletion, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(Constants.AtBatchid, batchId);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            return new BatchDetailsgeneration
                            {
                                GenerationStartedAt = reader[Constants.GenerationStartedAt] as DateTime?,
                                GenerationEndedAt = reader[Constants.GenerationEndedAt] as DateTime?
                            };
                        }
                    }
                }
                connection.Close();
                connection.Open();
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                errorOccurred = true;
                connection.Close();
                connection.Open();
                return null;
            }
        }

        public void UpadteHCCServicesWithErrors(int batchId)
        {
            try
            {
                errorOccurred = false;

                // Call the stored procedure to update HCCServices
                using (SqlCommand updateCmd = new SqlCommand(Constants.UpdateHccServicesWithErrors, GetConnection()))
                {
                    updateCmd.CommandType = CommandType.StoredProcedure;
                    updateCmd.Parameters.AddWithValue(Constants.AtBatchid, batchId);
                    updateCmd.CommandTimeout = 120;
                    updateCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
            }
        }

        public void UpadteHCCClientsWithErrors(int batchId)
        {
            try
            {
                errorOccurred = false;

                // Call the stored procedure to update HCCServices
                using (SqlCommand updateCmd = new SqlCommand(Constants.UpdateHCCClientswithErrors, GetConnection()))
                {
                    updateCmd.CommandType = CommandType.StoredProcedure;
                    updateCmd.Parameters.AddWithValue(Constants.AtBatchid, batchId);
                    updateCmd.CommandTimeout = 120;
                    updateCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
            }
        }

        public void UpdateSucessfullyExportedServices(int selectedBatchId)
        {
            try
            {
                errorOccurred = false;

                // Call the stored procedure to update HCCServices
                using (SqlCommand updateCmd = new SqlCommand(Constants.UpdateSucessfullyExportServices, GetConnection()))
                {
                    updateCmd.CommandType = CommandType.StoredProcedure;
                    updateCmd.Parameters.AddWithValue(Constants.AtBatchid, selectedBatchId);
                    updateCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
            }
        }
        public DataTable LoadDatafilterServiceReconbatchid(int[] batchids)// to display the data based on BatchId
        {
            DataTable dy;
            List<DataTable> result = new List<DataTable>();
            List<int> noDataIds = new List<int>();
            try
            {
                errorOccurred = false;

                // Call the stored procedure to update HCCServices
                using (SqlCommand updateCmd = new SqlCommand(Constants.UpdateHccServicesWithErrors, GetConnection()))
                {
                    updateCmd.CommandType = CommandType.StoredProcedure;
                    updateCmd.ExecuteNonQuery();
                }
                connection.Close();

                foreach (int onebatch in batchids)
                {
                    SqlCommand cmd = new SqlCommand(Constants.SpServiceReconBatchId, GetConnection())
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue(Constants.AtBatchid, onebatch);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows == false)
                        {
                            noDataIds.Add(onebatch);
                        }

                        // Read each result set into a DataTable
                        DataTable table = new DataTable();
                        table.Load(reader);
                        result.Add(table); // Add the DataTable to the result list
                        if (Array.IndexOf(batchids, onebatch) == batchids.Length - 1 && noDataIds.Count != 0)
                        {
                            MessageBox.Show(string.Join(",", noDataIds.ToArray()) +
                                            Constants.NodatafoundfortheseBatchids);
                        }
                    }
                    connection.Close();
                }
                dy = result[0].Clone();
                try
                {
                    // Create an empty table with the same structure
                    foreach (var table in result)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            dy.ImportRow(row); // Add each row from the result set
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                if (dy.Rows.Count == 0 && batchids.Length != 1 && batchids.Length == noDataIds.Count)
                {
                    MessageBox.Show(Constants.Nodataexistsforthisbatchid, Constants.ServiceReconciliationReport);
                }
                return dy; // Return the populated DataTable
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                throw new Exception(Constants.AnErrorOccurredWhileLoadingData, ex);
            }
        }

        public class BatchDetailsgeneration // to set and get the batch generation timings 
        {
            public DateTime? GenerationStartedAt { get; set; }
            public DateTime? GenerationEndedAt { get; set; }
        }
        public class BatchDetails // to set and get the batch timings
        {
            public DateTime? ConversionStartedAt { get; set; }
            public DateTime? ConversionEndedAt { get; set; }
        }

        public int GetNextBatchId()// Getting BatchId for particular file insertion
        {
            try
            {
                errorOccurred = false;
                int nextBatchId;

                SqlCommand command = new SqlCommand(Constants.GetNextBatchIdQuery, GetConnection());

                // Get the last batch ID from the database
                object result = command.ExecuteScalar();
                int lastBatchIdFromDb = result == DBNull.Value ? 0 : Convert.ToInt32(result);

                // Determine the next batch ID to use based on the current state of batchIDIncremented
                if (!batchIdIncremented)
                {
                    // If batchIDIncremented is false, determine the next batch ID to use
                    if (lastBatchIdFromDb == 0)
                    {
                        // If no batch IDs exist in the database, start with batch ID 1
                        nextBatchId = 1;
                    }
                    else
                    {
                        // Increment the last batch ID to get the next available batch ID
                        nextBatchId = ++lastBatchIdFromDb;
                    }

                    // Set batchIDIncremented to true to indicate that the batch ID has been incremented
                    batchIdIncremented = true;
                }
                else
                {
                    // If batchIDIncremented is already true, return the last batch ID from the database
                    nextBatchId = lastBatchIdFromDb;
                }
                return nextBatchId;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                errorOccurred = true;
                return 0;
            }
        }
        public int GetMaxXmlBatchId()// Getting BatchId for particular file insertion
        {
            try
            {
                errorOccurred = false;

                SqlCommand command = new SqlCommand(Constants.GetMaxXmlBatchIdQuery, GetConnection());

                // Get the last batch ID from the database
                object result = command.ExecuteScalar();
                int maxBatchXmlidFromDb = result == DBNull.Value ? 0 : Convert.ToInt32(result);

                return maxBatchXmlidFromDb;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                errorOccurred = true;
                return 0;
            }
        }
        public void InsertBatch(int batchId, string fileName, string path, int type, string description, DateTime startedAt, int totalRowsInCurrentFile, int successfulRows, int status)// update batch status in database
        {
            try
            {
                errorOccurred = false;

                // Proceed with inserting the new batch record
                SqlCommand command = new SqlCommand(Constants.InsertBatchTable, GetConnection())
                {
                    CommandType = CommandType.StoredProcedure
                };

                string date = startedAt.ToString(Constants.DdMMyyyyHyphen);
                string time = startedAt.ToString(Constants.HHmmss);
                command.Parameters.AddWithValue(Constants.AtBatchid, batchId);
                command.Parameters.AddWithValue(Constants.AtFilename, fileName);
                command.Parameters.AddWithValue(Constants.AtDescription, description);
                command.Parameters.AddWithValue(Constants.AtPath, path);
                command.Parameters.AddWithValue(Constants.AtType, type);
                command.Parameters.AddWithValue(Constants.AtUploadStartedAt, startedAt);
                command.Parameters.AddWithValue(Constants.AtUploadEndedAt, DateTime.Now);
                command.Parameters.AddWithValue(Constants.AtConversionStartedAt, DBNull.Value);
                command.Parameters.AddWithValue(Constants.AtConversionEndedAt, DBNull.Value);
                command.Parameters.AddWithValue(Constants.AtGenerationStartedAt, DBNull.Value);
                command.Parameters.AddWithValue(Constants.AtGenerationEndedAt, DBNull.Value); // Assuming EndedAt is set to current time
                command.Parameters.AddWithValue(Constants.AtTotalRows, totalRowsInCurrentFile);
                command.Parameters.AddWithValue(Constants.AtSuccessfulRows, successfulRows);
                command.Parameters.AddWithValue(Constants.AtFailedRows, totalRowsInCurrentFile - successfulRows); // Calculate failed rows
                command.Parameters.AddWithValue(Constants.AtStatus, status);
                command.Parameters.AddWithValue(Constants.AtCreatedBy, Constants.CreatedBy);
                command.Parameters.AddWithValue(Constants.AtCreatedOn, DateTime.Now);
                command.Parameters.AddWithValue(Constants.AtComments, string.Empty); // Provide default value for Comments

                command.ExecuteNonQuery(); // Execute the SQL insert command
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Constants.ErrorInsertingBatch, ex.Message));
                errorOccurred = true;

                // Log or handle the exception appropriately
            }
        }

        public void InsertClientServiceDataOchin(SqlConnection connection, string[] data, int batchId, string fileName) // insertion of client service data
        {
            try
            {
                string original = data[5];
                string formattedName = string.Empty;

                if (!string.IsNullOrWhiteSpace(original) && original.Contains(","))
                {
                    var parts = original.Split(',');
                    if (parts.Length == 2)
                    {
                        string lastName = parts[0].Trim().ToLower();
                        string firstName = parts[1].Trim().ToLower();

                        // Capitalize first letters
                        firstName = char.ToUpper(firstName[0]) + firstName.Substring(1);
                        lastName = char.ToUpper(lastName[0]) + lastName.Substring(1);

                        formattedName = firstName + " " + lastName; // e.g., Annie Lee
                    }
                }

                errorOccurred = false;
                using (SqlCommand cmd = new SqlCommand(Constants.InsertClientServicesOchin, GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters with appropriate conversion and null handling
                    cmd.Parameters.AddWithValue(Constants.AtBatchid, batchId);
                    cmd.Parameters.AddWithValue(Constants.AtClntId, data[1]);
                    cmd.Parameters.AddWithValue(Constants.AtServiceDate, DateTime.Parse(data[3]));
                    cmd.Parameters.AddWithValue(Constants.AtContractIdsp, string.IsNullOrWhiteSpace(data[4]) ? 0 : int.Parse(data[4]));
                    cmd.Parameters.AddWithValue(Constants.AtStaffId, formattedName);
                    cmd.Parameters.AddWithValue(Constants.AtPrimServDesc, data[6]);
                    cmd.Parameters.AddWithValue(Constants.AtSecServDesc, data[7]);
                    cmd.Parameters.AddWithValue(Constants.AtSubServDesc, data[8]);
                    cmd.Parameters.AddWithValue(Constants.AtQuantityServed, decimal.Parse(data[9]));
                    cmd.Parameters.AddWithValue(Constants.AtUnitCd, data[10]);
                    cmd.Parameters.AddWithValue(Constants.AtActualMinutesSpent, string.IsNullOrWhiteSpace(data[4]) ? 0 : int.Parse(data[13]));
                    cmd.Parameters.AddWithValue(Constants.AtServiceId, data[15]);
                    // Construct AdditionalServiceInformation with the new format
                    string additionalServiceInformation = $"{Constants.AtIdEqualTto}{data[15]};";
                    cmd.Parameters.AddWithValue(Constants.AtAdditionalServiceInformation, additionalServiceInformation);

                    cmd.Parameters.AddWithValue(Constants.AtCreatedBy, Constants.CreatedBy);
                    cmd.Parameters.AddWithValue(Constants.AtCreatedOn, DateTime.Now);

                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    cmd.ExecuteNonQuery();
                }
            }
            catch (FormatException ex)
            {
                var st = new StackTrace(ex, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                LogError(ex.Message, ex.StackTrace, nameof(InsertClientServiceDataOchin), fileName, lineNumber, Constants.OchinCode);
            }
            catch (IndexOutOfRangeException ex)
            {
                var st = new StackTrace(ex, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                LogError(ex.Message, ex.StackTrace, nameof(InsertClientServiceDataOchin), fileName, lineNumber, Constants.OchinCode);
            }
            catch (SqlException sqlEx)
            {
                errorOccurred = true;
                MessageBox.Show($@"{Constants.SqlError}{sqlEx.Message}");
                var st = new StackTrace(sqlEx, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                LogError(sqlEx.Message, sqlEx.StackTrace, nameof(InsertClientServiceDataOchin), fileName, lineNumber, Constants.OchinCode);

            }
            catch (OverflowException overflowEx)
            {
                errorOccurred = true;
                MessageBox.Show($@"{Constants.SqlError}{overflowEx.Message}");
                var st = new StackTrace(overflowEx, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                LogError(overflowEx.Message, overflowEx.StackTrace, nameof(InsertClientServiceDataOchin), fileName, lineNumber, Constants.OchinCode);

            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(string.Format(Constants.ErrorMessagedynamic, ex.Message));
            }
        }

        public void InsertClientServiceDataCT(SqlConnection connection, string[] data, int batchId, string fileName) // insertion of client service data
        {
            try
            {
                errorOccurred = false;
                using (SqlCommand cmd = new SqlCommand(Constants.InsertClientServices, GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters with appropriate conversion and null handling
                    cmd.Parameters.AddWithValue(Constants.AtBatchid, batchId);
                    cmd.Parameters.AddWithValue(Constants.AtClntId, int.Parse(data[0]));
                    cmd.Parameters.AddWithValue(Constants.AtServiceDate, DateTime.Parse(data[1]));
                    cmd.Parameters.AddWithValue(Constants.AtContractIdsp, string.IsNullOrWhiteSpace(data[2]) ? 0 : int.Parse(data[2]));
                    cmd.Parameters.AddWithValue(Constants.AtStaffId, data[3]);
                    cmd.Parameters.AddWithValue(Constants.AtPrimServDesc, data[4]);
                    cmd.Parameters.AddWithValue(Constants.AtQuantityServed, decimal.Parse(data[6]));
                    cmd.Parameters.AddWithValue(Constants.AtUnitCd, data[5]);
                    cmd.Parameters.AddWithValue(Constants.AtActualMinutesSpent, string.IsNullOrWhiteSpace(data[2]) ? 0 : int.Parse(data[7]));
                    cmd.Parameters.AddWithValue(Constants.AtServiceId, data[8]);
                    // Construct AdditionalServiceInformation with the new format
                    string additionalServiceInformation = $"{Constants.AtIdEqualTto}{data[8]};";
                    cmd.Parameters.AddWithValue(Constants.AtAdditionalServiceInformation, additionalServiceInformation);

                    cmd.Parameters.AddWithValue(Constants.AtCreatedBy, Constants.CreatedBy);
                    cmd.Parameters.AddWithValue(Constants.AtCreatedOn, DateTime.Now);

                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    cmd.ExecuteNonQuery();
                }
            }
            catch (FormatException ex)
            {
                var st = new StackTrace(ex, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                LogError(ex.Message, ex.StackTrace, nameof(InsertClientServiceDataCT), fileName, lineNumber, Constants.OchinCode);
            }
            catch (IndexOutOfRangeException ex)
            {
                var st = new StackTrace(ex, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                LogError(ex.Message, ex.StackTrace, nameof(InsertClientServiceDataCT), fileName, lineNumber, Constants.OchinCode);
            }
            catch (SqlException sqlEx)
            {
                errorOccurred = true;
                MessageBox.Show($@"{Constants.SqlError}{sqlEx.Message}");
                var st = new StackTrace(sqlEx, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                LogError(sqlEx.Message, sqlEx.StackTrace, nameof(InsertClientServiceDataCT), fileName, lineNumber, Constants.OchinCode);

            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(string.Format(Constants.ErrorMessagedynamic, ex.Message));
            }
        }

        public void InsertClientServiceDataPhi(SqlConnection connection, string[] data, int batchId,string fileName)// insertion client data phi masked
        {
            try
            {
                errorOccurred = false;
                using (SqlCommand cmd = new SqlCommand(Constants.InsertClientServicesPhi, GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters with appropriate conversion and null handling
                    cmd.Parameters.AddWithValue(Constants.AtBatchid, batchId);
                    cmd.Parameters.AddWithValue(Constants.AtClntId, int.Parse(data[0]));
                    cmd.Parameters.AddWithValue(Constants.AtServiceDate, DateTime.Parse(data[1]));
                    cmd.Parameters.AddWithValue(Constants.AtContractIdsp, int.Parse(data[2]));
                    cmd.Parameters.AddWithValue(Constants.AtStaffId, data[3]);
                    cmd.Parameters.AddWithValue(Constants.AtPrimServDesc, data[4]);
                    cmd.Parameters.AddWithValue(Constants.AtQuantityServed, decimal.Parse(data[6]));
                    cmd.Parameters.AddWithValue(Constants.AtUnitCd, data[5]);
                    cmd.Parameters.AddWithValue(Constants.AtActualMinutesSpent, int.Parse(data[7]));
                    cmd.Parameters.AddWithValue(Constants.AtServiceId, int.Parse(data[8]));
                    // Construct AdditionalServiceInformation with the new format
                    string additionalServiceInformation = $"{Constants.AtIdEqualTto}{data[8]};";
                    cmd.Parameters.AddWithValue(Constants.AtAdditionalServiceInformation, additionalServiceInformation);

                    cmd.Parameters.AddWithValue(Constants.AtCreatedBy, Constants.CreatedBy);
                    cmd.Parameters.AddWithValue(Constants.AtCreatedOn, DateTime.Now);
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    cmd.ExecuteNonQuery();
                }
            }
            catch (FormatException ex)
            {
                var st = new StackTrace(ex, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                LogError(ex.Message, ex.StackTrace, nameof(InsertClientServiceDataPhi), fileName, lineNumber, Constants.OchinCode);
            }
            catch (IndexOutOfRangeException ex)
            {
                var st = new StackTrace(ex, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                LogError(ex.Message, ex.StackTrace, nameof(InsertClientServiceDataPhi), fileName, lineNumber, Constants.OchinCode);
            }
            catch (SqlException sqlEx)
            {
                errorOccurred = true;
                MessageBox.Show($@"{Constants.SqlError}{sqlEx.Message}");
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(string.Format(Constants.ErrorMessagedynamic, ex.Message));
            }
        }

         //for OCHIN FILE
        public void InsertClientInformationOchin(SqlConnection connection, string[] data, int batchid, string fileName) // cms client insertion
        {
            try
            {
                errorOccurred = false;
                using (SqlCommand command = new SqlCommand(Constants.InsertClientInfoOchin, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters with appropriate conversion and null handling
                    command.Parameters.AddWithValue(Constants.AtBatchid, batchid);
                    command.Parameters.AddWithValue(Constants.AtCreatedOn, DateTime.Now);
                    command.Parameters.AddWithValue(Constants.AtClntId, data[0] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFirstNm, data[1] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtLastNm, data[2] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtMi, data[3] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtChsnNm, data[4] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtDob, ConvertToDateTimeOrNull(data[6]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtIsDecd, ConvertToIntOrNull(data[7]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtDtOfDeath, ConvertToDateTimeOrNull(data[8]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtPlaceOfDeath, data[9] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtSsn, data[10] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHomelessFlg, ConvertToIntOrNull(data[5]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtGndrCd, data[12] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtSexCd, data[13] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtLangPrefCd, data[14] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtMrtlStatCd, data[15] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtSexualOrntTypeCd, data[16] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtEduLvl, data[17] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtVeteran, ConvertTo(data[18]) ?? (object)DBNull.Value);// 17 MISSING
                    command.Parameters.AddWithValue(Constants.AtEmail, data[20] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAllowCntctEmailInd, ConvertToIntOrNull(data[21]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtPrsnMobilePhn, data[22] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAllowCntctMobileInd, ConvertToIntOrNull(data[23]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAllowMsgsMobileInd, ConvertToIntOrNull(data[24]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAllowVmMobileInd, ConvertToIntOrNull(data[25]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtEmergencyCntctNm, data[26] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtEmergencyCntctRltnshp, data[27] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtEmergencyPrsnMobilePhn, data[28] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAllowEmergencyCntctInd, ConvertToIntOrNull(data[29]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAllowEmergencyCntctMsgsInd, ConvertToIntOrNull(data[30]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAllowEmergencyCntctVmInd, ConvertToIntOrNull(data[31]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAgencyId, ConvertToIntOrNull(data[33]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtRegstrnDt, ConvertToDateTimeOrNull(data[34]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAgencyClient1, data[35] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAgencyClient2, data[36] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkAgencyStatusCd, data[37] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAgencyStatusDt, ConvertToDateTimeOrNull(data[38]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtRelocFkStateCd, data[39] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtRelocFkCountyCd, data[40] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkAddrTypeCd, data[42] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAddressLine1, data[43] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAddressLine2, data[44] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtCity, data[45] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkStateCd, data[48] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtCounty, data[46] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtZip, data[47] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAddressSince, data[49] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtMailAllwInd, ConvertToIntOrNull(data[50]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtClientIncomeYear, ConvertToIntOrNull(data[52]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtClientNoIncomeFinResInd, ConvertToIntOrNull(data[53]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtClientTotalMthIncm, ConvertToDecimalOrNull(data[54]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHhIncomeYear, ConvertToIntOrNull(data[56]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHhNoIncomeFinResInd, ConvertToIntOrNull(data[57]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtEarnIncmFrmEmplmnt, ConvertToDecimalOrNull(data[58]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtRetirementIncm, ConvertToDecimalOrNull(data[59]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtSupSecIncm, ConvertToDecimalOrNull(data[60]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtSocDisInsIncm, ConvertToDecimalOrNull(data[61]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtOthrWlfrAsstIncm, ConvertToDecimalOrNull(data[62]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtPvtDisabInsIncm, ConvertToDecimalOrNull(data[63]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtVtrnDisPymtIncm, ConvertToDecimalOrNull(data[64]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtRegCntbrOthrIncm, ConvertToDecimalOrNull(data[65]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtWrkrCompIncm, ConvertToDecimalOrNull(data[66]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtGnrlAsstIncm, ConvertToDecimalOrNull(data[67]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtUnemplInsIncm, ConvertToDecimalOrNull(data[68]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtOthrSrcIncm, ConvertToDecimalOrNull(data[69]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHshldSize, ConvertToIntOrNull(data[70]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkEmplymntStatCd, data[71] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkRaceCd, data[73] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkRaceDtlCd, data[74] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkEthnCd, data[76] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkEthnDtlCd, data[77] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtCurrHivStatCd, data[79] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHivDxDt, ConvertToDateTimeOrNull(data[80]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHivDxSrcTxt, data[81] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAidsDxDt, ConvertToDateTimeOrNull(data[82]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAidsDxSrcTxt, data[83] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtPerinatalTransmission, ConvertToIntOrNull(data[84]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtMaleToMaleSexualContact, ConvertToIntOrNull(data[85]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHighRiskHeterosexualContact, ConvertToIntOrNull(data[86]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtInjectionDrugUse, ConvertToIntOrNull(data[87]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHemophiliaCoagulationDisorder, ConvertToIntOrNull(data[88]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtReceiptOfBloodTransfusion, ConvertToIntOrNull(data[89]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtRiskFactorNotReportedIdentifier, ConvertToIntOrNull(data[90]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHivTestDt, ConvertToDateTimeOrNull(data[92]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHivRsltStatCd, data[93] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkInsTypeCd, data[95] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkInsSubTypeCd, data[96] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtNewCovrgCvrsOldCvrgInd, ConvertToIntOrNull(data[97]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtStartDate, ConvertToDateTimeOrNull(data[98]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtEndDate, ConvertToDateTimeOrNull(data[99]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtNotes, data[100] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHsngAsstncCd, data[109] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAsstncStartDt, ConvertToDateTimeOrNull(data[110]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAsstncEndDt, ConvertToDateTimeOrNull(data[111]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkLvngSttnCd, data[113] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkLvngSttnDtlCd, data[114] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHousingStatus, data[114] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAsofDt, ConvertToDateTimeOrNull(data[115]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtCreatedBy, Constants.CreatedBy);
                    command.Parameters.AddWithValue(Constants.AtSourceSystemName, Constants.Ochin);
                    command.Parameters.AddWithValue(Constants.AtUserId, Constants.Userid);
                    command.Parameters.AddWithValue(Constants.AtAgencyIdCaps, Constants.Agencyid);
                    command.Parameters.AddWithValue(Constants.AtSourceId, DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtPrflSrcID, data[11] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtDemoSrcID, data[19] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtCnctInfoSrcID, data[32] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtSiteSrcID, data[41] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAddrSrcID, data[51] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtIncmSrcID, data[55] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHshldIncmSrcID, data[72] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtRaceSrcID, data[75] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtEthnSrcID, data[78] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHIVInfoSrcID, data[91] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHIVTestSrcID, data[94] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtInsrSrcID, data[101] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtMedOvrvwSrcID, data[108] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHsngAsstncSrcID, data[112] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtLvngSttnSrcID, data[116] ?? (object)DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
            catch (FormatException ex)
            {
                var st = new StackTrace(ex, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                LogError(ex.Message + data[1] + data[2], ex.StackTrace, nameof(InsertClientInformationOchin), fileName, lineNumber, Constants.OchinCode);
            }
            catch (IndexOutOfRangeException ex)
            {
                var st = new StackTrace(ex, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                LogError(ex.Message + data[1] + data[2], ex.StackTrace, nameof(InsertClientInformationOchin), fileName, lineNumber, Constants.OchinCode);
            }
            catch (SqlException ex)
            {
                var st = new StackTrace(ex, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                LogError(ex.Message + data[1] + data[2], ex.StackTrace, nameof(InsertClientInformationOchin), fileName, lineNumber, Constants.OchinCode);
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
            }
        }

        // for RWDE Generated CSV
        public void InsertClientInformationCt(SqlConnection connection, string[] data, int batchid, string fileName) // cms client insertion
        {
            try
            {
                errorOccurred = false;
                using (SqlCommand command = new SqlCommand(Constants.InsertClientInfoTest, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters with appropriate conversion and null handling
                    command.Parameters.AddWithValue(Constants.AtBatchid, batchid);
                    command.Parameters.AddWithValue(Constants.AtCreatedOn, DateTime.Now);
                    command.Parameters.AddWithValue(Constants.AtClntId, ConvertToIntOrNull(data[0]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFirstNm, data[1] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtLastNm, data[2] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtMi, data[3] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtChsnNm, data[4] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtDob, ConvertToDateTimeOrNull(data[5]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtIsDecd, ConvertToIntOrNull(data[6]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtDtOfDeath, ConvertToDateTimeOrNull(data[7]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtPlaceOfDeath, data[8] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtSsn, data[9] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHomelessFlg, ConvertToIntOrNull(data[10]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtGndrCd, data[11] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtSexCd, data[12] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtLangPrefCd, data[13] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtMrtlStatCd, data[14] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtSexualOrntTypeCd, data[15] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtEduLvl, data[16] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtVeteran, ConvertTo(data[17]) ?? (object)DBNull.Value);// 17 MISSING
                    command.Parameters.AddWithValue(Constants.AtEmail, data[18] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAllowCntctEmailInd, ConvertToIntOrNull(data[19]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtPrsnMobilePhn, data[20] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAllowCntctMobileInd, ConvertToIntOrNull(data[21]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAllowMsgsMobileInd, ConvertToIntOrNull(data[22]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAllowVmMobileInd, ConvertToIntOrNull(data[23]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtEmergencyCntctNm, data[24] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtEmergencyCntctRltnshp, data[25] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtEmergencyPrsnMobilePhn, data[26] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAllowEmergencyCntctInd, ConvertToIntOrNull(data[27]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAllowEmergencyCntctMsgsInd, ConvertToIntOrNull(data[28]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAllowEmergencyCntctVmInd, ConvertToIntOrNull(data[29]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAgencyId, ConvertToIntOrNull(data[30]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtRegstrnDt, ConvertToDateTimeOrNull(data[31]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAgencyClient1, data[32] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAgencyClient2, data[33] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkAgencyStatusCd, data[34] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAgencyStatusDt, ConvertToDateTimeOrNull(data[35]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtRelocFkStateCd, data[36] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtRelocFkCountyCd, data[27] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkAddrTypeCd, data[38] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAddressLine1, data[39] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAddressLine2, data[40] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtCity, data[41] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkStateCd, data[42] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtCounty, data[43] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtZip, data[44] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAddressSince, data[45] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtMailAllwInd, ConvertToIntOrNull(data[46]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtClientIncomeYear, ConvertToIntOrNull(data[47]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtClientNoIncomeFinResInd, ConvertToIntOrNull(data[48]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtClientTotalMthIncm, ConvertToDecimalOrNull(data[49]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHhIncomeYear, ConvertToIntOrNull(data[50]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHhNoIncomeFinResInd, ConvertToIntOrNull(data[51]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtEarnIncmFrmEmplmnt, ConvertToDecimalOrNull(data[52]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtRetirementIncm, ConvertToDecimalOrNull(data[53]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtSupSecIncm, ConvertToDecimalOrNull(data[54]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtSocDisInsIncm, ConvertToDecimalOrNull(data[55]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtOthrWlfrAsstIncm, ConvertToDecimalOrNull(data[56]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtPvtDisabInsIncm, ConvertToDecimalOrNull(data[57]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtVtrnDisPymtIncm, ConvertToDecimalOrNull(data[58]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtRegCntbrOthrIncm, ConvertToDecimalOrNull(data[59]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtWrkrCompIncm, ConvertToDecimalOrNull(data[60]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtGnrlAsstIncm, ConvertToDecimalOrNull(data[61]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtUnemplInsIncm, ConvertToDecimalOrNull(data[62]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtOthrSrcIncm, ConvertToDecimalOrNull(data[63]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHshldSize, ConvertToIntOrNull(data[64]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkEmplymntStatCd, data[65] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkRaceCd, data[66] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkRaceDtlCd, data[67] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkEthnCd, data[68] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkEthnDtlCd, data[69] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtCurrHivStatCd, data[70] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHivDxDt, ConvertToDateTimeOrNull(data[71]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHivDxSrcTxt, data[72] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAidsDxDt, ConvertToDateTimeOrNull(data[73]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAidsDxSrcTxt, data[74] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtPerinatalTransmission, ConvertToIntOrNull(data[75]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtMaleToMaleSexualContact, ConvertToIntOrNull(data[76]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHighRiskHeterosexualContact, ConvertToIntOrNull(data[77]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtInjectionDrugUse, ConvertToIntOrNull(data[78]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHemophiliaCoagulationDisorder, ConvertToIntOrNull(data[79]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtReceiptOfBloodTransfusion, ConvertToIntOrNull(data[80]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtRiskFactorNotReportedIdentifier, ConvertToIntOrNull(data[81]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHivTestDt, ConvertToDateTimeOrNull(data[82]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHivRsltStatCd, data[83] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkInsTypeCd, data[84] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkInsSubTypeCd, data[85] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtNewCovrgCvrsOldCvrgInd, ConvertToIntOrNull(data[86]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtStartDate, ConvertToDateTimeOrNull(data[87]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtEndDate, ConvertToDateTimeOrNull(data[88]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtNotes, data[89] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHsngAsstncCd, data[90] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAsstncStartDt, ConvertToDateTimeOrNull(data[91]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAsstncEndDt, ConvertToDateTimeOrNull(data[92]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkLvngSttnCd, data[93] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkLvngSttnDtlCd, data[94] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHousingStatus, data[95] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAsofDt, ConvertToDateTimeOrNull(data[96]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtCreatedBy, Constants.CreatedBy);
                    command.Parameters.AddWithValue(Constants.AtSourceSystemName, Constants.Ochin);
                    command.Parameters.AddWithValue(Constants.AtUserId, Constants.Userid);
                    command.Parameters.AddWithValue(Constants.AtAgencyIdCaps, Constants.Agencyid);
                    command.Parameters.AddWithValue(Constants.AtSourceId, DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
            catch (FormatException ex)
            {
                var st = new StackTrace(ex, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                LogError(ex.Message, ex.StackTrace, nameof(InsertClientInformationCt), fileName, lineNumber, Constants.OchinCode);
            }
            catch (IndexOutOfRangeException ex)
            {
                var st = new StackTrace(ex, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                LogError(ex.Message, ex.StackTrace, nameof(InsertClientInformationCt), fileName, lineNumber, Constants.OchinCode);
            }
            catch (SqlException ex)
            {
                var st = new StackTrace(ex, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                LogError(ex.Message, ex.StackTrace, nameof(InsertClientInformationCt), fileName, lineNumber, Constants.OchinCode);
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
            }
        }

        public void InsertClientInformationphiurn(SqlConnection connection, string[] data, int batchid, string fileName)// cms client insertion
        {
            try
            {
                errorOccurred = false;
                using (SqlCommand command = new SqlCommand(Constants.InsertClientInfoPhiWithUrn, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters with appropriate conversion and null handling
                    command.Parameters.AddWithValue(Constants.AtBatchid, batchid);
                    command.Parameters.AddWithValue(Constants.AtCreatedOn, DateTime.Now);
                    command.Parameters.AddWithValue(Constants.AtClntId, ConvertToIntOrNull(data[0]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFirstNm, data[1] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtLastNm, data[2] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtMi, data[3] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtChsnNm, data[4] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtDob, ConvertToDateTimeOrNull(data[6]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtIsDecd, ConvertToIntOrNull(data[7]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtDtOfDeath, ConvertToDateTimeOrNull(data[8]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtPlaceOfDeath, data[9] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtSsn, data[10] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHomelessFlg, ConvertToIntOrNull(data[5]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtGndrCd, data[12] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtSexCd, data[13] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtLangPrefCd, data[14] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtMrtlStatCd, data[15] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtSexualOrntTypeCd, data[16] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtEduLvl, data[17] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtVeteran, ConvertTo(data[18]) ?? (object)DBNull.Value);// 17 MISSING
                    command.Parameters.AddWithValue(Constants.AtEmail, data[20] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAllowCntctEmailInd, ConvertToIntOrNull(data[21]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtPrsnMobilePhn, data[22] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAllowCntctMobileInd, ConvertToIntOrNull(data[23]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAllowMsgsMobileInd, ConvertToIntOrNull(data[24]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAllowVmMobileInd, ConvertToIntOrNull(data[25]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtEmergencyCntctNm, data[26] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtEmergencyCntctRltnshp, data[27] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtEmergencyPrsnMobilePhn, data[28] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAllowEmergencyCntctInd, ConvertToIntOrNull(data[29]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAllowEmergencyCntctMsgsInd, ConvertToIntOrNull(data[30]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAllowEmergencyCntctVmInd, ConvertToIntOrNull(data[31]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAgencyId, ConvertToIntOrNull(data[33]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtRegstrnDt, ConvertToDateTimeOrNull(data[34]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAgencyClient1, data[35] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAgencyClient2, data[36] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkAgencyStatusCd, data[37] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAgencyStatusDt, ConvertToDateTimeOrNull(data[38]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtRelocFkStateCd, data[39] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtRelocFkCountyCd, data[40] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkAddrTypeCd, data[42] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAddressLine1, data[43] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAddressLine2, data[44] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtCity, data[45] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkStateCd, data[48] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtCounty, data[46] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtZip, data[47] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAddressSince, data[49] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtMailAllwInd, ConvertToIntOrNull(data[50]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtClientIncomeYear, ConvertToIntOrNull(data[52]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtClientNoIncomeFinResInd, ConvertToIntOrNull(data[53]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtClientTotalMthIncm, ConvertToDecimalOrNull(data[54]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHhIncomeYear, ConvertToIntOrNull(data[56]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHhNoIncomeFinResInd, ConvertToIntOrNull(data[57]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtEarnIncmFrmEmplmnt, ConvertToDecimalOrNull(data[58]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtRetirementIncm, ConvertToDecimalOrNull(data[59]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtSupSecIncm, ConvertToDecimalOrNull(data[60]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtSocDisInsIncm, ConvertToDecimalOrNull(data[61]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtOthrWlfrAsstIncm, ConvertToDecimalOrNull(data[62]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtPvtDisabInsIncm, ConvertToDecimalOrNull(data[63]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtVtrnDisPymtIncm, ConvertToDecimalOrNull(data[64]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtRegCntbrOthrIncm, ConvertToDecimalOrNull(data[65]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtWrkrCompIncm, ConvertToDecimalOrNull(data[66]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtGnrlAsstIncm, ConvertToDecimalOrNull(data[67]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtUnemplInsIncm, ConvertToDecimalOrNull(data[68]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtOthrSrcIncm, ConvertToDecimalOrNull(data[69]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHshldSize, ConvertToIntOrNull(data[70]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkEmplymntStatCd, data[71] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkRaceCd, data[73] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkRaceDtlCd, data[74] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkEthnCd, data[76] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkEthnDtlCd, data[77] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtCurrHivStatCd, data[79] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHivDxDt, ConvertToDateTimeOrNull(data[80]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHivDxSrcTxt, data[81] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAidsDxDt, ConvertToDateTimeOrNull(data[82]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAidsDxSrcTxt, data[83] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtPerinatalTransmission, ConvertToIntOrNull(data[84]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtMaleToMaleSexualContact, ConvertToIntOrNull(data[85]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHighRiskHeterosexualContact, ConvertToIntOrNull(data[86]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtInjectionDrugUse, ConvertToIntOrNull(data[87]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHemophiliaCoagulationDisorder, ConvertToIntOrNull(data[88]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtReceiptOfBloodTransfusion, ConvertToIntOrNull(data[89]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtRiskFactorNotReportedIdentifier, ConvertToIntOrNull(data[90]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHivTestDt, ConvertToDateTimeOrNull(data[92]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHivRsltStatCd, data[93] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkInsTypeCd, data[95] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkInsSubTypeCd, data[96] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtNewCovrgCvrsOldCvrgInd, ConvertToIntOrNull(data[97]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtStartDate, ConvertToDateTimeOrNull(data[98]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtEndDate, ConvertToDateTimeOrNull(data[99]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtNotes, data[100] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHsngAsstncCd, data[109] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAsstncStartDt, ConvertToDateTimeOrNull(data[110]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAsstncEndDt, ConvertToDateTimeOrNull(data[111]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkLvngSttnCd, data[113] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkLvngSttnDtlCd, data[114] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHousingStatus, data[114] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAsofDt, ConvertToDateTimeOrNull(data[115]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtCreatedBy, Constants.CreatedBy);
                    command.Parameters.AddWithValue(Constants.AtSourceSystemName, Constants.Ochin);
                    command.Parameters.AddWithValue(Constants.AtUserId, Constants.Userid);
                    command.Parameters.AddWithValue(Constants.AtAgencyIdCaps, Constants.Agencyid);
                    command.Parameters.AddWithValue(Constants.AtSourceId, DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
            catch (FormatException ex)
            {
                var st = new StackTrace(ex, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                LogError(ex.Message, ex.StackTrace, nameof(InsertClientInformationphiurn), fileName, lineNumber, Constants.OchinCode);
            }
            catch (IndexOutOfRangeException ex)
            {
                var st = new StackTrace(ex, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                LogError(ex.Message, ex.StackTrace, nameof(InsertClientInformationphiurn), fileName, lineNumber, Constants.OchinCode);
            }
            catch (SqlException ex)
            {
                var st = new StackTrace(ex, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                LogError(ex.Message, ex.StackTrace, nameof(InsertClientInformationphiurn), fileName, lineNumber, Constants.OchinCode);
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
            }
        }

        // Helper methods for conversions
        private int? ConvertToIntOrNull(string input)// convert to int
        {
            try
            {
                if (int.TryParse(input, out int result))
                {
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        private string ConvertTo(string input)// convert to int
        {
            try
            {
                return input;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        private DateTime ConvertToDateTime(string inputdate)
        {
            try
            {
                DateTime parsedDate = DateTime.ParseExact(inputdate, "MM/dd/yyyy", null);
                string formattedDate = parsedDate.ToString("yyyy-MM-dd");
                DateTime date = DateTime.Parse(formattedDate);
                return date;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return DateTime.Now;
            }
        }
        private DateTime? ConvertToDateTimeOrNull(string input)// convert to date or null
        {
            try
            {
                if (DateTime.TryParse(input, out DateTime result))
                {
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        private decimal? ConvertToDecimalOrNull(string input)// convert to decimal
        {
            try
            {
                if (decimal.TryParse(input, out decimal result))
                {
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public void InsertClientInformationPhi(SqlConnection connection, string[] data, int batchid, string fileName)// cms client insertion
        {
            try
            {
                errorOccurred = false;
                using (SqlCommand command = new SqlCommand(Constants.InsertClientInfoPhi, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters with appropriate conversion and null handling
                    command.Parameters.AddWithValue(Constants.AtBatchid, batchid);
                    command.Parameters.AddWithValue(Constants.AtCreatedOn, DateTime.Now);
                    command.Parameters.AddWithValue(Constants.AtClntId, ConvertToIntOrNull(data[0]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFirstNm, data[1] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtLastNm, data[2] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtMi, data[3] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtChsnNm, data[4] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtDob, ConvertToDateTimeOrNull(data[5]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtIsDecd, ConvertToIntOrNull(data[6]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtDtOfDeath, ConvertToDateTimeOrNull(data[7]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtPlaceOfDeath, data[8] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtSsn, data[9] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHomelessFlg, ConvertToIntOrNull(data[10]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtGndrCd, data[11] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtSexCd, data[12] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtLangPrefCd, data[13] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtMrtlStatCd, data[14] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtSexualOrntTypeCd, data[15] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtEduLvl, data[16] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtVeteran, ConvertTo(data[17]) ?? (object)DBNull.Value);// 17 MISSING
                    command.Parameters.AddWithValue(Constants.AtEmail, data[18] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAllowCntctEmailInd, ConvertToIntOrNull(data[19]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtPrsnMobilePhn, data[20] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAllowCntctMobileInd, ConvertToIntOrNull(data[21]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAllowMsgsMobileInd, ConvertToIntOrNull(data[22]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAllowVmMobileInd, ConvertToIntOrNull(data[23]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtEmergencyCntctNm, data[24] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtEmergencyCntctRltnshp, data[25] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtEmergencyPrsnMobilePhn, data[26] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAllowEmergencyCntctInd, ConvertToIntOrNull(data[27]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAllowEmergencyCntctMsgsInd, ConvertToIntOrNull(data[28]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAllowEmergencyCntctVmInd, ConvertToIntOrNull(data[29]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAgencyId, ConvertToIntOrNull(data[30]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtRegstrnDt, ConvertToDateTimeOrNull(data[31]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAgencyClient1, data[32] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAgencyClient2, data[33] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkAgencyStatusCd, data[34] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAgencyStatusDt, ConvertToDateTimeOrNull(data[35]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtRelocFkStateCd, data[36] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtRelocFkCountyCd, data[37] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkAddrTypeCd, data[38] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAddressLine1, data[39] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAddressLine2, data[40] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtCity, data[41] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkStateCd, data[42] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtCounty, data[43] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtZip, data[44] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAddressSince, data[45] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtMailAllwInd, ConvertToIntOrNull(data[46]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtClientIncomeYear, ConvertToIntOrNull(data[47]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtClientNoIncomeFinResInd, ConvertToIntOrNull(data[48]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtClientTotalMthIncm, ConvertToDecimalOrNull(data[49]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHhIncomeYear, ConvertToIntOrNull(data[50]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHhNoIncomeFinResInd, ConvertToIntOrNull(data[51]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtEarnIncmFrmEmplmnt, ConvertToDecimalOrNull(data[52]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtRetirementIncm, ConvertToDecimalOrNull(data[53]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtSupSecIncm, ConvertToDecimalOrNull(data[54]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtSocDisInsIncm, ConvertToDecimalOrNull(data[55]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtOthrWlfrAsstIncm, ConvertToDecimalOrNull(data[56]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtPvtDisabInsIncm, ConvertToDecimalOrNull(data[57]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtVtrnDisPymtIncm, ConvertToDecimalOrNull(data[58]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtRegCntbrOthrIncm, ConvertToDecimalOrNull(data[59]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtWrkrCompIncm, ConvertToDecimalOrNull(data[60]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtGnrlAsstIncm, ConvertToDecimalOrNull(data[61]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtUnemplInsIncm, ConvertToDecimalOrNull(data[62]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtOthrSrcIncm, ConvertToDecimalOrNull(data[63]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHshldSize, ConvertToIntOrNull(data[64]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkEmplymntStatCd, data[65] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkRaceCd, data[66] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkRaceDtlCd, data[67] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkEthnCd, data[68] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkEthnDtlCd, data[69] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtCurrHivStatCd, data[70] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHivDxDt, ConvertToDateTimeOrNull(data[71]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHivDxSrcTxt, data[72] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAidsDxDt, ConvertToDateTimeOrNull(data[73]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAidsDxSrcTxt, data[74] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtPerinatalTransmission, ConvertToIntOrNull(data[75]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtMaleToMaleSexualContact, ConvertToIntOrNull(data[76]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHighRiskHeterosexualContact, ConvertToIntOrNull(data[77]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtInjectionDrugUse, ConvertToIntOrNull(data[78]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHemophiliaCoagulationDisorder, ConvertToIntOrNull(data[79]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtReceiptOfBloodTransfusion, ConvertToIntOrNull(data[80]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtRiskFactorNotReportedIdentifier, ConvertToIntOrNull(data[81]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHivTestDt, ConvertToDateTimeOrNull(data[82]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHivRsltStatCd, data[83] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkInsTypeCd, data[84] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkInsSubTypeCd, data[85] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtNewCovrgCvrsOldCvrgInd, ConvertToIntOrNull(data[86]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtStartDate, ConvertToDateTimeOrNull(data[87]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtEndDate, ConvertToDateTimeOrNull(data[88]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtNotes, data[89] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHsngAsstncCd, data[90] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAsstncStartDt, ConvertToDateTimeOrNull(data[91]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAsstncEndDt, ConvertToDateTimeOrNull(data[92]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkLvngSttnCd, data[93] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFkLvngSttnDtlCd, data[94] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtHousingStatus, data[95] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtAsofDt, ConvertToDateTimeOrNull(data[96]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtCreatedBy, Constants.CreatedBy);
                    command.Parameters.AddWithValue(Constants.AtSourceSystemName, Constants.Ochin);
                    command.Parameters.AddWithValue(Constants.AtUserId, Constants.Userid);
                    command.Parameters.AddWithValue(Constants.AtAgencyIdCaps, Constants.Agencyid);
                    command.Parameters.AddWithValue(Constants.AtSourceId, DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
            catch (FormatException ex)
            {
                var st = new StackTrace(ex, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                LogError(ex.Message, ex.StackTrace, nameof(InsertClientInformationPhi), fileName, lineNumber, Constants.OchinCode);
            }
            catch (IndexOutOfRangeException ex)
            {
                var st = new StackTrace(ex, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                LogError(ex.Message, ex.StackTrace, nameof(InsertClientInformationPhi), fileName, lineNumber, Constants.OchinCode);
            }
            catch (SqlException ex)
            {
                var st = new StackTrace(ex, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                LogError(ex.Message, ex.StackTrace, nameof(InsertClientInformationPhi), fileName, lineNumber, Constants.OchinCode);
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
            }
        }

        public bool InsertClientData(SqlConnection connection, string[] data, int batchid, string fileName)// Client table insertion
        {
            try
            {
                errorOccurred = false;
                string ariesId = GetStringValue(data, 9); // Extract Aries ID from data array

                SqlCommand command = new SqlCommand(Constants.InsertIntoDlClients, GetConnection())
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Add parameters to the stored procedure
                command.Parameters.AddWithValue(Constants.AtBatchid, batchid);
                command.Parameters.AddWithValue(Constants.AtClientIdCaps, GetStringData(data, 9)?.Trim('"'));
                command.Parameters.AddWithValue(Constants.AtClientFirstName, data[0]?.Trim()); // Trim leading and trailing whitespaces
                command.Parameters.AddWithValue(Constants.AtClientLastName, data[1]?.Trim()); // Trim leading and trailing whitespaces
                command.Parameters.AddWithValue(Constants.AtClientMiddleInitial, data[2]?.Trim()); // Trim leading and trailing whitespaces
                command.Parameters.AddWithValue(Constants.AtClientMothersMaidenNameFirstAndThirdCharacters, data[3]?.Trim()); // Trim leading and trailing whitespaces

                string[] dateFormats = { Constants.MdYyyySlash, Constants.DmYyyy, Constants.DmYyyySlash, Constants.MdYyyy, Constants.DMmmYyyy, Constants.MmmDYyyy, Constants.YyyyMd };

                string[] parts = data[4].Split(' ');

                string dateString = parts[0].Replace("\"", "").Trim(); // Remove double quotes and trim

                if (dateString.Length > 0 && DateTime.TryParseExact(dateString, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime clientDateOfBirth))
                {
                    // If parsing succeeds, use the parsed date
                    command.Parameters.AddWithValue(Constants.AtClientDateOfBirth, clientDateOfBirth);
                }
                else
                {
                    command.Parameters.AddWithValue(Constants.AtClientDateOfBirth, DBNull.Value); // Add DBNull instead
                }
                command.Parameters.AddWithValue(Constants.AtClientGender, data[5]?.Trim()); // Trim leading and trailing whitespaces
                command.Parameters.AddWithValue(Constants.AtClientIsRelatedOrAffected, data[6]?.Trim()); // Trim leading and trailing whitespaces
                command.Parameters.AddWithValue(Constants.AtClientRecordIsShared, data[7]?.Trim()); // Trim leading and trailing whitespaces
                command.Parameters.AddWithValue(Constants.AtClientUrnExtended, data[8]?.Trim()); // Trim leading and trailing whitespaces
                command.Parameters.AddWithValue(Constants.AtAgencyClientId1, data[10]?.Trim()); // Trim leading and trailing whitespace
                command.Parameters.AddWithValue(Constants.AtDownloadDate, DateTime.Now);
                command.Parameters.AddWithValue(Constants.AtExtracted, 3);
                command.Parameters.AddWithValue(Constants.AtExtractionDate, new DateTime(2024, 6, 2));
                command.Parameters.AddWithValue(Constants.AtCmsMatch, 2);
                command.Parameters.AddWithValue(Constants.AtCmsMatchDate, new DateTime(2024, 5, 2));
                command.Parameters.AddWithValue(Constants.AtCreatedBy, 6);
                command.Parameters.AddWithValue(Constants.AtCreatedOn, DateTime.Now);  // Execute the command
                command.ExecuteNonQuery();
                return true;
            }
            catch (SqlException ex)
            {
                var st = new StackTrace(ex, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                LogError(ex.Message, ex.StackTrace, nameof(InsertClientData), fileName, lineNumber, Constants.HccCode);
                return false;
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                var st = new StackTrace(ex, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                LogError(ex.Message, ex.StackTrace, nameof(InsertClientData), fileName, lineNumber, Constants.HccCode);
                if (ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return false;
                }
                throw;
            }
        }
        public bool InsertClientDataPhi(SqlConnection connection, string[] data, int batchid, string fileName)// Client table insertion
        {
            try
            {
                errorOccurred = false;
                string ariesId = GetStringValue(data, 9); // Extract Aries ID from data array

                SqlCommand command = new SqlCommand(Constants.InsertIntoDlClientsPhi, GetConnection())
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Add parameters to the stored procedure
                command.Parameters.AddWithValue(Constants.AtBatchid, batchid);
                command.Parameters.AddWithValue(Constants.AtClientIdCaps, GetStringData(data, 9)?.Trim('"'));
                command.Parameters.AddWithValue(Constants.AtClientFirstName, data[0]?.Trim()); // Trim leading and trailing whitespaces
                command.Parameters.AddWithValue(Constants.AtClientLastName, data[1]?.Trim()); // Trim leading and trailing whitespaces
                command.Parameters.AddWithValue(Constants.AtClientMiddleInitial, data[2]?.Trim()); // Trim leading and trailing whitespaces
                command.Parameters.AddWithValue(Constants.AtClientMothersMaidenNameFirstAndThirdCharacters, data[3]?.Trim()); // Trim leading and trailing whitespaces

                string[] dateFormats = { Constants.MdYyyySlash, Constants.DmYyyy, Constants.DmYyyySlash, Constants.MdYyyy, Constants.DMmmYyyy, Constants.MmmDYyyy, Constants.YyyyMd };

                string[] parts = data[4].Split(' ');

                string dateString = parts[0].Replace("\"", "").Trim(); // Remove double quotes and trim

                if (dateString.Length > 0 && DateTime.TryParseExact(dateString, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime clientDateOfBirth))
                {
                    // If parsing succeeds, use the parsed date
                    command.Parameters.AddWithValue(Constants.AtClientDateOfBirth, clientDateOfBirth);
                }
                else
                {
                    command.Parameters.AddWithValue(Constants.AtClientDateOfBirth, DBNull.Value); // Add DBNull instead
                }
                command.Parameters.AddWithValue(Constants.AtClientGender, data[5]?.Trim()); // Trim leading and trailing whitespaces
                command.Parameters.AddWithValue(Constants.AtClientIsRelatedOrAffected, data[6]?.Trim()); // Trim leading and trailing whitespaces
                command.Parameters.AddWithValue(Constants.AtClientRecordIsShared, data[7]?.Trim()); // Trim leading and trailing whitespaces
                command.Parameters.AddWithValue(Constants.AtClientUrnExtended, data[8]?.Trim()); // Trim leading and trailing whitespaces
                command.Parameters.AddWithValue(Constants.AtAgencyClientId1, data[10]?.Trim()); // Trim leading and trailing whitespace
                command.Parameters.AddWithValue(Constants.AtDownloadDate, DateTime.Now);
                command.Parameters.AddWithValue(Constants.AtExtracted, 3);
                command.Parameters.AddWithValue(Constants.AtExtractionDate, new DateTime(2024, 6, 2));
                command.Parameters.AddWithValue(Constants.AtCmsMatch, 2);
                command.Parameters.AddWithValue(Constants.AtCmsMatchDate, new DateTime(2024, 5, 2));
                command.Parameters.AddWithValue(Constants.AtCreatedBy, 6);
                command.Parameters.AddWithValue(Constants.AtCreatedOn, DateTime.Now);  // Execute the command
                command.ExecuteNonQuery();
                return true;
            }
            catch (SqlException ex)
            {
                var st = new StackTrace(ex, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                LogError(ex.Message, ex.StackTrace, nameof(InsertClientDataPhi), fileName, lineNumber, Constants.HccCode);
                return false;
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                var st = new StackTrace(ex, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                LogError(ex.Message, ex.StackTrace, nameof(InsertClientDataPhi), fileName, lineNumber, Constants.HccCode);
                if (ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return true;
                }
                throw;
            }
        }
        public void InsertdeceasedData(SqlConnection connection, string[] data, int batchid)// Deceased Table Insertion
        {
            try
            {
                errorOccurred = false;
                string ariesId = GetStringValue(data, 0); // Extract Aries ID from data array

                using (SqlCommand command = new SqlCommand(Constants.InsertIntoDlDeceasedClients, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(Constants.AtClientIdCaps, Convert.ToInt32(GetStringValue(data, 0)));
                    command.Parameters.AddWithValue(Constants.AtBatchid, batchid);
                    command.Parameters.AddWithValue(Constants.AtClientLastFirstName, string.Join(GetStringValue(data, 1), GetStringValue(data, 2)));
                    command.Parameters.AddWithValue(Constants.AtClientStatus,GetStringValue(data,3));
            
                    command.Parameters.AddWithValue(Constants.AtStatusAsOfDate, ConvertToDateTime(GetStringValue(data, 4)));
                    command.Parameters.AddWithValue(Constants.AtLastServiceDate, ConvertToDateTime(GetStringValue(data, 5)));
                   
                    command.Parameters.AddWithValue(Constants.AtDownloadDate, DateTime.Parse("2024-08-01")); // Assuming 2024-08-01 is the correct date
                    command.Parameters.AddWithValue(Constants.AtExtracted, Constants.ExtractedCode); // Assuming 3 is a valid value for Extracted
                    command.Parameters.AddWithValue(Constants.AtExtractionDate, DateTime.Parse("2024-02-06")); // Assuming 2024-02-06 is the correct date
                    command.Parameters.AddWithValue(Constants.AtCmsMatch, Constants.CmsMatchDateCode); // Assuming 2 is a valid value for CMSMatch
                    command.Parameters.AddWithValue(Constants.AtCmsMatchDate, DateTime.Parse("2024-02-05")); // Assuming 2024-02-05 is the correct date
                    command.Parameters.AddWithValue(Constants.AtCreatedBy, Constants.CreatedBy); // Assuming "Admin" is the correct creator
                    command.Parameters.AddWithValue(Constants.AtCreatedOn, DateTime.Parse("2024-09-03")); // Assuming 2024-09-03 is the correct date
                    command.ExecuteNonQuery(); // Execute the SQL command to insert client data
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                error = ex.Message; // Log error message
                Log($"{ex.Message}", Constants.Error, Constants.InsertClientData, Constants.Uploadct); // Assuming fileName is accessible herE
                if (ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
                throw;
            }
        }
        public void InsertConsentData(SqlConnection connection, string[] data, int batchid)// Consent table insertion
        {
            try
            {
                errorOccurred = false;
                // Parse date values from the CSV data
                DateTime? documentDate = ParseDateTime(GetStringValue(data, 5));
                DateTime? obtainDate = ParseDateTime(GetStringValue(data, 6));
                DateTime? expireDate = ParseDateTime(GetStringValue(data, 7));
                DateTime? eligibilityDocumentExpireDate = ParseDateTime(GetStringValue(data, 12));

                string clientLastFirstName = $"{GetStringValue(data, 2)} {GetStringValue(data, 3)}";

                using (SqlCommand command = new SqlCommand(Constants.InsertDlConsent, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue(Constants.AtBatchid, batchid);
                    command.Parameters.AddWithValue(Constants.AtClientIdCaps, GetStringValue(data, 0)?.Trim('"'));
                    command.Parameters.AddWithValue(Constants.AtAgencyClientId, GetStringValue(data, 1));
                    command.Parameters.AddWithValue(Constants.AtClientLastFirstName, clientLastFirstName);
                    command.Parameters.AddWithValue(Constants.AtDocumentType, GetStringValue(data, 4)); // Assuming this is correct
                    command.Parameters.AddWithValue(Constants.AtDocumentDate, (object)documentDate ?? DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtObtainDate, (object)obtainDate ?? DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtExpireDate, (object)expireDate ?? DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtSource, GetStringValue(data, 8));
                    command.Parameters.AddWithValue(Constants.AtCreatedSource, GetStringValue(data, 9));
                    command.Parameters.AddWithValue(Constants.AtCreateAgency, GetStringValue(data, 10));
                    command.Parameters.AddWithValue(Constants.AtClientStatus, GetStringValue(data, 11));
                    command.Parameters.AddWithValue(Constants.AtEligibilityDocumentExpireDate, (object)eligibilityDocumentExpireDate ?? DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtDownloadDate, DateTime.Now); // Assuming current date/time
                    command.Parameters.AddWithValue(Constants.AtExtracted, Constants.ExtractedCode); // Assuming a value for Extracted
                    command.Parameters.AddWithValue(Constants.AtExtractionDate, DateTime.Now); // Assuming current date/time
                    command.Parameters.AddWithValue(Constants.AtCmsMatch, Constants.CmsMatchDateCode); // Assuming a value for CMSMatch
                    command.Parameters.AddWithValue(Constants.AtCmsMatchDate, DateTime.Now); // Assuming current date/time
                    command.Parameters.AddWithValue(Constants.AtCreatedBy, Constants.CreatedBy); // Assuming a value for CreatedBy
                    command.Parameters.AddWithValue(Constants.AtCreatedOn, DateTime.Now); // Assuming current date/time

                    // Execute the query
                    command.ExecuteNonQuery();
                    // Increment totalRows counter
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                error = ex.Message;
                Log($"{ex.Message}", Constants.Error, Constants.ConsentData, Constants.Uploadct); // Assuming fileName is accessible here
                if (ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
                throw;
            }
        }
        public void InsertDlEligibility(SqlConnection connection, string[] data, int batchid)// Eligibility Table insertion
        {
            try
            {
                errorOccurred = false;
                // Parse date values from the CSV data
                DateTime? documentDate = ParseDateTime(GetStringValue(data, 5));
                DateTime? obtainDate = ParseDateTime(GetStringValue(data, 6));
                DateTime? expireDate = ParseDateTime(GetStringValue(data, 7));
                DateTime? eligibilityDocumentExpireDate = ParseDateTime(GetStringValue(data, 12));

                string clientLastFirstName = $"{GetStringValue(data, 2)} {GetStringValue(data, 3)}";

                using (SqlCommand command = new SqlCommand(Constants.InsertDlEligibility, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue(Constants.AtBatchid, batchid);
                    command.Parameters.AddWithValue(Constants.AtClientIdCaps, GetStringValuedata(data, 0));
                    command.Parameters.AddWithValue(Constants.AtAgencyClientId1, GetStringValuedata(data, 1));
                    command.Parameters.AddWithValue(Constants.AtClientLastFirstName, clientLastFirstName);
                    command.Parameters.AddWithValue(Constants.AtDocumentType, GetStringValuedata(data, 4));
                    command.Parameters.AddWithValue(Constants.AtDocumentDate, (object)documentDate ?? DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtObtainDate, (object)obtainDate ?? DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtExpireDate, (object)expireDate ?? DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtSource, GetStringValuedata(data, 8));
                    command.Parameters.AddWithValue(Constants.AtCreatedSource, GetStringValuedata(data, 9));
                    command.Parameters.AddWithValue(Constants.AtCreateAgency, GetStringValuedata(data, 10));
                    command.Parameters.AddWithValue(Constants.AtClientStatus, GetStringValuedata(data, 11));
                    command.Parameters.AddWithValue(Constants.AtEligibilityDocumentExpireDate, (object)eligibilityDocumentExpireDate ?? DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtDownloadDate, DateTime.Now);
                    command.Parameters.AddWithValue(Constants.AtExtracted, Constants.ExtractedCode);
                    command.Parameters.AddWithValue(Constants.AtExtractionDate, DateTime.Now);
                    command.Parameters.AddWithValue(Constants.AtCmsMatch, Constants.CmsMatchDateCode);
                    command.Parameters.AddWithValue(Constants.AtCmsMatchDate, DateTime.Now);
                    command.Parameters.AddWithValue(Constants.AtCreatedBy, Constants.CreatedBy);
                    command.Parameters.AddWithValue(Constants.AtCreatedOn, DateTime.Now);
                    // Execute the query
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
                Log($"{ex.Message}", Constants.Error, Constants.AriesEligibility, Constants.Uploadct);
                if (ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
                throw;
            }
        }
        private DateTime? ParseDateTime(string value)// parsing the datetime in required format
        {
            try
            {
                DateTime? result = null;
                if (!string.IsNullOrEmpty(value))
                {
                    // Trim any extra quotation marks
                    value = value.Trim('"');

                    // Define potential date formats
                    string[] dateFormats = { Constants.MMddyyyyhhmmsstt, Constants.MMddyyyyHHmm, Constants.YyyyMMddHHmmss, Constants.YyyyMmDd };

                    // Try parsing with specified formats
                    if (DateTime.TryParseExact(value, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                    {
                        result = parsedDate;
                    }
                    else
                    {
                        MessageBox.Show(string.Format(Constants.FailedToParseDate, value));
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private string GetStringValuedata(string[] data, int index)// to get the eaxct string values
        {
            try
            {
                return data.Length > index ? data[index].Trim('"') : string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void InsertDlServices(SqlConnection connection, string[] data, int batchid, string fileName, int rowNumber) // DlServices table insertion
        {
            try
            {
                using (SqlCommand command = new SqlCommand(Constants.InsertDlServices, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Extract service ID from service notes
                    string serviceNotes = GetStringData(data, 2)?.Trim('"'); // Assuming service notes are at index 2
                    string serviceId = ExtractServiceId(serviceNotes);
                    
                    // Add parameters to the stored procedure
                    command.Parameters.AddWithValue(Constants.AtServiceId, serviceId);
                    command.Parameters.AddWithValue(Constants.AtBatchid, batchid);
                    command.Parameters.AddWithValue(Constants.AtClientIdCaps, GetStringData(data, 0)?.Trim('"')); // Treat ClientID as string
                    command.Parameters.AddWithValue(Constants.AtClientUrn, GetStringData(data, 1)?.Trim('"'));
                    command.Parameters.AddWithValue(Constants.AtServiceNotes, serviceNotes); // Original service notes
                    command.Parameters.AddWithValue(Constants.AtDownloadDate, DateTime.Now);
                    command.Parameters.AddWithValue(Constants.AtExtracted, Constants.ExtractedCode);
                    command.Parameters.AddWithValue(Constants.AtExtractionDate, DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtCmsMatch, Constants.CmsMatchCode);
                    command.Parameters.AddWithValue(Constants.AtCmsMatchDate, DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtCreatedBy, Constants.CreatedBy);
                    command.Parameters.AddWithValue(Constants.AtCreatedOn, DateTime.Now);

                    command.ExecuteNonQuery(); // Execute the SQL command to insert data
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                // Get detailed error information from the stack trace

                var st = new StackTrace(ex, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                LogError(ex.Message, ex.StackTrace, nameof(InsertDlServices), fileName, rowNumber, Constants.HccCode);
                if (ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
            }
        }
        public void InsertDlServicesPhi(SqlConnection connection, string[] data, int batchid, string fileName, int rowNumber) // DlServices table insertion with PHI
        {
            try
            {
                errorOccurred = false;
                using (SqlCommand command = new SqlCommand(Constants.InsertDlServicesPhi, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Extract service ID from service notes
                    string serviceNotes = GetStringData(data, 2)?.Trim('"'); // Assuming service notes are at index 2
                    string serviceId = ExtractServiceId(serviceNotes);
                    
                    // Add parameters to the stored procedure
                    command.Parameters.AddWithValue(Constants.AtServiceId, serviceId);
                    command.Parameters.AddWithValue(Constants.AtBatchid, batchid);
                    command.Parameters.AddWithValue(Constants.AtClientIdCaps, GetStringData(data, 0)?.Trim('"')); // Treat ClientID as string
                    command.Parameters.AddWithValue(Constants.AtClientUrn, GetStringData(data, 1)?.Trim('"'));
                    command.Parameters.AddWithValue(Constants.AtServiceNotes, serviceNotes); // Original service notes
                    command.Parameters.AddWithValue(Constants.AtDownloadDate, DateTime.Now);
                    command.Parameters.AddWithValue(Constants.AtExtracted, Constants.ExtractedCode);
                    command.Parameters.AddWithValue(Constants.AtExtractionDate, DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtCmsMatch, Constants.CmsMatchCode);
                    command.Parameters.AddWithValue(Constants.AtCmsMatchDate, DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtCreatedBy, Constants.CreatedBy);
                    command.Parameters.AddWithValue(Constants.AtCreatedOn, DateTime.Now);

                    command.ExecuteNonQuery(); // Execute the SQL command to insert data
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                // Get detailed error information from the stack trace

                var st = new StackTrace(ex, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                LogError(ex.Message, ex.StackTrace, nameof(InsertDlServices), fileName, rowNumber, Constants.HccCode);
                if (ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
            }
        }
        private string ExtractServiceId(string serviceNotes) // extraction of Service Id
        {
            try
            {
                if (string.IsNullOrWhiteSpace(serviceNotes))
                {
                    throw new ArgumentException(Constants.ServiceNotesCannotBeNullOrEmpty);
                }

                // Define patterns to match service ID
                string[] patterns = { $"{Constants.AtIdEqualTto}", $"{Constants.AtIdEqualTto}", Constants.IdHyphen, Constants.IdColon };
                foreach (var pattern in patterns)
                {
                    if (serviceNotes.StartsWith(pattern, StringComparison.OrdinalIgnoreCase))
                    {
                        // Find the index of the first occurrence of "=", "-", or ":"
                        int separatorIndex = serviceNotes.IndexOfAny(new char[] { Constants.EqualTo, Constants.Hyphen, Constants.Colon });

                        if (separatorIndex != -1)
                        {
                            // Extract the service ID after the separator
                            string idString = serviceNotes.Substring(separatorIndex + 1).Trim(';').Trim();
                            if (!string.IsNullOrEmpty(idString))
                            {
                                return idString;
                            }
                        }
                    }
                }

                // If no valid pattern is found or service ID extraction fails
                throw new ArgumentException(Constants.Invalidserviceid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        private string GetStringData(string[] data, int index) // convert to string
        {
            try
            {
                if (index >= 0 && index < data.Length)
                {
                    return data[index];
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public void LogError(string message, string stackTrace, string functionName, string fileName, int? lineNumber,int Code) // Logger table insertion for errors and completion
        {
            try
            {
                errorOccurred = false;
                int maxStackLength = 1000; // Adjust this to match your database schema

                // Truncate the stack trace if it exceeds the maximum length
                if (stackTrace.Length > maxStackLength)
                {
                    stackTrace = stackTrace.Substring(0, maxStackLength);
                }
                try
                {
                    SqlCommand cmd = new SqlCommand(Constants.LoggerError, GetConnection())
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue(Constants.AtType, Constants.Error); // Error type
                    cmd.Parameters.AddWithValue(Constants.AtModule, Code); // Module name
                    cmd.Parameters.AddWithValue(Constants.AtStack, stackTrace);
                    cmd.Parameters.AddWithValue(Constants.AtMessage, message);
                    cmd.Parameters.AddWithValue(Constants.AtFilename, fileName);
                    cmd.Parameters.AddWithValue(Constants.AtLineNumber, lineNumber.HasValue ? (object)lineNumber.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue(Constants.AtFunctionName, functionName);
                    cmd.Parameters.AddWithValue(Constants.AtComments, DBNull.Value); // Assuming no comments
                    cmd.Parameters.AddWithValue(Constants.AtCreatedBy, Constants.CreatedBy); // Assuming a specific user ID
                    cmd.Parameters.AddWithValue(Constants.AtCreatedOn, DateTime.Now);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    errorOccurred = true;
                    // Log an additional error if logging itself fails
                    MessageBox.Show(ex.Message);
                }

                errorLogged = true;
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);

            }// Set errorLogged flag to true after logging the first error
        }
        public void UpdateBatchServices(int batchId, DateTime startTime, DateTime endTime, int allTotalRows)// update batch data
        {// Updating status and Time on Batch Table     
            try
            {
                errorOccurred = false;
                allTotalRows++;

                // Define the SQL query to update the ConversionStartedAt and ConversionEndedAt columns
                string updateQuery = Constants.UpdateConversionServices;

                using (SqlCommand command = new SqlCommand(updateQuery, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    // Set the parameters
                    command.Parameters.AddWithValue(Constants.AtBatchid, batchId);
                    command.Parameters.AddWithValue(Constants.AtConversionStartedAt, startTime);
                    command.Parameters.AddWithValue(Constants.AtConversionEndedAt, endTime);
                    command.Parameters.AddWithValue(Constants.AtAllTotalRows, allTotalRows - 1);

                    // Execute the SQL update command
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show($@"{Constants.Errorupdatingbatch}{ex.Message}");
                // Log or handle the exception appropriately
            }
        }
        public void UpdateBatchclient(int batchId, DateTime startTime, DateTime endTime, int allTotalRows)// update batch for client data
        {// Updating status and Time on Batch Table     
            try
            {
                errorOccurred = false;
                allTotalRows++;

                // Define the SQL query to update the ConversionStartedAt and ConversionEndedAt columns
                string updateQuery = Constants.UpdateConversionClient;

                using (SqlCommand command = new SqlCommand(updateQuery, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    // Set the parameters
                    command.Parameters.AddWithValue(Constants.AtBatchid, batchId);
                    command.Parameters.AddWithValue(Constants.AtConversionStartedAt, startTime);
                    command.Parameters.AddWithValue(Constants.AtConversionEndedAt, endTime);
                    command.Parameters.AddWithValue(Constants.AtAllTotalRows, allTotalRows - 1);

                    // Execute the SQL update command
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show($@"{Constants.Errorupdatingbatch}{ex.Message}");
                // Log or handle the exception appropriately
            }
        }
        public void InsertDlFinancials(SqlConnection connection, string[] data, int batchid)// Financial data insertion
        {
            try
            {
                errorOccurred = false;
                using (SqlCommand command = new SqlCommand(Constants.InsertDlFinancial, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters with proper null or empty value checks
                    command.Parameters.AddWithValue(Constants.AtBatchid, batchid);
                    command.Parameters.AddWithValue(Constants.AtClientIdCaps, GetStringValue(data, 0)?.Trim('"'));
                    AddDecimalParameter(command, Constants.AtFinancialTotalIncomeMonthly, GetStringValue(data, 1));
                    AddDecimalParameter(command, Constants.AtFinancialTotalIncomeAnnual, GetStringValue(data, 2));
                    command.Parameters.AddWithValue(Constants.AtFinancialIsClientIncomeMonthly, GetStringValue(data, 3)?.Trim('"'));
                    command.Parameters.AddWithValue(Constants.AtFinancialEmploymentStatus, GetStringValue(data, 4)?.Trim('"'));
                    command.Parameters.AddWithValue(Constants.AtFinancialPublicAssistance, GetStringValue(data, 5)?.Trim('"'));
                    AddDecimalParameter(command, Constants.AtFinancialEmploymentSalaryWages, GetStringValue(data, 6));
                    AddDecimalParameter(command, Constants.AtFinancialUnemploymentBenefits, GetStringValue(data, 7));
                    AddDecimalParameter(command, Constants.AtFinancialVeteransBenefits, GetStringValue(data, 8));
                    AddDecimalParameter(command, Constants.AtFinancialSsi, GetStringValue(data, 9));
                    AddDecimalParameter(command, Constants.AtFinancialSsdi, GetStringValue(data, 10));
                    AddDecimalParameter(command, Constants.AtFinancialSsa, GetStringValue(data, 11));
                    AddDecimalParameter(command, Constants.AtFinancialGeneralAssistance, GetStringValue(data, 12));
                    AddDecimalParameter(command, Constants.AtFinancialTanf, GetStringValue(data, 13));
                    AddDecimalParameter(command, Constants.AtFinancialFoodStamps, GetStringValue(data, 14));
                    AddDecimalParameter(command, Constants.AtFinancialStateDisability, GetStringValue(data, 15));
                    AddDecimalParameter(command, Constants.AtFinancialLongTermDisability, GetStringValue(data, 16));
                    AddDecimalParameter(command, Constants.AtFinancialGift, GetStringValue(data, 17));
                    AddDecimalParameter(command, Constants.AtFinancialRetirement, GetStringValue(data, 18));
                    AddDecimalParameter(command, Constants.AtFinancialAlimony, GetStringValue(data, 19));
                    AddDecimalParameter(command, Constants.AtFinancialInvestment, GetStringValue(data, 20));
                    AddDecimalParameter(command, Constants.AtFinancialWorkersCompensation, GetStringValue(data, 21));
                    command.Parameters.AddWithValue(Constants.AtFinancialOther1, GetStringValue(data, 22)?.Trim('"'));
                    AddDecimalParameter(command, Constants.AtFinancialOtherAmount1, GetStringValue(data, 23));
                    command.Parameters.AddWithValue(Constants.AtFinancialOther2, GetStringValue(data, 24)?.Trim('"'));
                    AddDecimalParameter(command, Constants.AtFinancialOtherAmount2, GetStringValue(data, 25));
                    command.Parameters.AddWithValue(Constants.AtFinancialOther3, GetStringValue(data, 26)?.Trim('"'));
                    AddDecimalParameter(command, Constants.AtFinancialOtherAmount3, GetStringValue(data, 27));
                    command.Parameters.AddWithValue(Constants.AtFinancialHasNoSourceOfIncome, GetStringValue(data, 28)?.Trim('"'));
                    AddDecimalParameter(command, Constants.AtFinancialHouseholdIncome, GetStringValue(data, 29));
                    command.Parameters.AddWithValue(Constants.AtFinancialIsHouseholdIncomeMonthly, GetStringValue(data, 30)?.Trim('"'));
                    AddIntParameter(command, Constants.AtFinancialPeopleInHousehold, GetStringValue(data, 31));
                    AddIntParameter(command, Constants.AtFinancialChildrenInHousehold, GetStringValue(data, 32));
                    command.Parameters.AddWithValue(Constants.AtFinancialHivPositiveInHousehold, GetStringValue(data, 33)?.Trim('"'));
                    command.Parameters.AddWithValue(Constants.AtFinancialHouseholdPovertyLevelbyGroup, GetStringValue(data, 34)?.Trim('"'));
                    command.Parameters.AddWithValue(Constants.AtFinancialHouseholdPovertyLevel, GetStringValue(data, 35)?.Trim('"'));
                    AddDecimalParameter(command, Constants.AtFinancialFamilyIncome, GetStringValue(data, 36));
                    command.Parameters.AddWithValue(Constants.AtFinancialIsFamilyIncomeMonthly, GetStringValue(data, 37)?.Trim('"'));
                    AddIntParameter(command, Constants.AtFinancialPeopleInFamily, GetStringValue(data, 38));
                    command.Parameters.AddWithValue(Constants.AtFinancialFamilyPovertyLevel, GetStringValue(data, 39)?.Trim('"'));
                    command.Parameters.AddWithValue(Constants.AtFinancialOwnsHouse, GetStringValue(data, 40)?.Trim('"'));
                    command.Parameters.AddWithValue(Constants.AtFinancialOwnsCar, GetStringValue(data, 41)?.Trim('"'));
                    command.Parameters.AddWithValue(Constants.AtFinancialHasOtherAssets, GetStringValue(data, 42)?.Trim('"'));
                    AddDecimalParameter(command, Constants.AtFinancialOtherAssets, GetStringValue(data, 43));
                    command.Parameters.AddWithValue(Constants.AtFinancialLastSavedDate, GetStringValue(data, 44)?.Trim('"'));
                    command.Parameters.AddWithValue(Constants.AtFinancialCreateSource, GetStringValue(data, 45)?.Trim('"'));
                    command.Parameters.AddWithValue(Constants.AtDownloadDate, DateTime.Parse(Constants.TwoThreeFour));
                    command.Parameters.AddWithValue(Constants.AtExtracted, Constants.ExtractedCode);
                    command.Parameters.AddWithValue(Constants.AtExtractionDate, DateTime.Parse(Constants.TwoThreeFour));
                    command.Parameters.AddWithValue(Constants.AtCmsMatch, Constants.CmsMatchCode);
                    command.Parameters.AddWithValue(Constants.AtCmsMatchDate, DateTime.Parse(Constants.TwoThreeFour));
                    command.Parameters.AddWithValue(Constants.AtCreatedBy, Constants.CreatedBy);
                    command.Parameters.AddWithValue(Constants.AtCreatedOn, DateTime.Now); // Or provide the appropriate DateTime value

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                error = $"{Constants.ErrorInsertingClientParametersIntoTheTable}{ex.Message}\n{ex.StackTrace}";
                MessageBox.Show(error);
                Log($"{ex.Message}", Constants.Error, Constants.DlFinancials, Constants.Uploadct); // Assuming fileName is accessible here
                if (ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
                throw;// Log the error
            }
        }
        private void AddDecimalParameter(SqlCommand command, string parameterName, string value)// parse decimaL value
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (decimal.TryParse(value, out decimal decimalValue))
                    {
                        command.Parameters.AddWithValue(parameterName, decimalValue);
                    }
                    else
                    {
                        // Log or handle the invalid value
                        MessageBox.Show($@"{Constants.InvalidDecimalValue}{value}");
                        command.Parameters.AddWithValue(parameterName, DBNull.Value);
                    }
                }
                else
                {
                    command.Parameters.AddWithValue(parameterName, DBNull.Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void AddIntParameter(SqlCommand command, string parameterName, string value)// add parameters
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (int.TryParse(value, out int intValue))
                    {
                        command.Parameters.AddWithValue(parameterName, intValue);
                    }
                    else
                    {
                        // Log or handle the invalid value
                        MessageBox.Show($@"{Constants.InvalidIntegerValue}{value}");
                        command.Parameters.AddWithValue(parameterName, DBNull.Value);
                    }
                }
                else
                {
                    command.Parameters.AddWithValue(parameterName, DBNull.Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        // Utility method to safely retrieve string values from the data array
        private string GetStringValue(string[] data, int index)// convert to string
        {
            try
            {
                if (index >= 0 && index < data.Length)
                {
                    return data[index];
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public int InsertClients(XmlDocument xmlDoc, int batchId, SqlConnection connection, string xmlFilePath, bool value)// insert clients into database from Xml
        {
            string fileName = Path.GetFileName(xmlFilePath);
            int insertedCount = 0;
            try
            {
                errorOccurred = false;
                // Create a command for the stored procedure within the transaction
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                var procedure = value ? Constants.InsertCtClientsFromXmlPhiMaskingTest : Constants.InsertCtClientsFromXml;
                cmd.CommandText = procedure;
                cmd.CommandTimeout = 120;

                // Set the parameters for the stored procedure
                SqlParameter xmlDataParam = new SqlParameter(Constants.XmlData, SqlDbType.Xml)
                {
                    Value = new SqlXml(new XmlTextReader(new StringReader(xmlDoc.OuterXml)))
                };
                cmd.Parameters.Add(xmlDataParam);

                cmd.Parameters.AddWithValue(Constants.AtBatchid, batchId);

                // Execute the command to insert clients
                cmd.ExecuteNonQuery();

                // Return the number of inserted clients
                return insertedCount++;
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                var st = new StackTrace(ex, true);
                var frame = (st.GetFrames() ?? throw new InvalidOperationException()).FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame?.GetFileLineNumber() ?? 0;
                LogError(ex.Message, ex.StackTrace, nameof(InsertClients), fileName, lineNumber,Constants.ClientTrackCode);
                if (ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return 0;
                }
                LogError($"{ex.Message}", fileName); // Handle the exception within the transaction (e.g., log it)
                if (ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                }
                throw;
            }
        }

        public int InsertEligibilityDocuments(XmlDocument xmlDoc, int batchId, SqlConnection connection, string xmlFilePath)// insertion of eligibility document from xml file
        {
            int insertedCount = 0;
            string fileName = Path.GetFileName(xmlFilePath);
            try
            {
                errorOccurred = false;
                // Process each Client node in the XML document
                foreach (XmlNode clientNode in xmlDoc.SelectNodes(Constants.BkslashClient))
                {
                    XmlAttribute ariesIdAttribute = clientNode.Attributes[Constants.AriesId];
                    if (ariesIdAttribute != null && int.TryParse(ariesIdAttribute.Value, out int ariesId))
                    {
                        // Check if the Client node contains EligibilityDocument nodes
                        if (clientNode.SelectNodes(Constants.EligibilityDocument).Count > 0)
                        {
                            XmlNode agencySpecificsNode = clientNode.SelectSingleNode(Constants.AgencySpecifics);
                            if (agencySpecificsNode != null && agencySpecificsNode.Attributes != null)
                            {
                                int agencyClientId = int.Parse(agencySpecificsNode.Attributes[Constants.AgencyClientId1].Value);

                                // Check if the AgencyClientID and AriesID pair should be inserted
                                if (ShouldInsertAgencyClient(ariesId, xmlDoc))
                                {
                                    if (ErrorOccurred)
                                    {
                                        MessageBox.Show(Constants.ErrorOccurred);
                                        break;
                                    }
                                    // Process each EligibilityDocument node within the Client node
                                    foreach (XmlNode eligibilityNode in clientNode.SelectNodes(Constants.EligibilityDocument))
                                    {
                                        string documentType = GetAttributeValue(eligibilityNode, Constants.DocumentType);
                                        DateTime? documentDate = GetNullableDateTimeAttributeValue(eligibilityNode, Constants.DocumentDate);
                                       
                                        DateTime? obtainDate = GetNullableDateTimeAttributeValue(eligibilityNode, Constants.ObtainDate);
                                        
                                        DateTime? expireDate = GetNullableDateTimeAttributeValue(eligibilityNode, Constants.ExpireDate);
                                        
                                        string source = GetAttributeValue(eligibilityNode, Constants.Source);
                                        string notes = GetAttributeValue(eligibilityNode, Constants.Notes);

                                        // Prepare and execute SQL command to insert into EligibilityDocuments table within the transaction
                                        using (SqlCommand insertCmd = new SqlCommand(Constants.CtClientsEligibilityDocQuery, GetConnection()))
                                        {
                                            // Set SQL parameters based on the values extracted from the XML document
                                            insertCmd.Parameters.AddWithValue(Constants.AtDocumentType, string.IsNullOrEmpty(documentType) ? (object)DBNull.Value : documentType);
                                            insertCmd.Parameters.AddWithValue(Constants.AtDocumentDate, documentDate ?? (object)DBNull.Value);
                                            insertCmd.Parameters.AddWithValue(Constants.AtObtainDate, obtainDate ?? (object)DBNull.Value);
                                            insertCmd.Parameters.AddWithValue(Constants.AtExpireDate, expireDate ?? (object)DBNull.Value);
                                            insertCmd.Parameters.AddWithValue(Constants.AtSource, string.IsNullOrEmpty(source) ? (object)DBNull.Value : source);
                                            insertCmd.Parameters.AddWithValue(Constants.AtNotesCaps, string.IsNullOrEmpty(notes) ? (object)DBNull.Value : notes);
                                            insertCmd.Parameters.AddWithValue(Constants.AtBatchid, batchId);
                                            insertCmd.Parameters.AddWithValue(Constants.AtAgencyClientId1, agencyClientId);
                                            insertCmd.Parameters.AddWithValue(Constants.AtAriesId, ariesId);
                                            insertCmd.Parameters.AddWithValue(Constants.AtCreatedBy, Constants.CreatedBy); // Assuming a constant value for CreatedBy
                                            insertCmd.Parameters.AddWithValue(Constants.AtCreatedOn, DateTime.Now); // Current date/time
                                            insertCmd.Parameters.AddWithValue(Constants.AtEligibilityDocId, insertedCount % 3 + 1); // Incremental EligibilityDocumentID

                                            // Execute the insert command
                                            insertCmd.ExecuteNonQuery();
                                            insertedCount++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                // Log error for the main insertion process
                MessageBox.Show(Constants.ErrorInsertingEligibilityDocuments + ex.Message);
                LogError($"{ex.Message}", fileName);
                if (ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                }
                // Return zero inserted count to indicate failure
                return 0;
            }
            return insertedCount;
        }

        // Helper method to determine if AgencyClientID and AriesID pair should be inserted
        private bool ShouldInsertAgencyClient(int ariesId, XmlDocument xmlDoc)
        {
            try
            {
                errorOccurred = false;
                // Check if the AgencyClientID and AriesID pair has associated EligibilityDocument nodes in the XML
                XmlNodeList eligibilityNodes = xmlDoc.SelectNodes(string.Format(Constants.ClientariesIdEligibilityDocument, ariesId));

                return eligibilityNodes != null && eligibilityNodes.Count > 0;
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private string GetAttributeValue(XmlNode node, string attributeName)// getting value
        {
            try
            {
                if (node != null && node.Attributes != null && node.Attributes[attributeName] != null)
                {
                    return node.Attributes[attributeName].Value;
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        // Helper method to parse nullable DateTime attribute value from XML node
        private DateTime? GetNullableDateTimeAttributeValue(XmlNode node, string attributeName) // to get correct time format
        {
            try
            {
                string attributeValue = GetAttributeValue(node, attributeName);
                if (!string.IsNullOrEmpty(attributeValue) && DateTime.TryParse(attributeValue, out DateTime result))
                {
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public int InsertServiceLineItems(XmlDocument xmlDoc, int batchId, SqlConnection connection, string xmlFilePath)// insertion of service table from xml file
        {
            int insertedCount = 0;
            string fileName = Path.GetFileName(xmlFilePath);
            try
            {
                errorOccurred = false;
                // Process each ServiceLineItem node in the XML document
                foreach (XmlNode serviceNode in xmlDoc.SelectNodes(Constants.BkslashServiceLineItem))
                {
                    // Create SqlCommand for the stored procedure within the transaction
                    using (SqlCommand insertCmd = new SqlCommand(Constants.InsertServiceLineItemFromXmlPhi, GetConnection()))
                    {
                        insertCmd.CommandType = CommandType.StoredProcedure;
                        string clientAriesId = GetAttributeValue(serviceNode, Constants.ClientAriesId);
                        if (clientAriesId == null)
                        {
                            insertCmd.Parameters.AddWithValue(Constants.AtClientAriesId, DBNull.Value);
                        }
                        else
                        {
                            insertCmd.Parameters.AddWithValue(Constants.AtClientAriesId, clientAriesId);
                        }
                        // Set parameters for the stored procedure based on node attributes
                        insertCmd.Parameters.AddWithValue(Constants.AtClientUrnExt, GetAttributeValue(serviceNode, Constants.ClientUrnExt));
                        insertCmd.Parameters.AddWithValue(Constants.AtSiteName, GetAttributeValue(serviceNode, Constants.SiteName));
                        insertCmd.Parameters.AddWithValue(Constants.AtStaffLogin, GetAttributeValue(serviceNode, Constants.StaffLogin));
                        insertCmd.Parameters.AddWithValue(Constants.AtContractName, GetAttributeValue(serviceNode, Constants.ContractNamesmall));
                        insertCmd.Parameters.AddWithValue(Constants.AtServiceDateCaps, GetDateTimeValue(serviceNode, Constants.ServiceDatesmall));
                        insertCmd.Parameters.AddWithValue(Constants.AtProgram, GetAttributeValue(serviceNode, Constants.Program));
                        insertCmd.Parameters.AddWithValue(Constants.AtPrimaryService, GetAttributeValue(serviceNode, Constants.PrimaryServiceSmall));
                        insertCmd.Parameters.AddWithValue(Constants.AtSecondaryService, GetAttributeValue(serviceNode, Constants.SecondaryServiceSmall));
                        insertCmd.Parameters.AddWithValue(Constants.AtSubservice, GetAttributeValue(serviceNode, Constants.Subservice));
                        insertCmd.Parameters.AddWithValue(Constants.AtUnitsOfService, GetDecimalValue(serviceNode, Constants.UnitsOfService));
                        insertCmd.Parameters.AddWithValue(Constants.AtRateForUnitOfService, GetDecimalValue(serviceNode, Constants.RateForUnitOfService));
                        insertCmd.Parameters.AddWithValue(Constants.AtMeasurementUnit, GetAttributeValue(serviceNode, Constants.MeasurementUnit));
                        insertCmd.Parameters.AddWithValue(Constants.AtTotalCost, GetDecimalValue(serviceNode, Constants.TotalCost));
                        insertCmd.Parameters.AddWithValue(Constants.AtNotesCaps, GetAttributeValue(serviceNode, Constants.Notes));
                        insertCmd.Parameters.AddWithValue(Constants.AtBatchid, batchId);
                        insertCmd.Parameters.AddWithValue(Constants.AtCreatedBy, Constants.CreatedBy); // Assuming a constant value for CreatedBy
                        insertCmd.Parameters.AddWithValue(Constants.AtActualMinutesSpentCaps, GetAttributeValueOrDefault<object>(serviceNode, Constants.ActualTimeSpentMinutes, DBNull.Value));
                        insertCmd.Parameters.AddWithValue(Constants.AtCreatedOn, DateTime.Now);

                        // Extract and set ServiceID
                        string serviceId = ExtractServiceId(serviceNode);
                        if (ErrorOccurred)
                        {
                            MessageBox.Show(Constants.ErrorOccurred);
                            break;
                        }
                        if (!string.IsNullOrEmpty(serviceId))
                        {
                            insertCmd.Parameters.AddWithValue(Constants.AtServiceId, serviceId);
                        }
                        insertCmd.ExecuteNonQuery();
                        insertedCount++; // Increment inserted count for each service line item
                    }
                }
            }

            catch (Exception ex)
            {
                errorOccurred = true;
                // Log error for the main insertion process
                LogError($"{Constants.ErrorInsertingServiceLineItems}{ex.Message}", fileName);
                if (ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                }
                // Return zero inserted count to indicate failure
            }
            return insertedCount;
        }

        private T GetAttributeValue<T>(XmlNode node, string attributeName)// getting value to the XMl nodes
        {
            try
            {
                if (node == null || string.IsNullOrEmpty(attributeName))// 
                {
                    return default;
                }

                XmlNode attributeNode = node.Attributes.GetNamedItem(attributeName);
                if (attributeNode != null && !string.IsNullOrEmpty(attributeNode.Value))
                {
                    return (T)Convert.ChangeType(attributeNode.Value, typeof(T));
                }
                return default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return default;
            }
        }
        private T GetAttributeValueOrDefault<T>(XmlNode node, string attributeName, T defaultValue)// Getting or setting values to the XMl nodes
        {
            try
            {
                if (node == null || string.IsNullOrEmpty(attributeName))
                {
                    return defaultValue;
                }

                XmlNode attributeNode = node.Attributes.GetNamedItem(attributeName);
                if (attributeNode != null && !string.IsNullOrEmpty(attributeNode.Value))
                {
                    return (T)Convert.ChangeType(attributeNode.Value, typeof(T));
                }
                return defaultValue;
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
                return defaultValue;
            }
        }
        private DateTime GetDateTimeValue(XmlNode node, string attributeName)// getting date value
        {
            try
            {
                string dateString = GetAttributeValue<string>(node, attributeName);
               
                if (!string.IsNullOrEmpty(dateString))
                {
                    if (DateTime.TryParse(dateString, out DateTime result))
                    {
                        return result;
                    }
                }
                return DateTime.MinValue; // or throw an exception if parsing fails
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return DateTime.MinValue;
            }
        }
        private decimal GetDecimalValue(XmlNode node, string attributeName)// getting decimal value
        {
            try
            {
                string decimalString = GetAttributeValue<string>(node, attributeName);
                
                if (!string.IsNullOrEmpty(decimalString) && decimal.TryParse(decimalString, out decimal result))
                {
                    return result;
                }
                return 0; // or throw an exception if parsing fails
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }

        public void LogError(string message, string xmlFilePath)// Looger table insertion for errors and completion
        {
            try
            {
                errorOccurred = false;
                if (errorLogged)
                    return; // Abort further logging if an error has already been logged

                string fileName = Path.GetFileName(xmlFilePath);
                string stackTrace = Environment.StackTrace;
                string functionName = new StackTrace(true).GetFrame(1).GetMethod().Name;
                int lineNumber = new StackTrace(true).GetFrame(1).GetFileLineNumber();

                try
                {
                    SqlCommand cmd = new SqlCommand(Constants.LoggingErrorQuery, GetConnection());
                    cmd.Parameters.AddWithValue(Constants.AtType, Constants.ErrorCode); // Error type
                    cmd.Parameters.AddWithValue(Constants.AtModule, Constants.Module); // Module name
                    cmd.Parameters.AddWithValue(Constants.AtStack, stackTrace);
                    cmd.Parameters.AddWithValue(Constants.AtMessage, message);
                    cmd.Parameters.AddWithValue(Constants.AtFilename, fileName);
                    cmd.Parameters.AddWithValue(Constants.AtLineNumber, lineNumber);
                    cmd.Parameters.AddWithValue(Constants.AtFunctionName, functionName);
                    cmd.Parameters.AddWithValue(Constants.AtComments, DBNull.Value);
                    cmd.Parameters.AddWithValue(Constants.AtCreatedBy, Constants.CreatedBy); // Assuming a specific user ID
                    cmd.Parameters.AddWithValue(Constants.AtCreatedOn, DateTime.Now);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    errorOccurred = true;
                    // Log an additional error if logging itself fails
                    MessageBox.Show(ex.Message);
                }

                errorLogged = true; // Set errorLogged flag to true after logging the first error
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
                return;
            }
        }
        private string ExtractServiceId(XmlNode serviceNode)// extracting variables according to the format
        {
            try
            {
                errorOccurred = false;
                string notes = GetAttributeValue(serviceNode, Constants.Notes);

                // Check if notes attribute starts with "{Constants.AtIdEqualTto}", "{Constants.AtIdEqualTtoCaps}", Constants.IdHyphen, or Constants.IdColon
                if (notes.StartsWith($"{Constants.AtIdEqualTto}") || notes.StartsWith($"{Constants.AtIdEqualTtoCaps}") || notes.StartsWith(Constants.IdHyphen) || notes.StartsWith(Constants.IdColon))
                {
                    // Find the index of the first occurrence of "=", "-", or ":"
                    int separatorIndex = notes.IndexOfAny(new char[] { Constants.EqualTo, Constants.Hyphen, Constants.Colon });

                    // If separatorIndex is found
                    if (separatorIndex != -1)
                    {
                        // Extract the service ID after the separator
                        string serviceId = notes.Substring(separatorIndex + 1).Trim(';');
                        return serviceId; // Return extracted service ID as string
                    }
                }

                throw new ArgumentException(Constants.Invalidserviceid);
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        // Method to log messages with log type provided by RWDEFileUploads
        public void Log(string message, int type, string baseFilename, int module)// insertion into log table
        {
            try
            {
                errorOccurred = false;
                {
                    SqlCommand command = new SqlCommand(Constants.InsertLog, GetConnection())
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue(Constants.AtType, type);
                    command.Parameters.AddWithValue(Constants.AtMessage, message);
                    command.Parameters.AddWithValue(Constants.AtModule, module);
                    command.Parameters.AddWithValue(Constants.AtFilename, baseFilename);
                    command.Parameters.AddWithValue(Constants.AtLineNumber, DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtFunctionName, DBNull.Value);
                    command.Parameters.AddWithValue(Constants.AtComments, DBNull.Value);

                    command.Parameters.AddWithValue(Constants.AtCreatedBy, 100);
                    command.Parameters.AddWithValue(Constants.AtCreatedOn, DateTime.Now);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show($@"{Constants.ErrorLoggingMessage}{ex.Message}");
            }
        }
        public void DeleteBatchochin(string batchId)// Delete All Values form all Ochin tables 
        {
            try
            {
                errorOccurred = false;

                using (SqlCommand command = new SqlCommand(Constants.DeleteOchinBatchDatas, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(Constants.AtBatchid, batchId);
                    command.CommandTimeout = 120;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                // Handle exception (logging, rethrow, etc.)
                MessageBox.Show(ex.Message);
            }
        }
        public void DeleteBatch(string batchId, string type)// Delete All Values form all tables 
        {
            try
            {
                errorOccurred = false;
                string storedProcedure = GetStoredProcedureByType(type);
               
                using (SqlCommand command = new SqlCommand(storedProcedure, GetConnection()))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(Constants.AtBatchid, batchId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                // Handle exception (logging, rethrow, etc.)
                throw new Exception($"{Constants.ErrorDeletingBatch}{batchId}", ex);
            }
        }
        private string GetStoredProcedureByType(string type)// to select the storeProcedure to call 
        {
            try
            {
                // Adjust the stored procedure name based on the batch type
                if (type == Constants.ClientTrack)
                {
                    return Constants.DeleteBatchData;
                }
                else if (type == Constants.Hcc)
                {
                    return Constants.DeleteHccBatchData;
                }
                else if (type == Constants.Ochin)
                {
                    return Constants.OchinDataDelete;
                }
                else
                {
                    return Constants.OchinDataDelete;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public DataTable GetAllBatches()// Get all Values from Batch Table
        {
            DataTable dataTable = new DataTable();
            try
            {
                errorOccurred = false;

                using (SqlCommand command = new SqlCommand(Constants.ViewAllBatchDatas, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                // Handle exception (logging, rethrow, etc.)
                throw new Exception(Constants.ErrorRetrievingBatchData, ex);
            }
            return dataTable;
        }

        public DataTable GetAllBatchesLoad()// Get all Values from Batch Table
        {
            DataTable dataTable = new DataTable();
            try
            {
                errorOccurred = false;

                using (SqlCommand command = new SqlCommand(Constants.ViewAllBatchDatasLoad, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                // Handle exception (logging, rethrow, etc.)
                throw new Exception(Constants.ErrorRetrievingBatchData, ex);
            }
            return dataTable;
        }

        public DataTable GetAllBatcheshcc()// Get all Values from Batch Table
        {
            DataTable dataTable = new DataTable();
            try
            {
                errorOccurred = false;

                using (SqlCommand command = new SqlCommand(Constants.ViewAllBatchDatasHcc, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                // Handle exception (logging, rethrow, etc.)
                throw new Exception(Constants.ErrorRetrievingBatchData, ex);
            }
            return dataTable;
        }

        public int GetNextBatchId(bool batchId) // To get next batch Id from the database
        {
            try
            {
                errorOccurred = false;

                SqlCommand command = new SqlCommand(Constants.GetNextBatchIdQuery, GetConnection());
                int lastBatchIdFromDb = (int)command.ExecuteScalar();

                if (batchId || !batchIdIncremented)
                {
                    lastBatchIdFromDb++;
                    batchIdIncremented = true;
                }

                return lastBatchIdFromDb;
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
                return 0;
            }
        }
        public DataTable GetParticularDataFromBatchTable(string batchType, DateTime fromDate, DateTime endDate)// extraction of particular batch from data
        {
            DataTable dataTable = new DataTable();

            try
            {
                errorOccurred = false;

                using (SqlCommand command = new SqlCommand(Constants.GetParticularBatchDatas, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(Constants.AtBatchType, batchType);
                    command.Parameters.AddWithValue(Constants.AtFromDate, fromDate);
                    command.Parameters.AddWithValue(Constants.AtEndDateCaps, endDate);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                // Handle exception (logging, rethrow, etc.)
                throw new Exception(Constants.ErrorRetrievingBatchData, ex);
            }
            return dataTable;
        }
        public List<string> GetAllBatchTypes()// get all batches type
        {
            List<string> batchTypeValues = new List<string>();
            try
            {
                errorOccurred = false;
                string query = Constants.GetAllBatchType;

                using (SqlCommand com = new SqlCommand(query, GetConnection()))
                {

                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            batchTypeValues.Add(reader[Constants.Value].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                // Optionally, log the exception
                MessageBox.Show(ex.Message);
            }
            return batchTypeValues;
        }

        public List<string> GetAllBatchTypesHcc()// get all batches type except Client Track
        {
            List<string> batchTypeValues = new List<string>();
            try
            {
                errorOccurred = false;
                string query = Constants.GetAllBatchTypeHcc;

                using (SqlCommand com = new SqlCommand(query, GetConnection()))
                {

                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            batchTypeValues.Add(reader[Constants.Value].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                // Optionally, log the exception
                MessageBox.Show(ex.Message);
            }

            return batchTypeValues;
        }

        public List<string> GetAllBatchTypesview() // get all batches type
        {
            List<string> batchTypeValues = new List<string>();
            try
            {
                errorOccurred = false;
                string query = Constants.GetallbatchtypEview;
                {
                    using (SqlCommand com = new SqlCommand(query, GetConnection()))
                    {

                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                batchTypeValues.Add(reader[Constants.Value].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                // Optionally, log the exception
                MessageBox.Show(ex.Message);
            }

            return batchTypeValues;
        }
        public DataTable GetParticularConversionDatas(string batchType, DateTime fromDate, DateTime endDate)// retrieve data for according to the start and end date
        {
            DataTable dataTable = new DataTable();
            try
            {
                errorOccurred = false;
                using (SqlCommand command = new SqlCommand(Constants.GetParticularConversionDatas, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(Constants.AtBatchType, batchType);
                    command.Parameters.AddWithValue(Constants.AtFromDate, fromDate);
                    command.Parameters.AddWithValue(Constants.AtEndDateCaps, endDate);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                // Handle exception (logging, rethrow, etc.)
                MessageBox.Show(ex.Message);
            }
            return dataTable;
        }
        public DataTable GetParticularnGenerationDatas(string batchType, DateTime fromDate, DateTime endDate)// get generation data according to start and end time
        {
            DataTable dataTable = new DataTable();
            try
            {
                errorOccurred = false;

                using (SqlCommand command = new SqlCommand(Constants.GetParticularnGenerationDatasConversionXml, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(Constants.AtBatchType, batchType);
                    command.Parameters.AddWithValue(Constants.AtFromDate, fromDate);
                    command.Parameters.AddWithValue(Constants.AtEndDateCaps, endDate);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                // Handle exception (logging, rethrow, etc.)
                throw new Exception(Constants.ErrorRetrievingBatchData, ex);
            }
            return dataTable;
        }
        public DataTable GetParticularnGenerationDatasconversion(string batchType, DateTime fromDate, DateTime endDate)// get generation data according to start and end time
        {
            DataTable dataTable = new DataTable();
            try
            {
                errorOccurred = false;
                using (SqlCommand command = new SqlCommand(Constants.GetParticularnGenerationDatasConversion, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(Constants.AtBatchType, batchType);
                    command.Parameters.AddWithValue(Constants.AtFromDate, fromDate);
                    command.Parameters.AddWithValue(Constants.AtEndDateCaps, endDate);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                // Handle exception (logging, rethrow, etc.)
                throw new Exception(Constants.ErrorRetrievingBatchData, ex);
            }
            return dataTable;
        }

        public DataTable GetParticularGenerationDatasHcc(string batchType, DateTime fromDate, DateTime endDate)// get generation data according to start and end time
        {
            DataTable dataTable = new DataTable();

            try
            {
                errorOccurred = false;
                using (SqlCommand command = new SqlCommand(Constants.GetParticularnGenerationDatasConversionHcc, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(Constants.AtBatchType, batchType);
                    command.Parameters.AddWithValue(Constants.AtFromDate, fromDate);
                    command.Parameters.AddWithValue(Constants.AtEndDateCaps, endDate);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                // Handle exception (logging, rethrow, etc.)
                throw new Exception(Constants.ErrorRetrievingBatchData, ex);
            }
            return dataTable;
        }
        public DataTable GetAllContractLists()// get data for allcontractsid
        {
            // Create a new DataTable to store the results
            DataTable dataTable = new DataTable();

            // Define the name of the stored procedure
            string storedProcedureName = Constants.GetAllContractLists; // Change to the actual stored procedure name

            try
            {
                errorOccurred = false;

                // Use a using statement to ensure the SqlCommand is properly disposed of after use
                using (SqlCommand com = new SqlCommand(storedProcedureName, GetConnection()))
                {
                    // Specify that the SqlCommand is a stored procedure
                    com.CommandType = CommandType.StoredProcedure;

                    // Use a using statement to ensure the SqlDataAdapter is properly disposed of after use
                    using (SqlDataAdapter adapter = new SqlDataAdapter(com))
                    {
                        // Fill the DataTable with the results of the stored procedure
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                // Catch any exceptions and write the error message to the console
                MessageBox.Show(ex.Message);
            }
            // Return the populated DataTable
            return dataTable;
        }
        public DataTable GetAllServiceLists()// to retrieve the service list data
        {
            DataTable dataTable = new DataTable();
            string query = Constants.SpGetTopServiceCodeSetup;
            try
            {
                errorOccurred = false;

                using (SqlCommand com = new SqlCommand(query, GetConnection()))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(com))
                    {
                        adapter.Fill(dataTable);
                    }
                }
                if (dataTable.Rows.Count == 0)
                {
                    MessageBox.Show(Constants.QueryExecutedButNoDataFound);
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
            }
            return dataTable;
        }

        public List<Dictionary<string, string>> GetServices(int batchId) // Get All Service Values from ServiceGenerator Procedure
        {
            try
            {
                errorOccurred = false;
                var results = new List<Dictionary<string, string>>();
                var insertData = new List<SqlParameter[]>();

                using (SqlCommand command = new SqlCommand(Constants.ServiceXmlGeneration, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(Constants.AtBatchid, batchId);
                    command.CommandTimeout = 120; // Extend timeout

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var row = new Dictionary<string, string>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[reader.GetName(i)] = reader[i].ToString();
                            }
                            results.Add(row);

                            // Collect data for batch insert
                            string clientId = (string)reader[Constants.Clntid]; // Replace with actual column name
                            DateTime date = DateTime.Now;

                            insertData.Add(new SqlParameter[]
                            {
                                    new SqlParameter(Constants.AtClientid, clientId),
                                    new SqlParameter(Constants.AtDatetime, date)
                            });
                        }
                    }
                }

                // Batch insert collected data
                if (insertData.Count > 0)
                {

                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        foreach (var parameters in insertData)
                        {
                            using (SqlCommand cmd = new SqlCommand(Constants.InsertXmlgeneratorTimeServices, connection, transaction))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddRange(parameters);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        transaction.Commit();
                    }
                }
                return results;
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public List<Dictionary<string, string>> GetServiceserror(int batchId) // Get All Service Values from ServiceGenerator Procedure
        {
            try
            {
                errorOccurred = false;
                var results = new List<Dictionary<string, string>>();
                var insertData = new List<SqlParameter[]>();

                using (SqlCommand command = new SqlCommand(Constants.ServicegeneratorError, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(Constants.AtBatchid, batchId);// 
                    command.CommandTimeout = 120;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var row = new Dictionary<string, string>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[reader.GetName(i)] = reader[i].ToString();
                            }
                            results.Add(row);

                            // Collect data for batch insert
                            int clientId = Convert.ToInt32(reader[Constants.Clntid]); // Replace with actual column name
                            DateTime date = DateTime.Now;

                            insertData.Add(new SqlParameter[]
                            {
                                    new SqlParameter(Constants.AtClientid, clientId),
                                    new SqlParameter(Constants.AtDatetime, date)
                            });
                        }
                    }
                }

                // Batch insert collected data
                if (insertData.Count > 0)
                {

                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        foreach (var parameters in insertData)
                        {
                            using (SqlCommand cmd = new SqlCommand(Constants.InsertXmlgeneratorTimeServices, connection, transaction))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddRange(parameters);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        transaction.Commit();
                    }
                }
                return results;
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public List<Dictionary<string, string>> GetXmlStructure() // Get All XmlServiceStructure Values from XmlStructure Table
        {
            try
            {
                errorOccurred = false;
                var results = new List<Dictionary<string, string>>();

                using (SqlCommand command = new SqlCommand(Constants.XmlStructureServiceValues, GetConnection()))
                {

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var row = new Dictionary<string, string>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[reader.GetName(i)] = reader[i].ToString();
                            }
                            results.Add(row);
                        }
                    }
                }
                return results;
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        // For future purpose
        public DataTable DeceasedClientsData()// get clients rows count from table
        {
            DataTable dataTable = new DataTable();
            string storedProcedureName = Constants.SpCreateDeceasedClientViewCount;

            try
            {
                errorOccurred = false;

                using (SqlCommand command = new SqlCommand(storedProcedureName, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
            }
            return dataTable;
        }

        public List<Dictionary<string, string>> GetClientFileXmlStructure() // Get All XmlClientStructure Values from XmlStructure Table
        {
            try
            {
                errorOccurred = false;
                var results = new List<Dictionary<string, string>>();

                using (SqlCommand command = new SqlCommand(Constants.XmlStructureClientValues, GetConnection()))
                {

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var row = new Dictionary<string, string>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[reader.GetName(i)] = reader[i].ToString();
                            }
                            results.Add(row);
                        }
                    }
                }
                return results;
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public List<Dictionary<string, string>> GetClients(int batchId) // get clients data
        {
            try
            {
                errorOccurred = false;
                var results = new List<Dictionary<string, string>>();

                using (SqlCommand command = new SqlCommand(Constants.ClientsXmlGeneration, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(Constants.AtBatchid, batchId);
                    command.CommandTimeout = 300;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var row = new Dictionary<string, string>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                var columnName = reader.GetName(i);
                                var value = reader[i];
                                if (value is bool b)
                                {
                                    row[columnName] = b ? Constants.One : Constants.Zero;
                                }
                                else
                                {
                                    row[columnName] = value.ToString();
                                }
                            }

                            results.Add(row);

                            using (SqlCommand cmd = new SqlCommand(Constants.InsertXmlgeneratorTimeClient, GetConnection()))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandTimeout = 300;
                                DateTime date = DateTime.Now;
                                cmd.Parameters.AddWithValue(Constants.AtClientid, reader[32]); // Convert clientid to int
                                cmd.Parameters.AddWithValue(Constants.AtDatetime, date);
                                cmd.Parameters.AddWithValue(Constants.AtBatchid, batchId);

                                // Execute the second command here, after the reader is done with the row data
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }

                return results;
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public List<Dictionary<string, string>> FetchSubClientValues(string clientid, int batchid, string storedProcedureName) // fetch particular client data values
        {
            try
            {
                errorOccurred = false;
                var result = new List<Dictionary<string, string>>();

                using (SqlCommand com = new SqlCommand(storedProcedureName, GetConnection()))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue(Constants.AtBatchid, batchid);
                    com.Parameters.AddWithValue(Constants.AtClientId, clientid);

                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var row = new Dictionary<string, string>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                var columnName = reader.GetName(i);
                                var value = reader[i];

                                if (value is bool)
                                {
                                    row[columnName] = (bool)value ? Constants.One : Constants.Zero;
                                }
                                else if (value is DateTime dateValue)
                                {
                                    // Format date as yyyy/MM/dd
                                    row[columnName] = dateValue.ToString(Constants.YyyyMmDdSlash);
                                }
                                else
                                {
                                    row[columnName] = value.ToString();
                                }
                            }
                            result.Add(row);
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                throw new Exception(Constants.ErrorRetrievingData, ex);
            }
        }
        public List<Dictionary<string, string>> FetchSubClientValuesFromMedC4(string clientid, int batchid)// fetch particular client Medical values
        {
            try
            {
                errorOccurred = false;
                return FetchSubClientValues(clientid, batchid, Constants.FetchSubClientDataFromMedCd4);// fetch particular client data values
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public List<Dictionary<string, string>> FetchSubClientValuesFromMedVl(string clientid, int batchid)// fetch particular client Medical values
        {
            try
            {
                errorOccurred = false;
                return FetchSubClientValues(clientid, batchid, Constants.FetchSubClientDataFromMedVl);// fetch particular client data values
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public List<Dictionary<string, string>> FetchSubClientValuesFromHivTest(string clientid, int batchid)// fetch particular client HIV test values
        {
            try
            {
                errorOccurred = false;
                return FetchSubClientValues(clientid, batchid, Constants.FetchSubClientDataFromHivTest);// fetch particular client data values
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public List<Dictionary<string, string>> FetchSubClientValuesFromInsur(string clientid, int batchid)// fetch particular client Insurance values
        {
            try
            {
                errorOccurred = false;
                return FetchSubClientValues(clientid, batchid, Constants.FetchSubClientDataFromInsur);// fetch particular client data values
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public List<Dictionary<string, string>> FetchSubClientValuesFromRace(string clientid, int batchid)// fetch particular client Race values
        {
            try
            {
                errorOccurred = false;
                return FetchSubClientValues(clientid, batchid, Constants.FetchSubClientDataFromRace);// fetch particular client data values
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public string InsertOrUpdateContract(int contractId, string contractName, DateTime startedDateTime, DateTime endedDateTime, string statusValue, string createdBy, DateTime createdOn)// insert and update into database table
        {
            string operation = "";
            try
            {
                errorOccurred = false;

                using (SqlCommand command = new SqlCommand(Constants.InsertOrUpdateContract, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue(Constants.AtContractId, contractId);
                    command.Parameters.AddWithValue(Constants.AtContractName, contractName);
                    command.Parameters.AddWithValue(Constants.AtStartedDateTime, startedDateTime);
                    command.Parameters.AddWithValue(Constants.AtEndedDateTime, endedDateTime);
                    command.Parameters.AddWithValue(Constants.AtStatus, statusValue);
                    command.Parameters.AddWithValue(Constants.AtCreatedBy, createdBy);
                    command.Parameters.AddWithValue(Constants.AtCreatedOn, createdOn);

                    // Add output parameter
                    SqlParameter outputParam = new SqlParameter(Constants.AtOperation, SqlDbType.NVarChar, 50) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(outputParam);

                    command.ExecuteNonQuery();

                    // Get the value of the output parameter
                    operation = outputParam.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                // Log the exception or handle it as per your application's error handling strategy
                MessageBox.Show($@"{Constants.ErrorInInsertOrUpdateContractMethod}{ex.Message}");
                throw; // Re-throw the exception to propagate it to the caller
            }
            return operation;
        }

        public void RenewContract(int contractId, string contractName, DateTime startedDateTime, DateTime endedDateTime, string statusValue, string createdBy, DateTime createdOn) // insert and update into database table
        {
            try
            {
                errorOccurred = false;

                using (SqlCommand command = new SqlCommand(Constants.RenewContract, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue(Constants.AtContractId, contractId);
                    command.Parameters.AddWithValue(Constants.AtContractName, contractName);
                    command.Parameters.AddWithValue(Constants.AtStartedDateTime, startedDateTime);
                    command.Parameters.AddWithValue(Constants.AtEndedDateTime, endedDateTime);
                    command.Parameters.AddWithValue(Constants.AtStatus, statusValue);
                    command.Parameters.AddWithValue(Constants.AtCreatedBy, createdBy);
                    command.Parameters.AddWithValue(Constants.AtCreatedOn, createdOn);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                // Log the exception or handle it as per your application's error handling strategy
                MessageBox.Show($@"{Constants.ErrorInInsertOrUpdateContractMethod}{ex.Message}");
                throw; // Re-throw the exception to propagate it to the caller
            }
        }

        public void ContractIdUpdateStatus(int contractId, string status) // to update the status of the particular ContractID 
        {
            try
            {
                errorOccurred = false;
                // Define the name of the stored procedure
                string storedProcedureName = Constants.UpdateContractStatus;

                // Use a using statement to ensure the SqlCommand is properly disposed of after use
                using (SqlCommand command = new SqlCommand(storedProcedureName, GetConnection()))
                {
                    // Specify that the SqlCommand is a stored procedure
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the SqlCommand
                    command.Parameters.AddWithValue(Constants.AtContractId, contractId);
                    command.Parameters.AddWithValue(Constants.AtStatus, status);

                    // Open the database connection

                    // Execute the stored procedure
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                // Show a message box with the error message
                MessageBox.Show(ex.Message);
            }
        }
        public void ContractIdEdit(int contractId, String contractName, Object startedDateTime, Object endedDateTime, string status)// to modify particular contract id from contract list
        {
            try
            {
                errorOccurred = false;
                using (SqlCommand command = new SqlCommand(Constants.UpdateContract, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(Constants.AtContractId, contractId);
                    command.Parameters.AddWithValue(Constants.AtContractName, contractName);
                    command.Parameters.AddWithValue(Constants.AtStartedDateTime, startedDateTime);
                    command.Parameters.AddWithValue(Constants.AtEndedDateTime, endedDateTime);
                    command.Parameters.AddWithValue(Constants.AtStatus, status);
                    command.Parameters.AddWithValue(Constants.AtCreatedBy, Constants.Sakkusmall);
                    command.Parameters.AddWithValue(Constants.AtCreatedOn, DateTime.Now);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
            }
        }
        public void ServiceCodeIdUpdateStatus(int serviceCodeId, string status)// updating service after editing in service code setup
        {
            try
            {
                errorOccurred = false;
                // Define the name of the stored procedure
                string storedProcedureName = Constants.UpdateServiceCodeStatus;

                // Use a using statement to ensure the SqlCommand is properly disposed of after use
                using (SqlCommand command = new SqlCommand(storedProcedureName, GetConnection()))
                {
                    // Specify that the SqlCommand is a stored procedure
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the SqlCommand
                    command.Parameters.AddWithValue(Constants.AtServiceCodeId, serviceCodeId);
                    command.Parameters.AddWithValue(Constants.AtStatus, status);

                    // Open the database connection

                    // Execute the stored procedure
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                // Show a message box with the error message
                MessageBox.Show(ex.Message);
            }
        }
        public void EditServiceCode(int serviceCodeId, string service, string hccExportToAries, string hccContractId, string hccPrimaryService, string hccSecondaryService, string hccSubservice, string unitsOfMeasure, decimal unitValue, string status)// edit service code data according to service id
        {
            try
            {
                errorOccurred = false;

                using (SqlCommand command = new SqlCommand(Constants.InsertOrUpdateServiceCode, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue(Constants.AtServiceCodeId, serviceCodeId);
                    command.Parameters.AddWithValue(Constants.AtService, service);
                    command.Parameters.AddWithValue(Constants.AtHccExportToAries, hccExportToAries);
                    command.Parameters.AddWithValue(Constants.AtHccContractId, hccContractId);
                    command.Parameters.AddWithValue(Constants.AtHccPrimaryService, hccPrimaryService);
                    command.Parameters.AddWithValue(Constants.AtHccSecondaryService, hccSecondaryService);
                    command.Parameters.AddWithValue(Constants.AtHccSubservice, hccSubservice);
                    command.Parameters.AddWithValue(Constants.AtUnitsOfMeasure, unitsOfMeasure);
                    command.Parameters.AddWithValue(Constants.AtUnitValue, unitValue);
                    command.Parameters.AddWithValue(Constants.AtStatus, status);

                    // Add output parameter
                    SqlParameter outputParameter = new SqlParameter
                    {
                        ParameterName = Constants.AtOperation,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 50,
                        Direction = ParameterDirection.Output,
                    };
                    command.Parameters.Add(outputParameter);

                    // Open the connection and execute the command

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
            }
        }
        // To handle the Insertion or Deletion o Service Code 
        public string InsertOrUpdateServiceCode(
            int serviceCodeId,
            string service,
            string hccExportToAries,
            int hccContractId,
            string hccPrimaryService,
            string hccSecondaryService,
            string hccSubservice,
            string unitsOfMeasure,
            decimal unitValue,
            string status)
        {
            try
            {
                errorOccurred = false;

                using (SqlCommand command = new SqlCommand(Constants.InsertOrUpdateServiceCode, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue(Constants.AtServiceCodeId, serviceCodeId);
                    command.Parameters.AddWithValue(Constants.AtService, service);
                    command.Parameters.AddWithValue(Constants.AtHccExportToAries, hccExportToAries);
                    command.Parameters.AddWithValue(Constants.AtHccContractId, hccContractId);
                    command.Parameters.AddWithValue(Constants.AtHccPrimaryService, hccPrimaryService);
                    command.Parameters.AddWithValue(Constants.AtHccSecondaryService, hccSecondaryService);
                    command.Parameters.AddWithValue(Constants.AtHccSubservice, hccSubservice);
                    command.Parameters.AddWithValue(Constants.AtUnitsOfMeasure, unitsOfMeasure);
                    command.Parameters.AddWithValue(Constants.AtUnitValue, unitValue);
                    command.Parameters.AddWithValue(Constants.AtStatus, status);

                    // Add output parameter
                    SqlParameter outputParameter = new SqlParameter
                    {
                        ParameterName = Constants.AtOperation,
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 50,
                        Direction = ParameterDirection.Output,
                    };
                    command.Parameters.Add(outputParameter);

                    // Open the connection and execute the command

                    command.ExecuteNonQuery();

                    // Return the value of the output parameter
                    return outputParameter.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                // Log or handle the error as needed
                MessageBox.Show($@"{Constants.AnErrorOccurred}{ex.Message}");
                return null; // or return an appropriate error value
            }
        }
        public DataTable GetActiveContracts()// get values from table of particular contract list
        {
            DataTable contracts = new DataTable();
            try
            {
                errorOccurred = false;

                // Define the name of the stored procedure
                string storedProcedureName = Constants.GetActiveContracts;

                // Use a using statement to ensure the SqlCommand is properly disposed of after use
                using (SqlCommand command = new SqlCommand(storedProcedureName, GetConnection()))
                {
                    // Specify that the SqlCommand is a stored procedure
                    command.CommandType = CommandType.StoredProcedure;

                    // Open the database connection

                    // Use a SqlDataAdapter to fill the DataTable with the results of the stored procedure
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(contracts);
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                // Log or handle the error as needed
                MessageBox.Show($@"{Constants.AnErrorOccurred}{ex.Message}");
            }

            // Return the populated DataTable
            return contracts;
        }
        public DataTable GetClientIDs(DateTime startDate, DateTime endDate)// get client id according to start and end date
        {
            try
            {
                errorOccurred = false;
                // Validate that the dates are within the SQL Server range
                if (startDate < new DateTime(1753, 1, 1)) startDate = new DateTime(1753, 1, 1);
                if (endDate > new DateTime(9999, 12, 31)) endDate = new DateTime(9999, 12, 31);

                DataTable clientIds = new DataTable();

                string query = Constants.GetClientIdsQuery;
                using (SqlCommand cmd = new SqlCommand(query, GetConnection()))
                {
                    cmd.Parameters.Add(new SqlParameter(Constants.AtStartDateCaps, SqlDbType.DateTime)).Value = startDate;
                    cmd.Parameters.Add(new SqlParameter(Constants.AtEndDateCaps, SqlDbType.DateTime)).Value = endDate;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(clientIds);
                    }
                }
                return clientIds;
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public DataTable GetServiceIDs(DateTime startDate, DateTime endDate)// get client id according to start and end date
        {
            try
            {
                errorOccurred = false;
                // Validate that the dates are within the SQL Server range
                if (startDate < new DateTime(1753, 1, 1)) startDate = new DateTime(1753, 1, 1);
                if (endDate > new DateTime(9999, 12, 31)) endDate = new DateTime(9999, 12, 31);

                DataTable serviceIds = new DataTable();

                string query = Constants.GetServiceIdsQuery;
                using (SqlCommand cmd = new SqlCommand(query, GetConnection()))
                {
                    cmd.Parameters.Add(new SqlParameter(Constants.AtStartDateCaps, SqlDbType.DateTime)).Value = startDate;
                    cmd.Parameters.Add(new SqlParameter(Constants.AtEndDateCaps, SqlDbType.DateTime)).Value = endDate;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(serviceIds);
                    }
                }
                return serviceIds;
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public DataTable GetFilteredDataFromDatabase(DateTime startDate, DateTime endDate) // to get deceased data accordinbg to date filter
        {
            try
            {
                errorOccurred = false;
                DataTable dataTable = new DataTable();
                string query = Constants.GetFilteredDataQuery;

                using (SqlCommand command = new SqlCommand(query, GetConnection()))
                {
                    command.Parameters.AddWithValue(Constants.AtStartDateCaps, startDate);
                    command.Parameters.AddWithValue(Constants.AtEndDateCaps, endDate);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public DataTable LoadDatafilter(DateTime startDate, DateTime endDate) // load data for monthly dashboard
        {
            DataTable dt = new DataTable();

            try
            {
                errorOccurred = false;

                string query = Constants.SpUploadDashboardReport; // Ensure this is the correct store procedure name

                using (SqlCommand cmd = new SqlCommand(query, GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Specify that it is a stored procedure

                    // Add start and end date parameters directly as DateTime
                    cmd.Parameters.AddWithValue(Constants.AtStartDateCaps, startDate);
                    cmd.Parameters.AddWithValue(Constants.AtEndDateCaps, endDate);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                // Handle exceptions (logging, rethrowing, etc.)// PUSH AGAIN
                MessageBox.Show(ex.Message);
            }

            return dt;
        }

        public DataTable LoadDatafilterServiceRecon(DateTime startDate, DateTime endDate, string filterType)// to load the service-recon for created and service date filter
        {
            DataTable dt = new DataTable();

            try
            {
                errorOccurred = false;

                // Execute the stored procedure to update HCCServices if needed
                using (SqlCommand updateCmd = new SqlCommand(Constants.UpdateHccServicesWithErrors, GetConnection()))
                {
                    updateCmd.CommandTimeout = 180;
                    updateCmd.CommandType = CommandType.StoredProcedure;
                    updateCmd.ExecuteNonQuery();
                }

                // Select query based on filter type
                string query;
                if (filterType == Constants.ServiceDateSp)
                {
                    query = Constants.ServiceReconServiceDateQuery;
                }
                else if (filterType == Constants.CreatedDate)
                {
                    query = Constants.ServiceReconCreatedDateQuery;
                }
                else
                {
                    query = Constants.ServiceReconBatchIdQuery;
                }

                using (SqlCommand cmd = new SqlCommand(query, GetConnection()))
                {
                    // Add parameters based on the filter type
                    if (filterType == Constants.ServiceDateSp || filterType == Constants.CreatedDate)
                    {
                        cmd.Parameters.AddWithValue(Constants.AtStartDateCaps, startDate);
                        cmd.Parameters.AddWithValue(Constants.AtEndDateCaps, endDate);
                    }
                    else if (filterType == Constants.BatchId)
                    {

                    }
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                throw new Exception(Constants.AnErrorOccurredWhileLoadingData, ex);
            }
            return dt;
        }
        public List<DataTable> LoadDatafilterhccreconBatchid(DateTime startDate, DateTime endDate, int[] batchids, String filterType)// to load the HCCRecon for Batch ID filter
        {
            List<DataTable> result = new List<DataTable>();
            List<int> noDataIds = new List<int>();
            try
            {
                errorOccurred = false;
                foreach (int onebatch in batchids)
                {
                    SqlCommand cmd = new SqlCommand(Constants.SpHccRecon, GetConnection())
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue(Constants.AtStartDateCaps, startDate);
                    cmd.Parameters.AddWithValue(Constants.AtEndDateCaps, endDate);
                    cmd.Parameters.AddWithValue(Constants.AtBatchid, onebatch);
                    cmd.Parameters.AddWithValue(Constants.AtType, filterType);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows == false)
                        {
                            noDataIds.Add(onebatch);
                        }
                        // Read each result set into a DataTable
                        DataTable table = new DataTable();
                        table.Load(reader);
                        result.Add(table);  // Add the DataTable to the result list
                        if (Array.IndexOf(batchids, onebatch) == batchids.Length - 1 && noDataIds.Count != 0)
                        {
                            MessageBox.Show(string.Join(",", noDataIds.ToArray()) + Constants.NodatafoundfortheseBatchids);// 
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
            }
            return result;
        }

        public DataTable CombineAllResults(List<DataTable> result)// to combine all the data for different BatchID
        {
            DataTable dt = result[0].Clone();
            try
            {
                // Create an empty table with the same structure
                foreach (var table in result)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        dt.ImportRow(row);  // Add each row from the result set
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }
        public DataTable LoadDatafilterhccrecon(DateTime startDate, DateTime endDate, String filterType)// to load the HCCRecon for created and service date filter
        {
            DataTable dt = new DataTable();

            try
            {
                errorOccurred = false;

                // Choose stored procedure based on filterType
                string query = Constants.SpHccRecon;

                using (SqlCommand cmd = new SqlCommand(query, GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Specify that it is a stored procedure

                    // Add start and end date parameters directly as DateTime
                    cmd.Parameters.AddWithValue(Constants.AtStartDateCaps, startDate);
                    cmd.Parameters.AddWithValue(Constants.AtEndDateCaps, endDate);
                    cmd.Parameters.AddWithValue(Constants.AtBatchid, 0);
                    cmd.Parameters.AddWithValue(Constants.AtType, filterType);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
            }
            return dt;
        }
        public DataTable LoadConfigurationfilter(DateTime startDate, DateTime endDate)// to get details of clients applied for services
        {
            DataTable dt = new DataTable();

            try
            {
                errorOccurred = false;

                string query = Constants.ClientDemographicsQuery; // Ordering by the minimum date in each group

                using (SqlCommand cmd = new SqlCommand(query, GetConnection()))
                {
                    cmd.Parameters.AddWithValue(Constants.AtStartDateCaps, startDate);
                    cmd.Parameters.AddWithValue(Constants.AtEndDateCaps, endDate);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                // Handle exceptions (logging, rethrowing, etc.)
                MessageBox.Show(ex.Message);
            }
            return dt;
        }
        public DataTable LoadManualUploadReport(DateTime startDate, DateTime endDate)// to get details of clients applied for services
        {
            DataTable dt = new DataTable();
            try
            {
                errorOccurred = false;

                string query = Constants.ManualUploadReport; // Ordering by the minimum date in each group

                using (SqlCommand cmd = new SqlCommand(query, GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(Constants.AtStartDateCaps, startDate);
                    cmd.Parameters.AddWithValue(Constants.AtEndDateCaps, endDate);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                // Handle exceptions (logging, rethrowing, etc.)
                MessageBox.Show(ex.Message);

                return dt;
            }
        }
        public DataTable LoadErrorlog(DateTime startDate, DateTime endDate)// load the error obtained while processing
        {
            DataTable dt = new DataTable();

            try
            {
                errorOccurred = false;

                string query = Constants.LoadLogErrorQuery; // Ordering by the minimum date in each group

                using (SqlCommand cmd = new SqlCommand(query, GetConnection()))
                {
                    cmd.Parameters.AddWithValue(Constants.AtStartDateCaps, startDate);
                    cmd.Parameters.AddWithValue(Constants.AtEndDateCaps, endDate);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                // Handle exceptions (logging, rethrowing, etc.)
                MessageBox.Show(ex.Message);
            }
            return dt;
        }

        public DataTable GetHccServices()// get created Services 
        {
            try
            {
                errorOccurred = false;
                DataTable hccServices = new DataTable();

                string query = Constants.GetHccServicesQuery;

                SqlDataAdapter adapter = new SqlDataAdapter(query, GetConnection());
                adapter.Fill(hccServices);
                return hccServices;
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public DataTable GetHccClients()// get client created date from table inserted
        {
            try
            {
                errorOccurred = false;
                DataTable hccClients = new DataTable();

                string query = Constants.GetHccClientsQuery;
                SqlDataAdapter adapter = new SqlDataAdapter(query, GetConnection());
                adapter.Fill(hccClients);
                return hccClients;
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        // Utility method to execute scalar queries

        public DataTable GetPieChartData(DateTime StartDate, DateTime EndDate)// to get the data of the PieChartt
        {
            DataTable dt = new DataTable();

            try
            {
                errorOccurred = false;

                string query = Constants.PieChartData;

                using (SqlCommand cmd = new SqlCommand(query, GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Specify that it is a stored procedure

                    // Add start and end date parameters directly as DateTime
                    cmd.Parameters.AddWithValue(Constants.AtStartDateCaps, StartDate);
                    cmd.Parameters.AddWithValue(Constants.AtEndDateCaps, EndDate);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
            }
            return dt;
        }
        public void ClearTables(int batchId)// This method clears or resets tables associated with the specified batch ID.
        {
            try
            {
                errorOccurred = false;

                using (SqlCommand command = new SqlCommand(Constants.AbortDelete, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(Constants.AtBatchid, batchId);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show($@"{Constants.ErrorClearingtables}{ex.Message}");
                throw; // Re-throw if you want to handle it in the calling method
            }
        }

        public void WriteClientCsvData(string Filepath) // to Write the CSV data of Clients
        {
            try
            {
                errorOccurred = false;
                // Getting BatchId for particular file to generate CSV
                int batchid = GetMaxXmlBatchId();
                if (ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
                // to execute the stored procedure
                using (SqlCommand cmd = new SqlCommand(Constants.Ctclientsmapping, GetConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(Constants.AtBatchid, batchid);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Create a StreamWriter to write to the CSV file
                        using (StreamWriter writer = new StreamWriter(Filepath))
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
                MessageBox.Show(Constants.CsVfilehasbeencreatedsuccessfullyat + Filepath);// 
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
            }
        }
        public void WriteServicesCsvData(string filePath)// to Write the CSV data of Services
        {
            try
            {
                errorOccurred = false;
                // Getting BatchId for particular file to generate CSV
                int batchid = GetMaxXmlBatchId();
                if (ErrorOccurred)
                {
                    MessageBox.Show(Constants.ErrorOccurred);
                    return;
                }
                // SQL query to execute the stored procedure

                // Call the stored procedure
                using (SqlCommand cmd = new SqlCommand(Constants.GetCtServicesForCsv, GetConnection()))
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
            catch (UnauthorizedAccessException)
            {
                errorOccurred = true;
                MessageBox.Show(Constants.Accessdeniedtothefolder, Constants.PermissionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Constants.Errorsp + ex.Message);
            }
        }

        public void InsertIntoDatabase(SqlConnection connection, SqlTransaction transaction, string hccTable, string errorMessage, string clientId, string sourceFileName, int batchId) // to insert the data into the database
        {
            try
            {
                errorOccurred = false;
                using (SqlCommand cmd = new SqlCommand(Constants.InsertIntoDatabaseQuery, connection, transaction))
                {
                    cmd.Parameters.AddWithValue(Constants.AtBatchid, batchId);
                    cmd.Parameters.AddWithValue(Constants.AtHccTable, hccTable);
                    cmd.Parameters.AddWithValue(Constants.AtErrorMessage, errorMessage);

                    // Check if clientId starts with "246_" and remove it if it does
                    string modifiedClientId = clientId.StartsWith(Constants.AgencyCode) ? clientId.Substring(4) : clientId;

                    cmd.Parameters.AddWithValue(Constants.AtClientId, modifiedClientId);
                    cmd.Parameters.AddWithValue(Constants.AtSourceFileName, sourceFileName);
                    cmd.CommandTimeout = 120;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                errorOccurred = true;
                // Handle SQL exceptions
                MessageBox.Show($@"{Constants.SqlError}{ex.Message}");
                transaction.Rollback(); // Rollback transaction if needed
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                // Handle any other exceptions
                MessageBox.Show($@"{Constants.Errorsp}{ex.Message}");
                transaction.Rollback(); // Rollback transaction if needed
            }
        }
        public DataTable FilterHccErrors(string sourceFileName)// filter HCCErrors as per the filename
        {
            DataTable dt = new DataTable();
            try
            {
                errorOccurred = false;

                using (SqlCommand command = new SqlCommand(Constants.Filtersourcefilename, GetConnection()))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(Constants.AtSourceFileName, sourceFileName);
                    command.ExecuteNonQuery();// Execute the SQL command to insert client data
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        try
                        {
                            // Fill the DataTable with the result of the stored procedure
                            adapter.Fill(dt);
                            return dt;
                        }
                        catch (Exception ex)
                        {
                            errorOccurred = true;
                            // Handle exceptions (e.g., logging)
                            MessageBox.Show(Constants.Errorsp + ex.Message);
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public object FormatStatus(string statusValue)// to get value of the ListId
        {
            try
            {
                errorOccurred = false;
                if (!string.IsNullOrEmpty(statusValue))
                {
                    string valueSelectQuery = Constants.ListValueXml;

                    using (SqlCommand com = new SqlCommand(valueSelectQuery, GetConnection()))
                    {
                        com.CommandType = CommandType.StoredProcedure;
                        com.Parameters.AddWithValue(Constants.AtListsId, statusValue);

                        var result = com.ExecuteScalar();
                        return result;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public int GetTotalRowsForBatch(int selectedBatchId) // Getting total rows from particular table
        {
            int totalRows = 0;
            try
            {
                errorOccurred = false;

                using (SqlCommand command = new SqlCommand(Constants.CountXml, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(Constants.AtBatchid, selectedBatchId);
                    totalRows = (int)command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show($@"{Constants.ErrorGettingTotalRows}  {ex.Message}");
            }
            return totalRows;
        }
        public void UpdateStatus(int batchId, int listId, DateTime startTime, DateTime endTime)// to update status in batch table
        {
            try
            {
                errorOccurred = false;
                if (listId == 20)
                {
                    // Define the SQL query to update the GenerationStartedAt column
                    string updateQuery = Constants.UpdateXmlClient;

                    using (SqlCommand command = new SqlCommand(updateQuery, GetConnection()))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        // Set the parameters
                        command.Parameters.AddWithValue(Constants.AtBatchid, batchId);
                        command.Parameters.AddWithValue(Constants.AtGenerationStartedAt, startTime);

                        command.Parameters.AddWithValue(Constants.AtGenerationEndedAt, endTime);

                        // Execute the SQL update command
                        command.ExecuteNonQuery();
                    }
                }
                else
                {
                    // Define the SQL query to update the GenerationEndedAt column
                    string updateQuery = $"@{Constants.UpdateStatusColumnQuery}";

                    using (SqlCommand command = new SqlCommand(updateQuery, GetConnection()))
                    {
                        // Set the parameters
                        command.Parameters.AddWithValue(Constants.AtBatchid, batchId);
                        command.Parameters.AddWithValue(Constants.AtGenerationEndedAt, endTime);

                        // Execute the SQL update command
                        command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
            }
        }
        public void UpdateServiceStatus(int batchId, int listId, DateTime startTime, DateTime endTime)// to update the status of service file
        {
            try
            {
                errorOccurred = false;
                if (listId == 20)
                {

                    // Define the SQL query to update the GenerationStartedAt column"
                    string updateQuery = Constants.UpdateXml;

                    using (SqlCommand command = new SqlCommand(updateQuery, GetConnection()))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Set the parameters
                        command.Parameters.AddWithValue(Constants.AtBatchid, batchId);
                        command.Parameters.AddWithValue(Constants.AtGenerationStartedAt, startTime);

                        command.Parameters.AddWithValue(Constants.AtGenerationEndedAt, endTime);

                        // Execute the SQL update command
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
            }
        }
        public int GetTotalRowsForBatchclient(int selectedBatchId)// Getting total rows from particular table
        {
            try
            {
                errorOccurred = false;
                int totalRows = 0;

                using (SqlCommand command = new SqlCommand(Constants.CountXmlClients, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 120;
                    command.Parameters.AddWithValue(Constants.AtBatchid, selectedBatchId);
                    totalRows = (int)command.ExecuteScalar();
                    //totalRows = 179;
                    return totalRows;
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show($@"{Constants.ErrorGettingTotalRows}  {ex.Message}");
            }
            return 0;
        }
        public void UpdateBatchStatusabort(int batchId, int status, string filename)// to update aborted batch status 
        {
            try
            {
                errorOccurred = false;

                // Construct the SQL UPDATE statement
                string query = Constants.UpdateXmlStatus;

                // Create and execute the SqlCommand
                using (SqlCommand command = new SqlCommand(query, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    // Add parameters to the command
                    command.Parameters.AddWithValue(Constants.AtStatus, status);
                    command.Parameters.AddWithValue(Constants.AtBatchid, batchId);
                    command.Parameters.AddWithValue(Constants.AtFilename, filename);

                    // Execute the update query
                    int rowsAffected = command.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(Constants.Errorupdatingbatchstatus, ex.Message);
                // Log or handle the exception appropriately
            }
        }
        public void UpdateBatchStatusTime(int batchId, int status, DateTime timestamp)// to upadte the status of the Batch
        {
            try
            {
                errorOccurred = false;

                using (SqlCommand command = new SqlCommand(Constants.UpdateBatchStatusQuery, GetConnection()))
                {
                    command.Parameters.AddWithValue(Constants.AtStatus, status);
                    command.Parameters.AddWithValue(Constants.AtTimestamp, timestamp);
                    command.Parameters.AddWithValue(Constants.AtBatchid, batchId);
                    command.ExecuteNonQuery();
                    // to delete the Aborted Batch data
                    ClearAbortedTables(batchId);
                    if (ErrorOccurred)
                    {
                        MessageBox.Show(Constants.ErrorOccurred);
                        return;
                    }

                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
            }
        }
        public void ClearAbortedTables(int batchId)// to delete the Aborted Batch data
        {
            try
            {
                errorOccurred = false;

                using (SqlCommand command = new SqlCommand(Constants.AbortConversionDelete, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(Constants.AtBatchid, batchId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show($@"{Constants.ErrorClearingtables}{ex.Message}");
                throw; // Re-throw if you want to handle it in the calling method
            }
        }
        public (DateTime? conversionStartedAt, DateTime? conversionEndedAt) GetCoversionTime(int selectedBatchId)// to get the conversion time of the Batch
        {
            try
            {
                errorOccurred = false;
                DateTime? conversionStartedAt = null;
                DateTime? conversionEndedAt = null;

                using (SqlCommand command = new SqlCommand(Constants.GetConversionTimeQuery, GetConnection()))
                {
                    command.Parameters.AddWithValue(Constants.AtBatchid, selectedBatchId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            conversionStartedAt = reader.IsDBNull(0) ? null : (DateTime?)reader.GetDateTime(0);
                            conversionEndedAt = reader.IsDBNull(1) ? null : (DateTime?)reader.GetDateTime(1);
                        }
                    }
                }
                return (conversionStartedAt, conversionEndedAt);
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
                return (null, null);
            }
        }
        public int GetTotalRows(int batchId)// getting total rows from particular tables
        {
            int totalRows = 0;
            try
            {
                errorOccurred = false;

                using (SqlCommand command = new SqlCommand($"@{Constants.GetTotalRowsForBatchQuery}", GetConnection()))
                {
                    command.Parameters.AddWithValue(Constants.AtBatchid, batchId);
                    totalRows = (int)command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
            }
            return totalRows;
        }
        public async Task MapCmsClientsAsync(int selectedBatchId)// to map the Cms Clients to Hcc Tables
        {
            try
            {
                errorOccurred = false;
                FrmConvertToHcc frmConvertToHcc = new FrmConvertToHcc();

                using (SqlCommand command = new SqlCommand(Constants.MapCmsClientstest, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Pass the selected BatchID to the stored procedure
                    command.Parameters.AddWithValue(Constants.AtBatchid, selectedBatchId);

                    // Get the total number of rows to be inserted
                    int totalRows = GetTotalRows(selectedBatchId);
                    if (ErrorOccurred)
                    {
                        MessageBox.Show(Constants.ErrorOccurred);
                        return;
                    }
                    // Initialize progress variables
                    int insertedRows = 0;
                    // Update progress textbox with initial progress information
                    await frmConvertToHcc.UpdateProgressAsync(insertedRows, totalRows);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (insertedRows < totalRows)
                        {
                            // Process each row
                            insertedRows++;

                            // Update progress bar and text box
                            await frmConvertToHcc.UpdateProgressAsync(insertedRows, totalRows);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
            }
        }
        public void UpdateBatch(int batchId, DateTime startTime, DateTime endTime, int allTotalRows)// Updating status and Time on Batch Table  
        {
            try
            {
                errorOccurred = false;
                allTotalRows++;

                // Define the SQL query to update the ConversionStartedAt and ConversionEndedAt columns
                string updateQuery = $"@{Constants.UpdateBatchConversionTimeQuery}";

                using (SqlCommand command = new SqlCommand(updateQuery, GetConnection()))
                {
                    // Set the parameters
                    command.Parameters.AddWithValue(Constants.AtBatchid, batchId);
                    command.Parameters.AddWithValue(Constants.AtConversionStartedAt, startTime);
                    command.Parameters.AddWithValue(Constants.AtConversionEndedAt, endTime);
                    command.Parameters.AddWithValue(Constants.AtAllTotalRows, allTotalRows - 1);

                    // Execute the SQL update command
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show($@"{Constants.Errorupdatingbatch}{ex.Message}");
                // Log or handle the exception appropriately
            }
        }
        public void UpdateBatchStatus(int selectedBatchId, int status)// Updating Batch Status
        {
            try
            {
                errorOccurred = false;

                // Construct the SQL UPDATE statement
                string query = Constants.UpdateBatchStatus;
                // Create and execute the SqlCommand
                using (SqlCommand command = new SqlCommand(query, GetConnection()))
                {
                    // Add parameters to the command
                    command.Parameters.AddWithValue(Constants.AtStatus, status);
                    command.Parameters.AddWithValue(Constants.AtBatchid, selectedBatchId);

                    // Execute the update query
                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(Constants.Errorupdatingbatchstatus, ex.Message);
            }
        }
        public async Task MapDlServicesAsync(int selectedBatchId)// to Map the DlServices to Hcc Tables
        {
            try
            {
                errorOccurred = false;
                FrmConvertToHcc frmConvertToHcc = new FrmConvertToHcc();

                using (SqlCommand command = new SqlCommand(Constants.MapDlServicesToHccServices, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Pass the selected BatchID to the stored procedure
                    command.Parameters.AddWithValue(Constants.AtBatchid, selectedBatchId);

                    // Get the total number of rows to be inserted
                    int totalRows = GetTotalRowsForBatchservices(selectedBatchId);
                    if (ErrorOccurred)
                    {
                        MessageBox.Show(Constants.ErrorOccurred);
                        return;
                    }

                    // Initialize progress variables
                    int insertedRows = 0;
                    // Update progress textbox with initial progress information
                    await frmConvertToHcc.UpdateProgressAsyncservices(insertedRows, totalRows);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (insertedRows < totalRows)
                        {
                            // Process each row
                            insertedRows++;

                            // Update progress bar and text box
                            await frmConvertToHcc.UpdateProgressAsyncservices(insertedRows, totalRows);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
            }
        }
        public int GetTotalRowsForBatchservices(int batchId)// Getting total rows from required table
        {

            try
            {
                errorOccurred = false;
                int totalRows = 0;

                using (SqlCommand command = new SqlCommand(Constants.GetTotalRowsForBatchservicesQuery, GetConnection()))
                {
                    command.Parameters.AddWithValue(Constants.AtBatchid, batchId);
                    totalRows = (int)command.ExecuteScalar();
                    return totalRows;
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
                return 0;
            }

        }
        public int GetTotalForBatch(int batchId)// getting total rows from particular tables
        {
            try
            {
                errorOccurred = false;
                int totalRows = 0;

                using (SqlCommand command = new SqlCommand(Constants.GetTotalRowsQuery, GetConnection()))
                {
                    command.Parameters.AddWithValue(Constants.AtBatchid, batchId);
                    totalRows = (int)command.ExecuteScalar();
                    return totalRows;
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
                return 0;
            }

        }
        public void AddRemovedBatchIdToDatabase(int batchId)// // Method to add a removed batch ID to the database table
        {
            try
            {
                errorOccurred = false;

                SqlCommand cmd = new SqlCommand(Constants.AddRemovedBatchIdToDatabaseQuery, GetConnection());
                cmd.Parameters.AddWithValue(Constants.AtBatchid, batchId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show($@"{Constants.ErroraddingremovedbatchIDtodatabase} {ex.Message}");
            }
        }
        public object UpdateGridStatus(int status)// to upadte the Grid status 
        {
            try
            {
                errorOccurred = false;
                string valueSelectQuery = Constants.UpdateGridStatusQuery;

                using (SqlCommand com = new SqlCommand(valueSelectQuery, GetConnection()))
                {
                    com.Parameters.AddWithValue(Constants.AtListsId, status);

                    var result = com.ExecuteScalar();
                    return result;
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public object GetListValue(string statusValue)// to get the value of the status 
        {
            try
            {
                errorOccurred = false;
                if (!string.IsNullOrEmpty(statusValue))
                {
                    string valueSelectQuery = Constants.ListConversion;

                    using (SqlCommand com = new SqlCommand(valueSelectQuery, GetConnection()))
                    {
                        com.CommandType = CommandType.StoredProcedure;
                        com.Parameters.AddWithValue(Constants.AtListsId, statusValue);
                        var result = com.ExecuteScalar();
                        return result;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public int GetTotalRowsOfCmsClients(int batchId)// getting total rows from particular tables
        {
            try
            {
                errorOccurred = false;
                int totalRows = 0;
                using (SqlCommand command = new SqlCommand(Constants.CountCmsClients, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 120;
                    command.Parameters.AddWithValue(Constants.AtBatchid, batchId);
                    totalRows = (int)command.ExecuteScalar();
                    return totalRows;
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
                return 0;
            }
        }
        public void MapCmsClients(int selectedBatchId, bool isOchin) // to Map the Cms  clients to HCC tables(Ochin to RWDE)
        {
            try
            {
                string query = string.Empty;
                if (isOchin)
                {
                    query = Constants.MapCmsClientsOchin;
                }
                else
                {
                    query = Constants.MapCmsClients;
                }
                errorOccurred = false;
                OchinToRwdeConversion ochinToRwdeConversion = new OchinToRwdeConversion();

                using (SqlCommand command = new SqlCommand(query, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 120;
                    // Pass the selected BatchID to the stored procedure
                    command.Parameters.AddWithValue(Constants.AtBatchid, selectedBatchId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
            }
        }
        public void AbortServiceConversion(int batchId)// clear tables while Aborting
        {
            try
            {
                errorOccurred = false;

                using (SqlCommand command = new SqlCommand(Constants.AbortServiceConversion, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(Constants.AtBatchid, batchId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);

                throw; // Re-throw if you want to handle it in the calling method
            }
        }
        public void AbortClientConversion(int batchId)// clear tables while Aborting
        {
            try
            {
                errorOccurred = false;

                using (SqlCommand command = new SqlCommand(Constants.AbortClientConversion, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(Constants.AtBatchid, batchId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);

                throw; // Re-throw if you want to handle it in the calling method
            }
        }

        public void UpdateBatchStatusOchin(int selectedBatchId, int status,string FileName)// Updating Batch Status calling batchid here 
        {
            try
            {
                errorOccurred = false;

                // Construct the SQL UPDATE statement
                string query = Constants.UpdateBatch;
                // Create and execute the SqlCommand
                using (SqlCommand command = new SqlCommand(query, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    // Add parameters to the command
                    command.Parameters.AddWithValue(Constants.AtStatus, status);
                    command.Parameters.AddWithValue(Constants.AtBatchid, selectedBatchId);
                    command.Parameters.AddWithValue(Constants.AtFileName, FileName);

                    // Execute the update query
                    int rowsAffected = command.ExecuteNonQuery();
                    // Check if any rows were affected
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(Constants.Errorupdatingbatchstatus, ex.Message);
            }
        }
        public int GetTotalRowsForBatchservicesOchin(int batchId)// Getting total rows from required table
        {
            try
            {
                errorOccurred = false;
                int totalRows = 0;

                using (SqlCommand command = new SqlCommand(Constants.CountCmsServices, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(Constants.AtBatchid, batchId);
                    totalRows = (int)command.ExecuteScalar();
                    return totalRows;
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
            }
            return 0;
        }

        // to Map the CMS Services to Hcc Tables
        public void MapCmsServicesToHccServices(int selectedBatchId, bool isOchin)
        {
            try
            {
                string query = string.Empty;
                errorOccurred = false;

                if (isOchin)
                {
                    query = Constants.MapCMSServicesToHCCServicesOchin;
                }
                else
                {
                    query = Constants.MapCmsServicesToHccServices;
                }

                using (SqlCommand command = new SqlCommand(query, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(Constants.AtBatchid, selectedBatchId);

                    // Getting total rows from required table
                    int totalRows = GetTotalRowsForBatchservicesOchin(selectedBatchId);
                    if (ErrorOccurred)
                    {
                        MessageBox.Show(Constants.ErrorOccurred);
                        return;
                    }

                    if (totalRows <= 0)
                    {
                        throw new InvalidOperationException("Total rows must be greater than zero.");
                    }

                    // Execute the stored procedure
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
            }
        }
        public void UpdateGridStatus(int batchId, int status)// Updates the status label on the form based on a given status code retrieved from the database.
        {
            try
            {
                errorOccurred = false;
                string valueSelectQuery = Constants.UpdateGrid;

                using (SqlCommand com = new SqlCommand(valueSelectQuery, GetConnection()))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue(Constants.AtListsId, status);

                    com.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                // Log or handle the exception appropriately
                MessageBox.Show($@"{Constants.ErrorUpdatingGridStatus}{ex.Message}");
            }
        }
        public DataTable FillTheGrid(string query)// to fill the Gird of Requested screen
        {
            try
            {
                errorOccurred = false;
                SqlCommand command = new SqlCommand(query, GetConnection())
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public DataTable FillTheGridQuery(string query) // to fill the Gird of Requested screen using Query
        {
            try
            {
                errorOccurred = false;
                SqlCommand command = new SqlCommand(query, GetConnection());
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public bool IsContractRenewed(string contractId)
        {
            try
            {
                errorOccurred = false;

                using (SqlCommand command = new SqlCommand(Constants.IsContractRenewed, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue(Constants.AtContractId, contractId);
                    SqlParameter outputParameter = new SqlParameter
                    {
                        ParameterName = Constants.AtIsRenewed,
                        SqlDbType = SqlDbType.Bit,
                        Direction = ParameterDirection.Output,
                    };
                    command.Parameters.Add(outputParameter);
                    command.ExecuteNonQuery();

                    return outputParameter.Value.AsBool();
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool IsContractPresent(string contractId)
        {
            try
            {
                errorOccurred = false;

                using (SqlCommand command = new SqlCommand(Constants.IsContractPresent, GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue(Constants.AtContractId, contractId);
                    SqlParameter outputParameter = new SqlParameter
                    {
                        ParameterName = Constants.AtIsPresent,
                        SqlDbType = SqlDbType.Bit,
                        Direction = ParameterDirection.Output,
                    };
                    command.Parameters.Add(outputParameter);
                    command.ExecuteNonQuery();

                    return outputParameter.Value.AsBool();
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public List<int> GetNotUpdatedBatchIds(string fileType) // get all batches type except Client Track
        {
            List<int> batchIds = new List<int>();
            try
            {
                errorOccurred = false;
                string query = string.Empty;
                if (fileType==Constants.Clients)
                {
                    query = Constants.GetNotUpdatedClientBatchIds;
                }
                else if(fileType == Constants.Services)
                {
                    query = Constants.GetNotUpdatedServiceBatchIds;
                }
                else
                {
                    MessageBox.Show(Constants.InvalidFileType);
                }
                using (SqlCommand com = new SqlCommand(query, GetConnection()))
                {

                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            batchIds.Add(Convert.ToInt32(reader[Constants.BatchId]));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorOccurred = true;
                // Optionally, log the exception
                MessageBox.Show(ex.Message);
            }

            return batchIds;
        }

        public void UpdateServiceErrors()
        {
            try
            {
                errorOccurred = false;


            }
            catch(Exception ex)
            {
                errorOccurred = true;
                MessageBox.Show(ex.Message);
            }
        }
    }
}