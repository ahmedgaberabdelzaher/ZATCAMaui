using ZATCAMAUI.ViewModel.NewDesignViewModel.HomeViewModels;

namespace ZATCAMAUI.Views.NewDesign.HomePages
{
    public partial class GeneralServices : BaseContentPage
    {
        HomeViewModel viewModel;
        public GeneralServices()
        {
            viewModel = App.Locator.homeViewModel;
            viewModel.GetGeneralServiceMenuLst();
            viewModel.ItemCountPerRow = 2;
            viewModel.CurrentService = ViewModel.NewDesignViewModel.HomeViewModels.Services.GeneralServices;
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}
