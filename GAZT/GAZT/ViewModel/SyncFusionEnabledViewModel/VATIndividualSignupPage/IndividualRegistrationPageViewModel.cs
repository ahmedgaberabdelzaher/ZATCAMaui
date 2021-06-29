//using CalendarView;
using EGAZT.Manager;
using EGAZT.Models;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using GalaSoft.MvvmLight;
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
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage
{
    [Preserve(AllMembers = true)]
    public class IndividualRegistrationPageViewModel : ViewModelBase
    {
        public int DefaultMonth;
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnContinueButtonClick { get; set; }
        public ICommand OnBackButtonClick { get; set; }
        public ICommand OnResendButtonClick { get; set; }
        //public ICommand DateSelectedCommand { get; set; }
        public ICommand GoButtonClick { get; set; }
        public int currentStep { get; set; }
        public VATSignUpData vATSignUpData { get; set; }
        public VATSignUpCaseId SignUpCaseIdD { get; set; }
        public VATSignUp _VATSignUp { get; set; }
        public DateTime dateTime { get; set; }
        public int numberOfSeconds = 120;
        public int TotalSec;
        public bool StopTimer = false;

        #region Variable

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
            }
        }
        public bool MarkComplete { get; private set; } = false;
        public int MaxIndex { get; private set; } = 5;
        #endregion

        #region Properties
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
        public string _MinEight = "error";
        public string MinEight
        {
            get
            {
                return _MinEight;
            }
            set
            {
                if (_MinEight == value) return;

                _MinEight = value;
                RaisePropertyChanged("MinEight");
            }
        }

        public string _CapsSmall = "error";
        public string CapsSmall
        {
            get
            {
                return _CapsSmall;
            }
            set
            {
                if (_CapsSmall == value) return;

                _CapsSmall = value;
                RaisePropertyChanged("CapsSmall");
            }
        }

        public string _MaxSixteen = "error";
        public string MaxSixteen
        {
            get
            {
                return _MaxSixteen;
            }
            set
            {
                if (_MaxSixteen == value) return;

                _MaxSixteen = value;
                RaisePropertyChanged("MaxSixteen");
            }
        }

        public string _NumSymbol = "error";
        public string NumSymbol
        {
            get
            {
                return _NumSymbol;
            }
            set
            {
                if (_NumSymbol == value) return;

                _NumSymbol = value;
                RaisePropertyChanged("NumSymbol");
            }
        }


        public string _dateselected;
        public string dateselected
        {
            get
            {
                return _dateselected;
            }
            set
            {
                if (_dateselected == value) return;

                _dateselected = value;
                RaisePropertyChanged("dateselected");
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
                RaisePropertyChanged("IsLoading");
            }
        }
        private bool _isOTPEncripted = true;
        public bool IsOTPEncripted
        {
            get
            {
                return _isOTPEncripted;
            }
            set
            {
                if (_isOTPEncripted == value) return;

                _isOTPEncripted = value;
                RaisePropertyChanged("IsOTPEncripted");
            }
        }
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

        private bool _framePasswordError = false;
        public bool FramePasswordError
        {
            get
            {
                return _framePasswordError;
            }
            set
            {
                if (_framePasswordError == value) return;

                _framePasswordError = value;
                RaisePropertyChanged("FramePasswordError");
            }
        }
        private bool _frameConfirmPasswordError = false;
        public bool FrameConfirmPasswordError
        {
            get
            {
                return _frameConfirmPasswordError;
            }
            set
            {
                if (_frameConfirmPasswordError == value) return;

                _frameConfirmPasswordError = value;
                RaisePropertyChanged("FrameConfirmPasswordError");
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
        private bool _frameOTPError = false;
        public bool FrameOTPError
        {
            get
            {
                return _frameOTPError;
            }
            set
            {
                if (_frameOTPError == value) return;

                _frameOTPError = value;
                RaisePropertyChanged("FrameOTPError");
            }
        }


        private string _txtCountryCode = string.Empty;
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
                    MobileNumber = string.Empty;
                }
                else
                {
                    MaxDigids = "15";
                }

                RaisePropertyChanged("TxtCountryCode");
            }
        }


        private bool _frameIDError = false;
        public bool FrameIDError
        {
            get
            {
                return _frameIDError;
            }
            set
            {
                if (_frameIDError == value) return;

                _frameIDError = value;
                RaisePropertyChanged("FrameIDError");
            }
        }
        private bool _frameDOBError = false;
        public bool FrameDOBError
        {
            get
            {
                return _frameDOBError;
            }
            set
            {
                if (_frameDOBError == value) return;

                _frameDOBError = value;
                RaisePropertyChanged("FrameDOBError");
            }
        }
        private bool _frameEmailError = false;
        public bool FrameEmailError
        {
            get
            {
                return _frameEmailError;
            }
            set
            {
                if (_frameEmailError == value) return;

                _frameEmailError = value;
                RaisePropertyChanged("FrameEmailError");
            }
        }
        private bool _frameConfirmEmailError = false;
        public bool FrameConfirmEmailError
        {
            get
            {
                return _frameConfirmEmailError;
            }
            set
            {
                if (_frameConfirmEmailError == value) return;

                _frameConfirmEmailError = value;
                RaisePropertyChanged("FrameConfirmEmailError");
            }
        }
        private bool _frameMobileNumberError = false;
        public bool FrameMobileNumberError
        {
            get
            {
                return _frameMobileNumberError;
            }
            set
            {
                if (_frameMobileNumberError == value) return;

                _frameMobileNumberError = value;
                RaisePropertyChanged("FrameMobileNumberError");
            }
        }
        private bool _frameNameError = false;
        public bool FrameNameError
        {
            get
            {
                return _frameNameError;
            }
            set
            {
                if (_frameNameError == value) return;

                _frameNameError = value;
                RaisePropertyChanged("FrameNameError");
            }
        }

        private bool _individualRegistrationView = true;
        public bool IndividualRegistrationView
        {
            get
            {
                return _individualRegistrationView;
            }
            set
            {
                if (_individualRegistrationView == value) return;

                _individualRegistrationView = value;
                if (_individualRegistrationView == true)
                {
                    currentStep = 1;
                    SetcolorForDots("IndividualRegistrationView");
                }
                RaisePropertyChanged("IndividualRegistrationView");
            }
        }

        private bool _nationalAddressView = false;
        public bool NationalAddressView
        {
            get
            {
                return _nationalAddressView;
            }
            set
            {
                if (_nationalAddressView == value) return;

                _nationalAddressView = value;
                if (_nationalAddressView == true)
                {
                    currentStep = 2;
                    SetcolorForDots("NationalAddressView");
                }

                RaisePropertyChanged("NationalAddressView");
            }
        }

        private bool _contactInformationView = false;
        public bool ContactInformationView
        {
            get
            {
                return _contactInformationView;
            }
            set
            {
                if (_contactInformationView == value) return;

                _contactInformationView = value;
                if (_contactInformationView == true)
                {
                    currentStep = 3;
                    SetcolorForDots("ContactInformationView");
                }
                RaisePropertyChanged("ContactInformationView");
            }
        }


        private bool _summeryView = false;
        public bool SummeryView
        {
            get
            {
                return _summeryView;
            }
            set
            {
                if (_summeryView == value) return;

                _summeryView = value;
                if (_summeryView == true)
                {
                    currentStep = 4;
                    SetcolorForDots("SummeryView");
                }
                RaisePropertyChanged("SummeryView");
            }
        }

        private bool _frameGccCountyError = false;
        public bool FrameGccCountyError
        {
            get
            {
                return _frameGccCountyError;
            }
            set
            {
                if (_frameGccCountyError == value) return;

                _frameGccCountyError = value;
                RaisePropertyChanged("FrameGccCountyError");
            }
        }
        private bool _frameCityError = false;
        public bool FrameCityError
        {
            get
            {
                return _frameCityError;
            }
            set
            {
                if (_frameCityError == value) return;

                _frameCityError = value;
                RaisePropertyChanged("FrameCityError");
            }
        }
        private bool _frameRegionError = false;
        public bool FrameRegionError
        {
            get
            {
                return _frameRegionError;
            }
            set
            {
                if (_frameRegionError == value) return;

                _frameRegionError = value;
                RaisePropertyChanged("FrameRegionError");
            }
        }

        private bool _passwordView = false;
        public bool PasswordView
        {
            get
            {
                return _passwordView;
            }
            set
            {
                if (_passwordView == value) return;

                _passwordView = value;
                if (_passwordView == true)
                {
                    currentStep = 5;
                    SetcolorForDots("PasswordView");
                    BackArrowVisible = false;
                }
                else
                {
                    BackArrowVisible = true;
                }
                RaisePropertyChanged("PasswordView");
            }
        }



        public Color _BoxColorOne = Color.FromHex("#DDDDDD");
        public Color BoxColorOne
        {
            get { return _BoxColorOne; }
            set
            {
                if (_BoxColorTwo == value) return;

                _BoxColorOne = value;
                RaisePropertyChanged("BoxColorOne");
            }
        }
        public Color _BoxColorTwo = Color.FromHex("#DDDDDD");
        public Color BoxColorTwo
        {
            get { return _BoxColorTwo; }
            set
            {
                if (_BoxColorTwo == value) return;

                _BoxColorTwo = value;
                RaisePropertyChanged("BoxColorTwo");
            }
        }
        public Color _BoxColorThree = Color.FromHex("#DDDDDD");
        public Color BoxColorThree
        {
            get { return _BoxColorThree; }
            set
            {
                if (_BoxColorThree == value) return;

                _BoxColorThree = value;
                RaisePropertyChanged("BoxColorThree");
            }
        }

        public Color _BoxColorFour = Color.FromHex("#DDDDDD");
        public Color BoxColorFour
        {
            get { return _BoxColorFour; }
            set
            {
                if (_BoxColorFour == value) return;

                _BoxColorFour = value;
                RaisePropertyChanged("BoxColorFour");
            }
        }

        public Color _BoxColorFive = Color.FromHex("#DDDDDD");
        public Color BoxColorFive
        {
            get { return _BoxColorFive; }
            set
            {
                if (_BoxColorFive == value) return;

                _BoxColorFive = value;
                RaisePropertyChanged("BoxColorFive");
            }
        }

        public List<SignUpIdType> _idTypeList;
        public List<SignUpIdType> IdTypeList
        {
            get
            {
                return _idTypeList;
            }
            set
            {
                if (_idTypeList == value) return;

                _idTypeList = value;
                RaisePropertyChanged("IdTypeList");
            }
        }

        public int _iDTypeIndex = 0;
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
        private string _DOBddyymm = string.Empty;
        public string DOBddyymm
        {
            get
            {
                return _DOBddyymm;
            }
            set
            {
                if (_DOBddyymm == value) return;

                _DOBddyymm = value;
                RaisePropertyChanged("DOBddyymm");
            }
        }


        private string _continueButtonText = string.Empty;
        public string ContinueButtonText
        {
            get
            {
                return _continueButtonText;
            }
            set
            {
                if (_continueButtonText == value) return;

                _continueButtonText = value;
                RaisePropertyChanged("ContinueButtonText");
            }
        }

        private string _DOBPrev = string.Empty;
        public string DOBPrev
        {
            get
            {
                return _DOBPrev;
            }
            set
            {
                if (_DOBPrev == value) return;

                _DOBPrev = value;
                RaisePropertyChanged("DOBPrev");
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
                    catch (Exception Ex)
                    {
                    }
                }
                RaisePropertyChanged("SelectedIdType");
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
        private string _confirmEmail = string.Empty;
        public string ConfirmEmail
        {
            get
            {
                return _confirmEmail;
            }
            set
            {
                if (_confirmEmail == value) return;

                _confirmEmail = value;
                RaisePropertyChanged("ConfirmEmail");
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

        private string _password = string.Empty;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (_password == value) return;

                _password = value;
                RaisePropertyChanged("Password");
            }
        }

        private string _oTP = string.Empty;
        public string OTP
        {
            get
            {
                return _oTP;
            }
            set
            {
                if (_oTP == value) return;

                _oTP = value;
                RaisePropertyChanged("OTP");
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




        public string _countryName;
        public string CountryName
        {
            get
            {
                return _countryName;
            }
            set
            {
                if (_countryName == value) return;

                _countryName = value;
                RaisePropertyChanged("CountryName");
            }
        }


        public IList<VATSignUpDataResults> _countryList;
        public IList<VATSignUpDataResults> CountryList
        {
            get
            {
                return _countryList;
            }
            set
            {
                if (_countryList == value) return;

                _countryList = value;
                RaisePropertyChanged("CountryList");
            }
        }

        private VATSignUpDataResults _selectedCountry = null;
        public VATSignUpDataResults SelectedCountry
        {
            get
            {
                return _selectedCountry;
            }
            set
            {
                if (_selectedCountry == value) return;

                _selectedCountry = value;

                RaisePropertyChanged("SelectedCountry");
            }
        }


        public int _selectedCountryIndex;
        public int SelectedCountryIndex
        {
            get
            {
                return _selectedCountryIndex;
            }
            set
            {
                if (_selectedCountryIndex == value) return;

                _selectedCountryIndex = value;
                RaisePropertyChanged("SelectedCountryIndex");
            }
        }

        public IList<VATSignUpGCC> _gCCCountryList;
        public IList<VATSignUpGCC> GCCCountryList
        {
            get
            {
                return _gCCCountryList;
            }
            set
            {
                if (_gCCCountryList == value) return;

                _gCCCountryList = value;
                RaisePropertyChanged("GCCCountryList");
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

        public int _selectedGCCCountryIndex;
        public int SelectedGCCCountryIndex
        {
            get
            {
                return _selectedGCCCountryIndex;
            }
            set
            {
                if (_selectedGCCCountryIndex == value) return;

                _selectedGCCCountryIndex = value;
                RaisePropertyChanged("SelectedGCCCountryIndex");
            }
        }

        public IList<VATSignUpStateResults> _regionList;
        public IList<VATSignUpStateResults> RegionList
        {
            get
            {
                return _regionList;
            }
            set
            {
                if (_regionList == value) return;

                _regionList = value;
                RaisePropertyChanged("RegionList");
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
                    SetCityList();
                }

                RaisePropertyChanged("SelectedRegion");
            }
        }

        public int _selectedRegionIndex;
        public int SelectedRegionIndex
        {
            get
            {
                return _selectedRegionIndex;
            }
            set
            {
                if (_selectedRegionIndex == value) return;

                _selectedRegionIndex = value;
                RaisePropertyChanged("SelectedRegionIndex");
            }
        }

        public string _region = "";
        public string Region
        {
            get
            {
                return _region;
            }
            set
            {
                if (_region == value) return;

                _region = value;
                RaisePropertyChanged("Region");
            }
        }

        public string _cityName = "";
        public string CityName
        {
            get
            {
                return _cityName;
            }
            set
            {
                if (_cityName == value) return;

                _cityName = value;
                RaisePropertyChanged("CityName");
            }
        }


        public IList<VATSignUPCityResults> _cityList;
        public IList<VATSignUPCityResults> CityList
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

        public string _postalCode = "";
        public string PostalCode
        {
            get
            {
                return _postalCode;
            }
            set
            {
                if (_postalCode == value) return;

                _postalCode = value;
                RaisePropertyChanged("PostalCode");
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
        private string _oTPValidDuration = "0:00";
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
                    ButtonDisableColor = Color.FromHex("#d49504");
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
                if (_isVerifyOTPEnabled == value) return;

                _isVerifyOTPEnabled = value;
                RaisePropertyChanged("IsVerifyOTPEnabled");
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
        private bool _backArrowVisible = true;
        public bool BackArrowVisible
        {
            get
            {
                return _backArrowVisible;
            }
            set
            {
                if (_backArrowVisible == value) return;

                _backArrowVisible = value;
                RaisePropertyChanged("BackArrowVisible");
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
        private Color _verifybuttonDisableColor = Color.FromHex("#d49504");
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
        private Color _buttonDisableColor = Color.FromHex("#9EA4A9");
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


        private string captcha = string.Empty;
        public string Captcha
        {
            get
            {
                return captcha;
            }
            set
            {
                if (captcha == value) return;

                captcha = value;
                RaisePropertyChanged("Captcha");
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
        public bool IsAPICalledSuccessfully = true;


        #endregion

        #region Constructor
        public IndividualRegistrationPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            _navigationService = navigationService;
            _dialogService = dialogService;
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }

            //this.DateSelectedCommand = new Command<DateSelectionArgs>(this.HandleDateSelected);

            OnContinueButtonClick = new Xamarin.Forms.Command(async () =>
            {
                if (IsVerifyOTPEnabled)
                {
                    await SetFormVisibility();
                }

            });
            OnResendButtonClick = new Xamarin.Forms.Command(async () =>
            {
                // ContinueButtonText = AppResources.ZZZZContinue;
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                if (IsResendOTPEnabled)
                {
                    await SetRequestObjectResendOtp();
                }
                await Task.Run(() =>
                {
                    IsLoading = false;
                });

            });

            OnBackButtonClick = new Xamarin.Forms.Command(() =>
                {
                    SetBackFormVisibility();
                });
            GoButtonClick = new Xamarin.Forms.Command(() =>
                 {
                     _navigationService.GoBack();
                 });
        }
        #endregion

        #region Method
        public void PopulateDataInChips()
        {
            ChipDataFilterlist = new ObservableCollection<ChipModel>()
            {
                new ChipModel(){Text =AppResources.NDGregorian, TemplateType = AppResources.NDGregorian, ImageSource="Paid_check.png"},
                new ChipModel(){Text =AppResources.NDHijri, TemplateType = AppResources.NDHijri,ImageSource = "partially_clock.png"},
            };
        }
        //private void HandleDateSelected(DateSelectionArgs obj)
        //{
        //    var obj1 = obj.SelectedDate;
        //    DOBddyymm = obj.SelectedDate.ToString();
        //    // App.Current.MainPage.DisplayAlert("Date Selected", "Selected Date = " + dateselected, "Ok");
        //}
        public void ClearData()
        {
            SetDefaultDate();
            VerifyButtonDisableColor = Color.FromHex("#d49504");
            IsVerifyOTPEnabled = true;
            IsResendOTPEnabled = false;
            ButtonDisableColor = Color.Gray;
            IdNumber = string.Empty;
            Name = string.Empty;
            DOB = string.Empty;
            Email = string.Empty;
            ConfirmEmail = string.Empty;
            MobileNumber = string.Empty;
            Password = string.Empty;
            ConfirmEmail = string.Empty;
            CountryName = string.Empty;
            CityName = string.Empty;
            Region = string.Empty;
            Neighborhood = string.Empty;
            BuildingNumber = string.Empty;
            UnitNumber = string.Empty;
            PostalCode = string.Empty;
            BackArrowVisible = true;
            ConfirmPassword = string.Empty;
            DOBddyymm = string.Empty;
            OTP = string.Empty;


        }
        public void SetBackFormVisibility()
        {
            try
            {
                if (currentStep == 1)
                {
                    _navigationService.GoBack();
                }
                else if (currentStep == 2)
                {
                    NationalAddressView = false;
                    IndividualRegistrationView = true;
                    //  currentStep--;
                }
                else if (currentStep == 3)
                {
                    ContactInformationView = false;
                    NationalAddressView = true;
                    //currentStep--;
                }
                else if (currentStep == 4)
                {
                    SummeryView = false;
                    ContactInformationView = true;
                    //currentStep--;

                }

            }
            catch
            { }
        }

        public async Task OnPageLoad()
        {
            // TimerStart(120);
            currentStep = 1;
            GetSignUpIdType();
            IsLoading = true;

            try
            {
                SignUpCaseIdD = await TaxEvasionWebServiceManager.GAZTGetVATSignUpCaseId();// working


                //string aaa = await WebServiceManager.GAZTVATSignUpValidateIDTypes("ZS0015", "1048089609", "19650224");
                //var dd = await WebServiceManager.GAZTGetVATSignUpCityListForSignup();
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


                IsLoading = false;

                //await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                _navigationService.GoBack();

            }
            catch (HttpRequestException ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    // IsLoading = false;

                    //await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                    //_navigationService.GoBack();
                });
            }


            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    //_dialogService.ShowMessage(ex.Message, AppResources.Information);
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                });
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    // IsLoading = false;

                    //await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));

                });
            }

            IsLoading = false;
        }

        public async Task SetFormVisibility()
        {
            try
            {
                if (currentStep == 1)
                {
                    ContinueButtonText = AppResources.ZZZZContinue;
                    await steponevalidation();


                }
                else if (currentStep == 2)
                {
                    await steptwovalidation();
                }
                else if (currentStep == 3)
                {
                    await stepthreevalidation();
                }
                else if (currentStep == 4)
                {
                    IsLoading = true;
                    await SetRequestObjectFirst();
                    ContinueButtonText = AppResources.ZZZZContinue;
                    IsLoading = false;
                }
                else if (currentStep == 5)
                {
                    ContinueButtonText = AppResources.ZZZZContinue;
                    IsLoading = true;
                    await StepfivedataValidation();
                    IsLoading = false;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public async Task stepthreevalidation()
        {
            string messageforuserr = string.Empty;
            bool showErrorMessage = false;
            if (string.IsNullOrEmpty(Email))
            {
                showErrorMessage = true;
                messageforuserr = AppResources.ZZPleasefillthemandatoryfields;
                FrameEmailError = true;
            }
            if (string.IsNullOrEmpty(ConfirmEmail))
            {
                showErrorMessage = true;
                messageforuserr = AppResources.ZZPleasefillthemandatoryfields;
                FrameConfirmEmailError = true;
            }
            if (string.IsNullOrEmpty(MobileNumber))
            {
                showErrorMessage = true;
                messageforuserr = AppResources.ZZPleasefillthemandatoryfields;
                FrameMobileNumberError = true;
            }
            if (!string.IsNullOrEmpty(ConfirmEmail) && !string.IsNullOrEmpty(Email))
            {
                if (Email.ToLower() != ConfirmEmail.ToLower())
                {

                    showErrorMessage = true;
                    messageforuserr = AppResources.ZZZZEmailandconfirmemailshouldmatchup;
                    FrameConfirmEmailError = true;
                }

            }
            if (showErrorMessage)
            {
                //_dialogService.ShowMessage(messageforuserr, AppResources.Information);
                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(messageforuserr));
            }
            else
            {
                ContactInformationView = false;
                SummeryView = true;
                CurrentIndex = 4;
                ContinueButtonText = AppResources.Confirm;
                // currentStep++;
            }


        }
        public async Task steptwovalidation()
        {
            bool showErrorMesage = false;
            if (SelectedIdType.ID.Equals("ZS0018"))
            {
                if (string.IsNullOrEmpty(CountryName))
                {
                    FrameGccCountyError = true;
                    showErrorMesage = true;
                }

            }
            else
            {
                if (string.IsNullOrEmpty(CountryName))
                {
                    FrameGccCountyError = true;
                    showErrorMesage = true;
                }

                if (string.IsNullOrEmpty(Region))
                {
                    FrameRegionError = true;
                    showErrorMesage = true;
                }
                if (string.IsNullOrEmpty(CityName))
                {
                    FrameCityError = true;
                    showErrorMesage = true;
                }

            }
            if (showErrorMesage)
            {
                //_dialogService.ShowMessage(AppResources.ZZPleasefillthemandatoryfields, AppResources.Information);
                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillthemandatoryfields));
            }
            else
            {
                NationalAddressView = false;
                ContactInformationView = true;
                CurrentIndex = 3;
                ContinueButtonText = AppResources.ZZZZContinue;
                //currentStep++;
            }
        }

        #endregion
        public async Task steponevalidation()
        {
            bool flag = true;
            if (SelectedIdType == null)
            {
                flag = false;
            }
            if (string.IsNullOrEmpty(IdNumber) || FrameIDError == true)
            {
                flag = false;
                FrameIDError = true;
            }
            if (string.IsNullOrEmpty(Name))
            {
                flag = false;
                FrameNameError = true;

            }
            if (string.IsNullOrEmpty(DOB) || FrameDOBError == true)
            {
                flag = false;
                FrameDOBError = true;

            }
            if (flag)
            {
                if (SelectedIdType != null)
                {
                    if (SelectedIdType.ID.Equals("ZS0018"))
                    {
                        IndividualRegistrationView = false;
                        NationalAddressView = true;
                        CurrentIndex = 2;
                        //  currentStep++;
                        flag = true;
                        SetVisibilityToNationalAddressContent();


                    }
                    else
                    {
                        bool isValidId = await ValidateId();
                        if (isValidId)
                        {
                            IndividualRegistrationView = false;
                            NationalAddressView = true;
                            //  currentStep++;
                            await Task.Run(() =>
                            {
                                IsLoading = true;
                            });
                            await Task.Run(async () =>
                            {
                                //IsLoading = true;

                                try
                                {
                                    vATSignUpData = await TaxEvasionWebServiceManager.GAZTGetVATSignUpCityListForSignup();


                                    //string aaa = await WebServiceManager.GAZTVATSignUpValidateIDTypes("ZS0015", "1048089609", "19650224");
                                    //var dd = await WebServiceManager.GAZTGetVATSignUpCityListForSignup();
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

                                        //await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                        _navigationService.GoBack();
                                    });
                                }
                                catch (HttpRequestException ex)
                                {
                                    Console.Write(ex.ToString());
                                    Console.Write(ex.StackTrace.ToString());
                                    string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        // IsLoading = false;

                                        //await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                        //_navigationService.GoBack();
                                    });
                                }


                                catch (InternetException ex)
                                {
                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        //_dialogService.ShowMessage(ex.Message, AppResources.Information);
                                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                                    });
                                }
                                catch (Exception ex)
                                {
                                    Console.Write(ex.ToString());
                                    Console.Write(ex.StackTrace.ToString());

                                    string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                                    Device.BeginInvokeOnMainThread(async () =>
                                    {
                                        // IsLoading = false;

                                        //await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));

                                    });
                                }
                                SetVisibilityToNationalAddressContent();
                            });
                            await Task.Run(() =>
                            {
                                IsLoading = false;
                            });


                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {

                                FrameIDError = true;
                                FrameDOBError = true;
                                //_dialogService.ShowMessageBox("Wrong Id", AppResources.ZError);
                                PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp("Wrong Id"));
                                //_dialogService.ShowMessage(AppResources.ZZPleasefillthemandatoryfields, AppResources.Information);

                            });


                        }
                    }
                }

            }
            else
            {
                //_dialogService.ShowMessage(AppResources.ZZPleasefillthemandatoryfields, AppResources.Information);
                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillthemandatoryfields));
            }
        }
        public async Task StepfivedataValidation()
        {
            StringBuilder PopMsg = new StringBuilder();
            bool IsAllValid = true;
            if (string.IsNullOrEmpty(OTP))
            {
                FrameOTPError = true;
                if (PopMsg.Length > 0)
                {
                    PopMsg.Append(Environment.NewLine);
                    PopMsg.Append(Environment.NewLine);
                    PopMsg.Append(AppResources.ZZPleaseenterconfirmationcodesenttoyourmobilenumber);
                }
                else
                {
                    PopMsg.Append(AppResources.ZZPleaseenterconfirmationcodesenttoyourmobilenumber);
                }
                IsAllValid = false;
            }
            else
            {
                FrameOTPError = false;
            }
            if (string.IsNullOrEmpty(Password))
            {
                FramePasswordError = true;
                if (PopMsg.Length > 0)
                {
                    PopMsg.Append(Environment.NewLine);
                    PopMsg.Append(Environment.NewLine);
                    PopMsg.Append(AppResources.ZZPasswordregulationsforSignup);
                }
                else
                {
                    PopMsg.Append(AppResources.ZZPasswordregulationsforSignup);
                }
                IsAllValid = false;
            }
            else
            {
                FramePasswordError = false;
                bool IsValidPass = UtilityManager.IsPasswordValid(Password);
                if (!IsValidPass)
                {
                    FramePasswordError = true;
                    if (PopMsg.Length > 0)
                    {
                        PopMsg.Append(Environment.NewLine);
                        PopMsg.Append(Environment.NewLine);
                        PopMsg.Append(AppResources.ZZPasswordregulationsforSignup);
                    }
                    else
                    {
                        PopMsg.Append(AppResources.ZZPasswordregulationsforSignup);
                    }
                    IsAllValid = false;
                }
                else
                {
                    FramePasswordError = false;
                    // frmPass.HasError = false;
                }
                if (Password != ConfirmPassword)
                {
                    FramePasswordError = true;
                    //frmPass.HasError = true;
                    if (PopMsg.Length > 0)
                    {
                        PopMsg.Append(Environment.NewLine);
                        PopMsg.Append(Environment.NewLine);
                        PopMsg.Append(AppResources.ZZNewpasswordfieldandconfirmPasswordfieldshouldmatchup);
                    }
                    else
                    {
                        PopMsg.Append(AppResources.ZZNewpasswordfieldandconfirmPasswordfieldshouldmatchup);
                    }
                    IsAllValid = false;
                }
                else
                {
                    FrameConfirmPasswordError = false;
                    //  frmCfrmPass.HasError = false;
                }
            }
            if (IsAllValid == true)
            {
                await SetRequestObjectFirst();
            }
            else
            {
                if (PopMsg.Length > 0)
                {
                    PopUp popUp = new PopUp();
                    popUp.Message = PopMsg.ToString();
                    popUp.IsLinkAvailable = false;
                    if (App.IsArabic)
                    {
                        popUp.FlowDirections = "RightToLeft";
                        popUp.isFontSet = true;
                    }
                    else
                    {
                        popUp.FlowDirections = "LeftToRight";
                    }
                    //PopupNavigation.Instance.PushAsync(new AddPopPageView(popUp));
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(PopMsg.ToString()));

                }
            }
        }



        public void GetSignUpIdType()
        {

            List<SignUpIdType> signUpIdTypeList = new List<SignUpIdType>{
           new SignUpIdType {ID = "ZS0015",Name = AppResources.NationaID},
                      new SignUpIdType {ID = "ZS0017",Name = AppResources.ZZIqamaID},
                                            new SignUpIdType {ID = "ZS0018",Name = AppResources.ZZGCCID},


            };
            List<SignUpIdType> lst = new List<SignUpIdType>();
            lst = signUpIdTypeList;
            IdTypeList = signUpIdTypeList;
            IDTypeIndex = 0;
            TxtIDType = AppResources.ZZNationalID;

            TxtIDType = IdTypeList[IDTypeIndex].Name;
            SelectedIdType = IdTypeList[IDTypeIndex];
        }
        /// <summary>
        /// Validate the National and Iqama ID
        /// </summary>
        /// 
        public async Task<string> ValidateIDs()
        {
            try
            {
                string dob = DOB.Replace("/", "");
                string resposne = await TaxEvasionWebServiceManager.GAZTVATSignUpValidateIDTypesStringResp(IdTypeList[IDTypeIndex].ID, IdNumber, dob);
                return resposne;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> ValidateId()
        {
            try
            {
                string dob = DOB.Replace("/", "");
                _VATSignUp = await TaxEvasionWebServiceManager.GAZTVATSignUpValidateIDTypes(IdTypeList[IDTypeIndex].ID, IdNumber, dob);
                if (_VATSignUp != null && _VATSignUp.d != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                return false;
            }
        }


        public void SetCountryList()
        {
            try
            {
                if (SelectedIdType.ID.Equals("ZS0018"))
                {
                    SetCountryVisibility = false;
                    SetGCCCountryVisibility = true;
                    GetVATSignUpGCCList();
                    //CountryList = vATSignUpData.d.country_dropdownSet.results;
                }
                else
                {
                    SetCountryVisibility = true;
                    SetGCCCountryVisibility = false;
                    SetEnabilityToCountryList = false;
                    CountryName = AppResources.ZZZZSaudiArabia;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());

            }



        }

        public void SetStateList()
        {
            try
            {
                if (!SelectedIdType.ID.Equals("ZS0018"))
                {
                    RegionList = vATSignUpData.d.State_dropdownSet.results;
                    RegionList = vATSignUpData.d.State_dropdownSet.results.Where(x => x.Land1 == "SA").ToList();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }


        }

        public void SetCityList()
        {
            try
            {
                if (!SelectedIdType.ID.Equals("ZS0018"))
                {

                    CityList = vATSignUpData.d.city_dropdownSet.results;
                    string selectedRegioncode = SelectedRegion.Bland;

                    CityList = vATSignUpData.d.city_dropdownSet.results.Where(x => x.Region == selectedRegioncode).ToList();
                    if (CityList != null)
                    {
                        CityList = CityList.Where(c => c.Country == "SA").ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }

        }

        public void SetVisibilityToNationalAddressContent()
        {
            try
            {
                if (SelectedIdType.ID.Equals("ZS0018"))
                {
                    SetStateListVisibility = false;
                    SetCityListVisibility = false;
                    SetEnabilityToCountryList = true;
                    SetCountryList();


                }
                else
                {
                    SetStateListVisibility = true;
                    SetCityListVisibility = true;
                    SetStateList();
                    CountryName = AppResources.ZZZZSaudiArabia;
                    SetEnabilityToCountryList = false;

                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }

        }

        public void GetCityList(string Bland)
        {

        }

        public void GetVATSignUpGCCList()
        {

            List<VATSignUpGCC> signUpIdTypeList = new List<VATSignUpGCC>{
           new VATSignUpGCC {CountryName = AppResources.ZZZZUAE,CountryCode="AE",CountryId="1"},
           new VATSignUpGCC {CountryName = AppResources.ZZZZBahrain,CountryCode="BH",CountryId="2"},
           new VATSignUpGCC {CountryName = AppResources.ZZZZKuwait,CountryCode="KW",CountryId="3"},
           new VATSignUpGCC {CountryName = AppResources.ZZZZOman,CountryCode="OM",CountryId="4"},
           new VATSignUpGCC {CountryName =AppResources.ZZZZQatar,CountryCode="QA",CountryId="5"},

            };
            List<VATSignUpGCC> lst = new List<VATSignUpGCC>();
            lst = signUpIdTypeList;
            GCCCountryList = lst;
        }

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
                d.Application = "VTIA";

                forgotPasswordOTP.d = d;
                forgotPasswordOTP = await WebServiceManager.GAZTCaptchaAndGUID(forgotPasswordOTP);

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


        public async Task SetRequestObjectFirst()
        {
            try
            {
                string[] date1 = DOB.Split('/');
                Int32 unixTimestamp = (Int32)(dateTime.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                var Bdt = string.Empty;
                if (IsHijriCal)
                {
                    string date = UtilityManager.HijriToGreg(date1[0] + "/" + date1[1] + "/" + date1[2]);
                    string[] SplitDate = date.Split('/');
                    Bdt = SplitDate[0] + "-" + SplitDate[1] + "-" + SplitDate[2] + "T00:00:00";
                }
                else
                {
                    Bdt = date1[0] + "-" + date1[1] + "-" + date1[2] + "T00:00:00";
                }

                string _City = string.Empty;
                string _Region = string.Empty;
                string _Country = string.Empty;
                if (SelectedIdType != null)
                {
                    if (SelectedIdType.ID.Equals("ZS0018"))
                    {
                        if (SelectedGCCCountry != null)
                        {
                            _Country = SelectedGCCCountry.CountryCode;

                        }
                    }
                    else
                    {
                        _Country = "SA";
                        _Region = SelectedRegion.Bland;
                        _City = _selectedCity.CityCode;

                    }
                }
                //TimeSpan span = (dateTime - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
                //string unixTime = span.TotalSeconds.ToString("N0");
                //unixTime = unixTime.Replace(",", "");
                // string dd = "" + "/Date(" + unixTime + ")/";// need to
                string submitValue;
                if (currentStep == 4)
                {
                    submitValue = "";
                }
                else
                {
                    submitValue = "X";
                }



                string newCountryCodeString = TxtCountryCode.Replace("+", "00");

                VATSignUpSubmit vATSignUpSubmit = new VATSignUpSubmit
                {
                    // {"Type":"1","IdType":"ZS0018","Idnumber":"11111111111","Firstname":"Ashish","Lastname":"Ranjan","PostCode1":"00000","City1":"","Country":"OM","Region":"","Building":" ","Floor":" ","Street":" ","Begda":"\/Date(1593139376000)\/","Endda":"\/Date(253402251010000)\/","Email":"ashish.ranjan@parallelminds.in","Mobile":"00966546825230","CaseGuid":"005056B1FE5D1EEAADC647121D569A67","Birthdt":"\/Date(1577846576000)\/","Password":"Init@1234","SmsCode":"6506","EmailCode":"","Submit":"X"}
                    Type = "1",
                    IdType = SelectedIdType.ID,//"ZS0018",
                    Idnumber = IdNumber,
                    Firstname = Name,
                    Lastname = string.Empty,
                    PostCode1 = "00000",
                    City1 = _City,
                    //Country = SelectedCountry.Land1,
                    //Region = SelectedRegion.Land1,
                    Region = _Region,
                    //Country = SelectedGCCCountry.CountryCode,
                    Country = _Country,
                    MobileCountry = MobileCountryCode,

                    Building = BuildingNumber,
                    Floor = UnitNumber,
                    Street = Neighborhood,
                    Begda = "/Date(1593139376000)/",
                    Endda = "/Date(253402251010000)/",
                    Email = Email,
                    Mobile = newCountryCodeString + MobileNumber,
                    //Mobile = "00966" + MobileNumber,
                    //CaseGuid = SignUpCaseIdD.d.results[0].CaseGuid,
                    CaseGuid = Guid,

                    Birthdt = Bdt,//"/Date(1577846576000)/",
                    //Birthdt = "" + "/Date(" + unixDateTime + ")/",//"/Date(1577846576000)/",
                    Password = Password,
                    SmsCode = OTP,
                    EmailCode = "",
                    Submit = submitValue,
                    



                    //Type = "1",
                    //IdType = SelectedIdType.ID,//"ZS0018",
                    //Idnumber = "11111112221",
                    //Firstname = "Ashish",
                    //Lastname = "Ranjan",
                    //PostCode1 = "00000",
                    //City1 = "",
                    //Country = "OM",
                    //Region = "",
                    //Building = "",
                    //Floor = "",
                    //Street = "",
                    //Begda = "/Date(1593139376000)/",
                    //Endda = "/Date(253402251010000)/",
                    //Email = "abc@gmail.com",
                    //Mobile = "00966546825230",
                    //CaseGuid = SignUpCaseIdD.d.results[0].CaseGuid,
                    //Birthdt = "/Date(1577846576000)/",
                    //Password = "",
                    //SmsCode = "",
                    //EmailCode = "",
                    //Submit = "",
                };

                //VATSignUpSubmit response = await WebServiceManager.GAZTCreateVATSignUp(vATSignUpSubmit);
                string response = await TaxEvasionWebServiceManager.GAZTCreateVATSignUpFirst(vATSignUpSubmit);

                VATSignUpSubmitResponse VatSignUpSubmitResponse = new VATSignUpSubmitResponse();
                VatSignUpSubmitResponse = JsonConvert.DeserializeObject<VATSignUpSubmitResponse>(response);
                if (VatSignUpSubmitResponse.d == null)
                {


                    SignupErrorModelRootObject SignupErrorModelRootObjectModel = JsonConvert.DeserializeObject<SignupErrorModelRootObject>(response);
                    StringBuilder Message = new StringBuilder();
                    foreach (SignupErrorModelErrordetail itemerror in SignupErrorModelRootObjectModel.error.innererror.errordetails)
                    {
                        if (itemerror.code.Contains("ZD_ZVTX/006"))
                        {

                            Message.AppendLine(AppResources.ZZZZErroMessage6);
                        }
                        if (itemerror.code.Contains("ZD_ZVTX/007"))
                        {

                            Message.AppendLine(AppResources.ZZZZErroMessage7);
                        }
                        if (itemerror.code.Contains("ZD_ZVTX/008"))
                        {

                            Message.AppendLine(AppResources.ZZZZErroMessage8);
                        }
                        if (itemerror.code.Contains("ZD_ZVTX/009"))
                        {

                            Message.AppendLine(AppResources.ZZZZErroMessage9);
                        }
                        if (itemerror.code.Contains("ZD_ZVTX/0010"))
                        {

                            Message.AppendLine(AppResources.ZZZZErroMessage10);
                        }
                        if (itemerror.code.Contains("ZD_ZVTX/0011"))
                        {

                            Message.AppendLine(AppResources.ZZZZErroMessage11);
                        }
                        if (itemerror.code.Contains("ZD_ZVTX/001"))
                        {

                            Message.AppendLine(AppResources.ZZZZErroMessage1);
                        }
                        if (itemerror.code.Contains("ZD_ZVTX/002"))
                        {

                            Message.AppendLine(AppResources.ZZZZErroMessage2);
                        }
                        if (itemerror.code.Contains("ZD_ZVTX/003"))
                        {

                            Message.AppendLine(AppResources.ZZZZErroMessage3);
                        }
                        if (itemerror.code.Contains("ZD_ZVTX/004"))
                        {

                            Message.AppendLine(AppResources.ZZZZErroMessage4);
                        }
                        if (itemerror.code.Contains("ZD_ZVTX/005"))
                        {

                            Message.AppendLine(AppResources.ZZZZErroMessage5);
                        }

                        if (itemerror.code.Contains("ZD_ZREG/303"))
                        {

                            Message.AppendLine(AppResources.ZZZZErrorMessage303);
                        }

                    }
                    OTP = string.Empty;

                    //_dialogService.ShowMessage(Message.ToString(), AppResources.Information);
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Message.ToString()));


                }
                else
                {
                    if (currentStep == 4)
                    {
                        SummeryView = false;
                        PasswordView = true;
                        CurrentIndex = 5;
                        int timeToExpireOTP = 120;
                        TimerStart(timeToExpireOTP);
                    }
                    else if (currentStep == 5)
                    {
                        PasswordView = false;
                        currentStep = 1;
                        string TinNumber = VatSignUpSubmitResponse.d.Tin;
                        _navigationService.NavigateTo(App.RegistrationSuccessfulPageView, TinNumber);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }

        public async Task SetRequestObjectResendOtp()
        {
            try
            {
                string[] date1 = DOB.Split('/');
                //var dateTime = new DateTime(year, month, day, 10, 2, 0, DateTimeKind.Local);
                //var dateTimeOffset = new DateTimeOffset(dateTime);
                //var unixDateTime = dateTimeOffset.ToUnixTimeSeconds();
                //var unixDateTime = dateTimeOffset.ToUnixTimeSeconds();
                Int32 unixTimestamp = (Int32)(dateTime.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                var Bdt = date1[0] + "-" + date1[1] + "-" + date1[2] + "T00:00:00";
                string _City = string.Empty;
                string _Region = string.Empty;
                string _Country = string.Empty;
                if (SelectedIdType != null)
                {
                    if (SelectedIdType.ID.Equals("ZS0018"))
                    {
                        if (SelectedGCCCountry != null)
                        {
                            _Country = SelectedGCCCountry.CountryCode;

                        }
                    }
                    else
                    {
                        _Country = "SA";
                        _Region = SelectedRegion.Bland;
                        _City = _selectedCity.CityCode;

                    }
                }
                string newCountryCodeString = TxtCountryCode.Replace("+", "00");

                string submitValue;
                submitValue = "";
                VATSignUpSubmit vATSignUpSubmit = new VATSignUpSubmit
                {
                    Type = "1",
                    IdType = SelectedIdType.ID,//"ZS0018",
                    Idnumber = IdNumber,
                    Firstname = Name,
                    Lastname = ".",
                    PostCode1 = "00000",
                    City1 = _City,
                    //Country = SelectedCountry.Land1,
                    //Region = SelectedRegion.Land1,
                    Region = _Region,
                    //Country = SelectedGCCCountry.CountryCode,
                    Country = _Country,
                    MobileCountry = MobileCountryCode,
                    //Building = BuildingNumber,
                    //Floor = "",
                    //Street = "",
                    Building = BuildingNumber,
                    Floor = UnitNumber,
                    Street = Neighborhood,
                    Begda = "/Date(1593139376000)/",
                    Endda = "/Date(253402251010000)/",
                    Email = Email,
                    Mobile = newCountryCodeString + MobileNumber,
                    //  Mobile = "00966" + MobileNumber,
                    //CaseGuid = SignUpCaseIdD.d.results[0].CaseGuid,
                    CaseGuid = Guid,
                    Birthdt = Bdt,//"/Date(1577846576000)/",
                    //Birthdt = "" + "/Date(" + unixDateTime + ")/",//"/Date(1577846576000)/",
                    Password = "",
                    SmsCode = "",
                    EmailCode = "",
                    Submit = submitValue,
                   
                };

                string response = await TaxEvasionWebServiceManager.GAZTCreateVATSignUpFirst(vATSignUpSubmit);
                if (response != null)
                {
                    int timeToExpireOTP = 120;
                    TimerStart(timeToExpireOTP);
                    ButtonDisableColor = Color.FromHex("#9EA4A9");//9EA4A9
                    IsResendOTPEnabled = false;
                    VerifyButtonDisableColor = Color.FromHex("#d49504");
                    IsVerifyOTPEnabled = true;
                    OTP = string.Empty;


                }
                VATSignUpSubmitResponse VatSignUpSubmitResponse = new VATSignUpSubmitResponse();
                VatSignUpSubmitResponse = JsonConvert.DeserializeObject<VATSignUpSubmitResponse>(response);
                if (VatSignUpSubmitResponse.d == null)
                {
                    SignupErrorModelRootObject SignupErrorModelRootObjectModel = JsonConvert.DeserializeObject<SignupErrorModelRootObject>(response);
                    StringBuilder Message = new StringBuilder();
                    foreach (SignupErrorModelErrordetail itemerror in SignupErrorModelRootObjectModel.error.innererror.errordetails)
                    {
                        if (itemerror.code.Contains("ZD_ZVTX/006"))
                        {

                            Message.AppendLine(AppResources.ZZZZErroMessage6);
                        }
                        if (itemerror.code.Contains("ZD_ZVTX/007"))
                        {

                            Message.AppendLine(AppResources.ZZZZErroMessage7);
                        }
                        if (itemerror.code.Contains("ZD_ZVTX/008"))
                        {

                            Message.AppendLine(AppResources.ZZZZErroMessage8);
                        }
                        if (itemerror.code.Contains("ZD_ZVTX/009"))
                        {

                            Message.AppendLine(AppResources.ZZZZErroMessage9);
                        }
                        if (itemerror.code.Contains("ZD_ZVTX/0010"))
                        {

                            Message.AppendLine(AppResources.ZZZZErroMessage10);
                        }
                        if (itemerror.code.Contains("ZD_ZVTX/0011"))
                        {

                            Message.AppendLine(AppResources.ZZZZErroMessage11);
                        }
                        if (itemerror.code.Contains("ZD_ZVTX/001"))
                        {

                            Message.AppendLine(AppResources.ZZZZErroMessage1);
                        }
                        if (itemerror.code.Contains("ZD_ZVTX/002"))
                        {

                            Message.AppendLine(AppResources.ZZZZErroMessage2);
                        }
                        if (itemerror.code.Contains("ZD_ZVTX/003"))
                        {

                            Message.AppendLine(AppResources.ZZZZErroMessage3);
                        }
                        if (itemerror.code.Contains("ZD_ZVTX/004"))
                        {

                            Message.AppendLine(AppResources.ZZZZErroMessage4);
                        }
                        if (itemerror.code.Contains("ZD_ZVTX/005"))
                        {

                            Message.AppendLine(AppResources.ZZZZErroMessage5);
                        }
                        if (itemerror.code.Contains("ZD_ZVTX/005"))
                        {

                            Message.AppendLine(AppResources.ZZZZErroMessage5);
                        }
                        if (itemerror.code.Contains("ZD_ZREG/303"))
                        {

                            Message.AppendLine(AppResources.ZZZZErrorMessage303);
                        }

                    }
                    //_dialogService.ShowMessage(Message.ToString(), AppResources.Information);
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(Message.ToString()));
                }
                else
                {



                }


            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }

        //public async Task SetRequestObject()
        //{

        //    try
        //    {
        //        //TimeSpan span = (dateTime - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
        //        //string unixTime = span.TotalSeconds.ToString("N0");
        //        //unixTime = unixTime.Replace(",", "");
        //        var date = DOB;
        //        var dateTime = new DateTime(2015, 05, 24, 10, 2, 0, DateTimeKind.Local);
        //        var dateTimeOffset = new DateTimeOffset(dateTime);
        //        var unixDateTime = dateTimeOffset.ToUnixTimeSeconds();
        //        string dd = "" + "/Date(" + unixDateTime + ")/";// need to
        //        string submitValue;
        //        if (currentStep == 4)
        //        {
        //            submitValue = "";
        //        }
        //        else
        //        {
        //            submitValue = "X";
        //        }





        //        VATSignUpSubmit vATSignUpSubmit = new VATSignUpSubmit
        //        {
        //            // {"Type":"1","IdType":"ZS0018","Idnumber":"11111111111","Firstname":"Ashish","Lastname":"Ranjan","PostCode1":"00000","City1":"","Country":"OM","Region":"","Building":" ","Floor":" ","Street":" ","Begda":"\/Date(1593139376000)\/","Endda":"\/Date(253402251010000)\/","Email":"ashish.ranjan@parallelminds.in","Mobile":"00966546825230","CaseGuid":"005056B1FE5D1EEAADC647121D569A67","Birthdt":"\/Date(1577846576000)\/","Password":"Init@1234","SmsCode":"6506","EmailCode":"","Submit":"X"}
        //            Type = "1",
        //            IdType = SelectedIdType.ID,//"ZS0018",
        //            Idnumber = IdNumber,
        //            Firstname = Name,
        //            Lastname = ".",
        //            PostCode1 = PostalCode,
        //            // City1 =CityName,
        //            City1 = " ",
        //            Country = SelectedGCCCountry.CountryCode,
        //            // Region = SelectedRegion.Land1,
        //            Region = " ",
        //            Building = BuildingNumber,
        //            Floor = "",
        //            Street = "",
        //            Begda = "/Date(1593139376000)/",
        //            Endda = "/Date(253402251010000)/",
        //            Email = Email,
        //            Mobile = "00966" + MobileNumber,
        //            CaseGuid = SignUpCaseIdD.d.results[0].CaseGuid,
        //            // Birthdt = "" + "/Date(" + unixTime + ")/",//"/Date(1577846576000)/",
        //            Birthdt = "" + "/Date(" + unixDateTime + ")/",//"/Date(1577846576000)/",
        //            Password = Password,
        //            SmsCode = OTP,
        //            EmailCode = "",
        //            Submit = submitValue,




        //            //Type = "1",
        //            //IdType = SelectedIdType.ID,//"ZS0018",
        //            //Idnumber = "11111112221",
        //            //Firstname = "Ashish",
        //            //Lastname = "Ranjan",
        //            //PostCode1 = "00000",
        //            //City1 = "",
        //            //Country = "OM",
        //            //Region = "",
        //            //Building = "",
        //            //Floor = "",
        //            //Street = "",
        //            //Begda = "/Date(1593139376000)/",
        //            //Endda = "/Date(253402251010000)/",
        //            //Email = "abc@gmail.com",
        //            //Mobile = "00966546825230",
        //            //CaseGuid = SignUpCaseIdD.d.results[0].CaseGuid,
        //            //Birthdt = "/Date(1577846576000)/",
        //            //Password = "",
        //            //SmsCode = "",
        //            //EmailCode = "",
        //            //Submit = "",
        //        };

        //       // VATSignUpSubmit response = await WebServiceManager.GAZTCreateVATSignUp(vATSignUpSubmit);

        //        string response = await WebServiceManager.GAZTCreateVATSignUpFirst(vATSignUpSubmit);
        //        VATSignUpSubmit vatSignUpSubmit = new VATSignUpSubmit();
        //        vatSignUpSubmit = JsonConvert.DeserializeObject<VATSignUpSubmit>(response);
        //        if (vatSignUpSubmit == null)
        //        {
        //            SignupErrorModelRootObject SignupErrorModelRootObjectModel = JsonConvert.DeserializeObject<SignupErrorModelRootObject>(response);
        //            StringBuilder Message = new StringBuilder();
        //            foreach (SignupErrorModelErrordetail itemerror in SignupErrorModelRootObjectModel.error.innererror.errordetails)
        //            {
        //                if (itemerror.code.Contains("ZD_PUSR"))
        //                {
        //                    if (Message.Length > 0)
        //                    {
        //                        Message.Append(Environment.NewLine);
        //                    }
        //                    Message.Append(itemerror.message);
        //                }
        //            }
        //            _dialogService.ShowMessage(Message.ToString(), AppResources.Information);
        //        }
        //        else
        //        {//success
        //         //_navigationService.NavigateTo(App.CreateGaztAccountPageView, ResultFirstSubmitModel);
        //            PasswordView = false;
        //            currentStep = 1;

        //            _navigationService.NavigateTo(App.RegistrationSuccessfulPageView);
        //        }
        //    }
        //    catch (Exception ex)
        //    { 

        //    }
        //}

        public void SetcolorForDots(string visiliblityItemName)
        {
            if (visiliblityItemName.Equals("IndividualRegistrationView"))

            {
                BoxColorOne = Color.FromHex("#006450");
                BoxColorTwo = Color.FromHex("#DDDDDD");
                BoxColorThree = Color.FromHex("#DDDDDD");
                BoxColorFour = Color.FromHex("#DDDDDD");
                BoxColorFive = Color.FromHex("#DDDDDD");
                CurrentIndex = 1;
            }
            else if (visiliblityItemName.Equals("NationalAddressView"))
            {
                BoxColorOne = Color.FromHex("#006450");
                BoxColorTwo = Color.FromHex("#006450");
                BoxColorThree = Color.FromHex("#DDDDDD");
                BoxColorFour = Color.FromHex("#DDDDDD");
                BoxColorFive = Color.FromHex("#DDDDDD");
                CurrentIndex = 2;
            }
            //ContactInformationView
            else if (visiliblityItemName.Equals("ContactInformationView"))
            {
                BoxColorOne = Color.FromHex("#006450");
                BoxColorTwo = Color.FromHex("#006450");
                BoxColorThree = Color.FromHex("#006450");
                BoxColorFour = Color.FromHex("#DDDDDD");
                BoxColorFive = Color.FromHex("#DDDDDD");
                CurrentIndex = 3;
            }
            else if (visiliblityItemName.Equals("SummeryView"))
            {
                BoxColorOne = Color.FromHex("#006450");
                BoxColorTwo = Color.FromHex("#006450");
                BoxColorThree = Color.FromHex("#006450");
                BoxColorFour = Color.FromHex("#006450");
                BoxColorFive = Color.FromHex("#DDDDDD");
                CurrentIndex = 4;
            }
            else if (visiliblityItemName.Equals("PasswordView"))
            {
                BoxColorOne = Color.FromHex("#006450");
                BoxColorTwo = Color.FromHex("#006450");
                BoxColorThree = Color.FromHex("#006450");
                BoxColorFour = Color.FromHex("#006450");
                BoxColorFive = Color.FromHex("#006450");
            }


        }
        public void TimerStart(int Seconds)
        {
            // IsVerifyOTPEnabled = true;
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
                        TimerStart(TotalSec);
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
                    //else if (!StopTimer)
                    //{
                    //    return false;
                    //}
                    //else
                    //{#006450 green 
                    //}#d49504 golden
                    if (TotalSec < 0)
                    {
                        OTPValidDuration = " 0:00";
                        ButtonDisableColor = Color.FromHex("#d49504");
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
    }
}
