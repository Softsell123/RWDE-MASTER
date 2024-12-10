using System; //shannu comments
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using Rwde;
using System.Globalization;// Add appropriate using directive
using System.Xml;
using System.Data.SqlTypes;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Web.UI.DataVisualization.Charting;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Diagnostics.Eventing.Reader;
using ClosedXML.Excel;//

namespace RWDE
{
    public class DBHelper
    {
        private readonly string connectionString; // Stores the connection string for the database
        private string error;
        private bool batchIDIncremented = false;// Stores any error messages encountered during database operations
        private bool batchIDIncre;
        
        // Constructor to initialize the DBHelper class with a connection string
        public DBHelper()
        {
            // Define the connection string within the DBHelper class
            connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
        }
        public string GetConnectionString()//get connection string
        {
            return connectionString;
        }

        // Method to check if a message is already logged
        private bool IsMessageAlreadyLogged(string message)
        {
            return false; // Placeholder implementation
        }
        public async Task<BatchDetails> GetBatchDetailsFromSPAsync(int batchID)//to check whether the conversion completed or not
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Conversioncompletion", conn))

                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BatchID", batchID);

                        await conn.OpenAsync();

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                return new BatchDetails
                                {
                                    ConversionStartedAt = reader["ConversionStartedAt"] as DateTime?,
                                    ConversionEndedAt = reader["ConversionEndedAt"] as DateTime?
                                };
                            }
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public async Task<BatchDetails> GetBatchDetailsFromSPAsyncclients(int batchID)//to check whether the generation completed or not
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ClientCONVERSIONCOMPLETION", conn))

                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BatchID", batchID);

