using ZATCAMAUI.ViewModel.NewDesignViewModel.EDeclaration;

namespace ZATCAMAUI.Views.NewDesign.EDeclaration
{
    public partial class NewDeclarationPage : ContentPage
    {
        BaseEDeclarationViewModel viewModel;

        object payload;

        public NewDeclarationPage()
        {
            InitializeComponent();
            viewModel = App.Locator.BaseEDeclarationViewModel;

            viewModel.SubmitModel.travelerDeclaration = new Models.EDeclerationsModel.SubmitModels.TravelerDeclaration();

            viewModel.SubmitModel.travelerDeclaration.Isvisitor = true;

            App.Locator.StateManager.SetItem("IsLoggedIn", viewModel.SubmitModel.travelerDeclaration.Isvisitor);

            BindingContext = viewModel;
        }

        public NewDeclarationPage(object payload = null)
        {
            InitializeComponent();
            viewModel = App.Locator.BaseEDeclarationViewModel;

            viewModel.SubmitModel.travelerDeclaration = new Models.EDeclerationsModel.SubmitModels.TravelerDeclaration();

            viewModel.SubmitModel.travelerDeclaration.Isvisitor = true;

            App.Locator.StateManager.SetItem("IsLoggedIn", viewModel.SubmitModel.travelerDeclaration.Isvisitor);

            this.payload = payload;

            if (this.payload != null)
            {
                viewModel.SetPassangerData(this.payload);
                viewModel.SubmitModel.travelerDeclaration.Isvisitor = false;

            }
            BindingContext = viewModel;

        }

        protected override void OnAppearing()
        {
            if (this.payload != null)
            {
                viewModel.SubmitModel.travelerDeclaration.Isvisitor = false;
                App.Locator.StateManager.SetItem("IsLoggedIn", viewModel.SubmitModel.travelerDeclaration.Isvisitor);
            }
            base.OnAppearing();
        }
    }
}

