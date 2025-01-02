
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Vml;
using System.Windows.Forms;

namespace RWDE
{
    /// <summary>
    /// Class containing constants used in the application.
    /// </summary>
    public static class Constants
    {
        public const string Sakku = "sakku ";
        public const string Sakkusmall = "sakku";

        public const string AgencyCode = "246_";

        public const string MyConnection = "MyConnection";
        public const string PlaceHolder = "PLACEHOLDER";

        public const string ConnectionStrings = "connectionStrings";



        public const string ClientTrackUploadon = "CLIENT Track Upload on";
        public const string Theservicefilenamecannotbenull = "The Service FileName Cannot be null";
        public const string BatchNotfound = "Batch not found.";
        public const string Conversionhasalreadybeencompletedforthisbatch = "Conversion has already been completed for this batch.";
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
        public const string ThesefileshavealreadybeenuploadedCloseandreopentouploadnewfiles = "These files have already been uploaded. Close and reopen to upload new files.";
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
        public const int ClientTrackCode = 24;
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
        public const string Selecrthefoldertosave = "Select the folder to save the data";
        public const string Startdatemustbeearlierthenenddate = "Start date must be earlier than End date.";
        public const string Datasuccessfullysaved = "Data successfully saved in the selected folder";
        public const string ServiceReconciliationReportFilename = "Service_Reconciliation Report";
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
        public const string Pleaseselectavalidfile = "Please select a valid Excel(.xlsx) file";
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
        public const string ActiveSmall = "Active";
        public const string Inactive = "INACTIVE";
        public const string ActiveContractstatus = "29";
        public const string InactiveContractstatus = "28";
        public const string DeleteContractstatus = "30";
        public const string ContractIDs = "Contract ID";
        public const string AreYouSureDeleteContract = "Are you sure you want to delete contract '{0}'?";
        public const string AreYouSureMakeActive = "Do you want to make the contract '{0}' active?";
        public const string AreYouSureMakeInactive = "Do you want to make the contract '{0}' inactive?";
        public const string ConfirmStatus = "Confirm '{0}' Status";

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
        public const string DdMMyyyyHHmmss = "dd-MM-yyyy HH:mm:ss";
        public const string YyyyMMdd = "yyyyMMdd";
        public const string YyyyMmDd = "yyyy-MM-dd";
        public const string YyyyMmDdSlash = "yyyy/MM/dd";
        public const string DdMMyyyy = "ddMMyyyy";
        public const string YyyyMMddHHmmss = "yyyy-MM-dd HH:mm:ss";
        public const string MMddyyyyHHmm = "MM-dd-yyyy HH:mm";
        public const string DdMMyyyyHyphen = "dd-MM-yyyy";
        public const string MMddyyyybkslash = "MM/dd/yyyy";
        public const string HHmmss = "HH:mm:ss";
        public const string MdYyyySlash = "M/d/yyyy";
        public const string DMYyyy = "d-M-yyyy";
        public const string DMYyyySlash = "d/M/yyyy";
        public const string MdYyyy = "M-d-yyyy";
        public const string DMmmYyyy = "d-MMM-yyyy";
        public const string MmmDYyyy = "MMM-d-yyyy";
        public const string YyyyMd = "yyyy-M-d";



        public const string MMddyyyyHHmmssbkslash = "MM/dd/yyyy HH:mm:ss";
        public const string MMddyyyyhhmmsstt = "MM/dd/yyyy hh:mm:ss tt";
        public const string Seconds = "Seconds";



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
        public const string ContractIdisrequired = "Contract ID is required.";
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
        public const string InvalidInput = "Invalid Input";




        public const string Dataprocessedandinsertedintothedatabasesuccessfully = "Data processed and inserted into the database successfully.";

        public const string Errorinsertingdataintothedatabase = "Error inserting data into the database: ";
        public const string ErroraddingremovedbatchIDtodatabase = "Error adding removed batch ID to database: ";
        public const string Errorupdatingbatch = "Error updating batch: ";
        public const string ErrorUpdatingFileProgress = "Error updating file progress: ";
        public const string AnErrorOccurred = "An error occurred: ";
        public const string AnErrorOccurredWhileLoadingData = "An error occurred while loading data.";

        public const string SqlError = "SQL Error: ";
        public const string DataInsertedSuccessfully = "Data inserted successfully.";
        public const string ErrorGettingTotalRows = "Error getting total rows: ";
        public const string ErrorUpdatingGridStatus = "Error updating grid status: ";
        public const string ErrorClearingtables = "Error clearing tables: ";
        public const string ErrorDeletingXmlFiles = "Error deleting XML files: ";
        public const string BatchIdAlreadyExists = "Batch ID {existingBatchId} already exist";
        public const string BatchIdAlreadyExistsCloseAndReopen = "Batch ID {existingBatchId} already exists. Close and reopen to upload new files.";
        public const string ErrorAddingRemovedBatchIdToDatabase = "Error adding removed batch ID to database:";
        public const string DoYouWantToDeleteServiceCodeId = "Do you want to delete Service {0}?";


