
using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{
    public class TokenRequestModel
    {
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("lang")]
        public string Lang { get; set; }
        [JsonProperty("otp")]
        public string OTP { get; set; }
        [JsonProperty("sourceType")]
        public string SourceType { get; set; }
        [JsonProperty("osname")]
        public string OsName { get; set; }
        [JsonProperty("browsername")]
        public string BrowserName { get; set; }
        [JsonProperty("latitude")]
        public string Latitude { get; set; }
        [JsonProperty("longitude")]
        public string Longitude { get; set; }
        [JsonProperty("ipaddress")]
        public string IpAddress { get; set; }

    }

    public class TokenResponseModel
    {
        [JsonProperty("result")]
        public TokenResultModel Result { get; set; }
        [JsonProperty("header")]
        public HeaderModel Header { get; set; }
    }

    public class TokenErrorModel
    {
        [JsonProperty("result")]
        public TokenResultErrorModel Result { get; set; }
        [JsonProperty("header")]
        public HeaderModel Header { get; set; }
    }
    public class TokenResultErrorModel
    {
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }
        [JsonProperty("error_code")]
        public string ErrorCode { get; set; }
        [JsonProperty("token")]
        public string ErrorToken { get; set; }
    }

    public class TokenResultModel
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }
        [JsonProperty("mobileNumber")]
        public string MobileNumber { get; set; }
    }

    public class ResentTokenRequestModel
    {
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("lang")]
        public string Lang { get; set; }
    }


}
