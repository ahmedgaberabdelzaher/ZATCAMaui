using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Models.ZakatInstalationModels;
using static ZATCAMAUI.Models.ErrorMessage;

namespace ZATCAMAUI.Core.Mangers
{

    public static class ZakatWebServiceManager
    {
        #region Zakat
        public async static Task<ZakatInstalmentPlanRequestListModel> GAZTGetZakatInstalmentPlanRequestList(string zuser, string fbguid, string euser1)
        {

            if (NetworkCheck.IsInternet())
            {
                ZakatInstalmentPlanRequestListModel _ZakatInstalmentPlanRequestList = new ZakatInstalmentPlanRequestListModel();
                string NewToken = string.Empty;
                try
                {

                    string callServ = "IPRA";
                    zuser = "";
                    euser1 = "null";
                    string auditor = "null";
                    string euser2 = "null";
                    string euser3 = "null";
                    string euser4 = "null";
                    string euser5 = "null";
                    fbguid = "";
                    string userTyp = "TP";

                    var lang = UtilityManager.GetLanguageParameter();
                    //String url = ZATCAConstants.ZakatListOfInstalmentplanRequestUrl + "CallServ='" + callServ + "',HostName='" + "',Bpnum='" + App.LoginDataRetrieved.TIN + "',Zuser='" + "'," +
                    //   "Auditor='" + auditor + "'," +
                    // "Lang='" + lang + "',Euser1='" + euser1 + "',Euser2='" + euser2 + "',Euser3='" + euser3 + "'," +
                    // "Euser4='" + euser4 + "',Euser5='" + euser5 + "',Fbguid='" + fbguid + "',UserTin='" + "',Fbnum='" + "',UserTyp='" + userTyp + "')?$expand=WorklistSet,AuthServSet,EvtNotif12Set,EvtNotif1Set,RevokeListSet&$format=json";
                    //HttpResponseMessage GAZTzakatInstalmentDataResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    string url = ZATCAConstants.ZakatListOfInstalmentplanRequestUrl + App.TP.TIN + "&language=" + lang;
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTzakatInstalmentDataResponse = await client.GetAsync(uri);

                    if (GAZTzakatInstalmentDataResponse != null)
                    {
                        if (GAZTzakatInstalmentDataResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTzakatInstalmentDataResponse.Headers;
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
                        var _zakatInstalmentRequestData = GAZTzakatInstalmentDataResponse.Content.ReadAsStringAsync().Result;
                        _ZakatInstalmentPlanRequestList = JsonConvert.DeserializeObject<ZakatInstalmentPlanRequestListModel>(_zakatInstalmentRequestData);

                        if (!string.IsNullOrEmpty(_zakatInstalmentRequestData) && _ZakatInstalmentPlanRequestList.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatInstalmentRequestData);
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
                    return _ZakatInstalmentPlanRequestList;
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


        public async static Task<ZakatInstalmentPlanRequestListModel> GAZTGetZakatRevokeList(string zuser, string fbguid, string euser1)
        {

            if (NetworkCheck.IsInternet())
            {
                ZakatInstalmentPlanRequestListModel _zakatRevokeList = new ZakatInstalmentPlanRequestListModel();
                string NewToken = string.Empty;
                try
                {

                    string callServ = "IPRR";
                    zuser = "";
                    euser1 = "null";
                    string auditor = "null";
                    string euser2 = "null";
                    string euser3 = "null";
                    string euser4 = "null";
                    string euser5 = "null";
                    fbguid = "";
                    string userTyp = "TP";


                    var lang = UtilityManager.GetLanguageParameter();
                    //String url = ZATCAConstants.ZakatRevokeRequestListUrl + "CallServ='" + callServ + "',HostName='" + "',Bpnum='" + App.LoginDataRetrieved.TIN + "',Zuser='" + "'," +
                    //   "Auditor='" + auditor + "'," +
                    // "Lang='" + lang + "',Euser1='" + euser1 + "',Euser2='" + euser2 + "',Euser3='" + euser3 + "'," +
                    // "Euser4='" + euser4 + "',Euser5='" + euser5 + "',Fbguid='" + fbguid + "',UserTin='" + "',Fbnum='" + "',UserTyp='" + userTyp + "')?$expand=WorklistSet,AuthServSet,EvtNotif12Set,EvtNotif1Set,RevokeListSet&$format=json";
                    //  HttpResponseMessage GAZTzakatRevokeInstalmentDataResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    string url = ZATCAConstants.ZakatRevokeRequestListUrl + App.TP.TIN + "&language=" + lang;
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTzakatRevokeInstalmentDataResponse = await client.GetAsync(uri);


                    if (GAZTzakatRevokeInstalmentDataResponse != null)
                    {
                        if (GAZTzakatRevokeInstalmentDataResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTzakatRevokeInstalmentDataResponse.Headers;
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

                        string _zakatRevokeInstalmentRequestData = GAZTzakatRevokeInstalmentDataResponse.Content.ReadAsStringAsync().Result;
                        _zakatRevokeList = JsonConvert.DeserializeObject<ZakatInstalmentPlanRequestListModel>(_zakatRevokeInstalmentRequestData);

                        if (!string.IsNullOrEmpty(_zakatRevokeInstalmentRequestData) && _zakatRevokeList.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatRevokeInstalmentRequestData);
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
                    return _zakatRevokeList;
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



        public async static Task<SummaryDisplayModel> GAZTGetZakatRequestDisplayData(string fbnum, string status)
        {
            if (NetworkCheck.IsInternet())
            {
                SummaryDisplayModel _zakatRequestDisplayModel = new SummaryDisplayModel();
                string NewToken = string.Empty;
                try
                {
                    var summaryInputs = await GAZTGetZakatSummaryInputData(fbnum, status);
                    string euser = "00000000000000000000";
                    string fbguid = summaryInputs.d.Fbguid;
                    var lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    //String url = ZATCAConstants.ZakatRequestDisplayUrl + "Tin='',Euser='00000000000000000000',SearchTin='" + "',Langz='EN',Fbguid='" + fbguid + "'," +
                    //  "Fbnum='" + fbnum + "',FormMode='S')?$expand=AttachSet,NotesSet,FnDtlSet&$format=json";
                    String url = ZATCAConstants.ZakatRequestDisplayUrl + App.TP.TIN + "&language=" + lang + "&formBundleNumber=" + fbnum + "&formMode=N";
                    // HttpResponseMessage GAZTzakatDisplayResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTzakatDisplayResponse = await client.GetAsync(uri);

                    if (GAZTzakatDisplayResponse != null)
                    {
                        if (GAZTzakatDisplayResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTzakatDisplayResponse.Headers;
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
                        string _zakatDisplayRequestData = GAZTzakatDisplayResponse.Content.ReadAsStringAsync().Result;
                        _zakatRequestDisplayModel = JsonConvert.DeserializeObject<SummaryDisplayModel>(_zakatDisplayRequestData);



                        if (!string.IsNullOrEmpty(_zakatDisplayRequestData) && _zakatRequestDisplayModel.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatDisplayRequestData);
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
                    return _zakatRequestDisplayModel;
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
        private async static Task<ZakatSummaryInputModel> GAZTGetZakatSummaryInputData(string fbnum, string status)
        {
            ZakatSummaryInputModel _zakatSummaryInputModel = new ZakatSummaryInputModel();
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    string fbtyp = "IPRF";

                    var lang = UtilityManager.GetLanguageParameter();
                    //HttpClient client = new HttpClient(App.httpClientHandler);
                    //String url = ZATCAConstants.GetZAKATSummaryInputURL + "Euser1='00000000000000000000',Fbguid='" + "',Fbnum='" + fbnum + "',Fbtyp='" + fbtyp + "'," +
                    // "Gpart='" + App.LoginDataRetrieved.TIN + "',Lang='" + lang + "',Persl='" + "',Status='" + status + "',TaxOffUid='" + "')?$format=json";
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    String url = ZATCAConstants.GetZAKATSummaryInputURL + App.TP.TIN + "&language=" + lang + "&Status=" + status;

                    var uri = new Uri(url);
                    HttpResponseMessage _zakatSumamryInputResponse = await client.GetAsync(uri);
                    if (_zakatSumamryInputResponse != null)
                    {
                        if (_zakatSumamryInputResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _zakatSumamryInputResponse.Headers;
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
                        string _ZakatSummaryInputData = _zakatSumamryInputResponse.Content.ReadAsStringAsync().Result;
                        _zakatSummaryInputModel = JsonConvert.DeserializeObject<ZakatSummaryInputModel>(_ZakatSummaryInputData);





                        if (!string.IsNullOrEmpty(_ZakatSummaryInputData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_ZakatSummaryInputData);
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
                    return _zakatSummaryInputModel;
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
