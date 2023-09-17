using System;
using EGAZT.AppConfigurations;
using EGAZT.Helper;
using EGAZT.Models.SurveyModels;
using System.Net.Http;
using System.Threading.Tasks;
using EGAZT.Services.Interface;
using EGAZT.Models.CustomServices.Tawreed;
using EGAZT.Models.BaseModels;
using EGAZT.Models.EDeclerationsModel;
using System.Collections.ObjectModel;

namespace EGAZT.Services.Classes
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
            var response = await HttpManager.GetAsync<DATAPowerBaseResponse<CrTinNoModel>>($"{PageSettings.ZATCABaseURL}/v1/erad/real-estate/enterprise/?IDType=CommercialRegistrationNumber&IDNumber={CRNo}",false,"99",true).ConfigureAwait(false);
            return response;
        }
    }
}

