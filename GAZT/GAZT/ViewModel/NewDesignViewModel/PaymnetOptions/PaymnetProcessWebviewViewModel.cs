using EGAZT.Models.EnumModels;
using EGAZT.Models.PaymentModel;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    [Preserve(AllMembers = true)]
    public class PaymnetProcessWebviewViewModel : ViewModelBase
    {
        public ICommand GoBackClick { get; set; }
        public int PaymentType;

        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        private bool _isLoading = false;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }


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

        public PaymnetProcessWebviewViewModel(INavigationService navigationService, IDialogService dialogService)
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
                CancelPaymentService();
            });

        }


        #endregion


        public async Task UpdateMadaPaymentDetails(string caseGuid)
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
                PaymentData = await WebServiceManager.GAZTUpdateMadaPaymentDetails(caseGuid, platform);


                if (PaymentData != null && PaymentData.d != null)
                {

                    if (PaymentData.d.PayRef != null)
                    {
                        //Device.BeginInvokeOnMainThread(() =>
                        //{
                        //    var _navigation = Application.Current.MainPage.Navigation;
                        //    foreach (var item in _navigation.NavigationStack)
                        //    {
                        //        if (item.GetType().Name == App.PaymentProcessWebview)
                        //        {
                        //            _navigation.RemovePage(item);
                        //            break;
                        //        }
                        //    }
                        //    _navigationService.GoBack();
                        //});

                        Device.BeginInvokeOnMainThread(() =>
                        {

                            //_navigationService.NavigateTo(App.ZakatReturnNewSuccessPageView, PaymentData.d.PayRef);
                            //await App.Current.MainPage.Navigation.PushAsync(new PaymentProcessWebview());

                            if (PaymentType == 0)
                            {
                                _navigationService.NavigateTo(App.ZakatReturnNewSuccessPageView, PaymentData.d.PayRef);
                            }
                            else if (PaymentType == 1)
                            {
                                _navigationService.NavigateTo(App.VatReturnNewSuccessPageView, PaymentData.d.PayRef);
                            }
                            else if (PaymentType == 2)
                            {

                                _navigationService.NavigateTo(App.MyBillsSuccessPageView, PaymentData.d.PayRef);

                                //_navigationService.GoBack();
                                /*var _navigation = Application.Current.MainPage.Navigation;
                                foreach (var item in _navigation.NavigationStack)
                                {
                                    if (item.GetType().Name == App.GAZTNewDesignMyBillsPageView)
                                    {
                                        _navigation.RemovePage(item);
                                        break;
                                    }
                                }
                                foreach (var item in _navigation.NavigationStack)
                                {
                                    if (item.GetType().Name == App.PaymentProcessWebview)
                                    {
                                        _navigation.RemovePage(item);
                                        break;
                                    }
                                }
                                _navigationService.NavigateTo(App.GAZTNewDesignDashBoardPageView);*/
                            }

                        });

                    }


                }

                IsLoading = false;

            }
            catch (GAZTValidatePaymentInProcessException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                    //await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
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

                //IsLoading = true;
                //var platform = "";

                //if (Device.RuntimePlatform == Device.iOS)
                //{
                //    platform = "C4";
                //}
                //else if (Device.RuntimePlatform == Device.Android)
                //{
                //    platform = "C3";
                //}
               // await WebServiceManager.GAZTCancelPayment(App.PaymentGuid, platform);

                  Device.BeginInvokeOnMainThread(() =>
            {
                _navigationService.GoBack();
            });



               // IsLoading = false;

            }
            catch (GAZTValidatePaymentInProcessException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        //_navigationService.GoBack();
                    });
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                        //   await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                    _navigationService.GoBack();
                });
            }

        }
    }
}
