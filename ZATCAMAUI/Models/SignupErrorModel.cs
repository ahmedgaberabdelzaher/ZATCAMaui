using System.Runtime.Serialization;
using Foundation;

namespace ZATCAMAUI.Models
{

    class SignupErrorModel
    {
    }
    

    public class SignupErrorModelMessage
    {
        [DataMember]
        public string lang { get; set; }
        [DataMember]
        public string value { get; set; }
    }
    

    public class SignupErrorModelApplication
    {
        [DataMember]

        public string component_id { get; set; }
        [DataMember]

        public string service_namespace { get; set; }
        [DataMember]

        public string service_id { get; set; }
        [DataMember]

        public string service_version { get; set; }
    }
    

    public class SignupErrorModelErrorResolution
    {
        [DataMember]

        public string SAP_Transaction { get; set; }
        [DataMember]

        public string SAP_Note { get; set; }
    }
    

    public class SignupErrorModelErrordetail
    {
        [DataMember]

        public string code { get; set; }
        [DataMember]

        public string message { get; set; }
        [DataMember]

        public string propertyref { get; set; }
        [DataMember]

        public string severity { get; set; }
        [DataMember]

        public string target { get; set; }
    }
    

    public class SignupErrorModelInnererror
    {
        [DataMember]

        public SignupErrorModelApplication application { get; set; }
        [DataMember]

        public string transactionid { get; set; }
        [DataMember]

        public string timestamp { get; set; }
        [DataMember]

        public SignupErrorModelErrorResolution Error_Resolution { get; set; }
        [DataMember]

        public List<SignupErrorModelErrordetail> errordetails { get; set; }
    }
    

    public class SignupErrorModelError
    {
        [DataMember]

        public string code { get; set; }
        [DataMember]

        public SignupErrorModelMessage message { get; set; }
        [DataMember]

        public SignupErrorModelInnererror innererror { get; set; }
    }
    

    public class SignupErrorModelRootObject
    {
        [DataMember]

        public SignupErrorModelError error { get; set; }
        [DataMember]
        public Header header { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class ErrorDetail
    {
        [DataMember]
        public string code { get; set; }
        [DataMember]
        public string message { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class Header
    {
        [DataMember]
        public string requestID { get; set; }
        [DataMember]
        public Status status { get; set; }
        [DataMember]
        public MoreInformation moreInformation { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class MoreInformation
    {
        public List<ErrorDetail> errorDetails { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class Status
    {
        [DataMember]
        public string code { get; set; }
        [DataMember]
        public string description { get; set; }
    }

}
