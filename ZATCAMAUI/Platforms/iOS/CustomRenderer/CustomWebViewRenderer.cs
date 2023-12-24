using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using Microsoft.Maui.Controls.Platform;
using ZATCAMAUI.Core.CustomControls;
using ZATCAMAUI.Platforms.iOS.CustomRenderer;

[assembly: ExportRenderer(typeof(MyWebView), typeof(CustomWebViewRenderer))]
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
