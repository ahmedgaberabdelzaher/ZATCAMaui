using System;
using System.Collections.Generic;
using Xamarin.Forms.Internals;

namespace EGAZT.Models.ContractRelease
{
    [Preserve(AllMembers = true)]
    public class ContractReleaseModel
    {
        public ContractReleaseModel()
        {
        }
    }
    [Preserve(AllMembers = true)]
    public class ContractReleaseFormResponse
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public CotractResponse d { get; set; }

    }
    [Preserve(AllMembers = true)]
    public class ContractReleaseFormRequest
    {
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
            public AttDetSet[] results { get; set; }
        }
    [Preserve(AllMembers = true)]
    public class ZnotesSetResult
          {
                public ZnotesSet[] results { get; set; }
            }
    [Preserve(AllMembers = true)]
    public partial class AttDetSet {


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
    public class CotractResponse
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
            public object AContEndDt { get; set; }
            public string AContEndDtCh { get; set; }
            public string AHijriPeriodFrom { get; set; }
            public string PeriodKey { get; set; }
            public string ABranch { get; set; }
            public string AContEndDtFg { get; set; }
            public string AHijriPeriodTo { get; set; }
            public string ACalTp { get; set; }
            public string AComments { get; set; }
            public string AContChk { get; set; }
            public object AContDt { get; set; }
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
            public AttDetSetResult AttDetSet { get; set; }
            public ZnotesSetResult znotesSet { get; set; }
        }
    [Preserve(AllMembers = true)]
    public class CotractRequest
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
        public object AContEndDt { get; set; }
        public string AContEndDtCh { get; set; }
        public string AHijriPeriodFrom { get; set; }
        public string PeriodKey { get; set; }
        public string ABranch { get; set; }
        public string AContEndDtFg { get; set; }
        public string AHijriPeriodTo { get; set; }
        public string ACalTp { get; set; }
        public string AComments { get; set; }
        public string AContChk { get; set; }
        public object AContDt { get; set; }
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
        public AttDetSet[] AttDetSet { get; set; }
        public ZnotesSet[] znotesSet { get; set; }
    }
 
    public class ContractReLeaseApplicationFormModel
    {
        [Preserve(AllMembers = true)]
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
            public string DueDt { get; set; }
            public string Due { get; set; }
            public string Abrzu { get; set; }
            public string Abrzo { get; set; }
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
        public class ListSet
        {
            public List<ContractResult> results { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class AuthServSet
        {
            public List<object> results { get; set; }
        }
        [Preserve(AllMembers = true)]
        public class D
        {
            public Metadata __metadata { get; set; }
            public string CallServ { get; set; }
            public int Accnum { get; set; }
            public int Actcnt { get; set; }
            public string Auditor { get; set; }
            public bool AudObjection { get; set; }
            public bool AudRefund { get; set; }
            public bool AudRefundTrn { get; set; }
            public bool AudRequest { get; set; }
            public bool AudReturn { get; set; }
            public string Bpnum { get; set; }
            public string Branch { get; set; }
            public string Caltype { get; set; }
            public string Client { get; set; }
            public int Cnlcnt { get; set; }
            public int Corrnum { get; set; }
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
            public int Indcorrnum { get; set; }
            public string Interest { get; set; }
            public string IntPortal { get; set; }
            public bool IsBankruptcy { get; set; }
            public string Lang { get; set; }
            public string Name { get; set; }
            public bool NotifLogFlag { get; set; }
            public string NregDtFlg { get; set; }
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
            public int Zfillingoblig { get; set; }
            public string Zregstatus { get; set; }
            public string Zuser { get; set; }
            public ListSet ListSet { get; set; }
            public AuthServSet AuthServSet { get; set; }
        }
    }
}
