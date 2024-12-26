
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using RWDE.Properties;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Policy;
using System.Windows.Forms;
using static Spire.Pdf.General.Render.Decode.Jpeg2000.j2k.codestream.HeaderInfo;

namespace RWDE
{
    /// <summary>
    /// Class containing constants used in the application.
    /// </summary>
    public static class Constants
    {
        public const string Sakku = "sakku ";
        public const string AgencyCode = "246_";

        public const string MyConnection = "MyConnection";
        public const string ConnectionStrings = "connectionStrings";



        public const string ClientTrackUploadon = "CLIENT Track Upload on";
        public const string Theservicefilenamecannotbenull = "The Service FileName Cannot be null";
        public const string BatchNotfound = "Batch not found.";
        public const string Conversionhasalreadybeencompletedforthisbatch= "Conversion has already been completed for this batch.";
        public const string Generationhasalreadybeencompletedforthisbatch = "Generation has already been completed for this batch.";
        public const string LastFolderPathOchin = "LastFolderPathOCHIN";
        public const string LastFolderPathhcc = "LastFolderPathhcc";
        public const string LastFolderPathxml = "LastFolderPathxml";
        public const string Abortochindelete = "abortochindelete";
        public const string Norecordsfound = "No Records Found";
        public const string Uploadfailed = "Upload Failed";
        public const string Hccdata = "HCC";
        public const string ServicegeneratorError = "ServicegeneratorERROR";
        public const string Nodatatoinsertintotable = "Please Select an Error File";
        public const string Nodataavailableforthissourcefilename = "NO Records  or check the file name";
        public const string Csvpath = "Csvpath";
        public const string Ochintorwdeconversion = "OCHIN to RWDE Conversion";
        public const string ThesefileshavealreadybeenuploadedCloseandreopentouploadnewfiles="These files have already been uploaded. Close and reopen to upload new files.";
        public const string Xmlfileuploads = "XML file Uploads";
        public const string Hcccsvfiles = "HCC File Upload";
        public const string GenerateXml = "Generate HCC XML Files";
        public const string ThefolderisemptyPleaseuploadfiles = "The folder is empty. Please upload files.";
        public const string Thefolderhasmorethanonefileorduplicatefiles = "The folder has more than one file or duplicate files.";
        public const string Thefolderhasmorethantwofileorduplicatefiles = "The folder has more than two files or duplicate files.";
        public const string PleaseselectarowwithaBatchIDtoproceed = "Please select a row with a Batch ID to proceed";
       
        public const string Abortedfile = "aborted file";
        public const string ThefoldercontainsnonXmLfilesorfolderisemptyUploadisallowedonlyforXmLfiles = "The folder contains non-XML files or folder is empty.Upload is allowed only for XML files.";
        public const string Selectafilebeforeuploading = "Select a file before uploading";
        public const string ThefoldercontainsnonCsVfilesUploadisallowedonlyforCsVfiles = "The folder contains non-CSV files or folder is empty to proceed. Upload is allowed only for CSV files";
        public const string UploadingEmptyFile = "Uploading Empty File ";
        public const string TheFileisbeingUsedinanotherprocessClosethefileandTryagain = "The File is being used in another process.Please Close the file and Try again. ";

        public const string AriesClients = "ARIES_Clients";
        public const string Edit = "Edit";
        public const string StartdatemustbeearlierthanEnddate = "Start date must be earlier than End date";
        public const string AriesConsent = "ARIES_Consent";
        public const string AriesDeceased = "ARIES_Decased";
        public const string AriesEligibility = "ARIES_Eligibility";
        public const string AriesServices = "ARIES_Services";
        public const string AriesFinanacial = "ARIES_Financial";
        public const string Module = "H";
        public const string UploadforOchincsVstarted = "Upload for OCHIN  CSV started";
        public const string UploadforOchincsVcompletedsuccessfull = "Upload for OCHIN CSV  completed successfull";
        public const string Trace = "T";
        public const string Debug = "D";
        public const string Info = "I";
        public const string Warn = "W";
        public const string ErrorCode = "E";
        public const string Fatal = "F";
        public const string Createddate = "Created Date";
        public const string Servicedate = "Service Date";
        public const int StatusCode = 11;
        public const int OchinCode = 23;
        public const int ClientTrack = 24;
        public const int Hcc = 25;
        public const int Hccxmlstatusf = 20;
        public const int Hccxmlstatusg = 21;
        public const int Hccxmlstatush = 22;
        public const int Hccstartcon = 17;
        public const int Hccendcon = 18;
        public const int Hccabort = 19;
        public const string AbortCode = "A";

