using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using static EGAZT.Models.ZakatInstalationModels.ZAKATRequestPlanModel;

namespace EGAZT.Models.ZakatInstalationModels
{
    [Preserve(AllMembers = true)]
    public class OldZakatInstalmentPlanModel
    {
        public string ActiveOutletDecisionOptions { get; set; }
        public bool ActiveOutletDecisionOptionsIsSelected { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class OldInstalmentAgreementFrequencyModel
    {

        public string FrequencyOptions { get; set; }
        public bool IsSelected { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class OldInstalmentAgreementAttachmentsModel
    {

        public string FieldTitle { get; set; }
        public string FieldSubTitle { get; set; }
        public string AttachmentName { get; set; }

        public bool IsAttachmentAttached { get; set; }
    }
    [Preserve(AllMembers = true)]
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
    [Preserve(AllMembers = true)]
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
    [Preserve(AllMembers = true)]

    public class OldZakatSummaryViewModel
    {

        public string billNumber { get; set; }
        public string amount { get; set; }
        public string saadNumber { get; set; }
        public string taxPeriod { get; set; }
        public bool isSelected { get; set; }
        public string billType { get; set; }

    }
    [Preserve(AllMembers = true)]

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
    [Preserve(AllMembers = true)]

    public partial class ZakatNotesSet
    {
        public Metadata __metadata { get; set; }
        public string Notenoz { get; set; }
        public string Refnamez { get; set; }
        public string XInvoicez { get; set; }
        public string XObsoletez { get; set; }
        public string Rcodez { get; set; }
        public string Erfusrz { get; set; }
        public string Erfdtz { get; set; }
        public string AttByz { get; set; }
        public string ByGpartz { get; set; }
        public string DataVersionz { get; set; }
        public string Noteno { get; set; }
        public long Lineno { get; set; }
        public long ElemNo { get; set; }
        public string Tdformat { get; set; }
        public string Tdline { get; set; }

    }
    [Preserve(AllMembers = true)]

    public partial class OldZakatInstalmentPlanResponse
    {
        public ZakatInstalment d { get; set; }
    }

    [Preserve(AllMembers = true)]
    public partial class Metadata
    {
        public string id { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    [Preserve(AllMembers = true)]

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

    [Preserve(AllMembers = true)]

    public partial class OldAttachSet
    {
        public Attachment[] results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class OldFnDtlSet
    {
        public FnDtlSetObject[] results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class OldInsPlanOffSet
    {
        public object[] result { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class OldInsPlanSet
    {
        public Results4[] results { get; set; }
    }
    [Preserve(AllMembers = true)]
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
    [Preserve(AllMembers = true)]
    public partial class OldInvDtlsSet
    {
        public Results5[] results { get; set; }
    }
    [Preserve(AllMembers = true)]
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
    [Preserve(AllMembers = true)]


    public partial class OldRetmsgSet
    {
        public object[] results { get; set; }
    }

    [Preserve(AllMembers = true)]




    public partial class OldZakatInstalmentPlanRequest
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
        public string ARev1yrTbFg { get; set; }
        public string SavNot { get; set; }
        public string Approvez { get; set; }
        public string ARev2yrTbFg { get; set; }
        public string ALegalEnty1 { get; set; }
        public string ARev3yrTbFg { get; set; }
        public string ANi1yrTbFg { get; set; }
        public string Rejectz { get; set; }
        public string ANi2yrTbFg { get; set; }
        public string CaseGuid { get; set; }
        public string ANi3yrTbFg { get; set; }
        public int AStep { get; set; }
        public string ACb1yrTbFg { get; set; }
        public string ACb2yrTbFg { get; set; }
        public string ALegalEnty2 { get; set; }
        public string ACb3yrTbFg { get; set; }
        public string ASi1yrTbFg { get; set; }
        public string ASi2yrTbFg { get; set; }
        public string RegIdz { get; set; }
        public string ASi3yrTbFg { get; set; }
        public string ATa1yrTbFg { get; set; }
        public string ATa2yrTbFg { get; set; }
        public string ATa3yrTbFg { get; set; }
        public string Fbnum { get; set; }
        public string ATl1yrTbFg { get; set; }
        public string ATin { get; set; }
        public string ATl2yrTbFg { get; set; }
        public string ATaxpayerNm { get; set; }
        public string ATl3yrTbFg { get; set; }
        public string ADeb1yrTbFg { get; set; }
        public string ATelNo { get; set; }
        public string ADeb2yrTbFg { get; set; }
        public string AMobNo { get; set; }
        public string ADeb3yrTbFg { get; set; }
        public string AEmail { get; set; }
        public string ACr1yrTbFg { get; set; }
        public string AInstReqFor { get; set; }
        public string ACr2yrTbFg { get; set; }
        public string AInstReqReason { get; set; }
        public string ABnkStat3mhChk { get; set; }
        public string ACr3yrTbFg { get; set; }
        public string AFinStat3yrChk { get; set; }
        public string ARe1yrTbFg { get; set; }
        public string AOtherDocChk { get; set; }
        public string ARe2yrTbFg { get; set; }
        public string AHoldFinStat { get; set; }
        public string ARe3yrTbFg { get; set; }
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
        public string ARev1yrTb { get; set; }
        public string ARev2yrTb { get; set; }
        public string ARev3yrTb { get; set; }
        public string ANi1yrTb { get; set; }
        public string ANi2yrTb { get; set; }
        public string ANi3yrTb { get; set; }
        public string ACb1yrTb { get; set; }
        public string ACb2yrTb { get; set; }
        public string ACb3yrTb { get; set; }
        public string ASi1yrTb { get; set; }
        public string ASi2yrTb { get; set; }
        public string ASi3yrTb { get; set; }
        public string ATa1yrTb { get; set; }
        public string ATa2yrTb { get; set; }
        public string ATa3yrTb { get; set; }
        public string ATl1yrTb { get; set; }
        public string ATl2yrTb { get; set; }
        public string ATl3yrTb { get; set; }
        public string ADeb1yrTb { get; set; }
        public string ADeb2yrTb { get; set; }
        public string ADeb3yrTb { get; set; }
        public string ACr1yrTb { get; set; }
        public string ACr2yrTb { get; set; }
        public string ACr3yrTb { get; set; }
        public string ARe1yrTb { get; set; }
        public string ARe2yrTb { get; set; }
        public string ARe3yrTb { get; set; }
        public string APr1yrTb { get; set; }
        public string APr2yrTb { get; set; }
        public string APr3yrTb { get; set; }
        public string ACrt1yrTb { get; set; }
        public string ACrt2yrTb { get; set; }
        public string ACrt3yrTb { get; set; }
        public string APc1yrTb { get; set; }
        public string APc2yrTb { get; set; }
        public string APc3yrTb { get; set; }
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
        public OldAttDetSet[] AttDetSet { get; set; }
        public ZakatNotesSet[] Off_notesSet { get; set; }
        public OldZInvoiceSet[] z_invoiceSet { get; set; }
        public OldZProposedinsSet[] z_proposedinsSet { get; set; }
        public OldResults3[] Z_INVOICE_UI5Set { get; set; }

    }


    
    public class OldZAKATRequestPlanModel
    {
        [Preserve(AllMembers = true)]
        public D d { get; set; }
        [Preserve(AllMembers = true)]
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
        [Preserve(AllMembers = true)]
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
        [Preserve(AllMembers = true)]
        public class AuthServSet
        {
            public List<object> results { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class WorklistSet
        {
            public List<object> results { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class RevokeListSet
        {
            public List<object> results { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class EvtNotif12Set
        {
            public List<object> results { get; set; }
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
        [Preserve(AllMembers = true)]
        public class EvtNotif1Set
        {
            public List<Result> results { get; set; }
        }
        [Preserve(AllMembers = true)]
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
    [Preserve(AllMembers = true)]
    public partial class OldZakatInvoiceList
    {
        public ZakatInvoices d { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class OldZakatInvoices
    {
        public List<OldZakatInvoicesResult> results { get; set; }
    }
    [Preserve(AllMembers = true)]
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
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        [Preserve(AllMembers = true)]
        public D d { get; set; }
        [Preserve(AllMembers = true)]
        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class AuthServSet
        {
            public List<object> results { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class OldMetadata2
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
        [Preserve(AllMembers = true)]
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
        [Preserve(AllMembers = true)]
        public class OldWorklistSet
        {
            public List<OldResult> results { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class OldRevokeListSet
        {
            public List<OldRevokeListResult> results { get; set; }
        }
        [Preserve(AllMembers = true)]
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

        [Preserve(AllMembers = true)]
        public class ListSet
        {
            public List<OldResult2> results { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class OldResult2
        {
            public Metadata __metadata { get; set; }
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
        public class OldEvtNotif1Set
        {
            public List<object> results { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class D
        {
            public Metadata __metadata { get; set; }
            public string CallServ { get; set; }
            public long Accnum { get; set; }
            public long Actcnt { get; set; }
            public string Auditor { get; set; }
            public bool AudObjection { get; set; }
            public bool AudRefund { get; set; }
            public bool AudRefundTrn { get; set; }
            public bool AudRequest { get; set; }
            public bool AudReturn { get; set; }
            public string Bpnum { get; set; }
            public string Branch { get; set; }
            public string Caltype { get; set; }
            public long Client { get; set; }
            public long Cnlcnt { get; set; }
            public long Corrnum { get; set; }
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
            public long Indcorrnum { get; set; }
            public string Interest { get; set; }
            public string IntPortal { get; set; }
            public bool IsBankruptcy { get; set; }
            public string Lang { get; set; }
            public string Name { get; set; }
            public bool NotifLogFlag { get; set; }
            public string NregDtFlg { get; set; }
            public long Oblnum { get; set; }
            public string Overdue { get; set; }
            public string Penalty { get; set; }
            public string PortNo { get; set; }
            public string Protocol { get; set; }
            public long Refnum { get; set; }
            public long Regnum { get; set; }
            public long Rencnt { get; set; }
            public long Reqnum { get; set; }
            public long RetItCnt { get; set; }
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
            public long Zfillingoblig { get; set; }
            public string Zregstatus { get; set; }
            public string Zuser { get; set; }
            public ListSet ListSet { get; set; }
            public AuthServSet AuthServSet { get; set; }

        }

    }

    [Preserve(AllMembers = true)]

    public partial class OldZakatRequestDisplayModel
    {
        public OldD d { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class OldD
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
        public string ARev1yrTbFg { get; set; }
        public string SavNot { get; set; }
        public string Approvez { get; set; }
        public string ARev2yrTbFg { get; set; }
        public string ALegalEnty1 { get; set; }
        public string ARev3yrTbFg { get; set; }
        public string ANi1yrTbFg { get; set; }
        public string Rejectz { get; set; }
        public string ANi2yrTbFg { get; set; }
        public string CaseGuid { get; set; }
        public string ANi3yrTbFg { get; set; }
        public int AStep { get; set; }
        public string ACb1yrTbFg { get; set; }
        public string ACb2yrTbFg { get; set; }
        public string ALegalEnty2 { get; set; }
        public string ACb3yrTbFg { get; set; }
        public string ASi1yrTbFg { get; set; }
        public string ASi2yrTbFg { get; set; }
        public string RegIdz { get; set; }
        public string ASi3yrTbFg { get; set; }
        public string ATa1yrTbFg { get; set; }
        public string ATa2yrTbFg { get; set; }
        public string ATa3yrTbFg { get; set; }
        public string Fbnum { get; set; }
        public string ATl1yrTbFg { get; set; }
        public string ATin { get; set; }
        public string ATl2yrTbFg { get; set; }
        public string ATaxpayerNm { get; set; }
        public string ATl3yrTbFg { get; set; }
        public string ADeb1yrTbFg { get; set; }
        public string ATelNo { get; set; }
        public string ADeb2yrTbFg { get; set; }
        public string AMobNo { get; set; }
        public string ADeb3yrTbFg { get; set; }
        public string AEmail { get; set; }
        public string ACr1yrTbFg { get; set; }
        public string AInstReqFor { get; set; }
        public string ACr2yrTbFg { get; set; }
        public string AInstReqReason { get; set; }
        public string ABnkStat3mhChk { get; set; }
        public string ACr3yrTbFg { get; set; }
        public string AFinStat3yrChk { get; set; }
        public string ARe1yrTbFg { get; set; }
        public string AOtherDocChk { get; set; }
        public string ARe2yrTbFg { get; set; }
        public string AHoldFinStat { get; set; }
        public string ARe3yrTbFg { get; set; }
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
        public string ARev1yrTb { get; set; }
        public string ARev2yrTb { get; set; }
        public string ARev3yrTb { get; set; }
        public string ANi1yrTb { get; set; }
        public string ANi2yrTb { get; set; }
        public string ANi3yrTb { get; set; }
        public string ACb1yrTb { get; set; }
        public string ACb2yrTb { get; set; }
        public string ACb3yrTb { get; set; }
        public string ASi1yrTb { get; set; }
        public string ASi2yrTb { get; set; }
        public string ASi3yrTb { get; set; }
        public string ATa1yrTb { get; set; }
        public string ATa2yrTb { get; set; }
        public string ATa3yrTb { get; set; }
        public string ATl1yrTb { get; set; }
        public string ATl2yrTb { get; set; }
        public string ATl3yrTb { get; set; }
        public string ADeb1yrTb { get; set; }
        public string ADeb2yrTb { get; set; }
        public string ADeb3yrTb { get; set; }
        public string ACr1yrTb { get; set; }
        public string ACr2yrTb { get; set; }
        public string ACr3yrTb { get; set; }
        public string ARe1yrTb { get; set; }
        public string ARe2yrTb { get; set; }
        public string ARe3yrTb { get; set; }
        public string APr1yrTb { get; set; }
        public string APr2yrTb { get; set; }
        public string APr3yrTb { get; set; }
        public string ACrt1yrTb { get; set; }
        public string ACrt2yrTb { get; set; }
        public string ACrt3yrTb { get; set; }
        public string APc1yrTb { get; set; }
        public string APc2yrTb { get; set; }
        public string APc3yrTb { get; set; }
        public string AP13yrTb { get; set; }
        public string AP23yrTb { get; set; }
        public string AP33yrTb { get; set; }
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
        public OldAttDetSet AttDetSet { get; set; }
        public OldOffNotesSet Off_notesSet { get; set; }
        public OldZInvoiceSet z_invoiceSet { get; set; }
        public OldZProposedinsSet z_proposedinsSet { get; set; }
        public OldZInvoiceUi5Set Z_INVOICE_UI5Set { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class OldAttDetSet
    {
        public List<Attachment> results { get; set; }
    }
    [Preserve(AllMembers = true)]

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


    [Preserve(AllMembers = true)]


    public partial class OldOffNotesSet
    {
        public List<ZakatNotesSet> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class OldZInvoiceSet
    {
        public List<object> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class OldZInvoiceUi5Set
    {
        public List<OldResults3> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class OldResults3
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
    [Preserve(AllMembers = true)]
    public partial class OldZProposedinsSet
    {
        public List<object> results { get; set; }
    }
   
    public class OldZakatSummaryInputModel
    {
        [Preserve(AllMembers = true)]
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
        [Preserve(AllMembers = true)]

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
        [Preserve(AllMembers = true)]

        public D d { get; set; }



    }
   

    public class OldZakatInstalmentInvListModel
    {
        [Preserve(AllMembers = true)]
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


         [Preserve(AllMembers = true)]
        public class D
        {
            public List<Result> results { get; set; }
        }

    }

    public class OldSummaryDisplayModel
    {
        [Preserve(AllMembers = true)]
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }

        [Preserve(AllMembers = true)]

        public class FnDtlSet
        {
            public List<object> results { get; set; }
        }

        [Preserve(AllMembers = true)]

        public class Deferred
        {
            public string uri { get; set; }
        }
        [Preserve(AllMembers = true)]


        public class InsPlanOffSet
        {
            public Deferred __deferred { get; set; }
        }
        [Preserve(AllMembers = true)]


        public class Deferred2
        {
            public string uri { get; set; }
        }
        [Preserve(AllMembers = true)]


        public class InvDtlsSet
        {
            public Deferred2 __deferred { get; set; }
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


        [Preserve(AllMembers = true)]
        public class AttachSet
        {
            public List<Result> results { get; set; }
        }

        [Preserve(AllMembers = true)]

        public class Deferred3
        {
            public string uri { get; set; }
        }
        [Preserve(AllMembers = true)]


        public class InsPlanSet
        {
            public Deferred3 __deferred { get; set; }
        }

        [Preserve(AllMembers = true)]

        public class NotesSet
        {
            public List<object> results { get; set; }
        }
        [Preserve(AllMembers = true)]


        public class Deferred4
        {
            public string uri { get; set; }
        }
        [Preserve(AllMembers = true)]


        public class RetmsgSet
        {
            public Deferred4 __deferred { get; set; }
        }
        [Preserve(AllMembers = true)]


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
        [Preserve(AllMembers = true)]
        public D d { get; set; }

    }

}

