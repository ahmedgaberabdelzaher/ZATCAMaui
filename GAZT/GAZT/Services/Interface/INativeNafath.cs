using System;
using System.Net.Http;
using System.Threading.Tasks;
using EGAZT.Models.NativeNafath;

namespace EGAZT.Services.Interface
{
	public interface INativeNafath
	{
        Task<HttpResponseMessage> SubmitNafath(string IqamaId);
        Task<HttpResponseMessage> GetNafathStatus(string IqamaId, string transactionId, int randomNumber);
        Task<Tuple<CustomsNafathUserProfileResponse, bool, string>> GetNfathProfile(string BDHjri, string ID);

    }
}