        public const string Nodataexistsforthisbatchid = "No data exists for this Batch ID(s)";
        public const string Batchidnull = "Batch ID Cannot be null";
        public const string Xmluploadstarted = "XML Upload Started";
        public const string XmluploadCompleted = "XMl Upload Completed";
        public const int CreatedBy = 100;
        public const string TotalRowsInsertedClient = "Total rows inserted for 'aries' client";
        public const string Consent = "Total rows inserted for 'aries' consent";
        public const string DeceasedClient = "Total rows inserted for 'aries' deceasedclient";
        public const string UploadSuccess = "Upload Success";
        public const string CtClients = "CTClients";
        public const string Eligibility = "CTClientsEligibilityDoc ";
        public const string CtServices = "CTServices";
        public const string ServiceCttohcc = "Services CT TO HCC";
        public const string Clientsandservicescttohcc = "CT TO HCC";
        public const int ExtractedCode = 3;
        public const int CmsMatchCode = 2;
        public const int CmsMatchDateCode = 7;
        public const string FolderSelectMessage = "Select folder containing CSV files";
        public const string NoFolderSelectedMessage = "Please select a folder containing CSV files.";
        public const string Upload = "ARIES_Clients";
        public const string UploadStartedMessage = "Upload started for {0} CSV files.";
        public const string UploadedFileMessage = "Uploaded file: {0}";
        public const string ErrorMessagedynamic = "Error: {0}";
        public const string HccReconciliation = "HCC_Reconciliation";
        public const string NodatafoundfortheseBatchids = "No Data Found For these Batchid(s)";
        public const string UploadSuccessMessage = "CSV data inserted into the database successfully.\n\n" +
                                                    "Time taken: {0} seconds\n" +
                                                    "Total rows inserted: {1}";
        public const string Errorupdatingbatchstatus = "Error updating batch status:";
        public const string PleaseselectonlyonebatchId = "Please select only one batch Id";
        public const string PleaseselectabatchtogenerateXml = "Please select a batch to generate XML";
        public const string Pleaseselectthefolder = "Please select the folder";
        public const string GeneratetoHcCforbatchIdStarted = "Generate to HCC for batch ID Started";
        public const string GeneratetoHcCformatcompletedsuccessfully = "Generate to HCC format completed successfully";
        public const string Batchstatusupdatedsuccessfully = "Batch status updated successfully";
        public const string NobatchwasfoundwiththegivenId = "No batch was found with the given ID";
        public const string Abort = "Abort";
        public const string Warning = "Warning";
        public const string Sheet1 = "Sheet1";
        public const string XlsxExtention = ".xlsx";
        public const string CsvExtention = ".csv";
        public const string AllCsvExtention = "*.csv";
        public const string XmlExtention = ".xml";
        public const string AllXmlExtention = "*.xml";
        public const string AllExtention = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";




        public const string Pleaseselectonlyonebatchatatime = "Please select only one batch at a time";
        public const string Pleaseselectavalidrowtoproceed = "Please select a valid row to proceed(It seems selected row is an empty row)";
        public const string StartTime = "Start Time";
        public const string TotalTimeTaken = "Total time taken";
        public const string EndedAt = "Ended At";
        public const string TotalRowsInserted = "Total Rows Inserted";
        public const string ServiceCodeSetup = "Service Code_Setup";
    public const string Xmlselect = "Please select a batch to generate XML.";
        public const string ContractsSetup = "Contracts Setup";
        public const string Selecrthefoldertosave  = "Select the folder to save the data";
        public const string Startdatemustbeearlierthenenddate = "Start date must be earlier than End date.";
        public const string Datasuccessfullysaved = "Data successfully saved in the selected folder";
        public const string ServiceReconciliationReport = "Service_Reconciliation Report";
        public const string DeceasedClients = "Deceased_Clients";
        public const string Nodatafoundbetweenselecteddates = "No data found between selected dates";
        public const string NoDataAvailableToDownload = "No data available to download.";






