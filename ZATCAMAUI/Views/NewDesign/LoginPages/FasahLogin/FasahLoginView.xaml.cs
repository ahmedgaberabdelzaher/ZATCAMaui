using ZATCAMAUI.ViewModel.NewDesignViewModel.LoginViewModels;

namespace ZATCAMAUI.Views.NewDesign.LoginPages.FasahLogin
{
    public partial class FasahLoginView : BaseContentPage
    {
        FasahLoginViewModel viewModel;
        public FasahLoginView()
        {
            viewModel = App.Locator.FasahLoginViewModel;
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}

