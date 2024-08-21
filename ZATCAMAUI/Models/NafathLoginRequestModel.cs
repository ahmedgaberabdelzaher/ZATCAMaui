
using Newtonsoft.Json;
namespace ZATCAMAUI.Models
{
    
    public class NafathLoginRequestModel
    {
        //public string ApiCall { get; set; }
        [JsonProperty("idNumber")]
        public string Idnumber { get; set; }
        [JsonProperty("inputChannel")]
        public string Inpchz { get; set; }
        [JsonProperty("language")]
        public string Langz { get; set; }
        public string processType { get; set; }
        
    }
}
