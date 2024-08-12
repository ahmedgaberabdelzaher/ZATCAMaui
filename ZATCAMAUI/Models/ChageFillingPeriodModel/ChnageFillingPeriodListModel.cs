using System.Runtime.Serialization;
using Foundation;
using Newtonsoft.Json;

namespace ZATCAMAUI.Models.ChageFillingPeriodModel
{

    public class VATChangeFillingListModel
    {
        [Preserve(AllMembers = true)]
        [JsonProperty("data")]
        public D d { get; set; }
       
        public class Metadata
        {
            [DataMember]
            public string id { get; set; }
            [DataMember]
            public string uri { get; set; }
            [DataMember]
            public string type { get; set; }
        }
       

        public class Metadata2
        {
            [DataMember]
            public string id { get; set; }
            [DataMember]
            public string uri { get; set; }
            [DataMember]
            public string type { get; set; }
        }
       
        public class Result
        {
            //[DataMember]
            //public Metadata2 __metadata { get; set; }
            [DataMember]
            [JsonProperty("formBundleTypeDescription")]
            public string FbtText { get; set; }
            [DataMember]
            [JsonProperty("formBundleType")]
            public string Fbtyp { get; set; }
            [DataMember]
            [JsonProperty("userError")]
            public string UserErrFg { get; set; }
            [DataMember]
            [JsonProperty("system")]
            public string SysFlg { get; set; }
            [DataMember]
            [JsonProperty("lastDate")]
            public object Ldate { get; set; }
        }

        [Preserve(AllMembers = true)]
        public class REQTYPSet
        {

            [DataMember]
            public List<Result> results { get; set; }
        }
       
        public class Metadata3
        {
            [DataMember]
            public string id { get; set; }
            [DataMember]
            public string uri { get; set; }
            [DataMember]
            public string type { get; set; }
        }
       
        public class Result2
        {
            //[DataMember]
            //public Metadata3 __metadata { get; set; }
            [DataMember]
            [JsonProperty("systemCode")]
            public string Mandt { get; set; }
            [DataMember]
            [JsonProperty("statusProfile")]
            public string Stsma { get; set; }
            [DataMember]
            [JsonProperty("userStatus")]
            public string Estat { get; set; }
            [DataMember]
            [JsonProperty("language")]
            public string Spras { get; set; }
            [DataMember]
            [JsonProperty("statusId ")]
            public string Txt04 { get; set; }
            [DataMember]
            [JsonProperty("statusDescription")]
            public string Txt30 { get; set; }
            [DataMember]
            [JsonProperty("longText ")]
            public bool Ltext { get; set; }
        }
       
        public class STATUSSet
        {

            [DataMember]
            public List<Result2> results { get; set; }
        }

        [Preserve(AllMembers = true)]
        public class Metadata4
        {
            [DataMember]
            public string id { get; set; }
            [DataMember]
            public string uri { get; set; }
            [DataMember]
            public string type { get; set; }
        }
       
        public class ChangeFillingFrequency
        {
            //[DataMember]
            //public Metadata4 __metadata { get; set; }
            [DataMember]
            [JsonProperty("selector")]
            public string Selector { get; set; }
            [DataMember]
            [JsonProperty("formBundleNumber")]
            public string Fbnum { get; set; }
            [DataMember]
            [JsonProperty("formBundleType")]
            public string Fbtyp { get; set; }
            [DataMember]
            [JsonProperty("TIN")]
            public string Gpart { get; set; }
            [DataMember]
            [JsonProperty("lastName")]
            public string NameLast { get; set; }
            [DataMember]
            [JsonProperty("firstName")]
            public string NameFirst { get; set; }
            [DataMember]
            [JsonProperty("organizationName1")]
            public string NameOrg1 { get; set; }
            [DataMember]
            [JsonProperty("organizationName2")]
            public string NameOrg2 { get; set; }
            [DataMember]
            [JsonProperty("fullName")]
            public string FullNm { get; set; }
            [DataMember]
            [JsonProperty("TINFullName")]
            public string GpartFullNm { get; set; }
            [DataMember]
            [JsonProperty("workflowSubreason")]
            public string WfSub { get; set; }
            [DataMember]
            [JsonProperty("userStatus")]
            public string Fbust { get; set; }
            [DataMember]
            [JsonProperty("formStatusDescription")]
            public string FbustTxt { get; set; }
            [DataMember]
            [JsonProperty("receiptDate")]
            public string Receipt { get; set; }
            [DataMember]
            [JsonProperty("assignedUser")]
            public string AssignUsr { get; set; }
            [DataMember]
            [JsonProperty("userName")]
            public string LoginUsr { get; set; }
            [DataMember]
            [JsonProperty("assignMe")]

            public string AssignMe { get; set; }
            [DataMember]
            [JsonProperty("newUserName")]
            public string NewUser { get; set; }
            [DataMember]
            [JsonProperty("tile")]
            public string TileInd { get; set; }
            [DataMember]
            [JsonProperty("formBundleTypeDescription")]
            public string FbtText { get; set; }
            [DataMember]
            [JsonProperty("transactionType")]
            public string TransactionType { get; set; }
            [DataMember]
            [JsonProperty("ID")]
            public string WiId { get; set; }
            [DataMember]
            [JsonProperty("priority")]
            public string WiPrio { get; set; }
            [DataMember]
            [JsonProperty("priorityDescription")]
            public string WiPrioDesc { get; set; }
        }
       
        public class ASSLISTSet
        {

            [DataMember]
            public List<ChangeFillingFrequency> results { get; set; }
        }

        [Serializable]
        [DataContract]
       
