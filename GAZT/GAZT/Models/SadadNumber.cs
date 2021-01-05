using EGAZT.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms.Internals;

namespace GAZT.Models
{
    [Preserve(AllMembers = true)]
    public class IBANType
    {
        public string key { get; set; }
        public string Text { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class IBANIDNumber
    {
        public string Partner { get; set; }
        public string Idnumber { get; set; }
        public string Type { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class SadadNumber
    {
        public SadadNumberD d { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class SadadNumberMetadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class SadadNumberResult
    {
        public Metadata __metadata { get; set; }
        public string Fbnum { get; set; }
        public string Langu { get; set; }
        public string Sopbel { get; set; }
        public string TaxType { get; set; }
        public string Abtypt { get; set; }
        public string Betrh { get; set; }
        public string Waers { get; set; }
        public string Vtref { get; set; }
        public bool IsAutoAsmnt { get; set; }
        public string Fbust { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class SadadNumberD
    {
        public List<SadadNumberResult> results { get; set; }
    }
}
