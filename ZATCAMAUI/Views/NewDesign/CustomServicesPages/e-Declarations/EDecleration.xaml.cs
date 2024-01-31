using ZATCAMAUI.ViewModel.NewDesignViewModel.CustomServicesViewModels;

namespace ZATCAMAUI.Views.NewDesign.CustomServicesPages.eDeclarations
{
    public partial class EDeclerationView : ContentPage
    {
        E_DeclerationViewModel viewModel;
        public EDeclerationView()
        {
            viewModel = App.Locator.eDeclerationViewModel;
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}
