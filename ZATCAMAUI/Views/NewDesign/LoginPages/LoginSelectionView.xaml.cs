using ZATCAMAUI.ViewModel.NewDesignViewModel.LoginViewModels;

namespace ZATCAMAUI.Views.NewDesign.LoginPages
{
    public partial class LoginSelectionView : BaseContentPage
    {
        CustomLoginViewModel viewModel;
        public LoginSelectionView()
        {
            viewModel = App.Locator.CustomLoginViewModel;
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}
