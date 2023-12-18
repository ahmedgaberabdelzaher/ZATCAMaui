using ZATCAMAUI.Core.AppConfigurations;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Services.Interface;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.CustomServices;

namespace ZATCAMAUI.Core.Services.Classes
{
    public class CommonServices : ICommonServices
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
        public async Task<Tuple<CustomPortsModel, bool, string>> GetCustomPorts(int portType)
        {
            var response = await HttpManager.GetAsync<CustomPortsModel>(App.CustomBaseUrl + $"Common/GetCustomPort?porttype={portType}", true).ConfigureAwait(false);

            return response;
        }

        public async Task<HttpResponseMessage> ZATCAUserRegister(ZATCAUserRegisterModel user)
        {
            var response = await HttpManager.PostAsync<ZATCAUserRegisterModel>($"{PageSettings.ZATCABaseURL}v1/zatca-portal/user-register", user).ConfigureAwait(false);
            return response;
        }

    }
}
