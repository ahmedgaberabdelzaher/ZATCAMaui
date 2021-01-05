using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace EGAZT.Models.Form5Models
{
    [Preserve(AllMembers = true)]
    public class ZakatForm5SummaryModel
    {
        public ZakatForm5SummaryResult D { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class ZakatForm5SummaryResult
    {
        public Metadata metadata { get; set; }
        public string Flag { get; set; }
        public string Fbnum { get; set; }
        public SchGP01Set SchGP01Set { get; set; }
        public SchGP04Set SchGP04Set { get; set; }
        public SchGP07Set SchGP07Set { get; set; }
        public SchGP09Set SchGP09Set { get; set; }
        public SchGP10Set SchGP10Set { get; set; }
        public SchGP12Set SchGP12Set { get; set; }

        public SchGP08Set SchGP08Set { get; set; }
        public SchGP11Set SchGP11Set { get; set; }
        public SadadSet SadadSet { get; set; }
        public HeadsumSet headsumSet { get; set; }
        public SchGP06Set SchGP06Set { get; set; }
        public SchGP02Set SchGP02Set { get; set; }
        public SchGP03Set SchGP03Set { get; set; }
        public SchGP05Set SchGP05Set { get; set; }
    }
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
    public class Result_
    {
        public Metadata2 __metadata { get; set; }
        public string Mandt { get; set; }
        public string CaseGuid { get; set; }
        public string Fbnum { get; set; }
        public string ActivityProfitTp { get; set; }
        public string ZakatBaseTp { get; set; }
        public string ActivityProfitRr { get; set; }
        public string ZakatBaseRr { get; set; }
        public string Waers { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class SchGP01Set
    {
        public List<Result_> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class Metadata3
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class Result_2
    {
        public Metadata3 __metadata { get; set; }
        public string Mandt { get; set; }
        public string CaseGuid { get; set; }
        public string Fbnum { get; set; }
        public string NoLaboursTp { get; set; }
        public string ActivityProfitTp { get; set; }
        public string ZakatBaseTp { get; set; }
        public string NoLaboursRr { get; set; }
        public string ActivityProfitRr { get; set; }
        public string ZakatBaseRr { get; set; }
        public string Waers { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class SchGP04Set
    {
        public List<Result_2> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class Metadata4
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class Result_3
    {
        public Metadata4 __metadata { get; set; }
        public string Mandt { get; set; }
        public string CaseGuid { get; set; }
        public string Fbnum { get; set; }
        public string ActivityProfitTp { get; set; }
        public string ZakatBaseTp { get; set; }
        public string ActivityProfitRr { get; set; }
        public string ZakatBaseRr { get; set; }
        public string Waers { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class SchGP07Set
    {
        public List<Result_3> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class Metadata5
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class Result_4
    {
        public Metadata5 __metadata { get; set; }
        public string Mandt { get; set; }
        public string CaseGuid { get; set; }
        public string Fbnum { get; set; }
        public string ActivityProfitTp { get; set; }
        public string ZakatBaseTp { get; set; }
        public string ActivityProfitRr { get; set; }
        public string ZakatBaseRr { get; set; }
        public string Waers { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class SchGP09Set
    {
        public List<Result_4> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class Metadata6
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class Result_5
    {
        public Metadata6 __metadata { get; set; }
        public string Mandt { get; set; }
        public string CaseGuid { get; set; }
        public string Fbnum { get; set; }
        public string ActivityProfitTp { get; set; }
        public string ZakatBaseTp { get; set; }
        public string ActivityProfitRr { get; set; }
        public string ZakatBaseRr { get; set; }
        public string Waers { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class SchGP10Set
    {
        public List<Result_5> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class Metadata7
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class Result_6
    {
        public Metadata7 __metadata { get; set; }
        public string Mandt { get; set; }
        public string CaseGuid { get; set; }
        public string Fbnum { get; set; }
        public string ActivityProfitTp { get; set; }
        public string ZakatBaseTp { get; set; }
        public string ActivityProfitRr { get; set; }
        public string ZakatBaseRr { get; set; }
        public string Waers { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class SchGP12Set
    {
        public List<Result_6> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class Metadata8
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class Result_7
    {
        public Metadata8 __metadata { get; set; }
        public string Mandt { get; set; }
        public string CaseGuid { get; set; }
        public string Fbnum { get; set; }
        public string ActivityProfitTp { get; set; }
        public string ZakatBaseTp { get; set; }
        public string ActivityProfitRr { get; set; }
        public string ZakatBaseRr { get; set; }
        public string Waers { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class SchGP08Set
    {
        public List<Result_7> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class Metadata9
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class Result_8
    {
        public Metadata9 __metadata { get; set; }
        public string Mandt { get; set; }
        public string CaseGuid { get; set; }
        public string Fbnum { get; set; }
        public string ActivityProfitTp { get; set; }
        public string ZakatBaseTp { get; set; }
        public string ActivityProfitRr { get; set; }
        public string ZakatBaseRr { get; set; }
        public string Waers { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class SchGP11Set
    {
        public List<Result_8> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class Metadata10
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class Result_9
    {
        public Metadata10 __metadata { get; set; }
        public string Sopbel { get; set; }
        public string TaxType { get; set; }
        public string Abtypt { get; set; }
        public string Betrh { get; set; }
        public string Waers { get; set; }
        public string Vtref { get; set; }
        public bool IsAutoAsmnt { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class SadadSet
    {
        public List<Result_9> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class Metadata11
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class Result_10
    {
        public Metadata11 __metadata { get; set; }
        public string Mandt { get; set; }
        public string CaseGuid { get; set; }
        public string Fbnum { get; set; }
        public string ZakatBaseTp { get; set; }
        public string ZakatPayableTp { get; set; }
        public string ContractRelTp { get; set; }
        public string PayableTotTp { get; set; }
        public string ZakatBaseRr { get; set; }
        public string ZakatPayableRr { get; set; }
        public string ContractRelRr { get; set; }
        public string PayableTotRr { get; set; }
        public string Waers { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class HeadsumSet
    {
        public List<Result_10> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class Metadata12
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class Result_11
    {
        public Metadata12 __metadata { get; set; }
        public string Mandt { get; set; }
        public string CaseGuid { get; set; }
        public string Fbnum { get; set; }
        public string GovtContractProfTp { get; set; }
        public string CivilContrRevTp { get; set; }
        public string NoLaboursTp { get; set; }
        public string ActivityProfitTp { get; set; }
        public string ZakatBaseTp { get; set; }
        public string GovtContractProfRr { get; set; }
        public string CivilContrRevRr { get; set; }
        public string NoLaboursRr { get; set; }
        public string ActivityProfitRr { get; set; }
        public string ZakatBaseRr { get; set; }
        public string Waers { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class SchGP06Set
    {
        public List<Result_11> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class Metadata13
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class Result_12
    {
        public Metadata13 __metadata { get; set; }
        public string Mandt { get; set; }
        public string CaseGuid { get; set; }
        public string Fbnum { get; set; }
        public string ActivityProfitTp { get; set; }
        public string ZakatBaseTp { get; set; }
        public string ActivityProfitRr { get; set; }
        public string ZakatBaseRr { get; set; }
        public string Waers { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class SchGP02Set
    {
        public List<Result_12> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class Metadata14
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class Result_13
    {
        public Metadata14 __metadata { get; set; }
        public string Mandt { get; set; }
        public string CaseGuid { get; set; }
        public string Fbnum { get; set; }
        public string ActivityCapitalTp { get; set; }
        public string ExternalImportTp { get; set; }
        public string ActivityProfitTp { get; set; }
        public string ZakatBaseTp { get; set; }
        public string ActivityCapitalRr { get; set; }
        public string ExternalImportRr { get; set; }
        public string ActivityProfitRr { get; set; }
        public string ZakatBaseRr { get; set; }
        public string Waers { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class SchGP03Set
    {
        public List<Result_13> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class Metadata15
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class Result_14
    {
        public Metadata15 __metadata { get; set; }
        public string Mandt { get; set; }
        public string CaseGuid { get; set; }
        public string Fbnum { get; set; }
        public string ActivityProfitTp { get; set; }
        public string ZakatBaseTp { get; set; }
        public string ActivityProfitRr { get; set; }
        public string ZakatBaseRr { get; set; }
        public string Waers { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class SchGP05Set
    {
        public List<Result_14> results { get; set; }
    }

}
