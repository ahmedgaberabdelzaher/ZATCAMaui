using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using EGAZT;
using EGAZT.Views.SyncFusionEnabledViews.SFLogin;
using Foundation;
using GAZT.iOS.CustomRenderer;
using GAZT.Manager;
using GAZT.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ObjCRuntime;
using UIKit;
using WebKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(HybridWebView), typeof(HybridWebViewRenderer))]
namespace GAZT.iOS.CustomRenderer

{
    public class HybridWebViewRenderer : ViewRenderer<HybridWebView, WKWebView>
    {
        const string JavaScriptFunction = "function invokeCSharpAction(data){window.webkit.messageHandlers.invokeAction.postMessage(data);}";
        WKUserContentController userController;

        WKWebView _wkWebView;
        protected override void OnElementChanged(ElementChangedEventArgs<HybridWebView> e)
        {
            base.OnElementChanged(e);
            

            if (Control == null)
            {
                var config = new WKWebViewConfiguration();
                _wkWebView = new WKWebView(Frame, config);
                _wkWebView.NavigationDelegate = new DisplayLinkWebViewDelegate(Element);

                SetNativeControl(_wkWebView);
            }

            if (e.NewElement != null)
            {
                var tempElement = (HybridWebView)e.NewElement;
                WKHttpCookieStore wKHttpCookieStore = Control.Configuration.WebsiteDataStore.HttpCookieStore;

                string lang = "en";
                string domain = string.Empty;

                if (App.IsArabic == true)
                {
                    lang = "ar";
                }

                tempElement.RefreshCommand = async () =>
                {
                    wKHttpCookieStore = Control.Configuration.WebsiteDataStore.HttpCookieStore;

                    if (App.LoginCookiesRetrieved != null && App.LoginCookiesRetrieved.Count > 0)
                    {
                        foreach (CookieModel cookieModel in App.LoginCookiesRetrieved)
                        {
                            NSHttpCookie cookiesTemp = new NSHttpCookie(cookieModel.CName, cookieModel.CValue, "/", cookieModel.Domain);
                           
                            Task deleteCookie = new Task(() =>
                            {
                                wKHttpCookieStore.DeleteCookie(cookiesTemp, null);
                            });

                            deleteCookie.RunSynchronously();
                        }
                    }

                    Console.WriteLine(Element.Url.ToString());
                    Console.WriteLine(Element.Url.ToString());

                    await Task.Run(() =>
                    {
                        string langVal = "en";

                        if (App.IsArabic == true)
                        {
                            langVal = "ar";
                        }

                        //NSHttpCookie langCookieTemp = new NSHttpCookie(GAZT.Helper.Constants.LanguageCookieNameForLogin, langVal, "/", GAZT.Helper.Constants.DomainUrlForCookies);

                        //wKHttpCookieStore.SetCookie(langCookieTemp, () =>
                        //{
                        //    Control.LoadRequest(new NSUrlRequest(new NSUrl(Element.Url)));
                        //});

                        NSUrlRequest nSUrlRequest = new NSUrlRequest(new NSUrl(Element.Url));
                        NSMutableDictionary cookieDictionary = new NSMutableDictionary();
                        NSString url = (NSString)Element.Url.ToString();
                        cookieDictionary.Add(NSHttpCookie.KeyName, new NSString(GAZT.Helper.Constants.LanguageCookieNameForLogin));
                        cookieDictionary.Add(NSHttpCookie.KeyValue, new NSString(langVal));
                        cookieDictionary.Add(NSHttpCookie.KeyDomain, new NSString(GAZT.Helper.Constants.DomainUrlForCookies));
                        cookieDictionary.Add(NSHttpCookie.KeyPath, new NSString("/"));
                        var myCookie = new NSHttpCookie(cookieDictionary);
                        NSHttpCookieStorage.SharedStorage.AcceptPolicy = NSHttpCookieAcceptPolicy.Always;
                        NSHttpCookieStorage.SharedStorage.SetCookie(myCookie);

                        Control.LoadRequest(nSUrlRequest);
                    });

                    App.ArePreLoginLangCookiesSet = true;

                    //await Task.Run(async () =>
                    //{
                    //    Control.LoadRequest(new NSUrlRequest(new NSUrl(Element.Url)));
                    //    Console.WriteLine(Element.Url.ToString());
                    //});

                    await Task.Run(async () =>
                    {
                        await tempElement.FadeTo(1, 3000);
                    });
                };

                Task.Run(async () =>
                {
                    string langVal = "en";

                    if (App.IsArabic == true)
                    {
                        langVal = "ar";
                    }

                    //NSHttpCookie langCookieTemp = new NSHttpCookie(GAZT.Helper.Constants.LanguageCookieNameForLogin, langVal, "/", GAZT.Helper.Constants.DomainUrlForCookies);
                    //wKHttpCookieStore.SetCookie(langCookieTemp, () =>
                    //{
                    //    Control.LoadRequest(new NSUrlRequest(new NSUrl(Element.Url)));
                    //});

                    NSUrlRequest nSUrlRequest = new NSUrlRequest(new NSUrl(Element.Url));
                    NSMutableDictionary cookieDictionary = new NSMutableDictionary();
                    NSString url = (NSString)Element.Url.ToString();
                    cookieDictionary.Add(NSHttpCookie.KeyName, new NSString(GAZT.Helper.Constants.LanguageCookieNameForLogin));
                    cookieDictionary.Add(NSHttpCookie.KeyValue, new NSString(langVal));
                    cookieDictionary.Add(NSHttpCookie.KeyDomain, new NSString(GAZT.Helper.Constants.DomainUrlForCookies));
                    cookieDictionary.Add(NSHttpCookie.KeyPath, new NSString("/"));

                    var myCookie = new NSHttpCookie(cookieDictionary);
                    NSHttpCookieStorage.SharedStorage.AcceptPolicy = NSHttpCookieAcceptPolicy.Always;
                    NSHttpCookieStorage.SharedStorage.SetCookie(myCookie);

                    Control.LoadRequest(nSUrlRequest);
                });

                App.ArePreLoginLangCookiesSet = true;

                Task.Run(async () =>
                {
                    //await tempElement.FadeTo(1, 3000);
                });

                _wkWebView.NavigationDelegate = new DisplayLinkWebViewDelegate(Element);
                SetNativeControl(_wkWebView);

                //rohith changes
            }
        }
    }

