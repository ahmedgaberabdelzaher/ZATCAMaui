using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace GAZT.Models
{
    [Preserve(AllMembers = true)]
    public class FormBundleMetadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    public class FormBundleResult
    {
        public FormBundleMetadata __metadata { get; set; }
        public string Lang { get; set; }
        public string Gpart { get; set; }
        public string Fbtyp { get; set; }
        public string Txt50 { get; set; }
    }
    public class FormBundleD
    {
        public List<FormBundleResult> results { get; set; }
    }
    public class FormBundleModel
    {
        public FormBundleD d { get; set; }
    }
    public class FormBundleApplicationNumberModelMetadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    public class FormBundleApplicationNumberModelResult
    {
        public FormBundleApplicationNumberModelMetadata __metadata { get; set; }
        public string Fbstatus { get; set; }
        public string Lang { get; set; }
        public string Gpart { get; set; }
        public string Fbtyp { get; set; }
        public string Txt50 { get; set; }
        public string Fbnum { get; set; }
        public string PeriodKey { get; set; }
        public string Perslt { get; set; }
        public string Fbsta { get; set; }
        public string Fbstb { get; set; }
        public string Fbust { get; set; }
    }
    public class FormBundleApplicationNumberModelD
    {
        public List<FormBundleApplicationNumberModelResult> results { get; set; }
    }
    public class FormBundleApplicationNumberModel
    {
        public FormBundleApplicationNumberModelD d { get; set; }
    }
    //
    public class FbnumDetailList
    {
        public string Fbnum { get; set; }
        public string Fbsta { get; set; }
        public string FbStatus { get; set; }
        public string FbDesc { get; set; }
    }
}
