using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace GAZT.Models
{
    [Preserve(AllMembers = true)]
    public class ZakatReturns
    {
        public string IDNumber { get; set; }
        public string FiscalYear { get;set; }
        public string ReturnPeriod { get; set; }
        public string DueDate { get; set; }
        public string Status { get; set; }
        public string StatusImage { get; set; } = "ic_Paid.png";
        public string BorderColour { get; set; } = "#bfbebe";
    }

    [Preserve(AllMembers = true)]
    public class ZakatReturnStatus
    {
        public string key { get; set; }
        public int Value { get; set; }
    }

    [Preserve(AllMembers = true)]
    public class SalesDetailsAttachments
    {
        public string Id { get; set; }
        public string DocumentName { get; set; }
        public string Size { get; set; }
    }
}
