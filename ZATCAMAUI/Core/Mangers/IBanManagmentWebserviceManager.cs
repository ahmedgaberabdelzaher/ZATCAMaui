using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models;
using static ZATCAMAUI.Models.ErrorMessage;
using static ZATCAMAUI.Models.IBanManagementListModel;
namespace ZATCAMAUI.Manager
{
    public static class IBanManagmentWebserviceManager
    {

        public async static Task<IBanAccountManagementResponseModel> GAZTGetIBanAccounts()
        {
            if (NetworkCheck.IsInternet())
            {
                IBanAccountManagementResponseModel IBanModelResponse = new IBanAccountManagementResponseModel();
                string NewToken = string.Empty;
                try
                {
                    string lang = WebServiceManager.GetLangZParameterAREN();
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    String url = ZATCAConstants.GetBankAccountInformation + "?TIN=" + App.LoginDataRetrieved.TIN;

                    var uri = new Uri(url);
                    HttpResponseMessage GAZTIBanAccountsResponse = await client.GetAsync(uri);
                    if (GAZTIBanAccountsResponse != null)
                    {
                        if (GAZTIBanAccountsResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTIBanAccountsResponse.Headers;
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
                        String IBanData = GAZTIBanAccountsResponse.Content.ReadAsStringAsync().Result;
                        IBanModelResponse = JsonConvert.DeserializeObject<IBanAccountManagementResponseModel>(IBanData);

                        if (!string.IsNullOrEmpty(IBanData) && IBanModelResponse == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(IBanData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                throw new Exception(errorMessage);
                            }
                        }
                        return IBanModelResponse;

                    }
                    return IBanModelResponse;
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

        public async static Task<IBANPostResponse> GAZTSubmitBankAccountIBAN(IBANPostRequest postdata)
        {
            IBANPostResponse IBANPostResponse = new IBANPostResponse();
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    string lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    String url = ZATCAConstants.PostBankAccountIBAN;
                    var uri = new Uri(url);
                    var serilized = JsonConvert.SerializeObject(postdata);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    var detailJson = res.Content.ReadAsStringAsync().Result;
                    IBANPostResponse = JsonConvert.DeserializeObject<IBANPostResponse>(detailJson);

                    if (IBANPostResponse == null || IBANPostResponse.d == null)
                    {
                        ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
                        if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                        {
                            WebServiceManager.ErrorMessageForVAT = errorMesg.error.innererror.errordetails[0].message;
                            WebServiceManager.ErrorMessageForVAT += errorMesg.error.innererror.errordetails[1].message;
                            String WithReplacedString = WebServiceManager.ErrorMessageForVAT.Replace("An exception was raised", string.Empty);
                            WebServiceManager.ErrorMessageForVAT = WithReplacedString;
                            //ErrorMessageForVAT
                            throw new GAZTVATRegistrationInProcessException(WebServiceManager.ErrorMessageForVAT);
                        }
                    }

                    return IBANPostResponse;
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

        public async static Task<IBANPostResponse> GAZTSubmitBankAccountIBAN(IBANClickRequest postdata)
        {
            IBANPostResponse IBANPostResponse = new IBANPostResponse();
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    string lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    String url = ZATCAConstants.PostBankAccountIBAN;
                    var uri = new Uri(url);
                    var serilized = JsonConvert.SerializeObject(postdata);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    var detailJson = res.Content.ReadAsStringAsync().Result;
                    IBANPostResponse = JsonConvert.DeserializeObject<IBANPostResponse>(detailJson);

                    if (IBANPostResponse == null || IBANPostResponse.d == null)
                    {
                        ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
                        if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                        {
                            WebServiceManager.ErrorMessageForVAT = errorMesg.error.innererror.errordetails[0].message;
                            WebServiceManager.ErrorMessageForVAT += errorMesg.error.innererror.errordetails[1].message;
                            String WithReplacedString = WebServiceManager.ErrorMessageForVAT.Replace("An exception was raised", string.Empty);
                            WebServiceManager.ErrorMessageForVAT = WithReplacedString;
                            //ErrorMessageForVAT
                            throw new GAZTVATRegistrationInProcessException(WebServiceManager.ErrorMessageForVAT);
                        }
                    }

                    return IBANPostResponse;
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

        public async static Task<IbanAccountFormGuidResponse> GAZTGetIBanAccountsFormGUID()
        {
            if (NetworkCheck.IsInternet())
            {
                IbanAccountFormGuidResponse IBanModelResponse = new IbanAccountFormGuidResponse();
                string NewToken = string.Empty;
                try
                {
                    string lang = WebServiceManager.GetLangZParameterAREN();
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    String url = ZATCAConstants.GetIBANAcoountFormGUID + "?formGUID=";


                    var uri = new Uri(url);
                    HttpResponseMessage GAZTIBanAccountsResponse = await client.GetAsync(uri);
                    if (GAZTIBanAccountsResponse != null)
                    {
                        if (GAZTIBanAccountsResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTIBanAccountsResponse.Headers;
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
                        String IBanData = GAZTIBanAccountsResponse.Content.ReadAsStringAsync().Result;
                        IBanModelResponse = JsonConvert.DeserializeObject<IbanAccountFormGuidResponse>(IBanData);

                        if (!string.IsNullOrEmpty(IBanData) && IBanModelResponse == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(IBanData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                throw new Exception(errorMessage);
                            }
                        }
                        return IBanModelResponse;

                    }
                    return IBanModelResponse;
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
        //public async static Task<IBANPostResponse> GAZTSubmitBankAccountIBAN(IBANPostRequest postdata)
        //{
        //    IBANPostResponse IBANPostResponse = new IBANPostResponse();
        //    if (NetworkCheck.IsInternet())
        //    {
        //        try
        //        {
        //            string LangZAREN = WebServiceManager.GetLangZParameterAREN();

        //            char LangZ = WebServiceManager.GetLangZParameter();
        //            string lang = UtilityManager.GetLanguageParameter();
        //            string url = ZATCAConstants.PostBankAccountIBAN + lang;

        //            var uri = new Uri(url);
        //            HttpClient client = new HttpClient(App.httpClientHandler);

        //            client.DefaultRequestHeaders.Add("Token", "123");
        //            client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

        //            client.DefaultRequestHeaders.Add("X-Requested-With", "X");
        //            client.DefaultRequestHeaders.Add("Accept", "application/json");



        //            var serilized = JsonConvert.SerializeObject(postdata);
        //            HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
        //            HttpResponseMessage res = await client.PostAsync(uri, contentPost);
        //            var detailJson = res.Content.ReadAsStringAsync().Result;
        //            IBANPostResponse = JsonConvert.DeserializeObject<IBANPostResponse>(detailJson);

        //            if (IBANPostResponse == null || IBANPostResponse.d == null)
        //            {
        //                ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
        //                if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
        //                {
        //                    WebServiceManager.ErrorMessageForVAT = errorMesg.error.innererror.errordetails[0].message;
        //                    WebServiceManager.ErrorMessageForVAT += errorMesg.error.innererror.errordetails[1].message;
        //                    String WithReplacedString = WebServiceManager.ErrorMessageForVAT.Replace("An exception was raised", string.Empty);
        //                    WebServiceManager.ErrorMessageForVAT = WithReplacedString;
        //                    //ErrorMessageForVAT
        //                    throw new GAZTVATRegistrationInProcessException(WebServiceManager.ErrorMessageForVAT);
        //                }
        //            }

        //            return IBANPostResponse;
        //        }
        //        catch (GAZTVATRegistrationInProcessException ex)
        //        {
        //            throw new GAZTVATRegistrationInProcessException(ex.Message);
        //        }

        //        catch (Exception)
        //        {
        //            return null;
        //        }
        //    }
        //    else
        //    {
        //        throw new InternetException(AppResources.ZZInternetConnectionMessage);
        //    }
        //}

        //public async static Task<IBANPostResponse> GAZTSubmitBankAccountIBAN(IBANClickRequest postdata)
        //{
        //    IBANPostResponse IBANPostResponse = new IBANPostResponse();
        //    if (NetworkCheck.IsInternet())
        //    {
        //        try
        //        {
        //            string LangZAREN = WebServiceManager.GetLangZParameterAREN();

        //            char LangZ = WebServiceManager.GetLangZParameter();
        //            string lang = UtilityManager.GetLanguageParameter();
        //            String url = ZATCAConstants.PostBankAccountIBAN + lang;

        //            var uri = new Uri(url);
        //            HttpClient client = new HttpClient(App.httpClientHandler);

        //            client.DefaultRequestHeaders.Add("Token", "123");
        //            client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

        //            client.DefaultRequestHeaders.Add("X-Requested-With", "X");
        //            client.DefaultRequestHeaders.Add("Accept", "application/json");



        //            var serilized = JsonConvert.SerializeObject(postdata);
        //            HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
        //            HttpResponseMessage res = await client.PostAsync(uri, contentPost);
        //            var detailJson = res.Content.ReadAsStringAsync().Result;
        //            IBANPostResponse = JsonConvert.DeserializeObject<IBANPostResponse>(detailJson);

        //            if (IBANPostResponse == null || IBANPostResponse.d == null)
        //            {
        //                ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
        //                if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
        //                {
        //                    WebServiceManager.ErrorMessageForVAT = errorMesg.error.innererror.errordetails[0].message;
        //                    WebServiceManager.ErrorMessageForVAT += errorMesg.error.innererror.errordetails[1].message;
        //                    String WithReplacedString = WebServiceManager.ErrorMessageForVAT.Replace("An exception was raised", string.Empty);
        //                    WebServiceManager.ErrorMessageForVAT = WithReplacedString;
        //                    //ErrorMessageForVAT
        //                    throw new GAZTVATRegistrationInProcessException(WebServiceManager.ErrorMessageForVAT);
        //                }
        //            }

        //            return IBANPostResponse;
        //        }
        //        catch (GAZTVATRegistrationInProcessException ex)
        //        {
        //            throw new GAZTVATRegistrationInProcessException(ex.Message);
        //        }

        //        catch (Exception)
        //        {
        //            return null;
        //        }
        //    }
        //    else
        //    {
        //        throw new InternetException(AppResources.ZZInternetConnectionMessage);
        //    }
        //}

   }
}
