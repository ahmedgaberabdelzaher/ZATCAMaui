using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.FormBundleStatusPage_ViewModel
{
    [Preserve(AllMembers = true)]
    public class FormBundleStatusPageViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
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
                RaisePropertyChanged("TxtFBtype");
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
                RaisePropertyChanged("TxtFBnum");
            }
        }
        public FormBundleStatusPageViewModel(INavigationService navigationService, IDialogService dialogService)
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
                RaisePropertyChanged("FormBundleList");
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
                RaisePropertyChanged("IsCPickerEnable");
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
                RaisePropertyChanged("SelectedFormBindleFbnum");
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
                RaisePropertyChanged("SelectedFormBindleFbnumPrev");
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
                RaisePropertyChanged("Fbnumdetail");
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
                RaisePropertyChanged("FormBundleApplicatioNumberList");
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
                    catch (Exception ex)
                    {
                    }
                }
                ListFormBudles = null;
                RaisePropertyChanged("SelectedFormBindleFbtyp");
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
                RaisePropertyChanged("SelectedFormBindleFbtypCancel");
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
                RaisePropertyChanged("FbnumDetailList");
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
                RaisePropertyChanged("ListFormBudles");
            }
        }
        private bool _isLoading;
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
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
            }
        }
        public async void onSelectedFormBindleFbtyp()
        {
            try
            {
                Task.Run(() =>
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
                Task.Run(() =>
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

                Device.BeginInvokeOnMainThread(async () =>
                {
                    IsLoading = false;

                    await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                    //viewModel._navigationService.GoBack();
                });
            }

            catch (HttpRequestException ex)
            {
                string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    // IsLoading = false;

                    await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                    Task.Run(() =>
                    {
                        IsLoading = false;
                    });
                });
            }

            catch (InternetException ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    _dialogService.ShowMessage(ex.Message, AppResources.Information);
                });
                Task.Run(() =>
                {
                    IsLoading = false;
                });
            }
            catch (Exception)
            {
                string MessageForTheUser = AppResources.ZZSomethingwentwrong;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    // IsLoading = false;

                    await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                    Task.Run(() =>
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
                Device.BeginInvokeOnMainThread(async () =>
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
                        //if (Object.Fbtyp.Equals("NREG") || Object.Fbtyp.Equals("ZREG"))
                        //{
                        //    Object.Txt50 = Object.Fbtyp + " - " + Object.Txt50;
                        //}
                        //else
                        //{
                        //    Object.Txt50 = Object.Txt50;
                        //}

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
            catch (Exception ex)
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
