using System;
namespace EGAZT.Models
{
    public class NewYesorNoPageModel
    {
        public class ProfitGoods
        {
            public Metadata __metadata { get; set; }
            public string FormGuid { get; set; }
            public string DataVersion { get; set; }
            public string Gpart { get; set; }
            public string Fbnum { get; set; }
            public string IdType { get; set; }
            public string IdNumber { get; set; }
            public string LicenseIssuer { get; set; }
            public object LicenseStartDt { get; set; }
            public bool SaleUgmCb { get; set; }
            public bool OthActyCb { get; set; }
            public string Ernam { get; set; }
            public object Erdat { get; set; }
            public string Erzet { get; set; }
        }

        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }

        public class Success
        {
            public ProfitGoods d { get; set; }
        }

       // [Preserve(AllMembers = true)]
        public class VATFoodResults
        {


            public string Gpart { get; set; }
            public bool SaleUgmCb { get; set; }
            public bool OthActyCb { get; set; }

        }
    }

    


}

