using System;
using System.Threading.Tasks;
using EGAZT.Models.CustomServices;

namespace EGAZT.Services.Interface
{
    public interface ICommonServices
    {
        Task<Tuple<SMSResponse, bool, string>> SendOtpSms(string mobileNo,string Msg);
        Task<Tuple<CustomPortsModel, bool, string>> GetCustomPorts(bool isContainOther = false);

    }
}
