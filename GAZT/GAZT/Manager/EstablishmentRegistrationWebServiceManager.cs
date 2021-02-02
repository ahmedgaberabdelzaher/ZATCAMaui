using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EGAZT.Models;
using EGAZT.Models.EstablishmentRegistration;
using GAZT.Helper;
using GAZT.Manager;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Connectivity;
using Xamarin.Forms.Internals;
using static GAZT.ErrorMessage;

namespace EGAZT.Manager
{
    [Preserve(AllMembers = true)]
    public static class EstablishmentRegistrationWebServiceManager
    {
        #region Establishment Registration API Calls
        public static async Task<List<BranchesDropDownModel>> ESTBranchesDropDown()
        {
            List<BranchesDropDownModel> dropDownModels = new List<BranchesDropDownModel>();
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    char lang = WebServiceManager.GetLangZParameter();
                    if (false == CrossConnectivity.Current.IsConnected)
                    {
                        throw new GAZTInternetException();
                    }
                    HttpResponseMessage ESTBranchesDropDownResponse = await GetServiceManager.MakeGetAPICall(string.Format("{0}?&$format=json&$filter=Spras eq '{1}'", Constants.ESTBranchesDropDown, lang), false, "");
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
                            ESTBranchesDropDownResponseJSON = JObject.Parse(ESTBranchesDropDownResponseJSON)["d"].ToString();
                            ESTBranchesDropDownResponseJSON = JObject.Parse(ESTBranchesDropDownResponseJSON)["results"].ToString();
                            dropDownModels = JsonConvert.DeserializeObject<List<BranchesDropDownModel>>(ESTBranchesDropDownResponseJSON);
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
                    throw new GAZTNetworkConnectivityIssueException();
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
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                srcidentify = (string.IsNullOrEmpty(srcidentify) || string.IsNullOrWhiteSpace(srcidentify)) ? string.Empty : string.Format("O{0}", srcidentify);
                Fbnum = (string.IsNullOrEmpty(Fbnum) || string.IsNullOrWhiteSpace(Fbnum)) ? string.Empty : Fbnum;
                try
                {
                    char lang = WebServiceManager.GetLangZParameter();
                    if (false == CrossConnectivity.Current.IsConnected)
                    {
                        throw new GAZTInternetException();
                    }
                    HttpResponseMessage ESTBranchesDropDownResponse = await GetServiceManager.MakeGetAPICall(string.Format("{0}(Euser='',Fbguid='',Gpartx='{1}',Langx='{2}',Operationx='',PortalUsrx='{3}',Srcidentifyx='{4}',StepNumberx='{5}',Fbnumx='{6}',Fbstax='',Fbustx='')?&$expand=Nreg_ActivitySet,Nreg_AddressSet,Nreg_ContactSet,Nreg_CpersonSet,Nreg_IdSet,Nreg_OutletSet,Nreg_ShareholderSet,Nreg_FormEdit,Nreg_BtnSet,off_notesSet,AttDetSet,Nreg_MSGSet&$format=json",
                        Constants.ESTTaxPayerDetails, TIN, lang, emailID, srcidentify, step, Fbnum), false, "");
                    if (ESTBranchesDropDownResponse != null)
                    {
                        if (ESTBranchesDropDownResponse.StatusCode == HttpStatusCode.BadRequest)
                        {
                            String _responseData = ESTBranchesDropDownResponse.Content.ReadAsStringAsync().Result;
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

                                String WithReplacedString = errorMsg.Replace("An exception was raised", string.Empty);
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
                            try
                            {
                                ESTBranchesDropDownResponseJSON = JObject.Parse(ESTBranchesDropDownResponseJSON)["d"].ToString();
                            }
                            catch (Exception)
                            {

                            }
                            taxPayer = JsonConvert.DeserializeObject<TaxPayerDetails>(ESTBranchesDropDownResponseJSON);
                        }
                    }
                }
                catch (GAZTErrorException ex)
                {
                    throw ex;
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
                    throw new GAZTNetworkConnectivityIssueException();
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
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                srcidentify = (string.IsNullOrEmpty(srcidentify) || string.IsNullOrWhiteSpace(srcidentify)) ? string.Empty : string.Format("O{0}", srcidentify);
                Fbnum = (string.IsNullOrEmpty(Fbnum) || string.IsNullOrWhiteSpace(Fbnum)) ? string.Empty : Fbnum;
                try
                {
                    char lang = WebServiceManager.GetLangZParameter();
                    if (false == CrossConnectivity.Current.IsConnected)
                    {
                        throw new GAZTInternetException();
                    }
                    var uri = Constants.ESTTaxPayerDetails + "(" + "Gpartx='" + App.LoginDataRetrieved.TIN + "',Langx='" + lang + "',Operationx='" + "',PortalUsrx='" + emailID + "',Srcidentifyx='" + srcidentify + "',StepNumberx='" + step + "',Euser='" + "',Fbguid='" + "',Fbnumx='" + Fbnum + "',Fbstax='" + Fbstax + "',Fbustx='" + Fbustx + "')?&$expand=Nreg_ActivitySet,Nreg_AddressSet,Nreg_ContactSet,Nreg_CpersonSet,Nreg_IdSet,Nreg_OutletSet,Nreg_ShareholderSet,AttDetSet,Nreg_MSGSet&$format=json";
                    HttpResponseMessage ESTBranchesDropDownResponse = await GetServiceManager.MakeGetAPICall(uri, false, "");
                    if (ESTBranchesDropDownResponse != null)
                    {
                        if (ESTBranchesDropDownResponse.StatusCode == HttpStatusCode.BadRequest)
                        {
                            String _responseData = ESTBranchesDropDownResponse.Content.ReadAsStringAsync().Result;
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

                                String WithReplacedString = errorMsg.Replace("An exception was raised", string.Empty);
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
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")))
                            {
                                throw new GAZTSessionExpiredException();
                            }
                            App.Token = NewToken;
                        }
                        string ESTBranchesDropDownResponseJSON = await ESTBranchesDropDownResponse.Content.ReadAsStringAsync();
                        string jsonReplace = ESTBranchesDropDownResponseJSON.Replace("\"Begda\":\"\\/Date(-6", "\"Begda\":\"\\/Date(");
                        string replaceDString = string.Empty;
                        if (!string.IsNullOrEmpty(ESTBranchesDropDownResponseJSON))
                        {
                            try
                            {
                                replaceDString = JObject.Parse(jsonReplace)["d"].ToString();

                            }
                            catch (Exception ex)
                            {

                            }
                            taxPayer = JsonConvert.DeserializeObject<TaxPayerDetails>(replaceDString);
                        }
                    }
                }
                catch (GAZTErrorException ex)
                {
                    throw ex;
                }
                catch (JsonReaderException ex)
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
                catch (Exception ex)
                {
                    throw new GAZTNetworkConnectivityIssueException();
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
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    char lang = WebServiceManager.GetLangZParameter();
                    if (false == CrossConnectivity.Current.IsConnected)
                    {
                        throw new GAZTInternetException();
                    }
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    client.Timeout = TimeSpan.FromMinutes(10);

                    client.DefaultRequestHeaders.Add("Token", App.Token);
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    var serializeOptions = new JsonSerializerSettings
                    {
                        DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                        DateTimeZoneHandling = DateTimeZoneHandling.Utc
                    };
                    serializeOptions.Converters.Add(new JsonFieldListConverter());
                    var serialized = JsonConvert.SerializeObject(taxPayer, serializeOptions);

                    HttpContent contentPost = new StringContent(serialized, Encoding.UTF8, Constants.ContentType);

                    HttpResponseMessage ESTBranchesDropDownResponse = await client.PostAsync(new Uri(string.Format("{0}?sap-language={1}", Constants.ESTTaxPayerDetails, lang)), contentPost);
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
                        if (ESTBranchesDropDownResponse.StatusCode == HttpStatusCode.BadRequest)
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
                        else
                        {
                            if (!string.IsNullOrEmpty(ESTBranchesDropDownResponseJSON))
                            {
                                ESTBranchesDropDownResponseJSON = JObject.Parse(ESTBranchesDropDownResponseJSON)["d"].ToString();
                                taxPayer = JsonConvert.DeserializeObject<TaxPayerDetails>(ESTBranchesDropDownResponseJSON);
                            }
                        }
                    }
                }
                catch (JsonReaderException ex)
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
                catch (Exception ex)
                {
                    throw new GAZTNetworkConnectivityIssueException();
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
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                nationality = (string.IsNullOrEmpty(nationality) || string.IsNullOrWhiteSpace(nationality)) ? "SAUDI" : nationality;
                try
                {
                    char lang = WebServiceManager.GetLangZParameter();
                    if (false == CrossConnectivity.Current.IsConnected)
                    {
                        throw new GAZTInternetException();
                    }
                    HttpResponseMessage ESTBranchesDropDownResponse = await GetServiceManager.MakeGetAPICall(string.Format("{0}?&$format=json&$filter=ANationality eq '{1}' and Spras eq '{2}'",
                       Constants.ESTTaxPayerNationality, nationality, lang), false, "");
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
                            ESTBranchesDropDownResponseJSON = JObject.Parse(ESTBranchesDropDownResponseJSON)["d"].ToString();
                            ESTBranchesDropDownResponseJSON = JObject.Parse(ESTBranchesDropDownResponseJSON)["results"].ToString();
                            nationalities = JsonConvert.DeserializeObject<List<TaxpayerNationality>>(ESTBranchesDropDownResponseJSON);
                        }
                    }
                }
                catch (JsonReaderException ex)
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
                catch (Exception ex)
                {
                    throw new GAZTNetworkConnectivityIssueException();
                }
            }
            else
            {
                throw new GAZTInternetException();
            }
            return nationalities;
        }
        public static async Task<Attachment> ESTAttachment(byte[] AttachmentByte, string fileName, string RetGuid, string Doctype, string contentType, string outletref = null) //RG16 for Residency, RG19 for passport RG01 for CR copy RG02 licence copy
        {
            char lang = WebServiceManager.GetLangZParameter();
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    outletref = (string.IsNullOrEmpty(outletref) || string.IsNullOrWhiteSpace(outletref)) ? string.Empty : outletref;

                    var uri = new Uri(string.Format("{0}(RetGuid='{1}',OutletRef='{2}',Flag='N',Dotyp='{3}',SchGuid='',Srno=1,Doguid='',AttBy='TP')/AttachMedSet",
                        Constants.ESTPostAttachment, RetGuid, outletref, Doctype));

                    HttpClient client = new HttpClient(App.httpClientHandler);

                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("slug", WebUtility.UrlEncode(fileName));

                    ByteArrayContent baContent = new ByteArrayContent(AttachmentByte);
                    if (!string.IsNullOrEmpty(contentType))
                        baContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                    var response = await client.PostAsync(uri, baContent);
                    var responsestr = response.Content.ReadAsStringAsync().Result;
                    responsestr = JObject.Parse(responsestr)["d"].ToString();
                    Attachment _attachment = JsonConvert.DeserializeObject<Attachment>(responsestr);
                    return _attachment;
                }
                catch (Exception ex)
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
            if (CrossConnectivity.Current.IsConnected)
            {
                string DeleteToken = string.Empty;
                try
                {
                    var uri = new Uri(string.Format("{0}(RetGuid='{1}',Flag='N',OutletRef='',Dotyp='{2}',SchGuid='',Srno=1,Doguid='{3}',AttBy='X')/$value",
                        Constants.ESTDeleteAttachment, RetGuid, docType, docguid));
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("slug", WebUtility.UrlEncode(fileName));

                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "multipart/form-data");

                    HttpResponseMessage response = client.DeleteAsync(uri).Result;
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
                catch (Exception ex)
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
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    if (false == CrossConnectivity.Current.IsConnected)
                    {
                        throw new GAZTInternetException();
                    }
                    HttpResponseMessage ESTBranchesDropDownResponse = await GetServiceManager.MakeGetAPICall(string.Format("{0}(Fbnum='{1}',Gpart='')?&$format=json",
                        Constants.ESTOutletNumber, Fbnum), false, "");
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
                            ESTBranchesDropDownResponseJSON = JObject.Parse(ESTBranchesDropDownResponseJSON)["d"].ToString();
                            outletNumber = JsonConvert.DeserializeObject<OutletNumber>(ESTBranchesDropDownResponseJSON);
                        }
                    }
                }
                catch (JsonReaderException ex)
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
                catch (Exception ex)
                {
                    throw new GAZTNetworkConnectivityIssueException();
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
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    if (false == CrossConnectivity.Current.IsConnected)
                    {
                        throw new GAZTInternetException();
                    }
                    HttpResponseMessage ESTBranchesDropDownResponse = await GetServiceManager.MakeGetAPICall(string.Format("{0}(Fbnum='{1}',Gpart='{2}')?&$format=json",
                        Constants.ESTOutletNumber, Fbnum, tin), false, "");
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
                            ESTBranchesDropDownResponseJSON = JObject.Parse(ESTBranchesDropDownResponseJSON)["d"].ToString();
                            outletNumber = JsonConvert.DeserializeObject<OutletNumber>(ESTBranchesDropDownResponseJSON);
                        }
                    }
                }
                catch (JsonReaderException ex)
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
                catch (Exception ex)
                {
                    throw new GAZTNetworkConnectivityIssueException();
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
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    char lang = WebServiceManager.GetLangZParameter();
                    if (false == CrossConnectivity.Current.IsConnected)
                    {
                        throw new GAZTInternetException();
                    }

                    HttpResponseMessage ESTBranchesDropDownResponse = await GetServiceManager.MakeGetAPICall(string.Format("{0}(Spras='{1}',Land1='',Bland='',Cityc='')?&$expand=country_dropdownSet,State_dropdownSet,city_dropdownSet&$format=json",
                        Constants.ESTOutletCityStateCountryDropDown, lang), false, "");
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
                            ESTBranchesDropDownResponseJSON = JObject.Parse(ESTBranchesDropDownResponseJSON)["d"].ToString();
                            dropDownModels = JsonConvert.DeserializeObject<OutletDropDowns>(ESTBranchesDropDownResponseJSON);
                        }
                    }
                }
                catch (JsonReaderException ex)
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
                catch (Exception ex)
                {
                    throw new GAZTNetworkConnectivityIssueException();
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
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    char lang = WebServiceManager.GetLangZParameter();
                    indSector = (string.IsNullOrEmpty(indSector) || string.IsNullOrWhiteSpace(indSector)) ? string.Empty : indSector;
                    if (false == CrossConnectivity.Current.IsConnected)
                    {
                        throw new GAZTInternetException();
                    }
                    HttpResponseMessage ESTBranchesDropDownResponse = await GetServiceManager.MakeGetAPICall(string.Format("{0}(Spras='{1}',IndSector='{2}')?&$expand=act_groupSet,act_subgroupSet,activitySet&$format=json",
                        Constants.ESTActiivtyGroupSubGroupList, lang, indSector), false, "");
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
                            ESTBranchesDropDownResponseJSON = JObject.Parse(ESTBranchesDropDownResponseJSON)["d"].ToString();
                            list = JsonConvert.DeserializeObject<ActivitySetsList>(ESTBranchesDropDownResponseJSON);
                        }
                    }
                }
                catch (JsonReaderException ex)
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
                catch (Exception ex)
                {
                    throw new GAZTNetworkConnectivityIssueException();
                }
            }
            else
            {
                throw new GAZTInternetException();
            }
            return list;
        }
        public static async Task<ValidateCR> ESTValidateCRNum(string cr)
        {
            ValidateCR validate = null;
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    if (false == CrossConnectivity.Current.IsConnected)
                    {
                        throw new GAZTInternetException();
                    }
                    HttpResponseMessage ESTBranchesDropDownResponse = await GetServiceManager.MakeGetAPICall(string.Format("{0}(Crnum='{1}')?$format=json",
                        Constants.ESTValidateCRNum, cr), false, "");
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
                            ESTBranchesDropDownResponseJSON = JObject.Parse(ESTBranchesDropDownResponseJSON)["d"].ToString();
                            validate = JsonConvert.DeserializeObject<ValidateCR>(ESTBranchesDropDownResponseJSON);
                        }
                    }
                }
                catch (JsonReaderException ex)
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
                catch (Exception ex)
                {
                    throw new GAZTNetworkConnectivityIssueException();
                }
            }
            else
            {
                throw new GAZTInternetException();
            }
            return validate;
        }
        public static async Task<List<OutletItem>> ESTOutletList(string email, string gpart, string fbnum)
        {
            List<OutletItem> outlets = new List<OutletItem>();
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    if (false == CrossConnectivity.Current.IsConnected)
                    {
                        throw new GAZTInternetException();
                    }
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var uri = new Uri(string.Format("{0}/?&$format=json&$filter=PortalUsrx eq '{1}' and Gpartx eq '{2}' and Fbnumx eq '{3}' and Actno eq ''",
                        Constants.ESTOutletList, email, gpart, fbnum));
                    HttpResponseMessage ESTBranchesDropDownResponse = await GetServiceManager.MakeGetAPICall(string.Format("{0}/?&$format=json&$filter=PortalUsrx eq '{1}' and Gpartx eq '{2}' and Fbnumx eq '{3}' and Actno eq ''",
                        Constants.ESTOutletList, email, gpart, fbnum), false, "");
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
                            var checkObj = JObject.Parse(ESTBranchesDropDownResponseJSON);//["d"].ToString();

                            if (ESTBranchesDropDownResponseJSON != null && checkObj != null)
                            {
                                ESTBranchesDropDownResponseJSON = JObject.Parse(ESTBranchesDropDownResponseJSON)["d"].ToString();
                                ESTBranchesDropDownResponseJSON = JObject.Parse(ESTBranchesDropDownResponseJSON)["results"].ToString();
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

                                    String WithReplacedString = errorMsg.Replace("An exception was raised", string.Empty);
                                    errorMsg = WithReplacedString;

                                    throw new GAZTErrorException(errorMsg);
                                }
                            }
                        }
                    }
                }
                catch (JsonReaderException ex)
                {
                    throw new GAZTInvalidDataException();
                }

                catch (HttpRequestException ex)
                {
                    throw ex;
                }
                catch (GAZTException gex)
                {
                    throw new GAZTErrorException(gex.Message);
                }

                catch (Exception ex)
                {
                    throw new GAZTNetworkConnectivityIssueException();
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
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    if (false == CrossConnectivity.Current.IsConnected)
                    {
                        throw new GAZTInternetException();
                    }
                    HttpResponseMessage ESTBranchesDropDownResponse = await GetServiceManager.MakeGetAPICall(string.Format("{0}?$format=json&$filter=IdType eq '{1}' and IdNumber eq '{2}' and Tin eq '{3}' and TpType eq 'I'",
                        Constants.ESTOutletAddressFetch, idType, IdNumber, tin), false, "");
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
                            ESTBranchesDropDownResponseJSON = JObject.Parse(ESTBranchesDropDownResponseJSON)["d"].ToString();
                            ESTBranchesDropDownResponseJSON = JObject.Parse(ESTBranchesDropDownResponseJSON)["results"].ToString();
                            address = JsonConvert.DeserializeObject<List<OutletAddress>>(ESTBranchesDropDownResponseJSON);
                        }
                    }
                }
                catch (JsonReaderException ex)
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
                catch (Exception ex)
                {
                    throw new GAZTNetworkConnectivityIssueException();
                }
            }
            else
            {
                throw new GAZTInternetException();
            }
            return address;
        }
        public static string ESTDeleteOutletItem(string fbnumx, string actno, string email)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                string DeleteToken = string.Empty;
                try
                {
                    var uri = new Uri(string.Format("{0}(Fbnumx='{1}',Actno='{2}',PortalUsrx='{3}',Gpartx='')",
                        Constants.ESTOutletList, fbnumx, actno, email));
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "multipart/form-data");

                    HttpResponseMessage response = client.DeleteAsync(uri).Result;
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
                catch (Exception ex)
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
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    if (false == CrossConnectivity.Current.IsConnected)
                    {
                        throw new GAZTInternetException();
                    }
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    client.DefaultRequestHeaders.Add("Token", App.Token);
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    var uri = new Uri(string.Format(Constants.ESTFinancialMaxDate));
                    var financeData = JsonConvert.SerializeObject(financialDetailRequest, new JsonSerializerSettings
                    {
                        DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                        DateTimeZoneHandling = DateTimeZoneHandling.Utc
                    });
                    HttpContent contentPost = new StringContent(financeData, Encoding.UTF8, Constants.ContentType);
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
                            ESTBranchesDropDownResponseJSON = JObject.Parse(ESTBranchesDropDownResponseJSON)["d"].ToString();
                            financial = JsonConvert.DeserializeObject<FinancialDetail>(ESTBranchesDropDownResponseJSON);
                        }
                    }
                }
                catch (JsonReaderException ex)
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
                catch (Exception ex)
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
        #endregion
    }
}
