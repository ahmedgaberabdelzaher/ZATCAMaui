using EGAZT.AppConfigurations;
using EGAZT.Helper;
using System.Net.Http;
using System.Threading.Tasks;
using EGAZT.Services.Interface;

namespace EGAZT.Services.Classes
{
    public class NativeNafathServices : INativeNafath
    {
        static string version = "v1";

        public async Task<HttpResponseMessage> SubmitNafath(string IqamaId)
        {
            var body = new { id= IqamaId};
            var response = await HttpManager.PostAsync($"{PageSettings.ZATCABaseURL}{version}/nafath/ZATKA", body).ConfigureAwait(false);
            return response;
        }

        public async Task<HttpResponseMessage> GetNafathStatus(string IqamaId, string transactionId, int randomNumber)
        {
            var body = new { id = IqamaId, transactionId = transactionId, randomNumber = randomNumber };
            var response = await HttpManager.PostAsync($"{PageSettings.ZATCABaseURL}{version}/nafath/ZATKA/status", body).ConfigureAwait(false);
            return response;
        }
    }
}

