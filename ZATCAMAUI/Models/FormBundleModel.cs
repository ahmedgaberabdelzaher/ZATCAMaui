
using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{

    public class FormBundleMetadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    
    public class FormBundleResult
    {
        public FormBundleMetadata __metadata { get; set; }
        [JsonProperty("language")]
        public string Lang { get; set; }
        [JsonProperty("TIN")]
        public string Gpart { get; set; }
        [JsonProperty("formBundleType")]
        public string Fbtyp { get; set; }
        [JsonProperty("statusDescription")]
        public string Txt50 { get; set; }
    }
    
    public class FormBundleD
    {
        [JsonProperty("returnsStatus")]
        public List<FormBundleResult> results { get; set; }
    }
    
    public class FormBundleModel
    {
        [JsonProperty("data")]
        public FormBundleD d { get; set; }
    }
    
    public class FormBundleApplicationNumberModelMetadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    
    public class FormBundleApplicationNumberModelResult
    {
       // public FormBundleApplicationNumberModelMetadata __metadata { get; set; }
        [JsonProperty("formBundleStatusDescription")]
        public string Fbstatus { get; set; }
        [JsonProperty("language")]
        public string Lang { get; set; }
        [JsonProperty("TIN")]
        public string Gpart { get; set; }
        [JsonProperty("formBundleType")]
        public string Fbtyp { get; set; }
        [JsonProperty("statusDescription")]
        public string Txt50 { get; set; }
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [JsonProperty("periodKey")]
        public string PeriodKey { get; set; }
        [JsonProperty("periodDescription")]
        public string Perslt { get; set; }
        [JsonProperty("formBundleStatus")]
        public string Fbsta { get; set; }
        //[JsonProperty("systemStatus2")]
        //public string Fbstb { get; set; }
        [JsonProperty("userStatus")]
        public string Fbust { get; set; }
    }
    
    public class FormBundleApplicationNumberModelD
    {
        [JsonProperty("returnsStatus")]
        public List<FormBundleApplicationNumberModelResult> results { get; set; }
    }
    
    public class FormBundleApplicationNumberModel
    {
        [JsonProperty("data")]
        public FormBundleApplicationNumberModelD d { get; set; }
    }
    
    public class FbnumDetailList
    {
        public string Fbnum { get; set; }
        public string Fbsta { get; set; }
        public string FbStatus { get; set; }
        public string FbDesc { get; set; }
    }
}
