using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using EGAZT.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Xamarin.Forms;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.TaxEvasionRegistrationPage
{
    public class TaxEvasionRegistrationViewModel: ViewModelBase
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
                this.RaisePropertyChanged("IsLoading");
            }
        }
        private string _tEmail = string.Empty;
        public string TEmail
        {
            get
            {
                return _tEmail;
            }
            set
            {
                _tEmail = value;
                RaisePropertyChanged("TEmail");
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

        public TaxEvasionRegistrationViewModel(INavigationService navigationService, IDialogService dialogService)
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

            this.RegisterUserClicked = new Command(this.RegisterCommandClick);
        }

        public async void RegisterCommandClick()
        {
            try
            {
                IsLoading = true;
                TaxEvasionRegisterUserModel taxEvasionRegisterUserModel = new TaxEvasionRegisterUserModel();
                taxEvasionRegisterUserModel.FullName = TxtName;
                taxEvasionRegisterUserModel.Mobile = App.TaxEvasionUserData.Mobile;
                taxEvasionRegisterUserModel.Email = TEmail;
                taxEvasionRegisterUserModel.City = TxtReportDetailCity;

                TaxEvasionUserRegistrationResponseModel taxEvasionUserRegistrationResponseModel = new TaxEvasionUserRegistrationResponseModel();
                taxEvasionUserRegistrationResponseModel = await WebServiceManager.GAZTTaxEvasionRegisterUser(taxEvasionRegisterUserModel);
                App.TaxEvasionUserData = taxEvasionUserRegistrationResponseModel.Data;
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
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Run(() =>
                    {
                        IsLoading = false;
                    });

                    await _dialogService.ShowMessage(ex.Message, AppResources.Information);
                    //viewModel._navigationService.GoBack();
                });
            }
        }
    }
}
