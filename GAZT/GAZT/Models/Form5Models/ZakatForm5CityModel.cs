using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace EGAZT.Models.Form5Models
{
    [Preserve(AllMembers = true)]
    public class ZakatForm5CityModel
    {

        public ZakatForm5CityDataResult d { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class __metadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }

    }
    [Preserve(AllMembers = true)]

    public class Results
    {
        public __metadata __metadata { get; set; }
        public string Mandt { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string OldDescription { get; set; }

    }
    [Preserve(AllMembers = true)]
    public class GOVCODESet
    {
        public IList<Results> results { get; set; }

    }
    [Preserve(AllMembers = true)]

    public class Results11
    {
        public __metadata __metadata { get; set; }
        public string Mandt { get; set; }
        public string Fbtyp { get; set; }
        public string Zgroup { get; set; }
        public string Button { get; set; }
        public string Srno { get; set; }
        public string Lang { get; set; }
        public string Msg { get; set; }

    }
    [Preserve(AllMembers = true)]
    public class MSGSet
    {
        public IList<Results11> results { get; set; }

    }
    [Preserve(AllMembers = true)]

    public class Results12
    {
        public __metadata __metadata { get; set; }
        public string Spras { get; set; }
        public string Desciption { get; set; }
        public string SubDesc { get; set; }
        public string Text { get; set; }

    }
    [Preserve(AllMembers = true)]
    public class Zsub_desc_ASet
    {
        public IList<Results12> results { get; set; }

    }
    [Preserve(AllMembers = true)]

    public class Results13
    {
        public __metadata __metadata { get; set; }
        public string Mandt { get; set; }
        public string SysFlg { get; set; }
        public string Lang { get; set; }
        public string Fbtyp { get; set; }
        public string Sourceid { get; set; }
        public string Url { get; set; }

    }
    [Preserve(AllMembers = true)]
    public class URLSet
    {
        public IList<Results13> results { get; set; }

    }
    [Preserve(AllMembers = true)]

    public class Results14
    {
        public __metadata __metadata { get; set; }
        public string Spras { get; set; }
        public string Desciption { get; set; }
        public string SubDesc { get; set; }
        public string Text { get; set; }

    }
    [Preserve(AllMembers = true)]
    public class _Zsub_desc_ASet
    {
        public IList<Results14> results { get; set; }

    }
    [Preserve(AllMembers = true)]
    public class Results15
    {
        public __metadata __metadata { get; set; }
        public string Spras { get; set; }
        public string Desciption { get; set; }
        public string SubDesc { get; set; }
        public string Text { get; set; }

    }
    [Preserve(AllMembers = true)]
    public class Zsub_desc_ASet2
    {
        public IList<Results15> results { get; set; }

    }
    [Preserve(AllMembers = true)]
    public class Results16
    {
        public __metadata __metadata { get; set; }
        public string Spras { get; set; }
        public string Gensch { get; set; }
        public string Desciption { get; set; }
        public string Livsch { get; set; }
        public string Text { get; set; }
        public string Lstsch { get; set; }

    }
    [Preserve(AllMembers = true)]
    public class Zmain_descSet
    {
        public IList<Results16> results { get; set; }

    }
    [Preserve(AllMembers = true)]
    public class Results17
    {
        public __metadata __metadata { get; set; }
        public string Langu { get; set; }
        public string Country { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string McCity { get; set; }
        public string CityShort { get; set; }
        public string CitySh10 { get; set; }
        public string CityExt { get; set; }

    }
    [Preserve(AllMembers = true)]
    public class ZcitySet
    {
        public IList<Results17> results { get; set; }

    }
    [Preserve(AllMembers = true)]
    public class ZakatForm5CityDataResult
    {
        public __metadata __metadata { get; set; }
        public string Langu { get; set; }
        public string Country { get; set; }
        public GOVCODESet GOVCODESet { get; set; }
        public MSGSet MSGSet { get; set; }
        public Zsub_desc_ASet zsub_desc_A60Set { get; set; }
        public URLSet URLSet { get; set; }
        public Zsub_desc_ASet zsub_desc_A62Set { get; set; }
        public Zsub_desc_ASet zsub_desc_A61Set { get; set; }
        public Zmain_descSet zmain_descSet { get; set; }
        public ZcitySet zcitySet { get; set; }

    }
    //public class Application
    //{
    //    public D d { get; set; }

    //}
}
