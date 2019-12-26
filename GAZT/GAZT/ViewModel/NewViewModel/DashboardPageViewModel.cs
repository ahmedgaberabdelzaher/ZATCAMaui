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


namespace GAZT.ViewModel
{
   public class DashboardPageViewModel : ViewModelBase
    {

        #region Variable

        public readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        public ICommand OnMyCertificateClicked { get; set; }
        public ICommand OnBellClicked { get; set; }
        public ICommand OnMyTaxPayerProfileClicked { get; set; }
        public ICommand OnMyBillsClicked { get; set; }
        public Dashboard dashboard { get; set; }

        #endregion

        #region Property

        private List<BillReturn> _billReturn;
        public List<BillReturn> BillReturn
        {
            get
            {
                return _billReturn;
            }
            set
            {
                _billReturn = value;
                RaisePropertyChanged("BillReturn");
            }
        }


        private List<BillPaid> _billPaid;
        public List<BillPaid> BillPaid
        {
            get
            {
                return _billPaid;
            }
            set
            {
                _billPaid = value;
                RaisePropertyChanged("BillPaid");
            }
        }

        private double _deviceWidth;
        public double DeviceWidth
        {
            get
            {
                return _deviceWidth;
            }
            set
            {
                _deviceWidth = value;
                RaisePropertyChanged("DeviceWidth");
            }
        }

        private int _heightRequestForCollectionView=0;
        public int HeightRequestForCollectionView
        {
            get
            {
                return _heightRequestForCollectionView;
            }
            set
            {
                _heightRequestForCollectionView = value;
                RaisePropertyChanged("HeightRequestForCollectionView");
            }
        }



        private double _deviceHeight;
        public double DeviceHeight
        {
            get
            {
                return _deviceHeight;
            }
            set
            {
                _deviceHeight = value;
                RaisePropertyChanged("DeviceHeight");
            }
        }

        private double _paddingHeight;
        public double PaddingHeight
        {
            get
            {
                return _paddingHeight;
            }
            set
            {
                _paddingHeight = value;
                RaisePropertyChanged("PaddingHeight");
            }
        }

        private int _calendarHeightRequest;
        public int CalendarHeightRequest
        {
            get
            {
                return _calendarHeightRequest;
            }
            set
            {
                _calendarHeightRequest = value;
                RaisePropertyChanged("CalendarHeightRequest");
            }
        }


        private Xamarin.Forms.Thickness _paddingForCollectionView = new Xamarin.Forms.Thickness(0, 0, 0, 0);
        public Xamarin.Forms.Thickness PaddingForCollectionView
        {
            get => _paddingForCollectionView;
            set
            {
                _paddingForCollectionView = value;
            }
        }


        #endregion


        #region Constructor

        public DashboardPageViewModel(INavigationService navigationService, IDialogService dialogService)
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

            OnMyCertificateClicked = new Xamarin.Forms.Command(() =>
            {
                _navigationService.NavigateTo(App.MyCertificate);

            });
            OnBellClicked = new Xamarin.Forms.Command(async () =>
            {
            });
            OnMyTaxPayerProfileClicked = new Xamarin.Forms.Command(async () =>
            {
                _navigationService.NavigateTo(App.TaxPayerProfileView);

            });

            OnMyBillsClicked = new Xamarin.Forms.Command(async () =>
            {
                _navigationService.NavigateTo(App.MyBillsView);

            });

            OnMyTaxPayerProfileClicked = new Xamarin.Forms.Command(async () =>
            {
                _navigationService.NavigateTo(App.TaxPayerProfilePageView);

            });


        }

        #endregion


        #region Method

