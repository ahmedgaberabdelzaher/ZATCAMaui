using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using ZATCAMAUI.Models.InstalmentPlanModel;
using ZATCAMAUI.Models.ZakatInstalationModels;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ZakatInstalmentViewModel;
using Application = Microsoft.Maui.Controls.Application;
using ItemTappedEventArgs = Syncfusion.Maui.ListView.ItemTappedEventArgs;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;

namespace ZATCAMAUI.Views.NewDesign.ZakatInstalmentPlan
{
   
    public partial class ZakatInstalmentPlanListPageView : ContentPage
    {



        #region Variable
        ZakatInstalmentPlanListViewModel viewModel;


        #endregion

        public ZakatInstalmentPlanListPageView()
        {
            try
            {
                InitializeComponent();

                NavigationPage.SetBackButtonTitle(this, "");

                //App.IsArabic = true;
                ChangeAeroIcon();
                SetLTR();
                On<iOS>().SetUseSafeArea(true);

                viewModel = App.Locator.ZakatInstalmentPlanListPageView;
                BindingContext = viewModel;

                _ = viewModel.onPageLoad();

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
                Resources["StyleReverseBack"] = Application.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = Application.Current.Resources["Back"];
            }
        }

        async void outletDecisionOptionsListView_SelectionChanged(object sender, Syncfusion.Maui.ListView.ItemSelectionChangedEventArgs e)
        {
            try
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
                    await viewModel.GetZakatRevokList();
                }
            }
            catch (Exception)
            {


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


        private async void SummaryattachmentsListView_ItemTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
        {
            try
            {
                var item = e.DataItem as ZakatListModel;
                if (item != null)
                {


                    if (item.statusType == "E0013")
                    {
                        App.selectedZakatItem = item.fbNum;




                        viewModel._navigationService.NavigateTo(App.ZakatInstalmentPlanPageView);



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

        private void RevokListView_ItemTapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
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
        void OtpFourthEntry_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        void OtpFourthEntry_Unfocused(object sender, FocusEventArgs e)
        {

        }

    }
}
