using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using EGAZT;
using Foundation;
using GAZT;
using GAZT.CustomControl;
using GAZT.iOS.CustomRenderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms.PlatformConfiguration;
[assembly: ExportRenderer(typeof(GAZTBorderlessEntry), typeof(GAZTBorderlessEntryRenderer))]
namespace GAZT.iOS.CustomRenderer
{
    [Xamarin.Forms.Internals.Preserve(AllMembers = true)]
    public class GAZTBorderlessEntryRenderer : EntryRenderer
    {
        public static void Init() { }
        double fontSize;
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Control != null)
            {
                if (App.IsArabic)
                {
                    Control.TextAlignment = UITextAlignment.Right;
                }
                else
                {
                    Control.TextAlignment = UITextAlignment.Left;
                }
                //if (StyleId.Equals("OTPEntry"))
                //{
                //    Control.TextAlignment = UITextAlignment.Center;
                //}
                base.OnElementPropertyChanged(sender, e);
                Control.Layer.BorderWidth = 0;
                Control.BorderStyle = UITextBorderStyle.None;
                Control.Layer.BorderWidth = 0;
                Control.BorderStyle = UITextBorderStyle.None;
               // Control.BackgroundColor = UIColor Color.FromHex("#CCE0DC");
            }
            //fontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            //this.Control.Font = UIFont.FromName(NSBundle.MainBundle.LocalizedString("FontName", ""), (float)fontSize);
            // this.Control.Font = UIFont.SystemFontOfSize((float)fontSize);
            //fontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            //if (App.IsArabic)
            //    this.Control.Font = UIFont.FromName("Cairo-Regular", (float)fontSize);
            //else
            //this.Control.Font = UIFont.FromName("Helvetica-Normal", (float)fontSize);
        }
    }
}