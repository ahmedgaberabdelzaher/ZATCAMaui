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
            if (e.Url.Contains("https://zatca.gov.sa/") || e.Url.Contains("http://gazt.gov.sa/"))
            {
                Navigation.PopAsync();
            }
        }
    }
}
