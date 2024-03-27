using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.TaxEvasionRegistrationPage
{

    public class TaxEvasionRegistrationViewModel : BaseViewModel
    {
        #region variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand BackButtonClicked { get; set; }
        public ICommand RegisterUserClicked { get; set; }

        #endregion

        private bool _isVisiblePickerAr = false;
        public bool IsVisiblePickerAr
        {
            get
            {
                return _isVisiblePickerAr;
            }
            set
            {
                _isVisiblePickerAr = value;
                RaisePropertyChanged("IsVisiblePickerAr");
            }
        }
        private bool _isVisiblePickerEn = false;
        public bool IsVisiblePickerEn
        {
            get
            {
                return _isVisiblePickerEn;
            }
            set
            {
                _isVisiblePickerEn = value;
                RaisePropertyChanged("IsVisiblePickerEn");
            }
        }
       
        private string _txtEmailAddress = string.Empty;
        public string TxtEmailAddress
        {
            get
            {
                return _txtEmailAddress;
            }
            set
            {
                _txtEmailAddress = value;
                RaisePropertyChanged("TxtEmailAddress");
            }
        }

        private string _txtName = string.Empty;
        public string TxtName
        {
            get
            {
                return _txtName;
            }
            set
            {
                _txtName = value;
                RaisePropertyChanged("TxtName");
            }
        }

        private string _txtReportDetailCity = string.Empty;
        public string TxtReportDetailCity
        {
            get
            {
                return _txtReportDetailCity;
            }
            set
            {
                _txtReportDetailCity = value;
                RaisePropertyChanged("TxtReportDetailCity");
            }
        }

        private TaxEvasionRegionCityDatum _selectLCType = null;
        public TaxEvasionRegionCityDatum SelectLCType
        {
            get
            {
                return _selectLCType;
            }
            set
            {
                _selectLCType = value;
                if (_selectLCType != null)
                {
                    if (App.IsArabic)
                    {
                        TxtReportDetailCity = _selectLCType.Name;
                    }
                    else
                    {
                        TxtReportDetailCity = _selectLCType.Name;
                    }
                }
                RaisePropertyChanged("SelectLCType");
            }

        }

        private TaxEvasionRegionCityDatum _selectLCTypePrev = null;
        public TaxEvasionRegionCityDatum SelectLCTypePrev
        {
            get
            {
                return _selectLCTypePrev;
            }
            set
            {
                _selectLCTypePrev = value;
                RaisePropertyChanged("SelectLCTypePrev");
            }
        }
        private List<TaxEvasionRegionCityDatum> _rList = null;
        public List<TaxEvasionRegionCityDatum> RList
        {
            get
            {
                return _rList;
            }
            set
            {
                _rList = value;
                RaisePropertyChanged("RList");
            }
        }

        private List<TaxEvasionRegionCityDatum> _cityList = null;
        public List<TaxEvasionRegionCityDatum> CList
        {
            get
            {
                return _cityList;
            }
            set
            {
                _cityList = value;
                RaisePropertyChanged("CList");
            }
        }

        public TaxEvasionRegistrationViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
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
            BackButtonClicked = new Command(async () =>
            {
                var _navigation = Application.Current.MainPage.Navigation;
                _navigationService.GoBack();
            });

            //this.RegisterUserClicked = new Command(this.RegisterCommandClick);
        }
        public async Task OnPageLoad()
        {
            try
            {

                try
                {

                    TaxEvasionRegionsCityModel regionlist = new TaxEvasionRegionsCityModel();
                    regionlist = await TaxEvasionWebServiceManager.GAZTTaxEvasionGetAllRegions();

                    if (regionlist != null && regionlist.Data.Count() != 0)
                    {
                        if (CList != null && CList.Count > 0)
                        {
                            CList.Clear();
                            TxtReportDetailCity = string.Empty;
                        }

                        CList = regionlist.Data;
                    }
                    else
                    {
                        NoInternetGoBack();
                    }

                }
                catch (InternetException ex)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                     {
                         _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                         _navigationService.GoBack();
                     });
                }
            }
            catch (Exception)
            {



                MainThread.BeginInvokeOnMainThread(async () =>
                 {
                     _dialogService.ShowMessage(AppResources.ZZSomethingwentwrong, AppResources.Information);
                     _navigationService.GoBack();
                 });
            }
        }
        public async void NoInternetGoBack()
        {
            MainThread.BeginInvokeOnMainThread(async () =>
             {
                 await _dialogService.ShowMessage(AppResources.NetworkConnectivityIssue, AppResources.Information);
                 _navigationService.GoBack();
             });
        }

        public async Task navigateToListPage()
        {
            await Task.Run(() =>
            {
                IsLoading = true;
            });

            _navigationService.NavigateTo(App.TaxEvasionReportListPageView, App.TaxEvasionUserData.Mobile);
        }

        public async Task RegisterCommandClick()
        {
            try
            {
                IsLoading = true;
                TaxEvasionRegisterUserModel taxEvasionRegisterUserModel = new TaxEvasionRegisterUserModel();
                taxEvasionRegisterUserModel.FullName = TxtName;
                taxEvasionRegisterUserModel.Mobile = App.TaxEvasionUserData.Mobile;
                taxEvasionRegisterUserModel.Email = TxtEmailAddress;
                taxEvasionRegisterUserModel.City = TxtReportDetailCity;

                TaxEvasionUserRegistrationResponseModel taxEvasionUserRegistrationResponseModel = new TaxEvasionUserRegistrationResponseModel();
                taxEvasionUserRegistrationResponseModel = await TaxEvasionWebServiceManager.GAZTTaxEvasionRegisterUser(taxEvasionRegisterUserModel);

                IsLoading = false;
                if (taxEvasionUserRegistrationResponseModel.Status == true)
                {
                    App.TaxEvasionUserData = taxEvasionUserRegistrationResponseModel.Data;
                    navigateToListPage();
                }
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
            catch (Exception ex)
            {


                MainThread.BeginInvokeOnMainThread(async () =>
                 {
                     await Task.Run(() =>
                     {
                         IsLoading = false;
                     });

                     await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                 });
            }
        }
    }
}
