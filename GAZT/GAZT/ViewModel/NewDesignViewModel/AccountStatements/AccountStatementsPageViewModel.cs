using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models.AccountStatements;
using EGAZT.Views.NewDesign.AccountStatements;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
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

        public ASRevenueDropDownSet _transactionTypeDropDownParent = null;
        public ASRevenueDropDownSet TransactionTypeDropDownParent
        {
            get
            {
                return _transactionTypeDropDownParent;
            }
            set
            {
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
                _TaxperiodFilterItem = value;
                RaisePropertyChanged("TaxperiodFilterItem");
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
                _StatusFilterItem = value;
                RaisePropertyChanged("StatusFilterItem");
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
                _TotalBalance = value;
                RaisePropertyChanged("TotalBalance");
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
                _statementsLineItems = value;
                RaisePropertyChanged("StatementsLineItems");
            }
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
                _allTransactionFilters = value;
                RaisePropertyChanged("AllTransactionFilters");
            }
        }

        public ObservableCollection<ASRevenueDropDownSetDataResults> _transactionTypeFilter = null;
        public ObservableCollection<ASRevenueDropDownSetDataResults> TransactionTypeFilter
        {
            get
            {
                return _transactionTypeFilter;
            }
            set
            {
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
                _chipDataFilterlist = value;
                RaisePropertyChanged("ChipDataFilterlist");
            }
        }

        public ObservableCollection<ASChipModel> _chipDataFilterlistForYears = null;
        public ObservableCollection<ASChipModel> ChipDataFilterlistForYears
        {
            get
            {
                return _chipDataFilterlistForYears;
            }
            set
            {
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
                _SelectedTaxTypeForFilter = value;

                if (_SelectedTaxTypeForFilter != null)
                {
                    FilterLabelText = _SelectedTaxTypeForFilter.TaxType;
                    FilterOnTaxType(SelectedTaxTypeForFilter.Id);
                }

                RaisePropertyChanged("SelectedTaxTypeForFilter");
            }
        }

        public ASRevenueDropDownSetDataResults _selectedTransactionTypeFilter = null;
        public ASRevenueDropDownSetDataResults SelectedTransactionTypeFilter
        {
            get
            {
                return _selectedTransactionTypeFilter;
            }
            set
            {
                _selectedTransactionTypeFilter = value;

                if(_selectedTransactionTypeFilter.StatementFilter != null)
                {
                    IsYearsChipVisible = true;
                    
                }
                else
                {
                    IsYearsChipVisible = false;
                }

                RaisePropertyChanged("SelectedTransactionTypeFilter");
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
                _selectedTransactionType = value;
                
                RaisePropertyChanged("SelectedTransactionType");
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
                _filterLabelText = value;

                RaisePropertyChanged("FilterLabelText");
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

            GoBackBtnTapped = new Command(async () =>
            {
                _navigationService.GoBack();
            });

            DownloadBtnTapped = new Command(DownloadBtnClicked);

            TransactionTypeFilter = new ObservableCollection<ASRevenueDropDownSetDataResults>();
            IsSortByVisible = false;
            FiltersTapped = new Command(FiltersClicked);
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

                    _navigationService.NavigateTo(App.AccountStatementsDownloadPageView, aSTaxpayerSelectedValues);
                }
                else
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.PdfIsNoteAvailable));
                    });
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async void PopulateReturnTypeList()
        {
            try
            {
                
                TabIdentification = await WebServiceManager.GAZTGetAccountStatementsTabIdentification();
                TaxTypeForFilter = new ObservableCollection<ASReturnTypes>();
                
                if(StatementsLineItems == null)
                {
                    StatementsLineItems = new ObservableCollection<ASResult>();
                }

                StatementsLineItems.Clear();

                var tempDirectTax = new ASReturnTypes { Id = "D", TaxType = AppResources.ASAccountStatementDirectTax };
                var tempInDirectTax = new ASReturnTypes { Id = "I", TaxType = AppResources.ASAccountStatementInDirectTax };

                if(TabIdentification.D.Direct == "X")
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

            }
        }

        public  async void FilterOnTaxType(string taxType)
        {
            //API Call
            try
            {
                string statementFilter = string.Empty;
                if (taxType == "D")
                {
                    statementFilter = "04";
                }
              if(taxType=="I")
                {
                    statementFilter = "08";
                }
                    
                HeaderSet = await WebServiceManager.GAZTGetAccountStatementHeaderSet(statementFilter, string.Empty, taxType);
                //if (HeaderSet != null && HeaderSet.D != null)
                //{
                //    TotalDebit = HeaderSet.D.DebitAmount;
                //    TotalCredit = HeaderSet.D.CreditAmount;
                //    TotalBalance = HeaderSet.D.CloseAmount;

                //}

            }
            catch(Exception)
            {

            }

            TransactionTypeFilter = new ObservableCollection<ASRevenueDropDownSetDataResults>(AllTransactionFilters.Where(x => x.TaxType.Equals(taxType)).ToList());
            SelectedTransactionTypeFilter = TransactionTypeFilter.FirstOrDefault();
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

        public async Task PopulateDataInChipsForYears(string taxType, string statementFilter)
        {
            YearValuesHeader = await WebServiceManager.GAZTGetAccountStatementYearValuesHeaderSet(statementFilter, taxType);

            ChipDataFilterlistForYears = new ObservableCollection<ASChipModel>();

            if(YearValuesHeader != null && YearValuesHeader.D != null)
            {
                foreach (ASYearValuesResults aSYearValuesResults in YearValuesHeader.D.Results)
                {
                    ChipDataFilterlistForYears.Add(new ASChipModel() { Text = aSYearValuesResults.Persl, TemplateType = AppResources.Paid });
                }
            }
            else
            {
                ChipDataFilterlistForYears.Add(new ASChipModel() { Text = "2015", TemplateType = AppResources.Paid });
                ChipDataFilterlistForYears.Add(new ASChipModel() { Text = "2016", TemplateType = AppResources.Paid });
                ChipDataFilterlistForYears.Add(new ASChipModel() { Text = "2017", TemplateType = AppResources.Paid });
                ChipDataFilterlistForYears.Add(new ASChipModel() { Text = "2018", TemplateType = AppResources.Paid });
                ChipDataFilterlistForYears.Add(new ASChipModel() { Text = "2019", TemplateType = AppResources.Paid });
            }
        }

        public async void PopulateASFilterData()
        {
            try
            {
                TransactionTypeDropDownParent = new ASRevenueDropDownSet();

                HeaderSet = await WebServiceManager.GAZTGetAccountStatementHeaderSet(string.Empty,string.Empty, string.Empty);
             

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

                        if (HeaderSet.D.TaxType == "D")
                        {
                            FilterOnTaxType("D");
                        }
                        else if (HeaderSet.D.TaxType == "I")
                        {
                            FilterOnTaxType("I");
                        }
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
                //await Task.Run(() =>
                //{
                //    IsLoading = false;
                //});

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
            catch (Exception ex)
            {
                //await Task.Run(() =>
                //{
                //    IsLoading = false;
                //    Console.WriteLine(ex.Message);
                //});
            }
        }

        public async void PopulateStatements(string taxType, string statementFilter, string year)
        {
            try
            {
                HeaderSet = await WebServiceManager.GAZTGetAccountStatementHeaderSet(statementFilter, year, taxType);

                if(HeaderSet.D.StatmenetLineItemsSet != null)
                {
                    if(HeaderSet.D.StatmenetLineItemsSet.Results.Count() > 0)
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
                    }
                }
                else
                {
                
                    IsDownloadBtnVisile = false;
                    IsNoStatementsAvaiableVisible = true;
                }
                //if (HeaderSet != null && HeaderSet.D != null)
                //{
                //    TotalDebit = HeaderSet.D.DebitAmount;
                //    TotalCredit = HeaderSet.D.CreditAmount;
                //    TotalBalance = HeaderSet.D.CloseAmount;
                //}
            }
            catch(Exception ex)
            {

            }
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
