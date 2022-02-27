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
            if (Control != null)
            {
                this.Control.BackgroundColor = UIColor.White;
                this.Control.BorderStyle = UITextBorderStyle.None;
                if (App.IsArabic)
                    this.Control.Font = UIFont.FromName("Somar-SemiBold", 12);
                else
                    this.Control.Font = UIFont.FromName("Somar-SemiBold", 12);

                if (App.IsArabic)
                {
                    Control.TextAlignment = UITextAlignment.Right;
                }
            }
        }
    }
}