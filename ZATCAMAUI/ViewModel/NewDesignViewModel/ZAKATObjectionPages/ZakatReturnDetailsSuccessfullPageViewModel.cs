using System.Windows.Input;


using Mopups.Services;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.PaymentModel;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Views.NewDesign.PaymentOptions;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.ZAKATObjectionPages
{
    public class ZakatReturnDetailsSuccessfullPageViewModel : BaseViewModel
    {
        #region Variable
        public ICommand OnInvoiceClicked { get; set; }
        string Cokey = "";
        public bool IsrefreshEnabled = false;
        public ICommand OnBackButtonClicked { get; set; }

        string Cotyp = "";
        #endregion

        #region Property
       
        private ZakatReturnDetails _zakatReturnDetails;
        public ZakatReturnDetails ZakatReturnDetails
        {
            get
            {
                return _zakatReturnDetails;
            }
            set
            {
                if (_zakatReturnDetails == value) return;

                _zakatReturnDetails = value;
                OnPropertyChanged("ZakatReturnDetails");
            }
        }

        private EstimatedZAKATReturnsSADADNumberResult _estimatedZAKATSADADNumber;
        public EstimatedZAKATReturnsSADADNumberResult EstimatedZAKATSADADNumber
        {
            get
            {
                return _estimatedZAKATSADADNumber;
            }
            set
            {
                if (_estimatedZAKATSADADNumber == value) return;

                _estimatedZAKATSADADNumber = value;
                OnPropertyChanged("EstimatedZAKATSADADNumber");
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

        private string _zAKATAmount;
        public string ZAKATAmount
        {
            get
            {
                return _zAKATAmount;
            }
            set
            {


                _zAKATAmount = value;
                TotalAmount = _zAKATAmount;
                OnPropertyChanged("ZAKATAmount");
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

        private string _sADADNumber;
        public string SADADNumber
        {
            get
            {
                return _sADADNumber;
            }
            set
            {

                _sADADNumber = value;
                OnPropertyChanged("SADADNumber");
            }
        }

        private string _referenceNumber;
        public string ReferenceNumber
        {
            get
            {
                return _referenceNumber;
            }
            set
            {

                _referenceNumber = value;
                SadadBindNumber = value;

                OnPropertyChanged("ReferenceNumber");
            }
        }

        private string _taxablePeriod;
        public string TaxablePeriod
        {
            get
            {
                return _taxablePeriod;
            }
            set
            {

                _taxablePeriod = value;

                OnPropertyChanged("TaxablePeriod");
            }
        }

        private string _refreshIconImageSource = "";
        public string RefreshIconImageSource
        {
            get
            {
                return _refreshIconImageSource;
            }
            set
            {
                if (_refreshIconImageSource == value) return;

                _refreshIconImageSource = value;
                OnPropertyChanged("RefreshIconImageSource");
            }
        }

        private bool _setSuccessMessageVisibility = false;
        public bool SetSuccessMessageVisibility
        {
            get
            {
                return _setSuccessMessageVisibility;
            }
            set
            {
                if (_setSuccessMessageVisibility == value) return;

                _setSuccessMessageVisibility = value;
                OnPropertyChanged("SetSuccessMessageVisibility");
            }
        }

        private ZakatReturnDetailsD _zakatReturnDetail;
        public ZakatReturnDetailsD ZakatReturnDetail
        {
            get
            {
                return _zakatReturnDetail;
            }
            set
            {
                if (_zakatReturnDetail == value) return;

                _zakatReturnDetail = value;
                OnPropertyChanged("ZakatReturnDetail");
            }
        }

        public string ApplePayTokenData;

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

        private bool _isPayNowVisible;
        public bool IsPayNowVisible
        {
            get
            {
                return _isPayNowVisible;
            }
            set
            {
                if (_isPayNowVisible == value) return;

                _isPayNowVisible = value;
                OnPropertyChanged("IsPayNowVisible");
            }
        }



        #endregion

        #region Constructor
        public ZakatReturnDetailsSuccessfullPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            OnInvoiceClicked = new Command(() =>
            {
                OnDownLoadInvoiceClicked();
            });

            OnBackButtonClicked = new Command(() =>
            {
                var _navigation = Application.Current.MainPage.Navigation;
               
                if (!App.ZakatReturnBilldetails)
                {

                    if (_navigation.NavigationStack.Count > 0)
                    {
                       Page pg = _navigation.NavigationStack[_navigation.NavigationStack.Count - 2];
                        _navigation.RemovePage(pg);
                    }

                    App.ZakatReturnBilldetails = false;
                }


                _navigationService.GoBack();
            });



        }
        #endregion

        #region Method
        public async Task OnPageLoad(ZakatReturnDetailsD zakatReturnDetailsD)
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });
            await Task.Run(async () =>
            {
                try
                {
                    SetSuccussMessageVisibility();
                    EstimatedZAKATReturnsSADADNumber estimatedZAKATReturnsSADADNumber = await WebServiceManager.GAZTGetEstimatedZakatReturnSADADNumber(zakatReturnDetailsD.Fbnum, ZAKATReturnDetailsViewModel.Fbguid); // Method to get the invoice
                                                                                                                                                                                                                       //  PopToRootPage();
                    if (estimatedZAKATReturnsSADADNumber != null && estimatedZAKATReturnsSADADNumber.d != null)
                    {
                       // IsMainGridVisble = true;
                        if (Convert.ToDouble(estimatedZAKATReturnsSADADNumber.d.results[0].Undisamt) > 0 || Convert.ToDouble(estimatedZAKATReturnsSADADNumber.d.results[0].Disamt) > 0)
                        {
                            Cokey = estimatedZAKATReturnsSADADNumber.d.Cokey;
                            Cotyp = estimatedZAKATReturnsSADADNumber.d.Cotyp;
                            estimatedZAKATReturnsSADADNumber.d.results[0].ObjectionInvoiceVisibility = true;
                            estimatedZAKATReturnsSADADNumber.d.results[0].InvoiceVisibility = false;
                            EstimatedZAKATSADADNumber = estimatedZAKATReturnsSADADNumber.d.results[0];
                            if (string.IsNullOrEmpty(estimatedZAKATReturnsSADADNumber.d.results[0].Sopbel)) 
                            {
                                IsrefreshEnabled = true;
                                RefreshIconImageSource = "ic_refresh.png";
                            }
                            else
                            {
                                ReferenceNumber = EstimatedZAKATSADADNumber.Sopbel;
                                SADADNumber = EstimatedZAKATSADADNumber.Sadadid;


                                if (string.Equals(zakatReturnDetailsD.Statusz, "E0005"))// In Processing
                                {
                                    ZAKATAmount = EstimatedZAKATSADADNumber.Stotamt;
                                }
                                else
                                {
                                    ZAKATAmount = EstimatedZAKATSADADNumber.Totamt;

                                }

                                IsrefreshEnabled = false;
                                RefreshIconImageSource = "";

                            }
                            //  RefreshIconImageSource = "ic_refresh.png";
                            //else
                            //    IsrefreshEnabled = false;
                        }
                        else
                        {
                            Cokey = estimatedZAKATReturnsSADADNumber.d.Cokey;
                            Cotyp = estimatedZAKATReturnsSADADNumber.d.Cotyp;
                            estimatedZAKATReturnsSADADNumber.d.results[0].ObjectionInvoiceVisibility = false;
                            estimatedZAKATReturnsSADADNumber.d.results[0].InvoiceVisibility = true;
                          

                            if (string.IsNullOrEmpty(estimatedZAKATReturnsSADADNumber.d.results[0].Sopbel)) 
                            {
                                IsrefreshEnabled = true;
                                RefreshIconImageSource = "ic_refresh.png";
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZVatAcknowledgmentWaitingText));


                            }
                            else
                            {
                                EstimatedZAKATSADADNumber = estimatedZAKATReturnsSADADNumber.d.results[0];

                                ReferenceNumber = EstimatedZAKATSADADNumber.Sopbel;
                                SADADNumber = EstimatedZAKATSADADNumber.Sadadid;

                                if (string.Equals(zakatReturnDetailsD.Statusz, "E0005"))// In Processing
                                {
                                    ZAKATAmount = EstimatedZAKATSADADNumber.Totamt;
                                }
                                else
                                {
                                    ZAKATAmount = EstimatedZAKATSADADNumber.Stotamt;

                                }
                                IsrefreshEnabled = false;
                                RefreshIconImageSource = "";

                            }

                            //    RefreshIconImageSource = "ic_refresh.png";
                            //else
                            //    IsrefreshEnabled = false;
                        }
                        GetUpdatedDataAfterAddingComma();


                    }
                    else
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            // await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));

                            _navigationService.GoBack();
                        });
                        IsLoading = false;
                        estimatedZAKATReturnsSADADNumber.d.results[0].ObjectionInvoiceVisibility = true;
                        estimatedZAKATReturnsSADADNumber.d.results[0].InvoiceVisibility = false;
                    }
                }
                catch (InternetException ex)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                        // _dialogService.ShowMessage(, AppResources.Information);
                    });
                }
            });
            await Task.Run(() =>
            {
                IsLoading = false;
            });
        }


        public async Task doValidateZakatAmount()
        {


            ZakatReturnDetails = await WebServiceManager.GAZTGetZAKATReturn(App.selectedForm12Fbguid);




            if (ZakatReturnDetails.d.MadabutFg == "X")
            {

                MopupService.Instance.PushAsync(new PaymentOptionsPageView(true, false, false, ""));
            }
            else
            {

                MopupService.Instance.PushAsync(new PaymentOptionsPageView(true, false, true, ZakatReturnDetails.d.OpenliMsg));

            }

        }

        protected void OnDownLoadInvoiceClicked()
        {
            GetPdfUrl();
        }

        public void GetPdfUrl()
        {
            string url = ZATCAConstants.GAZTGetEstimatedZAKATReturnInvoicePdf + Cokey + "&correspondenceType=" + Cotyp;
            // string url =  await  WebServiceManager.GAZTEstimatedZAKATReturnInvoicePdf(Cokey);
            ShowPdf(url);
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
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PdfIsNoteAvailable));

                    // await _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
                });
            }
        }

        private void GetUpdatedDataAfterAddingComma()
        {
            if (EstimatedZAKATSADADNumber != null)
            {
                EstimatedZAKATSADADNumber.Undisamt = UtilityManager.GetCommaSeparatedAmount(EstimatedZAKATSADADNumber.Undisamt);
                EstimatedZAKATSADADNumber.Disamt = UtilityManager.GetCommaSeparatedAmount(EstimatedZAKATSADADNumber.Disamt);
                EstimatedZAKATSADADNumber.Totamt = UtilityManager.GetCommaSeparatedAmount(EstimatedZAKATSADADNumber.Totamt);
                EstimatedZAKATSADADNumber.Stotamt = UtilityManager.GetCommaSeparatedAmount(EstimatedZAKATSADADNumber.Stotamt);
                EstimatedZAKATSADADNumber.Sdisamt = UtilityManager.GetCommaSeparatedAmount(EstimatedZAKATSADADNumber.Sdisamt);
                EstimatedZAKATSADADNumber.Stotamt = UtilityManager.GetCommaSeparatedAmount(EstimatedZAKATSADADNumber.Stotamt);
            }
        }


        public void ClearData()
        {

            ReferenceNumber = string.Empty;
            TaxablePeriod = string.Empty;
            SADADNumber = string.Empty;
            ZAKATAmount = string.Empty;

            IsrefreshEnabled = false;
        }

        private void SetSuccussMessageVisibility()
        {
            if (ZAKATReturnDetailsViewModel.IsBillsButtonTapped == true)
            {
                SetSuccessMessageVisibility = false;
            }
            else
            {
                SetSuccessMessageVisibility = true;
            }
        }
        #endregion

        public void gotoSuccessPage()
        {

            _navigationService.NavigateTo(App.MyBillsSadadDetailsPageView, ZakatReturnDetail);
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
                    // PaymentData = await WebServiceManager.GAZTValidatePayment(fbNum, App.LoginDataRetrieved.TIN, platform, paymentType);


                    ValidatePayment modelDetails = new ValidatePayment();
                    modelDetails.Fbnum = fbNum;
                    modelDetails.Pymntty = paymentType;
                    modelDetails.Tin = App.LoginDataRetrieved.TIN;
                    modelDetails.Srcid = platform;
                    modelDetails.Srctile = "12";
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
                                    _navigationService.NavigateTo(App.PaymentProcessWebview, 0);
                                    //await App.Current.MainPage.Navigation.PushAsync(new PaymentProcessWebview());
                                }
                            });
                        }
                        else
                        {
                            var ZakatAmount = ZakatReturnDetail.Zkamt.Replace(",", "");

                            await ProcessApplePay();
                        }



                       
                    }

                    IsLoading = false;

                }
                catch (GAZTValidatePaymentInProcessException ex)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {

                        IsLoading = false;
                        if (ex.Message == "There is no open liability to be paid against this declaration")
                        {

                            await MopupService.Instance.PushAsync(new PaymentOptionsPageView(false, true, false, AppResources.NoOpenLiabilityToBePaid));

                        }
                        else
                        {
                            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                            _navigationService.GoBack();
                        }



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
                Device.BeginInvokeOnMainThread(async () =>
                {

                    var message = ex.Message.Substring(0, 1).ToUpper() + ex.Message.Substring(1).ToLower();
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(message));
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

        public async Task UpdateApplePayPaymentGuid()
        {
            try
            {
                try
                {

                    IsLoading = true;

                    string platform = string.Empty;

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
                    //var token = "GrPRb/eyYkhLaxIi8ugsU5I0D2/IE6JT6SYb4o6CH/emQV7n5twiqt8IVazkcItvmCkHXeie16Nvbq+uFFx0mS4O/1+SoDHrP8HcDbJ/Q1swCCHR/Dwv69oTcTUy1riK6Zvpe0w1r+WJ21I36gorRUn7u94Yi9n4afOfnGJC3EmFd6DKSIRQWlT4BuLlNv5826XruanuFjdL3MKty/xoCyx2GKN+e8W6BFVnQc/gsBe4UW7oqHIQ5PrQJlQwymi5Ytd1IIJT8QsUMxiVjz6yVS5zdQBaN86ZtuokJRmC89jCwVkUMwDl9jQ5xYbFlIFS1VXKJjtWKDfMGwCWK3jvWdtCcdb4VrPIxtK7LvTWc+4C7m6SPzkOhdC/XPn7ufwvrh95no7p9tpQMkP7zOJIYAl+hS4oEqvOxdpw55dCytGXJ0yjN/HOQ3t4ofyW9mBGiHoq";
                    modelDetails.PaymentToken = ApplePayTokenData;
                    modelDetails.SrcId = platform;


                    ApplePayTokenResponse response = await WebServiceManager.GAZTUpdateApplePayGuid(modelDetails);


                    if (response != null && response.d != null)
                    {


                        if (response.d.Success)
                        {

                            MainThread.BeginInvokeOnMainThread(() =>
                            {

                                //_navigationService.NavigateTo(App.ZakatReturnNewSuccessPageView, PaymentData.d.PayRef);
                                //await App.Current.MainPage.Navigation.PushAsync(new PaymentProcessWebview());

                                PaymentSucess paymentInfo = new PaymentSucess();
                                paymentInfo.Paymentref = response.d.PayRef;
                                if (response.d.PerslTxt != null)
                                {
                                    paymentInfo.Period = response.d.PerslTxt;
                                }

                                _navigationService.NavigateTo(App.ZakatReturnNewSuccessPageView, paymentInfo);

                            });
                        }
                        else
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                //await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                                //_navigationService.GoBack();

                                await MopupService.Instance.PushAsync(new PaymentExceptionPageView());
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
                        //   await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
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
                    //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                    _navigationService.GoBack();
                });
            }
        }

        public async Task MadaPaymentSelectedAsync()
        {

            await DoValidatePayment(fbNum: ZakatReturnDetail.Fbnum, "Mada Payment");


        }

        public async Task ApplePaySelected()
        {
            DoValidatePayment(fbNum: ZakatReturnDetail.Fbnum, "A");

        }

        private async Task<bool> ProcessApplePay()
        {


            var Amount = Convert.ToDouble(PaymentData.d.Amount);
            var ZakatAmount = Math.Round(Amount, 2);
            DependencyService.Get<IApplePayAuthorizer>().IsPaymentFromDashboard(false);
            return DependencyService.Get<IApplePayAuthorizer>().AuthorizePayment(ZakatAmount, AppResources.ApplePayText);
        }

        public async Task SadadPaymentSelected()
        {

            _navigationService.NavigateTo(App.MyBillsSadadDetailsPageView, ZakatReturnDetail);
        }

    }
}
