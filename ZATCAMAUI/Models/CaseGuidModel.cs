using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{

    class CaseGuidModel
    {
    }
    
    public class CaseGuidModelMetadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    
    public class CaseGuidModelResult
    {
        public CaseGuidModelMetadata __metadata { get; set; }
        public object AAgreeDt { get; set; }
        [JsonProperty("hasTIN")]
        public string ATinExist { get; set; }
        [JsonProperty("cityCode")]
        public string ACityCode { get; set; }
        public object ABirthdt { get; set; }
        [JsonProperty("external")]
        public string AExternal { get; set; }
        [JsonProperty("language")]
        public string ALang { get; set; }
        [JsonProperty("city")]
        public string ACity { get; set; }
        public object ACrexpdt { get; set; }
        [JsonProperty("internal")]
        public string AInternal { get; set; }
        [JsonProperty("idType")]
        public string AIdtype { get; set; }
        [JsonProperty("password")]
        public string APassword { get; set; }
        [JsonProperty("caseGUID")]
        public string CaseGuid { get; set; }
        [JsonProperty("issuedBy")]
        public string AIssuedBy { get; set; }
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [JsonProperty("submit")]
        public string ASubmit { get; set; }
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [JsonProperty("type")]
        public string AType { get; set; }
        [JsonProperty("firstname")]
        public string AFirstname { get; set; }
        [JsonProperty("lastName")]
        public string ALastname { get; set; }
        [JsonProperty("TIN")]
        public string ATin { get; set; }
        [JsonProperty("idNumber")]
        public string AIdnumber { get; set; }
        [JsonProperty("licenceNumber")]
        public string ALicenceNo { get; set; }
        [JsonProperty("email")]
        public string AEmail { get; set; }
        [JsonProperty("phone")]
        public string APhone { get; set; }
        [JsonProperty("mobile")]
        public string AMobile { get; set; }
        [JsonProperty("company")]
        public string ACompany { get; set; }
        [JsonProperty("SMSCode")]
        public string ASmsCode { get; set; }
        [JsonProperty("emailCode")]
        public string AEmailCode { get; set; }
        [JsonProperty("commercialId")]
        public string ACommId { get; set; }
        [JsonProperty("contractNumber")]
        public string AContNo { get; set; }
        [JsonProperty("governmentAgency")]
        public string AGovtAgency { get; set; }
        [JsonProperty("taxNumber")]
        public string ATaxNo { get; set; }
        [JsonProperty("practicingCertificate")]
        public string APractcingCert { get; set; }
        [JsonProperty("licenceDocument")]
        public string ALicDoc { get; set; }
        [JsonProperty("companyBaseId")]
        public string ACompBaseId { get; set; }
        [JsonProperty("agree")]
        public string AAgree { get; set; }
        public string AAgreeTm { get; set; }
    }
    
    public class CaseGuidModelD
    {
        public List<CaseGuidModelResult> results { get; set; }
    }
    
    public class CaseGuidModelRootObject
    {
        [JsonProperty("data")]
        public List<CaseGuidModelResult> d { get; set; }
    }
}