        public const string Nofolderselected = "No Folder Selected";
        public const string Therecordhasbeenmarkedasdeleted = "The record has been marked as deleted";
        public const string TheStatuscolumnismissing = "The 'Status' column is missing";
        public const string HccContractIDcannotbeempty = "HCC_ContractID cannot be empty";
        public const string InvalidServiceCodeId = "Invalid ServiceCodeID";
        public const string InvalidHccContractId = "Invalid HCC_ContractID";
        public const string InvalidUnitValue = "Invalid UnitValue";
        public const string Operationnotrecognized = "Operation not recognized";
        public const string Invalidserviceid = "Invalid service notes format: Unable to extract the service ID";
        public const string Close = "Close";
        public const string AreyousureyouwanttodeletethisbatchId = "Are you sure you want to delete this batchId?";
        public const string DeleteBatch = "Delete Batch";
        public const string Ochin = "OCHIN";
        public const int Userid = 20;
        public const int Xmlabort = 22;
        public const int Fileaborted = 12;
        public const int Agencyid = 101;
        public const string AbortedFileUpload = "Aborted File Upload";
        public const string Totalrowinserted = "Total rows Inserted :";
        public const int Uploadct = 26;
        public const int Uploadochin = 27;
        public const int Uploadhcc = 13;
        public const int Error = 5;

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
        public const string BatchId = "BatchID";
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
        public const string Status = "Status";

        // Header text
        public const string BatchIdHeader = "Batch ID";
        public const string DescriptionHeader = "Description";
        public const string BatchTypeHeader = "Batch Type";
        public const string UploadStartedAtHeader = "Upload Started At";
        public const string UploadEndedAtHeader = "Upload Ended At";
        public const string ConversionStartedAtHeader = "Conversion Started At";
        public const string ConversionEndedAtHeader = "Conversion Ended At";
        public const string GenerationStartedAtHeader = "Generation Started At";
        public const string GenerationEndedAtHeader = "Generation Ended At";
        public const string TotalRowsHeader = "Total Rows";
        public const string SuccessfulRowsHeader = "Successful Rows";
        public const string StatusHeader = Constants.Status;
        public const string CreatedDatesp = "Created Date";
        

        public const string EmptyvalueMessage = "Enter The Correct Details";
        public const string FilterTitle = "View All Batches";
        public const string Deletioncancelled = "Deletion cancelled";
        public const string Services = "Service_Sample";
        public const string Errorfileuplodedsuccessfully = "Error File Uploded Successfully";
        public const string NoFilterDatas = "The filter values you selected do not match any available data for OCHIN.";
        public const string NoFilterDatasHcc = "The filter values you selected do not match any available data.";
        public const string DateShouldBeGreaterThen = "The end date must be greater than the start date.";
        public const string Pleaseselectavalidfile="Please select a valid Excel(.xlsx) file";
        public const string AreyousureyouwanttodeleteSelectedrow = "Are you sure you want to delete Selected row";
        public const string AreyousureyouwanttoaddanewService = "Are you sure you want to add a new Service";
        public const string Areyousureyouwanttoaddanewcontract = "Are you sure you want to add a new contract";
        public const string Selectonlyonerowatatime = "Select only one row at a time";
        public const string ServiceCodeIdiDalreadyexists = "ServiceCodeID ID already exists";
        public const string ContractIDalreadyexists = "Contract ID already exists";
       public const string PleaseenteravalidStartedDateTimeandEndedDateTime = "Please enter a valid Started DateTime and  Ended DateTime";
        public const string ContractIDhastobepresentbeforeediting = "Contract ID has to be present before editing.";
        public const string PleaseaddContractIDbeforesaving = "Please add ContractID before saving";
        public const string PleasefillinContractNamebeforesaving = "Please fill in ContractName before saving";
        public const string ServiceCodeIDhastobepresentbeforeediting = "ServiceCodeID has to be present before editing";
        public const string AreyousureyouwanttoeditServiceCodeId = "Are you sure you want to edit ServiceCode ID:";
        public const string ServiceIDhastobepresentbeforedeleting = "Service ID has to be present before deleting";
        public const string Servicecodeupdatedsuccessfully = "Service code updated successfully";
        public const string Servicecodeinsertedsuccessfully = "Service code inserted successfully";
        public const string ContractIDhastobepresentbeforedeleting = "ContractID has to be present before deleting";
        public const string AreyousureyouwanttoeditContractId = "Are you sure you want to edit ContractID";
        //
        //ContractIdList
        //
        public const string ContractName = "ContractName";
        public const string Name = "Name";
        public const string ContractId = "ContractID";
        public const string Active = "ACTIVE";
        public const string Inactive = "INACTIVE";
        public const string ActiveContractstatus = "29";
        public const string InactiveContractstatus = "28";
        public const string DeleteContractstatus = "30";
        public const string ContractIDs = "Contract ID";
        public const string AreYouSureDeleteContract = "Are you sure you want to delete contract '{0}'?";
        public const string AreYouSureMakeActive = "Do you want to make the contract '{0}' active?";
        public const string AreYouSureMakeInactive = "Do you want to make the contract '{0}' inactive?";
        //
        //DeceasedClientReport
        //
        public const string HccId = "HCCID";
        public const string HccIdsp = "HCC ID";
        public const string ClientName = "ClientName";
        public const string ClientNamesp = "Client Name";
        public const string DateOfDeath = "DateOfDeath";
        public const string DateOfDeathsp = "Date Of Death";
        public const string LastServiceDate = "LastServiceDate";
        public const string LastServiceDatesp = "Last Service Date";
        public const string DownloadDate = "DownloadDate";
        public const string DownloadDatesp = "Download Date";
        public const string Extracted = "Extracted";
        public const string Extractedsp = "Extracted Y/N";
        public const string ExtractionDate = "ExtractionDate";
        public const string ExtractionDatesp = "Extraction Date";
        public const string CmsMatch = "CMSMatch";
        public const string CmsMatchsp = "CMS Match";
        public const string CmsMatchDate = "CMSMatchDate";
        public const string CmsMatchDatesp = "CMS Match Date";
        public const string ServiceCountAfterDeath = "Service Count After Death";
        public const string CreatedOn = "CreatedOn";
        public const string CreatedOnsp = "Created On";
        public const string FntfmlyCalibre = "Calibre";
        public const string DeceasedClientsp = "Deceased Clients";



