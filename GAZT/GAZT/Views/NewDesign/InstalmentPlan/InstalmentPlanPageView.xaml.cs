using System;
using EGAZT.Models.InstalmentPlanModel;
using EGAZT.ViewModel.NewDesignViewModel.InstalmentPlanViewModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EGAZT.Views.NewDesign.InstalmentPlan
{
    [Preserve(AllMembers = true)]
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

                Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");

                //App.IsArabic = true;
                ChangeAeroIcon();
                SetLTR();
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

                viewModel = App.Locator.InstalmentPlanPageView;
                this.BindingContext = viewModel;


            }
            catch (Exception ex)
            {

            }

        }
        private void SetLTR()
        {
            if (!App.IsArabic)
            {
                this.FlowDirection = FlowDirection.LeftToRight;
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
            this.Padding = safeInsets;
            viewModel.AddOutletDecisionOptions();
        }

        public async void outletDecisionOptionsListView_SelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
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
