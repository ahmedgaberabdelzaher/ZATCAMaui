using EGAZT;
using EGAZT.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.ForgotUsernamePasswordPage_ViewModel
{
    [Preserve(AllMembers = true)]
    public class ForgotUsernamePasswordPageViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnSubmitClicked { get; set; }
        public ICommand OnCaptchaRegenerateClicked { get; set; }
        public ICommand OnChangePasswordSubmitClicked { get; set; }
        public Command OnResendOTPClicked { get; set; }
        public Command OnValidateOTPClicked { get; set; }
        public Command OnLogInClick { get; set; }
        public ICommand OnLoginPageLinkClicked { get; set; }
        public ICommand BackButtonClicked { get; set; }
        public int currentAttempts = 0;
        int TotalSec;
        public int numberOfSeconds = 120;
        ForgotPasswordOTP forgotPasswordOTP { get; set; }
        public bool StopTimer = true;
        #endregion
        #region Property
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
                RaisePropertyChanged(() => IsLoading);
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
                RaisePropertyChanged("SelectedTaxPayerType");
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
                RaisePropertyChanged("SelectedTaxPayerTypePrev");
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
                RaisePropertyChanged("ForgotTypeList");
            }
        }
        private List<TIN> _tINs;
        public List<TIN> TINs
        {
            get
            {
                return _tINs;
            }
            set
            {
                _tINs = value;
                RaisePropertyChanged("TINs");
            }
        }
        private String _txtSelectedUsernameAndPassword;
        public String TxtSelectedUsernameAndPassword
        {
            get
            {
                return _txtSelectedUsernameAndPassword;
            }
            set
            {
                _txtSelectedUsernameAndPassword = value;
                RaisePropertyChanged("TxtSelectedUsernameAndPassword");
            }
        }
        private String _txtSelectTaxpayerType;
        public String TxtSelectTaxpayerType
        {
            get
            {
                return _txtSelectTaxpayerType;
            }
            set
            {
                _txtSelectTaxpayerType = value;
                RaisePropertyChanged("TxtSelectTaxpayerType");
            }
        }
        private String _txtTIN;
        public String TxtTIN
        {
            get
            {
                return _txtTIN;
            }
            set
            {
                _txtTIN = value;
                RaisePropertyChanged("TxtTIN");
            }
        }
        private TIN _selectedTinId;
        public TIN SelectedTinId
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
                    TxtTIN = _selectedTinId.Tin;
                    App.CurrentDropdownTIN = SelectedTinId;
                    // Password = string.Empty;
                }
                RaisePropertyChanged("SelectedTinId");
            }
        }
        private TIN _selectedTinIdPrev;
        public TIN SelectedTinIdPrev
        {
            get
            {
                return _selectedTinIdPrev;
            }
            set
            {
                _selectedTinIdPrev = value;
                RaisePropertyChanged("SelectedTinIdPrev");
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
                RaisePropertyChanged("IsVisibleTinIds");
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
                RaisePropertyChanged("_selectedForgotType");
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
                RaisePropertyChanged("_selectedForgotTypePrev");
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
                RaisePropertyChanged("ForgotCredentialType");
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
                RaisePropertyChanged("TaxpayerTypeList");
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
                RaisePropertyChanged("IDNumber");
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
                RaisePropertyChanged("EnteredCaptchaValue");
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
                RaisePropertyChanged("CorporateID");
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
                RaisePropertyChanged("MobileNumber");
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
                    ButtonDisableColor =  (Color)Application.Current.Resources["Primary"];
                    VerifyButtonDisableColor =  (Color)Application.Current.Resources["ButtonGray"];
                    IsResendOTPEnabled = true;
                    IsVerifyOTPEnabled = false;
                    IsOTPEntryEnable = false;
                }
                RaisePropertyChanged("OTPValidDuration");
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
                RaisePropertyChanged("UserName");
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
                RaisePropertyChanged("IsForgotUserName");
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
                RaisePropertyChanged("IsForgotUserNameWithCorporate");
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
                RaisePropertyChanged("IsForgotPassword");
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
                RaisePropertyChanged("IsTaxPayerTypeEnable");
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
                RaisePropertyChanged("NewPasswordLayoutVisibility");
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
                RaisePropertyChanged("MainPageLayoutVisibility");
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
                RaisePropertyChanged("OTPLayoutVisibility");
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
                RaisePropertyChanged("NavigateToLoginLinkVisibility");
            }
        }
        private string _newPassword = "";
        public string NewPassword
        {
            get
            {
                return _newPassword;
            }
            set
            {
                _newPassword = value;
                RaisePropertyChanged("NewPassword");
            }
        }
        private string _confirmPassword = "";
        public string ConfirmPassword
        {
            get
            {
                return _confirmPassword;
            }
            set
            {
                _confirmPassword = value;
                RaisePropertyChanged("ConfirmPassword");
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
                RaisePropertyChanged("IDNumberOrCorporateIDOrUserName");
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
                RaisePropertyChanged("Captcha");
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
                RaisePropertyChanged("ForgotPasswordUserNameChangedMessage");
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
                RaisePropertyChanged("EnteredOTP");
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
                RaisePropertyChanged("IsIDTypeVisible");
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
                RaisePropertyChanged("NewPasswordVisibility");
            }
        }
        private Color _buttonDisableColor =  (Color)Application.Current.Resources["ButtonGray"];
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
                OnResendOTPClicked.ChangeCanExecute();
                RaisePropertyChanged("IsResendOTPEnabled");
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
                RaisePropertyChanged("ConfirmPasswordVisibility");
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
                RaisePropertyChanged("IsSubmitEnabled");
            }
        }
        private Color _submitDisableButtonColor =  (Color)Application.Current.Resources["ButtonGray"];
        public Color SubmitDisableButtonColor
        {
            get
            {
                return _submitDisableButtonColor;
            }
            set
            {
                _submitDisableButtonColor = value;
                RaisePropertyChanged("SubmitDisableButtonColor");
            }
        }
        private Color _verifybuttonDisableColor =  (Color)Application.Current.Resources["Primary"];
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
                OnValidateOTPClicked.ChangeCanExecute();
                RaisePropertyChanged("IsVerifyOTPEnabled");
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
                RaisePropertyChanged("MaxChar");
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

                RaisePropertyChanged("ForgotTypeIndex");
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
                RaisePropertyChanged("SelectedTaxPayerTypeIndex");
            }
        }

        #endregion
        #region Constructor
        public ForgotUsernamePasswordPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            _navigationService = navigationService;
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            _dialogService = dialogService;
            BackButtonClicked = new Xamarin.Forms.Command(() =>
            {
                _navigationService.GoBack();
            });
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
                            if (SelectedForgotType.id.Equals("1") && ((SelectedTaxPayerType.id.Equals("1")) || (SelectedTaxPayerType.id.Equals("2"))) && !(string.IsNullOrEmpty(IDNumber)))
                            {
                                await SendUserNameToRegidteredEmail();
                            }
                            else
                            {
                                if (!String.IsNullOrEmpty(IDNumber))
                                {
                                    await SendOTPToRegisterMobileNumber();
                                }
                                else
                                {
                                    _dialogService.ShowMessageBox(AppResources.PleaseenterUsername, AppResources.Information);
                                }
                            }
                        }
                        else
                        {
                            await _dialogService.ShowMessageBox(AppResources.enteredcaptchacodeisincorrect, AppResources.Information);
                        }
                    }
                    else
                    {
                        await _dialogService.ShowMessageBox(AppResources.ZZMandatorydatanotentered, AppResources.Information);
                    }
                    //StringBuilder captcha = GetCaptcha();
                    //Captcha = captcha.ToString();
                    //EnteredCaptchaValue = string.Empty;
                }
                catch (Exception ex)
                {
                }
            });
            OnCaptchaRegenerateClicked = new Command(async () =>
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
                    // bool _isNewPasswordAndConfirmPasswordSame = IsNewPasswordSameAsOldPasswordSame();
                    await ShowMandatoryFieldNotEnteredInformation(_isMandatoryFieldEntered);
                    if (_isMandatoryFieldEntered)
                    {
                        //if (!_isNewPasswordAndConfirmPasswordSame)
                        //{
                        await ChangePassword();
                        //}
                        //else
                        //{
                        //    await ShowNewPasswordAndOldPassowrdNotBeSameInformation();
                        //}
                    }
                }
                catch (Exception ex)
                {
                }
            });
            //OnResendOTPClicked = new Command(async () =>
            //{
            //    await SendOTPToRegisterMobileNumber();
            //});
            OnResendOTPClicked = new Command(ExecuteResendOTPClickCommand, CanExecuteResendOTPClickCommand);
            //OnValidateOTPClicked = new Command(async () =>
            //{
            //    await ValidateOTP();
            //});
            OnValidateOTPClicked = new Command(ExecuteSubmitClickCommand, CanExecuteSubmitClickCommand);
            OnLoginPageLinkClicked = new Command(() =>
           {
               _navigationService.GoBack();
           });
        }
        #endregion Constructor
        #region Method
        bool CanExecuteSubmitClickCommand(object arg)
        {
            return _isVerifyOTPEnabled;
        }
        public async void ExecuteSubmitClickCommand(object obj)
        {
            await ValidateOTP();
        }
        bool CanExecuteResendOTPClickCommand(object arg)
        {
            return _isResendOTPEnabled;
        }
        public async void ExecuteResendOTPClickCommand(object obj)
        {
            await SendOTPToRegisterMobileNumber();
        }
        public async Task OnPageLoad()
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
            catch (Exception ex)
            {
            }
            VerifyButtonDisableColor =  (Color)Application.Current.Resources["Primary"];
            IsVerifyOTPEnabled = true;
        }
        private void SetLayoutVisibilityForSelectedForgotType()
        {
            if (SelectedForgotType.id.Equals("2"))
            {
                IDNumberOrCorporateIDOrUserName = AppResources.UserName;
                //Device.BeginInvokeOnMainThread(() => {
                IsTaxPayerTypeEnable = false;
                SelectedTaxPayerType = null;
                IsIDTypeVisible = false;
                MaxChar = 256;
                //IsForgotPassword = true;
                //IsForgotUserNameWithIndividual = false;
                //IsForgotUserNameWithCorporate = false;
                //});
            }
            else
            {
                IDNumberOrCorporateIDOrUserName = AppResources.IDNumber;
                IsIDTypeVisible = true;
                //IsTaxPayerTypeEnable = true;
                //IsForgotPassword = false;
                //IsForgotUserNameWithIndividual = true;
                //IsForgotUserNameWithCorporate = false;
            }
        }
        private void SetLayoutVisibilityForSelectedTaxpayerType()
        {
            if (SelectedTaxPayerType.id.Equals("1"))
            {
                if (!(SelectedForgotType.id.Equals("2")))
                {
                    IDNumberOrCorporateIDOrUserName = AppResources.IDNumber;
                }

                MaxChar = 10;
                //IsForgotUserNameWithIndividual = true;
                //IsForgotUserNameWithCorporate = false;
            }
            else
            {
                //IsForgotUserNameWithIndividual = false;
                //IsForgotUserNameWithCorporate = true;
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
                        //if (SelectedTinId != null && !string.IsNullOrEmpty(EnteredCaptchaValue))
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
                        //if (!string.IsNullOrEmpty(EnteredCaptchaValue))
                        //{
                        //    IsAllDataAvailable = true;
                        //}
                        //else
                        //{
                        //    IsAllDataAvailable = false; 
                        //}
                    }
                }
                else
                {
                    IsAllDataAvailable = false;
                }
            }
            else if (SelectedTaxPayerType != null && (SelectedTaxPayerType.id.Equals("1") || SelectedTaxPayerType.id.Equals("2")))
            {
                //if (SelectedTaxPayerType != null && !string.IsNullOrEmpty(IDNumber) && !string.IsNullOrEmpty(EnteredCaptchaValue))
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
                //Session["captcha"] = captcha.ToString();
                //imgCaptcha.ImageUrl = "~/Captcha/GenerateCaptcha.aspx?" + DateTime.Now.Ticks.ToString();
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
                // _dialogService.ShowMessageBox(AppResources.InvaliedCaptcha, AppResources.Information);
                isValidCaptcha = false;
            }
            return isValidCaptcha;
        }
        private async Task SendOTPToRegisterMobileNumber()
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    try
                    {
                       
                        string idNumber = GetTinId();
                        string lang = UtilityManager.GetLanguageParameter();
                        string st = Constants.BaseUrlOfODataServices + Constants.ForgotPasswordServiceName + "/HeaderSet(Tin=";
                        string id = st + "'" + idNumber + "'" + ",Langu='" + lang + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "" + "'" + ",MobileNo='" + "" + "'" + ",SubType='" + "" + "'" + ",Idnumber='" + "" + "'" + ",Otp='" + EnteredOTP + "'" + ",Dob=datetime'" + "2019-12-21T00%3A00%3A00" + "'" + ",NewPwd='" + "" + "'" + ",RdBt='" + "P" + "')";
                        string st1 = Constants.BaseUrlOfODataServices + Constants.ForgotPasswordServiceName + "/HeaderSet(Tin=";
                        string uri = st1 + "'" + idNumber + "'" + ",Langu='" + lang + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "" + "'" + ",MobileNo='" + "" + "'" + ",SubType='" + "" + "'" + ",Idnumber='" + "" + "'" + ",Otp='" + EnteredOTP + "'" + ",Dob=datetime'" + "2019-12-21T00%3A00%3A00" + "'" + ",NewPwd='" + "'" + ",RdBt='" + "P" + "')";
                        string type = Constants.ForgotPasswordServiceName + ".Header";// "ZDP_FRGT_USRNM_PWD_SRV.Header";
                        ForgotPasswordOTP forgotPassword = new ForgotPasswordOTP();
                        Metadata metadata = new Metadata();
                        metadata.id = id;
                        metadata.uri = uri;
                        metadata.type = type;
                        D d = new D();
                      //  d.__metadata = metadata;
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
                        // d.otPasswordOTP.d.Dob = "/Date(1576886400000)/";
                        d.NewPwd = "";
                        d.CnfPwd = "";
                        d.RdBt = "P";
                        d.Hyperlink = "";
                        forgotPassword.d = d;
                        forgotPasswordOTP = await WebServiceManager.GAZTFogotPasswordSendOTP(forgotPassword);
                        await PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                        if (forgotPasswordOTP.d != null && !string.IsNullOrEmpty(forgotPasswordOTP.d.EmailId))
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                // await _dialogService.ShowMessageBox("OTP sent to registered mobile", AppResources.Information);
                                MainPageLayoutVisibility = false;
                                OTPLayoutVisibility = true;
                                ButtonDisableColor =  (Color)Application.Current.Resources["ButtonGray"];
                                VerifyButtonDisableColor =  (Color)Application.Current.Resources["Primary"];
                                IsResendOTPEnabled = false;
                                IsVerifyOTPEnabled = true;
                                IsOTPEntryEnable = true;
                                string _mobileNumber = forgotPasswordOTP.d.MobileNo.Substring(forgotPasswordOTP.d.MobileNo.Length - 4);
                                MobileNumber = "XXXXXXXXXX" + _mobileNumber;
                                numberOfSeconds = 120;
                                TimerStart(numberOfSeconds);
                            });
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessageBox(AppResources.ZPleaseEnterAValidUserID, AppResources.ZError);
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
        }
        private async Task ValidateOTP()
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    if (!string.IsNullOrEmpty(EnteredOTP))
                    {
                        currentAttempts++;
                        string idNumber = GetTinId();
                        string lang = UtilityManager.GetLanguageParameter();
                        string st = Constants.BaseUrlOfODataServices + Constants.ForgotPasswordServiceName + "/HeaderSet(Tin=";
                        string id = st + "'" + idNumber + "'" + ",Langu='" + lang + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "" + "'" + ",MobileNo='" + "" + "'" + ",SubType='" + "" + "'" + ",Idnumber='" + "" + "'" + ",Otp='" + EnteredOTP + "'" + ",Dob=datetime'" + "2019-12-21T00%3A00%3A00" + "'" + ",NewPwd='" + "" + "'" + ",RdBt='" + "P" + "')";
                        string st1 = Constants.BaseUrlOfODataServices + Constants.ForgotPasswordServiceName + "/HeaderSet(Tin=";
                        string uri = st1 + "'" + idNumber + "'" + ",Langu='" + lang + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "" + "'" + ",MobileNo='" + "" + "'" + ",SubType='" + "" + "'" + ",Idnumber='" + "" + "'" + ",Otp='" + EnteredOTP + "'" + ",Dob=datetime'" + "2019-12-21T00%3A00%3A00" + "'" + ",NewPwd='" + "'" + ",RdBt='" + "P" + "')";
                        string type = Constants.ForgotPasswordServiceName + ".Header";// "ZDP_FRGT_USRNM_PWD_SRV.Header";
                        ForgotPasswordOTP forgotPassword = new ForgotPasswordOTP();
                        Metadata metadata = new Metadata();
                        metadata.id = id;
                        metadata.uri = uri;
                        metadata.type = type;
                        D d = new D();
                        //d.__metadata = metadata;
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
                        // d.otPasswordOTP.d.Dob = "/Date(1576886400000)/";
                        d.NewPwd = "";
                        d.CnfPwd = "";
                        d.RdBt = "P";
                        d.Hyperlink = "";
                        forgotPassword.d = d;
                        forgotPassword = await WebServiceManager.GAZTForgotPasswordValidateOTP(forgotPassword);
                        await PopToRootPage();// If seesion Expired it will navigate to Dashboard page

                        if (forgotPassword != null && forgotPassword.d != null && forgotPassword.d.Action.Equals("01"))
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessageBox(AppResources.Pleasechangepassword, AppResources.Information);
                            });
                            OTPLayoutVisibility = false;
                            NewPasswordLayoutVisibility = true;
                            MobileNumber = forgotPassword.d.MobileNo;
                        }
                        else if (forgotPassword != null && forgotPassword.d != null && forgotPassword.d.Action.Equals("42"))
                        {
                            Device.BeginInvokeOnMainThread(async () =>
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
                            });
                        }
                        else
                        {
                            if (currentAttempts == 1)
                            {
                                //Invalied user name
                                Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        //await _dialogService.ShowMessageBox(AppResources.Invalidverificationcodeentered, AppResources.ZError);
                                        await _dialogService.ShowMessageBox(AppResources.ZZZWrongverificationcode, AppResources.ZError);
                                    });
                            }
                            else if (currentAttempts == 2)
                            {
                                // You have one remaining attaampt
                                Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        String message = String.Format(AppResources.ZYouhaveoneremainingattemptthentheaccountwillbelocked, "1");
                                        await _dialogService.ShowMessageBox(message, AppResources.ZError);
                                    });
                            }
                            else
                            {
                            }
                            EnteredOTP = "";
                        }
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessageBox(AppResources.EnterVerificationCode, AppResources.Information);
                        });
                    }
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
        }
        private async Task SendUserNameToRegidteredEmail()
        {
            try
            {
                string idNumber = GetTinId();
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    string lang = UtilityManager.GetLanguageParameter();
                    string st = Constants.BaseUrlOfODataServices + Constants.ForgotPasswordServiceName + "/HeaderSet(Tin=";
                    string id = st + "'" + "" + "'" + ",Langu='" + lang + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "1" + "'" + ",MobileNo='" + "" + "'" + ",SubType='" + "ZS001" + "'" + ",Idnumber='" + idNumber + "'" + ",Otp='" + "" + "'" + ",Dob=datetime'" + "2019-12-21T00%3A00%3A00" + "'" + ",NewPwd='" + "" + "'" + ",RdBt='" + "U" + "')";
                    string st1 = Constants.BaseUrlOfODataServices + Constants.ForgotPasswordServiceName + "/HeaderSet(Tin=";
                    string uri = st1 + "'" + "" + "'" + ",Langu='" + lang + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "" + "'" + ",MobileNo='" + "1" + "'" + ",SubType='" + "ZS001" + "'" + ",Idnumber='" + IDNumber + "'" + ",Otp='" + "" + "'" + ",Dob=datetime'" + "2019-12-21T00%3A00%3A00" + "'" + ",NewPwd='" + "" + "'" + ",RdBt='" + "U" + "')";
                    string type = Constants.ForgotPasswordServiceName + ".Header";// "ZDP_FRGT_USRNM_PWD_SRV.Header";
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
                    // d.otPasswordOTP.d.Dob = "/Date(1576886400000)/";
                    d.NewPwd = NewPassword;
                    d.CnfPwd = ConfirmPassword;
                    d.RdBt = "U";
                    d.Hyperlink = "";
                    forgotPassword.d = d;
                    forgotPassword = await WebServiceManager.GAZTSendUserNameToEmail(forgotPassword);
                    await PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                    if (forgotPassword.d != null && !string.IsNullOrEmpty(forgotPassword.d.EmailId))
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            //await _dialogService.ShowMessageBox(AppResources.Usernamehasbeensenttoregisteredmobilenumber, AppResources.Information);
                            // _navigationService.GoBack();
                            MainPageLayoutVisibility = false;
                            NewPasswordLayoutVisibility = false;
                            OTPLayoutVisibility = false;
                            NavigateToLoginLinkVisibility = true;
                            ForgotPasswordUserNameChangedMessage = AppResources.Usernamehasbeensenttoregisteredmobilenumber;
                        });
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessageBox(AppResources.ZPleaseEnterAValidUserID, AppResources.ZError);
                        });
                    }
                });
                await Task.Run(async () =>
                {
                    IsLoading = false;
                });
            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessageBox(ex.Message, AppResources.ZError);
                await Task.Run(async () =>
                {
                    IsLoading = false;
                });
            }
        }
        private async Task ChangePassword()
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    currentAttempts = 0;
                    bool isNewPasswordValid = UtilityManager.IsPasswordValid(NewPassword);
                    bool isConfirmPasswordValid = UtilityManager.IsPasswordValid(ConfirmPassword);
                    if (isNewPasswordValid && isConfirmPasswordValid)
                    {
                        string idNumber = GetTinId();
                        string lang = UtilityManager.GetLanguageParameter();
                        string st = Constants.BaseUrlOfODataServices + Constants.ForgotPasswordServiceName + "/HeaderSet(Tin=";
                        string id = st + "'" + idNumber + "'" + ",Langu='" + lang + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "" + "'" + ",MobileNo='" + "" + "'" + ",SubType='" + "" + "'" + ",Idnumber='" + "" + "'" + ",Otp='" + "" + "'" + ",Dob=datetime'" + "2019-12-21T00%3A00%3A00" + "'" + ",NewPwd='" + NewPassword + "'" + ",RdBt='" + "P" + "')";
                        string st1 = Constants.BaseUrlOfODataServices + Constants.ForgotPasswordServiceName + "/HeaderSet(Tin=";
                        string uri = st1 + "'" + idNumber + "'" + ",Langu='" + lang + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "" + "'" + ",MobileNo='" + "" + "'" + ",SubType='" + "" + "'" + ",Idnumber='" + "" + "'" + ",Otp='" + "" + "'" + ",Dob=datetime'" + "2019-12-21T00%3A00%3A00" + "'" + ",NewPwd='" + NewPassword + "'" + ",RdBt='" + "P" + "')";
                        string type = Constants.ForgotPasswordServiceName + ".Header";// "ZDP_FRGT_USRNM_PWD_SRV.Header";
                        ForgotPasswordOTP forgotPassword = new ForgotPasswordOTP();
                        Metadata metadata = new Metadata();
                        metadata.id = id;
                        metadata.uri = uri;
                        metadata.type = type;
                        D d = new D();
                       // d.__metadata = metadata;
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
                        // d.otPasswordOTP.d.Dob = "/Date(1576886400000)/";
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
                                // await _dialogService.ShowMessageBox(AppResources.YourPasswordhasbeenChangedsuccessfully, AppResources.Information);
                                NewPasswordLayoutVisibility = false;
                                OTPLayoutVisibility = false;
                                NavigateToLoginLinkVisibility = true;
                                ForgotPasswordUserNameChangedMessage = AppResources.ZZYourPasswordhasbeenChangedsuccessfully;
                                //  _navigationService.GoBack();
                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    await _dialogService.ShowMessageBox(AppResources.Somethingwentwrong, AppResources.Information);
                                });
                            }
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessageBox(AppResources.Boththepasswordfieldsshouldmatch, AppResources.Information);
                            });
                        }
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessageBox(AppResources.PasswordGuidelineText, AppResources.Alerts);
                        });
                    }
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessageBox(ex.Message, AppResources.Alerts);
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
        }
        public async Task PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(async () =>
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
                    _navigationService.NavigateTo(App.SFAnonymousLandingPageView);
                    _navigation.NavigationStack.ToList().Clear();
                    //var _navigation = Application.Current.MainPage.Navigation;
                    //_navigation.PopToRootAsync();
                });
            }
        }
        public async Task SetTinsListLayoutVisibility(bool IsEmailUserName)
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });
            await Task.Run(async () =>
            {
                if (IsEmailUserName)
                {
                    TINs = new List<TIN>();
                    List<TIN> Tins = new List<TIN>();
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
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    IsVisibleTinIds = false;
                                    await _dialogService.ShowMessageBox(AppResources.NoTINsAvailable, AppResources.Information);
                                });
                                //IsVisibleTinIds = false;
                            }
                            await Task.Run(() =>
                            {
                                IsLoading = false;
                            });
                        }
                        catch (Exception e)
                        {
                            IsVisibleTinIds = false;
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                IsVisibleTinIds = false;
                                await _dialogService.ShowMessageBox(AppResources.NetworkConnectivityIssue, AppResources.Information);
                            });
                            await Task.Run(() =>
                            {
                                IsLoading = false;
                            });
                        }
                    }
                    catch (InternetException ex)
                    {
                        await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                        await Task.Run(() =>
                        {
                            IsLoading = false;
                        });
                    }
                }
                else
                {
                    IsVisibleTinIds = false;
                }
            });
            await Task.Run(() =>
            {
                IsLoading = false;
            });
        }
        private string GetTinId()
        {
            string tinId = "";
            bool isEmailUser = UtilityManager.IsValidEmailAddress(IDNumber);
            if (isEmailUser)
            {
                tinId = SelectedTinId.Tin;
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
                        ButtonDisableColor =  (Color)Application.Current.Resources["Primary"];
                        IsResendOTPEnabled = true;
                        VerifyButtonDisableColor =  (Color)Application.Current.Resources["ButtonGray"];
                        IsVerifyOTPEnabled = false;
                        IsOTPEntryEnable = false;
                        return false;
                    }
                    TotalSec = TotalSec - 1;
                    numberOfSeconds = TotalSec;
                    TimeSpan _TimeSpan = TimeSpan.FromSeconds(TotalSec);
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        OTPValidDuration = " " + string.Format("{0:00}:{1:00}", _TimeSpan.Minutes, _TimeSpan.Seconds);
                    });
                    return true;
                }
            });
        }
        //private void TimerStart()
        //{
        //    CancellationTokenSource _CancellationTokenSource = new CancellationTokenSource();
        //    int TotalSec = 120;
        //    CancellationTokenSource CTS = _CancellationTokenSource;
        //    Device.StartTimer(new TimeSpan(0, 0, 1), () =>
        //    {
        //        if (CTS.IsCancellationRequested)
        //        {
        //            return false;
        //        }
        //        else
        //        {
        //            if (TotalSec == 0)
        //            {
        //                return false;
        //            }
        //            else if (!StopTimer)
        //            {
        //                return false;
        //            }
        //            Device.BeginInvokeOnMainThread(() =>
        //            {
        //                TotalSec = TotalSec - 1;
        //                TimeSpan _TimeSpan = TimeSpan.FromSeconds(TotalSec);
        //                OTPValidDuration = " " + string.Format("{0:00}:{1:00}", _TimeSpan.Minutes, _TimeSpan.Seconds);
        //            });
        //            return true;
        //        }
        //    });
        //}
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
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsVisibleTinIds = false;
                    await _dialogService.ShowMessageBox(AppResources.ZZMandatorydatanotentered, AppResources.Alerts);
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });
                });
            }
        }
        //private async Task ShowNewPasswordAndOldPassowrdNotBeSameInformation()
        //{
        //        Device.BeginInvokeOnMainThread(async () =>
        //        {
        //            await _dialogService.ShowMessageBox(AppResources.ZZThenewpasswordmustnotmatchtheexistingpassword, AppResources.Alerts);
        //            await Task.Run(() =>
        //            {
        //                IsLoading = false;
        //            });
        //        });
        //}
        //private bool IsNewPasswordSameAsOldPasswordSame()
        //{
        //    //if (NewPassword.Equals(App.TP.Password))
        //    //{
        //    //    return true;
        //    //}
        //    //else
        //    //{
        //    //    return false;
        //    //}
        //    ret
        //}

        public void ClearData()
        {
            SelectedTaxPayerType = null;
        }
        #endregion
    }
}
