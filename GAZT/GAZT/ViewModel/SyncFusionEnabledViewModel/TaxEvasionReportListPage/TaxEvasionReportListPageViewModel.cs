using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
namespace GAZT.ViewModel.NewViewModel
{
    public class TaxEvasionReportListPageViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnCloseClicked_Tapped { get; set; }
        public ICommand BackButtonClicked { get; set; }
        public ICommand GoBackClick { get; set; }
        public ICommand AddButtonClicked { get; set; }
        public ICommand OnOpenClicked_Tapped { get; set; }
        private bool _setNoDataLabelVisibility = false;//SelectedTaxEvasionListItem
        private TaxEvasionReport _selectedTaxEvasionListItem;
        public TaxEvasionReport SelectedTaxEvasionListItem
        {
            get
            {
                return _selectedTaxEvasionListItem;
            }
            set
            {
                try
                {
                    _selectedTaxEvasionListItem = value;
                    if (_selectedTaxEvasionListItem != null)
                    {
                        passSelectedTaxEvasionItem();
                    }
                    RaisePropertyChanged("SelectedTaxEvasionListItem");
                }
                catch (Exception ex)
                {
                }
            }
        }
        private string _mobileNumber = string.Empty;
        public string MobileNumber
        {
            get
            {
                return _mobileNumber;
            }
            set
            {
                _mobileNumber = value;
                RaisePropertyChanged("MobileNumber");
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
        private string _addIcon = string.Empty;
        public string AddIcon
        {
            get
            {
                return _addIcon;
            }
            set
            {
                _addIcon = value;
                RaisePropertyChanged("AddIcon");
            }
        }
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
        private List<TaxEvasionReport> _taxEvasionReportList;
        public List<TaxEvasionReport> TERListReportbymobno
        {
            get
            {
                return _taxEvasionReportList;
            }
            set
            {
                _taxEvasionReportList = value;
                RaisePropertyChanged("TERListReportbymobno");
            }
        }
        private List<TaxEvasionReport> _taxEvasionReportListClosed;
        public List<TaxEvasionReport> TERListReportbymobnoClosed
        {
            get
            {
                return _taxEvasionReportListClosed;
            }
            set
            {
                _taxEvasionReportListClosed = value;
                RaisePropertyChanged("TERListReportbymobnoClosed");
            }
        }
        private List<TaxEvasionReport> _tERListReportbymobnoDummy;
        public List<TaxEvasionReport> TERListReportbymobnoDummy
        {
            get
            {
                return _tERListReportbymobnoDummy;
            }
            set
            {
                _tERListReportbymobnoDummy = value;
                RaisePropertyChanged("TERListReportbymobnoDummy");
            }
        }
        public TaxEvasionReportListPageViewModel(INavigationService navigationService, IDialogService dialogService)
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
            BackButtonClicked = new Xamarin.Forms.Command(() =>
            {
                _navigationService.NavigateTo(App.SFLandingPageView);
            });
            AddButtonClicked = new Xamarin.Forms.Command(() =>
            {
                _navigationService.NavigateTo(App.TaxEvasionReportTypePageView, MobileNumber);
            });
            GoBackClick = new Command(async () =>
            {
                if (App.TP != null && !string.IsNullOrEmpty(App.TP.Mobile))
                {
                    _navigationService.GoBack();
                }
                else
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    var _lastPage = _navigation.NavigationStack.LastOrDefault();
                    //Remove last page
                    _navigation.RemovePage(_lastPage);
                    //Go back 
                    _navigation.PopAsync();
                }
            });
            OnCloseClicked_Tapped = new RelayCommand(async () =>
            {
                try
                {
                    TERListReportbymobno = TERListReportbymobnoDummy.Where(x => (x.ReportStatus == "3")).ToList();
                    TERListReportbymobnoClosed.Clear();
                    TERListReportbymobnoClosed = TERListReportbymobnoDummy.Where(x => (x.ReportStatus == "3")).ToList();
                    if (TERListReportbymobnoClosed == null)
                    {
                        SetNoDataLabelVisibility = true;
                    }
                }
                catch (Exception ex)
                {
                }
            });
            OnOpenClicked_Tapped = new RelayCommand(async () =>
            {
                try
                {
                    TERListReportbymobno = TERListReportbymobnoDummy.Where(x => (x.ReportStatus == "0") || (x.ReportStatus == "1") || (x.ReportStatus == "2")).ToList();//SetNoDataLabelVisibility
                    if (TERListReportbymobno != null)
                    {
                        SetNoDataLabelVisibility = false;
                    }
                    else
                    {
                        SetNoDataLabelVisibility = true;
                    }
                }
                catch (Exception ex)
                {
                }
            });
        }
        public void passSelectedTaxEvasionItem()
        {
            //Task.Run(() =>
            //{
            //    IsLoading = true;
            //});
            //await Task.Run(() =>
            //{
            //    try
            //    {
                _navigationService.NavigateTo(App.TaxEvasionReportFormPageView, SelectedTaxEvasionListItem);
                //Device.BeginInvokeOnMainThread(async () =>
                //{
                //    _navigationService.NavigateTo(App.TaxEvasionReportFormPageView, SelectedTaxEvasionListItem);
                //});
            //}
            //catch (Exception ex)
            //    {
            //    }
            //});
            //Task.Run(() =>
            //    {
            //        IsLoading = false;
            //    });
            }
        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    await _navigation.PopToRootAsync();
                });
            }
        }
        public void OnPageLoad()
        {
            try
            {
                //string test = App.TP.Mobile;
                ReportRetriveByMobNoRootObject rootObject = new ReportRetriveByMobNoRootObject();
                rootObject = WebServiceManager.GAZTTESReportByMobNo(MobileNumber);
                PopToRootPage();
                if (rootObject != null)
                {
                    if (rootObject.TaxEvasionReportList != null && rootObject.TaxEvasionReportList.Count > 0)
                    {
                        SetNoDataLabelVisibility = false;
                        TERListReportbymobnoDummy = rootObject.TaxEvasionReportList;
                        TERListReportbymobno = TERListReportbymobnoDummy.Where(x => (x.ReportStatus == "0") || (x.ReportStatus == "1") || (x.ReportStatus == "2")).ToList();
                        if (TERListReportbymobno != null)
                        { SetNoDataLabelVisibility = false; }
                        TERListReportbymobnoClosed = TERListReportbymobnoDummy.Where(x => (x.ReportStatus == "3")).ToList();
                    }
                    else
                    {
                        SetNoDataLabelViewVisibility();
                    }
                }
                else
                {
                    SetNoDataLabelVisibility = true;
                }
            }
            catch (Exception ex)
            {
                SetNoDataLabelVisibility = true;
                _dialogService.ShowMessageBox(AppResources.ZZInternetConnectionMessage, AppResources.Alerts);
            }
        }
        private void SetNoDataLabelViewVisibility()
        {
            SetNoDataLabelVisibility = true;
        }
    }
}
