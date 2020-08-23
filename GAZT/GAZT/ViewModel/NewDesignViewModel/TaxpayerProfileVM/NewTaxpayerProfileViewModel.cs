using System;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using Xamarin.Forms;

namespace EGAZT.ViewModel.NewDesignViewModel.TaxpayerProfileVM
{
    public class NewTaxpayerProfileViewModel : ViewModelBase
    {
        #region Variable
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;

        public string UpdatedMobileNumber = string.Empty;
        public string UpdatedEmail = string.Empty;
        #endregion

        #region Properties
        private bool _IsLoading = false;
        public bool IsLoading
        {
            get
            {
                return _IsLoading;
            }
            set
            {
                _IsLoading = value;
                RaisePropertyChanged(() => IsLoading);
            }
        }
        private TINStatus _listTINStatus;
        public TINStatus ListTINStatus
        {
            get
            {
                return _listTINStatus;
            }
            set
            {
                _listTINStatus = value;
                RaisePropertyChanged("ListTINStatus");
            }
        }
        private string _TinStatusLabelText;
        public string TinStatusLabelText
        {
            get
            {
                return _TinStatusLabelText;
            }
            set
            {
                _TinStatusLabelText = value;
                RaisePropertyChanged("TinStatusLabelText");
            }
        }
        private string _TPProfileNameLbl;
        public string TPProfileNameLbl
        {
            get { return _TPProfileNameLbl; }
            set
            {
                _TPProfileNameLbl = value;
                RaisePropertyChanged("TPProfileNameLbl");
            }
        }

        private string _TINLabel;
        public string TINLabel
        {
            get { return _TINLabel; }
            set
            {
                _TINLabel = value;
                RaisePropertyChanged("TINLabel");
            }
        }

        private string _MobileNumber;
        public string MobileNumber
        {
            get { return _MobileNumber; }
            set
            {
                _MobileNumber = value;
                RaisePropertyChanged("MobileNumber");
            }
        }

        private string _EmailEntry;
        public string EmailEntry
        {
            get { return _EmailEntry; }
            set
            {
                _EmailEntry = value;
                RaisePropertyChanged("EmailEntry");
            }
        }

        private string _PasswordEntry;
        public string PasswordEntry
        {
            get { return _PasswordEntry; }
            set
            {
                _PasswordEntry = value;
                RaisePropertyChanged("PasswordEntry");
            }
        }

        private string _ShowHidePasswordImage;
        public string ShowHidePasswordImage
        {
            get { return _ShowHidePasswordImage; }
            set
            {
                _ShowHidePasswordImage = value;
                RaisePropertyChanged("ShowHidePasswordImage");
            }
        }
        #endregion

        public NewTaxpayerProfileViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null) { throw new ArgumentNullException("navigationService"); }
            _navigationService = navigationService;

            if (dialogService == null) { throw new ArgumentNullException("dialogService"); }
            _dialogService = dialogService;
        }

        public async Task<TaxPayerProfile> GetTPProfileData()
        {
            /*await Task.Run(() =>
            {
                IsLoading = true;
            });*/

            IsLoading = true;
            TaxPayerProfile APIResponse = null;
            string lang = "EN";
            if (App.IsArabic == true) { lang = "AR"; }

            try
            {
                await Task.Run(async () =>
                {
                    APIResponse = WebServiceManager.SFGAZTGetTaxPayerProfile(App.TP.Tin, lang);
                    IsLoading = false;
                });

            }
            catch (Exception ex)
            {
                IsLoading = false;
                System.Diagnostics.Debug.WriteLine("Exception : ", ex.Message);
                Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                {
                    await _dialogService.ShowMessageBox(ex.Message, AppResources.Information);
                });
            }

            return APIResponse;
        }

        public async Task GetTinStatusDATA()
        {
            string Lang = UtilityManager.GetLanguageParameter();
            ListTINStatus = new TINStatus();
            try
            { 

           
                ListTINStatus = await WebServiceManager.GAZTGetTinStatus(Lang, App.TP.Tin);
                PopToRootPage();
                IsLoading = false;
                if (ListTINStatus!=null)
                {
                    if (ListTINStatus.d != null)
                    {
                        if (!String.IsNullOrEmpty(ListTINStatus.d.StatusText))
                        {
                            TinStatusLabelText = ListTINStatus.d.StatusText;
                        }
                        else
                        {
                            TinStatusLabelText = " - ";
                        }
                    }
                    else
                    {
                        TinStatusLabelText = " - ";
                    }

                }
                else
                {
                    TinStatusLabelText = " - ";
                }
             


            }
            catch
            {
             
            
                TinStatusLabelText = " - ";
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
    }
}
