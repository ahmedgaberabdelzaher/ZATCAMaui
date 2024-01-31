using ZATCAMAUI.ViewModel.NewDesignViewModel.EDeclaration.EDeclarationInformations;

namespace ZATCAMAUI.Views.NewDesign.EDeclaration.InfoPages
{
    public partial class ContactInformationPage : ContentPage
    {
        EDeclarationInformationsViewModel viewModel;
        public ContactInformationPage()
        {

            InitializeComponent();
            viewModel = App.Locator.EDeclarationInformationsViewModel;
            BindingContext = viewModel;

        }
        protected override void OnAppearing()
        {
            viewModel.isContactPage = true;

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            viewModel.isContactPage = false;
            base.OnDisappearing();
        }
        protected override bool OnBackButtonPressed()
        {
            viewModel.BackMethod();
            return true;
        }
    }
}

