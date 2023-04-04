using System;
using System.Net.Http;
using System.Threading.Tasks;
using EGAZT.AppConfigurations;
using EGAZT.Helper;
using EGAZT.Models.CustomServices.FasahModels;
using EGAZT.Models.LoginModels;
using EGAZT.Services.Interface;

namespace EGAZT.Services.Classes
{
    public class UserServices : IUserServices
    {
        public async Task<HttpResponseMessage> CustomLogin(CustomLoginModel body)
        {
            var response = await HttpManager.PostAsync(App.VatCustom + $"User", body, true).ConfigureAwait(false);
            return response;
        }
        public async Task<HttpResponseMessage> FasahLogin(FasahLoginBody body)
        {
            var response = await HttpManager.FasahPostAsync(PageSettings.FasahBaseUrl+ "api/login", body, false,"",false,true).ConfigureAwait(false);
            return response;
        }
        public async Task<HttpResponseMessage> VerifyFasahLoginOtp(VerifyLoginBody body,string fasahLoginHeader)
        {
            var response = await HttpManager.FasahPostAsync(PageSettings.FasahBaseUrl+ "api/user/login/verify", body, false, fasahLoginHeader, false, true).ConfigureAwait(false);
            return response;
        }
        public async Task<HttpResponseMessage> ResendOtp(ResendOtpBody body, string fasahLoginHeader)
        {
            var response = await HttpManager.FasahPostAsync(PageSettings.FasahBaseUrl + "api/user/login/resendToken", body, false, fasahLoginHeader, false, true).ConfigureAwait(false);
            return response;
        }
    }
}
