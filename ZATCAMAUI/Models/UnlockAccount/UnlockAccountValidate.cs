
namespace ZATCAMAUI.Models.UnlockAccount
{
    public class UnlockAccountValidate
    {
        public string TIN { get; set; }
        public string OTP { get; set; }
        public string taxpayerGuid { get; set; }
        public string captchaCode { get; set; }
        public string language { get; set; }
    }
}
