using System;
using System.Collections.Generic;
using Xamarin.Forms.Internals;


namespace EGAZT.Models
{
    [Preserve(AllMembers = true)]
    public class MyBillsFilterDropdownResult
    {
        public MyBillsFilterDropdownList d { get; set; }
    }

    [Preserve(AllMembers = true)]
    public class MyBillsFilterDropdownList
    {
        public List<MyBillsFilterDropdown> results { get; set; }

    }
    [Preserve(AllMembers = true)]
    public class MyBillsFilterDropdown
    {
        public Metadata __metadata { get; set; }
        public string Mandt { get; set; }
        public string Spras { get; set; }
        public string TaxType { get; set; }
        public DateTime FromDate { get; set; }
        public string StatementFilter { get; set; }
        public string Counter { get; set; }
        public string Contractobject { get; set; }
        public string AbtypPs { get; set; }
        public string Txt30 { get; set; }
        public DateTime ToDate { get; set; }
        public string Txt30Mobile { get; set; }
    }
}