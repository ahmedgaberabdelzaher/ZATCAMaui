using System;
using System.Collections.Generic;

namespace EGAZT.Models.VatReviewModel
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
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

        public D d { get; set; }

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

    public class Metadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class VATObjectionListModel
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


        public class Metadata2
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }

        public class AppRefNumResult
        {
            public Metadata2 __metadata { get; set; }
            public DateTime? Abrzu { get; set; }
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
            public DateTime? Abrzo { get; set; }
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
            public DateTime? DecDt { get; set; }
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


    public class ReasonSetResult
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

    public class ReasonSet1
    {
        public List<ReasonSetResult> results { get; set; }
    }

    public class AddressSet1
    {
        public List<AddressResults> results { get; set; }
    }

    public class AddressResults
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

    public class QuesListSet1
    {
        public List<object> results { get; set; }
    }

    public class AttdetSet1
    {
        public List<Attachment> results { get; set; }
    }

    public class IdDetailSet
    {
        public List<object> results { get; set; }
    }

    public class NotesSet
    {
        public List<NotesSetResults> results { get; set; }
    }

    public partial class NotesSetResults
    {
        public Metadata __metadata { get; set; }
        public string Notenoz { get; set; }
        public string Refnamez { get; set; }
        public string DataVersionz { get; set; }
        public string XInvoicez { get; set; }
        public string XObsoletez { get; set; }
        public string Rcodez { get; set; }
        public string Erfusrz { get; set; }
        public object Erfdtz { get; set; }
        public object Erftmz { get; set; }
        public string ByGpartz { get; set; }
        public string AttByz { get; set; }
        public string Noteno { get; set; }
        public long Lineno { get; set; }
        public long ElemNo { get; set; }
        public string Tdformat { get; set; }
        public string Tdline { get; set; }
    }

    public partial class SecurityDtl
    {
        public Metadata __metadata { get; set; }
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
        public string Abrzu { get; set; }
        public string ChkBank { get; set; }
        public string Abrzo { get; set; }
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

    public class MainReasonSetResults
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
        public List<MainReasonSetResults> results { get; set; }
    }
    public class VATObjectionSummaryModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public D d { get; set; }


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
            public string Skipst5covid19 { get; set; }
            public string Actnm { get; set; }
            public string Golivefg { get; set; }
            public string DecDt { get; set; }
            public string Appfg { get; set; }
            public string Persl { get; set; }
            public string Actno { get; set; }
            public bool AgreeFg { get; set; }
            public string Branchx { get; set; }
            public string CalTyp { get; set; }
            public string CrNo { get; set; }
            public string DataVersion { get; set; }
            public string DateFrm { get; set; }
            public string DateFrmOld { get; set; }
            public string DateTo { get; set; }
            public string DateToOld { get; set; }
            public string PeriodKey { get; set; }
            public bool DecFlg1 { get; set; }
            public bool DecFlg2 { get; set; }
            public string DecIdNo { get; set; }
            public string Declarationdt { get; set; }
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
            public AddressSet1 AddressSet { get; set; }
            public NotesSet NotesSet { get; set; }
            public QuesListSet1 QuesListSet { get; set; }
            public AttdetSet1 AttdetSet { get; set; }
            public IdDetailSet IdDetailSet { get; set; }
            public MainReasonSet MainReasonSet { get; set; }
            public SecurityDtl SecurityDtl { get; set; }
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
        public D d { get; set; }


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

    public partial class VatObjectionsRequest
    {
        public Metadata __metadata { get; set; }
        public string Skipst5covid19 { get; set; }
        public string Actnm { get; set; }
        public string Golivefg { get; set; }
        public string DecDt { get; set; }
        public string Appfg { get; set; }
        public string Persl { get; set; }
        public string Actno { get; set; }
        public bool AgreeFg { get; set; }
        public string Branchx { get; set; }
        public string CalTyp { get; set; }
        public string CrNo { get; set; }
        public string DataVersion { get; set; }
        public string DateFrm { get; set; }
        public string DateFrmOld { get; set; }
        public string DateTo { get; set; }
        public string DateToOld { get; set; }
        public string PeriodKey { get; set; }
        public bool DecFlg1 { get; set; }
        public bool DecFlg2 { get; set; }
        public string DecIdNo { get; set; }
        public string Declarationdt { get; set; }
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
        public List<ReasonSetResult> ReasonSet { get; set; }
        public List<AddressResults> AddressSet { get; set; }
        public List<NotesSetResults> NotesSet { get; set; }
        public List<object> QuesListSet { get; set; }
        public List<Attachment> AttdetSet { get; set; }
        public List<object> IdDetailSet { get; set; }
        public List<MainReasonSetResults> MainReasonSet { get; set; }
        public SecurityDtl SecurityDtl { get; set; }
    }


    public class VATObjectionSummaryInputModel
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
            public object Abrzo { get; set; }
            public object Abrzu { get; set; }
            public object Aedat { get; set; }
            public bool AudFlag { get; set; }
            public string Auditor { get; set; }
            public object Begda { get; set; }
            public string Branch { get; set; }
            public string CalendrTyp { get; set; }
            public string Comb { get; set; }
            public string Dispflag { get; set; }
            public string Due { get; set; }
            public object DueDt { get; set; }
            public string DueDtC { get; set; }
            public object Endda { get; set; }
            public string Euser { get; set; }
            public string Euser1 { get; set; }
            public string Euser2 { get; set; }
            public string Euser3 { get; set; }
            public string Euser4 { get; set; }
            public string Euser5 { get; set; }
            public string Fbguid { get; set; }
            public string Fbnum { get; set; }
            public string FbtText { get; set; }
            public string Fbtyp { get; set; }
            public string Flag { get; set; }
            public string Gpart { get; set; }
            public string Incotext { get; set; }
            public string Incotyp { get; set; }
            public string InfoMsg { get; set; }
            public string Lang { get; set; }
            public string Monthz { get; set; }
            public string Msg { get; set; }
            public bool ObjFiled { get; set; }
            public string ObligFlag { get; set; }
            public bool Open { get; set; }
            public string Period { get; set; }
            public string Persl { get; set; }
            public bool RefundFiled { get; set; }
            public string SadadDoc1 { get; set; }
            public string SadadDoc2 { get; set; }
            public string Stat { get; set; }
            public string Statflag { get; set; }
            public string Status { get; set; }
            public string TaxPeriod { get; set; }
            public string Vtref { get; set; }
        }

        public class VATReviewRequestTPFVModel
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
                public string Caltp { get; set; }
                public bool ReviewFg { get; set; }
                public string Remark { get; set; }
                public string Fbguid { get; set; }
                public string Idno { get; set; }
                public string Mandtz { get; set; }
                public string Fbnumz { get; set; }
                public string PortalUsrz { get; set; }
                public string Langz { get; set; }
                public string Operationz { get; set; }
                public string StepNumberz { get; set; }
                public string ReturnIdz { get; set; }
                public string Officerz { get; set; }
                public string Gpartz { get; set; }
                public string Statusz { get; set; }
                public string UserTypz { get; set; }
                public string TxnTpz { get; set; }
                public string Formprocz { get; set; }
                public string OfficerTz { get; set; }
                public string SrcAppz { get; set; }
                public string Fbnum { get; set; }
                public string Gpart { get; set; }
                public string Inschk { get; set; }
                public DateTime Edtfr { get; set; }
                public DateTime Edtto { get; set; }
                public string Cptp { get; set; }
                public string Cpep { get; set; }
                public string Ctpp { get; set; }
                public string Cepp { get; set; }
                public string Pcptp { get; set; }
                public string Pcpep { get; set; }
                public string Pctpp { get; set; }
                public string Pcepp { get; set; }
                public DateTime Cdtfr { get; set; }
                public DateTime Cdtto { get; set; }
                public string Decchk1 { get; set; }
                public string Decchk2 { get; set; }
                public string Idtp { get; set; }
                public string Cnpr { get; set; }
                public string Trtp { get; set; }
                public string Stpno { get; set; }
                public string Mandt { get; set; }
                public string FormGuid { get; set; }
                public string DataVersion { get; set; }
                public int LineNo { get; set; }
                public string RankingOrder { get; set; }
                public string Euser { get; set; }
                public string DmodeFlg { get; set; }
                public string EvStatus { get; set; }
                public AttdetSet AttdetSet { get; set; }
                public NotesSet NotesSet { get; set; }
                public QUESLISTSet QUESLISTSet { get; set; }
                public QUESTIONSSet QUESTIONSSet { get; set; }
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

            public class Result2
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
                public List<Result4> results { get; set; }
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
                public DateTime? Abrzu { get; set; }
                public string Persl { get; set; }
                public string Txt50 { get; set; }
                public string EffDt { get; set; }
            }

            public class EFFDATESet
            {
                public List<Result5> results { get; set; }
            }

            public class D
            {
                public Metadata __metadata { get; set; }
                public string VatSupply { get; set; }
                public string VatPuchase { get; set; }
                public string UserTypz { get; set; }
                public string Txt50 { get; set; }
                public string TxnTpz { get; set; }
                public string TpDescr { get; set; }
                public bool SupplyFg { get; set; }
                public string Street { get; set; }
                public string StepNumberz { get; set; }
                public string StepNumber { get; set; }
                public string Statusz { get; set; }
                public string Srcidentifyz { get; set; }
                public string Srcidentify { get; set; }
                public bool ReviewFg { get; set; }
                public string ReturnIdz { get; set; }
                public string RegionDesc { get; set; }
                public string Region { get; set; }
                public DateTime RegDt { get; set; }
                public string Reason { get; set; }
                public string Quarter { get; set; }
                public bool PurchaseFg { get; set; }
                public string PostalCd { get; set; }
                public string PortalUsrz { get; set; }
                public string PersonTp { get; set; }
                public string Persl { get; set; }
                public bool Otheratt { get; set; }
                public string OtherTp { get; set; }
                public string Operationz { get; set; }
                public string Officerz { get; set; }
                public object OffRegDt { get; set; }
                public string OffPersl { get; set; }
                public bool Memoatt { get; set; }
                public string Mandt { get; set; }
                public string LicenseCrno { get; set; }
                public string Langz { get; set; }
                public bool ImporterFg { get; set; }
                public string Importer { get; set; }
                public string Gpart { get; set; }
                public string FullNm { get; set; }
                public string Formprocz { get; set; }
                public string FormGuid { get; set; }
                public string Fbnumz { get; set; }
                public string Fbguid { get; set; }
                public bool ExporterFg { get; set; }
                public string Exporter { get; set; }
                public string Euser { get; set; }
                public string Decname { get; set; }
                public string DecidTy { get; set; }
                public string DecidNo { get; set; }
                public string Decfg { get; set; }
                public string Decdesignation { get; set; }
                public object Decdate { get; set; }
                public string DataVersion { get; set; }
                public string City { get; set; }
                public string BuildingNo { get; set; }
                public bool Bothatt { get; set; }
                public bool Articleatt { get; set; }
                public string AgrFg { get; set; }
                public string AggreSupply { get; set; }
                public string AggrePurchase { get; set; }
                public string Addrnumber { get; set; }
                public string AdditionalNo { get; set; }
                public string Actnm { get; set; }
                public string Account { get; set; }
                public ELGBLDOCSet ELGBL_DOCSet { get; set; }
                public TABLESet TABLESet { get; set; }
                public NOTESSet NOTESSet { get; set; }
                public ATTDETSet ATTDETSet { get; set; }
                public QUESLISTSet QUESLISTSet { get; set; }
                public EFFDATESet EFFDATESet { get; set; }
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
                public string TxnTp { get; set; }
                public string Lang { get; set; }
                public string Reason { get; set; }
                public string Rdesc { get; set; }
            }

            public class D
            {
                public List<Result> results { get; set; }
            }


        }

        public class VATReviewDREGViewApplicationModel
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
                public string FormGuid { get; set; }
                public string DataVersion { get; set; }
                public int LineNo { get; set; }
                public string RankingOrder { get; set; }
                public string AddrType { get; set; }
                public string Srcidentify { get; set; }
                public DateTime Begda { get; set; }
                public DateTime Endda { get; set; }
                public string Gpart { get; set; }
                public string Street { get; set; }
                public string HouseNum1 { get; set; }
                public string HouseNum2 { get; set; }
                public string Building { get; set; }
                public string Floor { get; set; }
                public string PostCode1 { get; set; }
                public string City1 { get; set; }
                public string City2 { get; set; }
                public string Country { get; set; }
                public string Region { get; set; }
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

            public class D
            {
                public Metadata __metadata { get; set; }
                public bool CaseFg { get; set; }
                public string CaseReason { get; set; }
                public bool OpenCaseFg { get; set; }
                public bool ReviewFg { get; set; }
                public bool Agreeflg { get; set; }
                public bool Auditatt { get; set; }
                public bool Certatt { get; set; }
                public bool Chkdt { get; set; }
                public bool Contractatt { get; set; }
                public bool Declareflg { get; set; }
                public bool Declareflg2 { get; set; }
                public bool Incomatt { get; set; }
                public bool Incstmtatt { get; set; }
                public bool Insatt { get; set; }
                public bool Legatt { get; set; }
                public bool Otheratt { get; set; }
                public DateTime? Dregdt { get; set; }
                public DateTime? Duedate { get; set; }
                public object SuspDtfrom { get; set; }
                public object SuspDtto { get; set; }
                public object NextDtfrom { get; set; }
                public object NextDtto { get; set; }
                public DateTime? Declaredt { get; set; }
                public object EndDate { get; set; }
                public object Lasticrdt { get; set; }
                public object StartDate { get; set; }
                public DateTime? Taxdt { get; set; }
                public string Actnm { get; set; }
                public string CaseId { get; set; }
                public string SuspPeriod { get; set; }
                public string Atype { get; set; }
                public string NextPeriod { get; set; }
                public string Branchx { get; set; }
                public string Caltp { get; set; }
                public string Contactnm { get; set; }
                public string Crno { get; set; }
                public string DataVersion { get; set; }
                public string Designation { get; set; }
                public string Euser { get; set; }
                public string Fbnumx { get; set; }
                public string Fbstax { get; set; }
                public string Fbustx { get; set; }
                public string FormGuid { get; set; }
                public string Formprocx { get; set; }
                public string Forwardx { get; set; }
                public string FullName { get; set; }
                public string Gpart { get; set; }
                public string Gpartx { get; set; }
                public string Idnumbr { get; set; }
                public string Langx { get; set; }
                public string Mandt { get; set; }
                public string Mandtx { get; set; }
                public string NameFirst { get; set; }
                public string NameLast { get; set; }
                public string NameOrg1 { get; set; }
                public string NameOrg2 { get; set; }
                public string Officerx { get; set; }
                public string Operationx { get; set; }
                public string Other { get; set; }
                public string PortalUsrx { get; set; }
                public string Reason { get; set; }
                public string Reqtp { get; set; }
                public string ReturnIdx { get; set; }
                public string Srcidentifyx { get; set; }
                public string Statusx { get; set; }
                public string StepNumber { get; set; }
                public string StepNumberx { get; set; }
                public string TxnTpx { get; set; }
                public string Type { get; set; }
                public string UserTypx { get; set; }
                public AddressSet AddressSet { get; set; }
                public NotesSet NotesSet { get; set; }
                public AttdetSet AttdetSet { get; set; }
                public QuesListSet QuesListSet { get; set; }
            }

        }

        public class VATReviewDREGSuspensionListModel
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
                public string Gpart { get; set; }
                public DateTime StartDate { get; set; }
                public DateTime EndDate { get; set; }
                public DateTime Duedate { get; set; }
                public DateTime SuspDtfrom { get; set; }
                public DateTime SuspDtto { get; set; }
                public DateTime NextDtfrom { get; set; }
                public DateTime NextDtto { get; set; }
            }

            public class D
            {
                public List<Result> results { get; set; }
            }

        }

    }

}