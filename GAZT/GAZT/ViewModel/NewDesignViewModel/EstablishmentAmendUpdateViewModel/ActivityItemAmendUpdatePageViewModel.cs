using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Manager;
using EGAZT.Models;
using EGAZT.Models.EstablishmentRegistration;
using EGAZT.Views.NewDesign.Common;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using EGAZT.Views.NewDesign.GenericPickers;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Plugin.FilePicker;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel.EstablishmentAmendUpdateViewModel
{
    [Preserve(AllMembers = true)]
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
                RaisePropertyChanged(nameof(DisplayCompleteDetailsLabel));
            }
        }
        public TaxPayerDetails taxPayerDetails { get; set; } = null;
        private ActivitySetsList activityList = null;
        public OutletNumber newNumber { get; set; } = null;
        public ValidateCR validateCR { get; set; } = null;
        public Nreg_ActivityItem validateLicense { get; set; } = null;
        public List<Nreg_ActivityItem> NregActivityList = new List<Nreg_ActivityItem>();
        private Nreg_ActivityItem SelectedLicenseItem = null;
        private Nreg_ActivityItem SelectedCRItem = null;
        public ActicityListDelegate goBackAction = null;

        private EstablishmentOutletActivitiesTabsEnum _currentTab = EstablishmentOutletActivitiesTabsEnum.CRDetails;
        public EstablishmentOutletActivitiesTabsEnum CurrentTab
        {
            get => _currentTab;
            set
            {
               // if (_currentTab == value) return;

                _currentTab = value;
                RaisePropertyChanged(nameof(CurrentTab));
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
                RaisePropertyChanged(nameof(ActivityTitle));
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
                RaisePropertyChanged(nameof(LicenseDetails));
            }
        }
        private Dictionary<string, string> EnIssueBy = new Dictionary<string, string>()
        {
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
            {"90718", "Other" }
        };
        private Dictionary<string, string> ArIssueBy = new Dictionary<string, string>()
        {
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
            {"90718", "غير معرف" }
        };
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
                RaisePropertyChanged(nameof(CRIssueCountry));
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
                RaisePropertyChanged(nameof(CRIssueBy));
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
                RaisePropertyChanged(nameof(AddLicenseEnabled));
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
                RaisePropertyChanged(nameof(CRIssueCity));
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
                RaisePropertyChanged(nameof(CRNumber));
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
                RaisePropertyChanged(nameof(EnableInputFields));
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
                RaisePropertyChanged(nameof(EnableIssueByDropDown));
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
                RaisePropertyChanged(nameof(EnableCRInputField));
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
                RaisePropertyChanged(nameof(OutletDropDowns));
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
                RaisePropertyChanged(nameof(SelectedCRValidFromDate));
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
                RaisePropertyChanged(nameof(SelectedCRValidFromHijiriDate));
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
                RaisePropertyChanged(nameof(CRValidFrom));
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
                RaisePropertyChanged(nameof(DisplayCRValidFrom));
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
                RaisePropertyChanged(nameof(AttachmentCount));
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
                RaisePropertyChanged(nameof(MainActivity));
            }
        }
        private ActivityGroupSubGroup _cRMainGroup = null;
        public ActivityGroupSubGroup CRMainGroup
        {
            get => _cRMainGroup;
            set
            {
                if (_cRMainGroup == value) return;

                if (value != null)
                {
                    _cRMainGroup = value;
                    RaisePropertyChanged(nameof(CRMainGroup));
                }
            }
        }
        private ActivityGroupSubGroup _cRSubGroup = null;
        public ActivityGroupSubGroup CRSubGroup
        {
            get => _cRSubGroup;
            set
            {
                if (_cRSubGroup == value) return;

                _cRSubGroup = value;
                RaisePropertyChanged(nameof(CRSubGroup));
            }
        }
        private ActivityGroupSubGroup _cRAcitivity;
        public ActivityGroupSubGroup CRAcitivity
        {
            get => _cRAcitivity;
            set
            {
                if (_cRAcitivity == value) return;

                _cRAcitivity = value;
                RaisePropertyChanged(nameof(CRAcitivity));
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
                    RaisePropertyChanged(nameof(CRsCopies));
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
                    RaisePropertyChanged(nameof(TransferCRsCopies));
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
                RaisePropertyChanged(nameof(SelectedValidFromDate));
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
                RaisePropertyChanged(nameof(SelectedValidFromHijiriDate));
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
                RaisePropertyChanged(nameof(ValidFrom));
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
                RaisePropertyChanged(nameof(DisplayValidFrom));
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
                RaisePropertyChanged(nameof(LicenseIssueCountry));
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
                RaisePropertyChanged(nameof(LicenseIssueBy));
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
                RaisePropertyChanged(nameof(LicenseIssueCity));
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
                RaisePropertyChanged(nameof(LicenseNumber));
            }
        }
        private ActivityGroupSubGroup _licenseMainGroup = null;
        public ActivityGroupSubGroup LicenseMainGroup
        {
            get => _licenseMainGroup;
            set
            {
                if (_licenseMainGroup == value) return;

                _licenseMainGroup = value;
                RaisePropertyChanged(nameof(LicenseMainGroup));
            }
        }
        private ActivityGroupSubGroup _licenseSubGroup = null;
        public ActivityGroupSubGroup LicenseSubGroup
        {
            get => _licenseSubGroup;
            set
            {
                if (_licenseSubGroup == value) return;

                _licenseSubGroup = value;
                RaisePropertyChanged(nameof(LicenseSubGroup));
            }
        }
        private ActivityGroupSubGroup _licenseAcitivity;
        public ActivityGroupSubGroup LicenseAcitivity
        {
            get => _licenseAcitivity;
            set
            {
                if (_licenseAcitivity == value) return;

                _licenseAcitivity = value;
                RaisePropertyChanged(nameof(LicenseAcitivity));
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
                RaisePropertyChanged(nameof(LicensesCopies));
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
                RaisePropertyChanged(nameof(LicenseData));
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
                RaisePropertyChanged(nameof(CanExecute));
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
                                    CRMainGroup = activityList?.act_groupSet?.results?.Where(i => i.Text == PickerModel.SelectedValue).FirstOrDefault(); ;
                                if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                                    LicenseMainGroup = activityList?.act_groupSet?.results?.Where(i => i.Text == PickerModel.SelectedValue).FirstOrDefault(); 
                            }

                            if (PickerModel.PickerId == "SubGroupPicker")
                            {
                                if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                                    CRSubGroup = activityList?.act_subgroupSet?.results?.Where(i => i.Text == PickerModel.SelectedValue).FirstOrDefault(); ;
                                if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                                    LicenseSubGroup = activityList?.act_subgroupSet?.results?.Where(i => i.Text == PickerModel.SelectedValue).FirstOrDefault(); 

                            }
                            if (PickerModel.PickerId == "ActivityPicker")
                            {
                                if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                                {
                                    CRAcitivity = activityList?.activitySet.results?.Where(i => i.Text == PickerModel.SelectedValue).FirstOrDefault(); ;
                                }
                                if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                                {
                                    LicenseAcitivity = activityList?.activitySet.results?.Where(i => i.Text == PickerModel.SelectedValue).FirstOrDefault(); ;

                                }
                            }
                            if (PickerModel.PickerId == "LicenseCountryPicker")
                            {
                                if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                                {
                                    CRIssueCountry = OutletDropDowns.country_dropdownSet.results.Where(i => i.Landx50 == PickerModel.SelectedValue).FirstOrDefault();

                                    if (App.IsArabic)
                                    {
                                        CRIssueBy = CRIssueCountry.Land1 == "SA" ? ArIssueBy["90702"] : ArIssueBy["90718"];
                                    }
                                    else
                                    {
                                        CRIssueBy = CRIssueCountry.Land1 == "SA" ? EnIssueBy["90702"] : EnIssueBy["90718"];
                                    }
                                    CRIssueCity = null;
                                }
                                if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                                {
                                    LicenseIssueCountry = OutletDropDowns.country_dropdownSet.results.Where(i => i.Landx50 == PickerModel.SelectedValue).FirstOrDefault();

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
                                CRIssueCity = OutletDropDowns?.city_dropdownSet?.results.Where(i => i.CityName == PickerModel.SelectedValue).FirstOrDefault();

                                if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                                LicenseIssueCity = OutletDropDowns?.city_dropdownSet?.results.Where(i => i.CityName == PickerModel.SelectedValue).FirstOrDefault();
                            }

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); Console.WriteLine(ex.ToString());
                }

                RaisePropertyChanged("PickerModel");
            }
        }

        #endregion

        #region commands
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
        public ActivityItemAmendUpdatePageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            ActivityDetails = new ActivityDetails();
            LicenseDetails = new LicenseDetails();
            OnNextButtonClick = new Command(() => navigateToNext(), () => CanExecute);
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
            OnIssueCountrySelectButtonClick = new Command((object o) =>
            {
                //ListPopUpViewPage poupWindow = new ListPopUpViewPage(OutletDropDowns?.country_dropdownSet?.results);
                //poupWindow.OnItemSelect = (item) =>
                //{
                //    try
                //    {
                //        if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                //        {
                //            CRIssueCountry = item as CountryDropdownItem;
                //            if (App.IsArabic)
                //            {
                //                CRIssueBy = CRIssueCountry.Land1 == "SA" ? ArIssueBy["90702"] : ArIssueBy["90718"];
                //            }
                //            else
                //            {
                //                CRIssueBy = CRIssueCountry.Land1 == "SA" ? EnIssueBy["90702"] : EnIssueBy["90718"];
                //            }
                //            CRIssueCity = null;
                //        }
                //        if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                //        {
                //            LicenseIssueCountry = item as CountryDropdownItem;
                //            LicenseIssueCity = null;
                //        }
                //    }
                //    catch (Exception e)
                //    {
                //        Console.WriteLine(e.StackTrace);
                //    }
                //};
                //PopupNavigation.Instance.PushAsync(poupWindow);
                try
                {
                    List<string> countryDropdownData = new List<string>();

                    foreach (CountryDropdownItem reportingBranch in OutletDropDowns?.country_dropdownSet?.results)
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

                    PopupNavigation.Instance.PushAsync(new PickerPageView(genericPickerModel));
                }
                catch (GAZTUnlockAccountException ex)
                {
                    Console.WriteLine(ex.Message); Console.WriteLine(ex.ToString());
                }
                catch (InternetException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        _navigationService.GoBack();
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); Console.WriteLine(ex.ToString());
                }

            }, CanExecuteClickCommand);
            OnIssueBySelectButtonClick = new Command((object o) =>
            {

                //ListPopUpViewPage poupWindow = new ListPopUpViewPage(App.IsArabic ? ArIssueBy.Values : EnIssueBy.Values);
                //poupWindow.OnItemSelect = (item) =>
                //{
                //    try
                //    {
                //        if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                //            CRIssueBy = item as string;
                //        if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                //            LicenseIssueBy = item as string;
                //    }
                //    catch (Exception e)
                //    {
                //        Console.WriteLine(e.StackTrace);
                //    }
                //};
                //PopupNavigation.Instance.PushAsync(poupWindow);
                try
                {
                    List<string> countryDropdownData = new List<string>();
                    if(App.IsArabic)
                    {
                        foreach (string reportingBranch in ArIssueBy.Values)
                        {
                            if (!string.IsNullOrEmpty(reportingBranch) && !string.IsNullOrWhiteSpace(reportingBranch))
                            {
                                countryDropdownData.Add(reportingBranch);

                            }
                        }
                    }
                    else
                    {
                        foreach (string reportingBranch in EnIssueBy.Values)
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

                    PopupNavigation.Instance.PushAsync(new PickerPageView(genericPickerModel));
                }
                catch (GAZTUnlockAccountException ex)
                {
                    Console.WriteLine(ex.Message); Console.WriteLine(ex.ToString());
                }
                catch (InternetException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        _navigationService.GoBack();
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); Console.WriteLine(ex.ToString());
                }

            }, CanIssueByExecuteClickCommand);
            OnIssueCitySelectButtonClick = new Command((object o) =>
            {
                var filterCities = OutletDropDowns?.city_dropdownSet?.results.Where(i =>
                {
                    if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                    {
                        return i.Country == CRIssueCountry.Land1;
                    }
                    else
                    {
                        return i.Country == LicenseIssueCountry.Land1;
                    }
                }).ToList();
                if (filterCities.Count > 0)
                {
                    //ListPopUpViewPage poupWindow = new ListPopUpViewPage(filterCities);

                    //poupWindow.OnItemSelect = (item) =>
                    //{
                    //    try
                    //    {
                    //        if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                    //            CRIssueCity = item as CityDropdownItem;
                    //        if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                    //            LicenseIssueCity = item as CityDropdownItem;
                    //    }
                    //    catch (Exception e)
                    //    {
                    //        Console.WriteLine(e.StackTrace);
                    //    }
                    //};
                    //PopupNavigation.Instance.PushAsync(poupWindow);
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

                        PopupNavigation.Instance.PushAsync(new PickerPageView(genericPickerModel));
                    }
                    catch (GAZTUnlockAccountException ex)
                    {
                        Console.WriteLine(ex.Message); Console.WriteLine(ex.ToString());
                    }
                    catch (InternetException ex)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                            _navigationService.GoBack();
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message); Console.WriteLine(ex.ToString());
                    }

                }
            }, CanExecuteClickCommand);
            OnTransferCopyOfCRChoiceButtonClick = new Command(async (type) =>
            {
                var typeValue = type as string;
                if (CRsCopies.Count < 5 && typeValue == "RG01")
                {
                    Console.WriteLine("OnTransferCopyOfCRChoiceButtonClick");
                    await AddAttachment(type as string);
                }
                else if (TransferCRsCopies.Count < 5 && typeValue == "RG12")
                {
                    Console.WriteLine("OnTransferCopyOfCRChoiceButtonClick");
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

            OnMainGroupSelectButtonClick = new Command(() =>
            {
                var dropDownData = activityList?.act_groupSet?.results?.ToList();
                //ListPopUpViewPage poupWindow = new ListPopUpViewPage(dropDownData);
                //poupWindow.OnItemSelect = (item) =>
                //{
                //    try
                //    {
                //        if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                //        {
                //            CRMainGroup = item as ActivityGroupSubGroup;
                //            CRSubGroup = null;
                //            CRAcitivity = null;
                //        }
                //        if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                //        {
                //            LicenseMainGroup = item as ActivityGroupSubGroup;
                //            LicenseSubGroup = null;
                //            LicenseAcitivity = null;
                //        }

                //    }
                //    catch (Exception e)
                //    {
                //        Console.WriteLine(e.StackTrace);
                //    }
                //};
                //PopupNavigation.Instance.PushAsync(poupWindow);
                try
                {
                    List<string> reportingBranchData = new List<string>();
                
                        foreach (ActivityGroupSubGroup reportingBranch in activityList?.act_groupSet?.results?.ToList())
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

                    PopupNavigation.Instance.PushAsync(new PickerPageView(genericPickerModel));
                }
                catch (GAZTUnlockAccountException ex)
                {
                    Console.WriteLine(ex.Message); Console.WriteLine(ex.ToString());
                }
                catch (InternetException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        _navigationService.GoBack();
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); Console.WriteLine(ex.ToString());
                }

            });
            OnSubGroupSelectButtonClick = new Command(() =>
            {
                List <ActivityGroupSubGroup> subGroupList = new List<ActivityGroupSubGroup>();
                var dropDownData = new List<ActivityGroupSubGroup>();
                if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                    subGroupList = activityList?.act_subgroupSet?.results?.Where(i => i.IndSector.StartsWith(CRMainGroup?.IndSector)).ToList();
                if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                    subGroupList = activityList?.act_subgroupSet?.results?.Where(i => i.IndSector.StartsWith(LicenseMainGroup?.IndSector)).ToList();

                //ListPopUpViewPage poupWindow = new ListPopUpViewPage(dropDownData);
                //poupWindow.OnItemSelect = (item) =>
                //{
                //    try
                //    {
                //        if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                //        {
                //            CRSubGroup = item as ActivityGroupSubGroup;
                //            CRAcitivity = null;
                //        }
                //        if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                //        {
                //            LicenseSubGroup = item as ActivityGroupSubGroup;
                //            LicenseAcitivity = null;
                //        }
                //    }
                //    catch (Exception e)
                //    {
                //        Console.WriteLine(e.StackTrace);
                //    }
                //};
                //PopupNavigation.Instance.PushAsync(poupWindow);

                try
                {
                    List<string> reportingBranchData = new List<string>();

                    foreach (ActivityGroupSubGroup reportingBranch in subGroupList)
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

                    PopupNavigation.Instance.PushAsync(new PickerPageView(genericPickerModel));
                }
                catch (GAZTUnlockAccountException ex)
                {
                    Console.WriteLine(ex.Message); Console.WriteLine(ex.ToString());
                }
                catch (InternetException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        _navigationService.GoBack();
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); Console.WriteLine(ex.ToString());
                }

            });
            OnAcitivitySelectButtonClick = new Command(() =>
            {
                List<ActivityGroupSubGroup> subGroupList = new List<ActivityGroupSubGroup>();

                var dropDownData = new List<ActivityGroupSubGroup>();
                if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                    subGroupList = activityList?.activitySet?.results?.Where(i => i.IndSector.StartsWith(CRSubGroup?.IndSector)).ToList();
                if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                    subGroupList = activityList?.activitySet?.results?.Where(i => i.IndSector.StartsWith(LicenseSubGroup?.IndSector)).ToList();

                //ListPopUpViewPage poupWindow = new ListPopUpViewPage(dropDownData);
                //poupWindow.OnItemSelect = (item) =>
                //{
                //    try
                //    {
                //        if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
                //        {
                //            CRAcitivity = item as ActivityGroupSubGroup;
                //            CRMainGroup = activityList.act_groupSet.results.Where(i => i.IndSector.StartsWith(CRAcitivity?.IndSector?.Substring(0, 2))).FirstOrDefault();
                //            CRSubGroup = activityList.act_subgroupSet.results.Where(i => i.IndSector.StartsWith(CRAcitivity?.IndSector?.Substring(0, 4))).FirstOrDefault();
                //        }
                //        if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                //        {
                //            LicenseAcitivity = item as ActivityGroupSubGroup;
                //            LicenseMainGroup = activityList.act_groupSet.results.Where(i => i.IndSector.StartsWith(LicenseAcitivity?.IndSector?.Substring(0, 2))).FirstOrDefault();
                //            LicenseSubGroup = activityList.act_subgroupSet.results.Where(i => i.IndSector.StartsWith(LicenseAcitivity?.IndSector?.Substring(0, 4))).FirstOrDefault();
                //        }
                //    }
                //    catch (Exception e)
                //    {
                //        Console.WriteLine(e.StackTrace);
                //    }
                //};
                //PopupNavigation.Instance.PushAsync(poupWindow);
                try
                {
                    List<string> reportingBranchData = new List<string>();

                    foreach (ActivityGroupSubGroup reportingBranch in subGroupList)
                    {
                        if (!string.IsNullOrEmpty(reportingBranch.Text) && !string.IsNullOrWhiteSpace(reportingBranch.Text))
                        {
                            reportingBranchData.Add(reportingBranch.Text);

                        }
                    }

                    GenericPickerModel genericPickerModel = new GenericPickerModel();
                    genericPickerModel.PickerData = reportingBranchData;
                    //genericPickerModel.PickerTitle = AppResources.TinDeregistrationReason;
                    genericPickerModel.PickerId = "ActivityPicker";

                    PopupNavigation.Instance.PushAsync(new PickerPageView(genericPickerModel));
                }
                catch (GAZTUnlockAccountException ex)
                {
                    Console.WriteLine(ex.Message); Console.WriteLine(ex.ToString());
                }
                catch (InternetException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        _navigationService.GoBack();
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); Console.WriteLine(ex.ToString());
                }

            });

            TappedOnAttachmentInformationIcon = new Command(() =>
            ShowAlertPopup(
                AppResources.ESTAttachmentSizeNotfication
                + System.Environment.NewLine
                + AppResources.ZZChooseonlyfilewithextensionForZAKAT
                + System.Environment.NewLine + AppResources.ZMaximumnoof5attachmentscanbeuploaded
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
                case Enums.PageExecutionType.Amend:
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
                        ActivityDetails.MainGroup = false;
                        ActivityDetails.SubGroup = false;
                        ActivityDetails.Activity = false;

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
                    }

                    break;
                case Enums.PageExecutionType.Update:
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
        private async void navigateToNext()
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
                        Nreg_ActivityItem item = new Nreg_ActivityItem
                        {
                            Type = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? "BUP002" : "ZS0004",
                            ValidDateFrom = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? crIssueDate : issueDate,
                            ValidDateTo = maxDate,
                            Idnumber = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? CRNumber : LicenseNumber,
                            Country = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? CRIssueCountry.Land1 : LicenseIssueCountry.Land1,
                            City = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? CRIssueCity.CityName : LicenseIssueCity.CityName,
                            CityCode = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? CRIssueCity.CityCode : LicenseIssueCity.CityCode,
                            Activity = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? CRAcitivity.IndSector : LicenseAcitivity.IndSector,
                            ActMgrp = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? CRMainGroup.IndSector : LicenseMainGroup.IndSector,
                            ActSgrp = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? CRSubGroup.IndSector : LicenseSubGroup.IndSector,
                            Actcat = MainActivity ? "M" : "S",
                            Actno = $"{Int16.Parse(newNumber?.Actno):00000}",
                            Crattfg = CRsCopies.Count > 0 ? "X" : string.Empty
                        };

                        if (App.IsArabic)
                        {
                            item.Institute = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? ArIssueBy.FirstOrDefault(i => i.Value == CRIssueBy).Key : ArIssueBy.FirstOrDefault(i => i.Value == LicenseIssueBy).Key;
                        }
                        else
                        {
                            item.Institute = CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails ? EnIssueBy.FirstOrDefault(i => i.Value == CRIssueBy).Key : EnIssueBy.FirstOrDefault(i => i.Value == LicenseIssueBy).Key;
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
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                CanExecute = true;
            }
        }
        public void OnAppearing()
        {
            var activityItems = taxPayerDetails?.Nreg_ActivitySet.results.Where(i => (new List<string> { "BUP002", "ZS0004" }).Contains(i.Type) && i.Actno == newNumber?.Actno).ToList();
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
                string[] filetypes = DependencyService.Get<IDeviceInfo>().GetAttachmentTypeStringForTaxEvasion();
                var fileData = await CrossFilePicker.Current.PickFile(filetypes);

                if (fileData != null)
                {
                    var attachmentByte = fileData.DataArray;

                    string base64String = Convert.ToBase64String(attachmentByte, 0, attachmentByte.Length);
                    var attachmentName = fileData.FileName;
                    bool isFileAlreayUploaded = IsFileAlreadyAttached(docType, attachmentName);
                    if (!isFileAlreayUploaded)
                    {
                        float sizemb = (attachmentByte.Length / 1024f) / 1024f;
                        decimal attachmentSize = 0;
                        attachmentSize = attachmentSize + (Decimal)sizemb;

                        if (fileData.FileName.Contains("."))
                        {
                            string Extention = fileData.FileName.Split('.')[1];
                            if (Extention.ToLower() == "doc" || Extention.ToLower() == "docx" || Extention.ToLower() == "jpg" || Extention.ToLower() == "pdf" || Extention.ToLower() == "jpeg")
                            {
                                attachmentSize = Math.Round(Convert.ToDecimal((Convert.ToDouble(attachmentByte.Length) / 1048576.0)), 2);
                                decimal AttachmentSizeTillFourDecimal = Math.Round(Convert.ToDecimal((Convert.ToDouble(attachmentByte.Length) / 1048576.0)), 4);
                                if (Convert.ToDecimal(attachmentSize) <= 10)
                                {
                                    if (Convert.ToDecimal(AttachmentSizeTillFourDecimal) > 0)
                                    {
                                        try
                                        {
                                            string attachmentType = UtilityManager.GetContentType(Extention);
                                            await SaveAttachment(attachmentByte, attachmentName, docType, attachmentType);
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
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZFileWithTheSameNameAlreadyExists));
                            IsLoading = false;
                        });
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
        private async void updateActivityList(string indSector)
        {
            IsLoading = true;
            activityList = await EstablishmentRegistrationWebServiceManager.ESTOutletGetActivitySetsList(indSector);
            IsLoading = false;
        }
        private async void fetchTabDataAndBind()
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
                    CRIssueCountry = OutletDropDowns.country_dropdownSet.results.Where(i => i.Land1 == "SA").FirstOrDefault();
                    if (App.IsArabic)
                    {
                        CRIssueBy = CRIssueCountry.Land1 == "SA" ? ArIssueBy["90702"] : ArIssueBy["90718"];
                    }
                    else
                    {
                        CRIssueBy = CRIssueCountry.Land1 == "SA" ? EnIssueBy["90702"] : EnIssueBy["90718"];
                    }
                    CRIssueCity = new CityDropdownItem()
                    {
                        CityName = validateCR?.CityAry,
                        CityCode = string.Empty
                    };
                    CRValidFrom = validateCR?.Issuedt?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                    EnableCRInputField = string.IsNullOrEmpty(validateCR?.Crname);
                    SelectedCRItem = taxPayerDetails?.Nreg_ActivitySet?.results?.FirstOrDefault(i => i.Type == "BUP002");
                    if (SelectedCRItem != null)
                    {
                        EnableIssueByDropDown = false;
                        CRNumber = SelectedCRItem?.Idnumber;
                        CRIssueCountry = OutletDropDowns.country_dropdownSet.results.Where(i => i.Land1 == SelectedCRItem?.Country).FirstOrDefault();
                        CRIssueBy = App.IsArabic ? ArIssueBy[SelectedCRItem?.Institute] : EnIssueBy[SelectedCRItem?.Institute];
                        CRValidFrom = SelectedCRItem?.ValidDateFrom?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));

                        CRIssueCity = new CityDropdownItem()
                        {
                            CityName = SelectedCRItem?.City,
                            CityCode = SelectedCRItem?.CityCode
                        };

                        if (SelectedCRItem.Actcat.Equals("M"))
                        {
                            MainActivity = true;
                        }
                        else
                        {
                            MainActivity = false;
                        }
                        updateActivityList(SelectedCRItem?.Activity);
                        CRAcitivity = activityList.activitySet.results.Where(i => i.IndSector == SelectedCRItem?.Activity).FirstOrDefault();
                        CRMainGroup = activityList.act_groupSet.results.Where(i => i.IndSector == SelectedCRItem?.ActMgrp).FirstOrDefault();
                        CRSubGroup = activityList.act_subgroupSet.results.Where(i => i.IndSector == SelectedCRItem?.ActSgrp).FirstOrDefault();
                        updateCRAttachments();
                    }
                    if (!string.IsNullOrEmpty(CRNumber))
                        validateCRNumber();
                }
                else if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
                {
                    OutletDropDowns = await EstablishmentRegistrationWebServiceManager.ESTOutletDropDowns();
                    activityList = await EstablishmentRegistrationWebServiceManager.ESTOutletGetActivitySetsList();

                    if ((/*editModeEnabled == true &&*/ SelectedLicenseItem != null) || validateLicense != null)
                    {
                        if (SelectedLicenseItem == null && validateLicense != null)
                        {
                            SelectedLicenseItem = validateLicense;
                        }
                        LicenseNumber = SelectedLicenseItem?.Idnumber;
                        LicenseIssueCountry = OutletDropDowns.country_dropdownSet.results.Where(i => i.Land1 == SelectedLicenseItem?.Country).FirstOrDefault();
                        LicenseIssueBy = App.IsArabic ? ArIssueBy[SelectedLicenseItem?.Institute] : EnIssueBy[SelectedLicenseItem?.Institute];
                        ValidFrom = SelectedLicenseItem?.ValidDateFrom?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
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
                        LicenseAcitivity = activityList.activitySet.results.Where(i => i.IndSector == SelectedLicenseItem?.Activity).FirstOrDefault();
                        LicenseMainGroup = activityList.act_groupSet.results.Where(i => i.IndSector == SelectedLicenseItem?.ActMgrp).FirstOrDefault();
                        LicenseSubGroup = activityList.act_subgroupSet.results.Where(i => i.IndSector == SelectedLicenseItem?.ActSgrp).FirstOrDefault();

                        List<Attachment> list = new List<Attachment>();
                        var lists = taxPayerDetails.AttDetSet.results.Where(x => x.Dotyp == "RG02" && x.OutletRef == string.Format("{0}-{1}", SelectedLicenseItem?.Actno, SelectedLicenseItem?.Idnumber)).ToList();
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
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
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
            if (taxPayerDetails?.Caltp == "G")
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
                if (taxPayerDetails?.Caltp == "G")
                {
                    SelectedCRValidFromDate = _selectedDOBDate;
                    if (!string.IsNullOrWhiteSpace(CRValidFrom))
                        DisplayCRValidFrom = _crValidFrom.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                }
                else
                {
                    SelectedCRValidFromHijiriDate = _selectedDOBDate;
                    if (!string.IsNullOrWhiteSpace(CRValidFrom))
                        DisplayCRValidFrom = _crValidFrom.ToString("yyyy/MM/dd", new CultureInfo("ar-sa"));
                }
            }
            else if (_enum == EstablishmentOutletActivitiesTabsEnum.LicenseDetails)
            {
                DateTime.TryParseExact(ValidFrom, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime _validFrom);
                if (taxPayerDetails?.Caltp == "G")
                {
                    SelectedValidFromDate = _selectedDOBDate;
                    if (!string.IsNullOrWhiteSpace(ValidFrom))
                        DisplayValidFrom = _validFrom.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                }
                else
                {
                    SelectedValidFromHijiriDate = _selectedDOBDate;
                    if (!string.IsNullOrWhiteSpace(ValidFrom))
                        DisplayValidFrom = _validFrom.ToString("yyyy/MM/dd", new CultureInfo("ar-sa"));
                }
            }
        }
        public async void validateCRNumber()
        {
            IsLoading = true;
            validateCR = await EstablishmentRegistrationWebServiceManager.ESTValidateCRNum(CRNumber);
            IsLoading = false;
            updateCRAttachments();
            if (validateCR?.NotFound == "X")
            {
                await _dialogService.ShowMessage(AppResources.ESTValidateCRNumberInValid, AppResources.Information);
                return;
            }
            if (validateCR?.Excption == "X")
            {
                return;
            }
            CRIssueCity = new CityDropdownItem()
            {
                CityName = validateCR?.CityAry,
                CityCode = string.Empty
            };
            CRValidFrom = validateCR?.Issuedt?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
            EnableInputFields = string.IsNullOrEmpty(validateCR?.Crname);
            updateDatePickers(EstablishmentOutletActivitiesTabsEnum.CRDetails);
        }
        private void updateCRAttachments()
        {
            List<Attachment> list = new List<Attachment>();
            var lists = taxPayerDetails.AttDetSet.results.Where(x =>
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
                if (!list.Contains(obj))
                {
                    list.Add(obj);
                }
            }
            CRsCopies = new ObservableCollection<Attachment>(list);


            list.Clear();
            lists = taxPayerDetails.AttDetSet.results.Where(x =>
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
                if (!list.Contains(obj))
                {
                    list.Add(obj);
                }
            }
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
        private async Task SaveAttachment(byte[] attachmentByteData, string fileName, string docType, string contentType)
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
                    string outletref = $"{Int16.Parse(newNumber?.Actno):00000-}" + CRLicenseNo;


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
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                IsLoading = false;
            }
        }
        private async void OnDeleteAttachment(Attachment item, string docType)
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
            await PopupNavigation.Instance.PushAsync(confirmPopup);
        }

        private async Task<bool> ValidateForm()
        {
            if (CurrentTab == EstablishmentOutletActivitiesTabsEnum.CRDetails)
            {
                if (CRIssueCountry == null)
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAIssueCountry));
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(CRIssueBy))
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAIssueBy));
                    return false;
                }
                else if (CRIssueCity == null)
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAIssueCity));
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(CRNumber))
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateACRNumber));
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(CRValidFrom))
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateACRValidFrom));
                    return false;
                }
                else if (CRsCopies == null || CRsCopies.Count == 0)
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAAttachCR));
                    return false;
                }
                else if (CRMainGroup == null && ActivityDetails.MainGroup == true)
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAMainGroup));
                    return false;
                }
                else if (CRSubGroup == null && ActivityDetails.SubGroup == true)
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateASubGroup));
                    return false;
                }
                else if (CRAcitivity == null && ActivityDetails.Activity == true)
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateActivity));
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
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAIssueCountry));
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(LicenseIssueBy))
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAIssueBy));
                    return false;
                }
                else if (LicenseIssueCity == null)
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAIssueCity));
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(LicenseNumber))
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateACLicense));
                    return false;
                }
                else if (string.IsNullOrWhiteSpace(ValidFrom))
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateALicenseValidFrom));
                    return false;
                }
                else if (LicensesCopies == null || LicensesCopies.Count == 0)
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAAttachLicense));
                    return false;
                }
                else if (LicenseMainGroup == null)
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAMainGroup));
                    return false;
                }
                else if (LicenseSubGroup == null)
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateASubGroup));
                    return false;
                }
                else if (LicenseAcitivity == null)
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateActivity));
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

        #endregion
    }
}
