namespace ZATCAMAUI.Models
{

    class IDTypeModel
    {
    }
    
    public class IDTypeModelMetadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    
    public class IDTypeModelD
    {
        public IDTypeModelMetadata __metadata { get; set; }
        public DateTime? Birthdt { get; set; }
        public string Bpkind { get; set; }
        public string Country { get; set; }
        public string IdIssueingCountry { get; set; }
        public string Source { get; set; }
        public string TaxpDob { get; set; }
        public string PassExpDt { get; set; }
        public string Title { get; set; }
        public string FullName { get; set; }
        public string Floor { get; set; }
        public string Tin { get; set; }
        public string AdditionalNo { get; set; }
        public string HouseNo { get; set; }
        public string Idtype { get; set; }
        public string BirthdtC { get; set; }
        public string BuildingNo { get; set; }
        public string Birthdt10 { get; set; }
        public string Idnum { get; set; }
        public string FatherName { get; set; }
        public string PoBox { get; set; }
        public string GrandfatherName { get; set; }
        public string Street1 { get; set; }
        public string FamilyName { get; set; }
        public string Street2 { get; set; }
        public string Initials { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Quarter { get; set; }
        public string PostalCode { get; set; }
        public string Telephone { get; set; }
        public string FaxNumber { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string DefltComm { get; set; }
        public string Adrnr { get; set; }
        public string Website { get; set; }
        public string Augrp { get; set; }
        public string BranchDesc { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string BpkindDesc { get; set; }
        public string RegionDesc { get; set; }
        public string IqamaType { get; set; }
        public string IqamaDesc { get; set; }

    }

    public class IDTypeModelRootObject
    {
        public IDTypeModelD d { get; set; }
    }
}
