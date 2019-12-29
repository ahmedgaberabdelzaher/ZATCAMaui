using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
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
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessageBox(AppResources.NoBillsAvailable, AppResources.Information);
                            });
                        }
                    }
                else
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessageBox(AppResources.NoBillsAvailable, AppResources.Information);
                        });
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
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessageBox(AppResources.NoBillsAvailable, AppResources.Information);
                            });
                        }
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessageBox(AppResources.NoBillsAvailable, AppResources.Information);
                        });
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
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessageBox(AppResources.NoBillsAvailable, AppResources.Information);
                            });
                        }
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessageBox(AppResources.NoBillsAvailable, AppResources.Information);
                        });
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
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessageBox(AppResources.NoBillsAvailable, AppResources.Information);
                            });
                        }
                       
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessageBox(AppResources.NoBillsAvailable, AppResources.Information);
                        });
                    }
                });
            }
            catch(Exception e)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessageBox(AppResources.NoBillsAvailable, AppResources.Information);
                });
            }
        }


        public async void onPageLoad()
        {
            MyBills = null;

            List<MyBills> myBills = null;
            try
            {


                myBills = await WebServiceManager.GAZTGetMyBills(App.TP.Tin);
                await PopToRootPage();// If seesion Expired it will navigate to Dashboard page


                if (myBills != null && myBills.Count != 0)
                {
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
                var _navigation = Application.Current.MainPage.Navigation;
                await _navigation.PopToRootAsync();
            }
        }

    }
}
