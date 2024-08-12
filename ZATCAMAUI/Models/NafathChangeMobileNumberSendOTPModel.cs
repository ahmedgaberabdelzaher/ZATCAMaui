using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZATCAMAUI.Models
{
    public class NafathChangeMobileNumberSendOTPModel
    {
        public string ErrorMsg { get; set; }
        [JsonProperty("GUID")]
        public string Guid { get; set; }
        [JsonProperty("TIN")]
        public string Partner { get; set; }
        [JsonProperty("mobileExtension")]
        public string MobExten { get; set; }
        [JsonProperty("mobileNumber")]
        public string MobNum { get; set; }
        [JsonProperty("language")]
        public string Lang { get; set; }
        [JsonProperty("sourceId")]
        public string Scrid { get; set; }
        [JsonProperty("sendResendOTP")]
        public string SendResendOtp { get; set; }

    }

    public class NafathChangeMobileNumberSendOTPResponse
    {
        [JsonProperty("result")]
        public NafathChangeMobileNumberSendOTPModel d { get; set; }
    }
}
