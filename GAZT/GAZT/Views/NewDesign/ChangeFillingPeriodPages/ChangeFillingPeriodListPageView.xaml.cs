using System;
using System.Collections.Generic;
using EGAZT.Models.ChageFillingPeriodModel;
using EGAZT.ViewModel.NewDesignViewModel.ChangeFillingPeriodViewModel;
using GAZT.Helper;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

namespace EGAZT.Views.NewDesign.ChangeFillingPeriodPages
{
    public partial class ChangeFillingPeriodListPageView : ContentPage
    {

        #region Variable
        ChangeFillingPeriodListViewModel viewModel;
        #endregion

        public ChangeFillingPeriodListPageView()
        {
            try
            {
                InitializeComponent();

                Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");

                ChangeAeroIcon();
                SetLTR();
                On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

                viewModel = App.Locator.ChangeFillingPeriodListPageView;

                this.BindingContext = viewModel;

              

            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                viewModel.ResetData();
                viewModel.GetVATChangeFillingList();
            }
            catch (Exception e)
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

        private void Request_Item_Tapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.ItemData as VATChangeFillingListModel.ChangeFillingFrequency;



            if(item.Fbust == "E0018" || item.Fbust == "E0075" || item.Fbust == "E0074" || item.Fbust == "E0013") {

                App.selectedVatFillingItem = item.Fbnum;
                App.selectedVATItemFbust = item.Fbust;
                viewModel._navigationService.NavigateTo(App.ChangeFillingPeriodPageView);

            }
            else {



                viewModel.GetVATChangeFillingSummary(item);
                viewModel.EnableSummaryView();
            }

        }

        private void Download_Acknowledgement(object sender, EventArgs e)
        {

            if (viewModel.vATChangingSummaryData.Fbnum != null)
            {

                String downloadurl = Constants.downloadFile + "'" + viewModel.vATChangingSummaryData.Fbnum + "')/$value";
                //await WebServiceManager.FileDownload(downloadurl, "pdf");
                viewModel._navigationService.NavigateTo(App.PdfView, downloadurl);

            }
        }


    }
}
