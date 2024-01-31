using ZATCAMAUI.ViewModel.NewDesignViewModel.LoginViewModels;

namespace ZATCAMAUI.Views.NewDesign.CustomServicesPages.CustomDashBoard
{
    public partial class CustomDashBoardVi : BaseContentPage
    {

        CustomLoginViewModel viewModel;
        public CustomDashBoardVi()
        {
            viewModel = App.Locator.CustomLoginViewModel;
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}
