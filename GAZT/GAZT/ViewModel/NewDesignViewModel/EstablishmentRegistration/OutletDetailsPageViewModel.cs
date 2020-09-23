using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.Models.EstablishmentRegistration;
using EGAZT.Views.NewDesign.Common;
using EGAZT.Views.NewDesign.EstablishmentRegistrationPages;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.EstablishmentRegistration
{
    public class OutletDetailsPageViewModel : BaseViewModel
    {
        #region Variable
        //public List<Nreg_ActivityItem> activityItems = new List<Nreg_ActivityItem>();
        public TaxPayerDetails taxPayerDetails { get; set; } = null;
        //public bool editModeEnabled { get; set; } = false;
        private OutletNumber newNumber = null;
        public Nreg_IdItem idItem { get; set; } = null;
        private ValidateCR validateCR = null;
        private Nreg_ActivityItem PreLoadedLicenseItem = null;
        private EstablishmentRegistrationOutletTabsEnum _currentTab = EstablishmentRegistrationOutletTabsEnum.OutletDetail;
        public EstablishmentRegistrationOutletTabsEnum currentTab
        {
            get => _currentTab;
            set
            {
                _currentTab = value;
                RaisePropertyChanged(nameof(currentTab));
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
                _currenrIndex = value;
                RaisePropertyChanged(nameof(CurrentIndex));
                //MarkComplete = _currenrIndex == MaxIndex;
                //RaisePropertyChanged(nameof(MarkComplete));
            }
        }

        public string SelectedTabText { get; private set; } = AppResources.TinDeregistrationRegistrationOutlets;
        private string _selectedOutletTabText = AppResources.ESTAddressDetails;
        public string SelectedOutletTabText
        {
            get => _selectedOutletTabText;
            private set
            {
                _selectedOutletTabText = value;
                RaisePropertyChanged(nameof(SelectedOutletTabText));
            }
        }

        private string _nxtButtonLabel = AppResources.ZZNext;
        public string NxtButtonLabel
        {
            get => _nxtButtonLabel;
            set
            {
                _nxtButtonLabel = value;
                RaisePropertyChanged(nameof(NxtButtonLabel));
            }
        }

        private string _outletName = string.Empty;
        public string OutletName
        {
            get => _outletName;
            set
            {
                if (value != null)
                {
                    _outletName = value;
                    RaisePropertyChanged(nameof(OutletName));
                }
            }
        }
        private string _outletActNumber = string.Empty;
        public string OutletActNumber
        {
            get => _outletActNumber;
            set
            {
                if (value != null)
                {
                    _outletActNumber = value;
                    RaisePropertyChanged(nameof(OutletActNumber));
                }
            }
        }


        private string _houseNumber = string.Empty;
        public string HouseNumber
        {
            get => _houseNumber;
            set
            {
                if (value != null)
                {
                    _houseNumber = value;
                    RaisePropertyChanged(nameof(HouseNumber));
                }
            }
        }
        private string _buildingNumber = string.Empty;
        public string BuildingNumber
        {
            get => _buildingNumber;
            set
            {
                if (value != null)
                {
                    _buildingNumber = value;
                    RaisePropertyChanged(nameof(BuildingNumber));
                }
            }
        }
        private string _floorNumber = string.Empty;
        public string FloorNumber
        {
            get => _floorNumber;
            set
            {
                if (value != null)
                {
                    _floorNumber = value;
                    RaisePropertyChanged(nameof(FloorNumber));
                }
            }
        }
        private string _street = string.Empty;
        public string Street
        {
            get => _street;
            set
            {
                if (value != null)
                {
                    _street = value;
                    RaisePropertyChanged(nameof(Street));
                }
            }
        }
        private string _quarter = string.Empty;
        public string Quarter
        {
            get => _quarter;
            set
            {
                if (value != null)
                {
                    _quarter = value;
                    RaisePropertyChanged(nameof(Quarter));
                }
            }
        }
        private string _postalCode = string.Empty;
        public string PostalCode
        {
            get => _postalCode;
            set
            {
                if (value != null)
                {
                    _postalCode = value;
                    RaisePropertyChanged(nameof(PostalCode));
                }
            }
        }
        private string _addNumber = string.Empty;
        public string AddNumber
        {
            get => _addNumber;
            set
            {
                if (value != null)
                {
                    _addNumber = value;
                    RaisePropertyChanged(nameof(AddNumber));
                }
            }
        }
        private CountryDropdownItem _country = null;
        public CountryDropdownItem Country
        {
            get => _country;
            set
            {
                if (value != null)
                {
                    _country = value;
                    RaisePropertyChanged(nameof(Country));
                }
            }
        }
        private StateDropdownItem _provinance = null;
        public StateDropdownItem Provinance
        {
            get => _provinance;
            set
            {
                if (value != null)
                {
                    _provinance = value;
                    RaisePropertyChanged(nameof(Provinance));
                }
            }
        }
        private CityDropdownItem _city = null;
        public CityDropdownItem City
        {
            get => _city;
            set
            {
                if (value != null)
                {
                    _city = value;
                    RaisePropertyChanged(nameof(City));
                }
            }
        }
        private bool _postalAsPhysical = false;
        public bool PostalAsPhysical
        {
            get => _postalAsPhysical;
            set
            {
                _postalAsPhysical = value;
                RaisePropertyChanged(nameof(PostalAsPhysical));
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
                if (value != null)
                {
                    _houseNumberSame = value;
                    RaisePropertyChanged(nameof(HouseNumberSame));
                }
            }
        }
        private string _buildingNumberSame = string.Empty;
        public string BuildingNumberSame
        {
            get => _buildingNumberSame;
            set
            {
                if (value != null)
                {
                    _buildingNumberSame = value;
                    RaisePropertyChanged(nameof(BuildingNumberSame));
                }
            }
        }
        private string _floorNumberSame = string.Empty;
        public string FloorNumberSame
        {
            get => _floorNumberSame;
            set
            {
                if (value != null)
                {
                    _floorNumberSame = value;
                    RaisePropertyChanged(nameof(FloorNumberSame));
                }
            }
        }
        private string _streetSame = string.Empty;
        public string StreetSame
        {
            get => _streetSame;
            set
            {
                if (value != null)
                {
                    _streetSame = value;
                    RaisePropertyChanged(nameof(StreetSame));
                }
            }
        }
        private string _quarterSame = string.Empty;
        public string QuarterSame
        {
            get => _quarterSame;
            set
            {
                if (value != null)
                {
                    _quarterSame = value;
                    RaisePropertyChanged(nameof(QuarterSame));
                }
            }
        }
        private string _postalCodeSame = string.Empty;
        public string PostalCodeSame
        {
            get => _postalCodeSame;
            set
            {
                if (value != null)
                {
                    _postalCodeSame = value;
                    RaisePropertyChanged(nameof(PostalCodeSame));
                }
            }
        }
        private string _addNumberSame = string.Empty;
        public string AddNumberSame
        {
            get => _addNumberSame;
            set
            {
                if (value != null)
                {
                    _addNumberSame = value;
                    RaisePropertyChanged(nameof(AddNumberSame));
                }
            }
        }
        private CountryDropdownItem _countrySame = null;
        public CountryDropdownItem CountrySame
        {
            get => _countrySame;
            set
            {
                if (value != null)
                {
                    _countrySame = value;
                    RaisePropertyChanged(nameof(CountrySame));
                }
            }
        }
        private StateDropdownItem _provinanceSame = null;
        public StateDropdownItem ProvinanceSame
        {
            get => _provinanceSame;
            set
            {
                if (value != null)
                {
                    _provinanceSame = value;
                    RaisePropertyChanged(nameof(ProvinanceSame));
                }
            }
        }
        private CityDropdownItem _citySame = null;
        public CityDropdownItem CitySame
        {
            get => _citySame;
            set
            {
                if (value != null)
                {
                    _citySame = value;
                    RaisePropertyChanged(nameof(CitySame));
                }
            }
        }

        private OutletDropDowns _outletDropDowns = null;
        public OutletDropDowns OutletDropDowns
        {
            get => _outletDropDowns;
            set
            {
                //if (value != null)
                //{
                _outletDropDowns = value;
                RaisePropertyChanged(nameof(OutletDropDowns));
                //}
            }
        }
        private bool _canExecute = true;
        public bool CanExecute
        {
            get => _canExecute;
            set
            {
                _canExecute = value;
                RaisePropertyChanged(nameof(CanExecute));
            }
        }
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
        public OutletDetailsPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            OnNextButtonClick = new Command(() => {
                navigateToNext();
            }, () =>
            {
                return CanExecute;
            });
            OnPreButtonClick = new Command(() => _navigationService.GoBack());
            //editModeEnabled = false;
            OnActivityItemButtonClick = new Command((_enum) => openNewActivity((EstablishmentOutletActivitiesTabsEnum)_enum));
            OnCountrySelectButtonClick = new Command((str) =>
            {
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(OutletDropDowns?.country_dropdownSet?.results);
                poupWindow.OnItemSelect = (item) =>
                {
                    try
                    {
                        if (str != null && str.ToString().Equals("same"))
                        {
                            CountrySame = item as CountryDropdownItem;
                        }
                        else
                        {
                            Country = item as CountryDropdownItem;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                    }
                };
                PopupNavigation.Instance.PushAsync(poupWindow);
            });
            OnProvinanceSelectButtonClick = new Command((str) =>
            {
                List<StateDropdownItem> states = OutletDropDowns?.State_dropdownSet?.results;
                if (str != null && str.ToString().Equals("same"))
                {
                    states = new List<StateDropdownItem>();
                    if (CountrySame != null && !string.IsNullOrWhiteSpace(CountrySame?.Landx50))
                    {
                        states.AddRange(OutletDropDowns?.State_dropdownSet?.results.Where(i => i.Land1 == CountrySame.Land1));
                    }
                }
                else
                {
                    states = new List<StateDropdownItem>();
                    if (Country != null && !string.IsNullOrWhiteSpace(Country?.Landx50))
                    {
                        states.AddRange(OutletDropDowns?.State_dropdownSet?.results.Where(i => i.Land1 == Country.Land1));
                    }
                }
                if (states?.Count > 0)
                {
                    ListPopUpViewPage poupWindow = new ListPopUpViewPage(states);
                    poupWindow.OnItemSelect = (item) =>
                    {
                        try
                        {
                            if (str != null && str.ToString().Equals("same"))
                            {
                                ProvinanceSame = item as StateDropdownItem;
                            }
                            else
                            {
                                Provinance = item as StateDropdownItem;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.StackTrace);
                        }
                    };
                    PopupNavigation.Instance.PushAsync(poupWindow);
                }
            });
            OnCitySelectButtonClick = new Command((str) =>
            {
                List<CityDropdownItem> cities = OutletDropDowns?.city_dropdownSet?.results;
                if (str != null && str.ToString().Equals("same"))
                {
                    cities = new List<CityDropdownItem>();
                    if (CountrySame != null && !string.IsNullOrWhiteSpace(CountrySame?.Landx50) && ProvinanceSame != null && !string.IsNullOrWhiteSpace(ProvinanceSame?.Bezei))
                    {
                        cities.AddRange(OutletDropDowns?.city_dropdownSet?.results.Where(i => i.Country == CountrySame.Land1 && i.Region == ProvinanceSame.Bland));
                    }
                }
                else
                {
                    cities = new List<CityDropdownItem>();
                    if (Country != null && !string.IsNullOrWhiteSpace(Country?.Landx50) && Provinance != null && !string.IsNullOrWhiteSpace(Provinance?.Bezei))
                    {
                        cities.AddRange(OutletDropDowns?.city_dropdownSet?.results.Where(i => i.Country == Country.Land1 && i.Region == Provinance.Bland));
                    }
                }
                if (cities?.Count > 0)
                {
                    ListPopUpViewPage poupWindow = new ListPopUpViewPage(cities);
                    poupWindow.OnItemSelect = (item) =>
                    {
                        try
                        {
                            if (str != null && str.ToString().Equals("same"))
                            {
                                CitySame = item as CityDropdownItem;
                            }
                            else
                            {
                                City = item as CityDropdownItem;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.StackTrace);
                        }
                    };
                    PopupNavigation.Instance.PushAsync(poupWindow);
                }
            });
        }
        #endregion

        #region Method
        public void OnAppearing()
        {
        }
        private void openNewActivity(EstablishmentOutletActivitiesTabsEnum _enum)
        {
            Console.WriteLine(_enum);

            //if (_enum == EstablishmentOutletActivitiesTabsEnum.CRDetails)
            //{
            //    var mainactivity = taxPayerDetails?.Nreg_ActivitySet.results?.Where(i => i.Type == "BUP002").ToList();

            //    if (mainactivity.Count() == 1 && editModeEnabled == false)
            //    {
            //        return;
            //    }
            //}
            if (_enum == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
            {
                var mainactivity = taxPayerDetails?.Nreg_ActivitySet.results?.Where(i => i.Type == "ZS0004").ToList();
                //if (mainactivity.Count() == 4 && editModeEnabled == false)
                //{
                //    return;
                //}
                if (/*editModeEnabled == true ||*/ mainactivity.Count > 0)
                {
                    _enum = EstablishmentOutletActivitiesTabsEnum.ActivityList;
                }
            }


            _navigationService.NavigateTo(App.ActivityItemPage, new ActivityNavigationModels()
            {
                openedTab = _enum,
                taxPayerDetails = taxPayerDetails,
                nextNumber = newNumber,
                //EditEnabledMode = editModeEnabled,
                //newActivityItems = activityItems,
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
                        _navigationService.NavigateTo(App.ActivityItemPage, new ActivityNavigationModels()
                        {
                            openedTab = PreLoadedLicenseItem != null ? EstablishmentOutletActivitiesTabsEnum.LicenseDetails : EstablishmentOutletActivitiesTabsEnum.CRDetails,
                            taxPayerDetails = taxPayerDetails,
                            nextNumber = newNumber,
                            validateCR = validateCR,
                            validateLicense = PreLoadedLicenseItem,
                            //cRActivityItem = taxPayerDetails?.Nreg_ActivitySet.results.Where(i => IDs.Contains(i.Type)).FirstOrDefault(),
                            //newActivityItems = activityItems,
                            goBackAction = (List<Nreg_ActivityItem> list) =>
                            {
                                addActivities(list);
                                currentTab = EstablishmentRegistrationOutletTabsEnum.ActivityDetails;
                            }
                        });
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

                        taxPayerDetails?.Nreg_AddressSet.results?.Clear();
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
                        defaultAddress.Begda = DateTime.UtcNow;
                        defaultAddress.Endda = maxDate;
                        taxPayerDetails?.Nreg_AddressSet.results?.Add(defaultAddress);

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
                        _address.Srcidentify = string.Format("O{0}", OutletActNumber);
                        _address.Begda = DateTime.UtcNow;
                        _address.Endda = maxDate;
                        taxPayerDetails?.Nreg_AddressSet.results?.Add(_address);

                        taxPayerDetails?.Nreg_OutletSet?.results?.Clear();
                        Nreg_OutletItem outletItem = new Nreg_OutletItem();
                        outletItem.Actnm = OutletName;
                        outletItem.Actno = OutletActNumber;
                        outletItem.Caltp = "G";
                        outletItem.Actcat = OutletActNumber == "000" ? "M" : "S";
                        //outletItem.Conatt = "X";
                        taxPayerDetails?.Nreg_OutletSet?.results?.Add(outletItem);
                        taxPayerDetails.StepNumberx = "03";
                        taxPayerDetails.Gpartx = App.LoginDataRetrieved.TIN;
                        taxPayerDetails.UserTypx = "TP";
                        await WebServiceManager.ESTTaxPayerDetailPostService(taxPayerDetails);
                        IsLoading = false;
                        _navigationService.GoBack();
                    }
                    catch (Exception ex)
                    {
                        IsLoading = false;
                        Console.WriteLine(ex.StackTrace);
                        if (ex is HTTPBadRequestException)
                        {
                            //editModeEnabled = true;
                            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        }
                    }
                    finally
                    {
                        CanExecute = true;
                    }
                }
            }
            CanExecute = true;
            //else
            //{
            //    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));
            //}
        }
        private async void addActivities(List<Nreg_ActivityItem> list)
        {
            IsLoading = true;
            try
            {
                taxPayerDetails?.Nreg_ActivitySet.results?.Clear();
                var newList = new List<Nreg_ActivityItem>();
                //newList.Insert(0, new Nreg_ActivityItem()
                //{
                //    ValidDateType = "X"
                //});
                newList.AddRange(list);
                //newList.Insert(2, new Nreg_ActivityItem()
                //{
                //    Type = "ZS0007",
                //    ValidDateType = "X",
                //    Actno = OutletActNumber,
                //    Actcat = "M"
                //});
                taxPayerDetails?.Nreg_ActivitySet.results?.AddRange(newList);
                var _taxPayerDetails = await WebServiceManager.ESTTaxPayerDetailGetService("03", App.LoginDataRetrieved.TIN, App.LoginDataRetrieved.Emailid, OutletActNumber, taxPayerDetails?.Fbnumx);
                taxPayerDetails?.AttDetSet.results?.Clear();
                taxPayerDetails?.AttDetSet.results?.AddRange(_taxPayerDetails?.AttDetSet.results);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
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
        }


        private async void fetchTabDataAndBind(EstablishmentRegistrationOutletTabsEnum _enum)
        {
            try
            {
                IsLoading = true;
                if (_enum == EstablishmentRegistrationOutletTabsEnum.OutletDetail)
                {
                    clearFormData();
                    newNumber = await WebServiceManager.ESTOutletNumber(taxPayerDetails?.Fbnumx);
                    OutletActNumber = $"{Int16.Parse(newNumber?.Actno):000}";
                    taxPayerDetails = await WebServiceManager.ESTTaxPayerDetailGetService("03", App.LoginDataRetrieved.TIN, App.LoginDataRetrieved.Emailid, OutletActNumber, taxPayerDetails?.Fbnumx);
                    if (OutletActNumber == "000")
                    {
                        var preLoadedItem = taxPayerDetails?.Nreg_ActivitySet.results.Where(i => (new List<string> { "BUP002", "ZS0004" }).Contains(i.Type)).FirstOrDefault();
                        if (preLoadedItem?.Type == "BUP002")
                        {
                            validateCR = await WebServiceManager.ESTValidateCRNum(preLoadedItem?.Idnumber);
                            if (!string.IsNullOrEmpty(validateCR?.Crname))
                            {
                                OutletName = validateCR?.Crname;
                                validateCR.Crnum = preLoadedItem?.Idnumber;
                            }
                            PreLoadedLicenseItem = null;
                        }
                        else if (preLoadedItem?.Type == "ZS0004")
                        {
                            validateCR = null;
                            PreLoadedLicenseItem = preLoadedItem;
                        }
                        else
                        {
                            validateCR = null;
                            PreLoadedLicenseItem = null;
                        }
                    }
                    else
                    {
                        validateCR = null;
                        PreLoadedLicenseItem = null;
                    }
                }
                else if (_enum == EstablishmentRegistrationOutletTabsEnum.AddressDetails)
                {
                    OutletDropDowns = await WebServiceManager.ESTOutletDropDowns();
                    if (idItem != null)
                    {
                        List<OutletAddress> addressess = await WebServiceManager.ESTOutletAddress(idItem?.Type, idItem?.Idnumber, App.LoginDataRetrieved.TIN);
                        if (addressess.Count > 0)
                        {
                            if (addressess.Count == 1)
                            {
                                populateAddress(addressess.FirstOrDefault());
                            }
                            else
                            {
                                AddressPickerPopPage addressPickerPopPage = new AddressPickerPopPage(addressess) {
                                    CloseWhenBackgroundIsClicked = false
                                };
                                addressPickerPopPage.OnItemSelect = (item) => {
                                    populateAddress(item as OutletAddress);
                                };
                                await PopupNavigation.Instance.PushAsync(addressPickerPopPage);
                            }
                        }
                    }
                }
                else
                {
                    //activityList = await WebServiceManager.ESTOutletGetActivitySetsList();
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                IsLoading = false;
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
                if (taxPayerDetails?.Nreg_ActivitySet.results?.Count == 0)
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAddActivity));
                    return false;
                }
                var mainactivity = taxPayerDetails?.Nreg_ActivitySet.results?.Where(i => i.Actcat == "M").ToList();
                var count = mainactivity.Count();
                if (taxPayerDetails?.Nreg_ActivitySet.results?.Count != 0 && count == 0)
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAddActivity));
                    return false;
                }
            }
            else if (currentTab == EstablishmentRegistrationOutletTabsEnum.AddressDetails)
            {
                //if (string.IsNullOrWhiteSpace(HouseNumber))
                //{
                //    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAddHouseNumber));
                //    return false;
                //}
                //else
                if (string.IsNullOrWhiteSpace(BuildingNumber))
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAddBuildingNumber));
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(FloorNumber))
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAddFloorNumber));
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(Street))
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAddStreet));
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(Quarter))
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAddQuarter));
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(PostalCode))
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAddPostal));
                    return false;
                }
                else if (PostalCode.Length <= 1)
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAddPostalCode5));
                    return false;
                }
                else if (PostalCode == "12345")
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTInValidPostalCode));
                    return false;
                }
                //else if (string.IsNullOrWhiteSpace(AddNumber))
                //{
                //    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAddAdditional));
                //    return false;
                //}
                else if (Country == null)
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAddCountry));
                    return false;
                }
                else if (Provinance == null)
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAddProvinance));
                    return false;
                }
                else if (City == null)
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAddCity));
                    return false;
                }
                //if (string.IsNullOrWhiteSpace(HouseNumberSame))
                //{
                //    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePAddHouseNumber));
                //    return false;
                //}
                else
                if (string.IsNullOrWhiteSpace(BuildingNumberSame))
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePAddBuildingNumber));
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(FloorNumberSame))
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePAddFloorNumber));
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(StreetSame))
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePAddStreet));
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(QuarterSame))
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePAddQuarter));
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(PostalCodeSame))
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePAddPostal));
                    return false;
                }
                else if (PostalCodeSame.Length <= 1)
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePAddPostalCode5));
                    return false;
                }
                else if (PostalCodeSame == "12345")
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp("12345 is invalid postal's postal codes"));
                    return false;
                }
                //else if (string.IsNullOrWhiteSpace(AddNumberSame))
                //{
                //    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePAddAdditional));
                //    return false;
                //}
                else if (CountrySame == null)
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePAddCountry));
                    return false;
                }
                else if (ProvinanceSame == null)
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePAddProvinance));
                    return false;
                }
                else if (CitySame == null)
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePAddCity));
                    return false;
                }
            }
            else if (currentTab == EstablishmentRegistrationOutletTabsEnum.OutletDetail)
            {
                if (string.IsNullOrWhiteSpace(OutletName))
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateOutletName));
                    return false;
                }
            }
            return true;
        }
        private void clearFormData()
        {
            CanExecute = true;
            OutletName = string.Empty;
            taxPayerDetails?.Nreg_ActivitySet.results?.Clear();
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
