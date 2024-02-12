using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System.Windows.Input;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.TaxEvasionReportListPage
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
        private bool _setNoDataLabelVisibilityforOpen = false;//SelectedTaxEvasionListItem
        private TaxEvasionReportDetails _selectedTaxEvasionListItem;
        public TaxEvasionReportDetails SelectedTaxEvasionListItem
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
                        passSelectedTaxEvasionItem(_selectedTaxEvasionListItem);
                    }
                    RaisePropertyChanged("SelectedTaxEvasionListItem");
                }
                catch (Exception)
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
        public bool SetNoDataLabelVisibilityforOpen
        {
            get
            {
                return _setNoDataLabelVisibilityforOpen;
            }
            set
            {
                _setNoDataLabelVisibilityforOpen = value;
                RaisePropertyChanged("SetNoDataLabelVisibilityforOpen");
            }
        }
        private bool _setNoDataLabelVisibilityforClose = false;
        public bool SetNoDataLabelVisibilityforClose
        {
            get
            {
                return _setNoDataLabelVisibilityforClose;
            }
            set
            {
                _setNoDataLabelVisibilityforClose = value;
                RaisePropertyChanged("SetNoDataLabelVisibilityforClose");
            }
        }
        private TaxEvasionReportDetails[] _taxEvasionReportList;
        public TaxEvasionReportDetails[] TERListReportbymobno
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
        private TaxEvasionReportDetails[] _taxEvasionReportListClosed;
        public TaxEvasionReportDetails[] TERListReportbymobnoClosed
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
            BackButtonClicked = new Command(() =>
            {
                _navigationService.NavigateTo(App.SFLandingPageView);
            });
            AddButtonClicked = new Command(() =>
            {
                _navigationService.NavigateTo(App.TaxEvasionReportTypePageView, MobileNumber);
            });
            GoBackClick = new Command(async () =>
            {
                _navigationService.GoBack();

            });
            OnCloseClicked_Tapped = new RelayCommand(async () =>
            {
                try
                {

                }
                catch (Exception)
                {


                }
            });
            OnOpenClicked_Tapped = new RelayCommand(async () =>
            {
                try
                {

                }
                catch (Exception)
                {


                }
            });
        }
        public async Task passSelectedTaxEvasionItem(TaxEvasionReportDetails SelectedTaxEvasionReport)
        {
            try
            {
                await Task.Run(() =>
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
               {
                   IsLoading = true;
               });

                });
                await Task.Run(() =>
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        _navigationService.NavigateTo(App.TaxEvasionFormPage, SelectedTaxEvasionReport);
                        //_navigationService.NavigateTo(App.TaxEvasionReportFormPageView, SelectedTaxEvasionReport);
                    });
                });
                await Task.Run(() =>
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        IsLoading = false;
                    });
                });
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
                    await _navigation.PopToRootAsync();
                });
            }
        }

        public async void NavigateToAddReport()
        {
            try
            {
                _navigationService.NavigateTo(App.TaxEvasionReportTypePageView, App.TaxEvasionUserData.Mobile);
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
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });

                    await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                    //viewModel._navigationService.GoBack();
                });
            }
            catch (Exception)
            {


                MainThread.BeginInvokeOnMainThread(async () =>
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

        public async Task OnPageLoad()
        {
            SetNoDataLabelVisibilityforOpen = false;
            SetNoDataLabelVisibilityforClose = false;

            try
            {
                //string test = App.TP.Mobile;
                TaxEvasionReportsModel rootObject = new TaxEvasionReportsModel();
                TaxEvasionSendSmsModel taxEvasionSendSmsModel = new TaxEvasionSendSmsModel();
                taxEvasionSendSmsModel.mobile = App.TaxEvasionUserData.Mobile;

                rootObject = await TaxEvasionWebServiceManager.GAZTTaxEvasionGetAllReportsByMobileNumber(taxEvasionSendSmsModel);

                PopToRootPage();

                if (rootObject != null)
                {
                    if (rootObject.Data != null)
                    {
                        if (rootObject.Data.Opened.Count() > 0)
                        {
                            rootObject.Data.Opened = rootObject.Data.Opened.OrderByDescending(c => c.TicketId).ToArray();
                            TERListReportbymobno = rootObject.Data.Opened;
                            SetNoDataLabelVisibilityforOpen = false;
                        }
                        else
                        {
                            SetNoDataLabelVisibilityforOpen = true;
                        }

                        if (rootObject.Data.Closed.Count() > 0)
                        {
                            rootObject.Data.Opened = rootObject.Data.Closed.OrderByDescending(c => c.TicketId).ToArray();
                            TERListReportbymobnoClosed = rootObject.Data.Closed;
                            SetNoDataLabelVisibilityforClose = false;
                        }
                        else
                        {
                            SetNoDataLabelVisibilityforClose = true;
                        }
                    }
                    else
                    {
                        SetNoDataLabelVisibilityforOpen = true;
                        SetNoDataLabelVisibilityforClose = true;
                    }
                }
                else
                {
                    SetNoDataLabelVisibilityforOpen = true;
                    SetNoDataLabelVisibilityforClose = true;
                }
            }
            catch (GAZTException gex)
            {
                SetNoDataLabelVisibilityforOpen = true;
                SetNoDataLabelVisibilityforClose = true;

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
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });
                    await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                    //viewModel._navigationService.GoBack();
                });
            }
            catch (Exception)
            {


                SetNoDataLabelVisibilityforOpen = true;
                SetNoDataLabelVisibilityforClose = true;

                MainThread.BeginInvokeOnMainThread(async () =>
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

    }
}
