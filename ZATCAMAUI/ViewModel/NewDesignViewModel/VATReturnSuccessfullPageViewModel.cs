
using Mopups.Services;
using System.Windows.Input;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.PaymentModel;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Views.NewDesign.PaymentOptions;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel
{

    public class VATReturnSuccessfullPageViewModel : BaseViewModel
    {
        public ICommand OnDownloadFormClicked { get; set; }
        public ICommand OnAcknowlwdgementClicked { get; set; }
        public ICommand OnBackButtonClicked { get; set; }


        #region Properties

        public string _returnReferenceNumber;
        public string ReturnReferenceNumber
        {
            get
            {
                return _returnReferenceNumber;
            }
            set
            {
                if (_returnReferenceNumber == value) return;
                _returnReferenceNumber = value;
                OnPropertyChanged("ReturnReferenceNumber");
            }
        }

        public string ApplePayTokenData;

        public string _taxablePeriod;
        public string TaxablePeriod
        {
            get
            {
                return _taxablePeriod;
            }
            set
            {
                if (_taxablePeriod == value) return;

                _taxablePeriod = value;
                OnPropertyChanged("TaxablePeriod");
            }
        }

        public string _sadadNumber;
        public string SadadNumber
        {
            get
            {
                return _sadadNumber;
            }
            set
            {
                if (_sadadNumber == value) return;
                SadadBindNumber = value;
                _sadadNumber = value;
                OnPropertyChanged("SadadNumber");
            }
        }

        private string _sadadBindNumber = "";
        public string SadadBindNumber
        {
            get
            {
                return _sadadBindNumber;
            }
            set
            {
                if (_sadadBindNumber == value) return;

                _sadadBindNumber = value;
                OnPropertyChanged("SadadBindNumber");
            }
        }

        private string _totalAmount = "0.0";
        public string TotalAmount
        {
            get
            {
                return _totalAmount;
            }
            set
            {
                if (_totalAmount == value) return;

                _totalAmount = value;
                OnPropertyChanged("TotalAmount");
            }
        }

        public string _amountPayable;
        public string AmountPayable
        {
            get
            {
                return _amountPayable;
            }
            set
            {
                if (_amountPayable == value) return;
                TotalAmount = value;
                _amountPayable = value;
                OnPropertyChanged("AmountPayable");
            }
        }

        public ValidatePaymentResponse _paymentData = null;
        public ValidatePaymentResponse PaymentData
        {
            get
            {
                return _paymentData;
            }
            set
            {
                if (_paymentData == value) return;

                _paymentData = value;
                OnPropertyChanged("PaymentData");
            }
        }

        private bool _applePayStatus;
        public bool ApplePayStatus
        {
            get
            {
                return _applePayStatus;
            }
            set
            {
                if (_applePayStatus == value) return;

                _applePayStatus = value;
                OnPropertyChanged("ApplePayStatus");
            }
        }

        public bool _isSadadNumberVisible;
        public bool IsSadadNumberVisible
        {
            get
            {
                return _isSadadNumberVisible;
            }
            set
            {
                if (_isSadadNumberVisible == value) return;

                _isSadadNumberVisible = value;
                OnPropertyChanged("IsSadadNumberVisible");
            }
        }



        public VATDeclaration _vATDeclarationData;
        public VATDeclaration VATDeclarationData
        {
            get
            {
                return _vATDeclarationData;
            }
            set
            {
                if (_vATDeclarationData == value) return;

                _vATDeclarationData = value;
                OnPropertyChanged("VATDeclarationData");
            }
        }
        private bool _isButtonVisible = false;
        public bool IsButtonVisible
        {
            get
            {
                return _isButtonVisible;
            }
            set
            {
                if (_isButtonVisible == value) return;

                _isButtonVisible = value;
                OnPropertyChanged("IsButtonVisible");
            }
        }
        private bool _isCreditCarriedTextVisible = false;
        public bool IsCreditCarriedTextVisible
        {
            get
            {
                return _isCreditCarriedTextVisible;
            }
            set
            {
                if (_isCreditCarriedTextVisible == value) return;

                _isCreditCarriedTextVisible = value;
                OnPropertyChanged("IsCreditCarriedTextVisible");
            }
        }


        private bool _isAcknowledgementButtonVisible = false;
        public bool IsAcknowledgementButtonVisible
        {
            get
            {
                return _isAcknowledgementButtonVisible;
            }
            set
            {
                if (_isAcknowledgementButtonVisible == value) return;

                _isAcknowledgementButtonVisible = value;
                OnPropertyChanged("IsAcknowledgementButtonVisible");
            }
        }

        private bool _isRefreshButtonVisible = true;
        public bool IsRefreshButtonVisible
        {
            get
            {
                return _isRefreshButtonVisible;
            }
            set
            {
                if (_isRefreshButtonVisible == value) return;

                _isRefreshButtonVisible = value;
                OnPropertyChanged("IsRefreshButtonVisible");
            }
        }


        #endregion

        public VATReturnSuccessfullPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

            OnDownloadFormClicked = new Command(async () =>
            {
                string Url = string.Empty;

                Url = ZATCAConstants.downloadFile + VATDeclarationData.data.Fbnumz;

               
               await ShowPdf(Url);
            });
            OnAcknowlwdgementClicked = new Command(async () =>
            {
                string Url = string.Empty;
                
                Url = ZATCAConstants.downloadFile + VATDeclarationData.data.Fbnumz;
               await ShowPdf(Url);
            });

            OnBackButtonClicked = new Command(() =>
            {
                var _navigation = Application.Current.MainPage.Navigation;
                if (_navigation.NavigationStack.Count > 0)
                {
                    Page pg = _navigation.NavigationStack[_navigation.NavigationStack.Count - 2];
                    _navigation.RemovePage(pg);
                }
                _navigationService.GoBack();
            });
        }
        public async Task ShowPdf(string pdfUrl)
        {

            if (pdfUrl != null)
            {
               await _navigationService.NavigateTo(App.PdfView, pdfUrl);
            }
            else
            {
                await _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
            }
        }

        public async Task MadaPaymentSelected()
        {

          await  DoValidatePayment(fbNum: VATDeclarationData.data.Fbnumz, "Mada Payment");
        }

        public async Task ApplePaySelected()
        {
            //DoProcessApplePayPayment(VATDeclarationData.d.Fbnum);

          await  DoValidatePayment(fbNum: VATDeclarationData.data.Fbnumz, "A");



        }

        public async Task gotoSuccessPage()
        {
           await _navigationService.NavigateTo(App.MyBillsSadadDetailsPageView, _vATDeclarationData);
        }
        public async Task DoValidatePayment(string fbNum, string paymentType)
        {
            try
            {
                try
                {

                    IsLoading = true;

                    var platform = "";

                    if (DeviceInfo.Platform == DevicePlatform.iOS)
                    {
                        platform = "C4";
                    }
                    else if (DeviceInfo.Platform == DevicePlatform.Android)
                    {
                        platform = "C3";
                    }


                    ValidatePayment modelDetails = new ValidatePayment();
                    modelDetails.Fbnum = fbNum;
                    modelDetails.Pymntty = paymentType;
                    modelDetails.Tin = App.LoginDataRetrieved.TIN;
                    modelDetails.Srcid = platform;
                    modelDetails.Srctile = "36";
                    modelDetails.Sadad = "";

                    PaymentData = await WebServiceManager.GAZTValidatePayment(modelDetails);




                    if (PaymentData != null && PaymentData.d != null)
                    {

                        if (PaymentData.d.Guid != null && PaymentData.d.Guid == "")
                        {
                            await MopupService.Instance.PushAsync(new PaymentExceptionPageView());
                            return;
                        }

                        if (PaymentData.d.Guid != null)
                        {

                            App.PaymentGuid = PaymentData.d.Guid;

                        }


                        if (paymentType == "Mada Payment")
                        {
                            IsLoading = true;
                            //CR7420
                            CreateMadaResponseRoot respose = await GetWebviewContent(PaymentData.d.Srcid);
                            IsLoading = false;
                            if (!string.IsNullOrEmpty(respose?.result?.securityAuthorizationKey))
                            {
                                App.securityAuthorizationKey = respose.result.securityAuthorizationKey;
                                await _navigationService.NavigateTo(App.PaymentProcessWebview, 1);
                            }
                        }
                        else
                        {

                            ApplePayStatus = await ProcessApplePay();
                        }
                    }

                    IsLoading = false;

                }
                catch (GAZTValidatePaymentInProcessException ex)
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                }
                catch (InternetException )
                {
                    IsLoading = false;
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                    _navigationService.GoBack();
                }
                catch (GAZTNetworkConnectivityIssueException )
                {
                    IsLoading = false;
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));

                }
            }
            catch (InternetException )
            {
                IsLoading = false;
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                _navigationService.GoBack();
            }
        }
        public async Task<CreateMadaResponseRoot> GetWebviewContent(string srcid)
        {
            try
            {
                var paymentPayload = new CreateMadaPaymentPayload
                {
                    GUID = App.PaymentGuid,
                    sourceId = srcid
                };

                CreateMadaResponseRoot respose = await WebServiceManager.GAZTCreateMadaPayment(paymentPayload);
                return respose;
            }
            catch (GAZTValidateMadaPaymentException ex)
            {
                IsLoading = false;
                var message = ex.Message.Substring(0, 1).ToUpper() + ex.Message.Substring(1).ToLower();
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(message));

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async Task<bool> ProcessApplePay()
        {
            //var VatAmount = NetdueVat.Replace(",", "");

            var Amount = Convert.ToDouble(PaymentData.d.Amount);
            var VatAmount = Math.Round(Amount, 2);

            DependencyService.Get<IApplePayAuthorizer>().IsPaymentFromDashboard(false);
            return DependencyService.Get<IApplePayAuthorizer>().AuthorizePayment(VatAmount, AppResources.ApplePayText);
        }

        public async Task UpdateApplePayPaymentGuid()
        {
            try
            {
                try
                {

                    IsLoading = true;

                    string platform = "C4";

                    if (DeviceInfo.Platform == DevicePlatform.iOS)
                    {
                        platform = "C4";
                    }
                    else if (DeviceInfo.Platform == DevicePlatform.Android)
                    {
                        platform = "C3";
                    }



                    ApplePayToken modelDetails = new ApplePayToken();
                    modelDetails.Guid = App.PaymentGuid;
                    modelDetails.SrcId = platform;
                    modelDetails.PaymentToken = ApplePayTokenData;



                    ApplePayTokenResponse response = await WebServiceManager.GAZTUpdateApplePayGuid(modelDetails);


                    if (response != null && response.d != null)
                    {



                        if (response.d.Success)
                        {
                            PaymentSucess paymentInfo = new PaymentSucess();
                            paymentInfo.Paymentref = response.d.PayRef;
                            if (response.d.PerslTxt != null)
                            {
                                paymentInfo.Period = response.d.PerslTxt;
                            }

                            await _navigationService.NavigateTo(App.VatReturnNewSuccessPageView, paymentInfo);
                        }
                        else
                        {
                            await MopupService.Instance.PushAsync(new PaymentExceptionPageView());

                        }

                    }
                    IsLoading = false;

                }
                catch (GAZTValidatePaymentInProcessException ex)
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                }
                catch (InternetException )
                {
                    IsLoading = false;
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                    _navigationService.GoBack();
                }
            }
            catch (InternetException )
            {
                IsLoading = false;
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                _navigationService.GoBack();
            }
        }

        public async Task OnRefreshClick()
        {
            try
            {
                IsLoading = true;
                var response = await WebServiceManager.GAZTGetVATDeclarationSADADNumber(VATDeclarationData.data.Fbnumz);
                await PopToRootPage();
                if (response != null && response.d != null && response.d.results.Count != 0)
                {
                    SadadNumber = response.d.results[0].Sopbel;
                    AmountPayable = response.d.results[0].Betrh;
                    if (!string.IsNullOrEmpty(SadadNumber))
                    {
                        if (response.d.results[0].Fbust.Equals("E0045") || response.d.results[0].Fbust.Equals("E0006"))
                        {
                            IsRefreshButtonVisible = false;
                            if (VATDeclarationData.data.RefundFg == "1")
                            {
                                IsSadadNumberVisible = false;
                            }
                            else
                            {
                                IsSadadNumberVisible = true;
                            }
                            IsButtonVisible = true;
                            if (VATDeclarationData.data.EstimatedFg == "X")
                            {
                                IsAcknowledgementButtonVisible = false;
                            }
                            else
                            {
                                IsAcknowledgementButtonVisible = true;
                            }
                        }
                        else
                        {
                            IsRefreshButtonVisible = true;
                        }
                        
                    }
                    else
                    {
                        IsSadadNumberVisible = false;
                        IsButtonVisible = false;
                        IsAcknowledgementButtonVisible = false;
                        IsRefreshButtonVisible = true;
                    }
                }

                IsLoading = false;

            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            }
        }
    }
}
