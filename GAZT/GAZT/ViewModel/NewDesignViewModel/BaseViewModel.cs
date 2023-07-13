using EGAZT.Models;
using EGAZT.Models.SubmitReportModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
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
        bool isValidationError;

        public bool IsValidationError
        {
            get { return isValidationError; }

            set
            {
                isValidationError = value;
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
        public virtual ICommand CloseMsgViewCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsShowMsgView = false;
                    IsValidationError = false;
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


        ObservableCollection<MenuModel> afterLoginMenuLst;
        public ObservableCollection<MenuModel> AfterLoginMenuLst { get { return afterLoginMenuLst; } set { afterLoginMenuLst = value; RaisePropertyChanged(); } }


        public void GetDashBoardMenuLst(int CurrentTab=0)
        {
            AfterLoginMenuLst = new ObservableCollection<MenuModel>()
           {

                 new MenuModel()
                {
                   Name=AppResources.Home, ID="Home",ImageSource="HomeNotSelected",ColumnNo=0,IsSelected=CurrentTab==3?true:false,SelectedImageSource="HomeSelected"
                },
                 new MenuModel()
                {
                   Name=AppResources.MenuDashboard, ID="GAZTNewDesignDashBoardPageView",ImageSource="NotSelectedDashBoardicon",ColumnNo=1,IsSelected=CurrentTab==1?true:false,SelectedImageSource="SelectedDashBoardicon"
                },
                     new MenuModel()
                {
                   Name=AppResources.AccountS, ID="AccountStatementBillsPageView",ImageSource="NotSelectedAccountStatlement",ColumnNo=2,IsSelected=CurrentTab==2?true:false,SelectedImageSource="SelectedAccountStatlement"
                },
                 new MenuModel()
                {
                   Name=AppResources.ZZZMenu, ID="menu",ImageSource="Menu",ColumnNo=3,IsSelected=CurrentTab==4?true:false,SelectedImageSource="MenuSelected"
                },
           };
        }


        public virtual ICommand MenuNavigationCommand
        {
            get
            {
                return new Command<MenuModel>((Selecteditem) =>
                {
                   /* foreach (var item in AfterLoginMenuLst)
                    {
                        item.IsSelected = false;
                    }
                    Selecteditem.IsSelected = true;*/
                    if (Selecteditem.ID=="Home")
                    {

                        _navigationService.NavigateTo($"/{Selecteditem.ID}", "3");
                        return;
                    }
                  else if(Selecteditem.ID == "menu")
                    {
                        _navigationService.NavigateTo($"/GAZTNewDesignDashBoardPageView",true);
                        return;
                    }
                    else if (Selecteditem.ID== "GAZTNewDesignDashBoardPageView")
                    {
                        _navigationService.NavigateTo($"/GAZTNewDesignDashBoardPageView", "1");
                        return;
                    }
                    _navigationService.NavigateTo($"/{Selecteditem.ID}");

                });
            }
        }

        #region File Upload
        

        public async Task<ObservableCollection<ReportFileModel>> PickAndShow(PickOptions options, ObservableCollection<ReportFileModel> uploadedFiles, string maximumFileSizeMsg, string numberOfAttachmentMsg, int maxCount=1, int maxFileSize = 2)
        {
            try
            {
                var result = await FilePicker.PickAsync(options);
                if (result != null)
                {
                    var Text = $"File Name: {result.FileName}";
                    if (result.FileName.EndsWith("pdf", StringComparison.OrdinalIgnoreCase))
                    {

                        var lenght = new FileInfo(result.FullPath).Length;
                        double LenghtInKb = lenght / 1024;
                        double LenInMb = LenghtInKb / 1024;
                        double size = LenInMb;
                        double filesize = size;
                        if (filesize > maxFileSize )
                        {
                            MessageTxt = maximumFileSizeMsg;
                            IsShowMsgView = true;
                            return uploadedFiles;
                        }

                        else if (uploadedFiles != null && uploadedFiles.Count < maxCount)
                        {
                            var stream = await result.OpenReadAsync();
                            string content = await ConvertToBase64(stream);
                            ReportFileModel reportfile = new ReportFileModel();
                            reportfile.fileBase64 = content;
                            reportfile.fileFullName = result.FileName;
                            reportfile.fileSize = filesize;
                            reportfile.fileExtinction = Path.GetExtension(result.FileName);
                            uploadedFiles.Add(reportfile);
                            return uploadedFiles;
                        }

                        else
                        {
                            IsShowMsgView = true;
                            MessageTxt = numberOfAttachmentMsg;
                            return uploadedFiles;
                        }
                    }
                    else
                    {
                        IsShowMsgView = true;
                        MessageTxt = maximumFileSizeMsg;
                        return uploadedFiles;
                    }

                }
                return uploadedFiles;
            }
            catch (Exception ex)
            {
                IsShowMsgView = true;
                MessageTxt = AppResources.Somethingwentwrong;
                return new ObservableCollection<ReportFileModel>();
            }


        }
        private async Task<string> ConvertToBase64(Stream stream)
        {
            if (stream is MemoryStream memoryStream)
            {
                return Convert.ToBase64String(memoryStream.ToArray());
            }

            var bytes = new Byte[(int)stream.Length];

            stream.Seek(0, SeekOrigin.Begin);
            await stream.ReadAsync(bytes, 0, (int)stream.Length);

            return Convert.ToBase64String(bytes);
        }
       
        public virtual ICommand SelectedUploadLabelCommand
        {
            get
            {
                return new Xamarin.Forms.Command<string>(async (selectedLabel) =>
                {
                    await PopupNavigation.Instance.PopAsync(true);

                });
            }
        }
        #endregion
        public object GetTokenData(string token = "")
        {
            try
            {
                string secretKey = "ByYM000OLlMQG6VVVp1OH7Xzyr7gHuw1qvUC5dcGt3SNM";
                var payload = JWT.JsonWebToken.DecodeToObject(token, secretKey);
                return payload;
            }
            catch (Exception ex)
            {
                return null;
            }


        }

    }
}
