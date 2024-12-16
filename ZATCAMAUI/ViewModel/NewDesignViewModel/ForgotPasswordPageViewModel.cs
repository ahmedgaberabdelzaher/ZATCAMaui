using System.Windows.Input;
using System.Timers;
using ZATCAMAUI.Models;
using Mopups.Services;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Views.NewDesign.ForgotPasswordPages;
using Newtonsoft.Json.Linq;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Models.ForgotModel;
using ZATCAMAUI.Models.SignUP;
using ZATCAMAUI.Core.Services.Interfac;
using System;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel
{

    public class GAZTNewDesignForgotPasswordPageViewModel : BaseViewModel

    {
        #region Variable
        public readonly IZatacaAPIHAndle _zatacaAPIHAndle;
        private string captcha = string.Empty;
        private string GUID = string.Empty;
        public Command OnResendOTPClicked { get; set; }
        public Command OnValidateOTPClicked { get; set; }
        public Command OnUserNameCardClicked { get; set; }
        public Command OnPasswordCardClicked { get; set; }
        public Command OnCorporateCardClicked { get; set; }
        public Command VerifyOTPCommand { get; set; }
        public Command OnIndividualOrPersonalBusinessCardClicked { get; set; }
        public Command OnLogInClick { get; set; }
        public System.Timers.Timer otpTimer;
        public int countDownSeconds;
        public ICommand OnContinueClick { get; set; }

        public int currentAttempts = 0;

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
        private int _StartPage = 1;
        public int StartPage
        {
            get
            {
                return _StartPage;
            }
            set
            {


                _StartPage = value;
                if (_StartPage == 2)
                {
                    IsContinueButtonVisibe = false;
                    ForgotPwHeadingVisibility = false;

                }
                else if (_StartPage == 3)
                {
                    IsContinueButtonVisibe = true;
                    ForgotPwHeadingVisibility = false;
                }
                else if (_StartPage == 1)
                {
                    IsContinueButtonVisibe = ValidateFirstStep();
                    ForgotPwHeadingVisibility = true;
                }
                else
                {
                    IsContinueButtonVisibe = true;
                    ForgotPwHeadingVisibility = true;
                }
                OnPropertyChanged("StartPage");
            }
        }
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
                if (_iDNumber == value) return;
                _iDNumber = value;
                if (IsUserNameCardTapped == true && IDNumber.Length > MaximumUserNameCharacter)
                {
                    IDNumber = IDNumber.Substring(0, IDNumber.Length - 1);
                }

                if (CorporateCardBackgroundImg.Equals("FP_selected_tile") && !string.IsNullOrEmpty(IDNumber))
                {
                    try
                    {
                        if (IDNumber.Length > 0)
                        {
                            string firstlettorOfIdNumber = IDNumber.Substring(0, 1);
                            if (!firstlettorOfIdNumber.Equals("7"))
                            {
                                IDNumber = string.Empty;
                                MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NDCorporateIDMustStartWithSeven));

                            }

                        }
                    }
                    catch (Exception)
                    {
                    }



                }
                OnPropertyChanged("IDNumber");
            }
        }
        private bool _isContinueButtonVisibe = true;
        public bool IsContinueButtonVisibe
        {
            get
            {
                return _isContinueButtonVisibe;
            }
            set
            {


                _isContinueButtonVisibe = value;
                OnPropertyChanged("IsContinueButtonVisibe");
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
                if (_isValiedEmailAddress == value) return;

                _isValiedEmailAddress = value;
                OnPropertyChanged("IsValiedEmailAddress");
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
                if (_isTinDopDownVisible == value) return;

                _isTinDopDownVisible = value;
                OnPropertyChanged("IsTinDopDownVisible");
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
                if (_email == value) return;

                _email = value;
                OnPropertyChanged("Email");
            }
        }

        private string _userNameLabelText = AppResources.UserName;
        public string UserNameLabelText
        {
            get
            {
                return _userNameLabelText;
            }
            set
            {
                if (_userNameLabelText == value) return;

                _userNameLabelText = value;
                OnPropertyChanged("UserNameLabelText");
            }
        }

        private bool _Enabled;
        public bool Enabled
        {
            get { return _Enabled; }
            set
            {
                if (_Enabled == value) return;

                _Enabled = value;
                OnPropertyChanged("Enabled");
            }
        }

        private Color _resendOTPTextColor = (Color)Application.Current.Resources["DarkGrayTextColor"];
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
                OnPropertyChanged("ResendOTPTextColor");
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
                if (_passwordLayoutVisibility == value) return;

                _passwordLayoutVisibility = value;
                OnPropertyChanged("PasswordLayoutVisibility");
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
                if (_recoverUserNameLayout == value) return;

                _recoverUserNameLayout = value;
                OnPropertyChanged("RecoverUserNameLayout");
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
                if (_recoverPasswordLayout == value) return;

                _recoverPasswordLayout = value;
                OnPropertyChanged("RecoverPasswordLayout");
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
                if (_LblCountDownTimer == value) return;

                _LblCountDownTimer = value;
                OnPropertyChanged("LblCountDownTimer");
            }
        }

        private bool _ForgotPwHeadingVisibility = true;
        public bool ForgotPwHeadingVisibility
        {
            get
            {
                return _ForgotPwHeadingVisibility;
            }
            set
            {
                if (_ForgotPwHeadingVisibility == value) return;

                _ForgotPwHeadingVisibility = value;
                OnPropertyChanged("ForgotPwHeadingVisibility");
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
                if (_verificationCodeVisibility == value) return;

                _verificationCodeVisibility = value;
                if (_verificationCodeVisibility)
                {
                    ForgotPwHeadingVisibility = false;
                }
                else
                {
                    ForgotPwHeadingVisibility = false;
                }
                OnPropertyChanged("VerificationCodeVisibility");
            }
        }

        private bool _userIDLayoutVisibility = false;
        public bool UserIDLayoutVisibility
        {
            get
            {
                return _userIDLayoutVisibility;
            }
            set
            {
                if (_userIDLayoutVisibility == value) return;

                _userIDLayoutVisibility = value;
                OnPropertyChanged("UserIDLayoutVisibility");
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
                if (_defaultCardLayoutVisibility == value) return;

                _defaultCardLayoutVisibility = value;
                OnPropertyChanged("DefaultCardLayoutVisibility");
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
                if (_oTPFirstDigit == value) return;

                _oTPFirstDigit = value;
                if (!string.IsNullOrEmpty(OTPFirstDigit))
                {
                    bool isNumberEntered = CheckOnlyNumber(OTPFirstDigit[0]);
                    if (!isNumberEntered)
                    {
                        OTPFirstDigit = string.Empty;
                    }
                }

                OnPropertyChanged("OTPFirstDigit");
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
                if (_OTPSecondDigit == value) return;

                _OTPSecondDigit = value;
                if (!string.IsNullOrEmpty(OTPSecondDigit))
                {
                    bool isNumberEntered = CheckOnlyNumber(OTPSecondDigit[0]);
                    if (!isNumberEntered)
                    {
                        OTPSecondDigit = string.Empty;
                    }
                }
                OnPropertyChanged("OTPSecondDigit");
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
                if (_OTPThirdDigit == value) return;

                _OTPThirdDigit = value;
                if (!string.IsNullOrEmpty(OTPThirdDigit))
                {
                    bool isNumberEntered = CheckOnlyNumber(OTPThirdDigit[0]);
                    if (!isNumberEntered)
                    {
                        OTPThirdDigit = string.Empty;
                    }
                }
                OnPropertyChanged("OTPThirdDigit");
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
                if (_OTPFourthDigit == value) return;

                _OTPFourthDigit = value;
                if (!string.IsNullOrEmpty(OTPFourthDigit))
                {
                    bool isNumberEntered = CheckOnlyNumber(OTPFourthDigit[0]);
                    if (!isNumberEntered)
                    {
                        OTPFourthDigit = string.Empty;
                    }
                }
                OnPropertyChanged("OTPFourthDigit");
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
                if (_MinEight == value) return;

                _MinEight = value;
                OnPropertyChanged("MinEight");
            }
        }

        private string _CapsSmall;
        public string CapsSmall
        {
            get { return _CapsSmall; }
            set
            {
                if (_CapsSmall == value) return;

                _CapsSmall = value;
                OnPropertyChanged("CapsSmall");
            }
        }

        private string _MaxSixteen;
        public string MaxSixteen
        {
            get { return _MaxSixteen; }
            set
            {
                if (_MaxSixteen == value) return;

                _MaxSixteen = value;
                OnPropertyChanged("MaxSixteen");
            }
        }

        private string _NumSymbol;
        public string NumSymbol
        {
            get { return _NumSymbol; }
            set
            {
                if (_NumSymbol == value) return;

                _NumSymbol = value;
                OnPropertyChanged("NumSymbol");
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
                if (_userNameLayoutVisibility == value) return;

                _userNameLayoutVisibility = value;
                OnPropertyChanged(nameof(UserNameLayoutVisibility));
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
                if (_continueButtonEnability == value) return;

                _continueButtonEnability = value;

                OnPropertyChanged(nameof(ContinueButtonEnability));
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
                if (_newPassword == value) return;

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
                if (_confirmPassword == value) return;

                _confirmPassword = value;
                OnPropertyChanged("ConfirmPassword");
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
                if (_setIDNumberEnability == value) return;

                _setIDNumberEnability = value;
                OnPropertyChanged("SetIDNumberEnability");
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
                if (_forgotUserNameCardLayoutVisibility == value) return;

                _forgotUserNameCardLayoutVisibility = value;
                OnPropertyChanged("ForgotUserNameCardLayoutVisibility");
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
                if (_enteredOTP == value) return;

                _enteredOTP = value;
                OnPropertyChanged("EnteredOTP");
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
                if (_continueORConfirmButtonText == value) return;

                _continueORConfirmButtonText = value;
                OnPropertyChanged("ContinueORConfirmButtonText");
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
                // OnResendOTPClicked.ChangeCanExecute();
                OnPropertyChanged("IsResendOTPEnabled");
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
                if (_userNameCardBackgroundImg == value) return;

                _userNameCardBackgroundImg = value;
                OnPropertyChanged("UserNameCardBackgroundImg");
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
                if (_userIcon == value) return;

                _userIcon = value;
                OnPropertyChanged("UserIcon");
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
                if (_passwordCardBackgroundImg == value) return;

                _passwordCardBackgroundImg = value;
                OnPropertyChanged("PasswordCardBackgroundImg");
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
                if (_passwordIcon == value) return;

                _passwordIcon = value;
                OnPropertyChanged("PasswordIcon");
            }
        }

        private Color _userNameTextColor = (Color)Application.Current.Resources["Primary"];
        public Color UserNameTextColor
        {
            get
            {
                return _userNameTextColor;
            }
            set
            {
                if (_userNameTextColor == value) return;

                _userNameTextColor = value;
                OnPropertyChanged("UserNameTextColor");
            }
        }

        private Color _passwordTextColor = (Color)Application.Current.Resources["Primary"];
        public Color PasswordTextColor
        {
            get
            {
                return _passwordTextColor;
            }
            set
            {
                if (_passwordTextColor == value) return;

                _passwordTextColor = value;
                OnPropertyChanged("PasswordTextColor");
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
                if (_oTPSentOnThisMobileNumber == value) return;

                _oTPSentOnThisMobileNumber = value;
                OnPropertyChanged("OTPSentOnThisMobileNumber");
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
                if (_isOTPEntryEnable == value) return;

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
                if (_selectedTaxPayerType == value) return;

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
                if (_selectedTaxPayerTypePrev == value) return;

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
                if (_forgotTypeList == value) return;

                _forgotTypeList = value;
                OnPropertyChanged("ForgotTypeList");
            }
        }
        private bool _isDropImageVisible;
        public bool IsDropImageVisible
        {
            get => _isDropImageVisible;
            set
            {
                _isDropImageVisible = value;
                OnPropertyChanged("IsDropImageVisible");
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
                if (_tINs == value) return;

                _tINs = value;
                if (_tINs != null)
                {
                    if (_tINs.Count > 0)
                    {
                        IsTinDopDownVisible = true;
                    }
                    if (_tINs.Count > 1)
                    {
                        IsDropImageVisible = true;
                    }
                    else
                    {
                        IsDropImageVisible = false;
                        IsTinDopDownVisible = false;
                    }
                }
                else
                {
                    IsTinDopDownVisible = false;
                }

                OnPropertyChanged("TINs");
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
                if (_txtSelectedUsernameAndPassword == value) return;

                _txtSelectedUsernameAndPassword = value;
                OnPropertyChanged("TxtSelectedUsernameAndPassword");
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
                if (_txtSelectTaxpayerType == value) return;

                _txtSelectTaxpayerType = value;
                OnPropertyChanged("TxtSelectTaxpayerType");
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
                if (_txtTIN == value) return;

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
                if (_selectedTinId == value) return;

                _selectedTinId = value;
                if (_selectedTinId != null)
                {
                    TxtTIN = _selectedTinId.TIN;
                    //App.CurrentDropdownTIN = SelectedTinId;
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
                if (_selectedTinIdPrev == value) return;

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
                if (_isVisibleTinIds == value) return;

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
                if (_selectedForgotType == value) return;

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
                if (_selectedForgotTypePrev == value) return;

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
                if (_forgotCredentialType == value) return;

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
                if (_taxpayerTypeList == value) return;

                _taxpayerTypeList = value;
                OnPropertyChanged("TaxpayerTypeList");
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
                if (_enteredCaptchaValue == value) return;

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
                if (_corporateID == value) return;

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
                if (_mobileNumber == value) return;

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
                if (_oTPValidDuration == value) return;

                _oTPValidDuration = value;
                if (_oTPValidDuration.Equals(" 00:00"))
                {
                    ButtonDisableColor = (Color)Application.Current.Resources["Primary"];
                    VerifyButtonDisableColor = (Color)Application.Current.Resources["ButtonGray"];

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
                if (_userName == value) return;

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
                if (_isForgotUserNameWithIndividual == value) return;

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
                if (_isForgotUserNameWithCorporate == value) return;

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
                if (_isForgotPassword == value) return;

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
                if (_isTaxPayerTypeEnable == value) return;

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
                if (_newPasswordLayoutVisibility == value) return;

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
                if (_mainPageLayoutVisibility == value) return;

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
                if (_oTPLayoutVisibility == value) return;

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
                if (_navigateToLoginLinkVisibility == value) return;

                _navigateToLoginLinkVisibility = value;
                OnPropertyChanged("NavigateToLoginLinkVisibility");
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
                if (_iDNumberOrCorporateIDOrUserName == value) return;

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
                if (_captcha == value) return;

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
                if (_forgotPasswordUserNameChangedMessage == value) return;

                _forgotPasswordUserNameChangedMessage = value;
                OnPropertyChanged("ForgotPasswordUserNameChangedMessage");
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
                if (_isIDTypeVisible == value) return;

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
                if (_newPasswordVisibility == value) return;

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
                if (_buttonDisableColor == value) return;

                _buttonDisableColor = value;
                OnPropertyChanged("ButtonDisableColor");
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
                if (_confirmPasswordVisibility == value) return;

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
                if (_isSubmitEnabled == value) return;

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
                if (_submitDisableButtonColor == value) return;

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
                if (_verifybuttonDisableColor == value) return;

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
                if (_isVerifyOTPEnabled == value) return;

                _isVerifyOTPEnabled = value;
                OnValidateOTPClicked.ChangeCanExecute();
                OnPropertyChanged("IsVerifyOTPEnabled");
            }
        }
        private int _maxChar = 10;
        public int MaxChar
        {
            get
            {
                return _maxChar;
            }
            set
            {
                if (_maxChar == value) return;

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
                if (_forgotTypeIndex == value) return;

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
                if (_selectedTaxPayerTypeIndex == value) return;

                _selectedTaxPayerTypeIndex = value;
                OnPropertyChanged("SelectedTaxPayerTypeIndex");
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
                if (_corporateCardBackgroundImg == value) return;

                _corporateCardBackgroundImg = value;
                OnPropertyChanged(nameof(CorporateCardBackgroundImg));
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
                if (_individualOrPersonalBusinessCardBackgroundImg == value) return;

                _individualOrPersonalBusinessCardBackgroundImg = value;
                OnPropertyChanged(nameof(IndividualOrPersonalBusinessCardBackgroundImg));
            }
        }

        private Color _corporateTextColor = (Color)Application.Current.Resources["Primary"];
        public Color CorporateTextColor
        {
            get
            {
                return _corporateTextColor;
            }
            set
            {
                if (_corporateTextColor == value) return;

                _corporateTextColor = value;
                OnPropertyChanged(nameof(CorporateTextColor));
            }
        }

        private Color _individualOrPersonalBusinessTextColor = Colors.White;
        public Color IndividualOrPersonalBusinessTextColor
        {
            get
            {
                return _individualOrPersonalBusinessTextColor;
            }
            set
            {
                if (_individualOrPersonalBusinessTextColor == value) return;

                _individualOrPersonalBusinessTextColor = value;
                OnPropertyChanged(nameof(IndividualOrPersonalBusinessTextColor));
            }
        }
        private string imageBase64;
        public string ImageBase64
        {
            get { return imageBase64; }
            set
            {
                imageBase64 = value;
                OnPropertyChanged("ImageBase64");

                Image = ImageSource.FromStream(
                    () => new MemoryStream(Convert.FromBase64String(imageBase64)));
            }
        }

        private ImageSource image;
        public ImageSource Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }
        #endregion
        #region Constructor
        public GAZTNewDesignForgotPasswordPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {


            OnContinueClick = new Command(async () =>
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
                                await SendOTPToRegisterMobileNumber();
                            }
                            else
                            {
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NDTypeyourIDNumber));
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
                                        IDNumber = SelectedTinId.TIN;
                                        await SendOTPToRegisterMobileNumber();
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
                                if (!string.IsNullOrEmpty(Email)) //Forgot Password
                                {
                                    if (!string.IsNullOrEmpty(Captcha))
                                    {
                                        await SendOTPToRegisterMobileNumber();
                                    }
                                    else
                                    {
                                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.EnterCaptchaMsg));
                                    }
                                }
                                else
                                {
                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.TypeYourUserName));
                                }
                            }

                        }


                    }
                    else if (StartPage == 2)
                    {
                        if (!string.IsNullOrEmpty(EnteredOTP))
                        {
                            await ValidateOTP();

                        }
                        else
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Pleaseenterconfirmationcodesenttoyourmobilenumber));
                        }
                    }
                    else if (StartPage == 3)
                    {
                        if (!string.IsNullOrEmpty(NewPassword) && !string.IsNullOrEmpty(ConfirmPassword))
                        {
                            if (NewPassword.Equals(ConfirmPassword))
                            {
                                await ChangePassword();
                            }
                            else
                            {
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZNewpasswordandconfirmpassworddoesnotmatch));
                            }

                        }
                        else
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PasswordValidationMesseg));
                        }
                    }

                }
                else if (IsUserNameCardTapped == true)
                {
                    if (!string.IsNullOrEmpty(IDNumber))
                    {
                        if (!string.IsNullOrEmpty(Captcha))
                        {
                            await SendIDNumberToUsernameEmail();
                        }
                        else
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.EnterCaptchaMsg));
                        }
                    }
                    else
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NDTypeyourIDNumber));
                    }
                }
                else if (IsUserNameCardTapped == false && IsPasswordCardTapped == false)
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZZZPleaseSelect));

                }



            });

            OnCorporateCardClicked = new Command(() =>
            {
                MaximumUserNameCharacter = 10;
                CorporateCardBackgroundImg = "FP_selected_tile";
                CorporateTextColor = Colors.White;
                IndividualOrPersonalBusinessCardBackgroundImg = "FP_unselected_tile";
                IndividualOrPersonalBusinessTextColor = (Color)Application.Current.Resources["Primary"];
                IDNumber = string.Empty;

            });

            VerifyOTPCommand = new Command(async() =>
            {
                IsLoading = true;
                await OtpFilled();
                IsLoading = false;
            });


            OnIndividualOrPersonalBusinessCardClicked = new Command(() =>
            {
                MaximumUserNameCharacter = 10;
                IndividualOrPersonalBusinessCardBackgroundImg = "FP_selected_tile";
                IndividualOrPersonalBusinessTextColor = Colors.White;
                CorporateCardBackgroundImg = "FP_unselected_tile";
                CorporateTextColor = (Color)Application.Current.Resources["Primary"];
                IDNumber = string.Empty;
            });

            OnLogInClick = new Command(() =>
            {
                RecoverPasswordLayout = false;
                RecoverUserNameLayout = false;
                _navigationService.GoBack();
            });



            OnResendOTPClicked = new Command(async () =>
            {
                if (IsResendOTPEnabled)
                {
                    StartPage = 2;
                    await SendOTPToRegisterMobileNumber();
                }

            });

        }
        #endregion Constructor
        #region Method


        public void SetPasswordCardLayoutVisibility()
        {
            IsPasswordCardSelected = true;
            ForgotUserNameCardLayoutVisibility = false;
            UserNameLayoutVisibility = true;

        }
        public async Task OtpFilled()
        {
            if (StartPage == 2)
            {
                SetOTP();
            }
            if (!string.IsNullOrEmpty(EnteredOTP))
            {
                await ValidateOTP();

            }
            else
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Pleaseenterconfirmationcodesenttoyourmobilenumber));
            }
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
        public async Task ExecuteSubmitClickCommand(object obj)
        {
            await ValidateOTP();
        }
        bool CanExecuteResendOTPClickCommand(object arg)
        {
            return _isResendOTPEnabled;
        }
        public async Task ExecuteResendOTPClickCommand(object obj)
        {
            await SendOTPToRegisterMobileNumber();
        }
        public void OnPageLoad()
        {
            PasswordTextColor = Colors.Black;
            UserNameTextColor = Colors.Black;

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
                if (!(SelectedForgotType.id.Equals("2")))
                {
                    IDNumberOrCorporateIDOrUserName = AppResources.IDNumber;
                }

                MaxChar = 10;
            }
            else
            {
                IDNumberOrCorporateIDOrUserName = AppResources.CorportaeID;
                MaxChar = 10;
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
        public async Task GetCaptchImage(string ApplicationCode)
        {
            try
            {
                IsLoading = true;
                ImageCaptchaModel result = await WebServiceManager.GetCaptchaImage(ApplicationCode, GUID);
                PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                if (result != null)
                {
                    ImageBase64 = result.data.cval;
                    GUID = result.data.GUID;
                    IsAPICalledSuccessfully = true;
                    Captcha = string.Empty;
                }
                IsLoading = false;
            }
            catch (GAZTErrorException ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                IsLoading = false;
                SetIDNumberEnability = true;
                IDNumber = String.Empty;
            }

            catch (InternetException ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                IsLoading = false;

                SetIDNumberEnability = true;
                IDNumber = String.Empty;
            }
        }
        private async Task SendOTPToRegisterMobileNumber()
        {
            try
            {
                IsLoading = true;
                Enabled = false;
                try
                {
                    string idNumber = GetTinId();
                    string lang = UtilityManager.GetLanguageParameter();
                    string st = ZATCAConstants.BaseUrlOfODataServices + ZATCAConstants.ForgotPasswordServiceName + "/SecuredHeaderSet(Tin=";
                    string id = st + "'" + "" + "'" + ",Langu='" + lang + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "1" + "'" + ",MobileNo='" + "" + "'" + ",SubType='" + "" + "'" + ",Idnumber='" + "" + "'" + ",Otp='" + "" + "'" + ",Dob=datetime'" + "2019-12-21T00%3A00%3A00" + "'" + ",NewPwd='" + "" + "'" + ",RdBt='" + "P" + "')";
                    string st1 = ZATCAConstants.BaseUrlOfODataServices + ZATCAConstants.ForgotPasswordServiceName + "/SecuredHeaderSet(Tin=";
                    string uri = st1 + "'" + "" + "'" + ",Langu='" + lang + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "" + "'" + ",MobileNo='" + "" + "'" + ",SubType='" + "" + "'" + ",Idnumber='" + "" + "'" + ",Otp='" + "" + "'" + ",Dob=datetime'" + "2019-12-21T00%3A00%3A00" + "'" + ",NewPwd='" + "" + "'" + ",RdBt='" + "P" + "')";
                    string type = ZATCAConstants.ForgotPasswordServiceName + ".Header";// "ZDP_FRGT_USRNM_PWD_SRV.Header";
                    ForgotPasswordOTP forgotPasswordOTP = new ForgotPasswordOTP();
                    Metadata metadata = new Metadata();
                    metadata.id = id;
                    metadata.uri = uri;
                    metadata.type = type;
                    D d = new D();
                    //d.__metadata =  metadata;
                    d.Action = "";
                    d.Tin = idNumber;
                    d.Langu = UtilityManager.GetLanguageParameter();
                    d.EmailId = "";
                    d.SubType = "";
                    d.Idnumber = "";
                    d.RdBt = "P";
                    d.TpType = "";
                    d.MobileNo = "";
                    // d.Refresh = "";
                    d.Hyperlink = "";
                    //d.Taxpayer = "";
                    d.Name = "";
                    d.Otp = "";
                    d.NewPwd = "";
                    d.CnfPwd = "";
                    d.Application = "FPWD";
                    d.Captcha = Captcha;
                    d.Guid = GUID;
                    forgotPasswordOTP.d = d;
                    var OTPreq = new OTPRequest();

                    OTPreq.TIN = idNumber;
                    OTPreq.email = "";
                    OTPreq.birthDate = "";
                    OTPreq.language = UtilityManager.GetLanguageParameter();
                    OTPreq.captchaCode = Captcha;
                    OTPreq.GUID = GUID;

                    var OTPResponseDATA = await WebServiceManager.SendOTP(OTPreq);

                    await PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                    if (OTPResponseDATA?.result != null/* && !string.IsNullOrEmpty(forgotPasswordOTP.d.EmailId)*/)
                    {
                        ContinueButtonEnability = true;
                        IsResendOTPEnabled = false;
                        StartOTPTimer();
                        ResendOTPTextColor = (Color)Application.Current.Resources["DarkGrayTextColor"];
                        IsAPICalledSuccessfully = true;

                        OTPFirstDigit = string.Empty;
                        OTPSecondDigit = string.Empty;
                        OTPThirdDigit = string.Empty;
                        OTPFourthDigit = string.Empty;

                        StartPage = 2;

                        DefaultCardLayoutVisibility = false;
                        UserIDLayoutVisibility = false;
                        VerificationCodeVisibility = true;
                        MobileNumber = OTPResponseDATA.result.mobileNumber;
                        OTPSentOnThisMobileNumber = AppResources.MobileNumber + " " + MobileNumber;
                        Captcha = OTPResponseDATA.result.captchaCode;
                    }
                    else
                    {
                        IsLoading = false;
                        MessageTxt = AppResources.ErrorCaptcha;
                        IsShowMsgView = true;
                    }

                }
                catch (GAZTErrorException ex)
                {
                    IsAPICalledSuccessfully = false;
                    SetIDNumberEnability = true;
                    IDNumber = String.Empty;
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                }
                IsLoading = false;
            }
            catch (InternetException ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                IsLoading = false;

                SetIDNumberEnability = true;
                IDNumber = String.Empty;
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
                    string st = ZATCAConstants.BaseUrlOfODataServices + ZATCAConstants.ForgotPasswordServiceName + "/SecuredHeaderSet(Tin=";
                    string id = st + "'" + idNumber + "'" + ",Langu='" + lang + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "" + "'" + ",MobileNo='" + "" + "'" + ",SubType='" + "" + "'" + ",Idnumber='" + "" + "'" + ",Otp='" + EnteredOTP + "'" + ",Dob=datetime'" + "2019-12-21T00%3A00%3A00" + "'" + ",NewPwd='" + "" + "'" + ",RdBt='" + "P" + "')";
                    string st1 = ZATCAConstants.BaseUrlOfODataServices + ZATCAConstants.ForgotPasswordServiceName + "/SecuredHeaderSet(Tin=";
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
                    d.Captcha = captcha;
                    d.Guid = GUID;
                    d.Application = "FPWD";
                    // d.otPasswordOTP.d.Dob = "/Date(1576886400000)/";
                    d.NewPwd = "";
                    d.CnfPwd = "";
                    d.RdBt = "P";
                    d.Hyperlink = "";
                    forgotPassword.d = d;
                    VAliadteOTP vAliadteOTP = new VAliadteOTP();
                    vAliadteOTP.TIN = idNumber;
                    vAliadteOTP.language = UtilityManager.GetLanguageParameter();
                    vAliadteOTP.captchaCode = captcha;
                    vAliadteOTP.GUID = GUID;
                    vAliadteOTP.OTP = EnteredOTP;
                    var response = await WebServiceManager.ValidateOTPNEW(vAliadteOTP);
                    await PopToRootPage();// If seesion Expired it will navigate to Dashboard page

                    if (response != null && response.result != null)
                    {
                        StartPage = StartPage + 1;
                        VerificationCodeVisibility = false;
                        PasswordLayoutVisibility = true;

                        IsAPICalledSuccessfully = true;

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

                        OTPFirstDigit = string.Empty;
                        OTPSecondDigit = string.Empty;
                        OTPThirdDigit = string.Empty;
                        OTPFourthDigit = string.Empty;
                        string messagefordialogue = AppResources.ZYouraccounthasbeenlockedPleasecontactourcallcenter;
                        if (!App.IsArabic)
                        {
                            messagefordialogue = messagefordialogue.Replace("{0}", "3");
                        }
                        else
                        {
                            messagefordialogue = messagefordialogue.Replace("}0{", "3");
                        }
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(messagefordialogue));

                        _navigationService.GoBack();
                    }
                    else
                    {
                        if (currentAttempts == 1)
                        {
                            //Invalied user name
                            OTPFirstDigit = string.Empty;
                            OTPSecondDigit = string.Empty;
                            OTPThirdDigit = string.Empty;
                            OTPFourthDigit = string.Empty;
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZZZWrongVerificationCode));
                        }
                        else if (currentAttempts == 2)
                        {
                            // You have one remaining attaampt
                            OTPFirstDigit = string.Empty;
                            OTPSecondDigit = string.Empty;
                            OTPThirdDigit = string.Empty;
                            OTPFourthDigit = string.Empty;
                            String message = String.Format(AppResources.ZYouhaveoneremainingattemptthentheaccountwillbelocked, "1");
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(message));
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

                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.EnterVerificationCode));
                }
                IsLoading = false;
            }
            catch (GAZTErrorException ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                IsLoading = false;
            }
            catch (InternetException ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                IsLoading = false;
            }
        }
        public async Task SendIDNumberToUsernameEmail()
        {
            try
            {
                string idNumber = GetTinId();

                IsLoading = true;
                string lang = UtilityManager.GetLanguageParameter();

                ForgotPasswordOTP1 forgotPassword = new ForgotPasswordOTP1();
                ForgotPasswordOTP forgotPassword1 = new ForgotPasswordOTP();
                forgotPassword.Action = "";
                forgotPassword.Langu = UtilityManager.GetLanguageParameter();
                forgotPassword.TpType = "1";
                forgotPassword.MobileNo = "";
                forgotPassword.SubType = "ZS0001";
                forgotPassword.Idnumber = idNumber;
                forgotPassword.Otp = EnteredOTP;
                forgotPassword.Captcha = Captcha;
                forgotPassword.Guid = GUID;
                forgotPassword.RdBt = "U";
                forgotPassword.Application = "FUSR";
                forgotPassword1 = await WebServiceManager.GAZTSendUserNameToEmail(forgotPassword);
                await PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                if (forgotPassword1 != null && forgotPassword1.d != null)
                {

                    if (forgotPassword1.d != null)
                    {
                        captcha = forgotPassword1.d.Captcha;
                    }
                    await SendUserNameToRegidteredEmail();
                }
                else
                {
                    IsAPICalledSuccessfully = false;
                    IDNumber = string.Empty;
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NDEntervaliduserid));
                }
                IsLoading = false;
            }
            catch (GAZTErrorException ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                IsLoading = false;
            }
            catch (InternetException ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                IsLoading = false;
            }
        }
        private async Task SendUserNameToRegidteredEmail()
        {
            try
            {
                string idNumber = GetTinId();
                IsLoading = true;
                ForgotPasswordOTP1 forgotPassword = new ForgotPasswordOTP1();
                ForgotPasswordOTP forgotPassword1 = new ForgotPasswordOTP();

                D d = new D();
                forgotPassword.Action = "40";
                forgotPassword.Langu = UtilityManager.GetLanguageParameter();
                forgotPassword.MobileNo = "";
                forgotPassword.SubType = "ZS0001";
                forgotPassword.Idnumber = idNumber;
                forgotPassword.Otp = EnteredOTP;
                forgotPassword.Captcha = captcha;
                forgotPassword.Guid = GUID;
                forgotPassword.RdBt = "U";
                forgotPassword1 = await WebServiceManager.GAZTSendUserNameToEmail(forgotPassword);
                await PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                if (forgotPassword1?.d != null)
                {

                    var _navigation = Application.Current.MainPage.Navigation;
                    try
                    {
                        _navigation.NavigationStack.ToList().Clear();
                        await _navigation.PopToRootAsync();
                        await Application.Current.MainPage.Navigation.PushModalAsync(new GAZTNewDesignRecoverUsernamePageView());
                    }
                    catch (Exception)
                    {


                    }
                }
                else
                {
                    IsAPICalledSuccessfully = false;
                    IDNumber = string.Empty;
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NDEntervaliduserid));
                }
                IsLoading = false;
            }
            catch (GAZTErrorException ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                IsLoading = false;
            }
            catch (InternetException ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                IsLoading = false;
            }
        }
        private async Task ChangePassword()
        {
            try
            {
                currentAttempts = 0;

                bool isNewPasswordValid = UtilityManager.IsPasswordValid(NewPassword);
                if (isNewPasswordValid)
                {
                    IsLoading = true;



                    PasswordChangeRequest passwordChangeRequest = new PasswordChangeRequest();
                    passwordChangeRequest.TIN = IDNumber;
                    passwordChangeRequest.GUID = GUID;
                    passwordChangeRequest.language = UtilityManager.GetLanguageParameter(); ;
                    passwordChangeRequest.newPassword = NewPassword;
                    passwordChangeRequest.TIN = IDNumber;




                    if (NewPassword.Equals(ConfirmPassword))
                    {
                        var dataPassoerd = await WebServiceManager.ChangePassword(passwordChangeRequest);
                        await PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                        if (dataPassoerd != null && dataPassoerd.result != null)
                        {
                            StartPage = StartPage + 1;
                            NewPasswordLayoutVisibility = false;
                            OTPLayoutVisibility = false;
                            await navigateLogin();
                            return;

                        }
                        else
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Somethingwentwrong));
                        }
                    }
                    else
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Boththepasswordfieldsshouldmatch));
                    }
                }
                else
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PasswordGuidelineText));
                }
                IsLoading = false;
            }
            catch (GAZTErrorException ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

            }
            catch (InternetException ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                IsLoading = false;
            }
        }

        private async Task navigateLogin()
        {
            try
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.YourPasswordhasbeenChangedsuccessfully));
                var _navigation = Application.Current.MainPage.Navigation;

                await _navigationService.NavigateTo(App.SFLoginPageView, App.GAZTNewDesignDashBoardPageView);
                _navigation.NavigationStack.ToList().Clear();

            }
            catch (Exception)
            {

            }
        }

        public async Task PopToRootPage()
        {
            if (App.IsSessionExpired)
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
                await _navigationService.NavigateTo(App.SFLoginPageView, App.GAZTNewDesignDashBoardPageView);
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
                        IsLoading = true;
                        SelectedTinId = null;
                        Tins = await WebServiceManager.GAZTGetAllTins(Email);
                        TINs = Tins;
                        if (Tins.Count != 0)
                        {
                            Enabled = true;
                            IsAPICalledSuccessfully = true;
                            UserNameLabelText = AppResources.UserName;
                            IsVisibleTinIds = true;
                            SelectedTinId = TINs.FirstOrDefault();
                        }
                        else
                        {
                            IsAPICalledSuccessfully = false;

                            IsVisibleTinIds = false;
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NoTINsAvailable));

                        }
                        IsLoading = false;
                    }
                    catch (Exception)
                    {
                        IsAPICalledSuccessfully = false;

                        IsVisibleTinIds = false;
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NetworkConnectivityIssue));
                        IsLoading = false;
                    }
                }
                catch (InternetException ex)
                {

                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                    IsLoading = false;
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
                    if (TotalSec <= 0)
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
                    if (TotalSec <= 0)
                    {
                        OTPValidDuration = " 0:00";
                        ButtonDisableColor = (Color)Application.Current.Resources["Primary"];
                        VerifyButtonDisableColor = (Color)Application.Current.Resources["ButtonGray"];


                        IsResendOTPEnabled = true;

                        IsVerifyOTPEnabled = false;
                        IsOTPEntryEnable = false;
                        return false;
                    }
                    TotalSec = TotalSec - 1;
                    numberOfSeconds = TotalSec;
                    TimeSpan _TimeSpan = TimeSpan.FromSeconds(TotalSec);
                    OTPValidDuration = " " + string.Format("{0:00}:{1:00}", _TimeSpan.Minutes, _TimeSpan.Seconds);
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
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZMandatorydatanotentered));

                IsLoading = false;
            }
        }
        public bool ValidateFirstStep()
        {
            if (UserNameCardBackgroundImg == "FP_selected_tile")
            {
                if (CorporateCardBackgroundImg == "FP_selected_tile" || IndividualOrPersonalBusinessCardBackgroundImg == "FP_selected_tile")
                {
                    return true;
                }
            }
            if (PasswordCardBackgroundImg == "FP_selected_tile")
            {
                return true;
            }
            return false;
        }

        public void ClearData()
        {
            GUID = string.Empty;

            PasswordLayoutVisibility = false;
            RecoverUserNameLayout = false;
            RecoverPasswordLayout = false;
            VerificationCodeVisibility = false;
            ForgotUserNameCardLayoutVisibility = false;
            UserIDLayoutVisibility = false;
            DefaultCardLayoutVisibility = true;
            UserNameLayoutVisibility = false;
            IDNumber = string.Empty;


            OTPFirstDigit = string.Empty;
            OTPSecondDigit = string.Empty;
            OTPThirdDigit = string.Empty;
            OTPFourthDigit = string.Empty;


            IsAPICalledSuccessfully = false;
            SetIDNumberEnability = false;
            PasswordCardBackgroundImg = "FP_unselected_tile";
            PasswordIcon = "Green_Key";
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
            EnteredOTP = OTPFirstDigit + OTPSecondDigit + OTPThirdDigit + OTPFourthDigit;
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
            if (countDownSeconds <= 0)
            {
                ContinueButtonEnability = false;
                IsResendOTPEnabled = true;
                ResendOTPTextColor = (Color)Application.Current.Resources["Primary"];
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

            otpTimer.Start();
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