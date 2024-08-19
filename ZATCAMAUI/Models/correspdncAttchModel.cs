using Newtonsoft.Json;
namespace ZATCAMAUI.Models
{
	public class correspdncAttchModel
	{
        [JsonProperty("data")]
        public D d { get; set; }
        
        public class D
        {
            public List<CorrDetails> results { get; set; }
        }

        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }

        public class CorrDetails
        {
            public Metadata __metadata { get; set; }
            [JsonProperty("flag224")]
            public bool Flg224 { get; set; }
            [JsonProperty("serialNumber")]
            public int Srno { get; set; }
            [JsonProperty("entryDate")]
            public DateTime Erfdt { get; set; }
            [JsonProperty("fileSize")]
            public string ZfileSize { get; set; }
            [JsonProperty("formBundleNumber")]
            public string Fbnum { get; set; }
            [JsonProperty("returnGUID")]
            public string RetGuid { get; set; }
            [JsonProperty("TIN")]
            public string Gpart { get; set; }
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
            [JsonProperty("portalUser")]
            public string ByPusr { get; set; }
            [JsonProperty("dataVersion")]
            public string DataVersion { get; set; }
            [JsonProperty("documentURL")]
            public string DocUrl { get; set; }
            [JsonProperty("outletReference")]
            public string OutletRef { get; set; }
            [JsonProperty("createdBy")]
            public string Ernam { get; set; }
            [JsonProperty("enableDelete")]
            public string Enbdele { get; set; }
            [JsonProperty("visibleEdit")]
            public string Visedit { get; set; }
            [JsonProperty("visibleDelete")]
            public string Visdel { get; set; }
            [JsonProperty("attachmentName")]
            public string AttachNm { get; set; }
            [JsonProperty("correspondenceKey")]
            public string Cokey { get; set; }
            [JsonProperty("correspondenceType")]
            public string Cotyp { get; set; }
            [JsonProperty("content")]
            public string Content { get; set; }
            [JsonProperty("printURL")]
            public string Printurl { get; set; }
        }

       
    }
}

