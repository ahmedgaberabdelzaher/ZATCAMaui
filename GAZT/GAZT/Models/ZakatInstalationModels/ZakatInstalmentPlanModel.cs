using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using static EGAZT.Models.ZakatInstalationModels.ZAKATRequestPlanModel;

namespace EGAZT.Models.ZakatInstalationModels
{
    [Preserve(AllMembers = true)]
    public class ZakatInstalmentPlanModel
    {
        public string CardLabel { get => ActiveOutletDecisionOptions; }
        public string ActiveOutletDecisionOptions { get; set; }
        public bool ActiveOutletDecisionOptionsIsSelected { get; set; }
    }
    [Preserve(AllMembers = true)]

    public class InstalmentAgreementFrequencyModel
    {
        public string CardLabel { get => FrequencyOptions; }
        public string FrequencyOptions { get; set; }
        public bool IsSelected { get; set; }
    }
     [Preserve(AllMembers = true)]
    public class InstalmentAgreementAttachmentsModel
    {
       
        public string FieldTitle { get; set; }
        public string FieldSubTitle { get; set; }
        public string AttachmentName { get; set; }

        public bool IsAttachmentAttached { get; set; }
    }
    [Preserve(AllMembers = true)]
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



    }
    [Preserve(AllMembers = true)]
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
    [Preserve(AllMembers = true)]

   
    public class ZakatSummaryViewModel
    {

        public string billNumber { get; set; }
        public string amount { get; set; }
        public string saadNumber { get; set; }
        public string taxPeriod { get; set; }
        public bool isSelected { get; set; }
        public string billType { get; set; }

    }
    [Preserve(AllMembers = true)]

    public class InstalmentAgreementInstalmentPlansModel
    {
        public string DueDate { get; set; }
        public string NumberOfMonths { get; set; }
        public string MonthlyInstalment { get; set; }
        public string TotalAmountPaid { get; set; }
        public string TotalAmountRemaining { get; set; }
    }
    [Preserve(AllMembers = true)]

    //-----API Object will starts from herer-------



    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 


    //[Preserve(AllMembers = true)]
    public partial class ZakatInstalmentPlanResponse
    {
        public ZakatInstalment d { get; set; }
    }
    [Preserve(AllMembers = true)]


    public partial class ZakatInstalment
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



    public partial class AttachSet
    {
        public Attachment[] results { get; set; }
    }
    [Preserve(AllMembers = true)]

    public partial class FnDtlSet
    {
        public FnDtlSetObject[] results { get; set; }
    }
    [Preserve(AllMembers = true)]

    public partial class InsPlanOffSet
    {
        public object[] result { get; set; }
    }
    [Preserve(AllMembers = true)]

    public partial class InsPlanSet
    {
        public Results4[] results { get; set; }
    }
    [Preserve(AllMembers = true)]

    public partial class Results4
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

    public partial class InvDtlsSet
    {
        public Results5[] results { get; set; }
    }
    [Preserve(AllMembers = true)]

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
    [Preserve(AllMembers = true)]



    public partial class RetmsgSet
    {
        public object[] results { get; set; }
    }
    [Preserve(AllMembers = true)]


    public partial class ZakatInstalmentPlanRequest
    {
        public string Fbguid { get; set; }
        public string UserTyp { get; set; }
        public string TxnTp { get; set; }
        public string MobNo { get; set; }
        public string FormGuid { get; set; }
        public string Fbnum { get; set; }
        public string DataVersion { get; set; }
        public string Operation { get; set; }
        public string Euser { get; set; }
        public string StepNumber { get; set; }
        public string Email { get; set; }
        public string Officer { get; set; }
        public string Langz { get; set; }
        public string Status { get; set; }
        public string SuAmt { get; set; }
        public string SuAmtFg { get; set; }
        public string Formproc { get; set; }
        public string Tin { get; set; }
        public string Periodkey { get; set; }
        public string ReturnId { get; set; }
        public string TinNm { get; set; }
        public string AltMobNo { get; set; }
        public string InstReqFor { get; set; }
        public string InstReqReason { get; set; }
        public string TotAmt { get; set; }
        public string DpAmt { get; set; }
        public string PymntFreq { get; set; }
        public string PlanDur { get; set; }
        public string OffPymntFreq { get; set; }
        public string OffPlanDur { get; set; }
        public string DecCb { get; set; }
        public string Waers { get; set; }
        public string AccMethod { get; set; }
        public string Sopbel { get; set; }
        public string OffAmt { get; set; }
        public string PaymtDt { get; set; }
        public string PenlAmt { get; set; }
        public string InsDtOff { get; set; }
        public FnDtlSetObject[] FnDtlSet { get; set; }
        public object[] AttachSet { get; set; }
        public NotesSet[] NotesSet { get; set; }
        public ZakatInvoicesResult[] invDtlsSet { get; set; }
        public object[] insPlanSet { get; set; }
        public object[] retmsgSet { get; set; }
        public object[] insPlan_OffSet { get; set; }
    }


 
    public class ZAKATRequestPlanModel
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
    public partial class ZakatInvoiceList
    {
        public ZakatInvoices d { get; set; }
    }
    [Preserve(AllMembers = true)]

    public partial class ZakatInvoices
    {
        public List<ZakatInvoicesResult> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class ZakatInvoicesResult
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

    

    public class ZakatInstalmentPlanRequestListModel
    {
        [Preserve(AllMembers = true)] // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

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
        public class WorklistSet
        {
            public List<Result> results { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class RevokeListSet
        {
            public List<RevokeListResult> results { get; set; }
        }
        [Preserve(AllMembers = true)]
        public partial class RevokeListResult
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
        public class EvtNotif12Set
        {
            public List<Result2> results { get; set; }
        }
        [Preserve(AllMembers = true)]
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
        [Preserve(AllMembers = true)]
        public class EvtNotif1Set
        {
            public List<object> results { get; set; }
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
    public partial class ZakatRequestDisplayModel
    {
        public D d { get; set; }
    }
    [Preserve(AllMembers = true)]
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
    [Preserve(AllMembers = true)]
    public partial class AttDetSet
    {
        public List<object> results { get; set; }
    }
    [Preserve(AllMembers = true)]

    public partial class OffNotesSet
    {
        public List<NotesSet> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class ZInvoiceSet
    {
        public List<object> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class ZInvoiceUi5Set
    {
        public List<Results3> results { get; set; }
    }
    [Preserve(AllMembers = true)]
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
    [Preserve(AllMembers = true)]
    public partial class ZProposedinsSet
    {
        public List<object> results { get; set; }
    }
  
    public class ZakatSummaryInputModel
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

    [Preserve(AllMembers = true)]
    public partial class ZakatInstalmentPlanRevokeResponse
    {
        public ZakatRevoke d { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class ZakatRevoke
    {
        public Metadata __metadata { get; set; }
        public string Lokst { get; set; }
        public string Fbguid { get; set; }
        public string FbnumIprr { get; set; }
        public string InsDtOff { get; set; }
        public string PenlAmt { get; set; }
        public string SuAmtFg { get; set; }
        public string OffAmt { get; set; }
        public string TotAmt { get; set; }
        public string UserTyp { get; set; }
        public string DpAmt { get; set; }
        public string FormMode { get; set; }
        public string PaymtDt { get; set; }
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
        public Set FnDtlSet { get; set; }
        public Set insPlan_OffSet { get; set; }
        public Set invDtlsSet { get; set; }
        public AttachSet AttachSet { get; set; }
        public Set insPlanSet { get; set; }
        public NotesSetResult NotesSet { get; set; }
        public Set retmsgSet { get; set; }
    }

    [Preserve(AllMembers = true)]

    public partial class AttachSetResults
    {
        public Metadata __metadata { get; set; }
        public string RetGuid { get; set; }
        public string Seqno { get; set; }
        public string SchGuid { get; set; }
        public string Dotyp { get; set; }
        public long Srno { get; set; }
        public string Doguid { get; set; }
        public string AttBy { get; set; }
        public string Filename { get; set; }
        public string FileExtn { get; set; }
        public string Mimetype { get; set; }
        public string ByPusr { get; set; }
        public string Erfdt { get; set; }
        public string Erftm { get; set; }
        public string DataVersion { get; set; }
        public string DocUrl { get; set; }
        public string OutletRef { get; set; }
        public string Enbedit { get; set; }
        public string Enbdele { get; set; }
    }



    [Preserve(AllMembers = true)]

    public partial class Set
    {
        public Deferred Deferred { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class Deferred
    {
        public Uri Uri { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class NotesSetResult
    {
        public NotesSet[] results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class NotesSet
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

    public partial class ZakatInstalmentPlanRevokeRequest
    {
        public string Fbguid { get; set; }
        public string UserTyp { get; set; }
        public string TxnTp { get; set; }
        public string MobNo { get; set; }
        public string FormGuid { get; set; }
        public string Fbnum { get; set; }
        public string DataVersion { get; set; }
        public string Operation { get; set; }
        public string Euser { get; set; }
        public string StepNumber { get; set; }
        public string Email { get; set; }
        public string Officer { get; set; }
        public string Langz { get; set; }
        public string Status { get; set; }
        public string FbnumIprr { get; set; }
        public string SuAmt { get; set; }
        public string SuAmtFg { get; set; }
        public string Formproc { get; set; }
        public string Tin { get; set; }
        public string Periodkey { get; set; }
        public string ReturnId { get; set; }
        public string TinNm { get; set; }
        public string AltMobNo { get; set; }
        public string InstReqFor { get; set; }
        public string InstReqReason { get; set; }
        public string TotAmt { get; set; }
        public string DpAmt { get; set; }
        public string PymntFreq { get; set; }
        public string PlanDur { get; set; }
        public string OffPymntFreq { get; set; }
        public string OffPlanDur { get; set; }
        public string DecCb { get; set; }
        public string Waers { get; set; }
        public string AccMethod { get; set; }
        public string Sopbel { get; set; }
        public string OffAmt { get; set; }
        public string PaymtDt { get; set; }
        public string PenlAmt { get; set; }
        public object InsDtOff { get; set; }
        public object[] FnDtlSet { get; set; }
        public AttachSetResults[] AttachSet { get; set; }
        public NotesSet[] NotesSet { get; set; }
        public ZakatInvoicesResult[] invDtlsSet { get; set; }
        public object[] insPlanSet { get; set; }
        public object[] retmsgSet { get; set; }
        public object[] insPlan_OffSet { get; set; }
    }
  

    public class ZakatInstalmentInvListModel
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

    public class SummaryDisplayModel
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

