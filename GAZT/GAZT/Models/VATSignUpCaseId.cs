using System;
using System.Collections.Generic;
using Xamarin.Forms.Internals;

namespace EGAZT
{
    [Preserve(AllMembers = true)]
    public class VATSignUpCaseId
    {
        public VATSignUpCaseIdD d { get; set; }
    }

    [Preserve(AllMembers = true)]
    public class VATSignUpCaseIdResults
    {
        public __metadata __metadata { get; set; }
        public string Actcat { get; set; }
        public string Actno { get; set; }
        public string AddrType { get; set; }
        public string AExternal { get; set; }
        public string AInternal { get; set; }
        public string Augrp { get; set; }
       // public IList<undefined> Begda { get; set; }
        //public IList<undefined> Birthdt { get; set; }
        public string Building { get; set; }
        public string CaseGuid { get; set; }
        public string City1 { get; set; }
        public string City2 { get; set; }
        public string Country { get; set; }
        public string Crlicenceno { get; set; }
        public string Email { get; set; }
        public string EmailCode { get; set; }
       // public IList<undefined> Endda { get; set; }
      //  public IList<undefined> EndDate { get; set; }
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
       // public IList<undefined> StartDate { get; set; }
        public string Street { get; set; }
        public string StrSuppl1 { get; set; }
        public string StrSuppl2 { get; set; }
        public string Submit { get; set; }
        public string Tin { get; set; }
        public string Type { get; set; }

    }
    [Preserve(AllMembers = true)]
    public class VATSignUpCaseIdD
    {
        public IList<VATSignUpCaseIdResults> results { get; set; }

    }
   
}
