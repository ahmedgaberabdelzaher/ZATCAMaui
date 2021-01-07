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
    }
    [Preserve(AllMembers = true)]
    public class VATLookUpD
    {
        public List<VATLookUpResult> results { get; set; }
    }
}
