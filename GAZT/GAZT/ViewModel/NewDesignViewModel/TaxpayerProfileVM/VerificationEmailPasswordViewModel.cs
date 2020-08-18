using System;
using System.Threading.Tasks;
using System.Timers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;

namespace EGAZT.ViewModel.NewDesignViewModel.TaxpayerProfileVM
{
    public class VerificationEmailPasswordViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public UpdateEmailDataModel _updateEmailData;

        public System.Timers.Timer otpTimer;
        public int countDownSeconds;
        public string EnteredOTP = string.Empty;
        #endregion

        #region Properties
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

        private string _CurrentPasswordEntry;
        public string CurrentPasswordEntry
        {
            get
            {
                return _CurrentPasswordEntry;
            }
            set
            {
                _CurrentPasswordEntry = value;
                RaisePropertyChanged("CurrentPasswordEntry");
            }
        }

        private string _NewPasswordEntry;
        public string NewPasswordEntry
        {
            get
            {
                return _NewPasswordEntry;
            }
            set
            {
                _NewPasswordEntry = value;
                RaisePropertyChanged("NewPasswordEntry");
            }
        }

        private string _ConfirmPasswordEntry;
        public string ConfirmPasswordEntry
        {
            get
            {
                return _ConfirmPasswordEntry;
            }
            set
            {
                _ConfirmPasswordEntry = value;
                RaisePropertyChanged("ConfirmPasswordEntry");
            }
        }
        #endregion

        public VerificationEmailPasswordViewModel(INavigationService navigationService, IDialogService dialogService)
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

        // * Call API
        public async Task<TaxPayerProfile> ChangePassword()
        {
            /*await Task.Run(() =>
            {
                IsLoading = true;
            });*/

            TaxPayerProfile TP = null;
            string lang = "EN";

            if (App.IsArabic == true) { lang = "AR"; }

            try
            {
                TP = await WebServiceManager.GAZTValidateOTPForEmail(lang,
                                                                    EnteredOTP,
                                                                    App.TP.Tin,
                                                                    _updateEmailData.CurrentEmail, _updateEmailData.NewEmail,
                                                                    CurrentPasswordEntry, NewPasswordEntry);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception : ", ex.ToString());
            }

            return TP;
        }
    }

    // * Need Updated Email Data From Update Email Page
    public class UpdateEmailDataModel
    {
        public string CurrentEmail { get; set; }
        public string NewEmail { get; set; }
    }
}
