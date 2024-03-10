using EGAZT.CustomControl;
using EGAZT.Enums;
using EGAZT.ViewModel.NewDesignViewModel;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
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
using System.Web;
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
        private bool isPaymentProcessed = false;

        public PaymentProcessWebview(int type)
        {

            InitializeComponent();
            viewModel = App.Locator.PaymentProcessWebview;

            this.BindingContext = viewModel;
            SetLTR();
            ChangeAeroIcon();
            viewModel.PaymentType = type;
            //WebviewGrid.LowerChild(webView);
          
            //NSHttpCookie langCookieTemp = new NSHttpCookie(GAZT.Helper.Constants.LanguageCookieNameForLogin, langVal, "/", GAZT.Helper.Constants.DomainUrlForCookies);
            
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            isPaymentProcessed = false;
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

            var PaymentSAPClient = "300";
            if (Constants.PaymentUrl.Contains(Constants.DevBaseUrlForODataServices)) {

                PaymentSAPClient = Constants.DevPaymentSapClinet;
            }
            else if (Constants.PaymentUrl.Contains(Constants.QABaseUrlForODataServices))
            {
                PaymentSAPClient = Constants.QAPaymentSapClinet;

            }
            else if (Constants.PaymentUrl.Contains(Constants.PreProdBaseUrlForODataServices))
            {
                PaymentSAPClient = Constants.PreProdPaymentSapClinet;

            }
            else if (Constants.PaymentUrl.Contains(Constants.ProdBaseUrlForODataServices))
            {
                PaymentSAPClient = Constants.ProdPaymentSapClinet;

            }


            string paymentUrl = Constants.PaymentUrl + App.PaymentGuid + "&Srcid=" + platform + "&sap-ui-language=" + UtilityManager.GetLanguageParameter() + "&sap-client=" + PaymentSAPClient;


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

            catch (Exception)
            {
                
                
                
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

        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {

                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
            }
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


                string paymentUrl = Constants.PaymentUrl + App.PaymentGuid + "&Srcid=" + platform + "&sap-ui-language=" + UtilityManager.GetLanguageParameter();


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

                catch (Exception)
                {
                    
                    
                    
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
           

            isLoginLoaded = false;

            if (e.Url.Contains("IsPmtSts"))
            {
                var splitString = e.Url.Split('=');
                if (splitString.Length > 0)
                {

                    var responseGUID = splitString[1];
                    //("Payment Successful:" + e.Url);

                    webView.IsVisible = false;
                    viewModel.IsLoading = true;


                    if (!isPaymentProcessed) {

                        isPaymentProcessed = true;

                        await viewModel.UpdateMadaPaymentDetails(responseGUID);


                    }



                }

            }
            else if ((e.Url.Contains("isAuthErr")) || (e.Url.Contains("IsPROCBlank")))
            {
                var splitString = e.Url.Split('&');
                if (splitString.Length > 0)
                {

                    isPaymentProcessed = false;

                    var responseMessage = splitString[1].Replace("msg='", "");
                    string decodedMessage = Uri.UnescapeDataString(responseMessage);
                    string[] parts = decodedMessage.Split('\'');
                    //("Payment Successful:" + e.Url);

                    webView.IsVisible = false;
                    viewModel.IsLoading = true;

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(parts[1]));
                        //await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        viewModel._navigationService.GoBack();
                    });

                    //await viewModel.UpdateMadaPaymentDetails(responseGUID);

                }

            }
            else if (e.Url.Contains(Constants.DomainUrlForCookies) && (!(e.Url.Contains("madapmnt.Madaconfirm"))))
            {
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