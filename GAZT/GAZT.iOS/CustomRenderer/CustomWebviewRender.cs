using System;
using EGAZT.CustomControl;
using Foundation;
using GAZT.iOS.CustomRenderer;
using ObjCRuntime;
using UIKit;
using WebKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomWebview), typeof(CustomWebviewRender))]
namespace GAZT.iOS.CustomRenderer
{

     [Preserve(AllMembers = true)]
    public class CustomWebviewRender : WkWebViewRenderer
    {

        public CustomWebviewRender() : this(new WKWebViewConfiguration())
        {
        }

        public CustomWebviewRender(WKWebViewConfiguration config) : base(config)
        {
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                //...
            }

            if (e.NewElement != null)
            {
                this.NavigationDelegate = new NavigationDelegate();
            }

        }
    }
    [Preserve(AllMembers = true)]
    public class NavigationDelegate : WKNavigationDelegate
    {
        NSMutableArray multiCookieArr = new NSMutableArray();

        public override void DecidePolicy(WKWebView webView, WKNavigationResponse navigationResponse, [BlockProxy(typeof(Action))] Action<WKNavigationResponsePolicy> decisionHandler)
        {

            if (UIDevice.CurrentDevice.CheckSystemVersion(12, 0))
            {
                WKHttpCookieStore wKHttpCookieStore = webView.Configuration.WebsiteDataStore.HttpCookieStore;
                Console.WriteLine("wKHttpCookieStore is :" + wKHttpCookieStore.GetDebugDescription());
                wKHttpCookieStore.GetAllCookies(cookies =>
                {
                    if (cookies.Length > 0)
                    {
                        foreach (NSHttpCookie cookie in cookies)
                        {
                            NSHttpCookieStorage.SharedStorage.SetCookie(cookie);
                            Console.WriteLine("cookie is :" + cookie);
                        }

                    }
                });
            }
            else
            {
                NSHttpUrlResponse response = navigationResponse.Response as NSHttpUrlResponse;
                NSHttpCookie[] cookiesAll = NSHttpCookie.CookiesWithResponseHeaderFields(response.AllHeaderFields, response.Url);
                foreach (NSHttpCookie cookie in cookiesAll)
                {
                    Console.WriteLine("Here is the cookie inside wkwebview is :" + cookie);
                    NSArray cookieArr = NSArray.FromObjects(cookie.Name, cookie.Value, cookie.Domain, cookie.Path);
                    multiCookieArr.Add(cookieArr);
                }
                Console.WriteLine("cookie is :" + cookiesAll);
            }

            decisionHandler(WKNavigationResponsePolicy.Allow);

            //base.DecidePolicy(webView, navigationResponse, decisionHandler);
        }
    }
}
