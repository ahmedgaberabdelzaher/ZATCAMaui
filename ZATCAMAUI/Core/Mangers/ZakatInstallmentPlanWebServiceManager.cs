using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Models.ZakatInstalationModels;
using static ZATCAMAUI.Models.ErrorMessage;

namespace ZATCAMAUI.Core.Mangers
{
    
    public class ZakatInstallmentPlanWebServiceManager
    {
        #region Zakat Instalment Plan

        public async static Task<ZakatInstalmentInvListModel> GAZTGetZakatInstalmentInvData(string fbNum)
        {
            ZakatInstalmentInvListModel _zakatInstalmentInvListModel = new ZakatInstalmentInvListModel();
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    char lang = WebServiceManager.GetLangZParameter();
                    string url = ZATCAConstants.ZakatInstalmentInvoiceURL + "Tin eq'" + App.LoginDataRetrieved.TIN + "' " +
                        "and Fbnum eq '" + fbNum + "' and Langz eq '" + lang + "' and InstReqFor eq '01'&$format=json";
                    HttpResponseMessage _zakatInstalmentInvListResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
                    if (_zakatInstalmentInvListResponse != null)
                    {
                        if (_zakatInstalmentInvListResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _zakatInstalmentInvListResponse.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                            App.IsSessionExpired = false;
                        }
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        string __zakatInstalmentInvListData = _zakatInstalmentInvListResponse.Content.ReadAsStringAsync().Result;
                        _zakatInstalmentInvListModel = JsonConvert.DeserializeObject<ZakatInstalmentInvListModel>(__zakatInstalmentInvListData);
                        if (!string.IsNullOrEmpty(__zakatInstalmentInvListData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__zakatInstalmentInvListData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                string WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _zakatInstalmentInvListModel;
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


        public async static Task<ZakatInstalmentValidateNewRequestModel> GAZTGetZakatInstalmentValidateNewReq()
        {
            ZakatInstalmentValidateNewRequestModel _zakatInstalmentValidateNewRequestModel = new ZakatInstalmentValidateNewRequestModel();



            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    char lang = WebServiceManager.GetLangZParameter();
                    string url = ZATCAConstants.ZakatInstalmentValidateNewRequestURL + "CallServ='IPRA',HostName='" + "',Zuser='" + App.LoginDataRetrieved.TIN + "',Bpnum='" + App.LoginDataRetrieved.TIN + "'," +
                     "Auditor='null',Lang='" + lang + "',Euser1='" + "',Euser2='null',Euser3='null',Euser4='null',Euser5='null'" +
                     ",Fbguid='" + "',UserTin='" + "',Fbnum='" + "',UserTyp='TP')?$expand=WorklistSet,AuthServSet,EvtNotif12Set,EvtNotif1Set,RevokeListSet&$format=json";
                    HttpResponseMessage zakatInstalmentValidateNewRequestResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
                    if (zakatInstalmentValidateNewRequestResponse != null)
                    {
                        if (zakatInstalmentValidateNewRequestResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = zakatInstalmentValidateNewRequestResponse.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                            App.IsSessionExpired = false;
                        }
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        string _zakatInstalmentValidateNewRequestData = zakatInstalmentValidateNewRequestResponse.Content.ReadAsStringAsync().Result;
                        _zakatInstalmentValidateNewRequestModel = JsonConvert.DeserializeObject<ZakatInstalmentValidateNewRequestModel>(_zakatInstalmentValidateNewRequestData);
                        if (!string.IsNullOrEmpty(_zakatInstalmentValidateNewRequestData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatInstalmentValidateNewRequestData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                string WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _zakatInstalmentValidateNewRequestModel;
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
        public async static Task<ZakatInstalmentPlanResponse> GetZakatInstalmentPostData(string fbnum)
        {
            if (NetworkCheck.IsInternet())
            {
                ZakatInstalmentPlanResponse zakatInstalmentDetails = new ZakatInstalmentPlanResponse();
                string NewToken = string.Empty;
                try
                {
                    char lang = WebServiceManager.GetLangZParameter();
                    string formMode = "N";
                    if (fbnum == "")
                    {
                        formMode = "N";
                    }
                    else
                    {
                        formMode = "S";
                    }
                    string url = ZATCAConstants.GetZAKATInstalmentdata + "Tin='" + App.LoginDataRetrieved.TIN + "',Euser='" + "',Fbguid='" + "',Fbnum='" + fbnum + "',FormMode='" + formMode + "',Langz='" + lang + "')?$expand=AttachSet%2cNotesSet%2cFnDtlSet&$format=json";
                    HttpResponseMessage GAZTZakatInstalmentDataResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
                    if (GAZTZakatInstalmentDataResponse != null)
                    {
                        if (GAZTZakatInstalmentDataResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTZakatInstalmentDataResponse.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                            App.IsSessionExpired = false;
                        }
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        string zakatInstalmentData = GAZTZakatInstalmentDataResponse.Content.ReadAsStringAsync().Result;
                        zakatInstalmentDetails = JsonConvert.DeserializeObject<ZakatInstalmentPlanResponse>(zakatInstalmentData);
                        if (!string.IsNullOrEmpty(zakatInstalmentData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(zakatInstalmentData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                string WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return zakatInstalmentDetails;
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


        public async static Task<ZakatInvoiceList> GetZakatInvoicesList(bool IsZakat, string fbnum)
        {
            if (NetworkCheck.IsInternet())
            {
                ZakatInvoiceList invoicesList = new ZakatInvoiceList();
                string NewToken = string.Empty;
                try
                {
                    char lang = WebServiceManager.GetLangZParameter();
                    string url = "";
                    if (IsZakat)
                    {
                        url = ZATCAConstants.GetZAKATInvoices + "Tin eq '" + App.LoginDataRetrieved.TIN + "'and " + "Fbnum eq '" + fbnum + "' and " + "Langz eq '" + lang + "' and " + "InstReqFor eq '" + "01" + "'&$format=json";
                    }
                    else
                    {
                        url = ZATCAConstants.GetZAKATInvoices + "Tin eq '" + App.LoginDataRetrieved.TIN + "'and " + "Fbnum eq '" + fbnum + "' and " + "Langz eq '" + lang + "' and " + "InstReqFor eq '" + "02" + "'&$format=json";
                    }
                    url = System.Web.HttpUtility.UrlPathEncode(url);
                    HttpResponseMessage GAZTZakatInvoiceDataResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
                    if (GAZTZakatInvoiceDataResponse != null)
                    {
                        if (GAZTZakatInvoiceDataResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTZakatInvoiceDataResponse.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                            App.IsSessionExpired = false;
                        }
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        string zakatInvoicesData = GAZTZakatInvoiceDataResponse.Content.ReadAsStringAsync().Result;
                        invoicesList = JsonConvert.DeserializeObject<ZakatInvoiceList>(zakatInvoicesData);
                        if (!string.IsNullOrEmpty(zakatInvoicesData) && invoicesList.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(zakatInvoicesData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                string WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return invoicesList;
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




        public async static Task<ZakatInstalmentPlanResponse> SaveZakatInstalmentData(ZakatInstalmentPlanRequest _zakatInstalmentDetails)
        {


            if (NetworkCheck.IsInternet())
            {
                try
                {

                    ZakatInstalmentPlanResponse _zakatResponseObject = new ZakatInstalmentPlanResponse();
                    string LangZ = WebServiceManager.GetLangZParameterAREN();
                    string url = ZATCAConstants.GetZAKATPostdata;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    var serilized = JsonConvert.SerializeObject(_zakatInstalmentDetails);
                    client.DefaultRequestHeaders.Add("Token", "123");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = client.PostAsync(uri, contentPost).Result;
                    var _zakatReturnDetailsDesponsestr = res.Content.ReadAsStringAsync().Result;
                    _zakatResponseObject = JsonConvert.DeserializeObject<ZakatInstalmentPlanResponse>(_zakatReturnDetailsDesponsestr);
                    if (!string.IsNullOrEmpty(_zakatReturnDetailsDesponsestr) && _zakatResponseObject.d == null)
                    {
                        WebServiceManager.ErrorMessage = string.Empty;
                        ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatReturnDetailsDesponsestr);
                        if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                        {
                            WebServiceManager.ErrorMessage = errorMesg.error.innererror.errordetails[0].message;
                            throw new GAZTVATRegistrationInProcessException(WebServiceManager.ErrorMessage);

                        }

                    }
                    return _zakatResponseObject;
                    //  }



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


        public async static Task<ZakatInstalmentPlanRevokeResponse> GetZakatInstalmentRevokePostData(string FBnum)
        {
            if (NetworkCheck.IsInternet())
            {
                ZakatInstalmentPlanRevokeResponse zakatInstalmentDetails = new ZakatInstalmentPlanRevokeResponse();
                string NewToken = string.Empty;
                try
                {
                    char lang = WebServiceManager.GetLangZParameter();
                    string formMode = "S";
                    string url = ZATCAConstants.GetZAKATInstalmentdata + "Tin='" + App.LoginDataRetrieved.TIN + "',Euser='" + "',Fbguid='" + "',Fbnum='" + FBnum + "',FormMode='" + formMode + "',Langz='" + lang + "')?&$format=json";
                    HttpResponseMessage GAZTZakatInstalmentDataResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
                    if (GAZTZakatInstalmentDataResponse != null)
                    {
                        if (GAZTZakatInstalmentDataResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTZakatInstalmentDataResponse.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                            App.IsSessionExpired = false;
                        }
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        string zakatInstalmentData = GAZTZakatInstalmentDataResponse.Content.ReadAsStringAsync().Result;
                        zakatInstalmentDetails = JsonConvert.DeserializeObject<ZakatInstalmentPlanRevokeResponse>(zakatInstalmentData);
                        if (!string.IsNullOrEmpty(zakatInstalmentData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(zakatInstalmentData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                string WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return zakatInstalmentDetails;
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

        public async static Task<ZakatInstalmentPlanRevokeResponse> SaveZakatInstalmentRevokeData(ZakatInstalmentPlanRevokeRequest _zakatInstalmentDetails)
        {


            if (NetworkCheck.IsInternet())
            {
                try
                {

                    ZakatInstalmentPlanRevokeResponse _zakatResponseObject = new ZakatInstalmentPlanRevokeResponse();
                    string LangZ = WebServiceManager.GetLangZParameterAREN();
                    string url = ZATCAConstants.GetZAKATPostdata;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var serilized = JsonConvert.SerializeObject(_zakatInstalmentDetails);
                    client.DefaultRequestHeaders.Add("Token", "123");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = client.PostAsync(uri, contentPost).Result;
                    var _zakatReturnDetailsDesponsestr = res.Content.ReadAsStringAsync().Result;
                    _zakatResponseObject = JsonConvert.DeserializeObject<ZakatInstalmentPlanRevokeResponse>(_zakatReturnDetailsDesponsestr);
                    if (!string.IsNullOrEmpty(_zakatReturnDetailsDesponsestr) && _zakatResponseObject.d == null)
                    {
                        WebServiceManager.ErrorMessage = string.Empty;
                        ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatReturnDetailsDesponsestr);
                        if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                        {
                            WebServiceManager.ErrorMessage = errorMesg.error.innererror.errordetails[0].message;
                            throw new GAZTVATRegistrationInProcessException(WebServiceManager.ErrorMessage);

                        }

                    }
                    return _zakatResponseObject;
                    //  }



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


        #endregion
    }
}
