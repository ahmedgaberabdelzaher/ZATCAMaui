using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
using System.Globalization;
using System.Windows.Input;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Views.SyncFusionEnabledViews.ZakatReturnListPages;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ZakatReturnListPage
{

    public class ZakatReturnListPageViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnHomeButtonClicked { get; set; }
        public ICommand BackButtonClicked { get; set; }
        public EstimatedZakatReturns estimatedZakatReturnsList { get; set; }
        public List<EstimatedZakatReturnsResult> myZakatReturnsList = new List<EstimatedZakatReturnsResult>();
        public static string ReturnPeriod = "";
        public static int SelectedICRStatusWhileGoingToZAKATDetails = 13;
        #endregion
        #region Property
        private string _txtSelectedStatus = string.Empty;
        public string TxtSelectedStatus
        {
            get
            {
                return _txtSelectedStatus;
            }
            set
            {
                _txtSelectedStatus = value;
                RaisePropertyChanged("TxtSelectedStatus");
            }
        }
        private List<ZAKATStatus> _iCRStatusList;
        public List<ZAKATStatus> ICRStatusList
        {
            get
            {
                return _iCRStatusList;
            }
            set
            {
                _iCRStatusList = value;
                RaisePropertyChanged("ICRStatusList");
            }
        }
        private ZAKATStatus _selectedICRStatus;
        public ZAKATStatus SelectedICRStatus
        {
            get
            {
                return _selectedICRStatus;
            }
            set
            {
                _selectedICRStatus = value;
                RaisePropertyChanged("SelectedICR");
            }
        }
        private ZAKATStatus _selectedICRStatusPrev;
        public ZAKATStatus SelectedICRStatusPrev
        {
            get
            {
                return _selectedICRStatusPrev;
            }
            set
            {
                _selectedICRStatusPrev = value;
                RaisePropertyChanged("SelectedICRPrev");
            }
        }
        private ZAKATStatus _previousSelectedICRStatus;
        public ZAKATStatus PreviousSelectedICRStatus
        {
            get
            {
                return _previousSelectedICRStatus;
            }
            set
            {
                _previousSelectedICRStatus = value;
                RaisePropertyChanged("PreviousSelectedICRStatus");
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
                RaisePropertyChanged("SelectedZakatReturn");
                if (SelectedZakatReturn != null)// FZ12 to check that the selected return belongs to Form 12 return
                {
                    if (SelectedZakatReturn.Fbtyp.Equals("FZ12"))
                    {
                        ReturnPeriod = SelectedZakatReturn.Period;
                        SelectedICRStatusWhileGoingToZAKATDetails = SelectedICRStatus.index;
                        if (SelectedICRStatus.Value == "All" || SelectedICRStatus.Value == "All")
                        {
                            SelectedIndex = 13;
                        }

                        App.IsZakatLoadingFromMyReturns = false;
                        //ReturnPeriod =UtilityManager.GetTaxPeriodDate(ReturnPeriod);
                        _navigationService.NavigateTo(App.ZakatReturnDetailsPageView, SelectedZakatReturn.Fbguid);
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
        private List<EstimatedZakatReturnsResult> _myZakatReturns;
        public List<EstimatedZakatReturnsResult> MyZakatReturns
        {
            get
            {
                return _myZakatReturns;
            }
            set
            {
                _myZakatReturns = value;
                if (MyZakatReturns != null && MyZakatReturns.Count > 0)
                {
                    HideNoDataMessage();
                }
                RaisePropertyChanged("MyZakatReturns");
            }
        }
        private List<ZakatReturnStatus> _zakatReturnStatus;
        public List<ZakatReturnStatus> ZakatReturnStatus
        {
            get
            {
                return _zakatReturnStatus;
            }
            set
            {
                _zakatReturnStatus = value;
                RaisePropertyChanged("ZakatReturnStatus");
            }
        }
        private ZakatReturnStatus _selectedZakatStatus;
        public ZakatReturnStatus SelectedZakatStatus
        {
            get
            {
                return _selectedZakatStatus;
            }
            set
            {
                _selectedZakatStatus = value;
                RaisePropertyChanged("SelectedZakatStatus");
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
        private int _selectedIndex;
        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                _selectedIndex = value;
                RaisePropertyChanged("SelectedIndex");
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
        #endregion
        #region Constructor
        public ZakatReturnListPageViewModel(INavigationService navigationService, IDialogService dialogService)
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
            OnHomeButtonClicked = new Command(() =>
            {
                _navigationService.NavigateTo(App.SFLandingPageView);
            });
            BackButtonClicked = new Command(() =>
            {
                _navigationService.NavigateTo(App.SFLandingPageView);
            });
        }
        #endregion
        #region Method
        public async Task OnPageLoad()
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });
            await Task.Run(async () =>
            {
                try
                {
                    estimatedZakatReturnsList = await WebServiceManager.GAZTGetEstimateZakatReturnList();
                    PopToRootPage();
                    UpdateICRList();
                }
                catch (InternetException ex)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    });
                }

            });

            await Task.Run(() =>
            {
                IsLoading = false;
            });
        }
        public void GetZAKATICRStatusList()
        {
            List<ZAKATStatus> ZAKATStatusListEn = new List<ZAKATStatus>()
            {
                new ZAKATStatus{ Key ="IP011", Value = "Submitted",index = 0},
                 new ZAKATStatus{ Key ="IP014", Value = "Billed",index = 1},
                  new ZAKATStatus{ Key ="U", Value = "Unsubmitted",index = 2},
                   new ZAKATStatus{ Key ="P", Value = "Paid",index = 3},
                   new ZAKATStatus{ Key ="I", Value = "Partially paid",index = 4},
                 new ZAKATStatus{ Key ="IP015", Value = "In Processing",index = 5},
                  new ZAKATStatus{ Key ="IP017", Value = "Parked",index = 6},
                   new ZAKATStatus{ Key ="IP019", Value = "Rejected",index = 7},
                   new ZAKATStatus{ Key ="IP021", Value = "To Be Approved",index = 8},
                 new ZAKATStatus{ Key ="C0021", Value = "To Be Filled & Parked",index = 9},
                  new ZAKATStatus{ Key ="ZP017", Value = "Parked in Amendment",index = 10},
                   new ZAKATStatus{ Key ="E0089", Value = "GSTC – Escalation In Process",index = 11},
                   new ZAKATStatus{ Key ="E0090", Value = "GSTC – Escalation Completed",index = 12},
                 new ZAKATStatus{ Key ="ALL", Value = "All",index = 13},
            };
            List<ZAKATStatus> ZAKATStatusListAr = new List<ZAKATStatus>()
            {
                new ZAKATStatus{ Key ="IP011", Value = "تم تقديمه",index = 0},
                 new ZAKATStatus{ Key ="IP014", Value = "مفوتر",index = 1},
                  new ZAKATStatus{ Key ="U", Value = "لم يتم تقديمه",index = 2},
                   new ZAKATStatus{ Key ="P", Value = "مسدد",index = 3},
                   new ZAKATStatus{ Key ="I", Value = "مسدد جزئياً",index = 4},
                 new ZAKATStatus{ Key ="IP015", Value = "في طور المعالجة",index = 5},
                  new ZAKATStatus{ Key ="IP017", Value = "محفوظ كمسودة",index = 6},
                   new ZAKATStatus{ Key ="IP019", Value = "مرفوض",index = 7},
                   new ZAKATStatus{ Key ="IP021", Value = "إنتظار الموافقة",index = 8},
                 new ZAKATStatus{ Key ="C0021", Value = "جاهز للتعبئة و الحفظ كمسودة",index = 9},
                  new ZAKATStatus{ Key ="ZP017", Value = "محفوظ كمسودة تعديل",index = 10},
                   new ZAKATStatus{ Key ="E0089", Value = "الأمانة –قيد التصعيد",index = 11},
                   new ZAKATStatus{ Key ="E0090", Value = "الأمانة – انتهاء التصعيد",index = 12},
                 new ZAKATStatus{ Key ="ALL", Value = "الجميع",index = 13},
            };
            if (App.IsArabic)
            {
                ICRStatusList = ZAKATStatusListAr;
            }
            else
            {
                ICRStatusList = ZAKATStatusListEn;
            }
        }
        public void GetFilteredZAKATICRList(ZAKATStatus selectedICR)
        {
            try
            {
                List<EstimatedZakatReturnsResult> FilteredCRStatusList = new List<EstimatedZakatReturnsResult>();
                if (myZakatReturnsList != null)
                {
                    if (selectedICR.Key.Equals("ALL"))
                    {
                        MyZakatReturns = myZakatReturnsList;
                        HandleNoDataMessageVisibility(MyZakatReturns);
                    }
                    else
                    {
                        for (int i = 0; i < myZakatReturnsList.Count; i++)
                        {
                            if (string.IsNullOrEmpty(myZakatReturnsList[i].Statfg))
                            {
                                if (selectedICR.Key.Equals(myZakatReturnsList[i].Stat))
                                {
                                    FilteredCRStatusList.Add(myZakatReturnsList[i]);
                                }
                            }
                            else
                            {
                                if (selectedICR.Key.Equals(myZakatReturnsList[i].Statfg))
                                {
                                    FilteredCRStatusList.Add(myZakatReturnsList[i]);
                                }
                            }
                        }
                        MyZakatReturns = FilteredCRStatusList;
                        HandleNoDataMessageVisibility(FilteredCRStatusList);
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        private void UpdateICRList()
        {
            try
            {
                List<EstimatedZakatReturnsResult> myZakatReturnsListTemp = new List<EstimatedZakatReturnsResult>();
                myZakatReturnsListTemp = new List<EstimatedZakatReturnsResult>(GetSortedList(estimatedZakatReturnsList.d.listSet.results));
                if (myZakatReturnsListTemp != null && myZakatReturnsListTemp.Count > 0)
                {
                    for (int i = 0; i < myZakatReturnsListTemp.Count; i++)
                    {

                        if (myZakatReturnsListTemp[i].Period.Contains("-"))
                        {
                            //FormatedAbrzu = _abrzu.ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                            DateTime _dueDate = JsonConvert.DeserializeObject<DateTime>(@"""" + myZakatReturnsListTemp[i].DueDt + @"""");
                            // Convert.ToDateTime(myZakatReturnsListTemp[i].DueDt);
                            string DueDate = _dueDate.ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));

                            myZakatReturnsListTemp[i].DueDtC = DueDate;
                            DateTime _abrzu = JsonConvert.DeserializeObject<DateTime>(@"""" + myZakatReturnsListTemp[i].Abrzu + @""""); // Convert.ToDateTime(myZakatReturnsListTemp[i].Abrzu);
                            DateTime _abrzo = JsonConvert.DeserializeObject<DateTime>(@"""" + myZakatReturnsListTemp[i].Abrzo + @"""");// Convert.ToDateTime(myZakatReturnsListTemp[i].Abrzo);
                            myZakatReturnsListTemp[i].Period = _abrzu.ToString("dd-MMMM-yyyy", new CultureInfo("en-US")) + " - " + _abrzo.ToString("dd-MMMM-yyyy", new CultureInfo("en-US")); ;


                        }
                        if (string.IsNullOrEmpty(myZakatReturnsListTemp[i].Statfg))
                        {
                            if (string.Equals(myZakatReturnsListTemp[i].Stat, "IP011"))//UnSubmitted_status, "IP011") || string.Equals(_status, "IP014") || 
                            {
                                myZakatReturnsListTemp[i].StatusImage = "ic_unsubmitted.png";
                                myZakatReturnsListTemp[i].BorderColour = "#944E22";
                            }
                            else if (string.Equals(myZakatReturnsListTemp[i].Stat, "P"))//Paid|| string.Equals(_status, "I") || string.Equals(_status, "IP015")
                            {
                                myZakatReturnsListTemp[i].BorderColour = "#003672";
                                myZakatReturnsListTemp[i].StatusImage = "ic_Paid.png";
                            }
                            else if (string.Equals(myZakatReturnsListTemp[i].Stat, "IP015"))//In processing || string.Equals(_status, "IP019") || string.Equals(_status, "IP021") || string.Equals(_status, "E0058") || string.Equals(_status, "E0076") || string.Equals(_status, "E0077") || string.Equals(_status, "For Officer's Review") || string.Equals(_status, "E0089")
                            {
                                myZakatReturnsListTemp[i].BorderColour = "#c49b2d";
                                myZakatReturnsListTemp[i].StatusImage = "ic_loading.png";
                            }
                            else if (string.Equals(myZakatReturnsListTemp[i].Stat, "IP014"))//Build || string.Equals(_status, "IP019") || string.Equals(_status, "IP021") || string.Equals(_status, "E0058") || string.Equals(_status, "E0076") || string.Equals(_status, "E0077") || string.Equals(_status, "For Officer's Review") || string.Equals(_status, "E0089")
                            {
                                myZakatReturnsListTemp[i].BorderColour = "#003672";
                                myZakatReturnsListTemp[i].StatusImage = "ic_Paid.png";
                            }
                            else if (string.Equals(myZakatReturnsListTemp[i].Stat, "E0008"))//Build || string.Equals(_status, "IP019") || string.Equals(_status, "IP021") || string.Equals(_status, "E0058") || string.Equals(_status, "E0076") || string.Equals(_status, "E0077") || string.Equals(_status, "For Officer's Review") || string.Equals(_status, "E0089")
                            {
                                myZakatReturnsListTemp[i].BorderColour = "#003672";
                                myZakatReturnsListTemp[i].StatusImage = "ic_Paid.png";
                            }
                            else if (string.Equals(myZakatReturnsListTemp[i].Stat, "IP021"))//To be approved || string.Equals(_status, "IP019") || string.Equals(_status, "IP021") || string.Equals(_status, "E0058") || string.Equals(_status, "E0076") || string.Equals(_status, "E0077") || string.Equals(_status, "For Officer's Review") || string.Equals(_status, "E0089")
                            {
                                myZakatReturnsListTemp[i].BorderColour = "#c49b2d";
                                myZakatReturnsListTemp[i].StatusImage = "ic_loading.png";
                            }
                        }
                        else
                        {
                            if (string.Equals(myZakatReturnsListTemp[i].Statfg, "U"))//UnSubmitted_status, "IP011") || string.Equals(_status, "IP014") || 
                            {
                                myZakatReturnsListTemp[i].StatusImage = "ic_unsubmitted.png";
                                myZakatReturnsListTemp[i].BorderColour = "#944E22";
                            }
                            else if (string.Equals(myZakatReturnsListTemp[i].Statfg, "P"))//Paid|| string.Equals(_status, "I") || string.Equals(_status, "IP015")
                            {
                                myZakatReturnsListTemp[i].BorderColour = "#003672";
                                myZakatReturnsListTemp[i].StatusImage = "ic_Paid.png";
                            }
                            else if (string.Equals(myZakatReturnsListTemp[i].Statfg, "I"))//Paid|| string.Equals(_status, "I") || string.Equals(_status, "IP015")
                            {
                                myZakatReturnsListTemp[i].BorderColour = "#003672";
                                myZakatReturnsListTemp[i].StatusImage = "ic_Paid.png";
                            }
                            else if (string.Equals(myZakatReturnsListTemp[i].Statfg, "IP015"))//In processing || string.Equals(_status, "IP019") || string.Equals(_status, "IP021") || string.Equals(_status, "E0058") || string.Equals(_status, "E0076") || string.Equals(_status, "E0077") || string.Equals(_status, "For Officer's Review") || string.Equals(_status, "E0089")
                            {
                                myZakatReturnsListTemp[i].BorderColour = "#c49b2d";
                                myZakatReturnsListTemp[i].StatusImage = "ic_loading.png";
                            }
                            else if (string.Equals(myZakatReturnsListTemp[i].Statfg, "IP014"))//Build || string.Equals(_status, "IP019") || string.Equals(_status, "IP021") || string.Equals(_status, "E0058") || string.Equals(_status, "E0076") || string.Equals(_status, "E0077") || string.Equals(_status, "For Officer's Review") || string.Equals(_status, "E0089")
                            {
                                myZakatReturnsListTemp[i].BorderColour = "#003672";
                                myZakatReturnsListTemp[i].StatusImage = "ic_Paid.png";
                            }
                        }
                        if (myZakatReturnsListTemp[i].Fbtyp.Equals("FZ12") || myZakatReturnsListTemp[i].Fbtyp.Equals("ZKTE") || myZakatReturnsListTemp[i].Incotyp.Equals("H-05-A") || myZakatReturnsListTemp[i].Incotyp.Equals("H-05-A-I") || myZakatReturnsListTemp[i].Incotyp.Equals("G-05-A") || myZakatReturnsListTemp[i].Incotyp.Equals("G-05-A-I"))
                        {
                            myZakatReturnsList.Add(myZakatReturnsListTemp[i]);
                        }
                        //  myZakatReturnsList.Add(myZakatReturnsListTemp[i]);
                    }
                    MyZakatReturns = myZakatReturnsList;
                    if (ZakatReturnListPageView.AreYouUsingFilterFirstTimeAfterComingFromZAKATDetailsPage)
                    {
                        if (SelectedIndex != 13)
                        {
                            SelectedIndex = SelectedICRStatusWhileGoingToZAKATDetails;
                            TxtSelectedStatus = ICRStatusList[SelectedIndex].Value;
                            SelectedICRStatus = ICRStatusList[SelectedIndex];
                            ZakatReturnListPageView.AreYouUsingFilterFirstTimeAfterComingFromZAKATDetailsPage = false;
                            GetFilteredZAKATICRList(SelectedICRStatus);
                        }
                        else
                        {
                            //SelectedIndex = SelectedICRStatusWhileGoingToZAKATDetails;
                            TxtSelectedStatus = ICRStatusList[13].Value;
                            ZakatReturnListPageView.AreYouUsingFilterFirstTimeAfterComingFromZAKATDetailsPage = false;
                            SelectedIndex = 13;
                            SelectedICRStatus = ICRStatusList[SelectedIndex];
                            GetFilteredZAKATICRList(SelectedICRStatus);
                        }
                    }
                    else
                    {
                        SelectedIndex = 13;
                    }
                }
                else
                {
                    HideNoDataMessage();
                }
            }
            catch (Exception)
            {
            }
        }
        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
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
                    //var _navigation = Application.Current.MainPage.Navigation;
                    //_navigation.PopToRootAsync();
                });
            }
        }
        public void HandleNoDataMessageVisibility(List<EstimatedZakatReturnsResult> filteredCRStatusList)
        {
            if (filteredCRStatusList != null && filteredCRStatusList.Count > 0)
            {
                HideNoDataMessage();
            }
            else
            {
                ShowNoDataMessage();
            }
        }
        private void ShowNoDataMessage()
        {
            SetNoDataLabelVisibility = true;
        }
        private void HideNoDataMessage()
        {
            SetNoDataLabelVisibility = false;
        }
        public void ClearData()
        {
            MyZakatReturns = new List<EstimatedZakatReturnsResult>();
            myZakatReturnsList = new List<EstimatedZakatReturnsResult>();
        }
        public IEnumerable<EstimatedZakatReturnsResult> GetSortedList(IList<EstimatedZakatReturnsResult> ICRList)
        {
            try
            {
                var SortedList = ICRList.OrderBy(x => Convert.ToDateTime(x.DueDtC).TimeOfDay)
                              .ThenBy(x => Convert.ToDateTime(x.DueDtC).Date)
                              .ThenBy(x => Convert.ToDateTime(x.DueDtC).Year);
                return SortedList.OrderByDescending(x => Convert.ToDateTime(x.DueDtC)).ToList();
            }
            catch (Exception)
            {
                return ICRList;
            }
        }
        public void SetCurrentIndex()
        {
        }
        #endregion
    }
}
