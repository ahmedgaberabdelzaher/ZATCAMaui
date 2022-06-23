using System;
using System.Net.Http;
using System.Threading.Tasks;
using EGAZT.Models.CustomServices;
using EGAZT.Models.CustomServices.BalaghModels;

namespace EGAZT.Services.Interface
{
    public interface IBalaghServices
    {
        Task<Tuple<BalaghLocationsResponse, bool, string>> GetCustomPorts();
        Task<Tuple<BalaghTypes, bool, string>> GetBalaghTypes();
        Task<HttpResponseMessage> AddNewBalaghTicket(BalaghTicket balaghTicket);
    }
}
