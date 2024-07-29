using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Mopups.Services;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using ZATCAMAUI.ViewModel.NewDesignViewModel.ZAKATObjectionPages;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

namespace ZATCAMAUI.Views.NewDesign.MyBillsPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class MyBillsSadadDetailsPageView : ContentPage
    {
        GAZTNewDesignMyBillsPageViewModel viewModel;
        GAZTNewDesignDashBoardPageViewModel _dashBoardPageViewModel;
        public VATReturnSuccessfullPageViewModel _VatReturnSuccessPageViewModel;
        ZakatReturnDetailsSuccessfullPageViewModel _ZakatReturnSuccessPageViewModel;
        ZakatReturnDetailsD _zakatReturnDetail;
        private int screenIndex;

        public MyBillsSadadDetailsPageView(int screenIndex)
        {
            InitializeComponent();
            this.screenIndex = screenIndex;


            if (screenIndex == 0)
            {

                _dashBoardPageViewModel = App.Locator.GAZTNewDesignDashBoardPageView;
                _dashBoardPageViewModel.SadadBindNumber = _dashBoardPageViewModel.selectedSadadNo;
                _dashBoardPageViewModel.TotalAmount = _dashBoardPageViewModel.selectedAmount;
                BindingContext = _dashBoardPageViewModel;
            }
            else if (screenIndex == 1)
            {

                viewModel = App.Locator.GAZTNewDesignMyBillsPageView;
                viewModel.SadadBindNumber = viewModel.selectedSadadNo;
                viewModel.TotalAmount = viewModel.selectedAmount;
                BindingContext = viewModel;
            }
            ChangeAeroIcon();
            On<iOS>().SetUseSafeArea(true);

        }

        public MyBillsSadadDetailsPageView(VATDeclaration vATDeclaration)
        {
            try
            {
                InitializeComponent();
                _VatReturnSuccessPageViewModel = App.Locator.VATReturnSuccessfullPageView;
                screenIndex = 2;
                BindingContext = _VatReturnSuccessPageViewModel;
                ChangeAeroIcon();
                On<iOS>().SetUseSafeArea(true);
                if (vATDeclaration != null && vATDeclaration.d != null)
                {
                    _VatReturnSuccessPageViewModel.SadadNumber = string.Empty;
                    _VatReturnSuccessPageViewModel.IsSadadNumberVisible = false;
                    _VatReturnSuccessPageViewModel.IsButtonVisible = false;
                    _VatReturnSuccessPageViewModel.IsAcknowledgementButtonVisible = false;
                    _VatReturnSuccessPageViewModel.IsCreditCarriedTextVisible = false;
                    _VatReturnSuccessPageViewModel.VATDeclarationData = vATDeclaration;
                    _VatReturnSuccessPageViewModel.ReturnReferenceNumber = vATDeclaration.d.Fbnum;
                    _VatReturnSuccessPageViewModel.TaxablePeriod = vATDeclaration.d.Perslt;

                  

                    if (App.ICRStatus == "E0045" && _VatReturnSuccessPageViewModel.VATDeclarationData.d.RefundFg != "1")
                    {
                        if (Convert.ToDouble(_VatReturnSuccessPageViewModel.VATDeclarationData.d.NetdueVat) <= 0)
                        {
                            _VatReturnSuccessPageViewModel.IsSadadNumberVisible = false;
                            _VatReturnSuccessPageViewModel.IsRefreshButtonVisible = false;
                            _VatReturnSuccessPageViewModel.IsButtonVisible = true;
                            if (vATDeclaration.d.EstimatedFg == "X")
                            {
                                _VatReturnSuccessPageViewModel.IsAcknowledgementButtonVisible = false;
                            }
                            else
                            {
                                _VatReturnSuccessPageViewModel.IsAcknowledgementButtonVisible = true;
                            }
                        }
                        else
                        {
                            RefreshForSadad();
                        }
                    }
                    else
                    {
                        if (App.ICRStatus == "E0006" && Convert.ToDouble(_VatReturnSuccessPageViewModel.VATDeclarationData.d.NetdueVat) <= 0 || (App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057") && Convert.ToDouble(_VatReturnSuccessPageViewModel.VATDeclarationData.d.NetdueVat) <= 0 || App.ICRStatus == "E0055" && Convert.ToDouble(_VatReturnSuccessPageViewModel.VATDeclarationData.d.NetdueVat) <= 0)
                        {
                            _VatReturnSuccessPageViewModel.IsSadadNumberVisible = false;
                            _VatReturnSuccessPageViewModel.IsRefreshButtonVisible = false;
                            _VatReturnSuccessPageViewModel.IsButtonVisible = true;
                            _VatReturnSuccessPageViewModel.IsCreditCarriedTextVisible = true;
                            if (vATDeclaration.d.EstimatedFg == "X")
                            {
                                _VatReturnSuccessPageViewModel.IsAcknowledgementButtonVisible = false;
                            }
                            else
                            {
                                _VatReturnSuccessPageViewModel.IsAcknowledgementButtonVisible = true;
                            }
                        }
                        else
                        {
                            if (App.ICRStatus == "E0006" && Convert.ToDouble(_VatReturnSuccessPageViewModel.VATDeclarationData.d.NetdueVat) > 0 || App.ICRStatus == "E0056" && Convert.ToDouble(_VatReturnSuccessPageViewModel.VATDeclarationData.d.NetdueVat) > 0 || App.ICRStatus == "E0001" && Convert.ToDouble(_VatReturnSuccessPageViewModel.VATDeclarationData.d.NetdueVat) > 0 || App.ICRStatus == "E0013" && Convert.ToDouble(_VatReturnSuccessPageViewModel.VATDeclarationData.d.NetdueVat) > 0)
                            {
                                RefreshForSadad();
                            }
                            else
                            {
                                _VatReturnSuccessPageViewModel.IsRefreshButtonVisible = true;
                            }
                        }
                    }
                    if (_VatReturnSuccessPageViewModel.VATDeclarationData.d.RefundFg == "1")
                    {
                        _VatReturnSuccessPageViewModel.IsSadadNumberVisible = false;
                        _VatReturnSuccessPageViewModel.IsRefreshButtonVisible = false;
                        _VatReturnSuccessPageViewModel.IsButtonVisible = true;
                        if (vATDeclaration.d.EstimatedFg == "X")
                        {
                            _VatReturnSuccessPageViewModel.IsAcknowledgementButtonVisible = false;
                        }
                        else
                        {
                            _VatReturnSuccessPageViewModel.IsAcknowledgementButtonVisible = true;
                        }
                    }


                }
            }
            catch (Exception)
            {

            }
        }

        public MyBillsSadadDetailsPageView(ZakatReturnDetailsD ZakatReturnDetail)
        {
            InitializeComponent();
            _ZakatReturnSuccessPageViewModel = App.Locator.ZakatReturnDetailsSuccessfullPageView;
            screenIndex = 3;
            // Xamarin.Forms.NavigationPage.SetHasBackButton(this, false);
            _zakatReturnDetail = ZakatReturnDetail;
            On<iOS>().SetUseSafeArea(true);
            BindingContext = _ZakatReturnSuccessPageViewModel;
            ChangeAeroIcon();

            _ = _ZakatReturnSuccessPageViewModel.OnPageLoad(ZakatReturnDetail);


        }

        public async void RefreshForSadad()
        {
            try
            {
                await Task.Run(() =>
                {
                    _VatReturnSuccessPageViewModel.IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    await _VatReturnSuccessPageViewModel.OnRefreshClick();
                });
                await Task.Run(() =>
                {
                    _VatReturnSuccessPageViewModel.IsLoading = false;
                });
            }
            catch (Exception)
            {
                await Task.Run(() =>
                {
                    _VatReturnSuccessPageViewModel.IsLoading = false;
                });



            }
        }

        private async void OnCopySADADNumberButtonClicked(object sender, EventArgs e)
        {
            if (screenIndex == 0)
            {
                await Clipboard.SetTextAsync(_dashBoardPageViewModel.SadadBindNumber);
            }
            else if (screenIndex == 1)
            {
                await Clipboard.SetTextAsync(viewModel.SadadBindNumber);
            }
            else if (screenIndex == 2)
            {
                await Clipboard.SetTextAsync(_VatReturnSuccessPageViewModel.SadadBindNumber);
            }
            else if (screenIndex == 3)
            {
                await Clipboard.SetTextAsync(_ZakatReturnSuccessPageViewModel.SadadBindNumber);
            }

            if (Clipboard.HasText)
            {
                var text = await Clipboard.GetTextAsync();
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.MyBillsSADADPaymentNumberCopy + " " + text));
                // await viewModel._dialogService.ShowMessageBox(AppResources.ZSadadInvoiceNumber + " " + text, AppResources.Copied);
            }
        }

        private void DoneClicked(object sender, EventArgs e)
        {
            if (screenIndex == 0)
            {
                _dashBoardPageViewModel._navigationService.GoBack();
            }
            else if (screenIndex == 1)
            {
                viewModel._navigationService.GoBack();
            }
            else if (screenIndex == 2)
            {
                _VatReturnSuccessPageViewModel._navigationService.GoBack();
            }
            else if (screenIndex == 3)
            {
                _ZakatReturnSuccessPageViewModel._navigationService.GoBack();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = -10;
            Padding = safeInsets;

        }
        public void ChangeAeroIcon()
        {
            if (App.IsArabic)
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForArabicStyle"];
            }
            else
            {
                Resources["BackButtonArrow"] = Resources["ArrowImageForEnglishStyle"];
            }
        }

    }
}