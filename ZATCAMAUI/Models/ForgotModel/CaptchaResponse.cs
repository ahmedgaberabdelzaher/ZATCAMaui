
namespace ZATCAMAUI.Models.ForgotModel
{
    public class CaptchaResponse
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
            public string idNumber { get; set; }
            public string refresh { get; set; }
            public string applicationName { get; set; }
            public string taxpayer { get; set; }
            public string captchaCode { get; set; }
            public string GUID { get; set; }
        }

        public class Status
        {
            public string code { get; set; }
            public string description { get; set; }
        }
    }

}
