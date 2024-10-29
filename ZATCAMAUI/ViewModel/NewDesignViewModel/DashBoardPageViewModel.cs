
using AppDynamics.Agent;
using Mopups.Services;
using Newtonsoft.Json;
using Syncfusion.Maui.Charts;
using Syncfusion.Maui.ProgressBar;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Input;
using ZATCAMAUI.Core.AppConfigurations;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Core.Services.Interface;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.AccountStatements;
using ZATCAMAUI.Models.PaymentModel;
using ZATCAMAUI.Models.SurveyModels;
using ZATCAMAUI.Models.SyncfusionEnabledModels;
using ZATCAMAUI.Views.NewDesign.DashBoardPages;
using ZATCAMAUI.Views.NewDesign.DashBoardPages.PopUpPages;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Views.NewDesign.MyBillsPages;
using ZATCAMAUI.Views.NewDesign.PaymentOptions;
using ReturnType = ZATCAMAUI.Models.SyncfusionEnabledModels.ReturnType;
namespace ZATCAMAUI.ViewModel.NewDesignViewModel.DashBoardPageViewModel
{

    public class GAZTNewDesignDashBoardPageViewModel : BaseViewModel
    {
        #region Variable
        bool isSurveyVisible;
        public bool IsSurveyVisible
        {
            get
            {
                return isSurveyVisible;
            }
            set
            {


                isSurveyVisible = value;
                OnPropertyChanged();
            }
        }
        ////Cr6264
        //private bool _IstileUpdated = true;
        //public bool istileUpdated
        //{
        //    get => _IstileUpdated;
        //    set
        //    {
        //        if (_IstileUpdated == value) return;

