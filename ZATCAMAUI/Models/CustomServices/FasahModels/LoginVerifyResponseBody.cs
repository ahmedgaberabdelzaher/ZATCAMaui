namespace ZATCAMAUI.Models.CustomServices.FasahModels
{
    public class LoginVerifyResponseBody
    {
        public int code { get; set; }
        public string name { get; set; }
        public string token { get; set; }
        public string refreshToken { get; set; }
        public object lastDigitsMobile { get; set; }
        public List<string> groups { get; set; }
        public string landingPage { get; set; }
        public string username { get; set; }
        public bool updateProfile { get; set; }
        public bool passwordExpired { get; set; }
        public bool passwordForceChange { get; set; }
        public bool impersonated { get; set; }
        public object impersonatedBy { get; set; }
        public object impersonatedDate { get; set; }
        public object firstEmailLetters { get; set; }
        public int restLengthEmailLetters { get; set; }
        public string mobile { get; set; }
        public bool isMobileEmailGlobalUpdate { get; set; }
        public string email { get; set; }
        public string message { get; set; }
    }
}

