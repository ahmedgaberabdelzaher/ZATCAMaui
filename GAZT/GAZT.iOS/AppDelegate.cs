using EGAZT;
using Foundation;
using Microsoft.AppCenter.Distribute;
using Syncfusion.ListView.XForms.iOS;
using Syncfusion.SfBusyIndicator.XForms.iOS;
using Syncfusion.SfCalendar.XForms.iOS;
using Syncfusion.SfPicker.XForms.iOS;
using Syncfusion.SfRotator.iOS;
using Syncfusion.XForms.Cards;
using Syncfusion.XForms.Graphics;
using Syncfusion.XForms.iOS.Cards;
using Syncfusion.XForms.iOS.EffectsView;
using Syncfusion.XForms.iOS.MaskedEdit;
using Syncfusion.XForms.iOS.TextInputLayout;
using Syncfusion.XForms.Pickers.iOS;
using System.Net;
using Tavant.XToolkit;
using UIKit;
using Xamarin;
using Xamarin.Forms;
using AppDynamics.Agent;
using Syncfusion.SfRotator.XForms.iOS;
using Syncfusion.XForms.iOS.Graphics;
using Syncfusion.XForms.iOS.Buttons;
using System;

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
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();
            ServicePointManager
            .ServerCertificateValidationCallback +=
            (sender, cert, chain, sslPolicyErrors) => true;
            Xamarin.FormsMaps.Init();
            Xamarin.Forms.Forms.Init();
            InitRoundedCornerView.Init();
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
            new Syncfusion.SfNavigationDrawer.XForms.iOS.SfNavigationDrawerRenderer();

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
            SfMaskedEditRenderer.Init();

            // Add the below line if you are using SfLinearProgressBar.
            Syncfusion.XForms.iOS.ProgressBar.SfLinearProgressBarRenderer.Init();

            // Add the below line if you are using SfCircularProgressBar.  
            Syncfusion.XForms.iOS.ProgressBar.SfCircularProgressBarRenderer.Init();

            SfRotatorRenderer.Init();
            SfButtonRenderer.Init();
            SfGradientViewRenderer.Init();

            Distribute.DontCheckForUpdatesInDebug();
            Xamarin.FormsGoogleMaps.Init("AIzaSyCnIhK1NNzYNX-pZ1JjZpsLAXzHPgQOgSM");

            App.InitializeAppDynamics();

            LoadApplication(iosapp);
            //Code for PUSH notification
            //UIView statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
            //if (statusBar != null && statusBar.RespondsToSelector(new ObjCRuntime.Selector("setBackgroundColor:")))
            //{
            //    statusBar.BackgroundColor = UIColor.Green;// //Color.FromHex("#7f6550").ToUIColor(); // change to your desired color 
            //}

            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, new NSSet());
                UIApplication.SharedApplication.RegisterUserNotificationSettings(pushSettings);
                UIApplication.SharedApplication.RegisterForRemoteNotifications();
            }
            else
            {
                UIRemoteNotificationType notificationType = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
                UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationType);
            }


            return base.FinishedLaunching(app, options);
        }

        public override void OnActivated(UIApplication application)
        {
            Console.WriteLine("OnActivated called, App is active.");
        }

        public override void WillEnterForeground(UIApplication application)
        {
            if (App.IsLoginPageVisible() == false)
            {
                App.ShouldStopTimer = true;
            }
            else
            {
                App.ShouldStopLoginRefreshTimer = true;
            }
           
            Console.WriteLine("App will enter foreground");
        }

        public override void OnResignActivation(UIApplication application)
        {
            Console.WriteLine("OnResignActivation called, App moving to inactive state.");
        }

        public override void DidEnterBackground(UIApplication application)
        {
            if(App.IsLoginPageVisible() == false)
            {
                App.ShouldStopTimer = false;
                App.DoesLoginNeedToBeRefreshed = false;
                App.StartTimerForBackground(0, 5, 0);
                //Console.WriteLine("App entering background state.");
            }
            else
            {
                App.ShouldStopLoginRefreshTimer = false;
                App.StartTimerForLoginRefresh(0, 3, 0);
            }
        }

        // not guaranteed that this will run
        public override void WillTerminate(UIApplication application)
        {
            Console.WriteLine("App is terminating.");
        }

        //Code for PUSH notification
        //public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        //{
        //    new UIAlertView("Error whie registering for Push Notifications", error.LocalizedDescription, null, "Ok", null).Show();
        //}
        //public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        //{
        //    String DeviceToken = deviceToken.Description;
        //    if (!String.IsNullOrWhiteSpace(DeviceToken))
        //    {
        //        DeviceToken = DeviceToken.Trim('<').Trim('>');
        //    }
        //    Console.WriteLine("Device Token: " + DeviceToken);
        //    NSUserDefaults.StandardUserDefaults.SetString(DeviceToken, "PushDeviceToken");
        //}
        //public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
        //{
        //    NSDictionary aps = userInfo.ObjectForKey(new NSString("aps")) as NSDictionary;
        //    String alert = string.Empty;
        //    if (aps.ContainsKey(new NSString("alert")))
        //        alert = (aps[new NSString("alert")] as NSString).ToString();
        //    Console.WriteLine(userInfo);
        //    //show alert
        //    if (!string.IsNullOrEmpty(alert))
        //    {
        //        UIAlertView avAlert = new UIAlertView("Notification", alert, null, "OK", null);
        //        avAlert.Show();
        //    }
        //}

        //        BOOL isJailbroken()
        //        {
        //#if !(TARGET_IPHONE_SIMULATOR)

        //            if ([[NSFileManager defaultManager] fileExistsAtPath: @"/Applications/Cydia.app"] ||

        //                 [[NSFileManager defaultManager] fileExistsAtPath: @"/Library/MobileSubstrate/MobileSubstrate.dylib"] ||

        //                  [[NSFileManager defaultManager] fileExistsAtPath: @"/bin/bash"] ||

        //                   [[NSFileManager defaultManager] fileExistsAtPath: @"/usr/sbin/sshd"] ||

        //                    [[NSFileManager defaultManager] fileExistsAtPath: @"/etc/apt"] ||

        //                     [[NSFileManager defaultManager] fileExistsAtPath: @"/private/var/lib/apt/"] ||

        //                      [[UIApplication sharedApplication] canOpenURL:[NSURL URLWithString:@"cydia://package/com.example.package"]])  {
        //                return YES;
        //            }

        //            FILE* f = NULL;
        //            if ((f = fopen("/bin/bash", "r")) ||
        //               (f = fopen("/Applications/Cydia.app", "r")) ||
        //               (f = fopen("/Library/MobileSubstrate/MobileSubstrate.dylib", "r")) ||
        //               (f = fopen("/usr/sbin/sshd", "r")) ||
        //               (f = fopen("/etc/apt", "r")))
        //            {
        //                fclose(f);
        //                return YES;
        //            }
        //            fclose(f);

        //            NSError* error;
        //            NSString* stringToBeWritten = @"This is a test.";
        //            [stringToBeWritten writeToFile:@"/private/jailbreak.txt" atomically: YES encoding:NSUTF8StringEncoding error:&error];
        //            [[NSFileManager defaultManager] removeItemAtPath: @"/private/jailbreak.txt" error: nil];
        //            if (error == nil)
        //            {
        //                return YES;
        //            }

        //#endif

        //            return NO;
        //        }

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
