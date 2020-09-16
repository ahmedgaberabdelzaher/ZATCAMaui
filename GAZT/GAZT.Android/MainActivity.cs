using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Views;
using EGAZT;
using Microsoft.AppCenter.Distribute;
using Plugin.Permissions;
using Tavant.XToolkit;
using AppDynamics.Agent;

namespace GAZT.Droid
{
    [Activity(Label = "GAZT E-Services", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false,ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Xamarin.FormsMaps.Init(this, savedInstanceState);
            base.OnCreate(savedInstanceState);
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.FormsGoogleMaps.Init(this, savedInstanceState);

            InitRoundedCornerView.Init();
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.P)
            {
                Window.Attributes.LayoutInDisplayCutoutMode = Android.Views.LayoutInDisplayCutoutMode.ShortEdges;
            } 
            // Xamarin.Essentials.Platform.Init(this, bundle);
            System.Net.ServicePointManager.ServerCertificateValidationCallback += (o, cert, chain, errors) => true;
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage) != (int)Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.WriteExternalStorage }, 0);
            }
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) != (int)Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.ReadExternalStorage }, 0);
            }
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.Camera) != (int)Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.Camera }, 0);
            }
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessFineLocation) != (int)Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.AccessFineLocation }, 0);
            }
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessCoarseLocation) != (int)Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.AccessCoarseLocation }, 0);
            }

            App.InitializeAppDynamics();

            PackageInfo info = this.PackageManager.GetPackageInfo(this.PackageName, 0);
            App.AppVersion = info.VersionName;
            App app = new App();
            App.appObj = app;
            Distribute.SetEnabledForDebuggableBuild(true);
            LoadApplication(app);
        }

        protected override void OnResume()
        {
            base.OnResume();
        }

        protected override void OnStop()
        {
            if (App.IsLoginPageVisible() == false)
            {
                App.ShouldStopTimer = false;
                App.DoesLoginNeedToBeRefreshed = false;
                App.StartTimerForBackground(0, 5, 0);
            }
            else
            {
                App.ShouldStopLoginRefreshTimer = false;
                App.StartTimerForLoginRefresh(0, 3, 0);
            }

            base.OnStop();
        }

        protected override void OnRestart()
        {
            if (App.IsLoginPageVisible() == false)
            {
                App.ShouldStopTimer = true;
            }
            else
            {
                App.ShouldStopLoginRefreshTimer = true;
            }

            base.OnRestart();
        }

        protected override void OnPause()
        {
           
            base.OnPause();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            for (int i = 0; i < permissions.Length; i++)
            {
                if (permissions[i].Equals("android.permission.CAMERA") && grantResults[i] == Permission.Granted)
                {
                    global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
                }
                if (permissions[i].Equals("android.permission.STORAGE") && grantResults[i] == Permission.Granted)
                {
                    global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
                }
                //else
                //{
                //    Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                //    AlertDialog alert = dialog.Create();
                //    alert.SetTitle("Alert");
                //    alert.SetMessage("Kindly Grant Camera Permission");
                //    alert.SetButton("OK", (c, ev) =>
                //    {
                //        // Ok button click task  
                //    });
                //    alert.Show();
                //}
            }
           // global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                // Do something if there are some pages in the `PopupStack`
            }
            else
            {
                // Do something if there are not any pages in the `PopupStack`
            }
        }
    }
}
