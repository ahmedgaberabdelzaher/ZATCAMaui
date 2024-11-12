using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{

    class CRValidationModel
    {
    }
    
    public class CRValidationModelMetadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    
    public class CRValidationModelD
    {
        // public CRValidationModelMetadata __metadata { get; set; }
        [JsonProperty("CRNumber")]
        public string Crnum { get; set; }
        [JsonProperty("city")]
        public string CityAry { get; set; }
        [JsonProperty("validDateFrom")]
        public object Validfrm { get; set; }
        [JsonProperty("countryDescription")]
        public string CountryAry { get; set; }
        [JsonProperty("validDateTo")]
        public object Validto { get; set; }
        [JsonProperty("notFound")]
        public string NotFound { get; set; }
        [JsonProperty("issueDate")]
        public string Issuedt { get; set; }
        [JsonProperty("expiryDate")]
        public object Expdt { get; set; }
        [JsonProperty("CRName")]
        public string Crname { get; set; }
        [JsonProperty("exception")]
        public string Excption { get; set; }
        [JsonProperty("telephoneNumber")]
        public string TelephoneNumbery { get; set; }
        [JsonProperty("faxNumber")]
        public string FaxNumbery { get; set; }
        [JsonProperty("email")]
        public string Emaily { get; set; }
        [JsonProperty("telex")]
        public string Telexy { get; set; }
        [JsonProperty("internetAddress")]
        public string InternetAddressy { get; set; }
        [JsonProperty("postalAddress")]
        public string AddressPostaly { get; set; }
        [JsonProperty("addressType")]
        public string Addresstypey { get; set; }
        [JsonProperty("physicalAddress")]
        public string AddressPhysicaly { get; set; }
        [JsonProperty("700Number")]
        public string Z700Crnum { get; set; }
    }
    
    public class CRValidationModelRootObject
    {
        [JsonProperty("data")]
        public CRValidationModelD d { get; set; }
    }
}
