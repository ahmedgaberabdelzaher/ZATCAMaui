using System;
using System.Collections.Generic;
using EGAZT.ViewModel.NewDesignViewModel.TahqaqViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using ZXing;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;
using System.Linq;
namespace EGAZT.Views.NewDesign.TahqaqViews
{
    public partial class E_InvoicesScan : ContentPage
    {
        TahqaqScanPageViewModel viewModel;
        ZXingScannerView zxing;
        public E_InvoicesScan()
        {
            viewModel = App.Locator.tahqaqScanPageViewModel;
            BindingContext = viewModel;
            zxing = new ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                AutomationId = "zxingScannerView",
              
            };
            zxing.SetBinding(ZXingScannerView.IsScanningProperty, new Binding("IsScanning"));
            zxing.SetBinding(ZXingScannerView.IsAnalyzingProperty, new Binding("IsScanning"));
            //zxing.AutoFocus();
            zxing.Options = new MobileBarcodeScanningOptions()
            {
                UseFrontCameraIfAvailable = false,
                PossibleFormats = new List<BarcodeFormat>() { BarcodeFormat.QR_CODE, BarcodeFormat.DATA_MATRIX, BarcodeFormat.EAN_13, BarcodeFormat.All_1D },
                TryHarder = true,
                AutoRotate = false,
                TryInverted = true,
                UseCode39ExtendedMode = true, UseNativeScanning = true, 
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
                Device.BeginInvokeOnMainThread(async () =>
                {
                    viewModel.IsScanning = false;
                 //   zxing.IsScanning = false;
                    // Stop analysis until we navigate away so we don't keep reading barcodes
                    // zxing.IsAnalyzing = false;
                    viewModel.scanCode = result.Text;
                    viewModel.ScanEnvoiceQrCommand.Execute(null);
                    // Show an alert
                    //  await DisplayAlert("Scanned Barcode", result.Text, "OK");

                    // Navigate away
                    // await Navigation.PopAsync();
                });

            InitializeComponent();
            MainGrid.Children.Add(zxing);
            zxing.AutoFocus();
          //  viewModel.ScanEnvoiceQrCommand.Execute(null);
           
        }
        protected override async void OnAppearing()
        {
           PermissionStatus granted = await Permissions.CheckStatusAsync<Permissions.Camera>();
             if (granted != PermissionStatus.Granted)
             {
                 _ = await Permissions.RequestAsync<Permissions.Camera>();
             }
           // zxing.IsScanning = viewModel.IsScanning;
            base.OnAppearing();
      
        }
    }
}
