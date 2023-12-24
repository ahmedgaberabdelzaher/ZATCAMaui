using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Content.PM;
namespace ZATCAMAUI.Platforms.Android
{
    [Activity(MainLauncher = true, Theme = "@style/Theme.Splash", NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            StartActivity(typeof(MainActivity));
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public bool IsPlayServiceAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode)) { }
                // msgText.Text = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                else
                {
                    //msgText.Text = "This device is not supported";
                    Console.Write("This device is not supported");
                    Finish();
                }
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
