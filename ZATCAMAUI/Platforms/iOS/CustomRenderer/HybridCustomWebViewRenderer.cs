using ObjCRuntime;
using UIKit;
using WebKit;
using ZATCAMAUI.Core.CustomControls;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using Microsoft.Maui.Controls.Platform;

using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Models;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Core.Exceptions;
using Foundation;

namespace ZATCAMAUI.Platforms.iOS.CustomRenderer
{
    public class HybridCustomWebViewRenderer : WkWebViewRenderer
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
            try
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
                dic.Add(new NSString(ZATCAConstants.LanguageCookieNameForLogin), new NSString(langVal));
                portalReq.Headers = dic;

                LoadRequest(portalReq);
            }

            App.ArePreLoginLangCookiesSet = true;
            this.NavigationDelegate = new DisplayLinkWebViewDelegateNew((HybridWebView)Element);
            }
            catch (Exception)
            {

            }
        }


    }

    public class DisplayLinkWebViewDelegateNew : WKNavigationDelegate
    {
        private HybridWebView element;

        public DisplayLinkWebViewDelegateNew(HybridWebView element)
        {
            this.element = element;
            //  this.element.Navigation = this;
        }

        private bool IsError = false;

        public override void ContentProcessDidTerminate(WKWebView webView)
        {

        }

        [Foundation.Export("webView:didFailProvisionalNavigation:withError:")]
        public override void DidFailProvisionalNavigation(WKWebView webView, WKNavigation navigation, NSError error)
        {
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
                else
                {
                    App.IsLoginCalled = false;
                    element.InvokeAction("requestTimedout");
                }
            }
            catch (Exception)
            {


            }
        }

        private static bool isUserLogingApiCalled = false;

        public override void DidStartProvisionalNavigation(WKWebView webView, WKNavigation navigation)
        {
            try
            {

           

            isUserLogingApiCalled = false;
            Uri apiUrl = webView.Url;

            //speradsso.eradsso

            if (apiUrl.ToString().Contains(ZATCAConstants.GAZTSAMLLoginServicePart) && App.ArePreLoginLangCookiesSet == true && App.IsLoginCalled == false)
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

            if (apiUrl.ToString().Contains("IsSSOLogon=Y"))
            {
                element.InvokeAction("IsloginControl");
            }
            if (apiUrl.ToString().Contains("IsSignUp=Y&guid="))
            {
                App.GUIDFrSSO = apiUrl.ToString();
                element.InvokeAction("navigateToVATIndividualSignupPageSSO");
            }

            if (apiUrl.ToString().Contains("IsSIGNUP=Y"))
            {
                element.InvokeAction("navigateToVATIndividualSignupPage");
            }
            if (apiUrl.ToString().Contains(ZATCAConstants.WebKeyChangeMobCompanay))
            {
                element.InvokeAction(ZATCAConstants.AppChangeMobCompanay);
            }
            if (apiUrl.ToString().Contains(ZATCAConstants.WebKeyChangeMobCompanayNafath))
            {
                string guid = apiUrl.ToString();
                App.GUIDFrChangeMob = guid;
                element.InvokeAction(ZATCAConstants.AppChangeMobCompanayNafath);
            }
            if (apiUrl.ToString().Contains(ZATCAConstants.DevDomainForCookies))
            {
                App.IsLoginCalled = true;
            }

            if (App.IsLoginCalled == true && IsError == false)
            {
                try
                {
                    if (apiUrl.ToString().Contains(ZATCAConstants.GAZTSAMLLoginServicePart))
                    {
                        element.InvokeAction("displayLoginLoadingIndicator");
                    }
                }
                catch (Exception )
                {



                }
            }
            }
            catch (Exception)
            {

            }
        }

        public override void DidFinishNavigation(WKWebView webView, WKNavigation navigation)
        {
            try
            {

            
            WKHttpCookieStore wKHttpCookieStore = webView.Configuration.WebsiteDataStore.HttpCookieStore;

            Uri tempUrl = webView.Url;


            if (tempUrl.ToString().Contains(ZATCAConstants.DevDomainForCookies) && App.IsLoginCalled == false)
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

                        if (url.ToString().Contains(ZATCAConstants.GAZTSAMLLoginServicePart) && App.IsLoginCalled == true)
                        {
                            if (isUserLogingApiCalled == false)
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

                                }

                                try
                                {
                                    App.httpClientHandler = new HttpClientHandler();
                                    App.httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                                }
                                catch (Exception)
                                {



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


                    IsError = true;
                    App.IsLoginCalled = false;
                    App.LoginDataRetrieved.ResponseStatusMessage = "error";
                    element.InvokeAction("error");
                }
                catch (Exception)
                {
                    IsError = true;
                    App.IsLoginCalled = false;
                    App.LoginDataRetrieved.ResponseStatusMessage = "error";
                    element.InvokeAction("error");
                }
            });
            }
            catch (Exception)
            {

            }
        }

        public override void DidFailNavigation(WKWebView webView, WKNavigation navigation, NSError error)
        {

        }

        NSMutableArray multiCookieArr = new NSMutableArray();
        NSHttpCookie[] allCookies;

        public override void DecidePolicy(WKWebView webView, WKNavigationResponse navigationResponse, [BlockProxy(typeof(Action))] Action<WKNavigationResponsePolicy> decisionHandler)
        {
            try
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


                }

                decisionHandler(WKNavigationResponsePolicy.Allow);
            }
            catch (Exception)
            {

            }
            
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
