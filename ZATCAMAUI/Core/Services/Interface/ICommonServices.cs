using ZATCAMAUI.Models;
using ZATCAMAUI.Models.CustomServices;

namespace ZATCAMAUI.Core.Services.Interface
{
    public interface ICommonServices
    {
        Task<Tuple<SMSResponse, bool, string>> SendOtpSms(string mobileNo, string Msg);
        Task<Tuple<CustomPortsModel, bool, string>> GetCustomPorts(bool isContainOther = false);
        Task<Tuple<CustomPortsModel, bool, string>> GetCustomPorts(int portType);
        Task<HttpResponseMessage> ZATCAUserRegister(ZATCAUserRegisterModel user);

    }
}
