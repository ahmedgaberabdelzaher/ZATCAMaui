using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{

    public class CreateGaztAccountModel
    {
        [JsonProperty("language")]
        public string ALang { get; set; }
        [JsonProperty("type")]
        public string AType { get; set; }
        [JsonProperty("firstName")]
        public string AFirstname { get; set; }
        [JsonProperty("lastName")]
        public string ALastname { get; set; }
        [JsonProperty("idNumber")]
        public string AIdnumber { get; set; }
        [JsonProperty("commercialId")]
        public string ACommId { get; set; }
        [JsonProperty("email")]
        public string AEmail { get; set; }
        [JsonProperty("phone")]
        public string APhone { get; set; }
        [JsonProperty("mobile")]
        public string AMobile { get; set; }
        [JsonProperty("issuedBy")]
        public string AIssuedBy { get; set; }
        [JsonProperty("city")]
        public string ACity { get; set; }
        [JsonProperty("idType")]
        public string AIdtype { get; set; }
        [JsonProperty("birthDate")]
        public string ABirthdt { get; set; }
        [JsonProperty("hasTIN")]
        public string ATinExist { get; set; }
        [JsonProperty("caseGuid")]
        public string CaseGuid { get; set; }
        [JsonProperty("TIN")]
        public string ATin { get; set; }
        [JsonProperty("licenceNumber")]
        public string ALicenceNo { get; set; }
        [JsonProperty("cityCode")]
        public string ACityCode { get; set; }
        [JsonProperty("SMSCode")]
        public string ASmsCode { get; set; }
        [JsonProperty("emailCode")]
        public string AEmailCode { get; set; }
        [JsonProperty("password")]
        public string APassword { get; set; }
        [JsonProperty("submit")]
        public string ASubmit { get; set; }
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        [JsonProperty("country")]
        public string ACountry { get; set; }
        [JsonProperty("absherGUID")]
        public string AAbsherGuid { get; set; }
        [JsonProperty("absherOTP")]
        public string AAbsherOtp { get; set; }
        [JsonProperty("captcha")]
        public string ACaptcha { get; set; }
        [JsonProperty("iqamaDescription")]
        public string AIqamaDesc { get; set; }
        [JsonProperty("iqamaFlag")]
        public string AIqamaFg { get; set; }
    }
}
