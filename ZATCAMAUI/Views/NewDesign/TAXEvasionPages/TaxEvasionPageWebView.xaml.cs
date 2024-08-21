
using ZATCAMAUI.ViewModel.NewDesignViewModel.TaxEvasionViewModels;

namespace ZATCAMAUI.Views.NewDesign.TAXEvasionPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxEvasionPageWebView : ContentPage
    {
        TaxEvasionPageWebViewModel viewModel;
        public TaxEvasionPageWebView()
        {
            InitializeComponent();
            viewModel = App.Locator.TaxEvasionPageWebView;
            BindingContext = viewModel;
            viewModel.IsLoading = true;
            SetLanguage();
        }

        void SetLanguage()
        {
            string Url = "";
            if (App.IsArabic)
            {

                // Url = "https://zatca.gov.sa/ar/ContactUs/Pages/ReportFraudMV.aspx";
                Url = "https://stgextportal.gazt.gov.sa/ar/ContactUs/Pages/ReportFraudMVV1.aspx";


            }
            else
            {
                //Url = "https://zatca.gov.sa/en/ContactUs/Pages/ReportFraudMV.aspx";
                Url = "https://stgextportal.gazt.gov.sa/en/ContactUs/Pages/ReportFraudMVV1.aspx";

            }
            taxEvasionWebView.Source = Url;
            // taxEvasionHybridWebView.Source = Url;

        }


        private void TOnBackButtonClicked(object sender, EventArgs e)
        {
            if (taxEvasionWebView.CanGoBack)
            {
                taxEvasionWebView.GoBack();
                return;
            }
            viewModel._navigationService.GoBack();
        }

        private void taxEvasionWebView_Navigated(object sender, WebNavigatedEventArgs e)
        {

            viewModel.IsLoading = false;
        }

        private void taxEvasionWebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            viewModel.IsLoading = true;
        }

        void taxEvasionHybridWebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            viewModel.IsLoading = false;
        }

        void taxEvasionHybridWebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            if (e.Url == "https://stgextportal.gazt.gov.sa/ar/ContactUs/Pages/ReportFraudMVV1.aspx")
            {
                viewModel.IsLoading = false;
                return;
            }
            viewModel.IsLoading = false;
        }
    }
}