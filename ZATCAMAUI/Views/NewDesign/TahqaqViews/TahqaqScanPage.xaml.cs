using Camera.MAUI;
using Camera.MAUI.ZXing;
using ZATCAMAUI.ViewModel.NewDesignViewModel.TahqaqViewModels;

namespace ZATCAMAUI.Views.NewDesign.TahqaqViews
{
    public partial class TahqaqScanPage : ContentPage
    {
        string barcodeResultValue;
        TahqaqScanPageViewModel viewModel;
        public TahqaqScanPage()
        {
            InitializeComponent();
            viewModel = App.Locator.tahqaqScanPageViewModel;
            BindingContext = viewModel;
            cameraView.BarCodeDecoder = new ZXingBarcodeDecoder();

            //zxing.Options = new BarcodeReaderOptions()
            //{
            //    Formats = BarcodeFormats.All,
            //    TryHarder = true,
            //    AutoRotate = false,

            //};

            cameraView.BarCodeOptions = new BarcodeDecodeOptions
            {
                AutoRotate = true,
                PossibleFormats = { Camera.MAUI.BarcodeFormat.QR_CODE },
                ReadMultipleCodes = false,
                TryHarder = true,
                TryInverted = true
            };

        }
        protected override async void OnAppearing()
        {
            PermissionStatus granted = await Permissions.CheckStatusAsync<Permissions.Camera>();
            var permissionStatus = await Permissions.RequestAsync<Permissions.Camera>();

            base.OnAppearing();
        }

        //private void zxing_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
        //{
        //    try
        //    {
        //        zxing.IsDetecting = false;
        //        foreach (var barcode in e.Results)
        //        {
        //            barcodeResultValue = barcode.Value;
        //        }
        //        MainThread.BeginInvokeOnMainThread(async () =>
        //        {
        //            viewModel.scanCode = barcodeResultValue;
        //            await viewModel.CheckQr();
        //        });
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}
        void cameraView_CamerasLoaded(System.Object sender, System.EventArgs e)
        {
            if (cameraView.Cameras.Count > 0)
            {
                cameraView.Camera = cameraView.Cameras.First();
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await cameraView.StopCameraAsync();
                    await cameraView.StartCameraAsync();
                });
            }
        }

        void cameraView_BarcodeDetected(System.Object sender, Camera.MAUI.ZXingHelper.BarcodeEventArgs args)
        {
            try
            {
                foreach (var barcode in args.Result)
                {
                    barcodeResultValue = barcode.Text;
                }
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    viewModel.scanCode = barcodeResultValue;
                    await viewModel.CheckQr();
                });
            }
            catch (Exception)
            {
            }
        }
    }
}
