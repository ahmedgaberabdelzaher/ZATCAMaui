using Newtonsoft.Json;

namespace ZATCAMAUI.Models.TPProfile
{

    public class UpdateManagerModel
    {
        [JsonProperty("data")]
        public D D { get; set; }

        [JsonProperty("result")]
        public D result { set { D = value; } }
    }
    
    public class D
    {
        [JsonProperty("managers")]
        public List<ManagerList> Results { get; set; }
    }
    
    public class ManagerList
    {
        [JsonProperty("__metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("TIN")]
        public string Gpart { get; set; }

        [JsonProperty("edit")]
        public string Editfg { get; set; }

        [JsonProperty("managerId")]
        public string Mgrid { get; set; }

        [JsonProperty("managerName")]
        public string Mgrnm { get; set; }

        [JsonProperty("birthDate")]
        public object BirthDt { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("mobileNumber")]
        public string MobNo { get; set; }

        [JsonIgnore]
        public bool EnableIDNumber { get; set; }
        [JsonIgnore]
        public bool EnableManagerName { get; set; }
        [JsonIgnore]
        public bool EnableBirthdate { get; set; }
        [JsonIgnore]
        public bool EnableMobNumber { get; set; }
        [JsonIgnore]
        public bool EnableEmail { get; set; }
    }
    
    public class ManagerDetailsSet
    {
        [JsonProperty("birthDate")]
        public object BirthDt;

        [JsonProperty("edit")]
        public string Editfg;

        [JsonProperty("email")]
        public string Email;

        [JsonProperty("TIN")]
        public string Gpart;

        [JsonProperty("managerId")]
        public string Mgrid;

        [JsonProperty("managerName")]
        public string Mgrnm;

        [JsonProperty("mobileNumber")]
        public string MobNo;
    }
    
    public class ManagerDetailsPayload
    {
        [JsonProperty("managers")]
        public List<ManagerDetailsSet> ManagerDetailsSet;

        [JsonProperty("operation")]
        public string Operation;

        [JsonProperty("TIN")]
        public string Taxpayer;
    }
}

