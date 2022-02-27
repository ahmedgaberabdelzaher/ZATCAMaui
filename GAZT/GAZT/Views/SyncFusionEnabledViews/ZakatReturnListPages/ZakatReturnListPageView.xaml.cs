using EGAZT.Models;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ZakatReturnListPage_ViewModel;
using Syncfusion.SfPicker.XForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
namespace EGAZT.Views.SyncFusionEnabledViews.ZakatReturnList
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZakatReturnListPageView : ContentPage
    {
        #region Variable
        ZakatReturnListPageViewModel viewModel;
        private double width = 0;
        private double height = 0;
        public static bool AreYouUsingFilterFirstTimeAfterComingFromZAKATDetailsPage = false;
        #endregion
        #region Property
        #endregion
        #region Constructor
        public ZakatReturnListPageView()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            viewModel = App.Locator.ZakatReturnListPageView;
            ZakatICRListLayout.Padding = new Thickness(10, 0, 10, 0);
            BPicker.Margin = new Thickness(10, 0, 10, 0);
            FrmLicenseIssuedBy.Margin = new Thickness(10, 0, 10, 5);
            ChangeAeroIcon();
            SetLTR();
            SetPickerFont();
            this.BindingContext = viewModel;
            viewModel.ClearData();
            viewModel.GetZAKATICRStatusList();
            viewModel.SelectedIndex = 13;
            viewModel.TxtSelectedStatus = viewModel.ICRStatusList[13].Value;
            viewModel.HandleNoDataMessageVisibility(viewModel.MyZakatReturns);
            ZakatICRList.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                // don't do anything if we just de-selected the row.
                if (e.Item == null) return;
                if (sender is Xamarin.Forms.ListView lv) lv.SelectedItem = null;
            };
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");

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
                        On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(false);
                        ZakatICRListLayout.Padding = new Thickness(40, 0, 40, 0);
                        BPicker.Margin = new Thickness(40, 0, 40, 0);
                        FrmLicenseIssuedBy.Margin = new Thickness(40, 0, 40, 5);
                        //safeInsets.Left = 80;
                        //safeInsets.Right = 80;
                        //Padding = safeInsets;
                    }
                    else
                    {
                        On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                        ZakatICRListLayout.Padding = new Thickness(10, 0, 10, 0);
                        //ZakatICRList.Margin = new Thickness(10, 5, 0, 0);
                        BPicker.Margin = new Thickness(10, 0, 10, 0);
                        FrmLicenseIssuedBy.Margin = new Thickness(10, 0, 10, 5);
                    }
                }
                //reconfigure layout
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
        private void SetLTR()
        {
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
                CultureInfo.CurrentUICulture = new CultureInfo("ar-AE");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                PickerResourceManager.Manager = new ResourceManager("EGAZT.SyncfusionControl", Xamarin.Forms.Application.Current.GetType().Assembly);
            }
            else
            {
                this.FlowDirection = FlowDirection.LeftToRight;
                CultureInfo.CurrentUICulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
                PickerResourceManager.Manager = new ResourceManager("GAZT.AppResources", Xamarin.Forms.Application.Current.GetType().Assembly);
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
                switch (Xamarin.Forms.Device.RuntimePlatform)
                {

                    case Xamarin.Forms.Device.iOS:
                        {
                            if (App.IsArabic)
                            {
                                BPicker.HeaderFontFamily = "Somar-SemiBold";
                                BPicker.ColumnHeaderFontFamily = "Somar-SemiBold";
                                BPicker.SelectedItemFontFamily = "Somar-SemiBold";
                                BPicker.UnSelectedItemFontFamily = "Somar-SemiBold";
                            }
                            else
                            {
                                BPicker.HeaderFontFamily = "Somar-SemiBold";
                                BPicker.ColumnHeaderFontFamily = "Somar-SemiBold";
                                BPicker.SelectedItemFontFamily = "Somar-SemiBold";
                                BPicker.UnSelectedItemFontFamily = "Somar-SemiBold";
                            }
                        }
                        break;
                    case Xamarin.Forms.Device.Android:
                        BPicker.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"Somar-SemiBold.otf#Somar-SemiBold";
                        BPicker.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        BPicker.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";

                        BPicker.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        break;
                }
            }
            catch (Exception )
            {
                //TODO BPicker would be null
            }

        }

        private void Bills_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((Xamarin.Forms.ListView)sender).SelectedItem = null;
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
        private void BPicker_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            ZAKATStatus selectedZakatStatus = (ZAKATStatus)e.NewValue;
            BPicker.SelectedItem = selectedZakatStatus;
            viewModel.SelectedICRStatus = selectedZakatStatus;
            viewModel.SelectedICRStatusPrev = selectedZakatStatus;
            viewModel.TxtSelectedStatus = selectedZakatStatus.Value;
        }
        private void ICRStatusChnaged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            //if(AreYouUsingFilterFirstTimeAfterComingFromZAKATDetailsPage)
            //{
            //    ZAKATStatus selectedICRStatus = (ZAKATStatus)e.NewValue;
            //    viewModel.SelectedICRStatus = selectedICRStatus;
            //    viewModel.TxtSelectedStatus = viewModel.PreviousSelectedICRStatus.Value;
            //    viewModel.GetFilteredZAKATICRList(viewModel.PreviousSelectedICRStatus);
            //      viewModel.PreviousSelectedICRStatus = selectedICRStatus;             
            //}
            //else
            //{
            if (viewModel.myZakatReturnsList != null)
            {
                try
                {
                    ZAKATStatus selectedICRStatus = (ZAKATStatus)e.NewValue;
                    viewModel.SelectedICRStatus = selectedICRStatus;
                    viewModel.TxtSelectedStatus = selectedICRStatus.Value;
                    viewModel.PreviousSelectedICRStatus = selectedICRStatus;
                    viewModel.GetFilteredZAKATICRList(selectedICRStatus);
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
            }
            //}
        }
        private void BPicker_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            viewModel.SelectedICRStatus = viewModel.SelectedICRStatusPrev;
            BPicker.SelectedItem = viewModel.SelectedICRStatusPrev;
            if (viewModel.SelectedICRStatusPrev == null)
            {
                viewModel.TxtSelectedStatus = string.Empty;
            }
        }
    }
}