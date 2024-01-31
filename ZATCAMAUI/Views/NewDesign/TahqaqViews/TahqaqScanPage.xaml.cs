using ZATCAMAUI.ViewModel.NewDesignViewModel.TahqaqViewModels;
using ZXing.Net.Maui;

namespace ZATCAMAUI.Views.NewDesign.TahqaqViews
{
    public partial class TahqaqScanPage : ContentPage
    {
        bool scanFinished = false;
        string barcodeResultValue;
        TahqaqScanPageViewModel viewModel;
        public TahqaqScanPage()
        {
            InitializeComponent();
            viewModel = App.Locator.tahqaqScanPageViewModel;
            BindingContext = viewModel;
            zxing.Options = new BarcodeReaderOptions()
            {
                Formats = BarcodeFormats.All,

                TryHarder = true,
                AutoRotate = false,
                TryInverted = true,

            };

           
        }
        protected override async void OnAppearing()
        {
            PermissionStatus granted = await Permissions.CheckStatusAsync<Permissions.Camera>();
            var permissionStatus = await Permissions.RequestAsync<Permissions.Camera>();

            base.OnAppearing();
        }

        private void zxing_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
        {
            try
            {
                foreach (var barcode in e.Results)
                {
                    Console.WriteLine($"Barcodes: {barcode.Format} -> {barcode.Value}");
                    barcodeResultValue = barcode.Value;
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
