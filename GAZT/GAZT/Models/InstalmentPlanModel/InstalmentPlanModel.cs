using System;
using Xamarin.Forms.Internals;

namespace EGAZT.Models.InstalmentPlanModel
{
    [Preserve(AllMembers = true)]
    public class InstalmentPlanModel
    {
        public InstalmentPlanModel()
        {
        }
        public string ActiveOutletDecisionOptions { get; set; }
        public bool ActiveOutletDecisionOptionsIsSelected { get; set; }
    }
}
