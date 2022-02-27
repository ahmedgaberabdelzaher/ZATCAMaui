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
            client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

            if (istoken)
            {
                client.DefaultRequestHeaders.Add("Token", token);
            }

            var uri = new Uri(URL);
            HttpResponseMessage response = await client.GetAsync(uri);
            return response;
        }

        public static async Task<HttpResponseMessage> MakeGetAPICallForZakatForm5Response(String URL, bool istoken, string token)
        {
            try
            {
                HttpClient client = new HttpClient(App.httpClientHandler);
                client.Timeout = TimeSpan.FromMilliseconds(250000);
                client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

                if (istoken)
                {
                    client.DefaultRequestHeaders.Add("Token", token);
                }

                var uri = new Uri(URL);
                HttpResponseMessage response = await client.GetAsync(uri);
                return response;
            }
            catch (TimeoutException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static async Task<HttpResponseMessage> MakeGetAPICallWithIncomingChannel(String URL, bool istoken, string token)
        {
            HttpClient client = new HttpClient(App.httpClientHandler);
            if (istoken)
            {
                client.DefaultRequestHeaders.Add("Token", token);
            }
            client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

            var uri = new Uri(URL);
            HttpResponseMessage response = await client.GetAsync(uri);
            return response;
        }
    }
}
