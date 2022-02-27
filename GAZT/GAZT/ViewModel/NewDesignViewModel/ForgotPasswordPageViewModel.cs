
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
using Rg.Plugins.Popup.Services;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using EGAZT.Views.NewDesign.ForgotPasswordPages;
using Xamarin.Forms.Internals;
using GAZTeServicesBusinessLibrary.GAZTExceptions;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    [Preserve(AllMembers = true)]
    public class GAZTNewDesignForgotPasswordPageViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        private string captcha = string.Empty;
        private string GUID = string.Empty;
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
                RaisePropertyChanged("StartPage");
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
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NDCorporateIDMustStartWithSeven));

                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.ToString());
                        Console.Write(ex.StackTrace.ToString());
                    }



                }
                RaisePropertyChanged("IDNumber");
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
                RaisePropertyChanged("IsContinueButtonVisibe");
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
                if (_isTinDopDownVisible == value) return;

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
                if (_email == value) return;

                _email = value;
                RaisePropertyChanged("Email");
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
                RaisePropertyChanged("UserNameLabelText");
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
                RaisePropertyChanged("Enabled");
            }
        }

        private Color _resendOTPTextColor =  (Color)Application.Current.Resources["DarkGrayTextColor"];
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
                RaisePropertyChanged("ResendOTPTextColor");
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
                if (_recoverUserNameLayout == value) return;

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
                if (_recoverPasswordLayout == value) return;

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
                if (_LblCountDownTimer == value) return;

                _LblCountDownTimer = value;
                RaisePropertyChanged("LblCountDownTimer");
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
                RaisePropertyChanged("ForgotPwHeadingVisibility");
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
                if (_userIDLayoutVisibility == value) return;

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
                if (_defaultCardLayoutVisibility == value) return;

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
                if (_isLoading == value) return;

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
                if (_MinEight == value) return;

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
                if (_CapsSmall == value) return;

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
                if (_MaxSixteen == value) return;

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
                if (_NumSymbol == value) return;

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
                if (_userNameLayoutVisibility == value) return;

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
                if (_continueButtonEnability == value) return;

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
                if (_newPassword == value) return;

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
                if (_confirmPassword == value) return;

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
                if (_setIDNumberEnability == value) return;

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
                if (_forgotUserNameCardLayoutVisibility == value) return;

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
                if (_enteredOTP == value) return;

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
                if (_continueORConfirmButtonText == value) return;

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
                if (_isResendOTPEnabled == value) return;

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
                if (_userNameCardBackgroundImg == value) return;

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
                if (_userIcon == value) return;

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
                if (_passwordCardBackgroundImg == value) return;

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
                if (_passwordIcon == value) return;

                _passwordIcon = value;
                RaisePropertyChanged("PasswordIcon");
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
                RaisePropertyChanged("UserNameTextColor");
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
                if (_oTPSentOnThisMobileNumber == value) return;

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
                if (_isOTPEntryEnable == value) return;

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
                if (_selectedTaxPayerType == value) return;

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
                if (_selectedTaxPayerTypePrev == value) return;

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
                if (_forgotTypeList == value) return;

                _forgotTypeList = value;
                RaisePropertyChanged("ForgotTypeList");
            }
        }
        private bool _isDropImageVisible;
        public bool IsDropImageVisible
        {
            get => _isDropImageVisible;
            set
            {
                _isDropImageVisible = value;
                RaisePropertyChanged("IsDropImageVisible");
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
                if (_txtSelectedUsernameAndPassword == value) return;

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
                if (_txtSelectTaxpayerType == value) return;

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
                if (_txtTIN == value) return;

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
                if (_selectedTinId == value) return;

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
                if (_selectedTinIdPrev == value) return;

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
                if (_isVisibleTinIds == value) return;

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
                if (_selectedForgotType == value) return;

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
                if (_selectedForgotTypePrev == value) return;

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
                if (_forgotCredentialType == value) return;

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
                if (_taxpayerTypeList == value) return;

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
                if (_enteredCaptchaValue == value) return;

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
                if (_corporateID == value) return;

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
                if (_mobileNumber == value) return;

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
                if (_oTPValidDuration == value) return;

                _oTPValidDuration = value;
                if (_oTPValidDuration.Equals(" 00:00"))
                {
                    ButtonDisableColor =  (Color)Application.Current.Resources["Primary"];
                    VerifyButtonDisableColor =  (Color)Application.Current.Resources["ButtonGray"];
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
                if (_userName == value) return;

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
                if (_isForgotUserNameWithIndividual == value) return;

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
                if (_isForgotUserNameWithCorporate == value) return;

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
                if (_isForgotPassword == value) return;

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
                if (_isTaxPayerTypeEnable == value) return;

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
                if (_newPasswordLayoutVisibility == value) return;

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
                if (_mainPageLayoutVisibility == value) return;

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
                if (_oTPLayoutVisibility == value) return;

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
                if (_navigateToLoginLinkVisibility == value) return;

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
                if (_iDNumberOrCorporateIDOrUserName == value) return;

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
                if (_captcha == value) return;

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
                if (_forgotPasswordUserNameChangedMessage == value) return;

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
                if (_isIDTypeVisible == value) return;

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
                if (_newPasswordVisibility == value) return;

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
                if (_buttonDisableColor == value) return;

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
                if (_confirmPasswordVisibility == value) return;

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
                if (_isSubmitEnabled == value) return;

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
                if (_submitDisableButtonColor == value) return;

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
                if (_verifybuttonDisableColor == value) return;

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
                if (_isVerifyOTPEnabled == value) return;

                _isVerifyOTPEnabled = value;
                OnValidateOTPClicked.ChangeCanExecute();
                RaisePropertyChanged("IsVerifyOTPEnabled");
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
                if (_selectedTaxPayerTypeIndex == value) return;

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
                if (_corporateCardBackgroundImg == value) return;

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
                if (_individualOrPersonalBusinessCardBackgroundImg == value) return;

                _individualOrPersonalBusinessCardBackgroundImg = value;
                RaisePropertyChanged(nameof(IndividualOrPersonalBusinessCardBackgroundImg));
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
                if (_individualOrPersonalBusinessTextColor == value) return;

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
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NDTypeyourIDNumber));

                                //   _dialogService.ShowMessageBox(AppResources.NDTypeyourIDNumber, AppResources.Information);
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
                                if (!string.IsNullOrEmpty(Email))
                                {
                                    await SendOTPToRegisterMobileNumber();
                                }
                                else
                                {
                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.TypeYourUserName));

                                    //   _dialogService.ShowMessageBox(AppResources.NDTypeyourIDNumber, AppResources.Information);

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
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Pleaseenterconfirmationcodesenttoyourmobilenumber));

                            // _dialogService.ShowMessageBox(AppResources.Pleaseenterconfirmationcodesenttoyourmobilenumber, AppResources.Information);

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
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZNewpasswordandconfirmpassworddoesnotmatch));

                                //  _dialogService.ShowMessageBox(AppResources.ZZNewpasswordandconfirmpassworddoesnotmatch, AppResources.Information);
                            }

                        }
                        else
                        {
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PasswordValidationMesseg));

                            //     _dialogService.ShowMessageBox(AppResources.NDTypeyourIDNumber, AppResources.Information);

                        }
                    }

                }
                else if (IsUserNameCardTapped == true)
                {
                    if (!string.IsNullOrEmpty(IDNumber))
                    {
                        await SendIDNumberToUsernameEmail();
                    }
                    else
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NDTypeyourIDNumber));

                        //    _dialogService.ShowMessageBox(AppResources.NDTypeyourIDNumber, AppResources.Information);
                    }


                }
                else if (IsUserNameCardTapped == false && IsPasswordCardTapped == false)
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZZZPleaseSelect));

                    //  _dialogService.ShowMessageBox(AppResources.ZZZZPleaseSelect, AppResources.Information);

                }
                else
                {

                }



            });




            OnCorporateCardClicked = new Command(() =>
            {
                MaximumUserNameCharacter = 10;
                CorporateCardBackgroundImg = "FP_selected_tile";
                CorporateTextColor = Color.White;
                IndividualOrPersonalBusinessCardBackgroundImg = "FP_unselected_tile";
                IndividualOrPersonalBusinessTextColor = (Color)Application.Current.Resources["Primary"];
                IDNumber = string.Empty;

            });


            OnIndividualOrPersonalBusinessCardClicked = new Command(() =>
            {
                MaximumUserNameCharacter = 10;
                IndividualOrPersonalBusinessCardBackgroundImg = "FP_selected_tile";
                IndividualOrPersonalBusinessTextColor = Color.White;
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
        public async void OtpFilled()
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
                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Pleaseenterconfirmationcodesenttoyourmobilenumber));

                // _dialogService.ShowMessageBox(AppResources.Pleaseenterconfirmationcodesenttoyourmobilenumber, AppResources.Information);

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
        public async Task GetCaptchAndGUID()
        {
            try
            {
                
                    IsLoading = true;
                    Enabled = false;
                
                    
                        string lang = UtilityManager.GetLanguageParameter();
                        string st = Constants.CaptchaAndGUID;
                        string type = "ZDP_CREATE_CAPTCHA_SRV.Header";// "ZDP_FRGT_USRNM_PWD_SRV.Header";
                        GenerateCaptchaGUID forgotPasswordOTP = new GenerateCaptchaGUID();
                        Metadata metadata = new Metadata();
                        metadata.id = st;
                        metadata.uri = st;
                        metadata.type = type;

                        GetCaptcha d = new GetCaptcha();
                        d.__metadata = metadata;
                        d.Captcha = "";
                        d.Guid = "";
                        d.Taxpayer = "";
                        d.Refresh = "";
                        d.Application = "FPWD";

                        forgotPasswordOTP.d = d;
                        forgotPasswordOTP = await WebServiceManager.GAZTCaptchaAndGUID(forgotPasswordOTP);
                        PopToRootPage();// If seesion Expired it will navigate to Dashboard page

                        if (forgotPasswordOTP?.d != null && !string.IsNullOrEmpty(forgotPasswordOTP.d.Captcha))
                        {
                            captcha = forgotPasswordOTP.d.Captcha;
                            GUID = forgotPasswordOTP.d.Guid;
                            IsAPICalledSuccessfully = true;
                        }
                        else
                        {

                        }


                IsLoading = false;
            }
                    
            catch (InternetException ex)
            {
                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                //   await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                await Task.Run(() =>
                {
                    IsLoading = false;

                    SetIDNumberEnability = true;
                    IDNumber = String.Empty;
                    // UserIDLayoutVisibility = true;
                });
            }
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
                        string st = Constants.BaseUrlOfODataServices + Constants.ForgotPasswordServiceName + "/SecuredHeaderSet(Tin=";
                        string id = st + "'" + "" + "'" + ",Langu='" + lang + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "1" + "'" + ",MobileNo='" + "" + "'" + ",SubType='" + "" + "'" + ",Idnumber='" + "" + "'" + ",Otp='" + "" + "'" + ",Dob=datetime'" + "2019-12-21T00%3A00%3A00" + "'" + ",NewPwd='" + "" + "'" + ",RdBt='" + "P" + "')";
                        string st1 = Constants.BaseUrlOfODataServices + Constants.ForgotPasswordServiceName + "/SecuredHeaderSet(Tin=";
                        string uri = st1 + "'" + "" + "'" + ",Langu='" + lang + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "" + "'" + ",MobileNo='" + "" + "'" + ",SubType='" + "" + "'" + ",Idnumber='" + "" + "'" + ",Otp='" + "" + "'" + ",Dob=datetime'" + "2019-12-21T00%3A00%3A00" + "'" + ",NewPwd='" + "" + "'" + ",RdBt='" + "P" + "')";
                        string type = Constants.ForgotPasswordServiceName + ".Header";// "ZDP_FRGT_USRNM_PWD_SRV.Header";
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
                        d.Captcha = captcha;
                        d.Guid = GUID;
                        forgotPasswordOTP.d = d;
                        forgotPasswordOTP = await WebServiceManager.GAZTFogotPasswordSendOTP(forgotPasswordOTP);
                        PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                        if (forgotPasswordOTP.d != null/* && !string.IsNullOrEmpty(forgotPasswordOTP.d.EmailId)*/)
                        {
                            ContinueButtonEnability = true;
                            IsResendOTPEnabled = false;
                            StartOTPTimer();
                            ResendOTPTextColor =  (Color)Application.Current.Resources["DarkGrayTextColor"];
                            IsAPICalledSuccessfully = true;

                            OTPFirstDigit = string.Empty;
                            OTPSecondDigit = string.Empty;
                            OTPThirdDigit = string.Empty;
                            OTPFourthDigit = string.Empty;

                            //StartPage = StartPage + 1;
                            StartPage = 2;

                            DefaultCardLayoutVisibility = false;
                            UserIDLayoutVisibility = false;
                            VerificationCodeVisibility = true;
                            string _mobileNumber = forgotPasswordOTP.d.MobileNo.Substring(forgotPasswordOTP.d.MobileNo.Length - 4);
                            //MobileNumber = "XXXXXXXXXX" + _mobileNumber;
                            MobileNumber = forgotPasswordOTP.d.MobileNo;
                            OTPSentOnThisMobileNumber = AppResources.MobileNumber + " " + MobileNumber;

                        }
                      
                    }
                    catch (GAZTVATRegistrationInProcessException ex)
                    {
                        IsAPICalledSuccessfully = false;
                        SetIDNumberEnability = true;
                        IDNumber = String.Empty;
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.ToString());
                        Console.Write(ex.StackTrace.ToString());
                    }
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (InternetException ex)
            {
                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                //   await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                await Task.Run(() =>
                {
                    IsLoading = false;

                    SetIDNumberEnability = true;
                    IDNumber = String.Empty;
                    // UserIDLayoutVisibility = true;
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
                        string st = Constants.BaseUrlOfODataServices + Constants.ForgotPasswordServiceName + "/SecuredHeaderSet(Tin=";
                        string id = st + "'" + idNumber + "'" + ",Langu='" + lang + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "" + "'" + ",MobileNo='" + "" + "'" + ",SubType='" + "" + "'" + ",Idnumber='" + "" + "'" + ",Otp='" + EnteredOTP + "'" + ",Dob=datetime'" + "2019-12-21T00%3A00%3A00" + "'" + ",NewPwd='" + "" + "'" + ",RdBt='" + "P" + "')";
                        string st1 = Constants.BaseUrlOfODataServices + Constants.ForgotPasswordServiceName + "/SecuredHeaderSet(Tin=";
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
                        d.Captcha = captcha;
                        d.Guid = GUID;
                        d.Application = "FPWD";
                        // d.otPasswordOTP.d.Dob = "/Date(1576886400000)/";
                        d.NewPwd = "";
                        d.CnfPwd = "";
                        d.RdBt = "P";
                        d.Hyperlink = "";
                        forgotPassword.d = d;
                        forgotPassword = await WebServiceManager.GAZTForgotPasswordValidateOTP(forgotPassword);
                        PopToRootPage();// If seesion Expired it will navigate to Dashboard page

                        if (forgotPassword != null && forgotPassword.d != null && forgotPassword.d.Action.Equals("01"))
                        {
                            StartPage = StartPage + 1;
                            VerificationCodeVisibility = false;
                            PasswordLayoutVisibility = true;

                            IsAPICalledSuccessfully = true;

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Pleasechangepassword));

                                //   await _dialogService.ShowMessageBox(AppResources.Pleasechangepassword, AppResources.Information);
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
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(messagefordialogue));

                                //  await _dialogService.ShowMessageBox(messagefordialogue, AppResources.Information);
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
                                    OTPFirstDigit = string.Empty;
                                    OTPSecondDigit = string.Empty;
                                    OTPThirdDigit = string.Empty;
                                    OTPFourthDigit = string.Empty;
                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZZZWrongVerificationCode));

                                    // await _dialogService.ShowMessageBox(AppResources.ZZZZWrongVerificationCode, AppResources.ZError);
                                });
                            }
                            else if (currentAttempts == 2)
                            {
                                // You have one remaining attaampt
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    OTPFirstDigit = string.Empty;
                                    OTPSecondDigit = string.Empty;
                                    OTPThirdDigit = string.Empty;
                                    OTPFourthDigit = string.Empty;
                                    String message = String.Format(AppResources.ZYouhaveoneremainingattemptthentheaccountwillbelocked, "1");
                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(message));

                                    //  await _dialogService.ShowMessageBox(message, AppResources.ZError);
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
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.EnterVerificationCode));

                            //  await _dialogService.ShowMessageBox(AppResources.EnterVerificationCode, AppResources.Information);
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
                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                // await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
        }
        public async Task SendIDNumberToUsernameEmail()
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

                    ForgotPasswordOTP forgotPassword = new ForgotPasswordOTP();

                    D d = new D();
                    //d.__metadata = metadata;
                    d.Action = "";
                    d.Tin = "";
                    d.Langu = UtilityManager.GetLanguageParameter();
                    d.CurrAttmps = 0;
                    d.EmailId = "";
                    d.TpType = "1";
                    d.MobileNo = "";
                    d.SubType = "ZS0001";
                    d.Idnumber = idNumber;
                    d.Otp = EnteredOTP;
                    d.Minutes = 0;
                    d.Name = "";
                    d.Attempts = 0;
                    d.Captcha = captcha;
                    d.Guid = GUID;
                    d.NewPwd = "";
                    d.CnfPwd = "";
                    d.RdBt = "U";
                    d.Hyperlink = "";
                    d.Application = "FUSR";
                    forgotPassword.d = d;
                    forgotPassword = await WebServiceManager.GAZTSendUserNameToEmail(forgotPassword);
                    PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                    if (forgotPassword?.d != null)
                    {
                        //IsAPICalledSuccessfully = true;
                        //RecoverUserNameLayout = true;
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            //  _navigationService.NavigateTo(App.GAZTNewDesignRecoverUsername);
                            //await Application.Current.MainPage.Navigation.PopModalAsync(true);
                            //await Application.Current.MainPage.Navigation.PushModalAsync(new GAZTNewDesignRecoverUsernamePageView());
                            await SendUserNameToRegidteredEmail();

                            //    await Navigation.PushModalAsync(new GAZTNewDesignRecoverUsername(), true);


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
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NDEntervaliduserid));

                            //   await _dialogService.ShowMessageBox(AppResources.NDEntervaliduserid, AppResources.ZError);
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
                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                //   await _dialogService.ShowMessageBox(ex.Message, AppResources.ZError);
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
                    string st = Constants.BaseUrlOfODataServices + Constants.ForgotPasswordServiceName + "/SecuredHeaderSet(Tin=";
                    string id = st + "'" + "" + "'" + ",Langu='" + lang + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "1" + "'" + ",MobileNo='" + "" + "'" + ",SubType='" + "ZS001" + "'" + ",Idnumber='" + idNumber + "'" + ",Otp='" + "" + "'" + ",Dob=datetime'" + "2019-12-21T00%3A00%3A00" + "'" + ",NewPwd='" + "" + "'" + ",RdBt='" + "U" + "')";
                    string st1 = Constants.BaseUrlOfODataServices + Constants.ForgotPasswordServiceName + "/SecuredHeaderSet(Tin=";
                    string uri = st1 + "'" + "" + "'" + ",Langu='" + lang + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "" + "'" + ",MobileNo='" + "1" + "'" + ",SubType='" + "ZS001" + "'" + ",Idnumber='" + IDNumber + "'" + ",Otp='" + "" + "'" + ",Dob=datetime'" + "2019-12-21T00%3A00%3A00" + "'" + ",NewPwd='" + "" + "'" + ",RdBt='" + "U" + "')";
                    string type = Constants.ForgotPasswordServiceName + ".Header";// "ZDP_FRGT_USRNM_PWD_SRV.Header";
                    ForgotPasswordOTP forgotPassword = new ForgotPasswordOTP();
                    Metadata metadata = new Metadata();
                    metadata.id = id;
                    metadata.uri = uri;
                    metadata.type = type;
                    D d = new D();
                    //d.__metadata = metadata;
                    d.Action = "40";
                    d.Tin = "";
                    d.Langu = UtilityManager.GetLanguageParameter();
                    d.CurrAttmps = 0;
                    d.EmailId = "";
                    d.TpType = "1";
                    d.MobileNo = "";
                    d.SubType = "ZS0001";
                    d.Idnumber = idNumber;
                    d.Otp = EnteredOTP;
                    d.Minutes = 0;
                    d.Name = "";
                    d.Attempts = 0;
                    d.Captcha = captcha;
                    d.Guid = GUID;
                    // d.otPasswordOTP.d.Dob = "/Date(1576886400000)/";
                    d.NewPwd = NewPassword;
                    d.CnfPwd = ConfirmPassword;
                    d.RdBt = "U";
                    d.Hyperlink = "";
                    d.Application = "FUSR";
                    forgotPassword.d = d;
                    forgotPassword = await WebServiceManager.GAZTSendUserNameToEmail(forgotPassword);
                    PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                    if (forgotPassword!=null&&forgotPassword?.d != null)
                    {
                        //IsAPICalledSuccessfully = true;
                        //RecoverUserNameLayout = true;
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            //  _navigationService.NavigateTo(App.GAZTNewDesignRecoverUsername);
                            await Application.Current.MainPage.Navigation.PopModalAsync(true);
                            await Application.Current.MainPage.Navigation.PushModalAsync(new GAZTNewDesignRecoverUsernamePageView());

                            //    await Navigation.PushModalAsync(new GAZTNewDesignRecoverUsername(), true);


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
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NDEntervaliduserid));

                            //   await _dialogService.ShowMessageBox(AppResources.NDEntervaliduserid, AppResources.ZError);
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
                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                //   await _dialogService.ShowMessageBox(ex.Message, AppResources.ZError);
                await Task.Run(() =>
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
                        d.Captcha = captcha;
                        d.Guid = GUID;
                        d.Application = "FPWD";
                        // d.otPasswordOTP.d.Dob = "/Date(1576886400000)/";
                        d.NewPwd = NewPassword;
                        d.CnfPwd = ConfirmPassword;
                        d.RdBt = "P";
                        d.Hyperlink = "";
                        forgotPassword.d = d;
                        if (NewPassword.Equals(ConfirmPassword))
                        {
                            forgotPassword = await WebServiceManager.GAZTChangePassword(forgotPassword);
                            PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                            if (forgotPassword != null && forgotPassword.d != null)
                            {
                                StartPage = StartPage + 1;
                                //  RecoverPasswordLayout = true;
                                // await _dialogService.ShowMessageBox(AppResources.YourPasswordhasbeenChangedsuccessfully, AppResources.Information);
                                NewPasswordLayoutVisibility = false;
                                OTPLayoutVisibility = false;
                                //  NavigateToLoginLinkVisibility = true;
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    await Application.Current.MainPage.Navigation.PopModalAsync(true);
                                    await Application.Current.MainPage.Navigation.PushModalAsync(new GAZTNewDesignRecoverPasswordPageView());
                                    //  _navigationService.NavigateTo(App.GAZTNewDesignRecoverPasswordPageView);
                                });

                                ForgotPasswordUserNameChangedMessage = AppResources.ZZYourPasswordhasbeenChangedsuccessfully;
                                //  _navigationService.GoBack();
                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Somethingwentwrong));

                                    // await _dialogService.ShowMessageBox(AppResources.Somethingwentwrong, AppResources.Information);
                                });
                            }
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Boththepasswordfieldsshouldmatch));

                                //  await _dialogService.ShowMessageBox(AppResources.Boththepasswordfieldsshouldmatch, AppResources.Information);
                            });
                        }
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PasswordGuidelineText));

                            //  await _dialogService.ShowMessageBox(AppResources.PasswordGuidelineText, AppResources.Alerts);
                        });
                    }
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (GAZTVATRegistrationInProcessException ex)
            {
                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

            }
            catch (InternetException ex)
            {
                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                //   await _dialogService.ShowMessageBox(ex.Message, AppResources.Alerts);
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
        }
        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(() =>
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
                            if (Tins.Count != 0)
                            {
                                Enabled = true;
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
                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NoTINsAvailable));

                                    // await _dialogService.ShowMessageBox(AppResources.NoTINsAvailable, AppResources.Information);
                                });
                                //IsVisibleTinIds = false;
                            }
                            await Task.Run(() =>
                            {
                                IsLoading = false;
                            });
                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex.ToString());
                            Console.Write(ex.StackTrace.ToString());
                            IsAPICalledSuccessfully = false;

                            IsVisibleTinIds = false;
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                IsVisibleTinIds = false;
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.NetworkConnectivityIssue));

                                //  await _dialogService.ShowMessageBox(AppResources.NetworkConnectivityIssue, AppResources.Information);
                            });
                            await Task.Run(() =>
                            {
                                IsLoading = false;
                            });
                        }
                    }
                    catch (InternetException ex)
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));

                        //  await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
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
        private void ShowMandatoryFieldNotEnteredInformation(bool IsMandatoryFieldEntered)
        {
            if (!IsMandatoryFieldEntered)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsVisibleTinIds = false;
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZMandatorydatanotentered));

                    //   await _dialogService.ShowMessageBox(AppResources.ZZMandatorydatanotentered, AppResources.Alerts);
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });
                });
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
                ResendOTPTextColor =  (Color)Application.Current.Resources["Primary"];

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