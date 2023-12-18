using System.Runtime.Serialization;

namespace ZATCAMAUI.Models
{

    public class VATSignUp
    {
        [DataMember]
        public VATSignUpD d { get; set; }
    }
  
    public class _metadata
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string uri { get; set; }
        [DataMember]
        public string type { get; set; }

    }
  
    public class VATSignUpD
    {
        [DataMember]
        public _metadata __metadata { get; set; }
        [DataMember]
        public string Birthdt { get; set; }

        [DataMember]
        public string Bpkind { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public string IdIssueingCountry { get; set; }
        [DataMember]
        public string Source { get; set; }
        [DataMember]
        public string TaxpDob { get; set; }
        [DataMember]
        public string PassExpDt { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string FullName { get; set; }
        [DataMember]
        public string Floor { get; set; }
        [DataMember]
        public string Tin { get; set; }
        [DataMember]
        public string AdditionalNo { get; set; }
        [DataMember]
        public string HouseNo { get; set; }
        [DataMember]
        public string Idtype { get; set; }
        [DataMember]
        public string BirthdtC { get; set; }
        [DataMember]
        public string BuildingNo { get; set; }
        [DataMember]
        public string Birthdt10 { get; set; }
        [DataMember]
        public string Idnum { get; set; }
        [DataMember]
        public string FatherName { get; set; }
        [DataMember]
        public string PoBox { get; set; }
        [DataMember]
        public string GrandfatherName { get; set; }
        [DataMember]
        public string Street1 { get; set; }
        [DataMember]
        public string FamilyName { get; set; }
        [DataMember]
        public string Street2 { get; set; }
        [DataMember]
        public string Initials { get; set; }
        [DataMember]
        public string Province { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string Quarter { get; set; }
        [DataMember]
        public string PostalCode { get; set; }
        [DataMember]
        public string Telephone { get; set; }
        [DataMember]
        public string FaxNumber { get; set; }
        [DataMember]
        public string Mobile { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string DefltComm { get; set; }
        [DataMember]
        public string Adrnr { get; set; }
        [DataMember]
        public string Website { get; set; }
        [DataMember]
        public string Augrp { get; set; }
        [DataMember]
        public string BranchDesc { get; set; }
        [DataMember]
        public string Name1 { get; set; }
        [DataMember]
        public string Name2 { get; set; }
        [DataMember]
        public string BpkindDesc { get; set; }
        [DataMember]
        public string RegionDesc { get; set; }

    }

}
