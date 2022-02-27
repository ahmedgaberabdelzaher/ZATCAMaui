using System;
using System.Collections.Generic;
using System.Net.Http;
using EGAZT;
using Foundation;
using GAZT.iOS.CustomRenderer;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using ObjCRuntime;
using UIKit;
using WebKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(HybridWebView), typeof(HybridCustomWebViewRenderer))]
namespace GAZT.iOS.CustomRenderer
{
    [Preserve(AllMembers = true)]
    public class HybridCustomWebViewRenderer: WkWebViewRenderer
    {
        public HybridCustomWebViewRenderer() : this(new WKWebViewConfiguration())
        {
            if (Device.Idiom == TargetIdiom.Tablet)
            {
                this.CustomUserAgent = "GaztMobileAppUserAgent";
            }
        }

        WKUserContentController userController;
        WKWebView _wkWebView;

        public HybridCustomWebViewRenderer(WKWebViewConfiguration config) : base(config)
        {
            userController = config.UserContentController;
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                userController.RemoveAllUserScripts();
                userController.RemoveScriptMessageHandler("invokeAction");
                HybridWebView hybridWebView = e.OldElement as HybridWebView;
            }

            if (e.NewElement != null)
            {
                HybridWebView hybridWebView = e.NewElement as HybridWebView;
                
                string langVal = "en";

                if (App.IsArabic == true)
                {
                    langVal = "ar";
                }

                NSUrl portalLogin = new NSUrl(hybridWebView.Url);
                NSMutableUrlRequest portalReq = new NSMutableUrlRequest(portalLogin);
                
                NSMutableDictionary dic = new NSMutableDictionary();
                dic.Add(new NSString(GAZT.Helper.Constants.LanguageCookieNameForLogin), new NSString(langVal));
                portalReq.Headers = dic;

                LoadRequest(portalReq);
            }

            App.ArePreLoginLangCookiesSet = true;
            this.NavigationDelegate = new DisplayLinkWebViewDelegateNew((HybridWebView)Element);
        }
    }

    [Preserve(AllMembers = true)]
    public class DisplayLinkWebViewDelegateNew : WKNavigationDelegate
    {
        private HybridWebView element;

        public DisplayLinkWebViewDelegateNew(HybridWebView element)
        {
            this.element = element;
        }
        
        private bool IsError = false;

        public override void ContentProcessDidTerminate(WKWebView webView)
        {
            Console.WriteLine("ContentProcessDidTerminate");
        }

        public override void DidFailProvisionalNavigation(WKWebView webView, WKNavigation navigation, NSError error)
        {
            Console.WriteLine("DidFailProvisionalNavigation");
            try
            {
                if (error != null && error.Code == -1001)
                {
                    //Request timeout error -1001

                    App.LoginDataRetrieved = new LoginModel();
                    IsError = true;
                    App.IsLoginCalled = false;
                    App.LoginDataRetrieved.ResponseStatusMessage = "requestTimedout";
                    element.InvokeAction("requestTimedout");
                }
            }
            catch(Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }

        private static bool isUserLogingApiCalled = false;

        public override void DidStartProvisionalNavigation(WKWebView webView, WKNavigation navigation)
        {
            isUserLogingApiCalled = false;
            Uri apiUrl = webView.Url;

            if (apiUrl.ToString().Contains(GAZT.Helper.Constants.GAZTSAMLLoginServicePart) && App.ArePreLoginLangCookiesSet == true && App.IsLoginCalled == false)
            {
                element.InvokeAction("displayLoadingIndicator");
                Clear();
            }

            if (apiUrl.ToString().Contains("IsFGTCK=Y"))
            {
                element.InvokeAction("navigateToForgotUsernamePage");
            }

            if (apiUrl.ToString().Contains("IsRstPw=Y"))
            {
                element.InvokeAction("navigateToUnlockAccountPage");
            }

            if (apiUrl.ToString().Contains("IsBacktoLogin=Y"))
            {
                element.InvokeAction("navigateBackToLoginPage");
            }

            if (apiUrl.ToString().Contains("IsSIGNUP=Y"))
            {
                element.InvokeAction("navigateToVATIndividualSignupPage");
            }
            if (apiUrl.ToString().Contains(GAZT.Helper.Constants.DomainUrlForCookies))
            {
                App.IsLoginCalled = true;
            }

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
                            if(isUserLogingApiCalled == false)
                            {
                                isUserLogingApiCalled = true;

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

                                try
                                {
                                    App.httpClientHandler = new HttpClientHandler();
                                    App.httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                                }
                                catch (Exception ex)
                                {
                                    Console.Write(ex.ToString());
                                    Console.Write(ex.StackTrace.ToString());
                                }

                                App.LoginDataRetrieved = new LoginModel();
                                App.LoginDataRetrieved = await WebServiceManager.SFGAZTGetLoginData(url.ToString());

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
                            }
                            else
                            {

                            }
                        }
                    }
                }
                catch (GAZTInvalidDataException ex)
                {
                    Console.Write(ex.ToString());
                    Console.Write(ex.StackTrace.ToString());
                    IsError = true;
                    App.IsLoginCalled = false;
                    App.LoginDataRetrieved.ResponseStatusMessage = "error";
                    element.InvokeAction("error");
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    Console.Write(ex.StackTrace.ToString());
                    IsError = true;
                    App.IsLoginCalled = false;
                    App.LoginDataRetrieved.ResponseStatusMessage = "error";
                    element.InvokeAction("error");
                }
            });
        }

        public override void DidFailNavigation(WKWebView webView, WKNavigation navigation, NSError error)
        {
            Console.WriteLine("DidFailNavigation");
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

        public void Clear()
        {
            NSHttpCookieStorage.SharedStorage.RemoveCookiesSinceDate(NSDate.DistantPast);

            WKWebsiteDataStore.DefaultDataStore.FetchDataRecordsOfTypes(WKWebsiteDataStore.AllWebsiteDataTypes, (NSArray records) => {

                for (nuint i = 0; i < records.Count; i++)
                {
                    var record = records.GetItem<WKWebsiteDataRecord>(i);
                    WKWebsiteDataRecord[] recordArray = new WKWebsiteDataRecord[record.DataTypes.Count];
                    WKWebsiteDataStore.DefaultDataStore.RemoveDataOfTypes(record.DataTypes, NSDate.DistantPast, () => { });
                }

            });

        }

    }



}
