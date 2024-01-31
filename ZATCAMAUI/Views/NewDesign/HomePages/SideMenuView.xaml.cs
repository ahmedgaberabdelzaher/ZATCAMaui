using ZATCAMAUI.ViewModel.NewDesignViewModel.HomeViewModels;

namespace ZATCAMAUI.Views.NewDesign.HomePages
{
    public partial class SideMenuView : BaseContentPage
    {
        HomeViewModel viewModel;
        public SideMenuView()
        {
            viewModel = App.Locator.homeViewModel;
            BindingContext = viewModel;
            viewModel.GetSideMenuLst();
            viewModel.CurrentTab = 2;
            InitializeComponent();
        }
    }
}