    public class DisplayLinkWebViewDelegate : WKNavigationDelegate
    {
        private HybridWebView element;

        public DisplayLinkWebViewDelegate(HybridWebView element)
        {
            this.element = element;
        }

        private void ClearCookies(WKWebView webView)
        {

        }

        private bool IsError = false;

        public override void DidStartProvisionalNavigation(WKWebView webView, WKNavigation navigation)
        {
            Uri apiUrl = webView.Url;

            if (apiUrl.ToString().Contains(GAZT.Helper.Constants.GAZTSAMLLoginServicePart) && App.ArePreLoginLangCookiesSet == true && App.IsLoginCalled == false)
            {
                //Task task = new Task(()=>{
                //    ClearCookies(webView);
                //});
                //task.RunSynchronously();

                element.InvokeAction("displayLoadingIndicator");
            }

            if(apiUrl.ToString().Contains("IsFGTCK=Y"))
            {
                element.InvokeAction("navigateToForgotUsernamePage");
            }

            if (apiUrl.ToString().Contains(GAZT.Helper.Constants.DomainUrlForCookies))
            {
                App.IsLoginCalled = true;
            }

            //rohith changes
            if (App.IsLoginCalled == true && IsError == false)
            {
                try
                {
                    if (apiUrl.ToString().Contains(GAZT.Helper.Constants.GAZTSAMLLoginServicePart))
                    {
                        element.InvokeAction("displayLoginLoadingIndicator");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                //element.InvokeAction("displayLoadingIndicator");
            }
            //rohith changes

            //base.DidStartProvisionalNavigation(webView, navigation);
        }

        public override void DidFinishNavigation(WKWebView webView, WKNavigation navigation)
        {
            Console.WriteLine("DidFinishNavigation");
            WKHttpCookieStore wKHttpCookieStore = webView.Configuration.WebsiteDataStore.HttpCookieStore;

            Uri tempUrl = webView.Url;

            if (tempUrl.ToString().Contains(GAZT.Helper.Constants.DomainUrlForCookies) && App.IsLoginCalled == false)
            {
                element.InvokeAction("hideLoadingIndicator");
            }

            wKHttpCookieStore.GetAllCookies(async (cookies) =>
            {
                try
                {
                    if (cookies.Length > 0)
                    {
                        Uri url = webView.Url;

                        if (url.ToString().Contains(GAZT.Helper.Constants.GAZTSAMLLoginServicePart) && App.IsLoginCalled == true)
                        {

                            WebClient wc = new WebClient();
                            using (Stream st = wc.OpenRead(url.ToString()))
                            {
                                using (StreamReader sr = new StreamReader(st, Encoding.UTF8))
                                {
                                    string html = sr.ReadToEnd();
                                    Console.Write(html);
                                }
                            }

                            //NSObject htmlData = await webView.EvaluateJavaScriptAsync("document.documentElement.outerHTML.toString()").ConfigureAwait(false);
                            //App.LoginDataRetrieved = new LoginModel();

                            //HtmlDocument document = new HtmlDocument();
                            //document.LoadHtml(htmlData.ToString());

                            //var htmlResponse = document.DocumentNode.InnerText;
                            //var LoginConfirmation = htmlResponse.ToString();

                            //if (!string.IsNullOrEmpty(LoginConfirmation))
                            //{
                            //    LoginConfirmation = JObject.Parse(LoginConfirmation)["d"].ToString();
                            //    App.LoginDataRetrieved = JsonConvert.DeserializeObject<LoginModel>(LoginConfirmation.ToString());
                            //}

                            App.LoginCookiesRetrieved = new List<CookieModel>();

                            foreach (NSHttpCookie cookie in cookies)
                            {
                                CookieModel cookieModel = new CookieModel();
                                cookieModel.CName = cookie.Name;
                                cookieModel.CValue = cookie.Value;
                                cookieModel.Comment = cookie.Comment;
                                cookieModel.IsHttpOnly = cookie.IsHttpOnly;
                                cookieModel.Path = cookie.Comment;
                                cookieModel.Secure = cookie.IsSecure;
                                cookieModel.Comment = cookie.Comment;
                                cookieModel.Version = (int)cookie.Version;
                                cookieModel.Domain = cookie.Domain;

                                App.LoginCookiesRetrieved.Add(cookieModel);
                                Console.WriteLine("FinishNav: Cookie Name: " + cookieModel.CName);
                            }

                            App.LoginDataRetrieved = new LoginModel();
                            App.LoginDataRetrieved = await WebServiceManager.SFGAZTGetLoginData(url.ToString());

                            //rohith changes
                            if (App.LoginDataRetrieved != null && App.LoginDataRetrieved.ResponseStatusMessage == null)
                            {
                                if (App.LoginDataRetrieved.MsgTitle != null && App.LoginDataRetrieved.MsgTitle.Length >= 2)
                                {
                                    IsError = true;
                                    App.IsLoginCalled = false;
                                    App.LoginDataRetrieved.ResponseStatusMessage = "error";
                                    element.InvokeAction("error");
                                }
                                else
                                {
                                    App.LoginDataRetrieved.ResponseStatusMessage = "success";
                                    element.InvokeAction("success");
                                }
                            }
                            else
                            {
                                IsError = true;
                                App.IsLoginCalled = false;
                                App.LoginDataRetrieved.ResponseStatusMessage = "errorGeneric";
                                element.InvokeAction("errorGeneric");
                            }
                            //rohith changes
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });


            //base.DidFinishNavigation(webView, navigation);
        }

        public override void DidFailNavigation(WKWebView webView, WKNavigation navigation, NSError error)
        {
            //base.DidFailNavigation(webView, navigation, error);
        }

        NSMutableArray multiCookieArr = new NSMutableArray();
        NSHttpCookie[] allCookies;

        public override void DecidePolicy(WKWebView webView, WKNavigationResponse navigationResponse, [BlockProxy(typeof(Action))]Action<WKNavigationResponsePolicy> decisionHandler)
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(12, 0))
            {

            }
            else
            {
                NSHttpUrlResponse response = navigationResponse.Response as NSHttpUrlResponse;
                NSHttpCookie[] cookiesAll = NSHttpCookie.CookiesWithResponseHeaderFields(response.AllHeaderFields, response.Url);

                foreach (NSHttpCookie cookie in cookiesAll)
                {
                    NSArray cookieArr = NSArray.FromObjects(cookie.Name, cookie.Value, cookie.Domain, cookie.Path);
                    multiCookieArr.Add(cookieArr);
                }

                Console.WriteLine("cookie is :" + cookiesAll);
            }

            decisionHandler(WKNavigationResponsePolicy.Allow);
        }

    }
}