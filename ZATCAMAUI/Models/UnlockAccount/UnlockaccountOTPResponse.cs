
namespace ZATCAMAUI.Models.UnlockAccount
{
    public class UnlockaccountOTPResponse
    {
        public Header header { get; set; }
        public Result result { get; set; }
    }
    public class Header
    {
        public string requestID { get; set; }
        public Status status { get; set; }
    }

    public class Result
    {
        public string action { get; set; }
        public string captchaCode { get; set; }
        public string taxpayerGuid { get; set; }
        public string mobileNumber { get; set; }
        public string OTP { get; set; }
        public int minutes { get; set; }
        public int attempts { get; set; }
        public string newPassword { get; set; }
        public string confirmPassword { get; set; }
        public string language { get; set; }
        public string hyperLink { get; set; }
        public string TIN { get; set; }
        public string radioButtonType { get; set; }
    }



    public class Status
    {
        public string code { get; set; }
        public string description { get; set; }
    }
}
