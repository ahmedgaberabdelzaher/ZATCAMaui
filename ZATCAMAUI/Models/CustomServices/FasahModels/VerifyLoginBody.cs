namespace ZATCAMAUI.Models.CustomServices.FasahModels
{
    public class VerifyLoginBody
    {
        public string smsCode { get; set; }
    }
    public class ResendOtpBody
    {
        public string username { get; set; }
    }
}

