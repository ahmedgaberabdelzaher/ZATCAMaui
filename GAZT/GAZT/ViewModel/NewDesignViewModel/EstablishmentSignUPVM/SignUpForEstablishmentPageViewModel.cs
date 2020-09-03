using EGAZT.Models;
using EGAZT.Models.EnumModels;
using EGAZT.Views.SyncFusionEnabledViews.AddPop;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
//MobileVerification = SummaryView
//EmailVerificatiom =VerificationView
namespace EGAZT.ViewModel.NewDesignViewModel.EstablishmentSignUPVM
{
    public class SignUpForEstablishmentPageViewModel : BaseViewModel
    {
      
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public int DefaultMonth;
        public DateTime dateTime { get; set; }
        public int numberOfSeconds = 120;
        int TotalSec;
        public bool StopTimer = false;
        public VATSignUpData vATSignUpData { get; set; }
        public VATSignUpCaseId SignUpCaseIdD { get; set; }
        #region Variable
        private EstablishmentSignUPTabEnum _currentTab = EstablishmentSignUPTabEnum.TermsAndConditions;
        public EstablishmentSignUPTabEnum CurrentTab
        {
            get => _currentTab;
            set
            {
                _currentTab = value;
                RaisePropertyChanged(nameof(CurrentTab));
                CurrentIndex = (int)_currentTab;
                RaisePropertyChanged(nameof(CurrentIndex));
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

        public void setCurrentTab()
        {
            CurrentTab = EstablishmentSignUPTabEnum.TermsAndConditions;
        }


        private int _currenrIndex = 1;
        public int CurrentIndex
        {
            get => _currenrIndex;
            set
            {
                _currenrIndex = value;
                RaisePropertyChanged(nameof(CurrentIndex));
                if (_currenrIndex == MaxIndex)
                {
                    MarkComplete = true;
                    RaisePropertyChanged(nameof(MarkComplete));
                }
                else if (MarkComplete==true && _currenrIndex < MaxIndex)
                {
                    MarkComplete = false;
                    RaisePropertyChanged(nameof(MarkComplete));
                }
            }
        }
        public bool MarkComplete { get; private set; } = false;
        public int MaxIndex { get; private set; } = 6;
        #endregion

        #region Commands
        public ICommand OnNextButtonClick { get; private set; }
        public ICommand OnBackButtonClick { get; private set; }
        public ICommand OnResendOTPClicked { get; set; }
        #endregion

        #region Propetry

        private string _maxDigids = "9";
        public string MaxDigids
        {
            get
            {
                return _maxDigids;
            }
            set
            {
                _maxDigids = value;
                RaisePropertyChanged("MaxDigids");
            }
        }


        private string _txtLicenseNumber = string.Empty;
        public string TxtLicenseNumber
        {
            get
            {
                return _txtLicenseNumber;
            }
            set
            {
                _txtLicenseNumber = value;
                RaisePropertyChanged("TxtLicenseNumber");
            }
        }
        private bool _issuedByTapped = false;
        public bool IssuedByTapped
        {
            get
            {
                return _issuedByTapped;
            }
            set
            {
                _issuedByTapped = value;
                RaisePropertyChanged("IssuedByTapped");
            }
        }
        private bool _issuedByCityTapped = false;
        public bool IssuedByCityTapped
        {
            get
            {
                return _issuedByCityTapped;
            }
            set
            {
                _issuedByCityTapped = value;
                RaisePropertyChanged("IssuedByCityTapped");
            }
        }

        private string _txtLOrCIssuedBy = string.Empty;
        public string TxtLOrCIssuedBy
        {
            get
            {
                return _txtLOrCIssuedBy;
            }
            set
            {
                _txtLOrCIssuedBy = value;
                RaisePropertyChanged("TxtLOrCIssuedBy");
            }
        }

        private IssuedByResponse _selectedIssuedBy = null;
        public IssuedByResponse SelectedIssuedBy
        {
            get
            {
                return _selectedIssuedBy;
            }
            set
            {
                _selectedIssuedBy = value;
                if (_selectedIssuedBy != null)
                {
                    TxtLOrCIssuedBy = _selectedIssuedBy.txt50;
                }
                RaisePropertyChanged("SelectedIssuedBy");
            }
        }
        private IssuedByResponse _selectedIssuedByPrev = null;
        public IssuedByResponse SelectedIssuedByPrev
        {
            get
            {
                return _selectedIssuedByPrev;
            }
            set
            {
                _selectedIssuedByPrev = value;
                RaisePropertyChanged("SelectedIssuedByPrev");
            }
        }

        private SignupCityResult _selectCityListPrev = null;
        public SignupCityResult SelectCityListPrev
        {
            get
            {
                return _selectCityListPrev;
            }
            set
            {
                _selectCityListPrev = value;
                RaisePropertyChanged("SelectCityListPrev");
            }
        }

        private SignupCityResult _selectCityList = null;
        public SignupCityResult SelectCityList
        {
            get
            {
                return _selectCityList;
            }
            set
            {
                _selectCityList = value;
                if (_selectCityList != null)
                {
                    TxtLOrCIssuedByCity = _selectCityList.CityName;
                }
                RaisePropertyChanged("SelectCityList");
            }
        }
        private string _txtLOrCIssuedByCity = string.Empty;
        public string TxtLOrCIssuedByCity
        {
            get
            {
                return _txtLOrCIssuedByCity;
            }
            set
            {
                _txtLOrCIssuedByCity = value;
                RaisePropertyChanged("TxtLOrCIssuedByCity");
            }
        }

        private string _pkrDBOPrev = string.Empty;
        public string PkrDBOPrev
        {
            get
            {
                return _pkrDBOPrev;
            }
            set
            {
                _pkrDBOPrev = value;
                RaisePropertyChanged("PkrDBOPrev");
            }
        }

        private string _pkrDBO = string.Empty;
        public string PkrDBO
        {
            get
            {
                return _pkrDBO;
            }
            set
            {
                _pkrDBO = value;
                RaisePropertyChanged("PkrDBO");
            }
        }
        private bool _isDeclarationCheckEnabled = false;
        public bool IsDeclarationCheckEnabled
        {
            get
            {
                return _isDeclarationCheckEnabled;
            }
            set
            {
                _isDeclarationCheckEnabled = value;
                RaisePropertyChanged("IsDeclarationCheckEnabled");
            }
        }
        private bool _isDeclarationCheckedForInstruction = false;
        public bool IsDeclarationCheckedForInstruction
        {
            get
            {
                return _isDeclarationCheckedForInstruction;
            }
            set
            {
                _isDeclarationCheckedForInstruction = value;
                if (_isDeclarationCheckedForInstruction == true)
                {
                    IsMainButtonEnabled = true;

                   // VATDeclarationData.d.TcFg = "1";
                }
                else
                {
                    IsMainButtonEnabled = false;

                   // VATDeclarationData.d.TcFg = "0";
                }
                RaisePropertyChanged("IsDeclarationCheckedForInstruction");
            }
        }

        private bool _isMainButtonEnabled = false;
        public bool IsMainButtonEnabled
        {
            get
            {
                return _isMainButtonEnabled;
            }
            set
            {
                
                _isMainButtonEnabled = value;
                //OnStepButtonClicked.ChangeCanExecute();
                RaisePropertyChanged("IsMainButtonEnabled");
            }
        }



        public string _PageTitle = AppResources.ZVatTermsAndConditions;
        public string PageTitle
        {
            get
            {
                return _PageTitle;
            }
            set
            {
                _PageTitle = value;
                RaisePropertyChanged("PageTitle");
            }
        }

        public string _BodyText;
        public string BodyText
        {
            get
            {
                return _BodyText;
            }
            set
            {
                _BodyText = value;
                RaisePropertyChanged("BodyText");
            }
        }

        public string _NextBTN = AppResources.ZZProceedtoindividualSignup;
        public string NextBTN
        {
            get
            {
                return _NextBTN;
            }
            set
            {
                _NextBTN = value;
                RaisePropertyChanged("NextBTN");
            }
        }

        public string _ImgBackgroundYes = "re_Property_Tile_Background_White";
        public string ImgBackgroundYes
        {
            get
            {
                return _ImgBackgroundYes;
            }
            set
            {
                _ImgBackgroundYes = value;
                RaisePropertyChanged("ImgBackgroundYes");
            }
        }

        public string _ImgBackgroundNo = "re_Tile_Background";
        public string ImgBackgroundNo
        {
            get
            {
                return _ImgBackgroundNo;
            }
            set
            {
                _ImgBackgroundNo = value;
                RaisePropertyChanged("ImgBackgroundNo");
            }
        }

        public string _ImgBackgroundCRNubmer = "FP_selected_tile";
        public string ImgBackgroundCRNubmer
        {
            get
            {
                return _ImgBackgroundCRNubmer;
            }
            set
            {
                _ImgBackgroundCRNubmer = value;
                RaisePropertyChanged("ImgBackgroundCRNubmer");
            }
        }

        public string _ImgBackgroundLicenseNubmer = "FP_unselected_tile";
        public string ImgBackgroundLicenseNubmer
        {
            get
            {
                return _ImgBackgroundLicenseNubmer;
            }
            set
            {
                _ImgBackgroundLicenseNubmer = value;
                RaisePropertyChanged("ImgBackgroundLicenseNubmer");
            }
        }

        // *Email OTP Verification Properties
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
        // *Mobile OTP Verification Properties
        private string _mOTPFirstDigit;
        public string MOTPFirstDigit
        {
            get
            {
                return _mOTPFirstDigit;
            }
            set
            {
                _mOTPFirstDigit = value;
                if (!string.IsNullOrEmpty(MOTPFirstDigit))
                {
                    bool isNumberEntered = CheckOnlyNumber(MOTPFirstDigit[0]);
                    if (!isNumberEntered)
                    {
                        MOTPFirstDigit = string.Empty;
                    }
                }

                RaisePropertyChanged("MOTPFirstDigit");
            }
        }

        private string _mOTPSecondDigit;
        public string MOTPSecondDigit
        {
            get
            {
                return _mOTPSecondDigit;
            }
            set
            {
                _mOTPSecondDigit = value;
                if (!string.IsNullOrEmpty(MOTPSecondDigit))
                {
                    bool isNumberEntered = CheckOnlyNumber(MOTPSecondDigit[0]);
                    if (!isNumberEntered)
                    {
                        MOTPSecondDigit = string.Empty;
                    }
                }
                RaisePropertyChanged("MOTPSecondDigit");
            }
        }

        private string _mOTPThirdDigit;
        public string MOTPThirdDigit
        {
            get
            {
                return _mOTPThirdDigit;
            }
            set
            {
                _mOTPThirdDigit = value;
                if (!string.IsNullOrEmpty(MOTPThirdDigit))
                {
                    bool isNumberEntered = CheckOnlyNumber(MOTPThirdDigit[0]);
                    if (!isNumberEntered)
                    {
                        MOTPThirdDigit = string.Empty;
                    }
                }
                RaisePropertyChanged("MOTPThirdDigit");
            }
        }

        private string _mOTPFourthDigit;
        public string MOTPFourthDigit
        {
            get
            {
                return _mOTPFourthDigit;
            }
            set
            {
                _mOTPFourthDigit = value;
                if (!string.IsNullOrEmpty(MOTPFourthDigit))
                {
                    bool isNumberEntered = CheckOnlyNumber(MOTPFourthDigit[0]);
                    if (!isNumberEntered)
                    {
                        MOTPFourthDigit = string.Empty;
                    }
                }
                RaisePropertyChanged("MOTPFourthDigit");
            }
        }
        // * End

        //timer
        private string _lblCountDownTimer = string.Empty;
        public string LblCountDownTimer
        {
            get
            {
                return _lblCountDownTimer;
            }
            set
            {
                _lblCountDownTimer = value;
                RaisePropertyChanged("LblCountDownTimer");
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
                //if (_isResendOTPEnabled)
                //{
                //    ResendOtpButtonColor = Color.DarkGreen;
                //}
                //else
                //{
                //    ResendOtpButtonColor = Color.FromHex("#999999");
                //}
                RaisePropertyChanged("IsResendOTPEnabled");
            }
        }
        private bool _isTimerCancel = false;
        public bool IsTimerCancel
        {
            get
            {
                return _isTimerCancel;
            }
            set
            {
                _isTimerCancel = value;
                RaisePropertyChanged("IsTimerCancel");
            }
        }
        //end timer

        private bool _isPasswordEncripted = true;
        public bool IsPasswordEncripted
        {
            get
            {
                return _isPasswordEncripted;
            }
            set
            {
                _isPasswordEncripted = value;
                RaisePropertyChanged("IsPasswordEncripted");
            }
        }
        private bool _isConfirmPasswordEncripted = true;
        public bool IsConfirmPasswordEncripted
        {
            get
            {
                return _isConfirmPasswordEncripted;
            }
            set
            {
                _isConfirmPasswordEncripted = value;
                RaisePropertyChanged("IsConfirmPasswordEncripted");
            }
        }
        private string _DOB = string.Empty;
        public string DOB
        {
            get
            {
                return _DOB;
            }
            set
            {
                _DOB = value;
                RaisePropertyChanged("DOB");
            }
        }

        private SignUpUsing _selectedSignUpUsing = null;
        public SignUpUsing SelectedSignUpUsing
        {
            get
            {
                return _selectedSignUpUsing;
            }
            set
            {
                _selectedSignUpUsing = value;
                if (_selectedSignUpUsing != null)
                {
                    try
                    {
                        if (_selectedSignUpUsing.ID == 1)
                        {
                            MaxLengthID = 10;
                        }
                        else if (_selectedSignUpUsing.ID == 2)
                        {
                            MaxLengthID = 10;
                        }
                        else if (_selectedSignUpUsing.ID == 3)
                        {
                            MaxLengthID = 15;
                        }
                        TxtIDType = _selectedSignUpUsing.SUType;
                    }
                    catch (Exception Ex)
                    {
                    }
                }
                RaisePropertyChanged("SelectedSignUpUsing");
            }
        }

        private string _txtName = string.Empty;
        public string TxtName
        {
            get
            {
                return _txtName;
            }
            set
            {
                _txtName = value;
                RaisePropertyChanged("TxtName");
            }
        }

        private SignUpIdType _selectedIdType = null;
        public SignUpIdType SelectedIdType
        {
            get
            {
                return _selectedIdType;
            }
            set
            {
                _selectedIdType = value;
                if (_selectedIdType != null)
                {
                    try
                    {
                        if (_selectedIdType.ID.Equals("ZS0015"))
                        {
                            MaxLengthID = 10;
                            SetStateListVisibility = true;
                            SetCityListVisibility = true;
                            SetCountryVisibility = true;
                            SetGCCCountryVisibility = false;
                            SetEnabilityToCountryList = false;
                        }
                        else if (_selectedIdType.ID.Equals("ZS0017"))
                        {
                            MaxLengthID = 10;
                            SetStateListVisibility = true;
                            SetCityListVisibility = true;
                            SetCountryVisibility = true;
                            SetGCCCountryVisibility = false;
                            SetEnabilityToCountryList = false;
                        }
                        else if (_selectedIdType.ID.Equals("ZS0018"))
                        {
                            MaxLengthID = 15;
                            SetStateListVisibility = false;
                            SetCityListVisibility = false;
                            SetCountryVisibility = false;
                            SetGCCCountryVisibility = true;
                        }
                        TxtIDType = _selectedIdType.Name;
                    }
                    catch (Exception Ex)
                    {
                    }
                }
                RaisePropertyChanged("SelectedIdType");
            }
        }
        private int _maxLengthID = 10;
        public int MaxLengthID
        {
            get
            {
                return _maxLengthID;
            }
            set
            {
                _maxLengthID = value;
                RaisePropertyChanged("MaxLengthID");
            }
        }
        public bool _setStateListVisibility = true;
        public bool SetStateListVisibility
        {
            get
            {
                return _setStateListVisibility;
            }
            set
            {
                _setStateListVisibility = value;
                RaisePropertyChanged("SetStateListVisibility");
            }
        }
        public bool _setCityListVisibility = true;
        public bool SetCityListVisibility
        {
            get
            {
                return _setCityListVisibility;
            }
            set
            {
                _setCityListVisibility = value;
                RaisePropertyChanged("SetCityListVisibility");
            }
        }
        public bool _setCountryVisibility = true;
        public bool SetCountryVisibility
        {
            get
            {
                return _setCountryVisibility;
            }
            set
            {
                _setCountryVisibility = value;
                RaisePropertyChanged("SetCountryVisibility");
            }
        }
        public bool _setGCCCountryVisibility = false;
        public bool SetGCCCountryVisibility
        {
            get
            {
                return _setGCCCountryVisibility;
            }
            set
            {
                _setGCCCountryVisibility = value;
                RaisePropertyChanged("SetGCCCountryVisibility");
            }
        }
        private string _txtIDType = string.Empty;
        public string TxtIDType
        {
            get
            {
                return _txtIDType;
            }
            set
            {
                _txtIDType = value;
                RaisePropertyChanged("TxtIDType");
            }
        }
        private VATSignUpGCC _selectedGCCCountry = null;
        public VATSignUpGCC SelectedGCCCountry
        {
            get
            {
                return _selectedGCCCountry;
            }
            set
            {
                _selectedGCCCountry = value;

                RaisePropertyChanged("SelectedGCCCountry");
            }
        }
        public int _gelectedGCCCountryIndex;
        public int SelectedGCCCountryIndex
        {
            get
            {
                return _gelectedGCCCountryIndex;
            }
            set
            {
                _gelectedGCCCountryIndex = value;
                RaisePropertyChanged("SelectedGCCCountryIndex");
            }
        }
        public IList<SignupCityResult> _cityList;
        public IList<SignupCityResult> CityList
        {
            get
            {
                return _cityList;
            }
            set
            {
                _cityList = value;
                RaisePropertyChanged("CityList");
            }
        }
        public bool _setEnabilityToCountryList = false;
        public bool SetEnabilityToCountryList
        {
            get
            {
                return _setEnabilityToCountryList;
            }
            set
            {
                _setEnabilityToCountryList = value;
                RaisePropertyChanged("SetEnabilityToCountryList");
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
                    ButtonDisableColor = Color.FromHex("#006450");
                    ButtonDisableTextColor = Color.White;
                    IsResendOTPEnabled = true;
                    VerifyButtonDisableColor = Color.FromHex("#9EA4A9");
                    VerifyButtonDisableTextColor = Color.Gray;
                    IsVerifyOTPEnabled = false;
                    IsOTPEntryEnable = false;
                }
                RaisePropertyChanged("OTPValidDuration");
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
        private Color _verifybuttonDisableTextColor = Color.White;
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
        private Color _buttonDisableTextColor = Color.Gray;
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


        private SignUpModelRootObject _signUpFirstSubmitModel = null;
        public SignUpModelRootObject SignUpFirstSubmitModel
        {
            get
            {
                return _signUpFirstSubmitModel;
            }
            set
            {
                _signUpFirstSubmitModel = value;
                RaisePropertyChanged("SignUpFirstSubmitModel");
            }
        }
        private VATSignUpStateResults _selectedRegion = null;
        public VATSignUpStateResults SelectedRegion
        {
            get
            {
                return _selectedRegion;
            }
            set
            {
                _selectedRegion = value;
                if (_selectedRegion != null)
                {
                    SetCityList();
                }

                RaisePropertyChanged("SelectedRegion");
            }
        }
        private VATSignUPCityResults _selectedCity = null;
        public VATSignUPCityResults SelectedCity
        {
            get
            {
                return _selectedCity;
            }
            set
            {
                _selectedCity = value;

                RaisePropertyChanged("SelectedCity");
            }
        }

        public int _selectedCityIndex;
        public int SelectedCityIndex
        {
            get
            {
                return _selectedCityIndex;
            }
            set
            {
                _selectedCityIndex = value;
                RaisePropertyChanged("SelectedCityIndex");
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

        private bool _isTIN = false;
        public bool IsTIN
        {
            get
            {
                return _isTIN;
            }
            set
            {
                _isTIN = value;
                if (_isTIN == true)
                {
                    IsTINVisible = true;
                    TxtTIN = string.Empty;
                }
                else
                {
                    IsTINVisible = false;
                    TxtTIN = string.Empty;
                }
                RaisePropertyChanged("IsTIN");
            }
        }
        private bool _isTINVisible = false;
        public bool IsTINVisible
        {
            get
            {
                return _isTINVisible;
            }
            set
            {
                _isTINVisible = value;
                RaisePropertyChanged("IsTINVisible");
            }
        }
        private string _txtPhoneNumber = "";
        public string TxtPhoneNumber
        {
            get
            {
                return _txtPhoneNumber;
            }
            set
            {
                _txtPhoneNumber = value;
                RaisePropertyChanged("TxtPhoneNumber");
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
        private string _txtCountryCode = "+966";
        public string TxtCountryCode
        {
            get
            {
                return _txtCountryCode;
            }
            set
            {

                _txtCountryCode = value;
                if (_txtCountryCode != null)
                {
                    MaxDigids = (14 - _txtCountryCode.Length).ToString();
                }
                else
                {
                    MaxDigids = "15";
                }
                RaisePropertyChanged("TxtCountryCode");
            }
        }

        private string _txtMobileNumberwithCountryCode = "";
        public string TxtMobileNumberwithCountryCode
        {
            get
            {
                return _txtMobileNumberwithCountryCode;
            }
            set
            {
                _txtMobileNumberwithCountryCode = value;
                RaisePropertyChanged("TxtMobileNumberwithCountryCode");
            }
        }


        public string _idNumber;
        public string IdNumber
        {
            get
            {
                return _idNumber;
            }
            set
            {
                _idNumber = value;
                RaisePropertyChanged("IdNumber");
            }
        }
        private string _name = string.Empty;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }
        private string _mobileCountryCode = string.Empty;
        public string MobileCountryCode
        {
            get
            {
                return _mobileCountryCode;
            }
            set
            {
                _mobileCountryCode = value;
                RaisePropertyChanged("MobileCountryCode");
            }
        }
        public string _buildingNumber = "";
        public string BuildingNumber
        {
            get
            {
                return _buildingNumber;
            }
            set
            {
                _buildingNumber = value;
                RaisePropertyChanged("BuildingNumber");
            }
        }
        public string _unitNumber = "";
        public string UnitNumber
        {
            get
            {
                return _unitNumber;
            }
            set
            {
                _unitNumber = value;
                RaisePropertyChanged("UnitNumber");
            }
        }
        public string _neighborhood = "";
        public string Neighborhood
        {
            get
            {
                return _neighborhood;
            }
            set
            {
                _neighborhood = value;
                RaisePropertyChanged("Neighborhood");
            }
        }
        private string _email = string.Empty;
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
        private string _mobileNumber = string.Empty;
        public string MobileNumber
        {
            get
            {
                return _mobileNumber;
            }
            set
            {
                _mobileNumber = value;
                if (_mobileNumber != null && _mobileNumber.Length > 1)
                {
                    if (_mobileNumber.Length >= 9)
                    {
                        string mystring = _mobileNumber.Substring(_mobileNumber.Length - 4);
                        EncriptedMobileNumber = "xxxxxx" + mystring;

                    }
                }
                RaisePropertyChanged("MobileNumber");
            }
        }
        private string _encriptedMobileNumber = string.Empty;
        public string EncriptedMobileNumber
        {
            get
            {
                return _encriptedMobileNumber;
            }
            set
            {
                _encriptedMobileNumber = value;
                RaisePropertyChanged("EncriptedMobileNumber");
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
        private Color _verifybuttonDisableColor = Color.FromHex("#d49504");
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
        private ObservableCollection<object> _todayDate;
        public ObservableCollection<object> TodayDate
        {
            get
            {
                return _todayDate;
            }
            set
            {
                _todayDate = value;
                RaisePropertyChanged("TodayDate");
            }
        }
        private string _newPassword = string.Empty;
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
        private string _confirmPassword = string.Empty;
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

        //


        #endregion

        #region new Properties
        private List<SignUpUsing> _signUpUsingList = null;
        public List<SignUpUsing> SignUpUsingList
        {
            get
            {
                return _signUpUsingList;
            }
            set
            {
                _signUpUsingList = value;
                RaisePropertyChanged("SignUpUsingList");
            }
        }

        private int _iDTypeIndex = 0;
        public int IDTypeIndex
        {
            get
            {
                return _iDTypeIndex;
            }
            set
            {
                _iDTypeIndex = value;
                RaisePropertyChanged("IDTypeIndex");
            }
        }

        private List<IssuedByResponse> _issuedByList = null;
        public List<IssuedByResponse> IssuedByList
        {
            get
            {
                return _issuedByList;
            }
            set
            {
                _issuedByList = value;
                RaisePropertyChanged("IssuedByList");
            }
        }

        private bool _isCRVisible = false;
        public bool IsCRVisible
        {
            get
            {
                return _isCRVisible;
            }
            set
            {
                _isCRVisible = value;
                RaisePropertyChanged("IsCRVisible");
            }
        }

        private bool _isLicenseVisible = false;
        public bool IsLicenseVisible
        {
            get
            {
                return _isLicenseVisible;
            }
            set
            {
                _isLicenseVisible = value;
                RaisePropertyChanged("IsLicenseVisible");
            }
        }

        private bool _isCRChecked = false;
        public bool IsCRChecked
        {
            get
            {
                return _isCRChecked;
            }
            set
            {
                _isCRChecked = value;
                if (_isCRChecked == true)
                {
                    IsCRVisible = true;
                }
                else
                {
                    IsCRVisible = false;
                }
                RaisePropertyChanged("IsCRChecked");
            }
        }

        private bool _isLNChecked = false;
        public bool IsLNChecked
        {
            get
            {
                return _isLNChecked;
            }
            set
            {
                _isLNChecked = value;
                if (_isLNChecked == true)
                {
                    IsLicenseVisible = true;
                }
                else
                {
                    IsLicenseVisible = false;
                }
                RaisePropertyChanged("IsLNChecked");
            }
        }

        private string _txtTIN = string.Empty;
        public string TxtTIN
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

        private string _txtIDNumber = string.Empty;
        public string TxtIDNumber
        {
            get
            {
                return _txtIDNumber;
            }
            set
            {
                _txtIDNumber = value;
                RaisePropertyChanged("TxtIDNumber");
            }
        }

       

        private SignUpUsing _selectedSignUpUsingSetForCancle = null;
        public SignUpUsing SelectedSignUpUsingSetForCancle
        {
            get
            {
                return _selectedSignUpUsingSetForCancle;
            }
            set
            {
                _selectedSignUpUsingSetForCancle = value;
                RaisePropertyChanged("SelectedSignUpUsingSetForCancle");
            }
        }
        #endregion

        #region Constructor
        public SignUpForEstablishmentPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
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

            OnNextButtonClick = new Command(() => navigateToNext());
            OnBackButtonClick = new Command(() => navigateBack());
            OnResendOTPClicked = new Command(() => ResendOTPAsync());
        }


        private string _txtCRNumber = string.Empty;
        public string TxtCRNumber
        {
            get
            {
                return _txtCRNumber;
            }
            set
            {
                _txtCRNumber = value;
                RaisePropertyChanged("TxtCRNumber");
            }
        }

        private bool _isAllValidContactDataEnteredEmail = false;
        public bool IsAllValidContactDataEnteredEmail
        {
            get
            {
                return _isAllValidContactDataEnteredEmail;
            }
            set
            {
                _isAllValidContactDataEnteredEmail = value;
                RaisePropertyChanged("IsAllValidContactDataEnteredEmail");
            }
        }
        private bool _isAllValidContactDataEnteredMobileNbr = false;
        public bool IsAllValidContactDataEnteredMobileNbr
        {
            get
            {
                return _isAllValidContactDataEnteredMobileNbr;
            }
            set
            {
                _isAllValidContactDataEnteredMobileNbr = value;
                RaisePropertyChanged("IsAllValidContactDataEnteredMobileNbr");
            }
        }

        private bool _isAllValidContactDataEnteredPhoneNbr = false;
        public bool IsAllValidContactDataEnteredPhoneNbr
        {
            get
            {
                return _isAllValidContactDataEnteredPhoneNbr;
            }
            set
            {
                _isAllValidContactDataEnteredPhoneNbr = value;
                RaisePropertyChanged("IsAllValidContactDataEnteredPhoneNbr");
            }
        }

        private bool _isAllValidDataEntered = false;
        public bool IsAllValidDataEntered
        {
            get
            {
                return _isAllValidDataEntered;
            }
            set
            {
                _isAllValidDataEntered = value;
                RaisePropertyChanged("IsAllValidDataEntered");
            }
        }

        private bool _isAllValidCRNumberEntered = false;
        public bool IsAllValidCRNumberEntered
        {
            get
            {
                return _isAllValidCRNumberEntered;
            }
            set
            {
                _isAllValidCRNumberEntered = value;
                RaisePropertyChanged("IsAllValidCRNumberEntered");
            }
        }



        #endregion

        #region Methods



   public async void navigateToNext()
        {
            switch (CurrentTab)
            {
                case EstablishmentSignUPTabEnum.TermsAndConditions:
                    if (IsDeclarationCheckedForInstruction == true)
                    {
                       // _navigationService.NavigateTo(App.SignUpFormPageView);
                        PageTitle = AppResources.ZZZIndividualInformation;
                        BodyText = AppResources.ZZZZCompletethebelowdetails;
                        NextBTN = AppResources.ZZZZContinue;
                        CurrentTab = EstablishmentSignUPTabEnum.IndividualInformation;
                       
                        IsDeclarationCheckedForInstruction = false;
                        IsMainButtonEnabled = true;
                    }
                    else
                    {
                        PopUp popUp = new PopUp();
                        popUp.Message = AppResources.ZZPleaseselecttermsandconditions;
                        if (App.IsArabic)
                        {
                            popUp.FlowDirections = "RightToLeft";
                            popUp.isFontSet = true;
                        }
                        else
                        {
                            popUp.FlowDirections = "LeftToRight";
                        }
                        await PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                    }

                        
                    break;

                case EstablishmentSignUPTabEnum.IndividualInformation:
                        if (IsAllValidDataEntered)
                        {
                            PageTitle = AppResources.ZZZBusinessInformation;
                            BodyText = AppResources.ZZZZCompletethebelowdetails;
                            CurrentTab = EstablishmentSignUPTabEnum.BusinessInformation;
                    }
                        else
                        {
                            _dialogService.ShowMessage(AppResources.ZZPleasefillthemandatoryfields, AppResources.Information); _dialogService.ShowMessage(AppResources.ZZPleasefillthemandatoryfields, AppResources.Information);
                        }
                    
                    break;

                case EstablishmentSignUPTabEnum.BusinessInformation:

                    //CRNumber Tile
                    if (IsCRChecked == true)
                    {
                        if (IsAllValidCRNumberEntered)
                        {
                            PageTitle = AppResources.ZZZContactInformation;
                            BodyText = AppResources.ZZZZCompletethebelowdetails;

                            CurrentTab = EstablishmentSignUPTabEnum.ContactInformation;
                        }
                        else
                        {
                            _dialogService.ShowMessage(AppResources.ZZPleasefillthemandatoryfields, AppResources.Information);
                        }
                    }
                    //License Number Validation
                    else
                    {
                        Console.WriteLine(TxtLOrCIssuedByCity);
                        Console.WriteLine(TxtLOrCIssuedBy);

                        //AllValid Data Entered
                        if (!string.IsNullOrEmpty(TxtLicenseNumber)
                            &&TxtLicenseNumber.Length == 20
                            && IssuedByTapped
                            && IssuedByCityTapped)
                            
                        {
                            PageTitle = AppResources.ZZZContactInformation;
                            BodyText = AppResources.ZZZZCompletethebelowdetails;

                            CurrentTab = EstablishmentSignUPTabEnum.ContactInformation;

                        }
                        else {
                            _dialogService.ShowMessage(AppResources.ZZPleasefillthemandatoryfields, AppResources.Information);
                        }
                        
                    }
                    
                    break;

                case EstablishmentSignUPTabEnum.ContactInformation:

                    if (IsAllValidContactDataEnteredEmail &&
                        IsAllValidContactDataEnteredMobileNbr&&
                        IsAllValidContactDataEnteredPhoneNbr)
                    {
                        PageTitle = AppResources.CRSummary;
                        BodyText = AppResources.CRReviewthebelowinformation;
                        NextBTN = AppResources.ZZZZContinue;
                        CurrentTab = EstablishmentSignUPTabEnum.MobileVerification;
                        //PageTitle = AppResources.VerificationCode;
                        //BodyText = AppResources.ZZPleaseenteraccessCode;
                        //NextBTN = AppResources.ZZZZContinue;
//                        CurrentTab = EstablishmentSignUPTabEnum.EmailVerification;
                        //StartTimer(0, 2, 0);
                        //TimerStart(numberOfSeconds);
                    }
                    else
                    {
                        _dialogService.ShowMessage(AppResources.ZZPleasefillthemandatoryfields, AppResources.Information);
                    }

                    break;

                case EstablishmentSignUPTabEnum.MobileVerification:
                    PageTitle = AppResources.VerificationCode;
                    BodyText = AppResources.ZZPleaseenteraccessCode;
                    NextBTN = AppResources.ZZZZContinue;
                    CurrentTab = EstablishmentSignUPTabEnum.EmailVerification;
                    //StartTimer(0, 2, 0);
                    //ResendOTPAsync();
                    TimerStart(numberOfSeconds);
                    break;

                    //case EstablishmentSignUPTabEnum.MobileVerification:
                    //    PageTitle = AppResources.Password;
                    //    BodyText = AppResources.CreateASecurePassword;
                    //    NextBTN = AppResources.Confirm;

                    //    CurrentTab = EstablishmentSignUPTabEnum.Password;
                    //    break;
            }
        }


        
        private void navigateBack()
        {
            switch (CurrentTab)
            {
                case EstablishmentSignUPTabEnum.TermsAndConditions:
                    _navigationService.GoBack();

                    break;

                case EstablishmentSignUPTabEnum.EmailVerification:
                                            PageTitle = AppResources.CRSummary;
                        BodyText = AppResources.CRReviewthebelowinformation;
                        NextBTN = AppResources.ZZZZContinue;
                        CurrentTab = EstablishmentSignUPTabEnum.MobileVerification;
                    break;

                case EstablishmentSignUPTabEnum.MobileVerification:
                    PageTitle = AppResources.ZZZContactInformation;
                    BodyText = AppResources.ZZZZCompletethebelowdetails;
                    CurrentTab = EstablishmentSignUPTabEnum.ContactInformation;
                    clearVerificationCodeFrom();
                    break;

                case EstablishmentSignUPTabEnum.ContactInformation:
                    PageTitle = AppResources.ZZZBusinessInformation;
                    BodyText = AppResources.ZZZZCompletethebelowdetails;
                    CurrentTab = EstablishmentSignUPTabEnum.BusinessInformation;
                    break;

                case EstablishmentSignUPTabEnum.BusinessInformation:
                    PageTitle = AppResources.ZZZIndividualInformation;
                    BodyText = AppResources.ZZZZCompletethebelowdetails;
                    CurrentTab = EstablishmentSignUPTabEnum.IndividualInformation;
                    break;

                case EstablishmentSignUPTabEnum.IndividualInformation:
                    PageTitle = AppResources.ZVatTermsAndConditions;
                    BodyText = "";
                    NextBTN = AppResources.ZZProceedtoindividualSignup;
                    CurrentTab = EstablishmentSignUPTabEnum.TermsAndConditions;
                    break;
            }
        }

        public void ClearData()
        {
            SetDefaultDate();
            VerifyButtonDisableColor = Color.FromHex("#d49504");
            //IsVerifyOTPEnabled = true;
            IsResendOTPEnabled = false;
            ButtonDisableColor = Color.Gray;
            IdNumber = string.Empty;
            Name = string.Empty;
            DOB = string.Empty;
            
            Email = string.Empty;
            //ConfirmEmail = string.Empty;
            MobileNumber = string.Empty;
            //Password = string.Empty;
            //ConfirmEmail = string.Empty;
            //CountryName = string.Empty;
            //CityName = string.Empty;
            //Region = string.Empty;
            Neighborhood = string.Empty;
            BuildingNumber = string.Empty;
            UnitNumber = string.Empty;
            //PostalCode = string.Empty;
            //BackArrowVisible = true;
            //ConfirmPassword = string.Empty;
            //DOBddyymm = string.Empty;

        }

        private void clearVerificationCodeFrom()
        {
            OTPFirstDigit = string.Empty;
            OTPSecondDigit = string.Empty;
            OTPThirdDigit = string.Empty;
            OTPFourthDigit = string.Empty;

            MOTPFirstDigit = string.Empty;
            MOTPSecondDigit = string.Empty;
            MOTPThirdDigit = string.Empty;
            MOTPFourthDigit = string.Empty;

            NewPassword = string.Empty;
            ConfirmPassword = string.Empty;
        }

        public async Task SetDefaultDate()
        {
            ObservableCollection<object> todaycollection = new ObservableCollection<object>();
            //Select today dates

            if (DateTime.Now.Date.Day < 10)
                todaycollection.Add("0" + DateTime.Now.Date.Day);
            else
                todaycollection.Add(DateTime.Now.Date.Day.ToString());
            if (DateTime.Now.Date.Month < 10)
                todaycollection.Add("0" + DateTime.Now.Date.Month);
            else
                todaycollection.Add(DateTime.Now.Date.Month.ToString());
            todaycollection.Add(DateTime.Now.Date.Year.ToString());
            TodayDate = todaycollection;
            DefaultMonth = DateTime.Now.Date.Month;
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

        //private void StartTimer(int h, int m, int sec)
        //{
        //    int hour = h;
        //    int mins = m;
        //    int counter = sec;
        //    Device.StartTimer(new TimeSpan(0, 0, 1), () =>
        //    {
        //        if (IsTimerCancel)
        //        {
        //            return false;
        //        }
        //        else
        //        {
        //            Device.BeginInvokeOnMainThread(() =>
        //            {
        //                counter = counter - 1;
        //                if (counter < 0)
        //                {
        //                    counter = 59;
        //                    mins = mins - 1;
        //                    if (mins < 0)
        //                    {
        //                        mins = 59;
        //                        hour = hour - 1;
        //                        if (hour < 0)
        //                        {
        //                            hour = 0;
        //                            mins = 0;
        //                            counter = 0;
        //                        }
        //                    }
        //                }
        //                IsResendOTPEnabled = false;
        //                LblCountDownTimer = string.Format("{0:00}:{1:00}", mins, counter);
        //            });
        //            if (hour == 0 && mins == 0 && counter == 0)
        //            {
        //                IsResendOTPEnabled = true;
        //                return false;
        //            }
        //            else
        //            {
        //                return true;
        //            }
        //        }
        //    });

        //}

        

        private async Task ResendOTPAsync()
        {
            OTPFirstDigit = string.Empty;
            OTPSecondDigit = string.Empty;
            OTPThirdDigit = string.Empty;
            OTPFourthDigit = string.Empty;

            MOTPFirstDigit = string.Empty;
            MOTPSecondDigit = string.Empty;
            MOTPThirdDigit = string.Empty;
            MOTPFourthDigit = string.Empty;

            try
            {
                await Task.Run(async () =>
                {
                    try
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
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                _dialogService.ShowMessage(SignupErrorModelRootObjectModel.error.innererror.errordetails[0].message, AppResources.Information);
                            });
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                _dialogService.ShowMessage(AppResources.ZZYournewEmailandSMSValidationCodehasbeenresenttoyou, AppResources.Information);
                            });
                            ButtonDisableColor = Color.FromHex("#9EA4A9");
                            ButtonDisableTextColor = Color.Gray;
                            VerifyButtonDisableColor = Color.FromHex("#006450");
                            VerifyButtonDisableTextColor = Color.White;
                            IsResendOTPEnabled = false;
                            IsVerifyOTPEnabled = true;
                            IsOTPEntryEnable = true;
                            numberOfSeconds = 120;
                            TimerStart(numberOfSeconds);
                        }
                    }
                    catch (InternetException ex)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        });
                    }
                });
            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
            }

        }



