using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.AttachmentRequest;
using ZATCAMAUI.Models.EstablishmentRegistration;
using ZATCAMAUI.Models.VATRefunds;
using static ZATCAMAUI.Models.ErrorMessage;

namespace ZATCAMAUI.Core.Mangers
{

    public class VATDeregistrationWebServiceManager
    {
        #region VAT Deregistration
        public static async Task<AttachmentRootOject> GAZTSaveVATDeregAttachment(Stream AttachmentByte, string fileName, string RetGuid, string Dotyp, string contentType)//, string returnedFguid
        {
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    AttachmentRootOject _attachment = new AttachmentRootOject();
                    var content = new MultipartFormDataContent();
                    var fileContent = new StreamContent(AttachmentByte);
                    fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = "attachmentFile",
                        FileName = fileName
                    };
                    content.Add(fileContent, "attachmentFile", fileName);
                    var lang = UtilityManager.GetLanguageParameter();
                    string AttBy = "TP";
                    //String url = Constants.GAZTSaveAttachment + " + RetGuid + "'" + ",Flag='" + "N" + "'" + ",Dotyp='" + Dotyp + "'" + ",SchGuid='" + "'" + ",Srno=" + "1" + ",Doguid='" + "'" + ",AttBy='" + AttBy + "'" + ")/AttachMedSet";
                    String url = ZATCAConstants.GAZTSaveAttachment + "&attachmentFlag=New" + "&returnGUID=" + RetGuid + "&formGUID=" + "&documentCategory=" + Dotyp + "&serialNumber=1" + "&documentId=" + "&attachedByPerson=TP" + "&fileName=" + fileName;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);

                    var serializeOptions = new JsonSerializerSettings
                    {
                        DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                        DateTimeZoneHandling = DateTimeZoneHandling.Utc
                    };
                    serializeOptions.Converters.Add(new JsonFieldListConverter());
                    var serialized = JsonConvert.SerializeObject(_attachment, serializeOptions);
                    var response = await client.PostAsync(url, content);
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
                    string LangZ = WebServiceManager.GetLangZParameterAREN();
                    string AttBy = "TP";
                    DeleteAttachmentRequest _attachmentReq = new DeleteAttachmentRequest()
                    {
                        fileName = fileName,
                        returnGUID = "",
                        formGUID = "",
                        documentCategory = Dotyp,
                        documentId = RetGuid,
                        serialNumber = "1",
                        attachedByPerson = AttBy
                    };
                    String url = ZATCAConstants.GAZTDeteleAttachment;                                                                                                                                                                                                                                                               // lang + "'" + "&$filter=Idtype eq " + IdType + ",RetGuid='" + RetGuid + "'" +
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", LangZ);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    var serialized = JsonConvert.SerializeObject(_attachmentReq);
                    HttpContent contentPost = new StringContent(serialized, Encoding.UTF8, ZATCAConstants.ContentType);
                    //  client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "multipart/form-data");
                    HttpResponseMessage res = client.PostAsync(url, contentPost).Result;
                    var responsestr = res.Content.ReadAsStringAsync().Result;
                    _attachment = JsonConvert.DeserializeObject<AttachmentRootOject>(responsestr);
                    //HttpResponseMessage res = client.DeleteAsync(url).Result;
                    //var responsestr = res.Content.ReadAsStringAsync().Result;
                    //_attachment = JsonConvert.DeserializeObject<AttachmentRootOject>(responsestr);
                    if (res != null)
                    {
                        //HttpHeaders headers = res.Headers;
                        //IEnumerable<string> values;
                        //if (headers.TryGetValues("delete", out values))
                        //{
                        //    DeleteToken = values.First();
                        //}
                        if (res.StatusCode == HttpStatusCode.NoContent || res.StatusCode == HttpStatusCode.OK)
                            DeleteToken = "X";
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
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var lang = UtilityManager.GetLanguageParameter();
                    string url = ZATCAConstants.GAZTGETVATDeregReasonDropdownList + "&language=" + lang + "&transactionType=" + selectedType;
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);

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
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var lang = UtilityManager.GetLanguageParameter();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    string status = "E0001";

