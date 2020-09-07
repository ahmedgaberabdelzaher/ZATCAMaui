using System;
using System.Collections.Generic;

namespace EGAZT.Models.VatReviewModel
{
    public class VATObjectionButtonFormModeModel
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

        public class Result2
        {
            public Metadata3 __metadata { get; set; }
            public string Fbtyp { get; set; }
            public string FbtText { get; set; }
            public string TrnTyp { get; set; }
            public string TrnTxt { get; set; }
        }

        public class ReasonSet
        {
            public List<Result2> results { get; set; }
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
            public ReasonSet ReasonSet { get; set; }
        }

    }
    
    public class VATObjectionEnableSubmitModel
    {
        public D d { get; set; }
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }

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

     public  class VATObjectionFormModel
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
         // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
         public class Metadata
         {
             public string id { get; set; }
             public string uri { get; set; }
             public string type { get; set; }
         }

         public class D
         {
             public Metadata __metadata { get; set; }
             public object BnkValto { get; set; }
             public string Persl { get; set; }
             public string Secamt { get; set; }
             public bool Flag { get; set; }
             public string Fbnum { get; set; }
             public string FormGuid { get; set; }
             public object BnkValfm { get; set; }
             public string DataVersion { get; set; }
             public DateTime Abrzu { get; set; }
             public DateTime Abrzo { get; set; }
             public string Disamt { get; set; }
             public string Liaamt { get; set; }
             public string Sectp { get; set; }
             public string Bnkid { get; set; }
             public string Bkext { get; set; }
             public string Sopbel { get; set; }
             public string Security { get; set; }
             public string ChkCash { get; set; }
             public string ChkBank { get; set; }
             public string Perslt { get; set; }
             public string Gpart { get; set; }
         }

     }
     
     public class VATObjectionListModel
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
            public string FbtText { get; set; }
            public string Fbtyp { get; set; }
            public string UserErrFg { get; set; }
            public string SysFlg { get; set; }
            public DateTime? Ldate { get; set; }
        }

        public class REQTYPSet
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
            public DateTime? Receipt { get; set; }
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
            public List<Result3> results { get; set; }
        }

        public class D
        {
            public Metadata __metadata { get; set; }
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
            public REQTYPSet REQTYPSet { get; set; }
            public STATUSSet STATUSSet { get; set; }
            public ASSLISTSet ASSLISTSet { get; set; }
        }

    }

     public class VATObjectionRejectedFormModel
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

        public class AppRefNumResult
        {
            public Metadata2 __metadata { get; set; }
            public DateTime Abrzu { get; set; }
            public string Msgflg { get; set; }
            public string Pentyp { get; set; }
            public string Cokey { get; set; }
            public string Msgtxt { get; set; }
            public string Opbel { get; set; }
            public string Penamount { get; set; }
            public string CaseRef { get; set; }
            public int Zdays { get; set; }
            public string Clramt { get; set; }
            public string CaseGuid { get; set; }
            public string Periodicity { get; set; }
            public DateTime Abrzo { get; set; }
            public string DateFrm { get; set; }
            public string DateTo { get; set; }
            public string Persl { get; set; }
            public string Fbnum { get; set; }
            public string Liaamt { get; set; }
            public bool Del { get; set; }
            public string Fbtyp { get; set; }
            public string Fbsta { get; set; }
            public string Perslt { get; set; }
            public string Fbust { get; set; }
            public string Aenam { get; set; }
            public string TrnTyp { get; set; }
            public DateTime DecDt { get; set; }
        }

        public class RejectedFormSet
        {
            public List<AppRefNumResult> results { get; set; }
        }

        public class D
        {
            public Metadata __metadata { get; set; }
            public string Fbnumx { get; set; }
            public string Sopbel { get; set; }
            public string Langx { get; set; }
            public string Gpartx { get; set; }
            public string Fbustx { get; set; }
            public string Fbstax { get; set; }
            public string UserTypx { get; set; }
            public string TxnTpx { get; set; }
            public string Formprocx { get; set; }
            public string RvRsn { get; set; }
            public string RvSubRsn { get; set; }
            public RejectedFormSet RejectedFormSet { get; set; }
        }
    }
     
     public class VATObjectionSecurityAmountModel
     {
         public D d { get; set; }
         // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
         public class Metadata
         {
             public string id { get; set; }
             public string uri { get; set; }
             public string type { get; set; }
         }

         public class D
         {
             public Metadata __metadata { get; set; }
             public string Disamt { get; set; }
             public string Clramt { get; set; }
             public string Amttp { get; set; }
             public string Liaamt { get; set; }
             public string Secamt { get; set; }
         }
     }
     
     public class VATObjectionSummaryModel
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
            public string Pentyp { get; set; }
            public string ProcCd { get; set; }
            public string Fbtyp { get; set; }
            public string Code { get; set; }
            public string FbtText { get; set; }
            public string TypeT { get; set; }
            public string TrnTyp { get; set; }
            public string SubtypT { get; set; }
            public string TrnTxt { get; set; }
            public string DtFrmFlg { get; set; }
            public string DtToFlg { get; set; }
        }

        public class ReasonSet
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
            public string Addrnumber { get; set; }
            public string City { get; set; }
            public string Quarter { get; set; }
            public string PostalCd { get; set; }
            public string Street { get; set; }
            public string AdditionalNo { get; set; }
            public string BuildingNo { get; set; }
            public string Region { get; set; }
            public string RegionDesc { get; set; }
        }

        public class AddressSet
        {
            public List<Result2> results { get; set; }
        }

        public class NotesSet
        {
            public List<object> results { get; set; }
        }

        public class QuesListSet
        {
            public List<object> results { get; set; }
        }

        public class AttdetSet
        {
            public List<object> results { get; set; }
        }

        public class IdDetailSet
        {
            public List<object> results { get; set; }
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
            public string Fbtyp { get; set; }
            public string Pentyp { get; set; }
            public string FbtText { get; set; }
            public string TrnTyp { get; set; }
            public string TrnTxt { get; set; }
            public string ProcCd { get; set; }
            public string Code { get; set; }
            public string TypeT { get; set; }
            public string SubtypT { get; set; }
            public string DtFrmFlg { get; set; }
            public string DtToFlg { get; set; }
        }

        public class MainReasonSet
        {
            public List<Result3> results { get; set; }
        }

        public class Metadata5
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }

        public class SecurityDtl
        {
            public Metadata5 __metadata { get; set; }
            public string Rvdsc { get; set; }
            public string Amttp { get; set; }
            public string PenamtI { get; set; }
            public string SeczeroFlg { get; set; }
            public string PenamtR { get; set; }
            public string Opbel { get; set; }
            public string Penamount { get; set; }
            public string CaseRef { get; set; }
            public string Pentp { get; set; }
            public string Crpnl { get; set; }
            public string Penamt { get; set; }
            public string Lfpnl { get; set; }
            public string Vatamt { get; set; }
            public string Clramt { get; set; }
            public string Dueamt { get; set; }
            public string FormGuid { get; set; }
            public string Secamt { get; set; }
            public string Gpart { get; set; }
            public string Security { get; set; }
            public string ChkCash { get; set; }
            public string DataVersion { get; set; }
            public object Abrzu { get; set; }
            public string ChkBank { get; set; }
            public object Abrzo { get; set; }
            public string Disamt { get; set; }
            public string Liaamt { get; set; }
            public string Sectp { get; set; }
            public string Bnkid { get; set; }
            public object BnkValto { get; set; }
            public object BnkValfm { get; set; }
            public string Bkext { get; set; }
            public string Sopbel { get; set; }
            public string Perslt { get; set; }
        }

        public class D
        {
            public Metadata __metadata { get; set; }
            public string Skipst5covid19 { get; set; }
            public string Actnm { get; set; }
            public string Golivefg { get; set; }
            public object DecDt { get; set; }
            public string Appfg { get; set; }
            public string Persl { get; set; }
            public string Actno { get; set; }
            public bool AgreeFg { get; set; }
            public string Branchx { get; set; }
            public string CalTyp { get; set; }
            public string CrNo { get; set; }
            public string DataVersion { get; set; }
            public object DateFrm { get; set; }
            public object DateFrmOld { get; set; }
            public object DateTo { get; set; }
            public object DateToOld { get; set; }
            public string PeriodKey { get; set; }
            public bool DecFlg1 { get; set; }
            public bool DecFlg2 { get; set; }
            public string DecIdNo { get; set; }
            public object Declarationdt { get; set; }
            public string Decnm { get; set; }
            public string Euserx { get; set; }
            public string Evstatus { get; set; }
            public string Fbnumx { get; set; }
            public string Fbstax { get; set; }
            public string Fbustx { get; set; }
            public string FormGuid { get; set; }
            public string Formprocx { get; set; }
            public string Forwardx { get; set; }
            public string FullName { get; set; }
            public string Gpartx { get; set; }
            public string Iban { get; set; }
            public string IdType { get; set; }
            public string Langx { get; set; }
            public string Officerx { get; set; }
            public string Operationx { get; set; }
            public string PortalUsrx { get; set; }
            public string RejFb { get; set; }
            public string ReturnIdx { get; set; }
            public string RvRsn { get; set; }
            public string RvSubRsn { get; set; }
            public string Srcidentifyx { get; set; }
            public string Statusx { get; set; }
            public string StepNumberx { get; set; }
            public string TxnTpx { get; set; }
            public string UserTypx { get; set; }
            public ReasonSet ReasonSet { get; set; }
            public AddressSet AddressSet { get; set; }
            public NotesSet NotesSet { get; set; }
            public QuesListSet QuesListSet { get; set; }
            public AttdetSet AttdetSet { get; set; }
            public IdDetailSet IdDetailSet { get; set; }
            public MainReasonSet MainReasonSet { get; set; }
            public SecurityDtl SecurityDtl { get; set; }
        }

    }
     
     public class VATObjectionValidateTaxpayerModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

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
             public string Perslt { get; set; }
             public string Opbel { get; set; }
             public string Vtre2 { get; set; }
             public string Hvorg { get; set; }
             public string Tvorg { get; set; }
             public DateTime Bldat { get; set; }
             public DateTime Studt { get; set; }
             public string Betrh { get; set; }
             public string Persl { get; set; }
             public DateTime Abrzu { get; set; }
             public DateTime Abrzo { get; set; }
             public string Desc { get; set; }
         }

         public class D
         {
             public List<Result> results { get; set; }
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
}