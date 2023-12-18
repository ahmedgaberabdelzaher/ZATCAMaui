using ZATCAMAUI.Models.CustomServices.FasahModels;
using ZATCAMAUI.Models.LoginModels;

namespace ZATCAMAUI.Core.Services.Interface
{
    public interface IUserServices
    {
        Task<HttpResponseMessage> CustomLogin(CustomLoginModel body);
        Task<HttpResponseMessage> FasahLogin(FasahLoginBody body);
        Task<HttpResponseMessage> VerifyFasahLoginOtp(VerifyLoginBody body, string fasahLoginHeader);
        Task<HttpResponseMessage> ResendOtp(ResendOtpBody body, string fasahLoginHeader);
    }
}
