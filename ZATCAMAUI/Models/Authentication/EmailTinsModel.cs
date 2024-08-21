
using Newtonsoft.Json;
namespace ZATCAMAUI.Models.Authentication
{
    
    public class EmailTinsModel
    {
        [JsonProperty("header")]
        public Header Header;

        [JsonProperty("data")]
        public List<Datum> Data;
    }
    
    public class Datum
    {
        [JsonProperty("TINNumber")]
        public string TINNumber;

        [JsonProperty("email")]
        public string Email;

        [JsonProperty("taxpayerType")]
        public string TaxpayerType;
    }
    
    public class Header
    {
        [JsonProperty("requestID")]
        public string RequestID;

        [JsonProperty("status")]
        public Status Status;
    }
    
    public class Status
    {
        [JsonProperty("code")]
        public string Code;

        [JsonProperty("description")]
        public string Description;
    }
}

