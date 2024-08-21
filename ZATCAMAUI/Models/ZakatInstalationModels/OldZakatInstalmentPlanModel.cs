
using Newtonsoft.Json;
using static ZATCAMAUI.Models.ZakatInstalationModels.ZAKATRequestPlanModel;

namespace ZATCAMAUI.Models.ZakatInstalationModels
{

    public class OldZakatInstalmentPlanModel
    {
        public string ActiveOutletDecisionOptions { get; set; }
        public bool ActiveOutletDecisionOptionsIsSelected { get; set; }
    }
    
    public class OldInstalmentAgreementFrequencyModel
    {

        public string FrequencyOptions { get; set; }
        public bool IsSelected { get; set; }
    }
    
    public class OldInstalmentAgreementAttachmentsModel
    {

        public string FieldTitle { get; set; }
        public string FieldSubTitle { get; set; }
        public string AttachmentName { get; set; }

        public bool IsAttachmentAttached { get; set; }
    }
    
    public class OldZakatListModel
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



    }
    
    public class OldZakatSelectBillModel
    {

        public string billNumber { get; set; }
        public string amount { get; set; }
        public string saadNumber { get; set; }
        public string taxPeriod { get; set; }
        public bool isSelected { get; set; }
        public string billType { get; set; }
        public string frequency { get; set; }
    }
    

    public class OldZakatSummaryViewModel
    {

        public string billNumber { get; set; }
        public string amount { get; set; }
        public string saadNumber { get; set; }
        public string taxPeriod { get; set; }
        public bool isSelected { get; set; }
        public string billType { get; set; }

    }
    

    public class OldInstalmentAgreementInstalmentPlansModel
    {
        public string DueDate { get; set; }
        public string NumberOfMonths { get; set; }
        public string MonthlyInstalment { get; set; }
        public string TotalAmountPaid { get; set; }
        public string TotalAmountRemaining { get; set; }
    }


    //-----API Object will starts from herer-------



    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    

    public partial class ZakatNotesSet
    {
        public Metadata __metadata { get; set; }
        //[JsonProperty("noteNumber")]
        //public string Notenoz { get; set; }
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
        [JsonProperty("noteNumber")]
        public string Noteno { get; set; }
        [JsonProperty("lineNumber")]
        public long Lineno { get; set; }
        [JsonProperty("elementNumber")]
        public long ElemNo { get; set; }
        [JsonProperty("tagColumn")]
        public string Tdformat { get; set; }
        [JsonProperty("textLine")]
        public string Tdline { get; set; }

    }
    

    public partial class OldZakatInstalmentPlanResponse
    {
        public ZakatInstalment d { get; set; }
    }

    
    public partial class Metadata
    {
        public string id { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    

    public partial class OldZakatInstalment
    {
        public Metadata __metadata { get; set; }
        public string AccMethod { get; set; }
        public string AltMobNo { get; set; }
        public AttachSet AttachSet { get; set; }
        public string DataVersion { get; set; }
        public string DecCb { get; set; }
        public string DpAmt { get; set; }
        public string Email { get; set; }
        public string Euser { get; set; }
        public string Fbguid { get; set; }
        public string Fbnum { get; set; }
        public string FbnumIprr { get; set; }
        public FnDtlSet FnDtlSet { get; set; }
        public string FormGuid { get; set; }
        public string FormMode { get; set; }
        public string Formproc { get; set; }
        public string InsDtOff { get; set; }
        public InsPlanOffSet insPlan_OffSet { get; set; }
        public InsPlanSet insPlanSet { get; set; }
        public string InstReqFor { get; set; }
        public string InstReqReason { get; set; }
        public InvDtlsSet invDtlsSet { get; set; }
        public string Langz { get; set; }
        public string Lokst { get; set; }
        public string MobNo { get; set; }
        public NotesSetResult NotesSet { get; set; }
        public string OffAmt { get; set; }
        public string Officer { get; set; }
        public string OffPlanDur { get; set; }
        public string OffPymntFreq { get; set; }
        public string Operation { get; set; }
        public string PaymtDt { get; set; }
        public string PenlAmt { get; set; }
        public string Periodkey { get; set; }
        public string PlanDur { get; set; }
        public string PymntFreq { get; set; }
        public RetmsgSet retmsgSet { get; set; }
        public string ReturnId { get; set; }
        public string Sopbel { get; set; }
        public string Status { get; set; }
        public string StepNumber { get; set; }
        public string SuAmt { get; set; }
        public string SuAmtFg { get; set; }
        public string Tin { get; set; }
        public string TinNm { get; set; }
        public string TotAmt { get; set; }
        public string TxnTp { get; set; }
        public string UserTyp { get; set; }
        public string Waers { get; set; }
        public string Zdays { get; set; }
    }

    

    public partial class OldAttachSet
    {
        public Attachment[] results { get; set; }
    }
    
    public partial class OldFnDtlSet
    {
        public FnDtlSetObject[] results { get; set; }
    }
    
    public partial class OldInsPlanOffSet
    {
        public object[] result { get; set; }
    }
    
    public partial class OldInsPlanSet
    {
        public Results4[] results { get; set; }
    }
    
    public partial class OldResults4
    {
        public Metadata __metadata { get; set; }
        public string Fbnum { get; set; }
        public string InsStatusTxt { get; set; }
        public string Mandt { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public long LineNo { get; set; }
        public string RankingOrder { get; set; }
        public string InsFbnum { get; set; }
        public string UserTyp { get; set; }
        public string BasicAmt { get; set; }
        public string PenatlyAmt { get; set; }
        public string InterestAmt { get; set; }
        public string InsAmt { get; set; }
        public string DueDt { get; set; }
        public string InsStatus { get; set; }
        public string Waers { get; set; }
        public string ReturnId { get; set; }
    }
    
    public partial class OldInvDtlsSet
    {
        public Results5[] results { get; set; }
    }
    
    public partial class OldResults5
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
    


    public partial class OldRetmsgSet
    {
        public object[] results { get; set; }
    }

    




    public partial class OldZakatInstalmentPlanRequest
    {
        public Metadata __metadata { get; set; }
        [JsonProperty("fractionalPercentage")]
        public string Percentage { get; set; }
        //[JsonProperty("userTIN")]
        public string UserTin { get; set; }
        [JsonProperty("authenticationUser")]
        public string Euser { get; set; }
        [JsonProperty("formBundleGUID")]
        public string Fbguid { get; set; }
        [JsonProperty("auditorTIN")]
        public string Auditorz { get; set; }
        [JsonProperty("language")]
        public string Langz { get; set; }
        [JsonProperty("userTIN")]
        public string Taxpayerz { get; set; }
        [JsonProperty("save")]
        public string Savez { get; set; }
        public string Fbnumz { get; set; }
        [JsonProperty("periodKey")]
        public string PeriodKeyz { get; set; }
        [JsonProperty("submit")]
        public string Submitz { get; set; }
        [JsonProperty("agree")]
        public string AAgree { get; set; }
        [JsonProperty("downPaymentRequestLetter")]
        public string ADwPaymentReqLetter { get; set; }
        [JsonProperty("effectiveCalendarType")]
        public string AEffectiveCalTp { get; set; }
        [JsonProperty("authenticationUser1")]
        public string Euser1 { get; set; }
        [JsonProperty("formBundleTypeDescription")]
        public string FbtypDescr { get; set; }
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [JsonProperty("auditor")]
        public string ZauditorFlag { get; set; }
        [JsonProperty("installmentTaxpayerNumber")]
        public string ANoOfInstTp { get; set; }
        [JsonProperty("revenueYear1")]
        public string ARev1yrTbFg { get; set; }
        [JsonProperty("saveNotes")]
        public string SavNot { get; set; }
        [JsonProperty("approve")]
        public string Approvez { get; set; }
        [JsonProperty("revenueYear2")]
        public string ARev2yrTbFg { get; set; }
        [JsonProperty("legalEntity1")]
        public string ALegalEnty1 { get; set; }
        [JsonProperty("revenueYear3")]
        public string ARev3yrTbFg { get; set; }
        [JsonProperty("netIncomeYear1")]
        public string ANi1yrTbFg { get; set; }
        [JsonProperty("rejectionAction")]
        public string Rejectz { get; set; }
        [JsonProperty("netIncomeYear2")]
        public string ANi2yrTbFg { get; set; }
        [JsonProperty("caseGUID")]
        public string CaseGuid { get; set; }
        [JsonProperty("netIncomeYear3")]
        public string ANi3yrTbFg { get; set; }
        [JsonProperty("step")]
        public int AStep { get; set; }
        [JsonProperty("cashYear1")]
        public string ACb1yrTbFg { get; set; }
        [JsonProperty("cashYear2")]
        public string ACb2yrTbFg { get; set; }
        [JsonProperty("legalEntity2")]
        public string ALegalEnty2 { get; set; }
        [JsonProperty("cashYear3")]
        public string ACb3yrTbFg { get; set; }
        [JsonProperty("shortTermInvestmentYear1")]
        public string ASi1yrTbFg { get; set; }
        [JsonProperty("shortTermInvestmentYear2")]
        public string ASi2yrTbFg { get; set; }
        [JsonProperty("contractNumber")]
        public string RegIdz { get; set; }
        [JsonProperty("shortTermInvestmentYear3")]
        public string ASi3yrTbFg { get; set; }
        [JsonProperty("currentAssetsYear1")]
        public string ATa1yrTbFg { get; set; }
        [JsonProperty("currentAssetsYear2")]
        public string ATa2yrTbFg { get; set; }
        [JsonProperty("currentAssetsYear3")]
        public string ATa3yrTbFg { get; set; }
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [JsonProperty("currentLiabilitiesYear1")]
        public string ATl1yrTbFg { get; set; }
        [JsonProperty("TIN")]
        public string ATin { get; set; }
        [JsonProperty("currentLiabilitiesYear2")]
        public string ATl2yrTbFg { get; set; }
        [JsonProperty("taxpayerName")]
        public string ATaxpayerNm { get; set; }
        [JsonProperty("currentLiabilitiesYear3")]
        public string ATl3yrTbFg { get; set; }
        [JsonProperty("debitedYear1")]
        public string ADeb1yrTbFg { get; set; }
        [JsonProperty("telephoneNumber")]
        public string ATelNo { get; set; }
        [JsonProperty("debitedYear2")]
        public string ADeb2yrTbFg { get; set; }
        [JsonProperty("mobileNumber")]
        public string AMobNo { get; set; }
        [JsonProperty("debitedYear3")]
        public string ADeb3yrTbFg { get; set; }
        [JsonProperty("email")]
        public string AEmail { get; set; }
        [JsonProperty("creditedYear1")]
        public string ACr1yrTbFg { get; set; }
        [JsonProperty("installmentRequestedFor")]
        public string AInstReqFor { get; set; }
        [JsonProperty("creditedYear2")]
        public string ACr2yrTbFg { get; set; }
        [JsonProperty("installmentRequestedReason")]
        public string AInstReqReason { get; set; }
        [JsonProperty("bankStatements")]
        public string ABnkStat3mhChk { get; set; }
        [JsonProperty("creditedYear3")]
        public string ACr3yrTbFg { get; set; }
        [JsonProperty("financialStatementsYear3")]
        public string AFinStat3yrChk { get; set; }
        [JsonProperty("retainEarningYear1")]
        public string ARe1yrTbFg { get; set; }
        [JsonProperty("anotherDocument")]
        public string AOtherDocChk { get; set; }
        [JsonProperty("retainEarningYear2")]
        public string ARe2yrTbFg { get; set; }
        [JsonProperty("holdingFinancialStatement")]
        public string AHoldFinStat { get; set; }
        [JsonProperty("retainEarningYear3")]
        public string ARe3yrTbFg { get; set; }
        [JsonProperty("downPayment")]
        public string ADpAmtFg { get; set; }
        [JsonProperty("item")]
        public string AItTb { get; set; }
        [JsonProperty("year1")]
        public string AOneYrTb { get; set; }
        [JsonProperty("year2")]
        public string ATwoYrTb { get; set; }
        [JsonProperty("year3")]
        public string AThreeYrTb { get; set; }
        [JsonProperty("downPaymentAmount")]
        public string ADpAmt { get; set; }
        [JsonProperty("planDurationNumber")]
        public string APlanDurNo { get; set; }
        [JsonProperty("planDurationPeriod")]
        public string APlanDurPeri { get; set; }
        [JsonProperty("paymentFrequency")]
        public string APaymentFreq { get; set; }
        [JsonProperty("COPlanDurationPeriod")]
        public string ACoPlanDurPeri { get; set; }
        [JsonProperty("COPaymentFrequency")]
        public string ACoPaymentFreq { get; set; }
        [JsonProperty("requiredDownPayment")]
        public string ADpRequ { get; set; }
        [JsonProperty("downPaymentDocumentNumber")]
        public string ADpDocNo { get; set; }
        [JsonProperty("downPaymentPercentage")]
        public string ADpPer { get; set; }
        [JsonProperty("CODownPaymentAmount")]
        public string ACoDpAmt { get; set; }
        [JsonProperty("downPaymentRecordAmount")]
        public string ADpRecAmt { get; set; }
        [JsonProperty("installmentAmount")]
        public string AAppInstAmt { get; set; }
        [JsonProperty("installmentDownPaymentAmount")]
        public string AInstDpAmt { get; set; }
        [JsonProperty("balanceAmount")]
        public string ABalAmt { get; set; }
        [JsonProperty("COBackOfficerReview")]
        public string ACoBoRev { get; set; }
        [JsonProperty("COBackOfficerName")]
        public string ACoBoNm { get; set; }
        [JsonProperty("COBackOfficerApproval")]
        public string ACoBoRd { get; set; }
        [JsonProperty("CMBackOfficerReview")]
        public string ACmBoRev { get; set; }
        [JsonProperty("CMBackOfficerName")]
        public string ACmBoNm { get; set; }
        [JsonProperty("CMBackOfficerApproval")]
        public string ACmBoRd { get; set; }
        [JsonProperty("COHeadOfficerReview")]
        public string ACoHoRev { get; set; }
        [JsonProperty("COHeadOfficerName")]
        public string ACoHoNm { get; set; }
        [JsonProperty("COHeadOfficerApproval")]
        public string ACoHoRd { get; set; }
        [JsonProperty("CMHeadOfficerReview")]
        public string ACmHoRev { get; set; }
        [JsonProperty("CMHeadOfficerName")]
        public string ACmHoNm { get; set; }
        [JsonProperty("CMHeadOfficerApproval")]
        public string ACmHoRd { get; set; }
        [JsonProperty("MOFApprovalLetter")]
        public string AMofApprChk { get; set; }
        [JsonProperty("anotherSupportingDocument")]
        public string AOtherSuppDocChk { get; set; }
        [JsonProperty("rejectionReason")]
        public string ARejReason { get; set; }
        [JsonProperty("COPlanDurationNumber")]
        public string ACoPlanDurNo { get; set; }
        [JsonProperty("revenueAmountYear1")]
        public string ARev1yrTb { get; set; }
        [JsonProperty("revenueAmountYear2")]
        public string ARev2yrTb { get; set; }
        [JsonProperty("revenueAmountYear3")]
        public string ARev3yrTb { get; set; }
        [JsonProperty("netIncomeAmountYear1")]
        public string ANi1yrTb { get; set; }
        [JsonProperty("netIncomeAmountYear2")]
        public string ANi2yrTb { get; set; }
        [JsonProperty("netIncomeAmountYear3")]
        public string ANi3yrTb { get; set; }
        [JsonProperty("cashAmountYear1")]
        public string ACb1yrTb { get; set; }
        [JsonProperty("cashAmountYear2")]
        public string ACb2yrTb { get; set; }
        [JsonProperty("cashAmountYear3")]
        public string ACb3yrTb { get; set; }
        [JsonProperty("shortTermInvestmentAmountYear1")]
        public string ASi1yrTb { get; set; }
        [JsonProperty("shortTermInvestmentAmountYear2")]
        public string ASi2yrTb { get; set; }
        [JsonProperty("shortTermInvestmentAmountYear3")]
        public string ASi3yrTb { get; set; }
        [JsonProperty("currentAssetsAmountYear1")]
        public string ATa1yrTb { get; set; }
        [JsonProperty("currentAssetsAmountYear2")]
        public string ATa2yrTb { get; set; }
        [JsonProperty("currentAssetsAmountYear3")]
        public string ATa3yrTb { get; set; }
        [JsonProperty("currentLiabilitiesAmountYear1")]
        public string ATl1yrTb { get; set; }
        [JsonProperty("currentLiabilitiesAmountYear2")]
        public string ATl2yrTb { get; set; }
        [JsonProperty("currentLiabilitiesAmountYear3")]
        public string ATl3yrTb { get; set; }
        [JsonProperty("debitedAmountYear1")]
        public string ADeb1yrTb { get; set; }
        [JsonProperty("debitedAmountYear2")]
        public string ADeb2yrTb { get; set; }
        [JsonProperty("debitedAmountYear3")]
        public string ADeb3yrTb { get; set; }
        [JsonProperty("creditedAmountYear1")]
        public string ACr1yrTb { get; set; }
        [JsonProperty("creditedAmountYear2")]
        public string ACr2yrTb { get; set; }
        [JsonProperty("creditedAmountYear3")]
        public string ACr3yrTb { get; set; }
        [JsonProperty("retainEarningAmountYear1")]
        public string ARe1yrTb { get; set; }
        [JsonProperty("retainEarningAmountYear2")]
        public string ARe2yrTb { get; set; }
        [JsonProperty("retainEarningAmountYear3")]
        public string ARe3yrTb { get; set; }
        [JsonProperty("profitabilityRatioYear1")]
        public string APr1yrTb { get; set; }
        [JsonProperty("profitabilityRatioYear2")]
        public string APr2yrTb { get; set; }
        [JsonProperty("profitabilityRatioYear3")]
        public string APr3yrTb { get; set; }
        [JsonProperty("currentRatioYear1")]
        public string ACrt1yrTb { get; set; }
        [JsonProperty("currentRatioYear2")]
        public string ACrt2yrTb { get; set; }
        [JsonProperty("currentRatioYear3")]
        public string ACrt3yrTb { get; set; }
        [JsonProperty("proportionOfCashYear1")]
        public string APc1yrTb { get; set; }
        [JsonProperty("proportionOfCashYear2")]
        public string APc2yrTb { get; set; }
        [JsonProperty("proportionOfCashYear3")]
        public string APc3yrTb { get; set; }
        [JsonProperty("percentageAmountApproval")]
        public string APerAmtRd { get; set; }
        [JsonProperty("requiredDownPaymentDrop")]
        public string ADpRequDrp { get; set; }
        [JsonProperty("percentage")]
        public string APer { get; set; }
        [JsonProperty("formStatus")]
        public string AFormStatus { get; set; }
        [JsonProperty("downLetterCheck")]
        public string ADownLetterChk { get; set; }
        [JsonProperty("downYear")]
        public string ADownYear { get; set; }
        [JsonProperty("downMonth")]
        public string ADownMonth { get; set; }
        [JsonProperty("downToYear")]
        public string ADownToYear { get; set; }
        [JsonProperty("downToMonth")]
        public string ADownToMonth { get; set; }
        [JsonProperty("branch")]
        public string ABranch { get; set; }
        [JsonProperty("saudiShare")]
        public string ASaudiShare { get; set; }
        [JsonProperty("nonSaudiShare")]
        public string ANonsaudiShare { get; set; }
        [JsonProperty("mainAccount")]
        public string AMainAct { get; set; }
        [JsonProperty("mainAccountDescription")]
        public string AMainActDesc { get; set; }
        [JsonProperty("poBox")]
        public string APoBox { get; set; }
        [JsonProperty("postalCode")]
        public string APostalCode { get; set; }
        [JsonProperty("faxNumber")]
        public string AFaxNo { get; set; }
        [JsonProperty("building")]
        public string ABuilding { get; set; }
        [JsonProperty("street")]
        public string AStreet { get; set; }
        [JsonProperty("district")]
        public string ADistrict { get; set; }
        [JsonProperty("city")]
        public string ACity { get; set; }
        [JsonProperty("errorMessage")]
        public string ALvError { get; set; }
        [JsonProperty("totalAmount")]
        public string ATotalAmt { get; set; }
        [JsonProperty("statusCode")]
        public string Status { get; set; }
        [JsonProperty("attachments")]
        public OldAttDetSet[] AttDetSet { get; set; }
        [JsonProperty("notes")]
        public ZakatNotesSet[] Off_notesSet { get; set; }
        [JsonProperty("invoices")]
        public OldZInvoiceSet[] z_invoiceSet { get; set; }
        [JsonProperty("proposedInstallments")]
        public OldZProposedinsSet[] z_proposedinsSet { get; set; }
        [JsonProperty("invoicesUI5")]
        public OldResults3[] Z_INVOICE_UI5Set { get; set; }


/* Unmerged change from project 'ZATCAMAUI (net7.0-android33.0)'
Before:
    }



    public class OldZAKATRequestPlanModel
After:
    }



    public class OldZAKATRequestPlanModel
*/
    }



    public class OldZAKATRequestPlanModel
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
            public string CashBankY1 { get; set; }
            public string CashBankY2 { get; set; }
            public string CashBankY3 { get; set; }
            public string CashRatioY1 { get; set; }
            public string CashRatioY2 { get; set; }
            public string CashRatioY3 { get; set; }
            public string DataVersion { get; set; }
            public string DebitorsY1 { get; set; }
            public string DebitorsY2 { get; set; }
            public string DebitorsY3 { get; set; }
            public string Euser { get; set; }
            public string Fbguid { get; set; }
            public string Fbnum { get; set; }
            public string FormGuid { get; set; }
            public string Formproc { get; set; }
            public string Gpart { get; set; }
            public string InventoryY1 { get; set; }
            public string InventoryY2 { get; set; }
            public string InventoryY3 { get; set; }
            public long LineNo { get; set; }
            public string Mandt { get; set; }
            public string NcFlowY1 { get; set; }
            public string NcFlowY2 { get; set; }
            public string NcFlowY3 { get; set; }
            public string NetIncomeY1 { get; set; }
            public string NetIncomeY2 { get; set; }
            public string NetIncomeY3 { get; set; }
            public string ProfitRatioY1 { get; set; }
            public string ProfitRatioY2 { get; set; }
            public string ProfitRatioY3 { get; set; }
            public string RankingOrder { get; set; }
            public string ReturnId { get; set; }
            public string RevenueY1 { get; set; }
            public string RevenueY2 { get; set; }
            public string RevenueY3 { get; set; }
            public string Status { get; set; }
            public string StiY1 { get; set; }
            public string StiY2 { get; set; }
            public string StiY3 { get; set; }
            public string TcAssetsY1 { get; set; }
            public string TcAssetsY2 { get; set; }
            public string TcAssetsY3 { get; set; }
            public string TcLiabltyY1 { get; set; }
            public string TcLiabltyY2 { get; set; }
            public string TcLiabltyY3 { get; set; }
            public string TxnTp { get; set; }
            public string UserTyp { get; set; }
            public string Waers { get; set; }
            public string Year1 { get; set; }
            public string Year2 { get; set; }
            public string Year3 { get; set; }
            public string ZakatY1 { get; set; }
            public string ZakatY2 { get; set; }
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
    
    public partial class OldZakatInvoiceList
    {
        public ZakatInvoices d { get; set; }
    }
    
    public partial class OldZakatInvoices
    {
        public List<OldZakatInvoicesResult> results { get; set; }
    }
    
    public partial class OldZakatInvoicesResult
    {
        public Metadata __metadata { get; set; }
        public string InvCbDeflt { get; set; }
        public string DueYr { get; set; }
        public string TotAmt { get; set; }
        public string Fbnum { get; set; }
        public string Fbguid { get; set; }
        public string Inccbflag { get; set; }
        public string ReturnId { get; set; }
        public string Tin { get; set; }
        public string FormGuid { get; set; }
        public string Taxtype { get; set; }
        public string DataVersion { get; set; }
        public string Euser { get; set; }
        public string Langz { get; set; }
        public long LineNo { get; set; }
        public string RankingOrder { get; set; }
        public string InvCb { get; set; }
        public string InvNo { get; set; }
        public string DueDt { get; set; }
        public string InvAmt { get; set; }
        public string ClearedAmt { get; set; }
        public string DueAmt { get; set; }
        public string InstReqFor { get; set; }
        public string Abtyp { get; set; }
        public string Waers { get; set; }
    }



    public class OldZakatInstalmentPlanRequestListModel
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
            public List<object> results { get; set; }
        }
        
        public class OldMetadata2
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
        
        public class OldResult
        {
            public Metadata2 __metadata { get; set; }
            public string DueAmt { get; set; }
            public string Fbtyp { get; set; }
            public string PymntFreq { get; set; }
            public string SubmitDt { get; set; }
            public string Fbsta { get; set; }
            public string UserTyp { get; set; }
            public string Fbust { get; set; }
            public string Tin { get; set; }
            public string Fbnum { get; set; }
            public string TotAmt { get; set; }
            public string DpAmt { get; set; }
            public string PlanDur { get; set; }
            public string Waers { get; set; }
            public string Status { get; set; }
            public string IptypeFg { get; set; }

        }
        
        public class OldWorklistSet
        {
            public List<OldResult> results { get; set; }
        }
        
        public class OldRevokeListSet
        {
            public List<OldRevokeListResult> results { get; set; }
        }
        
        public partial class OldRevokeListResult
        {
            public Metadata __metadata { get; set; }
            public string UserTyp { get; set; }
            public string Fbtyp { get; set; }
            public string Tin { get; set; }
            public string Fbnum { get; set; }
            public string SubmitDt { get; set; }
            public string TotAmt { get; set; }
            public string DpAmt { get; set; }
            public string DueAmt { get; set; }
            public string PymntFreq { get; set; }
            public string PlanDur { get; set; }
            public string Waers { get; set; }
            public string Fbsta { get; set; }
            public string Fbust { get; set; }
            public string Status { get; set; }
            public bool Revoke { get; set; }
        }

        
        public class ListSet
        {
            public List<OldResult2> results { get; set; }
        }
        
        public class OldResult2
        {
            public Metadata __metadata { get; set; }
            [JsonProperty("formBundleNumber")]
            public string Fbnum { get; set; }
            [JsonProperty("objectionStatus")]
            public string Objstatus { get; set; }
            [JsonProperty("formBundleStatus")]
            public string Fbsta { get; set; }
            [JsonProperty("statusDescription")]
            public string StatText { get; set; }
            [JsonProperty("formBundleType")]
            public string Fbtyp { get; set; }
            [JsonProperty("formBundleTypeDescription")]
            public string FbtText { get; set; }
            [JsonProperty("createdOn")]
            public string Erfdate { get; set; }
            [JsonProperty("createdAt")]
            public string Erftime { get; set; }
            [JsonProperty("periodkey")]
            public string Persl { get; set; }
            [JsonProperty("taxPeriod")]
            public string TaxPeriod { get; set; }
            [JsonProperty("dueDate")]
            public object DueDt { get; set; }

            public string Due { get; set; }
            [JsonProperty("billPeriodStartDate")]
            public object Abrzu { get; set; }
            [JsonProperty("billPeriodEndDate")]
            public object Abrzo { get; set; }
            [JsonProperty("inboundCorrespondenceType")]
            public string Incotyp { get; set; }
            [JsonProperty("inboundCorrespondenceDescription")]
            public string Incotext { get; set; }
            [JsonProperty("flag")]
            public string Flag { get; set; }

            public string CalendrTyp { get; set; }
            [JsonProperty("attachedByPerson")]
            public string PrcBy { get; set; }
            [JsonProperty("group")]
            public string Grp { get; set; }
            [JsonProperty("creditDate")]
            public string CrdtText { get; set; }
            [JsonProperty("serialNumber")]
            public string Euser { get; set; }
            [JsonProperty("formBundleGUID")]
            public string Fbguid { get; set; }
        }
        
        public class OldEvtNotif1Set
        {
            public List<object> results { get; set; }
        }
        
        public class D
        {
            public Metadata __metadata { get; set; }
            [JsonProperty("callService")]
            public string CallServ { get; set; }
            [JsonProperty("accountNumber")]
            public long Accnum { get; set; }
            [JsonProperty("activeCount")]
            public long Actcnt { get; set; }
            [JsonProperty("auditor")]
            public string Auditor { get; set; }
            [JsonProperty("isAuditorObjection")]
            public bool AudObjection { get; set; }
            [JsonProperty("isAuditorRefund")]
            public bool AudRefund { get; set; }
            [JsonProperty("isAuditorRefundTransaction")]
            public bool AudRefundTrn { get; set; }
            [JsonProperty("isAuditorRequest")]
            public bool AudRequest { get; set; }
            [JsonProperty("isAuditorReturn")]
            public bool AudReturn { get; set; }
            [JsonProperty("TIN")]
            public string Bpnum { get; set; }
            [JsonProperty("branch")]
            public string Branch { get; set; }
            // [JsonProperty("data")]
            public string Caltype { get; set; }
            [JsonProperty("client")]
            public long Client { get; set; }
            [JsonProperty("cancellationCount")]
            public long Cnlcnt { get; set; }
            [JsonProperty("correspondenceNumber")]
            public long Corrnum { get; set; }
            [JsonProperty("department")]
            public string Dept { get; set; }
            [JsonProperty("isDisplayShare")]
            public bool DisSharetile { get; set; }
            [JsonProperty("isEnableInstallmentPlan")]
            public bool EnableInstPlan { get; set; }
            [JsonProperty("isEnableTile")]
            public bool EnableTile { get; set; }
            [JsonProperty("exciseTaxTransaction")]
            public string Ettr { get; set; }
            [JsonProperty("serialNumber")]
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
            [JsonProperty("exciseTaxAppeal")]
            public string ExeAppFlg { get; set; }
            [JsonProperty("exciseTaxDetail")]
            public string ExeDtFlg { get; set; }
            [JsonProperty("formBundleGUID")]
            public string Fbguid { get; set; }
            [JsonProperty("hostName")]
            public string HostName { get; set; }
            [JsonProperty("inboundCorrespondenceNumber")]
            public long Indcorrnum { get; set; }
            [JsonProperty("interestAmount")]
            public string Interest { get; set; }
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
            [JsonProperty("newRegistrationDetail")]
            public string NregDtFlg { get; set; }
            [JsonProperty("obligationNumber")]
            public long Oblnum { get; set; }
            [JsonProperty("overdueAmount")]
            public string Overdue { get; set; }
            [JsonProperty("penaltyAmount")]
            public string Penalty { get; set; }
            [JsonProperty("portNumber")]
            public string PortNo { get; set; }
            [JsonProperty("protocol")]
            public string Protocol { get; set; }
            [JsonProperty("referenceNumber")]
            public long Refnum { get; set; }
            [JsonProperty("registrationNumber")]
            public long Regnum { get; set; }
            [JsonProperty("renewalCount")]
            public long Rencnt { get; set; }
            [JsonProperty("requestNumber")]
            public long Reqnum { get; set; }
            [JsonProperty("returnCount")]
            public long RetItCnt { get; set; }
            [JsonProperty("return")]
            public string RetItFlg { get; set; }
            [JsonProperty("systemName")]
            public string SystemName { get; set; }
            [JsonProperty("taxType")]
            public string Taxtype { get; set; }
            [JsonProperty("isTileOutlet")]
            public bool TileOutlet { get; set; }
            [JsonProperty("isTilePermit")]
            public bool TilePermit { get; set; }
            [JsonProperty("isTileTIN")]
            public bool TileTin { get; set; }
            [JsonProperty("title")]
            public string Title { get; set; }
            [JsonProperty("type")]
            public string Type { get; set; }
            [JsonProperty("isUpgradeOutlet")]
            public bool UpdregOutflag { get; set; }
            [JsonProperty("VATConfiguration")]
            public string VatConfFlg { get; set; }
            [JsonProperty("VATDetail")]
            public string VatDtFlg { get; set; }
            [JsonProperty("VATEligiblePerson")]
            public string VtepFg { get; set; }
            [JsonProperty("VATSignup")]
            public string VtiaSignFg { get; set; }
            [JsonProperty("warehouseDetail")]
            public string WarDtFlg { get; set; }
            [JsonProperty("fillingObligation")]
            public long Zfillingoblig { get; set; }
            [JsonProperty("registrationStatus")]
            public string Zregstatus { get; set; }
            //[JsonProperty("data")]
            public string Zuser { get; set; }
            [JsonProperty("lists")]
            public List<OldResult2> ListSet { get; set; }
            [JsonProperty("authorizationServer")]
            public List<object> AuthServSet { get; set; }

        }

    }

    

    public partial class OldZakatRequestDisplayModel
    {
        [JsonProperty("data")]
        public OldD d { get; set; }

        [JsonProperty("result")]
        public OldD result { set { d = value; } }
    }
    
    public partial class OldD
    {
        public Metadata __metadata { get; set; }
        [JsonProperty("fractionalPercentage")]
        public string Percentage { get; set; }
        [JsonProperty("userTIN")]
        public string UserTin { get; set; }
        [JsonProperty("authenticationUser")]
        public string Euser { get; set; }
        [JsonProperty("formBundleGUID")]
        public string Fbguid { get; set; }
        [JsonProperty("auditor")]
        public string Auditorz { get; set; }
        [JsonProperty("language")]
        public string Langz { get; set; }
        [JsonProperty("TIN")]
        public string Taxpayerz { get; set; }
        [JsonProperty("save")]
        public string Savez { get; set; }
        //[JsonProperty("formBundleNumber")]
        //public string Fbnumz { get; set; }
        [JsonProperty("periodKey")]
        public string PeriodKeyz { get; set; }
        [JsonProperty("submit")]
        public string Submitz { get; set; }
        [JsonProperty("agree")]
        public string AAgree { get; set; }
        [JsonProperty("downPaymentRequestLetter")]
        public string ADwPaymentReqLetter { get; set; }
        [JsonProperty("effectiveCalendarType")]
        public string AEffectiveCalTp { get; set; }
        [JsonProperty("authenticationUser1")]
        public string Euser1 { get; set; }
        [JsonProperty("formBundleTypeDescription")]
        public string FbtypDescr { get; set; }
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        //[JsonProperty("auditor")]
        //public string ZauditorFlag { get; set; }
        [JsonProperty("installmentTaxpayerNumber")]
        public string ANoOfInstTp { get; set; }
        [JsonProperty("revenueYear1")]
        public string ARev1yrTbFg { get; set; }
        [JsonProperty("saveNotes")]
        public string SavNot { get; set; }
        [JsonProperty("approve")]
        public string Approvez { get; set; }
        [JsonProperty("revenueYear2")]
        public string ARev2yrTbFg { get; set; }
        [JsonProperty("legalEntity1")]
        public string ALegalEnty1 { get; set; }
        [JsonProperty("revenueYear3")]
        public string ARev3yrTbFg { get; set; }
        [JsonProperty("netIncomeYear1")]
        public string ANi1yrTbFg { get; set; }
        [JsonProperty("rejectionAction")]
        public string Rejectz { get; set; }
        [JsonProperty("netIncomeYear2")]
        public string ANi2yrTbFg { get; set; }
        [JsonProperty("caseGUID")]
        public string CaseGuid { get; set; }
        [JsonProperty("netIncomeYear3")]
        public string ANi3yrTbFg { get; set; }
        [JsonProperty("step")]
        public int AStep { get; set; }
        [JsonProperty("cashYear1")]
        public string ACb1yrTbFg { get; set; }
        [JsonProperty("cashYear2")]
        public string ACb2yrTbFg { get; set; }
        [JsonProperty("legalEntity2")]
        public string ALegalEnty2 { get; set; }
        [JsonProperty("cashYear3")]
        public string ACb3yrTbFg { get; set; }
        [JsonProperty("shortTermInvestmentYear1")]
        public string ASi1yrTbFg { get; set; }
        [JsonProperty("shortTermInvestmentYear2")]
        public string ASi2yrTbFg { get; set; }
        [JsonProperty("contractNumber")]
        public string RegIdz { get; set; }
        [JsonProperty("shortTermInvestmentYear3")]
        public string ASi3yrTbFg { get; set; }
        [JsonProperty("currentAssetsYear1")]
        public string ATa1yrTbFg { get; set; }
        [JsonProperty("currentAssetsYear2")]
        public string ATa2yrTbFg { get; set; }
        [JsonProperty("currentAssetsYear3")]
        public string ATa3yrTbFg { get; set; }
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [JsonProperty("currentLiabilitiesYear1")]
        public string ATl1yrTbFg { get; set; }
        //[JsonProperty("")]
        //public string ATin { get; set; }
        [JsonProperty("currentLiabilitiesYear2")]
        public string ATl2yrTbFg { get; set; }
        [JsonProperty("taxpayerName")]
        public string ATaxpayerNm { get; set; }
        [JsonProperty("currentLiabilitiesYear3")]
        public string ATl3yrTbFg { get; set; }
        [JsonProperty("debitedYear1")]
        public string ADeb1yrTbFg { get; set; }
        [JsonProperty("telephoneNumber")]
        public string ATelNo { get; set; }
        [JsonProperty("debitedYear2")]
        public string ADeb2yrTbFg { get; set; }
        [JsonProperty("mobileNumber")]
        public string AMobNo { get; set; }
        [JsonProperty("debitedYear3")]
        public string ADeb3yrTbFg { get; set; }
        [JsonProperty("email")]
        public string AEmail { get; set; }
        [JsonProperty("creditedYear1")]
        public string ACr1yrTbFg { get; set; }
        [JsonProperty("installmentRequestedFor")]
        public string AInstReqFor { get; set; }
        [JsonProperty("creditedYear2")]
        public string ACr2yrTbFg { get; set; }
        [JsonProperty("installmentRequestedReason")]
        public string AInstReqReason { get; set; }
        [JsonProperty("bankStatements")]
        public string ABnkStat3mhChk { get; set; }
        [JsonProperty("creditedYear3")]
        public string ACr3yrTbFg { get; set; }
        [JsonProperty("financialStatementsYear3")]
        public string AFinStat3yrChk { get; set; }
        [JsonProperty("retainEarningYear1")]
        public string ARe1yrTbFg { get; set; }
        [JsonProperty("anotherDocument")]
        public string AOtherDocChk { get; set; }
        [JsonProperty("retainEarningYear2")]
        public string ARe2yrTbFg { get; set; }
        [JsonProperty("holdingFinancialStatement")]
        public string AHoldFinStat { get; set; }
        [JsonProperty("retainEarningYear3")]
        public string ARe3yrTbFg { get; set; }
        [JsonProperty("downPayment")]
        public string ADpAmtFg { get; set; }
        [JsonProperty("item")]
        public string AItTb { get; set; }
        [JsonProperty("year1")]
        public string AOneYrTb { get; set; }
        [JsonProperty("year2")]
        public string ATwoYrTb { get; set; }
        [JsonProperty("year3")]
        public string AThreeYrTb { get; set; }
        [JsonProperty("downPaymentAmount")]
        public string ADpAmt { get; set; }
        [JsonProperty("planDurationNumber")]
        public string APlanDurNo { get; set; }
        [JsonProperty("planDurationPeriod")]
        public string APlanDurPeri { get; set; }
        [JsonProperty("paymentFrequency")]
        public string APaymentFreq { get; set; }
        [JsonProperty("COPlanDurationPeriod")]
        public string ACoPlanDurPeri { get; set; }
        [JsonProperty("COPaymentFrequency")]
        public string ACoPaymentFreq { get; set; }
        [JsonProperty("requiredDownPayment")]
        public string ADpRequ { get; set; }
        [JsonProperty("downPaymentDocumentNumber")]
        public string ADpDocNo { get; set; }
        [JsonProperty("downPaymentPercentage")]
        public string ADpPer { get; set; }
        [JsonProperty("CODownPaymentAmount")]
        public string ACoDpAmt { get; set; }
        [JsonProperty("downPaymentRecordAmount")]
        public string ADpRecAmt { get; set; }
        [JsonProperty("installmentAmount")]
        public string AAppInstAmt { get; set; }
        [JsonProperty("installmentDownPaymentAmount")]
        public string AInstDpAmt { get; set; }
        [JsonProperty("balanceAmount")]
        public string ABalAmt { get; set; }
        [JsonProperty("COBackOfficerReview")]
        public string ACoBoRev { get; set; }
        [JsonProperty("COBackOfficerName")]
        public string ACoBoNm { get; set; }
        [JsonProperty("COBackOfficerApproval")]
        public string ACoBoRd { get; set; }
        [JsonProperty("CMBackOfficerReview")]
        public string ACmBoRev { get; set; }
        [JsonProperty("CMBackOfficerName")]
        public string ACmBoNm { get; set; }
        [JsonProperty("CMBackOfficerApproval")]
        public string ACmBoRd { get; set; }
        [JsonProperty("COHeadOfficerReview")]
        public string ACoHoRev { get; set; }
        [JsonProperty("COHeadOfficerName")]
        public string ACoHoNm { get; set; }
        [JsonProperty("COHeadOfficerApproval")]
        public string ACoHoRd { get; set; }
        [JsonProperty("CMHeadOfficerReview")]
        public string ACmHoRev { get; set; }
        [JsonProperty("CMHeadOfficerName")]
        public string ACmHoNm { get; set; }
        [JsonProperty("CMHeadOfficerApproval")]
        public string ACmHoRd { get; set; }
        [JsonProperty("MOFApprovalLetter")]
        public string AMofApprChk { get; set; }
        [JsonProperty("anotherSupportingDocument")]
        public string AOtherSuppDocChk { get; set; }
        [JsonProperty("rejectionReason")]
        public string ARejReason { get; set; }
        [JsonProperty("COPlanDurationNumber")]
        public string ACoPlanDurNo { get; set; }
        [JsonProperty("revenueAmountYear1")]
        public string ARev1yrTb { get; set; }
        [JsonProperty("revenueAmountYear2")]
        public string ARev2yrTb { get; set; }
        [JsonProperty("revenueAmountYear3")]
        public string ARev3yrTb { get; set; }
        [JsonProperty("netIncomeAmountYear1")]
        public string ANi1yrTb { get; set; }
        [JsonProperty("netIncomeAmountYear2")]
        public string ANi2yrTb { get; set; }
        [JsonProperty("netIncomeAmountYear3")]
        public string ANi3yrTb { get; set; }
        [JsonProperty("cashAmountYear1")]
        public string ACb1yrTb { get; set; }
        [JsonProperty("cashAmountYear2")]
        public string ACb2yrTb { get; set; }
        [JsonProperty("cashAmountYear3")]
        public string ACb3yrTb { get; set; }
        [JsonProperty("shortTermInvestmentAmountYear1")]
        public string ASi1yrTb { get; set; }
        [JsonProperty("shortTermInvestmentAmountYear2")]
        public string ASi2yrTb { get; set; }
        [JsonProperty("shortTermInvestmentAmountYear3")]
        public string ASi3yrTb { get; set; }
        [JsonProperty("currentAssetsAmountYear1")]
        public string ATa1yrTb { get; set; }
        [JsonProperty("currentAssetsAmountYear2")]
        public string ATa2yrTb { get; set; }
        [JsonProperty("currentAssetsAmountYear3")]
        public string ATa3yrTb { get; set; }
        [JsonProperty("currentLiabilitiesAmountYear1")]
        public string ATl1yrTb { get; set; }
        [JsonProperty("currentLiabilitiesAmountYear2")]
        public string ATl2yrTb { get; set; }
        [JsonProperty("currentLiabilitiesAmountYear3")]
        public string ATl3yrTb { get; set; }
        [JsonProperty("debitedAmountYear1")]
        public string ADeb1yrTb { get; set; }
        [JsonProperty("debitedAmountYear2")]
        public string ADeb2yrTb { get; set; }
        [JsonProperty("debitedAmountYear3")]
        public string ADeb3yrTb { get; set; }
        [JsonProperty("creditedAmountYear1")]
        public string ACr1yrTb { get; set; }
        [JsonProperty("creditedAmountYear2")]
        public string ACr2yrTb { get; set; }
        [JsonProperty("creditedAmountYear3")]
        public string ACr3yrTb { get; set; }
        [JsonProperty("retainEarningAmountYear1")]
        public string ARe1yrTb { get; set; }
        [JsonProperty("retainEarningAmountYear2")]
        public string ARe2yrTb { get; set; }
        [JsonProperty("retainEarningAmountYear3")]
        public string ARe3yrTb { get; set; }
        [JsonProperty("profitabilityRatioYear1")]
        public string APr1yrTb { get; set; }
        [JsonProperty("profitabilityRatioYear2")]
        public string APr2yrTb { get; set; }
        [JsonProperty("profitabilityRatioYear3")]
        public string APr3yrTb { get; set; }
        [JsonProperty("currentRatioYear1")]
        public string ACrt1yrTb { get; set; }
        [JsonProperty("currentRatioYear2")]
        public string ACrt2yrTb { get; set; }
        [JsonProperty("currentRatioYear3")]
        public string ACrt3yrTb { get; set; }
        [JsonProperty("proportionOfCashYear1")]
        public string APc1yrTb { get; set; }
        [JsonProperty("proportionOfCashYear2")]
        public string APc2yrTb { get; set; }
        [JsonProperty("proportionOfCashYear3")]
        public string APc3yrTb { get; set; }
        //[JsonProperty("")]
        //public string AP13yrTb { get; set; }
        //[JsonProperty("")]
        //public string AP23yrTb { get; set; }
        //[JsonProperty("")]
        //public string AP33yrTb { get; set; }
        [JsonProperty("percentageAmountApproval")]
        public string APerAmtRd { get; set; }
        [JsonProperty("requiredDownPaymentDrop")]
        public string ADpRequDrp { get; set; }
        [JsonProperty("percentage")]
        public string APer { get; set; }
        [JsonProperty("formStatus")]
        public string AFormStatus { get; set; }
        [JsonProperty("downLetterCheck")]
        public string ADownLetterChk { get; set; }
        [JsonProperty("downYear")]
        public string ADownYear { get; set; }
        [JsonProperty("downMonth")]
        public string ADownMonth { get; set; }
        [JsonProperty("downToYear")]
        public string ADownToYear { get; set; }
        [JsonProperty("downToMonth")]
        public string ADownToMonth { get; set; }
        [JsonProperty("branch")]
        public string ABranch { get; set; }
        [JsonProperty("saudiShare")]
        public string ASaudiShare { get; set; }
        [JsonProperty("nonSaudiShare")]
        public string ANonsaudiShare { get; set; }
        [JsonProperty("mainAccount")]
        public string AMainAct { get; set; }
        [JsonProperty("mainAccountDescription")]
        public string AMainActDesc { get; set; }
        [JsonProperty("poBox")]
        public string APoBox { get; set; }
        [JsonProperty("postalCode")]
        public string APostalCode { get; set; }
        [JsonProperty("faxNumber")]
        public string AFaxNo { get; set; }
        [JsonProperty("building")]
        public string ABuilding { get; set; }
        [JsonProperty("street")]
        public string AStreet { get; set; }
        [JsonProperty("district")]
        public string ADistrict { get; set; }
        [JsonProperty("city")]
        public string ACity { get; set; }
        [JsonProperty("errorMessage")]
        public string ALvError { get; set; }
        [JsonProperty("totalAmount")]
        public string ATotalAmt { get; set; }
        [JsonProperty("statusCode")]
        public string Status { get; set; }
        [JsonProperty("attachments")]
        public List<Attachment> AttDetSet { get; set; }
        [JsonProperty("notes")]
        public List<ZakatNotesSet> Off_notesSet { get; set; }
        [JsonProperty("invoices")]
        public List<object> z_invoiceSet { get; set; }
        [JsonProperty("proposedInstallments")]
        public List<object> z_proposedinsSet { get; set; }
        [JsonProperty("invoicesUI5")]
        public List<OldResults3> Z_INVOICE_UI5Set { get; set; }
    }
    
    public partial class OldAttDetSet
    {
        public List<Attachment> results { get; set; }
    }
    

    public class AttResult
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
    }


    


    public partial class OldOffNotesSet
    {
        public List<ZakatNotesSet> results { get; set; }
    }
    
    public partial class OldZInvoiceSet
    {
        public List<object> results { get; set; }
    }
    
    public partial class OldZInvoiceUi5Set
    {
        public List<OldResults3> results { get; set; }
    }
    
    public partial class OldResults3
    {
        public Metadata __metadata { get; set; }
        [JsonProperty("amount")]
        public string AAmtTb { get; set; }
        [JsonProperty("clearedAmount")]
        public string AClearedAmtTb { get; set; }
        [JsonProperty("dueAmount")]
        public string ADueAmtTb { get; set; }
        [JsonProperty("dueDate")]
        public string ADueDtTb { get; set; }
        [JsonProperty("invoiceAmount")]
        public string AIvAmtTb { get; set; }
        [JsonProperty("invoiceNumber")]
        public string AIvNoTb { get; set; }
        [JsonProperty("invoiceSerialNumber")]
        public string AIvSrNoTb { get; set; }
        [JsonProperty("invoice")]
        public string AIvTb { get; set; }
        [JsonProperty("taxType")]
        public string AIvAbtyp { get; set; }
    }
    
    public partial class OldZProposedinsSet
    {
        public List<object> results { get; set; }
    }

    public class OldZakatSummaryInputModel
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
            public string Monthz { get; set; }
            public string Gpart { get; set; }
            public string Lang { get; set; }
            public string Begda { get; set; }
            public string Endda { get; set; }
            public string ObligFlag { get; set; }
            public string TaxtpFg { get; set; }
            public string UserTin { get; set; }
            public string Vtref { get; set; }
            public string Incotyp { get; set; }
            public string Incotext { get; set; }
            public string Fbtyp { get; set; }
            public string FbtText { get; set; }
            public string Due { get; set; }
            public string Persl { get; set; }
            public string TaxPeriod { get; set; }
            public string DueDt { get; set; }
            public string Fbnum { get; set; }
            public string Status { get; set; }
            public string CalendrTyp { get; set; }
            public string Abrzu { get; set; }
            public string Abrzo { get; set; }
            public string Stat { get; set; }
            public string SadadDoc1 { get; set; }
            public string SadadDoc2 { get; set; }
            public string InfoMsg { get; set; }
            public string Statfg { get; set; }
            public string Period { get; set; }
            public string Statflag { get; set; }
            public string DueDtC { get; set; }
            public string Flag { get; set; }
            public string Comb { get; set; }
            public string Auditor { get; set; }
            public bool AudFlag { get; set; }
            public string Branch { get; set; }
            public string Aedat { get; set; }
            public bool Open { get; set; }
            public string Msg { get; set; }
            public string Dispflag { get; set; }
            public string Euser { get; set; }
            public string Fbguid { get; set; }
            public string Euser1 { get; set; }
            public string Euser2 { get; set; }
            public string Euser3 { get; set; }
            public string Euser4 { get; set; }
            public string Euser5 { get; set; }
            public string Retguid { get; set; }
            public string Liveflg { get; set; }
            public string UserErrFg { get; set; }
            public string TaxOffUid { get; set; }
        }
        

        public D d { get; set; }



    }


    public class OldZakatInstalmentInvListModel
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
            public string InvCbDeflt { get; set; }
            public string DueYr { get; set; }
            public string TotAmt { get; set; }
            public string Fbnum { get; set; }
            public string Fbguid { get; set; }
            public string Inccbflag { get; set; }
            public string ReturnId { get; set; }
            public string Tin { get; set; }
            public string FormGuid { get; set; }
            public string Taxtype { get; set; }
            public string DataVersion { get; set; }
            public string Euser { get; set; }
            public string Langz { get; set; }
            public int LineNo { get; set; }
            public string RankingOrder { get; set; }
            public string InvCb { get; set; }
            public string InvNo { get; set; }
            public string DueDt { get; set; }
            public string InvAmt { get; set; }
            public string ClearedAmt { get; set; }
            public string DueAmt { get; set; }
            public string InstReqFor { get; set; }
            public string Abtyp { get; set; }
            public string Waers { get; set; }
        }

        
        public class D
        {
            public List<Result> results { get; set; }
        }

    }

    public class OldSummaryDisplayModel
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
            public List<object> results { get; set; }
        }

        

        public class Deferred
        {
            public string uri { get; set; }
        }
        


        public class InsPlanOffSet
        {
            public Deferred __deferred { get; set; }
        }
        


        public class Deferred2
        {
            public string uri { get; set; }
        }
        


        public class InvDtlsSet
        {
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
        }


        
        public class AttachSet
        {
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
            public List<object> results { get; set; }
        }
        


        public class Deferred4
        {
            public string uri { get; set; }
        }
        


        public class RetmsgSet
        {
            public Deferred4 __deferred { get; set; }
        }
        


        public class D
        {
            public Metadata __metadata { get; set; }
            public string Lokst { get; set; }
            public string Fbguid { get; set; }
            public string FbnumIprr { get; set; }
            public object InsDtOff { get; set; }
            public string PenlAmt { get; set; }
            public string SuAmtFg { get; set; }
            public string OffAmt { get; set; }
            public string TotAmt { get; set; }
            public string UserTyp { get; set; }
            public string DpAmt { get; set; }
            public string FormMode { get; set; }
            public object PaymtDt { get; set; }
            public string Fbnum { get; set; }
            public string Sopbel { get; set; }
            public string TxnTp { get; set; }
            public string MobNo { get; set; }
            public string Zdays { get; set; }
            public string FormGuid { get; set; }
            public string DataVersion { get; set; }
            public string Operation { get; set; }
            public string Euser { get; set; }
            public string StepNumber { get; set; }
            public string Email { get; set; }
            public string Officer { get; set; }
            public string Langz { get; set; }
            public string Status { get; set; }
            public string Formproc { get; set; }
            public string SuAmt { get; set; }
            public string Periodkey { get; set; }
            public string Tin { get; set; }
            public string ReturnId { get; set; }
            public string TinNm { get; set; }
            public string AltMobNo { get; set; }
            public string InstReqFor { get; set; }
            public string InstReqReason { get; set; }
            public string PymntFreq { get; set; }
            public string PlanDur { get; set; }
            public string OffPymntFreq { get; set; }
            public string OffPlanDur { get; set; }
            public string DecCb { get; set; }
            public string Waers { get; set; }
            public string AccMethod { get; set; }
            public FnDtlSet FnDtlSet { get; set; }
            public InsPlanOffSet insPlan_OffSet { get; set; }
            public InvDtlsSet invDtlsSet { get; set; }
            public AttachSet AttachSet { get; set; }
            public InsPlanSet insPlanSet { get; set; }
            public NotesSet NotesSet { get; set; }
            public RetmsgSet retmsgSet { get; set; }
        }
        
        public D d { get; set; }

    }

}

