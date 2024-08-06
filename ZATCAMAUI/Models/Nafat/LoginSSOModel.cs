namespace ZATCAMAUI.Models.Nafat
{
    public class LoginSSOModel
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
            public string Actcat { get; set; }
            public string Murl { get; set; }
            public string Captcha { get; set; }
            public string Mguid { get; set; }
            public string MobileCountry { get; set; }
            public string Actno { get; set; }
            public string AddrType { get; set; }
            public string AExternal { get; set; }
            public string AInternal { get; set; }
            public string Augrp { get; set; }
            public object Begda { get; set; }
            public DateTime? Birthdt { get; set; }

            public string Building { get; set; }
            public string CaseGuid { get; set; }
            public string City1 { get; set; }
            public string City2 { get; set; }
            public string Country { get; set; }
            public string Crlicenceno { get; set; }
            public string Email { get; set; }
            public string EmailCode { get; set; }
            public object Endda { get; set; }
            public object EndDate { get; set; }
            public string Fbnum { get; set; }
            public string Firstname { get; set; }
            public string Floor { get; set; }
            public string FormGuid { get; set; }
            public string HouseNum1 { get; set; }
            public string HouseNum2 { get; set; }
            public string Idnumber { get; set; }
            public string IdType { get; set; }
            public string Lastname { get; set; }
            public string Mobile { get; set; }
            public string Password { get; set; }
            public string PostCode1 { get; set; }
            public string Region { get; set; }
            public bool Sameasphy { get; set; }
            public string SmsCode { get; set; }
            public string Source { get; set; }
            public object StartDate { get; set; }
            public string Street { get; set; }
            public string StrSuppl1 { get; set; }
            public string StrSuppl2 { get; set; }
            public string Submit { get; set; }
            public string Tin { get; set; }
            public string Type { get; set; }
            public string AIqamaType { get; set; }
            public string AIqamaDesc { get; set; }
            public string AIqamaFg { get; set; }

        }

        public class LoginSSOModelClass
        {
            public List<Result> results { get; set; }
        }

    }
}

