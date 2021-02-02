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
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    [Preserve(AllMembers = true)]
    public class GAZTNewDesignDashBoardPageViewModel : BaseViewModel
    {
        #region Variable

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

        private bool _ifnotRegInVATAndZakat;
        public bool IfnotRegInVATAndZakat
        {
            get
            {
                return _ifnotRegInVATAndZakat;
            }
            set
            {
                if (_ifnotRegInVATAndZakat == value) return;

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
                if (_accountStatementVisible == value) return;

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
                if (_liveChatVisible == value) return;
                _liveChatVisible = value;
                if (_liveChatVisible)
                {
                    _homeIndicatorColor = Color.FromHex("#005e4b");
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
                    RaisePropertyChanged("OverDue");

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
            IsToolbarTaxVisible = true;

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
                DashboardData = WebServiceManager.GAZTGetDashboardData(UtilityManager.GetLanguageParameter(), App.TP.Userid);
                _ = Task.Run(GetAccountStatments);
                //_ = Task.Run(GetBillsAndReturns);
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
            }

            IsLoading = false;
            SelectedCommitmentFilterLabelValue = AppResources.ZZOverdueCommitments;
        }

        private async Task GetAccountStatments()
        {
            TabIdentification = await WebServiceManager.GAZTGetAccountStatementsTabIdentification();
            HeaderSet = await WebServiceManager.GAZTGetAccountStatementHeaderSet("10", string.Empty, "A");

            var items = new ObservableCollection<ASResult>();
            foreach (ASResult singleItem in HeaderSet.D.StatmenetLineItemsSet.Results)
            {
                items.Add(singleItem);
            }
            AccountStatementsList = items;
            if (AccountStatementsList.Count > 2)
            {
                LastTransactionsListHeight = 220;
            }else if(AccountStatementsList.Count>1)
            {
                LastTransactionsListHeight = 150;
            }
            else
            {
                LastTransactionsListHeight = 75;
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

        private async Task GetBillsAndReturns()
        {
            var temp1 = new List<OverduePaymentAndUnSubmittedReturn>();
            List<OverduePaymentAndUnSubmittedReturn> TempBills = await WebServiceManager.GAZTGetPaymentOverdueSetForDashboardData(App.IsArabic ? "A" : "E", App.TP.Userid);

            foreach (OverduePaymentAndUnSubmittedReturn ee in TempBills)
            {
                temp1.Add(ee);
            }
            Device.BeginInvokeOnMainThread(() => Bills = temp1);
            System.Diagnostics.Debug.WriteLine("Bills " + Bills.Count);

            var temp2 = new List<OverduePaymentAndUnSubmittedReturn>();
            List<OverduePaymentAndUnSubmittedReturn> TempReturns = await WebServiceManager.GAZTGetUnSubmittedReturnSetForDashboardData(App.IsArabic ? "A" : "E", App.TP.Userid);
            System.Diagnostics.Debug.WriteLine("Returns " + Returns.Count);
            foreach (OverduePaymentAndUnSubmittedReturn ee in TempReturns)
            {
                temp2.Add(ee);
            }
            Device.BeginInvokeOnMainThread(() => Returns = temp2);

        }
        private async Task getDashboardInstalmentPlan()
        {

            InstalmentResponse = WebServiceManager.GAZTGetDashboardInstalmentPlanData(App.IsArabic ? "AR" : "EN", App.TP.Userid);



        }


        public void PopualateCommittmentsInformation()
        {
            try
            {

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
                    BillsAndReturnsCommitmentsOverdurItems = BillsAndReturnsCommitmentsOverdurItems.OrderByDescending(i => DateTime.Parse(i.DueDate)).ToList();

                    var tempList = new List<OverduePaymentAndUnSubmittedReturn>();

                    try
                    {
                        if (SelectedCommitmentFilterValue.Equals(AppResources.ZZOverdueCommitments))
                        {
                            BillsAndReturnsCommitmentsOverdurItems = BillsAndReturnsCommitmentsTemp.Where(a => DateTime.Compare(a.DueDateDateTime, Today) <= 0).ToList();

                            foreach (var item in BillsAndReturnsCommitmentsOverdurItems)
                            {
                                var date = Convert.ToDateTime(item.DueDate);
                                if (date.Year != Today.Year)
                                {
                                    if (App.IsArabic)
                                    {
                                        item.Day = UtilityManager.GetMonthName(Convert.ToDateTime(date).ToString("MMMM", new CultureInfo("en-US")));
                                    }
                                    else
                                    {
                                        item.Day = Convert.ToDateTime(date).ToString("MMM", new CultureInfo("en-US"));
                                    }
                                    item.Month = date.Year.ToString();
                                }
                            }
                        }
                        else if (SelectedCommitmentFilterValue.Equals(AppResources.ZZUpcomingCommitments))
                        {
                            BillsAndReturnsCommitmentsOverdurItems = BillsAndReturnsCommitmentsTemp.Where(a => DateTime.Compare(a.DueDateDateTime, Today) > 0).ToList();
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

                    BillsAndReturnsCommitmentsLocal = BillsAndReturnsCommitmentsLocal.OrderByDescending(i => DateTime.Parse(i.DueDate)).ToList();
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
            catch
            {
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

                        MyBillsChartModelsTemp.Add(new MyBillsChartModel { BillCount = PartiallyPaidBillCountAndAmount.BillCount, BillType = PartiallyPaidBillCountAndAmount.BillTypeName, BillColor = Xamarin.Forms.Color.FromHex("#E39800") });
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

                        MyBillsChartModelsTemp.Add(new MyBillsChartModel { BillCount = UnPaidBillCountAndAmount.BillCount, BillType = UnPaidBillCountAndAmount.BillTypeName, BillColor = Xamarin.Forms.Color.FromHex("#EC0000") });
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
            _navigationService.GoBack();
        }

        #endregion
    }
}