        public const string CsVfilehasbeencreatedsuccessfullyat = "CSV file has been created successfully at ";
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
        public const string ServiceReconciliationReport = "Service Reconciliation Report";



        public const string Areyousureyouwanttoabort = "Are you sure you want to abort?";
        public const string TheFolderPathCannotBeEmpty = "The folder path cannot be empty. Please select a valid folder.";
        public const string SelectAFolderBeforeUploading = "Select a folder before uploading.";
        public const string PleaseSelectAFolderToUploadXmlFiles = "Please select a folder to upload XML files.";

        public const string TheFileIsAlreadyUploaded = "This File is already uploaded.Click On Browse to Choose another file to upload.";

        public const string TheSelectedFolderIsEmpty = "The selected folder is empty. Please select a folder containing .csv files.";
        public const string TheFolderContainsNonXmlFiles = "The folder contains non-XML files or folder is empty. Upload is allowed only for XML files.";

        public const string ErrorFetchingTheBatchId = "Error fetching the batch ID: ";






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
        //DbHelper
        public const string ConversionCompletion = "Conversioncompletion";
        public const string ClientConversionCompletion = "ClientCONVERSIONCOMPLETION";
        public const string ClientGenerationCompletion = "ClientgenerationCOMPLETION";
        public const string ServiceGenerationCompletion = "ServicegenerationCOMPLETION";
        public const string UpdateHcServicesWithErrors = "UpdateHCCServicesWithErrors";
        public const string SpServiceReconBatchId = "sp_service_reconbatchid";
        public const string InsertBatchTable = "insertbatchtable";
        public const string InsertClientServices = "InsertClientServices";
        public const string InsertClientInfoTest = "InsertClientInfotest";
        public const string InsertClientInfoPhiWithUrn = "InsertClientInfoPHIWithURN";
        public const string InsertClientInfoPhi = "InsertClientInfoPHI";
        public const string InsertIntoDlClients = "InsertIntoDlClients";
        public const string InsertIntoDlDeceasedClients = "InsertIntoDlDeceasedClients";
        public const string InsertClientData = "InsertClientData";
        public const string InsertDlConsent = "InsertDLConsent";
        public const string InsertDlEligibility = "InsertDlEligibility";
        public const string InsertDlServices = "InsertDlServices";
        public const string InsertDlServicesPhi = "InsertDlServicesPHI";
        public const string LoggerError = "Loggererror";
        public const string UpdateConversionServices = "updateconversionservices";
        public const string UpdateConversionClient = "updateconversionclient";
        public const string InsertDlFinancial = "InsertDlFinancial";
        public const string InsertCtClientsFromXmlPhiMaskingTest = "InsertCTClientsFromXMLPHIMASKINGTEST";
        public const string InsertCtClientsFromXml = "InsertCTClientsFromXML";
        public const string InsertServiceLineItemFromXmlPhi = "InsertServiceLineItemFromXMLPHI";
        public const string InsertLog = "InsertLog";
        public const string DeleteOchinBatchDatas = "DeleteochinBatchDatas";
        public const string DeleteBatchData = "DeleteBatchData";
        public const string DeleteHccBatchData = "DeleteHccBatchData";
        public const string OchinDataDelete = "OCHINDATADELETE";
        public const string ViewAllBatchDatas = "ViewAllBatchDatas";
        public const string ViewAllBatchDatasLoad = "ViewAllBatchDatasLOAD";
        public const string ViewAllBatchDatasHcc = "ViewAllBatchDatasHCC";
        public const string GetParticularBatchDatas = "GetParticularBatchDatas";
        public const string GetParticularConversionDatas = "GetParticularConversionDatas";
        public const string GetAllBatchType = "GETALLBATCHTYPE";
        public const string GetAllBatchTypeHcc = "GETALLBATCHTYPEHCC";
        public const string GETALLBATCHTYPEview = "GETALLBATCHTYPEview";
        public const string GetParticularnGenerationDatasConversionXml = "GetParticularnGenerationDatasCONVERSIONxml";
        public const string GetParticularnGenerationDatasConversion = "GetParticularnGenerationDatasCONVERSION";
        public const string GetParticularnGenerationDatasConversionHcc = "GetParticularnGenerationDatasCONVERSIONHCC";
        public const string GetAllContractLists = "GetAllContractLists";
        public const string Sp_GetTopServiceCodeSetup = "sp_GetTopServiceCodeSetup";
        public const string InsertXmlgeneratorTimeServices = "insertXMLgeneratortimeServices";
        public const string SpCreateDeceasedClientViewCount = "spCreateDeceasedClientViewcount";
        public const string ClientGeneratorXmlDemo = "clientgeneratorXMLDEMO";
        public const string InsertXmlgeneratorTimeClient = "insertXMLgeneratortimeClient";
        public const string FetchSubClientDataFromMedCd4 = "FetchSubClientDataFromMedCD4";
        public const string FetchSubClientDataFromMedVl = "FetchSubClientDataFromMedVL";
        public const string FetchSubClientDataFromHivTest = "FetchSubClientDataFromHIVTest";
        public const string FetchSubClientDataFromInsur = "FetchSubClientDataFromInsur";
        public const string FetchSubClientDataFromRace = "FetchSubClientDataFromRace";
        public const string InsertOrUpdateContract = "InsertOrUpdateContract";
        public const string UpdateContractStatus = "UpdateContractStatus";
        public const string UpdateContract = "UpdateContract";
        public const string UpdateServiceCodeStatus = "UpdateServiceCodeStatus";
        public const string InsertOrUpdateServiceCode = "InsertOrUpdateServiceCode";
        public const string GetActiveContracts = "GetActiveContracts";
        public const string SpUploadDashboardReport = "sp_Upload_DashboardREPORT";
        public const string UpdateHccServicesWithErrors = "UpdateHCCServicesWithErrors";
        public const string SpHccRecon = "sp_HCCRecon";
        public const string ManualUploadReport = "ManualUploadReport";





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


