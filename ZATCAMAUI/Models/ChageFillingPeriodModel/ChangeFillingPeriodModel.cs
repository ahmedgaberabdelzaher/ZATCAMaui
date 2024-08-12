using Foundation;
using Newtonsoft.Json;

namespace ZATCAMAUI.Models.ChageFillingPeriodModel
{

    public class ChangeFillingPeriodModel
    {
        public ChangeFillingPeriodModel()
        {
        }
        public string CardLabel { get => ActiveOutletDecisionOptions; }
        public string ActiveOutletDecisionOptions { get; set; }
        public bool ActiveOutletDecisionOptionsIsSelected { get; set; }
    }

   
    public class InstalmentAgreementAttachmentsModel
    {
        public InstalmentAgreementAttachmentsModel()
        {
        }

        public string FieldTitle { get; set; }
        public string FieldSubTitle { get; set; }
        public string AttachmentName { get; set; }

        public bool IsAttachmentAttached { get; set; }
    }

   
    public class MyRequestsListModel
    {
        public MyRequestsListModel()
        {
        }

        public string Title { get; set; }
        public string ReferenceNumber { get; set; }
        public string Status { get; set; }
        public string CurrentFrequency { get; set; }
        public string NewFrequency { get; set; }
        public string EffectiveDate { get; set; }
        public string ReleaseDate { get; set; }
    }


/* Unmerged change from project 'ZATCAMAUI (net7.0-android33.0)'
Before:
    #region VATchangeFillingPeriodPostModel

    public class VATchangeFillingPeriodPostModel
After:
    #region VATchangeFillingPeriodPostModel

    public class VATchangeFillingPeriodPostModel
*/
    #region VATchangeFillingPeriodPostModel

    public class VATchangeFillingPeriodPostModel
    {
        [Preserve(AllMembers = true)]
        [JsonProperty("data")]
        public RequestVATFillingPeriod d { get; set; }
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
       
        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
       
        public class UIBTNSet
        {
            public List<object> results { get; set; }
        }
       
        public class QuesListSet
        {
            public List<object> results { get; set; }
        }
       
        public class RequestVATFillingPeriod
        {
            //public Metadata __metadata { get; set; }
            [JsonProperty("documentCategory")]
            public string Attchk { get; set; }
            [JsonProperty("currentPeriodKey")]
            public string CPersl { get; set; }
            [JsonProperty("formBundleNumber")]
            public string Fbnumz { get; set; }
            [JsonProperty("agree")]
            public string Iagrfg { get; set; }
            [JsonProperty("request")]
            public string Reqfg { get; set; }
            [JsonProperty("stepNumber")]
            public string StepNumber { get; set; }
            [JsonProperty("startDate")]
            public object Begda { get; set; }
            [JsonProperty("portalUser")]
            public string PortalUsrz { get; set; }
            [JsonProperty("language")]
            public string Langz { get; set; }
            [JsonProperty("transactionType")]
            public string TransType { get; set; }
            [JsonProperty("TIN")]
            public string Gpart { get; set; }
            [JsonProperty("operation")]
            public string Operationz { get; set; }
            [JsonProperty("formBundleType")]
            public string Fbtyp { get; set; }
            //[JsonProperty("")]
            //public string StepNumberz { get; set; }
            [JsonProperty("status")]
            public string Fbust { get; set; }
            //[JsonProperty("returnId")]
            //public string ReturnIdz { get; set; }
            [JsonProperty("userName")]
            public string Officerz { get; set; }
            [JsonProperty("userType")]
            public string UserTyp { get; set; }
            //[JsonProperty("TIN")]
            //public string Gpartz { get; set; }

