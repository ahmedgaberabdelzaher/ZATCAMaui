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
                    string url = ZATCAConstants.GetReqVATInstalmentdata + "TaxType='" + taxType + "',AudTin='" + "',Gpart='" + App.LoginDataRetrieved.TIN + "',Lang='" + lang + "',UserTin='" + "')?&$expand=ASSLISTSet,STATUSSet,REQTYPSet&$format=json";
                    HttpResponseMessage _requestVATInstalmentPlanresponse = await GetServiceManager.MakeGetAPICall(url, false, "");
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

                    char lang = WebServiceManager.GetLangZParameter();
                    string LangZAREN = WebServiceManager.GetLangZParameterAREN();

                    string url = ZATCAConstants.VATGetFormGUIDURL + "Euser1='" + "',Fbguid='" + fbguid + "',Fbnum='" + fbnum + "',Fbtyp='" + fbtyp + "'," +
                     "Gpart='" + gpart + "',Lang='" + lang + "',Persl='" + "',Status='" + Status + "',Dispflag='" + "')?=&$format=json&sap-language=" + LangZAREN;
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


                    App.IsSessionExpired = true;
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
                    string url = ZATCAConstants.GetRequestToVATInstalmentPlanById
                        + "FormGuid='" + FormGuid + "',Euser='" + Euser + "',Gpartz=''," +
                        "Langz='" + lang + "',Officerz='" + "',PortalUsrz='" + "',TxnTpz='" + "')?&$expand=VTIASet,VTISSet,NOTESSet,ATTACHMENTSet,VTADSet&$format=json";
                    HttpResponseMessage _requestVATInstalmentPlanresponse = await GetServiceManager.MakeGetAPICall(url, false, "");
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
                    string url = ZATCAConstants.GetDisplayInstallmentAgreementSchedule
                        + "FormGuid='" + FormGuid + "',Euser='" + Euser + "',Gpart=''," +
                        "Langz='" + lang + "',Opbel='" + "')?&$expand=VTIA_IADTSet,VTIA_IAHDSet&$format=json";
                    HttpResponseMessage _requestVATInstalmentPlanresponse = await GetServiceManager.MakeGetAPICall(url, false, "");
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
                    string url = ZATCAConstants.GetInstallmentSchedule
                        + "FormGuid='" + FormGuid + "',Euser='" + Euser + "',Gpart=''," +
                        "Langz='E',Opbel='" + Opbel + "')?$expand=VTIA_IADTSet,VTIA_IAHDSet&$format=json";
                    HttpResponseMessage _requestVATInstalmentPlanresponse = await GetServiceManager.MakeGetAPICall(url, false, "");
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
                    char lang = WebServiceManager.GetLangZParameter();
                    string url = "";

                    if (formGuid == "")
                    {

                        url = ZATCAConstants.GetVATInstalmentdata + "FormGuid='" + "',Euser='" + "',Gpartz='" + App.LoginDataRetrieved.TIN + "',Langz='" + lang + "',Officerz='" + "',PortalUsrz='" + "',TxnTpz='" + "')?$expand=VTIASet,VTISSet,NOTESSet,ATTACHMENTSet,VTADSet&$format=json";

                    }
                    else
                    {

                        url = ZATCAConstants.GetVATInstalmentdata + "FormGuid='" + formGuid + "',Euser='" + euser + "',Gpartz='" + "',Langz='" + lang + "',Officerz='" + "',PortalUsrz='" + "',TxnTpz='" + "')?$expand=VTIASet,VTISSet,NOTESSet,ATTACHMENTSet,VTADSet&$format=json";

                    }
                    HttpResponseMessage GAZTVATInstalmentDataResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
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
                                //throw new GAZTVATRegistrationInProcessException(errorMessage);
                                throw new GAZTErrorException(errorMessage);

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
                    string LangZ = WebServiceManager.GetLangZParameterAREN();
                    string url = ZATCAConstants.SaveVATInstalmentdata;

                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    if (_vATInstalment.d.Operationz == "01" || _vATInstalment.d.Operationz == "05" || _vATInstalment.d.Operationz == "04")
                    {

                        var serilized = JsonConvert.SerializeObject(_vATInstalment.d);
                        client.DefaultRequestHeaders.Add("Token", "123");
                        client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                        client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                        client.DefaultRequestHeaders.Add("Accept", "application/json");

                        HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                        HttpResponseMessage res = client.PostAsync(uri, contentPost).Result;
                        var _zakatReturnDetailsDesponsestr = res.Content.ReadAsStringAsync().Result;
                        _vatResponseObject = JsonConvert.DeserializeObject<VatInstalmentPlanResponse>(_zakatReturnDetailsDesponsestr);
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
                        var serilized = JsonConvert.SerializeObject(_vATInstalment.d);
                        client.DefaultRequestHeaders.Add("Token", "123");
                        client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                        client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                        client.DefaultRequestHeaders.Add("Accept", "application/json");

                        HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                        HttpResponseMessage res = client.PostAsync(uri, contentPost).Result;
                        var _zakatReturnDetailsDesponsestr = res.Content.ReadAsStringAsync().Result;
                        _vatResponseObject = JsonConvert.DeserializeObject<VatInstalmentPlanResponse>(_zakatReturnDetailsDesponsestr);
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
        #endregion
    }
}
