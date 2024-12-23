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
using ZATCAMAUI.Views.NewDesign.Common;
using ZATCAMAUI.Views.NewDesign.EstablishmentRegistrationPages;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Views.NewDesign.EstablishmentAmendUpdatePages;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.EstablishmentAmendUpdateViewModel
{

    public class EstablishmentAmendUpdatePageViewModel : BaseViewModel
    {

        #region Variable

        public string PageTitle { get; set; }
        public bool IsExceptionPopupVisible { get; set; } = false;
        public static TaxPayerDetails taxPayerDetails { get; set; } = null;
        private FinancialDetail financialDetail { get; set; } = null;
        private FinancialDetail financialDetailPeriod { get; set; } = null;
        public bool isFinaceDetailsChanged { get; set; } = false;
        public string isDraftEnabled { get; set; } = "";
        public Nreg_IdItem idItem { get; set; } = null;
        public bool IsNavigationCompletedToSuccessfulPage { get; set; } = false;
        private EstablishmentRegistrationTabsEnum previousTab;
        private EstablishmentRegistrationTabsEnum _currentTab;
        public EstablishmentRegistrationTabsEnum currentTab
        {
            get => _currentTab;
            set
            {
                try
                {
                    /// if (_currentTab == value) return;
                    if (_currentTab == value)
                    {
                        if (!IsNavigationCompletedToSuccessfulPage)
                        {
                            MainThread.BeginInvokeOnMainThread(async () => await fetchTabDataAndBind(_currentTab));
                        }
                        previousTab = _currentTab;
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
                    previousTab = _currentTab;
                }
                catch (NullReferenceException)
                {
                }

                MainThread.BeginInvokeOnMainThread(async () => await fetchTabDataAndBind(_currentTab));
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
                //if (_isClickedNoneOfTheAboveOption == value) return;

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
        public Dictionary<string, string> NationalityMapping = null;
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

        private TaxpayerNationality _selectedPassportIssueCountry;
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
                if (_selectedPassportIssueHijiriDate == value) return;

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
                OutlettUiList.Clear();
                OutletData.Where(i => i.Actnm.StartsWith(_searchText)).ToList().ForEach(_out =>
                {
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
                    if (newItem.MciEntry.ToUpper().ToString().Equals("X"))
                    {
                        newItem.ShowEditIcon = false;
                    }
                    else
                    {
                        newItem.ShowEditIcon = true;
                    }


                    OutlettUiList.Add(newItem);
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
                if (value != null && value.Count > 0)
                {
                    _searchableOutletData = value;
                    OnPropertyChanged(nameof(SearchableOutletData));
                }
            }
        }

        private bool _showOutletList = false;
        public bool ShowOutletList
        {
            get => _showOutletList;
            set
            {
                if (_showOutletList == value) return;

                _showOutletList = value;
                OnPropertyChanged("ShowOutletList");
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
             {"Estimated Method", AppResources.NDEstimated }
        };
        private Dictionary<string, string> EnCalendarTypeList = new Dictionary<string, string>()
        {
            {"2", AppResources.Hijri },
            {"1", AppResources.Gregorian }
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

                if (value != null)
                {
                    _selectedMethod = value;
                    OnPropertyChanged(nameof(SelectedMethod));
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
                        if (PickerModel.PickerId == "nationalityPicker")
                        {
                            SelectedTaxpayerPDNationality = TaxpayerFullNationlityList?.Where(i => i.Landx50 == PickerModel.SelectedValue).FirstOrDefault();

                        }
                        if (PickerModel.PickerId == "citizenPicker")
                        {
                            SelectedCitizen = TaxpayerFullNationlityList?.Where(i => i.Landx50 == PickerModel.SelectedValue).FirstOrDefault();

                        }
                        if (PickerModel.PickerId == "residencyPicker")
                        {
                            SelectedResidence = TaxpayerFullNationlityList?.Where(i => i.Landx50 == PickerModel.SelectedValue).FirstOrDefault();

                        }
                        if (PickerModel.PickerId == "passportIssueCountryPicker")
                        {
                            SelectedPassportIssueCountry = TaxpayerFullNationlityList?.Where(i => i.Landx50 == PickerModel.SelectedValue).FirstOrDefault();

                        }

                        if (PickerModel.PickerId == "reportingBranchPicker")
                        {
                            SelectedReportingBranch = ReportingBranchList?.Where(i => i.authorizationGroup == PickerModel.SelectedValue).FirstOrDefault();

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
        private ObservableCollection<string> _outletList = new ObservableCollection<string>();
        public ObservableCollection<string> OutletList
        {
            get => _outletList;
            set
            {
                if (_outletList == value) return;

                if (value != null)
                {
                    _outletList = value;
                    OnPropertyChanged(nameof(OutletList));
                }
            }
        }
        private bool _isDeclarationBtnEnabled;
        public bool IsDeclarationBtnEnabled
        {
            get => _isDeclarationBtnEnabled;
            set
            {
                if (_isDeclarationBtnEnabled == value) return;

                _isDeclarationBtnEnabled = value;
                OnPropertyChanged(nameof(IsDeclarationBtnEnabled));
            }
        }
        private bool _eSTLedge = false;
        public bool ESTLedge
        {
            get => _eSTLedge;
            set
            {
                MessagingCenter.Send(this, "IsInstrunctionChecked", value);
                if (_eSTLedge == value) return;

                _eSTLedge = value;
                IsDeclarationBtnEnabled = _eSTLedge;
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
        #endregion

        #endregion

        public TaxPayerTypeAvailability RegTaxPayerTypeAvailability { get; set; }
        public TaxPayerPersonalDetailsAvailability TaxPayerDetailsAvailability { get; set; }
        public PassportDetails PassportDetails { get; set; }
        public FinancialDetails FinancialDetails { get; set; }


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

        public ICommand OnAppearingCommand { get; set; }
        public ICommand OnActivitiesButtonClick { get; set; }
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
        public ICommand OnEditOutletButtonClick { get; set; }
        public ICommand OnEditOutletButtonClick2 { get; set; }
        public ICommand OnNewOutletButtonClick { get; set; }
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
        public EstablishmentAmendUpdatePageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            RegTaxPayerTypeAvailability = new TaxPayerTypeAvailability();
            TaxPayerDetailsAvailability = new TaxPayerPersonalDetailsAvailability();
            FinancialDetails = new FinancialDetails();
            PassportDetails = new PassportDetails();
            OnActivitiesButtonClick = new Command(async (e) =>
            {
                try
                {
                    var newItem = e as Nreg_ActivityItem;
                    var dataModel = UtilityManager.FilterActivityDetails(taxPayerDetails, activityList, newItem);
                   await MopupService.Instance.PushAsync(new ActivitiesPopupPageView(dataModel, false), true);
                }
                catch (Exception)
                {
                }
            });

            OnNextButtonClick = new Command(async () => await navigateToNext(), () => CanExecute);
            OnPreButtonClick = new Command(() =>
            {
                if (currentTab == EstablishmentRegistrationTabsEnum.Unknown ||
                currentTab == EstablishmentRegistrationTabsEnum.RegistrationType)
                    _navigationService.GoBack();

                else if ((int)previousTab >= 2)
                {
                    var index = (int)previousTab - 1;
                    currentTab = (EstablishmentRegistrationTabsEnum)index;
                }
            });

            OnAppearingCommand = new Command(async () =>
            {
                await OnAppearing();
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
            OnEstablishmentRegistrationAttachmentTapped = new Command(async () => await OnRentAddAttachmentTapped());
            #endregion

            OnReportingBranchSelectButtonClick = new Command(async () =>
            {
                try
                {
                    List<string> reportingBranchData = new List<string>();

                    foreach (BranchesDropDownModel reportingBranch in ReportingBranchList)
                    {
                        if (!string.IsNullOrEmpty(reportingBranch.authorizationGroup) && !string.IsNullOrWhiteSpace(reportingBranch.authorizationGroup))
                        {
                            reportingBranchData.Add(reportingBranch.authorizationGroup);

                        }
                    }

                    GenericPickerModel genericPickerModel = new GenericPickerModel();
                    genericPickerModel.PickerData = reportingBranchData;
                    genericPickerModel.PickerId = "reportingBranchPicker";

                    await MopupService.Instance.PushAsync(new PickerPageView(genericPickerModel));
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

            #endregion

            #region TaxPayer Variable initialization

            GetGenderList();

            OnPDNatinalitySelectButtonClick = new Command(async () =>
            {
                try
                {
                    List<string> nationalityData = new List<string>();

                    foreach (TaxpayerNationality nationality in TaxpayerFullNationlityList)
                    {
                        if (!string.IsNullOrEmpty(nationality.Landx50) && !string.IsNullOrWhiteSpace(nationality.Landx50))
                        {
                            nationalityData.Add(nationality.Landx50);

                        }
                    }

                    GenericPickerModel genericPickerModel = new GenericPickerModel();
                    genericPickerModel.PickerData = nationalityData;
                    genericPickerModel.PickerId = "nationalityPicker";

                    await MopupService.Instance.PushAsync(new PickerPageView(genericPickerModel));
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

            OnPDCitizenSelectButtonClick = new Command(async () =>
            {
                try
                {
                    List<string> citizenData = new List<string>();

                    foreach (TaxpayerNationalityLandx50 citizen in TaxpayerPDNationlityList)
                    {
                        if (!string.IsNullOrEmpty(citizen.Landx50) && !string.IsNullOrWhiteSpace(citizen.Landx50))
                        {
                            citizenData.Add(citizen.Landx50);

                        }
                    }

                    GenericPickerModel genericPickerModel = new GenericPickerModel();
                    genericPickerModel.PickerData = citizenData;
                    genericPickerModel.PickerId = "citizenPicker";

                    await MopupService.Instance.PushAsync(new PickerPageView(genericPickerModel));
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

            OnPDResidenceSelectButtonClick = new Command(async () =>
            {
                try
                {
                    List<string> residenceData = new List<string>();

                    foreach (TaxpayerNationalityLandx50 residence in TaxpayerPDNationlityList)
                    {
                        if (!string.IsNullOrEmpty(residence.Landx50) && !string.IsNullOrWhiteSpace(residence.Landx50))
                        {
                            residenceData.Add(residence.Landx50);

                        }
                    }

                    GenericPickerModel genericPickerModel = new GenericPickerModel();
                    genericPickerModel.PickerData = residenceData;
                    genericPickerModel.PickerId = "residencyPicker";

                    await MopupService.Instance.PushAsync(new PickerPageView(genericPickerModel));
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
            #endregion

            #region Passport Variable Initialization

            OnPassportIssueCountryButtonClick = new Command(async () =>
            {
                try
                {
                    List<string> passportIssueCountryData = new List<string>();

                    foreach (TaxpayerNationality issueCountry in TaxpayerFullNationlityList)
                    {
                        if (!string.IsNullOrEmpty(issueCountry.Landx50) && !string.IsNullOrWhiteSpace(issueCountry.Landx50))
                        {
                            passportIssueCountryData.Add(issueCountry.Landx50);

                        }
                    }

                    GenericPickerModel genericPickerModel = new GenericPickerModel();
                    genericPickerModel.PickerData = passportIssueCountryData;
                    genericPickerModel.PickerId = "passportIssueCountryPicker";

                    await MopupService.Instance.PushAsync(new PickerPageView(genericPickerModel));
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

            OnPassportAttachmentTapped = new Command(async () => await OnPassportAddAttachmentButtonTapped());
            #endregion

            TappedOnAttachmentInformationIcon = new Command(() =>
            ShowValidationPopup(
                AppResources.ESTAttachmentSizeNotfication
                + Environment.NewLine
                + AppResources.ZZChooseonlyfilewithextensionForZAKAT
                + Environment.NewLine + AppResources.ZMaximumnoof5attachmentscanbeuploaded
                ));

            #region Outlet Tabs variable initialization
            OnEditOutletButtonClick = new Command(async (item) => await openEditOutlet(item as OuteltInfo_NestedListView, 1)); // Expander
            OnEditOutletButtonClick2 = new Command(async (item) => await openEditOutlet(item as OuteltInfo_NestedListView, 2)); //edit
            OnNewOutletButtonClick = new Command(async () => await openNewOutlet());
            OnDeleteOutletButtonClick = new Command(async (item) =>
            {
                var newItem = item as OuteltInfo_NestedListView;
                if (newItem.Oldmst.ToString().ToUpper().Equals("X"))
                {
                    await _dialogService.ShowMessage(AppResources.ExistingOutletError, AppResources.Information);
                }
                else if (newItem.MciEntry.ToString().ToUpper().Equals("X"))
                {
                    await _dialogService.ShowMessage(AppResources.DeleteError, AppResources.Information);
                }
                else if (newItem.ActNo.Equals("00000"))
                {
                    await _dialogService.ShowMessage(AppResources.MainOutletError, AppResources.Information); // Main ol cannot be deleted 

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
                    confirmPopup.OnSelect = async (str) =>
                    {
                        if (str == "Yes")
                        {
                           await deleteOutlet(item as OuteltInfo_NestedListView);
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
                poupWindow.OnItemSelect = (item) =>
                {
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
            OutletList.Clear();

            OnVoidOrSaveDraftClick = new Command(() =>
            {
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(new List<string> { AppResources.ZZSaveAsDraft, AppResources.ZZVoid, AppResources.FORM5CalendarType });
                poupWindow.OnItemSelect = async (item) =>
                {
                    var actionName = item as string;
                    if (actionName == AppResources.ZZSaveAsDraft)
                    {

                        IsLoading = true;
                        await MopupService.Instance.PopAsync(true);
                        try
                        {
                            if (SetDataForSaveDraft(_currentTab))
                            {
                                taxPayerDetails.Draftfg = "X";
                                taxPayerDetails.Gpart = App.LoginDataRetrieved.TIN;
                                taxPayerDetails.UserTypx = "TP";

                                if (taxPayerDetails.Augrp == null)
                                    taxPayerDetails.Augrp = string.Empty;


                                var _taxPayerDetails = await EstablishmentRegistrationWebServiceManager.ESTTaxPayerDetailPostService(taxPayerDetails);
                                if (_taxPayerDetails != null && !string.IsNullOrEmpty(_taxPayerDetails.Fbnumx))
                                {

                                    await MopupService.Instance.PushAsync(new SingleButtonPopupView(AppResources.ZZZOkayText, String.Format(AppResources.ZZZApplicationSaved, "" + _taxPayerDetails.Fbnumx), string.Empty), true);

                                }
                            }
                        }
                        catch (Exception e)
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(e.Message));
                        }
                        finally
                        {
                            IsLoading = false;
                        }
                    }
                    if (actionName == AppResources.ZZVoid)
                    {
                        await MopupService.Instance.PopAsync(true);
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
                                    _navigationService.GoBack();

                                }
                                catch (Exception e)
                                {
                                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(e.Message));
                                }
                                finally
                                {
                                    IsLoading = false;
                                }
                            }
                        };
                        await MopupService.Instance.PushAsync(voidNotePop);
                    }
                    if (actionName == AppResources.FORM5CalendarType)
                    {
                        await MopupService.Instance.PopAsync(true);
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
            TabList
            = new ObservableCollection<string>{ AppResources.ESTRegTaxTabTitleLabel, AppResources.ESTTaxpayerPersonalDetailsTabTitleLabel,
                AppResources.ESTPassportDetailsTabTitleLabel, AppResources.ESTOutletsTabTitleLabel,
                AppResources. VATRFinancialDetails, AppResources.ZVatSummary };
            SetUIAvailability();
            if (currentTab == EstablishmentRegistrationTabsEnum.Outlets)
            {
                await bindingOutletList();

                await fetchTabDataAndBind(currentTab);
            }
        }

        void SetUIAvailability()
        {
            try
            {
                switch (App.ZAKATType)
                {
                    case PageExecutionType.Amend:
                        RegTaxPayerTypeAvailability.ReportingBranch = false;
                        RegTaxPayerTypeAvailability.IsReportingBranchVisible = false;
                        RegTaxPayerTypeAvailability.EntityType = false;
                        RegTaxPayerTypeAvailability.TaxPayerType = false;
                        RegTaxPayerTypeAvailability.IsTaxPayerTypeVisible = false;
                        RegTaxPayerTypeAvailability.Nationality = false;
                        RegTaxPayerTypeAvailability.IsNationalityStatusVisible = true;
                        RegTaxPayerTypeAvailability.ResidencyStatus = true;

                        TaxPayerDetailsAvailability.DOB = true;
                        TaxPayerDetailsAvailability.Title = false;
                        TaxPayerDetailsAvailability.FirstName = false;
                        TaxPayerDetailsAvailability.LastName = true;
                        TaxPayerDetailsAvailability.FathersName = false;
                        TaxPayerDetailsAvailability.GrandFathersName = false;
                        TaxPayerDetailsAvailability.IsFamilyNameVisible = false;
                        TaxPayerDetailsAvailability.IsInitialVisible = false;
                        TaxPayerDetailsAvailability.IsGenderVisible = false;
                        TaxPayerDetailsAvailability.Nationality = false;
                        TaxPayerDetailsAvailability.IsNationalityVisible = false;
                        TaxPayerDetailsAvailability.Citizen = false;
                        TaxPayerDetailsAvailability.IsCitizenVisible = false;
                        TaxPayerDetailsAvailability.Residence = false;
                        TaxPayerDetailsAvailability.IsResidenceVisible = false;

                        PassportDetails.PassportNo = true;
                        PassportDetails.IssueCountry = true;
                        PassportDetails.IssueDate = true;
                        PassportDetails.ExpiryDate = true;
                        PassportDetails.Attachment = true;

                        FinancialDetails.FinancialRecords = true;
                        FinancialDetails.CalendarType = false;
                        FinancialDetails.FiscalMonthEnd = false;
                        FinancialDetails.FiscalDayEnd = false;
                        FinancialDetails.CommencementDate = false;
                        FinancialDetails.TaxableDate = false;
                        break;
                    case PageExecutionType.Update:
                        RegTaxPayerTypeAvailability.ReportingBranch = false;
                        RegTaxPayerTypeAvailability.IsReportingBranchVisible = true;
                        RegTaxPayerTypeAvailability.EntityType = false;
                        RegTaxPayerTypeAvailability.TaxPayerType = false;
                        RegTaxPayerTypeAvailability.IsTaxPayerTypeVisible = false;
                        RegTaxPayerTypeAvailability.Nationality = false;
                        RegTaxPayerTypeAvailability.IsNationalityStatusVisible = false;
                        RegTaxPayerTypeAvailability.ResidencyStatus = false;
                        RegTaxPayerTypeAvailability.IsResidencyStatusVisible = false;

                        TaxPayerDetailsAvailability.DOB = true;
                        TaxPayerDetailsAvailability.Title = false;
                        TaxPayerDetailsAvailability.FirstName = false;
                        TaxPayerDetailsAvailability.LastName = true;
                        TaxPayerDetailsAvailability.FathersName = false;
                        TaxPayerDetailsAvailability.GrandFathersName = false;
                        TaxPayerDetailsAvailability.FamilyName = false;
                        TaxPayerDetailsAvailability.Initial = false;
                        TaxPayerDetailsAvailability.Gender = true;
                        TaxPayerDetailsAvailability.IsFamilyNameVisible = true;
                        TaxPayerDetailsAvailability.IsInitialVisible = true;
                        TaxPayerDetailsAvailability.IsGenderVisible = true;
                        TaxPayerDetailsAvailability.Nationality = true;
                        TaxPayerDetailsAvailability.IsNationalityVisible = true;
                        TaxPayerDetailsAvailability.Citizen = true;
                        TaxPayerDetailsAvailability.IsCitizenVisible = true;
                        TaxPayerDetailsAvailability.Residence = true;
                        TaxPayerDetailsAvailability.IsResidenceVisible = true;

                        PassportDetails.PassportNo = false;
                        PassportDetails.IssueCountry = false;
                        PassportDetails.IssueDate = false;
                        PassportDetails.ExpiryDate = false;
                        PassportDetails.Attachment = false;

                        FinancialDetails.FinancialRecords = false;
                        FinancialDetails.CalendarType = false;
                        FinancialDetails.FiscalMonthEnd = false;
                        FinancialDetails.FiscalDayEnd = false;
                        FinancialDetails.CommencementDate = false;
                        FinancialDetails.TaxableDate = false;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
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
                    if (await PushDatatoServer(currentTab))
                    {
                        currentTab = EstablishmentRegistrationTabsEnum.Declaration;
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

                        if ((isDraftEnabled == "X") || (isFinaceDetailsChanged))
                        {

                            var somewarningpopup = new AttachmentInformationPopUp(AppResources.AmendRegistrationSubmitWarning)
                            {
                                CloseWhenBackgroundIsClicked = false
                            };
                            somewarningpopup.OnDone = async () =>
                            {
                                if (await PushDatatoServer(currentTab))
                                {
                                    await _navigationService.NavigateTo(App.EstablishmentAmendUpdateSuccessfulPage, taxPayerDetails);
                                }


                            };
                            await MopupService.Instance.PushAsync(somewarningpopup);
                        }
                        else
                        {

                            if (await PushDatatoServer(currentTab))
                            {
                                await _navigationService.NavigateTo(App.EstablishmentAmendUpdateSuccessfulPage, taxPayerDetails);
                            }
                        }





                    }
                }

                SetUIAvailability();
                previousTab = _currentTab;
            }
            catch (Exception)
            {
            }
            finally
            {
                CanExecute = true;
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
                string[] filetypes = DependencyService.Get<ZATCAMAUI.Core.Interfaces.IDeviceInfoZATCA>().GetAttachmentTypeStringForTaxEvasion();

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
                    float sizemb = attachmentByte.Length / 1024f / 1024f;
                    decimal attachmentSize = 0;
                    attachmentSize = attachmentSize + (decimal)sizemb;
                    if (!isFileAlreayUploaded)
                    {
                        if (fileData.FileName.Contains("."))
                        {
                            //string Extention = fileData.FileName.Split('.')[1];//pdf
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
            {
            }
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
            {
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void OnExpandCollapseGridViewClick(object _enum)
        {
            SummaryExpendedCard = (EstablishmentRegistrationTabsEnum)_enum;
        }

        public async Task fetchTabDataAndBind(EstablishmentRegistrationTabsEnum _enum)
        {
            clearFormData(_enum);
            try
            {
                IsLoading = true;
                NationalityMapping = new Dictionary<string, string>()
                     {
                        { "SAUDI", AppResources.ESTNationalitySAUDI },
                        { "GCC", AppResources.ESTNationalityGCC },
                        { "FOREIGN", AppResources.ESTNationalityFOREIGN }
                    };
                if (_enum == EstablishmentRegistrationTabsEnum.RegistrationType)
                {
                    await GetReportingBranchListFromServer();
                    taxPayerDetails = await EstablishmentRegistrationWebServiceManager.ESTTaxPayerDetailGetService("01", App.LoginDataRetrieved.TIN, App.LoginDataRetrieved.Emailid);
                    if (!string.IsNullOrEmpty(taxPayerDetails?.Fbsta) && taxPayerDetails?.Fbsta != "IP011")
                    {
                        string message = string.Empty;
                        message = AppResources.ZDearTaxpayerZakatSubmitMessage1 + " " + taxPayerDetails.Fbnumx + " " + AppResources.ZDearTaxpayerZakatSubmitMessage2;
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(message));
                        _navigationService.GoBack();
                    }

                    if (!string.IsNullOrEmpty(taxPayerDetails?.Atype))
                    {
                        if (taxPayerDetails?.Atype == "Company")
                        {
                            try
                            {


                                var VisitPortalPopup = new ReturnPortalNavigationPopUp(AppResources.PleaseVisitGAZTPortalToChangeTheRegistrationType)
                                {
                                    CloseWhenBackgroundIsClicked = false
                                };
                                VisitPortalPopup.OnDone = () =>
                                {

                                    _navigationService.GoBack();

                                };

                                if (App.IsArabic)
                                {
                                    VisitPortalPopup.OnGotoPortal = () =>
                                    {
                                        _navigationService.GoBack();
                                        Launcher.OpenAsync(ZATCAConstants.GAZTVisitPortalUrlAR);

                                    };
                                }
                                else
                                {
                                    VisitPortalPopup.OnGotoPortal = () =>
                                    {
                                        _navigationService.GoBack();
                                        Launcher.OpenAsync(ZATCAConstants.GAZTVisitPortalUrlEN);

                                    };
                                }


                                await MopupService.Instance.PushAsync(VisitPortalPopup);
                            }
                            catch (Exception)
                            {


                            }


                            return;
                        }
                    }

                    SelectedReportingBranch = ReportingBranchList.Where(i => i.authorizationGroup == taxPayerDetails?.Augrp).FirstOrDefault();
                    SelectedEntityType = AppResources.ESTSelectedEntityTypeLabel;
                    SelectedTaxPayerType = AppResources.ESTSelectedTaxPayerType;
                    if (taxPayerDetails?.Tpnationality == "SAUDI")
                        SelectedRegNationalityType = AppResources.ESTNationalitySAUDI;
                    else if (taxPayerDetails?.Tpnationality == "GCC")
                        SelectedRegNationalityType = AppResources.ESTNationalityGCC;
                    else if (taxPayerDetails?.Tpnationality == "FOREIGN")
                        SelectedRegNationalityType = AppResources.ESTNationalityFOREIGN;
                    if (!string.IsNullOrEmpty(taxPayerDetails?.Tpnationality))
                    {
                        if (!NationalityMapping.ContainsKey(taxPayerDetails?.Tpnationality) || ReportingBranchList?.Count == 0)
                        {
                            var someThingWhentWrong = new AttachmentInformationPopUp(AppResources.Somethingwentwrong)
                            {
                                CloseWhenBackgroundIsClicked = false
                            };
                            someThingWhentWrong.OnDone = async () =>
                            {
                                currentTab = EstablishmentRegistrationTabsEnum.Unknown;
                                await _navigationService.NavigateTo(App.GAZTNewDesignDashBoardPageView);
                            };
                            await MopupService.Instance.PushAsync(someThingWhentWrong);
                            return;
                        }
                    }
                    IsSaudi = taxPayerDetails?.Tpnationality == "SAUDI";
                    if (IsSaudi)
                    {
                        TabList.Remove(AppResources.ESTPassportDetailsTabTitleLabel);
                        //taxPayerDetails.Tpresidence = "3";
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
                    if (App.IsArabic)
                    {
                        GCCIDType = idItem != null ? ArIDType[idItem?.Type] : "";
                    }
                    else
                    {
                        GCCIDType = idItem != null ? EnIDType[idItem?.Type] : "";
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
                    GCCIDTypeIdNumberValue = idItem?.Idnumber;
                    SelectedDOB = Convert.ToDateTime(taxPayerDetails?.Birthdt).ToString("yyyy/MM/dd", new CultureInfo("en-US"));
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
                    SelectedTaxpayerPDNationality = TaxpayerFullNationlityList?.Where(i => i.Land1 == taxPayerDetails?.Natio).FirstOrDefault();
                    SelectedCitizen = TaxpayerFullNationlityList?.Where(i => i.Land1 == taxPayerDetails?.Citizen).FirstOrDefault();
                    SelectedResidence = TaxpayerFullNationlityList?.Where(i => i.Land1 == taxPayerDetails?.Residence).FirstOrDefault();

                }
                else if (_enum == EstablishmentRegistrationTabsEnum.PassportDetails)
                {
                    taxPayerDetails = await EstablishmentRegistrationWebServiceManager.ESTTaxPayerDetailGetService("02", App.LoginDataRetrieved.TIN, App.LoginDataRetrieved.Emailid);
                    Nreg_IdItem passportItem = taxPayerDetails?.Nreg_IdSet?.Where(i => i.Type == "FS0002").FirstOrDefault();
                    PassportNumber = passportItem?.Idnumber;
                    SelectedPassportIssueCountry = TaxpayerFullNationlityList?.Where(i => i.Land1 == passportItem?.Country).FirstOrDefault();
                    PassportIssueDate = Convert.ToDateTime(passportItem?.ValidDateFrom).ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                    PassportExpireDate = Convert.ToDateTime(passportItem?.ValidDateTo).ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                    PassportAttachmentPrepopulateCheck(taxPayerDetails);
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.Outlets)
                {
                    IsLoading = true;
                    await bindingOutletList();
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.FinancialDetail)
                {
                    taxPayerDetails = await EstablishmentRegistrationWebServiceManager.ESTTaxPayerDetailGetService("04", App.LoginDataRetrieved.TIN, App.LoginDataRetrieved.Emailid, null, taxPayerDetails?.Fbnumx);
                    EnMethodList = new Dictionary<string, string>()
                    {
                        {"Accounting Method", AppResources.NDAccounting },
                        {"Estimated Method", AppResources.NDEstimated }
                    };
                    EnCalendarTypeList = new Dictionary<string, string>()
                    {
                        {"Hijri", AppResources.Hijri },
                        {"Gregorian", AppResources.Gregorian }
                    };
                    MethodList.Clear();
                    MethodList.AddRange(EnMethodList.Values);
                    MethodList = new List<string>(MethodList);
                    CalendarTypeList.Clear();
                    CalendarTypeList.AddRange(EnCalendarTypeList.Values);
                    CalendarTypeList = new List<string>(CalendarTypeList);
                    //TODO recheck
                    SelectedMethod = EnMethodList?[taxPayerDetails?.Accmethod];

                    if (SelectedMethod == AppResources.NDAccounting)
                    {

                        IsFinancePeriodVisible = true;
                    }
                    else
                    {
                        IsFinancePeriodVisible = false;

                    }
                    CalendarType = EnCalendarTypeList?[taxPayerDetails?.Fdcalender];
                    await udpdateDates();
                }
                if (IsSaudi)
                {
                    TabList.Remove(AppResources.ESTPassportDetailsTabTitleLabel);
                }
            }
            catch (GAZTErrorException e)
            {
                IsExceptionPopupVisible = true;
                await MopupService.Instance.PushAsync(new SingleButtonPopupView(AppResources.ZZZZDone, e.Message, AppResources.Information));
            }
            catch (Exception)
            {
            }
            finally
            {
                IsLoading = false;
                updateDatePickers(_enum);
            }
        }

        private void updateDatePickers(EstablishmentRegistrationTabsEnum _enum)
        {
            DateTime dob = DateTime.Now;
            ObservableCollection<object> _selectedDOBDate = new ObservableCollection<object>();
            if (taxPayerDetails != null && string.IsNullOrWhiteSpace(taxPayerDetails?.Caltp))
                taxPayerDetails.Caltp = "Gregorian";

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

                if (taxPayerDetails?.Caltp == "Gregorian")
                {
                    SelectedDOBDate = _selectedDOBDate;
                    var date = DateTimeHelper.DateTimeFormater(SelectedDOB);
                    if (!string.IsNullOrWhiteSpace(SelectedDOB))
                        DisplaySelectedDOB = date.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                }
                else
                {
                    SelectedDOBHijiriDate = _selectedDOBDate;
                    if (!string.IsNullOrWhiteSpace(SelectedDOB))
                        DisplaySelectedDOB = DateTimeHelper.ConvertToUmAlQuraHigriDate(SelectedDOB).Item2;
                }
            }
            else if (_enum == EstablishmentRegistrationTabsEnum.PassportDetails)
            {

                if (taxPayerDetails?.Caltp == "Gregorian")
                {
                    SelectedPassportIssueDate = _selectedDOBDate;
                    var _issueDate = DateTimeHelper.DateTimeFormater(PassportIssueDate);

                    if (!string.IsNullOrWhiteSpace(PassportIssueDate))
                        DisplayPassportIssueDate = _issueDate.ToString("yyyy/MM/dd", new CultureInfo("en-US"));

                    SelectedPassportExpireDate = _selectedDOBDate;
                    var _expiryDate = DateTimeHelper.DateTimeFormater(PassportExpireDate);
                    if (!string.IsNullOrWhiteSpace(PassportExpireDate))
                        DisplayPassportExpireDate = _expiryDate.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                }
                else
                {
                    SelectedPassportIssueHijiriDate = _selectedDOBDate;
                    if (!string.IsNullOrWhiteSpace(PassportIssueDate))
                        DisplayPassportIssueDate = DateTimeHelper.ConvertToUmAlQuraHigriDate(PassportIssueDate).Item2;

                    SelectedPassportExpireHijiriDate = _selectedDOBDate;
                    if (!string.IsNullOrWhiteSpace(PassportExpireDate))
                        DisplayPassportExpireDate = DateTimeHelper.ConvertToUmAlQuraHigriDate(PassportExpireDate).Item2;
                }
            }
        }

        private async Task udpdateDates(string selectedDate = null)
        {
            try
            {
                IsLoading = true;
                var _CalendarType = EnCalendarTypeList.FirstOrDefault(i => i.Value == CalendarType).Key == "Hijri" ? "Hijri" : "Gregorian";
                financialDetail = await EstablishmentRegistrationWebServiceManager.ESTFinancialMaxDate(new FinancialDetailRequest()
                {
                    ACaltype = _CalendarType,
                    ADateComm = taxPayerDetails?.Commdt
                });
                if (_CalendarType == "Hijri")
                {
                    string dd = financialDetail?.ACommDate.Substring(8, 2);
                    string mm = financialDetail?.ACommDate.Substring(5, 2);
                    string yy = financialDetail?.ACommDate.Substring(0, 4);
                    CommDate = $"{Int16.Parse(yy) - 1:0000}/{Int16.Parse(mm):00}/{Int16.Parse(dd):00}";
                    CommDate = Convert.ToDateTime(financialDetail?.ACommDate).ToString("yyyy/mm/dd");
                }
                else
                {
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

                if (taxPayerDetails.LastFilledRetdt != null)
                {
                    var toDay = string.Empty;
                    if (_CalendarType == "Hijri")
                    {

                        LastFulfilledReturn = taxPayerDetails.LastFilledRetdt;   //?.ToString("yyyy/MM/dd", new CultureInfo("ar-sa"));


                    }
                    else
                    {

                        LastFulfilledReturn = taxPayerDetails.LastFilledRetdt;   //?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));

                    }

                }
                if (taxPayerDetails.Zyear != null)
                {

                    ZYear = AppResources.FinacialDetailsZyear.Replace("yyyy", taxPayerDetails.Zyear);

                }
                if (string.IsNullOrEmpty(financialDetail?.EIslmedate) /*&& (taxPayerDetails?.Fdcalender == "2")*/)
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
                IsLoading = true;

                var selectedFintype = "";

                if (!string.IsNullOrEmpty(SelectedMethod))
                {
                    selectedFintype = EnMethodList.FirstOrDefault(i => i.Value == SelectedMethod).Key;
                }
                else
                {
                    selectedFintype = taxPayerDetails.Accmethod;
                }



                financialDetailPeriod = await EstablishmentRegistrationWebServiceManager.ESTFinancialMaxDateForPeriod(new FinancialDetailPeriodRequest()
                {
                    ACaltype = _CalendarType,
                    AMonth = FiscalMonth,
                    EIslmedate = FiscalDay == AppResources.ESTFinLastDay ? "32" : FiscalDay,
                    ADateComm = taxPayerDetails?.Commdt,
                    Gpart = App.LoginDataRetrieved.TIN,
                    Zfintype = selectedFintype,
                    PeriodSet = new List<PeriodSetResult>()
                });

                if (financialDetailPeriod != null)
                {

                    var fincialPeriodDetials = financialDetailPeriod.PeriodSet;

                    foreach (var s in fincialPeriodDetials)
                    {
                        var fromDate = string.Empty;
                        var toDay = string.Empty;
                        if (_CalendarType == "Hijri")
                        {

                            fromDate = s.FromDate; //.ToString("yyyy/MM/dd", new CultureInfo("ar-sa"));
                            toDay = s.ToDate; //.ToString("yyyy/MM/dd", new CultureInfo("ar-sa"));

                        }
                        else
                        {

                            fromDate = s.FromDate; //.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                            toDay = s.ToDate; //.ToString("yyyy/MM/dd", new CultureInfo("en-US"));

                        }

                        s.ConvretedFromDate = fromDate;
                        s.ConvretedToDate = toDay;


                    }

                    PeriodList = fincialPeriodDetials;
                    TaxDate = String.Empty;
                    PeriodList = financialDetailPeriod.PeriodSet;
                    isDraftEnabled = financialDetailPeriod.Draft;
                    if (PeriodList.Count > 0)
                    {

                        if (!string.IsNullOrEmpty(taxPayerDetails.FinPeriod))
                        {
                            SelectedPeriod = PeriodList.Where(temp => (temp.FinPeriod == taxPayerDetails.FinPeriod)).FirstOrDefault();

                            TaxDate = SelectedPeriod.ConvretedToDate;

                        }
                    }
                    else
                    {
                        TaxDate = financialDetailPeriod.EIsldate;
                    }


                }


                IsLoading = false;
            }
            catch (Exception)
            {
                IsLoading = false;
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Somethingwentwrong));
            }
        }

        private async Task bindingOutletList()
        {
            try
            {
                await GetPdNationalityListFromServer(taxPayerDetails?.Tpnationality);

                IsLoading = true;

                taxPayerDetails = await EstablishmentRegistrationWebServiceManager.ESTTaxPayerDetailGetService("02", App.LoginDataRetrieved.TIN, App.LoginDataRetrieved.Emailid);
                idItem = taxPayerDetails?.Nreg_IdSet.Where(i => EnIDType.ContainsKey(i.Type)).FirstOrDefault();
                if (App.IsArabic)
                {
                    GCCIDType = idItem != null ? ArIDType[idItem?.Type] : "";
                }
                else
                {
                    GCCIDType = idItem != null ? EnIDType[idItem?.Type] : "";
                }
                GCCIDTypeIdNumberValue = idItem?.Idnumber;
                SelectedDOB = taxPayerDetails?.Birthdt;//?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
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
                SelectedTaxpayerPDNationality = TaxpayerFullNationlityList?.Where(i => i.Land1 == taxPayerDetails?.Natio).FirstOrDefault();
                SelectedCitizen = TaxpayerFullNationlityList?.Where(i => i.Land1 == taxPayerDetails?.Citizen).FirstOrDefault();
                SelectedResidence = TaxpayerFullNationlityList?.Where(i => i.Land1 == taxPayerDetails?.Residence).FirstOrDefault();

                IsLoading = true;

                var _outletTempData = await EstablishmentRegistrationWebServiceManager.ESTOutletList(taxPayerDetails?.PortalUsrx, App.LoginDataRetrieved.TIN, taxPayerDetails?.Fbnumx);

                OutletData.Clear();
                SearchableOutletData?.Clear();
                OutlettUiList.Clear();
                _outletTempData.ForEach(_out =>
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
                    newItem.ShowEditIcon = true;
                    newItem.ShowDeleteIcon = true;

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
                });
                ShowOutletList = true;
                IsLoading = false;
            }
            catch (GAZTErrorException ex)
            {
                IsLoading = false;
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
            }
            catch (Exception)
            {
                IsLoading = false;


            }
        }

        private async Task openEditOutlet(OuteltInfo_NestedListView item, int btnCode)
        {
            try
            {
                if (btnCode == 1)
                {
                    if (item != null)
                    {
                        if (item.ActNo == item.ActNo && item.IsInnerListVisible == true)
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
                SelectedItem = item;
                var index = OutlettUiList.IndexOf(item);
                IsLoading = true;
                OutletNavigationModels outletNavigationModels = new OutletNavigationModels();
                outletNavigationModels.taxPayerDetails = taxPayerDetails;
                var OutletActNumber = (item.ActNo == null || string.IsNullOrEmpty(item?.ActNo)) ? "00000" : item.ActNo;
                item.ContactDetails = new ObservableCollection<Nreg_ActivityItem>();
                item.ContactDetails2 = new ObservableCollection<Nreg_ActivityItem>();
                OutletDropDowns = await EstablishmentRegistrationWebServiceManager.ESTOutletDropDowns();
                activityList = await EstablishmentRegistrationWebServiceManager.ESTOutletGetActivitySetsList();
                taxPayerDetails = await EstablishmentRegistrationWebServiceManager.ZakatAmendESTTaxPayerDetailGetService("03", App.LoginDataRetrieved.TIN, App.LoginDataRetrieved.Emailid, OutletActNumber,
                    taxPayerDetails?.Fbnumx, taxPayerDetails?.Fbstax, taxPayerDetails?.Fbustx);
                item = await PrepareUIBranchesList(item);
                MainThread.BeginInvokeOnMainThread(() => {
                    if (item.ContactDetails.Count > 0)
                    {
                        ShowCRNoData = false;
                    }
                    else
                    {
                        ShowCRNoData = true;
                    }
                    if (item.ContactDetails2.Count > 0)
                    {
                        ShowLicenceNoData = false;
                    }
                    else
                    {
                        ShowLicenceNoData = true;
                    }
                    IsLoading = false;

                });
                if (btnCode == 2)
                {
                    outletNavigationModels.idItem = idItem;
                    outletNavigationModels.selectedOutletItem = GetSelectedItem(item);
                    outletNavigationModels.IsEditingMode = true;
                    outletNavigationModels.openedTab = EstablishmentRegistrationOutletTabsEnum.OutletDetail;
                    await _navigationService.NavigateTo(App.OutletDetailsAmendUpdatePageView, outletNavigationModels);
                }
            }
            catch (Exception)
            {
            }
        }

        private async Task openNewOutlet()
        {
            OutletNavigationModels outletNavigationModels = new OutletNavigationModels();
            outletNavigationModels.taxPayerDetails = taxPayerDetails;
            outletNavigationModels.selectedOutletItem = null;
            outletNavigationModels.idItem = idItem;
            outletNavigationModels.IsEditingMode = false;
            await _navigationService.NavigateTo(App.OutletDetailsAmendUpdatePageView, outletNavigationModels);
        }

        private async Task deleteOutlet(OuteltInfo_NestedListView item)
        {
            IsLoading = true;
            var delete = EstablishmentRegistrationWebServiceManager.ESTDeleteOutletItem(taxPayerDetails?.Fbnumx, item?.ActNo, taxPayerDetails?.PortalUsrx).GetAwaiter().GetResult(); ;
            if (!string.IsNullOrEmpty(delete) && delete == "delete")
            {
                await bindingOutletList();
            }
            IsLoading = false;
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
            return newItem;
        }

        private async Task<OuteltInfo_NestedListView> PrepareUIBranchesList(OuteltInfo_NestedListView outletItem)
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
                    obj.Z700Number = item.Z700Number;

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

                    var resultAct = activityList.activitySet.Where(i => i.IndSector == obj.Activity).FirstOrDefault();
                    var resultMaingrp = activityList.act_groupSet.Where(i => i.IndSector == obj.ActMgrp).FirstOrDefault();
                    var resultSubGrp = activityList.act_subgroupSet.Where(i => i.IndSector == obj.ActSgrp).FirstOrDefault();
                    var resultCity = OutletDropDowns.city_dropdownSet.Where(i => i.CityCode == obj.CityCode).FirstOrDefault();
                    var resultCountry = OutletDropDowns.country_dropdownSet.Where(i => i.Land1 == obj.Country).FirstOrDefault();

                    if (resultAct != null)
                    {
                        obj.ActivityDesc = resultAct.Text;
                    }
                    if (resultMaingrp != null)
                    {
                        obj.ActMgrpDesc = resultMaingrp.Text;
                    }
                    if (resultSubGrp != null)
                    {
                        obj.ActSgrpDesc = resultSubGrp.Text;
                    }
                    if (resultCity != null)
                    {
                        obj.IssuedCity = resultCity.CityName;
                    }
                    if (resultAct != null)
                    {
                        obj.IssuedCountry = resultCountry.Landx;
                    }

                    if (App.IsArabic)
                    {

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
            }
            catch (Exception)
            {

            }

            return outletItem;
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

        public async Task<bool> ValidateIDAndDOB(string IDType, string IDNumber, string DOB)
        {
            IsLoading = true;
            string Result = await TaxEvasionWebServiceManager.GAZTVATSignUpValidateIDTypesStringResp(IDType, IDNumber, DOB);
            IsLoading = false;
            VATSignUp vATSignUpData = new VATSignUp();
            vATSignUpData = JsonConvert.DeserializeObject<VATSignUp>(Result);
            if (vATSignUpData.d == null)
            {
                IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                }
                else
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                }
                IsLoading = false;
                return false;
            }
            else
            {
                IsLoading = false;
                return true;
            }
        }

        private async Task<bool> FormValidation(EstablishmentRegistrationTabsEnum _enum)
        {
            try
            {
                if (_enum == EstablishmentRegistrationTabsEnum.RegistrationType)
                {
                    if (RegTaxPayerTypeAvailability.ReportingBranch && (SelectedReportingBranch == null || string.IsNullOrWhiteSpace(SelectedReportingBranch.branchDescription)))
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateBranch));
                        return false;
                    }
                    if (!IsSaudi)
                    {
                        if (RegTaxPayerTypeAvailability.ResidencyStatus && string.IsNullOrWhiteSpace(SelectedTpresidence))
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateResidenceType));
                            return false;
                        }
                        else if (RegTaxPayerTypeAvailability.ResidencyStatus && SelectedTpresidence == "2" && (UploadedRentDocumentsList == null || UploadedRentDocumentsList.Count <= 0))
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAttachRent));
                            return false;
                        }
                        else if (RegTaxPayerTypeAvailability.ResidencyStatus && SelectedTpresidence == "3" && string.IsNullOrWhiteSpace(SelectedOrgNonResident))
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateLegalEntity));
                            return false;
                        }
                        else if (RegTaxPayerTypeAvailability.ResidencyStatus && SelectedTpresidence == "3" && SelectedOrgNonResident == "1" && string.IsNullOrWhiteSpace(SelectedOrgNonResidentOptions))
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateParmanentEst));
                            return false;
                        }
                        else if (RegTaxPayerTypeAvailability.ResidencyStatus && SelectedTpresidence == "3" && SelectedOrgNonResident == "2" && string.IsNullOrWhiteSpace(SelectedOrgNonResidentActivity))
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateOtherTax));
                            return false;
                        }
                    }
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.TaxpayerDetail)
                {

                    if (TaxPayerDetailsAvailability.DOB && string.IsNullOrWhiteSpace(SelectedDOB))
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateDOB));
                        return false;
                    }
                    else if (TaxPayerDetailsAvailability.FirstName && string.IsNullOrWhiteSpace(FirstName))
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateFirstName));
                        return false;
                    }
                    else if ((TaxPayerDetailsAvailability.IsGenderVisible || TaxPayerDetailsAvailability.Gender) && string.IsNullOrWhiteSpace(SelectedGender))
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateGender));
                        return false;
                    }
                    else if ((TaxPayerDetailsAvailability.IsNationalityVisible || TaxPayerDetailsAvailability.Nationality) && null == SelectedTaxpayerPDNationality)
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateNationality));
                        return false;
                    }
                    else if ((TaxPayerDetailsAvailability.IsCitizenVisible || TaxPayerDetailsAvailability.Citizen) && null == SelectedCitizen)
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateCitizen));
                        return false;
                    }
                    else if ((TaxPayerDetailsAvailability.IsResidenceVisible || TaxPayerDetailsAvailability.Residence) && null == SelectedResidence)
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateResidence));
                        return false;
                    }
                    if (TaxPayerDetailsAvailability.DOB && !string.IsNullOrWhiteSpace(SelectedDOB))
                    {
                        if (DateTime.TryParse(SelectedDOB, out DateTime parsedDate))
                        {
                            // Format the DateTime object to display only the date part
                            string formattedDate = parsedDate.ToString("yyyy-MM-dd");

                            var result = await ValidateIDAndDOB(idItem?.Type, GCCIDTypeIdNumberValue, formattedDate);
                            if (!result)
                            {
                                return false;
                            }
                        }
                    }
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.PassportDetails)
                {
                    if (PassportDetails.PassportNo && string.IsNullOrWhiteSpace(PassportNumber))
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePassportNumber));
                        return false;
                    }
                    else if (PassportDetails.IssueCountry && null == SelectedPassportIssueCountry)
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePIssueCountry));
                        return false;
                    }
                    else if (PassportDetails.IssueDate && string.IsNullOrWhiteSpace(PassportIssueDate))
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePIssueDate));
                        return false;
                    }
                    else if (PassportDetails.ExpiryDate && string.IsNullOrWhiteSpace(PassportExpireDate))
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePExpiryDate));
                        return false;
                    }
                    else if (PassportDetails.Attachment && (UploadedPassportDocumentsList == null || UploadedPassportDocumentsList.Count <= 0))
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePAttachCopy));
                        return false;
                    }
                    else if (PassportDetails.PassportNo)
                    {
                        DateTime dob = new DateTime();
                        DateTime issue = new DateTime();
                        DateTime expiry = new DateTime();

                        issue = DateTimeHelper.DateTimeFormater(PassportIssueDate);
                        expiry = DateTimeHelper.DateTimeFormater(PassportExpireDate);

                        if (taxPayerDetails?.Caltp == "Gregorian")
                        {
                            dob = DateTimeHelper.DateTimeFormater(SelectedDOB);

                        }
                        else
                        {
                            dob = DateTimeHelper.ConvertToUmAlQuraHigriDate(SelectedDOB).Item1;
                        }

                        if (DateTime.Compare(issue.Date, dob.Date) < 0)
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PassportIssueDisclaimer));
                            return false;
                        }
                        else if (DateTime.Compare(expiry.Date, dob.Date) < 0)
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PassportExpiryDisclaimer));
                            return false;
                        }
                        else if (DateTime.Compare(expiry.Date, issue.Date) < 0)
                        {
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PassportDisclaimer));
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
            { }
            return true;
        }

        private async Task<bool> PushDatatoServer(EstablishmentRegistrationTabsEnum _enum)
        {
            try
            {
                IsLoading = true;
                if (_enum == EstablishmentRegistrationTabsEnum.RegistrationType)
                {
                    if (SelectedReportingBranch?.authorizationGroup != null)
                    {
                        taxPayerDetails.Augrp = SelectedReportingBranch?.authorizationGroup;
                    }

                    taxPayerDetails.Atype = string.IsNullOrEmpty(SelectedEntityType) ? string.Empty : SelectedEntityType;
                    taxPayerDetails.Tpnationality = NationalityMapping.Where(i => i.Value == SelectedRegNationalityType).FirstOrDefault().Key;
                    taxPayerDetails.Tpnationality = taxPayerDetails.Tpnationality == null ? "" : taxPayerDetails.Tpnationality;
                    taxPayerDetails.Taxtpdetermination = "1";
                    if (!SelectedTpresidence.Equals(""))
                    {
                        taxPayerDetails.Tpresidence = SelectedTpresidence;
                    }
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
                    DateTime dob = new DateTime();
                    if (taxPayerDetails?.Caltp == "Gregorian")
                    {
                        dob = DateTimeHelper.DateTimeFormater(SelectedDOB);

                    }
                    else
                    {
                        dob = DateTimeHelper.ConvertToUmAlQuraHigriDate(SelectedDOB).Item1;
                    }

                    taxPayerDetails.Birthdt = dob.ToString("yyyy-MM-ddThh:mm:ss", new CultureInfo("en-US"));
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
                    taxPayerDetails.Natio = SelectedTaxpayerPDNationality?.Land1 == null ? "" : SelectedTaxpayerPDNationality?.Land1;
                    taxPayerDetails.Citizen = SelectedCitizen?.Land1 == null ? "" : SelectedCitizen.Land1;
                    taxPayerDetails.Residence = SelectedResidence?.Land1 == null ? "" : SelectedResidence?.Land1;

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
                    passportObj.Idnumber = PassportNumber == null ? "" : PassportNumber;
                    passportObj.Country = SelectedPassportIssueCountry?.Land1 == null ? "" : SelectedPassportIssueCountry?.Land1;
                    var issueDate = DateTimeHelper.DateTimeFormater(PassportIssueDate);

                    var expireDate = DateTimeHelper.DateTimeFormater(PassportExpireDate);

                    passportObj.ValidDateFrom = issueDate.ToString("yyyy-MM-ddThh:mm:ss", new CultureInfo("en-US"));
                    passportObj.ValidDateTo = expireDate.ToString("yyyy-MM-ddThh:mm:ss", new CultureInfo("en-US"));
                    passportObj.Type = "FS0002";
                    passportObj.Srcidentify = "00000";

                    taxPayerDetails.Nreg_IdSet.Clear();
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

                    if (string.IsNullOrWhiteSpace(taxPayerDetails.Birthdt))
                    {
                        DateTime dob = new DateTime();
                        if (taxPayerDetails?.Caltp == "Gregorian")
                        {
                            dob = DateTimeHelper.DateTimeFormater(SelectedDOB);

                        }
                        else
                        {
                            dob = DateTimeHelper.ConvertToUmAlQuraHigriDate(SelectedDOB).Item1;
                        }
                        taxPayerDetails.Birthdt = dob.ToString("yyyy-MM-ddThh:mm:ss", new CultureInfo("en-US"));
                    }

                    var taxPayerDetailsResult = await EstablishmentRegistrationWebServiceManager.ESTTaxPayerDetailPostService(taxPayerDetails);

                    IsLoading = false;
                    return true;
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.FinancialDetail)
                {

                    if (taxPayerDetails.Fdenddt == null)
                    {
                        DateTime.TryParseExact(TaxDate, string.Format("{0:0000/00/00}", Int64.Parse(financialDetail?.EIsldate)), new CultureInfo("en-US"), DateTimeStyles.None, out DateTime Fdenddt);

                        taxPayerDetails.Fdenddt = Fdenddt.ToString();
                    }
                    else
                    {
                        taxPayerDetails.Fdenddt = taxPayerDetails.Fdenddt;

                    }
                    taxPayerDetails.Accmethod = EnMethodList.FirstOrDefault(i => i.Value == SelectedMethod).Key;
                    taxPayerDetails.Accmethod = taxPayerDetails.Accmethod == null ? "" : taxPayerDetails.Accmethod;
                    taxPayerDetails.Fdcalender = EnCalendarTypeList.FirstOrDefault(i => i.Value == CalendarType).Key;
                    taxPayerDetails.Fdcalender = taxPayerDetails.Fdcalender == null ? "" : taxPayerDetails.Fdcalender;
                    taxPayerDetails.Fdmonth = FiscalMonth == null ? "" : FiscalMonth;
                    taxPayerDetails.Fdday = FiscalDay == AppResources.ESTFinLastDay ? "LD" : FiscalDay;
                    taxPayerDetails.Commdt = financialDetail?.ACommDate;
                    taxPayerDetails.Chkfg = "X";

                    if (SelectedPeriod != null)
                    {
                        taxPayerDetails.FinPeriod = SelectedPeriod.FinPeriod;
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
                    taxPayerDetails.Acsactivitydet = "X";
                    taxPayerDetails.Acscontactper = "X";
                    taxPayerDetails.Mandt = "330";
                    taxPayerDetails = await EstablishmentRegistrationWebServiceManager.ESTTaxPayerDetailPostService(taxPayerDetails);

                    IsLoading = false;
                    return true;
                }

                IsLoading = false;
            }
            catch (Exception ex)
            {
                IsLoading = false;
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                return false;
            }
            return false;
        }

        private bool SetDataForSaveDraft(EstablishmentRegistrationTabsEnum _enum)
        {
            bool flag = false;
            try
            {
                IsLoading = true;
                if (_enum == EstablishmentRegistrationTabsEnum.RegistrationType)
                {
                    if (SelectedReportingBranch?.authorizationGroup != null)
                    {
                        taxPayerDetails.Augrp = SelectedReportingBranch?.authorizationGroup;
                    }
                    taxPayerDetails.Tpnationality = NationalityMapping.Where(i => i.Value == SelectedRegNationalityType).FirstOrDefault().Key;
                    taxPayerDetails.Tpnationality = taxPayerDetails.Tpnationality == null ? "" : taxPayerDetails.Tpnationality;
                    taxPayerDetails.Taxtpdetermination = "1";
                    if (!SelectedTpresidence.Equals(""))
                    {
                        taxPayerDetails.Tpresidence = SelectedTpresidence;
                    }
                    taxPayerDetails.Orgnonresident = string.IsNullOrEmpty(SelectedOrgNonResident) ? string.Empty : SelectedOrgNonResident;
                    taxPayerDetails.Orgnonresidentoptions = string.IsNullOrEmpty(SelectedOrgNonResidentOptions) ? string.Empty : SelectedOrgNonResidentOptions;
                    taxPayerDetails.Orgnonresidentactivity = string.IsNullOrEmpty(SelectedOrgNonResidentActivity) ? string.Empty : SelectedOrgNonResidentActivity;

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
                    flag = true;
                    return flag;
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.TaxpayerDetail)
                {
                    if (!string.IsNullOrEmpty(DisplaySelectedDOB))
                    {
                        DateTime.TryParseExact(DisplaySelectedDOB, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime dob);
                        taxPayerDetails.Birthdt = dob.ToString("yyyy-MM-ddThh:mm:ss");
                    }
                    taxPayerDetails.TpTitle = Title;
                    taxPayerDetails.NameFirst = FirstName;
                    taxPayerDetails.NameLast = string.IsNullOrEmpty(LastName) ? string.Empty : LastName;
                    taxPayerDetails.FatherName = string.IsNullOrEmpty(FatherName) ? string.Empty : FatherName;
                    taxPayerDetails.GrandfatherName = string.IsNullOrEmpty(GrandFatherName) ? string.Empty : GrandFatherName;
                    taxPayerDetails.FamilyName = string.IsNullOrEmpty(FamilyName) ? string.Empty : FamilyName;
                    taxPayerDetails.Initials = string.IsNullOrEmpty(Initial) ? string.Empty : Initial;
                    if (SelectedGender?.ToLower() == GenderList.FirstOrDefault().ToLower())
                    {
                        taxPayerDetails.Xsexm = "X";
                    }
                    else if (SelectedGender?.ToLower() == GenderList.LastOrDefault().ToLower())
                    {
                        taxPayerDetails.Xsexf = "X";
                    }
                    taxPayerDetails.Natio = SelectedTaxpayerPDNationality?.Land1;
                    taxPayerDetails.Natio = taxPayerDetails.Natio == null ? "" : taxPayerDetails.Natio;
                    taxPayerDetails.Citizen = SelectedCitizen?.Land1 == null ? "" : SelectedCitizen.Land1;
                    taxPayerDetails.Residence = SelectedResidence?.Land1 == null ? "" : SelectedResidence?.Land1;

                    taxPayerDetails.StepNumberx = "02";
                    taxPayerDetails.Gpart = App.LoginDataRetrieved.TIN;
                    taxPayerDetails.UserTypx = "TP";
                    flag = true;
                    return flag;
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.PassportDetails)
                {

                    Nreg_IdItem passportObj = new Nreg_IdItem();
                    passportObj.Idnumber = PassportNumber == null ? "" : PassportNumber;
                    passportObj.Country = SelectedPassportIssueCountry?.Land1 == null ? "" : SelectedPassportIssueCountry?.Land1;

                    if (!string.IsNullOrEmpty(PassportIssueDate))
                    {
                        DateTime.TryParseExact(PassportIssueDate, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime issueDate);
                        passportObj.ValidDateFrom = issueDate.ToString("yyyy-MM-ddThh:mm:ss");
                    }
                    if (!string.IsNullOrEmpty(PassportExpireDate))
                    {
                        DateTime.TryParseExact(PassportExpireDate, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime expireDate);
                        passportObj.ValidDateTo = expireDate.ToString("yyyy-MM-ddThh:mm:ss");
                    }

                    passportObj.Type = "FS0002";
                    passportObj.Srcidentify = "00000";

                    taxPayerDetails.Nreg_IdSet.Clear();
                    taxPayerDetails.Nreg_IdSet.Add(passportObj);

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
                    flag = true;
                    return flag;
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.FinancialDetail)
                {
                    if (!string.IsNullOrEmpty(TaxDate))
                    {
                        DateTime.TryParseExact(TaxDate, string.Format("{0:0000/00/00}", Int64.Parse(financialDetail?.EIsldate)), new CultureInfo("en-US"), DateTimeStyles.None, out DateTime Fdenddt);
                        taxPayerDetails.Fdenddt = Fdenddt.ToString();
                    }
                    taxPayerDetails.Accmethod = EnMethodList.FirstOrDefault(i => i.Value == SelectedMethod).Key;//Value Recheck
                    taxPayerDetails.Accmethod = taxPayerDetails.Accmethod == null ? "" : taxPayerDetails.Accmethod;
                    taxPayerDetails.Fdcalender = EnCalendarTypeList.FirstOrDefault(i => i.Value == CalendarType).Key;
                    taxPayerDetails.Fdcalender = taxPayerDetails.Fdcalender == null ? "" : taxPayerDetails.Fdcalender;
                    taxPayerDetails.Fdmonth = FiscalMonth == null ? "" : FiscalMonth;
                    taxPayerDetails.Fdday = FiscalDay == AppResources.ESTFinLastDay ? "LD" : FiscalDay;
                    taxPayerDetails.Commdt = financialDetail?.ACommDate;
                    taxPayerDetails.Gpart = App.LoginDataRetrieved.TIN;
                    taxPayerDetails.StepNumberx = "04";
                    taxPayerDetails.UserTypx = "TP";
                    if (SelectedPeriod != null)
                    {
                        taxPayerDetails.FinPeriod = SelectedPeriod.FinPeriod;
                    }
                    flag = true;
                    return flag;
                }
                taxPayerDetails?.off_notesSet?.Clear();
                flag = true;
            }
            catch (Exception ex)
            {
                _dialogService.ShowMessage(ex.Message, AppResources.Information);
                return flag;
            }
            return flag;
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
                    Title = string.Empty;
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
        #endregion
    }


}