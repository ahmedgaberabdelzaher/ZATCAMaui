using ZATCAMAUI.Core.AppConfigurations;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Services.Interface;
using ZATCAMAUI.Models.NativeNafath;

namespace ZATCAMAUI.Core.Services.Classes
{
    public class NativeNafathServices : INativeNafath
    {
        static string version = "v1";

        public async Task<HttpResponseMessage> SubmitNafath(string IqamaId)
        {

            var body = new { id= IqamaId};
            var response = await HttpManager.PostAsync($"{PageSettings.ZATCABaseURL}{version}/nafath/ZAKATY", body).ConfigureAwait(false);
            return response;
        }
  
        public async Task<HttpResponseMessage> GetNafathStatus(string IqamaId, string transactionId, int randomNumber)
        {
            var body = new { id = IqamaId, transactionId = transactionId, randomNumber = randomNumber };
            var response = await HttpManager.PostAsync($"{PageSettings.ZATCABaseURL}{version}/nafath/ZAKATY/status", body).ConfigureAwait(false);
            return response;
        }
        public async Task<Tuple<CustomsIamUserResponse, bool, string>> GetNfathProfile(string BDHjri, string ID)
        {
            //https://test-api.zatca.gov.sa/test/third-party/v1/zatca-portal/iam-user?identity=1130174889&dateOfBirthHijri=1385-03-01
            var response = await HttpManager.GetAsync<CustomsIamUserResponse>($"{PageSettings.ZATCABaseURL}{version}/zatca-portal/iam-user?identity={ID}&dateOfBirthHijri={BDHjri}",true).ConfigureAwait(false);
            return response;
        }

        public async Task<HttpResponseMessage> PremiumResidencyType(PremiumResidencytypeBody model)
        {
             var response = await HttpManager.PostAsync($"{PageSettings.ZATCABaseURL}{version}/nic/individual/premium-residency/type", model).ConfigureAwait(false);
            return response;
        }
    }
}

