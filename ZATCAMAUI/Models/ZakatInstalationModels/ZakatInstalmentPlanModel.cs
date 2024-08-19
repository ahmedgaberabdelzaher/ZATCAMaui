
using Newtonsoft.Json;
using static ZATCAMAUI.Models.ZakatInstalationModels.ZAKATRequestPlanModel;
namespace ZATCAMAUI.Models.ZakatInstalationModels
{

    public class ZakatInstalmentPlanModel
    {
        public string CardLabel { get => ActiveOutletDecisionOptions; }
        public string ActiveOutletDecisionOptions { get; set; }
        public bool ActiveOutletDecisionOptionsIsSelected { get; set; }
    }
    

    public class InstalmentAgreementFrequencyModel
    {
        public string CardLabel { get => FrequencyOptions; }
        public string FrequencyOptions { get; set; }
        public bool IsSelected { get; set; }
    }
    
    public class InstalmentAgreementAttachmentsModel
    {

        public string FieldTitle { get; set; }
        public string FieldSubTitle { get; set; }
        public string AttachmentName { get; set; }

        public bool IsAttachmentAttached { get; set; }
    }
    
    public class ZakatListModel
    {

        public string referanceNumber { get; set; }
        public string status { get; set; }
        public string dueamount { get; set; }
        public string instalmentAmount { get; set; }
        public string noOfInstalments { get; set; }
        public string downpayment { get; set; }
        public string dateOfSubmission { get; set; }
        public string Fbtyp { get; set; }
        public string frequency { get; set; }
        public string SelectedType { get; set; }
        public string statusType { get; set; }
        public string fbNum { get; set; }
        public bool isShowApproveView { get; set; }

    }
    
    public class ZakatSelectBillModel
    {

        public string billNumber { get; set; }
        public string amount { get; set; }
        public string saadNumber { get; set; }
        public string taxPeriod { get; set; }
        public bool isSelected { get; set; }
        public string billType { get; set; }
        public string frequency { get; set; }
    }

    
    public class ZakatSummaryViewModel
    {

        public string billNumber { get; set; }
        public string amount { get; set; }
        public string saadNumber { get; set; }
        public string taxPeriod { get; set; }
        public bool isSelected { get; set; }
        public string billType { get; set; }

    }
    
    public class InstalmentAgreementInstalmentPlansModel
    {
        public string DueDate { get; set; }
        public string NumberOfMonths { get; set; }
        public string MonthlyInstalment { get; set; }
        public string TotalAmountPaid { get; set; }
        public string TotalAmountRemaining { get; set; }
    }

    //-----API Object will starts from herer-------



    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 


    
    public partial class ZakatInstalmentPlanResponse
    {
        [JsonProperty("data")]
        public ZakatInstalment d { get; set; }
        [JsonProperty("result")]
        public ZakatInstalment result { get; set; }
    }

    
    public partial class ZakatInstalment
    {
        public Metadata __metadata { get; set; }
        [JsonProperty("accountingMethod")]
        public string AccMethod { get; set; }
        [JsonProperty("alternateMobileNumber")]
        public string AltMobNo { get; set; }
        [JsonProperty("attachments")]
        public List<Attachment> AttachSet { get; set; }
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [JsonProperty("agree")]
        public string DecCb { get; set; }
        [JsonProperty("downPaymentAmount")]
        public string DpAmt { get; set; }
        [JsonProperty("requiredDownPayment")]
        public string DownPayReq { get; set; }
        [JsonProperty("officerReason")]
        public string OfcReason { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("portalUser")]
        public string Euser { get; set; }
        [JsonProperty("formBundleGUID")]
        public string Fbguid { get; set; }
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        // [JsonProperty("")]
        public string FbnumIprr { get; set; }
        [JsonProperty("financialStatements")]
        public List<FnDtlSetObject> FnDtlSet { get; set; }
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [JsonProperty("formMode")]
        public string FormMode { get; set; }
        [JsonProperty("formProcess")]
        public string Formproc { get; set; }
        [JsonProperty("installementDueDate")]
        public string InsDtOff { get; set; }
        [JsonProperty("officerInstallmentPlans")]
        public List<object> insPlan_OffSet { get; set; }
        [JsonProperty("installementPlans")]
        public List<Results4> insPlanSet { get; set; }
        [JsonProperty("installmentRequestedFor")]
        public string InstReqFor { get; set; }
        [JsonProperty("installmentRequestedReason")]
        public string InstReqReason { get; set; }
        [JsonProperty("investments")]
        public Results5[] invDtlsSet { get; set; }
        [JsonProperty("language")]
        public string Langz { get; set; }
        [JsonProperty("lockingStatus")]
        public string Lokst { get; set; }
        [JsonProperty("mobileNumber")]
        public string MobNo { get; set; }
        [JsonProperty("notes")]
        public List<NotesSet> NotesSet { get; set; }
        [JsonProperty("officerAmount")]
        public string OffAmt { get; set; }
        // [JsonProperty("")]
        public string Officer { get; set; }
        [JsonProperty("officerPlanDuration")]
        public string OffPlanDur { get; set; }
        [JsonProperty("officerPaymentFrequency")]
        public string OffPymntFreq { get; set; }
        [JsonProperty("operation")]
        public string Operation { get; set; }
        [JsonProperty("paymentDate")]
        public string PaymtDt { get; set; }
        [JsonProperty("penaltyAmount")]
        public string PenlAmt { get; set; }
        [JsonProperty("periodKey")]
        public string Periodkey { get; set; }
        [JsonProperty("planDuration")]
        public string PlanDur { get; set; }
        [JsonProperty("paymentFrequency")]
        public string PymntFreq { get; set; }
        [JsonProperty("messages")]
        public List<object> retmsgSet { get; set; }
        [JsonProperty("returnId")]
        public string ReturnId { get; set; }
        [JsonProperty("sadadBillNumber")]
        public string Sopbel { get; set; }
        [JsonProperty("statusCode")]
        public string Status { get; set; }
        [JsonProperty("stepNumber")]
        public string StepNumber { get; set; }
        [JsonProperty("reviseDownPaymentAmount")]
        public string SuAmt { get; set; }
        [JsonProperty("supervisiorAMT")]
        public string SuAmtFg { get; set; }
        [JsonProperty("TIN")]
        public string Tin { get; set; }
        [JsonProperty("taxpayerName")]
        public string TinNm { get; set; }
        [JsonProperty("totalAmount")]
        public string TotAmt { get; set; }
        [JsonProperty("transactionType")]
        public string TxnTp { get; set; }
        [JsonProperty("userType")]
        public string UserTyp { get; set; }
        [JsonProperty("currency")]
        public string Waers { get; set; }
        [JsonProperty("days")]
        public string Zdays { get; set; }
    }
    
    public partial class AttachSet
    {
        [JsonProperty("attachments")]
        public List<Attachment> results { get; set; }
    }

    
    public partial class FnDtlSet
    {
        [JsonProperty("financialStatements")]
        public List<FnDtlSetObject> results { get; set; }
    }
    
    public partial class InsPlanOffSet
    {
        [JsonProperty("officerInstallmentPlans")]
        public List<object> result { get; set; }
    }
    
    public partial class InsPlanSet
    {
        [JsonProperty("installementPlans")]
        public List<Results4> results { get; set; }
    }
    
    public partial class Results4
    {

