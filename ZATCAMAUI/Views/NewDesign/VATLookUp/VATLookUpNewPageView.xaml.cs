
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using RGPopup.Maui.Services;
using Syncfusion.Maui.Picker;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZXing.Net.Maui;
using Application = Microsoft.Maui.Controls.Application;
using NavigationPage = Microsoft.Maui.Controls.NavigationPage;

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
            ChangeAeroIcon();
            SetLTR();
            //On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(false);

            NavigationPage.SetBackButtonTitle(this, "");
            viewModel.ResetFormData();
            viewModel.IsTooltipEnableVisible = false;
            viewModel.TxtSearchParameter = string.Empty;
            viewModel.OnPageLoad();
            viewModel.MaxDigids = "15";
            viewModel.LookUpButtonText = AppResources.ZVATLookUpSearchButtonText;

            btnScan.Clicked += async (a, e) =>
            {
                viewModel.IsShowScanView = true;
                MainGrid.Children.Add(zxing);
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
            base.OnAppearing();
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            Padding = safeInsets;

            if (Device.RuntimePlatform == Device.Android)
            {
                PPicker.Background = (Color)Application.Current.Resources["PickerBgGray"];
            }
            else
            {
                PPicker.Background = (Color)Application.Current.Resources["White"];
            }

            MessagingCenter.Send(this, "ScanData", "abc");
        }
        private void SetLTR()
        {
            if (App.IsArabic)
            {
                FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                FlowDirection = FlowDirection.LeftToRight;
            }
        }

        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["StyleReverseBack"] = Microsoft.Maui.Controls.Application.Current.Resources["ReverseBack"];
            }
            else
            {
                Resources["StyleReverseBack"] = Microsoft.Maui.Controls.Application.Current.Resources["Back"];
            }
        }

        void PPicker_btn_Clicked(object sender, EventArgs e)
        {
            //if (viewModel.IsNameVisible)
            //    return;
            viewModel.IsNameVisible = false;
            PPicker.IsOpen = true;
        }

        void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (viewModel.SelectedParameterType != null)
            {
               
                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(viewModel.VATACCOrCRNOOrVATCER));

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
                    foreach (var barcode in e.Results)
                    {
                        Console.WriteLine($"Barcodes: {barcode.Format} -> {barcode.Value}");
                        barcodeResultValue = barcode.Value;
                    }
                    //  MessagingCenter.Send(this, "ScanData", result.Text);
                    viewModel.SelectedParameterType = viewModel.ParameterTypeList?.Where(x => x.id == "3")?.FirstOrDefault();
                    // viewModel.LookupNumber = result.Text;
                    viewModel.getBarcodeData(barcodeResultValue);
                }
                catch (Exception)
                {

                }


            });
        }
    }
}
