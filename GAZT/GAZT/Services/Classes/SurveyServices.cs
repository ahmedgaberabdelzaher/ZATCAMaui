using System;
using System.Net.Http;
using System.Threading.Tasks;
using EGAZT.Helper;
using EGAZT.Models.SurveyModels;
using EGAZT.Services.Interface;

namespace EGAZT.Services.Classes
{
    public class SurveyServices: ISurveyServices
    {
        public async Task<HttpResponseMessage> AddSurveyData(AddSurveyBody addSurveyBody)
        {
            var response = await HttpManager.PostAsync(App.CustomBaseUrl + $"Survey/AddUserSurvey",addSurveyBody, true).ConfigureAwait(false);
            return response;
        }

        public async Task<Tuple<SurveyByDateResponse, bool, string>> GetSurveyByDate(string TIN,string Date)
        {
            var response = await HttpManager.GetAsync<SurveyByDateResponse>(App.CustomBaseUrl + $"Survey/GetSurveyByDate/{TIN}/{Date}", true).ConfigureAwait(false);
            return response;
        }

    }
}
