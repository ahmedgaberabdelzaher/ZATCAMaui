using Newtonsoft.Json;

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
        [JsonProperty("birthDate")]
        public string Birthdt { get; set; }
        [JsonProperty("partnerKind")]
        public string Bpkind { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("idIssueingCountry")]
        public string IdIssueingCountry { get; set; }
        [JsonProperty("source")]
        public string Source { get; set; }
        [JsonProperty("taxpayerBirthDate")]
        public string TaxpDob { get; set; }
        [JsonProperty("passExpiryDate")]
        public string PassExpDt { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("fullName")]
        public string FullName { get; set; }
        [JsonProperty("floor")]
        public string Floor { get; set; }
        [JsonProperty("TIN")]
        public string Tin { get; set; }
        [JsonProperty("additionalNumber")]
        public string AdditionalNo { get; set; }
        [JsonProperty("houseNumber")]
        public string HouseNo { get; set; }
        [JsonProperty("idType")]
        public string Idtype { get; set; }
        //[JsonProperty("result")]
        //public string BirthdtC { get; set; }
        [JsonProperty("buildingNumber")]
        public string BuildingNo { get; set; }
        //[JsonProperty("result")]
        //public string Birthdt10 { get; set; }
        [JsonProperty("idNumber")]
        public string Idnum { get; set; }
        [JsonProperty("fatherName")]
        public string FatherName { get; set; }
        [JsonProperty("poBox")]
        public string PoBox { get; set; }
        [JsonProperty("grandfatherName")]
        public string GrandfatherName { get; set; }
        [JsonProperty("street1")]
        public string Street1 { get; set; }
        [JsonProperty("familyName")]
        public string FamilyName { get; set; }
        [JsonProperty("street2")]
        public string Street2 { get; set; }
        [JsonProperty("initials")]
        public string Initials { get; set; }
        [JsonProperty("province")]
        public string Province { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("quarter")]
        public string Quarter { get; set; }
        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }
        [JsonProperty("telephone")]
        public string Telephone { get; set; }
        [JsonProperty("faxNumber")]
        public string FaxNumber { get; set; }
        [JsonProperty("mobile")]
        public string Mobile { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("deafultCommunication")]
        public string DefltComm { get; set; }
        [JsonProperty("addressNumber")]
        public string Adrnr { get; set; }
        [JsonProperty("website")]
        public string Website { get; set; }
        [JsonProperty("authorizationGroup")]
        public string Augrp { get; set; }
        [JsonProperty("branchDescription")]
        public string BranchDesc { get; set; }
        [JsonProperty("name1")]
        public string Name1 { get; set; }
        [JsonProperty("name2")]
        public string Name2 { get; set; }
        [JsonProperty("partnerKindDescription")]
        public string BpkindDesc { get; set; }
        [JsonProperty("regionDescription")]
        public string RegionDesc { get; set; }
        [JsonProperty("iqamaType")]
        public string IqamaType { get; set; }
        [JsonProperty("iqamaDescription")]
        public string IqamaDesc { get; set; }

        public string taxpayerTitle { get; set; }
        public string taxpayerFullName { get; set; }
        public string OTPCode { get; set; }
        public string formBundleGUID { get; set; }
    }

    public class IDTypeModelRootObject
    {
        [JsonProperty("result")]
        public IDTypeModelD d { get; set; }
    }
}