        //
        //frmConvertToHCC
        //
        public const string FntfmlyArial = "Arial";






        //
        //DateTime
        //
        public const string StartedDateTime = "StartedDateTime";
        public const string EndedDateTime = "EndedDateTime";
        public const string StartedOn = "Started On";
        public const string MMddyyyyHHmmss = "MM-dd-yyyy HH:mm:ss";
        public const string DateFormatMMddyyyy = "MM-dd-yyyy";
        public const string DdMMyyyyHHmmss= "dd-MM-yyyy HH:mm:ss";
        public const string YyyyMMdd = "yyyyMMdd";
        public const string DdMMyyyy = "ddMMyyyy";
        public const string MMddyyyybkslash = "MM/dd/yyyy";
        public const string HHmmss = "HH:mm:ss";

        public const string MMddyyyyHHmmssbkslash = "MM/dd/yyyy HH:mm:ss";
        public const string Seconds = "Seconds";


        //
        //MessageBox
        //
        public const string Alreadysaved = " already saved";
        public const string Norowselectedtosave = "No row selected to save.";
        public const string SaveError = "Save Error";
        public const string Errorsp = "Error: ";
        public const string InputError = "Input Error";


        public const string FilterSelectionRequired = "Filter Selection Required";
        public const string PermissionError = "Permission Error";
        public const string AbortConfirmation = "Abort Confirmation";
        public const string DeletedContractId = "Deleted ContractID:";
        public const string ContractIdisrequired="Contract ID is required.";
        public const string ContractsSetupError = "Contracts Setup - Error";
        public const string InvalidFileType = "Invalid File Type";
        public const string BatchIdExists = "Batch ID Exists";
        public const string ServiceCodeSetupError = "ServiceCode Setup-Error";




        public const string DataSourcecannotbeempty = "Data Source cannot be empty.";
        public const string ValidationError = "Validation Error";
        public const string UnabletoconnectPleasecheckandtryagain = "Unable to connect to the specified Data Source. Please check and try again.";
        public const string Theselectedfolderdoesnotexist = "The selected folder does not exist.";
        public const string DataSourceupdatedsuccessfully = "Data Source updated successfully!";
        public const string Hasalreadybeendeleted = " has already been deleted.";
        public const string Selectafoldertosavethefile = "Select a folder to save the file.";
        public const string Hasalreadycompletedtheconversion = " has already completed the conversion.";
        public const string ConverttoHcCforbatchIdStarted = "Convert to HCC  for batch ID Started.";
        public const string ConverttoHcCformatcompletedsuccessfully = "Convert to HCC format completed successfully.";
        public const string MappingcompletedsuccessfullyforBatchId = "Mapping completed successfully for BatchID: ";
        public const string TheRequiredColumnsAreMissing = "The required columns 'HCCTABLE' or 'ErrorMessage' are missing in the Excel sheet.";

