using System.Windows.Input;

using Newtonsoft.Json;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Services.Interface;
using ZATCAMAUI.Models.LoginModels;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.LoginViewModels
{
    public class CustomLoginViewModel : BaseViewModelWithOTP
    {
        string passwordTxt;
        public string PasswordTxt { get { return passwordTxt; } set { passwordTxt = value; OnPropertyChanged(); } }

        string userNameTxt;
        public string UserNameTxt { get { return userNameTxt; } set { userNameTxt = value; OnPropertyChanged(); } }


        string email;
        public string Email { get { return email; } set { email = value; OnPropertyChanged(); } }


        IUserServices _userServices;

        public CustomLoginViewModel(INavigationService navigationServices, IDialogService dialogService, IUserServices userServices, ICommonServices commonServices) : base(navigationServices, dialogService, commonServices)
        {

            _userServices = userServices;
        }

        public ICommand LoginCommand
        {
            get
            {
                return new Command(() =>
                {
                    Login();
                });

            }
        }

        public async Task Login()
        {
            IsLoading = true;
            if (string.IsNullOrEmpty(UserNameTxt) || string.IsNullOrEmpty(PasswordTxt))
            {
                IsShowMsgView = true;
                MessageTxt = AppResources.ZEntertherequiredfield;
                return;
            }
            CustomLoginModel body = new CustomLoginModel()
            {
                userName = UserNameTxt,
                password = PasswordTxt

            };

            var res = await _userServices.CustomLogin(body);
            if (res.IsSuccessStatusCode)
            {
                var content = await res.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<CustomLoginResponse>(content);
                if (result != null && result.isSuccess)
                {

                    Email = result.data.firstName + " " + result.data.thirdName;
                    await SendOtpSMS(result.data.mobileNumber);
                    IsOTPView = true;

                    //_navigationService.NavigateTo("/CustomDashBoardVi");
                    Preferences.Set("CustomUser", content);
                }
                else
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.InvalidUserNameOrPassword;
                }
            }
            else
            {
                IsShowMsgView = true;
                MessageTxt = AppResources.InvalidUserNameOrPassword;
            }
            IsLoading = false;

        }

        public ICommand VerifyOTPCommand
        {
            get
            {
                return new Command(async () =>
                {

                    bool isverified = VerifyOtp();
                    if (isverified)
                    {
                        _navigationService.NavigateTo("/CustomDashBoardVi");
                    }

                });
            }
        }

        public ICommand GoToCustomLoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    UserNameTxt = PasswordTxt = "";
                    _navigationService.NavigateTo("CustomLogin");
                });
            }
        }

        public ICommand GoToZakatLoginCommand
        {
            get
            {
                return new Command(() =>
                {
                    _navigationService.NavigateTo(App.SFLoginPageView, App.GAZTNewDesignDashBoardPageView);
                });
            }
        }

        public ICommand GoToZakatRegistrationCommand
        {
            get
            {
                return new Command(() =>
                {
                    _navigationService.NavigateTo(App.EstablishmentSignUPPageView);
                });
            }
        }
    }
}
