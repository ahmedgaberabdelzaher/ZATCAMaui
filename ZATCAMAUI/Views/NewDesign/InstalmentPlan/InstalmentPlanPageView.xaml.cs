using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Syncfusion.Maui.ListView;
using ZATCAMAUI.Models.InstalmentPlanModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.InstalmentPlanViewModel;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;

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

                NavigationPage.SetBackButtonTitle(this, "");

                //App.IsArabic = true;
                ChangeAeroIcon();
                SetLTR();
                On<iOS>().SetUseSafeArea(true);

                viewModel = App.Locator.InstalmentPlanPageView;
                BindingContext = viewModel;
                viewModel.AddOutletDecisionOptions();
            }
            catch (Exception)
            {


            }

        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                FlowDirection = FlowDirection.LeftToRight;
            }
        }
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            Padding = safeInsets;
        }

        public void outletDecisionOptionsListView_SelectionChanged(object sender,ItemSelectionChangedEventArgs e)
        {
            InstalmentPlanModel selectedItem = e.AddedItems[0] as InstalmentPlanModel;
            viewModel.SelectedOutletOptionIndex = viewModel.OutletDecisionOptions.IndexOf(selectedItem);
            //            viewModel.ReasonContinueBtnClicked();


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
