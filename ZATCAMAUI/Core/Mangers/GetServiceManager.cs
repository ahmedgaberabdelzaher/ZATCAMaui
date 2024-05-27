
using System.Text;
using ZATCAMAUI.Core.Helper;

namespace ZATCAMAUI.Core.Mangers
{


    public static class GetServiceManager
    {
        public static async Task<HttpResponseMessage> MakeGetAPICall(string URL, bool istoken, string token)
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

        public static async Task<HttpResponseMessage> MakeGetAPICallForZakatForm5Response(string URL, bool istoken, string token)
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
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<HttpResponseMessage> MakeGetAPICallWithIncomingChannel(string URL, bool istoken, string token)
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
        public static async Task<HttpResponseMessage> PostApiCall(String URL, bool istoken, string PayLoad)
        {
            HttpClient client = new HttpClient(App.httpClientHandler);
            client.DefaultRequestHeaders.Add("Token", App.Token);
            client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
            client.DefaultRequestHeaders.Add("X-Requested-With", "X");
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            var uri = new Uri(string.Format(URL));
            //var financeData = JsonConvert.SerializeObject(PayLoad, new JsonSerializerSettings
            //{
            //    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
            //    DateTimeZoneHandling = DateTimeZoneHandling.Utc
            //});
            HttpContent contentPost = new StringContent(PayLoad, Encoding.UTF8, ZATCAConstants.ContentType);
            HttpResponseMessage response = await client.PostAsync(uri, contentPost);
            return response;
        }
    }
}
