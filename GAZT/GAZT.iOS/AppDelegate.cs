using EGAZT;
using Foundation;
using Syncfusion.ListView.XForms.iOS;
using Syncfusion.SfBusyIndicator.XForms.iOS;
using Syncfusion.SfCalendar.XForms.iOS;
using Syncfusion.SfPicker.XForms.iOS;
using Syncfusion.SfRotator.iOS;
using Syncfusion.XForms.Cards;
using Syncfusion.XForms.Graphics;
using Syncfusion.XForms.iOS.Cards;
using Syncfusion.XForms.iOS.EffectsView;
//using Syncfusion.XForms.iOS.MaskedEdit;
using Syncfusion.XForms.iOS.TextInputLayout;
using Syncfusion.XForms.Pickers.iOS;
using System.Net;
using UIKit;
using AppDynamics.Agent;
using Syncfusion.SfRotator.XForms.iOS;
using Syncfusion.XForms.iOS.Graphics;
using Syncfusion.XForms.iOS.Buttons;
using System;
using System.Threading.Tasks;

namespace GAZT.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;

            ZXing.Net.Mobile.Forms.iOS.Platform.Init();
            ServicePointManager
            .ServerCertificateValidationCallback +=
            (sender, cert, chain, sslPolicyErrors) => true;
            Xamarin.FormsMaps.Init();
            Xamarin.Forms.Forms.Init();
            Rg.Plugins.Popup.Popup.Init();
            //  UINavigationBar.Appearance.TintColor = UIColor.Red;
            App.AppVersion = NSBundle.MainBundle.InfoDictionary["CFBundleShortVersionString"].ToString();
            App iosapp = new App();
            App.appObj = iosapp;
            SfTextInputLayoutRenderer.Init();
            Syncfusion.XForms.iOS.Buttons.SfCheckBoxRenderer.Init();
            Syncfusion.XForms.iOS.Border.SfBorderRenderer.Init();
            SfDatePickerRenderer.Init();
            SfPickerRenderer.Init();
            SfCalendarRenderer.Init();
            new SfBusyIndicatorRenderer();
            SfCardLayoutRenderer.Init();
       
            new SfRotator();
            SfListViewRenderer.Init();
            SfEffectsViewRenderer.Init();  //Initialize only when effects view is added to Listview.
            new SfCardView();
            Syncfusion.XForms.iOS.Graphics.SfGradientViewRenderer.Init();
            new SfLinearGradientBrush();
            Syncfusion.XForms.iOS.TabView.SfTabViewRenderer.Init();
            Syncfusion.SfChart.XForms.iOS.Renderers.SfChartRenderer.Init();
            Syncfusion.XForms.iOS.Expander.SfExpanderRenderer.Init();
            Syncfusion.SfPdfViewer.XForms.iOS.SfPdfDocumentViewRenderer.Init();
            Syncfusion.SfRangeSlider.XForms.iOS.SfRangeSliderRenderer.Init();
            //SfMaskedEditRenderer.Init();

            var config = AppDynamics.Agent.AgentConfiguration.Create("EUM-AAB-AUM");
            config.LoggingLevel = AppDynamics.Agent.LoggingLevel.Debug;
            AppDynamics.Agent.Instrumentation.enableAggregateExceptionReporting = true;
            config.CollectorURL = "https://eum.gazt.gov.sa:443";
            AppDynamics.Agent.Instrumentation.InitWithConfiguration(config);


            // Add the below line if you are using SfLinearProgressBar.
            Syncfusion.XForms.iOS.ProgressBar.SfLinearProgressBarRenderer.Init();

            // Add the below line if you are using SfCircularProgressBar.  
            Syncfusion.XForms.iOS.ProgressBar.SfCircularProgressBarRenderer.Init();

            SfRotatorRenderer.Init();
            SfButtonRenderer.Init();
            SfGradientViewRenderer.Init();

            //Distribute.DontCheckForUpdatesInDebug();
            Xamarin.FormsGoogleMaps.Init("AIzaSyCnIhK1NNzYNX-pZ1JjZpsLAXzHPgQOgSM");

            App.InitializeAppDynamics();

            LoadApplication(iosapp);

            //Code for PUSH notification
            //UIView statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
            //if (statusBar != null && statusBar.RespondsToSelector(new ObjCRuntime.Selector("setBackgroundColor:")))
            //{
            //    statusBar.BackgroundColor = UIColor.Green;// //Color.FromHex("#7f6550").ToUIColor(); // change to your desired color 
            //}

            //if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            //{
            //    var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, new NSSet());
            //    UIApplication.SharedApplication.RegisterUserNotificationSettings(pushSettings);
            //    UIApplication.SharedApplication.RegisterForRemoteNotifications();
            //}
            //else
            //{
            //    UIRemoteNotificationType notificationType = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
            //    UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationType);
            //}


           

            return base.FinishedLaunching(app, options);
        }



        #region unhandled exceptions
        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            var newExc = new System.Exception("CurrentDomainOnUnhandledException", unhandledExceptionEventArgs.ExceptionObject as System.Exception);

            AppDynamics.Agent.Instrumentation.ReportError(newExc, ErrorSeverityLevel.CRITICAL);
        }
        private static void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs)
        {
            var newExc = new System.Exception("TaskSchedulerOnUnobservedTaskException", unobservedTaskExceptionEventArgs.Exception);

            AppDynamics.Agent.Instrumentation.ReportError(newExc, ErrorSeverityLevel.CRITICAL);
        }
        #endregion
        public override void OnActivated(UIApplication application)
        {
            //App.IsAppRunningInBackground = false;
            //App.ResetAndContinueSession();
            Console.WriteLine("OnActivated called, App is active.");
        }

        public override void WillEnterForeground(UIApplication application)
        {
            //App.IsAppRunningInBackground = false;
            //App.ResetAndContinueSession();

            Console.WriteLine("App will enter foreground");
        }

        public override void OnResignActivation(UIApplication application)
        {
            Console.WriteLine("OnResignActivation called, App moving to inactive state.");
        }

        public override void DidEnterBackground(UIApplication application)
        {
            //App.IsAppRunningInBackground = true;
            //App.ResetAndContinueSession();
        }

        // not guaranteed that this will run
        public override void WillTerminate(UIApplication application)
        {
            Console.WriteLine("App is terminating.");
        }

        private static void InitArabicCalendarCrashFix()
        {
            var localeIdentifier = NSLocale.CurrentLocale.LocaleIdentifier;
            if (localeIdentifier == "ar_SA")
            {
                new System.Globalization.UmAlQuraCalendar();
            }
        }

    }
}
