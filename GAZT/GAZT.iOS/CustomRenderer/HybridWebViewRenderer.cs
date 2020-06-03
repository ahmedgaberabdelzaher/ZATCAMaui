using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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
using GAZTeServicesBusinessLibrary.GAZTExceptions;
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
                //WKWebViewConfiguration* config = [[WKWebViewConfiguration alloc] init];

                var source = "var meta = document.createElement('meta');" +
                 "meta.name = 'viewport';" +
                 "meta.content = 'width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no';" +
                 "var head = document.getElementsByTagName('head')[0];" + "head.appendChild(meta);";

                var script = new WKUserScript(new NSString(source), WKUserScriptInjectionTime.AtDocumentEnd, true);
                _wkWebView.Configuration.UserContentController.AddUserScript(script);

                SetNativeControl(_wkWebView);
            }

            if (e.NewElement != null)
            {
                var tempElement = (HybridWebView)e.NewElement;
                WKHttpCookieStore wKHttpCookieStore = Control.Configuration.WebsiteDataStore.HttpCookieStore;

                /*WKWebsiteDataStore.default().fetchDataRecords(ofTypes: WKWebsiteDataStore.allWebsiteDataTypes()) { records in
                records.forEach { record in
                    WKWebsiteDataStore.default().removeData(ofTypes: record.dataTypes, for: [record], completionHandler: {})
                    print("[WebCacheCleaner] Record \(record) deleted")
                }
                    }
                 *
                 */

                //wKHttpCookieStore.GetAllCookies(async (cookies) =>
                //{
                //    if (cookies.Length > 0)
                //    {
                //     Console.WriteLine(cookies);
                //    }
                //});

                string lang = "en";
                string domain = string.Empty;

                if (App.IsArabic == true)
                {
                    lang = "ar";
                }

                tempElement.RefreshCommand = async () =>
                {
                    wKHttpCookieStore = Control.Configuration.WebsiteDataStore.HttpCookieStore;
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
                        //    wKHttpCookieStore.GetAllCookies(async (cookies) =>
                        //    {

                        //        if (cookies.Length > 0)
                        //        {
                        //            Console.WriteLine(cookies);
                        //        }
                        //    });

                        //});

                        NSUrl portalLogin = new NSUrl(Element.Url);
                        NSMutableUrlRequest portalReq = new NSMutableUrlRequest(portalLogin);

                        NSMutableDictionary dic = new NSMutableDictionary();
                        dic.Add(new NSString(GAZT.Helper.Constants.LanguageCookieNameForLogin), new NSString(langVal));
                        portalReq.Headers = dic;

                        Control.LoadRequest(portalReq);

                        //NSUrlRequest nSUrlRequest = new NSUrlRequest(new NSUrl(Element.Url));
                        //NSMutableDictionary cookieDictionary = new NSMutableDictionary();
                        //NSString url = (NSString)Element.Url.ToString();
                        //cookieDictionary.Add(NSHttpCookie.KeyName, new NSString(GAZT.Helper.Constants.LanguageCookieNameForLogin));
                        //cookieDictionary.Add(NSHttpCookie.KeyValue, new NSString(langVal));
                        //cookieDictionary.Add(NSHttpCookie.KeyDomain, new NSString(GAZT.Helper.Constants.DomainUrlForCookies));
                        //cookieDictionary.Add(NSHttpCookie.KeyPath, new NSString("/"));
                        //var myCookie = new NSHttpCookie(cookieDictionary);
                        //NSHttpCookieStorage.SharedStorage.AcceptPolicy = NSHttpCookieAcceptPolicy.Always;
                        //NSHttpCookieStorage.SharedStorage.SetCookie(myCookie);

                        //Control.LoadRequest(nSUrlRequest);
                    });

                    App.ArePreLoginLangCookiesSet = true;

                    //await Task.Run(async () =>
                    //{
                    //    Control.LoadRequest(new NSUrlRequest(new NSUrl(Element.Url)));
                    //    Console.WriteLine(Element.Url.ToString());
                    //});

                    //await Task.Run(async () =>
                    //{
                    //    await tempElement.FadeTo(1, 3000);
                    //});
                };

                //NSArray allCache = new NSArray();

                //Task task2 = new Task(() =>
                //{
                //    for (uint i = 0; i < allCache.Count; i++)
                //    {
                //        WKWebsiteDataRecord wKWebsiteDataRecord = allCache.GetItem<WKWebsiteDataRecord>(i);

                //        WKWebsiteDataRecord[] currRecord = new WKWebsiteDataRecord[5];
                //        currRecord.Append(wKWebsiteDataRecord);

                //        WKWebsiteDataStore.DefaultDataStore.RemoveDataOfTypes(wKWebsiteDataRecord.DataTypes, currRecord, null);
                //    }
                //});

                //Task.Run(async () =>
                //{
                //    WKWebsiteDataStore.DefaultDataStore.FetchDataRecordsOfTypes(WKWebsiteDataStore.AllWebsiteDataTypes, (NSArray allCookies) =>
                //    {

                //        for (uint i = 0; i < allCookies.Count; i++)
                //        {
                //            WKWebsiteDataRecord wKWebsiteDataRecord = allCookies.GetItem<WKWebsiteDataRecord>(i);

                //            WKWebsiteDataRecord[] currRecord = new WKWebsiteDataRecord[5];
                //            currRecord.Append(wKWebsiteDataRecord);
                //        }

                //        allCache = allCookies;

                //        Console.WriteLine(allCookies);
                //        task2.Start();
                //    });
                //});


                /*
                 * let websiteDataTypes = NSSet(array: [WKWebsiteDataTypeDiskCache, WKWebsiteDataTypeMemoryCache])
                    let date = Date(timeIntervalSince1970: 0)
                    WKWebsiteDataStore.default().removeData(ofTypes: websiteDataTypes as! Set<String>, modifiedSince: date, completionHandler:{ })
                 */

                //NSMutableArray nSArray = new NSMutableArray();
                //nSArray.Add(WKWebsiteDataType.DiskCache);
                //nSArray.Add(WKWebsiteDataType.MemoryCache);

                //NSString[] nSString = new NSString[2];
                //nSString.Append(WKWebsiteDataType.DiskCache);
                //nSString.Append(WKWebsiteDataType.MemoryCache);

                //NSArray nSStrings = NSArray.FromObjects(WKWebsiteDataType.DiskCache, WKWebsiteDataType.MemoryCache);

                //NSDate date = NSDate.FromTimeIntervalSince1970(0);
                //NSSet<NSString> set = WKWebsiteDataStore.AllWebsiteDataTypes;

                //NSHttpCookieStorage CookieStorage = NSHttpCookieStorage.SharedStorage;
                //CookieStorage.RemoveCookiesSinceDate(date);
                //NSUserDefaults.StandardUserDefaults.Synchronize();

                //NSHttpCookie[] allcCookies = new NSHttpCookie[100];

                //Task task3 = new Task(() =>
                //{
                //    string langVal = "en";

                //    if (App.IsArabic == true)
                //    {
                //        langVal = "ar";
                //    }

                //    NSUrl portalLogin = new NSUrl(Element.Url);
                //    NSMutableUrlRequest portalReq = new NSMutableUrlRequest(portalLogin);

                //    NSMutableDictionary dic = new NSMutableDictionary();
                //    dic.Add(new NSString(GAZT.Helper.Constants.LanguageCookieNameForLogin), new NSString(langVal));
                //    portalReq.Headers = dic;
                //    Control.LoadRequest(portalReq);
                //});

                //Task task1 = new Task(() =>
                //{
                //    foreach (NSHttpCookie nSHttpCookie in allcCookies)
                //    {
                //        WKWebsiteDataStore.DefaultDataStore.HttpCookieStore.DeleteCookie(nSHttpCookie, () =>
                //        {
                //            Console.WriteLine("Deleted");
                //        });
                //    }

                //    task3.RunSynchronously();
                //});

                //Task task2 = new Task(() =>
                //{
                //    WKWebsiteDataStore.DefaultDataStore.HttpCookieStore.GetAllCookies(async (cookies) =>
                //    {
                //        try
                //        {
                //            if (cookies.Length > 0)
                //            {
                //                allcCookies = cookies;
                //                task1.RunSynchronously();
                //            }
                //        }
                //        catch (Exception ex)
                //        {

                //        }
                //    });
                //});

                //task2.RunSynchronously();

                //WKWebsiteDataStore.DefaultDataStore.RemoveDataOfTypes(new NSSet<NSString>(nSString), date, null);
                //try
                //{
                //    WKWebsiteDataStore.DefaultDataStore.RemoveDataOfTypes(set, date, () =>
                //    {


                //    });
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex.Message);
                //}

                Task.Run(async () =>
                {
                    string langVal = "en";

                    if (App.IsArabic == true)
                    {
                        langVal = "ar";
                    }

                    //NSString encodedUrl = (NSString)Element.Url;
                    //encodedUrl = encodedUrl.CreateStringByAddingPercentEncoding(null);

                    NSUrl portalLogin = new NSUrl(Element.Url);
                    NSMutableUrlRequest portalReq = new NSMutableUrlRequest(portalLogin);

                    NSMutableDictionary dic = new NSMutableDictionary();
                    dic.Add(new NSString(GAZT.Helper.Constants.LanguageCookieNameForLogin), new NSString(langVal));
                    portalReq.Headers = dic;
                    Control.LoadRequest(portalReq);
                });

                App.ArePreLoginLangCookiesSet = true;

                //Task.Run(async () =>
                //{
                //    //await tempElement.FadeTo(1, 3000);
                //});

                _wkWebView.NavigationDelegate = new DisplayLinkWebViewDelegate(Element);
                SetNativeControl(_wkWebView);
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

                            //WebClient wc = new WebClient();
                            //using (Stream st = wc.OpenRead(url.ToString()))
                            //{
                            //    using (StreamReader sr = new StreamReader(st, Encoding.UTF8))
                            //    {
                            //        string html = sr.ReadToEnd();
                            //        Console.Write(html);
                            //    }
                            //}

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

                            //Task task1 = new Task(() =>
                            //{
                            //    foreach (NSHttpCookie nSHttpCookie in allCookies)
                            //    {
                            //        WKWebsiteDataStore.DefaultDataStore.HttpCookieStore.DeleteCookie(nSHttpCookie, () =>
                            //        {
                            //            Console.WriteLine("Deleted");
                            //        });
                            //    }
                            //});

                            //Task task2 = new Task(() =>
                            //{
                            //    WKWebsiteDataStore.DefaultDataStore.HttpCookieStore.GetAllCookies(async (cookiesTemp) =>
                            //    {
                            //        try
                            //        {
                            //            if (cookiesTemp.Length > 0)
                            //            {
                            //                allCookies = cookiesTemp;
                            //                task1.RunSynchronously();
                            //            }
                            //        }
                            //        catch (Exception ex)
                            //        {

                            //        }
                            //    });
                            //});

                            //task2.RunSynchronously();

                            //NSDate date = NSDate.FromTimeIntervalSince1970(0);
                            //NSHttpCookieStorage CookieStorage = NSHttpCookieStorage.SharedStorage;
                            //CookieStorage.RemoveCookiesSinceDate(date);
                            //NSUserDefaults.StandardUserDefaults.Synchronize();

                            //NSSet<NSString> set = WKWebsiteDataStore.AllWebsiteDataTypes;
                            //try
                            //{
                            //    WKWebsiteDataStore.DefaultDataStore.RemoveDataOfTypes(set, date, () =>
                            //    {
                            //        Console.WriteLine(set);
                            //    });
                            //}
                            //catch (Exception ex)
                            //{
                            //    Console.WriteLine(ex.Message);
                            //}

                            try
                            {
                                App.httpClientHandler = new HttpClientHandler();
                                App.httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                            }
                            catch(Exception ex)
                            {

                            }

                            //NSUrlSession.SharedSession.Reset(()=> {
                            //    Console.WriteLine("NSUrlSession.SharedSession.Reset");
                            //});

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
                    }
                }
                catch (GAZTInvalidDataException ex)
                {
                    IsError = true;
                    App.IsLoginCalled = false;
                    App.LoginDataRetrieved.ResponseStatusMessage = "error";
                    element.InvokeAction("error");
                }
                catch (Exception ex)
                {
                    IsError = true;
                    App.IsLoginCalled = false;
                    App.LoginDataRetrieved.ResponseStatusMessage = "error";
                    element.InvokeAction("error");
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