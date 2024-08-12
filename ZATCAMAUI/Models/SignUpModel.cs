using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{

    class SignUpModel
    {
    }
    public class SignUpModelMetadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    [Serializable]
    
    [DataContract]
    public class SignUpModelD
    {
        [DataMember]
        public SignUpModelMetadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("agreedDate")]
        public string AAgreeDt { get; set; }
        [DataMember]
        [JsonProperty("hasTIN")]
        public string ATinExist { get; set; }
        [DataMember]
        [JsonProperty("cityCode")]
        public string ACityCode { get; set; }
        [DataMember]
        [JsonProperty("birthDate")]
        public string ABirthdt { get; set; }
        [DataMember]
        [JsonProperty("external")]
        public string AExternal { get; set; }
        [DataMember]
        [JsonProperty("language")]
        public string ALang { get; set; }
        [DataMember]
        [JsonProperty("city")]
        public string ACity { get; set; }
        [DataMember]
        [JsonProperty("CRExpiryDate")]
        public string ACrexpdt { get; set; }
        [DataMember]
        [JsonProperty("internal")]
        public string AInternal { get; set; }
        [DataMember]
        [JsonProperty("idType")]
        public string AIdtype { get; set; }
        [DataMember]
        [JsonProperty("password")]
        public string APassword { get; set; }
        [DataMember]
        [JsonProperty("caseGuid")]
        public string CaseGuid { get; set; }
        [DataMember]
        [JsonProperty("issuedBy")]
        public string AIssuedBy { get; set; }
        [DataMember]
        [JsonProperty("formGuid")]
        public string FormGuid { get; set; }
        [DataMember]
        [JsonProperty("submit")]
        public string ASubmit { get; set; }
        [DataMember]
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [DataMember]
        [JsonProperty("type")]
        public string AType { get; set; }
        [DataMember]
        [JsonProperty("firstName")]
        public string AFirstname { get; set; }
        [DataMember]
        [JsonProperty("lastName")]
        public string ALastname { get; set; }
        [DataMember]
        [JsonProperty("TIN")]
        public string ATin { get; set; }
        [DataMember]
        [JsonProperty("idNumber")]
        public string AIdnumber { get; set; }
        [DataMember]
        [JsonProperty("licenceNumber")]
        public string ALicenceNo { get; set; }
        [DataMember]
        [JsonProperty("email")]
        public string AEmail { get; set; }
        [DataMember]
        [JsonProperty("phone")]
        public string APhone { get; set; }
        [DataMember]
        [JsonProperty("mobile")]
        public string AMobile { get; set; }
        [DataMember]
        [JsonProperty("company")]
        public string ACompany { get; set; }
        [DataMember]
        [JsonProperty("SMSCode")]
        public string ASmsCode { get; set; }
        [DataMember]
        [JsonProperty("emailCode")]
        public string AEmailCode { get; set; }
        [DataMember]
        [JsonProperty("commercialId")]
        public string ACommId { get; set; }
        [DataMember]
        [JsonProperty("contractNumber")]
        public string AContNo { get; set; }
        [DataMember]
        [JsonProperty("governmentAgency")]
        public string AGovtAgency { get; set; }
        [DataMember]
        [JsonProperty("taxNumber")]
        public string ATaxNo { get; set; }
        [DataMember]
        [JsonProperty("practicingCertificate")]
        public string APractcingCert { get; set; }
        [DataMember]
        [JsonProperty("licenceDocument")]
        public string ALicDoc { get; set; }
        [DataMember]
        [JsonProperty("companyBaseId")]
        public string ACompBaseId { get; set; }
        [DataMember]
        [JsonProperty("agree")]
        public string AAgree { get; set; }
        [DataMember]
        [JsonProperty("agreedTime")]
        public string AAgreeTm { get; set; }
        [DataMember]
        [JsonProperty("country")]
        public string ACountry { get; set; }

        [DataMember]
        [JsonProperty("captcha")]
        public string ACaptcha { get; set; }
        
    }
    public class SignupDetails
    {
        [JsonProperty("signupDetails")]
        public SignUpModelD signupD { get; set; }
    }
    
    public class SignUpModelRootObject
    {
        [JsonProperty("result")]
        public SignupDetails d { get; set; }
    }
}
