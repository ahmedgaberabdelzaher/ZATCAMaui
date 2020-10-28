using System;
using System.Collections.Generic;
using System.Text;

namespace EGAZT.Models.ZakatObjectionsModel
{
    public class ZakatObjectionListModel
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

        public class ListSet
        {
            public List<Result> results { get; set; }
        }

        public class Deferred
        {
            public string uri { get; set; }
        }

        public class AuthServSet
        {
            public Deferred __deferred { get; set; }
        }

        public class D
        {
            public Metadata __metadata { get; set; }
            public string CallServ { get; set; }
            public int Accnum { get; set; }
            public int Actcnt { get; set; }
            public string Auditor { get; set; }
            public bool AudObjection { get; set; }
            public bool AudRefund { get; set; }
            public bool AudRefundTrn { get; set; }
            public bool AudRequest { get; set; }
            public bool AudReturn { get; set; }
            public string Bpnum { get; set; }
            public string Branch { get; set; }
            public string Caltype { get; set; }
            public string Client { get; set; }
            public int Cnlcnt { get; set; }
            public int Corrnum { get; set; }
            public string Dept { get; set; }
            public bool DisSharetile { get; set; }
            public bool EnableInstPlan { get; set; }
            public bool EnableTile { get; set; }
            public string Ettr { get; set; }
            public string Euser { get; set; }
            public string Euser1 { get; set; }
            public string Euser2 { get; set; }
            public string Euser3 { get; set; }
            public string Euser4 { get; set; }
            public string Euser5 { get; set; }
            public string ExeAppFlg { get; set; }
            public string ExeDtFlg { get; set; }
            public string Fbguid { get; set; }
            public string HostName { get; set; }
            public int Indcorrnum { get; set; }
            public string Interest { get; set; }
            public string IntPortal { get; set; }
            public bool IsBankruptcy { get; set; }
            public string Lang { get; set; }
            public string Name { get; set; }
            public bool NotifLogFlag { get; set; }
            public string NregDtFlg { get; set; }
            public int Oblnum { get; set; }
            public string Overdue { get; set; }
            public string Penalty { get; set; }
            public string PortNo { get; set; }
            public string Protocol { get; set; }
            public int Refnum { get; set; }
            public int Regnum { get; set; }
            public int Rencnt { get; set; }
            public int Reqnum { get; set; }
            public int RetItCnt { get; set; }
            public string RetItFlg { get; set; }
            public string SystemName { get; set; }
            public string Taxtype { get; set; }
            public bool TileOutlet { get; set; }
            public bool TilePermit { get; set; }
            public bool TileTin { get; set; }
            public string Title { get; set; }
            public string Type { get; set; }
            public bool UpdregOutflag { get; set; }
            public string VatConfFlg { get; set; }
            public string VatDtFlg { get; set; }
            public string VtepFg { get; set; }
            public string VtiaSignFg { get; set; }
            public string WarDtFlg { get; set; }
            public int Zfillingoblig { get; set; }
            public string Zregstatus { get; set; }
            public string Zuser { get; set; }
            public ListSet ListSet { get; set; }
            public AuthServSet AuthServSet { get; set; }
        }
    }

    public class ZakatObjectionRequestSummaryModel
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
            public string AFbtyp { get; set; }
            public string ACr121gldtfg { get; set; }
            public string ACalTyp { get; set; }
            public string APenaltyTyp { get; set; }
            public string ARefNo { get; set; }
            public string ASopbel { get; set; }
            public string AAssnmtYr { get; set; }
            public string Waers { get; set; }
            public DateTime? APeriodFrom { get; set; }
            public string ATaxTy { get; set; }
            public string ACurr { get; set; }
            public string AAssnmtAmt { get; set; }
            public string ARevAmt { get; set; }
            public string ADisputeAmt { get; set; }
            public DateTime? APeriodTo { get; set; }
            public string ASelect { get; set; }
            public string ARetDet { get; set; }
        }

        public class ZNOBObjSet
        {
            public List<Result> results { get; set; }
        }

        public class OffNotesSet
        {
            public List<object> results { get; set; }
        }
        public class ZnotesSet
        {
            public List<Result4> results { get; set; }
        }
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

        public class Deferred
        {
            public string uri { get; set; }
        }

        public class ZNOBPenaltySet
        {
            public Deferred __deferred { get; set; }
        }

        public class Deferred2
        {
            public string uri { get; set; }
        }

        public class ZNOBYearAmtSet
        {
            public Deferred2 __deferred { get; set; }
        }

        public class Deferred3
        {
            public string uri { get; set; }
        }

        public class ZNOBINPAYSet
        {
            public Deferred3 __deferred { get; set; }
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
            public string DataVersion { get; set; }
            public string DocUrl { get; set; }
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
            public DateTime? Erfdt { get; set; }
            public string Erftm { get; set; }
            public string Enbedit { get; set; }
            public string Enbdele { get; set; }
            public string Visedit { get; set; }
            public string Visdel { get; set; }
        }

        public class AttDetSet
        {
            public List<Attachment> results { get; set; }
        }

        public class D
        {
            public Metadata __metadata { get; set; }
            public string AAddress { get; set; }
            public string AChkbg1 { get; set; }
            public string ATotZkt { get; set; }
            public string ATotCit { get; set; }
            public string AZsopbelCit { get; set; }
            public string AZundisam { get; set; }
            public string ACaltyp { get; set; }
            public string ACitdisam { get; set; }
            public string ATpTyp { get; set; }
            public string ACitundisam { get; set; }
            public string AZdisam { get; set; }
            public string Operationz { get; set; }
            public string UserTypz { get; set; }
            public string AAgree { get; set; }
            public string AAppNoOff { get; set; }
            public string AAssnmtAmt { get; set; }
            public string AAssnmtYr { get; set; }
            public string ABkext { get; set; }
            public string ABnkid { get; set; }
            public DateTime? ABnvfr { get; set; }
            public DateTime? ABnvto { get; set; }
            public string ABranch { get; set; }
            public string ABulding { get; set; }
            public string ACalender { get; set; }
            public string ACapacity { get; set; }
            public string ACdNm { get; set; }
            public string AChkbg { get; set; }
            public string AChkcs { get; set; }
            public string ACity { get; set; }
            public string ACompNm { get; set; }
            public string Acr121gldtfg { get; set; }
            public string ACurr { get; set; }
            public string ADisputeAmt { get; set; }
            public string ADistrict { get; set; }
            public string AEmail { get; set; }
            public DateTime? AEndpr { get; set; }
            public string AExtdy { get; set; }
            public string AExtfg { get; set; }
            public string AFaxNo { get; set; }
            public DateTime? AFromDt { get; set; }
            public string AGactn { get; set; }
            public string AGoliveChk { get; set; }
            public string Agolivedtfg { get; set; }
            public string AInstr { get; set; }
            public string ALegalSt { get; set; }
            public string AMainAct { get; set; }
            public string AMainActDesc { get; set; }
            public string AmdRsnz { get; set; }
            public string AName { get; set; }
            public string ANoFinance { get; set; }
            public string AObjectOn { get; set; }
            public string AObjIntPenalty { get; set; }
            public string AObjReturn { get; set; }
            public string AObjSum { get; set; }
            public string AOthAttch { get; set; }
            public string APenaltyTyp { get; set; }
            public DateTime? APeriodFrom { get; set; }
            public string APeriodFromCh { get; set; }
            public DateTime? APeriodTo { get; set; }
            public string APeriodToCh { get; set; }
            public string APoBox { get; set; }
            public string Approvez { get; set; }
            public string ARecByOff { get; set; }
            public DateTime? ARecDtOff { get; set; }
            public string ARecDtOffCh { get; set; }
            public string ARefNo { get; set; }
            public string ARepBldNm { get; set; }
            public string ARepCity { get; set; }
            public string ARepDes { get; set; }
            public string ARepEmail { get; set; }
            public string ARepFax { get; set; }
            public string ARepName { get; set; }
            public string ARepPhone { get; set; }
            public string ARepSteetNo { get; set; }
            public string AResidency { get; set; }
            public string ARetDet { get; set; }
            public string ARevAmt { get; set; }
            public string ASecam { get; set; }
            public string ASectp { get; set; }
            public string ASelect { get; set; }
            public string ASitedocs { get; set; }
            public string ASopbel { get; set; }
            public int AStep { get; set; }
            public string AStreet { get; set; }
            public DateTime? ASubdt { get; set; }
            public string ASubmitDateCh { get; set; }
            public string ATaxOfOff { get; set; }
            public string ATaxTy { get; set; }
            public string ATelephone { get; set; }
            public string ATin { get; set; }
            public string ATinCountry { get; set; }
            public string ATinOff { get; set; }
            public DateTime? AToDt { get; set; }
            public string ATpNm { get; set; }
            public string ATransactionType { get; set; }
            public string Auditorz { get; set; }
            public string AZipCd { get; set; }
            public string AZsopbel { get; set; }
            public string Cal { get; set; }
            public string CaseGuid { get; set; }
            public string CreateTxAssesz { get; set; }
            public string Dmodez { get; set; }
            public string Euser { get; set; }
            public string Fbguid { get; set; }
            public string Fbnum { get; set; }
            public string Fbnumz { get; set; }
            public string FormId { get; set; }
            public string Formtype { get; set; }
            public string GliveFlg { get; set; }
            public string Langz { get; set; }
            public string Line0 { get; set; }
            public string Line1 { get; set; }
            public string Line2 { get; set; }
            public string Line3 { get; set; }
            public string Line4 { get; set; }
            public string Line5 { get; set; }
            public string Line6 { get; set; }
            public string Line7 { get; set; }
            public string Line8 { get; set; }
            public string Line9 { get; set; }
            public string Mode { get; set; }
            public string Monthz { get; set; }
            public string OfficerUidz { get; set; }
            public string PeriodKeyz { get; set; }
            public string PortalUsrz { get; set; }
            public string RegIdz { get; set; }
            public string Rejectz { get; set; }
            public string Retguid { get; set; }
            public string Savez { get; set; }
            public string SavNot { get; set; }
            public string Status { get; set; }
            public string Submitz { get; set; }
            public string Taxpayerz { get; set; }
            public string Xvoidz { get; set; }
            public ZNOBObjSet ZNOB_ObjSet { get; set; }
            public OffNotesSet Off_notesSet { get; set; }
            public ZNOBPenaltySet ZNOB_penaltySet { get; set; }
            public ZNOBYearAmtSet ZNOB_YearAmtSet { get; set; }
            public ZNOBINPAYSet ZNOB_INPAYSet { get; set; }
            public AttDetSet AttDetSet { get; set; }
            public ZnotesSet znotesSet { get; set; }
        }
    }

    public class ZAKATObjectionReturnModel
    {
        public class ZAKATObjectionReviewReturnModel
        {
            public string Agree { get; set; }
            public string TaxPayerName { get; set; }
            public string Branch { get; set; }
            public string Address { get; set; }
            public string ElectronicMail { get; set; }
            public string TelephoneNo { get; set; }
            public string FaxNo { get; set; }
            public string RegerenceNo { get; set; }
            public string AssessmentYear { get; set; }
            public DateTime? PeriodFrom { get; set; }
            public DateTime? PeriodTo { get; set; }
            public string TaxType { get; set; }
            public string Currency { get; set; }
            public string AssessmentAmountGAZT { get; set; }
            public string RevisedAmount { get; set; }
            public string DisputeAmount { get; set; }
            public string ReturnDetails { get; set; }
            public string ObjectionReasons { get; set; }
            public string PaymentAmount { get; set; }
            public string PaymentMethod { get; set; }
            public string AckSADADPayment { get; set; }
            public string AckBankGuarantee { get; set; }
            public string ACKBG1 { get; set; }
            public string ACKBG2 { get; set; }
            public string BGAttachmentName { get; set; }
            public string BGAttachmentURL { get; set; }
            public string ZAKATSADADInvoiceNumber { get; set; }
            public string TotalZAKATPayableAmount { get; set; }
            public string CITSADADInvoiceNumber { get; set; }
            public string TotalCITPayableAmount { get; set; }
            public string DisputedAmount { get; set; }
            public string UnDisputedAmount { get; set; }
            public string SADADGAZTID { get; set; }
            public string DisputedZAKATAmount { get; set; }
            public double QuarterDisputedZAKATAmount { get; set; }
            public string UnDisputedZAKATAmount { get; set; }
            public string DisputedCITAmount { get; set; }
            public string UnDisputedCITAmount { get; set; }
            public string Reason { get; set; }
            public string BankGuaranteeID { get; set; }
            public string CalendarType { get; set; }
            public DateTime? ValidFrom { get; set; }
            public DateTime? ValidTo { get; set; }
            public string BankName { get; set; }
            public string TotalPaymentAmount { get; set; }
            public string TaxOfficerComments { get; set; }

            public string AttachmentName { get; set; }
            public string AttachmentURL { get; set; }

            public string BGAttachment_2_Name { get; set; }
            public string BGAttachment_2_URL { get; set; }
            public string RepFullName { get; set; }
            public string RepPhoneNo { get; set; }
            public string RepFaxNo { get; set; }
            public string RepElectronicMail { get; set; }
            public string RepDesignation { get; set; }
            public string RepBuildingName { get; set; }
            public string RepLevelStreetNumber { get; set; }
            public string RepCity { get; set; }
            public string ApplicantName { get; set; }
            public string Capacity { get; set; }
        }
    }
}
