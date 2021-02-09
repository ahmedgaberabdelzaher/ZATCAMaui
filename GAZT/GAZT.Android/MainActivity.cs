using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Views;
using EGAZT;
using Plugin.Permissions;
using AppDynamics.Agent;
using Plugin.Media;
using Java.Lang;
using System;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace GAZT.Droid
{
    [Activity(Label = "GAZT E-Services", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]

    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        Handler handler;
        Runnable r;
        System.Action action;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            PreventLinkerFromStrippingCommonLocalizationReferences();
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            Xamarin.FormsMaps.Init(this, savedInstanceState);
            base.OnCreate(savedInstanceState);
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            await CrossMedia.Current.Initialize();
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Xamarin.FormsGoogleMaps.Init(this, savedInstanceState);

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

            var config = AppDynamics.Agent.AgentConfiguration.Create("EUM-AAB-AUM");
            config.LoggingLevel = AppDynamics.Agent.LoggingLevel.Debug;

            AppDynamics.Agent.Instrumentation.enableAggregateExceptionReporting = true;

            config.EnableAggregateExceptionReporting = true;
            config.CollectorURL = "https://eum.gazt.gov.sa";
            AppDynamics.Agent.Instrumentation.InitWithConfiguration(config);

            PackageInfo info = this.PackageManager.GetPackageInfo(this.PackageName, 0);
            App.AppVersion = info.VersionName;
            App app = new App();
            App.appObj = app;
            //Distribute.SetEnabledForDebuggableBuild(true);
           
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
            LoadApplication(app);
            global::Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>()
             .UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
        }
        private static void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs)
        {
            var newExc = new System.Exception("TaskSchedulerOnUnobservedTaskException", unobservedTaskExceptionEventArgs.Exception);

            LogUnhandledException(newExc);
        }
        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            var newExc = new System.Exception("CurrentDomainOnUnhandledException", unhandledExceptionEventArgs.ExceptionObject as System.Exception);

            LogUnhandledException(newExc);
        }

        internal static void LogUnhandledException(System.Exception exception)
        {
            try
            {
                const string errorFileName = "Fatal.log";
                var libraryPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); 
                var errorFilePath = Path.Combine(libraryPath, errorFileName);
                File.WriteAllText(errorFilePath, JsonConvert.SerializeObject(exception));
            }
            catch
            {
                // just suppress any error logging exceptions
            }
        }
        public class MyHandlerICallback : Java.Lang.Object, Handler.ICallback
        {
            private MainActivity mainActivity;

            public MyHandlerICallback(MainActivity mainActivity)
            {
                this.mainActivity = mainActivity;
            }

            public bool HandleMessage(Message msg)
            {
                //ToDo
                return true;
            }
        }

        public void stopDisconnectTimer()
        {
            disconnectHandler.RemoveCallbacks(action);
        }

        public static long DISCONNECT_TIMEOUT = 60000; // 5 min = 5 * 60 * 1000 ms

        public Handler disconnectHandler;

        public void resetDisconnectTimer()
        {
            disconnectHandler.RemoveCallbacks(action);
            disconnectHandler.PostDelayed(action, DISCONNECT_TIMEOUT);
        }

        public override void OnUserInteraction()
        {
            base.OnUserInteraction();
            Xamarin.Forms.Application.Current.Properties["timeOut"] = DateTime.Now;
        }

        protected override void OnResume()
        {
            base.OnResume();
        }

        protected override void OnStop()
        {

            base.OnStop();
        }

        protected override void OnRestart()
        {
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
            }

            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            App.OnBackPressed();
        }

        private void PreventLinkerFromStrippingCommonLocalizationReferences()
        {
            var gregorianCalendar = new System.Globalization.GregorianCalendar();
            var arabivAlQuraCalendar = new System.Globalization.UmAlQuraCalendar();
        }
    }
}
