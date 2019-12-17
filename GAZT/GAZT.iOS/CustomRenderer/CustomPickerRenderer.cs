using GAZT;
using GAZT.iOS.CustomRenderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace GAZT.iOS.CustomRenderer
{
     public class CustomPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Picker> e)        {
            base.OnElementChanged(e);
            var fontSize = 10;// Device.GetNamedSize(NamedSize.Small, typeof(Label));
            if(Control != null)
            {
                this.Control.BackgroundColor = UIColor.White;
            
                this.Control.BorderStyle = UITextBorderStyle.None;
                if (App.IsArabic)
                    this.Control.Font = UIFont.FromName("Cairo-Regular", (float)fontSize);
                else
                    this.Control.Font = UIFont.FromName("Helvetica-Normal", (float)fontSize);

                var element = (CustomPicker)this.Element;
                if (this.Control != null && this.Element != null && !string.IsNullOrEmpty(element.Image))
                {
                    var downarrow = UIImage.FromBundle(element.Image);
                    Control.RightViewMode = UITextFieldViewMode.Always;
                    Control.RightView = new UIImageView(downarrow);
                }

                if (App.IsArabic)
                {
                    Control.TextAlignment = UITextAlignment.Right;
                }
               
            }
            
        }
    }
}