using Newtonsoft.Json;
using Mopups.Services;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Windows.Input;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Core.Interfaces;
using static ZATCAMAUI.Models.ErrorMessage;
using Application = Microsoft.Maui.Controls.Application;
using static ZATCAMAUI.Models.LoginSSOModelERAD;
using ZATCAMAUI.Models.SignUP;
using ZATCAMAUI.Views.SyncFusionEnabledViews.VATIndividualSignupPage;
using Syncfusion.Maui.Picker;
using System.Text.RegularExpressions;
using ZATCAMAUI.Core.CustomControls;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage
{

    public class IndividualRegistrationPageViewModel : BaseViewModel
    {
        public int DefaultMonth;
        public ICommand OnContinueButtonClick { get; set; }
        public ICommand OnBackButtonClick { get; set; }
        public ICommand OnResendButtonClick { get; set; }
        public ICommand GoButtonClick { get; set; }
        public int currentStep { get; set; }
        public VATSignUpData vATSignUpData { get; set; }
        public VATSignUpCaseId SignUpCaseIdD { get; set; }
        public VATSignUp _VATSignUp { get; set; }
        public DateTime dateTime { get; set; }
        public int numberOfSeconds = 120;
        public int TotalSec;
        public bool StopTimer = false;
        private bool _isGulfER = false;
        public bool IsGulfER
        {
            get
            {
                return _isGulfER;
            }
            set
            {
                if (_isGulfER == value)
                {
                    return;
                }
                _isGulfER = value;
                OnPropertyChanged("IsGulfER");
            }
        }

        GenerateCaptchaGUID forgotPasswordOTP = new GenerateCaptchaGUID();

        private bool _passwordMainView = false;
        public bool passwordMainView
        {
            get
            {
                return _passwordMainView;
            }
            set
            {
                if (_passwordMainView == value)
                {
                    return;
                }
                _passwordMainView = value;
                OnPropertyChanged("passwordMainView");
            }
        }

        #region Variable

        private int _currenrIndex = 1;
        public int CurrentIndex
        {
            get => _currenrIndex;
            set
            {
                if (_currenrIndex == value) return;
                _currenrIndex = value;
                OnPropertyChanged(nameof(CurrentIndex));
                if (_currenrIndex == MaxIndex)
                {
                    MarkComplete = true;
                    OnPropertyChanged(nameof(MarkComplete));
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
                OnPropertyChanged("ChipDataFilterlist");
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
                OnPropertyChanged("MinEight");
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
                OnPropertyChanged("CapsSmall");
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
                OnPropertyChanged("MaxSixteen");
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
                OnPropertyChanged("NumSymbol");
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
                OnPropertyChanged("dateselected");
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
                OnPropertyChanged("TodayDate");
            }
        }
        //IsCitizen
        private bool _isCitizen = false;
        public bool IsCitizen
        {
            get
            {
                return _isCitizen;
            }
            set
            {
                if (_isCitizen == value) return;

                _isCitizen = value;
                OnPropertyChanged("IsCitizen");
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
                OnPropertyChanged("IsOTPEncripted");
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
                OnPropertyChanged("IsPasswordEncripted");
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
                OnPropertyChanged("MaxDigids");
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
                OnPropertyChanged("IsConfirmPasswordEncripted");
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
                OnPropertyChanged("FramePasswordError");
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
                OnPropertyChanged("FrameConfirmPasswordError");
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
                OnPropertyChanged("FrameOTPError");
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

                OnPropertyChanged("TxtCountryCode");
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
                OnPropertyChanged("FrameIDError");
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
                OnPropertyChanged("FrameDOBError");
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
                OnPropertyChanged("FrameEmailError");
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
                OnPropertyChanged("FrameConfirmEmailError");
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
                OnPropertyChanged("FrameMobileNumberError");
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
                OnPropertyChanged("FrameNameError");
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
                OnPropertyChanged("IndividualRegistrationView");
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

                OnPropertyChanged("NationalAddressView");
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
                OnPropertyChanged("ContactInformationView");
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
                OnPropertyChanged("SummeryView");
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
                OnPropertyChanged("FrameGccCountyError");
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
                OnPropertyChanged("FrameCityError");
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
                OnPropertyChanged("FrameRegionError");
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
                    oneStepBackArrowVisible = false;
                }
                OnPropertyChanged("PasswordView");
            }
        }



        public Color _BoxColorOne = (Color)Application.Current.Resources["Gray"];
        public Color BoxColorOne
        {
            get { return _BoxColorOne; }
            set
            {
                if (_BoxColorTwo == value) return;

                _BoxColorOne = value;
                OnPropertyChanged("BoxColorOne");
            }
        }
        public Color _BoxColorTwo = (Color)Application.Current.Resources["Gray"];
        public Color BoxColorTwo
        {
            get { return _BoxColorTwo; }
            set
            {
                if (_BoxColorTwo == value) return;

                _BoxColorTwo = value;
                OnPropertyChanged("BoxColorTwo");
            }
        }
        public Color _BoxColorThree = (Color)Application.Current.Resources["Gray"];
        public Color BoxColorThree
        {
            get { return _BoxColorThree; }
            set
            {
                if (_BoxColorThree == value) return;

                _BoxColorThree = value;
                OnPropertyChanged("BoxColorThree");
            }
        }

        public Color _BoxColorFour = (Color)Application.Current.Resources["Gray"];
        public Color BoxColorFour
        {
            get { return _BoxColorFour; }
            set
            {
                if (_BoxColorFour == value) return;

                _BoxColorFour = value;
                OnPropertyChanged("BoxColorFour");
            }
        }

        public Color _BoxColorFive = (Color)Application.Current.Resources["Gray"];
        public Color BoxColorFive
        {
            get { return _BoxColorFive; }
            set
            {
                if (_BoxColorFive == value) return;

                _BoxColorFive = value;
                OnPropertyChanged("BoxColorFive");
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
                OnPropertyChanged("IdTypeList");
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
                OnPropertyChanged("IDTypeIndex");
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
                OnPropertyChanged("IdNumber");
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
                OnPropertyChanged("DOB");
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
                OnPropertyChanged("DOBddyymm");
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
                OnPropertyChanged("ContinueButtonText");
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
                OnPropertyChanged("DOBPrev");
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
                OnPropertyChanged("MaxLengthID");
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
                OnPropertyChanged("TxtIDType");
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
                OnPropertyChanged("IsHijriCal");
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


                    }
                }
                OnPropertyChanged("SelectedIdType");
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
                OnPropertyChanged("Name");
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
                OnPropertyChanged("Email");
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
                OnPropertyChanged("ConfirmEmail");
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
                OnPropertyChanged("MobileNumber");
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
                OnPropertyChanged("MobileCountryCode");
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
                OnPropertyChanged("EncriptedMobileNumber");
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
                OnPropertyChanged("Password");
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
                OnPropertyChanged("OTP");
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
                OnPropertyChanged("ConfirmPassword");
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
                OnPropertyChanged("CountryName");
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
                OnPropertyChanged("CountryList");
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

                OnPropertyChanged("SelectedCountry");
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
                OnPropertyChanged("SelectedCountryIndex");
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
                OnPropertyChanged("GCCCountryList");
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

                OnPropertyChanged("SelectedGCCCountry");
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
                OnPropertyChanged("SelectedGCCCountryIndex");
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
                OnPropertyChanged("RegionList");
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

                OnPropertyChanged("SelectedRegion");
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
                OnPropertyChanged("SelectedRegionIndex");
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
                OnPropertyChanged("Region");
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
                OnPropertyChanged("CityName");
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
                OnPropertyChanged("CityList");
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

                OnPropertyChanged("SelectedCity");
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
                OnPropertyChanged("SelectedCityIndex");
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
                OnPropertyChanged("SetCityListVisibility");
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
                OnPropertyChanged("SetEnabilityToCountryList");
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
                OnPropertyChanged("SetStateListVisibility");
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
                OnPropertyChanged("Neighborhood");
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
                OnPropertyChanged("PostalCode");
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
                OnPropertyChanged("BuildingNumber");
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
                OnPropertyChanged("UnitNumber");
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
                OnPropertyChanged("SetGCCCountryVisibility");
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
                OnPropertyChanged("SetCountryVisibility");
            }
        }
        private string _oTPValidDuration = "";
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
                    ButtonDisableColor = (Color)Application.Current.Resources["Secondary"];
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
                OnPropertyChanged("IsVerifyOTPEnabled");
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
                if (_buttonDisableTextColor == value) return;

                _buttonDisableTextColor = value;
                OnPropertyChanged("ButtonDisableTextColor");
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
                if (_verifybuttonDisableTextColor == value) return;

                _verifybuttonDisableTextColor = value;
                OnPropertyChanged("VerifyButtonDisableTextColor");
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
                OnPropertyChanged("IsResendOTPEnabled");
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
                OnPropertyChanged("BackArrowVisible");
            }
        }

        private bool _oneStepBackArrowVisible = true;
        public bool oneStepBackArrowVisible
        {
            get
            {
                return _oneStepBackArrowVisible;
            }
            set
            {
                if (_oneStepBackArrowVisible == value) return;

                _oneStepBackArrowVisible = value;
                OnPropertyChanged("oneStepBackArrowVisible");
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
                OnPropertyChanged(nameof(IsOTPEntryEnable));
            }
        }


        //6094 Nafath
        private LoginSSOModelClassERAD _ModelSSOID;
        public LoginSSOModelClassERAD modelSSOID
        {
            get
            {
                return _ModelSSOID;
            }
            set
            {
                if (_ModelSSOID == value) return;

                _ModelSSOID = value;
                OnPropertyChanged(nameof(modelSSOID));
            }
        }

        private Color _verifybuttonDisableColor = (Color)Application.Current.Resources["Secondary"];
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
                OnPropertyChanged("Captcha");
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
                OnPropertyChanged("Guid");
            }
        }
        public bool IsAPICalledSuccessfully = true;
        private object date1DOB;

        private bool _titleVisibility = false;
        public bool TitleVisibility
        {
            get
            {
                return _titleVisibility;
            }
            set
            {
                if (_titleVisibility == value) return;

                _titleVisibility = value;
                OnPropertyChanged("TitleVisibility");
            }
        }

        private string _iqamaTypeDesc = string.Empty;
        public string IqamaTypeDesc
        {
            get
            {
                return _iqamaTypeDesc;
            }
            set
            {
                _iqamaTypeDesc = value;
                OnPropertyChanged("IqamaTypeDesc");
            }
        }

        private bool _showIqamaTypeDesc = false;
        public bool ShowIqamaTypeDesc
        {
            get
            {
                return _showIqamaTypeDesc;
            }
            set
            {
                _showIqamaTypeDesc = value;
                OnPropertyChanged("ShowIqamaTypeDesc");
            }
        }

        private bool _frameCaptchaError = false;
        public bool FrameCaptchaError
        {
            get
            {
                return _frameCaptchaError;
            }
            set
            {
                if (_frameCaptchaError == value) return;

                _frameCaptchaError = value;
                OnPropertyChanged("FrameCaptchaError");
            }
        }

        private string mobileCountry = string.Empty;
        public string MobileCountry
        {
            get
            {
                return mobileCountry;
            }
            set
            {
                if (mobileCountry == value) return;

                mobileCountry = value;
                OnPropertyChanged("MobileCountry");
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

        private string _lgid = null;
        public string LgId
        {
            get
            {
                return _lgid;
            }
            set
            {
                _lgid = value;
                OnPropertyChanged("LgId");
            }
        }

        private string _birthdate = null;
        public string BirthDate
        {
            get
            {
                return _birthdate;
            }
            set
            {
                _birthdate = value;
                OnPropertyChanged("BirthDate");
            }
        }

        private string _begindate = null;
        public string Begindate
        {
            get
            {
                return _begindate;
            }
            set
            {
                _begindate = value;
                OnPropertyChanged("Begindate");
            }
        }

        private string _enddate = null;
        public string EndDate
        {
            get
            {
                return _enddate;
            }
            set
            {
                _enddate = value;
                OnPropertyChanged("EndDate");
            }
        }


        private string _postCode = null;
        public string PostCode
        {
            get
            {
                return _postCode;
            }
            set
            {
                _postCode = value;
                OnPropertyChanged("PostCode");
            }
        }

        bool issEntryNameEnabled;
        public bool IsEntryNameEnabled { get { return issEntryNameEnabled; } set { issEntryNameEnabled = value; OnPropertyChanged(); } }

        string idType;
        public string IdType { get { return idType; } set { idType = value; OnPropertyChanged(); } }

        string idnumber;
        public string Idnumber { get { return idnumber; } set { idnumber = value; OnPropertyChanged(); } }

        string firstname;
        public string Firstname { get { return firstname; } set { firstname = value; OnPropertyChanged(); } }

        string birthdt;
        public string Birthdt { get { return birthdt; } set { birthdt = value; OnPropertyChanged(); } }

        string imageSeePasswordSource = "hidePassword.png";
        public string ImageSeePasswordSource { get { return imageSeePasswordSource; } set { imageSeePasswordSource = value; OnPropertyChanged(); } }

        string imageSeeConfirmPasswordSource = "hidePassword.png";
        public string ImageSeeConfirmPasswordSource { get { return imageSeeConfirmPasswordSource; } set { imageSeeConfirmPasswordSource = value; OnPropertyChanged(); } }

        ChipModel chipGroupStatusFilterSelectedItem;
        public ChipModel ChipGroupStatusFilterSelectedItem { get { return chipGroupStatusFilterSelectedItem; } set { chipGroupStatusFilterSelectedItem = value; OnPropertyChanged(); } }

        ObservableCollection<InternationalMobileData> mobileData = null;
        #endregion

        #region Constructor
        public IndividualRegistrationPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

            OnContinueButtonClick = new Command(async () =>
            {
                if (IsVerifyOTPEnabled)
                {
                    await SetFormVisibility();
                }

            });
            OnResendButtonClick = new Command(async () =>
            {
                if (IsResendOTPEnabled)
                {
                    await Task.Run(() =>
                    {
                        IsLoading = true;
                    });
                    await SetRequestObjectResendOtp();
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });
                }
            });

            OnBackButtonClick = new Command(() =>
            {
                SetBackFormVisibility();
            });
        }
        #endregion

        public ICommand OnAppearingIndividualRegistrationCommand
        {

            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        IsLoading = true;
                        await OnPageLoad();

                        ClearData();


                        IndividualRegistrationView = true;
                        TxtCountryCode = "+966";

                        currentStep = 1;
                        NationalAddressView = false;
                        ContactInformationView = false;
                        SummeryView = false;
                        PasswordView = false;
                        ContinueButtonText = AppResources.ZZZZContinue;
                        PopulateDataInChips();
                        ChipGroupStatusFilterSelectedItem = ChipDataFilterlist.Where(x => x.TemplateType == AppResources.NDGregorian).FirstOrDefault();
                        IsHijriCal = false;
                        DOBddyymm = string.Empty;
                        DOB = string.Empty;

                        await GetCaptchImage();

                        IdType = string.Empty;
                        Idnumber = string.Empty;
                        Birthdt = string.Empty;
                        Firstname = string.Empty;

                        if (IsCitizen)
                        {

                            modelSSOID = await WebServiceManager.LoginDataSSO();
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                if (modelSSOID.results[0].IdType == "ZS0015")
                                {
                                    IdType = AppResources.NationaID;
                                }
                                else
                                {
                                    IdType = AppResources.VFCIqamaID;

                                    if (modelSSOID.results[0].AIqamaType?.Length > 0)
                                    {
                                        IqamaTypeDesc = modelSSOID.results[0].AIqamaDesc ?? "";
                                        ShowIqamaTypeDesc = true;
                                    }
                                    else
                                    {
                                        IqamaTypeDesc = "";
                                        ShowIqamaTypeDesc = false;
                                    }
                                }

                                Idnumber = modelSSOID.results[0].Idnumber;
                                Birthdt = UtilityManager.StringToDDMMYYYYFormat(modelSSOID.results[0].Birthdt);
                                DOBddyymm = UtilityManager.StringToDDMMYYYYFormat(modelSSOID.results[0].Birthdt);
                                Firstname = modelSSOID.results[0].Firstname;
                                Begindate = UtilityManager.StringToDDMMYYYYFormat(modelSSOID.results[0].Begda);
                                EndDate = UtilityManager.StringToDDMMYYYYFormat(modelSSOID.results[0].Endda);
                                PostCode = modelSSOID.results[0].PostCode1;
                                BirthDate = modelSSOID.results[0].Birthdt;
                                await Task.Run(() =>
                                {
                                    IsLoading = false;
                                });
                            });
                        }



                        MessagingCenter.Subscribe<InternationalCodeSearchPage, string>(this, "SelectedItem", (sender, arg) =>
                        {
                            TxtCountryCode = arg;
                        });
                        MessagingCenter.Subscribe<InternationalCodeSearchPage, string>(this, "SelectedCountryCode", (sender, arg) =>
                        {

                            MobileCountryCode = arg;
                        });

                        mobileData = await WebServiceManager.GAZTGetMobileRegionDropdown();
                        MobileCountry = mobileData.Where(x => x.Telefto == TxtCountryCode).FirstOrDefault().Land1;
                        if (currentStep == 5 && App.IsComingFromSleepMode)
                        {

                            int timeToExpireOTP = 120;
                            TimerStart(timeToExpireOTP);
                        }
                        IsLoading = false;
                    }
                    catch (Exception)
                    {
                        IsLoading = false;
                    }

                });
            }
        }

        public ICommand ConfirmPasswordTextChangedCommand
        {

            get
            {
                return new Command(() =>
                {
                    if (!string.IsNullOrEmpty(ConfirmPassword))
                    {
                        if (Password != ConfirmPassword)
                        {
                            FrameConfirmPasswordError = true;
                        }
                        else
                        {
                            FrameConfirmPasswordError = false;
                        }
                    }

                });
            }
        }

        public ICommand ImageSeePasswordCommand
        {

            get
            {
                return new Command(() =>
                {
                    if (IsPasswordEncripted)
                    {
                        IsPasswordEncripted = false;
                        ImageSeePasswordSource = "showPassword";
                    }
                    else
                    {
                        IsPasswordEncripted = true;
                        ImageSeePasswordSource = "hidePassword";
                    }

                });
            }
        }

        public ICommand ImageSeeConfirmPasswordCommand
        {

            get
            {
                return new Command(() =>
                {
                    if (IsConfirmPasswordEncripted)
                    {
                        IsConfirmPasswordEncripted = false;
                        ImageSeeConfirmPasswordSource = "showPassword";
                    }
                    else
                    {
                        IsConfirmPasswordEncripted = true;
                        ImageSeeConfirmPasswordSource = "hidePassword";
                    }
                });
            }
        }

        public ICommand GccCountryNameTextChangedCommand
        {

            get
            {
                return new Command(() =>
                {
                    if (!string.IsNullOrEmpty(CountryName))
                    {
                        FrameGccCountyError = false;
                    }
                });
            }
        }

        public ICommand EntryRegionTextChangedCommand
        {

            get
            {
                return new Command(() =>
                {
                    if (!string.IsNullOrEmpty(Region))
                    {
                        FrameRegionError = false;
                    }
                });
            }
        }

        public ICommand EntryCityTextChangedCommand
        {

            get
            {
                return new Command(() =>
                {
                    if (!string.IsNullOrEmpty(CityName))
                    {
                        FrameCityError = false;
                    }
                });
            }
        }

        public ICommand DateEntryTextChangedCommand
        {

            get
            {
                return new Command(() =>
                {
                    FrameDOBError = false;
                });
            }
        }

        public ICommand EntryNameTextChangedCommand
        {

            get
            {
                return new Command(() =>
                {
                    FrameNameError = false;
                });
            }
        }

        public ICommand EditPersonalInformationCommand
        {

            get
            {
                return new Command(() =>
                {
                    IndividualRegistrationView = true;
                    NationalAddressView = false;
                    ContactInformationView = false;
                    SummeryView = false;
                });
            }
        }

        public ICommand EditNationalAddressCommand
        {

            get
            {
                return new Command(() =>
                {
                    IndividualRegistrationView = false;
                    NationalAddressView = true;
                    ContactInformationView = false;
                    SummeryView = false;
                });
            }
        }

        public ICommand EditContactInformationCommand
        {

            get
            {
                return new Command(() =>
                {
                    IndividualRegistrationView = false;
                    NationalAddressView = false;
                    ContactInformationView = true;
                    SummeryView = false;
                });
            }
        }

        public ICommand RefreshCaptchaCommand
        {

            get
            {
                return new Command(async () =>
                {
                    await GetCaptchImage();
                    Captcha = string.Empty;
                });
            }
        }

        public ICommand CountryCodesCommand
        {

            get
            {
                return new Command(async () =>
                {
                    await MopupService.Instance.PushAsync(new InternationalCodeSearchPage(mobileData));
                });
            }
        }

        public ICommand DDlIDTypeOkayButtonCommand
        {

            get
            {
                return new Command(async () =>
                {
                    TxtIDType = IdTypeList[IDTypeIndex].Name;
                    SelectedIdType = IdTypeList[IDTypeIndex];
                    IdNumber = string.Empty;
                    if (!SelectedIdType.ID.Equals("ZS0018"))
                    {
                        if (string.IsNullOrEmpty(DOB) && string.IsNullOrEmpty(IdNumber))
                        {
                            await ValidateIDNumber();
                        }

                    }
                });
            }
        }

        public ICommand RegionOkayButtonCommand
        {

            get
            {
                return new Command(() =>
                {

                    try
                    {
                        SelectedRegion = RegionList[SelectedRegionIndex];
                        Region = RegionList[SelectedRegionIndex].Bezei;
                        CityName = string.Empty;
                    }
                    catch (Exception)
                    { }
                });
            }
        }
        public ICommand RegioSelectionChangedCommand
        {

            get
            {
                return new Command<PickerSelectionChangedEventArgs>((e) =>
                {
                    try
                    {
                        SelectedRegionIndex = e.NewValue;
                        SelectedRegion = RegionList[e.NewValue];
                        Region = RegionList[e.NewValue].Bezei;
                        CityName = string.Empty;
                    }
                    catch (Exception)
                    {

                    }
                });
            }
        }
        public ICommand GCCCountryOkayButtonCommand
        {

            get
            {
                return new Command(() =>
                {

                    try
                    {
                        CountryName = GCCCountryList[SelectedGCCCountryIndex].CountryName;
                        SelectedGCCCountry = GCCCountryList[SelectedGCCCountryIndex];

                    }
                    catch (Exception)
                    { }
                });
            }
        }

        public ICommand CityOkayButtonCommand
        {

            get
            {
                return new Command(() =>
                {

                    try
                    {
                        SelectedCity = CityList[SelectedCityIndex];
                        CityName = CityList[SelectedCityIndex].CityName;

                    }
                    catch (Exception)
                    { }
                });
            }
        }
        public ICommand CitySelectionChangedCommand
        {

            get
            {
                return new Command<PickerSelectionChangedEventArgs>((e) =>
                {
                    try
                    {
                        SelectedCityIndex = e.NewValue;
                        SelectedCity = CityList[e.NewValue];
                        CityName = CityList[e.NewValue].CityName;
                    }
                    catch (Exception)
                    {

                    }
                });
            }
        }
        public ICommand EntryIDNumberTextCommand
        {

            get
            {
                return new Command(() =>
                {

                    try
                    {
                        if (!string.IsNullOrEmpty(IdNumber))
                        {
                            if (SelectedIdType.ID == "ZS0015")
                            {
                                if (IdNumber.Length < MaxLengthID)
                                {
                                    FrameIDError = true;
                                }
                                else
                                {
                                    FrameIDError = false;
                                }

                            }
                            if (SelectedIdType.ID == "ZS0017")
                            {
                                if (IdNumber.Length < MaxLengthID)
                                {
                                    FrameIDError = true;
                                }
                                else
                                {
                                    FrameIDError = false;
                                }

                            }
                            if (SelectedIdType.ID == "ZS0018")
                            {
                                if (IdNumber.Length < 7 || IdNumber.Length > 15)
                                {
                                    FrameIDError = true;
                                }
                                else
                                {
                                    FrameIDError = false;
                                }

                            }
                        }
                        else
                        {
                            FrameIDError = false;
                        }
                    }
                    catch (Exception)
                    { }
                });
            }
        }

        public ICommand DOBOkButtonCommand
        {

            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (IsHijriCal)
                        {
                            string month = TodayDateinHijri[1].ToString();
                            string day = TodayDateinHijri[0].ToString();
                            string year = TodayDateinHijri[2].ToString();
                            DOB = year + "/" + month + "/" + day;
                            DOBddyymm = day + "/" + month + "/" + year;
                            DOBPrev = DOB;

                        }
                        else
                        {
                            string month = TodayDate[1].ToString();
                            string day = TodayDate[0].ToString();
                            string year = TodayDate[2].ToString();
                            DOB = year + "/" + month + "/" + day;
                            DOBddyymm = day + "/" + month + "/" + year;
                            DOBPrev = DOB;

                        }
                        await ValidateIDNumber();
                    }
                    catch (Exception)
                    {

                    }
                });
            }
        }

        public ICommand ChipGroupSelectedCommand
        {

            get
            {
                return new Command(() =>
                {
                    try
                    {
                        if (ChipGroupStatusFilterSelectedItem.Text.Equals(AppResources.NDHijri))
                        {
                            IsHijriCal = true;
                            if (TodayDateinHijri != null)
                            {
                                string month = TodayDateinHijri[1].ToString();
                                string day = TodayDateinHijri[0].ToString();
                                string year = TodayDateinHijri[2].ToString();
                                DOB = year + "/" + month + "/" + day;
                                DOBddyymm = day + "/" + month + "/" + year;
                                DOBPrev = DOB;

                            }
                            else
                            {
                                DOB = string.Empty;
                                DOBddyymm = string.Empty;

                            }

                        }
                        else
                        {
                            IsHijriCal = false;
                            if (TodayDate != null)
                            {
                                string month = TodayDate[1].ToString();
                                string day = TodayDate[0].ToString();
                                string year = TodayDate[2].ToString();
                                DOB = year + "/" + month + "/" + day;
                                DOBddyymm = day + "/" + month + "/" + year;
                                DOBPrev = DOB;

                            }
                            else
                            {
                                DOB = string.Empty;
                                DOBddyymm = string.Empty;
                            }

                        }

                    }
                    catch (Exception)
                    {
                    }
                });
            }
        }

        public ICommand GCCPickerCountrySelectionChanged
        {

            get
            {
                return new Command<PickerSelectionChangedEventArgs>((e) =>
                {
                    try
                    {
                        SelectedGCCCountryIndex = e.NewValue;
                        SelectedCityIndex = e.NewValue;
                        CountryName = GCCCountryList[e.NewValue].CountryName;
                        SelectedGCCCountry = GCCCountryList[e.NewValue];
                    }
                    catch (Exception)
                    {

                    }
                });
            }
        }

        public ICommand IDTypeSelectedIndexCommand
        {

            get
            {
                return new Command<PickerSelectionChangedEventArgs>(async (e) =>
                {
                    try
                    {
                        IDTypeIndex = e.NewValue;
                        TxtIDType = IdTypeList[e.NewValue].Name;
                        SelectedIdType = IdTypeList[e.NewValue];
                        IdNumber = string.Empty;
                        if (!SelectedIdType.ID.Equals("ZS0018"))
                        {
                            if (string.IsNullOrEmpty(DOB) && string.IsNullOrEmpty(IdNumber))
                            {
                                await ValidateIDNumber();
                            }

                        }
                    }
                    catch (Exception)
                    {



                    }
                });
            }
        }

        public ICommand EntryPasswordTextChangedCommand
        {

            get
            {
                return new Command(() =>
                {
                    ResetPasswordValidationConditions();
                    bool ValidPassword = UtilityManager.ValidateNewPassword(Password);
                    if (ValidPassword)
                    {
                        MinEight = "check_oval";
                        CapsSmall = "check_oval";
                        MaxSixteen = "check_oval";
                        NumSymbol = "check_oval";

                        // check the new and confirm password condition
                    }
                    else
                    {
                        if (UtilityManager.ValidMinEight) { MinEight = "check_oval"; }
                        if (UtilityManager.ValidSmallL && UtilityManager.ValidCapsL) { CapsSmall = "check_oval"; }
                        if (UtilityManager.ValidMaxSixteen) { MaxSixteen = "check_oval"; }
                        if (UtilityManager.ValidNumber && UtilityManager.ValidSymbol) { NumSymbol = "check_oval"; }
                    }
                });
            }
        }

        public ICommand DatePickerUnfocusedCommand
        {

            get
            {
                return new Command(async () =>
                {
                    await ValidateIDNumber();
                });
            }
        }

        public ICommand EntryEmailUnfocusedCommand
        {

            get
            {
                return new Command(async () =>
                {
                    if (!string.IsNullOrEmpty(Email))
                    {
                        bool flag = IsValid(Email);
                        if (!flag)
                        {
                            PopUp popUp = new PopUp();
                            popUp.Message = AppResources.ZZPleaseenteravalidEmailAddress;

                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleaseenteravalidEmailAddress));
                            FrameEmailError = true;
                            Email = string.Empty;
                        }
                        else
                        {
                            FrameEmailError = false;
                        }
                    }
                });
            }
        }

        public ICommand EntryIDNumberUnfocusedCommand
        {

            get
            {
                return new Command(async () =>
                {
                    PopUp popUp = new PopUp();
                    StringBuilder Messages = new StringBuilder();
                    if (!string.IsNullOrEmpty(IdNumber))
                    {
                        if (SelectedIdType.ID == "ZS0015")
                        {
                            if (IdNumber.Substring(0, 1) != "1")
                            {
                                popUp.Message = AppResources.ZZNationalIDstartswith1;
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
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZNationalIDstartswith1));

                                FrameIDError = true;
                                IdNumber = string.Empty;
                            }
                            else
                            {
                                if (IdNumber.Length != 10)
                                {
                                    if (Messages.Length > 0)
                                    {
                                        Messages.Append(Environment.NewLine);
                                    }
                                    Messages.AppendLine(AppResources.ZZNationalIDlengthis10digit);
                                }
                                if (Messages.Length > 0)
                                {
                                    popUp.Message = Messages.ToString();
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
                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));
                                    //FrmIDNumber.HasError = true;
                                    FrameIDError = true;
                                    IdNumber = string.Empty;
                                }
                                else
                                {
                                    //FrmIDNumber.HasError = false;
                                    FrameIDError = false;
                                    if (!string.IsNullOrEmpty(DOB))
                                    {
                                        await ValidateIDNumber();
                                    }


                                }
                            }


                        }
                        if (SelectedIdType.ID == "ZS0017")
                        {
                            if (IdNumber.Substring(0, 1) != "2")
                            {
                                popUp.Message = AppResources.ZZIqamaIDstartswith2;
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
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZIqamaIDstartswith2));
                                FrameIDError = true;
                                IdNumber = string.Empty;
                            }
                            else
                            {
                                if (IdNumber.Length != 10)
                                {
                                    if (Messages.Length > 0)
                                    {
                                        Messages.Append(Environment.NewLine);
                                    }
                                    Messages.AppendLine(AppResources.ZZIqamaIDlengthis10digit);
                                }
                                if (Messages.Length > 0)
                                {
                                    popUp.Message = Messages.ToString();
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
                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(Messages.ToString()));
                                    FrameIDError = true;
                                    IdNumber = string.Empty;
                                }
                                else
                                {
                                    FrameIDError = false;
                                    if (!string.IsNullOrEmpty(DOB))
                                    {
                                        await ValidateIDNumber();
                                    }
                                }
                            }


                        }
                        if (SelectedIdType.ID == "ZS0018")
                        {
                            if (IdNumber.Substring(0, 1) == "0")
                            {
                                //Have to change to neww error message
                                popUp.Message = AppResources.ZZGCCIDdonotstartwith0;
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
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGCCIDdonotstartwith0));
                                FrameIDError = true;
                                IdNumber = string.Empty;
                            }
                            else if (!(IdNumber.Length <= 15 && IdNumber.Length >= 7))
                            {
                                popUp.Message = AppResources.ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit;
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
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGulfCooperationCouncilGCCIDlengthisbetween7to15digit));
                                FrameIDError = true;
                                IdNumber = string.Empty;
                            }


                        }
                    }
                    else
                    {
                        FrameIDError = false;
                    }
                });
            }
        }

        public ICommand EntryConfirmEmailUnfocusedCommand
        {

            get
            {
                return new Command(async () =>
                {
                    if (!string.IsNullOrEmpty(ConfirmEmail))
                    {
                        bool flag = IsValid(ConfirmEmail);
                        if (!flag)
                        {
                            PopUp popUp = new PopUp();
                            popUp.Message = AppResources.ZZPleaseenteravalidEmailAddress;
                            popUp.IsLinkAvailable = false;
                            if (App.IsArabic)
                            {
                                popUp.FlowDirections = "RightToLeft";
                            }
                            else
                            {
                                popUp.FlowDirections = "LeftToRight";
                            }
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleaseenteravalidEmailAddress));
                            FrameConfirmEmailError = true;
                            ConfirmEmail = string.Empty;
                        }
                        else
                        {
                            FrameConfirmEmailError = false;
                        }
                    }
                });
            }
        }

        public ICommand EntryMobileNumberUnfocusedCommand
        {

            get
            {
                return new Command(async () =>
                {
                    StringBuilder Message = new StringBuilder();
                    PopUp popUp = new PopUp();
                    if (!string.IsNullOrEmpty(MobileNumber))
                    {
                        if (MobileNumber.Substring(0, 1) == "0")
                        {
                            Message.AppendLine(AppResources.ZZMobilenumberCannotStartWith0);
                        }
                        if (MobileNumber.Length < 9)
                        {
                            if (Message.Length > 0)
                            {
                                Message.Append(Environment.NewLine);
                            }
                            Message.AppendLine(AppResources.ZZMobilenumberlengthcannotbelessthan9digits);
                        }
                        if (Message.Length > 0)
                        {
                            popUp.Message = Message.ToString();
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
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(Message.ToString()));
                            FrameMobileNumberError = true;
                            MobileNumber = string.Empty;
                        }
                        else
                        {
                            FrameMobileNumberError = false;
                        }
                    }
                    else
                    {
                        Message.AppendLine(AppResources.EnterMobileNumber);
                        popUp.Message = Message.ToString();
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(Message.ToString()));
                    }
                });
            }
        }

        public ICommand EntryPostalCodeUnfocusedCommand
        {

            get
            {
                return new Command(async () =>
                {
                    StringBuilder Message = new StringBuilder();
                    PopUp popUp = new PopUp();
                    if (!string.IsNullOrEmpty(PostalCode))
                    {
                        if (PostalCode.Length != 5)
                        {
                            if (Message.Length > 0)
                            {
                                Message.Append(Environment.NewLine);
                            }
                            Message.AppendLine(AppResources.ZZZZInvalidPostalCode);

                            if (Message.Length > 0)
                            {
                                popUp.Message = Message.ToString();
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
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(Message.ToString()));
                                PostalCode = string.Empty;
                            }
                        }


                    }
                });
            }
        }

        #region Method

        private bool IsValid(string emailaddress)
        {
            bool isEmail = Regex.IsMatch(emailaddress, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if (isEmail)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task ValidateIDNumber()
        {
            try
            {
                IsLoading = true;

                string dob = DOB.Replace("/", "");

                IsEntryNameEnabled = true;

                if (SelectedIdType.ID == "ZS0015")
                {
                    if (!string.IsNullOrEmpty(IdNumber))
                    {
                        try
                        {

                            string Result = await TaxEvasionWebServiceManager.GAZTVATSignUpValidateIDTypesStringResp("ZS0015", IdNumber, dob);
                            VATSignUp vATSignUpData = new VATSignUp();
                            vATSignUpData = JsonConvert.DeserializeObject<VATSignUp>(Result);
                            if (vATSignUpData.d == null)
                            {
                                IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                                if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                                {
                                    FrameIDError = true;
                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                                }
                                else
                                {
                                    FrameIDError = false;
                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                                }
                            }
                            else
                            {
                                Name = vATSignUpData.d.name1 + " " + vATSignUpData.d.name2;
                                IsEntryNameEnabled = false;
                                FrameIDError = false;
                            }
                        }
                        catch
                        {
                            try
                            {
                                string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0015", IdNumber, dob);
                                IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                                if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                                {
                                    FrameIDError = true;
                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                                }
                                else
                                {
                                    FrameIDError = false;
                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
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

                                IsLoading = false;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                _navigationService.GoBack();
                            }
                            catch (InternetException ex)
                            {
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                                IsLoading = false;
                            }
                            catch (HttpRequestException)
                            {
                                string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                            }
                            catch (Exception)
                            {
                                string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                            }
                        }
                    }
                }

                if (SelectedIdType.ID == "ZS0017")
                {
                    IsEntryNameEnabled = true;
                    if (!string.IsNullOrEmpty(IdNumber))
                    {
                        try
                        {

                            string Result = await TaxEvasionWebServiceManager.GAZTVATSignUpValidateIDTypesStringResp("ZS0017", IdNumber, dob);
                            VATSignUp vATSignUpData = new VATSignUp();
                            vATSignUpData = JsonConvert.DeserializeObject<VATSignUp>(Result);
                            if (vATSignUpData.d == null)
                            {
                                IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                                if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                                {
                                    FrameIDError = true;
                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                                }
                                else
                                {
                                    FrameIDError = false;
                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                                }
                            }
                            else
                            {
                                Name = vATSignUpData.d.name1 + " " + vATSignUpData.d.name2;
                                IsEntryNameEnabled = false;
                                FrameIDError = false;
                            }
                        }
                        catch
                        {
                            try
                            {
                                string Result = await WebServiceManager.GAZTValidateIDTypes("ZS0017", IdNumber, dob);
                                IDTypeValidateRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                                if (SignupIsIDTypeValid.error.message.value == "An exception was raised.")
                                {
                                    FrameIDError = true;
                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
                                }
                                else
                                {
                                    FrameIDError = false;
                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValid.error.innererror.errordetails[0].message));
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

                                IsLoading = false;

                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                _navigationService.GoBack();
                            }
                            catch (InternetException ex)
                            {
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                                IsLoading = false;
                            }
                            catch (HttpRequestException)
                            {
                                string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                            }
                            catch (Exception)
                            {
                                string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                            }
                        }
                    }
                }

                IsLoading = false;
            }
            catch (Exception)
            {

            }

        }

        private void ResetPasswordValidationConditions()
        {
            MinEight = "error";
            CapsSmall = "error";
            MaxSixteen = "error";
            NumSymbol = "error";
        }

        public void PopulateDataInChips()
        {
            ChipDataFilterlist = new ObservableCollection<ChipModel>()
            {
                new ChipModel(){Text =AppResources.NDGregorian, TemplateType = AppResources.NDGregorian, ImageSource="Paid_check.png"},
                new ChipModel(){Text =AppResources.NDHijri, TemplateType = AppResources.NDHijri,ImageSource = "partially_clock.png"},
            };
        }

        public void ClearData()
        {
            SetDefaultDate();
            VerifyButtonDisableColor = (Color)Application.Current.Resources["Secondary"];
            IsVerifyOTPEnabled = true;
            IsResendOTPEnabled = false;
            ButtonDisableColor = Colors.Gray;
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
            oneStepBackArrowVisible = true;
            ConfirmPassword = string.Empty;
            DOBddyymm = string.Empty;
            OTP = string.Empty;
            Title = string.Empty;
            ShowIqamaTypeDesc = false;
            IqamaTypeDesc = string.Empty;
            Captcha = string.Empty;
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

                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                _navigationService.GoBack();

            }
            catch (HttpRequestException)
            {
                string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
            }


            catch (InternetException ex)
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
            }
            catch (Exception)
            {


                string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
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
            catch (Exception)
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
            if (string.IsNullOrEmpty(Captcha))
            {
                showErrorMessage = true;
                messageforuserr = AppResources.ZZPleasefillthemandatoryfields;
                FrameCaptchaError = true;
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
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(messageforuserr));
            }
            else
            {
                ContactInformationView = false;
                SummeryView = true;
                CurrentIndex = 4;
                ContinueButtonText = AppResources.Confirm;
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
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillthemandatoryfields));
            }
            else
            {
                NationalAddressView = false;
                ContactInformationView = true;
                CurrentIndex = 3;
                ContinueButtonText = AppResources.ZZZZContinue;
            }
        }

        public async Task steponevalidation()
        {
            if (IsCitizen)
            {


                List<SignUpIdType> signUpIdTypeListCitizenNC = new List<SignUpIdType>{
           new SignUpIdType {ID = "ZS0015",Name = AppResources.NationaID},

                                          };

                List<SignUpIdType> signUpIdTypeListCitizenIQ = new List<SignUpIdType>{
           new SignUpIdType { ID = "ZS0017", Name = AppResources.ZZIqamaID },
                };


                if (modelSSOID.results[0].Idnumber.Substring(0, 1) == "1")
                {
                    IdTypeList = signUpIdTypeListCitizenNC;
                    TxtIDType = AppResources.ZZNationalID;
                    SelectedIdType = signUpIdTypeListCitizenNC[0];
                }
                else
                {
                    IdTypeList = signUpIdTypeListCitizenIQ;
                    TxtIDType = AppResources.ZZIqamaID;
                    SelectedIdType = signUpIdTypeListCitizenIQ[0];
                }

                Name = modelSSOID.results[0].Firstname;
                DOB = modelSSOID.results[0].Birthdt.ToString();
                IdNumber = modelSSOID.results[0].Idnumber;


            }

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
                            bool isValidId = false;
                            if (IsCitizen)
                            {
                                isValidId = true;
                            }
                            else
                            {
                                isValidId = await ValidateId();
                            }
                            if (isValidId)
                            {
                                IndividualRegistrationView = false;
                                NationalAddressView = true;
                                IsLoading = true;
                                try
                                {
                                    vATSignUpData = await TaxEvasionWebServiceManager.GAZTGetVATSignUpCityListForSignup();

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

                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                    _navigationService.GoBack();
                                }
                                catch (HttpRequestException)
                                {


                                    string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                }


                                catch (InternetException ex)
                                {
                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                                }
                                catch (Exception)
                                {



                                    string MessageForTheUser = AppResources.ZZSomethingwentwrong;
                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));
                                }
                                SetVisibilityToNationalAddressContent();
                                IsLoading = false;


                            }
                            else
                            {
                                FrameIDError = true;
                                FrameDOBError = true;
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp("Wrong Id"));



                            }
                        }
                    }

                }
                else
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillthemandatoryfields));
                }
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
            if (!IsCitizen)
            {
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



            }
            else
            {
                FramePasswordError = false;
                FrameConfirmPasswordError = false;
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
                    //MopupService.Instance.PushAsync(new AddPopPageView(popUp));
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(PopMsg.ToString()));

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

            List<SignUpIdType> signUpIdTypeListGulf = new List<SignUpIdType>{

                                            new SignUpIdType {ID = "ZS0018",Name = AppResources.ZZGCCID},


            };

            List<SignUpIdType> signUpIdTypeListCitizenNC = new List<SignUpIdType>{
           new SignUpIdType {ID = "ZS0015",Name = AppResources.NationaID},

                                          };

            List<SignUpIdType> signUpIdTypeListCitizenIQ = new List<SignUpIdType>{
           new SignUpIdType { ID = "ZS0017", Name = AppResources.ZZIqamaID },
                                          };




            List<SignUpIdType> lst = new List<SignUpIdType>();
            if (IsGulfER)
            {
                lst = signUpIdTypeListGulf;
                IdTypeList = signUpIdTypeListGulf;

                TxtIDType = AppResources.ZZGCCID;
                SelectedIdType = signUpIdTypeListGulf[0];
            }
        }

        public async Task<string> ValidateIDs()
        {
            try
            {
                string dob = DOB.Replace("/", "");
                string resposne = await TaxEvasionWebServiceManager.GAZTVATSignUpValidateIDTypesStringResp(IdTypeList[IDTypeIndex].ID, IdNumber, dob);
                return resposne;
            }
            catch (Exception)
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
            catch (Exception)
            {


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
            catch (Exception)
            {



            }



        }

        public void SetStateList()
        {
            try
            {
                if (!SelectedIdType.ID.Equals("ZS0018"))
                {
                    RegionList = vATSignUpData.d.State_dropdownSet;
                    RegionList = vATSignUpData.d.State_dropdownSet.Where(x => x.Land1 == "SA").ToList();
                }
            }
            catch (Exception)
            {


            }


        }

        public void SetCityList()
        {
            try
            {
                if (!SelectedIdType.ID.Equals("ZS0018"))
                {

                    CityList = vATSignUpData.d.city_dropdownSet;
                    string selectedRegioncode = SelectedRegion.Bland;

                    CityList = vATSignUpData.d.city_dropdownSet.Where(x => x.Region == selectedRegioncode).ToList();
                    if (CityList != null)
                    {
                        CityList = CityList.Where(c => c.Country == "SA").ToList();
                    }
                }
            }
            catch (Exception)
            {


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
            catch (Exception)
            {


            }

        }

        public void GetVATSignUpGCCList()
        {

            List<VATSignUpGCC> signUpIdTypeList = new List<VATSignUpGCC>{
           new VATSignUpGCC {CountryName = AppResources.ZZZZUAE,CountryCode="AE",CountryId="1"},
           new VATSignUpGCC {CountryName = AppResources.ZZZZBahrain,CountryCode="BH",CountryId="2"},
           new VATSignUpGCC {CountryName = AppResources.ZZZZKuwait,CountryCode="KW",CountryId="3"},
           new VATSignUpGCC {CountryName = AppResources.ZZZZOman,CountryCode="OM",CountryId="4"},
           new VATSignUpGCC {CountryName = AppResources.ZZZZQatar,CountryCode="QA",CountryId="5"},

            };

            List<VATSignUpGCC> lst = new List<VATSignUpGCC>();
            lst = signUpIdTypeList;
            GCCCountryList = lst;
        }

        public async Task GetCaptchImage()
        {
            IsLoading = true;
            ImageCaptchaModel result = await WebServiceManager.GetCaptchaImage("C6", LgId);
            if (result != null)
            {
                ImageBase64 = result.data.cval;
                LgId = result.data.GUID;
            }

            IsLoading = false;
        }

        public async Task SetRequestObjectFirst()
        {
            try
            {
                var Bdt1 = string.Empty;
                string date1 = DOB;
                DateTime dt = Convert.ToDateTime(date1);

                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };

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
                string submitValue;
                if (currentStep == 4)
                {
                    submitValue = "";
                }
                else
                {
                    submitValue = "X";
                }
                var Mguid = string.Empty;
                if (IsCitizen)
                {

                    Mguid = App.GUIDFrSSO;
                }
                else
                {
                    Mguid = string.Empty;
                }

                Begindate = "2022-02-23T08:05:26";
                EndDate = "2025-02-23T08:05:26";
                string newCountryCodeString = TxtCountryCode.Replace("+", "00");
                CreateVatSignUPRequest createVatSignUPRequest = new CreateVatSignUPRequest();
                createVatSignUPRequest.taxpayerTitle = "";
                createVatSignUPRequest.type = "1";
                createVatSignUPRequest.idType = SelectedIdType.ID;
                createVatSignUPRequest.idNumber = IdNumber;
                createVatSignUPRequest.firstName = Name;
                createVatSignUPRequest.lastName = string.Empty;
                createVatSignUPRequest.postCode = PostCode;
                createVatSignUPRequest.city = _City;
                createVatSignUPRequest.region = _Region;
                createVatSignUPRequest.country = _Country;
                createVatSignUPRequest.mobileCountry = MobileCountry;
                createVatSignUPRequest.buildingCode = BuildingNumber;
                createVatSignUPRequest.floor = UnitNumber;
                createVatSignUPRequest.street = Neighborhood;
                createVatSignUPRequest.beginDate = Begindate;
                createVatSignUPRequest.endDate = EndDate;
                createVatSignUPRequest.email = Email;
                if (IsGulfER)
                {
                    createVatSignUPRequest.birthDate = date1 + "T00:00:00";
                    createVatSignUPRequest.birthDate = dt.ToString("yyyy-MM-dd") + "T00:00:00";
                }
                else
                {
                    createVatSignUPRequest.birthDate = BirthDate;
                }
                createVatSignUPRequest.password = Password;
                createVatSignUPRequest.SMSCode = OTP;
                createVatSignUPRequest.emailCode = "";
                createVatSignUPRequest.submit = submitValue;
                createVatSignUPRequest.captchaCode = Captcha;
                createVatSignUPRequest.mobile = newCountryCodeString + MobileNumber;
                createVatSignUPRequest.caseGUID = LgId;
                createVatSignUPRequest.GUID = Mguid;




                VATSignUpSubmit vATSignUpSubmit = new VATSignUpSubmit
                {
                    Type = "1",
                    IdType = SelectedIdType.ID,//"ZS0018",
                    Idnumber = IdNumber,
                    Firstname = Name,
                    Lastname = string.Empty,
                    PostCode1 = "00000",
                    City1 = _City,
                    Region = _Region,
                    Country = _Country,
                    MobileCountry = MobileCountry,

                    Building = BuildingNumber,
                    Floor = UnitNumber,
                    Street = Neighborhood,
                    Begda = "/Date(1593139376000)/",
                    Endda = "/Date(253402251010000)/",
                    Email = Email,
                    Mobile = newCountryCodeString + MobileNumber,
                    CaseGuid = LgId,

                    Birthdt = Bdt1,//"/Date(1577846576000)/",
                    Password = Password,
                    SmsCode = OTP,
                    EmailCode = "",
                    Submit = submitValue,
                    Captcha = Captcha,
                    Mguid = Mguid

                };

                if (!string.IsNullOrEmpty(IqamaTypeDesc)) //CR6407
                {
                    vATSignUpSubmit.AIqamaDesc = IqamaTypeDesc;
                    vATSignUpSubmit.AIqamaFg = "X";
                }
                else
                {
                    vATSignUpSubmit.AIqamaDesc = "";
                    vATSignUpSubmit.AIqamaFg = "";
                }

                //  VATSignUpSubmit response = await WebServiceManager.GAZTCreateVATSignUp(vATSignUpSubmit);
                var res = await TaxEvasionWebServiceManager.VatSignUP(createVatSignUPRequest);
                var response = JsonConvert.DeserializeObject<CreateVatSignUPResponse>(res);
                response.data = response.Result;
                if (response.data == null)
                {
                    ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(res);

                    string Message = string.Empty;
                    if (errorMesg != null && errorMesg.header != null && errorMesg.header.moreInformation != null && errorMesg.header.moreInformation.errorDetails != null && errorMesg.header.moreInformation.errorDetails[0].message != null)
                    {
                        WebServiceManager.ErrorMessageForVAT = errorMesg.header.moreInformation.errorDetails[0].message;
                        WebServiceManager.ErrorMessageForVAT += errorMesg.header.moreInformation.errorDetails[1].message;
                        Message = WebServiceManager.ErrorMessageForVAT.Replace("An exception was raised", string.Empty);
                    }
                    OTP = string.Empty;
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(Message.ToString()));


                }
                else
                {
                    if (currentStep == 4)
                    {
                        Captcha = response.data.captchaCode;
                        Mguid = response.data.GUID;
                        SummeryView = false;
                        PasswordView = true;
                        oneStepBackArrowVisible = false;
                        if (IsCitizen)
                        {
                            passwordMainView = false;
                        }
                        else
                        {
                            passwordMainView = true;
                        }
                        CurrentIndex = 5;
                        int timeToExpireOTP = 120;
                        TimerStart(timeToExpireOTP);
                    }
                    else if (currentStep == 5)
                    {
                        PasswordView = false;
                        currentStep = 1;
                        string TinNumber = response.data.TIN;
                        await _navigationService.NavigateTo(App.RegistrationSuccessfulPageView, TinNumber);
                    }
                }
            }
            catch (Exception ex)
            {
                string MessageForTheUser = AppResources.Somethingwentwrong;
                if (ex is InternetException)
                {
                    MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                }
                else
                {
                    IsVerifyOTPEnabled = true;
                }
                IsLoading = false;
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));


            }
        }

        public async Task SetRequestObjectResendOtp()
        {
            try
            {
                string[] date1 = DOB.Split('/');
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
                var Mguid = string.Empty;
                if (IsCitizen)
                {
                    var guid = App.GUIDFrSSO.Split(new string[] { "guid=" }, StringSplitOptions.None)[1];
                    Mguid = guid;
                }
                else
                {
                    Mguid = string.Empty;
                }
                VATSignUpSubmit vATSignUpSubmit = new VATSignUpSubmit
                {
                    Type = "1",
                    IdType = SelectedIdType.ID,//"ZS0018",
                    Idnumber = IdNumber,
                    Firstname = Name,
                    Lastname = ".",
                    PostCode1 = "00000",
                    City1 = _City,
                    Region = _Region,
                    Country = _Country,
                    MobileCountry = MobileCountry,
                    Building = BuildingNumber,
                    Floor = UnitNumber,
                    Street = Neighborhood,
                    Begda = "2003-02-23T08:05:26",
                    Endda = "2024-02-23T08:05:26",
                    Email = Email,
                    Mobile = newCountryCodeString + MobileNumber,
                    CaseGuid = LgId,
                    Birthdt = Bdt,//"/Date(1577846576000)/",
                    Password = "",
                    SmsCode = "",
                    EmailCode = "",
                    Submit = submitValue,
                    Captcha = Captcha,
                    Mguid = Mguid,
                };

                if (!string.IsNullOrEmpty(IqamaTypeDesc)) //CR6407
                {
                    vATSignUpSubmit.AIqamaDesc = IqamaTypeDesc;
                    vATSignUpSubmit.AIqamaFg = "X";
                }
                else
                {
                    vATSignUpSubmit.AIqamaDesc = "";
                    vATSignUpSubmit.AIqamaFg = "";
                }


                string response = await TaxEvasionWebServiceManager.GAZTCreateVATSignUpFirst(vATSignUpSubmit);
                if (response != null)
                {
                    int timeToExpireOTP = 120;
                    TimerStart(timeToExpireOTP);
                    ButtonDisableColor = (Color)Application.Current.Resources["ButtonGray"];
                    IsResendOTPEnabled = false;
                    VerifyButtonDisableColor = (Color)Application.Current.Resources["Secondary"];
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
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(Message.ToString()));
                }
                else
                {



                }


            }
            catch (Exception)
            {
                string MessageForTheUser = AppResources.Somethingwentwrong;


                IsLoading = false;

                IsVerifyOTPEnabled = true;
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(MessageForTheUser));

            }
        }

        public void SetcolorForDots(string visiliblityItemName)
        {
            if (visiliblityItemName.Equals("IndividualRegistrationView"))

            {
                BoxColorOne = (Color)Application.Current.Resources["Primary"];
                BoxColorTwo = (Color)Application.Current.Resources["TabGray"];
                BoxColorThree = (Color)Application.Current.Resources["TabGray"];
                BoxColorFour = (Color)Application.Current.Resources["TabGray"];
                BoxColorFive = (Color)Application.Current.Resources["TabGray"];
                CurrentIndex = 1;
            }
            else if (visiliblityItemName.Equals("NationalAddressView"))
            {
                BoxColorOne = (Color)Application.Current.Resources["Primary"];
                BoxColorTwo = (Color)Application.Current.Resources["Primary"];
                BoxColorThree = (Color)Application.Current.Resources["TabGray"];
                BoxColorFour = (Color)Application.Current.Resources["TabGray"];
                BoxColorFive = (Color)Application.Current.Resources["TabGray"];
                CurrentIndex = 2;
            }
            //ContactInformationView
            else if (visiliblityItemName.Equals("ContactInformationView"))
            {
                BoxColorOne = (Color)Application.Current.Resources["Primary"];
                BoxColorTwo = (Color)Application.Current.Resources["Primary"];
                BoxColorThree = (Color)Application.Current.Resources["Primary"];
                BoxColorFour = (Color)Application.Current.Resources["TabGray"];
                BoxColorFive = (Color)Application.Current.Resources["TabGray"];
                CurrentIndex = 3;
            }
            else if (visiliblityItemName.Equals("SummeryView"))
            {
                BoxColorOne = (Color)Application.Current.Resources["Primary"];
                BoxColorTwo = (Color)Application.Current.Resources["Primary"];
                BoxColorThree = (Color)Application.Current.Resources["Primary"];
                BoxColorFour = (Color)Application.Current.Resources["Primary"];
                BoxColorFive = (Color)Application.Current.Resources["TabGray"];
                CurrentIndex = 4;
            }
            else if (visiliblityItemName.Equals("PasswordView"))
            {
                BoxColorOne = (Color)Application.Current.Resources["Primary"];
                BoxColorTwo = (Color)Application.Current.Resources["Primary"];
                BoxColorThree = (Color)Application.Current.Resources["Primary"];
                BoxColorFour = (Color)Application.Current.Resources["Primary"];
                BoxColorFive = (Color)Application.Current.Resources["Primary"];
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
                    if (DeviceInfo.Platform == DevicePlatform.iOS)
                    {
                        TotalSec = TotalSec - Convert.ToInt32(App.TimeDifference);
                        App.IsComingFromSleepMode = false;
                        // StopTimer = true;
                    }
                    else
                    {


                        TotalSec = TotalSec - Convert.ToInt32(App.TimeDifference) - (120 - App.CurrentTimeDifference);
                        App.IsComingFromSleepMode = false;
                        StopTimer = true;

                        if (TotalSec > 0)
                        {
                            IsResendOTPEnabled = false;
                            ButtonDisableColor = Colors.Gray;
                        }

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
                    if (TotalSec < 0)
                    {
                        OTPValidDuration = " 0:00";
                        ButtonDisableColor = (Color)Application.Current.Resources["Secondary"];
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
                    App.CurrentTimeDifference = TotalSec;
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
            var calendar = new UmAlQuraCalendar();
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

        #endregion

    }
}
