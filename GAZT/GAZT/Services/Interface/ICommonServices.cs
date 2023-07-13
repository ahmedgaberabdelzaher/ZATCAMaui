using System;
using System.Net.Http;
using System.Threading.Tasks;
using EGAZT.Models;
using EGAZT.Models.CustomServices;

namespace EGAZT.Services.Interface
{
    public interface ICommonServices
    {
        Task<Tuple<SMSResponse, bool, string>> SendOtpSms(string mobileNo,string Msg);
        Task<Tuple<CustomPortsModel, bool, string>> GetCustomPorts(bool isContainOther = false);
        Task<Tuple<CustomPortsModel, bool, string>> GetCustomPorts(int portType);
        Task<HttpResponseMessage> ZATCAUserRegister(ZATCAUserRegisterModel user);

    }
}