        public const string GetNextBatchIdQuery = "SELECT ISNULL(MAX(BatchID), 0) FROM Batch";
        public const string GetAbortBatchIdQuery = "SELECT MAX(batchid) FROM Batch";
        public const string GetMaxXmlBatchIdQuery = "SELECT ISNULL(MAX(BatchID), 0) FROM CTClients";
        public const string GetMXmlBatchIdQuery = "SELECT Max(batchid) FROM Batch WHERE FileName LIKE '%XML%'";

        public const string CTClientsEligibilityDocQuery = "INSERT INTO [RWDE].[dbo].[CTClientsEligibilityDoc] (DocumentType, DocumentDate, ObtainDate, ExpireDate, Source, Notes, BatchID, AgencyClientID1, AriesID, CreatedBy, CreatedOn, EligibilityDocID) VALUES (@DocumentType, @DocumentDate, @ObtainDate, @ExpireDate, @Source, @Notes, @BatchID, @AgencyClientID1, @AriesID, @CreatedBy, @CreatedOn, @EligibilityDocID)";
        public const string LoggingErrorQuery = "INSERT INTO Logger (Type, Module, Stack, Message, FileName, LineNumber, FunctionName, Comments, CreatedBy, CreatedOn) VALUES (@Type, @Module, @Stack, @Message, @FileName, @LineNumber, @FunctionName, @Comments, @CreatedBy, @CreatedOn)";
        public const string GetClientIdsQuery = "SELECT Clnt_id,Agency_client_2 FROM HCCClients WHERE CreatedOn >= @StartDate AND CreatedOn <= @EndDate";
        public const string GetServiceIdsQuery = "SELECT ServiceID, Service_date FROM HCCServices WHERE CreatedOn >= @StartDate AND CreatedOn <= @EndDate";
        public const string GetFilteredDataQuery = @"SELECT * FROM vwDeceased_Client WHERE [Download Date] BETWEEN @StartDate AND @EndDate;";
        public const string LoadDataMonthYearQuery = @"SELECT * FROM HCCServices WHERE YEAR(CreatedOn) = @Year AND MONTH(CreatedOn) = @Month AND Status IN ('Upload Succeeded', 'Upload Failed')";

        public const string LoadDataQuery = @"
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

        public const string ServiceReconServiceDateQuery = @"SELECT * FROM vwService_Reconciliationdatefilter WHERE ServiceDate BETWEEN @StartDate AND @EndDate OR ServiceDate = @StartDate OR ServiceDate = @EndDate";
        public const string ServiceReconCreatedDateQuery = @"SELECT * FROM vwService_ReconciliationCreatedDateFilter WHERE EntryDate BETWEEN @StartDate AND @EndDate OR EntryDate = @StartDate OR EntryDate = @EndDate";
        public const string ServiceReconBatchIdQuery = @"SELECT * FROM vwService_Reconciliationtest WHERE BatchID = @BatchID";

        public const string ClientDemographicsQuery = @"select * from vw_ClientDemographics where [Created On]  between @StartDate and @EndDate";

        public const string LoadLogErrorQuery = @"SELECT * FROM vw_ErrorLog WHERE  Date  BETWEEN @StartDate AND @EndDate";

        public const string GetHccServicesQuery = "SELECT  ServiceID,Actual_minutes_spent,Staff_id,Clnt_id, Service_date, CreatedOn,Contract_id,MappedToHCC,Quantity_served FROM HCCServices";
        public const string GetHccClientsQuery = "SELECT Clnt_id,CreatedOn,Agency_client_1 FROM HCCClients";
        public const string GetHccClientsFilterQuery = @" SELECT  Staff_id,  Clnt_id, Service_date,  CreatedOn, DATEADD(day, -1, CreatedOn) AS CreatedOnMinusOne -- Subtract 1 day from CreatedOn FROM HCCServices WHERE CreatedOn >= @StartDate AND CreatedOn <= @EndDate";

