
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Models.ChageFillingPeriodModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ChangeFillingPeriodViewModel;

namespace ZATCAMAUI.Views.NewDesign.ChangeFillingPeriodPages
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
                viewModel = App.Locator.ChangeFillingPeriodListPageView;
                BindingContext = viewModel;
            }
            catch (Exception)
            {


            }
        }

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                viewModel.ResetData();
                _ = viewModel.GetVATChangeFillingList();
            }
            catch (Exception)
            {

            }
        }


        async void Request_Item_Tapped(object sender, Syncfusion.Maui.ListView.ItemTappedEventArgs e)
        {
            try
            {
                var item = e.DataItem as VATChangeFillingListModel.ChangeFillingFrequency;
                if (item.Fbust == "E0018" || item.Fbust == "E0075" || item.Fbust == "E0074" || item.Fbust == "E0013")
                {
                    App.selectedVatFillingItem = item.Fbnum;
                    App.selectedVATItemFbust = item.Fbust;
                    viewModel._navigationService.NavigateTo(App.ChangeFillingPeriodPageView);
                }
                else
                {

                    await viewModel.GetVATChangeFillingSummary(item);

                    if (viewModel.vATChangingSummaryData != null)
                    {

                        viewModel.EnableSummaryView();

                    }

                }
            }
            catch (Exception)
            {


            }
        }

        private void Download_Acknowledgement(object sender, EventArgs e)
        {

            if (viewModel.vATChangingSummaryData.Fbnum != null)
            {

                String downloadurl = ZATCAConstants.downloadFile + viewModel.vATChangingSummaryData.Fbnum;
                viewModel._navigationService.NavigateTo(App.PdfView, downloadurl);
            }
        }
    }
}
