
using Syncfusion.Maui.ListView;
using ZATCAMAUI.Models.InstalmentPlanModel;
using ZATCAMAUI.Models.ZakatInstalationModels;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatInstalmentViewModel;
using ItemTappedEventArgs = Syncfusion.Maui.ListView.ItemTappedEventArgs;

namespace ZATCAMAUI.Views.NewDesign.ZakatInstalmentPlan
{
 
    public partial class OldZakatInstalmentPlanListPageView : ContentPage
    {
        #region Variable
        OldZakatInstalmentPlanListViewModel viewModel;
        #endregion

        public OldZakatInstalmentPlanListPageView()
        {
            try
            {
                InitializeComponent();


                viewModel = App.Locator.OldZakatInstalmentPlanListPageView;
                BindingContext = viewModel;
                viewModel.ReqVatInstalmentPlanResponseList = null;

            }
            catch (Exception)
            {
            }

        }

        async void outletDecisionOptionsListView_SelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        {
            viewModel.IsLoading = true;
            InstalmentPlanModel selectedItem = e.AddedItems[0] as InstalmentPlanModel;
            await Task.Delay(1000);

            if (viewModel.OutletDecisionOptions.IndexOf(selectedItem) == 0)
            {
                viewModel.EnableCreateZakatInstalment();
                await viewModel.GetZakatInstalmentPlanList();

            }
            else if (viewModel.OutletDecisionOptions.IndexOf(selectedItem) == 1)
            {
                viewModel.EnableRevokZakatInstalment();
            }
            viewModel.IsLoading = false;
        }



        private async void SummaryattachmentsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                viewModel.IsLoading = true;
                var item = e.DataItem as OldZakatListModel;
                if (item != null)
                {


                    if (item.statusType == "IP017")
                    {
                        App.selectedZakatItem = item.fbNum;
                        viewModel._navigationService.NavigateTo(App.OldZakatInstalmentPlanPageView);

                    }
                    else
                    {

                        viewModel.SelectedFbNum = item.fbNum;
                        var index = viewModel.ZakatListData.IndexOf(item);

                        await viewModel.GetSummaryDetailsClickedAsync(index);
                        viewModel.EnableZakatInstalmentSummary();
                    }
                }
                viewModel.IsLoading = false;

            }
            catch (Exception)
            {
                viewModel.IsLoading = false;

            }


        }

        void OtpFirstEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.OTPFirstDigit.Length > 0)
            {
                OTPSecondEntry.Focus();
            }
        }

        void OtpSecondEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.OTPSecondDigit.Length > 0)
            {
                OTPThirdEntry.Focus();
            }
        }

        void OtpThirdEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (viewModel.OTPThirdDigit.Length > 0)
            {
                OTPFourthEntry.Focus();
            }
        }


    }
}
