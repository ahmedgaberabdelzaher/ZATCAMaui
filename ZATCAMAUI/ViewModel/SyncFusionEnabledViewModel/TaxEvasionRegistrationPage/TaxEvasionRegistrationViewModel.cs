using System.Windows.Input;


using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using ZATCAMAUI.ViewModel.NewDesignViewModel;

namespace ZATCAMAUI.ViewModel.SyncFusionEnabledViewModel.TaxEvasionRegistrationPage
{

    public class TaxEvasionRegistrationViewModel : BaseViewModel
    {
        #region variable
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
                OnPropertyChanged("IsVisiblePickerAr");
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
                OnPropertyChanged("IsVisiblePickerEn");
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
                OnPropertyChanged("TxtEmailAddress");
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
                OnPropertyChanged("TxtName");
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
                OnPropertyChanged("TxtReportDetailCity");
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
                OnPropertyChanged("SelectLCType");
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
                OnPropertyChanged("SelectLCTypePrev");
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
                OnPropertyChanged("RList");
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
                OnPropertyChanged("CList");
            }
        }

        public TaxEvasionRegistrationViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService, dialogService)
        {
            BackButtonClicked = new Command( () =>
            {
                var _navigation = Application.Current.MainPage.Navigation;
                _navigationService.GoBack();
            });
        }
        public async Task OnPageLoad()
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
                  await  NoInternetGoBack();
                }

            }
            catch (GAZTNetworkConnectivityIssueException)
            {
                await UtilityManager.HandleExceptionMessage(AppResources.NetworkConnectivityIssue, true, _navigationService);
            }

            catch (InternetException)
            {
                await UtilityManager.HandleExceptionMessage(AppResources.ZZInternetConnectionMessage, false);
            }
            catch (Exception)
            {
                await UtilityManager.HandleExceptionMessage(AppResources.Somethingwentwrong, true, _navigationService);
            }
            finally
            {
                IsLoading = false;
            }
        }
        public async Task NoInternetGoBack()
        {
            await _dialogService.ShowMessage(AppResources.NetworkConnectivityIssue, AppResources.Information);
            _navigationService.GoBack();
        }

        public async Task navigateToListPage()
        {
            IsLoading = true;
            await  _navigationService.NavigateTo(App.TaxEvasionReportListPageView, App.TaxEvasionUserData.Mobile);
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
                   await navigateToListPage();
                }
            }
            catch (GAZTNetworkConnectivityIssueException)
            {
                await UtilityManager.HandleExceptionMessage(AppResources.NetworkConnectivityIssue, false);
            }

            catch (InternetException)
            {
                await UtilityManager.HandleExceptionMessage(AppResources.ZZInternetConnectionMessage, false);
            }
            catch (Exception)
            {
                await UtilityManager.HandleExceptionMessage(AppResources.Somethingwentwrong, false);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
