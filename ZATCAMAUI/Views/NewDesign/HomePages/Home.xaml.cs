using ZATCAMAUI.ViewModel.NewDesignViewModel.HomeViewModels;

namespace ZATCAMAUI.Views.NewDesign.HomePages
{
    public partial class Home : BaseContentPage
    {
        HomeViewModel viewModel;
        public Home(string tab = "0")
        {
            viewModel = App.Locator.homeViewModel;
            BindingContext = viewModel;

            viewModel.CurrentTab = 0;
            InitializeComponent();
            if (tab == "3")
            {
                tab = "0";
                viewModel.GetDashBoardMenuLst(3);
                HasBackButton = true;
                preLoginMenu.IsVisible = false;

            }
            else
            {
                HasBackButton = false;
                beforeLoginMenu.IsVisible = false;
                preLoginMenu.IsVisible = true;
            }

        }
    }
}
