using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel
{
    [Preserve(AllMembers = true)]
    public class BaseViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        private bool _isLoading = false;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                if (_isLoading == value) return;
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }

        private string _HijriDateToBeDisplayed ;
        public string HijriDateToBeDisplayed
        {
            get
            {
                return _HijriDateToBeDisplayed;
            }
            set
            {

                _HijriDateToBeDisplayed = value;
               
                RaisePropertyChanged();
            }
        }

        bool isArabicLang { get; set; }

        public bool IsArabicLang
        {
            get { return App.IsArabic; }

            set
            {
                isArabicLang = value;
                RaisePropertyChanged();
            }
        }

       FlowDirection appDirection { get; set; }

        public FlowDirection AppDirection
        {
            get
            {
                if (!App.IsArabic)
                {
                    appDirection = FlowDirection.LeftToRight;
                }
                else
                {
                    appDirection = FlowDirection.RightToLeft;
                }
                return appDirection;
            }

            set
            {
                appDirection = value;
                RaisePropertyChanged();
            }
        }

        public BaseViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            SetFlowDirection();
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
        }

        public void SetFlowDirection()
        {

            if (!App.IsArabic)
            {
                AppDirection = FlowDirection.LeftToRight;
            }
            else
            {
                AppDirection = FlowDirection.RightToLeft;
            }

        }

        int currentTab = 1;
        public int CurrentTab { get { return currentTab; } set { currentTab = value; RaisePropertyChanged(); } }

        public ICommand ChangeCurrentTabCommand
        {
            get
            {
                return new Command<string>((tab) =>
                {
                    if (tab != CurrentTab.ToString())
                    {
                        switch (tab)
                        {
                            case "0":
                                _navigationService.NavigateTo("/Home", tab);
                                
                                break;
                            case "1":
                                _navigationService.NavigateTo($"/{App.SFLoginPageView}", App.GAZTNewDesignDashBoardPageView);
                                break;
                            case "2":
                                _navigationService.NavigateTo("/SideMenuView");
                                break;
                            case "3":
                                _navigationService.NavigateTo($"/LiveVideoPage");
                                break;
                            default:
                                break;
                        }
                        /*if (tab=="1")
                        {
                           
                            return;
                        }
                        else if (tab =="3")
                        {
                            _navigationService.NavigateTo($"/LiveVideoPage");
                            return;
                        }*/
                        var di = AppDirection;
                        CurrentTab = int.Parse(tab);
                        Title = tab == "0" ? AppResources.Home : AppResources.ZZZMenu;


                    }

                });
            }
        }
        string title;
        public string Title { get { return title; } set { title = value; RaisePropertyChanged(); } }

        public void PopToRootPage()
        {
            App.IsSessionExpired = false;

            if (App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(() =>
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
                    _navigationService.NavigateTo(App.SFLoginPageView, App.GAZTNewDesignDashBoardPageView);
                    _navigation.NavigationStack.ToList().Clear();
                });
            }
        }

        protected FlowDirection GetFlowDirectionToApply()
        {
            if (App.IsArabic)
                return FlowDirection.LeftToRight;
            else
                return FlowDirection.RightToLeft;
        }

        public virtual ICommand BackCommand
        {
            get
            {
                return new Command(() =>
                {
                    _navigationService.GoBack();
                });
            }
        }

        private ObservableCollection<object> _todayDateinHijri;
        public ObservableCollection<object> TodayDateinHijri
        {
            get
            {
                return _todayDateinHijri;
            }
            set
            {
                if (_todayDateinHijri == value) return;

                _todayDateinHijri = value;
               
                RaisePropertyChanged("TodayDateinHijri");
            }
        }
        public void SetDefaultDate()
        {

            //TodayDateinHijri
            ObservableCollection<object> todaycollectionHijri = new ObservableCollection<object>();
            var calendar = new HijriCalendar();
            if (calendar.GetDayOfMonth(DateTime.Now.Date) < 10)
                todaycollectionHijri.Add("0" + calendar.GetDayOfMonth(DateTime.Now.Date).ToString());
            else
                todaycollectionHijri.Add(calendar.GetDayOfMonth(DateTime.Now.Date).ToString());
            if (calendar.GetMonth(DateTime.Now.Date) < 10)
                todaycollectionHijri.Add("0" + calendar.GetMonth(DateTime.Now.Date));
            else
                todaycollectionHijri.Add(calendar.GetMonth(DateTime.Now.Date).ToString());
            todaycollectionHijri.Add(calendar.GetYear(DateTime.Now.Date).ToString());
            TodayDateinHijri = todaycollectionHijri;
            //     DefaultMonthHijri = calendar.GetMonth(DateTime.Now.Date);


        }

       public void ResetDate()
        {
            SetDefaultDate();
            if (TodayDateinHijri != null && TodayDateinHijri.Count > 0)
            {
                string month = TodayDateinHijri[1].ToString();
                string day = TodayDateinHijri[0].ToString();
                string year = TodayDateinHijri[2].ToString();
                HijriDateToBeDisplayed = day + "/" + month + "/" + year;

            }
        }

        bool isShowMsgView { get; set; }

        public bool IsShowMsgView
        {
            get { return isShowMsgView; }

            set
            {
                isShowMsgView = value;
                RaisePropertyChanged();
            }
        }
        public ICommand CloseMsgViewCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsShowMsgView = false;
                });
            }
        }


        string messageTxt { get; set; }

        public string MessageTxt
        {
            get { return messageTxt; }

            set
            {
                messageTxt = value;
                RaisePropertyChanged();
            }
        }


    }
}
