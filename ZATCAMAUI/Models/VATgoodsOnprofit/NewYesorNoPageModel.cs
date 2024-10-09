using Newtonsoft.Json;

namespace ZATCAMAUI.Models.VATgoodsOnprofit
{
    public class NewYesorNoPageModel
    {
        public class ProfitGoods
        {
            public Metadata __metadata { get; set; }
            [JsonProperty("formGUID")]
            public string FormGuid { get; set; }
            [JsonProperty("dataVersion")]
            public string DataVersion { get; set; }
            [JsonProperty("TIN")]
            public string Gpart { get; set; }
            [JsonProperty("formBundleNumber")]
            public string Fbnum { get; set; }
            [JsonProperty("idType")]
            public string IdType { get; set; }
            [JsonProperty("idNumber")]
            public string IdNumber { get; set; }
            [JsonProperty("licenseIssuer")]
            public string LicenseIssuer { get; set; }
            [JsonProperty("licenseStartDate")]
            public object LicenseStartDt { get; set; }
            [JsonProperty("isSaleUsedGoods")]
            public bool SaleUgmCb { get; set; }
            [JsonProperty("isOtherActivity")]
            public bool OthActyCb { get; set; }
            [JsonProperty("createdBy")]
            public string Ernam { get; set; }
            [JsonProperty("createdOn")]
            public object Erdat { get; set; }
            [JsonProperty("time")]
            public string Erzet { get; set; }
            public string RegFlag { get; set; }
            [JsonProperty("deregistrationFormBundleNumber")]
            public string DregFbnum { get; set; }
            public string taxpayerName { get; set; }
            public string authenticationUser { get; set; }
            public string formBudleGUID { get; set; }
            public string registration { get; set; }
            public string returnId { get; set; }
            public string systemCode { get; set; }
            public string VATProfitMargin { get; set; }
            public string TpName { get; set; }

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

        public class VATFoodResults
        {


            [JsonProperty("TIN")]
            public string Gpart { get; set; }
            [JsonProperty("isSaleUsedGoods")]
            public bool SaleUgmCb { get; set; }
            [JsonProperty("isOtherActivity")]
            public bool OthActyCb { get; set; }
            [JsonProperty("formBundleNumber")]
            public string Fbnum { get; set; }
            [JsonProperty("taxpayerName")]
            public string TpName { get; set; }


        }
        public class ProfitGoodsResponse
        {
            public ProfitGoods data { get; set; }
        }
    }
}

