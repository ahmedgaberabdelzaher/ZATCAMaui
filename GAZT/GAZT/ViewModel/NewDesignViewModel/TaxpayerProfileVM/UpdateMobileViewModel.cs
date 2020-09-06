using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Timers;
using EGAZT.Models;
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
        private string _maxDigids = "9";
        public string MaxDigids
        {
            get
            {
                return _maxDigids;
            }
            set
            {
                _maxDigids = value;
                RaisePropertyChanged("MaxDigids");
            }
        }
        private string _CountryCode = "+966";
        public string CountryCode
        {
            get
            {
                return _CountryCode;
            }
            set
            {
                _CountryCode = value;
                if (_CountryCode != null)
                {
                    MaxDigids = (14 - _CountryCode.Length).ToString();
                }
                else
                {
                    MaxDigids = "15";
                }

                RaisePropertyChanged("CountryCode");
            }
        }
        private string _mobileCountryCode = string.Empty;
        public string MobileCountryCode
        {
            get
            {
                return _mobileCountryCode;
            }
            set
            {
                _mobileCountryCode = value;
                RaisePropertyChanged("MobileCountryCode");
            }
        }
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

                BtnEnableFlag = false;
                if (value.Length > 0)
                    BtnEnableFlag = true;

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
            IsLoading = true;
            bool callAPIFlag = false;
            string lang = "EN";
            if (App.IsArabic == true) { lang = "AR"; }

            string currentMobileNumber = MobileNumberFormate(CurrentMobileNumberEntryText);
            string newMobileNumber = NewMobileNumberFormate(NewMobileNumberEntryText);
            string MobileCountry = string.Empty;
            if (MobileCountryCode == string.Empty)
            {
                MobileCountry = "SA";
            }
            else
            {
                MobileCountry = MobileCountryCode;
            }
            try
            {
                await Task.Run(async () =>
                {
                    callAPIFlag = await WebServiceManager.GAZTValidateMobileNumber(lang, App.TP.Tin,
                                                                                currentMobileNumber,
                                                                                newMobileNumber, MobileCountry);
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                IsLoading = false;
                System.Diagnostics.Debug.WriteLine("Exception : ", ex.Message);
                Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                });
            }

            return callAPIFlag;
        }

        public async Task<TaxPayerProfile> VarifyOTPToUpdateMobileNumber()
        {
            IsLoading = true;
            TaxPayerProfile TP = null;
            string lang = "EN";
            if (App.IsArabic == true) { lang = "AR"; }

            try
            {
                await Task.Run(async () =>
                {
                    string currentMobileNumber = MobileNumberFormate(CurrentMobileNumberEntryText);
                    string newMobileNumber = NewMobileNumberFormate(NewMobileNumberEntryText);
                    string MobileCountry = string.Empty;
                    if (MobileCountryCode == string.Empty)
                    {
                        MobileCountry = "SA";
                    }
                    else
                    {
                       MobileCountry = MobileCountryCode;
                    }
                    TP = await WebServiceManager.GAZTValidateOTPForMobileNumber(lang, EnteredOTP,
                                                                                App.TP.Tin,
                                                                                currentMobileNumber,
                                                                                newMobileNumber, MobileCountry);
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                IsLoading = false;
                System.Diagnostics.Debug.WriteLine("VERIFY OTP ERROR : {0}", ex.ToString());
                Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                });
            }

            return TP;
        }

        private string MobileNumberFormate(string mobileNumber)
        {
         
            return App.TP.Mobile;
        }
        private string NewMobileNumberFormate(string mobileNumber)
        {
            string formatedCountryCode = CountryCode.Replace("+", "");
            return formatedCountryCode + NewMobileNumberEntryText;
        }
    }
}