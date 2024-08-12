using Foundation;
using Newtonsoft.Json;

namespace ZATCAMAUI.Models.ContractRelease
{

    public class ContractReleaseModel
    {
        public ContractReleaseModel()
        {
        }
    }

    public class ContractReleaseFormResponse
    {
        [Preserve(AllMembers = true)]
        [JsonProperty("data")]
        public CotractResponse d { get; set; }

    }
    public class ContractReleaseFormResponse1
    {
        [Preserve(AllMembers = true)]
        [JsonProperty("result")]
        public CotractResponse d { get; set; }

    }
    public class ContractReleaseFormRequest
    {
        [Preserve(AllMembers = true)]
        [JsonProperty("data")]
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public CotractRequest d { get; set; }

    }
    [Preserve(AllMembers = true)]
    public class Metadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    [Preserve(AllMembers = true)]
    public class AttDetSetResult
    {
        [JsonProperty("attachments")]
        public AttDetSet[] results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class ZnotesSetResult
    {
        [JsonProperty("notes")]
        public ZnotesSet[] results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class AttDetSet
    {


    }


    [Preserve(AllMembers = true)]
    public partial class ZnotesSet
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
        public object Erfdtz { get; set; }
        [JsonProperty("createdAt")]
        public object Erftmz { get; set; }
        [JsonProperty("attachedByPerson")]
        public string AttByz { get; set; }
        // [JsonProperty("")]
        public string Noteno { get; set; }
        [JsonProperty("lineNumber")]
        public long Lineno { get; set; }
        [JsonProperty("elementNumber")]
        public long ElemNo { get; set; }
        [JsonProperty("notesFormat")]
        public string Tdformat { get; set; }
        [JsonProperty("textLine")]
        public string Tdline { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class CotractResponse
    {
        public Metadata __metadata { get; set; }
        [JsonProperty("serialNumber")]
        public string Euser { get; set; }
        [JsonProperty("userTIN")]
        public string UserTin { get; set; }
        [JsonProperty("auditor")]
        public string Auditorz { get; set; }
        [JsonProperty("formBundleNumber")]
        public string Fbnumz { get; set; }
        [JsonProperty("language")]
        public string Langz { get; set; }
        //[JsonProperty("")]
        public string PeriodKeyz { get; set; }
        // [JsonProperty("")]
        public string RegIdz { get; set; }
        [JsonProperty("save")]
        public string Savez { get; set; }
        [JsonProperty("submit")]
        public string Submitz { get; set; }
        [JsonProperty("TIN")]
        public string Taxpayerz { get; set; }
        [JsonProperty("agreedTime")]
        public string AAgreeTm { get; set; }
        [JsonProperty("otherDescription")]
        public string AOtherDes { get; set; }
        [JsonProperty("contractEndDateTime")]
        public object AContEndDt { get; set; }
        [JsonProperty("contractEndDate")]
        public string AContEndDtCh { get; set; }
        [JsonProperty("hijriPeriodFrom")]
        public string AHijriPeriodFrom { get; set; }
        [JsonProperty("periodKey")]
        public string PeriodKey { get; set; }
        [JsonProperty("authorizationGroup")]
        public string ABranch { get; set; }
        [JsonProperty("contractEndDateCalendarType")]
        public string AContEndDtFg { get; set; }
        [JsonProperty("hijriPeriodTo")]
        public string AHijriPeriodTo { get; set; }
        [JsonProperty("calendarType")]
        public string ACalTp { get; set; }
        [JsonProperty("comments")]
        public string AComments { get; set; }
        [JsonProperty("isContract")]
        public string AContChk { get; set; }
        [JsonProperty("contractDateTime")]
        public object AContDt { get; set; }
        [JsonProperty("contractDate")]
        public string AContDt1 { get; set; }
        [JsonProperty("contractDateCalendarType")]
        public string AContDtFg { get; set; }
        [JsonProperty("contractName")]
        public string AContNm { get; set; }
        [JsonProperty("contractNumber")]
        public string AContNo { get; set; }
        [JsonProperty("contractProfitAmount")]
        public string AContProfit { get; set; }
        [JsonProperty("contractProfitPercentage")]
        public string AContProfitPer { get; set; }
        [JsonProperty("isDocument1")]
        public string ADoc1 { get; set; }
        [JsonProperty("isDocument2")]
        public string ADoc2 { get; set; }
        [JsonProperty("isDocument3")]
        public string ADoc3 { get; set; }
        [JsonProperty("dueTaxAmount")]
        public string ADueTax { get; set; }
        [JsonProperty("dueTotalAmount")]
        public string ADueTot { get; set; }
        [JsonProperty("dueZakatAmount")]
        public string ADueZakat { get; set; }
        [JsonProperty("isInvoice")]
        public string AInvoiceChk { get; set; }
        [JsonProperty("amendmentReason")]
        public string AmdRsnz { get; set; }
        [JsonProperty("billPeriodFrom")]
        public object APeriodFrom { get; set; }
        [JsonProperty("billPeriodTo")]
        public object APeriodTo { get; set; }
        [JsonProperty("approve")]
        public string Approvez { get; set; }
        [JsonProperty("receiveDate")]
        public object AReceiveDt { get; set; }
        [JsonProperty("remark")]
        public string ARemark { get; set; }
        [JsonProperty("requestAmount")]
        public string AReqAmt { get; set; }
        [JsonProperty("taxProfitAmount")]
        public string ATaxProfi { get; set; }
        [JsonProperty("taxProfitPercentage")]
        public string ATaxProfitPer { get; set; }
        //  [JsonProperty("")]
        public string ATin { get; set; }
        [JsonProperty("totalAmount")]
        public string ATotalAmt { get; set; }
        [JsonProperty("taxpayerName")]
        public string ATpNm { get; set; }
        [JsonProperty("taxpayerType")]
        public string AType { get; set; }
        [JsonProperty("zakatProfitAmount")]
        public string AZakatProfit { get; set; }
        [JsonProperty("zakatProfitPercentage")]
        public string AZakatProfitPer { get; set; }
        [JsonProperty("caseGUID")]
        public string CaseGuid { get; set; }
        [JsonProperty("createTaxAssessment")]
        public string CreateTxAssesz { get; set; }
        [JsonProperty("currentDate")]
        public string CurrDatumz { get; set; }
        //[JsonProperty("")]
        public string Fbnum { get; set; }
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [JsonProperty("legacyDocumentNumber")]
        public string LegacyDocNo { get; set; }
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [JsonProperty("month")]
        public string Monthz { get; set; }
        // [JsonProperty("")]
        public string OfficerUidz { get; set; }
        [JsonProperty("portalUser")]
        public string PortalUsrz { get; set; }
        [JsonProperty("reject")]
        public string Rejectz { get; set; }
        [JsonProperty("statusCode")]
        public string Status { get; set; }
        [JsonProperty("noteDescription")]
        public string Textnote { get; set; }
        [JsonProperty("void")]
        public string Xvoidz { get; set; }
        [JsonProperty("attachments")]
        public AttDetSet[] AttDetSet { get; set; }
        [JsonProperty("notes")]
        public ZnotesSet[] znotesSet { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class CotractRequest
    {
        public Metadata __metadata { get; set; }
        [JsonProperty("serialNumber")]
        public string Euser { get; set; }
        [JsonProperty("userTIN")]
        public string UserTin { get; set; }
        [JsonProperty("auditor")]
        public string Auditorz { get; set; }
        [JsonProperty("formBundleNumber")]
        public string Fbnumz { get; set; }
        [JsonProperty("language")]
        public string Langz { get; set; }
        // [JsonProperty("")]
        public string PeriodKeyz { get; set; }
        [JsonProperty("registrationId")]
        public string RegIdz { get; set; }
        [JsonProperty("save")]
        public string Savez { get; set; }
        [JsonProperty("submit")]
        public string Submitz { get; set; }
        [JsonProperty("TIN")]
        public string Taxpayerz { get; set; }
        [JsonProperty("agreedTime")]
        public string AAgreeTm { get; set; }
        [JsonProperty("otherDescription")]
        public string AOtherDes { get; set; }
        [JsonProperty("contractEndDateTime")]
        public object AContEndDt { get; set; }
        [JsonProperty("contractEndDate")]
        public string AContEndDtCh { get; set; }
        [JsonProperty("hijriPeriodFrom")]
        public string AHijriPeriodFrom { get; set; }
        [JsonProperty("periodKey")]
        public string PeriodKey { get; set; }
        [JsonProperty("authorizationGroup")]
        public string ABranch { get; set; }
        [JsonProperty("contractEndDateCalendarType")]
        public string AContEndDtFg { get; set; }
        [JsonProperty("hijriPeriodTo")]
        public string AHijriPeriodTo { get; set; }
        [JsonProperty("calendarType")]
        public string ACalTp { get; set; }
        [JsonProperty("comments")]
        public string AComments { get; set; }
        [JsonProperty("isContract")]
        public string AContChk { get; set; }
        [JsonProperty("contractDateTime")]
        public object AContDt { get; set; }
        [JsonProperty("contractDate")]
        public string AContDt1 { get; set; }
        [JsonProperty("contractDateCalendarType")]
        public string AContDtFg { get; set; }
        [JsonProperty("contractName")]
        public string AContNm { get; set; }
        [JsonProperty("contractNumber")]
        public string AContNo { get; set; }
        [JsonProperty("contractProfitAmount")]
        public string AContProfit { get; set; }
        [JsonProperty("contractProfitPercentage")]
        public string AContProfitPer { get; set; }
        [JsonProperty("isDocument1")]
        public string ADoc1 { get; set; }
        [JsonProperty("isDocument2")]
        public string ADoc2 { get; set; }
        [JsonProperty("isDocument3")]
        public string ADoc3 { get; set; }
        [JsonProperty("dueTaxAmount")]
        public string ADueTax { get; set; }
        [JsonProperty("dueTotalAmount")]
        public string ADueTot { get; set; }
        [JsonProperty("dueZakatAmount")]
        public string ADueZakat { get; set; }
        [JsonProperty("isInvoice")]
        public string AInvoiceChk { get; set; }
        [JsonProperty("amendmentReason")]
        public string AmdRsnz { get; set; }
        [JsonProperty("billPeriodFrom")]
        public object APeriodFrom { get; set; }
        [JsonProperty("billPeriodTo")]
        public object APeriodTo { get; set; }
        [JsonProperty("approve")]
        public string Approvez { get; set; }
        [JsonProperty("receiveDate")]
        public object AReceiveDt { get; set; }

        [JsonProperty("declaration")]
        public object declaration { get; set; }
        [JsonProperty("remark")]
        public string ARemark { get; set; }
        [JsonProperty("requestAmount")]
        public string AReqAmt { get; set; }
        [JsonProperty("taxProfitAmount")]
        public string ATaxProfi { get; set; }
        [JsonProperty("taxProfitPercentage")]
        public string ATaxProfitPer { get; set; }
        // [JsonProperty("")]
        public string ATin { get; set; }
        [JsonProperty("totalAmount")]
        public string ATotalAmt { get; set; }
        [JsonProperty("taxpayerName")]
        public string ATpNm { get; set; }
        [JsonProperty("taxpayerType")]
        public string AType { get; set; }
        [JsonProperty("zakatProfitAmount")]
        public string AZakatProfit { get; set; }
        [JsonProperty("zakatProfitPercentage")]
        public string AZakatProfitPer { get; set; }
        [JsonProperty("caseGUID")]
        public string CaseGuid { get; set; }
        [JsonProperty("createTaxAssessment")]
        public string CreateTxAssesz { get; set; }
        [JsonProperty("currentDate")]
        public string CurrDatumz { get; set; }
        // [JsonProperty("")]
        public string Fbnum { get; set; }
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [JsonProperty("legacyDocumentNumber")]
        public string LegacyDocNo { get; set; }
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [JsonProperty("month")]
        public string Monthz { get; set; }
        //[JsonProperty("")]
        public string OfficerUidz { get; set; }
        [JsonProperty("portalUser")]
        public string PortalUsrz { get; set; }
        [JsonProperty("reject")]
        public string Rejectz { get; set; }
        [JsonProperty("statusCode")]
        public string Status { get; set; }
        [JsonProperty("noteDescription")]
        public string Textnote { get; set; }
        [JsonProperty("void")]
        public string Xvoidz { get; set; }
        [JsonProperty("attachments")]
        public AttDetSet[] AttDetSet { get; set; }
        [JsonProperty("notes")]
        public ZnotesSet[] znotesSet { get; set; }
    }

    public class ContractReLeaseApplicationFormModel
    {
        [Preserve(AllMembers = true)]
        [JsonProperty("data")]
        public D d { get; set; }
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        [Preserve(AllMembers = true)]
        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class Metadata2
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class ContractResult
        {
            public Metadata2 __metadata { get; set; }
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
            public string DueDt { get; set; }
            // [JsonProperty("")]
            public string Due { get; set; }
            [JsonProperty("billPeriodStartDate")]
            public string Abrzu { get; set; }
            [JsonProperty("billPeriodEndDate")]
            public string Abrzo { get; set; }
            [JsonProperty("inboundCorrespondenceType")]
            public string Incotyp { get; set; }
            [JsonProperty("inboundCorrespondenceDescription")]
            public string Incotext { get; set; }
            [JsonProperty("flag")]
            public string Flag { get; set; }
            // [JsonProperty("")]
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
        [Preserve(AllMembers = true)]
        public class ListSet
        {
            [JsonProperty("lists")]
            public List<ContractResult> results { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class AuthServSet
        {
            [JsonProperty("penalties")]
            public List<object> results { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class D
        {
            //public Metadata __metadata { get; set; }
            [JsonProperty("callService")]
            public string CallServ { get; set; }
            [JsonProperty("accountNumber")]
            public int Accnum { get; set; }
            [JsonProperty("activeCount")]
            public int Actcnt { get; set; }
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
            // [JsonProperty("")]
            public string Caltype { get; set; }
            [JsonProperty("client")]
            public string Client { get; set; }
            [JsonProperty("cancellationCount")]
            public int Cnlcnt { get; set; }
            [JsonProperty("correspondenceNumber")]
            public int Corrnum { get; set; }
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
            public int Indcorrnum { get; set; }
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
            public int Oblnum { get; set; }
            [JsonProperty("overdueAmount")]
            public string Overdue { get; set; }
            [JsonProperty("penaltyAmount")]
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
            public int Zfillingoblig { get; set; }
            [JsonProperty("registrationStatus")]
            public string Zregstatus { get; set; }
            [JsonProperty("email")]
            public string Zuser { get; set; }
            [JsonProperty("lists")]
            public List<ContractResult> ListSet { get; set; }
            [JsonProperty("authorizationServer")]
            public List<object> AuthServSet { get; set; }
        }
    }
}