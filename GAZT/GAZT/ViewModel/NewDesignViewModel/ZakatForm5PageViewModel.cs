using EGAZT.Models;
using EGAZT.Models.Form5Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ZakatForm5Model;

namespace EGAZT.ViewModel.NewDesignViewModel
{
     public class ZakatForm5PageViewModel : BaseViewModel
    {

        #region Variable

        //private FinancialSubTabEnum _subTab = FinancialSubTabEnum.Cabs;
        //public FinancialSubTabEnum subTab
        //{
        //    get => _subTab;
        //    private set
        //    {
        //        _subTab = value;
        //        RaisePropertyChanged(nameof(subTab));
        //    }
        //}


        private ZakatForm5TabEnum _currentTab = ZakatForm5TabEnum.BasicInformation;
        public ZakatForm5TabEnum currentTab
        {
            get => _currentTab;
            private set
            {
                _currentTab = value;
                RaisePropertyChanged(nameof(currentTab));
                CurrentIndex = (int)_currentTab;
                RaisePropertyChanged(nameof(CurrentIndex));
            }
        }
        private int _currenrIndex = 1;
        public int CurrentIndex
        {
            get => _currenrIndex;
            set
            {
                _currenrIndex = value;
                RaisePropertyChanged(nameof(CurrentIndex));
                if (_currenrIndex == MaxIndex)
                {
                    MarkComplete = true;
                    RaisePropertyChanged(nameof(MarkComplete));
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
                _isAdditionalVisible = value;
                RaisePropertyChanged("isAdditionalVisible");
            }
        }

        public bool _IsLoading = false;
        public bool IsLoading
        {
            get
            {
                return _IsLoading;
            }
            set
            {
                _IsLoading = value;
                RaisePropertyChanged("IsLoading");
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
                _isMineralVisible = value;
                RaisePropertyChanged("isMineralVisible");
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
                _isCarVisible = value;
                RaisePropertyChanged("isCarVisible");
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
                _isPoultryVisible = value;
                RaisePropertyChanged("isPoultryVisible");
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
                _isEducationVisible = value;
                RaisePropertyChanged("isEducationVisible");
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
                _isHotelVisible = value;
                RaisePropertyChanged("isHotelVisible");
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
                _isRealEstateVisible = value;
                RaisePropertyChanged("isRealEstateVisible");
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
                _isContractingVisible = value;
                RaisePropertyChanged("isContractingVisible");
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
                _isIndustryVisible = value;
                RaisePropertyChanged("isIndustryVisible");
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
                _isLabourOccupancyVisible = value;
                RaisePropertyChanged("isLabourOccupancyVisible");
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
                _isBuyVisible = value;
                RaisePropertyChanged("isBuyVisible");
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
                _isProfessionalVisible = value;
                RaisePropertyChanged("isProfessionalVisible");
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
                _isCabVisible = value;
                RaisePropertyChanged("isCabVisible");
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
                _nextText = value;
                RaisePropertyChanged("NextText");
            }
        }
        #endregion

        #region Commands
        public ICommand OnNextButtonClick { get; private set; }
        public ICommand OnBackButtonClick { get; private set; }
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
                _zakatForm5DataResult = value;
                RaisePropertyChanged("ZakatForm5DataResult");
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
                _isNoDataLableVisible = value;
                RaisePropertyChanged("isNoDataLableVisible");
            }
        }


        //Return Detials

        //public string _financialYear = "Value Unavailable";
        //public string FinancialYear
        //{
        //    get
        //    {
        //        return _financialYear;
        //    }
        //    set
        //    {
        //        _financialYear = value;
        //        RaisePropertyChanged(FinancialYear);
        //    }
        //}


        public string _fbguid;
        public string Fbguid
        {
            get => _fbguid;

            set
            {
                _fbguid = value;
                RaisePropertyChanged(() => Fbguid);
            }
        }



        public string _financialYear ;
        public string FinancialYear
        {
            get => _financialYear;
            
            set
            {
                _financialYear = value;
                RaisePropertyChanged(() => FinancialYear);
            }
        }




        public string _period;
        public string Period
        {
            get=> _period;
            
            set
            {
                _period = value;
                RaisePropertyChanged(() => Period);
            }
        }

        public string _ZakatToDate;
        public string ZakatToDate
        {
            get => _ZakatToDate;

            set
            {
                _ZakatToDate = value;
                RaisePropertyChanged(() => ZakatToDate);
            }
        }


        public string _ZakatFromDate;
        public string ZakatFromDate
        {
            get => _ZakatFromDate;

            set
            {
                _ZakatFromDate = value;
                RaisePropertyChanged(() => ZakatFromDate);
            }
        }
        //Taxpayer Detials


        public string _taxpayer;
        public string Taxpayer
        {
            get=> _taxpayer;
            
            set
            {
                _taxpayer = value;
                RaisePropertyChanged(() => Taxpayer);
            }
        }


        public string _branch;
        public string Branch
        {
            get=>_branch;
            
            set
            {
                _branch = value;
                RaisePropertyChanged(() => Branch);
            }
        }


        public string _address;
        public string Address
        {
            get=>_address;
            
            set
            {
                var newAddress = value.Replace(@", ,", "");
                newAddress = newAddress.Replace(@", ,", "");
                newAddress = newAddress.Replace(@",,", "");
                _address = newAddress.Replace(@" ,", "");
                RaisePropertyChanged(() => Address);
            }
        }

        public string _userEmail;
        public string UserEmail
        {
            get=> _userEmail;
            
            set
            {
                _userEmail = value;
                RaisePropertyChanged(() => UserEmail);
            }
        }

        public string _mobileNumber;
        public string MobileNumber
        {
            get=> _mobileNumber;
            
            set
            {
                _mobileNumber = value;
                RaisePropertyChanged(() => MobileNumber);
            }
        }


        //Registration Information
        public string _numberOfOutlet;
        public string NumberOfOutlet
        {
            get=>_numberOfOutlet;
            
            set
            {
                _numberOfOutlet = value;
                RaisePropertyChanged(() => NumberOfOutlet);
            }
        }


        public string _residencyStatus;
        public string Residency_Status
        {
            get=>_residencyStatus;
            
            set
            {
                _residencyStatus = value;
                RaisePropertyChanged(() => Residency_Status);
            }
        }

        public string _mainOutlet;
        public string MainOutlet
        {
            get=> _mainOutlet;
            
            set
            {
                _mainOutlet = value;
                RaisePropertyChanged(() => MainOutlet);
            }
        }

        public string _accMethod;
        public string AccountMethod
        {
            get=> _accMethod;
            
            set
            {
                _accMethod = value;
                RaisePropertyChanged(() => AccountMethod);
            }
        }

        public string _financialPeriod;
        public string FinancialPeriod
        {
            get=> _financialPeriod;
            
            set
            {
                _financialPeriod = value;
                RaisePropertyChanged(() => FinancialPeriod);
            }
        }

        public string _calendarType;
        public string Calendar_Type
        {
            get=>_calendarType;
            
            set
            {
                _calendarType = value;
                RaisePropertyChanged(() => Calendar_Type);
            }
        }


        public bool _IsConditionRadio;
        public bool IsConditionRadio
        {
            get => _IsConditionRadio;

            set
            {
                _IsConditionRadio = value;
                RaisePropertyChanged(() => IsConditionRadio);
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
                _IsCabBtn = value;
                RaisePropertyChanged("IsCabBtn");
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
                _IsProfessionBtn = value;
                RaisePropertyChanged("IsProfessionBtn");
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
                _IsSellBtn = value;
                RaisePropertyChanged("IsSellBtn");
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
                _IsLabourBtn = value;
                RaisePropertyChanged("IsLabourBtn");
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
                _IsIndustryBtn = value;
                RaisePropertyChanged("IsIndustryBtn");
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
                _IsContractBtn = value;
                RaisePropertyChanged("IsContractBtn");
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
                _IsInvestBtn = value;
                RaisePropertyChanged("IsInvestBtn");
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
                _IsHotelsBtn = value;
                RaisePropertyChanged("IsHotelsBtn");
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
                _IsEduBtn = value;
                RaisePropertyChanged("IsEduBtn");
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
                _IsPoultryBtn = value;
                RaisePropertyChanged("IsPoultryBtn");
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
                _IsCarBtn = value;
                RaisePropertyChanged("IsCarBtn");
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
                _IsMineralsBtn = value;
                RaisePropertyChanged("IsMineralsBtn");
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
                _IsAddBtn = value;
                RaisePropertyChanged("IsAddBtn");
            }
        }


