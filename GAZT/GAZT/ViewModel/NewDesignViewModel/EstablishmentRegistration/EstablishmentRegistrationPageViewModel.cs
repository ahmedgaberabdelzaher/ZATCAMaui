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
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using Plugin.FilePicker;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.EstablishmentRegistration
{
    public class EstablishmentRegistrationPageViewModel : BaseViewModel
    {
        #region Variable
        private TaxPayerDetails taxPayerDetails = null;
        private OutletNumber number;
        private EstablishmentRegistrationTabsEnum _currentTab = EstablishmentRegistrationTabsEnum.PassportDetails;
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
                        SelectedTabText = "Taxpayer Personal Details";
                        break;
                    case EstablishmentRegistrationTabsEnum.PassportDetails:
                        SelectedTabText = "Passport Details";
                        break;
                    case EstablishmentRegistrationTabsEnum.Outlets:
                        SelectedTabText = "Outlets";
                        break;
                    case EstablishmentRegistrationTabsEnum.FinancialDetail:
                        SelectedTabText = "Financial Details";
                        break;
                    case EstablishmentRegistrationTabsEnum.Declaration:
                        SelectedTabText = "Summary";
                        break;
                    case EstablishmentRegistrationTabsEnum.RegistrationType:
                    default:
                        SelectedTabText = "Registration/Taxpayer type";
                        break;
                }
                fetchTabDataAndBind(_currentTab);
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



        private int _currenrIndex = (int)EstablishmentRegistrationTabsEnum.RegistrationType;
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

        private string _selectedTabText = "Registration/Taxpayer type";
        public string SelectedTabText
        {
            get => _selectedTabText;
            set
            {
                _selectedTabText = value;
                RaisePropertyChanged(nameof(SelectedTabText));
            }
        }

        private List<string> _draftMenuList = new List<string>();
        public List<string> DraftMenuList
        {
            get
            {
                return _draftMenuList;
            }
            set
            {
                if (value != null)
                {
                    _draftMenuList = value;
                    RaisePropertyChanged(nameof(DraftMenuList));
                }
            }
        }

        private string _selectedMenuList = null;
        public string SelectedMenuList
        {
            get => _selectedMenuList;
            set
            {
                _selectedMenuList = value;
                RaisePropertyChanged(nameof(SelectedMenuList));
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




        private ObservableCollection<string> _orgNonResidentActivityList = new ObservableCollection<string>();
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

        private string _selectedReportingBranch = string.Empty;
        public string SelectedReportingBranch
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

        private string _selectedNationalityStatus;
        public string SelectedNationalityStatus
        {
            get => _selectedNationalityStatus;
            set
            {
                _selectedNationalityStatus = value;
                RaisePropertyChanged(nameof(SelectedNationalityStatus));
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
                RaisePropertyChanged(nameof(TaxpayerPDNationlityList));
            }
        }

        private List<string> _taxpayerPDNationlityList;
        public List<string> TaxpayerPDNationlityList
        {
            get => _taxpayerPDNationlityList;
            set
            {
                _taxpayerPDNationlityList = value;
                RaisePropertyChanged(nameof(TaxpayerPDNationlityList));
            }
        }

        private string _selectedTaxpayerPDNationality;
        public string SelectedTaxpayerPDNationality
        {
            get => _selectedTaxpayerPDNationality;
            set
            {
                _selectedTaxpayerPDNationality = value;
                RaisePropertyChanged(nameof(SelectedTaxpayerPDNationality));
            }
        }

        private List<string> _citizenList;
        public List<string> CitizenList
        {
            get => _citizenList;
            set
            {
                _citizenList = value;
                RaisePropertyChanged(nameof(CitizenList));
            }
        }

        private string _selectedCitizen;
        public string SelectedCitizen
        {
            get => _selectedCitizen;
            set
            {
                _selectedCitizen = value;
                RaisePropertyChanged(nameof(SelectedCitizen));
            }
        }

        private List<string> _residenceList;
        public List<string> ResidenceList
        {
            get => _residenceList;
            set
            {
                _residenceList = value;
                RaisePropertyChanged(nameof(ResidenceList));
            }
        }

        private string _selectedResidence;
        public string SelectedResidence
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

        private List<string> _passportIssueCountryList;
        public List<string> PassportIssueCountryList
        {
            get => _passportIssueCountryList;
            set
            {
                _passportIssueCountryList = value;
                RaisePropertyChanged(nameof(PassportIssueCountryList));
            }
        }

        private string _selectedPassportIssueCountry;
        public string SelectedPassportIssueCountry
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

        #endregion

        #region Outlet variables
        private ObservableCollection<Nreg_OutletItem> _outletData = new ObservableCollection<Nreg_OutletItem>();
        public ObservableCollection<Nreg_OutletItem> OutletData
        {
            get => _outletData;
            set
            {
                if(value != null)
                {
                    _outletData = value;
                    RaisePropertyChanged(nameof(OutletData));
                }
            }
        }
        #endregion

        #region Financial Details Tabs variables
        private ObservableCollection<string> _methodList = new ObservableCollection<string>();
        public ObservableCollection<string> MethodList
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
        private ObservableCollection<string> _calendarTypeList = new ObservableCollection<string>();
        public ObservableCollection<string> CalendarTypeList
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
                _calendarType = value;
                RaisePropertyChanged(nameof(CalendarType));
            }
        }
        private string _fiscalMonth = "09";
        public string FiscalMonth
        {
            get => _fiscalMonth;
            set
            {
                _fiscalMonth = value;
                RaisePropertyChanged(nameof(FiscalMonth));
            }
        }
        private string _fiscalDay = "28";
        public string FiscalDay
        {
            get => _fiscalDay;
            set
            {
                _fiscalDay = value;
                RaisePropertyChanged(nameof(FiscalDay));
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

        public ICommand OnERAttachmentCloseTapped { get; set; }
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

        public byte[] PassportAttachmentByte;
        int attachmentCount = 0;

        private ObservableCollection<ZakatAttachment> _passportAttachmentsList;
        public ObservableCollection<ZakatAttachment> PassportAttachmentsList
        {
            get
            {
                return _passportAttachmentsList;
            }
            set
            {
                _passportAttachmentsList = value;
                RaisePropertyChanged("PassportAttachmentsList");
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

            GetMenuListFromServer();
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
            OnERAttachmentCloseTapped = new Command(() => onERAttachmentCloseTapped());
            OnEstablishmentRegistrationAttachmentTapped = new Command(() => onEstablishmentRegistrationAttachmentTapped());
            #endregion

            GetOrgNonResidentActivityList();

            OnReportingBranchSelectButtonClick = new Command(() =>
           {
               ListPopUpViewPage poupWindow = new ListPopUpViewPage(ReportingBranchList);
               poupWindow.OnItemSelect = (item) => SelectedReportingBranch = (item as BranchesDropDownModel)?.Augrp;
               PopupNavigation.Instance.PushAsync(poupWindow);
           });

            #endregion

            #region TaxPayer Variable initialization

            getGenderList();

            OnPDNatinalitySelectButtonClick = new Command(() =>
            {
                TaxpayerPDNationlityList = TaxpayerFullNationlityList.Select(x => x.Natio50).ToList();
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(TaxpayerPDNationlityList);
                poupWindow.OnItemSelect = (item) => SelectedTaxpayerPDNationality = item as string;
                PopupNavigation.Instance.PushAsync(poupWindow);
            });

            OnPDCitizenSelectButtonClick = new Command(() =>
            {
                CitizenList = TaxpayerFullNationlityList.Select(x => x.Landx50).ToList();
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(CitizenList);
                poupWindow.OnItemSelect = (item) => SelectedCitizen = item as string;
                PopupNavigation.Instance.PushAsync(poupWindow);
            });

            OnPDResidenceSelectButtonClick = new Command(() =>
            {
                ResidenceList = TaxpayerFullNationlityList.Select(x => x.Landx50).ToList();
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(ResidenceList);
                poupWindow.OnItemSelect = (item) => SelectedResidence = item as string;
                PopupNavigation.Instance.PushAsync(poupWindow);
            });
            #endregion

            #region Passport Variable Initialization

            OnPassportIssueCountryButtonClick = new Command(() =>
            {
                PassportIssueCountryList = TaxpayerFullNationlityList.Select(x => x.Landx50).ToList();
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(PassportIssueCountryList);
                poupWindow.OnItemSelect = (item) => SelectedPassportIssueCountry = item as string;
                PopupNavigation.Instance.PushAsync(poupWindow);
            });

            OnPassportAttachmentTapped = new Command(() => onPassportAttachmentTapped());
            OnPassportCloseTapped = new Command(() => onPassportCloseTapped());
            #endregion

            #region Outlet Tabs variable initialization
            OnNewOutletButtonClick = new Command(() =>
            {
                OutletNavigationModels outletNavigationModels = new OutletNavigationModels();
                //if (OutletData?.Count > 0)
                //{
                //    outletNavigationModels.openedTab = EstablishmentRegistrationOutletTabsEnum.ActivityDetails;
                //}
                outletNavigationModels.taxPayerDetails = taxPayerDetails;
                outletNavigationModels.nextNumber = number;
                _navigationService.NavigateTo(App.OutletDetailsPageView, outletNavigationModels);
            });
            #endregion

            #region Financial Details Tabs variable initialization
            MethodList.Clear();
            MethodList.Add("Accounts");
            MethodList.Add("Estimate");

            CalendarTypeList.Clear();
            CalendarTypeList.Add("Hijri");
            CalendarTypeList.Add("Gregorian");

            SelectedMethod = MethodList.FirstOrDefault();
            CalendarType = CalendarTypeList.LastOrDefault();

            OnMonthSelectButtonClick = new Command(() =>
            {
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(new List<string> { "11", "10", "09", "08", "07", "06", "05", "04", "03", "02", "01" });
                poupWindow.OnItemSelect = (item) => FiscalMonth = item as string;
                PopupNavigation.Instance.PushAsync(poupWindow);
            });
            OnDaySelectButtonClick = new Command(() =>
            {
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(new List<string> { "28", "27", "26", "25", "24", "23", "22", "21", "20", "19", "18", "17", "16", "15", "14", "13", "12", "11", "10", "09", "08", "07", "06", "05", "04", "03", "02", "01" });
                poupWindow.OnItemSelect = (item) => FiscalDay = item as string;
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
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(DraftMenuList);
                poupWindow.OnItemSelect = (item) => SelectedMenuList = item as string;
                PopupNavigation.Instance.PushAsync(poupWindow);
            });
            #endregion
        }

        #endregion

        #region Method
        public async void OnAppearing()
        {
            GetMenuListFromServer();
            var branchTask = GetReportingBranchListFromServer();
            var nationalityTask = GetPdNationalityListFromServer(null);
            await Task.WhenAll(branchTask, nationalityTask);
            if (taxPayerDetails == null)
            {
                fetchTabDataAndBind(EstablishmentRegistrationTabsEnum.RegistrationType);
            }
        }

        private void navigateToNext()
        {
            if (currentTab == EstablishmentRegistrationTabsEnum.TaxpayerDetail)
            {
                currentTab = EstablishmentRegistrationTabsEnum.PassportDetails;
            }
            else if (currentTab == EstablishmentRegistrationTabsEnum.PassportDetails)
            {
                currentTab = EstablishmentRegistrationTabsEnum.Outlets;
            }
            else if (currentTab == EstablishmentRegistrationTabsEnum.Outlets)
            {
                currentTab = EstablishmentRegistrationTabsEnum.FinancialDetail;
            }
            else if (currentTab == EstablishmentRegistrationTabsEnum.FinancialDetail)
            {
                currentTab = EstablishmentRegistrationTabsEnum.Declaration;
            }
            else if (currentTab == EstablishmentRegistrationTabsEnum.RegistrationType)
            {
                currentTab = EstablishmentRegistrationTabsEnum.TaxpayerDetail;
            }
            else if (currentTab == EstablishmentRegistrationTabsEnum.Declaration)
            {
                _navigationService.NavigateTo(App.RegistrationSuccessfulPage);
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

        bool ValidateRegistrationForm()
        {
            if (SelectedReportingBranch == null)
            {
                return false;
            }
            else if (SelectedNationalityStatus == null)
            {
                return false;
            }
           

            return true;
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
                        SelectedNationalityStatus = "Stay More than or equal to 183 days in KSA";

                        //Options clear or done false
                        IsClickedPermanentLegalEntity = false;
                        IsClickedOtherTaxableIncomeLegalEntity = false;

                        //Sub - Options clear or done false
                        IsClickedABranchOfNonResidentCompanyPE = false;
                        IsClickedConstructionSitePE = false;
                        IsClickedInstallationPE = false;
                        IsClickedAFixedBasePE = false;
                        IsClickedNonResidentPartnerPE = false;
                    }
                    break;
                case OrgResidenceNationalityEstablishmentRegistrationEnum.RentOwnhouseMoreThanThirtyDays:

                    IsClickedStayMoreThanKSAOption = false;
                    IsClickedOwnRentOption = true;
                    IsClickedNoneOfTheAboveOption = false;
                    SelectedNationalityStatus = "Rent/Own a house more than 30 days";


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
                    SelectedNationalityStatus = "None of the Above";


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
                default:
                    IsClickedStayMoreThanKSAOption = false;
                    IsClickedOwnRentOption = false;
                    IsClickedNoneOfTheAboveOption = false;
                    SelectedNationalityStatus = "";
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
            }
        }

        private void OrgNonResidentSelection(OrgNonResidentEstablishmentRegistrationEnum selectedOption)
        {
            switch (selectedOption)
            {
                case OrgNonResidentEstablishmentRegistrationEnum.PermanentEstablishment:

                    IsClickedPermanentLegalEntity = true;
                    IsClickedOtherTaxableIncomeLegalEntity = false;
                    SelectedLegalEntity = "Permanent Establishment";
                    //Options clear or done false
                    IsClickedABranchOfNonResidentCompanyPE = false;
                    IsClickedConstructionSitePE = false;
                    IsClickedInstallationPE = false;
                    IsClickedAFixedBasePE = false;
                    IsClickedNonResidentPartnerPE = false;
                    break;
                case OrgNonResidentEstablishmentRegistrationEnum.OtherTaxIncomeFromSourceWithInTheSKA:

                    IsClickedPermanentLegalEntity = false;
                    IsClickedOtherTaxableIncomeLegalEntity = true;
                    SelectedLegalEntity = "Other Taxable Income from source with in the KSA";


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
                    SelectedLegalEntity = "";

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
                    break;
                case OrgNonResidentOptionsEstablishmentEnum.ConstructionSitePE:
                    IsClickedABranchOfNonResidentCompanyPE = false;
                    IsClickedConstructionSitePE = true;
                    IsClickedInstallationPE = false;
                    IsClickedAFixedBasePE = false;
                    IsClickedNonResidentPartnerPE = false;
                    break;
                case OrgNonResidentOptionsEstablishmentEnum.InstallationPE:
                    IsClickedABranchOfNonResidentCompanyPE = false;
                    IsClickedConstructionSitePE = false;
                    IsClickedInstallationPE = true;
                    IsClickedAFixedBasePE = false;
                    IsClickedNonResidentPartnerPE = false;
                    break;
                case OrgNonResidentOptionsEstablishmentEnum.AFixedBasePE:
                    IsClickedABranchOfNonResidentCompanyPE = false;
                    IsClickedConstructionSitePE = false;
                    IsClickedInstallationPE = false;
                    IsClickedAFixedBasePE = true;
                    IsClickedNonResidentPartnerPE = false;
                    break;
                case OrgNonResidentOptionsEstablishmentEnum.NonResidentPartnerPE:
                    IsClickedABranchOfNonResidentCompanyPE = false;
                    IsClickedConstructionSitePE = false;
                    IsClickedInstallationPE = false;
                    IsClickedAFixedBasePE = false;
                    IsClickedNonResidentPartnerPE = true;
                    break;
                default:
                    //Options clear or done false
                    IsClickedABranchOfNonResidentCompanyPE = false;
                    IsClickedConstructionSitePE = false;
                    IsClickedInstallationPE = false;
                    IsClickedAFixedBasePE = false;
                    IsClickedNonResidentPartnerPE = false;
                    break;

            }
        }


        private void GetOrgNonResidentActivityList()
        {
            OrgNonResidentActivityList.Clear();
            OrgNonResidentActivityList.Add("Derived from an activity which occurs in KSA");
            OrgNonResidentActivityList.Add("Derived from immoviable property located in the Kingdome");
            OrgNonResidentActivityList.Add("Derived from the disposal of shares or a partnership in resident company");
            OrgNonResidentActivityList.Add("Derived from lease of moveable properties used in Kingdome");
            OrgNonResidentActivityList.Add("Derived from Sales or license for use of industrial or intellectual Properties used in Kingdome");
            OrgNonResidentActivityList.Add("Dividends, Managment or directors fees paid by resident company");
            OrgNonResidentActivityList.Add("Amounts paid against services rendered to the company's head office or to an affiliated company");
            OrgNonResidentActivityList.Add("Amounts paid by a resident against serivces performed in whole or in part in the Kingdome");
            OrgNonResidentActivityList.Add("Amounts for exploitation of a natural resource in the kingdome");



        }


        private void getGenderList()
        {
            GenderList.Clear();
            GenderList.Add("Male");
            GenderList.Add("Female");


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
        }

        private void GetMenuListFromServer()
        {
            if (DraftMenuList == null && DraftMenuList.Count() == 0)
            {
                DraftMenuList = new List<string> { "Save", "Void", "CalenderType" };

                //await WebServiceManager.();
            }
        }



        private void onPassportCloseTapped()
        {
            IsVisbleAttachmentPassportList = false;
        }

        private void onPassportAttachmentTapped()
        {
            IsVisbleAttachmentPassportList = true;
             new Xamarin.Forms.Command(async () =>
            {
                try
                {
                    string[] filetypes;

                    filetypes = DependencyService.Get<IDeviceInfo>().GetAttachmentTypeStringForZakat();
                    var fileData = await CrossFilePicker.Current.PickFile(filetypes);
                    PassportAttachmentByte = fileData.DataArray;
                    await Task.Run(() =>
                    {
                        IsLoading = true;
                    });
                    await Task.Run(async () =>
                    {
                        try
                        {
                            //AttachmentName = fileData.FileName;
                            //if (fileData.FileName.Contains("."))
                            //{
                            //    string Extention = AttachmentName.Split('.')[1];
                            //    string ContentType = UtilityManager.GetContentType(Extention);
                            //    bool isFileAlreayUploaded = IsFileAlreadyAttached(AttachmentName);
                            //    decimal AttachmentSizeTillFourDecimal = Math.Round(Convert.ToDecimal((Convert.ToDouble(attachment.Length) / 1048576.0)), 4);

                            //    if (Convert.ToDecimal(AttachmentSizeTillFourDecimal) > 0)
                            //    {
                            //        if (!isFileAlreayUploaded)
                            //        {
                            //            if (Extention.ToLower() == "doc" || Extention.ToLower() == "docx" || Extention.ToLower() == "jpg" || Extention.ToLower() == "jpeg" || Extention.ToLower() == "pdf" || Extention.ToLower() == "xlsx" || Extention.ToLower() == "xls")
                            //            {
                            //                if (attachment.Length < 5242880)
                            //                {
                            //                    if (ZakatReturnAttachmentsList.Count < 5)
                            //                    {
                            //                        AttachmentRootOject _attachment = await WebServiceManager.GAZTSaveEstimatedZAKATAttachment(attachment, AttachmentName, SalesDetailsPageViewModel.RetGuid, "Z12L", ContentType);
                            //                        PopToRootPage();
                            //                        if (_attachment != null && _attachment.d != null)
                            //                        {
                            //                            AttachmentName = string.Empty;
                            //                            EstimateZakatAttachment _estimateZakatAttachment = new EstimateZakatAttachment();
                            //                            _estimateZakatAttachment.Doguid = _attachment.d.Doguid;
                            //                            _estimateZakatAttachment.Seqno = string.Empty;
                            //                            _estimateZakatAttachment.SchGuid = string.Empty;
                            //                            _estimateZakatAttachment.AttBy = string.Empty;// DateTime.Now.ToString("ddd, dd MMM yyy HH’:’mm’:’ss ‘GMT’");// string.Empty;
                            //                            _estimateZakatAttachment.FileExtn = string.Empty;
                            //                            _estimateZakatAttachment.ByPusr = string.Empty;
                            //                            _estimateZakatAttachment.OutletRef = string.Empty;
                            //                            _estimateZakatAttachment.Filename = _attachment.d.Filename;
                            //                            _estimateZakatAttachment.RetGuid = _attachment.d.RetGuid;
                            //                            _estimateZakatAttachment.Dotyp = "FZ01";
                            //                            _estimateZakatAttachment.Mimetype = string.Empty;
                            //                            _estimateZakatAttachment.DocUrl = _attachment.d.DocUrl;
                            //                            _estimateZakatAttachment.DataVersion = string.Empty;
                            //                            DateTime currentDate = DateTime.Now.ToLocalTime();
                            //                            long ticks = currentDate.Ticks;
                            //                            //_estimateZakatAttachment.UploadedDate = currentDate.ToString();
                            //                            TimeSpan span = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
                            //                            string unixTime = span.TotalSeconds.ToString("N0");
                            //                            unixTime = unixTime.Replace(",", "");
                            //                            _estimateZakatAttachment.Erfdt = "" + "/Date(" + unixTime + ")/";// need to
                            //                                                                                             //_estimateZakatAttachment.Erfdt = "/Date(" + unixTime + ")/";// need to
                            //                            SelectedSalesDetails.estimateZakatAttachment.Add(_estimateZakatAttachment);
                            //                            ZakatReturnAttachmentsList = CloneAttachmmentListInLocalList(SelectedSalesDetails.estimateZakatAttachment);
                            //                            IsValueChanged();// 1584987294.32348//1584987210.06955
                            //                                             // ZakatReturnAttachmentsList.Add(_estimateZakatAttachment);
                            //                        }
                            //                        else
                            //                        {
                            //                            Device.BeginInvokeOnMainThread(async () =>
                            //                            {
                            //                                AttachmentName = string.Empty;
                            //                                await _dialogService.ShowMessage(AppResources.Somethingwentwrong, AppResources.Information);
                            //                                IsLoading = false;
                            //                            });
                            //                        }
                            //                    }
                            //                    else
                            //                    {
                            //                        Device.BeginInvokeOnMainThread(async () =>
                            //                        {
                            //                            AttachmentName = string.Empty;
                            //                            await _dialogService.ShowMessage(AppResources.ZZYoucannotuploadmorethan5attachment, AppResources.Alerts);
                            //                            IsLoading = false;
                            //                        });
                            //                    }
                            //                }
                            //                else
                            //                {
                            //                    Device.BeginInvokeOnMainThread(async () =>
                            //                    {
                            //                        AttachmentName = string.Empty;
                            //                        await _dialogService.ShowMessage(AppResources.ZZFilesizemustbelessthan5MB, AppResources.Alerts);
                            //                        IsLoading = false;
                            //                    });
                            //                }
                            //            }
                            //            else
                            //            {
                            //                Device.BeginInvokeOnMainThread(async () =>
                            //                {
                            //                    AttachmentName = string.Empty;
                            //                    await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly, AppResources.Information);
                            //                    IsLoading = false;
                            //                });
                            //            }
                            //        }
                            //        else
                            //        {
                            //            Device.BeginInvokeOnMainThread(async () =>
                            //            {
                            //                await _dialogService.ShowMessage(AppResources.ZZFileWithTheSameNameAlreadyExists, AppResources.Alerts);
                            //                IsLoading = false;
                            //                AttachmentName = string.Empty;
                            //            });
                            //        }
                            //    }
                            //    else
                            //    {
                            //        AttachmentName = string.Empty;
                            //        IsLoading = false;
                            //        Device.BeginInvokeOnMainThread(async () =>
                            //        {
                            //            await _dialogService.ShowMessage(AppResources.Somethingwentwrong, AppResources.Information);
                            //        });


                            //    }

                            //}
                            //else
                            //{
                            //    Device.BeginInvokeOnMainThread(async () =>
                            //    {
                            //        AttachmentName = string.Empty;
                            //        await _dialogService.ShowMessage(AppResources.ZZGeneralMessage_UploadFilesWithAllowedExtensionsOnly, AppResources.Information);
                            //        IsLoading = false;
                            //    });
                            //}
                        }
                        catch (InternetException ex)
                        {
                            IsLoading = false;
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                _dialogService.ShowMessage(ex.Message, AppResources.Information);
                            });
                        }
                    });
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });
                }
                catch (Exception ex)
                {
                }
            });
        }

        private void onERAttachmentCloseTapped()
        {
        }

        private void onEstablishmentRegistrationAttachmentTapped()
        {
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
                    SelectedReportingBranch = ReportingBranchList.Where(i => i.Augrp == taxPayerDetails?.Augrp).FirstOrDefault().ToString();
                    SelectedEntityType = Int16.Parse(taxPayerDetails?.Atype) == 1 ? "Individual" : "Company";
                    SelectedRegNationalityType = taxPayerDetails?.Tpnationality;

                    ResidenceTypePrePopulateData(taxPayerDetails);
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.TaxpayerDetail)
                {

                    await GetPdNationalityListFromServer(taxPayerDetails?.Tpnationality);
                    taxPayerDetails = await WebServiceManager.ESTTaxPayerDetailGetService("02", "3102448184", "DKOTHI-C@GAZT.GOV.SA");
                    Nreg_IdItem idItem = taxPayerDetails?.Nreg_IdSet.results.Where(i => EnIDType.ContainsKey(i.Type)).FirstOrDefault();
                    GCCIDType = EnIDType[idItem?.Type];
                    GCCIDTypeIdNumberValue = idItem.Idnumber;
                    SelectedDOB = taxPayerDetails?.Birthdt?.ToString("dd/MM/yyyy", new CultureInfo("en-US"));
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

                    SelectedTaxpayerPDNationality = TaxpayerFullNationlityList.Where(i => i.Land1 == taxPayerDetails?.Natio).FirstOrDefault().Natio50;
                    SelectedCitizen = TaxpayerFullNationlityList.Where(i => i.Land1 == taxPayerDetails?.Citizen).FirstOrDefault()?.Landx50;
                    SelectedResidence = TaxpayerFullNationlityList.Where(i => i.Land1 == taxPayerDetails?.Residence).FirstOrDefault()?.Landx50;
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.PassportDetails)
                {
                    taxPayerDetails = await WebServiceManager.ESTTaxPayerDetailGetService("02", "3102448184", "DKOTHI-C@GAZT.GOV.SA");
                    Nreg_IdItem passportItem = taxPayerDetails?.Nreg_IdSet.results.Where(i => i.Type == "FS0002").FirstOrDefault();
                    PassportNumber = passportItem.Idnumber;
                    SelectedPassportIssueCountry = TaxpayerFullNationlityList.Where(i => i.Land1 == passportItem?.Country).FirstOrDefault()?.Landx50;
                    PassportIssueDate = passportItem?.ValidDateFrom?.ToString("dd/MM/yyyy", new CultureInfo("en-US"));
                    PassportExpireDate = passportItem?.ValidDateTo?.ToString("dd/MM/yyyy", new CultureInfo("en-US"));
                }else if(_enum == EstablishmentRegistrationTabsEnum.Outlets)
                {
                    var _outletTempData = await WebServiceManager.ESTOutletList(taxPayerDetails?.PortalUsrx, "3102448184", taxPayerDetails?.Fbnumx);
                    if (_outletTempData.Count > 0)
                    {
                        OutletData.Clear();
                        _outletTempData.ForEach(_out => OutletData.Add(_out));
                        RaisePropertyChanged(nameof(OutletData));
                    }

                    number = await WebServiceManager.ESTOutletNumber(taxPayerDetails?.Fbnumx);
                    taxPayerDetails = await WebServiceManager.ESTTaxPayerDetailGetService("03", "3102448184", "DKOTHI-C@GAZT.GOV.SA", number?.Actno, taxPayerDetails?.Fbnumx);
                    //await WebServiceManager.ESTOutletDropDowns();
                    //await WebServiceManager.ESTOutletGetActivitySetsList();
                    //ValidateCR crItem = await WebServiceManager.ESTValidateCRNum(taxPayerDetails?.Nreg_ActivitySet.results?.FirstOrDefault()?.Idnumber);
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

        private void ResidenceTypePrePopulateData(TaxPayerDetails taxPayerDetails)
        {
            if (taxPayerDetails.Orgresidence == "1")
            {
                OrgResidenceSelection(OrgResidenceNationalityEstablishmentRegistrationEnum.StayMoreThanKSA);
            }
            else if (taxPayerDetails.Orgresidence == "2")
            {
                OrgResidenceSelection(OrgResidenceNationalityEstablishmentRegistrationEnum.RentOwnhouseMoreThanThirtyDays);

            }
            else if (taxPayerDetails.Orgresidence == "3")
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
                    if (taxPayerDetails.Orgnonresidentactivity=="1")
                    {
                        SelectedOrgNonResidentActivity = "Derived from an activity which occurs in KSA";
                    }
                    else if (taxPayerDetails.Orgnonresidentactivity == "2")
                    {
                        SelectedOrgNonResidentActivity = "Derived from immoviable property located in the Kingdome";
                    }
                    else if (taxPayerDetails.Orgnonresidentactivity == "3")
                    {
                        SelectedOrgNonResidentActivity = "Derived from the disposal of shares or a partnership in resident company";
                    }
                    else if (taxPayerDetails.Orgnonresidentactivity == "4")
                    {
                        SelectedOrgNonResidentActivity = "Derived from lease of moveable properties used in Kingdome";

                    }
                    else if (taxPayerDetails.Orgnonresidentactivity == "5")
                    {
                        SelectedOrgNonResidentActivity = "Derived from Sales or license for use of industrial or intellectual Properties used in Kingdome";

                    }
                    else if (taxPayerDetails.Orgnonresidentactivity == "6")
                    {
                        SelectedOrgNonResidentActivity = "Dividends, Managment or directors fees paid by resident company";

                    }
                    else if (taxPayerDetails.Orgnonresidentactivity == "7")
                    {
                        SelectedOrgNonResidentActivity = "Amounts paid against services rendered to the company's head office or to an affiliated company";

                    }
                    else if (taxPayerDetails.Orgnonresidentactivity == "8")
                    {
                        SelectedOrgNonResidentActivity = "Amounts paid by a resident against serivces performed in whole or in part in the Kingdome";

                    }
                    else if (taxPayerDetails.Orgnonresidentactivity == "9")
                    {
                        SelectedOrgNonResidentActivity = "Amounts for exploitation of a natural resource in the kingdome";

                    }
                }
            }
        }
        #endregion
    }


}