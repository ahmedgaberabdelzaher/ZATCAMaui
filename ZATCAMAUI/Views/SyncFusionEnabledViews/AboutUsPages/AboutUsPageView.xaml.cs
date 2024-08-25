
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
                BindingContext = viewModel;
                if (DeviceInfo.Platform == DevicePlatform.iOS)
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