using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace EGAZT.Models.ZakatInstalationModels
{
    public class ZakatInstalmentPlanModel
    {
        public ZakatInstalmentPlanModel()
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

    public class Metadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class ZakatInstalmentPlanResponse
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

        public ZakatInstalment d { get; set; }


        public class AttDetSet
        {
            public AttDetSet[] results { get; set; }
        }

        public class OffNotesSet
        {
            public OffNotesSet[] results { get; set; }
        }

        public class ZInvoiceSet
        {
            public ZInvoiceSet[] results { get; set; }
        }

        public class ZProposedinsSet
        {
            public ZProposedinsSet[] results { get; set; }
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
            public string AAmtTb { get; set; }
            public string AClearedAmtTb { get; set; }
            public string ADueAmtTb { get; set; }
            public object ADueDtTb { get; set; }
            public string AIvAmtTb { get; set; }
            public string AIvNoTb { get; set; }
            public string AIvSrNoTb { get; set; }
            public string AIvTb { get; set; }
            public string AIvAbtyp { get; set; }
        }

        public class ZInvoiceResult
        {
            public Metadata2 __metadata { get; set; }
            public string AAmtTb { get; set; }
            public string AClearedAmtTb { get; set; }
            public string ADueAmtTb { get; set; }
            public object ADueDtTb { get; set; }
            public string AIvAmtTb { get; set; }
            public string AIvNoTb { get; set; }
            public string AIvSrNoTb { get; set; }
            public string AIvTb { get; set; }
            public string AIvAbtyp { get; set; }
        }

        public class ZINVOICEUI5Set
        {
            public ZInvoiceResult[] results { get; set; }
        }

        public class ZakatInstalment
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
            public long Operation { get; set; }
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
           // public Waers Waers { get; set; }
            public string AccMethod { get; set; }
            //public Set FnDtlSet { get; set; }
            //public Set InsPlanOffSet { get; set; }
            //public Set InvDtlsSet { get; set; }
            //public Set AttachSet { get; set; }
            //public Set InsPlanSet { get; set; }
            //public Set NotesSet { get; set; }
            //public Set RetmsgSet { get; set; }
            public AttDetSet AttDetSet { get; set; }
            public OffNotesSet Off_notesSet { get; set; }
            public ZInvoiceSet z_invoiceSet { get; set; }
            public ZProposedinsSet z_proposedinsSet { get; set; }
            public ZINVOICEUI5Set Z_INVOICE_UI5Set { get; set; }
        }
    }

    public class ZINVOICEUI5Set
    {
        public Metadata2 __metadata { get; set; }
        public string AAmtTb { get; set; }
        public string AClearedAmtTb { get; set; }
        public string ADueAmtTb { get; set; }
        public object ADueDtTb { get; set; }
        public string AIvAmtTb { get; set; }
        public string AIvNoTb { get; set; }
        public string AIvSrNoTb { get; set; }
        public string AIvTb { get; set; }
        public string AIvAbtyp { get; set; }
    }

    public class ZakatInstalmentPlanRequest
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public ZakatInstalmentReuqest d { get; set; }


        public class Metadata2
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }



        public class ZakatInstalmentReuqest
        {
            public string Fbguid { get; set; }
            public string UserTyp { get; set; }
            public string TxnTp { get; set; }
            public string MobNo { get; set; }
            public string FormGuid { get; set; }
            public string Fbnum { get; set; }
            public string DataVersion { get; set; }
            public long Operation { get; set; }
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
            public long PlanDur { get; set; }
            public string OffPymntFreq { get; set; }
            public string OffPlanDur { get; set; }
            public string DecCb { get; set; }
            public string Waers { get; set; }
            public string AccMethod { get; set; }
            public string Sopbel { get; set; }
            public string OffAmt { get; set; }
            public object PaymtDt { get; set; }
            public string PenlAmt { get; set; }
            public object InsDtOff { get; set; }
            public ZINVOICEUI5Set[] Z_INVOICE_UI5Set { get; set; }
            //public List<object> Z_INVOICE_UI5Set { get; set; }
        }

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
            public DateTime Abrzu { get; set; }
            public DateTime Abrzo { get; set; }
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
        public ZakatInvoices d { get; set; }
    }

    public partial class ZakatInvoices
    {
        public List<ZakatInvoicesResult> results { get; set; }
    }

    public partial class ZakatInvoicesResult
    {
        public Metadata __metadata { get; set; }
        public string InvCbDeflt { get; set; }
        public long DueYr { get; set; }
        public string TotAmt { get; set; }
        public string Fbnum { get; set; }
        public string Fbguid { get; set; }
        public bool Inccbflag { get; set; }
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
        public long InvNo { get; set; }
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
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

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

        public class Metadata2
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }

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
        }

        public class WorklistSet
        {
            public List<Result> results { get; set; }
        }

        public class RevokeListSet
        {
            public List<object> results { get; set; }
        }


        public class EvtNotif12Set
        {
            public List<Result2> results { get; set; }
        }

        public class EvtNotif1Set
        {
            public List<object> results { get; set; }
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
    public class ZakatRequestDisplayModel
    {
        public D d { get; set; }
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
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
            public string DocUrl { get; set; }
            public string ByPusr { get; set; }
            public string Mimetype { get; set; }
            public string RetGuid { get; set; }
            public string DataVersion { get; set; }
            public string Seqno { get; set; }
            public string SchGuid { get; set; }
            public string Dotyp { get; set; }
            public int Srno { get; set; }
            public string Doguid { get; set; }
            public string AttBy { get; set; }
            public string Filename { get; set; }
            public string FileExtn { get; set; }
            public string Erfdt { get; set; }
            public string Erftm { get; set; }
            public string Enbedit { get; set; }
            public string Enbdele { get; set; }
            public string Visedit { get; set; }
            public string Visdel { get; set; }
        }

        public class AttDetSet
        {
            public List<Result> results { get; set; }
        }

        public class OffNotesSet
        {
            public List<object> results { get; set; }
        }

        public class ZInvoiceSet
        {
            public List<object> results { get; set; }
        }

        public class ZProposedinsSet
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
            public string AAmtTb { get; set; }
            public string AClearedAmtTb { get; set; }
            public string ADueAmtTb { get; set; }
            public DateTime ADueDtTb { get; set; }
            public string AIvAmtTb { get; set; }
            public string AIvNoTb { get; set; }
            public string AIvSrNoTb { get; set; }
            public string AIvTb { get; set; }
            public string AIvAbtyp { get; set; }
        }

        public class ZINVOICEUI5Set
        {
            public List<Result2> results { get; set; }
        }

        public class D
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
            public AttDetSet AttDetSet { get; set; }
            public OffNotesSet Off_notesSet { get; set; }
            public ZInvoiceSet z_invoiceSet { get; set; }
            public ZProposedinsSet z_proposedinsSet { get; set; }
            public ZINVOICEUI5Set Z_INVOICE_UI5Set { get; set; }
        }



        public class DisplayInfoModel
        {
            public List<Result2> Z_INVOICE_UI5Set { get; set; }
            public string DownPaymentAmount { get; set; }
            public string PaymentFrequency { get; set; }
            public string PlanDuration { get; set; }


        }



    }

    public class ZakatSummaryInputModel
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
            public object Begda { get; set; }
            public object Endda { get; set; }
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
            public object DueDt { get; set; }
            public string Fbnum { get; set; }
            public string Status { get; set; }
            public string CalendrTyp { get; set; }
            public object Abrzu { get; set; }
            public object Abrzo { get; set; }
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
            public object Aedat { get; set; }
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

}
