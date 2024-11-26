using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using Mopups.Services;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.EstablishmentRegistration;
using ZATCAMAUI.Views.NewDesign.Common;
using ZATCAMAUI.Views.NewDesign.EstablishmentRegistrationPages;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.EstablishmentRegistration
{

    public class EstablishmentRegistrationPageViewModel : BaseViewModel
    {
        #region Variable

        public static TaxPayerDetails taxPayerDetails { get; set; } = null;
        private FinancialDetail financialDetail { get; set; } = null;
        private FinancialDetail financialDetailPeriod { get; set; } = null;
        public bool isFinaceDetailsChanged { get; set; } = false;
        public string isDraftEnabled { get; set; } = "";
        private Nreg_IdItem idItem { get; set; } = null;
        private string calType = string.Empty;
        public bool IsNavigationCompletedToSuccessfulPage { get; set; } = false;
        private EstablishmentRegistrationTabsEnum _currentTab;
        public EstablishmentRegistrationTabsEnum currentTab
        {
            get => _currentTab;
            set
            {
                if (_currentTab == value)
                {
                    if (IsNavigationCompletedToSuccessfulPage == false)
                    {
                       MainThread.BeginInvokeOnMainThread(async() => await fetchTabDataAndBind(_currentTab));
                    }
                    return;
                }
                _currentTab = value;
                OnPropertyChanged(nameof(currentTab));
                CurrentIndex = (int)_currentTab;
                OnPropertyChanged(nameof(CurrentIndex));
                switch (value)
                {
                    case EstablishmentRegistrationTabsEnum.TaxpayerDetail:
                        SelectedTabText = AppResources.ESTTaxpayerPersonalDetailsTabTitleLabel;
                        NxtButtonLabel = AppResources.ZZNext;
                        break;
                    case EstablishmentRegistrationTabsEnum.PassportDetails:
                        SelectedTabText = AppResources.ESTPassportDetailsTabTitleLabel;
                        NxtButtonLabel = AppResources.ZZNext;
                        break;
                    case EstablishmentRegistrationTabsEnum.Outlets:
                        SelectedTabText = AppResources.ESTOutletsTabTitleLabel;
                        NxtButtonLabel = AppResources.ZZNext;
                        break;
                    case EstablishmentRegistrationTabsEnum.FinancialDetail:
                        SelectedTabText = AppResources.VATRFinancialDetails;
                        NxtButtonLabel = AppResources.ZZNext;
                        break;
                    case EstablishmentRegistrationTabsEnum.Declaration:
                        SelectedTabText = AppResources.ZVatSummary;
                        NxtButtonLabel = AppResources.Submit;
                        break;
                    case EstablishmentRegistrationTabsEnum.RegistrationType:
                    default:
                        SelectedTabText = AppResources.ESTRegTaxTabTitleLabel;
                        NxtButtonLabel = AppResources.ZZNext;
                        break;
                }
               MainThread.BeginInvokeOnMainThread(async() => await fetchTabDataAndBind(_currentTab));
            }
        }
        public ObservableCollection<string> _tabList { get; set; }
            = new ObservableCollection<string>{ AppResources.ESTRegTaxTabTitleLabel, AppResources.ESTTaxpayerPersonalDetailsTabTitleLabel,
                AppResources.ESTPassportDetailsTabTitleLabel, AppResources.ESTOutletsTabTitleLabel,
                AppResources. VATRFinancialDetails, AppResources.ZVatSummary };

        public ObservableCollection<string> TabList
        {
            get => _tabList;
            set
            {
                if (_tabList == value) return;

                _tabList = value;
                OnPropertyChanged(nameof(TabList));
            }
        }
        public bool MarkComplete { get; set; } = false;
        private int _maxIndex = 6;
        public int MaxIndex
        {
            get => _maxIndex; set
            {
                _maxIndex = value;
                OnPropertyChanged(nameof(MaxIndex));
            }
        }

        private int _currenrIndex;
        public int CurrentIndex
        {
            get => _currenrIndex;
            set
            {
                if (_currenrIndex == value) return;

                _currenrIndex = value;
                OnPropertyChanged(nameof(CurrentIndex));
                MarkComplete = _currenrIndex == MaxIndex;
                OnPropertyChanged(nameof(MarkComplete));
            }
        }

        private string _selectedTabText = AppResources.ESTRegTaxTabTitleLabel;
        public string SelectedTabText
        {
            get => _selectedTabText;
            set
            {
                if (_selectedTabText == value) return;

                _selectedTabText = value;
                OnPropertyChanged(nameof(SelectedTabText));
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

        #region Registration Details Tab Variables

        private bool _isClickedOwnRentOption = false;
        public bool IsClickedOwnRentOption
        {
            get => _isClickedOwnRentOption;
            set
            {
                if (_isClickedOwnRentOption == value) return;

                _isClickedOwnRentOption = value;
                OnPropertyChanged("IsClickedOwnRentOption");
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
        private ActivitySetsList activityList = null;


        private bool _isClickedStayMoreThanKSAOption = false;
        public bool IsClickedStayMoreThanKSAOption
        {
            get => _isClickedStayMoreThanKSAOption;
            set
            {
                if (_isClickedStayMoreThanKSAOption == value) return;

                _isClickedStayMoreThanKSAOption = value;
                OnPropertyChanged("IsClickedStayMoreThanKSAOption");
            }
        }


        private bool _isClickedNoneOfTheAboveOption = false;
        public bool IsClickedNoneOfTheAboveOption
        {
            get => _isClickedNoneOfTheAboveOption;
            set
            {
                if (_isClickedNoneOfTheAboveOption == value) return;

                _isClickedNoneOfTheAboveOption = value;
                OnPropertyChanged("IsClickedNoneOfTheAboveOption");
            }
        }


        private bool _isClickedPermanentLegalEntity = false;
        public bool IsClickedPermanentLegalEntity
        {
            get => _isClickedPermanentLegalEntity;
            set
            {
                if (_isClickedPermanentLegalEntity == value) return;

                _isClickedPermanentLegalEntity = value;
                OnPropertyChanged("IsClickedPermanentLegalEntity");
            }
        }


        private bool _isClickedOtherTaxableIncomeLegalEntity = false;
        public bool IsClickedOtherTaxableIncomeLegalEntity
        {
            get => _isClickedOtherTaxableIncomeLegalEntity;
            set
            {
                if (_isClickedOtherTaxableIncomeLegalEntity == value) return;

                _isClickedOtherTaxableIncomeLegalEntity = value;
                OnPropertyChanged("IsClickedOtherTaxableIncomeLegalEntity");
            }
        }




        private bool _isClickedABranchOfNonResidentCompanyPE = false;
        public bool IsClickedABranchOfNonResidentCompanyPE
        {
            get => _isClickedABranchOfNonResidentCompanyPE;
            set
            {
                if (_isClickedABranchOfNonResidentCompanyPE == value) return;

                _isClickedABranchOfNonResidentCompanyPE = value;
                OnPropertyChanged("IsClickedABranchOfNonResidentCompanyPE");
            }
        }



        private bool _isClickedConstructionSitePE = false;
        public bool IsClickedConstructionSitePE
        {
            get => _isClickedConstructionSitePE;
            set
            {
                if (_isClickedConstructionSitePE == value) return;

                _isClickedConstructionSitePE = value;
                OnPropertyChanged("IsClickedConstructionSitePE");
            }
        }


        private bool _isClickedInstallationPE = false;
        public bool IsClickedInstallationPE
        {
            get => _isClickedInstallationPE;
            set
            {
                if (_isClickedInstallationPE == value) return;

                _isClickedInstallationPE = value;
                OnPropertyChanged("IsClickedInstallationPE");
            }
        }


        private bool _isClickedAFixedBasePE = false;
        public bool IsClickedAFixedBasePE
        {
            get => _isClickedAFixedBasePE;
            set
            {
                if (_isClickedAFixedBasePE == value) return;

                _isClickedAFixedBasePE = value;
                OnPropertyChanged("IsClickedAFixedBasePE");
            }
        }

        private bool _isFinancePeriodVisible = false;
        public bool IsFinancePeriodVisible
        {
            get => _isFinancePeriodVisible;
            set
            {

                if (_isFinancePeriodVisible == value) return;
                _isFinancePeriodVisible = value;
                OnPropertyChanged("IsFinancePeriodVisible");
            }
        }

        private bool _isClickedNonResidentPartnerPE = false;
        public bool IsClickedNonResidentPartnerPE
        {
            get => _isClickedNonResidentPartnerPE;
            set
            {
                if (_isClickedNonResidentPartnerPE == value) return;

                _isClickedNonResidentPartnerPE = value;
                OnPropertyChanged("IsClickedNonResidentPartnerPE");
            }
        }
        private bool _showLicenceNoData = true;
        public bool ShowLicenceNoData
        {
            get => _showLicenceNoData;
            set
            {
                if (_showLicenceNoData == value) return;

                _showLicenceNoData = value;
                OnPropertyChanged("ShowLicenceNoData");
            }
        }

        private bool _showCRNoData = true;
        public bool ShowCRNoData
        {
            get => _showCRNoData;
            set
            {
                if (_showCRNoData == value) return;

                _showCRNoData = value;
                OnPropertyChanged("ShowCRNoData");
            }
        }

        private string _selectedTpresidence = null;
        public string SelectedTpresidence
        {
            get => _selectedTpresidence;
            set
            {
                if (_selectedTpresidence == value) return;

                _selectedTpresidence = value;
                OnPropertyChanged(nameof(SelectedTpresidence));
            }
        }

        private string _selectedOrgNonResident = null;
        public string SelectedOrgNonResident
        {
            get => _selectedOrgNonResident;
            set
            {
                if (_selectedOrgNonResident == value) return;

                _selectedOrgNonResident = value;
                OnPropertyChanged(nameof(SelectedOrgNonResident));
            }
        }

        private string _selectedOrgNonResidentOptions = null;
        public string SelectedOrgNonResidentOptions
        {
            get => _selectedOrgNonResidentOptions;
            set
            {
                if (_selectedOrgNonResidentOptions == value) return;

                _selectedOrgNonResidentOptions = value;
                OnPropertyChanged(nameof(SelectedOrgNonResidentOptions));
            }
        }

        private string _selectedOrgNonResidentActivity = null;
        public string SelectedOrgNonResidentActivity
        {
            get => _selectedOrgNonResidentActivity;
            set
            {
                if (_selectedOrgNonResidentActivity == value) return;

                _selectedOrgNonResidentActivity = value;
                OnPropertyChanged(nameof(SelectedOrgNonResidentActivity));
            }
        }


        private ObservableCollection<string> _orgNonResidentActivityList = new ObservableCollection<string>()
        {
            AppResources.ESTOrgNonResidentActivityValueOne, AppResources.ESTOrgNonResidentActivityValueTwo,
            AppResources.ESTOrgNonResidentActivityValueThree, AppResources.ESTOrgNonResidentActivityValueFour,
            AppResources.ESTOrgNonResidentActivityValueFive, AppResources.ESTOrgNonResidentActivityValueSix,
            AppResources.ESTOrgNonResidentActivityValueSeven, AppResources.ESTOrgNonResidentActivityValueEight,
            AppResources.ESTOrgNonResidentActivityValueNine
        };
        public ObservableCollection<string> OrgNonResidentActivityList
        {
            get
            {
                return _orgNonResidentActivityList;
            }
            set
            {
                if (_orgNonResidentActivityList == value) return;

                if (value != null)
                {
                    _orgNonResidentActivityList = value;
                    OnPropertyChanged(nameof(OrgNonResidentActivityList));
                }
            }
        }

        private string _selectedOrgNonResidentActivityItem = null;
        public string SelectedOrgNonResidentActivityItem
        {
            get => _selectedOrgNonResidentActivityItem;
            set
            {
                if (_selectedOrgNonResidentActivityItem == value) return;

                _selectedOrgNonResidentActivityItem = value;

                if (SelectedOrgNonResidentActivityItem != null)
                {
                    SelectOrgNonResidentActivity(SelectedOrgNonResidentActivityItem);
                }
                OnPropertyChanged(nameof(SelectedOrgNonResidentActivityItem));
            }
        }

        private bool _isAttachmentEnable = false;
        public bool IsAttachmentEnabled
        {
            get
            {
                return _isAttachmentEnable;
            }
            set
            {
                if (_isAttachmentEnable == value) return;

                _isAttachmentEnable = value;
                OnPropertyChanged("IsAttachmentEnable");
            }
        }

        private BranchesDropDownModel _selectedReportingBranch = null;
        public BranchesDropDownModel SelectedReportingBranch
        {
            get => _selectedReportingBranch;
            set
            {
                if (_selectedReportingBranch == value) return;

                _selectedReportingBranch = value;
                OnPropertyChanged(nameof(SelectedReportingBranch));
            }
        }


        private List<BranchesDropDownModel> _reportingBranchList = new List<BranchesDropDownModel>();
        public List<BranchesDropDownModel> ReportingBranchList
        {
            get => _reportingBranchList;
            set
            {
                if (_reportingBranchList == value) return;

                _reportingBranchList = value;
                OnPropertyChanged(nameof(ReportingBranchList));
            }
        }


        private string _selectedEntityType = string.Empty;
        public string SelectedEntityType
        {
            get => _selectedEntityType;
            set
            {
                if (_selectedEntityType == value) return;

                _selectedEntityType = value;
                OnPropertyChanged(nameof(SelectedEntityType));
            }
        }

        private string _selectedTaxPayerType = string.Empty;
        public string SelectedTaxPayerType
        {
            get => _selectedTaxPayerType;
            set
            {
                if (_selectedTaxPayerType == value) return;

                _selectedTaxPayerType = value;
                OnPropertyChanged(nameof(SelectedTaxPayerType));
            }
        }
        private Dictionary<string, string> NationalityMapping = new Dictionary<string, string>() {
            { "SAUDI", AppResources.ESTNationalitySAUDI },
            { "GCC", AppResources.ESTNationalityGCC },
            { "FOREIGN", AppResources.ESTNationalityFOREIGN }
        };
        private string _selectedRegNationalityType = string.Empty;
        public string SelectedRegNationalityType
        {
            get => _selectedRegNationalityType;
            set
            {
                if (_selectedRegNationalityType == value) return;

                _selectedRegNationalityType = value;
                OnPropertyChanged(nameof(SelectedRegNationalityType));
            }
        }

        private string _selectedLegalEntity;
        private bool _isSaudi = false;
        public bool IsSaudi
        {
            get => _isSaudi;
            set
            {
                if (_isSaudi == value) return;

                _isSaudi = value;
                OnPropertyChanged(nameof(IsSaudi));
            }
        }
        public string SelectedLegalEntity
        {
            get => _selectedLegalEntity;
            set
            {
                if (_selectedLegalEntity == value) return;

                _selectedLegalEntity = value;
                OnPropertyChanged(nameof(SelectedLegalEntity));
            }
        }

        private ObservableCollection<Attachment> _uploadedRentDocumentsList = new ObservableCollection<Attachment>();
        public ObservableCollection<Attachment> UploadedRentDocumentsList
        {
            get
            {
                return _uploadedRentDocumentsList;
            }
            set
            {
                if (_uploadedRentDocumentsList == value) return;

                _uploadedRentDocumentsList = value;
                if (UploadedRentDocumentsList.Count > 0)
                {
                    IsVisbleRentAttachmentmentList = true;
                }
                OnPropertyChanged(nameof(UploadedRentDocumentsList));
            }
        }


        private bool _isVisbleRentAttachmentmentList;
        public bool IsVisbleRentAttachmentmentList
        {
            get => _isVisbleRentAttachmentmentList;
            set
            {
                if (_isVisbleRentAttachmentmentList == value) return;

                _isVisbleRentAttachmentmentList = value;
                OnPropertyChanged(nameof(IsVisbleRentAttachmentmentList));
            }
        }

        private string _selectedRentFileName;
        public string SelectedRentFileName
        {
            get => _selectedRentFileName;
            set
            {
                if (_selectedRentFileName == value) return;

                _selectedRentFileName = value;
                OnPropertyChanged(nameof(SelectedRentFileName));
            }
        }

        #endregion

        #region TaxPayer Personal Details Tab Variables

        private ObservableCollection<string> _genderList = new ObservableCollection<string>();
        public ObservableCollection<string> GenderList
        {
            get => _genderList;
            set
            {
                if (_genderList == value) return;

                if (value != null)
                {
                    _genderList = value;
                    OnPropertyChanged(nameof(GenderList));
                }
                else
                {
                    _genderList = null;
                    OnPropertyChanged(nameof(GenderList));
                }
            }
        }
        private string _selectedGender;
        public string SelectedGender
        {
            get => _selectedGender;
            set
            {
                if (_selectedGender == value) return;

                _selectedGender = value;
                OnPropertyChanged(nameof(SelectedGender));
            }
        }

        Dictionary<string, string> EnIDType = new Dictionary<string, string>() {
            { "ZS0001", "National ID" },
            { "ZS0002", "Iqama ID" },
            { "ZS0003", "GCC ID" }
        };
        Dictionary<string, string> ArIDType = new Dictionary<string, string>() {
             { "ZS0001", "رقم الهوية الوطنية" },
            { "ZS0002", "رقم الإقامة" },
            { "ZS0003", "رقم هوية مواطني دول الخليج" }
        };

        private string _gCCIDType = string.Empty;
        public string GCCIDType
        {
            get => _gCCIDType;
            set
            {
                if (_gCCIDType == value) return;

                _gCCIDType = value;
                OnPropertyChanged(nameof(GCCIDType));
            }
        }

        private string _gCCIDTypeIdNumberValue = string.Empty;
        public string GCCIDTypeIdNumberValue
        {
            get => _gCCIDTypeIdNumberValue;
            set
            {
                if (_gCCIDTypeIdNumberValue == value) return;

                _gCCIDTypeIdNumberValue = value;
                OnPropertyChanged(nameof(GCCIDTypeIdNumberValue));
            }
        }

        private string _selectedDOB = string.Empty;
        public string SelectedDOB
        {
            get => _selectedDOB;
            set
            {
                if (_selectedDOB == value) return;

                _selectedDOB = value;
                OnPropertyChanged(nameof(SelectedDOB));
            }
        }
        private string _displaySelectedDOB = string.Empty;
        public string DisplaySelectedDOB
        {
            get => _displaySelectedDOB;
            set
            {
                if (_displaySelectedDOB == value) return;

                _displaySelectedDOB = value;
                OnPropertyChanged(nameof(DisplaySelectedDOB));
            }
        }
        private ObservableCollection<object> _selectedDOBDate;
        public ObservableCollection<object> SelectedDOBDate
        {
            get
            {
                return _selectedDOBDate;
            }
            set
            {
                if (_selectedDOBDate == value) return;

                _selectedDOBDate = value;
                OnPropertyChanged(nameof(SelectedDOBDate));
            }
        }
        private ObservableCollection<object> _selectedDOBHijiriDate;
        public ObservableCollection<object> SelectedDOBHijiriDate
        {
            get
            {
                return _selectedDOBHijiriDate;
            }
            set
            {
                if (_selectedDOBHijiriDate == value) return;

                _selectedDOBHijiriDate = value;
                OnPropertyChanged(nameof(SelectedDOBHijiriDate));
            }
        }

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                if (_title == value) return;

                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
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
        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (_firstName == value) return;

                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                if (_lastName == value) return;

                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        private string _fatherName;
        public string FatherName
        {
            get => _fatherName;
            set
            {

                if (_fatherName == value) return;

                _fatherName = value;
                OnPropertyChanged(nameof(FatherName));
            }
        }

        private string _grandFatherName;
        public string GrandFatherName
        {
            get => _grandFatherName;
            set
            {
                if (_grandFatherName == value) return;

                _grandFatherName = value;
                OnPropertyChanged(nameof(GrandFatherName));
            }
        }

        private string _iqamaDesc;
        public string IqamaDesc
        {
            get => _iqamaDesc;
            set
            {
                if (_iqamaDesc == value) return;

                _iqamaDesc = value;
                OnPropertyChanged(nameof(IqamaDesc));
            }
        }

        private bool _showIqamaType;
        public bool ShowIqamaType
        {
            get => _showIqamaType;
            set
            {
                if (_showIqamaType == value) return;

                _showIqamaType = value;
                OnPropertyChanged(nameof(ShowIqamaType));
            }
        }

        private string _familyName;
        public string FamilyName
        {
            get => _familyName;
            set
            {
                if (_familyName == value) return;

                _familyName = value;
                OnPropertyChanged(nameof(FamilyName));
            }
        }

        private string _initial;
        public string Initial
        {
            get => _initial;
            set
            {
                if (_initial == value) return;

                _initial = value;

                OnPropertyChanged(nameof(Initial));
            }
        }

        private GenericDatePickerModel _datepickerModel { get; set; }
        public GenericDatePickerModel DatePickerModel
        {
            get
            {
                return _datepickerModel;
            }
            set
            {
                if (_datepickerModel == value) return;

                _datepickerModel = value;
                OnPropertyChanged("DatePickerModel");
            }
        }
        private List<TaxpayerNationality> _taxpayerFullNationlityList;
        public List<TaxpayerNationality> TaxpayerFullNationlityList
        {
            get => _taxpayerFullNationlityList;
            set
            {
                if (_taxpayerFullNationlityList == value) return;

                _taxpayerFullNationlityList = value;
                OnPropertyChanged(nameof(TaxpayerFullNationlityList));
            }
        }

        private List<TaxpayerNationalityLandx50> _taxpayerPDNationlityList;
        public List<TaxpayerNationalityLandx50> TaxpayerPDNationlityList
        {
            get => _taxpayerPDNationlityList;
            set
            {
                if (_taxpayerPDNationlityList == value) return;

                _taxpayerPDNationlityList = value;
                OnPropertyChanged(nameof(TaxpayerPDNationlityList));
            }
        }

        private TaxpayerNationality _selectedTaxpayerPDNationality;
        public TaxpayerNationality SelectedTaxpayerPDNationality
        {
            get => _selectedTaxpayerPDNationality;
            set
            {
                if (_selectedTaxpayerPDNationality == value) return;

                _selectedTaxpayerPDNationality = value;
                OnPropertyChanged(nameof(SelectedTaxpayerPDNationality));
            }
        }

        private TaxpayerNationalityLandx50 _selectedCitizen;
        public TaxpayerNationalityLandx50 SelectedCitizen
        {
            get => _selectedCitizen;
            set
            {
                if (_selectedCitizen == value) return;

                _selectedCitizen = value;
                OnPropertyChanged(nameof(SelectedCitizen));
            }
        }

        private TaxpayerNationalityLandx50 _selectedResidence;
        public TaxpayerNationalityLandx50 SelectedResidence
        {
            get => _selectedResidence;
            set
            {
                if (_selectedResidence == value) return;

                _selectedResidence = value;
                OnPropertyChanged(nameof(SelectedResidence));
            }
        }

        #endregion

        #region Passport Details Tab Variables

        private string _passportNumber;
        public string PassportNumber
        {
            get => _passportNumber;
            set
            {
                if (_passportNumber == value) return;

                _passportNumber = value;
                OnPropertyChanged(nameof(PassportNumber));
            }
        }

        private TaxpayerNationality _selectedPassportIssueCountry ;
        public TaxpayerNationality SelectedPassportIssueCountry
        {
            get => _selectedPassportIssueCountry;
            set
            {
                if (_selectedPassportIssueCountry == value) return;

                _selectedPassportIssueCountry = value;
                OnPropertyChanged(nameof(SelectedPassportIssueCountry));
            }
        }

        private ObservableCollection<object> _selectedPassportIssueDate;
        public ObservableCollection<object> SelectedPassportIssueDate
        {
            get
            {
                return _selectedPassportIssueDate;
            }
            set
            {
                if (_selectedPassportIssueDate == value) return;

                _selectedPassportIssueDate = value;
                OnPropertyChanged(nameof(SelectedPassportIssueDate));
            }
        }
        private ObservableCollection<object> _selectedPassportIssueHijiriDate;
        public ObservableCollection<object> SelectedPassportIssueHijiriDate
        {
            get
            {
                return _selectedPassportIssueHijiriDate;
            }
            set
            {
                if (_selectedPassportIssueDate == value) return;

                _selectedPassportIssueHijiriDate = value;
                OnPropertyChanged(nameof(SelectedPassportIssueHijiriDate));
            }
        }
        private string _passportIssueDate;
        public string PassportIssueDate
        {
            get => _passportIssueDate;
            set
            {
                if (_passportIssueDate == value) return;

                _passportIssueDate = value;
                OnPropertyChanged(nameof(PassportIssueDate));
            }
        }
        private string _displayPassportIssueDate;
        public string DisplayPassportIssueDate
        {
            get => _displayPassportIssueDate;
            set
            {
                if (_displayPassportIssueDate == value) return;

                _displayPassportIssueDate = value;
                OnPropertyChanged(nameof(DisplayPassportIssueDate));
            }
        }
        private ObservableCollection<object> _selectedPassportExpireDate;
        public ObservableCollection<object> SelectedPassportExpireDate
        {
            get
            {
                return _selectedPassportExpireDate;
            }
            set
            {
                if (_selectedPassportExpireDate == value) return;

                _selectedPassportExpireDate = value;
                OnPropertyChanged(nameof(SelectedPassportExpireDate));
            }
        }
        private ObservableCollection<object> _selectedPassportExpireHijiriDate;
        public ObservableCollection<object> SelectedPassportExpireHijiriDate
        {
            get
            {
                return _selectedPassportExpireHijiriDate;
            }
            set
            {
                if (_selectedPassportExpireHijiriDate == value) return;

                _selectedPassportExpireHijiriDate = value;
                OnPropertyChanged(nameof(SelectedPassportExpireHijiriDate));
            }
        }
        private string _passportExpireDate;
        public string PassportExpireDate
        {
            get => _passportExpireDate;
            set
            {
                if (_passportExpireDate == value) return;

                _passportExpireDate = value;
                OnPropertyChanged(nameof(PassportExpireDate));
            }
        }
        private string _displayPassportExpireDate;
        public string DisplayPassportExpireDate
        {
            get => _displayPassportExpireDate;
            set
            {
                if (_displayPassportExpireDate == value) return;

                _displayPassportExpireDate = value;
                OnPropertyChanged(nameof(DisplayPassportExpireDate));
            }
        }
        private ObservableCollection<Attachment> _uploadedPassportDocumentsList = new ObservableCollection<Attachment>();
        public ObservableCollection<Attachment> UploadedPassportDocumentsList
        {
            get
            {
                return _uploadedPassportDocumentsList;
            }
            set
            {
                if (_uploadedPassportDocumentsList == value) return;

                _uploadedPassportDocumentsList = value;

                if (UploadedPassportDocumentsList.Count > 0)
                {
                    IsVisbleAttachmentPassportList = true;
                }
                OnPropertyChanged(nameof(UploadedPassportDocumentsList));
            }
        }

        private bool _isVisbleAttachmentPassportList = false;
        public bool IsVisbleAttachmentPassportList
        {
            get
            {
                return _isVisbleAttachmentPassportList;
            }
            set
            {
                if (_isVisbleAttachmentPassportList == value) return;

                _isVisbleAttachmentPassportList = value;
                OnPropertyChanged("IsVisbleAttachmentPassportList");
            }
        }

        private string _passportFileName;
        public string SelectedPassportFileName
        {
            get => _passportFileName;
            set
            {
                if (_passportFileName == value) return;

                _passportFileName = value;
                OnPropertyChanged(nameof(SelectedPassportFileName));
            }
        }
        #endregion

        #region Outlet variables

        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText == value) return;

                _searchText = value == null ? string.Empty : value;
                SearchableOutletData?.Clear();
                OutletData.Where(i => i.Actnm.StartsWith(_searchText)).ToList().ForEach(j =>
                {
                    SearchableOutletData.Add(j);
                });
                OnPropertyChanged(SearchText);
            }
        }
        private ObservableCollection<OutletItem> _outletData = new ObservableCollection<OutletItem>();
        public ObservableCollection<OutletItem> OutletData
        {
            get => _outletData;
            set
            {
                if (_outletData == value) return;

                if (value != null && value.Count > 0)
                {
                    _outletData = value;
                    OnPropertyChanged(nameof(OutletData));
                }
            }
        }
        private ObservableCollection<OutletItem> _searchableOutletData = new ObservableCollection<OutletItem>();
        public ObservableCollection<OutletItem> SearchableOutletData
        {
            get => _searchableOutletData;
            private set
            {
                if (_searchableOutletData == value) return;

                if (value != null && value.Count > 0)
                {
                    _searchableOutletData = value;
                    OnPropertyChanged(nameof(SearchableOutletData));
                }
            }
        }

        private ObservableCollection<OuteltInfo_NestedListView> _outlettUiList = new ObservableCollection<OuteltInfo_NestedListView>();
        public ObservableCollection<OuteltInfo_NestedListView> OutlettUiList
        {
            get
            {
                return _outlettUiList;
            }
            set
            {
                if (_outlettUiList == value) return;

                _outlettUiList = value;

                OnPropertyChanged("OutlettUiList");
            }
        }

        #endregion

        #region Financial Details Tabs variables
        private List<string> dates = new List<string> { AppResources.ESTFinLastDay, "30", "29", "28", "27", "26", "25", "24", "23", "22", "21", "20", "19", "18", "17", "16", "15", "14", "13", "12", "11", "10", "09", "08", "07", "06", "05", "04", "03", "02", "01" };

        private Dictionary<string, string> EnMethodList = new Dictionary<string, string>()
        {
            {"Accounting Method", AppResources.NDAccounting },
            //{"Estimated", AppResources.NDEstimated }
            {"Estimated Method", AppResources.NDEstimated }
        };
        private Dictionary<string, string> EnCalendarTypeList = new Dictionary<string, string>()
        {
            {"Hijri", AppResources.Hijri },
            {"Gregorian", AppResources.Gregorian }
        };
        private List<string> _methodList = new List<string>();
        public List<string> MethodList
        {
            get => _methodList;
            set
            {
                if (_methodList == value) return;

                if (value != null)
                {
                    _methodList = value;
                    OnPropertyChanged(nameof(MethodList));
                }
            }
        }
        private string _selectedMethod = null;
        public string SelectedMethod
        {
            get => _selectedMethod;
            set
            {
                if (_selectedMethod == value) return;

                if (value != null)
                {
                    _selectedMethod = value;
                    OnPropertyChanged(nameof(SelectedMethod));
                }
            }
        }

        private string _finSelectedMethod = null;
        public string FinSelectedMethod
        {
            get => _finSelectedMethod;
            set
            {
                if (_finSelectedMethod == value) return;

                if (value != null)
                {
                    _finSelectedMethod = value;
                    OnPropertyChanged(nameof(FinSelectedMethod));
                }
            }
        }

        private List<string> _calendarTypeList = new List<string>();
        public List<string> CalendarTypeList
        {
            get => _calendarTypeList;
            set
            {
                if (_calendarTypeList == value) return;

                if (value != null)
                {
                    _calendarTypeList = value;
                    OnPropertyChanged(nameof(CalendarTypeList));
                }
            }
        }
        private string _calendarType = null;
        public string CalendarType
        {
            get => _calendarType;
            set
            {
                if (_calendarType == value) return;

                if (value != null)
                {
                    _calendarType = value;
                    OnPropertyChanged(nameof(CalendarType));
                    if (taxPayerDetails != null)
                        udpdateDates();
                }
            }
        }

        private List<PeriodSetResult> _periodList = new List<PeriodSetResult>();
        public List<PeriodSetResult> PeriodList
        {
            get => _periodList;
            set
            {
                if (_periodList == value) return;

                if (value != null)
                {
                    _periodList = value;
                    OnPropertyChanged(nameof(PeriodList));
                }
            }
        }
        private PeriodSetResult _selectedPeriod = null;
        public PeriodSetResult SelectedPeriod
        {
            get => _selectedPeriod;
            set
            {
                if (_selectedPeriod == value) return;

                if (value != null)
                {
                    _selectedPeriod = value;
                    OnPropertyChanged(nameof(SelectedPeriod));
                    //if (taxPayerDetails != null)
                    //    udpdateDates();
                }
            }
        }
        private string _fiscalMonth = string.Empty;
        public string FiscalMonth
        {
            get => _fiscalMonth;
            set
            {
                if (_fiscalMonth == value) return;

                _fiscalMonth = value;
                OnPropertyChanged(nameof(FiscalMonth));
            }
        }
        private string _fiscalDay = string.Empty;
        public string FiscalDay
        {
            get => _fiscalDay;
            set
            {
                if (_fiscalDay == value) return;

                _fiscalDay = value;
                OnPropertyChanged(nameof(FiscalDay));
            }
        }
        private string _commDate = string.Empty;
        public string CommDate
        {
            get => _commDate;
            set
            {
                if (_commDate == value) return;

                _commDate = value;
                OnPropertyChanged(nameof(CommDate));
            }
        }
        private string _taxDate = string.Empty;
        public string TaxDate
        {
            get => _taxDate;
            set
            {
                if (_taxDate == value) return;

                _taxDate = value;
                OnPropertyChanged(nameof(TaxDate));
            }
        }

        private string _lastFulfilledReturn = string.Empty;
        public string LastFulfilledReturn
        {
            get => _lastFulfilledReturn;
            set
            {
                if (_lastFulfilledReturn == value) return;

                _lastFulfilledReturn = value;
                OnPropertyChanged(nameof(LastFulfilledReturn));
            }
        }
        private string _zYear = string.Empty;
        public string ZYear
        {
            get => _zYear;
            set
            {
                if (_zYear == value) return;

                _zYear = value;
                OnPropertyChanged(nameof(ZYear));
            }
        }
        #endregion

        #region Summary Tabs variables
        private EstablishmentRegistrationTabsEnum _summaryExpendedCard = EstablishmentRegistrationTabsEnum.RegistrationType;
        public EstablishmentRegistrationTabsEnum SummaryExpendedCard
        {
            get => _summaryExpendedCard;
            set
            {
                if (_summaryExpendedCard == value) return;

                _summaryExpendedCard = value;
                OnPropertyChanged(nameof(SummaryExpendedCard));
            }
        }

        private bool _eSTLedge = false;
        public bool ESTLedge
        {
            get => _eSTLedge;
            set
            {
                if (_eSTLedge ==  value) return;

                _eSTLedge = value;
                OnPropertyChanged(nameof(ESTLedge));
            }
        }

        private OuteltInfo_NestedListView selectedItem;
        public OuteltInfo_NestedListView SelectedItem
        {
            get => selectedItem;
            set
            {
                if (selectedItem == value) return;

                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }


        #endregion

        #endregion

       

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
        #region Commands


        public Command OnNextButtonClick { get; set; }
        public ICommand OnPreButtonClick { get; set; }
        public ICommand OnVoidOrSaveDraftClick { get; set; }


        #region Registration Tab commands



        #region NationalityStatus option commands
        public ICommand NationalityStatusRentOwnHouseClick { get; set; }
        public ICommand NationalityStatusStayMoreThanKSAClick { get; set; }
        public ICommand NationalityStatusNoneOfTheAboveClick { get; set; }

        #endregion

        #region Legal Entity commands
        public ICommand PermanentEstablishmentLegalEntityClick { get; set; }

        public ICommand OtherTaxableIncomeLegalEntityClick { get; set; }

        #endregion

        #region Permanent Establishment options Commmands
        public ICommand ABranchOfNonResidentCompanyPEClick { get; set; }
        public ICommand ConstructionSitePEClick { get; set; }
        public ICommand InstallationPEClick { get; set; }
        public ICommand AFixedBasePEClick { get; set; }
        public ICommand NonResidentPartnerPEClick { get; set; }
        #endregion

        public ICommand OnEstablishmentRegistrationAttachmentTapped { get; set; }

        public ICommand OnReportingBranchSelectButtonClick { get; set; }

        #endregion

        #region TaxPayer Tab commands

        public ICommand OnPDNatinalitySelectButtonClick { get; set; }

        public ICommand OnPDCitizenSelectButtonClick { get; set; }

        public ICommand OnPDResidenceSelectButtonClick { get; set; }

        #endregion

        #region Passport Tab Commands

        public ICommand OnPassportIssueCountryButtonClick { get; set; }

        public ICommand OnPassportAttachmentTapped { get; set; }

        public ICommand TappedOnAttachmentInformationIcon { get; set; }

        public ICommand OnPassportCloseTapped { get; set; }

        public ICommand OnDeleteAttachmentClickedTapped { get; set; }
        #endregion


        #region Outlet Tabs commands
        public ICommand OnNewOutletButtonClick { get; set; }
        public ICommand OnEditOutletButtonClick { get; set; }
        public ICommand OnEditOutletButtonClick2 { get; set; }
        public ICommand OuterListTapCommand { get; set; }
        public ICommand OnDeleteOutletButtonClick { get; set; }
        #endregion


        #region Financial Details Tabs commands
        public ICommand OnMonthSelectButtonClick { get; set; }
        public ICommand OnDaySelectButtonClick { get; set; }
        #endregion


        #region Summary Tabs commands
        public ICommand OnExpendGridViewClick { get; set; }
        public ICommand OnEditPageViewClick { get; set; }
        #endregion

        #endregion

        #region Constructor
        public EstablishmentRegistrationPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            OnNextButtonClick = new Command(async() => await navigateToNext(), () => CanExecute);
            OnPreButtonClick = new Command(() =>
            {
                currentTab = EstablishmentRegistrationTabsEnum.Unknown;
                _navigationService.GoBack();
            });

            #region Registration Tab Variable initialization

            NationalityStatusStayMoreThanKSAClick = new Command(() => OrgResidenceSelection(OrgResidenceNationalityEstablishmentRegistrationEnum.StayMoreThanKSA));
            NationalityStatusRentOwnHouseClick = new Command(() => OrgResidenceSelection(OrgResidenceNationalityEstablishmentRegistrationEnum.RentOwnhouseMoreThanThirtyDays));
            NationalityStatusNoneOfTheAboveClick = new Command(() => OrgResidenceSelection(OrgResidenceNationalityEstablishmentRegistrationEnum.NoneOfTheAbove));
            PermanentEstablishmentLegalEntityClick = new Command(() => OrgNonResidentSelection(OrgNonResidentEstablishmentRegistrationEnum.PermanentEstablishment));
            OtherTaxableIncomeLegalEntityClick = new Command(() => OrgNonResidentSelection(OrgNonResidentEstablishmentRegistrationEnum.OtherTaxIncomeFromSourceWithInTheSKA));

            #region Permanent Establishment options

            ABranchOfNonResidentCompanyPEClick = new Command(() => OrgNonResidentOptionsSelection(OrgNonResidentOptionsEstablishmentEnum.ABranchOfNonResidentCompanyPE));
            ConstructionSitePEClick = new Command(() => OrgNonResidentOptionsSelection(OrgNonResidentOptionsEstablishmentEnum.ConstructionSitePE));
            InstallationPEClick = new Command(() => OrgNonResidentOptionsSelection(OrgNonResidentOptionsEstablishmentEnum.InstallationPE));
            AFixedBasePEClick = new Command(() => OrgNonResidentOptionsSelection(OrgNonResidentOptionsEstablishmentEnum.AFixedBasePE));
            NonResidentPartnerPEClick = new Command(() => OrgNonResidentOptionsSelection(OrgNonResidentOptionsEstablishmentEnum.NonResidentPartnerPE));

            #endregion

            #region Attachment commands 
            OnEstablishmentRegistrationAttachmentTapped = new Command(async() => await OnRentAddAttachmentTapped());
            #endregion

            OnReportingBranchSelectButtonClick = new Command(() =>

            {
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(ReportingBranchList);
                poupWindow.OnItemSelect = (item) =>
                {
                    SelectedReportingBranch = (item as BranchesDropDownModel);
                };
                MopupService.Instance.PushAsync(poupWindow);
            });

            #endregion

            #region TaxPayer Variable initialization

            GetGenderList();

            OnPDNatinalitySelectButtonClick = new Command(() =>
            {
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(TaxpayerFullNationlityList);
                poupWindow.OnItemSelect = (item) => SelectedTaxpayerPDNationality = item as TaxpayerNationality;
                MopupService.Instance.PushAsync(poupWindow);
            });

            OnPDCitizenSelectButtonClick = new Command(() =>
            {
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(TaxpayerPDNationlityList);
                poupWindow.OnItemSelect = (item) => SelectedCitizen = item as TaxpayerNationalityLandx50;
                MopupService.Instance.PushAsync(poupWindow);
            });

            OnPDResidenceSelectButtonClick = new Command(() =>
            {
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(TaxpayerPDNationlityList);
                poupWindow.OnItemSelect = (item) => SelectedResidence = item as TaxpayerNationalityLandx50;
                MopupService.Instance.PushAsync(poupWindow);
            });
            #endregion

            #region Passport Variable Initialization

            OnPassportIssueCountryButtonClick = new Command(() =>
            {
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(TaxpayerFullNationlityList);
                poupWindow.OnItemSelect = (item) => SelectedPassportIssueCountry = item as TaxpayerNationality;
                MopupService.Instance.PushAsync(poupWindow);
            });

            OnPassportAttachmentTapped = new Command(async() =>await OnPassportAddAttachmentButtonTapped());
            #endregion

            TappedOnAttachmentInformationIcon = new Command(() =>
            ShowValidationPopup(
                AppResources.ESTAttachmentSizeNotfication
                + Environment.NewLine
                + AppResources.ZZChooseonlyfilewithextensionForZAKAT
                + Environment.NewLine + AppResources.ZMaximumnoof5attachmentscanbeuploaded
                ));

            #region Outlet Tabs variable initialization
            OnNewOutletButtonClick = new Command(() => openNewOutlet());
            OnEditOutletButtonClick = new Command(async(item) =>await openEditOutletAsync(item as OuteltInfo_NestedListView, 1)); //Expander
            OuterListTapCommand = new Command(async (item) => await openEditOutletAsync(item as OuteltInfo_NestedListView, 0));
            OnEditOutletButtonClick2 = new Command(async (item) => await openEditOutletAsync(item as OuteltInfo_NestedListView, 2)); // Edit

            OnDeleteOutletButtonClick = new Command(async (item) =>
            {
                var newItem = item as OuteltInfo_NestedListView;

                if (newItem.Oldmst.ToString().ToUpper().Equals("X"))
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(AppResources.ExistingOutletError, AppResources.Information);

                    });
                }
                else if (newItem.MciEntry.ToString().ToUpper().Equals("X"))
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(AppResources.DeleteError, AppResources.Information);

                    });
                }
                else if (newItem.ActNo.Equals("00000"))
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(AppResources.MainOutletError, AppResources.Information); // Main ol cannot be deleted 

                    });
                }
                else
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
                    var confirmPopup = new ZAKATOkCancelPopUpView(AppResources.ZZDeleteAttachmentConfirmationText + "   " + newItem.ActNm + QuestionMark)
                    {
                        CloseWhenBackgroundIsClicked = false
                    };
                    confirmPopup.OnSelect = (str) =>
                    {
                        if (str == "Yes")
                        {
                            MainThread.BeginInvokeOnMainThread(() => deleteOutlet(item as OutletItem));
                        }
                    };
                    await MopupService.Instance.PushAsync(confirmPopup);
                }
            });
            #endregion

            #region Financial Details Tabs variable initialization
            MethodList.Clear();
            MethodList.AddRange(EnMethodList.Values);


            CalendarTypeList.Clear();
            CalendarTypeList.AddRange(EnCalendarTypeList.Values);

            SelectedMethod = MethodList.FirstOrDefault();
            CalendarType = CalendarTypeList.LastOrDefault();

            OnMonthSelectButtonClick = new Command(() =>
            {
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(new List<string> { "12", "11", "10", "09", "08", "07", "06", "05", "04", "03", "02", "01" });
                poupWindow.OnItemSelect = async (item) =>
                {

                    if (!FiscalMonth.Equals(item))
                    {
                        FiscalDay = string.Empty;
                       await udpdateDates(FiscalDay);
                        IsFinancePeriodVisible = false;
                    }

                    FiscalMonth = item as string;
                };
                MopupService.Instance.PushAsync(poupWindow);
            });
            OnDaySelectButtonClick = new Command(() =>
            {
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(dates);
                poupWindow.OnItemSelect = async (item) =>
                {
                    FiscalDay = item as string;
                   await udpdateDates(FiscalDay);
                };
                MopupService.Instance.PushAsync(poupWindow);
            });
            #endregion

            #region Summary Tabs variable initialization
            OnExpendGridViewClick = new Command((_enum) => OnExpandCollapseGridViewClick(_enum));
            OnEditPageViewClick = new Command((_enum) =>
            {
                currentTab = (EstablishmentRegistrationTabsEnum)_enum;
            });




            OnVoidOrSaveDraftClick = new Command(() =>
            {
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(new List<string> { AppResources.ZZSaveAsDraft, AppResources.ZZVoid, AppResources.FORM5CalendarType });
                poupWindow.OnItemSelect = async (item) =>
                {
                    var actionName = item as string;
                    if (actionName == AppResources.ZZSaveAsDraft)
                    {
                        IsLoading = true;
                        try
                        {
                            taxPayerDetails.Draftfg = "X";
                            taxPayerDetails.Gpart = App.LoginDataRetrieved.TIN;
                            taxPayerDetails.UserTypx = "TP";
                            var _taxPayerDetails = await EstablishmentRegistrationWebServiceManager.ESTTaxPayerDetailPostService(taxPayerDetails);
                            if (_taxPayerDetails != null && !string.IsNullOrEmpty(_taxPayerDetails.Fbnumx))
                            {
                                await MopupService.Instance.PushAsync(new SingleButtonPopupView(AppResources.ZZZOkayText, string.Format(AppResources.ZZZApplicationSaved, _taxPayerDetails.Fbnumx), string.Empty), true);

                            }
                        }
                        catch (Exception e)
                        {
                            if (e is HTTPBadRequestException)
                            {
                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(e.Message));
                            }
                        }
                        finally
                        {
                            IsLoading = false;
                        }
                    }
                    if (actionName == AppResources.ZZVoid)
                    {
                       MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            VoidNotePopPage voidNotePop = new VoidNotePopPage
                            {
                                OnVoidSelect = async (notes) =>
                                {
                                    IsLoading = true;
                                    try
                                    {
                                        OffNotes note = new OffNotes()
                                        {
                                            Tdline = notes,
                                            ByGpartz = App.LoginDataRetrieved.TIN
                                        };
                                        taxPayerDetails?.off_notesSet?.Clear();
                                        taxPayerDetails?.off_notesSet?.Add(note);
                                        taxPayerDetails.Operationx = "04";
                                        taxPayerDetails.Gpart = App.LoginDataRetrieved.TIN;
                                        taxPayerDetails.UserTypx = "TP";
                                        taxPayerDetails.StepNumberx = string.Empty;
                                        var _taxPayerDetails = await EstablishmentRegistrationWebServiceManager.ESTTaxPayerDetailPostService(taxPayerDetails);
                                        currentTab = EstablishmentRegistrationTabsEnum.Unknown;
                                        navigationService.NavigateTo(App.GAZTNewDesignDashBoardPageView);
                                    }
                                    catch (Exception e)
                                    {
                                        if (e is HTTPBadRequestException)
                                        {
                                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(e.Message));
                                        }
                                    }
                                    finally
                                    {
                                        IsLoading = false;
                                    }
                                }
                            };
                            await MopupService.Instance.PushAsync(voidNotePop);
                        });
                    }
                    if (actionName == AppResources.FORM5CalendarType)
                    {
                       MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            ListPopUpViewPage cal = new ListPopUpViewPage(new List<string> { AppResources.NDGregorian, AppResources.NDHijri });
                            cal.OnItemSelect = (_cal) =>
                            {
                                if (_cal as string == AppResources.NDGregorian)
                                {
                                    taxPayerDetails.Caltp = "Gregorian";
                                }
                                else if (_cal as string == AppResources.NDHijri)
                                {
                                    taxPayerDetails.Caltp = "Hijri";
                                }
                                updateDatePickers(currentTab);
                            };
                            await MopupService.Instance.PushAsync(cal);
                        });
                    }
                };
                MopupService.Instance.PushAsync(poupWindow);
            });
            #endregion
        }

        #endregion

        #region Method
        public async Task OnAppearing()
        {
            try
            {
                TabList
           = new ObservableCollection<string>{ AppResources.ESTRegTaxTabTitleLabel, AppResources.ESTTaxpayerPersonalDetailsTabTitleLabel,
                AppResources.ESTPassportDetailsTabTitleLabel, AppResources.ESTOutletsTabTitleLabel,
                AppResources. VATRFinancialDetails, AppResources.ZVatSummary };
                if (currentTab == EstablishmentRegistrationTabsEnum.Outlets)
                {
                  await  fetchTabDataAndBind(currentTab);
                }
            }
            catch (Exception )
            {

            }
           
        }

        private async Task navigateToNext()
        {
            CanExecute = false;
            try
            {
                var failedMesage = AppResources.Somethingwentwrong;
                if (currentTab == EstablishmentRegistrationTabsEnum.TaxpayerDetail)
                {
                    if (await FormValidation(currentTab))
                    {
                        if (await PushDatatoServer(currentTab))
                        {
                            currentTab = IsSaudi ? EstablishmentRegistrationTabsEnum.Outlets : EstablishmentRegistrationTabsEnum.PassportDetails;
                        }
                    }
                }
                else if (currentTab == EstablishmentRegistrationTabsEnum.PassportDetails)
                {
                    if (await FormValidation(currentTab))
                    {
                        if (await PushDatatoServer(currentTab))
                        {
                            currentTab = EstablishmentRegistrationTabsEnum.Outlets;
                        }
                    }
                }
                else if (currentTab == EstablishmentRegistrationTabsEnum.Outlets)
                {
                    if (await FormValidation(currentTab))
                    {
                        currentTab = EstablishmentRegistrationTabsEnum.FinancialDetail;
                    }
                }
                else if (currentTab == EstablishmentRegistrationTabsEnum.FinancialDetail)
                {

                    if (await FormValidation(currentTab))
                    {
                        if (await PushDatatoServer(currentTab))
                        {
                            currentTab = EstablishmentRegistrationTabsEnum.Declaration;
                        }
                    }

                }
                else if (currentTab == EstablishmentRegistrationTabsEnum.RegistrationType)
                {
                    if (await FormValidation(currentTab))
                    {
                        if (await PushDatatoServer(currentTab))
                        {
                            currentTab = EstablishmentRegistrationTabsEnum.TaxpayerDetail;
                        }
                    }
                }
                else if (currentTab == EstablishmentRegistrationTabsEnum.Declaration)
                {
                    if (await FormValidation(currentTab))
                    {
                        if (await PushDatatoServer(currentTab))
                        {
                            _navigationService.NavigateTo(App.RegistrationSuccessfulPage, taxPayerDetails);
                        }
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
        private void navigateToPre()
        {
            if (currentTab == EstablishmentRegistrationTabsEnum.TaxpayerDetail)
            {
                currentTab = EstablishmentRegistrationTabsEnum.RegistrationType;
            }
            else if (currentTab == EstablishmentRegistrationTabsEnum.PassportDetails)
            {
                currentTab = EstablishmentRegistrationTabsEnum.TaxpayerDetail;
            }
            else if (currentTab == EstablishmentRegistrationTabsEnum.Outlets)
            {
                currentTab = IsSaudi ? EstablishmentRegistrationTabsEnum.TaxpayerDetail : EstablishmentRegistrationTabsEnum.PassportDetails;
            }
            else if (currentTab == EstablishmentRegistrationTabsEnum.FinancialDetail)
            {
                currentTab = EstablishmentRegistrationTabsEnum.Outlets;
            }
            else if (currentTab == EstablishmentRegistrationTabsEnum.Declaration)
            {
                currentTab = EstablishmentRegistrationTabsEnum.FinancialDetail;
            }
            else if (currentTab == EstablishmentRegistrationTabsEnum.RegistrationType)
            {
                currentTab = EstablishmentRegistrationTabsEnum.Unknown;
                _navigationService.GoBack();
            }

        }


        private void ShowValidationPopup(string _message)
        {
            MopupService.Instance.PushAsync(new AttachmentInformationPopUp(_message));
        }

        private void OrgResidenceSelection(OrgResidenceNationalityEstablishmentRegistrationEnum selectedOption)
        {
            switch (selectedOption)
            {
                case OrgResidenceNationalityEstablishmentRegistrationEnum.StayMoreThanKSA:
                    {
                        IsClickedStayMoreThanKSAOption = true;
                        IsClickedOwnRentOption = false;
                        IsClickedNoneOfTheAboveOption = false;
                        SelectedTpresidence = "1";  // SelectedNationalityStatus = "Stay More than or equal to 183 days in KSA";

                        //Options clear or done false
                        IsClickedPermanentLegalEntity = false;
                        IsClickedOtherTaxableIncomeLegalEntity = false;

                        //Sub - Options clear or done false
                        IsClickedABranchOfNonResidentCompanyPE = false;
                        IsClickedConstructionSitePE = false;
                        IsClickedInstallationPE = false;
                        IsClickedAFixedBasePE = false;
                        IsClickedNonResidentPartnerPE = false;
                        // UploadedRentDocumentsList.Clear();
                    }
                    break;
                case OrgResidenceNationalityEstablishmentRegistrationEnum.RentOwnhouseMoreThanThirtyDays:

                    IsClickedStayMoreThanKSAOption = false;
                    IsClickedOwnRentOption = true;
                    IsClickedNoneOfTheAboveOption = false;
                    SelectedTpresidence = "2";// SelectedNationalityStatus = "Rent/Own a house more than 30 days";


                    //Options clear or done false
                    IsClickedPermanentLegalEntity = false;
                    IsClickedOtherTaxableIncomeLegalEntity = false;


                    //Sub - Options clear or done false
                    IsClickedABranchOfNonResidentCompanyPE = false;
                    IsClickedConstructionSitePE = false;
                    IsClickedInstallationPE = false;
                    IsClickedAFixedBasePE = false;
                    IsClickedNonResidentPartnerPE = false;

                    break;
                case OrgResidenceNationalityEstablishmentRegistrationEnum.NoneOfTheAbove:
                    IsClickedStayMoreThanKSAOption = false;
                    IsClickedOwnRentOption = false;
                    IsClickedNoneOfTheAboveOption = true;
                    SelectedTpresidence = "3";// SelectedNationalityStatus = "None of the Above";


                    //Options clear or done false
                    IsClickedPermanentLegalEntity = false;
                    IsClickedOtherTaxableIncomeLegalEntity = false;


                    //Sub - Options clear or done false
                    IsClickedABranchOfNonResidentCompanyPE = false;
                    IsClickedConstructionSitePE = false;
                    IsClickedInstallationPE = false;
                    IsClickedAFixedBasePE = false;
                    IsClickedNonResidentPartnerPE = false;
                    // UploadedRentDocumentsList.Clear();

                    break;
                default:
                    IsClickedStayMoreThanKSAOption = false;
                    IsClickedOwnRentOption = false;
                    IsClickedNoneOfTheAboveOption = false;
                    SelectedTpresidence = string.Empty; // SelectedNationalityStatus = "";
                    //Options clear or done false
                    IsClickedPermanentLegalEntity = false;
                    IsClickedOtherTaxableIncomeLegalEntity = false;


                    //Sub - Options clear or done false
                    IsClickedABranchOfNonResidentCompanyPE = false;
                    IsClickedConstructionSitePE = false;
                    IsClickedInstallationPE = false;
                    IsClickedAFixedBasePE = false;
                    IsClickedNonResidentPartnerPE = false;
                    UploadedRentDocumentsList.Clear();
                    break;
            }
        }

        private void OrgNonResidentSelection(OrgNonResidentEstablishmentRegistrationEnum selectedOption)
        {
            switch (selectedOption)
            {
                case OrgNonResidentEstablishmentRegistrationEnum.PermanentEstablishment:

                    IsClickedPermanentLegalEntity = true;
                    IsClickedOtherTaxableIncomeLegalEntity = false;
                    SelectedOrgNonResident = "1";// SelectedLegalEntity = "Permanent Establishment";
                    //Options clear or done false
                    IsClickedABranchOfNonResidentCompanyPE = false;
                    IsClickedConstructionSitePE = false;
                    IsClickedInstallationPE = false;
                    IsClickedAFixedBasePE = false;
                    IsClickedNonResidentPartnerPE = false;
                    SelectedOrgNonResidentActivityItem = string.Empty;
                    break;
                case OrgNonResidentEstablishmentRegistrationEnum.OtherTaxIncomeFromSourceWithInTheSKA:

                    IsClickedPermanentLegalEntity = false;
                    IsClickedOtherTaxableIncomeLegalEntity = true;
                    SelectedOrgNonResident = "2";// SelectedLegalEntity = "Other Taxable Income from source with in the KSA";


                    //Options clear or done false
                    IsClickedABranchOfNonResidentCompanyPE = false;
                    IsClickedConstructionSitePE = false;
                    IsClickedInstallationPE = false;
                    IsClickedAFixedBasePE = false;
                    IsClickedNonResidentPartnerPE = false;

                    break;
                default:
                    //Options clear or done false
                    IsClickedPermanentLegalEntity = false;
                    IsClickedOtherTaxableIncomeLegalEntity = false;
                    SelectedOrgNonResident = string.Empty; //SelectedLegalEntity = "";

                    IsClickedABranchOfNonResidentCompanyPE = false;
                    IsClickedConstructionSitePE = false;
                    IsClickedInstallationPE = false;
                    IsClickedAFixedBasePE = false;
                    IsClickedNonResidentPartnerPE = false;
                    break;
            }
        }

        private void OrgNonResidentOptionsSelection(OrgNonResidentOptionsEstablishmentEnum selectedOption)
        {
            switch (selectedOption)
            {
                case OrgNonResidentOptionsEstablishmentEnum.ABranchOfNonResidentCompanyPE:
                    IsClickedABranchOfNonResidentCompanyPE = true;
                    IsClickedConstructionSitePE = false;
                    IsClickedInstallationPE = false;
                    IsClickedAFixedBasePE = false;
                    IsClickedNonResidentPartnerPE = false;
                    SelectedOrgNonResidentOptions = "1";
                    break;
                case OrgNonResidentOptionsEstablishmentEnum.ConstructionSitePE:
                    IsClickedABranchOfNonResidentCompanyPE = false;
                    IsClickedConstructionSitePE = true;
                    IsClickedInstallationPE = false;
                    IsClickedAFixedBasePE = false;
                    IsClickedNonResidentPartnerPE = false;
                    SelectedOrgNonResidentOptions = "2";
                    break;
                case OrgNonResidentOptionsEstablishmentEnum.InstallationPE:
                    IsClickedABranchOfNonResidentCompanyPE = false;
                    IsClickedConstructionSitePE = false;
                    IsClickedInstallationPE = true;
                    IsClickedAFixedBasePE = false;
                    IsClickedNonResidentPartnerPE = false;
                    SelectedOrgNonResidentOptions = "3";
                    break;
                case OrgNonResidentOptionsEstablishmentEnum.AFixedBasePE:
                    IsClickedABranchOfNonResidentCompanyPE = false;
                    IsClickedConstructionSitePE = false;
                    IsClickedInstallationPE = false;
                    IsClickedAFixedBasePE = true;
                    IsClickedNonResidentPartnerPE = false;
                    SelectedOrgNonResidentOptions = "4";
                    break;
                case OrgNonResidentOptionsEstablishmentEnum.NonResidentPartnerPE:
                    IsClickedABranchOfNonResidentCompanyPE = false;
                    IsClickedConstructionSitePE = false;
                    IsClickedInstallationPE = false;
                    IsClickedAFixedBasePE = false;
                    IsClickedNonResidentPartnerPE = true;
                    SelectedOrgNonResidentOptions = "5";
                    break;
                default:
                    //Options clear or done false
                    IsClickedABranchOfNonResidentCompanyPE = false;
                    IsClickedConstructionSitePE = false;
                    IsClickedInstallationPE = false;
                    IsClickedAFixedBasePE = false;
                    IsClickedNonResidentPartnerPE = false;
                    SelectedOrgNonResidentOptions = string.Empty;
                    break;

            }
        }



        private void GetGenderList()
        {
            GenderList.Clear();
            GenderList.Add(AppResources.ESTMaleLabel);
            GenderList.Add(AppResources.ESTFemaleLabel);


        }


        private async Task GetReportingBranchListFromServer()
        {
            ReportingBranchList = await EstablishmentRegistrationWebServiceManager.ESTBranchesDropDown();
        }


        private async Task GetPdNationalityListFromServer(string nationality)
        {
            TaxpayerFullNationlityList = await EstablishmentRegistrationWebServiceManager.ESTTaxPayerNationality(nationality);
            TaxpayerPDNationlityList = new List<TaxpayerNationalityLandx50>();
            TaxpayerFullNationlityList.ForEach(i => TaxpayerPDNationlityList.Add((TaxpayerNationalityLandx50)i));
        }

        public void OnRentAttachmentDeleteButtonTapped(Attachment obj)
        {


            var delStatus = DeleteAttachment(obj.Filename, obj.RetGuid, obj.Dotyp, obj.Doguid);

            if (delStatus.ToLower() == "delete")
            {
                UploadedRentDocumentsList.Remove(obj);
            }
            else
            {
                ShowValidationPopup(AppResources.Somethingwentwrong);
            }
            if (UploadedRentDocumentsList == null || UploadedRentDocumentsList.Count() == 0)
            {
                IsVisbleRentAttachmentmentList = false;

            }

        }

        public async Task OnRentAddAttachmentTapped()
        {
            if (UploadedRentDocumentsList.Count() < 5)
            {
                await AddAttachment("RG16");
                if (UploadedRentDocumentsList.Count > 0)
                {
                    IsVisbleRentAttachmentmentList = true;
                }
            }
            else
            {
                ShowValidationPopup(AppResources.ZMaximumnoof5attachmentscanbeuploaded);
            }

        }

        public void OnPassportAttachmentDeleteButtonTapped(Attachment obj)
        {
            var delStatus = DeleteAttachment(obj.Filename, obj.RetGuid, obj.Dotyp, obj.Doguid);

            if (delStatus.ToLower() == "delete")
            {
                UploadedPassportDocumentsList.Remove(obj);
            }
            else
            {
                ShowValidationPopup(AppResources.Somethingwentwrong);
            }

            if (UploadedPassportDocumentsList == null || UploadedPassportDocumentsList.Count() == 0)
            {
                IsVisbleAttachmentPassportList = false;

            }
        }

        public async Task OnPassportAddAttachmentButtonTapped()
        {
            if (UploadedPassportDocumentsList.Count() < 5)
            {
                await AddAttachment("RG19");

                if (UploadedPassportDocumentsList.Count > 0)
                {
                    IsVisbleAttachmentPassportList = true;
                }
            }
            else
            {
                ShowValidationPopup(AppResources.ZMaximumnoof5attachmentscanbeuploaded);
            }
        }

        private void SelectOrgNonResidentActivity(string selectedOrgNonResidentActivityValue)
        {
            if (selectedOrgNonResidentActivityValue == AppResources.ESTOrgNonResidentActivityValueOne)
            {
                SelectedOrgNonResidentActivity = "O1";
            }
            else if (selectedOrgNonResidentActivityValue == AppResources.ESTOrgNonResidentActivityValueTwo)
            {
                SelectedOrgNonResidentActivity = "O2";
            }
            else if (selectedOrgNonResidentActivityValue == AppResources.ESTOrgNonResidentActivityValueThree)
            {
                SelectedOrgNonResidentActivity = "O3";
            }
            else if (selectedOrgNonResidentActivityValue == AppResources.ESTOrgNonResidentActivityValueFour)
            {
                SelectedOrgNonResidentActivity = "O4";
            }
            else if (selectedOrgNonResidentActivityValue == AppResources.ESTOrgNonResidentActivityValueFive)
            {
                SelectedOrgNonResidentActivity = "O5";
            }
            else if (selectedOrgNonResidentActivityValue == AppResources.ESTOrgNonResidentActivityValueSix)
            {
                SelectedOrgNonResidentActivity = "O6";
            }
            else if (selectedOrgNonResidentActivityValue == AppResources.ESTOrgNonResidentActivityValueSeven)
            {
                SelectedOrgNonResidentActivity = "O7";
            }
            else if (selectedOrgNonResidentActivityValue == AppResources.ESTOrgNonResidentActivityValueEight)
            {
                SelectedOrgNonResidentActivity = "O8";
            }
            else if (selectedOrgNonResidentActivityValue == AppResources.ESTOrgNonResidentActivityValueNine)
            {
                SelectedOrgNonResidentActivity = "O9";
            }
        }

        private string DeleteAttachment(string fileName, string RetGuid, string docType, string docguid)
        {
            return EstablishmentRegistrationWebServiceManager.ESTDeleteAttachment(fileName, RetGuid, docType, docguid);
        }

        private async Task AddAttachment(string docType)
        {
            try
            {
                string[] filetypes = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetAttachmentTypeStringForTaxEvasion();

                PickOptions options = UtilityManager.GetFilePickerOptionsForChooser(filetypes);

                var fileData = await FilePicker.PickAsync(options);
                var stream = await fileData.OpenReadAsync();
                var attachmentByte = UtilityManager.ReadFully(stream as Stream);
                if (fileData != null)
                {

                    string base64String = Convert.ToBase64String(attachmentByte, 0, attachmentByte.Length);
                    var attachmentName = fileData.FileName;
                    bool isFileAlreayUploaded = IsFileAlreadyAttached(docType, attachmentName);
                    float sizemb = attachmentByte.Length / 1024f / 1024f;
                    decimal attachmentSize = 0;
                    attachmentSize = attachmentSize + (decimal)sizemb;
                    if (!isFileAlreayUploaded)
                    {
                        if (fileData.FileName.Contains("."))
                        {
                            // string Extention = fileData.FileName.Split('.')[1];//pdf
                            string[] ExtensionArray = fileData.FileName.Split('.');
                            string Extention = ExtensionArray.Last();
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
                       MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZFileWithTheSameNameAlreadyExists));
                            IsLoading = false;
                        });
                    }

                }

            }
            catch (Exception)
            { }
        }


        private async Task SaveAttachment(Stream attachmentByteData, string fileName, string docType, string contentType)
        {
            try
            {
                IsLoading = true;

                Attachment dd = await EstablishmentRegistrationWebServiceManager.ESTAttachment(attachmentByteData, fileName, taxPayerDetails?.ReturnIdx, docType, contentType, null);

                if (docType == "RG16")
                {
                    UploadedRentDocumentsList.Add(dd);
                }
                else if (docType == "RG19")
                {
                    UploadedPassportDocumentsList.Add(dd);
                }

            }
            catch (Exception)
            { }
            finally
            {
                IsLoading = false;
            }
        }


        private void OnExpandCollapseGridViewClick(object _enum)
        {
            System.Diagnostics.Debug.WriteLine(_enum);
            SummaryExpendedCard = (EstablishmentRegistrationTabsEnum)_enum;
        }

        public async Task fetchTabDataAndBind(EstablishmentRegistrationTabsEnum _enum)
        {
            clearFormData(_enum);
            try
            {
                IsLoading = true;
                if (_enum == EstablishmentRegistrationTabsEnum.RegistrationType)
                {
                    await GetReportingBranchListFromServer();
                    taxPayerDetails = await EstablishmentRegistrationWebServiceManager.ESTTaxPayerDetailGetService("01", App.LoginDataRetrieved.TIN, App.LoginDataRetrieved.Emailid);
                    if (!string.IsNullOrEmpty(taxPayerDetails?.Fbsta) && taxPayerDetails?.Fbsta != "IP011")
                    {
                        if (IsNavigationCompletedToSuccessfulPage == false)
                        {
                            IsNavigationCompletedToSuccessfulPage = true;
                           await _navigationService.NavigateTo(App.RegistrationSuccessfulPage, taxPayerDetails);
                        }
                    }
                    else
                    {
                        IsNavigationCompletedToSuccessfulPage = false;
                    }
                    SelectedReportingBranch = ReportingBranchList.Where(i => i.authorizationGroup == taxPayerDetails?.Augrp).FirstOrDefault();
                    SelectedEntityType = AppResources.ESTSelectedEntityTypeLabel;// Int16.Parse(taxPayerDetails?.Atype) == 1 ? "Individual" : "Company";
                    SelectedTaxPayerType = AppResources.ESTSelectedTaxPayerType;
                    if (taxPayerDetails?.Tpnationality == "SAUDI")
                        SelectedRegNationalityType = AppResources.ESTNationalitySAUDI;
                    else if (taxPayerDetails?.Tpnationality == "GCC")
                        SelectedRegNationalityType = AppResources.ESTNationalityGCC;
                    else if (taxPayerDetails?.Tpnationality == "FOREIGN")
                        SelectedRegNationalityType = AppResources.ESTNationalityFOREIGN;

                    if (!NationalityMapping.ContainsKey(taxPayerDetails?.Tpnationality) || ReportingBranchList?.Count == 0)
                    {
                        var someThingWhentWrong = new AttachmentInformationPopUp(AppResources.Somethingwentwrong)
                        {
                            CloseWhenBackgroundIsClicked = false
                        };
                        someThingWhentWrong.OnDone = () =>
                        {
                            currentTab = EstablishmentRegistrationTabsEnum.Unknown;
                            _navigationService.NavigateTo(App.GAZTNewDesignDashBoardPageView);
                        };
                        await MopupService.Instance.PushAsync(someThingWhentWrong);
                        return;
                    }
                    IsSaudi = taxPayerDetails?.Tpnationality == "SAUDI";
                    if (IsSaudi)
                    {
                        TabList.Remove(AppResources.ESTPassportDetailsTabTitleLabel);
                    }
                    else
                    {
                        ResidenceTypePrePopulateData(taxPayerDetails);
                        RentAttachmentPrePopulateCheck(taxPayerDetails);
                    }
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.TaxpayerDetail)
                {

                    await GetPdNationalityListFromServer(taxPayerDetails?.Tpnationality);
                    taxPayerDetails = await EstablishmentRegistrationWebServiceManager.ESTTaxPayerDetailGetService("02", App.LoginDataRetrieved.TIN, App.LoginDataRetrieved.Emailid);
                    idItem = taxPayerDetails?.Nreg_IdSet.Where(i => EnIDType.ContainsKey(i.Type)).FirstOrDefault();
                    if (idItem != null)
                    {
                        if (App.IsArabic)
                        {
                            GCCIDType = ArIDType[idItem?.Type];
                        }
                        else
                        {
                            GCCIDType = EnIDType[idItem?.Type];
                        }
                        if (idItem?.Type == "ZS0001")
                        {
                            TitleVisibility = true;
                        }
                        //TODO CR5784
                        Title = taxPayerDetails?.TpTitle;
                        GCCIDTypeIdNumberValue = idItem.Idnumber;
                    }
                    else
                    {
                        if (App.IsArabic)
                        {
                            GCCIDType = ArIDType["ZS0001"];
                        }
                        else
                        {
                            GCCIDType = EnIDType["ZS0001"];
                        }
                        TitleVisibility = true;
                        Title = taxPayerDetails?.TpTitle;
                    }

                    if ((bool)(idItem?.IqamaFg.Equals("X")))
                    {
                        ShowIqamaType = true;
                        IqamaDesc = idItem?.IqamaDesc;
                    }
                    else
                    {
                        ShowIqamaType = false;
                        IqamaDesc = "";
                    }

                    SelectedDOB = Convert.ToDateTime(taxPayerDetails?.Birthdt).ToString("yyyy/MM/dd");

                    FirstName = taxPayerDetails?.NameFirst;
                    LastName = taxPayerDetails?.NameLast?.Replace(".", string.Empty);
                    FatherName = taxPayerDetails?.FatherName;
                    GrandFatherName = taxPayerDetails?.GrandfatherName;
                    FamilyName = taxPayerDetails?.FamilyName;
                    Initial = taxPayerDetails?.Initials;
                    if (taxPayerDetails?.Xsexm == "X")
                        SelectedGender = GenderList.FirstOrDefault();
                    if (taxPayerDetails?.Xsexf == "X")
                        SelectedGender = GenderList.LastOrDefault();
                    if (string.IsNullOrEmpty(SelectedGender))
                    {
                        SelectedGender = GenderList.FirstOrDefault();
                    }
                    SelectedTaxpayerPDNationality = TaxpayerFullNationlityList.Where(i => i.Land1 == taxPayerDetails?.Natio).FirstOrDefault();
                    SelectedCitizen = TaxpayerFullNationlityList.Where(i => i.Land1 == taxPayerDetails?.Citizen).FirstOrDefault();
                    SelectedResidence = TaxpayerFullNationlityList.Where(i => i.Land1 == taxPayerDetails?.Residence).FirstOrDefault();
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.PassportDetails)
                {
                    taxPayerDetails = await EstablishmentRegistrationWebServiceManager.ESTTaxPayerDetailGetService("02", App.LoginDataRetrieved.TIN, App.LoginDataRetrieved.Emailid);
                    Nreg_IdItem passportItem = taxPayerDetails?.Nreg_IdSet.Where(i => i.Type == "FS0002").FirstOrDefault();
                    PassportNumber = passportItem?.Idnumber;
                    SelectedPassportIssueCountry = TaxpayerFullNationlityList.Where(i => i.Land1 == passportItem?.Country).FirstOrDefault();
                    PassportIssueDate = Convert.ToDateTime(passportItem?.ValidDateFrom).ToString("yyyy/MM/dd");
                    PassportExpireDate = Convert.ToDateTime(passportItem?.ValidDateTo).ToString("yyyy/MM/dd");
                    PassportAttachmentPrepopulateCheck(taxPayerDetails);
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.Outlets)
                {
                   await bindingOutletList();
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.FinancialDetail)
                {
                    taxPayerDetails = await EstablishmentRegistrationWebServiceManager.ESTTaxPayerDetailGetService("04", App.LoginDataRetrieved.TIN, App.LoginDataRetrieved.Emailid, null, taxPayerDetails?.Fbnumx);

                    if (!string.IsNullOrEmpty(taxPayerDetails?.Accmethod)) {

                        SelectedMethod = EnMethodList[taxPayerDetails?.Accmethod];

                    }

                    if (!string.IsNullOrEmpty(taxPayerDetails?.Fdcalender))
                    {
                        CalendarType = EnCalendarTypeList[taxPayerDetails?.Fdcalender];

                    }
                    else
                    {
                        CalendarType = AppResources.Gregorian;
                        calType = "Gregorian";
                    }

                    if (!string.IsNullOrEmpty(taxPayerDetails.Fdmonth))
                    {

                        FiscalMonth = taxPayerDetails.Fdmonth;
                    }

                    if (!string.IsNullOrEmpty(taxPayerDetails.Fdday))
                    {

                        if(taxPayerDetails.Fdday == "LD") {


                            FiscalDay = AppResources.ESTFinLastDay;
                        }
                        else {

                            FiscalDay = taxPayerDetails.Fdday;

                        }


                    }

                    IsFinancePeriodVisible = false;


                   await udpdateDates();
                }
                if (IsSaudi)
                {
                    TabList.Remove(AppResources.ESTPassportDetailsTabTitleLabel);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                IsLoading = false;
                updateDatePickers(_enum);
            }
        }

        public async Task<bool> FetchDataForDisplayDetailsExt(EstablishmentRegistrationTabsEnum _enum)
        {
            clearFormData(_enum);
            try
            {
                IsLoading = true;
                if (_enum == EstablishmentRegistrationTabsEnum.RegistrationType)
                {
                    await GetReportingBranchListFromServer();
                    taxPayerDetails = await EstablishmentRegistrationWebServiceManager.ESTTaxPayerDetailGetService("01", App.LoginDataRetrieved.TIN, App.LoginDataRetrieved.Emailid);
                    if (!string.IsNullOrEmpty(taxPayerDetails?.Fbsta) && taxPayerDetails?.Fbsta != "IP011")
                    {
                       await _navigationService.NavigateTo(App.RegistrationSuccessfulPage, taxPayerDetails);
                    }
                    SelectedReportingBranch = ReportingBranchList.Where(i => i.authorizationGroup == taxPayerDetails?.Augrp).FirstOrDefault();
                    SelectedEntityType = AppResources.ESTSelectedEntityTypeLabel;// Int16.Parse(taxPayerDetails?.Atype) == 1 ? "Individual" : "Company";
                    SelectedTaxPayerType = AppResources.ESTSelectedTaxPayerType;
                    if (taxPayerDetails?.Tpnationality == "SAUDI")
                        SelectedRegNationalityType = AppResources.ESTNationalitySAUDI;
                    else if (taxPayerDetails?.Tpnationality == "GCC")
                        SelectedRegNationalityType = AppResources.ESTNationalityGCC;
                    else if (taxPayerDetails?.Tpnationality == "FOREIGN")
                        SelectedRegNationalityType = AppResources.ESTNationalityFOREIGN;

                    if (!NationalityMapping.ContainsKey(taxPayerDetails?.Tpnationality) || ReportingBranchList?.Count == 0)
                    {
                        return false;
                    }
                    IsSaudi = taxPayerDetails?.Tpnationality == "SAUDI";
                    if (IsSaudi)
                    {
                        TabList.Remove(AppResources.ESTPassportDetailsTabTitleLabel);
                    }
                    else
                    {
                        ResidenceTypePrePopulateData(taxPayerDetails);
                        RentAttachmentPrePopulateCheck(taxPayerDetails);
                    }
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.TaxpayerDetail)
                {

                    await GetPdNationalityListFromServer(taxPayerDetails?.Tpnationality);
                    taxPayerDetails = await EstablishmentRegistrationWebServiceManager.ESTTaxPayerDetailGetService("02", App.LoginDataRetrieved.TIN, App.LoginDataRetrieved.Emailid);
                    idItem = taxPayerDetails?.Nreg_IdSet?.Where(i => EnIDType.ContainsKey(i.Type)).FirstOrDefault();
                    if (idItem != null)
                        if (App.IsArabic)
                        {
                            GCCIDType = ArIDType[idItem?.Type];
                        }
                        else
                        {
                            GCCIDType = EnIDType[idItem?.Type];
                        }
                    GCCIDTypeIdNumberValue = idItem?.Idnumber;

                    if (!string.IsNullOrEmpty(taxPayerDetails?.Birthdt))
                    {
                        SelectedDOB = Convert.ToDateTime(taxPayerDetails?.Birthdt).ToString("yyyy/MM/dd");
                    }
                    Title = taxPayerDetails?.TpTitle;
                    FirstName = taxPayerDetails?.NameFirst;
                    LastName = taxPayerDetails?.NameLast?.Replace(".", string.Empty);
                    FatherName = taxPayerDetails?.FatherName;
                    GrandFatherName = taxPayerDetails?.GrandfatherName;
                    FamilyName = taxPayerDetails?.FamilyName;
                    Initial = taxPayerDetails?.Initials;
                    if (taxPayerDetails?.Xsexm == "X")
                        SelectedGender = GenderList.FirstOrDefault();
                    if (taxPayerDetails?.Xsexf == "X")
                        SelectedGender = GenderList.LastOrDefault();
                    if (string.IsNullOrEmpty(SelectedGender))
                    {
                        SelectedGender = GenderList.FirstOrDefault();
                    }
                    SelectedTaxpayerPDNationality = TaxpayerFullNationlityList.Where(i => i.Land1 == taxPayerDetails?.Natio).FirstOrDefault();
                    SelectedCitizen = TaxpayerFullNationlityList.Where(i => i.Land1 == taxPayerDetails?.Citizen).FirstOrDefault();
                    SelectedResidence = TaxpayerFullNationlityList.Where(i => i.Land1 == taxPayerDetails?.Residence).FirstOrDefault();
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                IsLoading = false;
                updateDatePickers(_enum);
            }

            return true;
        }


        public async Task FetchDataForDisplayDetails(EstablishmentRegistrationTabsEnum _enum)
        {
            clearFormData(_enum);
            try
            {
                IsLoading = true;
                if (_enum == EstablishmentRegistrationTabsEnum.RegistrationType)
                {
                    await GetReportingBranchListFromServer();
                    taxPayerDetails = await EstablishmentRegistrationWebServiceManager.ESTTaxPayerDetailGetService("01", App.LoginDataRetrieved.TIN, App.LoginDataRetrieved.Emailid);
                    if (!string.IsNullOrEmpty(taxPayerDetails?.Fbsta) && taxPayerDetails?.Fbsta != "IP011")
                    {
                      await  _navigationService.NavigateTo(App.RegistrationSuccessfulPage, taxPayerDetails);
                    }
                    SelectedReportingBranch = ReportingBranchList.Where(i => i.authorizationGroup == taxPayerDetails?.Augrp).FirstOrDefault();
                    SelectedEntityType = AppResources.ESTSelectedEntityTypeLabel;// Int16.Parse(taxPayerDetails?.Atype) == 1 ? "Individual" : "Company";
                    SelectedTaxPayerType = AppResources.ESTSelectedTaxPayerType;
                    if (taxPayerDetails?.Tpnationality == "SAUDI")
                        SelectedRegNationalityType = AppResources.ESTNationalitySAUDI;
                    else if (taxPayerDetails?.Tpnationality == "GCC")
                        SelectedRegNationalityType = AppResources.ESTNationalityGCC;
                    else if (taxPayerDetails?.Tpnationality == "FOREIGN")
                        SelectedRegNationalityType = AppResources.ESTNationalityFOREIGN;

                    if (!NationalityMapping.ContainsKey(taxPayerDetails?.Tpnationality) || ReportingBranchList?.Count == 0)
                    {
                        var someThingWhentWrong = new AttachmentInformationPopUp(AppResources.SomethingwentwrongTaxDetails)
                        {
                            CloseWhenBackgroundIsClicked = false
                        };

                        someThingWhentWrong.OnDone = () =>
                        {

                            currentTab = EstablishmentRegistrationTabsEnum.Unknown;
                            IsLoading = false;
                            _navigationService.GoBack();


                            currentTab = EstablishmentRegistrationTabsEnum.Unknown;
                            _navigationService.NavigateTo(App.GAZTNewDesignDashBoardPageView);

                        };

                        await MopupService.Instance.PushAsync(someThingWhentWrong);
                        return;
                    }
                    IsSaudi = taxPayerDetails?.Tpnationality == "SAUDI";
                    if (IsSaudi)
                    {
                        TabList.Remove(AppResources.ESTPassportDetailsTabTitleLabel);
                    }
                    else
                    {
                        ResidenceTypePrePopulateData(taxPayerDetails);
                        RentAttachmentPrePopulateCheck(taxPayerDetails);
                    }
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.TaxpayerDetail)
                {

                    await GetPdNationalityListFromServer(taxPayerDetails?.Tpnationality);
                    taxPayerDetails = await EstablishmentRegistrationWebServiceManager.ESTTaxPayerDetailGetService("02", App.LoginDataRetrieved.TIN, App.LoginDataRetrieved.Emailid);
                    idItem = taxPayerDetails?.Nreg_IdSet?.Where(i => EnIDType.ContainsKey(i.Type)).FirstOrDefault();
                    if (idItem != null)
                        if (App.IsArabic)
                        {
                            GCCIDType = ArIDType[idItem?.Type];
                        }
                        else
                        {
                            GCCIDType = EnIDType[idItem?.Type];
                        }
                    GCCIDTypeIdNumberValue = idItem?.Idnumber;
                    SelectedDOB = taxPayerDetails?.Birthdt;   //?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                    Title = taxPayerDetails?.TpTitle;
                    FirstName = taxPayerDetails?.NameFirst;
                    LastName = taxPayerDetails?.NameLast?.Replace(".", string.Empty);
                    FatherName = taxPayerDetails?.FatherName;
                    GrandFatherName = taxPayerDetails?.GrandfatherName;
                    FamilyName = taxPayerDetails?.FamilyName;
                    Initial = taxPayerDetails?.Initials;
                    if (taxPayerDetails?.Xsexm == "X")
                        SelectedGender = GenderList.FirstOrDefault();
                    if (taxPayerDetails?.Xsexf == "X")
                        SelectedGender = GenderList.LastOrDefault();
                    if (string.IsNullOrEmpty(SelectedGender))
                    {
                        SelectedGender = GenderList.FirstOrDefault();
                    }
                    SelectedTaxpayerPDNationality = TaxpayerFullNationlityList.Where(i => i.Land1 == taxPayerDetails?.Natio).FirstOrDefault();
                    SelectedCitizen = TaxpayerFullNationlityList.Where(i => i.Land1 == taxPayerDetails?.Citizen).FirstOrDefault();
                    SelectedResidence = TaxpayerFullNationlityList.Where(i => i.Land1 == taxPayerDetails?.Residence).FirstOrDefault();
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.PassportDetails)
                {
                    taxPayerDetails = await EstablishmentRegistrationWebServiceManager.ESTTaxPayerDetailGetService("02", App.LoginDataRetrieved.TIN, App.LoginDataRetrieved.Emailid);
                    Nreg_IdItem passportItem = taxPayerDetails?.Nreg_IdSet.Where(i => i.Type == "FS0002").FirstOrDefault();
                    PassportNumber = passportItem?.Idnumber;
                    SelectedPassportIssueCountry = TaxpayerFullNationlityList.Where(i => i.Land1 == passportItem?.Country).FirstOrDefault();
                    PassportIssueDate = passportItem?.ValidDateFrom;   //?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                    PassportExpireDate = passportItem?.ValidDateTo;   //?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                    PassportAttachmentPrepopulateCheck(taxPayerDetails);
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.Outlets)
                {
                    await GetPdNationalityListFromServer(taxPayerDetails?.Tpnationality);
                    taxPayerDetails = await EstablishmentRegistrationWebServiceManager.ESTTaxPayerDetailGetService("02", App.LoginDataRetrieved.TIN, App.LoginDataRetrieved.Emailid);
                    idItem = taxPayerDetails?.Nreg_IdSet?.Where(i => EnIDType.ContainsKey(i.Type)).FirstOrDefault();
                    if (idItem != null)
                        if (App.IsArabic)
                        {
                            GCCIDType = ArIDType[idItem?.Type];
                        }
                        else
                        {
                            GCCIDType = EnIDType[idItem?.Type];
                        }
                    GCCIDTypeIdNumberValue = idItem?.Idnumber;
                    SelectedDOB = taxPayerDetails?.Birthdt;//?ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                    Title = taxPayerDetails?.TpTitle;
                    FirstName = taxPayerDetails?.NameFirst;
                    LastName = taxPayerDetails?.NameLast?.Replace(".", string.Empty);
                    FatherName = taxPayerDetails?.FatherName;
                    GrandFatherName = taxPayerDetails?.GrandfatherName;
                    FamilyName = taxPayerDetails?.FamilyName;
                    Initial = taxPayerDetails?.Initials;
                    if (taxPayerDetails?.Xsexm == "X")
                        SelectedGender = GenderList.FirstOrDefault();
                    if (taxPayerDetails?.Xsexf == "X")
                        SelectedGender = GenderList.LastOrDefault();
                    if (string.IsNullOrEmpty(SelectedGender))
                    {
                        SelectedGender = GenderList.FirstOrDefault();
                    }
                    SelectedTaxpayerPDNationality = TaxpayerFullNationlityList.Where(i => i.Land1 == taxPayerDetails?.Natio).FirstOrDefault();
                    SelectedCitizen = TaxpayerFullNationlityList.Where(i => i.Land1 == taxPayerDetails?.Citizen).FirstOrDefault();
                    SelectedResidence = TaxpayerFullNationlityList.Where(i => i.Land1 == taxPayerDetails?.Residence).FirstOrDefault();

                  await  bindingOutletList();

                    //As per new CR changes this call is not needed anymore.

                    //  taxPayerDetails = await EstablishmentRegistrationWebServiceManager.ESTTaxPayerDetailGetService("03", App.LoginDataRetrieved.TIN, App.LoginDataRetrieved.Emailid, null, taxPayerDetails?.Fbnumx);
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.FinancialDetail)
                {
                    taxPayerDetails = await EstablishmentRegistrationWebServiceManager.ESTTaxPayerDetailGetService("04", App.LoginDataRetrieved.TIN, App.LoginDataRetrieved.Emailid, null, taxPayerDetails?.Fbnumx);
                    //TODO recheck
                    SelectedMethod = EnMethodList[taxPayerDetails?.Accmethod];
                    CalendarType = EnCalendarTypeList[taxPayerDetails?.Fdcalender];
                    if (taxPayerDetails?.Accmethod == "A")
                    {
                        SelectedMethod = AppResources.NDAccounting;
                        FinSelectedMethod = AppResources.FORM5AccountingMethod;
                    }
                    else
                    {
                        SelectedMethod = AppResources.NDEstimated;
                        FinSelectedMethod = AppResources.ESTEstimatedMethod;
                    }

                    if (taxPayerDetails?.Fdcalender == "1")
                    {
                        CalendarType = AppResources.Gregorian;
                        calType = "Gregorian";
                    }

                    else
                    {
                        CalendarType = AppResources.Hijri;
                        calType = "Hijri";

                    }
                
                   await udpdateDates();
                }
            }
            catch (Exception )
            {
                IsLoading = false;
            }
            finally
            {
                IsLoading = false;
                updateDatePickers(_enum);
            }
        }

        private void updateDatePickers(EstablishmentRegistrationTabsEnum _enum)
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
                if (_enum == EstablishmentRegistrationTabsEnum.TaxpayerDetail)
                {
                    DateTime.TryParseExact(SelectedDOB, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime _dob);
                    if (taxPayerDetails?.Caltp == "Gregorian")
                    {
                        SelectedDOBDate = _selectedDOBDate;
                        if (!string.IsNullOrWhiteSpace(SelectedDOB))
                            DisplaySelectedDOB = _dob.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                    }
                    else
                    {
                        SelectedDOBHijiriDate = _selectedDOBDate;
                        if (!string.IsNullOrWhiteSpace(SelectedDOB))
                            DisplaySelectedDOB = HijriDateString(Convert.ToDateTime(SelectedDOB));
                    }
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.PassportDetails)
                {
                    DateTime.TryParseExact(PassportIssueDate, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime _issueDate);
                    DateTime.TryParseExact(PassportExpireDate, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime _expiryDate);
                    if (taxPayerDetails?.Caltp == "Gregorian")
                    {
                        SelectedPassportIssueDate = _selectedDOBDate;
                        if (!string.IsNullOrWhiteSpace(PassportIssueDate))
                            DisplayPassportIssueDate = _issueDate.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                    }
                    else
                    {
                        SelectedPassportIssueHijiriDate = _selectedDOBDate;
                        if (!string.IsNullOrWhiteSpace(PassportIssueDate))
                            DisplayPassportIssueDate = HijriDateString(Convert.ToDateTime(PassportIssueDate));
                    }
                    if (taxPayerDetails?.Caltp == "Gregorian")
                    {
                        SelectedPassportExpireDate = _selectedDOBDate;
                        if (!string.IsNullOrWhiteSpace(PassportExpireDate))
                            DisplayPassportExpireDate = _expiryDate.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                    }
                    else
                    {
                        SelectedPassportExpireHijiriDate = _selectedDOBDate;
                        if (!string.IsNullOrWhiteSpace(PassportExpireDate))
                            DisplayPassportExpireDate = HijriDateString(Convert.ToDateTime(PassportExpireDate));
                    }
                }
            }
            catch (Exception)
            { }
        }

        public void updateDatePickers()
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


                DateTime.TryParseExact(PassportIssueDate, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime _issueDate);
                DateTime.TryParseExact(PassportExpireDate, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime _expiryDate);
                if (taxPayerDetails?.Caltp == "Gregorian")
                {
                    SelectedPassportIssueDate = _selectedDOBDate;
                    if (!string.IsNullOrWhiteSpace(PassportIssueDate))
                        DisplayPassportIssueDate = _issueDate.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                }
                else
                {
                    SelectedPassportIssueHijiriDate = _selectedDOBDate;
                    if (!string.IsNullOrWhiteSpace(PassportIssueDate))
                        DisplayPassportIssueDate = HijriDateString(_issueDate);
                }

            }
            catch (Exception)
            {
            }
        }

        public async Task udpdateDates(string selectedDate = null)
        {
            try
            {
                IsLoading = true;
                //TODO recheck
                var _CalendarType = EnCalendarTypeList.FirstOrDefault(i => i.Value == CalendarType).Key == "2" ? "Hijri" : "Gregorian";
                financialDetail = await EstablishmentRegistrationWebServiceManager.ESTFinancialMaxDate(new FinancialDetailRequest()
                {
                    ACaltype = _CalendarType,
                    ADateComm = taxPayerDetails?.Commdt
                });
                if (_CalendarType == "Hijri")
                {
                    string dd = financialDetail?.ACommDate.Substring(6, 2);
                    string mm = financialDetail?.ACommDate.Substring(4, 2);
                    string yy = financialDetail?.ACommDate.Substring(0, 4);
                    CommDate = $"{short.Parse(yy) - 1:0000}/{short.Parse(mm):00}/{short.Parse(dd):00}";
                }
                else
                {
                    // CommDate = string.Format("{0:0000/00/00}", Int64.Parse(financialDetail?.ACommDate));
                    CommDate = Convert.ToDateTime(financialDetail?.ACommDate).ToString("yyyy/MM/dd");
                }


                if (selectedDate == null)
                {
                    string dd = financialDetail?.ACommDate.Substring(8, 2);
                    string mm = financialDetail?.ACommDate.Substring(5, 2);
                    if (dd != "01")
                    {
                        if (taxPayerDetails?.Fdcalender == "Gregorian")
                        {
                            if (dd == "29" && mm == "02")
                            {
                                dd = $"{short.Parse(dd) - 2:00}";
                            }
                            else
                            {
                                dd = $"{short.Parse(dd) - 1:00}";
                            }
                        }
                        else
                        {
                            dd = $"{short.Parse(dd) - 1:00}";
                        }
                        FiscalMonth = mm;
                        FiscalDay = dd;
                    }
                    else if (dd == "01" && mm == "01")
                    {
                        FiscalMonth = "12";
                        FiscalDay = AppResources.ESTFinLastDay;
                    }
                    else
                    {
                        mm = $"{short.Parse(mm) - 1:00}";
                        FiscalMonth = mm;
                        FiscalDay = AppResources.ESTFinLastDay;
                    }
                }
                financialDetail = await EstablishmentRegistrationWebServiceManager.ESTFinancialMaxDate(new FinancialDetailRequest()
                {
                    ACaltype = _CalendarType,
                    AMonth = FiscalMonth,
                    EIslmedate = FiscalDay == AppResources.ESTFinLastDay ? "32" : FiscalDay,
                    ADateComm = taxPayerDetails?.Commdt
                });
                TaxDate = _CalendarType == "H" ? UtilityManager.ConvertToDateFormat(financialDetail?.ACommDate ?? "", "yyyy/MM/dd") : UtilityManager.ConvertToDateFormat(financialDetail?.EIsldate ?? "", "yyyy/MM/dd");

                if (!string.IsNullOrEmpty(FiscalMonth) && string.IsNullOrEmpty(FiscalDay))
                {

                    var selectedFintype = "";

                    if (!string.IsNullOrEmpty(SelectedMethod))
                    {
                        selectedFintype = EnMethodList.FirstOrDefault(i => i.Value == SelectedMethod).Key;
                    }
                    else
                    {
                        selectedFintype = taxPayerDetails.Accmethod;
                    }

                    financialDetail = await EstablishmentRegistrationWebServiceManager.ESTFinancialMaxDateForPeriod(new FinancialDetailPeriodRequest()
                    {
                        ACaltype = _CalendarType,
                        AMonth = FiscalMonth,
                        ADateComm = taxPayerDetails?.Commdt,
                        Gpart = "",
                        Zfintype = selectedFintype,
                        Fbnum = taxPayerDetails.Fbnumx,
                        PeriodSet = new List<PeriodSetResult>()
                    });

                    dates = new List<string> { AppResources.ESTFinLastDay, "30", "29", "28", "27", "26", "25", "24", "23", "22", "21", "20", "19", "18", "17", "16", "15", "14", "13", "12", "11", "10", "09", "08", "07", "06", "05", "04", "03", "02", "01" };


                    if (!string.IsNullOrEmpty(financialDetail?.EIslmedate) /*&& (taxPayerDetails?.Fdcalender == "2")*/)
                    {
                        if (financialDetail?.EIslmedate == "28")
                        {
                            dates.Remove("29");
                            dates.Remove("30");
                        }
                        else if (financialDetail?.EIslmedate == "29")
                        {
                            dates.Remove("29");
                            dates.Remove("30");
                        }
                        else if (financialDetail?.EIslmedate == "30")
                        {
                            dates.Remove("30");
                        }
                    }

                }
                else if (!string.IsNullOrEmpty(FiscalDay) && !string.IsNullOrEmpty(FiscalMonth))
                {

                    financialDetail = await EstablishmentRegistrationWebServiceManager.ESTFinancialMaxDate(new FinancialDetailRequest()
                    {
                        ACaltype = _CalendarType,
                        AMonth = FiscalMonth,
                        EIslmedate = FiscalDay == AppResources.ESTFinLastDay ? "32" : FiscalDay,
                        ADateComm = taxPayerDetails?.Commdt
                    });
                    TaxDate =_CalendarType == "Hijri" ? UtilityManager.ConvertToDateFormat(financialDetail?.ACommDate ?? "","yyyy/MM/dd") : UtilityManager.ConvertToDateFormat(financialDetail?.EIsldate??"","yyyy/MM/dd");

                    if (taxPayerDetails.LastFilledRetdt != null)
                    {

                        LastFulfilledReturn = taxPayerDetails.LastFilledRetdt;    //?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));

                    }
                    if (taxPayerDetails.Zyear != null)
                    {

                        ZYear = AppResources.FinacialDetailsZyear.Replace("yyyy", taxPayerDetails.Zyear);

                    }


                    var selectedFintype = "";

                    if (!string.IsNullOrEmpty(SelectedMethod))
                    {
                        selectedFintype = EnMethodList.FirstOrDefault(i => i.Value == SelectedMethod).Key;
                    }
                    else
                    {
                        selectedFintype = taxPayerDetails.Accmethod;
                    }


                    if (SelectedMethod == AppResources.NDAccounting)
                    {

                        IsFinancePeriodVisible = true;
                    }
                    else
                    {
                        IsFinancePeriodVisible = false;

                    }


                    financialDetailPeriod = await EstablishmentRegistrationWebServiceManager.ESTFinancialMaxDateForPeriod(new FinancialDetailPeriodRequest()
                    {
                        ACaltype = _CalendarType,
                        AMonth = FiscalMonth,
                        EIslmedate = FiscalDay == AppResources.ESTFinLastDay ? "32" : FiscalDay,
                        ADateComm = taxPayerDetails?.Commdt,
                        Gpart = App.LoginDataRetrieved.TIN,
                        Zfintype = selectedFintype,
                        Fbnum = taxPayerDetails.Fbnumx,
                        PeriodSet = new List<PeriodSetResult>()
                    });

                    if (financialDetailPeriod != null)
                    {
                        IsFinancePeriodVisible = true;

                        var fincialPeriodDetials = financialDetailPeriod.PeriodSet;

                        foreach (var s in fincialPeriodDetials)
                        {
                            var fromDate = string.Empty;
                            var toDay = string.Empty;
                            if (_CalendarType == "Hijri")
                            {

                                fromDate = s.FromDate;
                                toDay = s.ToDate;

                            }
                            else
                            {

                                fromDate = s.FromDate;
                                toDay = s.ToDate;

                            }

                            s.ConvretedFromDate = fromDate;
                            s.ConvretedToDate = toDay;


                        }

                        PeriodList = fincialPeriodDetials;


                        isDraftEnabled = financialDetailPeriod.Draft;
                        if (PeriodList.Count > 0)
                        {

                            if (!string.IsNullOrEmpty(taxPayerDetails.FinPeriod))
                            {
                                TaxDate = String.Empty;
                                SelectedPeriod = PeriodList.Where(temp => (temp.FinPeriod == taxPayerDetails.FinPeriod)).FirstOrDefault();
                                
                                TaxDate = SelectedPeriod?.ConvretedToDate;

                            }
                            else
                            {
                                SelectedPeriod = PeriodList.FirstOrDefault();
                            }



                        }
                        else
                        {
                            TaxDate = financialDetailPeriod.EIsldate;
                        }
                    }




                    IsLoading = false;

                }
                else
                {

                    PeriodList.Clear();
                    SelectedPeriod = null;
                    TaxDate = string.Empty;
                    taxPayerDetails.FinPeriod = string.Empty;
                }

                IsLoading = false;

            }
            catch (Exception ex)
            {
                IsLoading = false;


                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Somethingwentwrong));

            }
        }
        //ENYT
        private async Task bindingOutletList()
        {
            try
            {
                var _outletTempData = await EstablishmentRegistrationWebServiceManager.ESTOutletList(taxPayerDetails?.PortalUsrx, App.LoginDataRetrieved.TIN, taxPayerDetails?.Fbnumx);
                OutletData.Clear();
                SearchableOutletData?.Clear();
                OutlettUiList.Clear();
                foreach (var _out in _outletTempData)
                {
                    OutletData.Add(_out);
                    SearchableOutletData.Add(_out);

                    OuteltInfo_NestedListView newItem = new OuteltInfo_NestedListView();

                    newItem.Metadata = _out.__metadata;
                    newItem.CityCode = _out.CityCode;
                    newItem.City1 = _out.City1;
                    newItem.Crlicenceno = _out.Crlicenceno;
                    newItem.Oldmst = _out.Oldmst;
                    newItem.Outdocdreg = _out.Outdocdreg;
                    newItem.Caltp = _out.Caltp;
                    newItem.Cmatt = _out.Cmatt;
                    newItem.Mandtx = _out.Mandtx;
                    newItem.Fbnumx = _out.Fbnumx;
                    newItem.Rentatt = _out.Rentatt;
                    newItem.Conatt = _out.Conatt;
                    newItem.PortalUsrx = _out.PortalUsrx;
                    newItem.Langx = _out.Langx;
                    newItem.Operationx = _out.Operationx;
                    newItem.StepNumberx = _out.StepNumberx;
                    newItem.ReturnIdx = _out.ReturnIdx;
                    newItem.Officerx = _out.Officerx;
                    newItem.Gpartx = _out.Gpartx;
                    newItem.Mandt = _out.Mandt;
                    newItem.FormGuid = _out.FormGuid;
                    newItem.DataVersion = _out.DataVersion;
                    newItem.LineNo = _out.LineNo;
                    newItem.RankingOrder = _out.RankingOrder;
                    newItem.ActNo = _out.Actno;
                    newItem.StartDate = _out.StartDate;
                    newItem.EndDate = _out.EndDate;
                    newItem.ActCat = _out.Actcat;
                    newItem.ActNm = _out.Actnm;
                    newItem.ActNm2 = _out.Actnm2;
                    newItem.ChInd = _out.ChInd;
                    newItem.MciEntry = _out.MciEntry;
                    newItem.ShowDeleteIcon = true;
                   

                    if (newItem.MciEntry.ToUpper().ToString().Equals("X"))
                    {
                        newItem.ShowEditIcon = false;
                    }
                    else
                    {
                        newItem.ShowEditIcon = true;
                    }

                    if (newItem.ActCat.Equals("M"))
                    {
                        if (App.IsArabic)
                        {
                            newItem.ActCatDesc = "فرع رئيسي";
                        }
                        else
                        {
                            newItem.ActCatDesc = "Main Outlet";
                        }
                    }
                    else
                    {
                        if (App.IsArabic)
                        {
                            newItem.ActCatDesc = "فرع فرعي";
                        }
                        else
                        {
                            newItem.ActCatDesc = "Sub Outlet";
                        }
                    }

                    OutlettUiList.Add(newItem);
                }
            }
            catch (Exception)
            {

            }
        }

        private void openNewOutlet()
        {
            OutletNavigationModels outletNavigationModels = new OutletNavigationModels();
            outletNavigationModels.taxPayerDetails = taxPayerDetails;
            outletNavigationModels.idItem = idItem;
            _navigationService.NavigateTo(App.OutletDetailsPageView, outletNavigationModels);
        }

        private async Task openEditOutletAsync(OuteltInfo_NestedListView item, int btnCode)
        {
            try
            {



                SelectedItem = item;
                IsLoading = true;
                OutletNavigationModels outletNavigationModels = new OutletNavigationModels();
                outletNavigationModels.taxPayerDetails = taxPayerDetails;
                var OutletActNumber = (item.ActNo == null || string.IsNullOrEmpty(item?.ActNo)) ? "00000" : item.ActNo;


                item.ContactDetails = new ObservableCollection<Nreg_ActivityItem>();
                item.ContactDetails2 = new ObservableCollection<Nreg_ActivityItem>();

                OutletDropDowns = await EstablishmentRegistrationWebServiceManager.ESTOutletDropDowns();
                activityList = await EstablishmentRegistrationWebServiceManager.ESTOutletGetActivitySetsList();

                taxPayerDetails = await EstablishmentRegistrationWebServiceManager.ESTTaxPayerDetailGetService("03", App.LoginDataRetrieved.TIN, App.LoginDataRetrieved.Emailid, OutletActNumber, taxPayerDetails?.Fbnumx);
                PrepareUIBranchesList(item);
                IsLoading = false;
                if (btnCode == 1)
                {
                    if (SelectedItem != null)
                    {
                        if (SelectedItem.ActNo == item.ActNo && SelectedItem.IsInnerListVisible == true)
                        {
                            item.IsInnerListVisible = false;
                            return;
                        }
                        else
                        {
                            item.IsInnerListVisible = true;
                        }
                    }
                    else
                    {
                        item.IsInnerListVisible = !item.IsInnerListVisible;
                    }
                }

                if (btnCode == 2)
                {
                    outletNavigationModels.idItem = idItem;
                    outletNavigationModels.selectedOutletItem = GetSelectedItem(item);
                   await _navigationService.NavigateTo(App.OutletDetailsPageView, outletNavigationModels);
                }

            }
            catch (Exception)
            {
                IsLoading = false;
            }


        }

        private OutletItem GetSelectedItem(OuteltInfo_NestedListView _out)
        {
            OutletItem newItem = new OutletItem();
            newItem.__metadata = _out.Metadata;
            newItem.CityCode = _out.CityCode;
            newItem.City1 = _out.City1;
            newItem.Crlicenceno = _out.Crlicenceno;
            newItem.Oldmst = _out.Oldmst;
            newItem.Outdocdreg = _out.Outdocdreg;
            newItem.Caltp = _out.Caltp;
            newItem.Cmatt = _out.Cmatt;
            newItem.Mandtx = _out.Mandtx;
            newItem.Fbnumx = _out.Fbnumx;
            newItem.Rentatt = _out.Rentatt;
            newItem.Conatt = _out.Conatt;
            newItem.PortalUsrx = _out.PortalUsrx;
            newItem.Langx = _out.Langx;
            newItem.Operationx = _out.Operationx;
            newItem.StepNumberx = _out.StepNumberx;
            newItem.ReturnIdx = _out.ReturnIdx;
            newItem.Officerx = _out.Officerx;
            newItem.Gpartx = _out.Gpartx;
            newItem.Mandt = _out.Mandt;
            newItem.FormGuid = _out.FormGuid;
            newItem.DataVersion = _out.DataVersion;
            newItem.LineNo = _out.LineNo;
            newItem.RankingOrder = _out.RankingOrder;
            newItem.Actno = _out.ActNo;
            newItem.StartDate = _out.StartDate;
            newItem.EndDate = _out.EndDate;
            newItem.Actcat = _out.ActCat;
            newItem.Actnm = _out.ActNm;
            newItem.Actnm2 = _out.ActNm2;
            newItem.ChInd = _out.ChInd;
            newItem.MciEntry = _out.MciEntry;
            return newItem;
        }

        private void PrepareUIBranchesList(OuteltInfo_NestedListView outletItem)
        {
            try
            {
                outletItem.ContactDetails.Clear();
                outletItem.ContactDetails2.Clear();
                foreach (var item in taxPayerDetails.Nreg_ActivitySet)
                {
                    Nreg_ActivityItem obj = new Models.EstablishmentRegistration.Nreg_ActivityItem();
                    obj.__metadata = item.__metadata;
                    obj.CrType = item.CrType;
                    obj.ActName = item.ActName;
                    obj.MciEntry = item.MciEntry;
                    obj.CityCode = item.CityCode;
                    obj.ActSgrp = item.ActSgrp;
                    obj.Crstat = item.Crstat;
                    obj.Mncrfg = item.Mncrfg;
                    obj.Crexpdt = item.Crexpdt;
                    obj.Hstfg = item.Hstfg;
                    obj.Srno = item.Srno;
                    obj.Mandt = item.Mandt;
                    obj.FormGuid = item.FormGuid;
                    obj.Oldmst = item.Oldmst;
                    obj.Actdocdreg = item.Actdocdreg;
                    obj.DataVersion = item.DataVersion;
                    obj.Idnm = item.Idnm;
                    obj.Actno = item.Actno;
                    obj.Type = item.Type;
                    obj.Idnumber = item.Idnumber;


                    if (item.ValidDateFrom != null)
                    {
                        obj.ValidFromUI = Convert.ToDateTime(item.ValidDateFrom.ToString()).ToShortDateString();
                    }

                    obj.ValidDateFrom = item.ValidDateFrom;

                    obj.ValidDateTo = item.ValidDateTo;
                    obj.ValidDateType = item.ValidDateType;
                    obj.Country = item.Country;
                    obj.Institute = item.Institute;
                    obj.City = item.City;
                    obj.Crclsattfg = item.Crclsattfg;
                    obj.Crattfg = item.Crattfg;
                    obj.Crtrfattfg = item.Crtrfattfg;
                    obj.Activity = item.Activity;
                    obj.Actcat = item.Actcat;
                    obj.ActMgrp = item.ActMgrp;

                    obj.ActivityDesc = activityList.activitySet.Where(i => i.IndSector == obj.Activity).FirstOrDefault().Text;
                    obj.ActMgrpDesc = activityList.act_groupSet.Where(i => i.IndSector == obj.ActMgrp).FirstOrDefault().Text;
                    obj.ActSgrpDesc = activityList.act_subgroupSet.Where(i => i.IndSector == obj.ActSgrp).FirstOrDefault().Text;
                    obj.IssuedCity = OutletDropDowns.city_dropdownSet.Where(i => i.CityCode == obj.CityCode).FirstOrDefault()?.CityName;
                    obj.IssuedCountry = OutletDropDowns.country_dropdownSet.Where(i => i.Land1 == obj.Country).FirstOrDefault()?.Landx;

                    if (App.IsArabic)
                    {
                        //obj.IssuedBy = obj.IssuedCountry == "SA" ? Constants.ArIssueBy["90702"] : Constants.EnIssueBy["90718"];
                        if (obj.IssuedCountry == "SA" || obj.IssuedCountry == "Saudi Arabia" || obj.IssuedCountry.Equals("السعودية"))
                        {
                            obj.IssuedBy = ZATCAConstants.ArIssueBy["90702"];
                        }
                        else
                        {
                            obj.IssuedBy = ZATCAConstants.ArIssueBy["90718"];
                        }
                    }
                    else
                    {
                        //obj.IssuedBy = obj.IssuedCountry == "SA" ? Constants.ArIssueBy["90702"] : Constants.EnIssueBy["90718"];
                        if (obj.IssuedCountry == "SA" || obj.IssuedCountry == "Saudi Arabia" || obj.IssuedCountry.Equals("السعودية"))
                        {
                            obj.IssuedBy = ZATCAConstants.EnIssueBy["90702"];
                        }
                        else
                        {
                            obj.IssuedBy = ZATCAConstants.EnIssueBy["90718"];
                        }
                    }

                    if (item.Type.ToUpper().Equals("BUP002"))
                    {

                        if (item.CrType.Equals("M"))
                        {
                            if (App.IsArabic)
                            {
                                obj.CRTypeDesc = "سجل تجاري رئيسي";
                            }
                            else
                            {
                                obj.CRTypeDesc = "Main CR";
                            }
                        }
                        else
                        {
                            if (App.IsArabic)
                            {
                                obj.CRTypeDesc = "سجل تجاري فرعي";
                            }
                            else
                            {
                                obj.CRTypeDesc = "Sub CR";
                            }
                        }

                        outletItem.ContactDetails.Add(obj);

                    }
                    else
                    {
                        outletItem.ContactDetails2.Add(obj);

                    }


                }
                if (outletItem.ContactDetails.Count > 0)
                {
                    ShowCRNoData = false;
                }
                else
                {
                    ShowCRNoData = true;
                }
                if (outletItem.ContactDetails2.Count > 0)
                {
                    ShowLicenceNoData = false;
                }
                else
                {
                    ShowLicenceNoData = true;
                }
            }
            catch (Exception ex)
            {

            }

        }



        private void deleteOutlet(OutletItem item)
        {
            IsLoading = true;
            var delete = EstablishmentRegistrationWebServiceManager.ESTDeleteOutletItem(taxPayerDetails?.Fbnumx, item?.Actno, taxPayerDetails?.PortalUsrx).Result;
            if (!string.IsNullOrEmpty(delete) && delete == "delete")
            {
                bindingOutletList();
            }
            IsLoading = false;
        }
        private void ResidenceTypePrePopulateData(TaxPayerDetails taxPayerDetails)
        {
            if (taxPayerDetails.Tpresidence == "1")
            {
                OrgResidenceSelection(OrgResidenceNationalityEstablishmentRegistrationEnum.StayMoreThanKSA);
            }
            else if (taxPayerDetails.Tpresidence == "2")
            {
                OrgResidenceSelection(OrgResidenceNationalityEstablishmentRegistrationEnum.RentOwnhouseMoreThanThirtyDays);

            }
            else if (taxPayerDetails.Tpresidence == "3")
            {
                OrgResidenceSelection(OrgResidenceNationalityEstablishmentRegistrationEnum.NoneOfTheAbove);

                if (taxPayerDetails.Orgnonresident == "1")
                {
                    OrgNonResidentSelection(OrgNonResidentEstablishmentRegistrationEnum.PermanentEstablishment);

                    if (taxPayerDetails.Orgnonresidentoptions == "1")
                    {
                        OrgNonResidentOptionsSelection(OrgNonResidentOptionsEstablishmentEnum.ABranchOfNonResidentCompanyPE);
                    }
                    else if (taxPayerDetails.Orgnonresidentoptions == "2")
                    {
                        OrgNonResidentOptionsSelection(OrgNonResidentOptionsEstablishmentEnum.ConstructionSitePE);

                    }
                    else if (taxPayerDetails.Orgnonresidentoptions == "3")
                    {
                        OrgNonResidentOptionsSelection(OrgNonResidentOptionsEstablishmentEnum.InstallationPE);

                    }
                    else if (taxPayerDetails.Orgnonresidentoptions == "4")
                    {
                        OrgNonResidentOptionsSelection(OrgNonResidentOptionsEstablishmentEnum.AFixedBasePE);

                    }
                    else if (taxPayerDetails.Orgnonresidentoptions == "5")
                    {
                        OrgNonResidentOptionsSelection(OrgNonResidentOptionsEstablishmentEnum.NonResidentPartnerPE);

                    }
                }
                else if (taxPayerDetails.Orgnonresident == "2")
                {
                    OrgNonResidentSelection(OrgNonResidentEstablishmentRegistrationEnum.OtherTaxIncomeFromSourceWithInTheSKA);
                    SelectedOrgNonResidentActivityItem = "";
                    if (taxPayerDetails.Orgnonresidentactivity == "O1")
                    {
                        SelectedOrgNonResidentActivityItem = AppResources.ESTOrgNonResidentActivityValueOne;
                    }
                    else if (taxPayerDetails.Orgnonresidentactivity == "O2")
                    {
                        SelectedOrgNonResidentActivityItem = AppResources.ESTOrgNonResidentActivityValueTwo;
                    }
                    else if (taxPayerDetails.Orgnonresidentactivity == "O3")
                    {
                        SelectedOrgNonResidentActivityItem = AppResources.ESTOrgNonResidentActivityValueThree;
                    }
                    else if (taxPayerDetails.Orgnonresidentactivity == "O4")
                    {
                        SelectedOrgNonResidentActivityItem = AppResources.ESTOrgNonResidentActivityValueFour;

                    }
                    else if (taxPayerDetails.Orgnonresidentactivity == "O5")
                    {
                        SelectedOrgNonResidentActivityItem = AppResources.ESTOrgNonResidentActivityValueFive;

                    }
                    else if (taxPayerDetails.Orgnonresidentactivity == "O6")
                    {
                        SelectedOrgNonResidentActivityItem = AppResources.ESTOrgNonResidentActivityValueSix;

                    }
                    else if (taxPayerDetails.Orgnonresidentactivity == "O7")
                    {
                        SelectedOrgNonResidentActivityItem = AppResources.ESTOrgNonResidentActivityValueSeven;

                    }
                    else if (taxPayerDetails.Orgnonresidentactivity == "O8")
                    {
                        SelectedOrgNonResidentActivityItem = AppResources.ESTOrgNonResidentActivityValueEight;

                    }
                    else if (taxPayerDetails.Orgnonresidentactivity == "O9")
                    {
                        SelectedOrgNonResidentActivityItem = AppResources.ESTOrgNonResidentActivityValueNine;

                    }
                }
            }
        }

        private async Task<bool> FormValidation(EstablishmentRegistrationTabsEnum _enum)
        {
            try
            {
                if (_enum == EstablishmentRegistrationTabsEnum.RegistrationType)
                {
                    if (SelectedReportingBranch == null || string.IsNullOrWhiteSpace(SelectedReportingBranch.branchDescription))
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateBranch));
                        return false;
                    }
                    if (!IsSaudi)
                    {
                        if (string.IsNullOrWhiteSpace(SelectedTpresidence))
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateResidenceType));
                            return false;
                        }
                        else if (SelectedTpresidence == "2" && (UploadedRentDocumentsList == null || UploadedRentDocumentsList.Count <= 0))
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAttachRent));
                            return false;
                        }
                        else if (SelectedTpresidence == "3" && string.IsNullOrWhiteSpace(SelectedOrgNonResident))
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateLegalEntity));
                            return false;
                        }
                        else if (SelectedTpresidence == "3" && SelectedOrgNonResident == "1" && string.IsNullOrWhiteSpace(SelectedOrgNonResidentOptions))
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateParmanentEst));
                            return false;
                        }
                        else if (SelectedTpresidence == "3" && SelectedOrgNonResident == "2" && string.IsNullOrWhiteSpace(SelectedOrgNonResidentActivity))
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateOtherTax));
                            return false;
                        }
                    }
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.TaxpayerDetail)
                {

                    if (string.IsNullOrWhiteSpace(SelectedDOB))
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateDOB));
                        return false;
                    }
                    else if (string.IsNullOrWhiteSpace(FirstName))
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateFirstName));
                        return false;
                    }
                    else if (string.IsNullOrWhiteSpace(SelectedGender))
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateGender));
                        return false;
                    }
                    else if (null == SelectedTaxpayerPDNationality)
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateNationality));
                        return false;
                    }
                    else if (null == SelectedCitizen)
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateCitizen));
                        return false;
                    }
                    else if (null == SelectedResidence)
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateResidence));
                        return false;
                    }
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.PassportDetails)
                {
                    if (string.IsNullOrWhiteSpace(PassportNumber))
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePassportNumber));
                        return false;
                    }
                    else if (null == SelectedPassportIssueCountry)
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePIssueCountry));
                        return false;
                    }
                    else if (string.IsNullOrWhiteSpace(PassportIssueDate))
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePIssueDate));
                        return false;
                    }
                    else if (string.IsNullOrWhiteSpace(PassportExpireDate))
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePExpiryDate));
                        return false;
                    }
                    else if (UploadedPassportDocumentsList == null || UploadedPassportDocumentsList.Count <= 0)
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePAttachCopy));
                        return false;
                    }
                    else
                    {
                        DateTime.TryParseExact(SelectedDOB, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime dob);
                        DateTime.TryParseExact(PassportIssueDate, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime issue);
                        DateTime.TryParseExact(PassportExpireDate, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime expiry);
                        if (DateTime.Compare(issue, dob) < 0)
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp("passport issue date not before dob"));
                            return false;
                        }
                        else if (DateTime.Compare(expiry, dob) < 0)
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp("passport expiry date not before dob"));
                            return false;
                        }
                        else if (DateTime.Compare(expiry, issue) < 0)
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp("passport expiry date not before passport issue"));
                            return false;
                        }
                    }
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.Outlets)
                {
                    if (OutletData.Count == 0)
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAtleastOutlet));
                        return false;
                    }
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.FinancialDetail)
                {
                    if (string.IsNullOrEmpty(FiscalMonth))
                    {

                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.TPFinacialPeriodMonthValidation));
                        return false;
                    }
                    else if (string.IsNullOrEmpty(FiscalDay))
                    {

                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.TPFinacialPeriodMonthValidation));
                        return false;
                    }
                    //TODO 5250 New Reg commented.
                    //else if(SelectedPeriod == null ) {

                    //    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.TPFinacialPeriodValidation));
                    //    return false;
                    //}

                }
                else if (_enum == EstablishmentRegistrationTabsEnum.Declaration)
                {
                    if (!ESTLedge)
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePledge));
                        return false;
                    }
                }

            }
            catch (Exception)
            {}
            return true;
        }


        private async Task<bool> PushDatatoServer(EstablishmentRegistrationTabsEnum _enum)
        {
            try
            {
                IsLoading = true;
                if (_enum == EstablishmentRegistrationTabsEnum.RegistrationType)
                {
                    SelectedTpresidence = taxPayerDetails.Tpresidence;
                    taxPayerDetails.Augrp = SelectedReportingBranch?.authorizationGroup;
                    //TODO recheck
                    taxPayerDetails.Atype = "Individual";// SelectedEntityType.Equals("Individual") ? "1" : "2";
                    foreach (var s in NationalityMapping)
                    {
                        if(s.Value.Equals(SelectedRegNationalityType))
                        {
                            taxPayerDetails.Tpnationality = s.Key;
                        }

                    }
                    // taxPayerDetails.Tpnationality = NationalityMapping.Where(i => i.Value == SelectedRegNationalityType).FirstOrDefault().Key;
                    taxPayerDetails.Tpnationality = string.IsNullOrEmpty(taxPayerDetails.Tpnationality) ? "" : taxPayerDetails.Tpnationality;
                    taxPayerDetails.Taxtpdetermination = "1";
                    taxPayerDetails.Tpresidence = SelectedTpresidence != String.Empty ? SelectedTpresidence : taxPayerDetails.Tpresidence;
                    taxPayerDetails.Orgnonresident = string.IsNullOrEmpty(SelectedOrgNonResident) ? string.Empty : SelectedOrgNonResident;
                    taxPayerDetails.Orgnonresidentoptions = string.IsNullOrEmpty(SelectedOrgNonResidentOptions) ? string.Empty : SelectedOrgNonResidentOptions;
                    taxPayerDetails.Orgnonresidentactivity = string.IsNullOrEmpty(SelectedOrgNonResidentActivity) ? string.Empty : SelectedOrgNonResidentActivity;
                    taxPayerDetails.Chkfg = "X";
                    if (UploadedRentDocumentsList != null && UploadedRentDocumentsList.Count > 0)
                    {
                        taxPayerDetails.Rentatt = "X";
                    }
                    else
                    {
                        taxPayerDetails.Rentatt = "";
                    }
                    taxPayerDetails.StepNumberx = "01";
                    taxPayerDetails.Gpart = App.LoginDataRetrieved.TIN;
                    taxPayerDetails.UserTypx = "TP";
                    var taxPayerDetailsResult = await EstablishmentRegistrationWebServiceManager.ESTTaxPayerDetailPostService(taxPayerDetails);

                    IsLoading = false;
                    return true;
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.TaxpayerDetail)
                {
                    DateTime.TryParseExact(SelectedDOB, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime dob);
                    taxPayerDetails.Birthdt = dob.ToString("yyyy-MM-ddThh:mm:ss");
                    taxPayerDetails.TpTitle = Title;
                    taxPayerDetails.NameFirst = FirstName;
                    taxPayerDetails.NameLast = string.IsNullOrEmpty(LastName) ? string.Empty : LastName;
                    taxPayerDetails.FatherName = string.IsNullOrEmpty(FatherName) ? string.Empty : FatherName;
                    taxPayerDetails.GrandfatherName = string.IsNullOrEmpty(GrandFatherName) ? string.Empty : GrandFatherName;
                    taxPayerDetails.FamilyName = string.IsNullOrEmpty(FamilyName) ? string.Empty : FamilyName;
                    taxPayerDetails.Initials = string.IsNullOrEmpty(Initial) ? string.Empty : Initial;
                    taxPayerDetails.Chkfg = "X";

                    if (SelectedGender?.ToLower() == GenderList.FirstOrDefault().ToLower())
                    {
                        taxPayerDetails.Xsexm = "X";
                    }
                    else if (SelectedGender?.ToLower() == GenderList.LastOrDefault().ToLower())
                    {
                        taxPayerDetails.Xsexf = "X";
                    }
                    taxPayerDetails.Natio = SelectedTaxpayerPDNationality?.Land1;
                    taxPayerDetails.Citizen = SelectedCitizen?.Land1;
                    taxPayerDetails.Residence = SelectedResidence?.Land1;

                    taxPayerDetails.StepNumberx = "02";
                    taxPayerDetails.Gpart = App.LoginDataRetrieved.TIN;
                    taxPayerDetails.UserTypx = "TP";
                    var taxPayerDetailsResult = await EstablishmentRegistrationWebServiceManager.ESTTaxPayerDetailPostService(taxPayerDetails);


                    IsLoading = false;
                    return true;
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.PassportDetails)
                {
                    Nreg_IdItem passportObj = new Nreg_IdItem();
                    passportObj.Idnumber = PassportNumber;
                    passportObj.Country = SelectedPassportIssueCountry?.Land1;

                    DateTime.TryParseExact(PassportIssueDate, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime issueDate);
                    DateTime.TryParseExact(PassportExpireDate, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime expireDate);
                    passportObj.ValidDateFrom = issueDate.ToString("yyyy-MM-ddThh:mm:ss");
                    passportObj.ValidDateTo = expireDate.ToString("yyyy-MM-ddThh:mm:ss");
                    passportObj.Type = "FS0002";
                    passportObj.Srcidentify = "00000";
                    passportObj.Gpart = App.LoginDataRetrieved.TIN;
                    if (taxPayerDetails.Nreg_IdSet.Count > 0)
                    {
                        taxPayerDetails.Nreg_IdSet[0].Srcidentify = "00000";
                        taxPayerDetails.Nreg_IdSet[0].Gpart = App.LoginDataRetrieved.TIN;

                        Nreg_IdItem nreg_IdItem = new Nreg_IdItem();
                        nreg_IdItem = taxPayerDetails.Nreg_IdSet[0];
                        taxPayerDetails.Nreg_IdSet.Clear();
                        taxPayerDetails.Nreg_IdSet.Add(nreg_IdItem);

                    }
                    taxPayerDetails.Nreg_IdSet.Add(passportObj);
                    taxPayerDetails.Chkfg = "X";
                    if (UploadedPassportDocumentsList != null && UploadedPassportDocumentsList.Count > 0)
                    {

                        taxPayerDetails.Passatt = "X";
                    }
                    else
                    {
                        taxPayerDetails.Passatt = "";
                    }

                    taxPayerDetails.StepNumberx = "02";
                    taxPayerDetails.Gpart = App.LoginDataRetrieved.TIN;
                    taxPayerDetails.UserTypx = "TP";
                    var taxPayerDetailsResult = await EstablishmentRegistrationWebServiceManager.ESTTaxPayerDetailPostService(taxPayerDetails);

                    IsLoading = false;
                    return true;
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.FinancialDetail)
                {
                    // DateTime.TryParseExact(TaxDate, string.Format("{0:0000/00/00}", Int64.Parse(financialDetail?.EIsldate)), new CultureInfo("en-US"), DateTimeStyles.None, out DateTime Fdenddt);
                    taxPayerDetails.Accmethod = EnMethodList.FirstOrDefault(i => i.Value == SelectedMethod).Key;
                    taxPayerDetails.Fdcalender = EnCalendarTypeList.FirstOrDefault(i => i.Value == CalendarType).Key;
                    taxPayerDetails.Fdmonth = FiscalMonth;
                    taxPayerDetails.Fdday = FiscalDay == AppResources.ESTFinLastDay ? "LD" : FiscalDay;
                    taxPayerDetails.Commdt = financialDetail?.ACommDate;
                    taxPayerDetails.Caltp = calType;
                    //taxPayerDetails.Fdenddt = Fdenddt;
                    taxPayerDetails.Chkfg = "X";
                    if (SelectedPeriod != null)
                    {
                        taxPayerDetails.FinPeriod = SelectedPeriod.FinPeriodText;
                        taxPayerDetails.FromDt = SelectedPeriod.FromDate;
                        taxPayerDetails.Fdenddt = SelectedPeriod.ToDate;
                    }
                    taxPayerDetails.Gpart = App.LoginDataRetrieved.TIN;
                    taxPayerDetails.StepNumberx = "04";
                    taxPayerDetails.UserTypx = "TP";
                    var taxPayerDetailsResult = await EstablishmentRegistrationWebServiceManager.ESTTaxPayerDetailPostService(taxPayerDetails);

                    IsLoading = false;
                    return true;
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.Declaration)
                {
                    taxPayerDetails?.off_notesSet?.Clear();
                    taxPayerDetails.Decfg = "X";
                    taxPayerDetails.Operationx = "01";
                    taxPayerDetails.Gpart = App.LoginDataRetrieved.TIN;
                    taxPayerDetails.UserTypx = "TP";
                    taxPayerDetails = await EstablishmentRegistrationWebServiceManager.ESTTaxPayerDetailPostService(taxPayerDetails);

                    IsLoading = false;
                    return true;
                }

                IsLoading = false;
            }
            catch (Exception ex)
            {
                IsLoading = false;

                if (ex is HTTPBadRequestException)
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                }
                return false;
            }
            return false;
        }

        public void RentAttachmentPrePopulateCheck(TaxPayerDetails taxPayerDetails)
        {
            UploadedRentDocumentsList.Clear();
            var docRentResult = taxPayerDetails.AttDetSet.Where(x => x.Dotyp == "RG16").ToList();

            foreach (AttDetItem attDetItem in docRentResult)
            {
                var obj = new Attachment();
                obj.Filename = attDetItem.Filename;
                obj.FileExtn = attDetItem.FileExtn;
                obj.Mimetype = attDetItem.Mimetype;
                obj.RetGuid = attDetItem.RetGuid;
                obj.DocUrl = attDetItem.DocUrl;
                obj.Dotyp = attDetItem.Dotyp;
                obj.Doguid = attDetItem.Doguid;
                UploadedRentDocumentsList.Add(obj);
            }

            if (UploadedRentDocumentsList.Count > 0)
            {
                IsVisbleRentAttachmentmentList = true;
            }
            else
            {
                IsVisbleRentAttachmentmentList = false;
            }
        }


        public void PassportAttachmentPrepopulateCheck(TaxPayerDetails taxPayerDetails)
        {
            UploadedPassportDocumentsList.Clear();
            var docPassportResult = taxPayerDetails.AttDetSet.Where(x => x.Dotyp == "RG19").ToList();

            foreach (AttDetItem attDetItem in docPassportResult)
            {
                var obj = new Attachment();
                obj.Filename = attDetItem.Filename;
                obj.FileExtn = attDetItem.FileExtn;
                obj.Mimetype = attDetItem.Mimetype;
                obj.RetGuid = attDetItem.RetGuid;
                obj.DocUrl = attDetItem.DocUrl;
                obj.Dotyp = attDetItem.Dotyp;
                obj.Doguid = attDetItem.Doguid;
                UploadedPassportDocumentsList.Add(obj);
            }

            if (UploadedPassportDocumentsList.Count > 0)
            {
                IsVisbleAttachmentPassportList = true;
            }
            else
            {
                IsVisbleAttachmentPassportList = false;
            }
        }


        private bool IsFileAlreadyAttached(string doctype, string FileName)
        {
            bool isFileAlreadyAttached = false;
            if (doctype.Equals("RG16"))//Rent
            {
                if (UploadedRentDocumentsList != null && UploadedRentDocumentsList.Count > 0)
                {
                    for (int i = 0; i < UploadedRentDocumentsList.Count; i++)
                    {
                        if (UploadedRentDocumentsList[i].Filename.Equals(FileName))
                            isFileAlreadyAttached = true;
                        else
                            isFileAlreadyAttached = false;
                        if (isFileAlreadyAttached)
                            break;
                    }
                }
            }
            else if (doctype.Equals("RG19"))//PAssport
            {
                if (UploadedPassportDocumentsList != null && UploadedPassportDocumentsList.Count > 0)
                {
                    for (int i = 0; i < UploadedPassportDocumentsList.Count; i++)
                    {
                        if (UploadedPassportDocumentsList[i].Filename.Equals(FileName))
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

        public void clearFormData(EstablishmentRegistrationTabsEnum _enum)
        {
            switch (_enum)
            {
                case EstablishmentRegistrationTabsEnum.TaxpayerDetail:
                    idItem = null;
                    SelectedDOB = string.Empty;
                    DisplaySelectedDOB = string.Empty;
                    SelectedDOBDate = null;
                    SelectedDOBHijiriDate = null;
                    FirstName = string.Empty;
                    LastName = string.Empty;
                    FatherName = string.Empty;
                    GrandFatherName = string.Empty;
                    FamilyName = string.Empty;
                    Initial = string.Empty;
                    SelectedGender = string.Empty;
                    SelectedTaxpayerPDNationality = null;
                    SelectedCitizen = null;
                    SelectedResidence = null;
                    GCCIDType = string.Empty;
                    GCCIDTypeIdNumberValue = string.Empty;
                    break;
                case EstablishmentRegistrationTabsEnum.PassportDetails:
                    PassportNumber = string.Empty;
                    SelectedPassportIssueCountry = null;
                    PassportIssueDate = string.Empty;
                    DisplayPassportIssueDate = string.Empty;
                    SelectedPassportIssueDate = null;
                    SelectedPassportIssueHijiriDate = null;
                    PassportExpireDate = string.Empty;
                    DisplayPassportExpireDate = string.Empty;
                    SelectedPassportExpireDate = null;
                    SelectedPassportExpireHijiriDate = null;
                    UploadedPassportDocumentsList?.Clear();
                    break;
                case EstablishmentRegistrationTabsEnum.Outlets:
                    OutletData?.Clear();
                    SearchableOutletData?.Clear();
                    break;
                case EstablishmentRegistrationTabsEnum.FinancialDetail:
                    FiscalMonth = string.Empty;
                    FiscalDay = string.Empty;
                    CommDate = string.Empty;
                    TaxDate = string.Empty;
                    break;
                case EstablishmentRegistrationTabsEnum.Declaration:
                    ESTLedge = false;
                    break;
                case EstablishmentRegistrationTabsEnum.RegistrationType:
                default:
                    SelectedReportingBranch = null;
                    SelectedEntityType = string.Empty;
                    SelectedTaxPayerType = string.Empty;
                    SelectedTpresidence = string.Empty;
                    SelectedRegNationalityType = string.Empty;
                    SelectedOrgNonResident = string.Empty;
                    SelectedOrgNonResidentOptions = string.Empty;
                    SelectedOrgNonResidentActivity = string.Empty;
                    IsSaudi = false;
                    UploadedRentDocumentsList?.Clear();
                    break;
            }
        }
        private string HijriDateString(DateTime date)//01-01-0001
        {
            try
            {
                return date.ToString("yyyy/MM/dd", new CultureInfo("ar-sa"));
            }
            catch (Exception)
            {


                HijriCalendar hijriCalendar = new HijriCalendar();
                return $"{hijriCalendar.GetYear(date):0000}/{hijriCalendar.GetMonth(date):00}/{hijriCalendar.GetDayOfMonth(date):00}";
            }
        }

        #endregion
    }


}