            //public string TransactionType { get; set; }
            [JsonProperty("edit")]
            public string EditFg { get; set; }
            //[JsonProperty("status")]
            //public string Statusz { get; set; }
            [JsonProperty("authenticationUser")]
            public string Euser { get; set; }
            //[JsonProperty("userType")]
            public string UserTypz { get; set; }
            [JsonProperty("formBundleGUID")]
            public string Fbguid { get; set; }
            //[JsonProperty("transactionType")]
            public string TxnTpz { get; set; }
            //[JsonProperty("")]
            //public string DmodeFlg { get; set; }
            [JsonProperty("formProcess")]
            public string Formprocz { get; set; }
            //[JsonProperty("status")]
            //public string EvStatus { get; set; }
            //[JsonProperty("")]
            //public string OfficerTz { get; set; }
            [JsonProperty("sourceApplication")]
            public string SrcAppz { get; set; }
            [JsonProperty("systemCode")]
            public string Mandt { get; set; }
            [JsonProperty("formGUID")]
            public string FormGuid { get; set; }
            [JsonProperty("dataVersion")]
            public string DataVersion { get; set; }
            [JsonProperty("returnId")]
            public string ReturnId { get; set; }
            [JsonProperty("currentFrequency")]
            public string CureentF { get; set; }
            [JsonProperty("filingFrequency")]
            public string FilingF { get; set; }
            [JsonProperty("periodKey")]
            public string Persl { get; set; }
            [JsonProperty("declaration")]
            public string Decfg { get; set; }
            [JsonProperty("declarationName")]
            public string Decname { get; set; }
            [JsonProperty("declarationDesignation")]
            public string Decdesignation { get; set; }
            [JsonProperty("declarationDate")]
            public object Decdate { get; set; }
            [JsonProperty("declarationIdType")]
            public string DecidTy { get; set; }
            [JsonProperty("declarationIdNumber")]
            public string DecidNo { get; set; }
            [JsonProperty("effectiveDates")]
            public List<object> EffDateSet { get; set; }
            [JsonProperty("buttons")]
            public List<object> UI_BTNSet { get; set; }
            [JsonProperty("notes")]
            public List<NotesSetResult> NOTESSet { get; set; }
            [JsonProperty("attachments")]
            public List<Attachment> ATTACHSet { get; set; }
            [JsonProperty("attachmentsTypes")]
            public List<AttTypSetList> ATT_TYPSet { get; set; }
            [JsonProperty("questions")]
            public List<object> QuesListSet { get; set; }
        }


    }
    #endregion

   
    public class ATTTYPSet
    {
        public List<AttTypSetList> results { get; set; }
    }
   
    public partial class AttTypSetList
    {
        public Metadata __metadata { get; set; }
        [JsonProperty("documentCategory")]
        public string DmsTp { get; set; }
        [JsonProperty("name")]
        public string Txt50 { get; set; }
    }

   
    public partial class NotesSetResult
    {
        //[JsonProperty("noteNumber")]
        //public string Notenoz { get; set; }
        [JsonProperty("referenceName")]
        public string Refnamez { get; set; }
        [JsonProperty("displayOnAssessment")]
        public string XInvoicez { get; set; }
        [JsonProperty("completed")]
        public string XObsoletez { get; set; }
        [JsonProperty("processingReason")]
        public string Rcodez { get; set; }
        [JsonProperty("userName")]
        public string Erfusrz { get; set; }
        [JsonProperty("entryDate")]
        public string Erfdtz { get; set; }
        [JsonProperty("createdAt")]
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
        public string Namez { get; set; }
        [JsonProperty("noteNumber")]
        public string Noteno { get; set; }
        [JsonProperty("lineNumber")]
        public long Lineno { get; set; }
        [JsonProperty("elementNumber")]
        public long ElemNo { get; set; }
        [JsonProperty("notesFormat")]
        public string Tdformat { get; set; }
        [JsonProperty("textLine")]
        public string Tdline { get; set; }
        [JsonProperty("section")]
        public string Sect { get; set; }
        [JsonProperty("startDate")]
        public string Strdt { get; set; }
        [JsonProperty("startTime")]
        public string Strtime { get; set; }
        [JsonProperty("notesDescription")]
        public string Strline { get; set; }
    }
    #region VATChangeFillingPeriodRequestModel

    public class VATChangeFillingPeriodRequestModel
    {
       
        [JsonProperty("data")]
        public D d { get; set; }
        [JsonProperty("result")]
        public D d1 { get; set; }
        [Preserve(AllMembers = true)]
        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
       

