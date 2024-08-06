using System.Windows.Input;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Services.Interface;
using ZATCAMAUI.Models.LoginModels;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.LoginViewModels
{
    public class BaseLoginViewModel : BaseViewModelWithOTP
    {
        string passwordTxt;
        public string PasswordTxt { get { return passwordTxt; } set { passwordTxt = value; OnPropertyChanged(); } }

        string userNameTxt;
        public string UserNameTxt { get { return userNameTxt; } set { userNameTxt = value; OnPropertyChanged(); } }


        string email;
        public string Email { get { return email; } set { email = value; OnPropertyChanged(); } }


        public IUserServices _userServices;

        public BaseLoginViewModel(INavigationService navigationServices, IDialogService dialogService, IUserServices userServices, ICommonServices commonServices) : base(navigationServices, dialogService, commonServices)
        {

            _userServices = userServices;
        }

        public ICommand LoginCommand
        {
            get
            {
                return new Command(async() =>
                {
                   await Login();

                });

            }
        }

        public async Task<HttpResponseMessage> Login()
        {
            try
            {

                IsLoading = true;
                if (string.IsNullOrEmpty(UserNameTxt) || string.IsNullOrEmpty(PasswordTxt))
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.InquiryDataRequiredAttentionMsg;
                    IsLoading = false;
                    return null;
                }
                CustomLoginModel body = new CustomLoginModel()
                {
                    userName = UserNameTxt,
                    password = PasswordTxt

                };

                var res = await _userServices.CustomLogin(body);
                IsLoading = false;

                return res;

            }
            catch (Exception)
            {
                return null;
            }
            finally { IsLoading = false; }

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



    }
}

