

using System.Windows.Input;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.TaxEvasionReportMobilePage
{
    public class TaxEvasionReportMobilePageViewModel : BaseViewModel
    {
        public ICommand BackButtonClicked { get; set; }
        public ICommand VerifyCommand { get; set; }
        public ICommand RegisterCommand { get; set; }

        //VerifyCommand
        private bool _isVerifyEnable = false;
        public bool IsVerifyEnable
        {
            get
            {
                return _isVerifyEnable;
            }
            set
            {
                _isVerifyEnable = value;
                OnPropertyChanged("IsVerifyEnable");
            }
        }
        
        private string _mobileNumber;
        public string MobileNumber
        {
            get
            {
                return _mobileNumber;
            }
            set
            {
                _mobileNumber = value;
                OnPropertyChanged("MobileNumber");
            }
        }

        public string MobileNumberPrefix { get; set; }

        public TaxEvasionReportMobilePageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            BackButtonClicked = new Command(BackButtonClick);
            VerifyCommand = new Command(VerifyCommandClick);
            RegisterCommand = new Command(RegisterCommandClick);

        }
        public void BackButtonClick()
        {
            _navigationService.GoBack();
        }

        public async void RegisterCommandClick()
        {
            _navigationService.NavigateTo(App.TaxEvasionRegistrationPageView);
        }

        public async void VerifyCommandClick()
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

                        TaxEvasionSendSmsResponseModel taxEvasionSendSmsResponseModel = await TaxEvasionWebServiceManager.GAZTTaxEvasionSendSms(taxEvasionSendSmsModel);

                        await Task.Run(() =>
                        {
                            IsLoading = false;
                        });

                        if (taxEvasionSendSmsResponseModel.Status == true)
                        {
                            App.TaxEvasionUserData = new TaxEvasionUserRegistrationResponseData();
                            App.TaxEvasionUserData.Mobile = tesmobnoscreen.MobileNumber;
                            App.TaxEvasionUserData.LoginKey = taxEvasionSendSmsResponseModel.Data.Key;

                            _navigationService.NavigateTo(App.OTPPageView, tesmobnoscreen);
                        }
                        else
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
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

                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await Task.Run(() =>
                            {
                                IsLoading = false;
                            });

                            await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                            //viewModel._navigationService.GoBack();
                        });
                    }
                    catch (Exception)
                    {


                        MainThread.BeginInvokeOnMainThread(async () =>
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
                MainThread.BeginInvokeOnMainThread(async () =>
                {

                    await _dialogService.ShowMessage(AppResources.NetworkConnectivityIssue, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
            catch (Exception)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }
    }
}
