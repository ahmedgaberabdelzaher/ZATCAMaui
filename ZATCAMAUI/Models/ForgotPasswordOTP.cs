
namespace ZATCAMAUI.Models
{

    public class Metadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
 
    public class D
    {
   
        public string Action { get; set; }
        public string Tin { get; set; }
        public string Langu { get; set; }
        public int CurrAttmps { get; set; }
        public string EmailId { get; set; }
        public string TpType { get; set; }
        public string MobileNo { get; set; }
        public string SubType { get; set; }
        public string Idnumber { get; set; }
        public string Otp { get; set; }
        public int Minutes { get; set; }
        public string Name { get; set; }
        public int Attempts { get; set; }
        public string Captcha { get; set; }
        public string Application { get; set; }
        public string Guid { get; set; }

        public string NewPwd { get; set; }
        public string CnfPwd { get; set; }
        public string RdBt { get; set; }
        public string Hyperlink { get; set; }
    }
   
    public class ForgotPasswordOTP
    {
        public D d { get; set; }
    }

    public class GenerateCaptchaGUID
    {
        public GetCaptcha d { get; set; }

    }

 
    public class GetCaptcha
    {
        public Metadata __metadata { get; set; }
        public string Captcha { get; set; }
        public string Application { get; set; }
        public string Guid { get; set; }
        public string Refresh { get; set; }
        public string Taxpayer { get; set; }
    }

    public class getGuiD
    {
        public GetCaptcha d { get; set; }
        public string Idnum { get; set; }
        public string idtype { get; set; }
        public string TpDOb { get; set; }
        public string otpResponse { get; set; }
    }

    public class OTPModelD
    {
        public OtpPageResult d { get; set; }
    }

    public class OtpPageResult
    {
        public OtpMetadata __metadata { get; set; }
        public string Captcha { get; set; }
        public string Guid16 { get; set; }
        public string Idnum { get; set; }
        public string Idtype { get; set; }
        public string TaxpDob { get; set; }

    }

    public class OtpMetadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class OTPModelvalidateD
    {
        public otpVlidate d { get; set; }
    }

    public class OTPModelvalidatedD
    {
        public otpVlidateCheck d { get; set; }
    }

    public class otpVlidate
    {
        public OtpMetadata __metadata { get; set; }
        public string Captcha { get; set; }
        public string Guid16 { get; set; }
        public string Idnum { get; set; }
        public string OtpCode { get; set; }
    }
    public class otpVlidateCheck
    {
        public OtpMetadata __metadata { get; set; }
        public string Captcha { get; set; }
        public string Guid16 { get; set; }
        public string Idnum { get; set; }
        public string OtpCheck { get; set; }
        public string OtpCode { get; set; }
    }

}
