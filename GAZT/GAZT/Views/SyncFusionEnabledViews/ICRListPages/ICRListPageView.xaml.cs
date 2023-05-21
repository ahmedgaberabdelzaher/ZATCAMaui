using EGAZT.ViewModel.SyncFusionEnabledViewModel.AttachmentPage_ViewModel;
using EGAZT.ViewModel.SyncFusionEnabledViewModel.ICRListPage_ViewModel;
using GAZT.Models;
using Syncfusion.SfPicker.XForms;
using System;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
namespace EGAZT.Views.SyncFusionEnabledViews.ICRList
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ICRListPageView : ContentPage
    {
        #region Variable
        ICRListPageViewModel viewModel;
        public static bool AreYouUsingFilterFirstTimeAfterComingFromVATReturnPage = false;
        int Count = 0;
        private double width = 0;
        private double height = 0;
        #endregion
        #region Property
        #endregion
        #region Constructor
        public ICRListPageView()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            viewModel = App.Locator.ICRListPageView;
            this.BindingContext = viewModel;
            ChangeAeroIcon();
            SetLTR();
            SetPickerFont();
            Count = 1;
            FrmLicenseIssuedBy.Margin = new Thickness(5, 0, 5, 5);
            App.ICRStatus = string.Empty;
            viewModel.IsICRListVisible = true;
            viewModel.IsNoDataLabelVisible = false;
            IntialiseAsync();
            AreYouUsingFilterFirstTimeAfterComingFromVATReturnPage = false;
            ICRList.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                // don't do anything if we just de-selected the row.
                if (e.Item == null) return;
                if (sender is Xamarin.Forms.ListView lv) lv.SelectedItem = null;
            };
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
            //BPicker
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
                if(App.IsArabic)
                {
                    if (width > height)
                    {
                        On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(false);
                        //ICRList.Margin = new Thickness(0, 5, 0, 0);
                        BPicker.Margin = new Thickness(8, 0, 8, 0);
                        FrmLicenseIssuedBy.Margin = new Thickness(8, 0, 8, 5);
                        ListLayout.Padding = new Thickness(40, 0, 40, 5);
                        // On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(false);
                        // var safeInsets = On<iOS>().SafeAreaInsets();
                        //ICRList.Margin = new Thickness(0, 5, 60, 0);
                        // BPicker.Margin = new Thickness(20, 0, 60, 0);
                        // FrmLicenseIssuedBy.Margin = new Thickness(20, 0, 80, 5);
                        //safeInsets.Left = 80;
                        //safeInsets.Right = 80;
                        //Padding = safeInsets;
                    }
                    else
                    {
                        On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                        //ICRList.Margin = new Thickness(0, 5, 0, 0);
                        BPicker.Margin = new Thickness(8, 0, 8, 0);
                        FrmLicenseIssuedBy.Margin = new Thickness(8, 0, 8, 5);
                        ListLayout.Padding = new Thickness(0, 0, 0, 5);
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
        public async Task IntialiseAsync()
        {
            try
            {
                    await viewModel.onPageLoad();
                    if (viewModel.ICRStatusList != null && viewModel.ICRStatusList.Count != 0)
                    {
                        BPicker.SelectedIndex = 14;
                    }
            }
            catch(Exception e)
            {
            }
        }
        private void SelectedICR(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                if (AreYouUsingFilterFirstTimeAfterComingFromVATReturnPage)
                {
                    ICRStatus selectedICR = (ICRStatus)e.NewValue;
                    viewModel.SelectedICRStatus = viewModel.ICRStatusList[viewModel.SelectedPickerIndex];// selectedICR;
                    viewModel.TxtSelectedStatus = selectedICR.Txt30;
                    string str = App.ICRStatus;
                    viewModel.SetICRListData(viewModel.PreviousSelectedICRStatus);
                    viewModel.ICRSelectedIndex = viewModel.SelectedPickerIndex;
                }
                else
                {
                    ICRStatus selectedICR = (ICRStatus)e.NewValue;
                    viewModel.SelectedICRStatus = selectedICR;
                    viewModel.TxtSelectedStatus = selectedICR.Txt30;
                    string str = App.ICRStatus;
                    viewModel.SetICRListData(selectedICR);
                }
            }
            catch(Exception ex)
            {
            }
        }
            public async Task IntialiseAsyncForPreviousSelectedFilter()
        {
            try
            {
                await viewModel.onPageLoad();
                if (viewModel.ICRStatusList != null && viewModel.ICRStatusList.Count != 0 && viewModel.PreviousSelectedICRStatus!=null)
                {
                    if (AreYouUsingFilterFirstTimeAfterComingFromVATReturnPage = false)
                    {
                        viewModel.SelectedICRStatus = viewModel.PreviousSelectedICRStatus;
                    }
                    if(AreYouUsingFilterFirstTimeAfterComingFromVATReturnPage = true)
                    {
                        AreYouUsingFilterFirstTimeAfterComingFromVATReturnPage = false;
                    }
                    // BPicker.SelectedIndex = indexofPreviousSelectedFilter;
                }
            }
            catch (Exception e)
            {
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }

        }



        protected async override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                //var safeInsets = On().SafeAreaInsets();
                //safeInsets.Left = 24;
                //this.Padding = safeInsets;
                AttachmentPageViewModel.AttachmentUploadedSize = 0;
                AttachmentPageViewModel.IsToBeFilled = true;
                AttachmentPageViewModel.attachmentSizeVisibility = false;
                if (Count != 1)
                {
                    if (!string.IsNullOrEmpty(App.ICRStatus))
                    {
                        if (viewModel.PreviousSelectedICRStatus != null)
                        {
                            await IntialiseAsyncForPreviousSelectedFilter();
                        }
                    }
                    else
                    {
                       await IntialiseAsync();
                    }
                }
                Count++;
            }
            catch(Exception ex)
            {
            }
        }
        private void onDropdownButtonClicked(object sender, EventArgs e)
        {
            BPicker.Focus();
        }
        private void BPickerButton_Clicked(object sender, EventArgs e)
        {
            BPicker.IsOpen = true;
        }
        private void BPicker_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                ICRStatus selectedfbtyp = (ICRStatus)e.NewValue;
                BPicker.SelectedItem = selectedfbtyp;
                viewModel.SelectedICRStatus = selectedfbtyp;
                viewModel.SelectedICRStatusPrev = selectedfbtyp;
                viewModel.TxtSelectedStatus = selectedfbtyp.Txt30;
            }
            catch(Exception ex)
            {

            }
          
        }
        private void BPicker_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                viewModel.SelectedICRStatus = viewModel.SelectedICRStatusPrev;
                BPicker.SelectedItem = viewModel.SelectedICRStatusPrev;
                if (viewModel.SelectedICRStatusPrev == null)
                {
                    viewModel.TxtSelectedStatus = string.Empty;
                }
            }
            catch(Exception ex)
            {

            }
          
        }
    }
}