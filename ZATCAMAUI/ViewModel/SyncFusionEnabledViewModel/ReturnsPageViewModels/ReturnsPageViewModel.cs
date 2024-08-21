

using System.Windows.Input;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ReturnsPageViewModels
{

    public class ReturnsListCountsByStatus
    {
        public int SubmittedZakatCount;
        public int NonSubmittedZakatCount;
        public int OverdueZakatCount;
        public int SubmittedVATCount;
        public int NonSubmittedVATCount;
        public int OverdueVATCount;
        public string SubmittedZakatWithCount = AppResources.SubmittedReturn + "10";
        private string _submittedZakatWithCountTest = AppResources.SubmittedReturn + "10";
        public string SubmittedZakatWithCountTest
        {
            get
            {
                return _submittedZakatWithCountTest;
            }
            set
            {
                _submittedZakatWithCountTest = value;
            }
        }
    }
    public class ReturnsPageViewModel : BaseViewModel
    {
        public static string EUser = string.Empty;
        public static string ReturnPeriod = "";
        public EstimatedZakatReturns estimatedZakatReturnsList { get; set; }
        private List<ICRListSet> _iCRListVATSubmitted;
        private int _headerCount = 3;
        public int HeaderCount
        {
            get
            {
                return _headerCount;
            }
            set
            {
                _headerCount = value;
                OnPropertyChanged("HeaderCount");
            }
        }
        private bool _setNoDataLabelVisibilityVATSubmitted = false;
        public bool SetNoDataLabelVisibilityVATSubmitted
        {
            get
            {
                return _setNoDataLabelVisibilityVATSubmitted;
            }
            set
            {
                _setNoDataLabelVisibilityVATSubmitted = value;
                OnPropertyChanged("SetNoDataLabelVisibilityVATSubmitted");
            }
        }
        private bool _setNoDataLabelVisibilityZakatSubmitted = false;
        public bool SetNoDataLabelVisibilityZakatSubmitted
        {
            get
            {
                return _setNoDataLabelVisibilityZakatSubmitted;
            }
            set
            {
                _setNoDataLabelVisibilityZakatSubmitted = value;
                OnPropertyChanged("SetNoDataLabelVisibilityZakatSubmitted");
            }
        }
        private bool _setNoDataLabelVisibilityVATNonSubmitted = false;
        public bool SetNoDataLabelVisibilityVATNonSubmitted
        {
            get
            {
                return _setNoDataLabelVisibilityVATNonSubmitted;
            }
            set
            {
                _setNoDataLabelVisibilityVATNonSubmitted = value;
                OnPropertyChanged("SetNoDataLabelVisibilityVATNonSubmitted");
            }
        }
        private bool _setNoDataLabelVisibilityZakatNonSubmitted = false;
        public bool SetNoDataLabelVisibilityZakatNonSubmitted
        {
            get
            {
                return _setNoDataLabelVisibilityZakatNonSubmitted;
            }
            set
            {
                _setNoDataLabelVisibilityZakatNonSubmitted = value;
                OnPropertyChanged("SetNoDataLabelVisibilityZakatNonSubmitted");
            }
        }
        private bool _setNoDataLabelVisibilityVATOverDue = false;
        public bool SetNoDataLabelVisibilityVATOverDue
        {
            get
            {
                return _setNoDataLabelVisibilityVATOverDue;
            }
            set
            {
                _setNoDataLabelVisibilityVATOverDue = value;
                OnPropertyChanged("SetNoDataLabelVisibilityVATOverDue");
            }
        }
        private bool _setNoDataLabelVisibilityZakatOverDue = false;
        public bool SetNoDataLabelVisibilityZakatOverDue
        {
            get
            {
                return _setNoDataLabelVisibilityZakatOverDue;
            }
            set
            {
                _setNoDataLabelVisibilityZakatOverDue = value;
                OnPropertyChanged("SetNoDataLabelVisibilityZakatOverDue");
            }
        }
        private bool _setNoDataLabelVisibilityVATSubmittedList = false;
        public bool SetNoDataLabelVisibilityVATSubmittedList
        {
            get
            {
                return _setNoDataLabelVisibilityVATSubmittedList;
            }
            set
            {
                _setNoDataLabelVisibilityVATSubmittedList = value;
                OnPropertyChanged("SetNoDataLabelVisibilityVATSubmittedList");
            }
        }
        private bool _setNoDataLabelVisibilityZakatSubmittedList = false;
        public bool SetNoDataLabelVisibilityZakatSubmittedList
        {
            get
            {
                return _setNoDataLabelVisibilityZakatSubmittedList;
            }
            set
            {
                _setNoDataLabelVisibilityZakatSubmittedList = value;
                OnPropertyChanged("SetNoDataLabelVisibilityZakatSubmittedList");
            }
        }
        private bool _setNoDataLabelVisibilityVATNonSubmittedList = false;
        public bool SetNoDataLabelVisibilityVATNonSubmittedList
        {
            get
            {
                return _setNoDataLabelVisibilityVATNonSubmittedList;
            }
            set
            {
                _setNoDataLabelVisibilityVATNonSubmittedList = value;
                OnPropertyChanged("SetNoDataLabelVisibilityVATNonSubmittedList");
            }
        }
        private bool _setNoDataLabelVisibilityZakatNonSubmittedList = false;
        public bool SetNoDataLabelVisibilityZakatNonSubmittedList
        {
            get
            {
                return _setNoDataLabelVisibilityZakatNonSubmittedList;
            }
            set
            {
                _setNoDataLabelVisibilityZakatNonSubmittedList = value;
                OnPropertyChanged("SetNoDataLabelVisibilityZakatNonSubmittedList");
            }
        }
        private bool _setNoDataLabelVisibilityVATOverDueList = false;
        public bool SetNoDataLabelVisibilityVATOverDueList
        {
            get
            {
                return _setNoDataLabelVisibilityVATOverDueList;
            }
            set
            {
                _setNoDataLabelVisibilityVATOverDueList = value;
                OnPropertyChanged("SetNoDataLabelVisibilityVATOverDueList");
            }
        }
        private bool _setNoDataLabelVisibilityZakatOverDueList = false;
        public bool SetNoDataLabelVisibilityZakatOverDueList
        {
            get
            {
                return _setNoDataLabelVisibilityZakatOverDueList;
            }
            set
            {
                _setNoDataLabelVisibilityZakatOverDueList = value;
                OnPropertyChanged("SetNoDataLabelVisibilityZakatOverDueList");
            }
        }
        private bool _isVATVisible = false;
        public bool IsVATVisible
        {
            get
            {
                return _isVATVisible;
            }
            set
            {
                _isVATVisible = value;
                OnPropertyChanged("IsVATVisible");
            }
        }
        private bool _isZakatVisible = false;
        public bool IsZakatVisible
        {
            get
            {
                return _isZakatVisible;
            }
            set
            {
                _isZakatVisible = value;
                OnPropertyChanged("IsZakatVisible");
            }
        }
        private ReturnsListCountsByStatus _returnsListCountsByStatus;
        public ReturnsListCountsByStatus ReturnsListCountsByStatus
        {
            get
            {
                return _returnsListCountsByStatus;
            }
            set
            {
                _returnsListCountsByStatus = value;
                OnPropertyChanged("ReturnsListCountsByStatus");
            }
        }

        public List<ICRListSet> ICRListVATSubmitted
        {
            get
            {
                return _iCRListVATSubmitted;
            }
            set
            {
                _iCRListVATSubmitted = value;
                OnPropertyChanged("ICRListVATSubmitted");
            }
        }
        private List<ICRListSet> _iCRListVATNonSubmitted;
        public List<ICRListSet> ICRListVATNonSubmitted
        {
            get
            {
                return _iCRListVATNonSubmitted;
            }
            set
            {
                _iCRListVATNonSubmitted = value;
                OnPropertyChanged("ICRListVATNonSubmitted");
            }
        }
        private List<ICRListSet> _iCRListVATOverDue;
        public List<ICRListSet> ICRListVATOverDue
        {
            get
            {
                return _iCRListVATOverDue;
            }
            set
            {
                _iCRListVATOverDue = value;
                OnPropertyChanged("ICRListVATOverDue");
            }
        }
        private ICRListSet _selectedICRVATSubmitted;
        public ICRListSet SelectedICRVATSubmitted
        {
            get
            {
                return _selectedICRVATSubmitted;
            }
            set
            {
                try
                {
                    _selectedICRVATSubmitted = value;
                    if (_selectedICRVATSubmitted != null)
                    {
                        GetVATAllReturnsAsync();
                    }
                    OnPropertyChanged("SelectedICRVATSubmitted");
                }
                catch (Exception)
                {


                }
            }
        }
        private EstimatedZakatReturnsResult _selectedZakatReturn;
        public EstimatedZakatReturnsResult SelectedZakatReturn
        {
            get
            {
                return _selectedZakatReturn;
            }
            set
            {
                _selectedZakatReturn = value;
                OnPropertyChanged("SelectedZakatReturn");
                if (SelectedZakatReturn != null)// FZ12 to check that the selected return belongs to Form 12 return
                {
                    EstimatedZakatReturnsResult ChidlSelectedICR = new EstimatedZakatReturnsResult();
                    ChidlSelectedICR = SelectedZakatReturn;
                    if (ChidlSelectedICR != null && ChidlSelectedICR.Fbtyp != null)
                    {
                        if (ChidlSelectedICR.Fbtyp.Equals("FZ12"))
                        {
                            ReturnPeriod = ChidlSelectedICR.Period;
                            //  ReturnPeriod =UtilityManager.GetTaxPeriodDate(ReturnPeriod);
                            MainThread.BeginInvokeOnMainThread(() =>
                            {
                                _navigationService.NavigateTo(App.ZakatReturnDetailsPageView, ChidlSelectedICR.Fbnum);
                            });
                        }
                        else
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessageBox(AppResources.ZZFormFiveTappedMessage, AppResources.Information);
                            });
                        }
                    }
                }
            }
        }
        private List<EstimatedZakatReturnsResult> _myZakatReturnsSubmitted = null;
        public List<EstimatedZakatReturnsResult> MyZakatReturnsSubmitted
        {
            get
            {
                return _myZakatReturnsSubmitted;
            }
            set
            {
                _myZakatReturnsSubmitted = value;
                OnPropertyChanged("MyZakatReturnsSubmitted");
            }
        }
        private List<EstimatedZakatReturnsResult> _myZakatReturnsNonSubmitted = null;
        public List<EstimatedZakatReturnsResult> MyZakatReturnsNonSubmitted
        {
            get
            {
                return _myZakatReturnsNonSubmitted;
            }
            set
            {
                _myZakatReturnsNonSubmitted = value;
                OnPropertyChanged("MyZakatReturnsNonSubmitted");
            }
        }
        private List<EstimatedZakatReturnsResult> _myZakatReturnsOverDue = null;
        public List<EstimatedZakatReturnsResult> MyZakatReturnsOverDue
        {
            get
            {
                return _myZakatReturnsOverDue;
            }
            set
            {
                _myZakatReturnsOverDue = value;
                OnPropertyChanged("MyZakatReturnsOverDue");
            }
        }
        private int _tabIndexStatus = 0;
        public int TabIndexStatus
        {
            get
            {
                return _tabIndexStatus;
            }
            set
            {
                _tabIndexStatus = value;
                OnPropertyChanged("TabIndexStatus");
            }
        }
        private FlowDirection _fDirection = FlowDirection.RightToLeft;
        public FlowDirection FDirection
        {
            get
            {
                return _fDirection;
            }
            set
            {
                _fDirection = value;
                OnPropertyChanged("FDirection");
            }
        }
        public ReturnsPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
        }
        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    foreach (var item in _navigation.NavigationStack)
                    {
                        if (item.GetType().Name == App.SFAnonymousLandingPageView)
                        {
                            _navigation.RemovePage(item);
                            break;
                        }
                    }
                    _navigationService.NavigateTo(App.SFAnonymousLandingPageView);
                    _navigation.NavigationStack.ToList().Clear();
                });
            }
        }
        public async Task onPageLoad()
        {
            try
            {

                await Task.Run(async () =>
                {
                    if (App.IsArabic)
                    {
                        FDirection = FlowDirection.RightToLeft;
                    }
                    else
                    {
                        FDirection = FlowDirection.LeftToRight;
                    }
                    ICRListVATSubmitted = null;
                    ReturnsListCountsByStatus = new ReturnsListCountsByStatus();
                    IsVATVisible = false;
                    IsZakatVisible = false;
                    if (!string.IsNullOrEmpty(UtilityManager.TPTaxAvalable))
                    {
                        string[] TpTypes = UtilityManager.TPTaxAvalable.Split(',');
                        foreach (string ItemType in TpTypes)
                        {
                            if (ItemType == "05")
                            {
                                IsZakatVisible = true;
                            }
                            if (ItemType == "03" || ItemType == "13")
                            {
                                IsVATVisible = true;
                            }
                        }
                    }
                    HeaderCount = 3;
                    ICR icrList = null;
                    try
                    {
                        string lang = UtilityManager.GetLanguageParameter();
                        icrList = WebServiceManager.GAZTGetICRs(App.TP.Tin, lang);
                        PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                        try
                        {
                            if (icrList != null && icrList.ICR_STATUSSet != null && icrList.ICR_STATUSSet.Count != 0)
                            {
                                icrList.ICR_LISTSet = icrList.ICR_LISTSet.OrderByDescending(icr => DateTime.Parse(icr.DueDate)).ToList();
                            }
                            VATDeclaration vATDeclaration = new VATDeclaration();
                            if (icrList != null && icrList.ICR_LISTSet != null && icrList.ICR_LISTSet.Count != 0)
                            {
                                ICRListVATSubmitted = new List<ICRListSet>();
                                ICRListVATSubmitted = icrList.ICR_LISTSet.Where(a => a.Status == "E0055").ToList<ICRListSet>();
                                ICRListVATNonSubmitted = new List<ICRListSet>();
                                ICRListVATNonSubmitted = icrList.ICR_LISTSet.Where(a => a.Status == "E0001" || a.Status == "E0013").ToList<ICRListSet>();
                                DateTime Today = DateTime.Now;
                                ICRListVATOverDue = icrList.ICR_LISTSet.Where(a => a.Status != "E0045" && a.Status != "E0055" && a.DueDateDateTime < Today).ToList<ICRListSet>();
                                if (ICRListVATSubmitted.Count > 0)
                                {
                                    ReturnsListCountsByStatus.SubmittedVATCount = ICRListVATSubmitted.Count;
                                    SetNoDataLabelVisibilityVATSubmittedList = true;
                                    SetNoDataLabelVisibilityVATSubmitted = false;
                                }
                                else
                                {
                                    SetNoDataLabelVisibilityVATSubmittedList = false;
                                    SetNoDataLabelVisibilityVATSubmitted = true;
                                }
                                if (ICRListVATNonSubmitted.Count > 0)
                                {
                                    ReturnsListCountsByStatus.NonSubmittedVATCount = ICRListVATNonSubmitted.Count;
                                    SetNoDataLabelVisibilityVATNonSubmittedList = true;
                                    SetNoDataLabelVisibilityVATNonSubmitted = false;
                                }
                                else
                                {
                                    SetNoDataLabelVisibilityVATNonSubmittedList = false;
                                    SetNoDataLabelVisibilityVATNonSubmitted = true;
                                }
                                if (ICRListVATOverDue.Count > 0)
                                {
                                    ReturnsListCountsByStatus.OverdueVATCount = ICRListVATOverDue.Count;
                                    SetNoDataLabelVisibilityVATOverDueList = true;
                                    SetNoDataLabelVisibilityVATOverDue = false;
                                }
                                else
                                {
                                    SetNoDataLabelVisibilityVATOverDueList = false;
                                    SetNoDataLabelVisibilityVATOverDue = true;
                                }
                            }
                            else
                            {
                                _navigationService.GoBack();
                            }
                            try
                            {
                                estimatedZakatReturnsList = await WebServiceManager.GAZTGetEstimateZakatReturnList();
                                PopToRootPage();
                            }
                            catch (InternetException ex)
                            {
                                MainThread.BeginInvokeOnMainThread(async () =>
                                {
                                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                                });
                            }
                            List<EstimatedZakatReturnsResult> myZakatReturnsListTemp = new List<EstimatedZakatReturnsResult>();
                            myZakatReturnsListTemp = new List<EstimatedZakatReturnsResult>(GetSortedList(estimatedZakatReturnsList.d.results));
                            DateTime TodayNew = DateTime.Now;
                            List<EstimatedZakatReturnsResult> MyZakatReturnsNonSubmittedChild = new List<EstimatedZakatReturnsResult>();
                            List<EstimatedZakatReturnsResult> MyZakatReturnsSubmittedChild = new List<EstimatedZakatReturnsResult>();
                            List<EstimatedZakatReturnsResult> MyZakatReturnsOverDueChild = new List<EstimatedZakatReturnsResult>();
                            if (myZakatReturnsListTemp != null && myZakatReturnsListTemp.Count > 0)
                            {
                                for (int i = 0; i < myZakatReturnsListTemp.Count; i++)
                                {
                                    if (myZakatReturnsListTemp[i].Fbtyp.Equals("FZ12"))
                                    {
                                        if (App.IsArabic)
                                        {
                                            myZakatReturnsListTemp[i].Period = UtilityManager.GetTaxPeriodDate(myZakatReturnsListTemp[i].Period);
                                        }
                                        else
                                        {
                                            if (myZakatReturnsListTemp[i].Period.Contains("-"))
                                                myZakatReturnsListTemp[i].Period.Replace("-", "- ");
                                        }
                                        if (string.IsNullOrEmpty(myZakatReturnsListTemp[i].Statfg))
                                        {
                                            if (string.Equals(myZakatReturnsListTemp[i].Stat, "IP011"))//UnSubmitted_status, "IP011") || string.Equals(_status, "IP014") || 
                                            {
                                                myZakatReturnsListTemp[i].StatusImage = "ic_unsubmitted.png";
                                                myZakatReturnsListTemp[i].BorderColour = "#944E22";
                                                MyZakatReturnsNonSubmittedChild.Add(myZakatReturnsListTemp[i]);
                                            }
                                            else if (string.Equals(myZakatReturnsListTemp[i].Stat, "P"))//Paid|| string.Equals(_status, "I") || string.Equals(_status, "IP015")
                                            {
                                                myZakatReturnsListTemp[i].BorderColour = "#003672";
                                                myZakatReturnsListTemp[i].StatusImage = "ic_Paid.png";
                                                MyZakatReturnsSubmittedChild.Add(myZakatReturnsListTemp[i]);
                                            }
                                            if (!string.Equals(myZakatReturnsListTemp[i].Stat, "P") && !string.Equals(myZakatReturnsListTemp[i].Stat, "IP014") && Convert.ToDateTime(myZakatReturnsListTemp[i].DueDtC) < TodayNew)//In processing || string.Equals(_status, "IP019") || string.Equals(_status, "IP021") || string.Equals(_status, "E0058") || string.Equals(_status, "E0076") || string.Equals(_status, "E0077") || string.Equals(_status, "For Officer's Review") || string.Equals(_status, "E0089")
                                            {
                                                MyZakatReturnsOverDueChild.Add(myZakatReturnsListTemp[i]);
                                            }
                                        }
                                        else
                                        {
                                            if (string.Equals(myZakatReturnsListTemp[i].Statfg, "U"))//UnSubmitted_status, "IP011") || string.Equals(_status, "IP014") || 
                                            {
                                                myZakatReturnsListTemp[i].StatusImage = "ic_unsubmitted.png";
                                                myZakatReturnsListTemp[i].BorderColour = "#944E22";
                                                MyZakatReturnsNonSubmittedChild.Add(myZakatReturnsListTemp[i]);
                                            }
                                            else if (string.Equals(myZakatReturnsListTemp[i].Statfg, "P"))//Paid|| string.Equals(_status, "I") || string.Equals(_status, "IP015")
                                            {
                                                myZakatReturnsListTemp[i].BorderColour = "#003672";
                                                myZakatReturnsListTemp[i].StatusImage = "ic_Paid.png";
                                                MyZakatReturnsSubmittedChild.Add(myZakatReturnsListTemp[i]);
                                            }
                                            else if (string.Equals(myZakatReturnsListTemp[i].Statfg, "I"))//Paid|| string.Equals(_status, "I") || string.Equals(_status, "IP015")
                                            {
                                                myZakatReturnsListTemp[i].BorderColour = "#003672";
                                                myZakatReturnsListTemp[i].StatusImage = "ic_Paid.png";
                                            }
                                            if (!string.Equals(myZakatReturnsListTemp[i].Statfg, "P") && !string.Equals(myZakatReturnsListTemp[i].Statfg, "IP014") && Convert.ToDateTime(myZakatReturnsListTemp[i].DueDtC) < TodayNew)//In processing || string.Equals(_status, "IP019") || string.Equals(_status, "IP021") || string.Equals(_status, "E0058") || string.Equals(_status, "E0076") || string.Equals(_status, "E0077") || string.Equals(_status, "For Officer's Review") || string.Equals(_status, "E0089")
                                            {
                                                MyZakatReturnsOverDueChild.Add(myZakatReturnsListTemp[i]);
                                            }
                                        }
                                        //  myZakatReturnsList.Add(myZakatReturnsListTemp[i]);
                                    }
                                }
                            }
                            MyZakatReturnsNonSubmitted = MyZakatReturnsNonSubmittedChild;
                            MyZakatReturnsSubmitted = MyZakatReturnsSubmittedChild;
                            MyZakatReturnsOverDue = MyZakatReturnsOverDueChild;
                            if (MyZakatReturnsSubmitted.Count > 0)
                            {
                                ReturnsListCountsByStatus.SubmittedZakatCount = MyZakatReturnsSubmitted.Count;
                                SetNoDataLabelVisibilityZakatSubmittedList = true;
                                SetNoDataLabelVisibilityZakatSubmitted = false;
                            }
                            else
                            {
                                SetNoDataLabelVisibilityZakatSubmittedList = false;
                                SetNoDataLabelVisibilityZakatSubmitted = true;
                            }
                            if (MyZakatReturnsNonSubmitted.Count > 0)
                            {
                                ReturnsListCountsByStatus.NonSubmittedZakatCount = MyZakatReturnsNonSubmitted.Count;
                                SetNoDataLabelVisibilityZakatNonSubmittedList = true;
                                SetNoDataLabelVisibilityZakatNonSubmitted = false;
                            }
                            else
                            {
                                SetNoDataLabelVisibilityZakatNonSubmittedList = false;
                                SetNoDataLabelVisibilityZakatNonSubmitted = true;
                            }
                            if (MyZakatReturnsOverDue.Count > 0)
                            {
                                ReturnsListCountsByStatus.OverdueZakatCount = MyZakatReturnsOverDue.Count;
                                SetNoDataLabelVisibilityZakatOverDueList = true;
                                SetNoDataLabelVisibilityZakatOverDue = false;
                            }
                            else
                            {
                                SetNoDataLabelVisibilityZakatOverDueList = false;
                                SetNoDataLabelVisibilityZakatOverDue = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                                _navigationService.GoBack();
                            });
                        }
                    }
                    catch (InternetException ex)
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                            _navigationService.GoBack();
                        });
                    }
                });

            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    _navigationService.GoBack();
                });
            }
        }
        public IEnumerable<EstimatedZakatReturnsResult> GetSortedList(IList<EstimatedZakatReturnsResult> ICRList)
        {
            try
            {
                var SortedList = ICRList.OrderByDescending(x => Convert.ToDateTime(x.DueDtC).TimeOfDay)
                              .ThenBy(x => Convert.ToDateTime(x.DueDtC).Date)
                              .ThenBy(x => Convert.ToDateTime(x.DueDtC).Year);
                return SortedList.Reverse();
            }
            catch (Exception)
            {
                return ICRList;
            }
        }
        public async void GetVATAllReturnsAsync()
        {
            await GetVATAllReturns();
        }
        public bool isStatusNotValid()
        {
            bool isValid = true;
            if (SelectedICRVATSubmitted.Status == "E0020" || SelectedICRVATSubmitted.Status == "E0057" || SelectedICRVATSubmitted.Status == "E0076" || SelectedICRVATSubmitted.Status == "E0077" || SelectedICRVATSubmitted.Status == "E0078" || SelectedICRVATSubmitted.Status == "E0089" || SelectedICRVATSubmitted.Status == "E0090")
            {
                isValid = false;
            }
            return isValid;
        }
        private async Task GetVATAllReturns()
        {
            ICRListSet selectedICRForStatus = null;
            try
            {
                try
                {
                    if (SelectedICRVATSubmitted != null)
                    {
                        if (isStatusNotValid())
                        {
                            selectedICRForStatus = new ICRListSet();
                            selectedICRForStatus = SelectedICRVATSubmitted;
                            App.ICRStatus = selectedICRForStatus.Status;
                            //as per discussion with Vinay - the GUID is dynamic and will remain active and attched to ICR in a session. if the list of ICR' sis refreshed; meaning if the API is called again
                            // the GUID will be different
                            String SelectedICRGUID = SelectedICRVATSubmitted.Fbguid;
                            EUser = SelectedICRVATSubmitted.Euser;
                            VATDeclaration _vATDeclaration = await WebServiceManager.GAZTGetVATReturns(SelectedICRVATSubmitted.Fbguid, SelectedICRVATSubmitted.Fbnum, SelectedICRVATSubmitted.Euser, SelectedICRVATSubmitted.Persl);
                            PopToRootPage();
                            if (_vATDeclaration != null && _vATDeclaration.data != null)
                            {
                                _vATDeclaration.data.Fbguid = SelectedICRGUID;
                                VATDeclaration vATDeclaration = new VATDeclaration();
                                VATDeclarationD vATDeclarationD = new VATDeclarationD();
                                vATDeclaration.data = vATDeclarationD;
                                vATDeclaration.data.ADRSet = new List<Result5>();
                                MainThread.BeginInvokeOnMainThread(() =>
                                {
                                    _navigationService.NavigateTo(App.VATReturnsPageView, _vATDeclaration);
                                });
                            }
                            else
                            {
                                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                            }
                        }
                        else
                        {
                            await _dialogService.ShowMessage(AppResources.ZZZReturnUnderReview, AppResources.Information);
                        }
                    }
                }
                catch (InternetException )
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        // IsLoading = false;
                        _navigationService.GoBack();
                    });
                }
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    //  IsLoading = false;
                    _navigationService.GoBack();
                });
            }
        }
    }
}
