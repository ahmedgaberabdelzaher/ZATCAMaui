using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models.AccountStatements;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.AccountStatements
{
    public class AccountStatementsPageViewModel : BaseViewModel
    {

        public ICommand GoBackBtnTapped { get; set; }
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

        private DateTime _FromDateDownloadPage ;
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

        //Filter Item properties start
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
        
        //Filter Item properties  end
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

        private List<ObservableGroupCollection<string, ASResult>> _GroupedDataForDownload ;

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

        public bool isMonthWiseStatementsViewVisible = true;

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
                        //_dialogService.ShowMessage(AppResources.ASNoFinancialTransactions, AppResources.ZError);
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
                                  .GroupBy(p => UtilityManager.GetMonthNameHijri(p.Bldat?.ToString("MM", System.Globalization.CultureInfo.GetCultureInfo("ar"))) )
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

                        GroupedData = agroupedData;
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
                            StatementsLineItems = new ObservableCollection<ASResult>();
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

        //IsSortByVisible
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

            DownloadBtnTapped = new Command(DownloadBtnClicked);
            DownloadBtnTappedDownloadPage = new Command(DownloadBtnClickedDownloadPage);

            TransactionTypeFilter = new ObservableCollection<TaxRelationSetResult>();
            IsSortByVisible = false;
            FiltersTapped = new Command(FiltersClicked);
            FlowDirect = App.IsArabic ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
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
            IsSortByVisible = !IsSortByVisible;
            IsMonthWiseStatementsViewVisible = !IsMonthWiseStatementsViewVisible;
            //await PopupNavigation.Instance.PushAsync(new AccountStatementsFiltersPageView());
        }

        public void DownloadBtnClicked()
        {
            String pdfUrl = Constants.AccountStatementDownloadPdf + "Fguid='" + App.LoginDataRetrieved.FbGuid + "'" + ",Taxtype='" + SelectedTaxTypeForFilter.Id + "',FiscalYear='" + SelectedYear.Text + "',StatementFilter='" + SelectedTransactionTypeFilter.StatementFilter + "',FromDt=datetime'2020-1-1T00:00:00',ToDt=datetime'2020-11-6T00:00:00',Langz='" + GetLangZParameter() + "')/$value";
            ShowPdf(pdfUrl);
            //await WebServiceManager.GAZTGetAccountStatementDownloadPdf(SelectedTransactionTypeFilter.StatementFilter, SelectedTaxTypeForFilter.Id, SelectedYear.Text, string.Empty, string.Empty);
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
                        string[]  tempyear = UtilityManager.HijriToGreg("01 / 01 /"+ASTaxpayerSelectedValues.Year).Split('/');

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

                HeaderSet = await WebServiceManager.GAZTGetAccountStatementHeaderSet(statementFilter, string.Empty, taxType);

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

            // TransactionTypeFilter = new ObservableCollection<ASRevenueDropDownSetDataResults>(AllTransactionFilters.Where(x => x.TaxType.Equals(taxType)).ToList());
            // SelectedTransactionTypeFilter = TransactionTypeFilter.FirstOrDefault();
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

                HeaderSet = await WebServiceManager.GAZTGetAccountStatementHeaderSet(statementFilter, string.Empty, taxType);

                AccStmtnCreditAmount = HeaderSet.D.CreditAmount.Replace("-", string.Empty);

                var chipDataFilterlistForYears = new List<ASChipModel>();
                ChipDataFilterlistForYears = new List<ASChipModel>();


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
                    ChipDataFilterlistForYears.Add(aSChipModel);
                }
            }
            catch(Exception ex)
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

                ASRevenueDropDownSetDataResults defautlVal = new ASRevenueDropDownSetDataResults();
                defautlVal.Txt30 = AppResources.ASTransactionType;
                defautlVal.TaxType = "D";

                AllTransactionFilters.Insert(0, defautlVal);

                ASRevenueDropDownSetDataResults defautlValIndirectTax = new ASRevenueDropDownSetDataResults();
                defautlValIndirectTax.Txt30 = AppResources.ASTransactionType;
                defautlValIndirectTax.TaxType = "I";

                AllTransactionFilters.Insert(1, defautlValIndirectTax);

                if (TabIdentification.D.Direct == "X")
                {
                    await PopulateDataForTransactionTypes("D");
                }

                if (TabIdentification.D.Indirect == "X")
                {
                    await PopulateDataForTransactionTypes("I");
                }

                HeaderSet = await WebServiceManager.GAZTGetAccountStatementHeaderSet
                    (AllTransactionFilters.FirstOrDefault().StatementFilter, string.Empty, AllTransactionFilters.FirstOrDefault().TaxType);
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

                //TransactionTypeFilter = new ObservableCollection<TaxRelationSetResult>(HeaderSet.D.TaxRelationSet.Results.ToList());


                foreach (TaxRelationSetResult taxRelationSetResult in HeaderSet.D.TaxRelationSet.Results)
                {
                    if (TabIdentification.D.Direct == "X")
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
                    }
                }

                if (TransactionTypeFilter == null)
                {
                    TransactionTypeFilter = new ObservableCollection<TaxRelationSetResult>();
                }

                TransactionTypeFilter = new ObservableCollection<TaxRelationSetResult>(HeaderSet.D.TaxRelationSet.Results.Where(temp => temp.DisplayId == 01 || temp.DisplayId == 02 || temp.DisplayId == 03 || temp.DisplayId == 06 || temp.DisplayId == 07 || temp.DisplayId == 09).ToList());
                SelectedTransactionTypeFilter = TransactionTypeFilter.FirstOrDefault();

                /*if (HeaderSet.D.TaxType == "D")
                {
                    FilterOnTaxType("D");
                }
                else if (HeaderSet.D.TaxType == "I")
                {
                    FilterOnTaxType("I");
                }*/

                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                //if (HeaderSet != null && HeaderSet.D != null)
                //{
                //    TotalDebit = HeaderSet.D.DebitAmount;
                //    TotalCredit = HeaderSet.D.CreditAmount;
                //    TotalBalance = HeaderSet.D.CloseAmount;

                //}
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

        public async void PopulateStatements(string taxType, string statementFilter, string year)
        {
            try
            {
                    IsLoading = true;

                HeaderSet = await WebServiceManager.GAZTGetAccountStatementHeaderSet(statementFilter, year, taxType);

                AccStmtnCreditAmount = HeaderSet.D.CreditAmount.Replace("-", string.Empty);

                if (HeaderSet.D.StatmenetLineItemsSet != null)
                {
                    if (HeaderSet.D.StatmenetLineItemsSet.Results.Count() > 0)
                    {
                        IsDownloadBtnVisile = true;
                        IsNoStatementsAvaiableVisible = false;
                        StatementsLineItems = new ObservableCollection<ASResult>(HeaderSet.D.StatmenetLineItemsSet.Results);

                        //StatementsLineItems.OrderByDescending(p => DateTime.Parse(p.FormattedBldat));

                        //List<GroupedAccountStatements> list = new List<GroupedAccountStatements>();
                        //foreach (var item in StatementsLineItems)
                        //{
                        //    CultureInfo calCul;

                        //    if (App.IsArabic == true)
                        //    {
                        //        calCul = new CultureInfo("ar-SA");
                        //    }
                        //    else
                        //    {
                        //        calCul = new CultureInfo("en-US");
                        //    }

                        //    var test = DateTime.Parse(item.FormattedBldat);

                        //    var innerList = StatementsLineItems.Where(p => p.FormattedBldat == item.FormattedBldat).ToList();

                        //    if (!list.Any(p => p.Date.ToString("MMMM",calCul) == DateTime.Parse(item.FormattedBldat, calCul).ToString("MMMM")))
                        //    {
                        //        list.Add(new GroupedAccountStatements(item, innerList));
                        //    }
                        //}

                        //GroupedStatements = new List<GroupedAccountStatements>(list);
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
                // Month = "January";
            }
            else if (Month == "02" || Month == "2" || Month == "فبراير")
            {
                Month = "February";
                //  Month = "February";
            }
            else if (Month == "03" || Month == "3" || Month == "مارس")
            {
                Month = "March";
                // Month = "March";
            }
            else if (Month == "04" || Month == "4" || Month == "أبريل")
            {
                Month = "April";
                // Month = "April";
            }
            else if (Month == "05" || Month == "5" || Month == "مايو")
            {
                Month = "May";
                // Month = "May";
            }
            else if (Month == "06" || Month == "6" || Month == "يونيو")
            {
                Month = "June";
                //  Month = "June";
            }
            else if (Month == "07" || Month == "7" || Month == "يوليو")
            {
                Month = "July";
                //Month = "July";
            }
            else if (Month == "08" || Month == "8" || Month == "أغسطس")
            {

                Month = "August";
                //  Month = "August";
            }
            else if (Month == "09" || Month == "9" || Month == "سبتمبر")
            {
                Month = "September";
                //  Month = "September";
            }
            else if (Month == "10" || Month == "أكتوبر")
            {
                Month = "October";
                //Month = "October";
            }
            else if (Month == "11" || Month == "نوفمبر")
            {
                Month = "November";
                // Month = "November";
            }
            else if (Month == "12" || Month == "ديسمبر")
            {
                Month = "December";
                //   Month = "December";
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

        //public async Task PopToRootPage()
        //{
        //    try
        //    {
        //        if (App.IsSessionExpired)
        //        {
        //            Device.BeginInvokeOnMainThread(async () =>
        //            {
        //                if (App.TP != null)
        //                    App.TP = null;
        //                if (App.PreviousIsArabic)
        //                {
        //                    String langName = "ar-SA";
        //                    AppResources.Culture = new CultureInfo(langName);
        //                }
        //                else
        //                {
        //                    String langName = "en-US";
        //                    AppResources.Culture = new CultureInfo(langName);
        //                }
        //                var _navigation = Application.Current.MainPage.Navigation;
        //                foreach (var item in _navigation.NavigationStack)
        //                {
        //                    if (item.GetType().Name == App.SFLoginPageView)
        //                    {
        //                        _navigation.RemovePage(item);
        //                        break;
        //                    }
        //                }
        //                // _navigationService.NavigateTo(App.SFLoginPageView);
        //                _navigationService.NavigateTo(App.SFLoginPageView, App.GAZTNewDesignDashBoardPageView);
        //                _navigation.NavigationStack.ToList().Clear();
        //            });
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

    }
}