        public const string PleaseSelectAValidFilterTypeFromTheDropdown = "Please select a valid filter type from the dropdown.";
        public const string PleaseEnterAValidBatchIdOrSelectAFilterType = "Please enter a valid Batch ID or select a filter type.";
        public const string PleaseSelectABatchBeforeStartingTheConversion = "Please select a batch before starting the conversion";




        public const string Dataprocessedandinsertedintothedatabasesuccessfully = "Data processed and inserted into the database successfully.";
        
        public const string Errorinsertingdataintothedatabase = "Error inserting data into the database: ";
        public const string ErroraddingremovedbatchIDtodatabase = "Error adding removed batch ID to database: ";
        public const string Errorupdatingbatch = "Error updating batch: ";
        public const string ErrorUpdatingFileProgress= "Error updating file progress: ";
        public const string AnErrorOccurred = "An error occurred: ";
        public const string SqlError  = "SQL Error: ";
        public const string ErrorGettingTotalRows = "Error getting total rows: ";
        public const string ErrorUpdatingGridStatus = "Error updating grid status: ";
        public const string ErrorClearingtables = "Error clearing tables: ";
        public const string ErrorDeletingXmlFiles = "Error deleting XML files: ";
        public const string BatchIdAlreadyExists = "Batch ID {existingBatchId} already exist";
        public const string BatchIdAlreadyExistsCloseAndReopen= "Batch ID {existingBatchId} already exists. Close and reopen to upload new files.";
        public const string ErrorAddingRemovedBatchIdToDatabase= "Error adding removed batch ID to database:";
        public const string DoYouWantToDeleteServiceCodeId = "Do you want to delete Service {0}?";


        public const string CsVfilehasbeencreatedsuccessfullyat  = "CSV file has been created successfully at ";
        public const string Accessdeniedtothefolder = "Access to the path is denied. Please choose a different folder or run the application as an administrator.";


        public const string Successfully = "Successfully";
        public const string Confirmation = "Confirmation";
        public const string Information = "Information";
        public const string Success = "Success";
        public const string Delete = "DELETE";
        public const string This = "This ";
        public const string AbortedSuccessfully = "Aborted Successfully";
        public const string OchinToHccConversion = "OCHIN To HCC Conversion";
        public const string UploadHccCsv = "UPLOAD HCC CSV";
        public const string UploadOchinCsv = "UPLOAD OCHIN CSV";
        public const string XmlFileUpload = "XML File Upload";


        public const string Areyousureyouwanttoabort = "Are you sure you want to abort?";
        public const string TheFolderPathCannotBeEmpty = "The folder path cannot be empty. Please select a valid folder.";
        public const string SelectAFolderBeforeUploading = "Select a folder before uploading.";
        public const string PleaseSelectAFolderToUploadXmlFiles = "Please select a folder to upload XML files.";

        public const string TheFileIsAlreadyUploaded = "This File is already uploaded.Click On Browse to Choose another file to upload.";

        public const string TheSelectedFolderIsEmpty = "The selected folder is empty. Please select a folder containing .csv files.";
        public const string TheFolderContainsNonXmlFiles= "The folder contains non-XML files or folder is empty. Upload is allowed only for XML files.";








        //
        //DialogBox
        //
        public const string ExcelFilesXlsx = "Excel Files|*.xlsx";
        public const string SelectAnExcelFile = "Select an Excel File";



        //
        //StoreProcedureConstants
        //
        public const string Ctclientsmapping = "ctclientsmapping";
        public const string GetCtServicesForCsv = "GetCTServicesForCSV";
        public const string MapCmsClientstest = "MapCMSClientstest";
        public const string MapDlServicesToHccServices = "MapDlServicesToHCCServices";
        public const string Conversion = "Conversion";
        public const string Abortconversiondelete = "abortconversiondelete";
        public const string ListValueXml = "listvaluexml";
        public const string UpdateXmlBatch = "updatexmlBATCH";
        public const string UpdateXmlClient = "updatexmlCLIENT";
        public const string UpdateXml = "updatexml";
        public const string CountXml = "countxml";
        public const string CountXmlServices = "countxmlservices";
        public const string CountXmlRows = "countxmlrows";
        public const string AbortDelete = "abortdelete";
        public const string ListConversion = "listconversion";
        public const string MapCmsClients = "MapCMSClients";
        public const string MapCmsServicesToHccServices = "MapCMSServicesToHCCServices";
        public const string ConversionOchin = "Conversionochin";
        public const string ConversionHcc = "ConversionHCC";
        public const string CountCmsServices = "countcmsservices";
        public const string UpdateGrid = "updategrid";
        public const string CountCmsClients = "COUNTCMSCLIENTS";
        public const string UpdateBatch = "Updatebatch";
        public const string AbortConversionDelete = "abortconversiondelete";











