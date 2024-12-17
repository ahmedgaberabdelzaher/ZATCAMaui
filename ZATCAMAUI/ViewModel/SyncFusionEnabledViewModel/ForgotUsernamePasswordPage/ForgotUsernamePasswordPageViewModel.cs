


using System.Text;
using System.Windows.Input;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ForgotUsernamePasswordPage
{
    public class ForgotUsernamePasswordPageViewModel : BaseViewModel
    {
        #region Variable
        public ICommand OnSubmitClicked { get; set; }
        public ICommand OnCaptchaRegenerateClicked { get; set; }
        public ICommand OnChangePasswordSubmitClicked { get; set; }
        public Command OnLogInClick { get; set; }
        public ICommand OnLoginPageLinkClicked { get; set; }
        public int currentAttempts = 0;
        int TotalSec;
        public int numberOfSeconds = 120;
        ForgotPasswordOTP forgotPasswordOTP { get; set; }
        public bool StopTimer = true;
        #endregion
        #region Property

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
        private ForgotUserNamePassword _selectedTaxPayerType;
        public ForgotUserNamePassword SelectedTaxPayerType
        {
            get
            {
                return _selectedTaxPayerType;
            }
            set
            {
                _selectedTaxPayerType = value;
                if (_selectedTaxPayerType != null)
                {
                    TxtSelectTaxpayerType = _selectedTaxPayerType.TaxPayerType;
                }
                OnPropertyChanged("SelectedTaxPayerType");
                if (SelectedTaxPayerType != null)
                {
                    SetLayoutVisibilityForSelectedTaxpayerType();
                }
            }
        }
        private ForgotUserNamePassword _selectedTaxPayerTypePrev;
        public ForgotUserNamePassword SelectedTaxPayerTypePrev
        {
            get
            {
                return _selectedTaxPayerTypePrev;
            }
            set
            {
                _selectedTaxPayerTypePrev = value;
                OnPropertyChanged("SelectedTaxPayerTypePrev");
            }
        }
        private List<ForgotCredentialType> _forgotTypeList;
        public List<ForgotCredentialType> ForgotTypeList
        {
            get
            {
                return _forgotTypeList;
            }
            set
            {
                _forgotTypeList = value;
                OnPropertyChanged("ForgotTypeList");
            }
        }
        private List<TINModel> _tINs;
        public List<TINModel> TINs
        {
            get
            {
                return _tINs;
            }
            set
            {
                _tINs = value;
                OnPropertyChanged("TINs");
            }
        }
        private string _txtSelectedUsernameAndPassword;
        public string TxtSelectedUsernameAndPassword
        {
            get
            {
                return _txtSelectedUsernameAndPassword;
            }
            set
            {
                _txtSelectedUsernameAndPassword = value;
                OnPropertyChanged("TxtSelectedUsernameAndPassword");
            }
        }
        private string _txtSelectTaxpayerType;
        public string TxtSelectTaxpayerType
        {
            get
            {
                return _txtSelectTaxpayerType;
            }
            set
            {
                _txtSelectTaxpayerType = value;
                OnPropertyChanged("TxtSelectTaxpayerType");
            }
        }
        private string _txtTIN;
        public string TxtTIN
        {
            get
            {
                return _txtTIN;
            }
            set
            {
                _txtTIN = value;
                OnPropertyChanged("TxtTIN");
            }
        }
        private TINModel _selectedTinId;
        public TINModel SelectedTinId
        {
            get
            {
                return _selectedTinId;
            }
            set
            {
                _selectedTinId = value;
                if (_selectedTinId != null)
                {
                    TxtTIN = _selectedTinId.TIN;
                    App.CurrentDropdownTIN = SelectedTinId;
                    // Password = string.Empty;
                }
                OnPropertyChanged("SelectedTinId");
            }
        }
        private TINModel _selectedTinIdPrev;
        public TINModel SelectedTinIdPrev
        {
            get
            {
                return _selectedTinIdPrev;
            }
            set
            {
                _selectedTinIdPrev = value;
                OnPropertyChanged("SelectedTinIdPrev");
            }
        }
        private bool _isVisibleTinIds = false;
        public bool IsVisibleTinIds
        {
            get
            {
                return _isVisibleTinIds;
            }
            set
            {
                _isVisibleTinIds = value;
                OnPropertyChanged("IsVisibleTinIds");
            }
        }
        private ForgotCredentialType _selectedForgotType;
        public ForgotCredentialType SelectedForgotType
        {
            get
            {
                return _selectedForgotType;
            }
            set
            {
                _selectedForgotType = value;
                OnPropertyChanged("_selectedForgotType");
                if (SelectedForgotType != null)
                {
                    TxtSelectedUsernameAndPassword = _selectedForgotType.CredentialType;
                    IsTaxPayerTypeEnable = true;
                    IDNumber = string.Empty;
                    SetLayoutVisibilityForSelectedForgotType();
                }
                else
                {
                    IsTaxPayerTypeEnable = false;
                }
            }
        }
        private ForgotCredentialType _selectedForgotTypePrev;
        public ForgotCredentialType SelectedForgotTypePrev
        {
            get
            {
                return _selectedForgotTypePrev;
            }
            set
            {
                _selectedForgotTypePrev = value;
                OnPropertyChanged("_selectedForgotTypePrev");
            }
        }
        private ForgotUserNamePassword _forgotCredentialType;
        public ForgotUserNamePassword ForgotCredentialType
        {
            get
            {
                return _forgotCredentialType;
            }
            set
            {
                _forgotCredentialType = value;
                OnPropertyChanged("ForgotCredentialType");
            }
        }
        private List<ForgotUserNamePassword> _taxpayerTypeList;
        public List<ForgotUserNamePassword> TaxpayerTypeList
        {
            get
            {
                return _taxpayerTypeList;
            }
            set
            {
                _taxpayerTypeList = value;
                OnPropertyChanged("TaxpayerTypeList");
            }
        }
        private string _iDNumber;
        public string IDNumber
        {
            get
            {
                return _iDNumber;
            }
            set
            {
                _iDNumber = value;
                OnPropertyChanged("IDNumber");
            }
        }
        private string _enteredCaptchaValue;
        public string EnteredCaptchaValue
        {
            get
            {
                return _enteredCaptchaValue;
            }
            set
            {
                _enteredCaptchaValue = value;
                OnPropertyChanged("EnteredCaptchaValue");
            }
        }
        private string _corporateID;
        public string CorporateID
        {
            get
            {
                return _corporateID;
            }
            set
            {
                _corporateID = value;
                OnPropertyChanged("CorporateID");
            }
        }
        private string _mobileNumber;
        public string MobileNumber
        {
            get
            {
                return _mobileNumber;
            }
            set
            {
                _mobileNumber = value;
                OnPropertyChanged("MobileNumber");
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
                    VerifyButtonDisableColor = (Color)Application.Current.Resources["ButtonGray"];
                    IsResendOTPEnabled = true;
                    IsVerifyOTPEnabled = false;
                    IsOTPEntryEnable = false;
                }
                OnPropertyChanged("OTPValidDuration");
            }
        }
        private string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                OnPropertyChanged("UserName");
            }
        }
        private bool _isForgotUserNameWithIndividual = true;
        public bool IsForgotUserNameWithIndividual
        {
            get
            {
                return _isForgotUserNameWithIndividual;
            }
            set
            {
                _isForgotUserNameWithIndividual = value;
                OnPropertyChanged("IsForgotUserName");
            }
        }
        private bool _isForgotUserNameWithCorporate = false;
        public bool IsForgotUserNameWithCorporate
        {
            get
            {
                return _isForgotUserNameWithCorporate;
            }
            set
            {
                _isForgotUserNameWithCorporate = value;
                OnPropertyChanged("IsForgotUserNameWithCorporate");
            }
        }
        private bool _isForgotPassword = false;
        public bool IsForgotPassword
        {
            get
            {
                return _isForgotPassword;
            }
            set
            {
                _isForgotPassword = value;
                OnPropertyChanged("IsForgotPassword");
            }
        }
        private bool _isTaxPayerTypeEnable = false;
        public bool IsTaxPayerTypeEnable
        {
            get
            {
                return _isTaxPayerTypeEnable;
            }
            set
            {
                _isTaxPayerTypeEnable = value;
                OnPropertyChanged("IsTaxPayerTypeEnable");
            }
        }
        private bool _newPasswordLayoutVisibility = false;
        public bool NewPasswordLayoutVisibility
        {
            get
            {
                return _newPasswordLayoutVisibility;
            }
            set
            {
                _newPasswordLayoutVisibility = value;
                OnPropertyChanged("NewPasswordLayoutVisibility");
            }
        }
        private bool _mainPageLayoutVisibility = true;
        public bool MainPageLayoutVisibility
        {
            get
            {
                return _mainPageLayoutVisibility;
            }
            set
            {
                _mainPageLayoutVisibility = value;
                OnPropertyChanged("MainPageLayoutVisibility");
            }
        }
        private bool _oTPLayoutVisibility = false;
        public bool OTPLayoutVisibility
        {
            get
            {
                return _oTPLayoutVisibility;
            }
            set
            {
                _oTPLayoutVisibility = value;
                OnPropertyChanged("OTPLayoutVisibility");
            }
        }
        private bool _navigateToLoginLinkVisibility = false;
        public bool NavigateToLoginLinkVisibility
        {
            get
            {
                return _navigateToLoginLinkVisibility;
            }
            set
            {
                _navigateToLoginLinkVisibility = value;
                OnPropertyChanged("NavigateToLoginLinkVisibility");
            }
        }
        private string _newPassword;
        public string NewPassword
        {
            get
            {
                return _newPassword;
            }
            set
            {
                _newPassword = value;
                OnPropertyChanged("NewPassword");
            }
        }
        private string _confirmPassword;
        public string ConfirmPassword
        {
            get
            {
                return _confirmPassword;
            }
            set
            {
                _confirmPassword = value;
                OnPropertyChanged("ConfirmPassword");
            }
        }
        private string _iDNumberOrCorporateIDOrUserName = AppResources.IDNumber;
        public string IDNumberOrCorporateIDOrUserName
        {
            get
            {
                return _iDNumberOrCorporateIDOrUserName;
            }
            set
            {
                _iDNumberOrCorporateIDOrUserName = value;
                OnPropertyChanged("IDNumberOrCorporateIDOrUserName");
            }
        }
        private string _captcha;
        public string Captcha
        {
            get
            {
                return _captcha;
            }
            set
            {
                _captcha = value;
                OnPropertyChanged("Captcha");
            }
        }
        private string _forgotPasswordUserNameChangedMessage;
        public string ForgotPasswordUserNameChangedMessage
        {
            get
            {
                return _forgotPasswordUserNameChangedMessage;
            }
            set
            {
                _forgotPasswordUserNameChangedMessage = value;
                OnPropertyChanged("ForgotPasswordUserNameChangedMessage");
            }
        }
        private string _enteredOTP = "";
        public string EnteredOTP
        {
            get
            {
                return _enteredOTP;
            }
            set
            {
                _enteredOTP = value;
                OnPropertyChanged("EnteredOTP");
            }
        }
        private bool _isIDTypeVisible = false;
        public bool IsIDTypeVisible
        {
            get
            {
                return _isIDTypeVisible;
            }
            set
            {
                _isIDTypeVisible = value;
                OnPropertyChanged("IsIDTypeVisible");
            }
        }
        private bool _newPasswordVisibility = false;
        public bool NewPasswordVisibility
        {
            get
            {
                return _newPasswordVisibility;
            }
            set
            {
                _newPasswordVisibility = value;
                OnPropertyChanged("NewPasswordVisibility");
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
                OnResendOTPClicked.CanExecute(IsResendOTPEnabled);
                OnPropertyChanged("IsResendOTPEnabled");
            }
        }
        private bool _confirmPasswordVisibility = false;
        public bool ConfirmPasswordVisibility
        {
            get
            {
                return _confirmPasswordVisibility;
            }
            set
            {
                _confirmPasswordVisibility = value;
                OnPropertyChanged("ConfirmPasswordVisibility");
            }
        }
        private bool _isSubmitEnabled = false;
        public bool IsSubmitEnabled
        {
            get
            {
                return _isSubmitEnabled;
            }
            set
            {
                _isSubmitEnabled = value;
                OnPropertyChanged("IsSubmitEnabled");
            }
        }
        private Color _submitDisableButtonColor = (Color)Application.Current.Resources["ButtonGray"];
        public Color SubmitDisableButtonColor
        {
            get
            {
                return _submitDisableButtonColor;
            }
            set
            {
                _submitDisableButtonColor = value;
                OnPropertyChanged("SubmitDisableButtonColor");
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
                OnValidateOTPClicked.CanExecute(IsVerifyOTPEnabled);
                OnPropertyChanged("IsVerifyOTPEnabled");
            }
        }
        private int _maxChar = 60;
        public int MaxChar
        {
            get
            {
                return _maxChar;
            }
            set
            {
                _maxChar = value;
                OnPropertyChanged("MaxChar");
            }
        }
        private int _forgotTypeIndex;
        public int ForgotTypeIndex
        {
            get
            {
                return _forgotTypeIndex;
            }
            set
            {
                _forgotTypeIndex = value;
                if (_forgotTypeIndex == 0)
                {
                    // SelectedTaxPayerTypeIndex = 0;
                    if (TaxpayerTypeList != null && TaxpayerTypeList.Count > 0)
                    {
                        SelectedTaxPayerType = TaxpayerTypeList[0];
                    }
                }

                OnPropertyChanged("ForgotTypeIndex");
            }
        }

        private int _selectedTaxPayerTypeIndex;
        public int SelectedTaxPayerTypeIndex
        {
            get
            {
                return _selectedTaxPayerTypeIndex;
            }
            set
            {
                _selectedTaxPayerTypeIndex = value;
                OnPropertyChanged("SelectedTaxPayerTypeIndex");
            }
        }

        #endregion
        #region Constructor
        public ForgotUsernamePasswordPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            OnSubmitClicked = new Command(async () =>
            {
                try
                {
                    bool _isAllFormDataAvailable = ValidateForms();
                    if (_isAllFormDataAvailable)
                    {
                        // bool isValiedCaptcha = ValidateCaptcha();
                        if (true)
                        {
                            if (SelectedForgotType.id.Equals("1") && (SelectedTaxPayerType.id.Equals("1") || SelectedTaxPayerType.id.Equals("2")) && !string.IsNullOrEmpty(IDNumber))
                            {
                                await SendUserNameToRegidteredEmail();
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(IDNumber))
                                {
                                    await SendOTPToRegisterMobileNumber();
                                }
                                else
                                {
                                    await _dialogService.ShowMessageBox(AppResources.PleaseenterUsername, AppResources.Information);
                                }
                            }
                        }
                    }
                    else
                    {
                        await _dialogService.ShowMessageBox(AppResources.ZZMandatorydatanotentered, AppResources.Information);
                    }
                }
                catch (Exception)
                {


                }
            });
            OnCaptchaRegenerateClicked = new Command( () =>
            {
                StringBuilder captcha = GetCaptcha();
                Captcha = captcha.ToString();
                EnteredCaptchaValue = string.Empty;
            });
            OnChangePasswordSubmitClicked = new Command(async () =>
            {
                try
                {
                    bool _isMandatoryFieldEntered = IsMandatoryFieldEntered();
                    await ShowMandatoryFieldNotEnteredInformation(_isMandatoryFieldEntered);
                    if (_isMandatoryFieldEntered)
                    {
                        await ChangePassword();
                    }
                }
                catch (Exception)
                {


                }
            });

            OnLoginPageLinkClicked = new Command(() =>
           {
               _navigationService.GoBack();
           });
        }
        #endregion Constructor
        #region Method

        public ICommand OnResendOTPClicked
        {
            get
            {
                return new Command(async() =>
                {
                    await SendOTPToRegisterMobileNumber();
                }, ()=> _isResendOTPEnabled);
            }
        }

        public ICommand OnValidateOTPClicked
        {
            get
            {
                return new Command(async () =>
                {
                    await ValidateOTP();
                }, () => _isVerifyOTPEnabled);
            }
        }

        public Task OnPageLoad()
        {
            IDNumberOrCorporateIDOrUserName = AppResources.IDNumber;
            string lang = UtilityManager.GetLanguageParameter();
            try
            {
                StringBuilder captcha = GetCaptcha();
                Captcha = captcha.ToString();
                List<ForgotUserNamePassword> list = new List<ForgotUserNamePassword>
            {
                new ForgotUserNamePassword{ id = "1" , TaxPayerType = AppResources.Individual},
                new ForgotUserNamePassword{ id = "2" , TaxPayerType = AppResources.Company}
            };
                TaxpayerTypeList = list;
                List<ForgotCredentialType> forgotCredentialListlist = new List<ForgotCredentialType>
            {
                new ForgotCredentialType{ id = "1" , CredentialType = AppResources.ForgotUsername},
                new ForgotCredentialType{ id = "2" , CredentialType = AppResources.ForgotPassword}
            };
                ForgotTypeList = forgotCredentialListlist;
                if (ForgotTypeList != null && ForgotTypeList.Count != 0)
                {
                    SelectedForgotType = ForgotTypeList.Where(x => x.id == "2").FirstOrDefault();
                    ForgotTypeIndex = 1;
                }
            }
            catch (Exception)
            {


            }
            VerifyButtonDisableColor = (Color)Application.Current.Resources["Primary"];
            IsVerifyOTPEnabled = true;

            return Task.CompletedTask;
        }
        private void SetLayoutVisibilityForSelectedForgotType()
        {
            if (SelectedForgotType.id.Equals("2"))
            {
                IDNumberOrCorporateIDOrUserName = AppResources.UserName;
                IsTaxPayerTypeEnable = false;
                SelectedTaxPayerType = null;
                IsIDTypeVisible = false;
                MaxChar = 256;
            }
            else
            {
                IDNumberOrCorporateIDOrUserName = AppResources.IDNumber;
                IsIDTypeVisible = true;
            }
        }
        private void SetLayoutVisibilityForSelectedTaxpayerType()
        {
            if (SelectedTaxPayerType.id.Equals("1"))
            {
                if (!SelectedForgotType.id.Equals("2"))
                {
                    IDNumberOrCorporateIDOrUserName = AppResources.IDNumber;
                }

                MaxChar = 10;
            }
            else
            {
                IDNumberOrCorporateIDOrUserName = AppResources.CorportaeID;
                MaxChar = 60;
            }
        }
        private bool ValidateForms()
        {
            bool IsAllDataAvailable = false;
            if (SelectedForgotType != null && SelectedForgotType.id.Equals("2"))
            {
                if (!string.IsNullOrEmpty(IDNumber))
                {
                    bool IsEmailUserName = false;
                    IsEmailUserName = UtilityManager.IsValidEmailAddress(IDNumber);
                    if (IsEmailUserName)
                    {
                        if (SelectedTinId != null)
                        {
                            IsAllDataAvailable = true;
                        }
                        else
                        {
                            IsAllDataAvailable = false;
                        }
                    }
                    else
                    {
                        IsAllDataAvailable = true;

                    }
                }
                else
                {
                    IsAllDataAvailable = false;
                }
            }
            else if (SelectedTaxPayerType != null && (SelectedTaxPayerType.id.Equals("1") || SelectedTaxPayerType.id.Equals("2")))
            {
                if (SelectedTaxPayerType != null && !string.IsNullOrEmpty(IDNumber))
                {
                    IsAllDataAvailable = true;
                }
                else
                {
                    IsAllDataAvailable = false;
                }
            }
            else if (SelectedForgotType == null)
            {
                IsAllDataAvailable = false;
            }
            else
            {
                IsAllDataAvailable = false;
            }
            return IsAllDataAvailable;
        }
        public StringBuilder GetCaptcha()
        {
            StringBuilder Captcha;
            try
            {
                Random random = new Random();
                string combination = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                StringBuilder captcha = new StringBuilder();
                for (int i = 0; i < 6; i++)
                    captcha.Append(combination[random.Next(combination.Length)]);

                Captcha = captcha;
            }
            catch
            {
                throw;
            }
            return Captcha;
        }
        public bool ValidateCaptcha()
        {
            bool isValidCaptcha = false;
            isValidCaptcha = EnteredCaptchaValue.Equals(Captcha);
            if (EnteredCaptchaValue.Equals(Captcha))
            {
                isValidCaptcha = true;
                EnteredCaptchaValue = string.Empty;
            }
            else
            {

                isValidCaptcha = false;
            }
            return isValidCaptcha;
        }
        private async Task SendOTPToRegisterMobileNumber()
        {
            try
            {
                IsLoading = true;
                try
                {

                    string idNumber = GetTinId();
                    string lang = UtilityManager.GetLanguageParameter();
                    string st = ZATCAConstants.BaseUrlOfODataServices + ZATCAConstants.ForgotPasswordServiceName + "/HeaderSet(Tin=";
                    string id = st + "'" + idNumber + "'" + ",Langu='" + lang + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "" + "'" + ",MobileNo='" + "" + "'" + ",SubType='" + "" + "'" + ",Idnumber='" + "" + "'" + ",Otp='" + EnteredOTP + "'" + ",Dob=datetime'" + "2019-12-21T00%3A00%3A00" + "'" + ",NewPwd='" + "" + "'" + ",RdBt='" + "P" + "')";
                    string st1 = ZATCAConstants.BaseUrlOfODataServices + ZATCAConstants.ForgotPasswordServiceName + "/HeaderSet(Tin=";
                    string uri = st1 + "'" + idNumber + "'" + ",Langu='" + lang + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "" + "'" + ",MobileNo='" + "" + "'" + ",SubType='" + "" + "'" + ",Idnumber='" + "" + "'" + ",Otp='" + EnteredOTP + "'" + ",Dob=datetime'" + "2019-12-21T00%3A00%3A00" + "'" + ",NewPwd='" + "'" + ",RdBt='" + "P" + "')";
                    string type = ZATCAConstants.ForgotPasswordServiceName + ".Header";// "ZDP_FRGT_USRNM_PWD_SRV.Header";
                    ForgotPasswordOTP forgotPassword = new ForgotPasswordOTP();
                    Metadata metadata = new Metadata();
                    metadata.id = id;
                    metadata.uri = uri;
                    metadata.type = type;
                    D d = new D();
                    d.Action = "";
                    d.Tin = idNumber;
                    d.Langu = UtilityManager.GetLanguageParameter();
                    d.CurrAttmps = currentAttempts;
                    d.EmailId = "";
                    d.TpType = "";
                    d.MobileNo = "";
                    d.SubType = "";
                    d.Idnumber = "";
                    d.Otp = "";
                    d.Minutes = 0;
                    d.Name = "";
                    d.Attempts = 0;
                    d.NewPwd = "";
                    d.CnfPwd = "";
                    d.RdBt = "P";
                    d.Hyperlink = "";
                    forgotPassword.d = d;
                    forgotPasswordOTP = await WebServiceManager.GAZTFogotPasswordSendOTP(forgotPassword);
                    await PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                    if (forgotPasswordOTP.d != null && !string.IsNullOrEmpty(forgotPasswordOTP.d.EmailId))
                    {
                        MainPageLayoutVisibility = false;
                        OTPLayoutVisibility = true;
                        ButtonDisableColor = (Color)Application.Current.Resources["ButtonGray"];
                        VerifyButtonDisableColor = (Color)Application.Current.Resources["Primary"];
                        IsResendOTPEnabled = false;
                        IsVerifyOTPEnabled = true;
                        IsOTPEntryEnable = true;
                        string _mobileNumber = forgotPasswordOTP.d.MobileNo.Substring(forgotPasswordOTP.d.MobileNo.Length - 4);
                        MobileNumber = "XXXXXXXXXX" + _mobileNumber;
                        numberOfSeconds = 120;
                        TimerStart(numberOfSeconds);
                    }
                    else
                    {
                        await _dialogService.ShowMessageBox(AppResources.ZPleaseEnterAValidUserID, AppResources.ZError);
                    }
                }
                catch (GAZTVATRegistrationInProcessException exs)
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(exs.Message, AppResources.Information);

                }
                IsLoading = false;
            }
            catch (InternetException ex)
            {
                IsLoading = false;
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                
            }
        }
        private async Task ValidateOTP()
        {
            try
            {
                IsLoading = true;
                if (!string.IsNullOrEmpty(EnteredOTP))
                {
                    currentAttempts++;
                    string idNumber = GetTinId();
                    string lang = UtilityManager.GetLanguageParameter();
                    string st = ZATCAConstants.BaseUrlOfODataServices + ZATCAConstants.ForgotPasswordServiceName + "/HeaderSet(Tin=";
                    string id = st + "'" + idNumber + "'" + ",Langu='" + lang + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "" + "'" + ",MobileNo='" + "" + "'" + ",SubType='" + "" + "'" + ",Idnumber='" + "" + "'" + ",Otp='" + EnteredOTP + "'" + ",Dob=datetime'" + "2019-12-21T00%3A00%3A00" + "'" + ",NewPwd='" + "" + "'" + ",RdBt='" + "P" + "')";
                    string st1 = ZATCAConstants.BaseUrlOfODataServices + ZATCAConstants.ForgotPasswordServiceName + "/HeaderSet(Tin=";
                    string uri = st1 + "'" + idNumber + "'" + ",Langu='" + lang + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "" + "'" + ",MobileNo='" + "" + "'" + ",SubType='" + "" + "'" + ",Idnumber='" + "" + "'" + ",Otp='" + EnteredOTP + "'" + ",Dob=datetime'" + "2019-12-21T00%3A00%3A00" + "'" + ",NewPwd='" + "'" + ",RdBt='" + "P" + "')";
                    string type = ZATCAConstants.ForgotPasswordServiceName + ".Header";// "ZDP_FRGT_USRNM_PWD_SRV.Header";
                    ForgotPasswordOTP forgotPassword = new ForgotPasswordOTP();
                    Metadata metadata = new Metadata();
                    metadata.id = id;
                    metadata.uri = uri;
                    metadata.type = type;
                    D d = new D();
                    if (currentAttempts < 3)
                    {
                        d.Action = "01";
                    }
                    else
                    {
                        d.Action = "42";
                    }
                    d.Tin = idNumber;
                    d.Langu = UtilityManager.GetLanguageParameter();
                    d.CurrAttmps = currentAttempts;
                    d.EmailId = "";
                    d.TpType = "1";
                    d.MobileNo = "";
                    d.SubType = "ZS001";
                    d.Idnumber = "";
                    d.Otp = EnteredOTP;
                    d.Minutes = 0;
                    d.Name = "";
                    d.Attempts = 0;
                    d.NewPwd = "";
                    d.CnfPwd = "";
                    d.RdBt = "P";
                    d.Hyperlink = "";
                    forgotPassword.d = d;
                    forgotPassword = await WebServiceManager.GAZTForgotPasswordValidateOTP(forgotPassword);
                    await PopToRootPage();// If seesion Expired it will navigate to Dashboard page

                    if (forgotPassword != null && forgotPassword.d != null && forgotPassword.d.Action.Equals("01"))
                    {

                        await _dialogService.ShowMessageBox(AppResources.Pleasechangepassword, AppResources.Information);
                        OTPLayoutVisibility = false;
                        NewPasswordLayoutVisibility = true;
                        MobileNumber = forgotPassword.d.MobileNo;
                    }
                    else if (forgotPassword != null && forgotPassword.d != null && forgotPassword.d.Action.Equals("42"))
                    {
                        string messagefordialogue = AppResources.ZYouraccounthasbeenlockedPleasecontactourcallcenter;
                        if (!App.IsArabic)
                        {
                            messagefordialogue = messagefordialogue.Replace("{0}", "3");
                        }
                        else
                        {
                            messagefordialogue = messagefordialogue.Replace("}0{", "3");
                        }

                        await _dialogService.ShowMessageBox(messagefordialogue, AppResources.Information);
                        _navigationService.GoBack();
                    }
                    else
                    {
                        if (currentAttempts == 1)
                        {
                            await _dialogService.ShowMessageBox(AppResources.ZZZWrongverificationcode, AppResources.ZError);
                        }
                        else if (currentAttempts == 2)
                        {
                            string message = string.Format(AppResources.ZYouhaveoneremainingattemptthentheaccountwillbelocked, "1");
                            await _dialogService.ShowMessageBox(message, AppResources.ZError);
                        }
                        EnteredOTP = "";
                    }
                }
                else
                {
                    await _dialogService.ShowMessageBox(AppResources.EnterVerificationCode, AppResources.Information);
                }
                IsLoading = false;
            }
            catch (InternetException ex)
            {
                IsLoading = false;
                await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                
            }
        }
        private async Task SendUserNameToRegidteredEmail()
        {
            try
            {
                IsLoading = true;
                string idNumber = GetTinId();
               
                string lang = UtilityManager.GetLanguageParameter();
                string st = ZATCAConstants.BaseUrlOfODataServices + ZATCAConstants.ForgotPasswordServiceName + "/HeaderSet(Tin=";
                string id = st + "'" + "" + "'" + ",Langu='" + lang + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "1" + "'" + ",MobileNo='" + "" + "'" + ",SubType='" + "ZS001" + "'" + ",Idnumber='" + idNumber + "'" + ",Otp='" + "" + "'" + ",Dob=datetime'" + "2019-12-21T00%3A00%3A00" + "'" + ",NewPwd='" + "" + "'" + ",RdBt='" + "U" + "')";
                string st1 = ZATCAConstants.BaseUrlOfODataServices + ZATCAConstants.ForgotPasswordServiceName + "/HeaderSet(Tin=";
                string uri = st1 + "'" + "" + "'" + ",Langu='" + lang + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "" + "'" + ",MobileNo='" + "1" + "'" + ",SubType='" + "ZS001" + "'" + ",Idnumber='" + IDNumber + "'" + ",Otp='" + "" + "'" + ",Dob=datetime'" + "2019-12-21T00%3A00%3A00" + "'" + ",NewPwd='" + "" + "'" + ",RdBt='" + "U" + "')";
                string type = ZATCAConstants.ForgotPasswordServiceName + ".Header";// "ZDP_FRGT_USRNM_PWD_SRV.Header";
                ForgotPasswordOTP forgotPassword = new ForgotPasswordOTP();
                Metadata metadata = new Metadata();
                metadata.id = id;
                metadata.uri = uri;
                metadata.type = type;
                D d = new D();
                //   d.__metadata = metadata;
                d.Action = "40";
                d.Tin = "";
                d.Langu = UtilityManager.GetLanguageParameter();
                d.CurrAttmps = 0;
                d.EmailId = "";
                d.TpType = "1";
                d.MobileNo = "";
                d.SubType = "ZS001";
                d.Idnumber = idNumber;
                d.Otp = EnteredOTP;
                d.Minutes = 0;
                d.Name = "";
                d.Attempts = 0;
                d.NewPwd = NewPassword;
                d.CnfPwd = ConfirmPassword;
                d.RdBt = "U";
                d.Hyperlink = "";
                forgotPassword = await WebServiceManager.GAZTSendUserNameToEmail(forgotPassword);
                await PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                if (forgotPassword != null && !string.IsNullOrEmpty(forgotPassword.d.EmailId))
                {
                    MainPageLayoutVisibility = false;
                    NewPasswordLayoutVisibility = false;
                    OTPLayoutVisibility = false;
                    NavigateToLoginLinkVisibility = true;
                    ForgotPasswordUserNameChangedMessage = AppResources.Usernamehasbeensenttoregisteredmobilenumber;
                }
                else
                {
                    await _dialogService.ShowMessageBox(AppResources.ZPleaseEnterAValidUserID, AppResources.ZError);
                }
                IsLoading = false;
            }
            catch (InternetException ex)
            {
                IsLoading = false;
                await _dialogService.ShowMessageBox(ex.Message, AppResources.ZError);
               
            }
        }
        private async Task ChangePassword()
        {
            try
            {
                IsLoading = true;
                currentAttempts = 0;
                bool isNewPasswordValid = UtilityManager.IsPasswordValid(NewPassword);
                bool isConfirmPasswordValid = UtilityManager.IsPasswordValid(ConfirmPassword);
                if (isNewPasswordValid && isConfirmPasswordValid)
                {
                    string idNumber = GetTinId();
                    string lang = UtilityManager.GetLanguageParameter();
                    string st = ZATCAConstants.BaseUrlOfODataServices + ZATCAConstants.ForgotPasswordServiceName + "/HeaderSet(Tin=";
                    string id = st + "'" + idNumber + "'" + ",Langu='" + lang + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "" + "'" + ",MobileNo='" + "" + "'" + ",SubType='" + "" + "'" + ",Idnumber='" + "" + "'" + ",Otp='" + "" + "'" + ",Dob=datetime'" + "2019-12-21T00%3A00%3A00" + "'" + ",NewPwd='" + NewPassword + "'" + ",RdBt='" + "P" + "')";
                    string st1 = ZATCAConstants.BaseUrlOfODataServices + ZATCAConstants.ForgotPasswordServiceName + "/HeaderSet(Tin=";
                    string uri = st1 + "'" + idNumber + "'" + ",Langu='" + lang + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "" + "'" + ",MobileNo='" + "" + "'" + ",SubType='" + "" + "'" + ",Idnumber='" + "" + "'" + ",Otp='" + "" + "'" + ",Dob=datetime'" + "2019-12-21T00%3A00%3A00" + "'" + ",NewPwd='" + NewPassword + "'" + ",RdBt='" + "P" + "')";
                    string type = ZATCAConstants.ForgotPasswordServiceName + ".Header";// "ZDP_FRGT_USRNM_PWD_SRV.Header";
                    ForgotPasswordOTP forgotPassword = new ForgotPasswordOTP();
                    Metadata metadata = new Metadata();
                    metadata.id = id;
                    metadata.uri = uri;
                    metadata.type = type;
                    D d = new D();
                    d.Action = "40";
                    d.Tin = idNumber;
                    d.Langu = UtilityManager.GetLanguageParameter();
                    d.CurrAttmps = 0;
                    d.EmailId = "";
                    d.TpType = "1";
                    d.MobileNo = "";
                    d.SubType = "";
                    d.Idnumber = "";
                    d.Otp = EnteredOTP;
                    d.Minutes = 0;
                    d.Name = "";
                    d.Attempts = 0;
                    d.NewPwd = NewPassword;
                    d.CnfPwd = ConfirmPassword;
                    d.RdBt = "P";
                    d.Hyperlink = "";
                    forgotPassword.d = d;
                    if (NewPassword.Equals(ConfirmPassword))
                    {
                        forgotPassword = await WebServiceManager.GAZTChangePassword(forgotPassword);
                        await PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                        if (forgotPassword != null && !string.IsNullOrEmpty(forgotPassword.d.EmailId))
                        {
                            NewPasswordLayoutVisibility = false;
                            OTPLayoutVisibility = false;
                            NavigateToLoginLinkVisibility = true;
                            ForgotPasswordUserNameChangedMessage = AppResources.ZZYourPasswordhasbeenChangedsuccessfully;
                        }
                        else
                        {
                            await _dialogService.ShowMessageBox(AppResources.Somethingwentwrong, AppResources.Information);
                        }
                    }
                    else
                    {
                        await _dialogService.ShowMessageBox(AppResources.Boththepasswordfieldsshouldmatch, AppResources.Information);
                    }
                }
                else
                {
                    await _dialogService.ShowMessageBox(AppResources.PasswordGuidelineText, AppResources.Alerts);
                }
                IsLoading = false;
            }
            catch (InternetException ex)
            {
                IsLoading = false;
                await _dialogService.ShowMessageBox(ex.Message, AppResources.Alerts);
                
            }
        }
        public async Task PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                var _navigation = Application.Current.MainPage.Navigation;
                foreach (var item in _navigation.NavigationStack)
                {
                    if (item.GetType().Name == App.SFAnonymousLandingPageView)
                    {
                        _navigation.RemovePage(item);
                        break;
                    }
                }
                await _navigationService.NavigateTo(App.SFAnonymousLandingPageView);
                _navigation.NavigationStack.ToList().Clear();
            }
        }
        public async Task SetTinsListLayoutVisibility(bool IsEmailUserName)
        {
            IsLoading = true;
            if (IsEmailUserName)
            {
                TINs = new List<TINModel>();
                List<TINModel> Tins = new List<TINModel>();
                try
                {
                    try
                    {
                        SelectedTinId = null;
                        Tins = await WebServiceManager.GAZTGetAllTins(IDNumber);
                        TINs = Tins;
                        if (Tins.Count != 0 && SelectedTinId == null)
                        {
                            IsVisibleTinIds = true;
                            SelectedTinId = TINs[0];
                        }
                        else
                        {
                            IsVisibleTinIds = false;
                            await _dialogService.ShowMessageBox(AppResources.NoTINsAvailable, AppResources.Information);
                        }
                        IsLoading = false;
                    }
                    catch (Exception)
                    {
                        IsVisibleTinIds = false;
                        IsVisibleTinIds = false;
                        await _dialogService.ShowMessageBox(AppResources.NetworkConnectivityIssue, AppResources.Information);
                    }
                }
                catch (InternetException ex)
                {
                    IsLoading = false;
                    await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                    
                }
            }
            else
            {
                IsVisibleTinIds = false;
            }
            IsLoading = false;
        }
        private string GetTinId()
        {
            string tinId = "";
            bool isEmailUser = UtilityManager.IsValidEmailAddress(IDNumber);
            if (isEmailUser)
            {
                tinId = SelectedTinId.TIN;
            }
            else
            {
                tinId = IDNumber;
            }
            return tinId;
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
                        IsVerifyOTPEnabled = false;
                        return false;
                    }
                    else if (!StopTimer)
                    {
                        IsVerifyOTPEnabled = false;
                        return false;
                    }
                    else
                    {
                    }
                    if (TotalSec < 0)
                    {
                        OTPValidDuration = " 0:00";
                        ButtonDisableColor = (Color)Application.Current.Resources["Primary"];
                        IsResendOTPEnabled = true;
                        VerifyButtonDisableColor = (Color)Application.Current.Resources["ButtonGray"];
                        IsVerifyOTPEnabled = false;
                        IsOTPEntryEnable = false;
                        return false;
                    }
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

        private bool IsMandatoryFieldEntered()
        {
            bool IsMandatoryFieldEntered = false;
            if (string.IsNullOrEmpty(NewPassword) || string.IsNullOrEmpty(ConfirmPassword))
            {
                IsMandatoryFieldEntered = false;
            }
            else
            {
                IsMandatoryFieldEntered = true;
            }
            return IsMandatoryFieldEntered;
        }
        private async Task ShowMandatoryFieldNotEnteredInformation(bool IsMandatoryFieldEntered)
        {
            if (!IsMandatoryFieldEntered)
            {
                IsVisibleTinIds = false;
                IsLoading = false;
                await _dialogService.ShowMessageBox(AppResources.ZZMandatorydatanotentered, AppResources.Alerts);
            }
        }

        public void ClearData()
        {
            SelectedTaxPayerType = null;
        }
        #endregion
    }
}
