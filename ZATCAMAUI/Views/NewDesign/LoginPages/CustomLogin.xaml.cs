using ZATCAMAUI.ViewModel.NewDesignViewModel.LoginViewModels;

namespace ZATCAMAUI.Views.NewDesign.LoginPages
{

    public partial class CustomLogin : BaseContentPage
    {
        CustomLoginViewModel viewModel;
        public CustomLogin()
        {
            viewModel = App.Locator.CustomLoginViewModel;
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}
