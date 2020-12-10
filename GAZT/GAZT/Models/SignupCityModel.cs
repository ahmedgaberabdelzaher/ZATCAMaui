using EGAZT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace GAZT.Models
{
    [Preserve(AllMembers = true)]
    public class SignupCityModel
    {
    }
    public class SignupCityMetadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    public class SignupCityDeferred
    {
        public string uri { get; set; }
    }
    public class SignupCityCountryDropdownSet
    {
        public SignupCityDeferred __deferred { get; set; }
    }
    public class SignupCityDeferred2
    {
        public string uri { get; set; }
    }
    public class SignupCityStateDropdownSet
    {
        public SignupCityDeferred2 __deferred { get; set; }
    }
    public class SignupCityMetadata2
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    public class SignupCityResult
    {
        public Metadata2 __metadata { get; set; }
        public string Langu { get; set; }
        public string Country { get; set; }
        public string CityCode { get; set; }
        public string Region { get; set; }
        public string CityName { get; set; }
    }
    public class SignupCityDropdownSet
    {
        public List<SignupCityResult> results { get; set; }
    }
    public class SignupCityD
    {
        public Metadata __metadata { get; set; }
        public string Spras { get; set; }
        public string Land1 { get; set; }
        public string Bland { get; set; }
        public string Cityc { get; set; }
        public SignupCityCountryDropdownSet country_dropdownSet { get; set; }
        public SignupCityStateDropdownSet State_dropdownSet { get; set; }
        public SignupCityDropdownSet city_dropdownSet { get; set; }
    }
    public class SignupCityRootObject
    {
        public SignupCityD d { get; set; }
    }
}

