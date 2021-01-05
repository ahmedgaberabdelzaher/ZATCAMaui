using System;
using Newtonsoft.Json;
using Xamarin.Forms.Internals;

namespace GAZT.Models
{
    [Preserve(AllMembers = true)]
    public partial class LoginModel
    {
        public string token { get; set; }
        public string message { get; set; }
        public int failedAttempts { get; set; }
        public int invalidAttempts { get; set; }
        public string otpAttempts { get; set; }

        [JsonProperty("__metadata")]
        public LoginMetadata Metadata { get; set; }

        [JsonProperty("Guid")]
        public string FbGuid { get; set; }

        [JsonProperty("Euser")]
        public string Euser { get; set; }

        [JsonProperty("DeviceFlag")]
        public string DeviceFlag { get; set; }

        [JsonProperty("VtReg")]
        public string VtReg { get; set; }

        [JsonProperty("EtReg")]
        public string EtReg { get; set; }

        [JsonProperty("ZkReg")]
        public string ZkReg { get; set; }

        [JsonProperty("ExeDate")]
        public DateTimeOffset ExeDate { get; set; }

        [JsonProperty("DeviceId")]
        public string DeviceId { get; set; }

        [JsonProperty("Gpart")]
        public string TIN { get; set; }

        [JsonProperty("FcmId")]
        public string FcmId { get; set; }

        [JsonProperty("DeviceTyp")]
        public string DeviceTyp { get; set; }

        [JsonProperty("DeviceToken")]
        public string DeviceToken { get; set; }

        [JsonProperty("Msgtitle")]
        public string MsgTitle { get; set; }

        [JsonProperty("Appmsg")]
        public string AppMsg { get; set; }

        [JsonProperty("Appversion")]
        public string AppVersion { get; set; }

        [JsonProperty("Emailid")]
        public string Emailid { get; set; }

        [JsonProperty("ZkSignup")]
        public string ZkSignup { get; set; }
        [JsonProperty("VtSignup")]
        public string VtSignup { get; set; }
        [JsonProperty("EpSignup")]
        public string EpSignup { get; set; }
        [JsonProperty("EtSignup")]
        public string EtSignup { get; set; }

        [JsonProperty("NameFirst")]
        public string NameFirst { get; set; }
        [JsonProperty("NameLast")]
        public string NameLast { get; set; }
        [JsonProperty("NameOrg1")]
        public string NameOrg1 { get; set; }
        [JsonProperty("TypeChk")]
        public string TypeChk { get; set; }

        public string ResponseStatusMessage { get; set; }

        public string ResponseStatusCode { get; set; }
    }

    [Preserve(AllMembers = true)]
    public partial class LoginMetadata
    {
        [JsonProperty("id")]
        public Uri Id { get; set; }

        [JsonProperty("uri")]
        public Uri Uri { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    [Preserve(AllMembers = true)]
    public class LoginParametersModel
    {
        public string userId { get; set; }
        public string lang { get; set; }
        public string password { get; set; }
        public string deviceId { get; set; }
        public string count { get; set; }
    }
}