        public Metadata __metadata { get; set; }
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [JsonProperty("installmentStatusDescription")]
        public string InsStatusTxt { get; set; }
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [JsonProperty("lineNumber")]
        public long LineNo { get; set; }
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; }
        // [JsonProperty("")]
        public string InsFbnum { get; set; }
        [JsonProperty("userType")]
        public string UserTyp { get; set; }
        [JsonProperty("basicAmount")]
        public string BasicAmt { get; set; }
        [JsonProperty("penaltyAmount")]
        public string PenatlyAmt { get; set; }
        [JsonProperty("interestAmount")]
        public string InterestAmt { get; set; }
        [JsonProperty("installmentAmount")]
        public string InsAmt { get; set; }
        [JsonProperty("dueDate")]
        public string DueDt { get; set; }
        [JsonProperty("installmentStatus")]
        public string InsStatus { get; set; }
        [JsonProperty("currency")]
        public string Waers { get; set; }
        [JsonProperty("returnId")]
        public string ReturnId { get; set; }
    }
    
    public partial class InvDtlsSet
    {
        [JsonProperty("investments")]
        public Results5[] results { get; set; }
    }
    
    public partial class Results5
    {
        public Metadata Metadata { get; set; }
        public string Abtyp { get; set; }
        public string ClearedAmt { get; set; }
        public string DataVersion { get; set; }
        public string DueAmt { get; set; }
        public string DueDt { get; set; }
        public string DueYr { get; set; }
        public string Euser { get; set; }
        public string Fbguid { get; set; }
        public string Fbnum { get; set; }
        public string FormGuid { get; set; }
        public string Inccbflag { get; set; }
        public string InstReqFor { get; set; }
        public string InvAmt { get; set; }
        public string InvCb { get; set; }
        public string InvCbDeflt { get; set; }
        public string InvNo { get; set; }
        public string Langz { get; set; }
        public long LineNo { get; set; }
        public string RankingOrder { get; set; }
        public string ReturnId { get; set; }
        public string Taxtype { get; set; }
        public string Tin { get; set; }
        public string TotAmt { get; set; }
        public string Waers { get; set; }
    }
    
    public partial class RetmsgSet
    {
        [JsonProperty("messages")]
        public List<object> results { get; set; }
    }

    
    public partial class ZakatInstalmentPlanRequest
    {
        [JsonProperty("formBundleGUID")]
        public string Fbguid { get; set; }
        [JsonProperty("userType")]
        public string UserTyp { get; set; }
        [JsonProperty("transactionType")]
        public string TxnTp { get; set; }
        [JsonProperty("mobileNumber")]
        public string MobNo { get; set; }
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        //[JsonProperty("")]
        public string DownPayReq { get; set; }
        [JsonProperty("operation")]
        public string Operation { get; set; }
        //[JsonProperty("")]
        public string OfcReason { get; set; }
        [JsonProperty("portalUser")]

        public string Euser { get; set; }
        [JsonProperty("stepNumber")]
        public string StepNumber { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("userName")]
        public string Officer { get; set; }
        [JsonProperty("language")]
        public string Langz { get; set; }
        [JsonProperty("statusCode")]
        public string Status { get; set; }
        [JsonProperty("reviseDownPaymentAmount")]
        public string SuAmt { get; set; }
        [JsonProperty("supervisiorAMT")]
        public string SuAmtFg { get; set; }
        [JsonProperty("formProcess")]
        public string Formproc { get; set; }
        [JsonProperty("TIN")]
        public string Tin { get; set; }
        [JsonProperty("periodkey")]
        public string Periodkey { get; set; }
        [JsonProperty("returnId")]
        public string ReturnId { get; set; }
        [JsonProperty("taxpayerName")]
        public string TinNm { get; set; }
        [JsonProperty("alternateMobileNumber")]
        public string AltMobNo { get; set; }
        [JsonProperty("installmentRequestedFor")]
        public string InstReqFor { get; set; }
        [JsonProperty("installmentRequestedReason")]
        public string InstReqReason { get; set; }
        [JsonProperty("totalAmount")]
        public string TotAmt { get; set; }
        [JsonProperty("downPaymentAmount")]
        public string DpAmt { get; set; }
        [JsonProperty("paymentFrequency")]
        public string PymntFreq { get; set; }
        [JsonProperty("planDuration")]
        public string PlanDur { get; set; }
        [JsonProperty("officerPaymentFrequency")]
        public string OffPymntFreq { get; set; }
        [JsonProperty("officerPlanDuration")]
        public string OffPlanDur { get; set; }
        [JsonProperty("agree")]
        public string DecCb { get; set; }
        [JsonProperty("currency")]
        public string Waers { get; set; }
        [JsonProperty("accountingMethod")]
        public string AccMethod { get; set; }
        [JsonProperty("sadadBillNumber")]
        public string Sopbel { get; set; }
        [JsonProperty("officerAmount")]
        public string OffAmt { get; set; }
        [JsonProperty("paymentDate")]
        public string PaymtDt { get; set; }
        [JsonProperty("penaltyAmount")]
        public string PenlAmt { get; set; }
        [JsonProperty("installementDueDate")]
        public string InsDtOff { get; set; }
        [JsonProperty("financialStatements")]
        public List<FnDtlSetObject> FnDtlSet { get; set; }
        [JsonProperty("attachments")]
        public object[] AttachSet { get; set; }
        [JsonProperty("notes")]
        public List<NotesSet> NotesSet { get; set; }
        [JsonProperty("investments")]
        public ZakatInvoicesResult[] invDtlsSet { get; set; }
        [JsonProperty("installementPlans")]
        public object[] insPlanSet { get; set; }
        [JsonProperty("messages")]
        public object[] retmsgSet { get; set; }
        [JsonProperty("officerInstallmentPlans")]
        public object[] insPlan_OffSet { get; set; }
    }


    
    public class ZAKATRequestPlanModel
    {
        
        public D d { get; set; }
        
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
        
        public partial class FnDtlSetObject
        {
            [JsonProperty("cashBankYear1")]
            public string CashBankY1 { get; set; }
            [JsonProperty("cashBankYear2")]
            public string CashBankY2 { get; set; }
            [JsonProperty("cashBankYear3")]
            public string CashBankY3 { get; set; }
            [JsonProperty("cashRatioYear1")]
            public string CashRatioY1 { get; set; }
            [JsonProperty("cashRatioYear2")]
            public string CashRatioY2 { get; set; }
            [JsonProperty("cashRatioYear3")]
            public string CashRatioY3 { get; set; }
            [JsonProperty("dataVersion")]
            public string DataVersion { get; set; }
            [JsonProperty("debitorsYear1")]
            public string DebitorsY1 { get; set; }
            [JsonProperty("debitorsYear2")]
            public string DebitorsY2 { get; set; }
            [JsonProperty("debitorsYear3")]
            public string DebitorsY3 { get; set; }
            [JsonProperty("portalUser")]
            public string Euser { get; set; }
            [JsonProperty("formBundleGUID")]
            public string Fbguid { get; set; }
            [JsonProperty("formBundleNumber")]
            public string Fbnum { get; set; }
            [JsonProperty("formGUID")]
            public string FormGuid { get; set; }
            [JsonProperty("formProcess")]
            public string Formproc { get; set; }
            [JsonProperty("TIN")]
            public string Gpart { get; set; }
            [JsonProperty("inventoryYear1")]
            public string InventoryY1 { get; set; }
            [JsonProperty("inventoryYear2")]
            public string InventoryY2 { get; set; }
            [JsonProperty("inventoryYear3")]
            public string InventoryY3 { get; set; }

            [JsonProperty("lineNumber")]
            public long LineNo { get; set; }
            [JsonProperty("systemCode")]
            public string Mandt { get; set; }
            [JsonProperty("netCashFlowYear1")]
            public string NcFlowY1 { get; set; }
            [JsonProperty("netCashFlowYear2")]
            public string NcFlowY2 { get; set; }
            [JsonProperty("netCashFlowYear3")]
            public string NcFlowY3 { get; set; }
            [JsonProperty("netIncomeYear1")]
            public string NetIncomeY1 { get; set; }
            [JsonProperty("netIncomeYear2")]
            public string NetIncomeY2 { get; set; }
            [JsonProperty("netIncomeYear3")]
            public string NetIncomeY3 { get; set; }
            [JsonProperty("profitabilityRatioYear1")]
            public string ProfitRatioY1 { get; set; }
            [JsonProperty("profitabilityRatioYear2")]
            public string ProfitRatioY2 { get; set; }
            [JsonProperty("profitabilityRatioYear3")]
            public string ProfitRatioY3 { get; set; }
            [JsonProperty("rankingOrder")]
            public string RankingOrder { get; set; }
            [JsonProperty("returnId")]
            public string ReturnId { get; set; }
            [JsonProperty("revenueYear1")]
            public string RevenueY1 { get; set; }
            [JsonProperty("revenueYear2")]
            public string RevenueY2 { get; set; }
            [JsonProperty("revenueYear3")]
            public string RevenueY3 { get; set; }
            [JsonProperty("statusCode")]
            public string Status { get; set; }
            [JsonProperty("shortTermInvestYear1")]
            public string StiY1 { get; set; }
            [JsonProperty("shortTermInvestYear2")]
            public string StiY2 { get; set; }
            [JsonProperty("shortTermInvestYear3")]
            public string StiY3 { get; set; }
            [JsonProperty("currentAssetsAmountYear1")]
            public string TcAssetsY1 { get; set; }
            [JsonProperty("currentAssetsAmountYear2")]
            public string TcAssetsY2 { get; set; }
            [JsonProperty("currentAssetsAmountYear3")]
            public string TcAssetsY3 { get; set; }
            [JsonProperty("currentLiabilitiesAmountYear1")]
            public string TcLiabltyY1 { get; set; }
            [JsonProperty("currentLiabilitiesAmountYear2")]
            public string TcLiabltyY2 { get; set; }
            [JsonProperty("currentLiabilitiesAmountYear3")]
            public string TcLiabltyY3 { get; set; }
            [JsonProperty("transactionType")]
            public string TxnTp { get; set; }
            [JsonProperty("userType")]
            public string UserTyp { get; set; }
            [JsonProperty("currency")]
            public string Waers { get; set; }
            [JsonProperty("financialStatementYear1")]
            public string Year1 { get; set; }
            [JsonProperty("financialStatementYear2")]
            public string Year2 { get; set; }
            [JsonProperty("financialStatementYear3")]
            public string Year3 { get; set; }
            [JsonProperty("zakatYear1")]
            public string ZakatY1 { get; set; }
            [JsonProperty("zakatYear2")]
            public string ZakatY2 { get; set; }
            [JsonProperty("zakatYear3")]
            public string ZakatY3 { get; set; }
        }
        
        public class AuthServSet
        {
            public List<object> results { get; set; }
        }
        
        public class WorklistSet
        {
            public List<object> results { get; set; }
        }
        
        public class RevokeListSet
        {
            public List<object> results { get; set; }
        }
        
        public class EvtNotif12Set
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
            public string Gpart { get; set; }
            public string Persl { get; set; }
            public string Perslt { get; set; }
            public string RetFbn { get; set; }
            public string Abrzu { get; set; }
            public string Abrzo { get; set; }
            public string Vtre2 { get; set; }
            public string Betrh { get; set; }
            public string Opbel { get; set; }
        }
        
        public class EvtNotif1Set
        {
            public List<Result> results { get; set; }
        }
        
        public class D
        {
            public Metadata __metadata { get; set; }
            public string Client { get; set; }
            public string TinNm { get; set; }
            public int Accnum { get; set; }
            public int Actcnt { get; set; }
            public string Auditor { get; set; }
            public bool AudObjection { get; set; }
            public bool AudRequest { get; set; }
            public string Bpnum { get; set; }
            public string Branch { get; set; }
            public string CallServ { get; set; }
            public string Caltype { get; set; }
            public int Cnlcnt { get; set; }
            public string Dept { get; set; }
            public bool EnableInstPlan { get; set; }
            public bool EnableTile { get; set; }
            public string Ettr { get; set; }
            public string Euser { get; set; }
            public string Euser1 { get; set; }
            public string Euser2 { get; set; }
            public string Euser3 { get; set; }
            public string Euser4 { get; set; }
            public string Euser5 { get; set; }
            public string Fbguid { get; set; }
            public string Fbnum { get; set; }
            public string HostName { get; set; }
            public string IntPortal { get; set; }
            public bool IsBankruptcy { get; set; }
            public string Lang { get; set; }
            public string Name { get; set; }
            public bool NotifLogFlag { get; set; }
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
            public string Title { get; set; }
            public string Type { get; set; }
            public bool UpdregOutflag { get; set; }
            public string UserTin { get; set; }
            public string UserTyp { get; set; }
            public string Zuser { get; set; }
            public AuthServSet AuthServSet { get; set; }
            public WorklistSet WorklistSet { get; set; }
            public RevokeListSet RevokeListSet { get; set; }
            public EvtNotif12Set EvtNotif12Set { get; set; }
            public EvtNotif1Set EvtNotif1Set { get; set; }
        }
    }
    
    public partial class ZakatInvoiceList
    {
        [JsonProperty("data")]
        public List<ZakatInvoicesResult> d { get; set; }
        //public ZakatInvoices d { get; set; }
    }
    
    public partial class ZakatInvoices
    {
        public List<ZakatInvoicesResult> results { get; set; }
    }
    
    public partial class ZakatInvoicesResult
    {
        // public Metadata __metadata { get; set; }
        [JsonProperty("invoiceDefault")]
        public string InvCbDeflt { get; set; }
        [JsonProperty("dueYear")]
        public string DueYr { get; set; }
        [JsonProperty("totalAmount")]
        public string TotAmt { get; set; }
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [JsonProperty("formBundleGUID")]
        public string Fbguid { get; set; }
        [JsonProperty("isINCCB")]
        public string Inccbflag { get; set; }
        [JsonProperty("returnId")]
        public string ReturnId { get; set; }
        [JsonProperty("TIN")]
        public string Tin { get; set; }
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [JsonProperty("taxType")]
        public string Taxtype { get; set; }
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [JsonProperty("portalUser")]
        public string Euser { get; set; }
        [JsonProperty("language")]
        public string Langz { get; set; }
        [JsonProperty("lineNumber")]
        public long LineNo { get; set; }
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; }
        [JsonProperty("invoice")]
        public string InvCb { get; set; }
        [JsonProperty("documentNumber")]
        public string InvNo { get; set; }
        [JsonProperty("dueDate")]
        public string DueDt { get; set; }
        [JsonProperty("invoicedAmount")]
        public string InvAmt { get; set; }
        [JsonProperty("clearedAmount")]
        public string ClearedAmt { get; set; }
        [JsonProperty("dueAmount")]
        public string DueAmt { get; set; }
        [JsonProperty("installmentRequestedFor")]
        public string InstReqFor { get; set; }
        [JsonProperty("revenueType")]
        public string Abtyp { get; set; }
        [JsonProperty("currency")]
        public string Waers { get; set; }
        [JsonProperty("additionalReference")]
        public string Vtre2 { get; set; }
        [JsonProperty("statusCode")]
        public string Status { get; set; }
        [JsonProperty("userType")]
        public string UserTyp { get; set; }
        public string transactionType { get; set; }
        [JsonProperty("investment")]
        public string Investment { get { return InvCb; } set { InvCb = value; } }
        [JsonProperty("detailsOfInvoice")]
        public string DetailsOfInvoice { get { return InvCbDeflt; } set { InvCbDeflt = value; } }
        [JsonProperty("investmentNumber")]
        public string InvestmentNumber { get { return InvNo; } set { InvNo = value; } }
        [JsonProperty("isInvoice")]
        public string isInvoice { get { return Inccbflag; } set { Inccbflag = value; } }

    }


    
    public class ZakatInstalmentPlanRequestListModel
    {
        
        [JsonProperty("data")]
        public D d { get; set; }
        
        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
        
        public class AuthServSet
        {
            [JsonProperty("authorizationServers")]
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
            [JsonProperty("dueAmount")]
            public string DueAmt { get; set; }
            [JsonProperty("formBundleType")]
            public string Fbtyp { get; set; }
            [JsonProperty("paymentFrequency")]
            public string PymntFreq { get; set; }
            [JsonProperty("submitDate")]
            public string SubmitDt { get; set; }
            [JsonProperty("formBundleStatus")]
            public string Fbsta { get; set; }
            [JsonProperty("userType")]
            public string UserTyp { get; set; }
            [JsonProperty("userStatus")]
            public string Fbust { get; set; }
            [JsonProperty("TIN")]
            public string Tin { get; set; }
            [JsonProperty("formBundleNumber")]
            public string Fbnum { get; set; }
            [JsonProperty("totalAmount")]
            public string TotAmt { get; set; }
            [JsonProperty("depositedAmount")]
            public string DpAmt { get; set; }
            [JsonProperty("planDuration")]
            public string PlanDur { get; set; }
            [JsonProperty("currency")]
            public string Waers { get; set; }
            [JsonProperty("statusDescription")]
            public string Status { get; set; }
            [JsonProperty("IPType")]
            public string IptypeFg { get; set; }

        }
        
        public class WorklistSet
        {
            [JsonProperty("worklist")]
            public List<Result> results { get; set; }
        }
        
        public class RevokeListSet
        {
            [JsonProperty("revokeList")]
            public List<RevokeListResult> results { get; set; }
        }
        
        public partial class RevokeListResult
        {
            public Metadata __metadata { get; set; }
            [JsonProperty("userType")]
            public string UserTyp { get; set; }
            [JsonProperty("formBundleType")]
            public string Fbtyp { get; set; }
            [JsonProperty("TIN")]
            public string Tin { get; set; }
            [JsonProperty("formBundleNumber")]
            public string Fbnum { get; set; }
            [JsonProperty("submitDate")]
            public string SubmitDt { get; set; }
            [JsonProperty("totalAmount")]
            public string TotAmt { get; set; }
            [JsonProperty("depositedAmount")]
            public string DpAmt { get; set; }
            [JsonProperty("dueAmount")]
            public string DueAmt { get; set; }
            [JsonProperty("paymentFrequency")]
            public string PymntFreq { get; set; }
            [JsonProperty("planDuration")]
            public string PlanDur { get; set; }
            [JsonProperty("currency")]
            public string Waers { get; set; }
            [JsonProperty("formBundleStatus")]
            public string Fbsta { get; set; }
            [JsonProperty("statusDescription")]
            public string Fbust { get; set; }
            [JsonProperty("userStatus")]
            public string Status { get; set; }
            [JsonProperty("isRevoke")]
            public bool Revoke { get; set; }
        }

        
        public class EvtNotif12Set
        {
            [JsonProperty("notifications12")]
            public List<Result2> results { get; set; }
        }
        
        public class Result2
        {
            public Metadata __metadata { get; set; }
            public string AAmtTb { get; set; }
            public string AClearedAmtTb { get; set; }
            public string ADueAmtTb { get; set; }
            public string ADueDtTb { get; set; }
            public string AIvAmtTb { get; set; }
            public string AIvNoTb { get; set; }
            public string AIvSrNoTb { get; set; }
            public string AIvTb { get; set; }
            public string AIvAbtyp { get; set; }
        }
        
        public class EvtNotif1Set
        {
            [JsonProperty("notifications1")]
            public List<object> results { get; set; }
        }
        
        public class D
        {
            public Metadata __metadata { get; set; }
            [JsonProperty("client")]
            public string Client { get; set; }
            [JsonProperty("taxpayerName")]
            public string TinNm { get; set; }
            [JsonProperty("accountNumber")]
            public int Accnum { get; set; }
            [JsonProperty("activeCount")]
            public int Actcnt { get; set; }
            [JsonProperty("auditor")]
            public string Auditor { get; set; }
            [JsonProperty("isAuditorObjection")]
            public bool AudObjection { get; set; }
            [JsonProperty("isAuditorRequest")]
            public bool AudRequest { get; set; }
            [JsonProperty("TIN")]
            public string Bpnum { get; set; }
            [JsonProperty("branch")]
            public string Branch { get; set; }
            [JsonProperty("callService")]
            public string CallServ { get; set; }
            [JsonProperty("calendarType")]
            public string Caltype { get; set; }
            [JsonProperty("cancellationCount")]
            public int Cnlcnt { get; set; }
            [JsonProperty("department")]
            public string Dept { get; set; }
            [JsonProperty("isEnableInstallmentPlan")]
            public bool EnableInstPlan { get; set; }
            [JsonProperty("isEnableTile")]
            public bool EnableTile { get; set; }
            [JsonProperty("patchLevel")]
            public string Ettr { get; set; }
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
            [JsonProperty("hostName")]
            public string HostName { get; set; }
            [JsonProperty("portalLink")]
            public string IntPortal { get; set; }
            [JsonProperty("isBankruptcy")]
            public bool IsBankruptcy { get; set; }
            [JsonProperty("language")]
            public string Lang { get; set; }
            [JsonProperty("fullName")]
            public string Name { get; set; }
            [JsonProperty("isNotificationLog")]
            public bool NotifLogFlag { get; set; }
            [JsonProperty("objectionNumber")]
            public int Oblnum { get; set; }
            [JsonProperty("overdueAmount")]
            public string Overdue { get; set; }
            // [JsonProperty("")]
            public string Penalty { get; set; }
            [JsonProperty("portNumber")]
            public string PortNo { get; set; }
            [JsonProperty("protocol")]
            public string Protocol { get; set; }
            [JsonProperty("referenceNumber")]
            public int Refnum { get; set; }
            [JsonProperty("registrationNumber")]
            public int Regnum { get; set; }
            [JsonProperty("renewalCount")]
            public int Rencnt { get; set; }
            [JsonProperty("requestNumber")]
            public int Reqnum { get; set; }
            [JsonProperty("returnCount")]
            public int RetItCnt { get; set; }
            [JsonProperty("return")]
            public string RetItFlg { get; set; }
            [JsonProperty("systemName")]
            public string SystemName { get; set; }
            [JsonProperty("taxTypeDescription")]
            public string Taxtype { get; set; }
            [JsonProperty("title")]
            public string Title { get; set; }
            [JsonProperty("partnerCategory")]
            public string Type { get; set; }
            [JsonProperty("isOutletUpgrade")]
            public bool UpdregOutflag { get; set; }
            [JsonProperty("userTIN")]
            public string UserTin { get; set; }
            [JsonProperty("userType")]
            public string UserTyp { get; set; }
            [JsonProperty("email")]
            public string Zuser { get; set; }
            [JsonProperty("authorizationServers")]
            public List<object> AuthServSet { get; set; }
            [JsonProperty("worklist")]
            public List<Result> WorklistSet { get; set; }
            [JsonProperty("revokeList")]
            public List<RevokeListResult> RevokeListSet { get; set; }
            [JsonProperty("notifications12")]
            public List<Result2> EvtNotif12Set { get; set; }
            [JsonProperty("notifications1")]
            public List<object> EvtNotif1Set { get; set; }
        }

    }


    
    public partial class ZakatRequestDisplayModel
    {
        public D d { get; set; }
    }
    
    public partial class D
    {
        public Metadata __metadata { get; set; }
        public string Percentage { get; set; }
        public string UserTin { get; set; }
        public string Euser { get; set; }
        public string Fbguid { get; set; }
        public string Auditorz { get; set; }
        public string Langz { get; set; }
        public string Taxpayerz { get; set; }
        public string Savez { get; set; }
        public string Fbnumz { get; set; }
        public string PeriodKeyz { get; set; }
        public string Submitz { get; set; }
        public string AAgree { get; set; }
        public string ADwPaymentReqLetter { get; set; }
        public string AEffectiveCalTp { get; set; }
        public string Euser1 { get; set; }
        public string FbtypDescr { get; set; }
        public string FormGuid { get; set; }
        public string ZauditorFlag { get; set; }
        public string ANoOfInstTp { get; set; }
        public string ARev1YrTbFg { get; set; }
        public string SavNot { get; set; }
        public string Approvez { get; set; }
        public string ARev2YrTbFg { get; set; }
        public string ALegalEnty1 { get; set; }
        public string ARev3YrTbFg { get; set; }
        public string ANi1YrTbFg { get; set; }
        public string Rejectz { get; set; }
        public string ANi2YrTbFg { get; set; }
        public string CaseGuid { get; set; }
        public string ANi3YrTbFg { get; set; }
        public string AStep { get; set; }
        public string ACb1YrTbFg { get; set; }
        public string ACb2YrTbFg { get; set; }
        public string ALegalEnty2 { get; set; }
        public string ACb3YrTbFg { get; set; }
        public string ASi1YrTbFg { get; set; }
        public string ASi2YrTbFg { get; set; }
        public string RegIdz { get; set; }
        public string ASi3YrTbFg { get; set; }
        public string ATa1YrTbFg { get; set; }
        public string ATa2YrTbFg { get; set; }
        public string ATa3YrTbFg { get; set; }
        public string Fbnum { get; set; }
        public string ATl1YrTbFg { get; set; }
        public string ATin { get; set; }
        public string ATl2YrTbFg { get; set; }
        public string ATaxpayerNm { get; set; }
        public string ATl3YrTbFg { get; set; }
        public string ADeb1YrTbFg { get; set; }
        public string ATelNo { get; set; }
        public string ADeb2YrTbFg { get; set; }
        public string AMobNo { get; set; }
        public string ADeb3YrTbFg { get; set; }
        public string AEmail { get; set; }
        public string ACr1YrTbFg { get; set; }
        public string AInstReqFor { get; set; }
        public string ACr2YrTbFg { get; set; }
        public string AInstReqReason { get; set; }
        public string ABnkStat3MhChk { get; set; }
        public string ACr3YrTbFg { get; set; }
        public string AFinStat3YrChk { get; set; }
        public string ARe1YrTbFg { get; set; }
        public string AOtherDocChk { get; set; }
        public string ARe2YrTbFg { get; set; }
        public string AHoldFinStat { get; set; }
        public string ARe3YrTbFg { get; set; }
        public string ADpAmtFg { get; set; }
        public string AItTb { get; set; }
        public string AOneYrTb { get; set; }
        public string ATwoYrTb { get; set; }
        public string AThreeYrTb { get; set; }
        public string ADpAmt { get; set; }
        public string APlanDurNo { get; set; }
        public string APlanDurPeri { get; set; }
        public string APaymentFreq { get; set; }
        public string ACoPlanDurPeri { get; set; }
        public string ACoPaymentFreq { get; set; }
        public string ADpRequ { get; set; }
        public string ADpDocNo { get; set; }
        public string ADpPer { get; set; }
        public string ACoDpAmt { get; set; }
        public string ADpRecAmt { get; set; }
        public string AAppInstAmt { get; set; }
        public string AInstDpAmt { get; set; }
        public string ABalAmt { get; set; }
        public string ACoBoRev { get; set; }
        public string ACoBoNm { get; set; }
        public string ACoBoRd { get; set; }
        public string ACmBoRev { get; set; }
        public string ACmBoNm { get; set; }
        public string ACmBoRd { get; set; }
        public string ACoHoRev { get; set; }
        public string ACoHoNm { get; set; }
        public string ACoHoRd { get; set; }
        public string ACmHoRev { get; set; }
        public string ACmHoNm { get; set; }
        public string ACmHoRd { get; set; }
        public string AMofApprChk { get; set; }
        public string AOtherSuppDocChk { get; set; }
        public string ARejReason { get; set; }
        public string ACoPlanDurNo { get; set; }
        public string ARev1YrTb { get; set; }
        public string ARev2YrTb { get; set; }
        public string ARev3YrTb { get; set; }
        public string ANi1YrTb { get; set; }
        public string ANi2YrTb { get; set; }
        public string ANi3YrTb { get; set; }
        public string ACb1YrTb { get; set; }
        public string ACb2YrTb { get; set; }
        public string ACb3YrTb { get; set; }
        public string ASi1YrTb { get; set; }
        public string ASi2YrTb { get; set; }
        public string ASi3YrTb { get; set; }
        public string ATa1YrTb { get; set; }
        public string ATa2YrTb { get; set; }
        public string ATa3YrTb { get; set; }
        public string ATl1YrTb { get; set; }
        public string ATl2YrTb { get; set; }
        public string ATl3YrTb { get; set; }
        public string ADeb1YrTb { get; set; }
        public string ADeb2YrTb { get; set; }
        public string ADeb3YrTb { get; set; }
        public string ACr1YrTb { get; set; }
        public string ACr2YrTb { get; set; }
        public string ACr3YrTb { get; set; }
        public string ARe1YrTb { get; set; }
        public string ARe2YrTb { get; set; }
        public string ARe3YrTb { get; set; }
        public string APr1YrTb { get; set; }
        public string APr2YrTb { get; set; }
        public string APr3YrTb { get; set; }
        public string ACrt1YrTb { get; set; }
        public string ACrt2YrTb { get; set; }
        public string ACrt3YrTb { get; set; }
        public string APc1YrTb { get; set; }
        public string APc2YrTb { get; set; }
        public string APc3YrTb { get; set; }
        public string APerAmtRd { get; set; }
        public string ADpRequDrp { get; set; }
        public string APer { get; set; }
        public string AFormStatus { get; set; }
        public string ADownLetterChk { get; set; }
        public string ADownYear { get; set; }
        public string ADownMonth { get; set; }
        public string ADownToYear { get; set; }
        public string ADownToMonth { get; set; }
        public string ABranch { get; set; }
        public string ASaudiShare { get; set; }
        public string ANonsaudiShare { get; set; }
        public string AMainAct { get; set; }
        public string AMainActDesc { get; set; }
        public string APoBox { get; set; }
        public string APostalCode { get; set; }
        public string AFaxNo { get; set; }
        public string ABuilding { get; set; }
        public string AStreet { get; set; }
        public string ADistrict { get; set; }
        public string ACity { get; set; }
        public string ALvError { get; set; }
        public string ATotalAmt { get; set; }
        public string Status { get; set; }
        public AttDetSet AttDetSet { get; set; }
        public OffNotesSet Off_notesSet { get; set; }
        public ZInvoiceSet z_invoiceSet { get; set; }
        public ZProposedinsSet z_proposedinsSet { get; set; }
        public ZInvoiceUi5Set Z_INVOICE_UI5Set { get; set; }
    }
    
    public partial class AttDetSet
    {
        public List<object> results { get; set; }
    }
    

    public partial class OffNotesSet
    {
        public List<NotesSet> results { get; set; }
    }
    
    public partial class ZInvoiceSet
    {
        public List<object> results { get; set; }
    }
    
    public partial class ZInvoiceUi5Set
    {
        public List<Results3> results { get; set; }
    }
    
    public partial class Results3
    {
        public Metadata Metadata { get; set; }
        public string AAmtTb { get; set; }
        public string AClearedAmtTb { get; set; }
        public string ADueAmtTb { get; set; }
        public string ADueDtTb { get; set; }
        public string AIvAmtTb { get; set; }
        public string AIvNoTb { get; set; }
        public string AIvSrNoTb { get; set; }
        public string AIvTb { get; set; }
        public string AIvAbtyp { get; set; }
    }
    
    public partial class ZProposedinsSet
    {
        public List<object> results { get; set; }
    }
    

    public class ZakatSummaryInputModel
    {
        
        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
        
        public class D
        {
            //public Metadata __metadata { get; set; }
            [JsonProperty("month")]
            public string Monthz { get; set; }
            [JsonProperty("TIN")]
            public string Gpart { get; set; }
            [JsonProperty("language")]
            public string Lang { get; set; }
            //[JsonProperty("")]
            //public string Begda { get; set; }
            //[JsonProperty("")]
            //public string Endda { get; set; }
            [JsonProperty("obligation")]
            public string ObligFlag { get; set; }
            [JsonProperty("taxType")]
            public string TaxtpFg { get; set; }
            [JsonProperty("userTIN")]
            public string UserTin { get; set; }
            [JsonProperty("contractNumber")]
            public string Vtref { get; set; }
            [JsonProperty("inboundCorrespondenceType")]
            public string Incotyp { get; set; }
            [JsonProperty("inboundCorrespondenceDescription")]
            public string Incotext { get; set; }
            [JsonProperty("formBundleType")]
            public string Fbtyp { get; set; }
            [JsonProperty("formBundleTypeDescription")]
            public string FbtText { get; set; }
            [JsonProperty("due")]
            public string Due { get; set; }
            [JsonProperty("periodkey")]
            public string Persl { get; set; }
            [JsonProperty("taxPeriod")]
            public string TaxPeriod { get; set; }
            //[JsonProperty("")]
            //public string DueDt { get; set; }
            [JsonProperty("formBundleNumber")]
            public string Fbnum { get; set; }
            [JsonProperty("status")]
            public string Status { get; set; }
            [JsonProperty("calendarType")]
            public string CalendrTyp { get; set; }
            //[JsonProperty("")]
            //public string Abrzu { get; set; }
            //[JsonProperty("")]
            //public string Abrzo { get; set; }
            //  [JsonProperty("")]
            public string Stat { get; set; }
            [JsonProperty("sadadBillNumber1")]
            public string SadadDoc1 { get; set; }
            [JsonProperty("sadadBillNumber2")]
            public string SadadDoc2 { get; set; }
            [JsonProperty("informationMessage")]
            public string InfoMsg { get; set; }
            // [JsonProperty("")]
            public string Statfg { get; set; }
            [JsonProperty("period")]
            public string Period { get; set; }
            // [JsonProperty("")]
            public string Statflag { get; set; }
            // [JsonProperty("")]
            public string DueDtC { get; set; }
            [JsonProperty("flag")]
            public string Flag { get; set; }
            [JsonProperty("combination")]
            public string Comb { get; set; }
            [JsonProperty("auditor")]
            public string Auditor { get; set; }
            [JsonProperty("isAuditor")]
            public bool AudFlag { get; set; }
            [JsonProperty("branchDescription")]
            public string Branch { get; set; }
            //[JsonProperty("")]
            //public string Aedat { get; set; }
            [JsonProperty("isOpen")]
            public bool Open { get; set; }
            [JsonProperty("message")]
            public string Msg { get; set; }
            [JsonProperty("display")]
            public string Dispflag { get; set; }
            [JsonProperty("serialNumber")]
            public string Euser { get; set; }
            [JsonProperty("formBundleGUID")]
            public string Fbguid { get; set; }
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
            [JsonProperty("returnGUID")]
            public string Retguid { get; set; }
            [JsonProperty("live")]
            public string Liveflg { get; set; }
            [JsonProperty("userError")]
            public string UserErrFg { get; set; }
            [JsonProperty("taxOfficerUID")]
            public string TaxOffUid { get; set; }
        }
        
        [JsonProperty("data")]
        public D d { get; set; }



    }
    
    public class ZakatSummaryInputModel1
    {

        

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

        public class Metadata
        {

            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
        
        public class InvoicesUI5
        {
            public string amount { get; set; }
            public string clearedAmount { get; set; }
            public string dueAmount { get; set; }
            public DateTime dueDate { get; set; }
            public string invoiceAmount { get; set; }
            public string invoiceNumber { get; set; }
            public string invoiceSerialNumber { get; set; }
            public string invoice { get; set; }
            public string taxType { get; set; }
        }
        
        public class D
        {
            public string taxPeriod { get; set; }
            public string sadadBillNumber1 { get; set; }
            public string sadadBillNumber2 { get; set; }
            public string statusCode { get; set; }
            public bool refundFiled { get; set; }
            public string period { get; set; }
            public bool isOpen { get; set; }
            public string authenticationUser { get; set; }
            public string authenticationUser5 { get; set; }
            public string authenticationUser4 { get; set; }
            public string authenticationUser3 { get; set; }
            public string authenticationUser2 { get; set; }
            public string authenticationUser1 { get; set; }
            public string formBundleGUID { get; set; }
            public string formBundleNumber { get; set; }
            public string branch { get; set; }
            public string combination { get; set; }
            public string auditor { get; set; }
            public string month { get; set; }
            public string errorMessage { get; set; }
            public bool isObjectionFiled { get; set; }
            public string inboundCorrespondenceDescription { get; set; }
            public string inboundCorrespondenceType { get; set; }
            public string informationMessage { get; set; }
            public string selected { get; set; }
            public string formBundleTypeDescription { get; set; }
            public string display { get; set; }
            public string language { get; set; }
            public string formBundleType { get; set; }
            public string TIN { get; set; }
            public string periodkey { get; set; }
            public string dueStatus { get; set; }
            public string statusDescription { get; set; }
            public string dueDateCharacter { get; set; }
            public string obligation { get; set; }
            public bool auditorNumber { get; set; }
            public string contractNumber { get; set; }
        }
        
        [JsonProperty("data")]
        public D d { get; set; }
    }

    
    public partial class ZakatInstalmentPlanRevokeResponse
    {
        [JsonProperty("data")]
        public ZakatRevoke d { get; set; }

        //public static implicit operator ZakatInstalmentPlanRevokeResponse(ZakatInstalmentPlanResponse v)
        //{
        //    throw new NotImplementedException();
        //}
    }
    
    public partial class ZakatRevoke
    {
        public Metadata __metadata { get; set; }
        [JsonProperty("lockingStatus")]
        public string Lokst { get; set; }
        [JsonProperty("formBundleGUID")]
        public string Fbguid { get; set; }
        //[JsonProperty("")]
        public string FbnumIprr { get; set; }
        [JsonProperty("installementDueDate")]
        public string InsDtOff { get; set; }
        [JsonProperty("penaltyAmount")]
        public string PenlAmt { get; set; }
        [JsonProperty("supervisiorAMT")]
        public string SuAmtFg { get; set; }
        [JsonProperty("officerAmount")]
        public string OffAmt { get; set; }
        [JsonProperty("totalAmount")]
        public string TotAmt { get; set; }
        [JsonProperty("userType")]
        public string UserTyp { get; set; }
        [JsonProperty("downPaymentAmount")]
        public string DpAmt { get; set; }
        [JsonProperty("formMode")]
        public string FormMode { get; set; }
        [JsonProperty("paymentDate")]
        public string PaymtDt { get; set; }
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [JsonProperty("sadadBillNumber")]
        public string Sopbel { get; set; }
        [JsonProperty("transactionType")]
        public string TxnTp { get; set; }
        [JsonProperty("mobileNumber")]
        public string MobNo { get; set; }
        [JsonProperty("days")]
        public string Zdays { get; set; }
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [JsonProperty("operation")]
        public string Operation { get; set; }
        // [JsonProperty("")]
        public string Euser { get; set; }
        [JsonProperty("stepNumber")]
        public string StepNumber { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        //[JsonProperty("")]
        public string Officer { get; set; }
        [JsonProperty("language")]
        public string Langz { get; set; }
        [JsonProperty("statusCode")]
        public string Status { get; set; }
        [JsonProperty("formProcess")]
        public string Formproc { get; set; }
        [JsonProperty("reviseDownPaymentAmount")]
        public string SuAmt { get; set; }
        [JsonProperty("periodKey")]
        public string Periodkey { get; set; }
        [JsonProperty("TIN")]
        public string Tin { get; set; }
        [JsonProperty("returnId")]
        public string ReturnId { get; set; }
        [JsonProperty("taxpayerName")]
        public string TinNm { get; set; }
        [JsonProperty("alternateMobileNumber")]
        public string AltMobNo { get; set; }
        [JsonProperty("installmentRequestedFor")]
        public string InstReqFor { get; set; }
        [JsonProperty("installmentRequestedReason")]
        public string InstReqReason { get; set; }
        [JsonProperty("paymentFrequency")]
        public string PymntFreq { get; set; }
        [JsonProperty("planDuration")]
        public string PlanDur { get; set; }
        [JsonProperty("officerPaymentFrequency")]
        public string OffPymntFreq { get; set; }
        [JsonProperty("officerPlanDuration")]
        public string OffPlanDur { get; set; }
        [JsonProperty("agree")]
        public string DecCb { get; set; }
        [JsonProperty("currency")]
        public string Waers { get; set; }
        [JsonProperty("accountingMethod")]
        public string AccMethod { get; set; }
        [JsonProperty("financialStatements")]
        public List<Set> FnDtlSet { get; set; }
        [JsonProperty("officerInstallmentPlans")]
        public List<Set> insPlan_OffSet { get; set; }
        [JsonProperty("investments")]
        public List<Set> invDtlsSet { get; set; }
        [JsonProperty("attachments")]
        public List<Attachment> AttachSet { get; set; }
        [JsonProperty("installementPlans")]
        public List<Set> insPlanSet { get; set; }
        [JsonProperty("notes")]
        public List<NotesSet> NotesSet { get; set; }
        [JsonProperty("messages")]
        public List<Set> retmsgSet { get; set; }
    }

    
    public partial class AttachSetResults
    {
        public Metadata __metadata { get; set; }
        [JsonProperty("returnGUID")]
        public string RetGuid { get; set; }
        [JsonProperty("sequenceNumber")]
        public string Seqno { get; set; }
        //[JsonProperty("")]
        public string SchGuid { get; set; }
        //[JsonProperty("")]
        public string Dotyp { get; set; }
        [JsonProperty("serialNumber")]
        public long Srno { get; set; }
        [JsonProperty("documentId")]
        public string Doguid { get; set; }
        [JsonProperty("attachedByPerson")]
        public string AttBy { get; set; }
        [JsonProperty("filename")]
        public string Filename { get; set; }
        [JsonProperty("fileExtension")]
        public string FileExtn { get; set; }
        [JsonProperty("MIMEType")]
        public string Mimetype { get; set; }
        [JsonProperty("portalUser")]
        public string ByPusr { get; set; }
        [JsonProperty("entryDate")]
        public string Erfdt { get; set; }
        [JsonProperty("createdAt")]
        public string Erftm { get; set; }
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [JsonProperty("documentURL")]
        public string DocUrl { get; set; }
        [JsonProperty("outletReference")]
        public string OutletRef { get; set; }
        [JsonProperty("enableEdit")]
        public string Enbedit { get; set; }
        [JsonProperty("enableDelete")]
        public string Enbdele { get; set; }
    }



    
    public partial class Set
    {
        public Deferred Deferred { get; set; }
    }
    
    public partial class Deferred
    {
        public Uri Uri { get; set; }
    }
    
    public partial class NotesSetResult
    {
        [JsonProperty("notes")]
        public List<NotesSet> results { get; set; }
    }
    
    public partial class NotesSet
    {
        public Metadata __metadata { get; set; }
        [JsonProperty("notesNumber")]
        public string Notenoz { get; set; }
        [JsonProperty("referenceName")]
        public string Refnamez { get; set; }

        public string XInvoicez { get; set; }
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
        public string Noteno { get; set; }
        [JsonProperty("lineNumber")]
        public long Lineno { get; set; }
        [JsonProperty("elementNumber")]
        public long ElemNo { get; set; }
        [JsonProperty("notesFormat")]
        public string Tdformat { get; set; }
        [JsonProperty("notesDescription")]
        public string Tdline { get; set; }
        [JsonProperty("section")]
        public string section { get; set; }
        [JsonProperty("startDate")]
        public string startDate { get; set; }

    }
    
    public partial class ZakatInstalmentPlanRevokeRequest
    {
        [JsonProperty("formBundleGUID")]
        public string Fbguid { get; set; }
        [JsonProperty("userType")]
        public string UserTyp { get; set; }
        [JsonProperty("transactionType")]
        public string TxnTp { get; set; }
        [JsonProperty("mobileNumber")]
        public string MobNo { get; set; }
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [JsonProperty("operation")]
        public string Operation { get; set; }
        [JsonProperty("portalUser")]
        public string Euser { get; set; }
        [JsonProperty("stepNumber")]
        public string StepNumber { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("userName")]
        public string Officer { get; set; }
        [JsonProperty("language")]
        public string Langz { get; set; }
        [JsonProperty("statusCode")]
        public string Status { get; set; }
        //[JsonProperty("")]
        public string FbnumIprr { get; set; }
        [JsonProperty("reviseDownPaymentAmount")]
        public string SuAmt { get; set; }
        [JsonProperty("supervisiorAMT")]
        public string SuAmtFg { get; set; }
        [JsonProperty("formProcess")]
        public string Formproc { get; set; }
        [JsonProperty("TIN")]
        public string Tin { get; set; }
        [JsonProperty("periodkey")]
        public string Periodkey { get; set; }
        [JsonProperty("returnId")]
        public string ReturnId { get; set; }
        [JsonProperty("taxpayerName")]
        public string TinNm { get; set; }
        [JsonProperty("alternateMobileNumber")]
        public string AltMobNo { get; set; }
        [JsonProperty("installmentRequestedFor")]
        public string InstReqFor { get; set; }
        [JsonProperty("installmentRequestedReason")]
        public string InstReqReason { get; set; }
        [JsonProperty("totalAmount")]
        public string TotAmt { get; set; }
        [JsonProperty("downPaymentAmount")]
        public string DpAmt { get; set; }
        [JsonProperty("paymentFrequency")]
        public string PymntFreq { get; set; }
        [JsonProperty("planDuration")]
        public string PlanDur { get; set; }
        [JsonProperty("officerPaymentFrequency")]
        public string OffPymntFreq { get; set; }
        [JsonProperty("officerPlanDuration")]
        public string OffPlanDur { get; set; }
        [JsonProperty("agree")]
        public string DecCb { get; set; }
        [JsonProperty("currency")]
        public string Waers { get; set; }
        [JsonProperty("accountingMethod")]
        public string AccMethod { get; set; }
        [JsonProperty("sadadBillNumber")]
        public string Sopbel { get; set; }
        [JsonProperty("officerAmount")]
        public string OffAmt { get; set; }
        [JsonProperty("paymentDate")]
        public string PaymtDt { get; set; }
        [JsonProperty("penaltyAmount")]
        public string PenlAmt { get; set; }
        [JsonProperty("installementDueDate")]
        public String InsDtOff { get; set; }
        [JsonProperty("financialStatements")]
        public object[] FnDtlSet { get; set; }
        [JsonProperty("attachments")]
        public AttachSetResults[] AttachSet { get; set; }
        [JsonProperty("notes")]
        public NotesSet[] NotesSet { get; set; }
        [JsonProperty("investments")]
        public ZakatInvoicesResult[] invDtlsSet { get; set; }
        [JsonIgnore]
        public object[] insPlanSet { get; set; }
        [JsonIgnore]
        public object[] retmsgSet { get; set; }
        [JsonIgnore]
        public object[] insPlan_OffSet { get; set; }
    }

    
    public class ZakatInstalmentInvListModel
    {

        
        [JsonProperty("data")]
        public List<Result> d { get; set; }
        
        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }


        
        public class Result
        {
            public Metadata __metadata { get; set; }
            [JsonProperty("invoiceDefault")]
            public string InvCbDeflt { get; set; }
            [JsonProperty("dueYear")]
            public string DueYr { get; set; }
            [JsonProperty("totalAmount")]
            public string TotAmt { get; set; }
            [JsonProperty("formBundleNumber")]
            public string Fbnum { get; set; }
            [JsonProperty("formBundleGUID")]
            public string Fbguid { get; set; }
            [JsonProperty("isINCCB")]
            public string Inccbflag { get; set; }
            [JsonProperty("returnId")]
            public string ReturnId { get; set; }
            [JsonProperty("TIN")]
            public string Tin { get; set; }
            [JsonProperty("formGUID")]
            public string FormGuid { get; set; }
            [JsonProperty("taxType")]
            public string Taxtype { get; set; }
            [JsonProperty("dataVersion")]
            public string DataVersion { get; set; }
            [JsonProperty("portalUser")]
            public string Euser { get; set; }
            [JsonProperty("language")]
            public string Langz { get; set; }
            [JsonProperty("lineNumber")]
            public int LineNo { get; set; }
            [JsonProperty("rankingOrder")]
            public string RankingOrder { get; set; }
            [JsonProperty("invoice")]
            public string InvCb { get; set; }
            [JsonProperty("documentNumber")]
            public string InvNo { get; set; }
            [JsonProperty("dueDate")]
            public string DueDt { get; set; }
            [JsonProperty("invoicedAmount")]
            public string InvAmt { get; set; }
            [JsonProperty("clearedAmount")]
            public string ClearedAmt { get; set; }
            [JsonProperty("dueAmount")]
            public string DueAmt { get; set; }
            [JsonProperty("installmentRequestedFor")]
            public string InstReqFor { get; set; }
            [JsonProperty("revenueType")]
            public string Abtyp { get; set; }
            [JsonProperty("currency")]
            public string Waers { get; set; }
        }
        
        public class D
        {
            
            public List<Result> results { get; set; }

        }

    }
    
    public class SummaryDisplayModel
    {
        
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }

        

        public class FnDtlSet
        {
            [JsonProperty("financialStatements")]
            public List<object> results { get; set; }
        }


        
        public class Deferred
        {
            public string uri { get; set; }
        }

        

        public class InsPlanOffSet
        {
            [JsonProperty("officerInstallmentPlans")]
            public Deferred __deferred { get; set; }
        }

        

        public class Deferred2
        {
            public string uri { get; set; }
        }

        

        public class InvDtlsSet
        {
            [JsonProperty("investments")]
            public Deferred2 __deferred { get; set; }
        }
        


        public class Metadata2
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }

        
        public class Result
        {
            //public Metadata2 __metadata { get; set; }
            [JsonProperty("returnGUID")]
            public string RetGuid { get; set; }
            [JsonProperty("sequenceNumber")]
            public string Seqno { get; set; }
            [JsonProperty("formGUID")]
            public string SchGuid { get; set; }
            [JsonProperty("documentCategory")]
            public string Dotyp { get; set; }
            [JsonProperty("serialNumber")]
            public int Srno { get; set; }
            [JsonProperty("documentId")]
            public string Doguid { get; set; }
            [JsonProperty("attachedByPerson")]
            public string AttBy { get; set; }
            [JsonProperty("filename")]
            public string Filename { get; set; }
            [JsonProperty("fileExtension")]
            public string FileExtn { get; set; }
            [JsonProperty("MIMEType")]
            public string Mimetype { get; set; }
            [JsonProperty("portalUser")]
            public string ByPusr { get; set; }
            [JsonProperty("entryDate")]
            public string Erfdt { get; set; }
            [JsonProperty("createdAt")]
            public string Erftm { get; set; }
            [JsonProperty("dataVersion")]
            public string DataVersion { get; set; }
            [JsonProperty("documentURL")]
            public string DocUrl { get; set; }
            [JsonProperty("outletReference")]
            public string OutletRef { get; set; }
            [JsonProperty("enableEdit")]
            public string Enbedit { get; set; }
            [JsonProperty("enableDelete")]
            public string Enbdele { get; set; }
        }

        

        public class AttachSet
        {
            [JsonProperty("attachments")]
            public List<Result> results { get; set; }
        }

        

        public class Deferred3
        {
            public string uri { get; set; }
        }

        

        public class InsPlanSet
        {
            public Deferred3 __deferred { get; set; }
        }


        
        public class NotesSet
        {
            [JsonProperty("notes")]
            public List<object> results { get; set; }
        }

        

        public class Deferred4
        {
            public string uri { get; set; }
        }
        


        public class RetmsgSet
        {
            [JsonProperty("messages")]
            public Deferred4 __deferred { get; set; }
        }

        

        public class D
        {
            //public Metadata __metadata { get; set; }
            [JsonProperty("lockingStatus")]
            public string Lokst { get; set; }
            [JsonProperty("formBundleGUID")]
            public string Fbguid { get; set; }
            // [JsonProperty("")]
            public string FbnumIprr { get; set; }
            [JsonProperty("installementDueDate")]
            public object InsDtOff { get; set; }
            [JsonProperty("penaltyAmount")]
            public string PenlAmt { get; set; }
            // [JsonProperty("")]
            public string SuAmtFg { get; set; }
            [JsonProperty("officerAmount")]
            public string OffAmt { get; set; }
            [JsonProperty("totalAmount")]
            public string TotAmt { get; set; }
            [JsonProperty("userType")]
            public string UserTyp { get; set; }
            [JsonProperty("downPaymentAmount")]
            public string DpAmt { get; set; }
            [JsonProperty("formMode")]
            public string FormMode { get; set; }
            [JsonProperty("paymentDate")]
            public string PaymtDt { get; set; }
            [JsonProperty("formBundleNumber")]
            public string Fbnum { get; set; }
            [JsonProperty("sadadBillNumber")]
            public string Sopbel { get; set; }
            [JsonProperty("transactionType")]
            public string TxnTp { get; set; }
            [JsonProperty("mobileNumber")]
            public string MobNo { get; set; }
            [JsonProperty("days")]
            public string Zdays { get; set; }
            [JsonProperty("formGUID")]
            public string FormGuid { get; set; }
            [JsonProperty("dataVersion")]
            public string DataVersion { get; set; }
            [JsonProperty("operation")]
            public string Operation { get; set; }
            [JsonProperty("userName")]
            public string Euser { get; set; }
            [JsonProperty("stepNumber")]
            public string StepNumber { get; set; }
            [JsonProperty("email")]
            public string Email { get; set; }
            [JsonProperty("officerReason")]
            public string Officer { get; set; }
            [JsonProperty("language")]
            public string Langz { get; set; }
            [JsonProperty("statusCode")]
            public string Status { get; set; }
            [JsonProperty("formProcess")]
            public string Formproc { get; set; }
            [JsonProperty("supervisiorAMT")]
            public string SuAmt { get; set; }
            [JsonProperty("periodKey")]
            public string Periodkey { get; set; }
            [JsonProperty("TIN")]
            public string Tin { get; set; }
            [JsonProperty("returnId")]
            public string ReturnId { get; set; }
            [JsonProperty("taxpayerName")]
            public string TinNm { get; set; }
            [JsonProperty("alternateMobileNumber")]
            public string AltMobNo { get; set; }
            [JsonProperty("installmentRequestedFor")]
            public string InstReqFor { get; set; }
            [JsonProperty("installmentRequestedReason")]
            public string InstReqReason { get; set; }
            [JsonProperty("paymentFrequency")]
            public string PymntFreq { get; set; }
            [JsonProperty("planDuration")]
            public string PlanDur { get; set; }
            [JsonProperty("officerPaymentFrequency")]
            public string OffPymntFreq { get; set; }
            [JsonProperty("officerPlanDuration")]
            public string OffPlanDur { get; set; }
            [JsonProperty("agree")]
            public string DecCb { get; set; }
            [JsonProperty("currency")]
            public string Waers { get; set; }
            [JsonProperty("accountingMethod")]
            public string AccMethod { get; set; }
            [JsonProperty("attachments")]
            public List<Result> AttachSet { get; set; }
            
        }
        
        [JsonProperty("data")]
        public D d { get; set; }

    }

}

