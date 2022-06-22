using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using WebView = Xamarin.Forms.WebView;
using System.Net;
using Android.Graphics;
using Android.Webkit;
using GAZT.Droid.CustomRenderer;
using Org.Apache.Http.Impl.Client;
using System.Runtime.Remoting.Contexts;
using Android.Content;
using Java.Interop;
using GAZT.Models;
using System.Collections.Generic;
using Android.Net.Http;
using Android.OS;
using EGAZT;
using EGAZT.Droid.CustomRenderer;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Net.Http;
using GAZT.Manager;
using EGAZT.Views.SyncFusionEnabledViews.SFLogin;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(HybridWebView), typeof(HybridWebViewRenderer))]
namespace EGAZT.Droid.CustomRenderer
{
    public class HybridWebViewRenderer
         : Xamarin.Forms.Platform.Android.WebViewRenderer
    {
        const string JavascriptFunction = "function invokeCSharpAction(data){jsBridge.invokeAction(data);}";
        Android.Content.Context _context;

        public HybridWebViewRenderer(Android.Content.Context context) : base(context)
        {
            _context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                Control.RemoveJavascriptInterface("jsBridge");

                Control.Settings.JavaScriptEnabled = true;
                Control.Settings.DomStorageEnabled = true;

                //Control.Settings.MixedContentMode = MixedContentHandling.NeverAllow;

                Control.SetWebViewClient(new HybridWebViewClient((HybridWebView)Element));
                Control.AddJavascriptInterface(new JSBridge(this), "jsBridge");

                //((HybridWebView)Element).Cleanup();
            }

            if (e.NewElement != null)
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback += (o, cert, chain, errors) => true;
                Control.Settings.JavaScriptEnabled = true;
                Control.Settings.DomStorageEnabled = true;
                Control.Settings.JavaScriptCanOpenWindowsAutomatically = true;

                Control.Settings.MixedContentMode = MixedContentHandling.AlwaysAllow;
                Control.SetWebViewClient(new HybridWebViewClient((HybridWebView)Element));

                var tempElement = (HybridWebView)e.NewElement;
                tempElement.RefreshCommand = () =>
                {
                    string apiUrl = ((HybridWebView)Element).Url;
                    ResetCookies();

                    string langTemp = "en";
                    if (App.IsArabic == true)
                    {
                        langTemp = "ar";
                    }

                    Dictionary<string, string> headersTemp = new Dictionary<string, string>
                    {
                        [GAZT.Helper.Constants.LanguageCookieNameForLogin] = langTemp,
                    };

                    Control.LoadUrl(apiUrl, headersTemp);
                    App.ArePreLoginLangCookiesSet = true;
                    tempElement.FadeTo(1, 1000);
                };

                ResetCookies();

                string lang = "en";
                if (App.IsArabic == true)
                {
                    lang = "ar";
                }

                Dictionary<string, string> headers = new Dictionary<string, string>
                {
                    [GAZT.Helper.Constants.LanguageCookieNameForLogin] = lang,
                };

                
                Control.LoadUrl(((HybridWebView)Element).Url, headers);
                
                //Control.SetWebViewClient(new JavascriptWebViewClient(this, $"javascript: {JavascriptFunction}"));
                Control.AddJavascriptInterface(new JSBridge(this), "jsBridge");
            }
        }

        private void ResetCookies()
        {
            CookieManager.Instance.RemoveAllCookie();
            CookieManager.Instance.RemoveSessionCookie();
            var cookieManager = CookieManager.Instance;

            string lang = "en";
            if (App.IsArabic == true)
            {
                lang = "ar";
            }

            string cookieValue = lang;
            string cookieDomain = GAZT.Helper.Constants.PartialDomainUrlForCookies;
            string cookieName = GAZT.Helper.Constants.LanguageCookieNameForLogin;

            cookieManager.SetCookie(cookieDomain, cookieName + "=" + cookieValue);
        }
    }

    public class JavascriptWebViewClient : FormsWebViewClient
    {
        string _javascript;

        public JavascriptWebViewClient(HybridWebViewRenderer renderer, string javascript) : base(renderer)
        {
            _javascript = javascript;
        }

        [Obsolete]
        public override void OnReceivedError(Android.Webkit.WebView view, ClientError errorCode, string description, string failingUrl)
        {
            base.OnReceivedError(view, errorCode, description, failingUrl);
            Console.WriteLine(failingUrl, errorCode, description);
        }

        public override void OnReceivedLoginRequest(Android.Webkit.WebView view, string realm, string account, string args)
        {
            base.OnReceivedLoginRequest(view, realm, account, args);
            Console.WriteLine("OnReceivedLoginRequest");
        }

        public override void OnReceivedSslError(Android.Webkit.WebView view, SslErrorHandler handler, SslError error)
        {
            base.OnReceivedSslError(view, handler, error);
            System.String message = "Certificate error.";

            switch (error.PrimaryError)
            {
                case SslErrorType.Untrusted:
                    message = "The certificate authority is not trusted.";
                    break;
                case SslErrorType.Expired:
                    message = "The certificate has expired.";
                    break;
                case SslErrorType.Idmismatch:
                    message = "The certificate Hostname mismatch.";
                    break;
                case SslErrorType.Notyetvalid:
                    message = "The certificate is not yet valid.";
                    break;
            }

            handler.Proceed();

            Console.WriteLine(message);
        }

        public override void OnPageFinished(global::Android.Webkit.WebView view, string url)
        {
            base.OnPageFinished(view, url);

            try
            {
                var cookieHeader = CookieManager.Instance.GetCookie(url);
                App.LoginCookiesRetrieved = new List<CookieModel>();
                view.EvaluateJavascript(_javascript, null);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }

    public class JSBridge : Java.Lang.Object
    {
        readonly WeakReference<HybridWebViewRenderer> hybridWebViewRenderer;

        public JSBridge(HybridWebViewRenderer hybridRenderer)
        {
            hybridWebViewRenderer = new WeakReference<HybridWebViewRenderer>(hybridRenderer);
        }

        [JavascriptInterface]
        [Export("invokeAction")]
        public void InvokeAction(string data)
        {
            HybridWebViewRenderer hybridRenderer;

            if (hybridWebViewRenderer != null && hybridWebViewRenderer.TryGetTarget(out hybridRenderer))
            {
                ((HybridWebView)hybridRenderer.Element).InvokeAction(data);
            }
        }
    }

    internal class HybridWebViewClient : WebViewClient
    {
        private HybridWebView _hybridWebView;
        private bool IsError = false;

        internal HybridWebViewClient(HybridWebView hybridWebView)
        {
            var cookieManager = CookieManager.Instance;
            _hybridWebView = hybridWebView;
            cookieManager.SetAcceptCookie(true);
        }

        public override void OnPageStarted(global::Android.Webkit.WebView view, string url, Bitmap favicon)
        {
            if (url.ToString().Contains("IsFGTCK=Y"))
            {
                _hybridWebView.InvokeAction("navigateToForgotUsernamePage");
            }

            if (url.ToString().Contains("IsRstPw=Y"))
            {
                _hybridWebView.InvokeAction("navigateToUnlockAccountPage");
            }

            if (url.ToString().Contains("IsSIGNUP=Y"))
            {
                _hybridWebView.InvokeAction("navigateToVATIndividualSignupPage");
            }

            if (url.ToString().Contains("IsBacktoLogin=Y"))
            {
                _hybridWebView.InvokeAction("navigateBackToLoginPage");
            }

            if (url.ToString().Contains(GAZT.Helper.Constants.DomainUrlForCookies))
            {
                App.IsLoginCalled = true;
            }

            if (App.IsLoginCalled == true && App.IsSamlApiCalledAndroid == false)
            {
                try
                {
                    if (url.ToString().Contains(GAZT.Helper.Constants.DomainUrlForCookies))
                    {
                        _hybridWebView.InvokeAction("displayLoginLoadingIndicator");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
                }
            }

            if (url.ToString().Contains(GAZT.Helper.Constants.GAZTSAMLLoginServicePart))
            {
                App.IsSamlApiCalledAndroid = true;
            }

            //rohith changes

            if (App.IsSamlApiCalledAndroid == true && IsError == false)
            {
                _hybridWebView.InvokeAction("displayLoginLoadingIndicator");
            }
            //rohith changes

            base.OnPageStarted(view, url, favicon);
        }

        public override void OnReceivedSslError(Android.Webkit.WebView view, SslErrorHandler handler, SslError error)
        {
            base.OnReceivedSslError(view, handler, error);
            System.String message = "Certificate error.";

            switch (error.PrimaryError)
            {
                case SslErrorType.Untrusted:
                    message = "The certificate authority is not trusted.";
                    break;
                case SslErrorType.Expired:
                    message = "The certificate has expired.";
                    break;
                case SslErrorType.Idmismatch:
                    message = "The certificate Hostname mismatch.";
                    break;
                case SslErrorType.Notyetvalid:
                    message = "The certificate is not yet valid.";
                    break;
            }

            handler.Proceed();

            Console.WriteLine(message);
        }

        public override void OnReceivedHttpError(Android.Webkit.WebView view, IWebResourceRequest request, WebResourceResponse errorResponse)
        {
            base.OnReceivedHttpError(view, request, errorResponse);
        }

        public override void OnReceivedError(Android.Webkit.WebView view, IWebResourceRequest request, WebResourceError error)
        {
            //_hybridWebView.InvokeAction("requestTimedOut");
            base.OnReceivedError(view, request, error);
            //try
            //{
            //    if (error != null && error.ErrorCode == ClientError.Timeout)
            //    {
            //        _hybridWebView.Opacity = 0;
            //        IsError = true;
            //        App.IsLoginCalled = false;
            //        App.IsSamlApiCalledAndroid = false;
            //        App.LoginDataRetrieved = new LoginModel();
            //        App.LoginDataRetrieved.ResponseStatusMessage = "requestTimedout";
            //        _hybridWebView.InvokeAction("requestTimedout");
            //    }

            //}
            //catch(Exception ex)
            //{

            //}
        }

        public override void OnPageFinished(global::Android.Webkit.WebView view, string url)
        {
            var cookieHeader = CookieManager.Instance.GetCookie(url);
             
            string[] cookiePairs = new string[100];
            Android.Webkit.WebView webView;

            if (cookieHeader != null)
            {
                cookiePairs = cookieHeader.Split(';');
                webView = view;
            }

            //Hide element by class name
            //view.LoadUrl("javascript:document.getElementById('taxTypes_items').style.display = 'none'; void(0);");

            if (url.ToString().Contains(GAZT.Helper.Constants.DomainUrlForCookies))
            {
                _hybridWebView.InvokeAction("hideLoadingIndicator");
            }

            //https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZTP_ACCOUNT_SRV/GetInfoSet(Euser='',DeviceId='',FcmId='',DeviceTyp='')?sap-language=EN&$format=json
            if (url.ToString().Contains(GAZT.Helper.Constants.GAZTSAMLLoginServicePart) && App.IsLoginCalled == true)
            {
                //view.LoadUrl("javascript:window.HTMLOUT.processHTML('<head>'+document.getElementsByTagName('html')[0].innerHTML+'</head>');")
                App.LoginCookiesRetrieved = new List<CookieModel>();
                for (int i = 0; i < cookiePairs.Length; i++)
                {
                    try
                    {
                        IList<Java.Net.HttpCookie> allCookies = Java.Net.HttpCookie.Parse(cookiePairs[i]);

                        foreach (Java.Net.HttpCookie httpCookie in allCookies)
                        {
                            CookieModel cookie = new CookieModel();
                            cookie.CName = httpCookie.Name;
                            cookie.CValue = httpCookie.Value;
                            cookie.Domain = httpCookie.Domain;
                            App.LoginCookiesRetrieved.Add(cookie);
                        }

                        Console.WriteLine(allCookies);
                    }
                    catch (System.Exception ex)
                    {
                        Console.Write(ex.ToString());
                        Console.Write(ex.StackTrace.ToString());
                    }
                }

                try
                {
                    App.httpClientHandler = new System.Net.Http.HttpClientHandler();
                    App.httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    Console.Write(ex.StackTrace.ToString());
                }

                App.LoginDataRetrieved = new LoginModel();

                try
                {
                    App.LoginDataRetrieved = WebServiceManager.SFGAZTGetLoginDataAndroid(url);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                if (App.LoginDataRetrieved.TIN != null && App.LoginDataRetrieved.ResponseStatusMessage == null)
                {
                    if (App.LoginDataRetrieved.MsgTitle != null && App.LoginDataRetrieved.MsgTitle.Length >= 2)
                    {
                        _hybridWebView.Opacity = 0;
                        IsError = true;
                        App.IsLoginCalled = false;
                        App.IsSamlApiCalledAndroid = false;
                        App.LoginDataRetrieved.ResponseStatusMessage = "error";
                        _hybridWebView.InvokeAction("error");
                    }
                    else
                    {

                        App.LoginDataRetrieved.ResponseStatusMessage = "success";
                        _hybridWebView.InvokeAction("success");
                    }
                }
                else
                {
                    _hybridWebView.Opacity = 0;
                    IsError = true;
                    App.IsLoginCalled = false;
                    App.IsSamlApiCalledAndroid = false;
                    App.LoginDataRetrieved.ResponseStatusMessage = "errorGeneric";
                    _hybridWebView.InvokeAction("errorGeneric");
                }
                //rohith changes

                //view.EvaluateJavascript("javascript:document.getElementsByTagName('pre')[0].innerHTML.toString();", this);
            }
            base.OnPageFinished(view, url);
        }


        //public void OnReceiveValue(Java.Lang.Object value)
        //{
        //    Console.WriteLine(Convert.ToString(value));

        //    Console.WriteLine(value.ToString());

        //    try
        //    {
        //        HtmlDocument document = new HtmlDocument();
        //        document.LoadHtml(value.ToString());

        //        var htmlResponse = document.DocumentNode.InnerText;

        //        string LoginConfirmation = Convert.ToString(value);

        //        if (!string.IsNullOrEmpty(LoginConfirmation))
        //        {
        //            LoginConfirmation = JObject.Parse(LoginConfirmation)["d"].ToString();
        //            App.LoginDataRetrieved = JsonConvert.DeserializeObject<LoginModel>(LoginConfirmation.ToString());
        //        }

        //        if (App.LoginDataRetrieved.TIN != null)
        //        {
        //            App.LoginDataRetrieved.ResponseStatusMessage = "success";
        //            _hybridWebView.InvokeAction("success");
        //        }
        //        else
        //        {
        //            App.LoginDataRetrieved.ResponseStatusMessage = "error";
        //            _hybridWebView.InvokeAction("error");
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}
    }
}