        public const string ServiceCodeSetupQuery = "SELECT HCC_ExportToAries FROM ServiceCodeSetup";
        public const string GetAgencyClientIdQuery = "SELECT Agency_client_1 FROM HCCClients";

        public const string GetTotalServiceQuery = "SELECT COUNT(CMSServiceID) FROM CMSServices";
        public const string GetNotMappedServicesQuery = "SELECT COUNT(ServiceID) FROM HCCServices WHERE MappedToHCC = 'False'";
        public const string GetMappedServicesQuery = "SELECT COUNT(ServiceID) FROM HCCServices WHERE Status = 'Succeeded'";
        public const string GetNotExportedServicesQuery = "SELECT COUNT(ServiceID) FROM HCCServices WHERE Status <> 'Succeeded'";
        public const string GetMhServiceCountQuery = "SELECT COUNT(ServiceID) FROM HCCServices WHERE Service = 'MH'";
        public const string GetPostTimeBoxServicesQuery = "SELECT COUNT(ServiceID) FROM HCCServices WHERE PostTimeBox = 1";
        public const string GetMissingExpiryServicesQuery = "SELECT COUNT(ServiceID) FROM HCCServices WHERE HCCconsentExpired = '1'";
        public const string GetMissingHccIdServicesQuery = "SELECT COUNT(ServiceID) FROM HCCServices WHERE HCCID IS NULL";
        public const string GetNotEnrolledServicesQuery = "SELECT COUNT(ServiceID) FROM HCCServices WHERE ProgramEnrolled IS NULL";
        public const string GetPreRegServiceCountQuery = "SELECT COUNT(ServiceID) FROM HCCServices WHERE PreReg = 1";
        public const string GetEligibilityMissingServicesQuery = "SELECT COUNT(ServiceID) FROM HCCServices WHERE RWEligibilityExpired = 1";
        public const string GetStaffMissingServicesQuery = "SELECT COUNT(ServiceID) FROM HCCServices WHERE StaffloginMissing = 1";
        public const string GetZerUnitOfServicesQuery = "SELECT COUNT(ServiceID) FROM HCCServices WHERE Unit_cd = 0";
        public const string GetWaiverServicesQuery = "SELECT COUNT(ServiceID) FROM HCCServices WHERE Service = 'Waiver'";
        public const string GetServiceDatesQuery = @"SELECT Service_date  FROM HCCServices";
        public const string GetItDropServicesQuery = "SELECT COUNT(ServiceID) FROM HCCServices WHERE DataTeamInvestigationforErrors = 'True'";




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
        public const string AtDescription = "@Description";
        public const string AtPath = "@Path";
        public const string AtType = "@Type";
        public const string AtUploadStartedAt = "@UploadStartedAt";
        public const string AtUploadEndedAt = "@UploadEndedAt";
        public const string AtTotalRows = "@TotalRows";
        public const string AtSuccessfulRows = "@SuccessfulRows";
        public const string AtFailedRows = "@FailedRows";
        public const string AtCreatedBy = "@CreatedBy";
        public const string AtCreatedOn = "@CreatedOn";
        public const string AtComments = "@Comments";
        public const string AtClntId = "@clnt_id";
        public const string AtServiceDate = "@Service_date";
        public const string AtContractId = "@Contract_id";
        public const string AtStaffId = "@Staff_id";
        public const string AtPrimServDesc = "@Prim_serv_desc";
        public const string AtIdEqualTto = "Id=";
        public const string IdHyphen = "Id-";
        public const string IdColon = "Id:";
        public const char EqualTo = '=';
        public const char Hyphen = '-';
        public const char Colon = ':';
        public const string AtIdEqualTtoCaps = "ID=";
        public const string AtQuantityServed = "@Quantity_served";
        public const string AtUnitCd = "@Unit_cd";
        public const string AtActualMinutesSpent = "@Actual_minutes_spent";
        public const string AtServiceId = "@ServiceID";
        public const string AtAdditionalServiceInformation = "@AdditionalServiceInformation";

