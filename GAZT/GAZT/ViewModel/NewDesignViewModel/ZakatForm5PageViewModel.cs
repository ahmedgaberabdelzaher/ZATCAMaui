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

        #region Basic Information
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

                        if (ZakatForm5DataResult.SCH_GP01.results.Any())
                        {
                            NoOFEntityList.Add("Cabs");
                        Cabs = ZakatForm5DataResult.SCH_GP01.results;
                        }

                        if (ZakatForm5DataResult.SCH_GP01.results.Count() != 0)
                        {
                            NoOFEntityList.Add("Professionals");
                             Professionals = ZakatForm5DataResult.SCH_GP02.results;
                        }
                        if (ZakatForm5DataResult.SCH_GP01.results.Count() != 0)
                        {
                            NoOFEntityList.Add("Sell & Buy");
                             Sell_Buy = ZakatForm5DataResult.SCH_GP03.results;
                        }
                        if (ZakatForm5DataResult.SCH_GP01.results.Count() != 0)
                        {
                            NoOFEntityList.Add("Labour Occup.");
                            LabourOccup = ZakatForm5DataResult.SCH_GP04.results;
                        }
                        if (ZakatForm5DataResult.SCH_GP01.results.Count() != 0)
                        {
                            NoOFEntityList.Add("Industry");
                            Industry = ZakatForm5DataResult.SCH_GP05.results;
                        }
                        if (ZakatForm5DataResult.SCH_GP01.results.Count() != 0)
                        {
                            NoOFEntityList.Add("Contracting CO.");
                            Contracting = ZakatForm5DataResult.SCH_GP06.results;
                        }
                        if (ZakatForm5DataResult.SCH_GP01.results.Count() != 0)
                        {
                            NoOFEntityList.Add("Invst & Real Estate");
                            InvstRealEst = ZakatForm5DataResult.SCH_GP07.results;
                        }
                        if (ZakatForm5DataResult.SCH_GP01.results.Count() != 0)
                        {
                            NoOFEntityList.Add("Hotels");
                            Hotels = ZakatForm5DataResult.SCH_GP08.results;
                        }
                        if (ZakatForm5DataResult.SCH_GP01.results.Count() != 0)
                        {
                            NoOFEntityList.Add("Edu. & Health");
                            Edu_Health = ZakatForm5DataResult.SCH_GP09.results;
                        }
                        if (ZakatForm5DataResult.SCH_GP01.results.Count() != 0)
                        {
                            NoOFEntityList.Add("Poultry and Fish Farms Activities");
                            Poultry_FishFarm = ZakatForm5DataResult.SCH_GP10.results;
                        }
                        if (ZakatForm5DataResult.SCH_GP01.results.Count() != 0)
                        {
                            NoOFEntityList.Add("Cars");
                            Cars = ZakatForm5DataResult.SCH_GP11.results;
                        }
                        if (ZakatForm5DataResult.SCH_GP01.results.Count() != 0)
                        {
                            NoOFEntityList.Add("Minerals");
                            Minerals = ZakatForm5DataResult.SCH_GP12.results;
                        }
                        //   var AdditionalInfo = ZakatForm5DataResult..Results.ToList();

                        //Additional Information
                        NoOFEntityList.Add("Additional Information");
                        //Share in Persons Companies

                        var OtherCompanyCheck = ZakatForm5DataResult.APlShareChk;
                        var OtherCompanyShare = ZakatForm5DataResult.APlShare.ToString();
                        var ZakatBase = ZakatForm5DataResult.ACapital.ToString();



                        //Declaration
                        var NoOfBranch = ZakatForm5DataResult.ANoBranches.ToString();
                        var NoofEmp = ZakatForm5DataResult.ANoEmp.ToString();
                        var YearRent = ZakatForm5DataResult.AAnnualRent.ToString();
                        var TotalAnnualSalary = ZakatForm5DataResult.ATotAnnualSal.ToString();

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
                            var ReferenceNumber = ZakatForm5SummaryDataResult.Fbnum.ToString();
                            /// use same period property for which is used in Basic Information section.

                            if (ZakatForm5SummaryDataResult.SchGP01Set.results.Any())
                            {
                                //Cabs GP1

                                var CabSummary = ZakatForm5SummaryDataResult.SchGP01Set.results;

                            }

                            if (ZakatForm5SummaryDataResult.SchGP01Set.results.Any())
                            {
                                //Professionals
                                var CabSummary = ZakatForm5SummaryDataResult.SchGP02Set.results;
                            }
                            if (ZakatForm5SummaryDataResult.SchGP01Set.results.Any())
                            {
                                //Sell & Buy
                                var CabSummary = ZakatForm5SummaryDataResult.SchGP03Set.results;
                            }
                            if (ZakatForm5SummaryDataResult.SchGP01Set.results.Any())
                            {
                                //Labour Occup.
                                var CabSummary = ZakatForm5SummaryDataResult.SchGP04Set.results;
                            }
                            if (ZakatForm5SummaryDataResult.SchGP01Set.results.Any())
                            {
                                //Industry
                                var CabSummary = ZakatForm5SummaryDataResult.SchGP05Set.results;
                            }
                            if (ZakatForm5SummaryDataResult.SchGP01Set.results.Any())
                            {
                                //Contracting CO.
                                var CabSummary = ZakatForm5SummaryDataResult.SchGP06Set.results;
                            }
                            if (ZakatForm5SummaryDataResult.SchGP01Set.results.Any())
                            {
                                //Invst & Real Estate
                                var CabSummary = ZakatForm5SummaryDataResult.SchGP07Set.results;
                            }
                            if (ZakatForm5SummaryDataResult.SchGP01Set.results.Any())
                            {
                                //Hotels
                                var CabSummary = ZakatForm5SummaryDataResult.SchGP08Set.results;
                            }
                            if (ZakatForm5SummaryDataResult.SchGP01Set.results.Any())
                            {
                                //Edu. & Health
                                var CabSummary = ZakatForm5SummaryDataResult.SchGP09Set.results;
                            }
                            if (ZakatForm5SummaryDataResult.SchGP01Set.results.Any())
                            {
                                //Poultry and Fish Farms Activities
                                var CabSummary = ZakatForm5SummaryDataResult.SchGP10Set.results;
                            }
                            if (ZakatForm5SummaryDataResult.SchGP01Set.results.Any())
                            {
                                //Cars
                                var CabSummary = ZakatForm5SummaryDataResult.SchGP11Set.results;
                            }
                            if (ZakatForm5SummaryDataResult.SchGP01Set.results.Any())
                            {
                                //Minerls
                                var CabSummary = ZakatForm5SummaryDataResult.SchGP12Set.results;
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

                case ZakatForm5TabEnum.FinancialInformation: currentTab = ZakatForm5TabEnum.ZakatEstimation;
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
                    break;
            }
        }
        #endregion
    }
}
