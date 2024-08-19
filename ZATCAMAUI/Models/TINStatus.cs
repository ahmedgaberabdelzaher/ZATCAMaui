using Newtonsoft.Json;

namespace ZATCAMAUI.Models
{

    public class ConsumerRegisteration
    {
        public string activityName { get; set; }
        public string TIN { get; set; }
        public string calendarType { get; set; }
        public string idNumber { get; set; }
        public string date { get; set; }
        public string status { get; set; }
        public string statusDescription { get; set; }
    }
    
    public class ItemSet
    {
        public List<ConsumerRegisteration> results { get; set; }
    }
    
    public class CheckTINStatus
    {
        public Metadata __metadata { get; set; }
        public string language { get; set; }
        public string TIN { get; set; }
        public string date { get; set; }
        public string status { get; set; }
        public string statusDescription { get; set; }
        public List<ConsumerRegisteration> activities { get; set; }
    }
    
    public class TINStatus
    {
        [JsonProperty("data")]
        public CheckTINStatus d { get; set; }
    }
}
