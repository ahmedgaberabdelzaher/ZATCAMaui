
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ContactUsPage;
namespace ZATCAMAUI.Views.SyncFusionEnabledViews.ContactUsPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactUsPageView : ContentPage
    {
        ContactUsPageViewModel viewModel;
        public ContactUsPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.ContactUsPageView;
            SetUrl();
            BindingContext = viewModel;
        }
        public void SetUrl()
        {
            if (App.IsArabic)
            {
                viewModel.WebUrl = "https://zatca.gov.sa/ar/contactus/Pages/default.aspx";
            }
            else
            {
                viewModel.WebUrl = "https://zatca.gov.sa/en/contactus/Pages/default.aspx";
            }

        }

      
        private void BackButtonClicked(object sender, EventArgs e)
        {
            if (ContactWebView.CanGoBack)
            {
                ContactWebView.GoBack();
            }
            else
            {
                viewModel._navigationService.GoBack();
            }
        }
    }
}