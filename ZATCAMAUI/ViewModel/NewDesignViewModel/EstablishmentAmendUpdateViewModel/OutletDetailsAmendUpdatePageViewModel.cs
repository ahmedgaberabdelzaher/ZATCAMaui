using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using Newtonsoft.Json;
using Mopups.Services;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.EstablishmentRegistration;
using ZATCAMAUI.Views.NewDesign.EstablishmentRegistrationPages;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using static ZATCAMAUI.Models.ErrorMessage;
using ZATCAMAUI.Core.Interfaces;
namespace ZATCAMAUI.ViewModel.NewDesignViewModel.EstablishmentAmendUpdateViewModel
{
 
    public class OutletDetailsAmendUpdatePageViewModel : BaseViewModel
    {
        #region Variable

        public bool isMainOutletExists = false;
        public List<OutletItem> ListOutlets { get; set; }
        public bool IsEditingMode { get; set; }
        private List<string> _listOutletTypes;
        public List<string> ListOutletTypes
        {
            get => _listOutletTypes;
            set
            {
                if (_listOutletTypes == value) return;

                _listOutletTypes = value;
                OnPropertyChanged(nameof(ListOutletTypes));
            }
        }
        private string _selectedOutletType;
        public string SelectedOutletType
        {
            get => _selectedOutletType;
            set
            {
                if (_selectedOutletType == value) return;

                _selectedOutletType = value;
                OnPropertyChanged(nameof(SelectedOutletType));
            }
        }
        public TaxPayerDetails taxPayerDetails { get; set; } = null;
        private OutletNumber newNumber = null;
        public Nreg_IdItem idItem { get; set; } = null;
        private ValidateCR validateCR = null;
        private Nreg_ActivityItem PreLoadedLicenseItem = null;
        public OutletItem selectedOutletItem { get; set; } = null;

        private EstablishmentRegistrationOutletTabsEnum _currentTab = EstablishmentRegistrationOutletTabsEnum.OutletDetail;
        public EstablishmentRegistrationOutletTabsEnum currentTab
        {
            get => _currentTab;
            set
            {
                _currentTab = value;
                OnPropertyChanged(nameof(currentTab));
                CurrentIndex = (int)value;
                MarkComplete = (int)value == MaxIndex;
                switch (value)
                {
                    case EstablishmentRegistrationOutletTabsEnum.ActivityDetails:
                        SelectedOutletTabText = AppResources.ESTActivityDetails;
                        NxtButtonLabel = AppResources.ZZNext;
                        break;
                    case EstablishmentRegistrationOutletTabsEnum.AddressDetails:
                        SelectedOutletTabText = AppResources.ESTAddressDetails;
                        NxtButtonLabel = AppResources.Save;
                        break;
                    case EstablishmentRegistrationOutletTabsEnum.OutletDetail:
                    default:
                        SelectedOutletTabText = AppResources.TinDeregistrationOutletDetails;
                        NxtButtonLabel = AppResources.ZZNext;
                        break;
                }
                if (taxPayerDetails != null)
                {
                    fetchTabDataAndBind(value);
                }
            }
        }

        public ObservableCollection<string> OutletTabSfChipTabList { get; set; }
          = new ObservableCollection<string>{ AppResources.TinDeregistrationOutletDetails, AppResources.ESTActivityDetails,
                AppResources.ESTAddressDetails};
        public ObservableCollection<string> OutletTabSfChipGroupTabList { get; set; }
           = new ObservableCollection<string>{ AppResources.ESTPassportDetailsTabTitleLabel, AppResources.ESTOutletsTabTitleLabel,
                AppResources.ZZZZVATREFinancialDetails};


        public bool MarkComplete { get; private set; } = false;
        public int MaxIndex { get; private set; } = 3;


        private int _currenrIndex = (int)EstablishmentRegistrationOutletTabsEnum.OutletDetail;
        public int CurrentIndex
        {
            get => _currenrIndex;
            set
            {
                if (_currenrIndex == value) return;

                _currenrIndex = value;
                OnPropertyChanged(nameof(CurrentIndex));
            }
        }

        public string SelectedTabText { get; private set; } = AppResources.TinDeregistrationRegistrationOutlets;
        private string _selectedOutletTabText = AppResources.ESTAddressDetails;
        public string SelectedOutletTabText
        {
            get => _selectedOutletTabText;
            private set
            {
                if (_selectedOutletTabText == value) return;

                _selectedOutletTabText = value;
                OnPropertyChanged(nameof(SelectedOutletTabText));
            }
        }

        private string _nxtButtonLabel = AppResources.ZZNext;
        public string NxtButtonLabel
        {
            get => _nxtButtonLabel;
            set
            {
                if (_nxtButtonLabel == value) return;

                _nxtButtonLabel = value;
                OnPropertyChanged(nameof(NxtButtonLabel));
            }
        }

        private string _outletName = string.Empty;
        public string OutletName
        {
            get => _outletName;
            set
            {
                if (_outletName == value) return;

                if (value != null)
                {
                    _outletName = value;
                    OnPropertyChanged(nameof(OutletName));
                }
            }
        }
        private string _outletActNumber = string.Empty;
        public string OutletActNumber
        {
            get => _outletActNumber;
            set
            {
                if (_outletActNumber == value) return;

                if (value != null)
                {
                    _outletActNumber = value;
                    OnPropertyChanged(nameof(OutletActNumber));
                }
            }
        }


