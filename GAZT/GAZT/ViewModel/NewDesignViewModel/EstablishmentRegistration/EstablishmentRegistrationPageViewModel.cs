using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.Models.EstablishmentRegistration;
using EGAZT.Views.NewDesign.Common;
using EGAZT.Views.NewDesign.EstablishmentRegistrationPages;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using Plugin.FilePicker;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.EstablishmentRegistration
{
    public class EstablishmentRegistrationPageViewModel : BaseViewModel, INotifyPropertyChanged
    {
        #region Variable
        private TaxPayerDetails taxPayerDetails { get; set; } = null;
        private FinancialDetail financialDetail { get; set; } = null;
        private OutletNumber number;
        private EstablishmentRegistrationTabsEnum _currentTab = EstablishmentRegistrationTabsEnum.FinancialDetail;
        public EstablishmentRegistrationTabsEnum currentTab
        {
            get => _currentTab;
            set
            {
                _currentTab = value;
                RaisePropertyChanged(nameof(currentTab));
                CurrentIndex = (int)_currentTab;
                RaisePropertyChanged(nameof(CurrentIndex));
                switch (value)
                {
                    case EstablishmentRegistrationTabsEnum.TaxpayerDetail:
                        SelectedTabText = AppResources.ESTTaxpayerPersonalDetailsTabTitleLabel;
                        break;
                    case EstablishmentRegistrationTabsEnum.PassportDetails:
                        SelectedTabText = AppResources.ESTPassportDetailsTabTitleLabel;
                        break;
                    case EstablishmentRegistrationTabsEnum.Outlets:
                        SelectedTabText = AppResources.ESTOutletsTabTitleLabel;
                        break;
                    case EstablishmentRegistrationTabsEnum.FinancialDetail:
                        SelectedTabText = AppResources.VATRFinancialDetails;
                        break;
                    case EstablishmentRegistrationTabsEnum.Declaration:
                        SelectedTabText = AppResources.ZVatSummary;
                        break;
                    case EstablishmentRegistrationTabsEnum.RegistrationType:
                    default:
                        SelectedTabText = AppResources.ESTRegTaxTabTitleLabel;
                        break;
                }
                fetchTabDataAndBind(_currentTab);
            }
        }
        public ObservableCollection<string> TabList { get; set; }
            = new ObservableCollection<string>{ AppResources.ESTRegTaxTabTitleLabel, AppResources.ESTTaxpayerPersonalDetailsTabTitleLabel,
                AppResources.ESTPassportDetailsTabTitleLabel, AppResources.ESTOutletsTabTitleLabel,
                AppResources. VATRFinancialDetails, AppResources.ZVatSummary };
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



        private int _currenrIndex = (int)EstablishmentRegistrationTabsEnum.FinancialDetail;
        public int CurrentIndex
        {
            get => _currenrIndex;
            set
            {
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
                _selectedTabText = value;
                RaisePropertyChanged(nameof(SelectedTabText));
            }
        }

        #region Registration Details Tab Variables

        private bool _isClickedOwnRentOption = false;
        public bool IsClickedOwnRentOption
        {
            get => _isClickedOwnRentOption;
            set
            {
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
                _selectedOrgNonResidentOptions = value;
                RaisePropertyChanged(nameof(SelectedOrgNonResidentOptions));
            }
        }
        //

        private string _selectedOrgNonResidentActivity = null;
        public string SelectedOrgNonResidentActivity
        {
            get => _selectedOrgNonResidentActivity;
            set
            {
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
                _selectedOrgNonResidentActivityItem = value;

                if (SelectedOrgNonResidentActivityItem!=null)
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
                _reportingBranchList = value;
                RaisePropertyChanged(nameof(ReportingBranchList));
            }
        }


        private string _selectedEntityType = "Individual";
        public string SelectedEntityType
        {
            get => _selectedEntityType;
            set
            {
                _selectedEntityType = value;
                RaisePropertyChanged(nameof(SelectedEntityType));
            }
        }

        private string _selectedTaxPayerType = "Trade/Business";
        public string SelectedTaxPayerType
        {
            get => _selectedTaxPayerType;
            set
            {
                _selectedTaxPayerType = value;
                RaisePropertyChanged(nameof(SelectedTaxPayerType));
            }
        }

        private string _selectedRegNationalityType = "GCC";
        public string SelectedRegNationalityType
        {
            get => _selectedRegNationalityType;
            set
            {
                _selectedRegNationalityType = value;
                RaisePropertyChanged(nameof(SelectedRegNationalityType));
            }
        }

        private string _selectedLegalEntity;
        public string SelectedLegalEntity
        {
            get => _selectedLegalEntity;
            set
            {
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
                _selectedGender = value;
                RaisePropertyChanged(nameof(SelectedGender));
            }
        }

        Dictionary<string, string> EnIDType = new Dictionary<string, string>() {
            { "ZS0001", "National ID" },
            { "ZS0002", "Iqama ID" },
            { "ZS0003", "National ID" }
        };
        Dictionary<string, string> ArIDType = new Dictionary<string, string>() {
            { "ZS0001", "رقم الهوية الوطنية" },
            { "ZS0002", "رقم الإقامة" },
            { "ZS0003", "رقم هوية مواطني دول الخليج" }
        };

        private string _gCCIDType = "GCC ID";
        public string GCCIDType
        {
            get => _gCCIDType;
            set
            {
                _gCCIDType = value;
                RaisePropertyChanged(nameof(GCCIDType));
            }
        }

        private string _gCCIDTypeIdNumberValue = "12345677899";
        public string GCCIDTypeIdNumberValue
        {
            get => _gCCIDTypeIdNumberValue;
            set
            {
                _gCCIDTypeIdNumberValue = value;
                RaisePropertyChanged(nameof(GCCIDTypeIdNumberValue));
            }
        }

        private string _selectedDOB = "26/08/2020";
        public string SelectedDOB
        {
            get => _selectedDOB;
            set
            {
                _selectedDOB = value;
                RaisePropertyChanged(nameof(SelectedDOB));
            }
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
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
                _selectedTaxpayerPDNationality = value;
                RaisePropertyChanged(nameof(SelectedTaxpayerPDNationality));
            }
        }

        //private List<string> _citizenList;
        //public List<string> CitizenList
        //{
        //    get => _citizenList;
        //    set
        //    {
        //        _citizenList = value;
        //        RaisePropertyChanged(nameof(CitizenList));
        //    }
        //}

        private TaxpayerNationalityLandx50 _selectedCitizen;
        public TaxpayerNationalityLandx50 SelectedCitizen
        {
            get => _selectedCitizen;
            set
            {
                _selectedCitizen = value;
                RaisePropertyChanged(nameof(SelectedCitizen));
            }
        }

        //private List<string> _residenceList;
        //public List<string> ResidenceList
        //{
        //    get => _residenceList;
        //    set
        //    {
        //        _residenceList = value;
        //        RaisePropertyChanged(nameof(ResidenceList));
        //    }
        //}

        private TaxpayerNationalityLandx50 _selectedResidence;
        public TaxpayerNationalityLandx50 SelectedResidence
        {
            get => _selectedResidence;
            set
            {
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
                _passportNumber = value;
                RaisePropertyChanged(nameof(PassportNumber));
            }
        }

        //private List<TaxpayerNationality> _passportIssueCountryList;
        //public List<TaxpayerNationality> PassportIssueCountryList
        //{
        //    get => _passportIssueCountryList;
        //    set
        //    {
        //        _passportIssueCountryList = value;
        //        RaisePropertyChanged(nameof(PassportIssueCountryList));
        //    }
        //}

        private TaxpayerNationality _selectedPassportIssueCountry;
        public TaxpayerNationality SelectedPassportIssueCountry
        {
            get => _selectedPassportIssueCountry;
            set
            {
                _selectedPassportIssueCountry = value;
                RaisePropertyChanged(nameof(SelectedPassportIssueCountry));
            }
        }


        private string _passportIssueDate;
        public string PassportIssueDate
        {
            get => _passportIssueDate;
            set
            {
                _passportIssueDate = value;
                RaisePropertyChanged(nameof(PassportIssueDate));
            }
        }

        private string _passportExpireDate;
        public string PassportExpireDate
        {
            get => _passportExpireDate;
            set
            {
                _passportExpireDate = value;
                RaisePropertyChanged(nameof(PassportExpireDate));
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
                _passportFileName = value;
                RaisePropertyChanged(nameof(SelectedPassportFileName));
            }
        }
        #endregion

        #region Outlet variables
        private ObservableCollection<OutletItem> _outletData = new ObservableCollection<OutletItem>();
        public ObservableCollection<OutletItem> OutletData
        {
            get => _outletData;
            set
            {
                if(value != null && value.Count > 0)
                {
                    _outletData = value;
                    RaisePropertyChanged(nameof(OutletData));
                }
            }
        }
        #endregion

        #region Financial Details Tabs variables
        private List<string> dates = new List<string> { "LD", "30", "29", "28", "27", "26", "25", "24", "23", "22", "21", "20", "19", "18", "17", "16", "15", "14", "13", "12", "11", "10", "09", "08", "07", "06", "05", "04", "03", "02", "01" };

        private Dictionary<string, string> EnMethodList = new Dictionary<string, string>()
        {
            {"A", "Accounting" },
            {"E", "Estimated" }
        };
        private Dictionary<string, string> EnCalendarTypeList = new Dictionary<string, string>()
        {
            {"2", "hijri" },
            {"1", "Gregorian" }
        };
        private List<string> _methodList = new List<string>();
        public List<string> MethodList
        {
            get => _methodList;
            set
            {
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
                if (value != null)
                {
                    _calendarType = value;
                    RaisePropertyChanged(nameof(CalendarType));
                    if(taxPayerDetails != null)
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
                if (value != null)
                {
                    _outletList = value;
                    RaisePropertyChanged(nameof(OutletList));
                }
            }
        }
        #endregion

        #endregion



        #region Commands


        public ICommand OnNextButtonClick { get; set; }
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

        public ICommand OnPassportCloseTapped { get; set; }

        public ICommand OnDeleteAttachmentClickedTapped { get; set; }
        #endregion


        #region Outlet Tabs commands
        public ICommand OnNewOutletButtonClick { get; set; }
        #endregion


        #region Financial Details Tabs commands
        public ICommand OnMonthSelectButtonClick { get; set; }
        public ICommand OnDaySelectButtonClick { get; set; }
        #endregion


        #region Summary Tabs commands
        public ICommand OnExpendGridViewClick { get; set; }
        #endregion

        #endregion

        #region Constructor
        public EstablishmentRegistrationPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            OnNextButtonClick = new Command(() => navigateToNext());
            OnPreButtonClick = new Command(() => navigateToPre());

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

           

            #region Outlet Tabs variable initialization
            OnNewOutletButtonClick = new Command(() => openNewOutlet());
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
                poupWindow.OnItemSelect = async(item) => {
                    FiscalMonth = item as string;
                };
                PopupNavigation.Instance.PushAsync(poupWindow);
            });
            OnDaySelectButtonClick = new Command(() =>
            {
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(dates);
                poupWindow.OnItemSelect = (item) => {
                    FiscalDay = item as string;
                    udpdateDates(FiscalDay);
                };
                PopupNavigation.Instance.PushAsync(poupWindow);
            });
            #endregion

            #region Summary Tabs variable initialization
            OnExpendGridViewClick = new Command((_enum) => OnExpandCollapseGridViewClick(_enum));
            OutletList.Clear();
            OutletList.Add("1");
            OutletList.Add("2");

            OnVoidOrSaveDraftClick = new Command(() =>
            {
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(new List<string> { AppResources.Save, AppResources.ZZVoid });
                poupWindow.OnItemSelect = async (item) => {
                    var actionName = item as string;
                    Console.WriteLine(item);
                    if(actionName == AppResources.Save)
                    {
                        IsLoading = true;
                        taxPayerDetails.Draftfg = "X";
                        taxPayerDetails = await WebServiceManager.ESTTaxPayerDetailPostService(taxPayerDetails);
                        IsLoading = false;
                    }
                    if (actionName == AppResources.ZZVoid)
                    {
                        IsLoading = true;
                        taxPayerDetails.Operationx = "04";
                        taxPayerDetails = await WebServiceManager.ESTTaxPayerDetailPostService(taxPayerDetails);
                        IsLoading = false;
                    }
                };
                PopupNavigation.Instance.PushAsync(poupWindow);
            });
            #endregion
        }

        #endregion

        #region Method
        public async void OnAppearing()
        {
            var branchTask = GetReportingBranchListFromServer();
            var nationalityTask = GetPdNationalityListFromServer(null);
            await Task.WhenAll(branchTask, nationalityTask);
            if (taxPayerDetails == null)
            {
                fetchTabDataAndBind(currentTab);
            }
        }

        private async void navigateToNext()
        {
            try
            {
                var fillMandatory = "Please fill all mandatory Fields.";
                var failedMesage = "Failed to push the data to server";
                if (currentTab == EstablishmentRegistrationTabsEnum.TaxpayerDetail)
                {
                    if (FormValidation(currentTab))
                    {
                        if (await PushDatatoServer(currentTab))
                        {
                            currentTab = EstablishmentRegistrationTabsEnum.PassportDetails;
                        }
                        else
                        {
                            ShowValidationPopup(failedMesage);
                        }
                    }
                    else
                    {
                        ShowValidationPopup(fillMandatory);

                    }
                }
                else if (currentTab == EstablishmentRegistrationTabsEnum.PassportDetails)
                {
                    if (FormValidation(currentTab))
                    {
                        if (await PushDatatoServer(currentTab))
                        {
                            currentTab = EstablishmentRegistrationTabsEnum.Outlets;
                        }
                        else
                        {
                            ShowValidationPopup(failedMesage);
                        }
                    }
                    else
                    {
                        ShowValidationPopup(fillMandatory);

                    }
                }
                else if (currentTab == EstablishmentRegistrationTabsEnum.Outlets)
                {
                    currentTab = EstablishmentRegistrationTabsEnum.FinancialDetail;
                }
                else if (currentTab == EstablishmentRegistrationTabsEnum.FinancialDetail)
                {
                    if (FormValidation(currentTab))
                    {
                        if (await PushDatatoServer(currentTab))
                        {
                            currentTab = EstablishmentRegistrationTabsEnum.Declaration;
                        }
                        else
                        {
                            ShowValidationPopup(failedMesage);
                        }
                    }
                    else
                    {
                        ShowValidationPopup(fillMandatory);

                    }
                }
                else if (currentTab == EstablishmentRegistrationTabsEnum.RegistrationType)
                {
                    if (FormValidation(currentTab))
                    {
                        if (await PushDatatoServer(currentTab))
                        {
                            currentTab = EstablishmentRegistrationTabsEnum.TaxpayerDetail;
                        }
                        else
                        {
                            ShowValidationPopup(failedMesage);// _dialogService.ShowMessage("Failed to push the data to server", AppResources.Information);

                        }
                    }
                    else
                    {
                        ShowValidationPopup(fillMandatory);
                    }

                }
                else if (currentTab == EstablishmentRegistrationTabsEnum.Declaration)
                {
                    if(await PushDatatoServer(currentTab))
                    {
                        _navigationService.NavigateTo(App.RegistrationSuccessfulPage, taxPayerDetails);
                    }
                    else
                    {
                        ShowValidationPopup(failedMesage);
                    }
                }
            }
            catch(Exception e)
            {

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
                currentTab = EstablishmentRegistrationTabsEnum.PassportDetails;
            }
            else if (currentTab == EstablishmentRegistrationTabsEnum.FinancialDetail)
            {
                currentTab = EstablishmentRegistrationTabsEnum.Outlets;
            }
            else if (currentTab == EstablishmentRegistrationTabsEnum.Declaration)
            {
                currentTab = EstablishmentRegistrationTabsEnum.FinancialDetail;
            }
        }


        private  void ShowValidationPopup(string _message)
        {
            _dialogService.ShowError(_message, AppResources.Information,"Ok",null);
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
                    SelectedOrgNonResidentActivityItem =  string.Empty;
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
            if (ReportingBranchList == null || ReportingBranchList?.Count == 0)
            {
                ReportingBranchList = await WebServiceManager.ESTBranchesDropDown();
            }

        }


        private async Task GetPdNationalityListFromServer(string nationality)
        {
            TaxpayerFullNationlityList = await WebServiceManager.ESTTaxPayerNationality(nationality);
            TaxpayerPDNationlityList = new List<TaxpayerNationalityLandx50>();
            TaxpayerFullNationlityList.ForEach(i => TaxpayerPDNationlityList.Add((TaxpayerNationalityLandx50)i));
        }



        public void OnRentAttachmentCloseTapped(Attachment obj)
        {
            UploadedRentDocumentsList.Remove(obj);
            RaisePropertyChanged(nameof(UploadedRentDocumentsList));
            if (UploadedRentDocumentsList==null ||  UploadedRentDocumentsList.Count()==0)
            {
                IsVisbleRentAttachmentmentList = false;

            }

        }

        public async void OnRentAddAttachmentTapped()
        {
            if (UploadedRentDocumentsList.Count()<5)
            {
                await AddAttachment("RG16");
                if (UploadedRentDocumentsList.Count > 0)
                {
                    IsVisbleRentAttachmentmentList = true;
                }
            }
            else
            {
                ShowValidationPopup("You can add max 5 attachments only");
            }
          
        }

        public void OnPassportCloseButtonTapped(Attachment obj)
        {
            UploadedPassportDocumentsList.Remove(obj);
            RaisePropertyChanged(nameof(UploadedPassportDocumentsList));
            if (UploadedPassportDocumentsList==null ||  UploadedPassportDocumentsList.Count() == 0)
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
                ShowValidationPopup("You can add max 5 attachments only");
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


        private async Task AddAttachment(string docType)
        {
            try
            {
                decimal TotalAttachmentSize = 0;
                string[] filetypes = DependencyService.Get<IDeviceInfo>().GetAttachmentTypeStringForTaxEvasion();

                var fileData = await CrossFilePicker.Current.PickFile(filetypes);

                if (fileData != null)
                {
                    var attachmentByte = fileData.DataArray;

                    string base64String = Convert.ToBase64String(attachmentByte, 0, attachmentByte.Length);
                    var attachmentName = fileData.FileName;

                    float sizemb = (attachmentByte.Length / 1024f) / 1024f;
                    decimal attachmentSize = 0;
                    attachmentSize = attachmentSize + (Decimal)sizemb;

                    if (fileData.FileName.Contains("."))
                    {
                        string Extention = fileData.FileName.Split('.')[1];//pdf
                        if (Extention.ToLower() == "doc" || Extention.ToLower() == "docx" || Extention.ToLower() == "jpg" || Extention.ToLower() == "pdf" || Extention.ToLower() == "jpeg")
                        {
                            if (TotalAttachmentSize <= 30)
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
                                            //if (docType == "RG01")
                                            //{
                                            //    CRsCopies.Add(new Attachment());
                                            //}
                                            //else if (docType == "RG12")
                                            //{
                                            //    TransferCRsCopies.Add(new Attachment());
                                            //}
                                            //else if (docType == "RG02")
                                            //{
                                            //    LicensesCopies.Add(new Attachment());
                                            //}
                                        }
                                        catch (Exception ex)
                                        {
                                        }
                                    }
                                    else
                                    {
                                        attachmentName = string.Empty;
                                        await _dialogService.ShowMessage(AppResources.Somethingwentwrong, AppResources.Information);
                                    }
                                }
                            }
                        }
                        else
                        {
                            await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly, AppResources.Information);
                        }
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

                Attachment dd = await WebServiceManager.ESTAttachment(attachmentByteData, fileName, taxPayerDetails?.ReturnIdx, docType, contentType, null);

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

        private async void fetchTabDataAndBind(EstablishmentRegistrationTabsEnum _enum)
        {
            try
            {
                IsLoading = true;
                if (_enum == EstablishmentRegistrationTabsEnum.RegistrationType)
                {
                    taxPayerDetails = await WebServiceManager.ESTTaxPayerDetailGetService("01", "3102448184", "DKOTHI-C@GAZT.GOV.SA");
                    if (!string.IsNullOrEmpty(taxPayerDetails?.Fbsta) && taxPayerDetails?.Fbsta != "IP011" && taxPayerDetails?.UserTypx == "TP")
                    {
                        IsLoading = false;
                        _navigationService.NavigateTo(App.RegistrationSuccessfulPage, taxPayerDetails);
                    }
                    SelectedReportingBranch = ReportingBranchList.Where(i => i.Augrp == taxPayerDetails?.Augrp).FirstOrDefault();
                    SelectedEntityType = Int16.Parse(taxPayerDetails?.Atype) == 1 ? "Individual" : "Company";
                    SelectedRegNationalityType = taxPayerDetails?.Tpnationality;
                    
                    ResidenceTypePrePopulateData(taxPayerDetails);
                    RentAttachmentPrePopulateCheck(taxPayerDetails);
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.TaxpayerDetail)
                {

                    await GetPdNationalityListFromServer(taxPayerDetails?.Tpnationality);
                    taxPayerDetails = await WebServiceManager.ESTTaxPayerDetailGetService("02", "3102448184", "DKOTHI-C@GAZT.GOV.SA");
                    Nreg_IdItem idItem = taxPayerDetails?.Nreg_IdSet.results.Where(i => EnIDType.ContainsKey(i.Type)).FirstOrDefault();
                    GCCIDType = EnIDType[idItem?.Type];
                    GCCIDTypeIdNumberValue = idItem.Idnumber;
                    SelectedDOB = taxPayerDetails?.Birthdt?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                    FirstName = taxPayerDetails?.NameFirst;
                    LastName = taxPayerDetails?.NameLast;
                    FatherName = taxPayerDetails?.FatherName;
                    GrandFatherName = taxPayerDetails?.GrandfatherName;
                    FamilyName = taxPayerDetails?.FamilyName;
                    Initial = taxPayerDetails?.Initials;
                    if (taxPayerDetails?.Xsexm == "X")
                        SelectedGender = GenderList.FirstOrDefault();
                    if (taxPayerDetails?.Xsexf == "X")
                        SelectedGender = GenderList.LastOrDefault();

                    SelectedTaxpayerPDNationality = TaxpayerFullNationlityList.Where(i => i.Land1 == taxPayerDetails?.Natio).FirstOrDefault();
                    SelectedCitizen = TaxpayerFullNationlityList.Where(i => i.Land1 == taxPayerDetails?.Citizen).FirstOrDefault();
                    SelectedResidence = TaxpayerFullNationlityList.Where(i => i.Land1 == taxPayerDetails?.Residence).FirstOrDefault();
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.PassportDetails)
                {
                    taxPayerDetails = await WebServiceManager.ESTTaxPayerDetailGetService("02", "3102448184", "DKOTHI-C@GAZT.GOV.SA");
                    Nreg_IdItem passportItem = taxPayerDetails?.Nreg_IdSet.results.Where(i => i.Type == "FS0002").FirstOrDefault();
                    PassportNumber = passportItem.Idnumber;
                    SelectedPassportIssueCountry = TaxpayerFullNationlityList.Where(i => i.Land1 == passportItem?.Country).FirstOrDefault();
                    PassportIssueDate = passportItem?.ValidDateFrom?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                    PassportExpireDate = passportItem?.ValidDateTo?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                    PassportAttachmentPrepopulateCheck(taxPayerDetails);
                }
                else if(_enum == EstablishmentRegistrationTabsEnum.Outlets)
                {
                    Device.BeginInvokeOnMainThread(() => bindingOutletList());

                    number = await WebServiceManager.ESTOutletNumber(taxPayerDetails?.Fbnumx);
                    taxPayerDetails = await WebServiceManager.ESTTaxPayerDetailGetService("03", "3102448184", "DKOTHI-C@GAZT.GOV.SA", $"{Int16.Parse(number?.Actno):000}", taxPayerDetails?.Fbnumx);
                    //await WebServiceManager.ESTOutletDropDowns();
                    //await WebServiceManager.ESTOutletGetActivitySetsList();
                    //ValidateCR crItem = await WebServiceManager.ESTValidateCRNum(taxPayerDetails?.Nreg_ActivitySet.results?.FirstOrDefault()?.Idnumber);
                }
                else if(_enum == EstablishmentRegistrationTabsEnum.FinancialDetail)
                {
                    taxPayerDetails = await WebServiceManager.ESTTaxPayerDetailGetService("04", "3102448184", "DKOTHI-C@GAZT.GOV.SA");
                    SelectedMethod = EnMethodList[taxPayerDetails?.Accmethod];
                    CalendarType = EnCalendarTypeList[taxPayerDetails?.Fdcalender];
                    //udpdateDates();
                    //FiscalMonth = taxPayerDetails?.Fdmonth;
                    //FiscalDay = taxPayerDetails?.Fdday;
                    //CommDate = taxPayerDetails?.Commdt?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                    //TaxDate = taxPayerDetails?.Fdenddt?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                    //if (string.IsNullOrEmpty(TaxDate))
                    //{
                    //    if(taxPayerDetails?.Accmethod == "E")
                    //    {
                    //        updateDatesAccordingMethods();
                    //    }
                    //}
                }
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
        //private void updateDatesAccordingMethods()
        //{
        //    DateTime _new = new DateTime((long)(taxPayerDetails.Commdt?.AddDays(-1).AddYears(1).Ticks));
        //    TaxDate = _new.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
        //    FiscalMonth = _new.Month.ToString();
        //    FiscalDay = _new.Day.ToString();
        //}
        private async void udpdateDates(string selectedDate = null)
        {
            IsLoading = true;
            var _calendarType = EnCalendarTypeList.FirstOrDefault(i => i.Value == CalendarType).Key == "2" ? "H" : "G";
            financialDetail = await WebServiceManager.ESTFinancialMaxDate(new FinancialDetailRequest()
            {
                ACaltype = _calendarType,
                ADateComm = taxPayerDetails?.Commdt
            });
            CommDate = string.Format("{0:0000/00/00}", Int64.Parse(financialDetail?.ACommDate));


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
                    FiscalDay = "LD";
                }
                else
                {
                    mm = $"{Int16.Parse(mm) - 1:00}";
                    FiscalMonth = mm;
                    FiscalDay = "LD";
                }
                FiscalMonth = mm;
                FiscalDay = dd;
            }
            financialDetail = await WebServiceManager.ESTFinancialMaxDate(new FinancialDetailRequest()
            {
                ACaltype = _calendarType,
                AMonth = FiscalMonth,
                EIslmedate = FiscalDay == "LD" ? "32" : FiscalDay,
                ADateComm = taxPayerDetails?.Commdt
            });
            TaxDate = string.Format("{0:0000/00/00}", Int64.Parse(_calendarType == "H" ? financialDetail?.ACommDate : financialDetail?.EIsldate));

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
        private async void bindingOutletList()
        {
            var _outletTempData = await WebServiceManager.ESTOutletList(taxPayerDetails?.PortalUsrx, "3102448184", taxPayerDetails?.Fbnumx);
            if (_outletTempData.Count > 0)
            {
                OutletData.Clear();
                _outletTempData.ForEach(_out => OutletData.Add(_out));
            }
        }
        private void openNewOutlet()
        {
            OutletNavigationModels outletNavigationModels = new OutletNavigationModels();
            //if (OutletData?.Count > 0)
            //{
            //outletNavigationModels.openedTab = EstablishmentRegistrationOutletTabsEnum.AddressDetails;
            //}
            outletNavigationModels.taxPayerDetails = taxPayerDetails;
            //outletNavigationModels.nextNumber = number;
            _navigationService.NavigateTo(App.OutletDetailsPageView, outletNavigationModels);
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

                if (taxPayerDetails.Orgnonresident=="1")
                {
                    OrgNonResidentSelection(OrgNonResidentEstablishmentRegistrationEnum.PermanentEstablishment);

                    if (taxPayerDetails.Orgnonresidentoptions=="1")
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
                    if (taxPayerDetails.Orgnonresidentactivity== "O1")
                    {
                        SelectedOrgNonResidentActivityItem = AppResources.ESTOrgNonResidentActivityValueOne;
                    }
                    else if (taxPayerDetails.Orgnonresidentactivity == "O2")
                    {
                        SelectedOrgNonResidentActivityItem =  AppResources.ESTOrgNonResidentActivityValueTwo;
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

        private bool FormValidation(EstablishmentRegistrationTabsEnum _enum)
        {
            try
            {
                if (_enum == EstablishmentRegistrationTabsEnum.RegistrationType)
                {
                    if (SelectedReportingBranch == null)
                    {
                        return false;
                    }
                    else if (string.IsNullOrEmpty(SelectedTpresidence))
                    {
                        return false;
                    }
                    else if (SelectedTpresidence == "2" && (UploadedRentDocumentsList==null || UploadedRentDocumentsList.Count <= 0))
                    {
                        return false;
                    }
                    else if (SelectedTpresidence == "3" && string.IsNullOrEmpty(SelectedOrgNonResident))
                    {
                        return false;
                    }
                    else if (SelectedTpresidence == "3" && SelectedOrgNonResident == "1" && string.IsNullOrEmpty(SelectedOrgNonResidentOptions))
                    {
                        return false;
                    }
                    else if (SelectedTpresidence == "3" && SelectedOrgNonResident == "2" && string.IsNullOrEmpty(SelectedOrgNonResidentActivity))
                    {
                        return false;
                    }
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.TaxpayerDetail)
                {

                    if (string.IsNullOrEmpty(SelectedDOB))
                    {
                        return false;
                    }
                    else if (string.IsNullOrEmpty(FirstName))
                    {
                        return false;
                    }
                    else if (string.IsNullOrEmpty(SelectedGender))
                    {
                        return false;
                    }
                    else if (null == SelectedTaxpayerPDNationality)
                    {
                        return false;
                    }
                    else if (null == SelectedCitizen)
                    {
                        return false;
                    }
                    else if (null == SelectedResidence)
                    {
                        return false;
                    }
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.PassportDetails)
                {
                    if (string.IsNullOrEmpty(PassportNumber))
                    {
                        return false;
                    }
                    else if (null == SelectedPassportIssueCountry)
                    {
                        return false;
                    }
                    else if (string.IsNullOrEmpty(PassportIssueDate))
                    {
                        return false;
                    }
                    else if (string.IsNullOrEmpty(PassportExpireDate))
                    {
                        return false;
                    }
                    else if (UploadedPassportDocumentsList==null || UploadedPassportDocumentsList.Count<=0)
                    {
                        return false;
                    }
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.Outlets)
                {


                }
                else if (_enum == EstablishmentRegistrationTabsEnum.FinancialDetail)
                {


                }
                else if (_enum == EstablishmentRegistrationTabsEnum.Declaration)
                {


                }

            }
            catch(Exception ex)
            {
                ex.ToString();
            }
            return true;
        }


        private async  Task<bool> PushDatatoServer(EstablishmentRegistrationTabsEnum _enum)
        {
            
            try
            {
                IsLoading = true;
                if (_enum == EstablishmentRegistrationTabsEnum.RegistrationType)
                {
                    return true;
                    taxPayerDetails.Augrp = SelectedReportingBranch?.Augrp;
                    taxPayerDetails.Atype = SelectedEntityType.Equals("Individual") ? "1" : "2";
                    taxPayerDetails.Tpnationality = SelectedRegNationalityType;
                    taxPayerDetails.Taxtpdetermination = "1";
                    taxPayerDetails.Tpresidence = SelectedTpresidence;
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
                    var taxPayerDetailsResult = await WebServiceManager.ESTTaxPayerDetailPostService(taxPayerDetails);

                    IsLoading = false;
                    return true;
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.TaxpayerDetail)
                {
                    return true;
                    DateTime.TryParseExact(SelectedDOB, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime dob);
                    taxPayerDetails.Birthdt = dob;
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
                    taxPayerDetails.Citizen = SelectedCitizen?.Land1;
                    taxPayerDetails.Residence = SelectedResidence?.Land1;

                    taxPayerDetails.StepNumberx = "02";
                    var taxPayerDetailsResult = await  WebServiceManager.ESTTaxPayerDetailPostService(taxPayerDetails);


                    IsLoading = false;
                    return true;
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.PassportDetails)
                {
                    return true;
                    Nreg_IdItem passportObj = new Nreg_IdItem();
                    passportObj.Idnumber = PassportNumber;
                    passportObj.Country = SelectedPassportIssueCountry?.Land1;

                    DateTime.TryParseExact(PassportIssueDate, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime issueDate);
                    DateTime.TryParseExact(PassportExpireDate, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime expireDate);
                    passportObj.ValidDateFrom = issueDate;
                    passportObj.ValidDateTo = expireDate;
                    passportObj.Type = "FS0002";
                    passportObj.Srcidentify = "000";

                    taxPayerDetails.Nreg_IdSet.results.Clear();
                    taxPayerDetails.Nreg_IdSet.results.Add(passportObj);

                    if (UploadedPassportDocumentsList != null && UploadedPassportDocumentsList.Count > 0)
                    {

                        taxPayerDetails.Passatt = "X";
                    }
                    else
                    {
                        taxPayerDetails.Passatt = "";
                    }

                    taxPayerDetails.StepNumberx = "02";

                    var taxPayerDetailsResult = await WebServiceManager.ESTTaxPayerDetailPostService(taxPayerDetails);

                    IsLoading = false;
                    return true;
                }
                else if(_enum == EstablishmentRegistrationTabsEnum.FinancialDetail)
                {

                    //DateTime.TryParseExact(CommDate, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime Commdt);
                    DateTime.TryParseExact(TaxDate, string.Format("{0:0000/00/00}", Int64.Parse(financialDetail?.EIsldate)), new CultureInfo("en-US"), DateTimeStyles.None, out DateTime Fdenddt);
                    taxPayerDetails.Accmethod = EnMethodList.FirstOrDefault(i => i.Value == SelectedMethod).Key;
                    taxPayerDetails.Fdcalender = EnCalendarTypeList.FirstOrDefault(i => i.Value == CalendarType).Key;
                    taxPayerDetails.Fdmonth = FiscalMonth;
                    taxPayerDetails.Fdday = FiscalDay;
                    taxPayerDetails.Commdt = financialDetail?.ADateComm;
                    taxPayerDetails.Fdenddt = Fdenddt;

                    taxPayerDetails.Gpartx = "3102448184";
                    taxPayerDetails.StepNumberx = "04";
                    var taxPayerDetailsResult = await WebServiceManager.ESTTaxPayerDetailPostService(taxPayerDetails);

                    IsLoading = false;
                    return true;
                }
                else if(_enum == EstablishmentRegistrationTabsEnum.Declaration)
                {
                    taxPayerDetails.Decfg = "X";
                    taxPayerDetails.Operationx = "01";
                    taxPayerDetails = await WebServiceManager.ESTTaxPayerDetailPostService(taxPayerDetails);

                    IsLoading = false;
                    return true;
                }

                IsLoading = false;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                IsLoading = false;
            }
            return false;
        }

        public void RentAttachmentPrePopulateCheck(TaxPayerDetails taxPayerDetails)
        {
            UploadedRentDocumentsList.Clear();
            var docRentResult = taxPayerDetails.AttDetSet.results.Where(x => x.Dotyp == "RG16").ToList();
          
            foreach (AttDetItem attDetItem in docRentResult )
            {
                var obj = new Attachment();
                obj.Filename = attDetItem.Filename;
                obj.FileExtn = attDetItem.FileExtn;
                obj.Mimetype = attDetItem.Mimetype;
                obj.RetGuid = attDetItem.RetGuid;
                obj.DocUrl = attDetItem.DocUrl;
                obj.Dotyp = attDetItem.Dotyp;
                UploadedRentDocumentsList.Add(obj);
            }

            if (UploadedRentDocumentsList.Count>0)
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

       
        
        #endregion
    }


}