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


        #endregion

        #region Financial Information


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

        public bool CabsSummaryIsVisible { get;  set; }


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

        public bool ProfessionalsSummaryIsVisible { get;  set; }

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

        public bool Sell_BuySummaryIsVisible { get;  set; }

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

        public bool LabourOccupSummaryIsVisble { get;  set; }

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

        public bool IndustrySummaryIsVisible { get;  set; }

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

        public bool ContractingSummaryIsVisible { get;  set; }

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

        public bool InvstRealEstSummaryIsVisible { get;  set; }

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

        public bool HotelsSummaryIsVisible { get;  set; }

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

        public bool Edu_HealthSummaryIsVisible { get;  set; }

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

        public bool Poultry_FishFarmSummaryIsVisible { get;  set; }

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

        public bool CarsSummaryIsVisible { get;  set; }

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

        public bool MineralsSummaryIsVisible { get;  set; }

        #endregion

        #region Method



        public async Task LoadZakatForm5Data()
        {
            IsLoading = true;
            ZakatForm5DataResult = null;

            try
            {
                try
                {

                    ZakatForm5DataResult ZakatForm5DataResult = await WebServiceManager.GAZTZakatForm5Data();

                    
                    
                    
                    PopToRootPage();// If seesion Expired it will navigate to Dashboard page

                    if (ZakatForm5DataResult != null)
                    {

                        //Bind values to UI

                        //Return Detials
                        FinancialYear = ZakatForm5DataResult.PerslText;

                        // Period = String.Format("{0:ddd, MMM d, yyyy}", ZakatForm5DataResult.AFromDt) + " - " + String.Format("{0:ddd, MMM d, yyyy}", ZakatForm5DataResult.AToDt);
                        Period = Convert.ToDateTime(ZakatForm5DataResult.AFromDt).ToString("dd MMM yyyy", new CultureInfo("en-US")) + " - " + Convert.ToDateTime(ZakatForm5DataResult.AToDt).ToString("dd MMM yyyy", new CultureInfo("en-US"));
                        // Period = String.Format("{dd MMM yyyy}", DateTime.Now.ToString(ZakatForm5DataResult.AFromDt)) + " - " + String.Format("{dd MMM yyyy}", DateTime.Now.ToString(ZakatForm5DataResult.AToDt));
                        //var dates = DateTime.Now.ToString(ZakatForm5DataResult.AFromDt) ;
                        //  var newDAte = Convert.ToDateTime(ZakatForm5DataResult.AFromDt).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));

                        //Taxpayer Detials
                        Taxpayer = ZakatForm5DataResult.ACompNm;
                        Branch = ZakatForm5DataResult.APrctr;
                        Address = //ZakatForm5DataResult.Line0 + ", " +
                            ZakatForm5DataResult.Line1 + ", " +
                            ZakatForm5DataResult.Line2 + ", " +
                            ZakatForm5DataResult.Line3 + ", " + 
                            ZakatForm5DataResult.Line4 + ", " + 
                            ZakatForm5DataResult.Line5 + ", " + 
                            ZakatForm5DataResult.Line6 + ", " +
                            ZakatForm5DataResult.Line7 + ", " +
                            ZakatForm5DataResult.Line8 + ", " +
                            ZakatForm5DataResult.Line9;
                        UserEmail = ZakatForm5DataResult.AEmail;
                        MobileNumber = ZakatForm5DataResult.AMobile;

                        //Registration Information
                        NumberOfOutlet = ZakatForm5DataResult.ANoOfOutlet.ToString();
                        Residency_Status = ZakatForm5DataResult.AResidency;
                        MainOutlet = ZakatForm5DataResult.AMainAct;
                        AccountMethod = ZakatForm5DataResult.AActmethod;
                        FinancialPeriod = ZakatForm5DataResult.AFiscalPeriod;
                        Calendar_Type = ZakatForm5DataResult.AFiscalCalendar;


                        //Financial Information


                        //Cabs
                        if (ZakatForm5DataResult.SCH_GP01.results.Any())
                        {
                            NoOFEntityList.Add("Cabs");
                            for (int i = 0; i < ZakatForm5DataResult.SCH_GP01.results.Count; i++) {
                                if (ZakatForm5DataResult.SCH_GP01.results[i].Expenses != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP01.results[i].IsApplicable = "Applicable";
                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP01.results[i].IsApplicable = "Not Applicable";
                                }
                            }
                        Cabs = ZakatForm5DataResult.SCH_GP01.results;
                        }
                        //Professionals
                        if (ZakatForm5DataResult.SCH_GP02.results.Any())
                        {
                            NoOFEntityList.Add("Professionals");

                            for (int i = 0; i < ZakatForm5DataResult.SCH_GP02.results.Count; i++)
                            {
                                if (ZakatForm5DataResult.SCH_GP02.results[i].Expenses != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP02.results[i].IsApplicable = "Applicable";
                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP02.results[i].IsApplicable = "Not Applicable";
                                }
                            }
                            Professionals = ZakatForm5DataResult.SCH_GP02.results;
                        }
                        //Sell & Buy
                        if (ZakatForm5DataResult.SCH_GP03.results.Any())
                        {
                            NoOFEntityList.Add("Sell & Buy");

                            for (int i = 0; i < ZakatForm5DataResult.SCH_GP03.results.Count; i++)
                            {
                                if (ZakatForm5DataResult.SCH_GP03.results[i].SuppCon != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP03.results[i].IsApplicable = "Applicable";
                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP03.results[i].IsApplicable = "Not Applicable";
                                }

                                if (ZakatForm5DataResult.SCH_GP03.results[i].GenTrade != "0.00" || ZakatForm5DataResult.SCH_GP03.results[i].Livelihoods != "0.00" || ZakatForm5DataResult.SCH_GP03.results[i].LiveStkAnimals != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP03.results[i].IsImport = "Yes";
                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP03.results[i].IsImport = "No";
                                }

                                if (ZakatForm5DataResult.SCH_GP03.results[i].GenTrade != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP03.results[i].Imp_IsGeneralApplicable = "Applicable";
                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP03.results[i].Imp_IsGeneralApplicable = "Not Applicable";
                                }

                                if (ZakatForm5DataResult.SCH_GP03.results[i].Livelihoods != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP03.results[i].Imp_IsLiveLihoodsApplicable = "Applicable";
                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP03.results[i].Imp_IsLiveLihoodsApplicable = "Not Applicable";
                                }

                                if (ZakatForm5DataResult.SCH_GP03.results[i].LiveStkAnimals != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP03.results[i].Imp_IsLivestockApplicable = "Applicable";
                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP03.results[i].Imp_IsLivestockApplicable = "Not Applicable";
                                }

                                if (ZakatForm5DataResult.SCH_GP03.results[i].GenTradeI != "0.00" || ZakatForm5DataResult.SCH_GP03.results[i].LivelihoodsI != "0.00" || ZakatForm5DataResult.SCH_GP03.results[i].LiveStkAnimalsI != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP03.results[i].IsProcurement = "Yes";
                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP03.results[i].IsProcurement = "No";
                                }

                                if (ZakatForm5DataResult.SCH_GP03.results[i].GenTradeI != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP03.results[i].Pro_IsGeneralApplicable = "Applicable";
                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP03.results[i].Pro_IsGeneralApplicable = "Not Applicable";
                                }
                                if (ZakatForm5DataResult.SCH_GP03.results[i].LivelihoodsI != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP03.results[i].Pro_IsLiveLihoodsApplicable = "Applicable";
                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP03.results[i].Pro_IsLiveLihoodsApplicable = "Not Applicable";
                                }
                                if (ZakatForm5DataResult.SCH_GP03.results[i].LiveStkAnimalsI != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP03.results[i].Pro_IsLivestockApplicable = "Applicable";
                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP03.results[i].Pro_IsLivestockApplicable = "Not Applicable";
                                }



                                if (ZakatForm5DataResult.SCH_GP03.results[i].SuppCon != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP03.results[i].IsApplicable = "Applicable";
                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP03.results[i].IsApplicable = "Not Applicable";
                                }
                            }


                            Sell_Buy = ZakatForm5DataResult.SCH_GP03.results;
                        }
                        //Labour Occup.
                        if (ZakatForm5DataResult.SCH_GP04.results.Any())
                        {
                            NoOFEntityList.Add("Labour Occup.");

                            for (int i = 0; i < ZakatForm5DataResult.SCH_GP04.results.Count; i++)
                            {
                                if (ZakatForm5DataResult.SCH_GP04.results[i].Expenses != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP04.results[i].IsExpensesApplicable = "Applicable";
                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP04.results[i].IsExpensesApplicable = "Not Applicable";
                                }
                            }

                            LabourOccup = ZakatForm5DataResult.SCH_GP04.results;
                        }
                        //Industry
                        if (ZakatForm5DataResult.SCH_GP05.results.Any())
                        {
                            NoOFEntityList.Add("Industry");
                            Industry = ZakatForm5DataResult.SCH_GP05.results;
                        }
                       // Contracting CO.
                        if (ZakatForm5DataResult.SCH_GP06.results.Any())
                        {
                            NoOFEntityList.Add("Contracting CO.");

                            for (int i = 0; i < ZakatForm5DataResult.SCH_GP06.results.Count; i++)
                            {
                                if (ZakatForm5DataResult.SCH_GP06.results[i].GovtContractProf != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP06.results[i].IsGovenmentApplicable = "Applicable";
                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP06.results[i].IsGovenmentApplicable = "Not Applicable";
                                }

                                if (ZakatForm5DataResult.SCH_GP06.results[i].CivilContrRev != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP06.results[i].IsCivilProfitApplicable = "Applicable";
                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP06.results[i].IsCivilProfitApplicable = "Not Applicable";
                                }

                                if (ZakatForm5DataResult.SCH_GP06.results[i].OtherIncome != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP06.results[i].IsOtherProfitApplicable = "Applicable";
                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP06.results[i].IsOtherProfitApplicable = "Not Applicable";
                                }
                            }


                            Contracting = ZakatForm5DataResult.SCH_GP06.results;
                        }
                        //Invst & Real Estate
                        if (ZakatForm5DataResult.SCH_GP07.results.Any())
                        {
                            NoOFEntityList.Add("Invst & Real Estate");


                            for (int i = 0; i < ZakatForm5DataResult.SCH_GP07.results.Count; i++)
                            {
                                if (ZakatForm5DataResult.SCH_GP07.results[i].Expenses != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP07.results[i].IsExpencesApplicable = "Applicable";
                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP07.results[i].IsExpencesApplicable = "Not Applicable";
                                }
                            }



                            InvstRealEst = ZakatForm5DataResult.SCH_GP07.results;
                        }
                        //Hotels
                        if (ZakatForm5DataResult.SCH_GP08.results.Any())
                        {
                            NoOFEntityList.Add("Hotels");

                            for (int i = 0; i < ZakatForm5DataResult.SCH_GP08.results.Count; i++)
                            {
                                if (ZakatForm5DataResult.SCH_GP08.results[i].Owned == "1")
                                {
                                    ZakatForm5DataResult.SCH_GP08.results[i].IsOwned = "Yes";
                                }
                                else if(ZakatForm5DataResult.SCH_GP08.results[i].Owned == "2")
                                {
                                    ZakatForm5DataResult.SCH_GP08.results[i].IsOwned = "No";
                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP08.results[i].IsOwned = "";
                                }


                                if (ZakatForm5DataResult.SCH_GP08.results[i].Expenses != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP08.results[i].IsExpApplicable = "Applicable";
                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP08.results[i].IsExpApplicable = "Not Applicable";
                                }
                            }

                            Hotels = ZakatForm5DataResult.SCH_GP08.results;
                        }
                        //Edu. & Health
                        if (ZakatForm5DataResult.SCH_GP09.results.Any())
                        {
                            NoOFEntityList.Add("Edu. & Health");
                            for (int i = 0; i < ZakatForm5DataResult.SCH_GP09.results.Count; i++)
                            {
                                if (ZakatForm5DataResult.SCH_GP09.results[i].Expenses != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP09.results[i].IsExpApplicable = "Applicable";
                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP09.results[i].IsExpApplicable = "Not Applicable";
                                }
                            }

                            Edu_Health = ZakatForm5DataResult.SCH_GP09.results;
                        }
                        //Poultry and Fish Farms Activities
                        if (ZakatForm5DataResult.SCH_GP10.results.Any())
                        {
                            NoOFEntityList.Add("Poultry and Fish Farms Activities");

                            for (int i = 0; i < ZakatForm5DataResult.SCH_GP10.results.Count; i++)
                            {
                                if (ZakatForm5DataResult.SCH_GP10.results[i].IncrInCap != "0.00")
                                {
                                    ZakatForm5DataResult.SCH_GP10.results[i].IsIncCapApplicable = "Applicable";
                                }
                                else
                                {
                                    ZakatForm5DataResult.SCH_GP10.results[i].IsIncCapApplicable = "Not Applicable";
                                }
                            }

                            Poultry_FishFarm = ZakatForm5DataResult.SCH_GP10.results;
                        }
                        //Cars
                        if (ZakatForm5DataResult.SCH_GP11.results.Any())
                        {
                            NoOFEntityList.Add("Cars");
                            Cars = ZakatForm5DataResult.SCH_GP11.results;
                        }
                        //Minerals
                        if (ZakatForm5DataResult.SCH_GP12.results.Any())
                        {
                            NoOFEntityList.Add("Minerals");
                            Minerals = ZakatForm5DataResult.SCH_GP12.results;
                        }
                        //   var AdditionalInfo = ZakatForm5DataResult..Results.ToList();

                        //Additional Information
                        NoOFEntityList.Add("Additional Information");
                        //Share in Persons Companies

                        var OtherCompanyCheck = ZakatForm5DataResult.APlShareChk;
                       
                            if (ZakatForm5DataResult.APlShareChk != "0.00")
                            {
                                ZakatForm5DataResult.IsOtherShareApplicable = "Applicable";
                            }
                            else
                            {
                                ZakatForm5DataResult.IsOtherShareApplicable = "Not Applicable";
                            }
                        
                        OtherCompanyShare = ZakatForm5DataResult.APlShare.ToString();
                        IsOtherComShareApp = ZakatForm5DataResult.IsOtherShareApplicable;
                         ZakatBase = ZakatForm5DataResult.ACapital.ToString();



                        //Declaration
                         NoOfBranch = ZakatForm5DataResult.ANoBranches.ToString();
                         NoofEmp = ZakatForm5DataResult.ANoEmp.ToString();
                         YearRent = ZakatForm5DataResult.AAnnualRent.ToString();
                         TotalAnnualSalary = ZakatForm5DataResult.ATotAnnualSal.ToString();

                        //Zakat Details
                        var Zakatable = ZakatForm5DataResult.AZakat.ToString();
                        var Zakat = ZakatForm5DataResult.AZakat51.ToString();
                        var ZakatPaid = ZakatForm5DataResult.AReleaseOfContract.ToString();
                        var NewTaxAmt = ZakatForm5DataResult.ANetTaxableAmount.ToString();


                        // Zakat Estimation API Call

                        ZakatForm5SummaryResult ZakatForm5SummaryDataResult = await WebServiceManager.GAZTZakatForm5DataSummary();

                        if (ZakatForm5DataResult != null)
                        {

                            // information
                           ReferenceNumber = ZakatForm5SummaryDataResult.Fbnum.ToString();
                            /// use same period property for which is used in Basic Information section.

                            if (ZakatForm5SummaryDataResult.SchGP01Set.results.Any())
                            {
                                //Cabs GP1
                                 CabsSummary = ZakatForm5SummaryDataResult.SchGP01Set.results;
                                CabsSummaryIsVisible = true;

                            }

                            if (ZakatForm5SummaryDataResult.SchGP02Set.results.Any())
                            {
                                //Professionals
                                ProfessionalsSummary = ZakatForm5SummaryDataResult.SchGP02Set.results;
                                ProfessionalsSummaryIsVisible = true;
                            }
                            if (ZakatForm5SummaryDataResult.SchGP03Set.results.Any())
                            {
                                //Sell & Buy
                                Sell_BuySummary = ZakatForm5SummaryDataResult.SchGP03Set.results;
                                Sell_BuySummaryIsVisible = true;

                            }
                            if (ZakatForm5SummaryDataResult.SchGP04Set.results.Any())
                            {
                                //Labour Occup.
                                LabourOccupSummary = ZakatForm5SummaryDataResult.SchGP04Set.results;
                                LabourOccupSummaryIsVisble = true;
                            }
                            if (ZakatForm5SummaryDataResult.SchGP05Set.results.Any())
                            {
                                //Industry
                                IndustrySummary = ZakatForm5SummaryDataResult.SchGP05Set.results;
                                IndustrySummaryIsVisible = true;
                            }
                            if (ZakatForm5SummaryDataResult.SchGP06Set.results.Any())
                            {
                                //Contracting CO.
                                ContractingSummary = ZakatForm5SummaryDataResult.SchGP06Set.results;
                                ContractingSummaryIsVisible = true;
                            }
                            if (ZakatForm5SummaryDataResult.SchGP07Set.results.Any())
                            {
                                //Invst & Real Estate
                                InvstRealEstSummary = ZakatForm5SummaryDataResult.SchGP07Set.results;
                                InvstRealEstSummaryIsVisible = true;
                            }
                          
                            if (ZakatForm5SummaryDataResult.SchGP08Set.results.Any())
                            {
                                //Hotels
                                HotelsSummary = ZakatForm5SummaryDataResult.SchGP08Set.results;
                                HotelsSummaryIsVisible = true;
                            }
                            if (ZakatForm5SummaryDataResult.SchGP09Set.results.Any())
                            {
                                //Edu. & Health
                                Edu_HealthSummary = ZakatForm5SummaryDataResult.SchGP09Set.results;
                                Edu_HealthSummaryIsVisible = true;

                            }
                        
                            if (ZakatForm5SummaryDataResult.SchGP10Set.results.Any())
                            {
                                //Poultry and Fish Farms Activities
                                Poultry_FishFarmSummary = ZakatForm5SummaryDataResult.SchGP10Set.results;
                                Poultry_FishFarmSummaryIsVisible = true;
                            }
                          
                            if (ZakatForm5SummaryDataResult.SchGP11Set.results.Any())
                            {
                                //Cars
                                CarsSummary = ZakatForm5SummaryDataResult.SchGP11Set.results;
                                CarsSummaryIsVisible = true;
                            }
                           
                            
                            if (ZakatForm5SummaryDataResult.SchGP12Set.results.Any())
                            {
                                //Minerls
                                MineralsSummary = ZakatForm5SummaryDataResult.SchGP12Set.results;
                                MineralsSummaryIsVisible = true;
                            }
                            


                        }

                        IsLoading = false;

                    }
                    else
                    {
                        isNoDataLableVisible = true;
                        IsLoading = false;
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
            }
            catch (InternetException ex)
            {

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
                IsLoading = false;
            }
            IsLoading = false;
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
                    _navigationService.NavigateTo(App.SFLoginPageView);
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
                    NextText = "Finish";
                    break;
            }
        }

        private void navigateBack()
        {
            switch (currentTab)
            {
                case ZakatForm5TabEnum.FinancialInformation: 
                    currentTab = ZakatForm5TabEnum.BasicInformation;
                    break;
                case ZakatForm5TabEnum.ZakatEstimation:
                    currentTab = ZakatForm5TabEnum.FinancialInformation;
                    NextText = AppResources.ZZNext;
                    break;
            }
        }
        #endregion
    }
}
