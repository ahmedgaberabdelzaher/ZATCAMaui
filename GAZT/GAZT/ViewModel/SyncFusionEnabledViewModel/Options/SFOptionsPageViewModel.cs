using EGAZT.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EGAZT.ViewModel.SyncFusionEnabledViewModel.SFOptionsPage_ViewModel
{
    /// <summary>
    /// ViewModel for Setting page 
    /// </summary> 
    [Preserve(AllMembers = true)]
    public class SFOptionsPageViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;
        public readonly IDialogService _dialogService;
        private DateTime lastTapped;
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsPageViewModel" /> class
        /// </summary>
        public SFOptionsPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }
            _navigationService = navigationService;
            _dialogService = dialogService;
            if (dialogService == null)
            {
                throw new ArgumentNullException("dialogService");
            }
            this.BackButtonCommand = new Command(this.BackButtonClicked);
            this.EditProfileCommand = new Command(this.EditProfileClicked);
            this.ChangePasswordCommand = new Command(this.ChangePasswordClicked);
            this.LinkAccountCommand = new Command(this.LinkAccountClicked);
            this.HelpCommand = new Command(this.HelpClicked);
            this.TermsCommand = new Command(this.TermsServiceClicked);
            this.PolicyCommand = new Command(this.PrivacyPolicyClicked);
            this.FAQCommand = new Command(this.FAQClicked);
            this.CloseButtonClicked = new Command(this.CloseClicked);
            this.MyProfileCommand = new Command(this.MyProfileCommandClicked);
            this.EditMobileNumberCommand = new Command(this.EditMobileNumberClicked);
            this.EditPasswordCommand = new Command(this.EditPasswordClicked);
            this.EditEmailCommand = new Command(this.EditEmailClicked);
            this.AboutCommand = new Command(this.AboutUsClicked);
            this.CorrespondenceCommand = new Command(this.CorrespondenceClicked);
            this.ContactusCommand = new Command(this.ContactusClicked);
        }
        #endregion
        #region Commands
        public Command MyProfileCommand { get; set; }
        public Command ContactusCommand { get; set; }
        public Command CorrespondenceCommand { get; set; }
        public Command EditMobileNumberCommand { get; set; }
        public Command EditPasswordCommand { get; set; }
        public Command EditEmailCommand { get; set; }
        public Command AboutCommand { get; set; }
        private string _appVersion = App.AppVersion;
        public string AppVersion
        {
            get
            {
                return _appVersion;
            }
            set
            {
                _appVersion = value;
                RaisePropertyChanged("AppVersion");
            }
        }
        private string _translateText;
        public string TranslateText
        {
            get
            {
                return _translateText;
            }
            set
            {
                _translateText = value;
                RaisePropertyChanged("TranslateText");
            }
        }
        private bool _isTaxPayerProfileVisible;
        public bool IsTaxPayerProfileVisible
        {
            get
            {
                return _isTaxPayerProfileVisible;
            }
            set
            {
                _isTaxPayerProfileVisible = value;
                RaisePropertyChanged("_isTaxPayerProfileVisible");
            }
        }
        private bool _isCorrespondenceVisible;
        public bool IsCorrespondenceVisible
        {
            get
            {
                return _isCorrespondenceVisible;
            }
            set
            {
                _isCorrespondenceVisible = value;
                RaisePropertyChanged("IsCorrespondenceVisible");
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
        private ComingToOptionScreenFrom _isComingFrom;
        public ComingToOptionScreenFrom IsComingFrom
        {
            get
            {
                return _isComingFrom;
            }
            set
            {
                _isComingFrom = value;
                RaisePropertyChanged("IsComingFrom");
            }
        }
        /// <summary>
        /// Gets or sets the command is executed when the favourite button is clicked.
        /// </summary>
        public Command BackButtonCommand { get; set; }
        /// <summary>
        /// Gets or sets the command is executed when the edit profile option is clicked.
        /// </summary>
        public Command EditProfileCommand { get; set; }
        /// <summary>
        /// Gets or sets the command is executed when the change password option is clicked.
        /// </summary>
        public Command ChangePasswordCommand { get; set; }
        /// <summary>
        /// Gets or sets the command is executed when the account link option is clicked.
        /// </summary>
        public Command LinkAccountCommand { get; set; }
        /// <summary>
        /// Gets or sets the command is executed when the help option is clicked.
        /// </summary>
        public Command HelpCommand { get; set; }
        /// <summary>
        /// Gets or sets the command is executed when the terms of service option is clicked.
        /// </summary>
        public Command TermsCommand { get; set; }
        /// <summary>
        /// Gets or sets the command is executed when the privacy policy option is clicked.
        /// </summary>
        public Command PolicyCommand { get; set; }
        /// <summary>
        /// Gets or sets the command is executed when the FAQ option is clicked.
        /// </summary>
        public Command FAQCommand { get; set; }
        public Command CloseButtonClicked { get; set; }
        #endregion
        #region Methods
        /// <summary>
        /// Invoked when the back button clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void BackButtonClicked(object obj)
        {
            if (IsComingFrom == ComingToOptionScreenFrom.IsDashboardPage)
            {
                _navigationService.NavigateTo(App.SFLandingPageView);
            }
            else
            {
                _navigationService.NavigateTo(App.SFAnonymousLandingPageView);
            }
            // Do something
        }
        /// <summary>
        /// Invoked when the edit profile option clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void EditProfileClicked(object obj)
        {
            // Do something
        }
        /// <summary>
        /// Invoked when the change password clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void ChangePasswordClicked(object obj)
        {
            // Do something
        }
        /// <summary>
        /// Invoked when the account link clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void LinkAccountClicked(object obj)
        {
            // Do something
        }
        /// <summary>
        /// Invoked when the terms of service clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void TermsServiceClicked(object obj)
        {
            // Do something
        }
        /// <summary>
        /// Invoked when the privacy and policy clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void PrivacyPolicyClicked(object obj)
        {
            _navigationService.NavigateTo(App.PrivacyAndPolicyPageView);
        }
        /// <summary>
        /// Invoked when the FAQ clicked
        /// </summary>
        /// <param name="obj">The object</param>
        /// 
        private void FAQClicked(object obj)
        {
            _navigationService.NavigateTo(App.FAQPageView);
            // Do something
        }
        private void CloseClicked(object obj)
        {
            _navigationService.NavigateTo(App.SFLandingPageView);
            // Do something
        }
        /// <summary>
        /// Invoked when the help option is clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void HelpClicked(object obj)
        {
            // Do something
        }
        private void MyProfileCommandClicked(object obj)
        {
            if (lastTapped < DateTime.Now.AddSeconds(-2))
            {
                lastTapped = DateTime.Now;
                _navigationService.NavigateTo(App.TaxPayerProfilePageView);
            }
        }
        private void CorrespondenceClicked(object obj)
        {
            if (lastTapped < DateTime.Now.AddSeconds(-2))
            {
                lastTapped = DateTime.Now;
                _navigationService.NavigateTo(App.GAZTNewDesignMyReturnsNewPageView);
            }
        }
        private void ContactusClicked(object obj)
        {
            if (lastTapped < DateTime.Now.AddSeconds(-2))
            {
                lastTapped = DateTime.Now;
                _navigationService.NavigateTo(App.ContactUsPageView);
            }
        }
        private void EditMobileNumberClicked(object obj)
        {
            // Do something
        }
        private void EditPasswordClicked(object obj)
        {
            // Do something
        }
        private void EditEmailClicked(object obj)
        {
            // Do something
        }
        private void AboutUsClicked(object obj)
        {
            _navigationService.NavigateTo(App.AboutUsPageView);
        }
        public async Task LogOut()
        {
            await Task.Run(() =>
            {
                App.DisplayProgressView();
            });
            if (App.TP!=null)
                App.TP = null;
            if (App.PreviousIsArabic)
            {
                String langName = "ar-AE";
                AppResources.Culture = new CultureInfo(langName);
            }
            else
            {
                String langName = "en-US";
                AppResources.Culture = new CultureInfo(langName);
            }

            try
            {
                await WebServiceManager.GAZTLogOff();
            }
            catch
            {

            }

            await Task.Run(() =>
            {
                App.HideProgressView();
            });

            var _navigation = Application.Current.MainPage.Navigation; 
            foreach (var item in _navigation.NavigationStack)
            {
                if (item.GetType().Name == App.SFAnonymousLandingPageView)
                {
                    _navigation.RemovePage(item); 
                    break; 
                }
            }

            App.IsLogOut = true;
            App.IsLoginCalled = false;
            App.IsSamlApiCalledAndroid = false;

            try
            {
                App.httpClientHandler = new HttpClientHandler();
                App.httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                App.httpClientHandler.CookieContainer = new System.Net.CookieContainer();
            }
            catch (Exception ex)
            {

            }

            _navigationService.NavigateTo(App.SFAnonymousLandingPageView);
            _navigation.NavigationStack.ToList().Clear();
            //var _navigation = Application.Current.MainPage.Navigation;
            //_navigation.PopToRootAsync();
        }
        #endregion
    }
}
