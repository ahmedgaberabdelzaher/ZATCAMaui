using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.VATRefunds;
using static ZATCAMAUI.Models.ErrorMessage;

namespace ZATCAMAUI.Core.Mangers
{

    public class VATDeregistrationWebServiceManager
    {
        #region VAT Deregistration
        public static async Task<AttachmentRootOject> GAZTSaveVATDeregAttachment(byte[] AttachmentByte, string fileName, string RetGuid, string Dotyp, string contentType)//, string returnedFguid
        {
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    AttachmentRootOject _attachment = new AttachmentRootOject();
                    char LangZ = WebServiceManager.GetLangZParameter();
                    string AttBy = "TP";
                    string url = ZATCAConstants.GAZTSaveAttachment + "'" + "'" + ",RetGuid='" + RetGuid + "'" + ",Flag='" + "N" + "'" + ",Dotyp='" + Dotyp + "'" + ",SchGuid='" + "'" + ",Srno=" + "1" + ",Doguid='" + "'" + ",AttBy='" + AttBy + "'" + ")/AttachMedSet";

                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("slug", fileName);
                    ByteArrayContent baContent = new ByteArrayContent(AttachmentByte);
                    if (!string.IsNullOrEmpty(contentType))
                        baContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                    var response = await client.PostAsync(url, baContent);
                    var responsestr = response.Content.ReadAsStringAsync().Result;
                    _attachment = JsonConvert.DeserializeObject<AttachmentRootOject>(responsestr);
                    return _attachment;
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
        public static string GAZTDeleteVATDeRegistrationAttachment(string fileName, string RetGuid, string Dotyp)
        {
            if (NetworkCheck.IsInternet())
            {
                string DeleteToken = string.Empty;
                try
                {
                    AttachmentRootOject _attachment = new AttachmentRootOject();
                    char LangZ = WebServiceManager.GetLangZParameter();
                    string AttBy = "TP";
                    string url = ZATCAConstants.GAZTDeteleAttachment + "'" + "'" + ",RetGuid='undefined'" + ",Flag='" + "N" + "'" + ",Dotyp='" + Dotyp + "'" + ",SchGuid='" + "'" + ",Srno=" + "1" + ",Doguid='" + RetGuid + "'" + ",AttBy='" + AttBy + "'" + ")/$value?saml2=enabled"; //",RetGuid='005056B1F8FB1EDA8FF041CFF05E83A9',Flag='N',Dotyp='VTA0',SchGuid='',Srno=1,Doguid='',AttBy='TP')/AttachMedSet";// Constants.SaveVATDeclarationData;
                                                                                                                                                                                                                                                                                   // lang + "'" + "&$filter=Idtype eq " + IdType + ",RetGuid='" + RetGuid + "'" +
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("slug", fileName);

                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "multipart/form-data");
                    HttpResponseMessage res = client.DeleteAsync(url).Result;
                    var responsestr = res.Content.ReadAsStringAsync().Result;
                    _attachment = JsonConvert.DeserializeObject<AttachmentRootOject>(responsestr);
                    if (res != null)
                    {
                        HttpHeaders headers = res.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("delete", out values))
                        {
                            DeleteToken = values.First();
                        }
                    }
                    return DeleteToken;
                }
                catch (Exception)
                {


                    return DeleteToken;
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        #region VATDeregistration Reason

        public static async Task<VATDeregistrationModelRootObject> GAZTGETVATDeregReasonDropdownList(string selectedType)
        {
            if (NetworkCheck.IsInternet())
            {
                VATDeregistrationModelRootObject reasonData = new VATDeregistrationModelRootObject();
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    char lang = WebServiceManager.GetLangZParameter();
                    string url = ZATCAConstants.GAZTGETVATDeregReasonDropdownList + " eq " + "'" + selectedType + "'" + " and " + "Lang" + " eq " + "'" + lang + "'" + "&$format=json";

                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    var uri = new Uri(url);
                    HttpResponseMessage GAZTVATDeregreasonDataResponse = await client.GetAsync(uri);
                    if (GAZTVATDeregreasonDataResponse != null)
                    {
                        if (GAZTVATDeregreasonDataResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTVATDeregreasonDataResponse.Headers;
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

                        string GAZTVATDeregreasonDataResponseJSON = GAZTVATDeregreasonDataResponse.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(GAZTVATDeregreasonDataResponseJSON))
                        {
                            reasonData = JsonConvert.DeserializeObject<VATDeregistrationModelRootObject>(GAZTVATDeregreasonDataResponseJSON);
                            if (reasonData == null)
                            {
                                throw new Exception(AppResources.Nodataavailable);
                            }
                        }
                        else
                        {
                            throw new Exception(AppResources.NoBillsAvailable);
                        }
                    }
                    return reasonData;
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

        #region VATDeregistration Attachment DocumentType

        public static async Task<VATDeRegistrationAttachmentDropdownDetails> GAZTGETVATDeregAttachmentsDropdownList(string selectedType)
        {
            if (NetworkCheck.IsInternet())
            {
                VATDeRegistrationAttachmentDropdownDetails vATDeregAttDetails = new VATDeRegistrationAttachmentDropdownDetails();
                string NewToken = string.Empty;
                try
                {
                    char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string status = "E0001";

                    string url = ZATCAConstants.GAZTGETVATDeregAttachmentsDropdownList + "',Lang='" + lang + "',Officer='" + "',Gpart='" + App.LoginDataRetrieved.TIN + "',Status='" + status + "',TxnTp='" + selectedType + "',Formproc='ZTAX_VT_REG'" + ")?&$expand=VR_UI_BTNSet,ELGBL_DOCSet&$format=json";
                    client.DefaultRequestHeaders.Add("Token", "123");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTVATDeregAttDataResponse = await client.GetAsync(uri);
                    if (GAZTVATDeregAttDataResponse != null)
                    {
                        if (GAZTVATDeregAttDataResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTVATDeregAttDataResponse.Headers;
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
                        string VatDeregAttListResultModelSetResponseJson = GAZTVATDeregAttDataResponse.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(VatDeregAttListResultModelSetResponseJson))
                        {
                            VatDeregAttListResultModelSetResponseJson = JObject.Parse(VatDeregAttListResultModelSetResponseJson)["d"].ToString();

                            vATDeregAttDetails = JsonConvert.DeserializeObject<VATDeRegistrationAttachmentDropdownDetails>(VatDeregAttListResultModelSetResponseJson);
                            if (vATDeregAttDetails == null)
                            {
                                throw new GAZTErrorException(AppResources.ZZSomethingwentwrong);
                            }
                        }
                        else
                        {
                            throw new GAZTErrorException(AppResources.ZZSomethingwentwrong);
                        }
                    }
                    return vATDeregAttDetails;
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


        #region VATDeregistration Reason

        public async static Task<VATDeregistrationLastICRDateRootObject> GAZTGETVATDeregSuspensionDate(string selectedType)
        {
            if (NetworkCheck.IsInternet())
            {
                VATDeregistrationLastICRDateRootObject reasonData = new VATDeregistrationLastICRDateRootObject();
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    char lang = WebServiceManager.GetLangZParameter();
                    string url = ZATCAConstants.GAZTGETVATDeregSuspensionDate + "Gpartx" + " eq " + "'" + App.LoginDataRetrieved.TIN + "'" + " and " + "UserTypx" + " eq " + "'TP'" + " and " + "TxnTpx" + " eq " + "'" + "ZVAT_SUSP" + "'" + " and " + "Reqtp" + " eq " + "'S'" + "&$format=json";

                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    var uri = new Uri(url);
                    HttpResponseMessage Response = await GetServiceManager.MakeGetAPICall(url, false, "");
                    HttpResponseMessage GAZTVATDeregreasonDataResponse = client.GetAsync(uri).Result;
                    if (GAZTVATDeregreasonDataResponse != null)
                    {
                        if (GAZTVATDeregreasonDataResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTVATDeregreasonDataResponse.Headers;
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

                        string GAZTVATDeregreasonDataResponseJSON = GAZTVATDeregreasonDataResponse.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(GAZTVATDeregreasonDataResponseJSON))
                        {
                            reasonData = JsonConvert.DeserializeObject<VATDeregistrationLastICRDateRootObject>(GAZTVATDeregreasonDataResponseJSON);
                            if (reasonData == null)
                            {
                                throw new Exception(AppResources.Nodataavailable);
                            }
                        }
                        else
                        {
                            throw new Exception(AppResources.NoBillsAvailable);
                        }
                    }
                    return reasonData;
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

        #region VATDeregistration Reason

        public static string GAZTGETVATDeregReturnFilingDateList(DateTime StartDate, DateTime EndDate)
        {
            if (NetworkCheck.IsInternet())
            {
                VATDeregistrationSuspendedDateRootObject reasonData = new VATDeregistrationSuspendedDateRootObject();
                string NewToken = string.Empty;
                string GAZTVATDeregreasonDataResponseJSON = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    char lang = WebServiceManager.GetLangZParameter();

                    string startDate = StartDate.Year.ToString() + "-" + StartDate.Month.ToString() + "-" + StartDate.Day.ToString() + "T" + StartDate.Hour.ToString() + ":" + StartDate.Minute.ToString();
                    string endDate = EndDate.Year.ToString() + "-" + EndDate.Month.ToString() + "-" + EndDate.Day.ToString() + "T" + EndDate.Hour.ToString() + ":" + EndDate.Minute.ToString();

                    string url = ZATCAConstants.GAZTGETVATDeregReturnFilingDateList + "Gpart" + " eq " + "'" + App.LoginDataRetrieved.TIN + "'" + " and " + "StartDate" + " eq datetime" + "'" + startDate + "'" + " and " + "EndDate" + " eq datetime" + "'" + endDate + "'" + "&$format=json";

                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

                    var uri = new Uri(url);
                    HttpResponseMessage GAZTVATDeregreasonDataResponse = client.GetAsync(uri).Result;
                    if (GAZTVATDeregreasonDataResponse != null)
                    {
                        if (GAZTVATDeregreasonDataResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTVATDeregreasonDataResponse.Headers;
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

                        GAZTVATDeregreasonDataResponseJSON = GAZTVATDeregreasonDataResponse.Content.ReadAsStringAsync().Result;

                    }
                    return GAZTVATDeregreasonDataResponseJSON;
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


        public static async Task<VatRefundsListResultModel> GAZTGetVAtRefundList()
        {
            if (NetworkCheck.IsInternet())
            {
                VatRefundsListResultModel VatRefundsListResultModelSet = new VatRefundsListResultModel();
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    char lang = WebServiceManager.GetLangZParameter();

                    string url = ZATCAConstants.VatRefundList + "(TaxType='VT',Lang='" + lang + "',Gpart='" + App.LoginDataRetrieved.TIN + "',Euser='',Flag='W',Fbguid='')?&$expand=STATUSSet,WI_DTLSet,VatRef_HeaderSet,VatRef_SubItemsSet&$format=json";
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

                    var uri = new Uri(url);
                    HttpResponseMessage VatRefundsResponse = await client.GetAsync(uri);
                    if (VatRefundsResponse != null)
                    {
                        if (VatRefundsResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = VatRefundsResponse.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                            App.IsSessionExpired = false;
                        }
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }

                        string VatRefundsListResultModelSetResponseJson = VatRefundsResponse.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(VatRefundsListResultModelSetResponseJson))
                        {
                            VatRefundsListResultModelSetResponseJson = JObject.Parse(VatRefundsListResultModelSetResponseJson)["d"].ToString();

                            VatRefundsListResultModelSet = JsonConvert.DeserializeObject<VatRefundsListResultModel>(VatRefundsListResultModelSetResponseJson);
                            if (VatRefundsListResultModelSet == null)
                            {
                                throw new GAZTErrorException(AppResources.ZZSomethingwentwrong);
                            }
                        }
                        else
                        {
                            throw new GAZTErrorException(AppResources.ZZSomethingwentwrong);
                        }
                    }
                    return VatRefundsListResultModelSet;
                }
                catch (GAZTErrorException ex)
                {
                    throw new GAZTErrorException(AppResources.Somethingwentwrong);
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        public static async Task<VatRefundDisplayDataModel> GAZTGetVATRefundDisplayBankIdTypeData(string formguid)
        {
            if (NetworkCheck.IsInternet())
            {
                VatRefundDisplayDataModel VatRefundDisplayDataModel = new VatRefundDisplayDataModel();
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string lang = WebServiceManager.GetLangZParameterAREN();
                    string url = ZATCAConstants.VatRefundDisplayData + "FormGuid='" + formguid + "',Formprocx='ZTAX_VAT_MAISC_PROC',Gpartx='" + App.LoginDataRetrieved.TIN + "',Langx='" + lang + "',Officerx='',TxnTpx='')?$expand=AttdetSet,BankDtlSet,NotesSet&$format=json";
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

                    var uri = new Uri(url);
                    HttpResponseMessage VatRefundsResponse = await client.GetAsync(uri);
                    if (VatRefundsResponse != null)
                    {
                        if (VatRefundsResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = VatRefundsResponse.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                            App.IsSessionExpired = false;
                        }
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }

                        string VatRefundsListResultModelSetResponseJson = VatRefundsResponse.Content.ReadAsStringAsync().Result;

                        if (VatRefundsResponse.StatusCode == HttpStatusCode.BadRequest)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(VatRefundsListResultModelSetResponseJson);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                WebServiceManager.ErrorMessageForUnlockAccount = errorMesg.error.innererror.errordetails[0].message;
                                string WithReplacedString = WebServiceManager.ErrorMessageForUnlockAccount.Replace("An exception was raised", string.Empty);
                                WebServiceManager.ErrorMessageForUnlockAccount = WithReplacedString;
                                throw new GAZTErrorException(WebServiceManager.ErrorMessageForUnlockAccount);
                            }
                        }
                        else if (!string.IsNullOrEmpty(VatRefundsListResultModelSetResponseJson))
                        {
                            VatRefundsListResultModelSetResponseJson = JObject.Parse(VatRefundsListResultModelSetResponseJson)["d"].ToString();
                            VatRefundDisplayDataModel = JsonConvert.DeserializeObject<VatRefundDisplayDataModel>(VatRefundsListResultModelSetResponseJson);
                            if (VatRefundDisplayDataModel == null)
                            {
                                throw new GAZTErrorException(AppResources.ZZSomethingwentwrong);
                            }
                        }
                        else
                        {
                            throw new GAZTErrorException(AppResources.ZZSomethingwentwrong);
                        }
                    }
                    return VatRefundDisplayDataModel;
                }
                catch (GAZTErrorException ex)
                {
                    throw new GAZTErrorException(ex.Message);
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        public static async Task<VarRefundIbanDataModel> GAZTGetVATRefundGetIbanData(string fbNum)
        {
            if (NetworkCheck.IsInternet())
            {
                VarRefundIbanDataModel VarRefundIbanDataModel = new VarRefundIbanDataModel();
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string lang = WebServiceManager.GetLangZParameterAREN();
                    string url = ZATCAConstants.VatRefundGetIbanData + "Gpart='" + App.LoginDataRetrieved.TIN + "',Status='',TxnTp='',Formproc='')?&$expand=VR_UI_BTNSet,IBANSet&$format=json";
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

                    var uri = new Uri(url);
                    HttpResponseMessage VatRefundsResponse = await client.GetAsync(uri);
                    if (VatRefundsResponse != null)
                    {
                        if (VatRefundsResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = VatRefundsResponse.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                            App.IsSessionExpired = false;
                        }
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }

                        string VatRefundsListResultModelSetResponseJson = VatRefundsResponse.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(VatRefundsListResultModelSetResponseJson))
                        {
                            VatRefundsListResultModelSetResponseJson = JObject.Parse(VatRefundsListResultModelSetResponseJson)["d"].ToString();

                            VarRefundIbanDataModel = JsonConvert.DeserializeObject<VarRefundIbanDataModel>(VatRefundsListResultModelSetResponseJson);
                            if (VarRefundIbanDataModel == null)
                            {
                                throw new GAZTErrorException(AppResources.ZZSomethingwentwrong);
                            }
                        }
                        else
                        {
                            throw new GAZTErrorException(AppResources.ZZSomethingwentwrong);
                        }
                    }
                    return VarRefundIbanDataModel;
                }
                catch (GAZTErrorException )
                {
                    throw new GAZTErrorException(AppResources.Somethingwentwrong);
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        public static async Task<VatRefundDisplayDataModel> GAZTVATRefundSubmitRequest(VatRefundDisplayDataModel _newRequestSummaryData)
        {

            if (NetworkCheck.IsInternet())
            {
                VatRefundDisplayDataModel _newRequestSummaryDataResponse = new VatRefundDisplayDataModel();
                string NewToken = string.Empty;
                _newRequestSummaryData.Agrfg = "X";
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string lang = WebServiceManager.GetLangZParameterAREN();
                    string url = ZATCAConstants.VatRefundSubmitData;

                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

                    var uri = new Uri(url);
                    var serilized = JsonConvert.SerializeObject(_newRequestSummaryData);

                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage VatRefundsResponse = await client.PostAsync(uri, contentPost);

                    if (VatRefundsResponse != null)
                    {
                        if (VatRefundsResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = VatRefundsResponse.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                            App.IsSessionExpired = false;
                        }
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }

                        string VatRefundsListResultModelSetResponseJson = VatRefundsResponse.Content.ReadAsStringAsync().Result;

                        if (VatRefundsResponse.StatusCode == HttpStatusCode.BadRequest)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(VatRefundsListResultModelSetResponseJson);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                WebServiceManager.ErrorMessageForUnlockAccount = errorMesg.error.innererror.errordetails[0].message;
                                string WithReplacedString = WebServiceManager.ErrorMessageForUnlockAccount.Replace("An exception was raised", string.Empty);
                                WebServiceManager.ErrorMessageForUnlockAccount = WithReplacedString;
                            }
                        }
                        else if (!string.IsNullOrEmpty(VatRefundsListResultModelSetResponseJson))
                        {
                            VatRefundsListResultModelSetResponseJson = JObject.Parse(VatRefundsListResultModelSetResponseJson)["d"].ToString();
                            _newRequestSummaryDataResponse = JsonConvert.DeserializeObject<VatRefundDisplayDataModel>(VatRefundsListResultModelSetResponseJson);
                            if (_newRequestSummaryDataResponse == null)
                            {
                                throw new GAZTErrorException(AppResources.ZZSomethingwentwrong);
                            }
                        }
                        else
                        {
                            throw new GAZTErrorException(AppResources.ZZSomethingwentwrong);
                        }
                    }
                    return _newRequestSummaryDataResponse;
                }
                catch (GAZTUnlockAccountException ex)
                {
                    throw new GAZTUnlockAccountException(ex.Message);
                }

                catch (Exception)
                {




                    throw new GAZTErrorException(AppResources.Somethingwentwrong);
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
