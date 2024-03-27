using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using RGPopup.Maui.Services;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.PaymentModel;
using ZATCAMAUI.Models.SyncfusionEnabledModels;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using ZATCAMAUI.Views.NewDesign.GenericPickers;
using ZATCAMAUI.Views.NewDesign.MyBillsPages;
using ZATCAMAUI.Views.NewDesign.PaymentOptions;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel
{

    public class GAZTNewDesignMyBillsPageViewModel : BaseViewModel
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnBackButtonClicked { get; set; }
        public string selectedFbNum = "";
        MyBills BModel = null;
        public string selectedSadadNo = "";
        public string selectedAmount = "";
        public string selectedTaxablePeriod = "";
        private bool calculateMyBills = false;
        public bool isPayNowTapped = false;

        #region Property
        public List<MyBillsFilterDropdown> _TaxTypeForFilter = null;
        public List<MyBillsFilterDropdown> TaxTypeForFilter
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

        private string _sadadBindNumber = "";
        public string SadadBindNumber
        {
            get
            {
                return _sadadBindNumber;
            }
            set
            {
                if (_sadadBindNumber == value) return;

                _sadadBindNumber = value;
                RaisePropertyChanged("SadadBindNumber");
            }
        }

        private string _referenceNumber = "";
        public string ReferenceNumber
        {
            get
            {
                return _referenceNumber;
            }
            set
            {
                if (_referenceNumber == value) return;

                _referenceNumber = value;
                RaisePropertyChanged("ReferenceNumber");
            }
        }

        private string _taxablePeriod = "";
        public string TaxablePeriod
        {
            get
            {
                return _taxablePeriod;
            }
            set
            {
                if (_taxablePeriod == value) return;

                _taxablePeriod = value;
                RaisePropertyChanged("TaxablePeriod");
            }
        }

        private string _totalAmount = "0.0";
        public string TotalAmount
        {
            get
            {
                return _totalAmount;
            }
            set
            {
                if (_totalAmount == value) return;

                _totalAmount = value;
                RaisePropertyChanged("TotalAmount");
            }
        }

        public ChipModel _selectedChipFilterItem = null;
        public ChipModel SelectedChipFilterItem
        {
            get
            {
                return _selectedChipFilterItem;
            }
            set
            {


                _selectedChipFilterItem = value;
                if (value != null)
                {
                    FilterIfTypeAndStausFilterSelected(false);
                }

                RaisePropertyChanged("SelectedChipFilterItem");
            }
        }

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


        public ObservableCollection<ChipModel> _chipDataFilterlist = null;
        public ObservableCollection<ChipModel> ChipDataFilterlist
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
        public MyBillsFilterDropdown _SelectedTaxTypeForFilter = null;
        public MyBillsFilterDropdown SelectedTaxTypeForFilter
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
                    FilterLabelText = _SelectedTaxTypeForFilter.Txt30;
                    FilterIfTypeAndStausFilterSelected(true);
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

        private ObservableCollection<MyBills> _TaxTypeFilteredBills;
        public ObservableCollection<MyBills> TaxTypeFilteredBills
        {
            get
            {
                return _TaxTypeFilteredBills;
            }
            set
            {
                if (_TaxTypeFilteredBills == value) return;

                _TaxTypeFilteredBills = value;
            }
        }


        private ObservableCollection<MyBills> multiplePayableBills;
        public ObservableCollection<MyBills> MultiplePayableBills
        {
            get
            {
                return multiplePayableBills;
            }
            set
            {
                if (multiplePayableBills == value) return;

                multiplePayableBills = value;

                RaisePropertyChanged("MultiplePayableBills");
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


        private string _AmountTitle = AppResources.DashBoardMyTaxObligations;
        public string AmountTitle
        {
            get
            {
                return _AmountTitle;
            }
            set
            {
                if (_AmountTitle == value) return;

                _AmountTitle = value;


                RaisePropertyChanged("AmountTitle");
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

        public ValidatePaymentResponse _paymentData = null;
        public ValidatePaymentResponse PaymentData
        {
            get
            {
                return _paymentData;
            }
            set
            {
                if (_paymentData == value) return;

                _paymentData = value;
                RaisePropertyChanged("PaymentData");
            }
        }

        private bool _applePayStatus;
        public bool ApplePayStatus
        {
            get
            {
                return _applePayStatus;
            }
            set
            {
                if (_applePayStatus == value) return;

                _applePayStatus = value;
                RaisePropertyChanged("ApplePayStatus");
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
        public string ApplePayTokenData = "";

        #endregion

        #region Constructor
        public GAZTNewDesignMyBillsPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
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
            OnBackButtonClicked = new Command(() =>
            {
                _navigationService.GoBack();
            });

        }
        #endregion

        #region Method

        public async void verifyPaymentAndShowBillsPopup(MyBills BModel)
        {
            this.BModel = BModel;
            MultiplePayableBills = new ObservableCollection<MyBills>();
            if (MyBillsOriginal != null && MyBillsOriginal.Count > 0)
            {
                MultiplePayableBills = new ObservableCollection<MyBills>(MyBillsOriginal.Where(x => !string.IsNullOrEmpty(BModel.VTRE2) && x.VTRE2.Equals(BModel.VTRE2) && (BModel.Status == Enum.GetName(typeof(BillStatus), 1) || BModel.Status == Enum.GetName(typeof(BillStatus), 2))).ToList());

                //MultiplePayableBills = new ObservableCollection<MyBills>(MyBills.Where(x => (!String.IsNullOrEmpty(BModel.VTRE2) && x.VTRE2.Equals(BModel.VTRE2)) || (!String.IsNullOrEmpty(BModel.Fbnum) && x.Fbnum.Equals(BModel.Fbnum))).ToList());
            }

            if (MultiplePayableBills != null && MultiplePayableBills.Count > 1)
            {
                await PopupNavigation.Instance.PushAsync(new MyBillsMultiplePayableList(MultiplePayableBills));
                await Task.Delay(2000);
                isPayNowTapped = false;
                return;
            }
            else
            {
                showPaymentOptions();
            }


        }

        public async void showPaymentOptions()
        {

            if (BModel != null)
            {
                if (BModel.MadabutFg == "X")
                {
                    await PopupNavigation.Instance.PushAsync(new PaymentOptionsPageView(true, false, false, ""));
                }
                else
                {
                    await PopupNavigation.Instance.PushAsync(new PaymentOptionsPageView(true, false, true, BModel.OpenliMsg));
                }
                var total = "";
                if (MultiplePayableBills != null && MultiplePayableBills.Count > 0)
                {
                    total = MultiplePayableBills.Sum(x => double.Parse(x.TestDueAmount)).ToString();
                }
                else
                {
                    total = BModel.TestDueAmount;
                }
                selectedFbNum = BModel.Fbnum;
                selectedSadadNo = BModel.VTRE2;
                selectedAmount = total;
                selectedTaxablePeriod = BModel.Persl;
                isPayNowTapped = false;

            }
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
                    MyBills = WebServiceManager.GAZTGetMyBills(App.TP.Tin, lang, "Bills");
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
                        // await _dialogService.ShowMessageBox(e.Message, AppResources.Information);
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
                    // await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(ex.Message));
                    _navigationService.GoBack();
                });
                IsLoading = false;
            }
            IsLoading = false;
        }

        public void updatePicker()
        {

            var selectedFilter = new ObservableCollection<MyBillsFilterDropdown>(TaxTypeForFilter.Where(temp => temp.Txt30.Equals(PickerModel.SelectedValue.ToUpper()))).ToList();

            SelectedTaxTypeForFilter = selectedFilter.FirstOrDefault();

        }

        public async void showPickerDialog()
        {
            try
            {
                if (PickerModel != null)
                    await PopupNavigation.Instance.PushAsync(new PickerPageView(PickerModel));
            }
            catch (GAZTUnlockAccountException ex)
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

        public void PopulateFilterDropdown()
        {

            try
            {
                string lang = UtilityManager.GetLanguageParameter();
                var FilterValues = WebServiceManager.GAZTGetMyBillsFilterDropdownValues(App.TP.Tin, lang);
                if (FilterValues != null)
                {
                    TaxTypeForFilter = FilterValues;
                    SelectedTaxTypeForFilter = TaxTypeForFilter.FirstOrDefault();


                    var list = new List<string>();

                    foreach (MyBillsFilterDropdown dropdown in TaxTypeForFilter)
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
                    genericPickerModel.PickerId = "MyBills";
                    genericPickerModel.SelectedValue = SelectedTaxTypeForFilter.Txt30;

                    PickerModel = genericPickerModel;
                }

            }
            catch (Exception)
            {


            }
        }






        //public void PopulateReturnTypeList()
        //{
        //    try
        //    {
        //        TaxTypeForFilter = new List<ReturnTypes>
        //        {
        //                new ReturnTypes {Id = "00",TaxType = AppResources.AllBills},
        //                new ReturnTypes {Id = "01",TaxType = AppResources.ZakatnewUi},
        //                new ReturnTypes {Id = "02",TaxType = AppResources.ZZVAT},
        //                new ReturnTypes {Id = "03",TaxType = AppResources.ZZET},
        //                new ReturnTypes {Id = "04",TaxType = AppResources.ZZWithholding},
        //                new ReturnTypes {Id = "05",TaxType = AppResources.ZZIncomeTax}
        //        };

        //        SelectedTaxTypeForFilter = TaxTypeForFilter.FirstOrDefault();
        //    }
        //    catch
        //    {
        //    }


        //}
        public void PopulateDataInChips()

        {
            ChipDataFilterlist = new ObservableCollection<ChipModel>()
            {
                new ChipModel(){Text =AppResources.UnPaid, TemplateType = AppResources.UnPaid,ImageSource = "ic_unpaid.png", TextColor=(Color)Application.Current.Resources["Error"]},
                new ChipModel(){Text =AppResources.PartiallyPaid, TemplateType = AppResources.PartiallyPaid,ImageSource = "clock.png",TextColor=(Color)Application.Current.Resources["Partial"]}
            };
        }
        public void PopToRootPage()
        {
            try
            {
                if (App.IsSessionExpired)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        if (App.TP != null)
                            App.TP = null;
                        if (App.PreviousIsArabic)
                        {
                            string langName = "ar-SA";
                            AppResources.Culture = new CultureInfo(langName);
                        }
                        else
                        {
                            string langName = "en-US";
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
            catch
            {

            }
        }
        #endregion
        public async void FilterOnTaxType(ObservableCollection<MyBills> BillsToProcss)
        {

            await Task.Run(() =>
            {
                IsLoading = true;
            });


            switch (SelectedTaxTypeForFilter.StatementFilter)
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


        public async void FilterIfTypeAndStausFilterSelected(bool isTaxTypeFilter)
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });

            calculateMyBills = isTaxTypeFilter;

            if (MyBillsOriginal != null)
            {

                if (isTaxTypeFilter)
                {
                    MyBills = new ObservableCollection<MyBills>(MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 1) || x.Status == Enum.GetName(typeof(BillStatus), 2)).ToList());

                    FilterOnTaxType(MyBills);
                    return;
                }

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

                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            else
            {
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }

        }

        public async Task DoValidatePayment(string fbNum, string sdadNo, string paymentType)
        {
            try
            {
                try
                {

                    IsLoading = true;

                    var platform = "";

                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        platform = "C4";
                    }
                    else if (Device.RuntimePlatform == Device.Android)
                    {
                        platform = "C3";
                    }
                    //PaymentData = await WebServiceManager.GAZTValidatePayment(fbNum, App.LoginDataRetrieved.TIN, platform);


                    ValidatePayment modelDetails = new ValidatePayment();
                    modelDetails.Fbnum = fbNum;
                    modelDetails.Pymntty = paymentType;
                    modelDetails.Tin = App.LoginDataRetrieved.TIN;
                    modelDetails.Srcid = platform;
                    modelDetails.Srctile = "53";
                    modelDetails.Sadad = sdadNo;

                    PaymentData = await WebServiceManager.GAZTValidatePayment(modelDetails);

                    if (PaymentData.d.Guid != null && PaymentData.d.Guid == "")
                    {
                        await PopupNavigation.Instance.PushAsync(new PaymentExceptionPageView());
                        return;
                    }

                    if (PaymentData != null && PaymentData.d != null)
                    {

                        if (PaymentData.d.Guid != null)
                        {

                            App.PaymentGuid = PaymentData.d.Guid;

                        }

                        if (paymentType == "M")
                        {

                            MainThread.BeginInvokeOnMainThread(async () =>
                            {

                                _navigationService.NavigateTo(App.PaymentProcessWebview, 2);
                                //await App.Current.MainPage.Navigation.PushAsync(new PaymentProcessWebview());

                            });
                        }
                        else
                        {

                            ApplePayStatus = await ProcessApplePay();
                        }


                        // var VatAmount = NetdueVat.Replace(",", "");
                        //if (String.IsNullOrEmpty(amount) || Double.Parse(amount) == 0)
                        //{
                        //    await PopupNavigation.Instance.PushAsync(new PaymentOptionsPageView(true, true, false, string.Empty));
                        //}
                        //else if (!String.IsNullOrEmpty(amount) && Double.Parse(amount) > 20000)
                        //{
                        //    await PopupNavigation.Instance.PushAsync(new PaymentOptionsPageView(true, false, true, string.Empty));
                        //}
                        //else
                        //{
                        //    await PopupNavigation.Instance.PushAsync(new PaymentOptionsPageView(true, false, false, string.Empty));
                        //}

                    }

                    IsLoading = false;

                }
                catch (GAZTValidatePaymentInProcessException ex)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        IsLoading = false;
                        await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        //_navigationService.GoBack();
                    });
                }
                catch (InternetException ex)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        IsLoading = false;
                        //   await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                        _navigationService.GoBack();
                    });
                }
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                    _navigationService.GoBack();
                });
            }

            catch (GAZTNetworkConnectivityIssueException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));

                });
            }
        }

        public async Task UpdateApplePayPaymentGuid()
        {
            try
            {
                try
                {

                    IsLoading = true;

                    var platform = string.Empty;

                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        platform = "C4";
                    }
                    else if (Device.RuntimePlatform == Device.Android)
                    {
                        platform = "C3";
                    }



                    ApplePayToken modelDetails = new ApplePayToken();
                    modelDetails.Guid = App.PaymentGuid;
                    //var token = "GrPRb/eyYkhLaxIi8ugsU5I0D2/IE6JT6SYb4o6CH/emQV7n5twiqt8IVazkcItvmCkHXeie16Nvbq+uFFx0mS4O/1+SoDHrP8HcDbJ/Q1swCCHR/Dwv69oTcTUy1riK6Zvpe0w1r+WJ21I36gorRUn7u94Yi9n4afOfnGJC3EmFd6DKSIRQWlT4BuLlNv5826XruanuFjdL3MKty/xoCyx2GKN+e8W6BFVnQc/gsBe4UW7oqHIQ5PrQJlQwymi5Ytd1IIJT8QsUMxiVjz6yVS5zdQBaN86ZtuokJRmC89jCwVkUMwDl9jQ5xYbFlIFS1VXKJjtWKDfMGwCWK3jvWdtCcdb4VrPIxtK7LvTWc+4C7m6SPzkOhdC/XPn7ufwvrh95no7p9tpQMkP7zOJIYAl+hS4oEqvOxdpw55dCytGXJ0yjN/HOQ3t4ofyW9mBGiHoq";
                    modelDetails.PaymentToken = ApplePayTokenData;
                    modelDetails.SrcId = platform;



                    ApplePayTokenResponse response = await WebServiceManager.GAZTUpdateApplePayGuid(modelDetails);


                    if (response != null && response.d != null)
                    {

                        if (response.d.Success)
                        {

                            PaymentSucess paymentInfo = new PaymentSucess();
                            paymentInfo.Paymentref = response.d.PayRef;
                            if (response.d.PerslTxt != null)
                            {
                                paymentInfo.Period = response.d.PerslTxt;
                            }

                            _navigationService.NavigateTo(App.MyBillsSuccessPageView, paymentInfo);

                            //_navigationService.GoBack();
                        }
                        else
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                //await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                                //_navigationService.GoBack();

                                await PopupNavigation.Instance.PushAsync(new PaymentExceptionPageView());
                            });

                        }

                    }

                    IsLoading = false;

                }
                catch (GAZTValidatePaymentInProcessException ex)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        IsLoading = false;
                        await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                        _navigationService.GoBack();
                    });
                }
                catch (InternetException ex)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        IsLoading = false;
                        //   await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                        _navigationService.GoBack();
                    });
                }
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;
                    //await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    await PopupNavigation.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZSomethingwentwrong));
                    _navigationService.GoBack();
                });
            }
        }

        private async Task<bool> ProcessApplePay()
        {

            var Amount = Convert.ToDouble(PaymentData.d.Amount);
            var BillAmount = Math.Round(Amount, 2);
            DependencyService.Get<IApplePayAuthorizer>().IsPaymentFromDashboard(false);
            return DependencyService.Get<IApplePayAuthorizer>().AuthorizePayment(BillAmount, AppResources.ApplePayText);
        }

        public void MadaPaymentSelected()
        {
            DoValidatePayment(selectedFbNum, selectedSadadNo, "M");

        }

        public async Task ApplePaySelected()
        {
            DoValidatePayment(fbNum: selectedFbNum, selectedSadadNo, "A");

        }

        public async Task SadadPaymentSelected()
        {
            //_navigationService.NavigateTo(App.MyBillsSuccessPageView, selectedSadadNo);
            _navigationService.NavigateTo(App.MyBillsSadadDetailsPageView, 1);

            //_navigationService.GoBack();

        }
    }
}
