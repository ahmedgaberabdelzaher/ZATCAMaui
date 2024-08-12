using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{

    public class VATSignUpData
    {
        [DataMember]
        [JsonProperty("data")]
        public VATSignUpDataD d { get; set; }
    }

    
    public class __VATSignUpDatametadata
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string uri { get; set; }
        [DataMember]
        public string type { get; set; }

    }

    
    public class VATSignUpDataResults
    {
        [DataMember]
        [JsonProperty("")]
        public __metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("language")]
        public string Spras { get; set; }
        [DataMember]
        [JsonProperty("country")]
        public string Land1 { get; set; }
        [DataMember]
        [JsonProperty("countryName")]
        public string Landx { get; set; }
        [DataMember]
        [JsonProperty("nationality")]
        public string Natio { get; set; }
        [DataMember]
        [JsonProperty("countryDescription")]
        public string Landx50 { get; set; }
        [DataMember]
        [JsonProperty("nationalityDescription")]
        public string Natio50 { get; set; }
        [DataMember]
        [JsonProperty("superRegion")]
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

    
    public class VATSignUpStateResults
    {
        [DataMember]
        public __metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("language")]
        public string Spras { get; set; }
        [DataMember]
        [JsonProperty("country")]
        public string Land1 { get; set; }
       
        [DataMember]
        [JsonProperty("region")]
        public string Bland { get; set; }
        [DataMember]
        [JsonProperty("description")]
        public string Bezei { get; set; }

    }

    
    public class State_dropdownSet
    {
        [DataMember]
        public IList<VATSignUpStateResults> results { get; set; }

    }

    
    public class VATSignUPCityResults
    {
        [DataMember]
        public __metadata __metadata { get; set; }
        [JsonProperty("language")]
        public string Langu { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("cityCode")]
        public string CityCode { get; set; }
        [JsonProperty("region")]
        public string Region { get; set; }
        [JsonProperty("cityName")]
        public string CityName { get; set; }
    }

    
    public class City_dropdownSet
    {
        [DataMember]
        public IList<VATSignUPCityResults> results { get; set; }

    }

    
    public class VATSignUpDataD
    {
        [DataMember]
        public __metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("language")]
        public string Spras { get; set; }
        [DataMember]
        [JsonProperty("country")]
        public string Land1 { get; set; }
        [DataMember]
        [JsonProperty("region")]
        public string Bland { get; set; }
        [DataMember]
        [JsonProperty("cityCode")]
        public string Cityc { get; set; }
        [DataMember]
        [JsonProperty("countries")]
        public IList<VATSignUpDataResults> country_dropdownSet { get; set; }
        [DataMember]
        [JsonProperty("states")]
        public IList<VATSignUpStateResults> State_dropdownSet { get; set; }
        [DataMember]
        [JsonProperty("cities")]
        public IList<VATSignUPCityResults> city_dropdownSet { get; set; }

    }


}
