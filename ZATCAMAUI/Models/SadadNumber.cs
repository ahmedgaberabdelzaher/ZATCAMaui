using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{

    public class IBANType
    {
        public string key { get; set; }
        public string Text { get; set; }
    }
    
    public class IBANIDNumber
    {
        [JsonProperty("TIN")]
        public string Partner { get; set; }
        [JsonProperty("idNumber")]
        public string Idnumber { get; set; }
        [JsonProperty("idType")]
        public string Type { get; set; }
    }
    
    public class SadadNumber
    {
        [JsonProperty("data")]
        public SadadNumberD d { get; set; }
    }
    
    public class SadadNumberMetadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    
    public class SadadNumberResult
    {
        public Metadata __metadata { get; set; }
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [JsonProperty("language")]
        public string Langu { get; set; }
        [JsonProperty("sadadBillNumber")]
        public string Sopbel { get; set; }
        [JsonProperty("taxType")]
        public string TaxType { get; set; }
        [JsonProperty("revenueType")]
        public string Abtypt { get; set; }
        [JsonProperty("amountPayable")]
        public string Betrh { get; set; }
        [JsonProperty("currency")]
        public string Waers { get; set; }
        [JsonProperty("contract")]
        public string Vtref { get; set; }
        [JsonProperty("isAutoAmount")]
        public bool IsAutoAsmnt { get; set; }
        [JsonProperty("formBundleStatus")]
        public string Fbust { get; set; }
    }
    
    public class SadadNumberD
    {
        [JsonProperty("sadad")]
        public List<SadadNumberResult> results { get; set; }
    }
    public class CheckIbanModel
    {
        public string IBAN { get; set; }
    }
}
