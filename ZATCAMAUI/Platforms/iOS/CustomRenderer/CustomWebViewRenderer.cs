using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using Microsoft.Maui.Controls.Platform;

namespace ZATCAMAUI.Platforms.iOS.CustomRenderer
{
    public class CustomWebViewRenderer : WkWebViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            (this.Subviews[0] as UIKit.UIScrollView).ShowsVerticalScrollIndicator = false;
        }
    }
}
