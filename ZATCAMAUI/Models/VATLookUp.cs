
using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{

    public class VATLookUp
    {
        [JsonProperty("result")]
        public VATLookUpD d { get; set; }
    }
    
    public class VATLookUpResult
    {
        public Metadata __metadata { get; set; }
        [JsonProperty("idType")]
        public int Idtype { get; set; }
        [JsonProperty("idNumber")]
        public string Idnumber { get; set; }
        [JsonProperty("TIN")]
        public string Tin { get; set; }
        [JsonProperty("VATCertificateNumber")]
        public string VatCertNo { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("buildingCode")]
        public string BldgCode { get; set; }
        [JsonProperty("street")]
        public string Street { get; set; }
        [JsonProperty("houseNumber")]
        public string HouseNo { get; set; }
        [JsonProperty("postCode")]
        public string PostCode { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("region")]
        public string Region { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("errorCode")]
        public string Code { get; set; }
        [JsonProperty("errorDescription")]
        public string Description { get; set; }
        public string EinvEnfStatus { get; set; }
        public string EinvEnfDt { get; set; }
    }

    
    public class VATLookUpD
    {
        [JsonProperty("lookups")]
        public List<VATLookUpResult> results { get; set; }
    }
    public class VATLookUpModel
    {
        public string idType { get; set; }
        public string idNumber { get; set; }
    }
}
