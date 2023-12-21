using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using Microsoft.Maui.Controls.Platform;
using UIKit;
using ZATCAMAUI.Core.CustomControls;
using ZATCAMAUI.Platforms.iOS.CustomRenderer;

[assembly: ExportRenderer(typeof(CustomButton), typeof(CustomButtonRenderer))]
namespace ZATCAMAUI.Platforms.iOS.CustomRenderer
{
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
                        button.TitleLabel.Font = UIFont.FromName("Cairo-Regular", (float)fontSize);
                    else
                        button.TitleLabel.Font = UIFont.FromName("Helvetica-Normal", (float)fontSize);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
