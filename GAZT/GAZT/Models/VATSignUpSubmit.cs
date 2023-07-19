using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Xamarin.Forms.Internals;

namespace EGAZT.Models
{
    [Preserve(AllMembers = true)]
    public class VATSignUpSubmit
    {
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string IdType { get; set; }
        [DataMember]
        public string Idnumber { get; set; }
        [DataMember]
        public string Firstname { get; set; }
        [DataMember]
        public string Lastname { get; set; }
        [DataMember]
        public string PostCode1 { get; set; }
        [DataMember]
        public string City1 { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public string MobileCountry { get; set; }
        [DataMember]
        public string Region { get; set; }
        [DataMember]
        public string Building { get; set; }
        [DataMember]
        public string Floor { get; set; }
        [DataMember]
        public string Street { get; set; }
        [DataMember]
        public string Begda { get; set; }
        [DataMember]
        public string Endda { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Mobile { get; set; }
        [DataMember]
        public string CaseGuid { get; set; }
        [DataMember]
        public string Birthdt { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string SmsCode { get; set; }
        [DataMember]
        public string EmailCode { get; set; }
        [DataMember]
        public string Submit { get; set; }
         [DataMember]
        public string Captcha { get; set; }
     [DataMember]
        public string Mguid { get; set; }
    }



    [Preserve(AllMembers = true)]
    public class Metadata1
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string uri { get; set; }
        [DataMember]
        public string type { get; set; }
    }

    [Preserve(AllMembers = true)]
    public class D1
    {
        [DataMember]
        public Metadata1 __metadata { get; set; }
        [DataMember]
        public string Actcat { get; set; }
        [DataMember]
        public string Actno { get; set; }
        [DataMember]
        public string AddrType { get; set; }
        [DataMember]
        public string AExternal { get; set; }
        [DataMember]
        public string AInternal { get; set; }
        [DataMember]
        public string Augrp { get; set; }
        [DataMember]
        public DateTime Begda { get; set; }
        [DataMember]
        public DateTime Birthdt { get; set; }
        [DataMember]
        public string Building { get; set; }
        [DataMember]
        public string CaseGuid { get; set; }
        [DataMember]
        public string City1 { get; set; }
        [DataMember]
        public string City2 { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public string Crlicenceno { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string EmailCode { get; set; }
        [DataMember]
        public DateTime Endda { get; set; }
        [DataMember]
        public object EndDate { get; set; }
        [DataMember]
        public string Fbnum { get; set; }
        [DataMember]
        public string Firstname { get; set; }
        [DataMember]
        public string Floor { get; set; }
        [DataMember]
        public string FormGuid { get; set; }
        [DataMember]
        public string HouseNum1 { get; set; }
        [DataMember]
        public string HouseNum2 { get; set; }
        [DataMember]
        public string Idnumber { get; set; }
        [DataMember]
        public string IdType { get; set; }
        [DataMember]
        public string Lastname { get; set; }
        [DataMember]
        public string Mobile { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string PostCode1 { get; set; }
        [DataMember]
        public string Region { get; set; }
        [DataMember]
        public bool Sameasphy { get; set; }
        [DataMember]
        public string SmsCode { get; set; }
        [DataMember]
        public string Source { get; set; }
        [DataMember]
        public object StartDate { get; set; }
        [DataMember]
        public string Street { get; set; }
        [DataMember]
        public string StrSuppl1 { get; set; }
        [DataMember]
        public string StrSuppl2 { get; set; }
        [DataMember]
        public string Submit { get; set; }
        [DataMember]
        public string Tin { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string Captcha { get; set; }
    }

    [Preserve(AllMembers = true)]
    public class VATSignUpSubmitResponse
    {
        [DataMember]
        public D1 d { get; set; }
    }
}
