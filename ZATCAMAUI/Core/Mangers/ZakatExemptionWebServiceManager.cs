using System.Net;
using System.Net.Http.Headers;
using System.Text;

using Newtonsoft.Json;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using static ZATCAMAUI.Models.ErrorMessage;

namespace ZATCAMAUI.Core.Manager
{
    
    public class ZakatExemptionWebServiceManager
    {
        private static double _timeoutMinutes = 3;

        public static async Task<ZakatExemptionModel> GetRequestToZakatExemtionRequest(ZakatExemptionListModel.FbnumListSetResult fbnumListSet)
        {
            ZakatExemptionModel _requestZakatExemtionReq = new ZakatExemptionModel();

            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var lang = UtilityManager.GetLanguageParameter();
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    client.Timeout = TimeSpan.FromSeconds(60);



                    String url = string.Empty;
                    if (fbnumListSet != null)
                    {
                        url = ZATCAConstants.ZakatExemtionRequestInit + "formBundleType=ZERQ" + "&transactionType=CRE_ZERQ" + "&language=" + lang + "&TIN=" + App.LoginDataRetrieved.TIN + "&formBundleGUID=" + fbnumListSet.Fbguid + "&authenticationUser=" + fbnumListSet.Euser;

                    }
                    else
                    {
                        url = ZATCAConstants.ZakatExemtionRequestInit + "formBundleType=ZERQ" + "&transactionType=CRE_ZERQ" + "&language=" + lang + "&TIN=" + App.LoginDataRetrieved.TIN;

                    }
                    var uri = new Uri(url);
                    HttpResponseMessage _requestZakatExemtionReqResponse = await client.GetAsync(uri);
                    if (_requestZakatExemtionReqResponse != null)
                    {
                        if (_requestZakatExemtionReqResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _requestZakatExemtionReqResponse.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                            App.IsSessionExpired = false;
                        }
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        var detailJson = _requestZakatExemtionReqResponse.Content.ReadAsStringAsync().Result;
                        _requestZakatExemtionReq = JsonConvert.DeserializeObject<ZakatExemptionModel>(detailJson);
                        if (!string.IsNullOrEmpty(detailJson))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }

                    return _requestZakatExemtionReq;

                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
                }

                catch (Exception ex)
                {
                    App.IsSessionExpired = true;
                    return null;
                }

            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        public static async Task<ZakatExemptionListModel> GetZakatExemptionListRequest()
        {
            ZakatExemptionListModel _requestZakatExemtionReq = new ZakatExemptionListModel();

            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var lang = UtilityManager.GetLanguageParameter();
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    client.Timeout = TimeSpan.FromSeconds(60);


                    String url = ZATCAConstants.ZakatExemtionRequestList + "TIN=" + App.LoginDataRetrieved.TIN + "&userType=TP&language="+ lang;

                    var uri = new Uri(url);
                    HttpResponseMessage _requestZakatExemtionReqResponse = await client.GetAsync(uri).ConfigureAwait(false);

                    if (_requestZakatExemtionReqResponse != null)
                    {
                        if (_requestZakatExemtionReqResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true; 
                            return null;
                        }
                        HttpHeaders headers = _requestZakatExemtionReqResponse.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                            App.IsSessionExpired = false;
                        }
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        var detailJson = _requestZakatExemtionReqResponse.Content.ReadAsStringAsync().Result;
                        _requestZakatExemtionReq = JsonConvert.DeserializeObject<ZakatExemptionListModel>(detailJson);
                        if (!string.IsNullOrEmpty(detailJson))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }

                    return _requestZakatExemtionReq;

                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
                }

                catch (Exception)
                {
                    App.IsSessionExpired = true;
                    return null;
                }

            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        public static async Task<string> SubmitExemptionRequest(ZakatExcemptionReqModel requestData)
        {

            if (NetworkCheck.IsInternet())
            {
                try
                {
                    string _vatObjectionsResponsestr = string.Empty;
                    String url = ZATCAConstants.ZakatExemtionPostRequest;
                    var uri = new Uri(url);
                    var serilized = JsonConvert.SerializeObject(requestData);
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var lang = UtilityManager.GetLanguageParameter();
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);

                    client.Timeout = TimeSpan.FromMinutes(_timeoutMinutes);

                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    _vatObjectionsResponsestr = await res.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(_vatObjectionsResponsestr))
                    {
                        ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_vatObjectionsResponsestr);
                        if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                        {
                            string errorMessage = string.Empty;
                            errorMessage = errorMesg.error.innererror.errordetails[0].message;
                            errorMessage += errorMesg.error.innererror.errordetails[1].message;
                            String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                            errorMessage = WithReplacedString;
                            throw new GAZTVATRegistrationInProcessException(errorMessage);
                        }
                    }
                    return _vatObjectionsResponsestr;
                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
                }
                catch (Exception)
                {
                    return null;
                }

            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

    }
}