                        await conn.OpenAsync();

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                return new BatchDetails
                                {
                                    ConversionStartedAt = reader["ConversionStartedAt"] as DateTime?,
                                    ConversionEndedAt = reader["ConversionEndedAt"] as DateTime?
                                };
                            }
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public async Task<BatchDetailsgeneration> GetBatchDetailsFromSPAgenearationlients(int batchID)//to check whether the generation completed or not
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ClientgenerationCOMPLETION", conn))

                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BatchID", batchID);

                        await conn.OpenAsync();

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                return new BatchDetailsgeneration
                                {
                                    GenerationStartedAt = reader["GenerationStartedAt"] as DateTime?,
                                    GenerationEndedAt = reader["GenerationEndedAt"] as DateTime?
                                };
                            }
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public async Task<BatchDetailsgeneration> GetBatchDetailsFromSPAgenearationservices(int batchID)//to check whether the generation completed or not
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ServicegenerationCOMPLETION", conn))

                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BatchID", batchID);

                        await conn.OpenAsync();//CONNECTION STRING

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                return new BatchDetailsgeneration
                                {
                                    GenerationStartedAt = reader["GenerationStartedAt"] as DateTime?,
                                    GenerationEndedAt = reader["GenerationEndedAt"] as DateTime?//
                                };
                            }
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public DataTable LoadDatafilterServiceReconbatchid(List<int> batchIDs)
        {
            DataTable dy = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Call the stored procedure to update HCCServices
                    using (SqlCommand updateCmd = new SqlCommand("UpdateHCCServicesWithErrors", conn))
                    {
                        updateCmd.CommandType = CommandType.StoredProcedure;
                        updateCmd.ExecuteNonQuery();
                    }

                    // Validate batch IDs against the database
                    List<int> validBatchIDs = new List<int>();
                    List<int> invalidBatchIDs = new List<int>(); // To store invalid batch IDs
                    string validationQuery = "SELECT DISTINCT BatchID FROM vwService_Reconciliationtest WHERE BatchID IN (" +
                                             string.Join(",", batchIDs.Select((_, i) => "@BatchID" + i)) + ")";

                    using (SqlCommand validateCmd = new SqlCommand(validationQuery, conn))
                    {
                        for (int i = 0; i < batchIDs.Count; i++)
                        {
                            validateCmd.Parameters.AddWithValue("@BatchID" + i, batchIDs[i]);
                        }

                        using (SqlDataReader reader = validateCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                validBatchIDs.Add(reader.GetInt32(0));
                            }
                        }
                    }

                    // Check for invalid batch IDs
                    invalidBatchIDs = batchIDs.Except(validBatchIDs).ToList();
                    if (invalidBatchIDs.Any())
                    {
                        MessageBox.Show($"The following Batch ID(s) do not exist: {string.Join(", ", invalidBatchIDs)}",
                                         "Invalid Batch ID(s)");
                    }

                    // Construct the main query for valid batch IDs
                    if (validBatchIDs.Any())
                    {
                        string query = "SELECT * FROM vwService_Reconciliationtest WHERE BatchID IN (" +
                                       string.Join(",", validBatchIDs.Select((_, i) => "@ValidBatchID" + i)) + ")";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            for (int i = 0; i < validBatchIDs.Count; i++)
                            {
                                cmd.Parameters.AddWithValue("@ValidBatchID" + i, validBatchIDs[i]);
                            }

                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            adapter.Fill(dy);

                            if (dy.Rows.Count == 0)
                            {
                                MessageBox.Show(Constants.nobatchid, "Service Reconciliation Report");
                            }
                        }
                    }
                    else
                    {
                        // If no valid batch IDs, return an empty DataTable
                        dy.Columns.Add("BatchID", typeof(int));
                        dy.Columns.Add("ServiceDate", typeof(DateTime));
                        dy.Columns.Add("CreatedDate", typeof(DateTime));
                        dy.Columns.Add("ErrorDetails", typeof(string));
                        dy.Columns.Add("Status", typeof(string));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while loading data.", ex);
                }
            }

            return dy; // Return the populated DataTable
        }

        public class BatchDetailsgeneration
        {
            public DateTime? GenerationStartedAt { get; set; }
            public DateTime? GenerationEndedAt { get; set; }
        }
        public class BatchDetails
        {
            public DateTime? ConversionStartedAt { get; set; }
            public DateTime? ConversionEndedAt { get; set; }
        }
        // Method to map LogType enum values to database code
        public bool AnyBatchesExist()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Batch", connection);

                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }
        public int GetNextBatchID()//Getting BatchId for particular file insertion
        {
            try
            {
                int nextBatchID = 1;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SELECT ISNULL(MAX(BatchID), 0) FROM Batch", connection);

                    // Get the last batch ID from the database
                    object result = command.ExecuteScalar();
                    int lastBatchIDFromDB = result == DBNull.Value ? 0 : Convert.ToInt32(result);

                    // Determine the next batch ID to use based on the current state of batchIDIncremented
                    if (!batchIDIncremented)
                    {
                        // If batchIDIncremented is false, determine the next batch ID to use
                        if (lastBatchIDFromDB == 0)
                        {
                            // If no batch IDs exist in the database, start with batch ID 1
                            nextBatchID = 1;
                        }
                        else
                        {
                            // Increment the last batch ID to get the next available batch ID
                            nextBatchID = ++lastBatchIDFromDB;
                        }

                        // Set batchIDIncremented to true to indicate that the batch ID has been incremented
                        batchIDIncremented = true;
                    }
                    else
                    {
                        // If batchIDIncremented is already true, return the last batch ID from the database
                        nextBatchID = lastBatchIDFromDB;
                    }
                    return nextBatchID;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }
        public int GetMaxXMLBatchID()//Getting BatchId for particular file insertion
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SELECT ISNULL(MAX(BatchID), 0) FROM CTClients", connection);

                    // Get the last batch ID from the database
                    object result = command.ExecuteScalar();
                    int maxBatchXMLIDFromDB = result == DBNull.Value ? 0 : Convert.ToInt32(result);

                  
                    return maxBatchXMLIDFromDB;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }
        public int GetMaxBatchID()//Getting BatchId for particular file insertion
        {
            try
            {
                int nextBatchID = 1;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SELECT ISNULL(MAX(BatchID), 0) FROM Batch", connection);

                    // Get the last batch ID from the database
                    object result = command.ExecuteScalar();
                    int lastBatchIDFromDB = result == DBNull.Value ? 0 : Convert.ToInt32(result);

                    // If batchIDIncremented is false, determine the next batch ID to use
                    if (lastBatchIDFromDB == 0)
                    {
                        // If no batch IDs exist in the database, start with batch ID 1
                        nextBatchID = 1;
                    }
                    else
                    {
                        // Increment the last batch ID to get the next available batch ID
                        nextBatchID = lastBatchIDFromDB;
                    }
                    return nextBatchID;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }
        public void InsertBatch(int batchId, string fileName, string path, int type, string description, DateTime startedAt, int totalRowsInCurrentFile, int successfulRows, int status)//update batch status in database
        
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Loop until a unique batchId is found
                    //while (BatchExists(connection, batchId, fileName))
                    //{
                    //    batchId++;
                    //}

                    // Proceed with inserting the new batch record
                    SqlCommand command = new SqlCommand("insertbatchtable", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    string datePart = startedAt.ToString("dd-MM-yyyy");
                    string timePart = startedAt.ToString("HH:mm:ss");
                    command.Parameters.AddWithValue("@BatchID", batchId);
                    command.Parameters.AddWithValue("@FileName", fileName);
                    command.Parameters.AddWithValue("@Description", $"OCHIN TO RWDE On {datePart} at {timePart}");
                    command.Parameters.AddWithValue("@Path", path);
                    command.Parameters.AddWithValue("@Type", type);
                    command.Parameters.AddWithValue("@UploadStartedAt", startedAt);
                    command.Parameters.AddWithValue("@UploadEndedAt", DateTime.Now);
                    command.Parameters.AddWithValue("@ConversionStartedAt", DBNull.Value);
                    command.Parameters.AddWithValue("@ConversionEndedAt", DBNull.Value);
                    command.Parameters.AddWithValue("@GenerationStartedAt", DBNull.Value);
                    command.Parameters.AddWithValue("@GenerationEndedAt", DBNull.Value); // Assuming EndedAt is set to current time
                    command.Parameters.AddWithValue("@TotalRows", totalRowsInCurrentFile);
                    command.Parameters.AddWithValue("@SuccessfulRows", successfulRows);
                    command.Parameters.AddWithValue("@FailedRows", totalRowsInCurrentFile - successfulRows); // Calculate failed rows
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@CreatedBy", Constants.CreatedBy);
                    command.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                    command.Parameters.AddWithValue("@Comments", string.Empty); // Provide default value for Comments

                    command.ExecuteNonQuery(); // Execute the SQL insert command
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting batch: {ex.Message}");
                // Log or handle the exception appropriately
            }
        }
        private bool BatchExists(SqlConnection connection, int batchId, string fileName)//count of batche id
        {
            using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Batch WHERE BatchID = @BatchID AND FileName = @FileName", connection))
            {
                command.Parameters.AddWithValue("@BatchID", batchId);
                command.Parameters.AddWithValue("@FileName", fileName);

                return (int)command.ExecuteScalar() > 0;
            }
        }
        public void InsertClientServiceData(SqlConnection connection, string[] data, int batchId)//insertion of client service data
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("InsertClientServices", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters with appropriate conversion and null handling
                    cmd.Parameters.AddWithValue("@BatchID", batchId);
                    cmd.Parameters.AddWithValue("@Clnt_id", int.Parse(data[0]));
                    cmd.Parameters.AddWithValue("@Service_date", DateTime.Parse(data[1]));
                    cmd.Parameters.AddWithValue("@Contract_id", int.Parse(data[2]));
                    cmd.Parameters.AddWithValue("@Staff_id",data[3]);
                    cmd.Parameters.AddWithValue("@Prim_serv_desc", data[4]);
                    cmd.Parameters.AddWithValue("@Quantity_served", decimal.Parse(data[6]));
                    cmd.Parameters.AddWithValue("@Unit_cd", data[5]);
                    cmd.Parameters.AddWithValue("@Actual_minutes_spent", int.Parse(data[7]));
                    cmd.Parameters.AddWithValue("@ServiceID", int.Parse(data[8]));
                    // Construct AdditionalServiceInformation with the new format
                    string additionalServiceInformation = $"Id={data[8]};";
                    cmd.Parameters.AddWithValue("@AdditionalServiceInformation", additionalServiceInformation);

                    cmd.Parameters.AddWithValue("@CreatedBy", Constants.CreatedBy);
                    cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);

                    if (connection.State != System.Data.ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Data inserted successfully.");
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        public void InsertClientServiceDataPHI(SqlConnection connection, string[] data, int batchId)//insertion client data phi masked
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("InsertClientServicesPHI", connection))
                {

                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters with appropriate conversion and null handling
                    cmd.Parameters.AddWithValue("@BatchID", batchId-1);
                    cmd.Parameters.AddWithValue("@Clnt_id", int.Parse(data[0]));
                    cmd.Parameters.AddWithValue("@Service_date", DateTime.Parse(data[1]));
                    cmd.Parameters.AddWithValue("@Contract_id", int.Parse(data[2]));
                    cmd.Parameters.AddWithValue("@Staff_id", int.Parse(data[3]));
                    cmd.Parameters.AddWithValue("@Prim_serv_desc", data[4]);
                    cmd.Parameters.AddWithValue("@Quantity_served", decimal.Parse(data[5]));
                    cmd.Parameters.AddWithValue("@Unit_cd", data[6]);
                    cmd.Parameters.AddWithValue("@Actual_minutes_spent", int.Parse(data[7]));
                    cmd.Parameters.AddWithValue("@ServiceID", int.Parse(data[8]));
                    // Construct AdditionalServiceInformation with the new format
                    string additionalServiceInformation = $"Id={data[8]};";
                    cmd.Parameters.AddWithValue("@AdditionalServiceInformation", additionalServiceInformation);

                    cmd.Parameters.AddWithValue("@CreatedBy", Constants.CreatedBy);
                    cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                    if (connection.State != System.Data.ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Data inserted successfully.");
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        public void InsertClientInformation(SqlConnection connection, string[] data, int batchid)//cms client insertion
        {
            try
            {
                using (SqlCommand command = new SqlCommand("InsertClientInfotest", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters with appropriate conversion and null handling
                    command.Parameters.AddWithValue("@BatchID", batchid);
                    command.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                    command.Parameters.AddWithValue("@clnt_id", ConvertToIntOrNull(data[0]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@first_nm", data[1] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@last_nm", data[2] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@mi", data[3] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@chsn_nm", data[4] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@dob", ConvertToDateTimeOrNull(data[5]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@is_decd", ConvertToIntOrNull(data[6]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@dt_of_death", ConvertToDateTimeOrNull(data[7]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@place_of_death", data[8] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SSN", data[9] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@homeless_flg", ConvertToIntOrNull(data[10]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@gndr_cd", data[11] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@sex_cd", data[12] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@lang_pref_cd", data[13] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@mrtl_stat_cd", data[14] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@sexual_ornt_type_cd", data[15] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@edu_lvl", data[16] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@veteran", ConvertTo(data[17]) ?? (object)DBNull.Value);//17 MISSING
                    command.Parameters.AddWithValue("@email", data[18] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@allow_cntct_email_ind", ConvertToIntOrNull(data[19]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@prsn_mobile_phn", data[20] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@allow_cntct_mobile_ind", ConvertToIntOrNull(data[21]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@allow_msgs_mobile_ind", ConvertToIntOrNull(data[22]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@allow_vm_mobile_ind", ConvertToIntOrNull(data[23]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@emergency_cntct_nm", data[24] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@emergency_cntct_rltnshp", data[25] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@emergency_prsn_mobile_phn", data[26] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@allow_emergency_cntct_ind", ConvertToIntOrNull(data[27]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@allow_emergency_cntct_msgs_ind", ConvertToIntOrNull(data[28]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@allow_emergency_cntct_vm_ind", ConvertToIntOrNull(data[29]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@agency_id", ConvertToIntOrNull(data[30]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@regstrn_dt", ConvertToDateTimeOrNull(data[31]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@agency_client_1", data[32] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@agency_client_2", data[33] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fk_agency_status_cd", data[34] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@agency_status_dt", ConvertToDateTimeOrNull(data[35]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@reloc_fk_state_cd", data[36] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@reloc_fk_county_cd", data[37] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fk_addr_type_cd", data[38] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@address_line_1", data[39] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@address_line_2", data[40] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@city", data[41] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fk_state_cd", data[42] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@county", data[43] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@zip", data[44] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@AddressSince", data[45] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@mail_allw_ind", ConvertToIntOrNull(data[46]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@client_income_year", ConvertToIntOrNull(data[47]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@client_no_income_fin_res_ind", ConvertToIntOrNull(data[48]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@client_total_mth_incm", ConvertToDecimalOrNull(data[49]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@hh_income_year", ConvertToIntOrNull(data[50]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@hh_no_income_fin_res_ind", ConvertTo(data[51]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@earn_incm_frm_emplmnt", ConvertToDecimalOrNull(data[52]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@retirement_incm", ConvertToDecimalOrNull(data[53]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@sup_sec_incm", ConvertToDecimalOrNull(data[54]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@soc_dis_ins_incm", ConvertToDecimalOrNull(data[55]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@othr_wlfr_asst_incm", ConvertToDecimalOrNull(data[56]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@pvt_disab_ins_incm", ConvertToDecimalOrNull(data[57]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@vtrn_dis_pymt_incm", ConvertToDecimalOrNull(data[58]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@reg_cntbr_othr_incm", ConvertToDecimalOrNull(data[59]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@wrkr_comp_incm", ConvertToDecimalOrNull(data[60]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@gnrl_asst_incm", ConvertToDecimalOrNull(data[61]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@unempl_ins_incm", ConvertToDecimalOrNull(data[62]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@othr_src_incm", ConvertToDecimalOrNull(data[63]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@hshld_size", ConvertToIntOrNull(data[64]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fk_emplymnt_stat_cd", data[65] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fk_race_cd", data[66] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fk_race_dtl_cd", data[67] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fk_ethn_cd", data[68] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fk_ethn_dtl_cd", data[69] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@curr_hiv_stat_cd", data[70] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@hiv_dx_dt", ConvertToDateTimeOrNull(data[71]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@hiv_dx_src_txt", data[72] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@aids_dx_dt", ConvertToDateTimeOrNull(data[73]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@aids_dx_src_txt", data[74] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@perinatal_transmission", ConvertToIntOrNull(data[75]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@male_to_male_sexual_contact", ConvertTo(data[76]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@high_risk_heterosexual_contact", ConvertToIntOrNull(data[77]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@injection_drug_use", ConvertToIntOrNull(data[78]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@hemophilia_coagulation_disorder", ConvertToIntOrNull(data[79]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@receipt_of_blood_transfusion", ConvertToIntOrNull(data[80]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@risk_factor_not_reported_identifier", ConvertToIntOrNull(data[81]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@hiv_test_dt", ConvertToDateTimeOrNull(data[82]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@hiv_rslt_stat_cd", data[83] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fk_ins_type_cd", data[84] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fk_ins_sub_type_cd", data[85] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@new_covrg_cvrs_old_cvrg_ind", ConvertToIntOrNull(data[86]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@start_date", ConvertToDateTimeOrNull(data[87]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@end_date", ConvertToDateTimeOrNull(data[88]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@notes", data[89] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@hsng_asstnc_cd", data[90] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@asstnc_start_dt", ConvertToDateTimeOrNull(data[91]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@asstnc_end_dt", ConvertToDateTimeOrNull(data[92]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fk_lvng_sttn_cd", data[93] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fk_lvng_sttn_dtl_cd", data[94] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@housing_status", data[95] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@asof_dt", ConvertToDateTimeOrNull(data[96]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CreatedBy", Constants.CreatedBy);
                    command.Parameters.AddWithValue("@SourceSystemName", Constants.Ochin);
                    command.Parameters.AddWithValue("@UserID", Constants.userid);
                    command.Parameters.AddWithValue("@AgencyID", Constants.agencyid);
                    command.Parameters.AddWithValue("@sourceid", DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void InsertClientInformationphiurn(SqlConnection connection, string[] data, int batchid)//cms client insertion
        {
            try
            {
                using (SqlCommand command = new SqlCommand("InsertClientInfoPHIWithURN", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters with appropriate conversion and null handling
                    command.Parameters.AddWithValue("@BatchID", batchid);
                    command.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                    command.Parameters.AddWithValue("@clnt_id", ConvertToIntOrNull(data[0]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@first_nm", data[1] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@last_nm", data[2] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@mi", data[3] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@chsn_nm", data[4] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@dob", ConvertToDateTimeOrNull(data[5]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@is_decd", ConvertToIntOrNull(data[6]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@dt_of_death", ConvertToDateTimeOrNull(data[7]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@place_of_death", data[8] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SSN", data[9] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@homeless_flg", ConvertToIntOrNull(data[10]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@gndr_cd", data[11] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@sex_cd", data[12] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@lang_pref_cd", data[13] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@mrtl_stat_cd", data[14] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@sexual_ornt_type_cd", data[15] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@edu_lvl", data[16] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@veteran", ConvertTo(data[17]) ?? (object)DBNull.Value);//17 MISSING
                    command.Parameters.AddWithValue("@email", data[18] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@allow_cntct_email_ind", ConvertToIntOrNull(data[19]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@prsn_mobile_phn", data[20] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@allow_cntct_mobile_ind", ConvertToIntOrNull(data[21]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@allow_msgs_mobile_ind", ConvertToIntOrNull(data[22]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@allow_vm_mobile_ind", ConvertToIntOrNull(data[23]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@emergency_cntct_nm", data[24] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@emergency_cntct_rltnshp", data[25] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@emergency_prsn_mobile_phn", data[26] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@allow_emergency_cntct_ind", ConvertToIntOrNull(data[27]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@allow_emergency_cntct_msgs_ind", ConvertToIntOrNull(data[28]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@allow_emergency_cntct_vm_ind", ConvertToIntOrNull(data[29]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@agency_id", ConvertToIntOrNull(data[30]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@regstrn_dt", ConvertToDateTimeOrNull(data[31]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@agency_client_1", data[32] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@agency_client_2", data[33] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fk_agency_status_cd", data[34] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@agency_status_dt", ConvertToDateTimeOrNull(data[35]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@reloc_fk_state_cd", data[36] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@reloc_fk_county_cd", data[37] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fk_addr_type_cd", data[38] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@address_line_1", data[39] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@address_line_2", data[40] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@city", data[41] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fk_state_cd", data[42] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@county", data[43] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@zip", data[44] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@AddressSince", data[45] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@mail_allw_ind", ConvertToIntOrNull(data[46]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@client_income_year", ConvertToIntOrNull(data[47]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@client_no_income_fin_res_ind", ConvertToIntOrNull(data[48]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@client_total_mth_incm", ConvertToDecimalOrNull(data[49]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@hh_income_year", ConvertToIntOrNull(data[50]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@hh_no_income_fin_res_ind", ConvertToIntOrNull(data[51]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@earn_incm_frm_emplmnt", ConvertToDecimalOrNull(data[52]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@retirement_incm", ConvertToDecimalOrNull(data[53]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@sup_sec_incm", ConvertToDecimalOrNull(data[54]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@soc_dis_ins_incm", ConvertToDecimalOrNull(data[55]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@othr_wlfr_asst_incm", ConvertToDecimalOrNull(data[56]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@pvt_disab_ins_incm", ConvertToDecimalOrNull(data[57]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@vtrn_dis_pymt_incm", ConvertToDecimalOrNull(data[58]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@reg_cntbr_othr_incm", ConvertToDecimalOrNull(data[59]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@wrkr_comp_incm", ConvertToDecimalOrNull(data[60]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@gnrl_asst_incm", ConvertToDecimalOrNull(data[61]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@unempl_ins_incm", ConvertToDecimalOrNull(data[62]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@othr_src_incm", ConvertToDecimalOrNull(data[63]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@hshld_size", ConvertToIntOrNull(data[64]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fk_emplymnt_stat_cd", data[65] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fk_race_cd", data[66] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fk_race_dtl_cd", data[67] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fk_ethn_cd", data[68] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fk_ethn_dtl_cd", data[69] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@curr_hiv_stat_cd", data[70] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@hiv_dx_dt", ConvertToDateTimeOrNull(data[71]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@hiv_dx_src_txt", data[72] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@aids_dx_dt", ConvertToDateTimeOrNull(data[73]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@aids_dx_src_txt", data[74] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@perinatal_transmission", ConvertToIntOrNull(data[75]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@male_to_male_sexual_contact", ConvertToIntOrNull(data[76]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@high_risk_heterosexual_contact", ConvertToIntOrNull(data[77]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@injection_drug_use", ConvertToIntOrNull(data[78]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@hemophilia_coagulation_disorder", ConvertToIntOrNull(data[79]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@receipt_of_blood_transfusion", ConvertToIntOrNull(data[80]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@risk_factor_not_reported_identifier", ConvertToIntOrNull(data[81]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@hiv_test_dt", ConvertToDateTimeOrNull(data[82]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@hiv_rslt_stat_cd", data[83] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fk_ins_type_cd", data[84] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fk_ins_sub_type_cd", data[85] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@new_covrg_cvrs_old_cvrg_ind", ConvertToIntOrNull(data[86]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@start_date", ConvertToDateTimeOrNull(data[87]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@end_date", ConvertToDateTimeOrNull(data[88]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@notes", data[89] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@hsng_asstnc_cd", data[90] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@asstnc_start_dt", ConvertToDateTimeOrNull(data[91]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@asstnc_end_dt", ConvertToDateTimeOrNull(data[92]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fk_lvng_sttn_cd", data[93] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fk_lvng_sttn_dtl_cd", data[94] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@housing_status", data[95] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@asof_dt", ConvertToDateTimeOrNull(data[96]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CreatedBy", Constants.CreatedBy);
                    command.Parameters.AddWithValue("@SourceSystemName", Constants.Ochin);
                    command.Parameters.AddWithValue("@UserID", Constants.userid);
                    command.Parameters.AddWithValue("@AgencyID", Constants.agencyid);
                    command.Parameters.AddWithValue("@sourceid", DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private int? ConvertToIntOrNull(string input)//convert to int
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
        private string ConvertTo(string input)//convert to int
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
        private DateTime? ConvertToDateTimeOrNull(string input)//convert to date or null
        {
            if (DateTime.TryParse(input, out DateTime result))
            {
                return result;
            }
            return null;
        }
        private decimal? ConvertToDecimalOrNull(string input)//convert to decimal
        {
            if (decimal.TryParse(input, out decimal result))
            {
                return result;
            }
            return null;
        }
        private bool ConvertToBoolean(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            bool parsedBool;
            if (bool.TryParse(value, out parsedBool))
            {
                return parsedBool;
            }
            else
            {
                return false;
            }
        }
        private DateTime? ConvertToDateTime(string value)//convert to date and time
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            DateTime parsedDate;
            if (DateTime.TryParse(value, out parsedDate))
            {
                return parsedDate;
            }
            else
            {
                return null;
            }
        }
        private bool? ConvertToBoolOrNull(string value)//to convert to bool
        {
            return bool.TryParse(value, out bool result) ? (bool?)result : null;
        }
        private decimal? ConvertToDecimal(string value)//convert to decimal
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            decimal parsedDecimal;
            if (decimal.TryParse(value, out parsedDecimal))
            {
                return parsedDecimal;
            }
            else
            {
                return null;
            }
        }
        // Helper methods for conversions
        private string ConvertToString(object value)//convert to string
        {
            return value == null || value == DBNull.Value ? (string)null : value.ToString();
        }
        private string ConvertToDateTime(object value)//covert to correct date format
        {
            if (DateTime.TryParse(value?.ToString(), out DateTime result))
            {
                return result.ToString("yyyy-MM-dd HH:mm:ss");
            }
            return null; // or handle the error as needed
        }
        public void InsertClientInformationPHI(SqlConnection connection, string[] data, int batchid)//cms client insertion
        {
            try
            {
                using (SqlCommand command = new SqlCommand("InsertClientInfoPHI", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters with appropriate conversion and null handling
                    command.Parameters.AddWithValue("@BatchID", batchid);
                    command.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                    command.Parameters.AddWithValue("@clnt_id", ConvertToIntOrNull(data[0]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@first_nm", data[1] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@last_nm", data[2] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@mi", data[3] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@chsn_nm", data[4] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@dob", ConvertToDateTimeOrNull(data[5]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@is_decd", ConvertToIntOrNull(data[6]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@dt_of_death", ConvertToDateTimeOrNull(data[7]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@place_of_death", data[8] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SSN", data[9] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@homeless_flg", ConvertToIntOrNull(data[10]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@gndr_cd", data[11] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@sex_cd", data[12] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@lang_pref_cd", data[13] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@mrtl_stat_cd", data[14] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@sexual_ornt_type_cd", data[15] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@edu_lvl", data[16] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@veteran", ConvertTo(data[17]) ?? (object)DBNull.Value);//17 MISSING
                    command.Parameters.AddWithValue("@email", data[18] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@allow_cntct_email_ind", ConvertToIntOrNull(data[19]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@prsn_mobile_phn", data[20] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@allow_cntct_mobile_ind", ConvertToIntOrNull(data[21]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@allow_msgs_mobile_ind", ConvertToIntOrNull(data[22]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@allow_vm_mobile_ind", ConvertToIntOrNull(data[23]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@emergency_cntct_nm", data[24] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@emergency_cntct_rltnshp", data[25] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@emergency_prsn_mobile_phn", data[26] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@allow_emergency_cntct_ind", ConvertToIntOrNull(data[27]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@allow_emergency_cntct_msgs_ind", ConvertToIntOrNull(data[28]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@allow_emergency_cntct_vm_ind", ConvertToIntOrNull(data[29]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@agency_id", ConvertToIntOrNull(data[30]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@regstrn_dt", ConvertToDateTimeOrNull(data[31]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@agency_client_1", data[32] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@agency_client_2", data[33] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fk_agency_status_cd", data[34] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@agency_status_dt", ConvertToDateTimeOrNull(data[35]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@reloc_fk_state_cd", data[36] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@reloc_fk_county_cd", data[37] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fk_addr_type_cd", data[38] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@address_line_1", data[39] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@address_line_2", data[40] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@city", data[41] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fk_state_cd", data[42] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@county", data[43] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@zip", data[44] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@AddressSince", data[45] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@mail_allw_ind", ConvertToIntOrNull(data[46]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@client_income_year", ConvertToIntOrNull(data[47]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@client_no_income_fin_res_ind", ConvertToIntOrNull(data[48]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@client_total_mth_incm", ConvertToDecimalOrNull(data[49]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@hh_income_year", ConvertToIntOrNull(data[50]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@hh_no_income_fin_res_ind", ConvertToIntOrNull(data[51]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@earn_incm_frm_emplmnt", ConvertToDecimalOrNull(data[52]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@retirement_incm", ConvertToDecimalOrNull(data[53]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@sup_sec_incm", ConvertToDecimalOrNull(data[54]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@soc_dis_ins_incm", ConvertToDecimalOrNull(data[55]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@othr_wlfr_asst_incm", ConvertToDecimalOrNull(data[56]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@pvt_disab_ins_incm", ConvertToDecimalOrNull(data[57]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@vtrn_dis_pymt_incm", ConvertToDecimalOrNull(data[58]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@reg_cntbr_othr_incm", ConvertToDecimalOrNull(data[59]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@wrkr_comp_incm", ConvertToDecimalOrNull(data[60]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@gnrl_asst_incm", ConvertToDecimalOrNull(data[61]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@unempl_ins_incm", ConvertToDecimalOrNull(data[62]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@othr_src_incm", ConvertToDecimalOrNull(data[63]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@hshld_size", ConvertToIntOrNull(data[64]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fk_emplymnt_stat_cd", data[65] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fk_race_cd", data[66] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fk_race_dtl_cd", data[67] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fk_ethn_cd", data[68] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fk_ethn_dtl_cd", data[69] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@curr_hiv_stat_cd", data[70] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@hiv_dx_dt", ConvertToDateTimeOrNull(data[71]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@hiv_dx_src_txt", data[72] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@aids_dx_dt", ConvertToDateTimeOrNull(data[73]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@aids_dx_src_txt", data[74] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@perinatal_transmission", ConvertToIntOrNull(data[75]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@male_to_male_sexual_contact", ConvertToIntOrNull(data[76]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@high_risk_heterosexual_contact", ConvertToIntOrNull(data[77]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@injection_drug_use", ConvertToIntOrNull(data[78]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@hemophilia_coagulation_disorder", ConvertToIntOrNull(data[79]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@receipt_of_blood_transfusion", ConvertToIntOrNull(data[80]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@risk_factor_not_reported_identifier", ConvertToIntOrNull(data[81]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@hiv_test_dt", ConvertToDateTimeOrNull(data[82]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@hiv_rslt_stat_cd", data[83] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fk_ins_type_cd", data[84] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fk_ins_sub_type_cd", data[85] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@new_covrg_cvrs_old_cvrg_ind", ConvertToIntOrNull(data[86]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@start_date", ConvertToDateTimeOrNull(data[87]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@end_date", ConvertToDateTimeOrNull(data[88]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@notes", data[89] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@hsng_asstnc_cd", data[90] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@asstnc_start_dt", ConvertToDateTimeOrNull(data[91]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@asstnc_end_dt", ConvertToDateTimeOrNull(data[92]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fk_lvng_sttn_cd", data[93] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fk_lvng_sttn_dtl_cd", data[94] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@housing_status", data[95] ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@asof_dt", ConvertToDateTimeOrNull(data[96]) ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CreatedBy", Constants.CreatedBy);
                    command.Parameters.AddWithValue("@SourceSystemName", Constants.Ochin);
                    command.Parameters.AddWithValue("@UserID", Constants.userid);
                    command.Parameters.AddWithValue("@AgencyID", Constants.agencyid);
                    command.Parameters.AddWithValue("@sourceid", DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private int? ConvertToNullableInt(string value)//parse data to int
        {
            if (int.TryParse(value, out int result))
            {
                return result;
            }
            return null;
        }
        private bool? ConvertToNullableBool(string value)//parse data to bool
        {
            if (bool.TryParse(value, out bool result))
            {
                return result;
            }
            return null;
        }
        private decimal? ConvertToNullableDecimal(string value)//parse data to decimal
        {
            if (decimal.TryParse(value, out decimal result))
            {
                return result;
            }
            return null;
        }

        private DateTime? ConvertToNullableDateTime(string value)
        {
            if (DateTime.TryParse(value, out DateTime result))
            {
                return result;
            }
            return null;
        }
        public bool InsertClientData(SqlConnection connection, string[] data, int batchid, string fileName)//Client table insertion

        {
            try
            {
                string ariesID = GetStringValue(data, 9); // Extract Aries ID from data array

                SqlCommand command = new SqlCommand("InsertIntoDlClients", connection);

                command.CommandType = CommandType.StoredProcedure;

                // Add parameters to the stored procedure
                command.Parameters.AddWithValue("@BatchID", batchid);
                command.Parameters.AddWithValue("@ClientID", GetStringData(data, 9)?.Trim('"'));
                command.Parameters.AddWithValue("@ClientFirstName", data[0]?.Trim()); // Trim leading and trailing whitespaces
                command.Parameters.AddWithValue("@ClientLastName", data[1]?.Trim()); // Trim leading and trailing whitespaces
                command.Parameters.AddWithValue("@ClientMiddleInitial", data[2]?.Trim()); // Trim leading and trailing whitespaces
                command.Parameters.AddWithValue("@ClientMothersMaidenNameFirstandThirdCharacters", data[3]?.Trim()); // Trim leading and trailing whitespaces

                DateTime clientDateOfBirth;
                string[] dateFormats = { "M/d/yyyy", "d-M-yyyy", "d/M/yyyy", "M-d-yyyy", "d-MMM-yyyy", "MMM-d-yyyy", "yyyy-M-d" };

                string[] parts = data[4].Split(' ');

                string dateString = parts[0].Replace("\"", "").Trim(); // Remove double quotes and trim

                if (dateString.Length > 0 && DateTime.TryParseExact(dateString, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out clientDateOfBirth))
                {
                    // If parsing succeeds, use the parsed date
                    command.Parameters.AddWithValue("@ClientDateofBirth", clientDateOfBirth);
                }
                else
                {
                    command.Parameters.AddWithValue("@ClientDateofBirth", DBNull.Value); // Add DBNull instead
                }
                command.Parameters.AddWithValue("@ClientGender", data[5]?.Trim()); // Trim leading and trailing whitespaces
                command.Parameters.AddWithValue("@ClientIsRelatedOrAffected", data[6]?.Trim()); // Trim leading and trailing whitespaces
                command.Parameters.AddWithValue("@ClientRecordIsShared", data[7]?.Trim()); // Trim leading and trailing whitespaces
                command.Parameters.AddWithValue("@ClientURNExtended", data[8]?.Trim()); // Trim leading and trailing whitespaces
                command.Parameters.AddWithValue("@AgencyClientID1", data[10]?.Trim()); // Trim leading and trailing whitespace
                command.Parameters.AddWithValue("@DownloadDate", DateTime.Now);
                command.Parameters.AddWithValue("@Extracted", 3);
                command.Parameters.AddWithValue("@ExtractionDate", new DateTime(2024, 6, 2));
                command.Parameters.AddWithValue("@CMSMatch", 2);
                command.Parameters.AddWithValue("@CMSMatchDate", new DateTime(2024, 5, 2));
                command.Parameters.AddWithValue("@CreatedBy", 6);
                command.Parameters.AddWithValue("@CreatedOn", DateTime.Now);  // Execute the command
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrames().FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame != null ? frame.GetFileLineNumber() : 0;
                LogError(ex.Message, GetCurrentFilePath(), ex.StackTrace, nameof(InsertClientData), fileName, lineNumber);
                throw;
            }

        }
        public bool InsertClientDataPHI(SqlConnection connection, string[] data, int batchid, string fileName)//Client table insertion

        {
            try
            {
                string ariesID = GetStringValue(data, 9); // Extract Aries ID from data array

                SqlCommand command = new SqlCommand("InsertIntoDlClientsPHI", connection);

                command.CommandType = CommandType.StoredProcedure;

                // Add parameters to the stored procedure
                command.Parameters.AddWithValue("@BatchID", batchid);
                command.Parameters.AddWithValue("@ClientID", GetStringData(data, 9)?.Trim('"'));
                command.Parameters.AddWithValue("@ClientFirstName", data[0]?.Trim()); // Trim leading and trailing whitespaces
                command.Parameters.AddWithValue("@ClientLastName", data[1]?.Trim()); // Trim leading and trailing whitespaces
                command.Parameters.AddWithValue("@ClientMiddleInitial", data[2]?.Trim()); // Trim leading and trailing whitespaces
                command.Parameters.AddWithValue("@ClientMothersMaidenNameFirstandThirdCharacters", data[3]?.Trim()); // Trim leading and trailing whitespaces

                DateTime clientDateOfBirth;
                string[] dateFormats = { "M/d/yyyy", "d-M-yyyy", "d/M/yyyy", "M-d-yyyy", "d-MMM-yyyy", "MMM-d-yyyy", "yyyy-M-d" };

                string[] parts = data[4].Split(' ');

                string dateString = parts[0].Replace("\"", "").Trim(); // Remove double quotes and trim

                if (dateString.Length > 0 && DateTime.TryParseExact(dateString, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out clientDateOfBirth))
                {
                    // If parsing succeeds, use the parsed date
                    command.Parameters.AddWithValue("@ClientDateofBirth", clientDateOfBirth);
                }
                else
                {
                    command.Parameters.AddWithValue("@ClientDateofBirth", DBNull.Value); // Add DBNull instead
                }
                command.Parameters.AddWithValue("@ClientGender", data[5]?.Trim()); // Trim leading and trailing whitespaces
                command.Parameters.AddWithValue("@ClientIsRelatedOrAffected", data[6]?.Trim()); // Trim leading and trailing whitespaces
                command.Parameters.AddWithValue("@ClientRecordIsShared", data[7]?.Trim()); // Trim leading and trailing whitespaces
                command.Parameters.AddWithValue("@ClientURNExtended", data[8]?.Trim()); // Trim leading and trailing whitespaces
                command.Parameters.AddWithValue("@AgencyClientID1", data[10]?.Trim()); // Trim leading and trailing whitespace
                command.Parameters.AddWithValue("@DownloadDate", DateTime.Now);
                command.Parameters.AddWithValue("@Extracted", 3);
                command.Parameters.AddWithValue("@ExtractionDate", new DateTime(2024, 6, 2));
                command.Parameters.AddWithValue("@CMSMatch", 2);
                command.Parameters.AddWithValue("@CMSMatchDate", new DateTime(2024, 5, 2));
                command.Parameters.AddWithValue("@CreatedBy", 6);
                command.Parameters.AddWithValue("@CreatedOn", DateTime.Now);  // Execute the command
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrames().FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame != null ? frame.GetFileLineNumber() : 0;
                LogError(ex.Message, GetCurrentFilePath(), ex.StackTrace, nameof(InsertClientData), fileName, lineNumber);
                throw;
            }
        }
        public void InsertdeceasedData(SqlConnection connection, string[] data, int batchid, string fileName)//Deceased Table Insertion
        {
            try
            {
                string ariesID = GetStringValue(data, 0); // Extract Aries ID from data array

                using (SqlCommand command = new SqlCommand("InsertIntoDlDeceasedClients", connection))

                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ClientID", GetStringValue(data, 0));
                    command.Parameters.AddWithValue("@BatchID", batchid);
                    command.Parameters.AddWithValue("@ClientLastFirstName", GetStringValue(data, 1));
                    command.Parameters.AddWithValue("@ClientStatus", GetStringValue(data, 2));
                    command.Parameters.AddWithValue("@StatusAsofDate", GetStringValue(data, 3));
                    command.Parameters.AddWithValue("@LastServiceDate", GetStringValue(data, 4));
                    command.Parameters.AddWithValue("@DownloadDate", DateTime.Parse("2024-08-01")); // Assuming 2024-08-01 is the correct date
                    command.Parameters.AddWithValue("@Extracted", Constants.Extracted); // Assuming 3 is a valid value for Extracted
                    command.Parameters.AddWithValue("@ExtractionDate", DateTime.Parse("2024-02-06")); // Assuming 2024-02-06 is the correct date
                    command.Parameters.AddWithValue("@CMSMatch", Constants.CMSMatchDate); // Assuming 2 is a valid value for CMSMatch
                    command.Parameters.AddWithValue("@CMSMatchDate", DateTime.Parse("2024-02-05")); // Assuming 2024-02-05 is the correct date
                    command.Parameters.AddWithValue("@CreatedBy", Constants.CreatedBy); // Assuming "Admin" is the correct creator
                    command.Parameters.AddWithValue("@CreatedOn", DateTime.Parse("2024-09-03")); // Assuming 2024-09-03 is the correct date
                    command.ExecuteNonQuery(); // Execute the SQL command to insert client data
                }
            }
            catch (Exception ex)
            {
                error = ex.Message; // Log error message
                Log($"{ex.Message}", Constants.ERROR, "InsertClientData", Constants.uploadct); // Assuming fileName is accessible herE
                throw;
            }
        }
        public void InsertConsentData(SqlConnection connection, string[] data, int batchid, string fileName)//Consent table insertion
        {
            try
            {
                // Parse date values from the CSV data
                DateTime? documentDate = ParseDateTime(GetStringValue(data, 5));
                DateTime? obtainDate = ParseDateTime(GetStringValue(data, 6));
                DateTime? expireDate = ParseDateTime(GetStringValue(data, 7));
                DateTime? eligibilityDocumentExpireDate = ParseDateTime(GetStringValue(data, 12));

                string clientLastFirstName = $"{GetStringValue(data, 2)} {GetStringValue(data, 3)}";

                using (SqlCommand command = new SqlCommand("InsertDLConsent", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@BatchID", batchid);
                    command.Parameters.AddWithValue("@ClientID", GetStringValue(data, 0)?.Trim('"'));
                    command.Parameters.AddWithValue("@AgencyClientID", GetStringValue(data, 1));
                    command.Parameters.AddWithValue("@ClientLastFirstName", clientLastFirstName);
                    command.Parameters.AddWithValue("@DocumentType", GetStringValue(data, 4)); // Assuming this is correct
                    command.Parameters.AddWithValue("@DocumentDate", (object)documentDate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ObtainDate", (object)obtainDate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ExpireDate", (object)expireDate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Source", GetStringValue(data, 8));
                    command.Parameters.AddWithValue("@CreatedSource", GetStringValue(data, 9));
                    command.Parameters.AddWithValue("@CreateAgency", GetStringValue(data, 10));
                    command.Parameters.AddWithValue("@ClientStatus", GetStringValue(data, 11));
                    command.Parameters.AddWithValue("@EligibilityDocumentExpireDate", (object)eligibilityDocumentExpireDate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@DownloadDate", DateTime.Now); // Assuming current date/time
                    command.Parameters.AddWithValue("@Extracted", Constants.Extracted); // Assuming a value for Extracted
                    command.Parameters.AddWithValue("@ExtractionDate", DateTime.Now); // Assuming current date/time
                    command.Parameters.AddWithValue("@CMSMatch", Constants.CMSMatchDate); // Assuming a value for CMSMatch
                    command.Parameters.AddWithValue("@CMSMatchDate", DateTime.Now); // Assuming current date/time
                    command.Parameters.AddWithValue("@CreatedBy", Constants.CreatedBy); // Assuming a value for CreatedBy
                    command.Parameters.AddWithValue("@CreatedOn", DateTime.Now); // Assuming current date/time

                    // Execute the query
                    command.ExecuteNonQuery();
                    // Increment totalRows counter
                }
            }
            catch (Exception ex)
            {

                error = ex.Message;
                Log($"{ex.Message}", Constants.ERROR, "ConsentData", Constants.uploadct); // Assuming fileName is accessible here
                throw;
            }
        }
        public void InsertDlEligibility(SqlConnection connection, string[] data, int batchid, string fileName)
        {
            try
            {
                // Parse date values from the CSV data
                DateTime? documentDate = ParseDateTime(GetStringValue(data, 5));
                DateTime? obtainDate = ParseDateTime(GetStringValue(data, 6));
                DateTime? expireDate = ParseDateTime(GetStringValue(data, 7));
                DateTime? eligibilityDocumentExpireDate = ParseDateTime(GetStringValue(data, 12));

                string clientLastFirstName = $"{GetStringValue(data, 2)} {GetStringValue(data, 3)}";

                using (SqlCommand command = new SqlCommand("InsertDlEligibility", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@BatchID", batchid);
                    command.Parameters.AddWithValue("@ClientID", GetStringValuedata(data, 0));
                    command.Parameters.AddWithValue("@AgencyClientID1", GetStringValuedata(data, 1));
                    command.Parameters.AddWithValue("@ClientLastFirstName", clientLastFirstName);
                    command.Parameters.AddWithValue("@DocumentType", GetStringValuedata(data, 4));
                    command.Parameters.AddWithValue("@DocumentDate", (object)documentDate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ObtainDate", (object)obtainDate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ExpireDate", (object)expireDate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Source", GetStringValuedata(data, 8));
                    command.Parameters.AddWithValue("@CreatedSource", GetStringValuedata(data, 9));
                    command.Parameters.AddWithValue("@CreateAgency", GetStringValuedata(data, 10));
                    command.Parameters.AddWithValue("@ClientStatus", GetStringValuedata(data, 11));
                    command.Parameters.AddWithValue("@EligibilityDocumentExpireDate", (object)eligibilityDocumentExpireDate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@DownloadDate", DateTime.Now);
                    command.Parameters.AddWithValue("@Extracted", Constants.Extracted);
                    command.Parameters.AddWithValue("@ExtractionDate", DateTime.Now);
                    command.Parameters.AddWithValue("@CMSMatch", Constants.CMSMatchDate);
                    command.Parameters.AddWithValue("@CMSMatchDate", DateTime.Now);
                    command.Parameters.AddWithValue("@CreatedBy", Constants.CreatedBy);
                    command.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                    // Execute the query
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Log($"{ex.Message}", Constants.ERROR, Constants.AriesEligibility, Constants.uploadct);
                throw;
            }
        }
        private DateTime? ParseDateTime(string value)
        {
            DateTime? result = null;
            if (!string.IsNullOrEmpty(value))
            {
                // Trim any extra quotation marks
                value = value.Trim('"');

                // Define potential date formats
                string[] dateFormats = { "MM/dd/yyyy hh:mm:ss tt", "MM-dd-yyyy HH:mm", "yyyy-MM-dd HH:mm:ss", "yyyy-MM-dd" };

                // Try parsing with specified formats
                if (DateTime.TryParseExact(value, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                {
                    result = parsedDate;
                }
                else
                {
                    Console.WriteLine($"Failed to parse date: {value}");
                }
            }
            return result;
        }

        private string GetStringValuedata(string[] data, int index)
        {
            return data.Length > index ? data[index].Trim('"') : string.Empty;
        }

        public void InsertDlServices(SqlConnection connection, string[] data, int batchid, string fileName, int rowNumber) // Services table insertion
        {
            try
            {
                using (SqlCommand command = new SqlCommand("InsertDlServices", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Extract service ID from service notes
                    string serviceNotes = GetStringData(data, 2)?.Trim('"'); // Assuming service notes are at index 2
                    string serviceID = ExtractServiceID(serviceNotes);

                    // Add parameters to the stored procedure
                    command.Parameters.AddWithValue("@ServiceID", serviceID);
                    command.Parameters.AddWithValue("@BatchID", batchid);
                    command.Parameters.AddWithValue("@ClientID", GetStringData(data, 0)?.Trim('"')); // Treat ClientID as string
                    command.Parameters.AddWithValue("@ClientURN", GetStringData(data, 1)?.Trim('"'));
                    command.Parameters.AddWithValue("@ServiceNotes", serviceNotes); // Original service notes
                    command.Parameters.AddWithValue("@DownloadDate", DateTime.Now);
                    command.Parameters.AddWithValue("@Extracted", Constants.Extracted);
                    command.Parameters.AddWithValue("@ExtractionDate", DBNull.Value);
                    command.Parameters.AddWithValue("@CMSMatch", Constants.CMSMatch);
                    command.Parameters.AddWithValue("@CMSMatchDate", DBNull.Value);
                    command.Parameters.AddWithValue("@CreatedBy", Constants.CreatedBy);
                    command.Parameters.AddWithValue("@CreatedOn", DateTime.Now);

                    command.ExecuteNonQuery(); // Execute the SQL command to insert data
                }
            }
            catch (Exception ex)
            {
                // Get detailed error information from the stack trace

                var st = new StackTrace(ex, true);
                var frame = st.GetFrames().FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame != null ? frame.GetFileLineNumber() : 0;
                LogError(ex.Message, GetCurrentFilePath(), ex.StackTrace, nameof(InsertDlServices), fileName, rowNumber);
            }
        }
        public void InsertDlServicesPHI(SqlConnection connection, string[] data, int batchid, string fileName, int rowNumber) // Services table insertion
        {
            try
            {
                using (SqlCommand command = new SqlCommand("InsertDlServicesPHI", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Extract service ID from service notes
                    string serviceNotes = GetStringData(data, 2)?.Trim('"'); // Assuming service notes are at index 2
                    string serviceID = ExtractServiceID(serviceNotes);

                    // Add parameters to the stored procedure
                    command.Parameters.AddWithValue("@ServiceID", serviceID);
                    command.Parameters.AddWithValue("@BatchID", batchid);
                    command.Parameters.AddWithValue("@ClientID", GetStringData(data, 0)?.Trim('"')); // Treat ClientID as string
                    command.Parameters.AddWithValue("@ClientURN", GetStringData(data, 1)?.Trim('"'));
                    command.Parameters.AddWithValue("@ServiceNotes", serviceNotes); // Original service notes
                    command.Parameters.AddWithValue("@DownloadDate", DateTime.Now);
                    command.Parameters.AddWithValue("@Extracted", Constants.Extracted);
                    command.Parameters.AddWithValue("@ExtractionDate", DBNull.Value);
                    command.Parameters.AddWithValue("@CMSMatch", Constants.CMSMatch);
                    command.Parameters.AddWithValue("@CMSMatchDate", DBNull.Value);
                    command.Parameters.AddWithValue("@CreatedBy", Constants.CreatedBy);
                    command.Parameters.AddWithValue("@CreatedOn", DateTime.Now);

                    command.ExecuteNonQuery(); // Execute the SQL command to insert data
                }
            }
            catch (Exception ex)
            {
                // Get detailed error information from the stack trace

                var st = new StackTrace(ex, true);
                var frame = st.GetFrames().FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                int lineNumber = frame != null ? frame.GetFileLineNumber() : 0;
                LogError(ex.Message, GetCurrentFilePath(), ex.StackTrace, nameof(InsertDlServices), fileName, rowNumber);
            }
        }
        private string ExtractServiceID(string serviceNotes) // extraction of Service Id
        {
            try
            {
                if (string.IsNullOrWhiteSpace(serviceNotes))
                {
                    throw new ArgumentException("Service notes cannot be null or empty.");
                }

                // Define patterns to match service ID
                string[] patterns = { "Id=", "ID=", "Id-", "Id:" };
                foreach (var pattern in patterns)
                {
                    if (serviceNotes.StartsWith(pattern, StringComparison.OrdinalIgnoreCase))
                    {
                        // Find the index of the first occurrence of "=", "-", or ":"
                        int separatorIndex = serviceNotes.IndexOfAny(new char[] { '=', '-', ':' });

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
                //MessageBox.Show(ex.Message);
                return null;
            }
        }
        private string GetStringData(string[] data, int index) //convert to string
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
        public void LogError(string message, string xmlFilePath, string stackTrace, string functionName, string fileName, int? lineNumber) // Logger table insertion for errors and completion
        {

            int maxStackLength = 1000; // Adjust this to match your database schema

            // Truncate the stack trace if it exceeds the maximum length
            if (stackTrace.Length > maxStackLength)
            {
                stackTrace = stackTrace.Substring(0, maxStackLength);
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Loggererror", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Type", Constants.ERROR); // Error type
                    cmd.Parameters.AddWithValue("@Module", Constants.HCC); // Module name
                    cmd.Parameters.AddWithValue("@Stack", stackTrace);
                    cmd.Parameters.AddWithValue("@Message", message);
                    cmd.Parameters.AddWithValue("@FileName", fileName);
                    cmd.Parameters.AddWithValue("@LineNumber", lineNumber.HasValue ? (object)lineNumber.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@FunctionName", functionName);
                    cmd.Parameters.AddWithValue("@Comments", DBNull.Value); // Assuming no comments
                    cmd.Parameters.AddWithValue("@CreatedBy", Constants.CreatedBy); // Assuming a specific user ID
                    cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Log an additional error if logging itself fails
                Console.WriteLine(ex.Message);
            }

            errorLogged = true; // Set errorLogged flag to true after logging the first error
        }
        public void UpdateBatchServices(int batchId, DateTime startTime, DateTime endTime, int AllTotalRows)//update batch data
        {//Updating status and Time on Batch Table     
            try
            {
                AllTotalRows++;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Define the SQL query to update the ConversionStartedAt and ConversionEndedAt columns
                    string updateQuery = "updateconversionservices";

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        // Set the parameters
                        command.Parameters.AddWithValue("@BatchID", batchId);
                        command.Parameters.AddWithValue("@ConversionStartedAt", startTime);
                        command.Parameters.AddWithValue("@ConversionEndedAt", endTime);
                        command.Parameters.AddWithValue("@AllTotalRows", AllTotalRows - 1);

                        // Execute the SQL update command
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating batch: {ex.Message}");
                // Log or handle the exception appropriately
            }
        }
        public void UpdateBatchclient(int batchId, DateTime startTime, DateTime endTime, int AllTotalRows)//update batch for client data
        {//Updating status and Time on Batch Table     
            try
            {
                AllTotalRows++;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Define the SQL query to update the ConversionStartedAt and ConversionEndedAt columns
                    string updateQuery = "updateconversionclient";

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        // Set the parameters
                        command.Parameters.AddWithValue("@BatchID", batchId);
                        command.Parameters.AddWithValue("@ConversionStartedAt", startTime);
                        command.Parameters.AddWithValue("@ConversionEndedAt", endTime);
                        command.Parameters.AddWithValue("@AllTotalRows", AllTotalRows - 1);

                        // Execute the SQL update command
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating batch: {ex.Message}");
                // Log or handle the exception appropriately
            }
        }
        public void InsertDlFinancials(SqlConnection connection, string[] data, int batchid, string fileName)//Financial data insertion
        {
            try
            {
                using (SqlCommand command = new SqlCommand("InsertDlFinancial", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters with proper null or empty value checks
                    command.Parameters.AddWithValue("@BatchID", batchid);
                    command.Parameters.AddWithValue("@ClientID", GetStringValue(data, 0)?.Trim('"'));
                    AddDecimalParameter(command, "@FinancialTotalIncomeMonthly", GetStringValue(data, 1));
                    AddDecimalParameter(command, "@FinancialTotalIncomeAnnual", GetStringValue(data, 2));
                    command.Parameters.AddWithValue("@FinancialIsClientIncomeMonthly", GetStringValue(data, 3)?.Trim('"'));
                    command.Parameters.AddWithValue("@FinancialEmploymentStatus", GetStringValue(data, 4)?.Trim('"'));
                    command.Parameters.AddWithValue("@FinancialPublicAssistance", GetStringValue(data, 5)?.Trim('"'));
                    AddDecimalParameter(command, "@FinancialEmploymentSalaryWages", GetStringValue(data, 6));
                    AddDecimalParameter(command, "@FinancialUnemploymentBenefits", GetStringValue(data, 7));
                    AddDecimalParameter(command, "@FinancialVeteransBenefits", GetStringValue(data, 8));
                    AddDecimalParameter(command, "@FinancialSSI", GetStringValue(data, 9));
                    AddDecimalParameter(command, "@FinancialSSDI", GetStringValue(data, 10));
                    AddDecimalParameter(command, "@FinancialSSA", GetStringValue(data, 11));
                    AddDecimalParameter(command, "@FinancialGeneralAssistance", GetStringValue(data, 12));
                    AddDecimalParameter(command, "@FinancialTANF", GetStringValue(data, 13));
                    AddDecimalParameter(command, "@FinancialFoodStamps", GetStringValue(data, 14));
                    AddDecimalParameter(command, "@FinancialStateDisability", GetStringValue(data, 15));
                    AddDecimalParameter(command, "@FinancialLongTermDisability", GetStringValue(data, 16));
                    AddDecimalParameter(command, "@FinancialGift", GetStringValue(data, 17));
                    AddDecimalParameter(command, "@FinancialRetirement", GetStringValue(data, 18));
                    AddDecimalParameter(command, "@FinancialAlimony", GetStringValue(data, 19));
                    AddDecimalParameter(command, "@FinancialInvestment", GetStringValue(data, 20));
                    AddDecimalParameter(command, "@FinancialWorkersCompensation", GetStringValue(data, 21));
                    command.Parameters.AddWithValue("@FinancialOther1", GetStringValue(data, 22)?.Trim('"'));
                    AddDecimalParameter(command, "@FinancialOtherAmount1", GetStringValue(data, 23));
                    command.Parameters.AddWithValue("@FinancialOther2", GetStringValue(data, 24)?.Trim('"'));
                    AddDecimalParameter(command, "@FinancialOtherAmount2", GetStringValue(data, 25));
                    command.Parameters.AddWithValue("@FinancialOther3", GetStringValue(data, 26)?.Trim('"'));
                    AddDecimalParameter(command, "@FinancialOtherAmount3", GetStringValue(data, 27));
                    command.Parameters.AddWithValue("@FinancialHasNoSourceOfIncome", GetStringValue(data, 28)?.Trim('"'));
                    AddDecimalParameter(command, "@FinancialHouseholdIncome", GetStringValue(data, 29));
                    command.Parameters.AddWithValue("@FinancialIsHouseholdIncomeMonthly", GetStringValue(data, 30)?.Trim('"'));
                    AddIntParameter(command, "@FinancialPeopleInHousehold", GetStringValue(data, 31));
                    AddIntParameter(command, "@FinancialChildrenInHousehold", GetStringValue(data, 32));
                    command.Parameters.AddWithValue("@FinancialHIVPositiveInHousehold", GetStringValue(data, 33)?.Trim('"'));
                    command.Parameters.AddWithValue("@FinancialHouseholdPovertyLevelbyGroup", GetStringValue(data, 34)?.Trim('"'));
                    command.Parameters.AddWithValue("@FinancialHouseholdPovertyLevel", GetStringValue(data, 35)?.Trim('"'));
                    AddDecimalParameter(command, "@FinancialFamilyIncome", GetStringValue(data, 36));
                    command.Parameters.AddWithValue("@FinancialIsFamilyIncomeMonthly", GetStringValue(data, 37)?.Trim('"'));
                    AddIntParameter(command, "@FinancialPeopleInFamily", GetStringValue(data, 38));
                    command.Parameters.AddWithValue("@FinancialFamilyPovertyLevel", GetStringValue(data, 39)?.Trim('"'));
                    command.Parameters.AddWithValue("@FinancialOwnsHouse", GetStringValue(data, 40)?.Trim('"'));
                    command.Parameters.AddWithValue("@FinancialOwnsCar", GetStringValue(data, 41)?.Trim('"'));
                    command.Parameters.AddWithValue("@FinancialHasOtherAssets", GetStringValue(data, 42)?.Trim('"'));
                    AddDecimalParameter(command, "@FinancialOtherAssets", GetStringValue(data, 43));
                    command.Parameters.AddWithValue("@FinancialLastSavedDate", GetStringValue(data, 44)?.Trim('"'));
                    command.Parameters.AddWithValue("@FinancialCreateSource", GetStringValue(data, 45)?.Trim('"'));
                    command.Parameters.AddWithValue("@DownloadDate", DateTime.Parse("2/3/4"));
                    command.Parameters.AddWithValue("@Extracted", Constants.Extracted);
                    command.Parameters.AddWithValue("@ExtractionDate", DateTime.Parse("2/3/4"));
                    command.Parameters.AddWithValue("@CMSMatch", Constants.CMSMatch);
                    command.Parameters.AddWithValue("@CMSMatchDate", DateTime.Parse("2/3/4"));
                    command.Parameters.AddWithValue("@CreatedBy", Constants.CreatedBy);
                    command.Parameters.AddWithValue("@CreatedOn", DateTime.Now); // Or provide the appropriate DateTime value

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                error = $"Error inserting client parameters into the table: {ex.Message}\n{ex.StackTrace}";
                Console.WriteLine(error);
                Log($"{ex.Message}", Constants.ERROR, "DlFinancials", Constants.uploadct); // Assuming fileName is accessible here
                throw;// Log the error
            }
        }
        private void AddDecimalParameter(SqlCommand command, string parameterName, string value)//parse decimaL value
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
                        Console.WriteLine($"Invalid decimal value: {value}");
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
        private void AddIntParameter(SqlCommand command, string parameterName, string value)//add parameters
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
                        Console.WriteLine($"Invalid integer value: {value}");
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
        private string GetStringValue(string[] data, int index)//convert to string
        {
            if (index >= 0 && index < data.Length)
            {
                return data[index];
            }
            return null;
        }
        private DateTime? GetDateTimeValue(string[] data, int index)//convert to date 
        {
            if (index >= 0 && index < data.Length)
            {
                if (DateTime.TryParse(data[index], out DateTime result))
                {
                    return result;
                }
            }
            return null; // Return null if the conversion fails or the index is out of range
        }
        private int? GetIntValue(string[] data, int index)//convert to int
        {
            if (index >= 0 && index < data.Length)
            {
                if (int.TryParse(data[index], out int result))
                {
                    return result;
                }
            }
            return null; // Return null if the conversion fails or the index is out of range
        }
        private decimal? GetDecimalValue(string[] data, int index)//parse to decimal value
        {
            if (decimal.TryParse(data[index], out decimal result))
            {
                return result;
            }
            return null; // Return null if parsing fails
        }
        private bool? GetBooleanValue(string[] data, int index)//convert to boolean
        {
            if (bool.TryParse(data[index], out bool result))
            {
                return result;
            }
            return null; // Return null if parsing fails
        }
        public int InsertClients(XmlDocument xmlDoc, int batchId, SqlConnection conn, string xmlFilePath, bool value)//insert clients
        {
            string fileName = Path.GetFileName(xmlFilePath);
            int insertedCount = 0;

            //using (SqlTransaction trans = conn.BeginTransaction())
            //{
                try
                {
                    // Create a command for the stored procedure within the transaction
                    SqlCommand cmd = conn.CreateCommand();
                    //cmd.Transaction = trans; // Assign the transaction to the command
                    cmd.CommandType = CommandType.StoredProcedure;
                    var procedure = value ? "InsertCTClientsFromXMLPHIMASKINGTEST" : "InsertCTClientsFromXML";
                    cmd.CommandText = procedure;

                    // Set the parameters for the stored procedure
                    SqlParameter xmlDataParam = new SqlParameter("@XmlData", SqlDbType.Xml);
                    xmlDataParam.Value = new SqlXml(new XmlTextReader(new StringReader(xmlDoc.OuterXml)));
                    cmd.Parameters.Add(xmlDataParam);

                    cmd.Parameters.AddWithValue("@BatchID", batchId);

                    // Execute the command to insert clients
                    cmd.ExecuteNonQuery();

                    // Commit the transaction
                    //trans.Commit();

                    // Return the number of inserted clients
                    return insertedCount++;
                }
                catch (Exception ex)
                {
                    // Rollback the transaction if any error occurs
                    //trans.Rollback();
                    var st = new StackTrace(ex, true);
                    var frame = st.GetFrames().FirstOrDefault(f => !string.IsNullOrEmpty(f.GetFileName()));
                    int lineNumber = frame != null ? frame.GetFileLineNumber() : 0;
                    //LogError(ex.Message, GetCurrentFilePath(), ex.StackTrace, nameof(InsertClients), fileName, lineNumber);
                  //  LogError($"{ex.Message}", fileName); // Handle the exception within the transaction (e.g., log it)
                    throw;
                }
            }
        
        public int InsertEligibilityDocuments(XmlDocument xmlDoc, int batchId, SqlConnection conn, string xmlFilePath)//insertion of eligibility document from xml file
        {
            int insertedCount = 0;
            string fileName = Path.GetFileName(xmlFilePath);
            try
            {
                // Process each Client node in the XML document
                foreach (XmlNode clientNode in xmlDoc.SelectNodes("//Client"))
                {
                    XmlAttribute ariesIDAttribute = clientNode.Attributes["ariesID"];
                    if (ariesIDAttribute != null && int.TryParse(ariesIDAttribute.Value, out int ariesID))
                    {
                        // Check if the Client node contains EligibilityDocument nodes
                        if (clientNode.SelectNodes("EligibilityDocument").Count > 0)
                        {
                            XmlNode agencySpecificsNode = clientNode.SelectSingleNode("AgencySpecifics");
                            if (agencySpecificsNode != null && agencySpecificsNode.Attributes != null)
                            {
                                int agencyClientID = int.Parse(agencySpecificsNode.Attributes["agencyClientID1"].Value);

                                // Check if the AgencyClientID and AriesID pair should be inserted
                                if (ShouldInsertAgencyClient(agencyClientID, ariesID, xmlDoc))
                                {
                                    // Process each EligibilityDocument node within the Client node
                                    foreach (XmlNode eligibilityNode in clientNode.SelectNodes("EligibilityDocument"))
                                    {
                                        string documentType = GetAttributeValue(eligibilityNode, "documentType");
                                        DateTime? documentDate = GetNullableDateTimeAttributeValue(eligibilityNode, "documentDate");
                                        DateTime? obtainDate = GetNullableDateTimeAttributeValue(eligibilityNode, "obtainDate");
                                        DateTime? expireDate = GetNullableDateTimeAttributeValue(eligibilityNode, "expireDate");
                                        string source = GetAttributeValue(eligibilityNode, "source");
                                        string notes = GetAttributeValue(eligibilityNode, "notes");

                                        // Prepare and execute SQL command to insert into EligibilityDocuments table within the transaction
                                        using (SqlCommand insertCmd = new SqlCommand(
                                            "INSERT INTO [RWDE].[dbo].[CTClientsEligibilityDoc] " +
                                            "(DocumentType, DocumentDate, ObtainDate, ExpireDate, Source, Notes, BatchID, AgencyClientID1, AriesID, CreatedBy, CreatedOn, EligibilityDocID) " +
                                            "VALUES (@DocumentType, @DocumentDate, @ObtainDate, @ExpireDate, @Source, @Notes, @BatchID, @AgencyClientID1, @AriesID, @CreatedBy, @CreatedOn, @EligibilityDocID)",
                                            conn))
                                        {
                                            // Set SQL parameters based on the values extracted from the XML document
                                            insertCmd.Parameters.AddWithValue("@DocumentType", string.IsNullOrEmpty(documentType) ? (object)DBNull.Value : documentType);
                                            insertCmd.Parameters.AddWithValue("@DocumentDate", documentDate ?? (object)DBNull.Value);
                                            insertCmd.Parameters.AddWithValue("@ObtainDate", obtainDate ?? (object)DBNull.Value);
                                            insertCmd.Parameters.AddWithValue("@ExpireDate", expireDate ?? (object)DBNull.Value);
                                            insertCmd.Parameters.AddWithValue("@Source", string.IsNullOrEmpty(source) ? (object)DBNull.Value : source);
                                            insertCmd.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(notes) ? (object)DBNull.Value : notes);
                                            insertCmd.Parameters.AddWithValue("@BatchID", batchId);
                                            insertCmd.Parameters.AddWithValue("@AgencyClientID1", agencyClientID);
                                            insertCmd.Parameters.AddWithValue("@AriesID", ariesID);
                                            insertCmd.Parameters.AddWithValue("@CreatedBy", Constants.CreatedBy); // Assuming a constant value for CreatedBy
                                            insertCmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now); // Current date/time
                                            insertCmd.Parameters.AddWithValue("@EligibilityDocID", insertedCount % 3 + 1); // Incremental EligibilityDocumentID

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
                // Log error for the main insertion process
                Console.WriteLine("Error inserting eligibility documents: " + ex.Message);
                LogError($"{ex.Message}", fileName);
                // Rollback the transaction on error to ensure transactional integrity
                //transaction.Rollback();

                // Return zero inserted count to indicate failure
                return 0;
            }

            return insertedCount;
        }

        // Helper method to determine if AgencyClientID and AriesID pair should be inserted
        private bool ShouldInsertAgencyClient(int agencyClientID, int ariesID, XmlDocument xmlDoc)
        {
            // Check if the AgencyClientID and AriesID pair has associated EligibilityDocument nodes in the XML
            XmlNodeList eligibilityNodes = xmlDoc.SelectNodes($"//Client[@ariesID='{ariesID}']/EligibilityDocument");

            return eligibilityNodes != null && eligibilityNodes.Count > 0;
        }
        private string GetAttributeValue(XmlNode node, string attributeName)//getting value
        {
            if (node != null && node.Attributes != null && node.Attributes[attributeName] != null)
            {
                return node.Attributes[attributeName].Value;
            }
            return null;
        }
        // Helper method to parse nullable DateTime attribute value from XML node
        private DateTime? GetNullableDateTimeAttributeValue(XmlNode node, string attributeName)//to get correct time format
        {
            string attributeValue = GetAttributeValue(node, attributeName);
            if (!string.IsNullOrEmpty(attributeValue) && DateTime.TryParse(attributeValue, out DateTime result))
            {
                return result;
            }
            return null;
        }
        // Helper method to determine if a client should be processed based on the criteria of the first eligible client
        private bool ShouldProcessClient(int currentAriesID, int eligibleAriesID, XmlDocument xmlDoc)
        {
            // Check if the current client's AriesID matches the eligible client's AriesID
            return currentAriesID == eligibleAriesID;
        }
        public int InsertServiceLineItems(XmlDocument xmlDoc, int batchId, SqlConnection conn, string xmlFilePath)//insertion of service table from xml file
        {
            int insertedCount = 0;
            string fileName = Path.GetFileName(xmlFilePath);
            try
            {
                // Process each ServiceLineItem node in the XML document
                foreach (XmlNode serviceNode in xmlDoc.SelectNodes("//ServiceLineItem"))
                {
                    // Create SqlCommand for the stored procedure within the transaction
                    using (SqlCommand insertCmd = new SqlCommand("InsertServiceLineItemFromXMLPHI", conn))
                    {
                        insertCmd.CommandType = CommandType.StoredProcedure;
                        string clientAriesID = GetAttributeValue(serviceNode, "_clientAriesID");
                        if (clientAriesID == null)
                        {
                            insertCmd.Parameters.AddWithValue("@ClientAriesID", DBNull.Value);
                        }
                        else
                        {
                            insertCmd.Parameters.AddWithValue("@ClientAriesID", clientAriesID);
                        }
                        // Set parameters for the stored procedure based on node attributes
                        //insertCmd.Parameters.AddWithValue("@ClientAriesID", GetAttributeValue(serviceNode, "_clientAriesID"));
                        insertCmd.Parameters.AddWithValue("@ClientURNExt", GetAttributeValue(serviceNode, "_clientURNExt"));
                        insertCmd.Parameters.AddWithValue("@SiteName", GetAttributeValue(serviceNode, "_siteName"));
                        insertCmd.Parameters.AddWithValue("@StaffLogin", GetAttributeValue(serviceNode, "_staffLogin"));
                        insertCmd.Parameters.AddWithValue("@ContractName", GetAttributeValue(serviceNode, "_contractName"));
                        insertCmd.Parameters.AddWithValue("@ServiceDate", GetDateTimeValue(serviceNode, "serviceDate"));
                        insertCmd.Parameters.AddWithValue("@Program", GetAttributeValue(serviceNode, "program"));
                        insertCmd.Parameters.AddWithValue("@PrimaryService", GetAttributeValue(serviceNode, "primaryService"));
                        insertCmd.Parameters.AddWithValue("@SecondaryService", GetAttributeValue(serviceNode, "secondaryService"));
                        insertCmd.Parameters.AddWithValue("@Subservice", GetAttributeValue(serviceNode, "subservice"));
                        insertCmd.Parameters.AddWithValue("@UnitsOfService", GetDecimalValue(serviceNode, "unitsOfService"));
                        insertCmd.Parameters.AddWithValue("@RateForUnitOfService", GetDecimalValue(serviceNode, "rateForUnitOfService"));
                        insertCmd.Parameters.AddWithValue("@MeasurementUnit", GetAttributeValue(serviceNode, "measurementUnit"));
                        insertCmd.Parameters.AddWithValue("@TotalCost", GetDecimalValue(serviceNode, "totalCost"));
                        insertCmd.Parameters.AddWithValue("@Notes", GetAttributeValue(serviceNode, "notes"));
                        insertCmd.Parameters.AddWithValue("@BatchID", batchId);
                        insertCmd.Parameters.AddWithValue("@CreatedBy", Constants.CreatedBy); // Assuming a constant value for CreatedBy
                        insertCmd.Parameters.AddWithValue("@ActualMinutesSpent", GetAttributeValueOrDefault<object>(serviceNode, "actualTimeSpentMinutes", DBNull.Value));
                        insertCmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);

                        // Extract and set ServiceID
                        string serviceID = ExtractServiceId(serviceNode);
                        if (!string.IsNullOrEmpty(serviceID))
                        {
                            insertCmd.Parameters.AddWithValue("@ServiceID", serviceID);
                        }
                        insertCmd.ExecuteNonQuery();
                        insertedCount++; // Increment inserted count for each service line item
                    }
                }
            }

            catch (Exception ex)
            {
                // Log error for the main insertion process
                LogError($"Error inserting service line items: {ex.Message}", fileName);

                // Rollback the transaction on error to ensure transactional integrity
                //transaction.Rollback();

                // Return zero inserted count to indicate failure
            }
            finally
            {
                conn.Close(); // Close connection in case of any exception
            }

            return insertedCount;
        }

        private T GetAttributeValue<T>(XmlNode node, string attributeName)//getting value
        {
            if (node == null || string.IsNullOrEmpty(attributeName))//
            {
                return default(T);
            }

            XmlNode attributeNode = node.Attributes.GetNamedItem(attributeName);
            if (attributeNode != null && !string.IsNullOrEmpty(attributeNode.Value))
            {
                return (T)Convert.ChangeType(attributeNode.Value, typeof(T));
            }

            return default(T);
        }
        private T GetAttributeValueOrDefault<T>(XmlNode node, string attributeName, T defaultValue)//Getting values
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
        private DateTime GetDateTimeValue(XmlNode node, string attributeName)//getting date value
        {
            string dateString = GetAttributeValue<string>(node, attributeName);
            if (!string.IsNullOrEmpty(dateString))
            {
                DateTime result;
                if (DateTime.TryParse(dateString, out result))
                {
                    return result;
                }
            }
            return DateTime.MinValue; // or throw an exception if parsing fails
        }
        private decimal GetDecimalValue(XmlNode node, string attributeName)//getting decimal value
        {
            string decimalString = GetAttributeValue<string>(node, attributeName);
            decimal result;
            if (!string.IsNullOrEmpty(decimalString) && decimal.TryParse(decimalString, out result))
            {
                return result;
            }
            return 0; // or throw an exception if parsing fails
        }

        private bool errorLogged = false;
        public void LogError(string message, string xmlFilePath)//Looger table insertion for errors and completion
        {
            if (errorLogged)
                return; // Abort further logging if an error has already been logged

            string fileName = Path.GetFileName(xmlFilePath);
            string stackTrace = Environment.StackTrace;
            string functionName = new StackTrace(true).GetFrame(1).GetMethod().Name;
            int lineNumber = new StackTrace(true).GetFrame(1).GetFileLineNumber();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Logger (Type, Module, Stack, Message, FileName, LineNumber, FunctionName, Comments, CreatedBy, CreatedOn) " +
                                                     "VALUES (@Type, @Module, @Stack, @Message, @FileName, @LineNumber, @FunctionName, @Comments, @CreatedBy, @CreatedOn)", conn);
                    cmd.Parameters.AddWithValue("@Type", Constants.Error); // Error type
                    cmd.Parameters.AddWithValue("@Module", Constants.Module); // Module name
                    cmd.Parameters.AddWithValue("@Stack", stackTrace);
                    cmd.Parameters.AddWithValue("@Message", message);
                    cmd.Parameters.AddWithValue("@FileName", fileName);
                    cmd.Parameters.AddWithValue("@LineNumber", lineNumber);
                    cmd.Parameters.AddWithValue("@FunctionName", functionName);
                    cmd.Parameters.AddWithValue("@Comments", null);
                    cmd.Parameters.AddWithValue("@CreatedBy", Constants.CreatedBy); // Assuming a specific user ID
                    cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Log an additional error if logging itself fails
                Console.WriteLine(ex.Message);
            }

            errorLogged = true; // Set errorLogged flag to true after logging the first error
        }
        private string ExtractServiceId(XmlNode serviceNode)//extracting variables according to the format
        {
            string notes = GetAttributeValue(serviceNode, "notes");

            // Check if notes attribute starts with "Id=", "ID=", "Id-", or "Id:"
            if (notes.StartsWith("Id=") || notes.StartsWith("ID=") || notes.StartsWith("Id-") || notes.StartsWith("Id:"))
            {
                // Find the index of the first occurrence of "=", "-", or ":"
                int separatorIndex = notes.IndexOfAny(new char[] { '=', '-', ':' });

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
        public decimal? GetDecimalValueFromXml(XmlNode node, string attributeName)//extracting decimal value
        {
            try
            {

                if (node != null && node.Attributes != null)
                {
                    XmlAttribute attribute = node.Attributes[attributeName];
                    if (attribute != null && !string.IsNullOrWhiteSpace(attribute.Value))
                    {
                        string valueString = attribute.Value.Trim(); // Trim whitespace

                        if (decimal.TryParse(valueString, out decimal parsedValue))
                        {
                            return parsedValue;
                        }
                        else
                        {
                            // Handle parsing failure gracefully
                            throw new ArgumentException($"Invalid decimal format for attribute '{attributeName}': '{valueString}'");
                        }
                    }
                }

                return null; // Return null if node or attribute is missing or attribute value is empty
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        private int GetIntValueFromXml(XmlNode node, string attributeName)//extracting int value
        {
            if (node != null && node.Attributes[attributeName] != null)
            {
                int result;
                if (int.TryParse(node.Attributes[attributeName].Value, out result))
                {
                    return result;
                }
            }
            return 0; // Default value if attribute not found or cannot be parsed
        }
        private DateTime? GetDateTimeValueFromXml(XmlNode node, string attributeName)//extracting time in given format
        {
            if (node != null && node.Attributes[attributeName] != null)
            {
                string dateString = node.Attributes[attributeName].Value;
                if (!string.IsNullOrWhiteSpace(dateString))
                {
                    if (DateTime.TryParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                    {
                        return parsedDate;
                    }
                    else
                    {
                        throw new ArgumentException($"Invalid date format: {dateString}");
                    }
                }
            }
            return null; // Return null if attribute is missing or empty
        }
        private bool GetBoolValueFromXml(XmlNode node, string attributeName)//extracting boolean value
        {
            if (node != null && node.Attributes[attributeName] != null)
            {
                bool result;
                if (bool.TryParse(node.Attributes[attributeName].Value, out result))
                {
                    return result;
                }
            }
            return false; // Default value if attribute not found or cannot be parsed
        }
        private string GetStringValueFromXml(XmlNode node, string elementName)//extracting string value
        {
            if (node != null && node[elementName] != null)
            {
                return node[elementName].InnerText;
            }
            return string.Empty; // Default value if node or element not found
        }
        private DateTime ParseXmlDate(string dateString)//parse date value
        {
            Console.WriteLine($"Parsing date string: '{dateString}'");
            DateTime parsedDate;
            if (DateTime.TryParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
            {
                return parsedDate;
            }
            else
            {
                throw new ArgumentException($"Invalid date format: {dateString}");
            }
        }
        // Method to log messages with log type provided by RWDEFileUploads
        public void Log(string message, int type, string baseFilename, int module)//insertion into log table

        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("InsertLog", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Type", type);
                    command.Parameters.AddWithValue("@Message", message);
                    command.Parameters.AddWithValue("@Module", module);
                    command.Parameters.AddWithValue("@FileName", baseFilename);
                    command.Parameters.AddWithValue("@LineNumber", DBNull.Value);
                    command.Parameters.AddWithValue("@FunctionName", DBNull.Value);
                    command.Parameters.AddWithValue("@Comments", DBNull.Value);

                    command.Parameters.AddWithValue("@CreatedBy", 100);
                    command.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging message: {ex.Message}");
            }
        }
        public void DeleteBatchFromDatabase(string batchID, string Type)//delete data for particular batchid
        {
            try
            {
                string connectionString = GetConnectionString();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //  string fileName = dataGridView.Rows[selectedRowIndex].Cells["fileName"].Value.ToString();

                    // Adjust the stored procedure name based on the batch type
                    if (Type == "Client Track")
                        using (SqlCommand command = new SqlCommand("DeleteBatchData", connection))
                        {
                            connection.Open();
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@BatchID", batchID);
                            command.ExecuteNonQuery();
                            connection.Close();
                        }

                    else if (Type == "OCHIN")
                    {

                        using (SqlCommand command = new SqlCommand("OCHINDATADELETE", connection))
                        {
                            connection.Open();
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@BatchID", batchID);
                            command.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                    else if (Type == "OCHIN")
                    {

                        using (SqlCommand command = new SqlCommand("OCHINDATADELETE", connection))
                        {
                            connection.Open();
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@BatchID", batchID);
                            // command.Parameters.AddWithValue("@filename","Client");
                            command.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                    else
                    {
                        using (SqlCommand command = new SqlCommand("HCCDATADELETE", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            connection.Open();
                            command.Parameters.AddWithValue("@BatchID", batchID);
                            command.ExecuteNonQuery();
                            connection.Close();

                        }
                    }
                }
            }


            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting batch: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void DeleteBatch(string batchID)//Delete All Values form all tables 
        {
            string connectionString = GetConnectionString();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    using (SqlCommand command = new SqlCommand("DeleteHccBatchData", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BatchID", batchID);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception (logging, rethrow, etc.)
                MessageBox.Show(ex.Message);
            }
        }
        public void DeleteBatchochin(string batchID)//Delete All Values form all tables 
        {
            string connectionString = GetConnectionString();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    using (SqlCommand command = new SqlCommand("DeleteochinBatchDatas", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BatchID", batchID);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception (logging, rethrow, etc.)
                MessageBox.Show(ex.Message);
            }
        }
        public void DeleteBatch(string batchID, string type)//Delete All Values form all tables 
        {
            string connectionString = GetConnectionString();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string storedProcedure = GetStoredProcedureByType(type);

                    using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BatchID", batchID);
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception (logging, rethrow, etc.)
                throw new Exception($"Error deleting batch: {batchID}", ex);
            }
        }
        private string GetStoredProcedureByType(string type)//Calling storeProcedure 
        {
            // Adjust the stored procedure name based on the batch type
            if (type == "Client Track")
            {
                return "DeleteBatchData";
            }
            else if (type == "HCC")
            {
                return "DeleteHccBatchData";
            }
            else if (type == "OCHIN")
            {
                return "OCHINDATADELETE";
            }
            else
            {
                return "OCHINDATADELETE";
            }
        }
        public DataTable GetAllBatches()//Get all Values from Batch Table
        {
            DataTable dataTable = new DataTable();
            string connectionString = GetConnectionString();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("ViewAllBatchDatas", connection))
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
                // Handle exception (logging, rethrow, etc.)
                throw new Exception("Error retrieving batch data", ex);
            }

            return dataTable;
        }

        public DataTable GetAllBatchesLOAD()//Get all Values from Batch Table
        {
            DataTable dataTable = new DataTable();
            string connectionString = GetConnectionString();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("ViewAllBatchDatasLOAD", connection))
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
                // Handle exception (logging, rethrow, etc.)
                throw new Exception("Error retrieving batch data", ex);

            }
            return dataTable;
        }

        public DataTable GetAllBatcheshcc()//Get all Values from Batch Table
        {
            DataTable dataTable = new DataTable();
            string connectionString = GetConnectionString();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("ViewAllBatchDatasHCC", connection))
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
                // Handle exception (logging, rethrow, etc.)
                throw new Exception("Error retrieving batch data", ex);
            }

            return dataTable;
        }


        public int GetNextBatchId(bool batchId) // To get next batch Id from the database
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("SELECT ISNULL(MAX(BatchID), 0) FROM Batch", connection);
                    int lastBatchIDFromDB = (int)command.ExecuteScalar();

                    if (batchId || !batchIDIncremented)
                    {
                        lastBatchIDFromDB++;
                        batchIDIncremented = true;
                    }

                    return lastBatchIDFromDB;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }
        public DataTable GetParticularDataFromBatchTable(string BatchType, DateTime FromDate, DateTime EndDate)//extraction of particular batch from data
        {
            DataTable dataTable = new DataTable();
            string connectionString = GetConnectionString();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("GetParticularBatchDatas", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BatchType", BatchType);
                    command.Parameters.AddWithValue("@FromDate", FromDate);
                    command.Parameters.AddWithValue("@EndDate", EndDate);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception (logging, rethrow, etc.)
                throw new Exception("Error retrieving batch data", ex);
            }

            return dataTable;
        }
        public List<string> GetAllBatchTypes()//get all batches type
        {
            List<string> batchTypeValues = new List<string>();

            try
            {
                string query = "GETALLBATCHTYPE";

                using (SqlConnection sql = new SqlConnection(connectionString))
                {
                    using (SqlCommand com = new SqlCommand(query, sql))
                    {
                        sql.Open();

                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                batchTypeValues.Add(reader["Value"].ToString());
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                // Optionally, log the exception
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // The 'using' statements ensure that the connection is closed, so no additional action is needed here.
            }

            return batchTypeValues;
        }

        public List<string> GetAllBatchTypesHCC()//get all batches type
        {
            List<string> batchTypeValues = new List<string>();

            try
            {
                string query = "GETALLBATCHTYPEHCC";

                using (SqlConnection sql = new SqlConnection(connectionString))
                {
                    using (SqlCommand com = new SqlCommand(query, sql))
                    {
                        sql.Open();

                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                batchTypeValues.Add(reader["Value"].ToString());
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                // Optionally, log the exception
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // The 'using' statements ensure that the connection is closed, so no additional action is needed here.
            }

            return batchTypeValues;
        }
        public List<string> GetAllBatchTypesview()//get all batches type
        {
            List<string> batchTypeValues = new List<string>();

            try
            {
                string query = "GETALLBATCHTYPEview";

                using (SqlConnection sql = new SqlConnection(connectionString))
                {
                    using (SqlCommand com = new SqlCommand(query, sql))
                    {
                        sql.Open();

                        using (SqlDataReader reader = com.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                batchTypeValues.Add(reader["Value"].ToString());
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                // Optionally, log the exception
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // The 'using' statements ensure that the connection is closed, so no additional action is needed here.
            }

            return batchTypeValues;
        }
        public DataTable GetParticularConversionDatas(string BatchType, DateTime FromDate, DateTime EndDate)//retrieve data for according to the start and end date
        {
            DataTable dataTable = new DataTable();
            string connectionString = GetConnectionString();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("GetParticularConversionDatas", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BatchType", BatchType);
                    command.Parameters.AddWithValue("@FromDate", FromDate);
                    command.Parameters.AddWithValue("@EndDate", EndDate);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception (logging, rethrow, etc.)
                MessageBox.Show(ex.Message);
            }

            return dataTable;
        }
        public DataTable GetParticularnGenerationDatas(string BatchType, DateTime FromDate, DateTime EndDate)//get generation data according to start and end time
        {
            DataTable dataTable = new DataTable();
            string connectionString = GetConnectionString();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("GetParticularnGenerationDatasCONVERSIONxml", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BatchType", BatchType);
                    command.Parameters.AddWithValue("@FromDate", FromDate);
                    command.Parameters.AddWithValue("@EndDate", EndDate);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception (logging, rethrow, etc.)
                throw new Exception("Error retrieving batch data", ex);
            }

            return dataTable;
        }
        public DataTable GetParticularnGenerationDatasconversion(string BatchType, DateTime FromDate, DateTime EndDate)//get generation data according to start and end time
        {
            DataTable dataTable = new DataTable();
            string connectionString = GetConnectionString();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("GetParticularnGenerationDatasCONVERSION", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BatchType", BatchType);
                    command.Parameters.AddWithValue("@FromDate", FromDate);
                    command.Parameters.AddWithValue("@EndDate", EndDate);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception (logging, rethrow, etc.)
                throw new Exception("Error retrieving batch data", ex);
            }

            return dataTable;
        }

        public void UpdateBatch()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("GetParticularnGenerationDatasCONVERSIONHCC", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        
    }
    public void DeleteHCCABORTED(int batchid)
    {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("DeleteHCCABORTED", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BATCHID", batchid);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

    }

        public DataTable GetParticularnGenerationDatasHCC(string BatchType, DateTime FromDate, DateTime EndDate)//get generation data according to start and end time
        {
            DataTable dataTable = new DataTable();
            string connectionString = GetConnectionString();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("GetParticularnGenerationDatasCONVERSIONHCC", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BatchType", BatchType);
                    command.Parameters.AddWithValue("@FromDate", FromDate);
                    command.Parameters.AddWithValue("@EndDate", EndDate);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception (logging, rethrow, etc.)
                throw new Exception("Error retrieving batch data", ex);
            }

            return dataTable;
        }
        public DataTable GetAllContractLists()//get data for allcontractsid
        {
            // Create a new DataTable to store the results
            DataTable dataTable = new DataTable();

            // Define the name of the stored procedure
            string storedProcedureName = "GetAllContractLists"; // Change to the actual stored procedure name

            try
            {
                // Use a using statement to ensure the SqlConnection is properly disposed of after use
                using (SqlConnection com = new SqlConnection(connectionString))
                {
                    // Open the database connection
                    com.Open();

                    // Use a using statement to ensure the SqlCommand is properly disposed of after use
                    using (SqlCommand sql = new SqlCommand(storedProcedureName, com))
                    {
                        // Specify that the SqlCommand is a stored procedure
                        sql.CommandType = CommandType.StoredProcedure;

                        // Use a using statement to ensure the SqlDataAdapter is properly disposed of after use
                        using (SqlDataAdapter adapter = new SqlDataAdapter(sql))
                        {
                            // Fill the DataTable with the results of the stored procedure
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Catch any exceptions and write the error message to the console
                Console.WriteLine(ex.Message);
            }

            // Return the populated DataTable
            return dataTable;
        }
        public DataTable GetAllServiceLists()//to retrive the service list data
        {
            DataTable dataTable = new DataTable();
            string query = "sp_GetTopServiceCodeSetup";

            try
            {
                using (SqlConnection com = new SqlConnection(connectionString))
                {
                    com.Open();
                    using (SqlCommand sql = new SqlCommand(query, com))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(sql))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }

                if (dataTable.Rows.Count == 0)
                {
                    Console.WriteLine("Query executed, but no data found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return dataTable;
        }
        public List<Dictionary<string, string>> GetServiceDatas(int batchId) //Get All Service Values from ServiceGenerator Procedure
        {
            try
            {
                var results = new List<Dictionary<string, string>>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(Constants.ServiceXmlGenerationQuery, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@batchId", batchId);
                        connection.Open();
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
                }
                return results;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        } 
        public List<Dictionary<string, string>> getServices(int batchId) //Get All Service Values from ServiceGenerator Procedure
        {
            try
            {
                var results = new List<Dictionary<string, string>>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(Constants.ServiceXmlGenerationQuery, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@batchId", batchId);//
                        connection.Open();
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
                                using (SqlConnection con = new SqlConnection(connectionString))
                                {
                                    using (SqlCommand cmd = new SqlCommand("insertXMLgeneratortimeServices", con))
                                    {
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        DateTime date = DateTime.Now;
                                        cmd.Parameters.AddWithValue("@Clientid", Convert.ToInt32(reader[5])); // Convert clientid to int
                                        cmd.Parameters.AddWithValue("@Datetime", date);
                                        con.Open();
                                        // Execute the second command here, after the reader is done with the row data
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                    }
                                }
                            }
                        }
                    }
                }
                return results;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public List<Dictionary<string, string>> getServiceserror(int batchId) //Get All Service Values from ServiceGenerator Procedure
        {
            try
            {
                var results = new List<Dictionary<string, string>>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(Constants.ServicegeneratorERROR, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BatchId", batchId);//
                        connection.Open();
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
                                using (SqlConnection con = new SqlConnection(connectionString))
                                {
                                    using (SqlCommand cmd = new SqlCommand("insertXMLgeneratortimeServices", con))
                                    {
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        DateTime date = DateTime.Now;
                                        cmd.Parameters.AddWithValue("@Clientid", Convert.ToInt32(reader[5])); // Convert clientid to int
                                        cmd.Parameters.AddWithValue("@Datetime", date);
                                        con.Open();
                                        // Execute the second command here, after the reader is done with the row data
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                    }
                                }
                            }
                        }
                    }
                }
                return results;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public List<Dictionary<string, string>> getXmlStructure() //Get All XmlServiceStructure Values from Xmlstructure Table
        {
            try
            {
                var results = new List<Dictionary<string, string>>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(Constants.XmlStructureServiceValues, connection))
                    {
                        connection.Open();
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
                }
                return results;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public DataTable GetDataFromDatabase()//get clients rows count from table
        {
            DataTable dataTable = new DataTable();
            string storedProcedureName = "spCreateDeceasedClientViewcount";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
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
                    MessageBox.Show(ex.Message);
                }
            }

            return dataTable;
        }

        public List<Dictionary<string, string>> getClientFileXmlStructure() //Get All XmlClientStructure Values from Xmlstructure Table
        {
            try
            {
                var results = new List<Dictionary<string, string>>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(Constants.XmlStructureClientValues, connection))
                    {
                        connection.Open();
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
                }
                return results;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public List<Dictionary<string, string>> getClients(int batchId)//get clients data
        {
            try
            {
                var results = new List<Dictionary<string, string>>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("clientgeneratorXMLDEMO", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@batchId", batchId);
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
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
                                        row[columnName] = (bool)value ? "1" : "0";
                                    }
                                    else
                                    {
                                        row[columnName] = value.ToString();
                                    }
                                }
                                results.Add(row);
                                using (SqlConnection con = new SqlConnection(connectionString))
                                {
                                    using (SqlCommand cmd = new SqlCommand("insertXMLgeneratortimeClient", con))
                                    {
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        DateTime date = DateTime.Now;
                                        cmd.Parameters.AddWithValue("@Clientid", Convert.ToInt32(reader[32])); // Convert clientid to int
                                        cmd.Parameters.AddWithValue("@Datetime", date);
                                        con.Open();
                                        // Execute the second command here, after the reader is done with the row data
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                    }
                                }
                            }
                        }
                    }
                }
                return results;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public List<Dictionary<string, string>> FetchSubClientValues(string clientid, int batchid, string storedProcedureName)//fetch particular client data values
        {
            try
            {
                var result = new List<Dictionary<string, string>>();
                using (SqlConnection sql = new SqlConnection(connectionString))
                {
                    using (SqlCommand com = new SqlCommand(storedProcedureName, sql))
                    {
                        com.CommandType = CommandType.StoredProcedure;
                        com.Parameters.AddWithValue("@BatchId", batchid);
                        com.Parameters.AddWithValue("@ClientId", clientid);
                        sql.Open();
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
                                        row[columnName] = (bool)value ? "1" : "0";
                                    }
                                    else if (value is DateTime dateValue)
                                    {
                                        // Format date as yyyy/MM/dd
                                        row[columnName] = dateValue.ToString("yyyy/MM/dd");
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
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving data", ex);
            }
        }
        public List<Dictionary<string, string>> FetchSubClientValuesFromMedC4(string clientid, int batchid)
        {
            return FetchSubClientValues(clientid, batchid, "FetchSubClientDataFromMedCD4");
        }
        public List<Dictionary<string, string>> FetchSubClientValuesFromMedVL(string clientid, int batchid)
        {
            return FetchSubClientValues(clientid, batchid, "FetchSubClientDataFromMedVL");
        }
        public List<Dictionary<string, string>> FetchSubClientValuesFromHIVTest(string clientid, int batchid)
        {
            return FetchSubClientValues(clientid, batchid, "FetchSubClientDataFromHIVTest");
        }
        public List<Dictionary<string, string>> FetchSubClientValuesFromInsur(string clientid, int batchid)
        {
            return FetchSubClientValues(clientid, batchid, "FetchSubClientDataFromInsur");
        }
        public List<Dictionary<string, string>> FetchSubClientValuesFromRace(string clientid, int batchid)
        {
            return FetchSubClientValues(clientid, batchid, "FetchSubClientDataFromRace");
        }

        private string GetCurrentFilePath([CallerFilePath] string filePath = "") => filePath;
        private int GetCurrentLineNumber([CallerLineNumber] int lineNumber = 0) => lineNumber;
        private string GetCurrentMemberName([CallerMemberName] string memberName = "") => memberName;



        public string InsertOrUpdateContract(int contractID, string contractName, DateTime startedDateTime, DateTime endedDateTime, string statusValue, string createdBy, DateTime createdOn)//insert and update into database table
        {
            string operation = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    using (SqlCommand command = new SqlCommand("InsertOrUpdateContract", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@ContractID", contractID);
                        command.Parameters.AddWithValue("@ContractName", contractName);
                        command.Parameters.AddWithValue("@StartedDateTime", startedDateTime);
                        command.Parameters.AddWithValue("@EndedDateTime", endedDateTime);
                        command.Parameters.AddWithValue("@Status", statusValue);
                        command.Parameters.AddWithValue("@CreatedBy", createdBy);
                        command.Parameters.AddWithValue("@CreatedOn", createdOn);

                        // Add output parameter
                        SqlParameter outputParam = new SqlParameter("@Operation", SqlDbType.NVarChar, 50) { Direction = ParameterDirection.Output };
                        command.Parameters.Add(outputParam);

                        connection.Open();
                        command.ExecuteNonQuery();

                        // Get the value of the output parameter
                        operation = outputParam.Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as per your application's error handling strategy
                Console.WriteLine($"Error in InsertOrUpdateContract method: {ex.Message}");
                throw; // Re-throw the exception to propagate it to the caller
            }

            return operation;
        }


        public void ContractIdUpdateStatus(int contractID, string status)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    // Define the name of the stored procedure
                    string storedProcedureName = "UpdateContractStatus";

                    // Use a using statement to ensure the SqlCommand is properly disposed of after use
                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        // Specify that the SqlCommand is a stored procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the SqlCommand
                        command.Parameters.AddWithValue("@ContractID", contractID);
                        command.Parameters.AddWithValue("@Status", status);

                        // Open the database connection
                        connection.Open();

                        // Execute the stored procedure
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Show a message box with the error message
                MessageBox.Show(ex.Message);
            }
        }
        public void ContractIdEdit(int ContractID, String ContractName, Object startedDateTime, Object EndedDateTime, string status)//to modify particular contract id from contract list
        {
            string connectionString = GetConnectionString();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    using (SqlCommand command = new SqlCommand("UpdateContract", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@ContractID", ContractID);
                        command.Parameters.AddWithValue("@ContractName", ContractName);
                        command.Parameters.AddWithValue("@StartedDateTime", startedDateTime);
                        command.Parameters.AddWithValue("@EndedDateTime", EndedDateTime);
                        command.Parameters.AddWithValue("@Status", status);
                        command.Parameters.AddWithValue("@CreatedBy", "sakku");
                        command.Parameters.AddWithValue("@CreatedOn", DateTime.Now);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        public void ServiceCodeIdUpdateStatus(int ServiceCodeID, string status)//updating service after editing in service code setup
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    // Define the name of the stored procedure
                    string storedProcedureName = "UpdateServiceCodeStatus";

                    // Use a using statement to ensure the SqlCommand is properly disposed of after use
                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        // Specify that the SqlCommand is a stored procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the SqlCommand
                        command.Parameters.AddWithValue("@ServiceCodeID", ServiceCodeID);
                        command.Parameters.AddWithValue("@Status", status);

                        // Open the database connection
                        connection.Open();

                        // Execute the stored procedure
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Show a message box with the error message
                MessageBox.Show(ex.Message);
            }
        }
        public void EditServiceCode(int serviceCodeID, string service, string hccExportToAries, string hccContractID, string hccPrimaryService, string hccSecondaryService, string hccSubservice, string unitsOfMeasure, decimal unitValue, string status)//edit service code data according to service id
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("InsertOrUpdateServiceCode", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters
                        command.Parameters.AddWithValue("@ServiceCodeID", serviceCodeID);
                        command.Parameters.AddWithValue("@Service", service);
                        command.Parameters.AddWithValue("@HCC_ExportToAries", hccExportToAries);
                        command.Parameters.AddWithValue("@HCC_ContractID", hccContractID);
                        command.Parameters.AddWithValue("@HCC_PrimaryService", hccPrimaryService);
                        command.Parameters.AddWithValue("@HCC_SecondaryService", hccSecondaryService);
                        command.Parameters.AddWithValue("@HCC_Subservice", hccSubservice);
                        command.Parameters.AddWithValue("@UnitsOfMeasure", unitsOfMeasure);
                        command.Parameters.AddWithValue("@UnitValue", unitValue);
                        command.Parameters.AddWithValue("@Status", status);

                        // Add output parameter
                        SqlParameter outputParameter = new SqlParameter
                        {
                            ParameterName = "@Operation",
                            SqlDbType = SqlDbType.NVarChar,
                            Size = 50,
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputParameter);

                        // Open the connection and execute the command
                        connection.Open();
                        command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public string InsertOrUpdateServiceCode(
            int serviceCodeID,
            string service,
            string hccExportToAries,
            int hccContractID,
            string hccPrimaryService,
            string hccSecondaryService,
            string hccSubservice,
            string unitsOfMeasure,
            decimal unitValue,
            string status)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("InsertOrUpdateServiceCode", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters
                        command.Parameters.AddWithValue("@ServiceCodeID", serviceCodeID);
                        command.Parameters.AddWithValue("@Service", service);
                        command.Parameters.AddWithValue("@HCC_ExportToAries", hccExportToAries);
                        command.Parameters.AddWithValue("@HCC_ContractID", hccContractID);
                        command.Parameters.AddWithValue("@HCC_PrimaryService", hccPrimaryService);
                        command.Parameters.AddWithValue("@HCC_SecondaryService", hccSecondaryService);
                        command.Parameters.AddWithValue("@HCC_Subservice", hccSubservice);
                        command.Parameters.AddWithValue("@UnitsOfMeasure", unitsOfMeasure);
                        command.Parameters.AddWithValue("@UnitValue", unitValue);
                        command.Parameters.AddWithValue("@Status", status);

                        // Add output parameter
                        SqlParameter outputParameter = new SqlParameter
                        {
                            ParameterName = "@Operation",
                            SqlDbType = SqlDbType.NVarChar,
                            Size = 50,
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputParameter);

                        // Open the connection and execute the command
                        connection.Open();
                        command.ExecuteNonQuery();

                        // Return the value of the output parameter
                        return outputParameter.Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the error as needed
                MessageBox.Show($"An error occurred: {ex.Message}");
                return null; // or return an appropriate error value
            }
        }
        public DataTable GetActiveContracts()//get values from table of particular contract list
        {
            DataTable contracts = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Define the name of the stored procedure
                    string storedProcedureName = "GetActiveContracts";

                    // Use a using statement to ensure the SqlCommand is properly disposed of after use
                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        // Specify that the SqlCommand is a stored procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Open the database connection
                        connection.Open();

                        // Use a SqlDataAdapter to fill the DataTable with the results of the stored procedure
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(contracts);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the error as needed
                MessageBox.Show($"An error occurred: {ex.Message}");
            }

            // Return the populated DataTable
            return contracts;
        }
        public DataTable GetClientIDs(DateTime startDate, DateTime endDate)//get client id according to start and end date
        {
            try
            {
                // Validate that the dates are within the SQL Server range
                if (startDate < new DateTime(1753, 1, 1)) startDate = new DateTime(1753, 1, 1);
                if (endDate > new DateTime(9999, 12, 31)) endDate = new DateTime(9999, 12, 31);

                DataTable clientIds = new DataTable();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT Clnt_id,Agency_client_2 FROM HCCClients WHERE CreatedOn >= @StartDate AND CreatedOn <= @EndDate";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.DateTime)).Value = startDate;
                        cmd.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.DateTime)).Value = endDate;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(clientIds);
                        }
                    }
                }

                return clientIds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public DataTable GetServiceIDs(DateTime startDate, DateTime endDate)//get client id according to start and end date
        {
            try
            {
                // Validate that the dates are within the SQL Server range
                if (startDate < new DateTime(1753, 1, 1)) startDate = new DateTime(1753, 1, 1);
                if (endDate > new DateTime(9999, 12, 31)) endDate = new DateTime(9999, 12, 31);

                DataTable serviceIds = new DataTable();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT ServiceID, Service_date FROM HCCServices WHERE CreatedOn >= @StartDate AND CreatedOn <= @EndDate";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.DateTime)).Value = startDate;
                        cmd.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.DateTime)).Value = endDate;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(serviceIds);
                        }
                    }
                }

                return serviceIds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public DataTable GetFilteredDataFromDatabase(DateTime startDate, DateTime endDate)//to get deceased data accordinbg to date filter
        {
            try
            {
                DataTable dataTable = new DataTable();

                string query = @"
         SELECT * 
FROM vwDeceased_Client 
WHERE [Download Date] BETWEEN @StartDate AND @EndDate;
";

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }

                return dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public DataTable LoadDataForMonthYear(int year, int month)//load data according to month and date
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                SELECT *
                FROM HCCServices
                WHERE YEAR(CreatedOn) = @Year AND MONTH(CreatedOn) = @Month
                AND Status IN ('Upload Succeeded', 'Upload Failed')";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters with appropriate values
                        cmd.Parameters.AddWithValue("@Year", year);
                        cmd.Parameters.AddWithValue("@Month", month);

                        // Execute query and fill the DataTable
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions (logging, rethrowing, etc.)
                    throw new Exception("An error occurred while loading data.", ex);
                }
            }

            return dt;
        }
        public DataTable LoadData(DateTime startDate, DateTime endDate)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                    SELECT
                    FORMAT(HCCServices.CreatedOn, 'MMM-yyyy') AS 'Monthyear',
                    COUNT(CASE WHEN Status = 'Upload Succeeded' THEN HCCServices.ServiceID END) AS 'Service Entries Uploaded',
                    SUM(CASE WHEN Status = 'Upload Succeeded' THEN CAST(HCCServices.Quantity_served AS DECIMAL(18, 2)) ELSE 0 END) AS 'Units of Service Uploaded',
                    SUM(CASE WHEN Status = 'Upload Succeeded' THEN CAST(HCCServices.Price_served AS DECIMAL(18, 2)) ELSE 0 END) AS 'Cost of Services Uploaded',
                    COUNT(CASE WHEN Status = 'Upload Failed' THEN HCCServices.ServiceID END) AS 'Service Entries Failed',
                    SUM(CASE WHEN Status = 'Upload Failed' THEN CAST(HCCServices.Quantity_served AS DECIMAL(18, 2)) ELSE 0 END) AS 'Units of Service Failed',
                    SUM(CASE WHEN Status = 'Upload Failed' THEN CAST(HCCServices.Price_served AS DECIMAL(18, 2)) ELSE 0 END) AS 'Cost of Services Failed'
                FROM HCCServices
                WHERE HCCServices.CreatedOn BETWEEN @StartDate AND @EndDate
                GROUP BY FORMAT(HCCServices.CreatedOn, 'MMM-yyyy')
                ORDER BY FORMAT(HCCServices.CreatedOn, 'yyyy-MM') DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters with appropriate date values
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);

                        // Execute query and fill the DataTable
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions (logging, rethrowing, etc.)
                    throw new Exception("An error occurred while loading data.", ex);
                }
            }

            return dt;
        }

        public DataTable LoadDatafilter(DateTime startDate, DateTime endDate) // load data for monthly dashboard
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "sp_Upload_DashboardREPORT"; // Ensure this is the correct stored procedure name

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure; // Specify that it is a stored procedure

                        // Add start and end date parameters directly as DateTime
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions (logging, rethrowing, etc.)//PUSH AGAIN
                    MessageBox.Show(ex.Message);
                }
            }

            return dt;
        }


        public DataTable LoadDatafilterServiceReconbatchid(DateTime startDate, DateTime endDate, int batchID)
        {
            DataTable dy = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Call the stored procedure to update HCCServices
                    using (SqlCommand updateCmd = new SqlCommand("UpdateHCCServicesWithErrors", conn))
                    {
                        updateCmd.CommandType = CommandType.StoredProcedure;
                        updateCmd.ExecuteNonQuery();
                    }

                    // Now, load data from vwService_Reconciliation within the specified date range and batch ID
                    string query = @"
                SELECT * 
                FROM vwService_Reconciliationtest
                WHERE ServiceDate BETWEEN @StartDate AND @EndDate and batchID=@Batchid";


                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters for filtering dates and BatchID
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);
                        cmd.Parameters.AddWithValue("@Batchid", batchID);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dy);

                        // Check if the result is empty, which means no matching rows were found
                        if (dy.Rows.Count == 0)
                        {
                            MessageBox.Show(Constants.nobatchid, "Service Reconciliation Report");
                            return dy;

                        }

                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions (logging, rethrowing, etc.)
                    throw new Exception("An error occurred while loading data.", ex);
                }
            }

            return dy;

        }
        
        public DataTable LoadDatafilterServiceRecon(DateTime startDate, DateTime endDate, string filterType)
        {
            DataTable dt = new DataTable();
            string query;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Execute the stored procedure to update HCCServices if needed
                    using (SqlCommand updateCmd = new SqlCommand("UpdateHCCServicesWithErrors", conn))
                    {
                        updateCmd.CommandType = CommandType.StoredProcedure;
                        updateCmd.ExecuteNonQuery();
                    }

                    // Select query based on filter type
                    if (filterType == "ServiceDate")
                    {
                        query = @"SELECT*
    FROM vwService_Reconciliationdatefilter
    WHERE ServiceDate BETWEEN @StartDate AND @EndDate
    OR ServiceDate = @StartDate
    OR ServiceDate = @EndDate";

                    }
                    else if (filterType == "CreatedDate")
                    {
                        query = @"
                    SELECT * 
                    FROM vwService_ReconciliationCreatedDateFilter
                    WHERE EntryDate BETWEEN @StartDate AND @EndDate
 OR EntryDate = @StartDate
    OR EntryDate = @EndDate";
                    }
                    //else if (filterType == "BatchID")
                    //{
                    //    query = @"
                    //SELECT * 
                    //FROM vwService_Reconciliationtest
                    //WHERE BatchID = @BatchID";
                    //}
                    else
                    {
                        query = @"
                    SELECT * 
                    FROM vwService_Reconciliationtest
                    WHERE BatchID = @BatchID";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters based on the filter type
                        if (filterType == "ServiceDate" || filterType == "CreatedDate")
                        {
                            cmd.Parameters.AddWithValue("@StartDate", startDate);
                            cmd.Parameters.AddWithValue("@EndDate", endDate);
                        }
                        else if (filterType == "BatchID")
                        {
                           // cmd.Parameters.AddWithValue("@BatchID", batchid); // Assuming batchID is passed as an integer or similar
                        }

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while loading data.", ex);
                }
            }
            return dt;
        }
        public List<DataTable> LoadDatafilterhccreconBatchid(DateTime startDate, DateTime endDate, int[] Batchids, String filterType)
        {
            List<DataTable> result = new List<DataTable>();
            List<int> NoDataIds = new List<int>();

            try
            {
                foreach (int onebatch in Batchids)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("sp_HCCRecon", conn);
                        
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);
                        cmd.Parameters.AddWithValue("@BatchID", onebatch);
                        cmd.Parameters.AddWithValue("@Type", filterType);

                        conn.Open();

        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows == false)
                            {
                                NoDataIds.Add(onebatch);
                            }
                            // Read each result set into a DataTable
                            DataTable table = new DataTable();
                            table.Load(reader);
                            result.Add(table);  // Add the DataTable to the result list
                            if (Array.IndexOf(Batchids, onebatch) == Batchids.Length - 1 && NoDataIds.Count != 0)
                            {
                                MessageBox.Show(string.Join(",", NoDataIds.ToArray()) + Constants.NodatafoundfortheseBatchids);//

                            }
                            conn.Close();
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return result;
        }

        public DataTable CombineAllResults(List<DataTable> result)
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
        public DataTable LoadDatafilterhccrecon(DateTime startDate, DateTime endDate, String filterType)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Choose stored procedure based on filterType
                    string query = "sp_HCCRecon";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure; // Specify that it is a stored procedure

                        // Add start and end date parameters directly as DateTime
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);
                        cmd.Parameters.AddWithValue("@BatchID", 0);
                        cmd.Parameters.AddWithValue("@Type", filterType);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            return dt;
        }
        public DataTable LoadConfigurationfilter(DateTime startDate, DateTime endDate)//to get details of clients applied for services
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"select * from vw_ClientDemographics where [Created On]  between @StartDate and @EndDate"; // Ordering by the minimum date in each group

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions (logging, rethrowing, etc.)
                    MessageBox.Show(ex.Message);
                }
            }
            return dt;
        }
        public DataTable LoadManualUploadReport(DateTime startDate, DateTime endDate)//to get details of clients applied for services
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "ManualUploadReport"; // Ordering by the minimum date in each group

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions (logging, rethrowing, etc.)
                    MessageBox.Show(ex.Message);
                }
            }
            return dt;
        }
        public DataTable LoadERRORLOG(DateTime startDate, DateTime endDate)//load the error obtained while processing
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT * FROM vw_ErrorLog WHERE  Date  BETWEEN @StartDate AND @EndDate"; // Ordering by the minimum date in each group

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions (logging, rethrowing, etc.)
                    MessageBox.Show(ex.Message);
                }
            }
            return dt;
        }
        
        public DataTable GetHccServices()
        {
            try
            {
                DataTable hccServices = new DataTable();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT  ServiceID,Actual_minutes_spent,Staff_id,Clnt_id, Service_date, CreatedOn,Contract_id,MappedToHCC,Quantity_served FROM HCCServices";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.Fill(hccServices);
                }

                return hccServices;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public DataTable GetHccClients()//get client created date from table inserted
        {
            try
            {
                DataTable hccClients = new DataTable();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT Clnt_id,CreatedOn,Agency_client_1 FROM HCCClients";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.Fill(hccClients);
                }

                return hccClients;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public DataTable GetHccClientsFILTER(DateTime startDate, DateTime endDate)//to filter data accordingly
        {
            try
            {
                DataTable hccClients = new DataTable();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Modify the query to subtract one day from CreatedOn
                    string query = @"
            SELECT 
                Staff_id,
                Clnt_id,
                Service_date,
                CreatedOn,
                DATEADD(day, -1, CreatedOn) AS CreatedOnMinusOne -- Subtract 1 day from CreatedOn
            FROM HCCServices
            WHERE CreatedOn >= @StartDate AND CreatedOn <= @EndDate";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to the command
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(hccClients);
                    }
                }
                return hccClients;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public DataTable GetServicecodesetup()//to get export data
        {
            try
            {
                DataTable hccServicecodesetup = new DataTable();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT HCC_ExportToAries FROM ServiceCodeSetup";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.Fill(hccServicecodesetup);
                }
                return hccServicecodesetup;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public List<string> GetClientsIds()//to retrive agency client data
        {
            try
            {
                List<string> clientIds = new List<string>();
                string query = "SELECT Agency_client_1 FROM HCCClients";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                clientIds.Add(reader["Agency_client_1"].ToString());
                            }
                        }
                    }
                }

                return clientIds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public int GetTotalServiceEntries()//count os services provided
        {
            try
            {
                int totalServiceEntries = 0;
                string query = "SELECT COUNT(CMSServiceID) FROM CMSServices";


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        totalServiceEntries = Convert.ToInt32(command.ExecuteScalar());
                    }
                }

                return totalServiceEntries;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }
        public int GetServiceEntriesNotMappedToHCC()//count os services mapped
        {
            try
            {
                int serviceEntriesNotMappedToHCC = 0;
                string query = "SELECT COUNT(ServiceID) FROM HCCServices WHERE MappedToHCC = 'False'";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        serviceEntriesNotMappedToHCC = Convert.ToInt32(command.ExecuteScalar());
                    }
                }

                return serviceEntriesNotMappedToHCC;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        public int GetServiceEntriesSuccessfullyExportedToHCC()//count of services exported
        {
            try
            {
                string query = "SELECT COUNT(ServiceID) FROM HCCServices WHERE Status = 'Succeeded'";
                return ExecuteScalarQuery(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }

        }

        public int GetServiceEntriesNotExportedToHCC()//count of  services not exported
        {
            try
            {
                string query = "SELECT COUNT(ServiceID) FROM HCCServices WHERE Status <> 'Succeeded'";
                return ExecuteScalarQuery(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }

        }

        public int GetServiceEntriesForMHServicesOnlyClients()//count of  MHservices COUNT
        {
            try
            {
                string query = "SELECT COUNT(ServiceID) FROM HCCServices WHERE Service = 'MH'";
                return ExecuteScalarQuery(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }

        }

        public int GetServiceEntriesPostTimeboxPeriod()//count of  MHservices COUNT POST timebox period
        {
            try
            {
                string query = "SELECT COUNT(ServiceID) FROM HCCServices WHERE PostTimeBox = 1";
                return ExecuteScalarQuery(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }

        }

        public int GetServiceEntriesForExpiredMissingHCCConsent()//count of missing hccconsentcount
        {
            try
            {
                string query = "SELECT COUNT(ServiceID) FROM HCCServices WHERE HCCconsentExpired = '1'";
                return ExecuteScalarQuery(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }

        public int GetServiceEntriesForHCCIDMissing()//count of missing hccconsentcount
        {
            try
            {
                string query = "SELECT COUNT(ServiceID) FROM HCCServices WHERE HCCID IS NULL";
                return ExecuteScalarQuery(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }

        public int GetServiceEntriesNotEnrolledInProgram()//count of missing hccconsentcount which are not enrolled
        {
            try
            {
                string query = "SELECT COUNT(ServiceID) FROM HCCServices WHERE ProgramEnrolled IS NULL";
                return ExecuteScalarQuery(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }

        public int GetServiceEntriesForPreRegClients()//count of missing hccconsentcount
        {
            try
            {
                string query = "SELECT COUNT(ServiceID) FROM HCCServices WHERE PreReg = 1";
                return ExecuteScalarQuery(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }

        public int GetServiceEntriesForRWEligibilityExpired()//retrive count from services provided
        {
            try
            {
                string query = "SELECT COUNT(ServiceID) FROM HCCServices WHERE RWEligibilityExpired = 1";
                return ExecuteScalarQuery(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }

        public int GetServiceEntriesForMissingHCCStaffLogin()//retrive count from services provided
        {
            try
            {
                string query = "SELECT COUNT(ServiceID) FROM HCCServices WHERE StaffloginMissing = 1";
                return ExecuteScalarQuery(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }

        public int GetServiceEntriesWithZeroUnitOfService()//retrive count from services provided accordingly
        {
            try
            {
                string query = "SELECT COUNT(ServiceID) FROM HCCServices WHERE Unit_cd = 0";
                return ExecuteScalarQuery(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }

        public int GetServiceEntriesForWaiver()//retrive count from services provided accordingly
        {
            try
            {
                string query = "SELECT COUNT(ServiceID) FROM HCCServices WHERE Service = 'Waiver'";
                return ExecuteScalarQuery(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }

        public List<DateTime> GetServiceEntriesFor3DayDelayInHCCUpload()//retrive count from services provided accordingly
        {
            try
            {
                string query = @"
    SELECT Service_date
    FROM HCCServices";

                var serviceDates = new List<DateTime>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Assuming the date is in the first column
                                DateTime serviceDate = reader.GetDateTime(0);
                                serviceDates.Add(serviceDate);
                            }
                        }
                    }
                }

                return serviceDates;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private List<DateTime> ExecuteDateTimeQuery(string query)//retrive count from services provided accordingly
        {
            try
            {
                var result = new List<DateTime>();


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Assuming the date is in the first column
                                DateTime serviceDate = reader.GetDateTime(0);
                                result.Add(serviceDate);
                            }
                        }
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
        public int GetServiceEntriesForITDrops()//retrive count from services provided accordingly
        {
            try
            {
                string query = "SELECT COUNT(ServiceID) FROM HCCServices WHERE DataTeamInvestigationforErrors = 'True'";
                return ExecuteScalarQuery(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }

        // Utility method to execute scalar queries
        private int ExecuteScalarQuery(string query)
        {
            try
            {
                int result = 0;


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        result = Convert.ToInt32(command.ExecuteScalar());
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }


        }

        public int GetNextBatchIdabort(SqlConnection connection)
        {
            int currentBatchId = 0;

            // SQL query to get the latest batch ID from the database
            string query = "SELECT MAX(batchid) FROM Batch"; // Replace BatchTable with your actual table name

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Execute the query and get the result
                    object result = command.ExecuteScalar();

                    // If the result is not null, convert it to an integer (batch ID)
                    if (result != DBNull.Value)
                    {
                        currentBatchId = Convert.ToInt32(result);
                    }
                    else
                    {
                        // If there are no records, initialize the batch ID to a default value, e.g., 1
                        currentBatchId = 1;
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions, like logging or showing a message
                    MessageBox.Show("Error fetching the batch ID: " + ex.Message);
                }
                finally
                {
                    // Close the connection
                    connection.Close();
                }
            }

            return currentBatchId;
        }
        public int Getxmlbatchid()//retrive count from services provided accordingly
        {
            try
            {
                string query = "SELECT Max(batchid) FROM Batch WHERE FileName LIKE '%XML%'";
                return ExecuteScalarQuery(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }
        public static string GetFileName(bool includeClient)
        {
            string fileName = DateTime.Now.ToString("yyyyMMdd");

            if (includeClient)
            {
                fileName = "Client_" + fileName;
            }

            return fileName + ".xml";
        }
    }
}






