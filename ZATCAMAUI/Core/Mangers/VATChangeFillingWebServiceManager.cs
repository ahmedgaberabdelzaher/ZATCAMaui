using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.ChageFillingPeriodModel;
using ZATCAMAUI.Models.NewModelAPI;
using static ZATCAMAUI.Models.ErrorMessage;

namespace ZATCAMAUI.Core.Mangers
{

    public static class VATChangeFillingWebServiceManager
    {
        #region VAT Filling Change
        public async static Task<VATChangeFillingPeriodRequestModel> GAZTGetVATChangeFillingPeriodRequestData(string fbNum)
        {
            VATChangeFillingPeriodRequestModel _vATChangeFillingPeriodRequestModel = new VATChangeFillingPeriodRequestModel();

            string _VATChangeFillingPeriodRequestData = string.Empty;
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
                    String url = ZATCAConstants.VATChangeFillingPeriodGetURL + App.TP.TIN + "&language=" + lang;
                    HttpResponseMessage _vATChangeFillingPeriodGetResponse = await client.GetAsync(url);
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
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        _VATChangeFillingPeriodRequestData = _vATChangeFillingPeriodGetResponse.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(_VATChangeFillingPeriodRequestData))
                        {
                            _vATChangeFillingPeriodRequestModel = JsonConvert.DeserializeObject<VATChangeFillingPeriodRequestModel>(_VATChangeFillingPeriodRequestData);
                        }
                        if (!string.IsNullOrEmpty(_VATChangeFillingPeriodRequestData) && _vATChangeFillingPeriodRequestModel.d == null)
                        {
                            var message  = WebServiceManager.PrepareErrorMessageByJson(_VATChangeFillingPeriodRequestData);
                            throw new GAZTVATChangeFillingPeriodException(message);
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

            if (NetworkCheck.IsInternet())
            {

                string NewToken = string.Empty;
                try
                {
                    VATchangeFillingPeriodPostModel _VATchangeFillingPeriodPostModel = new VATchangeFillingPeriodPostModel();
                    _VATchangeFillingPeriodPostModel = request;
                    char lang = WebServiceManager.GetLangZParameter();
                    string LangZ = WebServiceManager.GetLangZParameterAREN();
                    string url = ZATCAConstants.VATChangeFillingPeriodPostURL;
                    var uri = new Uri(url);
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    HttpClient client = new HttpClient();
                    var serilized = JsonConvert.SerializeObject(_VATchangeFillingPeriodPostModel.d);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", LangZ);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);

                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = client.PostAsync(uri, contentPost).Result;
                    string _contractReleasesubmitResponse = res.Content.ReadAsStringAsync().Result;
                    _vATChangeFillingPeriodRequestModel = JsonConvert.DeserializeObject<VATChangeFillingPeriodRequestModel>(_contractReleasesubmitResponse);
                    if (!string.IsNullOrEmpty(_contractReleasesubmitResponse) && _vATChangeFillingPeriodRequestModel.d == null)
                    {
                        ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_contractReleasesubmitResponse);
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

            if (NetworkCheck.IsInternet())
            {

                string NewToken = string.Empty;
                try
                {
                    string lang = UtilityManager.GetLanguageParameter();
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
                    String url = ZATCAConstants.VATChangeFillingPeriodGetDropdownURL + gpart + "&language=" + lang;
                    var uri = new Uri(url);
                    HttpResponseMessage _vATRefillingGetDropdownResponse = client.GetAsync(uri).Result;
                  
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
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        String _VATRefillingRequestData = _vATRefillingGetDropdownResponse.Content.ReadAsStringAsync().Result;
                        
                        if (!string.IsNullOrEmpty(_VATRefillingRequestData))
                        {
                            if (string.IsNullOrEmpty(_VATRefillingRequestData) != true)
                            {
                                _vATRefillingDropdownModel = JsonConvert.DeserializeObject<VATRefillingDropdownModel>(_VATRefillingRequestData);
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
            if (NetworkCheck.IsInternet())
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
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", "EN");
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    var requestModel = new ValidateVATSignupTaxpayerRequest()
                    {
                        TIN = tin,
                        idType = string.Empty,
                        idNumber = string.Empty,
                        country = string.Empty,
                        passExpiryDate = string.Empty,
                        taxpayerBirthDate = string.Empty
                    };
                    var serilized = JsonConvert.SerializeObject(requestModel);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    String Url = ZATCAConstants.GAZTVATSignUpValidateId;
                    var uri = new Uri(Url);
                    HttpResponseMessage VATSignUpIdValidateObject = await client.PostAsync(uri, contentPost);
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
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
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
                catch (JsonReaderException )
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
            if (NetworkCheck.IsInternet())
            {
                VATSignUp vATSignUp = new VATSignUp();
                string IsIDTypeValidList = string.Empty;
                string NewToken = string.Empty;
                string SignUpCityList = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string lang = WebServiceManager.GetLangZParameterAREN();
                    string url = ZATCAConstants.GAZTVATSignUpValidateId;
                    ValidationRequest validationRequest = new ValidationRequest();
                    validationRequest.country = country;
                    validationRequest.TIN = tin;
                    validationRequest.idType = idType;
                    validationRequest.idNumber = idnum;
                    validationRequest.passExpiryDate = passExpdt;
                    validationRequest.taxpayerBirthDate = taxpDOB;
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;

                    // String url = Constants.GAZTVATSignUpValidateId + "(Tin='',Idtype='" + IDType + "',Idnum='" + IDNumber + "',Country='',PassExpDt='" + DBO + "',TaxpDob='" + DBO + "')?sap-language=" + lang + "&$format=json&saml2=enabled";
                    var uri = new Uri(url);
                    //  HttpClient client = new HttpClient(crmSignUphttpClientHandler);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    var serilized = JsonConvert.SerializeObject(validationRequest);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage VATSignUpIdValidateObject = await client.PostAsync(url, contentPost);
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
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
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
                                string WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                _validateIDResponse.errorMessage = errorMessage;
                            }
                        }
                    }
                    return _validateIDResponse;
                }
                catch (JsonReaderException)
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

        public async static Task<VATChangeFillingListModel> GAZTGetVATChangeFillingList(string gpart)
        {
            VATChangeFillingListModel _vATChangeFillingListModel = new VATChangeFillingListModel();

            if (NetworkCheck.IsInternet())
            {

                string NewToken = string.Empty;
                try
                {
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    string taxType = "VT";
                    string lang = UtilityManager.GetLanguageParameter();
                    //string lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    //String url = Constants.VATChangeFillingListURL + "TaxType='" + taxType + "',AudTin='" + "',Gpart='" + gpart + "',Lang='" + lang + "'," +
                    //  "UserTin='" + "')?&$expand=ASSLISTSet,STATUSSet,REQTYPSet&$format=json";
                    String url = ZATCAConstants.VATChangeFillingListURL + gpart + "&language=" + lang;
                    var uri = new Uri(url);
                    HttpResponseMessage _vatChangeFillingListResponse = client.GetAsync(uri).Result;
                    //HttpResponseMessage _vatChangeFillingListResponse = await GetServiceManager.MakeGetAPICall(url, false, "");

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
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }

                        String _vatChangeFillingListData = _vatChangeFillingListResponse.Content.ReadAsStringAsync().Result;
                        //_vATChangeFillingListModel = JObject.Parse(_vatChangeFillingListData)["d"].ToString();

                        //if (!string.IsNullOrEmpty(_vatChangeFillingListData) && _vATChangeFillingListModel.d == null)
                        //{

                        if (!string.IsNullOrEmpty(_vatChangeFillingListData))
                        {
                            // _vatChangeFillingListData = JObject.Parse(_vatChangeFillingListData)["data"].ToString();
                            if (string.IsNullOrEmpty(_vatChangeFillingListData) != true)
                            {
                                _vATChangeFillingListModel = JsonConvert.DeserializeObject<VATChangeFillingListModel>(_vatChangeFillingListData);
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

            if (NetworkCheck.IsInternet())
            {

                try
                {
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    string NewToken = string.Empty;
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

                    String url = ZATCAConstants.VATChangeFillingSummaryURL + App.TP.TIN + "&language=" + lang + "&formBundleNumber=" + fbnum;

                    HttpResponseMessage _vatChangeFillingSumamryResponse = await client.GetAsync(url);
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
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        String _vatChangeFillingSummaryData = _vatChangeFillingSumamryResponse.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(_vatChangeFillingSummaryData))
                        {
                            _vATChangeFillingSummaryModel = JsonConvert.DeserializeObject<VATChangeFillingSummaryModel>(_vatChangeFillingSummaryData);
                        }
                        if (!string.IsNullOrEmpty(_vatChangeFillingSummaryData) && _vATChangeFillingSummaryModel.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_vatChangeFillingSummaryData);
                            if (errorMesg != null && errorMesg.header != null && errorMesg.header.moreInformation != null && errorMesg.header.moreInformation.errorDetails != null && errorMesg.header.moreInformation.errorDetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.header.moreInformation.errorDetails[0].message;
                                errorMessage += errorMesg.header.moreInformation.errorDetails[1].message;
                                string WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
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