        /// <summary>
        ///  No of Entity List which shows horizontal chips scroll
        /// </summary>
        /// 
        private List<string> _noOfEntityList = new List<string>() ;
        public List<string> NoOFEntityList
        {
            get
            {
                return _noOfEntityList;
            }
            set
            {
                _noOfEntityList = value;
                RaisePropertyChanged("NoOFEntityList");
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
                _cabsList = value;
                RaisePropertyChanged("Cabs");
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
                _professionalsList = value;
                RaisePropertyChanged("Professionals");
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
                _sell_BuyList = value;
                RaisePropertyChanged("Sell_Buy");
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
                _labourOccupList = value;
                RaisePropertyChanged("LabourOccup");
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
                _industryList = value;
                RaisePropertyChanged("Industry");
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
                _contractingList = value;
                RaisePropertyChanged("Contracting");
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
                _invstRealEstList = value;
                RaisePropertyChanged("InvstRealEst");
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
                _hotelsList = value;
                RaisePropertyChanged("Hotels");
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
                _edu_HealthList = value;
                RaisePropertyChanged("Edu_Health");
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
                _poultry_FishFarmList = value;
                RaisePropertyChanged("Poultry_FishFarm");
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
                _carsList = value;
                RaisePropertyChanged("Cars");
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
                _mineralsList = value;
                RaisePropertyChanged("Minerals");
            }
        }

        /// <summary>
        ///  Other List
        /// </summary>

        //private List<Result28> _otherList;
        //public List<Result28> Other
        //{
        //    get
        //    {
        //        return _otherList;
        //    }
        //    set
        //    {
        //        _otherList = value;
        //        RaisePropertyChanged("Other");
        //    }
        //}


        /// <summary>
        /// Share in persons company
        /// </summary>

        public string _isOtherComShareApp ;
        public string IsOtherComShareApp
        {
            get => _isOtherComShareApp;
            
            set
            {
                _isOtherComShareApp = value;
                RaisePropertyChanged(() => IsOtherComShareApp);
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
                _IsOtherComShareAppVisible = value;
                RaisePropertyChanged("IsOtherComShareAppVisible");
            }
        }


        public string _otherCompanyShare;
        public string OtherCompanyShare
        {
            get => _otherCompanyShare;

            set
            {
                _otherCompanyShare = value;
                RaisePropertyChanged(() => OtherCompanyShare);
            }
        }


        public string _zakatBase;
        public string ZakatBase
        {
            get => _zakatBase;

            set
            {
                _zakatBase = value;
                RaisePropertyChanged(() => ZakatBase);
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
                _noOfBranch = value;
                RaisePropertyChanged(() => NoOfBranch);
            }
        }

        public string _noofEmp;
        public string NoofEmp
        {
            get => _noofEmp;

            set
            {
                _noofEmp = value;
                RaisePropertyChanged(() => NoofEmp);
            }
        }

        public string _yearRent;
        public string YearRent
        {
            get => _yearRent;

            set
            {
                _yearRent = value;
                RaisePropertyChanged(() => YearRent);
            }
        }

        public string _totalAnnualSalary;
        public string TotalAnnualSalary
        {
            get => _totalAnnualSalary;

            set
            {
                _totalAnnualSalary = value;
                RaisePropertyChanged(() => TotalAnnualSalary);
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
                _zakatable = value;
                RaisePropertyChanged(() => Zakatable);
            }
        }

        public string _zakat;
        public string Zakat
        {
            get => _zakat;

            set
            {
                _zakat = value;
                RaisePropertyChanged(() => Zakat);
            }
        }

        public string _zakatPaid;
        public string ZakatPaid
        {
            get => _zakatPaid;

            set
            {
                _zakatPaid = value;
                RaisePropertyChanged(() => ZakatPaid);
            }
        }

        public string _newTaxAmt;
        public string NewTaxAmt
        {
            get => _newTaxAmt;

            set
            {
                _newTaxAmt = value;
                RaisePropertyChanged(() => NewTaxAmt);
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
                _referenceNumber = value;
                RaisePropertyChanged(() => ReferenceNumber);
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
                _noOfZakatList = value;
                RaisePropertyChanged("NoOFZakatList");
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
                RaisePropertyChanged(() => IsZakatEstListVisible);
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
                _cabsSummaryList = value;
                RaisePropertyChanged("CabsSummary");
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
                _cabsSummaryIsVisible = value;
                RaisePropertyChanged(() => CabsSummaryIsVisible);
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
                _professionalsSummaryList = value;
                RaisePropertyChanged("ProfessionalsSummary");
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
                _professionalsSummaryIsVisible = value;
                RaisePropertyChanged(() => ProfessionalsSummaryIsVisible);
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
                _sell_BuySummaryList = value;
                RaisePropertyChanged("Sell_BuySummary");
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
                _sell_BuySummaryIsVisible = value;
                RaisePropertyChanged(() => Sell_BuySummaryIsVisible);
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
                _labourOccupSummaryList = value;
                RaisePropertyChanged("LabourOccupSummary");
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
                _LabourOccupSummaryIsVisble = value;
                RaisePropertyChanged(() => LabourOccupSummaryIsVisble);
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
                _industrySummaryList = value;
                RaisePropertyChanged("IndustrySummary");
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
                _IndustrySummaryIsVisible = value;
                RaisePropertyChanged(() => IndustrySummaryIsVisible);
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
                _contractingListSummary = value;
                RaisePropertyChanged("ContractingSummary");
            }
        }

        //  public bool ContractingSummaryIsVisible { get;  set; }

