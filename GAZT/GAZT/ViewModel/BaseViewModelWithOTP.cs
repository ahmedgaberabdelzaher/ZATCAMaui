using System;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using EGAZT.Helper;
using EGAZT.Services.Interface;
using EGAZT.ViewModel.NewDesignViewModel;
using GalaSoft.MvvmLight.Views;
using GAZT;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EGAZT.ViewModel
{
    public class BaseViewModelWithOTP:BaseViewModel
    {
        ICommonServices _commonServices;
        public BaseViewModelWithOTP(INavigationService navigationService, IDialogService dialogService, ICommonServices commonServices) : base(navigationService, dialogService)
        {
            _commonServices = commonServices;
        }


        private bool isResendCodeEnabled;
        public bool IsResendCodeEnabled
        {
            get
            {
                return isResendCodeEnabled;
            }
            set
            {
                if (isResendCodeEnabled == value) return;

                isResendCodeEnabled = value;
                RaisePropertyChanged();
            }
        }


        string oTPFirstDigit;
        public string OTPFirstDigit { get { return oTPFirstDigit; } set { oTPFirstDigit = value; RaisePropertyChanged(); } }

        string oTPSecondDigit;
        public string OTPSecondDigit { get { return oTPSecondDigit; } set { oTPSecondDigit = value; RaisePropertyChanged(); } }

        string oTPThirdDigit;
        public string OTPThirdDigit { get { return oTPThirdDigit; } set { oTPThirdDigit = value; RaisePropertyChanged(); } }

        string oTPFourthDigit;
        public string OTPFourthDigit { get { return oTPFourthDigit; } set { oTPFourthDigit = value; RaisePropertyChanged(); } }

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
                RaisePropertyChanged();
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
                RaisePropertyChanged();
            }
        }

        public System.Timers.Timer otpTimer;
        public int countDownSeconds;
        public string EnteredOTP = string.Empty;
        bool IsOtpValid;
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
            if (countDownSeconds == 0)
            {
                otpTimer.Elapsed -= OnCountDownTimedOTPEvent;
                otpTimer.Stop();
                ResendOTPTextColor = (Color)Application.Current.Resources["Primary"];
                IsOtpValid = false;
                IsResendCodeEnabled = true;
            }
        }


        public ICommand VerifyOTPCommand
        {
            get
            {
                return new Command(async () =>
                {
                    VerifyOtp();

                });
            }
        }

        protected bool VerifyOtp()
        {
            try
            {
                IsLoading = true;
                if (IsOtpValid)
                {

                 
                    EnteredOTP = OTPFirstDigit + OTPSecondDigit + OTPThirdDigit + OTPFourthDigit;
                    if (EnteredOTP == Preferences.Get("OTPValue", ""))
                    {
                        otpTimer.Stop();
                        ClearOTPData();
                        return true;
                    }
                    else
                    {
                        IsShowMsgView = true;
                        MessageTxt = AppResources.InvalidOTP;
                        return false;
                    }
                }
                else
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.InvalidOTP;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally { IsLoading = false; }
        }

        public ICommand GoToNextEntryCommand
        {

            get
            {
                return new Command<object>((e) =>
                {
                    if (e != null)
                    {

                        var entry = e as BorderlessEntry;

                        switch (entry.ClassId)
                        {
                            case "2":
                                if (!string.IsNullOrEmpty(OTPFirstDigit))
                                {
                                    entry.Focus();
                                }
                                break;
                            case "3":
                                if (!string.IsNullOrEmpty(OTPSecondDigit))
                                {
                                    entry.Focus();
                                }
                                break;
                            case "4":
                                if (!string.IsNullOrEmpty(OTPThirdDigit))
                                {
                                    entry.Focus();
                                }
                                break;
                            default:
                                break;
                        }


                    }
                });
            }
        }
        bool isOTPView { get; set; }

        public bool IsOTPView
        {
            get { return isOTPView; }

            set
            {
                isOTPView = value;
                RaisePropertyChanged();
            }
        }

        public ICommand BackFromOtpCommand
        {

            get
            {
                return new Command(() =>
                {
                    IsOTPView = IsOtpValid = false;
                    ClearOTPData();
                });
            }
        }

        public string Phone { get; set; }

        public ICommand ResendOtpCommand
        {

            get
            {
                return new Command(async () =>
                {

                    await SendOtpSMS(Phone);
                });
            }
        }

        public async Task SendOtpSMS(string PhoneNo)
        {
            try
            {
                //PhoneNo = "0551844232";
                Phone = PhoneNo;
                IsLoading = true;
                string otp = OTPHelper.Generate();
                Preferences.Set("OTPValue", otp);
                Preferences.Set("MobileNo", PhoneNo);
                var data = await _commonServices.SendOtpSms(PhoneNo, $"{AppResources.OTPMsgBody}{otp}");
                IsOtpValid = true;
                OTPSentOnThisMobileNumber = AppResources.MobileNumber + " xxxxxxx" + Phone.Substring(7, 3);
                ResendOTPTextColor = (Color)Application.Current.Resources["ResendOTPTextColor"];
                IsResendCodeEnabled = false;
                StartOTPTimer();
                if (data.Item2)
                {

                }
            }
            catch (Exception ex)
            {

            }
            finally { IsLoading = false; }
        }

        public void ClearOTPData()
        {
            Preferences.Remove("OTPValue");
            Preferences.Remove("MobileNo");
            OTPSentOnThisMobileNumber = OTPFirstDigit = OTPSecondDigit = OTPThirdDigit = OTPFourthDigit = EnteredOTP = "";
            IsResendCodeEnabled = IsOtpValid = false;
            ResendOTPTextColor = (Color)Application.Current.Resources["ResendOTPTextColor"];

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
                RaisePropertyChanged();
            }
        }

    }
}
