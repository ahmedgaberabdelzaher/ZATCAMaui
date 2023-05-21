using System;
using System.Net.Http;
using System.Threading.Tasks;
using EGAZT.AppConfigurations;
using EGAZT.Helper;
using EGAZT.Models.SurveyModels;
using EGAZT.Services.Interface;

namespace EGAZT.Services.Classes
{
    public class SurveyServices: ISurveyServices
    {
        public async Task<HttpResponseMessage> AddSurveyAnswerToVoc(VocAddSurveyAnswerModel model,string token)
        {
            var response = await HttpManager.PostAsync($"{PageSettings.ZATCABaseURL}v2/zatca/survey/submit-feedback",model, false).ConfigureAwait(false);
            return response;
        }

        public async Task<HttpResponseMessage> AddSurveyData(AddSurveyBody addSurveyBody)
        {
            var response = await HttpManager.PostAsync(PageSettings.ZATCABaseURL + $"v1/portal/survey/addUserSurvey",addSurveyBody, false).ConfigureAwait(false);
            return response;
        }

        public async Task<Tuple<SurveyByDateResponse, bool, string>> GetSurveyByDate(string TIN,string Date)
        {
            var response = await HttpManager.GetAsync<SurveyByDateResponse>(PageSettings.ZATCABaseURL + $"v1/portal/survey/surveyDate/{TIN}/{Date}", false).ConfigureAwait(false);
            return response;
        }

       

    }
}
