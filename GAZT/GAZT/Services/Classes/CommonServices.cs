using System;
using System.Threading.Tasks;
using EGAZT.Helper;
using EGAZT.Models.CustomServices;
using EGAZT.Services.Interface;

namespace EGAZT.Services.Classes
{
    public class CommonServices: ICommonServices
    {
        

        public async Task<Tuple<SMSResponse, bool, string>> SendOtpSms(string mobileNo, string Msg)
        {
            var response = await HttpManager.GetAsync<SMSResponse>(App.CustomBaseUrl + $"Common/SendSMS/{mobileNo}/{Msg}", true).ConfigureAwait(false);

            return response;
        }

        public async Task<Tuple<CustomPortsModel, bool, string>> GetCustomPorts(bool isContainOther = false)
        {
            var response = await HttpManager.GetAsync<CustomPortsModel>(App.CustomBaseUrl + $"Common/GetCustomPort?Other={isContainOther}", true).ConfigureAwait(false);

            return response;
        }

    }
}
