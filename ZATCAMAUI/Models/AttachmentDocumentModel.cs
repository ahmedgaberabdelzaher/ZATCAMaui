using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{


    public partial class AttachmentDocumentModel
    {
        [JsonProperty("d")]
        public DAttachmentDocumentModel D { get; set; }
    }
    

    public partial class DAttachmentDocumentModel
    {
        [JsonProperty("results")]
        public AttachmentResult[] Results { get; set; }
    }
    

    public partial class AttachmentResult
    {
        [JsonProperty("__metadata")]
        public MetadataAttachment Metadata { get; set; }

        [JsonProperty("RetGuid")]
        public string RetGuid { get; set; }

        [JsonProperty("ByPusr")]
        public string ByPusr { get; set; }

        [JsonProperty("Flag")]
        public string Flag { get; set; }

        [JsonProperty("Seqno")]
        public string Seqno { get; set; }

        [JsonProperty("SchGuid")]
        public string SchGuid { get; set; }

        [JsonProperty("Dotyp")]
        public string Dotyp { get; set; }

        [JsonProperty("Doguid")]
        public string Doguid { get; set; }

        [JsonProperty("AttBy")]
        public string AttBy { get; set; }

        [JsonProperty("Filename")]
        public string Filename { get; set; }

        [JsonProperty("FileExtn")]
        public string FileExtn { get; set; }

        [JsonProperty("Mimetype")]
        public string Mimetype { get; set; }

        [JsonProperty("Erfdt")]
        public string Erfdt { get; set; }

        [JsonProperty("DataVersion")]
        public string DataVersion { get; set; }

        [JsonProperty("DocUrl")]
        public Uri DocUrl { get; set; }

        [JsonProperty("Content")]
        public string Content { get; set; }

        [JsonProperty("OutletRef")]
        public string OutletRef { get; set; }
    }
    

    public partial class MetadataAttachment
    {
        [JsonProperty("id")]
        public Uri Id { get; set; }

        [JsonProperty("uri")]
        public Uri Uri { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
