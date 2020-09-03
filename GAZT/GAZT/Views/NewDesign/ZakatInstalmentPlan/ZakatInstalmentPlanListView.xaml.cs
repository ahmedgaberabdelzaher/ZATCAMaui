using System;
using System.Threading.Tasks;
using EGAZT.Models.InstalmentPlanModel;
using EGAZT.ViewModel.NewDesignViewModel.VATInstalmentPlanViewModel;
using static EGAZT.Models.ZakatInstalationModels.ZakatInstalmentPlanRequestListModel;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

namespace EGAZT.Views.NewDesign.ZakatInstalmentPlan
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

                Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");

                //App.IsArabic = true;
                ChangeAeroIcon();
                SetLTR();
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

                viewModel = App.Locator.ZakatInstalmentPlanListPageView;
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
                Resources["StyleReverseBack"] = App.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = App.Current.Resources["Back"];
            }
        }

        async void outletDecisionOptionsListView_SelectionChanged(System.Object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            InstalmentPlanModel selectedItem = e.AddedItems[0] as InstalmentPlanModel;
            await Task.Delay(1000);

            if (viewModel.OutletDecisionOptions.IndexOf(selectedItem) == 0)
            {
                viewModel.EnableCreateZakatInstalment();
                viewModel.GetZakatInstalmentPlanList();

            }
            else if (viewModel.OutletDecisionOptions.IndexOf(selectedItem) == 1)
            {
                viewModel.EnableRevokZakatInstalment();
                viewModel.GetZakatRevokList();
            }

        }

        protected async override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                viewModel.EnableZakatLandingPage();
            }
            catch (Exception e)
            {

            }
        }


        private void SummaryattachmentsListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var item = e.ItemData as Result;
            var index = viewModel.RequestForInstalmentPlanList.IndexOf(item);
            viewModel.GetSummaryDetailsClickedAsync(index);
            viewModel.EnableZakatInstalmentSummary();
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



    }
}
