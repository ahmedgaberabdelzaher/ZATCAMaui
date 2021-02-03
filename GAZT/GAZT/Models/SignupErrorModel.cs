using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Xamarin.Forms.Internals;

namespace GAZT.Models
{
    [Preserve(AllMembers = true)]
    class SignupErrorModel
    {
    }
    [Preserve(AllMembers = true)]

    public class SignupErrorModelMessage
    {
        [DataMember]
        public string lang { get; set; }
        [DataMember]
        public string value { get; set; }
    }
    [Preserve(AllMembers = true)]

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
    [Preserve(AllMembers = true)]

    public class SignupErrorModelErrorResolution
    {
        [DataMember]

        public string SAP_Transaction { get; set; }
        [DataMember]

        public string SAP_Note { get; set; }
    }
    [Preserve(AllMembers = true)]

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
    [Preserve(AllMembers = true)]

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
    [Preserve(AllMembers = true)]

    public class SignupErrorModelError
    {
        [DataMember]

        public string code { get; set; }
        [DataMember]

        public SignupErrorModelMessage message { get; set; }
        [DataMember]

        public SignupErrorModelInnererror innererror { get; set; }
    }
    [Preserve(AllMembers = true)]

    public class SignupErrorModelRootObject
    {
        [DataMember]

        public SignupErrorModelError error { get; set; }
    }
}
