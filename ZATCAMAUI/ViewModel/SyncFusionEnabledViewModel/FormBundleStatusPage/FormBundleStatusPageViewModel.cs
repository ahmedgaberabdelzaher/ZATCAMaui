

using System.Windows.Input;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.FormBundleStatusPage
{

    public class FormBundleStatusPageViewModel : BaseViewModel
    {
        private List<FormBundleResult> _formBundleList;
        private bool _isCPickerEnable = false;
        private FormBundleApplicationNumberModelResult _selectedFormBindleFbnum = null;
        private FormBundleApplicationNumberModelResult _selectedFormBindleFbnumPrev = null;
        private List<FormBundleApplicationNumberModelResult> _formBundleApplicationNumberList;
        private string _fbnumdetail;
        private FormBundleResult _selectedFormBindleFbtyp = null;
        private FormBundleResult _selectedFormBindleFbtypCancel = null;
        private List<FbnumDetailList> _fbnumDetailList;
        public ICommand BackButtonClicked { get; set; }
        private string _txtFBtype = string.Empty;
        public string TxtFBtype
        {
            get
            {
                return _txtFBtype;
            }
            set
            {
                _txtFBtype = value;
                OnPropertyChanged("TxtFBtype");
            }
        }
        private string _txtFBnum = string.Empty;
        public string TxtFBnum
        {
            get
            {
                return _txtFBnum;
            }
            set
            {
                _txtFBnum = value;
                OnPropertyChanged("TxtFBnum");
            }
        }
        public FormBundleStatusPageViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            BackButtonClicked = new Command(() =>
            {
                _navigationService.GoBack();
            });
        }
        public List<FormBundleResult> FormBundleList
        {
            get
            {
                return _formBundleList;
            }
            set
            {
                _formBundleList = value;
                OnPropertyChanged("FormBundleList");
            }
        }
        public bool IsCPickerEnable
        {
            get
            {
                return _isCPickerEnable;
            }
            set
            {
                _isCPickerEnable = value;
                OnPropertyChanged("IsCPickerEnable");
            }
        }
        public FormBundleApplicationNumberModelResult SelectedFormBindleFbnum
        {
            get
            {
                return _selectedFormBindleFbnum;
            }
            set
            {
                _selectedFormBindleFbnum = value;
                if (_selectedFormBindleFbnum != null)
                {
                    populate();
                    TxtFBnum = _selectedFormBindleFbnum.Fbnum;
                }
                OnPropertyChanged("SelectedFormBindleFbnum");
            }
        }
        public FormBundleApplicationNumberModelResult SelectedFormBindleFbnumPrev
        {
            get
            {
                return _selectedFormBindleFbnumPrev;
            }
            set
            {
                _selectedFormBindleFbnumPrev = value;
                OnPropertyChanged("SelectedFormBindleFbnumPrev");
            }
        }
        public string Fbnumdetail
        {
            get
            {
                return _fbnumdetail;
            }
            set
            {
                _fbnumdetail = value;
                OnPropertyChanged("Fbnumdetail");
            }
        }
        public List<FormBundleApplicationNumberModelResult> FormBundleApplicatioNumberList
        {
            get
            {
                return _formBundleApplicationNumberList;
            }
            set
            {
                _formBundleApplicationNumberList = value;
                OnPropertyChanged("FormBundleApplicatioNumberList");
            }
        }
        public FormBundleResult SelectedFormBindleFbtyp
        {
            get
            {
                return _selectedFormBindleFbtyp;
            }
            set
            {
                _selectedFormBindleFbtyp = value;
                if (_selectedFormBindleFbtyp != null)
                {
                    try
                    {
                        _formBundleApplicationNumberList = null;
                        TxtFBnum = string.Empty;
                        TxtFBtype = _selectedFormBindleFbtyp.Txt50;
                        IsCPickerEnable = true;
                        onSelectedFormBindleFbtyp();
                    }
                    catch (Exception)
                    {


                    }
                }
                ListFormBudles = null;
                OnPropertyChanged("SelectedFormBindleFbtyp");
            }
        }
        public FormBundleResult SelectedFormBindleFbtypCancel
        {
            get
            {
                return _selectedFormBindleFbtypCancel;
            }
            set
            {
                _selectedFormBindleFbtypCancel = value;
                OnPropertyChanged("SelectedFormBindleFbtypCancel");
            }
        }
        public List<FbnumDetailList> FbnumDetailList
        {
            get
            {
                return _fbnumDetailList;
            }
            set
            {
                _fbnumDetailList = value;
                OnPropertyChanged("FbnumDetailList");
            }
        }
        private List<FbnumDetailList> _listFormBudles = null;
        public List<FbnumDetailList> ListFormBudles
        {
            get
            {
                return _listFormBudles;
            }
            set
            {
                _listFormBudles = value;
                OnPropertyChanged("ListFormBudles");
            }
        }
       
        public async Task onPageLoad()
        {
            try
            {
                SelectedFormBindleFbnumPrev = null;
                SelectedFormBindleFbtypCancel = null;

                FormBundleModel formbundleList = new FormBundleModel();
                string lang = UtilityManager.GetLanguageParameter();
                formbundleList = await WebServiceManager.GAZTGetFormBundleModel();
                PopToRootPage();
                if (formbundleList != null && formbundleList.d != null)
                {
                    List<FormBundleResult> FormBundleResultList = GetUpdatedFormBundleTypeList(formbundleList.d.results);
                    FormBundleList = FormBundleResultList;
                }
                else
                {
                    // provide data not available message
                }
                //      FormBundleList = formbundleList.d.results;
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
            }
        }
        public async void onSelectedFormBindleFbtyp()
        {
            try
            {
                await Task.Run(() =>
                 {
                     IsLoading = true;
                 });
                await Task.Run(async () =>
                {
                    FormBundleApplicationNumberModel formbundleApplicationNumberList = new FormBundleApplicationNumberModel();
                    formbundleApplicationNumberList = await WebServiceManager.GAZTGetFormBundleApplicationNumberModel(SelectedFormBindleFbtyp.Fbtyp);
                    PopToRootPage();
                    if (formbundleApplicationNumberList != null)
                        FormBundleApplicatioNumberList = formbundleApplicationNumberList.d.results.OrderBy(x => x.Fbnum).ToList();
                });
                await Task.Run(() =>
                  {
                      IsLoading = false;
                  });
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
                    IsLoading = false;

                    await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                    //viewModel._navigationService.GoBack();
                });
            }

            catch (HttpRequestException ex)
            {
                string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    // IsLoading = false;

                    await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                    await Task.Run(() =>
                     {
                         IsLoading = false;
                     });
                });
            }

            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
                await Task.Run(() =>
                 {
                     IsLoading = false;
                 });
            }
            catch (Exception)
            {


                string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    // IsLoading = false;

                    await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                    await Task.Run(() =>
                      {
                          IsLoading = false;
                      });
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
                    //var _navigation = Application.Current.MainPage.Navigation;
                    //_navigation.PopToRootAsync();
                });
            }
        }
        private List<FormBundleResult> GetUpdatedFormBundleTypeList(List<FormBundleResult> FormBundleTypeList)
        {
            try
            {
                List<FormBundleResult> list = new List<FormBundleResult>();
                if (App.IsArabic)
                {
                    foreach (FormBundleResult Object in FormBundleTypeList)
                    {

                        Object.Txt50 = Object.Txt50;
                        list.Add(Object);
                    }
                }
                else
                {
                    return FormBundleTypeList;
                }
                return list;
            }
            catch (Exception)
            {


                return null;
            }
        }
        public void populate()
        {
            TxtFBnum = _selectedFormBindleFbnum.Fbnum;
            Fbnumdetail = _selectedFormBindleFbnum.Fbsta;
            List<FormBundleApplicationNumberModelResult> Formbundle = FormBundleApplicatioNumberList.Where(a => a.Fbnum == _selectedFormBindleFbnum.Fbnum).ToList();
            List<FbnumDetailList> Child = new List<FbnumDetailList>();
            foreach (FormBundleApplicationNumberModelResult itemF in Formbundle)
            {
                FbnumDetailList Item = new FbnumDetailList();
                Item.Fbnum = itemF.Fbnum;
                Item.Fbsta = itemF.Fbsta;
                Item.FbDesc = itemF.Txt50;
                Item.FbStatus = itemF.Fbstatus;
                Child.Add(Item);
            }
            ListFormBudles = Child;
        }

        public void ClearData()
        {
            FormBundleList = null;
            FormBundleApplicatioNumberList = null;
            SelectedFormBindleFbtyp = null;
            SelectedFormBindleFbnum = null;
            ListFormBudles = null;
        }
    }
}
