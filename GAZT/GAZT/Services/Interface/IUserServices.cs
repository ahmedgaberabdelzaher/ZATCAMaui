using System;
using System.Net.Http;
using System.Threading.Tasks;
using EGAZT.Models.LoginModels;

namespace EGAZT.Services.Interface
{
    public interface IUserServices
    {
        Task<HttpResponseMessage> CustomLogin(CustomLoginModel body);
    }
}
