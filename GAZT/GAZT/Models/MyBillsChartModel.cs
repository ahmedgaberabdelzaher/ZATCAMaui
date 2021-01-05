using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace GAZT.Models
{
    [Preserve(AllMembers = true)]
    public class MyBillsChartModel
    {
        public string BillType { get; set; }
        public int BillCount { get; set; }

        public string TotalBillCount { get; set; }
        public Xamarin.Forms.Color BillColor { get; set; }
    }
}
