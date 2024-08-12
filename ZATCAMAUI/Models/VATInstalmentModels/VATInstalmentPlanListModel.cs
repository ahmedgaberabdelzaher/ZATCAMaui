using Foundation;
using Newtonsoft.Json;
using static ZATCAMAUI.Models.VATInstalmentModels.RequestToVATInstallmentPlanDetails.DisplayInstallmentAgreementSchedulePlan;
using static ZATCAMAUI.Models.VATInstalmentModels.RequestToVATInstallmentPlanDetails.VATInstalmentScheduleDetailsModel;

namespace ZATCAMAUI.Models.VATInstalmentModels
{
    [Preserve(AllMembers = true)]
    public class VATInstalmentPlanListModel
    {
        public VATInstalmentPlanListModel()
        {
        }
    }


    
    public class RequestToVATInstallmentPlan
    {
        
        public class __metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }

        }
        
        public class Results
        {
            public __metadata __metadata { get; set; }
            public string FbtText { get; set; }
            public string Fbtyp { get; set; }
            public string UserErrFg { get; set; }
            public string SysFlg { get; set; }
            public string Ldate { get; set; }

        }
        
        public class REQTYPSet
        {

            public List<Results> results { get; set; }

        }
        
        public class STATUSSetResults
        {
            public __metadata __metadata { get; set; }
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
            public List<STATUSSetResults> results { get; set; }

        }
        
        public class ASSLISTSetResults
        {
            public __metadata __metadata { get; set; }
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
            public string Receipt { get; set; }
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
            public List<ASSLISTSetResults> results { get; set; }

        }
        
        public class D
        {
            public __metadata __metadata { get; set; }
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
        
        public class Application
        {
            public D d { get; set; }

        }

    }

    
    public class RequestToVATInstallmentPlanDetails
    {
        [Preserve(AllMembers = true)]
        [JsonProperty("data")]
        public D d { get; set; }

        [JsonProperty("result")]
        public D result { set { d = value; } }

        [Preserve(AllMembers = true)]
        public class __metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }

        }
        
        public class VTADSetResults
        {
            public __metadata __metadata { get; set; }
            [JsonProperty("systemCode")]
            public string Mandt { get; set; }
            [JsonProperty("formGUID")]
            public string FormGuid { get; set; }
            [JsonProperty("dataVersion")]
            public string DataVersion { get; set; }
            [JsonProperty("lineNumber")]
            public int LineNo { get; set; }
            [JsonProperty("rankingOrder")]
            public string RankingOrder { get; set; }
            [JsonProperty("addressType")]
            public string AddrType { get; set; }
            [JsonProperty("sourceIdentifier")]
            public string Srcidentify { get; set; }
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
            [JsonProperty("longitude")]
            public string Longitude { get; set; }
            [JsonProperty("latitude")]
            public string Latitude { get; set; }
            [JsonProperty("lengthSize")]
            public string LenSiz { get; set; }
            [JsonProperty("widthSize")]
            public string WidSiz { get; set; }
            [JsonProperty("heightSize")]
            public string HeiSiz { get; set; }
            [JsonProperty("sizeBaseUnit")]
            public string SizUn { get; set; }
            [JsonProperty("cubicalDimensionsSize")]
            public string CubicSc { get; set; }
            [JsonProperty("cubicBaseUnit")]
            public string CubicScUn { get; set; }
            [JsonProperty("squareDimensionsSize")]
            public string SquarSc { get; set; }
            [JsonProperty("squareBaseUnit")]
            public string SquarScUn { get; set; }
            //[JsonProperty("")]
            public string LongitudeC { get; set; }
            // [JsonProperty("")]
            public string LatitudeC { get; set; }
            [JsonProperty("regionDescription")]
            public string RegionDesc { get; set; }
            [JsonProperty("returnId")]
            public string ReturnId { get; set; }

        }
        
        public class VTADSet
        {
            [JsonProperty("taxpayerInformations")]
            public List<VTADSetResults> results { get; set; }

        }
        
        public class NOTESSetResults
        {
            public __metadata __metadata { get; set; }
            public string Notenoz { get; set; }
            public string Refnamez { get; set; }
            public string XInvoicez { get; set; }
            public string XObsoletez { get; set; }
            public string Rcodez { get; set; }
            public string Erfusrz { get; set; }
            public string Erfdtz { get; set; }
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
        
        public class VATNOTESSet
        {
            [JsonProperty("notes")]
            public List<NOTESSetResults> results { get; set; }

        }
        [Preserve(AllMembers = true)]
        public class VATNOTESSets
        {
            public List<NOTES> results { get; set; }

        }

        public class NOTES
        {
            [JsonProperty("Notenoz")]
            public string Notenoz;

            [JsonProperty("Refnamez")]
            public string Refnamez;

            [JsonProperty("DataVersionz")]
            public string DataVersionz;

            [JsonProperty("XInvoicez")]
            public string XInvoicez;

            [JsonProperty("XObsoletez")]
            public string XObsoletez;

            [JsonProperty("Rcodez")]
            public string Rcodez;

            [JsonProperty("Erfusrz")]
            public string Erfusrz;

            [JsonProperty("Erfdtz")]
            public object Erfdtz;

            [JsonProperty("ByGpartz")]
            public string ByGpartz;

            [JsonProperty("AttByz")]
            public string AttByz;

            [JsonProperty("Noteno")]
            public string Noteno;

            [JsonProperty("Lineno")]
            public int Lineno;

            [JsonProperty("ElemNo")]
            public int ElemNo;

            [JsonProperty("Tdformat")]
            public string Tdformat;

            [JsonProperty("Tdline")]
            public string Tdline;
        }

        [Preserve(AllMembers = true)]
        public class VTISSetResults
        {
            public __metadata __metadata { get; set; }
            [JsonProperty("systemCode")]
            public string Mandt { get; set; }
            [JsonProperty("formGUID")]
            public string FormGuid { get; set; }
            [JsonProperty("dataVersion")]
            public string DataVersion { get; set; }
            [JsonProperty("lineNumber")]
            public int LineNo { get; set; }
            [JsonProperty("rankingOrder")]
            public string RankingOrder { get; set; }
            [JsonProperty("dueDate")]
            public string Faedn { get; set; }
            [JsonProperty("period")]
            public string Monat { get; set; }
            [JsonProperty("outStandingLiability")]
            public string Betrw { get; set; }
            [JsonProperty("totalPaidAmount")]
            public string Totpaidamt { get; set; }
            [JsonProperty("totalRemainingAmount")]
            public string Totremainamt { get; set; }
            [JsonProperty("currency")]
            public string Waers { get; set; }
            [JsonProperty("returnId")]
            public string ReturnId { get; set; }

        }
        
        public class VTISSet
        {
            [JsonProperty("VATInstalmentSchedules")]
            public List<VTISSetResults> results { get; set; }

        }
        
        public class VTIASetResults
        {
            public __metadata __metadata { get; set; }
            [JsonProperty("systemCode")]
            public string Mandt { get; set; }
            [JsonProperty("sadadNumber")]
            public string SadadNo { get; set; }
            [JsonProperty("selected")]
            public string Xsele { get; set; }
            [JsonProperty("formGUID")]
            public string FormGuid { get; set; }
            [JsonProperty("dataVersion")]
            public string DataVersion { get; set; }
            [JsonProperty("lineNumber")]
            public int LineNo { get; set; }
            [JsonProperty("rankingOrder")]
            public string RankingOrder { get; set; }
            [JsonProperty("additionalReference")]
            public string Vtre2 { get; set; }
            [JsonProperty("billPeriodFrom")]
            public string Abrzu { get; set; }
            [JsonProperty("billPeriodTo")]
            public string Abrzo { get; set; }
            [JsonProperty("monthlyInstallment")]
            public string Betrh { get; set; }
            [JsonProperty("currency")]
            public string Waers { get; set; }
            [JsonProperty("taxPeriodDescription")]
            public string Taxperioddsc { get; set; }
            [JsonProperty("returnId")]
            public string ReturnId { get; set; }

        }
        
        public class VTIASet
        {
            [JsonProperty("VATInstallmentAgreement")]
            public List<VTIASetResults> results { get; set; }

        }
        
        public class ATTACHMENTSetResults
        {
            public __metadata __metadata { get; set; }
            public string AttBy { get; set; }
            public string ByPusr { get; set; }
            public string DataVersion { get; set; }
            public string DocUrl { get; set; }
            public string Doguid { get; set; }
            public string Dotyp { get; set; }
            public string Erfdt { get; set; }
            public string Erftm { get; set; }
            public string FileExtn { get; set; }
            public string Filename { get; set; }
            public string Mimetype { get; set; }
            public string OutletRef { get; set; }
            public string RetGuid { get; set; }
            public string SchGuid { get; set; }
            public string Seqno { get; set; }
            public int Srno { get; set; }

        }
        
        public class ATTACHMENTSet
        {
            [JsonProperty("attachments")]
            public List<ATTACHMENTSetResults> results { get; set; }

        }
        
        public class D
        {
            public __metadata __metadata { get; set; }
            [JsonProperty("isApp")]
            public bool Appchkbox { get; set; }
            [JsonProperty("status")]
            public string EvStatus { get; set; }
            [JsonProperty("captchaCode")]
            public string Captcha { get; set; }
            [JsonProperty("userName")]
            public string Officer { get; set; }
            [JsonProperty("totalInvestmentAmount")]
            public string TotInvAmt { get; set; }
            [JsonProperty("step1Confirmation")]
            public string Xstep1Conf { get; set; }
            [JsonProperty("startDate")]
            public string Begdaz { get; set; }
            [JsonProperty("step2Confirmation")]
            public string Xstep2Conf { get; set; }
            [JsonProperty("outStandingLiability")]
            public string Betrw { get; set; }
            [JsonProperty("declaration")]
            public string Decflg { get; set; }
            [JsonProperty("dataVersion")]
            public string DataVersion { get; set; }
            [JsonProperty("endDate")]
            public string Enddaz { get; set; }
            [JsonProperty("authenticationUser")]
            public string Euser { get; set; }
            [JsonProperty("formBundleNumber")]
            public string Fbnumz { get; set; }
            [JsonProperty("formGUID")]
            public string FormGuid { get; set; }
            [JsonProperty("formProcess")]
            public string Formprocz { get; set; }
            [JsonProperty("TIN")]
            public string Gpartz { get; set; }
            [JsonProperty("language")]
            public string Langz { get; set; }
            [JsonProperty("systemCode")]
            public string Mandt { get; set; }
            [JsonProperty("installmentNumber")]
            public string Noofinstallment { get; set; }
            // [JsonProperty("")]
            public string OfficerTz { get; set; }
            //  [JsonProperty("")]
            public string Officerz { get; set; }
            [JsonProperty("operation")]
            public string Operationz { get; set; }
            //[JsonProperty("")]
            public string Partner { get; set; }
            [JsonProperty("taxpayerName")]
            public string Partnernm { get; set; }
            [JsonProperty("penaltyAmount")]
            public string Peneltyamt { get; set; }
            [JsonProperty("periodkey")]
            public string Periodkeyz { get; set; }
            [JsonProperty("portalUser")]
            public string PortalUsrz { get; set; }
            [JsonProperty("returnId")]
            public string ReturnId { get; set; }
            //  [JsonProperty("")]
            public string ReturnIdz { get; set; }
            [JsonProperty("sourceApplication")]
            public string SrcAppz { get; set; }
            [JsonProperty("statusCode")]
            public string Statusz { get; set; }
            [JsonProperty("stepNumber")]
            public string StepNumberz { get; set; }
            [JsonProperty("totalDueAmount")]
            public string Totdueamt { get; set; }
            [JsonProperty("totalLiabilityAmount")]
            public string Totliablityamt { get; set; }
            [JsonProperty("transactionType")]
            public string TxnTpz { get; set; }
            [JsonProperty("userType")]
            public string UserTypz { get; set; }
            [JsonProperty("contract")]
            public string Vtref { get; set; }
            [JsonProperty("currency")]
            public string Waers { get; set; }
            [JsonProperty("totaldownpaymentTaxpayer")]
            public string Totdownpymtamt { get; set; }
            [JsonProperty("totaldownpaymentSupervisor")]
            public string TotdownpymtamtSu { get; set; }
            [JsonProperty("downpaymentChangeflag")]
            public string DownpymtChange { get; set; }
            [JsonProperty("downpaymentExemptFlag")]
            public string DownpymtExpmt { get; set; }
            [JsonProperty("telephoneNumber")]
            public string MobileNo { get; set; }
            [JsonProperty("OTPGuid")]
            public string OtpGuid { get; set; }
            [JsonProperty("additionalContactNumber")]
            public string AddContNumb { get; set; }
            [JsonProperty("downpaymentDays")]
            public string DpDays { get; set; }
            [JsonProperty("interestRate")]
            public string InterestRate { get; set; }
            [JsonProperty("sadadBillNumber")]
            public string Sopbel { get; set; }
            [JsonProperty("OTP")]
            public string Otp { get; set; }
            [JsonProperty("taxpayerInformations")]
            public List<VTADSetResults> VTADSet { get; set; }
            [JsonProperty("notes")]
            public List<NOTESSetResults> NOTESSet { get; set; }
            [JsonProperty("VATInstalmentSchedules")]
            public List<VTISSetResults> VTISSet { get; set; }
            [JsonProperty("VATInstallmentAgreement")]
            public List<VTIASetResults> VTIASet { get; set; }
            [JsonProperty("attachments")]
            public List<ATTACHMENTSetResults> ATTACHMENTSet { get; set; }
            [JsonProperty("questions")]
            public List<QuesSet> QuesListSet { get; set; }
            //[JsonProperty("taxTypes")] 
            public taxtypedrpdwnSet taxtypedrpdwnSet { get; set; }
            [JsonProperty("instructions")]
            public List<Instructions> INSTRUCTIONSet { get; set; }
        }
        public class INSTRUCTIONSet
        {
            [JsonProperty("results")]
            public List<Instructions> Results;
        }

        public class Instructions
        {
            [JsonProperty("__metadata")]
            public Metadata Metadata;

            [JsonProperty("Spras")]
            public string Spras;

            [JsonProperty("Fbtyp")]
            public string Fbtyp;

            [JsonProperty("SrNo")]
            public string SrNo;

            [JsonProperty("Zztext")]
            public string Zztext;
        }

        [Preserve(AllMembers = true)]
        public class taxtypedrpdwnSet
        {
            [JsonProperty("__deferred")]
            public Deferred Deferred;

        }

        [Preserve(AllMembers = true)]
        public class QuesListSet
        {
            public List<QuesSet> results { get; set; }

        }
        public class QuesSet
        {
            [JsonProperty("__metadata")]
            public Metadata Metadata;

            [JsonProperty("Fbtyp")]
            public string Fbtyp;

            [JsonProperty("Qnno")]
            public string Qnno;

            [JsonProperty("RequiredFg")]
            public string RequiredFg;

            [JsonProperty("TransactionType")]
            public string TransactionType;

            [JsonProperty("Rcode")]
            public string Rcode;

            [JsonProperty("QnnoDesc")]
            public string QnnoDesc;
        }
        [Preserve(AllMembers = true)]
        public class PednRtn
        {
            public Metadata __metadata { get; set; }
            public string Perslt { get; set; }
            public string Persl { get; set; }
            public string Taxtp { get; set; }
        }

        [Preserve(AllMembers = true)]
        public class RevokeSet
        {

            [JsonProperty("__metadata")]
            public Metadata Metadata;

            [JsonProperty("userType")]
            public string UserTyp;

            [JsonProperty("formBundleType")]
            public string Fbtyp;

            [JsonProperty("Tin")]
            public string Tin;

            [JsonProperty("formBundleNumber")]
            public string Fbnum;

            [JsonProperty("submittedDate")]
            public DateTime SubmitDt;

            [JsonProperty("totalAmount")]
            public string TotAmt;

            [JsonProperty("downPaymentAmount")]
            public string DpAmt;

            [JsonProperty("dueAmount")]
            public string DueAmt;

            [JsonProperty("paymentFrequency")]
            public string PymntFreq;

            [JsonProperty("planDuration")]
            public string PlanDur;

            [JsonProperty("currency")]
            public string Waers;

            [JsonProperty("formBundleStatus")]
            public string Fbsta;

            [JsonProperty("formBundleUserStatus")]
            public string Fbust;

            [JsonProperty("statusDescription")]
            public string Status;

            [JsonProperty("isRevoked")]
            public bool Revoke;

            [JsonProperty("IPType")]
            public string IptypeFg;

        }
        [Preserve(AllMembers = true)]
        public class PednRtnSet
        {
            public List<PednRtn> results { get; set; }
        }

        [Preserve(AllMembers = true)]
        public class RevokeListSet
        {
            [JsonProperty("results")]
            public List<RevokeSet> Results;
        }
        #region VATInstalmentList

        
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
            [JsonProperty("formBundleTypeDescription")]
            public string FbtText { get; set; }
            [JsonProperty("formBundleType")]
            public string Fbtyp { get; set; }
            [JsonProperty("userError")]
            public string UserErrFg { get; set; }
            [JsonProperty("system")]
            public string SysFlg { get; set; }
            // [JsonProperty("")]
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
        
        public class Result21
        {
            public Metadata3 __metadata { get; set; }
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
            public List<Result21> results { get; set; }
        }
        
        public class Metadata4
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
        
        public class Result31
        {
            public Metadata4 __metadata { get; set; }
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
            // [JsonProperty("")]
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
            public List<Result31> results { get; set; }
        }
        
        public class ReqVatInstalmentPlan
        {
            public Metadata __metadata { get; set; }
            [JsonProperty("userTIN")]
            public string UserTin { get; set; }
            [JsonProperty("auditorTIN")]
            public string AudTin { get; set; }
            //  [JsonProperty("")]
            public object Begda { get; set; }
            [JsonProperty("taxType")]
            public string TaxType { get; set; }
            //  [JsonProperty("")]
            public object Endda { get; set; }
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
            [JsonProperty("")]
            public string Officer { get; set; }
            [JsonProperty("operation")]
            public string Operation { get; set; }
            [JsonProperty("periodkey")]
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
            // [JsonProperty("")]
            public string TxnTp { get; set; }
            [JsonProperty("userType")]
            public string UserTyp { get; set; }
            [JsonProperty("requestTypeList")]
            public List<Result> REQTYPSet { get; set; }
            [JsonProperty("statusList")]
            public List<Result21> STATUSSet { get; set; }
            [JsonProperty("ASSList")]
            public List<Result31> ASSLISTSet { get; set; }
            [JsonProperty("periodReturns")]
            public List<PednRtn> PednRtnSet { get; set; }
            [JsonProperty("revokes")]
            public List<RevokeSet> RevokeListSet { get; set; }
        }
        
        public class ReqVatInstalmentPlanResponse
        {
            [JsonProperty("data")]
            public ReqVatInstalmentPlan d { get; set; }
        }

        #endregion

        #region Display Instalmenent Schedules 
        
        public class DisplayInstallmentAgreementSchedulePlan
        {
            

            [JsonProperty("data")]
            public D d { get; set; }

            
            public partial class D
            {

                public Metadata Metadata { get; set; }

                [JsonProperty("formGUID")]
                public string FormGuid { get; set; }

                [JsonProperty("language")]
                public string Langz { get; set; }

                [JsonProperty("numberOfMonths")]
                public string Noofmon { get; set; }

                [JsonProperty("serialNumber")]
                public string Euser { get; set; }

                [JsonProperty("documentNumber")]
                public string Opbel { get; set; }

                [JsonProperty("TIN")]
                public string Gpart { get; set; }

                [JsonProperty("installmentTotalAmount")]
                public string TotalInstall { get; set; }

                [JsonProperty("totalAmountPaid")]
                public string TotalAmntPaid { get; set; }

                [JsonProperty("remainingTotalAmount")]
                public string TotalRemAmnt { get; set; }

                [JsonProperty("instalmentAgreementDisplay")]
                public List<VtiaIadtSetResult> VtiaIadtSet { get; set; }

                [JsonProperty("instalmentScheduleDisplay")]
                public List<VtiaIaSetResult> VtiaIahdSet { get; set; }
            }
            
            public partial class Metadata
            {

                public Uri Id { get; set; }


                public Uri Uri { get; set; }


                public string Type { get; set; }
            }
            
            public partial class VtiaIaSet
            {
                [JsonProperty("instalmentAgreementDisplay")]
                public List<VtiaIaSetResult> Results { get; set; }
            }
            
            public partial class VtiaIaSetResult
            {

                public Metadata Metadata { get; set; }

                [JsonProperty("documentNumber")]
                public string Opbel { get; set; }

                [JsonProperty("installmentAmount")]
                public string InstalmentAmt { get; set; }

                [JsonProperty("remainingAmount")]
                public string RemainingAmt { get; set; }

                [JsonProperty("currency")]
                public string Waers { get; set; }

                [JsonProperty("periodDescription")]
                public string PeriodText { get; set; }

                [JsonProperty("agreementNumber")]
                public string AgreementNo { get; set; }
                public string formBundleNumber { get; set; }
            }
        }
        
        public class VATInstalmentDetailsInputModel
        {
            public class D
            {
                public object Abrzo { get; set; }
                public object Abrzu { get; set; }
                public object Aedat { get; set; }

                [JsonProperty("AudFlag")]
                public bool AudFlag { get; set; }

                [JsonProperty("auditor")]
                public string Auditor { get; set; }
                [JsonProperty("auditorNumber")]
                public object Begda { get; set; }

                [JsonProperty("branch")]
                public string Branch { get; set; }

                [JsonProperty("calendarType")]
                public string CalendrTyp { get; set; }

                [JsonProperty("combination")]
                public string Comb { get; set; }

                [JsonProperty("display")]
                public string Dispflag { get; set; }

                [JsonProperty("dueStatus")]
                public string Due { get; set; }

                [JsonProperty("dueDate")]
                public object DueDt { get; set; }

                [JsonProperty("dueDateCharacter")]
                public string DueDtC { get; set; }

                [JsonProperty("endDate")]
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

                [JsonProperty("fbText")]
                public string FbtText { get; set; }

                [JsonProperty("formBundleType")]
                public string Fbtyp { get; set; }

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

                [JsonProperty("errorMessage")]
                public string Msg { get; set; }

                [JsonProperty("isObjectionFiled")]
                public bool ObjFiled { get; set; }

                [JsonProperty("obligation")]
                public string ObligFlag { get; set; }

                [JsonProperty("isOpen")]
                public bool Open { get; set; }

                [JsonProperty("period")]
                public string Period { get; set; }

                public string Persl { get; set; }

                [JsonProperty("refundFiled")]
                public bool RefundFiled { get; set; }

                [JsonProperty("sadadBillNumber1")]
                public string SadadDoc1 { get; set; }

                [JsonProperty("sadadBillNumber2")]
                public string SadadDoc2 { get; set; }

                [JsonProperty("statusCode")]
                public string Stat { get; set; }


                public string Statflag { get; set; }

                [JsonProperty("status")]
                public string Status { get; set; }

                [JsonProperty("taxPeriod")]
                public string TaxPeriod { get; set; }
                public string Vtref { get; set; }
            }


            [Preserve(AllMembers = true)]
            [JsonProperty("data")]
            public D d { get; set; }

        }
        
        public class VATInstalmentScheduleDetailsModel
        {
            [Preserve(AllMembers = true)]
            [JsonProperty("data")]
            public D d { get; set; }

            
            public partial class D
            {
                // [JsonProperty("__metadata")]
                public Metadata Metadata { get; set; }

                [JsonProperty("formGUID")]
                public string FormGuid { get; set; }

                [JsonProperty("language")]
                public string Langz { get; set; }

                [JsonProperty("numberOfMonths")]
                public string Noofmon { get; set; }

                [JsonProperty("serialNumber")]
                public string Euser { get; set; }

                [JsonProperty("documentNumber")]
                public string Opbel { get; set; }

                [JsonProperty("TIN")]
                public string Gpart { get; set; }

                [JsonProperty("installmentTotalAmount")]
                public string TotalInstall { get; set; }

                [JsonProperty("totalAmountPaid")]
                public string TotalAmntPaid { get; set; }

                [JsonProperty("remainingTotalAmount")]
                public string TotalRemAmnt { get; set; }

                [JsonProperty("instalmentAgreementDisplay")]
                public List<VtiaIadtSetResult> VtiaIadtSet { get; set; }

                [JsonProperty("instalmentScheduleDisplay")]
                public List<VtiaIaSetResult> VtiaIahdSet { get; set; }
            }
            
            public partial class Metadata
            {

                public Uri Id { get; set; }


                public Uri Uri { get; set; }


                public string Type { get; set; }
            }
            
            public partial class VtiaIadtSet1
            {
                [JsonProperty("instalmentAgreementDisplay")]
                public List<VtiaIadtSetResult> Results { get; set; }
            }
            
            public partial class VtiaIadtSetResult
            {
                [JsonProperty("__metadata")]
                public Metadata Metadata { get; set; }

                [JsonProperty("dueDate")]
                public string DueDate { get; set; }

                [JsonProperty("numberOfMonths")]
                public string NoOfMonths { get; set; }

                [JsonProperty("installmentTotalAmount")]
                public string InstalmentAmt { get; set; }

                [JsonProperty("totalAmountPaid")]
                public string TotalPaidAmt { get; set; }

                [JsonProperty("remainingTotalAmount")]
                public string RemainingAmt { get; set; }

                [JsonProperty("status")]
                public string Status { get; set; }

                [JsonProperty("currency")]
                public string Waers { get; set; }
            }
            
            public partial class VtiaIahdSet
            {
                [JsonProperty("results")]
                public List<VtiaIahdSetResult> Results { get; set; }
            }
            
            public partial class VtiaIahdSetResult
            {
                [JsonProperty("__metadata")]
                public Metadata Metadata { get; set; }

                [JsonProperty("Opbel")]
                public string Opbel { get; set; }

                [JsonProperty("InstalmentAmt")]
                public string InstalmentAmt { get; set; }

                [JsonProperty("RemainingAmt")]
                public string RemainingAmt { get; set; }

                [JsonProperty("Waers")]
                public string Waers { get; set; }

                [JsonProperty("PeriodText")]
                public string PeriodText { get; set; }

                [JsonProperty("AgreementNo")]
                public string AgreementNo { get; set; }
            }
        }

        #endregion

    }
}
