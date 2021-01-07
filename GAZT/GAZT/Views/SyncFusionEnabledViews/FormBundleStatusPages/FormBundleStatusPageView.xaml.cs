using EGAZT.ViewModel.SyncFusionEnabledViewModel.FormBundleStatusPage_ViewModel;
using GAZT.Models;
using Syncfusion.SfPicker.XForms;
using System;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
namespace EGAZT.Views.SyncFusionEnabledViews.FormBundleStatus
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormBundleStatusPageView : ContentPage
    {
        FormBundleStatusPageViewModel viewModel;
        private double width = 0;
        private double height = 0;
        public FormBundleStatusPageView()
        {
            viewModel = App.Locator.FormBundleStatusPageView;
            InitializeComponent();
            ParentContainerForOTP.Padding = new Thickness(0, 0, 0, 0);
           
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
            this.BindingContext = viewModel;
            viewModel.ClearData();
            SetPickerFont();
            ChangeAeroIcon();
            OnPageLoad();
            //DDlIDType
            SetLTR();
        }
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
                                DDlIDType.HeaderFontFamily = "SSTArabic-Medium";
                                DDlIDType.ColumnHeaderFontFamily = "SSTArabic-Medium";
                                DDlIDType.SelectedItemFontFamily = "SSTArabic-Medium";
                                DDlIDType.UnSelectedItemFontFamily = "SSTArabic-Medium";//CPicker

                                CPicker.HeaderFontFamily = "SSTArabic-Medium";
                                CPicker.ColumnHeaderFontFamily = "SSTArabic-Medium";
                                CPicker.SelectedItemFontFamily = "SSTArabic-Medium";
                                CPicker.UnSelectedItemFontFamily = "SSTArabic-Medium";//CPicker
                            }
                            else
                            {
                                DDlIDType.HeaderFontFamily = "SSTArabic-Medium";
                                DDlIDType.ColumnHeaderFontFamily = "SSTArabic-Medium";
                                DDlIDType.SelectedItemFontFamily = "SSTArabic-Medium";
                                DDlIDType.UnSelectedItemFontFamily = "SSTArabic-Medium";//CPicker

                                CPicker.HeaderFontFamily = "SSTArabic-Medium";
                                CPicker.ColumnHeaderFontFamily = "SSTArabic-Medium";
                                CPicker.SelectedItemFontFamily = "SSTArabic-Medium";
                                CPicker.UnSelectedItemFontFamily = "SSTArabic-Medium";//CPicker
                            }
                        }
                        break;
                    case Xamarin.Forms.Device.Android:
                        DDlIDType.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        DDlIDType.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        DDlIDType.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        DDlIDType.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";//CPicker

                        CPicker.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        CPicker.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        CPicker.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";
                        CPicker.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "GE_SS_Two_Medium.ttf#GE_SS_Two_Medium";//CPicker
                        break;
                }
            }
            catch (Exception ex)
            {

            }

        }

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
                        ParentContainerForOTP.Padding = new Thickness(40, 0, 40, 0);
                    }
                    else
                    {
                        On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
                        ParentContainerForOTP.Padding = new Thickness(0, 0, 0, 0);
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
        public async void OnPageLoad()
        {
            Task.Run(() =>
            {
                viewModel.IsLoading = true;
            });
            await Task.Run(async () =>
            {
                await viewModel.onPageLoad();
            });
            Task.Run(() =>
            {
                viewModel.IsLoading = false;
            });
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            DDlIDType.IsOpen = true;
            try
            {
                viewModel.SelectedFormBindleFbtypCancel = (FormBundleResult)DDlIDType.SelectedItem;
            }
            catch (Exception ex)
            {
            }
            //BPicker.Focus();
        }
        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {if (viewModel.SelectedFormBindleFbtyp != null)
            {
               CPicker.IsOpen = true;
                //CPicker.Focus();
            }
        }
        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((Xamarin.Forms.ListView)sender).SelectedItem = null;
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
        private void DDlIDType_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            //EntryIDNumber.IsEnabled = true;
        }
        private void CPicker_SelectionChanged(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //your code here;
            viewModel.SelectedFormBindleFbnum = null;
            viewModel.SelectedFormBindleFbtyp = null;
            viewModel.TxtFBnum = string.Empty;
            viewModel.TxtFBtype = string.Empty;
        }
        private void CPicker_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {//SelectedFormBindleFbnum
            try
            {

                FormBundleApplicationNumberModelResult selectedfbnum = (FormBundleApplicationNumberModelResult)e.NewValue;
                CPicker.SelectedItem = selectedfbnum;//Fbnum
                viewModel.SelectedFormBindleFbnum = selectedfbnum;
                viewModel.SelectedFormBindleFbnumPrev = selectedfbnum;
                viewModel.TxtFBnum = selectedfbnum.Fbnum;
                //var item = sender as Picker;
                //var selectedItem = item.SelectedItem as FormBundleApplicationNumberModelResult;
                viewModel.populate();
            }
            catch (Exception ex)
            { 
            
            }
            }
        private void DDlIDType_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {try
            { 
            DDlIDType.SelectedItem = viewModel.SelectedFormBindleFbtypCancel;
            viewModel.SelectedFormBindleFbtyp = viewModel.SelectedFormBindleFbtypCancel;
            if (viewModel.SelectedFormBindleFbtypCancel == null)
            {
                viewModel.TxtFBtype = string.Empty;
            }
            }
            catch (Exception ex)
            {

            }
        }
        private void DDlIDType_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {//SelectedFormBindleFbtyp
            try
            {
                FormBundleResult selectedfbtyp = (FormBundleResult)e.NewValue;
                DDlIDType.SelectedItem = selectedfbtyp;
                viewModel.SelectedFormBindleFbnumPrev = null;
                viewModel.SelectedFormBindleFbtyp = selectedfbtyp;
                viewModel.TxtFBtype = selectedfbtyp.Txt50;

            }
            catch (Exception ex)
            { 
            
            }
        }
        private void CPicker_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            try
            {
                viewModel.SelectedFormBindleFbnum = viewModel.SelectedFormBindleFbnumPrev;
                CPicker.SelectedItem = viewModel.SelectedFormBindleFbnumPrev;
                if (viewModel.SelectedFormBindleFbnumPrev == null)
                {
                    viewModel.TxtFBnum = string.Empty;
                }
            }
            catch (Exception ex)
            { 
            
            }
}
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel.ClearData();
        }

    }
}