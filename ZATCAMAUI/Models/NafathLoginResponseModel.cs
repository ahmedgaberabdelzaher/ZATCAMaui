


namespace ZATCAMAUI.Models
{
    
    public class NafathLoginResponseModel
    {
        //public NafathLoginRequestModel d { get; set; }
        public string navigation { get; set; }

        public NafathLoginResponse result;
    }

    public class NafathLoginResponse
    {
        public string transactionId { get; set; }
        public string randomNumber { get; set; }
        public string idNumber { get; set; }
        public string language { get; set; }
        public string responseTime { get; set; }
        public string returnId { get; set; }
        public string inputChannel { get; set; }
        public string processType { get; set; }
        public string APICall { get; set; }
        public string statusCode { get; set; }
        public string statusDescription { get; set; }
    }

}