        #endregion

        #region new Methods


        public async Task OnPageLoad()
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                try
                {
                    //UpUsingList = null;
                    IsCRVisible = true;
                    IsLicenseVisible = false;
                    List<SignUpUsing> ListSignUpUsing = new List<SignUpUsing>();
                    ListSignUpUsing.Add(new SignUpUsing { ID = 1, SUType = AppResources.ZZNationalID });
                    ListSignUpUsing.Add(new SignUpUsing { ID = 2, SUType = AppResources.ZZIqamaID });
                    ListSignUpUsing.Add(new SignUpUsing { ID = 3, SUType = AppResources.ZZGCCID });
                    SignUpUsingList = ListSignUpUsing;
                    TxtIDType = AppResources.ZZNationalID;
                    SignUpUsing SignUpUsingM = new SignUpUsing();
                    SignUpUsingM.ID = 1;
                    SignUpUsingM.SUType = AppResources.ZZNationalID;
                    SelectedSignUpUsing = SignUpUsingM;
                    //LcTypeList = null;
                    //List<LicenseOrCRModel> LIstLcType = new List<LicenseOrCRModel>();
                    //LIstLcType.Add(new LicenseOrCRModel { ID = 1, LCType = AppResources.ZZLicenseNumber });
                    //LIstLcType.Add(new LicenseOrCRModel { ID = 2, LCType = AppResources.ZZCRNumber });
                    //LcTypeList = LIstLcType;
                    LicenseOrCRModel LicenseOrCRModelM = new LicenseOrCRModel();
                    LicenseOrCRModelM.ID = 2;
                    LicenseOrCRModelM.LCType = AppResources.ZZCRNumber;
                  //SelectLCType = LicenseOrCRModelM;
                                       //StringBuilder captcha = GetCaptcha();
                    //Captcha = captcha.ToString();
                  //IDTypeModelRootObject = null;
                    IDTypeIndex = 0;
                    //SelectedLOrC = 1;
                }
                catch (Exception ex)
                {
                }
                // PkrDBO = null;
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });
                    _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public async Task SetIssueIdList()
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });
            try
            {
                IssuedByList = null;
                List<IssuedByResponse> IssuedByResponseList = new List<IssuedByResponse>();
                var IssuedBy = await WebServiceManager.GAZTGetIssuedByList();
                IssuedByList = new List<IssuedByResponse>(IssuedBy);

            }
            catch (GAZTException gex)
            {
                // Handle the GAZT custom exception.
                string MessageForTheUser = gex.Message;
                if (gex is GAZTInvalidDataException)
                {
                    MessageForTheUser = AppResources.ZZSomethingwentwrong;
                }
                if (gex is GAZTNetworkConnectivityIssueException)
                {
                    MessageForTheUser = AppResources.NetworkConnectivityIssue;
                }
                else if (gex is GAZTInternetException)
                {
                    MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                }
                else if (gex is GAZTSessionExpiredException)
                {
                    MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                }

                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;

                    await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                    //_navigationService.GoBack();
                });
            }

            catch (HttpRequestException ex)
            {
                string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;

                    await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                    //_navigationService.GoBack();
                });
            }
            catch (Exception ex)
            {

                string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;

                    await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                    //_navigationService.GoBack();
                });
            }
            await Task.Run(() =>
            {
                IsLoading = false;
            });

        }

        public async Task SetCityList()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                IsLoading = true;

            });

            try
            {
                CityList = null;
                SignupCityRootObject CityListSignup = await WebServiceManager.GAZTGetCityListForSignup();
                List<SignupCityResult> CityR = new List<SignupCityResult>();
                IsCRChecked = true;
                IsLNChecked = false;
                CityR = CityListSignup.d.city_dropdownSet.results;
                CityList = CityR;
            }
            catch (GAZTException gex)
            {

                // Handle the GAZT custom exception.
                string MessageForTheUser = gex.Message;
                if (gex is GAZTInvalidDataException)
                {
                    MessageForTheUser = AppResources.ZZSomethingwentwrong;
                }
                if (gex is GAZTNetworkConnectivityIssueException)
                {
                    MessageForTheUser = AppResources.NetworkConnectivityIssue;
                }
                else if (gex is GAZTInternetException)
                {
                    MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                }
                else if (gex is GAZTSessionExpiredException)
                {
                    MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                }

                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;

                    await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                    //_navigationService.GoBack();
                });
            }
            catch (HttpRequestException ex)
            {
                string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;

                    await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                    //_navigationService.GoBack();
                });
            }
            catch (Exception ex)
            {

                string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;

                    await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                    //_navigationService.GoBack();
                });
            }
            Device.BeginInvokeOnMainThread(async () =>
            {
                IsLoading = false;

            });
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
                    else if (StopTimer)
                    {
                        return false;
                    }
                    else
                    {
                    }
                    if (TotalSec < 0)
                    {
                        OTPValidDuration = " 0:00";
                        ButtonDisableColor = Color.FromHex("#006450");
                        ButtonDisableTextColor = Color.White;
                        IsResendOTPEnabled = true;
                        VerifyButtonDisableColor = Color.FromHex("#9EA4A9");
                        VerifyButtonDisableTextColor = Color.Gray;
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
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        OTPValidDuration = " " + string.Format("{0:00}:{1:00}", _TimeSpan.Minutes, _TimeSpan.Seconds);
                    });
                    return true;
                }
            });
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
                string FinalSMSCode = MOTPFirstDigit+ MOTPSecondDigit + MOTPThirdDigit + MOTPFourthDigit;

                CreateModel.ASmsCode = FinalSMSCode;
                string FinalEMailCode = OTPFirstDigit + OTPSecondDigit + OTPThirdDigit + OTPFourthDigit;
                CreateModel.AEmailCode = FinalEMailCode;
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
                        _dialogService.ShowMessage(SignupErrorModelRootObjectModel.error.innererror.errordetails[0].message, AppResources.Information);

                    }
                    else
                    {
                        await Task.Run(() =>
                        {
                            IsLoading = false;
                        });
                        //est signup user created succesfully
                        _navigationService.NavigateTo(App.AccountCreatedSuccessfullyPageView);
                    }
                }
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
            }
        }
        #endregion

    }
}