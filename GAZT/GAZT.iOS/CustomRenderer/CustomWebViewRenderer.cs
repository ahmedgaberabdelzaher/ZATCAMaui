using GAZT;
using GAZT.iOS.CustomRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace GAZT.iOS.CustomRenderer
{
    public class CustomWebViewRenderer : WkWebViewRenderer
    {
        //WkWebViewRenderer wkWebView;
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            (this.Subviews[0] as UIKit.UIScrollView).ShowsVerticalScrollIndicator = false;

            //if (Control != null) return;
            //var config = new WKWebViewConfiguration();
            //wkWebView = new WKWebView(Frame, config) { NavigationDelegate = new MyNavigationDelegate() };
            //SetNativeControl(wkWebView);
        }

        //public class MyNavigationDelegate : WKNavigationDelegate
        //{
        //    public override void DidFinishNavigation(WKWebView webView, WKNavigation navigation)
        //    {

        //        //get url here
        //        var url = webView.Url;

        //        //webView.LoadFileUrl();

        //    }
        //}
    }
}