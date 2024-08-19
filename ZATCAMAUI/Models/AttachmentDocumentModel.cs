using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{


    public partial class AttachmentDocumentModel
    {
        [JsonProperty("data")]
        public AttachmentResult[] D { get; set; }
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

        [JsonProperty("returnGUID")]
        public string RetGuid { get; set; }

        [JsonProperty("portalUser")]
        public string ByPusr { get; set; }

        [JsonProperty("attachment")]
        public string Flag { get; set; }

        [JsonProperty("sequenceNumber")]
        public string Seqno { get; set; }

        [JsonProperty("formGUID")]
        public string SchGuid { get; set; }

        [JsonProperty("documentCategory")]
        public string Dotyp { get; set; }

        [JsonProperty("documentId")]
        public string Doguid { get; set; }

        [JsonProperty("attachedByPerson")]
        public string AttBy { get; set; }

        [JsonProperty("fileName")]
        public string Filename { get; set; }

        [JsonProperty("fileExtension")]
        public string FileExtn { get; set; }

        [JsonProperty("MIMEType")]
        public string Mimetype { get; set; }

        [JsonProperty("entryDate")]
        public string Erfdt { get; set; }

        [JsonProperty("dataVersion")]
        public string DataVersion { get; set; }

        [JsonProperty("documentURL")]
        public Uri DocUrl { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("outletReference")]
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
