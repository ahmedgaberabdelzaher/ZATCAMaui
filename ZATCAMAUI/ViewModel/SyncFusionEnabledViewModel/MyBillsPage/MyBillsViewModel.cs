

using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using ZATCAMAUI.Core.Enums;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.SyncfusionEnabledModels;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.MyBillsPage
{

    public class MyBillsViewModel : BaseViewModel
    {
        public ICommand onAllLabelClicked { get; set; }
        public ICommand onPaidLabelClicked { get; set; }
        public ICommand onUnpaidLabelClicked { get; set; }
        public ICommand onPartiallyPaidLabelClicked { get; set; }
        public ICommand OnHomeIconClicked { get; set; }
        public ICommand OnHomeButtonClicked { get; set; }
        private ChartColorCollection _colors = null;
        public ChartColorCollection Colors
        {
            get
            {
                return _colors;
            }
            set
            {
                if (_colors == value) return;
                _colors = value;
                OnPropertyChanged("Colors");
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

                if (_setNoDataLabelVisibilityALLList == value) return;
                _setNoDataLabelVisibilityALLList = value;
                OnPropertyChanged("SetNoDataLabelVisibilityALLList");
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
                if (_setNoDataLabelVisibilityALL == value) return;

                _setNoDataLabelVisibilityALL = value;
                OnPropertyChanged("SetNoDataLabelVisibilityALL");
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
                if (_setNoDataLabelVisibilityPAID == value) return;

                _setNoDataLabelVisibilityPAID = value;
                OnPropertyChanged("SetNoDataLabelVisibilityPAID");
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
                if (_setNoDataLabelVisibilityPAIDList == value) return;

                _setNoDataLabelVisibilityPAIDList = value;
                OnPropertyChanged("SetNoDataLabelVisibilityPAIDList");
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
                OnPropertyChanged("SetNoDataLabelVisibilityUNPAIDList");
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
                OnPropertyChanged("SetNoDataLabelVisibilityUNPAID");
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
                OnPropertyChanged("SetNoDataLabelVisibilityPPAID");
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
                OnPropertyChanged("SetNoDataLabelVisibilityPPAIDList");
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
                OnPropertyChanged("GroupValue");
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
                OnPropertyChanged("ListMyBillsChartModel");
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
                OnPropertyChanged("MyBillsObj");
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
                OnPropertyChanged("MyBills");
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
                OnPropertyChanged("MyBillsPaid");
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
                OnPropertyChanged("MyBillsUnPaid");
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
                OnPropertyChanged("MyBillsPartiallyPaid");
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
                OnPropertyChanged("SetNoDataLabelVisibility");
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
                OnPropertyChanged("MyBillsOriginal");
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
                OnPropertyChanged("IsUnderlineForAll");
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
                OnPropertyChanged("IsUnderlineForPartiallyPaid");
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
                OnPropertyChanged("IsUnderlineForPaid");
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
                OnPropertyChanged("SelcectedBillsIndex");
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
                OnPropertyChanged("IsUnderlineForUnPaid");
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
                OnPropertyChanged("StatusImage");
            }
        }
        public MyBillsViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            OnHomeIconClicked = new Command(() =>
            {
                _navigationService.NavigateTo(App.SFLandingPageView);
            });
            OnHomeButtonClicked = new Command(() =>
            {
                _navigationService.NavigateTo(App.SFLandingPageView);
            });
        }
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

                    var tell = WebServiceManager.GetUserBills(App.TP.TIN, lang);
                    PopToRootPage();// If seesion Expired it will navigate to Dashboard page

                    if (myBills != null && myBills.Count != 0)
                    {
                        SetNoDataLabelVisibilityALL = false;
                        SetNoDataLabelVisibilityALLList = true;

                        myBills = UpdateDueAmount(myBills);

                        MyBills = new ObservableCollection<MyBills>();
                        MyBills = myBills;
                        MyBillsOriginal = myBills;
                        MyBillsPaid = MyBillsOriginal.Where(x => x.Status == "Paid").ToList();
                        MyBillsUnPaid = MyBillsOriginal.Where(x => x.Status == "Open").ToList();
                        MyBillsPartiallyPaid = MyBillsOriginal.Where(x => x.Status == "Partially Paid").ToList();
                        ObservableCollection<MyBillsChartModel> myBillsChartModels = new ObservableCollection<MyBillsChartModel>();
                        GroupValue = MyBillsOriginal.Where(x => x.Status == "Paid").ToList().Count + MyBillsOriginal.Where(x => x.Status == "Open").ToList().Count + MyBillsOriginal.Where(x => x.Status == "Partially Paid").ToList().Count;
                        int iBillsCount = -1;
                        if ((iBillsCount = MyBillsOriginal.Where(x => x.Status == "Paid").ToList().Count) > 0)
                        {
                            myBillsChartModels.Add(new MyBillsChartModel { BillCount = iBillsCount, BillType = AppResources.Paid, BillColor = (Color)Application.Current.Resources["Primary"] });
                            ColorsChild.Add(Color.FromRgb(0, 100, 80));
                            SetNoDataLabelVisibilityPAID = false;
                            SetNoDataLabelVisibilityPAIDList = true;
                        }
                        else
                        {
                            // myBillsChartModels.Add(new MyBillsChartModel { BillCount = iBillsCount, BillType = AppResources.Paid, BillColor = Xamarin.Forms. (Color)Application.Current.Resources["Primary"] });
                            SetNoDataLabelVisibilityPAID = true;
                            SetNoDataLabelVisibilityPAIDList = false;
                        }
                        if ((iBillsCount = MyBillsOriginal.Where(x => x.Status == "Open").ToList().Count) > 0)
                        {
                            myBillsChartModels.Add(new MyBillsChartModel { BillCount = iBillsCount, BillType = AppResources.UnPaid, BillColor = (Color)Application.Current.Resources["ErrorColor"] });
                            ColorsChild.Add(Color.FromRgb(170, 12, 25));
                            SetNoDataLabelVisibilityUNPAID = false;
                            SetNoDataLabelVisibilityUNPAIDList = true;
                        }
                        else
                        {
                            SetNoDataLabelVisibilityUNPAID = true;
                            SetNoDataLabelVisibilityUNPAIDList = false;
                        }
                        if ((iBillsCount = MyBillsOriginal.Where(x => x.Status == "Partially Paid").ToList().Count) > 0)
                        {
                            myBillsChartModels.Add(new MyBillsChartModel { BillCount = iBillsCount, BillType = AppResources.PartiallyPaid, BillColor = (Color)Application.Current.Resources["Secondary"] });
                            //ColorsChild.Add(System.Drawing.Color.FromArgb(243, 108, 33));
                            ColorsChild.Add(Color.FromRgb(217, 154, 41));
                            SetNoDataLabelVisibilityPPAID = false;
                            SetNoDataLabelVisibilityPPAIDList = true;
                        }
                        else
                        {
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

                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessageBox(e.Message, AppResources.Information);
                        _navigationService.GoBack();
                    });
                    IsLoading = false;
                }
            }
            catch (InternetException ex)
            {

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
                IsLoading = false;
            }
            IsLoading = false;
        }
        public async Task PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
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
        private ObservableCollection<MyBills> UpdateDueAmount(ObservableCollection<MyBills> myBills)
        {
            for (int i = 0; i < myBills.Count; i++)
            {
                if (myBills[i].TestDueAmount.Contains("."))
                {
                    string[] Amount = new string[2];
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
                    string[] _dueDateTemp = new string[2];

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
                        string[] _dueDate = new string[2];
                        _dueDate = myBills[i].Faednar.Split('T');

                        myBills[i].Faednar = Convert.ToDateTime(_dueDate[0]).ToString("dd/MM/yyyy", new CultureInfo("en-US"));

                        string dt = string.Empty;
                        string[] dts = null;

                        dts = myBills[i].Faednar.Split('/');
                        dt = dts[0] + "-" + UtilityManager.GetMonthName(dts[1]) + "-" + dts[2];
                        myBills[i].Faednar = dt;
                    }
                    catch (Exception)
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
    }
}
