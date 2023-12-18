using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{
    public class TaxPayerSubsidyModel
    {
        public string Euser { get; set; }
        public string Fbguid { get; set; }
        public string Langz { get; set; }
        public string Source { get; set; }
        public string Partner { get; set; }

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
        [JsonProperty("__metadata")]
        public MetadataSubsidy Metadata { get; set; }

        [JsonProperty("Euser")]
        public string Euser { get; set; }

        [JsonProperty("ExternalPortal")]
        public string ExternalPortal { get; set; }

        [JsonProperty("Source")]
        public string Source { get; set; }

        [JsonProperty("Langz")]
        public string Langz { get; set; }

        [JsonProperty("Partner")]
        public string Partner { get; set; }

        [JsonProperty("Fbguid")]
        public string Fbguid { get; set; }

        [JsonProperty("Guid")]
        public string Guid { get; set; }
    }

    public class SubsidyResponseModel
    {
        [JsonProperty("d")]
        public SubsidyData D { get; set; }
    }
}
