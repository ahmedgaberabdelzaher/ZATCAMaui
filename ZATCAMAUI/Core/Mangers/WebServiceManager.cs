using AppDynamics.Agent;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Mopups.Services;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Interfaces;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.AccountStatements;
using ZATCAMAUI.Models.PaymentModel;
using ZATCAMAUI.Models.SyncfusionEnabledModels;
using ZATCAMAUI.Models.TPProfile;
using ZATCAMAUI.Models.ZakatInstalationModels;
using ZATCAMAUI.Views.NewDesign.EstimatedZAKATReturnsPages;
using static ZATCAMAUI.Models.ErrorMessage;
using static ZATCAMAUI.Models.Nafat.LoginSSOModel;
using static ZATCAMAUI.Models.VATgoodsOnprofit.NewYesorNoPageModel;
using static ZATCAMAUI.Models.correspdncAttchModel;
using ZATCAMAUI.Models.AccountDetails;
using ZATCAMAUI.Models.NewModelAPI.AbsherOTP;
using ZATCAMAUI.Models.ForgotModel;
using static ZATCAMAUI.Models.LoginSSOModelERAD;
using ZATCAMAUI.Models.NewModelAPI;
using ZATCAMAUI.Models.SignUP;
using ZATCAMAUI.Models.Authentication;
using ZATCAMAUI.Models.NewModelAPI.Logout;
using ZATCAMAUI.Models.AttachmentRequest;
using Microsoft.Maui;

namespace ZATCAMAUI.Core.Mangers
{

    public static class WebServiceManager
    {
        public static string ErrorMessage = string.Empty;
        public static string ErrorMessageForVAT = string.Empty;
        public static string NumberOfValiedAttempts = string.Empty;
        public static string ErrorMessageForUnlockAccount = string.Empty;
        public static char GetLangZParameter()
        {
            if (App.IsArabic)
                return 'A';
            else
                return 'E';
        }
        public static String GetLangZParameterAREN()
        {
            if (App.IsArabic)
                return "AR";
            else
                return "EN";
        }
        private static HttpWebRequest CreateGAZTSOAPWebRequestForAuthenticationService()
        {
            //Making Web Request  
            HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(ZATCAConstants.GAZTSOAPWebRequestForAuthenticationService);
            //SOAPAction  
            Req.Headers.Add(@"SOAPAction:http://tempuri.org/IsAuthenticated");
            //Content_type  
            Req.ContentType = "text/xml;charset=\"utf-8\"";
            Req.Accept = "text/xml";
            //HTTP method  
            Req.Method = "POST";
            //return HttpWebRequest  
            return Req;
        }

        //<Summary>
        public static async Task<bool> GAZTCheckConnectivity()
        {
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    String url = ZATCAConstants.BaseUrlOfODataServices;
                    HttpResponseMessage GAZTGetTINsResponse = await GetServiceManager.MakeGetAPICall(url, false, string.Empty);
                    return true;
                }
                catch (Exception)
                {


                    throw new Exception(AppResources.NetworkConnectivityIssue);
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        public static async Task<List<TINModel>> GAZTGetAllTins(String Username)
        {
            if (NetworkCheck.IsInternet())
            {
                String GAZTGetTINsResponseResult = String.Empty;
                List<TINModel> TINs = null;
                try
                {
                    String url = ZATCAConstants.GetAllTin + Username;

                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);

                    var uri = new Uri(url);
                    HttpResponseMessage GAZTGetTINsResponse = await client.GetAsync(uri);

                    //HttpResponseMessage GAZTGetTINsResponse = await GetServiceManager.MakeGetAPICall(url, false, string.Empty);

                    if (GAZTGetTINsResponse != null)
                    {
                        GAZTGetTINsResponseResult = GAZTGetTINsResponse.Content.ReadAsStringAsync().Result;
                    }
                    if (!string.IsNullOrEmpty(GAZTGetTINsResponseResult))
                    {
                        GAZTGetTINsResponseResult = JObject.Parse(GAZTGetTINsResponseResult)["data"].ToString();
                        string GAZTGetTINSResponseJSONJToken = JObject.Parse(GAZTGetTINsResponseResult)["taxpayers"].ToString();

                        TINs = JsonConvert.DeserializeObject<List<TINModel>>(GAZTGetTINSResponseJSONJToken);
                        if (TINs == null)
                        {
                            throw new Exception(AppResources.NoTINsAvailable);
                        }
                    }
                    return TINs;
                }
                catch (Exception ex)
                {
                    if (string.Equals(ex.Message, AppResources.NoTINsAvailable))
                    {
                        throw new Exception(AppResources.NoTINsAvailable);
                    }
                    else
                    {
                        throw new Exception(AppResources.NetworkConnectivityIssue);
                    }
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }
        public static async Task<ObservableCollection<AccountStatus>> GAZTGETBillsSTS(String Tin, string lang, string requestHeader)
        {
            {
                if (NetworkCheck.IsInternet())
                {
                    ObservableCollection<AccountStatus> myBillsSTS = new ObservableCollection<AccountStatus>();
                    String MobileNumber = string.Empty;
                    string PdfUrl = string.Empty;
                    string NewToken = string.Empty;
                    try
                    {
                        //HttpClient client = new HttpClient(App.httpClientHandler);

                        //client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                        //client.DefaultRequestHeaders.Add("ServiceType", requestHeader);
                        //filter=ZtpaccSts eq '' & sap-ui-language eq '" +this.lang +"'&$format=json"
                        //String url = ZATCAConstants.ZATCAAccStmtsStatusDtls + "filter=ZtpaccSts&$format=json&sap-language=" + lang;
                        HttpClient client = new HttpClient(App.httpClientHandler);
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
                        String url = ZATCAConstants.ZATCAAccStmtsStatusDtls;
                        var uri = new Uri(url);
                        HttpResponseMessage GAZTMyBillsResponse = await client.GetAsync(uri);
                        if (GAZTMyBillsResponse != null)
                        {
                            if (GAZTMyBillsResponse.StatusCode == HttpStatusCode.Unauthorized)
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            HttpHeaders headers = GAZTMyBillsResponse.Headers;
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
                            String GAZTMyBillsResponseJSON = GAZTMyBillsResponse.Content.ReadAsStringAsync().Result;
                            if (!string.IsNullOrEmpty(GAZTMyBillsResponseJSON))
                            {
                                GAZTMyBillsResponseJSON = JObject.Parse(GAZTMyBillsResponseJSON)["data"].ToString();

                                string GAZTMyBillsResponseJSONJToken = JObject.Parse(GAZTMyBillsResponseJSON)["paymentStatus"].ToString();
                                if (string.IsNullOrEmpty(GAZTMyBillsResponseJSON) != true)
                                {

                                    myBillsSTS = JsonConvert.DeserializeObject<ObservableCollection<AccountStatus>>(GAZTMyBillsResponseJSONJToken);
                                }
                                else
                                {
                                    throw new Exception(AppResources.NoBillsAvailable);
                                }
                            }
                            else
                            {
                                throw new Exception(AppResources.NoBillsAvailable);
                            }
                        }
                        return myBillsSTS;
                    }
                    catch (Exception ex)
                    {
                        var mock = new ObservableCollection<AccountStatus>();
                        mock.Add(new AccountStatus() { ZtpaccSts = "PD", PymtStatus = "Paid" });
                        mock.Add(new AccountStatus() { ZtpaccSts = "PP", PymtStatus = "Partially Paid" });
                        mock.Add(new AccountStatus() { ZtpaccSts = "UP", PymtStatus = "Unpaid" });
                        return mock;
                        
                        

                        if (string.Equals(ex.Message, AppResources.NoBillsAvailable))
                        {
                            throw new Exception(AppResources.NoBillsAvailable);
                        }
                        else
                        {
                            throw new Exception(AppResources.NetworkConnectivityIssue);
                        }
                    }
                }
                else
                {
                    throw new InternetException(AppResources.ZZInternetConnectionMessage);
                }
            }


        }
        public static async Task<ObservableCollection<MyBills>> GAZTGetMyBills(String Tin, string lang, string requestHeader)
        {
            if (NetworkCheck.IsInternet())
            {
                ObservableCollection<MyBills> myBills = new ObservableCollection<MyBills>();
                String MobileNumber = string.Empty;
                string PdfUrl = string.Empty;
                string NewToken = string.Empty;
                try
                {
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

                    String url = ZATCAConstants.GetMyBills + App.TP.TIN;
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTMyBillsResponse = await client.GetAsync(uri);
                    if (GAZTMyBillsResponse != null)
                    {
                        if (GAZTMyBillsResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTMyBillsResponse.Headers;
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
                        String GAZTMyBillsResponseJSON = GAZTMyBillsResponse.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(GAZTMyBillsResponseJSON))
                        {
                            GAZTMyBillsResponseJSON = JObject.Parse(GAZTMyBillsResponseJSON)["data"].ToString();
                            if (string.IsNullOrEmpty(GAZTMyBillsResponseJSON) != true)
                            {
                                myBills = JsonConvert.DeserializeObject<ObservableCollection<MyBills>>(GAZTMyBillsResponseJSON);
                            }
                            else
                            {
                                throw new Exception(AppResources.NoBillsAvailable);
                            }
                        }
                        else
                        {
                            throw new Exception(AppResources.NoBillsAvailable);
                        }
                    }
                    return myBills;
                }
                catch (Exception ex)
                {
                    if (string.Equals(ex.Message, AppResources.NoBillsAvailable))
                    {
                        throw new Exception(AppResources.NoBillsAvailable);
                    }
                    else
                    {
                        throw new Exception(AppResources.NetworkConnectivityIssue);
                    }
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }
        public static ObservableCollection<CorrDetails> ZATCACorrespondenceDetails(string fbnum, string cotype, string cokey)
        {
            if (NetworkCheck.IsInternet())
            {

                string NewToken = string.Empty;
                try
                {
                    //HttpClient client = new HttpClient(App.httpClientHandler);
                    //client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    // client.DefaultRequestHeaders.Add("ServiceType", requestHeader);
                    // String url = "https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/Z_TP_CORRES_SRV/CorrOffAttSet?$filter=Fbnum%20eq%20%2793000003406%27";
                    string lang = UtilityManager.GetLanguageParameter();
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
                    String url = ZATCAConstants.ZATCACRPDCATT + "?formBundleNumber=" + fbnum + "&correspondenceKey=" + cokey + "&correspondenceType=" + cotype;
                    //correspondenceKey ,correspondenceType
                    var uri = new Uri(url);
                    HttpResponseMessage ZATCAResponsce = client.GetAsync(uri).Result;
                    if (ZATCAResponsce != null)
                    {
                        if (ZATCAResponsce.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;

                        }
                        HttpHeaders headers = ZATCAResponsce.Headers;
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

                            }
                            App.Token = NewToken;
                        }
                        String GAZTMyBillsResponseJSON = ZATCAResponsce.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(GAZTMyBillsResponseJSON))
                        {
                            string GAZTMyBillsResponseJSON2 = JObject.Parse(GAZTMyBillsResponseJSON)["data"].ToString();
                            string GAZTMyBillsResponseJSONJToken = JObject.Parse(GAZTMyBillsResponseJSON2)["correspondenceAttachments"].ToString();
                            if (!string.IsNullOrEmpty(GAZTMyBillsResponseJSONJToken))
                            {

                                ObservableCollection<CorrDetails> correspdncAttchModel1 = JsonConvert.DeserializeObject<ObservableCollection<CorrDetails>>(GAZTMyBillsResponseJSONJToken);
                                if (correspdncAttchModel1 != null) { }
                                //  Console.WriteLine(GAZTMyBillsResponseJSONJToken.d.BILL_DTLSet.results[0].RetFbnum);
                                return correspdncAttchModel1;

                            }
                            else
                            {
                                throw new Exception(AppResources.Nodataavailable);
                            }
                        }
                        else
                        {
                            throw new Exception(AppResources.Nodataavailable);
                        }
                    }

                    return null;
                }

                catch (Exception ex)
                {
                    
                    

                    return null;
                }
            }
            else
            {
                throw new Exception(AppResources.NetworkConnectivityIssue);
            }
        }
        public static AccoungtDetails ZATCAAccGetDetails(string Obpel, string Fbnum, string lang, string requestHeader)
        {
            if (NetworkCheck.IsInternet())
            {
                //Opbel =% 2712000050065 % 27,Fbnum =% 2710000026885 % 27)?&$expand = BILL_DTLSet,INSTL_DTLSet,OBJ_DTLSet,RET_DTLSet\\
                //https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/Z_ACC_STAT_DETAILS_SRV/ACC_DTLSet(Opbel='1000174233',Fbnum='26000004939')?&$expand=BILL_DTLSet,INSTL_DTLSet,OBJ_DTLSet,RET_DTLSet&$format=json

                string NewToken = string.Empty;
                try
                {
                    //HttpClient client = new HttpClient(App.httpClientHandler);

                    //client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    //client.DefaultRequestHeaders.Add("ServiceType", requestHeader);
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    //var lang = UtilityManager.GetLanguageParameter();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);

                    // String url = "https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/Z_ACC_STAT_DETAILS_SRV/ACC_DTLSet(Opbel='1000174233',Fbnum='26000004939')?&$expand=BILL_DTLSet,INSTL_DTLSet,OBJ_DTLSet,RET_DTLSet&$format=json";
                    String url = ZATCAConstants.ZATCAAccStmtsDetails + Obpel + "&formBundleNumber=" + Fbnum;
                    // String url = ZATCAConstants.ZATCAAccStmtsDetails + "Opbel='" + Obpel + "'," + "Fbnum='" + Fbnum + "')?&$expand=BILL_DTLSet,INSTL_DTLSet,OBJ_DTLSet,RET_DTLSet&$format=json";
                    var uri = new Uri(url);
                    HttpResponseMessage ZATCAResponsce = client.GetAsync(uri).Result;
                    if (ZATCAResponsce != null)
                    {
                        if (ZATCAResponsce.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;

                        }
                        HttpHeaders headers = ZATCAResponsce.Headers;
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

                            }
                            App.Token = NewToken;
                        }
                        String GAZTMyBillsResponseJSON = ZATCAResponsce.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(GAZTMyBillsResponseJSON))
                        {
                            try
                            {
                                AccoungtDetails GAZTMyBillsResponseJSONJToken = JsonConvert.DeserializeObject<AccoungtDetails>(GAZTMyBillsResponseJSON);
                                if (GAZTMyBillsResponseJSONJToken != null)
                                {
                                    return GAZTMyBillsResponseJSONJToken;
                                }
                                else
                                {
                                    ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(GAZTMyBillsResponseJSON);
                                    if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                                    {
                                        WebServiceManager.ErrorMessageForVAT = errorMesg.error.innererror.errordetails[0].message;
                                        WebServiceManager.ErrorMessageForVAT += errorMesg.error.innererror.errordetails[1].message;
                                        String WithReplacedString = WebServiceManager.ErrorMessageForVAT.Replace("An exception was raised", string.Empty);
                                        WebServiceManager.ErrorMessageForVAT = WithReplacedString;
                                        //ErrorMessageForVAT
                                        throw new GAZTVATRegistrationInProcessException(WebServiceManager.ErrorMessageForVAT);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.Message);
                            }

                        }
                        else
                        {
                            throw new Exception(AppResources.Nodataavailable);
                        }
                    }

                    return null;
                }

                catch (Exception ex)
                {
                    
                    

                    return null;
                }
            }
            else
            {
                throw new Exception(AppResources.NetworkConnectivityIssue);
            }
        }

