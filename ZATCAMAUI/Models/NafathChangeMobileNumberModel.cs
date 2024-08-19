using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZATCAMAUI.Models
{
    public class NafathChangeMobileNumberModel
    {
        [JsonProperty("GUID")]
        public string Guid { get; set; }
        [JsonProperty("idNumber")]
        public string Idnumber { get; set; }
        [JsonProperty("language")]
        public string Lang { get; set; }
        [JsonProperty("sourceId")]
        public string Scrid { get; set; }
        [JsonProperty("changeMobile")]
        public string Chmb { get; set; }
        [JsonProperty("description")]
        public string Str { get; set; }
        [JsonProperty("GUIDNF")]
        public string GuidNf { get; set; }
        public string messageDescription { get; set; }

        //public MOB_EXTENSetArray MOB_EXTENSet { get; set; }
        public List<MOB_EXTENSet> mobileExtensions { get; set; }
        //public TP_REGTYPSetArray TP_REGTYPSet { get; set; }
        public List<TP_REGTYPSet> taxpayerRegistrationTypes { get; set; }
    }

    public class MOB_EXTENSet
    {
        [JsonProperty("land")]
        public string Land1 { get; set; }
        [JsonProperty("telephoneTo")]
        public string Telefto { get; set; }
    }

    public class MOB_EXTENSetArray
    {
        public List<MOB_EXTENSet> results { get; set; }
    }

    public class TP_REGTYPSet
    {
        [JsonProperty("GUID")]
        public string Guid { get; set; }
        [JsonProperty("TIN")]
        public string Partner { get; set; }
        [JsonProperty("taxpayerName")]
        public string Tpname { get; set; }
        [JsonProperty("registrationType  ")]
        public string RegTyp { get; set; }
    }
    public class TP_REGTYPSetArray
    {
        public List<TP_REGTYPSet> results { get; set; }

    }

    public class NafathChangeMobileNumberModelResponse
    {
        [JsonProperty("result")]
        public NafathChangeMobileNumberModel d { get; set; }
    }




}
