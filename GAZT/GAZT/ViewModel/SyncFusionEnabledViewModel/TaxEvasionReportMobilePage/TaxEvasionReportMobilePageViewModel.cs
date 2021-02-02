using EGAZT.Manager;
using EGAZT.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.TaxEvasionReportMobilePage_ViewModel
{
    [Preserve(AllMembers = true)]
    public class TaxEvasionReportMobilePageViewModel : ViewModelBase
    {
        public ICommand BackButtonClicked { get; set; }
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
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
                this.RaisePropertyChanged("IsVerifyEnable");
            }
        }
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
                this.RaisePropertyChanged("IsLoading");
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
                _mobileNumber =  value;         
                this.RaisePropertyChanged("MobileNumber");
            }
        }

        public string MobileNumberPrefix { get; set; }

        public TaxEvasionReportMobilePageViewModel(INavigationService navigationService, IDialogService dialogService)
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
            this.BackButtonClicked = new Command(this.BackButtonClick);
            this.VerifyCommand = new Command(this.VerifyCommandClick);
            this.RegisterCommand = new Command(this.RegisterCommandClick);

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
                            App.TaxEvasionUserData  = new TaxEvasionUserRegistrationResponseData();
                            App.TaxEvasionUserData.Mobile = tesmobnoscreen.MobileNumber;
                            App.TaxEvasionUserData.LoginKey = taxEvasionSendSmsResponseModel.Data.Key;

                            _navigationService.NavigateTo(App.OTPPageView, tesmobnoscreen);
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
            catch(Exception ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
//_navigationService.NavigateTo();
        }
    }
}
