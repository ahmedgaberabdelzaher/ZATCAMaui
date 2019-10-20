
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;
using GAZT.iOS.CustomRenderer;
using GAZT;
[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]

namespace GAZT.iOS.CustomRenderer
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
        public static void Init() { }
        double fontSize;

     
       
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Control != null)
            {
                Control.Layer.BorderWidth = 0;
                Control.BorderStyle = UITextBorderStyle.None;
                Control.TextColor = UIColor.Black;
                if (App.IsArabic)
                {
                    Control.TextAlignment = UITextAlignment.Right;
                }
               if(App.IsOTPiew)
                {
                    Control.TextAlignment = UITextAlignment.Center;
                }
                //if (StyleId.Equals("OTPEntry"))
                //{
                //    Control.TextAlignment = UITextAlignment.Center;
                //}
            }

            fontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            //this.Control.Font = UIFont.FromName(NSBundle.MainBundle.LocalizedString("FontName", ""), (float)fontSize);
            // this.Control.Font = UIFont.SystemFontOfSize((float)fontSize);
            fontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            //if (App.IsArabic)
            //    this.Control.Font = UIFont.FromName("Cairo-Regular", (float)fontSize);
            //else
            //this.Control.Font = UIFont.FromName("Helvetica-Normal", (float)fontSize);

        }
    }
}
