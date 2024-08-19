using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{
    public class TaxPayerSubsidyModel
    {
        public string authenticationUser { get; set; }
        public string formBundleGUID { get; set; }
        public string language { get; set; }
        public string source { get; set; }
        public string TIN { get; set; }

    }

    public class MetadataSubsidy
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class SubsidyData
    {
        [JsonProperty("authenticationUser")]
        public string AuthenticationUser { get; set; }

        [JsonProperty("externalPortal")]
        public string ExternalPortal { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("formBundleGUID")]
        public string FormBundleGUID { get; set; }

        [JsonProperty("GUID")]
        public string Guid { get; set; }

        [JsonProperty("TIN")]
        public string TIN { get; set; }
    }

    public class SubsidyResponseModel
    {
        [JsonProperty("result")]
        public SubsidyData Data { get; set; }
    }
}
