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

        public PaymentProcessWebview(int type)
        {

            InitializeComponent();
            viewModel = App.Locator.PaymentProcessWebview;

            this.BindingContext = viewModel;
            SetLTR();

            viewModel.paymentType = type;
            //WebviewGrid.LowerChild(webView);



        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
        
            webView = new WebView();

            CookieContainer cookieContainer = new CookieContainer();

            try
            {
                foreach (CookieModel cookieModel in App.LoginCookiesRetrieved)
                {
                    Cookie cookie = new Cookie();

                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        if (cookieModel.Domain.StartsWith(".") == false)
                        {
                            cookie.Domain = "." + cookieModel.Domain;
                        }
                        else
                        {
                            cookie.Domain = cookieModel.Domain;
                        }
                    }
                    else if (Device.RuntimePlatform == Device.Android)
                    {
                        cookie.Domain = Constants.PartialDomainUrlForCookies;
                    }

                    cookie.Comment = cookieModel.Comment;
                    cookie.Version = cookieModel.Version;
                    cookie.HttpOnly = cookieModel.IsHttpOnly;
                    cookie.Path = cookieModel.Path;
                    cookie.Name = cookieModel.CName;
                    cookie.Value = cookieModel.CValue;
                    cookie.Secure = cookieModel.Secure;
                    cookieContainer.Add(cookie);
                }

                App.httpClientHandler.CookieContainer = cookieContainer;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



            var platform = "";

            if (Device.RuntimePlatform == Device.iOS)
            {
                platform = "C4";
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                platform = "C3";
            }



            webView.Source = Constants.PaymentUrl + App.PaymentGuid + "&Srcid=" + platform;

            webView.Cookies = App.httpClientHandler.CookieContainer;
            webView.Navigated += OnNavigated;
            webView.Navigating += OnNavigating;

            WebviewGrid.Children.Add(webView, 0, 0);
            WebviewGrid.LowerChild(webView);
            //NSHttpCookie langCookieTemp = new NSHttpCookie(GAZT.Helper.Constants.LanguageCookieNameForLogin, langVal, "/", GAZT.Helper.Constants.DomainUrlForCookies);


        }

        private void OnNavigated(object sender, WebNavigatedEventArgs e)
        {
            viewModel.IsLoading = false;
        }
        protected async void OnNavigating(object sender, WebNavigatingEventArgs e)
        {
            Console.WriteLine("WebViewURL: " + e.Url);

            //viewModel.pushSomething();

            if (e.Url.Contains("http://bank/?IsPmtSts"))
            {
                var splitString = e.Url.Split('=');
                if (splitString.Length > 0)
                {
                    var responseGUID = splitString[1];
                    Console.WriteLine("Payment Successful:" + e.Url);

                    //webView.IsVisible = false;
                    viewModel.IsLoading = true;

                    await viewModel.UpdateMadaPaymentDetails(responseGUID);


                  

                }

            }
            
            viewModel.IsLoading = false;
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