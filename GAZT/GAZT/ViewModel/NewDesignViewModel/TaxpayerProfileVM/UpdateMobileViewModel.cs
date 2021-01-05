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

namespace EGAZT.ViewModel.NewDesignViewModel.TaxpayerProfileVM
{
    [Preserve(AllMembers = true)]
    public class UpdateMobileViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        public Timer otpTimer;
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
                if (_IsLoading == value) return;

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
                if (_maxDigids == value) return;

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
                if (_mobileCountryCode == value) return;

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
                if (_CurrentMobileNumberEntryText == value) return;

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
                if (_NewMobileNumberEntryText == value) return;

                _NewMobileNumberEntryText = value;

                /*BtnEnableFlag = false;
                if (value.Length > 0)
                    BtnEnableFlag = true;*/

                if(!string.IsNullOrEmpty(NewMobileNumberEntryText) && !string.IsNullOrEmpty(CountryCode))
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
                                if((ch >= 48 && ch <= 57))
                                {
                                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NDMobileNumberMustStartWithFive));
                                }
                            }
                        }
                        else
                        {
                            if(NewMobileNumberEntryText.Length > 0 && CountryCode.Length > 0)
                            {
                                string firstlettorOfNewMobileNumberEntryText = NewMobileNumberEntryText.Substring(0, 1);
                                if (firstlettorOfNewMobileNumberEntryText.Equals("0"))
                                {
                                    NewMobileNumberEntryText = string.Empty;
                                }
                            }
                        }
                    }
                    catch (Exception ex) {
                        Console.Write(ex.ToString());
                        Console.Write(ex.StackTrace.ToString());
                    }
                }
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

        private bool _BtnEnableFlag = true;
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
                System.Diagnostics.Debug.WriteLine("VERIFY OTP ERROR : {0}", ex.Message);
                ShowValidationPopup(ex.Message);
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
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

            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(sourceString));

           // PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
        }
    }
}