        public const string AtFirstNm = "@first_nm";
        public const string AtLastNm = "@last_nm";
        public const string AtMi = "@mi";
        public const string AtChsnNm = "@chsn_nm";
        public const string AtDob = "@dob";
        public const string AtIsDecd = "@is_decd";
        public const string AtDtOfDeath = "@dt_of_death";
        public const string AtPlaceOfDeath = "@place_of_death";
        public const string AtSsn = "@SSN";
        public const string AtHomelessFlg = "@homeless_flg";
        public const string AtGndrCd = "@gndr_cd";
        public const string AtSexCd = "@sex_cd";
        public const string AtLangPrefCd = "@lang_pref_cd";
        public const string AtMrtlStatCd = "@mrtl_stat_cd";
        public const string AtSexualOrntTypeCd = "@sexual_ornt_type_cd";
        public const string AtEduLvl = "@edu_lvl";
        public const string AtVeteran = "@veteran";
        public const string AtEmail = "@email";
        public const string AtAllowCntctEmailInd = "@allow_cntct_email_ind";
        public const string AtPrsnMobilePhn = "@prsn_mobile_phn";
        public const string AtAllowCntctMobileInd = "@allow_cntct_mobile_ind";
        public const string AtAllowMsgsMobileInd = "@allow_msgs_mobile_ind";
        public const string AtAllowVmMobileInd = "@allow_vm_mobile_ind";
        public const string AtEmergencyCntctNm = "@emergency_cntct_nm";
        public const string AtEmergencyCntctRltnshp = "@emergency_cntct_rltnshp";
        public const string AtEmergencyPrsnMobilePhn = "@emergency_prsn_mobile_phn";
        public const string AtAllowEmergencyCntctInd = "@allow_emergency_cntct_ind";
        public const string AtAllowEmergencyCntctMsgsInd = "@allow_emergency_cntct_msgs_ind";
        public const string AtAllowEmergencyCntctVmInd = "@allow_emergency_cntct_vm_ind";
        public const string AtAgencyId = "@agency_id";
        public const string AtRegstrnDt = "@regstrn_dt";
        public const string AtAgencyClient1 = "@agency_client_1";
        public const string AtAgencyClient2 = "@agency_client_2";
        public const string AtFkAgencyStatusCd = "@fk_agency_status_cd";
        public const string AtAgencyStatusDt = "@agency_status_dt";
        public const string AtRelocFkStateCd = "@reloc_fk_state_cd";
        public const string AtRelocFkCountyCd = "@reloc_fk_county_cd";
        public const string AtFkAddrTypeCd = "@fk_addr_type_cd";
        public const string AtAddressLine1 = "@address_line_1";
        public const string AtAddressLine2 = "@address_line_2";
        public const string AtCity = "@city";
        public const string AtFkStateCd = "@fk_state_cd";
        public const string AtCounty = "@county";
        public const string AtZip = "@zip";
        public const string AtAddressSince = "@AddressSince";
        public const string AtMailAllwInd = "@mail_allw_ind";
        public const string AtClientIncomeYear = "@client_income_year";
        public const string AtClientNoIncomeFinResInd = "@client_no_income_fin_res_ind";
        public const string AtClientTotalMthIncm = "@client_total_mth_incm";
        public const string AtHhIncomeYear = "@hh_income_year";
        public const string AtHhNoIncomeFinResInd = "@hh_no_income_fin_res_ind";
        public const string AtEarnIncmFrmEmplmnt = "@earn_incm_frm_emplmnt";
        public const string AtRetirementIncm = "@retirement_incm";
        public const string AtSupSecIncm = "@sup_sec_incm";
        public const string AtSocDisInsIncm = "@soc_dis_ins_incm";
        public const string AtOthrWlfrAsstIncm = "@othr_wlfr_asst_incm";
        public const string AtPvtDisabInsIncm = "@pvt_disab_ins_incm";
        public const string AtVtrnDisPymtIncm = "@vtrn_dis_pymt_incm";
        public const string AtRegCntbrOthrIncm = "@reg_cntbr_othr_incm";
        public const string AtWrkrCompIncm = "@wrkr_comp_incm";
        public const string AtGnrlAsstIncm = "@gnrl_asst_incm";
        public const string AtUnemplInsIncm = "@unempl_ins_incm";
        public const string AtOthrSrcIncm = "@othr_src_incm";
        public const string AtHshldSize = "@hshld_size";
        public const string AtFkEmplymntStatCd = "@fk_emplymnt_stat_cd";
        public const string AtFkRaceCd = "@fk_race_cd";
        public const string AtFkRaceDtlCd = "@fk_race_dtl_cd";
        public const string AtFkEthnCd = "@fk_ethn_cd"; 
        public const string AtFkEthnDtlCd = "@fk_ethn_dtl_cd";
        public const string AtCurrHivStatCd = "@curr_hiv_stat_cd";
        public const string AtHivDxDt = "@hiv_dx_dt";
        public const string AtHivDxSrcTxt = "@hiv_dx_src_txt";
        public const string AtAidsDxDt = "@aids_dx_dt";
        public const string AtAidsDxSrcTxt = "@aids_dx_src_txt";
        public const string AtPerinatalTransmission = "@perinatal_transmission";
        public const string AtMaleToMaleSexualContact = "@male_to_male_sexual_contact";
        public const string AtHighRiskHeterosexualContact = "@high_risk_heterosexual_contact";
        public const string AtInjectionDrugUse = "@injection_drug_use";
        public const string AtHemophiliaCoagulationDisorder = "@hemophilia_coagulation_disorder";
        public const string AtReceiptOfBloodTransfusion = "@receipt_of_blood_transfusion";
        public const string AtRiskFactorNotReportedIdentifier = "@risk_factor_not_reported_identifier";
        public const string AtHivTestDt = "@hiv_test_dt";
        public const string AtHivRsltStatCd = "@hiv_rslt_stat_cd";
        public const string AtFkInsTypeCd = "@fk_ins_type_cd";
        public const string AtFkInsSubTypeCd = "@fk_ins_sub_type_cd";
        public const string AtNewCovrgCvrsOldCvrgInd = "@new_covrg_cvrs_old_cvrg_ind";
        public const string AtStartDate = "@start_date";
        public const string AtEndDate = "@end_date";
        public const string AtNotes = "@notes";
        public const string AtHsngAsstncCd = "@hsng_asstnc_cd";
        public const string AtAsstncStartDt = "@asstnc_start_dt";
        public const string AtAsstncEndDt = "@asstnc_end_dt";
        public const string AtFkLvngSttnCd = "@fk_lvng_sttn_cd";
        public const string AtFkLvngSttnDtlCd = "@fk_lvng_sttn_dtl_cd";
        public const string AtHousingStatus = "@housing_status";
        public const string AtAsofDt = "@asof_dt";
        public const string AtSourceSystemName = "@SourceSystemName";
        public const string AtUserId = "@UserID";
        public const string AtSourceId = "@sourceid";
        public const string AtAgencyIdCaps = "@AgencyID";


