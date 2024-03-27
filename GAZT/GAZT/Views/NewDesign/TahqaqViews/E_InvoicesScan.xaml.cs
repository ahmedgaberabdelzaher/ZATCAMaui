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

            zxing.Options = new MobileBarcodeScanningOptions()
            {
                UseFrontCameraIfAvailable = false,
                PossibleFormats = new List<BarcodeFormat>()
                {
                    BarcodeFormat.AZTEC ,
                    BarcodeFormat.CODABAR ,
                    BarcodeFormat.CODE_39 ,
                    BarcodeFormat.CODE_93 ,
                    BarcodeFormat.CODE_128 ,
                    BarcodeFormat.DATA_MATRIX ,
                    BarcodeFormat.EAN_8,
                    BarcodeFormat.EAN_13 ,
                    BarcodeFormat.ITF,
                    BarcodeFormat.MAXICODE,
                    BarcodeFormat.PDF_417 ,
                    BarcodeFormat.QR_CODE ,
                    BarcodeFormat.RSS_14 ,
                    BarcodeFormat.RSS_EXPANDED ,
                    BarcodeFormat.UPC_A,
                    BarcodeFormat.UPC_E ,
                    BarcodeFormat.UPC_EAN_EXTENSION ,
                    BarcodeFormat.MSI,
                    BarcodeFormat.PLESSEY,
                    BarcodeFormat.IMB ,
                    BarcodeFormat.PHARMA_CODE ,
                    BarcodeFormat.All_1D,
                },
                TryHarder = true,
                AutoRotate = false,
                TryInverted = true,
                UseCode39ExtendedMode = true,
                UseNativeScanning = true,
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

            bool scanFinished = false;

            zxing.OnScanResult += (result) =>
                Device.BeginInvokeOnMainThread( () =>
                {
                    if (!scanFinished)
                    {
                        if (int.TryParse(result.Text, out int res))
                        {
                            return;
                        }
                        viewModel.IsScanning = false;
                        viewModel.scanCode = result.Text;
                        viewModel.ScanEnvoiceQrCommand.Execute(null);
                        zxing.IsScanning = false;
                        viewModel.IsScanning = false;
                        scanFinished = true;
                    }

                });

            InitializeComponent();
            MainGrid.Children.Add(zxing);
            zxing.AutoFocus();
           viewModel.ScanEnvoiceQrCommand.Execute(null);
        }

        protected override async void OnAppearing()
        {
            PermissionStatus granted = await Permissions.CheckStatusAsync<Permissions.Camera>();
            if (granted != PermissionStatus.Granted)
            {
                _ = await Permissions.RequestAsync<Permissions.Camera>();
            }
            base.OnAppearing();

        }

        protected override bool OnBackButtonPressed()
        {
            if (viewModel.IsShowRsltView)
            {

                viewModel.BackCommand.Execute(null);
                return false;
            }

            return base.OnBackButtonPressed();
        }
    }
}
