
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Platforms.iOS.DependencyServices;

[assembly: Dependency(typeof(StatusBarImplementation))]
namespace ZATCAMAUI.Platforms.iOS.DependencyServices
{
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
