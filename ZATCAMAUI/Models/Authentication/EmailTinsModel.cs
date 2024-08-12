using Foundation;
using Newtonsoft.Json;
namespace ZATCAMAUI.Models.Authentication
{
    [Preserve(AllMembers = true)]
    public class EmailTinsModel
    {
        [JsonProperty("header")]
        public Header Header;

        [JsonProperty("data")]
        public List<Datum> Data;
    }
    [Preserve(AllMembers = true)]
    public class Datum
    {
        [JsonProperty("TINNumber")]
        public string TINNumber;

        [JsonProperty("email")]
        public string Email;

        [JsonProperty("taxpayerType")]
        public string TaxpayerType;
    }
    [Preserve(AllMembers = true)]
    public class Header
    {
        [JsonProperty("requestID")]
        public string RequestID;

        [JsonProperty("status")]
        public Status Status;
    }
    [Preserve(AllMembers = true)]
    public class Status
    {
        [JsonProperty("code")]
        public string Code;

        [JsonProperty("description")]
        public string Description;
    }
}