        private string _houseNumber = string.Empty;
        public string HouseNumber
        {
            get => _houseNumber;
            set
            {
                if (_houseNumber == value) return;

                if (value != null)
                {
                    _houseNumber = value;
                    _houseNumberSame = value;
                    OnPropertyChanged(nameof(HouseNumber));
                    OnPropertyChanged(nameof(HouseNumberSame));
                }
            }
        }
        private string _buildingNumber = string.Empty;
        public string BuildingNumber
        {
            get => _buildingNumber;
            set
            {
                if (_buildingNumber == value) return;

                if (value != null)
                {
                    _buildingNumber = value;
                    _buildingNumberSame = value;
                    OnPropertyChanged(nameof(BuildingNumber));
                    OnPropertyChanged(nameof(BuildingNumberSame));
                }
            }
        }
        private string _floorNumber = string.Empty;
        public string FloorNumber
        {
            get => _floorNumber;
            set
            {
                if (_floorNumber == value) return;

                if (value != null)
                {
                    _floorNumber = value;
                    _floorNumberSame = value;
                    OnPropertyChanged(nameof(FloorNumber));
                    OnPropertyChanged(nameof(FloorNumberSame));
                }
            }
        }
        private string _street = string.Empty;
        public string Street
        {
            get => _street;
            set
            {
                if (_street == value) return;

                if (value != null)
                {
                    _street = value;
                    _streetSame = value;
                    OnPropertyChanged(nameof(Street));
                    OnPropertyChanged(nameof(StreetSame));
                }
            }
        }
        private string _quarter = string.Empty;
        public string Quarter
        {
            get => _quarter;
            set
            {
                if (_quarter == value) return;

                if (value != null)
                {
                    _quarter = value;
                    _quarterSame = value;
                    OnPropertyChanged(nameof(Quarter));
                    OnPropertyChanged(nameof(QuarterSame));
                }
            }
        }
        private string _postalCode = string.Empty;
        public string PostalCode
        {
            get => _postalCode;
            set
            {
                if (_postalCode == value) return;

                if (value != null)
                {
                    _postalCode = value;
                    _postalCodeSame = value;
                    OnPropertyChanged(nameof(PostalCode));
                    OnPropertyChanged(nameof(PostalCodeSame));
                }
            }
        }
        private string _addNumber = string.Empty;
        public string AddNumber
        {
            get => _addNumber;
            set
            {
                if (_addNumber == value) return;

                if (value != null)
                {
                    _addNumber = value;
                    _addNumberSame = value;
                    OnPropertyChanged(nameof(AddNumber));
                    OnPropertyChanged(nameof(AddNumberSame));
                }
            }
        }
        private CountryDropdownItem _country = null;
        public CountryDropdownItem Country
        {
            get => _country;
            set
            {
                if (_country == value) return;

                if (value != null)
                {
                    _country = value;
                    _countrySame = value;
                    OnPropertyChanged(nameof(Country));
                    OnPropertyChanged(nameof(CountrySame));
                }
            }
        }
        private StateDropdownItem _provinance = null;
        public StateDropdownItem Provinance
        {
            get => _provinance;
            set
            {
                if (_provinance == value) return;

                if (value != null)
                {
                    _provinance = value;
                    _provinanceSame = value;
                    OnPropertyChanged(nameof(Provinance));
                    OnPropertyChanged(nameof(ProvinanceSame));
                }
            }
        }
        private CityDropdownItem _city = null;
        public CityDropdownItem City
        {
            get => _city;
            set
            {
                if (_city == value) return;

                if (value != null)
                {
                    _city = value;
                    _citySame = value;
                    OnPropertyChanged(nameof(City));
                    OnPropertyChanged(nameof(CitySame));
                }
            }
        }
        private bool _postalAsPhysical = false;
        public bool PostalAsPhysical
        {
            get => _postalAsPhysical;
            set
            {
                if (_postalAsPhysical == value) return;

                _postalAsPhysical = value;
                OnPropertyChanged(nameof(PostalAsPhysical));
                if (value)
                {
                    HouseNumberSame = HouseNumber;
                    BuildingNumberSame = BuildingNumber;
                    FloorNumberSame = FloorNumber;
                    StreetSame = Street;
                    QuarterSame = Quarter;
                    PostalCodeSame = PostalCode;
                    AddNumberSame = AddNumber;
                    CountrySame = Country;
                    ProvinanceSame = Provinance;
                    CitySame = City;
                }
                else
                {
                    HouseNumberSame = string.Empty;
                    BuildingNumberSame = string.Empty;
                    FloorNumberSame = string.Empty;
                    StreetSame = string.Empty;
                    QuarterSame = string.Empty;
                    PostalCodeSame = string.Empty;
                    AddNumberSame = string.Empty;
                    CountrySame = null;
                    ProvinanceSame = null;
                    CitySame = null;
                }
            }
        }
        private string _houseNumberSame = string.Empty;
        public string HouseNumberSame
        {
            get => _houseNumberSame;
            set
            {
                if (_houseNumberSame == value) return;

                if (value != null)
                {
                    _houseNumberSame = value;
                    OnPropertyChanged(nameof(HouseNumberSame));
                }
            }
        }
        private string _buildingNumberSame = string.Empty;
        public string BuildingNumberSame
        {
            get => _buildingNumberSame;
            set
            {
                if (_buildingNumberSame == value) return;

                if (value != null)
                {
                    _buildingNumberSame = value;
                    OnPropertyChanged(nameof(BuildingNumberSame));
                }
            }
        }
        private string _floorNumberSame = string.Empty;
        public string FloorNumberSame
        {
            get => _floorNumberSame;
            set
            {
                if (_floorNumberSame == value) return;

                if (value != null)
                {
                    _floorNumberSame = value;
                    OnPropertyChanged(nameof(FloorNumberSame));
                }
            }
        }
        private string _streetSame = string.Empty;
        public string StreetSame
        {
            get => _streetSame;
            set
            {
                if (_streetSame == value) return;

                if (value != null)
                {
                    _streetSame = value;
                    OnPropertyChanged(nameof(StreetSame));
                }
            }
        }
        private string _quarterSame = string.Empty;
        public string QuarterSame
        {
            get => _quarterSame;
            set
            {
                if (_quarterSame == value) return;

                if (value != null)
                {
                    _quarterSame = value;
                    OnPropertyChanged(nameof(QuarterSame));
                }
            }
        }
        private string _postalCodeSame = string.Empty;
        public string PostalCodeSame
        {
            get => _postalCodeSame;
            set
            {
                if (_postalCodeSame == value) return;

                if (value != null)
                {
                    _postalCodeSame = value;
                    OnPropertyChanged(nameof(PostalCodeSame));
                }
            }
        }
        private string _addNumberSame = string.Empty;
        public string AddNumberSame
        {
            get => _addNumberSame;
            set
            {
                if (_addNumberSame == value) return;

                if (value != null)
                {
                    _addNumberSame = value;
                    OnPropertyChanged(nameof(AddNumberSame));
                }
            }
        }
        private CountryDropdownItem _countrySame = null;
        public CountryDropdownItem CountrySame
        {
            get => _countrySame;
            set
            {
                if (_countrySame == value) return;

                if (value != null)
                {
                    _countrySame = value;
                    OnPropertyChanged(nameof(CountrySame));
                }
            }
        }
        private StateDropdownItem _provinanceSame = null;
        public StateDropdownItem ProvinanceSame
        {
            get => _provinanceSame;
            set
            {
                if (_provinanceSame == value) return;

                if (value != null)
                {
                    _provinanceSame = value;
                    OnPropertyChanged(nameof(ProvinanceSame));
                }
            }
        }
        private CityDropdownItem _citySame = null;
        public CityDropdownItem CitySame
        {
            get => _citySame;
            set
            {
                if (_citySame == value) return;

                if (value != null)
                {
                    _citySame = value;
                    OnPropertyChanged(nameof(CitySame));
                }
            }
        }

