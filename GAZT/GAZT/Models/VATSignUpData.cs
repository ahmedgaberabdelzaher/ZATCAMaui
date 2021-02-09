using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Xamarin.Forms.Internals;

namespace EGAZT
{
    [Preserve(AllMembers = true)]
    public class VATSignUpData
    {
        [DataMember]
        public VATSignUpDataD d { get; set; }
    }

    [Preserve(AllMembers = true)]
    public class __VATSignUpDatametadata
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string uri { get; set; }
        [DataMember]
        public string type { get; set; }

    }

    [Preserve(AllMembers = true)]
    public class VATSignUpDataResults
    {
        [DataMember]
        public __metadata __metadata { get; set; }
        [DataMember]
        public string Spras { get; set; }
        [DataMember]
        public string Land1 { get; set; }
        [DataMember]
        public string Landx { get; set; }
        [DataMember]
        public string Natio { get; set; }
        [DataMember]
        public string Landx50 { get; set; }
        [DataMember]
        public string Natio50 { get; set; }
        [DataMember]
        public string PrqSpregt { get; set; }

    }
    public class Country_dropdownSet
    {
        [DataMember]
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
        [DataMember]
        public __metadata __metadata { get; set; }
        [DataMember]
        public string Spras { get; set; }
        [DataMember]
        public string Land1 { get; set; }
        [DataMember]
        public string Bland { get; set; }
        [DataMember]
        public string Bezei { get; set; }

    }

    [Preserve(AllMembers = true)]
    public class State_dropdownSet
    {
        [DataMember]
        public IList<VATSignUpStateResults> results { get; set; }

    }

    [Preserve(AllMembers = true)]
    public class VATSignUPCityResults
    {
        [DataMember]
        public __metadata __metadata { get; set; }
        [DataMember]
        public string Langu { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public string CityCode { get; set; }
        [DataMember]
        public string Region { get; set; }
        [DataMember]
        public string CityName { get; set; }

    }

    [Preserve(AllMembers = true)]
    public class City_dropdownSet
    {
        [DataMember]
        public IList<VATSignUPCityResults> results { get; set; }

    }

    [Preserve(AllMembers = true)]
    public class VATSignUpDataD
    {
        [DataMember]
        public __metadata __metadata { get; set; }
        [DataMember]
        public string Spras { get; set; }
        [DataMember]
        public string Land1 { get; set; }
        [DataMember]
        public string Bland { get; set; }
        [DataMember]
        public string Cityc { get; set; }
        [DataMember]
        public Country_dropdownSet country_dropdownSet { get; set; }
        [DataMember]
        public State_dropdownSet State_dropdownSet { get; set; }
        [DataMember]
        public City_dropdownSet city_dropdownSet { get; set; }

    }
   

}
