using EGAZT.CustomControl;
using GAZT.Droid.CustomRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(GAZT.CustomWebView), typeof(CustomWebViewRenderer))]
namespace GAZT.Droid.CustomRenderer
{
    public class CustomWebViewRenderer: WebViewRenderer
    {
        public CustomWebViewRenderer(Android.Content.Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);
            Control.VerticalScrollBarEnabled = false;
        }
    }
}
