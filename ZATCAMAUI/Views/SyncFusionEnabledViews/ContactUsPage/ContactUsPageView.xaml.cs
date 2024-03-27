using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ContactUsPage;
using Application = Microsoft.Maui.Controls.Application;
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
            On<iOS>().SetUseSafeArea(true);
            ChangeAeroIcon();
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