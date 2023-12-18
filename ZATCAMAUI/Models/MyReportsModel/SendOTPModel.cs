namespace ZATCAMAUI.Models.MyReportsModel
{
    public class SendOTPModel
    {
        public string status { get; set; }
        public KeyModel data { get; set; }
        public string code { get; set; }
    }

    public class KeyModel
    {
        public string key { get; set; }
    }
}

