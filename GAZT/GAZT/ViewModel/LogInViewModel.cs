using System;
using System;
using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Xamarin.Forms;
namespace GAZT
{
    public class LogInViewModel : ViewModelBase
    {
        #region Variable
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        public ICommand OnLoginButtonClicked { get; set; }
        public ICommand OnOnLanguageClickClicked { get; set; }

        #endregion

        #region Property
        //private bool _isLoading;

        //public bool IsLoading
        //{
        //    get
        //    {
        //        return _isLoading;
        //    }
        //    set
        //    {
        //        _isLoading = value;
        //        RaisePropertyChanged("IsLoading");
        //    }
        //}

        private bool _phoneVisibility;

        public bool PhoneVisibility
        {
            get
            {
                return _phoneVisibility;
            }
            set
            {
                _phoneVisibility = value;
                RaisePropertyChanged("PhoneVisibility");
            }
        }

        private bool _tabletVisibility;

        public bool TabletVisibility
        {
            get
            {
                return _tabletVisibility;
            }
            set
            {
                _tabletVisibility = value;
                RaisePropertyChanged("TabletVisibility");
            }
        }

        #endregion

        #region Constructor

        public LogInViewModel(INavigationService navigationService, IDialogService dialogService)
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
     

            OnLoginButtonClicked = new RelayCommand(async () =>
            {
                
                GAZTAuthenticationService.IsAuthenticatedClient AuthClient = new GAZTAuthenticationService.IsAuthenticatedClient();
                GAZTAuthenticationService.loginValidation lv = new GAZTAuthenticationService.loginValidation();

                lv.userId = "3300049744";
                lv.password = "Test@123";

                GAZTAuthenticationService.loginValidationRequest lvreq = new GAZTAuthenticationService.loginValidationRequest(lv);
                GAZTAuthenticationService.loginValidationResponse1 lvres =  await AuthClient.loginValidationAsync(lvreq);

                lvres.loginValidationResponse.@return = "Success";

                await _dialogService.ShowMessageBox("Login Succesful, please provide OTP in th enext screen","Information");


                bool IsNavigatingFromLogin = true;
                _navigationService.NavigateTo(App.OTPView, IsNavigatingFromLogin);

            });

          
        }

        #endregion

        #region Method
        
        public void OnPageLoad()
        {
            if (Device.Idiom == TargetIdiom.Phone)
            {

                TabletVisibility = false;
                PhoneVisibility = true;
            }
            else
            {
                TabletVisibility = true;
                PhoneVisibility = false;
            }
        }
        #endregion
    }
}
