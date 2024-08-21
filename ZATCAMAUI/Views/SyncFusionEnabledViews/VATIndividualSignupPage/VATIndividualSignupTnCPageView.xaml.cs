using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;
using ZATCAMAUI.Views.NewDesign.TaxpayerCorrespondancePages;
using Application = Microsoft.Maui.Controls.Application;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.VATIndividualSignupPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VATIndividualSignupTnCPageView : ContentPage
    {

        VATIndividualSignupTnCPageViewModel viewModel;
        public VATIndividualSignupTnCPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.VATIndividualSignupTnCPageView;
            BindingContext = viewModel;
            if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                string baseUrl = DependencyService.Get<IBaseUrl>().Get();
                string path = DependencyService.Get<IBaseUrl>().Get();
                if (!App.IsArabic)
                {
                    string url = Path.Combine(path, "TermsAndConditionsEN.html");
                    TCWebView.Source = url;
                }
                else
                {
                    string url = Path.Combine(path, "TermsAndConditionsAR.html");
                    TCWebView.Source = url;
                }
            }
            else
            {
                if (!App.IsArabic)
                {
                    TCWebView.Source = "file:///android_asset/TermsAndConditionsEN.html";
                }
                else
                {
                    TCWebView.Source = "file:///android_asset/TermsAndConditionsAR.html";
                }
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.IsButtonEnabled = false;
            viewModel.IschkTAndC = false;
            viewModel.VerifyButtonDisableColor = (Color)Application.Current.Resources["ButtonGray"];
            viewModel.IsLoading = false;
        }
       
    }
}