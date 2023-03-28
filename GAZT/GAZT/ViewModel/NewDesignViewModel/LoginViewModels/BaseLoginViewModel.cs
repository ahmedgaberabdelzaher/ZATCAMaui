using System;
using EGAZT.Models.LoginModels;
using EGAZT.Services.Interface;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using GalaSoft.MvvmLight.Views;
using System.Net.Http;

namespace EGAZT.ViewModel.NewDesignViewModel.LoginViewModels
{
	public class BaseLoginViewModel : BaseViewModelWithOTP
    {
        string passwordTxt;
        public string PasswordTxt { get { return passwordTxt; } set { passwordTxt = value; RaisePropertyChanged(); } }

        string userNameTxt;
        public string UserNameTxt { get { return userNameTxt; } set { userNameTxt = value; RaisePropertyChanged(); } }


        string email;
        public string Email { get { return email; } set { email = value; RaisePropertyChanged(); } }


        IUserServices _userServices;

        public BaseLoginViewModel(INavigationService navigationServices, IDialogService dialogService, IUserServices userServices, ICommonServices commonServices) : base(navigationServices, dialogService, commonServices)
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

        public async Task<HttpResponseMessage> Login()
        {
            try
            {

                IsLoading = true;
                if (String.IsNullOrEmpty(UserNameTxt) || String.IsNullOrEmpty(PasswordTxt))
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
            catch (Exception ex)
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

