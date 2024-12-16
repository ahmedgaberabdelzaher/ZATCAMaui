using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using Newtonsoft.Json;
using Mopups.Services;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.EstablishmentRegistration;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using static ZATCAMAUI.Models.ErrorMessage;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Views.NewDesign.EstablishmentAmendUpdatePages;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.EstablishmentAmendUpdateViewModel
{

    public class ActivityItemAmendUpdatePageViewModel : BaseViewModel
    {
        #region variables

        public bool IsEditingMode { get; set; }
        public EstablishmentOutletActivitiesTabsEnum PageType { get; set; }
        private bool _displayCompleteDetailsLabel;
        public bool DisplayCompleteDetailsLabel
        {
            get => _displayCompleteDetailsLabel;
            set
            {
                if (_displayCompleteDetailsLabel == value) return;
                _displayCompleteDetailsLabel = value;
                OnPropertyChanged(nameof(DisplayCompleteDetailsLabel));
            }
        }
        public TaxPayerDetails taxPayerDetails { get; set; } = null;
        private ActivitySetsList activityList = null;
        public OutletNumber newNumber { get; set; } = null;
        public ValidateCR validateCR { get; set; } = null;
        public Nreg_ActivityItem validateLicense { get; set; } = null;
        public List<Nreg_ActivityItem> NregActivityList = new List<Nreg_ActivityItem>();

        public List<NregMulSet> NregMulActivityList = new List<NregMulSet>();
        public Nreg_ActivityItem SelectedLicenseItem = null;
        public Nreg_ActivityItem SelectedCRItem = null;
        public ActicityListDelegate goBackAction = null;
        private PopUpServiceModel dataModel = null;

        private EstablishmentOutletActivitiesTabsEnum _currentTab = EstablishmentOutletActivitiesTabsEnum.CRDetails;
        public EstablishmentOutletActivitiesTabsEnum CurrentTab
        {
            get => _currentTab;
            set
            {
                // if (_currentTab == value) return;

                _currentTab = value;
                OnPropertyChanged(nameof(CurrentTab));
                switch (value)
                {
                    case EstablishmentOutletActivitiesTabsEnum.LicenseDetails:
                        ActivityTitle = AppResources.ESTAddLicense;
                        break;
                    case EstablishmentOutletActivitiesTabsEnum.ActivityList:
                        ActivityTitle = AppResources.ESTLicenseDetails;
                        break;
                    case EstablishmentOutletActivitiesTabsEnum.CRDetails:
                    default:
                        ActivityTitle = AppResources.ESTCommercialRegistration;
                        break;
                }
                fetchTabDataAndBind();
            }
        }
        private string _activityTitle = AppResources.ESTCommercialRegistration;
        public string ActivityTitle
        {
            get => _activityTitle;
            private set
            {
                if (_activityTitle == value) return;

                _activityTitle = value;
                OnPropertyChanged(nameof(ActivityTitle));
            }
        }

        public ActivityDetails ActivityDetails { get; set; }
        private LicenseDetails _licenseDetails;
        public LicenseDetails LicenseDetails
        {
            get => _licenseDetails;
            set
            {
                if (_licenseDetails == value) return;

                _licenseDetails = value;
                OnPropertyChanged(nameof(LicenseDetails));
            }
        }

        private bool CanExecuteClickCommand(object args) => EnableInputFields;
        private bool CanIssueByExecuteClickCommand(object args) => EnableIssueByDropDown;

        private CountryDropdownItem _cRIssueCountry = null;
        public CountryDropdownItem CRIssueCountry
        {
            get => _cRIssueCountry;
            set
            {
                if (_cRIssueCountry == value) return;
                _cRIssueCountry = value;
                OnPropertyChanged(nameof(CRIssueCountry));
            }
        }
        private string _cRIssueBy = null;
        public string CRIssueBy
        {
            get => _cRIssueBy;
            set
            {
                if (_cRIssueBy == value) return;

                _cRIssueBy = value;
                OnPropertyChanged(nameof(CRIssueBy));
            }
        }
        private bool _AddLicenseEnabled = true;
        public bool AddLicenseEnabled
        {
            get => _AddLicenseEnabled;
            set
            {
                if (_AddLicenseEnabled == value) return;

                _AddLicenseEnabled = value;
                OnPropertyChanged(nameof(AddLicenseEnabled));
            }
        }

        private List<NregMulSet> _mulActivitySetData = null;
        public List<NregMulSet> mulActivitySetData
        {
            get => _mulActivitySetData;
            set
            {
                if (_mulActivitySetData == value) return;
                _mulActivitySetData = value;
                OnPropertyChanged(nameof(mulActivitySetData));
            }
        }


        private string _cRNationalNumber = string.Empty;
        public string CRNationalNumber
        {
            get => _cRNationalNumber;
            set
            {
                if (_cRNationalNumber == value) return;

                _cRNationalNumber = value;
                OnPropertyChanged(nameof(CRNationalNumber));
            }
        }
        private bool _EnableCRNationalInputField = true;
        public bool EnableCRNationalInputField
        {
            get => _EnableCRNationalInputField;
            private set
            {
                if (_EnableCRNationalInputField == value) return;

                _EnableCRNationalInputField = value;
                OnPropertyChanged(nameof(EnableCRNationalInputField));
            }
        }

        private bool _UpdateButtonEnabled = true;
        public bool UpdateButtonEnabled
        {
            get => _UpdateButtonEnabled;
            set
            {
                if (_UpdateButtonEnabled == value) return;

                _UpdateButtonEnabled = value;
                OnPropertyChanged(nameof(UpdateButtonEnabled));
            }
        }

        public bool AddNewLicenseTapped = false;

        private CityDropdownItem _cRIssueCity = null;
        public CityDropdownItem CRIssueCity
        {
            get => _cRIssueCity;
            set
            {
                if (_cRIssueCity == value) return;

                _cRIssueCity = value;
                OnPropertyChanged(nameof(CRIssueCity));
            }
        }
        private string _cRNumber = string.Empty;
        public string CRNumber
        {
            get => _cRNumber;
            set
            {
                if (_cRNumber == value) return;

                _cRNumber = value;
                OnPropertyChanged(nameof(CRNumber));
            }
        }
        private bool _enableInputFields = true;
        public bool EnableInputFields
        {
            get => _enableInputFields;
            set
            {
                if (_enableInputFields == value) return;

                _enableInputFields = value;
                OnIssueCountrySelectButtonClick.ChangeCanExecute();
                OnIssueCitySelectButtonClick.ChangeCanExecute();
                OnPropertyChanged(nameof(EnableInputFields));
            }
        }
        private bool _enableIssueByDropDown = true;
        public bool EnableIssueByDropDown
        {
            get => _enableIssueByDropDown;
            set
            {
                if (_enableIssueByDropDown == value) return;

                _enableIssueByDropDown = value;
                OnIssueBySelectButtonClick.ChangeCanExecute();
                OnPropertyChanged(nameof(EnableIssueByDropDown));
            }
        }
        private bool _enableCRInputField = true;
        public bool EnableCRInputField
        {
            get => _enableCRInputField;
            private set
            {
                if (_enableCRInputField == value) return;

                _enableCRInputField = value;
                OnPropertyChanged(nameof(EnableCRInputField));
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
        private ObservableCollection<object> _selectedCRValidFromDate;
        public ObservableCollection<object> SelectedCRValidFromDate
        {
            get
            {
                return _selectedCRValidFromDate;
            }
            set
            {
                if (_selectedCRValidFromDate == value) return;

                _selectedCRValidFromDate = value;
                OnPropertyChanged(nameof(SelectedCRValidFromDate));
            }
        }
        private ObservableCollection<object> _selectedCRValidFromHijiriDate;
        public ObservableCollection<object> SelectedCRValidFromHijiriDate
        {
            get
            {
                return _selectedCRValidFromHijiriDate;
            }
            set
            {
                if (_selectedCRValidFromHijiriDate == value) return;

                _selectedCRValidFromHijiriDate = value;
                OnPropertyChanged(nameof(SelectedCRValidFromHijiriDate));
            }
        }
        private string _cRValidFrom = string.Empty;
        public string CRValidFrom
        {
            get => _cRValidFrom;
            set
            {
                if (_cRValidFrom == value) return;

                _cRValidFrom = value;
                OnPropertyChanged(nameof(CRValidFrom));
            }
        }
        private string _displayCRValidFrom = string.Empty;
        public string DisplayCRValidFrom
        {
            get => _displayCRValidFrom;
            set
            {
                if (_displayCRValidFrom == value) return;

                _displayCRValidFrom = value;
                OnPropertyChanged(nameof(DisplayCRValidFrom));
            }
        }
        public int _attachmentCount = 0;
        public int AttachmentCount
        {
            get
            {
                return _attachmentCount;
            }
            set
            {
                if (_attachmentCount == value) return;

                _attachmentCount = value;
                OnPropertyChanged(nameof(AttachmentCount));
            }
        }
        private bool _mainActivity = false;
        public bool MainActivity
        {
            get => _mainActivity;
            set
            {
                if (_mainActivity == value) return;

                _mainActivity = value;
                OnPropertyChanged(nameof(MainActivity));
            }
        }
        private ActGroupSet _cRMainGroup = null;
        public ActGroupSet CRMainGroup
        {
            get => _cRMainGroup;
            set
            {
                if (_cRMainGroup == value) return;

                if (value != null)
                {
                    _cRMainGroup = value;
                    OnPropertyChanged(nameof(CRMainGroup));
                }
            }
        }
        private ActGroupSet _cRSubGroup = null;
        public ActGroupSet CRSubGroup
        {
            get => _cRSubGroup;
            set
            {
                if (_cRSubGroup == value) return;

                _cRSubGroup = value;
                OnPropertyChanged(nameof(CRSubGroup));
            }
        }
        private ActGroupSet _cRAcitivity;
        public ActGroupSet CRAcitivity
        {
            get => _cRAcitivity;
            set
            {
                if (_cRAcitivity == value) return;

                _cRAcitivity = value;
                OnPropertyChanged(nameof(CRAcitivity));
            }
        }
        private ObservableCollection<Attachment> _cRsCopies = new ObservableCollection<Attachment>();
        public ObservableCollection<Attachment> CRsCopies
        {
            get => _cRsCopies;
            set
            {
                if (_cRsCopies == value) return;

                if (value != null)
                {
                    _cRsCopies = value;
                    OnPropertyChanged(nameof(CRsCopies));
                }
            }
        }
        private ObservableCollection<Attachment> _transferCRsCopies = new ObservableCollection<Attachment>();
        public ObservableCollection<Attachment> TransferCRsCopies
        {
            get => _transferCRsCopies;
            set
            {
                if (_transferCRsCopies == value) return;

                if (value != null)
                {
                    _transferCRsCopies = value;
                    OnPropertyChanged(nameof(TransferCRsCopies));
                }
            }
        }
        private ObservableCollection<object> _selectedValidFromDate;
        public ObservableCollection<object> SelectedValidFromDate
        {
            get
            {
                return _selectedValidFromDate;
            }
            set
            {
                if (_selectedValidFromDate == value) return;

                _selectedValidFromDate = value;
                OnPropertyChanged(nameof(SelectedValidFromDate));
            }
        }
        private ObservableCollection<object> _selectedValidFromHijiriDate;
        public ObservableCollection<object> SelectedValidFromHijiriDate
        {
            get
            {
                return _selectedValidFromHijiriDate;
            }
            set
            {
                if (_selectedValidFromHijiriDate == value) return;

                _selectedValidFromHijiriDate = value;
                OnPropertyChanged(nameof(SelectedValidFromHijiriDate));
            }
        }
        private string _validFrom = string.Empty;
        public string ValidFrom
        {
            get => _validFrom;
            set
            {
                if (_validFrom == value) return;

                _validFrom = value;
                OnPropertyChanged(nameof(ValidFrom));
            }
        }
        private string _displayValidFrom = string.Empty;
        public string DisplayValidFrom
        {
            get => _displayValidFrom;
            set
            {
                if (_displayValidFrom == value) return;

                _displayValidFrom = value;
                OnPropertyChanged(nameof(DisplayValidFrom));
            }
        }
        private CountryDropdownItem _licenseIssueCountry = null;
        public CountryDropdownItem LicenseIssueCountry
        {
            get => _licenseIssueCountry;
            set
            {
                if (_licenseIssueCountry == value) return;

                _licenseIssueCountry = value;
                OnPropertyChanged(nameof(LicenseIssueCountry));
            }
        }
        private string _licenseIssueBy = null;
        public string LicenseIssueBy
        {
            get => _licenseIssueBy;
            set
            {
                if (_licenseIssueBy == value) return;

                _licenseIssueBy = value;
                OnPropertyChanged(nameof(LicenseIssueBy));
            }
        }
        private CityDropdownItem _licenseIssueCity = null;
        public CityDropdownItem LicenseIssueCity
        {
            get => _licenseIssueCity;
            set
            {
                if (_licenseIssueCity == value) return;

                _licenseIssueCity = value;
                OnPropertyChanged(nameof(LicenseIssueCity));
            }
        }
        private string _licenseNumber = string.Empty;
        public string LicenseNumber
        {
            get => _licenseNumber;
            set
            {
                if (_licenseNumber == value) return;

                _licenseNumber = value;
                OnPropertyChanged(nameof(LicenseNumber));
            }
        }
        private ActGroupSet _licenseMainGroup = null;
        public ActGroupSet LicenseMainGroup
        {
            get => _licenseMainGroup;
            set
            {
                if (_licenseMainGroup == value) return;

                _licenseMainGroup = value;
                OnPropertyChanged(nameof(LicenseMainGroup));
            }
        }
        private ActGroupSet _licenseSubGroup = null;
        public ActGroupSet LicenseSubGroup
        {
            get => _licenseSubGroup;
            set
            {
                if (_licenseSubGroup == value) return;

                _licenseSubGroup = value;
                OnPropertyChanged(nameof(LicenseSubGroup));
            }
        }
        private ActGroupSet _licenseAcitivity;
        public ActGroupSet LicenseAcitivity
        {
            get => _licenseAcitivity;
            set
            {
                if (_licenseAcitivity == value) return;

                _licenseAcitivity = value;
                OnPropertyChanged(nameof(LicenseAcitivity));
            }
        }
        private ObservableCollection<Attachment> _licensesCopies = new ObservableCollection<Attachment>();
        public ObservableCollection<Attachment> LicensesCopies
        {
            get => _licensesCopies;
            set
            {
                if (_licensesCopies == value) return;

                _licensesCopies = value;
                OnPropertyChanged(nameof(LicensesCopies));
            }
        }
        private List<Nreg_ActivityItem> _licenseData = new List<Nreg_ActivityItem>();
        public List<Nreg_ActivityItem> LicenseData
        {
            get => _licenseData;
            private set
            {
                if (_licenseData == value) return;

                _licenseData = value;
                OnPropertyChanged(nameof(LicenseData));
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
        private string _crName = string.Empty;
        public string CrName
        {
            get => _crName;
            set
            {
                if (_crName == value) return;

                _crName = value;
                OnPropertyChanged(nameof(CrName));
            }
        }
        private string _licenseName = string.Empty;
        public string LicenseName
        {
            get => _licenseName;
            set
            {
                if (_licenseName == value) return;

                _licenseName = value;
                OnPropertyChanged(nameof(LicenseName));
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

                        if (PickerModel.PickerId == "MainGroupPicker")
                        {
                            if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                                CRMainGroup = activityList?.act_groupSet?.Where(i => i.Text == PickerModel.SelectedValue).FirstOrDefault(); ;
                            if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                                LicenseMainGroup = activityList?.act_groupSet?.Where(i => i.Text == PickerModel.SelectedValue).FirstOrDefault();
                        }

                        if (PickerModel.PickerId == "SubGroupPicker")
                        {
                            if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                                CRSubGroup = activityList?.act_subgroupSet?.Where(i => i.Text == PickerModel.SelectedValue).FirstOrDefault(); ;
                            if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                                LicenseSubGroup = activityList?.act_subgroupSet?.Where(i => i.Text == PickerModel.SelectedValue).FirstOrDefault();

                        }
                        if (PickerModel.PickerId == "ActivityPicker")
                        {
                            if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                            {
                                CRAcitivity = activityList?.activitySet?.Where(i => i.Text == PickerModel.SelectedValue).FirstOrDefault(); ;
                            }
                            if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                            {
                                LicenseAcitivity = activityList?.activitySet?.Where(i => i.Text == PickerModel.SelectedValue).FirstOrDefault(); ;

                            }
                        }
                        if (PickerModel.PickerId == "LicenseCountryPicker")
                        {
                            if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                            {
                                CRIssueCountry = OutletDropDowns.country_dropdownSet.Where(i => i.Landx50 == PickerModel.SelectedValue).FirstOrDefault();

                                if (App.IsArabic)
                                {

                                    CRIssueBy = CRIssueCountry.Land1 == "SA" ? ZATCAConstants.ArIssueBy["90702"] : ZATCAConstants.ArIssueBy["90718"];
                                }
                                else
                                {
                                    CRIssueBy = CRIssueCountry.Land1 == "SA" ? ZATCAConstants.EnIssueBy["90702"] : ZATCAConstants.EnIssueBy["90718"];
                                }
                                CRIssueCity = null;
                            }
                            if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                            {
                                LicenseIssueCountry = OutletDropDowns.country_dropdownSet.Where(i => i.Landx50 == PickerModel.SelectedValue).FirstOrDefault();

                                LicenseIssueCity = null;
                            }
                        }
                        if (PickerModel.PickerId == "LicenseIssueByPicker")
                        {
                            if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                                CRIssueBy = PickerModel.SelectedValue;
                            if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                                LicenseIssueBy = PickerModel.SelectedValue;
                        }
                        if (PickerModel.PickerId == "LicenseCityPicker")
                        {
                            if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                                CRIssueCity = OutletDropDowns?.city_dropdownSet?.Where(i => i.CityName == PickerModel.SelectedValue).FirstOrDefault();

                            if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                                LicenseIssueCity = OutletDropDowns?.city_dropdownSet?.Where(i => i.CityName == PickerModel.SelectedValue).FirstOrDefault();
                        }

                    }

                }
                catch (Exception)
                {
                }

                OnPropertyChanged("PickerModel");
            }
        }

        #endregion

        #region commands
        public ICommand OnActivitiesButtonClick { get; set; }
        public Command OnNextButtonClick { get; private set; }
        public Command OnUpdateButtonClick { get; private set; }
        public ICommand OnPreButtonClick { get; private set; }
        public Command OnIssueCountrySelectButtonClick { get; set; }
        public Command OnIssueBySelectButtonClick { get; set; }
        public Command OnIssueCitySelectButtonClick { get; set; }
        public ICommand OnTransferCopyOfCRChoiceButtonClick { get; set; }
        public ICommand OnMainGroupSelectButtonClick { get; set; }
        public ICommand OnSubGroupSelectButtonClick { get; set; }
        public ICommand OnAcitivitySelectButtonClick { get; set; }
        public ICommand OnNewLicenseButtonClick { get; private set; }
        public ICommand TappedOnAttachmentInformationIcon { get; set; }
        public ICommand OnTransferCopyOfLicenseChoiceButtonClick { get; set; }
        public ICommand OnDeleteCRsCopyButtonClick { get; set; }
        public ICommand OnLicenseSelected { get; set; }
        public ICommand OnDeleteTransferCRsCopyButtonClick { get; set; }
        public ICommand OnDeleteLicenseCopyButtonClick { get; set; }
        #endregion

        #region Constructor
        public ActivityItemAmendUpdatePageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            ActivityDetails = new ActivityDetails();
            LicenseDetails = new LicenseDetails();
            OnActivitiesButtonClick = new Command<string>(DisplayActivityPopUp);
            OnNextButtonClick = new Command(async () => await navigateToNext(), () => CanExecute);
            OnUpdateButtonClick = new Command(async () => await updateActivityCrOrLicense());

            OnPreButtonClick = new Command(() =>
            {

                if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                {
                    CurrentTab = EstablishmentOutletActivitiesTabsEnum.ActivityList;
                }
                else
                {
                    _navigationService.GoBack();
                }

            });


            OnNewLicenseButtonClick = new Command(() =>
            {
                AddNewLicenseTapped = true;

                if (AddLicenseEnabled)
                {
                    CurrentTab = EstablishmentOutletActivitiesTabsEnum.LicenseDetails;
                }
                else
                {
                    CurrentTab = EstablishmentOutletActivitiesTabsEnum.ActivityList;
                }
            });
            OnIssueCountrySelectButtonClick = new Command(async (object o) =>
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
                    //genericPickerModel.PickerTitle = AppResources.TinDeregistrationReason;
                    genericPickerModel.PickerId = "LicenseCountryPicker";

                    await MopupService.Instance.PushAsync(new PickerPageView(genericPickerModel));
                }
                catch (GAZTUnlockAccountException)
                {
                }
                catch (InternetException ex)
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                }
                catch (Exception)
                {

                }

            }, CanExecuteClickCommand);
            OnIssueBySelectButtonClick = new Command(async (object o) =>
            {
                try
                {
                    List<string> countryDropdownData = new List<string>();
                    if (App.IsArabic)
                    {
                        foreach (string reportingBranch in ZATCAConstants.ArIssueBy.Values)
                        {
                            if (!string.IsNullOrEmpty(reportingBranch) && !string.IsNullOrWhiteSpace(reportingBranch))
                            {
                                countryDropdownData.Add(reportingBranch);

                            }
                        }
                    }
                    else
                    {
                        foreach (string reportingBranch in ZATCAConstants.EnIssueBy.Values)
                        {
                            if (!string.IsNullOrEmpty(reportingBranch) && !string.IsNullOrWhiteSpace(reportingBranch))
                            {
                                countryDropdownData.Add(reportingBranch);

                            }
                        }
                    }


                    GenericPickerModel genericPickerModel = new GenericPickerModel();
                    genericPickerModel.PickerData = countryDropdownData;
                    //genericPickerModel.PickerTitle = AppResources.TinDeregistrationReason;
                    genericPickerModel.PickerId = "LicenseIssueByPicker";

                    await MopupService.Instance.PushAsync(new PickerPageView(genericPickerModel));
                }
                catch (GAZTUnlockAccountException)
                {
                }
                catch (InternetException ex)
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                }
                catch (Exception)
                {
                }

            }, CanIssueByExecuteClickCommand);
            OnIssueCitySelectButtonClick = new Command(async (object o) =>
            {
                var filterCities = OutletDropDowns?.city_dropdownSet?.Where(i =>
                {
                    if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                    {
                        return i.Country == CRIssueCountry?.Land1;
                    }
                    else
                    {
                        return i.Country == LicenseIssueCountry?.Land1;
                    }
                }).ToList();
                if (filterCities.Count > 0)
                {
                    try
                    {
                        List<string> countryDropdownData = new List<string>();

                        foreach (CityDropdownItem reportingBranch in filterCities)
                        {
                            if (!string.IsNullOrEmpty(reportingBranch.CityName) && !string.IsNullOrWhiteSpace(reportingBranch.CityName))
                            {
                                countryDropdownData.Add(reportingBranch.CityName);

                            }
                        }
                        GenericPickerModel genericPickerModel = new GenericPickerModel();
                        genericPickerModel.PickerData = countryDropdownData;
                        //genericPickerModel.PickerTitle = AppResources.TinDeregistrationReason;
                        genericPickerModel.PickerId = "LicenseCityPicker";

                        await MopupService.Instance.PushAsync(new PickerPageView(genericPickerModel));
                    }
                    catch (GAZTUnlockAccountException)
                    {
                    }
                    catch (InternetException ex)
                    {
                        await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        _navigationService.GoBack();
                    }
                    catch (Exception)
                    {
                    }

                }
            }, CanExecuteClickCommand);
            OnTransferCopyOfCRChoiceButtonClick = new Command(async (type) =>
            {
                var typeValue = type as string;
                if (CRsCopies.Count < 5 && typeValue == "RG01")
                {
                    await AddAttachment(type as string);
                }
                else if (TransferCRsCopies.Count < 5 && typeValue == "RG12")
                {
                    await AddAttachment(type as string);
                }
                else if (TransferCRsCopies.Count == 5 || CRsCopies.Count == 5)
                {
                    await _dialogService.ShowError(AppResources.ZMaximumnoof5attachmentscanbeuploaded, "Information", "Ok", null);
                }

            });
            OnTransferCopyOfLicenseChoiceButtonClick = new Command(async (type) =>
            {
                if (LicensesCopies.Count < 5)
                {
                    await AddAttachment(type as string);
                }
                else
                {
                    await _dialogService.ShowError(AppResources.ZMaximumnoof5attachmentscanbeuploaded, "Information", "Ok", null);
                }
            });

            OnMainGroupSelectButtonClick = new Command(async () =>
            {
                var dropDownData = activityList?.act_groupSet?.ToList();
                try
                {
                    List<string> reportingBranchData = new List<string>();

                    foreach (ActGroupSet reportingBranch in activityList?.act_groupSet?.ToList())
                    {
                        if (!string.IsNullOrEmpty(reportingBranch.Text) && !string.IsNullOrWhiteSpace(reportingBranch.Text))
                        {
                            reportingBranchData.Add(reportingBranch.Text);

                        }
                    }

                    GenericPickerModel genericPickerModel = new GenericPickerModel();
                    genericPickerModel.PickerData = reportingBranchData;
                    //genericPickerModel.PickerTitle = AppResources.TinDeregistrationReason;
                    genericPickerModel.PickerId = "MainGroupPicker";

                    await MopupService.Instance.PushAsync(new PickerPageView(genericPickerModel));
                }
                catch (GAZTUnlockAccountException ex)
                {
                    throw new GAZTErrorException(ex.Message.ToString());

                }
                catch (InternetException ex)
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                }
                catch (Exception)
                {
                }

            });
            OnSubGroupSelectButtonClick = new Command(async () =>
            {
                List<ActGroupSet> subGroupList = new List<ActGroupSet>();
                var dropDownData = new List<ActGroupSet>();
                if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                    subGroupList = activityList?.act_subgroupSet?.Where(i => i.IndSector.StartsWith(CRMainGroup?.IndSector)).ToList();
                if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                    subGroupList = activityList?.act_subgroupSet?.Where(i => i.IndSector.StartsWith(LicenseMainGroup?.IndSector)).ToList();


                try
                {
                    List<string> reportingBranchData = new List<string>();

                    foreach (ActGroupSet reportingBranch in subGroupList)
                    {
                        if (!string.IsNullOrEmpty(reportingBranch.Text) && !string.IsNullOrWhiteSpace(reportingBranch.Text))
                        {
                            reportingBranchData.Add(reportingBranch.Text);

                        }
                    }

                    GenericPickerModel genericPickerModel = new GenericPickerModel();
                    genericPickerModel.PickerData = reportingBranchData;
                    //genericPickerModel.PickerTitle = AppResources.TinDeregistrationReason;
                    genericPickerModel.PickerId = "SubGroupPicker";

                    await MopupService.Instance.PushAsync(new PickerPageView(genericPickerModel));
                }
                catch (GAZTUnlockAccountException ex)
                {
                    throw new GAZTErrorException(ex.Message.ToString());

                }
                catch (InternetException ex)
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                }
                catch (Exception)
                { }

            });
            OnAcitivitySelectButtonClick = new Command(async () =>
            {
                List<ActGroupSet> subGroupList = new List<ActGroupSet>();

                var dropDownData = new List<ActGroupSet>();
                if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                    subGroupList = activityList?.activitySet?.Where(i => i.IndSector.StartsWith(CRSubGroup?.IndSector)).ToList();
                if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                    subGroupList = activityList?.activitySet?.Where(i => i.IndSector.StartsWith(LicenseSubGroup?.IndSector)).ToList();


                try
                {
                    List<string> reportingBranchData = new List<string>();

                    foreach (ActGroupSet reportingBranch in subGroupList)
                    {
                        if (!string.IsNullOrEmpty(reportingBranch.Text) && !string.IsNullOrWhiteSpace(reportingBranch.Text))
                        {
                            reportingBranchData.Add(reportingBranch.Text);

                        }
                    }

                    GenericPickerModel genericPickerModel = new GenericPickerModel();
                    genericPickerModel.PickerData = reportingBranchData;
                    genericPickerModel.PickerId = "ActivityPicker";

                    await MopupService.Instance.PushAsync(new PickerPageView(genericPickerModel));
                }
                catch (GAZTUnlockAccountException ex)
                {
                    throw new GAZTErrorException(ex.Message.ToString());

                }
                catch (InternetException ex)
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                }
                catch (Exception)
                { }

            });

            TappedOnAttachmentInformationIcon = new Command(() =>
            ShowAlertPopup(
                AppResources.ESTAttachmentSizeNotfication
                + Environment.NewLine
                + AppResources.ZZChooseonlyfilewithextensionForZAKAT
                + Environment.NewLine + AppResources.ZMaximumnoof5attachmentscanbeuploaded
                ));

            OnDeleteCRsCopyButtonClick = new Command((item) => OnDeleteAttachment(item as Attachment, "RG01"));
            OnDeleteTransferCRsCopyButtonClick = new Command((item) => OnDeleteAttachment(item as Attachment, "RG12"));
            OnDeleteLicenseCopyButtonClick = new Command((item) => OnDeleteAttachment(item as Attachment, "RG02"));
            OnLicenseSelected = new Command((item) => OpenLicenseFormInEditMode(item as Nreg_ActivityItem));

        }

        #endregion

        #region Method

        public void SetUIAvailability(bool IsActivityDetailsDataExist = false, EstablishmentOutletActivitiesTabsEnum type = EstablishmentOutletActivitiesTabsEnum.ActivityList)
        {
            switch (App.ZAKATType)
            {
                case PageExecutionType.Amend:
                    if (IsActivityDetailsDataExist)
                    {

                        DisplayCompleteDetailsLabel = false;
                        ActivityDetails.IssueCountry = false;
                        ActivityDetails.IssueBy = false;
                        ActivityDetails.IssueCity = false;
                        ActivityDetails.CRNo = false;
                        ActivityDetails.ValidFrom = false;
                        ActivityDetails.MainActivity = false;
                        ActivityDetails.IsMainActivityVisible = false;
                        ActivityDetails.CRCopy = true;
                        ActivityDetails.DeleteCRcopy = true;
                        ActivityDetails.TransferCRCopy = false;
                        ActivityDetails.DeleteTransferCRCopy = false;
                        ActivityDetails.IsTransferCRCopyVisible = false;
                        ActivityDetails.MainGroup = true;
                        ActivityDetails.SubGroup = true;
                        ActivityDetails.Activity = true;

                        LicenseDetails.IssueCountry = false;
                        LicenseDetails.IssueBy = false;
                        LicenseDetails.IssueCity = false;
                        LicenseDetails.ValidFrom = false;
                        LicenseDetails.LicenseNo = false;
                        LicenseDetails.MainActivity = false;
                        LicenseDetails.IsMainActivityVisible = false;
                        LicenseDetails.LicenseCopy = true;
                        LicenseDetails.DeleteLicenseCopy = true;
                        LicenseDetails.MainGroup = false;
                        LicenseDetails.SubGroup = false;
                        LicenseDetails.Activity = false;
                        LicenseDetails.LicenseName = false;
                    }
                    else
                    {
                        ActivityTitle = type == EstablishmentOutletActivitiesTabsEnum.CRDetails ? AppResources.ESTCommercialRegistration : AppResources.ESTAddLicense;
                        DisplayCompleteDetailsLabel = true;
                        ActivityDetails.IssueCountry = true;
                        ActivityDetails.IssueBy = true;
                        ActivityDetails.IssueCity = true;
                        ActivityDetails.CRNo = true;
                        ActivityDetails.ValidFrom = true;
                        ActivityDetails.MainActivity = true;
                        ActivityDetails.IsMainActivityVisible = true;
                        ActivityDetails.CRCopy = true;
                        ActivityDetails.DeleteCRcopy = true;
                        ActivityDetails.TransferCRCopy = true;
                        ActivityDetails.DeleteTransferCRCopy = true;
                        ActivityDetails.IsTransferCRCopyVisible = true;
                        ActivityDetails.MainGroup = true;
                        ActivityDetails.SubGroup = true;
                        ActivityDetails.Activity = true;

                        LicenseDetails.IssueCountry = true;
                        LicenseDetails.IssueBy = true;
                        LicenseDetails.IssueCity = true;
                        LicenseDetails.ValidFrom = true;
                        LicenseDetails.LicenseNo = true;
                        LicenseDetails.MainActivity = true;
                        LicenseDetails.IsMainActivityVisible = true;
                        LicenseDetails.LicenseCopy = true;
                        LicenseDetails.DeleteLicenseCopy = true;
                        LicenseDetails.MainGroup = true;
                        LicenseDetails.SubGroup = true;
                        LicenseDetails.Activity = true;
                        LicenseDetails.LicenseName = true;
                    }

                    break;
                case PageExecutionType.Update:
                    if (IsActivityDetailsDataExist)
                    {
                        ActivityTitle = type == EstablishmentOutletActivitiesTabsEnum.CRDetails ? AppResources.ESTCommercialRegistration : AppResources.ESTLicenseDetails;
                        DisplayCompleteDetailsLabel = false;
                        ActivityDetails.IssueCountry = false;
                        ActivityDetails.IssueBy = false;
                        ActivityDetails.IssueCity = true;
                        ActivityDetails.CRNo = false;
                        ActivityDetails.ValidFrom = false;
                        ActivityDetails.MainActivity = true;
                        ActivityDetails.IsMainActivityVisible = true;
                        ActivityDetails.CRCopy = false;
                        ActivityDetails.DeleteCRcopy = false;
                        ActivityDetails.TransferCRCopy = true;
                        ActivityDetails.DeleteTransferCRCopy = false;
                        ActivityDetails.IsTransferCRCopyVisible = true;
                        ActivityDetails.MainGroup = true;
                        ActivityDetails.SubGroup = true;
                        ActivityDetails.Activity = true;

                        LicenseDetails.IssueCountry = false;
                        LicenseDetails.IssueBy = false;
                        LicenseDetails.IssueCity = true;
                        LicenseDetails.ValidFrom = false;
                        LicenseDetails.LicenseNo = false;
                        LicenseDetails.LicenseName = false;
                        LicenseDetails.MainActivity = true;
                        LicenseDetails.IsMainActivityVisible = true;
                        LicenseDetails.LicenseCopy = true;
                        LicenseDetails.DeleteLicenseCopy = false;
                        LicenseDetails.MainGroup = true;
                        LicenseDetails.SubGroup = true;
                        LicenseDetails.Activity = true;
                    }
                    else
                    {
                        ActivityTitle = type == EstablishmentOutletActivitiesTabsEnum.CRDetails ? AppResources.ESTCommercialRegistration : AppResources.ESTAddLicense;
                        DisplayCompleteDetailsLabel = true;
                        ActivityDetails.IssueCountry = true;
                        ActivityDetails.IssueBy = true;
                        ActivityDetails.IssueCity = true;
                        ActivityDetails.CRNo = true;
                        ActivityDetails.ValidFrom = true;
                        ActivityDetails.MainActivity = true;
                        ActivityDetails.IsMainActivityVisible = true;
                        ActivityDetails.CRCopy = true;
                        ActivityDetails.DeleteCRcopy = true;
                        ActivityDetails.DeleteTransferCRCopy = true;
                        ActivityDetails.TransferCRCopy = true;
                        ActivityDetails.IsTransferCRCopyVisible = true;
                        ActivityDetails.MainGroup = true;
                        ActivityDetails.SubGroup = true;
                        ActivityDetails.Activity = true;

                        LicenseDetails.IssueCountry = true;
                        LicenseDetails.IssueBy = true;
                        LicenseDetails.IssueCity = true;
                        LicenseDetails.ValidFrom = true;
                        LicenseDetails.LicenseNo = true;
                        LicenseDetails.LicenseName = true;
                        LicenseDetails.MainActivity = true;
                        LicenseDetails.IsMainActivityVisible = true;
                        LicenseDetails.LicenseCopy = true;
                        LicenseDetails.DeleteLicenseCopy = true;
                        LicenseDetails.MainGroup = true;
                        LicenseDetails.SubGroup = true;
                        LicenseDetails.Activity = true;
                    }

                    break;
                default:
                    break;
            }
        }

        private async Task updateActivityCrOrLicense()
        {
            string type = string.Empty;
            try
            {
                if ((CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails))
                {
                    type = "1";
                }
                else if ((CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails))
                {
                    type = "2";
                }
                await updateActivityLicense(type);

            }
            catch (Exception)
            {

            }
        }

        private async Task updateActivityLicense(String pageType)
        {
            IsLoading = true;
            Nreg_ActivityItem item;
            try
            {
                if (await ValidateForm())
                {
                    if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails || CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                    {
                        DateTime.TryParseExact(CRValidFrom, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime crIssueDate);
                        DateTime.TryParseExact(ValidFrom, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime issueDate);
                        DateTime.TryParseExact("9999/12/31", "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime maxDate);
                        item = new Nreg_ActivityItem
                        {
                            Type = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? "BUP002" : "ZS0004",

                            Idnumber = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? CRNumber : LicenseNumber,
                            ActName = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? CrName : LicenseName,

                            Activity = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? CRAcitivity.IndSector : LicenseAcitivity.IndSector
                        };
                        UpdateActivityLicenseModel updateActivityModel = new UpdateActivityLicenseModel();
                        updateActivityModel.Taxpayer = App.LoginDataRetrieved.TIN;
                        updateActivityModel.Idtype = item.Type;
                        updateActivityModel.Idnumber = item.Idnumber;
                        updateActivityModel.Activity = CRAcitivity == null ? string.Empty : CRAcitivity.IndSector;
                        updateActivityModel.MainGrp = "";
                        updateActivityModel.SubGrp = "";
                        updateActivityModel.UpdFlg = false;
                        IsLoading = true;
                        String Response = await EstablishmentRegistrationWebServiceManager.UpdateUserLicenseInActivityPage(updateActivityModel, pageType);
                        IsLoading = false;
                    }
                }
                else
                {
                    IsLoading = false;
                }
            }
            catch (Exception)
            {
                IsLoading = false;
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task navigateToNext()
        {
            CanExecute = false;
            try
            {
                if (await ValidateForm())
                {
                    if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails || CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                    {
                        DateTime.TryParseExact(CRValidFrom, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime crIssueDate);
                        DateTime.TryParseExact(ValidFrom, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime issueDate);
                        DateTime.TryParseExact("9999/12/31", "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime maxDate);

                        foreach (var licenseActivity in NregMulActivityList)
                        {
                            licenseActivity.Idnumber = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? CRNumber : LicenseNumber;
                        }

                        Nreg_ActivityItem item = new Nreg_ActivityItem
                        {
                            Type = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? "BUP002" : "ZS0004",
                            ValidDateFrom = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? crIssueDate.ToString("yyyy-MM-ddThh:mm:ss") : issueDate.ToString("yyyy-MM-ddThh:mm:ss"),
                            ValidDateTo = maxDate.ToString("yyyy-MM-ddThh:mm:ss"),
                            Idnumber = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? CRNumber : LicenseNumber,
                            ActName = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? CrName : LicenseName,
                            Country = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? CRIssueCountry.Land1 : LicenseIssueCountry.Land1,
                            City = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? CRIssueCity.CityName : LicenseIssueCity.CityName,
                            CityCode = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? CRIssueCity.CityCode : LicenseIssueCity.CityCode,
                            Actcat = MainActivity ? "M" : "S",

                            Actno = $"{Int16.Parse(newNumber?.Actno):00000}",
                            Crattfg = CRsCopies.Count > 0 ? "X" : string.Empty,
                            activitySet = new List<NregMulSet>(NregMulActivityList),
                            Z700Number = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? CRNationalNumber : ""

                        };

                        if (App.IsArabic)
                        {
                            item.ArrowImageSource = "arrowLeft.png";
                            item.Institute = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? ZATCAConstants.ArIssueBy.FirstOrDefault(i => i.Value == CRIssueBy).Key : ZATCAConstants.ArIssueBy.FirstOrDefault(i => i.Value == LicenseIssueBy).Key;
                        }
                        else
                        {
                            item.ArrowImageSource = "arrowRight.png";
                            item.Institute = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? ZATCAConstants.EnIssueBy.FirstOrDefault(i => i.Value == CRIssueBy).Key : ZATCAConstants.EnIssueBy.FirstOrDefault(i => i.Value == LicenseIssueBy).Key;
                        }

                        CanExecute = true;
                        if (SelectedCRItem != null)
                        {
                            NregActivityList.Remove(SelectedCRItem);
                            SelectedCRItem = null;
                        }
                        if (SelectedLicenseItem != null)
                        {
                            NregActivityList.Remove(SelectedLicenseItem);
                            SelectedLicenseItem = null;
                        }
                        NregActivityList.Add(item);
                        CurrentTab = EstablishmentOutletActivitiesTabsEnum.ActivityList;
                        NregMulActivityList.Clear();
                    }
                    else if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.ActivityList)
                    {
                        CanExecute = true;
                        goBackAction?.Invoke(NregActivityList);
                        NregActivityList = new List<Nreg_ActivityItem>();
                        _navigationService.GoBack();
                    }
                }
            }
            catch (Exception)
            { }
            finally
            {
                CanExecute = true;
            }
        }
        public void OnAppearing()
        {
            var activityItems = taxPayerDetails?.Nreg_ActivitySet.Where(i => new List<string> { "BUP002", "ZS0004" }.Contains(i.Type) && i.Actno == newNumber?.Actno).ToList();
            LicenseData = activityItems?.Where(p => p.Type == "ZS0004").ToList();
            var CRData = activityItems?.Where(p => p.Type == "BUP002").ToList();
            if (PageType == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
            {
                if (LicenseData.Count == 0)
                {
                    if (AddNewLicenseTapped)
                    {
                        SetUIAvailability(true, PageType);

                    }
                    else
                    {
                        SetUIAvailability(false, PageType);

                    }

                    AddLicenseEnabled = true;
                    ActivityTitle = AppResources.ESTAddLicense;
                }
                if (LicenseData.Count >= 4)
                {
                    AddLicenseEnabled = false;
                    ActivityTitle = AppResources.ESTLicenseDetails;
                }
                else
                {
                    AddLicenseEnabled = true;
                    ActivityTitle = AppResources.ESTAddLicense;
                }
            }
            else if (PageType == EstablishmentOutletActivitiesTabsEnum.CRDetails)
            {
                if (CRData != null && CRData.Count > 0)
                {
                    ActivityTitle = AppResources.ESTCommercialRegistration;
                    SetUIAvailability(true, PageType);
                }
                else
                {
                    SetUIAvailability(false, PageType);
                }
            }
        }
        public void OnDisappearing()
        {
        }
        private async Task AddAttachment(string docType)
        {
            try
            {
                string[] filetypes = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetAttachmentTypeStringForTaxEvasion();
                PickOptions options = UtilityManager.GetFilePickerOptionsForChooser(filetypes);
                //var fileData = await CrossFilePicker.Current.PickFile(filetypes);

                var fileData = await FilePicker.PickAsync(options);
                var stream = await fileData.OpenReadAsync();
                var attachmentByte = UtilityManager.ReadFully(stream as Stream);

                if (attachmentByte != null)
                {
                    //var attachmentByte = fileData.DataArray;

                    string base64String = Convert.ToBase64String(attachmentByte, 0, attachmentByte.Length);
                    var attachmentName = fileData.FileName;
                    bool isFileAlreayUploaded = IsFileAlreadyAttached(docType, attachmentName);
                    if (!isFileAlreayUploaded)
                    {
                        float sizemb = attachmentByte.Length / 1024f / 1024f;
                        decimal attachmentSize = 0;
                        attachmentSize = attachmentSize + (decimal)sizemb;

                        if (fileData.FileName.Contains("."))
                        {
                            string[] ExtentionArray = fileData.FileName.Split('.');
                            string Extention = ExtentionArray.Last();

                            if (Extention.ToLower() == "doc" || Extention.ToLower() == "docx" || Extention.ToLower() == "jpg" || Extention.ToLower() == "pdf" || Extention.ToLower() == "jpeg")
                            {
                                attachmentSize = Math.Round(Convert.ToDecimal(Convert.ToDouble(attachmentByte.Length) / 1048576.0), 2);
                                decimal AttachmentSizeTillFourDecimal = Math.Round(Convert.ToDecimal(Convert.ToDouble(attachmentByte.Length) / 1048576.0), 4);
                                if (Convert.ToDecimal(attachmentSize) <= 10)
                                {
                                    if (Convert.ToDecimal(AttachmentSizeTillFourDecimal) > 0)
                                    {
                                        try
                                        {
                                            string attachmentType = UtilityManager.GetContentType(Extention);
                                            await SaveAttachment(stream, attachmentName, docType, attachmentType);
                                        }
                                        catch (Exception)
                                        {


                                        }
                                    }
                                    else
                                    {
                                        attachmentName = string.Empty;
                                        await _dialogService.ShowMessage(AppResources.Somethingwentwrong, AppResources.Information);
                                    }
                                }
                                else
                                {
                                    await _dialogService.ShowMessage(AppResources.ESTAttachmentSizeNotfication, AppResources.Information);
                                }

                            }
                            else
                            {
                                await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly, AppResources.Information);
                            }
                        }
                    }
                    else
                    {
                        IsLoading = false;
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZFileWithTheSameNameAlreadyExists));
                    }

                }

            }
            catch (Exception)
            { }
        }
        private async Task updateActivityList(string indSector = "")
        {
            IsLoading = true;
            activityList = await EstablishmentRegistrationWebServiceManager.ESTOutletGetActivitySetsList(indSector);
            IsLoading = false;
        }
        private void DisplayActivityPopUp(string isFrom)
        {
            if (isFrom == "License")
            {
                if (SelectedLicenseItem?.activitySet?.Count > 0)
                {
                    dataModel = new PopUpServiceModel
                    {
                        existedActivities = SelectedLicenseItem?.activitySet,
                        activitiesList = activityList
                    };
                }
                else if (NregMulActivityList?.Count > 0)
                {
                    dataModel = new PopUpServiceModel
                    {
                        activitiesList = activityList,
                        existedActivities = NregMulActivityList
                    };
                }
                else
                {
                    dataModel = new PopUpServiceModel
                    {
                        activitiesList = activityList
                    };
                }
                // display PopUp screen
                MopupService.Instance.PushAsync(new ActivitiesPopupPageView(dataModel, true), true);
            }
            else
            {
                MopupService.Instance.PushAsync(new ActivitiesPopupPageView(dataModel = new PopUpServiceModel { existedActivities = NregMulActivityList }, false), true);
            }
        }
        private async Task fetchTabDataAndBind()
        {
            try
            {
                IsLoading = true;
                EnableIssueByDropDown = true;
                EnableInputFields = true;
                resetForm();
                if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                {
                    OutletDropDowns = await EstablishmentRegistrationWebServiceManager.ESTOutletDropDowns();
                    activityList = await EstablishmentRegistrationWebServiceManager.ESTOutletGetActivitySetsList();
                    EnableIssueByDropDown = false;
                    CRNumber = validateCR?.Crnum;
                    CRNationalNumber = validateCR?.Z700Crnum;
                    CrName = validateCR?.Crname;
                    CRIssueCountry = OutletDropDowns.country_dropdownSet.Where(i => i.Land1 == "SA").FirstOrDefault();
                    if (App.IsArabic)
                    {
                        CRIssueBy = CRIssueCountry.Land1 == "SA" ? ZATCAConstants.ArIssueBy["90702"] : ZATCAConstants.ArIssueBy["90718"];
                    }
                    else
                    {
                        CRIssueBy = CRIssueCountry.Land1 == "SA" ? ZATCAConstants.EnIssueBy["90702"] : ZATCAConstants.EnIssueBy["90718"];
                    }
                    CRIssueCity = new CityDropdownItem()
                    {
                        CityName = validateCR?.CityAry,
                        CityCode = string.Empty
                    };
                    CRValidFrom = validateCR?.Issuedt; //?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));


                    SelectedCRItem = taxPayerDetails?.Nreg_ActivitySet?.FirstOrDefault(i => i.Type == "BUP002");
                    if (SelectedCRItem != null)
                    {
                        EnableIssueByDropDown = false;
                        CRNumber = SelectedCRItem?.Idnumber;
                        CRNationalNumber = SelectedCRItem?.Z700Number;
                        CrName = SelectedCRItem?.ActName;
                        CRNationalNumber = SelectedCRItem?.Z700Number;
                        CRIssueCountry = OutletDropDowns.country_dropdownSet.Where(i => i.Land1 == SelectedCRItem?.Country).FirstOrDefault();
                        CRIssueBy = App.IsArabic ? ZATCAConstants.ArIssueBy[SelectedCRItem?.Institute] : ZATCAConstants.EnIssueBy[SelectedCRItem?.Institute];
                        CRValidFrom = SelectedCRItem?.ValidDateFrom;//?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));

                        CRIssueCity = new CityDropdownItem()
                        {
                            CityName = OutletDropDowns.city_dropdownSet.Where(i => i.CityCode == SelectedCRItem?.CityCode).FirstOrDefault().CityName,
                            CityCode = SelectedCRItem?.CityCode,
                        };

                        if (SelectedCRItem.Actcat.Equals("M"))
                        {
                            MainActivity = true;
                        }
                        else
                        {
                            MainActivity = false;
                        }
                        updateActivityList();
                        updateCRAttachments();
                    }
                    if (CrName.Length > 0)
                    {
                        EnableCRInputField = false;
                        if (!string.IsNullOrEmpty(CRNationalNumber))
                        {
                            EnableCRNationalInputField = false;
                        }
                    }
                    else
                    {
                        EnableCRInputField = true;
                        if (string.IsNullOrEmpty(CRNationalNumber))
                        {
                            EnableCRNationalInputField = true;
                        }
                    }

                    if (!string.IsNullOrEmpty(CRNumber))
                    {
                        PrepareDataToDisplayActivitiesPoPup(SelectedCRItem?.Idnumber);
                    }
                }
                else if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                {
                    OutletDropDowns = await EstablishmentRegistrationWebServiceManager.ESTOutletDropDowns();
                    activityList = await EstablishmentRegistrationWebServiceManager.ESTOutletGetActivitySetsList();

                    if (/*editModeEnabled == true &&*/ SelectedLicenseItem != null || validateLicense != null)
                    {
                        if (SelectedLicenseItem == null && validateLicense != null)
                        {
                            SelectedLicenseItem = validateLicense;
                        }
                        LicenseNumber = SelectedLicenseItem?.Idnumber;
                        LicenseName = SelectedLicenseItem?.ActName;
                        LicenseIssueCountry = OutletDropDowns.country_dropdownSet.Where(i => i.Land1 == SelectedLicenseItem?.Country).FirstOrDefault();
                        LicenseIssueBy = App.IsArabic ? ZATCAConstants.ArIssueBy[SelectedLicenseItem?.Institute] : ZATCAConstants.EnIssueBy[SelectedLicenseItem?.Institute];
                        ValidFrom = SelectedLicenseItem?.ValidDateFrom;//?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                        LicenseIssueCity = new CityDropdownItem()
                        {
                            CityName = SelectedLicenseItem?.City,
                            CityCode = SelectedLicenseItem?.CityCode
                        };

                        if (SelectedLicenseItem.Actcat.Equals("M"))
                        {
                            MainActivity = true;
                        }
                        else
                        {
                            MainActivity = false;
                        }
                        updateActivityList(SelectedLicenseItem?.Activity);

                        List<Attachment> list = new List<Attachment>();
                        var lists = taxPayerDetails.AttDetSet.Where(x => x.Dotyp == "RG02" && x.OutletRef == string.Format("{0}-{1}", SelectedLicenseItem?.Actno, SelectedLicenseItem?.Idnumber)).ToList();
                        if (SelectedLicenseItem?.activitySet != null && SelectedLicenseItem?.activitySet?.Count > 0)
                        {
                            NregMulActivityList?.AddRange(SelectedLicenseItem?.activitySet);
                        }
                        else
                        {
                            PrepareDataToDisplayActivitiesPoPup(SelectedLicenseItem?.Idnumber, true);
                        }

                        if (lists.Count > 0)
                        {
                            foreach (AttDetItem attDetItem in lists)
                            {
                                Attachment obj = new Attachment
                                {
                                    Filename = attDetItem.Filename,
                                    FileExtn = attDetItem.FileExtn,
                                    Mimetype = attDetItem.Mimetype,
                                    RetGuid = attDetItem.RetGuid,
                                    DocUrl = attDetItem.DocUrl,
                                    Dotyp = attDetItem.Dotyp,
                                    Doguid = attDetItem.Doguid
                                };
                                list.Add(obj);
                            }

                            LicensesCopies = new ObservableCollection<Attachment>(list);
                        }
                    }
                }
                else
                {
                    LicenseData = NregActivityList.Where(i => i.Type == "ZS0004").ToList();

                    if (LicenseData.Count >= 4)
                    {
                        AddLicenseEnabled = false;
                    }
                    else
                    {
                        AddLicenseEnabled = true;

                    }
                }
            }
            catch (Exception)
            {



            }
            finally
            {
                IsLoading = false;
                updateDatePickers(CurrentTab);
            }
        }
        private void updateDatePickers(EstablishmentOutletActivitiesTabsEnum _enum)
        {
            DateTime dob = DateTime.Now;
            ObservableCollection<object> _selectedDOBDate = new ObservableCollection<object>();
            if (taxPayerDetails?.Caltp == "Gregorian")
            {
                _selectedDOBDate?.Clear();
                _selectedDOBDate.Add($"{dob.Day:00}");
                _selectedDOBDate.Add($"{dob.Month:00}");
                _selectedDOBDate.Add(dob.Year.ToString());
            }
            else
            {
                _selectedDOBDate?.Clear();
                var hijiriDate = dob.ToString("yyyy/MM/dd", new CultureInfo("ar-sa"));
                var arr = hijiriDate.Split('/');
                _selectedDOBDate.Add(arr[2]);
                _selectedDOBDate.Add(arr[1]);
                _selectedDOBDate.Add(arr[0]);
            }
            if (_enum == EstablishmentOutletActivitiesTabsEnum.CRDetails)
            {
                DateTime.TryParseExact(CRValidFrom, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime _crValidFrom);
                if (taxPayerDetails?.Caltp == "Gregorian")
                {
                    SelectedCRValidFromDate = _selectedDOBDate;
                    if (!string.IsNullOrWhiteSpace(CRValidFrom))
                        DisplayCRValidFrom = _crValidFrom.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                }
                else
                {
                    SelectedCRValidFromHijiriDate = _selectedDOBDate;
                    if (!string.IsNullOrWhiteSpace(CRValidFrom))
                        //DisplayCRValidFrom = _crValidFrom.ToString("yyyy/MM/dd", new CultureInfo("ar-sa"));
                        DisplayCRValidFrom = HijriDateString(DateTime.Parse(CRValidFrom));
                }
            }
            else if (_enum == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
            {
                DateTime.TryParseExact(ValidFrom, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime _validFrom);
                if (taxPayerDetails?.Caltp == "Gregorian")
                {
                    SelectedValidFromDate = _selectedDOBDate;
                    if (!string.IsNullOrWhiteSpace(ValidFrom))
                        DisplayValidFrom = _validFrom.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                }
                else
                {
                    SelectedValidFromHijiriDate = _selectedDOBDate;
                    if (!string.IsNullOrWhiteSpace(ValidFrom))
                        //DisplayValidFrom = _validFrom.ToString("yyyy/MM/dd", new CultureInfo("ar-sa"));
                        DisplayValidFrom = HijriDateString(Convert.ToDateTime(ValidFrom));
                }
            }
        }
        public async Task validateCRNumber(string idNumber, bool CallMulset = false)
        {

            if (idNumber != null && idNumber.Length > 0)
            {
                IsLoading = true;
                var result = await EstablishmentRegistrationWebServiceManager.ESTValidateCRNum(idNumber);
                try
                {

                    if (!string.IsNullOrEmpty(result))
                    {
                        validateCR = JsonConvert.DeserializeObject<ValidateCR>(result);
                        CRNumber = validateCR?.Crnum;


                        if (CallMulset == false)
                        {
                            IsLoading = false;
                            return;
                        }
                        //Calling new API to get the specific acticities for that CR
                        var mulActivitySet = await EstablishmentRegistrationWebServiceManager.ESTGetCRMulActivitySetList(CRNumber);
                        if (mulActivitySet != null)
                        {
                            mulActivitySetData = mulActivitySet?.results;

                            List<NregMulSet> existingActivitiesList = new List<NregMulSet>();
                            NregMulSet existingActivities = null;


                            List<NregMulSet> filteredActivities = taxPayerDetails.Nreg_Mul_ActivitySet.Where(a => a.Idnumber == CRNumber).ToList();
                            foreach (var activities in filteredActivities)
                            {

                                existingActivities = new NregMulSet
                                {
                                    Activity = activityList?.activitySet?.Where(i => i.IndSector == activities?.Activity)?.FirstOrDefault()?.Text,
                                    ActMgrp = activityList?.act_groupSet?.Where(i => i.IndSector == activities?.ActMgrp)?.FirstOrDefault()?.Text,
                                    ActSgrp = activityList?.act_subgroupSet?.Where(i => i.IndSector == activities?.ActSgrp)?.FirstOrDefault()?.Text,
                                    Idnumber = activities?.Idnumber,
                                };

                                existingActivitiesList.Add(existingActivities);

                            }
                            dataModel = new PopUpServiceModel
                            {
                                existedActivities = existingActivitiesList
                            };
                            if (existingActivities != null)
                            {
                                NregMulActivityList?.AddRange(existingActivitiesList);
                            }

                        }

                    }
                }
                catch (Exception)
                {
                    IsLoading = false;
                }
                IsLoading = false;
                if (validateCR != null && validateCR.Crnum == null)
                {
                    await PrepareError(result);
                    return;
                }

                if (validateCR != null)
                {
                    if (!string.IsNullOrEmpty(validateCR.Z700Crnum))
                    {
                        CRNationalNumber = validateCR.Z700Crnum;
                    }

                }

                updateCRAttachments();
                if (validateCR?.NotFound == "X")
                {
                    await _dialogService.ShowMessage(AppResources.ESTValidateCRNumberInValid, AppResources.Information);
                    return;
                }
                if (validateCR?.Excption == "X")
                {
                    await _dialogService.ShowMessage(AppResources.ESTValidateCRNumberInValid, AppResources.Information);
                    return;
                }
                CRIssueCity = new CityDropdownItem()
                {
                    CityName = OutletDropDowns.city_dropdownSet.Where(i => i.CityCode == validateCR?.CityCode).FirstOrDefault().CityName,
                    CityCode = SelectedCRItem?.CityCode,
                };
                CRValidFrom = validateCR?.Issuedt;//?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                EnableInputFields = string.IsNullOrEmpty(validateCR?.Crname);

                updateDatePickers(EstablishmentOutletActivitiesTabsEnum.CRDetails);
            }

        }

        private async Task PrepareError(string result)
        {
            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(result);
            var errorID = string.Empty;
            if (errorMesg?.header?.moreInformation?.errorDetails[0]?.message != null)
            {

                WebServiceManager.ErrorMessageForUnlockAccount = errorMesg?.header?.moreInformation?.errorDetails[0].message;

               
                string line1 = "";

                for (int i = 0; i < errorMesg?.header?.moreInformation?.errorDetails.Count; i++)
                {
                    line1 = line1 + " " + errorMesg?.header?.moreInformation?.errorDetails[i].message;
                }
                WebServiceManager.ErrorMessageForUnlockAccount = line1;

                String WithReplacedString = WebServiceManager.ErrorMessageForUnlockAccount.Replace("An exception was raised", string.Empty);

                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(WithReplacedString));
              
            }
        }

        private void updateCRAttachments()
        {
            List<Attachment> list = new List<Attachment>();
            var lists = taxPayerDetails.AttDetSet.Where(x =>
            {
                var docIdentifier = string.Format("{0}-{1}", SelectedCRItem?.Actno, CRNumber);
                var outRef = x.OutletRef == docIdentifier;
                return x.Dotyp == "RG01" && outRef;
            }).ToList();
            foreach (AttDetItem attDetItem in lists)
            {
                var obj = new Attachment();
                obj.Filename = attDetItem.Filename;
                obj.FileExtn = attDetItem.FileExtn;
                obj.Mimetype = attDetItem.Mimetype;
                obj.RetGuid = attDetItem.RetGuid;
                obj.DocUrl = attDetItem.DocUrl;
                obj.Dotyp = attDetItem.Dotyp;
                obj.Doguid = attDetItem.Doguid;
                obj.showDelete = taxPayerDetails.ReturnIdx != attDetItem.RetGuid ? false : true;
                if (!list.Contains(obj))
                {
                    list.Add(obj);
                }
            }
            CRsCopies = new ObservableCollection<Attachment>(list);


            list.Clear();
            lists = taxPayerDetails.AttDetSet.Where(x =>
            {
                var docIdentifier = string.Format("{0}-{1}", SelectedCRItem?.Actno, CRNumber);
                var outRef = x.OutletRef == docIdentifier;
                return x.Dotyp == "RG12" && outRef;
            }).ToList();
            foreach (AttDetItem attDetItem in lists)
            {
                var obj = new Attachment();
                obj.Filename = attDetItem.Filename;
                obj.FileExtn = attDetItem.FileExtn;
                obj.Mimetype = attDetItem.Mimetype;
                obj.RetGuid = attDetItem.RetGuid;
                obj.DocUrl = attDetItem.DocUrl;
                obj.Dotyp = attDetItem.Dotyp;
                obj.Doguid = attDetItem.Doguid;
                obj.showDelete = taxPayerDetails.ReturnIdx != attDetItem.RetGuid ? false : true;
                if (!list.Contains(obj))
                {
                    list.Add(obj);
                }
            }
            //

            TransferCRsCopies.Clear();

            TransferCRsCopies = new ObservableCollection<Attachment>(list);
        }
        private void resetForm()
        {
            if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
            {
                CRIssueCountry = null;
                CRIssueBy = null;
                CRIssueCity = null;
                CRNumber = string.Empty;
                CRNationalNumber = string.Empty;
                CRsCopies = new ObservableCollection<Attachment>();
                TransferCRsCopies = new ObservableCollection<Attachment>();
                MainActivity = false;
                CRValidFrom = string.Empty;
                DisplayCRValidFrom = string.Empty;
                SelectedCRValidFromDate = null;
                SelectedCRValidFromHijiriDate = null;
                CRMainGroup = null;
                CRSubGroup = null;
                CRAcitivity = null;
            }
            else if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
            {
                LicenseIssueCountry = null;
                LicenseIssueBy = null;
                LicenseIssueCity = null;
                LicenseNumber = string.Empty;
                LicensesCopies = new ObservableCollection<Attachment>();
                MainActivity = false;
                ValidFrom = string.Empty;
                DisplayValidFrom = string.Empty;
                SelectedValidFromDate = null;
                SelectedValidFromHijiriDate = null;
                LicenseMainGroup = null;
                LicenseSubGroup = null;
                LicenseAcitivity = null;
            }
        }
        private async Task SaveAttachment(Stream attachmentByteData, string fileName, string docType, string contentType)
        {
            try
            {
                IsLoading = true;
                string CRLicenseNo = string.Empty;
                if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                {
                    CRLicenseNo = CRNumber;
                }
                if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                {
                    CRLicenseNo = LicenseNumber;
                }
                if (newNumber != null)
                {
                    string outletref = $"{short.Parse(newNumber?.Actno):00000-}" + CRLicenseNo;


                    Attachment dd = await EstablishmentRegistrationWebServiceManager.ESTAttachment(attachmentByteData, fileName, taxPayerDetails?.ReturnIdx, docType, contentType, outletref);

                    if (docType == "RG01")
                    {
                        CRsCopies.Add(dd);
                    }
                    else if (docType == "RG12")
                    {
                        TransferCRsCopies.Add(dd);
                    }
                    else if (docType == "RG02")
                    {
                        LicensesCopies.Add(dd);
                    }
                }
            }
            catch (Exception)
            { }
            finally
            {
                IsLoading = false;
            }
        }
        private async Task OnDeleteAttachment(Attachment item, string docType)
        {
            string QuestionMark = string.Empty;
            if (App.IsArabic)
            {
                QuestionMark = "؟";
            }
            else
            {
                QuestionMark = "?";
            }
            var confirmPopup = new ZAKATOkCancelPopUpView(AppResources.ZZUpdateZakatDelete + "   " + item.Filename + QuestionMark);
            confirmPopup.OnSelect = async (str) =>
            {
                if (str == "Yes")
                {
                    IsLoading = true;
                    var delete = EstablishmentRegistrationWebServiceManager.ESTDeleteAttachment(item?.Filename, item?.RetGuid, docType, item?.Doguid);
                    if (!string.IsNullOrEmpty(delete) && delete == "delete")
                    {
                        if (docType == "RG01")
                        {
                            CRsCopies.Remove(item);
                        }
                        else if (docType == "RG12")
                        {
                            TransferCRsCopies.Remove(item);
                        }
                        else if (docType == "RG02")
                        {
                            LicensesCopies.Remove(item);
                        }
                        IsLoading = false;
                    }
                    else
                    {
                        IsLoading = false;
                        await _dialogService.ShowError(AppResources.ZZSomethingwentwrong, AppResources.Information, "Ok", null);
                    }
                }
            };
            await MopupService.Instance.PushAsync(confirmPopup);
        }

        private async Task<bool> ValidateForm()
        {
            if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
            {
                if (CRIssueCountry == null)
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAIssueCountry));
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(CRIssueBy))
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAIssueBy));
                    return false;
                }
                else if (CRIssueCity == null)
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAIssueCity));
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(CRNumber))
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateACRNumber));
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(CrName))
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateACRName));
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(CRValidFrom))
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateACRValidFrom));
                    return false;
                }

                else
                {
                    return true;
                }
            }
            else if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
            {
                if (LicenseIssueCountry == null)
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAIssueCountry));
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(LicenseIssueBy))
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAIssueBy));
                    return false;
                }
                else if (LicenseIssueCity == null)
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAIssueCity));
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(LicenseNumber))
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateACLicense));
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(ValidFrom))
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateALicenseValidFrom));
                    return false;
                }

                else if (string.IsNullOrEmpty(LicenseName))
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateLicenseName));
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return true;
        }

        private void ShowAlertPopup(string _message)
        {
            _dialogService.ShowError(_message, AppResources.Information, "Ok", null);
        }


        public void OpenLicenseFormInEditMode(Nreg_ActivityItem LicenseData)
        {
            CurrentTab = EstablishmentOutletActivitiesTabsEnum.LicenseDetails;
            SelectedLicenseItem = LicenseData;
            SetUIAvailability(true, EstablishmentOutletActivitiesTabsEnum.LicenseDetails);
        }

        private bool IsFileAlreadyAttached(string doctype, string FileName)
        {
            bool isFileAlreadyAttached = false;
            if (doctype.Equals("RG01"))//Rent
            {
                if (CRsCopies != null && CRsCopies.Count > 0)
                {
                    for (int i = 0; i < CRsCopies.Count; i++)
                    {
                        if (CRsCopies[i].Filename.Equals(FileName))
                            isFileAlreadyAttached = true;
                        else
                            isFileAlreadyAttached = false;
                        if (isFileAlreadyAttached)
                            break;
                    }
                }
            }
            else if (doctype.Equals("RG12"))//Passport
            {
                if (TransferCRsCopies != null && TransferCRsCopies.Count > 0)
                {
                    for (int i = 0; i < TransferCRsCopies.Count; i++)
                    {
                        if (TransferCRsCopies[i].Filename.Equals(FileName))
                            isFileAlreadyAttached = true;
                        else
                            isFileAlreadyAttached = false;
                        if (isFileAlreadyAttached)
                            break;
                    }
                }
            }

            else if (doctype.Equals("RG02"))//PAssport
            {
                if (LicensesCopies != null && LicensesCopies.Count > 0)
                {
                    for (int i = 0; i < LicensesCopies.Count; i++)
                    {
                        if (LicensesCopies[i].Filename.Equals(FileName))
                            isFileAlreadyAttached = true;
                        else
                            isFileAlreadyAttached = false;
                        if (isFileAlreadyAttached)
                            break;
                    }
                }
            }
            return isFileAlreadyAttached;
        }
        private string HijriDateString(DateTime date)
        {
            try
            {
                return date.ToString("yyyy/MM/dd", new CultureInfo("ar-sa"));
            }
            catch (Exception)
            {


                UmAlQuraCalendar hijriCalendar = new UmAlQuraCalendar();
                return $"{hijriCalendar.GetYear(date):0000}/{hijriCalendar.GetMonth(date):00}/{hijriCalendar.GetDayOfMonth(date):00}";
            }
        }

        private void PrepareDataToDisplayActivitiesPoPup(string IdNumber, bool isLicense = false)
        {
            List<NregMulSet> existingActivitiesList = new List<NregMulSet>();
            NregMulSet existingActivities = null;
            PopUpServiceModel dataModel = null;

            List<NregMulSet> filteredActivities = taxPayerDetails?.Nreg_Mul_ActivitySet?.Where(a => a.Idnumber == IdNumber).ToList();
            int count = filteredActivities?.Count ?? 0;
            if (count > 0)
            {
                foreach (var activities in filteredActivities)
                {
                    existingActivities = new NregMulSet
                    {
                        Activity = activityList?.activitySet?.Where(i => i.IndSector == activities?.Activity)?.FirstOrDefault()?.Text,
                        ActMgrp = activityList?.act_groupSet?.Where(i => i.IndSector == activities?.ActMgrp)?.FirstOrDefault()?.Text,
                        ActSgrp = activityList?.act_subgroupSet?.Where(i => i.IndSector == activities?.ActSgrp)?.FirstOrDefault()?.Text,
                        Idnumber = activities.Idnumber,
                    };
                    existingActivitiesList.Add(existingActivities);
                    dataModel = new PopUpServiceModel
                    {
                        existedActivities = existingActivitiesList,
                    };

                }

                foreach (var item in dataModel.existedActivities)
                {
                    NregMulActivityList?.Add(item);
                }

                if (isLicense == true)
                {
                    foreach (var item in existingActivitiesList)
                    {
                        SelectedLicenseItem?.activitySet.Add(item);
                    }
                }
                else
                {
                    foreach (var item in existingActivitiesList)
                    {
                        SelectedCRItem?.activitySet.Add(item);
                    }
                    dataModel = new PopUpServiceModel { existedActivities = existingActivitiesList, };

                }
            }
        }

        #endregion
    }
}
