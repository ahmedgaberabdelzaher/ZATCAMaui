
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.FAQPage;
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