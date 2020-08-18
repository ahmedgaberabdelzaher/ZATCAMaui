using System;
using System.Threading.Tasks;
using System.Timers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;

namespace EGAZT.ViewModel.NewDesignViewModel.TaxpayerProfileVM
{
    public class UpdateMobileViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        public System.Timers.Timer otpTimer;
        public int countDownSeconds;
        public string EnteredOTP = string.Empty;
        #endregion

        #region Properties
        /*private bool _IsLoading = false;
        public bool IsLoading
        {
            get
            {
                return _IsLoading;
            }
            set
            {
                _IsLoading = value;
                RaisePropertyChanged(() => IsLoading);
            }
        }*/

        private string _CurrentMobileNumberEntryText;
        public string CurrentMobileNumberEntryText
        {
            get { return _CurrentMobileNumberEntryText; }
            set
            {
                _CurrentMobileNumberEntryText = value;
                RaisePropertyChanged("CurrentMobileNumberEntryText");
            }
        }

        private string _NewMobileNumberEntryText;

        public string NewMobileNumberEntryText
        {
            get { return _NewMobileNumberEntryText; }
            set
            {
                _NewMobileNumberEntryText = value;
                RaisePropertyChanged("NewMobileNumberEntryText");
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
        // * End

        private string _LblCountDownTimer;
        public string LblCountDownTimer
        {
            get
            {
                return _LblCountDownTimer;
            }
            set
            {
                _LblCountDownTimer = value;
                RaisePropertyChanged("LblCountDownTimer");
            }
        }

        private string _NewMobileNumberLabel;
        public string NewMobileNumberLabel
        {
            get
            {
                return _NewMobileNumberLabel;
            }
            set
            {
                _NewMobileNumberLabel = value;
                RaisePropertyChanged("NewMobileNumberLabel");
            }
        }
        #endregion

        public UpdateMobileViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null) { throw new ArgumentNullException("navigationService"); }
            _navigationService = navigationService;

            if (dialogService == null) { throw new ArgumentNullException("dialogService"); }
            _dialogService = dialogService;
        }

        // * Private methods
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

        public void StartOTPTimer()
        {
            // Timer            
            otpTimer = new System.Timers.Timer();
            otpTimer.Interval = 1000;

            // Event
            otpTimer.Elapsed += OnCountDownTimedOTPEvent;

            countDownSeconds = 120;

            otpTimer.Enabled = true;
        }

        private void OnCountDownTimedOTPEvent(object sender, ElapsedEventArgs e)
        {
            countDownSeconds--;

            if (countDownSeconds <= 9)
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
            if (countDownSeconds == 0) { otpTimer.Stop(); }
        }

        public async Task<bool> VarifyMobileNumber()
        {
            bool callAPIFlag = false;
            string lang = "EN";
            if (App.IsArabic == true) { lang = "AR"; }

            try
            {
                await Task.Run(async () =>
                {
                    callAPIFlag = await WebServiceManager.GAZTValidateMobileNumber(lang, App.TP.Tin,
                                                                                CurrentMobileNumberEntryText,
                                                                                NewMobileNumberEntryText);
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("UPDATE MOBILE NUMBER ERROR : {0}", ex.ToString());
            }

            return callAPIFlag;
        }

        public async Task<TaxPayerProfile> VarifyOTPToUpdateMobileNumber()
        {
            TaxPayerProfile TP = null;
            string lang = "EN";
            if (App.IsArabic == true) { lang = "AR"; }

            try
            {
                await Task.Run(async () =>
                {
                    TP = await WebServiceManager.GAZTValidateOTPForMobileNumber(lang, "OTP",
                                                                                App.TP.Tin,
                                                                                CurrentMobileNumberEntryText,
                                                                                NewMobileNumberEntryText);
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("VERIFY OTP ERROR : {0}", ex.ToString());
            }

            return TP;
        }
    }
}