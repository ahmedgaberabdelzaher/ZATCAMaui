using Foundation;
using Newtonsoft.Json;

namespace ZATCAMAUI.Models.VATInstalmentModels
{
    [Preserve(AllMembers = true)]
    public class VATInstalmentPlanModel
    {
        public VATInstalmentPlanModel()
        {
        }

        public string ActiveOutletDecisionOptions { get; set; }
        public bool ActiveOutletDecisionOptionsIsSelected { get; set; }
    }
  
    public class InstalmentAgreementFrequencyModel
    {
        public InstalmentAgreementFrequencyModel()
        {

        }

        public string FrequencyOptions { get; set; }
        public bool IsSelected { get; set; }
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
  
    public class ZakatSelectBillModel
    {
        public ZakatSelectBillModel()
        {

        }

        public string billNumber { get; set; }
        public string amount { get; set; }
        public string saadNumber { get; set; }
        public string taxPeriod { get; set; }
        public bool isSelected { get; set; }
        public string billType { get; set; }
    }
  
    public class ZakatSummaryViewModel
    {
        public ZakatSummaryViewModel()
        {

        }

        public string billNumber { get; set; }
        public string amount { get; set; }
        public string saadNumber { get; set; }
        public string taxPeriod { get; set; }
        public bool isSelected { get; set; }
        public string billType { get; set; }

    }
  
    public class InstalmentAgreementInstalmentPlansModel
    {
        public InstalmentAgreementInstalmentPlansModel()
        {
        }
        public string DueDate { get; set; }
        public string NumberOfMonths { get; set; }
        public string MonthlyInstalment { get; set; }
        public string TotalAmountPaid { get; set; }
        public string TotalAmountRemaining { get; set; }
    }

    //-----API Object will starts from herer-------

  
    public partial class Metadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
  
    public partial class VatInstalmentPlanResponse
    {
        [JsonProperty("data")]
        public VATInstalment d { get; set; }
        [JsonProperty("result")]
        public VATInstalment result { get; set; }
    }
  
    public partial class VATInstalment
    {
        public Metadata __metadata { get; set; }
        [JsonProperty("isApp")]
        public bool Appchkbox { get; set; }
        [JsonProperty("status")]
        public string EvStatus { get; set; }
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
        //[JsonProperty("TIN")]
        //public string Gpartz { get; set; }
        [JsonProperty("language")]
        public string Langz { get; set; }
        public string Mandt { get; set; }
        [JsonProperty("installmentNumber")]
        public string Noofinstallment { get; set; }
        public string OfficerTz { get; set; }
        public string Officerz { get; set; }
        [JsonProperty("operation")]
        public string Operationz { get; set; }
        [JsonProperty("TIN")]
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
        [JsonProperty("contractNumber")]
        public string Vtref { get; set; }
        [JsonProperty("currency")]
        public string Waers { get; set; }
        [JsonProperty("sadadBillNumber")]
        public string Sopbel { get; set; }
        [JsonProperty("totaldownpaymentTaxpayer")]
        public string Totdownpymtamt { get; set; }
        [JsonProperty("downpaymentDays")]
        public string DpDays { get; set; }
        public object[] VTADSet { get; set; }
        [JsonProperty("notes")]
        public List<NotesSetResult> NotesSet { get; set; }
        [JsonProperty("VATInstalmentSchedules")]
        public VATResults3[] VTISSet { get; set; }
        [JsonProperty("VATInstallmentAgreement")]
        public VATResults4[] VTIASet { get; set; }
        [JsonProperty("attachments")]
        public List<Attachment> AttachmentSet { get; set; }
        [JsonProperty("instructions")]
        public List<Instruction> InstructionSet { get; set; }
    }
  
