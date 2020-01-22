using System;
using System.Collections.Generic;
using System.Text;

namespace GAZT.Models
{
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

    public class ZakatReturnStatus
    {
        public string Value { get; set; }
        public int ID { get; set; }
    }

    public class SalesDetailsAttachments
    {
        public string Id { get; set; }
        public string DocumentName { get; set; }
        public string Size { get; set; }
    }
}
