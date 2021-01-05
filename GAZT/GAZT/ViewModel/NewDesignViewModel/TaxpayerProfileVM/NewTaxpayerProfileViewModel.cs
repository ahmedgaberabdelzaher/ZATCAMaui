using System;
using System.Linq;
using System.Threading.Tasks;
using EGAZT.Models.TPProfile;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.NewDesignViewModel.TaxpayerProfileVM
{
    [Preserve(AllMembers = true)]
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
            get { return _IsLoading; }
            set
            {
                if (_IsLoading == value) return;
                _IsLoading = value;
                RaisePropertyChanged(() => IsLoading);
            }
        }
        private TINStatus _listTINStatus;
        public TINStatus ListTINStatus
        {
            get { return _listTINStatus; }
            set
            {
                if (_listTINStatus == value) return;

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
                if (_TinStatusLabelText == value) return;

                _TinStatusLabelText = value;
                RaisePropertyChanged("TinStatusLabelText");
            }
        }
        private string _ResidenceText;
        public string ResidenceText 
        {
            get
            {
                return _ResidenceText;
            }
            set
            {
                if (_ResidenceText == value) return;

                _ResidenceText = value;
                RaisePropertyChanged("ResidenceText");
            }
        }
        private string _TPProfileNameLbl;
        public string TPProfileNameLbl
        {
            get { return _TPProfileNameLbl; }
            set
            {

                if (_TPProfileNameLbl == value) return;

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
                if (_TINLabel == value) return;

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
                if (_MobileNumber == value) return;

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
                if (_EmailEntry == value) return;

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
                if (_PasswordEntry == value) return;

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
                if (_ShowHidePasswordImage == value) return;

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

            // * TIN STATUS API CALLS
            Device.BeginInvokeOnMainThread(async () =>
            {
                await this.GetTinStatusDATA();
            });
        }

        public async Task GetTinStatusDATA()
        {
            IsLoading = true;
            string Lang = UtilityManager.GetLanguageParameter();
            ListTINStatus = new TINStatus();

            try
            {
                ListTINStatus = await WebServiceManager.GAZTGetTinStatus(Lang, App.TP.Tin);
                UpdateTinStatus();
                IsLoading = false;

                // Session Expired Or Not
                PopToRootPage();
            }
            catch(Exception ex)
            {
                IsLoading = false;
                TinStatusLabelText = " - ";
                Console.Write(ex.ToString());
                Console.Write(ex.StackTrace.ToString());
            }
        }

        private void UpdateTinStatus()
        {
            if (ListTINStatus != null)
            {
                if (ListTINStatus.d != null)
                {
                    if (!String.IsNullOrEmpty(ListTINStatus.d.StatusText))
                        TinStatusLabelText = ListTINStatus.d.StatusText;
                    else
                        TinStatusLabelText = " - ";
                }
                else
                    TinStatusLabelText = " - ";
            }
            else
                TinStatusLabelText = " - ";
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
                    //_navigationService.NavigateTo(App.SFAnonymousLandingPageView);
                    //_navigation.NavigationStack.ToList().Clear();

                    _navigationService.NavigateTo(App.SFLoginPageView, App.GAZTNewDesignDashBoardPageView);
                    _navigation.NavigationStack.ToList().Clear();
                });
            }
        }
    }
}
