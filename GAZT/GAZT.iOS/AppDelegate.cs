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
using Syncfusion.XForms.iOS.TextInputLayout;
using Syncfusion.XForms.Pickers.iOS;
using System.Net;
using Tavant.XToolkit;
using UIKit;
using Xamarin;

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
      
            ServicePointManager
            .ServerCertificateValidationCallback +=
            (sender, cert, chain, sslPolicyErrors) => true;
            Xamarin.FormsMaps.Init();
            Xamarin.Forms.Forms.Init();
            InitRoundedCornerView.Init();
            Rg.Plugins.Popup.Popup.Init();
            IQKeyboardManager.SharedManager.Enable = true;
            UINavigationBar.Appearance.TintColor = UIColor.Red;
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

            Distribute.DontCheckForUpdatesInDebug();

            LoadApplication(iosapp);

            //Code for PUSH notification

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
    }
}
