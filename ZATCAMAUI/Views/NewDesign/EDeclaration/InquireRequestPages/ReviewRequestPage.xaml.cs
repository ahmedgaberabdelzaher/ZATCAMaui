using ZATCAMAUI.ViewModel.NewDesignViewModel.EDeclaration;

namespace ZATCAMAUI.Views.NewDesign.EDeclaration.InquireRequestPages
{
    public partial class ReviewRequestPage : ContentPage
    {
        ReviewRequestViewModel viewModel;
        public ReviewRequestPage()
        {
            InitializeComponent();
            viewModel = App.Locator.ReviewRequestViewModel;
            BindingContext = viewModel;
        }

        protected override bool OnBackButtonPressed()
        {

            viewModel.BackMethod();
            return true;
        }
    }
}

