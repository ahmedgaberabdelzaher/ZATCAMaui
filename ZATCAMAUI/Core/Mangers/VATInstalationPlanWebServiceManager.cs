using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Models.VATInstalmentModels;
using static ZATCAMAUI.Models.ErrorMessage;
using static ZATCAMAUI.Models.VATInstalmentModels.RequestToVATInstallmentPlanDetails;

namespace ZATCAMAUI.Core.Mangers
{

    public class VATInstalationPlanWebServiceManager
    {
        #region VATInstalationPlan

        public static async Task<ReqVatInstalmentPlanResponse> GetRequestToVATInstalmentData()
        {
            ReqVatInstalmentPlanResponse _requestVATInstalmentPlan = new ReqVatInstalmentPlanResponse();

            if (NetworkCheck.IsInternet())
            {
                try
                {
                    string LangZAREN = WebServiceManager.GetLangZParameterAREN();
                    char LangZ = WebServiceManager.GetLangZParameter();
                    string lang = UtilityManager.GetLanguageParameter();
                    string taxType = "VT";
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfo>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfo>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfo>().Model;
                    // var lang = UtilityManager.GetLanguageParameter();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    String url = ZATCAConstants.GetReqVATInstalmentdata + App.LoginDataRetrieved.TIN + "&transactionType=" + taxType + "&language=" + lang;
                    //String url = Constants.GetReqVATInstalmentdata + "TaxType='" + taxType + "',AudTin='" + "',Gpart='" + App.LoginDataRetrieved.TIN + "',Lang='" + lang + "',UserTin='" + "')?&$expand=ASSLISTSet,STATUSSet,REQTYPSet&$format=json";
                    // HttpResponseMessage _requestVATInstalmentPlanresponse = await GetServiceManager.MakeGetAPICall(url, false, "");
                    var uri = new Uri(url);
                    HttpResponseMessage _requestVATInstalmentPlanresponse = await client.GetAsync(uri);
                    var detailJson = _requestVATInstalmentPlanresponse.Content.ReadAsStringAsync().Result;
                    _requestVATInstalmentPlan = JsonConvert.DeserializeObject<ReqVatInstalmentPlanResponse>(detailJson);

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
            return _requestVATInstalmentPlan;
        }

        public async static Task<VATInstalmentDetailsInputModel> GAZTGetFbGuidDetailsInputData(string fbguid, string fbnum, string gpart, string Status, string type)
        {
            VATInstalmentDetailsInputModel _InstalmentDetailsInputs = new VATInstalmentDetailsInputModel();

            if (NetworkCheck.IsInternet())
            {

                string NewToken = string.Empty;
                try
                {
                    string fbtyp = type;

                    var lang = UtilityManager.GetLanguageParameter();
                    string LangZAREN = WebServiceManager.GetLangZParameterAREN();

                    String url = ZATCAConstants.VATGetFormGUIDURL + "formBundleGUID=" + fbguid + "&formBundleNumber=" + fbnum + "&formBundleType=" + fbtyp + "&TIN=" + gpart + "&language=" + lang + "&status=" + Status;
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", "android");
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);

                    var uri = new Uri(url);
                    HttpResponseMessage _vATRefillingGetDropdownResponse = await client.GetAsync(uri);


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
                        string _VATRefillingRequestData = _vATRefillingGetDropdownResponse.Content.ReadAsStringAsync().Result;
                        _InstalmentDetailsInputs = JsonConvert.DeserializeObject<VATInstalmentDetailsInputModel>(_VATRefillingRequestData);



                        if (!string.IsNullOrEmpty(_VATRefillingRequestData) && _InstalmentDetailsInputs.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_VATRefillingRequestData);
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
                    return _InstalmentDetailsInputs;
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


        //Request To VAT Installment Plan Details
        public static async Task<RequestToVATInstallmentPlanDetails> GetRequestToVATInstalmentPlanDetails(string euser, string formGuid)
        {
            RequestToVATInstallmentPlanDetails _requestToVATInstallmentPlanDetails = new RequestToVATInstallmentPlanDetails();

            if (NetworkCheck.IsInternet())
            {
                try
                {
                    string LangZAREN = WebServiceManager.GetLangZParameterAREN();
                    char LangZ = WebServiceManager.GetLangZParameter();
                    string lang = UtilityManager.GetLanguageParameter();
                    string Euser = euser;
                    string FormGuid = formGuid;
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfo>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfo>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfo>().Model;
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    //var lang = UtilityManager.GetLanguageParameter();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    //String url = Constants.GetRequestToVATInstalmentPlanById
                    //  + "FormGuid='" + FormGuid + "',Euser='" + Euser + "',Gpartz=''," +
                    //  "Langz='" + lang + "',Officerz='" + "',PortalUsrz='" + "',TxnTpz='" + "')?&$expand=VTIASet,VTISSet,NOTESSet,ATTACHMENTSet,VTADSet&$format=json";
                    String url = ZATCAConstants.GetRequestToVATInstalmentPlanById
                        + App.TP.TIN + "&language=" + lang + "&formGUID=" + FormGuid;
                    // HttpResponseMessage _requestVATInstalmentPlanresponse = await GetServiceManager.MakeGetAPICall(url, false, "");
                    var uri = new Uri(url);
                    HttpResponseMessage _requestVATInstalmentPlanresponse = await client.GetAsync(uri);
                    var detailJson = _requestVATInstalmentPlanresponse.Content.ReadAsStringAsync().Result;
                    _requestToVATInstallmentPlanDetails = JsonConvert.DeserializeObject<RequestToVATInstallmentPlanDetails>(detailJson);

                    if (!string.IsNullOrEmpty(detailJson) && _requestToVATInstallmentPlanDetails.d == null)
                    {
                        ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
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
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
                }

                catch (Exception)
                {
                    return null;
                }

            }
            return _requestToVATInstallmentPlanDetails;
        }


        //Display Installment Agreement Schedule Plan
        public static async Task<DisplayInstallmentAgreementSchedulePlan> GetDisplayInstallmentAgreementSchedulePlan(string formGuid)
        {
            DisplayInstallmentAgreementSchedulePlan _displayInstallmentAgreementSchedulePlan = new DisplayInstallmentAgreementSchedulePlan();

            if (NetworkCheck.IsInternet())
            {
                try
                {
                    string LangZAREN = WebServiceManager.GetLangZParameterAREN();
                    char LangZ = WebServiceManager.GetLangZParameter();
                    string lang = UtilityManager.GetLanguageParameter();
                    string Euser = "";
                    string FormGuid = formGuid;
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfo>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfo>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfo>().Model;
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    //String url = Constants.GetDisplayInstallmentAgreementSchedule
                    //  + "FormGuid='" + FormGuid + "',Euser='" + Euser + "',Gpart=''," +
                    //  "Langz='" + lang + "',Opbel='" + "')?&$expand=VTIA_IADTSet,VTIA_IAHDSet&$format=json";
                    String url = ZATCAConstants.GetDisplayInstallmentAgreementSchedule + App.TP.TIN + "&formGUID=" + formGuid + "&language=" + lang;
                    var uri = new Uri(url);
                    HttpResponseMessage _requestVATInstalmentPlanresponse = await client.GetAsync(uri);

                    // HttpResponseMessage _requestVATInstalmentPlanresponse = await GetServiceManager.MakeGetAPICall(url, false, "");
                    var detailJson = _requestVATInstalmentPlanresponse.Content.ReadAsStringAsync().Result;

                    _displayInstallmentAgreementSchedulePlan = JsonConvert.DeserializeObject<DisplayInstallmentAgreementSchedulePlan>(detailJson);

                    if (!string.IsNullOrEmpty(detailJson) && _displayInstallmentAgreementSchedulePlan.d == null)
                    {
                        ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
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
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
                }

                catch (Exception)
                {
                    return null;
                }

            }
            return _displayInstallmentAgreementSchedulePlan;
        }


        //Instalment Schedule details
        public static async Task<VATInstalmentScheduleDetailsModel> GetDisplayInstallmentScheduleDetails(string opbel, string fromGuid, string eUser)
        {
            VATInstalmentScheduleDetailsModel _vATInstalmentScheduleDetailsModel = new VATInstalmentScheduleDetailsModel();

            if (NetworkCheck.IsInternet())
            {
                try
                {
                    string LangZAREN = WebServiceManager.GetLangZParameterAREN();
                    char LangZ = WebServiceManager.GetLangZParameter();
                    string lang = UtilityManager.GetLanguageParameter();
                    string Euser = eUser;
                    string FormGuid = fromGuid;
                    string Opbel = opbel;
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfo>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfo>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfo>().Model;
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    //String url = Constants.GetInstallmentSchedule
                    //    + "FormGuid='" + FormGuid + "',Euser='" + Euser + "',Gpart=''," +
                    //    "Langz='E',Opbel='" + Opbel + "')?$expand=VTIA_IADTSet,VTIA_IAHDSet&$format=json";
                    String url = ZATCAConstants.GetDisplayInstallmentAgreementSchedule + App.TP.TIN + "&formGUID=" + fromGuid + "&language=" + lang + "&documentNumber=" + opbel + "&serialNumber=" + eUser;
                    var uri = new Uri(url);
                    HttpResponseMessage _requestVATInstalmentPlanresponse = await client.GetAsync(uri);
                    // HttpResponseMessage _requestVATInstalmentPlanresponse = await GetServiceManager.MakeGetAPICall(url, false, "");
                    var detailJson = _requestVATInstalmentPlanresponse.Content.ReadAsStringAsync().Result;

                    _vATInstalmentScheduleDetailsModel = JsonConvert.DeserializeObject<VATInstalmentScheduleDetailsModel>(detailJson);

                    if (!string.IsNullOrEmpty(detailJson) && _vATInstalmentScheduleDetailsModel.d == null)
                    {
                        ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
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
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
                }

                catch (Exception)
                {
                    return null;
                }

            }
            return _vATInstalmentScheduleDetailsModel;
        }


        public async static Task<VatInstalmentPlanResponse> GAZTGetVATInstalmentData(string formGuid, string euser)
        {
            if (NetworkCheck.IsInternet())
            {
                VatInstalmentPlanResponse vATInstalmentDetails = new VatInstalmentPlanResponse();
                string NewToken = string.Empty;
                try
                {
                    var lang = UtilityManager.GetLanguageParameter();
                    String url = "";

                    if (formGuid == "")
                    {

                        url = ZATCAConstants.GetVATInstalmentdata + App.LoginDataRetrieved.TIN + "&formGUID=" + formGuid + "&language=" + lang;
                    }
                    else
                    {

                        url = ZATCAConstants.GetVATInstalmentdata + App.LoginDataRetrieved.TIN + "&formGUID=" + formGuid + "&language=" + lang;
                    }
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", "android");
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);

                    var uri = new Uri(url);
                    HttpResponseMessage GAZTVATInstalmentDataResponse = await client.GetAsync(uri);
                    if (GAZTVATInstalmentDataResponse != null)
                    {
                        if (GAZTVATInstalmentDataResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTVATInstalmentDataResponse.Headers;
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
                        string vatInstalmentData = GAZTVATInstalmentDataResponse.Content.ReadAsStringAsync().Result;
                        vATInstalmentDetails = JsonConvert.DeserializeObject<VatInstalmentPlanResponse>(vatInstalmentData);
                        if (!string.IsNullOrEmpty(vatInstalmentData) && vATInstalmentDetails.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(vatInstalmentData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                string WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                                //throw new GAZTErrorException(errorMessage);

                            }
                        }
                    }
                    return vATInstalmentDetails;
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

        public static async Task<VatInstalmentPlanResponse> SaveVATInstalmentData(VatInstalmentPlanRequest _vATInstalment)
        {

            if (NetworkCheck.IsInternet())
            {
                try
                {

                    VatInstalmentPlanResponse _vatResponseObject = new VatInstalmentPlanResponse();
                    //string LangZ = WebServiceManager.GetLangZParameterAREN();
                    var lang = UtilityManager.GetLanguageParameter();

                    String url = ZATCAConstants.SaveVATInstalmentdata;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfo>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfo>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfo>().Model;

                    if (_vATInstalment.Operationz == "01" || _vATInstalment.Operationz == "05" || _vATInstalment.Operationz == "04" || _vATInstalment.Operationz == "58")
                    {

                        var serilized = JsonConvert.SerializeObject(_vATInstalment);
                        // client.DefaultRequestHeaders.Add("Token", "123");
                        client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                        client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                        client.DefaultRequestHeaders.Add("Accept", "application/json");
                        client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                        client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                        client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                        client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                        client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                        client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                        client.DefaultRequestHeaders.Add("Authorization", App.Token);


                        HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                        HttpResponseMessage res = client.PostAsync(uri, contentPost).Result;
                        var _zakatReturnDetailsDesponsestr = res.Content.ReadAsStringAsync().Result;
                        _vatResponseObject = JsonConvert.DeserializeObject<VatInstalmentPlanResponse>(_zakatReturnDetailsDesponsestr);
                        if (_vatResponseObject.d == null && _vatResponseObject.result != null)
                        {
                            _vatResponseObject.d = _vatResponseObject.result;
                        }
                        if (_vatResponseObject == null || _vatResponseObject.d == null)
                        {
                            WebServiceManager.ErrorMessage = string.Empty;
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatReturnDetailsDesponsestr);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                WebServiceManager.ErrorMessage = errorMesg.error.innererror.errordetails[0].message;
                                throw new GAZTVATRegistrationInProcessException(WebServiceManager.ErrorMessage);
                            }
                        }
                        return _vatResponseObject;
                    }
                    else
                    {
                        var serilized = JsonConvert.SerializeObject(_vATInstalment);
                        client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                        client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                        client.DefaultRequestHeaders.Add("Accept", "application/json");
                        client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                        client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                        client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                        client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                        client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                        client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                        client.DefaultRequestHeaders.Add("Authorization", App.Token);

                        HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                        HttpResponseMessage res = client.PostAsync(uri, contentPost).Result;
                        var _zakatReturnDetailsDesponsestr = res.Content.ReadAsStringAsync().Result;
                        _vatResponseObject = JsonConvert.DeserializeObject<VatInstalmentPlanResponse>(_zakatReturnDetailsDesponsestr);
                        if (_vatResponseObject.d == null && _vatResponseObject.result != null)
                        {
                            _vatResponseObject.d = _vatResponseObject.result;
                        }
                        if (_vatResponseObject == null || _vatResponseObject.d == null)
                        {
                            WebServiceManager.ErrorMessage = string.Empty;
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatReturnDetailsDesponsestr);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                WebServiceManager.ErrorMessage = errorMesg.error.innererror.errordetails[0].message;
                                throw new GAZTVATRegistrationInProcessException(WebServiceManager.ErrorMessage);

                            }
                        }
                        return _vatResponseObject;
                    }



                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
                    return null;
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }


        }
        public async static Task<VtiaBtnSetModel> GAZTGetVATRevokeBtnSet(string Fbtypz, string Fbnum, string Formproc, string Status, string TxnTp)
        {
            if (NetworkCheck.IsInternet())
            {
                VtiaBtnSetModel btnDetails = new VtiaBtnSetModel();
                string NewToken = string.Empty;
                try
                {
                    var lang = UtilityManager.GetLanguageParameter();

                    String url = ZATCAConstants.VATGetRevokeBtnSet + "?formBundleType=" + Fbtypz + "&formBundleNumber=" + Fbnum + "&formProcess=" + Formproc + "&userName=" + "&TIN=" + App.LoginDataRetrieved.TIN + "&language=" + lang + "&statusCode=" + Status + "&transactionType=" + TxnTp;

                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfo>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfo>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfo>().Model;

                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);

                    var uri = new Uri(url);
                    HttpResponseMessage GAZTVATInstalmentDataResponse = await client.GetAsync(uri);
                    if (GAZTVATInstalmentDataResponse != null)
                    {
                        if (GAZTVATInstalmentDataResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTVATInstalmentDataResponse.Headers;
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
                        String vatInstalmentData = GAZTVATInstalmentDataResponse.Content.ReadAsStringAsync().Result;
                        btnDetails = JsonConvert.DeserializeObject<VtiaBtnSetModel>(vatInstalmentData);
                        if (!string.IsNullOrEmpty(vatInstalmentData) && btnDetails.D == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(vatInstalmentData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                                //throw new GAZTErrorException(errorMessage);

                            }
                        }
                    }
                    return btnDetails;
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
        public static async Task<string> SaveVATInstalmentRevokeData(RequestToVATInstallmentPlanDetails _vATInstalment)
        {

            if (NetworkCheck.IsInternet())
            {
                try
                {

                    RequestToVATInstallmentPlanDetails _vatResponseObject = new RequestToVATInstallmentPlanDetails();
                    string LangZ = WebServiceManager.GetLangZParameterAREN();
                    String url = ZATCAConstants.SaveVATInstalmentdata;

                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    var serilized = JsonConvert.SerializeObject(_vATInstalment.d);
                    //client.DefaultRequestHeaders.Add("Token", "123");
                    //client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    //client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    //client.DefaultRequestHeaders.Add("Accept", "application/json");

                    var lang = UtilityManager.GetLanguageParameter();

                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfo>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfo>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfo>().Model;

                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);

                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = client.PostAsync(uri, contentPost).Result;
                    var _zakatReturnDetailsDesponsestr = res.Content.ReadAsStringAsync().Result;
                    //_vatResponseObject = JsonConvert.DeserializeObject<RequestToVATInstallmentPlanDetails>(_zakatReturnDetailsDesponsestr);
                    //if (_vatResponseObject == null || _vatResponseObject.d == null)
                    //{
                    //    WebServiceManager.ErrorMessage = string.Empty;
                    //    ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatReturnDetailsDesponsestr);
                    //    if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                    //    {
                    //        WebServiceManager.ErrorMessage = errorMesg.error.innererror.errordetails[0].message;
                    //        throw new GAZTVATRegistrationInProcessException(WebServiceManager.ErrorMessage);
                    //    }
                    //}
                    return _zakatReturnDetailsDesponsestr;
                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
