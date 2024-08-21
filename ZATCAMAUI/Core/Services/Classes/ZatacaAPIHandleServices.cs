using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Services.Interfac;
using ZATCAMAUI.Models.ForgotModel;

namespace ZATCAMAUI.Core.Services.Classes
{
	public class ZatacaAPIHandleServices : IZatacaAPIHAndle
    {

        public async Task<CaptchaResponse> GetCaptcha(CAptchRequest cAptchRequest)
        {
            return await HttpManager.PostAsync<CAptchRequest, CaptchaResponse>("https://test-api.zatca.gov.sa/test/third-party/v1/captcha", cAptchRequest);
        }

        public async Task<OTPResponse> GetOTP(OTPRequest oTPRequest)
        {
            return await HttpManager.PostAsync<OTPRequest, OTPResponse>("https://test-api.zatca.gov.sa/test/third-party/v1/passwords/forgot-password/otp", oTPRequest);
        }




        public async Task<PasswordChangeResponse> ChangePassword(PasswordChangeRequest passwordChangeRequest)
        {
            return await HttpManager.PostAsync<PasswordChangeRequest, PasswordChangeResponse>("https://test-api.zatca.gov.sa/test/third-party/v1/passwords/forgot-password/password-changing", passwordChangeRequest);
        }

        public async Task<ChangeUsernameResponse> changeUsername(ChangeUsernameRequest changeUsernameRequest)
        {
            return await HttpManager.PostAsync<ChangeUsernameRequest, ChangeUsernameResponse>("https://test-api.zatca.gov.sa/test/third-party/v1/passwords/forgot-password/username-sending", changeUsernameRequest);
        }

        public async Task<ValidateOTPREsponse> ValidateOTP(VAliadteOTP validateOTP)
        {
            return await HttpManager.PostAsync<VAliadteOTP, ValidateOTPREsponse>("https://test-api.zatca.gov.sa/test/third-party/v1/passwords/forgot-password/otp/verification", validateOTP);
        }

    }
}

