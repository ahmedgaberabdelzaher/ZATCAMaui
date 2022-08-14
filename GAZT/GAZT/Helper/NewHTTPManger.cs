using Acr.UserDialogs;
using EGAZT.Models.SubmitReportModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EGAZT.Helper
{
    public static class NewHTTPManger
    {
        public static async Task<object> Get<T>(string Url)
        {
            try
            {
                if (NetworkCheck.IsInternet())
                {

                    using (var client = new HttpClient())
                    {
                        AdjustHeaders(client);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, Url);

                        using (var httpResponseMessage = await client.SendAsync(httpRequestMessage))
                        {

                            var response = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

                            if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized ||
                                httpResponseMessage.StatusCode == HttpStatusCode.Forbidden)
                            {
                                await UserDialogs.Instance.AlertAsync("بريد الكتروني أو كلمة مرور غير صحيحة برجاء اعادة المحاولة", "خطأ", "حسنا");
                                return null;
                            }

                            else if (response == null)
                            {
                                await UserDialogs.Instance.AlertAsync(AppResources.ServerError);
                                return null;
                            }

                            var result = DeserializeObject<T>(response);

                            if (httpResponseMessage.IsSuccessStatusCode)
                            {
                                client.Dispose();
                                return result;
                            }

                            else
                            {
                                await UserDialogs.Instance.AlertAsync(AppResources.ServerError);
                                return null;
                            }
                        }
                    }

                }
                else
                {
                    UserDialogs.Instance.Toast(AppResources.ServerErrorOrNoInternetConnection, TimeSpan.FromSeconds(1));
                    return null;
                }
            }
            catch (JsonReaderException e)
            {
                await UserDialogs.Instance.AlertAsync(AppResources.ServerErrorOrNoInternetConnection, AppResources.ServerError, AppResources.OKText);
                return null;
            }
            catch (Exception ex)
            {
                await UserDialogs.Instance.AlertAsync(AppResources.ServerErrorOrNoInternetConnection, AppResources.ServerError, AppResources.OKText);
                return null;
            }
        }

        public static async Task<object> Post<T>(string Url, object body = null)
        {
            try
            {
                if (NetworkCheck.IsInternet())
                {

                    using (var client = new HttpClient())
                    {

                        AdjustHeaders(client);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, Url);

                        httpRequestMessage.Content = new StringContent(CheckNullJsonObject(body), Encoding.UTF8);

                        using (var httpResponseMessage = await client.SendAsync(httpRequestMessage))
                        {

                            var response = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

                            if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized ||
                                httpResponseMessage.StatusCode == HttpStatusCode.Forbidden)
                            {
                                await UserDialogs.Instance.AlertAsync("بريد الكتروني أو كلمة مرور غير صحيحة برجاء اعادة المحاولة", "خطأ", "حسنا");
                                return null;
                            }

                            else if (response == null)
                            {
                                await UserDialogs.Instance.AlertAsync(AppResources.ServerError);
                                return null;
                            }

                            var result = DeserializeObject<T>(response);

                            if (httpResponseMessage.IsSuccessStatusCode)
                            {
                                client.Dispose();
                                return result;
                            }

                            else
                            {
                                await UserDialogs.Instance.AlertAsync(AppResources.ServerError);
                                return null;
                            }

                        }
                    }

                }
                else
                {
                    UserDialogs.Instance.Toast(AppResources.ServerErrorOrNoInternetConnection, TimeSpan.FromSeconds(1));
                    return null;
                }
            }
            catch (JsonReaderException e)
            {
                await UserDialogs.Instance.AlertAsync(AppResources.ServerErrorOrNoInternetConnection, AppResources.ServerError, AppResources.OKText);
                return null;
            }
            catch (Exception ex)
            {
                await UserDialogs.Instance.AlertAsync(AppResources.ServerErrorOrNoInternetConnection, AppResources.ServerError, AppResources.OKText);
                return null;
            }
        }

        public static async Task<object> PostFile<T>(string Url, Dictionary<string, string> body, ObservableCollection<ReportFileModel> files)
        {
            try
            {
                if (NetworkCheck.IsInternet())
                {

                    using (var client = new HttpClient())
                    {

                        AdjustHeaders(client);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, Url);

                        var multipartForm = new MultipartFormDataContent();

                        if (files != null)
                        {
                            foreach (var item in files)
                            {
                                multipartForm.Add(new StreamContent(item.filecontentStream),"Files",item.filename);
                            }
                        }

                        if (body != null)
                        {
                            foreach (var keyValuePair in body)
                            {
                                if (keyValuePair.Value == null || keyValuePair.Key == null)
                                    continue;

                                multipartForm.Add(new StringContent(keyValuePair.Value), keyValuePair.Key);

                            }
                        }

                        httpRequestMessage.Content = multipartForm;

                        using (var httpResponseMessage = await client.SendAsync(httpRequestMessage))
                        {

                            var response = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

                            if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized ||
                                httpResponseMessage.StatusCode == HttpStatusCode.Forbidden)
                            {
                                await UserDialogs.Instance.AlertAsync("بريد الكتروني أو كلمة مرور غير صحيحة برجاء اعادة المحاولة", "خطأ", "حسنا");
                                return null;
                            }

                            else if (response == null)
                            {
                                await UserDialogs.Instance.AlertAsync(AppResources.ServerError);
                                return null;
                            }

                            var result = DeserializeObject<T>(response);

                            if (httpResponseMessage.IsSuccessStatusCode)
                            {
                                client.Dispose();
                                return result;
                            }

                            else
                            {
                                await UserDialogs.Instance.AlertAsync(AppResources.ServerError);
                                return null;
                            }

                        }
                    }

                }
                else
                {
                    UserDialogs.Instance.Toast(AppResources.ServerErrorOrNoInternetConnection, TimeSpan.FromSeconds(1));
                    return null;
                }
            }
            catch (JsonReaderException e)
            {
                await UserDialogs.Instance.AlertAsync(AppResources.ServerErrorOrNoInternetConnection, AppResources.ServerError, AppResources.OKText);
                return null;
            }
            catch (Exception ex)
            {
                await UserDialogs.Instance.AlertAsync(AppResources.ServerErrorOrNoInternetConnection, AppResources.ServerError, AppResources.OKText);
                return null;
            }
        }

        private static string ObjectToKeyValueString(object obj)
        {
            var keyValuePair = string.Empty;

            if (obj == null) return keyValuePair;

            IEnumerable<string> properties = null;


            var result = new List<string>();
            var props = obj.GetType().GetProperties().Where(p => p.GetValue(obj, null) != null);
            foreach (var p in props)
            {
                var value = p.GetValue(obj, null);
                var enumerable = value as ICollection;
                if (enumerable != null && enumerable.Count > 0)
                {
                    result.AddRange(from object v in enumerable
                                    where v != null
                                    select string.Format("{0}={1}", p.Name, HttpUtility.UrlEncode(v.ToString())));
                }
                else if (enumerable != null && enumerable.Count == 0)
                {
                    continue;
                }
                else
                {
                    result.Add(string.Format("{0}={1}", p.Name, HttpUtility.UrlEncode(value.ToString())));
                }
            }
            properties = result;


            keyValuePair = string.Join("&", properties.ToArray());

            return keyValuePair;

        }

        private static void AdjustHeaders(HttpClient client)
        {

            client.DefaultRequestHeaders.Add("LanguageCode", App.IsArabic ? "ar" : "en");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", null);
        }
        private static string CheckNullJsonObject(object obj)
        {
            try
            {
                var serializerSettings = new JsonSerializerSettings();
                serializerSettings.ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };
                return obj == null ? string.Empty : JsonConvert.SerializeObject(obj, serializerSettings);
            }
            catch (Exception)
            {

                return null;
            }
        }

        private static T DeserializeObject<T>(string content)
        {
            try
            {
                var serializerSettings = new JsonSerializerSettings();
                serializerSettings.ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };
                return JsonConvert.DeserializeObject<T>(content, serializerSettings);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
