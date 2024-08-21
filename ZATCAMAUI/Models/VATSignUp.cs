using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{

    public class VATSignUp
    {
        [DataMember]
        [JsonProperty("result")]
        public VATSignUpD d { get; set; }
    }
    
    public class ValidateVATSignupTaxpayerRequest
    {
        [DataMember]
        public string country { get; set; }
        [DataMember]
        public string taxpayerBirthDate { get; set; }
        [DataMember]
        public string passExpiryDate { get; set; }
        [DataMember]
        public string TIN { get; set; }
        [DataMember]
        public string idType { get; set; }
        [DataMember]
        public string idNumber { get; set; }
    }


    //
    //public class __metadata
    //{
    //    [DataMember]
    //    public string id { get; set; }
    //    [DataMember]
    //    public string uri { get; set; }
    //    [DataMember]
    //    public string type { get; set; }

    //}
  
    public class VATSignUpD
    {
        //[DataMember]
        //public _metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("birthDate")]
        public string Birthdt { get; set; }

        [DataMember]
        [JsonProperty("partnerKind")]
        public string Bpkind { get; set; }
        [DataMember]
        [JsonProperty("country")]
        public string country { get; set; }
        [DataMember]
        [JsonProperty("idIssueingCountry")]
        public string idIssueingCountry { get; set; }
        [DataMember]
        [JsonProperty("source")]
        public string source { get; set; }
        [DataMember]
        [JsonProperty("taxpayerBirthDate")]
        public string TaxpDob { get; set; }
        [DataMember]
        [JsonProperty("passExpiryDate")]
        public string PassExpDt { get; set; }
        [DataMember]
        [JsonProperty("title")]
        public string title { get; set; }
        [DataMember]
        [JsonProperty("fullName")]
        public string fullName { get; set; }
        [DataMember]
        [JsonProperty("floor")]
        public string floor { get; set; }
        [DataMember]
        [JsonProperty("TIN")]
        public string TIN { get; set; }
        [DataMember]
        [JsonProperty("additionalNumber")]
        public string additionalNumber { get; set; }
        [DataMember]
        [JsonProperty("houseNumber")]
        public string houseNumber { get; set; }
        [DataMember]
        [JsonProperty("idType")]
        public string Idtype { get; set; }
        [DataMember]
        [JsonProperty("birthDateCalendarType")]
        public string birthDateCalendarType { get; set; }
        [DataMember]
        public string buildingNumber { get; set; }
        [DataMember]
        public string birthDate10 { get; set; }
        [DataMember]
        [JsonProperty("idNumber")]
        public string Idnum { get; set; }
        [DataMember]
        [JsonProperty("fatherName")]
        public string fatherName { get; set; }
        [DataMember]
        public string poBox { get; set; }
        [DataMember]
        [JsonProperty("grandfatherName")]
        public string grandfatherName { get; set; }
        [DataMember]
        [JsonProperty("street1")]
        public string street1 { get; set; }
        [DataMember]
        [JsonProperty("familyName")]
        public string familyName { get; set; }
        [DataMember]
        [JsonProperty("street2")]
        public string street2 { get; set; }
        [DataMember]
        [JsonProperty("initials")]
        public string initials { get; set; }
        [DataMember]
        public string province { get; set; }
        [DataMember]
        public string city { get; set; }
        [DataMember]
        public string quarter { get; set; }
        [DataMember]
        public string postalCode { get; set; }
        [DataMember]
        public string telephone { get; set; }
        [DataMember]
        public string faxNumber { get; set; }
        [DataMember]
        public string mobile { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string deafultCommunication { get; set; }
        [DataMember]
        [JsonProperty("addressNumber")]
        public string Adrnr { get; set; }
        [DataMember]
        public string website { get; set; }
        [DataMember]
        public string authorizationGroup { get; set; }
        [DataMember]
        public string branchDescription { get; set; }
        [DataMember]
        [JsonProperty("name1")]
        public string name1 { get; set; }
        [DataMember]
        [JsonProperty("name2")]
        public string name2 { get; set; }
        [DataMember]
        [JsonProperty("partnerKindDescription")]
        public string partnerKindDescription { get; set; }
        [DataMember]
        public string regionDescription { get; set; }
        [DataMember]
        public string taxpayerFullName { get; set; }
        [DataMember]
        public string taxpayerTitle { get; set; }

    }
    public class TaxpayerInfo
    {
        public string idNumber { get; set; }
        public string idType { get; set; }
        public string taxpayerBirthDate { get; set; }
    }
}
