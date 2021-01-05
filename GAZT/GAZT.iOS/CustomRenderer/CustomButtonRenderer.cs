using System;
using EGAZT;
using GAZT;
using GAZT.iOS.CustomRenderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.iOS;
[assembly: ExportRenderer(typeof(CustomButton), typeof(CustomButtonRenderer))]
namespace GAZT
{
    [Preserve(AllMembers = true)]
    public class CustomButtonRenderer : ButtonRenderer
    {
        double fontSize;
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            var button = Control;
            try
            {
                if (button != null)
                {
                    //SetColors();
                    fontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                    button.Layer.CornerRadius = 10;
                    button.VerticalAlignment = UIControlContentVerticalAlignment.Center;
                    if (App.IsArabic)
                        button.Font = UIFont.FromName("Cairo-Regular", (float)fontSize);
                    else
                        button.Font = UIFont.FromName("Helvetica-Normal", (float)fontSize);
                }
            }
            catch(Exception gex)
            {
            }
        }
    }
}
