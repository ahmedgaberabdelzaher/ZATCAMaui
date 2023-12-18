using System.Runtime.Serialization;

namespace ZATCAMAUI.Models.Form5Models
{
    
    public class ZakatForm5SummaryModel
    {
        [DataMember]
        public ZakatForm5SummaryResult D { get; set; }
    }
    
    public partial class ZakatForm5SummaryResult
    {
        [DataMember]
        public Metadata metadata { get; set; }
        [DataMember]
        public string Flag { get; set; }
        [DataMember]
        public string Fbnum { get; set; }
        [DataMember]
        public SchGP01Set SchGP01Set { get; set; }
        [DataMember]
        public SchGP04Set SchGP04Set { get; set; }
        [DataMember]
        public SchGP07Set SchGP07Set { get; set; }
        [DataMember]
        public SchGP09Set SchGP09Set { get; set; }
        [DataMember]
        public SchGP10Set SchGP10Set { get; set; }
        [DataMember]
        public SchGP12Set SchGP12Set { get; set; }
        [DataMember]

        public SchGP08Set SchGP08Set { get; set; }
        [DataMember]
        public SchGP11Set SchGP11Set { get; set; }
        [DataMember]
        public SadadSet SadadSet { get; set; }
        [DataMember]
        public HeadsumSet headsumSet { get; set; }
        [DataMember]
        public SchGP06Set SchGP06Set { get; set; }
        [DataMember]
        public SchGP02Set SchGP02Set { get; set; }
        [DataMember]
        public SchGP03Set SchGP03Set { get; set; }
        [DataMember]
        public SchGP05Set SchGP05Set { get; set; }
    }
   
    
    public class Result_
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        public string Mandt { get; set; }
        [DataMember]
        public string CaseGuid { get; set; }
        [DataMember]
        public string Fbnum { get; set; }
        [DataMember]
        public string ActivityProfitTp { get; set; }
        [DataMember]
        public string ZakatBaseTp { get; set; }
        [DataMember]
        public string ActivityProfitRr { get; set; }
        [DataMember]
        public string ZakatBaseRr { get; set; }
        [DataMember]
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
        public string Mandt { get; set; }
        [DataMember]
        public string CaseGuid { get; set; }
        [DataMember]
        public string Fbnum { get; set; }
        [DataMember]
        public string NoLaboursTp { get; set; }
        [DataMember]
        public string ActivityProfitTp { get; set; }
        [DataMember]
        public string ZakatBaseTp { get; set; }
        [DataMember]
        public string NoLaboursRr { get; set; }
        [DataMember]
        public string ActivityProfitRr { get; set; }
        [DataMember]
        public string ZakatBaseRr { get; set; }
        [DataMember]
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
        public string Mandt { get; set; }
        [DataMember]
        public string CaseGuid { get; set; }
        [DataMember]
        public string Fbnum { get; set; }
        [DataMember]
        public string ActivityProfitTp { get; set; }
        [DataMember]
        public string ZakatBaseTp { get; set; }
        [DataMember]
        public string ActivityProfitRr { get; set; }
        [DataMember]
        public string ZakatBaseRr { get; set; }
        [DataMember]
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
        public Metadata __metadata { get; set; }
        [DataMember]
        public string Mandt { get; set; }
        [DataMember]
        public string CaseGuid { get; set; }
        [DataMember]
        public string Fbnum { get; set; }
        [DataMember]
        public string ActivityProfitTp { get; set; }
        [DataMember]
        public string ZakatBaseTp { get; set; }
        [DataMember]
        public string ActivityProfitRr { get; set; }
        [DataMember]
        public string ZakatBaseRr { get; set; }
        [DataMember]
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
        public Metadata __metadata { get; set; }
        [DataMember]
        public string Mandt { get; set; }
        [DataMember]
        public string CaseGuid { get; set; }
        [DataMember]
        public string Fbnum { get; set; }
        [DataMember]
        public string ActivityProfitTp { get; set; }
        [DataMember]
        public string ZakatBaseTp { get; set; }
        [DataMember]
        public string ActivityProfitRr { get; set; }
        [DataMember]
        public string ZakatBaseRr { get; set; }
        [DataMember]
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
        public Metadata __metadata { get; set; }
        [DataMember]
        public string Mandt { get; set; }
        [DataMember]
        public string CaseGuid { get; set; }
        [DataMember]
        public string Fbnum { get; set; }
        [DataMember]
        public string ActivityProfitTp { get; set; }
        [DataMember]
        public string ZakatBaseTp { get; set; }
        [DataMember]
        public string ActivityProfitRr { get; set; }
        [DataMember]
        public string ZakatBaseRr { get; set; }
        [DataMember]
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
        public Metadata __metadata { get; set; }
        [DataMember]
        public string Mandt { get; set; }
        [DataMember]
        public string CaseGuid { get; set; }
        [DataMember]
        public string Fbnum { get; set; }
        [DataMember]
        public string ActivityProfitTp { get; set; }
        [DataMember]
        public string ZakatBaseTp { get; set; }
        [DataMember]
        public string ActivityProfitRr { get; set; }
        [DataMember]
        public string ZakatBaseRr { get; set; }
        [DataMember]
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
        public Metadata __metadata { get; set; }
        [DataMember]
        public string Mandt { get; set; }
        [DataMember]
        public string CaseGuid { get; set; }
        [DataMember]
        public string Fbnum { get; set; }
        [DataMember]
        public string ActivityProfitTp { get; set; }
        [DataMember]
        public string ZakatBaseTp { get; set; }
        [DataMember]
        public string ActivityProfitRr { get; set; }
        [DataMember]
        public string ZakatBaseRr { get; set; }
        [DataMember]
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
        public string Sopbel { get; set; }
        [DataMember]
        public string TaxType { get; set; }
        [DataMember]
        public string Abtypt { get; set; }
        [DataMember]
        public string Betrh { get; set; }
        [DataMember]
        public string Waers { get; set; }
        [DataMember]
        public string Vtref { get; set; }
        [DataMember]
        public bool IsAutoAsmnt { get; set; }
    }
    
    public class SadadSet
    {
        [DataMember]
        public List<Result_9> results { get; set; }
    }
  
    
    public class Result_10
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        public string Mandt { get; set; }
        [DataMember]
        public string CaseGuid { get; set; }
        [DataMember]
        public string Fbnum { get; set; }
        [DataMember]
        public string ZakatBaseTp { get; set; }
        [DataMember]
        public string ZakatPayableTp { get; set; }
        [DataMember]
        public string ContractRelTp { get; set; }
        [DataMember]
        public string PayableTotTp { get; set; }
        [DataMember]
        public string ZakatBaseRr { get; set; }
        [DataMember]
        public string ZakatPayableRr { get; set; }
        [DataMember]
        public string ContractRelRr { get; set; }
        [DataMember]
        public string PayableTotRr { get; set; }
        [DataMember]
        public string Waers { get; set; }
    }
    
    public class HeadsumSet
    {
        [DataMember]
        public List<Result_10> results { get; set; }
    }
    

    public class Result_11
    {
        [DataMember]
        public Metadata __metadata { get; set; }
        [DataMember]
        public string Mandt { get; set; }
        [DataMember]
        public string CaseGuid { get; set; }
        [DataMember]
        public string Fbnum { get; set; }
        [DataMember]
        public string GovtContractProfTp { get; set; }
        [DataMember]
        public string CivilContrRevTp { get; set; }
        [DataMember]
        public string NoLaboursTp { get; set; }
        [DataMember]
        public string ActivityProfitTp { get; set; }
        [DataMember]
        public string ZakatBaseTp { get; set; }
        [DataMember]
        public string GovtContractProfRr { get; set; }
        [DataMember]
        public string CivilContrRevRr { get; set; }
        [DataMember]
        public string NoLaboursRr { get; set; }
        [DataMember]
        public string ActivityProfitRr { get; set; }
        [DataMember]
        public string ZakatBaseRr { get; set; }
        [DataMember]
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
        public Metadata __metadata { get; set; }
        [DataMember]
        public string Mandt { get; set; }
        [DataMember]
        public string CaseGuid { get; set; }
        [DataMember]
        public string Fbnum { get; set; }
        [DataMember]
        public string ActivityProfitTp { get; set; }
        [DataMember]
        public string ZakatBaseTp { get; set; }
        [DataMember]
        public string ActivityProfitRr { get; set; }
        [DataMember]
        public string ZakatBaseRr { get; set; }
        [DataMember]
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
        public string Mandt { get; set; }
        [DataMember]
        public string CaseGuid { get; set; }
        [DataMember]
        public string Fbnum { get; set; }
        [DataMember]
        public string ActivityCapitalTp { get; set; }
        [DataMember]
        public string ExternalImportTp { get; set; }
        [DataMember]
        public string ActivityProfitTp { get; set; }
        [DataMember]
        public string ZakatBaseTp { get; set; }
        [DataMember]
        public string ActivityCapitalRr { get; set; }
        [DataMember]
        public string ExternalImportRr { get; set; }
        [DataMember]
        public string ActivityProfitRr { get; set; }
        [DataMember]
        public string ZakatBaseRr { get; set; }
        [DataMember]
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
        public Metadata __metadata { get; set; }
        [DataMember]
        public string Mandt { get; set; }
        [DataMember]
        public string CaseGuid { get; set; }
        [DataMember]
        public string Fbnum { get; set; }
        [DataMember]
        public string ActivityProfitTp { get; set; }
        [DataMember]
        public string ZakatBaseTp { get; set; }
        [DataMember]
        public string ActivityProfitRr { get; set; }
        [DataMember]
        public string ZakatBaseRr { get; set; }
        [DataMember]
        public string Waers { get; set; }
    }
    
    public class SchGP05Set
    {
        [DataMember]
        public List<Result_14> results { get; set; }
    }

}
