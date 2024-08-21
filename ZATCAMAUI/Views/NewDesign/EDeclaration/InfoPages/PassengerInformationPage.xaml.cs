using ZATCAMAUI.ViewModel.NewDesignViewModel.EDeclaration.EDeclarationInformations;

namespace ZATCAMAUI.Views.NewDesign.EDeclaration.InfoPages
{
    public partial class PassengerInformationPage : ContentPage
    {
        EDeclarationInformationsViewModel viewModel;
        public PassengerInformationPage()
        {
            InitializeComponent();
            viewModel = App.Locator.EDeclarationInformationsViewModel;
            BindingContext = viewModel;
        }
        protected override void OnDisappearing()
        {
            viewModel.isPassengerPage = false;
            base.OnDisappearing();
        }
        protected override void OnAppearing()
        {
            viewModel.isPassengerPage = true;

            base.OnAppearing();
        }
        protected override bool OnBackButtonPressed()
        {
            viewModel.BackMethod();
            return true;
        }
    }
}

