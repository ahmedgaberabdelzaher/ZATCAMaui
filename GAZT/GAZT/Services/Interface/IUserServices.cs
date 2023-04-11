using System;
using System.Net.Http;
using System.Threading.Tasks;
using EGAZT.Helper;
using EGAZT.Models.CustomServices.FasahModels;
using EGAZT.Models.LoginModels;

namespace EGAZT.Services.Interface
{
    public interface IUserServices
    {
        Task<HttpResponseMessage> CustomLogin(CustomLoginModel body);
        Task<HttpResponseMessage> FasahLogin(FasahLoginBody body);
        Task<HttpResponseMessage> VerifyFasahLoginOtp(VerifyLoginBody body, string fasahLoginHeader);
        Task<HttpResponseMessage> ResendOtp(ResendOtpBody body, string fasahLoginHeader);
    }
}
