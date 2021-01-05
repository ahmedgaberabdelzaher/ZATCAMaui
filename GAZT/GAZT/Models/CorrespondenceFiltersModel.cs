using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace GAZT.Models
{
    [Preserve(AllMembers = true)]
    public class CorrespondenceFiltersModel
    {
        public int ID { get; set; }
        public string Filter { get; set; }
    }
}
