using Foundation;
using MediaManager;
using UIKit;

namespace ZATCAMAUI.Platforms.iOS;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
        CrossMediaManager.Current.Init();
        return base.FinishedLaunching(application, launchOptions);
    }
}