                    // String url = Constants.GAZTGETVATDeregAttachmentsDropdownList + "',Lang='" + lang + "',Officer='" + "',Gpart='" + App.LoginDataRetrieved.TIN + "',Status='" + status + "',TxnTp='" + selectedType + "',Formproc='ZTAX_VT_REG'" + ")?&$expand=VR_UI_BTNSet,ELGBL_DOCSet&$format=json";
                    String url = ZATCAConstants.GAZTGETVATDeregAttachmentsDropdownList + App.LoginDataRetrieved.TIN + "&language=" + lang + "&status=" + status + "&transactionType=" + selectedType + "&formProcess=ZTAX_VT_REG";
                    //client.DefaultRequestHeaders.Add("Token", "123");
                    //client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
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
                            VatDeregAttListResultModelSetResponseJson = JObject.Parse(VatDeregAttListResultModelSetResponseJson)["data"].ToString();

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
                    string url = ZATCAConstants.GAZTGETVATDeregSuspensionDate + App.LoginDataRetrieved.TIN + "&userType=" + "TP" + "&transactionType=" + "VT_SUSP" + "&requestType=" + "Suspension";

                    var uri = new Uri(url);
                    //HttpResponseMessage Response = await GetServiceManager.MakeGetAPICall(url, false, "");
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
                    var lang = UtilityManager.GetLanguageParameter();

                    //string startDate = StartDate.Year.ToString() + "-" + StartDate.Month.ToString() + "-" + StartDate.Day.ToString() + "T" + StartDate.Hour.ToString() + ":" + StartDate.Minute.ToString();
                    //string endDate = EndDate.Year.ToString() + "-" + EndDate.Month.ToString() + "-" + EndDate.Day.ToString() + "T" + EndDate.Hour.ToString() + ":" + EndDate.Minute.ToString();
                    string startDate = StartDate.ToString("yyyy-MM-ddTHH\\%3AMM\\%3Ass");
                    string endDate = EndDate.ToString("yyyy-MM-ddTHH\\%3AMM\\%3Ass");
                    string url = ZATCAConstants.GAZTGETVATDeregReturnFilingDateList + App.LoginDataRetrieved.TIN + "&startDate=" + startDate + "&endDate=" + endDate;
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
                    //HttpClient client = new HttpClient(App.httpClientHandler);
                    //char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
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
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    //  client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    // string url = Constants.VatRefundList + "(TaxType='VT',Lang='" + lang + "',Gpart='" + App.LoginDataRetrieved.TIN + "',Euser='',Flag='W',Fbguid='')?&$expand=STATUSSet,WI_DTLSet,VatRef_HeaderSet,VatRef_SubItemsSet&$format=json";
                    string url = ZATCAConstants.VatRefundList + App.TP.TIN + "&language=" + lang + "&flag=W" + "&taxType=VT";

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
                            try
                            {
                                VatRefundsListResultModelSetResponseJson = JObject.Parse(VatRefundsListResultModelSetResponseJson)["data"].ToString();

                                VatRefundsListResultModelSet = JsonConvert.DeserializeObject<VatRefundsListResultModel>(VatRefundsListResultModelSetResponseJson);

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                                throw new GAZTErrorException(AppResources.Somethingwentwrong);
                            }
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

