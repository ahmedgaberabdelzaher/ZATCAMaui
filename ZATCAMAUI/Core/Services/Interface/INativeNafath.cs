
using ZATCAMAUI.Models.NativeNafath;

namespace ZATCAMAUI.Core.Services.Interface
{
    public interface INativeNafath
    {
        Task<HttpResponseMessage> SubmitNafath(string IqamaId);
        Task<HttpResponseMessage> GetNafathStatus(string IqamaId, string transactionId, int randomNumber);
        Task<Tuple<CustomsIamUserResponse, bool, string>> GetNfathProfile(string BDHjri, string ID);
        Task<HttpResponseMessage> PremiumResidencyType(PremiumResidencytypeBody model);
    }
}