        //
        //DatabaseQueries
        //
        public const string SelectValuefromListwhereListsId = "select Value from List where ListsID = @ListsID";
        public const string UpdateBatchStatusQuery = "UPDATE Batch SET Status = @Status, ConversionStartedAt = @Timestamp WHERE BatchID = @BatchID";
        public const string GetConversionTimeQuery = "SELECT ConversionStartedAt, ConversionEndedAt FROM Batch WHERE BatchID = @BatchID";
        public const string GetTotalRowsQuery = "@SELECT (SELECT COUNT(*) FROM RWDE.dbo.CTClients WHERE BatchID = @BatchID) +(SELECT COUNT(*) FROM RWDE.dbo.CTClientsEligibilityDoc WHERE BatchID = @BatchID) +(SELECT COUNT(*) FROM RWDE.dbo.CTServices WHERE BatchID = @BatchID)AS TotalCount";
        public const string UpdateBatchConversionTimeQuery = "UPDATE [RWDE].[dbo].[Batch] SET[ConversionStartedAt] = @ConversionStartedAt,[ConversionEndedAt] = @ConversionEndedAt,[SuccessfulRows] = @AllTotalRows,[Status] = '18'WHERE[BatchID] = @BatchID AND[Status] = 11 OR[Status]=17 OR[Status] = 19";
        public const string GetTotalRowsForBatchservicesQuery = "SELECT COUNT(*) FROM RWDE.dbo.CTServices WHERE BatchID = @BatchID";
        public const string AddRemovedBatchIdToDatabaseQuery = "INSERT INTO RemovedBatchIDs (BatchID) VALUES (@BatchID)";
        public const string UpdateGridStatusQuery = "SELECT Value FROM List WHERE ListsID = @ListsID";
        public const string GetTotalRowsForBatchQuery = "SELECT (SELECT COUNT(*) FROM RWDE.dbo.CMSClients WHERE BatchID = @BatchID) AS TotalCount";
        public const string UpdateBatchStatus = "UPDATE [RWDE].[dbo].[Batch] SET [Status] = @Status, [SuccessfulRows] = 0 WHERE [BatchID] = @BatchID";

        public const string InsertIntoDatabaseQuery = "INSERT INTO HCC_ErrorLog (HccTable, ErrorMessage, SourceId, SourceFileName) VALUES (@HccTable, @ErrorMessage, @ClientId, @SourceFileName)";

        public const string UpdateStatusColumnQuery = "UPDATE [RWDE].[dbo].[Batch] SET[GenerationEndedAt] = @GenerationEndedAt, [Status] = '21' WHERE[BatchID] = @BatchID AND[FileName] LIKE '%Client%'";

        public const string GetGenerationTime = "SELECT GenerationStartedAt, GenerationEndedAt FROM Batch WHERE BatchID = @BatchID";
        public const string BatchTableQuery = "SELECT [BatchID], [FileName], [Description], [Path], [StartedAt], [EndedAt], [TotalRows], [SuccessfulRows], [FailedRows], [Status], [Message], [Comments], [CreatedBy], [CreatedOn] FROM [RWDE].[dbo].[Batch]";
        public const string AddRemoveBatchIdQuery = "INSERT INTO RemovedBatchIDs(BatchID) VALUES(@BatchID)";


        //
        //SPparametersConstants
        //
        public const string AtBatchid = "@Batchid";
        public const string AtClientId = "@ClientId";

        public const string AtListsId = "@ListsID";
        public const string AtTimestamp = "@Timestamp";
        public const string AtStatus = "@Status";
        public const string AtConversionStartedAt = "@ConversionStartedAt";
        public const string AtConversionEndedAt = "@ConversionEndedAt";
        public const string AtGenerationStartedAt = "@GenerationStartedAt";
        public const string AtGenerationEndedAt = "@GenerationEndedAt";


        public const string AtAllTotalRows = "@AllTotalRows";


