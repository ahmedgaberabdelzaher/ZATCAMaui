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

                    zuser = "";
                    euser1 = "null";
                    fbguid = "";

                    var lang = UtilityManager.GetLanguageParameter();
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


                        ErrorObj statusHeader = JsonConvert.DeserializeObject<ErrorObj>(_zakatInstalmentRequestData);
                        if (statusHeader?.header?.status?.code != "E999999")
                        {
                            _ZakatInstalmentPlanRequestList = JsonConvert.DeserializeObject<ZakatInstalmentPlanRequestListModel>(_zakatInstalmentRequestData);
                            if (!string.IsNullOrEmpty(_zakatInstalmentRequestData) && _ZakatInstalmentPlanRequestList.d == null)
                            {
                                string errorMessage = WebServiceManager.PrepareErrorMessageByJson(_zakatInstalmentRequestData);
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                        else
                        {
                            throw new GAZTNetworkConnectivityIssueException();
                        }

                    }
                    return _ZakatInstalmentPlanRequestList;
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


        public async static Task<ZakatInstalmentPlanRequestListModel> GAZTGetZakatRevokeList(string zuser, string fbguid, string euser1)
        {

            if (NetworkCheck.IsInternet())
            {
                ZakatInstalmentPlanRequestListModel _zakatRevokeList = new ZakatInstalmentPlanRequestListModel();
                string NewToken = string.Empty;
                try
                {

                    zuser = "";
                    euser1 = "null";
                    fbguid = "";
                    var lang = UtilityManager.GetLanguageParameter();
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
                        ErrorObj statusHeader = JsonConvert.DeserializeObject<ErrorObj>(_zakatRevokeInstalmentRequestData);
                        if (statusHeader?.header?.status?.code != "E999999")
                        {
                            _zakatRevokeList = JsonConvert.DeserializeObject<ZakatInstalmentPlanRequestListModel>(_zakatRevokeInstalmentRequestData);

                            if (!string.IsNullOrEmpty(_zakatRevokeInstalmentRequestData) && _zakatRevokeList.d == null)
                            {
                                string errorMessage = WebServiceManager.PrepareErrorMessageByJson(_zakatRevokeInstalmentRequestData);
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                        else
                        {
                            throw new GAZTNetworkConnectivityIssueException();
                        }


                    }
                    return _zakatRevokeList;
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



        public async static Task<SummaryDisplayModel> GAZTGetZakatRequestDisplayData(string fbnum, string status)
        {
            if (NetworkCheck.IsInternet())
            {
                ZakatSummaryInputModel summaryInputs=null;
                SummaryDisplayModel _zakatRequestDisplayModel = new SummaryDisplayModel();
                string NewToken = string.Empty;
                try
                {
                    try
                    {
                         summaryInputs = await GAZTGetZakatSummaryInputData(fbnum, status);
                    }
                    catch (GAZTVATRegistrationInProcessException ex)
                    {
                        await UtilityManager.HandleExceptionMessage(ex.Message, false);
                    }
                    catch (GAZTNetworkConnectivityIssueException)
                    {
                        await UtilityManager.HandleExceptionMessage(AppResources.NetworkConnectivityIssue, false);
                    }
                    catch (InternetException)
                    {
                        await UtilityManager.HandleExceptionMessage(AppResources.ZZInternetConnectionMessage, false);
                    }
                    catch (Exception)
                    {
                        await UtilityManager.HandleExceptionMessage(AppResources.Somethingwentwrong, false);
                    }
                   
                    string fbguid = summaryInputs.d.Fbguid;
                    var lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    String url = ZATCAConstants.ZakatRequestDisplayUrl + App.TP.TIN + "&language=" + lang + "&formBundleNumber=" + fbnum + "&formMode=N";
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
                        ErrorObj statusHeader = JsonConvert.DeserializeObject<ErrorObj>(_zakatDisplayRequestData);
                        if (statusHeader?.header?.status?.code != "E999999")
                        {
                            _zakatRequestDisplayModel = JsonConvert.DeserializeObject<SummaryDisplayModel>(_zakatDisplayRequestData);
                            if (!string.IsNullOrEmpty(_zakatDisplayRequestData) && _zakatRequestDisplayModel.d == null)
                            {
                                string errorMessage = WebServiceManager.PrepareErrorMessageByJson(_zakatDisplayRequestData);
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                        else
                        {
                            throw new GAZTNetworkConnectivityIssueException();
                        }
                    }
                    return _zakatRequestDisplayModel;
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

                        ErrorObj statusHeader = JsonConvert.DeserializeObject<ErrorObj>(_ZakatSummaryInputData);
                        if (statusHeader?.header?.status?.code != "E999999")
                        {
                            _zakatSummaryInputModel = JsonConvert.DeserializeObject<ZakatSummaryInputModel>(_ZakatSummaryInputData);
                            if (!string.IsNullOrEmpty(_ZakatSummaryInputData))
                            {
                                ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_ZakatSummaryInputData);
                                if (errorMesg?.header?.moreInformation?.errorDetails != null ||
                       errorMesg?.header?.moreInformation?.errorDetails.Count > 0)
                                {
                                    string errorMessage = WebServiceManager.PrepareErrorMessageByJson(_ZakatSummaryInputData);
                                    throw new GAZTVATRegistrationInProcessException(errorMessage);
                                }
                            }
                        }
                        else
                        {
                            throw new GAZTNetworkConnectivityIssueException();
                        }

                    }
                    return _zakatSummaryInputModel;
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
    }
}
