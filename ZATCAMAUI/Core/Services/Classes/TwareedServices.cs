using System.Collections.ObjectModel;
using ZATCAMAUI.Core.AppConfigurations;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Services.Interface;
using ZATCAMAUI.Models.BaseModels;
using ZATCAMAUI.Models.CustomServices.Tawreed;

namespace ZATCAMAUI.Core.Services.Classes
{
    public class TwareedServices : ITwareedServices
    {
        static string version = "v3";
        public async Task<HttpResponseMessage> TawreedSubmitForm(TawreedSubmitFormModel model)
        {
            var response = await HttpManager.PostAsync($"{PageSettings.ZATCABaseURL}{version}/portal/twareed/submit-form", model, false).ConfigureAwait(false);
            return response;
        }

        public async Task<HttpResponseMessage> TawreedAddNewCr(AddNewCrBody model)
        {
            var response = await HttpManager.PostAsync($"{PageSettings.ZATCABaseURL}{version}/portal/twareed/submit-cr", model, false).ConfigureAwait(false);
            return response;
        }

        public async Task<Tuple<DATAPowerBaseResponse<ObservableCollection<UserCRResponseModel>>, bool, string>> GetUserCRs(int UserId)
        {
            var response = await HttpManager.GetAsync<DATAPowerBaseResponse<ObservableCollection<UserCRResponseModel>>>($"{PageSettings.ZATCABaseURL}{version}/portal/twareed/cr-details?userID={UserId}").ConfigureAwait(false);
            return response;
        }
        public async Task<Tuple<DATAPowerBaseResponse<CrTinNoModel>, bool, string>> GetCurrentCRTiNo(string CRNo)
        {
              var response = await HttpManager.GetAsync<DATAPowerBaseResponse<CrTinNoModel>>($"{PageSettings.ZATCABaseURL}/v2/dwh/taxpayers?idType=CRNumber&idNumber={CRNo}",false,"99",false).ConfigureAwait(false);
             return response;
        }
    }
}

