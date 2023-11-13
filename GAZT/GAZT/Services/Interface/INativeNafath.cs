using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EGAZT.Services.Interface
{
	public interface INativeNafath
	{
        Task<HttpResponseMessage> SubmitNafath(string IqamaId);
        Task<HttpResponseMessage> GetNafathStatus(string IqamaId, string transactionId, int randomNumber);

    }
}

