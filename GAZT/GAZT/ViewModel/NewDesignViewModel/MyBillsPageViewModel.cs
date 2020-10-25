using EGAZT.Models;
using EGAZT.Views.NewDesign.EstimatedZAKATReturnsPages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    public class GAZTNewDesignMyBillsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnBackButtonClicked { get; set; }
        #region Property
        public List<ReturnTypes> _TaxTypeForFilter = null;
        public List<ReturnTypes> TaxTypeForFilter
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
        
        public ChipModel _selectedChipFilterItem=null;
        public ChipModel SelectedChipFilterItem
        {
            get
            {
                return _selectedChipFilterItem;
            }
            set
            {
                _selectedChipFilterItem = value;
                if (_selectedChipFilterItem != null)
                {
                    FilterIfTypeAndStausFilterSelected();
                    
                }
                RaisePropertyChanged("SelectedChipFilterItem");
            }
        }

        public Color _SelectionColor = Color.Transparent;
        public Color SelectionColor
        {
            get
            {
                return _SelectionColor;
            }
            set
            {
                _SelectionColor = value;
                
                RaisePropertyChanged("SelectionColor");
            }
        }
        

        public ObservableCollection<ChipModel> _chipDataFilterlist=null;
        public ObservableCollection<ChipModel> ChipDataFilterlist
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
        public ReturnTypes _SelectedTaxTypeForFilter = null;
        public ReturnTypes SelectedTaxTypeForFilter
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
                    FilterOnTaxType(MyBillsOriginal);
                    FilterIfTypeAndStausFilterSelected();

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

        private ObservableCollection<MyBills> _TaxTypeFilteredBills;
        public ObservableCollection<MyBills> TaxTypeFilteredBills
        {
            get
            {
                return _TaxTypeFilteredBills;
            }
            set
            {
                _TaxTypeFilteredBills = value;
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
                _myBills = value;
                if(_myBills!=null)
                {

                    if (_myBills.Count != 0)
                    {
                        double Amount = 0.00;
                        foreach (var item in MyBills)
                        {
                            //P = 0 - Paid
                            //I = 1 - Partially Paid
                            //O = 2 - Unpaid

                            if(item.Status == "O")
                            {
                                if (item.TestDueAmount != null)
                                {
                                    Amount = Amount + Convert.ToDouble(item.TestDueAmount);
                                }
                            }
                            else if(item.Status == "I")
                            {
                                if(item.TotalRemainingAmount != null && item.TotalRemainingAmount != string.Empty)
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
                        AmountLabel = TestDueAmount + " "+AppResources.ZSAR ;
                 
                        IsListVisible = true;
                        isNoDataLableVisible = false;
                    }
                    else
                    {
                        AmountLabel = " - ";
                        IsListVisible = false;
                        isNoDataLableVisible = true;
                    }

                }
                RaisePropertyChanged("MyBills");
            }
        }

        private string _amountLabel= " - ";
        public string AmountLabel
        {
            get
            {
                return _amountLabel;
            }
            set
            {
                _amountLabel = value;
              
             
                RaisePropertyChanged("AmountLabel");
            }
        }
        
        private bool _isLoading = false;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
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
                _isListVisible = value;
                RaisePropertyChanged("IsListVisible");
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
                _isNoDataLableVisible = value;
                RaisePropertyChanged("isNoDataLableVisible");
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
                _selcectedBillsIndex = value;
                RaisePropertyChanged("SelcectedBillsIndex");
            }
        }
        
        #endregion

        #region Constructor
        public GAZTNewDesignMyBillsPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            _navigationService = navigationService;
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            _dialogService = dialogService;
            OnBackButtonClicked = new Xamarin.Forms.Command(() =>
            {
                _navigationService.GoBack();
            });

        }
        #endregion

        #region Method

        public void onPageLoad(BillInfo billInfo)
        {
            IsLoading = true;
            MyBills = null;
            
            try
            {
                try
                {
                    string lang = UtilityManager.GetLanguageParameter();
                    MyBills = WebServiceManager.GAZTGetMyBills(App.TP.Tin, lang);
                    PopToRootPage();// If seesion Expired it will navigate to Dashboard page

                    if (MyBills != null && MyBills.Count != 0)
                    {
                        MyBillsOriginal = MyBills;
                       
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

                        foreach(MyBills myBills in MyBills)
                        {
                            if(myBills.Period.Contains("000000") || myBills.PeriodPart1.Contains("000000") || myBills.PeriodPart2.Contains("000000"))
                            {
                                myBills.IsPeriodVisible = false;
                            }
                            else
                            {
                                myBills.IsPeriodVisible = true;
                            }

                            if (myBills.Status == "I")
                            {
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

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                       // await _dialogService.ShowMessageBox(e.Message, AppResources.Information);
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(e.Message));
                        _navigationService.GoBack();
                    });
                    IsLoading = false;
                }
            }
            catch (InternetException ex)
            {

                Device.BeginInvokeOnMainThread(async () =>
                {
                   // await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                    _navigationService.GoBack();
                });
                IsLoading = false;
            }
            IsLoading = false;
        }
        public void PopulateReturnTypeList()
        {
            try
            {
                TaxTypeForFilter = new List<ReturnTypes>
                {
                        new ReturnTypes {Id = "00",TaxType = AppResources.AllBills},
                        new ReturnTypes {Id = "01",TaxType = AppResources.ZakatnewUi},
                        new ReturnTypes {Id = "02",TaxType = AppResources.ZZVAT},
                        new ReturnTypes {Id = "03",TaxType = AppResources.ZZET},
                        new ReturnTypes {Id = "04",TaxType = AppResources.ZZWithholding},
                        new ReturnTypes {Id = "05",TaxType = AppResources.ZZIncomeTax}
                };

                SelectedTaxTypeForFilter = TaxTypeForFilter.FirstOrDefault();
            }
            catch (Exception ex)
            {
            }


        }
        public void PopulateDataInChips() 

        {
            ChipDataFilterlist = new ObservableCollection<ChipModel>()
            {
                //new ChipModel(){Text =AppResources.Paid, TemplateType = AppResources.Paid, ImageSource="Paid_check.png"},
                new ChipModel(){Text =AppResources.UnPaid, TemplateType = AppResources.UnPaid,ImageSource = "ic_unpaid.png"},
                new ChipModel(){Text =AppResources.Partiallynewui, TemplateType = AppResources.PartiallyPaid,ImageSource = "partially_clock.png"}
                //new ChipModel(){Text =AppResources.All, TemplateType = AppResources.All,ImageSource = "ic_money.png"}
            };
        }        
        public async Task PopToRootPage()
        {
            try
            {
                if (App.IsSessionExpired)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        if (App.TP != null)
                            App.TP = null;
                        if (App.PreviousIsArabic)
                        {
                            String langName = "ar-SA";
                            AppResources.Culture = new CultureInfo(langName);
                        }
                        else
                        {
                            String langName = "en-US";
                            AppResources.Culture = new CultureInfo(langName);
                        }
                        var _navigation = Application.Current.MainPage.Navigation;
                        foreach (var item in _navigation.NavigationStack)
                        {
                            if (item.GetType().Name == App.SFLoginPageView)
                            {
                                _navigation.RemovePage(item);
                                break;
                            }
                        }
                    // _navigationService.NavigateTo(App.SFLoginPageView);
                    _navigationService.NavigateTo(App.SFLoginPageView, App.GAZTNewDesignDashBoardPageView);
                        _navigation.NavigationStack.ToList().Clear();
                    });
                }

            }
            catch(Exception ex)
            {

            }
        }
        #endregion
        public void FilterOnTaxType(ObservableCollection<MyBills> BillsToProcss)
        {
            if (SelectedTaxTypeForFilter.Id == "00")
            {
                MyBills = new ObservableCollection<MyBills>(BillsToProcss);
            }
            
            if (SelectedTaxTypeForFilter.Id == "01")
            {
                if (App.IsArabic)
                {
                    MyBills = new ObservableCollection<MyBills>(BillsToProcss.Where(x => x.Abtypt.Equals("الزكاة") || x.Abtypt.Equals("الزكاة")).ToList());
                }
                else
                {
                    MyBills = new ObservableCollection<MyBills>(BillsToProcss.Where(x => x.Abtypt.Equals("Zakat")|| x.Abtypt.Equals("Voluntary Zakat")).ToList());
                }
            }
            
            if (SelectedTaxTypeForFilter.Id == "02")
            {
                if (App.IsArabic) 
                {
                    MyBills = new ObservableCollection<MyBills>(BillsToProcss.Where(x => x.Abtypt.Equals("ضريبة القيمة المضافة")|| x.Abtypt.Equals("ضريبة القيمة المضافة")).ToList());
                }
                else
                {
                    MyBills = new ObservableCollection<MyBills>(BillsToProcss.Where(x => x.Abtypt.Equals("VAT") || x.Abtypt.Equals("VAT Eligible Person")).ToList());
                }
            }
            
            if (SelectedTaxTypeForFilter.Id == "03")
            {
                if (!App.IsArabic)
                {
                    MyBills = new ObservableCollection<MyBills>(BillsToProcss.Where(x => x.Abtypt.Equals("Excise Tax") || x.Abtypt.Equals("ETAX")).ToList());
                }
                else
                {
                    MyBills = new ObservableCollection<MyBills>(BillsToProcss.Where(x => x.Abtypt.Equals("الضريبة الانتقائية")).ToList());
                }
            }
            
            if (SelectedTaxTypeForFilter.Id == "04")
            {
                if (!App.IsArabic)
                {
                    MyBills = new ObservableCollection<MyBills>(BillsToProcss.Where(x => x.Abtypt.Equals("Withholding Tax")).ToList());
                }
                else
                {
                    MyBills = new ObservableCollection<MyBills>(BillsToProcss.Where(x => x.Abtypt.Equals("ضريبة الاستقطاع") || x.Abtypt.Equals("Excise Tax")).ToList());
                }
            }
            if (SelectedTaxTypeForFilter.Id == "05")
            {
                if (!App.IsArabic)
                {
                    MyBills = new ObservableCollection<MyBills>(BillsToProcss.Where(x => x.Abtypt.Equals("Income Tax")).ToList());
                }
                else
                {
                    MyBills = new ObservableCollection<MyBills>(BillsToProcss.Where(x => x.Abtypt.Equals("ضريبة الدخل")).ToList());
                }
            }

        }

        public void FilterIfTypeAndStausFilterSelected()
        {
            if (SelectedChipFilterItem != null)
            {
                if (SelectedChipFilterItem.TemplateType.Equals(AppResources.Paid))
                {
                    MyBills = new ObservableCollection<MyBills>(MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 0)).ToList());
                }

                if (SelectedChipFilterItem.TemplateType.Equals(AppResources.PartiallyPaid))
                {
                    MyBills = new ObservableCollection<MyBills>(MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 1)).ToList());
                }

                if (SelectedChipFilterItem.TemplateType.Equals(AppResources.UnPaid))
                {
                    MyBills = new ObservableCollection<MyBills>(MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 2)).ToList());
                }

                if (SelectedChipFilterItem.TemplateType.Equals(AppResources.All))
                {
                    MyBills = new ObservableCollection<MyBills>(MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 0) || x.Status == Enum.GetName(typeof(BillStatus), 1) || x.Status == Enum.GetName(typeof(BillStatus), 2)).ToList());
                }

                FilterOnTaxType(MyBills);
            }
        }

    }
}
