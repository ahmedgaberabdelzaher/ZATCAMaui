using GalaSoft.MvvmLight.Views;
using RGPopup.Maui.Services;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
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

        #endregion

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

                RaisePropertyChanged("SelectionColor");
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
                RaisePropertyChanged("SelectedTransactionTypeFilter");

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
                RaisePropertyChanged("ChipDataFilterlistForStatus");
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


                RaisePropertyChanged("AmountLabel");
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
                RaisePropertyChanged("isNoDataLableVisible");
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
                RaisePropertyChanged("IsListVisible");
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
                RaisePropertyChanged("MyBillsOriginal");
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
                RaisePropertyChanged("SelcectedBillsIndex");
            }
        }

        //public List<ReturnTypes> _TaxTypeForFilter = null;
        //public List<ReturnTypes> TaxTypeForFilter
        //{
        //    get
        //    {
        //        return _TaxTypeForFilter;
        //    }
        //    set
        //    {
        //        if (_TaxTypeForFilter == value) return;

        //        _TaxTypeForFilter = value;
        //        RaisePropertyChanged("TaxTypeForFilter");
        //    }
        //}

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
                RaisePropertyChanged("SelectedTaxTypeForFilter");
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

        public bool _IsLoading = false;
        public bool IsLoading
        {
            get
            {
                return _IsLoading;
            }
            set
            {
                if (_IsLoading == value) return;

                _IsLoading = value;

                RaisePropertyChanged("IsLoading");
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

                RaisePropertyChanged("SearchText");
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
                RaisePropertyChanged("TodayDateNormal");
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
                RaisePropertyChanged("isFromFilter");
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
                RaisePropertyChanged("TxFromDate");
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
                RaisePropertyChanged("TxToDate");
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
                RaisePropertyChanged("TPFromDate");
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
                RaisePropertyChanged("TPToDate");
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

                RaisePropertyChanged("HeaderSet");
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
                if (_myBills != null && calculateMyBills)
                {

                    //if (_myBills.Count != 0)
                    //{
                    double Amount = 0.00;
                    foreach (var item in MyBills)
                    {
                        //P = 0 - Paid
                        //I = 1 - Partially Paid
                        //O = 2 - Unpaid

                        if (item.Status == "O")
                        {
                            if (item.TestDueAmount != null)
                            {
                                Amount = Amount + Convert.ToDouble(item.TestDueAmount);
                            }
                        }
                        if (item.Status == "P")
                        {
                            if (item.TestDueAmount != null)
                            {
                                Amount = Amount + Convert.ToDouble(item.TestDueAmount);
                            }
                        }
                        else if (item.Status == "I")
                        {
                            if (item.TotalRemainingAmount != null && item.TotalRemainingAmount != string.Empty)
                            {
                                Amount = Amount + Convert.ToDouble(item.TotalRemainingAmount);
                            }
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
                RaisePropertyChanged("MyBills");
            }
        }




        public void FiltersClicked()
        {
            _navigationService.NavigateTo(App.AccountStatementsNewFilterPageView);
            /*IsSortByVisible = !IsSortByVisible;*/
        }


        public void onPageLoad(BillInfo billInfo)
        {
            IsLoading = true;
            MyBills = null;

            try
            {
                try
                {
                    string lang = UtilityManager.GetLanguageParameter();
                    MyBills = WebServiceManager.GAZTGetMyBills(App.TP.Tin, lang, "AccountStatements");
                    PopToRootPage();// If seesion Expired it will navigate to Dashboard page

                    if (MyBills != null && MyBills.Count != 0)
                    {

                        var sortedBills = new ObservableCollection<MyBills>(MyBills.OrderByDescending(temp => temp.Faedn).ToList());
                        MyBillsOriginal = sortedBills;

                        SelcectedBillsIndex = 0;

                        int milliseconds = 1000;
                        Thread.Sleep(milliseconds);

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

                            if (myBills.Status == "I")
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
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(e.Message));
                        _navigationService.GoBack();
                    });
                    IsLoading = false;
                }
            }
            catch (InternetException ex)
            {

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
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
            foreach (ASRevenueDropDownSetDataResults aSRevenueDropDownSetDataResults in tempValues.D.Results)
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

                if (TabIdentification.D.Direct == "X")
                {
                    TaxTypeForFilter.Add(tempDirectTax);
                }

                if (TabIdentification.D.Indirect == "X")
                {
                    TaxTypeForFilter.Add(tempInDirectTax);
                }

                // IsLoading = false;
            }
            catch (Exception)
            {
                IsLoading = false;

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
                if (TabIdentification.D.Direct == "X")
                {
                    await PopulateDataForTransactionTypes("D");
                }
                if (TabIdentification.D.Indirect == "X")
                {
                    await PopulateDataForTransactionTypes("I");
                }

                HeaderSet = await WebServiceManager.GAZTGetAccountStatementHeaderSet
                    (AllTransactionFilters.FirstOrDefault().StatementFilter, string.Empty, AllTransactionFilters.FirstOrDefault().TaxType, false);

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
                TransactionTypeFilter = new ObservableCollection<TaxRelationSetResult>(HeaderSet.D.TaxRelationSet.Results.Where(temp => temp.DisplayId == 01 || temp.DisplayId == 02 || temp.DisplayId == 03 || temp.DisplayId == 04 || temp.DisplayId == 06 || temp.DisplayId == 07).ToList());


                SelectedTransactionTypeFilter = TransactionTypeFilter.FirstOrDefault();


                var list = new List<string>();

                foreach (TaxRelationSetResult dropdown in TransactionTypeFilter)
                {
                    try
                    {
                        list.Add(dropdown.Txt30.ToUpper());
                    }
                    catch (Exception)
                    {
                    }


                }


                GenericPickerModel genericPickerModel = new GenericPickerModel();
                genericPickerModel.PickerData = list;
                genericPickerModel.PickerTitle = "";
                genericPickerModel.PickerId = "AccountStatement";
                genericPickerModel.SelectedValue = SelectedTransactionTypeFilter.Txt30;

                PickerModel = genericPickerModel;

                //IsLoading = false;

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
            catch (Exception)
            {
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
            if (SelectedTransactionTypeFilter != null)
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
                        MyBills = new ObservableCollection<MyBills>(MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 0) || x.Status == Enum.GetName(typeof(BillStatus), 1) || x.Status == Enum.GetName(typeof(BillStatus), 2)).ToList());

                        FilterOnTaxType(MyBills);
                        ApplyFilter();
                        await Task.Run(() =>
                        {
                            IsLoading = false;
                        });
                        return;


                    }
                }
                catch (Exception)
                {
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

                                MyBills = new ObservableCollection<MyBills>(filterItems.Where(p => p.Faedn >= FormatedTxFromDate && p.Faedn <= FormatedTxToDate));
                            }
                            else
                            {
                                CultureInfo arCI = new CultureInfo("en-US");
                                DateTime FormatedTxFromDate = DateTime.ParseExact(TxFromDate, "yyyy/MM/dd", arCI.DateTimeFormat,
                                    DateTimeStyles.AllowInnerWhite);
                                DateTime FormatedTxToDate = DateTime.ParseExact(TxToDate, "yyyy/MM/dd", arCI.DateTimeFormat,
                                    DateTimeStyles.AllowInnerWhite);
                                MyBills = new ObservableCollection<MyBills>(filterItems.Where(p => p.Faedn >= FormatedTxFromDate && p.Faedn <= FormatedTxToDate));
                            }

                            FilterOnTaxType(MyBills);
                        }
                    }
                    catch (Exception)
                    {
                    }


                    try
                    {



                        if (TPFromDate != "" && TPToDate != "")
                        {
                            var filterItems = MyBills;


                            //if (IsHijriCal)
                            //{
                            //    CultureInfo arCI = new CultureInfo("ar-SA");
                            //    DateTime FormatedTxFromDate = DateTime.ParseExact(TPFromDate, "yyyy", arCI.DateTimeFormat,
                            //        DateTimeStyles.AllowInnerWhite);
                            //    DateTime FormatedTxToDate = DateTime.ParseExact(TPToDate, "yyyy", arCI.DateTimeFormat,
                            //        DateTimeStyles.AllowInnerWhite);

                            //    MyBills = new ObservableCollection<MyBills>(filterItems.Where(p => p.FormatedFromTaxPeriod >= FormatedTxFromDate && p.FormatedToTaxPeriod <= FormatedTxToDate));
                            //}
                            //else
                            //{
                            //    CultureInfo arCI = new CultureInfo("en-US");
                            //    DateTime FormatedTxFromDate = DateTime.ParseExact(TPFromDate, "yyyy", arCI.DateTimeFormat,
                            //        DateTimeStyles.AllowInnerWhite);
                            //    DateTime FormatedTxToDate = DateTime.ParseExact(TPToDate, "yyyy", arCI.DateTimeFormat,
                            //        DateTimeStyles.AllowInnerWhite);
                            //    MyBills = new ObservableCollection<MyBills>(filterItems.Where(p => p.FormatedFromTaxPeriod >= FormatedTxFromDate && p.FormatedToTaxPeriod <= FormatedTxToDate));
                            //}

                            if (IsHijriCal)
                            {
                                CultureInfo arCI = new CultureInfo("ar-SA");
                                DateTime FormatedTpFromDate = DateTime.ParseExact(TPFromDate, "yyyy", arCI.DateTimeFormat,
                                    DateTimeStyles.AllowInnerWhite);
                                DateTime FormatedTpToDate = DateTime.ParseExact(TPToDate, "yyyy", arCI.DateTimeFormat,
                                    DateTimeStyles.AllowInnerWhite);

                                MyBills = new ObservableCollection<MyBills>(filterItems.Where(p => Convert.ToInt32(string.Format("{0:yyyy}", p.PeriodPart1)) >= Convert.ToInt32(string.Format("{0:yyyy}", FormatedTpFromDate)) && Convert.ToInt32(string.Format("{0:yyyy}", p.PeriodPart2)) <= Convert.ToInt32(string.Format("{0:yyyy}", FormatedTpToDate))));
                            }
                            else
                            {
                                CultureInfo enCI = new CultureInfo("en-US");
                                DateTime FormatedTpFromDate = DateTime.ParseExact(TPFromDate, "yyyy", enCI.DateTimeFormat,
                                    DateTimeStyles.AllowInnerWhite);
                                DateTime FormatedTpToDate = DateTime.ParseExact(TPToDate, "yyyy", enCI.DateTimeFormat,
                                    DateTimeStyles.AllowInnerWhite);

                                MyBills = new ObservableCollection<MyBills>(filterItems.Where(p => Convert.ToInt32(p.PeriodPart1.Substring(p.PeriodPart1.Length - 4, 4)) >= Convert.ToInt32(string.Format("{0:yyyy}", FormatedTpFromDate)) && Convert.ToInt32(p.PeriodPart2.Substring(p.PeriodPart2.Length - 4, 4)) <= Convert.ToInt32(string.Format("{0:yyyy}", FormatedTpToDate))));

                            }

                            FilterOnTaxType(MyBills);


                        }
                    }
                    catch (Exception)
                    {
                    }




                    if (FromTxAmount != "" && ToTxAmount != "")
                    {
                        var filterItems = MyBills;
                        MyBills = new ObservableCollection<MyBills>(filterItems.Where(p => Convert.ToDouble(p.BETRW) >= Convert.ToDouble(FromTxAmount) && Convert.ToDouble(p.BETRW) <= Convert.ToDouble(ToTxAmount)));
                    }


                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });
                    if (string.IsNullOrEmpty(TxFromDate) && string.IsNullOrEmpty(TxToDate) && string.IsNullOrEmpty(TPFromDate) && string.IsNullOrEmpty(TPToDate) && string.IsNullOrEmpty(FromTxAmount) && string.IsNullOrEmpty(ToTxAmount))
                    {
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.AccountStatementsEnterAmount));

                        //                _dialogService.ShowMessage(AppResources.AccountStatementsTransactionDate, AppResources.Information);
                        return;
                    }
                    else if (string.IsNullOrEmpty(FromTxAmount) && !string.IsNullOrEmpty(ToTxAmount) || !string.IsNullOrEmpty(FromTxAmount) && string.IsNullOrEmpty(ToTxAmount))
                    {
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.AccountStatementsEnterAmount));
                        //                _dialogService.ShowMessage(AppResources.AccountStatementsEnterAmount, AppResources.Information);
                        return;
                    }
                    else if (!string.IsNullOrEmpty(TxFromDate) && string.IsNullOrEmpty(TxToDate) || string.IsNullOrEmpty(TxFromDate) && !string.IsNullOrEmpty(TxToDate))
                    {
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.AccountStatementsTransactionDate));
                        //                _dialogService.ShowMessage(AppResources.AccountStatementsTransactionDate, AppResources.Information);
                        return;
                    }
                    else if (!string.IsNullOrEmpty(TPFromDate) && string.IsNullOrEmpty(TPToDate) || string.IsNullOrEmpty(TPFromDate) && !string.IsNullOrEmpty(TPToDate))
                    {
                        PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.AccountStatementsTaxPeriod));
                        //_dialogService.ShowMessage(AppResources.AccountStatementsTaxPeriod, AppResources.Information);
                        return;
                    }
                    isFromFilter = false;
                    _navigationService.GoBack();
                }
                else
                {
                    if (FromStatus != "")
                    {
                        if (FromStatus.Equals(AppResources.Paid))
                        {
                            MyBills = new ObservableCollection<MyBills>(MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 0)).ToList());
                        }

                        if (FromStatus.Equals(AppResources.PartiallyPaid))
                        {
                            MyBills = new ObservableCollection<MyBills>(MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 1)).ToList());
                        }

                        if (FromStatus.Equals(AppResources.UnPaid))
                        {
                            MyBills = new ObservableCollection<MyBills>(MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 2)).ToList());
                        }

                        if (FromStatus.Equals(AppResources.All))
                        {
                            MyBills = new ObservableCollection<MyBills>(MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 0) || x.Status == Enum.GetName(typeof(BillStatus), 1) || x.Status == Enum.GetName(typeof(BillStatus), 2)).ToList());
                        }
                        //FromStatus = "";

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
                            var suggestion = MyBillsOriginal.Where(c => c.Abtypt.ToLower().Contains(SearchText.ToLower()) || c.Fbnum.ToLower().Contains(SearchText.ToLower())
                                || c.Status.ToLower().Contains(SearchText.ToLower()) || c.StatusText.ToLower().Contains(SearchText.ToLower())
                                || c.BETRW.ToLower().Contains(SearchText.ToLower()) || c.TestDueAmount.ToLower().Contains(SearchText.ToLower())
                                || c.Txt30.ToLower().Contains(SearchText.ToLower())).ToList();

                            MyBills = new ObservableCollection<MyBills>(suggestion);
                            FilterOnTaxType(MyBills);
                        }
                        else
                        {

                            MyBills = new ObservableCollection<MyBills>(MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 0) || x.Status == Enum.GetName(typeof(BillStatus), 1) || x.Status == Enum.GetName(typeof(BillStatus), 2)).ToList());

                            //FromStatus = "";

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

            if (SelectedTransactionTypeFilter != null)
            {

                try
                {

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
                catch (Exception)
                {
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
            FilterIfTypeAndStausFilterSelected(false);

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
                    await PopupNavigation.Instance.PushAsync(new PickerPageView(PickerModel));
            }
            catch (GAZTUnlockAccountException )
            {


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
            var chipmodelPaid = new ChipModel { TemplateType = "00FF00", Text = AppResources.Paid, ImageSource = null, TextColor = (Color)Application.Current.Resources["Success"] };
            var chipmodelPartiallyPaid = new ChipModel { TemplateType = "FFFFE0", Text = AppResources.PartiallyPaid, ImageSource = null, TextColor = (Color)Application.Current.Resources["Partial"] };
            var chipmodelUnPaid = new ChipModel { TemplateType = "FF7F50", Text = AppResources.UnPaid, ImageSource = null, TextColor = (Color)Application.Current.Resources["Error"] };
            statusList.Add(chipmodelPaid);
            statusList.Add(chipmodelPartiallyPaid);
            statusList.Add(chipmodelUnPaid);

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



        public AccountStatementBillsPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
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

                if (string.IsNullOrEmpty(TxFromDate) && string.IsNullOrEmpty(TxToDate) && string.IsNullOrEmpty(TPFromDate) && string.IsNullOrEmpty(TPToDate) && string.IsNullOrEmpty(FromTxAmount) && string.IsNullOrEmpty(ToTxAmount))
                {

                    PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.AcFilterEmptyState));

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
        }

    }
}