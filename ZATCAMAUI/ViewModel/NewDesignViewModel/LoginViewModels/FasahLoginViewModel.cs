using System.Windows.Input;

using Newtonsoft.Json;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Core.Services.Interface;
using ZATCAMAUI.Models.CustomServices.FasahModels;

namespace ZATCAMAUI.ViewModel.NewDesignViewModel.LoginViewModels
{
    public class FasahLoginViewModel : BaseLoginViewModel
    {

        public FasahLoginViewModel(INavigationService navigationServices, IDialogService dialogService, IUserServices userServices, ICommonServices commonServices) : base(navigationServices, dialogService, userServices, commonServices)
        {

        }

        public ICommand LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await Login();

                });

            }
        }

        public new ICommand VerifyOTPCommand
        {
            get
            {
                return new Command(async () =>
                {
                  await  VerifyFASAhOtp();

                });
            }
        }

        public new ICommand ResendOtpCommand
        {

            get
            {
                return new Command(async () =>
                {

                    await ResendOtp();

                });
            }
        }

        public async Task ResendOtp()
        {
            ResendOtpBody body = new ResendOtpBody()
            {
                username = UserNameTxt
            };
            var res = await _userServices.ResendOtp(body, Token);
            if (res.IsSuccessStatusCode)
            {
                var content = await res.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<LoginVerifyResponseBody>(content);
                if (data != null)
                {


                    IsOtpValid = true;
                    IsResendCodeEnabled = false;
                    StartOTPTimer();
                    Token = data.token;
                }
            }

            else
            {

                var content = await res.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<LoginVerifyResponseBody>(content);
                if (data != null)
                {
                    IsShowMsgView = true;
                    MessageTxt = data.message;
                }
                else
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.ServerError;
                }

            }

        }


        protected async Task<bool> VerifyFASAhOtp()
        {
            try
            {
                IsLoading = true;
                if (IsOtpValid)
                {


                    EnteredOTP = OTPFirstDigit + OTPSecondDigit + OTPThirdDigit + OTPFourthDigit + OTPFithDigit + OTPSixDigit;
                    VerifyLoginBody body = new VerifyLoginBody()
                    {
                        smsCode = EnteredOTP
                    };
                    var res = await _userServices.VerifyFasahLoginOtp(body, Token);
                    if (res.IsSuccessStatusCode)
                    {
                        var content = await res.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<LoginVerifyResponseBody>(content);
                        if (data != null)
                        {

                            Token = data.token;
                            _navigationService.NavigateTo("ReiewPreviousDeclerations", Token);
                            StopTimer();
                            ClearOTPData();
                            IsOTPView = false;
                        }
                    }

                    else if (res.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    {

                        var content = await res.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<LoginVerifyResponseBody>(content);
                        if (data != null)
                        {
                            IsShowMsgView = true;
                            MessageTxt = data.message;
                        }
                        else
                        {
                            IsShowMsgView = true;
                            MessageTxt = AppResources.ServerError;
                        }

                    }
                    else
                    {
                        var content = await res.Content.ReadAsStringAsync();
                    }
                    IsLoading = false;
                }
                else
                {
                    IsShowMsgView = true;
                    MessageTxt = AppResources.InvalidOTP;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
            finally { IsLoading = false; }
        }


        string Token = "";
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
                var credentials = string.Format("{0}:{1}", UserNameTxt, PasswordTxt);
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(credentials);
                var cred = Convert.ToBase64String(plainTextBytes);
                FasahLoginBody body = new FasahLoginBody()
                {
                    credentials = cred
                };

                var res = await _userServices.FasahLogin(body);
                if (res.IsSuccessStatusCode)
                {
                    var content = await res.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<FasahLoginResponse>(content);
                    if (data != null)
                    {
                        IsLoading = true;
                        string otp = OTPHelper.Generate();
                        Preferences.Set("OTPValue", otp);

                        IsOtpValid = true;
                        OTPSentOnThisMobileNumber = AppResources.MobileNumber + " xxxxxxx" + data.lastDigitsMobile;
                        ResendOTPTextColor = (Color)Application.Current.Resources["ResendOTPTextColor"];
                        IsResendCodeEnabled = false;
                        StartOTPTimer();
                        IsOTPView = true;
                        Token = data.token;
                    }
                }
                else if (res.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {

                    var content = await res.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<FasahLoginErrorResponse>(content);
                    if (data != null)
                    {
                        IsShowMsgView = true;
                        MessageTxt = data.message;
                    }
                    else
                    {
                        IsShowMsgView = true;
                        MessageTxt = AppResources.ServerError;
                    }

                }
                IsLoading = false;

                return res;

            }
            catch (Exception)
            {
                return null;
            }
            finally { IsLoading = false; }

        }

    }
}

