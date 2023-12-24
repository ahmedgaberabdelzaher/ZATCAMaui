using Android.Views;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Platforms.Android.DependencyServices;

[assembly: Dependency(typeof(StatusBarImplementation))]
namespace ZATCAMAUI.Platforms.Android.DependencyServices
{
    public class StatusBarImplementation : IStatusBar
    {
        WindowManagerFlags _originalFlags;
        #region IStatusBar implementation
        public void HideStatusBar()
        {
            //var activity = (Activity)Forms.Context;
            //var attrs = activity.Window.Attributes;
            //_originalFlags = attrs.Flags;
            //attrs.Flags |= Android.Views.WindowManagerFlags.Fullscreen;
            //activity.Window.Attributes = attrs;
        }
        public void ShowStatusBar()
        {
            //var activity = (Activity)Forms.Context;
            //var attrs = activity.Window.Attributes;
            //attrs.Flags = _originalFlags;
            //activity.Window.Attributes = attrs;
        }
        #endregion
    }
}