        public static async Task<VatRefundDisplayDataModel> GAZTGetVATRefundDisplayBankIdTypeData(string formguid)
        {
            if (NetworkCheck.IsInternet())
            {
                VatRefundDisplayDataModel VatRefundDisplayDataModel = new VatRefundDisplayDataModel();
                string NewToken = string.Empty;
                try
                {
                    //HttpClient client = new HttpClient(App.httpClientHandler);
                    //string lang = WebServiceManager.GetLangZParameterAREN();
                    //  string url = Constants.VatRefundDisplayData + "FormGuid='" + formguid + "',Formprocx='ZTAX_VAT_MAISC_PROC',Gpartx='" + App.LoginDataRetrieved.TIN + "',Langx='" + lang + "',Officerx='',TxnTpx='')?$expand=AttdetSet,BankDtlSet,NotesSet,VtfrAmtSet&$format=json"; //Cr4194 VAT refund ..
                    HttpClient client = new HttpClient(App.httpClientHandler);
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
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    //  client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    string url = ZATCAConstants.VatRefundDisplayData + App.TP.TIN + "&formProcess=ZTAX_VAT_MAISC_PROC" + "&language=" + lang + "&formGUID=" + formguid; //Cr4194 VAT refund ..

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
                        //else if (!string.IsNullOrEmpty(VatRefundsListResultModelSetResponseJson))
                        //{
                        //    VatRefundsListResultModelSetResponseJson = JObject.Parse(VatRefundsListResultModelSetResponseJson)["data"].ToString();
                        //    VatRefundDisplayDataModel = JsonConvert.DeserializeObject<VatRefundDisplayDataModel>(VatRefundsListResultModelSetResponseJson);
                        //    if (VatRefundDisplayDataModel == null)
                        //    {
                        //        throw new GAZTErrorException(AppResources.ZZSomethingwentwrong);
                        //    }
                        //}
                        //else
                        //{
                        //    throw new GAZTErrorException(AppResources.ZZSomethingwentwrong);
                        //}
                        else if (!string.IsNullOrEmpty(VatRefundsListResultModelSetResponseJson))
                        {
                            try
                            {
                                VatRefundsListResultModelSetResponseJson = JObject.Parse(VatRefundsListResultModelSetResponseJson)["data"].ToString();

                                VatRefundDisplayDataModel = JsonConvert.DeserializeObject<VatRefundDisplayDataModel>(VatRefundsListResultModelSetResponseJson);

                            }
                            catch (Exception ex)
                            {
                                string error = WebServiceManager.PrepareErrorMessageByJson(VatRefundsListResultModelSetResponseJson);
                                throw new GAZTErrorException(error);

                                //ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(VatRefundsListResultModelSetResponseJson);
                                //if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                                //{
                                //    WebServiceManager.ErrorMessageForUnlockAccount = errorMesg.error.innererror.errordetails[0].message;
                                //    String WithReplacedString = WebServiceManager.ErrorMessageForUnlockAccount.Replace("An exception was raised", string.Empty);
                                //    WebServiceManager.ErrorMessageForUnlockAccount = WithReplacedString;
                                //    throw new GAZTErrorException(WebServiceManager.ErrorMessageForUnlockAccount);
                                //}
                                //Console.WriteLine(ex);
                                //throw new GAZTErrorException(AppResources.Somethingwentwrong);
                            }
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
                    //HttpClient client = new HttpClient(App.httpClientHandler);
                    //string lang = WebServiceManager.GetLangZParameterAREN();
                    HttpClient client = new HttpClient(App.httpClientHandler);
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
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    // client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    string url = ZATCAConstants.VatRefundGetIbanData + App.LoginDataRetrieved.TIN + "&langauge=" + lang;

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
                            try
                            {
                                VatRefundsListResultModelSetResponseJson = JObject.Parse(VatRefundsListResultModelSetResponseJson)["data"].ToString();

                                VarRefundIbanDataModel = JsonConvert.DeserializeObject<VarRefundIbanDataModel>(VatRefundsListResultModelSetResponseJson);

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                                throw new GAZTErrorException(AppResources.Somethingwentwrong);
                            }
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
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    string url = ZATCAConstants.VatRefundSubmitData;
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var lang = UtilityManager.GetLanguageParameter();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
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
                            var result = JObject.Parse(VatRefundsListResultModelSetResponseJson);
                            if (result["result"] != null)
                            {
                                VatRefundsListResultModelSetResponseJson = JObject.Parse(VatRefundsListResultModelSetResponseJson)["result"].ToString();
                                _newRequestSummaryDataResponse = JsonConvert.DeserializeObject<VatRefundDisplayDataModel>(VatRefundsListResultModelSetResponseJson);
                            }
                            else
                            {
                                var error_message = WebServiceManager.PrepareErrorMessageByJson(VatRefundsListResultModelSetResponseJson);
                                throw new GAZTErrorException(error_message);
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
                catch (GAZTErrorException ex)
                {
                    throw new GAZTErrorException(ex.Message);
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
