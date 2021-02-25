using GAZT;
using GAZT.iOS.CustomRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace GAZT.iOS.CustomRenderer
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