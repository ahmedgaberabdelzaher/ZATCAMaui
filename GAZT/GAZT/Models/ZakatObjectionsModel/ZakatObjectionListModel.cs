using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Xamarin.Forms.Internals;

namespace EGAZT.Models.ZakatObjectionsModel
{
    [Preserve(AllMembers = true)]
    public class ZakatObjectionListModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public D d { get; set; }
        [Preserve(AllMembers = true)]
        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class Metadata2
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class Result
        {
            public Metadata2 __metadata { get; set; }
            public string Fbnum { get; set; }
            public string Objstatus { get; set; }
            public string Fbsta { get; set; }
            public string StatText { get; set; }
            public string Fbtyp { get; set; }
            public string FbtText { get; set; }
            public string Erfdate { get; set; }
            public string Erftime { get; set; }
            public string Persl { get; set; }
            public string TaxPeriod { get; set; }
            public object DueDt { get; set; }
            public string Due { get; set; }
            public object Abrzu { get; set; }
            public object Abrzo { get; set; }
            public string Incotyp { get; set; }
            public string Incotext { get; set; }
            public string Flag { get; set; }
            public string CalendrTyp { get; set; }
            public string PrcBy { get; set; }
            public string Grp { get; set; }
            public string CrdtText { get; set; }
            public string Euser { get; set; }
            public string Fbguid { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class ListSet
        {
            [DataMember]
            public List<Result> results { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class Deferred
        {
            public string uri { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class AuthServSet
        {
            public Deferred __deferred { get; set; }
        }
        [Serializable]
        [DataContract]
        [Preserve(AllMembers = true)]
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
            public ListSet ListSet { get; set; }
            [DataMember]
            public AuthServSet AuthServSet { get; set; }
        }
    }
    [Preserve(AllMembers = true)]
    public class ZakatObjectionRequestSummaryModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public D d { get; set; }
        [Preserve(AllMembers = true)]
        public class Metadata
        {
            [DataMember]
            public string id { get; set; }
            [DataMember]
            public string uri { get; set; }
            [DataMember]
            public string type { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class Metadata2
        {
            [DataMember]
            public string id { get; set; }
            [DataMember]
            public string uri { get; set; }
            [DataMember]
            public string type { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class Result
        {
            [DataMember]
            public Metadata2 __metadata { get; set; }
            [DataMember]
            public string AFbtyp { get; set; }
            [DataMember]
            public string ACr121gldtfg { get; set; }
            [DataMember]
            public string ACalTyp { get; set; }
            [DataMember]
            public string APenaltyTyp { get; set; }
            [DataMember]
            public string ARefNo { get; set; }
            [DataMember]
            public string ASopbel { get; set; }
            [DataMember]
            public string AAssnmtYr { get; set; }
            [DataMember]
            public string Waers { get; set; }
            [DataMember]
            public DateTime? APeriodFrom { get; set; }
            [DataMember]
            public string ATaxTy { get; set; }
            [DataMember]
            public string ACurr { get; set; }
            [DataMember]
            public string AAssnmtAmt { get; set; }
            [DataMember]
            public string ARevAmt { get; set; }
            [DataMember]
            public string ADisputeAmt { get; set; }
            [DataMember]
            public DateTime? APeriodTo { get; set; }
            [DataMember]
            public string ASelect { get; set; }
            [DataMember]
            public string ARetDet { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class ZNOBObjSet
        {
            [DataMember]
            public List<Result> results { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class OffNotesSet
        {
            [DataMember]
            public List<object> results { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class ZnotesSet
        {
            [DataMember]
            public List<Result4> results { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class Result4
        {
            public Metadata __metadata { get; set; }
            public string Notenoz { get; set; }
            public string Refnamez { get; set; }
            public string XInvoicez { get; set; }
            public string XObsoletez { get; set; }
            public string Rcodez { get; set; }
            public string Erfusrz { get; set; }
            public DateTime? Erfdtz { get; set; }
            public string Erftmz { get; set; }
            public string AttByz { get; set; }
            public string Noteno { get; set; }
            public int Lineno { get; set; }
            public int ElemNo { get; set; }
            public string Tdformat { get; set; }
            public string Tdline { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class Deferred
        {
            [DataMember]
            public string uri { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class ZNOBPenaltySet
        {
            [DataMember]
            public Deferred __deferred { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class Deferred2
        {
            [DataMember]
            public string uri { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class ZNOBYearAmtSet
        {
            [DataMember]
            public Deferred2 __deferred { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class Deferred3
        {
            [DataMember]
            public string uri { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class ZNOBINPAYSet
        {
            [DataMember]
            public Deferred3 __deferred { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class Metadata3
        {
            [DataMember]
            public string id { get; set; }
            [DataMember]
            public string uri { get; set; }
            [DataMember]
            public string type { get; set; }
        }
        [Preserve(AllMembers = true)]
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
        [Preserve(AllMembers = true)]
        public class AttDetSet
        {
            public List<Attachment> results { get; set; }
        }
        [Serializable]
        [DataContract]
        [Preserve(AllMembers = true)]
        public class D
        {
            [DataMember]
            public Metadata __metadata { get; set; }
            [DataMember]
            public string AAddress { get; set; }
            [DataMember]
            public string AChkbg1 { get; set; }
            [DataMember]
            public string ATotZkt { get; set; }
            [DataMember]
            public string ATotCit { get; set; }
            [DataMember]
            public string AZsopbelCit { get; set; }
            [DataMember]
            public string AZundisam { get; set; }
            [DataMember]
            public string ACaltyp { get; set; }
            [DataMember]
            public string ACitdisam { get; set; }
            [DataMember]
            public string ATpTyp { get; set; }
            [DataMember]
            public string ACitundisam { get; set; }
            [DataMember]
            public string AZdisam { get; set; }
            [DataMember]
            public string Operationz { get; set; }
            [DataMember]
            public string UserTypz { get; set; }
            [DataMember]
            public string AAgree { get; set; }
            [DataMember]
            public string AAppNoOff { get; set; }
            [DataMember]
            public string AAssnmtAmt { get; set; }
            [DataMember]
            public string AAssnmtYr { get; set; }
            [DataMember]
            public string ABkext { get; set; }
            [DataMember]
            public string ABnkid { get; set; }
            [DataMember]
            public DateTime? ABnvfr { get; set; }
            [DataMember]
            public DateTime? ABnvto { get; set; }
            [DataMember]
            public string ABranch { get; set; }
            [DataMember]
            public string ABulding { get; set; }
            [DataMember]
            public string ACalender { get; set; }
            [DataMember]
            public string ACapacity { get; set; }
            [DataMember]
            public string ACdNm { get; set; }
            [DataMember]
            public string AChkbg { get; set; }
            [DataMember]
            public string AChkcs { get; set; }
            [DataMember]
            public string ACity { get; set; }
            [DataMember]
            public string ACompNm { get; set; }
            [DataMember]
            public string Acr121gldtfg { get; set; }
            [DataMember]
            public string ACurr { get; set; }
            [DataMember]
            public string ADisputeAmt { get; set; }
            [DataMember]
            public string ADistrict { get; set; }
            [DataMember]
            public string AEmail { get; set; }
            [DataMember]
            public DateTime? AEndpr { get; set; }
            [DataMember]
            public string AExtdy { get; set; }
            [DataMember]
            public string AExtfg { get; set; }
            [DataMember]
            public string AFaxNo { get; set; }
            [DataMember]
            public DateTime? AFromDt { get; set; }
            [DataMember]
            public string AGactn { get; set; }
            [DataMember]
            public string AGoliveChk { get; set; }
            [DataMember]
            public string Agolivedtfg { get; set; }
            [DataMember]
            public string AInstr { get; set; }
            [DataMember]
            public string ALegalSt { get; set; }
            [DataMember]
            public string AMainAct { get; set; }
            [DataMember]
            public string AMainActDesc { get; set; }
            [DataMember]
            public string AmdRsnz { get; set; }
            [DataMember]
            public string AName { get; set; }
            [DataMember]
            public string ANoFinance { get; set; }
            [DataMember]
            public string AObjectOn { get; set; }
            [DataMember]
            public string AObjIntPenalty { get; set; }
            [DataMember]
            public string AObjReturn { get; set; }
            [DataMember]
            public string AObjSum { get; set; }
            [DataMember]
            public string AOthAttch { get; set; }
            [DataMember]
            public string APenaltyTyp { get; set; }
            [DataMember]
            public DateTime? APeriodFrom { get; set; }
            [DataMember]
            public string APeriodFromCh { get; set; }
            [DataMember]
            public DateTime? APeriodTo { get; set; }
            [DataMember]
            public string APeriodToCh { get; set; }
            [DataMember]
            public string APoBox { get; set; }
            [DataMember]
            public string Approvez { get; set; }
            [DataMember]
            public string ARecByOff { get; set; }
            [DataMember]
            public DateTime? ARecDtOff { get; set; }
            [DataMember]
            public string ARecDtOffCh { get; set; }
            [DataMember]
            public string ARefNo { get; set; }
            [DataMember]
            public string ARepBldNm { get; set; }
            [DataMember]
            public string ARepCity { get; set; }
            [DataMember]
            public string ARepDes { get; set; }
            [DataMember]
            public string ARepEmail { get; set; }
            [DataMember]
            public string ARepFax { get; set; }
            [DataMember]
            public string ARepName { get; set; }
            [DataMember]
            public string ARepPhone { get; set; }
            [DataMember]
            public string ARepSteetNo { get; set; }
            [DataMember]
            public string AResidency { get; set; }
            [DataMember]
            public string ARetDet { get; set; }
            [DataMember]
            public string ARevAmt { get; set; }
            [DataMember]
            public string ASecam { get; set; }
            [DataMember]
            public string ASectp { get; set; }
            [DataMember]
            public string ASelect { get; set; }
            [DataMember]
            public string ASitedocs { get; set; }
            [DataMember]
            public string ASopbel { get; set; }
            [DataMember]
            public int AStep { get; set; }
            [DataMember]
            public string AStreet { get; set; }
            [DataMember]
            public DateTime? ASubdt { get; set; }
            [DataMember]
            public string ASubmitDateCh { get; set; }
            [DataMember]
            public string ATaxOfOff { get; set; }
            [DataMember]
            public string ATaxTy { get; set; }
            [DataMember]
            public string ATelephone { get; set; }
            [DataMember]
            public string ATin { get; set; }
            [DataMember]
            public string ATinCountry { get; set; }
            [DataMember]
            public string ATinOff { get; set; }
            [DataMember]
            public DateTime? AToDt { get; set; }
            [DataMember]
            public string ATpNm { get; set; }
            [DataMember]
            public string ATransactionType { get; set; }
            [DataMember]
            public string Auditorz { get; set; }
            [DataMember]
            public string AZipCd { get; set; }
            [DataMember]
            public string AZsopbel { get; set; }
            [DataMember]
            public string Cal { get; set; }
            [DataMember]
            public string CaseGuid { get; set; }
            [DataMember]
            public string CreateTxAssesz { get; set; }
            [DataMember]
            public string Dmodez { get; set; }
            [DataMember]
            public string Euser { get; set; }
            [DataMember]
            public string Fbguid { get; set; }
            [DataMember]
            public string Fbnum { get; set; }
            [DataMember]
            public string Fbnumz { get; set; }
            [DataMember]
            public string FormId { get; set; }
            [DataMember]
            public string Formtype { get; set; }
            [DataMember]
            public string GliveFlg { get; set; }
            [DataMember]
            public string Langz { get; set; }
            [DataMember]
            public string Line0 { get; set; }
            [DataMember]
            public string Line1 { get; set; }
            [DataMember]
            public string Line2 { get; set; }
            [DataMember]
            public string Line3 { get; set; }
            [DataMember]
            public string Line4 { get; set; }
            [DataMember]
            public string Line5 { get; set; }
            [DataMember]
            public string Line6 { get; set; }
            [DataMember]
            public string Line7 { get; set; }
            [DataMember]
            public string Line8 { get; set; }
            [DataMember]
            public string Line9 { get; set; }
            [DataMember]
            public string Mode { get; set; }
            [DataMember]
            public string Monthz { get; set; }
            [DataMember]
            public string OfficerUidz { get; set; }
            [DataMember]
            public string PeriodKeyz { get; set; }
            [DataMember]
            public string PortalUsrz { get; set; }
            [DataMember]
            public string RegIdz { get; set; }
            [DataMember]
            public string Rejectz { get; set; }
            [DataMember]
            public string Retguid { get; set; }
            [DataMember]
            public string Savez { get; set; }
            [DataMember]
            public string SavNot { get; set; }
            [DataMember]
            public string Status { get; set; }
            [DataMember]
            public string Submitz { get; set; }
            [DataMember]
            public string Taxpayerz { get; set; }
            [DataMember]
            public string Xvoidz { get; set; }
            [DataMember]
            public ZNOBObjSet ZNOB_ObjSet { get; set; }
            [DataMember]
            public OffNotesSet Off_notesSet { get; set; }
            [DataMember]
            public ZNOBPenaltySet ZNOB_penaltySet { get; set; }
            [DataMember]
            public ZNOBYearAmtSet ZNOB_YearAmtSet { get; set; }
            [DataMember]
            public ZNOBINPAYSet ZNOB_INPAYSet { get; set; }
            [DataMember]
            public AttDetSet AttDetSet { get; set; }
            [DataMember]
            public ZnotesSet znotesSet { get; set; }
        }
    }
    [Preserve(AllMembers = true)]
    public class ZAKATObjectionReturnModel
    {
        [Preserve(AllMembers = true)]
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
