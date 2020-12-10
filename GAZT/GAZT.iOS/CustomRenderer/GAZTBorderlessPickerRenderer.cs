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
[assembly: ExportRenderer(typeof(GAZTBorderlessPicker), typeof(GAZTBorderlessPickerRenderer))]
namespace GAZT.iOS.CustomRenderer
{
    [Xamarin.Forms.Internals.Preserve(AllMembers = true)]
    public class GAZTBorderlessPickerRenderer: PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Picker> e)
        {
            base.OnElementChanged(e);
            // var fontSize = 10;// Device.GetNamedSize(NamedSize.Small, typeof(Label));
            if (Control != null)
            {
                this.Control.BackgroundColor = UIColor.White;
                this.Control.BorderStyle = UITextBorderStyle.None;
                if (App.IsArabic)
                    this.Control.Font = UIFont.FromName("GE_SS_Two_Medium", 12);
                else
                    this.Control.Font = UIFont.FromName("GE_SS_Two_Medium", 12);
                // var element = (CustomPicker)this.Element;
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
            }
        }
    }
}