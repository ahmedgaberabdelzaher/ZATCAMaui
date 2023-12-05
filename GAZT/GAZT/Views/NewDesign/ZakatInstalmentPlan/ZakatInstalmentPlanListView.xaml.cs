using System;
using System.Threading.Tasks;
using EGAZT.Models.InstalmentPlanModel;
using EGAZT.ViewModel.NewDesignViewModel.VATInstalmentPlanViewModel;
using static EGAZT.Models.ZakatInstalationModels.ZakatInstalmentPlanRequestListModel;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;
using EGAZT.Models.ZakatInstalationModels;
using Xamarin.Forms.Internals;

namespace EGAZT.Views.NewDesign.ZakatInstalmentPlan
{
    [Preserve(AllMembers = true)]
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

                Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");

                //App.IsArabic = true;
                ChangeAeroIcon();
                SetLTR();
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

                viewModel = App.Locator.ZakatInstalmentPlanListPageView;
                this.BindingContext = viewModel;
               
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
                this.FlowDirection = FlowDirection.LeftToRight;
            }
        }
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }

        async void outletDecisionOptionsListView_SelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            try { 
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
            catch (Exception )
            {

            }
        }


        private async void SummaryattachmentsListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            try { 
            var item = e.ItemData as ZakatListModel;
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

        private void RevokListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            /*var item = e.ItemData as Result;
            var index = viewModel.RequestForInstalmentPlanList.IndexOf(item);
            viewModel.GetSummaryDetailsClickedAsync(index);
            viewModel.EnableZakatInstalmentSummary();*/
        }


        private void DisplayListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }


        void OtpFirstEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (viewModel.OTPFirstDigit.Length > 0)
            {
                OTPSecondEntry.Focus();
            }
        }

        void OtpSecondEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (viewModel.OTPSecondDigit.Length > 0)
            {
                OTPThirdEntry.Focus();
            }
        }

        void OtpThirdEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (viewModel.OTPThirdDigit.Length > 0)
            {
                OTPFourthEntry.Focus();
            }
        }
        void OtpFourthEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {

        }
        void OtpFourthEntry_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {

        }

    }
}
