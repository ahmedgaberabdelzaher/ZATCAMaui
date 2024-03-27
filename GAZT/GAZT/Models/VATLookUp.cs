using EGAZT.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms.Internals;

namespace GAZT.Models
{
    [Preserve(AllMembers = true)]
    public class VATLookUp
    {
        public VATLookUpD d { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class VATLookUpResult
    {
        public Metadata __metadata { get; set; }
        public int Idtype { get; set; }
        public string Idnumber { get; set; }
        public string Tin { get; set; }
        public string VatCertNo { get; set; }
        public string Name { get; set; }
        public string BldgCode { get; set; }
        public string Street { get; set; }
        public string HouseNo { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string EinvEnfStatus { get; set; }
        public string EinvEnfDt { get; set; }
    }

    public class Lookup
    {
        public string idNumber { get; set; }
        public string TIN { get; set; }
        public string name { get; set; }
        public string buildingCode { get; set; }
        public string street { get; set; }
        public string houseNumber { get; set; }
        public string postCode { get; set; }
        public string city { get; set; }
        public string region { get; set; }
        public string country { get; set; }
        public string VATCertificateNumber { get; set; }
        public string errorCode { get; set; }
        public string errorDescription { get; set; }
        public int idType { get; set; }
        public string einvEnfStatus { get; set; }
        public string einvEnfDt { get; set; }
    }

    public class VATLokupsDP
    {
        public List<Lookup> lookups { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class VATLookUpD
    {
        public List<VATLookUpResult> results { get; set; }
    }
}
