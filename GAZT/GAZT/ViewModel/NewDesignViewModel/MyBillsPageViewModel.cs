using EGAZT.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
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
        public ICommand OnVerifyButtonClicked { get; set; }
        public ICommand OnBackButtonClicked { get; set; }
        #region Property
        public List<ReturnTypes> _returnTypeForFilter=null;
        public List<ReturnTypes> ReturnTypeForFilter
        {
            get
            {
                return _returnTypeForFilter;
            }
            set
            {
                _returnTypeForFilter = value;
                RaisePropertyChanged("ReturnTypeForFilter");
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
        public ReturnTypes _selectedReturnTypeForFilter=null;
        public ReturnTypes SelectedReturnTypeForFilter
        {
            get
            {
                return _selectedReturnTypeForFilter;
            }
            set
            {
                _selectedReturnTypeForFilter = value;
                if (_selectedReturnTypeForFilter != null)
                {
                    FilterLabelText = _selectedReturnTypeForFilter.TaxType;

                    if (_selectedChipFilterItem != null)
                    {
                        FilterIfTypeAndStausFilterSelected();
                    }
                    else
                    {
                        FilterOnTaxType();
                    }
                        
                }
                RaisePropertyChanged("SelectedReturnTypeForFilter");
            }
        }     //FilterLabelText
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
        //SelectedReturnTypeForFilter
        private ChartColorCollection _colors = null;
        public ChartColorCollection Colors
        {
            get
            {
                return _colors;
            }
            set
            {
                _colors = value;
                RaisePropertyChanged("Colors");
            }
        }
        private bool _setNoDataLabelVisibilityALLList = true;
        public bool SetNoDataLabelVisibilityALLList
        {
            get
            {
                return _setNoDataLabelVisibilityALLList;
            }
            set
            {
                _setNoDataLabelVisibilityALLList = value;
                RaisePropertyChanged("SetNoDataLabelVisibilityALLList");
            }
        }
        private bool _setNoDataLabelVisibilityALL = true;
        public bool SetNoDataLabelVisibilityALL
        {
            get
            {
                return _setNoDataLabelVisibilityALL;
            }
            set
            {
                _setNoDataLabelVisibilityALL = value;
                RaisePropertyChanged("SetNoDataLabelVisibilityALL");
            }
        }
        private bool _setNoDataLabelVisibilityPAID = true;
        public bool SetNoDataLabelVisibilityPAID
        {
            get
            {
                return _setNoDataLabelVisibilityPAID;
            }
            set
            {
                _setNoDataLabelVisibilityPAID = value;
                RaisePropertyChanged("SetNoDataLabelVisibilityPAID");
            }
        }
        private bool _setNoDataLabelVisibilityPAIDList = true;
        public bool SetNoDataLabelVisibilityPAIDList
        {
            get
            {
                return _setNoDataLabelVisibilityPAIDList;
            }
            set
            {
                _setNoDataLabelVisibilityPAIDList = value;
                RaisePropertyChanged("SetNoDataLabelVisibilityPAIDList");
            }
        }
        private bool _setNoDataLabelVisibilityUNPAIDList = true;
        public bool SetNoDataLabelVisibilityUNPAIDList
        {
            get
            {
                return _setNoDataLabelVisibilityUNPAIDList;
            }
            set
            {
                _setNoDataLabelVisibilityUNPAIDList = value;
                RaisePropertyChanged("SetNoDataLabelVisibilityUNPAIDList");
            }
        }
        private bool _setNoDataLabelVisibilityUNPAID = true;
        public bool SetNoDataLabelVisibilityUNPAID
        {
            get
            {
                return _setNoDataLabelVisibilityUNPAID;
            }
            set
            {
                _setNoDataLabelVisibilityUNPAID = value;
                RaisePropertyChanged("SetNoDataLabelVisibilityUNPAID");
            }
        }
        private bool _setNoDataLabelVisibilityPPAID = true;
        public bool SetNoDataLabelVisibilityPPAID
        {
            get
            {
                return _setNoDataLabelVisibilityPPAID;
            }
            set
            {
                _setNoDataLabelVisibilityPPAID = value;
                RaisePropertyChanged("SetNoDataLabelVisibilityPPAID");
            }
        }
        private bool _setNoDataLabelVisibilityPPAIDList = true;
        public bool SetNoDataLabelVisibilityPPAIDList
        {
            get
            {
                return _setNoDataLabelVisibilityPPAIDList;
            }
            set
            {
                _setNoDataLabelVisibilityPPAIDList = value;
                RaisePropertyChanged("SetNoDataLabelVisibilityPPAIDList");
            }
        }
        private int _groupValue = 0;
        public int GroupValue
        {
            get
            {
                return _groupValue;
            }
            set
            {
                _groupValue = value;
                RaisePropertyChanged("GroupValue");
            }
        }
        private ObservableCollection<MyBillsChartModel> _listMyBillsChaetModel = null;
        public ObservableCollection<MyBillsChartModel> ListMyBillsChartModel
        {
            get
            {
                return _listMyBillsChaetModel;
            }
            set
            {
                _listMyBillsChaetModel = value;
                RaisePropertyChanged("ListMyBillsChartModel");
            }
        }
        private MyBills _myBillsObj;
        public MyBills MyBillsObj
        {
            get
            {
                return _myBillsObj;
            }
            set
            {
                _myBillsObj = value;
                RaisePropertyChanged("MyBillsObj");
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
                            if (item.TestDueAmount != null)
                            {
                                Amount = Amount + Convert.ToDouble(item.TestDueAmount);
                            }
                        }
                        //\\AmountLabel = Math.Round(count, 2).ToString();
                        AmountLabel = Amount.ToString();
                    }
                    else
                    {
                        AmountLabel = string.Empty;
                    }

                }
                    // _listToDisplay.Sum(x => x.)
                    RaisePropertyChanged("MyBills");
            }
        }
        private string _amountLabel=string.Empty;
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
        private List<MyBills> _myBillsPaid;
        public List<MyBills> MyBillsPaid
        {
            get
            {
                return _myBillsPaid;
            }
            set
            {
                _myBillsPaid = value;
                RaisePropertyChanged("MyBillsPaid");
            }
        }
        private List<MyBills> _myBillsUnPaid;
        public List<MyBills> MyBillsUnPaid
        {
            get
            {
                return _myBillsUnPaid;
            }
            set
            {
                _myBillsUnPaid = value;
                RaisePropertyChanged("MyBillsUnPaid");
            }
        }
        private List<MyBills> _myBillsPartiallyPaid;
        public List<MyBills> MyBillsPartiallyPaid
        {
            get
            {
                return _myBillsPartiallyPaid;
            }
            set
            {
                _myBillsPartiallyPaid = value;
                RaisePropertyChanged("MyBillsPartiallyPaid");
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
        private bool _setNoDataLabelVisibility = false;
        public bool SetNoDataLabelVisibility
        {
            get
            {
                return _setNoDataLabelVisibility;
            }
            set
            {
                _setNoDataLabelVisibility = value;
                RaisePropertyChanged("SetNoDataLabelVisibility");
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
        private string _isUnderlineForAll = "Underline";
        public string IsUnderlineForAll
        {
            get
            {
                return _isUnderlineForAll;
            }
            set
            {
                _isUnderlineForAll = value;
                RaisePropertyChanged("IsUnderlineForAll");
            }
        }
        private string _isUnderlineForPartiallyPaid = "None";
        public string IsUnderlineForPartiallyPaid
        {
            get
            {
                return _isUnderlineForPartiallyPaid;
            }
            set
            {
                _isUnderlineForPartiallyPaid = value;
                RaisePropertyChanged("IsUnderlineForPartiallyPaid");
            }
        }
        private string _isUnderlineForPaid = "None";
        public string IsUnderlineForPaid
        {
            get
            {
                return _isUnderlineForPaid;
            }
            set
            {
                _isUnderlineForPaid = value;
                RaisePropertyChanged("IsUnderlineForPaid");
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
        private string _isUnderlineForUnPaid = "None";
        public string IsUnderlineForUnPaid
        {
            get
            {
                return _isUnderlineForUnPaid;
            }
            set
            {
                _isUnderlineForUnPaid = value;
                RaisePropertyChanged("IsUnderlineForUnPaid");
            }
        }
        private string _statusImage;
        public string StatusImage
        {
            get
            {
                return _statusImage;
            }
            set
            {
                _statusImage = value;
                RaisePropertyChanged("StatusImage");
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
            ListMyBillsChartModel = null;
            Colors = null;
            ObservableCollection<MyBills> myBills = null;
            ChartColorCollection ColorsChild = new ChartColorCollection();
            try
            {
                try
                {
                    string lang = UtilityManager.GetLanguageParameter();
                    myBills = WebServiceManager.GAZTGetMyBills(App.TP.Tin, lang);

                    PopToRootPage();// If seesion Expired it will navigate to Dashboard page

                    if (myBills != null && myBills.Count != 0)
                    {
                        SetNoDataLabelVisibilityALL = false;
                        SetNoDataLabelVisibilityALLList = true;

                     //   myBills = UpdateDueAmount(myBills);

                        MyBills = new ObservableCollection<MyBills>();
                        MyBills = myBills;
                        MyBillsOriginal = myBills;
                        MyBillsPaid = MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 0)).ToList();
                        MyBillsUnPaid = MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 2)).ToList();
                        MyBillsPartiallyPaid = MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 1)).ToList();
                        ObservableCollection<MyBillsChartModel> myBillsChartModels = new ObservableCollection<MyBillsChartModel>();
                        GroupValue = MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 0)).ToList().Count + MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 2)).ToList().Count + MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 1)).ToList().Count;
                        int iBillsCount = -1;
                        if ((iBillsCount = MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 0)).ToList().Count) > 0)
                        {
                            myBillsChartModels.Add(new MyBillsChartModel { BillCount = iBillsCount, BillType = AppResources.Paid, BillColor = Xamarin.Forms.Color.FromHex("#006450") });
                            ColorsChild.Add(System.Drawing.Color.FromArgb(0, 100, 80));
                            SetNoDataLabelVisibilityPAID = false;
                            SetNoDataLabelVisibilityPAIDList = true;
                        }
                        else
                        {
                            // myBillsChartModels.Add(new MyBillsChartModel { BillCount = iBillsCount, BillType = AppResources.Paid, BillColor = Xamarin.Forms.Color.FromHex("#006450") });
                            SetNoDataLabelVisibilityPAID = true;
                            SetNoDataLabelVisibilityPAIDList = false;
                        }
                        if ((iBillsCount = MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 2)).ToList().Count) > 0)
                        {
                            myBillsChartModels.Add(new MyBillsChartModel { BillCount = iBillsCount, BillType = AppResources.UnPaid, BillColor = Xamarin.Forms.Color.FromHex("#AA0C19") });
                            ColorsChild.Add(System.Drawing.Color.FromArgb(170, 12, 25));
                            SetNoDataLabelVisibilityUNPAID = false;
                            SetNoDataLabelVisibilityUNPAIDList = true;
                        }
                        else
                        {
                            //  myBillsChartModels.Add(new MyBillsChartModel { BillCount = iBillsCount, BillType = AppResources.UnPaid, BillColor = Xamarin.Forms.Color.FromHex("#AA0C19") });
                            SetNoDataLabelVisibilityUNPAID = true;
                            SetNoDataLabelVisibilityUNPAIDList = false;
                        }
                        if ((iBillsCount = MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 1)).ToList().Count) > 0)
                        {
                            myBillsChartModels.Add(new MyBillsChartModel { BillCount = iBillsCount, BillType = AppResources.PartiallyPaid, BillColor = Xamarin.Forms.Color.FromHex("#D99A29") });
                            //ColorsChild.Add(System.Drawing.Color.FromArgb(243, 108, 33));
                            ColorsChild.Add(System.Drawing.Color.FromArgb(217, 154, 41));
                            SetNoDataLabelVisibilityPPAID = false;
                            SetNoDataLabelVisibilityPPAIDList = true;
                        }
                        else
                        {
                            // myBillsChartModels.Add(new MyBillsChartModel { BillCount = iBillsCount, BillType = AppResources.PartiallyPaid, BillColor = Xamarin.Forms.Color.FromHex("#F36C21") });
                            SetNoDataLabelVisibilityPPAID = true;
                            SetNoDataLabelVisibilityPPAIDList = false;
                        }
                        Colors = ColorsChild;
                        ListMyBillsChartModel = myBillsChartModels;
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
                    }
                    else
                    {
                        SetNoDataLabelVisibilityALL = true;
                        SetNoDataLabelVisibilityALLList = false;
                        SetNoDataLabelVisibilityPAID = true;
                        SetNoDataLabelVisibilityPAIDList = false;
                        SetNoDataLabelVisibilityUNPAID = true;
                        SetNoDataLabelVisibilityUNPAIDList = false;
                        SetNoDataLabelVisibilityPPAID = true;
                        SetNoDataLabelVisibilityPPAIDList = false;
                    }
                }
                catch (Exception e)
                {

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(e.Message, AppResources.Information);
                        _navigationService.GoBack();
                    });
                    IsLoading = false;
                }
            }
            catch (InternetException ex)
            {

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
                IsLoading = false;
            }
            IsLoading = false;
        }
        public void FilterIfTypeAndStausFilterSelected() 
        {
            if (_selectedChipFilterItem.TemplateType.Equals("Paid"))
            {
                FilterOnTaxType();
                MyBills = new ObservableCollection<MyBills>(MyBills.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 0)).ToList());
            }
            if (_selectedChipFilterItem.TemplateType.Equals("Partial"))
            {
                FilterOnTaxType();
                MyBills = new ObservableCollection<MyBills>(MyBills.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 1)).ToList());
            }
            if (_selectedChipFilterItem.TemplateType.Equals("Unpaid"))
            {
                FilterOnTaxType();
                MyBills = new ObservableCollection<MyBills>(MyBills.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 2)).ToList());
            }
        }
        public void PopulateReturnTypeList()
        {
            try
            {
                List<ReturnTypes> ReturnTypesList = new List<ReturnTypes>{
           new ReturnTypes {Id = "00",TaxType = AppResources.All},
                      new ReturnTypes {Id = "01",TaxType = AppResources.ZZZAKAT},
                                            new ReturnTypes {Id = "02",TaxType = AppResources.ZZVAT},
                                            new ReturnTypes {Id = "03",TaxType = AppResources.ZZET},
                                            new ReturnTypes {Id = "04",TaxType = AppResources.WHTreturns},
            };
                ReturnTypeForFilter = new List<ReturnTypes>();
                ReturnTypeForFilter = ReturnTypesList;
                SelectedReturnTypeForFilter = ReturnTypeForFilter.FirstOrDefault();

            }
            catch (Exception ex)
            {

            }


        }
        public void PopulateDataInChips()
        {
            ChipDataFilterlist = new ObservableCollection<ChipModel>()
               {
                new ChipModel(){Text =AppResources.Paid, TemplateType = "Paid", ImageSource="ic_check_circle.png"},
                new ChipModel(){Text =AppResources.Partial, TemplateType = "Partial",ImageSource = "ic_loading.png"},
                new ChipModel(){Text =AppResources.UnPaid, TemplateType = "Unpaid",ImageSource = "ic_money.png"},
              
               };
        }
        private ObservableCollection<MyBills> UpdateDueAmount(ObservableCollection<MyBills> myBills)
        {
            for (int i = 0; i < myBills.Count; i++)
            {
                if (myBills[i].TestDueAmount.Contains("."))
                {
                    string[] Amount = new String[2];
                    Amount = myBills[i].TestDueAmount.Split('.');
                    double testDueAmount = Convert.ToDouble(Amount[0]);
                    string _testDueAmount = testDueAmount.ToString("#,##0");
                    _testDueAmount = _testDueAmount + "." + Amount[1];
                    myBills[i].TestDueAmount = _testDueAmount;
                }
                else
                {
                    double testDueAmount = Convert.ToDouble(myBills[i].TestDueAmount);
                    string _testDueAmount = testDueAmount.ToString("#,##0");
                    myBills[i].TestDueAmount = _testDueAmount;
                }

                //Caltype here tells if the date is Hijiri
                //Incase of the Hijri Date we are converting into Gregorian and displaying it to the user
                if (myBills[i].CalTyp == "H")
                {
                    string[] _dueDateTemp = new String[2];

                    if (myBills[i].Faednar.Contains("T"))
                    {
                        _dueDateTemp = myBills[i].Faednar.Split('T');
                        myBills[i].Faednar = Convert.ToDateTime(_dueDateTemp[0]).ToString("dd/MM/yyyy", new CultureInfo("en-us"));

                        CultureInfo cultureInfo = new CultureInfo("ar-SA");
                        DateTime dateStart = DateTime.ParseExact(myBills[i].Faednar, "dd/MM/yyyy", cultureInfo.DateTimeFormat, DateTimeStyles.AllowInnerWhite);

                        GregorianCalendar hjCalendar = new GregorianCalendar();
                        int year = hjCalendar.GetYear(dateStart);
                        int month = hjCalendar.GetMonth(dateStart);
                        int day = hjCalendar.GetDayOfMonth(dateStart);
                        string dateStr = string.Format("{0}/{1}/{2}", day, month, year);

                        myBills[i].Faednar = dateStr;

                        string dt = string.Empty;
                        string[] dts = null;

                        dts = myBills[i].Faednar.Split('/');
                        dt = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];
                        myBills[i].Faednar = dt;
                    }
                    else
                    {
                        //myBills[i].Faednar = Convert.ToDateTime(myBills[i].Faednar).ToString("dd/MM/yyyy", new CultureInfo("en-us"));
                        DateTime dateStart = new DateTime();


                        CultureInfo cultureInfo = new CultureInfo("ar-SA");

                        if (App.IsArabic)
                        {

                            dateStart = DateTime.ParseExact(myBills[i].Faednar, "yyyy/MM/dd", cultureInfo.DateTimeFormat, DateTimeStyles.AllowInnerWhite);
                        }
                        else
                        {

                            dateStart = DateTime.ParseExact(myBills[i].Faednar, "dd/MM/yyyy", cultureInfo.DateTimeFormat, DateTimeStyles.AllowInnerWhite);
                        }


                        GregorianCalendar hjCalendar = new GregorianCalendar();
                        int year = hjCalendar.GetYear(dateStart);
                        int month = hjCalendar.GetMonth(dateStart);
                        int day = hjCalendar.GetDayOfMonth(dateStart);

                        string dateStr = string.Format("{0:00}/{1}/{2}", day, month, year);

                        myBills[i].Faednar = dateStr;

                        string dt = string.Empty;
                        string[] dts = null;

                        dts = myBills[i].Faednar.Split('/');
                        dt = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];
                        myBills[i].Faednar = dt;


                    }
                }

                if (myBills[i].Faednar.Contains("T") && myBills[i].CalTyp == "G")
                {
                    try
                    {
                        string[] _dueDate = new String[2];
                        _dueDate = myBills[i].Faednar.Split('T');

                        myBills[i].Faednar = Convert.ToDateTime(_dueDate[0]).ToString("dd/MM/yyyy", new CultureInfo("en-US"));

                        string dt = string.Empty;
                        string[] dts = null;

                        dts = myBills[i].Faednar.Split('/');
                        dt = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];
                        myBills[i].Faednar = dt;
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else if (myBills[i].CalTyp == "G")
                {

                    DateTime dateStart = new DateTime();
                    CultureInfo cultureInfo = new CultureInfo("en-us");

                    if (App.IsArabic)
                    {

                        dateStart = DateTime.ParseExact(myBills[i].Faednar, "yyyy/MM/dd", cultureInfo.DateTimeFormat, DateTimeStyles.AllowInnerWhite);
                    }
                    else
                    {

                        dateStart = DateTime.ParseExact(myBills[i].Faednar, "dd/MM/yyyy", cultureInfo.DateTimeFormat, DateTimeStyles.AllowInnerWhite);
                    }

                    //myBills[i].Faednar = Convert.ToDateTime(myBills[i].Faednar).ToString("dd/MM/yyyy", new CultureInfo("en-US"));

                    GregorianCalendar hjCalendar = new GregorianCalendar();
                    int year = hjCalendar.GetYear(dateStart);
                    int month = hjCalendar.GetMonth(dateStart);
                    int day = hjCalendar.GetDayOfMonth(dateStart);
                    string dateStr = string.Format("{0:00}/{1}/{2}", day, month, year);


                    myBills[i].Faednar = dateStr;

                    string dt = string.Empty;
                    string[] dts = null;

                    dts = myBills[i].Faednar.Split('/');
                    dt = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];
                    myBills[i].Faednar = dt;
                }
            }
            return myBills;
        }
        public async Task PopToRootPage()
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
                        if (item.GetType().Name == App.SFAnonymousLandingPageView)
                        {
                            _navigation.RemovePage(item);
                            break;
                        }
                    }
                    _navigationService.NavigateTo(App.SFAnonymousLandingPageView);
                    _navigation.NavigationStack.ToList().Clear();
                });
            }
        }
        #endregion
        public void FilterOnTaxType()
        {
            if (_selectedReturnTypeForFilter.Id == "00")
            {

                MyBills = new ObservableCollection<MyBills>(MyBillsOriginal);
            }
            if (_selectedReturnTypeForFilter.Id == "01")
            {if (App.IsArabic)
                {
                    MyBills = new ObservableCollection<MyBills>(MyBillsOriginal.Where(x => x.Abtypt.Equals("الزكاة") || x.Abtypt.Equals("الزكاة")).ToList());
                    //MyBills = new ObservableCollection<MyBills>(MyBillsOriginal.Where(x => )).ToList());
                }
                else
                {
                    MyBills = new ObservableCollection<MyBills>(MyBillsOriginal.Where(x => x.Abtypt.Equals("Zakat")|| x.Abtypt.Equals("Voluntary Zakat")).ToList());
                   // MyBills = new ObservableCollection<MyBills>(MyBillsOriginal.Where(x => x.Abtypt.Equals("Voluntary Zakat")).ToList());
                }
              
            }
            if (_selectedReturnTypeForFilter.Id == "02")
            {
                if (App.IsArabic) 
                {
                    MyBills = new ObservableCollection<MyBills>(MyBillsOriginal.Where(x => x.Abtypt.Equals("ضريبة القيمة المضافة")|| x.Abtypt.Equals("ضريبة القيمة المضافة")).ToList());
                }
                else
                {
                    MyBills = new ObservableCollection<MyBills>(MyBillsOriginal.Where(x => x.Abtypt.Equals("VAT") || x.Abtypt.Equals("VAT Eligible Person")).ToList());
                }
            }
            if (_selectedReturnTypeForFilter.Id == "03")
            {
                if (!App.IsArabic)
                {
                    MyBills = new ObservableCollection<MyBills>(MyBillsOriginal.Where(x => x.Abtypt.Equals("Excise Tax") || x.Abtypt.Equals("ETAX")).ToList());
                }
                else
                {
                    MyBills = new ObservableCollection<MyBills>(MyBillsOriginal.Where(x => x.Abtypt.Equals("الضريبة الانتقائية")).ToList());
                }
            }
            if (_selectedReturnTypeForFilter.Id == "04")
            {
                if (!App.IsArabic)
                {
                    MyBills = new ObservableCollection<MyBills>(MyBillsOriginal.Where(x => x.Abtypt.Equals("Withholding Tax")).ToList());
                }
                else
                {
                    MyBills = new ObservableCollection<MyBills>(MyBillsOriginal.Where(x => x.Abtypt.Equals("ضريبة الاستقطاع") || x.Abtypt.Equals("Excise Tax")).ToList());
                }
            }
        }


    }
}
