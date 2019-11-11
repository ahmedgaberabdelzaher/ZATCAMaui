using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using GAZT.Models;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace GAZT
{
    public class TaxPayerProfileViewModel : ViewModelBase
    {
        #region Variable
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        public ICommand OnChangeMobileNumberClicked { get; set; }
        public ICommand OnChangeEmailClicked { get; set; }
        public ICommand OnChangePasswordClicked { get; set; }
        public ICommand OnSubmitButtonClicked { get; set; }
        public ICommand OnVerifyButtonClicked { get; set; }
        public ICommand OnChangeEmailSubmitButtonClicked { get; set; }
        public ICommand OnChangePasswordButtonClicked { get; set; }
        #endregion

        #region Property
        
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

        private bool _changeMobileNumberLayoutVisibility = false;
        public bool ChangeMobileNumberLayoutVisibility
        {
            get
            {
                return _changeMobileNumberLayoutVisibility;
            }
            set
            {
                _changeMobileNumberLayoutVisibility = value;
                RaisePropertyChanged("ChangeMobileNumberLayoutVisibility");
            }
        }

        private bool _changeEmailLayoutVisibility = false;
        public bool ChangeEmailLayoutVisibility
        {
            get
            {
                return _changeEmailLayoutVisibility;
            }
            set
            {
                _changeEmailLayoutVisibility = value;
                RaisePropertyChanged("ChangeEmailLayoutVisibility");
            }
        }

        private bool _changePasswordayoutVisibility = false;
        public bool ChangePasswordayoutVisibility
        {
            get
            {
                return _changePasswordayoutVisibility;
            }
            set
            {
                _changePasswordayoutVisibility = value;
                RaisePropertyChanged("ChangePasswordayoutVisibility");
            }
        }

        private string _NewMobile=string.Empty;
        public string NewMobile
        {
            get
            {
                return _NewMobile;
            }
            set
            {
                _NewMobile = value;
                RaisePropertyChanged("NewMobile");
            }
        }

        private string _NewPassword = string.Empty;
        public string NewPassword
        {
            get
            {
                return _NewPassword;
            }
            set
            {
                _NewPassword = value;
                RaisePropertyChanged("NewPassword");
            }
        }

        private string _RetypePassword = string.Empty;
        public string RetypePassword
        {
            get
            {
                return _RetypePassword;
            }
            set
            {
                _RetypePassword = value;
                RaisePropertyChanged("RetypePassword");
            }
        }


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


        #endregion

        #region Constructor

        public TaxPayerProfileViewModel(INavigationService navigationService, IDialogService dialogService)
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

            OnChangeMobileNumberClicked = new Command(() =>
            {
               TPProfileVisibility = false;
               ChangeMobileNumberLayoutVisibility = true;
            });

            OnChangeEmailClicked = new Command(() =>
            {
                TPProfileVisibility = false;
                ChangePasswordayoutVisibility = false;
                ChangeEmailLayoutVisibility = true;
            });

            OnChangePasswordClicked = new Command(() =>
            {
                TPProfileVisibility = false;
                ChangePasswordayoutVisibility = true;
            });

            OnSubmitButtonClicked = new Command(() =>
            {
                //_navigationService.NavigateTo(App.DashboardView);
                ChangeMobileNumberLayoutVisibility = false;
                TPProfileVisibility = true;

            });

            OnVerifyButtonClicked = new Command(async() =>
            {
                bool IsNavigatingFromLogin = false;

                // IsLoading = true;
               // TaxPayerProfile.Mobile = "00966534534645";
                String lang = "EN";
                if (App.IsArabic == true)
                    lang = "AR";

               

                bool response = await WebServiceManager.GAZTValidateMobileNumber(lang,TaxPayerProfile.Tin,TaxPayerProfile.Mobile,NewMobile);

                if(response==true)
                {

                    //TaxPayerProfile.NewMobile = NewMobile;
                    //App.TP.NewMobile = NewMobile;

                    String OnAuthenticationSuccess = AppResources.ResourceManager.GetString("MobileNumberVerificationSuccessful");

                    String OnSuccessfulAuthentication = AppResources.ResourceManager.GetString("EnterVerificationCode");

                    await _dialogService.ShowMessageBox(OnAuthenticationSuccess + ":" + OnSuccessfulAuthentication, "Information");

                    _navigationService.NavigateTo(App.OTPView, IsNavigatingFromLogin);

                  
                        
                }




            });



            OnChangeEmailSubmitButtonClicked = new Command(() =>
            {
                ChangeEmailLayoutVisibility = false;
                TPProfileVisibility = true;
                //bool IsNavigatingFromLogin = false;
                //_navigationService.NavigateTo(App.OTPView, IsNavigatingFromLogin);

            });

            OnChangePasswordButtonClicked = new Command(async() =>
            {
                bool IsNavigatingFromLogin = false;

                // IsLoading = true;
               // TaxPayerProfile.Mobile = "00966534534645";
                String lang = "EN";
                if (App.IsArabic == true)
                    lang = "AR";

                if (0 == String.Compare(NewPassword,RetypePassword, true))
                {
                    bool response = await WebServiceManager.GAZTValidateAndChangePassword(lang, TaxPayerProfile.Tin, TaxPayerProfile.Password, NewPassword);

                    if (response == true)
                    {

                        TaxPayerProfile.NewPassword = NewPassword;
                        App.TP.NewPassword = NewPassword;
                        App.TP.Password = NewPassword;
                        TaxPayerProfile.Password = NewPassword;

                        String OnAuthenticationSuccess = AppResources.ResourceManager.GetString("PassWordChangedSucessfully");


                        await _dialogService.ShowMessageBox(OnAuthenticationSuccess , "Information");

                        
                    }
                    else
                    {

                    }




                    ChangePasswordayoutVisibility = false;
                    TPProfileVisibility = true;
                }
                else
                {
                    await _dialogService.ShowMessageBox("New Password and RetypePasswordNotMatch ", "Information");
                }
                //bool IsNavigatingFromLogin = false;
                //_navigationService.NavigateTo(App.OTPView, IsNavigatingFromLogin);

            });

        }

        #endregion

        #region Method

        public async void SetTP()
        {
            String lang = "E";
            if (App.IsArabic == true)
                lang = "A";
            String mobilenumber = await WebServiceManager.GAZTGetTp(TaxPayerProfile.Tin, lang);
            TaxPayerProfile.Mobile = mobilenumber;
        }

        public void ClearData()
        {
            NewMobile = string.Empty;
            RetypePassword = string.Empty;
            NewPassword = string.Empty;
        }

        public void OnPageLoad()
        {
            TPProfileVisibility = true;
            ChangePasswordayoutVisibility = false;
            ChangeMobileNumberLayoutVisibility = false;
            ChangeEmailLayoutVisibility = false;
        }
        #endregion
    }
}
