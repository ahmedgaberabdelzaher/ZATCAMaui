
namespace ZATCAMAUI.Models
{

    public class ErrorMessage
    {
       
        public class Message
        {
            public string lang { get; set; }
            public string value { get; set; }
        }
       
        public class Application
        {
            public string component_id { get; set; }
            public string service_namespace { get; set; }
            public string service_id { get; set; }
            public string service_version { get; set; }
        }
       
        public class ErrorResolution
        {
            public string SAP_Transaction { get; set; }
            public string SAP_Note { get; set; }
        }
       
        public class Errordetail
        {
            public string code { get; set; }
            public string message { get; set; }
            public string propertyref { get; set; }
            public string severity { get; set; }
            public string target { get; set; }
        }
       
        public class Innererror
        {
            public Application application { get; set; }
            public string transactionid { get; set; }
            public string timestamp { get; set; }
            public ErrorResolution Error_Resolution { get; set; }
            public List<Errordetail> errordetails { get; set; }
        }

       
        public class Error
        {
            public string code { get; set; }
            public Message message { get; set; }
            public Innererror innererror { get; set; }
        }
       
        public class ErrorObj
        {
            public Error error { get; set; }
            public Header header { get; set; }
        }
        public class ErrorDetails
        {
            public string code { get; set; }
            public string message { get; set; }
        }

        public class Header
        {
            public string requestID { get; set; }
            public Status status { get; set; }
            public MoreInformation moreInformation { get; set; }
        }

        public class MoreInformation
        {
            public List<ErrorDetails> errorDetails { get; set; }
        }

        public class Status
        {
            public string code { get; set; }
            public string description { get; set; }
        }

    }
}
