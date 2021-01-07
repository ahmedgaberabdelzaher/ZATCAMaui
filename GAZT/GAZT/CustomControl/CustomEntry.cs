using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace GAZT
{
    [Preserve(AllMembers = true)]
    public class CustomEntry : BorderlessEntry
    {
        public CustomEntry()
        {
            //this.FontSize = Device.OnPlatform(
            //Device.GetNamedSize(NamedSize.Medium, this),
            //Device.GetNamedSize(NamedSize.Medium, this),
            //Device.GetNamedSize(NamedSize.Medium, this)
            //);
        }
    }
}
