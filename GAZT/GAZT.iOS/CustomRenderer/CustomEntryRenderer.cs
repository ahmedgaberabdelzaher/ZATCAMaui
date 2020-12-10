using System;
using System.ComponentModel;
using System.Drawing;
using GAZT;
using GAZT.iOS.CustomRenderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.iOS;
[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace GAZT.iOS.CustomRenderer
{
    [Preserve(AllMembers = true)]
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
           this.AddDoneButton();
        }
        protected void AddDoneButton()
        {
            var toolbar = new UIToolbar(new RectangleF(0.0f, 0.0f, 50.0f, 44.0f));

            var doneButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate
            {
                this.Control.ResignFirstResponder();
                var baseEntry = this.Element.GetType();
                ((IEntryController)Element).SendCompleted();
            });

            toolbar.Items = new UIBarButtonItem[] {
                new UIBarButtonItem (UIBarButtonSystemItem.FlexibleSpace),
                doneButton
            };
            this.Control.InputAccessoryView = toolbar;
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
