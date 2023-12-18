
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Services.Interface;
using ZATCAMAUI.Models.CustomServices.BalaghModels;

namespace ZATCAMAUI.Core.Services.Classes
{
    public class BalaghServices : IBalaghServices
    {
        public async Task<HttpResponseMessage> AddNewBalaghTicket(BalaghTicket balaghTicket)
        {
            var response = await HttpManager.PostAsync(App.CustomBaseUrl + $"Balagh/AddBalaghTicket", balaghTicket).ConfigureAwait(false);
            return response;
        }

        public async Task<Tuple<BalaghLocationsResponse, bool, string>> GetCustomPorts()
        {
            var response = await HttpManager.GetAsync<BalaghLocationsResponse>(App.CustomBaseUrl + $"Balagh/GetCustomsPorts", true).ConfigureAwait(false);

            return response;
        }

        public async Task<Tuple<BalaghTypes, bool, string>> GetBalaghTypes()
        {
            var response = await HttpManager.GetAsync<BalaghTypes>(App.CustomBaseUrl + $"Balagh/GetBalaghTypes", true).ConfigureAwait(false);

            return response;

        }
    }
}
