using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Models.ZakatObjectionsModel;
using static ZATCAMAUI.Models.ErrorMessage;

namespace ZATCAMAUI.Core.Mangers
{

    public static class ZAKATObjectionsWebServiceManager
    {
        #region ZAKATObjections
        public async static Task<ZakatObjectionListModel> GAZTGetZAKATObjectionList()
        {
            ZakatObjectionListModel _ZAKATObjectionList = new ZakatObjectionListModel();

            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {

                    var lang = UtilityManager.GetLanguageParameter();
                    String url = ZATCAConstants.GetZAKATObjectionListURL + App.TP.TIN + "&language=" + lang;
                    HttpClient client = new HttpClient();
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfo>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfo>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfo>().Model;

                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);

                    var uri = new Uri(url);
                    HttpResponseMessage response = await client.GetAsync(uri);
                    if (response != null)
                    {
                        if (response.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = response.Headers;
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

                        String __ZAKATObjectionListData = response.Content.ReadAsStringAsync().Result;
                        __ZAKATObjectionListData = JObject.Parse(__ZAKATObjectionListData).ToString();
                        _ZAKATObjectionList = JsonConvert.DeserializeObject<ZakatObjectionListModel>(__ZAKATObjectionListData);
                        if (!string.IsNullOrEmpty(__ZAKATObjectionListData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__ZAKATObjectionListData);
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
                    return _ZAKATObjectionList;
                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
                    App.IsSessionExpired = true;
                    return null;
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        public async static Task<ZAKATObjectionCreateNewModel> GAZTGetZAKATObjectionCreateNew()
        {
            ZAKATObjectionCreateNewModel _ZAKATObjectionCreateNew = new ZAKATObjectionCreateNewModel();

            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    string strEuser1 = "00000000000000000000";
                    string strFbtyp = "ZNOB";
                    string lang = WebServiceManager.GetLangZParameterAREN();
                    String url = ZATCAConstants.GetZAKATObjectionCreateNewURL + strEuser1 + "&formBundleGUID=" + "" + "&formBundleNumber=" + "" + "&formBundleType=" + strFbtyp + "&TIN=" + App.TP.TIN + "&language=" + lang + "&periodkey=" + "" + "status=" + "";
                    HttpClient client = new HttpClient();
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfo>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfo>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfo>().Model;

                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    var uri = new Uri(url);
                    HttpResponseMessage _ZAKATObjectionCreateNewResponse = await client.GetAsync(uri);
                    if (_ZAKATObjectionCreateNewResponse != null)
                    {
                        if (_ZAKATObjectionCreateNewResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _ZAKATObjectionCreateNewResponse.Headers;
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
                        string __ZAKATObjectionCreateNewData = _ZAKATObjectionCreateNewResponse.Content.ReadAsStringAsync().Result;
                        _ZAKATObjectionCreateNew = JsonConvert.DeserializeObject<ZAKATObjectionCreateNewModel>(__ZAKATObjectionCreateNewData);
                        if (!string.IsNullOrEmpty(__ZAKATObjectionCreateNewData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__ZAKATObjectionCreateNewData);
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
                    return _ZAKATObjectionCreateNew;
                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
                    App.IsSessionExpired = true;
                    return null;
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        public async static Task<ZAKATObjectionDetailsByReferenceNumberModel> GAZTGetZAKATObjectionDetailsByReferenceNumber(string inputSearch)
        {
            ZAKATObjectionDetailsByReferenceNumberModel _ZAKATObjectionDetailsByReferenceNumber = new ZAKATObjectionDetailsByReferenceNumberModel();

            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    string strTaxPayer = App.LoginDataRetrieved.TIN;
                    string strEuser = "00000000000000000000";
                    string strFbnumz = inputSearch;
                    string strSectp = "C";
                    string strAud = "X";
                    char lang = WebServiceManager.GetLangZParameter();
                    string url = ZATCAConstants.GetZAKATObjectionDetailsByReferenceNumberURL + "" +
                        "Taxpayerz='" + strTaxPayer + "'," +
                        "Fbnumz='" + strFbnumz + "'," +
                        "Euser='" + strEuser + "'," +
                        "Aud='" + strAud + "'," +
                        "Sectp='" + strSectp + "')";
                    HttpResponseMessage _ZAKATObjectionDetailsByReferenceNumberResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
                    if (_ZAKATObjectionDetailsByReferenceNumberResponse != null)
                    {
                        if (_ZAKATObjectionDetailsByReferenceNumberResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _ZAKATObjectionDetailsByReferenceNumberResponse.Headers;
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
                        string __ZAKATObjectionDetailsByReferenceNumberData = _ZAKATObjectionDetailsByReferenceNumberResponse.Content.ReadAsStringAsync().Result;
                        _ZAKATObjectionDetailsByReferenceNumber = JsonConvert.DeserializeObject<ZAKATObjectionDetailsByReferenceNumberModel>(__ZAKATObjectionDetailsByReferenceNumberData);
                        if (!string.IsNullOrEmpty(__ZAKATObjectionDetailsByReferenceNumberData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__ZAKATObjectionDetailsByReferenceNumberData);
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
                    return _ZAKATObjectionDetailsByReferenceNumber;
                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
                }
                catch (Exception ex)
                {
                               
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
                    App.IsSessionExpired = true;
                    return null;
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        public async static Task<ZAKATObjectionDetailsToAmendReturnModel> GAZTGetZAKATObjectionDetailsToAmendReturn()
        {
            ZAKATObjectionDetailsToAmendReturnModel _ZAKATObjectionDetailsToAmendReturn = new ZAKATObjectionDetailsToAmendReturnModel();

            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    string strFbnum = "27000008586";
                    string strRefnum = "26000004637";
                    char lang = WebServiceManager.GetLangZParameter();
                    string url = ZATCAConstants.GetZAKATObjectionDetailsToAmendReturnURL + "" +
                        "Fbnum='" + strFbnum + "'," +
                        "Refnum='" + strRefnum + "')";
                    HttpResponseMessage _ZAKATObjectionDetailsToAmendReturnResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
                    if (_ZAKATObjectionDetailsToAmendReturnResponse != null)
                    {
                        if (_ZAKATObjectionDetailsToAmendReturnResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _ZAKATObjectionDetailsToAmendReturnResponse.Headers;
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
                        string __ZAKATObjectionDetailsToAmendReturnData = _ZAKATObjectionDetailsToAmendReturnResponse.Content.ReadAsStringAsync().Result;
                        _ZAKATObjectionDetailsToAmendReturn = JsonConvert.DeserializeObject<ZAKATObjectionDetailsToAmendReturnModel>(__ZAKATObjectionDetailsToAmendReturnData);
                        if (!string.IsNullOrEmpty(__ZAKATObjectionDetailsToAmendReturnData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__ZAKATObjectionDetailsToAmendReturnData);
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
                    return _ZAKATObjectionDetailsToAmendReturn;
                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
                    App.IsSessionExpired = true;
                    return null;
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        public async static Task<ZAKATObjectionAmendReturnAndCloseModel> GAZTGetZAKATObjectionAmendReturnAndClose()
        {
            ZAKATObjectionAmendReturnAndCloseModel _ZAKATObjectionAmendReturnAndClose = new ZAKATObjectionAmendReturnAndCloseModel();

            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    string strFbnum = "26000004525";
                    string strSectp = "C";
                    string strBetrw = "65000.00d";
                    char lang = WebServiceManager.GetLangZParameter();
                    string url = ZATCAConstants.GetZAKATObjectionAmendReturnAndCloseURL + "" +
                        "Fbnum='" + strFbnum + "'," +
                        "Sectp='" + strSectp + "'," +
                        "Betrw='" + strBetrw + "')";
                    HttpResponseMessage _ZAKATObjectionAmendReturnAndCloseResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
                    if (_ZAKATObjectionAmendReturnAndCloseResponse != null)
                    {
                        if (_ZAKATObjectionAmendReturnAndCloseResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _ZAKATObjectionAmendReturnAndCloseResponse.Headers;
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
                        string __ZAKATObjectionAmendReturnAndCloseData = _ZAKATObjectionAmendReturnAndCloseResponse.Content.ReadAsStringAsync().Result;
                        _ZAKATObjectionAmendReturnAndClose = JsonConvert.DeserializeObject<ZAKATObjectionAmendReturnAndCloseModel>(__ZAKATObjectionAmendReturnAndCloseData);
                        if (!string.IsNullOrEmpty(__ZAKATObjectionAmendReturnAndCloseData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__ZAKATObjectionAmendReturnAndCloseData);
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
                    return _ZAKATObjectionAmendReturnAndClose;
                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
                    App.IsSessionExpired = true;

                


                    App.IsSessionExpired = true;
                    return null;
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        public async static Task<ZAKATObjectionOnPaymentMethodSelectionModel> GAZTGetZAKATObjectionOnPaymentMethodSelection()
        {
            ZAKATObjectionOnPaymentMethodSelectionModel _ZAKATObjectionOnPaymentMethodSelection = new ZAKATObjectionOnPaymentMethodSelectionModel();

            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    string strFbnum = "26000004533";
                    string strSectp = "C";
                    string strRevam = "20000.00d";
                    string strDisam = "30000.00d";
                    char lang = WebServiceManager.GetLangZParameter();
                    string url = ZATCAConstants.GetZAKATObjectionOnPaymentMethodSelectionURL + "" +
                        "Revam='" + strRevam + "'," +
                        "Fbnum='" + strFbnum + "'," +
                        "Disam='" + strDisam + "'," +
                        "Sectp='" + strSectp + "')";
                    HttpResponseMessage _ZAKATObjectionOnPaymentMethodSelectionResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
                    if (_ZAKATObjectionOnPaymentMethodSelectionResponse != null)
                    {
                        if (_ZAKATObjectionOnPaymentMethodSelectionResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _ZAKATObjectionOnPaymentMethodSelectionResponse.Headers;
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
                        string __ZAKATObjectionOnPaymentMethodSelectionData = _ZAKATObjectionOnPaymentMethodSelectionResponse.Content.ReadAsStringAsync().Result;
                        _ZAKATObjectionOnPaymentMethodSelection = JsonConvert.DeserializeObject<ZAKATObjectionOnPaymentMethodSelectionModel>(__ZAKATObjectionOnPaymentMethodSelectionData);
                        if (!string.IsNullOrEmpty(__ZAKATObjectionOnPaymentMethodSelectionData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__ZAKATObjectionOnPaymentMethodSelectionData);
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
                    return _ZAKATObjectionOnPaymentMethodSelection;
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

        public async static Task<ZAKATObjectionApplicationDetailsIfStatusIP017Model> GAZTGetZAKATObjectionApplicationDetailsIfStatusIP017()
        {
            ZAKATObjectionApplicationDetailsIfStatusIP017Model _ZAKATObjectionApplicationDetailsIfStatusIP017 = new ZAKATObjectionApplicationDetailsIfStatusIP017Model();

            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    string strFbnum = "26000004533";
                    string strEuser = "00000000000000000000";
                    char lang = WebServiceManager.GetLangZParameter();
                    string url = ZATCAConstants.GetZAKATObjectionApplicationDetailsIfStatusIP017URL + "" +
                        "Fbnum='" + strFbnum + "'," +
                        "Euser='" + strEuser + "')";
                    HttpResponseMessage _ZAKATObjectionApplicationDetailsIfStatusIP017Response = await GetServiceManager.MakeGetAPICall(url, false, "");
                    if (_ZAKATObjectionApplicationDetailsIfStatusIP017Response != null)
                    {
                        if (_ZAKATObjectionApplicationDetailsIfStatusIP017Response.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _ZAKATObjectionApplicationDetailsIfStatusIP017Response.Headers;
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
                        string __ZAKATObjectionApplicationDetailsIfStatusIP017Data = _ZAKATObjectionApplicationDetailsIfStatusIP017Response.Content.ReadAsStringAsync().Result;
                        _ZAKATObjectionApplicationDetailsIfStatusIP017 = JsonConvert.DeserializeObject<ZAKATObjectionApplicationDetailsIfStatusIP017Model>(__ZAKATObjectionApplicationDetailsIfStatusIP017Data);
                        if (!string.IsNullOrEmpty(__ZAKATObjectionApplicationDetailsIfStatusIP017Data))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__ZAKATObjectionApplicationDetailsIfStatusIP017Data);
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
                    return _ZAKATObjectionApplicationDetailsIfStatusIP017;
                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
                    App.IsSessionExpired = true;
                    return null;
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        public async static Task<ZAKATObjectionGenerateSADADNumberModel> GAZTGetZAKATObjectionGenerateSADADNumber()
        {
            ZAKATObjectionGenerateSADADNumberModel _ZAKATObjectionGenerateSADADNumber = new ZAKATObjectionGenerateSADADNumberModel();

            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    string strFbnum = "26000004533";
                    char lang = WebServiceManager.GetLangZParameter();
                    string url = ZATCAConstants.GetZAKATObjectionGenerateSADADNumberURL + "" +
                        "Fbnum='" + strFbnum + "')";
                    HttpResponseMessage _ZAKATObjectionGenerateSADADNumberResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
                    if (_ZAKATObjectionGenerateSADADNumberResponse != null)
                    {
                        if (_ZAKATObjectionGenerateSADADNumberResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _ZAKATObjectionGenerateSADADNumberResponse.Headers;
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
                        string __ZAKATObjectionGenerateSADADNumberData = _ZAKATObjectionGenerateSADADNumberResponse.Content.ReadAsStringAsync().Result;
                        _ZAKATObjectionGenerateSADADNumber = JsonConvert.DeserializeObject<ZAKATObjectionGenerateSADADNumberModel>(__ZAKATObjectionGenerateSADADNumberData);
                        if (!string.IsNullOrEmpty(__ZAKATObjectionGenerateSADADNumberData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__ZAKATObjectionGenerateSADADNumberData);
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
                    return _ZAKATObjectionGenerateSADADNumber;
                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
                    App.IsSessionExpired = true;
                    return null;
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        public async static Task<ZAKATObjectionBusyIndicatorModel> GAZTGetZAKATObjectionBusyIndicator()
        {
            ZAKATObjectionBusyIndicatorModel _ZAKATObjectionBusyIndicator = new ZAKATObjectionBusyIndicatorModel();

            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    string strPartner = "3311626033";
                    char lang = WebServiceManager.GetLangZParameter();
                    string url = ZATCAConstants.GetZAKATObjectionBusyIndicatorURL + "" +
                        "Partner='" + strPartner + "')";
                    HttpResponseMessage _ZAKATObjectionBusyIndicatorResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
                    if (_ZAKATObjectionBusyIndicatorResponse != null)
                    {
                        if (_ZAKATObjectionBusyIndicatorResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _ZAKATObjectionBusyIndicatorResponse.Headers;
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
                        string __ZAKATObjectionBusyIndicatorData = _ZAKATObjectionBusyIndicatorResponse.Content.ReadAsStringAsync().Result;
                        _ZAKATObjectionBusyIndicator = JsonConvert.DeserializeObject<ZAKATObjectionBusyIndicatorModel>(__ZAKATObjectionBusyIndicatorData);
                        if (!string.IsNullOrEmpty(__ZAKATObjectionBusyIndicatorData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__ZAKATObjectionBusyIndicatorData);
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
                    return _ZAKATObjectionBusyIndicator;
                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
                    App.IsSessionExpired = true;
                    return null;
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        public static async Task<ZakatBankListModel> GAZTGetBankList()
        {
            ZakatBankListModel zakatBankList = new ZakatBankListModel();
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    String url = ZATCAConstants.ZakatObjectionLoadBankListURL + UtilityManager.GetLanguageParameter();
                    HttpResponseMessage _zakatBankListResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
                    if (_zakatBankListResponse != null)
                    {
                        if (_zakatBankListResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _zakatBankListResponse.Headers;
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
                        string __zakatBankListData = _zakatBankListResponse.Content.ReadAsStringAsync().Result;
                        zakatBankList = JsonConvert.DeserializeObject<ZakatBankListModel>(__zakatBankListData);
                        if (!string.IsNullOrEmpty(__zakatBankListData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__zakatBankListData);
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
                    return zakatBankList;
                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
                    App.IsSessionExpired = true;
                    return null;
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        public static async Task<ZakatBankListModel> GAZTGetIntialLoadData(string fbguid)
        {
            ZakatBankListModel zakatBankList = new ZakatBankListModel();
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    string url = ZATCAConstants.ZakatObjectionIntialLoadURL + "Taxpayerz='" + "',Fbnumz='" + "',Langz='" + "'," +
"Auditorz='" + "',Euser='" + 00000000000000000000 + "',Fbguid='" + fbguid + "')?&$expand=ZNOB_ObjSet,Off_notesSet,AttDetSet&$format=json";
                    HttpResponseMessage _zakatBankListResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
                    if (_zakatBankListResponse != null)
                    {
                        if (_zakatBankListResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _zakatBankListResponse.Headers;
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
                        string __zakatBankListData = _zakatBankListResponse.Content.ReadAsStringAsync().Result;
                        zakatBankList = JsonConvert.DeserializeObject<ZakatBankListModel>(__zakatBankListData);
                        if (!string.IsNullOrEmpty(__zakatBankListData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__zakatBankListData);
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
                    return zakatBankList;
                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
                    App.IsSessionExpired = true;
                    return null;
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        public static async Task<ZakatBankListModel> GAZTGetZakatRemoveObjection(string retFbnum, string objFbnum)
        {
            ZakatBankListModel zakatBankList = new ZakatBankListModel();
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    string url = ZATCAConstants.ZakatObjectionRemoveobjectionURL + "RetFbnum='" + retFbnum + "',ObjFbnum='" + objFbnum + "')?$format=json";
                    HttpResponseMessage _zakatBankListResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
                    if (_zakatBankListResponse != null)
                    {
                        if (_zakatBankListResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _zakatBankListResponse.Headers;
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
                        string __zakatBankListData = _zakatBankListResponse.Content.ReadAsStringAsync().Result;
                        zakatBankList = JsonConvert.DeserializeObject<ZakatBankListModel>(__zakatBankListData);
                        if (!string.IsNullOrEmpty(__zakatBankListData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__zakatBankListData);
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
                    return zakatBankList;
                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
                    App.IsSessionExpired = true;
                    return null;
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        public static async Task<ZakatBankListModel> GAZTGetZakatRemoveObjectionACK(string retFbnum, string objFbnum)
        {
            ZakatBankListModel zakatBankList = new ZakatBankListModel();
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    String url = ZATCAConstants.ZakatObjectionRemoveObjAckURL + retFbnum + "&objectionFormBundleNumber=" + objFbnum;
                    HttpResponseMessage _zakatBankListResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
                    if (_zakatBankListResponse != null)
                    {
                        if (_zakatBankListResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _zakatBankListResponse.Headers;
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
                        string __zakatBankListData = _zakatBankListResponse.Content.ReadAsStringAsync().Result;
                        zakatBankList = JsonConvert.DeserializeObject<ZakatBankListModel>(__zakatBankListData);
                        if (!string.IsNullOrEmpty(__zakatBankListData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__zakatBankListData);
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
                    return zakatBankList;
                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
