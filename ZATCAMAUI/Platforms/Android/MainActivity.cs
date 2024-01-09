using System.Net;
using Acr.UserDialogs;
using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using AppDynamics.Agent;
using Java.Lang;
using MediaManager;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using Newtonsoft.Json;
using RGPopup.Maui.Services;
using Instrumentation = AppDynamics.Agent.Instrumentation;

namespace ZATCAMAUI.Platforms.Android;

[Activity(Label = "GAZT E-Services", Theme = "@style/Theme.Splash", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]

public class MainActivity : MauiAppCompatActivity
{
    Handler handler;
    Runnable r;
    System.Action action;

    protected override async void OnCreate(Bundle savedInstanceState)
    {
        try
        {


            PreventLinkerFromStrippingCommonLocalizationReferences();
            CrossMediaManager.Current.Init(this);
            UserDialogs.Init(this);
            //TabLayoutResource = Resource.Layout.Tabbar;
            //ToolbarResource = Resource.Layout.Toolbar;
            Firebase.FirebaseApp.InitializeApp(this);
            //Xamarin.FormsMaps.Init(this, savedInstanceState);
            base.OnCreate(savedInstanceState);
            //ZXing.Net.Mobile.Forms.Android.Platform.Init();
            //await CrossMedia.Current.Initialize();
            //Init(this, savedInstanceState);
            //Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            //Xamarin.FormsGoogleMaps.Init(this, savedInstanceState);
            //Rg.Plugins.Popup.Popup.Init(this);
            if (Build.VERSION.SdkInt >= BuildVersionCodes.P)
            {
                Window.Attributes.LayoutInDisplayCutoutMode = LayoutInDisplayCutoutMode.ShortEdges;
            }

            ServicePointManager.ServerCertificateValidationCallback += (o, cert, chain, errors) => true;
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


            //Code for holding screenshots 
            //  Window.SetFlags(WindowManagerFlags.Secure, WindowManagerFlags.Secure);

            if ((ContextCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage) != (int)Permission.Granted)
            || (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) != (int)Permission.Granted))
            {
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage }, 0);
            }

            var config = AgentConfiguration.Create("EUM-AAB-AUM");
            config.LoggingLevel = LoggingLevel.Debug;

            //Instrumentation.enableAggregateExceptionReporting = true;

            config.EnableAggregateExceptionReporting = true;
            config.CollectorURL = "https://eum.gazt.gov.sa";
            Instrumentation.InitWithConfiguration(config);

            PackageInfo info = PackageManager.GetPackageInfo(this.PackageName, 0);
            App.AppVersion = info.VersionName;
            App app = new App();
            App.appObj = app;
            //Distribute.SetEnabledForDebuggableBuild(true);

            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
            //LoadApplication(app);
            App.Current.On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
        }
        catch (System.Exception ex)
        {

        }
    }

    //private void RequestStorageAccess()
    //{
    //    if (!Android.OS.Environment.IsExternalStorageManager)
    //    {
    //        StartActivityForResult(new Intent(Android.Provider.Settings.ActionManageAllFilesAccessPermission), 3);
    //    }
    //}

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
    private readonly int REQUEST;

    public void resetDisconnectTimer()
    {
        disconnectHandler.RemoveCallbacks(action);
        disconnectHandler.PostDelayed(action, DISCONNECT_TIMEOUT);
    }

    public override void OnUserInteraction()
    {
        base.OnUserInteraction();
        Preferences.Default.Set("timeOut", DateTime.Now);
    }

    protected override void OnResume()
    {
        base.OnResume();
    }

    protected override void OnStop()
    {

        base.OnStop();
        App.isAndroidRefresh = false;//#CR2068
    }

    protected override void OnRestart()
    {
        base.OnRestart();
        App.isAndroidRefresh = false;//#CR2068
    }

    protected override void OnPause()
    {
        base.OnPause();
        App.isAndroidRefresh = false;//#CR2068
    }

    public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
    {
        Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

    }

    public override async void OnBackPressed()
    {
        if (RGPopup.Maui.Droid.Popup.SendBackPressed(base.OnBackPressed))
        {
            await PopupNavigation.Instance.PopAsync(true);
        }
        else
        {
            App.OnBackPressed();
        }

    }

    private void PreventLinkerFromStrippingCommonLocalizationReferences()
    {
        var gregorianCalendar = new System.Globalization.GregorianCalendar();
        var arabivAlQuraCalendar = new System.Globalization.UmAlQuraCalendar();
    }
}