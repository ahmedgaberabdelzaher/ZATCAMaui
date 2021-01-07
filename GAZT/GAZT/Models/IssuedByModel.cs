using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace GAZT.Models
{
    [Preserve(AllMembers = true)]
    public class IssuedByModel
    {
    }
    [Preserve(AllMembers = true)]
    public class IssuedByMetadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class IssuedByResult
    {
        public IssuedByMetadata __metadata { get; set; }
        public string Response { get; set; }
        public string Request { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class IssuedByD
    {
        public List<IssuedByResult> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class IssuedByRootObject
    {
        public IssuedByD d { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class IssuedByResponse
    {
        public string mandt { get; set; }
        public string lang { get; set; }
        public string procsType { get; set; }
        public string elementCode { get; set; }
        public string txt50 { get; set; }
    }
}
