using ZATCAMAUI.ViewModel.NewDesignViewModel.TahqaqViewModels;
using ZXing.Net.Maui;

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
           

            zxing.Options = new BarcodeReaderOptions()
            {
                Formats = BarcodeFormats.All,
                TryHarder = true,
                AutoRotate = false,
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

        private void zxing_BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
        {
            try
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    
                    if (!scanFinished)
                    {
                        zxing.IsDetecting = false;
                        foreach (var barcode in e.Results)
                        {
                            barcodeResultValue = barcode.Value;
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
