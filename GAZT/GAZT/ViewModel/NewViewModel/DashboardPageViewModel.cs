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




        }

        #endregion


        #region Method

        public async Task onPageLoad()
        {
            BillReturn = new List<BillReturn>();
            Type myType = typeof(ReturnType);


            string lang = UtilityManager.GetLanguageParameter();
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
                objBill1.ReturnTypeProperty = ReturnType.DueIcr;
                objBill1.ReturnCount = dashboard.results[0].DueIcr;

                BillReturn.Add(objBill2);

                BillReturn objBill3 = new BillReturn();
                objBill1.ReturnTypeProperty = ReturnType.PrtnTot;
                objBill1.ReturnCount = dashboard.results[0].PrtnTot;

                BillReturn.Add(objBill3);

                BillReturn objBill4 = new BillReturn();
                objBill1.ReturnTypeProperty = ReturnType.NrtnTot;
                objBill1.ReturnCount = dashboard.results[0].NrtnTot;

                BillReturn.Add(objBill4);

                BillReturn objBill5 = new BillReturn();
                objBill1.ReturnTypeProperty = ReturnType.UprtnTot;
                objBill1.ReturnCount = dashboard.results[0].UprtnTot;

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


            
        }

        #endregion
    }
}
