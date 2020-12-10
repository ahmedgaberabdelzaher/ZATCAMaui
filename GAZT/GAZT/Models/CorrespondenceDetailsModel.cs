using EGAZT.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace GAZT.Models
{
    [Preserve(AllMembers = true)]
    public class CorrespondenceDetailsModel
    {
    }
    [Preserve(AllMembers = true)]
    public class CorrespondenceDetailsMetadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class CorrespondenceDetailsResult
    {
        public Metadata __metadata { get; set; }
        public string Attfg { get; set; }
        public string Langu { get; set; }
        public string Cotyp { get; set; }
        public string Hotline { get; set; }
        public string Gpart { get; set; }
        public string Ltrno { get; set; }
        public string Tdformat { get; set; }
        public string Txtdo { get; set; }
        public string Fbnum { get; set; }
        public string Tdline { get; set; }
        public string Cokey { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class CorrespondenceDetailsD
    {
        public List<CorrespondenceDetailsResult> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class CorrespondenceDetailsRootObject
    {
        public CorrespondenceDetailsD d { get; set; }
    }
}