        public class EffDateSet
        {
            public List<object> results { get; set; }
        }
       
        public class UIBTNSet
        {
            public List<object> results { get; set; }
        }
       
        public class NOTESSet
        {
            public List<NotesSetResult> results { get; set; }
            public Metadata Metadata { get; set; }
            [JsonProperty("noteNumber")]
            public string Notenoz { get; set; }
            [JsonProperty("referenceName")]
            public string Refnamez { get; set; }
            [JsonProperty("displayOnAssessment")]
            public string XInvoicez { get; set; }
            [JsonProperty("completed")]
            public string XObsoletez { get; set; }
            [JsonProperty("processingReason")]
            public string Rcodez { get; set; }
            [JsonProperty("userName")]
            public string Erfusrz { get; set; }
            [JsonProperty("entryDate")]
            public string Erfdtz { get; set; }
            [JsonProperty("createdAt")]
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
            public string Namez { get; set; }
            [JsonProperty("noteNumber")]
            public string Noteno { get; set; }
            [JsonProperty("lineNumber")]
            public long Lineno { get; set; }
            [JsonProperty("elementNumber")]
            public long ElemNo { get; set; }
            [JsonProperty("notesFormat")]
            public string Tdformat { get; set; }
            [JsonProperty("textLine")]
            public string Tdline { get; set; }
            [JsonProperty("section")]
            public string Sect { get; set; }
            [JsonProperty("startDate")]
            public string Strdt { get; set; }
            [JsonProperty("startTime")]
            public string Strtime { get; set; }
            [JsonProperty("notesDescription")]
            public string Strline { get; set; }
        }
       


        public class ATTACHSet
        {
            public List<Attachment> results { get; set; }
        }
       


        public class QuesListSet
        {
            public List<object> results { get; set; }
            //public string questionNumber { get; set; }
            //public string formBundleType { get; set; }
            //public string transactionType { get; set; }
            //public string processingReason { get; set; }
            //public string questionDescription { get; set; }
            //public string required { get; set; }
        }
       
        public class D
        {
            public Metadata __metadata { get; set; }
            [JsonProperty("documentCategory")]
            public string Attchk { get; set; }
            [JsonProperty("currentPeriodKey")]
            public string CPersl { get; set; }
            [JsonProperty("formBundleNumber")]
            public string Fbnumz { get; set; }
            [JsonProperty("agree")]
            public string Iagrfg { get; set; }
            [JsonProperty("request")]
            public string Reqfg { get; set; }
            [JsonProperty("stepNumber")]
            public string StepNumber { get; set; }
            [JsonProperty("startDate")]
            public object Begda { get; set; }
            [JsonProperty("portalUser")]
            public string PortalUsrz { get; set; }
            [JsonProperty("language")]
            public string Langz { get; set; }
            //[JsonProperty("transactionType")]
            //public string TransType { get; set; }
            [JsonProperty("TIN")]
            public string Gpart { get; set; }
            [JsonProperty("operation")]
            public string Operationz { get; set; }
            [JsonProperty("formBundleType")]
            public string Fbtyp { get; set; }
            //[JsonProperty("stepNumber")]
            //public string StepNumberz { get; set; }
            [JsonProperty("status")]
            public string Fbust { get; set; }
            public string ReturnIdz { get; set; }
            public string Officerz { get; set; }
            public string UserTyp { get; set; }
            //[JsonProperty("TIN")]
            //public string Gpartz { get; set; }
            [JsonProperty("transactionType")]
            public string TransactionType { get; set; }
            [JsonProperty("edit")]
            public string EditFg { get; set; }
            //[JsonProperty("status")]
            //public string Statusz { get; set; }
            public string Euser { get; set; }
            public string UserTypz { get; set; }
            [JsonProperty("formBundleGUID")]
            public string Fbguid { get; set; }
            //[JsonProperty("transactionType")]
            //public string TxnTpz { get; set; }
            [JsonProperty("mode")]
            public string DmodeFlg { get; set; }
            [JsonProperty("formProcess")]
            public string Formprocz { get; set; }
            //[JsonProperty("status")]
            //public string EvStatus { get; set; }
            [JsonProperty("userName")]
            public string OfficerTz { get; set; }
            [JsonProperty("sourceApplication")]
            public string SrcAppz { get; set; }
            public string Mandt { get; set; }
            [JsonProperty("formGUID")]
            public string FormGuid { get; set; }
            [JsonProperty("dataVersion")]
            public string DataVersion { get; set; }
            [JsonProperty("returnId")]
            public string ReturnId { get; set; }
            [JsonProperty("currentFrequency")]
            public string CureentF { get; set; }
            [JsonProperty("filingFrequency")]
            public string FilingF { get; set; }
            [JsonProperty("periodKey")]
            public string Persl { get; set; }
            [JsonProperty("declaration")]
            public string Decfg { get; set; }
            [JsonProperty("declarationName")]
            public string Decname { get; set; }
            [JsonProperty("declarationDesignation")]
            public string Decdesignation { get; set; }
            public object Decdate { get; set; }
            [JsonProperty("declarationIdType")]
            public string DecidTy { get; set; }
            [JsonProperty("declarationIdNumber")]
            public string DecidNo { get; set; }
            [JsonProperty("effectiveDates")]
            public List<object> EffDateSet { get; set; }
            [JsonProperty("buttons")]
            public List<object> UI_BTNSet { get; set; }
            [JsonProperty("notes")]
            public List<NotesSetResult> NOTESSet { get; set; }
            [JsonProperty("attachments")]
            public List<Attachment> ATTACHSet { get; set; }
            [JsonProperty("attachmentsTypes")]
            public List<AttTypSetList> ATT_TYPSet { get; set; }
            [JsonProperty("questions")]
            public List<object> QuesListSet { get; set; }
        }
    }


