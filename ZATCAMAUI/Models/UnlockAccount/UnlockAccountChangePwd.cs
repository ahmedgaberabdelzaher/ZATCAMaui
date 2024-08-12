
namespace ZATCAMAUI.Models.UnlockAccount
{
    public class UnlockAccountChangePwd
    {
        public string TIN { get; set; }
        public string newPassword { get; set; }
        public string confirmPassword { get; set; }
        public string OTP { get; set; }
        public string language { get; set; }
        public string taxpayerGuid { get; set; }
        public string captchaCode { get; set; }

    }
}
