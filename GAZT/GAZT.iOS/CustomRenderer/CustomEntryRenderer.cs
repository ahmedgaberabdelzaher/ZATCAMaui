using System;
using System.ComponentModel;
using GAZT;
using GAZT.iOS.CustomRenderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace GAZT.iOS.CustomRenderer
{
    public class CustomEntryRenderer : EntryRenderer
    {
        double fontSize;

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //base.OnElementPropertyChanged(sender, e);
            //Control.Layer.BorderWidth = 0;
            //Control.BorderStyle = UITextBorderStyle.Line;
            //fontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            //if (App.IsArabic)
            //    this.Control.Font = UIFont.FromName("Cairo-Regular", (float)fontSize);
            //else
            //this.Control.Font = UIFont.FromName("Helvetica-Normal", (float)fontSize);

            //this.Control.Font = UIFont.SystemFontOfSize((float)fontSize);

           
        }
        //public CustomEntryRenderer()
        //{
        //    UIKeyboard.Notifications.ObserveWillShow((sender, args) => {

        //        if (Element != null)
        //        {
        //            Element.Margin = new Thickness(0, 0, 0, args.FrameEnd.Height); //push the entry up to keyboard height when keyboard is activated
        //        }
        //    });

        //    UIKeyboard.Notifications.ObserveWillHide((sender, args) => {

        //        if (Element != null)
        //        {
        //            Element.Margin = new Thickness(0); //set the margins to zero when keyboard is dismissed
        //        }

        //    });
        //}

    }
}
