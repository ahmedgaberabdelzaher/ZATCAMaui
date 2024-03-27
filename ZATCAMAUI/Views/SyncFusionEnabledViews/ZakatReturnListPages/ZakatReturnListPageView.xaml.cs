using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Syncfusion.Maui.Picker;
using System.Globalization;
using System.Resources;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ZakatReturnListPage;
using Application = Microsoft.Maui.Controls.Application;
using ListView = Microsoft.Maui.Controls.ListView;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;

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
            On<iOS>().SetUseSafeArea(true);
            viewModel = App.Locator.ZakatReturnListPageView;
            ZakatICRListLayout.Padding = new Thickness(10, 0, 10, 0);
            BPicker.Margin = new Thickness(10, 0, 10, 0);
            FrmLicenseIssuedBy.Margin = new Thickness(10, 0, 10, 5);
            ChangeAeroIcon();
            SetLTR();
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
            NavigationPage.SetBackButtonTitle(this, "");

        }
        #endregion
        #region Method
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height); //must be called
            if (this.width != width || this.height != height)
            {
                this.width = width;
                this.height = height;
                if (App.IsArabic)
                {
                    if (width > height)
                    {
                        On<iOS>().SetUseSafeArea(false);
                        ZakatICRListLayout.Padding = new Thickness(40, 0, 40, 0);
                        BPicker.Margin = new Thickness(40, 0, 40, 0);
                        FrmLicenseIssuedBy.Margin = new Thickness(40, 0, 40, 5);
                    }
                    else
                    {
                        On<iOS>().SetUseSafeArea(true);
                        ZakatICRListLayout.Padding = new Thickness(10, 0, 10, 0);
                        BPicker.Margin = new Thickness(10, 0, 10, 0);
                        FrmLicenseIssuedBy.Margin = new Thickness(10, 0, 10, 5);
                    }
                }
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
        private void SetLTR()
        {
            if (App.IsArabic)
            {
                //FlowDirection = FlowDirection.RightToLeft;
                CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                SfPickerResources.ResourceManager = new ResourceManager("ZATCAMAUI.SyncfusionControl", Application.Current.GetType().Assembly);
            }
            else
            {
                //FlowDirection = FlowDirection.LeftToRight;
                CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                SfPickerResources.ResourceManager = new ResourceManager("ZATCAMAUI.AppResources", Application.Current.GetType().Assembly);
            }
        }
        private void onDropdownButtonClicked(object sender, EventArgs e)
        {
            BPicker.Focus();
        }
        #endregion
        public void SetPickerFont()
        {
            try
            {
                switch (Device.RuntimePlatform)
                {

                    case Device.iOS:
                        {
                            BPicker.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                                BPicker.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                                BPicker.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                                BPicker.TextStyle.FontFamily = "Somar-SemiBold";
                        }
                        break;
                    case Device.Android:
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