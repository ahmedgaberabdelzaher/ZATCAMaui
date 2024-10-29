using ZATCAMAUI.ViewModel.NewDesignViewModel.ChangeFillingPeriodViewModel;

namespace ZATCAMAUI.Views.NewDesign.ChangeFillingPeriodPages
{

    public partial class ChangeFillingPeriodListPageView : ContentPage
    {

        #region Variable

        ChangeFillingPeriodListViewModel viewModel;

        #endregion

        public ChangeFillingPeriodListPageView()
        {
            try
            {
                InitializeComponent();
                viewModel = App.Locator.ChangeFillingPeriodListPageView;
                BindingContext = viewModel;
            }
            catch (Exception)
            {


            }
        }

    }
}
