using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.VATIndividualSignupPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationSuccessfulPageView : ContentPage
    {
        RegistrationSuccessfulPageViewModel viewModel;
        public RegistrationSuccessfulPageView(string TIN)
        {
            InitializeComponent();
            viewModel = App.Locator.VATRegistrationSuccessfulPageView;
            BindingContext = viewModel;
            viewModel.TINnumber = TIN;
            if (App.successMsg == true)
            {
                viewModel.IsGulf = true;
                viewModel.IsCitizen = false;
            }
            else
            {
                viewModel.IsCitizen = true;
                viewModel.IsGulf = false;
            }
        }
        private void btnVATRegistration_Clicked(object sender, EventArgs e)
        {
            viewModel._navigationService.NavigateTo(App.SFLoginPageView, App.SFLandingPageView);

        }

        protected override bool OnBackButtonPressed() => true;

    }
}