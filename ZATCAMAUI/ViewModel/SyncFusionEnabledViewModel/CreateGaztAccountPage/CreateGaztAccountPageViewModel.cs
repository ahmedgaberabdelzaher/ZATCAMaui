

using Newtonsoft.Json;
using System.Text;
using System.Windows.Input;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using static ZATCAMAUI.Models.ErrorMessage;
using Application = Microsoft.Maui.Controls.Application;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.CreateGaztAccountPage
{

    public class CreateGaztAccountPageViewModel : BaseViewModel
    {
        #region Veriables
        public ICommand GoBackClick { get; set; }
        public Command OnResendOTPClicked { get; set; }
        public int numberOfSeconds = 120;
        int TotalSec;
        public bool StopTimer = false;
        #endregion
        #region Properties



        private bool _isOTPEntryEnable = true;
        public bool IsOTPEntryEnable
        {
            get
            {
                return _isOTPEntryEnable;
            }
            set
            {
                _isOTPEntryEnable = value;
                OnPropertyChanged(nameof(IsOTPEntryEnable));
            }
        }
        private string _oTPValidDuration;
        public string OTPValidDuration
        {
            get
            {
                return _oTPValidDuration;
            }
            set
            {
                _oTPValidDuration = value;
                if (_oTPValidDuration.Equals(" 00:00"))
                {
                    ButtonDisableColor = (Color)Application.Current.Resources["Primary"];
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
        private bool _isResendOTPEnabled = false;
        public bool IsResendOTPEnabled
        {
            get
            {
                return _isResendOTPEnabled;
            }
            set
            {
                _isResendOTPEnabled = value;
                OnPropertyChanged("IsResendOTPEnabled");
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
                _isVerifyOTPEnabled = value;
                OnPropertyChanged("IsVerifyOTPEnabled");
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
                _buttonDisableColor = value;
                OnPropertyChanged("ButtonDisableColor");
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
                _buttonDisableTextColor = value;
                OnPropertyChanged("ButtonDisableTextColor");
            }
        }
        private Color _verifybuttonDisableColor = (Color)Application.Current.Resources["Primary"];
        public Color VerifyButtonDisableColor
        {
            get
            {
                return _verifybuttonDisableColor;
            }
            set
            {
                _verifybuttonDisableColor = value;
                OnPropertyChanged("VerifyButtonDisableColor");
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
                _verifybuttonDisableTextColor = value;
                OnPropertyChanged("VerifyButtonDisableTextColor");
            }
        }
        private string _txtEmailAddress = string.Empty;
        public string TxtEmailAddress
        {
            get
            {
                return _txtEmailAddress;
            }
            set
            {
                _txtEmailAddress = value;
                OnPropertyChanged("TxtEmailAddress");
            }
        }
        private string _txtEmailCode = string.Empty;
        public string TxtEmailCode
        {
            get
            {
                return _txtEmailCode;
            }
            set
            {
                _txtEmailCode = value;
                OnPropertyChanged("TxtEmailCode");
            }
        }
        private string _txtMobileNumber = string.Empty;
        public string TxtMobileNumber
        {
            get
            {
                return _txtMobileNumber;
            }
            set
            {
                _txtMobileNumber = value;
                OnPropertyChanged("TxtMobileNumber");
            }
        }
        private string _txtMobileNumberCode = string.Empty;
        public string TxtMobileNumberCode
        {
            get
            {
                return _txtMobileNumberCode;
            }
            set
            {
                _txtMobileNumberCode = value;
                OnPropertyChanged("TxtMobileNumberCode");
            }
        }
        private string _txtPassword = string.Empty;
        public string TxtPassword
        {
            get
            {
                return _txtPassword;
            }
            set
            {
                _txtPassword = value;
                OnPropertyChanged("TxtPassword");
            }
        }
        private string _txtConfirmPassword = string.Empty;
        public string TxtConfirmPassword
        {
            get
            {
                return _txtConfirmPassword;
            }
            set
            {
                _txtConfirmPassword = value;
                OnPropertyChanged("TxtConfirmPassword");
            }
        }
        private SignUpModelRootObject _signUpModelRootObjectM = null;
        public SignUpModelRootObject SignUpModelRootObjectM
        {
            get
            {
                return _signUpModelRootObjectM;
            }
            set
            {
                _signUpModelRootObjectM = value;
                OnPropertyChanged("SignUpModelRootObjectM");
            }
        }
        #endregion
        #region Constructor
        public CreateGaztAccountPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            try
            {
                OnResendOTPClicked = new Command(async () =>
                {
                    try
                    {
                        await SendOTPToRegisterMobileNumberToLogIn();
                    }
                    catch (Exception)
                    {


                    }

                });
                GoBackClick = new Command(() =>
                {
                    _navigationService.GoBack();
                });
            }
            catch (Exception)
            {


            }
        }
        #endregion
        #region Methods
        public void OnPageLoad()
        {
            StopTimer = true;
            //numberOfSeconds = 120;
            //TimerStart(numberOfSeconds);
            IsResendOTPEnabled = false;
            IsVerifyOTPEnabled = true;
        }
        private async Task SendOTPToRegisterMobileNumberToLogIn()
        {
            try
            {
                await Task.Run(async () =>
                {
                    SignUpNextBodyModel CreateModel = new SignUpNextBodyModel();
                    CreateModel.ABirthdt = SignUpModelRootObjectM.d.signupD.ABirthdt;
                    CreateModel.ACity = SignUpModelRootObjectM.d.signupD.ACity;
                    CreateModel.ACityCode = SignUpModelRootObjectM.d.signupD.ACityCode;
                    CreateModel.ACommId = SignUpModelRootObjectM.d.signupD.ACommId;
                    CreateModel.AEmail = SignUpModelRootObjectM.d.signupD.AEmail;
                    CreateModel.AFirstname = SignUpModelRootObjectM.d.signupD.AFirstname;
                    CreateModel.AIdnumber = SignUpModelRootObjectM.d.signupD.AIdnumber;
                    CreateModel.AIdtype = SignUpModelRootObjectM.d.signupD.AIdtype;
                    CreateModel.AIssuedBy = SignUpModelRootObjectM.d.signupD.AIssuedBy;
                    CreateModel.ALang = SignUpModelRootObjectM.d.signupD.ALang;
                    CreateModel.ALastname = SignUpModelRootObjectM.d.signupD.ALastname;
                    CreateModel.ALicenceNo = SignUpModelRootObjectM.d.signupD.ALicenceNo;
                    CreateModel.AMobile = SignUpModelRootObjectM.d.signupD.AMobile;
                    CreateModel.ACountry = SignUpModelRootObjectM.d.signupD.ACountry;

                    CreateModel.APhone = SignUpModelRootObjectM.d.signupD.APhone;
                    CreateModel.ATin = SignUpModelRootObjectM.d.signupD.ATin;
                    CreateModel.ATinExist = SignUpModelRootObjectM.d.signupD.ATinExist;
                    CreateModel.AType = SignUpModelRootObjectM.d.signupD.AType;
                    CreateModel.CaseGuid = SignUpModelRootObjectM.d.signupD.CaseGuid;
                    CreateModel.ACaptcha = SignUpModelRootObjectM.d.signupD.ACaptcha;
                    string ResultFirstSubmit = await WebServiceManager.GAZTSignUpFirstSubmitCGZTAcc(CreateModel);
                    SignUpModelRootObject ResultFirstSubmitModel = JsonConvert.DeserializeObject<SignUpModelRootObject>(ResultFirstSubmit);
                    if (ResultFirstSubmitModel.d == null)
                    {
                        SignupErrorModelRootObject SignupErrorModelRootObjectModel = JsonConvert.DeserializeObject<SignupErrorModelRootObject>(ResultFirstSubmit);
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            _dialogService.ShowMessage(SignupErrorModelRootObjectModel.error.innererror.errordetails[0].message, AppResources.Information);
                        });
                    }
                    else
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            _dialogService.ShowMessage(AppResources.ZZYournewEmailandSMSValidationCodehasbeenresenttoyou, AppResources.Information);
                        });
                        ButtonDisableColor = (Color)Application.Current.Resources["ButtonGray"];
                        ButtonDisableTextColor = Colors.Gray;
                        VerifyButtonDisableColor = (Color)Application.Current.Resources["Primary"];
                        VerifyButtonDisableTextColor = Colors.White;
                        IsResendOTPEnabled = false;
                        IsVerifyOTPEnabled = true;
                        IsOTPEntryEnable = true;
                        numberOfSeconds = 120;
                        TimerStart(numberOfSeconds);
                    }

                });
            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
            }
        }
        public void TimerStart(int Seconds)
        {
            IsVerifyOTPEnabled = true;
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
                        TotalSec = TotalSec - Convert.ToInt32(App.TimeDifference);
                        App.IsComingFromSleepMode = false;
                        StopTimer = true;
                        // TimerStart(TotalSec);
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
                    else if (!StopTimer)
                    {
                        return false;
                    }
                    else
                    {
                    }
                    if (TotalSec < 0)
                    {
                        OTPValidDuration = " 0:00";
                        ButtonDisableColor = (Color)Application.Current.Resources["Primary"];
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
        public async void CreateGaZTAccount()
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                CreateGaztAccountModel CreateModel = new CreateGaztAccountModel();
                CreateModel.ABirthdt = SignUpModelRootObjectM.d.signupD.ABirthdt;
                CreateModel.ACity = SignUpModelRootObjectM.d.signupD.ACity;
                CreateModel.ACityCode = SignUpModelRootObjectM.d.signupD.ACityCode;
                CreateModel.ACommId = SignUpModelRootObjectM.d.signupD.ACommId;
                CreateModel.AEmail = SignUpModelRootObjectM.d.signupD.AEmail;
                CreateModel.AFirstname = SignUpModelRootObjectM.d.signupD.AFirstname;
                CreateModel.AIdnumber = SignUpModelRootObjectM.d.signupD.AIdnumber;
                CreateModel.AIdtype = SignUpModelRootObjectM.d.signupD.AIdtype;
                CreateModel.AIssuedBy = SignUpModelRootObjectM.d.signupD.AIssuedBy;
                CreateModel.ALang = SignUpModelRootObjectM.d.signupD.ALang;
                CreateModel.ALastname = SignUpModelRootObjectM.d.signupD.ALastname;
                CreateModel.ALicenceNo = SignUpModelRootObjectM.d.signupD.ALicenceNo;
                CreateModel.AMobile = SignUpModelRootObjectM.d.signupD.AMobile;
                CreateModel.APhone = SignUpModelRootObjectM.d.signupD.APhone;
                CreateModel.ATin = SignUpModelRootObjectM.d.signupD.ATin;
                CreateModel.ATinExist = SignUpModelRootObjectM.d.signupD.ATinExist;
                CreateModel.AType = SignUpModelRootObjectM.d.signupD.AType;
                CreateModel.CaseGuid = SignUpModelRootObjectM.d.signupD.CaseGuid;
                CreateModel.APassword = TxtPassword;
                CreateModel.ASmsCode = TxtMobileNumberCode;
                CreateModel.AEmailCode = TxtEmailCode;
                CreateModel.ACountry = SignUpModelRootObjectM.d.signupD.ACountry;
                CreateModel.ASubmit = "X";
                CreateModel.Fbnum = SignUpModelRootObjectM.d.signupD.Fbnum;
                string ResultFirstSubmit = await WebServiceManager.GAZTCreateAccountSubmit(CreateModel);
                if (ResultFirstSubmit != null)
                {
                    SignUpModelRootObject ResultFirstSubmitModel = JsonConvert.DeserializeObject<SignUpModelRootObject>(ResultFirstSubmit);
                    if (ResultFirstSubmitModel.d == null)
                    {
                        await Task.Run(() =>
                        {
                            IsLoading = false;
                        });
                        ErrorObj SignupErrorModelRootObjectModel = JsonConvert.DeserializeObject<ErrorObj>(ResultFirstSubmit);

                        StringBuilder message = new StringBuilder();
                        foreach (Errordetail itemerror in SignupErrorModelRootObjectModel.error.innererror.errordetails)
                        {
                            message.AppendLine(itemerror.message);
                        }
                        message = message.Replace("An exception was raised", string.Empty);
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            _dialogService.ShowMessage(message.ToString(), AppResources.Information);
                        });
                    }
                    else
                    {
                        await Task.Run(() =>
                        {
                            IsLoading = false;
                        });
                        _navigationService.NavigateTo(App.AccountCreatedPageView);
                    }
                }
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
            }
        }
        #endregion
    }
}
