using System;
using System.Collections.Generic;
using System.Text;

namespace ZATCAMAUI.Models.NewModelAPI
{
    public class ESTFinancialMaxDateRequest
    {
        public string calendarType { get; set; }
        public string draft { get; set; }
        public string formBundleNumber { get; set; }
        public string financialType { get; set; }
        public string month { get; set; }
        public string TIN { get; set; }
        public string year { get; set; }
        public DateTime fiscalEndDate { get; set; }
        public string taxableDate { get; set; }
        public string fiscalEndDays { get; set; }
        public List<FinancialPeriod> financialPeriods { get; set; }

    }

    public class FinancialPeriod
    {
        public string financialPeriod { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
    }
}
