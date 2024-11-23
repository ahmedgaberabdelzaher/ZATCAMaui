using System.Text.RegularExpressions;
using System.Timers;
using System.Windows.Input;

using ZATCAMAUI.Core.CustomControls;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Services.Interface;
using ZATCAMAUI.Views.NewDesign.MyReports;
using ZATCAMAUI.Views.NewDesign.ReportOTP;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.ReportOTPVM
{
    public class ReportOTPViewModel : BaseViewModel
    {
        #region Properties
        string oTPFirstDigit;
        public string OTPFirstDigit { get { return oTPFirstDigit; } set { oTPFirstDigit = value; OnPropertyChanged(); } }

        string oTPSecondDigit;
        public string OTPSecondDigit { get { return oTPSecondDigit; } set { oTPSecondDigit = value; OnPropertyChanged(); } }

        string oTPThirdDigit;
        public string OTPThirdDigit { get { return oTPThirdDigit; } set { oTPThirdDigit = value; OnPropertyChanged(); } }

        string oTPFourthDigit;
        public string OTPFourthDigit { get { return oTPFourthDigit; } set { oTPFourthDigit = value; OnPropertyChanged(); } }

        string phoneORRportNumber;
        public string PhoneORRportNumber { get { return phoneORRportNumber; } set { phoneORRportNumber = value; OnPropertyChanged(); } }

        string oTPSentOnThisMobileNumber;
        public string OTPSentOnThisMobileNumber { get { return oTPSentOnThisMobileNumber; } set { oTPSentOnThisMobileNumber = value; OnPropertyChanged(); } }

        string lblCountDownTimer;
        public string LblCountDownTimer { get { return lblCountDownTimer; } set { lblCountDownTimer = value; OnPropertyChanged(); } }

        bool isResendCodeEnabled;
        public bool IsResendCodeEnabled { get { return isResendCodeEnabled; } set { isResendCodeEnabled = value; OnPropertyChanged(); } }

        bool isPhoneSelected = true;
        public bool IsPhoneSelected { get { return isPhoneSelected; } set { isPhoneSelected = value; OnPropertyChanged(); } }

        double opacity = 1.0;
        public double Opacity { get { return opacity; } set { opacity = value; OnPropertyChanged(); } }

        private System.Timers.Timer otpTimer;
        private string EnteredOTP = string.Empty;
        private string Key = string.Empty;
        private IMyReportsServices _myReportsServices;
        Regex phoneRegex = new Regex(@"^05[0-9]{8}$");
        #endregion
        public ReportOTPViewModel(IMyReportsServices myReportsServices, INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            _myReportsServices = myReportsServices;
        }

        #region Commands
        public ICommand GoToNextEntryCommand
        {

            get
            {
                return new Command<object>((e) =>
                {
                    if (e != null)
                    {

                        var entry = e as GAZTBorderlessEntry;

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

        public ICommand FocusEntryCommand
        {

            get
            {
                return new Command<object>((e) =>
                {
                    if (e != null)
                    {

                        var entry = e as GAZTBorderlessEntry;

                        entry.Focus();
                    }
                });
            }
        }

        public ICommand BackFromOtpCommand
        {

            get
            {
                return new Command(() =>
                {
                    ClearData();
                });
            }
        }

        public ICommand VerifyOTPCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        IsLoading = true;
                        otpTimer.Stop();
                        EnteredOTP = OTPFirstDigit + OTPSecondDigit + OTPThirdDigit + OTPFourthDigit;
                        if (!string.IsNullOrWhiteSpace(EnteredOTP))
                        {
                            var result = await _myReportsServices.VerifyCode(PhoneORRportNumber, Key, EnteredOTP);

                            if (result == null)
                            {
                                IsShowMsgView = true;
                                MessageTxt = AppResources.RequestTimeoutDescription;
                            }
                            else if (result.header.status.code == "I000000")
                            {
                                var navigation = Application.Current.MainPage.Navigation;
                                var currentPage = navigation.NavigationStack.LastOrDefault();
                                navigation.InsertPageBefore(new MyReportsPage(PhoneORRportNumber), currentPage);
                                _navigationService.GoBack();
                                PhoneORRportNumber = string.Empty;
                            }
                            else
                            {
                                IsShowMsgView = true;
                                MessageTxt = AppResources.InvalidOTP;
                            }
                        }
                        else
                        {
                            IsShowMsgView = true;
                            MessageTxt = AppResources.PleaseenterOTP;
                        }
                        IsLoading = false;
                    }
                    catch (Exception)
                    {
                        IsLoading = false;
                        IsShowMsgView = true;
                        MessageTxt = AppResources.RequestTimeoutDescription;
                    }


                });
            }
        }

        public ICommand CardSelectionCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsPhoneSelected = IsPhoneSelected == true ? false : true;
                });
            }
        }

        public ICommand StartTimerCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (IsPhoneSelected)
                    {
                        ClearData();
                        OTPSentOnThisMobileNumber = AppResources.ZZMobileNumber + " xxxxxxx" + PhoneORRportNumber?.Substring(7, 3);
                        IsResendCodeEnabled = false;
                        Opacity = 0.5;
                        counter = 120;//To rest
                        StartOTPTimer();
                    }

                });
            }
        }

        public override ICommand BackCommand
        {
            get
            {
                return new Command(() =>
                {
                    var navigation = Application.Current.MainPage.Navigation;
                    var currentPage = navigation.NavigationStack.LastOrDefault();
                    if (navigation.NavigationStack.Count == 1)
                    {
                        _navigationService.NavigateTo("/Home", "0");
                        PhoneORRportNumber = string.Empty;
                        return;
                    }
                    else if (currentPage.GetType().Name == new InquiryAboutMyReportsPage().GetType().Name)
                    {
                        PhoneORRportNumber = string.Empty;
                    }
                    _navigationService.GoBack();
                });
            }
        }

        public ICommand ResendOtpCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        StartTimerCommand.Execute(null);
                        IsLoading = true;
                        var result = await _myReportsServices.SendOTP(PhoneORRportNumber);
                        if (result == null)
                        {
                            IsShowMsgView = true;
                            MessageTxt = AppResources.RequestTimeoutDescription;
                        }
                        else if (result.header.status.code == "I000000")
                        {
                            Key = result?.result?.key;
                        }
                        else
                        {
                            IsShowMsgView = true;
                            MessageTxt = AppResources.RequestTimeoutDescription;
                        }
                        IsLoading = false;
                    }
                    catch (Exception)
                    {
                        IsLoading = false;
                    }
                   
                });
            }
        }

        public ICommand GoToSubmitReportCommand
        {
            get
            {
                return new Command(() =>
                {
                    _navigationService.NavigateTo("SubmitReportPage");
                });
            }
        }

        public ICommand GoToPerviousReportsCommand
        {
            get
            {
                return new Command(() =>
                {
                    _navigationService.NavigateTo("InquiryAboutMyReportsPage");
                });
            }
        }

        public ICommand InquireReportCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        IsLoading = true;
                        if (IsPhoneSelected)
                        {
                            // Phone senario
                            if (phoneRegex.IsMatch(PhoneORRportNumber))
                            {
                                var result = await _myReportsServices.SendOTP(PhoneORRportNumber);
                                if (result == null)
                                {
                                    IsShowMsgView = true;
                                    MessageTxt = AppResources.RequestTimeoutDescription;
                                }
                                else if (result.header.status.code == "I000000")
                                {
                                    Key = result?.result?.key;
                                    await _navigationService.NavigateTo("ReportOTPPage");
                                }
                                else
                                {
                                    IsShowMsgView = true;
                                    MessageTxt = AppResources.RequestTimeoutDescription;
                                }
                            }

                            else
                            {
                                IsShowMsgView = true;
                                MessageTxt = AppResources.ZZMobilenumberhastostartwithnumber05;
                            }

                        }
                        else
                        {
                            // Report number senario
                            if (string.IsNullOrWhiteSpace(PhoneORRportNumber))
                            {
                                IsShowMsgView = true;
                                MessageTxt = AppResources.ReportNumberRequired;
                            }
                        }
                        IsLoading = false;
                    }
                    catch (Exception)
                    {
                        IsLoading = false;
                        IsShowMsgView = true;
                        MessageTxt = AppResources.RequestTimeoutDescription;
                    }



                });
            }
        }
        #endregion

        #region Methods
        private int counter = 120;

        private System.Timers.Timer timer;

        private string GetTime(int s)
        {
            TimeSpan time = TimeSpan.FromSeconds(s);
            return time.ToString(@"m\:ss");
        }

        private void StartOTPTimer()
        {
            // Timer            
            otpTimer = new System.Timers.Timer();
            LblCountDownTimer = GetTime(counter);
            otpTimer.Interval = 1000;

            // Event
            otpTimer.Elapsed += OnCountDownTimedOTPEvent;
            otpTimer.Start();

            otpTimer.Enabled = true;
        }

        private void OnCountDownTimedOTPEvent(object sender, ElapsedEventArgs e)
        {
            if (counter > 0)
            {
                counter--;
                LblCountDownTimer = GetTime(counter);
            }
            else
            {
                otpTimer.Elapsed -= OnCountDownTimedOTPEvent;
                Opacity = 1.0;
                IsResendCodeEnabled = true;
                timer.Stop();
                counter = 120;//To rest
            }
        }

        private void ClearData()
        {
            EnteredOTP = string.Empty;
            OTPFirstDigit = string.Empty;
            OTPSecondDigit = string.Empty;
            OTPThirdDigit = string.Empty;
            OTPFourthDigit = string.Empty;
        }
        #endregion
    }
}