        public const string AtClientIdCaps = "@ClientID";
        public const string AtClientFirstName = "@ClientFirstName";
        public const string AtClientLastName = "@ClientLastName";
        public const string AtClientMiddleInitial = "@ClientMiddleInitial";
        public const string AtClientMothersMaidenNameFirstAndThirdCharacters = "@ClientMothersMaidenNameFirstandThirdCharacters";


        public const string AtClientDateOfBirth = "@ClientDateofBirth";
        public const string AtClientGender = "@ClientGender";
        public const string AtClientIsRelatedOrAffected = "@ClientIsRelatedOrAffected";
        public const string AtClientRecordIsShared = "@ClientRecordIsShared";
        public const string AtClientUrnExtended = "@ClientURNExtended";
        public const string AtAgencyClientId1 = "@AgencyClientID1";
        public const string AtDownloadDate = "@DownloadDate";
        public const string AtExtracted = "@Extracted";
        public const string AtExtractionDate = "@ExtractionDate";
        public const string AtCmsMatch = "@CMSMatch";
        public const string AtCmsMatchDate = "@CMSMatchDate";


        public const string AtClientLastFirstName = "@ClientLastFirstName";
        public const string AtClientStatus = "@ClientStatus";
        public const string AtStatusAsOfDate = "@StatusAsofDate";
        public const string AtLastServiceDate = "@LastServiceDate";
        public const string AtAgencyClientId = "@AgencyClientID";
        public const string AtDocumentType = "@DocumentType";
        public const string AtDocumentDate = "@DocumentDate";
        public const string AtObtainDate = "@ObtainDate";
        public const string AtExpireDate = "@ExpireDate";
        public const string AtSource = "@Source";
        public const string AtCreatedSource = "@CreatedSource";
        public const string AtCreateAgency = "@CreateAgency";
        public const string AtEligibilityDocumentExpireDate = "@EligibilityDocumentExpireDate";

        public const string AtClientUrn = "@ClientURN";
        public const string AtServiceNotes = "@ServiceNotes";

        public const string AtModule = "@Module";
        public const string AtStack = "@Stack";
        public const string AtMessage = "@Message";
        public const string AtLineNumber = "@LineNumber";
        public const string AtFunctionName = "@FunctionName";

