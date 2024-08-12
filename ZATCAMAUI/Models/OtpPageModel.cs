
using Foundation;

namespace ZATCAMAUI.Models
{
    [Preserve(AllMembers = true)]
    class OtpPageModel
	{
    }

    [Preserve(AllMembers = true)]
    public class OtpMetadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    [Preserve(AllMembers = true)]
    public class OtpPageResult
    {
        public OtpMetadata __metadata { get; set; }
        public string Captcha { get; set; }
        public string Guid16 { get; set; }
        public string Idnum { get; set; }
        public string Idtype { get; set; }
        public string TaxpDob { get; set; }

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

    [Preserve(AllMembers = true)]
    public class OTPModelD
    {
        public OtpPageResult d { get; set; }
    }

    [Preserve(AllMembers = true)]
    public class OTPModelvalidateD
    {
        public otpVlidate d { get; set; }
    }

    [Preserve(AllMembers = true)]
    public class OTPModelvalidatedD
    {
        public otpVlidateCheck d { get; set; }
    }
    
}

