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
using GAZT.Manager;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.EstablishmentRegistration
{
    public class EstablishmentRegistrationPageViewModel : BaseViewModel
    {
        #region Variable
        private TaxPayerDetails taxPayerDetails = null;
        private EstablishmentRegistrationTabsEnum _currentTab = EstablishmentRegistrationTabsEnum.RegistrationType;
        public EstablishmentRegistrationTabsEnum currentTab
        {
            get => _currentTab;
            private set
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

        public bool MarkComplete { get; private set; } = false;
        private int _maxIndex = 6;
        public int MaxIndex
        {
            get => _maxIndex; private set
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
            private set
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
            private set
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
            private set
            {
                _isClickedOwnRentOption = value;
                RaisePropertyChanged("IsClickedOwnRentOption");
            }
        }

        private bool _isClickedStayMoreThanKSAOption = false;
        public bool IsClickedStayMoreThanKSAOption
        {
            get => _isClickedStayMoreThanKSAOption;
            private set
            {
                _isClickedStayMoreThanKSAOption = value;
                RaisePropertyChanged("IsClickedStayMoreThanKSAOption");
            }
        }


        private bool _isClickedNoneOfTheAboveOption = false;
        public bool IsClickedNoneOfTheAboveOption
        {
            get => _isClickedNoneOfTheAboveOption;
            private set
            {
                _isClickedNoneOfTheAboveOption = value;
                RaisePropertyChanged("IsClickedNoneOfTheAboveOption");
            }
        }


        private bool _isClickedPermanentLegalEntity = false;
        public bool IsClickedPermanentLegalEntity
        {
            get => _isClickedPermanentLegalEntity;
            private set
            {
                _isClickedPermanentLegalEntity = value;
                RaisePropertyChanged("IsClickedPermanentLegalEntity");
            }
        }


        private bool _isClickedOtherTaxableIncomeLegalEntity = false;
        public bool IsClickedOtherTaxableIncomeLegalEntity
        {
            get => _isClickedOtherTaxableIncomeLegalEntity;
            private set
            {
                _isClickedOtherTaxableIncomeLegalEntity = value;
                RaisePropertyChanged("IsClickedOtherTaxableIncomeLegalEntity");
            }
        }




        private bool _isClickedABranchOfNonResidentCompanyPE = false;
        public bool IsClickedABranchOfNonResidentCompanyPE
        {
            get => _isClickedABranchOfNonResidentCompanyPE;
            private set
            {
                _isClickedABranchOfNonResidentCompanyPE = value;
                RaisePropertyChanged("IsClickedABranchOfNonResidentCompanyPE");
            }
        }



        private bool _isClickedConstructionSitePE = false;
        public bool IsClickedConstructionSitePE
        {
            get => _isClickedConstructionSitePE;
            private set
            {
                _isClickedConstructionSitePE = value;
                RaisePropertyChanged("IsClickedConstructionSitePE");
            }
        }


        private bool _isClickedInstallationPE = false;
        public bool IsClickedInstallationPE
        {
            get => _isClickedInstallationPE;
            private set
            {
                _isClickedInstallationPE = value;
                RaisePropertyChanged("IsClickedInstallationPE");
            }
        }


        private bool _isClickedAFixedBasePE = false;
        public bool IsClickedAFixedBasePE
        {
            get => _isClickedAFixedBasePE;
            private set
            {
                _isClickedAFixedBasePE = value;
                RaisePropertyChanged("IsClickedAFixedBasePE");
            }
        }

        private bool _isClickedNonResidentPartnerPE = false;
        public bool IsClickedNonResidentPartnerPE
        {
            get => _isClickedNonResidentPartnerPE;
            private set
            {
                _isClickedNonResidentPartnerPE = value;
                RaisePropertyChanged("IsClickedNonResidentPartnerPE");
            }
        }




        private ObservableCollection<string> _taxableIncomeSourceTypeList = new ObservableCollection<string>();
        public ObservableCollection<string> TaxableIncomeSourceTypeList
        {
            get
            {
                return _taxableIncomeSourceTypeList;
            }
            set
            {
                if (value != null)
                {
                    _taxableIncomeSourceTypeList = value;
                    RaisePropertyChanged(nameof(TaxableIncomeSourceTypeList));
                }
            }
        }

        private string _selectedTaxIncomeSourceType = null;
        public string SelectedTaxIncomeSourceType
        {
            get => _selectedTaxIncomeSourceType;
            private set
            {
                _selectedTaxIncomeSourceType = value;
                RaisePropertyChanged(nameof(SelectedTaxIncomeSourceType));
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
            private set
            {
                _selectedReportingBranch = value;
                RaisePropertyChanged(nameof(SelectedReportingBranch));
            }
        }


        private List<BranchesDropDownModel> _reportingBranchList = new List<BranchesDropDownModel>();
        public List<BranchesDropDownModel> ReportingBranchList
        {
            get => _reportingBranchList;
            private set
            {
                _reportingBranchList = value;
                RaisePropertyChanged(nameof(ReportingBranchList));
            }
        }


        private string _selectedEntityType = "Individual";
        public string SelectedEntityType
        {
            get => _selectedEntityType;
            private set
            {
                _selectedEntityType = value;
                RaisePropertyChanged(nameof(SelectedEntityType));
            }
        }

        private string _selectedTaxPayerType = "Trade/Business";
        public string SelectedTaxPayerType
        {
            get => _selectedTaxPayerType;
            private set
            {
                _selectedTaxPayerType = value;
                RaisePropertyChanged(nameof(SelectedTaxPayerType));
            }
        }

        private string _selectedRegNationalityType = "GCC";
        public string SelectedRegNationalityType
        {
            get => _selectedRegNationalityType;
            private set
            {
                _selectedRegNationalityType = value;
                RaisePropertyChanged(nameof(SelectedRegNationalityType));
            }
        }

        private string _selectedNationalityStatus;
        public string SelectedNationalityStatus
        {
            get => _selectedNationalityStatus;
            private set
            {
                _selectedNationalityStatus = value;
                RaisePropertyChanged(nameof(SelectedNationalityStatus));
            }
        }


        private string _selectedLegalEntity;
        public string SelectedLegalEntity
        {
            get => _selectedLegalEntity;
            private set
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
            private set
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
            private set
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
            private set
            {
                _gCCIDType = value;
                RaisePropertyChanged(nameof(GCCIDType));
            }
        }

        private string _gCCIDTypeIdNumberValue = "12345677899";
        public string GCCIDTypeIdNumberValue
        {
            get => _gCCIDTypeIdNumberValue;
            private set
            {
                _gCCIDTypeIdNumberValue = value;
                RaisePropertyChanged(nameof(GCCIDTypeIdNumberValue));
            }
        }

        private string _selectedDOB = "26/08/2020";
        public string SelectedDOB
        {
            get => _selectedDOB;
            private set
            {
                _selectedDOB = value;
                RaisePropertyChanged(nameof(SelectedDOB));
            }
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            private set
            {
                _firstName = value;
                RaisePropertyChanged(nameof(FirstName));
            }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            private set
            {
                _lastName = value;
                RaisePropertyChanged(nameof(LastName));
            }
        }

        private string _fatherName;
        public string FatherName
        {
            get => _fatherName;
            private set
            {
                _fatherName = value;
                RaisePropertyChanged(nameof(FatherName));
            }
        }

        private string _grandFatherName;
        public string GrandFatherName
        {
            get => _grandFatherName;
            private set
            {
                _grandFatherName = value;
                RaisePropertyChanged(nameof(GrandFatherName));
            }
        }

        private string _familyName;
        public string FamilyName
        {
            get => _familyName;
            private set
            {
                _familyName = value;
                RaisePropertyChanged(nameof(FamilyName));
            }
        }

        private string _initial;
        public string Initial
        {
            get => _initial;
            private set
            {
                _initial = value;
                RaisePropertyChanged(nameof(Initial));
            }
        }

        private List<TaxpayerNationality> _taxpayerPDNationlityList;
        public List<TaxpayerNationality> TaxpayerPDNationlityList
        {
            get => _taxpayerPDNationlityList;
            private set
            {
                _taxpayerPDNationlityList = value;
                RaisePropertyChanged(nameof(TaxpayerPDNationlityList));
            }
        }

        private TaxpayerNationality _selectedTaxpayerPDNationality;
        public TaxpayerNationality SelectedTaxpayerPDNationality
        {
            get => _selectedTaxpayerPDNationality;
            private set
            {
                _selectedTaxpayerPDNationality = value;
                RaisePropertyChanged(nameof(SelectedTaxpayerPDNationality));
            }
        }

        private List<string> _citizenList;
        public List<string> CitizenList
        {
            get => _citizenList;
            private set
            {
                _citizenList = value;
                RaisePropertyChanged(nameof(CitizenList));
            }
        }

        private string _selectedCitizen;
        public string SelectedCitizen
        {
            get => _selectedCitizen;
            private set
            {
                _selectedCitizen = value;
                RaisePropertyChanged(nameof(SelectedCitizen));
            }
        }

        private List<string> _residenceList;
        public List<string> ResidenceList
        {
            get => _residenceList;
            private set
            {
                _residenceList = value;
                RaisePropertyChanged(nameof(ResidenceList));
            }
        }

        private string _selectedResidence;
        public string SelectedResidence
        {
            get => _selectedResidence;
            private set
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
            private set
            {
                _passportNumber = value;
                RaisePropertyChanged(nameof(PassportNumber));
            }
        }

        private string _passportIssueCountryList;
        public string PassportIssueCountryList
        {
            get => _passportIssueCountryList;
            private set
            {
                _passportIssueCountryList = value;
                RaisePropertyChanged(nameof(PassportIssueCountryList));
            }
        }

        private string _selectedPassportIssueCountry;
        public string SelectedPassportIssueCountry
        {
            get => _selectedPassportIssueCountry;
            private set
            {
                _selectedPassportIssueCountry = value;
                RaisePropertyChanged(nameof(SelectedPassportIssueCountry));
            }
        }


        private string _passportIssueDate;
        public string PassportIssueDate
        {
            get => _passportIssueDate;
            private set
            {
                _passportIssueDate = value;
                RaisePropertyChanged(nameof(PassportIssueDate));
            }
        }

        private string _passportExpireDate;
        public string PassportExpireDate
        {
            get => _passportExpireDate;
            private set
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
            private set
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
            private set
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
            private set
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
            private set
            {
                _fiscalMonth = value;
                RaisePropertyChanged(nameof(FiscalMonth));
            }
        }
        private string _fiscalDay = "28";
        public string FiscalDay
        {
            get => _fiscalDay;
            private set
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
            private set
            {
                _summaryExpendedCard = value;
                RaisePropertyChanged(nameof(SummaryExpendedCard));
            }
        }
        private ObservableCollection<string> _outletList = new ObservableCollection<string>();
        public ObservableCollection<string> OutletList
        {
            get => _outletList;
            private set
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


        public ICommand OnNextButtonClick { get; private set; }
        public ICommand OnPreButtonClick { get; private set; }
        public ICommand OnVoidOrSaveDraftClick { get; private set; }


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

        public ICommand OnPassportIssueCountryButtonClick { get; private set; }

        public ICommand OnPassportAttachmentTapped { get; private set; }

        public ICommand OnPassportCloseTapped { get; private set; }


        #endregion


        #region Outlet Tabs commands
        public ICommand OnNewOutletButtonClick { get; set; }
        #endregion


        #region Financial Details Tabs commands
        public ICommand OnMonthSelectButtonClick { get; set; }
        public ICommand OnDaySelectButtonClick { get; set; }
        #endregion


        #region Summary Tabs commands
        public ICommand OnExpendGridViewClick { get; private set; }
        #endregion

        #endregion

        #region Constructor
        public EstablishmentRegistrationPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            OnNextButtonClick = new Command(() => navigateToNext());
            OnPreButtonClick = new Command(() => navigateToPre());

            GetMenuListFromServer();
            #region Registration Tab Variable initialization

            NationalityStatusStayMoreThanKSAClick = new Command(() => nationalityStatusSelection(EstablishmentRegistrationNationalityEnum.StayMoreThanKSA));
            NationalityStatusRentOwnHouseClick = new Command(() => nationalityStatusSelection(EstablishmentRegistrationNationalityEnum.RentOwnhouseMoreThanThirtyDays));
            NationalityStatusNoneOfTheAboveClick = new Command(() => nationalityStatusSelection(EstablishmentRegistrationNationalityEnum.NoneOfTheAbove));
            PermanentEstablishmentLegalEntityClick = new Command(() => legalEntitySelection(EstablishmentRegistrationLegalEntityEnum.PermanentEstablishment));
            OtherTaxableIncomeLegalEntityClick = new Command(() => legalEntitySelection(EstablishmentRegistrationLegalEntityEnum.OtherTaxIncomeFromSourceWithInTheSKA));

            #region Permanent Establishment options

            ABranchOfNonResidentCompanyPEClick = new Command(() => permanentEstablishmentsOptionSelection(EstablishmentRegistrationParmanentEstablishmentEnum.ABranchOfNonResidentCompanyPE));
            ConstructionSitePEClick = new Command(() => permanentEstablishmentsOptionSelection(EstablishmentRegistrationParmanentEstablishmentEnum.ConstructionSitePE));
            InstallationPEClick = new Command(() => permanentEstablishmentsOptionSelection(EstablishmentRegistrationParmanentEstablishmentEnum.InstallationPE));
            AFixedBasePEClick = new Command(() => permanentEstablishmentsOptionSelection(EstablishmentRegistrationParmanentEstablishmentEnum.AFixedBasePE));
            NonResidentPartnerPEClick = new Command(() => permanentEstablishmentsOptionSelection(EstablishmentRegistrationParmanentEstablishmentEnum.NonResidentPartnerPE));

            #endregion

            #region Attachment commands 
            OnERAttachmentCloseTapped = new Command(() => onERAttachmentCloseTapped());
            OnEstablishmentRegistrationAttachmentTapped = new Command(() => onEstablishmentRegistrationAttachmentTapped());
            #endregion

            taxableIncomeSourceTypeListObjPreparation();

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
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(TaxpayerPDNationlityList);
                poupWindow.OnItemSelect = (item) => SelectedTaxpayerPDNationality = item as TaxpayerNationality;
                PopupNavigation.Instance.PushAsync(poupWindow);
            });

            OnPDCitizenSelectButtonClick = new Command(() =>
            {
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(CitizenList);
                poupWindow.OnItemSelect = (item) => SelectedCitizen = item as string;
                PopupNavigation.Instance.PushAsync(poupWindow);
            });

            OnPDResidenceSelectButtonClick = new Command(() =>
            {
                ListPopUpViewPage poupWindow = new ListPopUpViewPage(ResidenceList);
                poupWindow.OnItemSelect = (item) => SelectedResidence = item as string;
                PopupNavigation.Instance.PushAsync(poupWindow);
            });
            #endregion

            #region Passport Variable Initialization

            OnPassportAttachmentTapped = new Command(() => onPassportAttachmentTapped());
            OnPassportCloseTapped = new Command(() => onPassportCloseTapped());
            #endregion

            #region Outlet Tabs variable initialization
            OnNewOutletButtonClick = new Command(() =>
            {
                System.Diagnostics.Debug.WriteLine("OnNewOutletButtonClick " + navigationService);
                _navigationService.NavigateTo(App.OutletDetailsPageView, new OutletNavigationModels());
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
            var branchTask = GetReportingBranchListFromServer();
            var nationalityTask = GetPdNationalityListFromServer();
            var citizenTask = GetPdCitizenListFromServer();
            var residenceTask = GetPdResidenceListFromServer();
            await Task.WhenAll(branchTask, nationalityTask, citizenTask, residenceTask);
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

        private void nationalityStatusSelection(EstablishmentRegistrationNationalityEnum selectedOption)
        {
            switch (selectedOption)
            {
                case EstablishmentRegistrationNationalityEnum.StayMoreThanKSA:
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
                case EstablishmentRegistrationNationalityEnum.RentOwnhouseMoreThanThirtyDays:

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
                case EstablishmentRegistrationNationalityEnum.NoneOfTheAbove:
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

        private void legalEntitySelection(EstablishmentRegistrationLegalEntityEnum selectedOption)
        {
            switch (selectedOption)
            {
                case EstablishmentRegistrationLegalEntityEnum.PermanentEstablishment:

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
                case EstablishmentRegistrationLegalEntityEnum.OtherTaxIncomeFromSourceWithInTheSKA:

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

        private void permanentEstablishmentsOptionSelection(EstablishmentRegistrationParmanentEstablishmentEnum selectedOption)
        {
            switch (selectedOption)
            {
                case EstablishmentRegistrationParmanentEstablishmentEnum.ABranchOfNonResidentCompanyPE:
                    IsClickedABranchOfNonResidentCompanyPE = true;
                    IsClickedConstructionSitePE = false;
                    IsClickedInstallationPE = false;
                    IsClickedAFixedBasePE = false;
                    IsClickedNonResidentPartnerPE = false;
                    break;
                case EstablishmentRegistrationParmanentEstablishmentEnum.ConstructionSitePE:
                    IsClickedABranchOfNonResidentCompanyPE = false;
                    IsClickedConstructionSitePE = true;
                    IsClickedInstallationPE = false;
                    IsClickedAFixedBasePE = false;
                    IsClickedNonResidentPartnerPE = false;
                    break;
                case EstablishmentRegistrationParmanentEstablishmentEnum.InstallationPE:
                    IsClickedABranchOfNonResidentCompanyPE = false;
                    IsClickedConstructionSitePE = false;
                    IsClickedInstallationPE = true;
                    IsClickedAFixedBasePE = false;
                    IsClickedNonResidentPartnerPE = false;
                    break;
                case EstablishmentRegistrationParmanentEstablishmentEnum.AFixedBasePE:
                    IsClickedABranchOfNonResidentCompanyPE = false;
                    IsClickedConstructionSitePE = false;
                    IsClickedInstallationPE = false;
                    IsClickedAFixedBasePE = true;
                    IsClickedNonResidentPartnerPE = false;
                    break;
                case EstablishmentRegistrationParmanentEstablishmentEnum.NonResidentPartnerPE:
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


        private void taxableIncomeSourceTypeListObjPreparation()
        {
            TaxableIncomeSourceTypeList.Clear();
            TaxableIncomeSourceTypeList.Add("Derived from an activity which occurs in KSA");
            TaxableIncomeSourceTypeList.Add("Derived from immoviable property located in the Kingdome");
            TaxableIncomeSourceTypeList.Add("Derived from the disposal of shares or a partnership in resident company");
            TaxableIncomeSourceTypeList.Add("Derived from lease of moveable properties used in Kingdome");
            TaxableIncomeSourceTypeList.Add("Derived from Sales or license for use of industrial or intellectual Properties used in Kingdome");
            TaxableIncomeSourceTypeList.Add("Dividends, Managment or directors fees paid by resident company");
            TaxableIncomeSourceTypeList.Add("Amounts paid against services rendered to the company's head office or to an affiliated company");
            TaxableIncomeSourceTypeList.Add("Amounts paid by a resident against serivces performed in whole or in part in the Kingdome");
            TaxableIncomeSourceTypeList.Add("Amounts for exploitation of a natural resource in the kingdome");



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

        private async Task GetPdNationalityListFromServer(string nationality = null)
        {
            //if (TaxpayerPDNationlityList == null)
            //{
            TaxpayerPDNationlityList = await WebServiceManager.ESTTaxPayerNationality(nationality);
            //}

        }

        private async Task GetPdCitizenListFromServer()
        {
            if (CitizenList == null)
            {
                CitizenList = new List<string> { "11", "10", "09", "08", "07", "06", "05", "04", "03", "02", "01" };

                //await WebServiceManager.();
            }

        }

        private async Task GetPdResidenceListFromServer()
        {
            if (ResidenceList == null)
            {
                ResidenceList = new List<string> { "11", "10", "09", "08", "07", "06", "05", "04", "03", "02", "01" };

                //await WebServiceManager.();
            }
        }

        private void GetMenuListFromServer()
        {
            if (DraftMenuList == null)
            {
                DraftMenuList = new List<string> { "Save", "Void", "CalenderType" };

                //await WebServiceManager.();
            }
        }



        private void onPassportCloseTapped()
        {
        }

        private void onPassportAttachmentTapped()
        {
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

                    SelectedTaxpayerPDNationality = TaxpayerPDNationlityList.Where(i => i.Land1 == taxPayerDetails?.Natio).FirstOrDefault();
                    SelectedCitizen = TaxpayerPDNationlityList.Where(i => i.Land1 == taxPayerDetails?.Citizen).FirstOrDefault()?.Landx50;
                    SelectedResidence = TaxpayerPDNationlityList.Where(i => i.Land1 == taxPayerDetails?.Residence).FirstOrDefault()?.Landx50;
                }
                else if (_enum == EstablishmentRegistrationTabsEnum.PassportDetails)
                {
                    taxPayerDetails = await WebServiceManager.ESTTaxPayerDetailGetService("02", "3102448184", "DKOTHI-C@GAZT.GOV.SA");
                    Nreg_IdItem passportItem = taxPayerDetails?.Nreg_IdSet.results.Where(i => i.Type == "FS0002").FirstOrDefault();
                    PassportNumber = passportItem.Idnumber;
                    SelectedPassportIssueCountry = TaxpayerPDNationlityList.Where(i => i.Land1 == passportItem?.Country).FirstOrDefault()?.Landx50;
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

                    //OutletNumber number = await WebServiceManager.ESTOutletNumber(taxPayerDetails?.Fbnumx);
                    //taxPayerDetails = await WebServiceManager.ESTTaxPayerDetailGetService("03", "3102448184", "DKOTHI-C@GAZT.GOV.SA", number?.Actno, taxPayerDetails?.Fbnumx);
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
        #endregion
    }


}