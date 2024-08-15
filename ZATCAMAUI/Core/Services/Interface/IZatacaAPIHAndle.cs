using System;
using System.Threading.Tasks;

namespace ZATCAMAUI.Core.Services.Interfac;

public interface IZatacaAPIHAndle
{
    Task<CaptchaResponse> GetCaptcha(CAptchRequest cAptchRequest);

    Task<OTPResponse> GetOTP(OTPRequest oTPRequest);

    Task<ValidateOTPREsponse> ValidateOTP(VAliadteOTP validateOTP);

    Task<PasswordChangeResponse> ChangePassword(PasswordChangeRequest passwordChangeRequest);

    Task<ChangeUsernameResponse> changeUsername(ChangeUsernameRequest changeUsernameRequest);
}


