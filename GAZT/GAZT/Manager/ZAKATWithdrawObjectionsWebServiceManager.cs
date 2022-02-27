using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EGAZT.Models;
using EGAZT.Models.ZakatObjectionsModel;
using GAZT;
using GAZT.Helper;
using GAZT.Manager;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Xamarin.Forms.Internals;
using static GAZT.ErrorMessage;

namespace EGAZT.Manager
{
    [Preserve(AllMembers = true)]
    public static class ZAKATWithdrawObjectionsWebServiceManager
    {
        #region Zakat Withdraw Objection
        public static async Task<ZakatWithdrawMainDataModel> GAZTGetZakatWithDrawMainData()
        {
            ZakatWithdrawMainDataModel _zakatWithdrawMainDataModel = new ZakatWithdrawMainDataModel();
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    Char lang = WebServiceManager.GetLangZParameter();
                    String url = Constants.ZakatObjectionWDMaindataRL + "Auditorz='" + "',Taxpayerz='" + App.LoginDataRetrieved.TIN + "',RegIdz='" + "'," +
                        "Submitz='" + "',Fbnumz='" + "',Langz='" + lang + "',UserTin='" + "')?$expand=znotesSet,AttDetSet,zobj_itemsSet&$format=json";
                    HttpResponseMessage _zakatWithdrawMainDataResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
                    if (_zakatWithdrawMainDataResponse != null)
                    {
                        if (_zakatWithdrawMainDataResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _zakatWithdrawMainDataResponse.Headers;
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
                        String _zakatWithdrawMainData = _zakatWithdrawMainDataResponse.Content.ReadAsStringAsync().Result;
                        _zakatWithdrawMainDataModel = JsonConvert.DeserializeObject<ZakatWithdrawMainDataModel>(_zakatWithdrawMainData);
                        if (!string.IsNullOrEmpty(_zakatWithdrawMainData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatWithdrawMainData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _zakatWithdrawMainDataModel;
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

        public static async Task<ZakatObjectionWithDrawListModel> GAZTGetZakatWithDrawList()
        {
            ZakatObjectionWithDrawListModel _zakatObjectionWithDrawListModel = new ZakatObjectionWithDrawListModel();
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.ZakatObjectionWDListRL + "Taxpy eq '" + App.LoginDataRetrieved.TIN + "'&$format=json";
                    var uri = new Uri(url);


                    HttpResponseMessage __zakatObjectionWithDrawListResponse = await client.GetAsync(uri);
                    if (__zakatObjectionWithDrawListResponse != null)
                    {
                        if (__zakatObjectionWithDrawListResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = __zakatObjectionWithDrawListResponse.Headers;
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
                        String ___zakatObjectionWithDrawListData = __zakatObjectionWithDrawListResponse.Content.ReadAsStringAsync().Result;
                        _zakatObjectionWithDrawListModel = JsonConvert.DeserializeObject<ZakatObjectionWithDrawListModel>(___zakatObjectionWithDrawListData);
                        if (!string.IsNullOrEmpty(___zakatObjectionWithDrawListData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(___zakatObjectionWithDrawListData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _zakatObjectionWithDrawListModel;
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

        public static async Task<ZakatObjectionWDDropdownModel> GAZTGetZakatWithDrawDDData(string objFbnum)
        {
            ZakatObjectionWDDropdownModel _zakatObjectionWDDropdownModel = new ZakatObjectionWDDropdownModel();
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.ZakatObjectionWDSelectedDDURL + "ObjFbnum eq '" + objFbnum + "'&$format=json";
                    var uri = new Uri(url);


                    HttpResponseMessage __zakatObjectionWDDropdownResponse = await client.GetAsync(uri);
                    if (__zakatObjectionWDDropdownResponse != null)
                    {
                        if (__zakatObjectionWDDropdownResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = __zakatObjectionWDDropdownResponse.Headers;
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
                        String __zakatObjectionWDDropdownData = __zakatObjectionWDDropdownResponse.Content.ReadAsStringAsync().Result;
                        _zakatObjectionWDDropdownModel = JsonConvert.DeserializeObject<ZakatObjectionWDDropdownModel>(__zakatObjectionWDDropdownData);
                        if (!string.IsNullOrEmpty(__zakatObjectionWDDropdownData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__zakatObjectionWDDropdownData);
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
                    return _zakatObjectionWDDropdownModel;
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

        public static async Task<AttachmentRootOject> GAZTSaveZakatObjectionWDAttachment(byte[] AttachmentByte, string fileName, string RetGuid, string Dotyp, string contentType)//, string returnedFguid
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    AttachmentRootOject _attachment = new AttachmentRootOject();
                    char LangZ = WebServiceManager.GetLangZParameter();
                    string AttBy = "TP";
                    String url = Constants.ZakatObjectionWDAttachmentURL + "RetGuid='" + RetGuid + "'" + ",Flag='" + "N" + "'" + ",Dotyp='" + Dotyp + "'" + ",SchGuid='" + "'" + ",Srno=" + "1" + ",Doguid='" + "'" + ",AttBy='" + AttBy + "'" + ",OutletRef='" + "'" + ")/AttachMedSet";
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("slug", fileName);
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

                    ByteArrayContent baContent = new ByteArrayContent(AttachmentByte);
                    if (!string.IsNullOrEmpty(contentType))
                        baContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                    var response = await client.PostAsync(url, baContent);
                    var responsestr = response.Content.ReadAsStringAsync().Result;
                    _attachment = JsonConvert.DeserializeObject<AttachmentRootOject>(responsestr);
                    return _attachment;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        public static string GAZTZakatObjectonWDDownloadacknowledgement(string fbnum)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    String Url = string.Empty;
                    Url = Constants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_ACK_LETTER_SRV/Ack_letterSet(Fbnum='" + fbnum + "')/$value";

                    return Url;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        public static string GAZTZakatObjectionWDDownloadForm(string fbnum)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    String Url = string.Empty;
                    Url = Constants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_COVER_FORM_SRV/cover_formSet(Fbnum='" + fbnum + "')/$value";

                    return Url;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }


        public static async Task<ZakatObjectionRequestSummaryModel> GAZTGetZakatRequestObjectionSummary(string fbnum)
        {
            ZakatObjectionRequestSummaryModel _zakatObjectionRequestSummaryModel = new ZakatObjectionRequestSummaryModel();
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.ZakatObjectionRequestSummaryURL + "Taxpayerz='" + "',Fbnumz='" + fbnum + "',Langz='" + "',Auditorz='" + "'," +
                        "Euser='00000000000000000000',Fbguid='" + "')?=&$expand=ZNOB_ObjSet,Off_notesSet,AttDetSet&$format=json";
                    var uri = new Uri(url);


                    HttpResponseMessage _zakatObjectionRequestSummaryResponse = await client.GetAsync(uri);
                    if (_zakatObjectionRequestSummaryResponse != null)
                    {
                        if (_zakatObjectionRequestSummaryResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _zakatObjectionRequestSummaryResponse.Headers;
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
                        String _zakatObjectionSummaryData = _zakatObjectionRequestSummaryResponse.Content.ReadAsStringAsync().Result;
                        _zakatObjectionRequestSummaryModel = JsonConvert.DeserializeObject<ZakatObjectionRequestSummaryModel>(_zakatObjectionSummaryData);
                        if (!string.IsNullOrEmpty(_zakatObjectionSummaryData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatObjectionSummaryData);
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
                    return _zakatObjectionRequestSummaryModel;
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

        public static async Task<ZakatObjectionWithdrawPostResponceModel> GAZTSaveZakatObjectionWithDrawData(ZakatObjectionWithdrawPostModel.Root postData)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    ZakatObjectionWithdrawPostResponceModel responseData = new ZakatObjectionWithdrawPostResponceModel();

                    string LangZ = WebServiceManager.GetLangZParameterAREN();
                    String url = Constants.ZakatObjectionWDPostURL;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var serilized = JsonConvert.SerializeObject(postData);

                    client.DefaultRequestHeaders.Add("Token", App.Token);
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    HttpResponseMessage res = client.PostAsync(uri, contentPost).Result;
                    var _ZakatWithDrawresult = res.Content.ReadAsStringAsync().Result;
                    responseData = JsonConvert.DeserializeObject<ZakatObjectionWithdrawPostResponceModel>(_ZakatWithDrawresult);
                    if (_ZakatWithDrawresult == null || responseData.d == null)
                    {
                        WebServiceManager.ErrorMessage = string.Empty;
                        ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_ZakatWithDrawresult);
                        if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                        {
                            WebServiceManager.ErrorMessage = errorMesg.error.innererror.errordetails[0].message;
                        }
                    }
                    return responseData;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        public static async Task<ZakatObjectionSummaryModel> GAZTGetZakatObjectionSummary(string fbnum)
        {
            ZakatObjectionSummaryModel _zakatObjectionSummaryModel = new ZakatObjectionSummaryModel();
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.ZakatObjectionSummaryURL + "Auditorz='" + "',Taxpayerz='" + App.LoginDataRetrieved.TIN + "',RegIdz='" + "',Submitz='" + "'," +
                        "Savez='" + "',Fbnumz='',Langz='" + lang + "',UserTin='" + "')?$expand=znotesSet,AttDetSet,zobj_itemsSet&$format=json";
                    var uri = new Uri(url);


                    HttpResponseMessage _zakatObjectionSummaryResponse = await client.GetAsync(uri);
                    if (_zakatObjectionSummaryResponse != null)
                    {
                        if (_zakatObjectionSummaryResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _zakatObjectionSummaryResponse.Headers;
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
                        String _zakatObjectionSummaryData = _zakatObjectionSummaryResponse.Content.ReadAsStringAsync().Result;
                        _zakatObjectionSummaryModel = JsonConvert.DeserializeObject<ZakatObjectionSummaryModel>(_zakatObjectionSummaryData);
                        if (!string.IsNullOrEmpty(_zakatObjectionSummaryData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatObjectionSummaryData);
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
                    return _zakatObjectionSummaryModel;
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

        public static async Task<ZakatObjectionSummaryModel> GAZTGetZakatObjectionSummaryTP10(string fbnum)
        {
            ZakatObjectionSummaryModel _zakatObjectionSummaryModel = new ZakatObjectionSummaryModel();
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.ZakatObjectionSummaryURLTP10 + "Auditorz='" + "',Taxpayerz='" + App.LoginDataRetrieved.TIN + "',RegIdz='" + "',Submitz='" + "'," +
                        "Savez='" + "',Fbnumz='" + fbnum + "',Langz='" + lang + "',UserTin='" + "')?$expand=znotesSet,AttDetSet&$format=json";
                    var uri = new Uri(url);


                    HttpResponseMessage _zakatObjectionSummaryResponse = await client.GetAsync(uri);
                    if (_zakatObjectionSummaryResponse != null)
                    {
                        if (_zakatObjectionSummaryResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _zakatObjectionSummaryResponse.Headers;
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
                        String _zakatObjectionSummaryData = _zakatObjectionSummaryResponse.Content.ReadAsStringAsync().Result;
                        _zakatObjectionSummaryModel = JsonConvert.DeserializeObject<ZakatObjectionSummaryModel>(_zakatObjectionSummaryData);
                        if (!string.IsNullOrEmpty(_zakatObjectionSummaryData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatObjectionSummaryData);
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
                    return _zakatObjectionSummaryModel;
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

        #endregion
    }
}
