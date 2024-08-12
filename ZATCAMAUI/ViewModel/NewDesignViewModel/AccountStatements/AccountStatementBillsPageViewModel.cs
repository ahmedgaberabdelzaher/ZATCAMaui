using Mopups.Services;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.AccountDetails;
using ZATCAMAUI.Models.AccountStatements;
using ZATCAMAUI.Models.SyncfusionEnabledModels;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Views.NewDesign.GenericPickers;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.AccountStatements
{

    public class AccountStatementBillsPageViewModel : BaseViewModel
    {
        #region commands
        public ICommand GoBackBtnTapped { get; set; }
        public ICommand FilterCloseClick { get; set; }
        public ICommand FilterBtnCommand { get; set; }
        public ICommand FiltersTapped { get; set; }
        public ICommand ClickOnSort { get; set; }

        #endregion
        public static string currentSortType = "default";

        private bool calculateMyBills = false;
        public string FromStatus = "";
        public string FromTxAmount = "";
        public string ToTxAmount = "";
        public bool isTxStartDate = true;
        public bool isTaxPeriodStartDate = true;

        public Color _SelectionColor = Colors.Transparent;
        public Color SelectionColor
        {
            get
            {
                return _SelectionColor;
            }
            set
            {
                if (_SelectionColor == value) return;

                _SelectionColor = value;

                OnPropertyChanged("SelectionColor");
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
                FilterIfTypeAndStausFilterSelected(true);
                OnPropertyChanged("SelectedTransactionTypeFilter");

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
                OnPropertyChanged("TransactionTypeFilter");
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
                OnPropertyChanged("PickerModel");
            }
        }

        public ObservableCollection<ChipModel> _chipDataFilterlistForStatus = null;
        public ObservableCollection<ChipModel> ChipDataFilterlistForStatus
        {
            get
            {
                return _chipDataFilterlistForStatus;
            }
            set
            {
                if (_chipDataFilterlistForStatus == value) return;

                _chipDataFilterlistForStatus = value;
                OnPropertyChanged("ChipDataFilterlistForStatus");
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
                OnPropertyChanged("IsSearchButtonVisible");
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
                OnPropertyChanged("IsCloseButtonVisible");
            }
        }

        private string _amountLabel = " - ";
        public string AmountLabel
        {
            get
            {
                return _amountLabel;
            }
            set
            {
                if (_amountLabel == value) return;

                _amountLabel = value;


                OnPropertyChanged("AmountLabel");
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
                if (_isNoDataLableVisible == value) return;

                _isNoDataLableVisible = value;
                OnPropertyChanged("isNoDataLableVisible");
            }
        }
        private bool _isListVisible = false;
        public bool IsListVisible
        {
            get
            {
                return _isListVisible;
            }
            set
            {
                if (_isListVisible == value) return;

                _isListVisible = value;
                OnPropertyChanged("IsListVisible");
            }
        }


        private ObservableCollection<MyBills> _myBillsOriginal;
        public ObservableCollection<MyBills> MyBillsOriginal
        {
            get
            {
                return _myBillsOriginal;
            }
            set
            {
                if (_myBillsOriginal == value) return;

                _myBillsOriginal = value;
                OnPropertyChanged("MyBillsOriginal");
            }
        }

        private int _selcectedBillsIndex = 0;
        public int SelcectedBillsIndex
        {
            get
            {
                return _selcectedBillsIndex;
            }
            set
            {
                if (_selcectedBillsIndex == value) return;

                _selcectedBillsIndex = value;
                OnPropertyChanged("SelcectedBillsIndex");
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
                    //FilterIfTypeAndStausFilterSelected(true);
                }
                OnPropertyChanged("SelectedTaxTypeForFilter");
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

                OnPropertyChanged("FilterLabelText");
            }
        }

        
        public string _searchText = "";
        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                if (_searchText == value) return;

                _searchText = value;

                OnPropertyChanged("SearchText");
            }
        }



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
                OnPropertyChanged("TodayDateNormal");
            }
        }

        private bool _isFromFilter = false;
        public bool isFromFilter
        {
            get
            {
                return _isFromFilter;
            }
            set
            {
                if (_isFromFilter == value) return;

                _isFromFilter = value;
                OnPropertyChanged("isFromFilter");
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
                OnPropertyChanged("TodayDateinHijri");
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
                OnPropertyChanged("IsHijriCal");
            }
        }

        private String Opbel = "";

        private string _txFromDate = "";
        public string TxFromDate
        {
            get
            {
                return _txFromDate;
            }
            set
            {
                if (_txFromDate == value) return;

                _txFromDate = value;
                OnPropertyChanged("TxFromDate");
            }
        }


        private string _txToDate = "";
        public string TxToDate
        {
            get
            {
                return _txToDate;
            }
            set
            {
                if (_txToDate == value) return;

                _txToDate = value;
                OnPropertyChanged("TxToDate");
            }
        }


        private string _tPFromDate = "";
        public string TPFromDate
        {
            get
            {
                return _tPFromDate;
            }
            set
            {
                if (_tPFromDate == value) return;

                _tPFromDate = value;
                OnPropertyChanged("TPFromDate");
            }
        }
        private string _tPToDate = "";
        public string TPToDate
        {
            get
            {
                return _tPToDate;
            }
            set
            {
                if (_tPToDate == value) return;

                _tPToDate = value;
                OnPropertyChanged("TPToDate");
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
                OnPropertyChanged("TransactionTypeDropDownParent");
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
                OnPropertyChanged("TaxTypeForFilter");
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
                OnPropertyChanged("AllTransactionFilters");
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

                OnPropertyChanged("HeaderSet");
            }
        }
        private ObservableCollection<MyBills> _myBills;
        public ObservableCollection<MyBills> MyBills
        {
            get
            {
                return _myBills;
            }
            set
            {
                if (_myBills == value) return;

                _myBills = value;
                if (_myBills != null)
                {

                    //if (_myBills.Count != 0)
                    //{
                    double Amount = 0.00;
                    foreach (var item in MyBills)
                    {
                        //P = 0 - Paid
                        //I = 1 - Partially Paid
                        //O = 2 - Unpaid

                        if (item.Status == "Open")
                        {
                            if (item.TestDueAmount != null)
                            {
                                Amount = Amount + Convert.ToDouble(item.TestDueAmount);
                            }
                            item.StatusTextColor = (Color)App.Current.Resources["Error"];
                            item.StatusBackGColor = (Color)App.Current.Resources["ErrorBg"];
                        }
                        else if (item.Status == "Paid")
                        {
                            if (item.TestDueAmount != null)
                            {
                               // Amount = Amount + Convert.ToDouble(item.TestDueAmount);
                               
                            }
                            item.StatusTextColor = (Color)App.Current.Resources["Success"];
                            item.StatusBackGColor = (Color)App.Current.Resources["SuccessBg"];
                        }
                        else if (item.Status == "Partially Paid")
                        {
                            if (item.TotalRemainingAmount != null && item.TotalRemainingAmount != string.Empty)
                            {
                                // Amount = Amount + Convert.ToDouble(item.TotalRemainingAmount);
                                Amount = Amount +  (Convert.ToDouble(item.BETRW) - Convert.ToDouble(item.Paidamt));
                            }
                            item.StatusTextColor = (Color)App.Current.Resources["Partial"];
                            item.StatusBackGColor = (Color)App.Current.Resources["PartialBg"];
                        }
                        else
                        {
                            if (item.TotalRemainingAmount != null && item.TotalRemainingAmount != string.Empty)
                            {
                                Amount = Amount + Convert.ToDouble(item.TotalRemainingAmount);
                            }
                            item.StatusTextColor = (Color)App.Current.Resources["color"];
                            item.StatusBackGColor = (Color)App.Current.Resources["gray"];

                            
                        }
                    }

                    string format = "$#,##0.00;-$#,##0.00;Zero";
                    decimal d = Convert.ToDecimal(Amount.ToString());
                    decimal positiveMoney = d;
                    positiveMoney.ToString(format);  //will return $24,508,975.94
                    string TestDueAmount = UtilityManager.GetCommaSeparatedAmount(positiveMoney.ToString());


                    AmountLabel = TestDueAmount + " " + AppResources.ZSAR;


                    //}

                    if (Amount == 0.00)
                    {
                        isNoDataLableVisible = false;
                    }
                    else
                    {
                        isNoDataLableVisible = true;
                    }

                }
                OnPropertyChanged("MyBills");
            }
        }

        private ObservableCollection<AccountStatus> _myBillsSTATUS;
        public ObservableCollection<AccountStatus> MyBillsSTATUS
        {
            get
            {
                return _myBillsSTATUS;
            }
            set
            {
                if (_myBillsSTATUS == value) return;
                _myBillsSTATUS = value;
                OnPropertyChanged("MyBillsSTATUS");
            }
        }

        public void FiltersClicked()
        {
            _navigationService.NavigateTo(App.AccountStatementsNewFilterPageView);
            /*IsSortByVisible = !IsSortByVisible;*/
        }

        public void ClickSorted(string type1)
        {
            //currentSortType += 1;
            
            var sortedBills = new ObservableCollection<MyBills>();
            if (type1 == AppResources.SortDefault)
            {
                type1 = AppResources.SortAcending;
                sortedBills = new ObservableCollection<MyBills>(MyBillsOriginal.ToList()); 
            }
            else if(type1 == AppResources.SortAcending)
            {
                type1 = AppResources.SortDecending;
                sortedBills = new ObservableCollection<MyBills>(MyBills.OrderBy(temp => float.Parse(temp.TestDueAmount)).ToList());  
            }
            else if(type1 == AppResources.SortDecending)
            {
                type1 = AppResources.SortDefault;
                sortedBills = new ObservableCollection<MyBills>(MyBills.OrderByDescending(temp => float.Parse(temp.TestDueAmount)).ToList());
                FromStatus = AppResources.All;
            }
                
            FilterOnTaxType(sortedBills);
        }

        private AccoungtDetails _AccDertails = null;
        public AccoungtDetails accoungtDetails1
        {
            get
            {
                return _AccDertails;
            }
            set
            {
                if (_AccDertails == value) return;
                _AccDertails = value;
               RaisePropertyChanged("accoungtDetails1");
            }

        }

        public AccoungtDetails ObjectBills(string Opbel, string fbnum)
        {
            string lang = UtilityManager.GetLanguageParameter();
            return WebServiceManager.ZATCAAccGetDetails(Opbel, fbnum, lang, "AccountStatements");
        }


        public async Task onPageLoad(BillInfo billInfo)
        {
            IsLoading = true;
            MyBills = null;

            try
            {
                try
                {
                    string lang = UtilityManager.GetLanguageParameter();
                    var tell = await WebServiceManager.GetUserBills(App.TP.TIN, lang);
                    MyBills = await WebServiceManager.GAZTGetMyBills(App.TP.TIN, lang, "AccountStatements");
                    MyBillsSTATUS = await WebServiceManager.GAZTGETBillsSTS(App.TP.TIN, lang, "AccountStatements");
                    //PopToRootPage();// If seesion Expired it will navigate to Dashboard page

                    if (MyBills != null && MyBills.Count != 0)
                    {

                        var sortedBills = new ObservableCollection<MyBills>(MyBills.OrderByDescending(temp => temp.Faedn).ToList());
                        MyBillsOriginal = sortedBills;

                        SelcectedBillsIndex = 0;

                        if (billInfo != null)
                        {
                            if (billInfo.BillTypeName == AppResources.Paid)
                            {
                                SelcectedBillsIndex = 1;
                            }
                            else if (billInfo.BillTypeName == AppResources.UnPaid)
                            {
                                SelcectedBillsIndex = 2;
                            }
                            else if (billInfo.BillTypeName == AppResources.Partial)
                            {
                                SelcectedBillsIndex = 3;
                            }
                        }

                        foreach (MyBills myBills in MyBills)
                        {
                            if (myBills.Period.Contains("000000") || myBills.PeriodPart1.Contains("000000") || myBills.PeriodPart2.Contains("000000"))
                            {
                                myBills.IsPeriodVisible = false;
                            }
                            else
                            {
                                myBills.IsPeriodVisible = true;
                            }

                            if (myBills.Status == "Partially Paid")
                            {
                                if (string.IsNullOrEmpty(myBills.Paidamt))
                                {
                                    myBills.Paidamt = "0";
                                }

                                if (!string.IsNullOrEmpty(myBills.BETRW) && !string.IsNullOrEmpty(myBills.Paidamt))
                                {
                                    myBills.TotalRemainingAmount = (Convert.ToDouble(myBills.BETRW) - Convert.ToDouble(myBills.Paidamt)).ToString();
                                    string format = "$#,##0.00;-$#,##0.00;Zero";
                                    decimal dRem = Convert.ToDecimal(myBills.TotalRemainingAmount);
                                    decimal positiveMoneyRem = dRem;
                                    positiveMoneyRem.ToString(format);  //will return $24,508,975.94
                                    myBills.TotalRemainingAmount = UtilityManager.GetCommaSeparatedAmount(positiveMoneyRem.ToString());
                                }
                            }
                        }

                    }
                    else
                    {
                        isNoDataLableVisible = true;
                    }
                }
                catch (Exception e)
                {

                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(e.Message));
                        _navigationService.GoBack();
                    });
                    IsLoading = false;
                }
            }
            catch (InternetException ex)
            {

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                    _navigationService.GoBack();
                });
                IsLoading = false;
            }
            IsLoading = false;
        }


        public async Task PopulateDataForTransactionTypes(string taxType)
        {
            IsLoading = true;
            var tempValues = await WebServiceManager.GAZTGetAccountStatementsRevenueDropDownSet(taxType);
            foreach (ASRevenueDropDownSetDataResults aSRevenueDropDownSetDataResults in tempValues.d)
            {
                aSRevenueDropDownSetDataResults.TaxType = taxType;
                AllTransactionFilters.Add(aSRevenueDropDownSetDataResults);

            }
        }


        public async Task PopulateReturnTypeList()
        {
            try
            {
                IsLoading = true;
                TabIdentification = await WebServiceManager.GAZTGetAccountStatementsTabIdentification();
                TaxTypeForFilter = new ObservableCollection<ASReturnTypes>();

               
                var tempDirectTax = new ASReturnTypes { Id = "D", TaxType = AppResources.ASAccountStatementDirectTax };
                var tempInDirectTax = new ASReturnTypes { Id = "I", TaxType = AppResources.ASAccountStatementInDirectTax };

                if (TabIdentification.d.Direct == "X")
                {
                    TaxTypeForFilter.Add(tempDirectTax);
                }

                if (TabIdentification.d.Indirect == "X")
                {
                    TaxTypeForFilter.Add(tempInDirectTax);
                }

               // IsLoading = false;
            }
            catch (Exception ex)
            {
                IsLoading = false;
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());


            }

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
                if (TabIdentification.d!=null && TabIdentification.d.Direct == "X")
                {
                    await PopulateDataForTransactionTypes("D");
                }

                if (TabIdentification.d != null && TabIdentification.d.Indirect == "X")
                {
                    await PopulateDataForTransactionTypes("I");
                }

                HeaderSet = await WebServiceManager.GAZTGetAccountStatementHeaderSet
                    (AllTransactionFilters.FirstOrDefault().StatementFilter, string.Empty, AllTransactionFilters.FirstOrDefault().TaxType, false);
                if (HeaderSet != null)
                {
                    foreach (ASReturnTypes aSReturnTypes in TaxTypeForFilter)
                    {
                        if (HeaderSet.d.TaxType == aSReturnTypes.Id)
                        {
                            SelectedTaxTypeForFilter = aSReturnTypes;
                        }
                        else
                        {
                            SelectedTaxTypeForFilter = TaxTypeForFilter.FirstOrDefault();
                        }
                    }



                    foreach (TaxRelationSetResult taxRelationSetResult in HeaderSet.d.TaxRelationSet)
                    {
                        if (TabIdentification.d.Direct == "X")
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
                        if (TabIdentification.d.Indirect == "X")
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
                    TransactionTypeFilter = new ObservableCollection<TaxRelationSetResult>(HeaderSet.d.TaxRelationSet.Where(temp => temp.DisplayId == 01 || temp.DisplayId == 02 || temp.DisplayId == 03 || temp.DisplayId == 04 || temp.DisplayId == 06 || temp.DisplayId == 07).ToList());


                    SelectedTransactionTypeFilter = TransactionTypeFilter.FirstOrDefault();


                    var list = new List<string>();

                    foreach (TaxRelationSetResult dropdown in TransactionTypeFilter)
                    {
                        try
                        {
                            list.Add(dropdown.Txt30.ToUpper());
                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine(ex.Message);
                            Console.Write(ex.ToString());
                            Console.Write(ex.StackTrace.ToString());
                        }


                    }


                    GenericPickerModel genericPickerModel = new GenericPickerModel();
                    genericPickerModel.PickerData = list;
                    genericPickerModel.PickerTitle = "";
                    genericPickerModel.PickerId = "AccountStatement";
                    genericPickerModel.SelectedValue = SelectedTransactionTypeFilter.Txt30;

                    PickerModel = genericPickerModel;
                }
                IsLoading = false;

            }
            catch (GAZTErrorException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
            }
            catch (InternetException ex)
            {
                IsLoading = false;
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
             
                _navigationService.GoBack();
                });
              
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



        public async void FilterIfTypeAndStausFilterSelected(bool isTaxTypeFilter)
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });
            if(SelectedTransactionTypeFilter != null)
            {


                if (SelectedTransactionTypeFilter.StatementFilter == "10")
                {
                    calculateMyBills = true;
                }
                else
                {
                    calculateMyBills = true;
                }
            }
            if (MyBillsOriginal != null)
            {
                try
                {
                    MyBills = new ObservableCollection<MyBills>(MyBillsOriginal);

                    if (isTaxTypeFilter)
                    {
                        MyBills = new ObservableCollection<MyBills>(MyBillsOriginal.Where(x => x.Status == "Paid" || x.Status == "Partially Paid" || x.Status == "Open").ToList());

                        FilterOnTaxType(MyBills);
                        ApplyFilter();
                        await Task.Run(() =>
                        {
                            IsLoading = false;
                        });
                        return;


                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.ToString());
                    Console.Write(ex.StackTrace.ToString());

                }


                if (isFromFilter)
                {

                    try
                    {
                        if (TxFromDate != "" && TxToDate != "")
                        {
                            var filterItems = MyBills;

                            if (IsHijriCal)
                            {
                                CultureInfo arCI = new CultureInfo("ar-SA");
                                DateTime FormatedTxFromDate = DateTime.ParseExact(TxFromDate, "yyyy/MM/dd", arCI.DateTimeFormat,
                                    DateTimeStyles.AllowInnerWhite);

                                DateTime FormatedTxToDate = DateTime.ParseExact(TxToDate, "yyyy/MM/dd", arCI.DateTimeFormat,
                                    DateTimeStyles.AllowInnerWhite);

                                MyBills = new ObservableCollection<MyBills>(filterItems.Where(p => p.faednDate>= FormatedTxFromDate && p.faednDate <= FormatedTxToDate));
                            }
                            else
                            {
                                CultureInfo arCI = new CultureInfo("en-US");
                                DateTime FormatedTxFromDate = DateTime.ParseExact(TxFromDate, "yyyy/MM/dd", arCI.DateTimeFormat,
                                    DateTimeStyles.AllowInnerWhite);
                                DateTime FormatedTxToDate = DateTime.ParseExact(TxToDate, "yyyy/MM/dd", arCI.DateTimeFormat,
                                    DateTimeStyles.AllowInnerWhite);
                                MyBills = new ObservableCollection<MyBills>(filterItems.Where(p => p.faednDate >= FormatedTxFromDate && p.faednDate <= FormatedTxToDate));
                            }

                            FilterOnTaxType(MyBills);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.Write(ex.ToString());
                        Console.Write(ex.StackTrace.ToString());

                    }


                    try
                    {



                        if (TPFromDate != "" && TPToDate != "")
                        {
                            var filterItems = MyBills;


                            if (IsHijriCal)
                            {
                                CultureInfo arCI = new CultureInfo("ar-SA");
                                DateTime FormatedTpFromDate = DateTime.ParseExact(TPFromDate, "yyyy/MM/dd", arCI.DateTimeFormat,
                                    DateTimeStyles.AllowInnerWhite);
                                DateTime FormatedTpToDate = DateTime.ParseExact(TPToDate, "yyyy/MM/dd", arCI.DateTimeFormat,
                                    DateTimeStyles.AllowInnerWhite);

                                MyBills = new ObservableCollection<MyBills>(filterItems.Where(p => UtilityManager.ConvertDateStringtoDateTime(p.PeriodPart1, "yyyy/MM/dd", arCI) >= FormatedTpFromDate && UtilityManager.ConvertDateStringtoDateTime(p.PeriodPart2, "yyyy/MM/dd", arCI) <= FormatedTpToDate));
                               }
                            else
                            {
                                CultureInfo enCI = new CultureInfo("en-US");
                                DateTime FormatedTpFromDate = DateTime.ParseExact(TPFromDate, "dd/MM/yyyy", enCI.DateTimeFormat,
                                    DateTimeStyles.AllowInnerWhite);
                                DateTime FormatedTpToDate = DateTime.ParseExact(TPToDate, "dd/MM/yyyy", enCI.DateTimeFormat,
                                    DateTimeStyles.AllowInnerWhite);
                                MyBills = new ObservableCollection<MyBills>(filterItems.Where(p => UtilityManager.ConvertDateStringtoDateTime(p.PeriodPart1, "dd/MM/yyyy", enCI) >= FormatedTpFromDate && UtilityManager.ConvertDateStringtoDateTime(p.PeriodPart2, "dd/MM/yyyy", enCI) <= FormatedTpToDate));
                            }

                            FilterOnTaxType(MyBills);


                        }
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);
                        Console.Write(ex.ToString());
                        Console.Write(ex.StackTrace.ToString());

                    }




                    if (FromTxAmount != "" && ToTxAmount != "")
                    {
                        var filterItems = MyBills;
                        MyBills = new ObservableCollection<MyBills>(filterItems.Where(p => Convert.ToDouble(p.BETRW) >= Convert.ToDouble(FromTxAmount) && Convert.ToDouble(p.BETRW) <= Convert.ToDouble(ToTxAmount)));

                        FilterOnTaxType(MyBills);

                    }


                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });
                    if (string.IsNullOrEmpty(TxFromDate) && string.IsNullOrEmpty(TxToDate) && string.IsNullOrEmpty(TPFromDate) && string.IsNullOrEmpty(TPToDate) && string.IsNullOrEmpty(FromTxAmount) && string.IsNullOrEmpty(ToTxAmount))
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.AccountStatementsEnterAmount));

                        return;
                    }
                    else if (string.IsNullOrEmpty(FromTxAmount) && !string.IsNullOrEmpty(ToTxAmount) || !string.IsNullOrEmpty(FromTxAmount) && string.IsNullOrEmpty(ToTxAmount))
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.AccountStatementsEnterAmount));
                        return;
                    }
                    else if (!string.IsNullOrEmpty(TxFromDate) && string.IsNullOrEmpty(TxToDate) || string.IsNullOrEmpty(TxFromDate) && !string.IsNullOrEmpty(TxToDate))
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.AccountStatementsTransactionDate));
                        return;
                    }
                    else if (!string.IsNullOrEmpty(TPFromDate) && string.IsNullOrEmpty(TPToDate) || string.IsNullOrEmpty(TPFromDate) && !string.IsNullOrEmpty(TPToDate))
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.AccountStatementsTaxPeriod));
                        return;
                    }
                    isFromFilter = false;
                    _navigationService.GoBack();
                }
                else
                {
                    if (FromStatus != "")
                    {
                        if (FromStatus.Equals(AppResources.PAIDPR))
                        {
                            MyBills = new ObservableCollection<MyBills>(MyBillsOriginal.Where(x => x.Status == "Paid").ToList());
                        }

                        if (FromStatus.Equals(AppResources.PARPAIDPR))
                        {
                            MyBills = new ObservableCollection<MyBills>(MyBillsOriginal.Where(x => x.Status == "Partially Paid").ToList());
                        }

                        if (FromStatus.Equals(AppResources.UNPAIDPR))
                        {
                            MyBills = new ObservableCollection<MyBills>(MyBillsOriginal.Where(x => x.Status == "Open").ToList());
                        }
                        if (FromStatus.Equals(AppResources.TRANSFEREDPR))
                        {
                            MyBills = new ObservableCollection<MyBills>(MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 3)).ToList());
                        }
                        if (FromStatus.Equals(AppResources.REVERSEPR))
                        {
                            MyBills = new ObservableCollection<MyBills>(MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 4)).ToList());
                        }
                        if (FromStatus.Equals(AppResources.All))
                        {
                            MyBills = new ObservableCollection<MyBills>(MyBillsOriginal.Where(x => x.Status == "Paid" || x.Status == "Partially Paid" || x.Status == "Open" || x.Status == Enum.GetName(typeof(BillStatus), 3) || x.Status == Enum.GetName(typeof(BillStatus), 4)).ToList());
                        }

                        FilterOnTaxType(MyBills);

                        await Task.Run(() =>
                        {
                            IsLoading = false;
                        });

                        return;
                    }
                    else if (FromStatus == "")
                    {
                        if (SearchText != "")
                        {
                            // suggestion ;

                            try
                            {
                                //MyBills = new ObservableCollection<MyBills>(MyBillsOriginal.Where<MyBills>(c => c.VTRE2.ToLower() == (SearchText.ToLower()) || c.Fbnum.ToLower() == (SearchText.ToLower())|| c.Status.ToLower().Contains(SearchText.ToLower()) || c.StatusText.ToLower().Contains(SearchText.ToLower())|| c.BETRW.ToLower().Contains(SearchText.ToLower()) || c.TestDueAmount.ToLower().Contains(SearchText.ToLower())|| c.Txt30.ToLower().Contains(SearchText.ToLower()) || c.Abtypt.ToLower().Contains(SearchText.ToLower())).ToList());

                               var suggest = MyBillsOriginal.Where<MyBills>(c => c.VTRE2.ToLower().Contains(SearchText.ToLower()) || c.Fbnum.ToLower().Contains(SearchText.ToLower())).ToList();

                                MyBills = new ObservableCollection<MyBills>(suggest);
                                FilterOnTaxType(MyBills);
                            }
                            catch (Exception ex)
                            {

                            }
                            
                                

                           
                        }
                        else {

                            MyBills = new ObservableCollection<MyBills>(MyBillsOriginal.Where(x => x.Status == "Paid" || x.Status == "Partially Paid" || x.Status == "Open" || x.Status == Enum.GetName(typeof(BillStatus), 3)|| x.Status == Enum.GetName(typeof(BillStatus), 4)).ToList());

                            FilterOnTaxType(MyBills);

                            await Task.Run(() =>
                            {
                                IsLoading = false;
                            });

                            return;

                        }


                    }

                    

                    isFromFilter = false;

                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });
                }

            }
            else
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }

        }

        public async void FilterOnTaxType(ObservableCollection<MyBills> BillsToProcss)
        {

            await Task.Run(() =>
            {
                IsLoading = true;
            });

            if(SelectedTransactionTypeFilter != null) {

                try{

                    switch (SelectedTransactionTypeFilter.StatementFilter)
                    {
                        case "10":
                            MyBills = new ObservableCollection<MyBills>(BillsToProcss);
                            break;
                        case "01":
                            if (App.IsArabic)
                            {
                                MyBills = new ObservableCollection<MyBills>(BillsToProcss.Where(x => x.Abtypt.Equals("الزكاة") || x.Abtypt.Equals("الزكاة")).ToList());
                            }
                            else
                            {
                                MyBills = new ObservableCollection<MyBills>(BillsToProcss.Where(x => x.Abtypt.Equals("Zakat") || x.Abtypt.Equals("Voluntary Zakat")).ToList());
                            }
                            break;
                        case "06":
                            if (App.IsArabic)
                            {
                                MyBills = new ObservableCollection<MyBills>(BillsToProcss.Where(x => x.Abtypt.Equals("ضريبة القيمة المضافة") || x.Abtypt.Equals("ضريبة القيمة المضافة")).ToList());
                            }
                            else
                            {
                                MyBills = new ObservableCollection<MyBills>(BillsToProcss.Where(x => x.Abtypt.Equals("VAT") || x.Abtypt.Equals("VAT Eligible Person")).ToList());
                            }
                            break;
                        case "07":
                            if (!App.IsArabic)
                            {
                                MyBills = new ObservableCollection<MyBills>(BillsToProcss.Where(x => x.Abtypt.Equals("Excise Tax") || x.Abtypt.Equals("ETAX")).ToList());
                            }
                            else
                            {
                                MyBills = new ObservableCollection<MyBills>(BillsToProcss.Where(x => x.Abtypt.Equals("ضريبة السلع الانتقائية")).ToList());
                            }
                            break;
                        case "03":
                            if (!App.IsArabic)
                            {
                                MyBills = new ObservableCollection<MyBills>(BillsToProcss.Where(x => x.Abtypt.Equals("Withholding Tax")).ToList());
                            }
                            else
                            {
                                MyBills = new ObservableCollection<MyBills>(BillsToProcss.Where(x => x.Abtypt.Equals("ضريبة الاستقطاع") || x.Abtypt.Equals("Withholding Tax")).ToList());
                            }
                            break;
                        case "02":
                            if (!App.IsArabic)
                            {
                                MyBills = new ObservableCollection<MyBills>(BillsToProcss.Where(x => x.Abtypt.Equals("Income Tax")).ToList());
                            }
                            else
                            {
                                MyBills = new ObservableCollection<MyBills>(BillsToProcss.Where(x => x.Abtypt.Equals("ضريبة الدخل")).ToList());
                            }
                            break;
                    }
                }
                catch (Exception ex) {

                    Console.WriteLine(ex.Message);
                    Console.Write(ex.ToString());
                    Console.Write(ex.StackTrace.ToString());

                }
            }

            if (MyBills != null)
            {

                if (MyBills.Count != 0)
                {

                    IsListVisible = true;
                    isNoDataLableVisible = false;
                }
                else
                {
                    IsListVisible = false;
                    isNoDataLableVisible = true;
                }

            }

            await Task.Run(() =>
            {
                IsLoading = false;
            });
        }

        public void ApplyFilter()
        {

            //if (FromStatus != "")
            //{
                FilterIfTypeAndStausFilterSelected(false);
            //}

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
                if (PickerModel != null)
                    await MopupService.Instance.PushAsync(new PickerPageView(PickerModel));
            }
            catch (GAZTUnlockAccountException)
            {
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }

        public void populateStatusChips()
        {
            var statusList = new ObservableCollection<ChipModel>();
            for (int i =0;i<MyBillsSTATUS.Count; i++)
            {
                var chipmodel = new ChipModel();
            //     < Color x: Key = "SuccessBg" >#1A008000</Color>
            //< Color x: Key = "PartialBg" >#1A0996d4</Color>
            //< Color x: Key = "ErrorBg" >#1AAA0C19</Color>

                if (MyBillsSTATUS[i].ZtpaccSts == "PD")
                {
                    chipmodel = new ChipModel { TemplateType = "#1A008000",
                        Text = MyBillsSTATUS[i].PymtStatus,
                        ImageSource = null,
                        ZTSTScts = MyBillsSTATUS[i].ZtpaccSts,
                        TextColor = (Color)App.Current.Resources["Success"] };
                }
                if (MyBillsSTATUS[i].ZtpaccSts == "PP")
                {
                    chipmodel = new ChipModel { TemplateType = "#1A0996d4",
                        Text = MyBillsSTATUS[i].PymtStatus,
                        ImageSource = null,
                        ZTSTScts = MyBillsSTATUS[i].ZtpaccSts,
                        TextColor = (Color)App.Current.Resources["Partial"] };
                }
                if (MyBillsSTATUS[i].ZtpaccSts == "RV")
                {
                    chipmodel = new ChipModel
                    {
                        TemplateType = "#cccccc",
                        Text = MyBillsSTATUS[i].PymtStatus,
                        ImageSource = null,
                        TextColor = (Color)App.Current.Resources["color"],
                        ZTSTScts = MyBillsSTATUS[i].ZtpaccSts
                    };
                 }
                if (MyBillsSTATUS[i].ZtpaccSts == "TF")
                {
                    chipmodel = new ChipModel
                    {
                        TemplateType = "#CCCCCC",
                        Text = MyBillsSTATUS[i].PymtStatus,
                        ImageSource = null,
                        TextColor = (Color)App.Current.Resources["color"],
                        ZTSTScts = MyBillsSTATUS[i].ZtpaccSts
                    };
                 }
                if (MyBillsSTATUS[i].ZtpaccSts == "UP")
                {
                    chipmodel = new ChipModel { TemplateType = "#1AAA0C19",
                        Text = MyBillsSTATUS[i].PymtStatus,
                        ImageSource = null,
                        TextColor = (Color)App.Current.Resources["Error"],
                        ZTSTScts = MyBillsSTATUS[i].ZtpaccSts };
                }
                statusList.Add(chipmodel);
            }
            ChipDataFilterlistForStatus = statusList;

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

        }


       // ChipDataFilterlistForStatus
        public AccountStatementBillsPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

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

                if (string.IsNullOrEmpty(TxFromDate) && string.IsNullOrEmpty(TxToDate) && string.IsNullOrEmpty(TPFromDate) && string.IsNullOrEmpty(TPToDate) && string.IsNullOrEmpty(FromTxAmount) && string.IsNullOrEmpty(ToTxAmount))
                {

                    MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.AcFilterEmptyState));

                }
                else
                {

                    isFromFilter = true;
                    FilterIfTypeAndStausFilterSelected(false);
                }


            

            });
            FiltersTapped = new Command(() =>
            {
                FiltersClicked();
            });
            
            //ClickOnSort = new Command(() =>
            //{
            //    ClickSorted();
            //});
        }

    }
}