using System;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.TaxEvasionViewModels
{
    public class TaxEvasionVerifyMobileViewModel : BaseViewModel
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        public ICommand SendOTPCommand { get; set; }
        public ICommand ResendOTPCommand { get; set; }

        #region proprety
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
        private bool _isShowMobileInput = false;
        public bool IsShowMobileInput
        {
            get
            {
                return _isShowMobileInput;
            }
            set
            {
                _isShowMobileInput = value;
                RaisePropertyChanged("IsShowMobileInput");
            }
        }
        private bool _isShowOTPInput = false;
        public bool IsShowOTPInput
        {
            get
            {
                return _isShowOTPInput;
            }
            set
            {
                _isShowOTPInput = value;
                RaisePropertyChanged("IsShowOTPInput");
            }
        }
        private string _pageTitle = "";
        public string PageTitle
        {
            get
            {
                return _pageTitle;
            }
            set
            {
                _pageTitle = value;
                RaisePropertyChanged("PageTitle");
            }
        }
        private string _pageTitleTag = "";
        public string PageTitleTag
        {
            get
            {
                return _pageTitleTag;
            }
            set
            {
                _pageTitleTag = value;
                RaisePropertyChanged("PageTitleTag");
            }
        }
        private string _mobileNumber = "";
        public string MobileNumber
        {
            get
            {
                return _mobileNumber;
            }
            set
            {
                _mobileNumber = value;
                RaisePropertyChanged("MobileNumber");
            }
        }
        private string _mobileNumberPrefix = "";
        public string MobileNumberPrefix
        {
            get
            {
                return _mobileNumberPrefix;
            }
            set
            {
                _mobileNumberPrefix = value;
                RaisePropertyChanged("MobileNumberPrefix");
            }
        }
        #endregion
        public TaxEvasionVerifyMobileViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
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

            IsShowMobileInput = true;
            IsShowOTPInput = false;

            SendOTPCommand = new Xamarin.Forms.Command(async () =>
            {
                await sendOTPAsync();
            });
            ResendOTPCommand = new Xamarin.Forms.Command(() =>
            {
                ShowMobileForm();
            });
        }


        #region Methods
        public void ShowMobileForm()
        {
            IsShowMobileInput = true;
            IsShowOTPInput = false;
            PageTitle = AppResources.ZTEReportReportScreenTitle;
            PageTitleTag = AppResources.EnterNewMobileNumber;
        }
        public void ShowOTPForm()
        {
            IsShowMobileInput = false;
            IsShowOTPInput = true;
            PageTitle = AppResources.VerificationCode;
            PageTitleTag = AppResources.NDPleaseEnterVerificationSenttomobile;
        }

        public async Task sendOTPAsync()
        {
            try
            {

                ComingToOTPVerificationScreenFromAndNavigatingTo tesmobnoscreen = new ComingToOTPVerificationScreenFromAndNavigatingTo();
                tesmobnoscreen.tes = "1";
                tesmobnoscreen._ComingToOTPVerificationScreenFrom = ComingToOTPVerificationScreenFrom.IsTes;

                if (MobileNumber.Length == 9)
                {
                    tesmobnoscreen.MobileNumber = MobileNumberPrefix + MobileNumber;

                    TaxEvasionSendSmsModel taxEvasionSendSmsModel = new TaxEvasionSendSmsModel();
                    taxEvasionSendSmsModel.mobile = tesmobnoscreen.MobileNumber;

                    try
                    {
                        await Task.Run(() =>
                        {
                            IsLoading = true;
                        });

                        TaxEvasionSendSmsResponseModel taxEvasionSendSmsResponseModel = await WebServiceManager.GAZTTaxEvasionSendSms(taxEvasionSendSmsModel);

                        await Task.Run(() =>
                        {
                            IsLoading = false;
                        });

                        if (taxEvasionSendSmsResponseModel.Status == true)
                        {
                            App.TaxEvasionUserData = new TaxEvasionUserRegistrationResponseData();
                            App.TaxEvasionUserData.Mobile = tesmobnoscreen.MobileNumber;
                            App.TaxEvasionUserData.LoginKey = taxEvasionSendSmsResponseModel.Data.Key;

                            ShowOTPForm();
                            //_navigationService.NavigateTo(App.OTPPageView, tesmobnoscreen);
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                            });
                        }
                    }
                    catch (GAZTException gex)
                    {
                        // Handle the GAZT custom exception.
                        string MessageForTheUser = gex.Message;
                        if (gex is GAZTInvalidDataException)
                        {
                            MessageForTheUser = AppResources.ZZSomethingwentwrong;
                        }

                        if (gex is GAZTNetworkConnectivityIssueException)
                        {
                            MessageForTheUser = AppResources.NetworkConnectivityIssue;
                        }
                        else if (gex is GAZTInternetException)
                        {
                            MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                        }
                        else if (gex is GAZTSessionExpiredException)
                        {
                            MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                        }

                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await Task.Run(() =>
                            {
                                IsLoading = false;
                            });

                            await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                            //viewModel._navigationService.GoBack();
                        });
                    }
                    catch (Exception ex)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await Task.Run(() =>
                            {
                                IsLoading = false;
                            });

                            await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                            //viewModel._navigationService.GoBack();
                        });
                    }


                }
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {

                    await _dialogService.ShowMessage(AppResources.NetworkConnectivityIssue, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        #endregion
    }
}
