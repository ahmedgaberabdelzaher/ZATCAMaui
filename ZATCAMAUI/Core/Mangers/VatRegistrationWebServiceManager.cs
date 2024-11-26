using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.UnlockAccount;
using static ZATCAMAUI.Models.ErrorMessage;
namespace ZATCAMAUI.Core.Mangers
{

    public static class VatRegistrationWebServiceManager
    {
       

        public async static Task<VatCommencementDateFormat> GAZTGetVATEligibilityDate(string vatEligibleStartDate, string txntpz)
        {

            if (NetworkCheck.IsInternet())
            {
                VatCommencementDateFormat vATcommencementDateResponse = new VatCommencementDateFormat();
                string NewToken = string.Empty;
                try
                {
                    string langz = UtilityManager.GetLanguageParameter();
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    String url = ZATCAConstants.GetVatEligilibilityDate + App.LoginDataRetrieved.TIN + "&VATTaxableDate=" + vatEligibleStartDate + "&transactionType=" + txntpz;
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", langz);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTVATRegistrationDataOtherResponse = await client.GetAsync(uri);
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
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        string VatRegistrationOtherData = GAZTVATRegistrationDataOtherResponse.Content.ReadAsStringAsync().Result;
                        vATcommencementDateResponse = JsonConvert.DeserializeObject<VatCommencementDateFormat>(VatRegistrationOtherData);


                        if (!string.IsNullOrEmpty(VatRegistrationOtherData) && vATcommencementDateResponse?.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(VatRegistrationOtherData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                WebServiceManager.ErrorMessageForVAT = errorMesg.error.innererror.errordetails[0].message;
                                WebServiceManager.ErrorMessageForVAT += errorMesg.error.innererror.errordetails[1].message;
                                string WithReplacedString = WebServiceManager.ErrorMessageForVAT.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //throw new Exception(errorMessage);
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }

                        if (vATcommencementDateResponse?.d?.ErrorFg == "X")
                        {
                            Console.WriteLine(AppResources.VATEligibleDateError1);
                        }

                    }
                    return vATcommencementDateResponse;
                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
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

        public async static Task<VATRegistrationDetails> GAZTGetVATRegistrationData()
        {
            if (NetworkCheck.IsInternet())
            {
                VATRegistrationDetails vATRegistrationDetails = new VATRegistrationDetails();
                string NewToken = string.Empty;
                try
                {
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    var lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient();
                    String url = ZATCAConstants.GAZTGetVATRegistrationData + App.LoginDataRetrieved.TIN + "&language=" + lang + "&transactionType=" + "04";
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", App.IncomingChannel);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTVATRegistrationDataResponse = await client.GetAsync(uri);
                    if (GAZTVATRegistrationDataResponse != null)
                    {
                        if (GAZTVATRegistrationDataResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTVATRegistrationDataResponse.Headers;
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
                        String VatRegistrationData = GAZTVATRegistrationDataResponse.Content.ReadAsStringAsync().Result;
                        vATRegistrationDetails = JsonConvert.DeserializeObject<VATRegistrationDetails>(VatRegistrationData);
                        if (!string.IsNullOrEmpty(VatRegistrationData) && vATRegistrationDetails.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(VatRegistrationData);
                            if (errorMesg != null && errorMesg.header != null && errorMesg.header.moreInformation != null && errorMesg.header.moreInformation.errorDetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.header.moreInformation.errorDetails[0].message;
                                errorMessage += errorMesg.header.moreInformation.errorDetails[1].message;
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return vATRegistrationDetails;
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
        public async static Task<VATRegistrationDetails> GAZTGetVATRegistrationData(string pageType)
        {
            if (NetworkCheck.IsInternet())
            {
                VATRegistrationDetails vATRegistrationDetails = new VATRegistrationDetails();
                string NewToken = string.Empty;
                try
                {
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    var lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient();
                    String url = ZATCAConstants.GAZTGetVATRegistrationData + App.LoginDataRetrieved.TIN + "&language=" + lang + "&transactionType=" + pageType;
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", App.IncomingChannel);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTVATRegistrationDataResponse = await client.GetAsync(uri);
                    if (GAZTVATRegistrationDataResponse != null)
                    {
                        if (GAZTVATRegistrationDataResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTVATRegistrationDataResponse.Headers;
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
                        string VatRegistrationData = GAZTVATRegistrationDataResponse.Content.ReadAsStringAsync().Result;
                        vATRegistrationDetails = JsonConvert.DeserializeObject<VATRegistrationDetails>(VatRegistrationData);
                        if (!string.IsNullOrEmpty(VatRegistrationData) && vATRegistrationDetails.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(VatRegistrationData);
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
                    return vATRegistrationDetails;
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

        public async static Task<VATRegistrationDetails> GAZTGetVATRegistrationDisplayDetailsData()
        {
            if (NetworkCheck.IsInternet())
            {
                VATRegistrationDetails vATRegistrationDetails = new VATRegistrationDetails();
                string NewToken = string.Empty;
                try
                {
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    var lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient();
                    String url = ZATCAConstants.GAZTGetVATRegistrationData + App.LoginDataRetrieved.TIN + "&language=" + lang + "&transactionType=CRE_RGVT";
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTVATRegistrationDataResponse = await client.GetAsync(uri);
                    if (GAZTVATRegistrationDataResponse != null)
                    {
                        if (GAZTVATRegistrationDataResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTVATRegistrationDataResponse.Headers;
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
                        string VatRegistrationData = GAZTVATRegistrationDataResponse.Content.ReadAsStringAsync().Result;
                        vATRegistrationDetails = JsonConvert.DeserializeObject<VATRegistrationDetails>(VatRegistrationData);
                        if (!string.IsNullOrEmpty(VatRegistrationData) && vATRegistrationDetails.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(VatRegistrationData);
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
                    return vATRegistrationDetails;
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

        public async static Task<VATDeRegistrationDetails> GAZTGetVATDeRegistrationData()
        {
            if (NetworkCheck.IsInternet())
            {
                VATDeRegistrationDetails vATDeRegistrationDetails = new VATDeRegistrationDetails();
                string NewToken = string.Empty;
                try
                {
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    var lang = UtilityManager.GetLanguageParameter();
                    bool isReview = false;
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    String url = ZATCAConstants.GAZTGetVATDeRegistrationData + "&TIN=" + App.LoginDataRetrieved.TIN + "&language=" + lang;
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTVATDeRegistrationDataResponse = await client.GetAsync(uri);
                    if (GAZTVATDeRegistrationDataResponse != null)
                    {
                        if (GAZTVATDeRegistrationDataResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTVATDeRegistrationDataResponse.Headers;
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
                        string VatRegistrationData = GAZTVATDeRegistrationDataResponse.Content.ReadAsStringAsync().Result;
                        vATDeRegistrationDetails = JsonConvert.DeserializeObject<VATDeRegistrationDetails>(VatRegistrationData);
                        if (!string.IsNullOrEmpty(VatRegistrationData) && vATDeRegistrationDetails.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(VatRegistrationData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message + " ";
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                string WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return vATDeRegistrationDetails;
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


        public async static Task<VATRegistrationOtherDetails> GAZTGetVATRegistrationDataWithButtons(string Fbnumz, string Officerz, string Status, string TxnTp, string Formproc)
        {
            if (NetworkCheck.IsInternet())
            {
                VATRegistrationOtherDetails vATRegistrationOtherDetails = new VATRegistrationOtherDetails();
                string NewToken = string.Empty;
                try
                {
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    var lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    String url = ZATCAConstants.GAZTGetVATRegistrationOtherDetails + Fbnumz + "&language=" + lang + "&TIN=" + App.LoginDataRetrieved.TIN + "&statusCode=" + Status + "&transactionType=" + TxnTp + "&formProcess=" + "ZTAX_VT_REG";
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTVATRegistrationDataOtherResponse = await client.GetAsync(uri);
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
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        string VatRegistrationOtherData = GAZTVATRegistrationDataOtherResponse.Content.ReadAsStringAsync().Result;
                        vATRegistrationOtherDetails = JsonConvert.DeserializeObject<VATRegistrationOtherDetails>(VatRegistrationOtherData);

                        if (!string.IsNullOrEmpty(VatRegistrationOtherData) && vATRegistrationOtherDetails.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(VatRegistrationOtherData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                string WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                throw new Exception(errorMessage);
                            }
                        }

                    }
                    return vATRegistrationOtherDetails;
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
        public static async Task<vATRegistration> SaveVATRegistrationData(vATRegistration vatRegistration)
        {
            vATRegistration RequestVATRegistration = new vATRegistration();
            vATRegistration _vATRegistration = new vATRegistration();
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    if (vatRegistration != null)
                    {
                        if (vatRegistration != null)
                        {
                            RequestVATRegistration = vatRegistration;
                            if (vatRegistration.ELGBL_DOCSet == null || vatRegistration.ELGBL_DOCSet == null)
                            {
                                ELGBL_DOCSetforsubmit eLGBL_DOCSet = new ELGBL_DOCSetforsubmit();
                                eLGBL_DOCSet.results = new List<ResultsItemForDOCSetforsubmit>();
                                RequestVATRegistration.ELGBL_DOCSet = eLGBL_DOCSet.results;

                            }
                            ATTDETSet aTTACHSet = new ATTDETSet();
                            aTTACHSet.results = new List<Attachment>();
                            RequestVATRegistration.ATTDETSet = aTTACHSet.results;
                        }
                        string LangZAREN = WebServiceManager.GetLangZParameterAREN();

                        string lang = UtilityManager.GetLanguageParameter();
                        String url = ZATCAConstants.SaveVATRegistration;
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
                        client.DefaultRequestHeaders.Add("X-Device-Platform", App.IncomingChannel);
                        var serilized = JsonConvert.SerializeObject(vatRegistration);
                        HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                        HttpResponseMessage res = await client.PostAsync(url, contentPost);
                        var detailJson = res.Content.ReadAsStringAsync().Result;
                        var dataresponse = JsonConvert.DeserializeObject<VATRegistrationDetails>(detailJson);
                        //_vATRegistration = JsonConvert.DeserializeObject<vATRegistration>(detailJson);
                        VATRegistrationDetailsResponse response = JsonConvert.DeserializeObject<VATRegistrationDetailsResponse>(detailJson);
                        _vATRegistration = response.d;

                        if (_vATRegistration != null)
                        {
                            if (_vATRegistration != null)
                            {
                                if (_vATRegistration.NOTESSet == null)
                                {
                                    NOTESSet nOTEs = new NOTESSet();
                                    nOTEs.results = new List<Note>();
                                    _vATRegistration.NOTESSet = nOTEs.results;
                                }
                                if (_vATRegistration.IBANSet == null)
                                {
                                    IBANSet iBANSet = new IBANSet();
                                    iBANSet.results = new List<Result2>();
                                    _vATRegistration.IBANSet = iBANSet.results;
                                }
                                if (_vATRegistration.ADDRESSSet == null)
                                {
                                    ADDRESSSet aDDRESSSet = new ADDRESSSet();
                                    aDDRESSSet.results = new List<ResultsItem>();
                                    _vATRegistration.ADDRESSSet = aDDRESSSet.results;
                                }
                                if (_vATRegistration.ATTDETSet == null)
                                {
                                    ATTDETSet aTTDETSet = new ATTDETSet();
                                    aTTDETSet.results = new List<Attachment>();
                                    _vATRegistration.ATTDETSet = aTTDETSet.results;
                                }
                                if (_vATRegistration.CONTACTDTSet == null)
                                {
                                    CONTACTDTSet cONTACTDT = new CONTACTDTSet();
                                    cONTACTDT.results = new List<ResultsItemForContact>();
                                    _vATRegistration.CONTACTDTSet = cONTACTDT.results;
                                }
                                if (_vATRegistration.CONTACT_PERSONSet == null)
                                {
                                    CONTACT_PERSONSet cONTACT_PERSONSet = new CONTACT_PERSONSet();
                                    cONTACT_PERSONSet.results = new List<ResultsItemForContactPerson>();
                                    _vATRegistration.CONTACT_PERSONSet = cONTACT_PERSONSet.results;
                                }
                                if (_vATRegistration.ELGBL_DOCSet == null)
                                {
                                    ELGBL_DOCSetforsubmit eLGBL_DOC = new ELGBL_DOCSetforsubmit();
                                    eLGBL_DOC.results = new List<ResultsItemForDOCSetforsubmit>();
                                    _vATRegistration.ELGBL_DOCSet = eLGBL_DOC.results;
                                }
                                if (_vATRegistration.QUESTIONSSet == null)
                                {
                                    QUESTIONSSet qUESTIONSSet = new QUESTIONSSet();
                                    qUESTIONSSet.results = new List<ResultsItemForQuestion>();
                                    _vATRegistration.QUESTIONSSet = qUESTIONSSet.results;
                                }
                                if (_vATRegistration.QUESCONFIG_MSet == null)
                                {
                                    QUESCONFIG_MSet qUESCONFIG = new QUESCONFIG_MSet();
                                    qUESCONFIG.results = new List<QuestionsetWithMinMax>();
                                    _vATRegistration.QUESCONFIG_MSet = qUESCONFIG.results;
                                }
                                //if (_vATRegistration.d.QUESLISTSet == null)
                                //{
                                //    QUESLISTSet qUESLIST = new QUESLISTSet();
                                //    qUESLIST.results = new List<string>();
                                //    _vATRegistration.d.QUESLISTSet = qUESLIST.results;
                                //}




                            }
                        }
                        if (_vATRegistration == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
                            try
                            {
                                if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                                {
                                    WebServiceManager.ErrorMessageForVAT = errorMesg.error.innererror.errordetails[0].message;
                                    WebServiceManager.ErrorMessageForVAT += errorMesg.error.innererror.errordetails[1].message;
                                    String WithReplacedString = WebServiceManager.ErrorMessageForVAT.Replace("An exception was raised", string.Empty);
                                    WebServiceManager.ErrorMessageForVAT = WithReplacedString;
                                    //ErrorMessageForVAT
                                    throw new GAZTVATRegistrationInProcessException(WebServiceManager.ErrorMessageForVAT);
                                    return null;
                                }
                            }
                            catch (Exception ex)
                            {
                                
                                
                                return null;
                            }

                        }
                        return _vATRegistration;
                    }
                    return _vATRegistration;
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

        public static async Task<VATDeRegistrationDetails> SaveVATDeRegistrationData(VATDeRegistrationDetails vATDeRegistration)
        {
            VATDeRegistrationDetails RequestVATDeRegistration = new VATDeRegistrationDetails();
            VATDeRegistrationDetails _vATDeRegistration = new VATDeRegistrationDetails();
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    if (vATDeRegistration != null && vATDeRegistration.d != null)
                    {
                        if (vATDeRegistration.d != null)
                        {
                            RequestVATDeRegistration = vATDeRegistration;

                            List<Attachment> aTTACHSet = new List<Attachment>();
                            RequestVATDeRegistration.d.AttdetSet = aTTACHSet;
                        }
                        string LangZAREN = WebServiceManager.GetLangZParameterAREN();

                        char LangZ = WebServiceManager.GetLangZParameter();
                        string lang = UtilityManager.GetLanguageParameter();
                        String url = ZATCAConstants.SaveVATDeRegistration;
                        vATDeRegistration.d.headerSet.Langx = lang;
                        var uri = new Uri(url);
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
                        var serilized = JsonConvert.SerializeObject(RequestVATDeRegistration.d.headerSet);
                        HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                        HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                        var detailJson = res.Content.ReadAsStringAsync().Result;
                        var response = JsonConvert.DeserializeObject<VATDeRegistrationDetails>(detailJson);
                        if (response.data != null)
                        {
                            _vATDeRegistration.d = new vATDeRegistration();
                            _vATDeRegistration.d.headerSet = response.data;
                            _vATDeRegistration.d.AddressSet = response.data.AddressSet;
                            _vATDeRegistration.d.NotesSet = response.data.NotesSet;
                            _vATDeRegistration.d.AttdetSet = response.data.AttdetSet;
                            _vATDeRegistration.d.QuesListSet = response.data.QuesListSet;
                            if (_vATDeRegistration != null)
                            {
                                if (_vATDeRegistration.d != null)
                                {
                                    if (_vATDeRegistration.d.NotesSet == null)
                                    {
                                        List<VATDeregNote> nOTEs = new List<VATDeregNote>();
                                        _vATDeRegistration.d.NotesSet = nOTEs;
                                    }


                                    if (_vATDeRegistration.d.AttdetSet == null)
                                    {
                                        List<Attachment> aTTDETSet = new List<Attachment>();
                                        _vATDeRegistration.d.AttdetSet = aTTDETSet;
                                    }

                                    if (_vATDeRegistration.d.AddressSet == null)
                                    {
                                        List<ResultsItemSet> addressSet = new List<ResultsItemSet>();
                                        _vATDeRegistration.d.AddressSet = addressSet;
                                    }

                                    if (_vATDeRegistration.d.QuesListSet == null)
                                    {
                                        List<string> quesList = new List<string>();
                                        _vATDeRegistration.d.QuesListSet = quesList;
                                    }

                                }
                            }
                        }
                        else
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
                        return _vATDeRegistration;
                    }
                    return _vATDeRegistration;
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

        public static async Task<UnlockAccountResponseModel> GaztUnlockAccount(UnlockAccountModel unlockAccountModel)
        {
            UnlockAccountResponseModel _unlockResponseModel = new UnlockAccountResponseModel();

            if (NetworkCheck.IsInternet())
            {
                try
                {
                    if (unlockAccountModel != null)
                    {
                        string LangZAREN = WebServiceManager.GetLangZParameterAREN();

                        char LangZ = WebServiceManager.GetLangZParameter();
                        string lang = UtilityManager.GetLanguageParameter();
                        string url = ZATCAConstants.GAZTUnlockAccountAllOperations;

                        unlockAccountModel.Language = LangZ.ToString();
                        unlockAccountModel.UserLocked = "L";

                        var uri = new Uri(url);
                        HttpClient client = new HttpClient(App.httpClientHandler);

                        client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                        client.DefaultRequestHeaders.Add("Accept", "application/json");

                        var serilized = JsonConvert.SerializeObject(unlockAccountModel);
                        HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                        HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                        var detailJson = res.Content.ReadAsStringAsync().Result;
                        _unlockResponseModel = JsonConvert.DeserializeObject<UnlockAccountResponseModel>(detailJson);

                        if (_unlockResponseModel == null || _unlockResponseModel.D == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                WebServiceManager.ErrorMessageForUnlockAccount = errorMesg.error.innererror.errordetails[0].message;
                                WebServiceManager.ErrorMessageForUnlockAccount += errorMesg.error.innererror.errordetails[1].message;
                                string WithReplacedString = WebServiceManager.ErrorMessageForUnlockAccount.Replace("An exception was raised", string.Empty);
                                WebServiceManager.ErrorMessageForUnlockAccount = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTUnlockAccountException(WebServiceManager.ErrorMessageForUnlockAccount);
                            }
                        }

                        return _unlockResponseModel;
                    }
                    return _unlockResponseModel;
                }
                catch (GAZTUnlockAccountException ex)
                {
                    throw new GAZTUnlockAccountException(ex.Message);
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

        public static async Task<UnlockAccountResponseModel> GaztUnlockAccountOtp(UnlockAccountModelOtp unlockAccountModel)
        {
            UnlockAccountResponseModel _unlockResponseModel = new UnlockAccountResponseModel();

            if (NetworkCheck.IsInternet())
            {
                try
                {
                    if (unlockAccountModel != null)
                    {
                        //string url = ZATCAConstants.GAZTSignUpFirstSubmit;
                        string LangZAREN = WebServiceManager.GetLangZParameterAREN();

                        char LangZ = WebServiceManager.GetLangZParameter();
                        string lang = UtilityManager.GetLanguageParameter();
                        string url = ZATCAConstants.GAZTUnlockAccountAllOperations;

                        unlockAccountModel.Language = LangZ.ToString();
                        unlockAccountModel.UserLocked = "L";

                        var uri = new Uri(url);
                        HttpClient client = new HttpClient(App.httpClientHandler);

                        client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                        client.DefaultRequestHeaders.Add("Accept", "application/json");

                        var serilized = JsonConvert.SerializeObject(unlockAccountModel);
                        HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                        HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                        var detailJson = res.Content.ReadAsStringAsync().Result;
                        _unlockResponseModel = JsonConvert.DeserializeObject<UnlockAccountResponseModel>(detailJson);

                        if (_unlockResponseModel == null || _unlockResponseModel.D == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                WebServiceManager.ErrorMessageForUnlockAccount = errorMesg.error.innererror.errordetails[0].message;
                                WebServiceManager.ErrorMessageForUnlockAccount += errorMesg.error.innererror.errordetails[1].message;
                                string WithReplacedString = WebServiceManager.ErrorMessageForUnlockAccount.Replace("An exception was raised", string.Empty);
                                WebServiceManager.ErrorMessageForUnlockAccount = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTUnlockAccountException(WebServiceManager.ErrorMessageForUnlockAccount);
                            }
                        }

                        return _unlockResponseModel;
                    }
                    return _unlockResponseModel;
                }
                catch (GAZTUnlockAccountException ex)
                {
                    throw new GAZTUnlockAccountException(ex.Message);
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

        public static async Task<UnlockAccountResponseModel> GaztUnlockAccountChangePassword(UnlockAccountModelChangePassword unlockAccountModel)
        {
            UnlockAccountResponseModel _unlockResponseModel = new UnlockAccountResponseModel();

            if (NetworkCheck.IsInternet())
            {
                try
                {
                    if (unlockAccountModel != null)
                    {
                        //string url = ZATCAConstants.GAZTSignUpFirstSubmit;
                        string LangZAREN = WebServiceManager.GetLangZParameterAREN();

                        char LangZ = WebServiceManager.GetLangZParameter();
                        string lang = UtilityManager.GetLanguageParameter();
                        string url = ZATCAConstants.GAZTUnlockAccountAllOperations;

                        unlockAccountModel.Language = LangZ.ToString();
                        unlockAccountModel.UserLocked = "L";

                        var uri = new Uri(url);
                        HttpClient client = new HttpClient(App.httpClientHandler);

                        client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                        client.DefaultRequestHeaders.Add("Accept", "application/json");

                        var serilized = JsonConvert.SerializeObject(unlockAccountModel);
                        HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                        HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                        var detailJson = res.Content.ReadAsStringAsync().Result;
                        _unlockResponseModel = JsonConvert.DeserializeObject<UnlockAccountResponseModel>(detailJson);

                        if (_unlockResponseModel == null || _unlockResponseModel.D == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                WebServiceManager.ErrorMessageForUnlockAccount = errorMesg.error.innererror.errordetails[0].message;
                                WebServiceManager.ErrorMessageForUnlockAccount += errorMesg.error.innererror.errordetails[1].message;
                                string WithReplacedString = WebServiceManager.ErrorMessageForUnlockAccount.Replace("An exception was raised", string.Empty);
                                WebServiceManager.ErrorMessageForUnlockAccount = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTUnlockAccountException(WebServiceManager.ErrorMessageForUnlockAccount);
                            }
                        }

                        return _unlockResponseModel;
                    }
                    return _unlockResponseModel;
                }
                catch (GAZTUnlockAccountException ex)
                {
                    throw new GAZTUnlockAccountException(ex.Message);
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

        public static async Task<UnlockaccountOTPResponse> SendOTP(UnlockaccountOTP unlockaccountOTP)
        {
            if (NetworkCheck.IsInternet())
            {
                try
                {

                    HttpClient client = new HttpClient();
                    string lang = UtilityManager.GetLanguageParameter();
                    string url = ZATCAConstants.GAZTUnlockAccountAllOperations;
                    // string url = "https://test-api.zatca.gov.sa/test/third-party/v1/taxpayers/accounts/unlocking";
                    // var uri = new Uri(url);

                    //  HttpClient client = new HttpClient(crmSignUphttpClientHandler);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", UtilityManager.GetLanguageParameter());
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);

                    var serilized = JsonConvert.SerializeObject(unlockaccountOTP);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(url, contentPost);
                    var detailJson = res.Content.ReadAsStringAsync().Result;
                    var dataresponse = JsonConvert.DeserializeObject<UnlockaccountOTPResponse>(detailJson);
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
                            throw new GAZTUnlockAccountException(errorMessage);
                        }
                    }
                    return dataresponse;
                }
                catch (GAZTUnlockAccountException ex)
                {
                    throw new GAZTUnlockAccountException(ex.Message);
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

        public static async Task<UnlockAccountValidateRes> ValidateOTP(UnlockAccountValidate unlockaccountOTP)
        {
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string lang = UtilityManager.GetLanguageParameter();
                    string url = ZATCAConstants.GZATVALIDATEOTP;
                    // var uri = new Uri(url);

                    //  HttpClient client = new HttpClient(crmSignUphttpClientHandler);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);

                    var serilized = JsonConvert.SerializeObject(unlockaccountOTP);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(url, contentPost);
                    var detailJson = res.Content.ReadAsStringAsync().Result;
                    var dataresponse = JsonConvert.DeserializeObject<UnlockAccountValidateRes>(detailJson);
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
                            throw new GAZTUnlockAccountException(errorMessage);
                        }
                    }
                    return dataresponse;
                }
                catch (GAZTUnlockAccountException ex)
                {
                    throw new GAZTUnlockAccountException(ex.Message);
                }
                catch (Exception )
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

        public static async Task<UnlockAccountValidateRes> ChangePwd(UnlockAccountChangePwd unlockAccountChangePwd)
        {
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string lang = UtilityManager.GetLanguageParameter();
                    string url = ZATCAConstants.GAZTUNLOCKChangePWD;
                    // string url = "https://test-api.zatca.gov.sa/test/third-party/v1/taxpayers/accounts/unlocking/password-changing";
                    // var uri = new Uri(url);

                    //  HttpClient client = new HttpClient(crmSignUphttpClientHandler);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);

                    var serilized = JsonConvert.SerializeObject(unlockAccountChangePwd);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(url, contentPost);
                    var detailJson = res.Content.ReadAsStringAsync().Result;
                    var dataresponse = JsonConvert.DeserializeObject<UnlockAccountValidateRes>(detailJson);
                  

                    if (!string.IsNullOrEmpty(detailJson) && dataresponse?.result == null)
                    {
                        ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
                        if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                        {
                            string errorMessage = string.Empty;
                            errorMessage = errorMesg.error.innererror.errordetails[0].message;
                            errorMessage += errorMesg.error.innererror.errordetails[1].message;
                            string WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                            errorMessage = WithReplacedString;
                            throw new GAZTUnlockAccountException(errorMessage);
                        }
                    }
                    return dataresponse;
                }
                catch (GAZTUnlockAccountException ex)
                {
                    throw new GAZTUnlockAccountException(ex.Message);
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
        public async static Task<VATDeregDeclaration> GAZTGetVATDeRegistrationDeclaration(string fbNum)
        {
            if (NetworkCheck.IsInternet())
            {
                VATDeregDeclaration vATDeRegistrationData = new VATDeregDeclaration();
                string NewToken = string.Empty;
                try
                {
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = ZATCAConstants.GetVATDeRegistrationDeclaration + fbNum;

                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;

                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", UtilityManager.GetLanguageParameter());
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTVATDeRegistrationDataResponse = await client.GetAsync(uri);
                    if (GAZTVATDeRegistrationDataResponse != null)
                    {
                        String VatRegistrationData = GAZTVATDeRegistrationDataResponse.Content.ReadAsStringAsync().Result;
                        vATDeRegistrationData = JsonConvert.DeserializeObject<VATDeregDeclaration>(VatRegistrationData);
                        if (!string.IsNullOrEmpty(VatRegistrationData) && vATDeRegistrationData.D == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(VatRegistrationData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message + " ";
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return vATDeRegistrationData;
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
    }
}
