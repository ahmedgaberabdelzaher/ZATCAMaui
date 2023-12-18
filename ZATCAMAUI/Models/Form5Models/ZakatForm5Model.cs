using System.Runtime.Serialization;

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
        public string SubDescription { get; set; }
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
   
    public class GP037Set
    {
        [DataMember]
        public List<Results2> results { get; set; }
    }
   
   
    public class Results3
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
        public string Tin { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Amount { get; set; }
        [DataMember]
        public string Waers { get; set; }
    }
   
    public class SCH200Set
    {
        [DataMember]
        public List<Results3> results { get; set; }
    }
   
    public class Results4
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
        public string ContractNo { get; set; }
        [DataMember]
        public string Fbnum { get; set; }
        [DataMember]
        public string ZakatPaid { get; set; }
        [DataMember]
        public string Waers { get; set; }
    }
   
    public class SCH800Set
    {
        [DataMember]
        public List<Results4> results { get; set; }
    }
   
   
    public class Results5
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        public string Mandt { get; set; }
        [DataMember]
        public string MinimumFg { get; set; }
        [DataMember]
        public string YearFg { get; set; }
        [DataMember]
        public string ObjFg { get; set; }
        [DataMember]
        public string MarkDel { get; set; }
        [DataMember]
        public string OutletTp { get; set; }
        [DataMember]
        public string RevenueFg { get; set; }
        [DataMember]
        public string Crnumber { get; set; }
        [DataMember]
        public string ExpensesFg { get; set; }
        [DataMember]
        public string FormGuid { get; set; }
        [DataMember]
        public string DataVersion { get; set; }
        [DataMember]
        public string MainInd { get; set; }
        [DataMember]
        public string NetPftFg { get; set; }
        [DataMember]
        public string ContainerFg { get; set; }
        [DataMember]
        public int LineNo { get; set; }
        [DataMember]
        public string SubInd { get; set; }
        [DataMember]
        public string RankingOrder { get; set; }
        [DataMember]
        public string ActCd { get; set; }
        [DataMember]
        public string ActDesc { get; set; }
        [DataMember]
        public string Revenue { get; set; }
        [DataMember]
        public string Expenses { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string NetPft { get; set; }
        [DataMember]
        public string Container { get; set; }
        [DataMember]
        public int LineCnt { get; set; }
        [DataMember]
        public string Waers { get; set; }
        [DataMember]
        public string IsExpencesApplicable { get; set; }
        [DataMember]
        public bool IsApplicableVisible { get; set; }


    }
   
    public class SCHGP07
    {
        [DataMember]
        public List<Results5> results { get; set; }
    }
   
    public class Results6
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        public string Capital { get; set; }
        [DataMember]
        public string GovtContractProfFg { get; set; }
        [DataMember]
        public string YearFg { get; set; }
        [DataMember]
        public string Mandt { get; set; }
        [DataMember]
        public string MarkDel { get; set; }
        [DataMember]
        public string OutletTp { get; set; }
        [DataMember]
        public string CivilContrRevFg { get; set; }
        [DataMember]
        public string Crnumber { get; set; }
        [DataMember]
        public string FormGuid { get; set; }
        [DataMember]
        public string DataVersion { get; set; }
        [DataMember]
        public string MainInd { get; set; }
        [DataMember]
        public string OtherIncomeFg { get; set; }
        [DataMember]
        public int LineNo { get; set; }
        [DataMember]
        public string SubInd { get; set; }
        [DataMember]
        public string TotalProfitFg { get; set; }
        [DataMember]
        public string ContainerFg { get; set; }
        [DataMember]
        public string RankingOrder { get; set; }
        [DataMember]
        public string ActCd { get; set; }
        [DataMember]
        public string NoOfLabourFg { get; set; }
        [DataMember]
        public string ActDesc { get; set; }
        [DataMember]
        public int GovtContCnt { get; set; }
        [DataMember]
        public string GovtContractProf { get; set; }
        [DataMember]
        public int CivilContCnt { get; set; }
        [DataMember]
        public string CivilContrRev { get; set; }
        [DataMember]
        public int OthIncCnt { get; set; }
        [DataMember]
        public string OtherIncome { get; set; }
        [DataMember]
        public string TotalProfit { get; set; }
        [DataMember]
        public string NoOfLabours { get; set; }
        [DataMember]
        public string Container { get; set; }
        [DataMember]
        public int LineCnt { get; set; }
        [DataMember]
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
        public List<Results6> results { get; set; }
    }
   
    public class Results7
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        public string YearFg { get; set; }
        [DataMember]
        public string FundingTotFg { get; set; }
        [DataMember]
        public string Mandt { get; set; }
        [DataMember]
        public string MarkDel { get; set; }
        [DataMember]
        public string OutletTp { get; set; }
        [DataMember]
        public string CapitalStrFg { get; set; }
        [DataMember]
        public string Crnumber { get; set; }
        [DataMember]
        public string FormGuid { get; set; }
        [DataMember]
        public string DataVersion { get; set; }
        [DataMember]
        public string MainInd { get; set; }
        [DataMember]
        public string NetCapitalFg { get; set; }
        [DataMember]
        public int LineNo { get; set; }
        [DataMember]
        public string SalesFg { get; set; }
        [DataMember]
        public string SubInd { get; set; }
        [DataMember]
        public string ProfitFg { get; set; }
        [DataMember]
        public string RankingOrder { get; set; }
        [DataMember]
        public string ActCd { get; set; }
        [DataMember]
        public string StatutoryPftFg { get; set; }
        [DataMember]
        public string ActDesc { get; set; }
        [DataMember]
        public string ContainerFg { get; set; }
        [DataMember]
        public string FundingTot { get; set; }
        [DataMember]
        public string CapitalStr { get; set; }
        [DataMember]
        public string NetCapital { get; set; }
        [DataMember]
        public string Sales { get; set; }
        [DataMember]
        public string Profit { get; set; }
        [DataMember]
        public string StatutoryPft { get; set; }
        [DataMember]
        public string Container { get; set; }
        [DataMember]
        public int LineCnt { get; set; }
        [DataMember]
        public string Waers { get; set; }

    }
   
    public class SCHGP05
    {
        [DataMember]
        public List<Results7> results { get; set; }
    }
   
   
    public class Results8
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        public string YearFg { get; set; }
        [DataMember]
        public string Mandt { get; set; }
        [DataMember]
        public string MarkDel { get; set; }
        [DataMember]
        public string OutletTp { get; set; }
        [DataMember]
        public string RevenueFg { get; set; }
        [DataMember]
        public string Crnumber { get; set; }
        [DataMember]
        public string ExpensesFg { get; set; }
        [DataMember]
        public string FormGuid { get; set; }
        [DataMember]
        public string DataVersion { get; set; }
        [DataMember]
        public string MainInd { get; set; }
        [DataMember]
        public string NetPftFg { get; set; }
        [DataMember]
        public string ContainerFg { get; set; }
        [DataMember]
        public int LineNo { get; set; }
        [DataMember]
        public string SubInd { get; set; }
        [DataMember]
        public string NoOfLabourFg { get; set; }
        [DataMember]
        public string RankingOrder { get; set; }
        [DataMember]
        public string ActCd { get; set; }
        [DataMember]
        public string ActDesc { get; set; }
        [DataMember]
        public string Revenue { get; set; }
        [DataMember]
        public string Expenses { get; set; }
        [DataMember]
        public string NetProfit { get; set; }
        [DataMember]
        public string NoOfLabour { get; set; }
        [DataMember]
        public string Container { get; set; }
        [DataMember]
        public int LineCnt { get; set; }
        [DataMember]
        public string Waers { get; set; }

        [DataMember]
        public string IsExpensesApplicable { get; set; }
        [DataMember]
        public bool IsApplicableVisible { get; set; }


    }
   
    public class SCHGP04
    {
        [DataMember]
        public List<Results8> results { get; set; }
    }
   
   
    public class Results9
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        public int GenTradeICnt { get; set; }
        [DataMember]
        public string GenTradeIFg { get; set; }
        [DataMember]
        public string YearFg { get; set; }
        [DataMember]
        public string LivelihoodsIFg { get; set; }
        [DataMember]
        public string Mandt { get; set; }
        [DataMember]
        public string GenTradeI { get; set; }
        [DataMember]
        public string LiveStkAnimalsIFg { get; set; }
        [DataMember]
        public string MarkDel { get; set; }
        [DataMember]
        public int ArzaqICnt { get; set; }
        [DataMember]
        public string OutletTp { get; set; }
        [DataMember]
        public string LivelihoodsI { get; set; }
        [DataMember]
        public string SuppConFg { get; set; }
        [DataMember]
        public string Crnumber { get; set; }
        [DataMember]
        public int LiveStkICnt { get; set; }
        [DataMember]
        public string FormGuid { get; set; }
        [DataMember]
        public string LiveStkAnimalsI { get; set; }
        [DataMember]
        public string SalesFg { get; set; }
        [DataMember]
        public string CapitalFg { get; set; }
        [DataMember]
        public string DataVersion { get; set; }
        [DataMember]
        public string MainInd { get; set; }
        [DataMember]
        public string ContainerFg { get; set; }
        [DataMember]
        public int LineNo { get; set; }
        [DataMember]
        public string SubInd { get; set; }
        [DataMember]
        public string GenTradeFg { get; set; }
        [DataMember]
        public string RankingOrder { get; set; }
        [DataMember]
        public string ActCd { get; set; }
        [DataMember]
        public string LivelihoodsFg { get; set; }
        [DataMember]
        public string ActDesc { get; set; }
        [DataMember]
        public string LiveStkAnimalsFg { get; set; }
        [DataMember]
        public string ExtnProcFg { get; set; }
        [DataMember]
        public int LineCnt { get; set; }
        [DataMember]
        public string IntnProcFg { get; set; }
        [DataMember]
        public string SuppCon { get; set; }
        [DataMember]
        public string ProfitRatio { get; set; }
        [DataMember]
        public string TotalFg { get; set; }
        [DataMember]
        public string ProfitRatioFg { get; set; }
        [DataMember]
        public string Sales { get; set; }
        [DataMember]
        public string SalePrfRatio { get; set; }
        [DataMember]
        public string SalePrfRatioFg { get; set; }
        [DataMember]
        public string Capital { get; set; }
        [DataMember]
        public string Container { get; set; }
        [DataMember]
        public int GenTradeCnt { get; set; }
        [DataMember]
        public string GenTrade { get; set; }
        [DataMember]
        public int ArzaqCnt { get; set; }
        [DataMember]
        public string Livelihoods { get; set; }
        [DataMember]
        public int LiveStkCnt { get; set; }
        [DataMember]
        public string LiveStkAnimals { get; set; }
        [DataMember]
        public string ExtnProc { get; set; }
        [DataMember]
        public int IntnProcCnt { get; set; }
        [DataMember]
        public string IntnProc { get; set; }
        [DataMember]
        public string Total { get; set; }
        [DataMember]
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
        public List<Results9> results { get; set; }
    }
   
   
    public class Results10
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
        public string Persl { get; set; }
        [DataMember]
        public string PerslDesc { get; set; }
        [DataMember]
        public string CapitalMci { get; set; }
        [DataMember]
        public string Capital { get; set; }
        [DataMember]
        public string CapitalAcc { get; set; }
        [DataMember]
        public string CapitalGain { get; set; }
        [DataMember]
        public string MaxCapital { get; set; }
    }
   
    public class SCHGP3S2Set
    {
        [DataMember]
        public List<Results10> results { get; set; }
    }
   
 
   
    public class Results11
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
        public string Persl { get; set; }
        [DataMember]
        public string PerslDesc { get; set; }
        [DataMember]
        public string ImportTot { get; set; }
        [DataMember]
        public string Import3rdParty { get; set; }
        [DataMember]
        public string HigherImport { get; set; }
        [DataMember]
        public string IntnProc { get; set; }
        [DataMember]
        public string TotImpIntn { get; set; }
        [DataMember]
        public string CapitalGain { get; set; }
    }
   
    public class SCHGP3S1Set
    {
        [DataMember]
        public List<Results11> results { get; set; }
    }
   

   
    public class Results12
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
   
    public class GP036Set
    {
        [DataMember]
        public List<Results12> results { get; set; }
    }
   
    public class AttDetSet
    {
        [DataMember]
        public List<object> results { get; set; }
    }
   

   
    public class Results13
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        public string MarkDel { get; set; }
        [DataMember]
        public string Delflag { get; set; }
        [DataMember]
        public string LoanDtTp { get; set; }
        [DataMember]
        public object LoanDt { get; set; }
        [DataMember]
        public string ApprovalDtTp { get; set; }
        [DataMember]
        public string Mandt { get; set; }
        [DataMember]
        public object ApprovalDt { get; set; }
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
        public string LoanVal { get; set; }
        [DataMember]
        public string NumberOfYr { get; set; }
        [DataMember]
        public string IncrCapital { get; set; }
        [DataMember]
        public string Waers { get; set; }
    }
   
    public class SUBSCHCAPITALSet
    {
        [DataMember]
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
        public string MarkDel { get; set; }
        [DataMember]
        public string Delflag { get; set; }
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
        public string Value { get; set; }
        [DataMember]
        public string ProfitRatio { get; set; }
        [DataMember]
        public string Profit { get; set; }
        [DataMember]
        public string Waers { get; set; }
    }
   
    public class GP035Set
    {
        [DataMember]
        public List<Result15> results { get; set; }
    }
   
   
    public class Result16
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
        public string ContractVal { get; set; }
        [DataMember]
        public string StipendContr { get; set; }
        [DataMember]
        public string PrfOfConrtact { get; set; }
        [DataMember]
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
        public string MarkDel { get; set; }
        [DataMember]
        public string RecordNo { get; set; }
        [DataMember]
        public string ContractValFg { get; set; }
        [DataMember]
        public string GovCode { get; set; }
        [DataMember]
        public string Delflag { get; set; }
        [DataMember]
        public string FormGuid { get; set; }
        [DataMember]
        public string StipendContrFg { get; set; }
        [DataMember]
        public string DataVersion { get; set; }
        [DataMember]
        public string PrfOfConrtactFg { get; set; }
        [DataMember]
        public int LineNo { get; set; }
        [DataMember]
        public string Flag { get; set; }
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
        public string ContractVal { get; set; }
        [DataMember]
        public string StipendContr { get; set; }
        [DataMember]
        public string PrfOfConrtact { get; set; }
        [DataMember]
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
        public string FormGuid { get; set; }
        [DataMember]
        public string Delflag { get; set; }
        [DataMember]
        public string TincrnoInd { get; set; }
        [DataMember]
        public string RecordNo { get; set; }
        [DataMember]
        public string Tin { get; set; }
        [DataMember]
        public string MarkDel { get; set; }
        [DataMember]
        public string ContractingName { get; set; }
        [DataMember]
        public string DataVersion { get; set; }
        [DataMember]
        public string MocContractor { get; set; }
        [DataMember]
        public int LineNo { get; set; }
        [DataMember]
        public string ContractValFg { get; set; }
        [DataMember]
        public string RankingOrder { get; set; }
        [DataMember]
        public string StipendContrFg { get; set; }
        [DataMember]
        public string ActCd { get; set; }
        [DataMember]
        public string PrfOfConrtactFg { get; set; }
        [DataMember]
        public string ActDesc { get; set; }
        [DataMember]
        public string Flag { get; set; }
        [DataMember]
        public int LineCnt { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string ContractVal { get; set; }
        [DataMember]
        public string StipendContr { get; set; }
        [DataMember]
        public string PrfOfConrtact { get; set; }
        [DataMember]
        public string Waers { get; set; }
    }
   
    public class GP062Set
    {
        [DataMember]
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
        public string ContractDtTp { get; set; }
        [DataMember]
        public string OriginalValFg { get; set; }
        [DataMember]
        public string ContractDur { get; set; }
        [DataMember]
        public string AdjustContFg { get; set; }
        [DataMember]
        public string Mandt { get; set; }
        [DataMember]
        public string PreyrWrkFg { get; set; }
        [DataMember]
        public string CurryrWrkFg { get; set; }
        [DataMember]
        public string FormGuid { get; set; }
        [DataMember]
        public string RemnWrkFg { get; set; }
        [DataMember]
        public string DataVersion { get; set; }
        [DataMember]
        public string ContractPartyInd { get; set; }
        [DataMember]
        public int LineNo { get; set; }
        [DataMember]
        public string ContractSubInd { get; set; }
        [DataMember]
        public string RankingOrder { get; set; }
        [DataMember]
        public string ContractDtInd { get; set; }
        [DataMember]
        public string ContractParty { get; set; }
        [DataMember]
        public string ContractDurInd { get; set; }
        [DataMember]
        public string ContractSub { get; set; }
        [DataMember]
        public string OriginalValInd { get; set; }
        [DataMember]
        public object ContractDt { get; set; }
        [DataMember]
        public string OriginalVal { get; set; }
        [DataMember]
        public string AdjustCont { get; set; }
        [DataMember]
        public string PreyrWrk { get; set; }
        [DataMember]
        public string CurryrWrk { get; set; }
        [DataMember]
        public string RemnWrk { get; set; }
        [DataMember]
        public string Waers { get; set; }
    }
   
    public class MAINACTIVITYSet
    {
        [DataMember]
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
        public string YearFg { get; set; }
        [DataMember]
        public string MinimumFg { get; set; }
        [DataMember]
        public string IntrPurchaseFg { get; set; }
        [DataMember]
        public string ObjFg { get; set; }
        [DataMember]
        public string Mandt { get; set; }
        [DataMember]
        public string MarkDel { get; set; }
        [DataMember]
        public string OutletTp { get; set; }
        [DataMember]
        public string ShareCapFg { get; set; }
        [DataMember]
        public string Crnumber { get; set; }
        [DataMember]
        public string ForgPurchaseFg { get; set; }
        [DataMember]
        public string FormGuid { get; set; }
        [DataMember]
        public string DataVersion { get; set; }
        [DataMember]
        public string MainInd { get; set; }
        [DataMember]
        public string SalesFg { get; set; }
        [DataMember]
        public string ContainerFg { get; set; }
        [DataMember]
        public int LineNo { get; set; }
        [DataMember]
        public string SubInd { get; set; }
        [DataMember]
        public string PerSalesGainFg { get; set; }
        [DataMember]

        public string RankingOrder { get; set; }
        [DataMember]
        public string ActCd { get; set; }
        [DataMember]

        public string ActDesc { get; set; }
        [DataMember]

        public string ShareCap { get; set; }
        [DataMember]

        public string IntrPurchase { get; set; }
        [DataMember]

        public string ForgPurchase { get; set; }
        [DataMember]

        public string Sales { get; set; }
        [DataMember]

        public string PerSalesGain { get; set; }
        [DataMember]

        public string Container { get; set; }
        [DataMember]

        public int LineCnt { get; set; }
        [DataMember]

        public string Waers { get; set; }
    }
   
    public class SCHGP12
    {
        [DataMember]

        public List<Result22> results { get; set; }
    }
   
   
    public class Result23
    {
        [DataMember]

        public Metadata __metadata { get; set; }
        [DataMember]

        public string YearFg { get; set; }
        [DataMember]

        public string MinimumFg { get; set; }
        [DataMember]
        public string IntrPurchaseFg { get; set; }
        [DataMember]
        public string ObjFg { get; set; }
        [DataMember]
        public string Mandt { get; set; }
        [DataMember]
        public string MarkDel { get; set; }
        [DataMember]
        public string OutletTp { get; set; }
        [DataMember]
        public string ShareCapFg { get; set; }
        [DataMember]

        public string Crnumber { get; set; }
        [DataMember]
        public string ForgPurchaseFg { get; set; }
        [DataMember]
        public string FormGuid { get; set; }
        [DataMember]
        public string DataVersion { get; set; }
        [DataMember]
        public string MainInd { get; set; }
        [DataMember]
        public string SalesFg { get; set; }
        [DataMember]
        public string ContainerFg { get; set; }
        [DataMember]
        public int LineNo { get; set; }
        [DataMember]
        public string SubInd { get; set; }
        [DataMember]
        public string PerSalesGainFg { get; set; }
        [DataMember]
        public string RankingOrder { get; set; }
        [DataMember]
        public string ActCd { get; set; }
        [DataMember]
        public string ActDesc { get; set; }
        [DataMember]
        public string ShareCap { get; set; }
        [DataMember]
        public string IntrPurchase { get; set; }
        [DataMember]
        public string ForgPurchase { get; set; }
        [DataMember]
        public string Sales { get; set; }
        [DataMember]
        public string PerSalesGain { get; set; }
        [DataMember]
        public string Container { get; set; }
        [DataMember]
        public int LineCnt { get; set; }
        [DataMember]
        public string Waers { get; set; }
    }
   
    public class SCHGP11
    {
        [DataMember]
        public List<Result23> results { get; set; }
    }
   
   
    public class Result24
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        public string CapitalFg { get; set; }
        [DataMember]
        public string YearFg { get; set; }
        [DataMember]
        public string Mandt { get; set; }
        [DataMember]
        public string MarkDel { get; set; }
        [DataMember]
        public string OutletTp { get; set; }
        [DataMember]
        public string Crnumber { get; set; }
        [DataMember]
        public string FormGuid { get; set; }
        [DataMember]
        public string IncrInCapFg { get; set; }
        [DataMember]
        public string CapitalRateFg { get; set; }
        [DataMember]
        public string DataVersion { get; set; }
        [DataMember]
        public string MainInd { get; set; }
        [DataMember]
        public int LineNo { get; set; }
        [DataMember]
        public string ProfitsFg { get; set; }
        [DataMember]
        public string SubInd { get; set; }
        [DataMember]
        public string ContainerFg { get; set; }
        [DataMember]
        public string RankingOrder { get; set; }
        [DataMember]
        public string ActCd { get; set; }
        [DataMember]
        public string ActDesc { get; set; }
        [DataMember]
        public string Capital { get; set; }
        [DataMember]
        public string IncrInCap { get; set; }
        [DataMember]
        public string CapitalRate { get; set; }
        [DataMember]
        public string Profits { get; set; }
        [DataMember]
        public string Container { get; set; }
        [DataMember]
        public int LineCnt { get; set; }
        [DataMember]
        public string Waers { get; set; }
        [DataMember]
        public string IsIncCapApplicable { get; set; }
        [DataMember]
        public bool IsApplicableVisible { get; set; }


    }
   
    public class SCHGP10
    {
        [DataMember]
        public List<Result24> results { get; set; }
    }
   
   
    public class Result25
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        public string Mandt { get; set; }
        [DataMember]
        public string YearFg { get; set; }
        [DataMember]
        public string MarkDel { get; set; }
        [DataMember]
        public string OutletTp { get; set; }
        [DataMember]
        public string RevenueFg { get; set; }
        [DataMember]
        public string Crnumber { get; set; }
        [DataMember]
        public string ExpensesFg { get; set; }
        [DataMember]
        public string FormGuid { get; set; }
        [DataMember]
        public string DataVersion { get; set; }
        [DataMember]
        public string MainInd { get; set; }
        [DataMember]
        public string NetPftFg { get; set; }
        [DataMember]
        public string ContainerFg { get; set; }
        [DataMember]
        public int LineNo { get; set; }
        [DataMember]
        public string SubInd { get; set; }
        [DataMember]
        public string RankingOrder { get; set; }
        [DataMember]
        public string SubsidyValFg { get; set; }
        [DataMember]
        public string ActCd { get; set; }
        [DataMember]
        public string ActDesc { get; set; }
        [DataMember]
        public string SubsidyVal { get; set; }
        [DataMember]
        public string Revenue { get; set; }
        [DataMember]
        public string Expenses { get; set; }
        [DataMember]
        public string NetPft { get; set; }
        [DataMember]
        public string Container { get; set; }
        [DataMember]
        public int LineCnt { get; set; }
        [DataMember]
        public string Waers { get; set; }
        [DataMember]
        public string IsExpApplicable { get; set; }
        [DataMember]
        public bool IsApplicableVisible { get; set; }


    }
   
    public class SCHGP09
    {
        [DataMember]
        public List<Result25> results { get; set; }
    }
   
   
    public class Result26
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        public string Mandt { get; set; }
        [DataMember]
        public string YearFg { get; set; }
        [DataMember]
        public string MarkDel { get; set; }
        [DataMember]
        public string OutletTp { get; set; }
        [DataMember]
        public string RoomRateFg { get; set; }
        [DataMember]
        public string AddRoomRateFg { get; set; }
        [DataMember]
        public string Crnumber { get; set; }
        [DataMember]
        public string FormGuid { get; set; }
        [DataMember]
        public string DataVersion { get; set; }
        [DataMember]
        public string MainInd { get; set; }
        [DataMember]
        public string OutputValFg { get; set; }
        [DataMember]
        public int LineNo { get; set; }
        [DataMember]
        public string RevenueFg { get; set; }
        public string SubInd { get; set; }
        [DataMember]
        public string ExpensesFg { get; set; }
        [DataMember]
        public string RankingOrder { get; set; }
        [DataMember]
        public string ActCd { get; set; }
        [DataMember]
        public string NetProfitFg { get; set; }
        [DataMember]
        public string ActDesc { get; set; }
        [DataMember]
        public string ContainerFg { get; set; }
        [DataMember]
        public string NoOfRooms { get; set; }
        [DataMember]
        public string NoOfRoomsFg { get; set; }
        [DataMember]
        public string RoomRate { get; set; }
        [DataMember]
        public string ServicFeeFg { get; set; }
        [DataMember]
        public string AddRoomRate { get; set; }
        [DataMember]
        public string OccRateFg { get; set; }
        [DataMember]
        public string ServicFee { get; set; }
        [DataMember]
        public string OccRate { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string Owned { get; set; }
        [DataMember]
        public string OutputVal { get; set; }
        [DataMember]
        public string Revenue { get; set; }
        [DataMember]
        public string Expenses { get; set; }
        [DataMember]
        public string NetProfit { get; set; }
        [DataMember]
        public string Container { get; set; }
        [DataMember]
        public int LineCnt { get; set; }
        [DataMember]
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
        public List<Result26> results { get; set; }
    }
   
   
    public class Result27
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        public string YearFg { get; set; }
        [DataMember]
        public string Mandt { get; set; }
        [DataMember]
        public string MarkDel { get; set; }
        [DataMember]
        public string OutletTp { get; set; }
        [DataMember]
        public string RevenueFg { get; set; }
        [DataMember]
        public string Crnumber { get; set; }
        [DataMember]
        public string ExpensesFg { get; set; }
        [DataMember]
        public string FormGuid { get; set; }
        [DataMember]
        public string DataVersion { get; set; }
        [DataMember]
        public string MainInd { get; set; }
        [DataMember]
        public string NetPftFg { get; set; }
        [DataMember]
        public string ContainerFg { get; set; }
        [DataMember]
        public int LineNo { get; set; }
        [DataMember]
        public string SubInd { get; set; }
        [DataMember]
        public string RankingOrder { get; set; }
        [DataMember]
        public string ActCd { get; set; }
        [DataMember]
        public string ActDesc { get; set; }
        [DataMember]
        public string Revenue { get; set; }
        [DataMember]
        public string Expenses { get; set; }
        [DataMember]
        public string NetPft { get; set; }
        [DataMember]
        public string Container { get; set; }
        [DataMember]
        public int LineCnt { get; set; }
        [DataMember]
        public string Waers { get; set; }
        [DataMember]
        public string IsApplicable { get; set; }
        [DataMember]
        public bool IsApplicableVisible { get; set; }


    }
   
    public class SCHGP02
    {
        [DataMember]
        public List<Result27> results { get; set; }
    }

    public class Result28
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        public string AgvDlyIncomeFg { get; set; }
        [DataMember]
        public string YearFg { get; set; }
        [DataMember]
        public string Mandt { get; set; }
        [DataMember]
        public string MarkDel { get; set; }
        [DataMember]
        public string OutletTp { get; set; }
        [DataMember]
        public string OutputValFg { get; set; }
        [DataMember]
        public string Crnumber { get; set; }
        [DataMember]
        public string NoOfCars { get; set; }
        [DataMember]
        public string RevenueFg { get; set; }
        [DataMember]
        public string ExpensesFg { get; set; }
        [DataMember]
        public string FormGuid { get; set; }
        [DataMember]
        public string MainInd { get; set; }
        [DataMember]
        public string DataVersion { get; set; }
        [DataMember]
        public string NetPftFg { get; set; }
        [DataMember]
        public string SubInd { get; set; }
        [DataMember]
        public string ContainerFg { get; set; }
        [DataMember]
        public int LineNo { get; set; }
        [DataMember]
        public string NoOfCarsFg { get; set; }
        [DataMember]
        public string RankingOrder { get; set; }
        [DataMember]
        public string ActCd { get; set; }
        [DataMember]
        public string OccRateFg { get; set; }
        [DataMember]
        public string ActDesc { get; set; }
        [DataMember]
        public string NetPftPerFg { get; set; }
        [DataMember]
        public string AgvDlyIncome { get; set; }
        [DataMember]
        public string OccRate { get; set; }
        [DataMember]
        public string NetPftPer { get; set; }
        [DataMember]
        public string OutputVal { get; set; }
        [DataMember]
        public string Revenue { get; set; }
        [DataMember]
        public string Expenses { get; set; }
        [DataMember]
        public string NetPft { get; set; }
        [DataMember]
        public string Container { get; set; }
        [DataMember]
        public int LineCnt { get; set; }
        [DataMember]
        public string Waers { get; set; }
        [DataMember]
        public string IsApplicable { get; set; }
        [DataMember]
        public bool IsApplicableVisible { get; set; }

    }
   
    public class SCHGP01
    {
        [DataMember]
        public List<Result28> results { get; set; }
    }

    public class Result29
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        public string MarkDel { get; set; }
        [DataMember]
        public string Delflag { get; set; }
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
        public string GrpNm { get; set; }
        [DataMember]
        public int LineCnt { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Amount { get; set; }
        [DataMember]
        public string Waers { get; set; }
    }
   
    public class GENSUBSCH
    {
        [DataMember]
        public List<Result29> results { get; set; }
    }
   
    public class ZakatForm5DataResult
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        public string AttenbChk { get; set; }
        [DataMember]
        public string LibChk { get; set; }
        [DataMember]
        public string SelfAmd { get; set; }
        [DataMember]
        public string AAccExam { get; set; }
        [DataMember]
        public string APayAmtTp { get; set; }
        [DataMember]
        public string Govcodfg { get; set; }
        [DataMember]
        public string Gp71112fg { get; set; }
        [DataMember]
        public string Instfg { get; set; }
        [DataMember]
        public string Msgfg { get; set; }
        [DataMember]
        public string Msgtx { get; set; }
        [DataMember]
        public string AMobile { get; set; }
        [DataMember]
        public string AActmethod { get; set; }
        [DataMember]
        public string AAdj { get; set; }
        [DataMember]
        public string AAgree { get; set; }
        [DataMember]
        public object AAgreeDt { get; set; }
        [DataMember]
        public string AAnnSalesrevAmt { get; set; }
        [DataMember]
        public string AAnnSalesrevOu { get; set; }
        [DataMember]
        public string AAnnSalesrevPr { get; set; }
        [DataMember]
        public string AAnnSalesrevTot { get; set; }
        [DataMember]
        public string AAnnSalesrevTotFg { get; set; }
        [DataMember]
        public string AAnnualRent { get; set; }
        [DataMember]
        public string AAnnualRentFg { get; set; }
        [DataMember]
        public string AAppAdopTp { get; set; }
        [DataMember]
        public string AArzaqIPer { get; set; }
        [DataMember]
        public string ABondsApp { get; set; }
        [DataMember]
        public string ABulding { get; set; }
        [DataMember]
        public string ACalendarTp { get; set; }
        [DataMember]
        public string ACapital { get; set; }
        [DataMember]
        public string ACapitalFg { get; set; }
        [DataMember]
        public string ACapitalOu { get; set; }
        [DataMember]
        public string ACdNm { get; set; }
        [DataMember]
        public string ACheckFg { get; set; }
        [DataMember]
        public string ACity { get; set; }
        [DataMember]
        public string ACompNm { get; set; }
        [DataMember]
        public string AConrevAmt { get; set; }
        [DataMember]
        public string AConrevAmtChk { get; set; }
        [DataMember]
        public string AConrevOu { get; set; }
        [DataMember]
        public string AConrevPr { get; set; }
        [DataMember]
        public string AConrevTot { get; set; }
        [DataMember]
        public string ADistrict { get; set; }
        [DataMember]
        public string AEmail { get; set; }
        [DataMember]
        public string AExamMngr { get; set; }
        [DataMember]
        public string AFaxNo { get; set; }
        [DataMember]
        public string AFiscalCalendar { get; set; }
        [DataMember]
        public string AFiscalPeriod { get; set; }
        [DataMember]
        public DateTime AFromDt { get; set; }
        [DataMember]
        public string AFromDtCh { get; set; }
        [DataMember]
        public string AFromDtTp { get; set; }
        [DataMember]
        public string AGenTradeAmt { get; set; }
        [DataMember]
        public string AGentradeIPer { get; set; }
        [DataMember]
        public string AGenTradeOu { get; set; }
        [DataMember]
        public string AGenTradePr { get; set; }
        [DataMember]
        public string AGenTradeTot { get; set; }
        [DataMember]
        public string ALegalSt { get; set; }
        [DataMember]
        public string ALivelihoodsAmt { get; set; }
        [DataMember]
        public string ALivelihoodsOu { get; set; }
        [DataMember]
        public string ALivelihoodsPr { get; set; }
        [DataMember]
        public string ALivelihoodsTot { get; set; }
        [DataMember]
        public string ALivestkIPer { get; set; }
        [DataMember]
        public string ALivestkVegtbAmt { get; set; }
        [DataMember]
        public string ALivestkVegtbOu { get; set; }
        [DataMember]
        public string ALivestkVegtbPr { get; set; }
        [DataMember]
        public string ALivestkVegtbTot { get; set; }
        [DataMember]
        public string AMainact { get; set; }
        [DataMember]
        public string AMainAct { get; set; }
        [DataMember]
        public string AMainActDesc { get; set; }
        [DataMember]
        public string AMangmentAudit { get; set; }
        [DataMember]
        public string AmdRsnz { get; set; }
        [DataMember]
        public string AName { get; set; }
        [DataMember]
        public string ANetIncomeOthActvAmt { get; set; }
        [DataMember]
        public string ANetIncomeOthActvAmtChk { get; set; }
        [DataMember]
        public string ANetIncomeOthActvOu { get; set; }
        [DataMember]
        public string ANetIncomeOthActvPr { get; set; }
        [DataMember]
        public string ANetIncomeOthActvTot { get; set; }
        [DataMember]
        public string ANetTaxableAmount { get; set; }
        [DataMember]
        public string ANetTaxableAmountOu { get; set; }
        [DataMember]
        public string ANoBranches { get; set; }
        [DataMember]
        public string ANoEmp { get; set; }
        [DataMember]
        public string ANoEmpFg { get; set; }
        [DataMember]
        public string ANoFinance { get; set; }
        [DataMember]
        public string ANonsaudiCaSh { get; set; }
        [DataMember]
        public string ANonsaudiPrSh { get; set; }
        [DataMember]
        public string ANoOfOutlet { get; set; }
        [DataMember]
        public string AOthincAmt { get; set; }
        [DataMember]
        public string AOthincOs { get; set; }
        [DataMember]
        public string AOthincPr { get; set; }
        [DataMember]
        public string AOthincTot { get; set; }
        [DataMember]
        public string AOthincTotFg { get; set; }
        [DataMember]
        public string AOwnerNmQuat { get; set; }
        [DataMember]
        public string APdRecptAmt { get; set; }
        [DataMember]
        public string APdRecptAmtChk { get; set; }
        [DataMember]
        public string APdRecptOu { get; set; }
        [DataMember]
        public string APlShare { get; set; }
        [DataMember]
        public string APlShareChk { get; set; }
        [DataMember]
        public string APlShareFg { get; set; }
        [DataMember]
        public string APlShareOu { get; set; }
        [DataMember]
        public string APoBox { get; set; }
        [DataMember]
        public string Approvez { get; set; }
        [DataMember]
        public string APrctr { get; set; }
        [DataMember]
        public string AReleaseOfContract { get; set; }
        [DataMember]
        public string AReleaseOfContractChk { get; set; }
        [DataMember]
        public string AReleaseOfContractOu { get; set; }
        [DataMember]
        public string AResidency { get; set; }
        [DataMember]
        public string ARestpaidAmt { get; set; }
        [DataMember]
        public string ARestpaidAmtOu { get; set; }
        [DataMember]
        public string ASaudiCaSh { get; set; }
        [DataMember]
        public string ASaudiPrSh { get; set; }
        [DataMember]
        public string ASeal { get; set; }
        [DataMember]
        public string ASign { get; set; }
        [DataMember]
        public string ASitedocs { get; set; }
        [DataMember]
        public int AStep { get; set; }
        [DataMember]
        public string AStreet { get; set; }
        [DataMember]
        public string ATelephone { get; set; }
        [DataMember]
        public DateTime AToDt { get; set; }
        [DataMember]
        public string AToDtCh { get; set; }
        [DataMember]
        public string AToDtTp { get; set; }
        [DataMember]
        public string ATotAnnualSal { get; set; }
        [DataMember]
        public string ATotAnnualSalFg { get; set; }
        [DataMember]
        public string ATotOwedpaid { get; set; }
        [DataMember]
        public string ATotOwedpaidOu { get; set; }
        [DataMember]
        public string ATotProfit { get; set; }
        [DataMember]
        public string ATotProfitOu { get; set; }
        [DataMember]
        public string Auditorz { get; set; }
        [DataMember]
        public string Augrp { get; set; }
        [DataMember]
        public string AZakat { get; set; }
        [DataMember]
        public string AZakat51 { get; set; }
        [DataMember]
        public string AZakat51Ou { get; set; }
        [DataMember]
        public string AZakatOu { get; set; }
        [DataMember]
        public string AZakatPrevYr { get; set; }
        [DataMember]
        public string AZakatPrevYrFg { get; set; }
        [DataMember]
        public string AZakatPrevYrOu { get; set; }
        [DataMember]
        public string AZipCd { get; set; }
        [DataMember]
        public string Bpkind { get; set; }
        [DataMember]
        public string Cal { get; set; }
        [DataMember]
        public string CaseGuid { get; set; }
        [DataMember]
        public string CreateTxAssesz { get; set; }
        [DataMember]
        public string Dmodez { get; set; }
        [DataMember]
        public string Euser { get; set; }
        [DataMember]
        public string Fbguid { get; set; }
        [DataMember]
        public string Fbguid1 { get; set; }
        [DataMember]
        public string Fbnum { get; set; }
        [DataMember]
        public string Fbnumz { get; set; }
        [DataMember]
        public string FormGuid { get; set; }
        [DataMember]
        public string Formtype { get; set; }
        [DataMember]
        public string ForUser { get; set; }
        [DataMember]
        public string Forward { get; set; }
        [DataMember]
        public string Fturn { get; set; }
        [DataMember]
        public string Gp0368fg { get; set; }
        [DataMember]
        public string InChannel { get; set; }
        [DataMember]
        public string Incotyp { get; set; }
        [DataMember]
        public string Isamend { get; set; }
        [DataMember]
        public string Langz { get; set; }
        [DataMember]
        public string LegacyDocNo { get; set; }
        [DataMember]
        public string Line0 { get; set; }
        [DataMember]
        public string Line1 { get; set; }
        [DataMember]
        public string Line2 { get; set; }
        [DataMember]
        public string Line3 { get; set; }
        [DataMember]
        public string Line4 { get; set; }
        [DataMember]
        public string Line5 { get; set; }
        [DataMember]
        public string Line6 { get; set; }
        [DataMember]
        public string Line7 { get; set; }
        [DataMember]
        public string Line8 { get; set; }
        [DataMember]
        public string Line9 { get; set; }
        [DataMember]
        public string Mode { get; set; }
        [DataMember]
        public string Monthz { get; set; }
        [DataMember]
        public string ObjSubmitz { get; set; }
        [DataMember]
        public string Ofbnum { get; set; }
        [DataMember]
        public string OfficerUidz { get; set; }
        [DataMember]

        public string Omode { get; set; }
        [DataMember]
        public string PeriodKeyz { get; set; }
        [DataMember]
        public string PerslText { get; set; }
        [DataMember]
        public string PortalUsrz { get; set; }
        [DataMember]
        public string RegIdz { get; set; }
        [DataMember]
        public string Rejectz { get; set; }
        [DataMember]
        public string Retguid { get; set; }
        [DataMember]
        public string Savez { get; set; }
        [DataMember]
        public string SavNot { get; set; }
        [DataMember]
        public string SkipBillingz { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string Submitz { get; set; }
        [DataMember]
        public string Taxpayerz { get; set; }
        [DataMember]
        public string UserType { get; set; }
        [DataMember]
        public string Xvoidz { get; set; }
        [DataMember]
        public GP038Set GP03_8Set { get; set; }
        [DataMember]
        public GP037Set GP03_7Set { get; set; }
        [DataMember]
        public SCH200Set SCH_200Set { get; set; }
        [DataMember]
        public SCH800Set SCH_800Set { get; set; }
        [DataMember]
        public SCHGP07 SCH_GP07 { get; set; }
        [DataMember]
        public SCHGP06 SCH_GP06 { get; set; }
        [DataMember]
        public SCHGP05 SCH_GP05 { get; set; }
        [DataMember]
        public SCHGP04 SCH_GP04 { get; set; }

        [DataMember]
        public SCHGP03 SCH_GP03 { get; set; }
        [DataMember]
        public SCHGP3S2Set SCH_GP3S2Set { get; set; }
        [DataMember]
        public SCHGP3S1Set SCH_GP3S1Set { get; set; }
        [DataMember]
        public GP036Set GP03_6Set { get; set; }
        [DataMember]
        public AttDetSet AttDetSet { get; set; }
        [DataMember]
        public SUBSCHCAPITALSet SUB_SCH_CAPITALSet { get; set; }
        [DataMember]
        public GP034Set GP03_4Set { get; set; }
        [DataMember]
        public GP035Set GP03_5Set { get; set; }
        [DataMember]
        public GP063Set GP06_3Set { get; set; }
        [DataMember]
        public GP032Set GP03_2Set { get; set; }
        [DataMember]
        public GP061Set GP06_1Set { get; set; }
        [DataMember]
        public GP062Set GP06_2Set { get; set; }
        [DataMember]
        public GP033Set GP03_3Set { get; set; }
        [DataMember]
        public MAINACTIVITYSet MAIN_ACTIVITYSet { get; set; }
        [DataMember]
        public LONGTEXTSet LONG_TEXTSet { get; set; }
        [DataMember]
        public SCHGP12 SCH_GP12 { get; set; }
        [DataMember]
        public SCHGP11 SCH_GP11 { get; set; }
        [DataMember]
        public SCHGP10 SCH_GP10 { get; set; }
        [DataMember]
        public SCHGP09 SCH_GP09 { get; set; }
        [DataMember]
        public SCHGP08 SCH_GP08 { get; set; }
        [DataMember]
        public SCHGP02 SCH_GP02 { get; set; }
        [DataMember]
        public SCHGP01 SCH_GP01 { get; set; }
        [DataMember]
        public GENSUBSCH GEN_SUB_SCH { get; set; }

        [DataMember]
        public string IsOtherShareApplicable { get; set; }
        [DataMember]
        public bool IsOtherShareApplicableVisible { get; set; }

    }
}