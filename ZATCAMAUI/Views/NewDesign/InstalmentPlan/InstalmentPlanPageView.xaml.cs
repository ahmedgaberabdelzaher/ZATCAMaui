using ZATCAMAUI.ViewModel.NewDesignViewModel.InstalmentPlanViewModel;

namespace ZATCAMAUI.Views.NewDesign.InstalmentPlan
{

    public partial class InstalmentPlanPageView : ContentPage
    {
        #region Variable
        InstalmentPlanViewModel viewModel;
        #endregion
        public InstalmentPlanPageView()
        {
            try
            {
                InitializeComponent();

                viewModel = App.Locator.InstalmentPlanPageView;
                BindingContext = viewModel;
                viewModel.AddOutletDecisionOptions();
            }
            catch (Exception)
            {


            }

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (outletDecisionOptionsListView != null)
            {
                outletDecisionOptionsListView.SelectedItem = null;
            }

        }
    }
}
