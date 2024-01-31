using ZATCAMAUI.ViewModel.NewDesignViewModel.EDeclaration;

namespace ZATCAMAUI.Views.NewDesign.EDeclaration
{
    public partial class EDeclarationPage : BaseContentPage
    {
        EDeclerationViewModel viewModel;
        public EDeclarationPage()
        {

            viewModel = App.Locator.EDeclerationViewModel;
            BindingContext = viewModel;
            InitializeComponent();
            viewModel.ShowReviewEntries = false;
            viewModel.IdentityType = 0;
        }
        protected override bool OnBackButtonPressed()
        {
            viewModel.BackMethod();
            return true;
        }
    }
}

