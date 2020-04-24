using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GAZT.ViewModel.SyncFusionEnabledViewModel.MyReturnsPageViewModel
{
    public class MyReturnsPageViewModel : ViewModelBase
    {
        #region Veriables
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand GoBackClick { get; set; }
        public MyReturnsRootObject MyReturns { get; set; }
        #endregion

        #region Properties
        private MyReturnsResult _selectedReturnsZakatSubmited = null;
        public MyReturnsResult SelectedReturnsVATSubmited
        {
            get
            {
                return _selectedReturnsZakatSubmited;
            }
            set
            {
                _selectedReturnsZakatSubmited = value;
                RaisePropertyChanged("SelectedReturnsVATSubmited");
            }
        }
        private List<MyReturnsResult> _returnsZakatSubmited = null;
        public List<MyReturnsResult> ReturnsZakatSubmited
        {
            get
            {
                return _returnsZakatSubmited;
            }
            set
            {
                _returnsZakatSubmited = value;
                RaisePropertyChanged("ReturnsZakatSubmited");
            }
        }

        private List<MyReturnsResult> _returnsZakatNonSubmited = null;
        public List<MyReturnsResult> ReturnsZakatNonSubmited
        {
            get
            {
                return _returnsZakatNonSubmited;
            }
            set
            {
                _returnsZakatNonSubmited = value;
                RaisePropertyChanged("ReturnsZakatNonSubmited");
            }
        }

        private List<MyReturnsResult> _returnsZakatOverDue = null;
        public List<MyReturnsResult> ReturnsZakatOverDue
        {
            get
            {
                return _returnsZakatOverDue;
            }
            set
            {
                _returnsZakatOverDue = value;
                RaisePropertyChanged("ReturnsZakatOverDue");
            }
        }

        private List<MyReturnsResult> _returnsVATSubmited = null;
        public List<MyReturnsResult> ReturnsVATSubmited
        {
            get
            {
                return _returnsVATSubmited;
            }
            set
            {
                _returnsVATSubmited = value;
                RaisePropertyChanged("ReturnsVATSubmited");
            }
        }

        private List<MyReturnsResult> _returnsVATNonSubmited = null;
        public List<MyReturnsResult> ReturnsVATNonSubmited
        {
            get
            {
                return _returnsVATNonSubmited;
            }
            set
            {
                _returnsVATNonSubmited = value;
                RaisePropertyChanged("ReturnsVATNonSubmited");
            }
        }

        private List<MyReturnsResult> _returnsVATOverDue = null;
        public List<MyReturnsResult> ReturnsVATOverDue
        {
            get
            {
                return _returnsVATOverDue;
            }
            set
            {
                _returnsVATOverDue = value;
                RaisePropertyChanged("ReturnsVATOverDue");
            }
        }


        private List<MyReturnsResult> _returnsETSubmited = null;
        public List<MyReturnsResult> ReturnsETSubmited
        {
            get
            {
                return _returnsETSubmited;
            }
            set
            {
                _returnsETSubmited = value;
                RaisePropertyChanged("ReturnsETSubmited");
            }
        }

        private List<MyReturnsResult> _returnsETNonSubmited = null;
        public List<MyReturnsResult> ReturnsETNonSubmited
        {
            get
            {
                return _returnsETNonSubmited;
            }
            set
            {
                _returnsETNonSubmited = value;
                RaisePropertyChanged("ReturnsETNonSubmited");
            }
        }

        private List<MyReturnsResult> _returnsETOverDue = null;
        public List<MyReturnsResult> ReturnsETOverDue
        {
            get
            {
                return _returnsETOverDue;
            }
            set
            {
                _returnsETOverDue = value;
                RaisePropertyChanged("ReturnsETOverDue");
            }
        }

        private List<MyReturnsResult> _returnsWHSubmited = null;
        public List<MyReturnsResult> ReturnsWHSubmited
        {
            get
            {
                return _returnsWHSubmited;
            }
            set
            {
                _returnsWHSubmited = value;
                RaisePropertyChanged("ReturnsWHSubmited");
            }
        }

        private List<MyReturnsResult> _returnsWHNonSubmited = null;
        public List<MyReturnsResult> ReturnsWHNonSubmited
        {
            get
            {
                return _returnsWHNonSubmited;
            }
            set
            {
                _returnsWHNonSubmited = value;
                RaisePropertyChanged("ReturnsWHNonSubmited");
            }
        }

        private List<MyReturnsResult> _returnsWHOverDue = null;
        public List<MyReturnsResult> ReturnsWHOverDue
        {
            get
            {
                return _returnsWHOverDue;
            }
            set
            {
                _returnsWHOverDue = value;
                RaisePropertyChanged("ReturnsWHOverDue");
            }
        }

        private bool _isVisibleVATSumbitted = false;
        public bool IsVisibleVATSumbitted
        {
            get
            {
                return _isVisibleVATSumbitted;
            }
            set
            {
                _isVisibleVATSumbitted = value;
                RaisePropertyChanged("IsVisibleVATSumbitted");
            }
        }
        private bool _isVisibleVATSumbittedLabel = false;
        public bool IsVisibleVATSumbittedLabel
        {
            get
            {
                return _isVisibleVATSumbittedLabel;
            }
            set
            {
                _isVisibleVATSumbittedLabel = value;
                RaisePropertyChanged("IsVisibleVATSumbittedLabel");
            }
        }

        private bool _isVisibleVATNonSumbitted = false;
        public bool IsVisibleVATNonSumbitted
        {
            get
            {
                return _isVisibleVATNonSumbitted;
            }
            set
            {
                _isVisibleVATNonSumbitted = value;
                RaisePropertyChanged("IsVisibleVATNonSumbitted");
            }
        }
        private bool _isVisibleVATNonSumbittedLabel = false;
        public bool IsVisibleVATNonSumbittedLabel
        {
            get
            {
                return _isVisibleVATNonSumbittedLabel;
            }
            set
            {
                _isVisibleVATNonSumbittedLabel = value;
                RaisePropertyChanged("IsVisibleVATNonSumbittedLabel");
            }
        }
 private bool _isVisibleVATOverDue = false;
        public bool IsVisibleVATOverDue
        {
            get
            {
                return _isVisibleVATOverDue;
            }
            set
            {
                _isVisibleVATOverDue = value;
                RaisePropertyChanged("IsVisibleVATOverDue");
            }
        }
        private bool _isVisibleVATOverDueLabel = false;
        public bool IsVisibleVATOverDueLabel
        {
            get
            {
                return _isVisibleVATOverDueLabel;
            }
            set
            {
                _isVisibleVATOverDueLabel = value;
                RaisePropertyChanged("IsVisibleVATOverDueLabel");
            }
        }
       

        private bool _isVisibleZakatSumbitted = false;
        public bool IsVisibleZakatSumbitted
        {
            get
            {
                return _isVisibleZakatSumbitted;
            }
            set
            {
                _isVisibleZakatSumbitted = value;
                RaisePropertyChanged("IsVisibleZakatSumbitted");
            }
        }
        private bool _isVisibleZakatSumbittedLabel = false;
        public bool IsVisibleZakatSumbittedLabel
        {
            get
            {
                return _isVisibleZakatSumbittedLabel;
            }
            set
            {
                _isVisibleZakatSumbittedLabel = value;
                RaisePropertyChanged("IsVisibleZakatSumbittedLabel");
            }
        }

        private bool _isVisibleZakatNonSumbitted = false;
        public bool IsVisibleZakatNonSumbitted
        {
            get
            {
                return _isVisibleZakatNonSumbitted;
            }
            set
            {
                _isVisibleZakatNonSumbitted = value;
                RaisePropertyChanged("IsVisibleZakatNonSumbitted");
            }
        }
        private bool _isVisibleZakatNonSumbittedLabel = false;
        public bool IsVisibleZakatNonSumbittedLabel
        {
            get
            {
                return _isVisibleZakatNonSumbittedLabel;
            }
            set
            {
                _isVisibleZakatNonSumbittedLabel = value;
                RaisePropertyChanged("IsVisibleZakatNonSumbittedLabel");
            }
        }

        private bool _isVisibleZakatOverDue = false;
        public bool IsVisibleZakatOverDue
        {
            get
            {
                return _isVisibleZakatOverDue;
            }
            set
            {
                _isVisibleZakatOverDue = value;
                RaisePropertyChanged("IsVisibleZakatOverDue");
            }
        }
        private bool _isVisibleZakatOverDueLabel = false;
        public bool IsVisibleZakatOverDueLabel
        {
            get
            {
                return _isVisibleZakatOverDueLabel;
            }
            set
            {
                _isVisibleZakatOverDueLabel = value;
                RaisePropertyChanged("IsVisibleZakatOverDueLabel");
            }
        }

        private bool _isVisibleETSumbitted = false;
        public bool IsVisibleETSumbitted
        {
            get
            {
                return _isVisibleETSumbitted;
            }
            set
            {
                _isVisibleETSumbitted = value;
                RaisePropertyChanged("IsVisibleETSumbitted");
            }
        }
        private bool _isVisibleETSumbittedLabel = false;
        public bool IsVisibleETSumbittedLabel
        {
            get
            {
                return _isVisibleETSumbittedLabel;
            }
            set
            {
                _isVisibleETSumbittedLabel = value;
                RaisePropertyChanged("IsVisibleETSumbittedLabel");
            }
        }

        private bool _isVisibleETNonSumbitted = false;
        public bool IsVisibleETNonSumbitted
        {
            get
            {
                return _isVisibleETNonSumbitted;
            }
            set
            {
                _isVisibleETNonSumbitted = value;
                RaisePropertyChanged("IsVisibleETNonSumbitted");
            }
        }
        private bool _isVisibleETNonSumbittedLabel = false;
        public bool IsVisibleETNonSumbittedLabel
        {
            get
            {
                return _isVisibleETNonSumbittedLabel;
            }
            set
            {
                _isVisibleETNonSumbittedLabel = value;
                RaisePropertyChanged("IsVisibleETNonSumbittedLabel");
            }
        }

        private bool _isVisibleETOverDue = false;
        public bool IsVisibleETOverDue
        {
            get
            {
                return _isVisibleETOverDue;
            }
            set
            {
                _isVisibleETOverDue = value;
                RaisePropertyChanged("IsVisibleETOverDue");
            }
        }
        private bool _isVisibleETOverDueLabel = false;
        public bool IsVisibleETOverDueLabel
        {
            get
            {
                return _isVisibleETOverDueLabel;
            }
            set
            {
                _isVisibleETOverDueLabel = value;
                RaisePropertyChanged("IsVisibleETOverDueLabel");
            }
        }

        private bool _isVisibleWHSumbitted = false;
        public bool IsVisibleWHSumbitted
        {
            get
            {
                return _isVisibleWHSumbitted;
            }
            set
            {
                _isVisibleWHSumbitted = value;
                RaisePropertyChanged("IsVisibleWHSumbitted");
            }
        }
        private bool _isVisibleWHSumbittedLabel = false;
        public bool IsVisibleWHSumbittedLabel
        {
            get
            {
                return _isVisibleWHSumbittedLabel;
            }
            set
            {
                _isVisibleWHSumbittedLabel = value;
                RaisePropertyChanged("IsVisibleWHSumbittedLabel");
            }
        }

        private bool _isVisibleWHNonSumbitted = false;
        public bool IsVisibleWHNonSumbitted
        {
            get
            {
                return _isVisibleWHNonSumbitted;
            }
            set
            {
                _isVisibleWHNonSumbitted = value;
                RaisePropertyChanged("IsVisibleWHNonSumbitted");
            }
        }
        private bool _isVisibleWHNonSumbittedLabel = false;
        public bool IsVisibleWHNonSumbittedLabel
        {
            get
            {
                return _isVisibleWHNonSumbittedLabel;
            }
            set
            {
                _isVisibleWHNonSumbittedLabel = value;
                RaisePropertyChanged("IsVisibleWHNonSumbittedLabel");
            }
        }
        
        private bool _isVisibleWHOverDue = false;
        public bool IsVisibleWHOverDue
        {
            get
            {
                return _isVisibleWHOverDue;
            }
            set
            {
                _isVisibleWHOverDue = value;
                RaisePropertyChanged("IsVisibleWHOverDue");
            }
        }
        private bool _isVisibleWHOverDueLabel = false;
        public bool IsVisibleWHOverDueLabel
        {
            get
            {
                return _isVisibleWHOverDueLabel;
            }
            set
            {
                _isVisibleWHOverDueLabel = value;
                RaisePropertyChanged("IsVisibleWHOverDueLabel");
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
                RaisePropertyChanged("IsVATVisible");
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
                RaisePropertyChanged("IsZakatVisible");
            }
        }


        private bool _isETVisible = false;
        public bool IsETVisible
        {
            get
            {
                return _isETVisible;
            }
            set
            {
                _isETVisible = value;
                RaisePropertyChanged("IsETVisible");
            }
        }

        private bool _isWHVisible = false;
        public bool IsWHVisible
        {
            get
            {
                return _isWHVisible;
            }
            set
            {
                _isWHVisible = value;
                RaisePropertyChanged("IsWHVisible");
            }
        }
        #endregion

        #region Custructor
        public MyReturnsPageViewModel(INavigationService navigationService, IDialogService dialogService)
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
            GoBackClick = new Command(async () =>
            {
                _navigationService.GoBack();


            });

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
                RaisePropertyChanged("FDirection");
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
                RaisePropertyChanged("TabIndexStatus");
            }
        }

        private string _submittedVATReturnsCount = string.Empty;
        public string SubmittedVATReturnsCount
        {
            get
            {
                return _submittedVATReturnsCount;
            }
            set
            {
                _submittedVATReturnsCount = value;
                RaisePropertyChanged("SubmittedVATReturnsCount");
            }
        }

        private string _nonSubmittedVATReturnsCount = string.Empty;
        public string NonSubmittedVATReturnsCount
        {
            get
            {
                return _nonSubmittedVATReturnsCount;
            }
            set
            {
                _nonSubmittedVATReturnsCount = value;
                RaisePropertyChanged("NonSubmittedVATReturnsCount");
            }
        }

        private string _overDueVATReturnsCount = string.Empty;
        public string OverDueVATReturnsCount
        {
            get
            {
                return _overDueVATReturnsCount;
            }
            set
            {
                _overDueVATReturnsCount = value;
                RaisePropertyChanged("OverDueVATReturnsCount");
            }
        }

        private string _submittedZakatReturnsCount = string.Empty;
        public string SubmittedZakatReturnsCount
        {
            get
            {
                return _submittedZakatReturnsCount;
            }
            set
            {
                _submittedZakatReturnsCount = value;
                RaisePropertyChanged("SubmittedZakatReturnsCount");
            }
        }

        private string _nonSubmittedZakatReturnsCount = string.Empty;
        public string NonSubmittedZakatReturnsCount
        {
            get
            {
                return _nonSubmittedZakatReturnsCount;
            }
            set
            {
                _nonSubmittedZakatReturnsCount = value;
                RaisePropertyChanged("NonSubmittedZakatReturnsCount");
            }
        }

        private string _overDueZakatReturnsCount = string.Empty;
        public string OverDueZakatReturnsCount
        {
            get
            {
                return _overDueZakatReturnsCount;
            }
            set
            {
                _overDueZakatReturnsCount = value;
                RaisePropertyChanged("OverDueZakatReturnsCount");
            }
        }


        private string _submittedETReturnsCount = string.Empty;
        public string SubmittedETReturnsCount
        {
            get
            {
                return _submittedETReturnsCount;
            }
            set
            {
                _submittedETReturnsCount = value;
                RaisePropertyChanged("SubmittedETReturnsCount");
            }
        }

        private string _nonSubmittedETReturnsCount = string.Empty;
        public string NonSubmittedETReturnsCount
        {
            get
            {
                return _nonSubmittedETReturnsCount;
            }
            set
            {
                _nonSubmittedETReturnsCount = value;
                RaisePropertyChanged("NonSubmittedETReturnsCount");
            }
        }

        private string _overDueETReturnsCount = string.Empty;
        public string OverDueETReturnsCount
        {
            get
            {
                return _overDueETReturnsCount;
            }
            set
            {
                _overDueETReturnsCount = value;
                RaisePropertyChanged("OverDueETReturnsCount");
            }
        }

        private string _submittedWHReturnsCount = string.Empty;
        public string SubmittedWHReturnsCount
        {
            get
            {
                return _submittedWHReturnsCount;
            }
            set
            {
                _submittedWHReturnsCount = value;
                RaisePropertyChanged("SubmittedWHReturnsCount");
            }
        }

        private string _nonSubmittedWHReturnsCount = string.Empty;
        public string NonSubmittedWHReturnsCount
        {
            get
            {
                return _nonSubmittedWHReturnsCount;
            }
            set
            {
                _nonSubmittedWHReturnsCount = value;
                RaisePropertyChanged("NonSubmittedWHReturnsCount");
            }
        }

        private string _overDueWHReturnsCount = string.Empty;
        public string OverDueWHReturnsCount
        {
            get
            {
                return _overDueWHReturnsCount;
            }
            set
            {
                _overDueWHReturnsCount = value;
                RaisePropertyChanged("OverDueWHReturnsCount");
            }
        }
        #endregion

        #region Methods
        //public async void GetVATAllReturnsAsync()
        //{

        //    Device.BeginInvokeOnMainThread(() =>
        //    {
        //        IsLoading = true;
        //    });

        //    await GetVATAllReturns();

        //    Device.BeginInvokeOnMainThread(() =>
        //    {
        //        IsLoading = false;
        //    });
        //}
        //private async Task GetVATAllReturns()
        //{

        //    try
        //    {
        //        try
        //        {

        //            if (SelectedReturnsVATSubmited != null)
        //            {
        //                if (isStatusNotValid())
        //                {

        //                    String SelectedICRGUID = SelectedReturnsVATSubmited.Fbguid;
        //                    EUser = SelectedICR.Euser;
        //                    VATDeclaration _vATDeclaration = await WebServiceManager.GAZTGetVATReturns(SelectedICR.Fbguid, SelectedICR.Fbnum, SelectedICR.Euser, SelectedICR.Persl);
        //                    PopToRootPage();


        //                    if (_vATDeclaration != null && _vATDeclaration.d != null)
        //                    {

        //                        _vATDeclaration.d.Fbguid = SelectedICRGUID;
        //                        VATDeclaration vATDeclaration = new VATDeclaration();
        //                        VATDeclarationD vATDeclarationD = new VATDeclarationD();
        //                        if (_vATDeclaration.d.ATTACHSet != null && _vATDeclaration.d.ATTACHSet.results != null && _vATDeclaration.d.ATTACHSet.results.Count > 0)
        //                            numberOfAttachmentComingFromServer = _vATDeclaration.d.ATTACHSet.results.Count;
        //                        Result5 result5 = new Result5();
        //                        List<Result5> lst = new List<Result5>();
        //                        ADRSet _aDRSet = new ADRSet();

        //                        lst.Add(result5);
        //                        vATDeclaration.d = vATDeclarationD;
        //                        vATDeclaration.d.ADRSet = _aDRSet;
        //                        vATDeclaration.d.ADRSet.results = lst;

        //                        Device.BeginInvokeOnMainThread(() =>
        //                        {
        //                            _navigationService.NavigateTo(App.VATReturnsPageView, _vATDeclaration);
        //                        });

        //                    }
        //                    else
        //                    {
        //                        await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
        //                    }
        //                }
        //                else
        //                {
        //                    await _dialogService.ShowMessage(AppResources.ZZZReturnUnderReview, AppResources.Information);
        //                }
        //            }
        //        }
        //        catch (InternetException ex)
        //        {
        //            Device.BeginInvokeOnMainThread(async () =>
        //            {
        //                await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);

        //                _navigationService.GoBack();

        //            });
        //        }
        //    }
        //    catch (InternetException ex)
        //    {
        //        Device.BeginInvokeOnMainThread(async () =>
        //        {
        //            await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);

        //            _navigationService.GoBack();

        //        });

        //    }

        //}
        public bool isStatusNotValid()
        {
            bool isValid = true;
            if (SelectedReturnsVATSubmited.Fbust == "E0020" || SelectedReturnsVATSubmited.Fbust == "E0057" || SelectedReturnsVATSubmited.Fbust == "E0076" || SelectedReturnsVATSubmited.Fbust == "E0077" || SelectedReturnsVATSubmited.Fbust == "E0078" || SelectedReturnsVATSubmited.Fbust == "E0089" || SelectedReturnsVATSubmited.Fbust == "E0090")
            {
                isValid = false;
            }
            return isValid;
        }
        public void PopToRootPage()
        {
            if (App.IsSessionExpired)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var _navigation = Application.Current.MainPage.Navigation;
                    _navigation.PopToRootAsync();
                });
            }
        }
        public async void OnPageLoad()
        {
            IsZakatVisible = false;
            IsVATVisible = false;
            IsETVisible = false;
            IsWHVisible = false;

            ReturnsZakatNonSubmited = null;
            ReturnsZakatOverDue = null;
            ReturnsZakatSubmited = null;
            List<MyReturnsResult> ReturnsZakatNonSubmitedChild = new List<MyReturnsResult>();
            List<MyReturnsResult> ReturnsZakatOverDueChild = new List<MyReturnsResult>();
            List<MyReturnsResult> ReturnsZakatSubmitedChild = new List<MyReturnsResult>();

            ReturnsVATNonSubmited = null;
            ReturnsVATOverDue = null;
            ReturnsVATSubmited = null;
            List<MyReturnsResult> ReturnsVATNonSubmitedChild = new List<MyReturnsResult>();
            List<MyReturnsResult> ReturnsVATOverDueChild = new List<MyReturnsResult>();
            List<MyReturnsResult> ReturnsVATSubmitedChild = new List<MyReturnsResult>();

            ReturnsETNonSubmited = null;
            ReturnsETOverDue = null;
            ReturnsETSubmited = null;
            List<MyReturnsResult> ReturnsETNonSubmitedChild = new List<MyReturnsResult>();
            List<MyReturnsResult> ReturnsETOverDueChild = new List<MyReturnsResult>();
            List<MyReturnsResult> ReturnsETSubmitedChild = new List<MyReturnsResult>();

            ReturnsWHNonSubmited = null;
            ReturnsWHOverDue = null;
            ReturnsWHSubmited = null;
            List<MyReturnsResult> ReturnsWHNonSubmitedChild = new List<MyReturnsResult>();
            List<MyReturnsResult> ReturnsWHOverDueChild = new List<MyReturnsResult>();
            List<MyReturnsResult> ReturnsWHSubmitedChild = new List<MyReturnsResult>();
            Task GetReturnDataTask = null;
            GetReturnDataTask = Task.Run(() =>
            {
                MyReturns = WebServiceManager.GAZTGetReturnData(UtilityManager.GetLanguageParameter(), App.TP.Userid);
            });

            try
            {
                if (GetReturnDataTask != null)
                    GetReturnDataTask.Wait();
            }
            catch (AggregateException ae)
            {
               
                foreach (var gex in ae.InnerExceptions)
                {
                    // Handle the GAZT custom exception.
                    if (gex is GAZTException)
                    {
                        string MessageForTheUser = gex.Message;
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
                            if (MessageForTheUser == AppResources.ZZInternetConnectionMessage)
                            {
                                await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                _navigationService.GoBack();
                            }
                            else if(MessageForTheUser == AppResources.NetworkConnectivityIssue)
                            {
                                await _dialogService.ShowMessage(MessageForTheUser, AppResources.Information);
                                _navigationService.GoBack();
                            }
                            else if (MessageForTheUser == AppResources.ZYourSessionhasexpiredPleaseLoginagain)
                            {
                                PopToRootPage();
                            }
                           
                        });
                    }
                    // Rethrow any other exception.
                    else
                    {
                        throw;
                    }
                }
            }
            catch (GAZTSessionExpiredException)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZYourSessionhasexpiredPleaseLoginagain, AppResources.Information);
                    PopToRootPage();
                });
            }
            catch (Exception)
            {
               
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                    PopToRootPage();
                });
            }
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
                    if (ItemType == "07")
                    {
                        IsETVisible = true;
                    }
                    if (ItemType == "01")
                    {
                        IsWHVisible = true;
                    }
                }
            }

            
             
            
            if (MyReturns != null && MyReturns.d != null && MyReturns.d.results.Count > 0)
            {

               


                foreach (MyReturnsResult ItemR in MyReturns.d.results)
                {
                    if (ItemR.TaxType == "ITAX" || ItemR.TaxType == "ZAKT")
                    {
                        if (ItemR.StatusTxt == "Submitted")
                        {
                            ReturnsZakatSubmitedChild.Add(ItemR);
                        }
                        else if (ItemR.StatusTxt == "Non Submitted")
                        {
                            ReturnsZakatNonSubmitedChild.Add(ItemR);
                            if (ItemR.Due == "X")
                            {
                                ReturnsZakatOverDueChild.Add(ItemR);
                            }
                        }
                    }
                    if (ItemR.TaxType == "VATX" || ItemR.TaxType == "VTEP")
                    {
                        if (ItemR.StatusTxt == "Submitted")
                        {
                            ReturnsVATSubmitedChild.Add(ItemR);
                        }
                        else if (ItemR.StatusTxt == "Non Submitted")
                        {
                            ReturnsVATNonSubmitedChild.Add(ItemR);
                            if (ItemR.Due == "X")
                            {
                                ReturnsVATOverDueChild.Add(ItemR);
                            }
                        }
                    }
                    if (ItemR.TaxType == "ETAX")
                    {
                        if (ItemR.StatusTxt == "Submitted")
                        {
                            ReturnsETSubmitedChild.Add(ItemR);
                        }
                        else if (ItemR.StatusTxt == "Non Submitted")
                        {
                            ReturnsETNonSubmitedChild.Add(ItemR);
                            if (ItemR.Due == "X")
                            {
                                ReturnsETOverDueChild.Add(ItemR);
                            }
                        }
                    }

                    if (ItemR.TaxType == "WHTX")
                    {
                        if (ItemR.StatusTxt == "Submitted")
                        {
                            ReturnsWHSubmitedChild.Add(ItemR);
                        }
                        else if (ItemR.StatusTxt == "Non Submitted")
                        {
                            ReturnsWHNonSubmitedChild.Add(ItemR);
                            if (ItemR.Due == "X")
                            {
                                ReturnsWHOverDueChild.Add(ItemR);
                            }
                        }
                    }
                }
                if (ReturnsZakatNonSubmitedChild.Count > 0)
                {
                    ReturnsZakatNonSubmited = ReturnsZakatNonSubmitedChild;
                    IsVisibleZakatNonSumbitted = true;
                    IsVisibleZakatNonSumbittedLabel = false;
                    NonSubmittedZakatReturnsCount = AppResources.ZAKATReturns + "(" + ReturnsZakatNonSubmitedChild.Count + ")";
                }
                else
                {
                    IsVisibleZakatNonSumbitted = false;
                    IsVisibleZakatNonSumbittedLabel = true;
                    NonSubmittedZakatReturnsCount = AppResources.ZAKATReturns + "(0)";

                }
                if (ReturnsZakatOverDueChild.Count > 0)
                {
                    ReturnsZakatOverDue = ReturnsZakatOverDueChild;
                    IsVisibleZakatOverDue = true;
                    IsVisibleZakatOverDueLabel = false;
                    OverDueZakatReturnsCount = AppResources.ZAKATReturns + "(" + ReturnsZakatOverDueChild.Count + ")";

                }
                else
                {
                    IsVisibleZakatOverDue = false;
                    IsVisibleZakatOverDueLabel = true;
                    OverDueZakatReturnsCount = AppResources.ZAKATReturns + "(0)";

                }
                if (ReturnsZakatSubmitedChild.Count > 0)
                {
                    ReturnsZakatSubmited = ReturnsZakatSubmitedChild;
                    IsVisibleZakatSumbitted = true;
                    IsVisibleZakatSumbittedLabel = false;
                    SubmittedZakatReturnsCount = AppResources.ZAKATReturns + "(" + ReturnsZakatSubmitedChild.Count + ")";

                }
                else
                {
                    IsVisibleZakatSumbitted = false;
                    IsVisibleZakatSumbittedLabel = true;
                    SubmittedZakatReturnsCount = AppResources.ZAKATReturns + "(0)";

                }
                if (ReturnsVATNonSubmitedChild.Count > 0)
                {
                    ReturnsVATNonSubmited = ReturnsVATNonSubmitedChild;
                    IsVisibleVATNonSumbitted = true;
                    IsVisibleVATNonSumbittedLabel = false;
                    NonSubmittedVATReturnsCount = AppResources.VatReturns + "(" + ReturnsVATNonSubmitedChild.Count + ")";
                }
                else
                {
                    IsVisibleVATNonSumbitted = false;
                    IsVisibleVATNonSumbittedLabel = true;
                    NonSubmittedVATReturnsCount = AppResources.VatReturns + "(0)";

                }
                if (ReturnsVATOverDueChild.Count > 0)
                {
                    ReturnsVATOverDue = ReturnsVATOverDueChild;
                    IsVisibleVATOverDue = true;
                    IsVisibleVATOverDueLabel = false;
                    OverDueVATReturnsCount = AppResources.VatReturns + "(" + ReturnsVATOverDueChild.Count + ")";

                }
                else
                {
                    IsVisibleVATOverDue = false;
                    IsVisibleVATOverDueLabel = true;
                    OverDueVATReturnsCount = AppResources.VatReturns + "(0)";

                }
                if (ReturnsVATSubmitedChild.Count > 0)
                {
                    ReturnsVATSubmited = ReturnsVATSubmitedChild;
                    IsVisibleVATSumbitted = true;
                    IsVisibleVATSumbittedLabel = false;
                    SubmittedVATReturnsCount = AppResources.VatReturns + "(" + ReturnsVATSubmitedChild.Count + ")";

                }
                else
                {
                    IsVisibleVATSumbitted = false;
                    IsVisibleVATSumbittedLabel = true;
                    SubmittedVATReturnsCount = AppResources.VatReturns + "(0)";

                }

                if (ReturnsETNonSubmitedChild.Count > 0)
                {
                    ReturnsETNonSubmited = ReturnsETNonSubmitedChild;
                    IsVisibleETNonSumbitted = true;
                    IsVisibleETNonSumbittedLabel = false;
                    NonSubmittedETReturnsCount = AppResources.ETReturns + "(" + ReturnsETNonSubmitedChild.Count + ")";


                }
                else
                {
                    IsVisibleETNonSumbitted = false;
                    IsVisibleETNonSumbittedLabel = true;
                    NonSubmittedETReturnsCount = AppResources.ETReturns + "(0)";

                }
                if (ReturnsETOverDueChild.Count > 0)
                {
                    ReturnsETOverDue = ReturnsETOverDueChild;
                    IsVisibleETOverDue = true;
                    IsVisibleETOverDueLabel = false;
                    OverDueETReturnsCount = AppResources.ETReturns + "(" + ReturnsETOverDueChild.Count + ")";

                }
                else
                {
                    IsVisibleETOverDue = false;
                    IsVisibleETOverDueLabel = true;
                    OverDueETReturnsCount = AppResources.ETReturns + "(0)";

                }
                if (ReturnsETSubmitedChild.Count > 0)
                {
                    ReturnsETSubmited = ReturnsETSubmitedChild;
                    IsVisibleETSumbitted = true;
                    IsVisibleETSumbittedLabel = false;
                    SubmittedETReturnsCount = AppResources.ETReturns + "(" + ReturnsETSubmitedChild.Count + ")";

                }
                else
                {
                    IsVisibleETSumbitted = false;
                    IsVisibleETSumbittedLabel = true;
                    SubmittedETReturnsCount = AppResources.ETReturns + "(0)";

                }

                if (ReturnsWHNonSubmitedChild.Count > 0)
                {
                    ReturnsWHNonSubmited = ReturnsWHNonSubmitedChild;
                    IsVisibleWHNonSumbitted = true;
                    IsVisibleWHNonSumbittedLabel = false;
                    NonSubmittedWHReturnsCount = AppResources.ZZWithholding + "(" + ReturnsWHNonSubmitedChild.Count + ")";

                }
                else
                {
                    IsVisibleWHNonSumbitted = false;
                    IsVisibleWHNonSumbittedLabel = true;
                    NonSubmittedWHReturnsCount = AppResources.ZZWithholding + "(0)";

                }
                if (ReturnsWHOverDueChild.Count > 0)
                {
                    ReturnsWHOverDue = ReturnsWHOverDueChild;
                    IsVisibleWHOverDue = true;
                    IsVisibleWHOverDueLabel = false;
                    OverDueWHReturnsCount = AppResources.ZZWithholding + "(" + ReturnsWHOverDueChild.Count + ")";

                }
                else
                {
                    IsVisibleWHOverDue = false;
                    IsVisibleWHOverDueLabel = true;
                    OverDueWHReturnsCount = AppResources.ZZWithholding + "(0)";

                }
                if (ReturnsWHSubmitedChild.Count > 0)
                {
                    ReturnsWHSubmited = ReturnsWHSubmitedChild;
                    IsVisibleWHSumbitted = true;
                    IsVisibleWHSumbittedLabel = false;
                    SubmittedWHReturnsCount = AppResources.ZZWithholding + "(" + ReturnsWHSubmitedChild.Count + ")";

                }
                else
                {
                    IsVisibleWHSumbitted = false;
                    IsVisibleWHSumbittedLabel = true;
                    SubmittedWHReturnsCount = AppResources.ZZWithholding + "(0)";

                }
            }
            else
            {
                IsVisibleETNonSumbitted = false;
                IsVisibleETNonSumbittedLabel = true;
                IsVisibleETOverDue = false;
                IsVisibleETOverDueLabel = true;
                IsVisibleETSumbitted = false;
                IsVisibleETSumbittedLabel = true;

                IsVisibleVATNonSumbitted = false;
                IsVisibleVATNonSumbittedLabel = true;
                IsVisibleVATOverDue = false;
                IsVisibleVATOverDueLabel = true;
                IsVisibleVATSumbitted = false;
                IsVisibleVATSumbittedLabel = true;

                IsVisibleZakatNonSumbitted = false;
                IsVisibleZakatNonSumbittedLabel = true;
                IsVisibleZakatOverDue = false;
                IsVisibleZakatOverDueLabel = true;
                IsVisibleZakatSumbitted = false;
                IsVisibleZakatSumbittedLabel = true;

                IsVisibleWHNonSumbitted = false;
                IsVisibleWHNonSumbittedLabel = true;
                IsVisibleWHOverDue = false;
                IsVisibleWHOverDueLabel = true;
                IsVisibleWHSumbitted = false;
                IsVisibleWHSumbittedLabel = true;

                NonSubmittedZakatReturnsCount = AppResources.ZAKATReturns + "(0)";
                OverDueZakatReturnsCount = AppResources.ZAKATReturns + "(0)";

                SubmittedZakatReturnsCount = AppResources.ZAKATReturns + "(0)";

                NonSubmittedVATReturnsCount = AppResources.VatReturns + "(0)";

                OverDueVATReturnsCount = AppResources.VatReturns + "(0)";

                SubmittedVATReturnsCount = AppResources.VatReturns + "(0)";

                NonSubmittedETReturnsCount = AppResources.ETReturns + "(0)";

                OverDueETReturnsCount = AppResources.ETReturns + "(0)";

                SubmittedETReturnsCount = AppResources.ETReturns + "(0)";

                NonSubmittedWHReturnsCount = AppResources.ZZWithholding + "(0)";

                OverDueWHReturnsCount = AppResources.ZZWithholding + "(0)";

                SubmittedWHReturnsCount = AppResources.ZZWithholding + "(0)";


            }

        }
        #endregion
    }
}
