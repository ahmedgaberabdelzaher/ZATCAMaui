using EGAZT.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Helper;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.TaxPayerProfilePage_ViewModel
{
    [Preserve(AllMembers = true)]
    public class TaxPayerProfilePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        public ICommand OnChangeMobileNumberClicked { get; set; }
        public ICommand OnChangeEmailClicked { get; set; }
        public ICommand OnChangePasswordClicked { get; set; }
        public ICommand OnHomeClick { get; set; }
        public ICommand BackButtonClicked { get; set; }
        public bool IscomingFromOTPViewViaEmail = false;
        #region Property
        private TaxPayerProfile _TaxPayerProfile = App.TP;
        public TaxPayerProfile TaxPayerProfile
        {
            get
            {
                return _TaxPayerProfile;
            }
            set
            {
                _TaxPayerProfile = value;
                RaisePropertyChanged("TaxPayerProfile");
            }
        }
        private bool _passwordVisibility = false;
        public bool PasswordVisibility
        {
            get
            {
                return _passwordVisibility;
            }
            set
            {
                _passwordVisibility = value;
                RaisePropertyChanged("PasswordVisibility");
            }
        }
        private bool _tPProfileVisibility = true;
        public bool TPProfileVisibility
        {
            get
            {
                return _tPProfileVisibility;
            }
            set
            {
                _tPProfileVisibility = value;
                RaisePropertyChanged("TPProfileVisibility");
            }
        }
        private string _CurrentMobile = string.Empty;
        public string CurrentMobile
        {
            get
            {
                return _CurrentMobile;
            }
            set
            {
                _CurrentMobile = value;
                RaisePropertyChanged("CurrentMobile");
            }
        }
        private string _CurrentPassword = string.Empty;
        public string CurrentPassword
        {
            get
            {
                return _CurrentPassword;
            }
            set
            {
                _CurrentPassword = value;
                RaisePropertyChanged("CurrentPassword");
            }
        }
        private string _OldEmail = string.Empty;
        public string OldEmail
        {
            get
            {
                return _OldEmail;
            }
            set
            {
                _OldEmail = value;
                RaisePropertyChanged("OldEmail");
            }
        }
        private string _CurrentPasswordForEmail = string.Empty;
        public string CurrentPasswordForEmail
        {
            get
            {
                return _CurrentPasswordForEmail;
            }
            set
            {
                _CurrentPasswordForEmail = value;
                RaisePropertyChanged("CurrentPasswordForEmail");
            }
        }
        #endregion
        #region Constructor
        public TaxPayerProfilePageViewModel(INavigationService navigationService, IDialogService dialogService)
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
            OnChangeMobileNumberClicked = new Xamarin.Forms.Command(() =>
            {
                _navigationService.NavigateTo(App.ChangeMobileNumberPageView);
            });
            OnChangeEmailClicked = new Xamarin.Forms.Command(async () =>
            {
            
                await _dialogService.ShowMessage(AppResources.ChangeEmailVerification,AppResources.Information);

                _navigationService.NavigateTo(App.ChangeEmailPageView);
            });
            OnChangePasswordClicked = new Xamarin.Forms.Command(() =>
            {
                _navigationService.NavigateTo(App.ChangePasswordPageView, ComingToOTPVerificationScreenFrom.IsLogin);
            });
            //OnHomeClick = new Xamarin.Forms.Command(() =>
            //{
            //    _navigationService.NavigateTo(App.DashboardPageView);
            //});
            BackButtonClicked = new Xamarin.Forms.Command(() =>
            {
                _navigationService.GoBack();
            });
        }
        #endregion
        #region Method
        public  void PopToRootPage()
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
        public void OnPageLoad()
        {
            try
            {
                TaxPayerProfile = App.TP;
                TPProfileVisibility = true;
                CurrentMobile = TaxPayerProfile.Mobile;
                // CurrentPassword = TaxPayerProfile.Password;
                CurrentPassword = "********";
                OldEmail = TaxPayerProfile.Email;
                CurrentPasswordForEmail = TaxPayerProfile.Password;
            }
            catch(Exception ex)
            {
            }
        }
        public async void SetTP()
        {
            try
            {
                String lang = "E";
                if (App.IsArabic == true)
                    lang = "A"; 
                if(TaxPayerProfile !=null && TaxPayerProfile.Tin != null)
                { 
                String mobilenumber = await WebServiceManager.GAZTGetTaxPayerProfile(TaxPayerProfile.Tin, lang);
                 PopToRootPage();
                if (mobilenumber != null)
                {
                    string MobileNo = "+" + mobilenumber.Substring(mobilenumber.Length - 12);
                    if (App.IsArabic)
                    {
                        MobileNo = mobilenumber.Substring(mobilenumber.Length - 12) + "+";
                        if (Device.RuntimePlatform == Device.iOS)
                        {
                            MobileNo = "+" + mobilenumber.Substring(mobilenumber.Length - 12);
                        }
                    }
                    CurrentMobile = MobileNo;
                    App.TP.Mobile = MobileNo;
                    TaxPayerProfile.Mobile = MobileNo;
                    App.TP.NewMobile = string.Empty;
                    TaxPayerProfile.NewMobile = string.Empty;
                     CurrentPassword = "********";

                        // CurrentPassword = TaxPayerProfile.Password;
                    }
                }
            }
            catch (InternetException ex)
            {
                await _dialogService.ShowMessage(ex.Message, AppResources.Information);
            }
        }
        public void ClearData()
        {
            CurrentPasswordForEmail = string.Empty;
        }
        #endregion
    }
}
