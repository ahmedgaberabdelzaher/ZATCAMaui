using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        private string _startDate;
        public string StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                _startDate = value;
                RaisePropertyChanged("StartDate");
            }
        }

        private string _endDate;
        public string EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                _endDate = value;
                RaisePropertyChanged("EndDate");
            }
        }


        

        private int _heightRequestForCollectionView = 0;
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
            OnMyTaxPayerProfileClicked = new Xamarin.Forms.Command(() =>
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
          
            List<BillReturn> BillReturnList = new List<BillReturn>();
            List<BillPaid> BillPaidList = new List<BillPaid>();

            BillReturn = new List<BillReturn>();
            BillPaid = new List<BillPaid>();

            string lang = UtilityManager.GetLanguageParameter();


            dashboard = WebServiceManager.GAZTGetDashboardData(lang, App.TP.Userid);



            if (dashboard.results != null)
            {

                //return
                //BillReturn = new List<BillReturn>();
                DateTime BegDate = dashboard.results[0].Begda;
                DateTime endDate = dashboard.results[0].Endda;
                 StartDate = BegDate.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE"));
                 EndDate = endDate.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE"));

                BillReturn objBill1 = new BillReturn();
                objBill1.ReturnTypeProperty = ReturnType.RtnTot;
                String RtnTotstr = dashboard.results[0].RtnTot.TrimStart(new Char[] { '0' });
                if (string.IsNullOrEmpty(RtnTotstr))
                {
                    RtnTotstr = "0";
                }
                objBill1.ReturnCount = RtnTotstr;

                BillReturnList.Add(objBill1);

                BillReturn objBill2 = new BillReturn();
                objBill2.ReturnTypeProperty = ReturnType.DueIcr;
                String DueIcrstr = dashboard.results[0].DueIcr.TrimStart(new Char[] { '0' });
                if (string.IsNullOrEmpty(DueIcrstr))
                {
                    DueIcrstr = "0";
                }
                objBill2.ReturnCount = DueIcrstr;

                BillReturnList.Add(objBill2);

                //BillReturn objBill3 = new BillReturn();
                //objBill3.ReturnTypeProperty = ReturnType.PrtnTot;
                //String PrtnTotstr = dashboard.results[0].PrtnTot.TrimStart(new Char[] { '0' });
                //if (string.IsNullOrEmpty(PrtnTotstr))
                //{
                //    PrtnTotstr = "0";
                //}
                //objBill3.ReturnCount = PrtnTotstr;

                //BillReturnList.Add(objBill3);

                BillReturn objBill4 = new BillReturn();
                objBill4.ReturnTypeProperty = ReturnType.NrtnTot;
                String NrtnTotstr = dashboard.results[0].NrtnTot.TrimStart(new Char[] { '0' });
                if (string.IsNullOrEmpty(NrtnTotstr))
                {
                    NrtnTotstr = "0";
                }

                objBill4.ReturnCount = NrtnTotstr;

                BillReturnList.Add(objBill4);
                BillReturn = BillReturnList;
                //BillReturn objBill5 = new BillReturn();
                //objBill5.ReturnTypeProperty = ReturnType.UprtnTot;
                //String UprtnTotstr = dashboard.results[0].UprtnTot.TrimStart(new Char[] { '0' });
                //if (string.IsNullOrEmpty(UprtnTotstr))
                //{
                //    UprtnTotstr = "0";
                //}
                //objBill5.ReturnCount = UprtnTotstr;

                //BillReturn.Add(objBill5);

                //Paid

                //  BillPaid = new List<BillPaid>();

                BillPaid objBillPaid1 = new BillPaid();
                objBillPaid1.BillTypeProperty = BillType.PbillsTot;
                String PbillsTotstr = dashboard.results[0].PbillsTot.TrimStart(new Char[] { '0' });
                if (string.IsNullOrEmpty(PbillsTotstr))
                {
                    PbillsTotstr = "0";
                }
                objBillPaid1.BillCount = PbillsTotstr;
                BillPaidList.Add(objBillPaid1);

                BillPaid objBillPaid2 = new BillPaid();
                objBillPaid2.BillTypeProperty = BillType.PrbillsTot;
                String PrbillsTotstr = dashboard.results[0].PrbillsTot.TrimStart(new Char[] { '0' });
                if (string.IsNullOrEmpty(PrbillsTotstr))
                {
                    PrbillsTotstr = "0";
                }
                objBillPaid2.BillCount = PrbillsTotstr;
                BillPaidList.Add(objBillPaid2);

                BillPaid objBillPaid3 = new BillPaid();
                objBillPaid3.BillTypeProperty = BillType.UpbillsTot;
                String UpbillsTotstr = dashboard.results[0].UpbillsTot.TrimStart(new Char[] { '0' });
                if (string.IsNullOrEmpty(UpbillsTotstr))
                {
                    UpbillsTotstr = "0";
                }
                objBillPaid3.BillCount = UpbillsTotstr;
                BillPaidList.Add(objBillPaid3);
                BillPaid = BillPaidList;



            }
            else
            {
                _dialogService.ShowMessageBox("No data available", AppResources.Information);
            }



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
