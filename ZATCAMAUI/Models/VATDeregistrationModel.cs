using System.ComponentModel;
using Foundation;
using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{

    public class VATDeregistrationModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        private string _activeOutletDecisionOptions { get; set; }
        public string ActiveOutletDecisionOptions
        {
            get
            {
                return _activeOutletDecisionOptions;
            }
            set
            {
                _activeOutletDecisionOptions = value;
                OnPropertyRaised("ActiveOutletDecisionOptions");
            }
        }

        private bool _activeOutletDecisionOptionsIsSelected { get; set; }
        public bool ActiveOutletDecisionOptionsIsSelected
        {
            get
            {
                return _activeOutletDecisionOptionsIsSelected;
            }
            set
            {
                _activeOutletDecisionOptionsIsSelected = value;
                OnPropertyRaised("ActiveOutletDecisionOptionsIsSelected");
            }
        }

        public string ActiveOutletDocumentOptions { get; set; }
        public bool ActiveOutletDocumentOptionsIsSelected { get; set; }
        private Color textCol = (Color)Application.Current.Resources["Primary"];
        public Color TextCol
        {
            get { return textCol; }
            set
            {
                if (value != textCol)
                    textCol = value; OnPropertyRaised("TextCol");

            }
        }

        private string imgSource = "vat_tile_listofsignup_W";
        public string ImgSource
        {
            get { return imgSource; }
            set
            {
                if (value != imgSource)
                    imgSource = value; OnPropertyRaised("ImgSource");


            }
        }

    }
    
    public class VATDeregistrationModelRootObject
    {
        [JsonProperty("data")]
        public VATDeregistrationModelDetailD d { get; set; }
    }
    
    public class VATDeregistrationModelDetailD
    {
        [JsonProperty("deregistrationReasons")]
        public List<VATDeregistrationModelDetailsResult> results { get; set; }
    }
    
    public class VATDeregistrationLastICRDateRootObject
    {
        [JsonProperty("data")]
        public List<VATDeregistrationLastICRDateDetailsResult> d { get; set; }
    }
    
    public class VATDeregistrationLastICRDateDetailD
    {
        [JsonProperty("results")]

        public List<VATDeregistrationLastICRDateDetailsResult> results { get; set; }
    }
    
    public class VATDeregistrationLastICRDateDetailsResult
    {
        public __metadata __metadata { get; set; }

        [JsonProperty("lastICRDate")]
        public string Lasticrdt { get; set; }
        [JsonProperty("requestType")]
        public string Reqtp { get; set; }
        [JsonProperty("transactionType")]
        public string TxnTpx { get; set; }
        [JsonProperty("userType")]
        public string UserTypx { get; set; }
        [JsonProperty("TIN")]
        public string Gpartx { get; set; }


    }
    [Preserve(AllMembers = true)]
    public class VATDeRegistrationAttachmentDropdownDetails
    {
        
        public Metadata Metadata { get; set; }

        [JsonProperty("eligibleDocuments")]
        public ResultsAttachmentItemForElgblDocSet[] VatDeregSubItemsSet { get; set; }
        //public VATDeregistrationAttachmentDetailD d { get; set; }
    }
    
    public partial class VatDeregSubItemsSet
    {
        [JsonProperty("eligibleDocuments")]
        public ResultsAttachmentItemForElgblDocSet[] Results { get; set; }
    }
    
    public class VATDeregistrationSuspendedDateRootObject
    {
        [JsonProperty("data")]
        public VATDeregSuspendedDateRootObjectDetailD d { get; set; }
    }
    
    public class VATDeregSuspendedDateRootObjectDetailD
    {
        [JsonProperty("suspensions")]
        public List<VATDeregSuspendedDateRootObjectDetailsResult> dateResults { get; set; }
    }
    
    public class VATDeregSuspendedDateRootObjectDetailsResult
    {
        public __metadata __metadata { get; set; }

        public string TIN { get; set; }
        [JsonProperty("startDate")]
        public string StartDate { get; set; }
        [JsonProperty("endDate")]
        public string EndDate { get; set; }
        [JsonProperty("suspensionDateFrom")]
        public string SuspDtfrom { get;set; }
        [JsonProperty("suspensionDateTo")]
        public string SuspDtto { get; set; }
        [JsonProperty("nextDateFrom")]
        public string NextDtfrom { get; set; }
        [JsonProperty("nextDateTo")]
        public string NextDtto { get; set; }
        [JsonProperty("correspondenceDueDate")]
        public DateTime? Duedate { get; set; }

    }




    
    public class VATDeregistrationModelDetailsResult
    {
        public __metadata __metadata { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }
        [JsonProperty("language")]
        public string Lang { get; set; }
        [JsonProperty("transactionType")]
        public string TxnTp { get; set; }
        [JsonProperty("reasonDescription")]
        public string Rdesc { get; set; }

    }
    
    public class ResultsAttachmentItemForElgblDocSet : INotifyPropertyChanged
    {
        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }
        public __metadata __metadata { get; set; }
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [JsonProperty("language")]
        public string Spras { get; set; }
        [JsonProperty("formBundleType")]
        public string Fbtyp { get; set; }
        [JsonProperty("transactionType")]
        public string TxnTp { get; set; }
        [JsonProperty("documentCategory")]
        public string DmsTp { get; set; }
        [JsonProperty("startDate")]
        public string StartDt { get; set; }
        [JsonProperty("endDate")]
        public string EndDt { get; set; }
        [JsonProperty("name")]
        public string Txt50 { get; set; }
        private Color textCol = (Color)Application.Current.Resources["Primary"];
        public Color TextCol
        {
            get { return textCol; }
            set
            {
                if (value != textCol)
                    textCol = value; OnPropertyRaised("TextCol");

            }
        }

        private string imgSource = "vat_tile_listofsignup_W";
        public event PropertyChangedEventHandler PropertyChanged;

        public string ImgSource
        {
            get { return imgSource; }
            set
            {
                if (value != imgSource)
                    imgSource = value; OnPropertyRaised("ImgSource");


            }
        }


    }
    
    public class VATDeRegistrationDetails
    {
        [JsonProperty("data")]
        public vATDeRegistration d { get; set; }

        [JsonProperty("result")]
        public HeaderSet data { get; set; }
    }
    
    public class vATDeRegistration
    {
        public __metadata __metadata { get; set; }
        //[JsonProperty("result")]
        public HeaderSet headerSet { get; set; }
        [JsonProperty("addressSet")]
        public List<ResultsItemSet> AddressSet { get; set; }
        [JsonProperty("notesSet")]
        public List<VATDeregNote> NotesSet { get; set; }
        [JsonProperty("attachmentDetSet")]
        public List<Attachment> AttdetSet { get; set; }
        [JsonProperty("questionListSet")]
        public List<string> QuesListSet { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class VATDeregAttachmentRootOject
    {
        public VATDeregAttachment d { get; set; }
    }
    
    public class QuesListSet
    {
       
        public List<string> results { get; set; }
    }
    public class HeaderSet
    {
        [JsonProperty("isCase")]
        public bool CaseFg { get; set; }
        [JsonProperty("isCaseReason")]
        public string CaseReason { get; set; }
        [JsonProperty("isAgreed")]
        public bool Agreeflg { get; set; }
        [JsonProperty("isOpenCase")]
        public bool OpenCaseFg { get; set; }
        [JsonProperty("isReviewed")]
        public bool ReviewFg { get; set; }
        [JsonProperty("isAuditAttachment")]
        public bool Auditatt { get; set; }
        [JsonProperty("isCertificateAttachment")]
        public bool Certatt { get; set; }
        [JsonProperty("isChecked")]
        public bool Chkdt { get; set; }
        [JsonProperty("isContractAttachment")]
        public bool Contractatt { get; set; }
        [JsonProperty("isDeclaration1")]
        public bool Declareflg { get; set; }
        [JsonProperty("isDeclaration2")]
        public bool Declareflg2 { get; set; }
        [JsonProperty("isIncomeAttachment")]
        public bool Incomatt { get; set; }
        [JsonProperty("isIncomeStatementAttachment")]
        public bool Incstmtatt { get; set; }
        [JsonProperty("isInstructionAttachment")]
        public bool Insatt { get; set; }
        [JsonProperty("isLegalAttachment")]
        public bool Legatt { get; set; }
        [JsonProperty("isOtherAttachment")]
        public bool Otheratt { get; set; }
        [JsonProperty("operation")]
        public string Operationx { get; set; }
        [JsonProperty("transactionType")]
        public string TxnTpx { get; set; }
        [JsonProperty("idType")]
        public string Type { get; set; }
        [JsonProperty("userType")]
        public string UserTypx { get; set; }
        [JsonProperty("deregistrationDate")]
        public string Dregdt { get; set; }
        [JsonProperty("dueDate")]
        public string Duedate { get; set; }
        [JsonProperty("suspensionFromDate")]
        public string SuspDtfrom { get; set; }
        [JsonProperty("suspensionDateTo")]
        public string SuspDtto { get; set; }
        [JsonProperty("nextDateFrom")]
        public string NextDtfrom { get; set; }
        [JsonProperty("nextDateTo")]
        public string NextDtto { get; set; }
        [JsonProperty("declarationDate")]
        public string Declaredt { get; set; }
        [JsonProperty("endDate")]
        public string EndDate { get; set; }
        [JsonProperty("lastICRDate")]
        public string Lasticrdt { get; set; }
        [JsonProperty("startDate")]
        public string StartDate { get; set; }
        [JsonProperty("taxDate")]
        public string Taxdt { get; set; }
        [JsonProperty("activityName")]
        public string Actnm { get; set; }
        [JsonProperty("language")]
        public string Langx { get; set; }
        [JsonProperty("caseId")]
        public string CaseId { get; set; }
        [JsonProperty("suspensionPeriodKey")]
        public string SuspPeriod { get; set; }
        [JsonProperty("type")]
        public string Atype { get; set; }
        [JsonProperty("nextPeriodKey")]
        public string NextPeriod { get; set; }
        [JsonProperty("authorizationGroup")]
        public string Branchx { get; set; }
        [JsonProperty("calendarType")]
        public string Caltp { get; set; }
        [JsonProperty("contactFirstName")]
        public string Contactnm { get; set; }
        [JsonProperty("CRNumber")]
        public string Crno { get; set; }
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [JsonProperty("designationFirstName")]
        public string Designation { get; set; }
        [JsonProperty("serialNumber")]
        public string Euser { get; set; }
        [JsonProperty("formBundleNumber")]
        public string Fbnumx { get; set; }
        [JsonProperty("status1")]
        public string Fbstax { get; set; }
        [JsonProperty("userStatus")]
        public string Fbustx { get; set; }
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [JsonProperty("formProcess")]
        public string Formprocx { get; set; }
        [JsonProperty("forward")]
        public string Forwardx { get; set; }
        [JsonProperty("fullName")]
        public string FullName { get; set; }
        [JsonProperty("TIN")]
        public string Gpart { get; set; }
        [JsonProperty("TINx")]
        public string Gpartx { get; set; }
        [JsonProperty("idNumber")]
        public string Idnumbr { get; set; }
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        public string Mandtx { get; set; }
        [JsonProperty("firstName")]
        public string NameFirst { get; set; }
        [JsonProperty("lastName")]
        public string NameLast { get; set; }
        [JsonProperty("organizationName1")]
        public string NameOrg1 { get; set; }
        [JsonProperty("organizationName2")]
        public string NameOrg2 { get; set; }
        [JsonProperty("userName")]
        public string Officerx { get; set; }
        [JsonProperty("reasonDescription")]
        public string Other { get; set; }
        [JsonProperty("portalUser")]
        public string PortalUsrx { get; set; }
        [JsonProperty("reason")]
        public string Reason { get; set; }
        [JsonProperty("requestType")]
        public string Reqtp { get; set; }
        [JsonProperty("returnId")]
        public string ReturnIdx { get; set; }
        [JsonProperty("sourceIdentifier")]
        public string Srcidentifyx { get; set; }
        [JsonProperty("statusCode")]
        public string Statusx { get; set; }
        [JsonProperty("stepNumber")]
        public string StepNumber { get; set; }
        public string StepNumberx { get; set; }
        [JsonProperty("caseReason")]
        public string caseReason { set { CaseReason = value; } }
        //[JsonProperty("idType")]
        //public string IdType { set { Type = value; } }

        [JsonProperty("addresses")]
        public List<ResultsItemSet> AddressSet { get; set; }
        [JsonProperty("notes")]
        public List<VATDeregNote> NotesSet { get; set; }
        [JsonProperty("attachments")]
        public List<Attachment> AttdetSet { get; set; }
        [JsonProperty("questions")]
        public List<string> QuesListSet { get; set; }
    }

    [Preserve(AllMembers = true)]
    public class ResultsItemSet
    {
        public __metadata __metadata { get; set; }
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [JsonProperty("formBundleGUID")]
        public string FormGuid { get; set; }
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [JsonProperty("lineNumber")]
        public long LineNo { get; set; }
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; }
        [JsonProperty("addressType")]
        public string AddrType { get; set; }
        [JsonProperty("sourceIdentifier")]
        public string Srcidentify { get; set; }
        [JsonProperty("beginDate")]
        public string Begda { get; set; }
        [JsonProperty("endDate")]
        public string Endda { get; set; }
        [JsonProperty("TIN")]
        public string Gpart { get; set; }
        [JsonProperty("houseNumber1")]
        public string HouseNum1 { get; set; }
        [JsonProperty("houseNumber2")]
        public string HouseNum2 { get; set; }
        [JsonProperty("floor")]
        public string Floor { get; set; }
        [JsonProperty("city")]
        public string City1 { get; set; }
        [JsonProperty("district")]
        public string City2 { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("postalCode")]
        public string PostCode1 { get; set; }
        [JsonProperty("street")]
        public string Street { get; set; }
        [JsonProperty("building")]
        public string Building { get; set; }
        [JsonProperty("region")]
        public string Region { get; set; }
        [JsonProperty("regionDescription")]
        public string RegionDesc { get; set; }
        [JsonProperty("formGUID")]
        public string formGuid { set { FormGuid = value; } }
        [JsonProperty("buildingCode")]
        public string BuildingCode { set { Building = value; } }
        [JsonProperty("startDate")]
        public string StartDate { set { Begda = value; } }
    }
    

    public class AddressSet
    {
        public List<ResultsItemSet> results { get; set; }
    }
    

    public class NotesSet
    {
        public List<VATDeregNote> results { get; set; }
    }
    
    public class VATDeregNote
    {
        public Metadata2 __metadata { get; set; }
        [JsonProperty("noteNumber")]
        public string Notenoz { get; set; }
        [JsonProperty("referenceName")]
        public string Refnamez { get; set; }
        public string XInvoicez { get; set; }
        public string XObsoletez { get; set; }
        public string Rcodez { get; set; }
        [JsonProperty("userName")]
        public string Erfusrz { get; set; }
        [JsonProperty("entryDate")]
        public string Erfdtz { get; set; }
        public string Erftmz { get; set; }
        [JsonProperty("attachedByPerson")]
        public string AttByz { get; set; }
        [JsonProperty("portalUser")]
        public string ByPusrz { get; set; }
        [JsonProperty("TIN")]
        public string ByGpartz { get; set; }
        [JsonProperty("dataVersion")]
        public string DataVersionz { get; set; }
        [JsonProperty("name")]
        public string Namez { get; set; } //Name
        public string Noteno { get; set; }
        [JsonProperty("lineNumber")]
        public int Lineno { get; set; }
        [JsonProperty("elementNumber")]
        public int ElemNo { get; set; }
        [JsonProperty("notesFormat")]
        public string Tdformat { get; set; }
        [JsonProperty("textLine")]
        public string Tdline { get; set; }
        [JsonProperty("section")]
        public string Sect { get; set; } //Section
        [JsonProperty("startDate")]
        public string Strdt { get; set; } //date
        [JsonProperty("startTime")]
        public string Strtime { get; set; } //time
        [JsonProperty("notestext")]
        public string Strline { get; set; }   //note
    }
    
    public class VATDeregAttachment
    {
        public Metadata5 __metadata { get; set; }
        public string RetGuid { get; set; }
        public string Seqno { get; set; }
        public string SchGuid { get; set; }
        public string Dotyp { get; set; }
        public int Srno { get; set; }
        public string Doguid { get; set; }
        public string AttBy { get; set; }
        public string Filename { get; set; }
        public string FileExtn { get; set; }
        public string Mimetype { get; set; }
        public string ByPusr { get; set; }
        public string Erfdt { get; set; }
        public string Erftm { get; set; }
        public string DataVersion { get; set; }
        public string DocUrl { get; set; }
        public string OutletRef { get; set; }
        public string Enbedit { get; set; }
        public string Enbdele { get; set; }
        public string Visedit { get; set; }
        public string Visdel { get; set; }
    }
    
    public class VATDeregistrationAttachmentsModel
    {
        public VATDeregistrationAttachmentsModel()
        {
        }

        public string FieldTitle { get; set; }
        public string FieldSubTitle { get; set; }
        public string AttachmentName { get; set; }

        public bool IsAttachmentAttached { get; set; }
    }
    

    public class AttdetSet
    {
        public List<Attachment> results { get; set; }
    }
    

    public class VATDeregistrationSummaryModel
    {
        public VATDeregistrationSummaryModel()
        {
        }

        public string SummaryTitle { get; set; }
        public string SummaryData { get; set; }
        public bool IsEditVisible { get; set; }
    }



}
