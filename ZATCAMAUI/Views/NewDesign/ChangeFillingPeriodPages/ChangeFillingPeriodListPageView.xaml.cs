using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Models.ChageFillingPeriodModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ChangeFillingPeriodViewModel;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;

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

                NavigationPage.SetBackButtonTitle(this, "");
                ChangeAeroIcon();
                On<iOS>().SetUseSafeArea(true);
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

                var safeInsets = On<iOS>().SafeAreaInsets();
                safeInsets.Bottom = -10;
                Padding = safeInsets;
                viewModel.ResetData();
                _ = viewModel.GetVATChangeFillingList();
            }
            catch (Exception)
            {

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

                    try
                    {

                        await viewModel.GetVATChangeFillingSummary(item);

                        if (viewModel.vATChangingSummaryData != null)
                        {

                            viewModel.EnableSummaryView();

                        }

                    }
                    catch (Exception)
                    {

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

                string downloadurl = ZATCAConstants.downloadFile + "'" + viewModel.vATChangingSummaryData.Fbnum + "')/$value";
                viewModel._navigationService.NavigateTo(App.PdfView, downloadurl);
            }
        }
    }
}
