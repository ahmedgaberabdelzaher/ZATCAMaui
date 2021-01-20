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
                base.OnElementPropertyChanged(sender, e);
                Control.Layer.BorderWidth = 0;
                Control.BorderStyle = UITextBorderStyle.None;
                Control.Layer.BorderWidth = 0;
                Control.BorderStyle = UITextBorderStyle.None;
            
            }
        }
    }
}