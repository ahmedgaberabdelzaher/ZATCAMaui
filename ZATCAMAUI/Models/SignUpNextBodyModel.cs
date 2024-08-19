using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{

    public class SignUpNextBodyModel
    {

        [DataMember]

        [JsonProperty("absherGUID")]
        public string AAbsherGuid { get; set; } = "";
        [DataMember]
        [JsonProperty("absherOTP")]
        public string AAbsherOtp { get; set; } = "";
        
        [DataMember]
        [JsonProperty("language")]
        public string ALang { get; set; }
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
        [JsonProperty("taxpayerTitle")]
        public string TpTitle { get; set; } = "";
        [DataMember]
        [JsonProperty("idNumber")]
        public string AIdnumber { get; set; }
        [DataMember]
        [JsonProperty("commercialId")]
        public string ACommId { get; set; }
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
        [JsonProperty("issuedBy")]
        public string AIssuedBy { get; set; }
        [DataMember]
        [JsonProperty("city")]
        public string ACity { get; set; }
        [DataMember]
        [JsonProperty("idType")]
        public string AIdtype { get; set; }
        [DataMember]
        [JsonProperty("birthDate")]
        public string ABirthdt { get; set; }
        [DataMember]
        [JsonProperty("hasTIN")]
        public string ATinExist { get; set; }
        [DataMember]
        [JsonProperty("caseGuid")]
        public string CaseGuid { get; set; }
        [DataMember]
        [JsonProperty("TIN")]
        public string ATin { get; set; }
        [DataMember]
        [JsonProperty("licenceNumber")]
        public string ALicenceNo { get; set; }
        [DataMember]
        [JsonProperty("cityCode")]
        public string ACityCode { get; set; }
        [DataMember]
        [JsonProperty("country")]
        public string ACountry { get; set; }

        [DataMember]
        [JsonProperty("captcha")]
        public string ACaptcha { get; set; }
        [DataMember]
        [JsonProperty("iqamaType")]
        public string AIqamaType { get; set; } = "";
        [DataMember]
        [JsonProperty("iqamaDescription")]
        public string AIqamaDesc { get; set; }
        [DataMember]
        [JsonProperty("iqamaFlag")]
        public string AIqamaFg { get; set; }
    }
}
