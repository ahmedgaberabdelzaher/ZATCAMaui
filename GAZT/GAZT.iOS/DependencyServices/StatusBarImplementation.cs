using System;
using GAZT.Helper;
using GAZT.iOS.DependencyServices;
using UIKit;
[assembly: Xamarin.Forms.Dependency(typeof(StatusBarImplementation))]
namespace GAZT.iOS.DependencyServices
{
    [Xamarin.Forms.Internals.Preserve(AllMembers = true)]
    public class StatusBarImplementation : IStatusBar
    {
        public StatusBarImplementation()
        {
        }
        #region IStatusBar implementation
        public void HideStatusBar()
        {
         //   UIApplication.SharedApplication.StatusBarHidden = true;
        }
        public void ShowStatusBar()
        {
           // UIApplication.SharedApplication.StatusBarHidden = false;
        }
        #endregion
    }
}
