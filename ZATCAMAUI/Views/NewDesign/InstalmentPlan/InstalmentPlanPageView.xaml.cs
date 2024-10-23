using Syncfusion.Maui.ListView;
using ZATCAMAUI.Models.InstalmentPlanModel;
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

        public void outletDecisionOptionsListView_SelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        {
            InstalmentPlanModel selectedItem = e.AddedItems[0] as InstalmentPlanModel;
            viewModel.SelectedOutletOptionIndex = viewModel.OutletDecisionOptions.IndexOf(selectedItem);
            if (selectedItem.ActiveOutletDecisionOptions == AppResources.DBSMZakatInstalmentPlan)
            {
                viewModel.IsZakatSelected = true;
                viewModel.IsIncomeTaxViewEnabled = false;
                Preferences.Set("isZakat", true);
                viewModel.ZakatBtnClicked();
            }
            else if (selectedItem.ActiveOutletDecisionOptions == AppResources.DBSMIncomeTax)
            {
                viewModel.IsZakatSelected = false;
                viewModel.IsIncomeTaxViewEnabled = true;
                Preferences.Set("isZakat", false);
                viewModel.IncomeTaxBtnClicked();
            }
            else
            {
                viewModel.IsZakatSelected = false;
                viewModel.IsIncomeTaxViewEnabled = false;
                viewModel.VatBtnClicked();
            }
        }
    }
}