        public class D
        {
            //[DataMember]
            //public Metadata __metadata { get; set; }
            [DataMember]
            [JsonProperty("userTIN")]
            public string UserTin { get; set; }
            [DataMember]
            [JsonProperty("auditorTIN")]
            public string AudTin { get; set; }
            //[DataMember]
            //[JsonProperty("ASSList")]
            //public object Begda { get; set; }
            [DataMember]
            [JsonProperty("taxType")]
            public string TaxType { get; set; }
            //[DataMember]
            //[JsonProperty("ASSList")]
            //public object Endda { get; set; }
            [DataMember]
            [JsonProperty("serialNumber")]
            public string Euser { get; set; }
            [DataMember]
            [JsonProperty("formBundleNumber")]
            public string Fbnum { get; set; }
            [DataMember]
            [JsonProperty("formBundleStatus")]
            public string Fbsta { get; set; }
            [DataMember]
            [JsonProperty("formBundleType")]
            public string Fbtyp { get; set; }
            //[DataMember]
            //[JsonProperty("ASSList")]
            //public string Fbust { get; set; }
            [DataMember]
            [JsonProperty("formProcess")]
            public string Formproc { get; set; }
            [DataMember]
            [JsonProperty("TIN")]
            public string Gpart { get; set; }
            [DataMember]
            [JsonProperty("language")]
            public string Lang { get; set; }
            //[DataMember]
            //[JsonProperty("ASSList")]
            //public string Mandt { get; set; }
            //[DataMember]
            //[JsonProperty("ASSList")]
            //public string Officer { get; set; }
            [DataMember]
            [JsonProperty("operation")]
            public string Operation { get; set; }
            //[DataMember]
            //[JsonProperty("ASSList")]
            //public string Persl { get; set; }
            [DataMember]
            [JsonProperty("portalUser")]
            public string PortalUsr { get; set; }
            [DataMember]
            [JsonProperty("returnId")]
            public string ReturnId { get; set; }
            [DataMember]
            [JsonProperty("statusCode")]
            public string Status { get; set; }
            [DataMember]
            [JsonProperty("stepNumber")]
            public string StepNumber { get; set; }
            [DataMember]
            [JsonProperty("transactionType")]
            public string TransactionType { get; set; }
            //[DataMember]
            //[JsonProperty("ASSList")]
            //public string TxnTp { get; set; }
            [DataMember]
            [JsonProperty("userType")]
            public string UserTyp { get; set; }
            [DataMember]
            [JsonProperty("requestTypeList")]
            public List<Result> REQTYPSet { get; set; }
            [DataMember]
            [JsonProperty("statusList")]
            public List<Result2> STATUSSet { get; set; }
            [DataMember]
            [JsonProperty("ASSList")]
            public List<ChangeFillingFrequency> ASSLISTSet { get; set; }
        }


       
        public class VATChangeFillingListData
        {
            [DataMember]
            public string Fbnum { get; set; }
            [DataMember]
            public string Gpart { get; set; }
            [DataMember]
            //Status code for Fbust and it used for sumamry purpose
            public string Fbust { get; set; }
            [DataMember]
            public string FbustTxt { get; set; }
            [DataMember]

            //FbtText means Request to change filing frequency
            public string FbtText { get; set; }

        }

    }
   
    public class VATChangeFillingSummaryModel
    {
        [Preserve(AllMembers = true)]
        [JsonProperty("data")]
        public D d { get; set; }
       
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
            public List<object> results { get; set; }
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
       
        public class ATTACHSet
        {
            public List<Attachment> results { get; set; }
        }
       
        public class ATTTYPSet
        {
            public List<object> results { get; set; }
        }
       
        public class QuesListSet
        {
            public List<object> results { get; set; }
        }
       
        public class D
        {
            public Metadata __metadata { get; set; }
            [JsonProperty("documentCategory")]
            public string Attchk { get; set; }
            [JsonProperty("periodKey")]
            public string CPersl { get; set; }
            [JsonProperty("formBundleNumber")]
            public string Fbnumz { get; set; }
            [JsonProperty("agree")]
            public string Iagrfg { get; set; }
            [JsonProperty("request")]
            public string Reqfg { get; set; }
            [JsonProperty("stepNumber")]
            public string StepNumber { get; set; }
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
            //[JsonProperty("periodKey")]
            //public string Persl { get; set; }
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
            public List<object> NOTESSet { get; set; }
            [JsonProperty("attachments")]
            public List<Attachment> ATTACHSet { get; set; }
            [JsonProperty("attachmentsTypes")]
            public List<object> ATT_TYPSet { get; set; }
            [JsonProperty("questions")]
            public List<object> QuesListSet { get; set; }
        }

       
        public class VATChangingSummaryData
        {

            //step1
            [JsonProperty("documentCategory")]
            public string Attchk { get; set; }
            [JsonProperty("formBundleNumber")]
            public string Fbnum { get; set; }

            //step2
            [JsonProperty("currentFrequency")]
            public string CureentF { get; set; }
            [JsonProperty("filingFrequency")]
            public string FilingF { get; set; }
            [JsonProperty("periodKey")]
            public string Persl { get; set; }
            //Step-3
            [JsonProperty("declaration")]
            public string Decfg { get; set; }
            [JsonProperty("declarationName")]
            public string Decname { get; set; }
            [JsonProperty("declarationIdNumber")]
            public string DecidNo { get; set; }
            [JsonProperty("declarationIdType")]
            public string DecidTy { get; set; }

            public List<Attachment> AttachmentList { get; set; }
            public List<object> NOTESSet { get; set; }
        }


    }
}
