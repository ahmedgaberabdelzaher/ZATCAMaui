using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;

using Newtonsoft.Json;
using Mopups.Services;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.EstablishmentRegistration;
using ZATCAMAUI.Views.NewDesign.Common;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using static ZATCAMAUI.Models.ErrorMessage;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Views.NewDesign.EstablishmentAmendUpdatePages;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.EstablishmentRegistration
{

    public class ActivityItemPageViewModel : BaseViewModel
    {
        #region variables

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
                _activityTitle = value;
                OnPropertyChanged(nameof(ActivityTitle));
            }
        }


        private bool CanExecuteClickCommand(object args) => EnableInputFields;
        private bool CanIssueByExecuteClickCommand(object args) => EnableIssueByDropDown;

        private CountryDropdownItem _cRIssueCountry = new CountryDropdownItem();
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
        private string _cRIssueBy = string.Empty;
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
        private CityDropdownItem _cRIssueCity = new CityDropdownItem();
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
        private bool _enableCRNationalInputField = true;
        public bool EnableCRNationalInputField
        {
            get => _enableCRNationalInputField;
            private set
            {
                if (_enableCRNationalInputField == value) return;

                _enableCRNationalInputField = value;
                OnPropertyChanged(nameof(EnableCRNationalInputField));
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
        private ActGroupSet _cRMainGroup = new ActGroupSet();
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
        private ActGroupSet _cRSubGroup = new ActGroupSet();
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
        private ActGroupSet _cRAcitivity = new ActGroupSet();
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
        private CountryDropdownItem _licenseIssueCountry = new CountryDropdownItem();
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
        private string _licenseIssueBy = string.Empty;
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
        private CityDropdownItem _licenseIssueCity = new CityDropdownItem();
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
        private ActGroupSet _licenseMainGroup = new ActGroupSet();
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
        private ActGroupSet _licenseSubGroup = new ActGroupSet();
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
        private ActGroupSet _licenseAcitivity = new ActGroupSet();
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

        private bool isIssueCountryEnable = true;
        public bool IsIssueCountryEnable
        {
            get => isIssueCountryEnable;
            set
            {
                if (isIssueCountryEnable == value) return;

                isIssueCountryEnable = value;
                OnPropertyChanged(nameof(IsIssueCountryEnable));
            }
        }


        private bool isIssueByEnable = true;
        public bool IsIssueByEnable
        {
            get => isIssueByEnable;
            set
            {
                if (isIssueByEnable == value) return;

                isIssueByEnable = value;
                OnPropertyChanged(nameof(IsIssueByEnable));
            }
        }

        private bool isIssueCityEnable = true;
        public bool IsIssueCityEnable
        {
            get => isIssueCityEnable;
            set
            {
                if (isIssueCityEnable == value) return;

                isIssueCityEnable = value;
                OnPropertyChanged(nameof(IsIssueCityEnable));
            }
        }

        private bool isValidFromEnable = true;
        public bool IsValidFromEnable
        {
            get => isValidFromEnable;
            set
            {
                if (isValidFromEnable == value) return;

                isValidFromEnable = value;
                OnPropertyChanged(nameof(IsValidFromEnable));
            }
        }

        private bool isMainGrpmEnable = true;
        public bool IsMainGrpmEnable
        {
            get => isMainGrpmEnable;
            set
            {
                if (isMainGrpmEnable == value) return;

                isMainGrpmEnable = value;
                OnPropertyChanged(nameof(IsMainGrpmEnable));
            }
        }

        private bool isSubGrpmEnable = true;
        public bool IsSubGrpmEnable
        {
            get => isSubGrpmEnable;
            set
            {
                if (isSubGrpmEnable == value) return;

                isSubGrpmEnable = value;
                OnPropertyChanged(nameof(IsSubGrpmEnable));
            }
        }

        private bool isActivityEnable = true;
        public bool IsActivityEnable
        {
            get => isActivityEnable;
            set
            {
                if (isActivityEnable == value) return;

                isActivityEnable = value;
                OnPropertyChanged(nameof(IsActivityEnable));
            }
        }

        #endregion

        #region commands
        public ICommand OnActivitiesButtonClick { get; set; }
        public Command OnNextButtonClick { get; private set; }
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
        public ActivityItemPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            OnActivitiesButtonClick = new Command<string>(DisplayActivityPopUp);
            OnNextButtonClick = new Command(() => navigateToNext(), () => CanExecute);
            OnPreButtonClick = new Command(() => navigationService.GoBack());
            OnNewLicenseButtonClick = new Command(() => CurrentTab = EstablishmentOutletActivitiesTabsEnum.LicenseDetails);
            OnIssueCountrySelectButtonClick = new Command((object o) =>
            {
                if (!IsIssueCountryEnable) return;
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(OutletDropDowns?.country_dropdownSet);
                poupWindow.OnItemSelect = (item) =>
                {
                    try
                    {
                        if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                        {
                            CRIssueCountry = item as CountryDropdownItem;
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
                            LicenseIssueCountry = item as CountryDropdownItem;
                            LicenseIssueCity = null;
                        }
                    }
                    catch (Exception)
                    {
                    }
                };
                MopupService.Instance.PushAsync(poupWindow);
            }, CanExecuteClickCommand);
            OnIssueBySelectButtonClick = new Command((object o) =>
            {
                if (!IsIssueByEnable) return;
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(App.IsArabic ? ZATCAConstants.ArIssueBy.Values : ZATCAConstants.EnIssueBy.Values);
                poupWindow.OnItemSelect = (item) =>
                {
                    try
                    {
                        if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                            CRIssueBy = item as string;
                        if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                            LicenseIssueBy = item as string;
                    }
                    catch (Exception)
                    {
                    }
                };
                MopupService.Instance.PushAsync(poupWindow);
            }, CanIssueByExecuteClickCommand);
            OnIssueCitySelectButtonClick = new Command((object o) =>
            {
                if (!IsIssueCityEnable) return;
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
                    ListPopUpViewPage poupWindow = new ListPopUpViewPage(filterCities);

                    poupWindow.OnItemSelect = (item) =>
                    {
                        try
                        {
                            if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                                CRIssueCity = item as CityDropdownItem;
                            if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                                LicenseIssueCity = item as CityDropdownItem;
                        }
                        catch (Exception)
                        {
                        }
                    };
                    MopupService.Instance.PushAsync(poupWindow);
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
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZMaximumnoof5attachmentscanbeuploaded));
                }

            });
            OnTransferCopyOfLicenseChoiceButtonClick = new Command(async (type) =>
            {
                if (LicensesCopies.Count < 5)//
                {
                    await AddAttachment(type as string);
                }
                else
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZMaximumnoof5attachmentscanbeuploaded));
                }
            });

            OnMainGroupSelectButtonClick = new Command(() =>
            {
                if (!IsMainGrpmEnable) return;
                var dropDownData = activityList?.act_groupSet?.ToList();
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(dropDownData);
                poupWindow.OnItemSelect = (item) =>
                {
                    try
                    {
                        if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                        {
                            CRMainGroup = item as ActGroupSet;
                            CRSubGroup = null;
                            CRAcitivity = null;
                        }
                        if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                        {
                            LicenseMainGroup = item as ActGroupSet;
                            LicenseSubGroup = null;
                            LicenseAcitivity = null;
                        }

                    }
                    catch (Exception)
                    {
                    }
                };
                MopupService.Instance.PushAsync(poupWindow);
            });
            OnSubGroupSelectButtonClick = new Command(() =>
            {
                if (!IsSubGrpmEnable) return;
                var dropDownData = new List<ActGroupSet>();
                if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                    dropDownData = activityList?.act_subgroupSet.Where(i => i.IndSector.StartsWith(CRMainGroup?.IndSector)).ToList();
                if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                    dropDownData = activityList?.act_subgroupSet.Where(i => i.IndSector.StartsWith(LicenseMainGroup?.IndSector)).ToList();

                ListPopUpViewPage poupWindow = new ListPopUpViewPage(dropDownData);
                poupWindow.OnItemSelect = (item) =>
                {
                    try
                    {
                        if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                        {
                            CRSubGroup = item as ActGroupSet;
                            CRAcitivity = null;
                        }
                        if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                        {
                            LicenseSubGroup = item as ActGroupSet;
                            LicenseAcitivity = null;
                        }
                    }
                    catch (Exception)
                    {
                    }
                };
                MopupService.Instance.PushAsync(poupWindow);
            });
            OnAcitivitySelectButtonClick = new Command(() =>
            {
                if (!IsActivityEnable) return;
                var dropDownData = new List<ActGroupSet>();
                if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                    dropDownData = activityList?.activitySet?.Where(i => i.IndSector.StartsWith(CRSubGroup?.IndSector)).ToList();
                if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                    dropDownData = activityList?.activitySet?.Where(i => i.IndSector.StartsWith(LicenseSubGroup?.IndSector)).ToList();

                ListPopUpViewPage poupWindow = new ListPopUpViewPage(dropDownData);
                poupWindow.OnItemSelect = (item) =>
                {
                    try
                    {
                        if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                        {
                            CRAcitivity = item as ActGroupSet;
                            CRMainGroup = activityList.act_groupSet.Where(i => i.IndSector.StartsWith(CRAcitivity?.IndSector?.Substring(0, 2))).FirstOrDefault();
                            CRSubGroup = activityList.act_subgroupSet.Where(i => i.IndSector.StartsWith(CRAcitivity?.IndSector?.Substring(0, 4))).FirstOrDefault();
                        }
                        if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                        {
                            LicenseAcitivity = item as ActGroupSet;
                            LicenseMainGroup = activityList.act_groupSet.Where(i => i.IndSector.StartsWith(LicenseAcitivity?.IndSector?.Substring(0, 2))).FirstOrDefault();
                            LicenseSubGroup = activityList.act_subgroupSet.Where(i => i.IndSector.StartsWith(LicenseAcitivity?.IndSector?.Substring(0, 4))).FirstOrDefault();
                        }
                    }
                    catch (Exception)
                    {
                    }
                };
                MopupService.Instance.PushAsync(poupWindow);
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
                            Country = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? CRIssueCountry?.Land1 : LicenseIssueCountry?.Land1,
                            City = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? CRIssueCity?.CityName : LicenseIssueCity?.CityName,
                            CityCode = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? CRIssueCity?.CityCode : LicenseIssueCity?.CityCode,
                            Actcat = MainActivity ? "M" : "S",
                            Actno = $"{Int16.Parse(newNumber?.Actno):00000}",
                            Crattfg = CRsCopies.Count > 0 ? "X" : string.Empty,
                            ActName = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? CrName : LicenseName,
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
            {
            }
            finally
            {
                CanExecute = true;
            }
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
                                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Somethingwentwrong));
                                    }
                                }
                                else
                                {
                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTAttachmentSizeNotfication));
                                }

                            }
                            else
                            {
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly));
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
                MopupService.Instance.PushAsync(new ActivitiesPopupPageView(dataModel), true);

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
                    EnableCRNationalInputField = string.IsNullOrEmpty(CRNationalNumber);
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
                        CityName = OutletDropDowns.city_dropdownSet.Where(i => i.CityCode == validateCR?.CityCode).FirstOrDefault().CityName,
                        CityCode = validateCR?.CityCode,
                    };
                    CRValidFrom = validateCR?.Issuedt;
                    EnableCRInputField = string.IsNullOrEmpty(validateCR?.Crname);
                    SelectedCRItem = taxPayerDetails?.Nreg_ActivitySet?.FirstOrDefault(i => i.Type == "BUP002");
                    if (SelectedCRItem != null)
                    {
                        EnableIssueByDropDown = false;
                        CRNumber = SelectedCRItem?.Idnumber;
                        CRNationalNumber = SelectedCRItem?.Z700Number;
                        CRIssueCountry = OutletDropDowns.country_dropdownSet.Where(i => i.Land1 == SelectedCRItem?.Country).FirstOrDefault();
                        CRIssueBy = App.IsArabic ? ZATCAConstants.ArIssueBy[SelectedCRItem?.Institute] : ZATCAConstants.EnIssueBy[SelectedCRItem?.Institute];
                        CRValidFrom = SelectedCRItem?.ValidDateFrom;


                        if (SelectedCRItem.Actcat.Equals("M"))
                        {
                            MainActivity = true;
                        }
                        else
                        {
                            MainActivity = false;
                        }
                        updateCRAttachments();
                    }
                    updateDatePickers(CurrentTab);
                    if (CRIssueCountry.Land1.Length > 0)
                    {
                        IsIssueCountryEnable = false;
                    }
                    if (CRIssueBy.Length > 0)
                    {
                        IsIssueByEnable = false;
                    }

                    if (CRIssueCity.CityName.Length > 0)
                    {
                        IsIssueCityEnable = false;
                    }

                    if (CRNumber.Length > 0)
                    {
                        EnableCRInputField = false;
                    }

                    if (DisplayCRValidFrom.Length > 0)
                    {
                        IsValidFromEnable = false;
                    }

                    makeDropdownFieldsNorEditable();


                    if (!string.IsNullOrEmpty(CRNumber))
                    {
                        PrepareDataToDisplayActivitiesPoPup(SelectedCRItem?.Idnumber);
                        validateCRNumber(CRNumber);
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
                        ValidFrom = SelectedLicenseItem?.ValidDateFrom;
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

                        List<Attachment> list = new List<Attachment>();
                        var lists = taxPayerDetails.AttDetSet.Where(x => x.Dotyp == "RG02" && x.OutletRef == string.Format("{0}-{1}", SelectedLicenseItem?.Actno, SelectedLicenseItem?.Idnumber)).ToList();
                        if (lists.Count > 0)
                        {
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
                                list.Add(obj);
                            }

                            LicensesCopies = new ObservableCollection<Attachment>(list);
                        }
                    }

                    if (SelectedLicenseItem?.activitySet != null && SelectedLicenseItem?.activitySet?.Count > 0)
                    {
                        NregMulActivityList?.AddRange(SelectedLicenseItem?.activitySet);
                    }
                    else
                    {
                        PrepareDataToDisplayActivitiesPoPup(SelectedLicenseItem?.Idnumber, true);
                    }

                    IsIssueCountryEnable = true;
                    IsIssueByEnable = true;
                    IsIssueCityEnable = true;
                    EnableCRInputField = true;
                    IsValidFromEnable = true;
                    IsMainGrpmEnable = true;
                    IsSubGrpmEnable = true;
                    IsActivityEnable = true;


                }
                else
                {
                    LicenseData = NregActivityList.Where(i => i.Type == "ZS0004").ToList();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsLoading = false;
                updateDatePickers(CurrentTab);
            }
        }

        private void getActivityPopupData(string crNumber = "")
        {
            try
            {

                List<NregMulSet> existingActivitiesList = new List<NregMulSet>();
                NregMulSet existingActivities = null;
                existingActivities = new NregMulSet
                {
                    Activity = CRAcitivity.Text,
                    ActMgrp = CRMainGroup.Text,
                    ActSgrp = CRSubGroup.Text,
                    Idnumber = crNumber,
                };

                existingActivitiesList.Add(existingActivities);
                dataModel = new PopUpServiceModel
                {
                    existedActivities = existingActivitiesList
                };
            }
            catch (Exception)
            {
            }
        }

        private void updateDatePickers(EstablishmentOutletActivitiesTabsEnum _enum)
        {
            try
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
                            DisplayValidFrom = HijriDateString(Convert.ToDateTime(ValidFrom));
                    }
                    else
                    {
                        SelectedValidFromHijiriDate = _selectedDOBDate;
                        if (!string.IsNullOrWhiteSpace(ValidFrom))
                            DisplayValidFrom = Convert.ToDateTime(ValidFrom).ToString("yyyy/MM/dd", new CultureInfo("ar-sa"));
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void makeDropdownFieldsNorEditable()
        {
            try
            {
                if (CRMainGroup.IndSector.Length > 0)
                {
                    IsMainGrpmEnable = false;
                }
                if (CRSubGroup.IndSector.Length > 0)
                {
                    IsSubGrpmEnable = false;
                }

                if (CRAcitivity.IndSector.Length > 0)
                {
                    IsActivityEnable = false;
                }

            }
            catch (Exception)
            {

            }
        }

        public async Task validateCRNumber(string idNumber, bool CallMulset = false)
        {
            IsLoading = true;


            try
            {
                if (string.IsNullOrEmpty(idNumber))
                {
                    return;
                }

                var result = await EstablishmentRegistrationWebServiceManager.ESTValidateCRNum(idNumber);

                if (!string.IsNullOrEmpty(result))
                {
                    try
                    {
                        validateCR = JsonConvert.DeserializeObject<ValidateCR>(result);
                        CrName = validateCR?.Crname;
                        CRNumber = validateCR?.Crnum;
                        CRNationalNumber = validateCR?.Z700Crnum;

                        EnableCRNationalInputField = string.IsNullOrEmpty(CRNationalNumber);
                        if (!string.IsNullOrEmpty(CrName))
                        {
                            EnableCRInputField = false;
                        }
                        else
                        {
                            EnableCRInputField = true;
                        }

                        makeDropdownFieldsNorEditable();
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


                            List<NregMulSet> filteredActivities = taxPayerDetails?.Nreg_Mul_ActivitySet?.Where(a => a.Idnumber == CRNumber).ToList();

                            existingActivitiesList = UtilityManager.GetExistingActivities(filteredActivities, existingActivities, activityList);

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
                            CRNumber = validateCR.Z700Crnum;
                        }

                    }

                }
            }
            catch (Exception)
            {

            }



            if (validateCR != null)
            {
                if (!string.IsNullOrEmpty(validateCR.Z700Crnum))
                {
                    CRNumber = validateCR.Z700Crnum;
                }

            }

            IsLoading = false;
            updateCRAttachments();
            if (validateCR?.NotFound == "X")
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateCRNumberInValid));
                return;
            }
            if (validateCR?.Excption == "X")
            {
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateCRNumberInValid));
                return;
            }
            CRIssueCity = new CityDropdownItem()
            {
                CityName = OutletDropDowns.city_dropdownSet.Where(i => i.CityCode == validateCR?.CityCode).FirstOrDefault().CityName,
                CityCode = validateCR?.CityCode,
            };
            CRValidFrom = validateCR?.Issuedt;//?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
            EnableInputFields = string.IsNullOrEmpty(validateCR?.Crname);
            updateDatePickers(EstablishmentOutletActivitiesTabsEnum.CRDetails);
        }

        private async Task PrepareError(string result)
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

                if (errorID.Contains("896"))
                {
                    await MopupService.Instance.PushAsync(new ErrorMessagePopup(AppResources.Error896));
                }
                else
                {
                    await _dialogService.ShowMessage(WithReplacedString, AppResources.ZError);
                }
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
                list.Add(obj);
            }
            CRsCopies = new ObservableCollection<Attachment>(list);

            list?.Clear();
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
                list.Add(obj);
            }

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

            var confirmPopup = new ZAKATOkCancelPopUpView(AppResources.ZZDeleteAttachmentConfirmationText + " " + item.Filename + QuestionMark);
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
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
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
                else if (string.IsNullOrWhiteSpace(CrName))
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateACRNumber));
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(CRNumber))
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateACRNumber));
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
                else if (string.IsNullOrEmpty(LicenseName))
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateLicenseName));
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
                else
                {
                    return true;
                }
            }
            return true;
        }

        private void ShowAlertPopup(string _message)
        {
            MopupService.Instance.PushAsync(new AttachmentInformationPopUp(_message));
        }


        public void OpenLicenseFormInEditMode(Nreg_ActivityItem LicenseData)
        {
            CurrentTab = EstablishmentOutletActivitiesTabsEnum.LicenseDetails;
            SelectedLicenseItem = LicenseData;
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
            else if (doctype.Equals("RG12"))//PAssport
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
            try
            {
                List<NregMulSet> existingActivitiesList = new List<NregMulSet>();
                NregMulSet existingActivities = null;
                PopUpServiceModel dataModel = null;

                List<NregMulSet> filteredActivities = taxPayerDetails?.Nreg_Mul_ActivitySet?.Where(a => a.Idnumber == IdNumber).ToList();
                int count = filteredActivities?.Count ?? 0;
                if (count > 0)
                {
                    existingActivitiesList = UtilityManager.GetExistingActivities(filteredActivities, existingActivities, activityList, true);
                    dataModel = new PopUpServiceModel
                    {
                        existedActivities = existingActivitiesList,
                    };
                    foreach (var item in dataModel.existedActivities)
                    {
                        NregMulActivityList?.Add(item);
                    }

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
            catch (Exception )
            {

            }

        }
        #endregion
    }
}
