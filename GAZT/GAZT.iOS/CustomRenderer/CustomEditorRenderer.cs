using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using GAZT.iOS.CustomRenderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Editor), typeof(CustomEditorRenderer))]
namespace GAZT.iOS.CustomRenderer
{
    public class CustomEditorRenderer: EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if(Control != null)
            {
                Control.TintColor = UIColor.White;
            }
        }

    }
}