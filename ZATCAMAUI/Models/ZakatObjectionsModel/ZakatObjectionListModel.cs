using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ZATCAMAUI.Models.ZakatObjectionsModel
{

    public class ZakatObjectionListModel
    {
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
            [DataMember]
            public Metadata2 __metadata { get; set; }
            [DataMember]
            [JsonProperty("formBundleNumber")]
            public string Fbnum { get; set; }
            [DataMember]
            [JsonProperty("objectionStatus")]
            public string Objstatus { get; set; }
            [DataMember]
            [JsonProperty("formBundleStatus")]
            public string Fbsta { get; set; }
            [DataMember]
            [JsonProperty("statusDescription")]
            public string StatText { get; set; }
            [DataMember]
            [JsonProperty("formBundleType")]
            public string Fbtyp { get; set; }
            [DataMember]
            [JsonProperty("formBundleTypeDescription")]
            public string FbtText { get; set; }
            //[DataMember]
            //[JsonProperty("formBundleNumber")]
            //public string Erfdate { get; set; }
            //[DataMember]
            //[JsonProperty("formBundleNumber")]
            //public string Erftime { get; set; }
            [DataMember]
            [JsonProperty("periodkey")]
            public string Persl { get; set; }
            [DataMember]
            [JsonProperty("taxPeriod")]
            public string TaxPeriod { get; set; }
            //[DataMember]
            //[JsonProperty("formBundleNumber")]
            //public object DueDt { get; set; }
            //[DataMember]
            //[JsonProperty("formBundleNumber")]
            //public string Due { get; set; }
            //[DataMember]
            //[JsonProperty("formBundleNumber")]
            //public object Abrzu { get; set; }
            //[DataMember]
            //[JsonProperty("formBundleNumber")]
            //public object Abrzo { get; set; }
            [DataMember]
            [JsonProperty("inboundCorrespondenceType")]
            public string Incotyp { get; set; }
            [DataMember]
            [JsonProperty("inboundCorrespondenceDescription")]
            public string Incotext { get; set; }
            [DataMember]
            [JsonProperty("flag")]
            public string Flag { get; set; }
            //[DataMember]
            //[JsonProperty("formBundleNumber")]
            //public string CalendrTyp { get; set; }
            [DataMember]
            [JsonProperty("attachedByPerson")]
            public string PrcBy { get; set; }
            [DataMember]
            [JsonProperty("group")]
            public string Grp { get; set; }
            [DataMember]
            [JsonProperty("creditDate")]
            public string CrdtText { get; set; }
            [DataMember]
            [JsonProperty("serialNumber")]
            public string Euser { get; set; }
            [DataMember]
            [JsonProperty("formBundleGUID")]
            public string Fbguid { get; set; }
        }

        public class ListSet
        {
            [DataMember]
            public List<Result> results { get; set; }
        }

        public class Deferred
        {
            [DataMember]
            public string uri { get; set; }
        }

        public class AuthServSet
        {
            [DataMember]
            public Deferred __deferred { get; set; }
        }
        [Serializable]
        [DataContract]

        public class D
        {
            [DataMember]
            public Metadata __metadata { get; set; }
            [DataMember]
            public string CallServ { get; set; }
            [DataMember]
            public int Accnum { get; set; }
            [DataMember]
            public int Actcnt { get; set; }
            [DataMember]
            public string Auditor { get; set; }
            [DataMember]
            public bool AudObjection { get; set; }
            [DataMember]
            public bool AudRefund { get; set; }
            [DataMember]
            public bool AudRefundTrn { get; set; }
            [DataMember]
            public bool AudRequest { get; set; }
            [DataMember]
            public bool AudReturn { get; set; }
            [DataMember]
            public string Bpnum { get; set; }
            [DataMember]
            public string Branch { get; set; }
            [DataMember]
            public string Caltype { get; set; }
            [DataMember]
            public string Client { get; set; }
            [DataMember]
            public int Cnlcnt { get; set; }
            [DataMember]
            public int Corrnum { get; set; }
            [DataMember]
            public string Dept { get; set; }
            [DataMember]
            public bool DisSharetile { get; set; }
            [DataMember]
            public bool EnableInstPlan { get; set; }
            [DataMember]
            public bool EnableTile { get; set; }
            [DataMember]
            public string Ettr { get; set; }
            [DataMember]
            public string Euser { get; set; }
            [DataMember]
            public string Euser1 { get; set; }
            [DataMember]
            public string Euser2 { get; set; }
            [DataMember]
            public string Euser3 { get; set; }
            [DataMember]
            public string Euser4 { get; set; }
            [DataMember]
            public string Euser5 { get; set; }
            [DataMember]
            public string ExeAppFlg { get; set; }
            [DataMember]
            public string ExeDtFlg { get; set; }
            [DataMember]
            public string Fbguid { get; set; }
            [DataMember]
            public string HostName { get; set; }
            [DataMember]
            public int Indcorrnum { get; set; }
            [DataMember]
            public string Interest { get; set; }
            [DataMember]
            public string IntPortal { get; set; }
            [DataMember]
            public bool IsBankruptcy { get; set; }
            [DataMember]
            public string Lang { get; set; }
            [DataMember]
            public string Name { get; set; }
            [DataMember]
            public bool NotifLogFlag { get; set; }
            [DataMember]
            public string NregDtFlg { get; set; }
            [DataMember]
            public int Oblnum { get; set; }
            [DataMember]
            public string Overdue { get; set; }
            [DataMember]
            public string Penalty { get; set; }
            [DataMember]
            public string PortNo { get; set; }
            [DataMember]
            public string Protocol { get; set; }
            [DataMember]
            public int Refnum { get; set; }
            [DataMember]
            public int Regnum { get; set; }
            [DataMember]
            public int Rencnt { get; set; }
            [DataMember]
            public int Reqnum { get; set; }
            [DataMember]
            public int RetItCnt { get; set; }
            [DataMember]
            public string RetItFlg { get; set; }
            [DataMember]
            public string SystemName { get; set; }
            [DataMember]
            public string Taxtype { get; set; }
            [DataMember]
            public bool TileOutlet { get; set; }
            [DataMember]
            public bool TilePermit { get; set; }
            [DataMember]
            public bool TileTin { get; set; }
            [DataMember]
            public string Title { get; set; }
            [DataMember]
            public string Type { get; set; }
            [DataMember]
            public bool UpdregOutflag { get; set; }
            [DataMember]
            public string VatConfFlg { get; set; }
            [DataMember]
            public string VatDtFlg { get; set; }
            [DataMember]
            public string VtepFg { get; set; }
            [DataMember]
            public string VtiaSignFg { get; set; }
            [DataMember]
            public string WarDtFlg { get; set; }
            [DataMember]
            public int Zfillingoblig { get; set; }
            [DataMember]
            public string Zregstatus { get; set; }
            [DataMember]
            public string Zuser { get; set; }
            [DataMember]
            [JsonProperty("lists")]
            public List<Result> ListSet { get; set; }
            [DataMember]
            public AuthServSet AuthServSet { get; set; }
        }
    }

    public class ZakatObjectionRequestSummaryModel
    {
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
            [DataMember]
            public Metadata2 __metadata { get; set; }
            [DataMember]
            [JsonProperty("formBundleType")]
            public string AFbtyp { get; set; }
            [DataMember]
            [JsonProperty("CR121GoLive")]
            public string ACr121gldtfg { get; set; }
            [DataMember]
            [JsonProperty("calendarType")]
            public string ACalTyp { get; set; }
            [DataMember]

            public string APenaltyTyp { get; set; }
            [DataMember]
            [JsonProperty("referenceAssessment")]
            public string ARefNo { get; set; }
            [DataMember]
            [JsonProperty("sadadNumber")]
            public string ASopbel { get; set; }
            [DataMember]
            [JsonProperty("taxYear")]
            public string AAssnmtYr { get; set; }
            [DataMember]
            [JsonProperty("currency")]
            public string Waers { get; set; }
            [DataMember]
            [JsonProperty("periodFrom")]
            public DateTime? APeriodFrom { get; set; }
            [DataMember]
            [JsonProperty("taxType")]
            public string ATaxTy { get; set; }
            [DataMember]
            [JsonProperty("aCurrency")]
            public string ACurr { get; set; }
            [DataMember]
            [JsonProperty("assessedAmount")]
            public string AAssnmtAmt { get; set; }
            [DataMember]
            [JsonProperty("revisedAmount")]
            public string ARevAmt { get; set; }
            [DataMember]
            [JsonProperty("disputeAmount")]
            public string ADisputeAmt { get; set; }
            [DataMember]
            [JsonProperty("periodTo")]
            public DateTime? APeriodTo { get; set; }
            [DataMember]
            [JsonProperty("selectedRow")]
            public string ASelect { get; set; }
            [DataMember]
            [JsonProperty("provideReturnDetails")]
            public string ARetDet { get; set; }
            //CR4912
            [DataMember]
            public string ADisputeAmtCit { get; set; }
            [DataMember]
            public string ADisAmtZkt10 { get; set; }
            [DataMember]
            public string ARevAmtCit { get; set; }
        }

        public class ZNOBObjSet
        {
            [DataMember]
            public List<Result> results { get; set; }
        }

        public class OffNotesSet
        {
            [DataMember]
            public List<object> results { get; set; }
        }

        public class ZnotesSet
        {
            [DataMember]
            public List<Result4> results { get; set; }
        }

        public class Result4
        {
            [DataMember]
            public Metadata __metadata { get; set; }
            [DataMember]
            [JsonProperty("noteNumber")]
            public string Notenoz { get; set; }
            [DataMember]
            [JsonProperty("referenceName")]
            public string Refnamez { get; set; }
            
            [DataMember]
            [JsonProperty("attachedByPerson")]
            public string AttByz { get; set; }
            
            [DataMember]
            [JsonProperty("lineNumber")]
            public int Lineno { get; set; }
            [DataMember]
            [JsonProperty("elementNumber")]
            public int ElemNo { get; set; }
           
        }

        public class Deferred
        {
            [DataMember]
            public string uri { get; set; }
        }

        public class ZNOBPenaltySet
        {
            [DataMember]
            public Deferred __deferred { get; set; }
        }

        public class Deferred2
        {
            [DataMember]
            public string uri { get; set; }
        }

        public class ZNOBYearAmtSet
        {
            [DataMember]
            public Deferred2 __deferred { get; set; }
        }

        public class Deferred3
        {
            [DataMember]
            public string uri { get; set; }
        }

        public class ZNOBINPAYSet
        {
            [DataMember]
            public Deferred3 __deferred { get; set; }
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
            [DataMember]
            public Metadata3 __metadata { get; set; }
            [DataMember]
            public string DataVersion { get; set; }
            [DataMember]
            public string DocUrl { get; set; }
            [DataMember]
            public string RetGuid { get; set; }
            [DataMember]
            public string Seqno { get; set; }
            [DataMember]
            public string SchGuid { get; set; }
            [DataMember]
            public string Dotyp { get; set; }
            [DataMember]
            public int Srno { get; set; }
            [DataMember]
            public string Doguid { get; set; }
            [DataMember]
            public string AttBy { get; set; }
            [DataMember]
            public string Filename { get; set; }
            [DataMember]
            public string FileExtn { get; set; }
            [DataMember]
            public string Mimetype { get; set; }
            [DataMember]
            public DateTime? Erfdt { get; set; }
            [DataMember]
            public string Erftm { get; set; }
            [DataMember]
            public string Enbedit { get; set; }
            [DataMember]
            public string Enbdele { get; set; }
            [DataMember]
            public string Visedit { get; set; }
            [DataMember]
            public string Visdel { get; set; }
        }

        public class AttDetSet
        {
            [DataMember]
            public List<Attachment> results { get; set; }
        }

        public class HeaderSet
        {
            [DataMember]
            [JsonProperty("address")]
            public string AAddress { get; set; }
            [DataMember]
            public string AChkbg1 { get; set; }
            [DataMember]
            [JsonProperty("totalZakatAmount")]
            public string ATotZkt { get; set; }
            [DataMember]
            [JsonProperty("totalCIT")]
            public string ATotCit { get; set; }
            [DataMember]

            public string AZsopbelCit { get; set; }
            [DataMember]
            public string AZundisam { get; set; }
            [DataMember]
            [JsonProperty("calendarType")]
            public string ACaltyp { get; set; }
            [DataMember]
            [JsonProperty("CITDisputeAmount")]
            public string ACitdisam { get; set; }
            [DataMember]
            [JsonProperty("taxpayerType")]
            public string ATpTyp { get; set; }
            [DataMember]
            [JsonProperty("CITUndisputedAmount")]
            public string ACitundisam { get; set; }
            [DataMember]
            [JsonProperty("zakatDisputeAmount")]
            public string AZdisam { get; set; }
            [DataMember]
            [JsonProperty("processingOperationNumber")]
            public string Operationz { get; set; }
            [DataMember]
            [JsonProperty("userType")]
            public string UserTypz { get; set; }
            [DataMember]
            [JsonProperty("agreed")]
            public string AAgree { get; set; }
            
            [DataMember]
            [JsonProperty("assessedAmount")]
            public string AAssnmtAmt { get; set; }
            [DataMember]
            [JsonProperty("taxYear")]
            public string AAssnmtYr { get; set; }
            [DataMember]
            [JsonProperty("bankDetails")]
            public string ABkext { get; set; }
            [DataMember]
            [JsonProperty("bankGuaranteeId")]
            public string ABnkid { get; set; }
           
            [DataMember]
            [JsonProperty("branch")]
            public string ABranch { get; set; }
            [DataMember]
            [JsonProperty("building")]
            public string ABulding { get; set; }
            
            [DataMember]
            [JsonProperty("capacity")]
            public string ACapacity { get; set; }
            [DataMember]
            [JsonProperty("code")]
            public string ACdNm { get; set; }
           
            [DataMember]
            [JsonProperty("city")]
            public string ACity { get; set; }
            [DataMember]
            [JsonProperty("companyName")]
            public string ACompNm { get; set; }
            [DataMember]
            [JsonProperty("CR121GoLive")]
            public string Acr121gldtfg { get; set; }
            [DataMember]
            [JsonProperty("currency")]
            public string ACurr { get; set; }
            [DataMember]
            [JsonProperty("disputeAmount")]
            public string ADisputeAmt { get; set; }
            [DataMember]
            [JsonProperty("district")]
            public string ADistrict { get; set; }
            [DataMember]
            [JsonProperty("email")]
            public string AEmail { get; set; }
            
            [DataMember]
            [JsonProperty("exactDay")]
            public string AExtdy { get; set; }
            
            [DataMember]
            [JsonProperty("faxNumber")]
            public string AFaxNo { get; set; }
            
            [DataMember]
            [JsonProperty("AGactn")]
            public string AGactn { get; set; }
            [DataMember]
            [JsonProperty("goLiveCheck")]
            public string AGoliveChk { get; set; }
            [DataMember]
            [JsonProperty("goLiveDate")]
            public string Agolivedtfg { get; set; }
            [DataMember]
            [JsonProperty("instructions")]
            public string AInstr { get; set; }
            [DataMember]
            [JsonProperty("legalStatus")]
            public string ALegalSt { get; set; }
            [DataMember]
            [JsonProperty("mainActivityCode")]
            public string AMainAct { get; set; }
            [DataMember]
            [JsonProperty("mainActivityDescription")]
            public string AMainActDesc { get; set; }
            [DataMember]
            [JsonProperty("amendmentReason")]
            public string AmdRsnz { get; set; }
            [DataMember]
            [JsonProperty("name")]
            public string AName { get; set; }
            [DataMember]
            [JsonProperty("financialNumber")]
            public string ANoFinance { get; set; }
            
            [DataMember]
            [JsonProperty("objectionPenalty")]
            public string AObjIntPenalty { get; set; }
            [DataMember]
            [JsonProperty("objectionReturn")]
            public string AObjReturn { get; set; }
            [DataMember]
            [JsonProperty("objectionSummary")]
            public string AObjSum { get; set; }
            [DataMember]
            [JsonProperty("otherAttachments")]
            public string AOthAttch { get; set; }
            
            [DataMember]
            [JsonProperty("PoBox")]
            public string APoBox { get; set; }
            [DataMember]
            [JsonProperty("approvalAction")]
            public string Approvez { get; set; }
            [DataMember]
            [JsonProperty("receivedBy")]
            public string ARecByOff { get; set; }
            
            [DataMember]
            [JsonProperty("referenceNumber")]
            public string ARefNo { get; set; }
            [DataMember]
            [JsonProperty("representativeBuildName")]
            public string ARepBldNm { get; set; }
            [DataMember]
            [JsonProperty("representativeCity")]
            public string ARepCity { get; set; }
            [DataMember]
            [JsonProperty("representaiveDesignation")]
            public string ARepDes { get; set; }
            [DataMember]
            [JsonProperty("representativeEmail")]
            public string ARepEmail { get; set; }
            [DataMember]
            [JsonProperty("representativeFax")]
            public string ARepFax { get; set; }
            [DataMember]
            [JsonProperty("representativeName")]
            public string ARepName { get; set; }
            [DataMember]
            [JsonProperty("representativePhoneNumber")]
            public string ARepPhone { get; set; }
            [DataMember]
            [JsonProperty("representativeStreetNumber")]
            public string ARepSteetNo { get; set; }
            [DataMember]
            [JsonProperty("residency")]
            public string AResidency { get; set; }
            [DataMember]
            [JsonProperty("returnDetails")]
            public string ARetDet { get; set; }
            [DataMember]
            [JsonProperty("revisedAmount")]
            public string ARevAmt { get; set; }
            [DataMember]
            [JsonProperty("securityAmount")]
            public string ASecam { get; set; }
            [DataMember]
            public string ASectp { get; set; }
            
            [DataMember]
            [JsonProperty("sadadNumber")]
            public string ASopbel { get; set; }
            [DataMember]
            [JsonProperty("step")]
            public int AStep { get; set; }
            [DataMember]
            [JsonProperty("street")]
            public string AStreet { get; set; }
           
            [DataMember]
            [JsonProperty("TaxpayerofficeOff")]
            public string ATaxOfOff { get; set; }
            [DataMember]
            [JsonProperty("TaxpayerType")]
            public string ATaxTy { get; set; }
            [DataMember]
            [JsonProperty("telephoneNumber")]
            public string ATelephone { get; set; }
            [DataMember]
            [JsonProperty("TIN")]
            public string ATin { get; set; }
            [DataMember]
            [JsonProperty("TINCountry")]
            public string ATinCountry { get; set; }
            [DataMember]
            [JsonProperty("TINOff")]
            public string ATinOff { get; set; }
            
            [DataMember]
            [JsonProperty("taxpayerName")]
            public string ATpNm { get; set; }
            [DataMember]
            [JsonProperty("transactionType")]
            public string ATransactionType { get; set; }
            [DataMember]
            [JsonProperty("auditor")]
            public string Auditorz { get; set; }
            [DataMember]
            [JsonProperty("zipCode")]
            public string AZipCd { get; set; }
            [DataMember]
            [JsonProperty("sadadBill")]
            public string AZsopbel { get; set; }
            [DataMember]
            [JsonProperty("calendarPeriod")]
            public string Cal { get; set; }
            [DataMember]
            [JsonProperty("caseGUID")]
            public string CaseGuid { get; set; }
            [DataMember]
            [JsonProperty("createTaxpayerAssesment")]
            public string CreateTxAssesz { get; set; }
            
            [DataMember]
            [JsonProperty("authenticationUser")]
            public string Euser { get; set; }
            [DataMember]
            [JsonProperty("formBundleGUID")]
            public string Fbguid { get; set; }
            [DataMember]
            [JsonProperty("formBundleNumber")]
            public string Fbnum { get; set; }
            
            [DataMember]
            [JsonProperty("formId")]
            public string FormId { get; set; }
            [DataMember]
            [JsonProperty("formType")]
            public string Formtype { get; set; }
            
            [DataMember]
            [JsonProperty("language")]
            public string Langz { get; set; }
            [DataMember]
            [JsonProperty("line0")]
            public string Line0 { get; set; }
            [DataMember]
            [JsonProperty("line1")]
            public string Line1 { get; set; }
            [DataMember]
            [JsonProperty("line2")]
            public string Line2 { get; set; }
            [DataMember]
            [JsonProperty("line3")]
            public string Line3 { get; set; }
            [DataMember]
            [JsonProperty("line4")]
            public string Line4 { get; set; }
            [DataMember]
            [JsonProperty("line5")]
            public string Line5 { get; set; }
            [DataMember]
            [JsonProperty("line6")]
            public string Line6 { get; set; }
            [DataMember]
            [JsonProperty("line7")]
            public string Line7 { get; set; }
            [DataMember]
            [JsonProperty("line8")]
            public string Line8 { get; set; }
            [DataMember]
            [JsonProperty("line9")]
            public string Line9 { get; set; }
            
            [DataMember]
            [JsonProperty("month")]
            public string Monthz { get; set; }
            
            [DataMember]
            [JsonProperty("periodKey")]
            public string PeriodKeyz { get; set; }
            [DataMember]
            [JsonProperty("portalUser")]
            public string PortalUsrz { get; set; }
            [DataMember]
            [JsonProperty("rejectionId")]
            public string RegIdz { get; set; }
            [DataMember]
            [JsonProperty("rejectionAction")]
            public string Rejectz { get; set; }
           
            [DataMember]
            [JsonProperty("save")]
            public string Savez { get; set; }
            [DataMember]
            [JsonProperty("savedNote")]
            public string SavNot { get; set; }
            [DataMember]
            [JsonProperty("status")]
            public string Status { get; set; }
            [DataMember]
            [JsonProperty("submit")]
            public string Submitz { get; set; }
            [DataMember]
            [JsonProperty("taxpayer")]
            public string Taxpayerz { get; set; }
            [DataMember]
            [JsonProperty("void")]
            public string Xvoidz { get; set; }
        }

        [Serializable]
        [DataContract]

        public class D
        {
            [DataMember]
            public Metadata __metadata { get; set; }
            [DataMember]
            public HeaderSet headerSet { get; set; }

            [DataMember]
            [JsonProperty("objectionSet")]
            public List<Result> ZNOB_ObjSet { get; set; }
            
            [DataMember]
            [JsonProperty("attachedDetailsSet")]
            public List<Attachment> AttDetSet { get; set; }
            [DataMember]
            [JsonProperty("notesSet")]
            public List<Result4> znotesSet { get; set; }
        }
    }

    public class ZAKATObjectionReturnModel
    {

        public class ZAKATObjectionReviewReturnModel
        {
            [DataMember]
            public string Agree { get; set; }
            [DataMember]
            public string TaxPayerName { get; set; }
            [DataMember]
            public string Branch { get; set; }
            [DataMember]
            public string Address { get; set; }
            [DataMember]
            public string ElectronicMail { get; set; }
            [DataMember]
            public string TelephoneNo { get; set; }
            [DataMember]
            public string FaxNo { get; set; }
            [DataMember]
            public string RegerenceNo { get; set; }
            [DataMember]
            public string AssessmentYear { get; set; }
            [DataMember]
            public DateTime? PeriodFrom { get; set; }
            [DataMember]
            public DateTime? PeriodTo { get; set; }
            [DataMember]
            public string TaxType { get; set; }
            [DataMember]
            public string Currency { get; set; }
            [DataMember]
            public string AssessmentAmountGAZT { get; set; }
            [DataMember]
            public string RevisedAmount { get; set; }
            [DataMember]
            public string DisputeAmount { get; set; }
            [DataMember]
            public string ReturnDetails { get; set; }
            [DataMember]
            public string ObjectionReasons { get; set; }
            [DataMember]
            public string PaymentAmount { get; set; }
            [DataMember]
            public string PaymentMethod { get; set; }
            [DataMember]
            public string AckSADADPayment { get; set; }
            [DataMember]
            public string AckBankGuarantee { get; set; }
            [DataMember]
            public string ACKBG1 { get; set; }
            [DataMember]
            public string ACKBG2 { get; set; }
            [DataMember]
            public string BGAttachmentName { get; set; }
            [DataMember]
            public string BGAttachmentURL { get; set; }
            [DataMember]
            public string ZAKATSADADInvoiceNumber { get; set; }
            [DataMember]
            public string TotalZAKATPayableAmount { get; set; }
            [DataMember]
            public string CITSADADInvoiceNumber { get; set; }
            [DataMember]
            public string TotalCITPayableAmount { get; set; }
            [DataMember]
            public string DisputedAmount { get; set; }
            [DataMember]
            public string UnDisputedAmount { get; set; }
            [DataMember]
            public string SADADGAZTID { get; set; }
            [DataMember]
            public string DisputedZAKATAmount { get; set; }
            [DataMember]
            public double QuarterDisputedZAKATAmount { get; set; }
            [DataMember]
            public string UnDisputedZAKATAmount { get; set; }
            [DataMember]
            public string DisputedCITAmount { get; set; }
            [DataMember]
            public string UnDisputedCITAmount { get; set; }
            [DataMember]
            public string Reason { get; set; }
            [DataMember]
            public string BankGuaranteeID { get; set; }
            [DataMember]
            public string CalendarType { get; set; }
            [DataMember]
            public DateTime? ValidFrom { get; set; }
            [DataMember]
            public DateTime? ValidTo { get; set; }
            [DataMember]
            public string BankName { get; set; }
            [DataMember]
            public string TotalPaymentAmount { get; set; }
            [DataMember]
            public string TaxOfficerComments { get; set; }
            [DataMember]

            public string AttachmentName { get; set; }
            [DataMember]
            public string AttachmentURL { get; set; }
            [DataMember]

            public string BGAttachment_2_Name { get; set; }
            [DataMember]
            public string BGAttachment_2_URL { get; set; }
            [DataMember]
            public string RepFullName { get; set; }
            [DataMember]
            public string RepPhoneNo { get; set; }
            [DataMember]
            public string RepFaxNo { get; set; }
            [DataMember]
            public string RepElectronicMail { get; set; }
            [DataMember]
            public string RepDesignation { get; set; }
            [DataMember]
            public string RepBuildingName { get; set; }
            [DataMember]
            public string RepLevelStreetNumber { get; set; }
            [DataMember]
            public string RepCity { get; set; }
            [DataMember]
            public string ApplicantName { get; set; }
            [DataMember]
            public string Capacity { get; set; }
        }
    }
}
