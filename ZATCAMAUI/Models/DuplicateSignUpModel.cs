using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{

    class DuplicateSignUpModel
    {
    }
    
    public class DuplicateSignUpModelMetadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    
    public class DuplicateSignUpModelD
    {
        public DuplicateSignUpModelMetadata __metadata { get; set; }
        [JsonProperty("flag")]
        public string Flag { get; set; }
        [JsonProperty("error")]
        public string ErrorFlag { get; set; }
        //public string StartDt { get; set; }
        [JsonProperty("TIN")]
        public string Partner { get; set; }
        [JsonProperty("idType")]
        public string Type { get; set; }
        [JsonProperty("idNumber")]
        public string Idnumber { get; set; }
        [JsonProperty("institution")]
        public string Institute { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
    }
    
    public class DuplicateSignUpModelRootObject
    {
        [JsonProperty("result")]
        public DuplicateSignUpModelD d { get; set; }
    }
    public class DuplicateSignUpReqModel
    {
        public string TIN { get; set; }
        public string idType { get; set; }
        public string idNumber { get; set; }
        public string institution { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string startDate { get; set; }
    }
}
