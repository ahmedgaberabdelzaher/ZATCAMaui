
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
namespace GAZT.Droid
{
    [Activity(MainLauncher = false, Theme = "@style/Theme.Splash", NoHistory = true, Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //base.OnCreate(savedInstanceState);
            ////   await Task.Delay(4000);
            //StartActivity(typeof(MainActivity));
        }
    }
}
