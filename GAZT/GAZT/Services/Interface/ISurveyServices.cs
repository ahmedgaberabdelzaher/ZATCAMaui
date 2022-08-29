using System;
using System.Net.Http;
using System.Threading.Tasks;
using EGAZT.Models.SurveyModels;

namespace EGAZT.Services.Interface
{
    public interface ISurveyServices
    {
        Task<Tuple<SurveyByDateResponse, bool, string>> GetSurveyByDate(string TIN, string Date);
        Task<HttpResponseMessage> AddSurveyData(AddSurveyBody addSurveyBody);

    }
}
