using System;
using System.Threading.Tasks;
using System.Timers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using EGAZT.Models.TPProfile;
using Xamarin.Forms.Internals;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.TaxpayerProfileVM
{
    [Preserve(AllMembers = true)]
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

        private bool _IsLoading = false;
        public bool IsLoading
        {
            get
            {
                return _IsLoading;
            }
            set
            {
                if (_IsLoading == value) return;

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
                if (_BtnEnableFlag == value) return;

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
                if (_LblCountDownTimer == value) return;

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
                if (_OTPSentOnThisMobileNumber == value) return;

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
                if (_CurrentPasswordEntry == value) return;

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
                if (_NewPasswordEntry == value) return;

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
                if (_ConfirmPasswordEntry == value) return;

                _ConfirmPasswordEntry = value;

                BtnEnableFlag = false;
                if (value.Length > 0)
                    BtnEnableFlag = true;

                RaisePropertyChanged("ConfirmPasswordEntry");
            }
        }


        private Color _resendOTPTextColor =  (Color)Application.Current.Resources["ResendOTPTextColor"];
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
            if (countDownSeconds == 0)
            {
                otpTimer.Stop();
                ResendOTPTextColor =  (Color)Application.Current.Resources["Primary"];

            }
        }

        // * Call API
        public async Task<TaxPayerProfile> ChangePassword()
        {
            IsLoading = true;
            TaxPayerProfile TP = null;

            try
            {
                TPProfileAPIRequestDataModel APIRequestDataModel = new TPProfileAPIRequestDataModel();
                APIRequestDataModel.RequestType = "VERIFYOTPEMAIL";
                APIRequestDataModel.OTP = EnteredOTP;
                APIRequestDataModel.OldEmail = _updateEmailData.CurrentEmail;
                APIRequestDataModel.NewEmail = _updateEmailData.NewEmail;
                APIRequestDataModel.OldPassword = CurrentPasswordEntry;
                APIRequestDataModel.NewPassword = NewPasswordEntry;

                TPProfileAPIRequest TPProfileAPIRequestData = TPProfileAPIRequest.PrepareRequestData(APIRequestDataModel);
                TP = await WebServiceManager.POSTTPProfileAPICalls(TPProfileAPIRequestData, "VERIFYOTPEMAIL");

                IsLoading = false;
            }
            catch (Exception ex)
            {
                IsLoading = false;
                System.Diagnostics.Debug.WriteLine("Exception : ", ex.Message);
                ShowValidationPopup(ex.Message);
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }

            return TP;
        }

        public async Task<TaxPayerProfile> VarifyEmail()
        {
            IsLoading = true;
            TaxPayerProfile TP = null;

            try
            {
                // API Calls
                await Task.Run(async () =>
                {
                    TPProfileAPIRequestDataModel APIRequestDataModel = new TPProfileAPIRequestDataModel();
                    APIRequestDataModel.RequestType = "GETOTPEMAIL";
                    APIRequestDataModel.NewEmail = _updateEmailData.NewEmail;
                    APIRequestDataModel.OldEmail = _updateEmailData.CurrentEmail;

                    TPProfileAPIRequest TPProfileAPIRequestData = TPProfileAPIRequest.PrepareRequestData(APIRequestDataModel);
                    TP = await WebServiceManager.POSTTPProfileAPICalls(TPProfileAPIRequestData, "GETOTPEMAIL");

                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                IsLoading = false;
                System.Diagnostics.Debug.WriteLine("Exception : ", ex.Message);
                ShowValidationPopup(ex.Message);
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }

            return TP;
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
            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(sourceString));

            // PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }
    }

    // * Need Updated Email Data From Update Email Page
    public class UpdateEmailDataModel
    {
        public string CurrentEmail { get; set; }
        public string NewEmail { get; set; }
    }
}