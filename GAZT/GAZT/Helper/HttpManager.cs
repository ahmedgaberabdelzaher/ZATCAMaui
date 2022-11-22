using EGAZT.AppConfigurations;
using GAZT.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EGAZT.Helper
{
    public static class HttpManager
    {
     private static readonly Lazy<HttpClient> _client = new Lazy<HttpClient>(() => new HttpClient() { Timeout = new TimeSpan(0, 3, 0)});

     /*   private static HttpClient getClient()
        {
            HttpClient _client = null;
           _client = new HttpClient();
            return _client
        }*/
      public static HttpClient client => _client.Value;


        private static bool ValidateCertificate(object sender,
                                            X509Certificate certificate,
                                            X509Chain chain,
                                            SslPolicyErrors sslPolicyErrors)
        => false;
    
    public static async Task<Tuple<T,bool ,string >> GetListAsync<T>(string requestUrl) where T : class
        {
            try
            {
                if (NetworkCheck.IsInternet())
                {
                     var client = new System.Net.Http.HttpClient();
                   // var client = App.Locator.httpClient;

                    //  client.DefaultRequestHeaders.Add("Authorization",app.CurrentToken);
                    var response =  client.GetAsync(requestUrl).GetAwaiter().GetResult();
                    if (response!=null)
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
            catch (System.Exception exp)
            {
                return Tuple.Create((T)Activator.CreateInstance(typeof(T)), false, AppResources.ServerErrorOrNoInternetConnection);
            }

        }

        public static async Task<Tuple<T, bool, string>> GetAsync<T>(string requestUrl,bool isBasicAuth=true,string routPortCode="99") where T : class
        {
            try
            {
                if (NetworkCheck.IsInternet())
                {

                    //var client = new System.Net.Http.HttpClient();
                    //  var client = App.Locator.httpClient;

                    //  client.Timeout = new TimeSpan(0,3,0);
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
                    /*if (routPortCode!="99")
                    {
                        routPortCode = "1" + routPortCode;
                    }*/

                    /*  client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", "a867a41eeccbd956b7f279b50d8535a5");
                      client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", "c9487460cd7dd8bc0f16ede707f4dad3");
                    */
                    var response = await client.GetAsync(requestUrl);
                    if (response != null)
                    {
                        Debug.WriteLine(requestUrl);
                        Debug.WriteLine(response.StatusCode);
                        if (response.IsSuccessStatusCode)
                        {
                            var responseJson = await response.Content.ReadAsStringAsync();
                            var JsonObject = JsonConvert.DeserializeObject<T>(responseJson);
                            return Tuple.Create(JsonObject, true, "");
                        }
                        else if (response.StatusCode==System.Net.HttpStatusCode.BadRequest)
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
                Debug.WriteLine(requestUrl);
                Debug.WriteLine(exp.StackTrace);
               

                return Tuple.Create((T)Activator.CreateInstance(typeof(T)), false, AppResources.ServerErrorOrNoInternetConnection);
            }

        }

        private static void AddBasicAuthToHeader(HttpClient client)
        {
            var authData = string.Format("{0}:{1}", Constants.CustomUserNameAuthorization, Constants.CustomPasswordAuthorization);
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        }

        static string jobject;
        public static async Task<HttpResponseMessage> PostAsync<T>(string requestUrl,T Data,bool isTahqaq=false,string token="") where T :  class
        {
            try
            {
                if (NetworkCheck.IsInternet())
                {
                    HttpClientHandler clientHandler = new HttpClientHandler();
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                    // Pass the handler to httpclient(from you are calling api)
                    HttpClient client = new HttpClient(clientHandler);
                   // var client = new System.Net.Http.HttpClient();
                    //var h = new HttpClientHandler();
                    //   h.ServerCertificateCustomValidationCallback = ValidateCertificate;

                    //  var client = App.Locator.httpClient;


                    client.DefaultRequestHeaders.Add("LanguageCode",App.IsArabic?"ar":"en");
                    //var JsonObject = JsonConvert.SerializeObject(Data);
                    // client.DefaultRequestHeaders.Add("routePortCode", routPortCode);
                    /*
                     client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", "a867a41eeccbd956b7f279b50d8535a5");
                     client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", "c9487460cd7dd8bc0f16ede707f4dad3");
                    */
                    if (token!="")
                    {
                     client.DefaultRequestHeaders.Add("Authorization",token);
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
                    AddBasicAuthToHeader(client);

                  var JsonObject = JsonConvert.SerializeObject(Data);
                   // var JsonObject =jobject;

                    var content = new StringContent(JsonObject,Encoding.UTF8, "application/json");
                   // var response = await client.PostAsync(requestUrl, content);
                    var response = await client.PostAsync(requestUrl, content).ConfigureAwait(false) ;
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
            catch (System.Exception exp)
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
                   var client = new System.Net.Http.HttpClient();
                   // var client = App.Locator.httpClient;

                    //   client.DefaultRequestHeaders.Add("Authorization", app.CurrentToken);
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
            catch (System.Exception exp)
            {
                return new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.BadRequest, ReasonPhrase = AppResources.ServerErrorOrNoInternetConnection };
            }

        }

    }
}