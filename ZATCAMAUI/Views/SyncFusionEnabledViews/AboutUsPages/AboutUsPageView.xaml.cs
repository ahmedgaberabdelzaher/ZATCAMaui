

using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.AboutUsPage;
using ZATCAMAUI.Views.NewDesign.TaxpayerCorrespondancePages;
using Application = Microsoft.Maui.Controls.Application;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.AboutUsPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutUsPageView : ContentPage
    {
        #region Variable
        AboutUsPageViewModel viewModel;
        #endregion

        #region Constructor
        public AboutUsPageView()
        {
            try
            {
                InitializeComponent();
                viewModel = App.Locator.AboutUsPageView;
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
                        string url = Path.Combine(path, "About_EN.html");
                        viewModel.WebUrl = url;
                    }
                    else
                    {
                        string url = Path.Combine(path, "About_AR.html");
                        viewModel.WebUrl = url;
                    }
                }
                else
                {
                    if (!App.IsArabic)
                    {
                        viewModel.WebUrl = "file:///android_asset/About_EN.html";
                    }
                    else
                    {
                        viewModel.WebUrl = "file:///android_asset/About_AR.html";
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region Method
        protected async override void OnAppearing()
        {
            base.OnAppearing();


            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            Padding = safeInsets;


        }
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
        private void BackButtonClicked(object sender, EventArgs e)
        {
            if (AboutUsWebView.CanGoBack)
            {
                AboutUsWebView.GoBack();
            }
            else
            {
                viewModel._navigationService.GoBack();
            }
        }

        void AboutUsWebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            viewModel.IsLoading = true;
        }

        void AboutUsWebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            viewModel.IsLoading = false;
        }
        #endregion

    }
}