        private OutletDropDowns _outletDropDowns = null;
        public OutletDropDowns OutletDropDowns
        {
            get => _outletDropDowns;
            set
            {
                if (_outletDropDowns == value) return;

                _outletDropDowns = value;
                OnPropertyChanged(nameof(OutletDropDowns));
            }
        }
        private bool _canExecute = true;
        public bool CanExecute
        {
            get => _canExecute;
            set
            {
                if (_canExecute == value) return;

                _canExecute = value;
                OnPropertyChanged(nameof(CanExecute));
            }
        }
        private GenericPickerModel _pickerModel { get; set; }
        public GenericPickerModel PickerModel
        {
            get
            {
                return _pickerModel;
            }
            set
            {
                if (_pickerModel == value) return;
                _pickerModel = value;
                try
                {
                    if (PickerModel != null && !string.IsNullOrEmpty(PickerModel.SelectedValue))
                    {


                        if (PickerModel.PickerId == "AddressCountryPicker")
                        {
                            if (PickerModel.PickerExtraData != null && PickerModel.PickerExtraData.ToString().Equals("same"))
                            {
                                CountrySame = OutletDropDowns?.country_dropdownSet?.Where(i => i.Landx50 == PickerModel.SelectedValue).FirstOrDefault();
                            }
                            else
                            {
                                Country = OutletDropDowns?.country_dropdownSet?.Where(i => i.Landx50 == PickerModel.SelectedValue).FirstOrDefault();
                            }
                        }
                        if (PickerModel.PickerId == "AddressStatePicker")
                        {
                            List<StateDropdownItem> states = OutletDropDowns?.State_dropdownSet;

                            if (PickerModel.PickerExtraData != null && PickerModel.PickerExtraData.ToString().Equals("same"))
                            {
                                ProvinanceSame = states.Where(i => i.Bezei == PickerModel.SelectedValue).FirstOrDefault();
                            }
                            else
                            {
                                Provinance = states.Where(i => i.Bezei == PickerModel.SelectedValue).FirstOrDefault();
                            }
                        }
                        if (PickerModel.PickerId == "AddressCityPicker")
                        {
                            if (PickerModel.PickerExtraData != null && PickerModel.PickerExtraData.ToString().Equals("same"))
                            {
                                CitySame = OutletDropDowns?.city_dropdownSet?.Where(i => i.CityName == PickerModel.SelectedValue).FirstOrDefault();
                            }
                            else
                            {
                                City = OutletDropDowns?.city_dropdownSet?.Where(i => i.CityName == PickerModel.SelectedValue).FirstOrDefault();
                            }
                        }

                    }

                }
                catch (Exception)
                {

                }

                OnPropertyChanged("PickerModel");
            }
        }
        public OutletDetails OutletDetails { get; set; }
        public AddressDetails AddressDetails { get; set; }

        #endregion

        #region commands
        public Command OnNextButtonClick { get; private set; }
        public ICommand OnPreButtonClick { get; private set; }
        public ICommand OnActivityItemButtonClick { get; private set; }
        public ICommand OnCountrySelectButtonClick { get; private set; }
        public ICommand OnProvinanceSelectButtonClick { get; private set; }
        public ICommand OnCitySelectButtonClick { get; private set; }
        #endregion