        public static List<MyBillsFilterDropdown> GAZTGetMyBillsFilterDropdownValues(String Tin, string lang)
        {
            if (NetworkCheck.IsInternet())
            {

                List<MyBillsFilterDropdown> myBillsFilters = new List<MyBillsFilterDropdown>();

                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    String url = ZATCAConstants.GetMyBillsFilterDropdown + lang;
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTMyBillsFilterResponse = client.GetAsync(uri).Result;
                    if (GAZTMyBillsFilterResponse != null)
                    {
                        if (GAZTMyBillsFilterResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTMyBillsFilterResponse.Headers;
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
                        String GAZTMyBillsResponseJSON = GAZTMyBillsFilterResponse.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(GAZTMyBillsResponseJSON))
                        {
                            GAZTMyBillsResponseJSON = JObject.Parse(GAZTMyBillsResponseJSON)["data"].ToString();
                            if (string.IsNullOrEmpty(GAZTMyBillsResponseJSON) != true)
                            {
                                myBillsFilters = JsonConvert.DeserializeObject<List<MyBillsFilterDropdown>>(GAZTMyBillsResponseJSON);
                            }
                            else
                            {
                                throw new Exception(AppResources.NoBillsAvailable);
                            }
                        }
                        else
                        {
                            throw new Exception(AppResources.NoBillsAvailable);
                        }
                    }
                    return myBillsFilters;
                }
                catch (Exception ex)
                {
                    if (string.Equals(ex.Message, AppResources.NoBillsAvailable))
                    {
                        throw new Exception(AppResources.NoBillsAvailable);
                    }
                    else
                    {
                        throw new Exception(AppResources.NetworkConnectivityIssue);
                    }
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }
        public async static Task<IBanListResponseModel> GetIBanDataForCR1645()
        {
            List<IBANIDNumber> iBANIDNumbers = new List<IBANIDNumber>();
            String IbanNumber = string.Empty;
            IBanListResponseModel IBanListModel = null;
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    string lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    String url = ZATCAConstants.GAZTGetValidIBanNumbers + App.LoginDataRetrieved.TIN + "&language=" + lang;
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTValidateOTPResponse = client.GetAsync(uri).Result;
                    if (GAZTValidateOTPResponse != null)
                    {
                        if (GAZTValidateOTPResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTValidateOTPResponse.Headers;
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
                        String IBANList = GAZTValidateOTPResponse.Content.ReadAsStringAsync().Result;


                        IBanListModel = JsonConvert.DeserializeObject<IBanListResponseModel>(IBANList);
                        return IBanListModel;
                    }
                    return IBanListModel;
                }
                catch (Exception)
                {
                    throw new Exception(AppResources.NetworkConnectivityIssue);
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        public static ICR GAZTGetICRs(String Tin, string lang)
        {
            if (NetworkCheck.IsInternet())
            {
                ICR myICRs = new ICR();
                string NewToken = string.Empty;
                try
                {
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    // var lang = UtilityManager.GetLanguageParameter();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    //  String url = ZATCAConstants.GetMyICRs + lang + "',Gpart='',Euser='',Fbguid='" + App.LoginDataRetrieved.FbGuid + "',UserTin='" + "'" + ")?&saml2=enabled" + "&$expand=ICR_LISTSet,ICR_STATUSSet&sap-language=" + lang + "&$format=json";
                    String url = ZATCAConstants.GetMyICRs + lang + App.LoginDataRetrieved.TIN;

                    // client.DefaultRequestHeaders.Add("Token", "123");
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTMyICRsResponse = client.GetAsync(uri).Result;
                    if (GAZTMyICRsResponse != null)
                    {
                        if (GAZTMyICRsResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTMyICRsResponse.Headers;
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
                        String GAZTMyICRsResponseJSON = GAZTMyICRsResponse.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(GAZTMyICRsResponseJSON))
                        {
                            GAZTMyICRsResponseJSON = JObject.Parse(GAZTMyICRsResponseJSON)["d"].ToString();
                            string GAZTMyICRsLISTSetResponseJSON = JObject.Parse(GAZTMyICRsResponseJSON)["ICR_LISTSet"].ToString();
                            string GAZTMyICRsSTATUSSetResponseJSON = JObject.Parse(GAZTMyICRsResponseJSON)["ICR_STATUSSet"].ToString();
                            GAZTMyICRsLISTSetResponseJSON = JObject.Parse(GAZTMyICRsLISTSetResponseJSON)["results"].ToString();
                            GAZTMyICRsSTATUSSetResponseJSON = JObject.Parse(GAZTMyICRsSTATUSSetResponseJSON)["results"].ToString();
                            if (string.IsNullOrEmpty(GAZTMyICRsSTATUSSetResponseJSON) != true)
                            {
                                myICRs.ICR_STATUSSet = JsonConvert.DeserializeObject<List<ICRStatus>>(GAZTMyICRsSTATUSSetResponseJSON);
                            }
                            if (string.IsNullOrEmpty(GAZTMyICRsLISTSetResponseJSON) != true)
                            {
                                myICRs.ICR_LISTSet = JsonConvert.DeserializeObject<List<ICRListSet>>(GAZTMyICRsLISTSetResponseJSON);
                            }
                            else
                            {
                                throw new Exception(AppResources.ZNoICRAvailable);
                            }
                        }
                        else
                        {
                            throw new Exception(AppResources.ZNoICRAvailable);
                        }
                    }
                    return myICRs;
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
        public async static Task<DashBoardUpdateResponseModel> getTaxPayerActivityUpdateStatusAfterTermsChecked(TPUpdateActivityModel activityUpdateModel)
        {
            DashBoardUpdateResponseModel dashBoardViewResponse = new DashBoardUpdateResponseModel();
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    string lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    String url = ZATCAConstants.GAZTPostActivityStatus;
                    var uri = new Uri(url);
                    var serilized = JsonConvert.SerializeObject(activityUpdateModel);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.PutAsync(uri, contentPost);
                    var detailJson = res.Content.ReadAsStringAsync().Result;
                    dashBoardViewResponse = JsonConvert.DeserializeObject<DashBoardUpdateResponseModel>(detailJson);

                    if (dashBoardViewResponse == null || dashBoardViewResponse.d == null)
                    {
                        ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
                        if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                        {
                            WebServiceManager.ErrorMessageForVAT = errorMesg.error.innererror.errordetails[0].message;
                            WebServiceManager.ErrorMessageForVAT += errorMesg.error.innererror.errordetails[1].message;
                            String WithReplacedString = WebServiceManager.ErrorMessageForVAT.Replace("An exception was raised", string.Empty);
                            WebServiceManager.ErrorMessageForVAT = WithReplacedString;
                            //ErrorMessageForVAT
                            throw new GAZTVATRegistrationInProcessException(WebServiceManager.ErrorMessageForVAT);
                        }
                    }

                    return dashBoardViewResponse;
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

        public async static Task<DashBoardUpdateViewResponseModel> getTaxPayerActivityUpdateStatus()
        {
            if (NetworkCheck.IsInternet())
            {
                DashBoardUpdateViewResponseModel dashBoardUpdateViewResponse = new DashBoardUpdateViewResponseModel();
                string NewToken = string.Empty;
                try
                {
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    HttpClient client = new HttpClient();
                    string lang = WebServiceManager.GetLangZParameterAREN();
                    String url = ZATCAConstants.GAZTGetTpActivityStatus + App.LoginDataRetrieved.TIN;
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    HttpResponseMessage GAZTVATRegistrationDataOtherResponse = await client.GetAsync(url);
                    if (GAZTVATRegistrationDataOtherResponse != null)
                    {
                        if (GAZTVATRegistrationDataOtherResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTVATRegistrationDataOtherResponse.Headers;
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

                        String VatRegistrationOtherData = GAZTVATRegistrationDataOtherResponse.Content.ReadAsStringAsync().Result;
                        dashBoardUpdateViewResponse = JsonConvert.DeserializeObject<DashBoardUpdateViewResponseModel>(VatRegistrationOtherData);

                        if (!string.IsNullOrEmpty(VatRegistrationOtherData) && dashBoardUpdateViewResponse == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(VatRegistrationOtherData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                throw new Exception(errorMessage);
                            }
                        }

                    }
                    return dashBoardUpdateViewResponse;
                }
                catch (Exception e)
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

        public static AllCertificate GAZTGetAllCertificate(String Lang, String Tin)
        {
            if (NetworkCheck.IsInternet())
            {
                DateTime dt = DateTime.Now;
                AllCertificate allCertificate = new AllCertificate();
                string currentDate = dt.ToString("yyyy-MM-ddTHH\\%3AMM\\%3Ass");
                string NewToken = string.Empty;
                try
                {
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", Lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    string uri = ZATCAConstants.GetAllCertificate + Tin + "&language=" + Lang + "&beginDate=" + "1946-08-04T00%3A00%3A00" + "&endDate=" + currentDate;
                    HttpResponseMessage GAZTGetAllCertificateResponse = client.GetAsync(uri).Result;
                    if (GAZTGetAllCertificateResponse != null)
                    {
                        if (GAZTGetAllCertificateResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }

                        HttpHeaders headers = GAZTGetAllCertificateResponse.Headers;
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
                        String GAZTGetAllCertificateResponseJSON = GAZTGetAllCertificateResponse.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(GAZTGetAllCertificateResponseJSON))
                        {
                            GAZTGetAllCertificateResponseJSON = JObject.Parse(GAZTGetAllCertificateResponseJSON)["data"].ToString();
                            allCertificate = JsonConvert.DeserializeObject<AllCertificate>(GAZTGetAllCertificateResponseJSON);
                        }
                    }
                    return allCertificate;
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

        public static async Task<ForgotPasswordOTP> GAZTFogotPasswordSendOTP(ForgotPasswordOTP forgotPasswordOTP)
        {
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                    ForgotPasswordOTP forgotPasswordResponse = new ForgotPasswordOTP();
                    string url = ZATCAConstants.SendUserNameToEmail;
                    var uri = new Uri(url);

                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);

                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("Token", "123");
                    var serilized = JsonConvert.SerializeObject(forgotPasswordOTP);
                    // var serilized = "{\r\n  \"TIN\": \"3311688087\",\r\n  \"email\": \"JABUYASIN-C@ZATCA.GOV.SA\",\r\n  \"birthDate\": \"1436-10-09T22:50:00\",\r\n  \"language\": \"EN\",\r\n  \"captchaCode\": \"6jldgr\",\r\n  \"GUID\": \"005056B1365C1EEDB6898A55D5A2433F\"\r\n}";
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    var detailJson = res.Content.ReadAsStringAsync().Result;
                    forgotPasswordResponse = JsonConvert.DeserializeObject<ForgotPasswordOTP>(detailJson);

                    if (!string.IsNullOrEmpty(detailJson) && forgotPasswordResponse.d == null)
                    {
                        ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
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
                    return forgotPasswordResponse;

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


        public static async Task<GenerateCaptchaGUID> GAZTCaptchaAndGUID(GetCaptcha readCaptcha)
        {

            if (NetworkCheck.IsInternet())
            {
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                    GenerateCaptchaGUID forgotPasswordCaptcha = new GenerateCaptchaGUID();
                    string url = ZATCAConstants.CaptchaAndGUID;
                    var uri = new Uri(url);

                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);

                    var serilized = JsonConvert.SerializeObject(readCaptcha);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    var detailJson = res.Content.ReadAsStringAsync().Result;
                    forgotPasswordCaptcha = JsonConvert.DeserializeObject<GenerateCaptchaGUID>(detailJson);
                    return forgotPasswordCaptcha;
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

        public static async Task<ForgotPasswordOTP> GAZTForgotPasswordValidateOTP(ForgotPasswordOTP ValidateOTP)
        {
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    ForgotPasswordOTP forgotPasswordOTP = new ForgotPasswordOTP();
                    string url = ZATCAConstants.ValidateOTP;
                    var uri = new Uri(url);

                    try
                    {
                        App.httpClientHandler.CookieContainer = null;
                    }
                    catch (Exception)
                    {


                    }

                    HttpClient client = new HttpClient(App.httpClientHandler);

                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    var serilized = JsonConvert.SerializeObject(ValidateOTP);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    var detailJson = res.Content.ReadAsStringAsync().Result;
                    forgotPasswordOTP = JsonConvert.DeserializeObject<ForgotPasswordOTP>(detailJson);
                    return forgotPasswordOTP;
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
        public static async Task<ForgotPasswordOTP> GAZTSendUserNameToEmail(ForgotPasswordOTP1 forgotUserOTP)
        {
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                    ForgotPasswordOTP forgotPasswordOTP = new ForgotPasswordOTP();
                    string url = ZATCAConstants.SendUserNameToEmail;
                    var uri = new Uri(url);
                    var _language = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient();
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", _language);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    var serilized = JsonConvert.SerializeObject(forgotUserOTP);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    var detailJson = res.Content.ReadAsStringAsync().Result;
                    forgotPasswordOTP = JsonConvert.DeserializeObject<ForgotPasswordOTP>(detailJson);

                    if (!string.IsNullOrEmpty(detailJson) && forgotPasswordOTP.d == null)
                    {
                        ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
                        if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                        {
                            string errorMessage = string.Empty;
                            errorMessage = errorMesg.error.innererror.errordetails[0].message;
                            errorMessage += errorMesg.error.innererror.errordetails[1].message;
                            String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                            errorMessage = WithReplacedString;
                            throw new GAZTErrorException(errorMessage);
                        }
                    }
                    return forgotPasswordOTP;


                }
                catch (GAZTErrorException ex)
                {
                    throw new GAZTErrorException(ex.Message);
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
        public static async Task<ForgotPasswordOTP> GAZTChangePassword(ForgotPasswordOTP forgotUserOTP)
        {
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    ForgotPasswordOTP forgotPasswordOTP = new ForgotPasswordOTP();
                    string url = ZATCAConstants.ChangePassword;
                    var uri = new Uri(url);

                    try
                    {
                        App.httpClientHandler.CookieContainer = null;
                    }
                    catch (Exception)
                    {


                    }
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    var serilized = JsonConvert.SerializeObject(forgotUserOTP);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    var detailJson = res.Content.ReadAsStringAsync().Result;
                    forgotPasswordOTP = JsonConvert.DeserializeObject<ForgotPasswordOTP>(detailJson);

                    if (!string.IsNullOrEmpty(detailJson) && forgotPasswordOTP.d == null)
                    {
                        ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
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

                    return forgotPasswordOTP;
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
        public static async Task<TINStatus> GAZTGetTinStatus(string lang, string Tin)
        {
            if (NetworkCheck.IsInternet())
            {
                TINStatus tINStatus = new TINStatus();
                string NewToken = string.Empty;
                try
                {
                    var _language = UtilityManager.GetLanguageParameter();
                    String url = ZATCAConstants.GetTinStatus + _language + "&TIN=" + Tin;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", _language);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
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
                        String TINStatusResponse = response.Content.ReadAsStringAsync().Result;
                        tINStatus = JsonConvert.DeserializeObject<TINStatus>(TINStatusResponse);
                    }
                    return tINStatus;
                }
                catch (Exception ex)
                {
                    if (string.Equals(ex.Message, AppResources.Nodataavailable))
                    {
                        throw new Exception(AppResources.Nodataavailable);
                    }
                    else
                    {
                        throw new Exception(AppResources.NetworkConnectivityIssue);
                    }
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }
        public static async Task<VATLookUp> GAZTGetVATLookUp(string lang, string IdType, String IdNumber)
        {
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    VATLookUp vATLookUp = new VATLookUp();
                    string url = ZATCAConstants.GetVATLookUpDetails;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient();
                    string deviceOs = DependencyService.Get<IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<IDeviceInfoZATCA>().Model;
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    VATLookUpModel VATlookup = new VATLookUpModel();
                    VATlookup.idNumber = IdNumber;
                    VATlookup.idType = IdType;
                    var serilized = JsonConvert.SerializeObject(VATlookup);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage GAZTVATLookUp = await client.PostAsync(url, contentPost);
                    if (GAZTVATLookUp != null)
                    {
                        if (GAZTVATLookUp.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            throw new GAZTSessionExpiredException();
                        }

                        HttpHeaders headers = GAZTVATLookUp.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();

                        }
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == string.Compare(NewToken, "Token has expaired")) || (0 == string.Compare(NewToken, "Invalid Token")))
                            {
                                throw new GAZTSessionExpiredException();
                            }
                            App.Token = NewToken;
                        }
                        string TINStatusResponse = GAZTVATLookUp.Content.ReadAsStringAsync().Result;
                        vATLookUp = JsonConvert.DeserializeObject<VATLookUp>(TINStatusResponse);
                    }
                    return vATLookUp;
                }
                catch (JsonReaderException)
                {
                    throw new GAZTInvalidDataException();
                }
                catch (Exception ex)
                {
                    throw new GAZTNetworkConnectivityIssueException(ex.ToString());
                }
            }

            else
            {
                throw new GAZTInternetException();
            }
        }
        public static async Task<VATDeclaration> GAZTGetVATReturns(string Fbguid, string Fbnumz, string EUser, string PeriodCode)
        {
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                string FbGuid = App.LoginDataRetrieved.FbGuid;
                try
                {
                    VATDeclaration _vATDeclaration = new VATDeclaration();
                    char LangZ = GetLangZParameter();
                    String Lang = UtilityManager.GetLanguageParameter();
                    string deviceOs = DependencyService.Get<IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<IDeviceInfoZATCA>().Model;
                    String url = ZATCAConstants.GAZTGetAllVATDeclarationReturnData + EUser + "&formBundleGUID=" + Fbguid + "&TIN=" + App.TP.TIN + "&language=" + Lang + "&periodKey=" + PeriodCode;
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", Lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTVATReturnStatus = await client.GetAsync(uri);
                    if (GAZTVATReturnStatus != null)
                    {
                        if (GAZTVATReturnStatus.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            throw new GAZTSessionExpiredException();
                        }
                        HttpHeaders headers = GAZTVATReturnStatus.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                        }
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")) || (0 == String.Compare(NewToken, "")))
                            {
                                App.IsSessionExpired = true;
                                throw new GAZTSessionExpiredException();
                            }

                            App.Token = NewToken;
                        }
                        String VATReturn = GAZTVATReturnStatus.Content.ReadAsStringAsync().Result;
                        _vATDeclaration = JsonConvert.DeserializeObject<VATDeclaration>(VATReturn);
                    }
                    return _vATDeclaration;
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

        public static async Task<VATDeclaration> SaveVATDeclarationData(VATDeclaration vATDeclaration)
        {
            VATDeclaration RequestVATDeclaration = new VATDeclaration();
            VATDeclaration _vATDeclarationD = new VATDeclaration();
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    if (vATDeclaration != null && vATDeclaration.data != null)
                    {
                        if (vATDeclaration.data != null)
                        {
                            RequestVATDeclaration = vATDeclaration;
                            if (string.IsNullOrEmpty(vATDeclaration.data.Fbnum))
                            {
                                RequestVATDeclaration.data.SubmitFg = "X";
                            }
                            else
                            {
                                RequestVATDeclaration.data.SubmitFg = string.Empty;
                            }
                            if (vATDeclaration.data.Caltp == "G")
                            {
                                RequestVATDeclaration.data.Caltp = "Gregorian";
                            }
                            else
                            {
                                RequestVATDeclaration.data.Caltp = "Hijri";
                            }
                            //ATTACHSet aTTACHSet = new ATTACHSet();
                            //aTTACHSet.results = new List<Attachment>();
                            RequestVATDeclaration.data.ATTACHSet = new List<ZATCAMAUI.Models.Attachment>();
                        }
                        //char LangZ = GetLangZParameter();
                        //string lang = UtilityManager.GetLanguageParameter();
                        String url = ZATCAConstants.SaveVATDeclarationData;
                        //vATDeclaration.data.Langz = lang;
                        var uri = new Uri(url);
                        //HttpClient client = new HttpClient(App.httpClientHandler);

                        //client.DefaultRequestHeaders.Add("Token", "123");
                        //client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

                        //client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                        //client.DefaultRequestHeaders.Add("Accept", "application/json");
                        HttpClient client = new HttpClient(App.httpClientHandler);
                        string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                        string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                        string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                        var lang = UtilityManager.GetLanguageParameter();
                        client.DefaultRequestHeaders.Add("Accept", "application/json");
                        client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                        client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                        client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                        client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                        client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                        client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                        client.DefaultRequestHeaders.Add("Authorization", App.Token);

                        var serilized = JsonConvert.SerializeObject(RequestVATDeclaration.data);
                        HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                        HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                        var detailJson = res.Content.ReadAsStringAsync().Result;
                        _vATDeclarationD = JsonConvert.DeserializeObject<VATDeclaration>(detailJson);
                        if (_vATDeclarationD != null)
                        {
                            if (_vATDeclarationD.data1 != null)
                            {
                                if (_vATDeclarationD.data1.NOTESSet == null)
                                {
                                    //NOTESSet nOTEs = new NOTESSet();
                                    //nOTEs.results = new List<Note>();
                                    _vATDeclarationD.data1.NOTESSet = new List<Note>();
                                }
                                if (_vATDeclarationD.data1.IBANSet == null)
                                {
                                    //IBANSet iBANSet = new IBANSet();
                                    //iBANSet.results = new List<Result2>();
                                    _vATDeclarationD.data1.IBANSet = new List<Result2>();
                                }
                                if (_vATDeclarationD.data1.CFSet == null)
                                {
                                    //CFSet cFSet = new CFSet();
                                    //cFSet.results = new List<EGAZT.Models.Result3>();
                                    _vATDeclarationD.data1.CFSet = new List<ZATCAMAUI.Models.Result3>();
                                }
                                if (_vATDeclarationD.data1.ATTACHSet == null)
                                {
                                    //ATTACHSet aTTACHSet = new ATTACHSet();
                                    //aTTACHSet.results = new List<Attachment>();
                                    _vATDeclarationD.data1.ATTACHSet = new List<ZATCAMAUI.Models.Attachment>();
                                }
                                if (_vATDeclarationD.data1.ADRSet == null)
                                {
                                    //ADRSet aDRSet = new ADRSet();
                                    //aDRSet.results = new List<Result5>();
                                    _vATDeclarationD.data1.ADRSet = new List<Result5>();
                                }
                                if (_vATDeclarationD.data1.VATR_MSGSet == null)
                                {
                                    //VATRMSGSet vATRMSGSet = new VATRMSGSet();
                                    //vATRMSGSet.results = new List<object>();
                                    _vATDeclarationD.data1.VATR_MSGSet = new List<object>();
                                }
                                if (_vATDeclarationD.data1.VATPERITEMSet == null)
                                {
                                    //VATPERITEMSet vATPERITEMSet = new VATPERITEMSet();
                                    //vATPERITEMSet.results = new List<Result6>();
                                    _vATDeclarationD.data1.VATPERITEMSet = new List<Result6>();
                                }
                            }
                        }
                        if (_vATDeclarationD == null || _vATDeclarationD.data1 == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                ErrorMessageForVAT = errorMesg.error.innererror.errordetails[0].message;
                                ErrorMessageForVAT += " " + errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = ErrorMessageForVAT.Replace("An exception was raised", string.Empty);
                                ErrorMessageForVAT = WithReplacedString;
                            }
                        }
                        return _vATDeclarationD;
                    }
                    return _vATDeclarationD;
                }
                catch (Exception ex)
                {
                    
                    
                    if (_vATDeclarationD != null && _vATDeclarationD.data == null)
                    {
                        return _vATDeclarationD;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }
        #region
        public static async Task<ObservableCollection<InternationalMobileData>> GAZTGetMobileRegionDropdown()
        {
             if (NetworkCheck.IsInternet())
                {
                ObservableCollection<InternationalMobileData> internationalCodes = new ObservableCollection<InternationalMobileData>();
                string NewToken = string.Empty;
                try
                {
                    string lang = UtilityManager.GetLanguageParameter();


                    string url = ZATCAConstants.GAZTInternationalMobileData + lang;

                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", "EN");
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    HttpResponseMessage GAZTInternationalMobileNumDataResponse = await client.GetAsync(url);
                    if (GAZTInternationalMobileNumDataResponse != null)
                    {
                        if (GAZTInternationalMobileNumDataResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTInternationalMobileNumDataResponse.Headers;
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
                        String GAZTInternationalNumberResponseJSON = GAZTInternationalMobileNumDataResponse.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(GAZTInternationalNumberResponseJSON))
                        {
                            GAZTInternationalNumberResponseJSON = JObject.Parse(GAZTInternationalNumberResponseJSON)["data"].ToString();
                            string GAZTInternationalNumberResponseJSONJToken = JObject.Parse(GAZTInternationalNumberResponseJSON)["countries"].ToString();
                            if (string.IsNullOrEmpty(GAZTInternationalNumberResponseJSONJToken) != true)
                            {
                                internationalCodes = JsonConvert.DeserializeObject<ObservableCollection<InternationalMobileData>>(GAZTInternationalNumberResponseJSONJToken);

                            }
                            else
                            {
                                throw new Exception(AppResources.NoBillsAvailable);
                            }
                        }
                        else
                        {
                            throw new Exception(AppResources.NoBillsAvailable);
                        }
                    }
                    return internationalCodes;
                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
                }
                catch (Exception ex)
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
        public static async Task<List<IBANIDNumber>> GAZTGetIBANIdNumber(string IBANType)
        {
            List<IBANIDNumber> iBANIDNumbers = new List<IBANIDNumber>();
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    string lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    String url = ZATCAConstants.GAZTGetIdNumber + App.TP.TIN + "&idType=" + IBANType;
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTValidateOTPResponse = await client.GetAsync(uri);
                    if (GAZTValidateOTPResponse != null)
                    {
                        if (GAZTValidateOTPResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }

                        HttpHeaders headers = GAZTValidateOTPResponse.Headers;
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
                        String IBANIdNumber = GAZTValidateOTPResponse.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(IBANIdNumber))
                        {
                            IBANIdNumber = JObject.Parse(IBANIdNumber)["data"].ToString();
                            IBANIdNumber = JObject.Parse(IBANIdNumber)["VATRegistrationIDNumberDetails"].ToString();
                            iBANIDNumbers = JsonConvert.DeserializeObject<List<IBANIDNumber>>(IBANIdNumber);
                        }
                    }
                    return iBANIDNumbers;
                }
                catch (Exception ex)
                {
                    
                    
                    throw new Exception(AppResources.NetworkConnectivityIssue);
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }
        public async static Task<string> GAZTCheckIBAN(string IBAN)
        {
            List<IBANIDNumber> iBANIDNumbers = new List<IBANIDNumber>();
            String IbanNumber = string.Empty;
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
                    String url = ZATCAConstants.GAZTCheckIBANNumber;
                    var uri = new Uri(url);
                    CheckIbanModel checkIBAN = new CheckIbanModel();
                    checkIBAN.IBAN = IBAN;
                    var serilized = JsonConvert.SerializeObject(checkIBAN);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage GAZTValidateOTPResponse = await client.PostAsync(url, contentPost);
                    if (GAZTValidateOTPResponse != null)
                    {
                        if (GAZTValidateOTPResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTValidateOTPResponse.Headers;
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
                        String IBANIdNumber = GAZTValidateOTPResponse.Content.ReadAsStringAsync().Result;
                        App.IBanValidatedResponse = IBANIdNumber;
                        try
                        {
                            if (!string.IsNullOrEmpty(IBANIdNumber))
                            {
                                IBANIdNumber = JObject.Parse(IBANIdNumber)["result"].ToString();
                                IBANIdNumber = JObject.Parse(IBANIdNumber)["IBAN"].ToString();
                                IbanNumber = IBANIdNumber;
                            }
                        }
                        catch (Exception)
                        {
                            return null;
                        }
                    }
                    return IbanNumber;
                }
                catch (Exception)
                {
                    throw new Exception(AppResources.NetworkConnectivityIssue);
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }
        public static async Task<VATCalculationData> GAZTGetVATDeclaratinCalculationData(string periodKey, string TxnTp, string status, string FormBundleNumber, string Gpart)
        {
            VATCalculationData vATCalculationData = new VATCalculationData();
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
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
                    // String url = ZATCAConstants.GAZTGetVATDeclarationCalculationDataUrl + "'" + FormBundleNumber + "'" + ",Lang='" + lang + "'" + ",Operation='" + "'" + ",Gpart='" + Gpart + "'" + ",Status='" + status + "'" + ",TxnTp='" + TxnTp + "'" + ",Formproc='" + "'" + ",Periodkey='" + periodKey + "'" + ")?saml2=enabled&$expand=IBANSet,IGRTSet,ITUDSet,UI_BTNSet,VATRSet,VTTHSet&$format=json";
                    String url = ZATCAConstants.GAZTGetVATDeclarationCalculationDataUrl + App.TP.TIN + "&formBundleNumber=" + FormBundleNumber + "&language=" + lang + "&status=" + status + "&transactionType=" + TxnTp + "&periodKey=" + periodKey;
                    // HttpResponseMessage GAZTValidateOTPResponse = await GetServiceManager.MakeGetAPICall(url, false, string.Empty);
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTValidateOTPResponse = await client.GetAsync(uri);
                    if (GAZTValidateOTPResponse != null)
                    {
                        if (GAZTValidateOTPResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }

                        HttpHeaders headers = GAZTValidateOTPResponse.Headers;
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
                        String vATCalculationDataSON = GAZTValidateOTPResponse.Content.ReadAsStringAsync().Result;
                        vATCalculationData = JsonConvert.DeserializeObject<VATCalculationData>(vATCalculationDataSON);
                    }
                    return vATCalculationData;
                }
                catch (Exception ex)
                {
                    
                    
                    if (string.Equals(ex.Message, AppResources.InvalidOTP))
                    {
                        throw new Exception(AppResources.InvalidEmail);
                    }
                    else
                    {
                        throw new Exception(AppResources.NetworkConnectivityIssue);
                    }
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }
        public static async Task<AttachmentRootOject> GAZTSaveVATDeclarationAttachmentForFD(Stream AttachmentByte, string fileName, string RetGuid, string Dotyp, string contentType)//, string returnedFguid
        {
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    AttachmentRootOject _attachment = new AttachmentRootOject();
                    AttachmentRequest attachment = new AttachmentRequest();
                    var content = new MultipartFormDataContent();
                    var fileContent = new StreamContent(AttachmentByte);
                    fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = "attachmentFile",
                        FileName = fileName
                    };
                    content.Add(fileContent, "attachmentFile", fileName);
                    if (Dotyp == null)
                    {
                        Dotyp = string.Empty;
                    }
                    String url = ZATCAConstants.GAZTSaveAttachment + "&returnGUID=" + RetGuid + "&attachmentFlag=New" + "&documentCategory=" + Dotyp + "&serialNumber=1" + "&attachedByPerson=TP" + "&fileName=" + fileName + "&documentId=";

                    var uri = new Uri(url);
                    //attachment.documentCategory = Dotyp;
                    //attachment.serialNumber = "1";
                    //attachment.attachedByPerson = AttBy;
                    //attachment.returnGUID = RetGuid;
                    //attachment.attachmentFlag = "New";
                    //attachment.fileName = fileName;

                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string lang = WebServiceManager.GetLangZParameterAREN();

                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    //var serilized = JsonConvert.SerializeObject(attachment);
                    //HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(url, content);

                    var responsestr = res.Content.ReadAsStringAsync().Result;
                    _attachment = JsonConvert.DeserializeObject<AttachmentRootOject>(responsestr);
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
        public static async Task<AttachmentRootOject> GAZTSaveVATDeclarationAttachment(Stream AttachmentByte, string fileName, string RetGuid, string Dotyp, string contentType)//, string returnedFguid
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

                    var LangZ = UtilityManager.GetLanguageParameter();
                    string AttBy = "TP";
                    //String url = ZATCAConstants.GAZTGETVATDeregAttachmentsDropdownList + App.LoginDataRetrieved.TIN + "&language="+lang+ "&status=" + status + "&transactionType=" + selectedType + "&formProcess=ZTAX_VT_REG" ;

                    //https://test-api.zatca.gov.sa/test/third-party/v1/vat-deregistration/attachments?
                    //outletReference=watazdi
                    //&=New
                    //&=8183986020941824
                    //&=4039013256134656
                    //&=tags
                    //&=58
                    //&=4536893815390208
                    //&attachedByPerson=va
                    //&fileName=Milton Rivera

                    String url = ZATCAConstants.GAZTGETVATAttachments + "outletReference=" + RetGuid + "&attachmentFlag=New" + "&returnGUID=" + RetGuid + "&formGUID=" + "&documentCategory=ZIP1" + "&serialNumber=1" + "&documentId=" + "&attachedByPerson=TP" + "&fileName=" + fileName;

                    //String url = ZATCAConstants.GAZTSaveAttachment + "&attachmentFlag=New" + "&returnGUID=" + RetGuid  + "&formGUID=" + "&documentCategory=ZIP1" + "&serialNumber=1" + "&documentId=" + "&attachedByPerson=TP" + "&fileName=" + fileName;
                    var uri = new Uri(url);

                    //ByteArrayContent baContent = new ByteArrayContent(AttachmentByte);
                    //attachment.documentId = "";
                    //attachment.attachment = "new";
                    //attachment.fileName = fileName;
                    //attachment.documentCategory = Dotyp;
                    //attachment.serialNumber = "1";
                    //attachment.attachedByPerson = AttBy;
                    //attachment.returnGUID = RetGuid;
                    //attachment.formGUID = "";
                    //if (!string.IsNullOrEmpty(contentType))
                    //    baContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                    HttpClient client = new HttpClient();
                    string lang = UtilityManager.GetLanguageParameter();

                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);

                    //var serilized = JsonConvert.SerializeObject(attachment);
                    //Console.WriteLine("API for readCaptcha+ ----------------" + serilized);
                    //HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(url, content);

                    var responsestr = res.Content.ReadAsStringAsync().Result;

                    _attachment = JsonConvert.DeserializeObject<AttachmentRootOject>(responsestr);
                    return _attachment;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.Message);
                    Console.Write(Ex.StackTrace.ToString());
                    return null;
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }
        public static string GAZTDeleteVATDeclarationAttachment(string fileName, string RetGuid)//, string returnedFguid
        {
            if (NetworkCheck.IsInternet())
            {
                string DeleteToken = string.Empty;
                try
                {
                    AttachmentRootOject _attachment = new AttachmentRootOject();
                    char LangZ = GetLangZParameter();
                    string Dotyp = "VTA0";
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
                    String url = ZATCAConstants.GAZTDeteleAttachment;
                    var uri = new Uri(url);
                    var lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient();

                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    var serialized = JsonConvert.SerializeObject(_attachmentReq);
                    HttpContent contentPost = new StringContent(serialized, Encoding.UTF8, ZATCAConstants.ContentType);
                    // client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "multipart/form-data");
                    HttpResponseMessage res = client.PostAsync(url, contentPost).Result;
                    var responsestr = res.Content.ReadAsStringAsync().Result;
                    _attachment = JsonConvert.DeserializeObject<AttachmentRootOject>(responsestr);
                    if (res != null)
                    {
                        if (res.StatusCode == HttpStatusCode.NoContent || res.StatusCode == HttpStatusCode.OK)
                            DeleteToken = "X";
                    }
                    return DeleteToken;
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
        public static async Task<SadadNumber> GAZTGetVATDeclarationSADADNumber(string FormBundleID)
        {
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    SadadNumber sadadNumber = new SadadNumber();
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
                    String url = ZATCAConstants.GAZTGetSADADNumber + lang + "&formBundleNumber=" + FormBundleID;
                    //  var response = await GetServiceManager.MakeGetAPICall(url, true, "123");
                    var uri = new Uri(url);
                    HttpResponseMessage response = await client.GetAsync(uri);
                    var responsestr = response.Content.ReadAsStringAsync().Result;
                    sadadNumber = JsonConvert.DeserializeObject<SadadNumber>(responsestr);
                    return sadadNumber;
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
        public static async Task<EstimatedZakatReturns> GAZTGetEstimateZakatReturnList()
        {
            if (NetworkCheck.IsInternet())
            {
                EstimatedZakatReturns zAKATICRList = new EstimatedZakatReturns();
                string NewToken = string.Empty;
                try
                {

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

                    String url = ZATCAConstants.GAZTGetZakatReturnList + App.TP.TIN + "&language=" + lang + "&userTIN" + App.TP.TIN;
                    //HttpResponseMessage GAZTEstimateZakatReturnList = await GetServiceManager.MakeGetAPICall(url, true, "123");
                    HttpResponseMessage GAZTEstimateZakatReturnList = await client.GetAsync(url);
                    if (GAZTEstimateZakatReturnList != null)
                    {
                        if (GAZTEstimateZakatReturnList.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTEstimateZakatReturnList.Headers;
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
                        String EstimateZakatReturnList = GAZTEstimateZakatReturnList.Content.ReadAsStringAsync().Result;
                        zAKATICRList = JsonConvert.DeserializeObject<EstimatedZakatReturns>(EstimateZakatReturnList);
                    }
                    return zAKATICRList;
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
        public static async Task<VATDeclaration> GAZTSetVATReturnVoid(VATDeclaration vATDeclaration)
        {
            if (NetworkCheck.IsInternet())
            {
                VATDeclaration RequestVATDeclaration = null;
                try
                {
                    RequestVATDeclaration = await SaveVATDeclarationData(vATDeclaration);
                }
                catch (Exception)
                {
                }
                return RequestVATDeclaration;
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }
        public static async Task<VATDeclaration> GAZTSetVATReturnReset(VATDeclaration vATDeclaration)
        {
            if (NetworkCheck.IsInternet())
            {
                VATDeclaration RequestVATDeclaration = null;
                try
                {
                    RequestVATDeclaration = await SaveVATDeclarationData(vATDeclaration);
                }
                catch (Exception)
                {


                }
                return RequestVATDeclaration;
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }
        public static async Task<VATDeclaration> GAZTSetVATReturnAmend(VATDeclaration vATDeclaration)
        {
            VATDeclaration RequestVATDeclaration = null;
            try
            {
                RequestVATDeclaration = await SaveVATDeclarationData(vATDeclaration);
            }
            catch (Exception)
            {


            }
            return RequestVATDeclaration;
        }
        public static async Task<ZakatReturnDetails> GAZTGetZAKATReturn(string fbguid)
        {
            ZakatReturnDetails zakatReturnDetails = new ZakatReturnDetails();
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
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




                    //char lang = GetLangZParameter();// "E";
                    String url = "";
                    if (App.IsZakatLoadingFromMyReturns == true)
                    {

                        // string url = ZATCAConstants.GAZTGetZakatReturn + "'" + ",Langz='" + lang + "'" + ",Gpartz='" + App.TP.TIN + "'" + ",Euser='" + App.TP.TIN + "'" + ",Fbguid='" + fbguid + "'" + ",Invflg='" + "'" + ",Fsource='" + "TP" + "'" + ")?saml2=enabled&sap-language='" + lang + "'&$expand=ReasonSet,AttachSet,ThresholdSet,InvoiceSet&$format=json";
                        url = ZATCAConstants.GAZTGetZakatReturn + App.LoginDataRetrieved.TIN + "&language=" + lang + "&versionNumberComponent=TP" + "&formBundleNumber=" + "&formBundleGUID=" + fbguid + "&serialNumber=" + App.TP.TIN;

                    }
                    else
                    {
                        // string  url = ZATCAConstants.GAZTGetZakatReturn + "'" + ",Langz='" + lang + "'" + ",Gpartz='" + App.TP.TIN + "'" + ",Euser='" + "'" + ",Fbguid='" + fbguid + "'" + ",Invflg='" + "'" + ",Fsource='" + "TP" + "'" + ")?saml2=enabled&sap-language='" + lang + "'&$expand=ReasonSet,AttachSet,ThresholdSet,InvoiceSet&$format=json";
                        url = ZATCAConstants.GAZTGetZakatReturn + App.LoginDataRetrieved.TIN + "&language=" + lang + "&versionNumberComponent=TP" + "&formBundleNumberD=" + "&formBundleGUI=" + fbguid + "&serialNumber=";


                    }

                    HttpResponseMessage GAZTValidateOTPResponse = await client.GetAsync(url);
                    // HttpResponseMessage GAZTValidateOTPResponse = await GetServiceManager.MakeGetAPICallWithIncomingChannel(url, true, "123");

                    if (GAZTValidateOTPResponse != null)
                    {
                        if (GAZTValidateOTPResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }

                        HttpHeaders headers = GAZTValidateOTPResponse.Headers;
                        IEnumerable<string> values;
                        try
                        {
                            if (headers.TryGetValues("token", out values))
                            {
                                NewToken = values.First();
                            }
                        }
                        catch (Exception ex)
                        {
                            
                            
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
                        try
                        {
                            String _zakatReturnDetailsJSON = GAZTValidateOTPResponse.Content.ReadAsStringAsync().Result;
                            zakatReturnDetails = JsonConvert.DeserializeObject<ZakatReturnDetails>(_zakatReturnDetailsJSON);
                            if (zakatReturnDetails == null || zakatReturnDetails.d == null)
                            {
                                ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatReturnDetailsJSON);
                                if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                                {
                                    ErrorMessage = errorMesg.error.innererror.errordetails[0].message;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            
                        }

                    }
                    return zakatReturnDetails;
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
        public static async Task<ZakatReturnDetails> GAZTSaveZakatReturnData(ZakatReturnDetails zakatReturnDetailsD, string OperationStatus)//, string returnedFguid
        {
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    zakatReturnDetailsD.d.Operationz = OperationStatus;
                    zakatReturnDetailsD.d.UserTypz = "TP";
                    zakatReturnDetailsD.d.Langz = UtilityManager.GetLanguageParameter();
                    ZakatReturnDetails _zakatReturnDetailsD = new ZakatReturnDetails();
                    string LangZ = GetLangZParameterAREN();
                    String url = ZATCAConstants.GAZTSaveEstimatedZaktReturn + LangZ;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var serilized = JsonConvert.SerializeObject(zakatReturnDetailsD.d);
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;

                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", LangZ);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    var _zakatReturnDetailsDesponsestr = res.Content.ReadAsStringAsync().Result;

                    try
                    {
                        _zakatReturnDetailsD = JsonConvert.DeserializeObject<ZakatReturnDetails>(_zakatReturnDetailsDesponsestr);
                        if (_zakatReturnDetailsD.result != null)
                        {
                            _zakatReturnDetailsD.d = _zakatReturnDetailsD.result;
                        }
                        Console.WriteLine("=====");
                    }
                    catch (Exception ex)
                    {
                        
                    }
                    if (_zakatReturnDetailsD == null || _zakatReturnDetailsD.result == null)
                    {
                        ErrorMessage = string.Empty;
                        ErrorObj errorMesg = null;
                        try
                        {

                            errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatReturnDetailsDesponsestr);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                ErrorMessage = errorMesg.error.innererror.errordetails[0].message;
                            }
                        }
                        catch (Exception ex)
                        {
                            
                        }

                    }
                    return _zakatReturnDetailsD;
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
        public static async Task<List<ApplicableButton>> GAZTVATReturnGetApplicableButtons(string Fbnum, string Lang, string Operation, string Gpart, string Status, string TxnTp, string PeriodKey)
        {
            if (NetworkCheck.IsInternet())
            {
                List<ApplicableButton> VATApplicableButtons = new List<ApplicableButton>();
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

                    //  String url = ZATCAConstants.GAZTVATReturnGetApplicableButtons + "'" + Fbnum + "'" + ",Lang='" + LangZ + "'" + ",Operation='" + Operation + "'," + "Gpart=" + "'" + Gpart + "',Status='" + Status + "',TxnTp='" + TxnTp + "',Formproc='',Periodkey='" + PeriodKey + "'" + ")?saml2=enabled&$expand=UI_BTNSet,IGRTSet&$format=json";
                    String url = ZATCAConstants.GAZTVATReturnGetApplicableButtons + Fbnum + "&langauge=" + lang + "&operation=" + Operation + "&status=" + Status + "&transactionType=" + TxnTp + "&periodKey=" + PeriodKey + "&TIN=" + App.TP.TIN;
                    var uri = new Uri(url);
                    HttpResponseMessage ApplicableButtonsResponse = await client.GetAsync(uri);
                    // HttpResponseMessage ApplicableButtonsResponse = await GetServiceManager.MakeGetAPICall(url, false, string.Empty);
                    if (ApplicableButtonsResponse != null)
                    {
                        if (ApplicableButtonsResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        string NewToken = string.Empty;
                        HttpHeaders headers = ApplicableButtonsResponse.Headers;
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
                        String ApplicableButtonsResponseJSON = ApplicableButtonsResponse.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(ApplicableButtonsResponseJSON))
                        {
                            ApplicableButtonsResponseJSON = JObject.Parse(ApplicableButtonsResponseJSON)["data"].ToString();
                            ApplicableButtonsResponseJSON = JObject.Parse(ApplicableButtonsResponseJSON)["buttons"].ToString();
                            //ApplicableButtonsResponseJSON = JObject.Parse(ApplicableButtonsResponseJSON)["results"].ToString();
                            if (string.IsNullOrEmpty(ApplicableButtonsResponseJSON) != true)
                            {
                                VATApplicableButtons = JsonConvert.DeserializeObject<List<ApplicableButton>>(ApplicableButtonsResponseJSON);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    
                    
                }
                //beforoe returning buttons we need to sest the value based on Buttons emumeration
                foreach (ApplicableButton button in VATApplicableButtons)
                {
                    Enum.TryParse(button.Button, out button.buttonEnumId);
                }
                return VATApplicableButtons;
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }
        public static async Task<AttachmentRootOject> GAZTSaveEstimatedZAKATAttachment(Stream AttachmentByte, string fileName, string RetGuid, string Dotyp, string ContentType)//, string returnedFguid
        {
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    var content = new MultipartFormDataContent();
                    var fileContent = new StreamContent(AttachmentByte);
                    fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = "attachmentFile",
                        FileName = fileName
                    };
                    content.Add(fileContent, "attachmentFile", fileName);

                    AttachmentRootOject _attachment = new AttachmentRootOject();
                    var lang = UtilityManager.GetLanguageParameter();
                    string AttBy = "TP";
                    if (Dotyp == null)
                    {
                        Dotyp = string.Empty;
                    }
                    // String url = ZATCAConstants.GAZTSaveAttachmentGeneric + "'" + "'" + ",RetGuid='" + RetGuid + "'" + ",Flag='" + "N" + "'" + ",Dotyp='" + Dotyp + "'" + ",SchGuid='" + "'" + ",Srno=" + "1" + ",Doguid='" + "'" + ",AttBy='" + AttBy + "'" + ")/AttachMedSet";
                    String url = ZATCAConstants.GAZTSaveEstimatedZAKATAttachement + "&returnGUID=" + RetGuid + "&attachmentFlag=New" + "&documentCategory=" + Dotyp + "&serialNumber=1" + "&attachedByPerson=TP" + "&fileName=" + fileName + "&documentId=";
                    // url = url.Replace("attachmentServiceurl", apiServiceUrl);
                    var uri = new Uri(url);
                    //char LangZ = GetLangZParameter();
                    //string url = ZATCAConstants.GAZTSaveEstimatedZAKATAttachement + RetGuid + "',Flag='N',Dotyp='Z12L',SchGuid='',Srno=1,Doguid='',AttBy='TP',OutletRef='')/AttachMedSet?saml2=enabled";
                    //var uri = new Uri(url);
                    //HttpClient client = new HttpClient(App.httpClientHandler);
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    //client.DefaultRequestHeaders.Add("slug", fileName);
                    //client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", ContentType);
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    //ByteArrayContent baContent = new ByteArrayContent(AttachmentByte);
                    //if (!string.IsNullOrEmpty(ContentType))
                    //    baContent.Headers.ContentType = new MediaTypeHeaderValue(ContentType);
                    var response = await client.PostAsync(url, content);
                    var responsestr = response.Content.ReadAsStringAsync().Result;
                    _attachment = JsonConvert.DeserializeObject<AttachmentRootOject>(responsestr);
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
        public static async Task<EstimatedZAKATReturnsSADADNumber> GAZTGetEstimatedZakatReturnSADADNumber(string FBNumber, string FBGuid)
        {
            if (NetworkCheck.IsInternet())
            {
                EstimatedZAKATReturnsSADADNumber _estimatedZAKATReturnsSADADNumber = new EstimatedZAKATReturnsSADADNumber();
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
                    String url;
                    if (App.IsZakatLoadingFromMyReturns == true)
                    {

                        url = ZATCAConstants.GAZTGetEstimatedZAKATSADADNumber + App.LoginDataRetrieved.TIN + "&formBundleNumber=" + FBNumber + "&language=" + lang + "&serialNumber=" + App.TP.TIN + "&formBundleGUID=" + FBGuid + "&versionNumberComponent=TP";
                    }
                    else
                    {
                        url = ZATCAConstants.GAZTGetEstimatedZAKATSADADNumber + App.LoginDataRetrieved.TIN + "&formBundleNumber=" + FBNumber + "&language=" + lang + "&serialNumber=" + "&formBundleGUID=" + FBGuid + "&versionNumberComponent=TP";
                    }
                    var uri = new Uri(url);
                    // HttpResponseMessage GAZTEstimateZakatReturnList = await GetServiceManager.MakeGetAPICall(url, true, "123");
                    HttpResponseMessage GAZTEstimateZakatReturnList = await client.GetAsync(uri);
                    if (GAZTEstimateZakatReturnList != null)
                    {
                        if (GAZTEstimateZakatReturnList.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }

                        HttpHeaders headers = GAZTEstimateZakatReturnList.Headers;
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
                        String EstimateZakatReturnList = GAZTEstimateZakatReturnList.Content.ReadAsStringAsync().Result;
                        _estimatedZAKATReturnsSADADNumber = JsonConvert.DeserializeObject<EstimatedZAKATReturnsSADADNumber>(EstimateZakatReturnList);
                    }
                    return _estimatedZAKATReturnsSADADNumber;
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
        public static async Task<EstimatedZakatReturns> GAZTDowmloadEstimatedZakatReturnInvoice(string FBNumber, string FBGuid)
        {
            string NewToken = string.Empty;
            try
            {
                string lang = UtilityManager.GetLanguageParameter();
                HttpClient client = new HttpClient(App.httpClientHandler);
                String url = ZATCAConstants.GAZTGetEstimatedZAKATSADADNumber + FBNumber + "'" + ",Langz='" + lang + "'" + ",Gpartz='" + "'" + ",Euser='00000000000000000000'" + ",Fbguid='" + FBGuid + "'" + ",Invflg='I',Fsource='TP')?saml2=enabled&$expand=InvoiceSet";
                var uri = new Uri(url);
                HttpResponseMessage GAZTEstimateZakatReturnList = await GetServiceManager.MakeGetAPICall(url, false, string.Empty);
                if (GAZTEstimateZakatReturnList != null)
                {
                    if (GAZTEstimateZakatReturnList.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        App.IsSessionExpired = true;
                        return null;
                    }
                    HttpHeaders headers = GAZTEstimateZakatReturnList.Headers;
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
                    String EstimateZakatReturnList = GAZTEstimateZakatReturnList.Content.ReadAsStringAsync().Result;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static string GAZTDeleteEstimatedZAKATRAttachment(string fileName, string DocumentID)
        {
            if (NetworkCheck.IsInternet())
            {
                string DeleteToken = string.Empty;
                try
                {
                    AttachmentRootOject _attachment = new AttachmentRootOject();
                    string LangZ = WebServiceManager.GetLangZParameterAREN();
                    string Dotyp = "VTA0";
                    string AttBy = "TP";
                    DeleteAttachmentRequest _attachmentReq = new DeleteAttachmentRequest()
                    {
                        fileName = fileName,
                        returnGUID = "",
                        formGUID = "",
                        documentCategory = Dotyp,
                        documentId = DocumentID,
                        serialNumber = "1",
                        attachedByPerson = AttBy
                    };
                    String url = ZATCAConstants.GAZTDeteleAttachment;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", LangZ);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    var serialized = JsonConvert.SerializeObject(_attachmentReq);
                    HttpContent contentPost = new StringContent(serialized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = client.PostAsync(url, contentPost).Result;
                    var responsestr = res.Content.ReadAsStringAsync().Result;
                    _attachment = JsonConvert.DeserializeObject<AttachmentRootOject>(responsestr);
                    if (res != null)
                    {
                        if (res.StatusCode == HttpStatusCode.NoContent || res.StatusCode == HttpStatusCode.OK)
                            DeleteToken = "X";
                    }
                    return DeleteToken;
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
        public static async Task<string> GAZTEstimatedZAKATReturnInvoicePdf(string Cokey)
        {
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                string DeleteToken = string.Empty;
                try
                {
                    AttachmentRootOject _attachment = new AttachmentRootOject();
                    string lang = UtilityManager.GetLanguageParameter();
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
                    String url = ZATCAConstants.GAZTGetEstimatedZAKATReturnInvoicePdf + Cokey + "&correspondenceType=" + "FZ01";
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTEstimateZakatReturnList = client.GetAsync(uri).Result;
                    if (GAZTEstimateZakatReturnList != null)
                    {
                        if (GAZTEstimateZakatReturnList.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTEstimateZakatReturnList.Headers;
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
                        String EstimateZakatReturnList = GAZTEstimateZakatReturnList.Content.ReadAsStringAsync().Result;
                        string PdfUrl = JsonConvert.DeserializeObject<string>(EstimateZakatReturnList);
                    }
                    return null;
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
        public static CorrespondenceRootObject GAZTGetZakatCorrespondece()
        {
            if (NetworkCheck.IsInternet())
            {
                CorrespondenceRootObject ZakatCorrespondenceList = new CorrespondenceRootObject();
                string NewToken = string.Empty;
                try
                {
                    string lang = UtilityManager.GetLanguageParameter();
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    HttpClient client = new HttpClient();
                    DateTime DateTimeNow = DateTime.Now;

                    var startDate = new DateTime(2007, 1, 1).ToString("yyyy-MM-ddTHH:mm:ss");

                    string CurrentTime = DateTimeNow.ToString("yyyy-MM-ddTHH:mm:ss");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    String url = ZATCAConstants.GAZTGetCorrespondence + App.LoginDataRetrieved.TIN + "&language=" + lang + "&startDate=" + startDate + "&endDate=" + CurrentTime;
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTZakatCorresList = client.GetAsync(uri).Result;
                    if (GAZTZakatCorresList != null)
                    {
                        if (GAZTZakatCorresList.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTZakatCorresList.Headers;
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
                        String VAtCorrespondenceList = GAZTZakatCorresList.Content.ReadAsStringAsync().Result;
                        ZakatCorrespondenceList = JsonConvert.DeserializeObject<CorrespondenceRootObject>(VAtCorrespondenceList);
                    }
                    return ZakatCorrespondenceList;
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
        public static CorrespondenceRootObject GAZTGetVATCorrespondece()
        {
            if (NetworkCheck.IsInternet())
            {
                CorrespondenceRootObject VATCorrespondenceList = new CorrespondenceRootObject();
                string NewToken = string.Empty;
                try
                {
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    string lang = UtilityManager.GetLanguageParameter();
                    //HttpClient client = new HttpClient(App.httpClientHandler);
                    HttpClient client = new HttpClient();
                    DateTime DateTimeNow = DateTime.Now;
                    string CurrentTime = DateTimeNow.ToString("yyyy-MM-ddTHH:mm:ss");
                    //String url = ZATCAConstants.GAZTGetCorrespondence + "'" + App.TP.TIN + "' and Langz eq '" + lang + "' and Begdaz eq datetime'2007-01-01T00:00' and Enddaz eq datetime'" + CurrentTime + "' and ObligFlagz eq 'I' and Auditor eq 'null' and TaxtpFg eq 'VAT' and UserTin eq ''";
                    ////client.DefaultRequestHeaders.Add("Token", App.Token);
                    //var uri = new Uri(url);
                    var startDate = new DateTime(2007, 1, 1).ToString("yyyy-MM-ddTHH:mm:ss");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    String url = ZATCAConstants.GAZTGetCorrespondence + App.TP.TIN + "&language=" + lang + "&startDate=" + startDate + "&endDate=" + CurrentTime + "&taxType=VAT" + "&obligation=I";
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTZakatCorresList = client.GetAsync(uri).Result;
                    if (GAZTZakatCorresList != null)
                    {
                        if (GAZTZakatCorresList.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }

                        HttpHeaders headers = GAZTZakatCorresList.Headers;
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
                        String VAtCorrespondenceList = GAZTZakatCorresList.Content.ReadAsStringAsync().Result;
                        VATCorrespondenceList = JsonConvert.DeserializeObject<CorrespondenceRootObject>(VAtCorrespondenceList);
                    }
                    return VATCorrespondenceList;// tINStatus;
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
        public static CorrespondenceRootObject GAZTGetETCorrespondece()
        {
            if (NetworkCheck.IsInternet())
            {
                CorrespondenceRootObject ETReturnCorrespondenceList = new CorrespondenceRootObject();
                string NewToken = string.Empty;
                try
                {
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    string lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient();
                    //HttpClient client = new HttpClient(App.httpClientHandler);
                    DateTime DateTimeNow = DateTime.Now;
                    string CurrentTime = DateTimeNow.ToString("yyyy-MM-ddTHH:mm:ss");
                    var startDate = new DateTime(2007, 1, 1).ToString("yyyy-MM-ddTHH:mm:ss");
                    //String url = ZATCAConstants.GAZTGetCorrespondence + " '" + App.TP.TIN + "' and Langz eq '" + lang + "' and Begdaz eq datetime'2007-01-01T00:00' and Enddaz eq datetime'" + CurrentTime + "' and ObligFlagz eq 'I' and Auditor eq 'null' and TaxtpFg eq 'ET' and UserTin eq ''";
                    //var uri = new Uri(url);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    String url = ZATCAConstants.GAZTGetCorrespondence + App.LoginDataRetrieved.TIN + "&language=" + lang + "&startDate=" + startDate + "&endDate=" + CurrentTime + "&taxType=ET" + "&obligation=I";
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTETCorresList = client.GetAsync(uri).Result;
                    if (GAZTETCorresList != null)
                    {
                        if (GAZTETCorresList.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTETCorresList.Headers;
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
                        String ETCorrespondenceList = GAZTETCorresList.Content.ReadAsStringAsync().Result;
                        ETReturnCorrespondenceList = JsonConvert.DeserializeObject<CorrespondenceRootObject>(ETCorrespondenceList);
                    }
                    return ETReturnCorrespondenceList;
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
        public static CorrespondenceRootObject GAZTGetCollectionsCorrespondece()
        {
            if (NetworkCheck.IsInternet())
            {
                CorrespondenceRootObject ETReturnCorrespondenceList = new CorrespondenceRootObject();
                string NewToken = string.Empty;
                try
                {
                    string lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    DateTime DateTimeNow = DateTime.Now;
                    string CurrentTime = DateTimeNow.ToString("yyyy-MM-ddTHH:mm");
                    String url = ZATCAConstants.GAZTGetCorrespondence + " '" + App.TP.TIN + "' and Langz eq '" + lang + "' and Begdaz eq datetime'2007-01-01T00:00' and Enddaz eq datetime'" + CurrentTime + "' and ObligFlagz eq 'L' and Auditor eq 'null' and TaxtpFg eq 'COLL' and UserTin eq ''";
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTETCorresList = client.GetAsync(uri).Result;
                    if (GAZTETCorresList != null)
                    {
                        if (GAZTETCorresList.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTETCorresList.Headers;
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
                        String ETCorrespondenceList = GAZTETCorresList.Content.ReadAsStringAsync().Result;
                        ETReturnCorrespondenceList = JsonConvert.DeserializeObject<CorrespondenceRootObject>(ETCorrespondenceList);
                    }
                    return ETReturnCorrespondenceList;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        public static CorrespondenceDetailsRootObject GAZTGetCorrespondeceDetails(CorrespondanceModel CorresModel)
        {
            if (NetworkCheck.IsInternet())
            {
                CorrespondenceDetailsRootObject CorrespondenceDetailsList = new CorrespondenceDetailsRootObject();
                string NewToken = string.Empty;
                try
                {
                    //string lang = UtilityManager.GetLanguageParameter();
                    //HttpClient client = new HttpClient(App.httpClientHandler);
                    //DateTime DateTimeNow = CorresModel.Txtco;
                    //string CurrentTime = DateTimeNow.Year + "/" + DateTimeNow.Day + "/" + DateTimeNow.Month + " - " + DateTimeNow.Hour.ToString("D2") + ":" + DateTimeNow.Minute.ToString("D2") + ":" + DateTimeNow.Second.ToString("D2");
                    //String url = ZATCAConstants.GAZTGetCorrespondenceDetails + "'" + App.TP.TIN + "' and Cotyp eq '" + CorresModel.Cotype + "' and Fbnum eq '' and Cokey eq '" + CorresModel.Cokey + "' and Ltrno eq '" + CorresModel.RefNumber + "' and Txtdo eq '" + CurrentTime + "'and Langu eq '" + lang + "'";

                    //var uri = new Uri(url);
                    //HttpResponseMessage GAZTCorresDList = client.GetAsync(uri).Result;

                    string lang = UtilityManager.GetLanguageParameter();
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    HttpClient client = new HttpClient();
                    DateTime DateTimeNow = CorresModel.Txtco;
                    string CurrentTime = DateTimeNow.Year + "/" + DateTimeNow.Day + "/" + DateTimeNow.Month + " - " + DateTimeNow.Hour.ToString("D2") + ":" + DateTimeNow.Minute.ToString("D2") + ":" + DateTimeNow.Second.ToString("D2");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    String url = ZATCAConstants.GAZTGetCorrespondenceDetails + App.LoginDataRetrieved.TIN
                        + "&correspondenceType=" + CorresModel.Cotype
                        + "&correspondenceKey=" + CorresModel.Cokey
                        + "&letterNumber=" + CorresModel.RefNumber
                        + "&generalDescription=" + CurrentTime
                        + "&language=" + lang;
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTCorresDList = client.GetAsync(uri).Result;
                    if (GAZTCorresDList != null)
                    {
                        if (GAZTCorresDList.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTCorresDList.Headers;
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
                        String ETCorrespondenceList = GAZTCorresDList.Content.ReadAsStringAsync().Result;
                        CorrespondenceDetailsList = JsonConvert.DeserializeObject<CorrespondenceDetailsRootObject>(ETCorrespondenceList);
                    }
                    return CorrespondenceDetailsList;
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
        public static string GAZTSetFavCorrespondence(CorrespondenceFavoriteModel FavoriteCorrespondence)
        {
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    string url = ZATCAConstants.GAZTSetFavCorrespondence;
                    var uri = new Uri(url);

                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string lang = WebServiceManager.GetLangZParameterAREN();
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;

                    // var uri = new Uri(url);

                    //  HttpClient client = new HttpClient(crmSignUphttpClientHandler);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", "EN");
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    var serilized = JsonConvert.SerializeObject(FavoriteCorrespondence);



                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = client.PutAsync(uri, contentPost).Result;
                    var detailJson = res.Content.ReadAsStringAsync().Result;
                    return null;
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
        public static async Task<FormBundleModel> GAZTGetFormBundleModel()
        {
            if (NetworkCheck.IsInternet())
            {
                FormBundleModel ReturnFormBundleList = new FormBundleModel();
                string NewToken = string.Empty;
                try
                {
                    var lang = UtilityManager.GetLanguageParameter();
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    String url = ZATCAConstants.GAZTGetFormBundleModel + App.LoginDataRetrieved.TIN + "&language=" + lang;

                    var uri = new Uri(url);
                    HttpResponseMessage GAZTFormBundleList = await client.GetAsync(uri);
                    if (GAZTFormBundleList != null)
                    {
                        if (GAZTFormBundleList.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTFormBundleList.Headers;
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
                        String FormBundleList = await GAZTFormBundleList.Content.ReadAsStringAsync();
                        ReturnFormBundleList = JsonConvert.DeserializeObject<FormBundleModel>(FormBundleList);
                    }
                    return ReturnFormBundleList;
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
        public static async Task<FormBundleApplicationNumberModel> GAZTGetFormBundleApplicationNumberModel(string Fbtyp)
        {
            if (NetworkCheck.IsInternet())
            {
                FormBundleApplicationNumberModel ReturnFormBundleList = new FormBundleApplicationNumberModel();
                string NewToken = string.Empty; string ApplicationNumber = Fbtyp;
                try
                {
                    //char lang = GetLangZParameter();
                    //String url = ZATCAConstants.GAZTGetFormBunleAccountNumberModel + "'" + lang + "' and Gpart eq '" + App.TP.TIN + "' and Fbtyp eq '" + ApplicationNumber + "'";
                    //HttpResponseMessage GAZTFormBundleList = await GetServiceManager.MakeGetAPICall(url, false, string.Empty);
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
                    String url = ZATCAConstants.GAZTGetFormBunleAccountNumberModel + App.TP.TIN + "&language=" + lang + "&formBundleType=" + Fbtyp;
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTFormBundleList = client.GetAsync(uri).Result;
                    if (GAZTFormBundleList != null)
                    {
                        if (GAZTFormBundleList.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }

                        HttpHeaders headers = GAZTFormBundleList.Headers;
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
                        String FormBundleList = await GAZTFormBundleList.Content.ReadAsStringAsync();
                        ReturnFormBundleList = JsonConvert.DeserializeObject<FormBundleApplicationNumberModel>(FormBundleList);
                    }
                    return ReturnFormBundleList;
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
                catch (Exception ex)
                {
                    
                    
                    throw new GAZTNetworkConnectivityIssueException();
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }
        public static async Task<SignupCityRootObject> GAZTGetCityListForSignup()
        {
            if (NetworkCheck.IsInternet())
            {
                SignupCityRootObject SignupCityList = new SignupCityRootObject();
                string NewToken = string.Empty;
                try
                {
                    string lang = WebServiceManager.GetLangZParameterAREN();
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    // client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    var url = ZATCAConstants.GAZTGetCityListForSignUp + "language=" + lang;
                    HttpResponseMessage GAZTSignupCityList = await client.GetAsync(url);
                    if (GAZTSignupCityList != null)
                    {
                        HttpHeaders headers = GAZTSignupCityList.Headers;
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
                        String SignUpCityList = await GAZTSignupCityList.Content.ReadAsStringAsync();
                        SignupCityList = JsonConvert.DeserializeObject<SignupCityRootObject>(SignUpCityList);
                    }
                    return SignupCityList;
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
                catch (Exception ex)
                {
                    
                    
                    throw new GAZTNetworkConnectivityIssueException();
                }

            }
            else
            {
                throw new GAZTInternetException();
            }
        }
        public static async Task<List<IssuedByResponse>> GAZTGetIssuedByList()
        {
            if (NetworkCheck.IsInternet())
            {
                IssuedByRootObject SignupIssuedByListRoot = new IssuedByRootObject();
                List<IssuedByResponse> SignupIssuedByList = new List<IssuedByResponse>();
                string NewToken = string.Empty;
                try
                {
                    char lang = GetLangZParameter();

                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);
                    String url = ZATCAConstants.GAZTSiguupIssuedByList + "'[{\"Lang\":\"" + lang + "\",\"Portal_usr\":\"1\",\"Process\":\"Trans\",\"Procs_Type\":\"PUSR1\"}]'&sap-language=EN&saml2=enabled&$format=json";
                    client.DefaultRequestHeaders.Add("Token", "123");
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTSignupIssuedByList = await client.GetAsync(uri);
                    if (GAZTSignupIssuedByList != null)
                    {
                        if (GAZTSignupIssuedByList.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            throw new GAZTSessionExpiredException();
                        }
                        HttpHeaders headers = GAZTSignupIssuedByList.Headers;
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
                        String IssuedByList = await GAZTSignupIssuedByList.Content.ReadAsStringAsync();
                        SignupIssuedByListRoot = JsonConvert.DeserializeObject<IssuedByRootObject>(IssuedByList);
                        SignupIssuedByList = JsonConvert.DeserializeObject<List<IssuedByResponse>>(SignupIssuedByListRoot.d.results[0].Response);
                        var sortedIssuedByList = SignupIssuedByList.OrderBy(a => a.txt50).ToList<IssuedByResponse>();

                        try
                        {
                            IEnumerable<IssuedByResponse> otherValueList = from otherVal in sortedIssuedByList
                                                                           where otherVal.elementCode == "90718"
                                                                           select otherVal;

                            IssuedByResponse otherObj = otherValueList.FirstOrDefault();
                            sortedIssuedByList.Remove(otherObj);
                            sortedIssuedByList.Add(otherObj);
                        }
                        catch (Exception)
                        {
                        }

                        SignupIssuedByList = new List<IssuedByResponse>(sortedIssuedByList);
                    }
                    return SignupIssuedByList;
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
                throw new GAZTInternetException();
            }
        }
        public static async Task<AbsherOTPResponse> getValidateAbsher(OTPModelD readCaptcha, bool Absher)
        {

            if (NetworkCheck.IsInternet())
            {
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                    AbsherOTPRequest forgotPasswordCaptcha = new AbsherOTPRequest();
                    forgotPasswordCaptcha.idNumber = readCaptcha.d.Idnum;
                    forgotPasswordCaptcha.idType = readCaptcha.d.Idtype;
                    forgotPasswordCaptcha.captcha = readCaptcha.d.Captcha;
                    forgotPasswordCaptcha.formBundleGUID = readCaptcha.d.Guid16;
                    forgotPasswordCaptcha.taxpayerBirthDate = readCaptcha.d.TaxpDob;
                    string url = "";
                    if (Absher == true)
                    {
                        url = ZATCAConstants.GetAbsherPassword;
                    }
                    else
                    {
                        url = ZATCAConstants.ValidateAbsher;
                    }
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", "EN");
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    //client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    var serilized = JsonConvert.SerializeObject(forgotPasswordCaptcha);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage SignupIsIDTypeValidList = await client.PostAsync(url, contentPost);
                    var detailJson = SignupIsIDTypeValidList.Content.ReadAsStringAsync().Result;
                    //var detailJson = res.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<AbsherOTPResponse>(detailJson);
                    return response;
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
        public static async Task<ValidateAbhserOTPModel> ValidateAbsher(OTPModelvalidateD readCaptcha, bool Absher)
        {

            if (NetworkCheck.IsInternet())
            {
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                    AbsherValidateRequestOTP forgotPasswordCaptcha = new AbsherValidateRequestOTP();
                    forgotPasswordCaptcha.idNumber = readCaptcha.d.Idnum;
                    forgotPasswordCaptcha.captcha = readCaptcha.d.Captcha;
                    forgotPasswordCaptcha.formBundleGUID = readCaptcha.d.Guid16;
                    forgotPasswordCaptcha.OTPCode = readCaptcha.d.OtpCode;
                    string url = "";
                    if (Absher == true)
                    {
                        url = ZATCAConstants.GetAbsherPassword;
                    }
                    else
                    {
                        url = ZATCAConstants.ValidateAbsher;
                    }
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", "EN");
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    var serilized = JsonConvert.SerializeObject(forgotPasswordCaptcha);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage SignupIsIDTypeValidList = await client.PostAsync(url, contentPost);
                    var detailJson = SignupIsIDTypeValidList.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<ValidateAbhserOTPModel>(detailJson);
                    return response;
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
        public async static Task<string> GAZTValidateIDTypesZAKATDelecration(string IDType, string IDNumber, string DBO, OTPModelvalidatedD otpModelD)
        {
            if (NetworkCheck.IsInternet())
            {
                IDTypeValidateRootObject SignupIsIDTypeValid = new IDTypeValidateRootObject();
                string IsIDTypeValidList = string.Empty;
                string NewToken = string.Empty;
                try
                {
                    string lang = GetLangZParameterAREN();

                    TaxPayerInformation TpInfo = new TaxPayerInformation();
                    TpInfo.idNumber = IDNumber;
                    TpInfo.idType = IDType;
                    TpInfo.taxpayerBirthDate = DBO;
                    TpInfo.formBundleGUID = otpModelD.d.Guid16;
                    TpInfo.OTPCode = otpModelD.d.OtpCode;
                    String url = ZATCAConstants.GAZTSiguupValidateIDTypesDeclZakat;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    var serilized = JsonConvert.SerializeObject(TpInfo);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage SignupIsIDTypeValidList = await client.PostAsync(url, contentPost);
                    if (SignupIsIDTypeValidList != null)
                    {
                        if (SignupIsIDTypeValidList.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = SignupIsIDTypeValidList.Headers;
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
                        IsIDTypeValidList = await SignupIsIDTypeValidList.Content.ReadAsStringAsync();
                    }
                    return IsIDTypeValidList;
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
                catch (Exception ex)
                {
                    
                    
                    throw new GAZTNetworkConnectivityIssueException();
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        public async static Task<string> GAZTValidateIDTypes(string IDType, string IDNumber, string DBO)
        {
            if (NetworkCheck.IsInternet())
            {
                IDTypeValidateRootObject SignupIsIDTypeValid = new IDTypeValidateRootObject();
                string IsIDTypeValidList = string.Empty;
                string NewToken = string.Empty;
                try
                {
                    string lang = WebServiceManager.GetLangZParameterAREN();
                    TaxpayerInfo TpInfo = new TaxpayerInfo();
                    TpInfo.idNumber = IDNumber;
                    TpInfo.idType = IDType;
                    TpInfo.taxpayerBirthDate = DBO;
                    HttpClient client = new HttpClient();
                    String url = ZATCAConstants.GAZTSiguupValidateIDTypes;
                    var uri = new Uri(url);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    var serilized = JsonConvert.SerializeObject(TpInfo);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage SignupIsIDTypeValidList = await client.PostAsync(url, contentPost);
                    if (SignupIsIDTypeValidList != null)
                    {
                        if (SignupIsIDTypeValidList.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = SignupIsIDTypeValidList.Headers;
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
                        IsIDTypeValidList = await SignupIsIDTypeValidList.Content.ReadAsStringAsync();
                    }
                    return IsIDTypeValidList;
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
                catch (Exception ex)
                {
                    
                    
                    throw new GAZTNetworkConnectivityIssueException();
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }
        public static CRValidationModelRootObject GAZTValidateCRNumber(string CRNumber)
        {
            if (NetworkCheck.IsInternet())
            {
                CRValidationModelRootObject CRValidationModelValid = new CRValidationModelRootObject();
                string IsIDTypeValidList = string.Empty;
                string NewToken = string.Empty;
                try
                {
                    var lang = UtilityManager.GetLanguageParameter();
                    String url = ZATCAConstants.GAZTSiguupValidateCR + CRNumber;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    HttpResponseMessage CRValidationModelList = client.GetAsync(uri).Result;
                    if (CRValidationModelList != null)
                    {
                        if (CRValidationModelList.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = CRValidationModelList.Headers;
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
                        IsIDTypeValidList = CRValidationModelList.Content.ReadAsStringAsync().Result;
                        CRValidationModelValid = JsonConvert.DeserializeObject<CRValidationModelRootObject>(IsIDTypeValidList);
                    }
                    return CRValidationModelValid;
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
        public static async Task<DuplicateSignUpModelRootObject> GAZTValidateDuplicate(string IDNum, string IDType, string Institude, string Country, string crNum)
        {
            if (NetworkCheck.IsInternet())
            {
                DuplicateSignUpModelRootObject ValidateDuplicate = new DuplicateSignUpModelRootObject();
                string IsIDTypeValidList = string.Empty;
                String url = string.Empty;
                string NewToken = string.Empty;
                try
                {
                    var lang = UtilityManager.GetLanguageParameter();
                    DuplicateSignUpReqModel duplicateModel = new DuplicateSignUpReqModel();
                    duplicateModel.TIN = "";
                    duplicateModel.idType = IDType;
                    duplicateModel.idNumber = IDNum;
                    if (!string.IsNullOrEmpty(crNum))
                    {
                        duplicateModel.institution = "90702";
                    }
                    else
                    {
                        duplicateModel.institution = Institude;
                    }
                    duplicateModel.country = Country;
                    duplicateModel.city = "";
                    duplicateModel.startDate = "";
                    url = ZATCAConstants.GAZTSiguupCheckDuplicate;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    var serilized = JsonConvert.SerializeObject(duplicateModel);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage ValidateDuplicateList = await client.PostAsync(url, contentPost);
                    //HttpResponseMessage ValidateDuplicateList = client.GetAsync(uri).Result;
                    if (ValidateDuplicateList != null)
                    {
                        if (ValidateDuplicateList.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = ValidateDuplicateList.Headers;
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
                        String ValidateDuplicateListResult = ValidateDuplicateList.Content.ReadAsStringAsync().Result;
                        ValidateDuplicate = JsonConvert.DeserializeObject<DuplicateSignUpModelRootObject>(ValidateDuplicateListResult);
                    }
                    return ValidateDuplicate;
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
        public async static Task<string> GAZTSignUpFirstSubmitCGZTAcc(SignUpNextBodyModel SignUpModel)
        {
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    string LangZ = GetLangZParameterAREN();
                    string FirstSignupSubmit = string.Empty;
                    string url = ZATCAConstants.GAZTSignUpFirstSubmit;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", LangZ);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    var serilized = JsonConvert.SerializeObject(SignUpModel);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    FirstSignupSubmit = await res.Content.ReadAsStringAsync();
                    return FirstSignupSubmit;
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
        public static string GAZTSignUpFirstSubmit(SignUpNextBodyModel SignUpModel)
        {
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    string LangZ = GetLangZParameterAREN();
                    string FirstSignupSubmit = string.Empty;
                    string url = ZATCAConstants.GAZTSignUpFirstSubmit;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", LangZ);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    var serilized = JsonConvert.SerializeObject(SignUpModel);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = client.PostAsync(uri, contentPost).Result;
                    FirstSignupSubmit = res.Content.ReadAsStringAsync().Result;
                    return FirstSignupSubmit;
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
        public async static Task<string> GAZTCreateAccountSubmit(CreateGaztAccountModel SignUpModel)
        {
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    string LangZ = GetLangZParameterAREN();
                    string FirstSignupSubmit = string.Empty;
                    string url = ZATCAConstants.GAZTSignUpFirstSubmit;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient();
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", LangZ);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    var serilized = JsonConvert.SerializeObject(SignUpModel);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    if (res != null && res.Content != null)
                    {
                        FirstSignupSubmit = res.Content.ReadAsStringAsync().Result;
                    }
                    return FirstSignupSubmit;
                }
                catch (JsonReaderException)
                {
                    throw new GAZTInvalidDataException();
                }
                catch (HttpRequestException)
                {
                    return null;
                }
                catch (GAZTException)
                {
                    return null;
                }
                catch (Exception ex)
                {
                    
                    
                    throw new GAZTNetworkConnectivityIssueException();
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }
        public static CaseGuidModelRootObject GAZTGetSignupGuid()
        {
            if (NetworkCheck.IsInternet())
            {
                CaseGuidModelRootObject GaztGuidModel = new CaseGuidModelRootObject();
                string IsIDTypeValidList = string.Empty;
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
                    String url = ZATCAConstants.GAZTSignUpGetGuid;
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTGuidList = client.GetAsync(uri).Result;
                    if (GAZTGuidList != null)
                    {
                        if (GAZTGuidList.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTGuidList.Headers;
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
                        String VGAZTGuidListResult = GAZTGuidList.Content.ReadAsStringAsync().Result;
                        GaztGuidModel = JsonConvert.DeserializeObject<CaseGuidModelRootObject>(VGAZTGuidListResult);
                    }
                    return GaztGuidModel;
                }

                catch (JsonReaderException)
                {
                    throw new GAZTInvalidDataException();
                }
                catch (HttpRequestException)
                {
                    return null;
                }
                catch (GAZTSessionExpiredException )
                {
                    return null;
                }
                catch (GAZTException )
                {
                    return null;
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
        public static async Task<TERFRegionRootObject> GAZTTESFormGetRegion()
        {
            RegionPost Cred = new RegionPost();
            Cred.WSUserName = "GAZT@CRM";
            Cred.WSPassword = "gazt@123";
            TERFRegionRootObject Listobject = new TERFRegionRootObject();
            if (NetworkCheck.IsInternet())
            {
                TERFRegionRootObject terfregion = new TERFRegionRootObject();
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };


                    string url = "http://tstcrmmwintg1.mygazt.gov.sa:82/IntegrationServices.svc/RegionRetrieve";
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);
                    var serilized = JsonConvert.SerializeObject(Cred);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    var response = res.Content.ReadAsStringAsync().Result;
                    terfregion = JsonConvert.DeserializeObject<TERFRegionRootObject>(response);
                    return terfregion;
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
        public static async Task<TERFCityRetrieveRootObject> GAZTTESFormGetCity(string regioncode)
        {
            CityPost Cred = new CityPost();
            Cred.WSUserName = "GAZT@CRM";
            Cred.WSPassword = "gazt@123";
            Cred.RegionCode = regioncode;
            TERFCityRetrieveRootObject Listobject = new TERFCityRetrieveRootObject();
            if (NetworkCheck.IsInternet())
            {
                TERFCityRetrieveRootObject terfcity = new TERFCityRetrieveRootObject();
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                    string url = "http://tstcrmmwintg1.mygazt.gov.sa:82/IntegrationServices.svc/CityRetrieve";
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);
                    var serilized = JsonConvert.SerializeObject(Cred);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    var response = res.Content.ReadAsStringAsync().Result;
                    terfcity = JsonConvert.DeserializeObject<TERFCityRetrieveRootObject>(response);
                    return terfcity;
                }
                catch (Exception)
                {


                    throw new InternetException(AppResources.ZZInternetConnectionMessage);
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }
        public static async Task<TERFAQs> GAZTTESFAQRetrive()
        {
            FAQPost Cred = new FAQPost();
            Cred.WSUserName = "GAZT@CRM";
            Cred.WSPassword = "gazt@123";
            Cred.Channel = "2";
            TERFAQs Listobject = new TERFAQs();
            if (NetworkCheck.IsInternet())
            {
                TERFAQs terffaq = new TERFAQs();
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                    string url = ZATCAConstants.GAZTGetFAQ;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);
                    var serilized = JsonConvert.SerializeObject(Cred);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    var response = await res.Content.ReadAsStringAsync();
                    terffaq = JsonConvert.DeserializeObject<TERFAQs>(response);
                    return terffaq;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                throw new InternetException(AppResources.NetworkConnectivityIssue);
            }
        }
        public static async Task<TEReportResponsePostRootObject> GAZTTESReportSubmit(TaxEvasionReportTobeUsedToSubmit Cred, List<UploadedDocumentsList> UploadedDocumentsListObj)
        {
            Cred.UploadedDocumentsList = UploadedDocumentsListObj;
            string mobilenew = Cred.ReporterMobileNumber;
            Cred.ReporterMobileNumber = "05" + Cred.ReporterMobileNumber;
            string mobilenew1 = Cred.CompanyMobileNumber;
            Cred.CompanyMobileNumber = "05" + Cred.CompanyMobileNumber;
            if (NetworkCheck.IsInternet())
            {
                TEReportResponsePostRootObject terfcity = new TEReportResponsePostRootObject();
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                    string url = "http://tstcrmmwintg1.mygazt.gov.sa:82/IntegrationServices.svc/TaxEvasionReportCreate";
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);
                    var serilized = JsonConvert.SerializeObject(Cred);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    var response = res.Content.ReadAsStringAsync().Result;
                    terfcity = JsonConvert.DeserializeObject<TEReportResponsePostRootObject>(response);
                    return terfcity;
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
        public static async Task<ReportRetriveByMobNoRootObject> GAZTTESReportByMobNo(string TPmobno)
        {
            ReportRetriveByMobileNumberPost Cred = new ReportRetriveByMobileNumberPost();
            Cred.WSUserName = "GAZT@CRM";
            Cred.WSPassword = "gazt@123";
            string trimedmob = TPmobno;
            Cred.MobileNumber = "05" + TPmobno;
            Cred.Channel = "2";
            ReportRetriveByMobNoRootObject Listobject = new ReportRetriveByMobNoRootObject();
            if (NetworkCheck.IsInternet())
            {
                ReportRetriveByMobNoRootObject terfreport = new ReportRetriveByMobNoRootObject();
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                    string url = "http://tstcrmmwintg1.mygazt.gov.sa:82/IntegrationServices.svc/TaxEvasionReportRetrieveByMobileNumber";
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);
                    var serilized = JsonConvert.SerializeObject(Cred);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    var response = await res.Content.ReadAsStringAsync();
                    terfreport = JsonConvert.DeserializeObject<ReportRetriveByMobNoRootObject>(response);
                    return terfreport;
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
        #region SYNFUSION INTEGRATION
        public static async Task<MyReturnsRootObject> GAZTGetReturnData(string TIN)
        {
            MyReturnsRootObject ReturnsdData = null;
            if (NetworkCheck.IsInternet())
            {
                DateTime currentDate = DateTime.Now;
                string NewToken = string.Empty;
                var lang = UtilityManager.GetLanguageParameter();
                try
                {
                    if (false == NetworkCheck.IsInternet())
                    {
                        throw new GAZTInternetException();
                    }
                    string url = ZATCAConstants.GAZTGetReturnList + TIN + "&language=" + lang;
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

                    var uri = new Uri(url);
                    HttpResponseMessage GAZTGetDashboardResponse = await client.GetAsync(uri);
                    if (GAZTGetDashboardResponse != null)
                    {
                        string GAZTGetDashboardResponseJSON = GAZTGetDashboardResponse.Content.ReadAsStringAsync().Result;

                        if (!string.IsNullOrEmpty(GAZTGetDashboardResponseJSON))
                        {
                            GAZTGetDashboardResponseJSON = JObject.Parse(GAZTGetDashboardResponseJSON)["data"].ToString();
                            ReturnsdData = JsonConvert.DeserializeObject<MyReturnsRootObject>(GAZTGetDashboardResponseJSON);
                        }
                    }
                }
                catch (JsonReaderException)
                {
                    throw new GAZTInvalidDataException();
                }
                catch (HttpRequestException )
                {
                    return null;
                }
                catch (GAZTSessionExpiredException )
                {
                    return null;
                }
                catch (GAZTException )
                {
                    return null;
                }
                catch (Exception ex)
                {
                    
                    
                    throw new GAZTNetworkConnectivityIssueException(ex.Message);
                }
            }
            else
            {
                throw new GAZTInternetException();
            }
            return ReturnsdData;
        }
        public static Dashboard GAZTGetAdditionalDashboardData(string lang, string TIN)
        {
            Dashboard dashboardData = null;
            if (NetworkCheck.IsInternet())
            {
                DateTime currentDate = DateTime.Now;
                string NewToken = string.Empty;
                try
                {
                    if (false == NetworkCheck.IsInternet())
                    {
                        throw new GAZTInternetException();
                    }
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string uri = ZATCAConstants.GAZTGetTheSetOfUnpaidAmounts + "'" + lang + "'" + " and Gpartz eq '" + TIN + "'" + "&sap-language=" + lang + "&saml2=enabled&$format=json";
                    HttpResponseMessage GAZTGetTheSetOfUnpaidAmountsResponse = client.GetAsync(uri).Result;
                    if (GAZTGetTheSetOfUnpaidAmountsResponse != null)
                    {
                        if (GAZTGetTheSetOfUnpaidAmountsResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            throw new GAZTSessionExpiredException();
                        }

                        HttpHeaders headers = GAZTGetTheSetOfUnpaidAmountsResponse.Headers;
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
                        string GAZTGetTheSetOfUnpaidAmountsResponseJSON = GAZTGetTheSetOfUnpaidAmountsResponse.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(GAZTGetTheSetOfUnpaidAmountsResponseJSON))
                        {
                            GAZTGetTheSetOfUnpaidAmountsResponseJSON = JObject.Parse(GAZTGetTheSetOfUnpaidAmountsResponseJSON)["d"].ToString();
                            dashboardData = JsonConvert.DeserializeObject<Dashboard>(GAZTGetTheSetOfUnpaidAmountsResponseJSON);
                        }
                    }
                }
                catch (JsonReaderException ex)
                {
                    throw new GAZTInvalidDataException(ex.Message);
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
                    
                    
                }
            }
            else
            {
                throw new GAZTInternetException();
            }
            return dashboardData;
        }
        public static async Task<List<OverduePaymentAndUnSubmittedReturn>> GAZTGetUnSubmittedReturnSetForDashboardData(string lang, string TIN)
        {
            List<OverduePaymentAndUnSubmittedReturn> overduePayments = null;
            if (NetworkCheck.IsInternet())
            {
                DateTime currentDate = DateTime.Now;
                string NewToken = string.Empty;
                try
                {
                    if (false == NetworkCheck.IsInternet())
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
                    string uri = ZATCAConstants.GAZTGetUnSubmittedReturnSetForDashboard + TIN + "&language=" + lang;
                    HttpResponseMessage GAZTGetUnSubmittedReturnSetResponse = await client.GetAsync(uri);
                    if (GAZTGetUnSubmittedReturnSetResponse != null)
                    {
                        if (GAZTGetUnSubmittedReturnSetResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            throw new GAZTSessionExpiredException();
                        }

                        HttpHeaders headers = GAZTGetUnSubmittedReturnSetResponse.Headers;
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
                        string GAZTGetUnSubmittedReturnSetResponseJSON = await GAZTGetUnSubmittedReturnSetResponse.Content.ReadAsStringAsync();
                        if (!string.IsNullOrEmpty(GAZTGetUnSubmittedReturnSetResponseJSON))
                        {
                            GAZTGetUnSubmittedReturnSetResponseJSON = JObject.Parse(GAZTGetUnSubmittedReturnSetResponseJSON)["data"].ToString();
                            GAZTGetUnSubmittedReturnSetResponseJSON = JObject.Parse(GAZTGetUnSubmittedReturnSetResponseJSON)["taxpayersDetails"].ToString();
                            overduePayments = JsonConvert.DeserializeObject<List<OverduePaymentAndUnSubmittedReturn>>(GAZTGetUnSubmittedReturnSetResponseJSON);
                        }
                    }
                }
                catch (JsonReaderException ex)
                {
                    throw new GAZTInvalidDataException(ex.Message);
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
                    
                    
                }
            }
            else
            {
                throw new GAZTInternetException();
            }
            return overduePayments;
        }
        public static async Task<List<OverduePaymentAndUnSubmittedReturn>> GAZTGetPaymentOverdueSetForDashboardData(string lang, string TIN)
        {
            List<OverduePaymentAndUnSubmittedReturn> paymentOverdueSet = null;
            if (NetworkCheck.IsInternet())
            {
                DateTime currentDate = DateTime.Now;
                string NewToken = string.Empty;
                try
                {
                    if (false == NetworkCheck.IsInternet())
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
                    string uri = ZATCAConstants.GAZTGetPaymentOverdueSetForDashboard + TIN + "&language=" + lang;
                    HttpResponseMessage GAZTGetPaymentOverdueSetResponse = await client.GetAsync(uri);
                    if (GAZTGetPaymentOverdueSetResponse != null)
                    {
                        if (GAZTGetPaymentOverdueSetResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            throw new GAZTSessionExpiredException();
                        }
                        HttpHeaders headers = GAZTGetPaymentOverdueSetResponse.Headers;
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
                        string GAZTGetPaymentOverdueSetResponseJSON = await GAZTGetPaymentOverdueSetResponse.Content.ReadAsStringAsync();
                        if (!string.IsNullOrEmpty(GAZTGetPaymentOverdueSetResponseJSON))
                        {
                            GAZTGetPaymentOverdueSetResponseJSON = JObject.Parse(GAZTGetPaymentOverdueSetResponseJSON)["data"].ToString();
                            paymentOverdueSet = JsonConvert.DeserializeObject<List<OverduePaymentAndUnSubmittedReturn>>(GAZTGetPaymentOverdueSetResponseJSON);

                            if (!string.IsNullOrEmpty(GAZTGetPaymentOverdueSetResponseJSON) && GAZTGetPaymentOverdueSetResponseJSON == null)
                            {
                                ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(GAZTGetPaymentOverdueSetResponseJSON);
                                if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                                {
                                    string errorMessage = string.Empty;
                                    errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                    errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                    String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                    errorMessage = WithReplacedString;
                                    throw new GAZTErrorException(errorMessage);
                                }
                            }


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
                catch (Exception ex)
                {
                    
                    
                }
            }
            else
            {
                throw new GAZTInternetException();
            }
            return paymentOverdueSet;
        }


        public static string CreateSAMLLoginURL(string Euser, string DeviceId, string FcmId, string DeviceTyp, string Language)
        {
            string FullUrl = ZATCAConstants.GAZTSAMLLoginService + "(Euser='" + Euser + "'" + ",DeviceId='" + DeviceId + "'" + ",FcmId='" +
                FcmId + "'" + ",DeviceTyp='" + DeviceTyp + "')?sap-language=" + Language + "&$format=json";
            return FullUrl;
        }

        public static LoginModel SFGAZTGetLoginDataAndroid(string url)
        {
            if (NetworkCheck.IsInternet())
            {
                String GAZTGetTINsResponseResult = String.Empty;
                string NewToken = string.Empty;
                try
                {
                    try
                    {
                        App.httpClientHandler = new HttpClientHandler();
                        App.httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    }
                    catch (Exception)
                    {



                    }

                    CookieContainer cookieContainer = new CookieContainer();

                    try
                    {
                        foreach (CookieModel cookieModel in App.LoginCookiesRetrieved)
                        {
                            Cookie cookie = new Cookie();
                            cookie.Domain = ZATCAConstants.DevPartialDomainForCookies;
                            cookie.Comment = cookieModel.Comment;
                            cookie.Version = cookieModel.Version;
                            cookie.HttpOnly = cookieModel.IsHttpOnly;
                            cookie.Path = cookieModel.Path;
                            cookie.Name = cookieModel.CName;
                            cookie.Value = cookieModel.CValue;
                            cookie.Secure = cookieModel.Secure;
                            cookieContainer.Add(cookie);
                        }

                        App.httpClientHandler.CookieContainer = cookieContainer;
                    }

                    catch (Exception)
                    {



                    }

                    HttpClient client = new HttpClient(App.httpClientHandler);
                    Uri uri = new Uri(url);
                    LoginModel loginModel = new LoginModel();

                    HttpResponseMessage GAZTGetLoginDataResponseJSON = client.GetAsync(uri).Result;

                    if (GAZTGetLoginDataResponseJSON.StatusCode == HttpStatusCode.OK)
                    {
                        if (GAZTGetLoginDataResponseJSON != null)
                        {
                            if (GAZTGetLoginDataResponseJSON.StatusCode == HttpStatusCode.Unauthorized)
                            {
                                throw new GAZTSessionExpiredException();
                            }

                            HttpHeaders headers = GAZTGetLoginDataResponseJSON.Headers;
                            IEnumerable<string> values;

                            if (headers.TryGetValues("token", out values))
                            {
                                NewToken = values.First();
                            }

                            if ((!string.IsNullOrEmpty(NewToken)))
                            {
                                if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")))
                                {
                                    throw new GAZTSessionExpiredException(string.Empty);
                                }
                                App.Token = NewToken;
                            }

                            String GAZTGetTaxPayerProfileResponseJSONString = GAZTGetLoginDataResponseJSON.Content.ReadAsStringAsync().Result;
                            if (!string.IsNullOrEmpty(GAZTGetTaxPayerProfileResponseJSONString))
                            {
                                GAZTGetTaxPayerProfileResponseJSONString = JObject.Parse(GAZTGetTaxPayerProfileResponseJSONString)["d"].ToString();
                                loginModel = JsonConvert.DeserializeObject<LoginModel>(GAZTGetTaxPayerProfileResponseJSONString);
                                NumberOfValiedAttempts = "3";

                                App.LoginDataRetrieved = loginModel;
                                App.Token = App.LoginDataRetrieved.DeviceToken;

                                if (loginModel == null)
                                    throw new GAZTTaxPayerProfileDataException();
                                else
                                    return loginModel;
                            }
                            else
                            {
                                throw new GAZTTaxPayerProfileDataException();
                            }
                        }
                        else
                            throw new GAZTTaxPayerProfileDataException();
                    }
                    else
                    {
                        throw new GAZTNetworkConnectivityIssueException();
                    }
                }
                catch (JsonReaderException)
                {
                    throw new GAZTInvalidDataException();
                }
                catch (HttpRequestException)
                {
                    return null;
                }
                catch (GAZTSessionExpiredException)
                {
                    return null;
                }
                catch (GAZTException)
                {
                    return null;
                }
                catch (Exception)
                {


                    throw new GAZTNetworkConnectivityIssueException();
                }
            }
            else
            {
                throw new GAZTInternetException(string.Empty);
            }
        }


        public static async Task<LoginModel> SFGAZTGetLoginData(string url)
        {
            if (NetworkCheck.IsInternet())
            {
                string GAZTGetTINsResponseResult = string.Empty;
                string NewToken = string.Empty;
                try
                {
                    try
                    {
                        App.httpClientHandler = new HttpClientHandler();
                        App.httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    }
                    catch (Exception)
                    {



                    }

                    CookieContainer cookieContainer = new CookieContainer();

                    try
                    {
                        foreach (CookieModel cookieModel in App.LoginCookiesRetrieved)
                        {
                            Cookie cookie = new Cookie();
                            cookie.Comment = cookieModel.Comment;
                            cookie.Version = cookieModel.Version;
                            cookie.HttpOnly = cookieModel.IsHttpOnly;
                            cookie.Path = cookieModel.Path;
                            cookie.Name = cookieModel.CName;

                            if (cookieModel.Domain.StartsWith(".") == false)
                            {
                                cookie.Domain = "." + cookieModel.Domain;
                            }
                            else
                            {
                                cookie.Domain = cookieModel.Domain;
                            }

                            cookie.Value = cookieModel.CValue;
                            cookie.Secure = cookieModel.Secure;
                            cookieContainer.Add(cookie);

                        }

                        App.httpClientHandler.CookieContainer = cookieContainer;
                    }

                    catch (Exception)
                    {}

                    HttpClient client = new HttpClient(App.httpClientHandler);
                    Uri uri = new Uri(url);
                    LoginModel loginModel = new LoginModel();

                    HttpResponseMessage GAZTGetLoginDataResponseJSON = await client.GetAsync(uri);

                    if (GAZTGetLoginDataResponseJSON.StatusCode == HttpStatusCode.OK)
                    {
                        if (GAZTGetLoginDataResponseJSON != null)
                        {
                            if (GAZTGetLoginDataResponseJSON.StatusCode == HttpStatusCode.Unauthorized)
                            {
                                throw new GAZTSessionExpiredException();
                            }

                            HttpHeaders headers = GAZTGetLoginDataResponseJSON.Headers;
                            IEnumerable<string> values;

                            if (headers.TryGetValues("token", out values))
                            {
                                NewToken = values.First();
                            }

                            if ((!string.IsNullOrEmpty(NewToken)))
                            {
                                if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")))
                                {
                                    throw new GAZTSessionExpiredException(string.Empty);
                                }
                                App.Token = NewToken;
                            }

                            String GAZTGetTaxPayerProfileResponseJSONString = GAZTGetLoginDataResponseJSON.Content.ReadAsStringAsync().Result;
                            if (!string.IsNullOrEmpty(GAZTGetTaxPayerProfileResponseJSONString))
                            {
                                GAZTGetTaxPayerProfileResponseJSONString = JObject.Parse(GAZTGetTaxPayerProfileResponseJSONString)["d"].ToString();
                                loginModel = JsonConvert.DeserializeObject<LoginModel>(GAZTGetTaxPayerProfileResponseJSONString);
                                NumberOfValiedAttempts = "3";

                                App.LoginDataRetrieved = loginModel;
                                App.Token = App.LoginDataRetrieved.DeviceToken;

                                if (loginModel == null)
                                    throw new GAZTTaxPayerProfileDataException();
                                else
                                    return loginModel;
                            }
                            else
                            {
                                throw new GAZTTaxPayerProfileDataException();
                            }
                        }
                        else
                            throw new GAZTTaxPayerProfileDataException();
                    }
                    else
                    {
                        throw new GAZTNetworkConnectivityIssueException();
                    }
                }
                catch (JsonReaderException)
                {
                    throw new GAZTInvalidDataException();
                }
                catch (HttpRequestException )
                {
                    return null;
                }
                catch (GAZTSessionExpiredException )
                {
                    return null;
                }
                catch (GAZTException )
                {
                    return null;
                }
                catch (Exception)
                {


                    throw new GAZTNetworkConnectivityIssueException();
                }
            }
            else
            {
                throw new GAZTInternetException(String.Empty);
            }
        }


        public static List<TINModel> SFGAZTGetAllTINs(string UserName)
        {
            if (NetworkCheck.IsInternet())
            {
                String GAZTGetTINsResponseResult = String.Empty;
                List<TINModel> TINs = null;
                try
                {

                    var lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    string url = ZATCAConstants.GetAllTin + UserName;
                    Uri uri = new Uri(url);
                    HttpResponseMessage GAZTGetTINsResponse = client.GetAsync(uri).Result;
                    if (GAZTGetTINsResponse != null)
                    {
                        GAZTGetTINsResponseResult = GAZTGetTINsResponse.Content.ReadAsStringAsync().Result;
                    }
                    if (!string.IsNullOrEmpty(GAZTGetTINsResponseResult))
                    {
                        GAZTGetTINsResponseResult = JObject.Parse(GAZTGetTINsResponseResult)["data"].ToString();
                        GAZTGetTINsResponseResult = JObject.Parse(GAZTGetTINsResponseResult)["taxpayers"].ToString();
                        TINs = JsonConvert.DeserializeObject<List<TINModel>>(GAZTGetTINsResponseResult);
                        if (TINs == null)
                        {
                            throw new GAZTNoTINsAvailableException(string.Empty);
                        }
                    }
                    return TINs;
                }
                catch (JsonReaderException)
                {
                    throw new GAZTInvalidDataException();
                }
                catch (HttpRequestException )
                {
                    return null;
                }
                catch (GAZTException )
                {
                    return null;
                }
                catch (Exception ex)
                {
                    
                    
                    throw new GAZTNetworkConnectivityIssueException();
                }
            }
            else
            {
                throw new GAZTInternetException(String.Empty);
            }
        }
        public static async Task<EmailTinsModel> GetTinsBasedOnEmail(string email)
        {
            if (NetworkCheck.IsInternet())
            {
                String GAZTGetTINsResponseResult = String.Empty;
                EmailTinsModel TINs = null;
                try
                {
                    var lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-IBM-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-IBM-Client-Secret", ZATCAConstants.ClientSecret);
                    //client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    string url = ZATCAConstants.GetAllTinsByEmail + email;
                    Uri uri = new Uri(url);
                    HttpResponseMessage GAZTGetTINsResponse = await client.GetAsync(uri);
                    if (GAZTGetTINsResponse != null)
                    {
                        GAZTGetTINsResponseResult = GAZTGetTINsResponse.Content.ReadAsStringAsync().Result;
                    }
                    if (!string.IsNullOrEmpty(GAZTGetTINsResponseResult))
                    {
                        GAZTGetTINsResponseResult = JObject.Parse(GAZTGetTINsResponseResult).ToString();
                        //GAZTGetTINsResponseResult = JObject.Parse(GAZTGetTINsResponseResult)["taxpayers"].ToString();
                        TINs = JsonConvert.DeserializeObject<EmailTinsModel>(GAZTGetTINsResponseResult);

                        //if (TINs == null)
                        //{
                        //    throw new GAZTNoTINsAvailableException(string.Empty);
                        //}
                    }
                    return TINs;
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
                catch (Exception ex)
                {
                    
                    
                    throw new GAZTNetworkConnectivityIssueException();
                }
            }
            else
            {
                throw new GAZTInternetException(String.Empty);
            }
        }

        public static async Task GAZTLogOff()
        {
            if (NetworkCheck.IsInternet())
            {
                
                try
                {
                    LogoutModel logoutModel = new LogoutModel();
                    String url = ZATCAConstants.GAZTSAMLLogoutService;
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", "EN");
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    var serilized = JsonConvert.SerializeObject(logoutModel);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(url, contentPost);
                    var detailJson = res.Content.ReadAsStringAsync().Result;
                    var dataresponse = JsonConvert.DeserializeObject<LogoutResponseModel>(detailJson);
                    App.LoginCookiesRetrieved = null;
                    App.IsLoginCalled = true;

                    if (dataresponse.result != null)
                    {
                        //App.IsLoginCalled = true;
                        // GAZTGetLogoffResponseResult = GAZTLogOffResponse.Content.ReadAsStringAsync().Result;
                    }

                    App.CreateClientHandler();
                }
                catch (Exception ex)
                {
                    
                    
                    if (string.Equals(ex.Message, AppResources.NoTINsAvailable))
                    {
                        throw new Exception(AppResources.NoTINsAvailable);
                    }
                    else
                    {
                        throw new Exception(AppResources.NetworkConnectivityIssue);
                    }
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }
        public static TaxPayerProfile SFGAZTGetTaxPayerProfile(string TIN, string Lang)
        {
            TaxPayerProfile profile = null;
            if (NetworkCheck.IsInternet())
            {
                
                try
                {
                    string MobileNumber = string.Empty;
                    string PdfUrl = string.Empty;
                    string NewToken = string.Empty;
                    CookieContainer cookieContainer = new CookieContainer();

                    try
                    {
                        foreach (CookieModel cookieModel in App.LoginCookiesRetrieved)
                        {
                            Cookie cookie = new Cookie();

                            if (DeviceInfo.Platform == DevicePlatform.iOS)
                            {
                                if (cookieModel.Domain.StartsWith(".") == false)
                                {
                                    cookie.Domain = "." + cookieModel.Domain;
                                }
                                else
                                {
                                    cookie.Domain = cookieModel.Domain;
                                }
                            }
                            else if (DeviceInfo.Platform == DevicePlatform.Android)
                            {
                                cookie.Domain = ZATCAConstants.DevPartialDomainForCookies;
                            }

                            cookie.Comment = cookieModel.Comment;
                            cookie.Version = cookieModel.Version;
                            cookie.HttpOnly = cookieModel.IsHttpOnly;
                            cookie.Path = cookieModel.Path;
                            cookie.Name = cookieModel.CName;
                            cookie.Value = cookieModel.CValue;
                            cookie.Secure = cookieModel.Secure;
                            cookieContainer.Add(cookie);
                        }

                        App.httpClientHandler.CookieContainer = cookieContainer;
                    }

                    catch (Exception)
                    {



                    }

                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = ZATCAConstants.GAZTGetTP + "='" + TIN + "',Langz='" + Lang + "')" + "?&$expand=TPOC_LIST&saml2=enabled&$format=json";
                    Uri uri = new Uri(url);
                    HttpResponseMessage GAZTGetTaxPayerProfileResponseJSON = client.GetAsync(uri).Result;
                    if (GAZTGetTaxPayerProfileResponseJSON != null)
                    {
                        if (GAZTGetTaxPayerProfileResponseJSON.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            throw new GAZTSessionExpiredException();
                        }

                        HttpHeaders headers = GAZTGetTaxPayerProfileResponseJSON.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                        }
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")))
                            {
                                throw new GAZTSessionExpiredException(string.Empty);
                            }
                            App.Token = NewToken;
                        }
                        String GAZTGetTaxPayerProfileResponseJSONString = GAZTGetTaxPayerProfileResponseJSON.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(GAZTGetTaxPayerProfileResponseJSONString))
                        {
                            GAZTGetTaxPayerProfileResponseJSONString = JObject.Parse(GAZTGetTaxPayerProfileResponseJSONString)["d"].ToString();
                            profile = JsonConvert.DeserializeObject<TaxPayerProfile>(GAZTGetTaxPayerProfileResponseJSONString);
                            if (profile == null)
                                throw new GAZTTaxPayerProfileDataException();
                        }
                        else
                        {
                            throw new GAZTTaxPayerProfileDataException();
                        }
                    }
                    else
                        throw new GAZTTaxPayerProfileDataException();
                }
                catch (JsonReaderException)
                {
                    throw new GAZTInvalidDataException();
                }
                catch (GAZTSessionExpiredException )
                {
                    return null;
                }
                catch (HttpRequestException )
                {
                    return null;
                }
                catch (GAZTException )
                {
                    return null;
                }
                catch (Exception)
                {
                }
            }
            else
            {
                throw new GAZTInternetException(String.Empty);
            }
            return profile;
        }
        public static async Task<string> GAZTTESVerfymobNoSendOtp(string mobno, string messageforsms)
        {
            string userName = "GaztApp";
            string password = "Gazt@2020";
            string tagName = "Gazt.gov.sa";
            string recepientNumber = "9665" + mobno;
            string message = messageforsms;
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    string url = "http://10.50.11.203/ZPService/SMSAPI.asmx/SendSingleSMS?userName=" + userName + "&password=" + password + "&tagName=" + tagName + "&recepientNumber=" + recepientNumber + "&message=" + message + "&sendDateTime=0";
                    HttpResponseMessage res = await GetServiceManager.MakeGetAPICall(url, false, "");
                    var response = await res.Content.ReadAsStringAsync();
                    XElement xmlroot = XElement.Parse(response);
                    string statuscode = xmlroot.Value;
                    return statuscode;
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
                throw new GAZTInternetException();
            }
        }
        public static async Task<AttachmentDocumentModel> GAZTGetAllAttachments(String retGuid, String fbNum)
        {
            if (NetworkCheck.IsInternet())
            {
                String GAZTAttachmentsResponseResult = String.Empty;
                AttachmentDocumentModel attachmentDocumentModel = null;
                try
                {
                    string url = ZATCAConstants.GAZTGetAllAttachments + retGuid + "&portalUser=" + fbNum;
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", "EN");
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTGetAllAttachmentsResponse = await client.GetAsync(uri);
                    if (GAZTGetAllAttachmentsResponse != null)
                    {
                        GAZTAttachmentsResponseResult = GAZTGetAllAttachmentsResponse.Content.ReadAsStringAsync().Result;
                    }
                    if (!string.IsNullOrEmpty(GAZTAttachmentsResponseResult))
                    {
                        GAZTAttachmentsResponseResult = JObject.Parse(GAZTAttachmentsResponseResult).ToString();
                        attachmentDocumentModel = JsonConvert.DeserializeObject<AttachmentDocumentModel>(GAZTAttachmentsResponseResult);
                        if (attachmentDocumentModel == null)
                        {
                            throw new Exception(AppResources.NoTINsAvailable);
                        }
                    }
                    return attachmentDocumentModel;
                }
                catch (Exception ex)
                {
                    
                    
                    if (string.Equals(ex.Message, AppResources.NoTINsAvailable))
                    {
                        throw new Exception(AppResources.NoTINsAvailable);
                    }
                    else
                    {
                        throw new Exception(AppResources.NetworkConnectivityIssue);
                    }
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }
        #endregion


        public async static Task<ZakatRevokeValidateModel> GAZTGetZakatRevokeValidate(string fbnum)
        {
            ZakatRevokeValidateModel _zakatRevokeValidateModel = new ZakatRevokeValidateModel();

            if (NetworkCheck.IsInternet())
            {

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
                    // String url = ZATCAConstants.ZakatValidateRevokeListUrl + "Fbnum='" + fbnum + "')?&$format=json";
                    String url = ZATCAConstants.ZakatValidateRevokeListUrl + fbnum;
                    var uri = new Uri(url);
                    HttpResponseMessage _zakatRevokeValidateResponse = await client.GetAsync(uri);
                    // HttpResponseMessage _zakatRevokeValidateResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
                    if (_zakatRevokeValidateResponse != null)
                    {
                        if (_zakatRevokeValidateResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _zakatRevokeValidateResponse.Headers;
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
                        String __zakatRevokeValidateResponseData = _zakatRevokeValidateResponse.Content.ReadAsStringAsync().Result;
                        _zakatRevokeValidateModel = JsonConvert.DeserializeObject<ZakatRevokeValidateModel>(__zakatRevokeValidateResponseData);

                        if (!string.IsNullOrEmpty(__zakatRevokeValidateResponseData) && _zakatRevokeValidateModel.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__zakatRevokeValidateResponseData);
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
                    }
                    return _zakatRevokeValidateModel;
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
        public static async Task<ZakatRevokeSendSMSModel> GAZTZakatRevokeSendOTP(string fbNum, string code)
        {
            ZakatRevokeSendSMSModel _zakatRevokeSendSMSModel = new ZakatRevokeSendSMSModel();
            if (NetworkCheck.IsInternet())
            {

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
                    String url = ZATCAConstants.ZakateRevokeSendOTPUrl + App.LoginDataRetrieved.TIN + "&formBundleNumber=" + fbNum + "&OTP=" + code;
                    var uri = new Uri(url);
                    HttpResponseMessage _zakatRevokeSendSMSResponse = await client.GetAsync(uri);
                    if (_zakatRevokeSendSMSResponse != null)
                    {
                        if (_zakatRevokeSendSMSResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _zakatRevokeSendSMSResponse.Headers;
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
                        String _zakatRevokeSendSMSResponseData = _zakatRevokeSendSMSResponse.Content.ReadAsStringAsync().Result;
                        _zakatRevokeSendSMSModel = JsonConvert.DeserializeObject<ZakatRevokeSendSMSModel>(_zakatRevokeSendSMSResponseData);

                        if (!string.IsNullOrEmpty(_zakatRevokeSendSMSResponseData) && _zakatRevokeSendSMSModel.results == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatRevokeSendSMSResponseData);
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
                    }
                    return _zakatRevokeSendSMSModel;
                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
                }
                catch (Exception ex)
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



        #region Download File

        public async static System.Threading.Tasks.Task<bool> FileDownload(string url, string fileExtension)
        {
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    string NewToken = string.Empty;
                    Char lang = WebServiceManager.GetLangZParameter();
                    System.IO.MemoryStream pdfStream = new MemoryStream();
                    var dependency = DependencyService.Get<IPrintService>();


                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var uri = new Uri(url);
                    HttpResponseMessage _fileDownloadResponse = await client.GetAsync(uri);

                    var fileName = Guid.NewGuid().ToString();

                    _fileDownloadResponse.EnsureSuccessStatusCode();
                    await _fileDownloadResponse.Content.CopyToAsync(pdfStream);
                    await dependency.Save(pdfStream, $"{fileName}." + fileExtension);

                    if (_fileDownloadResponse.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        App.IsSessionExpired = true;
                        return false;
                    }
                    HttpHeaders headers = _fileDownloadResponse.Headers;
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
                            return false;
                        }
                        App.Token = NewToken;
                    }
                    return true;
                }
                catch (GAZTVATChangeFillingPeriodException ex)
                {
                    return false;
                    throw new GAZTVATChangeFillingPeriodException(ex.Message);

                }
                catch (Exception)
                {


                    App.IsSessionExpired = true;
                    return false;
                }
            }
            else
            {
                return false;
                throw new InternetException(AppResources.ZZInternetConnectionMessage);

            }
        }

        public static async Task<VATDeclaration> GAZTGetVRVATReturns(string Fbguid, string Fbnumz, string EUser, string PeriodCode)
        {
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                string FbGuid = App.LoginDataRetrieved.FbGuid;
                try
                {
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    VATDeclaration _vATDeclaration = new VATDeclaration();
                    char LangZ = GetLangZParameter();
                    String Lang = UtilityManager.GetLanguageParameter();
                    String url = ZATCAConstants.GAZTGetAllVATDeclarationReturnData + EUser + "&formBundleGUID=" + Fbguid + "&TIN=" + App.TP.TIN + "&language=" + Lang + "&periodKey=" + PeriodCode + "&formBundleNumber=" + Fbnumz;
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", Lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTVATReturnStatus = await client.GetAsync(uri);
                    if (GAZTVATReturnStatus != null)
                    {
                        if (GAZTVATReturnStatus.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            throw new GAZTSessionExpiredException();
                        }
                        HttpHeaders headers = GAZTVATReturnStatus.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                        }
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")) || (0 == String.Compare(NewToken, "")))
                            {
                                App.IsSessionExpired = true;
                                throw new GAZTSessionExpiredException();
                            }
                            App.Token = NewToken;
                        }
                        String VATReturn = GAZTVATReturnStatus.Content.ReadAsStringAsync().Result;
                        _vATDeclaration = JsonConvert.DeserializeObject<VATDeclaration>(VATReturn);
                    }
                    return _vATDeclaration;
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



        public static Task<bool> ChangeTPProfilePasswordAPICall(string oldPassword, string newPassword)
        {
            if (NetworkCheck.IsInternet())
            {
                string lang = string.Empty;
                if (App.IsArabic) { lang = "AR"; }
                else { lang = "EN"; }
                TPProfileUpdatePasswordRequestModel APIRequestDataModel = new TPProfileUpdatePasswordRequestModel();
                APIRequestDataModel.email = App.TP.email;
                APIRequestDataModel.oldPassword = oldPassword;
                APIRequestDataModel.newPassword = newPassword;
                APIRequestDataModel.TIN = App.TP.TIN;
                APIRequestDataModel.confirmPassword = newPassword;


                var response = POSTTPPasswordUpdate(APIRequestDataModel);

                return response;
            }
            else
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
        }
        public static async Task<bool> POSTTPPasswordUpdate(TPProfileUpdatePasswordRequestModel model)
        {
            if (NetworkCheck.IsInternet())
            {
                bool TP = false;
                string GAZTTPProfileResponseJSON = string.Empty;

                try
                {
                    string lang = string.Empty;
                    if (App.IsArabic) { lang = "AR"; }
                    else { lang = "EN"; }

                    string url = ZATCAConstants.TPProfilePasswordUpdate;
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

                    var serilized = JsonConvert.SerializeObject(model);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.PutAsync(uri, contentPost);

                    GAZTTPProfileResponseJSON = res.Content.ReadAsStringAsync().Result;
                    return res.StatusCode == HttpStatusCode.OK;
                }
                catch (Exception ex)
                {
                    
                    

                }

                return false;
            }
            else
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
        }

        public static async Task<TaxPayerProfile> GetTPProfileAndUpdatePasswordAPICall(string TIN)
        {
            TaxPayerProfile TPProfileData = null;
            string NewToken = string.Empty;
            string GAZTTPProfileResponseJSON = string.Empty;
            string lang = UtilityManager.GetLanguageParameter();
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                client.DefaultRequestHeaders.Add("Authorization", App.Token);
                var url = ZATCAConstants.GAZTGetTP + TIN + "&language=" + UtilityManager.GetLanguageParameter();
                HttpResponseMessage UpdatePWDResponse = await client.GetAsync(url);
                if (UpdatePWDResponse != null)
                {
                    if (UpdatePWDResponse.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        App.IsSessionExpired = true;
                        return null;
                    }

                    if (UpdatePWDResponse.Headers != null)
                    {
                        HttpHeaders headers = UpdatePWDResponse.Headers;
                        IEnumerable<string> values;

                        if (headers.TryGetValues("token", out values)) { NewToken = values.First(); }

                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }

                        GAZTTPProfileResponseJSON = UpdatePWDResponse.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(GAZTTPProfileResponseJSON))
                        {
                            GAZTTPProfileResponseJSON = JObject.Parse(GAZTTPProfileResponseJSON)["data"].ToString();
                            TPProfileData = JsonConvert.DeserializeObject<TaxPayerProfile>(GAZTTPProfileResponseJSON);
                        }
                    }
                }
                else
                    throw new Exception(AppResources.NetworkConnectivityIssue);
            }
            catch (Exception ex)
            {
                
                
                System.Diagnostics.Debug.WriteLine("API RESPONSE ERROR : {0}", ex.Message);

                if (!string.IsNullOrEmpty(GAZTTPProfileResponseJSON))
                {
                    ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(GAZTTPProfileResponseJSON);
                    if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                    {
                        string errorMessage = string.Empty;
                        errorMessage = errorMesg.error.innererror.errordetails[0].message;
                        errorMessage += errorMesg.error.innererror.errordetails[1].message;

                        String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                        errorMessage = WithReplacedString;
                        throw new Exception(errorMessage);
                    }
                }
            }

            return TPProfileData;
        }
        public static async Task<TaxPayerAccountDetail> GetTPAccountDetails(string TIN)
        {
            TaxPayerAccountDetail TPProfileData = null;
            string NewToken = string.Empty;
            string GAZTTPProfileResponseJSON = string.Empty;
            string lang = UtilityManager.GetLanguageParameter();
            try
            {
                string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                client.DefaultRequestHeaders.Add("Authorization", App.Token);
                var url = ZATCAConstants.GAZTGetTPAccountDetails + "?deviceId=" + deviceUdid + "&deviceType=" + deviceOs;
                HttpResponseMessage UpdatePWDResponse = await client.GetAsync(url);
                if (UpdatePWDResponse != null)
                {
                    if (UpdatePWDResponse.StatusCode == HttpStatusCode.OK)
                    {

                        GAZTTPProfileResponseJSON = UpdatePWDResponse.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(GAZTTPProfileResponseJSON))
                        {
                            GAZTTPProfileResponseJSON = JObject.Parse(GAZTTPProfileResponseJSON)["data"].ToString();
                            TPProfileData = JsonConvert.DeserializeObject<TaxPayerAccountDetail>(GAZTTPProfileResponseJSON);
                        }
                    }
                }
                else
                    throw new Exception(AppResources.NetworkConnectivityIssue);
            }
            catch (Exception ex)
            {
                
                
                System.Diagnostics.Debug.WriteLine("API RESPONSE ERROR : {0}", ex.Message);

                if (!string.IsNullOrEmpty(GAZTTPProfileResponseJSON))
                {
                    ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(GAZTTPProfileResponseJSON);
                    if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                    {
                        string errorMessage = string.Empty;
                        errorMessage = errorMesg.error.innererror.errordetails[0].message;
                        errorMessage += errorMesg.error.innererror.errordetails[1].message;

                        String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                        errorMessage = WithReplacedString;
                        throw new Exception(errorMessage);
                    }
                }
            }

            return TPProfileData;
        }

        public static async Task<TaxPayerProfile> POSTTPProfileAPICalls(TPProfileAPIRequest TPProfileAPIRequestPOSTData, string APIType)
        {
            if (NetworkCheck.IsInternet())
            {
                TaxPayerProfile TP = null;
                string GAZTTPProfileResponseJSON = string.Empty;

                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var lang = UtilityManager.GetLanguageParameter();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", "android-20013fbc500");
                    client.DefaultRequestHeaders.Add("X-Device-Name", "Samsung-s20+");
                    client.DefaultRequestHeaders.Add("X-Device-Platform", "android");
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);


                    string url = ZATCAConstants.TPProfileURL;
                    var uri = new Uri(url);

                    try { App.httpClientHandler.CookieContainer = null; }
                    catch (Exception ex)
                    {
                        
                        
                    }
                    var serilized = JsonConvert.SerializeObject(TPProfileAPIRequestPOSTData);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);

                    // HttpResponseMessage res = await client.GetAsync(uri);

                    GAZTTPProfileResponseJSON = res.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(GAZTTPProfileResponseJSON))
                    {
                        GAZTTPProfileResponseJSON = JObject.Parse(GAZTTPProfileResponseJSON)["result"].ToString();
                        TP = JsonConvert.DeserializeObject<TaxPayerProfile>(GAZTTPProfileResponseJSON);
                    }
                }
                catch (Exception ex)
                {
                    
                    

                    if (!string.IsNullOrEmpty(GAZTTPProfileResponseJSON))
                    {
                        ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(GAZTTPProfileResponseJSON);
                        if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                        {
                            string errorMessage = string.Empty;
                            errorMessage = errorMesg.error.innererror.errordetails[0].message;
                            errorMessage += errorMesg.error.innererror.errordetails[1].message;

                            String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                            errorMessage = WithReplacedString;

                            throw new Exception(errorMessage);
                        }
                    }

                    System.Diagnostics.Debug.WriteLine("API RESPONSE ERROR : {0}", ex.Message);
                    if (APIType.Equals("GETOTPMOBILE"))
                        throw new Exception(AppResources.EnterValidMobileNumber);
                    else if (APIType.Equals("VERIFYOTPMOBILE"))
                        throw new Exception(AppResources.InvalidOTP);
                    else if (APIType.Equals("GETOTPEMAIL"))
                        throw new Exception(AppResources.InvalidEmail);
                    else if (APIType.Equals("VERIFYOTPEMAIL"))
                        throw new Exception(AppResources.InvalidOTP);
                }

                return TP;
            }
            else
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
        }
        // * End

        #endregion

        #region Payment Integration Codes

        public static async Task<ASTabIdentification> GAZTGetAccountStatementsTabIdentification()
        {
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                string FbGuid = App.LoginDataRetrieved.FbGuid;
                try
                {
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    ASTabIdentification _asTabIdentification = new ASTabIdentification();
                    char LangZ = GetLangZParameter();
                    String Lang = UtilityManager.GetLanguageParameter();

                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", Lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    String url = ZATCAConstants.AccountStatementTabIdentification + "&formBundleGUID=" + App.LoginDataRetrieved.FbGuid;

                    var uri = new Uri(url);
                    HttpResponseMessage GAZTASTabIdentificationStatus = await client.GetAsync(uri);
                    if (GAZTASTabIdentificationStatus != null)
                    {
                        if (GAZTASTabIdentificationStatus.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            throw new GAZTSessionExpiredException();
                        }
                        HttpHeaders headers = GAZTASTabIdentificationStatus.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                        }
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")) || (0 == String.Compare(NewToken, "")))
                            {
                                App.IsSessionExpired = true;
                                throw new GAZTSessionExpiredException();
                            }
                            App.Token = NewToken;
                        }
                        String data = GAZTASTabIdentificationStatus.Content.ReadAsStringAsync().Result;
                        _asTabIdentification = JsonConvert.DeserializeObject<ASTabIdentification>(data);
                    }
                    return _asTabIdentification;
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

        public static async Task<ASRevenueDropDownSet> GAZTGetAccountStatementsRevenueDropDownSet(string taxType)
        {
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                string FbGuid = App.LoginDataRetrieved.FbGuid;
                try
                {
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    ASRevenueDropDownSet _asTabIdentification = new ASRevenueDropDownSet();
                    char LangZ = GetLangZParameter();
                    String Lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", Lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    String url = ZATCAConstants.AccountStatementRevenueDropDownSet + "&language=" + Lang + "&formBundleGUID=" + App.LoginDataRetrieved.FbGuid + "&taxType=" + taxType;

                    var uri = new Uri(url);
                    HttpResponseMessage GAZTASTabIdentificationStatus = await client.GetAsync(uri);
                    if (GAZTASTabIdentificationStatus != null)
                    {
                        if (GAZTASTabIdentificationStatus.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            throw new GAZTSessionExpiredException();
                        }
                        HttpHeaders headers = GAZTASTabIdentificationStatus.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                        }
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")) || (0 == String.Compare(NewToken, "")))
                            {
                                App.IsSessionExpired = true;
                                throw new GAZTSessionExpiredException();
                            }
                            App.Token = NewToken;
                        }
                        String data = GAZTASTabIdentificationStatus.Content.ReadAsStringAsync().Result;
                        _asTabIdentification = JsonConvert.DeserializeObject<ASRevenueDropDownSet>(data);
                    }

                    return _asTabIdentification;
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

        public static async Task<ASYearValuesHeader> GAZTGetAccountStatementYearValuesHeaderSet(string statementFilter, string taxType)
        {
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                string FbGuid = App.LoginDataRetrieved.FbGuid;
                try
                {
                    ASYearValuesHeader _asTabIdentification = new ASYearValuesHeader();
                    char LangZ = GetLangZParameter();
                    String Lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = ZATCAConstants.AccountStatementGetYearValues + "Fguid eq '" + App.LoginDataRetrieved.FbGuid + "'" + " and TaxType eq '" + taxType + "'" + " and StatementFilter eq '" + statementFilter + "'" + "&$format=json";

                    client.DefaultRequestHeaders.Add("Token", "123");
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTASTabIdentificationStatus = await client.GetAsync(uri);
                    if (GAZTASTabIdentificationStatus != null)
                    {
                        if (GAZTASTabIdentificationStatus.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            throw new GAZTSessionExpiredException();
                        }
                        HttpHeaders headers = GAZTASTabIdentificationStatus.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                        }
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")) || (0 == String.Compare(NewToken, "")))
                            {
                                App.IsSessionExpired = true;
                                throw new GAZTSessionExpiredException();
                            }
                            App.Token = NewToken;
                        }
                        String data = GAZTASTabIdentificationStatus.Content.ReadAsStringAsync().Result;
                        _asTabIdentification = JsonConvert.DeserializeObject<ASYearValuesHeader>(data);
                    }

                    return _asTabIdentification;
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


        public async static Task<ValidatePaymentResponse> GAZTValidatePayment(ValidatePayment PayDetails)

        {

            ValidatePaymentResponse paymentResponse = null;



            string _paymentsubmitResponse = string.Empty;

            try

            {
                string lang = UtilityManager.GetLanguageParameter();
                String url = ZATCAConstants.ValidatePaymentInformation;

                var uri = new Uri(url);

                var serilized = JsonConvert.SerializeObject(PayDetails);
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
                HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);

                HttpResponseMessage res = client.PostAsync(uri, contentPost).Result;

                _paymentsubmitResponse = res.Content.ReadAsStringAsync().Result;

                paymentResponse = JsonConvert.DeserializeObject<ValidatePaymentResponse>(_paymentsubmitResponse);

                if (paymentResponse == null)
                {
                    string errorMessage = PrepareErrorMessageByJson(_paymentsubmitResponse);
                    throw new GAZTVATRegistrationInProcessException(errorMessage);
                }
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
            return paymentResponse;
        }


        public static DashboardInstalmentplan GAZTGetDashboardInstalmentPlanData(string lang, string TIN)
        {
            DashboardInstalmentplan dashboardInstalmentData = null;
            if (NetworkCheck.IsInternet())
            {
                DateTime currentDate = DateTime.Now;
                string NewToken = string.Empty;
                try
                {
                    if (false == NetworkCheck.IsInternet())
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
                    String uri = ZATCAConstants.GetDashboardInstalmentPlanData + TIN + "&language=" + lang;

                    HttpResponseMessage GAZTGetDashboardInstalmentResponse = new HttpResponseMessage();
                    try
                    {
                        GAZTGetDashboardInstalmentResponse = client.GetAsync(uri).Result;
                    }
                    catch (Exception ex)
                    {
                        
                        

                    }
                    if (GAZTGetDashboardInstalmentResponse != null)
                    {
                        if (GAZTGetDashboardInstalmentResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            throw new GAZTSessionExpiredException();
                        }

                        HttpHeaders headers = GAZTGetDashboardInstalmentResponse.Headers;
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
                        string GAZTGetDashboardInstalmentResponseJSON = GAZTGetDashboardInstalmentResponse.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(GAZTGetDashboardInstalmentResponseJSON))
                        {
                            GAZTGetDashboardInstalmentResponseJSON = JObject.Parse(GAZTGetDashboardInstalmentResponseJSON)["data"].ToString();
                            dashboardInstalmentData = JsonConvert.DeserializeObject<DashboardInstalmentplan>(GAZTGetDashboardInstalmentResponseJSON);
                        }
                    }
                }
                catch (JsonReaderException )
                {
                    throw new GAZTInvalidDataException();
                }
                catch (HttpRequestException )
                {
                    return null;
                }
                catch (GAZTSessionExpiredException )
                {
                    return null;
                }
                catch (GAZTException )
                {
                    return null;
                }
                catch (Exception)
                {

                }
            }
            else
            {
                throw new GAZTInternetException();
            }
            return dashboardInstalmentData;
        }



        public static async Task<CancelPaymentResponse> GAZTCancelPayment(string GUID, string type)

        {

            CancelPaymentResponse paymentResponse = null;

            if (NetworkCheck.IsInternet())

            {

                DateTime currentDate = DateTime.Now;

                string NewToken = string.Empty;

                try

                {

                    if (false == NetworkCheck.IsInternet())

                    {

                        throw new GAZTInternetException();

                    }
                    CancelPaymentRequest model = new CancelPaymentRequest();
                    model.sourceId = type;
                    model.GUID = GUID;
                    model.cancellationReason = "01";

                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    HttpClient client = new HttpClient();
                    string lang = UtilityManager.GetLanguageParameter();
                    String url = ZATCAConstants.CancelPaymentService;
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);

                    var serilized = JsonConvert.SerializeObject(model);
                    var uri = new Uri(url);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage GAZTValidatePaymentResponse = new HttpResponseMessage();
                    try
                    {
                        GAZTValidatePaymentResponse = await client.PostAsync(uri, contentPost);
                    }

                    catch (Exception ex)
                    {
                        
                        



                    }

                    if (GAZTValidatePaymentResponse != null)

                    {

                        if (GAZTValidatePaymentResponse.StatusCode == HttpStatusCode.Unauthorized)

                        {

                            throw new GAZTSessionExpiredException();

                        }



                        HttpHeaders headers = GAZTValidatePaymentResponse.Headers;

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


                        String paymentData = await GAZTValidatePaymentResponse.Content.ReadAsStringAsync();

                        paymentResponse = JsonConvert.DeserializeObject<CancelPaymentResponse>(paymentData);

                        if (!string.IsNullOrEmpty(paymentData) && paymentResponse.d == null)

                        {

                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(paymentData);

                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)

                            {

                                string errorMessage = string.Empty;

                                errorMessage = errorMesg.error.innererror.errordetails[0].message;

                                errorMessage += errorMesg.error.innererror.errordetails[1].message;

                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);

                                errorMessage = WithReplacedString;

                                throw new GAZTValidatePaymentInProcessException(errorMessage);

                            }

                        }



                    }

                }

                catch (JsonReaderException )

                {
                    throw new GAZTInvalidDataException();

                }

                catch (HttpRequestException )

                {
                    return null;
                }

                catch (GAZTSessionExpiredException)

                {
                    return null;
                }

                catch (GAZTException)

                {
                    return null;

                }

                catch (Exception ex)
                {
                    
                    


                }

            }

            else

            {

                throw new GAZTInternetException();

            }

            return paymentResponse;

        }





        public static async Task<ValidatePaymentResponse> GAZTValidateMyBillsPayment(string fbNum, string TIN, string devicetype, string sadadNo, string paymentType)

        {

            ValidatePaymentResponse paymentResponse = null;

            if (NetworkCheck.IsInternet())

            {

                DateTime currentDate = DateTime.Now;

                string NewToken = string.Empty;

                try

                {

                    if (false == NetworkCheck.IsInternet())

                    {

                        throw new GAZTInternetException();

                    }
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    string lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    var model = new ValidatePaymentRequestModel()
                    {
                        amount = 100,
                        formBundleNumber = fbNum,
                        link = "",
                        sadadBillNumber = sadadNo,
                        paymentType = paymentType,
                        TIN = TIN,

                    };
                    var serializedTokenRequest = JsonConvert.SerializeObject(model);
                    //String uri = ZATCAConstants.ValidatePaymentInformation + "'" + fbNum + "',Tin='" + TIN + "',Srcid='" + devicetype + "')" + "?$format=json";

                    String uri = ZATCAConstants.ValidatePaymentInformation;







                    HttpResponseMessage GAZTValidatePaymentResponse = new HttpResponseMessage();

                    try

                    {

                        GAZTValidatePaymentResponse = await client.GetAsync(uri);

                    }

                    catch (Exception ex)
                    {

                        
                        



                    }

                    if (GAZTValidatePaymentResponse != null)

                    {

                        if (GAZTValidatePaymentResponse.StatusCode == HttpStatusCode.Unauthorized)

                        {

                            throw new GAZTSessionExpiredException();

                        }



                        HttpHeaders headers = GAZTValidatePaymentResponse.Headers;

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



                        String paymentData = await GAZTValidatePaymentResponse.Content.ReadAsStringAsync();

                        paymentResponse = JsonConvert.DeserializeObject<ValidatePaymentResponse>(paymentData);

                        if (!string.IsNullOrEmpty(paymentData) && paymentResponse.d == null)

                        {

                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(paymentData);

                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)

                            {

                                string errorMessage = string.Empty;

                                errorMessage = errorMesg.error.innererror.errordetails[0].message;

                                errorMessage += errorMesg.error.innererror.errordetails[1].message;

                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);

                                errorMessage = WithReplacedString;

                                throw new GAZTValidatePaymentInProcessException(errorMessage);

                            }

                        }



                    }

                }

                catch (JsonReaderException ex)

                {

                    throw new GAZTInvalidDataException();

                }

                catch (HttpRequestException )

                {
                }

                catch (GAZTSessionExpiredException )

                {
                }

                catch (GAZTException )

                {
                }

                catch (Exception ex)
                {
                    
                    
                }

            }

            else

            {

                throw new GAZTInternetException();

            }

            return paymentResponse;

        }





        public static async Task<MadaPaymentResponse> GAZTUpdateMadaPaymentDetails(string caseGuid, string devicetype)
        {
            MadaPaymentResponse paymentResponse = null;
            if (NetworkCheck.IsInternet())
            {
                DateTime currentDate = DateTime.Now;
                string NewToken = string.Empty;
                try
                {
                    if (!NetworkCheck.IsInternet())
                    {
                        throw new GAZTInternetException();
                    }
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
                    String url = ZATCAConstants.UpdateMadaPaymentInformation + "?caseGUID=" + caseGuid + "&paymentSourceId=" + devicetype;

                    var uri = new Uri(url);
                    HttpResponseMessage GAZTValidatePaymentResponse = new HttpResponseMessage();
                    try
                    {
                        GAZTValidatePaymentResponse = await client.GetAsync(uri);
                    }
                    catch (Exception ex)
                    {
                        
                        
                    }

                    if (GAZTValidatePaymentResponse != null)
                    {
                        if (GAZTValidatePaymentResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            throw new GAZTSessionExpiredException();
                        }
                        HttpHeaders headers = GAZTValidatePaymentResponse.Headers;

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
                        String paymentData = await GAZTValidatePaymentResponse.Content.ReadAsStringAsync();
                        paymentResponse = JsonConvert.DeserializeObject<MadaPaymentResponse>(paymentData);
                        if (!string.IsNullOrEmpty(paymentData) && paymentResponse.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(paymentData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorCode = AppResources.ZError;
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                errorCode += errorMesg.error.innererror.errordetails[1].code;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                throw new GAZTValidateMadaPaymentException(errorCode, errorMessage);
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
                catch (GAZTSessionExpiredException gex)
                {
                    throw gex;
                }
                catch (GAZTException gex)
                {
                    throw gex;
                }
                catch (Exception ex)
                {
                    
                    
                    //throw new GAZTNetworkConnectivityIssueException();
                }
            }
            else
            {
                throw new GAZTInternetException();
            }
            return paymentResponse;
        }

        public static async Task<CreateMadaResponseRoot> GAZTCreateMadaPayment(CreateMadaPaymentPayload paymentPayload)
        {
            CreateMadaResponseRoot paymentResponse = null;
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
                    String url = ZATCAConstants.CreateMadaPaymentInformation;

                    var uri = new Uri(url);
                    var serilized = JsonConvert.SerializeObject(paymentPayload);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage GAZTValidatePaymentResponse = new HttpResponseMessage();
                    try
                    {
                        GAZTValidatePaymentResponse = await client.PostAsync(uri, contentPost);
                    }
                    catch (Exception ex)
                    {
                        
                        
                    }
                    if (GAZTValidatePaymentResponse != null)
                    {
                        if (GAZTValidatePaymentResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            throw new GAZTSessionExpiredException();
                        }
                        HttpHeaders headers = GAZTValidatePaymentResponse.Headers;

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
                        String paymentData = await GAZTValidatePaymentResponse.Content.ReadAsStringAsync();
                        paymentResponse = JsonConvert.DeserializeObject<CreateMadaResponseRoot>(paymentData);
                        if (!string.IsNullOrEmpty(paymentData) && paymentResponse.result == null)
                        {
                            string errorCode = AppResources.ZError;
                            string errorMessage = PrepareErrorMessageByJson(paymentData);
                            throw new GAZTValidateMadaPaymentException(errorCode, errorMessage);
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
                catch (GAZTSessionExpiredException gex)
                {
                    throw gex;
                }
                catch (GAZTException gex)
                {
                    throw gex;
                }
                catch (Exception ex)
                {
                    
                    
                    //throw new GAZTNetworkConnectivityIssueException();
                }
            }
            else
            {
                throw new GAZTInternetException();
            }
            return paymentResponse;
        }




        public async static Task<ApplePayGuidResponse> GAZTGenerateApplePayGuid(ApplePayRequestGuid applePayDetails)

        {

            ApplePayGuidResponse paymentResponse = null;



            string _paymentsubmitResponse = string.Empty;

            try

            {

                String url = ZATCAConstants.ApplePayGenerateGuid;

                var uri = new Uri(url);

                HttpClient client = new HttpClient(App.httpClientHandler);

                var serilized = JsonConvert.SerializeObject(applePayDetails);

                client.DefaultRequestHeaders.Add("Token", App.Token);

                client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

                client.DefaultRequestHeaders.Add("X-Requested-With", "X");

                client.DefaultRequestHeaders.Add("Accept", "application/json");






                HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);

                HttpResponseMessage res = client.PostAsync(uri, contentPost).Result;

                _paymentsubmitResponse = res.Content.ReadAsStringAsync().Result;



                paymentResponse = JsonConvert.DeserializeObject<ApplePayGuidResponse>(_paymentsubmitResponse);

                if (!string.IsNullOrEmpty(_paymentsubmitResponse))

                {

                    ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_paymentsubmitResponse);

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



            }

            catch (GAZTVATRegistrationInProcessException ex)

            {

                throw new GAZTVATRegistrationInProcessException(ex.Message);

            }

            catch (Exception ex)
            {
                
                



                App.IsSessionExpired = true;

                return null;

            }

            return paymentResponse;





        }



        public async static Task<ApplePayTokenResponse> GAZTUpdateApplePayGuid(ApplePayToken applePayDetails)

        {

            ApplePayTokenResponse paymentResponse = null;



            string _paymentsubmitResponse = string.Empty;

            try

            {



                string platform = "";



                if (DeviceInfo.Platform == DevicePlatform.iOS)

                {

                    platform = "C4";

                }

                else if (DeviceInfo.Platform == DevicePlatform.Android)

                {

                    platform = "C3";

                }

                applePayDetails.SrcId = platform;


                string lang = UtilityManager.GetLanguageParameter();
                String url = ZATCAConstants.UpdateApplePayGuid;

                var uri = new Uri(url);

                HttpClient client = new HttpClient();

                var serilized = JsonConvert.SerializeObject(applePayDetails);

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

                HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);

                HttpResponseMessage res = await client.PostAsync(uri, contentPost);

                _paymentsubmitResponse = await res.Content.ReadAsStringAsync();

                paymentResponse = JsonConvert.DeserializeObject<ApplePayTokenResponse>(_paymentsubmitResponse);

                if (!string.IsNullOrEmpty(_paymentsubmitResponse))

                {

                    ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_paymentsubmitResponse);

                    if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)

                    {

                        string errorMessage = string.Empty;

                        errorMessage = errorMesg.error.innererror.errordetails[0].message;

                        errorMessage += errorMesg.error.innererror.errordetails[1].message;

                        String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);

                        errorMessage = WithReplacedString;

                        throw new GAZTValidatePaymentInProcessException(errorMessage);

                    }

                }



            }

            catch (GAZTValidatePaymentInProcessException ex)

            {

                throw new GAZTValidatePaymentInProcessException(ex.Message);

            }

            catch (Exception ex)
            {
                
                
                return null;
            }

            return paymentResponse;



        }


        public static async Task<ASStatementHeaderSet> GAZTGetAccountStatementHeaderSet(string statementFilter, string fiscalYear, string taxType, bool isFromDashboard)
        {

            if (NetworkCheck.IsInternet())
            {

                string NewToken = string.Empty;

                string FbGuid = App.LoginDataRetrieved.FbGuid;

                try

                {

                    ASStatementHeaderSet _asTabIdentification = new ASStatementHeaderSet();

                    char LangZ = GetLangZParameter();

                    String Lang = UtilityManager.GetLanguageParameter();

                    HttpClient client = new HttpClient();

                    String isLoad = "";

                    if (!string.IsNullOrEmpty(statementFilter) && statementFilter == "10")

                    {
                        if (isFromDashboard)
                        {

                            isLoad = "X";
                        }

                    }
                    if (taxType == "I")
                    {
                        taxType = "Indirect";
                    }
                    else
                    {
                        taxType = "Direct";
                    }

                    String url = ZATCAConstants.AccountStatementGetHeaderSet + isLoad + "&language=" + Lang + "&formBundleGUID=" + App.LoginDataRetrieved.FbGuid + "&statementFilter=" + statementFilter + "&taxType=" + taxType + "&fiscalYear=" + fiscalYear;

                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", Lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", App.IncomingChannel);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);


                    var uri = new Uri(url);

                    HttpResponseMessage GAZTASTabIdentificationStatus = await client.GetAsync(uri);



                    if (GAZTASTabIdentificationStatus != null)
                    {

                        if (GAZTASTabIdentificationStatus.StatusCode == HttpStatusCode.Unauthorized)

                        {

                            App.IsSessionExpired = true;

                            throw new GAZTSessionExpiredException();

                        }

                        HttpHeaders headers = GAZTASTabIdentificationStatus.Headers;

                        IEnumerable<string> values;



                        if (headers.TryGetValues("token", out values))

                        {

                            NewToken = values.First();

                        }

                        if ((!string.IsNullOrEmpty(NewToken)))

                        {

                            if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")) || (0 == String.Compare(NewToken, "")))

                            {

                                App.IsSessionExpired = true;

                                throw new GAZTSessionExpiredException();

                            }

                            App.Token = NewToken;

                        }



                        String data = GAZTASTabIdentificationStatus.Content.ReadAsStringAsync().Result;

                        _asTabIdentification = JsonConvert.DeserializeObject<ASStatementHeaderSet>(data);

                    }



                    return _asTabIdentification;

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


        public static async Task<Dashboard> GAZTGetDashboardData(string lang, string TIN)

        {

            Dashboard dashboardData = null;

            if (NetworkCheck.IsInternet())

            {

                string NewToken = string.Empty;

                try

                {

                    if (false == NetworkCheck.IsInternet())

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

                    string uri = ZATCAConstants.GetDashboardData + TIN;

                    HttpResponseMessage GAZTGetDashboardResponse = new HttpResponseMessage();

                    try
                    {
                        GAZTGetDashboardResponse = await client.GetAsync(uri);
                    }
                    catch (Exception ex)
                    {
                        
                        
                    }

                    if (GAZTGetDashboardResponse != null)

                    {

                        if (GAZTGetDashboardResponse.StatusCode == HttpStatusCode.Unauthorized)

                        {

                            throw new GAZTSessionExpiredException();

                        }



                        HttpHeaders headers = GAZTGetDashboardResponse.Headers;

                        IEnumerable<string> values = null;

                        //if (headers.TryGetValues("token", out values))

                        //{

                        //    NewToken = values.First();

                        //}

                        //if ((!string.IsNullOrEmpty(NewToken)))

                        //{

                        //    if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")))

                        //    {

                        //        throw new GAZTSessionExpiredException();

                        //    }

                        //    App.Token = NewToken;

                        //}

                        string GAZTGetDashboardResponseJSON = await GAZTGetDashboardResponse.Content.ReadAsStringAsync();

                        if (!string.IsNullOrEmpty(GAZTGetDashboardResponseJSON))

                        {

                            //GAZTGetDashboardResponseJSON = JObject.Parse(GAZTGetDashboardResponseJSON)["data"].ToString();

                            dashboardData = JsonConvert.DeserializeObject<Dashboard>(GAZTGetDashboardResponseJSON);

                            if (!string.IsNullOrEmpty(GAZTGetDashboardResponseJSON) && dashboardData == null)
                            {
                                ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(GAZTGetDashboardResponseJSON);
                                if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                                {
                                    string errorMessage = string.Empty;
                                    errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                    errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                    String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                    errorMessage = WithReplacedString;
                                    throw new GAZTErrorException(errorMessage);
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

                catch (GAZTSessionExpiredException)

                {
                }

                catch (GAZTException)

                {
                }

                catch (Exception ex)
                {
                    
                    

                }

            }

            else

            {

                throw new GAZTInternetException();

            }

            return dashboardData;

        }

        public async static Task<string> SendNotificationToAuditorafterUploadingAttachments(VATDeclarationD _vatReturnsData)
        {
            _vatReturnsData.Operationz = "29";

            VATDeclaration dashBoardViewResponse = new VATDeclaration();
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    string LangZAREN = WebServiceManager.GetLangZParameterAREN();

                    //char LangZ = GetLangZParameter();
                    //String Lang = UtilityManager.GetLanguageParameter();
                    String url = ZATCAConstants.GAZTGetNotifyAuditorForAddingAttachments;
                    var uri = new Uri(url);
                    // HttpClient client = new HttpClient(App.httpClientHandler);

                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", LangZAREN);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);

                    var serilized = JsonConvert.SerializeObject(_vatReturnsData);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    var detailJson = res.Content.ReadAsStringAsync().Result;
                    dashBoardViewResponse = JsonConvert.DeserializeObject<VATDeclaration>(detailJson);

                    if (res.IsSuccessStatusCode)
                    {

                    }
                    else
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Somethingwentwrong));

                        ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
                        if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                        {
                            WebServiceManager.ErrorMessageForVAT = errorMesg.error.innererror.errordetails[0].message;
                            WebServiceManager.ErrorMessageForVAT += errorMesg.error.innererror.errordetails[1].message;
                            String WithReplacedString = WebServiceManager.ErrorMessageForVAT.Replace("An exception was raised", string.Empty);
                            WebServiceManager.ErrorMessageForVAT = WithReplacedString;
                            //ErrorMessageForVAT
                            throw new GAZTVATRegistrationInProcessException(WebServiceManager.ErrorMessageForVAT);
                        }
                    }

                    return "";
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

        //--CR6264
        public static async Task<ProfitGoods> SaveVAtProfitGoodsAsync(VATFoodResults modelDetails)
        {
            if (NetworkCheck.IsInternet())
            {
                try
                {

                    //ZakatInstalmentPlanResponse _zakatResponseObject = new ZakatInstalmentPlanResponse();
                    string LangZ = WebServiceManager.GetLangZParameterAREN();
                    String url = ZATCAConstants.TaxpayervatgoodsAmrgin;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient();
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;

                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", LangZ);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    var serilized = JsonConvert.SerializeObject(modelDetails);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = client.PostAsync(uri, contentPost).Result;
                    var _zakatReturnDetailsDesponsestr = res.Content.ReadAsStringAsync().Result;
                    _zakatReturnDetailsDesponsestr = JObject.Parse(_zakatReturnDetailsDesponsestr)["result"].ToString();
                    ProfitGoods profit = JsonConvert.DeserializeObject<ProfitGoods>(_zakatReturnDetailsDesponsestr);
                    if (res.IsSuccessStatusCode)
                    {

                    }
                    else
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Somethingwentwrong));

                        ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatReturnDetailsDesponsestr);
                        if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                        {
                            WebServiceManager.ErrorMessageForVAT = errorMesg.error.innererror.errordetails[0].message;
                            WebServiceManager.ErrorMessageForVAT += errorMesg.error.innererror.errordetails[1].message;
                            String WithReplacedString = WebServiceManager.ErrorMessageForVAT.Replace("An exception was raised", string.Empty);
                            WebServiceManager.ErrorMessageForVAT = WithReplacedString;
                            //ErrorMessageForVAT
                            throw new GAZTVATRegistrationInProcessException(WebServiceManager.ErrorMessageForVAT);
                        }
                    }
                    return profit;

                }

                catch (Exception ex)
                {
                    
                    
                    //App.IsSessionExpired = true;
                    return null;
                }

            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }

        }
        public static async Task<ProfitGoods> GetVATProfitGoodsAsync(string TIN)
        {
            if (NetworkCheck.IsInternet())
            {
                try
                {


                    string LangZ = WebServiceManager.GetLangZParameterAREN();
                    String url = ZATCAConstants.TaxpayervatgoodsAmrgin + "?TIN=" + TIN + "&formBundleGUID=" + App.LoginDataRetrieved.FbGuid;
                    var uri = new Uri(url);

                    HttpClient client = new HttpClient();
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;

                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", LangZ);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);

                    HttpResponseMessage response = await client.GetAsync(uri);
                    if (response != null)
                    {
                        var _profitGoodsResponse = response.Content.ReadAsStringAsync().Result;
                        var profitGoods = JsonConvert.DeserializeObject<ProfitGoodsResponse>(_profitGoodsResponse);
                        if (profitGoods != null && profitGoods.data != null)
                        {
                            return profitGoods.data;
                        }
                        else
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_profitGoodsResponse);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                WebServiceManager.ErrorMessageForVAT = errorMesg.error.innererror.errordetails[0].message;
                                WebServiceManager.ErrorMessageForVAT += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = WebServiceManager.ErrorMessageForVAT.Replace("An exception was raised", string.Empty);
                                WebServiceManager.ErrorMessageForVAT = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(WebServiceManager.ErrorMessageForVAT);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
            return null;
        }
        public static async Task<CaptchaResponse> CaptchaRequest(CAptchRequest model)
        {
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string lang = WebServiceManager.GetLangZParameterAREN();
                    string url = ZATCAConstants.CaptchaAndGUID;

                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", "EN");
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);


                    var serilized = JsonConvert.SerializeObject(model);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(url, contentPost);
                    var detailJson = res.Content.ReadAsStringAsync().Result;
                    var dataresponse = JsonConvert.DeserializeObject<CaptchaResponse>(detailJson);
                    if (!string.IsNullOrEmpty(detailJson) && dataresponse?.result == null)
                    {
                        ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
                        if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                        {
                            string errorMessage = string.Empty;
                            errorMessage = errorMesg.error.innererror.errordetails[0].message;
                            errorMessage += errorMesg.error.innererror.errordetails[1].message;
                            String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                            errorMessage = WithReplacedString;
                            throw new GAZTErrorException(errorMessage);
                        }
                    }

                    return dataresponse;
                }
                catch (GAZTErrorException ex)
                {
                    throw new GAZTErrorException(ex.Message);
                }
                catch (Exception ex)
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
        public static async Task<OTPResponse> SendOTP(OTPRequest otpRequest)
        {
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string lang = WebServiceManager.GetLangZParameterAREN();
                    string url = ZATCAConstants.FogotPasswordSendOTP;
                    // string url = "https://test-api.zatca.gov.sa/test/third-party/v1/passwords/forgot-password/otp";
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", UtilityManager.GetLanguageParameter());
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);

                    var serilized = JsonConvert.SerializeObject(otpRequest);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(url, contentPost);
                    var detailJson = res.Content.ReadAsStringAsync().Result;
                    var dataresponse = JsonConvert.DeserializeObject<OTPResponse>(detailJson);
                    if (!string.IsNullOrEmpty(detailJson) && dataresponse.result == null)
                    {
                        ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
                        if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                        {
                            string errorMessage = string.Empty;
                            errorMessage = errorMesg.error.innererror.errordetails[0].message;
                            errorMessage += errorMesg.error.innererror.errordetails[1].message;
                            String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                            errorMessage = WithReplacedString;
                            throw new GAZTErrorException(errorMessage);
                        }
                    }
                    return dataresponse;
                }
                catch (GAZTErrorException ex)
                {
                    throw new GAZTErrorException(ex.Message);
                }
                catch (Exception ex)
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

        //CR6094
        public static async Task<LoginSSOModelClassERAD> LoginDataSSO()
        {
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    var guid = "";
                    if (string.IsNullOrEmpty(App.GUIDFrSSO))
                    {
                        guid = "";
                    }
                    else
                    {
                        guid = App.GUIDFrSSO;

                    }
                    // String url = ZATCAConstants.GetLoginDetaialsSSO + guid + "'&$format=json&sap-language=" + LangZAREN;
                    String url = ZATCAConstants.GAZTGetVATSignUpCaseIdURL + "&GUID=" + guid;
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var lang = UtilityManager.GetLanguageParameter();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", "android-20013fbc500");
                    client.DefaultRequestHeaders.Add("X-Device-Name", "Samsung-s20+");
                    client.DefaultRequestHeaders.Add("X-Device-Platform", "android");

                    // HttpResponseMessage res = await GetServiceManager.MakeGetAPICall(url, false, "");
                    var uri = new Uri(url);
                    HttpResponseMessage res = await client.GetAsync(uri);
                    var _zakatReturnDetailsDesponsestr = res.Content.ReadAsStringAsync().Result;
                    //  _zakatReturnDetailsDesponsestr = JObject.Parse(_zakatReturnDetailsDesponsestr)["data"].ToString();
                    LoginSSOModelClassERAD SSOModel = JsonConvert.DeserializeObject<LoginSSOModelClassERAD>(_zakatReturnDetailsDesponsestr);

                    if (res.IsSuccessStatusCode)
                    {

                    }
                    else
                    {
                        await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(AppResources.Somethingwentwrong));

                        ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatReturnDetailsDesponsestr);
                        if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                        {
                            WebServiceManager.ErrorMessageForVAT = errorMesg.error.innererror.errordetails[0].message;
                            WebServiceManager.ErrorMessageForVAT += errorMesg.error.innererror.errordetails[1].message;
                            String WithReplacedString = WebServiceManager.ErrorMessageForVAT.Replace("An exception was raised", string.Empty);
                            WebServiceManager.ErrorMessageForVAT = WithReplacedString;
                            //ErrorMessageForVAT
                            throw new GAZTVATRegistrationInProcessException(WebServiceManager.ErrorMessageForVAT);
                        }
                    }
                    return SSOModel;

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
        public static async Task<HttpResponseMessage> LoginRequest(LoginRequestModel model)
        {
            HttpClient client = new HttpClient();
            string lang = UtilityManager.GetLanguageParameter();
            string url = ZATCAConstants.GAZTLogin;

            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("X-Session-Language", lang);
            client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
            client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);

            var uri = new Uri(url);
            var serilized = JsonConvert.SerializeObject(model);
            client.Timeout = new TimeSpan(0, 0, 180);
            HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
            var loginResponse = await client.PostAsync(uri, contentPost);

            return loginResponse;

        }
        public static async Task<HttpResponseMessage> ValidateOTP(TokenRequestModel model)
        {
            using (var client = new HttpClient())
            {
                string lang = UtilityManager.GetLanguageParameter();
                string url = ZATCAConstants.GAZTValidateLoginOTP;
                var tokenUri = new Uri(url);
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

                var serializedTokenRequest = JsonConvert.SerializeObject(model);
                HttpContent tokenContent = new StringContent(serializedTokenRequest, Encoding.UTF8, ZATCAConstants.ContentType);
                var tokenResponse = await client.PostAsync(tokenUri, tokenContent);
                return tokenResponse;
            }
        }
        public static async Task<PasswordChangeResponse> ChangePassword(PasswordChangeRequest passwordChangeRequest)
        {
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    HttpClient client = new HttpClient();
                    string lang = WebServiceManager.GetLangZParameterAREN();
                    string url = ZATCAConstants.ChangePassword;
                    // var uri = new Uri(url);

                    //  HttpClient client = new HttpClient(crmSignUphttpClientHandler);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", "EN");
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);

                    var serilized = JsonConvert.SerializeObject(passwordChangeRequest);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(url, contentPost);
                    var detailJson = res.Content.ReadAsStringAsync().Result;
                    var dataresponse = JsonConvert.DeserializeObject<PasswordChangeResponse>(detailJson);
                    //var serilized = JsonConvert.SerializeObject(otpRequest);
                    //        Console.WriteLine("RequestOTP" + serilized);
                    //        // var serilized = "{\r\n  \"TIN\": \"3311688087\",\r\n  \"email\": \"JABUYASIN-C@ZATCA.GOV.SA\",\r\n  \"birthDate\": \"1436-10-09T22:50:00\",\r\n  \"language\": \"EN\",\r\n  \"captchaCode\": \"6jldgr\",\r\n  \"GUID\": \"005056B1365C1EEDB6898A55D5A2433F\"\r\n}";
                    //        HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    //        HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    //        var detailJson = res.Content.ReadAsStringAsync().Result;
                    //        var OTPREsponses = JsonConvert.DeserializeObject<OTPResponse>(detailJson);

                    if (!string.IsNullOrEmpty(detailJson) && dataresponse.result == null)
                    {
                        ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
                        if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                        {
                            string errorMessage = string.Empty;
                            errorMessage = errorMesg.error.innererror.errordetails[0].message;
                            errorMessage += errorMesg.error.innererror.errordetails[1].message;
                            String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                            errorMessage = WithReplacedString;
                            throw new GAZTErrorException(errorMessage);
                        }
                    }
                    //Console.WriteLine("forgotPasswordResponse" + forgotPasswordResponse);
                    return dataresponse;
                }
                catch (GAZTErrorException ex)
                {
                    throw new GAZTErrorException(ex.Message);
                }
                catch (Exception ex)
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
        public static async Task<ValidateOTPREsponse> ValidateOTPNEW(VAliadteOTP vAliadteOTP)
        {
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string lang = WebServiceManager.GetLangZParameterAREN();

                    string url = ZATCAConstants.ValidateOTP;
                    // var uri = new Uri(url);

                    //  HttpClient client = new HttpClient(crmSignUphttpClientHandler);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", "EN");
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);

                    var serilized = JsonConvert.SerializeObject(vAliadteOTP);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(url, contentPost);
                    var detailJson = res.Content.ReadAsStringAsync().Result;
                    var dataresponse = JsonConvert.DeserializeObject<ValidateOTPREsponse>(detailJson);
                    //var serilized = JsonConvert.SerializeObject(otpRequest);
                    //        Console.WriteLine("RequestOTP" + serilized);
                    //        // var serilized = "{\r\n  \"TIN\": \"3311688087\",\r\n  \"email\": \"JABUYASIN-C@ZATCA.GOV.SA\",\r\n  \"birthDate\": \"1436-10-09T22:50:00\",\r\n  \"language\": \"EN\",\r\n  \"captchaCode\": \"6jldgr\",\r\n  \"GUID\": \"005056B1365C1EEDB6898A55D5A2433F\"\r\n}";
                    //        HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    //        HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    //        var detailJson = res.Content.ReadAsStringAsync().Result;
                    //        var OTPREsponses = JsonConvert.DeserializeObject<OTPResponse>(detailJson);

                    if (!string.IsNullOrEmpty(detailJson) && dataresponse.result == null)
                    {
                        ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
                        if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                        {
                            string errorMessage = string.Empty;
                            errorMessage = errorMesg.error.innererror.errordetails[0].message;
                            errorMessage += errorMesg.error.innererror.errordetails[1].message;
                            String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                            errorMessage = WithReplacedString;
                            throw new GAZTErrorException(errorMessage);
                        }
                    }
                    //Console.WriteLine("forgotPasswordResponse" + forgotPasswordResponse);
                    return dataresponse;
                }
                catch (GAZTErrorException ex)
                {
                    throw new GAZTErrorException(ex.Message);
                }
                catch (Exception ex)
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
        public static async Task<ObservableCollection<MyBills>> GetUserBills(string Tin, string lang)
        {
            ObservableCollection<MyBills> myBills = new ObservableCollection<MyBills>();
            string NewToken = string.Empty;
            string BillResponse = string.Empty;
            // MyBillsResponse myBillsResponse = null;
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                client.DefaultRequestHeaders.Add("Authorization", App.Token);
                String url = ZATCAConstants.GetMyBills + App.TP.TIN;
                HttpResponseMessage UpdatePWDResponse = await client.GetAsync(url);



                //  HttpResponseMessage UpdatePWDResponse = await client.GetAsync(url);
                if (UpdatePWDResponse != null)
                {
                    if (UpdatePWDResponse.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        App.IsSessionExpired = true;
                        return null;
                    }


                    if (UpdatePWDResponse.Headers != null)
                    {
                        HttpHeaders headers = UpdatePWDResponse.Headers;
                        IEnumerable<string> values;



                        if (headers.TryGetValues("token", out values)) { NewToken = values.First(); }



                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }



                        BillResponse = UpdatePWDResponse.Content.ReadAsStringAsync().Result;
                        BillResponse = JObject.Parse(BillResponse)["data"].ToString();
                        if (string.IsNullOrEmpty(BillResponse) != true)
                        {
                            myBills = JsonConvert.DeserializeObject<ObservableCollection<MyBills>>(BillResponse);
                        }
                        return myBills;
                    }
                }
                else
                    throw new Exception(AppResources.NetworkConnectivityIssue);



            }
            catch (Exception ex)
            {
                
                
                System.Diagnostics.Debug.WriteLine("API RESPONSE ERROR : {0}", ex.Message);



                if (!string.IsNullOrEmpty(BillResponse))
                {
                    ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(BillResponse);
                    if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                    {
                        string errorMessage = string.Empty;
                        errorMessage = errorMesg.error.innererror.errordetails[0].message;
                        errorMessage += errorMesg.error.innererror.errordetails[1].message;



                        String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                        errorMessage = WithReplacedString;
                        throw new Exception(errorMessage);
                    }
                }
            }




            return myBills;
        }
        public static async Task<HttpResponseMessage> ResendToken(ResentTokenRequestModel model)
        {
            using (var client = new HttpClient())
            {
                string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                string lang = WebServiceManager.GetLangZParameterAREN();
                string url = ZATCAConstants.GAZTResendOTP;
                var tokenUri = new Uri(url);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                client.DefaultRequestHeaders.Add("Authorization", App.Token);
                var serializedTokenRequest = JsonConvert.SerializeObject(model);
                HttpContent tokenContent = new StringContent(serializedTokenRequest, Encoding.UTF8, ZATCAConstants.ContentType);
                var tokenResponse = await client.PostAsync(tokenUri, tokenContent);
                return tokenResponse;
            }
        }

        public async static Task<string> GAZTValidateIDTypesDelecration(string IDType, string IDNumber, string DBO)
        {
            if (NetworkCheck.IsInternet())
            {
                IDTypeValidateRootObject SignupIsIDTypeValid = new IDTypeValidateRootObject();
                string IsIDTypeValidList = string.Empty;
                string NewToken = string.Empty;
                try
                {
                    AbsherTaxPayerRequest absherTaxPayerRequest = new AbsherTaxPayerRequest();
                    absherTaxPayerRequest.idType = IDType;
                    absherTaxPayerRequest.idNumber = IDNumber;
                    absherTaxPayerRequest.taxpayerBirthDate = DBO;
                    var lang = UtilityManager.GetLanguageParameter();
                    String url = ZATCAConstants.GAZTSiguupValidateIDTypesDecl;
                    //+ "(Tin='',Idtype='" + IDType + "',Idnum='" + IDNumber + "',Country='',PassExpDt='',TaxpDob='" + DBO + "')?sap-language=" + lang + "&$format=json&saml2=enabled";

                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    var serilized = JsonConvert.SerializeObject(absherTaxPayerRequest);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage SignupIsIDTypeValidList = await client.PostAsync(url, contentPost);
                    var detailJson = SignupIsIDTypeValidList.Content.ReadAsStringAsync().Result;

                    //HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    //crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };


                    //HttpClient client = new HttpClient(crmSignUphttpClientHandler);
                    //tring url = ZATCAConstants.GAZTSiguupValidateIDTypesDecl + "(Tin='',Idtype='" + IDType + "',Idnum='" + IDNumber + "',Country='',PassExpDt='',TaxpDob='" + DBO + "')?sap-language=" + lang + "&$format=json&saml2=enabled";
                    //var uri = new Uri(url);
                    //HttpResponseMessage SignupIsIDTypeValidList = await client.GetAsync(uri);
                    if (SignupIsIDTypeValidList != null)
                    {
                        if (SignupIsIDTypeValidList.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = SignupIsIDTypeValidList.Headers;
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
                        IsIDTypeValidList = await SignupIsIDTypeValidList.Content.ReadAsStringAsync();
                    }
                    return IsIDTypeValidList;
                }

                catch (JsonReaderException)
                {
                    throw new GAZTInvalidDataException();
                }
                catch (HttpRequestException)
                {
                    return null;
                }
                catch (GAZTSessionExpiredException)
                {
                    return null;
                }
                catch (GAZTException )
                {
                    return null;
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

        public static async Task<Tuple<HttpResponseMessage, string>> GetTPManagerDetails()
        {
            HttpResponseMessage response = null;
            string result = string.Empty;
            if (NetworkCheck.IsInternet())
            {

                try
                {
                    string LangZAREN = WebServiceManager.GetLangZParameterAREN();
                    //String url = ZATCAConstants.GetTpManagersList + "Gpart eq '" + App.TP.Tin + "'&$format=json";
                    String url = ZATCAConstants.GetTpManagersList + "?TIN=" + App.TP.Tin;
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    //client.DefaultRequestHeaders.Add("Token", "123");
                    //client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    //client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    //client.DefaultRequestHeaders.Add("Accept", "application/json");

                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;

                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", LangZAREN);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);

                    //response = await GetServiceManager.MakeGetAPICall(url, false, "");
                    var uri = new Uri(url);
                    response = await client.GetAsync(uri);
                    result = response.Content.ReadAsStringAsync().Result.ToString();


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
            return new Tuple<HttpResponseMessage, string>(response, result);
        }

        public static async Task<Tuple<HttpResponseMessage, string>> SaveManagersList(ManagerDetailsPayload modelDetails)
        {
            HttpResponseMessage response = null;
            string result = string.Empty;
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    string LangZ = WebServiceManager.GetLangZParameterAREN();
                    String url = ZATCAConstants.PostTpManagersList;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var serilized = JsonConvert.SerializeObject(modelDetails);
                    //client.DefaultRequestHeaders.Add("Token", "123");
                    //client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    //client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    //client.DefaultRequestHeaders.Add("Accept", "application/json");

                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;

                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", LangZ);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);

                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    response = client.PostAsync(uri, contentPost).Result;
                    result = response.Content.ReadAsStringAsync().Result;
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
            return new Tuple<HttpResponseMessage, string>(response, result);
        }

        public static async Task<NafathLoginResponseModel> NafathLogin(NafathLoginRequestModel model)
        {
            NafathLoginResponseModel response = null;
            if (NetworkCheck.IsInternet())
            {
                //string lang = WebServiceManager.GetLangZParameterAREN();
                String url = ZATCAConstants.NafathAuthentication;
                var uri = new Uri(url);
                HttpClient client = new HttpClient(App.httpClientHandler);

                //client.DefaultRequestHeaders.Add("Token", "123");
                //client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                //client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);

                var serilized = JsonConvert.SerializeObject(model);
                HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                var apiResponse = await client.PostAsync(uri, contentPost);
                var result = await apiResponse.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<NafathLoginResponseModel>(result);
            }
            return response;
        }

        //This API will be called periodically to get nafath authentication status
        public static async Task<NafathLoginResponseModel> CheckNafathAuthentication(NafathLoginResponse model)
        {
            NafathLoginResponseModel response = null;
            if (NetworkCheck.IsInternet())
            {
                //string lang = WebServiceManager.GetLangZParameterAREN();
                String url = ZATCAConstants.NafathAuthenticationVerification;
                var uri = new Uri(url);
                HttpClient client = new HttpClient(App.httpClientHandler);

                //client.DefaultRequestHeaders.Add("Token", "123");
                //client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                //client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);

                var serilized = JsonConvert.SerializeObject(model);
                HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                var apiResponse = await client.PostAsync(uri, contentPost);
                var result = await apiResponse.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<NafathLoginResponseModel>(result);
            }
            return response;
        }
        public static async Task<NafathChangeMobileNumberModelResponse> NafathChangeMobileNumber(NafathChangeMobileNumberModel model)
        {
            NafathChangeMobileNumberModelResponse response = null;

            try
            {
                if (NetworkCheck.IsInternet())
                {
                    //string lang = WebServiceManager.GetLangZParameterAREN();
                    String url = ZATCAConstants.NafathChangeMobileNumber;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    //client.DefaultRequestHeaders.Add("Token", "123");
                    //client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    //client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    //client.DefaultRequestHeaders.Add("Accept", "application/json");

                    string LangZ = WebServiceManager.GetLangZParameterAREN();
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;

                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", LangZ);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    //client.DefaultRequestHeaders.Add("Authorization", App.Token);

                    var serilized = JsonConvert.SerializeObject(model);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    var apiResponse = await client.PostAsync(uri, contentPost);
                    var result = await apiResponse.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject<NafathChangeMobileNumberModelResponse>(result);
                }
            }
            catch (Exception ex)
            {
                
            }
            return response;
        }

        public static async Task<NafathChangeMobileNumberSendOTPResponse> NafathChangeMobileNumberSendOTP(NafathChangeMobileNumberSendOTPModel model)
        {
            NafathChangeMobileNumberSendOTPResponse response = null;
            if (NetworkCheck.IsInternet())
            {
                //string lang = WebServiceManager.GetLangZParameterAREN();
                String url = ZATCAConstants.NafathChangeMobileNumberSendOTP;
                var uri = new Uri(url);
                HttpClient client = new HttpClient(App.httpClientHandler);

                var lang = UtilityManager.GetLanguageParameter();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                client.DefaultRequestHeaders.Add("X-Device-Id", "android-20013fbc500");
                client.DefaultRequestHeaders.Add("X-Device-Name", "Samsung-s20+");
                client.DefaultRequestHeaders.Add("X-Device-Platform", "android");
                //client.DefaultRequestHeaders.Add("Authorization", App.Token);

                var serilized = JsonConvert.SerializeObject(model);
                HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                var apiResponse = await client.PostAsync(uri, contentPost);
                var result = await apiResponse.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<NafathChangeMobileNumberSendOTPResponse>(result);
            }
            return response;
        }

        public static async Task<NafathChangeMobileNumberCheckOTPModelResponse> NafathChangeMobileNumberCheckOTP(NafathChangeMobileNumberCheckOTPModel model)
        {
            NafathChangeMobileNumberCheckOTPModelResponse response = null;
            if (NetworkCheck.IsInternet())
            {
                //string lang = WebServiceManager.GetLangZParameterAREN();
                String url = ZATCAConstants.NafathChangeMobileNumberCheckOTP;
                var uri = new Uri(url);
                var lang = UtilityManager.GetLanguageParameter();

                HttpClient client = new HttpClient(App.httpClientHandler);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                client.DefaultRequestHeaders.Add("X-Device-Id", "android-20013fbc500");
                client.DefaultRequestHeaders.Add("X-Device-Name", "Samsung-s20+");
                client.DefaultRequestHeaders.Add("X-Device-Platform", "android");
                //client.DefaultRequestHeaders.Add("Authorization", App.Token);

                var serilized = JsonConvert.SerializeObject(model);
                HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                var apiResponse = await client.PostAsync(uri, contentPost);
                var result = await apiResponse.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<NafathChangeMobileNumberCheckOTPModelResponse>(result);
                if (!string.IsNullOrEmpty(result) && response.d == null)
                {
                    ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(result);
                    if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                    {
                        string errorMessage = string.Empty;
                        errorMessage = errorMesg.error.innererror.errordetails[0].message;
                        errorMessage += errorMesg.error.innererror.errordetails[1].message;
                        String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                        errorMessage = WithReplacedString;
                        throw new GAZTErrorException(errorMessage);
                    }
                }
            }
            return response;
        }

        public static async Task<SSOUserAccountModelResponse> NafathSSOUserAccountsInquiry(string idNumber, string GUID, string idType)
        {
            SSOUserAccountModelResponse response = null;
            if (NetworkCheck.IsInternet())
            {
                string url = ZATCAConstants.NafathSSOUserAccountsInquiry;
                url += "?sourceSystemId=ERAD&idNumber=" + idNumber + "&flag=ZM&GUID=" + GUID + "&idType=" + idType;
                var uri = new Uri(url);
                HttpClient client = new HttpClient(App.httpClientHandler);

                string LangZ = WebServiceManager.GetLangZParameterAREN();
                string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;

                client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("X-Session-Language", LangZ);
                client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);

                var apiResponse = await client.GetAsync(uri);
                var result = await apiResponse.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<SSOUserAccountModelResponse>(result);
                if (!string.IsNullOrEmpty(result) && response.data == null)
                {
                    ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(result);
                    if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                    {
                        string errorMessage = string.Empty;
                        errorMessage = errorMesg.error.innererror.errordetails[0].message;
                        errorMessage += errorMesg.error.innererror.errordetails[1].message;
                        String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                        errorMessage = WithReplacedString;
                        throw new GAZTErrorException(errorMessage);
                    }
                }
            }
            return response;

        }

        public static async Task<TaxpayerLoginModelResponse> NafathLogin(NafathLoginModel model)
        {
            TaxpayerLoginModelResponse response = null;
            if (NetworkCheck.IsInternet())
            {
                string url = ZATCAConstants.NafathLogin;
                var uri = new Uri(url);
                HttpClient client = new HttpClient(App.httpClientHandler);

                string LangZ = WebServiceManager.GetLangZParameterAREN();
                string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;

                client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("X-Session-Language", LangZ);
                client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                client.DefaultRequestHeaders.Add("X-Device-Name", "Samsung");
                client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);

                var serilized = JsonConvert.SerializeObject(model);
                HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                var apiResponse = await client.PostAsync(uri, contentPost);
                var result = await apiResponse.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<TaxpayerLoginModelResponse>(result);
            }
            return response;
        }

        public static async Task<Tuple<HttpResponseMessage, string>> GetIDTypesForChangeMobNumber(string Nafathguid)
        {

            HttpResponseMessage response = null;
            string result = string.Empty;
            if (NetworkCheck.IsInternet())
            {

                try
                {
                    string LangZAREN = WebServiceManager.GetLangZParameterAREN();
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;

                    String url = ZATCAConstants.ChangeMobNumberGetIDTypes + "?sourceApplication=01&returnId=" + "" + "&nafathGUID=" + Nafathguid;
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", LangZAREN);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);


                    response = await client.GetAsync(new Uri(url));

                    result = response.Content.ReadAsStringAsync().Result.ToString();

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
            return new Tuple<HttpResponseMessage, string>(response, result);
        }

        public static async Task<Tuple<HttpResponseMessage, string>> SaveChangeMobileNumberAsync(ChangeMobileNumberModel modelDetails)
        {
            HttpResponseMessage response = null;
            string result = string.Empty;
            if (NetworkCheck.IsInternet())
            {
                try
                {

                    String url = ZATCAConstants.SaveChangeMobNumber;
                    var uri = new Uri(url);
                    var serilized = JsonConvert.SerializeObject(modelDetails.d);
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var lang = UtilityManager.GetLanguageParameter();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", "android-20013fbc500");
                    client.DefaultRequestHeaders.Add("X-Device-Name", "Samsung-s20+");
                    client.DefaultRequestHeaders.Add("X-Device-Platform", "android");

                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    response = client.PostAsync(uri, contentPost).Result;
                    result = response.Content.ReadAsStringAsync().Result;


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
            return new Tuple<HttpResponseMessage, string>(response, result);
        }

        public static string ChangeMobDeleteAttachment(string fileName, string RetGuid, string docType, string docguid)
        {
            if (NetworkCheck.IsInternet())
            {
                string DeleteToken = string.Empty;
                try
                {
                    

                    var attachment = new Dictionary<string, object>
            {
                { "attachment", "New" },
                { "returnGUID", RetGuid },
                { "documentCategory", docType },
                { "serialNumber", "1" },
                { "documentId", docguid },
                { "attachedByPerson", "X" },
            };

                    

                    var uri = new Uri(ZATCAConstants.ChangeMobPostAttachment + "/deletion");


                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var lang = UtilityManager.GetLanguageParameter();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", "android-20013fbc500");
                    client.DefaultRequestHeaders.Add("X-Device-Name", "Samsung-s20+");
                    client.DefaultRequestHeaders.Add("X-Device-Platform", "android");
                    var serilized = JsonConvert.SerializeObject(attachment);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage response = client.PostAsync(uri, contentPost).Result;
                    var responsestr = response.Content.ReadAsStringAsync().Result;
                    responsestr = JObject.Parse(responsestr)["result"].ToString();
                    if (response != null)
                    {
                        HttpHeaders headers = response.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("delete", out values))
                        {
                            DeleteToken = values.First();
                        }
                        return "delete";
                    }
                    return "";
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

        public static async Task<ZATCAMAUI.Models.Attachment> ChangeMobileNumberAttachment(byte[] AttachmentByte, string fileName, string RetGuid, string Doctype, string contentType) //RG16 for Residency, RG19 for passport RG01 for CR copy RG02 licence copy
        {
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    var uri = new Uri(ZATCAConstants.ChangeMobPostAttachment + "?returnGUID=" + RetGuid + "&documentCategory=" + Doctype + "&attachmentFlag=New&serialNumber=1&attachedByPerson=TP&fileName=" + fileName);

                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var lang = UtilityManager.GetLanguageParameter();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", "android-20013fbc500");
                    client.DefaultRequestHeaders.Add("X-Device-Name", "Samsung-s20+");
                    client.DefaultRequestHeaders.Add("X-Device-Platform", "android");


                    var content = new MultipartFormDataContent();
                    var fileContent = new ByteArrayContent(AttachmentByte);
                    fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = "attachmentFile",
                        FileName = fileName
                    };
                    content.Add(fileContent, "attachmentFile", fileName);

                    var response = await client.PostAsync(uri, content);
                    var responsestr = response.Content.ReadAsStringAsync().Result;
                    responsestr = JObject.Parse(responsestr)["result"].ToString();
                    ZATCAMAUI.Models.Attachment _attachment = JsonConvert.DeserializeObject<ZATCAMAUI.Models.Attachment>(responsestr);
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
        public static async Task<OTPModelD> GetValidateAbsher(OTPModelD readCaptcha, bool Absher)
        {

            if (NetworkCheck.IsInternet())
            {
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                    OTPModelD forgotPasswordCaptcha = new OTPModelD();
                    string url = "";
                    if (Absher == true)
                    {
                        url = ZATCAConstants.GetAbsherPassword;
                    }
                    else
                    {
                        url = ZATCAConstants.ValidateAbsher;
                    }
                    var uri = new Uri(url);

                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);

                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("Token", "123");

                    var serilized = JsonConvert.SerializeObject(readCaptcha);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    var detailJson = res.Content.ReadAsStringAsync().Result;
                    forgotPasswordCaptcha = JsonConvert.DeserializeObject<OTPModelD>(detailJson);
                    if (!string.IsNullOrEmpty(detailJson) && forgotPasswordCaptcha.d == null)
                    {
                        ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
                        if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                        {
                            string errorMessage = string.Empty;
                            errorMessage = errorMesg.error.innererror.errordetails[0].message;
                            errorMessage += errorMesg.error.innererror.errordetails[1].message;
                            String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                            errorMessage = WithReplacedString;
                            await MopupService.Instance.PushAsync(new AttachmentInformationPopUp(errorMessage));
                            return null;
                        }
                    }
                    return forgotPasswordCaptcha;
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
        
        public static async Task<ImageCaptchaModel> GetCaptchaImage(string ReqCode, string LgId = null)
        {
            if (NetworkCheck.IsInternet())
            {
                string requestUrl = string.Empty;
                if (LgId == null)
                {
                    requestUrl = ZATCAConstants.GAZTGetCaptchaImage + ReqCode;
                }
                else
                {
                    requestUrl = ZATCAConstants.GAZTGetCaptchaImage + ReqCode + "&GUID=" + LgId;
                }
                var uri = new Uri(requestUrl);
                HttpClient client = new HttpClient(App.httpClientHandler);

                string LangZ = WebServiceManager.GetLangZParameterAREN();
                string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;

                client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("X-Session-Language", LangZ);
                client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);

                var apiResponse = await client.GetAsync(uri);
                var result = await apiResponse.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<ImageCaptchaModel>(result);
                return response;
            }
            return null;
        }

        public static string PrepareErrorMessageByJson(string ErrorResposnse)
        {
            SignupErrorModelRootObject errorMesg = JsonConvert.DeserializeObject<SignupErrorModelRootObject>(ErrorResposnse);
            StringBuilder Message = new StringBuilder();
            foreach (ErrorDetail itemerror in errorMesg.header.moreInformation.errorDetails)
            {
                Message.Append(itemerror.message);
            }
            return Message.ToString().Replace("An exception was raised", string.Empty);
        }
        
    }
}
