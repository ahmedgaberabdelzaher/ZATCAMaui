using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using EGAZT.Models.AccountStatements;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.AccountStatements
{
    public class AccountStatementsPageViewModel:BaseViewModel
    {
        public ICommand GoBackBtnTapped { get; set; }

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

            PopulateReturnTypeList();
        }

        public async void PopulateReturnTypeList()
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

        public async void PopulateDataInChipsForTaxTypes()
        {
            ChipDataFilterlist = new ObservableCollection<ASChipModel>();

            foreach(ASRevenueDropDownSetDataResults aSRevenueDropDownSetDataResults in RevenueDropDownParent.D.Results)
            {
                ChipDataFilterlist.Add(new ASChipModel() { Text = aSRevenueDropDownSetDataResults.Txt30, TemplateType = AppResources.Paid});
            }

            //new ASChipModel(){Text =AppResources.ZZZAKAT, TemplateType = AppResources.Paid},
            //new ASChipModel(){Text =AppResources.ASIncomeTax, TemplateType = AppResources.PartiallyPaid,ImageSource = "partially_clock.png"},
            //new ASChipModel(){Text =AppResources.ASWithholdingTax, TemplateType = AppResources.UnPaid,ImageSource = "ic_unpaid.png"},
            //new ASChipModel(){Text =AppResources.ASAllTransactions, TemplateType = AppResources.UnPaid,ImageSource = "ic_unpaid.png"}
        }

        public void PopulateDataInChipsForYears()
        {
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
                RevenueDropDownParent = await WebServiceManager.GAZTGetAccountStatementsRevenueDropDownSet("D");
                HeaderSet = await WebServiceManager.GAZTGetAccountStatementHeaderSet(string.Empty,string.Empty, "D");
                StatementsLineItems = new ObservableCollection<ASResult>(HeaderSet.D.StatmenetLineItemsSet.Results);

                //YearValuesHeader = await WebServiceManager.GAZTGetAccountStatementYearValuesHeaderSet(string.Empty, string.Empty, "D");
                PopulateDataInChipsForTaxTypes();
                PopulateDataInChipsForYears();
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
