using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using EGAZT.Models.InstalmentPlanModel;
using EGAZT.Views.NewDesign.InstalmentPlan;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel.VATInstalmentPlanViewModel
{
    [Preserve(AllMembers = true)]
    public class VATInstalmentNotesPageViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        public Command OnResendOTPClicked { get; set; }

        public System.Timers.Timer otpTimer;
        public int countDownSeconds;

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
        private bool _notesPageVisible = false;
        public bool NotesPageVisible
        {
            get
            {
                return _notesPageVisible;
            }
            set
            {
                if (_notesPageVisible == value) return;

                _notesPageVisible = value;
                RaisePropertyChanged("NotesPageVisible");
            }
        }
        private bool _isOtpPageVisible = false;
        public bool IsOtpPageVisible
        {
            get
            {
                return _isOtpPageVisible;
            }
            set
            {
                if (_isOtpPageVisible == value) return;

                _isOtpPageVisible = value;
                RaisePropertyChanged("IsOtpPageVisible");
            }
        }

        private bool _isConfirmationPageVisible = false;
        public bool IsConfirmationPageVisible
        {
            get
            {
                return _isConfirmationPageVisible;
            }
            set
            {
                if (_isConfirmationPageVisible == value) return;

                _isConfirmationPageVisible = value;
                RaisePropertyChanged("IsConfirmationPageVisible");
            }
        }

        private string _noteEditor = string.Empty;
        public string NoteEditor
        {
            get
            {
                return _noteEditor;
            }
            set
            {
                if (_noteEditor == value) return;

                _noteEditor = value;
                RaisePropertyChanged("NoteEditor");
            }
        }
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

                RaisePropertyChanged("OTPFirstDigit");
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
                RaisePropertyChanged("OTPSecondDigit");
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
                RaisePropertyChanged("OTPThirdDigit");
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
                RaisePropertyChanged("OTPFourthDigit");
            }
        }

        private string _enteredOTP = "";
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
                RaisePropertyChanged("EnteredOTP");
            }
        }

        private string _nextSubmitText = AppResources.ZZNext;
        public string NextSubmitText
        {
            get
            {
                return _nextSubmitText;
            }
            set
            {
                if (_nextSubmitText == value) return;

                _nextSubmitText = value;
                RaisePropertyChanged("NextSubmitText");
            }
        }

        private string _vATRevokeMessage;
        public string VATRevokeMessage
        {
            get
            {
                return _vATRevokeMessage;
            }
            set
            {
                if (_vATRevokeMessage == value) return;

                _vATRevokeMessage = value;
                RaisePropertyChanged("VATRevokeMessage");
            }
        }

        private string _applicationNUmber = App.VatRevokeFBNum;
        public string ApplicationNumber
        {
            get
            {
                return _applicationNUmber;
            }
            set
            {
                if (_applicationNUmber == value) return;

                _applicationNUmber = value;
                RaisePropertyChanged("ApplicationNumber");
            }
        }

        private string _LblCountDownTimer;
        public string LblCountDownTimer
        {
            get
            {
                return _LblCountDownTimer;
            }
            set
            {
                if (_LblCountDownTimer == value) return;

                _LblCountDownTimer = value;
                RaisePropertyChanged("LblCountDownTimer");
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
                if (_mobileNumber == value) return;

                _mobileNumber = value;
                RaisePropertyChanged("MobileNumber");
            }
        }



        private bool _isResendOTPEnabled;
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
                RaisePropertyChanged("IsResendOTPEnabled");
            }
        }

        private Color _resendOTPTextColor = (Color)Application.Current.Resources["ResendOTPTextColor"];
        public Color ResendOTPTextColor
        {
            get
            {
                return _resendOTPTextColor;
            }
            set
            {
                if (_resendOTPTextColor == value) return;

                _resendOTPTextColor = value;
                RaisePropertyChanged("ResendOTPTextColor");
            }
        }


        private void OnCountDownTimedOTPEvent(object sender, ElapsedEventArgs e)
        {
            countDownSeconds--;

            /*if (countDownSeconds <= 9)
                LblCountDownTimer = "0:0" + countDownSeconds.ToString();
            else
                LblCountDownTimer = "0:" + countDownSeconds.ToString();*/


            if (countDownSeconds <= 9 && countDownSeconds > 0)
                LblCountDownTimer = "0:0" + countDownSeconds.ToString();
            else if (countDownSeconds > 60)
            {
                int countDownSecondsL = countDownSeconds - 60;
                LblCountDownTimer = "1:" + countDownSecondsL.ToString();

                if (countDownSecondsL <= 9)
                    LblCountDownTimer = "1:0" + countDownSecondsL.ToString();
            }
            else
                LblCountDownTimer = "0:" + countDownSeconds.ToString();


            // Stop timer
            if (countDownSeconds <= 0 && IsOtpPageVisible)
            {
                //ContinueButtonEnability = false;
                IsResendOTPEnabled = true;
                ResendOTPTextColor = (Color)Application.Current.Resources["Primary"];

                otpTimer.Stop();
            }
        }

        public void StartOTPTimer()
        {
            // Timer            
            otpTimer = new System.Timers.Timer();
            otpTimer.Interval = 1000;

            // Event
            otpTimer.Elapsed += OnCountDownTimedOTPEvent;

            countDownSeconds = 120;
            LblCountDownTimer = "0." + countDownSeconds.ToString();

            otpTimer.Start();
        }

        #endregion
        public ICommand ContinueClick { get; set; }
        public ICommand CancelButton { get; set; }

        public VATInstalmentNotesPageViewModel(INavigationService navigationService, IDialogService dialogService)
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
            ContinueClick = new Command(async () =>
            {
                await ContinueAsync();
            });
            CancelButton = new Command(async () =>
            {
                await PopupNavigation.Instance.PopAsync();
            });
            IsConfirmationPageVisible = true;


            OnResendOTPClicked = new Command(async () =>
            {
                if (IsResendOTPEnabled)
                {
                    callOTPRequest();
                }

            });
            VATRevokeMessage = string.Format(AppResources.VATRevokeMessage, App.VatRevokeFBNum);

        }

        private bool CheckOnlyNumber(char letter)
        {
            if ((letter >= 48 && letter <= 57))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task ContinueAsync()
        {
            if (IsConfirmationPageVisible)
            {
                IsConfirmationPageVisible = false;
                NotesPageVisible = true;
                IsOtpPageVisible = false;
            }
            else if (NotesPageVisible)
            {
                if (NoteEditor.Length > 0)
                {
                    callOTPRequest();
                    IsConfirmationPageVisible = false;
                    NotesPageVisible = false;
                    IsOtpPageVisible = true;
                    NextSubmitText = AppResources.Submit;
                    IsResendOTPEnabled = false;
                    ResendOTPTextColor = (Color)Application.Current.Resources["DarkGrayTextColor"];
                }
                else
                {
                    await _dialogService.ShowMessage(AppResources.VATRevokNotesError, AppResources.Information);
                    return;
                }



            }
            else if (IsOtpPageVisible)
            {
                EnteredOTP = OTPFirstDigit + OTPSecondDigit + OTPThirdDigit + OTPFourthDigit;
                if (EnteredOTP.Length != 4)
                {
                    await _dialogService.ShowMessage(AppResources.PleaseenterOTP, AppResources.Information);
                    return;
                }
                Tuple<string, string> data = new Tuple<string, string>(EnteredOTP, NoteEditor.ToString());

                MessagingCenter.Send<Object, Tuple<string, string>>(this, "Revoke_VAT_Submit", data);


                //Close here
            }
            //await PopupNavigation.Instance.PopAsync();
            //MessagingCenter.Send<Object, Boolean>(this, "ISCallBackFromConfirmmessage", true);
        }

        private void callOTPRequest()
        {
            MessagingCenter.Send<Object, Boolean>(this, "Revoke_VAT_OTP", true);
            IsResendOTPEnabled = false;
            StartOTPTimer();
            ResendOTPTextColor = (Color)Application.Current.Resources["DarkGrayTextColor"];

        }
    }
}

