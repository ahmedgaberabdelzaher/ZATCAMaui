
using MediaManager;
using System.Net;
using UIKit;
using AppDynamics.Agent;
using Microsoft.Maui.Handlers;
using ZATCAMAUI.Core.CustomControls;
using Foundation;
using System.Drawing;

namespace ZATCAMAUI.Platforms.iOS;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    static nint timerTaskID;
    protected override MauiApp CreateMauiApp()
    {
        EntryHandler.Mapper.AppendToMapping("EntryBorderless", (handler, view) =>
        {
            if (view is GAZTBorderlessEntry)
            {
                handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
                handler.PlatformView.Layer.BorderWidth = 0;
                handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;

                //Create a new toolbar.
                var toolbar = new UIToolbar(new RectangleF(0.0f, 0.0f, 50.0f, 44.0f));

                //Create a new UIBarButton with a delegate to clear the focus on the Entry by calling a method that forces the text to stop being edited.
                var doneButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate
                {
                    handler.PlatformView.EndEditing(true);
                    handler.VirtualView.Completed();
                });

                //Add the button to the toolbar previosly created
                toolbar.Items = new UIBarButtonItem[]
                {
                    new UIBarButtonItem (UIBarButtonSystemItem.FlexibleSpace),doneButton
                };
                handler.PlatformView.InputAccessoryView = toolbar;


            }
        });

        EditorHandler.Mapper.AppendToMapping("EditorBorderless", (handler, view) =>
        {
            if (view is GAZTBorderlessEditor)
            {
                handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
                handler.PlatformView.Layer.BorderWidth = 0;
            }
        });

        SearchBarHandler.Mapper.AppendToMapping("CancelButtonColor", (handler, view) =>
        {
            handler.PlatformView.SetShowsCancelButton(false, false);
        });

        return MauiProgram.CreateMauiApp();
    }

    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
        try
        {


            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
            CrossMediaManager.Current.Init();
            //Firebase.Core.App.Configure();
            ServicePointManager
            .ServerCertificateValidationCallback +=
            (sender, cert, chain, sslPolicyErrors) => true;
            App.AppVersion = NSBundle.MainBundle.InfoDictionary["CFBundleShortVersionString"].ToString();

            //var config = AgentConfiguration.Create("EUM-AAB-AUM");
            //config.LoggingLevel = LoggingLevel.Debug;

            //// Instrumentation.enableAggregateExceptionReporting = true;
            //config.CollectorURL = "https://eum.gazt.gov.sa:443";
            //Instrumentation.InitWithConfiguration(config);

            //App.InitializeAppDynamics();

        }
        catch (Exception)
        {

        }


        return base.FinishedLaunching(application, launchOptions);
    }
    #region unhandled exceptions
    private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
    {
        var newExc = new System.Exception("CurrentDomainOnUnhandledException", unhandledExceptionEventArgs.ExceptionObject as System.Exception);

        Instrumentation.ReportError(newExc, ErrorSeverityLevel.CRITICAL);
    }
    private static void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs)
    {
        var newExc = new System.Exception("TaskSchedulerOnUnobservedTaskException", unobservedTaskExceptionEventArgs.Exception);

        Instrumentation.ReportError(newExc, ErrorSeverityLevel.CRITICAL);
    }
    #endregion
    public override void OnActivated(UIApplication application)
    {

    }

    public override void WillEnterForeground(UIApplication application)
    {


    }

    public override void OnResignActivation(UIApplication application)
    {

    }

    void EndTimerTask()
    {

        if (timerTaskID != 0)
        {
            UIApplication.SharedApplication.EndBackgroundTask(timerTaskID);
        }
    }

    public override void DidEnterBackground(UIApplication application)
    {
        timerTaskID = UIApplication.SharedApplication.BeginBackgroundTask(() =>
        {
            EndTimerTask();
        });

    }

    // not guaranteed that this will run
    public override void WillTerminate(UIApplication application)
    {

    }

    private static void InitArabicCalendarCrashFix()
    {
        var localeIdentifier = NSLocale.CurrentLocale.LocaleIdentifier;
        if (localeIdentifier == "ar_SA")
        {
            new System.Globalization.UmAlQuraCalendar();
        }
    }

    //Export("AEDMApplicationDidBecomeActive:")]
    //private static void AEDMApplicationDidBecomeActive(UIApplication application)
    //{
    //    DidBecomeActive(application);
    //}
    [Export("oneSignalApplicationDidBecomeActive:")]
    public void OneSignalApplicationDidBecomeActive(UIApplication application)
    {
        // Remove line if you don't have a OnActivated method.
        OnActivated(application);
    }

    [Export("oneSignalApplicationWillResignActive:")]
    public void OneSignalApplicationWillResignActive(UIApplication application)
    {
        // Remove line if you don't have a OnResignActivation method.
        OnResignActivation(application);
    }

    [Export("oneSignalApplicationDidEnterBackground:")]
    public void OneSignalApplicationDidEnterBackground(UIApplication application)
    {
        // Remove line if you don't have a DidEnterBackground method.
        DidEnterBackground(application);
    }

    [Export("oneSignalApplicationWillTerminate:")]
    public void OneSignalApplicationWillTerminate(UIApplication application)
    {
        // Remove line if you don't have a WillTerminate method.
        WillTerminate(application);
    }
}
