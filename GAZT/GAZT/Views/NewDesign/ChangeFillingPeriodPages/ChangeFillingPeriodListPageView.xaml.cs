using System;
using System.Threading.Tasks;
using EGAZT.Models.ChageFillingPeriodModel;
using EGAZT.ViewModel.NewDesignViewModel.ChangeFillingPeriodViewModel;
using GAZT.Helper;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

namespace EGAZT.Views.NewDesign.ChangeFillingPeriodPages
{
    [Preserve(AllMembers = true)]
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

                var safeInsets = On<iOS>().SafeAreaInsets();
                safeInsets.Bottom = -10;
                this.Padding = safeInsets;
                viewModel.ResetData();
                _ = viewModel.GetVATChangeFillingList();
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
                Filinglbl.HorizontalOptions = LayoutOptions.StartAndExpand;
                Filinglbl.HorizontalTextAlignment = TextAlignment.Start;
            }
            else
            {
                this.FlowDirection = FlowDirection.RightToLeft;
                Filinglbl.HorizontalOptions = LayoutOptions.EndAndExpand;
                Filinglbl.HorizontalTextAlignment = TextAlignment.Start;
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

        async void Request_Item_Tapped(System.Object sender, ItemTappedEventArgs e)
        {
            try { 
            var item = e.ItemData as VATChangeFillingListModel.ChangeFillingFrequency;
            if (item.Fbust == "E0018" || item.Fbust == "E0075" || item.Fbust == "E0074" || item.Fbust == "E0013")
            {
                App.selectedVatFillingItem = item.Fbnum;
                App.selectedVATItemFbust = item.Fbust;
                viewModel._navigationService.NavigateTo(App.ChangeFillingPeriodPageView);
            }
            else
            {

                try {

                    await viewModel.GetVATChangeFillingSummary(item);

                    if(viewModel.vATChangingSummaryData != null) {

                        viewModel.EnableSummaryView();

                    }

                }
                catch(Exception ex) {

                }


               
            }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }
        }

        private void Download_Acknowledgement(object sender, EventArgs e)
        {

            if (viewModel.vATChangingSummaryData.Fbnum != null)
            {

                String downloadurl = Constants.downloadFile + "'" + viewModel.vATChangingSummaryData.Fbnum + "')/$value";
                viewModel._navigationService.NavigateTo(App.PdfView, downloadurl);
            }
        }
    }
}
