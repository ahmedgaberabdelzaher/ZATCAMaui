
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
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }




            OnDownloadFormClicked = new Command(() =>
            {
                string Url = string.Empty;
                // Url = "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/Z_GET_ACK_LETTER_SRV/Ack_letterSet(Fbnum=%2765000178937%27)/$value?saml2=disabled";
                // Url = Constants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_COVERFORM_SRV/cover_formSet(Fbnum='" + VATDeclarationData.d.Fbnum + "',Utype='')/$value?saml2=disabled";

                Url = ZATCAConstants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_COVERFORM_MOB_SRV/cover_formSet(Euser='" + App.TP.TIN + "',Fbnum='" + VATDeclarationData.data.Fbnumz + "',Utype='')/$value?saml2=enabled";

                // Url = Constants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_ACK_LETTER_SRV/Ack_letterSet(Fbnum='" + VATDeclarationData.d.Fbnum + "')/$value";
                ShowPdf(Url);
            });
            OnAcknowlwdgementClicked = new Command(() =>
            {
                string Url = string.Empty;
                // Url = Constants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_ACK_LETTER_SRV/Ack_letterSet(Fbnum='" + VATDeclarationData.d.Fbnum + "')/$value?saml2=disabled";

                Url = ZATCAConstants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_ACK_LETTER_MOB_SRV/Ack_letterSet(Euser='" + App.TP.TIN + "',Fbnum='" + VATDeclarationData.data.Fbnumz + "')/$value?saml2=enabled";
                ShowPdf(Url);
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
        public void ShowPdf(string pdfUrl)
        {

            if (pdfUrl != null)
            {
                _navigationService.NavigateTo(App.PdfView, pdfUrl);
            }
            else
            {
                //pop that certificate is not available
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
                });
            }
            //}
        }

        public void MadaPaymentSelected()
        {

            DoValidatePayment(fbNum: VATDeclarationData.data.Fbnumz, "Mada Payment");
        }

        public void ApplePaySelected()
        {
            //DoProcessApplePayPayment(VATDeclarationData.d.Fbnum);

            DoValidatePayment(fbNum: VATDeclarationData.data.Fbnumz, "A");



        }

        public void gotoSuccessPage()
        {
            _navigationService.NavigateTo(App.MyBillsSadadDetailsPageView, _vATDeclarationData);
        }
        public async Task DoValidatePayment(string fbNum, string paymentType)
        {
            try
            {
                try
                {

                    IsLoading = true;

                    var platform = "";

                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        platform = "C4";
                    }
                    else if (Device.RuntimePlatform == Device.Android)
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
                            MainThread.BeginInvokeOnMainThread(async () => {
                                IsLoading = true;
                                //CR7420
                                CreateMadaResponseRoot respose = await GetWebviewContent(PaymentData.d.Srcid);
                                IsLoading = false;
                                if (!string.IsNullOrEmpty(respose?.result?.securityAuthorizationKey))
                                {
                                    App.securityAuthorizationKey = respose.result.securityAuthorizationKey;
                                    _navigationService.NavigateTo(App.PaymentProcessWebview, 1);
                                    //await App.Current.MainPage.Navigation.PushAsync(new PaymentProcessWebview());
                                }
                            });
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
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        IsLoading = false;
                        await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        _navigationService.GoBack();
                    });
                }
                catch (InternetException ex)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {

                        IsLoading = false;
                        //   await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                        _navigationService.GoBack();
                    });
                }
                catch (GAZTNetworkConnectivityIssueException ex)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        IsLoading = false;
                        //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));

                    });
                }
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                    _navigationService.GoBack();
                });
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
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    var message = ex.Message.Substring(0, 1).ToUpper() + ex.Message.Substring(1).ToLower();
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(message));
                    //await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    //_navigationService.GoBack();
                });
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

                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        platform = "C4";
                    }
                    else if (Device.RuntimePlatform == Device.Android)
                    {
                        platform = "C3";
                    }



                    ApplePayToken modelDetails = new ApplePayToken();
                    modelDetails.Guid = App.PaymentGuid;
                    modelDetails.SrcId = platform;

                    //var token = "GrPRb/eyYkhLaxIi8ugsU5I0D2/IE6JT6SYb4o6CH/emQV7n5twiqt8IVazkcItvmCkHXeie16Nvbq+uFFx0mS4O/1+SoDHrP8HcDbJ/Q1swCCHR/Dwv69oTcTUy1riK6Zvpe0w1r+WJ21I36gorRUn7u94Yi9n4afOfnGJC3EmFd6DKSIRQWlT4BuLlNv5826XruanuFjdL3MKty/xoCyx2GKN+e8W6BFVnQc/gsBe4UW7oqHIQ5PrQJlQwymi5Ytd1IIJT8QsUMxiVjz6yVS5zdQBaN86ZtuokJRmC89jCwVkUMwDl9jQ5xYbFlIFS1VXKJjtWKDfMGwCWK3jvWdtCcdb4VrPIxtK7LvTWc+4C7m6SPzkOhdC/XPn7ufwvrh95no7p9tpQMkP7zOJIYAl+hS4oEqvOxdpw55dCytGXJ0yjN/HOQ3t4ofyW9mBGiHoq";
                    modelDetails.PaymentToken = ApplePayTokenData;



                    ApplePayTokenResponse response = await WebServiceManager.GAZTUpdateApplePayGuid(modelDetails);


                    if (response != null && response.d != null)
                    {



                        if (response.d.Success)
                        {

                            MainThread.BeginInvokeOnMainThread(() =>
                            {

                                PaymentSucess paymentInfo = new PaymentSucess();
                                paymentInfo.Paymentref = response.d.PayRef;
                                if (response.d.PerslTxt != null)
                                {
                                    paymentInfo.Period = response.d.PerslTxt;
                                }

                                _navigationService.NavigateTo(App.VatReturnNewSuccessPageView, paymentInfo);

                            });
                        }
                        else
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                MainThread.BeginInvokeOnMainThread(async () =>
                                {

                                    await MopupService.Instance.PushAsync(new PaymentExceptionPageView());

                                });
                            });

                        }

                    }
                    IsLoading = false;

                }
                catch (GAZTValidatePaymentInProcessException ex)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        IsLoading = false;
                        await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        _navigationService.GoBack();
                    });
                }
                catch (InternetException ex)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        IsLoading = false;
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                        _navigationService.GoBack();
                    });
                }
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                    _navigationService.GoBack();
                });
            }
        }

        public async Task OnRefreshClick()
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    var response = await WebServiceManager.GAZTGetVATDeclarationSADADNumber(VATDeclarationData.data.Fbnumz);
                    PopToRootPage();
                    if (response != null && response.d != null && response.d.results.Count != 0)
                    {
                        SadadNumber = response.d.results[0].Sopbel;
                        AmountPayable = response.d.results[0].Betrh;
                        if (!string.IsNullOrEmpty(SadadNumber))
                        {
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
                            IsRefreshButtonVisible = false;
                        }
                        else
                        {
                            IsSadadNumberVisible = false;
                            IsButtonVisible = false;
                            IsAcknowledgementButtonVisible = false;
                            IsRefreshButtonVisible = true;
                        }
                    }

                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
            }
        }
    }
}