        //        _IstileUpdated = value;
        //        OnPropertyChanged("istileUpdated");
        //    }
        //}

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
                OnPropertyChanged();
            }
        }

        int qNumber = 2;
        public int QNumber
        {
            get
            {
                return qNumber;
            }
            set
            {
                qNumber = value;
                OnPropertyChanged();
            }
        }


        int surveyCurrentStep = 0;
        public int SurveyCurrentStep
        {
            get
            {
                return surveyCurrentStep;
            }
            set
            {
                surveyCurrentStep = value;
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }



        ObservableCollection<SurveyQuestions> imojiesLst = new ObservableCollection<SurveyQuestions>() { new SurveyQuestions() { ImojieSource = "Stronglysatisfied", ID = "64087eadfe688b43c294529d" }, new SurveyQuestions() { ImojieSource = "Satisfied", ID = "64087eadfe688b43c294529c" }, new SurveyQuestions() { ImojieSource = "NeitherDissatisfiednorSatisfied", ID = "64087eadfe688b43c294529b" }, new SurveyQuestions() { ImojieSource = "Dissatisfied", ID = "64087eadfe688b43c294529a" }, new SurveyQuestions() { ImojieSource = "Angry", ID = "64087eadfe688b43c2945299" } };
        public ObservableCollection<SurveyQuestions> ImojiesLst
        {
            get
            {
                return new ObservableCollection<SurveyQuestions>(imojiesLst?.Reverse());
            }
            set
            {
                imojiesLst = value;
                OnPropertyChanged();
            }
        }



        public string selectedFbNum = "";
        public string selectedSadadNo = "";
        public string selectedTaxablePeriod = "";
        public string selectedAmount = "";

        private string _sadadBindNumber = "";
        OverduePaymentAndUnSubmittedReturn BModel = null;

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
                OnPropertyChanged("SadadBindNumber");
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

                OnPropertyChanged("MultiplePayableBills");
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
                OnPropertyChanged("TotalAmount");
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
                OnPropertyChanged("TaxablePeriod");
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
                OnPropertyChanged("ReferenceNumber");
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
                OnPropertyChanged(nameof(currentTab));
                CurrentIndex = (int)_currentTab;
                OnPropertyChanged(nameof(CurrentIndex));
            }
        }

        //istileUpdated CR6264

        private bool _IstileUpdated = true;
        public bool istileUpdated
        {
            get => _IstileUpdated;
            set
            {
                if (_IstileUpdated == value) return;

                _IstileUpdated = value;
                OnPropertyChanged("istileUpdated");
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
            }
        }

        private string _vatProfitGoodsTileTxt = string.Empty;
        public string VatProfitGoodsTileTxt
        {
            get => _vatProfitGoodsTileTxt;
            set
            {
                if (_vatProfitGoodsTileTxt == value) return;

                _vatProfitGoodsTileTxt = value;
                OnPropertyChanged("VatProfitGoodsTileTxt");
            }
        }

        #endregion

        #region Fields

        private ICommand EserviceCommand { get; set; }
        private TaxPayerProfile _TaxPayerProfile = App.TP;

        private bool _menuViewVisible = false;
        private bool _homeViewVisible = true;
        private bool _accountStatementVisible = false;
        private bool _IsToolbarTaxVisible = true;
        private bool _liveChatVisible = false;
        private Color _homeIndicatorColor = (Color)Application.Current.Resources["Primary"];
        private Color _menuIndicatorColor = Color.FromRgb(255, 255, 255);
        private Color _tabbarColor = Color.FromRgb(169, 169, 169);
        private Color _stackMenuColor = Color.FromRgb(255, 255, 255);
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
        //private CalendarEventCollection _CommittmentsSchedule = null;
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
                return _BillsAndReturnsCommitments;
            }
            set
            {
                if (_BillsAndReturnsCommitments == value) return;

                if (value != null)
                {
                    _BillsAndReturnsCommitments = value;
                    OnPropertyChanged("BillsAndReturnsCommitments");
                }
            }
        }

        private ObservableCollection<ASResult> _AccountStatementsList { get; set; }

        public ObservableCollection<ASResult> AccountStatementsList
        {
            get
            {
                return _AccountStatementsList;
            }
            set
            {
                if (_AccountStatementsList == value) return;

                if (value != null)
                {
                    _AccountStatementsList = value;
                    OnPropertyChanged("AccountStatementsList");
                }
            }
        }

        private List<OverduePaymentAndUnSubmittedReturn> allBills { get; set; }
        public List<OverduePaymentAndUnSubmittedReturn> AllBills
        {
            get
            {
                return allBills;
            }
            set
            {
                if (allBills == value) return;

                if (value != null)
                {
                    allBills = value;
                    OnPropertyChanged("AllBills");
                }
            }
        }
        private ObservableCollection<OverduePaymentAndUnSubmittedReturn> _PendingBills { get; set; }

        public ObservableCollection<OverduePaymentAndUnSubmittedReturn> PendingBills
        {
            get
            {
                return _PendingBills;
            }
            set
            {
                if (_PendingBills == value) return;

                if (value != null)
                {
                    _PendingBills = value;
                    OnPropertyChanged("PendingBills");
                }
            }
        }

        private ObservableCollection<InstalmentPlanResult> _InstalmentPlanList { get; set; }

        public ObservableCollection<InstalmentPlanResult> InstalmentPlanList
        {
            get
            {
                return _InstalmentPlanList;
            }
            set
            {
                if (_InstalmentPlanList == value) return;

                if (value != null)
                {
                    _InstalmentPlanList = value;
                    OnPropertyChanged("InstalmentPlanList");
                }
            }
        }
        public ChartColorCollection _InstalmentColors { get; set; }

        public ChartColorCollection InstalmentColors
        {
            get
            {
                return _InstalmentColors;
            }
            set
            {
                if (_InstalmentColors == value) return;

                if (value != null)
                {
                    _InstalmentColors = value;
                    OnPropertyChanged("InstalmentColors");
                }
            }
        }

        public List<ObservableCollection<ChartDataPoint>> _InstalmentDoughnutSeriesData { get; set; }
        public List<ObservableCollection<ChartDataPoint>> InstalmentDoughnutSeriesData
        {
            get
            {
                return _InstalmentDoughnutSeriesData;
            }
            set
            {
                if (_InstalmentDoughnutSeriesData == value) return;

                if (value != null)
                {
                    _InstalmentDoughnutSeriesData = value;
                    OnPropertyChanged("InstalmentDoughnutSeriesData");
                }
            }
        }

        public async Task getActivityUpdateStatus()
        {

            await Task.Run(async () =>
            {
                DashBoardUpdateViewResponseModel dashBoardUpdateViewResponse = await WebServiceManager.getTaxPayerActivityUpdateStatus();

                PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                if (dashBoardUpdateViewResponse != null && dashBoardUpdateViewResponse.d != null && dashBoardUpdateViewResponse.d.results != null
                && dashBoardUpdateViewResponse.d.results.Count > 0 && dashBoardUpdateViewResponse.d.results[0] != null
                && !string.IsNullOrEmpty(dashBoardUpdateViewResponse.d.results[0].Msg))
                    await MopupService.Instance.PushAsync(new UpdateActivityInstructionsPageView(false, dashBoardUpdateViewResponse.d.results[0].Msg));
            });

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
                    this.OnPropertyChanged("TaxTypeFilter");
                }
            }
        }
        public List<MyBillsChartModel> MyBillsChartModels
        {
            get
            {
                return _MyBillsChartModels;
            }
            set
            {
                if (_MyBillsChartModels == value) return;
                if (value != null)
                {
                    _MyBillsChartModels = value;
                    this.OnPropertyChanged("MyBillsChartModels");
                }
            }
        }
        public List<OverduePaymentAndUnSubmittedReturn> Bills
        {
            get
            {
                return _Bills;
            }
            set
            {
                if (_Bills == value) return;

                if (value != null)
                {
                    _Bills = value;
                    this.OnPropertyChanged("Bills");
                }
            }
        }
        public List<ReturnTypeAndCorrepsondingCount> SegregatedReturnTypesAndCorrepsondingCounts
        {
            get
            {
                return _SegregatedReturnTypesAndCorrepsondingCounts;
            }
            set
            {
                if (_SegregatedReturnTypesAndCorrepsondingCounts == value) return;

                if (value != null)
                {
                    _SegregatedReturnTypesAndCorrepsondingCounts = value;
                    this.OnPropertyChanged("SegregatedReturnTypesAndCorrepsondingCounts");
                }
            }
        }
        public List<OverduePaymentAndUnSubmittedReturn> Returns
        {
            get
            {
                return _Returns;
            }
            set
            {
                if (_Returns == value) return;

                if (value != null)
                {
                    _Returns = value;
                    this.OnPropertyChanged("Returns");
                }
            }
        }
        public List<eServiceInfo> eServicesAvailableToTheTP
        {
            get
            {
                return _eServices;
            }
            set
            {
                if (_eServices == value) return;

                if (value != null)
                {
                    _eServices = value;
                    this.OnPropertyChanged("eServicesAvailableToTheTP");
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
                OnPropertyChanged("IsAccountsStatementLoading");
            }
        }

        private bool isRefundreqMenuVisible;
        public bool IsRefundreqMenuVisible
        {
            get
            {
                return isRefundreqMenuVisible;
            }
            set
            {
                isRefundreqMenuVisible = value;
                OnPropertyChanged("IsRefundreqMenuVisible");
            }
        }

        private bool isRefundreqMenuBoxVisible;
        public bool IsRefundreqMenuBoxVisible
        {
            get
            {
                return isRefundreqMenuBoxVisible;
            }
            set
            {
                isRefundreqMenuBoxVisible = value;
                OnPropertyChanged("IsRefundreqMenuBoxVisible");
            }
        }

        private bool isFillingMenuVisible;
        public bool IsFillingMenuVisible
        {
            get
            {
                return isFillingMenuVisible;
            }
            set
            {
                isFillingMenuVisible = value;
                OnPropertyChanged("IsFillingMenuVisible");
            }
        }

        private bool isFillingMenuBoxVisible;
        public bool IsFillingMenuBoxVisible
        {
            get
            {
                return isFillingMenuBoxVisible;
            }
            set
            {
                isFillingMenuBoxVisible = value;
                OnPropertyChanged("IsFillingMenuBoxVisible");
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
                OnPropertyChanged("AppVersion");
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
                OnPropertyChanged("HeaderSet");
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
                OnPropertyChanged("TabIdentification");
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
                OnPropertyChanged("InstalmentResponse");
            }
        }



        private string _taxpayerName;
        public string TaxpayerName
        {
            get
            {
                return _taxpayerName;
            }
            set
            {
                if (_taxpayerName == value) return;

                _taxpayerName = value;
                OnPropertyChanged("TaxpayerName");
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
                OnPropertyChanged("ApplePayStatus");
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
                OnPropertyChanged("IfnotRegInVATAndZakat");
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
                OnPropertyChanged("IsAccountStatementAvilable");
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
                OnPropertyChanged("IfRegInZakat");
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
                OnPropertyChanged("IfRegInVAT");
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
                OnPropertyChanged("IsInstalmentPlanVisible");
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
                OnPropertyChanged("IfSignUpnNotRegInVAT");
            }
        }

        private bool _isGrpVATReg = false;
        public bool ISGRPVATReg
        {
            get
            {
                return _isGrpVATReg;
            }
            set
            {
                if (_isGrpVATReg == value) return;
                _isGrpVATReg = value;
                OnPropertyChanged("ISGRPVATReg");
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
                OnPropertyChanged("IfSignUpnNotRegInVATShowVATServie");
            }
        }

        //public CalendarEventCollection CommittmentsSchedule
        //{
        //    get
        //    {
        //        return _CommittmentsSchedule;
        //    }
        //    set
        //    {
        //        if (_CommittmentsSchedule == value)
        //        {
        //            return;
        //        }
        //        _CommittmentsSchedule = value;
        //        this.OnPropertyChanged("CommittmentsSchedule");
        //    }
        //}

        private double _debitAmountEndProgressBar = 0;
        public double DebitAmountEndProgressBar
        {
            get => _debitAmountEndProgressBar;
            set
            {
                if (_debitAmountEndProgressBar == value) return;

                _debitAmountEndProgressBar = value;
                OnPropertyChanged(nameof(DebitAmountEndProgressBar));
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
                OnPropertyChanged(nameof(CreditAmountStartProgressBar));
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
                OnPropertyChanged(nameof(_totalAmountProgressBar));
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

                OnPropertyChanged("SelectedTaxTypeForFilterValue");
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

                OnPropertyChanged("SelectedTaxTypeForFilter");
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
                OnPropertyChanged("Colors");
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
                OnPropertyChanged("WelcomeText");
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
                OnPropertyChanged("Rotation");
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
                OnPropertyChanged("TranslateText");
            }
        }


        private string _billCount;
        public string BillCount
        {
            get
            {
                return _billCount;
            }
            set
            {
                if (_billCount == value) return;

                _billCount = value;
                this.OnPropertyChanged("BillCount");
            }
        }

        public string PaidBillCount
        {
            get
            {
                return _paidbillCount;
            }
            set
            {
                if (_paidbillCount == value) return;

                _paidbillCount = value;
                this.OnPropertyChanged("PaidBillCount");
            }
        }

        public string PartiallyPaidBillCount
        {
            get
            {
                return _partiallypaidbillCount;
            }
            set
            {
                if (_partiallypaidbillCount == value) return;

                _partiallypaidbillCount = value;
                this.OnPropertyChanged("PartiallyPaidBillCount");
            }
        }

        public string UnPaidBillCount
        {
            get
            {
                return _unPaidbillCount;
            }
            set
            {
                if (_unPaidbillCount == value) return;

                _unPaidbillCount = value;
                this.OnPropertyChanged("UnPaidBillCount");
            }
        }

        private string _nDCommitments = AppResources.NDCommitments;
        public string NDCommitments
        {
            get
            {
                return _nDCommitments;
            }
            set
            {
                if (_nDCommitments == value) return;

                _nDCommitments = value;
                this.OnPropertyChanged("NDCommitments");
            }
        }

        private string _zBills = AppResources.Bills;
        public string ZBills
        {
            get
            {
                return _zBills;
            }
            set
            {
                if (_zBills == value) return;

                _zBills = value;
                this.OnPropertyChanged("ZBills");
            }
        }

        private string _returns = AppResources.Returns;
        public string Return
        {
            get
            {
                return _returns;
            }
            set
            {
                if (_returns == value) return;

                _returns = value;
                this.OnPropertyChanged("Return");
            }
        }

        private string _aboutUs = AppResources.ZZZAboutUs;
        public string AboutUs
        {
            get
            {
                return _aboutUs;
            }
            set
            {
                if (_aboutUs == value) return;

                _aboutUs = value;
                this.OnPropertyChanged("AboutUs");
            }
        }

        private string _contactus = AppResources.ZZZContactus;
        public string Contactus
        {
            get
            {
                return _contactus;
            }
            set
            {
                if (_contactus == value) return;

                _contactus = value;
                this.OnPropertyChanged("Contactus");
            }
        }

        private bool _setMyCommitmentsCollectionVisibility = false;
        public bool SetMyCommitmentsCollectionVisibility
        {
            get
            {
                return _setMyCommitmentsCollectionVisibility;
            }
            set
            {
                if (_setMyCommitmentsCollectionVisibility == value) return;

                _setMyCommitmentsCollectionVisibility = value;
                this.OnPropertyChanged("SetMyCommitmentsCollectionVisibility");
            }
        }

        private bool _setNoCommitmentsAvailableLabelVisibility = true;
        public bool SetNoCommitmentsAvailableLabelVisibility
        {
            get
            {
                return _setNoCommitmentsAvailableLabelVisibility;
            }
            set
            {
                if (_setNoCommitmentsAvailableLabelVisibility == value) return;

                _setNoCommitmentsAvailableLabelVisibility = value;
                this.OnPropertyChanged("SetNoCommitmentsAvailableLabelVisibility");
            }
        }

        private string _privacyandPolicy = AppResources.ZZZPrivacyandPolicy;
        public string PrivacyandPolicy
        {
            get
            {
                return _privacyandPolicy;
            }
            set
            {
                if (_privacyandPolicy == value) return;

                _privacyandPolicy = value;
                this.OnPropertyChanged("PrivacyandPolicy");
            }
        }
        private string _profitongoods = AppResources.ZProfitOnGoods;
        public string ProfitOnGoods
        {
            get
            {
                return this._profitongoods;
            }
            set
            {
                if (_profitongoods == value) return;

                this._profitongoods = value;
                this.OnPropertyChanged("ProfitOnGoods");
            }
        }

        private string _logout = AppResources.ZLogout;
        public string Logout
        {
            get
            {
                return _logout;
            }
            set
            {
                if (_logout == value) return;

                _logout = value;
                this.OnPropertyChanged("Logout");
            }
        }


        public TaxPayerProfile TaxPayerProfile
        {
            get
            {
                return _TaxPayerProfile;
            }
            set
            {
                if (_TaxPayerProfile == value) return;

                _TaxPayerProfile = value;
                this.OnPropertyChanged("TaxPayerProfile");
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
                OnPropertyChanged("IsVatRegistrationTileVisible");
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
                OnPropertyChanged("IsRegistrationDetailsTileVisible");
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
                OnPropertyChanged("IsVatAmendmentTileVisible");
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
                OnPropertyChanged("IsEstablishmentRegistrationTileVisible");
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
                OnPropertyChanged("IsSubsidyTileVisible");
            }

            /* Unmerged change from project 'ZATCAMAUI (net7.0-android33.0)'
            Before:
                    }

                    public bool MenuViewVisible
            After:
                    }

                    public bool MenuViewVisible
            */
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

                _menuViewVisible = value;
                this.OnPropertyChanged("MenuViewVisible");
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

                _isBillsTotalAmountAR = value;
                this.OnPropertyChanged("IsBillsTotalAmountAR");
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

                _isBillsTotalAmountEN = value;
                this.OnPropertyChanged("IsBillsTotalAmountEN");
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

                _homeViewVisible = value;
                if (_homeViewVisible != null)
                {
                    if (_homeViewVisible)
                    {
                        _homeIndicatorColor = (Color)Application.Current.Resources["Primary"];
                        MenuIndicatorColor = Color.FromRgb(255, 255, 255);
                    }

                }
                this.OnPropertyChanged("HomeViewVisible");
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

                _accountStatementVisible = value;
                if (_accountStatementVisible != null)
                {
                    if (_accountStatementVisible)
                    {
                        _homeIndicatorColor = (Color)Application.Current.Resources["Primary"];
                        MenuIndicatorColor = Color.FromRgb(255, 255, 255);
                    }

                }
                this.OnPropertyChanged("AccountStatementVisible");
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
                    _homeIndicatorColor = (Color)Application.Current.Resources["Primary"];
                    MenuIndicatorColor = Color.FromRgb(255, 255, 255);
                }

                this.OnPropertyChanged("LiveChatVisible");
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

                _homeIndicatorColor = value;
                this.OnPropertyChanged("HomeIndicatorColor");
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

                _menuIndicatorColor = value;
                this.OnPropertyChanged("MenuIndicatorColor");
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

                _tabbarColor = value;
                this.OnPropertyChanged("TabbarColor");
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

                _stackMenuColor = value;
                this.OnPropertyChanged("StackMenuColor");
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

                _NextCommitmentsString = value;
                this.OnPropertyChanged("NextCommitmentsString");
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

                _ReturnString = value;
                this.OnPropertyChanged("ReturnString");
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

                _PaidString = value;
                this.OnPropertyChanged("PaidString");
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

                _PartiallyPaidString = value;
                this.OnPropertyChanged("PartiallyPaidString");
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

                _UnPaidString = value;
                this.OnPropertyChanged("UnPaidString");
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

                _BillString = value;
                this.OnPropertyChanged("BillString");
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

                _TotalString = value;
                this.OnPropertyChanged("TotalString");
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
                OnPropertyChanged("AccStmtnCreditAmount");
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
                    OnPropertyChanged("SelectedCommitmentFilterValue");

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
                    OnPropertyChanged("SelectedCommitmentFilterLabelValue");

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

                _IsToolbarTaxVisible = value;

                this.OnPropertyChanged("IsToolbarTaxVisible");
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

                _isMenuLogoVisible = value;

                this.OnPropertyChanged("IsMenuLogoVisible");
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
                OnPropertyChanged("LastTransactionsListHeight");
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
                OnPropertyChanged("PendingBillsListHeight");
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
                OnPropertyChanged("IsPendingBillsVisible");
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
                    OnPropertyChanged("SubmittedCount");

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
                    OnPropertyChanged("UnSubmittedCount");

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
                    OnPropertyChanged("OverDueCount");

                }

            }
        }

        private double _MyObligationAmount = 0.0;
        public double MyObligationAmount
        {
            get
            {
                return _MyObligationAmount;
            }
            set
            {
                _MyObligationAmount = value;
                OnPropertyChanged("MyObligationAmount");
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
                OnPropertyChanged("MyObligationAmountCommas");
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
                OnPropertyChanged("IsMyObligationsClear");
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
                OnPropertyChanged("IsBodyMyTaxVisible");
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
                OnPropertyChanged("PaymentData");
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
                OnPropertyChanged("DayMonth");
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

                OnPropertyChanged("ACStatementBills");
            }
        }

        private bool _isContactZatcaEmpTileVisible = false;
        public bool IsContactZatcaEmpTileVisible
        {
            get
            {
                return _isContactZatcaEmpTileVisible;
            }
            set
            {
                if (_isContactZatcaEmpTileVisible == value) return;

                _isContactZatcaEmpTileVisible = value;
                OnPropertyChanged("IsContactZatcaEmpTileVisible");
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

        }
        #endregion

        #region Method
        public bool isTimerOff = false;
        public bool isFirstTime = true;

        public void RefreshDashboardCommand()
        {
            if (MenuViewVisible)
            {

                var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "OnMenuTapped", AppResources.ZZZMenu + " Page");
                MenuViewVisible = true;
                HomeViewVisible = false;
                AccountStatementVisible = false;
                LiveChatVisible = false;
                HomeIndicatorColor = Microsoft.Maui.Graphics.Colors.White;
                MenuIndicatorColor = (Color)Application.Current.Resources["Primary"];
                StackMenuColor = Microsoft.Maui.Graphics.Colors.Transparent;
                TabbarColor = Microsoft.Maui.Graphics.Colors.Transparent;
                IsToolbarTaxVisible = false;

                IsToolbarTaxVisible = false;


                Instrumentation.EndCall(callTracker);
            }
            else
            {

                var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "OnHomeTapped", "Home Page");
                MenuViewVisible = false;
                HomeViewVisible = true;
                AccountStatementVisible = false;
                LiveChatVisible = false;
                HomeIndicatorColor = (Color)Application.Current.Resources["Primary"];
                MenuIndicatorColor = Microsoft.Maui.Graphics.Colors.White;
                TabbarColor = Microsoft.Maui.Graphics.Colors.DarkGray;
                StackMenuColor = Microsoft.Maui.Graphics.Colors.White;

                IsToolbarTaxVisible = true;

                IsToolbarTaxVisible = true;

                Instrumentation.EndCall(callTracker);
            }
        }

        public async Task OnAppearing()
        {
            try
            {
                await OnDataLoad();
                RefreshDashboardCommand();
                getYesCommandToLogout();


                isPayNowTapped = false;

                NextCommitmentsString = AppResources.ZZMyCommitments;
                PaidString = AppResources.Paid + " " + PaidBillCount;
                UnPaidString = AppResources.UnPaid + " " + UnPaidBillCount;
                PartiallyPaidString = AppResources.Partiallynewui + " " + PartiallyPaidBillCount;
                TotalString = AppResources.NDTotalNumberOfBills;
                MessagingCenter.Subscribe<object>(this, "UpdateProgressBar", (sender) =>
                {
                    SfLinearProgressBar rangeColors = new SfLinearProgressBar();
                    rangeColors.GradientStops.Add(new ProgressGradientStop
                    {
                        Color = (Color)Application.Current.Resources["Green"],
                        Value = 0
                    });
                    rangeColors.GradientStops.Add(new ProgressGradientStop
                    {
                        Color = (Color)Application.Current.Resources["Error"],
                        Value = 100
                    });

                });
                isTimerOff = false;
                StartTimer();
                IsLoading = false;






                if (App.isMybillsRefresh)
                {

                    await LoadDashboardData();

                }


                //code to refresh Dashboard Returns count 

                if (SubmittedCount != null)
                {

                    IsLoading = true;

                    DashboardData = await WebServiceManager.GAZTGetDashboardData(UtilityManager.GetLanguageParameter(), App.TP.userId);

                    PopulateReturnsInformation();
                    IsLoading = false;

                }

                MessagingCenter.Unsubscribe<object, string>(this, "MultipleBillsContinue");
                MessagingCenter.Subscribe<object, string>(this, "MultipleBillsContinue", async (sender, arg) =>
                {
                    await ShowPaymentOptions();
                    isPayNowTapped = false;

                });
                MessagingCenter.Subscribe<object, string>(this, "Card_Payment", (sender, arg) =>
                {
                    MadaPaymentSelected();
                    isPayNowTapped = false;

                });
                MessagingCenter.Subscribe<object, string>(this, "SADAD", async (sender, arg) =>
                {
                    await SadadPaymentSelected();
                    isPayNowTapped = false;
                });

            }
            catch (Exception)
            {


            }
        }

        public async Task OpenBrowser(Uri uri)
        {
            await Launcher.OpenAsync(uri);
        }

        public void getYesCommandToLogout()
        {
            try
            {
                MessagingCenter.Subscribe<object, string>(this, "YesPressedToLogout", async (sender, arg) =>
                {
                    App.TP = null;
                    await LogOut();
                });
            }
            catch (Exception)
            {


            }
        }


        private async Task LoadData()
        {
            try
            {
                IsLoading = true;


                if (App.TP != null)
                {
                    await LoadDashboardData();

                }

                BillCount = string.Empty;
                BillsAndReturnsCommitments = new List<OverduePaymentAndUnSubmittedReturn>();

                //To load default commintments 
                SelectedCommitmentFilterLabelValue = CommitmentsListFilter[0];
                SelectedCommitmentFilterValue = CommitmentsListFilter[0];

                PopulateBillsInformation();
                PopulateReturnsInformation();
                PopualateCommittmentsInformation();


                IsLoading = false;
            }
            finally { IsLoading = false; }
        }

        public async Task OnDataLoad()
        {

            App.IsComingFromSleepMode = false;


            try
            {
                await LoadData();

                if (App.LoginDataRetrieved != null)
                {
                    if (App.TP.VtpmFg == "X")
                    {
                        VatProfitGoodsTileTxt = AppResources.VATProfitDeregisterTile;
                    }
                    else
                    {
                        VatProfitGoodsTileTxt = AppResources.ZProfitgoodsSCSRTile;
                    }
                    istileUpdated = true;
                    if (App.LoginDataRetrieved.ZkReg == "X")
                    {
                        IsEstablishmentRegistrationTileVisible = false;
                        IsVatRegistrationTileVisible = true;
                        IsRegistrationDetailsTileVisible = true;
                        IfRegInZakat = true;
                        IsRefundreqMenuVisible = IsRefundreqMenuBoxVisible = false;
                        IsFillingMenuVisible = IsFillingMenuBoxVisible = false;
                    }
                    else if (App.LoginDataRetrieved.ZkReg == "U")
                    {
                        IsEstablishmentRegistrationTileVisible = true;
                        IsRegistrationDetailsTileVisible = false;

                    }
                    else if (App.LoginDataRetrieved.ZkReg == "N")
                    {
                        IsEstablishmentRegistrationTileVisible = false;
                        IsRegistrationDetailsTileVisible = false;
                    }

                    if (App.LoginDataRetrieved.VtReg == "X")
                    {
                        IsVatRegistrationTileVisible = false;
                        IfRegInZakat = false;
                        IsSubsidyTileVisible = true;

                    }
                    else if (App.LoginDataRetrieved.VtReg == "R")
                    {
                        IsVatRegistrationTileVisible = false;
                        IfRegInZakat = false;
                        IfnotRegInVATAndZakat = true;
                        IfSignUpnNotRegInVATShowVATServie = true;
                        IsSubsidyTileVisible = false;

                    }
                    else if (App.LoginDataRetrieved.VtReg == "")
                    {
                        IfSignUpnNotRegInVATShowVATServie = false;
                    }

                    if (App.LoginDataRetrieved.ZkSignup == "X")
                    {
                        if (App.LoginDataRetrieved.ZkReg == string.Empty)
                        {
                            IsVatRegistrationTileVisible = false;
                            IsEstablishmentRegistrationTileVisible = true;
                        }

                    }
                    else if (App.LoginDataRetrieved.VtSignup == "X")
                    {
                        if (App.LoginDataRetrieved.VtReg == string.Empty)
                        {
                            IsEstablishmentRegistrationTileVisible = false;
                            IsVatRegistrationTileVisible = true;
                            IfSignUpnNotRegInVAT = true;
                            IsSubsidyTileVisible = false;
                        }
                    }
                    if (App.LoginDataRetrieved.ZkReg == "X" && App.LoginDataRetrieved.VtReg == "X")
                    {
                        IfRegInZakat = true;
                        IsRefundreqMenuVisible = IsRefundreqMenuBoxVisible = true;
                        IsFillingMenuVisible = IsFillingMenuBoxVisible = true;


                    }

                    if ((App.LoginDataRetrieved.VtSignup == "X" || App.LoginDataRetrieved.ZkSignup == "X") && (App.LoginDataRetrieved.ZkReg == string.Empty && App.LoginDataRetrieved.VtReg == string.Empty))
                    {
                        IfnotRegInVATAndZakat = false;
                    }
                    if (App.LoginDataRetrieved.VtReg == "X")
                    {
                        IfnotRegInVATAndZakat = true;
                        IfSignUpnNotRegInVATShowVATServie = true;
                        IfSignUpnNotRegInVAT = false;
                    }
                    else if (App.LoginDataRetrieved.VtReg == string.Empty)
                    {
                        IfSignUpnNotRegInVATShowVATServie = false;
                        IfSignUpnNotRegInVAT = true;
                    }
                    else if (App.LoginDataRetrieved.ZkReg == "X")
                    {
                        IfSignUpnNotRegInVAT = true;
                    }
                    if (App.LoginDataRetrieved.ZkReg == "X")
                    {
                        IfnotRegInVATAndZakat = true;
                    }
                    if (App.LoginDataRetrieved.CozatcaTile == "X")
                    {
                        IsContactZatcaEmpTileVisible = true;
                    }
                }

            }
            catch
            {
                IsVatRegistrationTileVisible = true;
            }

            App.HasToRefreshLoaderOnDashboard = false;


        }


        private void StartTimer()
        {
            int counter = 120;
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                counter = counter - 1;
                if (counter == 0)
                {
                    if (App.TP != null)
                    {

                        counter = 120;
                        App.HasToRefreshLoaderOnDashboard = true;
                        OnDataLoad();
                    }
                    else
                    {
                        isTimerOff = true;

                    }


                }
                return !isTimerOff;
            });
        }

        public void SetRTLDirection()
        {
            try
            {
                string langName = "ar-AE";
                CultureInfo ci = new CultureInfo(langName);
                AppResources.Culture = ci;
                TranslateText = "English";

                NextCommitmentsString = AppResources.ZZMyCommitments;
                BillString = AppResources.ZZZDBMyPayments;
                ReturnString = AppResources.ZZZDBMyReturns;

                PaidString = AppResources.Paid + " " + PaidBillCount;
                UnPaidString = AppResources.UnPaid + " " + UnPaidBillCount;
                PartiallyPaidString = AppResources.Partiallynewui + " " + PartiallyPaidBillCount;
                TotalString = AppResources.NDTotalNumberOfBills;
                WelcomeText = AppResources.ZZZWelcomeOnLanding;
                Rotation = 180;
            }
            catch (Exception)
            {


            }
        }

        public void SetLTRDirection()
        {
            try
            {
                string langName = "en-US";
                CultureInfo ci = new CultureInfo(langName);
                AppResources.Culture = ci;
                TranslateText = "عربي";

                NextCommitmentsString = AppResources.ZZMyCommitments;
                BillString = AppResources.ZZZDBMyPayments;
                ReturnString = AppResources.ZZZDBMyReturns;

                PaidString = AppResources.Paid + " " + PaidBillCount;
                UnPaidString = AppResources.UnPaid + " " + UnPaidBillCount;
                PartiallyPaidString = AppResources.Partiallynewui + " " + PartiallyPaidBillCount;
                TotalString = AppResources.NDTotalNumberOfBills;
                WelcomeText = AppResources.ZZZWelcomeOnLanding;
                Rotation = 0;
            }
            catch (Exception)
            {


            }

        }

        public async Task verifyPaymentAndShowBillsPopup(OverduePaymentAndUnSubmittedReturn BModel)
        {
            this.BModel = BModel;
            var newMultiplePayableBills = new ObservableCollection<MyBills>();

            foreach (var item in AllBills)
            {

                newMultiplePayableBills.Add(new MyBills
                {
                    VTRE2 = item.sadadBillNumber,
                    MadabutFg = item.MadabutFg,
                    TestDueAmount = item.amount,
                    //FormatedFaedn=item.FormatedDuedate,
                    StatusText = item.ICRStatus,
                    Fbnum = item.formBundleNumber,
                    Txt30 = item.taxTypeDescription

                });
            }

            if (AllBills != null && AllBills.Count > 0)
            {
                MultiplePayableBills = new ObservableCollection<MyBills>(newMultiplePayableBills.Where(x => !string.IsNullOrEmpty(BModel.sadadBillNumber) && x.VTRE2.Equals(BModel.sadadBillNumber)).ToList());
            }

            if (MultiplePayableBills != null && MultiplePayableBills.Count > 1)
            {
                await MopupService.Instance.PushAsync(new MyBillsMultiplePayableList(MultiplePayableBills));
                await Task.Delay(2000);
                isPayNowTapped = false;
                return;
            }
            else
            {
                await ShowPaymentOptions();
            }
        }

        public async Task ShowPaymentOptions()
        {

            if (BModel != null)
            {
                if (BModel.MadabutFg == "X")
                {
                    await MopupService.Instance.PushAsync(new PaymentOptionsPageView(true, false, false, ""));
                }
                else
                {
                    await MopupService.Instance.PushAsync(new PaymentOptionsPageView(true, false, true, BModel.OpenliMsg));
                }
                var total = "";
                if (MultiplePayableBills != null && MultiplePayableBills.Count > 0)
                {
                    total = MultiplePayableBills.Sum(x => double.Parse(x.TestDueAmount)).ToString();
                }
                else
                {
                    total = BModel.amount;
                }
                selectedFbNum = BModel.formBundleNumber;
                selectedSadadNo = BModel.sadadBillNumber;
                selectedAmount = total;
                selectedTaxablePeriod = BModel.periodDescription;
                isPayNowTapped = false;
            }
        }

        public async Task SadadPaymentSelected()
        {
            await _navigationService.NavigateTo(App.MyBillsSadadDetailsPageView, 0);
        }

        public async Task ApplePaySelected()
        {

            await DoValidatePayment(fbNum: selectedFbNum, selectedSadadNo, "A");
        }
        public async Task MadaPaymentSelected()
        {
            await DoValidatePayment(selectedFbNum, selectedSadadNo, "Mada Payment");

        }

        public async Task DoValidatePayment(string fbNum, string sdadNo, string paymentType)
        {
            try
            {
                try
                {

                    IsLoading = true;

                    var platform = "";

                    if (DeviceInfo.Platform == DevicePlatform.iOS)
                    {
                        platform = "C4";
                    }
                    else if (DeviceInfo.Platform == DevicePlatform.Android)
                    {
                        platform = "C3";
                    }
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
                            await MopupService.Instance.PushAsync(new PaymentExceptionPageView());
                            return;
                        }

                        if (PaymentData.d.Guid != null)
                        {

                            App.PaymentGuid = PaymentData.d.Guid;

                        }

                        if (paymentType == "Mada Payment")
                        {

                            IsLoading = true;
                            //CR7420
                            CreateMadaResponseRoot respose = await GetWebviewContent(PaymentData.d.Srcid);
                            IsLoading = false;
                            if (!string.IsNullOrEmpty(respose?.result?.securityAuthorizationKey))
                            {
                                App.securityAuthorizationKey = respose.result.securityAuthorizationKey;
                                await _navigationService.NavigateTo(App.PaymentProcessWebview, 2);
                            }
                        }

                    }

                    IsLoading = false;

                }
                catch (GAZTValidatePaymentInProcessException ex)
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                }

                catch (InternetException)
                {
                    IsLoading = false;
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                    _navigationService.GoBack();
                }
                catch (GAZTNetworkConnectivityIssueException)
                {
                    IsLoading = false;
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                }
            }
            catch (InternetException)
            {
                IsLoading = false;
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                _navigationService.GoBack();
            }
        }



        public async Task<CreateMadaResponseRoot> GetWebviewContent(string srcid)
        {
            try
            {
                var paymentPayload = new CreateMadaPaymentPayload
                {
                    GUID = App.PaymentGuid,
                    sourceId = srcid
                };

                CreateMadaResponseRoot respose = await WebServiceManager.GAZTCreateMadaPayment(paymentPayload);
                return respose;
            }
            catch (GAZTValidateMadaPaymentException ex)
            {
                IsLoading = false;
                var message = ex.Message.Substring(0, 1).ToUpper() + ex.Message.Substring(1).ToLower();
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(message));
                return null;
            }
            catch (Exception)
            {
                return null;
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

                    if (DeviceInfo.Platform == DevicePlatform.iOS)
                    {
                        platform = "C4";
                    }
                    else if (DeviceInfo.Platform == DevicePlatform.Android)
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

                            await _navigationService.NavigateTo(App.MyBillsSuccessPageView, paymentInfo);
                        }
                        else
                        {
                            await MopupService.Instance.PushAsync(new PaymentExceptionPageView());
                        }

                    }
                    IsLoading = false;

                }
                catch (GAZTValidatePaymentInProcessException ex)
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                }
                catch (InternetException)
                {
                    IsLoading = false;
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                    _navigationService.GoBack();
                }
            }
            catch (InternetException)
            {
                IsLoading = false;
                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                _navigationService.GoBack();
            }
        }

        public async Task LoadDashboardData()
        {
            try
            {
                IsLoading = true;
                string UserId = App.LoginDataRetrieved.TIN;
                TaxPayerProfile TPProfile = await WebServiceManager.GetTPProfileAndUpdatePasswordAPICall(UserId);
                if (TPProfile != null)
                {
                    if (App.TP != null)
                    {
                        App.TP = TPProfile;
                        if (App.TP.typeCheck == "X")
                        {
                            TaxpayerName = App.TP.TpTitle + " " + App.TP.firstName + " " + App.TP.lastName;
                        }
                        else
                        {
                            TaxpayerName = App.TP.organizationName;
                        }
                    }
                }
                DashboardData = await WebServiceManager.GAZTGetDashboardData(UtilityManager.GetLanguageParameter(), App.LoginDataRetrieved.TIN);

                await GetAccountStatments();
                await GetBillsAndReturns();

                if (App.isMybillsRefresh)
                {
                    PopulateBillsInformation();
                }
                else
                {
                    PopualateCommittmentsInformation();
                    if (DashboardData.data[0] != null && DashboardData.data[0].instructionAction != null)
                    {
                        if (DashboardData.data[0].instructionAction == "X")
                        {
                            IsInstalmentPlanVisible = true;
                            await getDashboardInstalmentPlan();
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
                    }

                }
            }
            catch (GAZTSessionExpiredException)
            {
                await _dialogService.ShowMessage(AppResources.ZYourSessionhasexpiredPleaseLoginagain, AppResources.Information);
                PopToRootPage();
            }
            catch (Exception)
            {
                IsLoading = false;
            }

            IsLoading = false;
            SelectedCommitmentFilterLabelValue = AppResources.ZZOverdueCommitments;
        }

        public async Task GetAccountStatments()
        {
            IsAccountsStatementLoading = true;



            string lang = UtilityManager.GetLanguageParameter();

            ObservableCollection<MyBills> ACBills = new ObservableCollection<MyBills>();

            ObservableCollection<MyBills> TempBills = await WebServiceManager.GAZTGetMyBills(App.LoginDataRetrieved.TIN, lang, "Bills");

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
                List<OverduePaymentAndUnSubmittedReturn> TempBills = await WebServiceManager.GAZTGetPaymentOverdueSetForDashboardData(UtilityManager.GetLanguageParameter(), App.LoginDataRetrieved.TIN);
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

                               
                                pendingBills.Add(singleItem);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < newItems.Count; i++)
                            {
                                var singleItem = newItems[i];

                                pendingBills.Add(singleItem);
                            }
                        }
                        foreach (OverduePaymentAndUnSubmittedReturn ee in newItems)
                        {
                            temp1.Add(ee);
                            if (ee.amount != null)
                            {
                                MyObligationAmount += Double.Parse(ee.amount);
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
                List<OverduePaymentAndUnSubmittedReturn> TempReturns = await WebServiceManager.GAZTGetUnSubmittedReturnSetForDashboardData(UtilityManager.GetLanguageParameter(), App.LoginDataRetrieved.TIN);
                foreach (OverduePaymentAndUnSubmittedReturn ee in TempReturns)
                {
                    temp2.Add(ee);
                }
                MainThread.BeginInvokeOnMainThread(() => Returns = temp2);
            }
            catch (GAZTErrorException ex)
            {



            }

        }
        private async Task getDashboardInstalmentPlan()
        {

            InstalmentResponse = await WebServiceManager.GAZTGetDashboardInstalmentPlanData(App.IsArabic ? "AR" : "EN", App.LoginDataRetrieved.TIN);

            var items = new ObservableCollection<InstalmentPlanResult>();

            ObservableCollection<Brush> CustomBrushes = new ObservableCollection<Brush>()
            {
                new SolidColorBrush(Color.FromRgba("#042e66")), //Primary - InstallmentTotalAmount
                new SolidColorBrush(Color.FromRgba("#61b34f")), //SuccessColor - NextInstallmentAmount
                new SolidColorBrush(Color.FromRgba("#999999")) //NeutralGreay
            };


            foreach (InstalmentPlanResult singleItem in InstalmentResponse.installmentPlans)
            {
                double totalPaidBills = 0;
                double nextBill = 0;
                double unPaidBills = 0;
                var Data = new ObservableCollection<Model>();
                totalPaidBills = String.IsNullOrEmpty(singleItem.TotalInstallmentsPaid) ? 0 : int.Parse(singleItem.TotalInstallmentsPaid);
                nextBill = String.IsNullOrEmpty(singleItem.NextInstallmentAmount) ? 0 : 1;
                unPaidBills = String.IsNullOrEmpty(singleItem.TotalInstallmentsPaid) ? 0 : int.Parse(singleItem.TotalInstallmentsPaid);
                if (unPaidBills > 0) { unPaidBills = unPaidBills--; }

                Data.Add(new Model("Paid", totalPaidBills));
                Data.Add(new Model("nextPayment", nextBill));
                Data.Add(new Model("Remaining", unPaidBills));
                singleItem.Series = Data;
                singleItem.ChartColors = CustomBrushes;
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
                        Bill.ColorCode = (Color)Application.Current.Resources["ErrorColor"];

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
                    var BillsAndReturnsCommitmentsOverdurItems = new List<OverduePaymentAndUnSubmittedReturn>();

                    var tempList = new List<OverduePaymentAndUnSubmittedReturn>();

                    try
                    {
                        if (SelectedCommitmentFilterValue != null && SelectedCommitmentFilterValue.Equals(AppResources.ZZOverdueCommitments))
                        {
                            BillsAndReturnsCommitmentsOverdurItems = BillsAndReturnsCommitmentsTemp.Where(a =>
                            (a.dueDate != null && DateTime.Compare(a.DueDateDateTime, Today) <= 0)
                            || (a.dueDate != null && DateTime.Compare(a.DueDateDateTime, Today) <= 0)).ToList();

                            foreach (var item in BillsAndReturnsCommitmentsOverdurItems)
                            {
                                var date = new DateTime();

                                date = item.DueDateDateTime;

                                if (App.IsArabic)
                                {
                                    if (item.calendarType?.Equals("H") == true || item.inboundCorrespondenceType.StartsWith("H"))
                                    {
                                        item.Day = UtilityManager.GetMonthNameHijri(date.Month.ToString("MMMM", new CultureInfo("en-US")));
                                        if (!item.IsPaymentOverdue)
                                        {
                                            if (item.calendarType?.Equals("H") == true || item.inboundCorrespondenceType.StartsWith("H"))
                                            {
                                                item.Day = UtilityManager.GetMonthNameHijri(date.Month.ToString("MMMM", new CultureInfo("en-US")));
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
                                                item.Day = UtilityManager.GetMonthName(date.Month.ToString("MMMM", new CultureInfo("en-US")));
                                                item.Month = date.Year.ToString();
                                            }
                                        }
                                        else
                                        {
                                            if (item.calendarType?.Equals("H") == true || item.inboundCorrespondenceType.StartsWith("H"))
                                            {
                                                item.Day = UtilityManager.GetMonthNameHijri(date.Month.ToString("MMMM", new CultureInfo("en-US")));
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
                                                item.Day = date.Month.ToString("MMM", new CultureInfo("en-US"));
                                                item.Month = date.Year.ToString();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        item.Day = UtilityManager.GetMonthName(date.Month.ToString("MMMM", new CultureInfo("en-US")));
                                        item.Month = date.Year.ToString();
                                    }
                                }
                                else
                                {
                                    if (item.calendarType?.Equals("H") == true || item.inboundCorrespondenceType.StartsWith("H"))
                                    {
                                        item.Day = UtilityManager.GetMonthNameHijri(date.Month.ToString("MMMM", new CultureInfo("en-US")));
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
                                        item.Day = date.Month.ToString("MMM", new CultureInfo("en-US"));
                                        item.Month = date.Year.ToString();
                                    }
                                }


                            }
                        }
                        else if (SelectedCommitmentFilterValue != null &&  SelectedCommitmentFilterValue.Equals(AppResources.ZZUpcomingCommitments))
                        {
                            BillsAndReturnsCommitmentsOverdurItems = BillsAndReturnsCommitmentsTemp.Where(a =>
                            (a.dueDate != null && DateTime.Compare(a.DueDateDateTime, Today) > 0)
                            || (a.dueDate != null && DateTime.Compare(a.DueDateDateTime, Today) > 0)).ToList();
                            foreach (var item in BillsAndReturnsCommitmentsOverdurItems)
                            {
                                var date = new DateTime();

                                date = item.DueDateDateTime;
                                if (App.IsArabic)
                                {
                                    if (item.calendarType?.Equals("H") == true || item.inboundCorrespondenceType.StartsWith("H"))
                                    {
                                        item.Day = UtilityManager.GetMonthNameHijri(date.Month.ToString("MMMM", new CultureInfo("en-US")));
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
                                        item.Day = UtilityManager.GetMonthName(date.Month.ToString("MMMM", new CultureInfo("en-US")));
                                        item.Month = date.Year.ToString();
                                    }
                                }
                                else
                                {
                                    if (item.calendarType?.Equals("H") == true || item.inboundCorrespondenceType.StartsWith("H"))
                                    {
                                        item.Day = UtilityManager.GetMonthNameHijri(date.Month.ToString("MMMM", new CultureInfo("en-US")));
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
                                        item.Day = date.Month.ToString("MMM", new CultureInfo("en-US"));
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
                    var BillsAndReturnsCommitmentsLocalDueDTC = BillsAndReturnsCommitmentsLocal.Where(x => x.dueDate != null).ToList();
                    var BillsAndReturnsCommitmentsLocalDueDT = BillsAndReturnsCommitmentsLocal.Where(x => x.dueDate != null).ToList();
                    //BillsAndReturnsCommitmentsLocalDueDT = BillsAndReturnsCommitmentsLocalDueDT.Select(x =>
                    //{ x.dueDate = Convert.ToDateTime(x.dueDate);return x; }
                    //    ).ToList() ;
                    BillsAndReturnsCommitmentsLocal = BillsAndReturnsCommitmentsLocalDueDTC.Concat(BillsAndReturnsCommitmentsLocalDueDT).ToList();

                    BillsAndReturnsCommitmentsLocal = BillsAndReturnsCommitmentsLocal.OrderByDescending(i => (i.dueDate)).ToList();
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
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                        });
                    }
                    // Rethrow any other exception.

                }
            }
            catch (GAZTSessionExpiredException)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZYourSessionhasexpiredPleaseLoginagain, AppResources.Information);
                    PopToRootPage();
                });
            }
            catch (Exception ex)
            {
                IsLoading = false;
            }
        }
        public class BillTypeCorrepsondingCountAndAmount
        {
            public BillType Status;
            public string BillTypeName;
            public int BillCount;
            public string BillAmount;
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
                if (DashboardData.data != null && DashboardData.data.Count > 0)
                {
                    //Partially Paid Bills
                    var MyBillsChartModelsTemp = new List<MyBillsChartModel>();

                    if (DashboardData.data[0] != null && DashboardData.data[0].partialReturnTotalNumber != null)
                    {
                        BillTypeCorrepsondingCountAndAmount PartiallyPaidBillCountAndAmount = new BillTypeCorrepsondingCountAndAmount();

                        PartiallyPaidBillCountAndAmount.Status = BillType.PrbillsTot;
                        String PartialPaidBillsstr = DashboardData.data[0].partialReturnTotalNumber.TrimStart(new Char[] { '0' });
                        String PartialPaidBillsAmountstr = DashboardData.data[0].partialReturnTotalNumber.TrimStart(new Char[] { '0' });
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
                        ColorsChild.Add(Color.FromRgb(227, 152, 0));

                        iBillsCount += Convert.ToInt32(BillCount);
                        PartiallyPaidBillCount = BillCount;
                    }

                    //Unpaid Bills
                    if (DashboardData.data[0] != null && DashboardData.data[0].unpaidBillsTotalNumber != null)
                    {
                        BillTypeCorrepsondingCountAndAmount UnPaidBillCountAndAmount = new BillTypeCorrepsondingCountAndAmount();

                        UnPaidBillCountAndAmount.Status = BillType.UpbillsTot;
                        String UnpaidBillsstr = DashboardData.data[0].unpaidBillsTotalNumber.TrimStart(new Char[] { '0' });
                        String UnpaidBillsAmountstr = DashboardData.data[0].unpaidBillsTotalNumber.TrimStart(new Char[] { '0' });
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
                        ColorsChild.Add(Color.FromRgb(236, 0, 0));

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
            catch (Exception)
            {
            }
        }
        public class ReturnTypeAndCorrepsondingCount
        {
            public string ReturnTypeName { get; set; }
            public string ReturnCount { get; set; }
            public ReturnType ReturnTypeProperty { get; set; }
        }
        public void PopulateReturnsInformation()
        {
            List<ReturnTypeAndCorrepsondingCount> SegregatedReturnTypeAndCorrepsondingCount = new List<ReturnTypeAndCorrepsondingCount>();
            try
            {
                if (DashboardData != null)
                {
                    if (DashboardData.data != null && DashboardData.data.Count > 0)
                    {
                        //Submited
                        if (DashboardData.data[0] != null && DashboardData.data[0].returnTotalNumber != null)
                        {
                            ReturnTypeAndCorrepsondingCount SubmittedReturnTypeAndCorrepsondingCount = new ReturnTypeAndCorrepsondingCount();

                            SubmittedReturnTypeAndCorrepsondingCount.ReturnTypeProperty = ReturnType.RtnTot;
                            String RtnTotstr = DashboardData.data[0].returnTotalNumber.TrimStart(new Char[] { '0' });
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
                        if (DashboardData.data[0] != null && DashboardData.data[0].icrTotal != null)
                        {
                            ReturnTypeAndCorrepsondingCount OverdueReturnTypeAndCorrepsondingCount = new ReturnTypeAndCorrepsondingCount();
                            OverdueReturnTypeAndCorrepsondingCount.ReturnTypeProperty = ReturnType.DueIcr;
                            String DueIcrstr = DashboardData.data[0].icrTotal.TrimStart(new Char[] { '0' });
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
                        if (DashboardData.data[0] != null && DashboardData.data[0].nonSubmittedReturnTotalNumber != null)
                        {
                            ReturnTypeAndCorrepsondingCount UnSubmittedReturnTypeAndCorrepsondingCount = new ReturnTypeAndCorrepsondingCount();

                            UnSubmittedReturnTypeAndCorrepsondingCount.ReturnTypeProperty = ReturnType.NrtnTot;
                            string NrtnTotstr = DashboardData.data[0].nonSubmittedReturnTotalNumber.TrimStart(new Char[] { '0' });
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
            }
        }
        public void PopulateeServicesApplicableToTheTaxPayer()
        {
            //Call the API to get the eSevrices applicable to the TP
            var eServicesAvailableToTheTPTemp = new List<eServiceInfo>();

            if (eServicesAvailableToTheTP != null)
                eServicesAvailableToTheTP.Clear();
            if (DashboardData.data[0].taxpayerType != null && DashboardData.data[0].taxpayerType != "")
            {
                UtilityManager.TPTaxAvalable = DashboardData.data[0].taxpayerType;
                UtilityManager.IsZakatAvailable = DashboardData.data[0].estimateZakat;
                string[] TpTypes = DashboardData.data[0].taxpayerType.Split(',');
                foreach (string ItemType in TpTypes)
                {
                    if (ItemType == "05" && DashboardData.data[0].estimateZakat == "X")
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

        public async Task PopulateStatements(string taxType, string statementFilter, string year)
        {
            try
            {
                HeaderSet = await WebServiceManager.GAZTGetAccountStatementHeaderSet(statementFilter, year, taxType, true);

                double tempEndProgressBar = (Convert.ToDouble(HeaderSet.d.DebitAmount));
                double startCreditProgressBar = (Convert.ToDouble(HeaderSet.d.Credit.Replace("-", string.Empty)));
                double totalBalance = tempEndProgressBar + startCreditProgressBar;

                AccStmtnCreditAmount = HeaderSet.d.CreditAmount.Replace("-", string.Empty);

                DebitAmountEndProgressBar = tempEndProgressBar / totalBalance * 100;
                CreditAmountStartProgressBar = startCreditProgressBar / totalBalance * 100;
                MessagingCenter.Send<object>(this, "UpdateProgressBar");

                IsLoading = false;
            }
            catch (Exception)
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
            catch (Exception)
            {
            }
            return amountWithComma;
        }

        public async Task LogOut()
        {
            try
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

                IsLoading = true;
                await WebServiceManager.GAZTLogOff();
                IsLoading = false;

                App.IsLogOut = true;
                App.IsLoginCalled = false;
                App.IsSamlApiCalledAndroid = false;

                App.LoginDataRetrieved = null;
                App.httpClientHandler = new HttpClientHandler();
                App.httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                App.httpClientHandler.CookieContainer = new System.Net.CookieContainer();
                await _navigationService.NavigateTo($"/{App.SFLoginPageView}", App.GAZTNewDesignDashBoardPageView);

            }
            catch (Exception)
            {
                IsLoading = false;

            }



        }

        #endregion

        #region Survey
        bool iSEndSurvey;
        public bool ISEndSurvey { get { return iSEndSurvey; } set { iSEndSurvey = value; OnPropertyChanged(); } }

        public ICommand SurveyNextCommand
        {
            get
            {
                return new Command<string>(async (currentStep) =>
                {
                    SurveyCurrentStep = int.Parse(currentStep);

                    if (SurveyCurrentStep == 4)
                    {
                        await MopupService.Instance.PopAsync(true);
                        SurveyCurrentStep = 0;
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
                    FQanswer = int.Parse(selectedAnswer.ID);
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
                    if (selected.ID == "64087eadfe688b43c294529a" || selected.ID == "64087eadfe688b43c2945299")
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
                return new Command<string>(async (action) =>
                {

                    if (action == "0")
                    {
                        IsShowMsgView = false;
                        IsLoading = true;
                        await AddSurveyForToday(true);
                        IsLoading = false;
                    }
                    else
                    {
                        IsSurveyVisible = true;
                        SurveyCurrentStep = 1;
                    }
                });
            }
        }
        public int SchedukeID { get; set; }

        public ICommand CheckSurveyCommand
        {
            get
            {
                return new Command(async () =>
                {
                    CultureInfo enCul = new CultureInfo("en-US");
                    var Defdate = DateTime.Now.Date.ToString("MM-dd-yyyy", enCul);
                    var date = Preferences.Get("DateOfSurvey", Defdate);
                    var isSurveyDone = Preferences.Get("IsSurveyTaken", false);
                    var UsedTin = Preferences.Get("TIN", "");
                    if (date == Defdate && isSurveyDone && UsedTin == App.TP.Tin)
                    {
                        return;
                    }
                    if (!IsShowMsgView)
                    {

                        IsShowMsgView = await HaveSurveyForToday();
                        if (IsShowMsgView)
                        {
                            SurveyPopUp poupWindow = new SurveyPopUp();
                            await MopupService.Instance.PushAsync(poupWindow);
                        }
                    }

                    await OnAppearing();
                });
            }
        }

        public ICommand AddSurveyCommand
        {
            get
            {
                return new Command<string>(async (isdismiss) =>
                {
                    SurveyCurrentStep = 3;
                    IsLoading = true;
                    await AddSurveyForToday(isdismiss == "0" ? false : true);
                    IsLoading = false;
                });
            }
        }

        public ICommand VatRegistrationCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "VATRegistration_Details_Tapped", "VAT Registration Details eService");
                    await _navigationService.NavigateTo(App.VATRegistrationPageView);

                    Instrumentation.EndCall(callTracker);
                });
            }
        }

        public ICommand GeneralServicesCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "GeneralServices_Tapped", "General Services");
                    await _navigationService.NavigateTo(App.GeneralServicesListPageView);

                    Instrumentation.EndCall(callTracker);
                });
            }
        }

        public ICommand TappedOnMyBills
        {
            get
            {
                return new Command(async () =>
                {
                    var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TappedOnMyBills", AppResources.MyBills + " Page");
                    BillInfo billInfo = new BillInfo();
                    await _navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);
                    Instrumentation.EndCall(callTracker);
                });
            }
        }

        public ICommand TappedOnUnSubmitted
        {
            get
            {
                return new Command(async () =>
                {
                    var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TappedOnSingleReturns", "Unsubmitted Return from Dashboard");
                    await _navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 1);

                    Instrumentation.EndCall(callTracker);
                });
            }
        }

        public ICommand TappedOnSubmitted
        {
            get
            {
                return new Command(async () =>
                {
                    var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TappedOnSingleReturns", "Submitted Return from Dashboard");
                    await _navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 0);


                    Instrumentation.EndCall(callTracker);
                });
            }
        }

        public ICommand TappedOnOverDue
        {
            get
            {
                return new Command(async () =>
                {
                    var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TappedOnSingleReturns", "Overdue Return from Dashboard");
                    await _navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 2);

                    Instrumentation.EndCall(callTracker);
                });
            }
        }

        public ICommand LabelMyBillsCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "MyBills_Tapped", "All Bills from Dashboard");

                    BillInfo billInfo = new BillInfo();
                    billInfo.BillTypeName = AppResources.UnPaid;
                    await _navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);

                    Instrumentation.EndCall(callTracker);
                });
            }
        }

        public ICommand LabelMyRetunsCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "MyRetuns_Tapped", "Returns eService");

                    await _navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 4);

                    Instrumentation.EndCall(callTracker);
                });
            }
        }

        public ICommand LabelMyProfileCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "MyProfile_Tapped", "My Profile eService");

                    await _navigationService.NavigateTo(App.TaxpayerProfilePageView);

                    Instrumentation.EndCall(callTracker);
                });
            }
        }

        public ICommand TaxpayerCertificateCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TaxpayerCertificate_Tapped", "My Certificates eService");
                    await _navigationService.NavigateTo(App.TaxpayersCertificatesPageView);

                    Instrumentation.EndCall(callTracker);
                });
            }
        }

        public ICommand OnApplicationStatusCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "OnApplicationStatus_Tapped", "Application Status eService");
                    await _navigationService.NavigateTo(App.FormBundleStatusPageView);

                    Instrumentation.EndCall(callTracker);
                });
            }
        }

        public ICommand ToolbarMyTaxCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TappedOnMyBills", AppResources.MyBills + " Page");
                    BillInfo billInfo = new BillInfo();
                    billInfo.BillTypeName = AppResources.All;
                    await _navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);


                    Instrumentation.EndCall(callTracker);
                });
            }
        }

        public ICommand OnEduLinkCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var callTracker = Instrumentation.BeginCall("DashboardPageView", "EduLink_Tapped", "Education Link");
                    Uri uri = new Uri("https://edujourneys.zatca.gov.sa/home/tracks");
                    await OpenBrowser(uri);


                    Instrumentation.EndCall(callTracker);
                });
            }
        }

        public ICommand BillsPayNowCommand
        {
            get
            {
                return new Command<object>(async (sender) =>
                {
                    try
                    {
                        if (!isPayNowTapped)
                        {
                            isPayNowTapped = true;
                            Border payNowCard = sender as Border;
                            OverduePaymentAndUnSubmittedReturn BModel = (OverduePaymentAndUnSubmittedReturn)payNowCard.BindingContext;

                            await verifyPaymentAndShowBillsPopup(BModel);
                        }

                    }
                    catch (Exception)
                    {

                    }
                });
            }
        }

        public ICommand InstalmentPlanCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "ZakatInstalmentPlan_Tapped", "Zakat Instalment eService");
                    await _navigationService.NavigateTo(App.InstalmentPlanPageView);
                    Instrumentation.EndCall(callTracker);
                });
            }
        }

        public ICommand AccountStatements
        {
            get
            {
                return new Command(async () =>
                {
                    var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "AccountStatements_Tapped", "Account Statements eService");

                    await _navigationService.NavigateTo(App.AccountStatementBillsPageView);


                    Instrumentation.EndCall(callTracker);
                });
            }
        }

        public ICommand OnZakatNowCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "OnZakatNowTapped", "Establishment Registration eService");
                    if (App.LoginDataRetrieved.ZkReg == "U")
                    {
                        App.ZAKATType = PageExecutionType.Update;
                        await _navigationService.NavigateTo(App.EstablishmentAmendUpdatePage);
                    }
                    else
                    {
                        await _navigationService.NavigateTo(App.EstablishmentRegistrationPage);
                    }
                    Instrumentation.EndCall(callTracker);
                });
            }
        }

        public ICommand OnSupportCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "OnSupportTapped", "Support");
                    await _navigationService.NavigateTo(App.SupportPageView);


                    Instrumentation.EndCall(callTracker);
                });
            }
        }

        public ICommand InboxCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "Inbox_Tapped", "Inbox eService");

                    await _navigationService.NavigateTo(App.TaxpayerCorrespondancePageView);
                    Instrumentation.EndCall(callTracker);
                });
            }
        }
        public ICommand RefundRequestCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "VATRefundRequest_Tapped", "VAT Refund Request eService");
                    await _navigationService.NavigateTo(App.VATRefundsListPageView);
                    Instrumentation.EndCall(callTracker);
                });
            }
        }

        public ICommand LogoutCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await MopupService.Instance.PushAsync(new LogoutPageView(AppResources.LogoutConfirmationMessage));
                });
            }
        }

        public ICommand TappedOnMyReturnsCommand
        {
            get
            {
                return new Command(async () =>
                {

                    var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TappedOnMyReturns", AppResources.Returns + " Page");
                    await _navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView, 3);

                    Instrumentation.EndCall(callTracker);
                });
            }
        }

        public ICommand ProfitOnGoodsCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await _navigationService.NavigateTo(App.NewYesorNoPageView);
                });
            }
        }

        public ICommand TaxPayerSubsidyRequestCommand
        {
            get
            {
                return new Command(async () =>
                {

                    var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TaxPayerSubsidyRequestTapped", "TaxPayer Subsidy Request");
                    var a = App.LoginDataRetrieved;
                    IsLoading = true;

                    string response = await TaxpayerSubsidyWebServiceManager.TaxpayerSubsidyPostRequestAsync("MSUB");

                    if (response != null && response.Length > 0)
                    {
                        SubsidyResponseModel subsidyResponseModel = JsonConvert.DeserializeObject<SubsidyResponseModel>(response);
                        if (subsidyResponseModel != null && subsidyResponseModel.Data != null)
                        {

                            if (!string.IsNullOrEmpty(subsidyResponseModel.Data.FormBundleGUID))
                            {

                                string url = subsidyResponseModel.Data.ExternalPortal;
                                url += "?";
                                url += "culture=" + WebServiceManager.GetLangZParameterAREN();
                                url += "&tin=" + App.LoginDataRetrieved.TIN;
                                url += "&token=" + subsidyResponseModel.Data.FormBundleGUID;
                                url += "&device=MA";
                                ZATCAConstants.TaxpayerSubsidyRequest = url;

                                await _navigationService.NavigateTo(App.TaxpayerSubsidyRequest);
                                Instrumentation.EndCall(callTracker);
                            }


                        }
                    }
                    IsLoading = false;
                });
            }
        }


        public async Task<bool> AddSurveyForToday(bool isDismiss = false)
        {
            CultureInfo enCul = new CultureInfo("en-US");
            var date = DateTime.Now.Date.ToString("MM-dd-yyyy", enCul);
            var tin = App.TP.Tin;
            AddSurveyBody body = new AddSurveyBody()
            {
                dismiss = isDismiss,
                scheduleid = SchedukeID,
                tin = long.Parse(App.TP.Tin)
            };
            if (isDismiss)
            {
                var res = await _surveyServices.AddSurveyData(body);
                if (res.IsSuccessStatusCode)
                {
                    var DateOfSurvey = DateTime.Now.Date.ToString("MM-dd-yyyy", enCul);
                    Preferences.Set("DateOfSurvey", DateOfSurvey);
                    Preferences.Set("IsSurveyTaken", true);
                    Preferences.Set("TIN", App.TP.Tin);
                    SurveyCurrentStep = 0;
                    ISEndSurvey = true;
                    await MopupService.Instance.PopAsync(true);
                    return false;
                }
            }
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
                          mobile=App.TP.mobile,
                           firstName=App.TP.Name,
                           TIN=App.TP.Tin,
                           email=App.TP.email
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
                                     rowID=PageSettings.Q1AnsID,
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
                                     rowID=QNumber==2?PageSettings.Q2AnsID:PageSettings.Q3AnsID,
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
                    Preferences.Set("TIN", App.TP.Tin);
                    // SurveyCurrentStep = 0;

                }
            }
            ISEndSurvey = true;

            SQAnswer = "";
            //  IsShowMsgView = false;
            return false;
        }


        public async Task<bool> HaveSurveyForToday()
        {
            IsLoading = true;
            CultureInfo enCul = new CultureInfo("en-US");
            var date = DateTime.Now.Date.ToString("MM-dd-yyyy", enCul);
            var res = await _surveyServices.GetSurveyByDate(App.TP.Tin, date);
            if (res.Item2)
            {
                if (res.Item1.data != null && res.Item1.data.isuservotedbefore == false && res.Item1.data.dismiss == false)
                {

                    SchedukeID = res.Item1.data.id;
                    return true;
                }
            }
            IsLoading = false;
            return false;
        }

        #endregion

        public override ICommand MenuNavigationCommand
        {
            get
            {
                return new Command<MenuModel>(async (Selecteditem) =>
                {
                    if (Selecteditem.ID == "Home")
                    {

                        await _navigationService.NavigateTo($"{Selecteditem.ID}", "3");

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
                    await _navigationService.NavigateTo($"{Selecteditem.ID}");

                });
            }
        }

        public ICommand ContractReleaseCommand
        {
            get
            {
                return new Command(async () =>
                {

                    var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "ContractRelease_Tapped", "Contract Release eService");

                    await _navigationService.NavigateTo(App.ContractReleaseListPageView);


                    Instrumentation.EndCall(callTracker);
                });
            }
        }
        public ICommand ZakatInstalmentPlanCommand
        {
            get
            {
                return new Command(async () =>
                {

                    var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "ZakatInstalmentPlan_Tapped", "Zakat Instalment eService");

                    await _navigationService.NavigateTo(App.InstalmentPlanPageView);



                    Instrumentation.EndCall(callTracker);
                });
            }
        }
        public ICommand ChnageFillingPeriodCommand
        {
            get
            {
                return new Command(async () =>
                {

                    var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "ChangeFillingPeriod_Tapped", "Change Filing Period eService");

                    await _navigationService.NavigateTo(App.ChangeFillingPeriodListPageView);


                    Instrumentation.EndCall(callTracker);
                });
            }
        }

        public ICommand VatReviewCommand
        {
            get
            {
                return new Command(async () =>
                {

                    var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "Vat_Review_Tapped", "Objections eService");

                    await _navigationService.NavigateTo(App.ObjectionsSelectionPageView);

                    Instrumentation.EndCall(callTracker);
                });
            }
        }

        public ICommand ChangeLanguageCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var navigation = Application.Current.MainPage.Navigation;
                    var currentPage = navigation.NavigationStack.LastOrDefault();
                    if (App.IsArabic)
                    {

                        var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "ChangeLanguage_Tapped", "Language Changed to English");

                        App.IsArabic = false;
                        App.changeFontFamily(App.appObj);

                        var vUpdatedPage = new GAZTNewDesignDashBoardPageView();

                        SelectedCommitmentFilterValue = null;
                        navigation.InsertPageBefore(vUpdatedPage, currentPage);
                        await navigation.PopAsync();
                        NDCommitments = AppResources.NDCommitments;
                        ZBills = AppResources.Bills;
                        Return = AppResources.Returns;

                        AboutUs = AppResources.ZZZAboutUs;
                        Contactus = AppResources.ZZZContactus;
                        PrivacyandPolicy = AppResources.ZZZPrivacyandPolicy;
                        Logout = AppResources.ZLogout;
                        App.HasToRefreshLoaderOnDashboard = true;
                        SetLTRDirection();
                        GetHomeMenuLst();
                        Instrumentation.EndCall(callTracker);
                    }
                    else
                    {

                        var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "ChangeLanguage_Tapped", "Language Changed to Arabic");

                        App.IsArabic = true;
                        App.changeFontFamily(App.appObj);
                        var vUpdatedPage = new GAZTNewDesignDashBoardPageView();
                        SelectedCommitmentFilterValue = null;

                        navigation.InsertPageBefore(vUpdatedPage, currentPage);
                        await navigation.PopAsync();
                        NDCommitments = AppResources.NDCommitments;
                        ZBills = AppResources.Bills;
                        Return = AppResources.Returns;

                        AboutUs = AppResources.ZZZAboutUs;
                        Contactus = AppResources.ZZZContactus;
                        PrivacyandPolicy = AppResources.ZZZPrivacyandPolicy;
                        Logout = AppResources.ZLogout;
                        App.HasToRefreshLoaderOnDashboard = true;
                        GetHomeMenuLst();

                        SetRTLDirection();

                        Instrumentation.EndCall(callTracker);
                    }
                    await OnAppearing();
                });
            }
        }
        public ICommand TinRegistrationDetailsCommand
        {
            get
            {
                return new Command(async () =>
                {

                    var callTracker = Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "TinRegistrationDetails_Tapped", "Registration Details");

                    await _navigationService.NavigateTo(App.ZakatRegistrationDetailsListPageView);


                    Instrumentation.EndCall(callTracker);
                });
            }
        }

        public ICommand GoToChatCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await _navigationService.NavigateTo("ChatPotView");
                });
            }
        }
        
        public ICommand ContactZatcaEmpCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var callTracker = AppDynamics.Agent.Instrumentation.BeginCall("GAZTNewDesignDashBoardPageView", "ContactZatcaEmp_Tapped", "Contact ZATCA Employee");
                    var a = App.LoginDataRetrieved;
                    IsLoading = true;
                    string response = await TaxpayerSubsidyWebServiceManager.TaxpayerSubsidyPostRequestAsync("6741");

                    if (response != null && response.Length > 0)
                    {
                        SubsidyResponseModel subsidyResponseModel = JsonConvert.DeserializeObject<SubsidyResponseModel>(response);
                        if (subsidyResponseModel != null && subsidyResponseModel.Data != null)
                        {

                            if (!string.IsNullOrEmpty(subsidyResponseModel.Data.FormBundleGUID))
                            {

                                String url = subsidyResponseModel.Data.ExternalPortal;
                                url = url.Replace("TINVALUE", App.LoginDataRetrieved.TIN);
                                url = url.Replace("TOKENVALUE", subsidyResponseModel.Data.FormBundleGUID);
                                ZATCAConstants.TaxpayerSubsidyRequest = url;

                                IsLoading = false;
                                await Browser.OpenAsync(url);
                                AppDynamics.Agent.Instrumentation.EndCall(callTracker);
                            }


                        }
                    }
                    IsLoading = false;
                });
            }
        }

        public ICommand GoToRateUsCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await _navigationService.NavigateTo("RateUs");
                });
            }
        }

    }
    public class ChartColorCollection : List<Brush>
    {
    }
    public class ChartDataPoint : INotifyPropertyChanged
    {
        private IComparable xValue;

        private double yValue;

        private double size;

        private double open;

        private double high;

        private double low;

        private double close;

        private double volume;

        //
        // Summary:
        //     Gets or sets the x-axis value of the data point. The x-axis value can be used
        //     to render chart series.
        public IComparable XValue
        {
            get
            {
                return xValue;
            }
            set
            {
                xValue = value;
                OnPropertyChanged("XValue");
            }
        }

        //
        // Summary:
        //     Gets or sets the y-axis value of the data point. The y-axis value can be used
        //     to render chart series.
        public double YValue
        {
            get
            {
                return yValue;
            }
            set
            {
                yValue = value;
                OnPropertyChanged("YValue");
            }
        }

        //
        // Summary:
        //     Gets or sets the size value of the data point for bubble series. Size is used
        //     to specify the size of each bubble segment.
        public double Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
                OnPropertyChanged("Size");
            }
        }

        //
        // Summary:
        //     Gets or sets the high value of the data point for CandleSeries and HiLoOpenCloseSeries
        //     to render it.
        public double High
        {
            get
            {
                return high;
            }
            set
            {
                high = value;
                OnPropertyChanged("High");
            }
        }

        //
        // Summary:
        //     Gets or sets the low value of the data point for CandleSeries and HiLoOpenCloseSeries
        //     to render it.
        public double Low
        {
            get
            {
                return low;
            }
            set
            {
                low = value;
                OnPropertyChanged("Low");
            }
        }

        //
        // Summary:
        //     Gets or sets the open value of the data point for CandleSeries and HiLoOpenCloseSeries
        //     to render it.
        public double Open
        {
            get
            {
                return open;
            }
            set
            {
                open = value;
                OnPropertyChanged("Open");
            }
        }

        //
        // Summary:
        //     Gets or sets the close value of the data point for CandleSeries and HiLoOpenCloseSeries
        //     to render it.
        public double Close
        {
            get
            {
                return close;
            }
            set
            {
                close = value;
                OnPropertyChanged("Close");
            }
        }

        //
        // Summary:
        //     Gets or sets the volume value of financial data that can be used to render technical
        //     indicators along with financial series.
        public double Volume
        {
            get
            {
                return volume;
            }
            set
            {
                volume = value;
                OnPropertyChanged("Volume");
            }
        }

        //
        // Summary:
        //     Called when any property changed in ChartDataPoint class.
        public event PropertyChangedEventHandler PropertyChanged;

        //
        // Summary:
        //     Sets the individual dataPoint for the chart items source, which can be used to
        //     render the chart series.
        //
        // Parameters:
        //   xValue:
        //     Any System.IComparable object.
        //
        //   yValue:
        //     The double value.
        public ChartDataPoint(IComparable xValue, double yValue)
        {
            XValue = xValue;
            YValue = yValue;
        }

        //
        // Summary:
        //     Sets the individual dataPoint for the items source of the Bubble chart and RangeColumn
        //     chart.
        //
        // Parameters:
        //   xValue:
        //     Any System.IComparable object.
        //
        //   value1:
        //     The double value which is assigned as High for Syncfusion.SfChart.XForms.RangeColumnSeries
        //     and as YValue for Syncfusion.SfChart.XForms.BubbleSeries
        //
        //   value2:
        //     The double value which is assigned as Low for Syncfusion.SfChart.XForms.RangeColumnSeries
        //     and as Size for Syncfusion.SfChart.XForms.BubbleSeries
        public ChartDataPoint(IComparable xValue, double value1, double value2)
        {
            XValue = xValue;
            High = value1;
            YValue = value1;
            Low = value2;
            Size = value2;
        }

        //
        // Summary:
        //     Sets the individual dataPoint for the items source of the financial chart.
        //
        // Parameters:
        //   xValue:
        //     Any System.IComparable object.
        //
        //   open:
        //     The double value - Open.
        //
        //   high:
        //     The double value - High.
        //
        //   low:
        //     The double value - Low.
        //
        //   close:
        //     The double value - Close.
        public ChartDataPoint(IComparable xValue, double open, double high, double low, double close)
        {
            XValue = xValue;
            High = high;
            Low = low;
            Open = open;
            Close = close;
        }

        //
        // Summary:
        //     Instantiates a new Chart data point with volume for technical indicators.
        //
        // Parameters:
        //   xValue:
        //     Any System.IComparable object.
        //
        //   open:
        //     The double value - Open.
        //
        //   high:
        //     The double value - High.
        //
        //   low:
        //     The double value - Low.
        //
        //   close:
        //     The double value - Close.
        //
        //   volume:
        //     The double value - Volume.
        public ChartDataPoint(IComparable xValue, double open, double high, double low, double close, double volume)
        {
            XValue = xValue;
            High = high;
            Low = low;
            Open = open;
            Close = close;
            Volume = volume;
        }

        private void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