    #endregion
    #region VATRefillingDropdownModel

    public class VATRefillingDropdownModel
    {
       
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        [JsonProperty("data")]
        public D d { get; set; }
       
        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
       
        public class Metadata2
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
       
        public class Result
        {
            public Metadata2 __metadata { get; set; }
            [JsonProperty("systemCode")]
            public string Mandt { get; set; }
            [JsonProperty("formBundleType")]
            public string Fbtyp { get; set; }
            [JsonProperty("formBundleStatus")]
            public string Fbust { get; set; }
            [JsonProperty("button")]
            public string Button { get; set; }
            [JsonProperty("transactionType")]
            public string TransactionType { get; set; }
            [JsonProperty("userType")]
            public string UserTyp { get; set; }
        }
       
        public class UIBTNSet
        {
            public List<Result> results { get; set; }
        }
       
        public class Metadata3
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
       
        public class EffDate
        {
            public Metadata3 __metadata { get; set; }
            [JsonProperty("systemCode")]
            public string Mandt { get; set; }
            [JsonProperty("language")]
            public string Spras { get; set; }
            [JsonProperty("periodkey")]
            public string Persl { get; set; }
            [JsonProperty("periodDescription")]
            public string Txt50 { get; set; }
        }
       
        public class EffDateSet
        {
            public List<EffDate> results { get; set; }
        }
       
        public class Metadata4
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class AttypResult
        {
            public Metadata4 __metadata { get; set; }
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
            //[JsonProperty("formBundleType")]
            public object StartDt { get; set; }
            //[JsonProperty("formBundleType")]
            public object EndDt { get; set; }
            [JsonProperty("name")]
            public string Txt50 { get; set; }
        }
       
        public class ATTTYPSet
        {
            public List<AttypResult> results { get; set; }
        }
       
