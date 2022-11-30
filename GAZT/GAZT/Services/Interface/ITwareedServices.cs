using System;
using EGAZT.Models.CustomServices.Tawreed;
using System.Net.Http;
using System.Threading.Tasks;

namespace EGAZT.Services.Interface
{
    public interface ITwareedServices
    {
        Task<HttpResponseMessage> TawreedSubmitForm(TawreedSubmitFormModel model);
    }
}