        public const string AtFinancialTotalIncomeMonthly = "@FinancialTotalIncomeMonthly";
        public const string AtFinancialTotalIncomeAnnual = "@FinancialTotalIncomeAnnual";
        public const string AtFinancialIsClientIncomeMonthly = "@FinancialIsClientIncomeMonthly";
        public const string AtFinancialEmploymentStatus = "@FinancialEmploymentStatus";
        public const string AtFinancialPublicAssistance = "@FinancialPublicAssistance";
        public const string AtFinancialEmploymentSalaryWages = "@FinancialEmploymentSalaryWages";
        public const string AtFinancialUnemploymentBenefits = "@FinancialUnemploymentBenefits";
        public const string AtFinancialVeteransBenefits = "@FinancialVeteransBenefits";
        public const string AtFinancialSsi = "@FinancialSSI";
        public const string AtFinancialSsdi = "@FinancialSSDI";
        public const string AtFinancialSsa = "@FinancialSSA";
        public const string AtFinancialGeneralAssistance = "@FinancialGeneralAssistance";
        public const string AtFinancialTanf = "@FinancialTANF";
        public const string AtFinancialFoodStamps = "@FinancialFoodStamps";
        public const string AtFinancialStateDisability = "@FinancialStateDisability";
        public const string AtFinancialLongTermDisability = "@FinancialLongTermDisability";
        public const string AtFinancialGift = "@FinancialGift";
        public const string AtFinancialRetirement = "@FinancialRetirement";
        public const string AtFinancialAlimony = "@FinancialAlimony";
        public const string AtFinancialInvestment = "@FinancialInvestment";
        public const string AtFinancialWorkersCompensation = "@FinancialWorkersCompensation";
        public const string AtFinancialOther1 = "@FinancialOther1";
        public const string AtFinancialOtherAmount1 = "@FinancialOtherAmount1";
        public const string AtFinancialOther2 = "@FinancialOther2";
        public const string AtFinancialOtherAmount2 = "@FinancialOtherAmount2";
        public const string AtFinancialOther3 = "@FinancialOther3";
        public const string AtFinancialOtherAmount3 = "@FinancialOtherAmount3";
        public const string AtFinancialHasNoSourceOfIncome = "@FinancialHasNoSourceOfIncome";
        public const string AtFinancialHouseholdIncome = "@FinancialHouseholdIncome";
        public const string AtFinancialIsHouseholdIncomeMonthly = "@FinancialIsHouseholdIncomeMonthly";
        public const string AtFinancialPeopleInHousehold = "@FinancialPeopleInHousehold";
        public const string AtFinancialChildrenInHousehold = "@FinancialChildrenInHousehold";
        public const string AtFinancialHivPositiveInHousehold = "@FinancialHIVPositiveInHousehold";
        public const string AtFinancialHouseholdPovertyLevelbyGroup = "@FinancialHouseholdPovertyLevelbyGroup";
        public const string AtFinancialHouseholdPovertyLevel = "@FinancialHouseholdPovertyLevel";
        public const string AtFinancialFamilyIncome = "@FinancialFamilyIncome";
        public const string AtFinancialIsFamilyIncomeMonthly = "@FinancialIsFamilyIncomeMonthly";
        public const string AtFinancialPeopleInFamily = "@FinancialPeopleInFamily";
        public const string AtFinancialFamilyPovertyLevel = "@FinancialFamilyPovertyLevel";
        public const string AtFinancialOwnsHouse = "@FinancialOwnsHouse";
        public const string AtFinancialOwnsCar = "@FinancialOwnsCar";
        public const string AtFinancialHasOtherAssets = "@FinancialHasOtherAssets";
        public const string AtFinancialOtherAssets = "@FinancialOtherAssets";
        public const string AtFinancialLastSavedDate = "@FinancialLastSavedDate";
        public const string AtFinancialCreateSource = "@FinancialCreateSource";

        public const string XmlData = "@XmlData";
        public const string AtNotesCaps = "@Notes";
        public const string AtAriesID = "@AriesID";
        public const string AtEligibilityDocID = "@EligibilityDocID";

        public const string AtClientAriesID = "@ClientAriesID";
        public const string AtClientUrnExt = "@ClientURNExt";
        public const string AtSiteName = "@SiteName";
        public const string AtStaffLogin = "@StaffLogin";
        public const string AtContractName = "@ContractName";
        public const string AtServiceDateCaps = "@ServiceDate";
        public const string AtProgram = "@Program";
        public const string AtPrimaryService = "@PrimaryService";
        public const string AtSecondaryService = "@SecondaryService";
        public const string AtSubservice = "@Subservice";
        public const string AtUnitsOfService = "@UnitsOfService";
        public const string AtRateForUnitOfService = "@RateForUnitOfService";
        public const string AtMeasurementUnit = "@MeasurementUnit";
        public const string AtTotalCost = "@TotalCost";
        public const string AtActualMinutesSpentCaps = "@ActualMinutesSpent";

        public const string AtBatchType = "@BatchType";
        public const string AtFromDate = "@FromDate";
        public const string AtEndDateCaps = "@EndDate";

        public const string AtClientid = "@Clientid";
        public const string AtDatetime = "@Datetime";

        public const string AtContractID = "@ContractID";
        public const string AtStartedDateTime = "@StartedDateTime";
        public const string AtEndedDateTime = "@EndedDateTime";
        public const string AtOperation = "@Operation";
        public const string AtServiceCodeID = "@ServiceCodeID";

        public const string AtService = "@Service";
        public const string AtHCCExportToAries = "@HCC_ExportToAries";
        public const string AtHCCContractID = "@HCC_ContractID";
        public const string AtHCCPrimaryService = "@HCC_PrimaryService";
        public const string AtHccSecondaryService = "@HCC_SecondaryService";
        public const string AtHccSubservice = "@HCC_Subservice";
        public const string AtUnitsOfMeasure = "@UnitsOfMeasure";
        public const string AtUnitValue = "@UnitValue";

        public const string AtStartDateCaps = "@StartDate";
        public const string AtYear = "@Year";
        public const string AtMonth = "@Month";






