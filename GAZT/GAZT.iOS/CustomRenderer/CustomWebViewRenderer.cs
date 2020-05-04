using System;
using System.IO;
using System.Net;
using Foundation;
using GAZT;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace GAZT
{
    [Foundation.Preserve(AllMembers = true)]
    public class CustomWebViewRenderer  : ViewRenderer<CustomWebView, UIWebView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<CustomWebView> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                SetNativeControl(new UIWebView());
            }
            if (e.OldElement != null)
            {
                // Cleanup
            }
            if (e.NewElement != null)
            {
                var customWebView = Element as CustomWebView;
                //string fileName = Path.Combine(NSBundle.MainBundle.BundlePath, string.Format("Content/{0}", customWebView.Uri));
                string fileName = Path.Combine(NSBundle.MainBundle.BundlePath, string.Format("Content/{0}", WebUtility.UrlEncode("https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_IT_CORR_MOOB_SRV/corr_dataSet(Cokey='005056B1F8FB1EDA87CF9DE76C046AEF',Cotyp='ZC05')/$value?saml2=disabled")));
                Control.LoadRequest(new NSUrlRequest(new NSUrl(fileName, false)));
                //Control.LoadRequest (new NSUrlRequest(new NSUrl(customWebView.Uri, false)));
                Control.ScalesPageToFit = true;
            }
        }
    }
}
