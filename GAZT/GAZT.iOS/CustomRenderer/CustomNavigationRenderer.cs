using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using UIKit;
using GAZT.CustomControl;
using GAZT.iOS.CustomRenderer;
using CoreGraphics;
using EGAZT;

[assembly: ExportRenderer(typeof(CustomNavigation), typeof(CustomNavigationRenderer))]
namespace GAZT.iOS.CustomRenderer
{
    public class CustomNavigationRenderer: NavigationRenderer
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
            this.NavigationBar.ShadowImage = new UIImage();
            var height = NavigationBar.Bounds.Height;
            App.NavigationBarHeightt = height;
            UIFont ft;
            //this.NavigationBar.TintColor = UIColor.Yellow;
            // this.NavigationBar.BarTintColor = UIColor.Green;
            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes()
            {
                Font = UIFont.FromName("GE_SS_Two_Medium", 16),
                TextColor = UIColor.White
            });
            //this.NavigationBar.TitleTextAttributes = new UIStringAttributes()
            //{
            //    Font = UIFont.FromName("SSTArabic-Medium", 16)
            //};
            //if (App.IsArabic)
            //    ft = UIFont.FromName("Cairo-Regular", 16);
            //else
            //    ft = UIFont.FromName("Helvetica-Normal", 16);
            //this.NavigationBar.TitleTextAttributes = new UIStringAttributes()
            //{
            //    //Font = ft
            //};
            //try
            //{
            //    MessagingCenter.Unsubscribe<string>(this, "SetFont");
            //}
            //catch (Exception exe)
            //{
            //    System.Diagnostics.Debug.WriteLine("Exception: " + exe.Message);
            //}
            //MessagingCenter.Subscribe<string>(this, "SetFont", message =>
            //{
            //    if (App.IsArabic)
            //        ft = UIFont.FromName("Cairo-Regular", 16);
            //    else
            //        ft = UIFont.FromName("Helvetica-Normal", 16);
            //    try
            //    {
            //        UIBarButtonItem backButton = new UIBarButtonItem();
            //        backButton.Title = "bbbb"; //Your BackBurron Title here
            //        NavigationController.NavigationBar.TopItem.BackBarButtonItem = backButton;
            //    }
            //    catch (Exception)
            //    {
            //    }
            //    //this.NavigationBar.TitleTextAttributes = new UIStringAttributes()
            //    //{
            //    //    Font = ft
            //    //};
            //    // NavigationBar.EffectiveUserInterfaceLayoutDirection = UIUserInterfaceLayoutDirection.RightToLeft;
            //});
            //    UIBarButtonItem button = new UIBarButtonItem("Logout", UIBarButtonItemStyle.Plain, (sender, e) =>
            //    {
            //        var Confirm = new UIAlertView("Confirmation", "Are you Sure You Want to Logout?", null, "Cancel", "Confirm");
            //        Confirm.Show();
            //        Confirm.Clicked += (object senders, UIButtonEventArgs es) =>
            //        {
            //            if (es.ButtonIndex == 0)
            //            {
            //                // do something if cancel
            //            }
            //            else
            //            {
            //                // Do something if yes
            //                this.NavigationController.PopViewController(true);
            //            }
            //        };
            //    });
            //    this.NavigationItem.LeftBarButtonItem = button;
            //}
        }
    }
}
