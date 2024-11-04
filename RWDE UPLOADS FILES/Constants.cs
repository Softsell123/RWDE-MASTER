using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Rwde;
using RWDE;
using RWDE_UPLOADS_FILES;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.IO;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Rwde
{
    /// <summary>
    /// Class containing constants used in the application.
    /// </summary>
    public static class Constants
    {
        public const string CLIENTTrackUploadon = "CLIENT Track Upload on";
        public const string theservicefilenamecannotbenull = "The Service FileName Cannot be null";
        public const string Batchnotfound = "Batch not found.";
        public const string Conversionhasalreadybeencompletedforthisbatch= "Conversion has already been completed for this batch.";
        public const string Generationhasalreadybeencompletedforthisbatch = "Generation has already been completed for this batch.";
        public const string LastFolderPathOCHIN = "LastFolderPathOCHIN";
        public const string LastFolderPathhcc = "LastFolderPathhcc";
        public const string LastFolderPathxml = "LastFolderPathxml";
        public const string Areyousureyouwanttoabort = "Are you sure you want to abort";
        public const string abortochindelete = "abortochindelete";
        public const string HCCDATA = "HCC";
        public const string nodatatoinsertintotable = "Please Select an Error File";
        public const string nodataavailableforthissourcefilename = "NO Records  or check the file name";
        public const string csvpath = "Csvpath";
        public const string ochintorwdeconversion = "OCHIN to RWDE Conversion";
        public const string ThesefileshavealreadybeenuploadedCloseandreopentouploadnewfiles="These files have already been uploaded. Close and reopen to upload new files.";
        public const string Errorclearingtables = "Error clearing tables";
        public const string xmlfileuploads = "XML file Uploads";
        public const string HCCCSVFILES = "HCC File Upload";
        public const string GenerateXML = "Generate HCC XML Files";
        public const string ThefolderisemptyPleaseuploadfiles = "The folder is empty. Please upload files.";
        public const string PleaseselectarowwithaBatchIDtoproceed = "Please select a row with a Batch ID to proceed";
        public const string Pleaseselectabatchbeforestartingtheconversion="Please select a batch before starting the conversion";
       
        public const string abortedfile = "aborted file";
        public const string ThefoldercontainsnonXMLfilesorfolderisemptyUploadisallowedonlyforXMLfiles = "The folder contains non-XML files or folder is empty.Upload is allowed only for XML files.";
        public const string Selectafilebeforeuploading = "Select a file before uploading";
        public const string ThefoldercontainsnonCSVfilesUploadisallowedonlyforCSVfiles = "The folder contains non-CSV files or folder is empty to proceed. Upload is allowed only for CSV files";


        public const string AriesClients = "ARIES_Clients";
        public const string EditColumnName = "Edit";
        public const string StartdatemustbelessthanEnddate = "Start date must be less than End date";
        public const string AriesConsent = "ARIES_Consent";
        public const string AriesDeceased = "ARIES_Decased";
        public const string AriesEligibility = "ARIES_Eligibility";
        public const string AriesServices = "ARIES_Services";
        public const string AriesFinanacial = "ARIES_Financial";
        public const string Module = "H";
        public const string UploadforOCHINCSVstarted = "Upload for OCHIN  CSV started";
        public const string UploadforOCHINCSVcompletedsuccessfull = "Upload for OCHIN CSV  completed successfull";
        public const string MappingcompletedsuccessfullyforBatchID = "Mapping completed successfully for BatchID:";
        public const string Trace = "T";
        public const string Debug = "D";
        public const string INFO = "I";
        public const string Warn = "W";
        public const string Error = "E";
        public const string Fatal = "F";
        public const string createddate = "Created Date";
        public const string servicedate = "Service Date";
        public const int Status = 11;
        public const int OCHIN = 23;
        public const int ClientTrack = 24;
        public const int HCC = 25;
        public const int HCCXMLSTATUSF = 20;
        public const int HCCXMLSTATUSG = 21;
        public const int HCCXMLSTATUSH = 22;
        public const int HCCSTARTCON = 17;
        public const int HCCENDCON = 18;
        public const int HCCABORT = 19;
        public const string Abort = "A";
        public const string CMSClients = "Client_20240523";
        public const string nobatchid = "No data exists for this batchid";
        public const string batchidnull = "Batch ID Cannot be null";
        public const string XMLUPLOADSTARTED = "XML Upload Started";
        public const string XMLUPLOADCompleted = "XMl Upload Completed";
        public const int CreatedBy = 100;
        public const string Client = "Total rows inserted for 'aries' client";
        public const string Consent = "Total rows inserted for 'aries' consent";
        public const string DeceasedClient = "Total rows inserted for 'aries' deceasedclient";
        public const string UploadSuccess = "Upload Success";
        public const string CTClients = "CTClients";
        public const string Eligibility = "CTClientsEligibilityDoc ";
        public const string CTServices = "CTServices";
        public const string ServiceCTTOHCC = "Services CT TO HCC";
        public const string HCCClients = "HCCClients";
        public const string CLIENTSANDSERVICESCTTOHCC = "CT TO HCC";
        public const int Extracted = 3;
        public const int CMSMatch = 2;
        public const int CMSMatchDate = 7;
        public const string FolderSelectMessage = "Select folder containing CSV files";
        public const string NoFolderSelectedMessage = "Please select a folder containing CSV files.";
        public const string Upload = "ARIES_Clients";
        public const string UploadStartedMessage = "Upload started for {0} CSV files.";
        public const string UploadedFileMessage = "Uploaded file: {0}";
        public const string ErrorMessage = "Error: {0}";
        public const string HCC_Reconciliation = "HCC_Reconciliation";  
        public const string UploadSuccessMessage = "CSV data inserted into the database successfully.\n\n" +
                                                    "Time taken: {0} seconds\n" +
                                                    "Total rows inserted: {1}";
        public const string Errorupdatingbatchstatus = "Error updating batch status:";
        public const string Abortedsuccessfully = "Aborted Successfully";
        public const string PleaseselectonlyonebatchId = "Please select only one batch Id";
        public const string PleaseselectabatchtogenerateXML = "Please select a batch to generate XML";
        public const string Pleaseselectthefolder = "Please select the folder";
        public const string GeneratetoHCCforbatchIDStarted = "Generate to HCC for batch ID Started";
        public const string GeneratetoHCCformatcompletedsuccessfully = "Generate to HCC format completed successfully";
        public const string Batchstatusupdatedsuccessfully = "Batch status updated successfully";
        public const string NobatchwasfoundwiththegivenID = "No batch was found with the given ID";
        public const string abort = "Abort";
       // public const string Pleaseselectabatchbeforestartingtheconversion = "Please select a batch before starting the conversion";
        public const string Pleaseselectonlyonebatchatatime = "Please select only one batch at a time";
        public const string ConverttoHCCforbatchIDStarted = "Convert to HCC  for batch ID Started";
        public const string ConverttoHCCformatcompletedsuccessfully = "Convert to HCC format completed successfully";
        public const string StartTime = "Start Time";
        public const string totalTimeTaken = "Total time taken";
        public const string endedat = "Ended At";
        public const string TotalRowsInserted = "Total Rows Inserted";
        public const string ServiceCodeSetup = "Service Code_Setup";
    public const string xmlselect = "Please select a batch to generate XML.";
        public const string ContractsSetup = "Contracts Setup";
        public const string selecrthefoldertosave  = "Select the folder to save the data";
        public const string Startdatemustbelessthenenddate = "Start date must be less then end date";
        public const string datasuccessfullysaved = "Data successfully saved in the selected folder";
        public const string Service_ReconciliationReport = "Service_Reconciliation Report";
        public const string Deceased_Clients = "Deceased_Clients";




        public const string nofolderselected = "No Folder Selected";
        public const string Therecordhasbeenmarkedasdeleted = "The record has been marked as deleted";
        public const string TheStatuscolumnismissing = "The 'Status' column is missing";
        public const string Norowselectedtosave = "No row selected to save";
        public const string HCCContractIDcannotbeempty = "HCC_ContractID cannot be empty";
        public const string InvalidServiceCodeID = "Invalid ServiceCodeID";
        public const string InvalidHCCContractID = "Invalid HCC_ContractID";
        public const string InvalidUnitValue = "Invalid UnitValue";
        public const string Operationnotrecognized = "Operation not recognized";
        public const string Invalidserviceid = "Invalid service notes format: Unable to extract the service ID";
        public const string close = "Close";
        public const string AreyousureyouwanttodeletethisbatchId = "Are you sure you want to delete this batchId?";
        public const string DeleteBatch = "Delete Batch";
        public const string Ochin = "OCHIN";
        public const int userid = 20;
        public const int xmlabort = 22;
        public const int fileaborted = 12;
        public const int agencyid = 101;
        public const string AbortedFileUpload = "Aborted File Upload";
        public const string totalrowinserted = "Total rows Inserted :";
        public const int uploadct = 26;
        public const int uploadochin = 27;
        public const int uploadhcc = 13;
        public const int ERROR = 5;

        //database Query
        public const string ServiceXmlGenerationQuery = "Servicegenerator";
        public const string ClientsXmlGeneration = "clientgeneratorXMLDEMO";
        public const string XmlStructureServiceValues = "SELECT TOP (1000) [xmlGeneratorID]\r\n      ,[FileID]\r\n      ,[TagNumber]\r\n      ,[Tag]\r\n      ,[Table]\r\n      ,[Field]\r\n      ,[PresetValue]\r\n      ,[Default]\r\n      ,[Empty]\r\n      ,[HasChild]\r\n      ,[DelimiterxmlGeneratorID]\r\n      ,[DataTransformerType]\r\n      ,[CreatedBy]\r\n      ,[CreatedOn]\r\n  FROM [RWDE].[dbo].[xmlGeneratorRules] where FileID = 1";
        public const string XmlStructureClientValues = "SELECT TOP (1000) [xmlGeneratorID]\r\n      ,[FileID]\r\n      ,[TagNumber]\r\n      ,[Tag]\r\n      ,[Table]\r\n      ,[Field]\r\n      ,[PresetValue]\r\n      ,[Default]\r\n      ,[Empty]\r\n      ,[HasChild]\r\n      ,[DelimiterxmlGeneratorID]\r\n      ,[DataTransformerType]\r\n      ,[CreatedBy]\r\n      ,[CreatedOn]\r\n  FROM [RWDE].[dbo].[xmlGeneratorRules] where FileID = 2";


        //View All Batches Constants String Values
        public const string ErrorTitle = "Error";
        public const string ErrorInitializingForm = "Error initializing form: ";
        public const string ErrorLoadingData = "Error loading data: ";
        public const string ErrorAddingColumn = "Error adding column ";
        public const string ErrorAdjustingColumnWidths = "Error adjusting column widths: ";
        public const string ErrorAdjustingColumnWidth = "Error adjusting width for column ";
        public const string ErrorHandlingCellClick = "Error handling cell click: ";
        public const string ErrorHandlingDeleteButtonClick = "Error handling delete button click: ";
        public const string ErrorClosingForm = "Error closing form: ";
        public const string DeleteColumnName = "Delete";
        public const string DeleteButtonText = "Delete";
        public const string ConfirmationMessage = "Are you sure you want to delete this batch ID";
        public const string ConfirmationTitle = "View All Batch";
        //Property Names
        public const string BatchID = "BatchID";
        public const string Description = "Description";
        public const string Type = "Type";
        public const string UploadStartedAt = "UploadStartedAt";
        public const string UploadEndedAt = "UploadEndedAt";
        public const string ConversionStartedAt = "ConversionStartedAt";
        public const string ConversionEndedAt = "ConversionEndedAt";
        public const string GenerationStartedAt = "GenerationStartedAt";
        public const string GenerationEndedAt = "GenerationEndedAt";
        public const string TotalRows = "TotalRows";
        public const string SuccessfulRows = "SuccessfulRows";
        public const string Statu = "Status";

        // Header text
        public const string BatchIDHeader = "Batch ID";
        public const string DescriptionHeader = "Description";
        public const string TypeHeader = "Batch Type";
        public const string UploadStartedAtHeader = "Upload Started At";
        public const string UploadEndedAtHeader = "Upload Ended At";
        public const string ConversionStartedAtHeader = "Conversion Started At";
        public const string ConversionEndedAtHeader = "Conversion Ended At";
        public const string GenerationStartedAtHeader = "Generation Started At";
        public const string GenerationEndedAtHeader = "Generation Ended At";
        public const string TotalRowsHeader = "Total Rows";
        public const string SuccessfulRowsHeader = "Successful Rows";
        public const string StatusHeader = "Status";
        public const string CreatedDate = "Created Date";

        public const string EmptyvalueMessage = "Enter The Correct Details";
        public const string FilterTitle = "View All Batches";
        public const string Deletioncancelled = "Deletion cancelled";
        public const string CMSServices = "Service_Sample_20240726";
        public const string Errorfileuplodedsuccessfully = "Error File Uploded Successfully";
        public const string NoFilterDatas = "The filter values you selected do not match any available data for OCHIN.";
        public const string NoFilterDatasHCC = "The filter values you selected do not match any available data.";
        public const string DateShouldBeGreaterThen = "The end date must be greater than the start date.";
        public const string Pleaseselectavalidfile="Please select a valid Excel(.xlsx) file";
        public const string DateTimeFormat = "MM-dd-yyyy";
        public const string AreyousureyouwanttodeleteSelectedrow = "Are you sure you want to delete Selected row";
        public const string AreyousureyouwanttoaddanewService = "Are you sure you want to add a new Service";
        public const string Areyousureyouwanttoaddanewcontract = "Are you sure you want to add a new contract";
        public const string Selectonlyonerowatatime = "Select only one row at a time";
        public const string ServiceCodeIDIDalreadyexists = "ServiceCodeID ID already exists";
        public const string ContractIDalreadyexists = "Contract ID already exists";
       public const string PleaseenteravalidStartedDateTimeandEndedDateTime = "Please enter a valid Started DateTime and  Ended DateTime";
        public const string ContractIDhastobepresentbeforeediting = "Contract ID has to be present before editing.";
        public const string PleaseaddContractIDbeforesaving = "Please add ContractID before saving";
        public const string PleasefillinContractNamebeforesaving = "Please fill in ContractName before saving";
        public const string ServiceCodeIDhastobepresentbeforeediting = "ServiceCodeID has to be present before editing";
        public const string AreyousureyouwanttoeditServiceCodeID = "Are you sure you want to edit ServiceCode ID:";
        public const string ServiceIDhastobepresentbeforedeleting = "Service ID has to be present before deleting";
        public const string Servicecodeupdatedsuccessfully = "Service code updated successfully";
        public const string Servicecodeinsertedsuccessfully = "Service code inserted successfully";
        public const string ContractIDhastobepresentbeforedeleting = "ContractID has to be present before deleting";
        public const string AreyousureyouwanttoeditContractID = "Are you sure you want to edit ContractID";
    }


    public static class ContractIDList
    {
        //Property Text
        public const string ContractID = "ContractID";
        public const string ContractName = "ContractName";

        //Header Text



        public const string StartedDateTime = "Started DateTime";
        public const string Error_Log_Report = "Error_Log_Report";
        public const string Success = "Success";
        public const string Monthly_Reports = "Monthly_Reports";
        public const string Client_Demographics_Report = "Client_Demographics_Report";

        public const string EndedDateTime = "Ended DateTime";
        public const string DoyouwanttodeletecontractContractName = "‘Do you want to delete contract ‘Contract Name’?";
        public const int activeContractstatus = 29;
        public const int inactiveContractstatus = 28;
        public const int deleteContractstatus = 30;
        public const string ACTIVE = "ACTIVE";
        public const string INACTIVE = "INACTIVE";
        public const string ConfirmDelete = "ConfirmDelete";
        public const string Information = "Information";
        public const string AreyousureyouwanttoEditthisrow = "Are you sure you want to Edit this row?";
        public const string Insert = "Insert";
        public const string Areyousureyouwanttodeletecontract = "Do you want to delete contract";

        public const string Update = "Update";
        public const string ConfirmEdit = "ConfirmEdit";
        public const string ContractIDortypeisinvalid = "ContractID or type is invalid.";


        public const string Contractupdatedsuccessfully = "Contract updated successfully";
        public const string Contractsavedsuccessfully = "Contract saved successfully";
        public const string DELETE = "DELETE";
        public const string Nochangesdetectedintheselectedrow = "No changes detected in the selected row.";
        public const string MustbeaDataGridViewCalendarCell = "Must be a DataGridViewCalendarCell";


        //Header Text
        public const string ContractIDs = "Contract ID";
        public const string ContractNames = "Contract Name";

        public const string ConfirmMakeActive = "Confirm Activation";
        public const string ConfirmMakeInactive = "Confirm Deactivation";
        public const string AreYouSureDeleteContract = "Are you sure you want to delete contract '{0}'?";
        public const string AreYouSureMakeActive = "Do you want to make the contract '{0}' active?";
        public const string AreYouSureMakeInactive = "Do you want to make the contract '{0}' inactive?";

        public const string DeleteColumnName = "DeleteColumn";
        public const string MakeActiveColumnName = "MakeActiveColumn";
        public const string MakeInactiveColumnName = "MakeInactiveColumn";
      

    }

    public static class XmlConstants
    {
        // XML Structure field names


        public const string DelimiterxmlGeneratorID = "DelimiterxmlGeneratorID";
        public const string Clntid = "Clnt_id";
        // TagNumber 
        public const string TagNumberFive = "5";
        public const string TagNumberTen = "10";
        public const string TagNumberThirty = "30";

        // Example error message
        public const string ErrorMessage = "Your constant error message here.";

        // Example constant values
        public const string ContractServicesTagNumber = "30";
        public const string ContractServicesTitleIdentifier = "TITLE";
        public const string ContractServicestitleIdentifier = "title";
        public const string TagNumber = "TagNumber";
        public const string Tag = "Tag";
        public const string PresetValue = "PresetValue";
        public const string Default = "Default";
        public const string Field = "Field";
        public const string Empty = "Empty";
        public const string HasChild = "HasChild";
        public const string Table = "Table";

        public const string Areyousureyouwanttodeletetheselectedrow = "Are you sure you want to delete the selected row";

        // TagNumber 
        public const string True = "TRUE";
        // TagNumber 
        public const string False = "FALSE";
        // TagNumber 


        // Example error message


    }

    public static class ServiceCodeSetup
    {
        public const string AreyousureyouwanttoaddanewService = "Are you sure you want to add a new Service";
    }
}



