using System;
using System.Collections.Generic;
using Xamarin.Forms.Internals;

namespace EGAZT
{
    [Preserve(AllMembers = true)]
    public class VATSignUpData
    {
        public VATSignUpDataD d { get; set; }
    }

    [Preserve(AllMembers = true)]
    public class __VATSignUpDatametadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }

    }

    [Preserve(AllMembers = true)]
    public class VATSignUpDataResults
    {
        public __metadata __metadata { get; set; }
        public string Spras { get; set; }
        public string Land1 { get; set; }
        public string Landx { get; set; }
        public string Natio { get; set; }
        public string Landx50 { get; set; }
        public string Natio50 { get; set; }
        public string PrqSpregt { get; set; }

    }
    public class Country_dropdownSet
    {
        public IList<VATSignUpDataResults> results { get; set; }

    }
    //public class __metadata
    //{
    //    public string id { get; set; }
    //    public string uri { get; set; }
    //    public string type { get; set; }

    //}

    [Preserve(AllMembers = true)]
    public class VATSignUpStateResults
    {
        public __metadata __metadata { get; set; }
        public string Spras { get; set; }
        public string Land1 { get; set; }
        public string Bland { get; set; }
        public string Bezei { get; set; }

    }

    [Preserve(AllMembers = true)]
    public class State_dropdownSet
    {
        public IList<VATSignUpStateResults> results { get; set; }

    }

    [Preserve(AllMembers = true)]
    public class VATSignUPCityResults
    {
        public __metadata __metadata { get; set; }
        public string Langu { get; set; }
        public string Country { get; set; }
        public string CityCode { get; set; }
        public string Region { get; set; }
        public string CityName { get; set; }

    }

    [Preserve(AllMembers = true)]
    public class City_dropdownSet
    {
        public IList<VATSignUPCityResults> results { get; set; }

    }

    [Preserve(AllMembers = true)]
    public class VATSignUpDataD
    {
        public __metadata __metadata { get; set; }
        public string Spras { get; set; }
        public string Land1 { get; set; }
        public string Bland { get; set; }
        public string Cityc { get; set; }
        public Country_dropdownSet country_dropdownSet { get; set; }
        public State_dropdownSet State_dropdownSet { get; set; }
        public City_dropdownSet city_dropdownSet { get; set; }

    }
   

}
