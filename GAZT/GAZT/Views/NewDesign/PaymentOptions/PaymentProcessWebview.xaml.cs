using EGAZT.CustomControl;
using EGAZT.Enums;
using EGAZT.ViewModel.NewDesignViewModel;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace EGAZT.Views.NewDesign.PaymentOptions
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentProcessWebview : ContentPage
    {
        PaymnetProcessWebviewViewModel viewModel;
        private double width = 0;
        private double height = 0;
        WebView webView;
        private bool isLoginLoaded = false;

        public PaymentProcessWebview(int type)
        {

            InitializeComponent();
            viewModel = App.Locator.PaymentProcessWebview;

            this.BindingContext = viewModel;
            SetLTR();

            viewModel.PaymentType = type;
            //WebviewGrid.LowerChild(webView);
          
            //NSHttpCookie langCookieTemp = new NSHttpCookie(GAZT.Helper.Constants.LanguageCookieNameForLogin, langVal, "/", GAZT.Helper.Constants.DomainUrlForCookies);
            
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            webView = new WebView();




            var platform = "";

            if (Device.RuntimePlatform == Device.iOS)
            {
                platform = "C4";
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                platform = "C3";
            }

            string paymentUrl = Constants.PaymentUrl + App.PaymentGuid + "&Srcid=" + platform;


            CookieContainer cookieContainer = new CookieContainer();


            try
            {
                foreach (CookieModel cookieModel in App.LoginCookiesRetrieved)
                {
                    Cookie cookie = new Cookie();
                    cookie.Domain = Constants.PartialDomainUrlForCookies;
                    cookie.Comment = cookieModel.Comment;
                    cookie.Version = cookieModel.Version;
                    cookie.HttpOnly = cookieModel.IsHttpOnly;
                    cookie.Path = cookieModel.Path;
                    cookie.Name = cookieModel.CName;
                    cookie.Value = cookieModel.CValue;
                    cookie.Secure = cookieModel.Secure;
                    cookieContainer.Add(cookie);
                }

                
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            viewModel.IsLoading = true;
            Uri uri = new Uri(paymentUrl, UriKind.RelativeOrAbsolute);
            webView.Cookies = cookieContainer;
            webView.Source = new UrlWebViewSource { Url = uri.ToString() };



            //webView.Source = Constants.PaymentUrl + App.PaymentGuid + "&Srcid=" + platform;


            isLoginLoaded = false;


            webView.Navigated += OnNavigated;
            webView.Navigating += OnNavigating;

            WebviewGrid.Children.Add(webView, 0, 0);
            WebviewGrid.LowerChild(webView);

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            viewModel.IsLoading = false;

            WebviewGrid.Children.Remove(webView);
        }

        private void OnNavigated(object sender, WebNavigatedEventArgs e)
        {
           

            if (isLoginLoaded) {


                if (webView != null)
                    WebviewGrid.Children.Remove(webView);

                webView = new WebView();


                var platform = "";

                if (Device.RuntimePlatform == Device.iOS)
                {
                    platform = "C4";
                }
                else if (Device.RuntimePlatform == Device.Android)
                {
                    platform = "C3";
                }

                string paymentUrl = Constants.PaymentUrl + App.PaymentGuid + "&Srcid=" + platform;


                CookieContainer cookieContainer = new CookieContainer();


                try
                {
                    foreach (CookieModel cookieModel in App.LoginCookiesRetrieved)
                    {
                        Cookie cookie = new Cookie();
                        cookie.Domain = Constants.PartialDomainUrlForCookies;
                        cookie.Comment = cookieModel.Comment;
                        cookie.Version = cookieModel.Version;
                        cookie.HttpOnly = cookieModel.IsHttpOnly;
                        cookie.Path = cookieModel.Path;
                        cookie.Name = cookieModel.CName;
                        cookie.Value = cookieModel.CValue;
                        cookie.Secure = cookieModel.Secure;
                        cookieContainer.Add(cookie);
                    }


                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }


                Uri uri = new Uri(paymentUrl, UriKind.RelativeOrAbsolute);
                webView.Cookies = cookieContainer;
                webView.Source = new UrlWebViewSource { Url = uri.ToString() };
                webView.Navigated += OnNavigated;
                webView.Navigating += OnNavigating;

                WebviewGrid.Children.Add(webView, 0, 0);
                WebviewGrid.LowerChild(webView);
                isLoginLoaded = false;

                viewModel.IsLoading = true;
            }
            else {
                viewModel.IsLoading = false;
            }


        }
        protected async void OnNavigating(object sender, WebNavigatingEventArgs e)
        {
            Console.WriteLine("WebViewURL: " + e.Url);

            //viewModel.pushSomething();

            isLoginLoaded = false;

            if (e.Url.Contains("http://bank/?IsPmtSts"))
            {
                var splitString = e.Url.Split('=');
                if (splitString.Length > 0)
                {
                    var responseGUID = splitString[1];
                    //Console.WriteLine("Payment Successful:" + e.Url);

                    webView.IsVisible = false;
                    viewModel.IsLoading = true;



                    await viewModel.UpdateMadaPaymentDetails(responseGUID);

                }

            }
            else if (e.Url.Contains(Constants.DomainUrlForCookies)) {

                isLoginLoaded = true;
                webView.IsVisible = false;
            }

            
        }


        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
        }



    }
}