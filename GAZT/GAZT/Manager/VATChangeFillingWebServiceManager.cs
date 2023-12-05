using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EGAZT.Models.ChageFillingPeriodModel;
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
    public static class VATChangeFillingWebServiceManager
    {
        #region VAT Filling Change
        public async static Task<VATChangeFillingPeriodRequestModel> GAZTGetVATChangeFillingPeriodRequestData(string fbNum)
        {
            VATChangeFillingPeriodRequestModel _vATChangeFillingPeriodRequestModel = new VATChangeFillingPeriodRequestModel();

            if (CrossConnectivity.Current.IsConnected)
            {

                string NewToken = string.Empty;
                try
                {
                    Char lang = WebServiceManager.GetLangZParameter();
                    String url = Constants.VATChangeFillingPeriodGetURL + "Fbnumz='" + fbNum + "',PortalUsrz='" + "',Langz='" + lang + "',Operationz='" + "'," +
                  "Gpartz='" + App.LoginDataRetrieved.TIN + "',Euser='" + "',UserTypz='" + "',Fbguid='" + "')?$expand=EffDateSet,UI_BTNSet,NOTESSet,ATTACHSet,ATT_TYPSet,QuesListSet&$format=json";
                    HttpResponseMessage _vATChangeFillingPeriodGetResponse = await GetServiceManager.MakeGetAPICall(url, false, "");



                    if (_vATChangeFillingPeriodGetResponse != null)
                    {
                        if (_vATChangeFillingPeriodGetResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _vATChangeFillingPeriodGetResponse.Headers;
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
                        String _VATChangeFillingPeriodRequestData = _vATChangeFillingPeriodGetResponse.Content.ReadAsStringAsync().Result;
                        _vATChangeFillingPeriodRequestModel = JsonConvert.DeserializeObject<VATChangeFillingPeriodRequestModel>(_VATChangeFillingPeriodRequestData);

                        if (!string.IsNullOrEmpty(_VATChangeFillingPeriodRequestData) && _vATChangeFillingPeriodRequestModel.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_VATChangeFillingPeriodRequestData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                throw new GAZTVATChangeFillingPeriodException(errorMessage);
                            }
                        }
                    }
                    return _vATChangeFillingPeriodRequestModel;
                }
                catch (GAZTVATChangeFillingPeriodException ex)
                {
                    throw new GAZTVATChangeFillingPeriodException(ex.Message);
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

        public async static Task<VATChangeFillingPeriodRequestModel> GAZTPostVATChangeFillingPeriodData(VATchangeFillingPeriodPostModel request)
        {
            VATChangeFillingPeriodRequestModel _vATChangeFillingPeriodRequestModel = new VATChangeFillingPeriodRequestModel();

            if (CrossConnectivity.Current.IsConnected)
            {

                string NewToken = string.Empty;
                try
                {
                    VATchangeFillingPeriodPostModel _VATchangeFillingPeriodPostModel = new VATchangeFillingPeriodPostModel();
                    _VATchangeFillingPeriodPostModel = request;
                    Char lang = WebServiceManager.GetLangZParameter();
                    string LangZ = WebServiceManager.GetLangZParameterAREN();
                    String url = Constants.VATChangeFillingPeriodPostURL;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var serilized = JsonConvert.SerializeObject(_VATchangeFillingPeriodPostModel.d);
                    client.DefaultRequestHeaders.Add("Token", App.Token);
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    HttpResponseMessage res = client.PostAsync(uri, contentPost).Result;
                    var _contractReleasesubmitResponse = res.Content.ReadAsStringAsync().Result;
                    _vATChangeFillingPeriodRequestModel = JsonConvert.DeserializeObject<VATChangeFillingPeriodRequestModel>(_contractReleasesubmitResponse);
                    return _vATChangeFillingPeriodRequestModel;
                }
                catch (GAZTVATChangeFillingPeriodException ex)
                {
                    throw new GAZTVATChangeFillingPeriodException(ex.Message);
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

        public async static Task<VATRefillingDropdownModel> GAZTGetVATChangeFillingPeriodDropdownData(string gpart)
        {
            VATRefillingDropdownModel _vATRefillingDropdownModel = new VATRefillingDropdownModel();

            if (CrossConnectivity.Current.IsConnected)
            {

                string NewToken = string.Empty;
                try
                {

                    Char lang = WebServiceManager.GetLangZParameter();
                    String url = Constants.VATChangeFillingPeriodGetDropdownURL + "Fbtypz='" + "',UserTypz='" + "',TransactionTypez='" + "',Lang='" + lang + "'," +
                     "Gpart='" + gpart + "',Status='" + "')?$expand=UI_BTNSet,ATT_TYPSet,EffDateSet&$format=json";
                    HttpResponseMessage _vATRefillingGetDropdownResponse = await GetServiceManager.MakeGetAPICall(url, false, "");



                    if (_vATRefillingGetDropdownResponse != null)
                    {
                        if (_vATRefillingGetDropdownResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _vATRefillingGetDropdownResponse.Headers;
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
                        String _VATRefillingRequestData = _vATRefillingGetDropdownResponse.Content.ReadAsStringAsync().Result;
                        _vATRefillingDropdownModel = JsonConvert.DeserializeObject<VATRefillingDropdownModel>(_VATRefillingRequestData);

                        if (!string.IsNullOrEmpty(_VATRefillingRequestData) && _vATRefillingDropdownModel.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_VATRefillingRequestData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATChangeFillingPeriodException(errorMessage);
                            }
                        }
                    }
                    return _vATRefillingDropdownModel;
                }
                catch (GAZTVATChangeFillingPeriodException ex)
                {
                    throw new GAZTVATChangeFillingPeriodException(ex.Message);
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

        public async static Task<string> GAZTGetTInNumberData(string tin)
        {

            VATSignUp _validateIDResponse = new VATSignUp();
            if (CrossConnectivity.Current.IsConnected)
            {
                VATSignUp vATSignUp = new VATSignUp();
                string IsIDTypeValidList = string.Empty;
                string NewToken = string.Empty;
                string SignUpCityList = string.Empty;
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);

                    String Url = Constants.GAZTVATSignUpValidateId + "(Tin='" + tin + "',Idtype='" + string.Empty + "',Idnum='" + string.Empty + "',Country='',PassExpDt='',TaxpDob='" + string.Empty + "')?sap-language=" + lang + "&$format=json&saml2=enabled";
                    var uri = new Uri(Url);
                    HttpResponseMessage VATSignUpIdValidateObject = await client.GetAsync(uri);
                    if (VATSignUpIdValidateObject != null)
                    {
                        if (VATSignUpIdValidateObject.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = VATSignUpIdValidateObject.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
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
                        SignUpCityList = await VATSignUpIdValidateObject.Content.ReadAsStringAsync();
                    }
                    return SignUpCityList;
                }
                catch (JsonReaderException ex)
                {
                    throw new GAZTInvalidDataException();
                }
                catch (HttpRequestException ex)
                {
                    throw ex;
                }
                catch (GAZTSessionExpiredException gex)
                {
                    throw gex;
                }
                catch (GAZTException gex)
                {
                    throw gex;
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

        public async static Task<ValidateIDResponse> GAZTVATChangeFillingPeriodValidateIDnumber(string tin, string idType, string idnum, string country, string passExpdt, string taxpDOB)
        {

            ValidateIDResponse _validateIDResponse = new ValidateIDResponse();
            if (CrossConnectivity.Current.IsConnected)
            {
                VATSignUp vATSignUp = new VATSignUp();
                string IsIDTypeValidList = string.Empty;
                string NewToken = string.Empty;
                String SignUpCityList = string.Empty;
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);

                    String Url = Constants.GAZTVATSignUpValidateId + "(Tin='',Idtype='" + idType + "',Idnum='" + idnum + "',Country='',PassExpDt='',TaxpDob='" + taxpDOB + "')?sap-language=" + lang + "&$format=json&saml2=enabled";
                    var uri = new Uri(Url);
                    HttpResponseMessage VATSignUpIdValidateObject = await client.GetAsync(uri);
                    if (VATSignUpIdValidateObject != null)
                    {
                        if (VATSignUpIdValidateObject.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = VATSignUpIdValidateObject.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
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
                        SignUpCityList = await VATSignUpIdValidateObject.Content.ReadAsStringAsync();
                        _validateIDResponse = JsonConvert.DeserializeObject<ValidateIDResponse>(SignUpCityList);
                        if (!string.IsNullOrEmpty(SignUpCityList) && _validateIDResponse.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(SignUpCityList);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                _validateIDResponse.errorMessage = errorMessage;
                            }
                        }
                    }
                    return _validateIDResponse;
                }
                catch (JsonReaderException ex)
                {
                    throw new GAZTInvalidDataException();
                }
                catch (HttpRequestException ex)
                {
                    throw ex;
                }
                catch (GAZTSessionExpiredException gex)
                {
                    throw gex;
                }
                catch (GAZTException gex)
                {
                    throw gex;
                }
                catch (Exception)
                {
                    throw new GAZTNetworkConnectivityIssueException();
                }
                //catch (Exception)
                //{
                //    return null;
                //}
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        public async static Task<VATChangeFillingListModel> GAZTGetVATChangeFillingList(string gpart)
        {
            VATChangeFillingListModel _vATChangeFillingListModel = new VATChangeFillingListModel();

            if (CrossConnectivity.Current.IsConnected)
            {

                string NewToken = string.Empty;
                try
                {
                    string taxType = "VT";

                    Char lang = WebServiceManager.GetLangZParameter();
                    String url = Constants.VATChangeFillingListURL + "TaxType='" + taxType + "',AudTin='" + "',Gpart='" + gpart + "',Lang='" + lang + "'," +
                      "UserTin='" + "')?&$expand=ASSLISTSet,STATUSSet,REQTYPSet&$format=json";
                    HttpResponseMessage _vatChangeFillingListResponse = await GetServiceManager.MakeGetAPICall(url, false, "");



                    if (_vatChangeFillingListResponse != null)
                    {
                        if (_vatChangeFillingListResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _vatChangeFillingListResponse.Headers;
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
                        String _vatChangeFillingListData = _vatChangeFillingListResponse.Content.ReadAsStringAsync().Result;
                        _vATChangeFillingListModel = JsonConvert.DeserializeObject<VATChangeFillingListModel>(_vatChangeFillingListData);

                        if (!string.IsNullOrEmpty(_vatChangeFillingListData) && _vATChangeFillingListModel.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_vatChangeFillingListData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                throw new GAZTVATChangeFillingPeriodException(errorMessage);
                            }
                        }
                    }
                    return _vATChangeFillingListModel;
                }
                catch (GAZTVATChangeFillingPeriodException ex)
                {
                    throw new GAZTVATChangeFillingPeriodException(ex.Message);
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

        public async static Task<VATChangeFillingSummaryModel> GAZTGetVATChangeFillingSummary(string fbnum, string status)
        {
            VATChangeFillingSummaryModel _vATChangeFillingSummaryModel = new VATChangeFillingSummaryModel();

            if (CrossConnectivity.Current.IsConnected)
            {

                try
                {
                    string NewToken = string.Empty;
                    Char lang = WebServiceManager.GetLangZParameter();
                    string LangZAREN = WebServiceManager.GetLangZParameterAREN();
                    String url = Constants.VATChangeFillingSummaryURL + "Fbnumz='" + fbnum + "',PortalUsrz='" + "',Langz='" + lang + "'," +
                      "Operationz='" + "',Euser='" + "',Gpartz='" + App.LoginDataRetrieved.TIN + "',UserTypz='" + "',Fbguid='" + "')?&$expand=EffDateSet,UI_BTNSet,NOTESSet,ATTACHSet,ATT_TYPSet,QuesListSet&$format=json&sap-language=" + LangZAREN;
                    HttpResponseMessage _vatChangeFillingSumamryResponse = await GetServiceManager.MakeGetAPICall(url, false, "");



                    if (_vatChangeFillingSumamryResponse != null)
                    {
                        if (_vatChangeFillingSumamryResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _vatChangeFillingSumamryResponse.Headers;
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
                        String _vatChangeFillingSummaryData = _vatChangeFillingSumamryResponse.Content.ReadAsStringAsync().Result;
                        _vATChangeFillingSummaryModel = JsonConvert.DeserializeObject<VATChangeFillingSummaryModel>(_vatChangeFillingSummaryData);

                        if (!string.IsNullOrEmpty(_vatChangeFillingSummaryData) && _vATChangeFillingSummaryModel.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_vatChangeFillingSummaryData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                throw new GAZTVATChangeFillingPeriodException(errorMessage);
                            }
                        }
                    }
                    return _vATChangeFillingSummaryModel;
                }
                catch (GAZTVATChangeFillingPeriodException ex)
                {
                    throw new GAZTVATChangeFillingPeriodException(ex.Message);
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
