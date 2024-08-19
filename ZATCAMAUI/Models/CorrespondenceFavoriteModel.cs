
using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{

    public class CorrespondenceFavoriteModel
    {
        [JsonProperty("correspondenceKey")]
        public string Cokey { get; set; }
        [JsonProperty("correspondenceType")]
        public string Cotyp { get; set; }
        [JsonProperty("TIN")]
        public string Gpart { get; set; }
        [JsonProperty("contractAccount")]
        public string Vkont { get; set; }
        [JsonProperty("startDate")]
        public string Begdaz { get; set; }
        [JsonProperty("endDate")]
        public string Enddaz { get; set; }
        [JsonProperty("isFavourite")]
        public bool Zzfav { get; set; }
    }
}
