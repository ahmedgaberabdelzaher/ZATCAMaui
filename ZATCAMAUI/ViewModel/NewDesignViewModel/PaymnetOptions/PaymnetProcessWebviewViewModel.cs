using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using RGPopup.Maui.Services;
using System.Windows.Input;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models.PaymentModel;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.PaymnetOptions
{

    public class PaymnetProcessWebviewViewModel : BaseViewModel
    {
        public ICommand GoBackClick { get; set; }
        public int PaymentType;

        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;


        public MadaPaymentResponse _paymentData = null;
        public MadaPaymentResponse PaymentData
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
        #region Constructor

        public PaymnetProcessWebviewViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
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

            GoBackClick = new Command(async () =>
            {
               await CancelPaymentService();
            });

        }


        #endregion


        public async Task UpdateMadaPaymentDetails(string caseGuid)
        {

            string caseGuidNumber = caseGuid;

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
                PaymentData = await WebServiceManager.GAZTUpdateMadaPaymentDetails(caseGuidNumber, platform);


                if (PaymentData != null && PaymentData.d != null)
                {

                    if (PaymentData.d.FinalStat == "02")
                    {
                        IsLoading = false;
                        MainThread.BeginInvokeOnMainThread(() =>
                        {

                            //_navigationService.NavigateTo(App.ZakatReturnNewSuccessPageView, PaymentData.d.PayRef);
                            //await App.Current.MainPage.Navigation.PushAsync(new PaymentProcessWebview());
                            PaymentSucess paymentInfo = new PaymentSucess();
                            paymentInfo.Paymentref = PaymentData.d.PayRef;
                            if (PaymentData.d.PerslTxt != null)
                            {
                                paymentInfo.Period = PaymentData.d.PerslTxt;
                            }

                            if (PaymentType == 0)
                            {
                                _navigationService.NavigateTo(App.ZakatReturnNewSuccessPageView, paymentInfo);
                            }
                            else if (PaymentType == 1)
                            {
                                _navigationService.NavigateTo(App.VatReturnNewSuccessPageView, paymentInfo);
                            }
                            else if (PaymentType == 2)
                            {

                                _navigationService.NavigateTo(App.MyBillsSuccessPageView, paymentInfo);

                            }

                        });

                    }
                    else if (PaymentData.d.FinalStat == "01")
                    {



                        _ = UpdateMadaPaymentDetails(caseGuidNumber);

                    }



                }



            }
            catch (GAZTValidateMadaPaymentException ex)
            {
                IsLoading = false;
                MainThread.BeginInvokeOnMainThread(async () =>
                {

                    var message = ex.Message.Substring(0, 1).ToUpper() + ex.Message.Substring(1).ToLower();

                    //removed based on CR6100 Hyper care
                    //await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(message));
                    _navigationService.GoBack();
                });
            }
            catch (InternetException ex)
            {
                IsLoading = false;
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    //   await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                    _navigationService.GoBack();
                });
            }


        }

        public async Task CancelPaymentService()
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
                var CancelAPI = await WebServiceManager.GAZTCancelPayment(App.PaymentGuid, platform);

                IsLoading = false;

                MainThread.BeginInvokeOnMainThread(() =>
            {
                _navigationService.GoBack();
            });





            }
            catch (GAZTValidatePaymentInProcessException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    //_navigationService.GoBack();
                });
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    //   await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                    _navigationService.GoBack();
                });
            }

        }
    }
}
