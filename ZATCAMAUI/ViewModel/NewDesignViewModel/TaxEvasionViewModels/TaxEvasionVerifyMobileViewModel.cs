using System.Windows.Input;

using Mopups.Services;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.TaxEvasionViewModels
{

    public class TaxEvasionVerifyMobileViewModel : BaseViewModel
    {
        public ICommand SendOTPCommand { get; set; }
        public ICommand ResendOTPCommand { get; set; }
        public ICommand OnBackButtonClicked { get; set; }

        public TaxEvasionVerifySmsResponseModel taxEvasionVerifySmsResponseModel;

        #region proprety
        private Color _ResendOtpButtonColor = (Color)Application.Current.Resources["NeutralGreay"];
        public Color ResendOtpButtonColor
        {
            get
            {
                return _ResendOtpButtonColor;
            }
            set
            {
                if (_ResendOtpButtonColor == value) return;
                _ResendOtpButtonColor = value;
                OnPropertyChanged("ResendOtpButtonColor");
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
                if (_isShowMobileInput == value) return;

                _isShowMobileInput = value;
                OnPropertyChanged("IsShowMobileInput");
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
                if (_isShowOTPInput == value) return;

                _isShowOTPInput = value;
                OnPropertyChanged("IsShowOTPInput");
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
                if (_pageTitle == value) return;

                _pageTitle = value;
                OnPropertyChanged("PageTitle");
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
                if (_pageTitleTag == value) return;

                _pageTitleTag = value;
                OnPropertyChanged("PageTitleTag");
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
                if (_mobileNumber == value) return;

                _mobileNumber = value;
                OnPropertyChanged("MobileNumber");
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
                if (_mobileNumberPrefix == value) return;

                _mobileNumberPrefix = value;
                OnPropertyChanged("MobileNumberPrefix");
            }
        }
        // * OTP Verification Properties
        private string _oTPFirstDigit;
        public string OTPFirstDigit
        {
            get
            {
                return _oTPFirstDigit;
            }
            set
            {
                if (_oTPFirstDigit == value) return;

                _oTPFirstDigit = value;
                if (!string.IsNullOrEmpty(OTPFirstDigit))
                {
                    bool isNumberEntered = CheckOnlyNumber(OTPFirstDigit[0]);
                    if (!isNumberEntered)
                    {
                        OTPFirstDigit = string.Empty;
                    }
                }

                OnPropertyChanged("OTPFirstDigit");
            }
        }

        private string _OTPSecondDigit;
        public string OTPSecondDigit
        {
            get
            {
                return _OTPSecondDigit;
            }
            set
            {
                if (_OTPSecondDigit == value) return;

                _OTPSecondDigit = value;
                if (!string.IsNullOrEmpty(OTPSecondDigit))
                {
                    bool isNumberEntered = CheckOnlyNumber(OTPSecondDigit[0]);
                    if (!isNumberEntered)
                    {
                        OTPSecondDigit = string.Empty;
                    }
                }
                OnPropertyChanged("OTPSecondDigit");
            }
        }

        private string _OTPThirdDigit;
        public string OTPThirdDigit
        {
            get
            {
                return _OTPThirdDigit;
            }
            set
            {
                if (_OTPThirdDigit == value) return;

                _OTPThirdDigit = value;
                if (!string.IsNullOrEmpty(OTPThirdDigit))
                {
                    bool isNumberEntered = CheckOnlyNumber(OTPThirdDigit[0]);
                    if (!isNumberEntered)
                    {
                        OTPThirdDigit = string.Empty;
                    }
                }
                OnPropertyChanged("OTPThirdDigit");
            }
        }

        private string _OTPFourthDigit;
        public string OTPFourthDigit
        {
            get
            {
                return _OTPFourthDigit;
            }
            set
            {
                if (_OTPFourthDigit == value) return;

                _OTPFourthDigit = value;
                if (!string.IsNullOrEmpty(OTPFourthDigit))
                {
                    bool isNumberEntered = CheckOnlyNumber(OTPFourthDigit[0]);
                    if (!isNumberEntered)
                    {
                        OTPFourthDigit = string.Empty;
                    }
                }
                OnPropertyChanged("OTPFourthDigit");
            }
        }
        // * End
        private string _enteredOTP = string.Empty;
        public string EnteredOTP
        {
            get
            {
                return _enteredOTP;
            }
            set
            {
                if (_enteredOTP == value) return;

                _enteredOTP = value;
                OnPropertyChanged("EnteredOTP");
            }
        }
        private string _oTPSentOnThisMobileNumber = string.Empty;
        public string OTPSentOnThisMobileNumber
        {
            get
            {
                return _oTPSentOnThisMobileNumber;
            }
            set
            {
                if (_oTPSentOnThisMobileNumber == value) return;

                _oTPSentOnThisMobileNumber = value;
                OnPropertyChanged("OTPSentOnThisMobileNumber");
            }
        }
        private string _EncriptedMobileNumber = string.Empty;
        public string EncriptedMobileNumber
        {
            get
            {
                return _EncriptedMobileNumber;
            }
            set
            {
                if (_EncriptedMobileNumber == value) return;

                _EncriptedMobileNumber = value;
                OnPropertyChanged("EncriptedMobileNumber");
            }
        }
        //timer

        private string _lblCountDownTimer = string.Empty;
        public string LblCountDownTimer
        {
            get
            {
                return _lblCountDownTimer;
            }
            set
            {
                if (_lblCountDownTimer == value) return;

                _lblCountDownTimer = value;
                OnPropertyChanged("LblCountDownTimer");
            }
        }
        private bool _isResendOTPEnabled = false;
        public bool IsResendOTPEnabled
        {
            get
            {
                return _isResendOTPEnabled;
            }
            set
            {
                if (_isResendOTPEnabled == value) return;

                _isResendOTPEnabled = value;
                if (_isResendOTPEnabled)
                {
                    ResendOtpButtonColor = Colors.Green;
                }
                else
                {
                    ResendOtpButtonColor = (Color)Application.Current.Resources["NeutralGreay"];
                }
                OnPropertyChanged("IsResendOTPEnabled");
            }
        }
        private bool _isTimerCancel = false;
        public bool IsTimerCancel
        {
            get
            {
                return _isTimerCancel;
            }
            set
            {
                if (_isTimerCancel == value) return;

                _isTimerCancel = value;
                OnPropertyChanged("IsTimerCancel");
            }
        }
        //end timer
        #endregion
        public TaxEvasionVerifyMobileViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

            IsShowMobileInput = true;
            IsShowOTPInput = false;
            OnBackButtonClicked = new Command(async () =>
            {
                if (App.TP != null)
                {
                    if (!string.IsNullOrEmpty(App.TP.TIN))
                    {
                        await Task.Run(() =>
                        {
                            App.HasToRefreshLoaderOnDashboard = true;
                        });
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            _navigationService.NavigateTo(App.GAZTNewDesignDashBoardPageView);
                        });
                    }
                    else
                    {
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            _navigationService.GoBack();
                        });

                    }
                }
                else
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        _navigationService.GoBack();
                    });

                }
                // 
                _navigationService.GoBack();
            });
            SendOTPCommand = new Command(async () =>
            {
                IsTimerCancel = true;
                IsResendOTPEnabled = false;
                await sendOTPAsync();
            });
            ResendOTPCommand = new Command(async () =>
            {
                //ShowMobileForm();
                if (IsResendOTPEnabled)
                {
                    IsTimerCancel = true;
                    IsResendOTPEnabled = false;
                    ClearOTPForm();
                    await sendOTPAsync();
                    StartTimer(0, 2, 0);
                }


            });
        }


        #region Methods
        public void ShowMobileForm()
        {
            IsShowMobileInput = true;
            IsShowOTPInput = false;
            PageTitle = AppResources.NDTaxEvasion;
            PageTitleTag = AppResources.EnterNewMobileNumber;
        }
        private void ShowOTPForm()
        {
            IsShowMobileInput = false;
            IsShowOTPInput = true;
            PageTitle = AppResources.VerificationCode;
            PageTitleTag = AppResources.NDPleaseEnterVerificationSenttomobile;
            IsTimerCancel = false;
            StartTimer(0, 2, 0);
        }
        private bool CheckOnlyNumber(char letter)
        {
            if (letter >= 48 && letter <= 57)
            {
                //tesmobnoscreen.MobileNumber = MobileNumberPrefix + MobileNumber;
                tesmobnoscreen.MobileNumber = "+966" + MobileNumber;

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
                        OTPSentOnThisMobileNumber = AppResources.MobileNumber + " "  ;
                       EncriptedMobileNumber = "xxxxxxxxxx"+ MobileNumber.Substring(MobileNumber.Length - 4, 4);

                        ShowOTPForm();
                        //_navigationService.NavigateTo(App.OTPPageView, tesmobnoscreen);
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
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

                        //await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                        //viewModel._navigationService.GoBack();
                    });
                }
                catch (Exception ex)
                {
                        Console.Write(ex.ToString());
                        Console.Write(ex.StackTrace.ToString());
                        Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Task.Run(() =>
                        {
                            IsLoading = false;
                        });

                      //  await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                        //viewModel._navigationService.GoBack();
                    });
                }


            }
        }
        catch (InternetException ex)
        {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                Device.BeginInvokeOnMainThread(async () =>
            {
                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NetworkConnectivityIssue));
               // await _dialogService.ShowMessage(AppResources.NetworkConnectivityIssue, AppResources.Information);
                _navigationService.GoBack();
            });
        }
        catch (Exception ex)
        {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                Device.BeginInvokeOnMainThread(async () =>
            {
                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
              //  await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                _navigationService.GoBack();
            });
        }
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
                    //tesmobnoscreen.MobileNumber = MobileNumberPrefix + MobileNumber;
                    tesmobnoscreen.MobileNumber = "+966" + MobileNumber;

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
                            OTPSentOnThisMobileNumber = AppResources.MobileNumber + " ";
                            EncriptedMobileNumber = "xxxxxxxxxx" + MobileNumber.Substring(MobileNumber.Length - 4, 4);

                            ShowOTPForm();
                            //_navigationService.NavigateTo(App.OTPPageView, tesmobnoscreen);
                        }
                        else
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
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

                            //await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
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

                        //  await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                        //viewModel._navigationService.GoBack();
                    });
                    }


                }
            }
            catch (InternetException ex)
            {


                MainThread.BeginInvokeOnMainThread(async () =>
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NetworkConnectivityIssue));
                // await _dialogService.ShowMessage(AppResources.NetworkConnectivityIssue, AppResources.Information);
                _navigationService.GoBack();
            });
            }
            catch (Exception)
            {


                MainThread.BeginInvokeOnMainThread(async () =>
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                //  await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                _navigationService.GoBack();
            });
            }
        }

        private void StartTimer(int h, int m, int sec)
        {
            int hour = h;
            int mins = m;
            int counter = sec;
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                if (IsTimerCancel)
                {
                    return false;
                }
                else
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                        {
                            counter = counter - 1;
                            if (counter < 0)
                            {
                                counter = 59;
                                mins = mins - 1;
                                if (mins < 0)
                                {
                                    mins = 59;
                                    hour = hour - 1;
                                    if (hour < 0)
                                    {
                                        hour = 0;
                                        mins = 0;
                                        counter = 0;
                                    }
                                }
                            }

                            LblCountDownTimer = string.Format("{0:00}:{1:00}", mins, counter);
                        });
                    if (hour == 0 && mins == 0 && counter == 0)
                    {
                        IsResendOTPEnabled = true;
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            });

        }

        private void ClearOTPForm()
        {
            OTPFirstDigit = string.Empty;
            OTPSecondDigit = string.Empty;
            OTPThirdDigit = string.Empty;
            OTPFourthDigit = string.Empty;
        }
        public async void VerifyOTP()
        {
            EnteredOTP = OTPFirstDigit + OTPSecondDigit + OTPThirdDigit + OTPFourthDigit;
            if (string.IsNullOrEmpty(EnteredOTP))
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleaseenteraccessCode));
                    //  await _dialogService.ShowMessageBox(AppResources.ZZPleaseenteraccessCode, AppResources.Information);
                });
            }
            else
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });

                try
                {
                    TaxEvasionVerifySmsModel taxEvasionVerifySmsModel = new TaxEvasionVerifySmsModel();
                    taxEvasionVerifySmsModel.key = App.TaxEvasionUserData.LoginKey;
                    taxEvasionVerifySmsModel.code = EnteredOTP;
                    var mobilenumberWithcode = "+966" + MobileNumber;
                    taxEvasionVerifySmsResponseModel = await TaxEvasionWebServiceManager.GAZTTaxEvasionVerifySms(taxEvasionVerifySmsModel, mobilenumberWithcode);

                    if (taxEvasionVerifySmsResponseModel.Status == true)
                    {
                        TaxEvasionSendSmsModel taxEvasionSendSmsModel = new TaxEvasionSendSmsModel();
                        taxEvasionSendSmsModel.mobile = mobilenumberWithcode;

                        try
                        {
                            TaxEvasionUserRegistrationResponseModel taxEvasionUserRegistrationResponseModel = await TaxEvasionWebServiceManager.GAZTTaxEvasionGetUserByMobile(taxEvasionSendSmsModel);
                            App.TaxEvasionUserData = taxEvasionUserRegistrationResponseModel.Data;

                            if (taxEvasionUserRegistrationResponseModel.Status == true)
                            {
                                try
                                {
                                    ClearOTPForm();
                                }
                                catch (Exception)
                                {
                                }
                                await navigateToListPage();
                                MobileNumber = string.Empty;
                            }
                        }
                        catch (Exception)
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await Task.Run(() =>
                                {
                                    IsLoading = false;
                                });
                                ClearOTPForm();
                                _navigationService.NavigateTo(App.TaxEvasionRegistrationPageView);
                            });
                        }
                    }
                    else
                    {
                        ClearOTPForm();
                        // await _dialogService.ShowMessageBox(taxEvasionVerifySmsResponseModel.SmsResponse.Message, AppResources.Information);
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(taxEvasionVerifySmsResponseModel.SmsResponse.Message));
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
                        //await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                        ClearOTPForm();
                    });
                }
                catch (Exception ex)
                {

                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await Task.Run(() =>
                        {
                            IsLoading = false;
                        });

                        if (ex.Message.Contains("The entered code is incorrect") || ex.Message.Contains("الرمز المدخل غير صحيح"))
                        {
                           await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.InvalidOTP));
                        }
                        else
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                        }
                        ClearOTPForm();
                    });
                }

            }
        }
        public async Task navigateToListPage()
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });

            _navigationService.NavigateTo(App.TaxEvasionMyReportsListPageView, "+966571006494");
            ShowMobileForm();
            IsTimerCancel = true;
        }
        #endregion
    }
}
