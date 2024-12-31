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

    public static class OldZakatInstallmentWebServiceManager
    {
        #region Old Zakat Instalment

        public async static Task<OldZakatInstalmentPlanRequestListModel> GAZTGetOldZakatInstalmentPlanRequestList(string callServ, string fbguid, string euser1)
        {

            if (NetworkCheck.IsInternet())
            {
                OldZakatInstalmentPlanRequestListModel _ZakatInstalmentPlanRequestList = new OldZakatInstalmentPlanRequestListModel();
                string NewToken = string.Empty;
                try
                {


                    euser1 = "00000000000000000000";
                    string auditor = "null";
                    string euser2 = "00000000000000000000";
                    string euser3 = "00000000000000000000";
                    string euser4 = "00000000000000000000";
                    string euser5 = "00000000000000000000";
                    fbguid = "";

                    //Char lang = WebServiceManager.GetLangZParameter();
                    string lang = WebServiceManager.GetLangZParameterAREN();

                    //String url = Constants.ZakatOldInstalmentsListUrl + App.LoginDataRetrieved.TIN + "CallServ='" + callServ + "',HostName='" + "',Bpnum='"  + "',Zuser='" + "'," +
                    //  "Auditor='" + auditor + "'," +
                    //"Lang='" + lang + "',Euser1='" + euser1 + "',Euser2='" + euser2 + "',Euser3='" + euser3 + "'," +
                    //"Euser4='" + euser4 + "',Euser5='" + euser5 + "',Fbguid='" + fbguid + "')?$expand=ListSet,AuthServSet&$format=json&sap-language=" + LangZAREN;
                    String url = ZATCAConstants.ZakatOldInstalmentsListUrl + App.LoginDataRetrieved.TIN + "&authenticationUser1=" + euser1 + "&authenticationUser2=" + euser2 + "&authenticationUser3=" + euser3 + "&authenticationUser4=" + euser4 + "&authenticationUser5=" + euser5 + "&language=" + lang + "&formBundleGUID=" + fbguid;
                    var uri = new Uri(url);
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
                    HttpResponseMessage GAZTzakatInstalmentDataResponse = await client.GetAsync(uri);

                    if (GAZTzakatInstalmentDataResponse != null)
                    {
                        if (GAZTzakatInstalmentDataResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            return null;
                        }

                        var _zakatInstalmentRequestData = GAZTzakatInstalmentDataResponse.Content.ReadAsStringAsync().Result;

                        ErrorObj statusHeader = JsonConvert.DeserializeObject<ErrorObj>(_zakatInstalmentRequestData);

                        if (statusHeader?.header?.status?.code != "E999999")
                        {
                            _ZakatInstalmentPlanRequestList = JsonConvert.DeserializeObject<OldZakatInstalmentPlanRequestListModel>(_zakatInstalmentRequestData);

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
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        public async static Task<OldZakatRequestDisplayModel> GAZTGetOldZakatRequestDisplayData(string fbnum, string status)
        {
            if (NetworkCheck.IsInternet())
            {
                OldZakatRequestDisplayModel _zakatRequestDisplayModel = new OldZakatRequestDisplayModel();
                string NewToken = string.Empty;
                try
                {
                    var summaryInputs = await GAZTGetOldZakatSummaryInputData(fbnum, status);

                    string euser = "00000000000000000000";
                    string fbguid = summaryInputs.d.formBundleGUID;
                    string lang = WebServiceManager.GetLangZParameterAREN();
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;

                    HttpClient client = new HttpClient();
                    string url = ZATCAConstants.ZakatOldInstalmentsSummarytUrl + App.LoginDataRetrieved.TIN + "&authenticationUser=" + euser + "&formBundleGUID=" + fbguid + "&language=" + lang;
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
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
                            _zakatRequestDisplayModel = JsonConvert.DeserializeObject<OldZakatRequestDisplayModel>(_zakatDisplayRequestData);

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
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }


        private async static Task<ZakatSummaryInputModel1> GAZTGetOldZakatSummaryInputData(string fbnum, string status)
        {
            ZakatSummaryInputModel1 _zakatSummaryInputModel = new ZakatSummaryInputModel1();



            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;

                string Newfbnum = fbnum;
                string euser = "00000000000000000000";
                if (fbnum != "")
                {

                    Newfbnum = "0" + fbnum;
                }

                try
                {
                    string fbtyp = "IPRF";

                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    HttpClient client = new HttpClient();
                    string lang = WebServiceManager.GetLangZParameterAREN();
                    String url = ZATCAConstants.GetOldZAKATSummaryInputURL + App.LoginDataRetrieved.TIN + "&authenticationUser1=" + euser + "&formBundleNumber=" + Newfbnum + "&formBundleType=" + fbtyp + "&language=" + lang + "&status=" + status;
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
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
                        String _ZakatSummaryInputData = _zakatSumamryInputResponse.Content.ReadAsStringAsync().Result;


                        ErrorObj statusHeader = JsonConvert.DeserializeObject<ErrorObj>(_ZakatSummaryInputData);

                        if (statusHeader?.header?.status?.code != "E999999")
                        {
                            _zakatSummaryInputModel = JsonConvert.DeserializeObject<ZakatSummaryInputModel1>(_ZakatSummaryInputData);
                            if (!string.IsNullOrEmpty(_ZakatSummaryInputData))
                            {
                                string errorMessage = WebServiceManager.PrepareErrorMessageByJson(_ZakatSummaryInputData);
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
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
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        public async static Task<OldZakatRequestDisplayModel> SaveOldZakatInstalmentData(OldZakatInstalmentPlanRequest _zakatInstalmentDetails)
        {


            if (NetworkCheck.IsInternet())
            {
                try
                {

                    OldZakatRequestDisplayModel _zakatResponseObject = new OldZakatRequestDisplayModel();
                    string LangZ = WebServiceManager.GetLangZParameterAREN();
                    string url = ZATCAConstants.GetOldZAKATPostdata;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    client.Timeout = TimeSpan.FromMinutes(10);

                    var serilized = JsonConvert.SerializeObject(_zakatInstalmentDetails);

                    var lang = UtilityManager.GetLanguageParameter();
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", App.IncomingChannel);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);

                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    var _zakatReturnDetailsDesponsestr = await res.Content.ReadAsStringAsync();


                    ErrorObj statusHeader = JsonConvert.DeserializeObject<ErrorObj>(_zakatReturnDetailsDesponsestr);

                    if (statusHeader?.header?.status?.code != "E999999")
                    {
                        _zakatResponseObject = JsonConvert.DeserializeObject<OldZakatRequestDisplayModel>(_zakatReturnDetailsDesponsestr);
                        if (!string.IsNullOrEmpty(_zakatReturnDetailsDesponsestr) && _zakatResponseObject.d == null)
                        {
                            string errorMessage = WebServiceManager.PrepareErrorMessageByJson(_zakatReturnDetailsDesponsestr);
                            throw new GAZTVATRegistrationInProcessException(errorMessage);
                        }
                    }
                    else
                    {
                        throw new GAZTNetworkConnectivityIssueException();
                    }
                    return _zakatResponseObject;
                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
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
