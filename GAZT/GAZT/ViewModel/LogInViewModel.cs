using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using GAZT.Manager;
using System;
using System.Globalization;
using System.Threading.Tasks;
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

        private string _UserName = "3300087028";
       // private string _UserName = string.Empty;
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

        private string _Password = "Test@123";
       // private string _Password = string.Empty;
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


            OnLoginButtonClicked = new Command(async () =>
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                });


                    //App.IsArabic = true;

                    if (App.IsArabic)
                    {
                        SetRTLDirectionTest();
                    }
                    else
                    {
                        SetLTRDirectionTest();
                    }

                    //String OnAuthenticationSuccess = AppResources.LoginSuccessful;// ResourceManager.GetString("LoginSuccessful");
                    //String OnSuccessfulAuthentication = AppResources.EnterVerificationCode;

                   // await _dialogService.ShowMessageBox(OnAuthenticationSuccess + ":" + OnSuccessfulAuthentication, "Information");

                await Task.Run(async() =>
                {
                    String response = WebServiceManager.GAZTAuthenticateTIN(UserName, Password);
                    if (0 == String.Compare("success", response, true))
                    {
                       


                        String OnAuthenticationSuccessMsg = AppResources.LoginSuccessful;
                        String OnSuccessfulAuthenticationqMsg = AppResources.EnterVerificationCode;

                        Device.BeginInvokeOnMainThread(async() =>
                        {
                            IsLoading = false;
                            await _dialogService.ShowMessageBox(OnAuthenticationSuccessMsg + ":" + OnSuccessfulAuthenticationqMsg, "Information");
                        });


                        String lang = "EN";
                        if (App.IsArabic == true)
                            lang = "AR";
                        await Task.Run(async() =>
                        {
                            response = await WebServiceManager.GAZTSendAndReceiveOTP(lang, UserName);
                            if (0 == String.Compare("OTP has send", response, true) || 0 == String.Compare("كلمة مرور مرة واحدة قد أرسلت", response, true))
                            {
                                App.TP = new Models.TaxPayerProfile();
                                App.TP.Userid = UserName;
                                App.TP.Password = Password;
                                bool IsNavigatingFromLogin = true;

                                if (App.IsArabic)
                                {
                                    SetRTLDirectionTest();
                                }
                                else
                                {
                                    SetLTRDirectionTest();
                                }

                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    _navigationService.NavigateTo(App.OTPView, IsNavigatingFromLogin);
                                });
                            }
                            else
                            {
                                await Task.Run(() =>
                                {
                                    IsLoading = false;
                                });
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    await _dialogService.ShowMessageBox(response, "Information");
                                });
                            }
                        });

                      
                    }
                    else
                    {
                        await Task.Run(() =>
                        {
                            IsLoading = false;
                        });
                        Device.BeginInvokeOnMainThread(async() =>
                        {
                            await _dialogService.ShowMessageBox(response, "Information");
                        });
                    }
                });


                await Task.Run(() =>
                {
                    IsLoading = false;
                });

            });


        }

        public void SetRTLDirectionTest()
        {
           // InitializeComponent();

            String langName = "ar-AE";
            CultureInfo ci = new CultureInfo(langName);
            AppResources.Culture = ci;

            // AppResources.ResourceManager.ReleaseAllResources();
           

        }

        public void SetLTRDirectionTest()
        {

          ///  InitializeComponent();

            String langName = "en-US";
            CultureInfo ci = new CultureInfo(langName);
            AppResources.Culture = ci;
            //var vUpdatedPage = new LogInView();
            //Navigation.InsertPageBefore(vUpdatedPage, this);
            //Navigation.PopAsync();
            


        }

        #endregion

        #region Method


        #endregion
    }
}
