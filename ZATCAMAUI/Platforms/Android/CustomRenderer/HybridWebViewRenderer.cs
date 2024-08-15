using Android.Content;
using Android.Graphics;
using Android.Net.Http;
using Android.Webkit;
using Java.Interop;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Platform;
using HybridWebView = ZATCAMAUI.Core.CustomControls.HybridWebView;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using WebView = Android.Webkit.WebView;
using System;
using System.Collections.Generic;

namespace ZATCAMAUI.Platforms.Android.CustomRenderer
{
    public class HybridWebViewRenderer :WebViewRenderer
    {
        const string JavascriptFunction = "function invokeCSharpAction(data){jsBridge.invokeAction(data);}";
        Context _context;

        public HybridWebViewRenderer(Context context) : base(context)
        {
            _context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Microsoft.Maui.Controls.WebView> e)
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
                        [ZATCAConstants.LanguageCookieNameForLogin] = langTemp,
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
                    [ZATCAConstants.LanguageCookieNameForLogin] = lang,
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
            string cookieDomain = ZATCAConstants.DevPartialDomainForCookies;
            string cookieName = ZATCAConstants.LanguageCookieNameForLogin;

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
        public override void OnReceivedError(WebView view, ClientError errorCode, string description, string failingUrl)
        {
            base.OnReceivedError(view, errorCode, description, failingUrl);
        }

        public override void OnReceivedLoginRequest(WebView view, string realm, string account, string args)
        {
            base.OnReceivedLoginRequest(view, realm, account, args);
        }


        public override void OnReceivedSslError(WebView view, SslErrorHandler handler, SslError error)
        {

            handler.Proceed();
        }

        public override void OnPageFinished(WebView view, string url)
        {
            base.OnPageFinished(view, url);

            try
            {
                var cookieHeader = CookieManager.Instance.GetCookie(url);
                App.LoginCookiesRetrieved = new List<CookieModel>();
                view.EvaluateJavascript(_javascript, null);
            }
            catch (System.Exception)
            {

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

            if (url.ToString().Contains("IsSSOLogon=Y"))
            {
                _hybridWebView.InvokeAction("displayLoadingIndicator");
                App.IsLoginCalled = true;
            }

            if (url.ToString().Contains("IsSignUp=Y&guid="))
            {
                string guid = url.ToString();
                App.GUIDFrSSO = guid;
                _hybridWebView.InvokeAction("navigateToVATIndividualSignupPageSSO");
            }


            if (url.ToString().Contains(ZATCAConstants.WebKeyChangeMobCompanay))
            {
                _hybridWebView.InvokeAction(ZATCAConstants.AppChangeMobCompanay);
            }

            if (url.ToString().Contains(ZATCAConstants.WebKeyChangeMobCompanayNafath))
            {
                string guid = url.ToString();
                App.GUIDFrChangeMob = guid;
                _hybridWebView.InvokeAction(ZATCAConstants.AppChangeMobCompanayNafath);
            }

            if (url.ToString().Contains(ZATCAConstants.DevDomainForCookies))
            {
                App.IsLoginCalled = true;
            }

            if (App.IsLoginCalled == true && App.IsSamlApiCalledAndroid == false)
            {
                try
                {
                    if (url.ToString().Contains(ZATCAConstants.DevDomainForCookies))
                    {
                        _hybridWebView.InvokeAction("displayLoginLoadingIndicator");
                    }
                }
                catch (Exception)
                {



                }
            }

            if (url.ToString().Contains(ZATCAConstants.GAZTSAMLLoginServicePart))
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

        public override void OnReceivedSslError(WebView view, SslErrorHandler handler, SslError error)
        {


            handler.Proceed();

        }

        public override void OnReceivedHttpError(WebView view, IWebResourceRequest request, WebResourceResponse errorResponse)
        {
            base.OnReceivedHttpError(view, request, errorResponse);
        }

        public override void OnReceivedError(WebView view, IWebResourceRequest request, WebResourceError error)
        {
            base.OnReceivedError(view, request, error);
        }

        public override void OnPageFinished(global::Android.Webkit.WebView view, string url)
        {
            var cookieHeader = CookieManager.Instance.GetCookie(url);

            string[] cookiePairs = new string[100];
           WebView webView;

            if (cookieHeader != null)
            {
                cookiePairs = cookieHeader.Split(';');
                webView = view;
            }


            if (url.ToString().Contains(ZATCAConstants.DevDomainForCookies))
            {
                _hybridWebView.InvokeAction("hideLoadingIndicator");
            }

            //https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZTP_ACCOUNT_SRV/GetInfoSet(Euser='',DeviceId='',FcmId='',DeviceTyp='')?sap-language=EN&$format=json
            if (url.ToString().Contains(ZATCAConstants.GAZTSAMLLoginServicePart) && App.IsLoginCalled == true)
            {
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
                    }
                    catch (System.Exception)
                    {


                    }
                }

                try
                {
                    App.httpClientHandler = new System.Net.Http.HttpClientHandler();
                    App.httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                }
                catch (Exception)
                {


                }

                App.LoginDataRetrieved = new LoginModel();

                try
                {
                    App.LoginDataRetrieved = WebServiceManager.SFGAZTGetLoginDataAndroid(url);
                }
                catch (Exception)
                {

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
            }
            base.OnPageFinished(view, url);
        }
    }
}
