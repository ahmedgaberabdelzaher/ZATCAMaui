using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Xamarin.Forms.Internals;

namespace EGAZT.Models.Form5Models
{
    [Preserve(AllMembers = true)]
    public class ZakatForm5CityModel
    {
        [DataMember]
        public ZakatForm5CityDataResult d { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class __metadata
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string uri { get; set; }
        [DataMember]
        public string type { get; set; }

    }
    [Preserve(AllMembers = true)]

    public class Results
    {
        [DataMember]
        public __metadata __metadata { get; set; }
        [DataMember]
        public string Mandt { get; set; }
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string OldDescription { get; set; }

    }
    [Preserve(AllMembers = true)]
    public class GOVCODESet
    {
        [DataMember]
        public List<Results> results { get; set; }

    }
    [Preserve(AllMembers = true)]

    public class Results11
    {
        [DataMember]
        public __metadata __metadata { get; set; }
        [DataMember]
        public string Mandt { get; set; }
        [DataMember]
        public string Fbtyp { get; set; }
        [DataMember]
        public string Zgroup { get; set; }
        [DataMember]
        public string Button { get; set; }
        [DataMember]
        public string Srno { get; set; }
        [DataMember]
        public string Lang { get; set; }
        [DataMember]
        public string Msg { get; set; }

    }
    [Preserve(AllMembers = true)]
    public class MSGSet
    {
        [DataMember]
        public List<Results11> results { get; set; }

    }
    [Preserve(AllMembers = true)]

    public class Results12
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
    [Preserve(AllMembers = true)]
    public class Zsub_desc_ASet
    {
        [DataMember]
        public List<Results12> results { get; set; }

    }
    [Preserve(AllMembers = true)]

    public class Results13
    {
        [DataMember]
        public __metadata __metadata { get; set; }
        [DataMember]
        public string Mandt { get; set; }
        [DataMember]
        public string SysFlg { get; set; }
        [DataMember]
        public string Lang { get; set; }
        [DataMember]
        public string Fbtyp { get; set; }
        [DataMember]
        public string Sourceid { get; set; }
        [DataMember]
        public string Url { get; set; }

    }
    [Preserve(AllMembers = true)]
    public class URLSet
    {
        [DataMember]
        public List<Results13> results { get; set; }

    }
    [Preserve(AllMembers = true)]

    public class Results14
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
    [Preserve(AllMembers = true)]
    public class _Zsub_desc_ASet
    {
        [DataMember]
        public IList<Results14> results { get; set; }

    }
    [Preserve(AllMembers = true)]
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
    [Preserve(AllMembers = true)]
    public class Zsub_desc_ASet2
    {
        [DataMember]
        public IList<Results15> results { get; set; }

    }
    [Preserve(AllMembers = true)]
    public class Results16
    {
        [DataMember]
        public __metadata __metadata { get; set; }
        [DataMember]
        public string Spras { get; set; }
        [DataMember]
        public string Gensch { get; set; }
        [DataMember]
        public string Desciption { get; set; }
        [DataMember]
        public string Livsch { get; set; }
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public string Lstsch { get; set; }

    }
    [Preserve(AllMembers = true)]
    public class Zmain_descSet
    {
        [DataMember]
        public List<Results16> results { get; set; }

    }
    [Preserve(AllMembers = true)]
    public class Results17
    {
        [DataMember]
        public __metadata __metadata { get; set; }
        [DataMember]
        public string Langu { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public string CityCode { get; set; }
        [DataMember]
        public string CityName { get; set; }
        [DataMember]
        public string McCity { get; set; }
        [DataMember]
        public string CityShort { get; set; }
        [DataMember]
        public string CitySh10 { get; set; }
        [DataMember]
        public string CityExt { get; set; }

    }
    [Preserve(AllMembers = true)]
    public class ZcitySet
    {
        [DataMember]
        public List<Results17> results { get; set; }

    }
    [Preserve(AllMembers = true)]
    public class ZakatForm5CityDataResult
    {
        [DataMember]
        public __metadata __metadata { get; set; }
        [DataMember]
        public string Langu { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public GOVCODESet GOVCODESet { get; set; }
        [DataMember]
        public MSGSet MSGSet { get; set; }
        [DataMember]
        public Zsub_desc_ASet zsub_desc_A60Set { get; set; }
        [DataMember]
        public URLSet URLSet { get; set; }
        [DataMember]
        public Zsub_desc_ASet zsub_desc_A62Set { get; set; }
        [DataMember]
        public Zsub_desc_ASet zsub_desc_A61Set { get; set; }
        [DataMember]
        public Zmain_descSet zmain_descSet { get; set; }
        [DataMember]
        public ZcitySet zcitySet { get; set; }

    }
    //public class Application
    //{
    //    public D d { get; set; }

    //}
}
