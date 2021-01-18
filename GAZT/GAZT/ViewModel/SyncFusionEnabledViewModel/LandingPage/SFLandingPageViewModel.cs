using EGAZT;
using EGAZT.Enums;
using EGAZT.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
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
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.SFLandingPage_ViewModel
{
    /// <summary>
    /// ViewModel for article list page.
    /// </summary> 
    [Preserve(AllMembers = true)]
    public class SFLandingPageViewModel : ViewModelBase
    {
        #region Fields
        private Dashboard DashboardData = null;
        public List<OverduePaymentAndUnSubmittedReturn> _listUnsubmittedReturn = null;
        public List<OverduePaymentAndUnSubmittedReturn> _listOverduePaymentReturn = null;
        public List<OverduePaymentAndUnSubmittedReturn> _listofPaymentReturn = null;
        public List<OverduePaymentAndUnSubmittedReturn> _listofPaymentReturnFive = null;
        public List<OverduePaymentAndUnSubmittedReturn> _listofPaymentReturnThree = null;
        public bool _isButtonEnabled = true;
        private ObservableCollection<eServiceInfo> _eServicesItems = null;
        private ObservableCollection<ReturnInfo> _ReturnInfoItems = null;
        private ObservableCollection<BillInfo> _PaymentInfoItems = null;
        private CalendarEventCollection _BillsAndReturnsSchedule = null;
        private bool _isListviewVisible = false;
        private bool _isNoDuesLabelVisible = false;
        private ICommand EserviceCommand { get; set; }
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public DateTime lastTapped;
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance for the <see cref="LandingPageViewModel" /> class.
        /// </summary>
        /// 
        public async Task LoadDashboardData()
        {
            //await Task.Run(() =>
            //{
            //    IsLoading = true;
            //});
            await Task.Delay(3000);
            Task GetDashboardDataTask = null;
            //Task GetUnsubmittedReturnDataTask = null;
            //Task GetOverduePaymentDataTask = null;
            if (App.TP != null)
            {
                GetDashboardDataTask = Task.Run(() =>
                {
                    DashboardData = WebServiceManager.GAZTGetDashboardData(UtilityManager.GetLanguageParameter(), App.TP.Userid);
                });
                //GetUnsubmittedReturnDataTask = Task.Run(async() =>
                //{
                //    listUnsubmittedReturn =await WebServiceManager.GAZTGetUnSubmittedReturnSetForDashboardData(App.IsArabic ? "A" : "E", App.TP.Userid);
                //});
                //GetOverduePaymentDataTask = Task.Run(async() =>
                //{
                //    listOverduePaymentReturn = await WebServiceManager.GAZTGetPaymentOverdueSetForDashboardData(App.IsArabic ? "A" : "E", App.TP.Userid);
                //});
            }
            try
            {
                if (GetDashboardDataTask != null)
                    GetDashboardDataTask.Wait();
                //GetUnsubmittedReturnDataTask.Wait();
                //GetOverduePaymentDataTask.Wait();
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
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            if (MessageForTheUser == AppResources.ZZInternetConnectionMessage)
                            {
                                await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                _navigationService.GoBack();
                            }
                            else if (MessageForTheUser == AppResources.NetworkConnectivityIssue)
                            {
                                await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                _navigationService.GoBack();
                            }
                            else if (MessageForTheUser == AppResources.ZYourSessionhasexpiredPleaseLoginagain)
                            {
                                PopToRootPage();
                            }
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
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZYourSessionhasexpiredPleaseLoginagain, AppResources.Information);
                    PopToRootPage();
                });
            }
            catch (Exception)
            {
                IsLoading = false;
                Device.BeginInvokeOnMainThread(async () =>
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
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    foreach (var item in _navigation.NavigationStack)
                    {
                        if (item.GetType().Name == App.SFLoginPageView)
                        {
                            _navigation.RemovePage(item);
                            break;
                        }
                    }
                    _navigationService.NavigateTo(App.SFLoginPageView);
                    _navigation.NavigationStack.ToList().Clear();
                    //var _navigation = Application.Current.MainPage.Navigation;
                    //_navigation.PopToRootAsync();
                });
            }
        }
        public async Task DuesData()
        {
            try
            {
                listOverduePaymentReturn = new List<OverduePaymentAndUnSubmittedReturn>();
                listUnsubmittedReturn = new List<OverduePaymentAndUnSubmittedReturn>();
                try
                {
                    listOverduePaymentReturn = await WebServiceManager.GAZTGetPaymentOverdueSetForDashboardData(App.IsArabic ? "A" : "E", App.TP.Userid);
                }
                catch (Exception ex)
                {
                    //Device.BeginInvokeOnMainThread(async () =>
                    //{
                    //    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    //});
                }
                listUnsubmittedReturn = await WebServiceManager.GAZTGetUnSubmittedReturnSetForDashboardData(App.IsArabic ? "A" : "E", App.TP.Userid);
                listofPaymentReturn = new List<OverduePaymentAndUnSubmittedReturn>();
                listofPaymentReturn.Clear();
                // Create events
                foreach (var PaymentReturn in listOverduePaymentReturn)
                {
                    PaymentReturn.IsUnSubmittedReturn = false;
                    PaymentReturn.IsPaymentOverdue = true;
                    PaymentReturn.ColorCode = Color.FromHex("#AA0C19");
                    listofPaymentReturn.Add(PaymentReturn);
                }
                foreach (var UnsubmittedReturn in listUnsubmittedReturn)
                {
                    UnsubmittedReturn.IsUnSubmittedReturn = true;
                    UnsubmittedReturn.IsPaymentOverdue = false;
                    UnsubmittedReturn.ColorCode = Color.FromHex("#5D6770");
                    listofPaymentReturn.Add(UnsubmittedReturn);
                }
                if (listofPaymentReturn != null)
                {
                    DateTime Today = DateTime.Now;
                    listofPaymentReturn = listofPaymentReturn.Where(a => a.DueDateDateTime.Date >= Today.Date).ToList<OverduePaymentAndUnSubmittedReturn>();
                    if (listofPaymentReturn.Count > 3)
                    {
                        if (listofPaymentReturn.Count == 4)
                        {
                            listofPaymentReturn = listofPaymentReturn.OrderBy(a => a.DueDateDateTime).Take(4).ToList<OverduePaymentAndUnSubmittedReturn>();
                        }
                        else
                        {
                            listofPaymentReturn = listofPaymentReturn.OrderBy(a => a.DueDateDateTime).Take(5).ToList<OverduePaymentAndUnSubmittedReturn>();
                        }
                        if (listofPaymentReturn.Count > 3)
                        {
                            if (listofPaymentReturn[2].DueDateDateTime.Date == listofPaymentReturn[3].DueDateDateTime.Date)
                            {
                                if (listofPaymentReturn.Count > 4)
                                {
                                    if (listofPaymentReturn[2].DueDateDateTime.Date == listofPaymentReturn[4].DueDateDateTime.Date)
                                    {
                                    }
                                    else
                                    {
                                        listofPaymentReturn.RemoveAt(4);
                                    }
                                }
                            }
                            else
                            {
                                if (listofPaymentReturn.Count > 3)
                                {
                                    listofPaymentReturn.RemoveAt(3);
                                }
                                if (listofPaymentReturn.Count > 3)
                                {
                                    listofPaymentReturn.RemoveAt(3);
                                }
                            }
                        }
                    }
                }
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
            catch (GAZTSessionExpiredException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZYourSessionhasexpiredPleaseLoginagain, AppResources.Information);
                    PopToRootPage();
                });
            }
            catch (Exception ex)
            {
                IsLoading = false;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    PopToRootPage();
                });
            }
        }
        public SFLandingPageViewModel(INavigationService navigationService, IDialogService dialogService) //: base(navigationService, dialogService)
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
        public List<OverduePaymentAndUnSubmittedReturn> listUnsubmittedReturn
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
        public List<OverduePaymentAndUnSubmittedReturn> listOverduePaymentReturn
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
        public List<OverduePaymentAndUnSubmittedReturn> listofPaymentReturn
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
        private List<OverduePaymentAndUnSubmittedReturn> _commitmentReturnsList;
        public List<OverduePaymentAndUnSubmittedReturn> CommitmentReturnsList
        {
            get
            {
                return this._commitmentReturnsList;
            }
            set
            {
                this._commitmentReturnsList = value;
                this.RaisePropertyChanged("CommitmentReturnsList");
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
                this.RaisePropertyChanged("IsButtonEnabled");
            }
        }
        public bool IsListviewVisible
        {
            get
            {
                return this._isListviewVisible;
            }
            set
            {
                this._isListviewVisible = value;
                this.RaisePropertyChanged("IsListviewVisible");
            }
        }
        public bool IsNoDuesLabelVisible
        {
            get
            {
                return this._isNoDuesLabelVisible;
            }
            set
            {
                this._isNoDuesLabelVisible = value;
                this.RaisePropertyChanged("IsNoDuesLabelVisible");
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
            if (Device.RuntimePlatform == Device.iOS)
            {
                if (App.IsArabic)
                {
                    MessagingCenter.Send(this, AppSettings.TransitionMessage, TransitionType.SlideFromRight);
                }
                else
                {
                    MessagingCenter.Send(this, AppSettings.TransitionMessage, TransitionType.SlideFromLeft);

                }
            }
            else
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    MessagingCenter.Send(this, AppSettings.TransitionMessage, TransitionType.None);
                }
                else
                {
                    ComingToOptionScreenFrom comingToOptionScreenFrom = ComingToOptionScreenFrom.IsDashboardPage;
                    _navigationService.NavigateTo(App.SFOptionsPageView, comingToOptionScreenFrom);

                }
            }
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
            }
            return amountWithComma;
        }
        public async Task SetDataForDues()
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });
            await Task.Run(async () =>
            {
                listUnsubmittedReturn = await WebServiceManager.GAZTGetUnSubmittedReturnSetForDashboardData(App.IsArabic ? "A" : "E", App.TP.Userid);
                listOverduePaymentReturn = await WebServiceManager.GAZTGetPaymentOverdueSetForDashboardData(App.IsArabic ? "A" : "E", App.TP.Userid);
                listofPaymentReturn = new List<OverduePaymentAndUnSubmittedReturn>();
                listofPaymentReturn.Clear();
                // Create events
                foreach (var PaymentReturn in listOverduePaymentReturn)
                {
                    PaymentReturn.IsUnSubmittedReturn = false;
                    PaymentReturn.IsPaymentOverdue = true;
                    listofPaymentReturn.Add(PaymentReturn);
                }
                foreach (var UnsubmittedReturn in listUnsubmittedReturn)
                {
                    UnsubmittedReturn.IsUnSubmittedReturn = true;
                    UnsubmittedReturn.IsPaymentOverdue = false;
                    listofPaymentReturn.Add(UnsubmittedReturn);
                }
            });
            await Task.Run(() =>
            {
                IsLoading = true;
            });
        }
        public void PopulateReturnsInformation()
        {
            ObservableCollection<ReturnInfo> _returnInfoItems = new ObservableCollection<ReturnInfo>();
            ReturnInfoItems = new ObservableCollection<ReturnInfo>();
            try
            {
                if (DashboardData != null)
                {
                    if (DashboardData.results != null && DashboardData.results.Count > 0)
                    {
                        if (DashboardData.results[0] != null && DashboardData.results[0].RtnTot != null)
                        {
                            ReturnInfo objReturnInfoRtnTot = new ReturnInfo();
                            objReturnInfoRtnTot.ReturnTypeProperty = GAZT.Models.ReturnType.RtnTot;
                            String RtnTotstr = DashboardData.results[0].RtnTot.TrimStart(new Char[] { '0' });
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
                            objReturnInfoRtnTot.BackgroundGradientStart = "#006450";
                            objReturnInfoRtnTot.BackgroundGradientEnd = "#CCE0DC";
                            objReturnInfoRtnTot.iConImagePath = "sf_ic_Submited_Returns_White.png";
                            objReturnInfoRtnTot.ReturnTypeName = AppResources.Submitted;
                            _returnInfoItems.Add(objReturnInfoRtnTot);
                            ReturnInfoItems = _returnInfoItems;
                            // ReturnInfoItems.Add(objReturnInfoRtnTot);
                        }
                        if (DashboardData.results[0] != null && DashboardData.results[0].NrtnTot != null)
                        {
                            ReturnInfo objReturnInfoNrtnTot = new ReturnInfo();
                            objReturnInfoNrtnTot.ReturnTypeProperty = GAZT.Models.ReturnType.NrtnTot;
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
                            objReturnInfoNrtnTot.ReturnTypeName = AppResources.UnSubmitted;
                            ReturnInfoItems.Add(objReturnInfoNrtnTot);
                        }
                        if (DashboardData.results[0] != null && DashboardData.results[0].DueIcr != null)
                        {
                            ReturnInfo objReturnInfoDueIcr = new ReturnInfo();
                            objReturnInfoDueIcr.ReturnTypeProperty = GAZT.Models.ReturnType.DueIcr;
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
            }
            catch (Exception ex)
            {
            }
        }
        public void PopulateBillsInformation()
        {
            BillsInfoItems = new ObservableCollection<BillInfo>();
            try
            {
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
            catch (Exception ex)
            {
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
                    PaymentReturn.IsUnSubmittedReturn = false;
                    PaymentReturn.IsPaymentOverdue = true;
                    listofPaymentReturn.Add(PaymentReturn);
                }
                foreach (var UnsubmittedReturn in listUnsubmittedReturn)
                {
                    UnsubmittedReturn.IsUnSubmittedReturn = true;
                    UnsubmittedReturn.IsPaymentOverdue = false;
                    listofPaymentReturn.Add(UnsubmittedReturn);
                }
                // listofPaymentReturn = listofPaymentReturn.Union(listOverduePaymentReturn).ToList();
                //foreach (var item in listofPaymentReturn)
                //{
                //    CalendarInlineEvent BillOrReturnDueEvent = new CalendarInlineEvent();
                //    BillOrReturnDueEvent.StartTime = item.DueDt;
                //    BillOrReturnDueEvent.EndTime = item.DueDt;
                //    if (item.IcrStatus == "O")
                //    {
                //        BillOrReturnDueEvent.Subject = item.Incotext + " | " + AppResources.SADADNumber + " : " + item.Fbnum + " | " + AppResources.ZStatus + " : " + item.IcrStatus + " | " + AppResources.ZSAR + " " + item.Amount
                //            + " | " + item.Txt50;
                //        BillOrReturnDueEvent.Color = Color.FromHex("#AA0C19");
                //    }
                //    else
                //    {
                //        BillOrReturnDueEvent.Subject = item.Incotext + " | " + AppResources.SADADNumber + " : " + item.Fbnum + " | " + AppResources.ZStatus + " : " + item.IcrStatus + " | " + item.Txt50;
                //        BillOrReturnDueEvent.Color = Color.FromHex("#7D858D");
                //    }
                //    BillsAndReturnsSchedule.Add(BillOrReturnDueEvent);
                //}
            }
            catch (Exception ex)
            {

            }
        }

        public async Task VerifyCommandClick()
        {
            try
            {
                string mobno = string.Empty;

                ComingToOTPVerificationScreenFromAndNavigatingTo tesmobnoscreen = new ComingToOTPVerificationScreenFromAndNavigatingTo();
                tesmobnoscreen.tes = "1";
                tesmobnoscreen._ComingToOTPVerificationScreenFrom = ComingToOTPVerificationScreenFrom.IsTes;

                if (App.TP != null && App.TP.Mobile != null)
                {
                    mobno = App.TP.Mobile;

                    if (mobno.StartsWith("00"))
                    {
                        mobno = mobno.Remove(0, 2);
                        mobno = "+" + mobno;
                    }
                }

                tesmobnoscreen.MobileNumber = mobno;

                TaxEvasionSendSmsModel taxEvasionSendSmsModel = new TaxEvasionSendSmsModel();
                taxEvasionSendSmsModel.mobile = mobno;
                try
                {
                    TaxEvasionSendSmsResponseModel taxEvasionSendSmsResponseModel = await WebServiceManager.GAZTTaxEvasionSendSms(taxEvasionSendSmsModel);
                    if (taxEvasionSendSmsResponseModel.Status == true)
                    {
                        App.TaxEvasionUserData = new TaxEvasionUserRegistrationResponseData();
                        App.TaxEvasionUserData.Mobile = mobno;
                        App.TaxEvasionUserData.LoginKey = taxEvasionSendSmsResponseModel.Data.Key;

                        _navigationService.NavigateTo(App.OTPPageView, tesmobnoscreen);
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        });
                    }
                }
                catch (GAZTException gex)
                {
                    // Handle the GAZT custom exception.
                    string MessageForTheUser = gex.Message;
                    if (gex is GAZTInvalidDataException)
                    {
                        MessageForTheUser = AppResources.ZZSomethingwentwrong;
                    }

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

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Task.Run(() =>
                        {
                            IsLoading = false;
                        });

                        await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                        //viewModel._navigationService.GoBack();
                    });
                }
                catch (Exception ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Task.Run(() =>
                        {
                            IsLoading = false;
                        });

                        await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        //viewModel._navigationService.GoBack();
                    });
                }
            }
            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {

                    await _dialogService.ShowMessage(AppResources.NetworkConnectivityIssue, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
            //_navigationService.NavigateTo();
        }

        public async void PopulateeServicesApplicableToTheTaxPayer()
        {
            //Call the API to get the eSevrices applicable to the TP

            try
            {
                eServicesAvailableToTheTP = new ObservableCollection<eServiceInfo>();

                if (DashboardData != null)
                {
                    if (DashboardData.results[0].TpType != null && DashboardData.results[0].TpType != "")
                    {
                        UtilityManager.TPTaxAvalable = DashboardData.results[0].TpType;
                        UtilityManager.IsZakatAvailable = DashboardData.results[0].EstimateZkat;
                        string[] TpTypes = DashboardData.results[0].TpType.Split(',');
                        foreach (string ItemType in TpTypes)
                        {
                            if (ItemType == "05" && DashboardData.results[0].EstimateZkat == "X")
                            {
                                eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.EstimateZakat, BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_Estimated_Zakat_Returns.png" });
                            }
                            if (ItemType == "03" || ItemType == "13")
                            {
                                eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.VatReturns, BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_VAT_Declaration.png" });
                            }
                        }
                    }
                }

                eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZZFormBundleStatus, BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_Form_Bundle_Status.png" });
                eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.Bills, BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_My_Bills.png" });
                eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.Certificates, BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_My_Certificate.png" });
                eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZTINStatus, BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_TIN_Status.png" });

                eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZRealEstateServiceTitle, BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_Service_6.png" });

                //eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZZCorrespondence, BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_Correspondence.png" });
                //Tax Evasion Section
                eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZTEReportReportScreenTitle, BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_Tax_Evasion.png" });
                //Tax Evasion Section
                eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZZZVatLookUpTitleTextNew, BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_VAT_Lookup.png" });
                //TaxRegistration
                //If tehe value is "X" that means the registration of the user is completed and hence we will not show the Tile.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                if (App.LoginDataRetrieved.VtReg == null)
                {
                    eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZZZZVatRegistrationTile, BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_VAT_Declaration.png" });
                }
                else if (App.LoginDataRetrieved.VtReg != "X")
                {
                    eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZZZZVatRegistrationTile, BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_VAT_Declaration.png" });
                }
            }
            catch
            {
                eServicesAvailableToTheTP.Add(new eServiceInfo { eServiceName = AppResources.ZZZZVatRegistrationTile, BackgroundGradientStart = "#006450", BackgroundGradientEnd = "#CCE0DC", iConImagePath = "sf_VAT_Declaration.png" });
            }
        }
        public void NavigateToMyBills(BillInfo billInfo)
        {
            _navigationService.NavigateTo(App.GAZTNewDesignMyBillsPageView, billInfo);
        }
    }
    #endregion
}

