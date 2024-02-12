using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
using System.Windows.Input;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.CreateGaztAccountPage
{

    public class CreateGaztAccountPageViewModel : ViewModelBase
    {
        #region Veriables
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand GoBackClick { get; set; }
        public Command OnResendOTPClicked { get; set; }
        public int numberOfSeconds = 120;
        int TotalSec;
        public bool StopTimer = false;
        #endregion
        #region Properties

        private bool _isLoading = false;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
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
                _isOTPEntryEnable = value;
                RaisePropertyChanged(() => IsOTPEntryEnable);
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
                RaisePropertyChanged("OTPValidDuration");
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
                RaisePropertyChanged("IsResendOTPEnabled");
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
                RaisePropertyChanged("IsVerifyOTPEnabled");
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
                RaisePropertyChanged("ButtonDisableColor");
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
                RaisePropertyChanged("ButtonDisableTextColor");
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
                RaisePropertyChanged("VerifyButtonDisableColor");
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
                RaisePropertyChanged("VerifyButtonDisableTextColor");
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
                RaisePropertyChanged("TxtEmailAddress");
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
                RaisePropertyChanged("TxtEmailCode");
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
                RaisePropertyChanged("TxtMobileNumber");
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
                RaisePropertyChanged("TxtMobileNumberCode");
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
                RaisePropertyChanged("TxtPassword");
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
                RaisePropertyChanged("TxtConfirmPassword");
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
                RaisePropertyChanged("SignUpModelRootObjectM");
            }
        }
        #endregion
        #region Constructor
        public CreateGaztAccountPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
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
                GoBackClick = new Command(async () =>
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
                    CreateModel.ABirthdt = SignUpModelRootObjectM.d.ABirthdt;
                    CreateModel.ACity = SignUpModelRootObjectM.d.ACity;
                    CreateModel.ACityCode = SignUpModelRootObjectM.d.ACityCode;
                    CreateModel.ACommId = SignUpModelRootObjectM.d.ACommId;
                    CreateModel.AEmail = SignUpModelRootObjectM.d.AEmail;
                    CreateModel.AFirstname = SignUpModelRootObjectM.d.AFirstname;
                    CreateModel.AIdnumber = SignUpModelRootObjectM.d.AIdnumber;
                    CreateModel.AIdtype = SignUpModelRootObjectM.d.AIdtype;
                    CreateModel.AIssuedBy = SignUpModelRootObjectM.d.AIssuedBy;
                    CreateModel.ALang = SignUpModelRootObjectM.d.ALang;
                    CreateModel.ALastname = SignUpModelRootObjectM.d.ALastname;
                    CreateModel.ALicenceNo = SignUpModelRootObjectM.d.ALicenceNo;
                    CreateModel.AMobile = SignUpModelRootObjectM.d.AMobile;
                    CreateModel.ACountry = SignUpModelRootObjectM.d.ACountry;

                    CreateModel.APhone = SignUpModelRootObjectM.d.APhone;
                    CreateModel.ATin = SignUpModelRootObjectM.d.ATin;
                    CreateModel.ATinExist = SignUpModelRootObjectM.d.ATinExist;
                    CreateModel.AType = SignUpModelRootObjectM.d.AType;
                    CreateModel.CaseGuid = SignUpModelRootObjectM.d.CaseGuid;
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
                    if (Device.RuntimePlatform == Device.iOS)
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
                CreateModel.ABirthdt = SignUpModelRootObjectM.d.ABirthdt;
                CreateModel.ACity = SignUpModelRootObjectM.d.ACity;
                CreateModel.ACityCode = SignUpModelRootObjectM.d.ACityCode;
                CreateModel.ACommId = SignUpModelRootObjectM.d.ACommId;
                CreateModel.AEmail = SignUpModelRootObjectM.d.AEmail;
                CreateModel.AFirstname = SignUpModelRootObjectM.d.AFirstname;
                CreateModel.AIdnumber = SignUpModelRootObjectM.d.AIdnumber;
                CreateModel.AIdtype = SignUpModelRootObjectM.d.AIdtype;
                CreateModel.AIssuedBy = SignUpModelRootObjectM.d.AIssuedBy;
                CreateModel.ALang = SignUpModelRootObjectM.d.ALang;
                CreateModel.ALastname = SignUpModelRootObjectM.d.ALastname;
                CreateModel.ALicenceNo = SignUpModelRootObjectM.d.ALicenceNo;
                CreateModel.AMobile = SignUpModelRootObjectM.d.AMobile;
                CreateModel.APhone = SignUpModelRootObjectM.d.APhone;
                CreateModel.ATin = SignUpModelRootObjectM.d.ATin;
                CreateModel.ATinExist = SignUpModelRootObjectM.d.ATinExist;
                CreateModel.AType = SignUpModelRootObjectM.d.AType;
                CreateModel.CaseGuid = SignUpModelRootObjectM.d.CaseGuid;
                CreateModel.APassword = TxtPassword;
                CreateModel.ASmsCode = TxtMobileNumberCode;
                CreateModel.AEmailCode = TxtEmailCode;
                CreateModel.ACountry = SignUpModelRootObjectM.d.ACountry;
                CreateModel.ASubmit = "X";
                CreateModel.Fbnum = SignUpModelRootObjectM.d.Fbnum;
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
                        SignupErrorModelRootObject SignupErrorModelRootObjectModel = JsonConvert.DeserializeObject<SignupErrorModelRootObject>(ResultFirstSubmit);
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            _dialogService.ShowMessage(SignupErrorModelRootObjectModel.error.innererror.errordetails[0].message, AppResources.Information);
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
