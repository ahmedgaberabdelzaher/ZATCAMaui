using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using GAZT.Views.NewViews;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

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
        public ICommand OnLogoutClicked { get; set; }
        public ICommand OnUserProfileClicked { get; set; }

        

        public Dashboard dashboard { get; set; }


        public ICommand OnEstimateZakatClicked { get; set; }
        public ICommand OnVATLookupClicked { get; set; }
        public ICommand OnCorrespondanceClicked { get; set; }
        public ICommand OnVatdeclarationClicked { get; set; }
        public ICommand OnTaxevasionClicked { get; set; }


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

        private string _tinNumber;
        public string TinNumber
        {
            get
            {
                return _tinNumber;
            }
            set
            {
                _tinNumber = value;
                RaisePropertyChanged("TinNumber");
            }
        }

        private string _taxPayerName;
        public string TaxPayerName
        {
            get
            {
                return _taxPayerName;
            }
            set
            {
                _taxPayerName = value;
                RaisePropertyChanged("TaxPayerName");
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

        private int _heightRequestForReturnCollectionView = 0;
        public int HeightRequestForReturnCollectionView
        {
            get
            {
                return _heightRequestForReturnCollectionView;
            }
            set
            {
                _heightRequestForReturnCollectionView = value;
                RaisePropertyChanged("HeightRequestForReturnCollectionView");
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

        //private bool _userProfileLayoutVisibility = false;
        //public bool UserProfileLayoutVisibility
        //{
        //    get
        //    {
        //        return _userProfileLayoutVisibility;
        //    }
        //    set
        //    {
        //        _userProfileLayoutVisibility = value;
        //        RaisePropertyChanged("UserProfileLayoutVisibility");
        //    }
        //}


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


        private string _totalNoOfReturns = "0";
        public string TotalNoOfReturns
        {
            get
            {
                return _totalNoOfReturns;
            }
            set
            {
                _totalNoOfReturns = value;



                RaisePropertyChanged("TotalNoOfReturns");
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

        private bool _footerImageInArabic;
        public bool FooterImageInArabic
        {
            get
            {
                return _footerImageInArabic;
            }
            set
            {
                _footerImageInArabic = value;
                RaisePropertyChanged("FooterImageInArabic");
            }
        }

        private bool _footerImageInEnglish;
        public bool FooterImageInEnglish
        {
            get
            {
                return _footerImageInEnglish;
            }
            set
            {
                _footerImageInEnglish = value;
                RaisePropertyChanged("FooterImageInEnglish");
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

            //OnLogoutClicked = new Xamarin.Forms.Command(async () =>
            //{
            //    LogOut();
            //});

            OnUserProfileClicked = new Xamarin.Forms.Command(async () =>
            {
              //  UserProfileLayoutVisibility = !UserProfileLayoutVisibility;
            });

            

            OnEstimateZakatClicked = new Xamarin.Forms.Command(async () =>
            {
                _navigationService.NavigateTo(App.ZakatReturnListPageView);

            });
            OnVATLookupClicked = new Xamarin.Forms.Command(async () =>
            {
                //await _dialogService.ShowMessage("Available in future release", AppResources.Information);
                _navigationService.NavigateTo(App.VATLookupPageView);
            });
            OnCorrespondanceClicked = new Xamarin.Forms.Command(async () =>
            {
                await _dialogService.ShowMessage("Available in future release", AppResources.Information);

            });
            OnVatdeclarationClicked = new Xamarin.Forms.Command(async () =>
            {
                await _dialogService.ShowMessage("Available in future release", AppResources.Information);

            });
            OnTaxevasionClicked = new Xamarin.Forms.Command(async () =>
            {
                await _dialogService.ShowMessage("Available in future release", AppResources.Information);
            });


        }

        #endregion


        #region Method

        public void onPageLoad()
        {
            try
            {
                if (App.IsArabic)
                {
                    TaxPayerName = AppResources.Hi + " (" + App.TP.Tin + ") " + App.TP.Name  ;
                }
                else
                {
                    TaxPayerName = AppResources.Hi + App.TP.Name + " (" + App.TP.Tin + ")";
                }
             
                SetFooterImageVisibility();
                TinNumber = App.TP.Userid;
                String TotalCountOfReturn = string.Empty;
                List<BillReturn> BillReturnList = new List<BillReturn>();
                List<BillPaid> BillPaidList = new List<BillPaid>();
                BillReturn = new List<BillReturn>();
                BillPaid = new List<BillPaid>();
                string lang = UtilityManager.GetLanguageParameter();
                dashboard = WebServiceManager.GAZTGetDashboardData(lang, App.TP.Userid);
                PopToRootPage();// If seesion Expired it will navigate to Dashboard page

                if (dashboard != null)
                {
                    if (dashboard.results != null)
                    {
                        TotalCountOfReturn = dashboard.results[0].IcrTot;

                        if (string.IsNullOrEmpty(TotalCountOfReturn))
                        {
                            TotalCountOfReturn = "0";
                        }
                        else
                        {
                            TotalCountOfReturn = TotalCountOfReturn.TrimStart(new Char[] { '0' });
                            if (TotalCountOfReturn.Substring(0, 1) == ".")
                            {
                                TotalCountOfReturn = "0" + TotalCountOfReturn;
                            }
                        }
                        TotalNoOfReturns = TotalCountOfReturn;


                        //return
                        //BillReturn = new List<BillReturn>();
                        DateTime? BegDate = dashboard.results[0].Begda;
                        DateTime? endDate = dashboard.results[0].Endda;
                        if (App.IsArabic)
                        {
                            //StartDate = BegDate.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-sa"));
                            //EndDate = endDate.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-sa"));
                            if (BegDate != null)
                            {
                                StartDate = Convert.ToDateTime(BegDate).ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-sa"));
                            }
                            if (endDate != null)
                            {
                                EndDate = Convert.ToDateTime(endDate).ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-sa"));
                            }
                        }
                        else
                        {

                            if (BegDate != null)
                            {
                                StartDate = Convert.ToDateTime(BegDate).ToString("yyyy,MM,dd", new CultureInfo("en-US"));

                            }
                            if (endDate != null)
                            {
                                EndDate = Convert.ToDateTime(endDate).ToString("yyyy,MM,dd", new CultureInfo("en-US"));

                            }


                        }
                        BillReturn objBill1 = new BillReturn();
                        objBill1.ReturnTypeProperty = Models.ReturnType.RtnTot;
                        String RtnTotstr = dashboard.results[0].RtnTot.TrimStart(new Char[] { '0' });



                        if (string.IsNullOrEmpty(RtnTotstr))
                        {
                            RtnTotstr = "0";
                        }
                        else if (RtnTotstr.Substring(0, 1) == ".")
                        {
                            RtnTotstr = "0" + RtnTotstr;
                        }

                        objBill1.ReturnCount = RtnTotstr;

                        BillReturnList.Add(objBill1);


                        BillReturn objBill4 = new BillReturn();
                        objBill4.ReturnTypeProperty = Models.ReturnType.NrtnTot;
                        String NrtnTotstr = dashboard.results[0].NrtnTot.TrimStart(new Char[] { '0' });
                        if (string.IsNullOrEmpty(NrtnTotstr))
                        {
                            NrtnTotstr = "0";
                        }
                        else if (NrtnTotstr.Substring(0, 1) == ".")
                        {
                            NrtnTotstr = "0" + NrtnTotstr;
                        }
                        objBill4.ReturnCount = NrtnTotstr;

                        BillReturnList.Add(objBill4);


                        BillReturn objBill2 = new BillReturn();
                        objBill2.ReturnTypeProperty = Models.ReturnType.DueIcr;
                        String DueIcrstr = dashboard.results[0].DueIcr.TrimStart(new Char[] { '0' });
                        if (string.IsNullOrEmpty(DueIcrstr))
                        {
                            DueIcrstr = "0";
                        }
                        else if (DueIcrstr.Substring(0, 1) == ".")
                        {
                            DueIcrstr = "0" + DueIcrstr;
                        }
                        objBill2.ReturnCount = DueIcrstr;

                        BillReturnList.Add(objBill2);

                        //BillReturn objBill3 = new BillReturn();
                        //objBill3.ReturnTypeProperty = Models.ReturnType.PprtnTot;
                        //String PprtnTotstr = dashboard.results[0].PprtnTot.TrimStart(new Char[] { '0' });
                        //if (string.IsNullOrEmpty(PprtnTotstr))
                        //{
                        //    PprtnTotstr = "0";
                        //}
                        //objBill3.ReturnCount = PprtnTotstr;

                        //BillReturnList.Add(objBill3);

                        BillReturn = BillReturnList;


                        BillPaid objBillPaid1 = new BillPaid();
                        objBillPaid1.BillTypeProperty = BillType.PbillsTot;
                        String PaidBillsstr = dashboard.results[0].PbillsTot.TrimStart(new Char[] { '0' });
                        String PaidBillsAmountstr = dashboard.results[0].PbillsBetrw.TrimStart(new Char[] { '0' });

                        if (string.IsNullOrEmpty(PaidBillsstr))
                        {
                            PaidBillsstr = "0";
                        }
                        else if (PaidBillsstr.Substring(0, 1) == ".")
                        {
                            PaidBillsstr = "0" + PaidBillsstr;
                        }
                        if (string.IsNullOrEmpty(PaidBillsAmountstr))
                        {
                            PaidBillsAmountstr = "0";
                        }
                        else if (PaidBillsAmountstr.Substring(0, 1) == ".")
                        {
                            PaidBillsAmountstr = "0" + PaidBillsAmountstr;
                        }
                        objBillPaid1.BillCount = PaidBillsstr;
                        objBillPaid1.BillAmount = ConvertintoCommaSeperated(PaidBillsAmountstr);
                        BillPaidList.Add(objBillPaid1);


                        BillPaid objBillPaid3 = new BillPaid();
                        objBillPaid3.BillTypeProperty = BillType.UpbillsTot;
                        String UnpaidBillsstr = dashboard.results[0].UpbillsTot.TrimStart(new Char[] { '0' });
                        String UnpaidBillsAmountstr = dashboard.results[0].UpbillsBetrw.TrimStart(new Char[] { '0' });
                        if (string.IsNullOrEmpty(UnpaidBillsstr))
                        {
                            UnpaidBillsstr = "0";
                        }
                        else if (UnpaidBillsstr.Substring(0, 1) == ".")
                        {
                            UnpaidBillsstr = "0" + UnpaidBillsstr;
                        }
                        if (string.IsNullOrEmpty(UnpaidBillsAmountstr))
                        {
                            UnpaidBillsAmountstr = "0";
                        }
                        else if (UnpaidBillsAmountstr.Substring(0, 1) == ".")
                        {
                            UnpaidBillsAmountstr = "0" + UnpaidBillsAmountstr;
                        }
                        objBillPaid3.BillCount = UnpaidBillsstr;
                        objBillPaid3.BillAmount = ConvertintoCommaSeperated(UnpaidBillsAmountstr);
                        BillPaidList.Add(objBillPaid3);
                        BillPaid = BillPaidList;


                        BillPaid objBillPaid2 = new BillPaid();
                        objBillPaid2.BillTypeProperty = BillType.PrbillsTot;
                        String PartialPaidBillsstr = dashboard.results[0].PrbillsTot.TrimStart(new Char[] { '0' });
                        String PartialPaidBillsAmountstr = dashboard.results[0].PrbillsBetrw.TrimStart(new Char[] { '0' });
                        if (string.IsNullOrEmpty(PartialPaidBillsstr))
                        {
                            PartialPaidBillsstr = "0";
                        }
                        else if (PartialPaidBillsstr.Substring(0, 1) == ".")
                        {
                            PartialPaidBillsstr = "0" + PartialPaidBillsstr;
                        }
                        if (string.IsNullOrEmpty(PartialPaidBillsAmountstr))
                        {
                            PartialPaidBillsAmountstr = "0";
                        }
                        else if (PartialPaidBillsAmountstr.Substring(0, 1) == ".")
                        {
                            PartialPaidBillsAmountstr = "0" + PartialPaidBillsAmountstr;
                        }
                        objBillPaid2.BillCount = PartialPaidBillsstr;
                        objBillPaid2.BillAmount = ConvertintoCommaSeperated(PartialPaidBillsAmountstr);
                        BillPaidList.Add(objBillPaid2);





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
            }
            catch(Exception ex)
            {

            }
           
        }
        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(async () => {
                    var _navigation = Application.Current.MainPage.Navigation;
                    await _navigation.PopToRootAsync();
                });
            }

        }

        public void LogOut()
        {

                var _navigation = Application.Current.MainPage.Navigation;
                _navigation.PopToRootAsync();
        }

        public string ConvertintoCommaSeperated(string strAmount)
        {
            string actualAmount = string.Empty;
            if (strAmount.Contains("."))
            {
                string[] Amount = new String[2];
                Amount = strAmount.Split('.');
                double testDueAmount = Convert.ToDouble(Amount[0]);
                string _testDueAmount = testDueAmount.ToString("#,##0");
                _testDueAmount = _testDueAmount + "." + Amount[1];
                actualAmount = _testDueAmount;
            }
            else
            {
                double testDueAmount = Convert.ToDouble(strAmount);
                string _testDueAmount = testDueAmount.ToString("#,##0");
                actualAmount = _testDueAmount;
            }
            return actualAmount;
        }

        private void SetFooterImageVisibility()
        {
            if(App.IsArabic)
            {
                FooterImageInArabic = false;
                FooterImageInEnglish = true;
            }
            else
            {
                FooterImageInArabic = true;
                FooterImageInEnglish = false;
            }
        }
        #endregion
    }
}
