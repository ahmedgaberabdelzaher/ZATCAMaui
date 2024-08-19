using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{

    public class VATSignUpSubmit
    {
        [DataMember]
        [JsonProperty("mainGuid")]
        public string Mguid { get; set; }
        [DataMember]
        [JsonProperty("type")]
        public string Type { get; set; }
        [DataMember]
        [JsonProperty("idType")]
        public string IdType { get; set; }
        [DataMember]
        [JsonProperty("idNumber")]
        public string Idnumber { get; set; }
        [DataMember]
        [JsonProperty("firstName")]
        public string Firstname { get; set; }
        [DataMember]
        [JsonProperty("lastName")]
        public string Lastname { get; set; }
        [DataMember]
        [JsonProperty("postCode")]
        public string PostCode1 { get; set; }
        [DataMember]
        [JsonProperty("city")]
        public string City1 { get; set; }
        [DataMember]
        [JsonProperty("country")]
        public string Country { get; set; }
        [DataMember]
        [JsonProperty("mobileCountry")]
        public string MobileCountry { get; set; }
        [DataMember]
        [JsonProperty("region")]
        public string Region { get; set; }
        [DataMember]
        [JsonProperty("building")]
        public string Building { get; set; }
        [DataMember]
        [JsonProperty("floor")]
        public string Floor { get; set; }
        [DataMember]
        [JsonProperty("street")]
        public string Street { get; set; }
        [DataMember]
        [JsonProperty("beginningDate")]
        public string Begda { get; set; }
        [DataMember]
        [JsonProperty("endDate")]
        public string Endda { get; set; }
        [DataMember]
        [JsonProperty("email")]
        public string Email { get; set; }
        [DataMember]
        [JsonProperty("mobile")]
        public string Mobile { get; set; }
        [DataMember]
        [JsonProperty("caseGUID")]
        public string CaseGuid { get; set; }
        [DataMember]
        [JsonProperty("birthDate")]
        public string Birthdt { get; set; }
        [DataMember]
        [JsonProperty("password")]
        public string Password { get; set; }
        [DataMember]
        [JsonProperty("SMSCode")]
        public string SmsCode { get; set; }
        [DataMember]
        [JsonProperty("emailCode")]
        public string EmailCode { get; set; }
        [DataMember]
        [JsonProperty("submit")]
        public string Submit { get; set; }
        [DataMember]
        [JsonProperty("captchaCode")]
        public string Captcha { get; set; }
        [DataMember]
        public string AIqamaDesc { get; set; }
        [DataMember]
        public string AIqamaFg { get; set; }
    }



    
    public class Metadata1
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string uri { get; set; }
        [DataMember]
        public string type { get; set; }
    }

    
    public class D1
    {
        [DataMember]
        public Metadata1 __metadata { get; set; }
        [DataMember]
        [JsonProperty("activityCategory")]
        public string Actcat { get; set; }
        [DataMember]
        [JsonProperty("activityNumber")]
        public string Actno { get; set; }
        [DataMember]
        [JsonProperty("addressType")]
        public string AddrType { get; set; }
        [DataMember]
        [JsonProperty("external")]
        public string AExternal { get; set; }
        [DataMember]
        [JsonProperty("internal")]
        public string AInternal { get; set; }
        [DataMember]
        [JsonProperty("authorizationGroup")]
        public string Augrp { get; set; }
       
        [DataMember]
        [JsonProperty("district")]
        public string City2 { get; set; }
        [DataMember]
        [JsonProperty("CRLicenseNumber")]
        public string Crlicenceno { get; set; }
        [DataMember]
        [JsonProperty("finishDate")]
        public object EndDate { get; set; }
        [DataMember]
        [JsonProperty("formBundleNumber")]
        public string Fbnum { get; set; }
        
        [DataMember]
        [JsonProperty("formGUID")]
        public string FormGuid { get; set; }
        [DataMember]
        [JsonProperty("houseNumber1")]
        public string HouseNum1 { get; set; }
        [DataMember]
        [JsonProperty("houseNumber2")]
        public string HouseNum2 { get; set; }
       
        [DataMember]
        [JsonProperty("isSameAsPhyiscal")]
        public bool Sameasphy { get; set; }
       
        [DataMember]
        [JsonProperty("source")]
        public string Source { get; set; }
        [DataMember]
        [JsonProperty("startDate")]
        public object StartDate { get; set; }
       
        [DataMember]
        [JsonProperty("street1")]
        public string StrSuppl1 { get; set; }
        [DataMember]
        [JsonProperty("street2")]
        public string StrSuppl2 { get; set; }
       
        [DataMember]
        [JsonProperty("TIN")]
        public string Tin { get; set; }

        [DataMember]
        [JsonProperty("type")]
        public string Type { get; set; }
        [DataMember]
        [JsonProperty("idType")]
        public string IdType { get; set; }
        [DataMember]
        [JsonProperty("idNumber")]
        public string Idnumber { get; set; }


        [DataMember]
        [JsonProperty("firstName")]
        public string Firstname { get; set; }
        [DataMember]
        [JsonProperty("lastName")]
        public string Lastname { get; set; }
        [DataMember]
        [JsonProperty("postCode")]
        public string PostCode1 { get; set; }
        [DataMember]
        [JsonProperty("city")]
        public string City1 { get; set; }
        [DataMember]
        [JsonProperty("country")]
        public string Country { get; set; }
        [DataMember]
        [JsonProperty("mobileCountry")]
        public string MobileCountry { get; set; }


        [DataMember]
        [JsonProperty("region")]
        public string Region { get; set; }
        [DataMember]
        [JsonProperty("building")]
        public string Building { get; set; }
        [DataMember]
        [JsonProperty("floor")]
        public string Floor { get; set; }
        [DataMember]
        [JsonProperty("street")]
        public string Street { get; set; }

        [DataMember]
        [JsonProperty("beginningDate")]
        public string Begda { get; set; }
        [DataMember]
        [JsonProperty("endDate")]
        public string Endda { get; set; }
        [DataMember]
        [JsonProperty("email")]
        public string Email { get; set; }
        [DataMember]
        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [DataMember]
        [JsonProperty("caseGuid")]
        public string CaseGuid { get; set; }
        [DataMember]
        [JsonProperty("birthDate")]
        public string Birthdt { get; set; }
        [DataMember]
        [JsonProperty("password")]
        public string Password { get; set; }

        [DataMember]
        [JsonProperty("smsCode")]
        public string SmsCode { get; set; }
        [DataMember]
        [JsonProperty("emailCode")]
        public string EmailCode { get; set; }
        [DataMember]
        [JsonProperty("submit")]
        public string Submit { get; set; }
        [DataMember]
        [JsonProperty("captchaCode")]
        public string Captcha { get; set; }
    }


    public class VATSignUpSubmitResponse
    {
        [DataMember]
        [JsonProperty("result")]
        public D1 d { get; set; }
    }
}
