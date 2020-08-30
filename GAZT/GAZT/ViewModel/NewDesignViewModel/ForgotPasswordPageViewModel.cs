
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
using System.Timers;

namespace EGAZT.ViewModel.NewDesignViewModel
{//TEST
    public class GAZTNewDesignForgotPasswordPageViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        //public ICommand OnSubmitClicked { get; set; }
        //public ICommand OnCaptchaRegenerateClicked { get; set; }
        //public ICommand OnChangePasswordSubmitClicked { get; set; }
        public Command OnResendOTPClicked { get; set; }
        public Command OnValidateOTPClicked { get; set; }
        public Command OnUserNameCardClicked { get; set; }
        public Command OnPasswordCardClicked { get; set; }
        public Command OnCorporateCardClicked { get; set; }
        public Command OnIndividualOrPersonalBusinessCardClicked { get; set; }
        public Command OnLogInClick { get; set; }
        public System.Timers.Timer otpTimer;
        public int countDownSeconds;
        //public ICommand OnLoginPageLinkClicked { get; set; }
        //public ICommand BackButtonClicked { get; set; }
        public ICommand OnContinueClick { get; set; }

        public int currentAttempts = 0;
        public int StartPage = 1;
        public bool IsPasswordCardSelected = true;
        public bool IsAPICalledSuccessfully = true;
        int TotalSec;
        public int numberOfSeconds = 120;
        ForgotPasswordOTP forgotPasswordOTP { get; set; }
        public int MaximumUserNameCharacter { get; set; } = 10;

        public bool IsUserNameCardTapped { get; set; } = false;
        public bool IsPasswordCardTapped { get; set; } = false;
        public bool StopTimer = true;
        #endregion

        #region Property

        // New Property starts
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
                if (IsUserNameCardTapped == true && IDNumber.Length > MaximumUserNameCharacter)
                {
                    IDNumber = IDNumber.Substring(0, IDNumber.Length - 1);
                }