        public class D
        {
            // public Metadata __metadata { get; set; }
            //[JsonProperty("data")]
            //public string Mandtz { get; set; }
            [JsonProperty("formBundleType")]
            public string Fbtypz { get; set; }
            [JsonProperty("formBundleStatus")]
            public string Fbustz { get; set; }
            [JsonProperty("userType")]
            public string UserTypz { get; set; }
            //[JsonProperty("data")]
            //public string TransactionTypez { get; set; }
            [JsonProperty("edit")]
            public string EditFgz { get; set; }
            //[JsonProperty("data")]
            //public string Mandt { get; set; }
            [JsonProperty("formBundleNumber")]
            public string Fbnum { get; set; }
            [JsonProperty("portalUser")]
            public string PortalUsr { get; set; }
            [JsonProperty("language")]
            public string Lang { get; set; }
            [JsonProperty("operation")]
            public string Operation { get; set; }
            [JsonProperty("stepNumber")]
            public string StepNumber { get; set; }
            //[JsonProperty("data")]
            //public string Officer { get; set; }
            [JsonProperty("TIN")]
            public string Gpart { get; set; }
            [JsonProperty("statusCode")]
            public string Status { get; set; }
            //[JsonProperty("data")]
            //public string UserTyp { get; set; }
            //[JsonProperty("data")]
            //public string TxnTp { get; set; }
            [JsonProperty("formProcess ")]
            public string Formproc { get; set; }
            //[JsonProperty("data")]
            //public string OfficerT { get; set; }
            //[JsonProperty("data")]
            //public string SrcApp { get; set; }
            //[JsonProperty("data")]
            //public string DestCheck { get; set; }
            [JsonProperty("buttons")]
            public List<Result> UI_BTNSet { get; set; }
            [JsonProperty("effectiveDates")]
            public List<EffDate> EffDateSet { get; set; }
            [JsonProperty("attachments")]
            public List<AttypResult> ATT_TYPSet { get; set; }
        }

    }
    #endregion
    #region VATRefillingWorkItemsModel

    public class VATRefillingWorkItemsModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
       
        public D d { get; set; }
       
        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
       
        public class Metadata2
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
       
        public class Result
        {
            public Metadata2 __metadata { get; set; }
            public string Selector { get; set; }
            public string Fbnum { get; set; }
            public string Fbtyp { get; set; }
            public string Gpart { get; set; }
            public string NameLast { get; set; }
            public string NameFirst { get; set; }
            public string NameOrg1 { get; set; }
            public string NameOrg2 { get; set; }
            public string FullNm { get; set; }
            public string GpartFullNm { get; set; }
            public string WfSub { get; set; }
            public string Fbust { get; set; }
            public string FbustTxt { get; set; }
            public DateTime Receipt { get; set; }
            public string AssignUsr { get; set; }
            public string LoginUsr { get; set; }
            public string AssignMe { get; set; }
            public string NewUser { get; set; }
            public string TileInd { get; set; }
            public string FbtText { get; set; }
            public string TransactionType { get; set; }
            public string WiId { get; set; }
            public string WiPrio { get; set; }
            public string WiPrioDesc { get; set; }
        }
       
        public class ASSLISTSet
        {
            public List<Result> results { get; set; }
        }
       
        public class Metadata3
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
       
        public class Result2
        {
            public Metadata3 __metadata { get; set; }
            public string Mandt { get; set; }
            public string Stsma { get; set; }
            public string Estat { get; set; }
            public string Spras { get; set; }
            public string Txt04 { get; set; }
            public string Txt30 { get; set; }
            public bool Ltext { get; set; }
        }
       
        public class STATUSSet
        {
            public List<Result2> results { get; set; }
        }
       
        public class Metadata4
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
       
        public class Result3
        {
            public Metadata4 __metadata { get; set; }
            public string UserErrFg { get; set; }
            public string Fbtyp { get; set; }
            public string FbtText { get; set; }
            public string SysFlg { get; set; }
            public DateTime? Ldate { get; set; }
        }
       
        public class REQTYPSet
        {
            public List<Result3> results { get; set; }
        }
       
