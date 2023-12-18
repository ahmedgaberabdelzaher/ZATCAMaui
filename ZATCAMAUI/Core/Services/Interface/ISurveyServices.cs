using ZATCAMAUI.Models.SurveyModels;

namespace ZATCAMAUI.Core.Services.Interface
{
    public interface ISurveyServices
    {
        Task<Tuple<SurveyByDateResponse, bool, string>> GetSurveyByDate(string TIN, string Date);
        Task<HttpResponseMessage> AddSurveyData(AddSurveyBody addSurveyBody);
        Task<HttpResponseMessage> AddSurveyAnswerToVoc(VocAddSurveyAnswerModel model, string token);

    }
}
