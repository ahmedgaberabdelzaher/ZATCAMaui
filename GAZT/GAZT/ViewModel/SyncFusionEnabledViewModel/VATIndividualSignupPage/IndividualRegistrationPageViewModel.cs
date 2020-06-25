using EGAZT.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.VATIndividualSignupPage
{
    public class IndividualRegistrationPageViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnContinueButtonClick { get; set; }
        public int currentStep { get; set; }
        public VATSignUpData vATSignUpData { get; set; }
        public VATSignUpCaseId SignUpCaseIdD { get; set; }
        public VATSignUp _VATSignUp { get; set; }
         


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


        private bool _individualRegistrationView = true;
        public bool IndividualRegistrationView
        {
            get
            {
                return _individualRegistrationView;
            }
            set
            {
                _individualRegistrationView = value;
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
                _nationalAddressView = value;
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
                _contactInformationView = value;
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
                _summeryView = value;
                RaisePropertyChanged("SummeryView");
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
                _passwordView = value;
                RaisePropertyChanged("PasswordView");
            }
        }



        public Color _BoxColorOne;
        public Color BoxColorOne
        {
            get { return _BoxColorOne; }
            set
            {
                _BoxColorOne = value;
                RaisePropertyChanged("BoxColorOne");
            }
        }
        public Color _BoxColorTwo;
        public Color BoxColorTwo
        {
            get { return _BoxColorTwo; }
            set
            {
                _BoxColorTwo = value;
                RaisePropertyChanged("BoxColorTwo");
            }
        }
        public Color _BoxColorThree;
        public Color BoxColorThree
        {
            get { return _BoxColorThree; }
            set
            {
                _BoxColorThree = value;
                RaisePropertyChanged("BoxColorThree");
            }
        }

        public Color _BoxColorFour;
        public Color BoxColorFour
        {
            get { return _BoxColorFour; }
            set
            {
                _BoxColorFive = value;
                RaisePropertyChanged("BoxColorFour");
            }
        }

        public Color _BoxColorFive;
        public Color BoxColorFive
        {
            get { return _BoxColorFive; }
            set
            {
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
                _idTypeList = value;
                RaisePropertyChanged("IdTypeList");
            }
        }

        public int _iDTypeIndex = 0;
        public int IDTypeIndex
        {
            get {
                return _iDTypeIndex;
            }
            set
            {
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
                _DOB = value;
                RaisePropertyChanged("DOB");
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
                _txtIDType = value;
                RaisePropertyChanged("TxtIDType");
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
                        }
                        else if (_selectedIdType.ID.Equals("ZS0017"))
                        {
                            MaxLengthID = 10;
                        }
                        else if (_selectedIdType.ID.Equals("ZS0018"))
                        {
                            MaxLengthID = 15;
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
                RaisePropertyChanged("MobileNumber");
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
                _password = value;
                RaisePropertyChanged("Password");
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


        

        public string _countryName;
        public string CountryName
        {
            get
            {
                return _countryName;
            }
            set
            {
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
                _selectedCountryIndex = value;
                RaisePropertyChanged("SelectedCountryIndex");
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
                _selectedRegion = value;

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
                _selectedRegionIndex = value;
                RaisePropertyChanged("SelectedRegionIndex");
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

        public bool _setStateListVisibility = true;
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

        public string _postalCode = "";
        public string PostalCode
        {
            get
            {
                return _postalCode;
            }
            set
            {
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

            OnContinueButtonClick = new Xamarin.Forms.Command(async () =>
            {
                SetFormVisibility();
            });
        }
        #endregion

        #region Method
        public void ClearData()
        {

        }

        public async Task OnPageLoad()
        {
            currentStep = 1;
            GetSignUpIdType();
            SignUpCaseIdD = await WebServiceManager.GAZTGetVATSignUpCaseId();// working
            //string aaa = await WebServiceManager.GAZTVATSignUpValidateIDTypes("ZS0015", "1048089609", "19650224");
            //var dd = await WebServiceManager.GAZTGetVATSignUpCityListForSignup();
 }

        public async Task SetFormVisibility()
        {
            try
            {
                if (currentStep == 1)
                {

                    if(SelectedIdType.ID.Equals("ZS0018"))
                    {
                        IndividualRegistrationView = false;
                        NationalAddressView = true;
                        currentStep++;
                    }
                    else
                    {
                        bool isValidId = await ValidateId();
                        if (isValidId)
                        {
                            IndividualRegistrationView = false;
                            NationalAddressView = true;
                            currentStep++;
                            vATSignUpData = await WebServiceManager.GAZTGetVATSignUpCityListForSignup();
                            SetVisibilityToNationalAddressContent();
                          

                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                _dialogService.ShowMessageBox("Wrong Id", AppResources.ZError);

                            });


                        }
                    }
                

                }
                else if (currentStep == 2)
                {
                    NationalAddressView = false;
                    ContactInformationView = true;
                 
                    currentStep++;
                }
                else if (currentStep == 3)
                {
                    ContactInformationView = false;
                    SummeryView = true;

                    currentStep++;
                }
                else if (currentStep == 4)
                {
                    SummeryView = false;
                    PasswordView = true;
                    currentStep++;
                }
                else if (currentStep == 5)
                {
                    PasswordView = false;
                    currentStep = 1;
                    _navigationService.NavigateTo(App.RegistrationSuccessfulPageView);
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

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

        }
        /// <summary>
        /// Validate the National and Iqama ID
        /// </summary>
        public async Task<bool>  ValidateId()
        {
            string dob = DOB.Replace("/","");
           _VATSignUp = await WebServiceManager.GAZTVATSignUpValidateIDTypes(IdTypeList[IDTypeIndex].ID, IdNumber, dob);
            if(_VATSignUp != null && _VATSignUp.d != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void SetCountryList()
        {
            if (SelectedIdType.ID.Equals("ZS0018"))
            {
                CountryList = vATSignUpData.d.country_dropdownSet.results;
            }
            else
            {
                SetEnabilityToCountryList = false;
                CountryName = "Saudi Arabiya";
            }

        }

        public void SetStateList()
        {
            if (!SelectedIdType.ID.Equals("ZS0018"))
            {
                RegionList = vATSignUpData.d.State_dropdownSet.results;
            }
        }

        public void SetCityList()
        {
            if (!SelectedIdType.ID.Equals("ZS0018"))
            {
                CityList = GetCityList(SelectedRegion.Bland);//vATSignUpData.d.city_dropdownSet.results;
            }
        }

        public void SetVisibilityToNationalAddressContent()
        {
            if(SelectedIdType.ID.Equals("ZS0018"))
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
                CountryName = "Saudi Arabiya";
                SetEnabilityToCountryList = false;
              
            }
        }

        public void GetCityList(string Bland)
        {
            
        }

    }
}
