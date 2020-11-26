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
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Newtonsoft.Json;
using Plugin.FilePicker;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.EstablishmentAmendUpdateViewModel
{
    public class EstablishmentAmendUpdatePageViewModel : BaseViewModel
    {

        #region Variable

        public string PageTitle { get; set; }
        public bool IsExceptionPopupVisible { get; set; } = false;
        public static TaxPayerDetails taxPayerDetails { get; set; } = null;
        private FinancialDetail financialDetail { get; set; } = null;
        public Nreg_IdItem idItem { get; set; } = null;
        public bool IsNavigationCompletedToSuccessfulPage { get; set; } = false;
        //private OutletNumber number;
        private EstablishmentRegistrationTabsEnum _currentTab;
        public EstablishmentRegistrationTabsEnum currentTab
        {
            get => _currentTab;
            set
            {
                if (_currentTab == value)
                {
                    if (!IsNavigationCompletedToSuccessfulPage)
                    {
                        // Task.Run((() => fetchTabDataAndBind(_currentTab)));
                        Device.BeginInvokeOnMainThread(async () => fetchTabDataAndBind(_currentTab));
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
                //Task.Run((() => fetchTabDataAndBind(_currentTab)));
                Device.BeginInvokeOnMainThread(async () => fetchTabDataAndBind(_currentTab));
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

        //public bool DatePickerInGregorian { get; set; } = true;

        private int _currenrIndex;
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


        private string _selectedEntityType = string.Empty;
        public string SelectedEntityType
        {
            get => _selectedEntityType;
            set
            {
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
                _selectedTaxPayerType = value;
                RaisePropertyChanged(nameof(SelectedTaxPayerType));
            }
        }
        public Dictionary<string, string> NationalityMapping = null;
        private string _selectedRegNationalityType = string.Empty;
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
        private bool _isSaudi = false;
        public bool IsSaudi
        {
            get => _isSaudi;
            set
            {
                _isSaudi = value;
                RaisePropertyChanged(nameof(IsSaudi));
            }
        }
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
                _gCCIDTypeIdNumberValue = value;
                RaisePropertyChanged(nameof(GCCIDTypeIdNumberValue));
            }
        }

        //private ObservableCollection<object> _todayDate;
        //public ObservableCollection<object> TodayDate
        //{
        //    get
        //    {
        //        return _todayDate;
        //    }
        //    set
        //    {
        //        _todayDate = value;
        //        RaisePropertyChanged("TodayDate");
        //    }
        //}

        private string _selectedDOB = string.Empty;
        public string SelectedDOB
        {
            get => _selectedDOB;
            set
            {
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

        private ObservableCollection<object> _selectedPassportIssueDate;
        public ObservableCollection<object> SelectedPassportIssueDate
        {
            get
            {
                return _selectedPassportIssueDate;
            }
            set
            {
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
        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set
            {
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
                    //SetUIAvailability();
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
        private bool _isDeclarationBtnEnabled;
        public bool IsDeclarationBtnEnabled
        {
            get => _isDeclarationBtnEnabled;
            set
            {
                _isDeclarationBtnEnabled = value;
                RaisePropertyChanged(nameof(IsDeclarationBtnEnabled));
            }
        }
        private bool _eSTLedge = false;
        public bool ESTLedge
        {
            get => _eSTLedge;
            set
            {
                _eSTLedge = value;
                IsDeclarationBtnEnabled = _eSTLedge;
                RaisePropertyChanged(nameof(ESTLedge));
            }
        }
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
        public ICommand OnEditOutletButtonClick { get; set; }
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
            OnEditOutletButtonClick = new Command((item) => openEditOutlet(item as OutletItem));
            OnNewOutletButtonClick = new Command(() => openNewOutlet());
            OnDeleteOutletButtonClick = new Command(async (item) =>
            {
                var newItem = item as OutletItem;
                string QuestionMark = string.Empty;
                if (App.IsArabic)
                {
                    QuestionMark = "؟";
                }
                else
                {
                    QuestionMark = "?";
                }
                var confirmPopup = new ZAKATOkCancelPopUpView(AppResources.ZZDeleteAttachmentConfirmationText + "   " + newItem.Actnm + QuestionMark)
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
                            if (SetDataForSaveDraft(_currentTab))
                            {
                                taxPayerDetails.Draftfg = "X";
                                taxPayerDetails.Gpartx = App.LoginDataRetrieved.TIN;
                                taxPayerDetails.UserTypx = "TP";

                                if (taxPayerDetails.Augrp == null)
                                    taxPayerDetails.Augrp = string.Empty;


                                var _taxPayerDetails = await WebServiceManager.ESTTaxPayerDetailPostService(taxPayerDetails);
                                if (_taxPayerDetails != null && !string.IsNullOrEmpty(_taxPayerDetails.Fbnumx))
                                {
                                    PopupNavigation.PushAsync(new SingleButtonPopupView(AppResources.OKText, "Application " + _taxPayerDetails.Fbnumx + " saved successfully"));
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.StackTrace);
                            if (e is HTTPBadRequestException)
                            {
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(e.Message));
                                // await _dialogService.ShowMessage(e.Message, AppResources.Information);
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
                                        var _taxPayerDetails = await WebServiceManager.ESTTaxPayerDetailPostService(taxPayerDetails);
                                        currentTab = EstablishmentRegistrationTabsEnum.Unknown;
                                        navigationService.NavigateTo(App.GAZTNewDesignDashBoardPageView);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.StackTrace);
                                        if (e is HTTPBadRequestException)
                                        {
                                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(e.Message));
                                            //await _dialogService.ShowMessage(e.Message, AppResources.Information);
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
            //var branchTask = GetReportingBranchListFromServer();
            //var nationalityTask = GetPdNationalityListFromServer(null);
            //await Task.WhenAll(branchTask, nationalityTask);
            //if (currentTab == EstablishmentRegistrationTabsEnum.Outlets)
            //{
            // Task.Run((() => fetchTabDataAndBind(EstablishmentRegistrationTabsEnum.RegistrationType)));
            //        Device.BeginInvokeOnMainThread(async () => fetchTabDataAndBind(EstablishmentRegistrationTabsEnum.RegistrationType));
            //bindingOutletList();
            // }
            SetUIAvailability();

            bindingOutletList();

        }

        void SetUIAvailability()
        {
            switch (App.ZAKATType)
            {
                case Enums.PageExecutionType.Amend:
                    RegTaxPayerTypeAvailability.ReportingBranch = false;
                    RegTaxPayerTypeAvailability.IsReportingBranchVisible = false;
                    RegTaxPayerTypeAvailability.EntityType = false;
                    RegTaxPayerTypeAvailability.TaxPayerType = false;
                    RegTaxPayerTypeAvailability.IsTaxPayerTypeVisible = false;
                    RegTaxPayerTypeAvailability.Nationality = false;
                    RegTaxPayerTypeAvailability.IsNationalityStatusVisible = true;
                    RegTaxPayerTypeAvailability.ResidencyStatus = true;

                    TaxPayerDetailsAvailability.DOB = true;
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
                case Enums.PageExecutionType.Update:
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

        //public async Task SetDefaultDate()
        //{
        //    ObservableCollection<object> todaycollection = new ObservableCollection<object>();
        //    //Select today dates

        //    if (DateTime.Now.Date.Day < 10)
        //        todaycollection.Add("0" + DateTime.Now.Date.Day);
        //    else
        //        todaycollection.Add(DateTime.Now.Date.Day.ToString());
        //    if (DateTime.Now.Date.Month < 10)
        //        todaycollection.Add("0" + DateTime.Now.Date.Month);
        //    else
        //        todaycollection.Add(DateTime.Now.Date.Month.ToString());
        //    todaycollection.Add(DateTime.Now.Date.Year.ToString());
        //    TodayDate = todaycollection;
        //    DefaultMonth = DateTime.Now.Date.Month;
        //}

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
                        //else
                        //{
                        //    ShowValidationPopup(failedMesage);
                        //}
                    }
                    //else
                    //{
                    //    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));
                    //}
                }
                else if (currentTab == EstablishmentRegistrationTabsEnum.PassportDetails)
                {
                    if (await FormValidation(currentTab))
                    {
                        if (await PushDatatoServer(currentTab))
                        {
                            currentTab = EstablishmentRegistrationTabsEnum.Outlets;
                        }
                        //else
                        //{
                        //    ShowValidationPopup(failedMesage);
                        //}
                    }
                    //else
                    //{
                    //    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));
                    //}
                }
                else if (currentTab == EstablishmentRegistrationTabsEnum.Outlets)
                {
                    if (await FormValidation(currentTab))
                    {
                        currentTab = EstablishmentRegistrationTabsEnum.FinancialDetail;
                    }
                    //else
                    //{
                    //    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));
                    //}
                }
                else if (currentTab == EstablishmentRegistrationTabsEnum.FinancialDetail)
                {
                    if (await PushDatatoServer(currentTab))
                    {
                        currentTab = EstablishmentRegistrationTabsEnum.Declaration;
                    }
                    //else
                    //{
                    //    ShowValidationPopup(failedMesage);
                    //}
                }
                else if (currentTab == EstablishmentRegistrationTabsEnum.RegistrationType)
                {
                    if (await FormValidation(currentTab))
                    {
                        if (await PushDatatoServer(currentTab))
                        {
                            currentTab = EstablishmentRegistrationTabsEnum.TaxpayerDetail;
                        }
                        //else
                        //{
                        //    ShowValidationPopup(failedMesage);// _dialogService.ShowMessage("Failed to push the data to server", AppResources.Information);

                        //}
                    }
                    //else
                    //{
                    //    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));
                    //}

                }
                else if (currentTab == EstablishmentRegistrationTabsEnum.Declaration)
                {
                    if (await FormValidation(currentTab))
                    {
                        if (await PushDatatoServer(currentTab))
                        {
                            _navigationService.NavigateTo(App.EstablishmentAmendUpdateSuccessfulPage, taxPayerDetails);
                        }
                        //else
                        //{
                        //    ShowValidationPopup(failedMesage);
                        //}
                    }
                    //else
                    //{
                    //    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZPleasefillallthemandatoryfields));
                    //}
                }

                SetUIAvailability();
            }
            catch (Exception e)
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
            SetUIAvailability();
        }


        private void ShowValidationPopup(string _message)
        {
            PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(_message));
            // _dialogService.ShowError(_message, AppResources.Information, "Ok", null);
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
            //if (ReportingBranchList == null || ReportingBranchList?.Count == 0)
            //{
            ReportingBranchList = await WebServiceManager.ESTBranchesDropDown();
            //}

        }


        private async Task GetPdNationalityListFromServer(string nationality)
        {
            TaxpayerFullNationlityList = await WebServiceManager.ESTTaxPayerNationality(nationality);
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
            return WebServiceManager.ESTDeleteAttachment(fileName, RetGuid, docType, docguid);
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
                                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Somethingwentwrong));
                                        // await _dialogService.ShowMessage(AppResources.Somethingwentwrong, AppResources.Information);
                                    }

                                }
                                else
                                {
                                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTAttachmentSizeNotfication));
                                    // await _dialogService.ShowMessage(AppResources.ESTAttachmentSizeNotfication, AppResources.Information);
                                }
                            }
                            else
                            {
                                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly));
                                //  await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly, AppResources.Information);
                            }
                        }
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZFileWithTheSameNameAlreadyExists));

                            //await _dialogService.ShowMessage(AppResources.ZZFileWithTheSameNameAlreadyExists, AppResources.Alerts);
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

        public async void fetchTabDataAndBind(EstablishmentRegistrationTabsEnum _enum)
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
                    //var nationalityTask = GetPdNationalityListFromServer(null);
                    //await Task.WhenAll(branchTask, nationalityTask);
                    taxPayerDetails = await WebServiceManager.ESTTaxPayerDetailGetService("01", App.LoginDataRetrieved.TIN, App.LoginDataRetrieved.Emailid);
                    if (!string.IsNullOrEmpty(taxPayerDetails?.Fbsta) && taxPayerDetails?.Fbsta != "IP011")
                    {
                        //if (!IsNavigationCompletedToSuccessfulPage)
                        //{

                        // IsNavigationCompletedToSuccessfulPage = true;
                        // _navigationService.NavigateTo(App.EstablishmentAmendUpdateSuccessfulPage, taxPayerDetails);
                        string message = string.Empty;
                        message = AppResources.ZDearTaxpayerZakatSubmitMessage1 +" " +taxPayerDetails.Fbnumx+" " + AppResources.ZDearTaxpayerZakatSubmitMessage2;
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(message));


                        _navigationService.GoBack();
                        // throw new GAZTVATRegistrationInProcessException("Dear Taxpayer, your ZAKAT registration application number :"+ taxPayerDetails.Fbnumx+"is in process with GAZT");
                        //}
                    }

                    if(!string.IsNullOrEmpty(taxPayerDetails?.Atype))
                    {
                        if(taxPayerDetails?.Atype == "2")
                        {
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PleaseVisitGAZTPortalToChangeTheRegistrationType));

                            _navigationService.GoBack();
                        }
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
                    if (!string.IsNullOrEmpty(taxPayerDetails?.Tpnationality))
                    {
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
                    taxPayerDetails = await WebServiceManager.ESTTaxPayerDetailGetService("02", App.LoginDataRetrieved.TIN, App.LoginDataRetrieved.Emailid);
                    idItem = taxPayerDetails?.Nreg_IdSet.results.Where(i => EnIDType.ContainsKey(i.Type)).FirstOrDefault();
                    if (App.IsArabic)
                    {
                        GCCIDType = idItem != null ? ArIDType[idItem?.Type] : "";
                    }
                    else
                    {
                        GCCIDType = idItem != null ? EnIDType[idItem?.Type] : "";
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
                    SelectedTaxpayerPDNationality = TaxpayerFullNationlityList?.Where(i => i.Land1 == taxPayerDetails?.Natio).FirstOrDefault();
                    SelectedCitizen = TaxpayerFullNationlityList?.Where(i => i.Land1 == taxPayerDetails?.Citizen).FirstOrDefault();
                    //SelectedCitizen = SelectedCitizen == null ? "" : SelectedCitizen;
                    SelectedResidence = TaxpayerFullNationlityList?.Where(i => i.Land1 == taxPayerDetails?.Residence).FirstOrDefault();
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.PassportDetails)
                {
                    taxPayerDetails = await WebServiceManager.ESTTaxPayerDetailGetService("02", App.LoginDataRetrieved.TIN, App.LoginDataRetrieved.Emailid);
                    Nreg_IdItem passportItem = taxPayerDetails?.Nreg_IdSet.results.Where(i => i.Type == "FS0002").FirstOrDefault();
                    PassportNumber = passportItem?.Idnumber;
                    SelectedPassportIssueCountry = TaxpayerFullNationlityList?.Where(i => i.Land1 == passportItem?.Country).FirstOrDefault();
                    PassportIssueDate = passportItem?.ValidDateFrom?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                    PassportExpireDate = passportItem?.ValidDateTo?.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                    PassportAttachmentPrepopulateCheck(taxPayerDetails);
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.Outlets)
                {
                    bindingOutletList();

                    //number = await WebServiceManager.ESTOutletNumber(taxPayerDetails?.Fbnumx);
                    //taxPayerDetails = await WebServiceManager.ESTTaxPayerDetailGetService("03", App.LoginDataRetrieved.TIN, App.LoginDataRetrieved.Emailid, $"{Int16.Parse(number?.Actno):000}", taxPayerDetails?.Fbnumx);

                    taxPayerDetails = await WebServiceManager.ESTTaxPayerDetailGetService("03", App.LoginDataRetrieved.TIN, App.LoginDataRetrieved.Emailid, null, taxPayerDetails?.Fbnumx);

                    //await WebServiceManager.ESTOutletDropDowns();
                    //await WebServiceManager.ESTOutletGetActivitySetsList();
                    //ValidateCR crItem = await WebServiceManager.ESTValidateCRNum(taxPayerDetails?.Nreg_ActivitySet.results?.FirstOrDefault()?.Idnumber);
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.FinancialDetail)
                {
                    taxPayerDetails = await WebServiceManager.ESTTaxPayerDetailGetService("04", App.LoginDataRetrieved.TIN, App.LoginDataRetrieved.Emailid, null, taxPayerDetails?.Fbnumx);
                    EnMethodList = new Dictionary<string, string>()
                    {
                        {"A", AppResources.NDAccounting },
                        {"E", AppResources.NDEstimated }
                    };
                    EnCalendarTypeList = new Dictionary<string, string>()
                    {
                        {"2", AppResources.Hijri },
                        {"1", AppResources.Gregorian }
                    };
                    MethodList.Clear();
                    MethodList.AddRange(EnMethodList.Values);
                    MethodList = new List<string>(MethodList);
                    CalendarTypeList.Clear();
                    CalendarTypeList.AddRange(EnCalendarTypeList.Values);
                    CalendarTypeList = new List<string>(CalendarTypeList);
                    SelectedMethod = EnMethodList?[taxPayerDetails?.Accmethod];
                    CalendarType = EnCalendarTypeList?[taxPayerDetails?.Fdcalender];
                    udpdateDates();
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
            catch (GAZTErrorException e)
            {
                IsExceptionPopupVisible = true;
                await PopupNavigation.Instance.PushAsync(new SingleButtonPopupView(AppResources.ZZZZDone, e.Message));
                // _navigationService.GoBack();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                IsLoading = false;
                Device.BeginInvokeOnMainThread(() => updateDatePickers(_enum));
            }
        }
        private void updateDatePickers(EstablishmentRegistrationTabsEnum _enum)
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
                    if (!string.IsNullOrEmpty(SelectedDOB))
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
                    //if (PassportIssueDate != null && (new DateTime()).Date.ToString() != _issueDate.Date.ToString())
                    if (!string.IsNullOrWhiteSpace(PassportIssueDate))
                        DisplayPassportIssueDate = _issueDate.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                }
                else
                {
                    SelectedPassportIssueHijiriDate = _selectedDOBDate;
                    //if (PassportIssueDate != null && (new DateTime()).Date.ToString("yyyy/MM/dd", new CultureInfo("en-sa")) != _issueDate.Date.ToString())
                    if (!string.IsNullOrWhiteSpace(PassportIssueDate))
                        DisplayPassportIssueDate = HijriDateString(_issueDate);
                }
                if (taxPayerDetails?.Caltp == "G")
                {
                    SelectedPassportExpireDate = _selectedDOBDate;
                    // if (PassportExpireDate != null && (new DateTime()).Date.ToString() != _expiryDate.Date.ToString())
                    if (!string.IsNullOrWhiteSpace(PassportExpireDate))
                        DisplayPassportExpireDate = _expiryDate.ToString("yyyy/MM/dd", new CultureInfo("en-US"));
                }
                else
                {
                    SelectedPassportExpireHijiriDate = _selectedDOBDate;
                    // if (PassportExpireDate != null && (new DateTime()).Date.ToString("yyyy/MM/dd", new CultureInfo("en-sa")) != _expiryDate.Date.ToString())
                    if (!string.IsNullOrWhiteSpace(PassportExpireDate))
                        DisplayPassportExpireDate = HijriDateString(_expiryDate);
                }
            }
        }
        private async void udpdateDates(string selectedDate = null)
        {
            try
            {
                IsLoading = true;
                var _CalendarType = EnCalendarTypeList.FirstOrDefault(i => i.Value == CalendarType).Key == "2" ? "H" : "G";
                financialDetail = await WebServiceManager.ESTFinancialMaxDate(new FinancialDetailRequest()
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
                    //FiscalMonth = mm;
                    //FiscalDay = dd;
                }
                financialDetail = await WebServiceManager.ESTFinancialMaxDate(new FinancialDetailRequest()
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
            catch (Exception ex)
            {
                IsLoading = false;
                await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Somethingwentwrong));
            }
        }
        private async void bindingOutletList()
        {
            var _outletTempData = await WebServiceManager.ESTOutletList(taxPayerDetails?.PortalUsrx, App.LoginDataRetrieved.TIN, taxPayerDetails?.Fbnumx);
            //if (_outletTempData.Count > 0)
            //{
            OutletData.Clear();
            SearchableOutletData?.Clear();
            _outletTempData.ForEach(_out =>
            {
                OutletData.Add(_out);
                SearchableOutletData.Add(_out);
            });
            //}
        }
        private void openEditOutlet(OutletItem item)
        {
            OutletNavigationModels outletNavigationModels = new OutletNavigationModels();
            outletNavigationModels.taxPayerDetails = taxPayerDetails;
            outletNavigationModels.idItem = idItem;
            outletNavigationModels.selectedOutletItem = item;
            outletNavigationModels.IsEditingMode = true;
            outletNavigationModels.openedTab = EstablishmentRegistrationOutletTabsEnum.OutletDetail;
            _navigationService.NavigateTo(App.OutletDetailsAmendUpdatePageView, outletNavigationModels);
        }
        private void openNewOutlet()
        {
            OutletNavigationModels outletNavigationModels = new OutletNavigationModels();
            outletNavigationModels.taxPayerDetails = taxPayerDetails;
            outletNavigationModels.selectedOutletItem = null;
            outletNavigationModels.idItem = idItem;
            outletNavigationModels.IsEditingMode = false;
            _navigationService.NavigateTo(App.OutletDetailsAmendUpdatePageView, outletNavigationModels);
        }
        private void deleteOutlet(OutletItem item)
        {
            IsLoading = true;
            Console.WriteLine(item.ToString());
            var delete = WebServiceManager.ESTDeleteOutletItem(taxPayerDetails?.Fbnumx, item?.Actno, taxPayerDetails?.PortalUsrx);
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
        public async Task<bool> ValidateIDAndDOB(string IDType, string IDNumber, string DOB)
        {
            IsLoading = true;
            string Result = await WebServiceManager.GAZTVATSignUpValidateIDTypesStringResp(IDType, IDNumber, DOB);
            VATSignUp vATSignUpData = new VATSignUp();
            vATSignUpData = JsonConvert.DeserializeObject<VATSignUp>(Result);
            //   IDTypeModelRootObject SignupIsIDTypeValid = JsonConvert.DeserializeObject<IDTypeModelRootObject>(Result);
            if (vATSignUpData.d == null)
            {
                IDTypeValidateRootObject SignupIsIDTypeValidError = JsonConvert.DeserializeObject<IDTypeValidateRootObject>(Result);
                if (SignupIsIDTypeValidError.error.message.value == "An exception was raised.")
                {
                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                }
                else
                {
                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(SignupIsIDTypeValidError.error.innererror.errordetails[0].message));
                }
                Device.BeginInvokeOnMainThread(() => IsLoading = false);
                return false;
            }
            else
            {
                Device.BeginInvokeOnMainThread(() => IsLoading = false);
                return true;
            }
        }
        private async Task<bool> FormValidation(EstablishmentRegistrationTabsEnum _enum)
        {
            //return true;
            try
            {
                if (_enum == EstablishmentRegistrationTabsEnum.RegistrationType)
                {
                    if (RegTaxPayerTypeAvailability.ReportingBranch && (SelectedReportingBranch == null || string.IsNullOrWhiteSpace(SelectedReportingBranch.Bez50)))
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateBranch));
                        return false;
                    }
                    //if (string.IsNullOrWhiteSpace(SelectedTaxPayerType))
                    //{
                    //    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp("Choose TaxPayer Type"));
                    //    return false;
                    //}
                    if (!IsSaudi)
                    {
                        if (RegTaxPayerTypeAvailability.ResidencyStatus && string.IsNullOrWhiteSpace(SelectedTpresidence))
                        {
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateResidenceType));
                            return false;
                        }
                        else if (RegTaxPayerTypeAvailability.ResidencyStatus && SelectedTpresidence == "2" && (UploadedRentDocumentsList == null || UploadedRentDocumentsList.Count <= 0))
                        {
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateAttachRent));
                            return false;
                        }
                        else if (RegTaxPayerTypeAvailability.ResidencyStatus && SelectedTpresidence == "3" && string.IsNullOrWhiteSpace(SelectedOrgNonResident))
                        {
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateLegalEntity));
                            return false;
                        }
                        else if (RegTaxPayerTypeAvailability.ResidencyStatus && SelectedTpresidence == "3" && SelectedOrgNonResident == "1" && string.IsNullOrWhiteSpace(SelectedOrgNonResidentOptions))
                        {
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateParmanentEst));
                            return false;
                        }
                        else if (RegTaxPayerTypeAvailability.ResidencyStatus && SelectedTpresidence == "3" && SelectedOrgNonResident == "2" && string.IsNullOrWhiteSpace(SelectedOrgNonResidentActivity))
                        {
                            await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateOtherTax));
                            return false;
                        }
                    }
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.TaxpayerDetail)
                {

                    if (TaxPayerDetailsAvailability.DOB && string.IsNullOrWhiteSpace(SelectedDOB))
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateDOB));
                        return false;
                    }
                    else if (TaxPayerDetailsAvailability.FirstName && string.IsNullOrWhiteSpace(FirstName))
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateFirstName));
                        return false;
                    }
                    else if ((TaxPayerDetailsAvailability.IsGenderVisible || TaxPayerDetailsAvailability.Gender) && string.IsNullOrWhiteSpace(SelectedGender))
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateGender));
                        return false;
                    }
                    else if ((TaxPayerDetailsAvailability.IsNationalityVisible || TaxPayerDetailsAvailability.Nationality) && null == SelectedTaxpayerPDNationality)
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateNationality));
                        return false;
                    }
                    else if ((TaxPayerDetailsAvailability.IsCitizenVisible || TaxPayerDetailsAvailability.Citizen) && null == SelectedCitizen)
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateCitizen));
                        return false;
                    }
                    else if ((TaxPayerDetailsAvailability.IsResidenceVisible || TaxPayerDetailsAvailability.Residence) && null == SelectedResidence)
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidateResidence));
                        return false;
                    }
                    if (TaxPayerDetailsAvailability.DOB && !string.IsNullOrWhiteSpace(SelectedDOB))
                    {
                        var dob = SelectedDOB.Replace("/", "");
                        var result = await ValidateIDAndDOB(idItem?.Type, GCCIDTypeIdNumberValue, dob);
                        if (!result)
                        {
                            return false;
                        }
                    }
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.PassportDetails)
                {
                    if (PassportDetails.PassportNo && string.IsNullOrWhiteSpace(PassportNumber))
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePassportNumber));
                        return false;
                    }
                    else if (PassportDetails.IssueCountry && null == SelectedPassportIssueCountry)
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePIssueCountry));
                        return false;
                    }
                    else if (PassportDetails.IssueDate && string.IsNullOrWhiteSpace(PassportIssueDate))
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePIssueDate));
                        return false;
                    }
                    else if (PassportDetails.ExpiryDate && string.IsNullOrWhiteSpace(PassportExpireDate))
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePExpiryDate));
                        return false;
                    }
                    else if (PassportDetails.Attachment && (UploadedPassportDocumentsList == null || UploadedPassportDocumentsList.Count <= 0))
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ESTValidatePAttachCopy));
                        return false;
                    }
                    else if (PassportDetails.PassportNo)
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
            //return true;
            try
            {
                IsLoading = true;
                if (_enum == EstablishmentRegistrationTabsEnum.RegistrationType)
                {
                    //return true;
                    taxPayerDetails.Augrp = SelectedReportingBranch?.Augrp;
                    taxPayerDetails.Atype = "1";// SelectedEntityType.Equals("Individual") ? "1" : "2";
                    taxPayerDetails.Tpnationality = NationalityMapping.Where(i => i.Value == SelectedRegNationalityType).FirstOrDefault().Key;
                    taxPayerDetails.Tpnationality = taxPayerDetails.Tpnationality == null ? "" : taxPayerDetails.Tpnationality;
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
                    var taxPayerDetailsResult = await WebServiceManager.ESTTaxPayerDetailPostService(taxPayerDetails);

                    IsLoading = false;
                    return true;
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.TaxpayerDetail)
                {
                    //return true;
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
                    taxPayerDetails.Natio = SelectedTaxpayerPDNationality?.Land1 == null ? "" : SelectedTaxpayerPDNationality?.Land1;
                    taxPayerDetails.Citizen = SelectedCitizen?.Land1 == null ? "" : SelectedCitizen.Land1;
                    taxPayerDetails.Residence = SelectedResidence?.Land1 == null ? "" : SelectedResidence?.Land1;

                    taxPayerDetails.StepNumberx = "02";
                    taxPayerDetails.Gpartx = App.LoginDataRetrieved.TIN;
                    taxPayerDetails.UserTypx = "TP";
                    var taxPayerDetailsResult = await WebServiceManager.ESTTaxPayerDetailPostService(taxPayerDetails);


                    IsLoading = false;
                    return true;
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.PassportDetails)
                {
                    //return true;
                    Nreg_IdItem passportObj = new Nreg_IdItem();
                    passportObj.Idnumber = PassportNumber == null ? "" : PassportNumber;
                    passportObj.Country = SelectedPassportIssueCountry?.Land1 == null ? "" : SelectedPassportIssueCountry?.Land1;

                    DateTime.TryParseExact(PassportIssueDate, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime issueDate);
                    DateTime.TryParseExact(PassportExpireDate, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime expireDate);
                    passportObj.ValidDateFrom = issueDate;
                    passportObj.ValidDateTo = expireDate;
                    passportObj.Type = "FS0002";
                    passportObj.Srcidentify = "000";

                    taxPayerDetails.Nreg_IdSet.results.Clear();
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
                    var taxPayerDetailsResult = await WebServiceManager.ESTTaxPayerDetailPostService(taxPayerDetails);

                    IsLoading = false;
                    return true;
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.FinancialDetail)
                {
                    //return true;
                    //DateTime.TryParseExact(CommDate, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime Commdt);
                    DateTime.TryParseExact(TaxDate, string.Format("{0:0000/00/00}", Int64.Parse(financialDetail?.EIsldate)), new CultureInfo("en-US"), DateTimeStyles.None, out DateTime Fdenddt);
                    taxPayerDetails.Accmethod = EnMethodList.FirstOrDefault(i => i.Value == SelectedMethod).Key;
                    taxPayerDetails.Accmethod = taxPayerDetails.Accmethod == null ? "" : taxPayerDetails.Accmethod;
                    taxPayerDetails.Fdcalender = EnCalendarTypeList.FirstOrDefault(i => i.Value == CalendarType).Key;
                    taxPayerDetails.Fdcalender = taxPayerDetails.Fdcalender == null ? "" : taxPayerDetails.Fdcalender;
                    taxPayerDetails.Fdmonth = FiscalMonth == null ? "" : FiscalMonth;
                    taxPayerDetails.Fdday = FiscalDay == AppResources.ESTFinLastDay ? "LD" : FiscalDay;
                    taxPayerDetails.Commdt = financialDetail?.ADateComm;
                    taxPayerDetails.Fdenddt = Fdenddt;
                    taxPayerDetails.Chkfg = "X";

                    taxPayerDetails.Gpartx = App.LoginDataRetrieved.TIN;
                    taxPayerDetails.StepNumberx = "04";
                    taxPayerDetails.UserTypx = "TP";
                    var taxPayerDetailsResult = await WebServiceManager.ESTTaxPayerDetailPostService(taxPayerDetails);

                    IsLoading = false;
                    return true;
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.Declaration)
                {
                    //return true;
                    taxPayerDetails?.off_notesSet?.results?.Clear();
                    taxPayerDetails.Decfg = "X";
                    taxPayerDetails.Operationx = "01";
                    taxPayerDetails.Gpartx = App.LoginDataRetrieved.TIN;
                    taxPayerDetails.UserTypx = "TP";
                    taxPayerDetails.Acsactivitydet = "X";
                    taxPayerDetails.Acscontactper = "X";
                    taxPayerDetails.Fbsta = "IP021";
                    taxPayerDetails.Fbstax = "IP021";
                    taxPayerDetails.Fbust = "E0015";
                    taxPayerDetails.Fbustx = "E0015";
                    taxPayerDetails.Mandt = "330";

                    /*
                     "Fbsta": "IP011",
                        "Fbstax": "IP011",
                        "Fbust": "E0001",
                        "Fbustx": "E0001",*/

                    taxPayerDetails = await WebServiceManager.ESTTaxPayerDetailPostService(taxPayerDetails);

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
                    //   await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                }
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
                // EstablishmentRegistrationTabsEnum.RegistrationType

                if (_enum == EstablishmentRegistrationTabsEnum.RegistrationType)
                {
                    taxPayerDetails.Augrp = SelectedReportingBranch?.Augrp;
                    taxPayerDetails.Atype = "1";// SelectedEntityType.Equals("Individual") ? "1" : "2";
                    taxPayerDetails.Tpnationality = NationalityMapping.Where(i => i.Value == SelectedRegNationalityType).FirstOrDefault().Key;
                    taxPayerDetails.Tpnationality = taxPayerDetails.Tpnationality == null ? "" : taxPayerDetails.Tpnationality;
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
                    taxPayerDetails.Gpartx = App.LoginDataRetrieved.TIN;
                    taxPayerDetails.UserTypx = "TP";
                    flag = true;
                    return flag;
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.TaxpayerDetail)
                {
                    if (!string.IsNullOrEmpty(DisplaySelectedDOB))
                    {
                        DateTime.TryParseExact(DisplaySelectedDOB, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime dob);
                        taxPayerDetails.Birthdt = dob;
                    }
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
                    taxPayerDetails.Gpartx = App.LoginDataRetrieved.TIN;
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
                        passportObj.ValidDateFrom = issueDate;
                    }
                    if (!string.IsNullOrEmpty(PassportExpireDate))
                    {
                        DateTime.TryParseExact(PassportExpireDate, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime expireDate);
                        passportObj.ValidDateTo = expireDate;
                    }

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
                    taxPayerDetails.Gpartx = App.LoginDataRetrieved.TIN;
                    taxPayerDetails.UserTypx = "TP";
                    flag = true;
                    return flag;
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.FinancialDetail)
                {
                    // EstablishmentRegistrationTabsEnum.FinancialDetail
                    //DateTime.TryParseExact(CommDate, "yyyy/MM/dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime Commdt);  
                    if (!string.IsNullOrEmpty(TaxDate))
                    {
                        DateTime.TryParseExact(TaxDate, string.Format("{0:0000/00/00}", Int64.Parse(financialDetail?.EIsldate)), new CultureInfo("en-US"), DateTimeStyles.None, out DateTime Fdenddt);
                        taxPayerDetails.Fdenddt = Fdenddt;
                    }
                    taxPayerDetails.Accmethod = EnMethodList.FirstOrDefault(i => i.Value == SelectedMethod).Key;
                    taxPayerDetails.Accmethod = taxPayerDetails.Accmethod == null ? "" : taxPayerDetails.Accmethod;
                    taxPayerDetails.Fdcalender = EnCalendarTypeList.FirstOrDefault(i => i.Value == CalendarType).Key;
                    taxPayerDetails.Fdcalender = taxPayerDetails.Fdcalender == null ? "" : taxPayerDetails.Fdcalender;
                    taxPayerDetails.Fdmonth = FiscalMonth == null ? "" : FiscalMonth;
                    taxPayerDetails.Fdday = FiscalDay == AppResources.ESTFinLastDay ? "LD" : FiscalDay;
                    taxPayerDetails.Commdt = financialDetail?.ADateComm;


                    taxPayerDetails.Gpartx = App.LoginDataRetrieved.TIN;
                    taxPayerDetails.StepNumberx = "04";
                    taxPayerDetails.UserTypx = "TP";
                    flag = true;
                    return flag;
                }

                // EstablishmentRegistrationTabsEnum.Declaration

                taxPayerDetails?.off_notesSet?.results?.Clear();
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