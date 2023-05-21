using System;
using System.Net.Http;
using System.Threading.Tasks;
using EGAZT.Helper;
using EGAZT.Models.LoginModels;
using EGAZT.Services.Interface;

namespace EGAZT.Services.Classes
{
    public class UserServices : IUserServices
    {
        public async Task<HttpResponseMessage> CustomLogin(CustomLoginModel body)
        {
            var response = await HttpManager.PostAsync(App.VatCustom + $"User", body, true).ConfigureAwait(false);
            return response;
        }
    }
}
