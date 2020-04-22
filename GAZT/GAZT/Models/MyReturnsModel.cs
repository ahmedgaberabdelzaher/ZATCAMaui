using System;
using System.Collections.Generic;
using System.Text;

namespace GAZT.Models
{
    public class MyReturnsModel
    {
    }

    public class MyReturnsMetadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class MyReturnsResult
    {
        public Metadata __metadata { get; set; }
        public string Gpart { get; set; }
        public string Lang { get; set; }
        public string Vkont { get; set; }
        public string Cokey { get; set; }
        public string Augrd { get; set; }
        public string Vtref { get; set; }
        public string Persl { get; set; }
        public string TaxPeriod { get; set; }
        public string Fbtyp { get; set; }
        public string FbtText { get; set; }
        public string Fbsta { get; set; }
        public string Fbust { get; set; }
        public string CalendarTyp { get; set; }
        public string StatusTxt { get; set; }
        public string Incotyp { get; set; }
        public string Incotext { get; set; }
        public DateTime Abrzu { get; set; }
        public DateTime Abrzo { get; set; }
        public string SadadDoc1 { get; set; }
        public string SadadDoc2 { get; set; }
        public object DueDt { get; set; }
        public string Stat { get; set; }
        public string RetStatTxt { get; set; }
        public string DueDtC { get; set; }
        public string Due { get; set; }
        public string Sortperiod { get; set; }
        public string TaxType { get; set; }
        public string Fbnum { get; set; }
    }

    public class MyReturnsD
    {
        public List<MyReturnsResult> results { get; set; }
    }

    public class MyReturnsRootObject
    {
        public MyReturnsD d { get; set; }
    }
}
