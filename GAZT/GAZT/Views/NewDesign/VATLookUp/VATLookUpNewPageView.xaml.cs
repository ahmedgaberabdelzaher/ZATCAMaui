using EGAZT.ViewModel.NewDesignViewModel;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using GAZT.Models;
using System.Linq;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Application = Xamarin.Forms.Application;
using ZXing.Mobile;
using ZXing;
using System.Collections.Generic;
using Xamarin.Essentials;

namespace EGAZT.Views.NewDesign.VATLookUp
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VATLookUpNewPageView : ContentPage
    {
 
        VATLookUpNewPageViewModel viewModel;
        ZXingScannerView zxing;
        public VATLookUpNewPageView()
        {
            InitializeComponent();

            viewModel = App.Locator.VATLookUpNewPageView;
            this.BindingContext = viewModel;

            SetPickerFont();
            ChangeAeroIcon();
            SetLTR();
            //On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(false);
        
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(this, "");
            viewModel.ResetFormData();
            viewModel.IsTooltipEnableVisible = false;
            viewModel.TxtSearchParameter = string.Empty;
            viewModel.OnPageLoad();
            viewModel.MaxDigids = "15";
            viewModel.LookUpButtonText = AppResources.ZVATLookUpSearchButtonText;

            ZXingScannerPage scanPage;
            btnScan.Clicked += async (a,e) =>
            {
                viewModel.IsShowScanView = true;
                zxing.IsScanning = true;
                MainGrid.Children.Add(zxing);
                zxing.AutoFocus();
                /*scanPage = new ZXingScannerPage();
                scanPage.OnScanResult += (result) => {
                    scanPage.IsScanning = false;
                    Device.BeginInvokeOnMainThread(async () => {
                        MessagingCenter.Send(this, "ScanData", result.Text);
                        await Navigation.PopAsync();

                        viewModel.SelectedParameterType = viewModel.ParameterTypeList?.Where(x => x.id == "3")?.FirstOrDefault();
                        viewModel.LookupNumber = result.Text;
                        viewModel.getBarcodeData();


                    });
                };
                await Navigation.PushAsync(scanPage);*/
            };

            #region QR
            zxing = new ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                AutomationId = "zxingScannerView"
            };
            //zxing.AutoFocus();
            zxing.Options = new MobileBarcodeScanningOptions()
            {
                UseFrontCameraIfAvailable = false,
                PossibleFormats = new List<BarcodeFormat>() { BarcodeFormat.QR_CODE, BarcodeFormat.DATA_MATRIX, BarcodeFormat.EAN_13 },
                TryHarder = true,
                AutoRotate = false,
                TryInverted = true,
                UseCode39ExtendedMode = true,
                DelayBetweenContinuousScans = 0,
                CameraResolutionSelector = availableResolutions =>
                {
                    var displayOrientationHeight = DeviceDisplay.MainDisplayInfo.Orientation == DisplayOrientation.Portrait ? DeviceDisplay.MainDisplayInfo.Height : DeviceDisplay.MainDisplayInfo.Width;
                    var displayOrientationWidth = DeviceDisplay.MainDisplayInfo.Orientation == DisplayOrientation.Portrait ? DeviceDisplay.MainDisplayInfo.Width : DeviceDisplay.MainDisplayInfo.Height;

                    var targetRatio = displayOrientationHeight / displayOrientationWidth;
                    var targetHeight = displayOrientationHeight;

                    var bestResolutions = from r in availableResolutions
                                          let aspectRatio = (double)r.Width / r.Height
                                          let aspectRatioDiff = Math.Abs(aspectRatio - targetRatio)
                                          let heightDiff = Math.Abs(r.Height - targetHeight)
                                          orderby aspectRatioDiff, heightDiff
                                          select r;

                    return bestResolutions.FirstOrDefault();
                },
            };

            var options = new MobileBarcodeScanningOptions
            {
                AutoRotate = true,
                // UseNativeScanning = true,
                TryHarder = true,

                TryInverted = true,
                CameraResolutionSelector = availableResolutions =>
                {
                    var displayOrientationHeight = DeviceDisplay.MainDisplayInfo.Orientation == DisplayOrientation.Portrait ? DeviceDisplay.MainDisplayInfo.Height : DeviceDisplay.MainDisplayInfo.Width;
                    var displayOrientationWidth = DeviceDisplay.MainDisplayInfo.Orientation == DisplayOrientation.Portrait ? DeviceDisplay.MainDisplayInfo.Width : DeviceDisplay.MainDisplayInfo.Height;

                    var targetRatio = displayOrientationHeight / displayOrientationWidth;
                    var targetHeight = displayOrientationHeight;

                    var bestResolutions = from r in availableResolutions
                                          let aspectRatio = (double)r.Width / r.Height
                                          let aspectRatioDiff = Math.Abs(aspectRatio - targetRatio)
                                          let heightDiff = Math.Abs(r.Height - targetHeight)
                                          orderby aspectRatioDiff, heightDiff
                                          select r;

                    return bestResolutions.FirstOrDefault();
                },

            };
            zxing.OnScanResult += (result) =>
                Device.BeginInvokeOnMainThread(() =>
                {;
                    try
                    {
                   zxing.IsScanning = false;
                  //  MessagingCenter.Send(this, "ScanData", result.Text);
                    viewModel.SelectedParameterType = viewModel.ParameterTypeList?.Where(x => x.id == "3")?.FirstOrDefault();
                   // viewModel.LookupNumber = result.Text;
                    viewModel.getBarcodeData(result.Text);
                    }
                    catch (Exception ex)
                    {

                    }
                  

                });
            viewModel.IsShowScanView = false;
            #endregion

        }
        public void SetPickerFont()
        {
            try
            {
                switch (Xamarin.Forms.Device.RuntimePlatform)
                {

                    case Xamarin.Forms.Device.iOS:
                        {

                            PPicker.HeaderFontFamily = "Somar-SemiBold";
                            PPicker.ColumnHeaderFontFamily = "Somar-SemiBold";
                            PPicker.SelectedItemFontFamily = "Somar-SemiBold";
                            PPicker.UnSelectedItemFontFamily = "Somar-SemiBold";//ddlLIssuedBy
                        }
                        break;
                    case Xamarin.Forms.Device.Android:
                        PPicker.HeaderFontFamily = "GAZT_FONT_MEDIUM";//"Somar-SemiBold.otf#Somar-SemiBold";
                        PPicker.ColumnHeaderFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        PPicker.SelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";
                        PPicker.UnSelectedItemFontFamily = "GAZT_FONT_MEDIUM";// "Somar-SemiBold.otf#Somar-SemiBold";//ddlLIssuedBy
                        break;
                }
            }
            catch (Exception ex)
            {

            }

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            this.Padding = safeInsets;

            if (Device.RuntimePlatform == Device.Android)
            {
                PPicker.BackgroundColor =  (Color)Application.Current.Resources["PickerBgGray"];
            }
            else
            {
                PPicker.BackgroundColor =  (Color)Application.Current.Resources["White"];
            }
            
                        MessagingCenter.Send(this, "ScanData", "abc");
        }
        private void SetLTR()
        {
            if (App.IsArabic)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
            }
            else
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
        
        void PPicker_btn_Clicked(System.Object sender, System.EventArgs e)
        {
            //if (viewModel.IsNameVisible)
            //    return;
            viewModel.IsNameVisible = false;
            PPicker.IsOpen = true;
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            if (viewModel.SelectedParameterType != null)
            {
                //PopUp popUp = new PopUp();//SetPlaceholderText();
                //popUp.Message = viewModel.VATACCOrCRNOOrVATCER;
                //if (App.IsArabic)
                //{
                //    popUp.FlowDirections = "RightToLeft";
                //}
                //else
                //{
                //    popUp.FlowDirections = "LeftToRight";
                //}
                //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(viewModel.VATACCOrCRNOOrVATCER));

            }
        }

        void PPicker_SelectionChanged(System.Object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            VATParameterType vATParameterType = (VATParameterType)e.NewValue;
            PPicker.SelectedItem = vATParameterType;
            viewModel.SelectedParameterType = vATParameterType;
        }

        private void BorderlessEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
          viewModel.IsNameVisible = false);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel.onDissapear();
        }
    }
}
