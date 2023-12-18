namespace ZATCAMAUI.Models.TahqaqModels
{
    public class EinvoiceQRCodeResponse
    {
        public bool status { get; set; }
        public Data data { get; set; }
        public string error { get; set; }
        public int code { get; set; }
    }
    public class Data
    {
        public string tin { get; set; }
        public string vaTCERTNO { get; set; }
        public string name { get; set; }
        public string bldGCODE { get; set; }
        public string street { get; set; }
        public string postCode { get; set; }
        public string city { get; set; }
        public string region { get; set; }
        public string country { get; set; }
        public string vaTACTNO { get; set; }
    }
    public class ErrorResponseResult
    {
        public string messageCode { get; set; }
        public bool success { get; set; }
        public Result result { get; set; }
    }
    public class Result
    {
        public string data { get; set; }
    }
}
