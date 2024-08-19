
using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{

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
    
    public class SignupCityDropdownSet
    {
        public List<SignupCityResult> results { get; set; }
    }
    
    public class SignupCityD
    {
        public string cityCode { get; set; }
        public string region { get; set; }
        public string country { get; set; }
        public List<SignupCityResult> cities { get; set; }
    }
    public class City
    {
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

    
    public class SignupCityRootObject
    {
        [JsonProperty("data")]
        public SignupCityD d { get; set; }
    }
}

