using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.ZakatObjectionsModel;
using static ZATCAMAUI.Models.ErrorMessage;
using static ZATCAMAUI.Models.ZakatObjectionsModel.ZakatObjectionWithDrawListModel;

namespace ZATCAMAUI.Core.Mangers
{
    public static class ZAKATWithdrawObjectionsWebServiceManager
    {
        #region Zakat Withdraw Objection
        public static async Task<ZakatWithdrawMainDataModel> GAZTGetZakatWithDrawMainData()
        {
            ZakatWithdrawMainDataModel _zakatWithdrawMainDataModel = new ZakatWithdrawMainDataModel();
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    char lang = WebServiceManager.GetLangZParameter();
                    string url = ZATCAConstants.ZakatObjectionWDMaindataRL + "Auditorz='" + "',Taxpayerz='" + App.LoginDataRetrieved.TIN + "',RegIdz='" + "'," +
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
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        string _zakatWithdrawMainData = _zakatWithdrawMainDataResponse.Content.ReadAsStringAsync().Result;
                        _zakatWithdrawMainDataModel = JsonConvert.DeserializeObject<ZakatWithdrawMainDataModel>(_zakatWithdrawMainData);
                        if (!string.IsNullOrEmpty(_zakatWithdrawMainData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatWithdrawMainData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                string WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
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

        public static async Task<ZakatObjectionWithDrawListModelClass> GAZTGetZakatWithDrawList()
        {

            ZakatObjectionWithDrawListModelClass _zakatObjectionWithDrawListModel = new ZakatObjectionWithDrawListModelClass();
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    string lang = WebServiceManager.GetLangZParameterAREN();
                    HttpClient client = new HttpClient();
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;

                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    String url = ZATCAConstants.ZakatObjectionWDListRL + App.LoginDataRetrieved.TIN;
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
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        String ___zakatObjectionWithDrawListData = __zakatObjectionWithDrawListResponse.Content.ReadAsStringAsync().Result;
                        _zakatObjectionWithDrawListModel = JsonConvert.DeserializeObject<ZakatObjectionWithDrawListModelClass>(___zakatObjectionWithDrawListData);
                        if (!string.IsNullOrEmpty(___zakatObjectionWithDrawListData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(___zakatObjectionWithDrawListData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                string WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
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
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var lang = UtilityManager.GetLanguageParameter();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", "android-20013fbc500");
                    client.DefaultRequestHeaders.Add("X-Device-Name", "Samsung-s20+");
                    client.DefaultRequestHeaders.Add("X-Device-Platform", "android");
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    String url = ZATCAConstants.ZakatObjectionWDSelectedDDURL + "formBundleNumber=" + objFbnum;
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
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        string __zakatObjectionWDDropdownData = __zakatObjectionWDDropdownResponse.Content.ReadAsStringAsync().Result;
                        _zakatObjectionWDDropdownModel = JsonConvert.DeserializeObject<ZakatObjectionWDDropdownModel>(__zakatObjectionWDDropdownData);
                        if (!string.IsNullOrEmpty(__zakatObjectionWDDropdownData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__zakatObjectionWDDropdownData);
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
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    AttachmentRootOject _attachment = new AttachmentRootOject();
                    char LangZ = WebServiceManager.GetLangZParameter();
                    string AttBy = "TP";
                    string url = ZATCAConstants.ZakatObjectionWDAttachmentURL + "RetGuid='" + RetGuid + "'" + ",Flag='" + "N" + "'" + ",Dotyp='" + Dotyp + "'" + ",SchGuid='" + "'" + ",Srno=" + "1" + ",Doguid='" + "'" + ",AttBy='" + AttBy + "'" + ",OutletRef='" + "'" + ")/AttachMedSet";
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
            if (NetworkCheck.IsInternet())
            {
                try
                {

                    String Url = string.Empty;
                    Url = ZATCAConstants.BaseUrlOfODataServices + "/v1/objections/forms/acknowledgments/attachments?formBundleNumber=" + fbnum;

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
            if (NetworkCheck.IsInternet())
            {
                try
                {

                    String Url = string.Empty;
                    Url = ZATCAConstants.BaseUrlOfODataServices + "/v1/objections/forms/acknowledgments/attachments?formBundleNumber=" + fbnum;

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
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    var lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    String url = ZATCAConstants.ZakatObjectionRequestSummaryURL + fbnum + "&language=" + lang + "&TIN=" + App.TP.TIN;
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
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        string _zakatObjectionSummaryData = _zakatObjectionRequestSummaryResponse.Content.ReadAsStringAsync().Result;
                        _zakatObjectionRequestSummaryModel = JsonConvert.DeserializeObject<ZakatObjectionRequestSummaryModel>(_zakatObjectionSummaryData);
                        if (!string.IsNullOrEmpty(_zakatObjectionSummaryData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatObjectionSummaryData);
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
                    return _zakatObjectionRequestSummaryModel;
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

        public static async Task<ZakatObjectionWithdrawPostResponceModel> GAZTSaveZakatObjectionWithDrawData(ZakatObjectionWithdrawPostModel.Root postData)
        {
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    ZakatObjectionWithdrawPostResponceModel responseData = new ZakatObjectionWithdrawPostResponceModel();

                    string LangZ = WebServiceManager.GetLangZParameterAREN();
                    string url = ZATCAConstants.ZakatObjectionWDPostURL;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var serilized = JsonConvert.SerializeObject(postData);

                    client.DefaultRequestHeaders.Add("Token", App.Token);
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
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
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string url = ZATCAConstants.ZakatObjectionSummaryURL + "Auditorz='" + "',Taxpayerz='" + App.LoginDataRetrieved.TIN + "',RegIdz='" + "',Submitz='" + "'," +
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
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        string _zakatObjectionSummaryData = _zakatObjectionSummaryResponse.Content.ReadAsStringAsync().Result;
                        _zakatObjectionSummaryModel = JsonConvert.DeserializeObject<ZakatObjectionSummaryModel>(_zakatObjectionSummaryData);
                        if (!string.IsNullOrEmpty(_zakatObjectionSummaryData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatObjectionSummaryData);
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
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string url = ZATCAConstants.ZakatObjectionSummaryURLTP10 + "Auditorz='" + "',Taxpayerz='" + App.LoginDataRetrieved.TIN + "',RegIdz='" + "',Submitz='" + "'," +
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
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        string _zakatObjectionSummaryData = _zakatObjectionSummaryResponse.Content.ReadAsStringAsync().Result;
                        _zakatObjectionSummaryModel = JsonConvert.DeserializeObject<ZakatObjectionSummaryModel>(_zakatObjectionSummaryData);
                        if (!string.IsNullOrEmpty(_zakatObjectionSummaryData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatObjectionSummaryData);
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
