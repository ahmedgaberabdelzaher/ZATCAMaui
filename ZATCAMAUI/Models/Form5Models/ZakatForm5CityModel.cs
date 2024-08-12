using System.Runtime.Serialization;
using Foundation;
using Newtonsoft.Json;

namespace ZATCAMAUI.Models.Form5Models
{
   
    public class ZakatForm5CityModel
    {
        [DataMember]
        public ZakatForm5CityDataResult d { get; set; }
    }
   
    public class __metadata
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string uri { get; set; }
        [DataMember]
        public string type { get; set; }

    }
   

    public class Results
    {
        [DataMember]
        public __metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("code")]
        public string Code { get; set; }
        [DataMember]
        [JsonProperty("description")]
        public string Description { get; set; }
        [DataMember]
        [JsonProperty("oldDescription")]
        public string OldDescription { get; set; }

    }
   
    public class GOVCODESet
    {
        [DataMember]
        [JsonProperty("governmentCodes")]
        public List<Results> results { get; set; }

    }
   

    public class _Results11
    {
        [DataMember]
        public __metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        [JsonProperty("formBundleType")]
        public string Fbtyp { get; set; }
        [DataMember]
        [JsonProperty("group")]
        public string Zgroup { get; set; }
        [DataMember]
        [JsonProperty("button")]
        public string Button { get; set; }
        [DataMember]
        [JsonProperty("serialNumber")]
        public string Srno { get; set; }
        [DataMember]
        [JsonProperty("language")]
        public string Lang { get; set; }
        [DataMember]
        [JsonProperty("message")]
        public string Msg { get; set; }

    }
   
    public class MSGSet
    {
        [DataMember]
        [JsonProperty("messages")]
        public List<Results11> results { get; set; }

    }
   

    public class _Results12
    {
        [DataMember]
        public __metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("language")]
        public string Spras { get; set; }
        [DataMember]
        [JsonProperty("descriptionCode")]
        public string Desciption { get; set; }
        [DataMember]
        [JsonProperty("subDescription")]
        public string SubDesc { get; set; }
        [DataMember]
        [JsonProperty("description")]
        public string Text { get; set; }

    }
   
    public class Zsub_desc_ASet
    {
        [DataMember]
        [JsonProperty("subDescriptionA60")]
        public List<Results12> results { get; set; }
    }
   

    public class _Results13
    {
        [DataMember]
        public __metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("systemCode")]
        public string Mandt { get; set; }
        [DataMember]
        //[JsonProperty("")]
        public string SysFlg { get; set; }
        [DataMember]
        [JsonProperty("language")]
        public string Lang { get; set; }
        [DataMember]
        [JsonProperty("formBundleType")]
        public string Fbtyp { get; set; }
        [DataMember]
        [JsonProperty("sourceId")]
        public string Sourceid { get; set; }
        [DataMember]
        [JsonProperty("URL")]
        public string Url { get; set; }

    }
   
    public class URLSet
    {
        [DataMember]
        [JsonProperty("URLs")]
        public List<Results13> results { get; set; }
    }
   

    public class _Results14
    {
        [DataMember]
        public __metadata __metadata { get; set; }
        [DataMember]
        public string Spras { get; set; }
        [DataMember]
        public string Desciption { get; set; }
        [DataMember]
        public string SubDesc { get; set; }
        [DataMember]
        public string Text { get; set; }

    }
   
    public class _Zsub_desc_ASet
    {
        [DataMember]
        public IList<_Results14> results { get; set; }

    }
   
    public class Results15
    {
        [DataMember]
        public __metadata __metadata { get; set; }
        [DataMember]
        public string Spras { get; set; }
        [DataMember]
        public string Desciption { get; set; }
        [DataMember]
        public string SubDesc { get; set; }
        [DataMember]
        public string Text { get; set; }

    }
   
    public class Zsub_desc_ASet2
    {
        [DataMember]
        public IList<Results15> results { get; set; }

    }
   
    public class Results16
    {
        [DataMember]
        public __metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("language")]
        public string Spras { get; set; }
        [DataMember]
        [JsonProperty("genralSchedule")]
        public string Gensch { get; set; }
        [DataMember]
        [JsonProperty("descriptionCode")]
        public string Desciption { get; set; }
        [DataMember]
        [JsonProperty("liveSchedule")]
        public string Livsch { get; set; }
        [DataMember]
        [JsonProperty("description")]
        public string Text { get; set; }
        [DataMember]
        [JsonProperty("lastSchedule")]
        public string Lstsch { get; set; }

    }
   
    public class Zmain_descSet
    {
        [DataMember]
        [JsonProperty("mainDescriptionA60")]
        public List<Results16> results { get; set; }

    }
   
    public class Results17
    {
        [DataMember]
        public __metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("language")]
        public string Langu { get; set; }
        [DataMember]
        [JsonProperty("country")]
        public string Country { get; set; }
        [DataMember]
        [JsonProperty("cityCode")]
        public string CityCode { get; set; }
        [DataMember]
        [JsonProperty("cityName")]
        public string CityName { get; set; }
        [DataMember]
        [JsonProperty("city")]
        public string McCity { get; set; }
        [DataMember]
        [JsonProperty("cityShort")]
        public string CityShort { get; set; }
        [DataMember]
        [JsonProperty("cityShort10")]
        public string CitySh10 { get; set; }
        [DataMember]
        [JsonProperty("cityExtension")]
        public string CityExt { get; set; }

    }
   
    public class ZcitySet
    {
        [DataMember]
        [JsonProperty("cities")]
        public List<Results17> results { get; set; }

    }
    [Preserve(AllMembers = true)]
    public class ZakatForm5CityDataResult
    {

        [DataMember]
        public __metadata __metadata { get; set; }
        [DataMember]
        [JsonProperty("language")]
        public string Langu { get; set; }
        [DataMember]
        [JsonProperty("country")]
        public string Country { get; set; }
        [DataMember]
        //[JsonProperty("governmentCodes")]
        //public List<Results> GOVCODESet { get; set;}
        [JsonProperty("governmentCodes")]
        public List<Results> GOVCODESET { get; set; }
        [DataMember]
        [JsonProperty("messages")]
        public List<Results11> MSGSet { get; set; }
        [DataMember]
        [JsonProperty("subDescriptionA60")]
        public List<Results12> zsub_desc_A60Set { get; set; }
        [DataMember]
        [JsonProperty("URLs")]
        public List<Results13> URLSet { get; set; }
        [DataMember]
        [JsonProperty("subDescriptionA62")]
        public List<Results12> zsub_desc_A62Set { get; set; }
        [DataMember]
        [JsonProperty("subDescriptionA61")]
        public List<Results12> zsub_desc_A61Set { get; set; }
        [DataMember]
        [JsonProperty("mainDescriptionA60")]
        public List<Results16> zmain_descSet { get; set; }
        [DataMember]
        [JsonProperty("cities")]
        public List<Results17> zcitySet { get; set; }

    }
    //public class Application
    //{
    //    public D d { get; set; }

    //}
}
