using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Models.Form5Models;
using static ZATCAMAUI.Models.ErrorMessage;
namespace ZATCAMAUI.Core.Mangers
{

    public static class ZakatForm5WebServiceManager
    {
        #region ZakatForm5
        #region Basic and Financial Information
        public static async Task<ZakatForm5DataResult> GAZTZakatForm5Data(string Fbguid)
        {
            if (NetworkCheck.IsInternet())
            {
                ZakatForm5DataResult ZakatForm5DataResultSet = new ZakatForm5DataResult();
                string NewToken = string.Empty;
                try

                {
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var lang = UtilityManager.GetLanguageParameter();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    string url = ZATCAConstants.Z_RET_F05_ZKTE + Fbguid + "&taxpayerNumber=" + App.TP.TIN + "&authenticationUser=" + App.TP.TIN + "&language=" + lang + "&objectionSubmit=X";
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTZakatForm5Response = await client.GetAsync(uri);
                    if (GAZTZakatForm5Response != null)
                    {
                        if (GAZTZakatForm5Response.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTZakatForm5Response.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                            App.IsSessionExpired = false;
                        }
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }

                        string GAZTZakatForm5ResponseJSON = GAZTZakatForm5Response.Content.ReadAsStringAsync().Result;
                        ErrorObj statusHeader = JsonConvert.DeserializeObject<ErrorObj>(GAZTZakatForm5ResponseJSON);
                        if (statusHeader?.header?.status?.code != "E999999")
                        {
                            if (!string.IsNullOrEmpty(GAZTZakatForm5ResponseJSON))
                            {
                                try
                                {
                                    GAZTZakatForm5ResponseJSON = JObject.Parse(GAZTZakatForm5ResponseJSON)["data"].ToString();
                                    ZakatForm5DataResultSet = JsonConvert.DeserializeObject<ZakatForm5DataResult>(GAZTZakatForm5ResponseJSON);
                                    if (ZakatForm5DataResultSet == null)
                                    {
                                        throw new GAZTVATRegistrationInProcessException(AppResources.NoTINsAvailable);
                                    }
                                }
                                catch (Exception)
                                {
                                    throw new GAZTVATRegistrationInProcessException(AppResources.ZZSomethingwentwrong);
                                }
                            }
                            else
                            {
                                throw new GAZTVATRegistrationInProcessException(AppResources.ZNoICRAvailable);
                            }
                        }
                        else
                        {
                            throw new GAZTNetworkConnectivityIssueException();
                        }

                    }
                    return ZakatForm5DataResultSet;
                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
                }
                catch (HttpRequestException)
                {
                    throw new GAZTNetworkConnectivityIssueException();
                }
                catch (GAZTNetworkConnectivityIssueException)
                {
                    throw new GAZTNetworkConnectivityIssueException();
                }
                catch (Exception)
                {
                    throw new GAZTNetworkConnectivityIssueException();
                }
            }
            else
            {
                throw new InternetException();
            }
        }
        #endregion
        #region City
        public static async Task<ZakatForm5CityDataResult> GAZTZakatForm5CityData()
        {
            if (NetworkCheck.IsInternet())
            {
                ZakatForm5CityDataResult ZakatForm5CityDataResultSet = new ZakatForm5CityDataResult();
                string NewToken = string.Empty;
                try
                {
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var lang = UtilityManager.GetLanguageParameter();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    string url = ZATCAConstants.Z_RET_F05_City + "&language=" + lang + "&country=SA";

                    var uri = new Uri(url);
                    HttpResponseMessage GAZTZakatForm5CityResponse = await client.GetAsync(uri);
                    if (GAZTZakatForm5CityResponse != null)
                    {
                        if (GAZTZakatForm5CityResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTZakatForm5CityResponse.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                            App.IsSessionExpired = false;
                        }
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }

                        string GAZTZakatForm5ResponseJSON = GAZTZakatForm5CityResponse.Content.ReadAsStringAsync().Result;
                        ErrorObj statusHeader = JsonConvert.DeserializeObject<ErrorObj>(GAZTZakatForm5ResponseJSON);
                        if (statusHeader?.header?.status?.code != "E999999")
                        {
                            if (!string.IsNullOrEmpty(GAZTZakatForm5ResponseJSON))
                            {
                                try
                                {
                                    GAZTZakatForm5ResponseJSON = JObject.Parse(GAZTZakatForm5ResponseJSON)["data"].ToString();

                                    ZakatForm5CityDataResultSet = JsonConvert.DeserializeObject<ZakatForm5CityDataResult>(GAZTZakatForm5ResponseJSON);
                                    if (ZakatForm5CityDataResultSet == null)
                                    {
                                        throw new GAZTVATRegistrationInProcessException(AppResources.NoTINsAvailable);
                                    }
                                }
                                catch (Exception)
                                {
                                    throw new GAZTVATRegistrationInProcessException(AppResources.ZZSomethingwentwrong);
                                }

                            }
                            else
                            {
                                throw new GAZTVATRegistrationInProcessException(AppResources.ZNoICRAvailable);
                            }
                        }
                        else
                        {
                            throw new GAZTNetworkConnectivityIssueException();
                        }


                    }
                    return ZakatForm5CityDataResultSet;
                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
                }
                catch (HttpRequestException)
                {
                    throw new GAZTNetworkConnectivityIssueException();
                }
                catch (GAZTNetworkConnectivityIssueException)
                {
                    throw new GAZTNetworkConnectivityIssueException();
                }
                catch (Exception)
                {
                    throw new GAZTNetworkConnectivityIssueException();
                }
            }
            else
            {
                throw new InternetException();
            }
        }
        #endregion

        #region Zakat Estimation Summary
        public static async Task<ZakatForm5SummaryResult> GAZTZakatForm5DataSummary(string Fbnum)
        {
            if (NetworkCheck.IsInternet())
            {
                ZakatForm5SummaryResult ZakatForm5SummaryResultSet = new ZakatForm5SummaryResult();
                string NewToken = string.Empty;
                try
                {
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var lang = UtilityManager.GetLanguageParameter();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);

                    string url = ZATCAConstants.Z_ZKTE_SUMMARY + "&formBundleNumber=" + Fbnum + "&flag=X";
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTZakatForm5SummaryResponse = await client.GetAsync(uri);
                    if (GAZTZakatForm5SummaryResponse != null)
                    {
                        if (GAZTZakatForm5SummaryResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTZakatForm5SummaryResponse.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                            App.IsSessionExpired = false;
                        }
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }

                        string GAZTZakatForm5SummaryResponseJSON = GAZTZakatForm5SummaryResponse.Content.ReadAsStringAsync().Result;

                        ErrorObj statusHeader = JsonConvert.DeserializeObject<ErrorObj>(GAZTZakatForm5SummaryResponseJSON);
                        if (statusHeader?.header?.status?.code != "E999999")
                        {
                            if (!string.IsNullOrEmpty(GAZTZakatForm5SummaryResponseJSON))
                            {
                                GAZTZakatForm5SummaryResponseJSON = JObject.Parse(GAZTZakatForm5SummaryResponseJSON)["data"].ToString();

                                ZakatForm5SummaryResultSet = JsonConvert.DeserializeObject<ZakatForm5SummaryResult>(GAZTZakatForm5SummaryResponseJSON);
                                if (ZakatForm5SummaryResultSet == null)
                                {
                                    throw new GAZTVATRegistrationInProcessException(AppResources.NoTINsAvailable);
                                }
                            }
                            else
                            {
                                throw new GAZTVATRegistrationInProcessException(AppResources.ZNoICRAvailable);
                            }
                        }
                        else
                        {
                            throw new GAZTNetworkConnectivityIssueException();
                        }

                    }
                    return ZakatForm5SummaryResultSet;
                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
                }
                catch (HttpRequestException)
                {
                    throw new GAZTNetworkConnectivityIssueException();
                }
                catch (GAZTNetworkConnectivityIssueException)
                {
                    throw new GAZTNetworkConnectivityIssueException();
                }
                catch (Exception)
                {
                    throw new GAZTNetworkConnectivityIssueException();
                }
            }
            else
            {
                throw new InternetException();
            }
        }
        #endregion
        #endregion
    }
}
