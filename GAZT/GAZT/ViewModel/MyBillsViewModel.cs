using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private List<MyBills> _myBills;
        public List<MyBills> MyBills
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

        private List<MyBills> _myBillsOriginal;
        public List<MyBills> MyBillsOriginal
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
       
        private string _isUnderlineForAll= "Underline";
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
        private string _isUnderlineForPartiallyPaid="None";
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
        private string _isUnderlineForPaid="None";
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
        private string _isUnderlineForUnPaid="None";
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
                _navigationService.GoBack();
            });

            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            try
            {
                onAllLabelClicked = new Command(() =>
                {
                // IsUnderlineForAll = "None";
                if (MyBillsOriginal.Count != 0)
                    {
                        List<MyBills> myBills = new List<MyBills>();
                        myBills = MyBillsOriginal.ToList();
                        if (myBills != null && myBills.Count > 0)
                        {
                            MyBills = myBills;
                            SetNoDataLabelVisibility = false;
                        }
                        else
                        {
                            MyBills = null;
                            //Device.BeginInvokeOnMainThread(async () =>
                            //{
                            //    await _dialogService.ShowMessageBox(AppResources.NoBillsAvailable, AppResources.Information);
                            //});
                            SetNoDataLabelVisibility = true;
                        }
                    }
                else
                    {
                        MyBills = null;
                        SetNoDataLabelVisibility = true;
                        //Device.BeginInvokeOnMainThread(async () =>
                        //{
                        //    await _dialogService.ShowMessageBox(AppResources.NoBillsAvailable, AppResources.Information);
                        //});
                    }
                });
                onPaidLabelClicked = new Command(() =>
                {
                    if (MyBillsOriginal.Count != 0)
                    {
                        List<MyBills> myBills = new List<MyBills>();
                        myBills = MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 0)).ToList();
                        if (myBills != null && myBills.Count > 0)
                        {
                            MyBills = myBills;
                            SetNoDataLabelVisibility = false;
                        }
                        else
                        {
                            MyBills = null;
                            SetNoDataLabelVisibility = true;
                            //Device.BeginInvokeOnMainThread(async () =>
                            //{
                            //    await _dialogService.ShowMessageBox(AppResources.NoBillsAvailable, AppResources.Information);
                            //});
                        }
                    }
                    else
                    {
                        SetNoDataLabelVisibility = true;
                        MyBills = null;
                        //Device.BeginInvokeOnMainThread(async () =>
                        //{
                        //    await _dialogService.ShowMessageBox(AppResources.NoBillsAvailable, AppResources.Information);
                        //});
                    }
                });
                onUnpaidLabelClicked = new Command(() =>
                {
                    if (MyBillsOriginal.Count != 0)
                    {
                        List<MyBills> myBills = new List<MyBills>();
                        myBills = MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 2)).ToList();
                        if (myBills != null && myBills.Count > 0)
                        {
                            MyBills = myBills;
                            SetNoDataLabelVisibility = false;
                        }
                        else
                        {
                            MyBills = null;
                            SetNoDataLabelVisibility = true;
                            //Device.BeginInvokeOnMainThread(async () =>
                            //{
                            //    await _dialogService.ShowMessageBox(AppResources.NoBillsAvailable, AppResources.Information);
                            //});
                        }
                    }
                    else
                    {
                        MyBills = null;
                        SetNoDataLabelVisibility = true;
                        //Device.BeginInvokeOnMainThread(async () =>
                        //{
                        //    await _dialogService.ShowMessageBox(AppResources.NoBillsAvailable, AppResources.Information);
                        //});
                    }

                });
                onPartiallyPaidLabelClicked = new Command(() =>
                {
                    if (MyBillsOriginal.Count != 0)
                    {
                        List<MyBills> myBills = new List<MyBills>();
                        myBills = MyBillsOriginal.Where(x => x.Status == Enum.GetName(typeof(BillStatus), 1)).ToList();
                        if(myBills != null && myBills.Count > 0)
                        {
                            MyBills = myBills;
                            SetNoDataLabelVisibility = false;
                        }
                        else
                        {
                            MyBills = null;
                            SetNoDataLabelVisibility = true;
                            //Device.BeginInvokeOnMainThread(async () =>
                            //{
                            //    await _dialogService.ShowMessageBox(AppResources.NoBillsAvailable, AppResources.Information);
                            //});
                        }
                       
                    }
                    else
                    {
                        MyBills = null;
                        SetNoDataLabelVisibility = true;
                        //Device.BeginInvokeOnMainThread(async () =>
                        //{
                        //    await _dialogService.ShowMessageBox(AppResources.NoBillsAvailable, AppResources.Information);
                        //});
                    }
                });
            }
            catch(Exception e)
            {

                //Device.BeginInvokeOnMainThread(async () =>
                //{
                //    await _dialogService.ShowMessageBox(AppResources.NoBillsAvailable, AppResources.Information);
                //});
            }
        }


        public async Task onPageLoad()
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });

                await Task.Run(async () =>
                {
                    MyBills = null;

                    List<MyBills> myBills = null;
                    try
                    {

                        string lang = UtilityManager.GetLanguageParameter();
                        myBills = await WebServiceManager.GAZTGetMyBills(App.TP.Tin, lang);
                        await PopToRootPage();// If seesion Expired it will navigate to Dashboard page


                    if (myBills != null && myBills.Count != 0)
                        {
                            myBills = UpdateDueAmount(myBills);
                            MyBills = new List<MyBills>();
                            MyBills = myBills;
                            MyBillsOriginal = myBills;
                        }
                        else
                        {
                            await _dialogService.ShowMessageBox(AppResources.NoBillsAvailable, AppResources.Information);
                            _navigationService.GoBack();
                        }
                    }
                    catch (Exception e)
                    {

                        await _dialogService.ShowMessageBox(AppResources.NoBillsAvailable, AppResources.Information);
                        _navigationService.GoBack();

                    }
                });

                await Task.Run(() =>
                {
                    IsLoading = false;
                });


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
            catch(InternetException ex)
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
        }

        public async Task PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(async () => {
                    var _navigation = Application.Current.MainPage.Navigation;
                    await _navigation.PopToRootAsync();
                });
            }
        }

        private List<MyBills> UpdateDueAmount(List<MyBills> myBills)
        {
           // List<MyBills> list = new List<MyBills>();
          

            for (int i = 0; i < myBills.Count; i++)
            {

                if (myBills[i].TestDueAmount.Contains("."))
                {
                    string[] Amount  = new String[2];
                    Amount = myBills[i].TestDueAmount.Split('.');
                    double testDueAmount = Convert.ToDouble(Amount[0]);
                    string _testDueAmount = testDueAmount.ToString("#,##0");
                    _testDueAmount = _testDueAmount +"." + Amount[1];
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
                 
                    myBills[i].FAEDN = _dueDate[0];
                }

            }
            return myBills;
        }

       

    }
}
