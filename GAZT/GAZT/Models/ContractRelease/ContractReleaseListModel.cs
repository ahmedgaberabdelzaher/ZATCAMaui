using System;
using System.Collections.Generic;
using Xamarin.Forms.Internals;

namespace EGAZT.Models.ContractRelease
{
  
    public class ContractReleaseListModel
    {
        public ContractReleaseListModel()
        {
        }
    }

  
    public class ContractReleaseSummaryModel
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
        public class Metadata2
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class Attachment
        {
            public Metadata2 __metadata { get; set; }
            public string RetGuid { get; set; }
            public string DocUrl { get; set; }
            public string Seqno { get; set; }
            public string SchGuid { get; set; }
            public string Dotyp { get; set; }
            public int Srno { get; set; }
            public string Doguid { get; set; }
            public string AttBy { get; set; }
            public string Filename { get; set; }
            public string FileExtn { get; set; }
            public string Mimetype { get; set; }
            public string Erfdt { get; set; }
            public string Erftm { get; set; }
            public string Enbedit { get; set; }
            public string Enbdele { get; set; }
            public string Visedit { get; set; }
            public string Visdel { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class AttDetSet
        {
            //public List<Attachment> results { get; set; }
            public List<Models.Attachment> results { get; set; }
        }
        [Preserve(AllMembers = true)]
        public partial class ZnotesSet
        {
            public Metadata __metadata { get; set; }
            public string Notenoz { get; set; }
            public string Refnamez { get; set; }
            public string XInvoicez { get; set; }
            public string XObsoletez { get; set; }
            public string Rcodez { get; set; }
            public string Erfusrz { get; set; }
            public object Erfdtz { get; set; }
            public object Erftmz { get; set; }
            public string AttByz { get; set; }
            public string Noteno { get; set; }
            public long Lineno { get; set; }
            public long ElemNo { get; set; }
            public string Tdformat { get; set; }
            public string Tdline { get; set; }
        }


        [Preserve(AllMembers = true)]
        public class D
        {
            public Metadata __metadata { get; set; }
            public string Euser { get; set; }
            public string UserTin { get; set; }
            public string Auditorz { get; set; }
            public string Fbnumz { get; set; }
            public string Langz { get; set; }
            public string PeriodKeyz { get; set; }
            public string RegIdz { get; set; }
            public string Savez { get; set; }
            public string Submitz { get; set; }
            public string Taxpayerz { get; set; }
            public string AAgreeTm { get; set; }
            public string AOtherDes { get; set; }
            public string AContEndDt { get; set; }
            public string AContEndDtCh { get; set; }
            public string AHijriPeriodFrom { get; set; }
            public string PeriodKey { get; set; }
            public string ABranch { get; set; }
            public string AContEndDtFg { get; set; }
            public string AHijriPeriodTo { get; set; }
            public string ACalTp { get; set; }
            public string AComments { get; set; }
            public string AContChk { get; set; }
            public string AContDt { get; set; }
            public string AContDt1 { get; set; }
            public string AContDtFg { get; set; }
            public string AContNm { get; set; }
            public string AContNo { get; set; }
            public string AContProfit { get; set; }
            public string AContProfitPer { get; set; }
            public string ADoc1 { get; set; }
            public string ADoc2 { get; set; }
            public string ADoc3 { get; set; }
            public string ADueTax { get; set; }
            public string ADueTot { get; set; }
            public string ADueZakat { get; set; }
            public string AInvoiceChk { get; set; }
            public string AmdRsnz { get; set; }
            public object APeriodFrom { get; set; }
            public object APeriodTo { get; set; }
            public string Approvez { get; set; }
            public object AReceiveDt { get; set; }
            public string ARemark { get; set; }
            public string AReqAmt { get; set; }
            public string ATaxProfi { get; set; }
            public string ATaxProfitPer { get; set; }
            public string ATin { get; set; }
            public string ATotalAmt { get; set; }
            public string ATpNm { get; set; }
            public string AType { get; set; }
            public string AZakatProfit { get; set; }
            public string AZakatProfitPer { get; set; }
            public string CaseGuid { get; set; }
            public string CreateTxAssesz { get; set; }
            public string CurrDatumz { get; set; }
            public string Fbnum { get; set; }
            public string FormGuid { get; set; }
            public string LegacyDocNo { get; set; }
            public string Mandt { get; set; }
            public string Monthz { get; set; }
            public string OfficerUidz { get; set; }
            public string PortalUsrz { get; set; }
            public string Rejectz { get; set; }
            public string Status { get; set; }
            public string Textnote { get; set; }
            public string Xvoidz { get; set; }
            public AttDetSet AttDetSet { get; set; }
            public ZnotesSetResult znotesSet { get; set; }

        }
        [Preserve(AllMembers = true)]
        public class ZnotesSetResult
        {
            public ZnotesSet[] results { get; set; }
        }
        [Preserve(AllMembers = true)]
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

            public AttDetSet AttDetSet { get; set; }
            public ZnotesSetResult znotesSet { get; set; }


        }


    }
}