    public partial class AttachmentSet
    {
        public List<Attachment> results { get; set; }
    }
    public class Instruction
    {
        public Metadata __metadata { get; set; }
        [JsonProperty("language")]
        public string Spras { get; set; }
        [JsonProperty("formBundleType")]
        public string Fbtyp { get; set; }
        [JsonProperty("versionNumber")]
        public string SrNo { get; set; }
        [JsonProperty("instructions")]
        public string Zztext { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class NotesSet
    {
        public List<NotesSetResult> results { get; set; }
    }
  
    public partial class NotesSetResult
    {
        public Metadata __metadata { get; set; }
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
        // [JsonProperty("portalUser")]
        public string ByGpartz { get; set; }
        [JsonProperty("dataVersion")]
        public string DataVersionz { get; set; }
        [JsonProperty("name")]
        public string Namez { get; set; }
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
        [JsonProperty("startLine")]
        public string Strline { get; set; }
    }
  
    public partial class NotesSetPost
    {
        public Metadata __metadata { get; set; }
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
        [JsonProperty("attachedByPerson")]
        public string AttByz { get; set; }
        [JsonProperty("TIN")]
        public string ByGpartz { get; set; }
        [JsonProperty("dataVersion")]
        public string DataVersionz { get; set; }
        //[JsonProperty("")]
        public string Noteno { get; set; }
        [JsonProperty("lineNumber")]
        public long Lineno { get; set; }
        [JsonProperty("elementNumber")]
        public long ElemNo { get; set; }
        [JsonProperty("notesFormat")]
        public string Tdformat { get; set; }
        [JsonProperty("startLine")]
        public string Tdline { get; set; }
    }
  
    public partial class VtadSet
    {
        public object[] results { get; set; }
    }
  
    public partial class VtiaSet
    {
        [JsonProperty("VATInstallmentAgreement")]
        public VATResults4[] results { get; set; }
    }
  
    public partial class VATResults4
    {
        public Metadata __metadata { get; set; }
        public string Mandt { get; set; }
        [JsonProperty("sadadNumber")]
        public string SadadNo { get; set; }
        [JsonProperty("selected")]
        public string Xsele { get; set; }
        [JsonProperty("enableCheckFlag")]
        public string EnableChk { get; set; }
        [JsonProperty("documentNumber")]
        public string Opbel { get; set; }
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
        [JsonIgnore]
        private bool _isArabic = false;
        [JsonIgnore]
        public bool IsArabic
        {
            get
            {
                return _isArabic;
            }
            set
            {
                _isArabic = App.IsArabic;

            }
        }
    }
  
    public partial class VtisSet
    {
        [JsonProperty("VATInstalmentSchedules")]
        public VATResults3[] results { get; set; }
    }
  
    public partial class VATResults3
    {
        public Metadata __metadata { get; set; }
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
        // public string dueDate { get; set; }
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
        [JsonIgnore]
        private bool _isArabic = false;
        [JsonIgnore]
        public bool IsArabic
        {
            get
            {
                return _isArabic;
            }
            set
            {
                _isArabic = App.IsArabic;

            }
        }

    }

    [Preserve(AllMembers = true)]
    public partial class VatInstalmentPlanRequest
    {
        public Metadata __metadata { get; set; }
        [JsonProperty("isApp")]
        public bool Appchkbox { get; set; }
        [JsonProperty("status")]
        public string EvStatus { get; set; }
        [JsonProperty("userName")]
        public string Officer { get; set; }
        [JsonProperty("totalInvestmentAmount")]
        public string TotInvAmt { get; set; }
        [JsonProperty("step1Confirmation")]
        public string Xstep1Conf { get; set; }
        [JsonProperty("startDate")]
        public object Begdaz { get; set; }
        [JsonProperty("step2Confirmation")]
        public string Xstep2Conf { get; set; }
        [JsonProperty("outStandingLiability")]
        public string Betrw { get; set; }
        [JsonProperty("declaration")]
        public string Decflg { get; set; }
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [JsonProperty("endDate")]
        public object Enddaz { get; set; }
        [JsonProperty("authenticationUser")]
        public string Euser { get; set; }
        [JsonProperty("formBundleNumber")]
        public string Fbnumz { get; set; }
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [JsonProperty("formProcess")]
        public string Formprocz { get; set; }
        //[JsonProperty("TIN")]
        //public string Gpartz { get; set; }
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
        [JsonProperty("TIN")]
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
        //[JsonProperty("")]
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
        [JsonProperty("contractNumber")]
        public string Vtref { get; set; }
        [JsonProperty("sadadBillNumber")]
        public string Sopbel { get; set; }
        [JsonProperty("currency")]
        public string Waers { get; set; }
        [JsonProperty("totaldownpaymentTaxpayer")]
        public string Totdownpymtamt { get; set; }
        [JsonProperty("downpaymentDays")]
        public string DpDays { get; set; }
        [JsonProperty("taxpayerInformations")]
        public object[] VTADSet { get; set; }
        [JsonProperty("notes")]
        public NotesSetPost[] NOTESSet { get; set; }
        [JsonProperty("VATInstalmentSchedules")]
        public VATResults3[] VTISSet { get; set; }
        [JsonProperty("VATInstallmentAgreement")]
        public VATResults4[] VTIASet { get; set; }
        [JsonProperty("attachments")]
        public List<Attachment> ATTACHMENTSet { get; set; }

    }
    [Preserve(AllMembers = true)]
    public partial class VtiaBtnSetModel
    {
        [JsonProperty("data")]
        public VtiaBtnSetResponse D;
    }
    [Preserve(AllMembers = true)]
    public class VtiaBtnSetResponse
    {
        [JsonProperty("__metadata")]
        public Metadata Metadata;

        [JsonProperty("edit")]
        public string EditFgz;

        [JsonProperty("formBundleNumber")]
        public string Fbnum;

        [JsonProperty("formBundleType")]
        public string Fbtypz;

        [JsonProperty("formBundleStatus")]
        public string Fbustz;

        [JsonProperty("formProcess")]
        public string Formproc;

        [JsonProperty("TIN")]
        public string Gpart;

        [JsonProperty("language")]
        public string Lang;

        [JsonProperty("userName")]
        public string Officer;

        [JsonProperty("operation")]
        public string Operation;

        [JsonProperty("portalUser")]
        public string PortalUsr;

        [JsonProperty("returnId")]
        public string ReturnId;

        [JsonProperty("statusCode")]
        public string Status;

        [JsonProperty("stepNumber")]
        public string StepNumber;

        [JsonProperty("transactionType")]
        public string TxnTp;

        [JsonProperty("userType")]
        public string UserTyp;

        [JsonProperty("VTIAButtons")]
        public List<VtiaBtn> Results;
    }
    [Preserve(AllMembers = true)]
    public class VtiaBtnSet
    {
        [JsonProperty("results")]
        public List<VtiaBtn> Results;
    }
    [Preserve(AllMembers = true)]
    public class VtiaBtn
    {
        [JsonProperty("__metadata")]
        public Metadata Metadata;

        [JsonProperty("formBundleType")]
        public string Fbtyp;

        [JsonProperty("formBundleStatus")]
        public string Fbust;

        [JsonProperty("button")]
        public string Button;

        [JsonProperty("transactionType")]
        public string TransactionType;

        [JsonProperty("userType")]
        public string UserTyp;
    }



}
