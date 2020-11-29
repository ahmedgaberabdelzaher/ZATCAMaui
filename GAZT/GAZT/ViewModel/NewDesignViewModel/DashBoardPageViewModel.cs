using EGAZT.Models.AccountStatements;
using EGAZT.Models.EnumModels;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Syncfusion.SfCalendar.XForms;
using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    public class GAZTNewDesignDashBoardPageViewModel : BaseViewModel
    {
        #region Variable
        private DashBoardModelTabEnum _currentTab = DashBoardModelTabEnum.DashBoard;
        public DashBoardModelTabEnum currentTab
        {
            get => _currentTab;
            set
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
                //if (_currenrIndex == MaxIndex)
                //{
                //    MarkComplete = true;
                //    RaisePropertyChanged(nameof(MarkComplete));
                //}
            }
        }
        #endregion

        #region Fields

        private ICommand EserviceCommand { get; set; }
        private GAZT.Models.TaxPayerProfile _TaxPayerProfile = App.TP;

        private bool _menuViewVisible = false;
        private bool _homeViewVisible = true;
        private bool _accountStatementVisible = false;
        private bool _liveChatVisible = false;
        private Color _homeIndicatorColor = Color.FromHex("#005e4b");
        private Color _menuIndicatorColor = Color.White;
        private Color _tabbarColor = Color.DarkGray;
        private Color _stackMenuColor = Color.White;
        private string _paidbillCount = string.Empty;
        private string _partiallypaidbillCount = string.Empty;
        private string _unPaidbillCount = string.Empty;
        private string _NextCommitmentsString = AppResources.ZZZZNextCommitments;
        private string _ReturnString = AppResources.NDReturns;
        private string _BillString = AppResources.Bills;
        private string _PaidString = AppResources.Paid;
        private string _PartiallyPaidString = AppResources.Partiallynewui;
        private string _UnPaidString = AppResources.UnPaid;
        private string _TotalString = AppResources.NDTotalNumberOfBills;
        private Dashboard DashboardData = null;
        private CalendarEventCollection _CommittmentsSchedule = null;


        #region Lists
        private List<OverduePaymentAndUnSubmittedReturn> _BillsAndReturnsCommitments = null;
        private List<OverduePaymentAndUnSubmittedReturn> _Bills = null;
        private List<ReturnTypeAndCorrepsondingCount> _SegregatedReturnTypesAndCorrepsondingCounts = null;
        private List<OverduePaymentAndUnSubmittedReturn> _Returns = null;
        private List<eServiceInfo> _eServices = null;
        private List<TaxRelationSetResult> Tax = null;
        private List<TaxRelationSetResult> _taxTypeFilter = null;
        //private List<string> _CommitmentsListFilter = null;
        private List<MyBillsChartModel> _MyBillsChartModels = null;


        public List<OverduePaymentAndUnSubmittedReturn> BillsAndReturnsCommitments
        {
            get
            {
                return this._BillsAndReturnsCommitments;
            }
            set
            {
                if (value != null)
                {
                    this._BillsAndReturnsCommitments = value;
                    RaisePropertyChanged("BillsAndReturnsCommitments");
                }

            }
        }
        public List<TaxRelationSetResult> TaxTypeFilter
        {
            get
            {
                return _taxTypeFilter;
            }
            set
            {
                if (value != null)
                {
                    value = new List<TaxRelationSetResult>(value.OrderBy(temp => temp.DisplayId).ToList());
                    _taxTypeFilter = value;
                    this.RaisePropertyChanged("TaxTypeFilter");
                }
            }
        }
        public List<MyBillsChartModel> MyBillsChartModels
        {
            get
            {
                return this._MyBillsChartModels;
            }
            set
            {
                if (value != null)
                {
                    this._MyBillsChartModels = value;
                    this.RaisePropertyChanged("MyBillsChartModels");
                }
            }
        }
        public List<OverduePaymentAndUnSubmittedReturn> Bills
        {
            get
            {
                return this._Bills;
            }
            set
            {
                if (value != null)
                {
                    this._Bills = value;
                    this.RaisePropertyChanged("Bills");
                }
            }
        }
        public List<ReturnTypeAndCorrepsondingCount> SegregatedReturnTypesAndCorrepsondingCounts
        {
            get
            {
                return this._SegregatedReturnTypesAndCorrepsondingCounts;
            }
            set
            {
                if (value != null)
                {
                    this._SegregatedReturnTypesAndCorrepsondingCounts = value;
                    this.RaisePropertyChanged("SegregatedReturnTypesAndCorrepsondingCounts");
                }
            }
        }
        public List<OverduePaymentAndUnSubmittedReturn> Returns
        {
            get
            {
                return this._Returns;
            }
            set
            {
                if (value != null)
                {
                    this._Returns = value;
                    this.RaisePropertyChanged("Returns");
                }
            }
        }
        public List<eServiceInfo> eServicesAvailableToTheTP
        {
            get
            {
                return this._eServices;
            }
            set
            {
                if (value != null)
                {
                    this._eServices = value;
                    this.RaisePropertyChanged("eServicesAvailableToTheTP");
                }
            }
        }
        public List<string> CommitmentsListFilter
        {
            get
            {
                return new List<string> { AppResources.ZZOverdueCommitments, AppResources.ZZUpcomingCommitments };
            }
            //set
            //{
            //    if (value != null)
            //    {
            //        _CommitmentsListFilter = value;
            //        this.RaisePropertyChanged("CommitmentsListFilter");
            //    }
            //}
        }
        #endregion

        #endregion

        #region Public Properties

        private string _appVersion = App.AppVersion;
        public string AppVersion
        {
            get
            {
                return _appVersion;
            }
            set
            {
                _appVersion = value;
                RaisePropertyChanged("AppVersion");
            }
        }

        public ASStatementHeaderSet _headerSet = null;
        public ASStatementHeaderSet HeaderSet
        {
            get
            {
                return _headerSet;
            }
            set
            {
                _headerSet = value;
                RaisePropertyChanged("HeaderSet");
            }
        }

        public ASTabIdentification _tabIdentification = null;
        public ASTabIdentification TabIdentification
        {
            get
            {
                return _tabIdentification;
            }
            set
            {
                _tabIdentification = value;
                RaisePropertyChanged("TabIdentification");
            }
        }

        private String _taxpayerName;
        public String TaxpayerName
        {
            get
            {
                return _taxpayerName;
            }
            set
            {
                _taxpayerName = value;
                RaisePropertyChanged("TaxpayerName");
            }
        }

        private bool _ifnotRegInVATAndZakat;
        public bool IfnotRegInVATAndZakat
        {
            get
            {
                return _ifnotRegInVATAndZakat;
            }
            set
            {
                _ifnotRegInVATAndZakat = value;
                RaisePropertyChanged("IfnotRegInVATAndZakat");
            }
        }

        private bool _ifRegInZakat;
        public bool IfRegInZakat
        {
            get
            {
                return _ifRegInZakat;
            }
            set
            {
                _ifRegInZakat = value;
                RaisePropertyChanged("IfRegInZakat");
            }
        }

        private bool _ifRegInVAT;
        public bool IfRegInVAT
        {
            get
            {
                return _ifRegInVAT;
            }
            set
            {
                _ifRegInVAT = value;
                RaisePropertyChanged("IfRegInVAT");
            }
        }

        private bool _ifSignUpnNotRegInVAT;
        public bool IfSignUpnNotRegInVAT
        {
            get
            {
                return _ifSignUpnNotRegInVAT;
            }
            set
            {
                _ifSignUpnNotRegInVAT = value;
                RaisePropertyChanged("IfSignUpnNotRegInVAT");
            }
        }

        private bool _ifSignUpnNotRegInVATShowVATServie;
        public bool IfSignUpnNotRegInVATShowVATServie
        {
            get
            {
                return _ifSignUpnNotRegInVATShowVATServie;
            }
            set
            {
                _ifSignUpnNotRegInVATShowVATServie = value;
                RaisePropertyChanged("IfSignUpnNotRegInVATShowVATServie");
            }
        }

        public CalendarEventCollection CommittmentsSchedule
        {
            get
            {
                return this._CommittmentsSchedule;
            }
            set
            {
                if (this._CommittmentsSchedule == value)
                {
                    return;
                }
                this._CommittmentsSchedule = value;
                this.RaisePropertyChanged("CommittmentsSchedule");
            }
        }

        private double _debitAmountEndProgressBar = 0;
        public double DebitAmountEndProgressBar
        {
            get => _debitAmountEndProgressBar;
            set
            {
                _debitAmountEndProgressBar = value;
                RaisePropertyChanged(nameof(DebitAmountEndProgressBar));
            }
        }

        private double _creditAmountStartProgressBar = 0;
        public double CreditAmountStartProgressBar
        {
            get => _creditAmountStartProgressBar;
            set
            {
                _creditAmountStartProgressBar = value;
                RaisePropertyChanged(nameof(CreditAmountStartProgressBar));
            }
        }

        private double _totalAmountProgressBar = 0;
        public double TotalAmountProgressBar
        {
            get => _totalAmountProgressBar;
            set
            {
                _creditAmountStartProgressBar = value;
                RaisePropertyChanged(nameof(_totalAmountProgressBar));
            }
        }


        public TaxRelationSetResult _selectedTaxTypeForFilterValue = null;
        public TaxRelationSetResult SelectedTaxTypeForFilterValue
        {
            get
            {
                return _selectedTaxTypeForFilterValue;
            }
            set
            {

                _selectedTaxTypeForFilterValue = value;
                PopulateStatements(_selectedTaxTypeForFilterValue.TaxType, _selectedTaxTypeForFilterValue.StatementFilter, string.Empty);

                RaisePropertyChanged("SelectedTaxTypeForFilterValue");
            }
        }

        public TaxRelationSetResult _SelectedTaxTypeForFilter = null;
        public TaxRelationSetResult SelectedTaxTypeForFilter
        {
            get
            {
                return _SelectedTaxTypeForFilter;
            }
            set
            {

                _SelectedTaxTypeForFilter = value;
                PopulateStatements(_SelectedTaxTypeForFilter.TaxType, _SelectedTaxTypeForFilter.StatementFilter, string.Empty);

                RaisePropertyChanged("SelectedTaxTypeForFilter");
            }
        }

        private ChartColorCollection _colors = null;
        public ChartColorCollection Colors
        {
            get
            {
                return _colors;
            }
            set
            {
                _colors = value;
                RaisePropertyChanged("Colors");
            }
        }

        private string _welcomeText;
        public string WelcomeText
        {
            get
            {
                return _welcomeText;
            }
            set
            {
                _welcomeText = value;
                RaisePropertyChanged("WelcomeText");
            }
        }

        private int _rotation = 0;
        public int Rotation
        {
            get
            {
                return _rotation;
            }
            set
            {
                _rotation = value;
                RaisePropertyChanged("Rotation");
            }
        }

        private string _translateText;
        public string TranslateText
        {
            get
            {
                return _translateText;
            }
            set
            {
                _translateText = value;
                RaisePropertyChanged("TranslateText");
            }
        }


        private string _billCount;
        public string BillCount
        {
            get
            {
                return this._billCount;
            }
            set
            {
                this._billCount = value;
                this.RaisePropertyChanged("BillCount");
            }
        }

        public string PaidBillCount
        {
            get
            {
                return this._paidbillCount;
            }
            set
            {
                this._paidbillCount = value;
                this.RaisePropertyChanged("PaidBillCount");
            }
        }

        public string PartiallyPaidBillCount
        {
            get
            {
                return this._partiallypaidbillCount;
            }
            set
            {
                this._partiallypaidbillCount = value;
                this.RaisePropertyChanged("PartiallyPaidBillCount");
            }
        }

        public string UnPaidBillCount
        {
            get
            {
                return this._unPaidbillCount;
            }
            set
            {
                this._unPaidbillCount = value;
                this.RaisePropertyChanged("UnPaidBillCount");
            }
        }

        private string _nDCommitments = AppResources.NDCommitments;
        public string NDCommitments
        {
            get
            {
                return this._nDCommitments;
            }
            set
            {
                this._nDCommitments = value;
                this.RaisePropertyChanged("NDCommitments");
            }
        }

        private string _zBills = AppResources.Bills;
        public string ZBills
        {
            get
            {
                return this._zBills;
            }
            set
            {
                this._zBills = value;
                this.RaisePropertyChanged("ZBills");
            }
        }

        private string _returns = AppResources.Returns;
        public string Return
        {
            get
            {
                return this._returns;
            }
            set
            {
                this._returns = value;
                this.RaisePropertyChanged("Return");
            }
        }

        private string _aboutUs = AppResources.ZZZAboutUs;
        public string AboutUs
        {
            get
            {
                return this._aboutUs;
            }
            set
            {
                this._aboutUs = value;
                this.RaisePropertyChanged("AboutUs");
            }
        }

        private string _contactus = AppResources.ZZZContactus;
        public string Contactus
        {
            get
            {
                return this._contactus;
            }
            set
            {
                this._contactus = value;
                this.RaisePropertyChanged("Contactus");
            }
        }

        private bool _setMyCommitmentsCollectionVisibility = false;
        public bool SetMyCommitmentsCollectionVisibility
        {
            get
            {
                return this._setMyCommitmentsCollectionVisibility;
            }
            set
            {
                this._setMyCommitmentsCollectionVisibility = value;
                this.RaisePropertyChanged("SetMyCommitmentsCollectionVisibility");
            }
        }

        private bool _setNoCommitmentsAvailableLabelVisibility = true;
        public bool SetNoCommitmentsAvailableLabelVisibility
        {
            get
            {
                return this._setNoCommitmentsAvailableLabelVisibility;
            }
            set
            {
                this._setNoCommitmentsAvailableLabelVisibility = value;
                this.RaisePropertyChanged("SetNoCommitmentsAvailableLabelVisibility");
            }
        }

        private string _privacyandPolicy = AppResources.ZZZPrivacyandPolicy;
        public string PrivacyandPolicy
        {
            get
            {
                return this._privacyandPolicy;
            }
            set
            {
                this._privacyandPolicy = value;
                this.RaisePropertyChanged("PrivacyandPolicy");
            }
        }

        private string _logout = AppResources.ZLogout;
        public string Logout
        {
            get
            {
                return this._logout;
            }
            set
            {
                this._logout = value;
                this.RaisePropertyChanged("Logout");
            }
        }

        
        public GAZT.Models.TaxPayerProfile TaxPayerProfile
        {
            get
            {
                return this._TaxPayerProfile;
            }
            set
            {
                this._TaxPayerProfile = value;
                this.RaisePropertyChanged("TaxPayerProfile");
            }
        }
        private bool _isVatRegistrationTileVisible = false;
        public bool IsVatRegistrationTileVisible
        {
            get
            {
                IsVatAmendmentTileVisible = !_isVatRegistrationTileVisible;
                return _isVatRegistrationTileVisible;
            }
            set
            {
                _isVatRegistrationTileVisible = value;
                RaisePropertyChanged("IsVatRegistrationTileVisible");
            }
        }

        private bool _isRegistrationDetailsTileVisible = true;
        public bool IsRegistrationDetailsTileVisible
        {
            get
            {
                return _isRegistrationDetailsTileVisible;
            }
            set
            {
                _isRegistrationDetailsTileVisible = value;
                RaisePropertyChanged("IsRegistrationDetailsTileVisible");
            }
        }

        private bool _isVatAmendmentTileVisible = false;
        public bool IsVatAmendmentTileVisible
        {
            get
            {
                return _isVatAmendmentTileVisible;
            }
            set
            {
                _isVatAmendmentTileVisible = value;
                RaisePropertyChanged("IsVatAmendmentTileVisible");
            }
        }

        private bool _isEstablishmentRegistrationTileVisible = false;
        public bool IsEstablishmentRegistrationTileVisible
        {
            get
            {
                return _isEstablishmentRegistrationTileVisible;
            }
            set
            {
                _isEstablishmentRegistrationTileVisible = value;
                RaisePropertyChanged("IsEstablishmentRegistrationTileVisible");
            }
        }
        public bool MenuViewVisible
        {
            get
            {
                return _menuViewVisible;
            }
            set
            {
                this._menuViewVisible = value;
                this.RaisePropertyChanged("MenuViewVisible");
            }
        }
        public bool HomeViewVisible
        {
            get
            {
                return _homeViewVisible;
            }
            set
            {
                this._homeViewVisible = value;
                if (_homeViewVisible != null)
                {
                    if (_homeViewVisible)
                    {
                        _homeIndicatorColor = Color.FromHex("#005e4b");
                        MenuIndicatorColor = Color.White;
                    }

                }
                this.RaisePropertyChanged("HomeViewVisible");
            }
        }

        public bool AccountStatementVisible
        {
            get
            {
                return _accountStatementVisible;
            }
            set
            {
                this._accountStatementVisible = value;
                if (_accountStatementVisible != null)
                {
                    if (_accountStatementVisible)
                    {
                        _homeIndicatorColor = Color.FromHex("#005e4b");
                        MenuIndicatorColor = Color.White;
                    }

                }
                this.RaisePropertyChanged("AccountStatementVisible");
            }
        }


        public bool LiveChatVisible
        {
            get
            {
                return _liveChatVisible;
            }
            set
            {
                this._liveChatVisible = value;
                if (_liveChatVisible != null)
                {
                    if (_liveChatVisible)
                    {
                        _homeIndicatorColor = Color.FromHex("#005e4b");
                        MenuIndicatorColor = Color.White;
                    }

                }
                this.RaisePropertyChanged("LiveChatVisible");
            }
        }

        public Color HomeIndicatorColor
        {
            get
            {
                return _homeIndicatorColor;
            }
            set
            {
                this._homeIndicatorColor = value;
                this.RaisePropertyChanged("HomeIndicatorColor");
            }
        }
        public Color MenuIndicatorColor
        {
            get
            {
                return _menuIndicatorColor;
            }
            set
            {
                this._menuIndicatorColor = value;
                this.RaisePropertyChanged("MenuIndicatorColor");
            }
        }
        public Color TabbarColor
        {
            get
            {
                return _tabbarColor;
            }
            set
            {
                this._tabbarColor = value;
                this.RaisePropertyChanged("TabbarColor");
            }
        }
        public Color StackMenuColor
        {
            get
            {
                return _stackMenuColor;
            }
            set
            {
                this._stackMenuColor = value;
                this.RaisePropertyChanged("StackMenuColor");
            }
        }
        public string NextCommitmentsString
        {
            get
            {
                return _NextCommitmentsString;
            }
            set
            {
                this._NextCommitmentsString = value;
                this.RaisePropertyChanged("NextCommitmentsString");
            }
        }
        public string ReturnString
        {
            get
            {
                return _ReturnString;
            }
            set
            {
                this._ReturnString = value;
                this.RaisePropertyChanged("ReturnString");
            }
        }
        public string PaidString
        {
            get
            {
                return _PaidString;
            }
            set
            {
                this._PaidString = value;
                this.RaisePropertyChanged("PaidString");
            }
        }
        public string PartiallyPaidString
        {
            get
            {
                return _PartiallyPaidString;
            }
            set
            {
                this._PartiallyPaidString = value;
                this.RaisePropertyChanged("PartiallyPaidString");
            }
        }
        public string UnPaidString
        {
            get
            {
                return _UnPaidString;
            }
            set
            {
                this._UnPaidString = value;
                this.RaisePropertyChanged("UnPaidString");
            }
        }
        public string BillString
        {
            get
            {
                return _BillString;
            }
            set
            {
                this._BillString = value;
                this.RaisePropertyChanged("BillString");
            }
        }
        public string TotalString
        {
            get
            {
                return _TotalString;
            }
            set
            {
                this._TotalString = value;
                this.RaisePropertyChanged("TotalString");
            }
        }

        private string _accStmtnCreditAmount { get; set; }
        public string AccStmtnCreditAmount
        {
            get
            {
                return _accStmtnCreditAmount;
            }
            set
            {
                _accStmtnCreditAmount = value;
                RaisePropertyChanged("AccStmtnCreditAmount");
            }
        }
        private string _selectedCommitmentFilterValue = null;

        public string SelectedCommitmentFilterValue
        {
            get
            {
                return _selectedCommitmentFilterValue;
            }
            set
            {

                if (!string.IsNullOrEmpty(value))
                {
                    _selectedCommitmentFilterValue = value;
                    PopualateCommittmentsInformation();
                    RaisePropertyChanged("SelectedCommitmentFilterValue");

                }

            }
        }

        private string _SelectedCommitmentFilterLabelValue = null;

        public string SelectedCommitmentFilterLabelValue
        {
            get
            {
                return _SelectedCommitmentFilterLabelValue;
            }
            set
            {

                if (!string.IsNullOrEmpty(value))
                {
                    _SelectedCommitmentFilterLabelValue = value;
                    RaisePropertyChanged("SelectedCommitmentFilterLabelValue");

                }

            }
        }

        
        #endregion

        #region Constructor
        public GAZTNewDesignDashBoardPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            MenuViewVisible = false;
            LiveChatVisible = false;
            AccountStatementVisible = false;
            TaxpayerName = string.Empty;
            HomeViewVisible = true;
            IsVatRegistrationTileVisible = false;
            if (App.TP != null)
                 TaxPayerProfile = App.TP;

            if (App.IsArabic)
            {
                //  TranslateText = AppResources.ZZZSetLanguageText; ;
                 TranslateText = AppResources.ZZZChangetoLanguage;
            }
            else
            {
                //  TranslateText = AppResources.ZZZSetLanguageText;
                 TranslateText = AppResources.ZZZChangetoLanguage;
            }

             MenuViewVisible = false;
             HomeViewVisible = true;


        }
        #endregion

        #region Method
        public async Task LoadDashboardData()
        {
            //CommitmentsListFilter = new List<string> { AppResources.ZZOverdueCommitments, AppResources.ZZUpcomingCommitments };

            if (App.TP != null)
            {
                if (App.TP.TypeChk == "X")
                {
                    TaxpayerName = AppResources.Hello + " " + App.TP.NameFirst + " " + App.TP.NameLast;
                }
                else
                {
                    TaxpayerName = AppResources.Hello + " " + App.TP.NameOrg1;
                }

                
                    DashboardData = WebServiceManager.GAZTGetDashboardData(UtilityManager.GetLanguageParameter(), App.TP.Userid);
                

                    var temp1 = new List<OverduePaymentAndUnSubmittedReturn>();
                    List<OverduePaymentAndUnSubmittedReturn> TempBills = await WebServiceManager.GAZTGetPaymentOverdueSetForDashboardData(App.IsArabic ? "A" : "E", App.TP.Userid);

                    foreach (OverduePaymentAndUnSubmittedReturn ee in TempBills)
                    {
                        temp1.Add(ee);
                    }

                    Bills = temp1;
                    //Bills = new List<OverduePaymentAndUnSubmittedReturn>((IEnumerable<OverduePaymentAndUnSubmittedReturn>)TempBills);
                    System.Diagnostics.Debug.WriteLine("Bills " + Bills.Count);

                    var temp2 = new List<OverduePaymentAndUnSubmittedReturn>();
                    List<OverduePaymentAndUnSubmittedReturn> TempReturns = await WebServiceManager.GAZTGetUnSubmittedReturnSetForDashboardData(App.IsArabic ? "A" : "E", App.TP.Userid);
                    foreach (OverduePaymentAndUnSubmittedReturn ee in TempReturns)
                    {
                        temp2.Add(ee);
                    }
                    Returns = temp2;

                    //Returns = new List<OverduePaymentAndUnSubmittedReturn>((IEnumerable<OverduePaymentAndUnSubmittedReturn>)TempReturns);
                    System.Diagnostics.Debug.WriteLine("Returns " + Returns.Count);
                    TabIdentification = await WebServiceManager.GAZTGetAccountStatementsTabIdentification();
                    HeaderSet = await WebServiceManager.GAZTGetAccountStatementHeaderSet(string.Empty, string.Empty, string.Empty);

                    foreach (TaxRelationSetResult taxRelationSetResult in HeaderSet.D.TaxRelationSet.Results)
                    {
                        if (TabIdentification.D?.Direct == "X")
                        {
                            if (taxRelationSetResult.StatementFilter == "01")
                            {
                                taxRelationSetResult.DisplayId = 01;
                            }

                            if (taxRelationSetResult.StatementFilter == "02")
                            {
                                taxRelationSetResult.DisplayId = 02;
                            }

                            if (taxRelationSetResult.StatementFilter == "03")
                            {
                                taxRelationSetResult.DisplayId = 03;
                            }
                        }

                        if (TabIdentification.D?.Indirect == "X")
                        {
                            if (taxRelationSetResult.StatementFilter == "06")
                            {
                                taxRelationSetResult.DisplayId = 06;
                            }

                            if (taxRelationSetResult.StatementFilter == "07")
                            {
                                taxRelationSetResult.DisplayId = 07;
                            }

                            if (taxRelationSetResult.StatementFilter == "09")
                            {
                                taxRelationSetResult.DisplayId = 09;
                            }
                        }
                    }

                        TaxTypeFilter = new List<TaxRelationSetResult>();

                    TaxTypeFilter = new List<TaxRelationSetResult>(HeaderSet.D.TaxRelationSet.Results.Where(temp => temp.DisplayId == 01 || temp.DisplayId == 02 || temp.DisplayId == 03 || temp.DisplayId == 06 || temp.DisplayId == 07 || temp.DisplayId == 09).ToList());
                if(TaxTypeFilter!=null && TaxTypeFilter.Count>0)
                SelectedTaxTypeForFilterValue = TaxTypeFilter.FirstOrDefault();

                    //foreach (TaxRelationSetResult aSReturnTypes in TaxTypeFilter)
                    //{
                    //    if (HeaderSet.D.TaxType == aSReturnTypes.TaxType)
                    //    {
                    //        SelectedTaxTypeForFilter = aSReturnTypes;
                    //    }
                    //}

                    double tempEndProgressBar = (Convert.ToDouble(HeaderSet.D.DebitAmount));
                    double startCreditProgressBar = (Convert.ToDouble(HeaderSet.D.CreditAmount.Replace("-", string.Empty)));
                    double totalBalance = tempEndProgressBar + startCreditProgressBar;

                    AccStmtnCreditAmount = HeaderSet.D.CreditAmount.Replace("-", string.Empty);

                    DebitAmountEndProgressBar = (tempEndProgressBar / totalBalance) * 100;
                    CreditAmountStartProgressBar = (startCreditProgressBar / totalBalance) * 100;

                    TotalAmountProgressBar = DebitAmountEndProgressBar + CreditAmountStartProgressBar;
                    MessagingCenter.Send<Object>(this, "UpdateProgressBar");
            }
            try
            {
                //await Task.Delay(2000);
                //PopualateCommittmentsInformation();
            }
            catch (AggregateException ae)
            {
                IsLoading = false;
                foreach (var gex in ae.InnerExceptions)
                {
                    // Handle the GAZT custom exception.
                    if (gex is GAZTException)
                    {
                        string MessageForTheUser = gex.Message;
                        if (gex is GAZTNetworkConnectivityIssueException)
                        {
                            MessageForTheUser = AppResources.NetworkConnectivityIssue;
                        }
                        else if (gex is GAZTInternetException)
                        {
                            MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                        }
                        else if (gex is GAZTSessionExpiredException)
                        {
                            MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                        }
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            if (MessageForTheUser == AppResources.ZZInternetConnectionMessage)
                            {
                                await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                _navigationService.GoBack();
                            }
                            else if (MessageForTheUser == AppResources.NetworkConnectivityIssue)
                            {
                                await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                _navigationService.GoBack();
                            }
                            else if (MessageForTheUser == AppResources.ZYourSessionhasexpiredPleaseLoginagain)
                            {
                                PopToRootPage();
                            }
                        });
                    }
                    // Rethrow any other exception.
                    else
                    {
                        throw;
                    }
                }
            }
            catch (GAZTSessionExpiredException)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZYourSessionhasexpiredPleaseLoginagain, AppResources.Information);
                    PopToRootPage();
                });
            }
            catch (Exception ex)
            {
                IsLoading = false;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    //PopToRootPage();
                });
            }
                IsLoading = false;
            SelectedCommitmentFilterLabelValue = AppResources.ZZOverdueCommitments;
        }


        public void PopualateCommittmentsInformation()
        {
            try
            {

                //if (BillsAndReturnsCommitments == null)
                //    BillsAndReturnsCommitments = new List<OverduePaymentAndUnSubmittedReturn>();

                var BillsAndReturnsCommitmentsTemp = new List<OverduePaymentAndUnSubmittedReturn>();
                // Create events
                foreach (var Bill in Bills)
                {
                    Bill.IsUnSubmittedReturn = false;
                    Bill.IsPaymentOverdue = true;
                    Bill.ColorCode = Color.FromHex("#AA0C19");

                    BillsAndReturnsCommitmentsTemp.Add(Bill);
                }
                foreach (var UnsubmittedReturn in Returns)
                {
                    UnsubmittedReturn.IsUnSubmittedReturn = true;
                    UnsubmittedReturn.IsPaymentOverdue = false;
                    UnsubmittedReturn.ColorCode = Color.FromHex("#5D6770");
                    BillsAndReturnsCommitmentsTemp.Add(UnsubmittedReturn);
                }
                
                if (BillsAndReturnsCommitments != null)
                {
                    DateTime Today = DateTime.Now;
                    var BillsAndReturnsCommitmentsLocal = new List<OverduePaymentAndUnSubmittedReturn>();
                    var BillsAndReturnsCommitmentsOverdurItems = BillsAndReturnsCommitmentsTemp.Where(a => a.DueDateDateTime.Date >= Today.Date).ToList();
                    try
                    {
                            /*foreach (var item in CommitmentsListFilter)
                            {*/

                            if (SelectedCommitmentFilterValue.Equals(AppResources.ZZOverdueCommitments))
                            {
                                BillsAndReturnsCommitmentsOverdurItems = BillsAndReturnsCommitmentsTemp.Where(a => DateTime.Compare(a.DueDateDateTime, Today) <= 0).ToList();
                                
                            }
                            else if (SelectedCommitmentFilterValue.Equals(AppResources.ZZUpcomingCommitments))
                            {
                                BillsAndReturnsCommitmentsOverdurItems = BillsAndReturnsCommitmentsTemp.Where(a => DateTime.Compare(a.DueDateDateTime, Today) > 0).ToList();

                            }
                            // }

                        if (BillsAndReturnsCommitmentsOverdurItems != null && BillsAndReturnsCommitmentsOverdurItems.Count > 0)
                        {
                            foreach (OverduePaymentAndUnSubmittedReturn temp in BillsAndReturnsCommitmentsOverdurItems)
                            {
                                BillsAndReturnsCommitmentsLocal.Add(temp);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    if (SelectedCommitmentFilterValue.Equals(AppResources.ZZUpcomingCommitments))
                    {
                        if (BillsAndReturnsCommitmentsLocal.Count > 3)
                        {
                            if (BillsAndReturnsCommitmentsLocal.Count == 4)
                            {
                                BillsAndReturnsCommitmentsLocal = BillsAndReturnsCommitmentsLocal.OrderBy(a => a.DueDateDateTime).Take(4).ToList();
                            }
                            else
                            {
                                BillsAndReturnsCommitmentsLocal = BillsAndReturnsCommitmentsLocal.OrderBy(a => a.DueDateDateTime).Take(5).ToList();
                            }
                            if (BillsAndReturnsCommitmentsLocal.Count > 3)
                            {
                                if (BillsAndReturnsCommitmentsLocal[2].DueDateDateTime.Date == BillsAndReturnsCommitmentsLocal[3].DueDateDateTime.Date)
                                {
                                    if (BillsAndReturnsCommitmentsLocal.Count > 4)
                                    {
                                        if (BillsAndReturnsCommitmentsLocal[2].DueDateDateTime.Date == BillsAndReturnsCommitmentsLocal[4].DueDateDateTime.Date)
                                        {
                                        }
                                        else
                                        {
                                            BillsAndReturnsCommitmentsLocal.RemoveAt(4);
                                        }
                                    }
                                }
                                else
                                {
                                    if (BillsAndReturnsCommitmentsLocal.Count > 3)
                                    {
                                        BillsAndReturnsCommitmentsLocal.RemoveAt(3);
                                    }
                                    if (BillsAndReturnsCommitmentsLocal.Count > 3)
                                    {
                                        BillsAndReturnsCommitmentsLocal.RemoveAt(3);
                                    }
                                }
                            }
                        }
                    }

                    BillsAndReturnsCommitmentsLocal = BillsAndReturnsCommitmentsLocal.OrderBy(i => DateTime.Parse(i.DueDate)).ToList();
                    BillsAndReturnsCommitmentsTemp.Clear();
                    //foreach (var item in BillsAndReturnsCommitmentsLocal)
                    //{

                    //    BillsAndReturnsCommitmentsTemp.Add(item);
                    //}
                    BillsAndReturnsCommitments = BillsAndReturnsCommitmentsLocal;
                   
                    // BillsAndReturnsCommitments = new List<OverduePaymentAndUnSubmittedReturn>((IEnumerable<OverduePaymentAndUnSubmittedReturn>)BillsAndReturnsCommitmentsLocal);
                    if (BillsAndReturnsCommitments != null && BillsAndReturnsCommitments.Count > 0)
                    {
                        SetNoCommitmentsAvailableLabelVisibility = false;
                        SetMyCommitmentsCollectionVisibility = true;
                    }
                    else
                    {
                        SetNoCommitmentsAvailableLabelVisibility = true;
                        SetMyCommitmentsCollectionVisibility = false;
                    }
                }
            }
            catch (AggregateException ae)
            {
                IsLoading = false;
                foreach (var gex in ae.InnerExceptions)
                {
                    // Handle the GAZT custom exception.
                    if (gex is GAZTException)
                    {
                        string MessageForTheUser = gex.Message;
                        if (gex is GAZTNetworkConnectivityIssueException)
                        {
                            MessageForTheUser = AppResources.NetworkConnectivityIssue;
                        }
                        else if (gex is GAZTInternetException)
                        {
                            MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                        }
                        else if (gex is GAZTSessionExpiredException)
                        {
                            MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                        }
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                        });
                    }
                    // Rethrow any other exception.
                    else
                    {
                        throw;
                    }
                }
            }
            catch (GAZTSessionExpiredException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZYourSessionhasexpiredPleaseLoginagain, AppResources.Information);
                    PopToRootPage();
                });
            }
            catch (Exception ex)
            {
                IsLoading = false;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    //PopToRootPage();
                });
            }
        }
        public class BillTypeCorrepsondingCountAndAmount
        {
            public BillType Status;
            public String BillTypeName;
            public int BillCount;
            public String BillAmount;
        }
        public void PopulateBillsInformation()
        {
            List<BillTypeCorrepsondingCountAndAmount> SegregatedBillTypeCorrepsondingCountAndAmount = new List<BillTypeCorrepsondingCountAndAmount>();
            ChartColorCollection ColorsChild = new ChartColorCollection();
            int iBillsCount = 0;
            BillCount = Convert.ToInt32(iBillsCount).ToString();

            try
            {
                if (DashboardData.results != null && DashboardData.results.Count > 0)
                {
                    //Paid Bills
                    //if (DashboardData.results[0] != null && DashboardData.results[0].PbillsTot != null)
                    //{
                    //    BillTypeCorrepsondingCountAndAmount PaidBillCountAndAmount = new BillTypeCorrepsondingCountAndAmount();

                    //    PaidBillCountAndAmount.Status = BillType.PbillsTot;

                    //    String PaidBillsstr = DashboardData.results[0].PbillsTot.TrimStart(new Char[] { '0' });
                    //    String PaidBillsAmountstr = DashboardData.results[0].PbillsBetrw.TrimStart(new Char[] { '0' });

                    //    if (string.IsNullOrEmpty(PaidBillsstr))
                    //    {
                    //        PaidBillsstr = "0";
                    //    }
                    //    else if (PaidBillsstr.Substring(0, 1) == ".")
                    //    {
                    //        PaidBillsstr = "0" + PaidBillsstr;
                    //    }
                    //    if (string.IsNullOrEmpty(PaidBillsAmountstr))
                    //    {
                    //        PaidBillsAmountstr = "0";
                    //    }
                    //    else if (PaidBillsAmountstr.Substring(0, 1) == ".")
                    //    {
                    //        PaidBillsAmountstr = "0" + PaidBillsAmountstr;
                    //    }
                    //    string TotalPaidAmount = PaidBillsAmountstr;
                    //    try
                    //    {


                    //        PaidBillCountAndAmount.BillCount = Convert.ToInt32(PaidBillsstr);
                    //        PaidBillCountAndAmount.BillAmount = ConvertintoCommaSeperated(PaidBillsAmountstr);
                    //        PaidBillCountAndAmount.BillTypeName = AppResources.Paid;

                    //        SegregatedBillTypeCorrepsondingCountAndAmount.Add(PaidBillCountAndAmount);
                    //        MyBillsChartModels = new List<MyBillsChartModel>();

                    //        MyBillsChartModels.Add(new MyBillsChartModel { BillCount = PaidBillCountAndAmount.BillCount, BillType = PaidBillCountAndAmount.BillTypeName, BillColor = Xamarin.Forms.Color.FromHex("#00674E") });
                    //        BillCount = PaidBillCountAndAmount.BillCount.ToString();

                    //        BillCount = (Convert.ToInt32(BillCount) + Convert.ToInt32(PaidBillCountAndAmount.BillCount)).ToString();
                    //        ColorsChild.Add(System.Drawing.Color.FromArgb(0, 103, 78));

                    //        iBillsCount = Convert.ToInt32(BillCount);
                    //        PaidBillCount = BillCount;

                    //    }
                    //    catch (Exception ex)
                    //    {

                    //    }


                    //}
                    //Partially Paid Bills
                    var MyBillsChartModelsTemp = new List<MyBillsChartModel>();

                    if (DashboardData.results[0] != null && DashboardData.results[0].PrbillsTot != null)
                    {
                        BillTypeCorrepsondingCountAndAmount PartiallyPaidBillCountAndAmount = new BillTypeCorrepsondingCountAndAmount();

                        PartiallyPaidBillCountAndAmount.Status = BillType.PrbillsTot;
                        String PartialPaidBillsstr = DashboardData.results[0].PrbillsTot.TrimStart(new Char[] { '0' });
                        String PartialPaidBillsAmountstr = DashboardData.results[0].PrbillsBetrw.TrimStart(new Char[] { '0' });
                        if (string.IsNullOrEmpty(PartialPaidBillsstr))
                        {
                            PartialPaidBillsstr = "0";
                        }
                        else if (PartialPaidBillsstr.Substring(0, 1) == ".")
                        {
                            PartialPaidBillsstr = "0" + PartialPaidBillsstr;
                        }
                        if (string.IsNullOrEmpty(PartialPaidBillsAmountstr))
                        {
                            PartialPaidBillsAmountstr = "0";
                        }
                        else if (PartialPaidBillsAmountstr.Substring(0, 1) == ".")
                        {
                            PartialPaidBillsAmountstr = "0" + PartialPaidBillsAmountstr;
                        }
                        PartiallyPaidBillCountAndAmount.BillCount = Convert.ToInt32(PartialPaidBillsstr);
                        PartiallyPaidBillCountAndAmount.BillAmount = ConvertintoCommaSeperated(PartialPaidBillsAmountstr);
                        PartiallyPaidBillCountAndAmount.BillTypeName = AppResources.Partial;

                        SegregatedBillTypeCorrepsondingCountAndAmount.Add(PartiallyPaidBillCountAndAmount);

                        MyBillsChartModelsTemp.Add(new MyBillsChartModel { BillCount = PartiallyPaidBillCountAndAmount.BillCount, BillType = PartiallyPaidBillCountAndAmount.BillTypeName, BillColor = Xamarin.Forms.Color.FromHex("#E39800") });
                        BillCount = PartiallyPaidBillCountAndAmount.BillCount.ToString();
                        // BillCount = (Convert.ToInt32(BillCount) + Convert.ToInt32(PartiallyPaidBillCountAndAmount.BillCount)).ToString();
                        ColorsChild.Add(System.Drawing.Color.FromArgb(227, 152, 0));

                        iBillsCount += Convert.ToInt32(BillCount);
                        PartiallyPaidBillCount = BillCount;
                    }
                    //Unpaid Bills
                    if (DashboardData.results[0] != null && DashboardData.results[0].UpbillsTot != null)
                    {
                        BillTypeCorrepsondingCountAndAmount UnPaidBillCountAndAmount = new BillTypeCorrepsondingCountAndAmount();

                        UnPaidBillCountAndAmount.Status = BillType.UpbillsTot;
                        String UnpaidBillsstr = DashboardData.results[0].UpbillsTot.TrimStart(new Char[] { '0' });
                        String UnpaidBillsAmountstr = DashboardData.results[0].UpbillsBetrw.TrimStart(new Char[] { '0' });
                        if (string.IsNullOrEmpty(UnpaidBillsstr))
                        {
                            UnpaidBillsstr = "0";
                        }
                        else if (UnpaidBillsstr.Substring(0, 1) == ".")
                        {
                            UnpaidBillsstr = "0" + UnpaidBillsstr;
                        }
                        if (string.IsNullOrEmpty(UnpaidBillsAmountstr))
                        {
                            UnpaidBillsAmountstr = "0";
                        }
                        else if (UnpaidBillsAmountstr.Substring(0, 1) == ".")
                        {
                            UnpaidBillsAmountstr = "0" + UnpaidBillsAmountstr;
                        }
                        UnPaidBillCountAndAmount.BillCount = Convert.ToInt32(UnpaidBillsstr);
                        UnPaidBillCountAndAmount.BillAmount = ConvertintoCommaSeperated(UnpaidBillsAmountstr);
                        UnPaidBillCountAndAmount.BillTypeName = AppResources.UnPaid;

                        SegregatedBillTypeCorrepsondingCountAndAmount.Add(UnPaidBillCountAndAmount);

                        MyBillsChartModelsTemp.Add(new MyBillsChartModel { BillCount = UnPaidBillCountAndAmount.BillCount, BillType = UnPaidBillCountAndAmount.BillTypeName, BillColor = Xamarin.Forms.Color.FromHex("#EC0000") });
                        BillCount = UnPaidBillCountAndAmount.BillCount.ToString();
                        // BillCount = (Convert.ToInt32(BillCount) + Convert.ToInt32(UnPaidBillCountAndAmount.BillCount)).ToString();
                        ColorsChild.Add(System.Drawing.Color.FromArgb(236, 0, 0));

                        iBillsCount += Convert.ToInt32(BillCount);
                        UnPaidBillCount = BillCount;

                    }
                    MyBillsChartModels = MyBillsChartModelsTemp;
                    if (Colors == null)
                        Colors = new ChartColorCollection();

                    Colors = ColorsChild;

                    BillCount = iBillsCount.ToString();

                    PaidString = AppResources.Paid + " " + PaidBillCount;
                    PartiallyPaidString = AppResources.Partiallynewui + " " + PartiallyPaidBillCount;
                    UnPaidString = AppResources.UnPaid + " " + UnPaidBillCount;

                }
            }
            catch (Exception ex)
            {
            }
        }
        public class ReturnTypeAndCorrepsondingCount
        {
            public string ReturnTypeName { get; set; }
            public string ReturnCount { get; set; }
            public GAZT.Models.ReturnType ReturnTypeProperty { get; set; }
        }
        public void PopulateReturnsInformation()
        {
            List<ReturnTypeAndCorrepsondingCount> SegregatedReturnTypeAndCorrepsondingCount = new List<ReturnTypeAndCorrepsondingCount>();
            try
            {
                if (DashboardData != null)
                {
                    if (DashboardData.results != null && DashboardData.results.Count > 0)
                    {
                        //Submited
                        if (DashboardData.results[0] != null && DashboardData.results[0].RtnTot != null)
                        {
                            ReturnTypeAndCorrepsondingCount SubmittedReturnTypeAndCorrepsondingCount = new ReturnTypeAndCorrepsondingCount();
                            SubmittedReturnTypeAndCorrepsondingCount.ReturnTypeProperty = GAZT.Models.ReturnType.RtnTot;
                            String RtnTotstr = DashboardData.results[0].RtnTot.TrimStart(new Char[] { '0' });
                            if (string.IsNullOrEmpty(RtnTotstr))
                            {
                                RtnTotstr = "0";
                            }
                            else
                            {
                                string returnToString = RtnTotstr.Substring(0, 1);
                                if (returnToString.Equals("."))
                                {
                                    RtnTotstr = "0" + RtnTotstr;
                                }
                                else
                                {
                                }
                            }
                            SubmittedReturnTypeAndCorrepsondingCount.ReturnCount = RtnTotstr;
                            SubmittedReturnTypeAndCorrepsondingCount.ReturnTypeName = AppResources.Submitted;

                            SegregatedReturnTypeAndCorrepsondingCount.Add(SubmittedReturnTypeAndCorrepsondingCount);
                        }

                        //Overdue
                        if (DashboardData.results[0] != null && DashboardData.results[0].DueIcr != null)
                        {
                            ReturnTypeAndCorrepsondingCount OverdueReturnTypeAndCorrepsondingCount = new ReturnTypeAndCorrepsondingCount();

                            OverdueReturnTypeAndCorrepsondingCount.ReturnTypeProperty = GAZT.Models.ReturnType.DueIcr;
                            String DueIcrstr = DashboardData.results[0].DueIcr.TrimStart(new Char[] { '0' });
                            if (string.IsNullOrEmpty(DueIcrstr))
                            {
                                DueIcrstr = "0";
                            }
                            else if (DueIcrstr.Substring(0, 1) == ".")
                            {
                                DueIcrstr = "0" + DueIcrstr;
                            }
                            OverdueReturnTypeAndCorrepsondingCount.ReturnCount = DueIcrstr;
                            OverdueReturnTypeAndCorrepsondingCount.ReturnTypeName = AppResources.OverDue;

                            SegregatedReturnTypeAndCorrepsondingCount.Add(OverdueReturnTypeAndCorrepsondingCount);
                        }

                        //UnSubmitted
                        if (DashboardData.results[0] != null && DashboardData.results[0].NrtnTot != null)
                        {
                            ReturnTypeAndCorrepsondingCount UnSubmittedReturnTypeAndCorrepsondingCount = new ReturnTypeAndCorrepsondingCount();

                            UnSubmittedReturnTypeAndCorrepsondingCount.ReturnTypeProperty = GAZT.Models.ReturnType.NrtnTot;
                            String NrtnTotstr = DashboardData.results[0].NrtnTot.TrimStart(new Char[] { '0' });
                            if (string.IsNullOrEmpty(NrtnTotstr))
                            {
                                NrtnTotstr = "0";
                            }
                            else if (NrtnTotstr.Substring(0, 1) == ".")
                            {
                                NrtnTotstr = "0" + NrtnTotstr;
                            }
                            UnSubmittedReturnTypeAndCorrepsondingCount.ReturnCount = NrtnTotstr;
                            UnSubmittedReturnTypeAndCorrepsondingCount.ReturnTypeName = AppResources.UnSubmitted;

                            SegregatedReturnTypeAndCorrepsondingCount.Add(UnSubmittedReturnTypeAndCorrepsondingCount);
                        }



                        if (SegregatedReturnTypesAndCorrepsondingCounts != null)
                            SegregatedReturnTypesAndCorrepsondingCounts.Clear();

                        SegregatedReturnTypesAndCorrepsondingCounts = SegregatedReturnTypeAndCorrepsondingCount;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void PopulateeServicesApplicableToTheTaxPayer()
        {
            //Call the API to get the eSevrices applicable to the TP
            var eServicesAvailableToTheTPTemp = new List<eServiceInfo>();

            if (eServicesAvailableToTheTP != null)
                eServicesAvailableToTheTP.Clear();
            if (DashboardData.results[0].TpType != null && DashboardData.results[0].TpType != "")
            {
                UtilityManager.TPTaxAvalable = DashboardData.results[0].TpType;
                UtilityManager.IsZakatAvailable = DashboardData.results[0].EstimateZkat;
                string[] TpTypes = DashboardData.results[0].TpType.Split(',');
                foreach (string ItemType in TpTypes)
                {
                    if (ItemType == "05" && DashboardData.results[0].EstimateZkat == "X")
                    {
                        eServicesAvailableToTheTPTemp.Add(new eServiceInfo { eServiceName = AppResources.EstimateZakat, BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_Estimated_Zakat_Returns.png" });
                    }
                    if (ItemType == "03" || ItemType == "13")
                    {
                        eServicesAvailableToTheTPTemp.Add(new eServiceInfo { eServiceName = AppResources.VatReturns, BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_VAT_Declaration.png" });
                    }
                }
            }

            eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZZFormBundleStatus, BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_Form_Bundle_Status.png" });
            eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.Bills, BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_My_Bills.png" });
            eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.Certificates, BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_My_Certificate.png" });
            eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZTINStatus, BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_TIN_Status.png" });

            eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZRealEstateServiceTitle, BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_Service_6.png" });

            //Tax Evasion Section
            eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZTEReportReportScreenTitle, BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_Tax_Evasion.png" });
            //Tax Evasion Section
            eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZZZVatLookUpTitleTextNew, BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_VAT_Lookup.png" });
            //TaxRegistration
            //If tehe value is "X" that means the registration of the user is completed and hence we will not show the Tile.

            try
            {
                if (App.LoginDataRetrieved.VtReg == null)
                {
                    eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZZZZVatRegistrationTile, BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_VAT_Declaration.png" });
                }
                else if (App.LoginDataRetrieved.VtReg != "X")
                {
                    eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZZZZVatRegistrationTile, BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_VAT_Declaration.png" });
                }
            }
            catch
            {
                eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZZZZVatRegistrationTile, BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_VAT_Declaration.png" });
            }
        }

        public async void PopulateStatements(string taxType, string statementFilter, string year)
        {
            try
            {
                    IsLoading = true;

                HeaderSet = await WebServiceManager.GAZTGetAccountStatementHeaderSet(statementFilter, year, taxType);

                double tempEndProgressBar = (Convert.ToDouble(HeaderSet.D.DebitAmount));
                double startCreditProgressBar = (Convert.ToDouble(HeaderSet.D.Credit.Replace("-", string.Empty)));
                double totalBalance = tempEndProgressBar + startCreditProgressBar;

                AccStmtnCreditAmount = HeaderSet.D.CreditAmount.Replace("-", string.Empty);

                DebitAmountEndProgressBar = (tempEndProgressBar / totalBalance) * 100;
                CreditAmountStartProgressBar = ((startCreditProgressBar / totalBalance) * 100);
                MessagingCenter.Send<Object>(this, "UpdateProgressBar");

                    IsLoading = false;
            }
            catch (Exception ex)
            {
                    IsLoading = false;
            }
        }

        private string ConvertintoCommaSeperated(string strAmount)
        {
            string amountWithComma = string.Empty;
            try
            {
                if (strAmount != null && strAmount.Length > 0)
                {
                    double testDueAmount = Convert.ToDouble(strAmount);
                    CultureInfo ci = new CultureInfo("en-us");
                    string _testDueAmount;//= testDueAmount.ToString("#,##0");
                    double floating = Convert.ToDouble(strAmount);
                    _testDueAmount = floating.ToString("N02", ci);
                    amountWithComma = _testDueAmount;
                }
            }
            catch (Exception ex)
            {

            }
            return amountWithComma;
        }
        public async Task LogOut()
        {
                // App.DisplayProgressView();
                IsLoading = true;
            if (App.TP != null)
                App.TP = null;
            if (App.PreviousIsArabic)
            {
                String langName = "ar-AE";
                AppResources.Culture = new CultureInfo(langName);
            }
            else
            {
                String langName = "en-US";
                AppResources.Culture = new CultureInfo(langName);
            }

            try
            {
                await WebServiceManager.GAZTLogOff();
            }
            catch
            {

            }

                //App.HideProgressView();
                IsLoading = false;

            //var _navigation = Application.Current.MainPage.Navigation;
            //foreach (var item in _navigation.NavigationStack)
            //{
            //    if (item.GetType().Name == App.GAZTNewDesignOnBoardingAnimationPageView)
            //    {
            //        _navigation.RemovePage(item);
            //        break;
            //    }
            //}

            App.IsLogOut = true;
            App.IsLoginCalled = false;
            App.IsSamlApiCalledAndroid = false;

            try
            {
                App.httpClientHandler = new HttpClientHandler();
                App.httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                App.httpClientHandler.CookieContainer = new System.Net.CookieContainer();
            }
            catch (Exception ex)
            {

            }

            //_navigationService.NavigateTo(App.GAZTNewDesignOnBoardingAnimationPageView);

            // await _navigation.PopToRootAsync();
            //_navigation.NavigationStack.ToList().Clear();
            _navigationService.GoBack();
        }
        #endregion
    }
}
