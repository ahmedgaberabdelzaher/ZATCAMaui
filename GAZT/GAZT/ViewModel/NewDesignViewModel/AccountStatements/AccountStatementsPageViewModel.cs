using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models.AccountStatements;
using EGAZT.Views.NewDesign.AccountStatements;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.AccountStatements
{
    public class AccountStatementsPageViewModel:BaseViewModel
    {
        public ICommand GoBackBtnTapped { get; set; }
        public ICommand FiltersTapped { get; set; }

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

        public ASRevenueDropDownSet _revenueDropDownParent = null;
        public ASRevenueDropDownSet RevenueDropDownParent
        {
            get
            {
                return _revenueDropDownParent;
            }
            set
            {
                _revenueDropDownParent = value;
                RaisePropertyChanged("RevenueDropDownParent");
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

        public List<ASReturnTypes> _TaxTypeForFilter = null;
        public List<ASReturnTypes> TaxTypeForFilter
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
                    //FilterOnTaxType(MyBillsOriginal);
                    //FilterIfTypeAndStausFilterSelected();

                }
                RaisePropertyChanged("SelectedTaxTypeForFilter");
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

            IsSortByVisible = false;
            FiltersTapped = new Command(FiltersClicked);
            PopulateReturnTypeList();
        }

        public void FiltersClicked()
        {
            IsSortByVisible = !IsSortByVisible;
            //await PopupNavigation.Instance.PushAsync(new AccountStatementsFiltersPageView());
        }

        public void PopulateReturnTypeList()
        {
            try
            {
                TaxTypeForFilter = new List<ASReturnTypes>
                {
                    new ASReturnTypes {Id = "00",TaxType = "Direct Taxes"},
                    new ASReturnTypes {Id = "01",TaxType = "Indirect Taxes"},
                };

                SelectedTaxTypeForFilter = TaxTypeForFilter.FirstOrDefault();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task PopulateDataInChipsForTaxTypes(string taxType)
        {
            RevenueDropDownParent = await WebServiceManager.GAZTGetAccountStatementsRevenueDropDownSet(taxType);

            ChipDataFilterlist = new ObservableCollection<ASChipModel>();

            foreach(ASRevenueDropDownSetDataResults aSRevenueDropDownSetDataResults in RevenueDropDownParent.D.Results)
            {
                ChipDataFilterlist.Add(new ASChipModel() { Text = aSRevenueDropDownSetDataResults.Txt30, StatementFilter = aSRevenueDropDownSetDataResults.StatementFilter, TemplateType = AppResources.Paid});
            }

            //new ASChipModel(){Text =AppResources.ZZZAKAT, TemplateType = AppResources.Paid},
            //new ASChipModel(){Text =AppResources.ASIncomeTax, TemplateType = AppResources.PartiallyPaid,ImageSource = "partially_clock.png"},
            //new ASChipModel(){Text =AppResources.ASWithholdingTax, TemplateType = AppResources.UnPaid,ImageSource = "ic_unpaid.png"},
            //new ASChipModel(){Text =AppResources.ASAllTransactions, TemplateType = AppResources.UnPaid,ImageSource = "ic_unpaid.png"}
        }

        public async Task PopulateDataInChipsForYears(string taxType, string statementFilter)
        {
            //YearValuesHeader = await WebServiceManager.GAZTGetAccountStatementYearValuesHeaderSet(string.Empty, string.Empty, taxType, statementFilter);

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

            //new ASChipModel(){Text =AppResources.ZZZAKAT, TemplateType = AppResources.Paid},
            //new ASChipModel(){Text =AppResources.ASIncomeTax, TemplateType = AppResources.PartiallyPaid,ImageSource = "partially_clock.png"},
            //new ASChipModel(){Text =AppResources.ASWithholdingTax, TemplateType = AppResources.UnPaid,ImageSource = "ic_unpaid.png"},
            //new ASChipModel(){Text =AppResources.ASAllTransactions, TemplateType = AppResources.UnPaid,ImageSource = "ic_unpaid.png"}
        }

        public async void PopulateASFilterData()
        {
            try
            {
                TabIdentification = await WebServiceManager.GAZTGetAccountStatementsTabIdentification();
                //await PopulateDataInChipsForTaxTypes();
                //RevenueDropDownParent = await WebServiceManager.GAZTGetAccountStatementsRevenueDropDownSet("D");
                HeaderSet = await WebServiceManager.GAZTGetAccountStatementHeaderSet(string.Empty,string.Empty, "D");

                ASResult totalBalances = new ASResult();
                totalBalances.IsTotalBalanceVisile = true;

                StatementsLineItems = new ObservableCollection<ASResult>(HeaderSet.D.StatmenetLineItemsSet.Results);

                StatementsLineItems.Insert(0, totalBalances);

                //YearValuesHeader = await WebServiceManager.GAZTGetAccountStatementYearValuesHeaderSet(string.Empty, string.Empty, "D");

                ASResult totalBalance = new ASResult();
                totalBalance.IsTotalBalanceVisile = true;
                totalBalance.OpeningBalance = HeaderSet.D.Open;
                totalBalance.ClosingBalance = HeaderSet.D.Close;

                //YearValuesHeader = await WebServiceManager.GAZTGetAccountStatementYearValuesHeaderSet(string.Empty, string.Empty, "D");
                PopulateDataInChipsForYears("","");
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
