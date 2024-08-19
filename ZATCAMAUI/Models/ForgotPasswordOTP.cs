
using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{
    
    
    public class Metadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    
    public class ForgotPasswordOTP1
    {
        [JsonProperty("action")]
        public string Action { get; set; }
        [JsonProperty("language")]
        public string Langu { get; set; }
        [JsonProperty("taxpayerType")]
        public string TpType { get; set; }
        [JsonProperty("mobileNumber")]
        public string MobileNo { get; set; }
        [JsonProperty("subType")]
        public string SubType { get; set; }
        [JsonProperty("idNumber")]
        public string Idnumber { get; set; }
        [JsonProperty("OTP")]
        public string Otp { get; set; }
        [JsonProperty("captchaCode")]
        public string Captcha { get; set; }
        [JsonProperty("applicationName")]
        public string Application { get; set; }
        [JsonProperty("GUID")]
        public string Guid { get; set; }

        public static implicit operator ForgotPasswordOTP1(ForgotPasswordOTP v)
        {
            throw new NotImplementedException();
        }
        [JsonProperty("radioButtonType")]
        public string RdBt { get; set; }
    }
    
    public class D
    {
        [JsonProperty("action")]
        public string Action { get; set; }
        [JsonProperty("TIN")]
        public string Tin { get; set; }
        [JsonProperty("language")]
        public string Langu { get; set; }
        [JsonProperty("currentAttempts")]
        public int CurrAttmps { get; set; }
        [JsonProperty("email")]
        public string EmailId { get; set; }
        [JsonProperty("taxpayerType")]
        public string TpType { get; set; }
        [JsonProperty("mobileNumber")]
        public string MobileNo { get; set; }
        [JsonProperty("subType")]
        public string SubType { get; set; }
        [JsonProperty("idNumber")]
        public string Idnumber { get; set; }
        [JsonProperty("OTP")]
        public string Otp { get; set; }
        [JsonProperty("minutes")]
        public int Minutes { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("attempts")]
        public int Attempts { get; set; }
        [JsonProperty("captchaCode")]
        public string Captcha { get; set; }
        [JsonProperty("applicationName")]
        public string Application { get; set; }
        [JsonProperty("GUID")]
        public string Guid { get; set; }
        //public string Refresh { get; set; }
        // public string Taxpayer { get; set; }
        [JsonProperty("birthDate")]
        public DateTime Dob { get; set; }
        [JsonProperty("newPassword")]
        public string NewPwd { get; set; }
        [JsonProperty("confirmPassword")]
        public string CnfPwd { get; set; }
        [JsonProperty("radioButtonType")]
        public string RdBt { get; set; }
        [JsonProperty("hyperlink")]
        public string Hyperlink { get; set; }
    }
    
    public class ForgotPasswordOTP
    {
        [JsonProperty("result")]
        public D d { get; set; }
    }

    public class GenerateCaptchaGUID
    {
        public GetCaptcha result { get; set; }

    }

    
    public class GetCaptcha
    {
        public Metadata __metadata { get; set; }
        public string captchaCode { get; set; }
        public string applicationName { get; set; }
        public string GUID { get; set; }
        public string refresh { get; set; }
        public string taxpayer { get; set; }
    }

    public class getGuiD
    {
        public GetCaptcha d { get; set; }
        public string Idnum { get; set; }
        public string idtype { get; set; }
        public string TpDOb { get; set; }
        public string Captcha { get; set; }
    }


}
