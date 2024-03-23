using EGAZT.Models;
using GAZT.Helper;
using GAZT.Manager;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static EGAZT.Models.IBanManagementListModel;
using static GAZT.ErrorMessage;

namespace EGAZT.Manager
{
    public static class IBanManagmentWebserviceManager
    {

        public async static Task<IBanAccountManagementResponseModel> GAZTGetIBanAccounts()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                IBanAccountManagementResponseModel IBanModelResponse = new IBanAccountManagementResponseModel();
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    client.DefaultRequestHeaders.Add("Token", "123");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    string LangZAREN = WebServiceManager.GetLangZParameterAREN();


                    String url = Constants.GetBankAccountInformation + "Tin=" + "'" + App.LoginDataRetrieved.TIN + "'" + ",Fbguid=''," + "Euser='')?sap-language=" + LangZAREN + "&$expand=BankListSet,IbanListSet,IdNumberListSet,IdTypeListSet&$format=json";

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
        public async static Task<IbanAccountFormGuidResponse> GAZTGetIBanAccountsFormGUID()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                IbanAccountFormGuidResponse IBanModelResponse = new IbanAccountFormGuidResponse();
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    client.DefaultRequestHeaders.Add("Token", "123");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    string LangZAREN = WebServiceManager.GetLangZParameterAREN();


                    //String url = Constants.GetBankAccountInformation + "Tin=" + "'" + App.LoginDataRetrieved.TIN + "'" + ",Fbguid=''," + "Euser='')?sap-language=" + LangZAREN + "&$expand=BankListSet,IbanListSet,IdNumberListSet,IdTypeListSet&$format=json";
                    String url = Constants.GetIBANAcoountFormGUID + LangZAREN + "&$format=json";
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



        public async static Task<IBANPostResponse> GAZTSubmitBankAccountIBAN(IBANPostRequest postdata)
        {
            IBANPostResponse IBANPostResponse = new IBANPostResponse();
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    string LangZAREN = WebServiceManager.GetLangZParameterAREN();

                    char LangZ = WebServiceManager.GetLangZParameter();
                    string lang = UtilityManager.GetLanguageParameter();
                    String url = Constants.PostBankAccountIBAN + lang;

                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    client.DefaultRequestHeaders.Add("Token", "123");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");



                    var serilized = JsonConvert.SerializeObject(postdata);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
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
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    string LangZAREN = WebServiceManager.GetLangZParameterAREN();

                    char LangZ = WebServiceManager.GetLangZParameter();
                    string lang = UtilityManager.GetLanguageParameter();
                    String url = Constants.PostBankAccountIBAN + lang;

                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    client.DefaultRequestHeaders.Add("Token", "123");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");



                    var serilized = JsonConvert.SerializeObject(postdata);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
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

    }
}