        //
        //DbHelper
        //
        public const string OchinToRwdeOnAt = "OCHIN TO RWDE On '{0}' at '{1}'";
        public const string ErrorInsertingBatch = "Error inserting batch: {0}";
        public const string ConsentData = "ConsentData";
        public const string FailedToParseDate = "Failed to parse date: {0}";
        public const string ServiceNotesCannotBeNullOrEmpty = "Service notes cannot be null or empty.";
        public const string TwoThreeFour = "2/3/4";
        public const string ErrorInsertingClientParametersIntoTheTable = "Error inserting client parameters into the table: ";
        public const string ErrorInsertingServiceLineItems = "Error inserting service line items: ";
        public const string ErrorLoggingMessage = "Error logging message: ";
        public const string ErrorDeletingBatch = "Error deleting batch: ";
        public const string ErrorRetrievingBatchData = "Error retrieving batch data";
        public const string ErrorRetrievingData = "Error retrieving data";
        public const string ErrorInInsertOrUpdateContractMethod = "Error in InsertOrUpdateContract method: ";


        public const string DlFinancials = "DlFinancials";
        public const string InvalidDecimalValue  = "Invalid decimal value: ";
        public const string InvalidIntegerValue = "Invalid integer value: ";
        public const string BkslashClient = "//Client";
        public const string BkslashServiceLineItem = "//ServiceLineItem";
        public const string ClientAriesID = "_clientAriesID";

        public const string AriesId = "ariesID";
        public const string EligibilityDocument = "EligibilityDocument";
        public const string AgencySpecifics = "AgencySpecifics";
        public const string AgencyClientId1 = "agencyClientID1";
        public const string DocumentType = "documentType";
        public const string DocumentDate = "documentDate";
        public const string ObtainDate = "obtainDate";
        public const string ExpireDate = "expireDate";
        public const string Source = "source";
        public const string Notes = "notes";
        public const string ErrorInsertingEligibilityDocuments = "Error inserting eligibility documents: ";
        public const string ClientariesIDEligibilityDocument = "//Client[@ariesID='{0}']/EligibilityDocument";

        public const string ClientUrnExt = "_clientURNExt";
        public const string SiteName = "_siteName";
        public const string StaffLogin = "_staffLogin";
        public const string ContractNamesmall = "_contractName"; 
        public const string ServiceDatesmall = "serviceDate";
        public const string Program = "program";
        public const string PrimaryServiceSmall = "primaryService";
        public const string SecondaryServiceSmall = "secondaryService";
        public const string Subservice = "subservice";
        public const string UnitsOfService = "unitsOfService";
        public const string RateForUnitOfService = "rateForUnitOfService";
        public const string MeasurementUnit = "measurementUnit";
        public const string TotalCost = "totalCost";
        public const string ActualTimeSpentMinutes = "actualTimeSpentMinutes";
        public const string ClientTrack = "Client Track";
        public const string Value = "Value";
        public const string QueryExecutedButNoDataFound = "Query executed, but no data found.";
        public const string Clntid = "Clnt_id";

        public const string AgencyClient1 = "Agency_client_1";


        public const string One = "1";
        public const string Zero = "0";









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
        public const string ServiceDate = Constants.ServiceDatesmall;
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
        //ServiceReconciliationReport
        //
        public const string SlNo = "Sl No";
        public const string Staff = "Staff";
        public const string HccConsentExpiryDate = "HCCConsentExpiryDate";
        public const string HccConsentExpiryDatesp = "HCC Consent Expiry Date";
        public const string RwEligibilityExpiryDate = "RWEligibilityExpiryDate";
        public const string RwEligibilityExpiryDatesp = "RW Eligibility Expiry Date";
        public const string ServiceCodeIdsp = "Service Code ID";

        public const string HccContractIdSp = "HCC Contract ID";
        public const string UnitsOfServices = "UnitsOfServices";
        public const string UnitsOfServicesSp = "Units of Services";
        public const string ActualMinutesSpent = "ActualMinutesSpent";
        public const string ActualMinutesSpentSp = "Actual Minutes Spent";
        public const string ServiceId = "ServiceID";
        public const string ServiceIdSp = "Service ID";
        public const string ServiceExportedToHcc = "ServiceExportedToHCC";
        public const string ServiceExportedToHccSp = "Service Exported to HCC";
        public const string ServiceDateSp = "Service Date";
        public const string EntryDate = "EntryDate";
        public const string EntryDateSp = "Entry Date";
        public const string Lag = "Lag";
        public const string LagStatus = "LagStatus";
        public const string LagStatusSp = "Lag Status";
        public const string HccExportFailureReason = "HCCExportFailureReason";
        public const string HccExportFailureReasonSp = "HCC Export Failure Reason";

        public const string PleaseEnterValidBatchIds = "Please enter valid Batch IDs.";
        public const string YourCondition = "YourCondition";




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



