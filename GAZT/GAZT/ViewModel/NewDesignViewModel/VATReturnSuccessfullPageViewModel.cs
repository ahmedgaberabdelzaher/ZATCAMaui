using EGAZT.Models;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Helper;
using EGAZT.Models.PaymentModel;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using EGAZT.Views.NewDesign.PaymentOptions;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    [Preserve(AllMembers = true)]
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
                RaisePropertyChanged("ReturnReferenceNumber");
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
                RaisePropertyChanged("TaxablePeriod");
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
                RaisePropertyChanged("SadadNumber");
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
                RaisePropertyChanged("AmountPayable");
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
                RaisePropertyChanged("PaymentData");
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
                RaisePropertyChanged("ApplePayStatus");
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
                RaisePropertyChanged("IsSadadNumberVisible");
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
                RaisePropertyChanged("VATDeclarationData");
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
                RaisePropertyChanged("IsButtonVisible");
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
                RaisePropertyChanged("IsCreditCarriedTextVisible");
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
                RaisePropertyChanged("IsAcknowledgementButtonVisible");
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
                RaisePropertyChanged("IsRefreshButtonVisible");
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




            OnDownloadFormClicked = new Xamarin.Forms.Command(() =>
            {
                String Url = string.Empty;
                // Url = "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/Z_GET_ACK_LETTER_SRV/Ack_letterSet(Fbnum=%2765000178937%27)/$value?saml2=disabled";
                // Url = Constants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_COVERFORM_SRV/cover_formSet(Fbnum='" + VATDeclarationData.d.Fbnum + "',Utype='')/$value?saml2=disabled";
                Url = Constants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_COVERFORM_MOB_SRV/cover_formSet(Euser='" + App.TP.Tin + "',Fbnum='" + VATDeclarationData.d.Fbnum + "',Utype='')/$value?saml2=enabled";

               // Url = Constants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_ACK_LETTER_SRV/Ack_letterSet(Fbnum='" + VATDeclarationData.d.Fbnum + "')/$value";
                ShowPdf(Url);
            });
            OnAcknowlwdgementClicked = new Xamarin.Forms.Command(() =>
            {
                String Url = string.Empty;
                // Url = Constants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_ACK_LETTER_SRV/Ack_letterSet(Fbnum='" + VATDeclarationData.d.Fbnum + "')/$value?saml2=disabled";
                Url = Constants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_ACK_LETTER_MOB_SRV/Ack_letterSet(Euser='" + App.TP.Tin + "',Fbnum='" + VATDeclarationData.d.Fbnum + "')/$value?saml2=enabled";
                ShowPdf(Url);
            });

            OnBackButtonClicked = new Xamarin.Forms.Command(() =>
            {
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
                    await _dialogService.ShowMessageBox(AppResources.PdfIsNoteAvailable, AppResources.Information);
                });
            }
            //}
        }

        public void MadaPaymentSelected()
        {

            DoValidatePayment(fbNum:VATDeclarationData.d.Fbnum,"M");

            //Device.BeginInvokeOnMainThread(async () => {

            //    _navigationService.NavigateTo(App.PaymentProcessWebview,1);
            //    //await App.Current.MainPage.Navigation.PushAsync(new PaymentProcessWebview());

            //});

        }

        public void ApplePaySelected()
        {
            //DoProcessApplePayPayment(VATDeclarationData.d.Fbnum);

            DoValidatePayment(fbNum: VATDeclarationData.d.Fbnum, "A");

            //var payment = DependencyService.Get<IApplePayAuthorizer>().AuthorizePayment("1","VAT Return");


        }

        public void gotoSuccessPage()
        {
            _navigationService.NavigateTo(App.MyBillsSadadDetailsPageView, _vATDeclarationData);
        }
        public async Task DoValidatePayment(string fbNum,string paymentType)
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
                    PaymentData = await WebServiceManager.GAZTValidatePayment(fbNum, App.LoginDataRetrieved.TIN, platform,paymentType);


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

                        if (paymentType == "M") {

                            Device.BeginInvokeOnMainThread(async () => {

                                _navigationService.NavigateTo(App.PaymentProcessWebview, 1);
                                //await App.Current.MainPage.Navigation.PushAsync(new PaymentProcessWebview());

                            });
                        }
                        else {

                            ApplePayStatus = await ProcessApplePay();
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
                catch (GAZTNetworkConnectivityIssueException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        IsLoading = false;
                        //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));

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
        
        private async Task<bool> ProcessApplePay()
        {
            //var VatAmount = NetdueVat.Replace(",", "");

            var Amount = Convert.ToDouble(PaymentData.d.Amount);
            var VatAmount = Math.Round(Amount, 2);

            DependencyService.Get<IApplePayAuthorizer>().IsPaymentFromDashboard(false);
            return DependencyService.Get<IApplePayAuthorizer>().AuthorizePayment(VatAmount, AppResources.ZAmount); 
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

                                _navigationService.NavigateTo(App.VatReturnNewSuccessPageView, paymentInfo);

                            });
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                //   await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                                _navigationService.GoBack();
                            });

                        }



                        //Device.BeginInvokeOnMainThread(() =>
                        //{

                        //    //_navigationService.NavigateTo(App.ZakatReturnNewSuccessPageView, PaymentData.d.PayRef);
                        //    //await App.Current.MainPage.Navigation.PushAsync(new PaymentProcessWebview());


                        //    _navigationService.NavigateTo(App.VatReturnNewSuccessPageView, "");

                        //});
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
                    var response = await WebServiceManager.GAZTGetVATDeclarationSADADNumber(VATDeclarationData.d.Fbnum);
                    PopToRootPage();
                    if (response != null && response.d != null && response.d.results.Count != 0)
                    {
                        SadadNumber = response.d.results[0].Sopbel;
                        AmountPayable = response.d.results[0].Betrh;
                        if (!string.IsNullOrEmpty(SadadNumber))
                        {
                            if (VATDeclarationData.d.RefundFg == "1")
                            {
                                IsSadadNumberVisible = false;
                            }
                            else
                            {
                                IsSadadNumberVisible = true;
                            }
                            IsButtonVisible = true;
                            if (VATDeclarationData.d.EstimatedFg == "X")
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
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
            }
        }
    }
}
