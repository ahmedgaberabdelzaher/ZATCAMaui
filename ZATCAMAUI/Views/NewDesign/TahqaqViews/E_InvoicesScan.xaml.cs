using Camera.MAUI;
using Camera.MAUI.ZXing;
using ZATCAMAUI.ViewModel.NewDesignViewModel.TahqaqViewModels;

namespace ZATCAMAUI.Views.NewDesign.TahqaqViews
{
    public partial class E_InvoicesScan : ContentPage
    {
        bool scanFinished = false;
        string barcodeResultValue;
        TahqaqScanPageViewModel viewModel;
        public E_InvoicesScan()
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

        //private void zxing_BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
        //{
        //    try
        //    {
        //        MainThread.BeginInvokeOnMainThread(() =>
        //        {

        //            if (!scanFinished)
        //            {
        //                zxing.IsDetecting = false;
        //                foreach (var barcode in e.Results)
        //                {
        //                    barcodeResultValue = barcode.Value;
        //                }


        //                if (int.TryParse(barcodeResultValue, out int res))
        //                {
        //                    return;
        //                }
        //                viewModel.IsScanning = false;
        //                viewModel.scanCode = barcodeResultValue;
        //                viewModel.ScanEnvoiceQrCommand.Execute(null);
        //                viewModel.IsScanning = false;
        //                scanFinished = true;
        //            }

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
                MainThread.BeginInvokeOnMainThread(() =>
                {

                    if (!scanFinished)
                    {
                        //zxing.IsDetecting = false;
                        foreach (var barcode in args.Result)
                        {
                            barcodeResultValue = barcode.Text;
                        }


                        if (int.TryParse(barcodeResultValue, out int res))
                        {
                            return;
                        }
                        viewModel.IsScanning = false;
                        viewModel.scanCode = barcodeResultValue;
                        viewModel.ScanEnvoiceQrCommand.Execute(null);
                        viewModel.IsScanning = false;
                        scanFinished = true;
                    }

                });
            }
            catch (Exception)
            {
            }


        }
    }
}
