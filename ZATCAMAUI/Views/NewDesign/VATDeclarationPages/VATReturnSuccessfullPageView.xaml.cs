
using Mopups.Services;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using ZATCAMAUI.Views.NewDesign.PaymentOptions;
using Page = Microsoft.Maui.Controls.Page;

namespace ZATCAMAUI.Views.NewDesign.VATDeclarationPages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VATReturnSuccessfullPageView : ContentPage
    {
        #region Variable
        public VATReturnSuccessfullPageViewModel viewModel;
        #endregion
        public VATReturnSuccessfullPageView(VATDeclaration vATDeclaration)
        {
            try
            {
                InitializeComponent();
                viewModel = App.Locator.VATReturnSuccessfullPageView;
                BindingContext = viewModel;
                if (vATDeclaration != null && vATDeclaration.data != null)
                {
                    viewModel.SadadNumber = string.Empty;
                    viewModel.IsSadadNumberVisible = false;
                    viewModel.IsButtonVisible = false;
                    viewModel.IsAcknowledgementButtonVisible = false;
                    viewModel.IsCreditCarriedTextVisible = false;
                    viewModel.VATDeclarationData = vATDeclaration;
                    viewModel.ReturnReferenceNumber = vATDeclaration.data.Fbnumz;
                    viewModel.TaxablePeriod = vATDeclaration.data.Perslt;

                    if (App.ICRStatus == "E0045" && viewModel.VATDeclarationData.data.RefundFg != "1")
                    {
                        if (Convert.ToDouble(viewModel.VATDeclarationData.data.NetdueVat) <= 0)
                        {
                            viewModel.IsSadadNumberVisible = false;
                            viewModel.IsRefreshButtonVisible = false;
                            viewModel.IsButtonVisible = true;
                            if (vATDeclaration.data.EstimatedFg == "X")
                            {
                                viewModel.IsAcknowledgementButtonVisible = false;
                            }
                            else
                            {
                                viewModel.IsAcknowledgementButtonVisible = true;
                            }
                        }
                        else
                        {
                            RefreshForSadad();
                        }
                    }
                    else
                    {
                        if (App.ICRStatus == "E0006" && Convert.ToDouble(viewModel.VATDeclarationData.data.NetdueVat) <= 0 || (App.ICRStatus == "E0013" || App.ICRStatus == "E0056" || App.ICRStatus == "E0057") && Convert.ToDouble(viewModel.VATDeclarationData.data.NetdueVat) <= 0 || App.ICRStatus == "E0055" && Convert.ToDouble(viewModel.VATDeclarationData.data.NetdueVat) <= 0)
                        {
                            viewModel.IsSadadNumberVisible = false;
                            viewModel.IsRefreshButtonVisible = false;
                            viewModel.IsButtonVisible = true;
                            viewModel.IsCreditCarriedTextVisible = true;
                            if (vATDeclaration.data.EstimatedFg == "X")
                            {
                                viewModel.IsAcknowledgementButtonVisible = false;
                            }
                            else
                            {
                                viewModel.IsAcknowledgementButtonVisible = true;
                            }
                        }
                        else
                        {
                            if (App.ICRStatus == "E0006" && Convert.ToDouble(viewModel.VATDeclarationData.data.NetdueVat) > 0 || App.ICRStatus == "E0056" && Convert.ToDouble(viewModel.VATDeclarationData.data.NetdueVat) > 0 || App.ICRStatus == "E0001" && Convert.ToDouble(viewModel.VATDeclarationData.data.NetdueVat) > 0 || App.ICRStatus == "E0013" && Convert.ToDouble(viewModel.VATDeclarationData.data.NetdueVat) > 0)
                            {
                                RefreshForSadad();
                            }
                            else
                            {
                                viewModel.IsRefreshButtonVisible = true;
                            }
                        }
                    }
                    if (viewModel.VATDeclarationData.data.RefundFg == "1")
                    {
                        viewModel.IsSadadNumberVisible = false;
                        viewModel.IsRefreshButtonVisible = false;
                        viewModel.IsButtonVisible = true;
                        if (vATDeclaration.data.EstimatedFg == "X")
                        {
                            viewModel.IsAcknowledgementButtonVisible = false;
                        }
                        else
                        {
                            viewModel.IsAcknowledgementButtonVisible = true;
                        }
                    }


                }
            }
            catch (Exception)
            {

            }
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                MessagingCenter.Subscribe<object, string>(this, "Card_Payment", (sender, arg) =>
                {
                    viewModel.MadaPaymentSelected();

                });
                MessagingCenter.Subscribe<object, string>(this, "Apple_Pay", (sender, arg) =>
                {
                    viewModel.ApplePaySelected();
                });
                MessagingCenter.Subscribe<object, string>(this, "SADAD", (sender, arg) =>
                {

                    viewModel.gotoSuccessPage();
                });
                MessagingCenter.Subscribe<App, string>(this, "ApplePayData", async (sender, arg) =>
                {

                    viewModel.ApplePayTokenData = arg.ToString();


                    await viewModel.UpdateApplePayPaymentGuid();


                });
            }
            catch (Exception)
            {
            }
        }

        public async void RefreshForSadad()
        {
            try
            {
                await Task.Run(() =>
                 {
                     viewModel.IsLoading = true;
                 });
                await Task.Run(async () =>
                {
                    await viewModel.OnRefreshClick();
                });
                await Task.Run(() =>
                 {
                     viewModel.IsLoading = false;
                 });
            }
            catch (Exception)
            {


                await Task.Run(() =>
                {
                    viewModel.IsLoading = false;
                });
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<object, string>(this, "Card_Payment");
            MessagingCenter.Unsubscribe<object, string>(this, "Apple_Pay");
            MessagingCenter.Unsubscribe<object, string>(this, "SADAD");
            MessagingCenter.Unsubscribe<App, string>(this, "ApplePayData");


        }

        private void SfButton_Clicked(object sender, EventArgs e)
        {
            //MopupService.Instance.PushAsync(new RefundAccountPopupPageView());
        }

        private async void OnVATRefreshButtonClicked(object sender, EventArgs e)
        {
            await viewModel.OnRefreshClick();
        }

        private void GotoreturnClicked(object sender, EventArgs e)
        {

            if (Navigation.NavigationStack.Count > 0)
            {
                Page pg = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                Navigation.RemovePage(pg);
            }
            viewModel._navigationService.GoBack();
        }

        private async void OnCopySadadNumberButtonClicked(object sender, EventArgs e)
        {
            await Clipboard.SetTextAsync(viewModel.SadadNumber);
            if (Clipboard.HasText)
            {
                var text = await Clipboard.GetTextAsync();

                List<HeaderWithInfo> headerWithInfos = new List<HeaderWithInfo>();
                HeaderWithInfo headerAmountInfo = new HeaderWithInfo();
                NewDesignPopUp newDesignPopUp = new NewDesignPopUp();

                headerAmountInfo.IsLinkAvailable = false;
                headerAmountInfo.Message = AppResources.ZSadadInvoiceNumber + " " + text;

                headerWithInfos.Add(headerAmountInfo);


                newDesignPopUp.HeaderWithInfos = new List<HeaderWithInfo>();
                newDesignPopUp.HeaderWithInfos = headerWithInfos;
                newDesignPopUp.MainHeader = AppResources.Copied;

                MopupService.Instance.PushAsync(new GAZTNewDesignShowVatInformationPopUpPageView(newDesignPopUp));
            }
        }

        private void OnVATPayNowClicked(object sender, EventArgs e)
        {


            doValidateVATReturnAmount();
        }


        public async Task doValidateVATReturnAmount()
        {

            VATDeclaration _vATDeclaration = await WebServiceManager.GAZTGetVATReturns(viewModel.VATDeclarationData.data.Fbguid, viewModel.VATDeclarationData.data.Fbnum, App.TP.Tin, viewModel.VATDeclarationData.data.Persl);


            if (_vATDeclaration.data.MadabutFg == "X")
            {
                await MopupService.Instance.PushAsync(new PaymentOptionsPageView(true, false, false, ""));
            }
            else
            {

                await MopupService.Instance.PushAsync(new PaymentOptionsPageView(true, false, true, _vATDeclaration.data.OpenliMsg));

            }

        }

    }
}