using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT;
using GAZTeServicesBusinessLibrary;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Syncfusion.SfCalendar.XForms;
using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace GAZTeServicesApp.ViewModels.LandingPage
{
    /// <summary>
    /// ViewModel for article list page.
    /// </summary> 
    [Preserve(AllMembers = true)]
    public class LandingPageViewModel : ViewModelBase
    {
        #region Fields

        private Dashboard DashboardData = null;
        public List<OverduePaymentsAndUnSubmittedReturn> _listUnsubmittedReturn = null;
        public List<OverduePaymentsAndUnSubmittedReturn> _listOverduePaymentReturn = null;
        public List<OverduePaymentsAndUnSubmittedReturn> _listofPaymentReturn = null;
        private ObservableCollection<eServiceInfo> _eServicesItems = null;
        private ObservableCollection<ReturnInfo> _ReturnInfoItems = null;
        private ObservableCollection<BillInfo> _PaymentInfoItems = null;
        private CalendarEventCollection _BillsAndReturnsSchedule = null;
        
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        #endregion
        
        #region Constructor
        /// <summary>
        /// Initializes a new instance for the <see cref="LandingPageViewModel" /> class.
        /// </summary>
        /// 

        public async Task LoadDashboardData()
        {

            listOverduePaymentReturn = GAZTeServicesBusinessLibrary.WebServiceManager.GAZTGetPaymentOverdueSetForDashboardData("E", App.TP.Tin);

            await Task.Run(() =>
            {
                IsLoading = true;
            });

            await Task.Delay(3000);

            Task GetDashboardDataTask = Task.Run(() =>
            {
                DashboardData = GAZTeServicesBusinessLibrary.WebServiceManager.GAZTGetDashboardData("EN", App.TP.Tin);
            });

            Task GetUnsubmittedReturnDataTask = Task.Run(() =>
            {
                listUnsubmittedReturn = GAZTeServicesBusinessLibrary.WebServiceManager.GAZTGetUnSubmittedReturnSetForDashboardData("E", App.TP.Tin);
            });

            Task GetOverduePaymentDataTask = Task.Run(() =>
            {
                listOverduePaymentReturn = GAZTeServicesBusinessLibrary.WebServiceManager.GAZTGetPaymentOverdueSetForDashboardData("E", App.TP.Tin);
            });

            try
            {
               GetDashboardDataTask.Wait();
            }
            catch (AggregateException ae)
            {
                IsLoading = false;
                foreach (var gex in ae.InnerExceptions)
                {
                    // Handle the GAZT custom exception.
                    if (gex is GAZTException)
                    {
                        string MessageForTheUser = gex.Message;
                        if (gex is GAZTNetworkConnectivityIssueException)
                        {
                            MessageForTheUser = AppResources.NetworkConnectivityIssue;
                        }
                        else if (gex is GAZTInternetException)
                        {
                            MessageForTheUser = AppResources.ZZInternetConnectionMessage;
                        }

                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                        });
                    }
                    // Rethrow any other exception.
                    else
                    {
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                IsLoading = false;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage("something went wrong", AppResources.Information);
                });
            }
      
            await Task.Run(() =>
            {
                IsLoading = false;
            });
        }
        public  LandingPageViewModel(INavigationService navigationService, IDialogService dialogService) //: base(navigationService, dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            _navigationService = navigationService;
            _dialogService = dialogService;

            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }

            this.ShowOptionsCommand = new Command(this.ShowOptionsCommandClicked);
            this.ItemSelectedCommand = new Command(this.ItemSelected);
        }

        #endregion

        #region Public Properties

        public ObservableCollection<eServiceInfo> eServicesAvailableToTheTP
        {
            get
            {
                return this._eServicesItems;
            }

            set
            {
                this._eServicesItems = value;
                this.RaisePropertyChanged("eServicesAvailableToTheTP");
            }
        }



        public List<OverduePaymentsAndUnSubmittedReturn> listUnsubmittedReturn
        {
            get
            {
                return this._listUnsubmittedReturn;
            }

            set
            {
                this._listUnsubmittedReturn = value;
                this.RaisePropertyChanged("listUnsubmittedReturn");
            }
        }

        public List<OverduePaymentsAndUnSubmittedReturn> listOverduePaymentReturn
        {
            get
            {
                return this._listOverduePaymentReturn;
            }

            set
            {
                this._listOverduePaymentReturn = value;
                this.RaisePropertyChanged("listOverduePaymentReturn");
            }
        }

        public List<OverduePaymentsAndUnSubmittedReturn> listofPaymentReturn
        {
            get
            {
                return this._listofPaymentReturn;
            }

            set
            {
                this._listofPaymentReturn = value;
                this.RaisePropertyChanged("listofPaymentReturn");
            }
        }


        /// <summary>
        /// Gets or sets the returninfo items collection.
        /// </summary>
        public ObservableCollection<ReturnInfo> ReturnInfoItems
        {
            get
            {
                return this._ReturnInfoItems;
            }

            set
            {
                this._ReturnInfoItems = value;
                this.RaisePropertyChanged("ReturnInfoItems");
            }
        }

        /// <summary>
        /// Gets or sets the billInfo items collection.
        /// </summary>
        public ObservableCollection<BillInfo> BillsInfoItems
        {
            get
            {
                return this._PaymentInfoItems;
            }
            set
            {
                this._PaymentInfoItems = value;
                this.RaisePropertyChanged("BillsInfoItems");
            }
        }

        /// <summary>
        /// Gets or sets the BillsAndReturnsSchedule.
        /// </summary>
        public CalendarEventCollection BillsAndReturnsSchedule
        {
            get
            {
                return this._BillsAndReturnsSchedule;
            }

            set
            {
                if (this._BillsAndReturnsSchedule == value)
                {
                    return;
                }

                this._BillsAndReturnsSchedule = value;
                this.RaisePropertyChanged("BillsAndReturnsSchedule");
            }
        }

        private GAZT.Models.TaxPayerProfile _TaxPayerProfile = App.TP;
        public GAZT.Models.TaxPayerProfile TaxPayerProfile
        {
            get
            {
                return this._TaxPayerProfile;
            }

            set
            {
                if (this._TaxPayerProfile == value)
                {
                    return;
                }

                this._TaxPayerProfile = value;
                this.RaisePropertyChanged("TaxPayerProfile");
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that will be executed when the menu button is clicked.
        /// </summary>
        public Command ShowOptionsCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when an item is selected.
        /// </summary>
        public Command ItemSelectedCommand { get; set; }

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

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the menu button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void ShowOptionsCommandClicked(object obj)
        {
            _navigationService.NavigateTo("OptionsPage");
        }

        /// <summary>
        /// Invoked when an item is selected.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void ItemSelected(object obj)
        {
            // Do something
        }

        private string ConvertintoCommaSeperated(string strAmount)
        {
            string amountWithComma = string.Empty;
            try
            {
                if (strAmount != null && strAmount.Length > 0)
                {
                    double testDueAmount = Convert.ToDouble(strAmount);
                    CultureInfo ci = new CultureInfo("en-us");
                    string _testDueAmount;//= testDueAmount.ToString("#,##0");
                    double floating = Convert.ToDouble(strAmount);
                    _testDueAmount = floating.ToString("N02", ci);
                    amountWithComma = _testDueAmount;
                }
            }
            catch (Exception ex)
            {
            }

            return amountWithComma;
        }

        public void PopulateReturnsInformation()
        {
            ReturnInfoItems = new ObservableCollection<ReturnInfo>();

            if (DashboardData.results != null && DashboardData.results.Count > 0)
            {
                if (DashboardData.results[0] != null && DashboardData.results[0].RtnTot != null)
                {
                    ReturnInfo objReturnInfoRtnTot = new ReturnInfo();
                    objReturnInfoRtnTot.ReturnTypeProperty = GAZTeServicesBusinessLibrary.ReturnType.RtnTot;

                    String RtnTotstr = DashboardData.results[0].RtnTot.TrimStart(new Char[] { '0' });

                    if (string.IsNullOrEmpty(RtnTotstr))
                    {
                        RtnTotstr = "0";
                    }
                    else if (RtnTotstr.Substring(0, 1) == ".")
                    {
                        RtnTotstr = "0" + RtnTotstr;
                    }

                    objReturnInfoRtnTot.ReturnCount = RtnTotstr;

                    objReturnInfoRtnTot.BackgroundGradientStart = "#006450";
                    objReturnInfoRtnTot.BackgroundGradientEnd = "#CCE0DC";
                    objReturnInfoRtnTot.iConImagePath = "sf_ic_Submited_Returns_White.png";
                    objReturnInfoRtnTot.ReturnTypeName = AppResources.Submitted;


                    ReturnInfoItems.Add(objReturnInfoRtnTot);

                }

                if (DashboardData.results[0] != null && DashboardData.results[0].NrtnTot != null)
                {
                    ReturnInfo objReturnInfoNrtnTot = new ReturnInfo();
                    objReturnInfoNrtnTot.ReturnTypeProperty = GAZTeServicesBusinessLibrary.ReturnType.NrtnTot;

                    String NrtnTotstr = DashboardData.results[0].NrtnTot.TrimStart(new Char[] { '0' });
                    if (string.IsNullOrEmpty(NrtnTotstr))
                    {
                        NrtnTotstr = "0";
                    }
                    else if (NrtnTotstr.Substring(0, 1) == ".")
                    {
                        NrtnTotstr = "0" + NrtnTotstr;
                    }
                    objReturnInfoNrtnTot.ReturnCount = NrtnTotstr;

                    objReturnInfoNrtnTot.BackgroundGradientStart = "#5D6770";
                    objReturnInfoNrtnTot.BackgroundGradientEnd = "#DFE1E2";
                    objReturnInfoNrtnTot.iConImagePath = "sf_ic_Unsubmited_Returns_White.png";
                    objReturnInfoNrtnTot.ReturnTypeName = AppResources.NonSubmitted;

                    ReturnInfoItems.Add(objReturnInfoNrtnTot);

                }


                if (DashboardData.results[0] != null && DashboardData.results[0].DueIcr != null)
                {

                    ReturnInfo objReturnInfoDueIcr = new ReturnInfo();
                    objReturnInfoDueIcr.ReturnTypeProperty = GAZTeServicesBusinessLibrary.ReturnType.DueIcr;

                    String DueIcrstr = DashboardData.results[0].DueIcr.TrimStart(new Char[] { '0' });
                    if (string.IsNullOrEmpty(DueIcrstr))
                    {
                        DueIcrstr = "0";
                    }
                    else if (DueIcrstr.Substring(0, 1) == ".")
                    {
                        DueIcrstr = "0" + DueIcrstr;
                    }
                    objReturnInfoDueIcr.ReturnCount = DueIcrstr;

                    objReturnInfoDueIcr.BackgroundGradientStart = "#AA0C19";
                    objReturnInfoDueIcr.BackgroundGradientEnd = "#EECED1";
                    objReturnInfoDueIcr.iConImagePath = "sf_ic_Overdue_Returns_White.png";
                    objReturnInfoDueIcr.ReturnTypeName = AppResources.OverDue;

                    ReturnInfoItems.Add(objReturnInfoDueIcr);
                }

            }
        }

        public void PopulateBillsInformation()
        {
            BillsInfoItems = new ObservableCollection<BillInfo>();

            if (DashboardData.results != null && DashboardData.results.Count > 0)
            {

                //Paid Bills
                if (DashboardData.results[0] != null && DashboardData.results[0].PbillsTot != null)
                {
                    BillInfo objBillInfoPbillsTot = new BillInfo();

                    objBillInfoPbillsTot.BillTypeProperty = BillType.PbillsTot;

                    String PaidBillsstr = DashboardData.results[0].PbillsTot.TrimStart(new Char[] { '0' });
                    String PaidBillsAmountstr = DashboardData.results[0].PbillsBetrw.TrimStart(new Char[] { '0' });

                    //PaidBillsstr = Convert.ToDouble(PaidBillsstr).ToString();

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

                    string TotalPaidAmount = PaidBillsAmountstr;

                    objBillInfoPbillsTot.BillCount = PaidBillsstr;
                    objBillInfoPbillsTot.BillAmount = ConvertintoCommaSeperated(PaidBillsAmountstr);

                    objBillInfoPbillsTot.BackgroundGradientStart = "#006450";
                    objBillInfoPbillsTot.BackgroundGradientEnd = "#CCE0DC";
                    objBillInfoPbillsTot.iConImagePath = "sf_ic_Paid.png";
                    objBillInfoPbillsTot.BillTypeName = AppResources.Paid;


                    DateTime dateTime = new DateTime(2019, 5, 1);

                    //objBillInfoPbillsTot.ChartData
                    //= new ObservableCollection<ChartDataPoint>()
                    //{
                    //new ChartDataPoint(dateTime, 15),
                    //new ChartDataPoint(dateTime.AddMonths(1), 20),
                    //new ChartDataPoint(dateTime.AddMonths(2), 30),
                    //new ChartDataPoint(dateTime.AddMonths(3), 17),
                    //new ChartDataPoint(dateTime.AddMonths(4), 13),
                    //new ChartDataPoint(dateTime.AddMonths(5), 25),
                    //new ChartDataPoint(dateTime.AddMonths(6), 19),
                    //new ChartDataPoint(dateTime.AddMonths(7), 43),
                    //new ChartDataPoint(dateTime.AddMonths(8), 43),
                    //new ChartDataPoint(dateTime.AddMonths(9), 43),
                    //new ChartDataPoint(dateTime.AddMonths(10), 43)
                    //};

                    BillsInfoItems.Add(objBillInfoPbillsTot);
                }

                //Partially Paid Bills

                if (DashboardData.results[0] != null && DashboardData.results[0].PrbillsTot != null)
                {
                    BillInfo objBillInfoPrbillsTot = new BillInfo();

                    objBillInfoPrbillsTot.BillTypeProperty = BillType.PrbillsTot;
                    String PartialPaidBillsstr = DashboardData.results[0].PrbillsTot.TrimStart(new Char[] { '0' });
                    String PartialPaidBillsAmountstr = DashboardData.results[0].PrbillsBetrw.TrimStart(new Char[] { '0' });

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
                    objBillInfoPrbillsTot.BillCount = PartialPaidBillsstr;
                    objBillInfoPrbillsTot.BillAmount = ConvertintoCommaSeperated(PartialPaidBillsAmountstr);

                    objBillInfoPrbillsTot.BackgroundGradientStart = "#D99A29";
                    objBillInfoPrbillsTot.BackgroundGradientEnd = "#F7EBD4";
                    objBillInfoPrbillsTot.iConImagePath = "sf_ic_Partially_Paid.png";
                    objBillInfoPrbillsTot.BillTypeName = AppResources.Partial;

                    BillsInfoItems.Add(objBillInfoPrbillsTot);
                }

                //Unpaid Bills
                if (DashboardData.results[0] != null && DashboardData.results[0].UpbillsTot != null)
                {
                    BillInfo objBillInfoUpbillsTot = new BillInfo();

                    objBillInfoUpbillsTot.BillTypeProperty = BillType.UpbillsTot;
                    String UnpaidBillsstr = DashboardData.results[0].UpbillsTot.TrimStart(new Char[] { '0' });
                    String UnpaidBillsAmountstr = DashboardData.results[0].UpbillsBetrw.TrimStart(new Char[] { '0' });
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
                    objBillInfoUpbillsTot.BillCount = UnpaidBillsstr;
                    objBillInfoUpbillsTot.BillAmount = ConvertintoCommaSeperated(UnpaidBillsAmountstr);

                    objBillInfoUpbillsTot.BackgroundGradientStart = " #AA0C19";
                    objBillInfoUpbillsTot.BackgroundGradientEnd = "#EECED1";
                    objBillInfoUpbillsTot.iConImagePath = "sf_ic_Unpaid.png";
                    objBillInfoUpbillsTot.BillTypeName = AppResources.UnPaid;

                    BillsInfoItems.Add(objBillInfoUpbillsTot);
                }
            }

        }

        public void PopulateBillsAndReturnsSchedule()
        {
            try
            {
                BillsAndReturnsSchedule = new CalendarEventCollection();
                
                listofPaymentReturn = new List<OverduePaymentsAndUnSubmittedReturn>();
                listofPaymentReturn.Clear();
                // Create events

                foreach (var PaymentReturn in listOverduePaymentReturn)
                {
                    listofPaymentReturn.Add(PaymentReturn);
                }

                foreach (var UnsubmittedReturn in listUnsubmittedReturn)
                {
                    listofPaymentReturn.Add(UnsubmittedReturn);
                }

               // listofPaymentReturn = listofPaymentReturn.Union(listOverduePaymentReturn).ToList();

                foreach (var item in listofPaymentReturn)
                {
                    CalendarInlineEvent event1 = new CalendarInlineEvent();

                    event1.StartTime = item.DueDt;
                 //   event1.EndTime = DateTime.Now;
                    event1.Subject = item.Txt50;
                    if (item.IcrStatus == "O")
                    {
                        event1.Color = Color.FromHex("#ff0000");
                    }
                    else
                    {
                        event1.Color = Color.FromHex("#7D858D");
                    }
                    //{
                    //    StartTime = item.DueDt,
                    //    EndTime = DateTime.Now,
                    //    Subject = item.Txt50,
                    //    if(item.IcrStatus== "O")
                    //{
                    //    color
                    //}
                      
                    //};
                    BillsAndReturnsSchedule.Add(event1);
                }


                //CalendarInlineEvent event1 = new CalendarInlineEvent()
                //{
                //    StartTime = DateTime.Today.AddHours(9),
                //    EndTime = DateTime.Today.AddHours(10),
                //    Subject = "Meeting",
                //    Color = Color.Green
                //};

                //CalendarInlineEvent event2 = new CalendarInlineEvent()
                //{
                //    StartTime = DateTime.Today.AddHours(11),
                //    EndTime = DateTime.Today.AddHours(12),
                //    Subject = "Planning",
                //    Color = Color.Fuchsia
                //};

                //// Add events into a CalendarInlineEvents collection
                //BillsAndReturnsSchedule.Add(event1);
                //BillsAndReturnsSchedule.Add(event2);
            }
            catch(Exception ex)
            { 
            }
        }

        public void PopulateeServicesApplicableToTheTaxPayer()
        {
            //Call the API to get the eSevrices applicable to the TP

            eServicesAvailableToTheTP = new ObservableCollection<eServiceInfo>();

            eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = "Estimated ZAKAT Returns", BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_Estimated_Zakat_Returns.png" });
            eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = "VAT Declarations", BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_VAT_Declarations.png" });
            eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = "Check FORM Bundle Status", BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_My_Bills.png" });
            eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = "Tax Evasion Reporting", BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_My_Bills.png" });
            eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = "Bills", BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_My_Bills.png" });
            eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = "Certificates", BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_My_Bills.png" });
            eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = "VAT Lookup", BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_Estimated_Zakat_Returns.png" });
            eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = "Correspondence", BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_Correspondence.png" });
        }
    }

    #endregion
}


