using EGAZT;
using EGAZT.Models;
using EGAZT.Models.EstablishmentRegistration;
using EGAZT.Models.Form5Models;
using EGAZT.Models.VATRefunds;
using EGAZT.Models.VATInstalationModels;
using EGAZT.Models.ZakatInstalationModels;
using EGAZT.ViewModel.NewDesignViewModel;
using GAZT.Helper;
using GAZT.Models;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using Xamarin.Forms;
using ZakatForm5Model;
using static GAZT.ErrorMessage;
using Metadata = EGAZT.Models.ZakatInstalationModels.Metadata;
using EGAZT.Models.VATInstalmentModels;
using NotesSet = EGAZT.Models.NotesSet;
using EGAZT.Models.ContractRelease;
using EGAZT.Models.ChageFillingPeriodModel;
using EGAZT.Models.ZakatInstalmentModels;
using static EGAZT.Models.VATInstalmentModels.RequestToVATInstallmentPlanDetails;
using RequestToVATInstallmentPlanDetails = EGAZT.Models.VATInstalmentModels.RequestToVATInstallmentPlanDetails;
using EGAZT.Models.VatReviewModel;
using static EGAZT.Models.VatReviewModel.VATObjectionFormModel;
using EGAZT.Models.ZakatObjectionsModel;
using EGAZT.Helper;
using EGAZT.Models.TPProfile;
using static EGAZT.Models.VatReviewModel.VATObjectionSummaryInputModel;
using Xamarin.Essentials;
using EGAZT.Models.AccountStatements;
using Formatting = Newtonsoft.Json.Formatting;
using Xamarin.Forms.Internals;
using EGAZT.Manager;
using EGAZT.Models.PaymentModel;

namespace GAZT.Manager
{
    [Preserve(AllMembers = true)]
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
            HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(Constants.GAZTSOAPWebRequestForAuthenticationService);
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
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    String url = Constants.BaseUrlOfODataServices;
                    HttpResponseMessage GAZTGetTINsResponse = await GetServiceManager.MakeGetAPICall(url,false,string.Empty);
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

        public static async Task<List<TIN>> GAZTGetAllTins(String Username)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                String GAZTGetTINsResponseResult = String.Empty;
                List<TIN> TINs = null;
                try
                {
                    String url = Constants.GetAllTin + Username + "'" + "&$format=json";

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
                        GAZTGetTINsResponseResult = JObject.Parse(GAZTGetTINsResponseResult)["d"].ToString();
                        string GAZTGetTINSResponseJSONJToken = JObject.Parse(GAZTGetTINsResponseResult)["results"].ToString();

                        TINs = JsonConvert.DeserializeObject<List<TIN>>(GAZTGetTINSResponseJSONJToken);
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
        public static ObservableCollection<MyBills> GAZTGetMyBills(String Tin, string lang)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                ObservableCollection<MyBills> myBills = new ObservableCollection<MyBills>();
                String MobileNumber = string.Empty;
                string PdfUrl = string.Empty;
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);


