using ZATCAMAUI.ViewModel.NewDesignViewModel.SurveyViewModels;

namespace ZATCAMAUI.Views.NewDesign.Survey
{
    public partial class RateUs : BaseContentPage
    {
        RateUsViewModel viewModel;
        public RateUs()
        {
            viewModel = App.Locator.rateUsViewModel;
            BindingContext = viewModel;
            InitializeComponent();
        }

        void WebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            viewModel.IsLoading = true;
            if (e.Url.Contains("https://zatca.gov.sa/")
                || e.Url.Contains("http://gazt.gov.sa/")
                || e.Url.Contains("http://zatca.gov.sa/"))
            {
                Navigation.PopAsync();
            }
        }

        void WebView_Navigated(System.Object sender, Microsoft.Maui.Controls.WebNavigatedEventArgs e)
        {
            viewModel.IsLoading = false;
        }
    }
}
