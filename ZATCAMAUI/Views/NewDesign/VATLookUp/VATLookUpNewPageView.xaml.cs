using Mopups.Services;
using Syncfusion.Maui.Picker;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZXing.Net.Maui;

namespace ZATCAMAUI.Views.NewDesign.VATLookUp
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VATLookUpNewPageView : ContentPage
    {
        string barcodeResultValue;
        VATLookUpNewPageViewModel viewModel;
        public VATLookUpNewPageView()
        {
            InitializeComponent();

            viewModel = App.Locator.VATLookUpNewPageView;
            BindingContext = viewModel;
            SetPickerFont();
            viewModel.ResetFormData();
            viewModel.IsTooltipEnableVisible = false;
            viewModel.TxtSearchParameter = string.Empty;
            viewModel.OnPageLoad();
            viewModel.MaxDigids = "15";
            viewModel.LookUpButtonText = AppResources.ZVATLookUpSearchButtonText;

            btnScan.Clicked += (a, e) =>
            {
                viewModel.IsShowScanView = true;
                zxing.IsDetecting = true;
                zxing.AutoFocus();
            };

            #region QR
            zxing.Options = new BarcodeReaderOptions()
            {
                Formats = BarcodeFormats.All,

                TryHarder = true,
                AutoRotate = false,
                TryInverted = true,

            };
            viewModel.IsShowScanView = false;
            #endregion

        }
        public void SetPickerFont()
        {
            try
            {
                switch (Device.RuntimePlatform)
                {

                    case Device.iOS:
                        {
                            PPicker.HeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            PPicker.ColumnHeaderView.TextStyle.FontFamily = "Somar-SemiBold";
                            PPicker.SelectedTextStyle.FontFamily = "Somar-SemiBold";
                            PPicker.TextStyle.FontFamily = "Somar-SemiBold";

                        }
                        break;
                    case Device.Android:
                        {
                            PPicker.HeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            PPicker.ColumnHeaderView.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            PPicker.SelectedTextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                            PPicker.TextStyle.FontFamily = "GAZT_FONT_MEDIUM";
                        }
                        break;
                }
            }
            catch (Exception)
            {

            }

        }

        protected override void OnAppearing()
        {

            MessagingCenter.Send(this, "ScanData", "abc");
        }


        void PPicker_btn_Clicked(object sender, TappedEventArgs e)
        {
            viewModel.IsNameVisible = false;
            PPicker.IsOpen = true;
        }

        void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (viewModel.SelectedParameterType != null)
            {
               
                MopupService.Instance.PushAsync(new AttachmentInformationPopUp(viewModel.VATACCOrCRNOOrVATCER));

            }
        }

        void PPicker_SelectionChanged(object sender, PickerSelectionChangedEventArgs e)
        {
            VATParameterType vATParameterType =viewModel.ParameterTypeList[e.NewValue];
            //PPicker.SelectedItem = vATParameterType;
            viewModel.SelectedParameterType = vATParameterType;
        }

        private void BorderlessEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
          viewModel.IsNameVisible = false);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel.onDissapear();
        }

        private void zxing_BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    zxing.IsDetecting = false;
                    foreach (var barcode in e.Results)
                    {
                        barcodeResultValue = barcode.Value;
                    }

                    viewModel.SelectedParameterType = viewModel.ParameterTypeList?.Where(x => x.id == "3")?.FirstOrDefault();
                    viewModel.getBarcodeData(barcodeResultValue);
                }
                catch (Exception)
                {

                }


            });
        }

    }
}
