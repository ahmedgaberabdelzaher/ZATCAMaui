using System;
using Newtonsoft.Json;
using Xamarin.Forms.Internals;

namespace EGAZT.Models
{
    [Preserve(AllMembers = true)]
    public class UnlockAccountModel
    {
        [JsonProperty("Action")]
        public string Action { get; set; }

        [JsonProperty("Langu")]
        public string Language { get; set; }

        [JsonProperty("RdBt")]
        public string UserLocked { get; set; }

        [JsonProperty("TaxpayerGuid")]
        public string TaxpayerGuid { get; set; }
        
        [JsonProperty("Zcaptcha")]
        public string Zcaptcha { get; set; }

        [JsonProperty("Tin")]
        public string Tin { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class UnlockAccountModelOtp:UnlockAccountModel
    {
        [JsonProperty("TaxpayerGuid")]
        public string TaxpayerGuid { get; set; }

        [JsonProperty("Zcaptcha")]
        public string Zcaptcha { get; set; }

        [JsonProperty("Otp")]
        public string Otp { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class UnlockAccountModelChangePassword : UnlockAccountModel
    {
        [JsonProperty("NewPwd")]
        public string NewPassword { get; set; }

        [JsonProperty("CnfPwd")]
        public string ConfirmPassword { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class UnlockAccountResponseModel
    {
        [JsonProperty("d")]
        public UnlockReponse_D D { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class UnlockReponse_D
    {
        [JsonProperty("__metadata")]
        public UnlockReponse_Metadata Metadata { get; set; }

        [JsonProperty("Action")]
        public string Action { get; set; }

        [JsonProperty("MobileNo")]
        public string MobileNo { get; set; }

        [JsonProperty("Otp")]
        public string Otp { get; set; }

        [JsonProperty("Minutes")]
        public long Minutes { get; set; }

        [JsonProperty("Attempts")]
        public long Attempts { get; set; }

        [JsonProperty("NewPwd")]
        public string NewPwd { get; set; }

        [JsonProperty("CnfPwd")]
        public string CnfPwd { get; set; }

        [JsonProperty("RdBt")]
        public string RdBt { get; set; }

        [JsonProperty("Hyperlink")]
        public string Hyperlink { get; set; }

        [JsonProperty("Langu")]
        public string Langu { get; set; }

        [JsonProperty("Tin")]
        public string Tin { get; set; }
    }
    [Preserve(AllMembers = true)]
    public partial class UnlockReponse_Metadata
    {
        [JsonProperty("id")]
        public Uri Id { get; set; }

        [JsonProperty("uri")]
        public Uri Uri { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

}
