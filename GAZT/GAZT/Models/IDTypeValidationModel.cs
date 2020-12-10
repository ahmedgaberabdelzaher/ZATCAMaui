using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace GAZT.Models
{
    [Preserve(AllMembers = true)]
    public class IDTypeValidationModel
    {
    }
    public class IDTypeValidateMessage
    {
        public string lang { get; set; }
        public string value { get; set; }
    }
    public class IDNumberApplication
    {
        public string component_id { get; set; }
        public string service_namespace { get; set; }
        public string service_id { get; set; }
        public string service_version { get; set; }
    }
    public class IDTypeValidateErrorResolution
    {
        public string SAP_Transaction { get; set; }
        public string SAP_Note { get; set; }
    }
    public class IDTypeValidateErrordetail
    {
        public string code { get; set; }
        public string message { get; set; }
        public string propertyref { get; set; }
        public string severity { get; set; }
        public string target { get; set; }
    }
    public class IDTypeValidateInnererror
    {
        public IDNumberApplication application { get; set; }
        public string transactionid { get; set; }
        public string timestamp { get; set; }
        public IDTypeValidateErrorResolution Error_Resolution { get; set; }
        public List<IDTypeValidateErrordetail> errordetails { get; set; }
    }
    public class IDTypeValidateError
    {
        public string code { get; set; }
        public IDTypeValidateMessage message { get; set; }
        public IDTypeValidateInnererror innererror { get; set; }
    }
    public class IDTypeValidateRootObject
    {
        public IDTypeValidateError error { get; set; }
    }
}
