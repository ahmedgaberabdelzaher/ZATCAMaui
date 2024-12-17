using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using ZATCAMAUI.Core.AppConfigurations;
using ZATCAMAUI.Core.Exceptions;

namespace ZATCAMAUI.Core.Helper
{
    public static class HttpManager
    {
        private static readonly Lazy<HttpClient> _client = new Lazy<HttpClient>(() => new HttpClient() { Timeout = new TimeSpan(0, 3, 0) });

        public static HttpClient client => _client.Value;


        private static bool ValidateCertificate(object sender,
                                            X509Certificate certificate,
                                            X509Chain chain,
                                            SslPolicyErrors sslPolicyErrors)
        => false;

        public static async Task<Tuple<T, bool, string>> GetListAsync<T>(string requestUrl) where T : class
        {
            try
            {
                if (NetworkCheck.IsInternet())
                {
                    var client = new System.Net.Http.HttpClient();
                    var response = client.GetAsync(requestUrl).GetAwaiter().GetResult();
                    if (response != null)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var responseJson = await response.Content.ReadAsStringAsync();
                            var JsonObject = JsonConvert.DeserializeObject<T>(responseJson);
                            return Tuple.Create(JsonObject, true, "");
                        }
                        else
                        {
                            return Tuple.Create((T)Activator.CreateInstance(typeof(T)), false, AppResources.ServerError);
                        }
                    }
                    else
                    {
                        return Tuple.Create((T)Activator.CreateInstance(typeof(T)), false, AppResources.ServerErrorOrNoInternetConnection);
                    }

                }
                else
                {
                    return Tuple.Create((T)Activator.CreateInstance(typeof(T)), false, AppResources.ZZInternetConnectionMessage);
                }

            }
            catch (Exception)
            {
                return Tuple.Create((T)Activator.CreateInstance(typeof(T)), false, AppResources.ServerErrorOrNoInternetConnection);
            }

        }

        public static async Task<Tuple<T, bool, string>> GetAsync<T>(string requestUrl, bool isBasicAuth = true, string routPortCode = "99",bool isIBMCLient=false) where T : class
        {
            try
            {
                if (NetworkCheck.IsInternet())
                {
                    if (client.DefaultRequestHeaders.Contains("X-ZATCA-Client-Id"))
                    {

                        client.DefaultRequestHeaders.Remove("X-ZATCA-Client-Id");
                        client.DefaultRequestHeaders.Remove("X-ZATCA-Client-Secret");
                        client.DefaultRequestHeaders.Remove("LanguageCode");
                        client.DefaultRequestHeaders.Remove("routePortCode");

                    }
                    if (client.DefaultRequestHeaders.Contains("X-IBM-Client-Id"))
                    {

                        client.DefaultRequestHeaders.Remove("X-IBM-Client-Id");
                        client.DefaultRequestHeaders.Remove("X-IBM-Client-Secret");
                        client.DefaultRequestHeaders.Remove("LanguageCode");
                        client.DefaultRequestHeaders.Remove("routePortCode");

                    }
                    if (isBasicAuth)
                    {
                        AddBasicAuthToHeader(client);

                    }
                    if (isIBMCLient)
                    {
                        client.DefaultRequestHeaders.Add("X-IBM-Client-Id", PageSettings.GetClientID());
                        client.DefaultRequestHeaders.Add("X-IBM-Client-Secret", PageSettings.GetClientSecret());
                    }
                    else
                    {

                        client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", PageSettings.GetClientID());
                        client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", PageSettings.GetClientSecret());
                    }
                    client.DefaultRequestHeaders.Remove("zatca-apikey");
                    client.DefaultRequestHeaders.Add("zatca-apikey", "z8KEZALrDtrZflr35Sw48cN592YVv2fa1cPeNHTKuTE=");
                    if (App.IsArabic)
                        client.DefaultRequestHeaders.Add("LanguageCode", "ar");
                    else
                        client.DefaultRequestHeaders.Add("LanguageCode", "en");
                    client.DefaultRequestHeaders.Add("routePortCode", routPortCode);
                    var response = await client.GetAsync(requestUrl);
                    if (response != null)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var responseJson = await response.Content.ReadAsStringAsync();
                            var JsonObject = JsonConvert.DeserializeObject<T>(responseJson);
                            return Tuple.Create(JsonObject, true, "");
                        }
                        else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                        {
                            return Tuple.Create((T)Activator.CreateInstance(typeof(T)), true, "400");

                        }
                        else
                        {
                            return Tuple.Create((T)Activator.CreateInstance(typeof(T)), false, AppResources.ServerError);
                        }
                    }
                    else
                    {
                        return Tuple.Create((T)Activator.CreateInstance(typeof(T)), false, AppResources.ServerErrorOrNoInternetConnection);
                    }

                }
                else
                {
                    return Tuple.Create((T)Activator.CreateInstance(typeof(T)), false, AppResources.ZZInternetConnectionMessage);
                }

            }
            catch (System.Exception exp)
            {
                return Tuple.Create((T)Activator.CreateInstance(typeof(T)), false, AppResources.ServerErrorOrNoInternetConnection);
            }

        }

        public static async Task<Tuple<string, bool, string>> GetStringAsync(string requestUrl, bool isBasicAuth = true, string routPortCode = "99")
        {
            try
            {
                if (NetworkCheck.IsInternet())
                {
                    if (client.DefaultRequestHeaders.Contains("X-ZATCA-Client-Id"))
                    {

                        client.DefaultRequestHeaders.Remove("X-ZATCA-Client-Id");
                        client.DefaultRequestHeaders.Remove("X-ZATCA-Client-Secret");
                        client.DefaultRequestHeaders.Remove("LanguageCode");
                        client.DefaultRequestHeaders.Remove("routePortCode");

                    }
                    if (isBasicAuth)
                    {
                        AddBasicAuthToHeader(client);

                    }
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", PageSettings.GetClientID());
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", PageSettings.GetClientSecret());
                    if (App.IsArabic)
                        client.DefaultRequestHeaders.Add("LanguageCode", "ar");
                    else
                        client.DefaultRequestHeaders.Add("LanguageCode", "en");
                    client.DefaultRequestHeaders.Add("routePortCode", routPortCode);
                    var response = await client.GetAsync(requestUrl);
                    if (response != null)
                    {
                        Debug.WriteLine(requestUrl);
                        Debug.WriteLine(response.StatusCode);
                        if (response.IsSuccessStatusCode)
                        {
                            var responseJson = await response.Content.ReadAsStringAsync();
                            return Tuple.Create(responseJson, true, "");
                        }
                        else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                        {
                            return Tuple.Create("", true, "400");

                        }
                        else
                        {
                            return Tuple.Create("", false, AppResources.ServerError);
                        }
                    }
                    else
                    {
                        return Tuple.Create("", false, AppResources.ServerErrorOrNoInternetConnection);
                    }

                }
                else
                {
                    return Tuple.Create("", false, AppResources.ZZInternetConnectionMessage);
                }

            }
            catch (System.Exception exp)
            {
                Debug.WriteLine(requestUrl);
                Debug.WriteLine(exp.StackTrace);


                return Tuple.Create("", false, AppResources.ServerErrorOrNoInternetConnection);
            }

        }


        private static void AddBasicAuthToHeader(HttpClient client, bool isEradQr = false)
        {
            if (isEradQr)
            {

                var authData = string.Format("{0}:{1}", "T2_USER", "T2user@123");
                var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
            }
            else
            {
                var authData = string.Format("{0}:{1}",ZATCAConstants.CustomUserNameAuthorization,ZATCAConstants.CustomPasswordAuthorization);
                var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
            }
        }

        private static void SetFasahHeaders(HttpClient client, string token)
        {
            client.DefaultRequestHeaders.Add("Referer", PageSettings.GetFasahRefere());
            client.DefaultRequestHeaders.Add("X-Service-Type", "CAR_IMP_SERVICE");
            client.DefaultRequestHeaders.Add("api-key", PageSettings.FasahApiKey);
            client.DefaultRequestHeaders.Add("token", token);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        static string jobject;

        public static async Task<HttpResponseMessage> PostAsync<T>(string requestUrl, T Data, bool isTahqaq = false, string token = "", bool isEradQr = false) where T : class
        {
            try
            {
                if (NetworkCheck.IsInternet())
                {
                    HttpClientHandler clientHandler = new HttpClientHandler();
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    // Pass the handler to httpclient(from you are calling api)
                    HttpClient client = new HttpClient(clientHandler);
                    client.DefaultRequestHeaders.Add("zatca-apikey", "z8KEZALrDtrZflr35Sw48cN592YVv2fa1cPeNHTKuTE=");
                    client.DefaultRequestHeaders.Add("LanguageCode", App.IsArabic ? "ar" : "en");
                    if (token != "")
                    {
                        client.DefaultRequestHeaders.Add("Authorization", token);
                    }
                    if (isTahqaq)
                    {
                        client.DefaultRequestHeaders.Add("client_id", "3d37d7dd9089b57f32820869df3d160f");
                        client.DefaultRequestHeaders.Add("Client_Secret", "b762be58b8803813ef0b36254a1a14df");
                    }
                    else
                    {
                        client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", PageSettings.GetClientID());
                        client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", PageSettings.GetClientSecret());
                    }
                    AddBasicAuthToHeader(client, isEradQr);

                    var JsonObject = JsonConvert.SerializeObject(Data);

                    var content = new StringContent(JsonObject, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(requestUrl, content).ConfigureAwait(false);

                    if (response != null)
                    {

                        if (response.IsSuccessStatusCode)
                        {
                            var responseJson = await response.Content.ReadAsStringAsync();
                            return response;
                        }
                        else
                        {
                            return response;
                        }
                    }
                    else
                    {
                        return new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.BadRequest, ReasonPhrase = AppResources.ServerErrorOrNoInternetConnection };
                    }

                }
                else
                {
                    return new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.BadRequest, ReasonPhrase = AppResources.ZZInternetConnectionMessage };
                }

            }
            catch (Exception)
            {
                return new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.BadRequest, ReasonPhrase = AppResources.ServerErrorOrNoInternetConnection };
            }

        }

        public static async Task<HttpResponseMessage> FasahPostAsync<T>(string requestUrl, T Data, bool isTahqaq = false, string token = "", bool isEradQr = false, bool isFasahHeaders = false) where T : class
        {
            try
            {
                if (NetworkCheck.IsInternet())
                {
                    HttpClientHandler clientHandler = new HttpClientHandler();
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                    // Pass the handler to httpclient(from you are calling api)
                    HttpClient client = new HttpClient(clientHandler);
                    client.DefaultRequestHeaders.Add("Accept-Language", App.IsArabic ? "ar" : "en");
                    if (isFasahHeaders)
                    {
                        SetFasahHeaders(client, token);
                    }
                    var JsonObject = JsonConvert.SerializeObject(Data);
                    var content = new StringContent(JsonObject, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(requestUrl, content).ConfigureAwait(false);
                    if (response != null)
                    {

                        if (response.IsSuccessStatusCode)
                        {
                            var responseJson = await response.Content.ReadAsStringAsync();
                            return response;
                        }
                        else
                        {
                            return response;
                        }
                    }
                    else
                    {
                        return new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.BadRequest, ReasonPhrase = AppResources.ServerErrorOrNoInternetConnection };
                    }

                }
                else
                {
                    return new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.BadRequest, ReasonPhrase = AppResources.ZZInternetConnectionMessage };
                }

            }
            catch (Exception)
            {
                return new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.BadRequest, ReasonPhrase = AppResources.ServerErrorOrNoInternetConnection };
            }

        }

        public static async Task<HttpResponseMessage> PutAsync<T>(string requestUrl, T Data) where T : class
        {
            try
            {
                if (NetworkCheck.IsInternet())
                {
                    var client = new HttpClient();
                    var JsonObject = JsonConvert.SerializeObject(Data);
                    var content = new StringContent(JsonObject, Encoding.UTF8, "application/json");
                    var response = await client.PutAsync(requestUrl, content);
                    if (response != null)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var responseJson = await response.Content.ReadAsStringAsync();
                            return response;
                        }
                        else
                        {
                            return new HttpResponseMessage() { StatusCode = response.StatusCode, ReasonPhrase = AppResources.ServerError };
                        }
                    }
                    else
                    {
                        return new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.BadRequest, ReasonPhrase = AppResources.ServerErrorOrNoInternetConnection };
                    }

                }
                else
                {
                    return new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.BadRequest, ReasonPhrase = AppResources.ZZInternetConnectionMessage };
                }

            }
            catch (Exception)
            {
                return new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.BadRequest, ReasonPhrase = AppResources.ServerErrorOrNoInternetConnection };
            }

        }


        public static async Task<TR> PostAsync<T, TR>(string uri, T data)
        {
            JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore,
            };


            HttpClient httpClient = CreateHttpClient();
            var jsonData = JsonConvert.SerializeObject(data);
            System.Diagnostics.Debug.WriteLine("Request Body" + jsonData);
            var content = new StringContent(jsonData);
            var sValueContent = content.ReadAsStringAsync();

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PostAsync(uri, content);

            await HandleResponse(response);
            string serialized = await response.Content.ReadAsStringAsync();



            string member = JsonConvert.DeserializeObject(serialized).ToString();
            TR result = await Task.Run(() =>

             JsonConvert.DeserializeObject<TR>(member, _jsonSerializerSettings));
            return result;


        }

        private static HttpClient CreateHttpClient()
        {
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("X-Session-Language", "EN");

            client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
            client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);


            return client;
        }

        private static async Task HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Forbidden ||
                    response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(content);
                }

                throw new HttpRequestExceptionEx(response.StatusCode, content);
            }
        }

    }
}