using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Syncfusion.Maui.ListView;
using ZATCAMAUI.Models.InstalmentPlanModel;
using ZATCAMAUI.Models.ZakatInstalationModels;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatInstalmentViewModel;
using ItemTappedEventArgs = Syncfusion.Maui.ListView.ItemTappedEventArgs;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;

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


                // viewModel.onPageLoad();

            }
            catch (Exception)
            {


            }

        }

        async void outletDecisionOptionsListView_SelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        {
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
                // viewModel.GetZakatRevokList();
            }

        }

        protected async override void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                viewModel.ResetData();
                viewModel.EnableCreateZakatInstalment();
                await viewModel.GetZakatInstalmentPlanList();


            }
            catch (Exception)
            {


            }
        }


        private async void SummaryattachmentsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
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


            }
            catch (Exception)
            {


            }


        }

        private void RevokListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
         
        }


        private void DisplayListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

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
        void OtpFourthEntry_TextChanged(object sender,TextChangedEventArgs e)
        {

        }
        void OtpFourthEntry_Unfocused(object sender, FocusEventArgs e)
        {

        }

    }
}
