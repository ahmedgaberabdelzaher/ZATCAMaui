using System.Collections.ObjectModel;
using ZATCAMAUI.Models.BaseModels;
using ZATCAMAUI.Models.CustomServices.Tawreed;

namespace ZATCAMAUI.Core.Services.Interface
{
    public interface ITwareedServices
    {
        Task<HttpResponseMessage> TawreedSubmitForm(TawreedSubmitFormModel model);
        Task<HttpResponseMessage> TawreedAddNewCr(AddNewCrBody model);
        Task<Tuple<DATAPowerBaseResponse<ObservableCollection<UserCRResponseModel>>, bool, string>> GetUserCRs(int UserId);
        Task<Tuple<DATAPowerBaseResponse<CrTinNoModel>, bool, string>> GetCurrentCRTiNo(string CRNo);
    }
}

