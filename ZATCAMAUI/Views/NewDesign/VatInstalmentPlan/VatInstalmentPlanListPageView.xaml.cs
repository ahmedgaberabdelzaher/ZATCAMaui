using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Syncfusion.Maui.ListView;
using System.Collections.Specialized;
using ZATCAMAUI.Models.InstalmentPlanModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATInstalmentPlanViewModel;
using static ZATCAMAUI.Models.VATInstalmentModels.RequestToVATInstallmentPlanDetails;
using static ZATCAMAUI.Models.VATInstalmentModels.RequestToVATInstallmentPlanDetails.DisplayInstallmentAgreementSchedulePlan;
using ItemTappedEventArgs = Syncfusion.Maui.ListView.ItemTappedEventArgs;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;

namespace ZATCAMAUI.Views.NewDesign.VatInstalmentPlan
{

    public partial class VatInstalmentPlanListPageView : ContentPage
    {

        #region Variable
        VATInstalmentPlanListViewModel viewModel;


        #endregion

        public VatInstalmentPlanListPageView()
        {
            try
            {
                InitializeComponent();

                NavigationPage.SetBackButtonTitle(this, "");

                //App.IsArabic = true;
                ChangeAeroIcon();
                SetLTR();
                On<iOS>().SetUseSafeArea(true);

                viewModel = App.Locator.VatInstalmentPlanListPageView;
                BindingContext = viewModel;
                viewModel.ResetData();
                VATInstalmentDisplayViewPage.DataSource.DisplayItems.CollectionChanged += DisplayItems_CollectionChanged;

            }
            catch (Exception)
            {


            }

        }

        private void DisplayItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (VATInstalmentDisplayViewPage.DataSource.DisplayItems.Count == 0)
            {
                VATInstalmentDisplayViewPage.IsVisible = false;
                NoDataLable.IsVisible = true;
            }
            if (VATInstalmentDisplayViewPage.DataSource.DisplayItems.Count > 0)
            {
                VATInstalmentDisplayViewPage.IsVisible = true;
                NoDataLable.IsVisible = false;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<object, bool>(this, "ISCallBackFromSuccess");

        }
        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                var safeInsets = On<iOS>().SafeAreaInsets();
                safeInsets.Bottom = -10;
                Padding = safeInsets;

                viewModel.EnableVATLandingPage();
                viewModel.AddOutletDecisionOptions();

                MessagingCenter.Subscribe<object, bool>(this, "ISCallBackFromSuccess", async (sender, arg) =>
                {
                    if (arg == true)
                    {
                        viewModel.EnableVAtInstalmentPlan();
                        await viewModel.GetVATInstalmentPlanList();
                    }
                    else
                    {

                        viewModel.EnableVATLandingPage();
                        viewModel.AddOutletDecisionOptions();
                    }

                });


                viewModel.NumberOfInstalmentPlans = "" + AppResources.ZakatInstalmetPlan;




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

        async void outletDecisionOptionsListView_SelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        {
            InstalmentPlanModel selectedItem = e.AddedItems[0] as InstalmentPlanModel;
            viewModel.SelectedOutletOptionIndex = viewModel.OutletDecisionOptions.IndexOf(selectedItem);
            await Task.Delay(1000);
            if (viewModel.OutletDecisionOptions.IndexOf(selectedItem) == 0)
            {
                viewModel.EnableVAtInstalmentPlan();
                await viewModel.GetVATInstalmentPlanList();
            }
            else if (viewModel.OutletDecisionOptions.IndexOf(selectedItem) == 1)
            {
                viewModel.EnableDisplayInstalment();
                await viewModel.GetVATDisplaySchedule();
            }

        }

        private async void SummaryattachmentsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.DataItem as Result31;


            if (item.Fbust == "E0018" || item.Fbust == "E0075" || item.Fbust == "E0074" || item.Fbust == "E0013")
            {

                App.selectedVATItem = item.Fbnum;
                App.selectedVATItemFbust = item.Fbust;
                viewModel._navigationService.NavigateTo(App.VatInstalmentPlanPageView);
            }

            else
            {

                //viewModel.SummaryData();
                var index = viewModel.RequestForInstalmentPlanList.IndexOf(item);
                await viewModel.GetDetailsClicked(index);
                viewModel.EnableVAtInstalmentSummary();
            }


        }

        private async void DisplayListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.DataItem as VtiaIaSetResult;
            var index = viewModel.RequestForScheduleList.IndexOf(item);
            await viewModel.GetDisplayDetailsClicked(index);


            viewModel.EnableDisplayDetails();
        }
    }
}
