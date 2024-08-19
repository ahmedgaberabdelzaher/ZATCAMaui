using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ZATCAMAUI.Models.Form5Models
{
    
    public class ZakatForm5SummaryModel
    {
        [DataMember]
        [JsonProperty("data")]
        public ZakatForm5SummaryResult D { get; set; }
    }
    
    public partial class ZakatForm5SummaryResult
    {
        [DataMember]
        public Metadata metadata { get; set; }
        [DataMember]
        [JsonProperty("flag")]
        public string Flag { get; set; }
        [DataMember]
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [DataMember]
        [JsonProperty("cabs")]
        public List<Result_> SchGP01Set { get; set; }
        [DataMember]
        [JsonProperty("labourOccup")]
        public List<Result_2> SchGP04Set { get; set; }
        [DataMember]
        [JsonProperty("professionals")]
        public List<Result_3> SchGP07Set { get; set; }
        [DataMember]
        [JsonProperty("industries")]
        public List<Result_4> SchGP09Set { get; set; }
        [DataMember]
        [JsonProperty("investmentReasEstate")]
        public List<Result_5> SchGP10Set { get; set; }
        [DataMember]
        [JsonProperty("hotels")]
        public List<Result_6> SchGP12Set { get; set; }
        [DataMember]
        [JsonProperty("education")]
        public List<Result_7> SchGP08Set { get; set; }
        [DataMember]
        [JsonProperty("poultryAndFields")]
        public List<Result_8> SchGP11Set { get; set; }
        [DataMember]
        [JsonProperty("sadad")]
        public List<Result_9> SadadSet { get; set; }
        [DataMember]
        [JsonProperty("zakatSummaryHeaderSet")]
        public List<Result_10> headsumSet { get; set; }
        [DataMember]
        [JsonProperty("contractingCompanies")]
        public List<Result_11> SchGP06Set { get; set; }
        [DataMember]
        [JsonProperty("cars")]
        public List<Result_12> SchGP02Set { get; set; }
        [DataMember]
        [JsonProperty("saleAndBuy")]
        public List<Result_13> SchGP03Set { get; set; }
        [DataMember]
        [JsonProperty("minerals")]
        public List<Result_14> SchGP05Set { get; set; }
    }
   
    
    public class Result_
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("caseGUID")]
        public string CaseGuid { get; set; }
        [DataMember]
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [DataMember]
        [JsonProperty("activityProfitTaxpayer")]
        public string ActivityProfitTp { get; set; }
        [DataMember]
        [JsonProperty("zakatBaseTaxpayer")]
        public string ZakatBaseTp { get; set; }
        [DataMember]
        [JsonProperty("activityProfitAmount")]
        public string ActivityProfitRr { get; set; }
        [DataMember]
        [JsonProperty("zakatBaseAmount")]
        public string ZakatBaseRr { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }
    }
    
    public class SchGP01Set
    {
        [DataMember]
        public List<Result_> results { get; set; }
    }

    public class Result_2
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("caseGUID")]
        public string CaseGuid { get; set; }
        [DataMember]
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [DataMember]
        [JsonProperty("taxpayerLaboursNumber")]
        public string NoLaboursTp { get; set; }
        [DataMember]
        [JsonProperty("activityProfitTaxpayer")]
        public string ActivityProfitTp { get; set; }
        [DataMember]
        [JsonProperty("zakatBaseTaxpayer")]
        public string ZakatBaseTp { get; set; }
        [DataMember]
        [JsonProperty("laboursNumber")]
        public string NoLaboursRr { get; set; }
        [DataMember]
        [JsonProperty("activityProfitAmount")]
        public string ActivityProfitRr { get; set; }
        [DataMember]
        [JsonProperty("zakatBaseAmount")]
        public string ZakatBaseRr { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }
    }
    
    public class SchGP04Set
    {
        [DataMember]
        public List<Result_2> results { get; set; }
    }
    
  
    public class Result_3
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("caseGUID")]
        public string CaseGuid { get; set; }
        [DataMember]
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [DataMember]
        [JsonProperty("activityProfitTaxpayer")]
        public string ActivityProfitTp { get; set; }
        [DataMember]
        [JsonProperty("zakatBaseTaxpayer")]
        public string ZakatBaseTp { get; set; }
        [DataMember]
        [JsonProperty("activityProfitAmount")]
        public string ActivityProfitRr { get; set; }
        [DataMember]
        [JsonProperty("zakatBaseAmount")]
        public string ZakatBaseRr { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }
    }
    
    public class SchGP07Set
    {
        [DataMember]
        public List<Result_3> results { get; set; }
    }
    
    public class Result_4
    {
        [DataMember]
        public Metadata5 __metadata { get; set; }
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("caseGUID")]
        public string CaseGuid { get; set; }
        [DataMember]
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [DataMember]
        [JsonProperty("activityProfitTaxpayer")]
        public string ActivityProfitTp { get; set; }
        [DataMember]
        [JsonProperty("zakatBaseTaxpayer")]
        public string ZakatBaseTp { get; set; }
        [DataMember]
        [JsonProperty("activityProfitAmount")]
        public string ActivityProfitRr { get; set; }
        [DataMember]
        [JsonProperty("zakatBaseAmount")]
        public string ZakatBaseRr { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }
    }
    
    public class SchGP09Set
    {
        [DataMember]
        public List<Result_4> results { get; set; }
    }
    public class Result_5
    {
        [DataMember]
        public Metadata5 __metadata { get; set; }
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("caseGUID")]
        public string CaseGuid { get; set; }
        [DataMember]
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [DataMember]
        [JsonProperty("activityProfitTaxpayer")]
        public string ActivityProfitTp { get; set; }
        [DataMember]
        [JsonProperty("zakatBaseTaxpayer")]
        public string ZakatBaseTp { get; set; }
        [DataMember]
        [JsonProperty("activityProfitAmount")]
        public string ActivityProfitRr { get; set; }
        [DataMember]
        [JsonProperty("zakatBaseAmount")]
        public string ZakatBaseRr { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }
    }
    
    public class SchGP10Set
    {
        [DataMember]
        public List<Result_5> results { get; set; }
    }
    
    public class Result_6
    {
        [DataMember]
        public Metadata5 __metadata { get; set; }
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("caseGUID")]
        public string CaseGuid { get; set; }
        [DataMember]
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [DataMember]
        [JsonProperty("activityProfitTaxpayer")]
        public string ActivityProfitTp { get; set; }
        [DataMember]
        [JsonProperty("zakatBaseTaxpayer")]
        public string ZakatBaseTp { get; set; }
        [DataMember]
        [JsonProperty("activityProfitAmount")]
        public string ActivityProfitRr { get; set; }
        [DataMember]
        [JsonProperty("zakatBaseAmount")]
        public string ZakatBaseRr { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }
    }
    
    public class SchGP12Set
    {
        [DataMember]
        public List<Result_6> results { get; set; }
    }
    
    
    public class Result_7
    {
        [DataMember]
        public Metadata5 __metadata { get; set; }
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("caseGUID")]
        public string CaseGuid { get; set; }
        [DataMember]
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [DataMember]
        [JsonProperty("activityProfitTaxpayer")]
        public string ActivityProfitTp { get; set; }
        [DataMember]
        [JsonProperty("zakatBaseTaxpayer")]
        public string ZakatBaseTp { get; set; }
        [DataMember]
        [JsonProperty("activityProfitAmount")]
        public string ActivityProfitRr { get; set; }
        [DataMember]
        [JsonProperty("zakatBaseAmount")]
        public string ZakatBaseRr { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }
    }
    
    public class SchGP08Set
    {
        [DataMember]
        public List<Result_7> results { get; set; }
    }
    
    public class Result_8
    {
        [DataMember]
        public Metadata5 __metadata { get; set; }
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("caseGUID")]
        public string CaseGuid { get; set; }
        [DataMember]
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [DataMember]
        [JsonProperty("activityProfitTaxpayer")]
        public string ActivityProfitTp { get; set; }
        [DataMember]
        [JsonProperty("zakatBaseTaxpayer")]
        public string ZakatBaseTp { get; set; }
        [DataMember]
        [JsonProperty("activityProfitAmount")]
        public string ActivityProfitRr { get; set; }
        [DataMember]
        [JsonProperty("zakatBaseAmount")]
        public string ZakatBaseRr { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }
    }
    
    public class SchGP11Set
    {
        [DataMember]
        public List<Result_8> results { get; set; }
    }

    
    public class Result_9
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("sadadBillNumber")]
        public string Sopbel { get; set; }
        [DataMember]
        [JsonProperty("taxType")]
        public string TaxType { get; set; }
        [DataMember]
        [JsonProperty("revenueType")]
        public string Abtypt { get; set; }
        [DataMember]
        [JsonProperty("payableAmount")]
        public string Betrh { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }
        [DataMember]
        [JsonProperty("contract")]
        public string Vtref { get; set; }
        [DataMember]
        [JsonProperty("isAutoAmount")]
        public bool IsAutoAsmnt { get; set; }
    }
    
    public class SadadSet
    {
        [DataMember]
        [JsonProperty("sadad")]
        public List<Result_9> results { get; set; }
    }
  
    
    public class Result_10
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("caseGUID")]
        public string CaseGuid { get; set; }
        [DataMember]
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [DataMember]
        [JsonProperty("zakatBaseTaxpayer")]
        public string ZakatBaseTp { get; set; }
        [DataMember]
        [JsonProperty("zakatPayableTaxpayer")]
        public string ZakatPayableTp { get; set; }
        [DataMember]
        [JsonProperty("contractReleaseTaxPayer")]
        public string ContractRelTp { get; set; }
        [DataMember]
        [JsonProperty("payableTotalTaxpayer")]
        public string PayableTotTp { get; set; }
        [DataMember]
        [JsonProperty("zakatBase")]
        public string ZakatBaseRr { get; set; }
        [DataMember]
        [JsonProperty("zakatPayableAmount")]
        public string ZakatPayableRr { get; set; }
        [DataMember]
        [JsonProperty("contractReleaseAmount")]
        public string ContractRelRr { get; set; }
        [DataMember]
        [JsonProperty("payableTotalAmount")]
        public string PayableTotRr { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }
    }
    
    public class HeadsumSet
    {
        [DataMember]
        [JsonProperty("zakatSummaryHeaderSet")]
        public List<Result_10> results { get; set; }
    }
    

    public class Result_11
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("caseGUID")]
        public string CaseGuid { get; set; }
        [DataMember]
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [DataMember]
        [JsonProperty("governmentContractProfitTaxpayer")]
        public string GovtContractProfTp { get; set; }
        [DataMember]
        [JsonProperty("civilContractRevenueTaxpayer")]
        public string CivilContrRevTp { get; set; }
        [DataMember]
        [JsonProperty("taxpayerLaboursNumber")]
        public string NoLaboursTp { get; set; }
        [DataMember]
        [JsonProperty("activityProfitTaxpayer")]
        public string ActivityProfitTp { get; set; }
        [DataMember]
        [JsonProperty("zakatBaseTaxpayer")]
        public string ZakatBaseTp { get; set; }
        [DataMember]
        [JsonProperty("governmentContractProfit")]
        public string GovtContractProfRr { get; set; }
        [DataMember]
        [JsonProperty("civilContractRevenue")]
        public string CivilContrRevRr { get; set; }
        [DataMember]
        [JsonProperty("laboursNumber")]
        public string NoLaboursRr { get; set; }
        [DataMember]
        [JsonProperty("activityProfit")]
        public string ActivityProfitRr { get; set; }
        [DataMember]
        [JsonProperty("zakatBaseAmount")]
        public string ZakatBaseRr { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }
    }
    
    public class SchGP06Set
    {
        [DataMember]
        public List<Result_11> results { get; set; }
    }

    
    public class Result_12
    {
        [DataMember]
        public Metadata5 __metadata { get; set; }
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("caseGUID")]
        public string CaseGuid { get; set; }
        [DataMember]
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [DataMember]
        [JsonProperty("activityProfitTaxpayer")]
        public string ActivityProfitTp { get; set; }
        [DataMember]
        [JsonProperty("zakatBaseTaxpayer")]
        public string ZakatBaseTp { get; set; }
        [DataMember]
        [JsonProperty("activityProfitAmount")]
        public string ActivityProfitRr { get; set; }
        [DataMember]
        [JsonProperty("zakatBaseAmount")]
        public string ZakatBaseRr { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }
    }
    
    public class SchGP02Set
    {
        [DataMember]
        public List<Result_12> results { get; set; }
    }

    public class Result_13
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("caseGUID")]
        public string CaseGuid { get; set; }
        [DataMember]
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [DataMember]
        [JsonProperty("activityCapitalTaxpayer")]
        public string ActivityCapitalTp { get; set; }
        [DataMember]
        [JsonProperty("externalImportTaxpayer")]
        public string ExternalImportTp { get; set; }
        [DataMember]
        [JsonProperty("activityProfitTaxpayer")]
        public string ActivityProfitTp { get; set; }
        [DataMember]
        [JsonProperty("zakatBaseTaxpayer")]
        public string ZakatBaseTp { get; set; }
        [DataMember]
        [JsonProperty("activityCapitalAmount")]
        public string ActivityCapitalRr { get; set; }
        [DataMember]
        [JsonProperty("externalImportAmount")]
        public string ExternalImportRr { get; set; }
        [DataMember]
        [JsonProperty("activityProfitAmount")]
        public string ActivityProfitRr { get; set; }
        [DataMember]
        [JsonProperty("zakatBaseAmount")]
        public string ZakatBaseRr { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }
    }
    
    public class SchGP03Set
    {
        [DataMember]
        public List<Result_13> results { get; set; }
    }
    

    
    public class Result_14
    {
        [DataMember]
        public Metadata5 __metadata { get; set; }
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("caseGUID")]
        public string CaseGuid { get; set; }
        [DataMember]
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [DataMember]
        [JsonProperty("activityProfitTaxpayer")]
        public string ActivityProfitTp { get; set; }
        [DataMember]
        [JsonProperty("zakatBaseTaxpayer")]
        public string ZakatBaseTp { get; set; }
        [DataMember]
        [JsonProperty("activityProfitAmount")]
        public string ActivityProfitRr { get; set; }
        [DataMember]
        [JsonProperty("zakatBaseAmount")]
        public string ZakatBaseRr { get; set; }
        [DataMember]
        [JsonProperty("currency")]
        public string Waers { get; set; }
    }
    
    public class SchGP05Set
    {
        [DataMember]
        public List<Result_14> results { get; set; }
    }

}
