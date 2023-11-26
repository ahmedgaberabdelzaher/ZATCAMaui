using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Timers;
using System.Windows.Input;
using EGAZT.Models.SubmitReportModel;
using EGAZT.Services.Interface;
using EGAZT.Views.NewDesign.MyReports;
using EGAZT.Views.NewDesign.ReportOTP;
using EGAZT.Views.NewDesign.SubmitReport;
using GalaSoft.MvvmLight.Views;
using GAZT;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Xamarin.Essentials.Permissions;

namespace EGAZT.ViewModel.NewDesignViewModel.ReportOTPVM
{
    public class ReportOTPViewModel :BaseViewModel
    {
        #region Properties
        string oTPFirstDigit;
        public string OTPFirstDigit { get { return oTPFirstDigit; } set { oTPFirstDigit = value; RaisePropertyChanged(); } }

        string oTPSecondDigit;
        public string OTPSecondDigit { get { return oTPSecondDigit; } set { oTPSecondDigit = value; RaisePropertyChanged(); } }

        string oTPThirdDigit;
        public string OTPThirdDigit { get { return oTPThirdDigit; } set { oTPThirdDigit = value; RaisePropertyChanged(); } }

        string oTPFourthDigit;
        public string OTPFourthDigit { get { return oTPFourthDigit; } set { oTPFourthDigit = value; RaisePropertyChanged(); } }

        string phoneORRportNumber;
        public string PhoneORRportNumber { get { return phoneORRportNumber; } set { phoneORRportNumber = value; RaisePropertyChanged(); } }

        string oTPSentOnThisMobileNumber;
        public string OTPSentOnThisMobileNumber { get { return oTPSentOnThisMobileNumber; } set { oTPSentOnThisMobileNumber = value; RaisePropertyChanged(); } }

        string lblCountDownTimer;
        public string LblCountDownTimer { get { return lblCountDownTimer; } set { lblCountDownTimer = value; RaisePropertyChanged(); } }

        bool isResendCodeEnabled;
        public bool IsResendCodeEnabled { get { return isResendCodeEnabled; } set { isResendCodeEnabled = value; RaisePropertyChanged(); } }

        bool isPhoneSelected = true;
        public bool IsPhoneSelected { get { return isPhoneSelected; } set { isPhoneSelected = value; RaisePropertyChanged(); } }

        double opacity =1.0;
        public double Opacity { get { return opacity; } set { opacity = value; RaisePropertyChanged(); } }

        private System.Timers.Timer otpTimer;
        private int countDownSeconds;
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

        public ICommand FocusEntryCommand
        {

            get
            {
                return new Command<object>((e) =>
                {
                    if (e != null)
                    {

                        var entry = e as BorderlessEntry;

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
                            else if(result.Status)
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
                    if(IsPhoneSelected)
                    {
                        ClearData();
                        OTPSentOnThisMobileNumber = AppResources.ZZMobileNumber + " xxxxxxx" + PhoneORRportNumber?.Substring(7, 3);
                        IsResendCodeEnabled = false;
                        Opacity = 0.5;
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
                return new Command(() =>
                {
                    StartTimerCommand.Execute(null);
                    InquireReportCommand.Execute(null);
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
                return new Command(async() =>
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
                                else if (result.Success)
                                {
                                    Key = result?.Result?.Data?.data?.key;
                                    _navigationService.NavigateTo("ReportOTPPage");
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
                            if (!string.IsNullOrWhiteSpace(PhoneORRportNumber))
                            {
                                
                            }

                            else
                            {
                                IsShowMsgView = true;
                                MessageTxt = AppResources.ReportNumberRequired;
                            }
                        }
                        IsLoading = false;
                    }
                    catch (Exception )
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
        private void StartOTPTimer()
        {
            // Timer            
            otpTimer = new System.Timers.Timer();
            otpTimer.Interval = 1000;

            // Event
            otpTimer.Elapsed += OnCountDownTimedOTPEvent;

            countDownSeconds = 60;

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
                Opacity = 1.0;
                IsResendCodeEnabled = true;
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

