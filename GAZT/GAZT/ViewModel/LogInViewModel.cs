using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using System;
using System.Windows.Input;

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

        private string _UserName = "3300087028";
        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                _UserName = value;
                RaisePropertyChanged("UserName");
            }
        }

        private string _Password="Test@123";
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
                RaisePropertyChanged("Password");
            }
        }

        private bool _isLoading;
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

                IsLoading = true;

                String response = WebServiceManager.GAZTAuthenticateTIN(UserName, Password);

                if (0 == String.Compare("success", response, true))
                {
                    IsLoading = false;

                    String OnAuthenticationSuccess = AppResources.ResourceManager.GetString("LoginSuccessful");
                    String OnSuccessfulAuthentication = AppResources.ResourceManager.GetString("EnterVerificationCode");

                    await _dialogService.ShowMessageBox(OnAuthenticationSuccess + ":" + OnSuccessfulAuthentication, "Information");

                    String lang = "EN";
                    if (App.IsArabic == true)
                        lang = "AR";

                    response = await WebServiceManager.GAZTSendAndReceiveOTP(lang, UserName);

                    if (0 == String.Compare("OTP has send", response, true))
                    {
                        App.TP = new Models.TaxPayerProfile();
                        App.TP.Userid = UserName;
                        bool IsNavigatingFromLogin = true;

                        _navigationService.NavigateTo(App.OTPView, IsNavigatingFromLogin);
                    }
                    else
                    {
                        await _dialogService.ShowMessageBox(response, "Information");
                    }
                }
                else
                {
                    await _dialogService.ShowMessageBox(response, "Information");
                }

            });


        }

        #endregion

        #region Method


        #endregion
    }
}
