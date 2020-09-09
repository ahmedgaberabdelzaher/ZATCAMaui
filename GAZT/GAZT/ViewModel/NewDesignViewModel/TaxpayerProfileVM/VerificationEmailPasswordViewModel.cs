using System;
using System.Threading.Tasks;
using System.Timers;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using Rg.Plugins.Popup.Services;

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

        private bool _IsLoading = false;
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
        }

        private bool _BtnEnableFlag;
        public bool BtnEnableFlag
        {
            get { return _BtnEnableFlag; }
            set
            {
                _BtnEnableFlag = value;
                RaisePropertyChanged("BtnEnableFlag");
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

        private string _OTPSentOnThisMobileNumber;
        public string OTPSentOnThisMobileNumber
        {
            get
            {
                return _OTPSentOnThisMobileNumber;
            }
            set
            {
                _OTPSentOnThisMobileNumber = value;
                RaisePropertyChanged("OTPSentOnThisMobileNumber");
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

                BtnEnableFlag = false;
                if (value.Length > 0)
                    BtnEnableFlag = true;

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

                BtnEnableFlag = false;
                if (value.Length > 0)
                    BtnEnableFlag = true;

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
            IsLoading = true;
            TaxPayerProfile TP = null;
            string lang = "EN";
            if (App.IsArabic == true) { lang = "AR"; }

            try
            {
                System.Diagnostics.Debug.WriteLine("NEW EMAIL : ", _updateEmailData.NewEmail);
                System.Diagnostics.Debug.WriteLine("CONFIRM EMAIL : ", _updateEmailData.CurrentEmail);
                System.Diagnostics.Debug.WriteLine("CURRENT PWD : ", CurrentPasswordEntry);
                System.Diagnostics.Debug.WriteLine("NEW EMAIL : ", NewPasswordEntry);

                TP = await WebServiceManager.GAZTValidateOTPForEmail(lang, EnteredOTP, App.TP.Tin,
                                                                    _updateEmailData.CurrentEmail, _updateEmailData.NewEmail,
                                                                    CurrentPasswordEntry, NewPasswordEntry);
                IsLoading = false;
            }
            catch (Exception ex)
            {
                IsLoading = false;
                System.Diagnostics.Debug.WriteLine("Exception : ", ex.Message);
                ShowValidationPopup(ex.Message);
            }

            return TP;
        }

        public async Task<bool> VarifyEmail()
        {
            IsLoading = true;
            bool APIResponse = false;
            string lang = "EN";
            if (App.IsArabic == true) { lang = "AR"; }

            try
            {
                // API Calls
                await Task.Run(async () =>
                {
                    APIResponse = await WebServiceManager.GAZTGetOTPForEmail(lang, App.TP.Tin, _updateEmailData.CurrentEmail, _updateEmailData.NewEmail);
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                IsLoading = false;
                System.Diagnostics.Debug.WriteLine("Exception : ", ex.Message);
                ShowValidationPopup(ex.Message);
            }

            return APIResponse;
        }

        public void ShowValidationPopup(string sourceString)
        {
            PopUp popUp = new PopUp();
            popUp.Message = sourceString;
            popUp.IsLinkAvailable = false;

            if (App.IsArabic)
                popUp.FlowDirections = "RightToLeft";
            else
                popUp.FlowDirections = "LeftToRight";

            PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }
    }

    // * Need Updated Email Data From Update Email Page
    public class UpdateEmailDataModel
    {
        public string CurrentEmail { get; set; }
        public string NewEmail { get; set; }
    }
}