                RaisePropertyChanged("IDNumber");
            }
        }

        private bool _isValiedEmailAddress = false;
        public bool IsValiedEmailAddress
        {
            get
            {
                return _isValiedEmailAddress;
            }
            set
            {
                _isValiedEmailAddress = value;
                RaisePropertyChanged("IsValiedEmailAddress");
            }
        }
        private bool _isTinDopDownVisible;
        public bool IsTinDopDownVisible
        {
            get
            {
                return _isTinDopDownVisible;
            }
            set
            {
                _isTinDopDownVisible = value;
                RaisePropertyChanged("IsTinDopDownVisible");
            }
        }


        private string _email;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                RaisePropertyChanged("Email");
            }
        }

        private string _userNameLabelText = AppResources.IDNumber;
        public string UserNameLabelText
        {
            get
            {
                return _userNameLabelText;
            }
            set
            {
                _userNameLabelText = value;
                RaisePropertyChanged("UserNameLabelText");
            }
        }

        private bool _Enabled;
        public bool Enabled
        {
            get { return _Enabled; }
            set
            {
                _Enabled = value;
                RaisePropertyChanged("Enabled");
            }
        }


        private bool _passwordLayoutVisibility = false;
        public bool PasswordLayoutVisibility
        {
            get
            {
                return _passwordLayoutVisibility;
            }
            set
            {
                _passwordLayoutVisibility = value;
                RaisePropertyChanged("PasswordLayoutVisibility");
            }
        }


        private bool _recoverUserNameLayout = false;
        public bool RecoverUserNameLayout
        {
            get
            {
                return _recoverUserNameLayout;
            }
            set
            {
                _recoverUserNameLayout = value;
                RaisePropertyChanged("RecoverUserNameLayout");
            }
        }

        private bool _recoverPasswordLayout = false;
        public bool RecoverPasswordLayout
        {
            get
            {
                return _recoverPasswordLayout;
            }
            set
            {
                _recoverPasswordLayout = value;
                RaisePropertyChanged("RecoverPasswordLayout");
            }
        }



        private string _LblCountDownTimer;
        public string LblCountDownTimer
        {
            get
            {
                return _LblCountDownTimer;
            }
            set
            {
                _LblCountDownTimer = value;
                RaisePropertyChanged("LblCountDownTimer");
            }
        }



        private bool _verificationCodeVisibility = false;
        public bool VerificationCodeVisibility
        {
            get
            {
                return _verificationCodeVisibility;
            }
            set
            {
                _verificationCodeVisibility = value;
                RaisePropertyChanged("VerificationCodeVisibility");
            }
        }

        private bool _userIDLayoutVisibility = true;
        public bool UserIDLayoutVisibility
        {
            get
            {
                return _userIDLayoutVisibility;
            }
            set
            {
                _userIDLayoutVisibility = value;
                RaisePropertyChanged("UserIDLayoutVisibility");
            }
        }


        private bool _defaultCardLayoutVisibility = true;
        public bool DefaultCardLayoutVisibility
        {
            get
            {
                return _defaultCardLayoutVisibility;
            }
            set
            {
                _defaultCardLayoutVisibility = value;
                RaisePropertyChanged("DefaultCardLayoutVisibility");
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
                _isLoading = value;
                RaisePropertyChanged(() => IsLoading);
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



        // * Password
        private string _MinEight;
        public string MinEight
        {
            get { return _MinEight; }
            set
            {
                _MinEight = value;
                RaisePropertyChanged("MinEight");
            }
        }

        private string _CapsSmall;
        public string CapsSmall
        {
            get { return _CapsSmall; }
            set
            {
                _CapsSmall = value;
                RaisePropertyChanged("CapsSmall");
            }
        }

        private string _MaxSixteen;
        public string MaxSixteen
        {
            get { return _MaxSixteen; }
            set
            {
                _MaxSixteen = value;
                RaisePropertyChanged("MaxSixteen");
            }
        }

        private string _NumSymbol;
        public string NumSymbol
        {
            get { return _NumSymbol; }
            set
            {
                _NumSymbol = value;
                RaisePropertyChanged("NumSymbol");
            }
        }
        // * End


        private bool _userNameLayoutVisibility = false;
        public bool UserNameLayoutVisibility
        {
            get
            {
                return _userNameLayoutVisibility;
            }
            set
            {
                _userNameLayoutVisibility = value;
                if (_userNameLayoutVisibility)
                {
                    UserIDLayoutVisibility = false;
                }
                else
                {
                    UserIDLayoutVisibility = true;
                }
                RaisePropertyChanged(() => UserNameLayoutVisibility);
            }
        }

        private bool _continueButtonEnability = true;
        public bool ContinueButtonEnability
        {
            get
            {
                return _continueButtonEnability;
            }
            set
            {
                _continueButtonEnability = value;

                RaisePropertyChanged(() => ContinueButtonEnability);
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

        private bool _setIDNumberEnability = false;
        public bool SetIDNumberEnability
        {
            get
            {
                return _setIDNumberEnability;
            }
            set
            {
                _setIDNumberEnability = value;
                RaisePropertyChanged("SetIDNumberEnability");
            }
        }





        private bool _forgotUserNameCardLayoutVisibility = false;
        public bool ForgotUserNameCardLayoutVisibility
        {
            get
            {
                return _forgotUserNameCardLayoutVisibility;
            }
            set
            {
                _forgotUserNameCardLayoutVisibility = value;
                RaisePropertyChanged("ForgotUserNameCardLayoutVisibility");
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


        private string _continueORConfirmButtonText = AppResources.ZZZZContinue;
        public string ContinueORConfirmButtonText
        {
            get
            {
                return _continueORConfirmButtonText;
            }
            set
            {
                _continueORConfirmButtonText = value;
                RaisePropertyChanged("ContinueORConfirmButtonText");
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
                // OnResendOTPClicked.ChangeCanExecute();
                RaisePropertyChanged("IsResendOTPEnabled");
            }
        }

        private string _userNameCardBackgroundImg = "FP_unselected_tile";
        public string UserNameCardBackgroundImg
        {
            get
            {
                return _userNameCardBackgroundImg;
            }
            set
            {
                _userNameCardBackgroundImg = value;
                RaisePropertyChanged("UserNameCardBackgroundImg");
            }
        }

        private string _userIcon = "vat_user";
        public string UserIcon
        {
            get
            {
                return _userIcon;
            }
            set
            {
                _userIcon = value;
                RaisePropertyChanged("UserIcon");
            }
        }

        private string _passwordCardBackgroundImg = "FP_unselected_tile";
        public string PasswordCardBackgroundImg
        {
            get
            {
                return _passwordCardBackgroundImg;
            }
            set
            {
                _passwordCardBackgroundImg = value;
                RaisePropertyChanged("PasswordCardBackgroundImg");
            }
        }

        private string _passwordIcon = "Green_Key";
        public string PasswordIcon
        {
            get
            {
                return _passwordIcon;
            }
            set
            {
                _passwordIcon = value;
                RaisePropertyChanged("PasswordIcon");
            }
        }

        private Color _userNameTextColor = Color.Black;
        public Color UserNameTextColor
        {
            get
            {
                return _userNameTextColor;
            }
            set
            {
                _userNameTextColor = value;
                RaisePropertyChanged("UserNameTextColor");
            }
        }

        private Color _passwordTextColor = Color.Black;
        public Color PasswordTextColor
        {
            get
            {
                return _passwordTextColor;
            }
            set
            {
                _passwordTextColor = value;
                RaisePropertyChanged("PasswordTextColor");
            }
        }


        private string _oTPSentOnThisMobileNumber = string.Empty;
        public string OTPSentOnThisMobileNumber
        {
            get
            {
                return _oTPSentOnThisMobileNumber;
            }
            set
            {
                _oTPSentOnThisMobileNumber = value;
                RaisePropertyChanged("OTPSentOnThisMobileNumber");
            }
        }




        // New Property Ends


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
                if (_tINs != null)
                {
                    if (_tINs.Count > 0)
                    {
                        IsTinDopDownVisible = true;
                    }
                    else
                    {
                        IsTinDopDownVisible = false;
                    }
                }
                else
                {
                    IsTinDopDownVisible = false;
                }

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
                    //App.CurrentDropdownTIN = SelectedTinId;
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
                    ButtonDisableColor = Color.FromHex("#005e4b");
                    VerifyButtonDisableColor = Color.FromHex("#9EA4A9");
                    // IsResendOTPEnabled = true;
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
        private Color _buttonDisableColor = Color.FromHex("#9EA4A9");
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
        private Color _submitDisableButtonColor = Color.FromHex("#9EA4A9");
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
        private Color _verifybuttonDisableColor = Color.FromHex("#005e4b");
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

        private string _corporateCardBackgroundImg = "FP_unselected_tile";
        public string CorporateCardBackgroundImg
        {
            get
            {
                return _corporateCardBackgroundImg;
            }
            set
            {
                _corporateCardBackgroundImg = value;
                RaisePropertyChanged(nameof(CorporateCardBackgroundImg));
            }
        }

        private string _individualOrPersonalBusinessCardBackgroundImg = "FP_unselected_tile";
        public string IndividualOrPersonalBusinessCardBackgroundImg
        {
            get
            {
                return _individualOrPersonalBusinessCardBackgroundImg;
            }
            set
            {
                _individualOrPersonalBusinessCardBackgroundImg = value;
                RaisePropertyChanged(nameof(IndividualOrPersonalBusinessCardBackgroundImg));
            }
        }

        private Color _corporateTextColor = Color.Black;
        public Color CorporateTextColor
        {
            get
            {
                return _corporateTextColor;
            }
            set
            {
                _corporateTextColor = value;
                RaisePropertyChanged(nameof(CorporateTextColor));
            }
        }

        private Color _individualOrPersonalBusinessTextColor = Color.White;
        public Color IndividualOrPersonalBusinessTextColor
        {
            get
            {
                return _individualOrPersonalBusinessTextColor;
            }
            set
            {
                _individualOrPersonalBusinessTextColor = value;
                RaisePropertyChanged(nameof(IndividualOrPersonalBusinessTextColor));
            }
        }

        #endregion
        #region Constructor
        public GAZTNewDesignForgotPasswordPageViewModel(INavigationService navigationService, IDialogService dialogService)
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

            // SetIDNumberEnability = true;


            //BackButtonClicked = new Xamarin.Forms.Command(() =>
            //{
            //    _navigationService.GoBack();
            //});
            //OnSubmitClicked = new Command(async () =>
            //{
            //    try
            //    {
            //        bool _isAllFormDataAvailable = ValidateForms();
            //        if (_isAllFormDataAvailable)
            //        {
            //            // bool isValiedCaptcha = ValidateCaptcha();
            //            if (true)
            //            {
            //                if (SelectedForgotType.id.Equals("1") && ((SelectedTaxPayerType.id.Equals("1")) || (SelectedTaxPayerType.id.Equals("2"))) && !(string.IsNullOrEmpty(IDNumber)))
            //                {
            //                    await SendUserNameToRegidteredEmail();
            //                }
            //                else
            //                {
            //                    if (!String.IsNullOrEmpty(IDNumber))
            //                    {
            //                        await SendOTPToRegisterMobileNumber();
            //                    }
            //                    else
            //                    {
            //                        _dialogService.ShowMessageBox(AppResources.PleaseenterUsername, AppResources.Information);
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                await _dialogService.ShowMessageBox(AppResources.enteredcaptchacodeisincorrect, AppResources.Information);
            //            }
            //        }
            //        else
            //        {
            //            await _dialogService.ShowMessageBox(AppResources.ZZMandatorydatanotentered, AppResources.Information);
            //        }
            //        //StringBuilder captcha = GetCaptcha();
            //        //Captcha = captcha.ToString();
            //        //EnteredCaptchaValue = string.Empty;
            //    }
            //    catch (Exception ex)
            //    {
            //    }
            //});

            OnContinueClick = new Command(() =>
            {
                if (StartPage == 2)
                {
                    SetOTP();
                }

                if (IsPasswordCardTapped == true)
                {

                    if (StartPage == 1)
                    {

                        if (UserIDLayoutVisibility)
                        {
                            if (!string.IsNullOrEmpty(IDNumber))
                            {
                                SendOTPToRegisterMobileNumber();
                            }
                            else
                            {
                                _dialogService.ShowMessageBox(AppResources.NDTypeyourIDNumber, AppResources.Information);
                            }
                        }

                        if (UserNameLayoutVisibility)
                        {
                            if (IsValiedEmailAddress)
                            {
                                if (IsAPICalledSuccessfully)
                                {
                                    if (TINs != null && TINs.Count > 0)
                                    {
                                        IDNumber = SelectedTinId.Tin;
                                        SendOTPToRegisterMobileNumber();
                                    }
                                }
                                else
                                {
                                    IsTinDopDownVisible = false;
                                }
                            }
                            else
                            {
                                IDNumber = Email;
                                if (!string.IsNullOrEmpty(Email))
                                {
                                    SendOTPToRegisterMobileNumber();
                                }
                                else
                                {
                                    _dialogService.ShowMessageBox(AppResources.NDTypeyourIDNumber, AppResources.Information);

                                }
                            }

                        }


                    }
                    else if (StartPage == 2)
                    {
                        if (!string.IsNullOrEmpty(EnteredOTP))
                        {
                            ValidateOTP();

                        }
                        else
                        {
                            _dialogService.ShowMessageBox(AppResources.Pleaseenterconfirmationcodesenttoyourmobilenumber, AppResources.Information);

                        }
                    }
                    else if (StartPage == 3)
                    {
                        if (!string.IsNullOrEmpty(NewPassword) && !string.IsNullOrEmpty(ConfirmPassword))
                        {
                            if (NewPassword.Equals(ConfirmPassword))
                            {
                                ChangePassword();
                            }
                            else
                            {
                                _dialogService.ShowMessageBox(AppResources.ZZNewpasswordandconfirmpassworddoesnotmatch, AppResources.Information);
                            }

                        }
                        else
                        {
                            _dialogService.ShowMessageBox(AppResources.NDTypeyourIDNumber, AppResources.Information);

                        }
                    }

                }
                else if (IsUserNameCardTapped == true)
                {
                    if (!string.IsNullOrEmpty(IDNumber))
                    {
                        SendUserNameToRegidteredEmail();
                    }
                    else
                    {
                        _dialogService.ShowMessageBox(AppResources.NDTypeyourIDNumber, AppResources.Information);
                    }


                }
                else if (IsUserNameCardTapped == false && IsPasswordCardTapped == false)
                {
                    _dialogService.ShowMessageBox(AppResources.ZZZZPleaseSelect, AppResources.Information);

                }
                else
                {

                }



            });




            OnCorporateCardClicked = new Command(() =>
            {
                MaximumUserNameCharacter = 60;
                CorporateCardBackgroundImg = "FP_selected_tile";
                CorporateTextColor = Color.White;
                IndividualOrPersonalBusinessCardBackgroundImg = "FP_unselected_tile";
                IndividualOrPersonalBusinessTextColor = Color.Black;
                IDNumber = string.Empty;

            });


            OnIndividualOrPersonalBusinessCardClicked = new Command(() =>
            {
                MaximumUserNameCharacter = 10;
                IndividualOrPersonalBusinessCardBackgroundImg = "FP_selected_tile";
                IndividualOrPersonalBusinessTextColor = Color.White;
                CorporateCardBackgroundImg = "FP_unselected_tile";
                CorporateTextColor = Color.Black;
                IDNumber = string.Empty;
            });

            OnLogInClick = new Command(() =>
            {
                RecoverPasswordLayout = false;
                RecoverUserNameLayout = false;
                _navigationService.GoBack();
            });



            OnResendOTPClicked = new Command(() =>
            {
                if (IsResendOTPEnabled)
                {
                    StartPage = 2;
                    SendOTPToRegisterMobileNumber();
                }

            });


            //  OnResendOTPClicked = new Command(ExecuteResendOTPClickCommand, CanExecuteResendOTPClickCommand);

        }
        #endregion Constructor
        #region Method


        public void SetPasswordCardLayoutVisibility()
        {
            IsPasswordCardSelected = true;
            ForgotUserNameCardLayoutVisibility = false;
            UserNameLayoutVisibility = true;

        }

        public void SetUserNameCardVisibility()
        {
            IsPasswordCardSelected = false;
            ForgotUserNameCardLayoutVisibility = true;
            UserNameLayoutVisibility = false;
        }
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
        public void OnPageLoad()
        {
            PasswordTextColor = Color.Black;
            UserNameTextColor = Color.Black;

            //IDNumberOrCorporateIDOrUserName = AppResources.IDNumber;
            //string lang = UtilityManager.GetLanguageParameter();
            //try
            //{
            //    StringBuilder captcha = GetCaptcha();
            //    Captcha = captcha.ToString();
            //    List<ForgotUserNamePassword> list = new List<ForgotUserNamePassword>
            //{
            //    new ForgotUserNamePassword{ id = "1" , TaxPayerType = AppResources.Individual},
            //    new ForgotUserNamePassword{ id = "2" , TaxPayerType = AppResources.Company}
            //};
            //    TaxpayerTypeList = list;
            //    List<ForgotCredentialType> forgotCredentialListlist = new List<ForgotCredentialType>
            //{
            //    new ForgotCredentialType{ id = "1" , CredentialType = AppResources.ForgotUsername},
            //    new ForgotCredentialType{ id = "2" , CredentialType = AppResources.ForgotPassword}
            //};
            //    ForgotTypeList = forgotCredentialListlist;
            //    if (ForgotTypeList != null && ForgotTypeList.Count != 0)
            //    {
            //        SelectedForgotType = ForgotTypeList.Where(x => x.id == "2").FirstOrDefault();
            //        ForgotTypeIndex = 1;
            //    }
            //}
            //catch (Exception ex)
            //{
            //}
            //VerifyButtonDisableColor = Color.FromHex("#005e4b");
            //IsVerifyOTPEnabled = true;
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
                    Enabled = false;
                });
                await Task.Run(async () =>
                {
                    try
                    {
                        string idNumber = GetTinId();
                        string lang = UtilityManager.GetLanguageParameter();
                        forgotPasswordOTP = await WebServiceManager.GAZTFogotPasswordSendOTP(lang, idNumber);
                        await PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                        if (forgotPasswordOTP.d != null && !string.IsNullOrEmpty(forgotPasswordOTP.d.EmailId))
                        {
                            ContinueButtonEnability = true;
                            IsResendOTPEnabled = false;
                            StartOTPTimer();
                            IsAPICalledSuccessfully = true;

                            //StartPage = StartPage + 1;
                            StartPage = 2;

                            DefaultCardLayoutVisibility = false;
                            UserIDLayoutVisibility = false;
                            VerificationCodeVisibility = true;
                            string _mobileNumber = forgotPasswordOTP.d.MobileNo.Substring(forgotPasswordOTP.d.MobileNo.Length - 4);
                            MobileNumber = "XXXXXXXXXX" + _mobileNumber;
                            OTPSentOnThisMobileNumber = AppResources.MobileNumber + " " + MobileNumber;



                            //        MobileNumber = "XXXXXXXXXX" + _mobileNumber;

                            //    Device.BeginInvokeOnMainThread(() =>
                            //    {
                            //        // await _dialogService.ShowMessageBox("OTP sent to registered mobile", AppResources.Information);
                            //        MainPageLayoutVisibility = false;
                            //        OTPLayoutVisibility = true;
                            //        ButtonDisableColor = Color.FromHex("#9EA4A9");
                            //        VerifyButtonDisableColor = Color.FromHex("#005e4b");
                            //        IsResendOTPEnabled = false;
                            //        IsVerifyOTPEnabled = true;
                            //        IsOTPEntryEnable = true;
                            //        string _mobileNumber = forgotPasswordOTP.d.MobileNo.Substring(forgotPasswordOTP.d.MobileNo.Length - 4);
                            //        MobileNumber = "XXXXXXXXXX" + _mobileNumber;
                            //        numberOfSeconds = 120;
                            //        TimerStart(numberOfSeconds);
                            //    });
                            //}
                            //else
                            //{
                            //    Device.BeginInvokeOnMainThread(async () =>
                            //    {
                            //        await _dialogService.ShowMessageBox(AppResources.ZPleaseEnterAValidUserID, AppResources.ZError);
                            //    });
                        }
                        else
                        {
                            IsAPICalledSuccessfully = false;
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessageBox(AppResources.NDEntervaliduserid, AppResources.ZError);

                                SetIDNumberEnability = true;
                                IDNumber = String.Empty;
                                UserIDLayoutVisibility = true;
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

                    SetIDNumberEnability = true;
                    IDNumber = String.Empty;
                    UserIDLayoutVisibility = true;
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
                        d.__metadata = metadata;
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
                            StartPage = StartPage + 1;
                            VerificationCodeVisibility = false;
                            PasswordLayoutVisibility = true;

                            IsAPICalledSuccessfully = true;

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
                            IsAPICalledSuccessfully = true;

                            StartPage = StartPage + 1;
                            VerificationCodeVisibility = false;
                            PasswordLayoutVisibility = true;

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
                                    await _dialogService.ShowMessageBox(AppResources.ZZZZWrongVerificationCode, AppResources.ZError);
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
                        IsAPICalledSuccessfully = false;

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
                    d.__metadata = metadata;
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
                        //IsAPICalledSuccessfully = true;
                        //RecoverUserNameLayout = true;
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            _navigationService.NavigateTo(App.GAZTNewDesignRecoverUsername);
                            //await _dialogService.ShowMessageBox(AppResources.Usernamehasbeensenttoregisteredmobilenumber, AppResources.Information);
                            // _navigationService.GoBack();
                            //MainPageLayoutVisibility = false;
                            //NewPasswordLayoutVisibility = false;
                            //OTPLayoutVisibility = false;
                            //NavigateToLoginLinkVisibility = true;
                            //ForgotPasswordUserNameChangedMessage = AppResources.Usernamehasbeensenttoregisteredmobilenumber;
                        });
                    }
                    else
                    {
                        IsAPICalledSuccessfully = false;
                        IDNumber = string.Empty;
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessageBox(AppResources.NDEntervaliduserid, AppResources.ZError);
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
                await Task.Run(async () =>
                {
                    currentAttempts = 0;

                    bool isNewPasswordValid = UtilityManager.IsPasswordValid(NewPassword);
                    //bool isConfirmPasswordValid = UtilityManager.IsPasswordValid(ConfirmPassword);

                    if (isNewPasswordValid)
                    {
                        await Task.Run(() =>
                        {
                            IsLoading = true;
                        });

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
                        d.__metadata = metadata;
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
                                StartPage = StartPage + 1;
                                //  RecoverPasswordLayout = true;
                                // await _dialogService.ShowMessageBox(AppResources.YourPasswordhasbeenChangedsuccessfully, AppResources.Information);
                                NewPasswordLayoutVisibility = false;
                                OTPLayoutVisibility = false;
                                //  NavigateToLoginLinkVisibility = true;
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    _navigationService.NavigateTo(App.GAZTNewDesignRecoverPasswordPageView);
                                });

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
                        if (item.GetType().Name == App.SFLoginPageView)
                        {
                            _navigation.RemovePage(item);
                            break;
                        }
                    }
                    //_navigationService.NavigateTo(App.SFLoginPageView);
                    //_navigation.NavigationStack.ToList().Clear();

                    _navigationService.NavigateTo(App.SFLoginPageView, App.GAZTNewDesignDashBoardPageView);
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
                            await Task.Run(() =>
                            {
                                IsLoading = true;
                            });
                            SelectedTinId = null;
                            Tins = await WebServiceManager.GAZTGetAllTins(Email);
                            TINs = Tins;
                            if (Tins.Count != 0 && SelectedTinId == null)
                            {
                                IsAPICalledSuccessfully = true;
                                UserNameLabelText = AppResources.UserName;
                                IsVisibleTinIds = true;
                                //SelectedTinId = TINs[0];
                                SelectedTinId = TINs.FirstOrDefault();
                            }
                            else
                            {
                                IsAPICalledSuccessfully = false;

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
                            IsAPICalledSuccessfully = false;

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
                        ButtonDisableColor = Color.FromHex("#005e4b");
                        IsResendOTPEnabled = true;
                        VerifyButtonDisableColor = Color.FromHex("#9EA4A9");
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

            PasswordLayoutVisibility = false;
            RecoverUserNameLayout = false;
            RecoverPasswordLayout = false;
            VerificationCodeVisibility = false;
            ForgotUserNameCardLayoutVisibility = false;
            UserIDLayoutVisibility = true;
            DefaultCardLayoutVisibility = true;
            UserNameLayoutVisibility = false;
            IDNumber = string.Empty;


            OTPFirstDigit = string.Empty;
            OTPSecondDigit = string.Empty;
            OTPThirdDigit = string.Empty;
            OTPFourthDigit = string.Empty;


            IsAPICalledSuccessfully = false;
            SetIDNumberEnability = false;
            PasswordCardBackgroundImg = "FP_selected_tile";
            PasswordIcon = "password_key";
            UserNameCardBackgroundImg = "FP_unselected_tile";
            UserIcon = "vat_user";
            Email = string.Empty;
            TxtTIN = string.Empty;
            if (TINs != null && TINs.Count > 0)
                TINs.Clear();

            NewPassword = string.Empty;
            ConfirmPassword = string.Empty;

        }


        public void SetOTP()
        {
            //if (App.IsArabic)
            //{
            //    EnteredOTP = OTPFourthDigit + OTPThirdDigit + OTPSecondDigit + OTPFirstDigit;
            //}
            //else
            //{
            EnteredOTP = OTPFirstDigit + OTPSecondDigit + OTPThirdDigit + OTPFourthDigit;
            //}
        }


        private void OnCountDownTimedOTPEvent(object sender, ElapsedEventArgs e)
        {
            countDownSeconds--;

            /*if (countDownSeconds <= 9)
                LblCountDownTimer = "0:0" + countDownSeconds.ToString();
            else
                LblCountDownTimer = "0:" + countDownSeconds.ToString();*/


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
            if (countDownSeconds == 0)
            {
                ContinueButtonEnability = false;
                IsResendOTPEnabled = true;

                otpTimer.Stop();
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
            LblCountDownTimer = "0." + countDownSeconds.ToString();

            otpTimer.Enabled = true;
        }

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
        #endregion


    }
}