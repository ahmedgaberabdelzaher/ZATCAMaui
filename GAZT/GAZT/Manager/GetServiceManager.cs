using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace EGAZT.Manager
{
    [Preserve(AllMembers = true)]
 
    public static class GetServiceManager
    {
        public static async Task<HttpResponseMessage> MakeGetAPICall(String URL,bool istoken,string token)
        {
            HttpClient client = new HttpClient(App.httpClientHandler);
            if (istoken)
            {
                client.DefaultRequestHeaders.Add("Token", token);
            }
            var uri = new Uri(URL);
            HttpResponseMessage response = await client.GetAsync(uri);
            return response;
        }
    }
}
