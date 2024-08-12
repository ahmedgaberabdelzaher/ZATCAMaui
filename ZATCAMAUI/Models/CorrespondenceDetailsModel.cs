using Foundation;
using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{

    public class CorrespondenceDetailsModel
    {
    }
    
    public class CorrespondenceDetailsMetadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    
    public class CorrespondenceDetailsResult
    {
       // public Metadata __metadata { get; set; }
        [JsonProperty("attachment")]
        public string Attfg { get; set; }
        [JsonProperty("language")]
        public string Langu { get; set; }
        [JsonProperty("correspondenceType")]
        public string Cotyp { get; set; }
        [JsonProperty("hotlineNumber")]
        public string Hotline { get; set; }
        [JsonProperty("TIN")]
        public string Gpart { get; set; }
        [JsonProperty("letterNumber")]
        public string Ltrno { get; set; }
        [JsonProperty("notesFormat")]
        public string Tdformat { get; set; }
        [JsonProperty("generalDescription")]
        public string Txtdo { get; set; }
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [JsonProperty("notesLine")]
        public string Tdline { get; set; }
        [JsonProperty("correspondenceKey")]
        public string Cokey { get; set; }
    }
    
    public class CorrespondenceDetailsD
    {
        [JsonProperty("correspondences")]
        public List<CorrespondenceDetailsResult> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class CorrespondenceDetailsRootObject
    {
        [JsonProperty("data")]
        public CorrespondenceDetailsD d { get; set; }
    }
}
