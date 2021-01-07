using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using GAZT;
using GAZT.CustomControl;
using GAZT.iOS.CustomRenderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly: ExportRenderer(typeof(GAZTBorderlessEditor), typeof(GAZTBorderlessEditorRenderer))]
namespace GAZT.iOS.CustomRenderer
{
    [Xamarin.Forms.Internals.Preserve(AllMembers = true)]
    class GAZTBorderlessEditorRenderer : EditorRenderer
    {
        double fontSize;
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                fontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                this.Control.AutocapitalizationType = UIKit.UITextAutocapitalizationType.Sentences;
                //  this.Control.Font = UIFont.FromName("SourceSansPro-Regular", (float)fontSize);
                //if (App.IsArabic)
                //{
                //    this.Control.Font = UIFont.FromName("Cairo-Regular", (float)fontSize);
                //    this.Control.TextAlignment = UITextAlignment.Right;
                //}
                //else
                //    this.Control.Font = UIFont.FromName("Helvetica-Normal", (float)fontSize);
            }
        }
    }
}