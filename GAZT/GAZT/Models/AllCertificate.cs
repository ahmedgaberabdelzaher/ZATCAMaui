using System;
using System.Collections.Generic;
using Xamarin.Forms.Internals;

namespace GAZT.Models
{[Preserve(AllMembers = true)]

    public class Result
    {
        public string Descript { get; set; }
        public string CaseId { get; set; }
        public string LetterNum { get; set; }
        public string Auditor { get; set; }
        public string TaxtpFg { get; set; }
        public string UserTin { get; set; }
        public string Pdfurl { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class ZakatSet
    {
        public List<Result> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class VATSet
    {
        public List<Result> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class ExciseSet
    {
        public List<Result> results { get; set; }
    }
    [Preserve(AllMembers = true)]
    public class AllCertificate
    {
        public ZakatSet ZakatSet { get; set; }
        public VATSet VATSet { get; set; }
        public ExciseSet ExciseSet { get; set; }
    }
}
