using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models;
using EGAZT.Models.AccountStatements;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using EGAZT.Views.NewDesign.GenericPickers;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel.AccountStatements
{
    [Preserve(AllMembers = true)]
    public class AccountStatementsPageViewModel : BaseViewModel
    {
        public ICommand GoBackBtnTapped { get; set; }
        public ICommand FilterCloseClick { get; set; }
        public ICommand FilterBtnCommand { get; set; }
        public ICommand FiltersTapped { get; set; }
        public ICommand DownloadBtnTapped { get; set; }
        public ICommand DownloadBtnTappedDownloadPage { get; set; }
        private string _fromDate = AppResources.ASAccountStatementFrom;
        public string FromDate
        {
            get
            {
                return _fromDate;
            }
            set
            {
                if (_fromDate == value) return;
                _fromDate = value;
                RaisePropertyChanged("FromDate");
            }
        }

        private DateTime _FromDateDownloadPage;
        public DateTime FromDateDownloadPage
        {
            get
            {
                return _FromDateDownloadPage;
            }
            set
            {
                if (_FromDateDownloadPage == value) return;

                _FromDateDownloadPage = value;
                RaisePropertyChanged("FromDateDownloadPage");
            }
        }
        private DateTime _ToDateDownloadPage;
        public DateTime ToDateDownloadPage
        {
            get
            {
                return _ToDateDownloadPage;
            }
            set
            {
                if (_ToDateDownloadPage == value) return;

                _ToDateDownloadPage = value;
                RaisePropertyChanged("ToDateDownloadPage");
            }
        }


        private ASTaxpayerSelectedValues _asTaxpayerSelectedValues;
        public ASTaxpayerSelectedValues ASTaxpayerSelectedValues
        {
            get
            {
                return _asTaxpayerSelectedValues;
            }
            set
            {
                if (_asTaxpayerSelectedValues == value) return;

                _asTaxpayerSelectedValues = value;
                RaisePropertyChanged("ASTaxpayerSelectedValues");
            }
        }
        private string _toDate = AppResources.ASAccountStatementTo;
        public string ToDate
        {
            get
            {
                return _toDate;
            }
            set
            {
                if (_toDate == value) return;

                _toDate = value;
                RaisePropertyChanged("ToDate");
            }
        }

        private List<GroupedAccountStatements> _groupedStatements;
        public List<GroupedAccountStatements> GroupedStatements
        {
            get => _groupedStatements;
            set
            {
                if (_groupedStatements == value) return;

                _groupedStatements = value;
                RaisePropertyChanged(nameof(GroupedStatements));
            }
        }

        private FlowDirection _FlowDirect = FlowDirection.RightToLeft;
        public FlowDirection FlowDirect
        {
            get
            {
                return _FlowDirect;
            }
            set
            {
                if (_FlowDirect == value) return;

                _FlowDirect = value;
                RaisePropertyChanged("FlowDirect");
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

        public ASRevenueDropDownSet _transactionTypeDropDownParent = null;
        public ASRevenueDropDownSet TransactionTypeDropDownParent
        {
            get
            {
                return _transactionTypeDropDownParent;
            }
            set
            {
                if (_transactionTypeDropDownParent == value) return;

                _transactionTypeDropDownParent = value;
                RaisePropertyChanged("TransactionTypeDropDownParent");
            }
        }

        public string _TransactionDateFilterItem = string.Empty;
        public string TransactionDateFilterItem
        {
            get
            {
                return _TransactionDateFilterItem;
            }
            set
            {
                if (_TransactionDateFilterItem == value) return;

                _TransactionDateFilterItem = value;
                RaisePropertyChanged("TransactionDateFilterItem");
            }
        }
        public string _TaxTypeFilterItem = string.Empty;
        public string TaxTypeFilterItem
        {
            get
            {
                return _TaxTypeFilterItem;
            }
            set
            {
                if (_TaxTypeFilterItem == value) return;

                _TaxTypeFilterItem = value;
                RaisePropertyChanged("TaxTypeFilterItem");
            }
        }
        public string _FBNumFilterItem = string.Empty;
        public string FBNumFilterItem
        {
            get
            {
                return _FBNumFilterItem;
            }
            set
            {
                if (_FBNumFilterItem == value) return;

                _FBNumFilterItem = value;
                RaisePropertyChanged("FBNumFilterItem");
            }
        }


        public string _SadadBillNumberFilterItem = string.Empty;
        public string SadadBillNumberFilterItem
        {
            get
            {
                return _SadadBillNumberFilterItem;
            }
            set
            {
                if (_SadadBillNumberFilterItem == value) return;

                _SadadBillNumberFilterItem = value;
                RaisePropertyChanged("SadadBillNumberFilterItem");
            }
        }

        public string _TaxperiodFilterItem = string.Empty;
        public string TaxperiodFilterItem
        {
            get
            {
                return _TaxperiodFilterItem;
            }
            set
            {
                if (_TaxperiodFilterItem == value) return;

                _TaxperiodFilterItem = value;
                RaisePropertyChanged("TaxperiodFilterItem");
            }
        }

        public ObservableCollection<ASResult> _SearchBarListItemSource = null;
        public ObservableCollection<ASResult> SearchBarListItemSource
        {
            get
            {
                return _SearchBarListItemSource;
            }
            set
            {
                if (_SearchBarListItemSource == value) return;

                _SearchBarListItemSource = value;
                RaisePropertyChanged("SearchBarListItemSource");
            }
        }

        public bool _IsVisible_SearchList = false;
        public bool IsVisible_SearchList
        {
            get
            {
                return _IsVisible_SearchList;
            }
            set
            {
                if (_IsVisible_SearchList == value) return;

                _IsVisible_SearchList = value;
                RaisePropertyChanged("IsVisible_SearchList");
            }
        }

        public string _DueDateFilterItem = string.Empty;
        public string DueDateFilterItem
        {
            get
            {
                return _DueDateFilterItem;
            }
            set
            {
                if (_DueDateFilterItem == value) return;

                _DueDateFilterItem = value;
                RaisePropertyChanged("DueDateFilterItem");
            }
        }
        public string _BillDescriptionFilterItem = string.Empty;
        public string BillDescriptionFilterItem
        {
            get
            {
                return _BillDescriptionFilterItem;
            }
            set
            {
                if (_toDate == value) return;

                _BillDescriptionFilterItem = value;
                RaisePropertyChanged("BillDescriptionFilterItem");
            }
        }

        public string _BillAmountFilterItem = string.Empty;
        public string BillAmountFilterItem
        {
            get
            {
                return _BillAmountFilterItem;
            }
            set
            {
                if (_BillAmountFilterItem == value) return;

                _BillAmountFilterItem = value;
                RaisePropertyChanged("BillAmountFilterItem");
            }
        }
        public string _StatusFilterItem = string.Empty;
        public string StatusFilterItem
        {
            get
            {
                return _StatusFilterItem;
            }
            set
            {
                if (_StatusFilterItem == value) return;

                _StatusFilterItem = value;
                RaisePropertyChanged("StatusFilterItem");
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

        private Color _TotalBalanceBackground = Color.FromHex("#006450");
        public Color TotalBalanceBackground
        {
            get
            {
                return _TotalBalanceBackground;
            }
            set
            {
                if (_TotalBalanceBackground == value) return;

                _TotalBalanceBackground = value;
                RaisePropertyChanged("TotalBalanceBackground");
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
                if (value != null)
                {
                    _headerSet = value;
                }

                if (_headerSet != null && _headerSet.D != null && !String.IsNullOrEmpty(_headerSet.D.Close) && Double.Parse(_headerSet.D.Close) < 0)
                {
                    TotalBalanceBackground = Color.FromHex("#AA0C19");
                }
                else
                {
                    TotalBalanceBackground = Color.FromHex("#006450");
                }

                RaisePropertyChanged("HeaderSet");
            }
        }
        public string _TotalDebit = string.Empty;
        public string TotalDebit
        {
            get
            {
                return _TotalDebit;
            }
            set
            {
                if (_TotalDebit == value) return;

                _TotalDebit = value;
                RaisePropertyChanged("TotalDebit");
            }
        }
        public string _TotalCredit = string.Empty;
        public string TotalCredit
        {
            get
            {
                return _TotalCredit;
            }
            set
            {
                if (_TotalCredit == value) return;

                _TotalCredit = value;
                RaisePropertyChanged("TotalCredit");
            }
        }
        public string _TotalBalance = string.Empty;
        public string TotalBalance
        {
            get
            {
                return _TotalBalance;
            }
            set
            {
                if (_TotalBalance == value) return;

                _TotalBalance = value;
                RaisePropertyChanged("TotalBalance");
            }
        }

        public bool isNotHaveStatements = false;
        public bool IsNotHaveStatements
        {
            get
            {
                return isNotHaveStatements;
            }
            set
            {
                if (isNotHaveStatements == value) return;

                if (isNotHaveStatements != value)
                {
                    isNotHaveStatements = value;
                    RaisePropertyChanged("IsNotHaveStatements");
                }
            }
        }

        Dictionary<string, string> monthlyStatementsLineItemsDic = new Dictionary<string, string>()
        {

        };

        [Preserve(AllMembers = true)]
        public class ObservableGroupCollection<S, T> : ObservableCollection<T>
        {
            private readonly S _key;
            public ObservableGroupCollection(IGrouping<S, T> group)
                : base(group)
            {
                _key = group.Key;
            }
            public S Key
            {
                get { return _key; }
            }
        }

        public IList<ASResult> Items { get; private set; }
        private List<ObservableGroupCollection<string, ASResult>> groupedDataValuetoUpdate = null;

        private List<ObservableGroupCollection<string, ASResult>> groupedData = null;

        public List<ObservableGroupCollection<string, ASResult>> GroupedData
        {
            get
            {
                return groupedData;
            }
            set
            {
                if (groupedData == value) return;

                if (groupedData != value)
                {
                    groupedData = value;
                }
                RaisePropertyChanged("GroupedData");
            }
        }

        private List<ObservableGroupCollection<string, ASResult>> _GroupedDataForDownload;

        public List<ObservableGroupCollection<string, ASResult>> GroupedDataForDownload
        {
            get
            {
                return _GroupedDataForDownload;
            }
            set
            {
                if (_GroupedDataForDownload == value) return;

                _GroupedDataForDownload = value;
                RaisePropertyChanged("GroupedDataForDownload");
            }
        }

        public bool isMonthWiseStatementsViewVisible = false;

        public bool IsMonthWiseStatementsViewVisible
        {
            get
            {
                return isMonthWiseStatementsViewVisible;
            }
            set
            {
                if (isMonthWiseStatementsViewVisible == value) return;

                if (isMonthWiseStatementsViewVisible != value)
                {
                    isMonthWiseStatementsViewVisible = value;
                    IsNormalStatementsViewVisible = !value;
                    RaisePropertyChanged("IsMonthWiseStatementsViewVisible");
                }
            }
        }


        public bool isTotalAmountVisible = false;

        public bool IsTotalAmountVisible
        {
            get
            {
                return isTotalAmountVisible;
            }
            set
            {
                if (isTotalAmountVisible == value) return;

                if (isTotalAmountVisible != value)
                {
                    isTotalAmountVisible = value;
                    RaisePropertyChanged("IsTotalAmountVisible");
                }
            }
        }
        public bool isNormalStatementsViewVisible = false;

        public bool IsNormalStatementsViewVisible
        {
            get
            {
                return isNormalStatementsViewVisible;
            }
            set
            {
                if (isNormalStatementsViewVisible == value) return;

                if (isNormalStatementsViewVisible != value)
                {
                    isNormalStatementsViewVisible = value;
                    RaisePropertyChanged("IsNormalStatementsViewVisible");
                }
            }
        }
        public ObservableCollection<ASResult> _statementsLineItemsDownloadPage = null;

        public ObservableCollection<ASResult> StatementsLineItemsDownloadPage
        {
            get
            {
                return _statementsLineItemsDownloadPage;
            }
            set
            {
                if (_statementsLineItemsDownloadPage == value) return;

                _statementsLineItemsDownloadPage = value;
                RaisePropertyChanged("StatementsLineItemsDownloadPage");
            }
        }
        public ObservableCollection<ASResult> _statementsLineItems = null;
        public ObservableCollection<ASResult> StatementsLineItems
        {
            get
            {
                return _statementsLineItems;
            }
            set
            {
                if (_statementsLineItems == value) return;
                if (_statementsLineItems != value)
                {
                    if (value.Count > 0)
                    {
                        IsNotHaveStatements = false;
                    }
                    else
                    {
                        IsNotHaveStatements = true;
                    }
                    if (IsNormalStatementsViewVisible)
                    {
                        GroupedData = new List<ObservableGroupCollection<string, ASResult>>();
                    }
                    else
                    {
                        Items = value.ToList();
                        List<ObservableGroupCollection<string, ASResult>> agroupedData;
                        if (App.IsArabic)
                        {
                            try
                            {
                                if (HeaderSet.D.CalType.Equals("G"))
                                {
                                    agroupedData = Items.OrderBy(p => p.Bldat)
                                  .GroupBy(p => UtilityManager.GetMonthName(p.Bldat?.ToString("MMMM", System.Globalization.CultureInfo.GetCultureInfo("en"))))
                                    .Select(p => new ObservableGroupCollection<string, ASResult>(p)).ToList();
                                }
                                else
                                {
                                    agroupedData = Items.OrderBy(p => p.Bldat)
                                  .GroupBy(p => p.Bldat?.ToString("MMMM", System.Globalization.CultureInfo.GetCultureInfo("ar")))
                                    .Select(p => new ObservableGroupCollection<string, ASResult>(p)).ToList();
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.Write(ex.ToString());
                                Console.Write(ex.StackTrace.ToString());
                                agroupedData = Items.OrderBy(p => p.Bldat)
                              .GroupBy(p => UtilityManager.GetMonthName(p.Bldat?.ToString("MMMM", System.Globalization.CultureInfo.GetCultureInfo("en"))))
                                .Select(p => new ObservableGroupCollection<string, ASResult>(p)).ToList();
                            }
                        }
                        else
                        {
                            try
                            {
                                if (HeaderSet.D.CalType.Equals("G"))
                                {
                                    agroupedData = Items.OrderBy(p => p.Bldat)
                                        .GroupBy(p => p.Bldat?.ToString("MMMM"))
                                        .Select(p => new ObservableGroupCollection<string, ASResult>(p)).ToList();
                                }
                                else
                                {
                                    agroupedData = Items.OrderBy(p => p.Bldat)
                                  .GroupBy(p => UtilityManager.GetMonthNameHijri(p.Bldat?.ToString("MM", System.Globalization.CultureInfo.GetCultureInfo("ar"))))
                                    .Select(p => new ObservableGroupCollection<string, ASResult>(p)).ToList();
                                }
                            }
                            catch (Exception ex)
                            {
                                agroupedData = Items.OrderBy(p => p.Bldat)
                                    .GroupBy(p => p.Bldat?.ToString("MMMM"))
                                    .Select(p => new ObservableGroupCollection<string, ASResult>(p)).ToList();
                            }
                        }

                        if (IsMonthWiseStatementsViewVisible)
                        {
                            GroupedData = agroupedData;
                        }
                        else
                        {
                            groupedDataValuetoUpdate = agroupedData;
                        }
                    }
                    _statementsLineItems = value;
                    RaisePropertyChanged("StatementsLineItems");
                }
            }
        }

        public ObservableCollection<MonthlyStatementsLineItem> _monthlyStatementsLineItems = null;
        public ObservableCollection<MonthlyStatementsLineItem> MonthlyStatementsLineItems
        {
            get
            {
                return _monthlyStatementsLineItems;
            }
            set
            {
                if (_monthlyStatementsLineItems == value) return;

                _monthlyStatementsLineItems = value;
                RaisePropertyChanged("MonthlyStatementsLineItems");
            }
        }

        public partial class MonthlyStatementsLineItem
        {

            public string monthName { get; set; }
            public ObservableCollection<ASResult> StatementsLineItems { get; set; }
        }

        public ASYearValuesHeader _yearValuesHeader = null;
        public ASYearValuesHeader YearValuesHeader
        {
            get
            {
                return _yearValuesHeader;
            }
            set
            {
                if (_yearValuesHeader == value) return;

                _yearValuesHeader = value;
                RaisePropertyChanged("YearValuesHeader");
            }
        }

        public ObservableCollection<ASReturnTypes> _TaxTypeForFilter = null;
        public ObservableCollection<ASReturnTypes> TaxTypeForFilter
        {
            get
            {
                return _TaxTypeForFilter;
            }
            set
            {
                if (_TaxTypeForFilter == value) return;

                _TaxTypeForFilter = value;
                RaisePropertyChanged("TaxTypeForFilter");
            }
        }


        public ObservableCollection<ASRevenueDropDownSetDataResults> _allTransactionFilters = null;
        public ObservableCollection<ASRevenueDropDownSetDataResults> AllTransactionFilters
        {
            get
            {
                return _allTransactionFilters;
            }
            set
            {
                if (_allTransactionFilters == value) return;

                _allTransactionFilters = value;
                RaisePropertyChanged("AllTransactionFilters");
            }
        }

        public ObservableCollection<TaxRelationSetResult> _transactionTypeFilter = new ObservableCollection<TaxRelationSetResult>();
        public ObservableCollection<TaxRelationSetResult> TransactionTypeFilter
        {
            get
            {
                return _transactionTypeFilter;
            }
            set
            {
                if (_transactionTypeFilter == value) return;

                if (value == null || value.Count == 0) return;
                value = new ObservableCollection<TaxRelationSetResult>(value.OrderBy(temp => temp.DisplayId).ToList());
                _transactionTypeFilter = value;
                RaisePropertyChanged("TransactionTypeFilter");
            }
        }

        private GenericPickerModel _pickerModel { get; set; }

        public GenericPickerModel PickerModel
        {
            get { return _pickerModel; }
            set
            {
                if (_pickerModel == value) return;

                _pickerModel = value;
                RaisePropertyChanged("PickerModel");
            }
        }

        public ObservableCollection<ASChipModel> _chipDataFilterlist = null;
        public ObservableCollection<ASChipModel> ChipDataFilterlist
        {
            get
            {
                return _chipDataFilterlist;
            }
            set
            {
                if (_chipDataFilterlist == value) return;

                _chipDataFilterlist = value;
                RaisePropertyChanged("ChipDataFilterlist");
            }
        }

        public List<ASChipModel> _chipDataFilterlistForYears = null;
        public List<ASChipModel> ChipDataFilterlistForYears
        {
            get
            {
                return _chipDataFilterlistForYears;
            }
            set
            {
                if (_chipDataFilterlistForYears == value) return;

                _chipDataFilterlistForYears = value;
                RaisePropertyChanged("ChipDataFilterlistForYears");
            }
        }

        public ASReturnTypes _SelectedTaxTypeForFilter = null;
        public ASReturnTypes SelectedTaxTypeForFilter
        {
            get
            {
                return _SelectedTaxTypeForFilter;
            }
            set
            {
                if (_SelectedTaxTypeForFilter == value) return;

                _SelectedTaxTypeForFilter = value;
                if (_SelectedTaxTypeForFilter != null)
                {
                    FilterLabelText = _SelectedTaxTypeForFilter.TaxType;
                    FilterOnTaxType(SelectedTaxTypeForFilter.Id);
                }
                RaisePropertyChanged("SelectedTaxTypeForFilter");
            }
        }

        public TaxRelationSetResult _selectedTransactionTypeFilter;
        public TaxRelationSetResult SelectedTransactionTypeFilter
        {
            get
            {
                return _selectedTransactionTypeFilter;
            }
            set
            {
                if (_selectedTransactionTypeFilter == value) return;

                if (value == null) return;
                _selectedTransactionTypeFilter = value;

               

                if (_selectedTransactionTypeFilter.StatementFilter != null)
                {
                    IsYearsChipVisible = true;
                }
                else
                {
                    IsYearsChipVisible = false;
                }
                RaisePropertyChanged("SelectedTransactionTypeFilter");
                if (_selectedTransactionTypeFilter.StatementFilter != null)
                {
                    Task.Run(async () =>
                    {
                        await PopulateDataInChipsForYears(SelectedTransactionTypeFilter.TaxType, SelectedTransactionTypeFilter.StatementFilter);
                        IsMonthWiseStatementsViewVisible = true;
                        GroupedData = groupedDataValuetoUpdate;
                        //StatementsLineItems = new ObservableCollection<ASResult>();
                    });
                }
            }
        }

        public ASChipModel _selectedTransactionType = null;
        public ASChipModel SelectedTransactionType
        {
            get
            {
                return _selectedTransactionType;
            }
            set
            {
                if (_selectedTransactionType == value) return;

                _selectedTransactionType = value;
                RaisePropertyChanged("SelectedTransactionType");
            }
        }
        public bool _IsNormalListDownloadPage = false;
        public bool IsNormalListDownloadPage
        {
            get
            {
                return _IsNormalListDownloadPage;
            }
            set
            {
                if (_IsNormalListDownloadPage == value) return;

                _IsNormalListDownloadPage = value;
                RaisePropertyChanged("IsNormalListDownloadPage");
            }
        }

        public bool _isYearsChipVisible;
        public bool IsYearsChipVisible
        {
            get
            {
                return _isYearsChipVisible;
            }
            set
            {
                if (_isYearsChipVisible == value) return;

                _isYearsChipVisible = value;
                RaisePropertyChanged("IsYearsChipVisible");
            }
        }

        public bool _isDownloadBtnVisile;
        public bool IsDownloadBtnVisile
        {
            get
            {
                return _isDownloadBtnVisile;
            }
            set
            {
                if (_isDownloadBtnVisile == value) return;

                _isDownloadBtnVisile = value;
                RaisePropertyChanged("IsDownloadBtnVisile");
            }
        }


        public bool _isNoStatementsAvaiableVisible;
        public bool IsNoStatementsAvaiableVisible
        {
            get
            {
                return _isNoStatementsAvaiableVisible;
            }
            set
            {
                if (_isNoStatementsAvaiableVisible == value) return;

                _isNoStatementsAvaiableVisible = value;
                RaisePropertyChanged("IsNoStatementsAvaiableVisible");
            }
        }


        public ASChipModel _selectedYear = null;
        public ASChipModel SelectedYear
        {
            get
            {
                return _selectedYear;
            }
            set
            {
                if (_selectedYear == value) return;

                _selectedYear = value;
                RaisePropertyChanged("SelectedYear");
            }
        }

        public string _filterLabelText;
        public string FilterLabelText
        {
            get
            {
                return _filterLabelText;
            }
            set
            {
                if (_filterLabelText == value) return;

                _filterLabelText = value;
                RaisePropertyChanged("FilterLabelText");
            }
        }

        private bool _isOpeningBalanceVisible = false;
        public bool IsOpeningBalanceVisible
        {
            get
            {
                return _isOpeningBalanceVisible;
            }

            set
            {
                if (_isOpeningBalanceVisible == value) return;

                _isOpeningBalanceVisible = value;
                RaisePropertyChanged("IsOpeningBalanceVisible");
            }
        }

        private bool _isSearchButtonVisible = true;
        public bool IsSearchButtonVisible
        {
            get
            {
                return _isSearchButtonVisible;
            }

            set
            {
                if (_isSearchButtonVisible == value) return;

                _isSearchButtonVisible = value;
                RaisePropertyChanged("IsSearchButtonVisible");
            }
        }

        private bool _isCloseButtonVisible = false;
        public bool IsCloseButtonVisible
        {
            get
            {
                return _isCloseButtonVisible;
            }

            set
            {
                if (_isCloseButtonVisible == value) return;

                _isCloseButtonVisible = value;
                RaisePropertyChanged("IsCloseButtonVisible");
            }
        }

        private bool _isSortByVisible = false;
        public bool IsSortByVisible
        {
            get
            {
                return _isSortByVisible;
            }

            set
            {
                if (_isSortByVisible == value) return;

                _isSortByVisible = value;
                RaisePropertyChanged("IsSortByVisible");
            }
        }

        public ObservableCollection<ASFilters> _filterList = null;
        public ObservableCollection<ASFilters> FilterList
        {
            get
            {
                return _filterList;
            }
            set
            {
                if (_filterList == value) return;

                _filterList = value;
                RaisePropertyChanged("FilterList");
            }
        }

        private bool _IsHijriCal = false;
        public bool IsHijriCal
        {
            get
            {
                return _IsHijriCal;
            }
            set
            {
                if (_IsHijriCal == value) return;

                _IsHijriCal = value;
                RaisePropertyChanged("IsHijriCal");
            }
        }

        public bool isTxStartDate = true;
        public bool isTaxPeriodStartDate = true;

        private ObservableCollection<object> _todayDateNormal;
        public ObservableCollection<object> TodayDateNormal
        {
            get
            {
                return _todayDateNormal;
            }
            set
            {
                if (_todayDateNormal == value) return;
                _todayDateNormal = value;
                RaisePropertyChanged("TodayDateNormal");
            }
        }
        private ObservableCollection<object> _todayDateinHijri;
        public ObservableCollection<object> TodayDateinHijri
        {
            get
            {
                return _todayDateinHijri;
            }
            set
            {
                if (_todayDateinHijri == value) return;

                _todayDateinHijri = value;
                RaisePropertyChanged("TodayDateinHijri");
            }
        }
        
        private String _txFromDate = "";
        public String TxFromDate
        {
            get
            {
                return _txFromDate;
            }
            set
            {
                if (_txFromDate == value) return;

                _txFromDate = value;
                RaisePropertyChanged("TxFromDate");
            }
        }
        private String _txToDate = "";
        public String TxToDate
        {
            get
            {
                return _txToDate;
            }
            set
            {
                if (_txToDate == value) return;

                _txToDate = value;
                RaisePropertyChanged("TxToDate");
            }
        }
        
        
        private String _tPFromDate = "";
        public String TPFromDate
        {
            get
            {
                return _tPFromDate;
            }
            set
            {
                if (_tPFromDate == value) return;

                _tPFromDate = value;
                RaisePropertyChanged("TPFromDate");
            }
        }
        private String _tPToDate = "";
        public String TPToDate
        {
            get
            {
                return _tPToDate;
            }
            set
            {
                if (_tPToDate == value) return;

                _tPToDate = value;
                RaisePropertyChanged("TPToDate");
            }
        }

        public static string CalType = "G";

        public String FromTxAmount = "";
        public String ToTxAmount = "";
        public AccountStatementsPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }

            GoBackBtnTapped = new Command(() =>
            {
                _navigationService.GoBack();
            });
            
            FilterCloseClick = new Command(() =>
            {
                _navigationService.GoBack();
            });
            
            FilterBtnCommand = new Command(() =>
            {
                ApplyFilter();
            });

            DownloadBtnTapped = new Command(DownloadBtnClicked);
            DownloadBtnTappedDownloadPage = new Command(DownloadBtnClickedDownloadPage);

            TransactionTypeFilter = new ObservableCollection<TaxRelationSetResult>();
            IsSortByVisible = false;
            FiltersTapped = new Command(FiltersClicked);
            FlowDirect = App.IsArabic ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
        }

        private void ApplyFilter()
        {
            if (HeaderSet == null)
            {
                _navigationService.GoBack();
                return;
            }

            var statementsLineItems = new ObservableCollection<ASResult>();
            Console.WriteLine(statementsLineItems);
            Console.WriteLine(statementsLineItems.Count);
            if (HeaderSet.D.StatmenetLineItemsSet != null)
            {
                if (HeaderSet.D.StatmenetLineItemsSet.Results.Count() > 0)
                {
                    statementsLineItems = new ObservableCollection<ASResult>(HeaderSet.D.StatmenetLineItemsSet.Results);
                }
                Console.WriteLine(statementsLineItems);
                Console.WriteLine(statementsLineItems.Count);
            }
            if (TxFromDate != "" && TxToDate != "")
            {
                var filterItems = statementsLineItems;
                Console.WriteLine(statementsLineItems);
                Console.WriteLine(statementsLineItems.Count);
                if (IsHijriCal)
                {
                    CultureInfo arCI = new CultureInfo("ar-SA");
                    DateTime FormatedTxFromDate = DateTime.ParseExact(TxFromDate, "yyyy/MM/dd", arCI.DateTimeFormat,
                        DateTimeStyles.AllowInnerWhite);
                    DateTime FormatedTxToDate = DateTime.ParseExact(TxToDate, "yyyy/MM/dd", arCI.DateTimeFormat,
                        DateTimeStyles.AllowInnerWhite);
                    
                    statementsLineItems = new ObservableCollection<ASResult>(filterItems.Where(p => p.Bldat >= FormatedTxFromDate && p.Bldat <= FormatedTxToDate));
                }
                else
                {
                    CultureInfo arCI = new CultureInfo("en-US");
                    DateTime FormatedTxFromDate = DateTime.ParseExact(TxFromDate, "yyyy/MM/dd", arCI.DateTimeFormat,
                        DateTimeStyles.AllowInnerWhite);
                    DateTime FormatedTxToDate = DateTime.ParseExact(TxToDate, "yyyy/MM/dd", arCI.DateTimeFormat,
                        DateTimeStyles.AllowInnerWhite);
                    statementsLineItems = new ObservableCollection<ASResult>(filterItems.Where(p => p.Bldat >= FormatedTxFromDate && p.Bldat <= FormatedTxToDate));
                    Console.WriteLine(statementsLineItems);
                    Console.WriteLine(statementsLineItems.Count);
                }
            }

            if (TPFromDate != "" && TPToDate != "")
            {
                var filterItems = statementsLineItems;
                Console.WriteLine(statementsLineItems);
                Console.WriteLine(statementsLineItems.Count);
                
                statementsLineItems = new ObservableCollection<ASResult>(filterItems.Where(p => (Convert.ToInt32(String.Format("{0:yyyy}",p.PeriodStartDt)) >= Convert.ToInt32(TPFromDate)) && (Convert.ToInt32(String.Format("{0:yyyy}",p.PeriodStartDt)) <= Convert.ToInt32(TPToDate))));
               // statementsLineItems = new ObservableCollection<ASResult>(filterItems.Where(p => (p.Persl) == "2021"));
                Console.WriteLine(statementsLineItems);
                Console.WriteLine(statementsLineItems.Count);
                /*if (IsHijriCal)
                {
                    CultureInfo arCI = new CultureInfo("ar-SA");
                    DateTime FormatedTPFromDate = DateTime.ParseExact(TPFromDate, "yyyy/MM/dd", arCI.DateTimeFormat,
                        DateTimeStyles.AllowInnerWhite);
                    DateTime FormatedTPToDate = DateTime.ParseExact(TPToDate, "yyyy/MM/dd", arCI.DateTimeFormat,
                        DateTimeStyles.AllowInnerWhite);
                    statementsLineItems = new ObservableCollection<ASResult>(filterItems.Where(p => p.Persl >= FormatedTPFromDate && p.Persl <= FormatedTPToDate));
                }
                else
                {
                    CultureInfo arCI = new CultureInfo("en-US");
                    DateTime FormatedTPFromDate = DateTime.ParseExact(TPFromDate, "yyyy/MM/dd", arCI.DateTimeFormat,
                        DateTimeStyles.AllowInnerWhite);
                    DateTime FormatedTPToDate = DateTime.ParseExact(TPToDate, "yyyy/MM/dd", arCI.DateTimeFormat,
                        DateTimeStyles.AllowInnerWhite);
                    statementsLineItems = new ObservableCollection<ASResult>(filterItems.Where(p => p.Persl >= FormatedTPFromDate && p.Persl <= FormatedTPToDate));
                    Console.WriteLine(statementsLineItems);
                    Console.WriteLine(statementsLineItems.Count);
                }*/
                
            }

            if (FromTxAmount != "" && ToTxAmount != "")
            {
                var filterItems = statementsLineItems;
                statementsLineItems = new ObservableCollection<ASResult>(filterItems.Where(p => Convert.ToDouble(p.Betrh) >= Convert.ToDouble(FromTxAmount) && Convert.ToDouble(p.Betrh) <= Convert.ToDouble(ToTxAmount)));
            }

            StatementsLineItems = statementsLineItems;
            _navigationService.GoBack();

        }

        public void ClearFilterItems()
        {
            TransactionDateFilterItem = string.Empty;
            BillAmountFilterItem = string.Empty;
            BillDescriptionFilterItem = string.Empty;
            DueDateFilterItem = string.Empty;
            FBNumFilterItem = string.Empty;
            SadadBillNumberFilterItem = string.Empty;
            StatusFilterItem = string.Empty;
            TaxperiodFilterItem = string.Empty;
            TaxTypeFilterItem = string.Empty;

        }
        public void FiltersClicked()
        {
            _navigationService.NavigateTo(App.AccountStatementsNewFilterPageView);
            IsSortByVisible = !IsSortByVisible;
            //IsMonthWiseStatementsViewVisible = !IsMonthWiseStatementsViewVisible;
        }

         public void SetDefaultDate()
        {
            ObservableCollection<object> todaycollection = new ObservableCollection<object>();
            //Select today dates

            if (DateTime.Now.Date.Day < 10)
                todaycollection.Add("0" + DateTime.Now.Date.Day);
            else
                todaycollection.Add(DateTime.Now.Date.Day.ToString());
            if (DateTime.Now.Date.Month < 10)
                todaycollection.Add("0" + DateTime.Now.Date.Month);
            else
                todaycollection.Add(DateTime.Now.Date.Month.ToString());
            todaycollection.Add(DateTime.Now.Date.Year.ToString());
            TodayDateNormal = todaycollection;
            //TodayDateEnd = todaycollection;
            //DefaultMonth = DateTime.Now.Date.Month;

            //TodayDateinHijri
            ObservableCollection<object> todaycollectionHijri = new ObservableCollection<object>();
            var calendar = new HijriCalendar();
            if (calendar.GetDayOfMonth(DateTime.Now.Date) < 10)
                todaycollectionHijri.Add("0" + calendar.GetDayOfMonth(DateTime.Now.Date).ToString());
            else
                todaycollectionHijri.Add(calendar.GetDayOfMonth(DateTime.Now.Date).ToString());
            if (calendar.GetMonth(DateTime.Now.Date) < 10)
                todaycollectionHijri.Add("0" + calendar.GetMonth(DateTime.Now.Date));
            else
                todaycollectionHijri.Add(calendar.GetMonth(DateTime.Now.Date).ToString());
            todaycollectionHijri.Add(calendar.GetYear(DateTime.Now.Date).ToString());

            TodayDateinHijri = todaycollectionHijri;
            //TodayDateinHijriEnd = todaycollectionHijri;

            /*if (ContractReleaseData != null)
            {


                if (ContractReleaseData.d.ACalTp == "H")
                {

                    FromDate = HDateNow();
                    ToDate = HDateNow();
                }
                else
                {
                    FromDate = (TodayDateStart[2] + "/" + TodayDateStart[1] + "/" + TodayDateStart[0]).ToString();
                    ToDate = (TodayDateEnd[2] + "/" + TodayDateEnd[1] + "/" + TodayDateEnd[0]).ToString();
                }

            }*/
        }


        public string HDateNow()
        {
            try
            {

                CultureInfo calCul;
                    if (IsHijriCal)
                    {
                        calCul = new CultureInfo("ar-SA");
                    }
                    else
                    {
                        calCul = new CultureInfo("en-US");
                    }
                    
                return DateTime.Now.ToString("yyyy/MM/dd", calCul.DateTimeFormat);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());

                return "";
            }
        }

        public void DownloadBtnClicked()
        {
            String pdfUrl = Constants.AccountStatementDownloadPdf + "Fguid='" + App.LoginDataRetrieved.FbGuid + "'" + ",Taxtype='" + SelectedTaxTypeForFilter.Id + "',FiscalYear='" + SelectedYear.Text + "',StatementFilter='" + SelectedTransactionTypeFilter.StatementFilter + "',FromDt=datetime'2020-1-1T00:00:00',ToDt=datetime'2020-11-6T00:00:00',Langz='" + GetLangZParameter() + "')/$value";
            ShowPdf(pdfUrl);
        }

        private static char GetLangZParameter()
        {
            if (App.IsArabic)
                return 'A';
            else
                return 'E';
        }
        public void DownloadBtnClickedDownloadPage()
        {
            DateTime dateTimeFrom = DateTime.Parse(FromDate);
            DateTime dateTimeTo = DateTime.Parse(ToDate);

            if (dateTimeFrom > dateTimeTo)
            {
                _dialogService.ShowMessage(AppResources.ASFromDateShouldNotbeGreaterThanToDate, AppResources.Information);
            }
            else
            {
                var temp = (dateTimeTo - dateTimeFrom).TotalDays;
                if (temp > 365)
                {
                    _dialogService.ShowMessage(AppResources.ASViewStatementForOnlyOneYear, AppResources.Information);
                }
                else
                {
                    int year;
                    if (Int32.Parse(ASTaxpayerSelectedValues.Year) > 1500)
                    {
                        year = Int32.Parse(ASTaxpayerSelectedValues.Year);
                    }
                    else
                    {
                        string[] tempyear = UtilityManager.HijriToGreg("01 / 01 /" + ASTaxpayerSelectedValues.Year).Split('/');
                        year = Int32.Parse(tempyear[0]);
                    }
                    if (year >= dateTimeFrom.Year && year <= dateTimeTo.Year)
                    {
                        if (IsNormalListDownloadPage)
                        {
                            if (StatementsLineItemsDownloadPage.Where(p => p.Bldat >= dateTimeFrom && p.Bldat <= dateTimeTo).Count() > 0)
                            {
                                string fromStr = dateTimeFrom.ToString("yyyy-MM-dd");
                                string toStr = dateTimeTo.ToString("yyyy-MM-dd");

                                String pdfUrl = Constants.AccountStatementDownloadPdf + "Fguid='" + App.LoginDataRetrieved.FbGuid + "'" + ",Taxtype='" + ASTaxpayerSelectedValues.TaxType + "',FiscalYear='" + dateTimeFrom.Year + "',StatementFilter='" + ASTaxpayerSelectedValues.StatementFilter + "',FromDt=datetime'" + fromStr + "T00:00:00',ToDt=datetime'" + toStr + "T00:00:00',Langz='" + GetLangZParameter() + "')/$value";
                                ShowPdf1(pdfUrl);
                            }
                            else
                            {
                                _dialogService.ShowMessage(AppResources.Therearenofinancialtransactions, AppResources.Information);
                            }
                        }
                        else
                        {
                            if (GroupedDataForDownload.First().Where(p => p.Bldat >= dateTimeFrom && p.Bldat <= dateTimeTo).Count() > 0)
                            {
                                string fromStr = dateTimeFrom.ToString("yyyy-MM-dd");
                                string toStr = dateTimeTo.ToString("yyyy-MM-dd");

                                String pdfUrl = Constants.AccountStatementDownloadPdf + "Fguid='" + App.LoginDataRetrieved.FbGuid + "'" + ",Taxtype='" + ASTaxpayerSelectedValues.TaxType + "',FiscalYear='" + dateTimeFrom.Year + "',StatementFilter='" + ASTaxpayerSelectedValues.StatementFilter + "',FromDt=datetime'" + fromStr + "T00:00:00',ToDt=datetime'" + toStr + "T00:00:00',Langz='" + GetLangZParameter() + "')/$value";
                                ShowPdf1(pdfUrl);
                            }
                            else
                            {
                                _dialogService.ShowMessage(AppResources.Therearenofinancialtransactions, AppResources.Information);
                            }
                        }
                    }
                    else
                    {
                        _dialogService.ShowMessage(AppResources.ASViewStatementForOnlyOneYear, AppResources.Information);
                    }
                }
            }

        }

        public void ShowPdf1(string pdfUrl)
        {
            try
            {
                if (pdfUrl != null)
                {
                    _navigationService.NavigateTo(App.PdfView, pdfUrl);
                }
                else
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PdfIsNoteAvailable));
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ShowPdf(string pdfUrl)
        {
            try
            {
                if (pdfUrl != null)
                {
                    ASTaxpayerSelectedValues aSTaxpayerSelectedValues = new ASTaxpayerSelectedValues();
                    aSTaxpayerSelectedValues.TaxType = SelectedTaxTypeForFilter.Id;
                    aSTaxpayerSelectedValues.StatementFilter = SelectedTransactionTypeFilter.StatementFilter;
                    aSTaxpayerSelectedValues.Year = SelectedYear.Text;
                    DataForDownloadPage Data = new DataForDownloadPage();
                    Data.ASTaxpayerSelectedValues = aSTaxpayerSelectedValues;
                    Data.GroupedDataForDownload = groupedData;
                    Data.StatementsLineItems = StatementsLineItems;
                    if (IsNormalStatementsViewVisible)
                    {
                        Data.isNormalList = true;
                    }
                    else
                    {
                        Data.isNormalList = false;
                    }
                    _navigationService.NavigateTo(App.AccountStatementsDownloadPageView, Data);
                }
                else
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PdfIsNoteAvailable));
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task PopulateReturnTypeList()
        {
            try
            {

                TabIdentification = await WebServiceManager.GAZTGetAccountStatementsTabIdentification();
                TaxTypeForFilter = new ObservableCollection<ASReturnTypes>();

                StatementsLineItems = new ObservableCollection<ASResult>();
                GroupedStatements = new List<GroupedAccountStatements>();

                var tempDirectTax = new ASReturnTypes { Id = "D", TaxType = AppResources.ASAccountStatementDirectTax };
                var tempInDirectTax = new ASReturnTypes { Id = "I", TaxType = AppResources.ASAccountStatementInDirectTax };

                if (TabIdentification.D.Direct == "X")
                {
                    TaxTypeForFilter.Add(tempDirectTax);
                }

                if (TabIdentification.D.Indirect == "X")
                {
                    TaxTypeForFilter.Add(tempInDirectTax);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }

        public async void FilterOnTaxType(string taxType)
        {
            //API Call
            try
            {
                string statementFilter = string.Empty;
                if (taxType == "D")
                {
                    statementFilter = "04";
                }
                if (taxType == "I")
                {
                    statementFilter = "08";
                }

                IsLoading = true;
                HeaderSet = await WebServiceManager.GAZTGetAccountStatementHeaderSet(statementFilter, string.Empty, taxType,false);
                if (HeaderSet.D.StatmenetLineItemsSet != null)
                {
                    if (HeaderSet.D.StatmenetLineItemsSet.Results.Count() > 0)
                    {
                        IsDownloadBtnVisile = true;
                        IsNoStatementsAvaiableVisible = false;
                        StatementsLineItems = new ObservableCollection<ASResult>(HeaderSet.D.StatmenetLineItemsSet.Results);
                    }
                    else
                    {
                        IsDownloadBtnVisile = false;
                        IsNoStatementsAvaiableVisible = true;
                        StatementsLineItems = new ObservableCollection<ASResult>(HeaderSet.D.StatmenetLineItemsSet.Results);
                        GroupedStatements = new List<GroupedAccountStatements>();
                    }
                }
                else
                {
                    IsDownloadBtnVisile = false;
                    IsNoStatementsAvaiableVisible = true;
                }
                AccStmtnCreditAmount = HeaderSet.D.CreditAmount.Replace("-", string.Empty);
                if (StatementsLineItems == null)
                {
                    StatementsLineItems = new ObservableCollection<ASResult>();
                }
                if (GroupedStatements == null)
                {
                    GroupedStatements = new List<GroupedAccountStatements>();
                }
                StatementsLineItems.Clear();
                GroupedStatements.Clear();

                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (Exception)
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
        }

        public async Task PopulateDataForTransactionTypes(string taxType)
        {
            var tempValues = await WebServiceManager.GAZTGetAccountStatementsRevenueDropDownSet(taxType);
            foreach (ASRevenueDropDownSetDataResults aSRevenueDropDownSetDataResults in tempValues.D.Results)
            {
                aSRevenueDropDownSetDataResults.TaxType = taxType;
                AllTransactionFilters.Add(aSRevenueDropDownSetDataResults);
                
            }
        }

        public async Task<bool> PopulateDataInChipsForYears(string taxType, string statementFilter)
        {
            try
            {
                IsLoading = true;
                YearValuesHeader = await WebServiceManager.GAZTGetAccountStatementYearValuesHeaderSet(statementFilter, taxType);
                HeaderSet = await WebServiceManager.GAZTGetAccountStatementHeaderSet(statementFilter, string.Empty, taxType,false);
                IsTotalAmountVisible = true;
                if (HeaderSet.D.StatmenetLineItemsSet != null)
                {
                    if (HeaderSet.D.StatmenetLineItemsSet.Results.Count() > 0)
                    {
                        IsDownloadBtnVisile = true;
                        IsNoStatementsAvaiableVisible = false;
                        StatementsLineItems = new ObservableCollection<ASResult>(HeaderSet.D.StatmenetLineItemsSet.Results);
                    }
                    else
                    {
                        IsDownloadBtnVisile = false;
                        IsNoStatementsAvaiableVisible = true;
                        StatementsLineItems = new ObservableCollection<ASResult>(HeaderSet.D.StatmenetLineItemsSet.Results);
                        GroupedStatements = new List<GroupedAccountStatements>();
                    }
                }
                else
                {
                    IsDownloadBtnVisile = false;
                    IsNoStatementsAvaiableVisible = true;
                }
                AccStmtnCreditAmount = HeaderSet.D.CreditAmount.Replace("-", string.Empty);
                var chipDataFilterlistForYears = new List<ASChipModel>();
                var orderedChipDataFilterlistForYears = new List<ASChipModel>();
                if (YearValuesHeader != null && YearValuesHeader.D != null)
                {
                    foreach (ASYearValuesResults aSYearValuesResults in YearValuesHeader.D.Results)
                    {
                        chipDataFilterlistForYears.Add(new ASChipModel() { Text = aSYearValuesResults.Persl, TemplateType = AppResources.Paid });
                    }
                }
                else
                {
                    chipDataFilterlistForYears.Add(new ASChipModel() { Text = "2015", TemplateType = AppResources.Paid });
                    chipDataFilterlistForYears.Add(new ASChipModel() { Text = "2016", TemplateType = AppResources.Paid });
                    chipDataFilterlistForYears.Add(new ASChipModel() { Text = "2017", TemplateType = AppResources.Paid });
                    chipDataFilterlistForYears.Add(new ASChipModel() { Text = "2018", TemplateType = AppResources.Paid });
                    chipDataFilterlistForYears.Add(new ASChipModel() { Text = "2019", TemplateType = AppResources.Paid });
                }
                var desc = chipDataFilterlistForYears.OrderByDescending(item => item.Text);
                foreach (ASChipModel aSChipModel in desc)
                {
                    orderedChipDataFilterlistForYears.Add(aSChipModel);
                }

                ChipDataFilterlistForYears = orderedChipDataFilterlistForYears;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                IsLoading = false;
            }
            return true;
        }

        public async Task PopulateASFilterData()
        {
            try
            {
                IsLoading = true;
                TransactionTypeDropDownParent = new ASRevenueDropDownSet();
                if (AllTransactionFilters == null)
                {
                    AllTransactionFilters = new ObservableCollection<ASRevenueDropDownSetDataResults>();
                }
                AllTransactionFilters.Clear();
               
              /*  ASRevenueDropDownSetDataResults defautlValIndirectTax = new ASRevenueDropDownSetDataResults();
                defautlValIndirectTax.Txt30 = AppResources.ASTransactionType;
                defautlValIndirectTax.TaxType = "I";
                defautlValIndirectTax.TaxType = "I";
                AllTransactionFilters.Insert(1, defautlValIndirectTax);*/
                if (TabIdentification.D.Direct == "X")
                {
                    await PopulateDataForTransactionTypes("D");
                }
                if (TabIdentification.D.Indirect == "X")
                {
                    await PopulateDataForTransactionTypes("I");
                }

                HeaderSet = await WebServiceManager.GAZTGetAccountStatementHeaderSet
                    (AllTransactionFilters.FirstOrDefault().StatementFilter, string.Empty, AllTransactionFilters.FirstOrDefault().TaxType,false);
                if (HeaderSet.D.StatmenetLineItemsSet != null)
                {
                    if (HeaderSet.D.StatmenetLineItemsSet.Results.Count() > 0)
                    {
                        IsDownloadBtnVisile = true;
                        IsNoStatementsAvaiableVisible = false;
                        StatementsLineItems = new ObservableCollection<ASResult>(HeaderSet.D.StatmenetLineItemsSet.Results);
                    }
                    else
                    {
                        IsDownloadBtnVisile = false;
                        IsNoStatementsAvaiableVisible = true;
                        StatementsLineItems = new ObservableCollection<ASResult>(HeaderSet.D.StatmenetLineItemsSet.Results);
                        GroupedStatements = new List<GroupedAccountStatements>();
                    }
                }
                else
                {
                    IsDownloadBtnVisile = false;
                    IsNoStatementsAvaiableVisible = true;
                }
                AccStmtnCreditAmount = HeaderSet.D.CreditAmount.Replace("-", string.Empty);
                IsOpeningBalanceVisible = false;
                IsDownloadBtnVisile = false;
                foreach (ASReturnTypes aSReturnTypes in TaxTypeForFilter)
                {
                    if (HeaderSet.D.TaxType == aSReturnTypes.Id)
                    {
                        SelectedTaxTypeForFilter = aSReturnTypes;
                    }
                    else
                    {
                        SelectedTaxTypeForFilter = TaxTypeForFilter.FirstOrDefault();
                    }
                }



                foreach (TaxRelationSetResult taxRelationSetResult in HeaderSet.D.TaxRelationSet.Results)
                {
                    if (TabIdentification.D.Direct == "X")
                    {
                        if (taxRelationSetResult.StatementFilter == "10")
                        {
                            taxRelationSetResult.DisplayId = 01;
                        }
                        if (taxRelationSetResult.StatementFilter == "01")
                        {
                            taxRelationSetResult.DisplayId = 02;
                        }
                        
                        if (taxRelationSetResult.StatementFilter == "02")
                        {
                            taxRelationSetResult.DisplayId = 03;
                        }
                        if (taxRelationSetResult.StatementFilter == "03")
                        {
                            taxRelationSetResult.DisplayId = 04;
                        }
                    }
                    if (TabIdentification.D.Indirect == "X")
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
                        if (taxRelationSetResult.StatementFilter == "10")
                        {
                            taxRelationSetResult.DisplayId = 01;
                        }
                    }
                }
                if (TransactionTypeFilter == null)
                {
                    TransactionTypeFilter = new ObservableCollection<TaxRelationSetResult>();
                }
                TransactionTypeFilter = new ObservableCollection<TaxRelationSetResult>(HeaderSet.D.TaxRelationSet.Results.Where(temp =>  temp.DisplayId == 01 || temp.DisplayId == 02 || temp.DisplayId == 03 || temp.DisplayId == 04 || temp.DisplayId == 06 || temp.DisplayId == 07 || temp.DisplayId == 09).ToList());


                SelectedTransactionTypeFilter = TransactionTypeFilter.FirstOrDefault();

                
                var list = new List<string>();

                foreach (TaxRelationSetResult dropdown in TransactionTypeFilter)
                {
                    
                   
                        list.Add(dropdown.Txt30.ToUpper());
                    


                    
                }

                GenericPickerModel genericPickerModel = new GenericPickerModel();
                genericPickerModel.PickerData = list;
                genericPickerModel.PickerTitle = "";
                genericPickerModel.PickerId = "AccountStatement";
                genericPickerModel.SelectedValue = SelectedTransactionTypeFilter.Txt30;

                PickerModel = genericPickerModel;

                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (GAZTErrorException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
            }
            catch (InternetException ex)
            {
                IsLoading = false;
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                _navigationService.GoBack();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
        }


        public void updatePicker()
        {

            var selectedFilter = new ObservableCollection<TaxRelationSetResult>(TransactionTypeFilter.Where(temp => temp.Txt30.Equals(PickerModel.SelectedValue.ToUpper()))).ToList();

            SelectedTransactionTypeFilter = selectedFilter.FirstOrDefault();

           

        }

        public async void showPickerDialog()
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new PickerPageView(PickerModel));
            }
            catch (GAZTUnlockAccountException ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }


        public async void PopulateStatements(string taxType, string statementFilter, string year)
        {
            try
            {
                IsLoading = true;
                HeaderSet = await WebServiceManager.GAZTGetAccountStatementHeaderSet(statementFilter, year, taxType,false);
                AccStmtnCreditAmount = HeaderSet.D.CreditAmount.Replace("-", string.Empty);
                if (HeaderSet.D.StatmenetLineItemsSet != null)
                {
                    if (HeaderSet.D.StatmenetLineItemsSet.Results.Count() > 0)
                    {
                        IsDownloadBtnVisile = true;
                        IsNoStatementsAvaiableVisible = false;
                        StatementsLineItems = new ObservableCollection<ASResult>(HeaderSet.D.StatmenetLineItemsSet.Results);
                    }
                    else
                    {
                        IsDownloadBtnVisile = false;
                        IsNoStatementsAvaiableVisible = true;
                        StatementsLineItems = new ObservableCollection<ASResult>(HeaderSet.D.StatmenetLineItemsSet.Results);
                        GroupedStatements = new List<GroupedAccountStatements>();
                    }
                }
                else
                {
                    IsDownloadBtnVisile = false;
                    IsNoStatementsAvaiableVisible = true;
                }
                IsLoading = false;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
        }
        public static string GetMonthName(string Month)
        {
            if (Month == "01" || Month == "1" || Month == "يناير")
            {
                Month = "January";
            }
            else if (Month == "02" || Month == "2" || Month == "فبراير")
            {
                Month = "February";
            }
            else if (Month == "03" || Month == "3" || Month == "مارس")
            {
                Month = "March";
            }
            else if (Month == "04" || Month == "4" || Month == "أبريل")
            {
                Month = "April";
            }
            else if (Month == "05" || Month == "5" || Month == "مايو")
            {
                Month = "May";
            }
            else if (Month == "06" || Month == "6" || Month == "يونيو")
            {
                Month = "June";
            }
            else if (Month == "07" || Month == "7" || Month == "يوليو")
            {
                Month = "July";
            }
            else if (Month == "08" || Month == "8" || Month == "أغسطس")
            {
                Month = "August";
            }
            else if (Month == "09" || Month == "9" || Month == "سبتمبر")
            {
                Month = "September";
            }
            else if (Month == "10" || Month == "أكتوبر")
            {
                Month = "October";
            }
            else if (Month == "11" || Month == "نوفمبر")
            {
                Month = "November";
            }
            else if (Month == "12" || Month == "ديسمبر")
            {
                Month = "December";
            }
            return Month;
        }

        public void PopulateFiltersData()
        {
            List<ASFilters> filters = new List<ASFilters>();
            filters.Add(new ASFilters
            {
                FilterHeader = AppResources.ASTransactionDate
            });
            filters.Add(new ASFilters
            {
                FilterHeader = AppResources.TaxType
            });
            filters.Add(new ASFilters
            {
                FilterHeader = AppResources.ASFBNum
            });
            filters.Add(new ASFilters
            {
                FilterHeader = AppResources.ASSadadBillNumber
            });
            filters.Add(new ASFilters
            {
                FilterHeader = AppResources.ASTaxperiod
            });
            filters.Add(new ASFilters
            {
                FilterHeader = AppResources.ASDueDate
            });
            filters.Add(new ASFilters
            {
                FilterHeader = AppResources.ASBillDescription
            });
            filters.Add(new ASFilters
            {
                FilterHeader = AppResources.ASBillAmount
            });
            filters.Add(new ASFilters
            {
                FilterHeader = AppResources.ZStatus
            });
            FilterList = new ObservableCollection<ASFilters>(filters);
        }
    }
}
