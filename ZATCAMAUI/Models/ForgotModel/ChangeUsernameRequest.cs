
namespace ZATCAMAUI.Models.ForgotModel
{
    public class ChangeUsernameRequest
    {
        public string captchaCode { get; set; }
        public string GUID { get; set; }
        public string idNumber { get; set; }
        public string language { get; set; }
        public string mobileNumber { get; set; }
        public string OTP { get; set; }
        public string subType { get; set; }
    }
}
