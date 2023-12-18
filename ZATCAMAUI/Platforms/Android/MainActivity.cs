using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using MediaManager;

namespace ZATCAMAUI.Platforms.Android;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        UserDialogs.Init(this);
        CrossMediaManager.Current.Init(this);
        base.OnCreate(savedInstanceState);
    }
}
