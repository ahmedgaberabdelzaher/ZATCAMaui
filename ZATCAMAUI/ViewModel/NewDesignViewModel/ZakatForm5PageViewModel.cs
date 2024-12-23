
using System.Globalization;
using System.Windows.Input;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models.Form5Models;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel
{

    public class ZakatForm5PageViewModel : BaseViewModel
    {

        #region Variable


        private ZakatForm5TabEnum _currentTab = ZakatForm5TabEnum.BasicInformation;
        public ZakatForm5TabEnum currentTab
        {
            get => _currentTab;
            private set
            {
                if (_currentTab == value) return;
                _currentTab = value;
                OnPropertyChanged(nameof(currentTab));
                CurrentIndex = (int)_currentTab;
                OnPropertyChanged(nameof(CurrentIndex));
            }
        }
        private int _currenrIndex = 1;
        public int CurrentIndex
        {
            get => _currenrIndex;
            set
            {
                if (_currenrIndex == value) return;

                _currenrIndex = value;
                OnPropertyChanged(nameof(CurrentIndex));
                if (_currenrIndex == MaxIndex)
                {
                    MarkComplete = true;
                    OnPropertyChanged(nameof(MarkComplete));
                }
            }
        }
        public bool MarkComplete { get; private set; } = false;
        public int MaxIndex { get; private set; } = 3;
        #endregion

        #region Property

        public bool _isAdditionalVisible = false;
        public bool isAdditionalVisible
        {
            get
            {
                return _isAdditionalVisible;
            }
            set
            {
                if (_isAdditionalVisible == value) return;

                _isAdditionalVisible = value;
                OnPropertyChanged("isAdditionalVisible");
            }
        }

        public bool _isMineralVisible = false;
        public bool isMineralVisible
        {
            get
            {
                return _isMineralVisible;
            }
            set
            {
                if (_isMineralVisible == value) return;

                _isMineralVisible = value;
                OnPropertyChanged("isMineralVisible");
            }
        }

        public bool _isCarVisible = false;
        public bool isCarVisible
        {
            get
            {
                return _isCarVisible;
            }
            set
            {
                if (_isCarVisible == value) return;

                _isCarVisible = value;
                OnPropertyChanged("isCarVisible");
            }
        }

        public bool _isPoultryVisible = false;
        public bool isPoultryVisible
        {
            get
            {
                return _isPoultryVisible;
            }
            set
            {
                if (_isPoultryVisible == value) return;

                _isPoultryVisible = value;
                OnPropertyChanged("isPoultryVisible");
            }
        }

        public bool _isEducationVisible = false;
        public bool isEducationVisible
        {
            get
            {
                return _isEducationVisible;
            }
            set
            {
                if (_isEducationVisible == value) return;

                _isEducationVisible = value;
                OnPropertyChanged("isEducationVisible");
            }
        }

        public bool _isHotelVisible = false;
        public bool isHotelVisible
        {
            get
            {
                return _isHotelVisible;
            }
            set
            {
                if (_isHotelVisible == value) return;

                _isHotelVisible = value;
                OnPropertyChanged("isHotelVisible");
            }
        }

        public bool _isRealEstateVisible = false;
        public bool isRealEstateVisible
        {
            get
            {
                return _isRealEstateVisible;
            }
            set
            {
                if (_isRealEstateVisible == value) return;

                _isRealEstateVisible = value;
                OnPropertyChanged("isRealEstateVisible");
            }
        }

        public bool _isContractingVisible = false;
        public bool isContractingVisible
        {
            get
            {
                return _isContractingVisible;
            }
            set
            {
                if (_isContractingVisible == value) return;

                _isContractingVisible = value;
                OnPropertyChanged("isContractingVisible");
            }
        }

        public bool _isIndustryVisible = false;
        public bool isIndustryVisible
        {
            get
            {
                return _isIndustryVisible;
            }
            set
            {
                if (_isIndustryVisible == value) return;

                _isIndustryVisible = value;
                OnPropertyChanged("isIndustryVisible");
            }
        }

        public bool _isLabourOccupancyVisible = false;
        public bool isLabourOccupancyVisible
        {
            get
            {
                return _isLabourOccupancyVisible;
            }
            set
            {
                if (_isLabourOccupancyVisible == value) return;

                _isLabourOccupancyVisible = value;
                OnPropertyChanged("isLabourOccupancyVisible");
            }
        }

        public bool _isBuyVisible = false;
        public bool isBuyVisible
        {
            get
            {
                return _isBuyVisible;
            }
            set
            {
                if (_isBuyVisible == value) return;

                _isBuyVisible = value;
                OnPropertyChanged("isBuyVisible");
            }
        }

        public bool _isProfessionalVisible = false;
        public bool isProfessionalVisible
        {
            get
            {
                return _isProfessionalVisible;
            }
            set
            {
                if (_isProfessionalVisible == value) return;

                _isProfessionalVisible = value;
                OnPropertyChanged("isProfessionalVisible");
            }
        }

        public bool _isCabVisible = true;
        public bool isCabVisible
        {
            get
            {
                return _isCabVisible;
            }
            set
            {
                if (_isCabVisible == value) return;

                _isCabVisible = value;
                OnPropertyChanged("isCabVisible");
            }
        }

        public string _nextText = AppResources.ZZNext;
        public string NextText
        {
            get
            {
                return _nextText;
            }
            set
            {
                if (_nextText == value) return;

                _nextText = value;
                OnPropertyChanged("NextText");
            }
        }
        #endregion

        #region Commands
        public ICommand OnNextButtonClick { get; private set; }
        public ICommand OnBackButtonClick { get; private set; }

        public ICommand OnAppearingZakatFormPageCommand
        {
            get
            {
                return new Command(async _ =>
                {
                    await LoadZakatForm5Data();
                    App.IsComingFromSleepMode = false;
                    NextText = AppResources.ZZNext;
                    setCurrentTab();

                });
            }
        }

        #endregion

        #region Constructor
        public ZakatForm5PageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            OnNextButtonClick = new Command(() => navigateToNext());
            OnBackButtonClick = new Command(() => navigateBack());
        }
        #endregion

        #region Basic Information


        public ZakatForm5Data _zakatForm5DataResult;
        public ZakatForm5Data ZakatForm5DataResult
        {
            get
            {
                return _zakatForm5DataResult;
            }
            set
            {
                if (_zakatForm5DataResult == value) return;

                _zakatForm5DataResult = value;
                OnPropertyChanged("ZakatForm5DataResult");
            }
        }

        private bool _isNoDataLableVisible = false;
        public bool isNoDataLableVisible
        {
            get
            {
                return _isNoDataLableVisible;
            }
            set
            {
                if (_isNoDataLableVisible == value) return;

                _isNoDataLableVisible = value;
                OnPropertyChanged("isNoDataLableVisible");
            }
        }



        public string _fbguid;
        public string Fbguid
        {
            get => _fbguid;

            set
            {
                if (_fbguid == value) return;

                _fbguid = value;
                OnPropertyChanged(nameof( Fbguid));
            }
        }



        public string _financialYear;
        public string FinancialYear
        {
            get => _financialYear;

            set
            {
                if (_financialYear == value) return;

                _financialYear = value;
                OnPropertyChanged(nameof( FinancialYear));
            }
        }




        public string _period;
        public string Period
        {
            get => _period;

            set
            {
                if (_period == value) return;

                _period = value;
                OnPropertyChanged(nameof( Period));
            }
        }

        public string _ZakatToDate;
        public string ZakatToDate
        {
            get => _ZakatToDate;

            set
            {
                if (_ZakatToDate == value) return;

                _ZakatToDate = value;
                OnPropertyChanged(nameof( ZakatToDate));
            }
        }


        public string _ZakatFromDate;
        public string ZakatFromDate
        {
            get => _ZakatFromDate;

            set
            {
                if (_ZakatFromDate == value) return;

                _ZakatFromDate = value;
                OnPropertyChanged(nameof( ZakatFromDate));
            }
        }
        //Taxpayer Detials


        public string _taxpayer;
        public string Taxpayer
        {
            get => _taxpayer;

            set
            {
                if (_taxpayer == value) return;

                _taxpayer = value;
                OnPropertyChanged(nameof( Taxpayer));
            }
        }


        public string _branch;
        public string Branch
        {
            get => _branch;

            set
            {
                if (_branch == value) return;

                _branch = value;
                OnPropertyChanged(nameof( Branch));
            }
        }


        public string _address;
        public string Address
        {
            get => _address;

            set
            {

                var newAddress = value.Replace(@", ,", "");
                newAddress = newAddress.Replace(@", ,", "");
                newAddress = newAddress.Replace(@",,", "");
                _address = newAddress.Replace(@" ,", "");
                OnPropertyChanged(nameof( Address));
            }
        }

        public string _userEmail;
        public string UserEmail
        {
            get => _userEmail;

            set
            {
                if (_userEmail == value) return;

                _userEmail = value;
                OnPropertyChanged(nameof( UserEmail));
            }
        }

        public string _mobileNumber;
        public string MobileNumber
        {
            get => _mobileNumber;

            set
            {
                if (_mobileNumber == value) return;

                _mobileNumber = value;
                OnPropertyChanged(nameof( MobileNumber));
            }
        }


        //Registration Information
        public string _numberOfOutlet;
        public string NumberOfOutlet
        {
            get => _numberOfOutlet;

            set
            {
                if (_numberOfOutlet == value) return;

                _numberOfOutlet = value;
                OnPropertyChanged(nameof( NumberOfOutlet));
            }
        }


        public string _residencyStatus;
        public string Residency_Status
        {
            get => _residencyStatus;

            set
            {
                if (_residencyStatus == value) return;

                _residencyStatus = value;
                OnPropertyChanged(nameof( Residency_Status));
            }
        }

        public string _mainOutlet;
        public string MainOutlet
        {
            get => _mainOutlet;

            set
            {
                if (_mainOutlet == value) return;

                _mainOutlet = value;
                OnPropertyChanged(nameof( MainOutlet));
            }
        }

        public string _accMethod;
        public string AccountMethod
        {

            get => _accMethod;

            set
            {
                if (_accMethod == value) return;

                _accMethod = value;
                OnPropertyChanged(nameof( AccountMethod));
            }
        }

        public string _financialPeriod;
        public string FinancialPeriod
        {
            get => _financialPeriod;

            set
            {
                if (_financialPeriod == value) return;

                _financialPeriod = value;
                OnPropertyChanged(nameof( FinancialPeriod));
            }
        }

        public string _calendarType;
        public string Calendar_Type
        {
            get => _calendarType;

            set
            {
                if (_calendarType == value) return;

                _calendarType = value;
                OnPropertyChanged(nameof( Calendar_Type));
            }
        }


        public bool _IsConditionRadio;
        public bool IsConditionRadio
        {
            get => _IsConditionRadio;

            set
            {
                if (_IsConditionRadio == value) return;

                _IsConditionRadio = value;
                OnPropertyChanged(nameof( IsConditionRadio));
            }
        }

        #endregion

        #region Financial Information


        /// chips visibility
        private bool _IsCabBtn = false;
        public bool IsCabBtn
        {
            get
            {
                return _IsCabBtn;
            }
            set
            {
                if (_IsCabBtn == value) return;

                _IsCabBtn = value;
                OnPropertyChanged("IsCabBtn");
            }
        }


        private bool _IsProfessionBtn = false;
        public bool IsProfessionBtn
        {
            get
            {
                return _IsProfessionBtn;
            }
            set
            {
                if (_IsProfessionBtn == value) return;

                _IsProfessionBtn = value;
                OnPropertyChanged("IsProfessionBtn");
            }
        }

        private bool _IsSellBtn = false;
        public bool IsSellBtn
        {
            get
            {
                return _IsSellBtn;
            }
            set
            {
                if (_IsSellBtn == value) return;

                _IsSellBtn = value;
                OnPropertyChanged("IsSellBtn");
            }
        }

        private bool _IsLabourBtn = false;
        public bool IsLabourBtn
        {
            get
            {
                return _IsLabourBtn;
            }
            set
            {
                if (_IsLabourBtn == value) return;

                _IsLabourBtn = value;
                OnPropertyChanged("IsLabourBtn");
            }
        }

        private bool _IsIndustryBtn = false;
        public bool IsIndustryBtn
        {
            get
            {
                return _IsIndustryBtn;
            }
            set
            {
                if (_IsIndustryBtn == value) return;

                _IsIndustryBtn = value;
                OnPropertyChanged("IsIndustryBtn");
            }
        }

        private bool _IsContractBtn = false;
        public bool IsContractBtn
        {
            get
            {
                return _IsContractBtn;
            }
            set
            {
                if (_IsContractBtn == value) return;

                _IsContractBtn = value;
                OnPropertyChanged("IsContractBtn");
            }
        }

        private bool _IsInvestBtn = false;
        public bool IsInvestBtn
        {
            get
            {
                return _IsInvestBtn;
            }
            set
            {
                if (_IsInvestBtn == value) return;

                _IsInvestBtn = value;
                OnPropertyChanged("IsInvestBtn");
            }
        }


        private bool _IsHotelsBtn = false;
        public bool IsHotelsBtn
        {
            get
            {
                return _IsHotelsBtn;
            }
            set
            {
                if (_IsHotelsBtn == value) return;

                _IsHotelsBtn = value;
                OnPropertyChanged("IsHotelsBtn");
            }
        }


        private bool _IsEduBtn = false;
        public bool IsEduBtn
        {
            get
            {
                return _IsEduBtn;
            }
            set
            {
                if (_IsEduBtn == value) return;

                _IsEduBtn = value;
                OnPropertyChanged("IsEduBtn");
            }
        }

        private bool _IsPoultryBtn = false;
        public bool IsPoultryBtn
        {
            get
            {
                return _IsPoultryBtn;
            }
            set
            {
                if (_IsPoultryBtn == value) return;

                _IsPoultryBtn = value;
                OnPropertyChanged("IsPoultryBtn");
            }
        }


        private bool _IsCarBtn = false;
        public bool IsCarBtn
        {
            get
            {
                return _IsCarBtn;
            }
            set
            {
                if (_IsCarBtn == value) return;

                _IsCarBtn = value;
                OnPropertyChanged("IsCarBtn");
            }
        }


        private bool _IsMineralsBtn = false;
        public bool IsMineralsBtn
        {
            get
            {
                return _IsMineralsBtn;
            }
            set
            {
                if (_IsMineralsBtn == value) return;

                _IsMineralsBtn = value;
                OnPropertyChanged("IsMineralsBtn");
            }
        }

        private bool _IsAddBtn = true;
        public bool IsAddBtn
        {
            get
            {
                return _IsAddBtn;
            }
            set
            {
                if (_IsAddBtn == value) return;

                _IsAddBtn = value;
                OnPropertyChanged("IsAddBtn");
            }
        }


        /// <summary>
        ///  No of Entity List which shows horizontal chips scroll
        /// </summary>
        /// 
        private List<string> _noOfEntityList = new List<string>();
        public List<string> NoOFEntityList
        {
            get
            {
                return _noOfEntityList;
            }
            set
            {
                if (_noOfEntityList == value) return;

                _noOfEntityList = value;
                OnPropertyChanged("NoOFEntityList");
            }
        }



        /// <summary>
        ///  Cabs List
        /// </summary>

        private List<Result28> _cabsList;
        public List<Result28> Cabs
        {
            get
            {
                return _cabsList;
            }
            set
            {
                if (_cabsList == value) return;

                _cabsList = value;
                OnPropertyChanged("Cabs");
            }
        }


        /// <summary>
        ///  Professionals List
        /// </summary>

        private List<Result27> _professionalsList;
        public List<Result27> Professionals
        {
            get
            {
                return _professionalsList;
            }
            set
            {
                if (_professionalsList == value) return;

                _professionalsList = value;
                OnPropertyChanged("Professionals");
            }
        }

        /// <summary>
        ///  Sell & Buy List
        /// </summary>

        private List<Results9> _sell_BuyList;
        public List<Results9> Sell_Buy
        {
            get
            {
                return _sell_BuyList;
            }
            set
            {
                if (_sell_BuyList == value) return;

                _sell_BuyList = value;
                OnPropertyChanged("Sell_Buy");
            }
        }

        /// <summary>
        ///  Labour Occup List
        /// </summary>

        private List<Results8> _labourOccupList;
        public List<Results8> LabourOccup
        {
            get
            {
                return _labourOccupList;
            }
            set
            {
                if (_labourOccupList == value) return;

                _labourOccupList = value;
                OnPropertyChanged("LabourOccup");
            }
        }

        /// <summary>
        ///  Industry List
        /// </summary>

        private List<Results7> _industryList;
        public List<Results7> Industry
        {
            get
            {
                return _industryList;
            }
            set
            {
                if (_industryList == value) return;

                _industryList = value;
                OnPropertyChanged("Industry");
            }
        }

        /// <summary>
        ///  Contracting CO. List
        /// </summary>

        private List<Results6> _contractingList;
        public List<Results6> Contracting
        {
            get
            {
                return _contractingList;
            }
            set
            {
                if (_contractingList == value) return;

                _contractingList = value;
                OnPropertyChanged("Contracting");
            }
        }

        /// <summary>
        ///  Invst & Real Estate List
        /// </summary>

        private List<Results5> _invstRealEstList;
        public List<Results5> InvstRealEst
        {
            get
            {
                return _invstRealEstList;
            }
            set
            {
                if (_invstRealEstList == value) return;

                _invstRealEstList = value;
                OnPropertyChanged("InvstRealEst");
            }
        }

        /// <summary>
        ///  Hotels List
        /// </summary>

        private List<Result26> _hotelsList;
        public List<Result26> Hotels
        {
            get
            {
                return _hotelsList;
            }
            set
            {
                if (_hotelsList == value) return;

                _hotelsList = value;
                OnPropertyChanged("Hotels");
            }
        }

        /// <summary>
        ///  Edu. & Health List
        /// </summary>

        private List<Result25> _edu_HealthList;
        public List<Result25> Edu_Health
        {
            get
            {
                return _edu_HealthList;
            }
            set
            {
                if (_edu_HealthList == value) return;

                _edu_HealthList = value;
                OnPropertyChanged("Edu_Health");
            }
        }

        /// <summary>
        ///  Poultry and Fish Farms Activities List
        /// </summary>

        private List<Result24> _poultry_FishFarmList;
        public List<Result24> Poultry_FishFarm
        {
            get
            {
                return _poultry_FishFarmList;
            }
            set
            {
                if (_poultry_FishFarmList == value) return;

                _poultry_FishFarmList = value;
                OnPropertyChanged("Poultry_FishFarm");
            }
        }

        /// <summary>
        ///  Cars List
        /// </summary>

        private List<Result23> _carsList;
        public List<Result23> Cars
        {
            get
            {
                return _carsList;
            }
            set
            {
                if (_carsList == value) return;

                _carsList = value;
                OnPropertyChanged("Cars");
            }
        }

        /// <summary>
        ///  Minerals List
        /// </summary>

        private List<Result22> _mineralsList;
        public List<Result22> Minerals
        {
            get
            {
                return _mineralsList;
            }
            set
            {
                if (_mineralsList == value) return;

                _mineralsList = value;
                OnPropertyChanged("Minerals");
            }
        }

        /// <summary>
        /// Share in persons company
        /// </summary>

        public string _isOtherComShareApp;
        public string IsOtherComShareApp
        {
            get => _isOtherComShareApp;

            set
            {
                if (_isOtherComShareApp == value) return;

                _isOtherComShareApp = value;
                OnPropertyChanged(nameof( IsOtherComShareApp));
            }
        }



        private bool _IsOtherComShareAppVisible = false;
        public bool IsOtherComShareAppVisible
        {
            get
            {
                return _IsOtherComShareAppVisible;
            }
            set
            {
                if (_IsOtherComShareAppVisible == value) return;

                _IsOtherComShareAppVisible = value;
                OnPropertyChanged("IsOtherComShareAppVisible");
            }
        }


        public string _otherCompanyShare;
        public string OtherCompanyShare
        {
            get => _otherCompanyShare;

            set
            {
                if (_otherCompanyShare == value) return;

                _otherCompanyShare = value;
                OnPropertyChanged(nameof( OtherCompanyShare));
            }
        }


        public string _zakatBase;
        public string ZakatBase
        {
            get => _zakatBase;

            set
            {
                if (_zakatBase == value) return;

                _zakatBase = value;
                OnPropertyChanged(nameof( ZakatBase));
            }
        }

        /// <summary>
        /// Declaration
        /// </summary>

        public string _noOfBranch;
        public string NoOfBranch
        {
            get => _noOfBranch;

            set
            {
                if (_noOfBranch == value) return;

                _noOfBranch = value;
                OnPropertyChanged(nameof( NoOfBranch));
            }
        }

        public string _noofEmp;
        public string NoofEmp
        {
            get => _noofEmp;

            set
            {
                if (_noofEmp == value) return;

                _noofEmp = value;
                OnPropertyChanged(nameof( NoofEmp));
            }
        }

        public string _yearRent;
        public string YearRent
        {
            get => _yearRent;

            set
            {
                if (_yearRent == value) return;

                _yearRent = value;
                OnPropertyChanged(nameof( YearRent));
            }
        }

        public string _totalAnnualSalary;
        public string TotalAnnualSalary
        {
            get => _totalAnnualSalary;

            set
            {
                if (_totalAnnualSalary == value) return;

                _totalAnnualSalary = value;
                OnPropertyChanged(nameof( TotalAnnualSalary));
            }
        }

        /// <summary>
        /// Zakat Details
        /// </summary>

        public string _zakatable;
        public string Zakatable
        {
            get => _zakatable;

            set
            {
                if (_zakatable == value) return;

                _zakatable = value;
                OnPropertyChanged(nameof( Zakatable));
            }
        }

        public string _zakat;
        public string Zakat
        {
            get => _zakat;

            set
            {
                if (_zakat == value) return;

                _zakat = value;
                OnPropertyChanged(nameof(Zakat));
            }
        }

        public string _zakatPaid;
        public string ZakatPaid
        {
            get => _zakatPaid;

            set
            {
                if (_zakatPaid == value) return;

                _zakatPaid = value;
                OnPropertyChanged(nameof( ZakatPaid));
            }
        }

        public string _newTaxAmt;
        public string NewTaxAmt
        {
            get => _newTaxAmt;

            set
            {
                if (_newTaxAmt == value) return;

                _newTaxAmt = value;
                OnPropertyChanged(nameof( NewTaxAmt));
            }
        }


        #endregion

        #region Zakat Estimation

        public string _referenceNumber;
        public string ReferenceNumber
        {
            get => _referenceNumber;

            set
            {
                if (_referenceNumber == value) return;

                _referenceNumber = value;
                OnPropertyChanged(nameof( ReferenceNumber));
            }
        }




        private List<string> _noOfZakatList = new List<string>();
        public List<string> NoOFZakatList
        {
            get
            {
                return _noOfZakatList;
            }
            set
            {
                if (_noOfZakatList == value) return;

                _noOfZakatList = value;
                OnPropertyChanged("NoOFZakatList");
            }
        }

        public bool _isZakatEstListVisible = false;
        public bool IsZakatEstListVisible
        {

            get
            {
                return _isZakatEstListVisible;
            }

            set
            {
                _isZakatEstListVisible = value;
                OnPropertyChanged(nameof( IsZakatEstListVisible));
            }
        }

        /// <summary>
        ///  Cabs List
        /// </summary>

        private List<Result_> _cabsSummaryList;
        public List<Result_> CabsSummary
        {
            get
            {
                return _cabsSummaryList;
            }
            set
            {
                if (_cabsSummaryList == value) return;

                _cabsSummaryList = value;
                OnPropertyChanged("CabsSummary");
            }
        }



        public bool _cabsSummaryIsVisible = false;
        public bool CabsSummaryIsVisible
        {
            get
            {
                return _cabsSummaryIsVisible;
            }

            set
            {
                if (_cabsSummaryIsVisible == value) return;

                _cabsSummaryIsVisible = value;
                OnPropertyChanged(nameof( CabsSummaryIsVisible));
            }
        }

        /// <summary>
        ///  Professionals List
        /// </summary>

        private List<Result_12> _professionalsSummaryList;
        public List<Result_12> ProfessionalsSummary
        {
            get
            {
                return _professionalsSummaryList;
            }
            set
            {
                if (_professionalsSummaryList == value) return;

                _professionalsSummaryList = value;
                OnPropertyChanged("ProfessionalsSummary");
            }
        }

        //  public bool ProfessionalsSummaryIsVisible { get;  set; }

        public bool _professionalsSummaryIsVisible = false;
        public bool ProfessionalsSummaryIsVisible
        {
            get
            {
                return _professionalsSummaryIsVisible;
            }

            set
            {
                if (_professionalsSummaryIsVisible == value) return;

                _professionalsSummaryIsVisible = value;
                OnPropertyChanged(nameof( ProfessionalsSummaryIsVisible));
            }
        }


        /// <summary>
        ///  Sell & Buy List
        /// </summary>

        private List<Result_13> _sell_BuySummaryList;
        public List<Result_13> Sell_BuySummary
        {
            get
            {
                return _sell_BuySummaryList;
            }
            set
            {
                if (_sell_BuySummaryList == value) return;

                _sell_BuySummaryList = value;
                OnPropertyChanged("Sell_BuySummary");
            }
        }

        //public bool Sell_BuySummaryIsVisible { get;  set; }

        public bool _sell_BuySummaryIsVisible = false;
        public bool Sell_BuySummaryIsVisible
        {
            get
            {
                return _sell_BuySummaryIsVisible;
            }
            set
            {
                if (_sell_BuySummaryIsVisible == value) return;

                _sell_BuySummaryIsVisible = value;
                OnPropertyChanged(nameof( Sell_BuySummaryIsVisible));
            }
        }

        /// <summary>
        ///  Labour Occup List
        /// </summary>

        private List<Result_2> _labourOccupSummaryList;
        public List<Result_2> LabourOccupSummary
        {
            get
            {
                return _labourOccupSummaryList;
            }
            set
            {
                if (_labourOccupSummaryList == value) return;

                _labourOccupSummaryList = value;
                OnPropertyChanged("LabourOccupSummary");
            }
        }

        // public bool LabourOccupSummaryIsVisble { get;  set; }

        public bool _LabourOccupSummaryIsVisble = false;
        public bool LabourOccupSummaryIsVisble
        {
            get
            {
                return _LabourOccupSummaryIsVisble;
            }

            set
            {
                if (_LabourOccupSummaryIsVisble == value) return;

                _LabourOccupSummaryIsVisble = value;
                OnPropertyChanged(nameof( LabourOccupSummaryIsVisble));
            }
        }


        /// <summary>
        ///  Industry List
        /// </summary>

        private List<Result_14> _industrySummaryList;
        public List<Result_14> IndustrySummary
        {
            get
            {
                return _industrySummaryList;
            }
            set
            {
                if (_industrySummaryList == value) return;

                _industrySummaryList = value;
                OnPropertyChanged("IndustrySummary");
            }
        }

        // public bool IndustrySummaryIsVisible { get;  set; }

        public bool _IndustrySummaryIsVisible = false;
        public bool IndustrySummaryIsVisible
        {
            get
            {
                return _IndustrySummaryIsVisible;
            }

            set
            {
                if (_IndustrySummaryIsVisible == value) return;

                _IndustrySummaryIsVisible = value;
                OnPropertyChanged(nameof( IndustrySummaryIsVisible));
            }
        }

        /// <summary>
        ///  Contracting CO. List
        /// </summary>

        private List<Result_11> _contractingListSummary;
        public List<Result_11> ContractingSummary
        {
            get
            {
                return _contractingListSummary;
            }
            set
            {
                if (_contractingListSummary == value) return;

                _contractingListSummary = value;
                OnPropertyChanged("ContractingSummary");
            }
        }

        //  public bool ContractingSummaryIsVisible { get;  set; }

        public bool _ContractingSummaryIsVisible = false;
        public bool ContractingSummaryIsVisible
        {
            get
            {
                return _ContractingSummaryIsVisible;
            }
            set
            {
                _ContractingSummaryIsVisible = value;
                OnPropertyChanged(nameof( ContractingSummaryIsVisible));
            }
        }

        /// <summary>
        ///  Invst & Real Estate List
        /// </summary>

        private List<Result_3> _invstRealEstSummaryList;
        public List<Result_3> InvstRealEstSummary
        {
            get
            {
                return _invstRealEstSummaryList;
            }
            set
            {
                if (_invstRealEstSummaryList == value) return;

                _invstRealEstSummaryList = value;
                OnPropertyChanged("InvstRealEstSummary");
            }
        }

        // public bool InvstRealEstSummaryIsVisible { get;  set; }
        public bool _InvstRealEstSummaryIsVisible = false;
        public bool InvstRealEstSummaryIsVisible
        {
            get
            {
                return _IndustrySummaryIsVisible;
            }

            set
            {
                if (_InvstRealEstSummaryIsVisible == value) return;

                _InvstRealEstSummaryIsVisible = value;
                OnPropertyChanged(nameof( InvstRealEstSummaryIsVisible));
            }
        }
        /// <summary>
        ///  Hotels List
        /// </summary>

        private List<Result_7> _hotelsSummaryList;
        public List<Result_7> HotelsSummary
        {
            get
            {
                return _hotelsSummaryList;
            }
            set
            {
                if (_hotelsSummaryList == value) return;

                _hotelsSummaryList = value;
                OnPropertyChanged("HotelsSummary");
            }
        }

        //public bool HotelsSummaryIsVisible { get;  set; }
        public bool _HotelsSummaryIsVisible = false;
        public bool HotelsSummaryIsVisible
        {
            get
            {
                return _HotelsSummaryIsVisible;
            }

            set
            {
                if (_HotelsSummaryIsVisible == value) return;

                _HotelsSummaryIsVisible = value;
                OnPropertyChanged(nameof( HotelsSummaryIsVisible));
            }
        }
        /// <summary>
        ///  Edu. & Health List
        /// </summary>

        private List<Result_4> _edu_HealthSummaryList;
        public List<Result_4> Edu_HealthSummary
        {
            get
            {
                return _edu_HealthSummaryList;
            }
            set
            {
                if (_edu_HealthSummaryList == value) return;

                _edu_HealthSummaryList = value;
                OnPropertyChanged("Edu_HealthSummary");
            }
        }

        // public bool Edu_HealthSummaryIsVisible { get;  set; }
        public bool _Edu_HealthSummaryIsVisible = false;
        public bool Edu_HealthSummaryIsVisible
        {
            get
            {
                return _HotelsSummaryIsVisible;
            }

            set
            {
                if (_Edu_HealthSummaryIsVisible == value) return;

                _Edu_HealthSummaryIsVisible = value;
                OnPropertyChanged(nameof( Edu_HealthSummaryIsVisible));
            }
        }


        /// <summary>
        ///  Poultry and Fish Farms Activities List
        /// </summary>

        private List<Result_5> _poultry_FishFarmSummaryList;
        public List<Result_5> Poultry_FishFarmSummary
        {
            get
            {
                return _poultry_FishFarmSummaryList;
            }
            set
            {
                if (_poultry_FishFarmSummaryList == value) return;

                _poultry_FishFarmSummaryList = value;
                OnPropertyChanged("Poultry_FishFarmSummary");
            }
        }

        //public bool Poultry_FishFarmSummaryIsVisible { get;  set; }

        public bool _Poultry_FishFarmSummaryIsVisible = false;
        public bool Poultry_FishFarmSummaryIsVisible
        {
            get
            {
                return _Poultry_FishFarmSummaryIsVisible;
            }

            set
            {
                if (_Poultry_FishFarmSummaryIsVisible == value) return;

                _Poultry_FishFarmSummaryIsVisible = value;
                OnPropertyChanged(nameof( Poultry_FishFarmSummaryIsVisible));
            }
        }

        /// <summary>
        ///  Cars List
        /// </summary>

        private List<Result_8> _carsSummaryList;
        public List<Result_8> CarsSummary
        {
            get
            {
                return _carsSummaryList;
            }
            set
            {
                if (_carsSummaryList == value) return;

                _carsSummaryList = value;
                OnPropertyChanged("CarsSummary");
            }
        }

        // public bool CarsSummaryIsVisible { get;  set; }

        public bool _CarsSummaryIsVisible = false;
        public bool CarsSummaryIsVisible
        {
            get
            {
                return _CarsSummaryIsVisible;
            }

            set
            {
                if (_CarsSummaryIsVisible == value) return;

                _CarsSummaryIsVisible = value;
                OnPropertyChanged(nameof( CarsSummaryIsVisible));
            }
        }

        /// <summary>
        ///  Minerals List
        /// </summary>

        private List<Result_6> _mineralsSummaryList;
        public List<Result_6> MineralsSummary
        {
            get
            {
                return _mineralsSummaryList;
            }
            set
            {
                if (_mineralsSummaryList == value) return;

                _mineralsSummaryList = value;
                OnPropertyChanged("MineralsSummary");
            }
        }

        // public bool MineralsSummaryIsVisible { get;  set; }

        public bool _MineralsSummaryIsVisible = false;
        public bool MineralsSummaryIsVisible
        {
            get
            {
                return _MineralsSummaryIsVisible;
            }

            set
            {
                if (_MineralsSummaryIsVisible == value) return;

                _MineralsSummaryIsVisible = value;
                OnPropertyChanged(nameof( MineralsSummaryIsVisible));
            }
        }

        #endregion

        #region Acknowledgement 
        private List<Result_9> _AknowledgementList;
        public List<Result_9> AknowledgementList
        {
            get
            {
                return _AknowledgementList;
            }
            set
            {
                if (_AknowledgementList == value) return;

                _AknowledgementList = value;
                OnPropertyChanged("AknowledgementList");
            }
        }

        #endregion

        #region Method

        public void setCurrentTab()
        {
            currentTab = ZakatForm5TabEnum.BasicInformation;
        }

        public async Task LoadZakatForm5Data()
        {
            IsLoading = true;

            try
            {
                ZakatForm5DataResult = null;

                ZakatForm5CityDataResult ZakatForm5CityDataResults = await ZakatForm5WebServiceManager.GAZTZakatForm5CityData();

                if (ZakatForm5CityDataResults != null)
                {

                    var CityDAta = ZakatForm5CityDataResults;
                    ZakatForm5DataResult ZakatForm5DataResult = await ZakatForm5WebServiceManager.GAZTZakatForm5Data(Fbguid);

                    if (ZakatForm5DataResult != null)
                    {

                        //Bind values to UI

                        //Return Detials
                        FinancialYear = ZakatForm5DataResult.PerslText;
                        IsConditionRadio = true;

                        string CalenderType = ZakatForm5DataResult.Incotyp.Substring(0, 1);
                        if (CalenderType.Equals("H"))
                        {
                            ZakatFromDate = ZakatForm5DataResult.AFromDt.ToString("dd-MM-yyyy", new CultureInfo("ar-SA"));
                            string[] dts = ZakatFromDate.Split('-');
                            string date = dts[0] + "-" + UtilityManager.GetMonthNameHijri(dts[1]) + "-" + dts[2];
                            ZakatFromDate = date;
                            ZakatToDate = ZakatForm5DataResult.AToDt.ToString("dd-MM-yyyy", new CultureInfo("ar-SA"));
                            string[] dts2 = ZakatToDate.Split('-');
                            string date2 = dts2[0] + "-" + UtilityManager.GetMonthNameHijri(dts2[1]) + "-" + dts2[2];
                            ZakatToDate = date2;
                            Period = ZakatFromDate + " - " + ZakatToDate;
                        }
                        else
                        {

                            ZakatFromDate = ZakatForm5DataResult.AFromDt.ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                            string[] dts = ZakatFromDate.Split('-');
                            string date = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];
                            ZakatFromDate = date;
                            ZakatToDate = ZakatForm5DataResult.AToDt.ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                            string[] dts2 = ZakatToDate.Split('-');
                            string date2 = dts2[0] + "-" + UtilityManager.GetMonthName(dts2[1]) + "-" + dts2[2];
                            ZakatToDate = date2;
                            Period = ZakatFromDate + " - " + ZakatToDate;
                        }


                        //Taxpayer Detials
                        Taxpayer = ZakatForm5DataResult.ACompNm;
                        Branch = ZakatForm5DataResult.APrctr;
                        string addAddress = ZakatForm5DataResult.Line0 + "," +
                            ZakatForm5DataResult.Line1 + "," +
                            ZakatForm5DataResult.Line2 + "," +
                            ZakatForm5DataResult.Line3 + "," +
                            ZakatForm5DataResult.Line4 + "," +
                            ZakatForm5DataResult.Line5 + "," +
                            ZakatForm5DataResult.Line6 + "," +
                            ZakatForm5DataResult.Line7 + "," +
                            ZakatForm5DataResult.Line8 + "," +
                            ZakatForm5DataResult.Line9;

                        Address = addAddress.ToString();
                        UserEmail = ZakatForm5DataResult.AEmail;
                        MobileNumber = ZakatForm5DataResult.AMobile;

                        //Registration Information
                        NumberOfOutlet = ZakatForm5DataResult.ANoOfOutlet.ToString();

                        if (ZakatForm5DataResult.AResidency == "Resident")
                        {
                            if (App.IsArabic)
                            {
                                Residency_Status = "مقيم";

                            }
                            else
                            {
                                Residency_Status = "Resident";
                            }
                        }
                        else if (ZakatForm5DataResult.AResidency == "مقيم")
                        {
                            if (App.IsArabic)
                            {
                                Residency_Status = "مقيم";

                            }
                            else
                            {
                                Residency_Status = "Resident";
                            }
                        }
                        else
                        {
                            Residency_Status = ZakatForm5DataResult.AResidency;
                        }

                        MainOutlet = ZakatForm5DataResult.AMainact;
                        AccountMethod = ZakatForm5DataResult.AActmethod;
                        FinancialPeriod = ZakatForm5DataResult.AFiscalPeriod;
                        Calendar_Type = ZakatForm5DataResult.AFiscalCalendar;

                        if (ZakatForm5DataResult.Status == "E0001")
                        {
                            IsConditionRadio = false;
                        }
                        else
                        {
                            IsConditionRadio = true;

                        }

                        //Financial Information
                        int ListViewFlag = 0;

                        //Cabs
                        if (ZakatForm5DataResult.SCH_GP01 != null && ZakatForm5DataResult.SCH_GP01.Any())
                        {
                            ListViewFlag++;
                            if (ListViewFlag == 1)
                            {
                                isCabVisible = true;
                                isProfessionalVisible = false;
                                isBuyVisible = false;
                                isLabourOccupancyVisible = false;
                                isIndustryVisible = false;
                                isContractingVisible = false;
                                isRealEstateVisible = false;
                                isHotelVisible = false;
                                isEducationVisible = false;
                                isPoultryVisible = false;
                                isCarVisible = false;
                                isMineralVisible = false;
                                isAdditionalVisible = false;
                            }

                            IsCabBtn = true;


                            NoOFEntityList.Add("Cabs");
                            for (int i = 0; i < ZakatForm5DataResult.SCH_GP01.Count; i++)
                            {

                                ZakatForm5DataResult.SCH_GP01[i].NoOfCars = Math.Round(Convert.ToDecimal(UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP01[i].NoOfCars))).ToString();
                                ZakatForm5DataResult.SCH_GP01[i].AgvDlyIncome = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP01[i].AgvDlyIncome);
                                ZakatForm5DataResult.SCH_GP01[i].OccRate = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP01[i].OccRate);
                                ZakatForm5DataResult.SCH_GP01[i].NetPftPer = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP01[i].NetPftPer);
                                ZakatForm5DataResult.SCH_GP01[i].Revenue = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP01[i].Revenue);
                                ZakatForm5DataResult.SCH_GP01[i].Expenses = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP01[i].Expenses);
                                ZakatForm5DataResult.SCH_GP01[i].NetPft = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP01[i].NetPft);



                                if (ZakatForm5DataResult.SCH_GP01[i].Expenses != "0.00")
                                {
                                    //ZakatForm5DataResult.SCH_GP01.results[i].IsApplicable =AppResources.FORM5Applicable.ToString();
                                    ZakatForm5DataResult.SCH_GP01[i].IsApplicable = AppResources.FORM5Applicable.ToString();
                                    ZakatForm5DataResult.SCH_GP01[i].IsApplicableVisible = true;

                                }
                                else
                                {
                                    // ZakatForm5DataResult.SCH_GP01.results[i].IsApplicable = AppResources.FORM5NotApplicable.ToString();
                                    ZakatForm5DataResult.SCH_GP01[i].IsApplicable = AppResources.FORM5NotApplicable.ToString();
                                    ZakatForm5DataResult.SCH_GP01[i].IsApplicableVisible = false;

                                }
                            }
                            Cabs = ZakatForm5DataResult.SCH_GP01;
                        }
                        //Professionals
                        if (ZakatForm5DataResult.SCH_GP02 != null && ZakatForm5DataResult.SCH_GP02.Any())
                        {
                            ListViewFlag++;
                            if (ListViewFlag == 1)
                            {
                                isCabVisible = false;
                                isProfessionalVisible = true;
                                isBuyVisible = false;
                                isLabourOccupancyVisible = false;
                                isIndustryVisible = false;
                                isContractingVisible = false;
                                isRealEstateVisible = false;
                                isHotelVisible = false;
                                isEducationVisible = false;
                                isPoultryVisible = false;
                                isCarVisible = false;
                                isMineralVisible = false;
                                isAdditionalVisible = false;
                            }

                            IsProfessionBtn = true;

                            NoOFEntityList.Add("Professionals");

                            for (int i = 0; i < ZakatForm5DataResult.SCH_GP02.Count; i++)
                            {

                                ZakatForm5DataResult.SCH_GP02[i].Revenue = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP02[i].Revenue);
                                ZakatForm5DataResult.SCH_GP02[i].Expenses = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP02[i].Expenses);
                                ZakatForm5DataResult.SCH_GP02[i].NetPft = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP02[i].NetPft);



                                if (ZakatForm5DataResult.SCH_GP02[i].Expenses != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP02[i].IsApplicable = AppResources.FORM5Applicable.ToString();
                                    ZakatForm5DataResult.SCH_GP02[i].IsApplicableVisible = true;
                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP02[i].IsApplicable = AppResources.FORM5NotApplicable.ToString();
                                    ZakatForm5DataResult.SCH_GP02[i].IsApplicableVisible = false;

                                }
                            }
                            Professionals = ZakatForm5DataResult.SCH_GP02;
                        }
                        //Sell & Buy
                        if (ZakatForm5DataResult.SCH_GP03 != null && ZakatForm5DataResult.SCH_GP03.Any())
                        {
                            ListViewFlag++;
                            if (ListViewFlag == 1)
                            {
                                isCabVisible = false;
                                isProfessionalVisible = false;
                                isBuyVisible = true;
                                isLabourOccupancyVisible = false;
                                isIndustryVisible = false;
                                isContractingVisible = false;
                                isRealEstateVisible = false;
                                isHotelVisible = false;
                                isEducationVisible = false;
                                isPoultryVisible = false;
                                isCarVisible = false;
                                isMineralVisible = false;
                                isAdditionalVisible = false;
                            }

                            IsSellBtn = true;


                            NoOFEntityList.Add("Sell & Buy");

                            for (int i = 0; i < ZakatForm5DataResult.SCH_GP03.Count; i++)
                            {
                                // ZakatForm5DataResult.SCH_GP03.results[i].IsImportVisible = false;
                                //  ZakatForm5DataResult.SCH_GP03.results[i].IsProcurementVisible = false;

                                ZakatForm5DataResult.SCH_GP03[i].Capital = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP03[i].Capital);
                                ZakatForm5DataResult.SCH_GP03[i].SuppCon = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP03[i].SuppCon);
                                ZakatForm5DataResult.SCH_GP03[i].ProfitRatio = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP03[i].ProfitRatio);
                                ZakatForm5DataResult.SCH_GP03[i].Sales = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP03[i].Sales);
                                ZakatForm5DataResult.SCH_GP03[i].SalePrfRatio = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP03[i].SalePrfRatio);

                                ZakatForm5DataResult.SCH_GP03[i].GenTradeI = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP03[i].GenTradeI);
                                ZakatForm5DataResult.SCH_GP03[i].Livelihoods = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP03[i].Livelihoods);
                                ZakatForm5DataResult.SCH_GP03[i].LiveStkAnimals = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP03[i].LiveStkAnimals);
                                ZakatForm5DataResult.SCH_GP03[i].ExtnProc = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP03[i].ExtnProc);

                                ZakatForm5DataResult.SCH_GP03[i].GenTradeI = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP03[i].GenTradeI);
                                ZakatForm5DataResult.SCH_GP03[i].LivelihoodsI = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP03[i].LivelihoodsI);
                                ZakatForm5DataResult.SCH_GP03[i].LiveStkAnimals = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP03[i].LiveStkAnimals);
                                ZakatForm5DataResult.SCH_GP03[i].IntnProc = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP03[i].IntnProc);


                                if (ZakatForm5DataResult.SCH_GP03[i].SuppCon != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP03[i].IsApplicable = AppResources.FORM5Applicable.ToString();
                                    ZakatForm5DataResult.SCH_GP03[i].IsApplicableVisible = true;

                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP03[i].IsApplicable = AppResources.FORM5NotApplicable.ToString();
                                    ZakatForm5DataResult.SCH_GP03[i].IsApplicableVisible = false;

                                }

                               


                                if (ZakatForm5DataResult.SCH_GP03[i].GenTradeI != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP03[i].IsImportVisible = true;


                                    ZakatForm5DataResult.SCH_GP03[i].IsImport = AppResources.ZYes.ToString();

                                }
                                else if (ZakatForm5DataResult.SCH_GP03[i].Livelihoods != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP03[i].IsImportVisible = true;

                                    ZakatForm5DataResult.SCH_GP03[i].IsImport = AppResources.ZYes.ToString();
                                }
                                else if (ZakatForm5DataResult.SCH_GP03[i].LiveStkAnimals != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP03[i].IsImportVisible = true;

                                    ZakatForm5DataResult.SCH_GP03[i].IsImport = AppResources.ZYes.ToString();
                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP03[i].IsImportVisible = false;
                                    ZakatForm5DataResult.SCH_GP03[i].IsImport = AppResources.ZNo.ToString();
                                }


                                if (ZakatForm5DataResult.SCH_GP03[i].GenTradeI != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP03[i].Imp_IsGeneralApplicable = AppResources.FORM5Applicable.ToString();
                                    ZakatForm5DataResult.SCH_GP03[i].Imp_IsGeneralApplicableVisible = true;

                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP03[i].Imp_IsGeneralApplicable = AppResources.FORM5NotApplicable.ToString();
                                    ZakatForm5DataResult.SCH_GP03[i].Imp_IsGeneralApplicableVisible = false;

                                }

                                if (ZakatForm5DataResult.SCH_GP03[i].Livelihoods != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP03[i].Imp_IsLiveLihoodsApplicable = AppResources.FORM5Applicable.ToString();
                                    ZakatForm5DataResult.SCH_GP03[i].Imp_IsLiveLihoodsApplicableVisible = true;

                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP03[i].Imp_IsLiveLihoodsApplicable = AppResources.FORM5NotApplicable.ToString();
                                    ZakatForm5DataResult.SCH_GP03[i].Imp_IsLiveLihoodsApplicableVisible = false;

                                }

                                if (ZakatForm5DataResult.SCH_GP03[i].LiveStkAnimals != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP03[i].Imp_IsLivestockApplicable = AppResources.FORM5Applicable.ToString();
                                    ZakatForm5DataResult.SCH_GP03[i].Imp_IsLivestockApplicableVisible = true;

                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP03[i].Imp_IsLivestockApplicable = AppResources.FORM5NotApplicable.ToString();
                                    ZakatForm5DataResult.SCH_GP03[i].Imp_IsLivestockApplicableVisible = false;

                                }

                                

                                if (ZakatForm5DataResult.SCH_GP03[i].GenTradeI != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP03[i].IsProcurement = AppResources.ZYes.ToString();
                                    ZakatForm5DataResult.SCH_GP03[i].IsProcurementVisible = true;
                                }
                                else if (ZakatForm5DataResult.SCH_GP03[i].LivelihoodsI != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP03[i].IsProcurement = AppResources.ZYes.ToString();
                                    ZakatForm5DataResult.SCH_GP03[i].IsProcurementVisible = true;
                                }
                                else if (ZakatForm5DataResult.SCH_GP03[i].LiveStkAnimalsI != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP03[i].IsProcurement = AppResources.ZYes.ToString();
                                    ZakatForm5DataResult.SCH_GP03[i].IsProcurementVisible = true;
                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP03[i].IsProcurement = AppResources.ZNo.ToString();
                                    ZakatForm5DataResult.SCH_GP03[i].IsProcurementVisible = false;
                                }





                                if (ZakatForm5DataResult.SCH_GP03[i].GenTradeI != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP03[i].Pro_IsGeneralApplicable = AppResources.FORM5Applicable.ToString();
                                    ZakatForm5DataResult.SCH_GP03[i].Pro_IsGeneralApplicableVisible = true;

                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP03[i].Pro_IsGeneralApplicable = AppResources.FORM5NotApplicable.ToString();
                                    ZakatForm5DataResult.SCH_GP03[i].Pro_IsGeneralApplicableVisible = false;

                                }
                                if (ZakatForm5DataResult.SCH_GP03[i].LivelihoodsI != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP03[i].Pro_IsLiveLihoodsApplicable = AppResources.FORM5Applicable.ToString();
                                    ZakatForm5DataResult.SCH_GP03[i].Pro_IsLiveLihoodsApplicableVisible = true;

                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP03[i].Pro_IsLiveLihoodsApplicable = AppResources.FORM5NotApplicable.ToString();
                                    ZakatForm5DataResult.SCH_GP03[i].Pro_IsLiveLihoodsApplicableVisible = false;

                                }
                                if (ZakatForm5DataResult.SCH_GP03[i].LiveStkAnimalsI != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP03[i].Pro_IsLivestockApplicable = AppResources.FORM5Applicable.ToString();
                                    ZakatForm5DataResult.SCH_GP03[i].Pro_IsLivestockApplicableVisible = true;

                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP03[i].Pro_IsLivestockApplicable = AppResources.FORM5NotApplicable.ToString();
                                    ZakatForm5DataResult.SCH_GP03[i].Pro_IsLivestockApplicableVisible = false;

                                }



                                if (ZakatForm5DataResult.SCH_GP03[i].SuppCon != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP03[i].IsApplicable = AppResources.FORM5Applicable.ToString();
                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP03[i].IsApplicable = AppResources.FORM5NotApplicable.ToString();
                                }


                            }


                            Sell_Buy = ZakatForm5DataResult.SCH_GP03;
                        }
                        //Labour Occup.
                        if (ZakatForm5DataResult.SCH_GP04 != null && ZakatForm5DataResult.SCH_GP04.Any())
                        {

                            ListViewFlag++;
                            if (ListViewFlag == 1)
                            {
                                isCabVisible = false;
                                isProfessionalVisible = false;
                                isBuyVisible = false;
                                isLabourOccupancyVisible = true;
                                isIndustryVisible = false;
                                isContractingVisible = false;
                                isRealEstateVisible = false;
                                isHotelVisible = false;
                                isEducationVisible = false;
                                isPoultryVisible = false;
                                isCarVisible = false;
                                isMineralVisible = false;
                                isAdditionalVisible = false;
                            }

                            IsLabourBtn = true;

                            NoOFEntityList.Add("Labour Occup.");

                            for (int i = 0; i < ZakatForm5DataResult.SCH_GP04.Count; i++)
                            {
                                ZakatForm5DataResult.SCH_GP04[i].NoOfLabour = Math.Round(Convert.ToDecimal(UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP04[i].NoOfLabour))).ToString();
                                ZakatForm5DataResult.SCH_GP04[i].Revenue = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP04[i].Revenue);
                                ZakatForm5DataResult.SCH_GP04[i].Expenses = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP04[i].Expenses);
                                ZakatForm5DataResult.SCH_GP04[i].NetProfit = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP04[i].NetProfit);


                                if (ZakatForm5DataResult.SCH_GP04[i].Expenses != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP04[i].IsExpensesApplicable = AppResources.FORM5Applicable.ToString();
                                    ZakatForm5DataResult.SCH_GP04[i].IsApplicableVisible = true;

                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP04[i].IsExpensesApplicable = AppResources.FORM5NotApplicable.ToString();
                                    ZakatForm5DataResult.SCH_GP04[i].IsApplicableVisible = false;

                                }
                            }

                            LabourOccup = ZakatForm5DataResult.SCH_GP04;
                        }
                        //Industry
                        if (ZakatForm5DataResult.SCH_GP05 != null && ZakatForm5DataResult.SCH_GP05.Any())
                        {
                            ListViewFlag++;
                            if (ListViewFlag == 1)
                            {
                                isCabVisible = false;
                                isProfessionalVisible = false;
                                isBuyVisible = false;
                                isLabourOccupancyVisible = false;
                                isIndustryVisible = true;
                                isContractingVisible = false;
                                isRealEstateVisible = false;
                                isHotelVisible = false;
                                isEducationVisible = false;
                                isPoultryVisible = false;
                                isCarVisible = false;
                                isMineralVisible = false;
                                isAdditionalVisible = false;
                            }

                            IsIndustryBtn = true;

                            for (int i = 0; i < ZakatForm5DataResult.SCH_GP05.Count; i++)
                            {
                                ZakatForm5DataResult.SCH_GP05[i].FundingTot = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP05[i].FundingTot);
                                ZakatForm5DataResult.SCH_GP05[i].CapitalStr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP05[i].CapitalStr);
                                ZakatForm5DataResult.SCH_GP05[i].Profit = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP05[i].Profit);
                                ZakatForm5DataResult.SCH_GP05[i].Sales = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP05[i].Sales);

                            }

                            NoOFEntityList.Add("Industry");
                            Industry = ZakatForm5DataResult.SCH_GP05;
                        }
                        // Contracting CO.
                        if (ZakatForm5DataResult.SCH_GP06 != null && ZakatForm5DataResult.SCH_GP06.Any())
                        {
                            ListViewFlag++;
                            if (ListViewFlag == 1)
                            {
                                isCabVisible = false;
                                isProfessionalVisible = false;
                                isBuyVisible = false;
                                isLabourOccupancyVisible = false;
                                isIndustryVisible = false;
                                isContractingVisible = true;
                                isRealEstateVisible = false;
                                isHotelVisible = false;
                                isEducationVisible = false;
                                isPoultryVisible = false;
                                isCarVisible = false;
                                isMineralVisible = false;
                                isAdditionalVisible = false;
                            }

                            IsContractBtn = true;
                            NoOFEntityList.Add("Contracting CO.");

                            for (int i = 0; i < ZakatForm5DataResult.SCH_GP06.Count; i++)
                            {
                                ZakatForm5DataResult.SCH_GP06[i].GovtContractProf = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP06[i].GovtContractProf);
                                ZakatForm5DataResult.SCH_GP06[i].CivilContrRev = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP06[i].CivilContrRev);
                                ZakatForm5DataResult.SCH_GP06[i].OtherIncome = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP06[i].OtherIncome);
                                ZakatForm5DataResult.SCH_GP06[i].TotalProfit = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP06[i].TotalProfit);
                                ZakatForm5DataResult.SCH_GP06[i].Capital = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP06[i].Capital);
                                ZakatForm5DataResult.SCH_GP06[i].NoOfLabours = Math.Round(Convert.ToDecimal(UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP06[i].NoOfLabours))).ToString();


                                if (ZakatForm5DataResult.SCH_GP06[i].GovtContractProf != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP06[i].IsGovenmentApplicable = AppResources.FORM5Applicable.ToString();
                                    ZakatForm5DataResult.SCH_GP06[i].IsGovenmentApplicableVisible = true;

                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP06[i].IsGovenmentApplicable = AppResources.FORM5NotApplicable.ToString();
                                    ZakatForm5DataResult.SCH_GP06[i].IsGovenmentApplicableVisible = false;

                                }

                                if (ZakatForm5DataResult.SCH_GP06[i].CivilContrRev != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP06[i].IsCivilProfitApplicable = AppResources.FORM5Applicable.ToString();
                                    ZakatForm5DataResult.SCH_GP06[i].IsCivilProfitApplicableVisible = true;

                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP06[i].IsCivilProfitApplicable = AppResources.FORM5NotApplicable.ToString();
                                    ZakatForm5DataResult.SCH_GP06[i].IsCivilProfitApplicableVisible = false;

                                }

                                if (ZakatForm5DataResult.SCH_GP06[i].OtherIncome != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP06[i].IsOtherProfitApplicable = AppResources.FORM5Applicable.ToString();
                                    ZakatForm5DataResult.SCH_GP06[i].IsOtherProfitApplicableVisible = true;

                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP06[i].IsOtherProfitApplicable = AppResources.FORM5NotApplicable.ToString();
                                    ZakatForm5DataResult.SCH_GP06[i].IsOtherProfitApplicableVisible = false;

                                }
                            }


                            Contracting = ZakatForm5DataResult.SCH_GP06;
                        }
                        //Invst & Real Estate
                        if (ZakatForm5DataResult.SCH_GP07 != null && ZakatForm5DataResult.SCH_GP07.Any())
                        {
                            ListViewFlag++;
                            if (ListViewFlag == 1)
                            {
                                isCabVisible = false;
                                isProfessionalVisible = false;
                                isBuyVisible = false;
                                isLabourOccupancyVisible = false;
                                isIndustryVisible = false;
                                isContractingVisible = false;
                                isRealEstateVisible = true;
                                isHotelVisible = false;
                                isEducationVisible = false;
                                isPoultryVisible = false;
                                isCarVisible = false;
                                isMineralVisible = false;
                                isAdditionalVisible = false;
                            }

                            NoOFEntityList.Add("Invst & Real Estate");
                            IsInvestBtn = true;


                            for (int i = 0; i < ZakatForm5DataResult.SCH_GP07.Count; i++)
                            {

                                ZakatForm5DataResult.SCH_GP07[i].Revenue = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP07[i].Revenue);
                                ZakatForm5DataResult.SCH_GP07[i].Expenses = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP07[i].Expenses);
                                ZakatForm5DataResult.SCH_GP07[i].NetPft = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP07[i].NetPft);

                                if (ZakatForm5DataResult.SCH_GP07[i].City != "")
                                {
                                    for (int j = 0; j < ZakatForm5CityDataResults.zcitySet.Count(); j++)
                                    {
                                        if (ZakatForm5DataResult.SCH_GP07[i].City == ZakatForm5CityDataResults.zcitySet[j].CityCode)
                                        {
                                            ZakatForm5DataResult.SCH_GP07[i].City = ZakatForm5CityDataResults.zcitySet[j].CityName;
                                        }
                                    }
                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP07[i].City = "-";
                                }


                                if (ZakatForm5DataResult.SCH_GP07[i].Expenses != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP07[i].IsExpencesApplicable = AppResources.FORM5Applicable.ToString();
                                    ZakatForm5DataResult.SCH_GP07[i].IsApplicableVisible = true;

                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP07[i].IsExpencesApplicable = AppResources.FORM5NotApplicable.ToString();
                                    ZakatForm5DataResult.SCH_GP07[i].IsApplicableVisible = false;

                                }
                            }



                            InvstRealEst = ZakatForm5DataResult.SCH_GP07;
                        }
                        //Hotels
                        if (ZakatForm5DataResult.SCH_GP08 != null && ZakatForm5DataResult.SCH_GP08.Any())
                        {
                            ListViewFlag++;

                            if (ListViewFlag == 1)
                            {
                                isCabVisible = false;
                                isProfessionalVisible = false;
                                isBuyVisible = false;
                                isLabourOccupancyVisible = false;
                                isIndustryVisible = false;
                                isContractingVisible = false;
                                isRealEstateVisible = false;
                                isHotelVisible = true;
                                isEducationVisible = false;
                                isPoultryVisible = false;
                                isCarVisible = false;
                                isMineralVisible = false;
                                isAdditionalVisible = false;
                            }

                            NoOFEntityList.Add("Hotels");
                            IsHotelsBtn = true;

                            for (int i = 0; i < ZakatForm5DataResult.SCH_GP08.Count; i++)
                            {
                                ZakatForm5DataResult.SCH_GP08[i].NoOfRooms = Math.Round(Convert.ToDecimal(UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP08[i].NoOfRooms))).ToString();
                                ZakatForm5DataResult.SCH_GP08[i].RoomRate = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP08[i].RoomRate);
                                ZakatForm5DataResult.SCH_GP08[i].AddRoomRate = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP08[i].AddRoomRate);
                                ZakatForm5DataResult.SCH_GP08[i].ServicFee = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP08[i].ServicFee);
                                ZakatForm5DataResult.SCH_GP08[i].OccRate = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP08[i].OccRate);
                                ZakatForm5DataResult.SCH_GP08[i].Revenue = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP08[i].Revenue);
                                ZakatForm5DataResult.SCH_GP08[i].Expenses = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP08[i].Expenses);
                                ZakatForm5DataResult.SCH_GP08[i].NetProfit = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP08[i].NetProfit);


                                if (ZakatForm5DataResult.SCH_GP08[i].City != "")
                                {
                                    for (int j = 0; j < ZakatForm5CityDataResults.zcitySet.Count(); j++)
                                    {
                                        if (ZakatForm5DataResult.SCH_GP08[i].City == ZakatForm5CityDataResults.zcitySet[j].CityCode)
                                        {
                                            ZakatForm5DataResult.SCH_GP08[i].City = ZakatForm5CityDataResults.zcitySet[j].CityName;
                                        }
                                    }
                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP08[i].City = "-";
                                }

                                if (ZakatForm5DataResult.SCH_GP08[i].Owned == "1")
                                {
                                    ZakatForm5DataResult.SCH_GP08[i].IsOwned = AppResources.ZYes.ToString();
                                }
                                //else if(ZakatForm5DataResult.SCH_GP08.results[i].Owned == "2")
                                //{
                                //    ZakatForm5DataResult.SCH_GP08.results[i].IsOwned = AppResources.ZNo.ToString();
                                //}
                                else
                                {
                                    // ZakatForm5DataResult.SCH_GP08.results[i].IsOwned = "";
                                    ZakatForm5DataResult.SCH_GP08[i].IsOwned = AppResources.ZNo.ToString();
                                }


                                if (ZakatForm5DataResult.SCH_GP08[i].Expenses != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP08[i].IsExpApplicable = AppResources.FORM5Applicable.ToString();
                                    ZakatForm5DataResult.SCH_GP08[i].IsApplicableVisible = true;

                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP08[i].IsExpApplicable = AppResources.FORM5NotApplicable.ToString();
                                    ZakatForm5DataResult.SCH_GP08[i].IsApplicableVisible = false;

                                }
                            }

                            Hotels = ZakatForm5DataResult.SCH_GP08;
                        }
                        //Edu. & Health
                        if (ZakatForm5DataResult.SCH_GP09 != null && ZakatForm5DataResult.SCH_GP09.Any())
                        {
                            ListViewFlag++;
                            if (ListViewFlag == 1)
                            {
                                isCabVisible = false;
                                isProfessionalVisible = false;
                                isBuyVisible = false;
                                isLabourOccupancyVisible = false;
                                isIndustryVisible = false;
                                isContractingVisible = false;
                                isRealEstateVisible = false;
                                isHotelVisible = false;
                                isEducationVisible = true;
                                isPoultryVisible = false;
                                isCarVisible = false;
                                isMineralVisible = false;
                                isAdditionalVisible = false;
                            }

                            NoOFEntityList.Add("Edu. & Health");
                            IsEduBtn = true;
                            for (int i = 0; i < ZakatForm5DataResult.SCH_GP09.Count; i++)
                            {
                                ZakatForm5DataResult.SCH_GP09[i].SubsidyVal = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP09[i].SubsidyVal);
                                ZakatForm5DataResult.SCH_GP09[i].Revenue = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP09[i].Revenue);
                                ZakatForm5DataResult.SCH_GP09[i].Expenses = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP09[i].Expenses);
                                ZakatForm5DataResult.SCH_GP09[i].NetPft = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP09[i].NetPft);



                                if (ZakatForm5DataResult.SCH_GP09[i].Expenses != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP09[i].IsExpApplicable = AppResources.FORM5Applicable.ToString();
                                    ZakatForm5DataResult.SCH_GP09[i].IsApplicableVisible = true;

                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP09[i].IsExpApplicable = AppResources.FORM5NotApplicable.ToString();
                                    ZakatForm5DataResult.SCH_GP09[i].IsApplicableVisible = false;

                                }
                            }

                            Edu_Health = ZakatForm5DataResult.SCH_GP09;
                        }
                        //Poultry and Fish Farms Activities
                        if (ZakatForm5DataResult.SCH_GP10 != null && ZakatForm5DataResult.SCH_GP10.Any())
                        {
                            ListViewFlag++;

                            if (ListViewFlag == 1)
                            {
                                isCabVisible = false;
                                isProfessionalVisible = false;
                                isBuyVisible = false;
                                isLabourOccupancyVisible = false;
                                isIndustryVisible = false;
                                isContractingVisible = false;
                                isRealEstateVisible = false;
                                isHotelVisible = false;
                                isEducationVisible = false;
                                isPoultryVisible = true;
                                isCarVisible = false;
                                isMineralVisible = false;
                                isAdditionalVisible = false;
                            }


                            NoOFEntityList.Add("Poultry and Fish Farms Activities");
                            IsPoultryBtn = true;

                            for (int i = 0; i < ZakatForm5DataResult.SCH_GP10.Count; i++)
                            {

                                ZakatForm5DataResult.SCH_GP10[i].Capital = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP10[i].Capital);
                                ZakatForm5DataResult.SCH_GP10[i].IncrInCap = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP10[i].IncrInCap);
                                ZakatForm5DataResult.SCH_GP10[i].CapitalRate = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP10[i].CapitalRate);
                                ZakatForm5DataResult.SCH_GP10[i].Profits = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP10[i].Profits);



                                if (ZakatForm5DataResult.SCH_GP10[i].IncrInCap != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP10[i].IsIncCapApplicable = AppResources.FORM5Applicable.ToString();
                                    ZakatForm5DataResult.SCH_GP10[i].IsApplicableVisible = true;

                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP10[i].IsIncCapApplicable = AppResources.FORM5NotApplicable.ToString();
                                    ZakatForm5DataResult.SCH_GP10[i].IsApplicableVisible = false;

                                }
                            }

                            Poultry_FishFarm = ZakatForm5DataResult.SCH_GP10;
                        }
                        //Cars
                        if (ZakatForm5DataResult.SCH_GP11 != null && ZakatForm5DataResult.SCH_GP11.Any())
                        {
                            ListViewFlag++;

                            if (ListViewFlag == 1)
                            {
                                isCabVisible = false;
                                isProfessionalVisible = false;
                                isBuyVisible = false;
                                isLabourOccupancyVisible = false;
                                isIndustryVisible = false;
                                isContractingVisible = false;
                                isRealEstateVisible = false;
                                isHotelVisible = false;
                                isEducationVisible = false;
                                isPoultryVisible = false;
                                isCarVisible = true;
                                isMineralVisible = false;
                                isAdditionalVisible = false;
                            }

                            for (int i = 0; i < ZakatForm5DataResult.SCH_GP11.Count; i++)
                            {

                                ZakatForm5DataResult.SCH_GP11[i].ShareCap = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP11[i].ShareCap);
                                ZakatForm5DataResult.SCH_GP11[i].IntrPurchase = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP11[i].IntrPurchase);
                                ZakatForm5DataResult.SCH_GP11[i].ForgPurchase = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP11[i].ForgPurchase);
                                ZakatForm5DataResult.SCH_GP11[i].Sales = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP11[i].Sales);
                                ZakatForm5DataResult.SCH_GP11[i].PerSalesGain = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP11[i].PerSalesGain);

                            }

                            IsCarBtn = true;

                            NoOFEntityList.Add("Cars");
                            Cars = ZakatForm5DataResult.SCH_GP11;
                        }
                        //Minerals
                        if (ZakatForm5DataResult.SCH_GP12 != null && ZakatForm5DataResult.SCH_GP12.Any())
                        {
                            ListViewFlag++;

                            if (ListViewFlag == 1)
                            {
                                isCabVisible = false;
                                isProfessionalVisible = false;
                                isBuyVisible = false;
                                isLabourOccupancyVisible = false;
                                isIndustryVisible = false;
                                isContractingVisible = false;
                                isRealEstateVisible = false;
                                isHotelVisible = false;
                                isEducationVisible = false;
                                isPoultryVisible = false;
                                isCarVisible = false;
                                isMineralVisible = true;
                                isAdditionalVisible = false;
                            }

                            for (int i = 0; i < ZakatForm5DataResult.SCH_GP12.Count; i++)
                            {


                                ZakatForm5DataResult.SCH_GP12[i].ShareCap = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP12[i].ShareCap);
                                ZakatForm5DataResult.SCH_GP12[i].IntrPurchase = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP12[i].IntrPurchase);
                                ZakatForm5DataResult.SCH_GP12[i].ForgPurchase = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP12[i].ForgPurchase);
                                ZakatForm5DataResult.SCH_GP12[i].Sales = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP12[i].Sales);
                                ZakatForm5DataResult.SCH_GP12[i].PerSalesGain = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP12[i].PerSalesGain);

                            }

                            IsMineralsBtn = true;
                            NoOFEntityList.Add("Minerals");
                            Minerals = ZakatForm5DataResult.SCH_GP12;
                        }
                        //   var AdditionalInfo = ZakatForm5DataResult..Results.ToList();

                        //Additional Information
                        NoOFEntityList.Add("Additional Information");
                        ListViewFlag++;

                        if (ListViewFlag == 1)
                        {
                            isCabVisible = false;
                            isProfessionalVisible = false;
                            isBuyVisible = false;
                            isLabourOccupancyVisible = false;
                            isIndustryVisible = false;
                            isContractingVisible = false;
                            isRealEstateVisible = false;
                            isHotelVisible = false;
                            isEducationVisible = false;
                            isPoultryVisible = false;
                            isCarVisible = false;
                            isMineralVisible = false;
                            isAdditionalVisible = true;
                        }
                        //Share in Persons Companies
                        IsAddBtn = true;

                        var OtherCompanyCheck = ZakatForm5DataResult.APlShareChk;

                        if (ZakatForm5DataResult.APlShareChk == "1")
                        {
                            ZakatForm5DataResult.IsOtherShareApplicable = AppResources.FORM5Applicable.ToString();
                            ZakatForm5DataResult.IsOtherShareApplicableVisible = true;

                        }
                        else
                        {
                            ZakatForm5DataResult.IsOtherShareApplicable = AppResources.FORM5NotApplicable.ToString();
                            ZakatForm5DataResult.IsOtherShareApplicableVisible = false;

                        }

                        OtherCompanyShare = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.APlShare);
                        IsOtherComShareApp = ZakatForm5DataResult.IsOtherShareApplicable;
                        IsOtherComShareAppVisible = ZakatForm5DataResult.IsOtherShareApplicableVisible;
                        ZakatBase = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.ACapital);



                        //Declaration
                        NoOfBranch = Math.Round(Convert.ToDecimal(UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.ANoBranches))).ToString();
                        NoofEmp = Math.Round(Convert.ToDecimal(UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.ANoEmp))).ToString();
                        YearRent = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.AAnnualRent);
                        TotalAnnualSalary = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.ATotAnnualSal);

                        //Zakat Details

                        Zakatable = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.AZakat);
                        Zakat = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.AZakat51);
                        ZakatPaid = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.AReleaseOfContract);
                        NewTaxAmt = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.ANetTaxableAmount);


                        // Zakat Estimation API Call


                        ZakatForm5SummaryResult ZakatForm5SummaryDataResult = await ZakatForm5WebServiceManager.GAZTZakatForm5DataSummary(ZakatForm5DataResult.Fbnum);

                        if (ZakatForm5SummaryDataResult != null)
                        {

                            //IsZakatEstListVisible = true;
                            int IsZakatFlag = 0;
                            // information
                            ReferenceNumber = ZakatForm5DataResult.Fbnum.ToString();
                            

                            AknowledgementList = ZakatForm5SummaryDataResult.SadadSet;

                            if (ZakatForm5SummaryDataResult.SchGP01Set.Any())
                            {
                                //Cabs GP1

                                CabsSummary = ZakatForm5SummaryDataResult.SchGP01Set;
                                CabsSummaryIsVisible = true;
                                NoOFZakatList.Add("Cabs");
                                IsZakatFlag++;

                                ZakatForm5SummaryDataResult.SchGP01Set[0].ActivityProfitTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP01Set[0].ActivityProfitTp);
                                ZakatForm5SummaryDataResult.SchGP01Set[0].ActivityProfitRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP01Set[0].ActivityProfitRr);

                                ZakatForm5SummaryDataResult.SchGP01Set[0].ZakatBaseTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP01Set[0].ZakatBaseTp);
                                ZakatForm5SummaryDataResult.SchGP01Set[0].ZakatBaseRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP01Set[0].ZakatBaseRr);



                            }

                            if (ZakatForm5SummaryDataResult.SchGP02Set.Any())
                            {
                                //Professionals
                                ProfessionalsSummary = ZakatForm5SummaryDataResult.SchGP02Set;
                                ProfessionalsSummaryIsVisible = true;

                                NoOFZakatList.Add("Professionals");
                                IsZakatFlag++;

                                ZakatForm5SummaryDataResult.SchGP02Set[0].ActivityProfitTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP02Set[0].ActivityProfitTp);
                                ZakatForm5SummaryDataResult.SchGP02Set[0].ActivityProfitRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP02Set[0].ActivityProfitRr);

                                ZakatForm5SummaryDataResult.SchGP02Set[0].ZakatBaseTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP02Set[0].ZakatBaseTp);
                                ZakatForm5SummaryDataResult.SchGP02Set[0].ZakatBaseRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP02Set[0].ZakatBaseRr);

                            }
                            if (ZakatForm5SummaryDataResult.SchGP03Set.Any())
                            {
                                //Sell & Buy
                                Sell_BuySummary = ZakatForm5SummaryDataResult.SchGP03Set;
                                Sell_BuySummaryIsVisible = true;
                                NoOFZakatList.Add("SellesAndBuy");
                                IsZakatFlag++;

                                ZakatForm5SummaryDataResult.SchGP03Set[0].ActivityCapitalTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP03Set[0].ActivityCapitalTp);
                                ZakatForm5SummaryDataResult.SchGP03Set[0].ActivityCapitalRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP03Set[0].ActivityCapitalRr);

                                ZakatForm5SummaryDataResult.SchGP03Set[0].ExternalImportTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP03Set[0].ExternalImportTp);
                                ZakatForm5SummaryDataResult.SchGP03Set[0].ExternalImportRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP03Set[0].ExternalImportRr);

                                ZakatForm5SummaryDataResult.SchGP03Set[0].ActivityProfitTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP03Set[0].ActivityProfitTp);
                                ZakatForm5SummaryDataResult.SchGP03Set[0].ActivityProfitRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP03Set[0].ActivityProfitRr);

                                ZakatForm5SummaryDataResult.SchGP03Set[0].ZakatBaseTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP03Set[0].ZakatBaseTp);
                                ZakatForm5SummaryDataResult.SchGP03Set[0].ZakatBaseRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP03Set[0].ZakatBaseRr);


                            }
                            if (ZakatForm5SummaryDataResult.SchGP04Set.Any())
                            {
                                //Labour Occup.
                                LabourOccupSummary = ZakatForm5SummaryDataResult.SchGP04Set;
                                LabourOccupSummaryIsVisble = true;
                                NoOFZakatList.Add("Labour");
                                IsZakatFlag++;

                                ZakatForm5SummaryDataResult.SchGP04Set[0].NoLaboursTp = Math.Round(Convert.ToDecimal(UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP04Set[0].NoLaboursTp))).ToString();
                                ZakatForm5SummaryDataResult.SchGP04Set[0].NoLaboursRr = Math.Round(Convert.ToDecimal(UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP04Set[0].NoLaboursRr))).ToString();

                                ZakatForm5SummaryDataResult.SchGP04Set[0].ActivityProfitTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP04Set[0].ActivityProfitTp);
                                ZakatForm5SummaryDataResult.SchGP04Set[0].ActivityProfitRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP04Set[0].ActivityProfitRr);

                                ZakatForm5SummaryDataResult.SchGP04Set[0].ZakatBaseTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP04Set[0].ZakatBaseTp);
                                ZakatForm5SummaryDataResult.SchGP04Set[0].ZakatBaseRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP04Set[0].ZakatBaseRr);

                            }
                            if (ZakatForm5SummaryDataResult.SchGP05Set.Any())
                            {
                                //Industry
                                IndustrySummary = ZakatForm5SummaryDataResult.SchGP05Set;
                                IndustrySummaryIsVisible = true;
                                NoOFZakatList.Add("Industry");
                                IsZakatFlag++;


                                ZakatForm5SummaryDataResult.SchGP05Set[0].ActivityProfitTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP05Set[0].ActivityProfitTp);
                                ZakatForm5SummaryDataResult.SchGP05Set[0].ActivityProfitRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP05Set[0].ActivityProfitRr);

                                ZakatForm5SummaryDataResult.SchGP05Set[0].ZakatBaseTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP05Set[0].ZakatBaseTp);
                                ZakatForm5SummaryDataResult.SchGP05Set[0].ZakatBaseRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP05Set[0].ZakatBaseRr);

                            }
                            if (ZakatForm5SummaryDataResult.SchGP06Set.Any())
                            {
                                //Contracting CO.
                                ContractingSummary = ZakatForm5SummaryDataResult.SchGP06Set;
                                ContractingSummaryIsVisible = true;
                                NoOFZakatList.Add("Contracting");
                                IsZakatFlag++;

                                ZakatForm5SummaryDataResult.SchGP06Set[0].GovtContractProfTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP06Set[0].GovtContractProfTp);
                                ZakatForm5SummaryDataResult.SchGP06Set[0].GovtContractProfRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP06Set[0].GovtContractProfRr);

                                ZakatForm5SummaryDataResult.SchGP06Set[0].CivilContrRevTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP06Set[0].CivilContrRevTp);
                                ZakatForm5SummaryDataResult.SchGP06Set[0].CivilContrRevRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP06Set[0].CivilContrRevRr);

                                ZakatForm5SummaryDataResult.SchGP06Set[0].NoLaboursTp = Math.Round(Convert.ToDecimal(UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP06Set[0].NoLaboursTp))).ToString();
                                ZakatForm5SummaryDataResult.SchGP06Set[0].NoLaboursRr = Math.Round(Convert.ToDecimal(UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP06Set[0].NoLaboursRr))).ToString();

                                ZakatForm5SummaryDataResult.SchGP06Set[0].ActivityProfitTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP06Set[0].ActivityProfitTp);
                                ZakatForm5SummaryDataResult.SchGP06Set[0].ActivityProfitRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP06Set[0].ActivityProfitRr);

                                ZakatForm5SummaryDataResult.SchGP06Set[0].ZakatBaseTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP06Set[0].ZakatBaseTp);
                                ZakatForm5SummaryDataResult.SchGP06Set[0].ZakatBaseRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP06Set[0].ZakatBaseRr);

                            }
                            if (ZakatForm5SummaryDataResult.SchGP07Set.Any())
                            {
                                //Invst & Real Estate
                                InvstRealEstSummary = ZakatForm5SummaryDataResult.SchGP07Set;
                                InvstRealEstSummaryIsVisible = true;
                                NoOFZakatList.Add("Invest");
                                IsZakatFlag++;

                                ZakatForm5SummaryDataResult.SchGP07Set[0].ActivityProfitTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP07Set[0].ActivityProfitTp);
                                ZakatForm5SummaryDataResult.SchGP07Set[0].ActivityProfitRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP07Set[0].ActivityProfitRr);

                                ZakatForm5SummaryDataResult.SchGP07Set[0].ZakatBaseTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP07Set[0].ZakatBaseTp);
                                ZakatForm5SummaryDataResult.SchGP07Set[0].ZakatBaseRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP07Set[0].ZakatBaseRr);

                            }

                            if (ZakatForm5SummaryDataResult.SchGP08Set.Any())
                            {
                                //Hotels
                                HotelsSummary = ZakatForm5SummaryDataResult.SchGP08Set;
                                HotelsSummaryIsVisible = true;
                                NoOFZakatList.Add("Hotels");
                                IsZakatFlag++;

                                ZakatForm5SummaryDataResult.SchGP08Set[0].ActivityProfitTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP08Set[0].ActivityProfitTp);
                                ZakatForm5SummaryDataResult.SchGP08Set[0].ActivityProfitRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP08Set[0].ActivityProfitRr);

                                ZakatForm5SummaryDataResult.SchGP08Set[0].ZakatBaseTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP08Set[0].ZakatBaseTp);
                                ZakatForm5SummaryDataResult.SchGP08Set[0].ZakatBaseRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP08Set[0].ZakatBaseRr);

                            }
                            if (ZakatForm5SummaryDataResult.SchGP09Set.Any())
                            {
                                //Edu. & Health
                                Edu_HealthSummary = ZakatForm5SummaryDataResult.SchGP09Set;
                                Edu_HealthSummaryIsVisible = true;
                                NoOFZakatList.Add("edu");
                                IsZakatFlag++;


                                ZakatForm5SummaryDataResult.SchGP09Set[0].ActivityProfitTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP09Set[0].ActivityProfitTp);
                                ZakatForm5SummaryDataResult.SchGP09Set[0].ActivityProfitRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP09Set[0].ActivityProfitRr);

                                ZakatForm5SummaryDataResult.SchGP09Set[0].ZakatBaseTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP09Set[0].ZakatBaseTp);
                                ZakatForm5SummaryDataResult.SchGP09Set[0].ZakatBaseRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP09Set[0].ZakatBaseRr);

                            }

                            if (ZakatForm5SummaryDataResult.SchGP10Set.Any())
                            {
                                //Poultry and Fish Farms Activities
                                Poultry_FishFarmSummary = ZakatForm5SummaryDataResult.SchGP10Set;
                                Poultry_FishFarmSummaryIsVisible = true;
                                NoOFZakatList.Add("Poultry");
                                IsZakatFlag++;


                                ZakatForm5SummaryDataResult.SchGP10Set[0].ActivityProfitTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP10Set[0].ActivityProfitTp);
                                ZakatForm5SummaryDataResult.SchGP10Set[0].ActivityProfitRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP10Set[0].ActivityProfitRr);

                                ZakatForm5SummaryDataResult.SchGP10Set[0].ZakatBaseTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP10Set[0].ZakatBaseTp);
                                ZakatForm5SummaryDataResult.SchGP10Set[0].ZakatBaseRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP10Set[0].ZakatBaseRr);

                            }

                            if (ZakatForm5SummaryDataResult.SchGP11Set.Any())
                            {
                                //Cars
                                CarsSummary = ZakatForm5SummaryDataResult.SchGP11Set;
                                CarsSummaryIsVisible = true;
                                NoOFZakatList.Add("Cars");
                                IsZakatFlag++;

                                ZakatForm5SummaryDataResult.SchGP11Set[0].ActivityProfitTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP11Set[0].ActivityProfitTp);
                                ZakatForm5SummaryDataResult.SchGP11Set[0].ActivityProfitRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP11Set[0].ActivityProfitRr);

                                ZakatForm5SummaryDataResult.SchGP11Set[0].ZakatBaseTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP11Set[0].ZakatBaseTp);
                                ZakatForm5SummaryDataResult.SchGP11Set[0].ZakatBaseRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP11Set[0].ZakatBaseRr);

                            }


                            if (ZakatForm5SummaryDataResult.SchGP12Set.Any())
                            {
                                //Minerls
                                MineralsSummary = ZakatForm5SummaryDataResult.SchGP12Set;
                                MineralsSummaryIsVisible = true;
                                NoOFZakatList.Add("Minerls");
                                IsZakatFlag++;

                                ZakatForm5SummaryDataResult.SchGP12Set[0].ActivityProfitTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP12Set[0].ActivityProfitTp);
                                ZakatForm5SummaryDataResult.SchGP12Set[0].ActivityProfitRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP12Set[0].ActivityProfitRr);

                                ZakatForm5SummaryDataResult.SchGP12Set[0].ZakatBaseTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP12Set[0].ZakatBaseTp);
                                ZakatForm5SummaryDataResult.SchGP12Set[0].ZakatBaseRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP12Set[0].ZakatBaseRr);

                            }

                            if (IsZakatFlag != 0)
                            {
                                IsZakatEstListVisible = true;
                            }
                            else
                            {
                                IsZakatEstListVisible = false;
                            }
                        }

                        //  IsLoading = false;

                    }
                    else
                    {
                        isNoDataLableVisible = true;
                        //   IsLoading = false;
                    }
                }
                else
                {
                    isNoDataLableVisible = true;
                    // IsLoading = false;
                }
            }
            catch (Exception ex)
            {
                await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                _navigationService.GoBack();
                IsLoading = false;
            }

            IsLoading = false;
        }

        public new void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                if (App.TP != null)
                    App.TP = null;
                if (App.PreviousIsArabic)
                {
                    string langName = "ar-SA";
                    AppResources.Culture = new CultureInfo(langName);
                }
                else
                {
                    string langName = "en-US";
                    AppResources.Culture = new CultureInfo(langName);
                }
                var _navigation = Application.Current.MainPage.Navigation;
                foreach (var item in _navigation.NavigationStack)
                {
                    if (item.GetType().Name == App.SFLoginPageView)
                    {
                        _navigation.RemovePage(item);
                        break;
                    }
                }

                _navigationService.NavigateTo(App.SFLoginPageView, App.GAZTNewDesignDashBoardPageView);
                _navigation.NavigationStack.ToList().Clear();
            }
        }


        private void navigateToNext()
        {
            switch (currentTab)
            {

                case ZakatForm5TabEnum.BasicInformation:
                    currentTab = ZakatForm5TabEnum.FinancialInformation;
                    break;

                case ZakatForm5TabEnum.FinancialInformation:
                    {
                        currentTab = ZakatForm5TabEnum.ZakatEstimation;
                        if (AknowledgementList.Any())
                        {

                            NextText = AppResources.ZZNext;
                        }
                        else
                        {
                            NextText = AppResources.Form5Finish;
                        }
                        break;
                    }
                case ZakatForm5TabEnum.ZakatEstimation:
                    {
                        if (AknowledgementList.Any())
                        {
                            _navigationService.NavigateTo(App.ZakatAcknowledgmentPageView, AknowledgementList);
                        }
                        else
                        {
                            _navigationService.GoBack();
                            currentTab = ZakatForm5TabEnum.BasicInformation;
                        }
                        break;
                    }
            }
        }

        private void navigateBack()
        {
            switch (currentTab)
            {
                case ZakatForm5TabEnum.BasicInformation:
                    _navigationService.GoBack();
                    break;
                case ZakatForm5TabEnum.FinancialInformation:
                    currentTab = ZakatForm5TabEnum.BasicInformation;
                    break;
                case ZakatForm5TabEnum.ZakatEstimation:
                    NextText = AppResources.ZZNext;
                    currentTab = ZakatForm5TabEnum.FinancialInformation;

                    break;
            }
        }

        public static string ConvertDateCalendar(DateTime DateConv, string Calendar, string DateLangCulture)
        {
            DateTimeFormatInfo DTFormat;
            DateLangCulture = DateLangCulture.ToLower();
            /// We can't have the hijri date writen in English. We will get a runtime error

            if (Calendar == "Hijri" && DateLangCulture.StartsWith("en-"))
            {
                DateLangCulture = "ar-sa";
            }

            /// Set the date time format to the given culture
            DTFormat = new CultureInfo(DateLangCulture, false).DateTimeFormat;

            /// Set the calendar property of the date time format to the given calendar
            switch (Calendar)
            {
                case "Hijri":
                    DTFormat.Calendar = new UmAlQuraCalendar();
                    break;

                case "Gregorian":
                    DTFormat.Calendar = new GregorianCalendar();
                    break;

                default:
                    return "";
            }

            /// We format the date structure to whatever we want
            DTFormat.ShortDatePattern = "dd/MM/yyyy";
            return DateConv.Date.ToString("f", DTFormat);
        }


        #endregion
    }
}
