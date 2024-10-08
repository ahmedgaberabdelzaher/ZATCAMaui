using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Mopups.Services;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.EstablishmentRegistration;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using static ZATCAMAUI.Models.ErrorMessage;
using ZATCAMAUI.Models.ESTOutletAddress;
using ZATCAMAUI.Models.AttachmentRequest;

namespace ZATCAMAUI.Core.Mangers
{

    public static class EstablishmentRegistrationWebServiceManager
    {
        #region Establishment Registration API Calls
        public static async Task<List<BranchesDropDownModel>> ESTBranchesDropDown()
        {
            List<BranchesDropDownModel> dropDownModels = new List<BranchesDropDownModel>();
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {

                    if (!NetworkCheck.IsInternet())
                    {
                        throw new GAZTInternetException();
                    }
                    var lang = UtilityManager.GetLanguageParameter();
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
                    var url = ZATCAConstants.ESTBranchesDropDown + lang;
                    var uri = new Uri(url);
                    HttpResponseMessage ESTBranchesDropDownResponse = await client.GetAsync(uri);
                    if (ESTBranchesDropDownResponse != null)
                    {
                        if (ESTBranchesDropDownResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            throw new GAZTSessionExpiredException();
                        }
                        HttpHeaders headers = ESTBranchesDropDownResponse.Headers;
                        IEnumerable<string> values = null;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                        }
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                throw new GAZTSessionExpiredException();
                            }
                            App.Token = NewToken;
                        }
                        string ESTBranchesDropDownResponseJSON = await ESTBranchesDropDownResponse.Content.ReadAsStringAsync();
                        if (!string.IsNullOrEmpty(ESTBranchesDropDownResponseJSON))
                        {
                            ESTBranchesDropDownResponseJSON = JObject.Parse(ESTBranchesDropDownResponseJSON)["data"].ToString();
                            dropDownModels = JsonConvert.DeserializeObject<List<BranchesDropDownModel>>(ESTBranchesDropDownResponseJSON);
                        }
                    }
                }
                catch (Exception)
                {

                }
            }
            else
            {
                throw new GAZTInternetException();
            }
            return dropDownModels;
        }
        public static async Task<TaxPayerDetails> ESTTaxPayerDetailGetService(string step, string TIN, string emailID, string srcidentify = null, string Fbnum = null)
        {
            TaxPayerDetails taxPayer = new TaxPayerDetails();
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                srcidentify = string.IsNullOrEmpty(srcidentify) || string.IsNullOrWhiteSpace(srcidentify) ? string.Empty : string.Format("O{0}", srcidentify);
                Fbnum = string.IsNullOrEmpty(Fbnum) || string.IsNullOrWhiteSpace(Fbnum) ? string.Empty : Fbnum;
                try
                {
                    var lang = UtilityManager.GetLanguageParameter();
                    if (!NetworkCheck.IsInternet())
                    {
                        throw new GAZTInternetException();
                    }
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
                    var URL = ZATCAConstants.ESTTaxPayerDetails + TIN + "&language=" + lang + "&stepNumber=" + step + "&sourceIdentifier=" + srcidentify + "&formBundleNumber=" + Fbnum;
                    var uri = new Uri(URL);
                    HttpResponseMessage ESTBranchesDropDownResponse = await client.GetAsync(uri);
                    if (ESTBranchesDropDownResponse != null)
                    {
                        if (ESTBranchesDropDownResponse.StatusCode == HttpStatusCode.BadRequest)
                        {
                            string _responseData = ESTBranchesDropDownResponse.Content.ReadAsStringAsync().Result;
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_responseData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorCode = errorMesg.error.innererror.errordetails[0].code;

                                var errorMsg = errorMesg.error.innererror.errordetails[0].message;

                                if (errorCode.Contains("206"))
                                {
                                    errorMsg = "206";
                                }
                                else if (errorCode.Contains("112"))
                                {
                                    errorMsg = "112";
                                }

                                string WithReplacedString = errorMsg.Replace("An exception was raised", string.Empty);
                                errorMsg = WithReplacedString;
                                throw new GAZTErrorException(errorMsg);
                            }
                        }
                        if (ESTBranchesDropDownResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            throw new GAZTSessionExpiredException();
                        }
                        HttpHeaders headers = ESTBranchesDropDownResponse.Headers;
                        IEnumerable<string> values = null;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                        }
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                throw new GAZTSessionExpiredException();
                            }
                            App.Token = NewToken;
                        }
                        string ESTBranchesDropDownResponseJSON = await ESTBranchesDropDownResponse.Content.ReadAsStringAsync();
                        if (!string.IsNullOrEmpty(ESTBranchesDropDownResponseJSON))
                        {
                            try
                            {
                                ESTBranchesDropDownResponseJSON = JObject.Parse(ESTBranchesDropDownResponseJSON)["data"].ToString();
                            }
                            catch (Exception)
                            {


                            }
                            taxPayer = JsonConvert.DeserializeObject<TaxPayerDetails>(ESTBranchesDropDownResponseJSON);
                            if (step.Equals("02") && taxPayer.Nreg_IdSet.Count > 0)
                            {
                                ZATCAConstants.IdSet = taxPayer.Nreg_IdSet;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
            else
            {
                throw new GAZTInternetException();
            }
            return taxPayer;
        }
        public static async Task<TaxPayerDetails> ZakatAmendESTTaxPayerDetailGetService(string step, string TIN, string emailID, string srcidentify = null, string Fbnum = null, string Fbstax = null, string Fbustx = null)
        {
            TaxPayerDetails taxPayer = new TaxPayerDetails();
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                srcidentify = string.IsNullOrEmpty(srcidentify) || string.IsNullOrWhiteSpace(srcidentify) ? string.Empty : string.Format("O{0}", srcidentify);
                Fbnum = string.IsNullOrEmpty(Fbnum) || string.IsNullOrWhiteSpace(Fbnum) ? string.Empty : Fbnum;
                try
                {
                    var lang = UtilityManager.GetLanguageParameter();
                    if (!NetworkCheck.IsInternet())
                    {
                        throw new GAZTInternetException();
                    }
                    var URL = ZATCAConstants.ESTTaxPayerDetails + App.LoginDataRetrieved.TIN + "&language=" + lang + "&portalUser=" + emailID + "&sourceIdentifier=" + srcidentify + "&stepNumber=" + step + "&formBundleNumber=" + Fbnum + "&formBundleStatus=" + Fbstax + "&formBundleStatusDescription=" + Fbustx;
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

                    var uri = new Uri(URL);
                    HttpResponseMessage ESTBranchesDropDownResponse = await client.GetAsync(uri);
                    if (ESTBranchesDropDownResponse != null)
                    {
                        if (ESTBranchesDropDownResponse.StatusCode == HttpStatusCode.BadRequest)
                        {
                            string _responseData = ESTBranchesDropDownResponse.Content.ReadAsStringAsync().Result;
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_responseData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorCode = errorMesg.error.innererror.errordetails[0].code;

                                var errorMsg = errorMesg.error.innererror.errordetails[0].message;

                                if (errorCode.Contains("206"))
                                {
                                    errorMsg = "206";
                                }
                                else if (errorCode.Contains("112"))
                                {
                                    errorMsg = "112";
                                }

                                string WithReplacedString = errorMsg.Replace("An exception was raised", string.Empty);
                                errorMsg = WithReplacedString;
                                throw new GAZTErrorException(errorMsg);
                            }
                        }
                        if (ESTBranchesDropDownResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            throw new GAZTSessionExpiredException();
                        }
                        HttpHeaders headers = ESTBranchesDropDownResponse.Headers;
                        IEnumerable<string> values = null;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                        }
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                throw new GAZTSessionExpiredException();
                            }
                            App.Token = NewToken;
                        }
                        string ESTBranchesDropDownResponseJSON = await ESTBranchesDropDownResponse.Content.ReadAsStringAsync();
                        if (!string.IsNullOrEmpty(ESTBranchesDropDownResponseJSON))
                        {
                            try
                            {
                                ESTBranchesDropDownResponseJSON = JObject.Parse(ESTBranchesDropDownResponseJSON)["data"].ToString();
                            }
                            catch (Exception)
                            {
                            }
                            taxPayer = JsonConvert.DeserializeObject<TaxPayerDetails>(ESTBranchesDropDownResponseJSON);
                        }
                    }
                }
                catch (JsonReaderException)
                {
                    throw new GAZTInvalidDataException();
                }
            }
            else
            {
                throw new GAZTInternetException();
            }
            return taxPayer;
        }

        public static async Task<TaxPayerDetails> ESTTaxPayerDetailPostService(TaxPayerDetails taxPayer)
        {
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    var lang = UtilityManager.GetLanguageParameter();
                    if (!NetworkCheck.IsInternet())
                    {
                        throw new GAZTInternetException();
                    }
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
                    var serializeOptions = new JsonSerializerSettings
                    {
                        DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                        DateTimeZoneHandling = DateTimeZoneHandling.Utc
                    };
                    serializeOptions.Converters.Add(new JsonFieldListConverter());
                    var serialized = JsonConvert.SerializeObject(taxPayer, serializeOptions);

                    HttpContent contentPost = new StringContent(serialized, Encoding.UTF8, ZATCAConstants.ContentType);

                    HttpResponseMessage ESTBranchesDropDownResponse = await client.PostAsync(new Uri(ZATCAConstants.ESTTaxPayerDetailsPost), contentPost);
                    if (ESTBranchesDropDownResponse != null)
                    {
                        if (ESTBranchesDropDownResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            throw new GAZTSessionExpiredException();
                        }
                        HttpHeaders headers = ESTBranchesDropDownResponse.Headers;
                        IEnumerable<string> values = null;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                        }
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                throw new GAZTSessionExpiredException();
                            }
                            App.Token = NewToken;
                        }
                        string ESTBranchesDropDownResponseJSON = await ESTBranchesDropDownResponse.Content.ReadAsStringAsync();
                        try
                        {

                            string deserialisedResponseJSONs = JObject.Parse(ESTBranchesDropDownResponseJSON)["result"]?.ToString();

                            if (deserialisedResponseJSONs == null)
                            {
                                ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(ESTBranchesDropDownResponseJSON);
                                if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails.Count > 0)
                                {
                                    string ErrorMessageFormServer = string.Empty;
                                    if (errorMesg.error.innererror.errordetails?.Count > 0)
                                    {

                                        if (errorMesg.error.innererror.errordetails.Count > 2)
                                        {
                                            for (int i = 0; i < errorMesg.error.innererror.errordetails.Count - 1; i++)
                                            {
                                                ErrorMessageFormServer = ErrorMessageFormServer + " " + errorMesg.error.innererror.errordetails[i].message;
                                            }
                                        }
                                        else
                                        {
                                            ErrorMessageFormServer = errorMesg.error.innererror.errordetails[0].message;
                                        }

                                    }


                                    throw new HTTPBadRequestException(ErrorMessageFormServer);
                                }
                                else if (errorMesg != null && errorMesg.error != null && errorMesg.error.message != null && !string.IsNullOrEmpty(errorMesg.error.message.value))
                                {
                                    string ErrorMessageFormServer = errorMesg.error.message.value;
                                    throw new HTTPBadRequestException(ErrorMessageFormServer);
                                }
                            }

                            else
                            {
                                if (!string.IsNullOrEmpty(deserialisedResponseJSONs))
                                {
                                    taxPayer = JsonConvert.DeserializeObject<TaxPayerDetails>(deserialisedResponseJSONs);
                                }
                            }
                        }
                        catch (Exception ex)
                        {


                            System.Diagnostics.Debug.WriteLine("API RESPONSE ERROR : {0}", ex);
                            if (!string.IsNullOrEmpty(ESTBranchesDropDownResponseJSON))
                            {
                                ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(ESTBranchesDropDownResponseJSON);
                                if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails.Count > 0)
                                {
                                    string ErrorMessageFormServer = errorMesg.error.innererror.errordetails[0].message;
                                    throw new HTTPBadRequestException(ErrorMessageFormServer);
                                }
                                else if (errorMesg != null && errorMesg.error != null && errorMesg.error.message != null && !string.IsNullOrEmpty(errorMesg.error.message.value))
                                {
                                    string ErrorMessageFormServer = errorMesg.error.message.value;
                                    throw new HTTPBadRequestException(ErrorMessageFormServer);
                                }
                            }
                        }


                    }
                }
                catch (JsonReaderException ex)
                {
                    throw new GAZTInvalidDataException();
                }
                catch (HttpRequestException)
                {
                }
                catch (GAZTException)
                {
                }
                catch (Exception)

                {
                }
            }
            else
            {
                throw new GAZTInternetException();
            }
            return taxPayer;
        }
        public static async Task<List<TaxpayerNationality>> ESTTaxPayerNationality(string nationality = null)
        {
            List<TaxpayerNationality> nationalities = new List<TaxpayerNationality>();
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                nationality = string.IsNullOrEmpty(nationality) || string.IsNullOrWhiteSpace(nationality) ? "SAUDI" : nationality;
                try
                {
                    var lang = UtilityManager.GetLanguageParameter();
                    if (!NetworkCheck.IsInternet())
                    {
                        throw new GAZTInternetException();
                    }
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

                    var url = ZATCAConstants.ESTTaxPayerNationality + nationality + "&language=" + lang;
                    var uri = new Uri(url);
                    HttpResponseMessage ESTBranchesDropDownResponse = await client.GetAsync(uri);
                    if (ESTBranchesDropDownResponse != null)
                    {
                        if (ESTBranchesDropDownResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            throw new GAZTSessionExpiredException();
                        }
                        HttpHeaders headers = ESTBranchesDropDownResponse.Headers;
                        IEnumerable<string> values = null;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                        }
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                throw new GAZTSessionExpiredException();
                            }
                            App.Token = NewToken;
                        }
                        string ESTBranchesDropDownResponseJSON = await ESTBranchesDropDownResponse.Content.ReadAsStringAsync();
                        if (!string.IsNullOrEmpty(ESTBranchesDropDownResponseJSON))
                        {
                            ESTBranchesDropDownResponseJSON = JObject.Parse(ESTBranchesDropDownResponseJSON)["data"].ToString();
                            ESTBranchesDropDownResponseJSON = JObject.Parse(ESTBranchesDropDownResponseJSON)["nationalities"].ToString();
                            nationalities = JsonConvert.DeserializeObject<List<TaxpayerNationality>>(ESTBranchesDropDownResponseJSON);
                        }
                    }
                }
                catch (JsonReaderException)
                {
                    throw new GAZTInvalidDataException();
                }
                catch (HttpRequestException)
                {
                }
                catch (GAZTException)
                {
                }
                catch (Exception)

                {
                }
            }
            else
            {
                throw new GAZTInternetException();
            }
            return nationalities;
        }
        public static async Task<Attachment> ESTAttachment(Stream AttachmentByte, string fileName, string RetGuid, string Doctype, string contentType, string outletref = null) //RG16 for Residency, RG19 for passport RG01 for CR copy RG02 licence copy
        {
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    outletref = string.IsNullOrEmpty(outletref) || string.IsNullOrWhiteSpace(outletref) ? string.Empty : outletref;
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
                    String url = ZATCAConstants.GAZTESTSaveAttachment + "&attachmentFlag=New" + "&returnGUID=" + RetGuid + "&formGUID=" + "&documentCategory=" + Doctype + "&serialNumber=1" + "&documentId=" + "&attachedByPerson=TP" + "&fileName=" + fileName;
                    var uri = new Uri(url);
                    //var uri = new Uri(string.Format("{0}(RetGuid='{1}',OutletRef='{2}',Flag='N',Dotyp='{3}',SchGuid='',Srno=1,Doguid='',AttBy='TP')/AttachMedSet",
                    //    Constants.ESTPostAttachment, RetGuid, outletref, Doctype));

                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);

                    //ByteArrayContent baContent = new ByteArrayContent(AttachmentByte);
                    //if (!string.IsNullOrEmpty(contentType))
                    //    baContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                    var response = await client.PostAsync(url, content);
                    var responsestr = response.Content.ReadAsStringAsync().Result;
                    responsestr = JObject.Parse(responsestr)["result"].ToString();
                    Attachment _attachment = JsonConvert.DeserializeObject<Attachment>(responsestr);
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
        public static string ESTDeleteAttachment(string fileName, string RetGuid, string docType, string docguid)
        {
            if (NetworkCheck.IsInternet())
            {
                string DeleteToken = string.Empty;
                try
                {
                    DeleteAttachmentRequest _attachmentReq = new DeleteAttachmentRequest()
                    {
                        fileName = fileName,
                        returnGUID = RetGuid,
                        formGUID = "",
                        documentCategory = docType,
                        documentId = docguid,
                        serialNumber = "1",
                        attachedByPerson = "X"
                    };
                    String url = ZATCAConstants.ESTDeleteAttachment;
                    var uri = new Uri(url);
                    var lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient();
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
                    var serialized = JsonConvert.SerializeObject(_attachmentReq);
                    HttpContent contentPost = new StringContent(serialized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = client.PostAsync(url, contentPost).Result;

                    //var uri = new Uri(string.Format("{0}(RetGuid='{1}',Flag='N',OutletRef='',Dotyp='{2}',SchGuid='',Srno=1,Doguid='{3}',AttBy='X')/$value",
                    //    , RetGuid, docType, docguid));
                    //HttpClient client = new HttpClient();

                    //client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    //client.DefaultRequestHeaders.Add("Accept", "application/json");
                    //client.DefaultRequestHeaders.Add("slug", WebUtility.UrlEncode(fileName));

                    //client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "multipart/form-data");

                    //HttpResponseMessage response = client.DeleteAsync(uri).Result;
                    var responsestr = res.Content.ReadAsStringAsync().Result;
                    if (res != null)
                    {
                        //HttpHeaders headers = response.Headers;
                        //IEnumerable<string> values;
                        //if (headers.TryGetValues("delete", out values))
                        //{
                        //    DeleteToken = values.First();
                        //}
                        if (res.StatusCode == HttpStatusCode.NoContent || res.StatusCode == HttpStatusCode.OK)
                            DeleteToken = "X";
                    }
                    return "delete";
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
        public static async Task<OutletNumber> ESTOutletNumber(string Fbnum)
        {
            OutletNumber outletNumber = null;
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    if (!NetworkCheck.IsInternet())
                    {
                        throw new GAZTInternetException();
                    }
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
                    var URL = ZATCAConstants.ESTOutletNumber + Fbnum;

                    var uri = new Uri(URL);
                    HttpResponseMessage ESTBranchesDropDownResponse = await client.GetAsync(uri);

                    if (ESTBranchesDropDownResponse != null)
                    {
                        if (ESTBranchesDropDownResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            throw new GAZTSessionExpiredException();
                        }
                        HttpHeaders headers = ESTBranchesDropDownResponse.Headers;
                        IEnumerable<string> values = null;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                        }
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                throw new GAZTSessionExpiredException();
                            }
                            App.Token = NewToken;
                        }
                        string ESTBranchesDropDownResponseJSON = await ESTBranchesDropDownResponse.Content.ReadAsStringAsync();
                        if (!string.IsNullOrEmpty(ESTBranchesDropDownResponseJSON))
                        {
                            ESTBranchesDropDownResponseJSON = JObject.Parse(ESTBranchesDropDownResponseJSON)["data"].ToString();
                            outletNumber = JsonConvert.DeserializeObject<OutletNumber>(ESTBranchesDropDownResponseJSON);
                        }
                    }
                }
                catch (JsonReaderException)
                {
                    throw new GAZTInvalidDataException();
                }
                catch (HttpRequestException)
                {
                }
                catch (GAZTException)
                {
                }
                catch (Exception)
                {
                }
            }
            else
            {
                throw new GAZTInternetException();
            }
            return outletNumber;
        }
        public static async Task<OutletNumber> ESTOutletNumberESAmendUpdate(string Fbnum, string tin)
        {
            OutletNumber outletNumber = null;
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    if (!NetworkCheck.IsInternet())
                    {
                        throw new GAZTInternetException();
                    }
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
                    var URL = ZATCAConstants.ESTOutletNumber + Fbnum + "&TIN=" + tin;

                    var uri = new Uri(URL);
                    HttpResponseMessage ESTBranchesDropDownResponse = await client.GetAsync(uri);
                    if (ESTBranchesDropDownResponse != null)
                    {
                        if (ESTBranchesDropDownResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            throw new GAZTSessionExpiredException();
                        }
                        HttpHeaders headers = ESTBranchesDropDownResponse.Headers;
                        IEnumerable<string> values = null;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                        }
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                throw new GAZTSessionExpiredException();
                            }
                            App.Token = NewToken;
                        }
                        string ESTBranchesDropDownResponseJSON = await ESTBranchesDropDownResponse.Content.ReadAsStringAsync();
                        if (!string.IsNullOrEmpty(ESTBranchesDropDownResponseJSON))
                        {
                            ESTBranchesDropDownResponseJSON = JObject.Parse(ESTBranchesDropDownResponseJSON)["data"].ToString();
                            outletNumber = JsonConvert.DeserializeObject<OutletNumber>(ESTBranchesDropDownResponseJSON);
                        }
                    }
                }
                catch (JsonReaderException)
                {
                    throw new GAZTInvalidDataException();
                }
                catch (HttpRequestException)
                {
                }
                catch (GAZTException)
                {
                }
                catch (Exception)
                {
                }
            }
            else
            {
                throw new GAZTInternetException();
            }
            return outletNumber;
        }
        public static async Task<OutletDropDowns> ESTOutletDropDowns()
        {
            OutletDropDowns dropDownModels = null;
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    var lang = UtilityManager.GetLanguageParameter();
                    if (!NetworkCheck.IsInternet())
                    {
                        throw new GAZTInternetException();
                    }
                    var url = ZATCAConstants.ESTOutletCityStateCountryDropDown + lang;
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

                    var uri = new Uri(url);
                    HttpResponseMessage ESTBranchesDropDownResponse = await client.GetAsync(uri);
                    //HttpResponseMessage ESTBranchesDropDownResponse = await GetServiceManager.MakeGetAPICall(string.Format("{0}(Spras='{1}',Land1='',Bland='',Cityc='')?&$expand=country_dropdownSet,State_dropdownSet,city_dropdownSet&$format=json",
                    //    Constants.ESTOutletCityStateCountryDropDown, lang), false, "");
                    if (ESTBranchesDropDownResponse != null)
                    {
                        if (ESTBranchesDropDownResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            throw new GAZTSessionExpiredException();
                        }
                        HttpHeaders headers = ESTBranchesDropDownResponse.Headers;
                        IEnumerable<string> values = null;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                        }
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                throw new GAZTSessionExpiredException();
                            }
                            App.Token = NewToken;
                        }
                        string ESTBranchesDropDownResponseJSON = await ESTBranchesDropDownResponse.Content.ReadAsStringAsync();
                        if (!string.IsNullOrEmpty(ESTBranchesDropDownResponseJSON))
                        {
                            ESTBranchesDropDownResponseJSON = JObject.Parse(ESTBranchesDropDownResponseJSON)["data"].ToString();
                            dropDownModels = JsonConvert.DeserializeObject<OutletDropDowns>(ESTBranchesDropDownResponseJSON);
                        }
                    }
                }
                catch (JsonReaderException)
                {
                    throw new GAZTInvalidDataException();
                }
                catch (HttpRequestException)
                {
                }
                catch (GAZTException)
                {
                }
                catch (Exception)
                {
                }
            }
            else
            {
                throw new GAZTInternetException();
            }
            return dropDownModels;
        }
        public static async Task<ActivitySetsList> ESTOutletGetActivitySetsList(string indSector = null)
        {
            ActivitySetsList list = null;
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    var lang = UtilityManager.GetLanguageParameter();
                    indSector = (string.IsNullOrEmpty(indSector) || string.IsNullOrWhiteSpace(indSector)) ? string.Empty : indSector;
                    if (!NetworkCheck.IsInternet())
                    {
                        throw new GAZTInternetException();
                    }
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", "EN");
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    var url = ZATCAConstants.ESTActiivtyGroupSubGroupList + lang + "&industrySector=" + indSector;
                    HttpResponseMessage ESTBranchesDropDownResponse = await client.GetAsync(url);
                    //HttpResponseMessage ESTBranchesDropDownResponse = await GetServiceManager.MakeGetAPICall(string.Format("{0}(Spras='{1}',IndSector='{2}')?&$expand=act_groupSet,act_subgroupSet,activitySet&$format=json",
                    //    Constants.ESTActiivtyGroupSubGroupList, lang, indSector), false, "");
                    if (ESTBranchesDropDownResponse != null)
                    {
                        if (ESTBranchesDropDownResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            throw new GAZTSessionExpiredException();
                        }
                        HttpHeaders headers = ESTBranchesDropDownResponse.Headers;
                        IEnumerable<string> values = null;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                        }
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                throw new GAZTSessionExpiredException();
                            }
                            App.Token = NewToken;
                        }
                        string ESTBranchesDropDownResponseJSON = await ESTBranchesDropDownResponse.Content.ReadAsStringAsync();
                        if (!string.IsNullOrEmpty(ESTBranchesDropDownResponseJSON))
                        {
                            ESTBranchesDropDownResponseJSON = JObject.Parse(ESTBranchesDropDownResponseJSON)["data"].ToString();
                            list = JsonConvert.DeserializeObject<ActivitySetsList>(ESTBranchesDropDownResponseJSON);
                        }
                    }
                }
                catch (JsonReaderException)
                {
                    throw new GAZTInvalidDataException();
                }
                catch (HttpRequestException ex)
                {
                    throw ex;
                }
                catch (GAZTException gex)
                {
                    throw gex;
                }
                catch (Exception)
                {
                }
            }
            else
            {
                throw new GAZTInternetException();
            }
            return list;
        }
        public static async Task<string> ESTValidateCRNum(string cr)
        {
            //ValidateCR validate = null;
            string ESTBranchesDropDownResponseJSON = string.Empty;
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    if (!NetworkCheck.IsInternet())
                    {
                        throw new GAZTInternetException();
                    }

                    var CrNumber = new JProperty("CRNumber", cr);

                    var idset = ZATCAConstants.IdSet.Where(i => (i.Srcidentify == "00000" || i.Srcidentify == "") && (i.Type != "FS0002")).FirstOrDefault();

                    var IdNo = new JProperty("idNumber", idset.Idnumber);
                    var Type = new JProperty("idType", idset.Type);
                    var Gpart = new JProperty("TIN", idset.Gpart);

                    HttpClient client = new HttpClient(App.httpClientHandler);

                    var lang = UtilityManager.GetLanguageParameter();
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);


                    JObject obj = new JObject(CrNumber, IdNo, Type, Gpart);

                    var serilized = JsonConvert.SerializeObject(obj);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage ESTBranchesDropDownResponse = await client.PostAsync(ZATCAConstants.ESTValidateCRNum, contentPost);


                    if (ESTBranchesDropDownResponse != null)
                    {
                        if (ESTBranchesDropDownResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            throw new GAZTSessionExpiredException();
                        }
                        HttpHeaders headers = ESTBranchesDropDownResponse.Headers;
                        IEnumerable<string> values = null;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                        }
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                throw new GAZTSessionExpiredException();
                            }
                            App.Token = NewToken;
                        }
                        ESTBranchesDropDownResponseJSON = await ESTBranchesDropDownResponse.Content.ReadAsStringAsync();
                        if (!string.IsNullOrEmpty(ESTBranchesDropDownResponseJSON))
                        {
                            ESTBranchesDropDownResponseJSON = JObject.Parse(ESTBranchesDropDownResponseJSON)["result"].ToString();
                            //validate = JsonConvert.DeserializeObject<ValidateCR>(ESTBranchesDropDownResponseJSON);
                        }
                    }
                }
                catch (JsonReaderException)
                {
                    throw new GAZTInvalidDataException();
                }
                catch (HttpRequestException)
                {
                }
                catch (GAZTException)
                {
                }
                catch (Exception)
                {
                }
            }
            else
            {
                throw new GAZTInternetException();
            }
            return ESTBranchesDropDownResponseJSON;
        }
        public static async Task<List<OutletItem>> ESTOutletList(string email, string gpart, string fbnum)
        {
            List<OutletItem> outlets = new List<OutletItem>();
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    if (!NetworkCheck.IsInternet())
                    {
                        throw new GAZTInternetException();
                    }
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
                    var url = ZATCAConstants.ESTOutletList + fbnum + "&portalUser=" + email + "&TIN=" + App.LoginDataRetrieved.TIN;
                    var uri = new Uri(url);


                    HttpResponseMessage ESTBranchesDropDownResponse = await client.GetAsync(uri);
                    if (ESTBranchesDropDownResponse != null)
                    {
                        if (ESTBranchesDropDownResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            throw new GAZTSessionExpiredException();
                        }
                        HttpHeaders headers = ESTBranchesDropDownResponse.Headers;
                        IEnumerable<string> values = null;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                        }
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                throw new GAZTSessionExpiredException();
                            }
                            App.Token = NewToken;
                        }
                        string ESTBranchesDropDownResponseJSON = await ESTBranchesDropDownResponse.Content.ReadAsStringAsync();
                        if (!string.IsNullOrEmpty(ESTBranchesDropDownResponseJSON))
                        {
                            var checkObj = JObject.Parse(ESTBranchesDropDownResponseJSON);//["d"].ToString();

                            if (ESTBranchesDropDownResponseJSON != null && checkObj != null)
                            {
                                ESTBranchesDropDownResponseJSON = JObject.Parse(ESTBranchesDropDownResponseJSON)["data"].ToString();
                                ESTBranchesDropDownResponseJSON = JObject.Parse(ESTBranchesDropDownResponseJSON)["outlets"].ToString();
                                outlets = JsonConvert.DeserializeObject<List<OutletItem>>(ESTBranchesDropDownResponseJSON);
                            }
                            else
                            {
                                ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(ESTBranchesDropDownResponseJSON);
                                if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null
                                    && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                                {
                                    string errorCode = errorMesg.error.innererror.errordetails[0].code;

                                    var errorMsg = errorMesg.error.innererror.errordetails[0].message;

                                    string WithReplacedString = errorMsg.Replace("An exception was raised", string.Empty);
                                    errorMsg = WithReplacedString;

                                    throw new GAZTErrorException(errorMsg);
                                }
                            }
                        }
                    }
                }
                catch (JsonReaderException)
                {
                    throw new GAZTInvalidDataException();
                }

                catch (HttpRequestException)
                {
                }
                catch (GAZTException gex)
                {
                    throw new GAZTErrorException(gex.Message);
                }

                catch (Exception)
                {
                }
            }
            else
            {
                throw new GAZTInternetException();
            }
            return outlets;
        }
        public static async Task<List<OutletAddress>> ESTOutletAddress(string idType, string IdNumber, string tin)
        {
            List<OutletAddress> address = new List<OutletAddress>();
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    if (!NetworkCheck.IsInternet())
                    {
                        throw new GAZTInternetException();
                    }
                    string url = ZATCAConstants.ESTOutletAddressFetch;
                    ESTOutletAddressRequest eSTOutletAddressRequest = new ESTOutletAddressRequest();
                    eSTOutletAddressRequest.idNumber = IdNumber;
                    eSTOutletAddressRequest.TIN = tin;
                    eSTOutletAddressRequest.idType = idType;
                    eSTOutletAddressRequest.taxpayerType = "Individual";
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
                    var serilized = JsonConvert.SerializeObject(eSTOutletAddressRequest);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage ESTBranchesDropDownResponse = await client.PostAsync(url, contentPost);
                    var detailJson = ESTBranchesDropDownResponse.Content.ReadAsStringAsync().Result;
                    /*HttpResponseMessage ESTBranchesDropDownResponse = await GetServiceManager.MakeGetAPICall(string.Format("{0}?$format=json&$filter=IdType eq '{1}' and IdNumber eq '{2}' and Tin eq '{3}' and TpType eq 'I'",
                   // Constants.ESTOutletAddressFetch, idType, IdNumber, tin), false, "");*/
                    if (ESTBranchesDropDownResponse != null)
                    {
                        if (ESTBranchesDropDownResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            throw new GAZTSessionExpiredException();
                        }
                        HttpHeaders headers = ESTBranchesDropDownResponse.Headers;
                        IEnumerable<string> values = null;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                        }
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                throw new GAZTSessionExpiredException();
                            }
                            App.Token = NewToken;
                        }
                        string ESTBranchesDropDownResponseJSON = await ESTBranchesDropDownResponse.Content.ReadAsStringAsync();
                        if (!string.IsNullOrEmpty(ESTBranchesDropDownResponseJSON))
                        {
                            ESTBranchesDropDownResponseJSON = JObject.Parse(ESTBranchesDropDownResponseJSON)["data"].ToString();
                            ESTBranchesDropDownResponseJSON = JObject.Parse(ESTBranchesDropDownResponseJSON)["results"].ToString();
                            address = JsonConvert.DeserializeObject<List<OutletAddress>>(ESTBranchesDropDownResponseJSON);
                        }
                    }
                }
                catch (JsonReaderException)
                {
                    throw new GAZTInvalidDataException();
                }
                catch (HttpRequestException)
                {
                }
                catch (GAZTException)
                {
                }
                catch (Exception)
                {
                }
            }
            else
            {
                throw new GAZTInternetException();
            }
            return address;
        }
        public static async Task<string> ESTDeleteOutletItem(string fbnumx, string actno, string email)
        {
            if (NetworkCheck.IsInternet())
            {
                string DeleteToken = string.Empty;
                try
                {
                    var uri = new Uri(ZATCAConstants.ESTDeleteOutlet);
                    var lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient();
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
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "multipart/form-data");
                    var deleteOutletRequest = new DeleteOutletRequest()
                    {
                        formBundleNumber = fbnumx,
                        activityNumber = actno,
                        portalUser = email,
                        TIN = App.LoginDataRetrieved.TIN
                    };
                    //var uri = new Uri(string.Format(Constants.ESTOutletList));
                    var deleteOutletData = JsonConvert.SerializeObject(deleteOutletRequest);
                    HttpContent contentPost = new StringContent(deleteOutletData, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage response = await client.PostAsync(uri, contentPost);
                    //HttpResponseMessage response = client.DeleteAsync(uri).Result;
                    var responsestr = response.Content.ReadAsStringAsync().Result;
                    if (response != null)
                    {
                        HttpHeaders headers = response.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("delete", out values))
                        {
                            DeleteToken = values.First();
                        }
                    }
                    return "delete";
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
        public static async Task<FinancialDetail> ESTFinancialMaxDate(FinancialDetailRequest financialDetailRequest)
        {
            FinancialDetail financial = null;
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    if (!NetworkCheck.IsInternet())
                    {
                        throw new GAZTInternetException();
                    }
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

                    var uri = new Uri(ZATCAConstants.ESTFinancialMaxDate);
                    var financeData = JsonConvert.SerializeObject(financialDetailRequest, new JsonSerializerSettings
                    {
                        DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                        DateTimeZoneHandling = DateTimeZoneHandling.Utc
                    });
                    HttpContent contentPost = new StringContent(financeData, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage ESTBranchesDropDownResponse = await client.PostAsync(uri, contentPost);
                    if (ESTBranchesDropDownResponse != null)
                    {
                        if (ESTBranchesDropDownResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            throw new GAZTSessionExpiredException();
                        }
                        HttpHeaders headers = ESTBranchesDropDownResponse.Headers;
                        IEnumerable<string> values = null;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                        }
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                throw new GAZTSessionExpiredException();
                            }
                            App.Token = NewToken;
                        }
                        string ESTBranchesDropDownResponseJSON = await ESTBranchesDropDownResponse.Content.ReadAsStringAsync();
                        if (!string.IsNullOrEmpty(ESTBranchesDropDownResponseJSON))
                        {
                            ESTBranchesDropDownResponseJSON = JObject.Parse(ESTBranchesDropDownResponseJSON)["result"].ToString();
                            financial = JsonConvert.DeserializeObject<FinancialDetail>(ESTBranchesDropDownResponseJSON);
                        }
                    }
                }
                catch (JsonReaderException)
                {
                    throw new GAZTInvalidDataException();
                }
                catch (HttpRequestException)
                {
                }
                catch (GAZTException)
                {
                }
                catch (Exception)
                {
                }
            }
            else
            {
                throw new GAZTInternetException();
            }
            return financial;
        }

        public static async Task<FinancialDetail> ESTFinancialMaxDateForPeriod(FinancialDetailPeriodRequest financialDetailRequest)
        {
            FinancialDetail financial = null;
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    if (!NetworkCheck.IsInternet())
                    {
                        throw new GAZTInternetException();
                    }
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

                    var uri = new Uri(string.Format(ZATCAConstants.ESTFinancialMaxDate));
                    var financeData = JsonConvert.SerializeObject(financialDetailRequest, new JsonSerializerSettings
                    {
                        DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                        DateTimeZoneHandling = DateTimeZoneHandling.Utc
                    });
                    HttpContent contentPost = new StringContent(financeData, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage ESTBranchesDropDownResponse = await client.PostAsync(uri, contentPost);
                    if (ESTBranchesDropDownResponse != null)
                    {
                        if (ESTBranchesDropDownResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            throw new GAZTSessionExpiredException();
                        }
                        HttpHeaders headers = ESTBranchesDropDownResponse.Headers;
                        IEnumerable<string> values = null;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                        }
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")))
                            {
                                throw new GAZTSessionExpiredException();
                            }
                            App.Token = NewToken;
                        }
                        string ESTBranchesDropDownResponseJSON = await ESTBranchesDropDownResponse.Content.ReadAsStringAsync();
                        if (!string.IsNullOrEmpty(ESTBranchesDropDownResponseJSON))
                        {

                            if (!ESTBranchesDropDownResponseJSON.Contains("An exception was raised") || !ESTBranchesDropDownResponseJSON.Contains("error"))
                            {

                                ESTBranchesDropDownResponseJSON = JObject.Parse(ESTBranchesDropDownResponseJSON)["result"].ToString();
                                financial = JsonConvert.DeserializeObject<FinancialDetail>(ESTBranchesDropDownResponseJSON);
                            }


                        }
                    }
                }
                catch (JsonReaderException)
                {
                    throw new GAZTInvalidDataException();
                }
                catch (HttpRequestException)
                {
                }
                catch (GAZTException)
                {
                }
                catch (Exception)
                {
                    throw new GAZTNetworkConnectivityIssueException();
                }
            }
            else
            {
                throw new GAZTInternetException();
            }
            return financial;
        }

        public static async Task<string> UpdateUserLicenseInActivityPage(UpdateActivityLicenseModel updateActivityModel, string pageType)
        {
            ActivityUpdateViewResponseModel financial = null;
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    if (!NetworkCheck.IsInternet())
                    {
                        throw new GAZTInternetException();
                    }
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    var uri = new Uri(string.Format(ZATCAConstants.UpdateLicenseAndCR));
                    var financeData = JsonConvert.SerializeObject(updateActivityModel);
                    HttpContent contentPost = new StringContent(financeData, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage ESTBranchesDropDownResponse = await client.PostAsync(uri, contentPost);


                    if (ESTBranchesDropDownResponse != null)
                    {
                        if (ESTBranchesDropDownResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            throw new GAZTSessionExpiredException();
                        }
                        HttpHeaders headers = ESTBranchesDropDownResponse.Headers;
                        IEnumerable<string> values = null;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                        }
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")))
                            {
                                throw new GAZTSessionExpiredException();
                            }
                            App.Token = NewToken;
                        }
                        var ESTBranchesDropDownResponseJSON = ESTBranchesDropDownResponse.Content.ReadAsStringAsync().Result;
                        financial = JsonConvert.DeserializeObject<ActivityUpdateViewResponseModel>(ESTBranchesDropDownResponseJSON);

                        if (ESTBranchesDropDownResponse.StatusCode == HttpStatusCode.OK || ESTBranchesDropDownResponse.StatusCode == HttpStatusCode.Created)
                        {
                            if (financial != null && financial.d != null)
                            {
                                if (financial.d.UpdFlg)
                                {
                                    if (pageType.Equals("2"))
                                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZZZCRUpdateSuccess));
                                    else if (pageType.Equals("1"))
                                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.ZZZZLicenseUpdateSuccess));
                                }
                            }
                        }


                        if (ESTBranchesDropDownResponse.StatusCode == HttpStatusCode.BadRequest)
                        {
                            ErrorObj errorMesgs = JsonConvert.DeserializeObject<ErrorObj>(ESTBranchesDropDownResponseJSON);
                            if (errorMesgs != null && errorMesgs.error != null && errorMesgs.error.innererror != null
                                                                && errorMesgs.error.innererror.errordetails != null && errorMesgs.error.innererror.errordetails[0].message != null)
                            {
                                string errorCode = errorMesgs.error.innererror.errordetails[0].code;

                                var errorMsg = errorMesgs.error.innererror.errordetails[0].message;

                                await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(errorMsg));

                            }
                        }
                    }
                }
                catch (JsonReaderException)
                {
                    throw new GAZTInvalidDataException();
                }
                catch (HttpRequestException)
                {
                }
                catch (GAZTException)
                {
                }
                catch (Exception)
                {
                    throw new GAZTNetworkConnectivityIssueException();
                }
            }
            else
            {
                throw new GAZTInternetException();
            }
            return "";
        }
        #endregion
    }
}
