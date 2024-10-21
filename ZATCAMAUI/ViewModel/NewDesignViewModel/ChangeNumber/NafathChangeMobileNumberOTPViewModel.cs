using System;
using System.Windows.Input;
using Mopups.Services;
using ZATCAMAUI.Core.CustomControls;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.ChangeNumber
{
    public class NafathChangeMobileNumberOTPViewModel : BaseViewModel
    {
        public ICommand VerifyOTPCommand { get; set; }
        public ICommand ResendCommand { get; set; }
        public ICommand GoToNextEntryCommandMobile
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
                                if (!string.IsNullOrEmpty(FirstDigit))
                                {
                                    entry.Focus();
                                }
                                break;
                            case "3":
                                if (!string.IsNullOrEmpty(SecondDigit))
                                {
                                    entry.Focus();
                                }
                                break;
                            case "4":
                                if (!string.IsNullOrEmpty(ThirdDigit))
                                {
                                    entry.Focus();
                                }
                                break;
                            case "5":
                                if (!string.IsNullOrEmpty(FourthDigit))
                                {
                                    entry.Focus();
                                }
                                break;
                            case "6":
                                if (!string.IsNullOrEmpty(FifthDigit))
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


        public NafathChangeMobileNumberSendOTPResponse Request { get; set; }

        private string _firstDigit;
        private string _secondDigit;
        private string _thirdDigit;
        private string _fourthDigit;
        private string _fifthDigit;
        private string _sixthDigit;

        public string FirstDigit
        {
            get
            {
                return _firstDigit;
            }
            set
            {
                _firstDigit = value;
                OnPropertyChanged(nameof(FirstDigit));
            }
        }
        public string SecondDigit
        {
            get
            {
                return _secondDigit;
            }
            set
            {
                _secondDigit = value;
                OnPropertyChanged(nameof(SecondDigit));
            }
        }
        public string ThirdDigit
        {
            get
            {
                return _thirdDigit;
            }
            set
            {
                _thirdDigit = value;
                OnPropertyChanged(nameof(ThirdDigit));
            }
        }
        public string FourthDigit
        {
            get
            {
                return _fourthDigit;
            }
            set
            {
                _fourthDigit = value;
                OnPropertyChanged(nameof(FourthDigit));
            }
        }
        public string FifthDigit
        {
            get
            {
                return _fifthDigit;
            }
            set
            {
                _fifthDigit = value;
                OnPropertyChanged(nameof(FifthDigit));
            }
        }
        public string SixthDigit
        {
            get
            {
                return _sixthDigit;
            }
            set
            {
                _sixthDigit = value;
                OnPropertyChanged(nameof(SixthDigit));
            }
        }

        private string time;
        public string Time
        {
            get { return time; }
            set
            {
                time = value;
                OnPropertyChanged(nameof(Time));
            }
        }

        private int counter = 120;

        private System.Timers.Timer timer;

        private bool isError;
        public bool IsError
        {
            get { return isError; }
            set
            {
                isError = value;
                OnPropertyChanged(nameof(IsError));
            }
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        private bool isResendEnabled = false;
        public bool IsResendEnabled
        {
            get { return isResendEnabled; }
            set
            {
                isResendEnabled = value;
                OnPropertyChanged(nameof(IsResendEnabled));
            }
        }

        private Color _resendOtpColor = (Color)Application.Current.Resources["ResendOTPTextColor"];

        public Color ResendOtpColor
        {
            get { return _resendOtpColor; }
            set
            {
                _resendOtpColor = value;
                OnPropertyChanged(nameof(ResendOtpColor));
            }
        }

        public NafathChangeMobileNumberOTPViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            BindCommands();
            Reset();
        }

        private void BindCommands()
        {
            VerifyOTPCommand = new Command(async () => await VerifyOTP());
            ResendCommand = new Command(() => Resend());

        }

        private async Task VerifyOTP()
        {
            try
            {
                if (!Validate())
                {
                    NafathChangeMobileNumberCheckOTPModel model = new NafathChangeMobileNumberCheckOTPModel();
                    model.Guid = Request.d.Guid;
                    model.Scrid = Device.RuntimePlatform == Device.iOS ? "C3" : "C4";
                    model.ErrorMsg = string.Empty;
                    model.Lang = WebServiceManager.GetLangZParameterAREN();
                    model.Otp = FirstDigit + SecondDigit + ThirdDigit + FourthDigit + FifthDigit + SixthDigit;
                    var response = await WebServiceManager.NafathChangeMobileNumberCheckOTP(model);
                    if (response != null && response.d != null)
                    {
                        _navigationService.NavigateTo(App.NafathChangeMobileNumberSuccessView);
                    }
                    else
                    {
                        await _dialogService.ShowMessage(AppResources.Somethingwentwrong, AppResources.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
            }

        }

        private bool Validate()
        {
            IsError = false;
            ErrorMessage = string.Empty;
            if (string.IsNullOrEmpty(FirstDigit) || string.IsNullOrEmpty(SecondDigit) || string.IsNullOrEmpty(ThirdDigit) || string.IsNullOrEmpty(FourthDigit))
            {
                IsError = true;
                ErrorMessage = AppResources.ZZPleaseenteraccessCode;
            }
            return IsError;
        }

        private async Task Resend()
        {
            counter = 120;
            await ResendOTP();

        }

        public void Reset()
        {
            FirstDigit = string.Empty;
            SecondDigit = string.Empty;
            ThirdDigit = string.Empty;
            FourthDigit = string.Empty;
            FifthDigit = string.Empty;
            SixthDigit = string.Empty;
            IsError = false;
            ErrorMessage = string.Empty;
            counter = 120;
        }

        public void StartTimer()
        {
            timer = new System.Timers.Timer();
            Time = GetTime(counter);
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (counter > 0)
            {
                counter--;
                Time = GetTime(counter);
            }
            else
            {
                timer.Stop();
                IsResendEnabled = true;
                ResendOtpColor = (Color)Application.Current.Resources["Primary"];
            }
        }

        public void StopTimer()
        {
            if (timer != null)
            {
                timer.Stop();
            }
        }

        private string GetTime(int s)
        {
            TimeSpan time = TimeSpan.FromSeconds(s);
            return time.ToString(@"m\:ss");
        }

        private async Task ResendOTP()
        {
            try
            {
                NafathChangeMobileNumberSendOTPModel model = new NafathChangeMobileNumberSendOTPModel();
                model.Partner = Request.d.Partner;
                model.Guid = Request.d.Guid;
                model.MobExten = Request.d.MobExten;
                model.MobNum = Request.d.MobNum;
                model.ErrorMsg = string.Empty;
                model.SendResendOtp = "2";
                model.Lang = WebServiceManager.GetLangZParameterAREN();
                model.Scrid = Device.RuntimePlatform == Device.iOS ? "C3" : "C4";
                var response = await WebServiceManager.NafathChangeMobileNumberSendOTP(model);
                if (response != null && response.d != null)
                {
                    Request.d.Guid = response.d.Guid;
                    IsResendEnabled = false;
                    ResendOtpColor = (Color)Application.Current.Resources["ResendOTPTextColor"];
                    timer.Start();
                }
            }
            catch (GAZTErrorException ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
            }
            catch (Exception ex)
            {

            }

        }

        public void OnAppearing()
        {
            Reset();
            StartTimer();
        }
    }
}
