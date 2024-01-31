using ZATCAMAUI.ViewModel.NewDesignViewModel.EDeclaration.EDeclarationInformations;

namespace ZATCAMAUI.Views.NewDesign.EDeclaration
{
    public partial class EDeclarationSuccessPage : ContentPage
    {
        EDeclarationInformationsViewModel viewModel;
        public EDeclarationSuccessPage()
        {
            InitializeComponent();
            viewModel = App.Locator.EDeclarationInformationsViewModel;
            BindingContext = viewModel;
        }
    }
}