        public const string AtHccTable = "@HccTable";
        public const string AtErrorMessage = "@ErrorMessage";
        public const string AtSourceFileName = "@SourceFileName";
        public const string AtFilename = "@Filename";







        //
        //FrmConvertToCsv
        //
        public const string Clients = "Client_";
        public const string ServiceSample = "Service_Sample_";
        public const string Testfiletxt = "testfile.txt";
        public const string Testingpermissions ="Testing permissions.";
        public const string Selectedfolder  = "Selected folder: ";




        //
        //FrmConvertToHCC
        //
        public const string FileName = "FileName";
        public const string FileNamesp = "File Name";


        //
        //frmDownloadHccErrors
        //
        public const string HccTable = "HccTable";
        public const string HccTableSp = "HCC Table";
        public const string ErrorMessage = "ErrorMessage";
        public const string ErrorMessageSp = "Error Message";

        public const string DownloadHccErrors = "Download HCC Errors";
        public const string SourceFileName = "SourceFileName";
        public const string SourceId = "SourceId";
        public const string ClientIdSp = "Client ID";
        public const string UpperService = "SERVICE";
        public const string UpperClient = "CLIENT";
        public const string Filtersourcefilename = "FILTERSOURCEFILENAME";



        //
        //frmGenerateXml
        //
        public const string GenerationStarted = "Generationstarted";
        public const string BatchDescriptionSp = "Batch Description";
        public const string GenerationOchin = "Generationochin";
        public const string SmallFileName = "fileName";
        public const string Client = "Client";
        public const string Service = "Service";
        public const string ServiceXmlHeader = "ServiceDetails_0246_0422_";
        public const string ClientXmlHeader = "ClientDetails_0246_0689_";
        public const string XmlFooter = "_143100.xml";
        public const string Generation = "Generation";


        //
        //frmUploadHCCCsv
        //
        public const string HccCsvUploadonAt = "HCC CSV Upload on {date} at {time}";
        public const string UploadForBaseFileNameHasStarted = "Upload for {baseFilename} has started";
        public const string UploadForBaseFileNameHasCompleted = "Upload for {baseFilename} has completed";


        //
        //frmUploadOchinCsv
        //
        public const string OchinCsvUploadonAt = "OCHIN CSV Upload on {date} at {time}";
        public const string AnotherProcess = "another proces";
        public const string ErrorInsertingCsvDataFromFileIntoTheTable = "Error inserting CSV data from file '{csvFilePath}' into the table: {ex.Message}";




        //
        //frmUploadXMLFile
        //
        public const string ClientTrackUploadOnAt= "ClientTrack Upload on {date} at {time}";
        
        public const string LastFolderPathTxt = "LastFolderPath.txt";
        public const string Batch = "Batch";
        public const string BkslhClient = "//Client";
        public const string BkslhEligibilityDocument = "//EligibilityDocument";
        public const string BkslhServiceLineItem = "//ServiceLineItem";


        //
        //frmUploadXMLFile
        //
        public const string ServiceDate = "ServiceDate";
        public const string CreatedDate = "CreatedDate";




        //
        //MonthlyReport
        //
        public const string PreviousWeek = "Previous Week";
        public const string CurrentWeek = "Current Week";
        public const string PreviousMonth = "Previous Month";
        public const string CurrentMonth = "Current Month";
        public const string SinceOneMonthAgo = "Since One Month Ago";
        public const string FirstQuarter = "First Quarter";
        public const string SecondQuarter = "Second Quarter";
        public const string ThirdQuarter = "Third Quarter";
        public const string FourthQuarter = "Fourth Quarter";
        public const string PreviousQuarter = "Previous Quarter";
        public const string CurrentQuarter = "Current Quarter";
        public const string ThisYear = "This Year";
        public const string LastYear = "Last Year";
        public const string SinceThisDateLastYear = "Since This Date Last Year";




        //
        //ServiceCodeSetup
        //
        public const string ServiceCodeId = "ServiceCodeID";
        public const string HccExportToAries = "HCC_ExportToAries";
        public const string MapToHcc = "Map to HCC";
        public const string HccContractId= "HCC_ContractID";
        public const string Contract = "Contract";
        public const string PrimaryService = "Primary Service";
        public const string HccPrimaryService = "HCC_PrimaryService";
        public const string SecondaryService = "Secondary Services";
        public const string HccSecondaryService = "HCC_SecondaryService";
        public const string SubService = "Sub Service";
        public const string HccSubService = "HCC_Subservice";
        public const string UnitsOfMeasuresp = "Units Of Measure";
        public const string UnitsOfMeasure = "UnitsOfMeasure";
        public const string Unit = "Unit";
        public const string UnitValue = "UnitValue";
        public const string Update = "Update";
        public const string Insert = "Insert";



