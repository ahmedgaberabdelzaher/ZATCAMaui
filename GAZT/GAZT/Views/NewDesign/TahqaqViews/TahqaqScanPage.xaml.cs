using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using EGAZT.ViewModel.NewDesignViewModel.TahqaqViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using ZXing;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace EGAZT.Views.NewDesign.TahqaqViews
{
    public partial class TahqaqScanPage : ContentPage
    {
        TahqaqScanPageViewModel viewModel;
        ZXingScannerView zxing;
        public TahqaqScanPage()
        {
            viewModel = App.Locator.tahqaqScanPageViewModel;
            BindingContext = viewModel;
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
                TryInverted = true, UseCode39ExtendedMode = true,
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
                CameraResolutionSelector  = availableResolutions =>
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
                Device.BeginInvokeOnMainThread(async () =>
                {

                    // Stop analysis until we navigate away so we don't keep reading barcodes
                    // zxing.IsAnalyzing = false;
                    viewModel.scanCode = result.Text;
                    await viewModel.CheckQr();
                    // Show an alert
                  //  await DisplayAlert("Scanned Barcode", result.Text, "OK");

                    // Navigate away
                   // await Navigation.PopAsync();
                });
          
            InitializeComponent();
            MainGrid.Children.Add(zxing);
        
            /*var options = new MobileBarcodeScanningOptions
            {
                CameraResolutionSelector = SelectLowestResolutionMatchingDisplayAspectRatio

            };*/
            zxing.AutoFocus();
            //zxing = new ZXingScannerView();
            // zxing.Options = options;
          /*  zxing.OnScanResult += (result) => Device.BeginInvokeOnMainThread(() => {
                Debug.WriteLine(result.Text);
            });*/
        }
        protected override async void OnAppearing()
        {
            PermissionStatus granted = await Permissions.CheckStatusAsync<Permissions.Camera>();
            var permissionStatus = await Permissions.RequestAsync<Permissions.Camera>();

            /*if (granted != PermissionStatus.Granted)
            {
                _ = await Permissions.RequestAsync<Permissions.Camera>();
            }*/
            /* var options = new MobileBarcodeScanningOptions
             {
                 AutoRotate = true,
                // UseNativeScanning = true,
                 TryHarder = true,

                 TryInverted = true,
                 CameraResolutionSelector= SelectLowestResolutionMatchingDisplayAspectRatio

             };
             zxing.Options = options;*/
            /*  var  scanPage = new ZXingScannerPage();
                 scanPage.OnScanResult += (result) =>
                 {
                     scanPage.IsScanning = false;

                     Device.BeginInvokeOnMainThread(async () =>
                     {
                         await Navigation.PopAsync();
                         await DisplayAlert("Scanned Barcode", result.Text, "OK");
                     });
                 };
            
                 await Navigation.PushAsync(scanPage);
             */
            zxing.IsScanning = true;
            base.OnAppearing();
          // await viewModel.CheckQr()
;           // zxing.IsScanning = true;
        }/*
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            zxing.IsScanning = false;
        }*/

        private CameraResolution SelectLowestResolutionMatchingDisplayAspectRatio(List<CameraResolution> availableResolutions)
        {
            var displayOrientationHeight = DeviceDisplay.MainDisplayInfo.Orientation == DisplayOrientation.Portrait ?
            DeviceDisplay.MainDisplayInfo.Height : DeviceDisplay.MainDisplayInfo.Width;
            var displayOrientationWidth = DeviceDisplay.MainDisplayInfo.Orientation == DisplayOrientation.Portrait ?
            DeviceDisplay.MainDisplayInfo.Width : DeviceDisplay.MainDisplayInfo.Height;

            var targetRatio = displayOrientationHeight / displayOrientationWidth;
            var targetHeight = displayOrientationHeight;

            //camera API lists all available resolutions from highest to lowest, perfect for us
            //making use of this sorting, following code runs some comparisons to select the lowest resolution that matches the 
           // screen aspect ratio and lies within tolerance
            //selecting the lowest makes Qr detection actual faster most of the time

            var bestResolutions = from r in availableResolutions
                                  let aspectRatio = (double)r.Width / r.Height
                                  let aspectRatioDiff = Math.Abs(aspectRatio - targetRatio)
                                  let heightDiff = Math.Abs(r.Height - targetHeight)
                                  orderby aspectRatioDiff, heightDiff
                                  select r;

            return bestResolutions.FirstOrDefault();
        }
    }
}
