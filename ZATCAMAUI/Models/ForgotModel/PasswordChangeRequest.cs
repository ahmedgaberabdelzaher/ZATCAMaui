
namespace ZATCAMAUI.Models.ForgotModel
{
    public class PasswordChangeRequest
    {
        public string captchaCode { get; set; }
        public string GUID { get; set; }
        public string language { get; set; }
        public string newPassword { get; set; }
        public string TIN { get; set; }
    }
}
