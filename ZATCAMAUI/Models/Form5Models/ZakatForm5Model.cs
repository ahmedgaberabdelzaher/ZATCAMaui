using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ZATCAMAUI.Models.Form5Models
{
   
    public partial class ZakatForm5Data
    {
        [DataMember]
        public ZakatForm5DataResult d { get; set; }
    }
   
    public class Metadata
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string uri { get; set; }
        [DataMember]
        public string type { get; set; }
    }
   
   
    public class Result
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        public string Mandt { get; set; }
        [DataMember]
        public string FormGuid { get; set; }
        [DataMember]
        public string DataVersion { get; set; }
        [DataMember]
        public int LineNo { get; set; }
        [DataMember]
        public string RankingOrder { get; set; }
        [DataMember]
        public string ActCd { get; set; }
        [DataMember]
        public string ActDesc { get; set; }
        [DataMember]
        public int LineCnt { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string SubDesc { get; set; }
        [DataMember]
        public string Value { get; set; }
        [DataMember]
        public string ProfitRatio { get; set; }
        [DataMember]
        public string Profit { get; set; }
        [DataMember]
        public string Waers { get; set; }
        [DataMember]
        public string ValueFg { get; set; }
        [DataMember]
        public string ProfitFg { get; set; }
        [DataMember]
        public string Flag { get; set; }
        [DataMember]
        public string MarkDel { get; set; }
        [DataMember]
        public string Delflag { get; set; }
    }
   
    public class GP038Set
    {
        [DataMember]
        public List<Result> results { get; set; }
    }
   
    public class Results2
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [DataMember]
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [DataMember]
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        [DataMember]
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; }
        [DataMember]
        [JsonProperty("activityCode")]
        public string ActCd { get; set; }
        [DataMember]
        [JsonProperty("activityDescription")]
        public string ActDesc { get; set; }
        [DataMember]
        [JsonProperty("lineCount")]
        public int LineCnt { get; set; }
        [DataMember]
        [JsonProperty("description")]
        public string Description { get; set; }
        [DataMember]
        [JsonProperty("subDescription")]
        public string SubDescription { get; set; }
        [DataMember]
        [JsonProperty("value")]
        public string Value { get; set; }
        [DataMember]
        [JsonProperty("profitRatio")]
        public string ProfitRatio { get; set; }
        [DataMember]
        [JsonProperty("profit")]
        public string Profit { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }
        [DataMember]
        [JsonProperty("amountValue")]
        public string ValueFg { get; set; }
        [DataMember]
        [JsonProperty("amountProfit")]
        public string ProfitFg { get; set; }
        [DataMember]
        [JsonProperty("flag")]
        public string Flag { get; set; }
        [DataMember]
        [JsonProperty("markDeletion")]
        public string MarkDel { get; set; }
        [DataMember]
        [JsonProperty("delete")]
        public string Delflag { get; set; }
    }
   
    public class GP037Set
    {
        [DataMember]
        [JsonProperty("GP03_7Set")]
        public List<Results2> results { get; set; }
    }
   
   
    public class Results3
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [DataMember]
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [DataMember]
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        [DataMember]
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; }
        [DataMember]
        [JsonProperty("TIN")]
        public string Tin { get; set; }
        [DataMember]
        [JsonProperty("taxpayerName")]
        public string Name { get; set; }
        [DataMember]
        [JsonProperty("amount")]
        public string Amount { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }
    }
   
    public class SCH200Set
    {
        [DataMember]
        [JsonProperty("profitLossPartnership")]
        public List<Results3> results { get; set; }
    }
   
    public class Results4
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [DataMember]
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [DataMember]
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        [DataMember]
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; }
        [DataMember]
        [JsonProperty("contractNumber")]
        public string ContractNo { get; set; }
        [DataMember]
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [DataMember]
        [JsonProperty("zakatPaid")]
        public string ZakatPaid { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }
    }
   
    public class SCH800Set
    {
        [DataMember]
        [JsonProperty("zakatContractReleasePayments")]
        public List<Results4> results { get; set; }
    }
   
   
    public class Results5
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("minimumValues")]
        public string MinimumFg { get; set; }
        [DataMember]
        [JsonProperty("year")]
        public string YearFg { get; set; }
        [DataMember]
        [JsonProperty("objection")]
        public string ObjFg { get; set; }
        [DataMember]
        [JsonProperty("markDeletion")]
        public string MarkDel { get; set; }
        [DataMember]
        [JsonProperty("outletType")]
        public string OutletTp { get; set; }
        [DataMember]
        [JsonProperty("revenue")]
        public string RevenueFg { get; set; }
        [DataMember]
        [JsonProperty("CRNumber")]
        public string Crnumber { get; set; }
        [DataMember]
        [JsonProperty("expenses")]
        public string ExpensesFg { get; set; }
        [DataMember]
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [DataMember]
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [DataMember]
        [JsonProperty("mainIndustry")]
        public string MainInd { get; set; }
        [DataMember]
        [JsonProperty("netProfit")]
        public string NetPftFg { get; set; }
        [DataMember]
        [JsonProperty("container")]
        public string ContainerFg { get; set; }
        [DataMember]
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        [DataMember]
        [JsonProperty("subIndustry")]
        public string SubInd { get; set; }
        [DataMember]
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; }
        [DataMember]
        [JsonProperty("activityCode")]
        public string ActCd { get; set; }
        [DataMember]
        [JsonProperty("activityDescription")]
        public string ActDesc { get; set; }
        [DataMember]
        [JsonProperty("revenueAmount")]
        public string Revenue { get; set; }
        [DataMember]
        [JsonProperty("expensesAmount")]
        public string Expenses { get; set; }
        [DataMember]
        [JsonProperty("city")]
        public string City { get; set; }
        [DataMember]
        [JsonProperty("netProfitAmount")]
        public string NetPft { get; set; }
        [DataMember]
        [JsonProperty("containerAmount")]
        public string Container { get; set; }
        [DataMember]
        [JsonProperty("lineCount")]
        public int LineCnt { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }
        [DataMember]
        public string IsExpencesApplicable { get; set; }
        [DataMember]
        public bool IsApplicableVisible { get; set; }


    }
   
    public class SCHGP07
    {
        [DataMember]
        [JsonProperty("schedulesGroup7")]
        public List<Results5> results { get; set; }
    }
   
    public class Results6
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("capitalGains")]
        public string Capital { get; set; }
        [DataMember]
        [JsonProperty("governmentContractProfit")]
        public string GovtContractProfFg { get; set; }
        [DataMember]
        [JsonProperty("year")]
        public string YearFg { get; set; }
        [DataMember]
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("markDeletion")]
        public string MarkDel { get; set; }
        [DataMember]
        [JsonProperty("outletType")]
        public string OutletTp { get; set; }
        [DataMember]
        [JsonProperty("civilContractRevenue")]
        public string CivilContrRevFg { get; set; }
        [DataMember]
        [JsonProperty("CRNumber")]
        public string Crnumber { get; set; }
        [DataMember]
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [DataMember]
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [DataMember]
        [JsonProperty("mainIndustry")]
        public string MainInd { get; set; }
        [DataMember]
        [JsonProperty("otherIncome")]
        public string OtherIncomeFg { get; set; }
        [DataMember]
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        [DataMember]
        [JsonProperty("subIndustry")]
        public string SubInd { get; set; }
        [DataMember]
        [JsonProperty("totalProfit")]
        public string TotalProfitFg { get; set; }
        [DataMember]
        [JsonProperty("container")]
        public string ContainerFg { get; set; }
        [DataMember]
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; }
        [DataMember]
        [JsonProperty("activityCode")]
        public string ActCd { get; set; }
        [DataMember]
        [JsonProperty("numberOfLabours")]
        public string NoOfLabourFg { get; set; }
        [DataMember]
        [JsonProperty("activityDescription")]
        public string ActDesc { get; set; }
        [DataMember]
        [JsonProperty("governmentContractCount")]
        public int GovtContCnt { get; set; }
        [DataMember]
        [JsonProperty("governmentContractProfitAmount")]
        public string GovtContractProf { get; set; }
        [DataMember]
        [JsonProperty("civilContractCount")]
        public int CivilContCnt { get; set; }
        [DataMember]
        [JsonProperty("civilContractRevenueAmount")]
        public string CivilContrRev { get; set; }
        [DataMember]
        [JsonProperty("otherIncomeContract")]
        public int OthIncCnt { get; set; }
        [DataMember]
        [JsonProperty("otherIncomeAmount")]
        public string OtherIncome { get; set; }
        [DataMember]
        [JsonProperty("totalProfitAmount")]
        public string TotalProfit { get; set; }
        [DataMember]
        [JsonProperty("laboursNumber")]
        public string NoOfLabours { get; set; }
        [DataMember]
        [JsonProperty("containerAmount")]
        public string Container { get; set; }
        [DataMember]
        [JsonProperty("lineCount")]
        public int LineCnt { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }
        [DataMember]
        public string IsGovenmentApplicable { get; set; }
        [DataMember]
        public bool IsGovenmentApplicableVisible { get; set; }
        [DataMember]
        public string IsCivilProfitApplicable { get; set; }
        [DataMember]
        public bool IsCivilProfitApplicableVisible { get; set; }
        [DataMember]
        public string IsOtherProfitApplicable { get; set; }
        [DataMember]
        public bool IsOtherProfitApplicableVisible { get; set; }





    }
   
    public class SCHGP06
    {
        [DataMember]
        [JsonProperty("schedulesGroup06")]
        public List<Results6> results { get; set; }
    }
   
    public class Results7
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("year")]
        public string YearFg { get; set; }
        [DataMember]
        [JsonProperty("fundingTotal")]
        public string FundingTotFg { get; set; }
        [DataMember]
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("markDeletion")]
        public string MarkDel { get; set; }
        [DataMember]
        [JsonProperty("outletType")]
        public string OutletTp { get; set; }
        [DataMember]
        [JsonProperty("capitalstructure")]
        public string CapitalStrFg { get; set; }
        [DataMember]
        [JsonProperty("CRNumber")]
        public string Crnumber { get; set; }
        [DataMember]
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [DataMember]
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [DataMember]
        [JsonProperty("mainIndustry")]
        public string MainInd { get; set; }
        [DataMember]
        [JsonProperty("netCapital")]
        public string NetCapitalFg { get; set; }
        [DataMember]
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        [DataMember]
        [JsonProperty("sales")]
        public string SalesFg { get; set; }
        [DataMember]
        [JsonProperty("subIndustry")]
        public string SubInd { get; set; }
        [DataMember]
        [JsonProperty("profit")]
        public string ProfitFg { get; set; }
        [DataMember]
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; }
        [DataMember]
        [JsonProperty("activityCode")]
        public string ActCd { get; set; }
        [DataMember]
        [JsonProperty("statutoryProfit")]
        public string StatutoryPftFg { get; set; }
        [DataMember]
        [JsonProperty("activityDescription")]
        public string ActDesc { get; set; }
        [DataMember]
        [JsonProperty("container")]
        public string ContainerFg { get; set; }
        [DataMember]
        [JsonProperty("fundingTotalAmount")]
        public string FundingTot { get; set; }
        [DataMember]
        [JsonProperty("capitalStructureAmount")]
        public string CapitalStr { get; set; }
        [DataMember]
        [JsonProperty("netCapitalAmount")]
        public string NetCapital { get; set; }
        [DataMember]
        [JsonProperty("salesAmount")]
        public string Sales { get; set; }
        [DataMember]
        [JsonProperty("profitAmount")]
        public string Profit { get; set; }
        [DataMember]
        [JsonProperty("statutoryProfitAmount")]
        public string StatutoryPft { get; set; }
        [DataMember]
        [JsonProperty("containerAmount")]
        public string Container { get; set; }
        [DataMember]
        [JsonProperty("lineCount")]
        public int LineCnt { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }

    }
   
    public class SCHGP05
    {
        [DataMember]
        [JsonProperty("schedulesGroup05")]
        public List<Results7> results { get; set; }
    }
   
   
    public class Results8
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("year")]
        public string YearFg { get; set; }
        [DataMember]
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("markDeletion")]
        public string MarkDel { get; set; }
        [DataMember]
        [JsonProperty("outletType")]
        public string OutletTp { get; set; }
        [DataMember]
        [JsonProperty("revenue")]
        public string RevenueFg { get; set; }
        [DataMember]
        [JsonProperty("CRNumber")]
        public string Crnumber { get; set; }
        [DataMember]
        [JsonProperty("expenses")]
        public string ExpensesFg { get; set; }
        [DataMember]
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [DataMember]
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [DataMember]
        [JsonProperty("mainIndustry")]
        public string MainInd { get; set; }
        [DataMember]
        [JsonProperty("netProfit")]
        public string NetPftFg { get; set; }
        [DataMember]
        [JsonProperty("container")]
        public string ContainerFg { get; set; }
        [DataMember]
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        [DataMember]
        [JsonProperty("subIndustry")]
        public string SubInd { get; set; }
        [DataMember]
        [JsonProperty("numberOfLabours")]
        public string NoOfLabourFg { get; set; }
        [DataMember]
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; }
        [DataMember]
        [JsonProperty("activityCode")]
        public string ActCd { get; set; }
        [DataMember]
        [JsonProperty("activityDescription")]
        public string ActDesc { get; set; }
        [DataMember]
        [JsonProperty("revenueAmount")]
        public string Revenue { get; set; }
        [DataMember]
        [JsonProperty("expensesAmount")]
        public string Expenses { get; set; }
        [DataMember]
        [JsonProperty("netProfitAmount")]
        public string NetProfit { get; set; }
        [DataMember]
        [JsonProperty("laboursNumber")]
        public string NoOfLabour { get; set; }
        [DataMember]
        [JsonProperty("containerAmount")]
        public string Container { get; set; }
        [DataMember]
        [JsonProperty("lineCount")]
        public int LineCnt { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }

        [DataMember]
        public string IsExpensesApplicable { get; set; }
        [DataMember]
        public bool IsApplicableVisible { get; set; }


    }
   
    public class SCHGP04
    {
        [DataMember]
        [JsonProperty("schedulesGroup4")]
        public List<Results8> results { get; set; }
    }
   
   
    public class Results9
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("generalTradeCount")]
        public int GenTradeICnt { get; set; }
        [DataMember]
        [JsonProperty("countGeneralTrade")]
        public string GenTradeIFg { get; set; }
        [DataMember]
        [JsonProperty("year")]
        public string YearFg { get; set; }
        [DataMember]
        [JsonProperty("livelihoods")]
        public string LivelihoodsIFg { get; set; }
        [DataMember]
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("generalTradeAmount")]
        public string GenTradeI { get; set; }
        [DataMember]
        [JsonProperty("liveStockAndAnimals")]
        public string LiveStkAnimalsIFg { get; set; }
        [DataMember]
        [JsonProperty("markDeletion")]
        public string MarkDel { get; set; }
        [DataMember]
        [JsonProperty("arzaqCount")]
        public int ArzaqICnt { get; set; }
        [DataMember]
        [JsonProperty("outletType")]
        public string OutletTp { get; set; }
        [DataMember]
        [JsonProperty("livehoods")]
        public string LivelihoodsI { get; set; }
        [DataMember]
        [JsonProperty("supplyContracts")]
        public string SuppConFg { get; set; }
        [DataMember]
        [JsonProperty("CRNumber")]
        public string Crnumber { get; set; }
        [DataMember]
        [JsonProperty("liveStockAndAnimalsCount")]
        public int LiveStkICnt { get; set; }
        [DataMember]
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [DataMember]
        [JsonProperty("liveStockAndAnimalsAmount")]
        public string LiveStkAnimalsI { get; set; }
        [DataMember]
        [JsonProperty("sales")]
        public string SalesFg { get; set; }
        [DataMember]
        [JsonProperty("capital")]
        public string CapitalFg { get; set; }
        [DataMember]
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [DataMember]
        [JsonProperty("mainIndustry")]
        public string MainInd { get; set; }
        [DataMember]
        [JsonProperty("container")]
        public string ContainerFg { get; set; }
        [DataMember]
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        [DataMember]
        [JsonProperty("subIndustry")]
        public string SubInd { get; set; }
        [DataMember]
        [JsonProperty("generalTrade")]
        public string GenTradeFg { get; set; }
        [DataMember]
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; }
        [DataMember]
        [JsonProperty("activityCode")]
        public string ActCd { get; set; }
        //[DataMember]
        //[JsonProperty("")]
        //public string LivelihoodsFg { get; set; }
        [DataMember]
        [JsonProperty("activityDescription")]
        public string ActDesc { get; set; }
        //[DataMember]
        //[JsonProperty("")]
        //public string LiveStkAnimalsFg { get; set; }
        [DataMember]
        [JsonProperty("externalProcrument")]
        public string ExtnProcFg { get; set; }
        [DataMember]
        [JsonProperty("lineCount")]
        public int LineCnt { get; set; }
        [DataMember]
        [JsonProperty("internalProcurement")]
        public string IntnProcFg { get; set; }
        [DataMember]
        [JsonProperty("supplyContractsAmount")]
        public string SuppCon { get; set; }
        [DataMember]
        [JsonProperty("profitRatioAmount")]
        public string ProfitRatio { get; set; }
        [DataMember]
        [JsonProperty("total")]
        public string TotalFg { get; set; }
        [DataMember]
        [JsonProperty("profitRatio")]
        public string ProfitRatioFg { get; set; }
        [DataMember]
        [JsonProperty("salesAmount")]
        public string Sales { get; set; }
        [DataMember]
        [JsonProperty("saleProfitRatioAmount")]
        public string SalePrfRatio { get; set; }
        [DataMember]
        [JsonProperty("saleProfitRatio")]
        public string SalePrfRatioFg { get; set; }
        [DataMember]
        [JsonProperty("capitalAmount")]
        public string Capital { get; set; }
        [DataMember]
        [JsonProperty("containerAmount")]
        public string Container { get; set; }
        //[DataMember]
        //[JsonProperty("")]
        //public int GenTradeCnt { get; set; }
        //[DataMember]
        //[JsonProperty("")]
        //public string GenTrade { get; set; }
        //[DataMember]
        //[JsonProperty("")]
        //public int ArzaqCnt { get; set; }
        [DataMember]
        [JsonProperty("livelihoodsAmount")]
        public string Livelihoods { get; set; }
        [DataMember]
        [JsonProperty("livestockCount")]
        public int LiveStkCnt { get; set; }
        [DataMember]
        [JsonProperty("livestockAnimals")]
        public string LiveStkAnimals { get; set; }
        [DataMember]
        [JsonProperty("externalProcurement")]
        public string ExtnProc { get; set; }
        [DataMember]
        [JsonProperty("internalProcurementCount")]
        public int IntnProcCnt { get; set; }
        [DataMember]
        [JsonProperty("internalProcurementAmount")]
        public string IntnProc { get; set; }
        [DataMember]
        [JsonProperty("totalAmount")]
        public string Total { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }

        [DataMember]
        public string IsApplicable { get; set; }
        [DataMember]
        public bool IsApplicableVisible { get; set; }

        [DataMember]
        public string IsImport { get; set; }
        [DataMember]
        public string IsProcurement { get; set; }


        [DataMember]
        public string Imp_IsGeneralApplicable { get; set; }
        [DataMember]
        public bool Imp_IsGeneralApplicableVisible { get; set; }

        [DataMember]
        public string Imp_IsLiveLihoodsApplicable { get; set; }
        [DataMember]
        public bool Imp_IsLiveLihoodsApplicableVisible { get; set; }

        [DataMember]
        public string Imp_IsLivestockApplicable { get; set; }
        [DataMember]
        public bool Imp_IsLivestockApplicableVisible { get; set; }



        [DataMember]
        public string Pro_IsGeneralApplicable { get; set; }
        [DataMember]
        public bool Pro_IsGeneralApplicableVisible { get; set; }

        [DataMember]
        public string Pro_IsLiveLihoodsApplicable { get; set; }
        [DataMember]
        public bool Pro_IsLiveLihoodsApplicableVisible { get; set; }

        [DataMember]
        public string Pro_IsLivestockApplicable { get; set; }
        [DataMember]
        public bool Pro_IsLivestockApplicableVisible { get; set; }



        [DataMember]
        public bool IsImportVisible { get; set; }
        [DataMember]
        public bool IsProcurementVisible { get; set; }




    }
   
    public class SCHGP03
    {
        [DataMember]
        [JsonProperty("schedulesGroup03")]
        public List<Results9> results { get; set; }
    }
   
   
    public class Results10
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [DataMember]
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [DataMember]
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        [DataMember]
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; }
        [DataMember]
        [JsonProperty("periodKey")]
        public string Persl { get; set; }
        [DataMember]
        [JsonProperty("periodDescription")]
        public string PerslDesc { get; set; }
        [DataMember]
        [JsonProperty("capitalMCI")]
        public string CapitalMci { get; set; }
        [DataMember]
        [JsonProperty("capital")]
        public string Capital { get; set; }
        [DataMember]
        [JsonProperty("capitalAccount")]
        public string CapitalAcc { get; set; }
        [DataMember]
        [JsonProperty("capitalGain")]
        public string CapitalGain { get; set; }
        [DataMember]
        [JsonProperty("maximumCapital")]
        public string MaxCapital { get; set; }
    }
   
    public class SCHGP3S2Set
    {
        [DataMember]
        [JsonProperty("capitalGainCalculations")]
        public List<Results10> results { get; set; }
    }
   
 
   
    public class Results11
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [DataMember]
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [DataMember]
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        [DataMember]
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; }
        [DataMember]
        [JsonProperty("periodKey")]
        public string Persl { get; set; }
        [DataMember]
        [JsonProperty("periodDescription")]
        public string PerslDesc { get; set; }
        [DataMember]
        [JsonProperty("importTotal")]
        public string ImportTot { get; set; }
        [DataMember]
        [JsonProperty("import3rdParty")]
        public string Import3rdParty { get; set; }
        [DataMember]
        [JsonProperty("higherImport")]
        public string HigherImport { get; set; }
        [DataMember]
        [JsonProperty("internalProcurement")]
        public string IntnProc { get; set; }
        [DataMember]
        [JsonProperty("totalImportInternalProcurement")]
        public string TotImpIntn { get; set; }
        [DataMember]
        [JsonProperty("capitalGain")]
        public string CapitalGain { get; set; }
    }
   
    public class SCHGP3S1Set
    {
        [DataMember]
        [JsonProperty("totalImportsAndInternalProcurements")]
        public List<Results11> results { get; set; }
    }
   

   
    public class Results12
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [DataMember]
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [DataMember]
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        [DataMember]
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; }
        [DataMember]
        [JsonProperty("activityCode")]
        public string ActCd { get; set; }
        [DataMember]
        [JsonProperty("activityDescription")]
        public string ActDesc { get; set; }
        [DataMember]
        [JsonProperty("lineCount")]
        public int LineCnt { get; set; }
        [DataMember]
        [JsonProperty("description")]
        public string Description { get; set; }
        [DataMember]
        [JsonProperty("subDescription")]
        public string SubDesc { get; set; }
        [DataMember]
        [JsonProperty("value")]
        public string Value { get; set; }
        [DataMember]
        [JsonProperty("profitRatio")]
        public string ProfitRatio { get; set; }
        [DataMember]
        [JsonProperty("profit")]
        public string Profit { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }
        [DataMember]
        [JsonProperty("amountValue")]
        public string ValueFg { get; set; }
        [DataMember]
        [JsonProperty("amountProfit")]
        public string ProfitFg { get; set; }
        [DataMember]
        [JsonProperty("flag")]
        public string Flag { get; set; }
        [DataMember]
        [JsonProperty("markDeletion")]
        public string MarkDel { get; set; }
        [DataMember]
        [JsonProperty("delete")]
        public string Delflag { get; set; }
    }
   
    public class GP036Set
    {
        [DataMember]
        [JsonProperty("GP03_6Set")]
        public List<Results12> results { get; set; }
    }
   
    public class AttDetSet
    {
        [DataMember]
        [JsonProperty("attachments")]
        public List<object> results { get; set; }
    }
   

   
    public class Results13
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("markDeletion")]
        public string MarkDel { get; set; }
        [DataMember]
        [JsonProperty("delete")]
        public string Delflag { get; set; }
        [DataMember]
        [JsonProperty("loanDateType")]
        public string LoanDtTp { get; set; }
        [DataMember]
        [JsonProperty("loanDate")]
        public object LoanDt { get; set; }
        [DataMember]
        [JsonProperty("approvalDateType")]
        public string ApprovalDtTp { get; set; }
        [DataMember]
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("approvalDate")]
        public object ApprovalDt { get; set; }
        [DataMember]
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [DataMember]
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [DataMember]
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        [DataMember]
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; }
        [DataMember]
        [JsonProperty("activityCode")]
        public string ActCd { get; set; }
        [DataMember]
        [JsonProperty("activityDescription")]
        public string ActDesc { get; set; }
        [DataMember]
        [JsonProperty("lineCount")]
        public int LineCnt { get; set; }
        [DataMember]
        [JsonProperty("description")]
        public string Description { get; set; }
        [DataMember]
        [JsonProperty("loanValue")]
        public string LoanVal { get; set; }
        [DataMember]
        [JsonProperty("yearsNumber")]
        public string NumberOfYr { get; set; }
        [DataMember]
        [JsonProperty("increaseCapital")]
        public string IncrCapital { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }
    }
   
    public class SUBSCHCAPITALSet
    {
        [DataMember]
        [JsonProperty("subSchedulesCapital")]
        public List<Results13> results { get; set; }
    }

   
    public class Results14
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        public string MarkDel { get; set; }
        [DataMember]
        public string Delflag { get; set; }
        [DataMember]
        public string FormGuid { get; set; }
        [DataMember]
        public string Flag { get; set; }
        [DataMember]
        public string DataVersion { get; set; }
        [DataMember]
        public int LineNo { get; set; }
        [DataMember]
        public string RankingOrder { get; set; }
        [DataMember]
        public string ActCd { get; set; }
        [DataMember]
        public string ActDesc { get; set; }
        [DataMember]
        public int LineCnt { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string SubDesc { get; set; }
        [DataMember]
        public string Value { get; set; }
        [DataMember]
        public string ProfitRatio { get; set; }
        [DataMember]
        public string Profit { get; set; }
        [DataMember]
        public string Waers { get; set; }
    }
   
    public class GP034Set
    {
        [DataMember]
        public List<Results14> results { get; set; }
    }
   
   
    public class Result15
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("markDeletion")]
        public string MarkDel { get; set; }
        [DataMember]
        [JsonProperty("delete")]
        public string Delflag { get; set; }
        [DataMember]
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [DataMember]
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [DataMember]
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        [DataMember]
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; }
        [DataMember]
        [JsonProperty("activityCode")]
        public string ActCd { get; set; }
        [DataMember]
        [JsonProperty("activityDescription")]
        public string ActDesc { get; set; }
        [DataMember]
        [JsonProperty("lineCount")]
        public int LineCnt { get; set; }
        [DataMember]
        [JsonProperty("description")]
        public string Description { get; set; }
        [DataMember]
        [JsonProperty("value")]
        public string Value { get; set; }
        [DataMember]
        [JsonProperty("profitRatio")]
        public string ProfitRatio { get; set; }
        [DataMember]
        [JsonProperty("profit")]
        public string Profit { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }
    }
   
    public class GP035Set
    {
        [DataMember]
        [JsonProperty("GP03_5Set")]
        public List<Result15> results { get; set; }
    }
   
   
    public class Result16
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("markDeletion")]
        public string MarkDel { get; set; }
        [DataMember]
        [JsonProperty("delete")]
        public string Delflag { get; set; }
        [DataMember]
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [DataMember]
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [DataMember]
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        [DataMember]
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; }
        [DataMember]
        [JsonProperty("activityCode")]
        public string ActCd { get; set; }
        [DataMember]
        [JsonProperty("activityDescription")]
        public string ActDesc { get; set; }
        [DataMember]
        [JsonProperty("lineCount")]
        public int LineCnt { get; set; }
        [DataMember]
        [JsonProperty("description")]
        public string Description { get; set; }
        [DataMember]
        [JsonProperty("contractAmount")]
        public string ContractVal { get; set; }
        [DataMember]
        [JsonProperty("stipendContractAmount")]
        public string StipendContr { get; set; }
        [DataMember]
        [JsonProperty("profitsOfContractAmount")]
        public string PrfOfConrtact { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }
    }
   
    public class GP063Set
    {
        [DataMember]
        public List<Result16> results { get; set; }
    }
   
   
    public class Result17
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        public string MarkDel { get; set; }
        [DataMember]
        public string Delflag { get; set; }
        [DataMember]
        public string Flag { get; set; }
        [DataMember]
        public string FormGuid { get; set; }
        [DataMember]
        public string DataVersion { get; set; }
        [DataMember]
        public int LineNo { get; set; }
        [DataMember]
        public string RankingOrder { get; set; }
        [DataMember]
        public string ActCd { get; set; }
        [DataMember]
        public string ActDesc { get; set; }
        [DataMember]
        public int LineCnt { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string SubDesc { get; set; }
        [DataMember]
        public string Value { get; set; }
        [DataMember]
        public string ProfitRatio { get; set; }
        [DataMember]
        public string Profit { get; set; }
        [DataMember]
        public string Waers { get; set; }
    }
   
    public class GP032Set
    {
        [DataMember]
        public List<Result17> results { get; set; }
    }

    public class Result18
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("markDeletion")]
        public string MarkDel { get; set; }
        [DataMember]
        [JsonProperty("recordNumber")]
        public string RecordNo { get; set; }
        [DataMember]
        [JsonProperty("contractValue")]
        public string ContractValFg { get; set; }
        [DataMember]
        [JsonProperty("governmentCode")]
        public string GovCode { get; set; }
        [DataMember]
        [JsonProperty("delete")]
        public string Delflag { get; set; }
        [DataMember]
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [DataMember]
        [JsonProperty("stipendContract")]
        public string StipendContrFg { get; set; }
        [DataMember]
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [DataMember]
        [JsonProperty("profitsOfConrtact")]
        public string PrfOfConrtactFg { get; set; }
        [DataMember]
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        [DataMember]
        [JsonProperty("flag")]
        public string Flag { get; set; }
        [DataMember]
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; }
        [DataMember]
        [JsonProperty("activityCode")]
        public string ActCd { get; set; }
        [DataMember]
        [JsonProperty("activityDescription")]
        public string ActDesc { get; set; }
        [DataMember]
        [JsonProperty("lineCount")]
        public int LineCnt { get; set; }
        [DataMember]
        [JsonProperty("description")]
        public string Description { get; set; }
        [DataMember]
        [JsonProperty("contractAmount")]
        public string ContractVal { get; set; }
        [DataMember]
        [JsonProperty("stipendContractAmount")]
        public string StipendContr { get; set; }
        [DataMember]
        [JsonProperty("profitsOfContractAmount")]
        public string PrfOfConrtact { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }
    }
   
    public class GP061Set
    {
        [DataMember]
        public List<Result18> results { get; set; }
    }
   
   
    public class Result19
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [DataMember]
        [JsonProperty("delete")]
        public string Delflag { get; set; }
        [DataMember]
        [JsonProperty("TINCRNumberIn")]
        public string TincrnoInd { get; set; }
        [DataMember]
        [JsonProperty("recordNumber")]
        public string RecordNo { get; set; }
        [DataMember]
        [JsonProperty("TIN")]
        public string Tin { get; set; }
        [DataMember]
        [JsonProperty("markDeletion")]
        public string MarkDel { get; set; }
        [DataMember]
        [JsonProperty("contractingName")]
        public string ContractingName { get; set; }
        [DataMember]
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [DataMember]
        [JsonProperty("MOCContractor")]
        public string MocContractor { get; set; }
        [DataMember]
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        [DataMember]
        [JsonProperty("contractValue")]
        public string ContractValFg { get; set; }
        [DataMember]
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; }
        [DataMember]
        [JsonProperty("stipendContract")]
        public string StipendContrFg { get; set; }
        [DataMember]
        [JsonProperty("activityCode")]
        public string ActCd { get; set; }
        [DataMember]
        [JsonProperty("profitsOfConrtact")]
        public string PrfOfConrtactFg { get; set; }
        [DataMember]
        [JsonProperty("activityDescription")]
        public string ActDesc { get; set; }
        [DataMember]
        [JsonProperty("flag")]
        public string Flag { get; set; }
        [DataMember]
        [JsonProperty("lineCount")]
        public int LineCnt { get; set; }
        [DataMember]
        [JsonProperty("description")]
        public string Description { get; set; }
        [DataMember]
        [JsonProperty("contractAmount")]
        public string ContractVal { get; set; }
        [DataMember]
        [JsonProperty("stipendContractAmount")]
        public string StipendContr { get; set; }
        [DataMember]
        [JsonProperty("profitsOfContractAmount")]
        public string PrfOfConrtact { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }
    }
   
    public class GP062Set
    {
        [DataMember]
        [JsonProperty("GP06_2Set")]
        public List<Result19> results { get; set; }
    }
   

    public class Result20
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        public string MarkDel { get; set; }
        [DataMember]
        public string Delflag { get; set; }
        [DataMember]
        public string Flag { get; set; }
        [DataMember]
        public string FormGuid { get; set; }
        [DataMember]
        public string DataVersion { get; set; }
        [DataMember]
        public int LineNo { get; set; }
        [DataMember]
        public string RankingOrder { get; set; }
        [DataMember]
        public string ActCd { get; set; }
        [DataMember]
        public string ActDesc { get; set; }
        [DataMember]
        public int LineCnt { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string SubDescription { get; set; }
        [DataMember]
        public string Value { get; set; }
        [DataMember]
        public string ProfitRatio { get; set; }
        [DataMember]
        public string Profit { get; set; }
        [DataMember]
        public string Waers { get; set; }
    }
   
    public class GP033Set
    {
        [DataMember]
        public List<Result20> results { get; set; }
    }

    public class Result21
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("contractDateType")]
        public string ContractDtTp { get; set; }
        [DataMember]
        //[JsonProperty("")]
        public string OriginalValFg { get; set; }
        [DataMember]
        [JsonProperty("contractDurationPlan")]
        public string ContractDur { get; set; }
        [DataMember]
        [JsonProperty("adjustContract")]
        public string AdjustContFg { get; set; }
        [DataMember]
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("previousYearWork")]
        public string PreyrWrkFg { get; set; }
        [DataMember]
        [JsonProperty("currentYearWork")]
        public string CurryrWrkFg { get; set; }
        [DataMember]
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [DataMember]
        [JsonProperty("remainingWork")]
        public string RemnWrkFg { get; set; }
        [DataMember]
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [DataMember]
        [JsonProperty("contractingParty")]
        public string ContractPartyInd { get; set; }
        [DataMember]
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        [DataMember]
        [JsonProperty("contractingSubject")]
        public string ContractSubInd { get; set; }
        [DataMember]
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; }
        [DataMember]
        [JsonProperty("contractDate")]
        public string ContractDtInd { get; set; }
        [DataMember]
        [JsonProperty("contractParty")]
        public string ContractParty { get; set; }
        [DataMember]
        [JsonProperty("contractDuration")]
        public string ContractDurInd { get; set; }
        [DataMember]
        [JsonProperty("contractSubject")]
        public string ContractSub { get; set; }
        [DataMember]
        [JsonProperty("originalValue")]
        public string OriginalValInd { get; set; }
        [DataMember]
        [JsonProperty("contractDateValue")]
        public object ContractDt { get; set; }
        [DataMember]
        [JsonProperty("originalAmount")]
        public string OriginalVal { get; set; }
        [DataMember]
        [JsonProperty("adjustContractAmount")]
        public string AdjustCont { get; set; }
        [DataMember]
        [JsonProperty("previousYearWorkAmount")]
        public string PreyrWrk { get; set; }
        [DataMember]
        [JsonProperty("currentYearWorkAmount")]
        public string CurryrWrk { get; set; }
        [DataMember]
        [JsonProperty("remainWork")]
        public string RemnWrk { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }
    }
   
    public class MAINACTIVITYSet
    {
        [DataMember]
        [JsonProperty("mainActivities")]
        public List<Result21> results { get; set; }
    }
   
    public class LONGTEXTSet
    {
        [DataMember]
        public List<object> results { get; set; }
    }
   
   
    public class Result22
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("year")]
        public string YearFg { get; set; }
        [DataMember]
        [JsonProperty("minimumValues")]
        public string MinimumFg { get; set; }
        [DataMember]
        [JsonProperty("interiorPurchases")]
        public string IntrPurchaseFg { get; set; }
        [DataMember]
        [JsonProperty("objection")]
        public string ObjFg { get; set; }
        [DataMember]
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("delete")]
        public string MarkDel { get; set; }
        [DataMember]
        [JsonProperty("outletType")]
        public string OutletTp { get; set; }
        [DataMember]
        [JsonProperty("shareCapital")]
        public string ShareCapFg { get; set; }
        [DataMember]
        [JsonProperty("CRNumber")]
        public string Crnumber { get; set; }
        [DataMember]
        [JsonProperty("foreignPurchases")]
        public string ForgPurchaseFg { get; set; }
        [DataMember]
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [DataMember]
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [DataMember]
        [JsonProperty("mainIndustry")]
        public string MainInd { get; set; }
        [DataMember]
        [JsonProperty("sales")]
        public string SalesFg { get; set; }
        [DataMember]
        [JsonProperty("container")]
        public string ContainerFg { get; set; }
        [DataMember]
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        [DataMember]
        [JsonProperty("subIndustry")]
        public string SubInd { get; set; }
        [DataMember]
        [JsonProperty("percentageSalesGain")]
        public string PerSalesGainFg { get; set; }
        [DataMember]
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; }
        [DataMember]
        [JsonProperty("activityCode")]
        public string ActCd { get; set; }
        [DataMember]
        [JsonProperty("activityDescription")]
        public string ActDesc { get; set; }
        [DataMember]
        [JsonProperty("shareCapitalAmount")]
        public string ShareCap { get; set; }
        [DataMember]
        [JsonProperty("interiorPurchasesAmount")]
        public string IntrPurchase { get; set; }
        [DataMember]
        [JsonProperty("foreignPurchasesAmount")]
        public string ForgPurchase { get; set; }
        [DataMember]
        [JsonProperty("salesAmount")]
        public string Sales { get; set; }
        [DataMember]
        [JsonProperty("percentageSalesGainAmount")]
        public string PerSalesGain { get; set; }
        [DataMember]
        [JsonProperty("containerAmount")]
        public string Container { get; set; }
        [DataMember]
        [JsonProperty("lineCount")]
        public int LineCnt { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }
    }
   
    public class SCHGP12
    {
        [DataMember]
        [JsonProperty("schedulesGroup12")]
        public List<Result22> results { get; set; }
    }
   
   
    public class Result23
    {
        [DataMember]

        public Metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("year")]
        public string YearFg { get; set; }
        [DataMember]
        [JsonProperty("minimumValues")]
        public string MinimumFg { get; set; }
        [DataMember]
        [JsonProperty("interiorPurchases")]
        public string IntrPurchaseFg { get; set; }
        [DataMember]
        [JsonProperty("objection")]
        public string ObjFg { get; set; }
        [DataMember]
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("delete")]
        public string MarkDel { get; set; }
        [DataMember]
        [JsonProperty("outletType")]
        public string OutletTp { get; set; }
        [DataMember]
        [JsonProperty("shareCapital")]
        public string ShareCapFg { get; set; }
        [DataMember]
        [JsonProperty("CRNumber")]
        public string Crnumber { get; set; }
        [DataMember]
        [JsonProperty("foreignPurchases")]
        public string ForgPurchaseFg { get; set; }
        [DataMember]
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [DataMember]
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [DataMember]
        [JsonProperty("mainIndustry")]
        public string MainInd { get; set; }
        [DataMember]
        [JsonProperty("sales")]
        public string SalesFg { get; set; }
        [DataMember]
        [JsonProperty("container")]
        public string ContainerFg { get; set; }
        [DataMember]
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        [DataMember]
        [JsonProperty("subIndustry")]
        public string SubInd { get; set; }
        [DataMember]
        [JsonProperty("percentageSalesGain")]
        public string PerSalesGainFg { get; set; }
        [DataMember]
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; }
        [DataMember]
        [JsonProperty("activityCode")]
        public string ActCd { get; set; }
        [DataMember]
        [JsonProperty("activityDescription")]
        public string ActDesc { get; set; }
        [DataMember]
        [JsonProperty("shareCapitalAmount")]
        public string ShareCap { get; set; }
        [DataMember]
        [JsonProperty("interiorPurchasesAmount")]
        public string IntrPurchase { get; set; }
        [DataMember]
        [JsonProperty("foreignPurchasesAmount")]
        public string ForgPurchase { get; set; }
        [DataMember]
        [JsonProperty("salesAmount")]
        public string Sales { get; set; }
        [DataMember]
        [JsonProperty("percentageSalesGainAmount")]
        public string PerSalesGain { get; set; }
        [DataMember]
        [JsonProperty("containerAmount")]
        public string Container { get; set; }
        [DataMember]
        [JsonProperty("lineCount")]
        public int LineCnt { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }
    }
   
    public class SCHGP11
    {
        [DataMember]
        [JsonProperty("schedulesGroup11")]
        public List<Result23> results { get; set; }
    }
   
   
    public class Result24
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("capital")]
        public string CapitalFg { get; set; }
        [DataMember]
        [JsonProperty("year")]
        public string YearFg { get; set; }
        [DataMember]
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("markDeletion")]
        public string MarkDel { get; set; }
        [DataMember]
        [JsonProperty("outletType")]
        public string OutletTp { get; set; }
        [DataMember]
        [JsonProperty("CRNumber")]
        public string Crnumber { get; set; }
        [DataMember]
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [DataMember]
        [JsonProperty("increaseInCaptial")]
        public string IncrInCapFg { get; set; }
        [DataMember]
        [JsonProperty("capitalRate")]
        public string CapitalRateFg { get; set; }
        [DataMember]
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [DataMember]
        [JsonProperty("mainIndustry")]
        public string MainInd { get; set; }
        [DataMember]
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        [DataMember]
        [JsonProperty("profits")]
        public string ProfitsFg { get; set; }
        [DataMember]
        [JsonProperty("subIndustry")]
        public string SubInd { get; set; }
        [DataMember]
        [JsonProperty("container")]
        public string ContainerFg { get; set; }
        [DataMember]
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; }
        [DataMember]
        [JsonProperty("activityCode")]
        public string ActCd { get; set; }
        [DataMember]
        [JsonProperty("activityDescription")]
        public string ActDesc { get; set; }
        [DataMember]
        [JsonProperty("capitalAmount")]
        public string Capital { get; set; }
        [DataMember]
        [JsonProperty("increaseInCapitalAmount")]
        public string IncrInCap { get; set; }
        [DataMember]
        [JsonProperty("capitalRateAmount")]
        public string CapitalRate { get; set; }
        [DataMember]
        [JsonProperty("profitsAmount")]
        public string Profits { get; set; }
        [DataMember]
        [JsonProperty("containerAmount")]
        public string Container { get; set; }
        [DataMember]
        [JsonProperty("lineCount")]
        public int LineCnt { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }
        [DataMember]
        public string IsIncCapApplicable { get; set; }
        [DataMember]
        public bool IsApplicableVisible { get; set; }


    }
   
    public class SCHGP10
    {
        [DataMember]
        [JsonProperty("schedulesGroup10")]
        public List<Result24> results { get; set; }
    }
   
   
    public class Result25
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("year")]
        public string YearFg { get; set; }
        [DataMember]
        [JsonProperty("markDeletion")]
        public string MarkDel { get; set; }
        [DataMember]
        [JsonProperty("outletType")]
        public string OutletTp { get; set; }
        [DataMember]
        [JsonProperty("revenue")]
        public string RevenueFg { get; set; }
        [DataMember]
        [JsonProperty("CRNumber")]
        public string Crnumber { get; set; }
        [DataMember]
        [JsonProperty("expenses")]
        public string ExpensesFg { get; set; }
        [DataMember]
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [DataMember]
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [DataMember]
        [JsonProperty("mainIndustry")]
        public string MainInd { get; set; }
        [DataMember]
        [JsonProperty("netProfit")]
        public string NetPftFg { get; set; }
        [DataMember]
        [JsonProperty("container")]
        public string ContainerFg { get; set; }
        [DataMember]
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        [DataMember]
        [JsonProperty("subIndustry")]
        public string SubInd { get; set; }
        [DataMember]
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; }
        [DataMember]
        [JsonProperty("subsidyValue")]
        public string SubsidyValFg { get; set; }
        [DataMember]
        [JsonProperty("activityCode")]
        public string ActCd { get; set; }
        [DataMember]
        [JsonProperty("activityDescription")]
        public string ActDesc { get; set; }
        [DataMember]
        [JsonProperty("subsidyAmount")]
        public string SubsidyVal { get; set; }
        [DataMember]
        [JsonProperty("revenueAmount")]
        public string Revenue { get; set; }
        [DataMember]
        [JsonProperty("expensesAmount")]
        public string Expenses { get; set; }
        [DataMember]
        [JsonProperty("netProfitAmount")]
        public string NetPft { get; set; }
        [DataMember]
        [JsonProperty("containerAmount")]
        public string Container { get; set; }
        [DataMember]
        [JsonProperty("lineCount")]
        public int LineCnt { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }
        [DataMember]
        public string IsExpApplicable { get; set; }
        [DataMember]
        public bool IsApplicableVisible { get; set; }


    }
   
    public class SCHGP09
    {
        [DataMember]
        [JsonProperty("schedulesGroup09")]
        public List<Result25> results { get; set; }
    }
   
   
    public class Result26
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("year")]
        public string YearFg { get; set; }
        [DataMember]
        [JsonProperty("markDeletion")]
        public string MarkDel { get; set; }
        [DataMember]
        [JsonProperty("outletType")]
        public string OutletTp { get; set; }
        [DataMember]
        [JsonProperty("roomRate")]
        public string RoomRateFg { get; set; }
        [DataMember]
        [JsonProperty("addRoomRate")]
        public string AddRoomRateFg { get; set; }
        [DataMember]
        [JsonProperty("CRNumber")]
        public string Crnumber { get; set; }
        [DataMember]
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [DataMember]
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [DataMember]
        [JsonProperty("mainIndustry")]
        public string MainInd { get; set; }
        [DataMember]
        [JsonProperty("outputValue")]
        public string OutputValFg { get; set; }
        [DataMember]
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        [DataMember]
        [JsonProperty("revenue")]
        public string RevenueFg { get; set; }
        [DataMember]
        [JsonProperty("subIndustry")]
        public string SubInd { get; set; }
        [DataMember]
        [JsonProperty("expenses")]
        public string ExpensesFg { get; set; }
        [DataMember]
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; }
        [DataMember]
        [JsonProperty("activityCode")]
        public string ActCd { get; set; }
        [DataMember]
        [JsonProperty("netProfit")]
        public string NetProfitFg { get; set; }
        [DataMember]
        [JsonProperty("activityDescription")]
        public string ActDesc { get; set; }
        [DataMember]
        [JsonProperty("container")]
        public string ContainerFg { get; set; }
        [DataMember]
        [JsonProperty("roomsNumber")]
        public string NoOfRooms { get; set; }
        [DataMember]
        [JsonProperty("numberOfRooms")]
        public string NoOfRoomsFg { get; set; }
        [DataMember]
        [JsonProperty("roomRateAmount")]
        public string RoomRate { get; set; }
        [DataMember]
        [JsonProperty("servicFees")]
        public string ServicFeeFg { get; set; }
        [DataMember]
        [JsonProperty("addRoomRateAmount")]
        public string AddRoomRate { get; set; }
        [DataMember]
        [JsonProperty("occupancyRate")]
        public string OccRateFg { get; set; }
        [DataMember]
        [JsonProperty("servicFeeAmount")]
        public string ServicFee { get; set; }
        [DataMember]
        [JsonProperty("occupancyRateAmount")]
        public string OccRate { get; set; }
        [DataMember]
        [JsonProperty("city")]
        public string City { get; set; }
        [DataMember]
        [JsonProperty("owned")]
        public string Owned { get; set; }
        [DataMember]
        [JsonProperty("outputAmount")]
        public string OutputVal { get; set; }
        [DataMember]
        [JsonProperty("revenueAmount")]
        public string Revenue { get; set; }
        [DataMember]
        [JsonProperty("expensesAmount")]
        public string Expenses { get; set; }
        [DataMember]
        [JsonProperty("netProfitAmount")]
        public string NetProfit { get; set; }
        [DataMember]
        [JsonProperty("containerAmount")]
        public string Container { get; set; }
        [DataMember]
        [JsonProperty("lineCount")]
        public int LineCnt { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }
        [DataMember]
        public string IsOwned { get; set; }
        [DataMember]
        public string IsExpApplicable { get; set; }
        [DataMember]
        public bool IsApplicableVisible { get; set; }


    }
   
    public class SCHGP08
    {
        [DataMember]
        [JsonProperty("schedulesGroup08")]
        public List<Result26> results { get; set; }
    }
   
   
    public class Result27
    {
        [DataMember]
        public Metadata6 __metadata { get; set; }
        [DataMember]
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("year")]
        public string YearFg { get; set; }
        [DataMember]
        [JsonProperty("objection")]
        public string ObjFg { get; set; }
        [DataMember]
        [JsonProperty("markDeletion")]
        public string MarkDel { get; set; }
        [DataMember]
        [JsonProperty("outletType")]
        public string OutletTp { get; set; }
        [DataMember]
        [JsonProperty("revenue")]
        public string RevenueFg { get; set; }
        [DataMember]
        [JsonProperty("CRNumber")]
        public string Crnumber { get; set; }
        [DataMember]
        [JsonProperty("expenses")]
        public string ExpensesFg { get; set; }
        [DataMember]
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [DataMember]
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [DataMember]
        [JsonProperty("mainIndustry")]
        public string MainInd { get; set; }
        [DataMember]
        [JsonProperty("netProfit")]
        public string NetPftFg { get; set; }
        [DataMember]
        [JsonProperty("container")]
        public string ContainerFg { get; set; }
        [DataMember]
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        [DataMember]
        [JsonProperty("subIndustry")]
        public string SubInd { get; set; }
        [DataMember]
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; }
        [DataMember]
        [JsonProperty("activityCode")]
        public string ActCd { get; set; }
        [DataMember]
        [JsonProperty("activityDescription")]
        public string ActDesc { get; set; }
        [DataMember]
        [JsonProperty("revenueAmount")]
        public string Revenue { get; set; }
        [DataMember]
        [JsonProperty("expensesAmount")]
        public string Expenses { get; set; }
        [DataMember]
        [JsonProperty("netProfitAmount")]
        public string NetPft { get; set; }
        [DataMember]
        [JsonProperty("containerAmount")]
        public string Container { get; set; }
        [DataMember]
        [JsonProperty("lineCount")]
        public int LineCnt { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }
        [DataMember]
        public string IsApplicable { get; set; }
        [DataMember]
        public bool IsApplicableVisible { get; set; }
    }
   
    public class SCHGP02
    {
        [DataMember]
        [JsonProperty("schedulesGroup02")]
        public List<Result27> results { get; set; }
    }

    public class Result28
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("averageDailyIncome")]
        public string AgvDlyIncomeFg { get; set; }
        [DataMember]
        [JsonProperty("year")]
        public string YearFg { get; set; }
        [DataMember]
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("markDeletion")]
        public string MarkDel { get; set; }
        [DataMember]
        [JsonProperty("outletType")]
        public string OutletTp { get; set; }
        [DataMember]
        [JsonProperty("outputValue")]
        public string OutputValFg { get; set; }
        [DataMember]
        [JsonProperty("CRNumber")]
        public string Crnumber { get; set; }
        [DataMember]
        [JsonProperty("carsNumber")]
        public string NoOfCars { get; set; }
        [DataMember]
        [JsonProperty("revenue")]
        public string RevenueFg { get; set; }
        [DataMember]
        [JsonProperty("expenses")]
        public string ExpensesFg { get; set; }
        [DataMember]
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [DataMember]
        [JsonProperty("mainIndustry")]
        public string MainInd { get; set; }
        [DataMember]
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [DataMember]
        [JsonProperty("netProfit")]
        public string NetPftFg { get; set; }
        [DataMember]
        [JsonProperty("subIndustry")]
        public string SubInd { get; set; }
        [DataMember]
        [JsonProperty("container")]
        public string ContainerFg { get; set; }
        [DataMember]
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        [DataMember]
        [JsonProperty("numberOfCars")]
        public string NoOfCarsFg { get; set; }
        [DataMember]
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; }
        [DataMember]
        [JsonProperty("activityCode")]
        public string ActCd { get; set; }
        [DataMember]
        [JsonProperty("occupancyRate")]
        public string OccRateFg { get; set; }
        [DataMember]
        [JsonProperty("activityDescription")]
        public string ActDesc { get; set; }
        [DataMember]
        [JsonProperty("netProfitPercentage")]
        public string NetPftPerFg { get; set; }
        [DataMember]
        [JsonProperty("averageDailyIncomeAmount")]
        public string AgvDlyIncome { get; set; }
        [DataMember]
        [JsonProperty("occupancyRateAmount")]
        public string OccRate { get; set; }
        [DataMember]
        [JsonProperty("netProfitPercentageAmount")]
        public string NetPftPer { get; set; }
        [DataMember]
        [JsonProperty("outputAmount")]
        public string OutputVal { get; set; }
        [DataMember]
        [JsonProperty("revenueAmount")]
        public string Revenue { get; set; }
        [DataMember]
        [JsonProperty("expensesAmount")]
        public string Expenses { get; set; }
        [DataMember]
        [JsonProperty("netProfitAmount")]
        public string NetPft { get; set; }
        [DataMember]
        [JsonProperty("containerAmount")]
        public string Container { get; set; }
        [DataMember]
        [JsonProperty("lineCount")]
        public int LineCnt { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }
        [DataMember]
        public string IsApplicable { get; set; }
        [DataMember]
        public bool IsApplicableVisible { get; set; }

    }
   
    public class SCHGP01
    {
        [DataMember]
        // [JsonProperty("schedulesGroup01")]
        public List<Result28> results { get; set; }
    }

    public class Result29
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("markDeletion")]
        public string MarkDel { get; set; }
        [DataMember]
        [JsonProperty("delete")]
        public string Delflag { get; set; }
        [DataMember]
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [DataMember]
        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }
        [DataMember]
        [JsonProperty("lineNumber")]
        public int LineNo { get; set; }
        [DataMember]
        [JsonProperty("rankingOrder")]
        public string RankingOrder { get; set; }
        [DataMember]
        [JsonProperty("activityCode")]
        public string ActCd { get; set; }
        [DataMember]
        [JsonProperty("activityDescription")]
        public string ActDesc { get; set; }
        [DataMember]
        [JsonProperty("groupName")]
        public string GrpNm { get; set; }
        [DataMember]
        [JsonProperty("lineCount")]
        public int LineCnt { get; set; }
        [DataMember]
        [JsonProperty("description")]
        public string Description { get; set; }
        [DataMember]
        [JsonProperty("amount")]
        public string Amount { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }
    }
   
    public class GENSUBSCH
    {
        [DataMember]
        [JsonProperty("generalSubSchedules")]
        public List<Result29> results { get; set; }
    }
   
    public class ZakatForm5DataResult
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("attendCheckbox")]
        public string AttenbChk { get; set; }
        [DataMember]
        [JsonProperty("amendmentAllow")]
        public string Amdallow { get; set; }
        [DataMember]
        [JsonProperty("amendmentCheck")]
        public string AmendFgChkz { get; set; }
        [DataMember]
        [JsonProperty("CR1317GoLive")]
        public string Cr1317golive { get; set; }
        [DataMember]
        [JsonProperty("CR1862GoLive")]
        public string Cr1862golive { get; set; }
        [DataMember]
        [JsonProperty("CR1890GoLive")]
        public string Cr1890golive { get; set; }
        [DataMember]
        [JsonProperty("preliminaryAttachment")]
        public string PrelAttachFgz { get; set; }
        [DataMember]
        [JsonProperty("userStatus")]
        public string Fbust { get; set; }
        [DataMember]
        [JsonProperty("liabilityCheckbox")]
        public string LibChk { get; set; }
        [DataMember]
        [JsonProperty("selfAmendment")]
        public string SelfAmd { get; set; }
        [DataMember]
        [JsonProperty("accountantExamine")]
        public string AAccExam { get; set; }
        [DataMember]
        [JsonProperty("paymentAmountTaxpayer")]
        public string APayAmtTp { get; set; }
        [DataMember]
        [JsonProperty("governmentCode")]
        public string Govcodfg { get; set; }
        [DataMember]
        [JsonProperty("CR71112GoLive")]
        public string Gp71112fg { get; set; }
        [DataMember]
        [JsonProperty("installment")]
        public string Instfg { get; set; }
        [DataMember]
        [JsonProperty("message")]
        public string Msgfg { get; set; }
        [DataMember]
        [JsonProperty("messageDescription")]
        public string Msgtx { get; set; }
        [DataMember]
        [JsonProperty("mobileNumber")]
        public string AMobile { get; set; }
        [DataMember]
        [JsonProperty("accountMethod")]
        public string AActmethod { get; set; }
        [DataMember]
        [JsonProperty("adjacentPosition")]
        public string AAdj { get; set; }
        [DataMember]
        [JsonProperty("agreed")]
        public string AAgree { get; set; }
        [DataMember]
        [JsonProperty("agreedDate ")]
        public object AAgreeDt { get; set; }
        [DataMember]
        [JsonProperty("annualSalesRevenueAmount")]
        public string AAnnSalesrevAmt { get; set; }
        [DataMember]
        [JsonProperty("annualSalesRevenueOverdue")]
        public string AAnnSalesrevOu { get; set; }
        [DataMember]
        [JsonProperty("annualSalesRevenueProfit")]
        public string AAnnSalesrevPr { get; set; }
        [DataMember]
        [JsonProperty("annualSalesRevenueTotal")]
        public string AAnnSalesrevTot { get; set; }
        [DataMember]
        [JsonProperty("annualSalesRevenueTo")]
        public string AAnnSalesrevTotFg { get; set; }
        [DataMember]
        [JsonProperty("annualRentAmount")]
        public string AAnnualRent { get; set; }
        [DataMember]
        [JsonProperty("annualRent")]
        public string AAnnualRentFg { get; set; }
        [DataMember]
        [JsonProperty("taxPayerApprovalBonds")]
        public string AAppAdopTp { get; set; }
        [DataMember]
        [JsonProperty("percentageShare")]
        public string AArzaqIPer { get; set; }
        [DataMember]
        [JsonProperty("bondsApprove")]
        public string ABondsApp { get; set; }
        [DataMember]
        [JsonProperty("building")]
        public string ABulding { get; set; }
        [DataMember]
        [JsonProperty("calendarType")]
        public string ACalendarTp { get; set; }
        [DataMember]
        [JsonProperty("capitalGains")]
        public string ACapital { get; set; }
        [DataMember]
        [JsonProperty("capital")]
        public string ACapitalFg { get; set; }
        [DataMember]
        [JsonProperty("capitalOfficialUse")]
        public string ACapitalOu { get; set; }
        [DataMember]
        [JsonProperty("codeName")]
        public string ACdNm { get; set; }
        [DataMember]
        [JsonProperty("check")]
        public string ACheckFg { get; set; }
        [DataMember]
        [JsonProperty("city")]
        public string ACity { get; set; }
        [DataMember]
        [JsonProperty("companyName")]
        public string ACompNm { get; set; }
        [DataMember]
        [JsonProperty("contractRevenueAmount")]
        public string AConrevAmt { get; set; }
        [DataMember]
        [JsonProperty("contractRevenueAmountCheckbox")]
        public string AConrevAmtChk { get; set; }
        [DataMember]
        [JsonProperty("contractRevenueOffcialUse")]
        public string AConrevOu { get; set; }
        [DataMember]
        [JsonProperty("contractRevenueProfit")]
        public string AConrevPr { get; set; }
        [DataMember]
        [JsonProperty("contractRevenueTotal")]
        public string AConrevTot { get; set; }
        [DataMember]
        [JsonProperty("district")]
        public string ADistrict { get; set; }
        [DataMember]
        [JsonProperty("email")]
        public string AEmail { get; set; }
        [DataMember]
        [JsonProperty("examinerManager")]
        public string AExamMngr { get; set; }
        [DataMember]
        [JsonProperty("faxNumber")]
        public string AFaxNo { get; set; }
        [DataMember]
        [JsonProperty("financialCalendar")]
        public string AFiscalCalendar { get; set; }
        [DataMember]
        [JsonProperty("financialPeriod")]
        public string AFiscalPeriod { get; set; }
        [DataMember]
        [JsonProperty("fromDateTime")]
        public DateTime AFromDt { get; set; }
        [DataMember]
        [JsonProperty("fromDate")]
        public string AFromDtCh { get; set; }
        [DataMember]
        [JsonProperty("fromDateCalendarType")]
        public string AFromDtTp { get; set; }
        [DataMember]
        [JsonProperty("generalTradingAmount")]
        public string AGenTradeAmt { get; set; }
        [DataMember]
        [JsonProperty("generalTradingIPercentage")]
        public string AGentradeIPer { get; set; }
        [DataMember]
        [JsonProperty("generalTradingOfficialUse")]
        public string AGenTradeOu { get; set; }
        [DataMember]
        [JsonProperty("generalTradingPercentage")]
        public string AGenTradePr { get; set; }
        [DataMember]
        [JsonProperty("generalTradingTotaal")]
        public string AGenTradeTot { get; set; }
        [DataMember]
        [JsonProperty("legalStatus")]
        public string ALegalSt { get; set; }
        [DataMember]
        [JsonProperty("livelihoodsAmount")]
        public string ALivelihoodsAmt { get; set; }
        [DataMember]
        [JsonProperty("livelihoodsOfficialUse")]
        public string ALivelihoodsOu { get; set; }
        [DataMember]
        [JsonProperty("livelihoodsPercentage")]
        public string ALivelihoodsPr { get; set; }
        [DataMember]
        [JsonProperty("livelihoodsTotal")]
        public string ALivelihoodsTot { get; set; }
        [DataMember]
        [JsonProperty("livestockPercntage")]
        public string ALivestkIPer { get; set; }
        [DataMember]
        [JsonProperty("livestockAndVegetablesAmount")]
        public string ALivestkVegtbAmt { get; set; }
        [DataMember]
        [JsonProperty("livestockAndVegetablesOfficialUse")]
        public string ALivestkVegtbOu { get; set; }
        [DataMember]
        [JsonProperty("livestockAndVegetablesProfit")]
        public string ALivestkVegtbPr { get; set; }
        [DataMember]
        [JsonProperty("livestockAndVegetablesTotalAmount")]
        public string ALivestkVegtbTot { get; set; }
        [DataMember]
        [JsonProperty("mainAccountAmount")]
        public string AMainact { get; set; }
        [DataMember]
        [JsonProperty("mainAccountCode")]
        public string AMainAct { get; set; }
        [DataMember]
        [JsonProperty("mainAccountDescription")]
        public string AMainActDesc { get; set; }
        [DataMember]
        [JsonProperty("managementAudit")]
        public string AMangmentAudit { get; set; }
        [DataMember]
        [JsonProperty("amendmentReaseon")]
        public string AmdRsnz { get; set; }
        [DataMember]
        [JsonProperty("name")]
        public string AName { get; set; }
        [DataMember]
        [JsonProperty("netIncomeOtherActivitiesAmount")]
        public string ANetIncomeOthActvAmt { get; set; }
        [DataMember]
        [JsonProperty("netIncomeOtherActivitiesAmountCheck")]
        public string ANetIncomeOthActvAmtChk { get; set; }
        [DataMember]
        [JsonProperty("netIncomeOtherActivitiesOfficialUse")]
        public string ANetIncomeOthActvOu { get; set; }
        [DataMember]
        [JsonProperty("netIncomeOtherActivitiesProfit")]
        public string ANetIncomeOthActvPr { get; set; }
        [DataMember]
        [JsonProperty("netIncomeOtherActivitiesTotal")]
        public string ANetIncomeOthActvTot { get; set; }
        [DataMember]
        [JsonProperty("netTaxableAmount")]
        public string ANetTaxableAmount { get; set; }
        [DataMember]
        [JsonProperty("netTaxableAmountOfficialUse")]
        public string ANetTaxableAmountOu { get; set; }
        [DataMember]
        [JsonProperty("branchesNumber")]
        public string ANoBranches { get; set; }
        [DataMember]
        [JsonProperty("employeesNumber")]
        public string ANoEmp { get; set; }
        [DataMember]
        [JsonProperty("employees")]
        public string ANoEmpFg { get; set; }
        [DataMember]
        [JsonProperty("financialNumber")]
        public string ANoFinance { get; set; }
        [DataMember]
        [JsonProperty("nonSaudiCashPercentage")]
        public string ANonsaudiCaSh { get; set; }
        [DataMember]
        [JsonProperty("nonSaudiPercentageShare")]
        public string ANonsaudiPrSh { get; set; }
        [DataMember]
        [JsonProperty("outletsNumber")]
        public string ANoOfOutlet { get; set; }
        [DataMember]
        [JsonProperty("otherRevenueAmount")]
        public string AOthincAmt { get; set; }
        [DataMember]
        [JsonProperty("otherRevenueOffice")]
        public string AOthincOs { get; set; }
        [DataMember]
        [JsonProperty("otherRevenueProfit")]
        public string AOthincPr { get; set; }
        [DataMember]
        [JsonProperty("otherRevenueTotalAmount")]
        public string AOthincTot { get; set; }
        [DataMember]
        [JsonProperty("otherRevenueTotal")]
        public string AOthincTotFg { get; set; }
        [DataMember]
        [JsonProperty("ownerNameQuaternary")]
        public string AOwnerNmQuat { get; set; }
        [DataMember]
        [JsonProperty("paidReceiptAmount")]
        public string APdRecptAmt { get; set; }
        [DataMember]
        [JsonProperty("paidReceiptAmountCheckbox")]
        public string APdRecptAmtChk { get; set; }
        [DataMember]
        [JsonProperty("paidReceiptOfficialUse")]
        public string APdRecptOu { get; set; }
        [DataMember]
        [JsonProperty("APlShareProfitLoss")]
        public string APlShare { get; set; }
        [DataMember]
        [JsonProperty("APlShareCheckbox")]
        public string APlShareChk { get; set; }
        [DataMember]
        [JsonProperty("APlShare")]
        public string APlShareFg { get; set; }
        [DataMember]
        [JsonProperty("APlShareOfficialUse")]
        public string APlShareOu { get; set; }
        [DataMember]
        [JsonProperty("POBox")]
        public string APoBox { get; set; }
        [DataMember]
        [JsonProperty("approve")]
        public string Approvez { get; set; }
        [DataMember]
        [JsonProperty("branchName")]
        public string APrctr { get; set; }
        [DataMember]
        [JsonProperty("releaseOfContract")]
        public string AReleaseOfContract { get; set; }
        [DataMember]
        [JsonProperty("releaseOfContractCheckbox")]
        public string AReleaseOfContractChk { get; set; }
        [DataMember]
        [JsonProperty("releaseOfContractOfficialUse")]
        public string AReleaseOfContractOu { get; set; }
        [DataMember]
        [JsonProperty("residency")]
        public string AResidency { get; set; }
        [DataMember]
        [JsonProperty("restPaidAmount")]
        public string ARestpaidAmt { get; set; }
        [DataMember]
        [JsonProperty("restPaidAmountOfficialUse")]
        public string ARestpaidAmtOu { get; set; }
        [DataMember]
        [JsonProperty("saudiCashPercentage")]
        public string ASaudiCaSh { get; set; }
        [DataMember]
        [JsonProperty("saudiProjectPercentage")]
        public string ASaudiPrSh { get; set; }
        [DataMember]
        [JsonProperty("stamp")]
        public string ASeal { get; set; }
        [DataMember]
        [JsonProperty("sign")]
        public string ASign { get; set; }
        [DataMember]
        [JsonProperty("documentLocationNumber")]
        public string ASitedocs { get; set; }
        [DataMember]
        [JsonProperty("step")]
        public int AStep { get; set; }
        [DataMember]
        [JsonProperty("street")]
        public string AStreet { get; set; }
        [DataMember]
        [JsonProperty("telephoneNumber")]
        public string ATelephone { get; set; }
        [DataMember]
        [JsonProperty("toDateTime")]
        public DateTime AToDt { get; set; }
        [DataMember]
        [JsonProperty("toDate")]
        public string AToDtCh { get; set; }
        [DataMember]
        [JsonProperty("toDateCalendarType")]
        public string AToDtTp { get; set; }
        [DataMember]
        [JsonProperty("annualSalary")]
        public string ATotAnnualSal { get; set; }
        [DataMember]
        [JsonProperty("totalAnnualSalary")]
        public string ATotAnnualSalFg { get; set; }
        [DataMember]
        [JsonProperty("totalOwedPaid")]
        public string ATotOwedpaid { get; set; }
        [DataMember]
        [JsonProperty("totalOwedPaidOfficialUse")]
        public string ATotOwedpaidOu { get; set; }
        [DataMember]
        [JsonProperty("totalProfit")]
        public string ATotProfit { get; set; }
        [DataMember]
        [JsonProperty("totalProfitOfficialUse")]
        public string ATotProfitOu { get; set; }
        [DataMember]
        [JsonProperty("auditor")]
        public string Auditorz { get; set; }
        [DataMember]
        [JsonProperty("authorizationGroup")]
        public string Augrp { get; set; }
        [DataMember]
        [JsonProperty("zakatPayable")]
        public string AZakat { get; set; }
        [DataMember]
        [JsonProperty("zakat51")]
        public string AZakat51 { get; set; }
        [DataMember]
        [JsonProperty("zakat51OfficialUse")]
        public string AZakat51Ou { get; set; }
        [DataMember]
        [JsonProperty("zakatOfficialUse")]
        public string AZakatOu { get; set; }
        [DataMember]
        [JsonProperty("zakatPreviousYearAmount")]
        public string AZakatPrevYr { get; set; }
        [DataMember]
        [JsonProperty("zakatPreviousYear")]
        public string AZakatPrevYrFg { get; set; }
        [DataMember]
        [JsonProperty("zakatPreviousYearOfficialUse")]
        public string AZakatPrevYrOu { get; set; }
        [DataMember]
        [JsonProperty("zipCode")]
        public string AZipCd { get; set; }
        [DataMember]
        [JsonProperty("partnerType")]
        public string Bpkind { get; set; }
        [DataMember]
        //  [JsonProperty("")]
        public string Cal { get; set; }
        [DataMember]
        [JsonProperty("caseGUID")]
        public string CaseGuid { get; set; }
        [DataMember]
        [JsonProperty("createTaxAssessment")]
        public string CreateTxAssesz { get; set; }
        [DataMember]
        [JsonProperty("displayMode")]
        public string Dmodez { get; set; }
        [DataMember]
        [JsonProperty("authenticationUser")]
        public string Euser { get; set; }
        [DataMember]
        [JsonProperty("formBundleGUID")]
        public string Fbguid { get; set; }
        [DataMember]
        [JsonProperty("formBundleGUID1")]
        public string Fbguid1 { get; set; }
        [DataMember]
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [DataMember]
        // [JsonProperty("")]
        public string Fbnumz { get; set; }
        [DataMember]
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [DataMember]
        [JsonProperty("formType")]
        public string Formtype { get; set; }
        [DataMember]
        [JsonProperty("forUser")]
        public string ForUser { get; set; }
        [DataMember]
        [JsonProperty("forward")]
        public string Forward { get; set; }
        [DataMember]
        [JsonProperty("forwardTurn")]
        public string Fturn { get; set; }
        [DataMember]
        [JsonProperty("CR0368GoLive")]
        public string Gp0368fg { get; set; }
        [DataMember]
        [JsonProperty("inboundChannel")]
        public string InChannel { get; set; }
        [DataMember]
        [JsonProperty("inboundCorrespondenceType")]
        public string Incotyp { get; set; }
        [DataMember]
        [JsonProperty("isAmend")]
        public string Isamend { get; set; }
        [DataMember]
        [JsonProperty("language")]
        public string Langz { get; set; }
        [DataMember]
        [JsonProperty("legacyDocumentNumber")]
        public string LegacyDocNo { get; set; }
        [DataMember]
        [JsonProperty("addressLine")]
        public string Line0 { get; set; }
        [DataMember]
        [JsonProperty("addressLine1")]
        public string Line1 { get; set; }
        [DataMember]
        [JsonProperty("addressLine2")]
        public string Line2 { get; set; }
        [DataMember]
        [JsonProperty("addressLine3")]
        public string Line3 { get; set; }
        [DataMember]
        [JsonProperty("addressLine4")]
        public string Line4 { get; set; }
        [DataMember]
        [JsonProperty("addressLine5")]
        public string Line5 { get; set; }
        [DataMember]
        [JsonProperty("addressLine6")]
        public string Line6 { get; set; }
        [DataMember]
        [JsonProperty("addressLine7")]
        public string Line7 { get; set; }
        [DataMember]
        [JsonProperty("addressLine8")]
        public string Line8 { get; set; }
        [DataMember]
        [JsonProperty("addressLine9")]
        public string Line9 { get; set; }
        [DataMember]
        [JsonProperty("mode")]
        public string Mode { get; set; }
        [DataMember]
        [JsonProperty("month")]
        public string Monthz { get; set; }
        [DataMember]
        [JsonProperty("objectionSubmit")]
        public string ObjSubmitz { get; set; }
        [DataMember]
        //  [JsonProperty("")]
        public string Ofbnum { get; set; }
        [DataMember]
        [JsonProperty("userName")]
        public string OfficerUidz { get; set; }
        [DataMember]
        [JsonProperty("onMode")]
        public string Omode { get; set; }
        [DataMember]
        [JsonProperty("periodKey")]
        public string PeriodKeyz { get; set; }
        [DataMember]
        [JsonProperty("periodDescription")]
        public string PerslText { get; set; }
        [DataMember]
        [JsonProperty("portalUser")]
        public string PortalUsrz { get; set; }
        [DataMember]
        [JsonProperty("contractNumber")]
        public string RegIdz { get; set; }
        [DataMember]
        [JsonProperty("reject")]
        public string Rejectz { get; set; }
        [DataMember]
        [JsonProperty("returnGUID")]
        public string Retguid { get; set; }
        [DataMember]
        [JsonProperty("save")]
        public string Savez { get; set; }
        [DataMember]
        [JsonProperty("saveNotes")]
        public string SavNot { get; set; }
        [DataMember]
        [JsonProperty("skipBilling")]
        public string SkipBillingz { get; set; }
        [DataMember]
        [JsonProperty("statusCode")]
        public string Status { get; set; }
        [DataMember]
        [JsonProperty("submit")]
        public string Submitz { get; set; }
        [DataMember]
        [JsonProperty("TIN")]
        public string Taxpayerz { get; set; }
        [DataMember]
        [JsonProperty("userType")]
        public string UserType { get; set; }
        [DataMember]
        [JsonProperty("void")]
        public string Xvoidz { get; set; }
        [DataMember]
        public GP038Set GP03_8Set { get; set; }
        [DataMember]
        [JsonProperty("livestockAndAnimalsInternal")]
        public List<Results2> GP03_7Set { get; set; }
        [DataMember]
        [JsonProperty("profitLossPartnership")]
        public List<Results3> SCH_200Set { get; set; }
        [DataMember]
        [JsonProperty("zakatContractReleasePayments")]
        public List<Results4> SCH_800Set { get; set; }
        [DataMember]
        [JsonProperty("schedulesGroup07")]
        public List<Results5> SCH_GP07 { get; set; }
        [DataMember]
        [JsonProperty("schedulesGroup06")]
        public List<Results6> SCH_GP06 { get; set; }
        [DataMember]
        [JsonProperty("schedulesGroup05")]
        public List<Results7> SCH_GP05 { get; set; }
        [DataMember]
        [JsonProperty("schedulesGroup04")]
        public List<Results8> SCH_GP04 { get; set; }

        [DataMember]
        [JsonProperty("schedulesGroup03")]
        public List<Results9> SCH_GP03 { get; set; }
        [DataMember]
        [JsonProperty("capitalGainCalculations")]
        public List<Results10> SCH_GP3S2Set { get; set; }
        [DataMember]
        [JsonProperty("totalImportsAndInternalProcurements")]
        public List<Results11> SCH_GP3S1Set { get; set; }
        [DataMember]
        [JsonProperty("GP03_6Set")]
        public List<Results12> GP03_6Set { get; set; }
        [DataMember]
        [JsonProperty("attachments")]
        public List<object> AttDetSet { get; set; }
        [DataMember]
        [JsonProperty("subSchedulesCapital")]
        public List<Results13> SUB_SCH_CAPITALSet { get; set; }
        [DataMember]
        public List<Results14> GP03_4Set { get; set; }
        [DataMember]
        [JsonProperty("GP03_5Set")]
        public List<Result15> GP03_5Set { get; set; }
        [DataMember]
        [JsonProperty("GP06_3Set")]
        public List<Result16> GP06_3Set { get; set; }
        [DataMember]
        public List<Result17> GP03_2Set { get; set; }
        [DataMember]
        [JsonProperty("governmentContractsRevenue")]
        public List<Result18> GP06_1Set { get; set; }
        [DataMember]
        [JsonProperty("GP06_2Set")]
        public List<Result19> GP06_2Set { get; set; }
        [DataMember]
        public List<Result20> GP03_3Set { get; set; }
        [DataMember]
        [JsonProperty("mainActivities")]
        public List<Result21> MAIN_ACTIVITYSet { get; set; }
        [DataMember]
        [JsonProperty("taxOfficerNotes")]
        public List<object> LONG_TEXTSet { get; set; }
        [DataMember]
        [JsonProperty("schedulesGroup12")]
        public List<Result22> SCH_GP12 { get; set; }
        [DataMember]
        [JsonProperty("schedulesGroup11")]
        public List<Result23> SCH_GP11 { get; set; }
        [DataMember]
        [JsonProperty("schedulesGroup10")]
        public List<Result24> SCH_GP10 { get; set; }
        [DataMember]
        [JsonProperty("schedulesGroup09")]
        public List<Result25> SCH_GP09 { get; set; }
        [DataMember]
        [JsonProperty("schedulesGroup08")]
        public List<Result26> SCH_GP08 { get; set; }
        [DataMember]
        [JsonProperty("schedulesGroup02")]
        public List<Result27> SCH_GP02 { get; set; }
        [DataMember]
        [JsonProperty("schedulesGroup01")]
        public List<Result28> SCH_GP01 { get; set; }
        [DataMember]
        [JsonProperty("generalSubSchedules")]
        public List<Result29> GEN_SUB_SCH { get; set; }

        [DataMember]
        public string IsOtherShareApplicable { get; set; }
        [DataMember]
        public bool IsOtherShareApplicableVisible { get; set; }

    }
}