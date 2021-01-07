using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace GAZT.Models
{
    [Preserve(AllMembers = true)]
    class SignupErrorModel
    {
    }
    public class SignupErrorModelMessage
    {
        public string lang { get; set; }
        public string value { get; set; }
    }
    public class SignupErrorModelApplication
    {
        public string component_id { get; set; }
        public string service_namespace { get; set; }
        public string service_id { get; set; }
        public string service_version { get; set; }
    }
    public class SignupErrorModelErrorResolution
    {
        public string SAP_Transaction { get; set; }
        public string SAP_Note { get; set; }
    }
    public class SignupErrorModelErrordetail
    {
        public string code { get; set; }
        public string message { get; set; }
        public string propertyref { get; set; }
        public string severity { get; set; }
        public string target { get; set; }
    }
    public class SignupErrorModelInnererror
    {
        public SignupErrorModelApplication application { get; set; }
        public string transactionid { get; set; }
        public string timestamp { get; set; }
        public SignupErrorModelErrorResolution Error_Resolution { get; set; }
        public List<SignupErrorModelErrordetail> errordetails { get; set; }
    }
    public class SignupErrorModelError
    {
        public string code { get; set; }
        public SignupErrorModelMessage message { get; set; }
        public SignupErrorModelInnererror innererror { get; set; }
    }
    public class SignupErrorModelRootObject
    {
        public SignupErrorModelError error { get; set; }
    }
}
