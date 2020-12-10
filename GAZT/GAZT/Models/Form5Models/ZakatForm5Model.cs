using System;
using System.Collections.Generic;
using Xamarin.Forms.Internals;

namespace ZakatForm5Model
{
    [Preserve(AllMembers = true)]
    public partial class ZakatForm5Data
    {
        public ZakatForm5DataResult d { get; set; }
    }


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
        public string Mandt { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public int LineNo { get; set; }
        public string RankingOrder { get; set; }
        public string ActCd { get; set; }
        public string ActDesc { get; set; }
        public int LineCnt { get; set; }
        public string Description { get; set; }
        public string SubDesc { get; set; }
        public string Value { get; set; }
        public string ProfitRatio { get; set; }
        public string Profit { get; set; }
        public string Waers { get; set; }
        public string ValueFg { get; set; }
        public string ProfitFg { get; set; }
        public string Flag { get; set; }
        public string MarkDel { get; set; }
        public string Delflag { get; set; }
    }

    public class GP038Set
    {
        public List<Result> results { get; set; }
    }

    public class Metadata3
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class Results2
    {
        public Metadata3 __metadata { get; set; }
        public string Mandt { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public int LineNo { get; set; }
        public string RankingOrder { get; set; }
        public string ActCd { get; set; }
        public string ActDesc { get; set; }
        public int LineCnt { get; set; }
        public string Description { get; set; }
        public string SubDescription { get; set; }
        public string Value { get; set; }
        public string ProfitRatio { get; set; }
        public string Profit { get; set; }
        public string Waers { get; set; }
        public string ValueFg { get; set; }
        public string ProfitFg { get; set; }
        public string Flag { get; set; }
        public string MarkDel { get; set; }
        public string Delflag { get; set; }
    }

    public class GP037Set
    {
        public List<Results2> results { get; set; }
    }

    public class Metadata4
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class Results3
    {
        public Metadata4 __metadata { get; set; }
        public string Mandt { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public int LineNo { get; set; }
        public string RankingOrder { get; set; }
        public string Tin { get; set; }
        public string Name { get; set; }
        public string Amount { get; set; }
        public string Waers { get; set; }
    }

    public class SCH200Set
    {
        public List<Results3> results { get; set; }
    }

    public class Metadata5
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class Results4
    {
        public Metadata5 __metadata { get; set; }
        public string Mandt { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public int LineNo { get; set; }
        public string RankingOrder { get; set; }
        public string ContractNo { get; set; }
        public string Fbnum { get; set; }
        public string ZakatPaid { get; set; }
        public string Waers { get; set; }
    }

    public class SCH800Set
    {
        public List<Results4> results { get; set; }
    }

    public class Metadata6
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class Results5
    {
        public Metadata6 __metadata { get; set; }
        public string Mandt { get; set; }
        public string MinimumFg { get; set; }
        public string YearFg { get; set; }
        public string ObjFg { get; set; }
        public string MarkDel { get; set; }
        public string OutletTp { get; set; }
        public string RevenueFg { get; set; }
        public string Crnumber { get; set; }
        public string ExpensesFg { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public string MainInd { get; set; }
        public string NetPftFg { get; set; }
        public string ContainerFg { get; set; }
        public int LineNo { get; set; }
        public string SubInd { get; set; }
        public string RankingOrder { get; set; }
        public string ActCd { get; set; }
        public string ActDesc { get; set; }
        public string Revenue { get; set; }
        public string Expenses { get; set; }
        public string City { get; set; }
        public string NetPft { get; set; }
        public string Container { get; set; }
        public int LineCnt { get; set; }
        public string Waers { get; set; }
        public string IsExpencesApplicable { get; set; }

        public bool IsApplicableVisible { get; set; }


    }

    public class SCHGP07
    {
        public List<Results5> results { get; set; }
    }

    public class Metadata7
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class Results6
    {
        public Metadata7 __metadata { get; set; }
        public string Capital { get; set; }
        public string GovtContractProfFg { get; set; }
        public string YearFg { get; set; }
        public string Mandt { get; set; }
        public string MarkDel { get; set; }
        public string OutletTp { get; set; }
        public string CivilContrRevFg { get; set; }
        public string Crnumber { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public string MainInd { get; set; }
        public string OtherIncomeFg { get; set; }
        public int LineNo { get; set; }
        public string SubInd { get; set; }
        public string TotalProfitFg { get; set; }
        public string ContainerFg { get; set; }
        public string RankingOrder { get; set; }
        public string ActCd { get; set; }
        public string NoOfLabourFg { get; set; }
        public string ActDesc { get; set; }
        public int GovtContCnt { get; set; }
        public string GovtContractProf { get; set; }
        public int CivilContCnt { get; set; }
        public string CivilContrRev { get; set; }
        public int OthIncCnt { get; set; }
        public string OtherIncome { get; set; }
        public string TotalProfit { get; set; }
        public string NoOfLabours { get; set; }
        public string Container { get; set; }
        public int LineCnt { get; set; }
        public string Waers { get; set; }

        public string IsGovenmentApplicable { get; set; }
        public bool IsGovenmentApplicableVisible { get; set; }

        public string IsCivilProfitApplicable { get; set; }
        public bool IsCivilProfitApplicableVisible { get; set; }

        public string IsOtherProfitApplicable { get; set; }
        public bool IsOtherProfitApplicableVisible { get; set; }





    }

    public class SCHGP06
    {
        public List<Results6> results { get; set; }
    }

    public class Metadata8
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class Results7
    {
        public Metadata8 __metadata { get; set; }
        public string YearFg { get; set; }
        public string FundingTotFg { get; set; }
        public string Mandt { get; set; }
        public string MarkDel { get; set; }
        public string OutletTp { get; set; }
        public string CapitalStrFg { get; set; }
        public string Crnumber { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public string MainInd { get; set; }
        public string NetCapitalFg { get; set; }
        public int LineNo { get; set; }
        public string SalesFg { get; set; }
        public string SubInd { get; set; }
        public string ProfitFg { get; set; }
        public string RankingOrder { get; set; }
        public string ActCd { get; set; }
        public string StatutoryPftFg { get; set; }
        public string ActDesc { get; set; }
        public string ContainerFg { get; set; }
        public string FundingTot { get; set; }
        public string CapitalStr { get; set; }
        public string NetCapital { get; set; }
        public string Sales { get; set; }
        public string Profit { get; set; }
        public string StatutoryPft { get; set; }
        public string Container { get; set; }
        public int LineCnt { get; set; }
        public string Waers { get; set; }

    }

    public class SCHGP05
    {
        public List<Results7> results { get; set; }
    }

    public class Metadata9
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class Results8
    {
        public Metadata9 __metadata { get; set; }
        public string YearFg { get; set; }
        public string Mandt { get; set; }
        public string MarkDel { get; set; }
        public string OutletTp { get; set; }
        public string RevenueFg { get; set; }
        public string Crnumber { get; set; }
        public string ExpensesFg { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public string MainInd { get; set; }
        public string NetPftFg { get; set; }
        public string ContainerFg { get; set; }
        public int LineNo { get; set; }
        public string SubInd { get; set; }
        public string NoOfLabourFg { get; set; }
        public string RankingOrder { get; set; }
        public string ActCd { get; set; }
        public string ActDesc { get; set; }
        public string Revenue { get; set; }
        public string Expenses { get; set; }
        public string NetProfit { get; set; }
        public string NoOfLabour { get; set; }
        public string Container { get; set; }
        public int LineCnt { get; set; }
        public string Waers { get; set; }

        public string IsExpensesApplicable { get; set; }
        public bool IsApplicableVisible { get; set; }


    }

    public class SCHGP04
    {
        public List<Results8> results { get; set; }
    }

    public class Metadata10
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class Results9
    {
        public Metadata10 __metadata { get; set; }
        public int GenTradeICnt { get; set; }
        public string GenTradeIFg { get; set; }
        public string YearFg { get; set; }
        public string LivelihoodsIFg { get; set; }
        public string Mandt { get; set; }
        public string GenTradeI { get; set; }
        public string LiveStkAnimalsIFg { get; set; }
        public string MarkDel { get; set; }
        public int ArzaqICnt { get; set; }
        public string OutletTp { get; set; }
        public string LivelihoodsI { get; set; }
        public string SuppConFg { get; set; }
        public string Crnumber { get; set; }
        public int LiveStkICnt { get; set; }
        public string FormGuid { get; set; }
        public string LiveStkAnimalsI { get; set; }
        public string SalesFg { get; set; }
        public string CapitalFg { get; set; }
        public string DataVersion { get; set; }
        public string MainInd { get; set; }
        public string ContainerFg { get; set; }
        public int LineNo { get; set; }
        public string SubInd { get; set; }
        public string GenTradeFg { get; set; }
        public string RankingOrder { get; set; }
        public string ActCd { get; set; }
        public string LivelihoodsFg { get; set; }
        public string ActDesc { get; set; }
        public string LiveStkAnimalsFg { get; set; }
        public string ExtnProcFg { get; set; }
        public int LineCnt { get; set; }
        public string IntnProcFg { get; set; }
        public string SuppCon { get; set; }
        public string ProfitRatio { get; set; }
        public string TotalFg { get; set; }
        public string ProfitRatioFg { get; set; }
        public string Sales { get; set; }
        public string SalePrfRatio { get; set; }
        public string SalePrfRatioFg { get; set; }
        public string Capital { get; set; }
        public string Container { get; set; }
        public int GenTradeCnt { get; set; }
        public string GenTrade { get; set; }
        public int ArzaqCnt { get; set; }
        public string Livelihoods { get; set; }
        public int LiveStkCnt { get; set; }
        public string LiveStkAnimals { get; set; }
        public string ExtnProc { get; set; }
        public int IntnProcCnt { get; set; }
        public string IntnProc { get; set; }
        public string Total { get; set; }
        public string Waers { get; set; }

        public string IsApplicable { get; set; }
        public bool IsApplicableVisible { get; set; }

        public string IsImport { get; set; }
        public string IsProcurement { get; set; }

        
        public string Imp_IsGeneralApplicable { get; set; }
        public bool Imp_IsGeneralApplicableVisible { get; set; }

        public string Imp_IsLiveLihoodsApplicable { get; set; }
        public bool Imp_IsLiveLihoodsApplicableVisible { get; set; }

        public string Imp_IsLivestockApplicable { get; set; }
        public bool Imp_IsLivestockApplicableVisible { get; set; }



        public string Pro_IsGeneralApplicable { get; set; }
        public bool Pro_IsGeneralApplicableVisible { get; set; }

        public string Pro_IsLiveLihoodsApplicable { get; set; }
        public bool Pro_IsLiveLihoodsApplicableVisible { get; set; }

        public string Pro_IsLivestockApplicable { get; set; }
        public bool Pro_IsLivestockApplicableVisible { get; set; }



        public bool IsImportVisible { get; set; }
        public bool IsProcurementVisible { get; set; }




    }

    public class SCHGP03
    {
        public List<Results9> results { get; set; }
    }

    public class Metadata11
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class Results10
    {
        public Metadata11 __metadata { get; set; }
        public string Mandt { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public int LineNo { get; set; }
        public string RankingOrder { get; set; }
        public string Persl { get; set; }
        public string PerslDesc { get; set; }
        public string CapitalMci { get; set; }
        public string Capital { get; set; }
        public string CapitalAcc { get; set; }
        public string CapitalGain { get; set; }
        public string MaxCapital { get; set; }
    }

    public class SCHGP3S2Set
    {
        public List<Results10> results { get; set; }
    }

    public class Metadata12
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class Results11
    {
        public Metadata12 __metadata { get; set; }
        public string Mandt { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public int LineNo { get; set; }
        public string RankingOrder { get; set; }
        public string Persl { get; set; }
        public string PerslDesc { get; set; }
        public string ImportTot { get; set; }
        public string Import3rdParty { get; set; }
        public string HigherImport { get; set; }
        public string IntnProc { get; set; }
        public string TotImpIntn { get; set; }
        public string CapitalGain { get; set; }
    }

    public class SCHGP3S1Set
    {
        public List<Results11> results { get; set; }
    }

    public class Metadata13
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class Results12
    {
        public Metadata13 __metadata { get; set; }
        public string Mandt { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public int LineNo { get; set; }
        public string RankingOrder { get; set; }
        public string ActCd { get; set; }
        public string ActDesc { get; set; }
        public int LineCnt { get; set; }
        public string Description { get; set; }
        public string SubDesc { get; set; }
        public string Value { get; set; }
        public string ProfitRatio { get; set; }
        public string Profit { get; set; }
        public string Waers { get; set; }
        public string ValueFg { get; set; }
        public string ProfitFg { get; set; }
        public string Flag { get; set; }
        public string MarkDel { get; set; }
        public string Delflag { get; set; }
    }

    public class GP036Set
    {
        public List<Results12> results { get; set; }
    }

    public class AttDetSet
    {
        public List<object> results { get; set; }
    }

    public class Metadata14
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class Results13
    {
        public Metadata14 __metadata { get; set; }
        public string MarkDel { get; set; }
        public string Delflag { get; set; }
        public string LoanDtTp { get; set; }
        public object LoanDt { get; set; }
        public string ApprovalDtTp { get; set; }
        public string Mandt { get; set; }
        public object ApprovalDt { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public int LineNo { get; set; }
        public string RankingOrder { get; set; }
        public string ActCd { get; set; }
        public string ActDesc { get; set; }
        public int LineCnt { get; set; }
        public string Description { get; set; }
        public string LoanVal { get; set; }
        public string NumberOfYr { get; set; }
        public string IncrCapital { get; set; }
        public string Waers { get; set; }
    }

    public class SUBSCHCAPITALSet
    {
        public List<Results13> results { get; set; }
    }

    public class Metadata15
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class Results14
    {
        public Metadata15 __metadata { get; set; }
        public string MarkDel { get; set; }
        public string Delflag { get; set; }
        public string FormGuid { get; set; }
        public string Flag { get; set; }
        public string DataVersion { get; set; }
        public int LineNo { get; set; }
        public string RankingOrder { get; set; }
        public string ActCd { get; set; }
        public string ActDesc { get; set; }
        public int LineCnt { get; set; }
        public string Description { get; set; }
        public string SubDesc { get; set; }
        public string Value { get; set; }
        public string ProfitRatio { get; set; }
        public string Profit { get; set; }
        public string Waers { get; set; }
    }

    public class GP034Set
    {
        public List<Results14> results { get; set; }
    }

    public class Metadata16
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class Result15
    {
        public Metadata16 __metadata { get; set; }
        public string MarkDel { get; set; }
        public string Delflag { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public int LineNo { get; set; }
        public string RankingOrder { get; set; }
        public string ActCd { get; set; }
        public string ActDesc { get; set; }
        public int LineCnt { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public string ProfitRatio { get; set; }
        public string Profit { get; set; }
        public string Waers { get; set; }
    }

    public class GP035Set
    {
        public List<Result15> results { get; set; }
    }

    public class Metadata17
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class Result16
    {
        public Metadata17 __metadata { get; set; }
        public string MarkDel { get; set; }
        public string Delflag { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public int LineNo { get; set; }
        public string RankingOrder { get; set; }
        public string ActCd { get; set; }
        public string ActDesc { get; set; }
        public int LineCnt { get; set; }
        public string Description { get; set; }
        public string ContractVal { get; set; }
        public string StipendContr { get; set; }
        public string PrfOfConrtact { get; set; }
        public string Waers { get; set; }
    }

    public class GP063Set
    {
        public List<Result16> results { get; set; }
    }

    public class Metadata18
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class Result17
    {
        public Metadata18 __metadata { get; set; }
        public string MarkDel { get; set; }
        public string Delflag { get; set; }
        public string Flag { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public int LineNo { get; set; }
        public string RankingOrder { get; set; }
        public string ActCd { get; set; }
        public string ActDesc { get; set; }
        public int LineCnt { get; set; }
        public string Description { get; set; }
        public string SubDesc { get; set; }
        public string Value { get; set; }
        public string ProfitRatio { get; set; }
        public string Profit { get; set; }
        public string Waers { get; set; }
    }

    public class GP032Set
    {
        public List<Result17> results { get; set; }
    }

    public class Metadata19
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class Result18
    {
        public Metadata19 __metadata { get; set; }
        public string MarkDel { get; set; }
        public string RecordNo { get; set; }
        public string ContractValFg { get; set; }
        public string GovCode { get; set; }
        public string Delflag { get; set; }
        public string FormGuid { get; set; }
        public string StipendContrFg { get; set; }
        public string DataVersion { get; set; }
        public string PrfOfConrtactFg { get; set; }
        public int LineNo { get; set; }
        public string Flag { get; set; }
        public string RankingOrder { get; set; }
        public string ActCd { get; set; }
        public string ActDesc { get; set; }
        public int LineCnt { get; set; }
        public string Description { get; set; }
        public string ContractVal { get; set; }
        public string StipendContr { get; set; }
        public string PrfOfConrtact { get; set; }
        public string Waers { get; set; }
    }

    public class GP061Set
    {
        public List<Result18> results { get; set; }
    }

    public class Metadata20
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class Result19
    {
        public Metadata20 __metadata { get; set; }
        public string FormGuid { get; set; }
        public string Delflag { get; set; }
        public string TincrnoInd { get; set; }
        public string RecordNo { get; set; }
        public string Tin { get; set; }
        public string MarkDel { get; set; }
        public string ContractingName { get; set; }
        public string DataVersion { get; set; }
        public string MocContractor { get; set; }
        public int LineNo { get; set; }
        public string ContractValFg { get; set; }
        public string RankingOrder { get; set; }
        public string StipendContrFg { get; set; }
        public string ActCd { get; set; }
        public string PrfOfConrtactFg { get; set; }
        public string ActDesc { get; set; }
        public string Flag { get; set; }
        public int LineCnt { get; set; }
        public string Description { get; set; }
        public string ContractVal { get; set; }
        public string StipendContr { get; set; }
        public string PrfOfConrtact { get; set; }
        public string Waers { get; set; }
    }

    public class GP062Set
    {
        public List<Result19> results { get; set; }
    }

    public class Metadata21
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class Result20
    {
        public Metadata21 __metadata { get; set; }
        public string MarkDel { get; set; }
        public string Delflag { get; set; }
        public string Flag { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public int LineNo { get; set; }
        public string RankingOrder { get; set; }
        public string ActCd { get; set; }
        public string ActDesc { get; set; }
        public int LineCnt { get; set; }
        public string Description { get; set; }
        public string SubDescription { get; set; }
        public string Value { get; set; }
        public string ProfitRatio { get; set; }
        public string Profit { get; set; }
        public string Waers { get; set; }
    }

    public class GP033Set
    {
        public List<Result20> results { get; set; }
    }

    public class Metadata22
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class Result21
    {
        public Metadata22 __metadata { get; set; }
        public string ContractDtTp { get; set; }
        public string OriginalValFg { get; set; }
        public string ContractDur { get; set; }
        public string AdjustContFg { get; set; }
        public string Mandt { get; set; }
        public string PreyrWrkFg { get; set; }
        public string CurryrWrkFg { get; set; }
        public string FormGuid { get; set; }
        public string RemnWrkFg { get; set; }
        public string DataVersion { get; set; }
        public string ContractPartyInd { get; set; }
        public int LineNo { get; set; }
        public string ContractSubInd { get; set; }
        public string RankingOrder { get; set; }
        public string ContractDtInd { get; set; }
        public string ContractParty { get; set; }
        public string ContractDurInd { get; set; }
        public string ContractSub { get; set; }
        public string OriginalValInd { get; set; }
        public object ContractDt { get; set; }
        public string OriginalVal { get; set; }
        public string AdjustCont { get; set; }
        public string PreyrWrk { get; set; }
        public string CurryrWrk { get; set; }
        public string RemnWrk { get; set; }
        public string Waers { get; set; }
    }

    public class MAINACTIVITYSet
    {
        public List<Result21> results { get; set; }
    }

    public class LONGTEXTSet
    {
        public List<object> results { get; set; }
    }

    public class Metadata23
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class Result22
    {
        public Metadata23 __metadata { get; set; }
        public string YearFg { get; set; }
        public string MinimumFg { get; set; }
        public string IntrPurchaseFg { get; set; }
        public string ObjFg { get; set; }
        public string Mandt { get; set; }
        public string MarkDel { get; set; }
        public string OutletTp { get; set; }
        public string ShareCapFg { get; set; }
        public string Crnumber { get; set; }
        public string ForgPurchaseFg { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public string MainInd { get; set; }
        public string SalesFg { get; set; }
        public string ContainerFg { get; set; }
        public int LineNo { get; set; }
        public string SubInd { get; set; }
        public string PerSalesGainFg { get; set; }
        public string RankingOrder { get; set; }
        public string ActCd { get; set; }
        public string ActDesc { get; set; }
        public string ShareCap { get; set; }
        public string IntrPurchase { get; set; }
        public string ForgPurchase { get; set; }
        public string Sales { get; set; }
        public string PerSalesGain { get; set; }
        public string Container { get; set; }
        public int LineCnt { get; set; }
        public string Waers { get; set; }
    }

    public class SCHGP12
    {
        public List<Result22> results { get; set; }
    }

    public class Metadata24
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class Result23
    {
        public Metadata24 __metadata { get; set; }
        public string YearFg { get; set; }
        public string MinimumFg { get; set; }
        public string IntrPurchaseFg { get; set; }
        public string ObjFg { get; set; }
        public string Mandt { get; set; }
        public string MarkDel { get; set; }
        public string OutletTp { get; set; }
        public string ShareCapFg { get; set; }
        public string Crnumber { get; set; }
        public string ForgPurchaseFg { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public string MainInd { get; set; }
        public string SalesFg { get; set; }
        public string ContainerFg { get; set; }
        public int LineNo { get; set; }
        public string SubInd { get; set; }
        public string PerSalesGainFg { get; set; }
        public string RankingOrder { get; set; }
        public string ActCd { get; set; }
        public string ActDesc { get; set; }
        public string ShareCap { get; set; }
        public string IntrPurchase { get; set; }
        public string ForgPurchase { get; set; }
        public string Sales { get; set; }
        public string PerSalesGain { get; set; }
        public string Container { get; set; }
        public int LineCnt { get; set; }
        public string Waers { get; set; }
    }

    public class SCHGP11
    {
        public List<Result23> results { get; set; }
    }

    public class Metadata25
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class Result24
    {
        public Metadata25 __metadata { get; set; }
        public string CapitalFg { get; set; }
        public string YearFg { get; set; }
        public string Mandt { get; set; }
        public string MarkDel { get; set; }
        public string OutletTp { get; set; }
        public string Crnumber { get; set; }
        public string FormGuid { get; set; }
        public string IncrInCapFg { get; set; }
        public string CapitalRateFg { get; set; }
        public string DataVersion { get; set; }
        public string MainInd { get; set; }
        public int LineNo { get; set; }
        public string ProfitsFg { get; set; }
        public string SubInd { get; set; }
        public string ContainerFg { get; set; }
        public string RankingOrder { get; set; }
        public string ActCd { get; set; }
        public string ActDesc { get; set; }
        public string Capital { get; set; }
        public string IncrInCap { get; set; }
        public string CapitalRate { get; set; }
        public string Profits { get; set; }
        public string Container { get; set; }
        public int LineCnt { get; set; }
        public string Waers { get; set; }
        public string IsIncCapApplicable { get; set; }
        public bool IsApplicableVisible { get; set; }


    }

    public class SCHGP10
    {
        public List<Result24> results { get; set; }
    }

    public class Metadata26
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class Result25
    {
        public Metadata26 __metadata { get; set; }
        public string Mandt { get; set; }
        public string YearFg { get; set; }
        public string MarkDel { get; set; }
        public string OutletTp { get; set; }
        public string RevenueFg { get; set; }
        public string Crnumber { get; set; }
        public string ExpensesFg { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public string MainInd { get; set; }
        public string NetPftFg { get; set; }
        public string ContainerFg { get; set; }
        public int LineNo { get; set; }
        public string SubInd { get; set; }
        public string RankingOrder { get; set; }
        public string SubsidyValFg { get; set; }
        public string ActCd { get; set; }
        public string ActDesc { get; set; }
        public string SubsidyVal { get; set; }
        public string Revenue { get; set; }
        public string Expenses { get; set; }
        public string NetPft { get; set; }
        public string Container { get; set; }
        public int LineCnt { get; set; }
        public string Waers { get; set; }
        public string IsExpApplicable { get; set; }
        public bool IsApplicableVisible { get; set; }


    }

    public class SCHGP09
    {
        public List<Result25> results { get; set; }
    }

    public class Metadata27
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class Result26
    {
        public Metadata27 __metadata { get; set; }
        public string Mandt { get; set; }
        public string YearFg { get; set; }
        public string MarkDel { get; set; }
        public string OutletTp { get; set; }
        public string RoomRateFg { get; set; }
        public string AddRoomRateFg { get; set; }
        public string Crnumber { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public string MainInd { get; set; }
        public string OutputValFg { get; set; }
        public int LineNo { get; set; }
        public string RevenueFg { get; set; }
        public string SubInd { get; set; }
        public string ExpensesFg { get; set; }
        public string RankingOrder { get; set; }
        public string ActCd { get; set; }
        public string NetProfitFg { get; set; }
        public string ActDesc { get; set; }
        public string ContainerFg { get; set; }
        public string NoOfRooms { get; set; }
        public string NoOfRoomsFg { get; set; }
        public string RoomRate { get; set; }
        public string ServicFeeFg { get; set; }
        public string AddRoomRate { get; set; }
        public string OccRateFg { get; set; }
        public string ServicFee { get; set; }
        public string OccRate { get; set; }
        public string City { get; set; }
        public string Owned { get; set; }
        public string OutputVal { get; set; }
        public string Revenue { get; set; }
        public string Expenses { get; set; }
        public string NetProfit { get; set; }
        public string Container { get; set; }
        public int LineCnt { get; set; }
        public string Waers { get; set; }
        public string IsOwned { get; set; }
        public string IsExpApplicable { get; set; }
        public bool IsApplicableVisible { get; set; }


    }

    public class SCHGP08
    {
        public List<Result26> results { get; set; }
    }

    public class Metadata28
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class Result27
    {
        public Metadata28 __metadata { get; set; }
        public string YearFg { get; set; }
        public string Mandt { get; set; }
        public string MarkDel { get; set; }
        public string OutletTp { get; set; }
        public string RevenueFg { get; set; }
        public string Crnumber { get; set; }
        public string ExpensesFg { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public string MainInd { get; set; }
        public string NetPftFg { get; set; }
        public string ContainerFg { get; set; }
        public int LineNo { get; set; }
        public string SubInd { get; set; }
        public string RankingOrder { get; set; }
        public string ActCd { get; set; }
        public string ActDesc { get; set; }
        public string Revenue { get; set; }
        public string Expenses { get; set; }
        public string NetPft { get; set; }
        public string Container { get; set; }
        public int LineCnt { get; set; }
        public string Waers { get; set; }
        public string IsApplicable { get; set; }
        public bool IsApplicableVisible { get; set; }


    }

    public class SCHGP02
    {
        public List<Result27> results { get; set; }
    }

    public class Metadata29
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class Result28
    {
        public Metadata29 __metadata { get; set; }
        public string AgvDlyIncomeFg { get; set; }
        public string YearFg { get; set; }
        public string Mandt { get; set; }
        public string MarkDel { get; set; }
        public string OutletTp { get; set; }
        public string OutputValFg { get; set; }
        public string Crnumber { get; set; }
        public string NoOfCars { get; set; }
        public string RevenueFg { get; set; }
        public string ExpensesFg { get; set; }
        public string FormGuid { get; set; }
        public string MainInd { get; set; }
        public string DataVersion { get; set; }
        public string NetPftFg { get; set; }
        public string SubInd { get; set; }
        public string ContainerFg { get; set; }
        public int LineNo { get; set; }
        public string NoOfCarsFg { get; set; }
        public string RankingOrder { get; set; }
        public string ActCd { get; set; }
        public string OccRateFg { get; set; }
        public string ActDesc { get; set; }
        public string NetPftPerFg { get; set; }
        public string AgvDlyIncome { get; set; }
        public string OccRate { get; set; }
        public string NetPftPer { get; set; }
        public string OutputVal { get; set; }
        public string Revenue { get; set; }
        public string Expenses { get; set; }
        public string NetPft { get; set; }
        public string Container { get; set; }
        public int LineCnt { get; set; }
        public string Waers { get; set; }
        public string IsApplicable { get; set; }
        public bool IsApplicableVisible { get; set; }

    }

    public class SCHGP01
    {
        public List<Result28> results { get; set; }
    }

    public class Metadata30
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class Result29
    {
        public Metadata30 __metadata { get; set; }
        public string MarkDel { get; set; }
        public string Delflag { get; set; }
        public string Mandt { get; set; }
        public string FormGuid { get; set; }
        public string DataVersion { get; set; }
        public int LineNo { get; set; }
        public string RankingOrder { get; set; }
        public string ActCd { get; set; }
        public string ActDesc { get; set; }
        public string GrpNm { get; set; }
        public int LineCnt { get; set; }
        public string Description { get; set; }
        public string Amount { get; set; }
        public string Waers { get; set; }
    }

    public class GENSUBSCH
    {
        public List<Result29> results { get; set; }
    }

    public class ZakatForm5DataResult
    {
        public Metadata __metadata { get; set; }
        public string AttenbChk { get; set; }
        public string LibChk { get; set; }
        public string SelfAmd { get; set; }
        public string AAccExam { get; set; }
        public string APayAmtTp { get; set; }
        public string Govcodfg { get; set; }
        public string Gp71112fg { get; set; }
        public string Instfg { get; set; }
        public string Msgfg { get; set; }
        public string Msgtx { get; set; }
        public string AMobile { get; set; }
        public string AActmethod { get; set; }
        public string AAdj { get; set; }
        public string AAgree { get; set; }
        public object AAgreeDt { get; set; }
        public string AAnnSalesrevAmt { get; set; }
        public string AAnnSalesrevOu { get; set; }
        public string AAnnSalesrevPr { get; set; }
        public string AAnnSalesrevTot { get; set; }
        public string AAnnSalesrevTotFg { get; set; }
        public string AAnnualRent { get; set; }
        public string AAnnualRentFg { get; set; }
        public string AAppAdopTp { get; set; }
        public string AArzaqIPer { get; set; }
        public string ABondsApp { get; set; }
        public string ABulding { get; set; }
        public string ACalendarTp { get; set; }
        public string ACapital { get; set; }
        public string ACapitalFg { get; set; }
        public string ACapitalOu { get; set; }
        public string ACdNm { get; set; }
        public string ACheckFg { get; set; }
        public string ACity { get; set; }
        public string ACompNm { get; set; }
        public string AConrevAmt { get; set; }
        public string AConrevAmtChk { get; set; }
        public string AConrevOu { get; set; }
        public string AConrevPr { get; set; }
        public string AConrevTot { get; set; }
        public string ADistrict { get; set; }
        public string AEmail { get; set; }
        public string AExamMngr { get; set; }
        public string AFaxNo { get; set; }
        public string AFiscalCalendar { get; set; }
        public string AFiscalPeriod { get; set; }
        public DateTime AFromDt { get; set; }
        public string AFromDtCh { get; set; }
        public string AFromDtTp { get; set; }
        public string AGenTradeAmt { get; set; }
        public string AGentradeIPer { get; set; }
        public string AGenTradeOu { get; set; }
        public string AGenTradePr { get; set; }
        public string AGenTradeTot { get; set; }
        public string ALegalSt { get; set; }
        public string ALivelihoodsAmt { get; set; }
        public string ALivelihoodsOu { get; set; }
        public string ALivelihoodsPr { get; set; }
        public string ALivelihoodsTot { get; set; }
        public string ALivestkIPer { get; set; }
        public string ALivestkVegtbAmt { get; set; }
        public string ALivestkVegtbOu { get; set; }
        public string ALivestkVegtbPr { get; set; }
        public string ALivestkVegtbTot { get; set; }
        public string AMainact { get; set; }
        public string AMainAct { get; set; }
        public string AMainActDesc { get; set; }
        public string AMangmentAudit { get; set; }
        public string AmdRsnz { get; set; }
        public string AName { get; set; }
        public string ANetIncomeOthActvAmt { get; set; }
        public string ANetIncomeOthActvAmtChk { get; set; }
        public string ANetIncomeOthActvOu { get; set; }
        public string ANetIncomeOthActvPr { get; set; }
        public string ANetIncomeOthActvTot { get; set; }
        public string ANetTaxableAmount { get; set; }
        public string ANetTaxableAmountOu { get; set; }
        public string ANoBranches { get; set; }
        public string ANoEmp { get; set; }
        public string ANoEmpFg { get; set; }
        public string ANoFinance { get; set; }
        public string ANonsaudiCaSh { get; set; }
        public string ANonsaudiPrSh { get; set; }
        public string ANoOfOutlet { get; set; }
        public string AOthincAmt { get; set; }
        public string AOthincOs { get; set; }
        public string AOthincPr { get; set; }
        public string AOthincTot { get; set; }
        public string AOthincTotFg { get; set; }
        public string AOwnerNmQuat { get; set; }
        public string APdRecptAmt { get; set; }
        public string APdRecptAmtChk { get; set; }
        public string APdRecptOu { get; set; }
        public string APlShare { get; set; }
        public string APlShareChk { get; set; }
        public string APlShareFg { get; set; }
        public string APlShareOu { get; set; }
        public string APoBox { get; set; }
        public string Approvez { get; set; }
        public string APrctr { get; set; }
        public string AReleaseOfContract { get; set; }
        public string AReleaseOfContractChk { get; set; }
        public string AReleaseOfContractOu { get; set; }
        public string AResidency { get; set; }
        public string ARestpaidAmt { get; set; }
        public string ARestpaidAmtOu { get; set; }
        public string ASaudiCaSh { get; set; }
        public string ASaudiPrSh { get; set; }
        public string ASeal { get; set; }
        public string ASign { get; set; }
        public string ASitedocs { get; set; }
        public int AStep { get; set; }
        public string AStreet { get; set; }
        public string ATelephone { get; set; }
        public DateTime AToDt { get; set; }
        public string AToDtCh { get; set; }
        public string AToDtTp { get; set; }
        public string ATotAnnualSal { get; set; }
        public string ATotAnnualSalFg { get; set; }
        public string ATotOwedpaid { get; set; }
        public string ATotOwedpaidOu { get; set; }
        public string ATotProfit { get; set; }
        public string ATotProfitOu { get; set; }
        public string Auditorz { get; set; }
        public string Augrp { get; set; }
        public string AZakat { get; set; }
        public string AZakat51 { get; set; }
        public string AZakat51Ou { get; set; }
        public string AZakatOu { get; set; }
        public string AZakatPrevYr { get; set; }
        public string AZakatPrevYrFg { get; set; }
        public string AZakatPrevYrOu { get; set; }
        public string AZipCd { get; set; }
        public string Bpkind { get; set; }
        public string Cal { get; set; }
        public string CaseGuid { get; set; }
        public string CreateTxAssesz { get; set; }
        public string Dmodez { get; set; }
        public string Euser { get; set; }
        public string Fbguid { get; set; }
        public string Fbguid1 { get; set; }
        public string Fbnum { get; set; }
        public string Fbnumz { get; set; }
        public string FormGuid { get; set; }
        public string Formtype { get; set; }
        public string ForUser { get; set; }
        public string Forward { get; set; }
        public string Fturn { get; set; }
        public string Gp0368fg { get; set; }
        public string InChannel { get; set; }
        public string Incotyp { get; set; }
        public string Isamend { get; set; }
        public string Langz { get; set; }
        public string LegacyDocNo { get; set; }
        public string Line0 { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string Line4 { get; set; }
        public string Line5 { get; set; }
        public string Line6 { get; set; }
        public string Line7 { get; set; }
        public string Line8 { get; set; }
        public string Line9 { get; set; }
        public string Mode { get; set; }
        public string Monthz { get; set; }
        public string ObjSubmitz { get; set; }
        public string Ofbnum { get; set; }
        public string OfficerUidz { get; set; }
        public string Omode { get; set; }
        public string PeriodKeyz { get; set; }
        public string PerslText { get; set; }
        public string PortalUsrz { get; set; }
        public string RegIdz { get; set; }
        public string Rejectz { get; set; }
        public string Retguid { get; set; }
        public string Savez { get; set; }
        public string SavNot { get; set; }
        public string SkipBillingz { get; set; }
        public string Status { get; set; }
        public string Submitz { get; set; }
        public string Taxpayerz { get; set; }
        public string UserType { get; set; }
        public string Xvoidz { get; set; }
        public GP038Set GP03_8Set { get; set; }
        public GP037Set GP03_7Set { get; set; }
        public SCH200Set SCH_200Set { get; set; }
        public SCH800Set SCH_800Set { get; set; }
        public SCHGP07 SCH_GP07 { get; set; }
        public SCHGP06 SCH_GP06 { get; set; }
        public SCHGP05 SCH_GP05 { get; set; }
        public SCHGP04 SCH_GP04 { get; set; }
        public SCHGP03 SCH_GP03 { get; set; }
        public SCHGP3S2Set SCH_GP3S2Set { get; set; }
        public SCHGP3S1Set SCH_GP3S1Set { get; set; }
        public GP036Set GP03_6Set { get; set; }
        public AttDetSet AttDetSet { get; set; }
        public SUBSCHCAPITALSet SUB_SCH_CAPITALSet { get; set; }
        public GP034Set GP03_4Set { get; set; }
        public GP035Set GP03_5Set { get; set; }
        public GP063Set GP06_3Set { get; set; }
        public GP032Set GP03_2Set { get; set; }
        public GP061Set GP06_1Set { get; set; }
        public GP062Set GP06_2Set { get; set; }
        public GP033Set GP03_3Set { get; set; }
        public MAINACTIVITYSet MAIN_ACTIVITYSet { get; set; }
        public LONGTEXTSet LONG_TEXTSet { get; set; }
        public SCHGP12 SCH_GP12 { get; set; }
        public SCHGP11 SCH_GP11 { get; set; }
        public SCHGP10 SCH_GP10 { get; set; }
        public SCHGP09 SCH_GP09 { get; set; }
        public SCHGP08 SCH_GP08 { get; set; }
        public SCHGP02 SCH_GP02 { get; set; }
        public SCHGP01 SCH_GP01 { get; set; }
        public GENSUBSCH GEN_SUB_SCH { get; set; }

        public string IsOtherShareApplicable { get; set; }
        public bool IsOtherShareApplicableVisible { get; set; }

    }
}

//public class ZakatForm5DataResult
//{
//    public D d { get; set; }
//}

///}