using System;
using System.Net.Http;
using System.Threading.Tasks;
using EGAZT.AppConfigurations;
using EGAZT.Helper;
using EGAZT.Models;
using EGAZT.Models.CustomServices;
using EGAZT.Models.EDeclerationsModel.SubmitModels;
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
        public async Task<Tuple<CustomPortsModel, bool, string>> GetCustomPorts(int portType )
        {
            var response = await HttpManager.GetAsync<CustomPortsModel>(App.CustomBaseUrl + $"Common/GetCustomPort?porttype={portType}", true).ConfigureAwait(false);

            return response;
        }

        public async Task<HttpResponseMessage> ZATCAUserRegister(ZATCAUserRegisterModel user)
        {
            var response = await HttpManager.PostAsync<ZATCAUserRegisterModel>($"{PageSettings.ZATCABaseURL}v1/zatca-portal/user-register",user).ConfigureAwait(false);
            return response;
        }

    }
}