        public class D
        {
            public Metadata __metadata { get; set; }
            public string Filffreqalwd { get; set; }
            public string UserTin { get; set; }
            public string AudTin { get; set; }
            public object Begda { get; set; }
            public string TaxType { get; set; }
            public object Endda { get; set; }
            public string Euser { get; set; }
            public string Fbnum { get; set; }
            public string Fbsta { get; set; }
            public string Fbtyp { get; set; }
            public string Fbust { get; set; }
            public string Formproc { get; set; }
            public string Gpart { get; set; }
            public string Lang { get; set; }
            public string Mandt { get; set; }
            public string Officer { get; set; }
            public string Operation { get; set; }
            public string Persl { get; set; }
            public string PortalUsr { get; set; }
            public string ReturnId { get; set; }
            public string Status { get; set; }
            public string StepNumber { get; set; }
            public string TransactionType { get; set; }
            public string TxnTp { get; set; }
            public string UserTyp { get; set; }
            public ASSLISTSet ASSLISTSet { get; set; }
            public STATUSSet STATUSSet { get; set; }
            public REQTYPSet REQTYPSet { get; set; }
        }

    }


    #endregion

    #region ValidateIdNumber
   
    public class Metadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
   
    public class D
    {
        // public Metadata __metadata { get; set; }
        [JsonProperty("birthDate")]
        public DateTime Birthdt { get; set; }
        [JsonProperty("partnerKind")]
        public string Bpkind { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("idIssueingCountry")]
        public string IdIssueingCountry { get; set; }
        [JsonProperty("source")]
        public string Source { get; set; }
        [JsonProperty("taxpayerBirthDate")]
        public string TaxpDob { get; set; }
        [JsonProperty("passExpiryDate")]
        public string PassExpDt { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("fullName")]
        public string FullName { get; set; }
        [JsonProperty("floor")]
        public string Floor { get; set; }
        [JsonProperty("TIN")]
        public string Tin { get; set; }
        [JsonProperty("additionalNumber")]
        public string AdditionalNo { get; set; }
        [JsonProperty("houseNumber")]
        public string HouseNo { get; set; }
        [JsonProperty("idType")]
        public string Idtype { get; set; }
        // [JsonProperty("")]
        public string BirthdtC { get; set; }
        [JsonProperty("buildingNumber")]
        public string BuildingNo { get; set; }
        // [JsonProperty("")]
        public string Birthdt10 { get; set; }
        [JsonProperty("idNumber")]
        public string Idnum { get; set; }
        [JsonProperty("fatherName")]
        public string FatherName { get; set; }
        [JsonProperty("poBox")]
        public string PoBox { get; set; }
        [JsonProperty("grandfatherName")]
        public string GrandfatherName { get; set; }
        [JsonProperty("street1")]
        public string Street1 { get; set; }
        [JsonProperty("familyName")]
        public string FamilyName { get; set; }
        [JsonProperty("street2")]
        public string Street2 { get; set; }
        [JsonProperty("initials")]
        public string Initials { get; set; }
        [JsonProperty("province")]
        public string Province { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("quarter")]
        public string Quarter { get; set; }
        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }
        [JsonProperty("telephone")]
        public string Telephone { get; set; }
        [JsonProperty("faxNumber")]
        public string FaxNumber { get; set; }
        [JsonProperty("mobile")]
        public string Mobile { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("deafultCommunication")]
        public string DefltComm { get; set; }
        [JsonProperty("addressNumber")]
        public string Adrnr { get; set; }
        [JsonProperty("website")]
        public string Website { get; set; }
        [JsonProperty("authorizationGroup")]
        public string Augrp { get; set; }
        [JsonProperty("branchDescription")]
        public string BranchDesc { get; set; }
        [JsonProperty("name1")]
        public string Name1 { get; set; }
        [JsonProperty("name2")]
        public string Name2 { get; set; }
        [JsonProperty("partnerKindDescription")]
        public string BpkindDesc { get; set; }
        [JsonProperty("regionDescription")]
        public string RegionDesc { get; set; }
        [JsonProperty("taxpayerTitle")]
        public string TpTitle { get; set; }
        [JsonProperty("taxpayerFullName")]
        public string TpFullNm { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class ValidateIDResponse
    {

        [Preserve(AllMembers = true)]
        [JsonProperty("data")]
        public D d { get; set; }
        [JsonProperty("result")]
        public D data
        {
            set
            {
                d = value;
            }
        }
        [Preserve(AllMembers = true)]
        public string errorMessage { get; set; }
    }
    #endregion
}
