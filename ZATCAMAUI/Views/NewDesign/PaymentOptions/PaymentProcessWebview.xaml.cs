using Mopups.Services;
using System.Net;
using System.Web;
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
        WebView webView;
        private bool isLoginLoaded = false;
        private bool isPaymentProcessed = false;

        public PaymentProcessWebview(int type)
        {
            InitializeComponent();
            viewModel = App.Locator.PaymentProcessWebview;

            this.BindingContext = viewModel;
            viewModel.PaymentType = type;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            isPaymentProcessed = false;
            webView = new WebView();
            viewModel.IsLoading = true;
            webView.Source = new HtmlWebViewSource { Html = HttpUtility.HtmlDecode(App.securityAuthorizationKey) };

            isLoginLoaded = false;
            webView.Navigated += OnNavigated;
            webView.Navigating += OnNavigating;


            WebviewGrid.Add(webView, 0, 0);

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            viewModel.IsLoading = false;

            WebviewGrid.Children.Remove(webView);
        }

        private void OnNavigated(object sender, WebNavigatedEventArgs e)
        {
            if (isLoginLoaded)
            {

                if (webView != null)
                    WebviewGrid.Children.Remove(webView);

                webView = new WebView();

                webView.Source = new HtmlWebViewSource { Html = HttpUtility.HtmlDecode(App.securityAuthorizationKey) };
                webView.Navigated += OnNavigated;
                webView.Navigating += OnNavigating;

                WebviewGrid.Add(webView, 0, 0);
                WebviewGrid.Insert(WebviewGrid.Count, webView);
                isLoginLoaded = false;

                viewModel.IsLoading = true;
            }
            else
            {
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

                    webView.IsVisible = false;
                    viewModel.IsLoading = true;

                    if (!isPaymentProcessed)
                    {
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

                    webView.IsVisible = false;
                    viewModel.IsLoading = true;

                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(parts[1]));
                        viewModel._navigationService.GoBack();
                    });
                }
            }
            else if (e.Url.Contains(ZATCAConstants.DevDomainForCookies) && (!(e.Url.Contains("madapmnt.Madaconfirm"))))
            {
                isLoginLoaded = true;
                webView.IsVisible = false;
            }
        }

    }
}