using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace GAZT
{
    [Preserve(AllMembers = true)]
    public class CustomLabel : Label
    {
        public double LineSpacing { get; set; } = 0.5;
        public CustomLabel()
        {
        }
    }
}