        //
        //DatabaseTables
        //
        public const string T_CLNT_DEMO = "T_CLNT_DEMO";
        public const string T_CLNT_ETHN_DTL = "T_CLNT_ETHN_DTL";
        public const string HCCCLIENTS = "HCCCLIENTS";
        public const string T_CLNT_HIV_INFO = "T_CLNT_HIV_INFO";
        public const string HCCClientMedCD4 = "HCCClientMedCD4";
        public const string T_CLNT_HIV_TEST = "T_CLNT_HIV_TEST";
        public const string HCCClientHIVTest = "HCCClientHIVTest";
        public const string T_CLNT_LVNG_STTN = "T_CLNT_LVNG_STTN";
        public const string HCCLvngSttn = "HCCLvngSttn";
        public const string T_CLNT_RACE_DTL = "T_CLNT_RACE_DTL";
        public const string HCCClientRace = "HCCClientRace";
        public const string T_CLNT_SITE = "T_CLNT_SITE";
        public const string HCCClientAddr = "HCCClientAddr";
        public const string T_CLNT_HSNG_ASSTNC = "T_CLNT_HSNG_ASSTNC";
        public const string T_CLNT_HSHLD_INCOME = "T_CLNT_HSHLD_INCOME";
        public const string HccClients = "HCCClients";
        public const string T_SITE = "T_SITE";
        public const string HccServices = "HCCServices";


        //
        //Status
        //
        public const string ZeroPercent = "0%";
        public const string ProgressUpdate = "ProgressUpdate";
        public const string InitialProgress = "0/0 (0%)";


    }


    public static class ContractIdList
    {
        //Property Text
       
        

        //Header Text



        
        public const string ErrorLogReport = "Error_Log_Report";
        
        public const string MonthlyReports = "Monthly_Reports";
        public const string ClientDemographicsReport = "Client_Demographics_Report";

        
        public const string DoyouwanttodeletecontractContractName = "‘Do you want to delete contract ‘Contract Name’?";
       
        
        public const string ConfirmDelete = "ConfirmDelete";
        
        public const string AreyousureyouwanttoEditthisrow = "Are you sure you want to Edit this row?";
        public const string Areyousureyouwanttodeletecontract = "Do you want to delete contract";

        public const string ConfirmEdit = "ConfirmEdit";
        public const string ContractIDortypeisinvalid = "ContractID or type is invalid.";


        public const string Contractupdatedsuccessfully = "Contract updated successfully";
        public const string Contractsavedsuccessfully = "Contract saved successfully";
        public const string Nochangesdetectedintheselectedrow = "No changes detected in the selected row.";
        public const string MustbeaDataGridViewCalendarCell = "Must be a DataGridViewCalendarCell";


        //Header Text
       
        public const string ContractNames = "Contract Name";

        public const string ConfirmMakeActive = "Confirm Activation";
        public const string ConfirmMakeInactive = "Confirm Deactivation";
        
       
       

        public const string DeleteColumnName = "DeleteColumn";
        public const string MakeActiveColumnName = "MakeActiveColumn";
        public const string MakeInactiveColumnName = "MakeInactiveColumn";
    }

    public static class XmlConstants
    {
        // XML Structure field names


        public const string DelimiterxmlGeneratorId = "DelimiterxmlGeneratorID";
        public const string Clntid = "Clnt_id";
        // TagNumber 
        public const string TagNumberFive = "5";
        public const string TagNumberTen = "10";
        public const string TagNumberThirty = Constants.DeleteContractstatus;

        // Example error message
        public const string ErrorMessage = "Your constant error message here.";

        // Example constant values
        public const string ContractServicesTagNumber = Constants.DeleteContractstatus;
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
    public static class ManualUploadConstants
    {
        public const string NoManualUploadsbetweenselecteddates = "No Manual Uploads between selected dates";
        public const string ManualUploadClientsReport = "Manual Upload Clients Report";








    }

    public static class ServiceCodeSetupConstants
    {
        public const string AreyousureyouwanttoaddanewService = "Are you sure you want to add a new Service";
    }
}



