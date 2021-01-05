using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace GAZT.Helper
{
    [Preserve(AllMembers = true)]
    public interface IStatusBar
    {
        void HideStatusBar();
        void ShowStatusBar();
    }
}
