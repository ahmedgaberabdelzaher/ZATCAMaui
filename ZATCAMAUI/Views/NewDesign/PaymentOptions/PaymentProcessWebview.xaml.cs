using RGPopup.Maui.Services;
using System.Net;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel.PaymnetOptions;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

namespace ZATCAMAUI.Views.NewDesign.PaymentOptions
{
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

            BindingContext = viewModel;
            ChangeAeroIcon();
            viewModel.PaymentType = type;
        }


        protected override void OnAppearing()
        {
            try
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
                if (ZATCAConstants.PaymentUrl.Contains(ZATCAConstants.DevBaseUrlForODataServices))
                {

                    PaymentSAPClient = ZATCAConstants.DevPaymentSapClinet;
                }
                else if (ZATCAConstants.PaymentUrl.Contains(ZATCAConstants.QABaseUrlForODataServices))
                {
                    PaymentSAPClient = ZATCAConstants.QAPaymentSapClinet;

                }
                else if (ZATCAConstants.PaymentUrl.Contains(ZATCAConstants.PreProdBaseUrlForODataServices))
                {
                    PaymentSAPClient = ZATCAConstants.PreProdPaymentSapClinet;

                }
                else if (ZATCAConstants.PaymentUrl.Contains(ZATCAConstants.ProdBaseUrlForODataServices))
                {
                    PaymentSAPClient = ZATCAConstants.ProdPaymentSapClinet;

                }


                string paymentUrl = ZATCAConstants.PaymentUrl + App.PaymentGuid + "&Srcid=" + platform + "&sap-ui-language=" + UtilityManager.GetLanguageParameter() + "&sap-client=" + PaymentSAPClient;


                CookieContainer cookieContainer = new CookieContainer();

                foreach (CookieModel cookieModel in App.LoginCookiesRetrieved)
                {
                    Cookie cookie = new Cookie();
                    cookie.Domain = ZATCAConstants.PartialDomainUrlForCookies;
                    cookie.Comment = cookieModel.Comment;
                    cookie.Version = cookieModel.Version;
                    cookie.HttpOnly = cookieModel.IsHttpOnly;
                    cookie.Path = cookieModel.Path;
                    cookie.Name = cookieModel.CName;
                    cookie.Value = cookieModel.CValue;
                    cookie.Secure = cookieModel.Secure;
                    cookieContainer.Add(cookie);
                }

                viewModel.IsLoading = true;
                Uri uri = new Uri(paymentUrl, UriKind.RelativeOrAbsolute);
                webView.Cookies = cookieContainer;
                webView.Source = new UrlWebViewSource { Url = uri.ToString() };



                //webView.Source = Constants.PaymentUrl + App.PaymentGuid + "&Srcid=" + platform;


                isLoginLoaded = false;


                webView.Navigated += OnNavigated;
                webView.Navigating += OnNavigating;

                WebviewGrid.Add(webView, 0, 0);
                //TODO
                WebviewGrid.Insert(WebviewGrid.Count, webView);

            }
            catch (Exception)
            {

            }

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

            try
            {
                if (isLoginLoaded)
                {


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


                    string paymentUrl = ZATCAConstants.PaymentUrl + App.PaymentGuid + "&Srcid=" + platform + "&sap-ui-language=" + UtilityManager.GetLanguageParameter();


                    CookieContainer cookieContainer = new CookieContainer();

                    foreach (CookieModel cookieModel in App.LoginCookiesRetrieved)
                    {
                        Cookie cookie = new Cookie();
                        cookie.Domain = ZATCAConstants.PartialDomainUrlForCookies;
                        cookie.Comment = cookieModel.Comment;
                        cookie.Version = cookieModel.Version;
                        cookie.HttpOnly = cookieModel.IsHttpOnly;
                        cookie.Path = cookieModel.Path;
                        cookie.Name = cookieModel.CName;
                        cookie.Value = cookieModel.CValue;
                        cookie.Secure = cookieModel.Secure;
                        cookieContainer.Add(cookie);
                    }


                    Uri uri = new Uri(paymentUrl, UriKind.RelativeOrAbsolute);
                    webView.Cookies = cookieContainer;
                    webView.Source = new UrlWebViewSource { Url = uri.ToString() };
                    webView.Navigated += OnNavigated;
                    webView.Navigating += OnNavigating;

                    WebviewGrid.Add(webView, 0, 0);
                    //TODO
                    WebviewGrid.Insert(WebviewGrid.Count,webView);
                    isLoginLoaded = false;

                    viewModel.IsLoading = true;
                }
                else
                {
                    viewModel.IsLoading = false;
                }

            }
            catch (Exception )
            {

            }


        }
        protected async void OnNavigating(object sender, WebNavigatingEventArgs e)
        {


            isLoginLoaded = false;

            if (e.Url.Contains("bank/?IsPmtSts"))
            {
                var splitString = e.Url.Split('=');
                if (splitString.Length > 0)
                {

                    var responseGUID = splitString[1];
                    //("Payment Successful:" + e.Url);

                    webView.IsVisible = false;
                    viewModel.IsLoading = true;


                    if (!isPaymentProcessed)
                    {

                        isPaymentProcessed = true;

                        await viewModel.UpdateMadaPaymentDetails(responseGUID);


                    }



                }

            }
            else if (e.Url.Contains("error/?isAuthErr"))
            {
                var splitString = e.Url.Split('&');
                if (splitString.Length > 0)
                {

                    isPaymentProcessed = false;

                    var responseMessage = splitString[1].Replace("msg", "");

                    //("Payment Successful:" + e.Url);

                    webView.IsVisible = false;
                    viewModel.IsLoading = true;

                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(responseMessage));
                        //await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        viewModel._navigationService.GoBack();
                    });

                    //await viewModel.UpdateMadaPaymentDetails(responseGUID);

                }

            }
            else if (e.Url.Contains(ZATCAConstants.DomainUrlForCookies))
            {

                isLoginLoaded = true;
                webView.IsVisible = false;
            }


        }

    }
}