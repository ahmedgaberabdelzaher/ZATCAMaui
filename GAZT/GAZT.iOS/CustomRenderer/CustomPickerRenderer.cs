using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace GAZT.iOS.CustomRenderer
{
     public class CustomPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Picker> e)
        {
            base.OnElementChanged(e);
            var fontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            this.Control.BackgroundColor = UIColor.Clear;
            this.Control.BorderStyle = UITextBorderStyle.None;
            if (App.IsArabic)
                this.Control.Font = UIFont.FromName("Cairo-Regular", (float)fontSize);
            else
                this.Control.Font = UIFont.FromName("Helvetica-Normal", (float)fontSize);
        }
    }
}