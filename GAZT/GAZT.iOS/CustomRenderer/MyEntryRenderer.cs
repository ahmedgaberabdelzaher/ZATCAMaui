using System;
using System.ComponentModel;
using GAZT.CustomControl;
using GAZT.iOS.CustomRenderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace GAZT.iOS.CustomRenderer
{
    public class MyEntryRenderer : EntryRenderer
    {
        double fontSize;

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            /*base.OnElementPropertyChanged(sender, e);
            Control.Layer.BorderWidth = 0;
            Control.BorderStyle = UITextBorderStyle.None;*/

            base.OnElementPropertyChanged(sender, e);
            if (Control != null)
            {
                Control.TextAlignment = UITextAlignment.Left;

                base.OnElementPropertyChanged(sender, e);
                Control.Layer.BorderWidth = 0;
                Control.BorderStyle = UITextBorderStyle.None;
                Control.TextColor = UIColor.Black;
            }

            fontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            fontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
        }
    }
}
