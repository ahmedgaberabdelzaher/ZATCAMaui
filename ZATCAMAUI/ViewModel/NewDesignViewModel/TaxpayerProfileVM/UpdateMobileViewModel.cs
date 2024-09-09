using System.Timers;
using Mopups.Services;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.TPProfile;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.TaxpayerProfileVM
{

    public class UpdateMobileViewModel : BaseViewModel
    {
        #region Variable

        public System.Timers.Timer otpTimer;
        public int countDownSeconds;
        public string EnteredOTP = string.Empty;

        #endregion

        #region Properties
        //engText
        private bool _Arabictext { get; set; }
        public bool Arabictext
        {
            get => _Arabictext;
            set
            {
                _Arabictext = value;
                OnPropertyChanged("Arabictext");

            }
        }

        private bool _engText { get; set; }
        public bool engText
        {
            get => _engText;
            set
            {
                _engText = value;
                OnPropertyChanged("engText");

            }
        }

        private bool _isOTPEntryEnable { get; set; }
        public bool IsOTPEntryEnable
        {
            get => _isOTPEntryEnable;
            set
            {
                _isOTPEntryEnable = value;
                OnPropertyChanged("IsOTPEntryEnable");
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
                if (_maxDigids == value) return;

                _maxDigids = value;
                OnPropertyChanged("MaxDigids");
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
                if (_CountryCode == value) return;

                _CountryCode = value;
                if (_CountryCode != null)
                {
                    MaxDigids = (14 - _CountryCode.Length).ToString();
                }
                else
                {
                    MaxDigids = "15";
                }

                OnPropertyChanged("CountryCode");
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
                if (_mobileCountryCode == value) return;

                _mobileCountryCode = value;
                OnPropertyChanged("MobileCountryCode");
            }
        }

        private string _CurrentMobileNumberEntryText;
        public string CurrentMobileNumberEntryText
        {
            get { return _CurrentMobileNumberEntryText; }
            set
            {
                if (_CurrentMobileNumberEntryText == value) return;

                _CurrentMobileNumberEntryText = value;
                OnPropertyChanged("CurrentMobileNumberEntryText");
            }
        }

        private string _NewMobileNumberEntryText;
        public string NewMobileNumberEntryText
        {
            get { return _NewMobileNumberEntryText; }
            set
            {
                if (_NewMobileNumberEntryText == value) return;

                _NewMobileNumberEntryText = value;

                /*BtnEnableFlag = false;
                if (value.Length > 0)
                    BtnEnableFlag = true;*/

                if (!string.IsNullOrEmpty(NewMobileNumberEntryText) && !string.IsNullOrEmpty(CountryCode))
                {
                    try
                    {
                        if (NewMobileNumberEntryText.Length > 0 && CountryCode.Equals("+966"))
                        {
                            string firstlettorOfNewMobileNumberEntryText = NewMobileNumberEntryText.Substring(0, 1);
                            char ch = NewMobileNumberEntryText.ToCharArray()[0];
                            if (!firstlettorOfNewMobileNumberEntryText.Equals("5"))
                            {
                                NewMobileNumberEntryText = string.Empty;
                                if (ch >= 48 && ch <= 57)
                                {
                                    MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NDMobileNumberMustStartWithFive));
                                }
                            }
                        }
                        else
                        {
                            if (NewMobileNumberEntryText.Length > 0 && CountryCode.Length > 0)
                            {
                                string firstlettorOfNewMobileNumberEntryText = NewMobileNumberEntryText.Substring(0, 1);
                                if (firstlettorOfNewMobileNumberEntryText.Equals("0"))
                                {
                                    NewMobileNumberEntryText = string.Empty;
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                OnPropertyChanged("NewMobileNumberEntryText");
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
                OnPropertyChanged("LblCountDownTimer");
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
                OnPropertyChanged("OTPSentOnThisMobileNumber");
            }
        }

        private string _OTPSentOnThisMobileNumber2;
        public string OTPSentOnThisMobileNumber2
        {
            get
            {
                return _OTPSentOnThisMobileNumber2;
            }
            set
            {
                if (_OTPSentOnThisMobileNumber2 == value) return;

                _OTPSentOnThisMobileNumber2 = value;
                OnPropertyChanged("OTPSentOnThisMobileNumber2");
            }
        }

        private bool _BtnEnableFlag = true;
        public bool BtnEnableFlag
        {
            get { return _BtnEnableFlag; }
            set
            {
                if (_BtnEnableFlag == value) return;

                _BtnEnableFlag = value;
                OnPropertyChanged("BtnEnableFlag");
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
                OnPropertyChanged("ResendOTPTextColor");
            }
        }

        #endregion

        public UpdateMobileViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
        }

        
        // * Private methods
        private bool CheckOnlyNumber(char letter)
        {
            if (letter >= 48 && letter <= 57)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        DateTime tempCheckerOne, tempCheckerTwo;


        public void StartOTPTimer()
        {
            // Timer
            otpTimer = new System.Timers.Timer();
            otpTimer.Interval = 1000;

            // Event
            otpTimer.Elapsed += OnCountDownTimedOTPEvent;

            tempCheckerOne = DateTime.Now;
            //countDownSeconds = 120;

            otpTimer.Enabled = true;
            IsOTPEntryEnable = true;
            BtnEnableFlag = true;
        }

        private void OnCountDownTimedOTPEvent(object sender, ElapsedEventArgs e)
        {
            tempCheckerTwo = DateTime.Now;
            countDownSeconds = 120 - Convert.ToInt32(tempCheckerTwo.Subtract(tempCheckerOne).TotalSeconds);

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
                IsOTPEntryEnable = false;
                BtnEnableFlag = false;
                ResendOTPTextColor = (Color)Application.Current.Resources["Primary"];
            }
        }
        public async Task<TaxPayerProfile> VarifyMobileNumber()
        {
            IsLoading = true;
            TaxPayerProfile TP = null;

            try
            {
                await Task.Run(async () =>
                {
                    string newMobileNumber = NewMobileNumberFormate(NewMobileNumberEntryText);
                    string MobileCountry = string.Empty;

                    if (MobileCountryCode == string.Empty)
                        MobileCountry = "SA";
                    else
                        MobileCountry = MobileCountryCode;

                    TPProfileAPIRequestDataModel APIRequestDataModel = new TPProfileAPIRequestDataModel();
                    APIRequestDataModel.RequestType = "GETOTPMOBILE";
                    APIRequestDataModel.NewMobile = newMobileNumber;
                    APIRequestDataModel.CountryCode = MobileCountry;

                    TPProfileAPIRequest TPProfileAPIRequestData = TPProfileAPIRequest.PrepareRequestData(APIRequestDataModel);
                    TP = await WebServiceManager.POSTTPProfileAPICalls(TPProfileAPIRequestData, "GETOTPMOBILE");
                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                IsLoading = false;
                System.Diagnostics.Debug.WriteLine("VERIFY OTP ERROR : {0}", ex.Message);
                ShowValidationPopup(ex.Message);
            }

            return TP;
        }

        public async Task<TaxPayerProfile> VarifyOTPToUpdateMobileNumber()
        {
            IsLoading = true;
            TaxPayerProfile TP = null;

            try
            {
                await Task.Run(async () =>
                {
                    string MobileCountry = string.Empty;
                    if (MobileCountryCode == string.Empty)
                        MobileCountry = "SA";
                    else
                        MobileCountry = MobileCountryCode;

                    string newMobileNumber = NewMobileNumberFormate(NewMobileNumberEntryText);
                    TPProfileAPIRequestDataModel APIRequestDataModel = new TPProfileAPIRequestDataModel();
                    APIRequestDataModel.RequestType = "VERIFYOTPMOBILE";
                    APIRequestDataModel.OTP = EnteredOTP;
                    APIRequestDataModel.NewMobile = newMobileNumber;
                    APIRequestDataModel.CountryCode = MobileCountry;

                    TPProfileAPIRequest TPProfileAPIRequestData = TPProfileAPIRequest.PrepareRequestData(APIRequestDataModel);
                    TP = await WebServiceManager.POSTTPProfileAPICalls(TPProfileAPIRequestData, "VERIFYOTPMOBILE");

                    IsLoading = false;
                });
            }
            catch (Exception ex)
            {
                IsLoading = false;
                ShowValidationPopup(ex.Message);
            }

            return TP;
        }

        private string NewMobileNumberFormate(string mobileNumber)
        {
            string formatedCountryCode = CountryCode.Replace("+", "00");
            return formatedCountryCode + NewMobileNumberEntryText;
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

            MopupService.Instance.PushAsync(new AttachmentInformationPopUp(sourceString));

            // MopupService.Instance.PushAsync(new AddPopPageView(popUp));
        }
    }
}