using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;

namespace ZATCAMAUI.Models
{
	public class LoginSSOModelERAD
	{
        public class Metadata
        {
            public string id { get; set; }
            public string uri { get; set; }
            public string type { get; set; }
        }

        public class Result
        {
            public Metadata __metadata { get; set; }
            [JsonProperty("activityCategory")]
            public string Actcat { get; set; }
            [JsonProperty("URL")]
            public string Murl { get; set; }
            [JsonProperty("captchaCode")]
            public string Captcha { get; set; }
            [JsonProperty("GIID")]
            public string Mguid { get; set; }
            [JsonProperty("mobileCountry")]
            public string MobileCountry { get; set; }
            [JsonProperty("activityNumber")]
            public string Actno { get; set; }
            [JsonProperty("addressType")]
            public string AddrType { get; set; }
            [JsonProperty("external")]
            public string AExternal { get; set; }
            [JsonProperty("internal")]
            public string AInternal { get; set; }
            [JsonProperty("authorizationGroup")]
            public string Augrp { get; set; }
            [JsonProperty("beginDate")]
            public string Begda { get; set; }
            [JsonProperty("birthDate")]
            public string Birthdt { get; set; }

            //private DateTime? _birthDt = new DateTime();
            //public DateTime? Birthdt {
            //    get { return _birthDt; }
            //    set
            //    {
            //        if (_birthDt == value) return;
            //        _birthDt = value;

            //        if(value == null)
            //        {
            //            _birthDt = new DateTime();
            //        }
            //    }
            //}


            [JsonProperty("buildingCode")]
            public string Building { get; set; }
            [JsonProperty("caseGIID")]
            public string CaseGuid { get; set; }
            [JsonProperty("city")]
            public string City1 { get; set; }
            [JsonProperty("district")]
            public string City2 { get; set; }
            [JsonProperty("country")]
            public string Country { get; set; }
            [JsonProperty("CRLicenceNumber")]
            public string Crlicenceno { get; set; }
            [JsonProperty("email")]
            public string Email { get; set; }
            [JsonProperty("emailCode")]
            public string EmailCode { get; set; }
            [JsonProperty("endDate")]
            public string Endda { get; set; }
            [JsonProperty("endDate2")]
            public object EndDate { get; set; }
            [JsonProperty("formBundleNumber")]
            public string Fbnum { get; set; }
            [JsonProperty("firstName")]
            public string Firstname { get; set; }
            [JsonProperty("floor")]
            public string Floor { get; set; }
            [JsonProperty("formGIID")]
            public string FormGuid { get; set; }
            [JsonProperty("houseNumber1")]
            public string HouseNum1 { get; set; }
            [JsonProperty("houseNumber2")]
            public string HouseNum2 { get; set; }
            [JsonProperty("idNumber")]
            public string Idnumber { get; set; }
            [JsonProperty("idType")]
            public string IdType { get; set; }
            [JsonProperty("lastName")]
            public string Lastname { get; set; }
            [JsonProperty("mobile")]
            public string Mobile { get; set; }
            [JsonProperty("password")]
            public string Password { get; set; }
            [JsonProperty("postCode")]
            public string PostCode1 { get; set; }
            [JsonProperty("region")]
            public string Region { get; set; }
            [JsonProperty("isSameAsPhyiscal")]
            public bool Sameasphy { get; set; }
            [JsonProperty("SMSCode")]
            public string SmsCode { get; set; }
            [JsonProperty("source")]
            public string Source { get; set; }
            [JsonProperty("startDate")]
            public string StartDate { get; set; }
            [JsonProperty("street")]
            public string Street { get; set; }
            [JsonProperty("street1")]
            public string StrSuppl1 { get; set; }
            [JsonProperty("street2")]
            public string StrSuppl2 { get; set; }
            [JsonProperty("submit")]
            public string Submit { get; set; }
            [JsonProperty("TIN")]
            public string Tin { get; set; }
            [JsonProperty("type")]
            public string Type { get; set; }
            public string TpTitle { get; set; }
            public string AIqamaType { get; set; }
            public string AIqamaDesc { get; set; }
            public string AIqamaFg { get; set; }
        }

        public class LoginSSOModelClassERAD
        {
            [JsonProperty("data")]
            public List<Result> results { get; set; }
        }
    
	}
}

