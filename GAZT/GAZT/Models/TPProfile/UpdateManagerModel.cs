using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Xamarin.Forms.Internals;

namespace EGAZT.Models.TPProfile
{
    [Preserve(AllMembers = true)]
    public class UpdateManagerModel
	{
        [JsonProperty("d")]
        public D D { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class D
    {
        [JsonProperty("results")]
        public List<ManagerList> Results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class ManagerList
    {
        [JsonProperty("__metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("Gpart")]
        public string Gpart { get; set; }

        [JsonProperty("Editfg")]
        public string Editfg { get; set; }

        [JsonProperty("Mgrid")]
        public string Mgrid { get; set; }

        [JsonProperty("Mgrnm")]
        public string Mgrnm { get; set; }

        [JsonProperty("BirthDt")]
        public object BirthDt { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("MobNo")]
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
    [Preserve(AllMembers = true)]
    public class ManagerDetailsSet
    {
        [JsonProperty("BirthDt")]
        public object BirthDt;

        [JsonProperty("Editfg")]
        public string Editfg;

        [JsonProperty("Email")]
        public string Email;

        [JsonProperty("Gpart")]
        public string Gpart;

        [JsonProperty("Mgrid")]
        public string Mgrid;

        [JsonProperty("Mgrnm")]
        public string Mgrnm;

        [JsonProperty("MobNo")]
        public string MobNo;
    }
    [Preserve(AllMembers = true)]
    public class ManagerDetailsPayload
    {
        [JsonProperty("ManagerDetailsSet")]
        public List<ManagerDetailsSet> ManagerDetailsSet;

        [JsonProperty("Operation")]
        public string Operation;

        [JsonProperty("Taxpayer")]
        public string Taxpayer;
    }
}

