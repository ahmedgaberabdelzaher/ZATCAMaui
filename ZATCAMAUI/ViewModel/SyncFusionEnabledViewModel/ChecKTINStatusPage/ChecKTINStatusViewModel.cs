

using System.Windows.Input;
using System.Globalization;
using Newtonsoft.Json;
using ZATCAMAUI.Models;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.ViewModel.NewDesignViewModel;
using ZATCAMAUI.Core.Interfaces;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ChecKTINStatusPage
{

    public class ChecKTINStatusViewModel : BaseViewModel
    {
        public ICommand BackButtonClicked { get; set; }
        public ICommand OnCloseClick { get; set; }
        public ICommand OnClickLessOrMore { get; set; }
        public DateTime lastTapped;
        private bool _isios = false;
        public bool Isios
        {
            get
            {
                return _isios;
            }
            set
            {
                _isios = value;
                OnPropertyChanged("Isios");
            }
        }
        private bool _isAndroid = false;
        public bool IsAndroid
        {
            get
            {
                return _isAndroid;
            }
            set
            {
                _isAndroid = value;
                OnPropertyChanged("IsAndroid");
            }
        }
        private string _TIN = string.Empty;
        public string TIN
        {
            get
            {
                return _TIN;
            }
            set
            {
                _TIN = value;
                OnPropertyChanged("TIN");
            }
        }
        private string _TINStatus = string.Empty;
        public string TINStatus
        {
            get
            {
                return _TINStatus;
            }
            set
            {
                _TINStatus = value;
                OnPropertyChanged("TINStatus");
            }
        }
        private string _LastUpdate = string.Empty;
        public string LastUpdate
        {
            get
            {
                return _LastUpdate;
            }
            set
            {
                _LastUpdate = value;
                OnPropertyChanged("LastUpdate");
            }
        }
        private bool _isLabelVisible = false;
        public bool IsLabelVisible
        {
            get
            {
                return _isLabelVisible;
            }
            set
            {
                _isLabelVisible = value;
                OnPropertyChanged("IsLabelVisible");
            }
        }
        private bool _isVisibleListItems = false;
        public bool IsVisibleListItems
        {
            get
            {
                return _isVisibleListItems;
            }
            set
            {
                _isVisibleListItems = value;
                OnPropertyChanged("IsVisibleListItems");
            }
        }
        //@Divya Jannapureddy adding line number 105 to 118
        private bool _isShowLessMoreLblVisible = true;
        public bool IsShowLessMoreLblVisible
        {
            get
            {
                return _isShowLessMoreLblVisible;
            }
            set
            {
                _isShowLessMoreLblVisible = value;
                OnPropertyChanged("IsShowLessMoreLblVisible");
            }
        }
        private TINStatus _listTINStatus;
        public TINStatus ListTINStatus
        {
            get
            {
                return _listTINStatus;
            }
            set
            {
                _listTINStatus = value;
                OnPropertyChanged("ListTINStatus");
            }
        }
        private List<ConsumerRegisteration> _consumerRegisteration;
        public List<ConsumerRegisteration> ConsumerRegisteration
        {
            get
            {
                return _consumerRegisteration;
            }
            set
            {
                _consumerRegisteration = value;
                OnPropertyChanged("ConsumerRegisteration");
            }
        }
        private string _showLessOrMore = AppResources.ZShowmoredetails;
        public string ShowLessOrMore
        {
            get
            {
                return _showLessOrMore;
            }
            set
            {
                _showLessOrMore = value;
                OnPropertyChanged("ShowLessOrMore");
            }
        }
        //@Divya Jannapureddy adding line number
        //Replace the below method
        public ChecKTINStatusViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            OnClickLessOrMore = new Command( () =>
            {
                ShowLessOrMore = string.Empty;
                if (IsVisibleListItems == false)
                {
                    IsVisibleListItems = true;
                    ShowLessOrMore = AppResources.ZShowlessdetails;
                }
                else
                {
                    IsVisibleListItems = false;
                    ShowLessOrMore = AppResources.ZShowmoredetails;
                }
            });
            BackButtonClicked = new Command(() =>
            {
                // _navigationService.NavigateTo(App.SFLandingPageView);
                _navigationService.GoBack();
            });
        }
        //@Divya Jannapureddy adding line number
        //Replace below method
        public async Task OnPageLoad()
        {
            try
            {
                ShowLessOrMore = AppResources.ZShowmoredetails;
                IsVisibleListItems = false;
                string Lang = UtilityManager.GetLanguageParameter();
                if (lastTapped < DateTime.Now.AddSeconds(-4))
                {
                    if (NetworkCheck.IsInternet())
                    {
                        ListTINStatus = await WebServiceManager.GAZTGetTinStatus(Lang, App.TP.Tin);
                        PopToRootPage();
                        TIN = ListTINStatus.d.Tin;
                        TINStatus = ListTINStatus.d.StatusText;
                        if (ListTINStatus.d.Udate != null)
                        {
                            if (App.IsArabic)
                            {
                                string dateLU = UtilityManager.FormatAccordingToDevice(ListTINStatus.d.Udate.ToString().Split(' ')[0]);
                                LastUpdate = dateLU;
                                //LastUpdate = UtilityManager.ToArabicDate(LastUpdate);
                            }
                            else
                            {
                                string dateLU = UtilityManager.FormatAccordingToDevice(ListTINStatus.d.Udate.ToString().Split(' ')[0]);
                                LastUpdate = dateLU;
                            }
                        }
                        ConsumerRegisteration = ListTINStatus.d.ItemSet.results;
                        if (ConsumerRegisteration.Count > 0)
                        {
                            IsShowLessMoreLblVisible = true;
                            IsLabelVisible = false;
                            if (ConsumerRegisteration != null)
                            {
                                if (ListTINStatus.d.ItemSet.results != null)
                                {
                                    foreach (ConsumerRegisteration itemCR in ListTINStatus.d.ItemSet.results)
                                    {
                                        if (itemCR.Udate != null)
                                        {
                                            if (App.IsArabic)
                                            {
                                                itemCR.Udate = JsonConvert.DeserializeObject<DateTime>(@"""" + itemCR.Udate + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                                //itemCR.Udate = UtilityManager.ToArabicDate(itemCR.Udate);
                                            }
                                            else
                                            {
                                                itemCR.Udate = JsonConvert.DeserializeObject<DateTime>(@"""" + itemCR.Udate + @"""").ToString("dd-MMMM-yyyy", new CultureInfo("en-US"));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            IsLabelVisible = true;
                            IsShowLessMoreLblVisible = false;
                        }
                    }
                    else
                    {
                    }
                }
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
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
                });
            }
        }
    }
}