        public bool _ContractingSummaryIsVisible = false;
        public bool ContractingSummaryIsVisible
        {
            get
            {
                return _IndustrySummaryIsVisible;
            }
            set
            {
                _ContractingSummaryIsVisible = value;
                RaisePropertyChanged(() => ContractingSummaryIsVisible);
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
                _invstRealEstSummaryList = value;
                RaisePropertyChanged("InvstRealEstSummary");
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
                _InvstRealEstSummaryIsVisible = value;
                RaisePropertyChanged(() => InvstRealEstSummaryIsVisible);
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
                _hotelsSummaryList = value;
                RaisePropertyChanged("HotelsSummary");
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
                _HotelsSummaryIsVisible = value;
                RaisePropertyChanged(() => HotelsSummaryIsVisible);
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
                _edu_HealthSummaryList = value;
                RaisePropertyChanged("Edu_HealthSummary");
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
                _Edu_HealthSummaryIsVisible = value;
                RaisePropertyChanged(() => Edu_HealthSummaryIsVisible);
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
                _poultry_FishFarmSummaryList = value;
                RaisePropertyChanged("Poultry_FishFarmSummary");
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
                _Poultry_FishFarmSummaryIsVisible = value;
                RaisePropertyChanged(() => Poultry_FishFarmSummaryIsVisible);
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
                _carsSummaryList = value;
                RaisePropertyChanged("CarsSummary");
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
                _CarsSummaryIsVisible = value;
                RaisePropertyChanged(() => CarsSummaryIsVisible);
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
                _mineralsSummaryList = value;
                RaisePropertyChanged("MineralsSummary");
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
                _MineralsSummaryIsVisible = value;
                RaisePropertyChanged(() => MineralsSummaryIsVisible);
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
                _AknowledgementList = value;
                RaisePropertyChanged("AknowledgementList");
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
            // IsLoading = true;
            await Task.Run(() =>
            {
                IsLoading = true;
            });
          

                

            await Task.Run(async() =>
            {
                try
                {
                    ZakatForm5DataResult = null;

                    ZakatForm5CityDataResult ZakatForm5CityDataResults = await WebServiceManager.GAZTZakatForm5CityData();

                    if (ZakatForm5CityDataResults != null)
                    {

                        var CityDAta = ZakatForm5CityDataResults;



                        ZakatForm5DataResult ZakatForm5DataResult = await WebServiceManager.GAZTZakatForm5Data(Fbguid);

                        PopToRootPage();// If seesion Expired it will navigate to Dashboard page




                        if (ZakatForm5DataResult != null)
                        {

                            //Bind values to UI

                            //Return Detials
                            FinancialYear = ZakatForm5DataResult.PerslText;
                            IsConditionRadio = true;

                            Period = Convert.ToDateTime(ZakatForm5DataResult.AFromDt).ToString("dd-MMMM-yyyy", new CultureInfo("en-US")) + " - " + Convert.ToDateTime(ZakatForm5DataResult.AToDt).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));


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

                            if(ZakatForm5DataResult.AResidency == "Resident")
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
                            if (ZakatForm5DataResult.SCH_GP01.results.Any())
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
                                for (int i = 0; i < ZakatForm5DataResult.SCH_GP01.results.Count; i++)
                                {

                                    ZakatForm5DataResult.SCH_GP01.results[i].NoOfCars = Math.Round(Convert.ToDecimal(UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP01.results[i].NoOfCars))).ToString();
                                    ZakatForm5DataResult.SCH_GP01.results[i].AgvDlyIncome = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP01.results[i].AgvDlyIncome);
                                    ZakatForm5DataResult.SCH_GP01.results[i].OccRate = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP01.results[i].OccRate);
                                    ZakatForm5DataResult.SCH_GP01.results[i].NetPftPer = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP01.results[i].NetPftPer);
                                    ZakatForm5DataResult.SCH_GP01.results[i].Revenue = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP01.results[i].Revenue);
                                    ZakatForm5DataResult.SCH_GP01.results[i].Expenses = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP01.results[i].Expenses);
                                    ZakatForm5DataResult.SCH_GP01.results[i].NetPft = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP01.results[i].NetPft);



                                    if (ZakatForm5DataResult.SCH_GP01.results[i].Expenses != "0.00")
                                    {
                                        //ZakatForm5DataResult.SCH_GP01.results[i].IsApplicable =AppResources.FORM5Applicable.ToString();
                                        ZakatForm5DataResult.SCH_GP01.results[i].IsApplicable = AppResources.FORM5Applicable.ToString();
                                        ZakatForm5DataResult.SCH_GP01.results[i].IsApplicableVisible = true;

                                    }
                                    else
                                    {
                                        // ZakatForm5DataResult.SCH_GP01.results[i].IsApplicable = AppResources.FORM5NotApplicable.ToString();
                                        ZakatForm5DataResult.SCH_GP01.results[i].IsApplicable = AppResources.FORM5NotApplicable.ToString();
                                        ZakatForm5DataResult.SCH_GP01.results[i].IsApplicableVisible = false;

                                    }
                                }
                                Cabs = ZakatForm5DataResult.SCH_GP01.results;
                            }
                            //Professionals
                            if (ZakatForm5DataResult.SCH_GP02.results.Any())
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

                                for (int i = 0; i < ZakatForm5DataResult.SCH_GP02.results.Count; i++)
                                {

                                    ZakatForm5DataResult.SCH_GP02.results[i].Revenue = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP02.results[i].Revenue);
                                    ZakatForm5DataResult.SCH_GP02.results[i].Expenses = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP02.results[i].Expenses);
                                    ZakatForm5DataResult.SCH_GP02.results[i].NetPft = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP02.results[i].NetPft);



                                    if (ZakatForm5DataResult.SCH_GP02.results[i].Expenses != "0.00")
                                    {
                                        ZakatForm5DataResult.SCH_GP02.results[i].IsApplicable = AppResources.FORM5Applicable.ToString();
                                        ZakatForm5DataResult.SCH_GP02.results[i].IsApplicableVisible = true;
                                    }
                                    else
                                    {
                                        ZakatForm5DataResult.SCH_GP02.results[i].IsApplicable = AppResources.FORM5NotApplicable.ToString();
                                        ZakatForm5DataResult.SCH_GP02.results[i].IsApplicableVisible = false;

                                    }
                                }
                                Professionals = ZakatForm5DataResult.SCH_GP02.results;
                            }
                            //Sell & Buy
                            if (ZakatForm5DataResult.SCH_GP03.results.Any())
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

                                for (int i = 0; i < ZakatForm5DataResult.SCH_GP03.results.Count; i++)
                                {
                                    // ZakatForm5DataResult.SCH_GP03.results[i].IsImportVisible = false;
                                    //  ZakatForm5DataResult.SCH_GP03.results[i].IsProcurementVisible = false;

                                    ZakatForm5DataResult.SCH_GP03.results[i].Capital = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP03.results[i].Capital);
                                    ZakatForm5DataResult.SCH_GP03.results[i].SuppCon = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP03.results[i].SuppCon);
                                    ZakatForm5DataResult.SCH_GP03.results[i].ProfitRatio = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP03.results[i].ProfitRatio);
                                    ZakatForm5DataResult.SCH_GP03.results[i].Sales = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP03.results[i].Sales);
                                    ZakatForm5DataResult.SCH_GP03.results[i].SalePrfRatio = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP03.results[i].SalePrfRatio);

                                    ZakatForm5DataResult.SCH_GP03.results[i].GenTrade = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP03.results[i].GenTrade);
                                    ZakatForm5DataResult.SCH_GP03.results[i].Livelihoods = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP03.results[i].Livelihoods);
                                    ZakatForm5DataResult.SCH_GP03.results[i].LiveStkAnimals = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP03.results[i].LiveStkAnimals);
                                    ZakatForm5DataResult.SCH_GP03.results[i].ExtnProc = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP03.results[i].ExtnProc);

                                    ZakatForm5DataResult.SCH_GP03.results[i].GenTradeI = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP03.results[i].GenTradeI);
                                    ZakatForm5DataResult.SCH_GP03.results[i].LivelihoodsI = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP03.results[i].LivelihoodsI);
                                    ZakatForm5DataResult.SCH_GP03.results[i].LiveStkAnimals = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP03.results[i].LiveStkAnimals);
                                    ZakatForm5DataResult.SCH_GP03.results[i].IntnProc = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP03.results[i].IntnProc);


                                    if (ZakatForm5DataResult.SCH_GP03.results[i].SuppCon != "0.00")
                                    {
                                        ZakatForm5DataResult.SCH_GP03.results[i].IsApplicable = AppResources.FORM5Applicable.ToString();
                                        ZakatForm5DataResult.SCH_GP03.results[i].IsApplicableVisible = true;

                                    }
                                    else
                                    {
                                        ZakatForm5DataResult.SCH_GP03.results[i].IsApplicable = AppResources.FORM5NotApplicable.ToString();
                                        ZakatForm5DataResult.SCH_GP03.results[i].IsApplicableVisible = false;

                                    }

                                    //if ((ZakatForm5DataResult.SCH_GP03.results[i].GenTrade == "0.00") && (ZakatForm5DataResult.SCH_GP03.results[i].Livelihoods == "0.00" )&& (ZakatForm5DataResult.SCH_GP03.results[i].LiveStkAnimals == "0.00"))
                                    //{
                                    //    ZakatForm5DataResult.SCH_GP03.results[i].IsImportVisible = false;

                                    //    ZakatForm5DataResult.SCH_GP03.results[i].IsImport = AppResources.ZNo.ToString();
                                    //}
                                    //else
                                    //{
                                    //    ZakatForm5DataResult.SCH_GP03.results[i].IsImportVisible = true;

                                    //    ZakatForm5DataResult.SCH_GP03.results[i].IsImport = AppResources.ZYes.ToString();

                                    //}


                                    if (ZakatForm5DataResult.SCH_GP03.results[i].GenTrade != "0.00")
                                    {
                                        ZakatForm5DataResult.SCH_GP03.results[i].IsImportVisible = true;


                                        ZakatForm5DataResult.SCH_GP03.results[i].IsImport = AppResources.ZYes.ToString();

                                    }
                                    else if (ZakatForm5DataResult.SCH_GP03.results[i].Livelihoods != "0.00")
                                    {
                                        ZakatForm5DataResult.SCH_GP03.results[i].IsImportVisible = true;

                                        ZakatForm5DataResult.SCH_GP03.results[i].IsImport = AppResources.ZYes.ToString();
                                    }
                                    else if (ZakatForm5DataResult.SCH_GP03.results[i].LiveStkAnimals != "0.00")
                                    {
                                        ZakatForm5DataResult.SCH_GP03.results[i].IsImportVisible = true;

                                        ZakatForm5DataResult.SCH_GP03.results[i].IsImport = AppResources.ZYes.ToString();
                                    }
                                    else
                                    {
                                        ZakatForm5DataResult.SCH_GP03.results[i].IsImportVisible = false;
                                        ZakatForm5DataResult.SCH_GP03.results[i].IsImport = AppResources.ZNo.ToString();
                                    }


                                    if (ZakatForm5DataResult.SCH_GP03.results[i].GenTrade != "0.00")
                                    {
                                        ZakatForm5DataResult.SCH_GP03.results[i].Imp_IsGeneralApplicable = AppResources.FORM5Applicable.ToString();
                                        ZakatForm5DataResult.SCH_GP03.results[i].Imp_IsGeneralApplicableVisible = true;

                                    }
                                    else
                                    {
                                        ZakatForm5DataResult.SCH_GP03.results[i].Imp_IsGeneralApplicable = AppResources.FORM5NotApplicable.ToString();
                                        ZakatForm5DataResult.SCH_GP03.results[i].Imp_IsGeneralApplicableVisible = false;

                                    }

                                    if (ZakatForm5DataResult.SCH_GP03.results[i].Livelihoods != "0.00")
                                    {
                                        ZakatForm5DataResult.SCH_GP03.results[i].Imp_IsLiveLihoodsApplicable = AppResources.FORM5Applicable.ToString();
                                        ZakatForm5DataResult.SCH_GP03.results[i].Imp_IsLiveLihoodsApplicableVisible = true;

                                    }
                                    else
                                    {
                                        ZakatForm5DataResult.SCH_GP03.results[i].Imp_IsLiveLihoodsApplicable = AppResources.FORM5NotApplicable.ToString();
                                        ZakatForm5DataResult.SCH_GP03.results[i].Imp_IsLiveLihoodsApplicableVisible = false;

                                    }

                                    if (ZakatForm5DataResult.SCH_GP03.results[i].LiveStkAnimals != "0.00")
                                    {
                                        ZakatForm5DataResult.SCH_GP03.results[i].Imp_IsLivestockApplicable = AppResources.FORM5Applicable.ToString();
                                        ZakatForm5DataResult.SCH_GP03.results[i].Imp_IsLivestockApplicableVisible = true;

                                    }
                                    else
                                    {
                                        ZakatForm5DataResult.SCH_GP03.results[i].Imp_IsLivestockApplicable = AppResources.FORM5NotApplicable.ToString();
                                        ZakatForm5DataResult.SCH_GP03.results[i].Imp_IsLivestockApplicableVisible = false;

                                    }

                                    //if ((ZakatForm5DataResult.SCH_GP03.results[i].GenTradeI == "0.00") && (ZakatForm5DataResult.SCH_GP03.results[i].LivelihoodsI == "0.00") && (ZakatForm5DataResult.SCH_GP03.results[i].LiveStkAnimalsI == "0.00"))
                                    //{
                                    //    ZakatForm5DataResult.SCH_GP03.results[i].IsProcurementVisible = false;

                                    //    ZakatForm5DataResult.SCH_GP03.results[i].IsProcurement = AppResources.ZNo.ToString();

                                    //}
                                    //else
                                    //{


                                    //    ZakatForm5DataResult.SCH_GP03.results[i].IsProcurement = AppResources.ZYes.ToString();
                                    //    ZakatForm5DataResult.SCH_GP03.results[i].IsProcurementVisible = true;
                                    //}

                                    if (ZakatForm5DataResult.SCH_GP03.results[i].GenTradeI != "0.00")
                                    {
                                        ZakatForm5DataResult.SCH_GP03.results[i].IsProcurement = AppResources.ZYes.ToString();
                                        ZakatForm5DataResult.SCH_GP03.results[i].IsProcurementVisible = true;
                                    }
                                    else if (ZakatForm5DataResult.SCH_GP03.results[i].LivelihoodsI != "0.00")
                                    {
                                        ZakatForm5DataResult.SCH_GP03.results[i].IsProcurement = AppResources.ZYes.ToString();
                                        ZakatForm5DataResult.SCH_GP03.results[i].IsProcurementVisible = true;
                                    }
                                    else if (ZakatForm5DataResult.SCH_GP03.results[i].LiveStkAnimalsI != "0.00")
                                    {
                                        ZakatForm5DataResult.SCH_GP03.results[i].IsProcurement = AppResources.ZYes.ToString();
                                        ZakatForm5DataResult.SCH_GP03.results[i].IsProcurementVisible = true;
                                    }
                                    else
                                    {
                                        ZakatForm5DataResult.SCH_GP03.results[i].IsProcurement = AppResources.ZNo.ToString();
                                        ZakatForm5DataResult.SCH_GP03.results[i].IsProcurementVisible = false;
                                    }





                                    if (ZakatForm5DataResult.SCH_GP03.results[i].GenTradeI != "0.00")
                                    {
                                        ZakatForm5DataResult.SCH_GP03.results[i].Pro_IsGeneralApplicable = AppResources.FORM5Applicable.ToString();
                                        ZakatForm5DataResult.SCH_GP03.results[i].Pro_IsGeneralApplicableVisible = true;

                                    }
                                    else
                                    {
                                        ZakatForm5DataResult.SCH_GP03.results[i].Pro_IsGeneralApplicable = AppResources.FORM5NotApplicable.ToString();
                                        ZakatForm5DataResult.SCH_GP03.results[i].Pro_IsGeneralApplicableVisible = false;

                                    }
                                    if (ZakatForm5DataResult.SCH_GP03.results[i].LivelihoodsI != "0.00")
                                    {
                                        ZakatForm5DataResult.SCH_GP03.results[i].Pro_IsLiveLihoodsApplicable = AppResources.FORM5Applicable.ToString();
                                        ZakatForm5DataResult.SCH_GP03.results[i].Pro_IsLiveLihoodsApplicableVisible = true;

                                    }
                                    else
                                    {
                                        ZakatForm5DataResult.SCH_GP03.results[i].Pro_IsLiveLihoodsApplicable = AppResources.FORM5NotApplicable.ToString();
                                        ZakatForm5DataResult.SCH_GP03.results[i].Pro_IsLiveLihoodsApplicableVisible = false;

                                    }
                                    if (ZakatForm5DataResult.SCH_GP03.results[i].LiveStkAnimalsI != "0.00")
                                    {
                                        ZakatForm5DataResult.SCH_GP03.results[i].Pro_IsLivestockApplicable = AppResources.FORM5Applicable.ToString();
                                        ZakatForm5DataResult.SCH_GP03.results[i].Pro_IsLivestockApplicableVisible = true;

                                    }
                                    else
                                    {
                                        ZakatForm5DataResult.SCH_GP03.results[i].Pro_IsLivestockApplicable = AppResources.FORM5NotApplicable.ToString();
                                        ZakatForm5DataResult.SCH_GP03.results[i].Pro_IsLivestockApplicableVisible = false;

                                    }



                                    if (ZakatForm5DataResult.SCH_GP03.results[i].SuppCon != "0.00")
                                    {
                                        ZakatForm5DataResult.SCH_GP03.results[i].IsApplicable = AppResources.FORM5Applicable.ToString();
                                    }
                                    else
                                    {
                                        ZakatForm5DataResult.SCH_GP03.results[i].IsApplicable = AppResources.FORM5NotApplicable.ToString();
                                    }


                                }


                                Sell_Buy = ZakatForm5DataResult.SCH_GP03.results;
                            }
                            //Labour Occup.
                            if (ZakatForm5DataResult.SCH_GP04.results.Any())
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

                                for (int i = 0; i < ZakatForm5DataResult.SCH_GP04.results.Count; i++)
                                {
                                    ZakatForm5DataResult.SCH_GP04.results[i].NoOfLabour = Math.Round(Convert.ToDecimal(UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP04.results[i].NoOfLabour))).ToString();
                                    ZakatForm5DataResult.SCH_GP04.results[i].Revenue = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP04.results[i].Revenue);
                                    ZakatForm5DataResult.SCH_GP04.results[i].Expenses = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP04.results[i].Expenses);
                                    ZakatForm5DataResult.SCH_GP04.results[i].NetProfit = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP04.results[i].NetProfit);


                                    if (ZakatForm5DataResult.SCH_GP04.results[i].Expenses != "0.00")
                                    {
                                        ZakatForm5DataResult.SCH_GP04.results[i].IsExpensesApplicable = AppResources.FORM5Applicable.ToString();
                                        ZakatForm5DataResult.SCH_GP04.results[i].IsApplicableVisible = true;

                                    }
                                    else
                                    {
                                        ZakatForm5DataResult.SCH_GP04.results[i].IsExpensesApplicable = AppResources.FORM5NotApplicable.ToString();
                                        ZakatForm5DataResult.SCH_GP04.results[i].IsApplicableVisible = false;

                                    }
                                }

                                LabourOccup = ZakatForm5DataResult.SCH_GP04.results;
                            }
                            //Industry
                            if (ZakatForm5DataResult.SCH_GP05.results.Any())
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

                                for (int i = 0; i < ZakatForm5DataResult.SCH_GP05.results.Count; i++)
                                {
                                    ZakatForm5DataResult.SCH_GP05.results[i].FundingTot = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP05.results[i].FundingTot);
                                    ZakatForm5DataResult.SCH_GP05.results[i].CapitalStr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP05.results[i].CapitalStr);
                                    ZakatForm5DataResult.SCH_GP05.results[i].Profit = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP05.results[i].Profit);
                                    ZakatForm5DataResult.SCH_GP05.results[i].Sales = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP05.results[i].Sales);


                                }

                                NoOFEntityList.Add("Industry");
                                Industry = ZakatForm5DataResult.SCH_GP05.results;
                            }
                            // Contracting CO.
                            if (ZakatForm5DataResult.SCH_GP06.results.Any())
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

                                for (int i = 0; i < ZakatForm5DataResult.SCH_GP06.results.Count; i++)
                                {
                                    ZakatForm5DataResult.SCH_GP06.results[i].GovtContractProf = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP06.results[i].GovtContractProf);
                                    ZakatForm5DataResult.SCH_GP06.results[i].CivilContrRev = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP06.results[i].CivilContrRev);
                                    ZakatForm5DataResult.SCH_GP06.results[i].OtherIncome = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP06.results[i].OtherIncome);
                                    ZakatForm5DataResult.SCH_GP06.results[i].TotalProfit = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP06.results[i].TotalProfit);
                                    ZakatForm5DataResult.SCH_GP06.results[i].Capital = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP06.results[i].Capital);
                                    ZakatForm5DataResult.SCH_GP06.results[i].NoOfLabours = Math.Round(Convert.ToDecimal(UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP06.results[i].NoOfLabours))).ToString();


                                    if (ZakatForm5DataResult.SCH_GP06.results[i].GovtContractProf != "0.00")
                                    {
                                        ZakatForm5DataResult.SCH_GP06.results[i].IsGovenmentApplicable = AppResources.FORM5Applicable.ToString();
                                        ZakatForm5DataResult.SCH_GP06.results[i].IsGovenmentApplicableVisible = true;

                                    }
                                    else
                                    {
                                        ZakatForm5DataResult.SCH_GP06.results[i].IsGovenmentApplicable = AppResources.FORM5NotApplicable.ToString();
                                        ZakatForm5DataResult.SCH_GP06.results[i].IsGovenmentApplicableVisible = false;

                                    }

                                    if (ZakatForm5DataResult.SCH_GP06.results[i].CivilContrRev != "0.00")
                                    {
                                        ZakatForm5DataResult.SCH_GP06.results[i].IsCivilProfitApplicable = AppResources.FORM5Applicable.ToString();
                                        ZakatForm5DataResult.SCH_GP06.results[i].IsCivilProfitApplicableVisible = true;

                                    }
                                    else
                                    {
                                        ZakatForm5DataResult.SCH_GP06.results[i].IsCivilProfitApplicable = AppResources.FORM5NotApplicable.ToString();
                                        ZakatForm5DataResult.SCH_GP06.results[i].IsCivilProfitApplicableVisible = false;

                                    }

                                    if (ZakatForm5DataResult.SCH_GP06.results[i].OtherIncome != "0.00")
                                    {
                                        ZakatForm5DataResult.SCH_GP06.results[i].IsOtherProfitApplicable = AppResources.FORM5Applicable.ToString();
                                        ZakatForm5DataResult.SCH_GP06.results[i].IsOtherProfitApplicableVisible = true;

                                    }
                                    else
                                    {
                                        ZakatForm5DataResult.SCH_GP06.results[i].IsOtherProfitApplicable = AppResources.FORM5NotApplicable.ToString();
                                        ZakatForm5DataResult.SCH_GP06.results[i].IsOtherProfitApplicableVisible = false;

                                    }
                                }


                                Contracting = ZakatForm5DataResult.SCH_GP06.results;
                            }
                            //Invst & Real Estate
                            if (ZakatForm5DataResult.SCH_GP07.results.Any())
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


                                for (int i = 0; i < ZakatForm5DataResult.SCH_GP07.results.Count; i++)
                                {

                                    ZakatForm5DataResult.SCH_GP07.results[i].Revenue = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP07.results[i].Revenue);
                                    ZakatForm5DataResult.SCH_GP07.results[i].Expenses = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP07.results[i].Expenses);
                                    ZakatForm5DataResult.SCH_GP07.results[i].NetPft = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP07.results[i].NetPft);

                                    if (ZakatForm5DataResult.SCH_GP07.results[i].City != "")
                                    {
                                        for (int j = 0; j < ZakatForm5CityDataResults.zcitySet.results.Count(); j++)
                                        {
                                            if (ZakatForm5DataResult.SCH_GP07.results[i].City == ZakatForm5CityDataResults.zcitySet.results[j].CityCode)
                                            {
                                                ZakatForm5DataResult.SCH_GP07.results[i].City = ZakatForm5CityDataResults.zcitySet.results[j].CityName;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        ZakatForm5DataResult.SCH_GP07.results[i].City = "-";
                                    }


                                    if (ZakatForm5DataResult.SCH_GP07.results[i].Expenses != "0.00")
                                    {
                                        ZakatForm5DataResult.SCH_GP07.results[i].IsExpencesApplicable = AppResources.FORM5Applicable.ToString();
                                        ZakatForm5DataResult.SCH_GP07.results[i].IsApplicableVisible = true;

                                    }
                                    else
                                    {
                                        ZakatForm5DataResult.SCH_GP07.results[i].IsExpencesApplicable = AppResources.FORM5NotApplicable.ToString();
                                        ZakatForm5DataResult.SCH_GP07.results[i].IsApplicableVisible = false;

                                    }
                                }



                                InvstRealEst = ZakatForm5DataResult.SCH_GP07.results;
                            }
                            //Hotels
                            if (ZakatForm5DataResult.SCH_GP08.results.Any())
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

                                for (int i = 0; i < ZakatForm5DataResult.SCH_GP08.results.Count; i++)
                                {
                                    ZakatForm5DataResult.SCH_GP08.results[i].NoOfRooms = Math.Round(Convert.ToDecimal(UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP08.results[i].NoOfRooms))).ToString();
                                    ZakatForm5DataResult.SCH_GP08.results[i].RoomRate = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP08.results[i].RoomRate);
                                    ZakatForm5DataResult.SCH_GP08.results[i].AddRoomRate = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP08.results[i].AddRoomRate);
                                    ZakatForm5DataResult.SCH_GP08.results[i].ServicFee = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP08.results[i].ServicFee);
                                    ZakatForm5DataResult.SCH_GP08.results[i].OccRate = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP08.results[i].OccRate);
                                    ZakatForm5DataResult.SCH_GP08.results[i].Revenue = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP08.results[i].Revenue);
                                    ZakatForm5DataResult.SCH_GP08.results[i].Expenses = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP08.results[i].Expenses);
                                    ZakatForm5DataResult.SCH_GP08.results[i].NetProfit = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP08.results[i].NetProfit);


                                    if (ZakatForm5DataResult.SCH_GP08.results[i].City != "")
                                    {
                                        for (int j = 0; j < ZakatForm5CityDataResults.zcitySet.results.Count(); j++)
                                        {
                                            if (ZakatForm5DataResult.SCH_GP08.results[i].City == ZakatForm5CityDataResults.zcitySet.results[j].CityCode)
                                            {
                                                ZakatForm5DataResult.SCH_GP08.results[i].City = ZakatForm5CityDataResults.zcitySet.results[j].CityName;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        ZakatForm5DataResult.SCH_GP08.results[i].City = "-";
                                    }

                                    if (ZakatForm5DataResult.SCH_GP08.results[i].Owned == "1")
                                    {
                                        ZakatForm5DataResult.SCH_GP08.results[i].IsOwned = AppResources.ZYes.ToString();
                                    }
                                    //else if(ZakatForm5DataResult.SCH_GP08.results[i].Owned == "2")
                                    //{
                                    //    ZakatForm5DataResult.SCH_GP08.results[i].IsOwned = AppResources.ZNo.ToString();
                                    //}
                                    else
                                    {
                                        // ZakatForm5DataResult.SCH_GP08.results[i].IsOwned = "";
                                        ZakatForm5DataResult.SCH_GP08.results[i].IsOwned = AppResources.ZNo.ToString();
                                    }


                                    if (ZakatForm5DataResult.SCH_GP08.results[i].Expenses != "0.00")
                                    {
                                        ZakatForm5DataResult.SCH_GP08.results[i].IsExpApplicable = AppResources.FORM5Applicable.ToString();
                                        ZakatForm5DataResult.SCH_GP08.results[i].IsApplicableVisible = true;

                                    }
                                    else
                                    {
                                        ZakatForm5DataResult.SCH_GP08.results[i].IsExpApplicable = AppResources.FORM5NotApplicable.ToString();
                                        ZakatForm5DataResult.SCH_GP08.results[i].IsApplicableVisible = false;

                                    }
                                }

                                Hotels = ZakatForm5DataResult.SCH_GP08.results;
                            }
                            //Edu. & Health
                            if (ZakatForm5DataResult.SCH_GP09.results.Any())
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
                                for (int i = 0; i < ZakatForm5DataResult.SCH_GP09.results.Count; i++)
                                {
                                    ZakatForm5DataResult.SCH_GP09.results[i].SubsidyVal = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP09.results[i].SubsidyVal);
                                    ZakatForm5DataResult.SCH_GP09.results[i].Revenue = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP09.results[i].Revenue);
                                    ZakatForm5DataResult.SCH_GP09.results[i].Expenses = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP09.results[i].Expenses);
                                    ZakatForm5DataResult.SCH_GP09.results[i].NetPft = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP09.results[i].NetPft);



                                    if (ZakatForm5DataResult.SCH_GP09.results[i].Expenses != "0.00")
                                    {
                                        ZakatForm5DataResult.SCH_GP09.results[i].IsExpApplicable = AppResources.FORM5Applicable.ToString();
                                        ZakatForm5DataResult.SCH_GP09.results[i].IsApplicableVisible = true;

                                    }
                                    else
                                    {
                                        ZakatForm5DataResult.SCH_GP09.results[i].IsExpApplicable = AppResources.FORM5NotApplicable.ToString();
                                        ZakatForm5DataResult.SCH_GP09.results[i].IsApplicableVisible = false;

                                    }
                                }

                                Edu_Health = ZakatForm5DataResult.SCH_GP09.results;
                            }
                            //Poultry and Fish Farms Activities
                            if (ZakatForm5DataResult.SCH_GP10.results.Any())
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

                                for (int i = 0; i < ZakatForm5DataResult.SCH_GP10.results.Count; i++)
                                {

                                    ZakatForm5DataResult.SCH_GP10.results[i].Capital = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP10.results[i].Capital);
                                    ZakatForm5DataResult.SCH_GP10.results[i].IncrInCap = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP10.results[i].IncrInCap);
                                    ZakatForm5DataResult.SCH_GP10.results[i].CapitalRate = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP10.results[i].CapitalRate);
                                    ZakatForm5DataResult.SCH_GP10.results[i].Profits = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP10.results[i].Profits);



                                    if (ZakatForm5DataResult.SCH_GP10.results[i].IncrInCap != "0.00")
                                    {
                                        ZakatForm5DataResult.SCH_GP10.results[i].IsIncCapApplicable = AppResources.FORM5Applicable.ToString();
                                        ZakatForm5DataResult.SCH_GP10.results[i].IsApplicableVisible = true;

                                    }
                                    else
                                    {
                                        ZakatForm5DataResult.SCH_GP10.results[i].IsIncCapApplicable = AppResources.FORM5NotApplicable.ToString();
                                        ZakatForm5DataResult.SCH_GP10.results[i].IsApplicableVisible = false;

                                    }
                                }

                                Poultry_FishFarm = ZakatForm5DataResult.SCH_GP10.results;
                            }
                            //Cars
                            if (ZakatForm5DataResult.SCH_GP11.results.Any())
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

                                for (int i = 0; i < ZakatForm5DataResult.SCH_GP11.results.Count; i++)
                                {

                                    ZakatForm5DataResult.SCH_GP11.results[i].ShareCap = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP11.results[i].ShareCap);
                                    ZakatForm5DataResult.SCH_GP11.results[i].IntrPurchase = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP11.results[i].IntrPurchase);
                                    ZakatForm5DataResult.SCH_GP11.results[i].ForgPurchase = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP11.results[i].ForgPurchase);
                                    ZakatForm5DataResult.SCH_GP11.results[i].Sales = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP11.results[i].Sales);
                                    ZakatForm5DataResult.SCH_GP11.results[i].PerSalesGain = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP11.results[i].PerSalesGain);

                                }

                                IsCarBtn = true;

                                NoOFEntityList.Add("Cars");
                                Cars = ZakatForm5DataResult.SCH_GP11.results;
                            }
                            //Minerals
                            if (ZakatForm5DataResult.SCH_GP12.results.Any())
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

                                for (int i = 0; i < ZakatForm5DataResult.SCH_GP12.results.Count; i++)
                                {


                                    ZakatForm5DataResult.SCH_GP12.results[i].ShareCap = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP12.results[i].ShareCap);
                                    ZakatForm5DataResult.SCH_GP12.results[i].IntrPurchase = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP12.results[i].IntrPurchase);
                                    ZakatForm5DataResult.SCH_GP12.results[i].ForgPurchase = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP12.results[i].ForgPurchase);
                                    ZakatForm5DataResult.SCH_GP12.results[i].Sales = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP12.results[i].Sales);
                                    ZakatForm5DataResult.SCH_GP12.results[i].PerSalesGain = UtilityManager.GetCommaSeparatedAmount(ZakatForm5DataResult.SCH_GP12.results[i].PerSalesGain);

                                }

                                IsMineralsBtn = true;
                                NoOFEntityList.Add("Minerals");
                                Minerals = ZakatForm5DataResult.SCH_GP12.results;
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


                            ZakatForm5SummaryResult ZakatForm5SummaryDataResult = await WebServiceManager.GAZTZakatForm5DataSummary(ZakatForm5DataResult.Fbnum);

                            if (ZakatForm5SummaryDataResult != null)
                            {

                                //IsZakatEstListVisible = true;
                                int IsZakatFlag = 0;
                                // information
                                ReferenceNumber = ZakatForm5DataResult.Fbnum.ToString();
                                ZakatFromDate = Convert.ToDateTime(ZakatForm5DataResult.AFromDt).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                ZakatToDate = Convert.ToDateTime(ZakatForm5DataResult.AToDt).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                /// use same period property for which is used in Basic Information section.

                                AknowledgementList = ZakatForm5SummaryDataResult.SadadSet.results;

                                if (ZakatForm5SummaryDataResult.SchGP01Set.results.Any())
                                {
                                    //Cabs GP1

                                    CabsSummary = ZakatForm5SummaryDataResult.SchGP01Set.results;
                                    CabsSummaryIsVisible = true;
                                    NoOFZakatList.Add("Cabs");
                                    IsZakatFlag++;

                                    ZakatForm5SummaryDataResult.SchGP01Set.results[0].ActivityProfitTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP01Set.results[0].ActivityProfitTp);
                                    ZakatForm5SummaryDataResult.SchGP01Set.results[0].ActivityProfitRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP01Set.results[0].ActivityProfitRr);

                                    ZakatForm5SummaryDataResult.SchGP01Set.results[0].ZakatBaseTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP01Set.results[0].ZakatBaseTp);
                                    ZakatForm5SummaryDataResult.SchGP01Set.results[0].ZakatBaseRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP01Set.results[0].ZakatBaseRr);



                                }

                                if (ZakatForm5SummaryDataResult.SchGP02Set.results.Any())
                                {
                                    //Professionals
                                    ProfessionalsSummary = ZakatForm5SummaryDataResult.SchGP02Set.results;
                                    ProfessionalsSummaryIsVisible = true;

                                    NoOFZakatList.Add("Professionals");
                                    IsZakatFlag++;

                                    ZakatForm5SummaryDataResult.SchGP02Set.results[0].ActivityProfitTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP02Set.results[0].ActivityProfitTp);
                                    ZakatForm5SummaryDataResult.SchGP02Set.results[0].ActivityProfitRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP02Set.results[0].ActivityProfitRr);

                                    ZakatForm5SummaryDataResult.SchGP02Set.results[0].ZakatBaseTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP02Set.results[0].ZakatBaseTp);
                                    ZakatForm5SummaryDataResult.SchGP02Set.results[0].ZakatBaseRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP02Set.results[0].ZakatBaseRr);

                                }
                                if (ZakatForm5SummaryDataResult.SchGP03Set.results.Any())
                                {
                                    //Sell & Buy
                                    Sell_BuySummary = ZakatForm5SummaryDataResult.SchGP03Set.results;
                                    Sell_BuySummaryIsVisible = true;
                                    NoOFZakatList.Add("SellesAndBuy");
                                    IsZakatFlag++;

                                    ZakatForm5SummaryDataResult.SchGP03Set.results[0].ActivityCapitalTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP03Set.results[0].ActivityCapitalTp);
                                    ZakatForm5SummaryDataResult.SchGP03Set.results[0].ActivityCapitalRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP03Set.results[0].ActivityCapitalRr);

                                    ZakatForm5SummaryDataResult.SchGP03Set.results[0].ExternalImportTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP03Set.results[0].ExternalImportTp);
                                    ZakatForm5SummaryDataResult.SchGP03Set.results[0].ExternalImportRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP03Set.results[0].ExternalImportRr);

                                    ZakatForm5SummaryDataResult.SchGP03Set.results[0].ActivityProfitTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP03Set.results[0].ActivityProfitTp);
                                    ZakatForm5SummaryDataResult.SchGP03Set.results[0].ActivityProfitRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP03Set.results[0].ActivityProfitRr);

                                    ZakatForm5SummaryDataResult.SchGP03Set.results[0].ZakatBaseTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP03Set.results[0].ZakatBaseTp);
                                    ZakatForm5SummaryDataResult.SchGP03Set.results[0].ZakatBaseRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP03Set.results[0].ZakatBaseRr);


                                }
                                if (ZakatForm5SummaryDataResult.SchGP04Set.results.Any())
                                {
                                    //Labour Occup.
                                    LabourOccupSummary = ZakatForm5SummaryDataResult.SchGP04Set.results;
                                    LabourOccupSummaryIsVisble = true;
                                    NoOFZakatList.Add("Labour");
                                    IsZakatFlag++;

                                    ZakatForm5SummaryDataResult.SchGP04Set.results[0].NoLaboursTp = Math.Round(Convert.ToDecimal(UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP04Set.results[0].NoLaboursTp))).ToString();
                                    ZakatForm5SummaryDataResult.SchGP04Set.results[0].NoLaboursRr = Math.Round(Convert.ToDecimal(UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP04Set.results[0].NoLaboursRr))).ToString();

                                    ZakatForm5SummaryDataResult.SchGP04Set.results[0].ActivityProfitTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP04Set.results[0].ActivityProfitTp);
                                    ZakatForm5SummaryDataResult.SchGP04Set.results[0].ActivityProfitRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP04Set.results[0].ActivityProfitRr);

                                    ZakatForm5SummaryDataResult.SchGP04Set.results[0].ZakatBaseTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP04Set.results[0].ZakatBaseTp);
                                    ZakatForm5SummaryDataResult.SchGP04Set.results[0].ZakatBaseRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP04Set.results[0].ZakatBaseRr);

                                }
                                if (ZakatForm5SummaryDataResult.SchGP05Set.results.Any())
                                {
                                    //Industry
                                    IndustrySummary = ZakatForm5SummaryDataResult.SchGP05Set.results;
                                    IndustrySummaryIsVisible = true;
                                    NoOFZakatList.Add("Industry");
                                    IsZakatFlag++;


                                    ZakatForm5SummaryDataResult.SchGP05Set.results[0].ActivityProfitTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP05Set.results[0].ActivityProfitTp);
                                    ZakatForm5SummaryDataResult.SchGP05Set.results[0].ActivityProfitRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP05Set.results[0].ActivityProfitRr);

                                    ZakatForm5SummaryDataResult.SchGP05Set.results[0].ZakatBaseTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP05Set.results[0].ZakatBaseTp);
                                    ZakatForm5SummaryDataResult.SchGP05Set.results[0].ZakatBaseRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP05Set.results[0].ZakatBaseRr);

                                }
                                if (ZakatForm5SummaryDataResult.SchGP06Set.results.Any())
                                {
                                    //Contracting CO.
                                    ContractingSummary = ZakatForm5SummaryDataResult.SchGP06Set.results;
                                    ContractingSummaryIsVisible = true;
                                    NoOFZakatList.Add("Contracting");
                                    IsZakatFlag++;

                                    ZakatForm5SummaryDataResult.SchGP06Set.results[0].GovtContractProfTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP06Set.results[0].GovtContractProfTp);
                                    ZakatForm5SummaryDataResult.SchGP06Set.results[0].GovtContractProfRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP06Set.results[0].GovtContractProfRr);

                                    ZakatForm5SummaryDataResult.SchGP06Set.results[0].CivilContrRevTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP06Set.results[0].CivilContrRevTp);
                                    ZakatForm5SummaryDataResult.SchGP06Set.results[0].CivilContrRevRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP06Set.results[0].CivilContrRevRr);

                                    ZakatForm5SummaryDataResult.SchGP06Set.results[0].NoLaboursTp = Math.Round(Convert.ToDecimal(UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP06Set.results[0].NoLaboursTp))).ToString();
                                    ZakatForm5SummaryDataResult.SchGP06Set.results[0].NoLaboursRr = Math.Round(Convert.ToDecimal(UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP06Set.results[0].NoLaboursRr))).ToString();

                                    ZakatForm5SummaryDataResult.SchGP06Set.results[0].ActivityProfitTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP06Set.results[0].ActivityProfitTp);
                                    ZakatForm5SummaryDataResult.SchGP06Set.results[0].ActivityProfitRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP06Set.results[0].ActivityProfitRr);

                                    ZakatForm5SummaryDataResult.SchGP06Set.results[0].ZakatBaseTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP06Set.results[0].ZakatBaseTp);
                                    ZakatForm5SummaryDataResult.SchGP06Set.results[0].ZakatBaseRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP06Set.results[0].ZakatBaseRr);

                                }
                                if (ZakatForm5SummaryDataResult.SchGP07Set.results.Any())
                                {
                                    //Invst & Real Estate
                                    InvstRealEstSummary = ZakatForm5SummaryDataResult.SchGP07Set.results;
                                    InvstRealEstSummaryIsVisible = true;
                                    NoOFZakatList.Add("Invest");
                                    IsZakatFlag++;

                                    ZakatForm5SummaryDataResult.SchGP07Set.results[0].ActivityProfitTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP07Set.results[0].ActivityProfitTp);
                                    ZakatForm5SummaryDataResult.SchGP07Set.results[0].ActivityProfitRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP07Set.results[0].ActivityProfitRr);

                                    ZakatForm5SummaryDataResult.SchGP07Set.results[0].ZakatBaseTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP07Set.results[0].ZakatBaseTp);
                                    ZakatForm5SummaryDataResult.SchGP07Set.results[0].ZakatBaseRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP07Set.results[0].ZakatBaseRr);

                                }

                                if (ZakatForm5SummaryDataResult.SchGP08Set.results.Any())
                                {
                                    //Hotels
                                    HotelsSummary = ZakatForm5SummaryDataResult.SchGP08Set.results;
                                    HotelsSummaryIsVisible = true;
                                    NoOFZakatList.Add("Hotels");
                                    IsZakatFlag++;

                                    ZakatForm5SummaryDataResult.SchGP08Set.results[0].ActivityProfitTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP08Set.results[0].ActivityProfitTp);
                                    ZakatForm5SummaryDataResult.SchGP08Set.results[0].ActivityProfitRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP08Set.results[0].ActivityProfitRr);

                                    ZakatForm5SummaryDataResult.SchGP08Set.results[0].ZakatBaseTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP08Set.results[0].ZakatBaseTp);
                                    ZakatForm5SummaryDataResult.SchGP08Set.results[0].ZakatBaseRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP08Set.results[0].ZakatBaseRr);

                                }
                                if (ZakatForm5SummaryDataResult.SchGP09Set.results.Any())
                                {
                                    //Edu. & Health
                                    Edu_HealthSummary = ZakatForm5SummaryDataResult.SchGP09Set.results;
                                    Edu_HealthSummaryIsVisible = true;
                                    NoOFZakatList.Add("edu");
                                    IsZakatFlag++;


                                    ZakatForm5SummaryDataResult.SchGP09Set.results[0].ActivityProfitTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP09Set.results[0].ActivityProfitTp);
                                    ZakatForm5SummaryDataResult.SchGP09Set.results[0].ActivityProfitRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP09Set.results[0].ActivityProfitRr);

                                    ZakatForm5SummaryDataResult.SchGP09Set.results[0].ZakatBaseTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP09Set.results[0].ZakatBaseTp);
                                    ZakatForm5SummaryDataResult.SchGP09Set.results[0].ZakatBaseRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP09Set.results[0].ZakatBaseRr);

                                }

                                if (ZakatForm5SummaryDataResult.SchGP10Set.results.Any())
                                {
                                    //Poultry and Fish Farms Activities
                                    Poultry_FishFarmSummary = ZakatForm5SummaryDataResult.SchGP10Set.results;
                                    Poultry_FishFarmSummaryIsVisible = true;
                                    NoOFZakatList.Add("Poultry");
                                    IsZakatFlag++;


                                    ZakatForm5SummaryDataResult.SchGP10Set.results[0].ActivityProfitTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP10Set.results[0].ActivityProfitTp);
                                    ZakatForm5SummaryDataResult.SchGP10Set.results[0].ActivityProfitRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP10Set.results[0].ActivityProfitRr);

                                    ZakatForm5SummaryDataResult.SchGP10Set.results[0].ZakatBaseTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP10Set.results[0].ZakatBaseTp);
                                    ZakatForm5SummaryDataResult.SchGP10Set.results[0].ZakatBaseRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP10Set.results[0].ZakatBaseRr);

                                }

                                if (ZakatForm5SummaryDataResult.SchGP11Set.results.Any())
                                {
                                    //Cars
                                    CarsSummary = ZakatForm5SummaryDataResult.SchGP11Set.results;
                                    CarsSummaryIsVisible = true;
                                    NoOFZakatList.Add("Cars");
                                    IsZakatFlag++;

                                    ZakatForm5SummaryDataResult.SchGP11Set.results[0].ActivityProfitTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP11Set.results[0].ActivityProfitTp);
                                    ZakatForm5SummaryDataResult.SchGP11Set.results[0].ActivityProfitRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP11Set.results[0].ActivityProfitRr);

                                    ZakatForm5SummaryDataResult.SchGP11Set.results[0].ZakatBaseTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP11Set.results[0].ZakatBaseTp);
                                    ZakatForm5SummaryDataResult.SchGP11Set.results[0].ZakatBaseRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP11Set.results[0].ZakatBaseRr);

                                }


                                if (ZakatForm5SummaryDataResult.SchGP12Set.results.Any())
                                {
                                    //Minerls
                                    MineralsSummary = ZakatForm5SummaryDataResult.SchGP12Set.results;
                                    MineralsSummaryIsVisible = true;
                                    NoOFZakatList.Add("Minerls");
                                    IsZakatFlag++;

                                    ZakatForm5SummaryDataResult.SchGP12Set.results[0].ActivityProfitTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP12Set.results[0].ActivityProfitTp);
                                    ZakatForm5SummaryDataResult.SchGP12Set.results[0].ActivityProfitRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP12Set.results[0].ActivityProfitRr);

                                    ZakatForm5SummaryDataResult.SchGP12Set.results[0].ZakatBaseTp = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP12Set.results[0].ZakatBaseTp);
                                    ZakatForm5SummaryDataResult.SchGP12Set.results[0].ZakatBaseRr = UtilityManager.GetCommaSeparatedAmount(ZakatForm5SummaryDataResult.SchGP12Set.results[0].ZakatBaseRr);

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
                catch (Exception e)
                {

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(e.Message, AppResources.Information);
                        _navigationService.GoBack();
                    });
                    IsLoading = false;
                }
            });

            await Task.Run(async() =>
            {
                IsLoading = false;
            });
            //IsLoading = false;
        }

        public async Task PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (App.TP != null)
                        App.TP = null;
                    if (App.PreviousIsArabic)
                    {
                        String langName = "ar-SA";
                        AppResources.Culture = new CultureInfo(langName);
                    }
                    else
                    {
                        String langName = "en-US";
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
                    //_navigationService.NavigateTo(App.SFLoginPageView);
                    //_navigation.NavigationStack.ToList().Clear();

                    _navigationService.NavigateTo(App.SFLoginPageView, App.GAZTNewDesignDashBoardPageView);
                    _navigation.NavigationStack.ToList().Clear();
                });
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
                    currentTab = ZakatForm5TabEnum.ZakatEstimation;
                    if (AknowledgementList.Any())
                    {
                        NextText = AppResources.ZZNext;
                    }
                    else {
                        NextText = AppResources.Form5Finish;

                    }
                    //  OnNextButtonClick = 
                    break;
                case ZakatForm5TabEnum.ZakatEstimation:

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
            DTFormat = new System.Globalization.CultureInfo(DateLangCulture, false).DateTimeFormat;

            /// Set the calendar property of the date time format to the given calendar
            switch (Calendar)
            {
                case "Hijri":
                    DTFormat.Calendar = new System.Globalization.HijriCalendar();
                    break;

                case "Gregorian":
                    DTFormat.Calendar = new System.Globalization.GregorianCalendar();
                    break;

                default:
                    return "";
            }

            /// We format the date structure to whatever we want
            DTFormat.ShortDatePattern = "dd/MM/yyyy";
            return (DateConv.Date.ToString("f", DTFormat));
        }


        #endregion
    }
}
