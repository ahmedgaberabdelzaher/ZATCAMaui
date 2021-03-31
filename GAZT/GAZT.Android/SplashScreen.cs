using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using Android.App;
using Android.Content.PM;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Gms.Common;
using Firebase.Iid;
namespace GAZT.Droid
{
    [Activity(MainLauncher = true, Theme = "@style/Theme.Splash", NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //   await Task.Delay(4000);
           // var refreshedToken = FirebaseInstanceId.Instance.Token;
            StartActivity(typeof(MainActivity));
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
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
                //msgText.Text = "Google Play Service is available";
                Console.WriteLine("Google Play Service is available");
                return true;
            }
        }
    }
}
