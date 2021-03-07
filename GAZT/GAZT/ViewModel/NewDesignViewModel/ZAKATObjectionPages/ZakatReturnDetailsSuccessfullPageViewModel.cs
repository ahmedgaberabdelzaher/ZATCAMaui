using System;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Helper;
using EGAZT.Models;
using EGAZT.Models.PaymentModel;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using EGAZT.Views.NewDesign.PaymentOptions;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel.ZAKATObjectionPages
{
    [Preserve(AllMembers = true)]
    public class ZakatReturnDetailsSuccessfullPageViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnInvoiceClicked { get; set; }
        string Cokey = "";
        public bool IsrefreshEnabled = false;
        public ICommand OnBackButtonClicked { get; set; }

        string Cotyp = "";
        #endregion

        #region Property
        private bool _isLoading = false;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                if (_isLoading == value) return;
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
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
                RaisePropertyChanged("EstimatedZAKATSADADNumber");
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
                RaisePropertyChanged("TotalAmount");
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
                RaisePropertyChanged("ZAKATAmount");
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
                RaisePropertyChanged("SadadBindNumber");
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
                RaisePropertyChanged("SADADNumber");
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

                RaisePropertyChanged("ReferenceNumber");
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

                RaisePropertyChanged("TaxablePeriod");
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
                RaisePropertyChanged("RefreshIconImageSource");
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
                RaisePropertyChanged("SetSuccessMessageVisibility");
            }
        }
        
        /*private ZakatReturnDetails _zakatReturnDetails;
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
                RaisePropertyChanged("ZakatReturnDetails");
            }
        }*/
        
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
                RaisePropertyChanged("ZakatReturnDetail");
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
                RaisePropertyChanged("PaymentData");
            }
        }

        #endregion

        #region Constructor
        public ZakatReturnDetailsSuccessfullPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            _navigationService = navigationService;
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            _dialogService = dialogService;

           
            OnInvoiceClicked = new Xamarin.Forms.Command(() =>
            {
                OnDownLoadInvoiceClicked();
            });

            OnBackButtonClicked = new Xamarin.Forms.Command(() =>
            {
               // _navigationService.GoBack();
                var _navigation = Application.Current.MainPage.Navigation;
                //    foreach (var item in _navigation.NavigationStack)
                //    {
                //        if (item.GetType().Name == App.PaymentProcessWebview)
                //        {
                //            _navigation.RemovePage(item);
                //            break;
                //        }
                //    }
                //    _navigationService.GoBack();
                if (_navigation.NavigationStack.Count > 0)
                {
                    Xamarin.Forms.Page pg =_navigation.NavigationStack[_navigation.NavigationStack.Count - 2];
                    _navigation.RemovePage(pg);
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
                        if (Convert.ToDouble(estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0].Undisamt) > 0 || Convert.ToDouble(estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0].Disamt) > 0)
                        {
                            Cokey = estimatedZAKATReturnsSADADNumber.d.Cokey;
                            Cotyp = estimatedZAKATReturnsSADADNumber.d.Cotyp;
                            estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0].ObjectionInvoiceVisibility = true;
                            estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0].InvoiceVisibility = false;
                            EstimatedZAKATSADADNumber = estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0];
                            if (string.IsNullOrEmpty(estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0].Sopbel)) 
                            {
                                IsrefreshEnabled = true;
                                RefreshIconImageSource = "ic_refresh.png";
                            }
                            else
                            {
                                ReferenceNumber = EstimatedZAKATSADADNumber.Sopbel;
                                SADADNumber = EstimatedZAKATSADADNumber.Sadadid;
                                ZAKATAmount = EstimatedZAKATSADADNumber.Stotamt;
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
                            estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0].ObjectionInvoiceVisibility = false;
                            estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0].InvoiceVisibility = true;
                          

                            if (string.IsNullOrEmpty(estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0].Sopbel)) 
                            {
                                IsrefreshEnabled = true;
                                RefreshIconImageSource = "ic_refresh.png";
                              await  PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZVatAcknowledgmentWaitingText));


                            }
                            else
                            {
                                EstimatedZAKATSADADNumber = estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0];

                                ReferenceNumber = EstimatedZAKATSADADNumber.Sopbel;
                                SADADNumber = EstimatedZAKATSADADNumber.Sadadid;
                                ZAKATAmount = EstimatedZAKATSADADNumber.Stotamt;
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
                        Device.BeginInvokeOnMainThread(async () => {
                           // await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));

                            _navigationService.GoBack();
                        });
                        IsLoading = false;
                        estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0].ObjectionInvoiceVisibility = true;
                        estimatedZAKATReturnsSADADNumber.d.InvoiceSet.results[0].InvoiceVisibility = false;
                    }
                }
                catch (InternetException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                       // _dialogService.ShowMessage(, AppResources.Information);
                    });
                }
            });
            await Task.Run(() =>
            {
                IsLoading = false;
            });
        }

        protected void OnDownLoadInvoiceClicked()
        {
            GetPdfUrl();
        }

        public void GetPdfUrl()
        {
            String url = Constants.GAZTGetEstimatedZAKATReturnInvoicePdf + Cokey + "',Cotyp='" + Cotyp + "')/$value?saml2=enabled";
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
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PdfIsNoteAvailable));

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
                    PaymentData = await WebServiceManager.GAZTValidatePayment(fbNum, App.LoginDataRetrieved.TIN, platform, paymentType);


                    if (PaymentData != null && PaymentData.d != null)
                    {

                        if (PaymentData.d.Guid != null&&PaymentData.d.Guid == "")
                        {
                            await PopupNavigation.Instance.PushAsync(new PaymentExceptionPageView());
                            return;
                        }
                        
                        if (PaymentData.d.Guid != null)
                        {

                            App.PaymentGuid = PaymentData.d.Guid;

                        }


                        if (paymentType == "M")
                        {

                            Device.BeginInvokeOnMainThread(async () => {

                                _navigationService.NavigateTo(App.PaymentProcessWebview, 0);
                                //await App.Current.MainPage.Navigation.PushAsync(new PaymentProcessWebview());

                            });
                        }
                        else
                        {
                            var ZakatAmount = ZakatReturnDetail.Zkamt.Replace(",", "");

                              await ProcessApplePay();
                        }



                        //if (ZakatReturnDetails.d.MadabutFg == "X")
                        //{

                        //    await PopupNavigation.Instance.PushAsync(new PaymentOptionsPageView(true, false, false, ""));
                        //}
                        //else
                        //{

                        //    await PopupNavigation.Instance.PushAsync(new PaymentOptionsPageView(true, false, true, ZakatReturnDetails.d.OpenliMsg));

                        //}


                        //var ZakatAmount = ZakatReturnDetails.d.Zkamt.Replace(",", "");
                        //if (String.IsNullOrEmpty(ZakatAmount) || Double.Parse(ZakatAmount) == 0)
                        //{
                        //    await PopupNavigation.Instance.PushAsync(new PaymentOptionsPageView(true, true, false, ZakatReturnDetails.d.OpenliMsg));

                        //}
                        //else if (!String.IsNullOrEmpty(ZakatAmount) && Double.Parse(ZakatAmount) > 20000)
                        //{
                        //    await PopupNavigation.Instance.PushAsync(new PaymentOptionsPageView(true, false, true, ZakatReturnDetails.d.OpenliMsg));

                        //}
                        //else
                        //{
                        //    await PopupNavigation.Instance.PushAsync(new PaymentOptionsPageView(true, false, false, ZakatReturnDetails.d.OpenliMsg));

                        //}

                    }

                    IsLoading = false;

                }
                catch (GAZTValidatePaymentInProcessException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {

                        IsLoading = false;
                        if (ex.Message == "There is no open liability to be paid against this declaration")
                        {

                            await PopupNavigation.Instance.PushAsync(new PaymentOptionsPageView(false, true, false, AppResources.NoOpenLiabilityToBePaid));

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
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        IsLoading = false;
                        //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));

                    });
                }
                catch (InternetException ex)
                {

                    Device.BeginInvokeOnMainThread(async () =>
                    {

                        IsLoading = false;
                        //   await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                        _navigationService.GoBack();
                    });
                }
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {

                    IsLoading = false;
                    //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                    _navigationService.GoBack();
                });
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

                            Device.BeginInvokeOnMainThread(() =>
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
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                //await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                                //_navigationService.GoBack();

                                await PopupNavigation.Instance.PushAsync(new PaymentExceptionPageView());
                            });

                        }

                    }
                    IsLoading = false;

                }
                catch (GAZTValidatePaymentInProcessException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        IsLoading = false;
                        await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        _navigationService.GoBack();
                    });
                }
                catch (InternetException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        IsLoading = false;
                        //   await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                        _navigationService.GoBack();
                    });
                }
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                    _navigationService.GoBack();
                });
            }
        }

        public async Task MadaPaymentSelectedAsync()
        {

            await DoValidatePayment(fbNum: ZakatReturnDetail.Fbnum, "M");


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
            return DependencyService.Get<IApplePayAuthorizer>().AuthorizePayment(ZakatAmount, AppResources.ZAmount);
        }

        public async Task SadadPaymentSelected()
        {

            _navigationService.NavigateTo(App.MyBillsSadadDetailsPageView, ZakatReturnDetail);
        }

    }
}
