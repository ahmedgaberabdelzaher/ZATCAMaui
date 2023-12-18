using ZATCAMAUI.Models.CustomServices.BalaghModels;

namespace ZATCAMAUI.Core.Services.Interface
{
    public interface IBalaghServices
    {
        Task<Tuple<BalaghLocationsResponse, bool, string>> GetCustomPorts();
        Task<Tuple<BalaghTypes, bool, string>> GetBalaghTypes();
        Task<HttpResponseMessage> AddNewBalaghTicket(BalaghTicket balaghTicket);
    }
}
