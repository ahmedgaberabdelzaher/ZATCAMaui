
using Newtonsoft.Json;

namespace ZATCAMAUI.Models.ContractReleas
{

    public class ContractReleaseListModel
    {
        public ContractReleaseListModel()
        {
        }
    }


    public class ContractReleaseSummaryModel
    {
        
        [JsonProperty("data")]
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
        
        public class Attachment
        {
            public Metadata2 __metadata { get; set; }
            [JsonProperty("returnGUID")]
            public string RetGuid { get; set; }
            [JsonProperty("documentURL")]
            public string DocUrl { get; set; }
            [JsonProperty("sequenceNumber")]
            public string Seqno { get; set; }
            // [JsonProperty("")]
            public string SchGuid { get; set; }
            [JsonProperty("documentCategory")]
            public string Dotyp { get; set; }
            [JsonProperty("serialNumber")]
            public int Srno { get; set; }
            [JsonProperty("documentId")]
            public string Doguid { get; set; }
            [JsonProperty("attachedByPerson")]
            public string AttBy { get; set; }
            [JsonProperty("fileName")]
            public string Filename { get; set; }
            [JsonProperty("fileExtension")]
            public string FileExtn { get; set; }
            [JsonProperty("MIMEType")]
            public string Mimetype { get; set; }
            [JsonProperty("entryDate")]
            public string Erfdt { get; set; }
            [JsonProperty("createdAt")]
            public string Erftm { get; set; }
            [JsonProperty("enableEdit")]
            public string Enbedit { get; set; }
            [JsonProperty("enableDelete")]
            public string Enbdele { get; set; }
            [JsonProperty("visibleEdit")]
            public string Visedit { get; set; }
            [JsonProperty("visibleDelete")]
            public string Visdel { get; set; }
        }
        
        public class AttDetSet
        {
            [JsonProperty("attachments")]
            public List<Attachment> results { get; set; }
        }
        
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


        
        public class D
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
            //[JsonProperty("periodKey")]
            public string PeriodKeyz { get; set; }
            //[JsonProperty("")]
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
            public string AContEndDt { get; set; }
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
            public string AContDt { get; set; }
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
            //[JsonProperty("")]
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
            public List<Models.Attachment> AttDetSet { get; set; }
            [JsonProperty("notes")]
            public ZnotesSet[] znotesSet { get; set; }

        }
        
        public class ZnotesSetResult
        {
            [JsonProperty("notes")]
            public ZnotesSet[] results { get; set; }
        }
        
        public class ContractReleaseSummaryData
        {

            public string RequestNumber { get; set; }
            public string TaxpayerName { get; set; }
            public string ContractingName { get; set; }
            public string Number { get; set; }
            public string Type { get; set; }
            public string ContractDate { get; set; }
            public string ContractEnddate { get; set; }
            public string TotalAmountofContract { get; set; }
            public string AmountRequiredtoRelease { get; set; }
            public string ContractprofitEstimatedRate { get; set; }
            public string ProfitEstimatedforContract { get; set; }
            public string EstimatedProfitforZakat { get; set; }
            public string EstimatedProfitforTax { get; set; }
            public string TheValueofZakatdues { get; set; }
            public string TheValueofTaxdues { get; set; }
            public string TotalDues { get; set; }
            public string Remark { get; set; }
            public string DetaiiledDesc { get; set; }

            public List<Models.Attachment> AttDetSet { get; set; }
            public ZnotesSet[] znotesSet { get; set; }


        }


    }
}