        #region Constructor
        public OutletDetailsAmendUpdatePageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            isMainOutletExists = false;
            OutletDetails = new OutletDetails();
            AddressDetails = new AddressDetails();
            ListOutletTypes = new List<string>();
            OnNextButtonClick = new Command(() =>
            {
                navigateToNext();
            }, () =>
            {
                return CanExecute;
            });
            OnPreButtonClick = new Command(() =>
            {
                selectedOutletItem = null;
                _navigationService.GoBack();
            });
            OnActivityItemButtonClick = new Command((_enum) => openNewActivity((EstablishmentOutletActivitiesTabsEnum)_enum));
            OnCountrySelectButtonClick = new Command((str) =>
            {
                try
                {
                    List<string> countryDropdownData = new List<string>();

                    foreach (CountryDropdownItem reportingBranch in OutletDropDowns?.country_dropdownSet)
                    {
                        if (!string.IsNullOrEmpty(reportingBranch.Landx50) && !string.IsNullOrWhiteSpace(reportingBranch.Landx50))
                        {
                            countryDropdownData.Add(reportingBranch.Landx50);

                        }
                    }

                    GenericPickerModel genericPickerModel = new GenericPickerModel();
                    genericPickerModel.PickerData = countryDropdownData;
                    if (str != null)
                    {
                        genericPickerModel.PickerExtraData = str.ToString();
                    }
                    genericPickerModel.PickerId = "AddressCountryPicker";

                    MopupService.Instance.PushAsync(new PickerPageView(genericPickerModel));
                }
                catch (GAZTUnlockAccountException ex)
                {

                }
                catch (InternetException ex)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        _navigationService.GoBack();
                    });
                }
                catch (Exception)
                {

                }
            });
            OnProvinanceSelectButtonClick = new Command((str) =>
            {
                List<StateDropdownItem> states = OutletDropDowns?.State_dropdownSet;
                if (str != null && str.ToString().Equals("same"))
                {
                    states = new List<StateDropdownItem>();
                    if (CountrySame != null && !string.IsNullOrWhiteSpace(CountrySame?.Landx50))
                    {
                        states.AddRange(OutletDropDowns?.State_dropdownSet?.Where(i => i.Land1 == CountrySame.Land1));
                    }
                }
                else
                {
                    states = new List<StateDropdownItem>();
                    if (Country != null && !string.IsNullOrWhiteSpace(Country?.Landx50))
                    {
                        states.AddRange(OutletDropDowns?.State_dropdownSet?.Where(i => i.Land1 == Country.Land1));
                    }
                }
                if (states?.Count > 0)
                {
                    try
                    {
                        List<string> countryDropdownData = new List<string>();

                        foreach (StateDropdownItem reportingBranch in states)
                        {
                            if (!string.IsNullOrEmpty(reportingBranch.Bezei) && !string.IsNullOrWhiteSpace(reportingBranch.Bezei))
                            {
                                countryDropdownData.Add(reportingBranch.Bezei);

                            }
                        }

                        GenericPickerModel genericPickerModel = new GenericPickerModel();
                        genericPickerModel.PickerData = countryDropdownData;
                        if (str != null)
                        {
                            genericPickerModel.PickerExtraData = str.ToString();
                        }
                        genericPickerModel.PickerId = "AddressStatePicker";

                        MopupService.Instance.PushAsync(new PickerPageView(genericPickerModel));
                    }
                    catch (GAZTUnlockAccountException ex)
                    {

                    }
                    catch (InternetException ex)
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                            _navigationService.GoBack();
                        });
                    }
                    catch (Exception)
                    {

                    }

                }
            });
            OnCitySelectButtonClick = new Command((str) =>
            {
                List<CityDropdownItem> cities = OutletDropDowns?.city_dropdownSet;
                if (str != null && str.ToString().Equals("same"))
                {
                    cities = new List<CityDropdownItem>();
                    if (CountrySame != null && !string.IsNullOrWhiteSpace(CountrySame?.Landx50) && ProvinanceSame != null && !string.IsNullOrWhiteSpace(ProvinanceSame?.Bezei))
                    {
                        cities.AddRange(OutletDropDowns?.city_dropdownSet?.Where(i => i.Country == CountrySame.Land1 && i.Region == ProvinanceSame.Bland));
                    }
                }
                else
                {
                    cities = new List<CityDropdownItem>();
                    if (Country != null && !string.IsNullOrWhiteSpace(Country?.Landx50) && Provinance != null && !string.IsNullOrWhiteSpace(Provinance?.Bezei))
                    {
                        cities.AddRange(OutletDropDowns?.city_dropdownSet?.Where(i => i.Country == Country.Land1 && i.Region == Provinance.Bland));
                    }
                }
                if (cities?.Count > 0)
                {
                    try
                    {
                        List<string> countryDropdownData = new List<string>();

                        foreach (CityDropdownItem reportingBranch in cities)
                        {
                            if (!string.IsNullOrEmpty(reportingBranch.CityName) && !string.IsNullOrWhiteSpace(reportingBranch.CityName))
                            {
                                countryDropdownData.Add(reportingBranch.CityName);

                            }
                        }

                        GenericPickerModel genericPickerModel = new GenericPickerModel();
                        genericPickerModel.PickerData = countryDropdownData;
                        if (str != null)
                        {
                            genericPickerModel.PickerExtraData = str.ToString();
                        }
                        genericPickerModel.PickerId = "AddressCityPicker";

                        MopupService.Instance.PushAsync(new PickerPageView(genericPickerModel));
                    }
                    catch (GAZTUnlockAccountException ex)
                    {

                    }
                    catch (InternetException ex)
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                            _navigationService.GoBack();
                        });
                    }
                    catch (Exception)
                    {

                    }

                }
            });
        }
        #endregion

        #region Method

        public void SetUIAvailability()
        {
            switch (App.ZAKATType)
            {
                case PageExecutionType.Amend:
                    OutletDetails.OutletName = true;
                    OutletDetails.OutletType = true;

                    AddressDetails.HouseNo = true;
                    AddressDetails.BuildinNo = true;
                    AddressDetails.Floor = true;
                    AddressDetails.Street = true;
                    AddressDetails.Quarter = true;
                    AddressDetails.PostelCode = true;
                    AddressDetails.AddNo = true;
                    AddressDetails.Country = true;
                    AddressDetails.Province = true;
                    AddressDetails.City = true;
                    AddressDetails.CBSameAsPhysical = true;
                    break;
                case PageExecutionType.Update:
                    OutletDetails.OutletName = true;
                    OutletDetails.OutletType = false;

                    AddressDetails.HouseNo = true;
                    AddressDetails.BuildinNo = true;
                    AddressDetails.Floor = true;
                    AddressDetails.Street = true;
                    AddressDetails.Quarter = true;
                    AddressDetails.PostelCode = true;
                    AddressDetails.AddNo = true;
                    AddressDetails.Country = true;
                    AddressDetails.Province = true;
                    AddressDetails.City = true;
                    AddressDetails.CBSameAsPhysical = true;
                    break;
                default:
                    break;
            }
        }
        public void OnAppearing()
        {
        }
        private void openNewActivity(EstablishmentOutletActivitiesTabsEnum _enum)
        {
            if (_enum == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
            {
                var mainactivity = taxPayerDetails?.Nreg_ActivitySet?.Where(i => i.Type == "ZS0004").ToList();
                if (mainactivity.Count > 0)
                {
                    _enum = EstablishmentOutletActivitiesTabsEnum.ActivityList;
                }
            }

            _navigationService.NavigateTo(App.ActivityItemAmendUpdatePage, new ActivityNavigationModels()
            {
                openedTab = _enum,
                taxPayerDetails = taxPayerDetails,
                nextNumber = newNumber,
                goBackAction = (List<Nreg_ActivityItem> list) =>
                {
                    addActivities(list);
                }
            }); ;
        }

        private async void navigateToNext()
        {
            CanExecute = false;
            if (await validateForm())
            {
                if (currentTab == EstablishmentRegistrationOutletTabsEnum.OutletDetail)
                {
                    if (!string.IsNullOrEmpty(validateCR?.Crname) || PreLoadedLicenseItem != null)
                    {
                        CanExecute = true;
                        currentTab = EstablishmentRegistrationOutletTabsEnum.ActivityDetails;
                    }
                    else
                    {
                        currentTab = EstablishmentRegistrationOutletTabsEnum.ActivityDetails;
                    }
                }
                else if (currentTab == EstablishmentRegistrationOutletTabsEnum.ActivityDetails)
                {
                    currentTab = EstablishmentRegistrationOutletTabsEnum.AddressDetails;
                }
                else if (currentTab == EstablishmentRegistrationOutletTabsEnum.AddressDetails)
                {
                    try
                    {
                        IsLoading = true;
                        DateTime.TryParseExact("9999/12/31", "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime maxDate);

                        taxPayerDetails?.Nreg_AddressSet?.Clear();
                        Nreg_AddressItem defaultAddress = new Nreg_AddressItem();
                        defaultAddress.HouseNum1 = HouseNumber;
                        defaultAddress.Building = BuildingNumber;
                        defaultAddress.Floor = FloorNumber;
                        defaultAddress.Street = Street;
                        defaultAddress.City2 = Quarter;
                        defaultAddress.PostCode1 = PostalCode;
                        defaultAddress.HouseNum2 = AddNumber;
                        defaultAddress.Country = Country.Land1;
                        defaultAddress.Region = Provinance.Bland;
                        defaultAddress.City1 = City.CityName;
                        defaultAddress.CityCode = City.CityCode;
                        defaultAddress.Sameasphy = PostalAsPhysical ? "X" : string.Empty;
                        defaultAddress.AddrType = "XXDEFAULT";
                        defaultAddress.Srcidentify = string.Format("O{0}", OutletActNumber);
                        defaultAddress.Begda = DateTime.UtcNow.ToString("yyyy-MM-ddThh:mm:ss");
                        defaultAddress.Endda = maxDate.ToString("yyyy-MM-ddThh:mm:ss");
                        taxPayerDetails?.Nreg_AddressSet?.Add(defaultAddress);

                        Nreg_AddressItem _address = new Nreg_AddressItem();
                        _address.HouseNum1 = HouseNumberSame;
                        _address.Building = BuildingNumberSame;
                        _address.Floor = FloorNumberSame;
                        _address.Street = StreetSame;
                        _address.City2 = QuarterSame;
                        _address.PostCode1 = PostalCodeSame;
                        _address.HouseNum2 = AddNumberSame;
                        _address.Country = CountrySame.Land1;
                        _address.Region = ProvinanceSame.Bland;
                        _address.City1 = CitySame.CityName;
                        _address.CityCode = CitySame.CityCode;
                        _address.Sameasphy = PostalAsPhysical ? "X" : string.Empty;
                        _address.AddrType = "0001";
                        _address.Srcidentify = $"O{OutletActNumber}";
                        _address.Begda = DateTime.UtcNow.ToString("yyyy-MM-ddThh:mm:ss");
                        _address.Endda = maxDate.ToString("yyyy-MM-ddThh:mm:ss");
                        taxPayerDetails?.Nreg_AddressSet?.Add(_address);

                        string oldMstFlasg = string.Empty;

                        if (ListOutlets != null && ListOutlets.Count > 0)
                        {
                            foreach (OutletItem nreg_OutletItem in ListOutlets)
                            {
                                if (nreg_OutletItem.Actno == OutletActNumber)
                                {
                                    oldMstFlasg = nreg_OutletItem.Oldmst;
                                    break;
                                }
                            }
                        }

                        taxPayerDetails?.Nreg_OutletSet?.Clear();
                        Nreg_OutletItem outletItem = new Nreg_OutletItem();
                        outletItem.Actnm = OutletName;
                        outletItem.Actno = OutletActNumber;
                        outletItem.Caltp = taxPayerDetails?.Caltp;
                        outletItem.Oldmst = oldMstFlasg;

                        outletItem.Actcat = SelectedOutletType == AppResources.MainOutlet ? "M" : SelectedOutletType == AppResources.SubOutlet ? "S" : "";
                        taxPayerDetails?.Nreg_OutletSet?.Add(outletItem);
                        taxPayerDetails.StepNumberx = "03";
                        taxPayerDetails.Gpart = App.LoginDataRetrieved.TIN;
                        taxPayerDetails.UserTypx = "TP";

                        //taxPayerDetails.off_notesSet = new Models.EstablishmentRegistration.OffNotesSet();
                        taxPayerDetails.off_notesSet = new List<OffNotes>();
                        taxPayerDetails.Nreg_BtnSet = new List<object>();

                        taxPayerDetails.Nreg_BtnSet = new List<object>();
                        await EstablishmentRegistrationWebServiceManager.ESTTaxPayerDetailPostService(taxPayerDetails);
                        IsLoading = false;

                        selectedOutletItem = null;
                        _navigationService.GoBack();
                    }
                    catch (Exception ex)
                    {
                        IsLoading = false;
                        if (ex is HTTPBadRequestException)
                        {
                            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        }
                    }
                    finally
                    {
                        CanExecute = true;
                    }
                }
            }
            SetUIAvailability();
            CanExecute = true;
        }
        private async void addActivities(List<Nreg_ActivityItem> list)
        {
            IsLoading = true;
            try
            {
                //taxPayerDetails?.Nreg_ActivitySet.results?.Clear();
                var newList = new List<Nreg_ActivityItem>();
                newList.AddRange(list);

                foreach (Nreg_ActivityItem newItem in newList)
                {
                    var count = taxPayerDetails?.Nreg_ActivitySet?.Where(i => i.Idnumber == newItem.Idnumber).ToList().Count();
                    if (count == 0)
                    {
                        taxPayerDetails?.Nreg_ActivitySet?.Add(newItem);
                    }
                }

                var _taxPayerDetails = await EstablishmentRegistrationWebServiceManager.ZakatAmendESTTaxPayerDetailGetService("03", App.LoginDataRetrieved.TIN, App.LoginDataRetrieved.Emailid, OutletActNumber, taxPayerDetails?.Fbnumx, taxPayerDetails?.Fbstax, taxPayerDetails?.Fbustx);
                taxPayerDetails?.AttDetSet?.Clear();
                taxPayerDetails?.AttDetSet?.AddRange(_taxPayerDetails?.AttDetSet);
            }
            catch (Exception)
            {
            }
            finally
            {
                IsLoading = false;
            }
        }
        private void navigateToPre()
        {
            if (currentTab == EstablishmentRegistrationOutletTabsEnum.AddressDetails)
            {
                currentTab = EstablishmentRegistrationOutletTabsEnum.ActivityDetails;
            }
            else if (currentTab == EstablishmentRegistrationOutletTabsEnum.ActivityDetails)
            {
                currentTab = EstablishmentRegistrationOutletTabsEnum.OutletDetail;
            }
            else if (currentTab == EstablishmentRegistrationOutletTabsEnum.OutletDetail)
            {
                _navigationService.GoBack();
            }
            SetUIAvailability();
        }
        private async void fetchTabDataAndBind(EstablishmentRegistrationOutletTabsEnum _enum)
        {
            try
            {
                IsLoading = true;
                if (_enum == EstablishmentRegistrationOutletTabsEnum.OutletDetail)
                {
                    clearFormData();
                    try
                    {
                        var _outletTempData = await EstablishmentRegistrationWebServiceManager.ESTOutletList(taxPayerDetails?.PortalUsrx, App.LoginDataRetrieved.TIN, taxPayerDetails?.Fbnumx);
                        _outletTempData.ForEach(_out =>
                        {
                            if (_out.Actcat == "M")
                            {
                                isMainOutletExists = true;
                            }
                        });
                        ListOutlets = _outletTempData;
                        ListOutletTypes.Clear();
                        ListOutletTypes.Add(AppResources.ESTMainOutlet);
                        ListOutletTypes.Add(AppResources.ESTSubOutlet);
                        if (isMainOutletExists)
                            SelectedOutletType = AppResources.ESTSubOutlet;
                        else
                            SelectedOutletType = AppResources.ESTMainOutlet;
                        if (selectedOutletItem != null)
                        {
                            newNumber = new OutletNumber()
                            {
                                Actno = selectedOutletItem?.Actno
                            };
                            OutletName = selectedOutletItem?.Actnm;
                            if (selectedOutletItem.Actcat == "M")
                            {
                                SelectedOutletType = AppResources.ESTMainOutlet;
                            }
                            else if (selectedOutletItem.Actcat == "S")
                            {
                                SelectedOutletType = AppResources.ESTSubOutlet;
                            }
                        }
                        else
                        {
                            newNumber = await EstablishmentRegistrationWebServiceManager.ESTOutletNumberESAmendUpdate(taxPayerDetails?.Fbnumx, App.LoginDataRetrieved.TIN);
                        }
                        OutletActNumber = newNumber == null || string.IsNullOrEmpty(newNumber?.Actno) ? "00000" : newNumber.Actno;

                        // OutletActNumber = $"{Int16.Parse(newNumber?.Actno):000}";
                        taxPayerDetails = await EstablishmentRegistrationWebServiceManager.ZakatAmendESTTaxPayerDetailGetService("03", App.LoginDataRetrieved.TIN, App.LoginDataRetrieved.Emailid, OutletActNumber, taxPayerDetails?.Fbnumx, taxPayerDetails?.Fbstax, taxPayerDetails?.Fbustx
                         );


                        //As per 5250 commented below code, in amend we no need to call this api.
                        //if (OutletActNumber == "00000")
                        //{
                        //    var preLoadedItems = taxPayerDetails?.Nreg_ActivitySet.results.Where(i => (new List<string> { "BUP002", "ZS0004" }).Contains(i.Type)).ToList();
                        //    if (preLoadedItems.Count == 1)
                        //    {
                        //        var preLoadedItem = preLoadedItems.FirstOrDefault();
                        //        if (preLoadedItem?.Type == "BUP002")
                        //        {
                        //            var result = await EstablishmentRegistrationWebServiceManager.ESTValidateCRNum(preLoadedItem?.Idnumber);
                        //            try
                        //            {
                        //                if (!string.IsNullOrEmpty(result))
                        //                {
                        //                    validateCR = JsonConvert.DeserializeObject<ValidateCR>(result);
                        //                }
                        //                if (validateCR.Crnum == null)
                        //                {
                        //                    PrepareError(result);
                        //                }
                        //            }
                        //            catch (Exception ex)
                        //            {

                        //            }


                        //            if (!string.IsNullOrEmpty(validateCR?.Crname))
                        //            {
                        //                OutletName = validateCR?.Crname;
                        //                validateCR.Crnum = preLoadedItem?.Idnumber;
                        //            }
                        //            PreLoadedLicenseItem = null;
                        //        }
                        //        else if (preLoadedItem?.Type == "ZS0004")
                        //        {
                        //            validateCR = null;
                        //            PreLoadedLicenseItem = preLoadedItem;
                        //        }
                        //        else
                        //        {
                        //            validateCR = null;
                        //            PreLoadedLicenseItem = null;
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    validateCR = null;
                        //    PreLoadedLicenseItem = null;
                        //}
                    }
                    catch (Exception)
                    {


                    }
                }
                else if (_enum == EstablishmentRegistrationOutletTabsEnum.AddressDetails)
                {
                    OutletDropDowns = await EstablishmentRegistrationWebServiceManager.ESTOutletDropDowns();
                    List<CountryDropdownItem> countries = OutletDropDowns?.country_dropdownSet;
                    List<StateDropdownItem> states = OutletDropDowns?.State_dropdownSet;

                    if (selectedOutletItem != null)
                    {

                        Nreg_AddressItem defaultAddress = taxPayerDetails?.Nreg_AddressSet.Where(i => i.Srcidentify.Equals(string.Format("O{0}", OutletActNumber)) && i.AddrType.Equals("XXDEFAULT")).FirstOrDefault();
                        HouseNumber = defaultAddress?.HouseNum1;
                        BuildingNumber = defaultAddress?.Building;
                        FloorNumber = defaultAddress?.Floor;
                        Street = defaultAddress?.Street;
                        Quarter = defaultAddress?.City2;
                        PostalCode = defaultAddress?.PostCode1;
                        AddNumber = defaultAddress?.HouseNum2;
                        Country = OutletDropDowns?.country_dropdownSet?.Where(i => i.Land1 == defaultAddress?.Country).FirstOrDefault();
                        foreach (var countryName in countries)
                        {
                            states = new List<StateDropdownItem>();
                            if (Country != null && !string.IsNullOrWhiteSpace(Country?.Landx50))
                            {
                                states.AddRange(OutletDropDowns?.State_dropdownSet?.Where(i => i.Land1 == Country.Land1));
                            }
                        }
                        Provinance = states.Where(i => i.Bland == defaultAddress?.Region).FirstOrDefault();
                        City = OutletDropDowns?.city_dropdownSet?.Where(i => i.CityCode == defaultAddress?.CityCode && i.CityName == defaultAddress?.City1).FirstOrDefault();
                        PostalAsPhysical = defaultAddress?.Sameasphy == "X";
                        Nreg_AddressItem _address = taxPayerDetails?.Nreg_AddressSet.Where(i => i.Srcidentify.Equals(string.Format("O{0}", OutletActNumber)) && i.AddrType.Equals("0001")).FirstOrDefault();
                        HouseNumberSame = _address?.HouseNum1;
                        BuildingNumberSame = _address?.Building;
                        FloorNumberSame = _address?.Floor;
                        StreetSame = _address?.Street;
                        QuarterSame = _address?.City2;
                        PostalCodeSame = _address?.PostCode1;
                        AddNumberSame = _address?.HouseNum2;
                        CountrySame = OutletDropDowns?.country_dropdownSet?.Where(i => i.Land1 == _address?.Country).FirstOrDefault();

                        foreach (var countryName in countries)
                        {
                            states = new List<StateDropdownItem>();
                            if (CountrySame != null && !string.IsNullOrWhiteSpace(CountrySame?.Landx50))
                            {
                                states.AddRange(OutletDropDowns?.State_dropdownSet?.Where(i => i.Land1 == CountrySame.Land1));
                            }
                        }
                        ProvinanceSame = states.Where(i => i.Bland == _address?.Region).FirstOrDefault();

                        CitySame = OutletDropDowns?.city_dropdownSet?.Where(i => i.CityCode == _address?.CityCode && i.CityName == _address?.City1).FirstOrDefault();
                    }
                    else
                    {
                        if (idItem != null)
                        {
                            List<OutletAddress> addressess = await EstablishmentRegistrationWebServiceManager.ESTOutletAddress(idItem?.Type, idItem?.Idnumber, App.LoginDataRetrieved.TIN);
                            if (addressess.Count > 0)
                            {
                                if (addressess.Count == 1)
                                {
                                    populateAddress(addressess.FirstOrDefault());
                                }
                                else
                                {
                                    AddressPickerPopPage addressPickerPopPage = new AddressPickerPopPage(addressess)
                                    {
                                        CloseWhenBackgroundIsClicked = false
                                    };
                                    addressPickerPopPage.OnItemSelect = (item) =>
                                    {
                                        populateAddress(item as OutletAddress);
                                    };
                                    await MopupService.Instance.PushAsync(addressPickerPopPage);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {


            }
            finally
            {
                IsLoading = false;
            }
        }
        private void PrepareError(string result)
        {
            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(result);
            var errorID = string.Empty;
            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
            {
                string errorCode = errorMesg.error.innererror.errordetails[0].code;

                WebServiceManager.ErrorMessageForUnlockAccount = errorMesg.error.innererror.errordetails[0].message;

                if (errorCode.Contains("206"))
                {
                    WebServiceManager.ErrorMessageForUnlockAccount = "206";
                }
                else if (errorCode.Contains("112"))
                {
                    WebServiceManager.ErrorMessageForUnlockAccount = "112";
                }
                else if (errorCode.Contains("896"))
                {
                    errorID = errorCode;
                }

                string line1 = "";

                for (int i = 0; i < errorMesg.error.innererror.errordetails.Count; i++)
                {
                    line1 = line1 + " " + errorMesg.error.innererror.errordetails[i].message;
                }
                WebServiceManager.ErrorMessageForUnlockAccount = line1;

                String WithReplacedString = WebServiceManager.ErrorMessageForUnlockAccount.Replace("An exception was raised", string.Empty);

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    if (errorID.Contains("896"))
                    {
                        await MopupService.Instance.PushAsync(new ErrorMessagePopup(AppResources.Error896));
                    }
                    else
                    {
                        await _dialogService.ShowMessage(WithReplacedString, AppResources.ZError);
                    }

                });

                //throw new GAZTErrorException(WithReplacedString);
            }
        }
        private void populateAddress(OutletAddress address)
        {
            BuildingNumber = address.BuildingNo;
            FloorNumber = address.UnitNo;
            Street = address.StreetName;
            Quarter = address.DistrictName;
            PostalCode = address.Zipcode;
            AddNumber = address.AdditionalNo;
        }
        private async Task<bool> validateForm()
        {
            if (currentTab == EstablishmentRegistrationOutletTabsEnum.ActivityDetails)
            {

                if (App.ZAKATType == PageExecutionType.Update && taxPayerDetails?.Nreg_ActivitySet?.Count == 0)
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAddActivity));
                    return false;
                }
                var mainactivity = taxPayerDetails?.Nreg_ActivitySet?.Where(i => i.Actcat == "M").ToList();
                var count = mainactivity.Count();

                if (App.ZAKATType == PageExecutionType.Update && taxPayerDetails?.Nreg_ActivitySet?.Count != 0 && count == 0)
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAddActivity));
                    return false;
                }
            }
            else if (currentTab == EstablishmentRegistrationOutletTabsEnum.AddressDetails)
            {

                if (AddressDetails.BuildinNo && string.IsNullOrWhiteSpace(BuildingNumber))
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAddBuildingNumber));
                    return false;
                }
                else if (AddressDetails.Floor && string.IsNullOrWhiteSpace(FloorNumber))
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAddFloorNumber));
                    return false;
                }
                else if (AddressDetails.Street && string.IsNullOrWhiteSpace(Street))
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAddStreet));
                    return false;
                }
                else if (AddressDetails.Quarter && string.IsNullOrWhiteSpace(Quarter))
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAddQuarter));
                    return false;
                }
                else if (AddressDetails.PostelCode && string.IsNullOrWhiteSpace(PostalCode))
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAddPostal));
                    return false;
                }
                else if (AddressDetails.PostelCode && PostalCode.Length <= 1)
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAddPostalCode5));
                    return false;
                }
                else if (AddressDetails.PostelCode && PostalCode == "12345")
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTInValidPostalCode));
                    return false;
                }
                else if (AddressDetails.Country && Country == null)
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAddCountry));
                    return false;
                }
                else if (AddressDetails.Province && Provinance == null)
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAddProvinance));
                    return false;
                }
                else if (AddressDetails.City && City == null)
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAddCity));
                    return false;
                }
                if (AddressDetails.CBSameAsPhysical)
                {
                    if (string.IsNullOrWhiteSpace(BuildingNumberSame))
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePAddBuildingNumber));
                        return false;
                    }
                    else if (string.IsNullOrWhiteSpace(FloorNumberSame))
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePAddFloorNumber));
                        return false;
                    }
                    else if (string.IsNullOrWhiteSpace(StreetSame))
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePAddStreet));
                        return false;
                    }
                    else if (string.IsNullOrWhiteSpace(QuarterSame))
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePAddQuarter));
                        return false;
                    }
                    else if (string.IsNullOrWhiteSpace(PostalCodeSame))
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePAddPostal));
                        return false;
                    }
                    else if (PostalCodeSame.Length <= 1)
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePAddPostalCode5));
                        return false;
                    }
                    else if (PostalCodeSame == "12345")
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp("12345 is invalid postal's postal codes"));
                        return false;
                    }
                    else if (CountrySame == null)
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePAddCountry));
                        return false;
                    }
                    else if (ProvinanceSame == null)
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePAddProvinance));
                        return false;
                    }
                    else if (CitySame == null)
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePAddCity));
                        return false;
                    }
                }
            }
            else if (currentTab == EstablishmentRegistrationOutletTabsEnum.OutletDetail)
            {
                if (OutletDetails.OutletName && string.IsNullOrWhiteSpace(OutletName))
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateOutletName));
                    return false;
                }
            }
            return true;
        }
        private void clearFormData()
        {
            CanExecute = true;
            OutletName = string.Empty;
            if (taxPayerDetails != null)
            {
                taxPayerDetails?.Nreg_ActivitySet?.Clear();
            }
            PostalAsPhysical = false;

            HouseNumber = string.Empty;
            BuildingNumber = string.Empty;
            FloorNumber = string.Empty;
            Street = string.Empty;
            Quarter = string.Empty;
            PostalCode = string.Empty;
            AddNumber = string.Empty;
            Country = null;
            Provinance = null;
            City = null;
            HouseNumberSame = string.Empty;
            BuildingNumberSame = string.Empty;
            FloorNumberSame = string.Empty;
            StreetSame = string.Empty;
            QuarterSame = string.Empty;
            PostalCode = string.Empty;
            AddNumberSame = string.Empty;
            CountrySame = null;
            ProvinanceSame = null;
            CitySame = null;
        }

        #endregion
    }
}