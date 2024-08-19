using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{

    public class VATSignUpCaseId
    {
        public Header header { get; set; }
        public List<Datum> data { get; set; }
    }
    
    public class Datum
    {
        public string activityCategory { get; set; }
        public string URL { get; set; }
        public string external { get; set; }
        public string captchaCode { get; set; }
        public string mobileCountry { get; set; }
        public string activityNumber { get; set; }
        public string addressType { get; set; }
        public string @internal { get; set; }
        public string authorizationGroup { get; set; }
        public string buildingCode { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string country { get; set; }
        public string email { get; set; }
        public string emailCode { get; set; }
        public string formBundleNumber { get; set; }
        public string firstName { get; set; }
        public string floor { get; set; }
        public string houseNumber1 { get; set; }
        public string houseNumber2 { get; set; }
        public string idNumber { get; set; }
        public string idType { get; set; }
        public string lastName { get; set; }
        public string mobile { get; set; }
        public string password { get; set; }
        public string postCode { get; set; }
        public string region { get; set; }
        public string SMSCode { get; set; }
        public string source { get; set; }
        public string street { get; set; }
        public string street1 { get; set; }
        public string street2 { get; set; }
        public string submit { get; set; }
        public string TIN { get; set; }
        public string type { get; set; }
        public string GUID { get; set; }
        public string caseGUID { get; set; }
        public string formGUID { get; set; }
        public bool isSameAsPhyiscal { get; set; }
        public string CRLicenseNumber { get; set; }
    }

    
    public class VATSignUpCaseIdResults
    {
        [DataMember]
        public __metadata __metadata { get; set; }
        [DataMember]
        public string activityCategory { get; set; }
        [DataMember]
        public string URL { get; set; }
        [DataMember]
        public string captchaCode { get; set; }
        [DataMember]
        public string GUID { get; set; }
        [DataMember]
        public string mobileCountry { get; set; }
        [DataMember]
        public string activityNumber { get; set; }
        [DataMember]
        public string addressType { get; set; }
        [DataMember]
        public string external { get; set; }
        [DataMember]
        [JsonProperty("internal")]
        public string Internal { get; set; }
        [DataMember]
        public string authorizationGroup { get; set; }
        [DataMember]
        public string buildingCode { get; set; }
        [DataMember]
        public string caseGUID { get; set; }
        [DataMember]
        public string city { get; set; }
        [DataMember]
        public string district { get; set; }
        [DataMember]
        public string country { get; set; }
        [DataMember]
        public string CRLicenseNumber { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string emailCode { get; set; }
        [DataMember]
        public string formBundleNumber { get; set; }
        [DataMember]
        public string firstName { get; set; }
        [DataMember]
        public string floor { get; set; }
        [DataMember]
        public string formGUID { get; set; }
        [DataMember]
        public string houseNumber1 { get; set; }
        [DataMember]
        public string houseNumber2 { get; set; }
        [DataMember]
        public string idNumber { get; set; }
        [DataMember]
        public string idType { get; set; }
        [DataMember]
        public string lastName { get; set; }
        [DataMember]
        public string mobile { get; set; }
        [DataMember]
        public string password { get; set; }
        [DataMember]
        public string postCode { get; set; }
        [DataMember]
        public string region { get; set; }
        [DataMember]
        public bool isSameAsPhyiscal { get; set; }
        [DataMember]
        public string SMSCode { get; set; }
        [DataMember]
        public string source { get; set; }
        [DataMember]
        public string street { get; set; }
        [DataMember]
        public string street1 { get; set; }
        [DataMember]
        public string street2 { get; set; }
        [DataMember]
        public string submit { get; set; }
        [DataMember]
        public string TIN { get; set; }
        [DataMember]
        public string type { get; set; }

    }

    
    public class VATSignUpCaseIdD
    {
        [DataMember]
        public List<VATSignUpCaseIdResults> results { get; set; }

    }

}
