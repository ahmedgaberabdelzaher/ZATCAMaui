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
using Plugin.Media;
using Java.Lang;
using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Com.Labiba.Bot.Others;

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
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            Xamarin.FormsMaps.Init(this, savedInstanceState);
            base.OnCreate(savedInstanceState);
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            await CrossMedia.Current.Initialize();
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
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

            //rohith - login
            //handler = new Handler();
            //r = new Runnable(() =>
            //{
            //    App.HandleSessionTimeout();
            //    startHandler();
            //});
            //startHandler();

            //action = () =>
            //{
            //    App.HandleSessionTimeout();
            //    resetDisconnectTimer();
            //};

            //disconnectHandler = new Handler(new MyHandlerICallback(this));
            //resetDisconnectTimer();   

            //StartTimerForLoginRefresh(0,1,0);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
            LoadApplication(app);
            Configuration.Connect(this);
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
            //App.IsAppRunningInBackground = false;
            //App.ResetAndContinueSession();
        }

        protected override void OnStop()
        {
            //if (App.IsLoginPageVisible() == false)
            //{
            //    App.ShouldStopTimer = false;
            //    App.DoesLoginNeedToBeRefreshed = false;
            //    App.StartTimerForBackground(0, 5, 0);
            //}
            //else
            //{
            //    App.ShouldStopLoginRefreshTimer = false;
            //    App.StartTimerForLoginRefresh(0, 3, 0);
            //}

            base.OnStop();
        }

        protected override void OnRestart()
        {
            //if (App.IsLoginPageVisible() == false)
            //{
            //    App.ShouldStopTimer = true;
            //}
            //else
            //{
            //    App.ShouldStopLoginRefreshTimer = true;
            //}

            base.OnRestart();
        }

        protected override void OnPause()
        {
            base.OnPause();
            //App.IsAppRunningInBackground = true;
            //App.ResetAndContinueSession();
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

            //global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
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
            App.OnBackPressed();
        }

        //public static bool ShouldStopLoginRefreshTimer = false;
        //public static bool InvalidateTimer = false;

        //public static void StartTimerForLoginRefresh(int h, int m, int sec)
        //{
        //    int hour = h;
        //    int mins = m;
        //    int counter = sec;

        //    Xamarin.Forms.Device.StartTimer(new TimeSpan(0, 0, 1), () =>
        //    {
        //        Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
        //        {
        //            counter = counter - 1;
        //            if (counter < 0)
        //            {
        //                counter = 59;
        //                mins = mins - 1;
        //                if (mins < 0)
        //                {
        //                    mins = 59;
        //                    hour = hour - 1;
        //                    if (hour < 0)
        //                    {
        //                        hour = 0;
        //                        mins = 0;
        //                        counter = 0;
        //                    }
        //                }
        //            }


        //            // LblCountDownTimer = string.Format("{0:00}:{1:00}", mins, counter);
        //        });

        //        if (ShouldStopLoginRefreshTimer == true)
        //        {
        //            return false;
        //        }

        //        if(InvalidateTimer == true)
        //        {

        //            InvalidateTimer = false;
        //            return false;
        //        }

        //        if (hour == 0 && mins == 0 && counter == 0)
        //        {
        //            App.IsLoginPageRefreshed = true;
        //            App.HandleSessionTimeout();

        //            mins = m;
        //            return true;
        //        }
        //        else
        //        {
        //            return true;
        //        }
        //    });
        //}

        //rohith-login

        //public void stopHandler()
        //{
        //    handler.RemoveCallbacks(r);
        //}

        //public void startHandler()
        //{
        //    handler.PostDelayed(r, App.IdleTimeToLogout * 50);
        //}
    }
}
