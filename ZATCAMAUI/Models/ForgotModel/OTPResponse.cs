namespace ZATCAMAUI.Models.ForgotModel
{
    public class OTPResponse
    {
        public Header header { get; set; }
        public Result result { get; set; }

        public class Header
        {
            public string requestID { get; set; }
            public Status status { get; set; }
        }

        public class Result
        {
            public string action { get; set; }
            public string applicationName { get; set; }
            public string attempts { get; set; }
            public string captchaCode { get; set; }
            public string confirmPassword { get; set; }
            public string currentAttempts { get; set; }
            public DateTime birthDate { get; set; }
            public string email { get; set; }
            public string GUID { get; set; }
            public string hyperlink { get; set; }
            public string idNumber { get; set; }
            public string language { get; set; }
            public int minutes { get; set; }
            public string mobileNumber { get; set; }
            public string name { get; set; }
            public string newPassword { get; set; }
            public string OTP { get; set; }
            public string radioButtonType { get; set; }
            public string subType { get; set; }
            public string TIN { get; set; }
            public string taxpayerType { get; set; }
        }

        public class Status
        {
            public string code { get; set; }
            public string description { get; set; }
        }
    }
}