        public void onPageLoad()
        {



            string lang = UtilityManager.GetLanguageParameter();
            Task.Run(async () =>
            {
                dashboard = await WebServiceManager.GAZTGetDashboardData(lang, App.TP.Userid);



                if (dashboard.results != null)
                {

                    //return
                    BillReturn = new List<BillReturn>();

                    BillReturn objBill1 = new BillReturn();
                    objBill1.ReturnTypeProperty = ReturnType.RtnTot;
                    objBill1.ReturnCount = dashboard.results[0].RtnTot;

                    BillReturn.Add(objBill1);

                    BillReturn objBill2 = new BillReturn();
                    objBill2.ReturnTypeProperty = ReturnType.DueIcr;
                    objBill2.ReturnCount = dashboard.results[0].DueIcr;

                    BillReturn.Add(objBill2);

                    BillReturn objBill3 = new BillReturn();
                    objBill3.ReturnTypeProperty = ReturnType.PrtnTot;
                    objBill3.ReturnCount = dashboard.results[0].PrtnTot;

                    BillReturn.Add(objBill3);

                    BillReturn objBill4 = new BillReturn();
                    objBill4.ReturnTypeProperty = ReturnType.NrtnTot;
                    objBill4.ReturnCount = dashboard.results[0].NrtnTot;

                    BillReturn.Add(objBill4);

                    BillReturn objBill5 = new BillReturn();
                    objBill5.ReturnTypeProperty = ReturnType.UprtnTot;
                    objBill5.ReturnCount = dashboard.results[0].UprtnTot;

                    BillReturn.Add(objBill5);

                    //Paid

                    BillPaid = new List<BillPaid>();

                    BillPaid objBillPaid1 = new BillPaid();
                    objBillPaid1.BillTypeProperty = BillType.PbillsTot;
                    objBillPaid1.BillCount = dashboard.results[0].PbillsTot;
                    BillPaid.Add(objBillPaid1);

                    BillPaid objBillPaid2 = new BillPaid();
                    objBillPaid2.BillTypeProperty = BillType.PrbillsTot;
                    objBillPaid2.BillCount = dashboard.results[0].PrbillsTot;
                    BillPaid.Add(objBillPaid2);

                    BillPaid objBillPaid3 = new BillPaid();
                    objBillPaid3.BillTypeProperty = BillType.UpbillsTot;
                    objBillPaid3.BillCount = dashboard.results[0].UpbillsTot;
                    BillPaid.Add(objBillPaid3);

                }
                else
                {
                    _dialogService.ShowMessageBox("No data available", AppResources.Information);
                }

            });


            //BillReturn = new List<BillReturn>();

            //BillReturn bill = new BillReturn();
            //bill.ReturnTypeProperty = ReturnType.RtnTot;
            //bill.ReturnCount = "00010";

            //BillReturn.Add(bill);

            //BillReturn bill1 = new BillReturn();
            //bill1.ReturnTypeProperty = ReturnType.DueIcr;
            //bill1.ReturnCount = "00030";
            //BillReturn.Add(bill1);

            //BillReturn bill2 = new BillReturn();
            //bill2.ReturnTypeProperty = ReturnType.PrtnTot;
            //bill2.ReturnCount = "00015";
            //BillReturn.Add(bill2);

            //BillReturn bill3 = new BillReturn();
            //bill3.ReturnTypeProperty = ReturnType.NrtnTot;
            //bill3.ReturnCount = "00034";
            //BillReturn.Add(bill3);

            //BillReturn bill4 = new BillReturn();
            //bill4.ReturnTypeProperty = ReturnType.UprtnTot;
            //bill4.ReturnCount = "00022";
            //BillReturn.Add(bill4);



            //BillPaid = new List<BillPaid>();

            //BillPaid billPaid1 = new BillPaid();
            //billPaid1.BillTypeProperty = BillType.PbillsTot;
            //billPaid1.BillCount = "00010";
            //BillPaid.Add(billPaid1);

            //BillPaid billPaid2 = new BillPaid();
            //billPaid2.BillTypeProperty = BillType.PrbillsTot;
            //billPaid2.BillCount = "00020";
            //BillPaid.Add(billPaid2);

            //BillPaid billPaid3 = new BillPaid();
            //billPaid3.BillTypeProperty = BillType.UpbillsTot;
            //billPaid3.BillCount = "00040";
            //BillPaid.Add(billPaid3);





        }

        #endregion
    }
}
