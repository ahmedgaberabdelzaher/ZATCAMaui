
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;
using GAZT.iOS.CustomRenderer;
using GAZT;
using CoreGraphics;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace GAZT.iOS.CustomRenderer
{
    public class BorderlessEntryForOTPViewRenderer : EntryRenderer
    {
        public static void Init()
        { }
        double fontSize;

     
       
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Control != null)
            {
                Control.Layer.BorderWidth = 0;
                Control.Layer.BorderColor = UIColor.FromRGB(52, 80, 44).CGColor;// new CGColor("#000000");
                Control.Layer.BackgroundColor = UIColor.FromRGB(0, 0, 0).CGColor;
                Control.BorderStyle = UITextBorderStyle.None;

                Control.TextAlignment = UITextAlignment.Center;
            }

           // fontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            //this.Control.Font = UIFont.FromName(NSBundle.MainBundle.LocalizedString("FontName", ""), (float)fontSize);
            // this.Control.Font = UIFont.SystemFontOfSize((float)fontSize);
           // fontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            //if (App.IsArabic)
            //    this.Control.Font = UIFont.FromName("Cairo-Regular", (float)fontSize);
            //else
            //this.Control.Font = UIFont.FromName("Helvetica-Normal", (float)fontSize);

        }
    }
}
