using Syncfusion.Maui.ListView;
using System.Collections.Specialized;
using ZATCAMAUI.Models.InstalmentPlanModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.VATInstalmentPlanViewModel;
using static ZATCAMAUI.Models.VATInstalmentModels.RequestToVATInstallmentPlanDetails;
using static ZATCAMAUI.Models.VATInstalmentModels.RequestToVATInstallmentPlanDetails.DisplayInstallmentAgreementSchedulePlan;
using ItemTappedEventArgs = Syncfusion.Maui.ListView.ItemTappedEventArgs;

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
                
                viewModel.EnableVATLandingPage();
                viewModel.AddOutletDecisionOptions();

                MessagingCenter.Subscribe<object, bool>(this, "ISCallBackFromSuccess", async (sender, arg) =>
                {
                    if (arg == true)
                    {
                        viewModel.EnableVAtInstalmentPlan();
                        viewModel.IsLoading = true;
                        await viewModel.GetVATInstalmentPlanList();
                        viewModel.IsLoading = false;
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


        async void outletDecisionOptionsListView_SelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        {
            viewModel.IsLoading = true;
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
            viewModel.IsLoading = false;
        }

        private async void SummaryattachmentsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            viewModel.IsLoading = true;
            var item = e.DataItem as Result31;


            if (item.Fbust == "E0018" || item.Fbust == "E0075" || item.Fbust == "E0074" || item.Fbust == "E0013")
            {

                App.selectedVATItem = item.Fbnum;
                App.selectedVATItemFbust = item.Fbust;
                viewModel._navigationService.NavigateTo(App.VatInstalmentPlanPageView);
            }

            else
            {
                var index = viewModel.RequestForInstalmentPlanList.IndexOf(item);
                await viewModel.GetDetailsClicked(index);
                viewModel.EnableVAtInstalmentSummary();
            }
            viewModel.IsLoading = false;

        }

        private async void DisplayListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            viewModel.IsLoading = true;
            var item = e.DataItem as VtiaIaSetResult;
            var index = viewModel.RequestForScheduleList.IndexOf(item);
            await viewModel.GetDisplayDetailsClicked(index);

            viewModel.EnableDisplayDetails();
            viewModel.IsLoading = false;
        }
    }
}
