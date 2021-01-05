using System;
using EGAZT;
using GAZT;
using GAZT.iOS.CustomRenderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace GAZT.iOS.CustomRenderer
{
    [Xamarin.Forms.Internals.Preserve(AllMembers = true)]
    public class CustomPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Picker> e)
        {
            base.OnElementChanged(e);
            // var fontSize = 10;// Device.GetNamedSize(NamedSize.Small, typeof(Label));
            CustomPicker element = null;
            if (Control != null)
            {
                this.Control.BackgroundColor = UIColor.White;
                this.Control.BorderStyle = UITextBorderStyle.None;
                //if (App.IsArabic)
                //    this.Control.Font = UIFont.FromName("SSTArabic-Medium", (float)fontSize);
                //else
                //    this.Control.Font = UIFont.FromName("SSTArabic-Medium", (float)fontSize);
                 element = (CustomPicker)this.Element;
                //if (this.Control != null && this.Element != null && !string.IsNullOrEmpty(element.Image))
                //{
                //    var downarrow = UIImage.FromBundle(element.Image);
                //    Control.RightViewMode = UITextFieldViewMode.Always;
                //    Control.RightView = new UIImageView(downarrow);
                //}
                if (App.IsArabic)
                {
                    Control.TextAlignment = UITextAlignment.Right;
                }
             //   SetUIButton(element.DoneButtonText);
            }
            else
            {
               // SetUIButton(element.DoneButtonText);
            }
        }
        public void SetUIButton(string doneButtonText)
        {
            UIToolbar toolbar = new UIToolbar();
            toolbar.BarStyle = UIBarStyle.Default;
            toolbar.Translucent = true;
            toolbar.SizeToFit();
            UIBarButtonItem doneButton = new UIBarButtonItem(String.IsNullOrEmpty(doneButtonText) ? "Go Corona" : doneButtonText, UIBarButtonItemStyle.Done, (s, ev) =>
            {
                Control.ResignFirstResponder();
            });
             UIBarButtonItem flexible = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);
            toolbar.SetItems(new UIBarButtonItem[] { doneButton, flexible }, true);
            Control.InputAccessoryView = toolbar;
        }
    }
}