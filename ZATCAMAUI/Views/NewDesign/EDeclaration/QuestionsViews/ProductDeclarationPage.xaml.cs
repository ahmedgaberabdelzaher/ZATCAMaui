using ZATCAMAUI.Models.EDeclerationsModel.FeesCalculators;
using ZATCAMAUI.ViewModel.NewDesignViewModel.EDeclaration.EDeclarationProduct;

namespace ZATCAMAUI.Views.NewDesign.EDeclaration.QuestionsViews
{
    public partial class ProductDeclarationPage : ContentPage
    {
        BaseProductDeclarationViewModel viewModel;
        public ProductDeclarationPage()
        {
            InitializeComponent();
            viewModel = App.Locator.ProductDeclarationViewModel;
            BindingContext = viewModel;
            viewModel.IsArrivingPlaneSelected = viewModel.SubmitModel.travelerDeclaration.travelingType == 2 ? false : true;
            viewModel.HeaderTitle = viewModel.IsArrivingPlaneSelected ? AppResources.EDeclarationArrivalHeader : AppResources.EDeclarationDepatureHeader;
            viewModel.FeesCalculatorResponse = new FeesCalculatorResponse();
            viewModel.FeesCalculatorBody = new FeesCalculatorBody();

        }

        protected override bool OnBackButtonPressed()
        {
            viewModel.BackMethod();
            return true;
        }
    }
}

