using System.Windows.Input;
using Mopups.Services;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.NewModelAPI.AbsherOTP;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel
{
    public class OtpPopUpPageViewModel : BaseViewModel
	{
        public int CurrentAttempt = 0;
        public int numberOfSeconds = 120;
        public int TotalSec;
        public bool StopTimer = false;
        public ICommand OnResendButtonClick { get; set; }
        public ICommand OnContinueButtonClick { get; set; }



        private string _oTPValidDuration = "";
        public void TimerStart(int Seconds)
        {
            // IsVerifyOTPEnabled = true;
            CancellationTokenSource _CancellationTokenSource = new CancellationTokenSource();
            TotalSec = Seconds;
            CancellationTokenSource CTS = _CancellationTokenSource;
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                if (App.IsComingFromSleepMode)
                {
                    if (DeviceInfo.Platform == DevicePlatform.iOS)
                    {
                        TotalSec = TotalSec - Convert.ToInt32(App.TimeDifference);
                        App.IsComingFromSleepMode = false;
                        // StopTimer = true;
                    }
                    else
                    {


                        TotalSec = TotalSec - Convert.ToInt32(App.TimeDifference) - (120 - App.CurrentTimeDifference);
                        App.IsComingFromSleepMode = false;
                        StopTimer = true;

                        if (TotalSec > 0)
                        {
                            IsResendOTPEnabled = false;
                            //TimerStart(TotalSec);
                            ButtonDisableColor = Colors.Gray;
                        }

                    }
                }
                if (CTS.IsCancellationRequested)
                {
                    return false;
                }
                else
                {
                    if (TotalSec == 0)
                    {
                        return false;
                    }
                    //else if (!StopTimer)
                    //{
                    //    return false;
                    //}
                    //else
                    //{{StaticResource Primary} green 
                    //}#d49504 golden
                    if (TotalSec < 0)
                    {
                        OTPValidDuration = " 0:00";
                        ButtonDisableColor = (Color)Application.Current.Resources["Secondary"];
                        ButtonDisableTextColor = Colors.White;
                        IsResendOTPEnabled = true;
                        VerifyButtonDisableColor = (Color)Application.Current.Resources["ButtonGray"];
                        VerifyButtonDisableTextColor = Colors.Gray;
                        IsVerifyOTPEnabled = false;
                        IsOTPEntryEnable = false;
                        return false;
                    }
                    //else if(TotalSec <0)
                    //{
                    //    TotalSec = 120;
                    //}
                    TotalSec = TotalSec - 1;
                    App.CurrentTimeDifference = TotalSec;
                    numberOfSeconds = TotalSec;
                    TimeSpan _TimeSpan = TimeSpan.FromSeconds(TotalSec);
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        OTPValidDuration = " " + string.Format("{0:00}:{1:00}", _TimeSpan.Minutes, _TimeSpan.Seconds);
                    });
                    return true;
                }
            });
        }

        public string OTPValidDuration
        {
            get
            {
                return _oTPValidDuration;
            }
            set
            {
                if (_oTPValidDuration == value) return;

                _oTPValidDuration = value;
                if (_oTPValidDuration.Equals(" 00:00"))
                {
                    ButtonDisableColor = (Color)Application.Current.Resources["Secondary"];
                    ButtonDisableTextColor = Colors.White;
                    IsResendOTPEnabled = true;
                    VerifyButtonDisableColor = (Color)Application.Current.Resources["ButtonGray"];
                    VerifyButtonDisableTextColor = Colors.Gray;
                    IsVerifyOTPEnabled = false;
                    IsOTPEntryEnable = false;
                }
                OnPropertyChanged("OTPValidDuration");
            }
        }
        private Color _verifybuttonDisableColor = (Color)Application.Current.Resources["Secondary"];
        public Color VerifyButtonDisableColor
        {
            get
            {
                return _verifybuttonDisableColor;
            }
            set
            {
                if (_verifybuttonDisableColor == value) return;

                _verifybuttonDisableColor = value;
                OnPropertyChanged("VerifyButtonDisableColor");
            }
        }
        private Color _buttonDisableColor = (Color)Application.Current.Resources["ButtonGray"];
        public Color ButtonDisableColor
        {
            get
            {
                return _buttonDisableColor;
            }
            set
            {
                if (_buttonDisableColor == value) return;

                _buttonDisableColor = value;
                OnPropertyChanged("ButtonDisableColor");
            }
        }
        private bool _isVerifyOTPEnabled = true;
        public bool IsVerifyOTPEnabled
        {
            get
            {
                return _isVerifyOTPEnabled;
            }
            set
            {
                if (_isVerifyOTPEnabled == value) return;

                _isVerifyOTPEnabled = value;
                OnPropertyChanged("IsVerifyOTPEnabled");
            }
        }
        private Color _buttonDisableTextColor = Colors.Gray;
        public Color ButtonDisableTextColor
        {
            get
            {
                return _buttonDisableTextColor;
            }
            set
            {
                if (_buttonDisableTextColor == value) return;

                _buttonDisableTextColor = value;
                OnPropertyChanged("ButtonDisableTextColor");
            }
        }
        private Color _verifybuttonDisableTextColor = Colors.White;
        public Color VerifyButtonDisableTextColor
        {
            get
            {
                return _verifybuttonDisableTextColor;
            }
            set
            {
                if (_verifybuttonDisableTextColor == value) return;

                _verifybuttonDisableTextColor = value;
                OnPropertyChanged("VerifyButtonDisableTextColor");
            }
        }

        private bool _isOTPEntryEnable = true;
        public bool IsOTPEntryEnable
        {
            get
            {
                return _isOTPEntryEnable;
            }
            set
            {
                if (_isOTPEntryEnable == value) return;

                _isOTPEntryEnable = value;
                OnPropertyChanged("IsOTPEntryEnable");
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
                OnPropertyChanged("IsResendOTPEnabled");
            }
        }

        private string _oTP = string.Empty;
        public string OTP
        {
            get
            {
                return _oTP;
            }
            set
            {
                if (_oTP == value) return;

                _oTP = value;
                OnPropertyChanged("OTP");
            }
        }

        private bool _isOTPEncripted = true;
        public bool IsOTPEncripted
        {
            get
            {
                return _isOTPEncripted;
            }
            set
            {
                if (_isOTPEncripted == value) return;

                _isOTPEncripted = value;
                OnPropertyChanged("IsOTPEncripted");
            }
        }

        private getGuiD gUID2 { get;  set; }

        private string _otptxtName = string.Empty;
        public string OtpTxtName
        {
            get
            {
                return _otptxtName;
            }
            set
            {
                if (_otptxtName == value) return;

                _otptxtName = value;
                OnPropertyChanged("OtpTxtName");
            }
        }
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
                OnPropertyChanged("IsLoading");
            }
        }
        private string _continueButtonText = string.Empty;
        public string ContinueButtonText
        {
            get
            {
                return _continueButtonText;
            }
            set
            {
                if (_continueButtonText == value) return;

                _continueButtonText = value;
                OnPropertyChanged("ContinueButtonText");
            }
        }

        public OtpPopUpPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
		{
            ContinueButtonText = AppResources.ZZZZContinue;

            OnContinueButtonClick = new Command(async () =>
            {
                if (IsVerifyOTPEnabled)
                {
                    ContinueButtonText = AppResources.ZZZZContinue;
                    //IsLoading = true;
                }
                if (!string.IsNullOrEmpty(OTP))
                {
                    await StepfivedataValidation();
                }
                else
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.OTPvaluenotempty));
                }

            });
            OnResendButtonClick = new Command(async () =>
            {
                // ContinueButtonText = AppResources.ZZZZContinue;

                if (IsResendOTPEnabled)
                {
                    await Task.Run(() =>
                    {
                        IsLoading = true;
                    });
                    await SetRequestObjectResendOtp();
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });
                }


            });
        }

        public AbsherOTPResponse otpRecvided { get; set; }

        public async Task SetRequestObjectResendOtp()
        {
            try
            {
                OTPModelD otp = new OTPModelD();
                OtpPageResult d = new OtpPageResult();

                d.Captcha = otpRecvided.result.captcha;
                d.Guid16 = otpRecvided.result.formBundleGUID;
                d.Idnum = otpRecvided.result.idNumber;
                d.Idtype = gUID2.idtype;
                d.TaxpDob = gUID2.TpDOb;

                otp.d = d;

                await Task.Run(async () =>
                {
                    otpRecvided = new AbsherOTPResponse();
                    otpRecvided = await WebServiceManager.getValidateAbsher(otp, true);
                });

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    IsVerifyOTPEnabled = true;
                 //   await MopupService.Instance.PushAsync(new AttachmentInformationPopUp("OTP Enabled"));

                });
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }
        }

        private bool _frameOTPError = false;
        public bool FrameOTPError
        {
            get
            {
                return _frameOTPError;
            }
            set
            {
                if (_frameOTPError == value) return;

                _frameOTPError = value;
                OnPropertyChanged("FrameOTPError");
            }
        } 
        public void OtpMaxCall(AbsherOTPResponse otpResponse)
        {
            try
            {
                Task.Run( () =>
                {
                    otpRecvided = otpResponse;
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }  
        }
        public async Task StepfivedataValidation()
        {
            try
            {
                OTPModelvalidateD otp = new OTPModelvalidateD();
                otpVlidate d = new otpVlidate();
                this.IsLoading = true;
                if (otpRecvided != null)
                {
                    d.Captcha = otpRecvided.result.captcha;
                    d.Guid16 = otpRecvided.result.formBundleGUID;
                    d.Idnum = otpRecvided.result.idNumber;
                    d.OtpCode = OTP;
                    otp.d = d;
                }

                await Task.Run(async () =>
                {
                    ValidateAbhserOTPModel otpRecvided2 = await WebServiceManager.ValidateAbsher(otp, false);
                    if (otpRecvided2 != null)
                    {
                        MessagingCenter.Send<Object, object>(this, "Otpvalidated", otpRecvided2);
                        await MopupService.Instance.PopAsync();
                    }
                    else
                    {
                        MessagingCenter.Send<Object, object>(this, "Otpvalidated", "error");
                        await MopupService.Instance.PopAsync();
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
                string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
            }
           
        }
    }
}

