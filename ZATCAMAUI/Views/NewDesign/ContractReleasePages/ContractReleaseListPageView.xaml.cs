using ZATCAMAUI.ViewModel.NewDesignViewModel.ContractRelease;

namespace ZATCAMAUI.Views.NewDesign.ContractReleasePages
{

    public partial class ContractReleaseListPageView : ContentPage
    {

        #region Variable
        ContractReleaseListViewModel viewModel;

        #endregion

        public ContractReleaseListPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.ContractReleasePageListView;
            BindingContext = viewModel;
        }


    }
}
