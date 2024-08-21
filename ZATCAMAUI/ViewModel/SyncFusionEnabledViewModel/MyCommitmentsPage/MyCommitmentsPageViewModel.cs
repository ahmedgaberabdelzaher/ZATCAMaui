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
using ReturnType = ZATCAMAUI.Models.SyncfusionEnabledModels.ReturnType;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.MyCommitmentsPage
{

    /// <summary>
    /// ViewModel for article list page.
    /// </summary> 
    public class MyCommitmentsPageViewModel : BaseViewModel
    {
        #region Fields

        private Dashboard DashboardData = null;
        public List<OverduePaymentAndUnSubmittedReturn> _listUnsubmittedReturn = null;
        public List<OverduePaymentAndUnSubmittedReturn> _listOverduePaymentReturn = null;
        public List<OverduePaymentAndUnSubmittedReturn> _listofPaymentReturn = null;
        public bool _isButtonEnabled = true;
        private ObservableCollection<eServiceInfo> _eServicesItems = null;
        private ObservableCollection<ReturnInfo> _ReturnInfoItems = null;
        private ObservableCollection<BillInfo> _PaymentInfoItems = null;
        private CalendarEventCollection _BillsAndReturnsSchedule = null;
        private ICommand EserviceCommand { get; set; }

        public DateTime lastTapped;

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance for the <see cref="LandingPageViewModel" /> class.
        /// </summary>
        /// 

        public async Task LoadDashboardData()
        {

            await Task.Run(() =>
            {
                IsLoading = true;
            });

            await Task.Delay(3000);

            Task GetDashboardDataTask = null;
            if (App.TP != null)
            {
                GetDashboardDataTask = Task.Run(async () =>
                {
                    DashboardData = await WebServiceManager.GAZTGetDashboardData(UtilityManager.GetLanguageParameter(), App.TP.userId);
                });

                Task GetUnsubmittedReturnDataTask = Task.Run(async () =>
                {
                    listUnsubmittedReturn = await WebServiceManager.GAZTGetUnSubmittedReturnSetForDashboardData(UtilityManager.GetLanguageParameter(), App.TP.userId);
                });

                Task GetOverduePaymentDataTask = Task.Run(async () =>
                {
                    listOverduePaymentReturn = await WebServiceManager.GAZTGetPaymentOverdueSetForDashboardData(UtilityManager.GetLanguageParameter(), App.TP.userId);
                });
            }

            try
            {
                if (GetDashboardDataTask != null)
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
                        else if (gex is GAZTSessionExpiredException)
                        {
                            MessageForTheUser = AppResources.ZYourSessionhasexpiredPleaseLoginagain;
                        }

                        MainThread.BeginInvokeOnMainThread(async () =>
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
            catch (GAZTSessionExpiredException)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZYourSessionhasexpiredPleaseLoginagain, AppResources.Information);
                    PopToRootPage();
                });
            }
            catch (Exception ex)
            {

                IsLoading = false;
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    PopToRootPage();
                });


            }

            await Task.Run(() =>
            {
                IsLoading = false;
            });
        }
        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    await _navigation.PopToRootAsync();
                });
            }

        }

        public MyCommitmentsPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {

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
                this.OnPropertyChanged("eServicesAvailableToTheTP");
            }
        }



        public List<OverduePaymentAndUnSubmittedReturn> listUnsubmittedReturn
        {
            get
            {
                return this._listUnsubmittedReturn;
            }

            set
            {
                this._listUnsubmittedReturn = value;
                this.OnPropertyChanged("listUnsubmittedReturn");
            }
        }

        public List<OverduePaymentAndUnSubmittedReturn> listOverduePaymentReturn
        {
            get
            {
                return this._listOverduePaymentReturn;
            }

            set
            {
                this._listOverduePaymentReturn = value;
                this.OnPropertyChanged("listOverduePaymentReturn");
            }
        }

        public List<OverduePaymentAndUnSubmittedReturn> listofPaymentReturn
        {
            get
            {
                return this._listofPaymentReturn;
            }

            set
            {
                this._listofPaymentReturn = value;
                this.OnPropertyChanged("listofPaymentReturn");
            }
        }

        public bool IsButtonEnabled
        {
            get
            {
                return this._isButtonEnabled;
            }

            set
            {
                this._isButtonEnabled = value;
                this.OnPropertyChanged("IsButtonEnabled");
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
                this.OnPropertyChanged("ReturnInfoItems");
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
                this.OnPropertyChanged("BillsInfoItems");
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
                this.OnPropertyChanged("BillsAndReturnsSchedule");
            }
        }

        private TaxPayerProfile _TaxPayerProfile = App.TP;
        public TaxPayerProfile TaxPayerProfile
        {
            get
            {
                return this._TaxPayerProfile;
            }

            set
            {
                this._TaxPayerProfile = value;
                this.OnPropertyChanged("TaxPayerProfile");
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

        

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the menu button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void ShowOptionsCommandClicked(object obj)
        {
            ComingToOptionScreenFrom comingToOptionScreenFrom = ComingToOptionScreenFrom.IsDashboardPage;
            _navigationService.NavigateTo(App.SFOptionsPageView, comingToOptionScreenFrom);
        }

        /// <summary>
        /// Invoked when an item is selected.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void ItemSelected(object obj)
        {
            _navigationService.NavigateTo(App.MyCertificate);
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
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }

            return amountWithComma;
        }

        public void PopulateReturnsInformation()
        {
            ObservableCollection<ReturnInfo> _returnInfoItems = new ObservableCollection<ReturnInfo>();
            ReturnInfoItems = new ObservableCollection<ReturnInfo>();

            try
            {

                if (DashboardData != null)
                {
                    if (DashboardData.data != null && DashboardData.data.Count > 0)
                    {
                        if (DashboardData.data[0] != null && DashboardData.data[0].returnTotalNumber != null)
                        {
                            ReturnInfo objReturnInfoRtnTot = new ReturnInfo();
                            objReturnInfoRtnTot.ReturnTypeProperty = ReturnType.RtnTot;

                            String RtnTotstr = DashboardData.data[0].returnTotalNumber.TrimStart(new Char[] { '0' });

                            if (string.IsNullOrEmpty(RtnTotstr))
                            {
                                RtnTotstr = "0";
                            }
                            else
                            {
                                string returnToString = RtnTotstr.Substring(0, 1);
                                if (returnToString.Equals("."))
                                {
                                    RtnTotstr = "0" + RtnTotstr;
                                }
                                else
                                {
                                }
                            }


                            objReturnInfoRtnTot.ReturnCount = RtnTotstr;

                            objReturnInfoRtnTot.BackgroundGradientStart = "{StaticResource Primary}";
                            objReturnInfoRtnTot.BackgroundGradientEnd = "#b6e7fc";
                            objReturnInfoRtnTot.iConImagePath = "sf_ic_Submited_Returns_White.png";
                            objReturnInfoRtnTot.ReturnTypeName = AppResources.Submitted;
                            _returnInfoItems.Add(objReturnInfoRtnTot);
                            ReturnInfoItems = _returnInfoItems;
                            // ReturnInfoItems.Add(objReturnInfoRtnTot);
                        }

                        if (DashboardData.data[0] != null && DashboardData.data[0].nonSubmittedReturnTotalNumber != null)
                        {
                            ReturnInfo objReturnInfoNrtnTot = new ReturnInfo();
                            objReturnInfoNrtnTot.ReturnTypeProperty = ReturnType.NrtnTot;

                            String NrtnTotstr = DashboardData.data[0].nonSubmittedReturnTotalNumber.TrimStart(new Char[] { '0' });
                            if (string.IsNullOrEmpty(NrtnTotstr))
                            {
                                NrtnTotstr = "0";
                            }
                            else if (NrtnTotstr.Substring(0, 1) == ".")
                            {
                                NrtnTotstr = "0" + NrtnTotstr;
                            }
                            objReturnInfoNrtnTot.ReturnCount = NrtnTotstr;

                            objReturnInfoNrtnTot.BackgroundGradientStart = "{StaticResource EntryTextColor}";
                            objReturnInfoNrtnTot.BackgroundGradientEnd = "#DFE1E2";
                            objReturnInfoNrtnTot.iConImagePath = "sf_ic_Unsubmited_Returns_White.png";
                            objReturnInfoNrtnTot.ReturnTypeName = AppResources.UnSubmitted;

                            ReturnInfoItems.Add(objReturnInfoNrtnTot);

                        }


                        if (DashboardData.data[0] != null && DashboardData.data[0].dueTotalNumber != null)
                        {

                            ReturnInfo objReturnInfoDueIcr = new ReturnInfo();
                            objReturnInfoDueIcr.ReturnTypeProperty = ReturnType.DueIcr;

                            String DueIcrstr = DashboardData.data[0].dueTotalNumber.TrimStart(new Char[] { '0' });
                            if (string.IsNullOrEmpty(DueIcrstr))
                            {
                                DueIcrstr = "0";
                            }
                            else if (DueIcrstr.Substring(0, 1) == ".")
                            {
                                DueIcrstr = "0" + DueIcrstr;
                            }
                            objReturnInfoDueIcr.ReturnCount = DueIcrstr;

                            objReturnInfoDueIcr.BackgroundGradientStart = "#e84941";
                            objReturnInfoDueIcr.BackgroundGradientEnd = "#EECED1";
                            objReturnInfoDueIcr.iConImagePath = "sf_ic_Overdue_Returns_White.png";
                            objReturnInfoDueIcr.ReturnTypeName = AppResources.OverDue;

                            ReturnInfoItems.Add(objReturnInfoDueIcr);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }
        }

        public void PopulateBillsInformation()
        {
            BillsInfoItems = new ObservableCollection<BillInfo>();

            try
            {
                if (DashboardData.data != null && DashboardData.data.Count > 0)
                {

                    //Paid Bills
                    if (DashboardData.data[0] != null && DashboardData.data[0].paidBillsTotalNumber != null)
                    {
                        BillInfo objBillInfoPbillsTot = new BillInfo();

                        objBillInfoPbillsTot.BillTypeProperty = BillType.PbillsTot;

                        String PaidBillsstr = DashboardData.data[0].paidBillsTotalNumber.TrimStart(new Char[] { '0' });
                        String PaidBillsAmountstr = DashboardData.data[0].paidBillsTotalNumber.TrimStart(new Char[] { '0' });

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

                        objBillInfoPbillsTot.BackgroundGradientStart = "{StaticResource Primary}";
                        objBillInfoPbillsTot.BackgroundGradientEnd = "#b6e7fc";
                        objBillInfoPbillsTot.iConImagePath = "sf_ic_Paid.png";
                        objBillInfoPbillsTot.BillTypeName = AppResources.Paid;


                        BillsInfoItems.Add(objBillInfoPbillsTot);
                    }

                    //Partially Paid Bills

                    if (DashboardData.data[0] != null && DashboardData.data[0].partialBillsTotalNumber != null)
                    {
                        BillInfo objBillInfoPrbillsTot = new BillInfo();

                        objBillInfoPrbillsTot.BillTypeProperty = BillType.PrbillsTot;
                        String PartialPaidBillsstr = DashboardData.data[0].partialBillsTotalNumber.TrimStart(new Char[] { '0' });
                        String PartialPaidBillsAmountstr = DashboardData.data[0].partialBillsTotalNumber.TrimStart(new Char[] { '0' });

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

                        objBillInfoPrbillsTot.BackgroundGradientStart = "{StaticResource Secondary}";
                        objBillInfoPrbillsTot.BackgroundGradientEnd = "#F7EBD4";
                        objBillInfoPrbillsTot.iConImagePath = "sf_ic_Partially_Paid.png";
                        objBillInfoPrbillsTot.BillTypeName = AppResources.Partial;

                        BillsInfoItems.Add(objBillInfoPrbillsTot);
                    }

                    //Unpaid Bills
                    if (DashboardData.data[0] != null && DashboardData.data[0].unpaidBillsTotalNumber != null)
                    {
                        BillInfo objBillInfoUpbillsTot = new BillInfo();

                        objBillInfoUpbillsTot.BillTypeProperty = BillType.UpbillsTot;
                        String UnpaidBillsstr = DashboardData.data[0].unpaidBillsTotalNumber.TrimStart(new Char[] { '0' });
                        String UnpaidBillsAmountstr = DashboardData.data[0].unpaidBillsTotalNumber.TrimStart(new Char[] { '0' });
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

                        objBillInfoUpbillsTot.BackgroundGradientStart = " #e84941";
                        objBillInfoUpbillsTot.BackgroundGradientEnd = "#EECED1";
                        objBillInfoUpbillsTot.iConImagePath = "sf_ic_Unpaid.png";
                        objBillInfoUpbillsTot.BillTypeName = AppResources.UnPaid;

                        BillsInfoItems.Add(objBillInfoUpbillsTot);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }
        }

        public void PopulateBillsAndReturnsSchedule()
        {
            try
            {
                BillsAndReturnsSchedule = new CalendarEventCollection();

                listofPaymentReturn = new List<OverduePaymentAndUnSubmittedReturn>();
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
                    CalendarInlineEvent BillOrReturnDueEvent = new CalendarInlineEvent();

                    BillOrReturnDueEvent.StartTime = item.DueDateDateTime;
                    BillOrReturnDueEvent.EndTime = item.DueDateDateTime;

                    if (item.ICRStatus == "O")
                    {
                        BillOrReturnDueEvent.Subject = item.inboundCorrespondenceTypeDescription + " | " + AppResources.SADADNumber + " : " + item.formBundleNumber + " | " + AppResources.ZStatus + " : " + item.ICRStatus + " | " + AppResources.ZSAR + " " + item.amount
                            + " | " + item.periodDescription;

                        BillOrReturnDueEvent.Color = (Color)Application.Current.Resources["ErrorColor"];
                    }
                    else
                    {
                        BillOrReturnDueEvent.Subject = item.inboundCorrespondenceTypeDescription + " | " + AppResources.SADADNumber + " : " + item.formBundleNumber + " | " + AppResources.ZStatus + " : " + item.ICRStatus + " | " + item.periodDescription;
                        BillOrReturnDueEvent.Color = (Color)Application.Current.Resources["ForgotPasswordGrayTextColor"];
                    }

                    BillsAndReturnsSchedule.Add(BillOrReturnDueEvent);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write(ex.StackTrace.ToString());
            }
        }

        public void PopulateeServicesApplicableToTheTaxPayer()
        {
            //Call the API to get the eSevrices applicable to the TP

            eServicesAvailableToTheTP = new ObservableCollection<eServiceInfo>();
            if (DashboardData.data[0].taxpayerType != null && DashboardData.data[0].taxpayerType != "")
            {
                UtilityManager.TPTaxAvalable = DashboardData.data[0].taxpayerType;
                UtilityManager.IsZakatAvailable = DashboardData.data[0].estimateZakat;
                string[] TpTypes = DashboardData.data[0].taxpayerType.Split(',');
                foreach (string ItemType in TpTypes)
                {
                    if (ItemType == "05" && DashboardData.data[0].estimateZakat == "X")
                    {

                        eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.EstimateZakat, BackgroundGradientStart = "{StaticResource Primary}", BackgroundGradientEnd = "#b6e7fc", iConImagePath = "sf_Estimated_Zakat_Returns.png" });

                    }
                    if (ItemType == "03" || ItemType == "13")
                    {
                        eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.VatReturns, BackgroundGradientStart = "{StaticResource Primary}", BackgroundGradientEnd = "#b6e7fc", iConImagePath = "sf_VAT_Declaration.png" });
                    }

                }
            }

            eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZZFormBundleStatus, BackgroundGradientStart = "{StaticResource Primary}", BackgroundGradientEnd = "#b6e7fc", iConImagePath = "sf_Form_Bundle_Status.png" });
            eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.MyBills, BackgroundGradientStart = "{StaticResource Primary}", BackgroundGradientEnd = "#b6e7fc", iConImagePath = "sf_My_Bills.png" });
            eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.MyCertificate, BackgroundGradientStart = "{StaticResource Primary}", BackgroundGradientEnd = "#b6e7fc", iConImagePath = "sf_My_Certificate.png" });
            eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZTINStatus, BackgroundGradientStart = "{StaticResource Primary}", BackgroundGradientEnd = "#b6e7fc", iConImagePath = "sf_TIN_Status.png" });
            //eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZZCorrespondence, BackgroundGradientStart = "{StaticResource Primary}", BackgroundGradientEnd = "#b6e7fc", iConImagePath = "sf_Correspondence.png" });
            eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZTEReportReportScreenTitle, BackgroundGradientStart = "{StaticResource Primary}", BackgroundGradientEnd = "#b6e7fc", iConImagePath = "sf_Tax_Evasion.png" });
            eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.VATLookup, BackgroundGradientStart = "{StaticResource Primary}", BackgroundGradientEnd = "#b6e7fc", iConImagePath = "sf_VAT_Lookup.png" });
        }

        public void NavigateToMyBills(BillInfo billInfo)
        {
            _navigationService.NavigateTo(App.MyBillsView, billInfo);
        }
    }

    #endregion
}

