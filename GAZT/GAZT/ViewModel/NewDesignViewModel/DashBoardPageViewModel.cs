using EGAZT.AppConfigurations;
using EGAZT.Helper;
using EGAZT.Models;
using EGAZT.Models.AccountStatements;
using EGAZT.Models.EnumModels;
using EGAZT.Models.PaymentModel;
using EGAZT.Models.SurveyModels;
using EGAZT.Services.Interface;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using EGAZT.Views.NewDesign.MyBillsPages;
using EGAZT.Views.NewDesign.PaymentOptions;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfCalendar.XForms;
using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    [Preserve(AllMembers = true)]
    public class GAZTNewDesignDashBoardPageViewModel : BaseViewModel
    {
        #region Variable
        bool isSurveyVisible ;
        public bool IsSurveyVisible
        {
            get
            {
                return isSurveyVisible;
            }
            set
            {


                isSurveyVisible = value;
                RaisePropertyChanged();
            }
        }

      
        int fQanswer;
        public int FQanswer
        {
            get
            {
                return fQanswer;
            }
            set
            {
                fQanswer = value;
                RaisePropertyChanged();
            }
        }

        int qNumber=2;
        public int QNumber
        {
            get
            {
                return qNumber;
            }
            set
            {
                qNumber = value;
                RaisePropertyChanged();
            }
        }


        int surveyCurrentStep =0;
        public int SurveyCurrentStep
        {
            get
            {
                return surveyCurrentStep;
            }
            set
            {
                surveyCurrentStep = value;
                RaisePropertyChanged();
            }
        }


        string sQAnswer;
        public string SQAnswer
        {
            get
            {
                return sQAnswer;
            }
            set
            {
                sQAnswer = value;
                RaisePropertyChanged();
            }
        }


        string questionTxt;
        public string QuestionTxt
        {
            get
            {
                return questionTxt;
            }
            set
            {
                questionTxt = value;
                RaisePropertyChanged();
            }
        }

        SurveyQuestions selctedImojy;
        public SurveyQuestions SelctedImojy
        {
            get
            {
                return selctedImojy;
            }
            set
            {
                selctedImojy = value;
                RaisePropertyChanged();
            }
        }


        ObservableCollection<SurveyQuestions> imojiesLst=new ObservableCollection<SurveyQuestions>() { new SurveyQuestions() { ImojieSource = "Stronglysatisfied", ID = "61c32bf2527cacedb5d3192c" }, new SurveyQuestions() { ImojieSource = "Satisfied", ID = "61c32bf2527cacedb5d3192d" },  new SurveyQuestions() { ImojieSource = "NeitherDissatisfiednorSatisfied",ID= "61c32bf2527cacedb5d3192e" }, new SurveyQuestions() { ImojieSource = "Dissatisfied", ID = "61c32bf2527cacedb5d3192f" },new SurveyQuestions() { ImojieSource = "Angry", ID = "61c32bf2527cacedb5d31930" }};
        public ObservableCollection<SurveyQuestions> ImojiesLst
        {
            get
            {
                return imojiesLst;
            }
            set
            {
               imojiesLst = value;
                RaisePropertyChanged();
            }
        }



        public string selectedFbNum = "";
        public string selectedSadadNo = "";
        public string selectedTaxablePeriod = "";
        public string selectedAmount = "";

        private string _sadadBindNumber = "";
        OverduePaymentAndUnSubmittedReturn BModel=null;

        public string SadadBindNumber
        {
            get
            {
                return _sadadBindNumber;
            }
            set
            {
                if (_sadadBindNumber == value) return;

                _sadadBindNumber = value;
                RaisePropertyChanged("SadadBindNumber");
            }
        }

        private ObservableCollection<MyBills> multiplePayableBills;
        public ObservableCollection<MyBills> MultiplePayableBills
        {
            get
            {
                return multiplePayableBills;
            }
            set
            {
                if (multiplePayableBills == value) return;

                multiplePayableBills = value;

                RaisePropertyChanged("MultiplePayableBills");
            }
        }

        private string _totalAmount = "0.0";
        public string TotalAmount
        {
            get
            {
                return _totalAmount;
            }
            set
            {
                if (_totalAmount == value) return;

                _totalAmount = value;
                RaisePropertyChanged("TotalAmount");
            }
        }

        private string _taxablePeriod = "";
        public string TaxablePeriod
        {
            get
            {
                return _taxablePeriod;
            }
            set
            {
                if (_taxablePeriod == value) return;

                _taxablePeriod = value;
                RaisePropertyChanged("TaxablePeriod");
            }
        }

        private string _referenceNumber = "";
        public string ReferenceNumber
        {
            get
            {
                return _referenceNumber;
            }
            set
            {
                if (_referenceNumber == value) return;

                _referenceNumber = value;
                RaisePropertyChanged("ReferenceNumber");
            }
        }

        private DashBoardModelTabEnum _currentTab = DashBoardModelTabEnum.DashBoard;
        public DashBoardModelTabEnum currentTab
        {
            get => _currentTab;
            set
            {
                if (_currentTab == value) return;
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
                if (_currenrIndex == value) return;

                _currenrIndex = value;
                RaisePropertyChanged(nameof(CurrentIndex));
            }
        }
        #endregion

        #region Fields

        private ICommand EserviceCommand { get; set; }
        private GAZT.Models.TaxPayerProfile _TaxPayerProfile = App.TP;

        private bool _menuViewVisible = false;
        private bool _homeViewVisible = true;
        private bool _accountStatementVisible = false;
        private bool _IsToolbarTaxVisible = true;
        private bool _liveChatVisible = false;
        private Color _homeIndicatorColor =  (Color)Application.Current.Resources["Primary"];
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
        public Dashboard DashboardData = null;
        private CalendarEventCollection _CommittmentsSchedule = null;
        public bool isPayNowTapped = false;

        #region Lists

        private List<OverduePaymentAndUnSubmittedReturn> _BillsAndReturnsCommitments = null;
        private List<OverduePaymentAndUnSubmittedReturn> _Bills = null;
        private List<ReturnTypeAndCorrepsondingCount> _SegregatedReturnTypesAndCorrepsondingCounts = null;
        private List<OverduePaymentAndUnSubmittedReturn> _Returns = null;
        private List<eServiceInfo> _eServices = null;
        private List<TaxRelationSetResult> Tax = null;
        private List<TaxRelationSetResult> _taxTypeFilter = null;
        private List<MyBillsChartModel> _MyBillsChartModels = null;

        public List<OverduePaymentAndUnSubmittedReturn> BillsAndReturnsCommitments
        {
            get
            {
                return this._BillsAndReturnsCommitments;
            }
            set
            {
                if (_BillsAndReturnsCommitments == value) return;

                if (value != null)
                {
                    this._BillsAndReturnsCommitments = value;
                    RaisePropertyChanged("BillsAndReturnsCommitments");
                }
            }
        }

        private ObservableCollection<ASResult> _AccountStatementsList { get; set; }

        public ObservableCollection<ASResult> AccountStatementsList
        {
            get
            {
                return this._AccountStatementsList;
            }
            set
            {
                if (_AccountStatementsList == value) return;

                if (value != null)
                {
                    this._AccountStatementsList = value;
                    RaisePropertyChanged("AccountStatementsList");
                }
            }
        }

        private List<OverduePaymentAndUnSubmittedReturn> allBills { get; set; }
        public List<OverduePaymentAndUnSubmittedReturn> AllBills
        {
            get
            {
                return this.allBills;
            }
            set
            {
                if (allBills == value) return;

                if (value != null)
                {
                    this.allBills = value;
                    RaisePropertyChanged("AllBills");
                }
            }
        }
        private ObservableCollection<OverduePaymentAndUnSubmittedReturn> _PendingBills { get; set; }

        public ObservableCollection<OverduePaymentAndUnSubmittedReturn> PendingBills
        {
            get
            {
                return this._PendingBills;
            }
            set
            {
                if (_PendingBills == value) return;

                if (value != null)
                {
                    this._PendingBills = value;
                    RaisePropertyChanged("PendingBills");
                }
            }
        }

        private ObservableCollection<InstalmentPlanResult> _InstalmentPlanList { get; set; }

        public ObservableCollection<InstalmentPlanResult> InstalmentPlanList
        {
            get
            {
                return this._InstalmentPlanList;
            }
            set
            {
                if (_InstalmentPlanList == value) return;

                if (value != null)
                {
                    this._InstalmentPlanList = value;
                    RaisePropertyChanged("InstalmentPlanList");
                }
            }
        }
        public ChartColorCollection _InstalmentColors { get; set; }

        public ChartColorCollection InstalmentColors
        {
            get
            {
                return this._InstalmentColors;
            }
            set
            {
                if (_InstalmentColors == value) return;

                if (value != null)
                {
                    this._InstalmentColors = value;
                    RaisePropertyChanged("InstalmentColors");
                }
            }
        }

        public List<ObservableCollection<ChartDataPoint>> _InstalmentDoughnutSeriesData { get; set; }
        public List<ObservableCollection<ChartDataPoint>> InstalmentDoughnutSeriesData
        {
            get
            {
                return this._InstalmentDoughnutSeriesData;
            }
            set
            {
                if (_InstalmentDoughnutSeriesData == value) return;

                if (value != null)
                {
                    this._InstalmentDoughnutSeriesData = value;
                    RaisePropertyChanged("InstalmentDoughnutSeriesData");
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
                if (_taxTypeFilter == value) return;

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
                if (_MyBillsChartModels == value) return;
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
                if (_Bills == value) return;

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
                if (_SegregatedReturnTypesAndCorrepsondingCounts == value) return;

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
                if (_Returns == value) return;

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
                if (_eServices == value) return;

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
        }
        #endregion

        #endregion

        #region Public Properties


        private bool _IsAccountsStatementLoading = true;
        public bool IsAccountsStatementLoading
        {
            get
            {
                return _IsAccountsStatementLoading;
            }
            set
            {
                _IsAccountsStatementLoading = value;
                RaisePropertyChanged("IsAccountsStatementLoading");
            }
        }

        private string _appVersion = App.AppVersion;
        public string AppVersion
        {
            get
            {
                return _appVersion;
            }
            set
            {
                if (_appVersion == value) return;

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
                if (_headerSet == value) return;

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
                if (_tabIdentification == value) return;

                _tabIdentification = value;
                RaisePropertyChanged("TabIdentification");
            }
        }

        public DashboardInstalmentplan _instalmentResponse = null;
        public DashboardInstalmentplan InstalmentResponse
        {
            get
            {
                return _instalmentResponse;
            }
            set
            {
                if (_instalmentResponse == value) return;

                _instalmentResponse = value;
                RaisePropertyChanged("InstalmentResponse");
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
                if (_taxpayerName == value) return;

                _taxpayerName = value;
                RaisePropertyChanged("TaxpayerName");
            }
        }
        private bool _applePayStatus;
        public bool ApplePayStatus
        {
            get
            {
                return _applePayStatus;
            }
            set
            {
                if (_applePayStatus == value) return;

                _applePayStatus = value;
                RaisePropertyChanged("ApplePayStatus");
            }
        }

        public string ApplePayTokenData = "";


        private bool _ifnotRegInVATAndZakat = false;
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

        private bool _IsAccountStatementAvilable = false;
        public bool IsAccountStatementAvilable
        {
            get
            {
                return _IsAccountStatementAvilable;
            }
            set
            {
                _IsAccountStatementAvilable = value;
                RaisePropertyChanged("IsAccountStatementAvilable");
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
                if (_ifRegInZakat == value) return;

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
                if (_ifRegInVAT == value) return;

                _ifRegInVAT = value;
                RaisePropertyChanged("IfRegInVAT");
            }
        }

        private bool _isInstalmentPlanVisible = false;
        public bool IsInstalmentPlanVisible
        {
            get
            {
                return _isInstalmentPlanVisible;
            }
            set
            {
                if (_isInstalmentPlanVisible == value) return;

                _isInstalmentPlanVisible = value;
                RaisePropertyChanged("IsInstalmentPlanVisible");
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
                if (_ifSignUpnNotRegInVAT == value) return;

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
                if (_ifSignUpnNotRegInVATShowVATServie == value) return;

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
                if (_debitAmountEndProgressBar == value) return;

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
                if (_creditAmountStartProgressBar == value) return;

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
                if (_totalAmountProgressBar == value) return;

                _totalAmountProgressBar = value;
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
                if (_selectedTaxTypeForFilterValue == value) return;

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
                if (_SelectedTaxTypeForFilter == value) return;

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
                if (_colors == value) return;

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
                if (_welcomeText == value) return;

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
                if (_rotation == value) return;

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
                if (_translateText == value) return;

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
                if (_billCount == value) return;

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
                if (_paidbillCount == value) return;

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
                if (_partiallypaidbillCount == value) return;

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
                if (_unPaidbillCount == value) return;

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
                if (_nDCommitments == value) return;

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
                if (_zBills == value) return;

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
                if (_returns == value) return;

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
                if (_aboutUs == value) return;

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
                if (_contactus == value) return;

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
                if (_setMyCommitmentsCollectionVisibility == value) return;

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
                if (_setNoCommitmentsAvailableLabelVisibility == value) return;

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
                if (_privacyandPolicy == value) return;

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
                if (_logout == value) return;

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
                if (_TaxPayerProfile == value) return;

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
                if (_isVatRegistrationTileVisible == value) return;

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
                if (_isRegistrationDetailsTileVisible == value) return;

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
                if (_isVatAmendmentTileVisible == value) return;

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
                if (_isEstablishmentRegistrationTileVisible == value) return;

                _isEstablishmentRegistrationTileVisible = value;
                RaisePropertyChanged("IsEstablishmentRegistrationTileVisible");
            }
        }

        private bool _isSubsidyTileVisible = false;
        public bool IsSubsidyTileVisible
        {
            get
            {
                return _isSubsidyTileVisible;
            }
            set
            {
                if (_isSubsidyTileVisible == value) return;

                _isSubsidyTileVisible = value;
                RaisePropertyChanged("IsSubsidyTileVisible");
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
                if (_menuViewVisible == value) return;

                this._menuViewVisible = value;
                this.RaisePropertyChanged("MenuViewVisible");
            }
        }

        private bool _isBillsTotalAmountAR = false;
        public bool IsBillsTotalAmountAR
        {
            get
            {
                return _isBillsTotalAmountAR;
            }
            set
            {
                if (_isBillsTotalAmountAR == value) return;

                this._isBillsTotalAmountAR = value;
                this.RaisePropertyChanged("IsBillsTotalAmountAR");
            }
        }
        private bool _isBillsTotalAmountEN = false;
        public bool IsBillsTotalAmountEN
        {
            get
            {
                return _isBillsTotalAmountEN;
            }
            set
            {
                if (_isBillsTotalAmountEN == value) return;

                this._isBillsTotalAmountEN = value;
                this.RaisePropertyChanged("IsBillsTotalAmountEN");
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
                if (_homeViewVisible == value) return;

                this._homeViewVisible = value;
                if (_homeViewVisible != null)
                {
                    if (_homeViewVisible)
                    {
                        _homeIndicatorColor =  (Color)Application.Current.Resources["Primary"];
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
                if (_accountStatementVisible == value) return;

                this._accountStatementVisible = value;
                if (_accountStatementVisible != null)
                {
                    if (_accountStatementVisible)
                    {
                        _homeIndicatorColor =  (Color)Application.Current.Resources["Primary"];
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
                if (_liveChatVisible == value) return;
                _liveChatVisible = value;
                if (_liveChatVisible)
                {
                    _homeIndicatorColor =  (Color)Application.Current.Resources["Primary"];
                    MenuIndicatorColor = Color.White;
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
                if (_homeIndicatorColor == value) return;

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
                if (_menuIndicatorColor == value) return;

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
                if (_tabbarColor == value) return;

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
                if (_stackMenuColor == value) return;

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
                if (_NextCommitmentsString == value) return;

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
                if (_ReturnString == value) return;

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
                if (_PaidString == value) return;

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
                if (_PartiallyPaidString == value) return;

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
                if (_UnPaidString == value) return;

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
                if (_BillString == value) return;

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
                if (_TotalString == value) return;

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
                if (_accStmtnCreditAmount == value) return;

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
                if (_selectedCommitmentFilterValue == value) return;

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
                if (_SelectedCommitmentFilterLabelValue == value) return;

                if (!string.IsNullOrEmpty(value))
                {
                    _SelectedCommitmentFilterLabelValue = value;
                    RaisePropertyChanged("SelectedCommitmentFilterLabelValue");

                }

            }
        }

        public bool IsToolbarTaxVisible
        {
            get
            {
                return _IsToolbarTaxVisible;
            }
            set
            {
                if (_IsToolbarTaxVisible == value) return;

                this._IsToolbarTaxVisible = value;

                this.RaisePropertyChanged("IsToolbarTaxVisible");
            }
        }

        private bool _isMenuLogoVisible = true;
        public bool IsMenuLogoVisible
        {
            get
            {
                return _isMenuLogoVisible;
            }
            set
            {
                if (_isMenuLogoVisible == value) return;

                this._isMenuLogoVisible = value;

                this.RaisePropertyChanged("IsMenuLogoVisible");
            }
        }

        //Single Line 74
        private int _LastTransactionsListHeight = 220;
        public int LastTransactionsListHeight
        {
            get
            {
                return _LastTransactionsListHeight;
            }
            set
            {
                _LastTransactionsListHeight = value;
                RaisePropertyChanged("LastTransactionsListHeight");
            }
        }

        //Single Line 84
        private int _PendingBillsListHeight = 250;
        public int PendingBillsListHeight
        {
            get
            {
                return _PendingBillsListHeight;
            }
            set
            {
                _PendingBillsListHeight = value;
                RaisePropertyChanged("PendingBillsListHeight");
            }
        }
        private bool _IsPendingBillsVisible = false;
        public bool IsPendingBillsVisible
        {
            get
            {
                return _IsPendingBillsVisible;
            }
            set
            {
                _IsPendingBillsVisible = value;
                RaisePropertyChanged("IsPendingBillsVisible");
            }
        }


        private string _SubmittedCount = null;
        public string SubmittedCount
        {
            get
            {
                return _SubmittedCount;
            }
            set
            {
                if (_SubmittedCount == value) return;

                if (!string.IsNullOrEmpty(value))
                {
                    _SubmittedCount = value;
                    RaisePropertyChanged("SubmittedCount");

                }

            }
        }

        private string _UnSubmittedCount = null;
        public string UnSubmittedCount
        {
            get
            {
                return _UnSubmittedCount;
            }
            set
            {
                if (_UnSubmittedCount == value) return;

                if (!string.IsNullOrEmpty(value))
                {
                    _UnSubmittedCount = value;
                    RaisePropertyChanged("UnSubmittedCount");

                }

            }
        }

        private string _OverDueCount = null;
        public string OverDueCount
        {
            get
            {
                return _OverDueCount;
            }
            set
            {
                if (_OverDueCount == value) return;

                if (!string.IsNullOrEmpty(value))
                {
                    _OverDueCount = value;
                    RaisePropertyChanged("OverDueCount");

                }

            }
        }

        private Double _MyObligationAmount = 0.0;
        public Double MyObligationAmount
        {
            get
            {
                return _MyObligationAmount;
            }
            set
            {
                _MyObligationAmount = value;
                RaisePropertyChanged("MyObligationAmount");
            }
        }
        private string _MyObligationAmountCommas = "";
        public string MyObligationAmountCommas
        {
            get
            {
                return _MyObligationAmountCommas;
            }
            set
            {
                _MyObligationAmountCommas = value;
                RaisePropertyChanged("MyObligationAmountCommas");
            }
        }


        private bool _IsMyObligationsClear = false;
        public bool IsMyObligationsClear
        {
            get
            {
                return _IsMyObligationsClear;
            }
            set
            {
                _IsMyObligationsClear = value;
                RaisePropertyChanged("IsMyObligationsClear");
            }
        }

        private bool _IsBodyMyTaxVisible = false;
        public bool IsBodyMyTaxVisible
        {
            get
            {
                return _IsBodyMyTaxVisible;
            }
            set
            {
                _IsBodyMyTaxVisible = value;
                RaisePropertyChanged("IsBodyMyTaxVisible");
            }
        }


        public ValidatePaymentResponse _paymentData = null;
        public ValidatePaymentResponse PaymentData
        {
            get
            {
                return _paymentData;
            }
            set
            {
                if (_paymentData == value) return;

                _paymentData = value;
                RaisePropertyChanged("PaymentData");
            }
        }

        public string _dayMonth = null;
        public string DayMonth
        {
            get
            {
                return _dayMonth;
            }
            set
            {
                if (_dayMonth == value) return;
                _dayMonth = value;
                RaisePropertyChanged("DayMonth");
            }
        }

        private ObservableCollection<MyBills> _ACStatementBills;
        public ObservableCollection<MyBills> ACStatementBills
        {
            get
            {
                return _ACStatementBills;
            }
            set
            {
                if (_ACStatementBills == value) return;

                _ACStatementBills = value;
                
                 RaisePropertyChanged("ACStatementBills");
            }
        }

        #endregion

        #region Constructor
        ISurveyServices _surveyServices;
        public GAZTNewDesignDashBoardPageViewModel(INavigationService navigationService, IDialogService dialogService, ISurveyServices surveyServices) : base(navigationService, dialogService)
        {
           // ISEndSurvey = true;
            // IsShowMsgView = true;
            _surveyServices = surveyServices;
            MenuViewVisible = false;
            LiveChatVisible = false;
            AccountStatementVisible = false;
            IsToolbarTaxVisible = true;

            MyObligationAmount = 0.0;

            TaxpayerName = string.Empty;
            HomeViewVisible = true;
            IsVatRegistrationTileVisible = false;
            IsInstalmentPlanVisible = false;
            if (App.TP != null)
                TaxPayerProfile = App.TP;

            if (App.IsArabic)
            {
                TranslateText = AppResources.ZZZChangetoLanguage;
            }
            else
            {
                TranslateText = AppResources.ZZZChangetoLanguage;
            }

            MenuViewVisible = false;
            HomeViewVisible = true;
            //GetDashBoardMenuLst(1);

        }
        #endregion

        #region Method
        public async void verifyPaymentAndShowBillsPopup(OverduePaymentAndUnSubmittedReturn BModel)
        {
            this.BModel = BModel;
           var newMultiplePayableBills = new ObservableCollection<MyBills>();

            foreach(var item in AllBills)
            {
                newMultiplePayableBills.Add(new MyBills { 
                    Abtypt = item.Abtypt,
                    VTRE2=item.Sopbel, 
                    MadabutFg =item.MadabutFg,
                    TestDueAmount=item.Amount,
                    FormatedFaedn=item.FormatedDuedate,
                    StatusText=item.IcrStatus,
                    Fbnum=item.Fbnum,
                    Txt30=item.Txt50

                });
            }

            if (AllBills != null && AllBills.Count > 0)
            {
                MultiplePayableBills = new ObservableCollection<MyBills>(newMultiplePayableBills.Where(x => !String.IsNullOrEmpty(BModel.Sopbel) && x.VTRE2.Equals(BModel.Sopbel)).ToList());
            }

            if (MultiplePayableBills != null && MultiplePayableBills.Count > 1)
            {
                await PopupNavigation.Instance.PushAsync(new MyBillsMultiplePayableList(MultiplePayableBills));
                await Task.Delay(2000);
                isPayNowTapped = false;
                return;
            }
            else
            {
                showPaymentOptions();
            }
        }

        public async void showPaymentOptions()
        {

            if (BModel != null)
            {

                if (BModel.MadabutFg == "X")
                {
                    await PopupNavigation.Instance.PushAsync(new PaymentOptionsPageView(true, false, false, ""));
                }
                else
                {
                    await PopupNavigation.Instance.PushAsync(new PaymentOptionsPageView(true, false, true, BModel.OpenliMsg));
                }
                var total = "";
                if (MultiplePayableBills != null && MultiplePayableBills.Count > 0)
                {
                    total = MultiplePayableBills.Sum(x => Double.Parse(x.TestDueAmount)).ToString();
                }
                else
                {
                    total = BModel.Amount;
                }
                selectedFbNum = BModel.Fbnum;
                selectedSadadNo = BModel.Sopbel;
                selectedAmount = total;
                selectedTaxablePeriod = BModel.Persl;
                isPayNowTapped = false;
    }
        }

        public async Task SadadPaymentSelected()
        {
            /*_navigationService.GoBack();*/
            _navigationService.NavigateTo(App.MyBillsSadadDetailsPageView, 0);
        }

        public async Task ApplePaySelected()
        {

            DoValidatePayment(fbNum: selectedFbNum, selectedSadadNo, "A");
        }
        public void MadaPaymentSelected()
        {


            DoValidatePayment(selectedFbNum, selectedSadadNo, "M");




        }

        public async Task DoValidatePayment(string fbNum, string sdadNo, string paymentType)
        {
            try
            {
                try
                {

                    IsLoading = true;

                    var platform = "";

                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        platform = "C4";
                    }
                    else if (Device.RuntimePlatform == Device.Android)
                    {
                        platform = "C3";
                    }
                    //PaymentData = await WebServiceManager.GAZTValidatePayment(fbNum, App.LoginDataRetrieved.TIN, platform);
                    PaymentData = null;
                    PaymentData = await WebServiceManager.GAZTValidateMyBillsPayment(fbNum, App.LoginDataRetrieved.TIN, platform, sdadNo, paymentType);


                    ValidatePayment modelDetails = new ValidatePayment();
                    modelDetails.Fbnum = fbNum;
                    modelDetails.Pymntty = paymentType;
                    modelDetails.Tin = App.LoginDataRetrieved.TIN;
                    modelDetails.Srcid = platform;
                    modelDetails.Srctile = "53";
                    modelDetails.Sadad = sdadNo;

                    PaymentData = await WebServiceManager.GAZTValidatePayment(modelDetails);

                    if (PaymentData != null && PaymentData.d != null)
                    {

                        if (PaymentData.d.Guid != null && PaymentData.d.Guid == "")
                        {
                            await PopupNavigation.Instance.PushAsync(new PaymentExceptionPageView());
                            return;
                        }

                        if (PaymentData.d.Guid != null)
                        {

                            App.PaymentGuid = PaymentData.d.Guid;

                        }

                        if (paymentType == "M")
                        {

                            Device.BeginInvokeOnMainThread(async () => {

                                _navigationService.NavigateTo(App.PaymentProcessWebview, 2);
                                //await App.Current.MainPage.Navigation.PushAsync(new PaymentProcessWebview());

                            });
                        }
                        else
                        {

                            ApplePayStatus = await ProcessApplePay();
                        }


                        // var VatAmount = NetdueVat.Replace(",", "");
                        //if (String.IsNullOrEmpty(amount) || Double.Parse(amount) == 0)
                        //{
                        //    await PopupNavigation.Instance.PushAsync(new PaymentOptionsPageView(true, true, false, string.Empty));
                        //}
                        //else if (!String.IsNullOrEmpty(amount) && Double.Parse(amount) > 20000)
                        //{
                        //    await PopupNavigation.Instance.PushAsync(new PaymentOptionsPageView(true, false, true, string.Empty));
                        //}
                        //else
                        //{
                        //    await PopupNavigation.Instance.PushAsync(new PaymentOptionsPageView(true, false, false, string.Empty));
                        //}

                    }

                    IsLoading = false;

                }
                catch (GAZTValidatePaymentInProcessException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        IsLoading = false;
                        await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        //_navigationService.GoBack();
                    });
                }
                catch (InternetException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        IsLoading = false;
                        //   await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                        _navigationService.GoBack();
                    });
                }
                catch (GAZTNetworkConnectivityIssueException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        IsLoading = false;
                        //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));

                    });
                }
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                    _navigationService.GoBack();
                });
            }
        }
        private async Task<bool> ProcessApplePay()
        {
            try
            {
                var Amount = Convert.ToDouble(PaymentData.d.Amount);
                var BillAmount = Math.Round(Amount, 2);
                DependencyService.Get<IApplePayAuthorizer>().IsPaymentFromDashboard(true);
                return DependencyService.Get<IApplePayAuthorizer>().AuthorizePayment(BillAmount, AppResources.ApplePayText);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                return false;
            }
        }

        public async Task UpdateApplePayPaymentGuid()
        {
            try
            {
                try
                {

                    IsLoading = true;

                    var platform = string.Empty;

                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        platform = "C4";
                    }
                    else if (Device.RuntimePlatform == Device.Android)
                    {
                        platform = "C3";
                    }



                    ApplePayToken modelDetails = new ApplePayToken();
                    modelDetails.Guid = App.PaymentGuid;
                    modelDetails.PaymentToken = ApplePayTokenData;
                    modelDetails.SrcId = platform;


                    ApplePayTokenResponse response = await WebServiceManager.GAZTUpdateApplePayGuid(modelDetails);


                    if (response != null && response.d != null)
                    {

                        if (response.d.Success)
                        {

                            PaymentSucess paymentInfo = new PaymentSucess();
                            paymentInfo.Paymentref = response.d.PayRef;
                            if (response.d.PerslTxt != null)
                            {
                                paymentInfo.Period = response.d.PerslTxt;
                            }

                            _navigationService.NavigateTo(App.MyBillsSuccessPageView, paymentInfo);
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                //await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                                //_navigationService.GoBack();

                                await PopupNavigation.Instance.PushAsync(new PaymentExceptionPageView());
                            });

                        }

                    }
                    IsLoading = false;

                }
                catch (GAZTValidatePaymentInProcessException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        IsLoading = false;
                        await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        _navigationService.GoBack();
                    });
                }
                catch (InternetException ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        IsLoading = false;
                        //   await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                        _navigationService.GoBack();
                    });
                }
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                    _navigationService.GoBack();
                });
            }
        }

        public async Task LoadDashboardData()
        {
            try
            {
                if (App.TP != null)
                {
                    if (App.TP.TypeChk == "X")
                    {
                        TaxpayerName = App.TP.NameFirst + " " + App.TP.NameLast;
                    }
                    else
                    {
                        TaxpayerName = App.TP.NameOrg1;
                    }
                }

                try
                {
                    DashboardData = await WebServiceManager.GAZTGetDashboardData(UtilityManager.GetLanguageParameter(), App.TP.Userid);

                }
                catch (GAZTErrorException ex)
                {

                    Console.Write(ex.ToString());
                    Console.Write(ex.StackTrace.ToString());
                }

                if (App.isMybillsRefresh) {

                    _ = Task.Run(async () => {
                        await GetAccountStatments();
                        await GetBillsAndReturns();
                        PopulateBillsInformation();
                    });
                }
                else {

                    _ = Task.Run(async () => {
                        await GetAccountStatments();
                        await GetBillsAndReturns();
                        PopualateCommittmentsInformation();
                    });
                    //_ = Task.Run(GetAccountStatments);
                    // _ = Task.Run(GetBillsAndReturns);
                    if (DashboardData.results[0] != null && DashboardData.results[0].InsActFlg != null)
                    {

                        if (DashboardData.results[0].InsActFlg == "X")
                        {
                            IsInstalmentPlanVisible = true;
                            _ = Task.Run(getDashboardInstalmentPlan);
                        }
                        else
                        {

                            IsInstalmentPlanVisible = false;
                        }

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
                            if (MessageForTheUser == AppResources.ZZInternetConnectionMessage)
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                     _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                });
                                _navigationService.GoBack();
                            }
                            else if (MessageForTheUser == AppResources.NetworkConnectivityIssue)
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                     _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                });
                                _navigationService.GoBack();
                            }
                            else if (MessageForTheUser == AppResources.ZYourSessionhasexpiredPleaseLoginagain)
                            {
                                PopToRootPage();
                            }
                        });
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
                Console.WriteLine(ex.Message);
                IsLoading = false;
                Console.Write(ex.StackTrace.ToString());
            }

            IsLoading = false;
            SelectedCommitmentFilterLabelValue = AppResources.ZZOverdueCommitments;
        }

        public async Task GetAccountStatments()
        {
            IsAccountsStatementLoading = true;



            string lang = UtilityManager.GetLanguageParameter();

            ObservableCollection<MyBills> ACBills = new ObservableCollection<MyBills>();

            ObservableCollection<MyBills> TempBills =  WebServiceManager.GAZTGetMyBills(App.TP.Tin, lang,"Bills");

            if (TempBills != null)
            {


                //var newItems = TempBills.OrderByDescending(o => o.Faedn).ToList();
                //newItems.Reverse();

                var newItems = TempBills.ToList();
               


                if (newItems != null)
                {
                    if (newItems.Count > 3)
                    {
                        for (int i = 0; i < 3; i++)
                        {


                            var singleItem = newItems[i];


                            ACBills.Add(singleItem);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < newItems.Count; i++)
                        {
                            var singleItem = newItems[i];


                            ACBills.Add(singleItem);
                        }
                    }

                }

                ACStatementBills = new ObservableCollection<MyBills>(ACBills);


            }


            //TabIdentification = await WebServiceManager.GAZTGetAccountStatementsTabIdentification();
            //HeaderSet = await WebServiceManager.GAZTGetAccountStatementHeaderSet("10", string.Empty, "A",true);

            //var items = new ObservableCollection<ASResult>();
            //foreach (ASResult singleItem in HeaderSet.D.StatmenetLineItemsSet.Results)
            //{

            //    try{

            //        if (singleItem.TaxType != null)
            //        {

            //            if (singleItem.TaxType.Equals("VATX") || singleItem.TaxType.Equals("ETAX"))
            //            {
            //                singleItem.FormattedBldat = string.Format(singleItem.Bldat?.ToString("dd MMMM yyyy", new CultureInfo("en-US")));

            //                string[] dts = singleItem.FormattedBldat.Split(' ');
            //                if (App.IsArabic)
            //                {

            //                    string date = dts[0] + " " + UtilityManager.GetMonthName(dts[1]) + " " + dts[2];

            //                    singleItem.FormattedBldat = date;
            //                }
            //                else
            //                {
            //                    string date = dts[0] + " " + UtilityManager.GetMonthName(dts[1]) + " " + dts[2];

            //                    singleItem.FormattedBldat = date;
            //                }
            //            }
            //            else
            //            {


            //                if (App.CalType.Equals("G"))
            //                {
            //                    singleItem.FormattedBldat = string.Format(singleItem.Bldat?.ToString("dd MMMM yyyy", new CultureInfo("en-US")));

            //                    string[] dts = singleItem.FormattedBldat.Split(' ');
            //                    if (App.IsArabic)
            //                    {

            //                        string date = dts[0] + " " + UtilityManager.GetMonthName(dts[1]) + " " + dts[2];

            //                        singleItem.FormattedBldat = date;
            //                    }
            //                    else
            //                    {
            //                        string date = dts[0] + " " + UtilityManager.GetMonthName(dts[1]) + " " + dts[2];

            //                        singleItem.FormattedBldat = date;
            //                    }
            //                }
            //                else
            //                {

            //                    singleItem.FormattedBldat = string.Format(singleItem.Bldat?.ToString("d/M/yyyy", new CultureInfo("ar-SA")));

            //                    string[] dts = singleItem.FormattedBldat.Split('/');
            //                    if (App.IsArabic)
            //                    {

            //                        string date = dts[0] + " " + UtilityManager.GetMonthNameHijri(dts[1]) + " " + dts[2];

            //                        singleItem.FormattedBldat = date;
            //                    }
            //                    else
            //                    {
            //                        string date = dts[0] + " " + UtilityManager.GetMonthNameHijri(dts[1]) + " " + dts[2];

            //                        singleItem.FormattedBldat = date;
            //                    }
            //                }





            //            }



            //            if (singleItem.TaxType.Equals("VATX") || singleItem.TaxType.Equals("ETAX"))
            //            {
            //                singleItem.FormattedBldat2 = string.Format(singleItem.Bldat2?.ToString("dd MMMM yyyy", new CultureInfo("en-US")));

            //                string[] dts = singleItem.FormattedBldat2.Split(' ');

            //                if (App.IsArabic)
            //                {

            //                    string date = dts[0] + " " + UtilityManager.GetMonthName(dts[1]) + " " + dts[2];

            //                    singleItem.FormattedBldat2 = date;
            //                }
            //                else
            //                {
            //                    string date = dts[0] + " " + UtilityManager.GetMonthName(dts[1]) + " " + dts[2];

            //                    singleItem.FormattedBldat2 = date;
            //                }
            //            }
            //            else
            //            {

            //                singleItem.FormattedBldat2 = string.Format(singleItem.Bldat2?.ToString("d/M/yyyy", new CultureInfo("ar-SA")));
            //                string[] dts = singleItem.FormattedBldat2.Split('/');
            //                if (App.IsArabic)
            //                {

            //                    string date = dts[0] + " " + UtilityManager.GetMonthNameHijri(dts[1]) + " " + dts[2];

            //                    singleItem.FormattedBldat2 = date;
            //                }
            //                else
            //                {
            //                    string date = dts[0] + " " + UtilityManager.GetMonthNameHijri(dts[1]) + " " + dts[2];

            //                    singleItem.FormattedBldat2 = date;
            //                }
            //            }

            //        }
            //    }
            //    catch (Exception e) {


            //    }





            //    items.Add(singleItem);
            //}

            //var newItems = items.OrderByDescending(i => i.Bldat).ToList();

            //AccountStatementsList = new ObservableCollection<ASResult>(newItems);

            if (ACStatementBills != null && ACStatementBills.Count > 2)
            {
                LastTransactionsListHeight = 220;
            }
            else if (ACStatementBills != null && ACStatementBills.Count > 1)
            {
                LastTransactionsListHeight = 150;
            }
            else
            {
                LastTransactionsListHeight = 75;
            }
            IsAccountsStatementLoading = false;

            if (ACStatementBills.Count == 0)
            {
                IsAccountStatementAvilable = false;
            }
            else
            {
                IsAccountStatementAvilable = true;
            }


            //Device.BeginInvokeOnMainThread(() =>
            //{
            //    foreach (TaxRelationSetResult taxRelationSetResult in HeaderSet.D.TaxRelationSet.Results)
            //    {
            //        if (TabIdentification.D?.Direct == "X")
            //        {
            //            if (taxRelationSetResult.StatementFilter == "01")
            //            {
            //                taxRelationSetResult.DisplayId = 01;
            //            }

            //            if (taxRelationSetResult.StatementFilter == "02")
            //            {
            //                taxRelationSetResult.DisplayId = 02;
            //            }

            //            if (taxRelationSetResult.StatementFilter == "03")
            //            {
            //                taxRelationSetResult.DisplayId = 03;
            //            }
            //        }

            //        if (TabIdentification.D?.Indirect == "X")
            //        {
            //            if (taxRelationSetResult.StatementFilter == "06")
            //            {
            //                taxRelationSetResult.DisplayId = 06;
            //            }

            //            if (taxRelationSetResult.StatementFilter == "07")
            //            {
            //                taxRelationSetResult.DisplayId = 07;
            //            }

            //            if (taxRelationSetResult.StatementFilter == "09")
            //            {
            //                taxRelationSetResult.DisplayId = 09;
            //            }
            //        }
            //    }

            //    TaxTypeFilter = new List<TaxRelationSetResult>();

            //    TaxTypeFilter = new List<TaxRelationSetResult>(HeaderSet.D.TaxRelationSet.Results.Where(temp => temp.DisplayId == 01 || temp.DisplayId == 02 || temp.DisplayId == 03 || temp.DisplayId == 06 || temp.DisplayId == 07 || temp.DisplayId == 09).ToList());
            //    if (TaxTypeFilter != null && TaxTypeFilter.Count > 0)
            //        SelectedTaxTypeForFilterValue = TaxTypeFilter.FirstOrDefault();

            //    double tempEndProgressBar = (Convert.ToDouble(HeaderSet.D.DebitAmount));
            //    double startCreditProgressBar = (Convert.ToDouble(HeaderSet.D.CreditAmount.Replace("-", string.Empty)));
            //    double totalBalance = tempEndProgressBar + startCreditProgressBar;

            //    AccStmtnCreditAmount = HeaderSet.D.CreditAmount.Replace("-", string.Empty);

            //    DebitAmountEndProgressBar = (tempEndProgressBar / totalBalance) * 100;
            //    CreditAmountStartProgressBar = (startCreditProgressBar / totalBalance) * 100;

            //    TotalAmountProgressBar = DebitAmountEndProgressBar + CreditAmountStartProgressBar;
            //    MessagingCenter.Send<Object>(this, "UpdateProgressBar");
            //});
        }

        public async Task GetBillsAndReturns()
        {
            App.isMybillsRefresh = false;
            AllBills = new List<OverduePaymentAndUnSubmittedReturn>();
            MyObligationAmount = 0.0;
            var temp1 = new List<OverduePaymentAndUnSubmittedReturn>();
            var pendingBills = new ObservableCollection<OverduePaymentAndUnSubmittedReturn>();


            try
            {
                List<OverduePaymentAndUnSubmittedReturn> TempBills = await WebServiceManager.GAZTGetPaymentOverdueSetForDashboardData(App.IsArabic ? "A" : "E", App.TP.Userid);
                AllBills = TempBills;

           
           

            if (TempBills != null)
            {

                var newItems = TempBills.ToList();

                if (newItems != null)
                {
                    if (newItems.Count > 3)
                    {
                        for (int i = 0; i < 3; i++)
                        {

                            var singleItem = newItems[i];

                            try
                            {

                                if (singleItem.Abtyp != null)
                                {

                                    if (singleItem.Abtyp.Equals("VATX") || singleItem.Abtyp.Equals("ETAX"))
                                    {
                                        singleItem.FormatedDuedate = string.Format(singleItem.DueDtC?.ToString("d/M/yyyy", new CultureInfo("en-US")));

                                        string[] dts = singleItem.FormatedDuedate.Split('/');
                                        if (App.IsArabic)
                                        {

                                            string date = dts[0] + " " + UtilityManager.GetMonthName(dts[1]) + " " + dts[2];

                                            singleItem.FormatedDuedate = date;
                                        }
                                        else
                                        {
                                            string date = dts[0] + " " + UtilityManager.GetMonthName(dts[1]) + " " + dts[2];

                                            singleItem.FormatedDuedate = date;
                                        }
                                    }
                                    else
                                    {


                                        if (singleItem.CalendarTyp.Equals("G"))
                                        {
                                            singleItem.FormatedDuedate = string.Format(singleItem.DueDtC?.ToString("d/M/yyyy", new CultureInfo("en-US")));

                                            string[] dts = singleItem.FormatedDuedate.Split('/');
                                            if (App.IsArabic)
                                            {

                                                string date = dts[0] + " " + UtilityManager.GetMonthName(dts[1]) + " " + dts[2];

                                                singleItem.FormatedDuedate = date;
                                            }
                                            else
                                            {
                                                string date = dts[0] + " " + UtilityManager.GetMonthName(dts[1]) + " " + dts[2];

                                                singleItem.FormatedDuedate = date;
                                            }
                                        }
                                        else
                                        {

                                            singleItem.FormatedDuedate = string.Format(singleItem.DueDtC?.ToString("d/M/yyyy", new CultureInfo("ar-SA")));

                                            string[] dts = singleItem.FormatedDuedate.Split('/');
                                            if (App.IsArabic)
                                            {

                                                string date = dts[0] + " " + UtilityManager.GetMonthNameHijri(dts[1]) + " " + dts[2];

                                                singleItem.FormatedDuedate = date;
                                            }
                                            else
                                            {
                                                string date = dts[0] + " " + UtilityManager.GetMonthNameHijri(dts[1]) + " " + dts[2];

                                                singleItem.FormatedDuedate = date;
                                            }
                                        }


                                    }


                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                Console.Write(ex.ToString());
                                Console.Write(ex.StackTrace.ToString());

                            }


                            pendingBills.Add(singleItem);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < newItems.Count; i++)
                        {
                            var singleItem = newItems[i];

                            try
                            {

                                if (singleItem.Abtyp != null)
                                {

                                    if (singleItem.Abtyp.Equals("VATX") || singleItem.Abtyp.Equals("ETAX"))
                                    {
                                        singleItem.FormatedDuedate = string.Format(singleItem.DueDtC?.ToString("d/M/yyyy", new CultureInfo("en-US")));

                                        string[] dts = singleItem.FormatedDuedate.Split('/');
                                        if (App.IsArabic)
                                        {

                                            string date = dts[0] + " " + UtilityManager.GetMonthName(dts[1]) + " " + dts[2];

                                            singleItem.FormatedDuedate = date;
                                        }
                                        else
                                        {
                                            string date = dts[0] + " " + UtilityManager.GetMonthName(dts[1]) + " " + dts[2];

                                            singleItem.FormatedDuedate = date;
                                        }
                                    }
                                    else
                                    {


                                        if (singleItem.CalendarTyp.Equals("G"))
                                        {
                                            singleItem.FormatedDuedate = string.Format(singleItem.DueDtC?.ToString("d/M/yyyy", new CultureInfo("en-US")));

                                            string[] dts = singleItem.FormatedDuedate.Split('/');
                                            if (App.IsArabic)
                                            {

                                                string date = dts[0] + " " + UtilityManager.GetMonthName(dts[1]) + " " + dts[2];

                                                singleItem.FormatedDuedate = date;
                                            }
                                            else
                                            {
                                                string date = dts[0] + " " + UtilityManager.GetMonthName(dts[1]) + " " + dts[2];

                                                singleItem.FormatedDuedate = date;
                                            }
                                        }
                                        else
                                        {

                                            singleItem.FormatedDuedate = string.Format(singleItem.DueDtC?.ToString("d/M/yyyy", new CultureInfo("ar-SA")));

                                            string[] dts = singleItem.FormatedDuedate.Split('/');
                                            if (App.IsArabic)
                                            {

                                                string date = dts[0] + " " + UtilityManager.GetMonthNameHijri(dts[1]) + " " + dts[2];

                                                singleItem.FormatedDuedate = date;
                                            }
                                            else
                                            {
                                                string date = dts[0] + " " + UtilityManager.GetMonthNameHijri(dts[1]) + " " + dts[2];

                                                singleItem.FormatedDuedate = date;
                                            }
                                        }


                                    }


                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                Console.Write(e.StackTrace.ToString());

                            }

                            pendingBills.Add(singleItem);
                        }
                    }
                    foreach (OverduePaymentAndUnSubmittedReturn ee in newItems)
                    {
                        temp1.Add(ee);
                        if (ee.Amount != null)
                        {
                            MyObligationAmount += Double.Parse(ee.Amount);
                        }
                    }
                }
            }


            if (MyObligationAmount > 0)
            {
                IsMyObligationsClear = false;
                IsBodyMyTaxVisible = true;
            }
            else
            {
                IsMyObligationsClear = true;
                IsBodyMyTaxVisible = false;
            }

            MyObligationAmountCommas = string.Format("{0:N2}", MyObligationAmount);

            Bills = temp1;
            PendingBills = pendingBills;
            if (PendingBills.Count == 0)
            {
                IsPendingBillsVisible = false;
            }
            else
            {
                IsPendingBillsVisible = true;
            }
            if (PendingBills != null && PendingBills.Count > 2)
            {
                PendingBillsListHeight = 250;
            }
            else if (PendingBills != null && PendingBills.Count > 1)
            {
                PendingBillsListHeight = 170;
            }
            else
            {
                PendingBillsListHeight = 83;
            }


            var temp2 = new List<OverduePaymentAndUnSubmittedReturn>();
            List<OverduePaymentAndUnSubmittedReturn> TempReturns = await WebServiceManager.GAZTGetUnSubmittedReturnSetForDashboardData(App.IsArabic ? "A" : "E", App.TP.Userid);
            foreach (OverduePaymentAndUnSubmittedReturn ee in TempReturns)
            {
                temp2.Add(ee);
            }
            Device.BeginInvokeOnMainThread(() => Returns = temp2);

            }
            catch (GAZTErrorException ex)
            {

                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }

        }
        private async Task getDashboardInstalmentPlan()
        {

            InstalmentResponse = WebServiceManager.GAZTGetDashboardInstalmentPlanData(App.IsArabic ? "AR" : "EN", App.TP.Userid);

            var items = new ObservableCollection<InstalmentPlanResult>();

            ChartColorCollection ColorsChild = new ChartColorCollection();
            ColorsChild.Add( (Color)Application.Current.Resources["Primary"]);
            ColorsChild.Add((Color)Application.Current.Resources["NewProgressLightGreenColor"]);
            ColorsChild.Add((Color)Application.Current.Resources["TabGray"]);


            foreach (InstalmentPlanResult singleItem in InstalmentResponse.INST_PLAN_itemSet.results)
            {
                double totalPaidBills = 0;
                double nextBill = 0;
                double unPaidBills = 0;
                var chartData = new ObservableCollection<Model>();
                totalPaidBills = String.IsNullOrEmpty(singleItem.TotalInstPaid) ? 0 : int.Parse(singleItem.TotalInstPaid);
                nextBill = String.IsNullOrEmpty(singleItem.NextInstAmt) ? 0 : 1;
                unPaidBills = String.IsNullOrEmpty(singleItem.TotalInstUnpaid) ? 0 : int.Parse(singleItem.TotalInstUnpaid);
                if (unPaidBills > 0) { unPaidBills = unPaidBills--; }
                chartData.Add(new Model("Paid", totalPaidBills));
                chartData.Add(new Model("nextPayment", nextBill));
                chartData.Add(new Model("Remaining", unPaidBills));

                var doughnutSeries = new DoughnutSeries();

                doughnutSeries.CircularCoefficient = 0.99;
                doughnutSeries.DoughnutCoefficient = 0.85;
                doughnutSeries.ColorModel.Palette = ChartColorPalette.Custom;
                doughnutSeries.ColorModel.CustomBrushes = ColorsChild;
                doughnutSeries.ItemsSource = chartData;
                singleItem.Series = new ChartSeriesCollection() { doughnutSeries };
                singleItem.DayMonthToDisplay = singleItem.Bldat.Value.Day + " " + UtilityManager.GetMonthName(singleItem.Bldat.Value.Month.ToString());


                /* try {

                     if (singleItem.Bldat!= null)
                     {
                         DateTime dateStart = new DateTime();
                         CultureInfo cultureInfo = new CultureInfo("ar-SA");
                         string apiDate = @"""" + singleItem.Bldat + @"""";
                         dateStart = JsonConvert.DeserializeObject<DateTime>(apiDate);

                         GregorianCalendar hjCalendar = new GregorianCalendar();
                         int year = hjCalendar.GetYear(dateStart);
                         int month = hjCalendar.GetMonth(dateStart);
                         int day = hjCalendar.GetDayOfMonth(dateStart);

                         string dateStr = string.Format("{0:00} {1}", day, UtilityManager.GetShortMonthName(""+month));

                         singleItem.nextPaymentDue = dateStr;

                     }
                 }
                 catch(Exception e)
                 {

                 }*/

                items.Add(singleItem);

            }


            InstalmentPlanList = new ObservableCollection<InstalmentPlanResult>();
            InstalmentPlanList = items;


            if (InstalmentPlanList.Count == 0)
            {
                IsInstalmentPlanVisible = false;
            }
            else
            {
                IsInstalmentPlanVisible = true;
            }

        }



        public void PopualateCommittmentsInformation()
        {
            try
            {
                BillsAndReturnsCommitments = new List<OverduePaymentAndUnSubmittedReturn>();
                var BillsAndReturnsCommitmentsTemp = new List<OverduePaymentAndUnSubmittedReturn>();
                // Create events

                if (Bills != null)
                {
                    foreach (var Bill in Bills)
                    {
                        Bill.IsUnSubmittedReturn = false;
                        Bill.IsPaymentOverdue = true;
                        Bill.ColorCode =  (Color)Application.Current.Resources["ErrorColor"];

                        BillsAndReturnsCommitmentsTemp.Add(Bill);
                    }
                }

                if (Returns != null)
                {
                    foreach (var UnsubmittedReturn in Returns)
                    {
                        UnsubmittedReturn.IsUnSubmittedReturn = true;
                        UnsubmittedReturn.IsPaymentOverdue = false;
                        UnsubmittedReturn.ColorCode = (Color)Application.Current.Resources["EntryTextColor"];
                        BillsAndReturnsCommitmentsTemp.Add(UnsubmittedReturn);
                    }
                }

                if (BillsAndReturnsCommitments != null)
                {
                    DateTime Today = DateTime.Now;
                    var BillsAndReturnsCommitmentsLocal = new List<OverduePaymentAndUnSubmittedReturn>();
                    var BillsAndReturnsCommitmentsOverdurItems = new List<OverduePaymentAndUnSubmittedReturn>();//BillsAndReturnsCommitmentsTemp.Where(a => a.DueDateDateTime.Date >= Today.Date).ToList();

                  //  BillsAndReturnsCommitmentsOverdurItems = BillsAndReturnsCommitmentsOverdurItems.OrderByDescending(i => (i.DueDtC)).OrderByDescending(i=>(i.DueDt)).ToList();

                    var tempList = new List<OverduePaymentAndUnSubmittedReturn>();

                    try
                    {
                        if (SelectedCommitmentFilterValue.Equals(AppResources.ZZOverdueCommitments))
                        {
                             BillsAndReturnsCommitmentsOverdurItems = BillsAndReturnsCommitmentsTemp.Where(a =>
                             (a.DueDtC!=null&&DateTime.Compare((DateTime)a.DueDtC, Today) <= 0)
                             ||(a.DueDt != null && DateTime.Compare(Convert.ToDateTime(a.DueDt), Today) <= 0)).ToList();

                            foreach (var item in BillsAndReturnsCommitmentsOverdurItems)
                            {
                                var date = new DateTime();

                                if (item.IsPaymentOverdue)
                                { date = Convert.ToDateTime(item.DueDtC); }
                                else { date = Convert.ToDateTime(item.DueDt);
                                }
                          
                                        if (App.IsArabic)
                                        {
                                            if (item.CalendarTyp.Equals("H") || item.Incotyp.StartsWith("H"))
                                            {
                                            item.Day = UtilityManager.GetMonthNameHijri(Convert.ToDateTime(date).ToString("MMMM", new CultureInfo("en-US")));
                                                if (!item.IsPaymentOverdue)
                                                {
                                                    var hijriDate = UtilityManager.ConvertToHijri(date.ToString("yyyy/MM/dd"));
                                                    string[] splitDate = hijriDate.Split('/');
                                                    item.Month = splitDate[0];
                                                }
                                                else
                                                {
                                                    item.Month = date.Year.ToString();
                                                }
                                            }
                                            else
                                            {
                                                item.Day = UtilityManager.GetMonthName(Convert.ToDateTime(date).ToString("MMMM", new CultureInfo("en-US")));
                                                item.Month = date.Year.ToString();
                                            }
                                        }
                                        else
                                        {
                                            if (item.CalendarTyp.Equals("H")|| item.Incotyp.StartsWith("H"))
                                            {
                                                item.Day = UtilityManager.GetMonthNameHijri(Convert.ToDateTime(date).ToString("MMMM", new CultureInfo("en-US")));
                                                if (!item.IsPaymentOverdue)
                                                {
                                                    var hijriDate = UtilityManager.ConvertToHijri(date.ToString("yyyy/MM/dd"));
                                                    string[] splitDate = hijriDate.Split('/');
                                                    item.Month = splitDate[0];
                                                }
                                                else
                                                {
                                                  item.Month = date.Year.ToString();
                                                }
                                            }
                                            else
                                            {
                                                item.Day = Convert.ToDateTime(date).ToString("MMM", new CultureInfo("en-US"));
                                                item.Month = date.Year.ToString();
                                            }
                                        }
                     
                                
                            }
                        }
                        else if (SelectedCommitmentFilterValue.Equals(AppResources.ZZUpcomingCommitments))
                        {
                            BillsAndReturnsCommitmentsOverdurItems = BillsAndReturnsCommitmentsTemp.Where(a =>
                            (a.DueDtC != null && DateTime.Compare((DateTime)a.DueDtC, Today) > 0)
                            || (a.DueDt != null && DateTime.Compare(Convert.ToDateTime(a.DueDt), Today) > 0)).ToList();
                            foreach (var item in BillsAndReturnsCommitmentsOverdurItems)
                            {
                                var date = new DateTime();

                                if (item.IsPaymentOverdue)
                                { date = Convert.ToDateTime(item.DueDtC); }
                                else
                                {
                                    date = Convert.ToDateTime(item.DueDt);
                                }

                                if (App.IsArabic)
                                {
                                    if (item.CalendarTyp.Equals("H") || item.Incotyp.StartsWith("H"))
                                    {
                                        item.Day = UtilityManager.GetMonthNameHijri(Convert.ToDateTime(date).ToString("MMMM", new CultureInfo("en-US")));
                                        if (!item.IsPaymentOverdue)
                                        {
                                            var hijriDate = UtilityManager.ConvertToHijri(date.ToString("yyyy/MM/dd"));
                                            string[] splitDate = hijriDate.Split('/');
                                            item.Month = splitDate[0];
                                        }
                                        else
                                        {
                                            item.Month = date.Year.ToString();
                                        }
                                    }
                                    else
                                    {
                                        item.Day = UtilityManager.GetMonthName(Convert.ToDateTime(date).ToString("MMMM", new CultureInfo("en-US")));
                                        item.Month = date.Year.ToString();
                                    }
                                }
                                else
                                {
                                    if (item.CalendarTyp.Equals("H") || item.Incotyp.StartsWith("H"))
                                    {
                                        item.Day = UtilityManager.GetMonthNameHijri(Convert.ToDateTime(date).ToString("MMMM", new CultureInfo("en-US")));
                                        if (!item.IsPaymentOverdue)
                                        {
                                            var hijriDate = UtilityManager.ConvertToHijri(date.ToString("yyyy/MM/dd"));
                                            string[] splitDate = hijriDate.Split('/');
                                            item.Month = splitDate[0];
                                        }
                                        else
                                        {
                                            item.Month = date.Year.ToString();
                                        }
                                    }
                                    else
                                    {
                                        item.Day = Convert.ToDateTime(date).ToString("MMM", new CultureInfo("en-US"));
                                        item.Month = date.Year.ToString();
                                    }
                                }


                            }
                        }
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
                        Console.Write(ex.ToString());
                        Console.Write(ex.StackTrace.ToString());
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

                    var BillsAndReturnsCommitmentsLocalDueDTC = BillsAndReturnsCommitmentsLocal.Where(x => x.DueDtC != null).ToList();
                    var BillsAndReturnsCommitmentsLocalDueDT = BillsAndReturnsCommitmentsLocal.Where(x => x.DueDt != null).ToList();
                    BillsAndReturnsCommitmentsLocalDueDT = BillsAndReturnsCommitmentsLocalDueDT.Select(x =>
                    { x.DueDtC = Convert.ToDateTime(x.DueDt);return x; }
                        ).ToList() ;
                    BillsAndReturnsCommitmentsLocal = BillsAndReturnsCommitmentsLocalDueDTC.Concat(BillsAndReturnsCommitmentsLocalDueDT).ToList();

                    BillsAndReturnsCommitmentsLocal = BillsAndReturnsCommitmentsLocal.OrderByDescending(i => (i.DueDtC)).ToList();
                    BillsAndReturnsCommitmentsTemp.Clear();
                    BillsAndReturnsCommitments = BillsAndReturnsCommitmentsLocal;

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
            catch(Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                IsLoading = false;
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

            if (App.IsArabic)
            {

                IsBillsTotalAmountAR = true;
                IsBillsTotalAmountEN = false;
            }
            else
            {
                IsBillsTotalAmountAR = false;
                IsBillsTotalAmountEN = true;

            }



            try
            {
                if (DashboardData.results != null && DashboardData.results.Count > 0)
                {
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

                        MyBillsChartModelsTemp.Add(new MyBillsChartModel { BillCount = PartiallyPaidBillCountAndAmount.BillCount, BillType = PartiallyPaidBillCountAndAmount.BillTypeName, BillColor = (Color)Application.Current.Resources["ColorYellow"] });
                        BillCount = PartiallyPaidBillCountAndAmount.BillCount.ToString();
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

                        MyBillsChartModelsTemp.Add(new MyBillsChartModel { BillCount = UnPaidBillCountAndAmount.BillCount, BillType = UnPaidBillCountAndAmount.BillTypeName, BillColor = (Color)Application.Current.Resources["NewRedColor"] });
                        BillCount = UnPaidBillCountAndAmount.BillCount.ToString();
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
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
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
                            SubmittedCount = RtnTotstr;
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
                            OverDueCount = DueIcrstr;

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
                            UnSubmittedCount = NrtnTotstr;

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
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
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
                        eServicesAvailableToTheTPTemp.Add(new eServiceInfo { eServiceName = AppResources.EstimateZakat, BackgroundGradientStart = "{StaticResource Primary}", BackgroundGradientEnd = "#b6e7fc", iConImagePath = "sf_Estimated_Zakat_Returns.png" });
                    }
                    if (ItemType == "03" || ItemType == "13")
                    {
                        eServicesAvailableToTheTPTemp.Add(new eServiceInfo { eServiceName = AppResources.VatReturns, BackgroundGradientStart = "{StaticResource Primary}", BackgroundGradientEnd = "#b6e7fc", iConImagePath = "sf_VAT_Declaration.png" });
                    }
                }
            }

            eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZZFormBundleStatus, BackgroundGradientStart = "{StaticResource Primary}", BackgroundGradientEnd = "#b6e7fc", iConImagePath = "sf_Form_Bundle_Status.png" });
            eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.Bills, BackgroundGradientStart = "{StaticResource Primary}", BackgroundGradientEnd = "#b6e7fc", iConImagePath = "sf_My_Bills.png" });
            eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.Certificates, BackgroundGradientStart = "{StaticResource Primary}", BackgroundGradientEnd = "#b6e7fc", iConImagePath = "sf_My_Certificate.png" });
            eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZTINStatus, BackgroundGradientStart = "{StaticResource Primary}", BackgroundGradientEnd = "#b6e7fc", iConImagePath = "sf_TIN_Status.png" });

            eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZRealEstateServiceTitle, BackgroundGradientStart = "{StaticResource Primary}", BackgroundGradientEnd = "#b6e7fc", iConImagePath = "sf_Service_6.png" });

            //Tax Evasion Section
            eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZTEReportReportScreenTitle, BackgroundGradientStart = "{StaticResource Primary}", BackgroundGradientEnd = "#b6e7fc", iConImagePath = "sf_Tax_Evasion.png" });
            //Tax Evasion Section
            eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZZZVatLookUpTitleTextNew, BackgroundGradientStart = "{StaticResource Primary}", BackgroundGradientEnd = "#b6e7fc", iConImagePath = "sf_VAT_Lookup.png" });
            //TaxRegistration
            //If tehe value is "X" that means the registration of the user is completed and hence we will not show the Tile.

            try
            {
                if (App.LoginDataRetrieved.VtReg == null)
                {
                    eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZZZZVatRegistrationTile, BackgroundGradientStart = "{StaticResource Primary}", BackgroundGradientEnd = "#b6e7fc", iConImagePath = "sf_VAT_Declaration.png" });
                }
                else if (App.LoginDataRetrieved.VtReg != "X")
                {
                    eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZZZZVatRegistrationTile, BackgroundGradientStart = "{StaticResource Primary}", BackgroundGradientEnd = "#b6e7fc", iConImagePath = "sf_VAT_Declaration.png" });
                }
            }
            catch
            {
                eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZZZZVatRegistrationTile, BackgroundGradientStart = "{StaticResource Primary}", BackgroundGradientEnd = "#b6e7fc", iConImagePath = "sf_VAT_Declaration.png" });
            }
        }

        public async void PopulateStatements(string taxType, string statementFilter, string year)
        {
            try
            {
                HeaderSet = await WebServiceManager.GAZTGetAccountStatementHeaderSet(statementFilter, year, taxType,true);

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
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
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
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            return amountWithComma;
        }
        public async Task LogOut()
        {
           
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
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    Console.Write(ex.StackTrace.ToString());
                }
            Device.BeginInvokeOnMainThread(() =>
            {
                IsLoading = false;
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
                    Console.Write(ex.ToString());
                    Console.Write(ex.StackTrace.ToString());
                }
                _navigationService.NavigateTo($"/{App.SFLoginPageView}", App.GAZTNewDesignDashBoardPageView);
                //_navigationService.GoBack();
            });
        }

        #endregion

        #region Survey
        bool iSEndSurvey;
        public bool ISEndSurvey { get { return iSEndSurvey; } set { iSEndSurvey = value;RaisePropertyChanged(); } }

        public ICommand SurveyNextCommand
        {
            get
            {
                return new Command<string>((currentStep) =>
                {

                   /* if (currentStep== "3")
                    {
                        ISEndSurvey = true;
                    } */
                    SurveyCurrentStep = int.Parse(currentStep);
                  
                    if (SurveyCurrentStep==4)
                    {
                       
                        IsShowMsgView = false;
                    }
                });
            }
        }

        public ICommand SelectedImojieCommand
        {
            get
            {
                return new Command<SurveyQuestions>((selectedAnswer) =>
                {
                    FQanswer =int.Parse(selectedAnswer.ID);
                });
            }
        }

        public ICommand SlectedImojieCommand
        {
            get
            {
                return new Command<SurveyQuestions>((selected) =>
                {

                    SelctedImojy = selected;
                    if (selected.ID== "61c32bf2527cacedb5d31930" || selected.ID== "61c32bf2527cacedb5d3192f")
                    {
                        QuestionTxt = AppResources.SurveyQ2;
                        QNumber = 2;
                    }
                    else
                    {
                        QuestionTxt = AppResources.SurveyQ3;
                        QNumber = 3;

                    }
                    SurveyCurrentStep = 2;
                });
            }
        }

        public ICommand SurveyActionCommand
        {
            get
            {
                return new Command<string>((action) =>
                {
                   
                    if (action =="0")
                    {
                        IsShowMsgView = false;
                    }
                    else
                    {
                        IsSurveyVisible = true;
                        SurveyCurrentStep = 1;
                    }
                });
            }
        }
        public int  SchedukeID { get; set; }

        public ICommand CheckSurveyCommand
        {
            get
            {
                return new Command(async() =>
                {
                    CultureInfo enCul = new CultureInfo("en-US");
                    var Defdate = DateTime.Now.Date.ToString("MM-dd-yyyy", enCul);
                    var date=  Preferences.Get("DateOfSurvey", Defdate);
                 var isSurveyDone=Preferences.Get("IsSurveyTaken", false);

                    if (date== Defdate&& isSurveyDone)
                    {
                        return;
                    }
                 IsShowMsgView = await HaveSurveyForToday();
                 });
            }
        }

        public ICommand AddSurveyCommand
        {
            get
            {
                return new Command<string>(async(isdismiss) =>
                {
                    SurveyCurrentStep = 3;
                    IsLoading = true;
                   await AddSurveyForToday(isdismiss == "0" ? false : true);
                    IsLoading = false;
                });
            }
        }


        public async Task<bool> AddSurveyForToday(bool isDismiss=false)
        {
            CultureInfo enCul = new CultureInfo("en-US");
            var date = DateTime.Now.Date.ToString("MM-dd-yyyy", enCul);
            var tin = App.TP.Tin;
            AddSurveyBody body = new AddSurveyBody()
            {
                dismiss = isDismiss,
                scheduleid = SchedukeID,
                tin =long.Parse( App.TP.Tin)
            };
            var VocBody = new VocAddSurveyAnswerModel()
            {

                collectID = PageSettings.CollectorId,
                surveyID = PageSettings.SurveyID,
                feedback = new List<ResponseArr>()
                {
                 new ResponseArr()
                 {
                     date=new Date()
                     {
                         start=date,
                         end=date
                     },
                     user=new CustomData()
                     {
                         CustomerSegment="TIN",
                          mobile=App.TP.Mobile,
                           firstName=App.TP.Name,
                           TIN=App.TP.Tin
                     },
                     surveyAnswers=new List<Answer>()
                     {
                         new Answer()
                         {
                             questionID=PageSettings.Q1ID,
                             answer=new List<An>()
                             {
                                 new An()
                                 {
                                     rowId=PageSettings.Q1AnsID,
                                     columnID=SelctedImojy.ID
                                 }
                             }
                         },
                         new Answer()
                         {
                             questionID=QNumber==2?PageSettings.Q2ID:PageSettings.Q3ID,
                             answer=new List<An>()
                             {
                                 new An()
                                 {
                                     rowId=QNumber==2?PageSettings.Q2AnsID:PageSettings.Q3AnsID,
                                     text=SQAnswer
                                 }
                             }
                         }
                     }
                 }
                }
            };
            var VocResp = await _surveyServices.AddSurveyAnswerToVoc(VocBody, "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJfaWQiOiI2MGJmNTVjZWI4ZDM5ZjAzZWQ4ZDY0NDIiLCJpYXQiOjE2NjE3NzE5Nzd9.Mkihfva6j2iGT6LV9aQzLFxxCvSMnpBnHhw1Ikz8-OI");
            if (VocResp.IsSuccessStatusCode)
            {
                var res = await _surveyServices.AddSurveyData(body);
                if (res.IsSuccessStatusCode)
                {
                    var DateOfSurvey = DateTime.Now.Date.ToString("MM-dd-yyyy", enCul);
                    Preferences.Set("DateOfSurvey", DateOfSurvey);
                    Preferences.Set("IsSurveyTaken", true);
                }
            }
            ISEndSurvey = true;
                //  IsShowMsgView = false;
            return false;
        }


        public async Task<bool> HaveSurveyForToday()
        {
            CultureInfo enCul = new CultureInfo("en-US");
            var date = DateTime.Now.Date.ToString("MM-dd-yyyy",enCul);
           var res=await _surveyServices.GetSurveyByDate("1012", date);
            if (res.Item2)
            {
                if (res.Item1.data!=null&&(res.Item1.data.isuservotedbefore==false&&res.Item1.data.dismiss==false))
                {
                    
                    SchedukeID = res.Item1.data.id;
                    return true;
                }
            }
            return false;
        }

        #endregion
        public override ICommand MenuNavigationCommand
        {
            get
            {
                return new Command<MenuModel>((Selecteditem) =>
                {
                    if (Selecteditem.ID == "Home")
                    {

                        _navigationService.NavigateTo($"{Selecteditem.ID}", "3");

                        return;
                    }
                    else if (Selecteditem.ID == "menu")
                    {
                        GetDashBoardMenuLst(4);
                        MenuViewVisible = true;
                        HomeViewVisible = false;
                        return;
                    }
                    else if (Selecteditem.ID == "GAZTNewDesignDashBoardPageView")
                    {
                        GetDashBoardMenuLst(1);
                        MenuViewVisible = false;
                        HomeViewVisible = true;
                        return;
                    }
                    _navigationService.NavigateTo($"{Selecteditem.ID}");

                });
            }
        }

    }
}
