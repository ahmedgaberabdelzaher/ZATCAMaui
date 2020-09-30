using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EGAZT.Models.AccountStatements;
using GalaSoft.MvvmLight.Views;
using GAZT.Models;

namespace EGAZT.ViewModel.NewDesignViewModel.AccountStatements
{
    public class AccountStatementsPageViewModel:BaseViewModel
    {
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

            PopulateReturnTypeList();
            PopulateDataInChipsForDirectTaxes();
            PopulateDataInChipsForIndirectTaxes();
        }

        public void PopulateReturnTypeList()
        {
            try
            {
                TaxTypeForFilter = new List<ASReturnTypes>
                {
                    new ASReturnTypes {Id = "00",TaxType = AppResources.ASDirectTaxes},
                    new ASReturnTypes {Id = "01",TaxType = AppResources.ASIndirectTaxes},
                };

                SelectedTaxTypeForFilter = TaxTypeForFilter.FirstOrDefault();
            }
            catch (Exception ex)
            {

            }
        }

        public void PopulateDataInChipsForDirectTaxes()
        {
            ChipDataFilterlist = new ObservableCollection<ASChipModel>()
            {
                new ASChipModel(){Text =AppResources.ZZZAKAT, TemplateType = AppResources.Paid},
                new ASChipModel(){Text =AppResources.ASIncomeTax, TemplateType = AppResources.PartiallyPaid,ImageSource = "partially_clock.png"},
                new ASChipModel(){Text =AppResources.ASWithholdingTax, TemplateType = AppResources.UnPaid,ImageSource = "ic_unpaid.png"},
                new ASChipModel(){Text =AppResources.ASAllTransactions, TemplateType = AppResources.UnPaid,ImageSource = "ic_unpaid.png"}
            };
        }

        public void PopulateDataInChipsForIndirectTaxes()
        {
            ChipDataFilterlist = new ObservableCollection<ASChipModel>()
            {
                new ASChipModel(){Text =AppResources.ZZVAT, TemplateType = AppResources.Paid},
                new ASChipModel(){Text =AppResources.ExciseCertificates, TemplateType = AppResources.PartiallyPaid,ImageSource = "partially_clock.png"},
                new ASChipModel(){Text =AppResources.ASCustoms, TemplateType = AppResources.UnPaid,ImageSource = "ic_unpaid.png"},
                new ASChipModel(){Text =AppResources.ASAllTransactions, TemplateType = AppResources.UnPaid,ImageSource = "ic_unpaid.png"}
            };
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
