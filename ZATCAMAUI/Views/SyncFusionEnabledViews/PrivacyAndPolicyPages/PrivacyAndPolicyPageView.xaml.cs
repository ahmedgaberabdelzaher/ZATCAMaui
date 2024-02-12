

using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.PrivacyAndPolicyPage;
using ZATCAMAUI.Views.NewDesign.TaxpayerCorrespondancePages;
using Application = Microsoft.Maui.Controls.Application;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.PrivacyAndPolicyPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrivacyAndPolicyPageView : ContentPage
    {
        #region Variable
        PrivacyAndPolicyPageViewModel viewModel;
        #endregion

        #region Constructor
        public PrivacyAndPolicyPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.PrivacyAndPolicyPageView;
            On<iOS>().SetUseSafeArea(true);
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            Padding = safeInsets;
            ChangeAeroIcon();
            SetLTR();
            BindingContext = viewModel;
            if (Device.RuntimePlatform == Device.iOS)
            {
                string baseUrl = DependencyService.Get<IBaseUrl>().Get();
                string path = DependencyService.Get<IBaseUrl>().Get();
                if (!App.IsArabic)
                {
                    string url = Path.Combine(path, "PrivacyPolicy_EN.html");
                    viewModel.WebUrl = url;
                }
                else
                {
                    string url = Path.Combine(path, "PrivacyPolicy_AR.html");
                    viewModel.WebUrl = url;
                }
            }
            else
            {
                if (!App.IsArabic)
                {
                    viewModel.WebUrl = "file:///android_asset/PrivacyPolicy_EN.html";
                }
                else
                {
                    viewModel.WebUrl = "file:///android_asset/PrivacyPolicy_AR.html";
                }
            }
        }
        #endregion

        #region Method
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = Application.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = Application.Current.Resources["Back"];
            }
        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                FlowDirection = FlowDirection.LeftToRight;
            }
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();


            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            Padding = safeInsets;


        }
        private void BackButtonClicked(object sender, EventArgs e)
        {
            if (PrivacyandPolicyWebView.CanGoBack)
            {
                PrivacyandPolicyWebView.GoBack();
            }
            else
            {
                viewModel._navigationService.GoBack();
            }
        }

        void PrivacyandPolicyWebView_Navigating(object sender,WebNavigatingEventArgs e)
        {
            viewModel.IsLoading = true;
        }

        void PrivacyandPolicyWebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            viewModel.IsLoading = false;
        }
        #endregion
    }
}