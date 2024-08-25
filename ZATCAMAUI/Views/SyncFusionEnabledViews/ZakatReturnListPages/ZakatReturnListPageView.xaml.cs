
using Syncfusion.Maui.Picker;
using System.Globalization;
using System.Resources;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ZakatReturnListPage;
using ListView = Microsoft.Maui.Controls.ListView;

namespace ZATCAMAUI.Views.SyncFusionEnabledViews.ZakatReturnListPages
{
   
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatReturnListPageView : ContentPage
    {
        #region Variable
        ZakatReturnListPageViewModel viewModel;
        private double width = 0;
        private double height = 0;
        public static bool AreYouUsingFilterFirstTimeAfterComingFromZAKATDetailsPage = false;
        #endregion
        #region Constructor
        public ZakatReturnListPageView()
        {
            InitializeComponent();
            viewModel = App.Locator.ZakatReturnListPageView;
            ZakatICRListLayout.Padding = new Thickness(10, 0, 10, 0);
            BPicker.Margin = new Thickness(10, 0, 10, 0);
            FrmLicenseIssuedBy.Margin = new Thickness(10, 0, 10, 5);
            SetPickerFont();
            BindingContext = viewModel;
            viewModel.ClearData();
            viewModel.GetZAKATICRStatusList();
            viewModel.SelectedIndex = 13;
            viewModel.TxtSelectedStatus = viewModel.ICRStatusList[13].Value;
            viewModel.HandleNoDataMessageVisibility(viewModel.MyZakatReturns);
            ZakatICRList.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                // don't do anything if we just de-selected the row.
                if (e.Item == null) return;
                if (sender is ListView lv) lv.SelectedItem = null;
            };

        }
        #endregion
        #region Method
      
        private void onDropdownButtonClicked(object sender, EventArgs e)
        {
            BPicker.Focus();
        }
        #endregion
        public void SetPickerFont()
        {
            try
            {
                switch (DeviceInfo.Platform)
                {

                    case var _ when DeviceInfo.Current.Platform == DevicePlatform.iOS:
                        {
                            BPicker.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                                BPicker.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                                BPicker.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                                BPicker.TextStyle.FontFamily = "Somar-SemiBold";
                        }
                        break;
                    case var _ when DeviceInfo.Current.Platform == DevicePlatform.Android:
                        BPicker.HeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        BPicker.ColumnHeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        BPicker.SelectedTextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        BPicker.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        break;
                }
            }
            catch (Exception)
            {
                //TODO BPicker would be null
            }

        }

        private void Bills_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
            return;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();// called from here so List have the updated data after amendment or release
            viewModel.MyZakatReturns = new List<Models.EstimatedZakatReturnsResult>();
            viewModel.myZakatReturnsList = new List<Models.EstimatedZakatReturnsResult>();
            await viewModel.OnPageLoad();
        }
        private void BPickerButton_Clicked(object sender, EventArgs e)
        {
            BPicker.IsOpen = true;
        }
        private void BPicker_OkButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var picker = sender as SfPicker;
                ZAKATStatus selectedZakatStatus = viewModel.ICRStatusList[picker.Columns[0].SelectedIndex];
                //BPicker.SelectedItem = selectedZakatStatus;
                viewModel.SelectedICRStatus = selectedZakatStatus;
                viewModel.SelectedICRStatusPrev = selectedZakatStatus;
                viewModel.TxtSelectedStatus = selectedZakatStatus.Value;
            }
            catch (Exception)
            {


            }
        }
        private void ICRStatusChnaged(object sender, PickerSelectionChangedEventArgs e)
        {
           
            if (viewModel.myZakatReturnsList != null)
            {
                try
                {
                    ZAKATStatus selectedICRStatus = viewModel.ICRStatusList[e.NewValue];
                    viewModel.SelectedICRStatus = selectedICRStatus;
                    viewModel.TxtSelectedStatus = selectedICRStatus.Value;
                    viewModel.PreviousSelectedICRStatus = selectedICRStatus;
                    viewModel.GetFilteredZAKATICRList(selectedICRStatus);
                }
                catch (Exception)
                {


                }
            }
            else
            {
            }
        }
        private void BPicker_CancelButtonClicked(object sender, EventArgs e)
        {
            viewModel.SelectedICRStatus = viewModel.SelectedICRStatusPrev;
           // BPicker.SelectedItem = viewModel.SelectedICRStatusPrev;
            if (viewModel.SelectedICRStatusPrev == null)
            {
                viewModel.TxtSelectedStatus = string.Empty;
            }
        }
    }
}