using System;
using EGAZT.AppConfigurations;
using EGAZT.Helper;
using EGAZT.Models.SurveyModels;
using System.Net.Http;
using System.Threading.Tasks;
using EGAZT.Services.Interface;
using EGAZT.Models.CustomServices.Tawreed;

namespace EGAZT.Services.Classes
{
    public class TwareedServices: ITwareedServices
    {
        public async Task<HttpResponseMessage> TawreedSubmitForm(TawreedSubmitFormModel model)
        {
            var response = await HttpManager.PostAsync($"{PageSettings.ZATCABaseURL}v2/portal/twareed/submit-form", model, false).ConfigureAwait(false);
            return response;
        }
    }
}

