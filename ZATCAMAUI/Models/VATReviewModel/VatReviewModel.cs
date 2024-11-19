
using Newtonsoft.Json;

namespace ZATCAMAUI.Models.VATReviewModel
{
    public class VATObjectionButtonFormModeModel
    {

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

        public D d { get; set; }


     
        public class Metadata2
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
     
        public class Result
        {
            public Metadata2 __metadata { get; set; }
            public string Fbtyp { get; set; }
            public string Fbust { get; set; }
            public string Button { get; set; }
        }
     
        public class VRUIBTNSet
        {
            public List<Result> results { get; set; }
        }
     
        public class ELGBLDOCSet
        {
            public List<object> results { get; set; }
        }
     
        public class Metadata3
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }


     
        public class D
        {
            public Metadata __metadata { get; set; }
            public string Fbnum { get; set; }
            public string Lang { get; set; }
            public string Officer { get; set; }
            public string Gpart { get; set; }
            public string Status { get; set; }
            public string TxnTp { get; set; }
            public string Formproc { get; set; }
            public string Mandtz { get; set; }
            public string Fbtypz { get; set; }
            public string Fbustz { get; set; }
            public string EditFgz { get; set; }
            public string UserTypz { get; set; }
            public string Mandt { get; set; }
            public string PortalUsr { get; set; }
            public string Operation { get; set; }
            public string StepNumber { get; set; }
            public string ReturnId { get; set; }
            public string UserTyp { get; set; }
            public VRUIBTNSet VR_UI_BTNSet { get; set; }
            public ELGBLDOCSet ELGBL_DOCSet { get; set; }
            public ReasonSet1 ReasonSet { get; set; }
        }

    }

    
    public class SadadGenerationObject
    {
        [JsonProperty("periodKey")]
        public string periodKey { get; set; }

        [JsonProperty("securityAmount")]
        public decimal securityAmount { get; set; }

        [JsonProperty("flag")]
        public bool flag { get; set; }

        [JsonProperty("periodStartDate")]
        public String periodStartDate { get; set; }

        [JsonProperty("periodEndDate")]
        public String periodEndDate { get; set; }

        [JsonProperty("disputedAmount")]
        public decimal disputedAmount { get; set; }

        [JsonProperty("totalTaxLiability")]
        public decimal totalTaxLiability { get; set; }

        [JsonProperty("securityType")]
        public string securityType { get; set; }

        [JsonProperty("security")]
        public string security { get; set; }

        [JsonProperty("TIN")]
        public string TIN { get; set; }
    }


 
    public class VATObjectionEnableSubmitModel
    {
     
        public D d { get; set; }
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
     
        public class D
        {
            public Metadata __metadata { get; set; }
            public string Fbnumx { get; set; }
            public string Gpartx { get; set; }
            public string Statusx { get; set; }
            public string RvRsn { get; set; }
            public string RvSubRsn { get; set; }
            public string RejFb { get; set; }
            public bool EnableSubmit { get; set; }
        }

    }

 
    public class VATObjectionFormModel
    {
     
        public class VATReviewsReturnModel
        {
            public string TIN { get; set; }
            public string TaxPayerName { get; set; }
            public string License { get; set; }
            public string Address { get; set; }

            public List<Dictionary<string, string>> ReasonDropdown { get; set; }
            public List<ReviewReason> ListReviewReason { get; set; }
            public List<string> ReferenceNumber { get; set; }
            public string DecisionDate { get; set; }
            public string DecisionTaken { get; set; }
            public string AttachmentName { get; set; }
            public string TaxPeriodofCase { get; set; }
            public string PeriodFrom { get; set; }
            public string PeriodTo { get; set; }
            public string TotalTaxLiability { get; set; }
            public string TaxPaid { get; set; }
            public string RequestToReviewAmount { get; set; }
            public string ParticularAmount { get; set; }
            public List<object> Corrections { get; set; }
            public string SecurityAmount { get; set; }
            public string SADADNumber { get; set; }
            public string MethodSubmitSecurity { get; set; }
            public string ChkSecurityPayment { get; set; }
            public string ChkBankGuarantee { get; set; }
            public bool ChkInfoCorrect { get; set; }
            public bool ChkAuthorize { get; set; }
            public string IDType { get; set; }
            public string IDNo { get; set; }
            public string DOB { get; set; }
            public string ContactName { get; set; }
            public string NameOfTaxPayer { get; set; }
            public string ApplicationNo { get; set; }
            public string Date { get; set; }
        }
     
        public class ReviewReason
        {
            public string ProcCD { get; set; }
            public string Reasons { get; set; }

            public List<SubReason> ListSubReason { get; set; }
        }
     
        public class SubReason
        {
            public string Code { get; set; }
            public string SubReasons { get; set; }
        }
    }

 
    public class VATObjectionGenrateSadadModel
    {
        
        [JsonProperty("result")] public D d { get; set; }
        public class D
        {
            public Metadata __metadata { get; set; }
            //[JsonProperty("")]
            public object BnkValto { get; set; }
            [JsonProperty("periodKey")]
            public string Persl { get; set; }
            [JsonProperty("securityAmount")]
            public string Secamt { get; set; }
            [JsonProperty("flag")]
            public bool Flag { get; set; }
            [JsonProperty("formBundleNumber")]
            public string Fbnum { get; set; }
            [JsonProperty("formGuid")]
            public string FormGuid { get; set; }
            //[JsonProperty("")]
            public object BnkValfm { get; set; }
            [JsonProperty("dataVersion")]
            public string DataVersion { get; set; }
            [JsonProperty("periodStartDate")]
            public DateTime Abrzu { get; set; }
            [JsonProperty("periodEndDate")]
            public DateTime Abrzo { get; set; }
            [JsonProperty("disputedAmount")]
            public string Disamt { get; set; }
            [JsonProperty("totalTaxLiability")]
            public string Liaamt { get; set; }
            [JsonProperty("securityType")]
            public string Sectp { get; set; }
            [JsonProperty("bankGuarantyId")]
            public string Bnkid { get; set; }
            //[JsonProperty("")]
            public string Bkext { get; set; }
            [JsonProperty("sadadBillNumber")]
            public string Sopbel { get; set; }
            [JsonProperty("security")]
            public string Security { get; set; }
            [JsonProperty("cash")]
            public string ChkCash { get; set; }
            [JsonProperty("bank")]
            public string ChkBank { get; set; }
            //[JsonProperty("")]
            public string Perslt { get; set; }
            [JsonProperty("TIN")]
            public string Gpart { get; set; }

        }

    }
 
    public class Metadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

 
    public class VATObjectionListModel
    {
        
        [JsonProperty("data")]
        public D d { get; set; }

     
        public class Metadata2
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
     
        public class Result
        {
            // public Metadata2 __metadata { get; set; }
            [JsonProperty("formBundleTypeDescription")]
            public string FbtText { get; set; }
            [JsonProperty("formBundleType")]
            public string Fbtyp { get; set; }
            [JsonProperty("userError")]
            public string UserErrFg { get; set; }
            [JsonProperty("system")]
            public string SysFlg { get; set; }
            [JsonProperty("lastDate")]
            public string Ldate { get; set; }
        }
        
        public class REQTYPSet
        {
            [JsonProperty("requestTypeList")]
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
            // public Metadata3 __metadata { get; set; }
            [JsonProperty("systemCode")]
            public string Mandt { get; set; }
            [JsonProperty("statusProfile")]
            public string Stsma { get; set; }
            [JsonProperty("userStatus")]
            public string Estat { get; set; }
            [JsonProperty("language")]
            public string Spras { get; set; }
            [JsonProperty("statusId")]
            public string Txt04 { get; set; }
            [JsonProperty("statusDescription")]
            public string Txt30 { get; set; }
            [JsonProperty("longText")]
            public bool Ltext { get; set; }
        }
     
        public class STATUSSet
        {
            [JsonProperty("statusList")]
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
            // public Metadata4 __metadata { get; set; }
            [JsonProperty("selector")]
            public string Selector { get; set; }
            [JsonProperty("formBundleNumber")]
            public string Fbnum { get; set; }
            [JsonProperty("formBundleType")]
            public string Fbtyp { get; set; }
            [JsonProperty("TIN")]
            public string Gpart { get; set; }
            [JsonProperty("lastName")]
            public string NameLast { get; set; }
            [JsonProperty("firstName")]
            public string NameFirst { get; set; }
            [JsonProperty("organizationName1")]
            public string NameOrg1 { get; set; }
            [JsonProperty("organizationName2")]
            public string NameOrg2 { get; set; }
            [JsonProperty("fullName")]
            public string FullNm { get; set; }
            [JsonProperty("TINFullName")]
            public string GpartFullNm { get; set; }
            [JsonProperty("workflowSubreason")]
            public string WfSub { get; set; }
            [JsonProperty("userStatus")]
            public string Fbust { get; set; }
            [JsonProperty("formStatusDescription")]
            public string FbustTxt { get; set; }
            [JsonProperty("receiptDate")]
            public string Receipt { get; set; }
            [JsonProperty("assignedUser")]
            public string AssignUsr { get; set; }
            [JsonProperty("userName")]
            public string LoginUsr { get; set; }
            [JsonProperty("assignMe")]
            public string AssignMe { get; set; }
            [JsonProperty("newUserName")]
            public string NewUser { get; set; }
            [JsonProperty("tile")]
            public string TileInd { get; set; }
            [JsonProperty("formBundleTypeDescription")]
            public string FbtText { get; set; }
            [JsonProperty("transactionType")]
            public string TransactionType { get; set; }
            [JsonProperty("ID")]
            public string WiId { get; set; }
            [JsonProperty("priority")]
            public string WiPrio { get; set; }
            [JsonProperty("priorityDescription")]
            public string WiPrioDesc { get; set; }
        }
     
        public class ASSLISTSet
        {
            [JsonProperty("ASSList")]
            public List<Result3> results { get; set; }
        }
     
        public class D
        {
            // public Metadata __metadata { get; set; }
            [JsonProperty("userTIN")]
            public string UserTin { get; set; }
            [JsonProperty("auditorTIN")]
            public string AudTin { get; set; }

            //public object Begda { get; set; }
            [JsonProperty("taxType")]
            public string TaxType { get; set; }

            //public object Endda { get; set; }
            [JsonProperty("serialNumber")]
            public string Euser { get; set; }
            [JsonProperty("formBundleNumber")]
            public string Fbnum { get; set; }
            [JsonProperty("formBundleStatus")]
            public string Fbsta { get; set; }
            [JsonProperty("formBundleType")]
            public string Fbtyp { get; set; }
            [JsonProperty("userStatus")]
            public string Fbust { get; set; }
            [JsonProperty("formProcess")]
            public string Formproc { get; set; }
            [JsonProperty("TIN")]
            public string Gpart { get; set; }
            [JsonProperty("language")]
            public string Lang { get; set; }
            [JsonProperty("systemCode")]
            public string Mandt { get; set; }
            [JsonProperty("userName")]
            public string Officer { get; set; }
            [JsonProperty("operation")]
            public string Operation { get; set; }
            [JsonProperty("periodKey")]
            public string Persl { get; set; }
            [JsonProperty("portalUser")]
            public string PortalUsr { get; set; }
            [JsonProperty("returnId")]
            public string ReturnId { get; set; }
            [JsonProperty("statusCode")]
            public string Status { get; set; }
            [JsonProperty("stepNumber")]
            public string StepNumber { get; set; }
            [JsonProperty("transactionType")]
            public string TransactionType { get; set; }
            //[JsonProperty("")]
            //public string TxnTp { get; set; }
            [JsonProperty("userType")]
            public string UserTyp { get; set; }
            [JsonProperty("requestTypeList")]
            public List<Result> REQTYPSet { get; set; }
            [JsonProperty("statusList")]
            public List<Result2> STATUSSet { get; set; }
            [JsonProperty("ASSList")]
            public List<Result3> ASSLISTSet { get; set; }
        }

    }

 
    public class VATObjectionRejectedFormModel
    {
        
        [JsonProperty("data")]
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public D d { get; set; }

     
        public class Metadata2
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
     
        public class AppRefNumResult
        {
            // public Metadata2 __metadata { get; set; }
            [JsonProperty("periodStartDate")]
            public string Abrzu { get; set; }
            [JsonProperty("message")]
            public string Msgflg { get; set; }
            [JsonProperty("penalityType")]
            public string Pentyp { get; set; }
            [JsonProperty("overdue")]
            public string OVERDUEFG { get; set; }
            [JsonProperty("correspondenceKey")]
            public string Cokey { get; set; }
            [JsonProperty("messageDescription")]
            public string Msgtxt { get; set; }
            [JsonProperty("documentNumber")]
            public string Opbel { get; set; }
            [JsonProperty("penalityAmount")]
            public string Penamount { get; set; }
            //  [JsonProperty("")]
            public string CaseRef { get; set; }
            [JsonProperty("days")]
            public int Zdays { get; set; }
            [JsonProperty("clearedAmount")]
            public string Clramt { get; set; }
            [JsonProperty("caseGUID")]
            public string CaseGuid { get; set; }
            [JsonProperty("periodicity")]
            public string Periodicity { get; set; }
            [JsonProperty("periodEndDate")]
            public string Abrzo { get; set; }
            [JsonProperty("dateFrom")]
            public string DateFrm { get; set; }
            [JsonProperty("dateTo")]
            public string DateTo { get; set; }
            [JsonProperty("periodKey")]
            public string Persl { get; set; }
            [JsonProperty("formBundleNumber")]
            public string Fbnum { get; set; }
            [JsonProperty("liabilityAmount")]
            public string Liaamt { get; set; }
            [JsonProperty("isDelete")]
            public bool Del { get; set; }
            [JsonProperty("formBundleType")]
            public string Fbtyp { get; set; }
            [JsonProperty("formBundleStatus")]
            public string Fbsta { get; set; }
            [JsonProperty("periodDescription")]
            public string Perslt { get; set; }
            [JsonProperty("userStatus")]
            public string Fbust { get; set; }
            [JsonProperty("changedBy")]
            public string Aenam { get; set; }
            [JsonProperty("transactionType")]
            public string TrnTyp { get; set; }
            [JsonProperty("declarationDate")]
            public string DecDt { get; set; }
            [JsonProperty("lastFulfilledDate")]
            public string LastFulfilledDt { get; set; }
            public string allowedDays { get; set; }
            //[JsonProperty("periodStartDate")]
            //public string periodStartDate { get; set; }
            //[JsonProperty("periodEndDate")]
            //public string periodEndDate { get; set; }
            [JsonProperty("securityAmount")]
            public string Secamt { get; set; }
            [JsonProperty("penaltyPaidAmount")]
            public string PenPaidAmt { get; set; }
            [JsonProperty("messageFlag")]
            public string Bgmsgflg { get; set; }
            [JsonProperty("messageText")]
            public string Bgmsgtxt { get; set; }
            [JsonProperty("unpaidAmount")]
            public string Unpaidamt { get; set; }
            [JsonProperty("unpaid")]
            public string Unpayfg { get; set; }

        }


        public class RejectedFormSet
        {
            [JsonProperty("rejectedFormList")]
            public List<AppRefNumResult> results { get; set; }
        }
     
        public class D
        {
            public Metadata __metadata { get; set; }
            [JsonProperty("formBundleNumber")]
            public string Fbnumx { get; set; }
            [JsonProperty("sadadBillNumber")]
            public string Sopbel { get; set; }
            [JsonProperty("language")]
            public string Langx { get; set; }
            [JsonProperty("TIN")]
            public string Gpartx { get; set; }
            [JsonProperty("userStatus")]
            public string Fbustx { get; set; }
            [JsonProperty("formBundleStatus")]
            public string Fbstax { get; set; }
            [JsonProperty("userType")]
            public string UserTypx { get; set; }
            [JsonProperty("transactionType")]
            public string TxnTpx { get; set; }
            [JsonProperty("formProcess")]
            public string Formprocx { get; set; }
            [JsonProperty("reviewReason")]
            public string RvRsn { get; set; }
            [JsonProperty("reviewSubReason")]
            public string RvSubRsn { get; set; }
            [JsonProperty("rejectedFormList")]
            public List<AppRefNumResult> RejectedFormSet { get; set; }
        }
    }

 
    public class VATObjectionSecurityAmountModel
    {
        
        [JsonProperty("data")]
        public D d { get; set; }
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
     
        public class D
        {
            // public Metadata __metadata { get; set; }
            [JsonProperty("disputedAmount")]
            public string Disamt { get; set; }
            [JsonProperty("clearedAmount")]
            public string Clramt { get; set; }
            [JsonProperty("amountType")]
            public string Amttp { get; set; }
            [JsonProperty("totalTaxLiability")]
            public string Liaamt { get; set; }
            [JsonProperty("securityAmount")]
            public string Secamt { get; set; }
        }
    }

 
    public class ReasonSetResult
    {

        [JsonProperty("penaltyType")]
        public string Pentyp { get; set; }
        [JsonProperty("processCode")]
        public string ProcCd { get; set; }
        [JsonProperty("formBundleType")]
        public string Fbtyp { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("formBundleTypeDescription")]
        public string FbtText { get; set; }
        [JsonProperty("reviewReasonTypeDescription")]
        public string TypeT { get; set; }
        [JsonProperty("transactionType")]
        public string TrnTyp { get; set; }
        [JsonProperty("subReasonTypeDescription")]
        public string SubtypT { get; set; }
        [JsonProperty("transactionDescription")]
        public string TrnTxt { get; set; }
        [JsonProperty("fromDate")]
        public string DtFrmFlg { get; set; }
        [JsonProperty("toDate")]
        public string DtToFlg { get; set; }
    }
 
    public class ReasonSet1
    {
        [JsonProperty("reasons")]
        public List<ReasonSetResult> results { get; set; }
    }
 
    public class AddressSet1
    {
        [JsonProperty("addresses")]
        public List<AddressResults> results { get; set; }
    }
 
    public class AddressResults
    {
        //public Metadata3 __metadata { get; set; }
        [JsonProperty("addressNumber")]
        public string Addrnumber { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("quarter")]
        public string Quarter { get; set; }
        [JsonProperty("postalCode")]
        public string PostalCd { get; set; }
        [JsonProperty("street")]
        public string Street { get; set; }
        [JsonProperty("additionalNumber")]
        public string AdditionalNo { get; set; }
        [JsonProperty("buildingNumber")]
        public string BuildingNo { get; set; }
        [JsonProperty("region")]
        public string Region { get; set; }
        [JsonProperty("regionDescription")]
        public string RegionDesc { get; set; }
    }
 
    public class QuesListSet1
    {
        [JsonProperty("questions")]
        public List<object> results { get; set; }
    }
 
    public class AttdetSet1
    {
        [JsonProperty("attachments")]
        public List<Attachment> results { get; set; }
    }
 
    public class IdDetailSet
    {
        [JsonProperty("idDetails")]
        public List<object> results { get; set; }
    }
 
    public class NotesSet
    {
        public List<NotesSetResults> results { get; set; }
    }
 
    public class NotesSetGet
    {
        [JsonProperty("notes")]
        public List<NotesSetResultsGet> results { get; set; }
    }
 
    public partial class NotesSetResultsGet
    {
        // public Metadata __metadata { get; set; }
        [JsonProperty("noteNumber")]
        public string Notenoz { get; set; }
        [JsonProperty("referenceName")]
        public string Refnamez { get; set; }
        [JsonProperty("dataVersion")]
        public string DataVersionz { get; set; }
        [JsonProperty("displayOnAssessment")]
        public string XInvoicez { get; set; }
        [JsonProperty("completed")]
        public string XObsoletez { get; set; }
        [JsonProperty("processingReason")]
        public string Rcodez { get; set; }
        [JsonProperty("userName")]
        public string Erfusrz { get; set; }
        [JsonProperty("createdAt")]
        public object Erfdtz { get; set; }
        // [JsonProperty("")]
        public object Erftmz { get; set; }
        [JsonProperty("TIN")]
        public string ByGpartz { get; set; }
        [JsonProperty("attachedByPerson")]
        public string AttByz { get; set; }
        // [JsonProperty("")]
        public string Noteno { get; set; }
        [JsonProperty("lineNumber")]
        public long Lineno { get; set; }
        [JsonProperty("elementNumber")]
        public long ElemNo { get; set; }
        [JsonProperty("tagColumn")]
        public string Tdformat { get; set; }
        [JsonProperty("textLine")]
        public string Tdline { get; set; }
        [JsonProperty("startLine")]
        public string Strline { get; set; }
        [JsonProperty("startTime")]
        public string startTime { get; set; }
        [JsonProperty("startDate")]
        public string startDate { get; set; }
        public string section { get; set; }
        public string name { get; set; }

    }
 
    public partial class NotesSetResults
    {
        public Metadata __metadata { get; set; }
        // [JsonProperty("noteNumber")]
        public string Notenoz { get; set; }
        [JsonProperty("referenceName")]
        public string Refnamez { get; set; }
        [JsonProperty("dataVersion")]
        public string DataVersionz { get; set; }
        [JsonProperty("displayOnAssessment")]
        public string XInvoicez { get; set; }
        [JsonProperty("completed")]
        public string XObsoletez { get; set; }
        [JsonProperty("processingReason")]
        public string Rcodez { get; set; }
        [JsonProperty("userName")]
        public string Erfusrz { get; set; }
        [JsonProperty("entryDate")]
        public object Erfdtz { get; set; }
        [JsonProperty("createdAt")]
        public object Erftmz { get; set; }
        [JsonProperty("TIN")]
        public string ByGpartz { get; set; }
        [JsonProperty("attachedByPerson")]
        public string AttByz { get; set; }
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

    }
    
    public partial class SecurityDtl
    {
        //public Metadata __metadata { get; set; }

        public string formBundleNumber { get; set; }
        [JsonProperty("reviewDecision")]
        public string Rvdsc { get; set; }
        [JsonProperty("amountType")]
        public string Amttp { get; set; }
        [JsonProperty("penaltyAmountL")]
        public string PenamtI { get; set; }
        [JsonProperty("securityZero")]
        public string SeczeroFlg { get; set; }
        [JsonProperty("penaltyAmountR")]
        public string PenamtR { get; set; }
        [JsonProperty("documentNumber")]
        public string Opbel { get; set; }
        [JsonProperty("penaltyAmount")]
        public string Penamount { get; set; }
        [JsonProperty("caseRefrence")]
        public string CaseRef { get; set; }
        [JsonProperty("penaltyType")]
        public string Pentp { get; set; }
        [JsonProperty("correctionPenalty")]
        public string Crpnl { get; set; }
        [JsonProperty("penaltyAmountByOfficer")]
        public string Penamt { get; set; }
        [JsonProperty("lateFillingPenalty")]
        public string Lfpnl { get; set; }
        [JsonProperty("VATDueAmount")]
        public string Vatamt { get; set; }
        [JsonProperty("clearedAmount")]
        public string Clramt { get; set; }
        [JsonProperty("totalTaxDue")]
        public string Dueamt { get; set; }
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [JsonProperty("securityAmount")]
        public string Secamt { get; set; }
        [JsonProperty("TIN")]
        public string Gpart { get; set; }
        [JsonProperty("security")]
        public string Security { get; set; }
        [JsonProperty("cashCheckBox")]
        public string ChkCash { get; set; }
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [JsonProperty("periodStartDate")]
        public string Abrzu { get; set; }
        [JsonProperty("bankCheckBox")]
        public string ChkBank { get; set; }
        [JsonProperty("periodEndDate")]
        public string Abrzo { get; set; }
        [JsonProperty("disputedAmount")]
        public string Disamt { get; set; }
        [JsonProperty("totalTaxLiability")]
        public string Liaamt { get; set; }
        [JsonProperty("securityType")]
        public string Sectp { get; set; }
        [JsonProperty("bankGuaranteeId")]
        public string Bnkid { get; set; }
        [JsonProperty("bankValidityToDate")]
        public object BnkValto { get; set; }
        [JsonProperty("bankValidityFromDate")]
        public object BnkValfm { get; set; }
        [JsonProperty("externalBankId")]
        public string Bkext { get; set; }
        [JsonProperty("sadadBillNumber")]
        public string Sopbel { get; set; }
        [JsonProperty("periodDescription")]
        public string Perslt { get; set; }
        [JsonProperty("unpaidAmount")]
        public string Unpaidamt { get; set; }
        [JsonProperty("unpaid")]
        public string Unpayfg { get; set; }
    }

    public class MainReasonSetResults
    {
        public Metadata4 __metadata { get; set; }
        [JsonProperty("formBundleType")]
        public string Fbtyp { get; set; }
        [JsonProperty("penaltyType")]
        public string Pentyp { get; set; }
        [JsonProperty("formBundleTypeDescription")]
        public string FbtText { get; set; }
        [JsonProperty("transactionType")]
        public string TrnTyp { get; set; }
        [JsonProperty("transactionDescription")]
        public string TrnTxt { get; set; }
        [JsonProperty("processCode")]
        public string ProcCd { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("reviewReasonTypeDescription")]
        public string TypeT { get; set; }
        [JsonProperty("subReasonTypeDescription")]
        public string SubtypT { get; set; }
        [JsonProperty("fromDate")]
        public string DtFrmFlg { get; set; }
        [JsonProperty("toDate")]
        public string DtToFlg { get; set; }
    }
 
    public class MainReasonSet
    {
        [JsonProperty("mainReasons")]
        public List<MainReasonSetResults> results { get; set; }
    }

    public class VATObjectionSummaryModel
    {
        
        [JsonProperty("data")]

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public D d { get; set; }

        [JsonProperty("result")]
        public D result { get; set; }

        
        public class Metadata2
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }

     

        public class ReasonSet
        {
            public List<ReasonSetResult> results { get; set; }
        }
     
        public class Metadata3
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }




     
        public class Metadata4
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }


     
        public class Metadata5
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }

     

        public class D
        {
            public Metadata __metadata { get; set; }
            [JsonProperty("skipCovid19")]
            public string Skipst5covid19 { get; set; }
            [JsonProperty("activityName")]
            public string Actnm { get; set; }
            public string allowedDays { get; set; }
            [JsonProperty("goLive")]
            public string Golivefg { get; set; }
            [JsonProperty("declarationDate")]
            public string DecDt { get; set; }
            [JsonProperty("overDue")]
            public string OVERDUEFG { get; set; }
            [JsonProperty("application")]
            public string Appfg { get; set; }
            [JsonProperty("periodKey")]
            public string Persl { get; set; }
            [JsonProperty("activityNumber")]
            public string Actno { get; set; }
            [JsonProperty("isAgree")]
            public bool AgreeFg { get; set; }
            // [JsonProperty("")]
            public string Branchx { get; set; }
            [JsonProperty("calendarType")]
            public string CalTyp { get; set; }
            [JsonProperty("CRNumber")]
            public string CrNo { get; set; }
            public string CR385 { get; set; }
            public string CR1317GoLive { get; set; }
            [JsonProperty("dataVersion")]
            public string DataVersion { get; set; }
            [JsonProperty("fromDate")]
            public string DateFrm { get; set; }
            [JsonProperty("oldFromDate")]
            public string DateFrmOld { get; set; }
            [JsonProperty("toDate")]
            public string DateTo { get; set; }
            [JsonProperty("oldToDate")]
            public string DateToOld { get; set; }
            //  [JsonProperty("")]
            public string PeriodKey { get; set; }
            [JsonProperty("isDeclaration1")]
            public bool DecFlg1 { get; set; }
            [JsonProperty("isDeclaration2")]
            public bool DecFlg2 { get; set; }
            [JsonProperty("declarationIdNumber")]
            public string DecIdNo { get; set; }
            //[JsonProperty("")]
            public string Declarationdt { get; set; }
            [JsonProperty("declarationName")]
            public string Decnm { get; set; }
            [JsonProperty("serialNumber")]
            public string Euserx { get; set; }
            // [JsonProperty("")]
            public string Evstatus { get; set; }
            [JsonProperty("formBundleNumber")]
            public string Fbnumx { get; set; }
            [JsonProperty("formBundleStatus")]
            public string Fbstax { get; set; }
            [JsonProperty("formBundleStatusText")]
            public string Fbustx { get; set; }
            //[JsonProperty("")]
            public string FormGuid { get; set; }
            [JsonProperty("formProcess")]
            public string Formprocx { get; set; }
            // [JsonProperty("")]
            public string Forwardx { get; set; }
            [JsonProperty("fullName")]
            public string FullName { get; set; }
            [JsonProperty("TIN")]
            public string Gpartx { get; set; }
            [JsonProperty("IBAN")]
            public string Iban { get; set; }
            [JsonProperty("idType")]
            public string IdType { get; set; }
            public string channel { get; set; }
            [JsonProperty("language")]
            public string Langx { get; set; }
            //[JsonProperty("")]
            public string Officerx { get; set; }
            [JsonProperty("operation")]
            public string Operationx { get; set; }
            [JsonProperty("portalUser")]
            public string PortalUsrx { get; set; }
            [JsonProperty("rejectFormBundleNumber")]
            public string RejFb { get; set; }
            [JsonProperty("returnId")]
            public string ReturnIdx { get; set; }
            [JsonProperty("reviewReason")]
            public string RvRsn { get; set; }
            [JsonProperty("reviewSubReason")]
            public string RvSubRsn { get; set; }
            //  [JsonProperty("")]
            public string Srcidentifyx { get; set; }
            [JsonProperty("statusCode")]
            public string Statusx { get; set; }
            [JsonProperty("stepNumber")]
            public string StepNumberx { get; set; }
            // [JsonProperty("")]
            public string TxnTpx { get; set; }
            //public string transactionType { get; set; }
            // [JsonProperty("")]
            public string UserTypx { get; set; }
            [JsonProperty("CR6490Flag")]
            public string Cr6490Fg { get; set; }

            [JsonProperty("reasons")]
            public List<ReasonSetResult> ReasonSet { get; set; }
            [JsonProperty("addresses")]
            public List<AddressResults> AddressSet { get; set; }
            [JsonProperty("notes")]
            public List<NotesSetResultsGet> NotesSet { get; set; }
            [JsonProperty("questions")]
            public List<object> QuesListSet { get; set; }
            [JsonProperty("attachments")]
            public List<Attachment> AttdetSet { get; set; }
            [JsonProperty("idDetails")]
            public List<object> IdDetailSet { get; set; }
            [JsonProperty("mainReasons")]
            public List<MainReasonSetResults> MainReasonSet { get; set; }
            [JsonProperty("securityDetails")]
            public SecurityDtl SecurityDtl { get; set; }

            [JsonProperty("securityDetail")]
            public SecurityDtl SecurityDetails { set { SecurityDtl = value; } }

        }

    }

    
    public class VATObjectionValidateTaxpayerModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
     
        public D d { get; set; }

     
        public class D
        {
            public Metadata __metadata { get; set; }
            public object Birthdt { get; set; }
            public string Bpkind { get; set; }
            public string Country { get; set; }
            public string IdIssueingCountry { get; set; }
            public string Source { get; set; }
            public string TaxpDob { get; set; }
            public string PassExpDt { get; set; }
            public string Title { get; set; }
            public string FullName { get; set; }
            public string Floor { get; set; }
            public string Tin { get; set; }
            public string AdditionalNo { get; set; }
            public string HouseNo { get; set; }
            public string Idtype { get; set; }
            public string BirthdtC { get; set; }
            public string BuildingNo { get; set; }
            public string Birthdt10 { get; set; }
            public string Idnum { get; set; }
            public string FatherName { get; set; }
            public string PoBox { get; set; }
            public string GrandfatherName { get; set; }
            public string Street1 { get; set; }
            public string FamilyName { get; set; }
            public string Street2 { get; set; }
            public string Initials { get; set; }
            public string Province { get; set; }
            public string City { get; set; }
            public string Quarter { get; set; }
            public string PostalCode { get; set; }
            public string Telephone { get; set; }
            public string FaxNumber { get; set; }
            public string Mobile { get; set; }
            public string Email { get; set; }
            public string DefltComm { get; set; }
            public string Adrnr { get; set; }
            public string Website { get; set; }
            public string Augrp { get; set; }
            public string BranchDesc { get; set; }
            public string Name1 { get; set; }
            public string Name2 { get; set; }
            public string BpkindDesc { get; set; }
            public string RegionDesc { get; set; }
        }

    }

 
    public class VATObjectionViewbillModel
    {
         // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        [JsonProperty("data")]
        public List<ResultBill> d { get; set; }

        
        public class ResultBill
        {
            // public Metadata __metadata { get; set; }
            [JsonProperty("periodDescription")]
            public string Perslt { get; set; }
            [JsonProperty("documentNumber")]
            public string Opbel { get; set; }
            [JsonProperty("sadadNumber")]
            public string Vtre2 { get; set; }
            [JsonProperty("mainTransaction")]
            public string Hvorg { get; set; }
            [JsonProperty("subTransaction")]
            public string Tvorg { get; set; }
            [JsonProperty("deferralToDate")]
            public string Bldat { get; set; }
            [JsonProperty("documentDate")]
            public string Studt { get; set; }
            [JsonProperty("amount")]
            public string Betrh { get; set; }
            [JsonProperty("periodKey")]
            public string Persl { get; set; }
            [JsonProperty("periodEndDate")]
            public string Abrzu { get; set; }
            [JsonProperty("periodStartDate")]
            public string Abrzo { get; set; }
            [JsonProperty("subReasonTypeDescription")]
            public string Desc { get; set; }
        }
     
        public class D
        {
            public List<ResultBill> results { get; set; }
        }
    }
 
    public class VATObjectionFormViewBillModel
    {
        public string DocumentNumber { get; set; }
        public string SadadNumber { get; set; }
        public string DateofPenality { get; set; }
        public string DescriptionOfPenality { get; set; }
        public string Periodkey { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string DueDate { get; set; }
        public string Amount { get; set; }


    }
 
    public partial class VatObjectionsRequest
    {
        public Metadata __metadata { get; set; }
        [JsonProperty("skipCovid19")]
        public string Skipst5covid19 { get; set; }
        [JsonProperty("activityName")]
        public string Actnm { get; set; }
        [JsonProperty("goLive")]
        public string Golivefg { get; set; }
        [JsonProperty("declarationDate")]
        public string DecDt { get; set; }
        [JsonProperty("application")]
        public string Appfg { get; set; }
        [JsonProperty("periodKey")]
        public string Persl { get; set; }
        [JsonProperty("activityNumber")]
        public string Actno { get; set; }
        [JsonProperty("isAgree")]
        public bool AgreeFg { get; set; }
        [JsonProperty("authorizationGroup")]
        public string Branchx { get; set; }
        [JsonProperty("calendarType")]
        public string CalTyp { get; set; }
        [JsonProperty("CRNumber")]
        public string CrNo { get; set; }
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [JsonProperty("fromDate")]
        public string DateFrm { get; set; }
        [JsonProperty("oldFromDate")]
        public string DateFrmOld { get; set; }
        [JsonProperty("toDate")]
        public string DateTo { get; set; }
        [JsonProperty("oldToDate")]
        public string DateToOld { get; set; }
        [JsonProperty("periodDescription")]
        public string PeriodKey { get; set; }
        [JsonProperty("isDeclaration1")]
        public bool DecFlg1 { get; set; }
        [JsonProperty("overDue")]
        public string OVERDUEFG { get; set; }
        [JsonProperty("isDeclaration2")]
        public bool DecFlg2 { get; set; }
        [JsonProperty("declarationIdNumber")]
        public string DecIdNo { get; set; }
        //[JsonProperty("declarationDate")]
        // public string Declarationdt { get; set; }
        [JsonProperty("declarationName")]
        public string Decnm { get; set; }
        [JsonProperty("serialNumber")]
        public string Euserx { get; set; }
        [JsonProperty("status")]
        public string Evstatus { get; set; }
        [JsonProperty("formBundleNumber")]
        public string Fbnumx { get; set; }
        [JsonProperty("formBundleStatus")]
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
        public string Gpartx { get; set; }
        [JsonProperty("IBAN")]
        public string Iban { get; set; }
        [JsonProperty("idType")]
        public string IdType { get; set; }
        [JsonProperty("language")]
        public string Langx { get; set; }
        [JsonProperty("userName")]
        public string Officerx { get; set; }
        [JsonProperty("operation")]
        public string Operationx { get; set; }
        [JsonProperty("portalUser")]
        public string PortalUsrx { get; set; }
        [JsonProperty("rejectFormBundleNumber")]
        public string RejFb { get; set; }
        [JsonProperty("returnId")]
        public string ReturnIdx { get; set; }
        [JsonProperty("reviewReason")]
        public string RvRsn { get; set; }
        [JsonProperty("reviewSubReason")]
        public string RvSubRsn { get; set; }
        [JsonProperty("sourceIdentifier")]
        public string Srcidentifyx { get; set; }
        [JsonProperty("statusCode")]
        public string Statusx { get; set; }
        [JsonProperty("stepNumber")]
        public string StepNumberx { get; set; }
        [JsonProperty("transactionType")]
        public string TxnTpx { get; set; }
        [JsonProperty("userType")]
        public string UserTypx { get; set; }
        [JsonProperty("reasons")]
        public List<ReasonSetResult> ReasonSet { get; set; }
        [JsonProperty("addresses")]
        public List<AddressResults> AddressSet { get; set; }
        [JsonProperty("notes")]
        public List<NotesSetResults> NotesSet { get; set; }
        [JsonProperty("questions")]
        public List<object> QuesListSet { get; set; }
        [JsonProperty("attachments")]
        public List<Attachment> AttdetSet { get; set; }
        [JsonProperty("ids")]
        public List<object> IdDetailSet { get; set; }
        [JsonProperty("mainReasons")]
        public List<MainReasonSetResults> MainReasonSet { get; set; }
        [JsonProperty("securityDetail")]
        public SecurityDtl SecurityDtl { get; set; }
    }


 
    public class VATObjectionSummaryInputModel
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
     
        public class D
        {
            public Metadata __metadata { get; set; }
            //[JsonProperty("")]
            public object Abrzo { get; set; }
            //[JsonProperty("")]
            public object Abrzu { get; set; }
            //[JsonProperty("")]
            public object Aedat { get; set; }
            //[JsonProperty("")]
            public bool AudFlag { get; set; }
            //[JsonProperty("")]
            public string Auditor { get; set; }
            //[JsonProperty("")]
            public object Begda { get; set; }
            //[JsonProperty("")]
            public string Branch { get; set; }
            //[JsonProperty("")]
            public string CalendrTyp { get; set; }
            //[JsonProperty("")]
            public string Comb { get; set; }
            //[JsonProperty("")]
            public string Dispflag { get; set; }
            //[JsonProperty("")]
            public string Due { get; set; }
            //[JsonProperty("")]
            public object DueDt { get; set; }
            //[JsonProperty("")]
            public string DueDtC { get; set; }
            //[JsonProperty("")]
            public object Endda { get; set; }
            [JsonProperty("authenticationUser")]
            public string Euser { get; set; }
            [JsonProperty("authenticationUser1")]
            public string Euser1 { get; set; }
            [JsonProperty("authenticationUser2")]
            public string Euser2 { get; set; }
            [JsonProperty("authenticationUser3")]
            public string Euser3 { get; set; }
            [JsonProperty("authenticationUser4")]
            public string Euser4 { get; set; }
            [JsonProperty("authenticationUser5")]
            public string Euser5 { get; set; }
            [JsonProperty("formBundleGUID")]
            public string Fbguid { get; set; }
            [JsonProperty("formBundleNumber")]
            public string Fbnum { get; set; }
            [JsonProperty("formBundleTypeDescription")]
            public string FbtText { get; set; }
            [JsonProperty("formBundleType")]
            public string Fbtyp { get; set; }
            //[JsonProperty("")]
            public string Flag { get; set; }
            [JsonProperty("TIN")]
            public string Gpart { get; set; }
            [JsonProperty("inboundCorrespondenceDescription")]
            public string Incotext { get; set; }
            [JsonProperty("inboundCorrespondenceType")]
            public string Incotyp { get; set; }
            [JsonProperty("informationMessage")]
            public string InfoMsg { get; set; }
            [JsonProperty("language")]
            public string Lang { get; set; }
            [JsonProperty("month")]
            public string Monthz { get; set; }
            // [JsonProperty("")]
            public string Msg { get; set; }
            [JsonProperty("isObjectionFiled")]
            public bool ObjFiled { get; set; }
            [JsonProperty("obligation")]
            public string ObligFlag { get; set; }
            [JsonProperty("isOpen")]
            public bool Open { get; set; }
            [JsonProperty("period")]
            public string Period { get; set; }
            [JsonProperty("periodkey")]
            public string Persl { get; set; }
            [JsonProperty("refundFiled")]
            public bool RefundFiled { get; set; }
            [JsonProperty("sadadBillNumber1")]
            public string SadadDoc1 { get; set; }
            [JsonProperty("sadadBillNumber2")]
            public string SadadDoc2 { get; set; }
            // [JsonProperty("")]
            public string Stat { get; set; }
            // [JsonProperty("")]
            public string Statflag { get; set; }
            //[JsonProperty("")]
            public string Status { get; set; }
            [JsonProperty("taxPeriod")]
            public string TaxPeriod { get; set; }
            [JsonProperty("contractNumber")]
            public string Vtref { get; set; }
        }

     
        public class VATReviewRequestTPFVModel
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
                public DateTime Erfdt { get; set; }
                public string Erftm { get; set; }
                public string DataVersion { get; set; }
                public string DocUrl { get; set; }
                public string OutletRef { get; set; }
                public string Enbedit { get; set; }
                public string Enbdele { get; set; }
                public string Visedit { get; set; }
                public string Visdel { get; set; }
            }
         
            public class AttdetSet
            {
                public List<Attachment> results { get; set; }
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
                public DateTime Erfdtz { get; set; }
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
                //[JsonProperty("")]
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
                public string Sect { get; set; }
                [JsonProperty("startDate")]
                public string Strdt { get; set; }
                //[JsonProperty("")]
                public string Strtime { get; set; }
                [JsonProperty("notesDescritption")]
                public string Strline { get; set; }
            }
         
            public class NotesSet
            {
                public List<Result2> results { get; set; }
            }
         
            public class QUESLISTSet
            {
                public List<object> results { get; set; }
            }
         
            public class QUESTIONSSet
            {
                public List<object> results { get; set; }
            }
         
            public class D
            {
                public Metadata __metadata { get; set; }
                [JsonProperty("calendarType")]
                public string Caltp { get; set; }
                [JsonProperty("isReview")]
                public bool ReviewFg { get; set; }
                [JsonProperty("remark")]
                public string Remark { get; set; }
                [JsonProperty("formBundleGUID")]
                public string Fbguid { get; set; }
                [JsonProperty("idNumber")]
                public string Idno { get; set; }
                [JsonProperty("systemCode")]
                public string Mandtz { get; set; }
                //[JsonProperty("formBundleNumber")]
                public string Fbnumz { get; set; }
                [JsonProperty("portalUser")]
                public string PortalUsrz { get; set; }
                [JsonProperty("language")]
                public string Langz { get; set; }
                [JsonProperty("operation")]
                public string Operationz { get; set; }
                [JsonProperty("stepNumber")]
                public string StepNumberz { get; set; }
                [JsonProperty("returnId")]
                public string ReturnIdz { get; set; }
                //[JsonProperty("")]
                public string Officerz { get; set; }
                // [JsonProperty("TIN")]
                public string Gpartz { get; set; }
                [JsonProperty("statusCode")]
                public string Statusz { get; set; }
                [JsonProperty("userType")]
                public string UserTypz { get; set; }
                [JsonProperty("transactionType")]
                public string TxnTpz { get; set; }
                [JsonProperty("formProcess")]
                public string Formprocz { get; set; }
                //[JsonProperty("")]
                public string OfficerTz { get; set; }
                [JsonProperty("sourceApplication")]
                public string SrcAppz { get; set; }
                [JsonProperty("formBundleNumber")]
                public string Fbnum { get; set; }
                [JsonProperty("TIN")]
                public string Gpart { get; set; }
                [JsonProperty("declaration")]
                public string Inschk { get; set; }
                [JsonProperty("effectiveDateFrom")]
                public DateTime Edtfr { get; set; }
                [JsonProperty("effectiveDateTo")]
                public DateTime Edtto { get; set; }
                [JsonProperty("currentProxyTaxablePurchase")]
                public string Cptp { get; set; }
                [JsonProperty("currentProxyExemptPurchase")]
                public string Cpep { get; set; }
                [JsonProperty("currentTaxablePurchasePercentage")]
                public string Ctpp { get; set; }
                [JsonProperty("currentExemptPurchasePercentage")]
                public string Cepp { get; set; }
                [JsonProperty("proposedProxyTaxablePurchase")]
                public string Pcptp { get; set; }
                [JsonProperty("proposedProxyExemptPurchase")]
                public string Pcpep { get; set; }
                [JsonProperty("proposedTaxablePurchasePercentage")]
                public string Pctpp { get; set; }
                [JsonProperty("proposedExemptPurchasePercentage")]
                public string Pcepp { get; set; }
                [JsonProperty("currentDatrFrom")]
                public DateTime Cdtfr { get; set; }
                [JsonProperty("currentDatrTo")]
                public DateTime Cdtto { get; set; }
                [JsonProperty("declarationCheckBox1")]
                public string Decchk1 { get; set; }
                [JsonProperty("declarationCheckBox2")]
                public string Decchk2 { get; set; }
                [JsonProperty("idType")]
                public string Idtp { get; set; }
                [JsonProperty("contactPersonName")]
                public string Cnpr { get; set; }
                //[JsonProperty("")]
                public string Trtp { get; set; }
                // [JsonProperty("")]
                public string Stpno { get; set; }
                //[JsonProperty("systemCode")]
                public string Mandt { get; set; }
                //[JsonProperty("formBundleGUID")]
                public string FormGuid { get; set; }
                [JsonProperty("dataVersion")]
                public string DataVersion { get; set; }
                [JsonProperty("lineNumber")]
                public int LineNo { get; set; }
                [JsonProperty("rankingOrder")]
                public string RankingOrder { get; set; }
                [JsonProperty("serialNumber")]
                public string Euser { get; set; }
                [JsonProperty("declarationMode")]
                public string DmodeFlg { get; set; }
                [JsonProperty("status")]
                public string EvStatus { get; set; }
                [JsonProperty("attachments")]
                public List<Attachment> AttdetSet { get; set; }
                [JsonProperty("notes")]
                public List<Result2> NotesSet { get; set; }
                [JsonProperty("questionLists")]
                public List<object> QUESLISTSet { get; set; }
                [JsonProperty("questions")]
                public List<object> QUESTIONSSet { get; set; }
            }

        }

     
        public class VATReviewRequestTPFVOn1stAPISuccessModel
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
                public string Mandt { get; set; }
                public string Fbtyp { get; set; }
                public string Fbust { get; set; }
                public string Button { get; set; }
                public string TransactionType { get; set; }
                public string UserTyp { get; set; }
            }
         
            public class UIBTNSet
            {
                public List<Result> results { get; set; }
            }
         
            public class D
            {
                public Metadata __metadata { get; set; }
                public string Mandtz { get; set; }
                public string Fbtypz { get; set; }
                public string Fbustz { get; set; }
                public string UserTypz { get; set; }
                public string TransactionTypez { get; set; }
                public string EditFgz { get; set; }
                public string Mandt { get; set; }
                public string Fbnum { get; set; }
                public string PortalUsr { get; set; }
                public string Lang { get; set; }
                public string Operation { get; set; }
                public string StepNumber { get; set; }
                public string ReturnId { get; set; }
                public string Officer { get; set; }
                public string Gpart { get; set; }
                public string Status { get; set; }
                public string UserTyp { get; set; }
                public string TxnTp { get; set; }
                public string Formproc { get; set; }
                public string OfficerT { get; set; }
                public string SrcApp { get; set; }
                public string DestCheck { get; set; }
                public UIBTNSet UI_BTNSet { get; set; }
            }

        }
     
        public class VATReviewRequestTPFVReturnModel
        {
            public string AgreeFlag { get; set; }
            public DateTime? EffectiveDateFrom { get; set; }
            public DateTime? EffectiveDateTo { get; set; }
            public string CurPrxTaxablePurchases { get; set; }
            public string CurPrxExemptPurchases { get; set; }
            public string CurSplitBWTaxableAndExemptPur { get; set; }
            public string CurTaxablePurchases { get; set; }
            public string CurExemptPurchases { get; set; }
            public string ProPrxTaxablePurchases { get; set; }
            public string ProPrxExemptPurchases { get; set; }
            public string ProSplitBWTaxableAndExemptPur { get; set; }
            public string ProTaxablePurchases { get; set; }
            public string ProExemptPurchases { get; set; }
            public string ExplanationNotes { get; set; }
            public string AttachmentName { get; set; }
            public string DeclarationFlag { get; set; }
            public string IDType { get; set; }
            public string IDNumber { get; set; }
            public string ContactPersonName { get; set; }
        }

     
        public class VATReviewRequestVTGRModel
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
                public string Mandt { get; set; }
                public string FormGuid { get; set; }
                public string DataVersion { get; set; }
                public int LineNo { get; set; }
                public string RankingOrder { get; set; }
                public string Gpart { get; set; }
                public string Fbtyp { get; set; }
                public string TxnTp { get; set; }
                public string DmsTp { get; set; }
                public string DmsTxt { get; set; }
            }
         
            public class ELGBLDOCSet
            {
                public List<Result> results { get; set; }
            }
         
            public class Metadata3
            {
                public string id { get; set; }
                public string uri { get; set; }
                public string type { get; set; }
            }
         
            public class TABLESetResult
            {
                public Metadata3 __metadata { get; set; }
                public string Actnm { get; set; }
                public string Account { get; set; }
                public string AdditionalNo { get; set; }
                public string Addrnumber { get; set; }
                public string AggrePurchase { get; set; }
                public string AggreSupply { get; set; }
                public bool Articleatt { get; set; }
                public bool Bothatt { get; set; }
                public string BuildingNo { get; set; }
                public string City { get; set; }
                public string DataVersion { get; set; }
                public string Exporter { get; set; }
                public bool ExporterFg { get; set; }
                public string FormGuid { get; set; }
                public string Gpart { get; set; }
                public string Importer { get; set; }
                public bool ImporterFg { get; set; }
                public string LicenseCrno { get; set; }
                public string Mandt { get; set; }
                public bool Memoatt { get; set; }
                public bool Otheratt { get; set; }
                public string OtherTp { get; set; }
                public string PersonTp { get; set; }
                public string PostalCd { get; set; }
                public bool PurchaseFg { get; set; }
                public string Quarter { get; set; }
                public object RegDt { get; set; }
                public string Region { get; set; }
                public string RegionDesc { get; set; }
                public string Srcidentify { get; set; }
                public string Street { get; set; }
                public bool SupplyFg { get; set; }
                public string TpDescr { get; set; }
                public string VatPuchase { get; set; }
                public string VatSupply { get; set; }
            }
         
            public class TABLESet
            {
                public List<TABLESetResult> results { get; set; }
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
                public string Notenoz { get; set; }
                public string Refnamez { get; set; }
                public string XInvoicez { get; set; }
                public string XObsoletez { get; set; }
                public string Rcodez { get; set; }
                public string Erfusrz { get; set; }
                public DateTime Erfdtz { get; set; }
                public string Erftmz { get; set; }
                public string AttByz { get; set; }
                public string ByPusrz { get; set; }
                public string ByGpartz { get; set; }
                public string DataVersionz { get; set; }
                public string Namez { get; set; }
                public string Noteno { get; set; }
                public int Lineno { get; set; }
                public int ElemNo { get; set; }
                public string Tdformat { get; set; }
                public string Tdline { get; set; }
                public string Sect { get; set; }
                public string Strdt { get; set; }
                public string Strtime { get; set; }
                public string Strline { get; set; }
            }
         
            public class NOTESSet
            {
                public List<Result3> results { get; set; }
            }
         
            public class Metadata5
            {
                public string id { get; set; }
                public string uri { get; set; }
                public string type { get; set; }
            }
         
            public class Result4
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
                public DateTime Erfdt { get; set; }
                public string Erftm { get; set; }
                public string DataVersion { get; set; }
                public string DocUrl { get; set; }
                public string OutletRef { get; set; }
                public string Enbedit { get; set; }
                public string Enbdele { get; set; }
                public string Visedit { get; set; }
                public string Visdel { get; set; }
            }
         
            public class ATTDETSet
            {
                public List<Attachment> results { get; set; }
            }
         
            public class QUESLISTSet
            {
                public List<object> results { get; set; }
            }
         
            public class Metadata6
            {
                public string id { get; set; }
                public string uri { get; set; }
                public string type { get; set; }
            }
         
            public class Result5
            {
                public Metadata6 __metadata { get; set; }
                //[JsonProperty("effectiveDates")]
                public DateTime? Abrzu { get; set; }
                [JsonProperty("periodkey")]
                public string Persl { get; set; }
                [JsonProperty("periodDescription")]
                public string Txt50 { get; set; }
                [JsonProperty("effectiveDate")]
                public string EffDt { get; set; }
            }
         

            public class EFFDATESet
            {
                public List<Result5> results { get; set; }
            }
         
            public class D
            {
                public Metadata __metadata { get; set; }
                [JsonProperty("VATSupply")]
                public string VatSupply { get; set; }
                [JsonProperty("VATPurchase")]
                public string VatPuchase { get; set; }
                [JsonProperty("userType")]
                public string UserTypz { get; set; }
                [JsonProperty("periodDescription")]
                public string Txt50 { get; set; }
                [JsonProperty("transactionType")]
                public string TxnTpz { get; set; }
                [JsonProperty("typeDescription")]
                public string TpDescr { get; set; }
                [JsonProperty("isSupply")]
                public bool SupplyFg { get; set; }
                [JsonProperty("street")]
                public string Street { get; set; }
                [JsonProperty("stepNumber")]
                public string StepNumberz { get; set; }
                //[JsonProperty("")]
                public string StepNumber { get; set; }
                // [JsonProperty("")]
                public string Statusz { get; set; }
                //[JsonProperty("sourceIdentifier")]
                public string Srcidentifyz { get; set; }
                [JsonProperty("sourceIdentifier")]
                public string Srcidentify { get; set; }
                [JsonProperty("isReview")]
                public bool ReviewFg { get; set; }
                [JsonProperty("returnId")]
                public string ReturnIdz { get; set; }
                [JsonProperty("regionDescription")]
                public string RegionDesc { get; set; }
                [JsonProperty("region")]
                public string Region { get; set; }
                //[JsonProperty("")]
                public DateTime RegDt { get; set; }
                [JsonProperty("reason")]
                public string Reason { get; set; }
                [JsonProperty("quarter")]
                public string Quarter { get; set; }
                [JsonProperty("isPurchase")]
                public bool PurchaseFg { get; set; }
                [JsonProperty("postalCode")]
                public string PostalCd { get; set; }
                [JsonProperty("portalUser")]
                public string PortalUsrz { get; set; }
                [JsonProperty("personType")]
                public string PersonTp { get; set; }
                [JsonProperty("periodKey")]
                public string Persl { get; set; }
                [JsonProperty("isOtherAttachment")]
                public bool Otheratt { get; set; }
                [JsonProperty("otherTypeSelected")]
                public string OtherTp { get; set; }
                [JsonProperty("operation")]
                public string Operationz { get; set; }
                //[JsonProperty("")]
                public string Officerz { get; set; }
                // [JsonProperty("")]
                public object OffRegDt { get; set; }
                [JsonProperty("officerPeriodKey")]
                public string OffPersl { get; set; }
                [JsonProperty("isMemoAttachment")]
                public bool Memoatt { get; set; }
                [JsonProperty("systemCode")]
                public string Mandt { get; set; }
                [JsonProperty("licenseCRNumber")]
                public string LicenseCrno { get; set; }
                [JsonProperty("language")]
                public string Langz { get; set; }
                [JsonProperty("isImporter")]
                public bool ImporterFg { get; set; }
                [JsonProperty("importer")]
                public string Importer { get; set; }
                [JsonProperty("TIN")]
                public string Gpart { get; set; }
                [JsonProperty("fullName")]
                public string FullNm { get; set; }
                [JsonProperty("formProcess")]
                public string Formprocz { get; set; }
                [JsonProperty("formGUID")]
                public string FormGuid { get; set; }
                [JsonProperty("formBundleNumber")]
                public string Fbnumz { get; set; }
                [JsonProperty("formBundleGUID")]
                public string Fbguid { get; set; }
                [JsonProperty("isExporter")]
                public bool ExporterFg { get; set; }
                [JsonProperty("exporter")]
                public string Exporter { get; set; }
                [JsonProperty("serialNumber")]
                public string Euser { get; set; }
                [JsonProperty("declarationName")]
                public string Decname { get; set; }
                [JsonProperty("declarationIdType")]
                public string DecidTy { get; set; }
                [JsonProperty("declarationIdNumber")]
                public string DecidNo { get; set; }
                [JsonProperty("declaration")]
                public string Decfg { get; set; }
                [JsonProperty("declarationDesignation")]
                public string Decdesignation { get; set; }
                //[JsonProperty("")]
                public object Decdate { get; set; }
                [JsonProperty("dataVersion")]
                public string DataVersion { get; set; }
                [JsonProperty("city")]
                public string City { get; set; }
                [JsonProperty("buildingNumber")]
                public string BuildingNo { get; set; }
                [JsonProperty("isBothAttachment")]
                public bool Bothatt { get; set; }
                [JsonProperty("isArticleAttachment")]
                public bool Articleatt { get; set; }
                [JsonProperty("instructionAgree")]
                public string AgrFg { get; set; }
                [JsonProperty("aggregatedSupply")]
                public string AggreSupply { get; set; }
                [JsonProperty("aggregatedPurchase")]
                public string AggrePurchase { get; set; }
                [JsonProperty("addressNumber")]
                public string Addrnumber { get; set; }
                [JsonProperty("additionalNumber")]
                public string AdditionalNo { get; set; }
                [JsonProperty("activityName")]
                public string Actnm { get; set; }
                [JsonProperty("account")]
                public string Account { get; set; }
                [JsonProperty("eligibleDocuments")]
                public List<Result> ELGBL_DOCSet { get; set; }
                [JsonProperty("tables")]
                public List<TABLESetResult> TABLESet { get; set; }
                [JsonProperty("notes")]
                public List<Result3> NOTESSet { get; set; }
                [JsonProperty("attachments")]
                public List<Attachment> ATTDETSet { get; set; }
                [JsonProperty("questions")]
                public List<object> QUESLISTSet { get; set; }
                [JsonProperty("effectiveDates")]
                public List<Result5> EFFDATESet { get; set; }
            }

        }

     
        public class VATReviewRequestVTGROn1stAPISuccessModel
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
                public string Mandt { get; set; }
                public string Spras { get; set; }
                public string Fbtyp { get; set; }
                public string TxnTp { get; set; }
                public string DmsTp { get; set; }
                public DateTime? StartDt { get; set; }
                public DateTime? EndDt { get; set; }
                public string Txt50 { get; set; }
            }
         
            public class ELGBLDOCSet
            {
                public List<Result> results { get; set; }
            }
         
            public class VRUIBTNSet
            {
                public List<object> results { get; set; }
            }
         
            public class D
            {
                public Metadata __metadata { get; set; }
                public string EditFgz { get; set; }
                public string Fbnum { get; set; }
                public string Fbtypz { get; set; }
                public string Fbustz { get; set; }
                public string Formproc { get; set; }
                public string Gpart { get; set; }
                public string Lang { get; set; }
                public string Mandt { get; set; }
                public string Officer { get; set; }
                public string Operation { get; set; }
                public string PortalUsr { get; set; }
                public string ReturnId { get; set; }
                public string Status { get; set; }
                public string StepNumber { get; set; }
                public string TxnTp { get; set; }
                public string UserTyp { get; set; }
                public ELGBLDOCSet ELGBL_DOCSet { get; set; }
                public VRUIBTNSet VR_UI_BTNSet { get; set; }
            }
        }
     
        public class VATDREGViewApllicationViewModel
        {
            public AttdetSet AttachmentSet { get; set; }
            public string RequestType { get; set; }
            public string ReasonforDeRegistration { get; set; }
            public string ContactPersonName { get; set; }
            public string DeclarationId { get; set; }
         
            public class AttdetSet
            {

                public List<Attachment> results { get; set; }

            }

            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public DateTime Duedate { get; set; }
            public DateTime SuspDtfrom { get; set; }
            public DateTime SuspDtto { get; set; }
            public DateTime NextDtfrom { get; set; }
            public DateTime NextDtto { get; set; }

         
            public class Result3
            {

                public Metadata4 __metadata { get; set; }
                public int Srno { get; set; }
                public DateTime Erfdt { get; set; }
                public string SchGuid { get; set; }
                public string Doguid { get; set; }
                public string RetGuid { get; set; }
                public string Seqno { get; set; }
                public string Dotyp { get; set; }
                public string AttBy { get; set; }
                public string Filename { get; set; }
                public string FileExtn { get; set; }
                public string Mimetype { get; set; }
                public string ByPusr { get; set; }
                public string DataVersion { get; set; }
                public string DocUrl { get; set; }
                public string OutletRef { get; set; }
                public string Enbedit { get; set; }
                public string Enbdele { get; set; }
                public string Visedit { get; set; }
                public string Visdel { get; set; }
                public string Erftm { get; set; }
            }

        }

     
        public class VATObjectionDREGReasonModel
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
         
            public class Result
            {
                public Metadata __metadata { get; set; }
                [JsonProperty("transactionType")]
                public string TxnTp { get; set; }
                [JsonProperty("language")]
                public string Lang { get; set; }
                [JsonProperty("reason")]
                public string Reason { get; set; }
                [JsonProperty("reasonDescription")]
                public string Rdesc { get; set; }
            }
         
            public class D
            {
                [JsonProperty("deregistrationReasons")]
                public List<Result> results { get; set; }
            }


        }

     
        public class VATReviewDREGViewApplicationModel
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
                // [JsonProperty("")]
                public string Mandt { get; set; }
                //[JsonProperty("")]
                public string FormGuid { get; set; }
                //[JsonProperty("")]
                public string DataVersion { get; set; }
                //[JsonProperty("")]
                public int LineNo { get; set; }
                //[JsonProperty("")]
                public string RankingOrder { get; set; }
                //[JsonProperty("")]
                public string AddrType { get; set; }
                // [JsonProperty("")]
                public string Srcidentify { get; set; }
                //[JsonProperty("")]
                public DateTime Begda { get; set; }
                // [JsonProperty("")]
                public DateTime Endda { get; set; }
                //[JsonProperty("")]
                public string Gpart { get; set; }
                //[JsonProperty("")]
                public string Street { get; set; }
                //[JsonProperty("")]
                public string HouseNum1 { get; set; }
                //[JsonProperty("")]
                public string HouseNum2 { get; set; }
                // [JsonProperty("")]
                public string Building { get; set; }
                //[JsonProperty("")]
                public string Floor { get; set; }
                //[JsonProperty("")]
                public string PostCode1 { get; set; }
                //[JsonProperty("")]
                public string City1 { get; set; }
                //[JsonProperty("")]
                public string City2 { get; set; }
                //[JsonProperty("")]
                public string Country { get; set; }
                //[JsonProperty("")]
                public string Region { get; set; }
                //[JsonProperty("")]
                public string RegionDesc { get; set; }
            }
         
            public class AddressSet
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
                //[JsonProperty("noteNumber")]
                public string Notenoz { get; set; }
                [JsonProperty("referenceName")]
                public string Refnamez { get; set; }
                //[JsonProperty("")]
                public string XInvoicez { get; set; }
                //[JsonProperty("")]
                public string XObsoletez { get; set; }
                [JsonProperty("processingReason")]
                public string Rcodez { get; set; }
                [JsonProperty("userName")]
                public string Erfusrz { get; set; }
                [JsonProperty("entryDate")]
                public String Erfdtz { get; set; }
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
                public int Lineno { get; set; }
                [JsonProperty("elementNumber")]
                public int ElemNo { get; set; }
                //[JsonProperty("")]
                public string Tdformat { get; set; }
                //[JsonProperty("")]
                public string Tdline { get; set; }
                [JsonProperty("section")]
                public string Sect { get; set; }
                //[JsonProperty("")]
                public string Strdt { get; set; }
                [JsonProperty("startTime")]
                public string Strtime { get; set; }
                [JsonProperty("notestext")]
                public string Strline { get; set; }
            }
         
            public class NotesSet
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
                public int Srno { get; set; }
                public DateTime Erfdt { get; set; }
                public string SchGuid { get; set; }
                public string Doguid { get; set; }
                public string RetGuid { get; set; }
                public string Seqno { get; set; }
                public string Dotyp { get; set; }
                public string AttBy { get; set; }
                public string Filename { get; set; }
                public string FileExtn { get; set; }
                public string Mimetype { get; set; }
                public string ByPusr { get; set; }
                public string DataVersion { get; set; }
                public string DocUrl { get; set; }
                public string OutletRef { get; set; }
                public string Enbedit { get; set; }
                public string Enbdele { get; set; }
                public string Visedit { get; set; }
                public string Visdel { get; set; }
                public string Erftm { get; set; }
            }
            
            public class AttdetSet
            {
                public List<Attachment> results { get; set; }
            }
         
            public class QuesListSet
            {
                public List<object> results { get; set; }
            }
            
            public class HeaderSet
            {
                [JsonProperty("isCase")]
                public bool CaseFg { get; set; }
                [JsonProperty("isCaseReason")]
                public string CaseReason { get; set; }
                [JsonProperty("isOpenCase")]
                public bool OpenCaseFg { get; set; }
                [JsonProperty("isReviewed")]
                public bool ReviewFg { get; set; }
                [JsonProperty("isAgree")]
                public bool Agreeflg { get; set; }
                [JsonProperty("isAuditAttachment")]
                public bool Auditatt { get; set; }
                [JsonProperty("isCertificateAttachment")]
                public bool Certatt { get; set; }
                [JsonProperty("isCheckDate")]
                public bool Chkdt { get; set; }
                [JsonProperty("isContractAttachment")]
                public bool Contractatt { get; set; }
                [JsonProperty("isdeclaration1")]
                public bool Declareflg { get; set; }
                [JsonProperty("isdeclaration2")]
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
                [JsonProperty("deregistrationDate")]
                public String Dregdt { get; set; }
                //[JsonProperty("")]
                public DateTime? Duedate { get; set; }
                // [JsonProperty("")]
                public object SuspDtfrom { get; set; }
                //[JsonProperty("")]
                public object SuspDtto { get; set; }
                //[JsonProperty("")]
                public object NextDtfrom { get; set; }
                //[JsonProperty("")]
                public object NextDtto { get; set; }
                [JsonProperty("declarationDate")]
                public String Declaredt { get; set; }
                [JsonProperty("endDate")]
                public String EndDate { get; set; }
                //[JsonProperty("")]
                public object Lasticrdt { get; set; }
                [JsonProperty("startDate")]
                public String StartDate { get; set; }
                [JsonProperty("taxDate")]
                public String Taxdt { get; set; }
                [JsonProperty("activityName")]
                public string Actnm { get; set; }
                [JsonProperty("caseId")]
                public string CaseId { get; set; }
                [JsonProperty("suspensionPeriod")]
                public string SuspPeriod { get; set; }
                [JsonProperty("partnerType")]
                public string Atype { get; set; }
                [JsonProperty("nextPeriod")]
                public string NextPeriod { get; set; }
                [JsonProperty("branch")]
                public string Branchx { get; set; }
                [JsonProperty("calendarType")]
                public string Caltp { get; set; }
                [JsonProperty("contactName")]
                public string Contactnm { get; set; }
                [JsonProperty("CRNumber")]
                public string Crno { get; set; }
                [JsonProperty("dataVersion")]
                public string DataVersion { get; set; }
                [JsonProperty("designation")]
                public string Designation { get; set; }
                [JsonProperty("authenticationUser")]
                public string Euser { get; set; }
                [JsonProperty("formBundleNumber")]
                public string Fbnumx { get; set; }
                [JsonProperty("formBundleStatus1")]
                public string Fbstax { get; set; }
                [JsonProperty("formBundleStatus2")]
                public string Fbustx { get; set; }
                [JsonProperty("formBundleGUID")]
                public string FormGuid { get; set; }
                [JsonProperty("formProc")]
                public string Formprocx { get; set; }
                [JsonProperty("forward")]
                public string Forwardx { get; set; }
                [JsonProperty("fullName")]
                public string FullName { get; set; }
                [JsonProperty("TIN")]
                public string Gpart { get; set; }
                // [JsonProperty("")]
                public string Gpartx { get; set; }
                [JsonProperty("idNumber")]
                public string Idnumbr { get; set; }
                [JsonProperty("language")]
                public string Langx { get; set; }
                [JsonProperty("systemCode")]
                public string Mandt { get; set; }
                // [JsonProperty("")]
                public string Mandtx { get; set; }
                [JsonProperty("firstName")]
                public string NameFirst { get; set; }
                [JsonProperty("lastName")]
                public string NameLast { get; set; }
                [JsonProperty("organizationName1")]
                public string NameOrg1 { get; set; }
                [JsonProperty("organizationName2")]
                public string NameOrg2 { get; set; }
                [JsonProperty("officer")]
                public string Officerx { get; set; }
                [JsonProperty("operation")]
                public string Operationx { get; set; }
                [JsonProperty("other")]
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
                [JsonProperty("status")]
                public string Statusx { get; set; }
                [JsonProperty("stepNumber1")]
                public string StepNumber { get; set; }
                // [JsonProperty("")]
                public string StepNumberx { get; set; }
                [JsonProperty("transactionType")]
                public string TxnTpx { get; set; }
                [JsonProperty("idType")]
                public string Type { get; set; }
                [JsonProperty("userType")]
                public string UserTypx { get; set; }
            }

            
            public class D
            {

                [JsonProperty("headerSet")]
                public HeaderSet HeaderSet { get; set; }

                [JsonProperty("addressSet")]
                public List<Result> AddressSet { get; set; }
                [JsonProperty("notesSet")]
                public List<Result2> NotesSet { get; set; }
                [JsonProperty("attachmentDetSet")]
                public List<Attachment> AttdetSet { get; set; }
                [JsonProperty("questionListSet")]
                public List<object> QuesListSet { get; set; }
            }

        }
     
        public class VATReviewRequestVTGRReturnModel
        {
            public string AgreeFlag { get; set; }
            public string TIN { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string CRLicense { get; set; }
            public string VATAccount { get; set; }
            public string LegalPersonType { get; set; }
            public string IsGrpMbrExporter { get; set; }
            public string IsGrpMbrImporter { get; set; }
            public string WhatIsYourVATEliigibleSupplies { get; set; }
            public string WhatIsYourVATEliigiblePurchases { get; set; }
            public string WhatIsYourGrpVATEliigibleSupplies { get; set; }
            public string WhatIsYourGrpVATEliigiblePurchases { get; set; }
            public string GroupEffectiveDate { get; set; }
            public string AttachmentName { get; set; }
            public string DeclarationFlag { get; set; }
            public string IDType { get; set; }
            public string IDNumber { get; set; }
            public string ContactPersonName { get; set; }
        }

     
        public class VATReviewDREGSuspensionListModel
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
         
            public class Result
            {
                public Metadata __metadata { get; set; }
                [JsonProperty("TIN")]
                public string Gpart { get; set; }
                [JsonProperty("startDate")]
                public String StartDate { get; set; }
                [JsonProperty("endDate")]
                public String EndDate { get; set; }
                [JsonProperty("correspondenceDueDate")]
                public String Duedate { get; set; }
                [JsonProperty("suspensionDateFrom")]
                public String SuspDtfrom { get; set; }
                [JsonProperty("suspensionDateTo")]
                public String SuspDtto { get; set; }
                [JsonProperty("nextDateFrom")]
                public String NextDtfrom { get; set; }
                [JsonProperty("nextDateTo")]
                public String NextDtto { get; set; }
            }
         
            public class D
            {
                [JsonProperty("suspensions")]
                public List<Result> results { get; set; }
            }

        }

    }

}