
namespace ZATCAMAUI.Models.ForgotModel
{
    public class CAptchRequest
    {
        public string GUID { get; set; }
        public string captchaCode { get; set; }
        public string taxpayer { get; set; }
        public string refresh { get; set; }
        public string applicationName { get; set; }
    }
}
