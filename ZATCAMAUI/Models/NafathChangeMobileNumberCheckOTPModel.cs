using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZATCAMAUI.Models
{
    public class NafathChangeMobileNumberCheckOTPModel
    {
        [JsonProperty("errorMessage")]
        public string ErrorMsg { get; set; }
        [JsonProperty("GUID")]
        public string Guid { get; set; }
        [JsonProperty("language")]
        public string Lang { get; set; }
        [JsonProperty("sourceId")]
        public string Scrid { get; set; }
        [JsonProperty("OTP")]
        public string Otp { get; set; }
    }

    public class NafathChangeMobileNumberCheckOTPModelResponse
    {
        [JsonProperty("result")]
        public NafathChangeMobileNumberCheckOTPModel d;
    }
}
