using Android.Content;
using Android.Views;
using EGAZT.Helper;
using pdfjs.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
[assembly: ExportRenderer(typeof(MyWebView), typeof(PdfWebViewRenderer))]
namespace pdfjs.Droid
{
	public class PdfWebViewRenderer : WebViewRenderer
	{
		public PdfWebViewRenderer(Context context) : base(context)
		{
          
        }

         protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            
            base.OnElementChanged(e);
            Control.VerticalScrollBarEnabled = false;

        }

        public override bool DispatchTouchEvent(MotionEvent e)
        {
            Parent.RequestDisallowInterceptTouchEvent(true);
            return base.DispatchTouchEvent(e);
        }


    }
}
