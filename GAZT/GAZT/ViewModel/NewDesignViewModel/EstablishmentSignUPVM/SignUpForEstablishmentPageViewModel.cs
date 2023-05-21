using EGAZT.Models;
using EGAZT.Models.EnumModels;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
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
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel.EstablishmentSignUPVM
{
    [Preserve(AllMembers = true)]
    public class SignUpForEstablishmentPageViewModel : BaseViewModel
    {
        public int DefaultMonth;
        public int DefaultMonthHijri;
        public int countDownSeconds;
        public DateTime dateTime { get; set; }
        public int numberOfSeconds = 120;
        int TotalSec;
        public System.Timers.Timer otpTimer;
        public bool StopTimer = false;
        public VATSignUpData vATSignUpData { get; set; }
        public VATSignUpCaseId SignUpCaseIdD { get; set; }
        public bool IsAPICalledSuccessfully = true;

        #region Variable

        private EstablishmentSignUPTabEnum _currentTab = EstablishmentSignUPTabEnum.TermsAndConditions;
        public EstablishmentSignUPTabEnum CurrentTab
        {
            get => _currentTab;
            set
            {
                if (_currentTab == value) return;
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
                if (_txtConfirmPassword == value) return;

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
                if (_currenrIndex == value) return;

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
        public ObservableCollection<ChipModel> _chipDataFilterlist = null;
        public ObservableCollection<ChipModel> ChipDataFilterlist
        {
            get
            {
                return _chipDataFilterlist;
            }
            set
            {
                if (_chipDataFilterlist == value) return;

                _chipDataFilterlist = value;
                RaisePropertyChanged("ChipDataFilterlist");
            }
        }

      
        private string guid = string.Empty;
        public string Guid
        {
            get
            {
                return guid;
            }
            set
            {
                if (guid == value) return;

                guid = value;
                RaisePropertyChanged("Guid");
            }
        }
        private string _maxDigids = "9";
        public string MaxDigids
        {
            get
            {
                return _maxDigids;
            }
            set
            {
                if (_maxDigids == value) return;

                _maxDigids = value;
                RaisePropertyChanged("MaxDigids");
            }
        }
        private string _PickerDobToDisplay = string.Empty;
        public string PickerDobToDisplay
        {
            get
            {
                return _PickerDobToDisplay;
            }
            set
            {
                if (_PickerDobToDisplay == value) return;

                _PickerDobToDisplay = value;
                RaisePropertyChanged("PickerDobToDisplay");
            }
        }
        private bool _IsNextButtonEnable = true;
        public bool IsNextButtonEnable
        {
            get
            {
                return _IsNextButtonEnable;
            }
            set
            {
                if (_IsNextButtonEnable == value) return;

                _IsNextButtonEnable = value;
                RaisePropertyChanged("IsNextButtonEnable");
            }
        }
        private string _EncriptedMobileNumberforOtpscreen = "xxxxxxxx6494";
        public string EncriptedMobileNumberforOtpscreen
        {
            get
            {
                return _EncriptedMobileNumberforOtpscreen;
            }
            set
            {
                if (_EncriptedMobileNumberforOtpscreen == value) return;

                _EncriptedMobileNumberforOtpscreen = value;
                RaisePropertyChanged("EncriptedMobileNumberforOtpscreen");
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
                if (_txtLicenseNumber == value) return;

                _txtLicenseNumber = value;
                RaisePropertyChanged("TxtLicenseNumber");
            }
        }

        private string _selectedHijriDate = string.Empty;
        public string SelectedHijriDate
        {
            get
            {
                return _selectedHijriDate;
            }
            set
            {
                if (_selectedHijriDate == value) return;

                _selectedHijriDate = value;
                RaisePropertyChanged("SelectedHijriDate");
            }
        }

        private string _selectedGregDate = string.Empty;
        public string SelectedGregDate
        {
            get
            {
                return _selectedGregDate;
            }
            set
            {
                if (_selectedGregDate == value) return;

                _selectedGregDate = value;
                RaisePropertyChanged("SelectedGregDate");
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
                if (_issuedByTapped == value) return;

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
                if (_issuedByCityTapped == value) return;

                _issuedByCityTapped = value;
                RaisePropertyChanged("IssuedByCityTapped");
            }
        }
        private bool _LicenseOptionsVisible = false;
        public bool LicenseOptionsVisible
        {
            get
            {
                return _LicenseOptionsVisible;
            }
            set
            {
                if (_LicenseOptionsVisible == value) return;

                _LicenseOptionsVisible = value;
                RaisePropertyChanged("LicenseOptionsVisible");
            }
        }
        private bool _CROptionsVisible = true;
        public bool CROptionsVisible
        {
            get
            {
                return _CROptionsVisible;
            }
            set
            {
                if (_CROptionsVisible == value) return;

                _CROptionsVisible = value;
                RaisePropertyChanged("CROptionsVisible");
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
                if (_txtLOrCIssuedBy == value) return;

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
                if (_selectedIssuedBy == value) return;

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
                if (_selectedIssuedByPrev == value) return;

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
                if (_selectCityListPrev == value) return;

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
                if (_selectCityList == value) return;

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
                if (_txtLOrCIssuedByCity == value) return;

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
                if (_pkrDBOPrev == value) return;

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
                if (_pkrDBO == value) return;

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
                if (_isDeclarationCheckEnabled == value) return;

                _isDeclarationCheckEnabled = value;
                RaisePropertyChanged("IsDeclarationCheckEnabled");
            }
        }
        private bool _IsHijriCal = false;
        public bool IsHijriCal
        {
            get
            {
                return _IsHijriCal;
            }
            set
            {
                if (_IsHijriCal == value) return;

                _IsHijriCal = value;
                RaisePropertyChanged("IsHijriCal");
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
                if (_isDeclarationCheckedForInstruction == value) return;

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
                if (_isMainButtonEnabled == value) return;

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
                if (_PageTitle == value) return;

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
                if (_BodyText == value) return;

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
                if (_NextBTN == value) return;

                _NextBTN = value;
                RaisePropertyChanged("NextBTN");
            }
        }

        public string _ImgBackgroundYes = "vat_tile_listofsignup_W";
        public string ImgBackgroundYes
        {
            get
            {
                return _ImgBackgroundYes;
            }
            set
            {
                if (_ImgBackgroundYes == value) return;

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
                if (_ImgBackgroundNo == value) return;

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
                if (_ImgBackgroundCRNubmer == value) return;

                _ImgBackgroundCRNubmer = value;
                RaisePropertyChanged("ImgBackgroundCRNubmer");
            }
        }

        public string _ImgBackgroundLicenseNubmer = "FP_selected_tile";
        public string ImgBackgroundLicenseNubmer
        {
            get
            {
                return _ImgBackgroundLicenseNubmer;
            }
            set
            {
                if (_ImgBackgroundLicenseNubmer == value) return;

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
                if (_mOTPFirstDigit == value) return;

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
                if (_mOTPSecondDigit == value) return;

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
                if (_mOTPThirdDigit == value) return;

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
                if (_mOTPFourthDigit == value) return;

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
                if (_lblCountDownTimer == value) return;

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
                if (_isResendOTPEnabled == value) return;

                _isResendOTPEnabled = value;
                
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
                if (_isTimerCancel == value) return;

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
                if (_isPasswordEncripted == value) return;

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
                if (_isConfirmPasswordEncripted == value) return;

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
                if (_DOB == value) return;

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
                if (_selectedSignUpUsing == value) return;

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
                    catch (Exception ex)
                    {
                        Console.Write(ex.ToString());
                        Console.Write(ex.StackTrace.ToString());
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
                if (_txtName == value) return;

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
                if (_selectedIdType == value) return;

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
                    catch (Exception ex)
                    {
                        Console.Write(ex.ToString());
                        Console.Write(ex.StackTrace.ToString());
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
                if (_maxLengthID == value) return;

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
                if (_setStateListVisibility == value) return;

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
                if (_setCityListVisibility == value) return;

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
                if (_setCountryVisibility == value) return;

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
                if (_setGCCCountryVisibility == value) return;

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
                if (_txtIDType == value) return;

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
                if (_selectedGCCCountry == value) return;

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
                if (_gelectedGCCCountryIndex == value) return;

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
                if (_cityList == value) return;

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
                if (_setEnabilityToCountryList == value) return;

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
                if (_isOTPEntryEnable == value) return;

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
                if (_oTPValidDuration == value) return;

                _oTPValidDuration = value;
                if (_oTPValidDuration.Equals(" 00:00"))
                {
                    ButtonDisableColor =  (Color)Application.Current.Resources["Primary"];
                    ButtonDisableTextColor = Color.White;
                    IsResendOTPEnabled = true;
                    VerifyButtonDisableColor =  (Color)Application.Current.Resources["ButtonGray"];
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
                if (_isVerifyOTPEnabled == value) return;

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
                if (_verifybuttonDisableTextColor == value) return;

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
                if (_buttonDisableTextColor == value) return;

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
                if (_signUpFirstSubmitModel == value) return;

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
                if (_selectedRegion == value) return;

                _selectedRegion = value;
                if (_selectedRegion != null)
                {
                    if(CityList==null||CityList.Count==0)
                    _ = SetCityList();
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
                if (_selectedCity == value) return;

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
                if (_selectedCityIndex == value) return;

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
                if (_txtEmailAddress == value) return;

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
                if (_isTIN == value) return;

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
                if (_isTINVisible == value) return;

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
                if (_txtPhoneNumber == value) return;

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
                if (_txtMobileNumber == value) return;

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
                if (_txtCountryCode == value) return;

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
                if (_txtMobileNumberwithCountryCode == value) return;

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
                if (_idNumber == value) return;

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
                if (_name == value) return;

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
                if (_mobileCountryCode == value) return;

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
                if (_buildingNumber == value) return;

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
                if (_unitNumber == value) return;

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
                if (_neighborhood == value) return;

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
                if (_email == value) return;

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
                if (_mobileNumber == value) return;

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
                if (_encriptedMobileNumber == value) return;

                _encriptedMobileNumber = value;
                RaisePropertyChanged("EncriptedMobileNumber");
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
        private Color _verifybuttonDisableColor =  (Color)Application.Current.Resources["Secondary"];
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
        private ObservableCollection<object> _todayDate;
        public ObservableCollection<object> TodayDate
        {
            get
            {
                return _todayDate;
            }
            set
            {
                if (_todayDate == value) return;

                _todayDate = value;
                RaisePropertyChanged("TodayDate");
            }
        }  
        private ObservableCollection<object> _todayDateinHijri;
        public ObservableCollection<object> TodayDateinHijri
        {
            get
            {
                return _todayDateinHijri;
            }
            set
            {
                if (_todayDateinHijri == value) return;

                _todayDateinHijri = value;
                RaisePropertyChanged("TodayDateinHijri");
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
                if (_newPassword == value) return;

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
                if (_confirmPassword == value) return;

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
                if (_signUpUsingList == value) return;

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
                if (_iDTypeIndex == value) return;

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
                if (_issuedByList == value) return;

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
                if (_isCRVisible == value) return;

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
                if (_isLicenseVisible == value) return;

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
                if (_isCRChecked == value) return;

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
                if (_isLNChecked == value) return;

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
                if (_txtTIN == value) return;

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
                if (_txtIDNumber == value) return;

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
                if (_selectedSignUpUsingSetForCancle == value) return;

                _selectedSignUpUsingSetForCancle = value;
                RaisePropertyChanged("SelectedSignUpUsingSetForCancle");
            }
        }







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
        #endregion

        #region Constructor
        public SignUpForEstablishmentPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            OnNextButtonClick = new Command(() => navigateToNext());
            OnBackButtonClick = new Command(() => navigateBack());
            OnResendOTPClicked = new Command(async () => await ResendOTPAsync());
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
                        //  await PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleaseselecttermsandconditions));
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
                       //     _dialogService.ShowMessage(AppResources.ZZPleasefillthemandatoryfields, AppResources.Information);
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillthemandatoryfields));
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
                           // _dialogService.ShowMessage(AppResources.ZZPleasefillthemandatoryfields, AppResources.Information);
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillthemandatoryfields));
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
//                            //_dialogService.ShowMessage(AppResources.ZZPleasefillthemandatoryfields, AppResources.Information);
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillthemandatoryfields));
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
                       // _dialogService.ShowMessage(AppResources.ZZPleasefillthemandatoryfields, AppResources.Information);
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillthemandatoryfields));
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
                    IsNextButtonEnable = true; IsResendOTPEnabled = false;
                    try
                    {
                        otpTimer.Stop();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.Write(ex.StackTrace.ToString());
                    }
                    
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
            //SetDefaultDate();
            VerifyButtonDisableColor =  (Color)Application.Current.Resources["Secondary"];
            //IsVerifyOTPEnabled = true;
            IsResendOTPEnabled = false;
            ButtonDisableColor = Color.Gray;
            IdNumber = string.Empty;
            Name = string.Empty;
            DOB = string.Empty;
            IsNextButtonEnable = true; 
            IsResendOTPEnabled = false;
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

        public void SetDefaultDate()
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

            //TodayDateinHijri
            ObservableCollection<object> todaycollectionHijri = new ObservableCollection<object>();
            var calendar = new HijriCalendar();
            if (calendar.GetDayOfMonth(DateTime.Now.Date) < 10)
                todaycollectionHijri.Add("0" + calendar.GetDayOfMonth(DateTime.Now.Date).ToString());
            else
                todaycollectionHijri.Add(calendar.GetDayOfMonth(DateTime.Now.Date).ToString());
            if (calendar.GetMonth(DateTime.Now.Date) < 10)
                todaycollectionHijri.Add("0" + calendar.GetMonth(DateTime.Now.Date));
            else
                todaycollectionHijri.Add(calendar.GetMonth(DateTime.Now.Date).ToString());
            todaycollectionHijri.Add(calendar.GetYear(DateTime.Now.Date).ToString());
            TodayDateinHijri = todaycollectionHijri;
       //     DefaultMonthHijri = calendar.GetMonth(DateTime.Now.Date);


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

        public async Task GetCaptchAndGUID()
        {
            try
            {

                IsLoading = true;


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
                d.Application = "PUSR";

                forgotPasswordOTP.d = d;
                forgotPasswordOTP = await WebServiceManager.GAZTCaptchaAndGUID(forgotPasswordOTP);
                PopToRootPage();// If seesion Expired it will navigate to Dashboard page

                if (forgotPasswordOTP?.d != null && !string.IsNullOrEmpty(forgotPasswordOTP.d.Captcha))
                {
                    Guid = forgotPasswordOTP.d.Guid;
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

                    //SetIDNumberEnability = true;
                    //IDNumber = String.Empty;
                    // UserIDLayoutVisibility = true;
                });
            }
        }



        private async Task ResendOTPAsync()
        {
            if (IsResendOTPEnabled)
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
                                   await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupErrorModelRootObjectModel.error.innererror.errordetails[0].message));
                                });
                            }
                            else
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZYournewEmailandSMSValidationCodehasbeenresenttoyou));
                                });
                                ButtonDisableColor =  (Color)Application.Current.Resources["ButtonGray"];
                                ButtonDisableTextColor = Color.Gray;
                                VerifyButtonDisableColor =  (Color)Application.Current.Resources["Primary"];
                                VerifyButtonDisableTextColor = Color.White;
                                IsResendOTPEnabled = false;
                                IsVerifyOTPEnabled = true;
                                IsOTPEntryEnable = true;
                                StartOTPTimer();
                                IsNextButtonEnable = true;
                                IsResendOTPEnabled = false;
                            }
                        }
                        catch (InternetException ex)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                            });
                        }
                    });
                }
                catch (InternetException ex)
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                }
            }
            

        }

        public void PopulateDataInChips()
        {
            ChipDataFilterlist = new ObservableCollection<ChipModel>()
            {
                new ChipModel(){Text =AppResources.NDGregorian, TemplateType = AppResources.NDGregorian, ImageSource="Paid_check.png"},
                new ChipModel(){Text =AppResources.NDHijri, TemplateType = AppResources.NDHijri,ImageSource = "partially_clock.png"},
            };
        }

        #endregion

        #region new Methods


        public  void OnPageLoad()
        {
            try
            {
                
                    IsLoading = true;
                try
                {
                    IsCRVisible = true;
                    IsLicenseVisible = false;
                    SignUpUsingList = new List<SignUpUsing>();
                    List <SignUpUsing> ListSignUpUsing = new List<SignUpUsing>();
                    ListSignUpUsing.Add(new SignUpUsing { ID = 1, SUType = AppResources.ZZNationalID });
                    ListSignUpUsing.Add(new SignUpUsing { ID = 2, SUType = AppResources.ZZIqamaID });
                    ListSignUpUsing.Add(new SignUpUsing { ID = 3, SUType = AppResources.ZZGCCID });
                    SignUpUsingList = ListSignUpUsing;
                    TxtIDType = AppResources.ZZNationalID;
                    SignUpUsing SignUpUsingM = new SignUpUsing();
                    SignUpUsingM.ID = 1;
                    SignUpUsingM.SUType = AppResources.ZZNationalID;
                    SelectedSignUpUsing = SignUpUsingM;
                   
                    LicenseOrCRModel LicenseOrCRModelM = new LicenseOrCRModel();
                    LicenseOrCRModelM.ID = 2;
                    LicenseOrCRModelM.LCType = AppResources.ZZCRNumber;
                    IDTypeIndex = 0;
                    SelectedIssuedBy = null;
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    Console.Write(ex.StackTrace.ToString());
                }
                    IsLoading = false;
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                        IsLoading = false;
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                    _navigationService.GoBack();
                });
            }
        }
        private Dictionary<string, string> EnIssueBy = new Dictionary<string, string>()
        {
            {"",""},
            {"90701", "STC" },
            {"90702", "Ministry of Commerce and Industry" },
            {"90703", "Ministry of Health" },
            {"90704", "Ministry of Culture and Information" },
            {"90705", "Ministry of Agriculture" },
            {"90706", "Ministry of Municipal and Rural Affairs" },
            {"90707", "Ministry of Education" },
            {"90708", "Technical and Vocational Training Corporation" },
            {"90709", "Ministry of Labor" },
            {"90710", "Ministry of Islamic Affairs, Endowments, Da`wah, and Guidance" },
            {"90711", "Ministry of Hajj" },
            {"90712", "Saudi Arabia General Investment Authority" },
            {"90713", "Ministry of Water and Electricity" },
            {"90714", "Saudi Arabian Monetary Agency" },
            {"90715", "General Authority of Civil Aviation" },
            {"90716", "Ministry of Interior" },
            {"90717", "Ministry of Transportation" },
            {"90719", "Same Government Agency" },
            {"90720", "Ministry of Social Affairs" },
            {"90722", "Saudi Organization for Certified public Accountants? SOCPA" },
            {"90723", "Saudi Organization Tourism & National Heritage" },
            {"90725", "Ministry Of Justice" },
            {"90729", "Saudi Council of Engineers" },
            {"90721", "Municipality" },
            {"90724", "Ministry of Petroleum and Mineral Resources" },
            {"90740", "General Sports Authority" },
            {"90742","General Commission For Audiovisual Media" },
            {"90718", "Other" }
        };


        private Dictionary<string, string> ArIssueBy = new Dictionary<string, string>()
        {
  {"",""},

            {"90701", "شركة الاتصالات السعوديه" },
            {"90702", "وزارة التجارة والصناعة" },
            {"90703", "وزارة الصحة" },
            {"90704", "وزارة الثقافه والاعلام" },
            {"90705", "وزارة الزراعة" },
            {"90706", "وزارة الشؤون البلدية والقروية" },
            {"90707", "وزارة التربية والتعليم" },
            {"90708", "التعليم الفني والتدريب المهني" },
            {"90709", "وزارة العمل" },
            {"90710", "وزارة الشؤون الإسلامية والأوقاف والدعوة والإرشاد" },
            {"90711", "وزارة الحـج" },
            {"90712", "الهيئة العامه للاستثمار" },
            {"90713", "وزارة المياه والكهرباء" },
            {"90714", "مؤسسة النقد العربي السعودي" },
            {"90715", "الهيئة العامة للطيران المدني" },
            {"90716", "وزارة الداخلية" },
            {"90717", "وزارة النقل" },
            {"90719", "نفس الجهة الحكومية" },
            {"90720", "وزارة الشؤون الإجتماعية" },
            {"90722", "الهيئة السعودية للمحاسبين القانونيين" },
            {"90723", "الهيئة العامة للسياحة والتراث الوطني" },
            {"90725", "لدية العمار" },
            {"90729", "وزارة العدل" },
            {"90721", "الهيئة السعودية للمهندسين" },
            {"90724", "وزارة البترول والثروة المعدنية" },
              {"90742", "هيئة الإعلام المرئي والمسموع"},
            {"90718", "غير معرف" }

        };
        public void SetIssueIdList()
        {
                try
                {
                    IsLoading = true;
                    //Hardcoded List
                    IssuedByList = new List<IssuedByResponse>();
                    List<IssuedByResponse> IssuedByListTest = new List<IssuedByResponse>();

                    if (!App.IsArabic)
                    {
                        foreach (var item in EnIssueBy)
                        {
                            IssuedByListTest.Add(new IssuedByResponse() { mandt = " ", lang = "En", procsType = " ", elementCode = item.Key, txt50 = item.Value });

                        }
                    }
                    else
                    {

                        foreach (var item in ArIssueBy)
                        {
                            IssuedByListTest.Add(new IssuedByResponse() { mandt = " ", lang = "AR", procsType = " ", elementCode = item.Key, txt50 = item.Value });

                        }
                    }
                    IssuedByList = IssuedByListTest;
                }
                catch
                {

                }
            try
            {
                if (!App.IsArabic)
                {
                    foreach (var item in EnIssueBy)
                    {
                       
                    }
                }
                else
                {

                }
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
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                });
            }

            catch (HttpRequestException )
            {
                string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;

                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                });
            }
            catch (Exception )
            {

                string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;

                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                });
            }
           
                IsLoading = false;
            

        }




        public async Task SetCityList()
        {
                IsLoading = true;
            try
            {
                CityList = null;
                SignupCityRootObject CityListSignup = await WebServiceManager.GAZTGetCityListForSignup();
                List<SignupCityResult> CityR = new List<SignupCityResult>();
                if (CityListSignup.d.city_dropdownSet.results.Count == 0)
                   await App.Current.MainPage.DisplayAlert("no records","no rec","OK");
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

                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                });
            }
            catch (HttpRequestException)
            {
                string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
                string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;

                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                });
            }
            Device.BeginInvokeOnMainThread(() =>
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
                        ButtonDisableColor =  (Color)Application.Current.Resources["Primary"];
                        ButtonDisableTextColor = Color.White;
                        IsResendOTPEnabled = true;
                        VerifyButtonDisableColor =  (Color)Application.Current.Resources["ButtonGray"];
                        VerifyButtonDisableTextColor = Color.Gray;
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

        private string _txtMobileNumberCode = string.Empty;
        public string TxtMobileNumberCode
        {
            get
            {
                return _txtMobileNumberCode;
            }
            set
            {
                if (_txtMobileNumberCode == value) return;
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
                if (_txtPassword == value) return;

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
                if (_signUpModelRootObjectM == value) return;

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
                if (_txtEmailCode == value) return;

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
                       // _dialogService.ShowMessage(SignupErrorModelRootObjectModel.error.innererror.errordetails[0].message, AppResources.Information);
                      await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupErrorModelRootObjectModel.error.innererror.errordetails[0].message));
                        OTPFirstDigit = string.Empty;
                       OTPSecondDigit = string.Empty;
                        OTPThirdDigit = string.Empty;
                        OTPFourthDigit = string.Empty;

                        MOTPFirstDigit = string.Empty;
                        MOTPSecondDigit = string.Empty;
                        MOTPThirdDigit = string.Empty;
                        MOTPFourthDigit = string.Empty;

                    }
                    else
                    {
                        await Task.Run(() =>
                        {
                            IsLoading = false;
                        });
                        //est signup user created succesfully
                        string tin = string.Empty;
                        tin = ResultFirstSubmitModel.d.ATin;

                        _navigationService.NavigateTo(App.AccountCreatedSuccessfullyPageView, tin);
                    }
                }
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

                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                });
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                });
            }
            catch (HttpRequestException )
            {
                string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                Device.BeginInvokeOnMainThread(async () =>
                {
                  IsLoading = false;

                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                });
            }
            catch (Exception )
            {

                string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;

                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                  
                });
            }
        }



        private void OnCountDownTimedOTPEvent(object sender, ElapsedEventArgs e)
        {
            countDownSeconds--;

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
                //ContinueButtonEnability = false;
                IsResendOTPEnabled = true;
                IsNextButtonEnable = false;
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
        #endregion

    }
}