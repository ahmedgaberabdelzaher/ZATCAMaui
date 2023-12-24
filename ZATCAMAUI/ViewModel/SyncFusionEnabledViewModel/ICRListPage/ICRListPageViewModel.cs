using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System.Windows.Input;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.ICRListPage
{

    public class ICRListPageViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public static string EUser = string.Empty;
        public static int numberOfAttachmentComingFromServer = 0;
        public ICommand OnHomeButtonClicked { get; set; }
        public ICommand BackButtonClicked { get; set; }
        public int SelectedPickerIndex { get; set; }
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
        private bool _isNoDataLabelVisible = false;
        public bool IsNoDataLabelVisible
        {
            get
            {
                return _isNoDataLabelVisible;
            }
            set
            {
                _isNoDataLabelVisible = value;
                RaisePropertyChanged("IsNoDataLabelVisible");
            }
        }
        private bool _isICRListVisible = false;
        public bool IsICRListVisible
        {
            get
            {
                return _isICRListVisible;
            }
            set
            {
                _isICRListVisible = value;
                RaisePropertyChanged("IsICRListVisible");
            }
        }
        private ICRStatus _previousSelectedICRStatus;
        public ICRStatus PreviousSelectedICRStatus
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
        private ICRStatus _selectedICRStatus;
        public ICRStatus SelectedICRStatus
        {
            get
            {
                return _selectedICRStatus;
            }
            set
            {
                _selectedICRStatus = value;
                RaisePropertyChanged("SelectedICRStatus");
            }
        }
        private ICRStatus _selectedICRStatusPrev;
        public ICRStatus SelectedICRStatusPrev
        {
            get
            {
                return _selectedICRStatusPrev;
            }
            set
            {
                _selectedICRStatusPrev = value;
                RaisePropertyChanged("SelectedICRStatusPrev");
            }
        }
        private int _sCRSelectedIndex;
        public int ICRSelectedIndex
        {
            get
            {
                return _sCRSelectedIndex;
            }
            set
            {
                _sCRSelectedIndex = value;
                RaisePropertyChanged("ICRSelectedIndex");
            }
        }
        private List<ICRStatus> _iCRStatusList;
        public List<ICRStatus> ICRStatusList
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
        private ICRListSet _selectedICR;
        public ICRListSet SelectedICR
        {
            get
            {
                return _selectedICR;
            }
            set
            {
                try
                {
                    _selectedICR = value;
                    if (SelectedICR != null)
                    {
                        SelectedPickerIndex = ICRSelectedIndex;
                        GetVATAllReturnsAsync();
                    }
                    RaisePropertyChanged("SelectedICR");
                }
                catch (Exception)
                {


                }
            }
        }
        private List<ICRListSet> _iCRDummyList;
        public List<ICRListSet> ICRDummyList
        {
            get
            {
                return _iCRDummyList;
            }
            set
            {
                _iCRDummyList = value;
            }
        }
        private List<ICRListSet> _iCRList;
        public List<ICRListSet> ICRList
        {
            get
            {
                return _iCRList;
            }
            set
            {
                _iCRList = value;
                if (_iCRList != null && _iCRList.Count != 0)
                {
                    IsNoDataLabelVisible = false;
                    IsICRListVisible = true;
                    // SelectedICRStatus = null;
                }
                else
                {
                    IsICRListVisible = false;
                    IsNoDataLabelVisible = true;
                    //  SelectedICRStatus = null;
                }
                RaisePropertyChanged("ICRList");
            }
        }
        #endregion
        #region Constructor
        public ICRListPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            _navigationService = navigationService;
            _dialogService = dialogService;
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
        public async Task onPageLoad()
        {
            try
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });
                await Task.Run(async () =>
                {
                    ICRList = null;
                    ICR icrList = null;
                    try
                    {
                        string lang = UtilityManager.GetLanguageParameter();
                        icrList = WebServiceManager.GAZTGetICRs(App.TP.Tin, lang);
                        PopToRootPage();// If seesion Expired it will navigate to Dashboard page
                        if (icrList != null && icrList.ICR_STATUSSet != null && icrList.ICR_STATUSSet.Count != 0)
                        {
                            ICRStatusList = new List<ICRStatus>();
                            ICRStatusList = icrList.ICR_STATUSSet;
                            if (App.IsArabic)
                            {
                                //foreach (var item in ICRStatusList)
                                //{
                                //    if(item.Txt30== "All")
                                //    {
                                //        item.Txt30 = "الجميع";
                                //    }
                                //    if(item.Txt30== "To be filled & In draft")
                                //    {
                                //        item.Txt30 = "جاهز للتعبئة والحفظ كمسودة";
                                //    }
                                //}
                                //ICRStatusList[ICRStatusList.FindIndex(ind => ind.Equals("All"))].Txt30 = "الجميع";
                                //ICRStatusList[ICRStatusList.FindIndex(ind => ind.Equals("To be filled & In draft"))].Txt30 = "جاهز للتعبئة والحفظ كمسودة";
                                //  ICRStatusList.Where(p => p.Txt30 == "All").();
                            }
                            if (string.IsNullOrEmpty(App.ICRStatus))
                            {
                                SelectedICRStatus = ICRStatusList.Where(x => x.Estat == "E01TP").FirstOrDefault();
                            }
                        }
                        VATDeclaration vATDeclaration = new VATDeclaration();
                        if (icrList != null && icrList.ICR_LISTSet != null && icrList.ICR_LISTSet.Count != 0)
                        {
                            ICRList = new List<ICRListSet>();
                            ICRList = icrList.ICR_LISTSet.OrderByDescending(x => x.DueDateDateTime).ToList();
                            ICRDummyList = ICRList;
                        }
                        else
                        {
                            IsLoading = false;
                            _navigationService.GoBack();
                        }
                    }
                    catch (InternetException ex)
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                            IsLoading = false;
                            _navigationService.GoBack();
                        });
                        //   await Task.Run(() =>
                        //   {
                        //  });
                    }
                });
                await Task.Run(() =>
                {
                    IsLoading = false;
                });
                
            }
            catch (InternetException ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                   await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    IsLoading = false;
                    _navigationService.GoBack();
                });
            }
        }
        public async void GetVATAllReturnsAsync()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                IsLoading = true;
            });
            await GetVATAllReturns();
        }
        private async Task GetVATAllReturns()
        {
            ICRListSet selectedICRForStatus = null;
            try
            {
                try
                {
                    if (SelectedICR != null)
                    {
                        if (isStatusNotValid())
                        {
                            selectedICRForStatus = new ICRListSet();
                            selectedICRForStatus = SelectedICR;
                            App.ICRStatus = selectedICRForStatus.Status;
                            App.EUser = selectedICRForStatus.Euser;
                            App.Fbguid = selectedICRForStatus.Fbguid;
                            //as per discussion with Vinay - the GUID is dynamic and will remain active and attched to ICR in a session. if the list of ICR' sis refreshed; meaning if the API is called again
                            // the GUID will be different
                            string SelectedICRGUID = SelectedICR.Fbguid;
                            EUser = SelectedICR.Euser;

                            VATDeclaration _vATDeclaration = await WebServiceManager.GAZTGetVATReturns(SelectedICR.Fbguid, SelectedICR.Fbnum, SelectedICR.Euser, SelectedICR.Persl);
                            PopToRootPage();
                            if (_vATDeclaration != null && _vATDeclaration.d != null)
                            {
                                PreviousSelectedICRStatus = _selectedICRStatus;
                                _vATDeclaration.d.Fbguid = SelectedICRGUID;
                                VATDeclaration vATDeclaration = new VATDeclaration();
                                VATDeclarationD vATDeclarationD = new VATDeclarationD();
                                if (_vATDeclaration.d.ATTACHSet != null && _vATDeclaration.d.ATTACHSet.results != null && _vATDeclaration.d.ATTACHSet.results.Count > 0)
                                    numberOfAttachmentComingFromServer = _vATDeclaration.d.ATTACHSet.results.Count;
                                Result5 result5 = new Result5();
                                List<Result5> lst = new List<Result5>();
                                ADRSet _aDRSet = new ADRSet();
                                lst.Add(result5);
                                vATDeclaration.d = vATDeclarationD;
                                vATDeclaration.d.ADRSet = _aDRSet;
                                vATDeclaration.d.ADRSet.results = lst;
                                MainThread.BeginInvokeOnMainThread(() =>
                                {
                                    _navigationService.NavigateTo(App.VATReturnsPageViewEX, _vATDeclaration);
                                });
                            }
                            else
                            {
                                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                                MainThread.BeginInvokeOnMainThread(() =>
                                {
                                    IsLoading = false;
                                });
                            }
                        }
                        else
                        {
                            await _dialogService.ShowMessage(AppResources.ZZZReturnUnderReview, AppResources.Information);
                            MainThread.BeginInvokeOnMainThread(() =>
                            {
                                IsLoading = false;
                            });
                        }
                    }
                }
                catch (InternetException )
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                        IsLoading = false;
                        _navigationService.GoBack();
                    });
                }
            }
            catch (InternetException )
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    IsLoading = false;
                    _navigationService.GoBack();
                });
            }
        }
        public bool isStatusNotValid()
        {
            bool isValid = true;
            if (SelectedICR.Status == "E0020" || SelectedICR.Status == "E0057" || SelectedICR.Status == "E0076" || SelectedICR.Status == "E0077" || SelectedICR.Status == "E0078" || SelectedICR.Status == "E0089" || SelectedICR.Status == "E0090")
            {
                isValid = false;
            }
            return isValid;
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
        public void SetICRListData(ICRStatus selectedICRStat)
        {
            if (selectedICRStat != null)
            {
                try
                {
                    if (ICRDummyList != null && ICRDummyList.Count != 0)
                    {
                        if (string.Equals(selectedICRStat.Txt30, "All") || string.Equals(selectedICRStat.Txt30, "الجميع"))
                        {
                            ICRList = ICRDummyList.OrderByDescending(x => x.DueDateDateTime).ToList();
                        }
                        else if (string.Equals(selectedICRStat.Estat, "E01TP"))
                        {
                            ICRList = ICRDummyList.Where(x => x.Status == "E0001" || x.Status == "E0013").OrderByDescending(x => x.DueDateDateTime).ToList();
                        }
                        else
                        {
                            ICRList = ICRDummyList.Where(x => x.Status == selectedICRStat.Estat).OrderByDescending(x => x.DueDateDateTime).ToList();
                        }
                    }
                }
                catch (Exception)
                {


                }
                TxtSelectedStatus = selectedICRStat.Txt30;
            }
        }
        #endregion
    }
}
