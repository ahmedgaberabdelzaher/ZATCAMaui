using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{

    public class InternationalMobileData
    {
        [JsonProperty("countryCode")]
        public string Land1 { get; set; }
        [JsonProperty("countryDescription")]
        public string Landx50 { get; set; }
        public string _telefto = string.Empty;
        [JsonProperty("telephoneCountryCode")]
        public string Telefto
        {
            get
            {

                return _telefto;
            }
            set
            {
                _telefto = "+" + value;
            }
        }

        [JsonProperty("language")]
        public string Spras { get; set; }
        [JsonProperty("countryName")]
        public string Landx { get; set; }
    }

}
