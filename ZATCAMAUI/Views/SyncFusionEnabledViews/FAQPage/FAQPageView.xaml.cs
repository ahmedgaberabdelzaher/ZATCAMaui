
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.FAQPage;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Application = Microsoft.Maui.Controls.Application;
using ZATCAMAUI.Core.Helper;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.FAQPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FAQPageView : ContentPage
    {
        FAQPageViewModel viewModel;
        public FAQPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.FAQPageView;
            BindingContext = viewModel;
            On<iOS>().SetUseSafeArea(true);
            ChangeAeroIcon();
            SetUrl();
        }

        public void SetUrl()
        {
            if (App.IsArabic)
            {
                // viewModel.WebUrl = "https://gazt.gov.sa/ar/contactus/Pages/default.aspx";
                viewModel.WebUrl = ZATCAConstants.GAZTFAQARUrl;
            }
            else
            {
                viewModel.WebUrl = ZATCAConstants.GAZTFAQEnUrl;
            }

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
        private void ContactWebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            viewModel.IsLoading = true;
        }

        private void ContactWebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            viewModel.IsLoading = false;
        }
    }
}