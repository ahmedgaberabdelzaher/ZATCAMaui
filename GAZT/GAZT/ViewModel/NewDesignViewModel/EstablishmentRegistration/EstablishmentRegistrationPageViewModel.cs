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
using EGAZT.Views.NewDesign.EstablishmentRegistrationPages;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using Plugin.FilePicker;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel.EstablishmentRegistration
{
    [Preserve(AllMembers = true)]
    public class EstablishmentRegistrationPageViewModel : BaseViewModel
    {
        #region Variable

        public static TaxPayerDetails taxPayerDetails { get; set; } = null;
        private FinancialDetail financialDetail { get; set; } = null;
        private Nreg_IdItem idItem { get; set; } = null;
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
                        Device.BeginInvokeOnMainThread(() => fetchTabDataAndBind(_currentTab));
                    }
                    return;
                }
                _currentTab = value;
                RaisePropertyChanged(nameof(currentTab));
                CurrentIndex = (int)_currentTab;
                RaisePropertyChanged(nameof(CurrentIndex));
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
                Device.BeginInvokeOnMainThread(() => fetchTabDataAndBind(_currentTab));
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
                RaisePropertyChanged(nameof(TabList));
            }
        }
        public bool MarkComplete { get; set; } = false;
        private int _maxIndex = 6;
        public int MaxIndex
        {
            get => _maxIndex; set
            {
                _maxIndex = value;
                RaisePropertyChanged(nameof(MaxIndex));
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
                RaisePropertyChanged(nameof(CurrentIndex));
                MarkComplete = _currenrIndex == MaxIndex;
                RaisePropertyChanged(nameof(MarkComplete));
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
                RaisePropertyChanged(nameof(SelectedTabText));
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
                RaisePropertyChanged(nameof(NxtButtonLabel));
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
                RaisePropertyChanged("IsClickedOwnRentOption");
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
                RaisePropertyChanged("IsClickedStayMoreThanKSAOption");
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
                RaisePropertyChanged("IsClickedNoneOfTheAboveOption");
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
                RaisePropertyChanged("IsClickedPermanentLegalEntity");
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
                RaisePropertyChanged("IsClickedOtherTaxableIncomeLegalEntity");
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
                RaisePropertyChanged("IsClickedABranchOfNonResidentCompanyPE");
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
                RaisePropertyChanged("IsClickedConstructionSitePE");
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
                RaisePropertyChanged("IsClickedInstallationPE");
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
                RaisePropertyChanged("IsClickedAFixedBasePE");
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
                RaisePropertyChanged("IsClickedNonResidentPartnerPE");
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
                RaisePropertyChanged(nameof(SelectedTpresidence));
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
                RaisePropertyChanged(nameof(SelectedOrgNonResident));
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
                RaisePropertyChanged(nameof(SelectedOrgNonResidentOptions));
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
                RaisePropertyChanged(nameof(SelectedOrgNonResidentActivity));
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
                    RaisePropertyChanged(nameof(OrgNonResidentActivityList));
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
                RaisePropertyChanged(nameof(SelectedOrgNonResidentActivityItem));
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
                RaisePropertyChanged("IsAttachmentEnable");
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
                RaisePropertyChanged(nameof(SelectedReportingBranch));
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
                RaisePropertyChanged(nameof(ReportingBranchList));
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
                RaisePropertyChanged(nameof(SelectedEntityType));
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
                RaisePropertyChanged(nameof(SelectedTaxPayerType));
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
                RaisePropertyChanged(nameof(SelectedRegNationalityType));
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
                RaisePropertyChanged(nameof(IsSaudi));
            }
        }
        public string SelectedLegalEntity
        {
            get => _selectedLegalEntity;
            set
            {
                if (_selectedLegalEntity == value) return;

                _selectedLegalEntity = value;
                RaisePropertyChanged(nameof(SelectedLegalEntity));
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
                RaisePropertyChanged(nameof(UploadedRentDocumentsList));
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
                RaisePropertyChanged(nameof(IsVisbleRentAttachmentmentList));
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
                RaisePropertyChanged(nameof(SelectedRentFileName));
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
                    RaisePropertyChanged(nameof(GenderList));
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
                RaisePropertyChanged(nameof(SelectedGender));
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
                RaisePropertyChanged(nameof(GCCIDType));
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
                RaisePropertyChanged(nameof(GCCIDTypeIdNumberValue));
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
                RaisePropertyChanged(nameof(SelectedDOB));
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
                RaisePropertyChanged(nameof(DisplaySelectedDOB));
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
                RaisePropertyChanged(nameof(SelectedDOBDate));
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
                RaisePropertyChanged(nameof(SelectedDOBHijiriDate));
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
                RaisePropertyChanged(nameof(FirstName));
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
                RaisePropertyChanged(nameof(LastName));
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
                RaisePropertyChanged(nameof(FatherName));
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
                RaisePropertyChanged(nameof(GrandFatherName));
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
                RaisePropertyChanged(nameof(FamilyName));
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

                RaisePropertyChanged(nameof(Initial));
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
                RaisePropertyChanged("DatePickerModel");
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
                RaisePropertyChanged(nameof(TaxpayerFullNationlityList));
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
                RaisePropertyChanged(nameof(TaxpayerPDNationlityList));
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
                RaisePropertyChanged(nameof(SelectedTaxpayerPDNationality));
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
                RaisePropertyChanged(nameof(SelectedCitizen));
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
                RaisePropertyChanged(nameof(SelectedResidence));
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
                RaisePropertyChanged(nameof(PassportNumber));
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
                RaisePropertyChanged(nameof(SelectedPassportIssueCountry));
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
                RaisePropertyChanged(nameof(SelectedPassportIssueDate));
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
                RaisePropertyChanged(nameof(SelectedPassportIssueHijiriDate));
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
                RaisePropertyChanged(nameof(PassportIssueDate));
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
                RaisePropertyChanged(nameof(DisplayPassportIssueDate));
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
                RaisePropertyChanged(nameof(SelectedPassportExpireDate));
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
                RaisePropertyChanged(nameof(SelectedPassportExpireHijiriDate));
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
                RaisePropertyChanged(nameof(PassportExpireDate));
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
                RaisePropertyChanged(nameof(DisplayPassportExpireDate));
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
                RaisePropertyChanged(nameof(UploadedPassportDocumentsList));
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
                RaisePropertyChanged("IsVisbleAttachmentPassportList");
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
                RaisePropertyChanged(nameof(SelectedPassportFileName));
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
                RaisePropertyChanged(SearchText);
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
                    RaisePropertyChanged(nameof(OutletData));
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
                    RaisePropertyChanged(nameof(SearchableOutletData));
                }
            }
        }
        #endregion

        #region Financial Details Tabs variables
        private List<string> dates = new List<string> { AppResources.ESTFinLastDay, "30", "29", "28", "27", "26", "25", "24", "23", "22", "21", "20", "19", "18", "17", "16", "15", "14", "13", "12", "11", "10", "09", "08", "07", "06", "05", "04", "03", "02", "01" };

        private Dictionary<string, string> EnMethodList = new Dictionary<string, string>()
        {
            {"A", AppResources.NDAccounting },
            {"E", AppResources.NDEstimated }
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
                    RaisePropertyChanged(nameof(MethodList));
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
                    RaisePropertyChanged(nameof(SelectedMethod));
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
                    RaisePropertyChanged(nameof(CalendarTypeList));
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
                    RaisePropertyChanged(nameof(CalendarType));
                    if (taxPayerDetails != null)
                        udpdateDates();
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
                RaisePropertyChanged(nameof(FiscalMonth));
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
                RaisePropertyChanged(nameof(FiscalDay));
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
                RaisePropertyChanged(nameof(CommDate));
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
                RaisePropertyChanged(nameof(TaxDate));
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
                RaisePropertyChanged(nameof(SummaryExpendedCard));
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
                    RaisePropertyChanged(nameof(OutletList));
                }
            }
        }
        private bool _eSTLedge = false;
        public bool ESTLedge
        {
            get => _eSTLedge;
            set
            {
                if (_eSTLedge == value) return;

                _eSTLedge = value;
                RaisePropertyChanged(nameof(ESTLedge));
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
                RaisePropertyChanged(nameof(CanExecute));
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
            OnNextButtonClick = new Command(() => navigateToNext(), () => CanExecute);
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
            OnEstablishmentRegistrationAttachmentTapped = new Command(() => OnRentAddAttachmentTapped());
            #endregion

            OnReportingBranchSelectButtonClick = new Command(() =>
           {
               ListPopUpViewPage poupWindow = new ListPopUpViewPage(ReportingBranchList);
               poupWindow.OnItemSelect = (item) =>
               {
                   SelectedReportingBranch = (item as BranchesDropDownModel);
               };
               PopupNavigation.Instance.PushAsync(poupWindow);
           });

            #endregion

            #region TaxPayer Variable initialization

            GetGenderList();

            OnPDNatinalitySelectButtonClick = new Command(() =>
            {
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(TaxpayerFullNationlityList);
                poupWindow.OnItemSelect = (item) => SelectedTaxpayerPDNationality = item as TaxpayerNationality;
                PopupNavigation.Instance.PushAsync(poupWindow);
            });

            OnPDCitizenSelectButtonClick = new Command(() =>
            {
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(TaxpayerPDNationlityList);
                poupWindow.OnItemSelect = (item) => SelectedCitizen = item as TaxpayerNationalityLandx50;
                PopupNavigation.Instance.PushAsync(poupWindow);
            });

            OnPDResidenceSelectButtonClick = new Command(() =>
            {
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(TaxpayerPDNationlityList);
                poupWindow.OnItemSelect = (item) => SelectedResidence = item as TaxpayerNationalityLandx50;
                PopupNavigation.Instance.PushAsync(poupWindow);
            });
            #endregion

            #region Passport Variable Initialization

            OnPassportIssueCountryButtonClick = new Command(() =>
            {
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(TaxpayerFullNationlityList);
                poupWindow.OnItemSelect = (item) => SelectedPassportIssueCountry = item as TaxpayerNationality;
                PopupNavigation.Instance.PushAsync(poupWindow);
            });

            OnPassportAttachmentTapped = new Command(() => OnPassportAddAttachmentButtonTapped());
            #endregion

            TappedOnAttachmentInformationIcon = new Command(() =>
            ShowValidationPopup(
                AppResources.ESTAttachmentSizeNotfication
                + System.Environment.NewLine
                + AppResources.ZZChooseonlyfilewithextensionForZAKAT
                + System.Environment.NewLine + AppResources.ZMaximumnoof5attachmentscanbeuploaded
                ));

            #region Outlet Tabs variable initialization
            OnNewOutletButtonClick = new Command(() => openNewOutlet());
            OnEditOutletButtonClick = new Command((item) => openEditOutlet(item as OutletItem));
            OnDeleteOutletButtonClick = new Command(async (item) =>
            {
                var confirmPopup = new ZAKATOkCancelPopUpView(AppResources.ZZDeleteAttachmentConfirmationText)
                {
                    CloseWhenBackgroundIsClicked = false
                };
                confirmPopup.OnSelect = (str) =>
                {
                    if (str == "Yes")
                    {
                        Device.BeginInvokeOnMainThread(() => deleteOutlet(item as OutletItem));
                    }
                };
                await PopupNavigation.Instance.PushAsync(confirmPopup);
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
                PopupNavigation.Instance.PushAsync(poupWindow);
            });
            OnDaySelectButtonClick = new Command(() =>
            {
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(dates);
                poupWindow.OnItemSelect = (item) =>
                {
                    FiscalDay = item as string;
                    udpdateDates(FiscalDay);
                };
                PopupNavigation.Instance.PushAsync(poupWindow);
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
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(new List<string> { AppResources.Save, AppResources.ZZVoid, AppResources.FORM5CalendarType });
                poupWindow.OnItemSelect = async (item) =>
                {
                    var actionName = item as string;
                    Console.WriteLine(item);
                    if (actionName == AppResources.Save)
                    {
                        IsLoading = true;
                        try
                        {
                            taxPayerDetails.Draftfg = "X";
                            taxPayerDetails.Gpartx = App.LoginDataRetrieved.TIN;
                            taxPayerDetails.UserTypx = "TP";
                            var _taxPayerDetails = await EstablishmentRegistrationWebServiceManager.ESTTaxPayerDetailPostService(taxPayerDetails);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.StackTrace);
                            if (e is HTTPBadRequestException)
                            {
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(e.Message));
                            }
                        }
                        finally
                        {
                            IsLoading = false;
                        }
                    }
                    if (actionName == AppResources.ZZVoid)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
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
                                        taxPayerDetails?.off_notesSet.results?.Clear();
                                        taxPayerDetails?.off_notesSet.results?.Add(note);
                                        taxPayerDetails.Operationx = "04";
                                        taxPayerDetails.Gpartx = App.LoginDataRetrieved.TIN;
                                        taxPayerDetails.UserTypx = "TP";
                                        taxPayerDetails.StepNumberx = string.Empty;
                                        var _taxPayerDetails = await EstablishmentRegistrationWebServiceManager.ESTTaxPayerDetailPostService(taxPayerDetails);
                                        currentTab = EstablishmentRegistrationTabsEnum.Unknown;
                                        navigationService.NavigateTo(App.GAZTNewDesignDashBoardPageView);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.StackTrace);
                                        if (e is HTTPBadRequestException)
                                        {
                                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(e.Message));
                                        }
                                    }
                                    finally
                                    {
                                        IsLoading = false;
                                    }
                                }
                            };
                            await PopupNavigation.Instance.PushAsync(voidNotePop);
                        });
                    }
                    if (actionName == AppResources.FORM5CalendarType)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            ListPopUpViewPage cal = new ListPopUpViewPage(new List<string> { AppResources.NDGregorian, AppResources.NDHijri });
                            cal.OnItemSelect = (_cal) =>
                            {
                                if (_cal as string == AppResources.NDGregorian)
                                {
                                    taxPayerDetails.Caltp = "G";
                                }
                                else if (_cal as string == AppResources.NDHijri)
                                {
                                    taxPayerDetails.Caltp = "H";
                                }
                                updateDatePickers(currentTab);
                            };
                            await PopupNavigation.Instance.PushAsync(cal);
                        });
                    }
                };
                PopupNavigation.Instance.PushAsync(poupWindow);
            });
            #endregion
        }

        #endregion

        #region Method
        public void OnAppearing()
        {
            TabList
            = new ObservableCollection<string>{ AppResources.ESTRegTaxTabTitleLabel, AppResources.ESTTaxpayerPersonalDetailsTabTitleLabel,
                AppResources.ESTPassportDetailsTabTitleLabel, AppResources.ESTOutletsTabTitleLabel,
                AppResources. VATRFinancialDetails, AppResources.ZVatSummary };
            if (currentTab == EstablishmentRegistrationTabsEnum.Outlets)
            {
                fetchTabDataAndBind(currentTab);
            }
        }

        private async void navigateToNext()
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
            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(_message));
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

        public async void OnRentAddAttachmentTapped()
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

        public async void OnPassportAddAttachmentButtonTapped()
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
                string[] filetypes = DependencyService.Get<IDeviceInfo>().GetAttachmentTypeStringForTaxEvasion();

                var fileData = await CrossFilePicker.Current.PickFile(filetypes);

                if (fileData != null)
                {
                    var attachmentByte = fileData.DataArray;

                    string base64String = Convert.ToBase64String(attachmentByte, 0, attachmentByte.Length);
                    var attachmentName = fileData.FileName;
                    bool isFileAlreayUploaded = IsFileAlreadyAttached(docType, attachmentName);
                    float sizemb = (attachmentByte.Length / 1024f) / 1024f;
                    decimal attachmentSize = 0;
                    attachmentSize = attachmentSize + (Decimal)sizemb;
                    if (!isFileAlreayUploaded)
                    {
                        if (fileData.FileName.Contains("."))
                        {
                            string Extention = fileData.FileName.Split('.')[1];//pdf
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
                                        catch (Exception ex)
                                        {
                                            Console.Write(ex.ToString());
                                            Console.Write(ex.StackTrace.ToString());
                                        }
                                    }
                                    else
                                    {
                                        attachmentName = string.Empty;
                                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Somethingwentwrong));
                                    }

                                }
                                else
                                {
                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTAttachmentSizeNotfication));
                                }
                            }
                            else
                            {
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly));
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


        private async Task SaveAttachment(byte[] attachmentByteData, string fileName, string docType, string contentType)
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
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

        public async void fetchTabDataAndBind(EstablishmentRegistrationTabsEnum _enum)
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
                            _navigationService.NavigateTo(App.RegistrationSuccessfulPage, taxPayerDetails);
                        }
                    }
                    else
                    {
                        IsNavigationCompletedToSuccessfulPage = false;
                    }
                    SelectedReportingBranch = ReportingBranchList.Where(i => i.Augrp == taxPayerDetails?.Augrp).FirstOrDefault();
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
                        await PopupNavigation.Instance.PushAsync(someThingWhentWrong);
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
                    idItem = taxPayerDetails?.Nreg_IdSet.results.Where(i => EnIDType.ContainsKey(i.Type)).FirstOrDefault();
                    if (App.IsArabic)
                    {
                        GCCIDType = ArIDType[idItem?.Type];
                    }
                    else
                    {
                        GCCIDType = EnIDType[idItem?.Type];
                    }
                    GCCIDTypeIdNumberValue = idItem.Idnumber;
                    SelectedDOB = taxPayerDetails?.Birthdt?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
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
                    Nreg_IdItem passportItem = taxPayerDetails?.Nreg_IdSet.results.Where(i => i.Type == "FS0002").FirstOrDefault();
                    PassportNumber = passportItem?.Idnumber;
                    SelectedPassportIssueCountry = TaxpayerFullNationlityList.Where(i => i.Land1 == passportItem?.Country).FirstOrDefault();
                    PassportIssueDate = passportItem?.ValidDateFrom?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                    PassportExpireDate = passportItem?.ValidDateTo?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                    PassportAttachmentPrepopulateCheck(taxPayerDetails);
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.Outlets)
                {
                    bindingOutletList();
                    taxPayerDetails = await EstablishmentRegistrationWebServiceManager.ESTTaxPayerDetailGetService("03", App.LoginDataRetrieved.TIN, App.LoginDataRetrieved.Emailid, null, taxPayerDetails?.Fbnumx);
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.FinancialDetail)
                {
                    taxPayerDetails = await EstablishmentRegistrationWebServiceManager.ESTTaxPayerDetailGetService("04", App.LoginDataRetrieved.TIN, App.LoginDataRetrieved.Emailid, null, taxPayerDetails?.Fbnumx);
                    SelectedMethod = EnMethodList[taxPayerDetails?.Accmethod];
                    CalendarType = EnCalendarTypeList[taxPayerDetails?.Fdcalender];
                    udpdateDates();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
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
                        _navigationService.NavigateTo(App.RegistrationSuccessfulPage, taxPayerDetails);
                    }
                    SelectedReportingBranch = ReportingBranchList.Where(i => i.Augrp == taxPayerDetails?.Augrp).FirstOrDefault();
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
                    idItem = taxPayerDetails?.Nreg_IdSet?.results?.Where(i => EnIDType.ContainsKey(i.Type)).FirstOrDefault();
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
                    SelectedDOB = taxPayerDetails?.Birthdt?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
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
                        _navigationService.NavigateTo(App.RegistrationSuccessfulPage, taxPayerDetails);
                    }
                    SelectedReportingBranch = ReportingBranchList.Where(i => i.Augrp == taxPayerDetails?.Augrp).FirstOrDefault();
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

                        await PopupNavigation.Instance.PushAsync(someThingWhentWrong);
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
                    idItem = taxPayerDetails?.Nreg_IdSet?.results?.Where(i => EnIDType.ContainsKey(i.Type)).FirstOrDefault();
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
                    SelectedDOB = taxPayerDetails?.Birthdt?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
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
                    Nreg_IdItem passportItem = taxPayerDetails?.Nreg_IdSet.results.Where(i => i.Type == "FS0002").FirstOrDefault();
                    PassportNumber = passportItem?.Idnumber;
                    SelectedPassportIssueCountry = TaxpayerFullNationlityList.Where(i => i.Land1 == passportItem?.Country).FirstOrDefault();
                    PassportIssueDate = passportItem?.ValidDateFrom?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                    PassportExpireDate = passportItem?.ValidDateTo?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                    PassportAttachmentPrepopulateCheck(taxPayerDetails);
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.Outlets)
                {
                    bindingOutletList();
                    taxPayerDetails = await EstablishmentRegistrationWebServiceManager.ESTTaxPayerDetailGetService("03", App.LoginDataRetrieved.TIN, App.LoginDataRetrieved.Emailid, null, taxPayerDetails?.Fbnumx);
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.FinancialDetail)
                {
                    taxPayerDetails = await EstablishmentRegistrationWebServiceManager.ESTTaxPayerDetailGetService("04", App.LoginDataRetrieved.TIN, App.LoginDataRetrieved.Emailid, null, taxPayerDetails?.Fbnumx);
                    SelectedMethod = EnMethodList[taxPayerDetails?.Accmethod];
                    CalendarType = EnCalendarTypeList[taxPayerDetails?.Fdcalender];
                    if (taxPayerDetails?.Accmethod == "A")
                        SelectedMethod = AppResources.NDAccounting;
                    else
                        SelectedMethod = AppResources.NDEstimated;

                    if (taxPayerDetails?.Fdcalender == "1")
                        CalendarType = AppResources.Gregorian;
                    else
                        CalendarType = AppResources.Hijri;
                    udpdateDates();
                }
            }
            catch (Exception e)
            {
                await Task.Run(() =>
                {

                    IsLoading = false;
                });
                throw e;
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
                if (_enum == EstablishmentRegistrationTabsEnum.TaxpayerDetail)
                {
                    DateTime.TryParseExact(SelectedDOB, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime _dob);
                    if (taxPayerDetails?.Caltp == "G")
                    {
                        SelectedDOBDate = _selectedDOBDate;
                        if (!string.IsNullOrWhiteSpace(SelectedDOB))
                            DisplaySelectedDOB = _dob.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                    }
                    else
                    {
                        SelectedDOBHijiriDate = _selectedDOBDate;
                        if (!string.IsNullOrWhiteSpace(SelectedDOB))
                            DisplaySelectedDOB = HijriDateString(_dob);
                    }
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.PassportDetails)
                {
                    DateTime.TryParseExact(PassportIssueDate, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime _issueDate);
                    DateTime.TryParseExact(PassportExpireDate, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime _expiryDate);
                    if (taxPayerDetails?.Caltp == "G")
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
                    if (taxPayerDetails?.Caltp == "G")
                    {
                        SelectedPassportExpireDate = _selectedDOBDate;
                        if (!string.IsNullOrWhiteSpace(PassportExpireDate))
                            DisplayPassportExpireDate = _expiryDate.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                    }
                    else
                    {
                        SelectedPassportExpireHijiriDate = _selectedDOBDate;
                        if (!string.IsNullOrWhiteSpace(PassportExpireDate))
                            DisplayPassportExpireDate = HijriDateString(_expiryDate);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                //TODO error : Not a valid calendar for the given culture.
            }
        }
        private async void udpdateDates(string selectedDate = null)
        {
            try
            {
                IsLoading = true;
                var _CalendarType = EnCalendarTypeList.FirstOrDefault(i => i.Value == CalendarType).Key == "2" ? "H" : "G";
                financialDetail = await EstablishmentRegistrationWebServiceManager.ESTFinancialMaxDate(new FinancialDetailRequest()
                {
                    ACaltype = _CalendarType,
                    ADateComm = taxPayerDetails?.Commdt
                });
                if (_CalendarType == "H")
                {
                    string dd = financialDetail?.ACommDate.Substring(6, 2);
                    string mm = financialDetail?.ACommDate.Substring(4, 2);
                    string yy = financialDetail?.ACommDate.Substring(0, 4);
                    CommDate = $"{Int16.Parse(yy) - 1:0000}/{Int16.Parse(mm):00}/{Int16.Parse(dd):00}";
                }
                else
                {
                    CommDate = string.Format("{0:0000/00/00}", Int64.Parse(financialDetail?.ACommDate));
                }


                if (selectedDate == null)
                {
                    string dd = financialDetail?.ACommDate.Substring(6, 2);
                    string mm = financialDetail?.ACommDate.Substring(4, 2);
                    if (dd != "01")
                    {
                        if (taxPayerDetails?.Fdcalender == "1")
                        {
                            if (dd == "29" && mm == "02")
                            {
                                dd = $"{Int16.Parse(dd) - 2:00}";
                            }
                            else
                            {
                                dd = $"{Int16.Parse(dd) - 1:00}";
                            }
                        }
                        else
                        {
                            dd = $"{Int16.Parse(dd) - 1:00}";
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
                        mm = $"{Int16.Parse(mm) - 1:00}";
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
                TaxDate = string.Format("{0:0000/00/00}", Int64.Parse(_CalendarType == "H" ? financialDetail?.ACommDate : financialDetail?.EIsldate));

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
                IsLoading = false;


            }
            catch (Exception)
            {
                IsLoading = false;
                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Somethingwentwrong));

            }
        }

        private async void bindingOutletList()
        {
            var _outletTempData = await EstablishmentRegistrationWebServiceManager.ESTOutletList(taxPayerDetails?.PortalUsrx, App.LoginDataRetrieved.TIN, taxPayerDetails?.Fbnumx);
            OutletData.Clear();
            SearchableOutletData?.Clear();
            _outletTempData.ForEach(_out =>
            {
                OutletData.Add(_out);
                SearchableOutletData.Add(_out);
            });
        }
        private void openNewOutlet()
        {
            OutletNavigationModels outletNavigationModels = new OutletNavigationModels();
            outletNavigationModels.taxPayerDetails = taxPayerDetails;
            outletNavigationModels.idItem = idItem;
            _navigationService.NavigateTo(App.OutletDetailsPageView, outletNavigationModels);
        }
        private void openEditOutlet(OutletItem item)
        {
            OutletNavigationModels outletNavigationModels = new OutletNavigationModels();
            outletNavigationModels.taxPayerDetails = taxPayerDetails;
            outletNavigationModels.idItem = idItem;
            outletNavigationModels.selectedOutletItem = item;
            _navigationService.NavigateTo(App.OutletDetailsPageView, outletNavigationModels);
        }
        private void deleteOutlet(OutletItem item)
        {
            IsLoading = true;
            Console.WriteLine(item.ToString());
            var delete = EstablishmentRegistrationWebServiceManager.ESTDeleteOutletItem(taxPayerDetails?.Fbnumx, item?.Actno, taxPayerDetails?.PortalUsrx);
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
                    if (SelectedReportingBranch == null || string.IsNullOrWhiteSpace(SelectedReportingBranch.Bez50))
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateBranch));
                        return false;
                    }
                    if (!IsSaudi)
                    {
                        if (string.IsNullOrWhiteSpace(SelectedTpresidence))
                        {
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateResidenceType));
                            return false;
                        }
                        else if (SelectedTpresidence == "2" && (UploadedRentDocumentsList == null || UploadedRentDocumentsList.Count <= 0))
                        {
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAttachRent));
                            return false;
                        }
                        else if (SelectedTpresidence == "3" && string.IsNullOrWhiteSpace(SelectedOrgNonResident))
                        {
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateLegalEntity));
                            return false;
                        }
                        else if (SelectedTpresidence == "3" && SelectedOrgNonResident == "1" && string.IsNullOrWhiteSpace(SelectedOrgNonResidentOptions))
                        {
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateParmanentEst));
                            return false;
                        }
                        else if (SelectedTpresidence == "3" && SelectedOrgNonResident == "2" && string.IsNullOrWhiteSpace(SelectedOrgNonResidentActivity))
                        {
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateOtherTax));
                            return false;
                        }
                    }
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.TaxpayerDetail)
                {

                    if (string.IsNullOrWhiteSpace(SelectedDOB))
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateDOB));
                        return false;
                    }
                    else if (string.IsNullOrWhiteSpace(FirstName))
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateFirstName));
                        return false;
                    }
                    else if (string.IsNullOrWhiteSpace(SelectedGender))
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateGender));
                        return false;
                    }
                    else if (null == SelectedTaxpayerPDNationality)
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateNationality));
                        return false;
                    }
                    else if (null == SelectedCitizen)
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateCitizen));
                        return false;
                    }
                    else if (null == SelectedResidence)
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateResidence));
                        return false;
                    }
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.PassportDetails)
                {
                    if (string.IsNullOrWhiteSpace(PassportNumber))
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePassportNumber));
                        return false;
                    }
                    else if (null == SelectedPassportIssueCountry)
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePIssueCountry));
                        return false;
                    }
                    else if (string.IsNullOrWhiteSpace(PassportIssueDate))
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePIssueDate));
                        return false;
                    }
                    else if (string.IsNullOrWhiteSpace(PassportExpireDate))
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePExpiryDate));
                        return false;
                    }
                    else if (UploadedPassportDocumentsList == null || UploadedPassportDocumentsList.Count <= 0)
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePAttachCopy));
                        return false;
                    }
                    else
                    {
                        DateTime.TryParseExact(SelectedDOB, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime dob);
                        DateTime.TryParseExact(PassportIssueDate, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime issue);
                        DateTime.TryParseExact(PassportExpireDate, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime expiry);
                        if (DateTime.Compare(issue, dob) < 0)
                        {
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp("passport issue date not before dob"));
                            return false;
                        }
                        else if (DateTime.Compare(expiry, dob) < 0)
                        {
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp("passport expiry date not before dob"));
                            return false;
                        }
                        else if (DateTime.Compare(expiry, issue) < 0)
                        {
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp("passport expiry date not before passport issue"));
                            return false;
                        }
                    }
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.Outlets)
                {
                    if (OutletData.Count == 0)
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAtleastOutlet));
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
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePledge));
                        return false;
                    }
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return true;
        }


        private async Task<bool> PushDatatoServer(EstablishmentRegistrationTabsEnum _enum)
        {
            try
            {
                IsLoading = true;
                if (_enum == EstablishmentRegistrationTabsEnum.RegistrationType)
                {
                    taxPayerDetails.Augrp = SelectedReportingBranch?.Augrp;
                    taxPayerDetails.Atype = "1";// SelectedEntityType.Equals("Individual") ? "1" : "2";
                    taxPayerDetails.Tpnationality = NationalityMapping.Where(i => i.Value == SelectedRegNationalityType).FirstOrDefault().Key;
                    taxPayerDetails.Tpnationality = string.IsNullOrEmpty(taxPayerDetails.Tpnationality) ? "" : taxPayerDetails.Tpnationality;
                    taxPayerDetails.Taxtpdetermination = "1";
                    taxPayerDetails.Tpresidence = SelectedTpresidence;
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
                    taxPayerDetails.Gpartx = App.LoginDataRetrieved.TIN;
                    taxPayerDetails.UserTypx = "TP";
                    var taxPayerDetailsResult = await EstablishmentRegistrationWebServiceManager.ESTTaxPayerDetailPostService(taxPayerDetails);

                    IsLoading = false;
                    return true;
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.TaxpayerDetail)
                {
                    DateTime.TryParseExact(SelectedDOB, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime dob);
                    taxPayerDetails.Birthdt = dob;
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
                    taxPayerDetails.Gpartx = App.LoginDataRetrieved.TIN;
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
                    passportObj.ValidDateFrom = issueDate;
                    passportObj.ValidDateTo = expireDate;
                    passportObj.Type = "FS0002";
                    passportObj.Srcidentify = "000";
                    if (taxPayerDetails.Nreg_IdSet.results.Count > 0)
                    {
                        taxPayerDetails.Nreg_IdSet.results[0].Srcidentify = "000";
                        taxPayerDetails.Nreg_IdSet.results[0].Gpart = "";

                        Nreg_IdItem nreg_IdItem = new Nreg_IdItem();
                        nreg_IdItem = taxPayerDetails.Nreg_IdSet.results[0];
                        taxPayerDetails.Nreg_IdSet.results.Clear();
                        taxPayerDetails.Nreg_IdSet.results.Add(nreg_IdItem);

                    }
                    taxPayerDetails.Nreg_IdSet.results.Add(passportObj);
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
                    taxPayerDetails.Gpartx = App.LoginDataRetrieved.TIN;
                    taxPayerDetails.UserTypx = "TP";
                    var taxPayerDetailsResult = await EstablishmentRegistrationWebServiceManager.ESTTaxPayerDetailPostService(taxPayerDetails);

                    IsLoading = false;
                    return true;
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.FinancialDetail)
                {
                    DateTime.TryParseExact(TaxDate, string.Format("{0:0000/00/00}", Int64.Parse(financialDetail?.EIsldate)), new CultureInfo("en-US"), DateTimeStyles.None, out DateTime Fdenddt);
                    taxPayerDetails.Accmethod = EnMethodList.FirstOrDefault(i => i.Value == SelectedMethod).Key;
                    taxPayerDetails.Fdcalender = EnCalendarTypeList.FirstOrDefault(i => i.Value == CalendarType).Key;
                    taxPayerDetails.Fdmonth = FiscalMonth;
                    taxPayerDetails.Fdday = FiscalDay == AppResources.ESTFinLastDay ? "LD" : FiscalDay;
                    taxPayerDetails.Commdt = financialDetail?.ADateComm;
                    taxPayerDetails.Fdenddt = Fdenddt;
                    taxPayerDetails.Chkfg = "X";
                    taxPayerDetails.Gpartx = App.LoginDataRetrieved.TIN;
                    taxPayerDetails.StepNumberx = "04";
                    taxPayerDetails.UserTypx = "TP";
                    var taxPayerDetailsResult = await EstablishmentRegistrationWebServiceManager.ESTTaxPayerDetailPostService(taxPayerDetails);

                    IsLoading = false;
                    return true;
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.Declaration)
                {
                    taxPayerDetails?.off_notesSet?.results?.Clear();
                    taxPayerDetails.Decfg = "X";
                    taxPayerDetails.Operationx = "01";
                    taxPayerDetails.Gpartx = App.LoginDataRetrieved.TIN;
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
                ex.ToString();
                if (ex is HTTPBadRequestException)
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                }
                return false;
            }
            return false;
        }

        public void RentAttachmentPrePopulateCheck(TaxPayerDetails taxPayerDetails)
        {
            UploadedRentDocumentsList.Clear();
            var docRentResult = taxPayerDetails.AttDetSet.results.Where(x => x.Dotyp == "RG16").ToList();

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
            var docPassportResult = taxPayerDetails.AttDetSet.results.Where(x => x.Dotyp == "RG19").ToList();

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
        private string HijriDateString(DateTime date)
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
