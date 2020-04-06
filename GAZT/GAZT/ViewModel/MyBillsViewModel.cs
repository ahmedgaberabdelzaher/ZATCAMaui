using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GAZT.ViewModel
{
    public class MyBillsViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand onAllLabelClicked { get; set; }
        public ICommand onPaidLabelClicked { get; set; }
        public ICommand onUnpaidLabelClicked { get; set; }
        public ICommand onPartiallyPaidLabelClicked { get; set; }
        public ICommand OnHomeIconClicked { get; set; }

        public ICommand OnHomeButtonClicked { get; set; }

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
        public ObservableCollection<MyBillsChartModel> ListMyBillsChaetModel
        {
            get
            {
                return _listMyBillsChaetModel;
            }
            set
            {
                _listMyBillsChaetModel = value;
                RaisePropertyChanged("ListMyBillsChaetModel");
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
                RaisePropertyChanged("MyBills");
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


        public MyBillsViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            _navigationService = navigationService;
            _dialogService = dialogService;

            OnHomeIconClicked = new Command(() =>
            {
                _navigationService.NavigateTo(App.SFLandingPageView);
            });
            OnHomeButtonClicked = new Xamarin.Forms.Command(() =>
            {
                _navigationService.NavigateTo(App.SFLandingPageView);
            });

            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            try
            {
                //onAllLabelClicked = new Command(() =>
                //{
                //    OnAllLabelClicked();
                //});

                //onPaidLabelClicked = new Command(() =>
                //{
                //    OnPaidClick();
                //});
                //onUnpaidLabelClicked = new Command(() =>
                //{
                //    OnUnpaidClick();
                //});
                //onPartiallyPaidLabelClicked = new Command(() =>
                //{
                //    OnPartiallyClicked();
                //});

            }
            catch (Exception e)
            {

                //Device.BeginInvokeOnMainThread(async () =>
                //{
                //    await _dialogService.ShowMessageBox(AppResources.NoBillsAvailable, AppResources.Information);
                //});
            }
        }


        public async Task onPageLoad(BillInfo billInfo)
        {

            //await Task.Run(() =>
            //{
            //    IsLoading = true;
            //});

            //await Task.Run(async () =>
            //{

            MyBills = null;

            ObservableCollection<MyBills> myBills = null;
            try
            {
                try
                {

                    string lang = UtilityManager.GetLanguageParameter();
                    myBills = await WebServiceManager.GAZTGetMyBills(App.TP.Tin, lang);
                    await PopToRootPage();// If seesion Expired it will navigate to Dashboard page


                    if (myBills != null && myBills.Count != 0)
                    {
                        myBills = UpdateDueAmount(myBills);
                        MyBills = new ObservableCollection<MyBills>();

                        MyBills = myBills;
                        MyBillsOriginal = myBills;
                        MyBillsPaid = MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 0)).ToList();
                        MyBillsUnPaid = MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 2)).ToList();
                        MyBillsPartiallyPaid = MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 1)).ToList();

                        ObservableCollection<MyBillsChartModel> myBillsChartModels = new ObservableCollection<MyBillsChartModel>();
                        GroupValue = MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 0)).ToList().Count + MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 2)).ToList().Count + MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 1)).ToList().Count;
                        myBillsChartModels.Add(new MyBillsChartModel { BillCount = MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 0)).ToList().Count, BillType = AppResources.Paid, BillColor = Xamarin.Forms.Color.FromHex("#006450") });
                        myBillsChartModels.Add(new MyBillsChartModel { BillCount = MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 2)).ToList().Count, BillType = AppResources.UnPaid, BillColor = Xamarin.Forms.Color.FromHex("#944E23") });
                        myBillsChartModels.Add(new MyBillsChartModel { BillCount = MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 1)).ToList().Count, BillType = AppResources.PartiallyPaid, BillColor = Xamarin.Forms.Color.FromHex("#F36C21") });
                        ListMyBillsChaetModel = myBillsChartModels;

                        SelcectedBillsIndex = 0;
                        int milliseconds = 5000;
                        Thread.Sleep(milliseconds);
                        if (billInfo != null)
                        {
                            if (billInfo.BillTypeName == AppResources.Paid)
                            {
                                //OnPaidClick();
                                SelcectedBillsIndex = 1;
                            }
                            else if (billInfo.BillTypeName == AppResources.UnPaid)
                            {
                                //OnPartiallyClicked();
                                SelcectedBillsIndex = 2;
                            }
                            else if (billInfo.BillTypeName == AppResources.PartiallyPaid)
                            {
                                // OnUnpaidClick();
                                SelcectedBillsIndex = 3;
                            }

                        }
                        //else
                        //{
                        //    //  _dialogService.ShowMessageBox(AppResources.NoBillsAvailable, AppResources.Information);
                        //    // _navigationService.GoBack();
                        //    //   await Task.Run(() =>
                        //    //  {
                        //    IsLoading = false;
                        //    //  });
                        //    MyBills = null;
                        //    SetNoDataLabelVisibility = true;
                        //    MyBillsPaid = null;
                        //    MyBillsUnPaid = null;
                        //    MyBillsPartiallyPaid = null;

                        //}
                        //  SelcectedBillsIndex = 0;
                    }
                }
                catch (Exception e)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {

                        _dialogService.ShowMessageBox(e.Message, AppResources.Information);
                        _navigationService.GoBack();
                    });
                    //  await Task.Run(() =>
                    //   {
                    IsLoading = false;
                    //   });

                }
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {

                    _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
                //   await Task.Run(() =>
                //   {
                IsLoading = false;
                //  });
            }
            //});

            //await Task.Run(() =>
            //{
            //    IsLoading = false;
            //});


            //int i = 5;
            //MyBills = new List<MyBills>();
            //for (i = 0; i < 6; i++)
            //{
            //    MyBills m = new MyBills();
            //    m.Abtypt = "Abc";
            //    m.BETRW = "100";
            //    m.FAEDN = "12:02:20";
            //    m.Status = "P";
            //    m.VTRE2 = "54321";

            //    MyBills.Add(m);
            //}

            //int j = 5;
            ////S MyBills = new List<MyBills>();
            //for (i = 0; i < 6; i++)
            //{
            //    MyBills m = new MyBills();
            //    m.Abtypt = "Abc";
            //    m.BETRW = "100";
            //    m.FAEDN = "12:02:20";
            //    m.Status = "I";
            //    m.VTRE2 = "54321";

            //    MyBills.Add(m);
            //}
            //MyBillsOriginal = MyBills;

        }

        public async Task PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    await _navigation.PopToRootAsync();
                });
            }
        }

        private ObservableCollection<MyBills> UpdateDueAmount(ObservableCollection<MyBills> myBills)
        {
            // List<MyBills> list = new List<MyBills>();


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

                if (myBills[i].FAEDN.Contains("T"))
                {
                    string[] _dueDate = new String[2];
                    _dueDate = myBills[i].FAEDN.Split('T');
                    if (App.IsArabic)
                    {
                        myBills[i].FAEDN = Convert.ToDateTime(_dueDate[0]).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                        myBills[i].FAEDN = UtilityManager.ToArabicDate(myBills[i].FAEDN);

                    }
                    else
                    {
                        myBills[i].FAEDN = Convert.ToDateTime(_dueDate[0]).ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                    }
                }

            }
            return myBills;
        }

        //   public void OnAllLabelClicked()
        //    {
        //        SelcectedBillsIndex = 0;
        //        // IsUnderlineForAll = "None";
        //        MyBills = null;
        //        if (MyBillsOriginal != null)
        //        {
        //            if (MyBillsOriginal.Count != 0)
        //            {
        //                List<MyBills> myBills = new List<MyBills>();
        //                myBills = MyBillsOriginal.ToList();
        //                if (myBills != null && myBills.Count > 0)
        //                {
        //                    MyBills = myBills;
        //                    SetNoDataLabelVisibility = false;
        //                }
        //                else
        //                {
        //                    MyBills = null;
        //                    //Device.BeginInvokeOnMainThread(async () =>
        //                    //{
        //                    //    await _dialogService.ShowMessageBox(AppResources.NoBillsAvailable, AppResources.Information);
        //                    //});
        //                    SetNoDataLabelVisibility = true;
        //                }
        //            }
        //            else
        //            {
        //                MyBills = null;
        //                SetNoDataLabelVisibility = true;
        //                //Device.BeginInvokeOnMainThread(async () =>
        //                //{
        //                //    await _dialogService.ShowMessageBox(AppResources.NoBillsAvailable, AppResources.Information);
        //                //});
        //            }
        //        }
        //        else
        //        {
        //            SetNoDataLabelVisibility = true;
        //            MyBills = null;
        //        }
        //    }

        //    public void OnPaidClick()
        //    {
        //        SelcectedBillsIndex = 1;
        //        MyBills = null;
        //        if (MyBillsOriginal != null)
        //        {
        //            if (MyBillsOriginal.Count != 0)
        //            {
        //                List<MyBills> myBills = new List<MyBills>();
        //                myBills = MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 0)).ToList();
        //                if (myBills != null && myBills.Count > 0)
        //                {
        //                    MyBills = myBills;
        //                    SetNoDataLabelVisibility = false;
        //                }
        //                else
        //                {
        //                    MyBills = null;
        //                    SetNoDataLabelVisibility = true;
        //                    //Device.BeginInvokeOnMainThread(async () =>
        //                    //{
        //                    //    await _dialogService.ShowMessageBox(AppResources.NoBillsAvailable, AppResources.Information);
        //                    //});
        //                }
        //            }
        //            else
        //            {
        //                SetNoDataLabelVisibility = true;
        //                MyBills = null;
        //                //Device.BeginInvokeOnMainThread(async () =>
        //                //{
        //                //    await _dialogService.ShowMessageBox(AppResources.NoBillsAvailable, AppResources.Information);
        //                //});
        //            }
        //        }
        //        else
        //        {
        //            SetNoDataLabelVisibility = true;
        //            MyBills = null;
        //        }
        //    }

        //    public void OnUnpaidClick()
        //    {
        //        SelcectedBillsIndex = 2;
        //        MyBills = null;
        //        if (MyBillsOriginal != null)
        //        {
        //            if (MyBillsOriginal.Count != 0)
        //            {
        //                List<MyBills> myBills = new List<MyBills>();
        //                myBills = MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 2)).ToList();
        //                if (myBills != null && myBills.Count > 0)
        //                {
        //                    MyBills = myBills;
        //                    SetNoDataLabelVisibility = false;
        //                }
        //                else
        //                {
        //                    MyBills = null;
        //                    SetNoDataLabelVisibility = true;
        //                    //Device.BeginInvokeOnMainThread(async () =>
        //                    //{
        //                    //    await _dialogService.ShowMessageBox(AppResources.NoBillsAvailable, AppResources.Information);
        //                    //});
        //                }
        //            }
        //            else
        //            {
        //                MyBills = null;
        //                SetNoDataLabelVisibility = true;
        //                //Device.BeginInvokeOnMainThread(async () =>
        //                //{
        //                //    await _dialogService.ShowMessageBox(AppResources.NoBillsAvailable, AppResources.Information);
        //                //});
        //            }
        //        }
        //        else
        //        {
        //            SetNoDataLabelVisibility = true;
        //            MyBills = null;
        //        }
        //    }

        //    public void OnPartiallyClicked()
        //    {
        //        SelcectedBillsIndex = 3;
        //        MyBills = null;
        //        if (MyBillsOriginal != null)
        //        {
        //            if (MyBillsOriginal.Count != 0)
        //            {
        //                List<MyBills> myBills = new List<MyBills>();
        //                myBills = MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 1)).ToList();
        //                if (myBills != null && myBills.Count > 0)
        //                {
        //                    MyBills = myBills;
        //                    SetNoDataLabelVisibility = false;
        //                }
        //                else
        //                {
        //                    MyBills = null;
        //                    SetNoDataLabelVisibility = true;
        //                    //Device.BeginInvokeOnMainThread(async () =>
        //                    //{
        //                    //    await _dialogService.ShowMessageBox(AppResources.NoBillsAvailable, AppResources.Information);
        //                    //});
        //                }

        //            }
        //            else
        //            {
        //                MyBills = null;
        //                SetNoDataLabelVisibility = true;
        //                //Device.BeginInvokeOnMainThread(async () =>
        //                //{
        //                //    await _dialogService.ShowMessageBox(AppResources.NoBillsAvailable, AppResources.Information);
        //                //});
        //            }
        //        }
        //        else
        //        {
        //            SetNoDataLabelVisibility = true;
        //            MyBills = null;
        //        }
        //    }
        //}
    }
}
