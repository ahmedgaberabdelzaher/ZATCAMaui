using System;
using EGAZT.Models.CustomServices.Tawreed;
using System.Net.Http;
using System.Threading.Tasks;
using EGAZT.Models.BaseModels;
using System.Collections.ObjectModel;

namespace EGAZT.Services.Interface
{
    public interface ITwareedServices
    {
        Task<HttpResponseMessage> TawreedSubmitForm(TawreedSubmitFormModel model);
        Task<HttpResponseMessage> TawreedAddNewCr(AddNewCrBody model);
        Task<Tuple<DATAPowerBaseResponse<ObservableCollection<UserCRResponseModel>>, bool, string>> GetUserCRs(int UserId);
        Task<Tuple<DATAPowerBaseResponse<CrTinNoModel>, bool, string>> GetCurrentCRTiNo(string CRNo);
    }
}