                    String url = Constants.GetMyBills + "Fbguid eq '" + "'and Euser eq '" + Tin + "'" + "&saml2=enabled&$format=json&sap-language=" + lang;
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTMyBillsResponse = client.GetAsync(uri).Result;
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
                            GAZTMyBillsResponseJSON = JObject.Parse(GAZTMyBillsResponseJSON)["d"].ToString();
                            string GAZTMyBillsResponseJSONJToken = JObject.Parse(GAZTMyBillsResponseJSON)["results"].ToString();
                            if (string.IsNullOrEmpty(GAZTMyBillsResponseJSONJToken) != true)
                            {
                                myBills = JsonConvert.DeserializeObject<ObservableCollection<MyBills>>(GAZTMyBillsResponseJSONJToken);
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
        public static ICR GAZTGetICRs(String Tin, string lang)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                ICR myICRs = new ICR();
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.GetMyICRs + lang + "',Gpart='',Euser='',Fbguid='" + App.LoginDataRetrieved.FbGuid + "',UserTin='" + "'" + ")?&saml2=enabled" + "&$expand=ICR_LISTSet,ICR_STATUSSet&sap-language=" + lang + "&$format=json";
                    client.DefaultRequestHeaders.Add("Token", "123");
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

        public static AllCertificate GAZTGetAllCertificate(String Lang, String Tin)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                DateTime dt = DateTime.Now;
                AllCertificate allCertificate = new AllCertificate();
                string currentDate = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString() + "T" + dt.Hour.ToString() + ":" + dt.Minute.ToString();
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string uri = Constants.GetAllCertificate + Tin + "'" + ",Langz='" + Lang + "'" + ",Begdaz=datetime'" + "2007-01-01T00%3A00%3A00'" + ",Enddaz=datetime'" + currentDate + "'" + ")?&$expand=ZakatSet,VATSet,ExciseSet&saml2=enabled&$format=json";
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
                            GAZTGetAllCertificateResponseJSON = JObject.Parse(GAZTGetAllCertificateResponseJSON)["d"].ToString();
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
        //public static async Task<ForgotPasswordOTP> GAZTFogotPasswordSendOTP(String Lang, String Tin)
        //{
        //    if (CrossConnectivity.Current.IsConnected)
        //    {
        //        DateTime dt = DateTime.Now;
        //        ForgotPasswordOTP forgotPasswordOTP = new ForgotPasswordOTP();
        //        char lang = GetLangZParameter();
        //        string NewToken = string.Empty;
        //        try
        //        {
        //            try
        //            {
        //                App.httpClientHandler.CookieContainer = null;
        //            }
        //            catch (Exception)
        //            {

        //            }
        //            string uri = Constants.FogotPasswordSendOTP + Tin + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "" + "'" + ",MobileNo='" + "" + "'" + ",SubType='" + "" + "'" + ",Idnumber='" + "" + "'" + ",Otp='" + "" + "'" + ",NewPwd='" + "" + "'" + ",RdBt='" + "P'" + ",Dob=datetime'" + "2015-07-05T15:13:49" + "'" + ",Langu='" + lang + "'" + ")?saml2=enabled&$format=json";
        //            HttpResponseMessage GAZTFogotPasswordSendOTPResponse = await GetServiceManager.MakeGetAPICall(uri, false, string.Empty);
        //            if (GAZTFogotPasswordSendOTPResponse != null)
        //            {
        //                HttpHeaders headers = GAZTFogotPasswordSendOTPResponse.Headers;
        //                String GAZTGetSendOTPResponseJSON = GAZTFogotPasswordSendOTPResponse.Content.ReadAsStringAsync().Result;
        //                forgotPasswordOTP = JsonConvert.DeserializeObject<ForgotPasswordOTP>(GAZTGetSendOTPResponseJSON);
        //            }
        //            return forgotPasswordOTP;
        //        }
        //        catch (Exception)
        //        {
        //            return null;
        //        }
        //    }
        //    else
        //    {
        //        throw new InternetException(AppResources.ZZInternetConnectionMessage);
        //    }
        //}

        public static async Task<ForgotPasswordOTP> GAZTFogotPasswordSendOTP(ForgotPasswordOTP forgotPasswordOTP)
        {
            //if (CrossConnectivity.Current.IsConnected)
            //{
            //    DateTime dt = DateTime.Now;
            //    ForgotPasswordOTP forgotPasswordOTP = new ForgotPasswordOTP();
            //    char lang = GetLangZParameter();
            //    string NewToken = string.Empty;
            //    try
            //    {
            //        try
            //        {
            //            App.httpClientHandler.CookieContainer = null;
            //        }
            //        catch (Exception)
            //        {

            //        }
            //        string uri = Constants.FogotPasswordSendOTP + Tin + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "" + "'" + ",MobileNo='" + "" + "'" + ",SubType='" + "" + "'" + ",Idnumber='" + "" + "'" + ",Otp='" + "" + "'" + ",NewPwd='" + "" + "'" + ",RdBt='" + "P'" + ",Dob=datetime'" + "2015-07-05T15:13:49" + "'" + ",Langu='" + lang + "'" + ")?saml2=enabled&$format=json";
            //        HttpResponseMessage GAZTFogotPasswordSendOTPResponse = await GetServiceManager.MakeGetAPICall(uri, false, string.Empty);
            //        if (GAZTFogotPasswordSendOTPResponse != null)
            //        {
            //            HttpHeaders headers = GAZTFogotPasswordSendOTPResponse.Headers;
            //            String GAZTGetSendOTPResponseJSON = GAZTFogotPasswordSendOTPResponse.Content.ReadAsStringAsync().Result;
            //            forgotPasswordOTP = JsonConvert.DeserializeObject<ForgotPasswordOTP>(GAZTGetSendOTPResponseJSON);
            //        }
            //        return forgotPasswordOTP;
            //    }
            //    catch (Exception)
            //    {
            //        return null;
            //    }
            //}
            //else
            //{
            //    throw new InternetException(AppResources.ZZInternetConnectionMessage);
            //}
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                    ForgotPasswordOTP forgotPasswordResponse = new ForgotPasswordOTP();
                    string url = Constants.SendUserNameToEmail;
                    var uri = new Uri(url);

                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);

                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("Token", "123");

                    var serilized = JsonConvert.SerializeObject(forgotPasswordOTP);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    var detailJson = res.Content.ReadAsStringAsync().Result;
                    forgotPasswordResponse = JsonConvert.DeserializeObject<ForgotPasswordOTP>(detailJson);
                    return forgotPasswordResponse;
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


        public static async Task<GenerateCaptchaGUID> GAZTCaptchaAndGUID(GenerateCaptchaGUID readCaptcha)
        {
            
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                    GenerateCaptchaGUID forgotPasswordCaptcha = new GenerateCaptchaGUID();
                    string url = Constants.CaptchaAndGUID;
                    var uri = new Uri(url);

                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);

                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("Token", "123");

                    var serilized = JsonConvert.SerializeObject(readCaptcha);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    var detailJson = res.Content.ReadAsStringAsync().Result;
                    forgotPasswordCaptcha = JsonConvert.DeserializeObject<GenerateCaptchaGUID>(detailJson);
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

        public static async Task<ForgotPasswordOTP> GAZTForgotPasswordValidateOTP(ForgotPasswordOTP ValidateOTP)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    ForgotPasswordOTP forgotPasswordOTP = new ForgotPasswordOTP();
                    string url = Constants.ValidateOTP;
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
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    var detailJson = res.Content.ReadAsStringAsync().Result;
                    forgotPasswordOTP = JsonConvert.DeserializeObject<ForgotPasswordOTP>(detailJson);
                    return forgotPasswordOTP;
                }
                catch (Exception )
                {
                    return null;
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }
        public static async Task<ForgotPasswordOTP> GAZTSendUserNameToEmail(ForgotPasswordOTP forgotUserOTP)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                    ForgotPasswordOTP forgotPasswordOTP = new ForgotPasswordOTP();
                    string url = Constants.SendUserNameToEmail;
                    var uri = new Uri(url);

                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);

                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("Token", "123");

                    var serilized = JsonConvert.SerializeObject(forgotUserOTP);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
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
        public static async Task<ForgotPasswordOTP> GAZTChangePassword(ForgotPasswordOTP forgotUserOTP)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    ForgotPasswordOTP forgotPasswordOTP = new ForgotPasswordOTP();
                    string url = Constants.ChangePassword;
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
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
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
        public static async Task<TINStatus> GAZTGetTinStatus(string lang, string Tin)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                TINStatus tINStatus = new TINStatus();
                string NewToken = string.Empty;
                try
                {
                    char _language = WebServiceManager.GetLangZParameter();
                    String url = Constants.GetTinStatus + _language + "',Tin='" + Tin + "" + "'" + ")?saml2=enabled&sap-language=’" + lang + "" + "'" + "&$expand=ItemSet&$format=json";
                    HttpResponseMessage GAZTTinStatus = await GetServiceManager.MakeGetAPICall(url,false,string.Empty); 
                    if (GAZTTinStatus != null)
                    {
                        if (GAZTTinStatus.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }

                        HttpHeaders headers = GAZTTinStatus.Headers;
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
                        String TINStatusResponse = GAZTTinStatus.Content.ReadAsStringAsync().Result;
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
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    VATLookUp vATLookUp = new VATLookUp();
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);

                    String url = Constants.GetVATLookUpDetails + lang + "'" + "&$filter=Idtype eq " + IdType + "  and Idnumber eq '" + IdNumber + "'&$format=json";
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTVATLookUp = await  client.GetAsync(uri);
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
                            if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")))
                            {
                                throw new GAZTSessionExpiredException();
                            }
                            App.Token = NewToken;
                        }
                        String TINStatusResponse = GAZTVATLookUp.Content.ReadAsStringAsync().Result;
                        vATLookUp = JsonConvert.DeserializeObject<VATLookUp>(TINStatusResponse);
                    }
                    return vATLookUp;
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
        public static async Task<VATDeclaration> GAZTGetVATReturns(string Fbguid, string Fbnumz, string EUser, string PeriodCode)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                string FbGuid = App.LoginDataRetrieved.FbGuid;
                try
                {
                    VATDeclaration _vATDeclaration = new VATDeclaration();
                    char LangZ = GetLangZParameter();
                    String Lang = UtilityManager.GetLanguageParameter();
                    String url = Constants.GAZTGetAllVATDeclarationReturnData + "" + "'" + ",Fbnumz='" + "" + "'" + ",Langz='" + Lang + "'" + ",Officerz='" + "" + "'" + ",Gpartz='" + App.TP.Tin + "'" + ",Euser='" + EUser + "'" + ",Fbguid='" + Fbguid + "'" + ")?saml2=enabled&sap-language=" + Lang + "&$expand=ADRSet,ATTACHSet,CFSet,IBANSet,NOTESSet,VATR_MSGSet,VATPERITEMSet&$format=json";
                    HttpResponseMessage GAZTVATReturnStatus = await GetServiceManager.MakeGetAPICall(url, true, "123");
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
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    if (vATDeclaration != null && vATDeclaration.d != null)
                    {
                        if (vATDeclaration.d != null)
                        {
                            RequestVATDeclaration = vATDeclaration;
                            if (string.IsNullOrEmpty(vATDeclaration.d.Fbnum))
                            {
                                RequestVATDeclaration.d.SubmitFg = "X";
                            }
                            else
                            {
                                RequestVATDeclaration.d.SubmitFg = string.Empty;
                            }
                            ATTACHSet aTTACHSet = new ATTACHSet();
                            aTTACHSet.results = new List<Attachment>();
                            RequestVATDeclaration.d.ATTACHSet = aTTACHSet;
                        }
                        char LangZ = GetLangZParameter();
                        string lang = UtilityManager.GetLanguageParameter();
                        String url = Constants.SaveVATDeclarationData;
                        vATDeclaration.d.Langz = lang;
                        var uri = new Uri(url);
                        HttpClient client = new HttpClient(App.httpClientHandler);

                        client.DefaultRequestHeaders.Add("Token", "123");
                        client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

                        client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                        client.DefaultRequestHeaders.Add("Accept", "application/json");

                        var serilized = JsonConvert.SerializeObject(RequestVATDeclaration);
                        HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                        HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                        var detailJson = res.Content.ReadAsStringAsync().Result;
                        _vATDeclarationD = JsonConvert.DeserializeObject<VATDeclaration>(detailJson);
                        if (_vATDeclarationD != null)
                        {
                            if (_vATDeclarationD.d != null)
                            {
                                if (_vATDeclarationD.d.NOTESSet == null)
                                {
                                    NOTESSet nOTEs = new NOTESSet();
                                    nOTEs.results = new List<Note>();
                                    _vATDeclarationD.d.NOTESSet = nOTEs;
                                }
                                if (_vATDeclarationD.d.IBANSet == null)
                                {
                                    IBANSet iBANSet = new IBANSet();
                                    iBANSet.results = new List<Result2>();
                                    _vATDeclarationD.d.IBANSet = iBANSet;
                                }
                                if (_vATDeclarationD.d.CFSet == null)
                                {
                                    CFSet cFSet = new CFSet();
                                    cFSet.results = new List<Result3>();
                                    _vATDeclarationD.d.CFSet = cFSet;
                                }
                                if (_vATDeclarationD.d.ATTACHSet == null)
                                {
                                    ATTACHSet aTTACHSet = new ATTACHSet();
                                    aTTACHSet.results = new List<Attachment>();
                                    _vATDeclarationD.d.ATTACHSet = aTTACHSet;
                                }
                                if (_vATDeclarationD.d.ADRSet == null)
                                {
                                    ADRSet aDRSet = new ADRSet();
                                    aDRSet.results = new List<Result5>();
                                    _vATDeclarationD.d.ADRSet = aDRSet;
                                }
                                if (_vATDeclarationD.d.VATR_MSGSet == null)
                                {
                                    VATRMSGSet vATRMSGSet = new VATRMSGSet();
                                    vATRMSGSet.results = new List<object>();
                                    _vATDeclarationD.d.VATR_MSGSet = vATRMSGSet;
                                }
                                if (_vATDeclarationD.d.VATPERITEMSet == null)
                                {
                                    VATPERITEMSet vATPERITEMSet = new VATPERITEMSet();
                                    vATPERITEMSet.results = new List<Result6>();
                                    _vATDeclarationD.d.VATPERITEMSet = vATPERITEMSet;
                                }
                            }
                        }
                        if (_vATDeclarationD == null || _vATDeclarationD.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                ErrorMessageForVAT = errorMesg.error.innererror.errordetails[0].message;
                                ErrorMessageForVAT += " "+errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = ErrorMessageForVAT.Replace("An exception was raised", string.Empty);
                                ErrorMessageForVAT = WithReplacedString;
                            }
                        }
                        return _vATDeclarationD;
                    }
                    return _vATDeclarationD;
                }
                catch (Exception)
                {
                    if (_vATDeclarationD != null && _vATDeclarationD.d == null)
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
        public static ObservableCollection<InternationalMobileData> GAZTGetMobileRegionDropdown()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                ObservableCollection<InternationalMobileData> internationalCodes = new ObservableCollection<InternationalMobileData>();
                string NewToken = string.Empty;
                try
                {
                    string lang = UtilityManager.GetLanguageParameter();

                    HttpClient client = new HttpClient(App.httpClientHandler);

                    string url = Constants.GAZTInternationalMobileData + " eq " + "'" + lang + "'" + "&$format=json";
                    client.DefaultRequestHeaders.Add("Token", "123");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTInternationalMobileNumDataResponse = client.GetAsync(uri).Result;
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
                            GAZTInternationalNumberResponseJSON = JObject.Parse(GAZTInternationalNumberResponseJSON)["d"].ToString();
                            string GAZTInternationalNumberResponseJSONJToken = JObject.Parse(GAZTInternationalNumberResponseJSON)["results"].ToString();
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
        public static async Task<List<IBANIDNumber>> GAZTGetIBANIdNumber(string IBANType)
        {
            List<IBANIDNumber> iBANIDNumbers = new List<IBANIDNumber>();
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    string lang = UtilityManager.GetLanguageParameter();
                   String url = Constants.GAZTGetIdNumber + App.TP.Tin + "'" + "and Type eq '" + IBANType + "'" + "&saml2=enabled&sap-langauge='" + lang + "'&$format=json";
                    HttpResponseMessage GAZTValidateOTPResponse = await GetServiceManager.MakeGetAPICall(url, false, string.Empty);
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
                            IBANIdNumber = JObject.Parse(IBANIdNumber)["d"].ToString();
                            IBANIdNumber = JObject.Parse(IBANIdNumber)["results"].ToString();
                            iBANIDNumbers = JsonConvert.DeserializeObject<List<IBANIDNumber>>(IBANIdNumber);
                        }
                    }
                    return iBANIDNumbers;
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
        public static string GAZTCheckIBAN(string IBAN)
        {
            List<IBANIDNumber> iBANIDNumbers = new List<IBANIDNumber>();
            String IbanNumber = string.Empty;
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    string lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                   String url = Constants.GAZTCheckIBANNumber + IBAN + "')" + "?saml2=enabled&sap-langauge=" + lang + "&$format=json";
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTValidateOTPResponse =  client.GetAsync(uri).Result;
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
                        try
                        {
                            if (!string.IsNullOrEmpty(IBANIdNumber))
                            {
                                IBANIdNumber = JObject.Parse(IBANIdNumber)["d"].ToString();
                                IBANIdNumber = JObject.Parse(IBANIdNumber)["Iban"].ToString();
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
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    char lang = GetLangZParameter();
                    String url = Constants.GAZTGetVATDeclarationCalculationDataUrl + "'" + FormBundleNumber + "'" + ",Lang='" + lang + "'" + ",Operation='" + "'" + ",Gpart='" + Gpart + "'" + ",Status='" + status + "'" + ",TxnTp='" + TxnTp + "'" + ",Formproc='" + "'" + ",Periodkey='" + periodKey + "'" + ")?saml2=enabled&$expand=IBANSet,IGRTSet,ITUDSet,UI_BTNSet,VATRSet,VTTHSet&$format=json";
                    HttpResponseMessage GAZTValidateOTPResponse = await GetServiceManager.MakeGetAPICall(url,false,string.Empty); 
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
        public static async Task<AttachmentRootOject> GAZTSaveVATDeclarationAttachmentForFD(byte[] AttachmentByte, string fileName, string RetGuid, string Dotyp, string contentType)//, string returnedFguid
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    AttachmentRootOject _attachment = new AttachmentRootOject();
                    char LangZ = GetLangZParameter();
                    string AttBy = "TP";
                    String url = Constants.GAZTSaveAttachment + "'" + "'" + ",RetGuid='" + RetGuid + "'" + ",Flag='" + "N" + "'" + ",Dotyp='" + Dotyp + "'" + ",SchGuid='" + "'" + ",Srno=" + "1" + ",Doguid='" + "'" + ",AttBy='" + AttBy + "'" + ")/AttachMedSet";
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("slug", WebUtility.UrlEncode(fileName));
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
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
        public static async Task<AttachmentRootOject> GAZTSaveVATDeclarationAttachment(byte[] AttachmentByte, string fileName, string RetGuid, string Dotyp, string contentType)//, string returnedFguid
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    AttachmentRootOject _attachment = new AttachmentRootOject();
                    char LangZ = GetLangZParameter();
                    string AttBy = "TP";
                    String url = Constants.GAZTSaveAttachment + "'" + "'" + ",RetGuid='" + RetGuid + "'" + ",Flag='" + "N" + "'" + ",Dotyp='" + Dotyp + "'" + ",SchGuid='" + "'" + ",Srno=" + "1" + ",Doguid='" + "'" + ",AttBy='" + AttBy + "'" + ")/AttachMedSet";
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("slug", fileName);
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

                    ByteArrayContent baContent = new ByteArrayContent(AttachmentByte);
                    if (!string.IsNullOrEmpty(contentType))
                        baContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                    var response = await client.PostAsync(url, baContent);
                    var responsestr = response.Content.ReadAsStringAsync().Result;
                    _attachment = JsonConvert.DeserializeObject<AttachmentRootOject>(responsestr);
                    return _attachment;
                }
                catch (Exception Ex)
                {
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
            if (CrossConnectivity.Current.IsConnected)
            {
                string DeleteToken = string.Empty;
                try
                {
                    AttachmentRootOject _attachment = new AttachmentRootOject();
                    char LangZ = GetLangZParameter();
                    string Dotyp = "VTA0";
                    string AttBy = "TP";
                   String url = Constants.GAZTDeteleAttachment + "'" + "'" + ",RetGuid='undefined'" + ",Flag='" + "N" + "'" + ",Dotyp='" + Dotyp + "'" + ",SchGuid='" + "'" + ",Srno=" + "1" + ",Doguid='" + RetGuid + "'" + ",AttBy='" + AttBy + "'" + ")/$value?saml2=enabled"; //",RetGuid='005056B1F8FB1EDA8FF041CFF05E83A9',Flag='N',Dotyp='VTA0',SchGuid='',Srno=1,Doguid='',AttBy='TP')/AttachMedSet";// Constants.SaveVATDeclarationData;
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
        public static async Task<SadadNumber> GAZTGetVATDeclarationSADADNumber(string FormBundleID)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    SadadNumber sadadNumber = new SadadNumber();
                    char LangZ = GetLangZParameter();
                    string lang = UtilityManager.GetLanguageParameter();
                    String url = Constants.GAZTGetSADADNumber + lang + "'" + "&$format=json&$filter=Langu eq'" + LangZ + "'and Fbnum eq '" + FormBundleID + "'" + "";
                    var response = await GetServiceManager.MakeGetAPICall(url,true, "123");
                    var responsestr = response.Content.ReadAsStringAsync().Result;
                    sadadNumber = JsonConvert.DeserializeObject<SadadNumber>(responsestr);
                    return sadadNumber;
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
        public static async Task<EstimatedZakatReturns> GAZTGetEstimateZakatReturnList()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                EstimatedZakatReturns zAKATICRList = new EstimatedZakatReturns();
                string NewToken = string.Empty;
                try
                {
                    string _language = UtilityManager.GetLanguageParameter();
                    String url = Constants.GAZTGetZakatReturnList + App.TP.Userid + "'" + ",Auditor='" + "'" + ",Lang='" + _language + "'" + ",UserTin='" + App.TP.Userid + "'" + ")?saml2=enabled&sap-language='" + _language + "'" + "&$expand=listSet&$format=json";
                    HttpResponseMessage GAZTEstimateZakatReturnList = await GetServiceManager.MakeGetAPICall(url,true, "123");
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
        public static async Task<VATDeclaration> GAZTSetVATReturnVoid(VATDeclaration vATDeclaration)
        {
            if (CrossConnectivity.Current.IsConnected)
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
            if (CrossConnectivity.Current.IsConnected)
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
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    char lang = GetLangZParameter();// "E";
                    String url = "";
                    if (App.IsZakatLoadingFromMyReturns == true)
                    {
                        url = Constants.GAZTGetZakatReturn + "'" + ",Langz='" + lang + "'" + ",Gpartz='" + App.TP.Tin + "'" + ",Euser='" + App.TP.Tin + "'" + ",Fbguid='" + fbguid + "'" + ",Invflg='" + "'" + ",Fsource='" + "TP" + "'" + ")?saml2=enabled&sap-language='" + lang + "'&$expand=ReasonSet,AttachSet,ThresholdSet,InvoiceSet&$format=json";
                    }
                    else
                    {
                        url = Constants.GAZTGetZakatReturn + "'" + ",Langz='" + lang + "'" + ",Gpartz='" + App.TP.Tin + "'" + ",Euser='" + "'" + ",Fbguid='" + fbguid + "'" + ",Invflg='" + "'" + ",Fsource='" + "TP" + "'" + ")?saml2=enabled&sap-language='" + lang + "'&$expand=ReasonSet,AttachSet,ThresholdSet,InvoiceSet&$format=json";
                    }
                    HttpResponseMessage GAZTValidateOTPResponse = await GetServiceManager.MakeGetAPICallWithIncomingChannel(url, true, "123");

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
                        catch (Exception)
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
                    return zakatReturnDetails;
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
        public static async Task<ZakatReturnDetails> GAZTSaveZakatReturnData(ZakatReturnDetails zakatReturnDetailsD, string OperationStatus)//, string returnedFguid
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    zakatReturnDetailsD.d.Operationz = OperationStatus;
                    zakatReturnDetailsD.d.UserTypz = "TP";
                    zakatReturnDetailsD.d.Langz = UtilityManager.GetLanguageParameter();
                    ZakatReturnDetails _zakatReturnDetailsD = new ZakatReturnDetails();
                    string LangZ = GetLangZParameterAREN();
                    String url = Constants.GAZTSaveEstimatedZaktReturn + LangZ;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var serilized = JsonConvert.SerializeObject(zakatReturnDetailsD);

                    client.DefaultRequestHeaders.Add("Token", App.Token);
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    var _zakatReturnDetailsDesponsestr = res.Content.ReadAsStringAsync().Result;
                    _zakatReturnDetailsD = JsonConvert.DeserializeObject<ZakatReturnDetails>(_zakatReturnDetailsDesponsestr);
                    if (_zakatReturnDetailsD == null || _zakatReturnDetailsD.d == null)
                    {
                        ErrorMessage = string.Empty;
                        ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatReturnDetailsDesponsestr);
                        if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                        {
                            ErrorMessage = errorMesg.error.innererror.errordetails[0].message;
                        }
                    }
                    return _zakatReturnDetailsD;
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
        public static async Task<List<ApplicableButton>> GAZTVATReturnGetApplicableButtons(string Fbnum, string Lang, string Operation, string Gpart, string Status, string TxnTp, string PeriodKey)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                List<ApplicableButton> VATApplicableButtons = new List<ApplicableButton>();
                try
                {
                    char LangZ = GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.GAZTVATReturnGetApplicableButtons + "'" + Fbnum + "'" + ",Lang='" + LangZ + "'" + ",Operation='" + Operation + "'," + "Gpart=" + "'" + Gpart + "',Status='" + Status + "',TxnTp='" + TxnTp + "',Formproc='',Periodkey='" + PeriodKey + "'" + ")?saml2=enabled&$expand=UI_BTNSet,IGRTSet&$format=json";

                    HttpResponseMessage ApplicableButtonsResponse = await GetServiceManager.MakeGetAPICall(url, false, string.Empty);
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
                            ApplicableButtonsResponseJSON = JObject.Parse(ApplicableButtonsResponseJSON)["d"].ToString();
                            ApplicableButtonsResponseJSON = JObject.Parse(ApplicableButtonsResponseJSON)["UI_BTNSet"].ToString();
                            ApplicableButtonsResponseJSON = JObject.Parse(ApplicableButtonsResponseJSON)["results"].ToString();
                            if (string.IsNullOrEmpty(ApplicableButtonsResponseJSON) != true)
                            {
                                VATApplicableButtons = JsonConvert.DeserializeObject<List<ApplicableButton>>(ApplicableButtonsResponseJSON);
                            }
                        }
                    }
                }
                catch (Exception)
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
        public static async Task<AttachmentRootOject> GAZTSaveEstimatedZAKATAttachment(byte[] AttachmentByte, string fileName, string RetGuid, string Dotyp, string ContentType)//, string returnedFguid
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    AttachmentRootOject _attachment = new AttachmentRootOject();
                    char LangZ = GetLangZParameter();
                    string url = Constants.GAZTSaveEstimatedZAKATAttachement + RetGuid + "',Flag='N',Dotyp='Z12L',SchGuid='',Srno=1,Doguid='',AttBy='TP',OutletRef='')/AttachMedSet?saml2=enabled";
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("slug", fileName);
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", ContentType);
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    ByteArrayContent baContent = new ByteArrayContent(AttachmentByte);
                    if (!string.IsNullOrEmpty(ContentType))
                        baContent.Headers.ContentType = new MediaTypeHeaderValue(ContentType);
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
        public static async Task<EstimatedZAKATReturnsSADADNumber> GAZTGetEstimatedZakatReturnSADADNumber(string FBNumber, string FBGuid)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                EstimatedZAKATReturnsSADADNumber _estimatedZAKATReturnsSADADNumber = new EstimatedZAKATReturnsSADADNumber();
                string NewToken = string.Empty;
                try
                {
                    string lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url;
                    if (App.IsZakatLoadingFromMyReturns == true)
                    {
                        url = Constants.GAZTGetEstimatedZAKATSADADNumber + FBNumber + "'" + ",Langz='" + lang + "'" + ",Gpartz='" + App.TP.Tin + "'" + ",Euser='" + App.TP.Tin + "'" + ",Fbguid='" + FBGuid + "'" + ",Invflg='',Fsource='TP')?saml2=enabled&$expand=InvoiceSet&$format=json";
                    }
                    else
                    {
                        url = Constants.GAZTGetEstimatedZAKATSADADNumber + FBNumber + "'" + ",Langz='" + lang + "'" + ",Gpartz='" + App.TP.Tin + "'" + ",Euser='" + "'" + ",Fbguid='" + FBGuid + "'" + ",Invflg='',Fsource='TP')?saml2=enabled&$expand=InvoiceSet&$format=json";
                    }
                    client.DefaultRequestHeaders.Add("Token", "123");
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTEstimateZakatReturnList = await GetServiceManager.MakeGetAPICall(url, true, "123");
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
        public static async Task<EstimatedZakatReturns> GAZTDowmloadEstimatedZakatReturnInvoice(string FBNumber, string FBGuid)
        {
            string NewToken = string.Empty;
            try
            {
                string lang = UtilityManager.GetLanguageParameter();
                HttpClient client = new HttpClient(App.httpClientHandler);
                String url = Constants.GAZTGetEstimatedZAKATSADADNumber + FBNumber + "'" + ",Langz='" + lang + "'" + ",Gpartz='" + "'" + ",Euser='00000000000000000000'" + ",Fbguid='" + FBGuid + "'" + ",Invflg='I',Fsource='TP')?saml2=enabled&$expand=InvoiceSet";
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
            if (CrossConnectivity.Current.IsConnected)
            {
                string DeleteToken = string.Empty;
                try
                {
                    AttachmentRootOject _attachment = new AttachmentRootOject();
                    char LangZ = GetLangZParameter();
                    string Dotyp = "VTA0";
                    string AttBy = "TP";
                    String url = Constants.GAZTDeteleAttachment + "'" + "'" + ",RetGuid='undefined'" + ",Flag='" + "N" + "'" + ",Dotyp='" + Dotyp + "'" + ",SchGuid='" + "'" + ",Srno=" + "1" + ",Doguid='" + DocumentID + "'" + ",AttBy='" + AttBy + "'" + ")/$value?saml2=enabled"; 
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient();

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
        public static async Task<string> GAZTEstimatedZAKATReturnInvoicePdf(string Cokey)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                string DeleteToken = string.Empty;
                try
                {
                    AttachmentRootOject _attachment = new AttachmentRootOject();
                    char LangZ = GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.GAZTGetEstimatedZAKATReturnInvoicePdf + Cokey + "',Cotyp='FZ01')/$value?saml2=enabled";
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTEstimateZakatReturnList = await  GetServiceManager.MakeGetAPICall(url, false, string.Empty);
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
        public static CorrespondenceRootObject GAZTGetZakatCorrespondece()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                CorrespondenceRootObject ZakatCorrespondenceList = new CorrespondenceRootObject();
                string NewToken = string.Empty;
                try
                {
                    string lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    DateTime DateTimeNow = DateTime.Now;
                    string CurrentTime = DateTimeNow.ToString("yyyy-MM-ddTHH:mm");
                    String url = Constants.GAZTGetCorrespondence + "'" + App.TP.Tin + "' and Langz eq '" + lang + "' and UserTin eq '' and Begdaz eq datetime'2007-01-01T00:00' and Enddaz eq datetime'" + CurrentTime + "' and ObligFlagz eq '' and Auditor eq ''";
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
        public static CorrespondenceRootObject GAZTGetVATCorrespondece()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                CorrespondenceRootObject VATCorrespondenceList = new CorrespondenceRootObject();
                string NewToken = string.Empty;
                try
                {
                    string lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    DateTime DateTimeNow = DateTime.Now;
                    string CurrentTime = DateTimeNow.ToString("yyyy-MM-ddTHH:mm");
                    String url = Constants.GAZTGetCorrespondence + "'" + App.TP.Tin + "' and Langz eq '" + lang + "' and Begdaz eq datetime'2007-01-01T00:00' and Enddaz eq datetime'" + CurrentTime + "' and ObligFlagz eq 'I' and Auditor eq 'null' and TaxtpFg eq 'VAT' and UserTin eq ''";
                    ////client.DefaultRequestHeaders.Add("Token", App.Token);
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
        public static CorrespondenceRootObject GAZTGetETCorrespondece()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                CorrespondenceRootObject ETReturnCorrespondenceList = new CorrespondenceRootObject();
                string NewToken = string.Empty;
                try
                {
                    string lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    DateTime DateTimeNow = DateTime.Now;
                    string CurrentTime = DateTimeNow.ToString("yyyy-MM-ddTHH:mm");
                    String url = Constants.GAZTGetCorrespondence + " '" + App.TP.Tin + "' and Langz eq '" + lang + "' and Begdaz eq datetime'2007-01-01T00:00' and Enddaz eq datetime'" + CurrentTime + "' and ObligFlagz eq 'I' and Auditor eq 'null' and TaxtpFg eq 'ET' and UserTin eq ''";
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
        public static CorrespondenceDetailsRootObject GAZTGetCorrespondeceDetails(CorrespondanceModel CorresModel)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                CorrespondenceDetailsRootObject CorrespondenceDetailsList = new CorrespondenceDetailsRootObject();
                string NewToken = string.Empty;
                try
                {
                    string lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    DateTime DateTimeNow = CorresModel.Txtco;
                    string CurrentTime = DateTimeNow.Year + "/" + DateTimeNow.Day + "/" + DateTimeNow.Month + " - " + DateTimeNow.Hour.ToString("D2") + ":" + DateTimeNow.Minute.ToString("D2") + ":" + DateTimeNow.Second.ToString("D2");
                    String url = Constants.GAZTGetCorrespondenceDetails + "'" + App.TP.Tin + "' and Cotyp eq '" + CorresModel.Cotype + "' and Fbnum eq '' and Cokey eq '" + CorresModel.Cokey + "' and Ltrno eq '" + CorresModel.RefNumber + "' and Txtdo eq '" + CurrentTime + "'and Langu eq '" + lang + "'";
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
        public static string GAZTSetFavCorrespondence(CorrespondenceFavoriteModel FavoriteCorrespondence)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    string url = Constants.GAZTSetFavCorrespondence;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("Token", "123");

                    var serilized = JsonConvert.SerializeObject(FavoriteCorrespondence);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    HttpResponseMessage res = client.PostAsync(uri, contentPost).Result;
                    var detailJson = res.Content.ReadAsStringAsync().Result;
                    return null;
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
        public static async Task<FormBundleModel> GAZTGetFormBundleModel()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                FormBundleModel ReturnFormBundleList = new FormBundleModel();
                string NewToken = string.Empty;
                try
                {
                    char lang = GetLangZParameter();
                    String url = Constants.GAZTGetFormBundleModel + " '" + lang + "' and Gpart eq '" + App.TP.Tin + "'";
                    HttpResponseMessage GAZTFormBundleList =  await GetServiceManager.MakeGetAPICall(url, false, string.Empty);
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
        public static async Task<FormBundleApplicationNumberModel> GAZTGetFormBundleApplicationNumberModel(string Fbtyp)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                FormBundleApplicationNumberModel ReturnFormBundleList = new FormBundleApplicationNumberModel();
                string NewToken = string.Empty; string ApplicationNumber = Fbtyp;
                try
                {
                    char lang = GetLangZParameter();
                    String url = Constants.GAZTGetFormBunleAccountNumberModel + "'" + lang + "' and Gpart eq '" + App.TP.Tin + "' and Fbtyp eq '" + ApplicationNumber + "'";
                    HttpResponseMessage GAZTFormBundleList =  await GetServiceManager.MakeGetAPICall(url, false, string.Empty);
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
        public static async Task<SignupCityRootObject> GAZTGetCityListForSignup()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                SignupCityRootObject SignupCityList = new SignupCityRootObject();
                string NewToken = string.Empty;
                try
                {
                    char lang = GetLangZParameter();

                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);
                    String url = Constants.GAZTGetCityListForSignUp + "dropdown_headerSet(Spras='" + lang + "',Land1='SA',Bland='',Cityc='')?&$expand=city_dropdownSet&saml2=enabled&$format=json";
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTSignupCityList = await client.GetAsync(uri);
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
        public static async Task<List<IssuedByResponse>> GAZTGetIssuedByList()
        {
            if (CrossConnectivity.Current.IsConnected)
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
                    String url = Constants.GAZTSiguupIssuedByList + "'[{\"Lang\":\"" + lang + "\",\"Portal_usr\":\"1\",\"Process\":\"Trans\",\"Procs_Type\":\"PUSR1\"}]'&sap-language=EN&saml2=enabled&$format=json";
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
                            Console.WriteLine("Unable to Find Other Value");
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
        public async static Task<string> GAZTValidateIDTypes(string IDType, string IDNumber, string DBO)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                IDTypeValidateRootObject SignupIsIDTypeValid = new IDTypeValidateRootObject();
                string IsIDTypeValidList = string.Empty;
                string NewToken = string.Empty;
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                    char lang = GetLangZParameter();
                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);
                    String url = Constants.GAZTSiguupValidateIDTypes + "(Tin='',Idtype='" + IDType + "',Idnum='" + IDNumber + "',Country='',PassExpDt='',TaxpDob='" + DBO + "')?sap-language=" + lang + "&$format=json&saml2=enabled";
                    var uri = new Uri(url);
                    HttpResponseMessage SignupIsIDTypeValidList = await client.GetAsync(uri);
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
        public static CRValidationModelRootObject GAZTValidateCRNumber(string CRNumber)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                CRValidationModelRootObject CRValidationModelValid = new CRValidationModelRootObject();
                string IsIDTypeValidList = string.Empty;
                string NewToken = string.Empty;
                try
                {
                    char lang = GetLangZParameter();
                    HttpClient client = new HttpClient();
                    String url = Constants.GAZTSiguupValidateCR + "(Crnum='" + CRNumber + "')?sap-language=" + lang + "&$format=json&saml2=enabled";
                    var uri = new Uri(url);
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
        public static DuplicateSignUpModelRootObject GAZTValidateDuplicate(string IDNum, string IDType, string Institude, string Country, string crNum)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                DuplicateSignUpModelRootObject ValidateDuplicate = new DuplicateSignUpModelRootObject();
                string IsIDTypeValidList = string.Empty;
                String url = string.Empty;
                string NewToken = string.Empty;
                try
                {
                    char lang = GetLangZParameter();
                    HttpClient client = new HttpClient();
                    if (!string.IsNullOrEmpty(crNum))
                    {
                        url = Constants.GAZTSiguupCheckDuplicate + "(Partner='',Type='" + IDType + "',Idnumber='" + IDNum + "',Institute='" + "90702" + "',Country='" + Country + "',City='',StartDt='')?$format=json&Saml2=enabled";
                    }
                    else
                    {
                        url = Constants.GAZTSiguupCheckDuplicate + "(Partner='',Type='" + IDType + "',Idnumber='" + IDNum + "',Institute='" + Institude + "',Country='" + Country + "',City='',StartDt='')?$format=json&Saml2=enabled";
                    }

                    var uri = new Uri(url);
                    HttpResponseMessage ValidateDuplicateList = client.GetAsync(uri).Result;
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
        public async static Task<string> GAZTSignUpFirstSubmitCGZTAcc(SignUpNextBodyModel SignUpModel)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    string LangZ = GetLangZParameterAREN();
                    string FirstSignupSubmit = string.Empty;
                    string url = Constants.GAZTSignUpFirstSubmit + LangZ;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

                    var serilized = JsonConvert.SerializeObject(SignUpModel);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    FirstSignupSubmit = await res.Content.ReadAsStringAsync();
                    return FirstSignupSubmit;
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
        public static string GAZTSignUpFirstSubmit(SignUpNextBodyModel SignUpModel)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    string LangZ = GetLangZParameterAREN();
                    string FirstSignupSubmit = string.Empty;
                    string url = Constants.GAZTSignUpFirstSubmit + LangZ;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

                    var serilized = JsonConvert.SerializeObject(SignUpModel);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    HttpResponseMessage res = client.PostAsync(uri, contentPost).Result;
                    FirstSignupSubmit = res.Content.ReadAsStringAsync().Result;
                    return FirstSignupSubmit;
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
        public async static Task<string> GAZTCreateAccountSubmit(CreateGaztAccountModel SignUpModel)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    string Langz = UtilityManager.GetLanguageParameter();
                    string FirstSignupSubmit = string.Empty;
                    string url = Constants.GAZTSignUpFirstSubmit + Langz;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    var serilized = JsonConvert.SerializeObject(SignUpModel);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
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
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }
        public static CaseGuidModelRootObject GAZTGetSignupGuid()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                CaseGuidModelRootObject GaztGuidModel = new CaseGuidModelRootObject();
                string IsIDTypeValidList = string.Empty;
                string NewToken = string.Empty;
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };


                    char lang = GetLangZParameter();
                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);
                    String url = Constants.GAZTSignUpGetGuid;
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
        public static async Task<TERFRegionRootObject> GAZTTESFormGetRegion()
        {
            RegionPost Cred = new RegionPost();
            Cred.WSUserName = "GAZT@CRM";
            Cred.WSPassword = "gazt@123";
            TERFRegionRootObject Listobject = new TERFRegionRootObject();
            if (CrossConnectivity.Current.IsConnected)
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
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
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
            if (CrossConnectivity.Current.IsConnected)
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
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
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
            if (CrossConnectivity.Current.IsConnected)
            {
                TERFAQs terffaq = new TERFAQs();
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                    string url = Constants.GAZTGetFAQ;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);
                    var serilized = JsonConvert.SerializeObject(Cred);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
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
            if (CrossConnectivity.Current.IsConnected)
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
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
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
            if (CrossConnectivity.Current.IsConnected)
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
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
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
        public static async Task<MyReturnsRootObject> GAZTGetReturnData(string lang, string TIN)
        {
            MyReturnsRootObject ReturnsdData = null;
            if (CrossConnectivity.Current.IsConnected)
            {
                DateTime currentDate = DateTime.Now;
                string NewToken = string.Empty;
                try
                {
                    if (false == CrossConnectivity.Current.IsConnected)
                    {
                        throw new GAZTInternetException();
                    }
                   string uri = Constants.GAZTGetReturnList + TIN + "' and Lang eq '" + lang + "'&saml2=enabled&$format=json";
                    HttpResponseMessage GAZTGetDashboardResponse = await GetServiceManager.MakeGetAPICall(uri, false, string.Empty);
                    if (GAZTGetDashboardResponse != null)
                    {
                        if (GAZTGetDashboardResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            throw new GAZTSessionExpiredException();
                        }

                        HttpHeaders headers = GAZTGetDashboardResponse.Headers;
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
                        string GAZTGetDashboardResponseJSON = GAZTGetDashboardResponse.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(GAZTGetDashboardResponseJSON))
                        {
                            GAZTGetDashboardResponseJSON = JObject.Parse(GAZTGetDashboardResponseJSON).ToString();
                            ReturnsdData = JsonConvert.DeserializeObject<MyReturnsRootObject>(GAZTGetDashboardResponseJSON);
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
            return ReturnsdData;
        }
        public static Dashboard GAZTGetAdditionalDashboardData(string lang, string TIN)
        {
            Dashboard dashboardData = null;
            if (CrossConnectivity.Current.IsConnected)
            {
                DateTime currentDate = DateTime.Now;
                string NewToken = string.Empty;
                try
                {
                    if (false == CrossConnectivity.Current.IsConnected)
                    {
                        throw new GAZTInternetException();
                    }
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string uri = Constants.GAZTGetTheSetOfUnpaidAmounts + "'" + lang + "'" + " and Gpartz eq '" + TIN + "'" + "&sap-language=" + lang + "&saml2=enabled&$format=json";
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
            return dashboardData;
        }
        public static async Task<List<OverduePaymentAndUnSubmittedReturn>> GAZTGetUnSubmittedReturnSetForDashboardData(string lang, string TIN)
        {
           List<OverduePaymentAndUnSubmittedReturn> overduePayments = null;
            if (CrossConnectivity.Current.IsConnected)
            {
                DateTime currentDate = DateTime.Now;
                string NewToken = string.Empty;
                try
                {
                    if (false == CrossConnectivity.Current.IsConnected)
                    {
                        throw new GAZTInternetException();
                    }
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    client.DefaultRequestHeaders.Add("Token", "123");
                    string uri = Constants.GAZTGetUnSubmittedReturnSetForDashboard + lang + "'" + " and Gpartz eq '" + TIN + "'" + "&sap-language=" + lang + "&saml2=enabled&$format=json";
                    HttpResponseMessage GAZTGetUnSubmittedReturnSetResponse = await GetServiceManager.MakeGetAPICall(uri, true, "123");
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
                            GAZTGetUnSubmittedReturnSetResponseJSON = JObject.Parse(GAZTGetUnSubmittedReturnSetResponseJSON)["d"].ToString();
                            GAZTGetUnSubmittedReturnSetResponseJSON = JObject.Parse(GAZTGetUnSubmittedReturnSetResponseJSON)["results"].ToString();
                            overduePayments = JsonConvert.DeserializeObject<List<OverduePaymentAndUnSubmittedReturn>>(GAZTGetUnSubmittedReturnSetResponseJSON);
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
            return overduePayments;
        }
        public static async Task<List<OverduePaymentAndUnSubmittedReturn>> GAZTGetPaymentOverdueSetForDashboardData(string lang, string TIN)
        {
            List<OverduePaymentAndUnSubmittedReturn> paymentOverdueSet = null;
            if (CrossConnectivity.Current.IsConnected)
            {
                DateTime currentDate = DateTime.Now;
                string NewToken = string.Empty;
                try
                {
                    if (false == CrossConnectivity.Current.IsConnected)
                    {
                        throw new GAZTInternetException();
                    }
                    string uri = Constants.GAZTGetPaymentOverdueSetForDashboard + lang + "'" + " and Gpartz eq '" + TIN + "'" + "&sap-language=" + lang + "&saml2=enabled&$format=json";
                    HttpResponseMessage GAZTGetPaymentOverdueSetResponse = await GetServiceManager.MakeGetAPICallWithIncomingChannel(uri, true, "123");
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
                            GAZTGetPaymentOverdueSetResponseJSON = JObject.Parse(GAZTGetPaymentOverdueSetResponseJSON)["d"].ToString();
                            GAZTGetPaymentOverdueSetResponseJSON = JObject.Parse(GAZTGetPaymentOverdueSetResponseJSON)["results"].ToString();
                            paymentOverdueSet = JsonConvert.DeserializeObject<List<OverduePaymentAndUnSubmittedReturn>>(GAZTGetPaymentOverdueSetResponseJSON);
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
            return paymentOverdueSet;
        }
        public static string SFGAZTAuthenticateTIN(string UserName, string Password, string DeviceId, string CurrentAttempt, string lang)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                string AuthenticationResult = String.Empty;
                string Message = string.Empty;
                try
                {
                    lang = "EN";
                    HttpWebRequest SOAPRequest = CreateGAZTSOAPWebRequestForAuthenticationService();
                    if (SOAPRequest != null)
                    {
                        XmlDocument SOAPReqBody = new XmlDocument();
                        SOAPReqBody.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>  
                        <soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-   instance"" xmlns:gazt=""http://gazt.gov.sa/""  xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" >  
                        <soap:Body>
                            <gazt:loginValidation>
                                <userId>" + UserName + @"</userId>
                                   <lang>" + lang + @"</lang> 
                                <password>" + Password + @"</password>
                                 <deviceId>" + DeviceId + @"</deviceId>
                                        <count>" + CurrentAttempt + @"</count>
                            </gazt:loginValidation>  
                        </soap:Body>  
                        </soap:Envelope>");
                        using (Stream stream = SOAPRequest.GetRequestStream())
                        {
                            SOAPReqBody.Save(stream);
                        }
                        //Geting response from request  
                        using (WebResponse SOAPRequestResponse = SOAPRequest.GetResponse())
                        {
                            using (StreamReader rd = new StreamReader(SOAPRequestResponse.GetResponseStream()))
                            {
                                if (rd != null)
                                {
                                    //reading stream  
                                    var ServiceResult = rd.ReadToEnd();
                                    XmlDocument xmlDoc = new XmlDocument();
                                    xmlDoc.LoadXml(ServiceResult);
                                    XmlNamespaceManager xmlnsManager = new System.Xml.XmlNamespaceManager(xmlDoc.NameTable);
                                    xmlnsManager.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                                    xmlnsManager.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
                                    xmlnsManager.AddNamespace("xsd", "http://www.w3.org/2001/XMLSchema");
                                    xmlnsManager.AddNamespace("ns2", "http://gazt.gov.sa/");
                                    XmlNode node = xmlDoc.SelectSingleNode("/soap:Envelope/soap:Body/ns2:loginValidationResponse/LoginResponse", xmlnsManager);
                                    App.Token = node.ChildNodes[0].InnerText;
                                    if (node.ChildNodes.Count == 5)
                                        NumberOfValiedAttempts = node.ChildNodes[4].InnerText;
                                    if ((0 == String.Compare(App.Token, "User does not exist")))
                                    {
                                        throw new GAZTUserDoesNotExistException();
                                    }
                                    if ((0 == String.Compare(App.Token, "User authentication failed")))
                                    {
                                        throw new GAZTUserAuthenticationFailedException();
                                    }
                                    if ((0 == String.Compare(App.Token, "Authentication failed. Password locked")))
                                    {
                                        throw new GAZTPasswordLockedException();
                                    }
                                    if ((0 == String.Compare(App.Token, "User is not currently valid")))
                                    {
                                        throw new GAZTUserCurrentlyInvalidException();
                                    }
                                    if ((0 == String.Compare(App.Token, "User account locked")))
                                    {
                                        throw new GAZTUserAccountLockedException();
                                    }
                                    if ((0 == String.Compare(App.Token, "Password is locked. Invalid attempts")))
                                    {
                                        throw new GAZTPasswordIsLockedDueToInvalidAttemptsException();
                                    }
                                    if ((0 == String.Compare(App.Token, "Taxpayer's account is not active with GAZT.")))
                                    {
                                        throw new GAZTTaxpayersAccountInActiveWithGAZTException();
                                    }
                                    if ((0 == String.Compare(App.Token, "Wrong entering for the TIN or the Email")))
                                    {
                                        throw new GAZTWrongTINOrEmailException();
                                    }
                                    if ((0 == String.Compare(App.Token, "Wrong password")))
                                    {
                                        throw new GAZTWrongPasswordException();
                                    }
                                    if ((0 == String.Compare(App.Token, "The account is locked for 60 minutes after the last login attempt")))
                                    {
                                        throw new GAZTAccountLockedFor60MinutesAfterLastLoginAttemptException();
                                    }
                                    if ((0 == String.Compare(App.Token, "Incomplete")) || (0 == String.Compare(App.Token, "Deregister - Death")) || (0 == String.Compare(App.Token, "Deregister - Bankruptcy")) || (0 == String.Compare(App.Token, "Deregister - Liquidation")) || (0 == String.Compare(App.Token, "Deregister - Merger")) || (0 == String.Compare(App.Token, "Deregister - Acquisition")) || (0 == String.Compare(App.Token, "Suspension - Bankruptcy")) || (0 == String.Compare(App.Token, "Suspension - Liquidation/Close")) || (0 == String.Compare(App.Token, "Deregister - Close")) || (0 == String.Compare(App.Token, "Deregister - Company-Establish")) || (0 == String.Compare(App.Token, "Suspension - Est. to Company")))
                                    {
                                        throw new GAZTTaxpayersAccountNotActiveWithGAZTException();
                                    }
                                    Message = node.ChildNodes[1].InnerText;
                                }
                                else
                                    throw new GAZTNetworkConnectivityIssueException("Network Connectivity Issue");
                            }
                        }
                    }
                    else
                        throw new GAZTNetworkConnectivityIssueException("Network Connectivity Issue");
                    return Message;
                }
                catch (XPathException)
                {
                    throw new GAZTInvalidDataException(AppResources.Somethingwentwrong);
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
                throw new GAZTInternetException("Network Issue");
            }
        }

        public static string CreateSAMLLoginURL(string Euser, string DeviceId, string FcmId, string DeviceTyp, string Language)
        {
            string FullUrl = Constants.GAZTSAMLLoginService + "(Euser='" + Euser + "'" + ",DeviceId='" + DeviceId + "'" + ",FcmId='" +
                FcmId + "'" + ",DeviceTyp='" + DeviceTyp + "')?sap-language=" + Language + "&$format=json";
            return FullUrl;
        }

        public static LoginModel SFGAZTGetLoginDataAndroid(string url)
        {
            if (CrossConnectivity.Current.IsConnected)
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
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    CookieContainer cookieContainer = new CookieContainer();

                    try
                    {
                        foreach (CookieModel cookieModel in App.LoginCookiesRetrieved)
                        {
                            Cookie cookie = new Cookie();
                            cookie.Domain = Constants.PartialDomainUrlForCookies;
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

                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    HttpClient client = new HttpClient(App.httpClientHandler);
                    Uri uri = new Uri(url);
                    LoginModel loginModel = new LoginModel();

                    HttpResponseMessage GAZTGetLoginDataResponseJSON = client.GetAsync(uri).Result;
                    Console.WriteLine(GAZTGetLoginDataResponseJSON);

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
                catch (HttpRequestException ex)
                {
                    throw ex;
                }
                catch (GAZTSessionExpiredException ex)
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
                throw new GAZTInternetException(String.Empty);
            }
        }


        public static async Task<LoginModel> SFGAZTGetLoginData(string url)
        {
            if (CrossConnectivity.Current.IsConnected)
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
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
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

                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    HttpClient client = new HttpClient(App.httpClientHandler);
                    Uri uri = new Uri(url);
                    LoginModel loginModel = new LoginModel();

                    HttpResponseMessage GAZTGetLoginDataResponseJSON = await client.GetAsync(uri);
                    Console.WriteLine(GAZTGetLoginDataResponseJSON);

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
                catch (HttpRequestException ex)
                {
                    throw ex;
                }
                catch (GAZTSessionExpiredException ex)
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
                throw new GAZTInternetException(String.Empty);
            }
        }


        public static List<TIN> SFGAZTGetAllTINs(string UserName)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                String GAZTGetTINsResponseResult = String.Empty;
                List<TIN> TINs = null;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string url = Constants.GetAllTin + UserName;
                    Uri uri = new Uri(url);
                    HttpResponseMessage GAZTGetTINsResponse = client.GetAsync(uri).Result;
                    if (GAZTGetTINsResponse != null)
                    {
                        GAZTGetTINsResponseResult = GAZTGetTINsResponse.Content.ReadAsStringAsync().Result;
                    }
                    if (!string.IsNullOrEmpty(GAZTGetTINsResponseResult))
                    {
                        GAZTGetTINsResponseResult = JObject.Parse(GAZTGetTINsResponseResult)["tinData"].ToString();
                        TINs = JsonConvert.DeserializeObject<List<TIN>>(GAZTGetTINsResponseResult);
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
                throw new GAZTInternetException(String.Empty);
            }
        }
        public static async Task GAZTLogOff()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                String GAZTGetLogoffResponseResult = String.Empty;
                try
                {
                    String url = Constants.GAZTSAMLLogoutService;
                    HttpResponseMessage GAZTLogOffResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
                    App.LoginCookiesRetrieved = null;

                    App.CreateClientHandler();

                    if (GAZTLogOffResponse != null)
                    {
                        GAZTGetLogoffResponseResult = GAZTLogOffResponse.Content.ReadAsStringAsync().Result;
                    }
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
            if (CrossConnectivity.Current.IsConnected)
            {
                string MobileNumber = string.Empty;
                string PdfUrl = string.Empty;
                string NewToken = string.Empty;
                try
                {
                    CookieContainer cookieContainer = new CookieContainer();

                    try
                    {
                        foreach (CookieModel cookieModel in App.LoginCookiesRetrieved)
                        {
                            Cookie cookie = new Cookie();

                            if (Device.RuntimePlatform == Device.iOS)
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
                            else if (Device.RuntimePlatform == Device.Android)
                            {
                                cookie.Domain = Constants.PartialDomainUrlForCookies;
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

                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.GAZTGetTP + "='" + TIN + "',Langz='" + Lang + "')" + "?&$expand=TPOC_LIST&saml2=enabled&$format=json";
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
                catch (GAZTSessionExpiredException ex)
                {
                    throw ex;
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
            if (CrossConnectivity.Current.IsConnected)
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
            if (CrossConnectivity.Current.IsConnected)
            {
                String GAZTAttachmentsResponseResult = String.Empty;
                AttachmentDocumentModel attachmentDocumentModel = null;
                try
                {
                    string url = Constants.GAZTGetAllAttachments + fbNum + "'" + " and RetGuid eq '" + retGuid + "'" + "&saml2=enabled&$format=json";
                    HttpResponseMessage GAZTGetAllAttachmentsResponse = await GetServiceManager.MakeGetAPICall(url, true, "123");
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

            if (CrossConnectivity.Current.IsConnected)
            {

                string NewToken = string.Empty;
                try
                {
                    Char lang = WebServiceManager.GetLangZParameter();
                    String url = Constants.ZakatValidateRevokeListUrl + "Fbnum='" + fbnum + "')?&$format=json";
                    HttpResponseMessage _zakatRevokeValidateResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
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
            if (CrossConnectivity.Current.IsConnected)
            {

                string NewToken = string.Empty;
                try
                {
                    Char lang = WebServiceManager.GetLangZParameter();
                    String url = Constants.ZakateRevokeSendOTPUrl + "Fbnum eq'" + fbNum + "'and Code eq'" + code + "'and  Tin eq '" + App.LoginDataRetrieved.TIN + "'&$format=json";
                    HttpResponseMessage _zakatRevokeSendSMSResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
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

                        if (!string.IsNullOrEmpty(_zakatRevokeSendSMSResponseData) && _zakatRevokeSendSMSModel.d == null)
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

        

        #region Download File

        public async static System.Threading.Tasks.Task<bool> FileDownload(string url, string fileExtension)
        {
            if (CrossConnectivity.Current.IsConnected)
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
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                string FbGuid = App.LoginDataRetrieved.FbGuid;
                try
                {
                    VATDeclaration _vATDeclaration = new VATDeclaration();
                    char LangZ = GetLangZParameter();
                    String Lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.GAZTGetAllVATDeclarationReturnData + "" + "'" + ",Fbnumz='" + Fbnumz + "" + "'" + ",Langz='" + Lang + "'" + ",Officerz='" + "" + "'" + ",Gpartz='" + App.TP.Tin + "'" + ",Euser='" + "'" + ",Fbguid='" + "'" + ")?&$expand=ADRSet,ATTACHSet,CFSet,IBANSet,NOTESSet,VATR_MSGSet,VATPERITEMSet&$format=json";
                    client.DefaultRequestHeaders.Add("Token", "123");
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
        public static Task<TaxPayerProfile> GetTPProfileDataAPICall(string TIN)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                string lang = string.Empty;
                if (App.IsArabic) { lang = "A"; }
                else { lang = "E"; }
                try
                {
                    CookieContainer cookieContainer = new CookieContainer();

                    try
                    {
                        foreach (CookieModel cookieModel in App.LoginCookiesRetrieved)
                        {
                            Cookie cookie = new Cookie();

                            if (Device.RuntimePlatform == Device.iOS)
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
                            else if (Device.RuntimePlatform == Device.Android)
                            {
                                cookie.Domain = Constants.PartialDomainUrlForCookies;
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

                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
                catch (Exception)
                {

                }

                HttpClient client = new HttpClient(App.httpClientHandler);
                string URL = Constants.TPProfileURL
                            + "(" + "Taxpayerz=" + "'" + TIN + "'"
                            + ",Langz=" + "'" + lang + "'"
                            + ",Euser=" + "'null'"
                            + ",Fbguid=" + "'null'"
                            + ",Euser1=" + "''"
                            + ",Euser2=" + "''"
                            + ",Euser3=" + "''"
                            + ",Euser4=" + "''"
                            + ",Euser5=" + "''" + ")?$format=json";

                var URI = new Uri(URL);
                Task<TaxPayerProfile> TPProfileData = GetTPProfileAndUpdatePasswordAPICall(URI);
                return TPProfileData;
            }
            else
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
        }

        public static Task<TaxPayerProfile> ChangeTPProfilePasswordAPICall(string oldPassword, string newPassword)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                string lang = string.Empty;
                if (App.IsArabic) { lang = "AR"; }
                else { lang = "EN"; }

                string URL = Constants.GetTPProfileChangePWDURL
                            + "(" + "Email=" + "'" + App.TP.Email + "'"
                            + ",PasswordOld=" + "'" + oldPassword + "'"
                            + ",PasswordNew=" + "'" + newPassword + "'"
                            + ",Partner=" + "'" + App.TP.Tin + "'"
                            + ",PasswordConf=" + "'" + newPassword + "'"
                            + ",Euser1=" + "''"
                            + ",Euser2=" + "''"
                            + ",Euser3=" + "''"
                            + ",Euser4=" + "''"
                            + ",Euser5=" + "''" + ")?$format=json" + "&sap-language=" + lang;

                var URI = new Uri(URL);
                Task<TaxPayerProfile> TPProfileData = GetTPProfileAndUpdatePasswordAPICall(URI);
                return TPProfileData;
            }
            else
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
        }

        private static async Task<TaxPayerProfile> GetTPProfileAndUpdatePasswordAPICall(Uri GetURL)
        {
            TaxPayerProfile TPProfileData = null;
            string NewToken = string.Empty;
            string GAZTTPProfileResponseJSON = string.Empty;
            try
            {
                HttpClient client = new HttpClient(App.httpClientHandler);
                HttpResponseMessage UpdatePWDResponse = await client.GetAsync(GetURL);
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
                            GAZTTPProfileResponseJSON = JObject.Parse(GAZTTPProfileResponseJSON)["d"].ToString();
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

        public static async Task<TaxPayerProfile> POSTTPProfileAPICalls(TPProfileAPIRequest TPProfileAPIRequestPOSTData, string APIType)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                TaxPayerProfile TP = null;
                string GAZTTPProfileResponseJSON = string.Empty;

                try
                {
                    string lang = string.Empty;
                    if (App.IsArabic) { lang = "AR"; }
                    else { lang = "EN"; }

                    string url = Constants.TPProfileURL + "?sap-language=" + lang;
                    var uri = new Uri(url);

                    try { App.httpClientHandler.CookieContainer = null; }
                    catch (Exception) { }

                    HttpClient client = new HttpClient(App.httpClientHandler);

                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    var serilized = JsonConvert.SerializeObject(TPProfileAPIRequestPOSTData);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);

                    GAZTTPProfileResponseJSON = res.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(GAZTTPProfileResponseJSON))
                    {
                        GAZTTPProfileResponseJSON = JObject.Parse(GAZTTPProfileResponseJSON)["d"].ToString();
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
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                string FbGuid = App.LoginDataRetrieved.FbGuid;
                try
                {
                    ASTabIdentification _asTabIdentification = new ASTabIdentification();
                    char LangZ = GetLangZParameter();
                    String Lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.AccountStatementTabIdentification + "Euser=''," + "Fbguid=" + "'" + App.LoginDataRetrieved.FbGuid + "')?$format=json";

                    client.DefaultRequestHeaders.Add("Token", "123");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

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
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                string FbGuid = App.LoginDataRetrieved.FbGuid;
                try
                {
                    ASRevenueDropDownSet _asTabIdentification = new ASRevenueDropDownSet();
                    char LangZ = GetLangZParameter();
                    String Lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.AccountStatementRevenueDropDownSet + "Euser eq ''" + " and Fbguid eq '" + App.LoginDataRetrieved.FbGuid + "'" + " and TaxType eq '" + taxType + "'" + " and Langz eq '" + LangZ + "'&$format=json";

                    client.DefaultRequestHeaders.Add("Token", "123");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

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
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                string FbGuid = App.LoginDataRetrieved.FbGuid;
                try
                {
                    ASYearValuesHeader _asTabIdentification = new ASYearValuesHeader();
                    char LangZ = GetLangZParameter();
                    String Lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.AccountStatementGetYearValues + "Fguid eq '" + App.LoginDataRetrieved.FbGuid + "'" + " and TaxType eq '" + taxType + "'" + " and StatementFilter eq '" + statementFilter + "'" + "&$format=json";

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

                String url = Constants.ValidatePaymentInformation + "?sap-language=" + UtilityManager.GetLanguageParameter() + "";



                var uri = new Uri(url);

                HttpClient client = new HttpClient(App.httpClientHandler);

                var serilized = JsonConvert.SerializeObject(PayDetails);

                client.DefaultRequestHeaders.Add("Token", App.Token);

                client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

                client.DefaultRequestHeaders.Add("X-Requested-With", "X");

                client.DefaultRequestHeaders.Add("Accept", "application/json");

                HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);

                HttpResponseMessage res = client.PostAsync(uri, contentPost).Result;

                _paymentsubmitResponse = res.Content.ReadAsStringAsync().Result;

                paymentResponse = JsonConvert.DeserializeObject<ValidatePaymentResponse>(_paymentsubmitResponse);

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


        public static DashboardInstalmentplan GAZTGetDashboardInstalmentPlanData(string lang, string TIN)
        {
            DashboardInstalmentplan dashboardInstalmentData = null;
            if (CrossConnectivity.Current.IsConnected)
            {
                DateTime currentDate = DateTime.Now;
                string NewToken = string.Empty;
                try
                {
                    if (false == CrossConnectivity.Current.IsConnected)
                    {
                        throw new GAZTInternetException();
                    }
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String uri = Constants.GetDashboardInstalmentPlanData + "='" + TIN + "',Lang='" + lang + "')" + "?$expand=INST_PLAN_itemSet&$format=json";

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
                            GAZTGetDashboardInstalmentResponseJSON = JObject.Parse(GAZTGetDashboardInstalmentResponseJSON)["d"].ToString();
                            dashboardInstalmentData = JsonConvert.DeserializeObject<DashboardInstalmentplan>(GAZTGetDashboardInstalmentResponseJSON);
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
                catch (Exception)
                {
                    throw new GAZTNetworkConnectivityIssueException();
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

            if (CrossConnectivity.Current.IsConnected)

            {

                DateTime currentDate = DateTime.Now;

                string NewToken = string.Empty;

                try

                {

                    if (false == CrossConnectivity.Current.IsConnected)

                    {

                        throw new GAZTInternetException();

                    }

                    HttpClient client = new HttpClient(App.httpClientHandler);

                    String uri = Constants.CancelPaymentService + "'" + GUID + "',SRCID='" + type + "',CANC_RES='01')" + "?$format=json";





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

                throw new GAZTInternetException();

            }

            return paymentResponse;

        }





        public static async Task<EGAZT.Models.PaymentModel.ValidatePaymentResponse> GAZTValidateMyBillsPayment(string fbNum, string TIN, string devicetype, string sadadNo, string paymentType)

        {

            ValidatePaymentResponse paymentResponse = null;

            if (CrossConnectivity.Current.IsConnected)

            {

                DateTime currentDate = DateTime.Now;

                string NewToken = string.Empty;

                try

                {

                    if (false == CrossConnectivity.Current.IsConnected)

                    {

                        throw new GAZTInternetException();

                    }

                    HttpClient client = new HttpClient(App.httpClientHandler);

                    // String uri = Constants.ValidatePaymentInformation + "'" + fbNum + "',Tin='" + TIN + "',Srcid='"+devicetype+"')" + "?$format=json";

                    String uri = Constants.ValidatePaymentInformation + "'" + fbNum + "',Tin='" + TIN + "',Srcid='" + devicetype + "',Sadad='" + sadadNo + "',Pymntty='" + paymentType + "')" + "?$format=json";







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

            return paymentResponse;

        }





        public static async Task<MadaPaymentResponse> GAZTUpdateMadaPaymentDetails(string caseGuid, string devicetype)

        {

            MadaPaymentResponse paymentResponse = null;

            if (CrossConnectivity.Current.IsConnected)

            {

                DateTime currentDate = DateTime.Now;

                string NewToken = string.Empty;

                try

                {

                    if (false == CrossConnectivity.Current.IsConnected)

                    {

                        throw new GAZTInternetException();

                    }

                    HttpClient client = new HttpClient(App.httpClientHandler);

                    String uri = Constants.UpdateMadaPaymentInformation + "'" + caseGuid + "',Srcid='" + devicetype + "')" + "?$format=json&sap-language=" + UtilityManager.GetLanguageParameter() + "";



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

                catch (Exception)

                {

                    throw new GAZTNetworkConnectivityIssueException();

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

                String url = Constants.ApplePayGenerateGuid;

                var uri = new Uri(url);

                HttpClient client = new HttpClient(App.httpClientHandler);

                var serilized = JsonConvert.SerializeObject(applePayDetails);

                client.DefaultRequestHeaders.Add("Token", App.Token);

                client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

                client.DefaultRequestHeaders.Add("X-Requested-With", "X");

                client.DefaultRequestHeaders.Add("Accept", "application/json");





                HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);

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



        public async static Task<EGAZT.Models.PaymentModel.ApplePayTokenResponse> GAZTUpdateApplePayGuid(ApplePayToken applePayDetails)

        {

            ApplePayTokenResponse paymentResponse = null;



            string _paymentsubmitResponse = string.Empty;

            try

            {



                string platform = "";



                if (Device.RuntimePlatform == Device.iOS)

                {

                    platform = "C4";

                }

                else if (Device.RuntimePlatform == Device.Android)

                {

                    platform = "C3";

                }

                applePayDetails.SrcId = platform;



                String url = Constants.UpdateApplePayGuid;

                var uri = new Uri(url);

                HttpClient client = new HttpClient(App.httpClientHandler);

                var serilized = JsonConvert.SerializeObject(applePayDetails);

                client.DefaultRequestHeaders.Add("Token", App.Token);

                client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

                client.DefaultRequestHeaders.Add("X-Requested-With", "X");

                client.DefaultRequestHeaders.Add("Accept", "application/json");



                HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);

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











        public static async Task<ASStatementHeaderSet> GAZTGetAccountStatementHeaderSet(string statementFilter, string fiscalYear, string taxType , bool isFromDashboard)

        {

            if (CrossConnectivity.Current.IsConnected)

            {

                string NewToken = string.Empty;

                string FbGuid = App.LoginDataRetrieved.FbGuid;

                try

                {

                    ASStatementHeaderSet _asTabIdentification = new ASStatementHeaderSet();

                    char LangZ = GetLangZParameter();

                    String Lang = UtilityManager.GetLanguageParameter();

                    HttpClient client = new HttpClient(App.httpClientHandler);

                    String isLoad = "";

                    if (!string.IsNullOrEmpty(statementFilter) && statementFilter == "10")

                    {
                        if (isFromDashboard) {

                            isLoad = "X";
                        }

                        



                    }

                    String url = Constants.AccountStatementGetHeaderSet + "Fbguid=" + "'" + App.LoginDataRetrieved.FbGuid + "',StatementFilter='" + statementFilter + "',FiscalYear='" + fiscalYear + "',TaxType='" + taxType + "',Lang='" + LangZ + "',Load='" + isLoad + "')?&$expand=StatmenetLineItemsSet,TaxRelationSet&$format=json";



                    client.DefaultRequestHeaders.Add("Token", "123");

                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);







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

            if (CrossConnectivity.Current.IsConnected)

            {

                DateTime currentDate = DateTime.Now;

                string NewToken = string.Empty;

                try

                {

                    if (false == CrossConnectivity.Current.IsConnected)

                    {

                        throw new GAZTInternetException();

                    }

                    HttpClient client = new HttpClient(App.httpClientHandler);

                    string uri = Constants.GetDashboardData + TIN + "'" + "&saml2=enabled" + "&$format=json";

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

                        string GAZTGetDashboardResponseJSON = GAZTGetDashboardResponse.Content.ReadAsStringAsync().Result;

                        if (!string.IsNullOrEmpty(GAZTGetDashboardResponseJSON))

                        {

                            GAZTGetDashboardResponseJSON = JObject.Parse(GAZTGetDashboardResponseJSON)["d"].ToString();

                            dashboardData = JsonConvert.DeserializeObject<Dashboard>(GAZTGetDashboardResponseJSON);

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

                catch (Exception)

                {

                    throw new GAZTNetworkConnectivityIssueException();

                }

            }

            else

            {

                throw new GAZTInternetException();

            }

            return dashboardData;

        }

        #endregion

    }
}
