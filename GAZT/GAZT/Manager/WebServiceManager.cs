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

namespace GAZT.Manager
{
    public static class WebServiceManager
    {
        public static string ErrorMessage = string.Empty;
        public static string ErrorMessageForVAT = string.Empty;
        public static string NumberOfValiedAttempts = string.Empty;
        public static string ErrorMessageForUnlockAccount = string.Empty;

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
        //Handling Internet Exception for the Login URL loaded in the webview
        public static async Task<bool> GAZTCheckConnectivity()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.BaseUrlOfODataServices;
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTGetTINsResponse = await client.GetAsync(uri);
                    return true;
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

        //done internet exception handling
        /// <summary>
        /// Method used for authenticaating TP
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static String GAZTAuthenticateTIN(String UserName, String Password, string DeviceId, string CurrentAttempt, string lang)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                lang = "EN";
                string AuthenticationResult = String.Empty;
                string Message = string.Empty;
                string Token = string.Empty;
                try
                {
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
                        using (WebResponse Serviceres = SOAPRequest.GetResponse())
                        {
                            using (StreamReader rd = new StreamReader(Serviceres.GetResponseStream()))
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
                                    Token = node.ChildNodes[0].InnerText;
                                    NumberOfValiedAttempts = "3";// node.ChildNodes[2].InnerText;
                                    if ((0 == String.Compare(Token, "User does not exist")))
                                    {
                                        throw new Exception(Token);
                                    }
                                    if ((0 == String.Compare(Token, "User authentication failed")))
                                    {
                                        throw new Exception(Token);
                                    }
                                    if ((0 == String.Compare(Token, "Authentication failed. Password locked")))
                                    {
                                        throw new Exception(Token);
                                    }
                                    if ((0 == String.Compare(Token, "User is not currently valid")))
                                    {
                                        throw new Exception(Token);
                                    }
                                    if ((0 == String.Compare(Token, "User account locked")))
                                    {
                                        throw new Exception(Token);
                                    }
                                    if ((0 == String.Compare(Token, "Password is locked. Invalid attempts")))
                                    {
                                        throw new Exception(Token);
                                    }
                                    if ((0 == String.Compare(Token, "Taxpayer's account is not active with GAZT.")))
                                    {
                                        throw new Exception(Token);
                                    }
                                    if ((0 == String.Compare(Token, "Wrong entering for the TIN or the Email")))
                                    {
                                        throw new Exception(Token);
                                    }
                                    if ((0 == String.Compare(Token, "Wrong password")))
                                    {
                                        throw new Exception(Token);
                                    }
                                    if ((0 == String.Compare(Token, "The account is locked for 60 minutes after the last login attempt")))
                                    {
                                        throw new Exception(Token);
                                    }
                                    if ((0 == String.Compare(Token, "Incomplete")) || (0 == String.Compare(Token, "Deregister - Death")) || (0 == String.Compare(Token, "Deregister - Bankruptcy")) || (0 == String.Compare(Token, "Deregister - Liquidation")) || (0 == String.Compare(Token, "Deregister - Merger")) || (0 == String.Compare(Token, "Deregister - Acquisition")) || (0 == String.Compare(Token, "Suspension - Bankruptcy")) || (0 == String.Compare(Token, "Suspension - Liquidation/Close")) || (0 == String.Compare(Token, "Deregister - Close")) || (0 == String.Compare(Token, "Deregister - Company-Establish")) || (0 == String.Compare(Token, "Suspension - Est. to Company")))
                                    {
                                        throw new Exception("User Deactive");
                                    }
                                    // Password is locked.Invalid attempts
                                    if (!string.IsNullOrEmpty(Token))
                                    {
                                        App.Token = Token;
                                        App.IsSessionExpired = false;
                                    }
                                    Message = node.ChildNodes[1].InnerText;
                                    if (Message.Equals("5"))
                                    {
                                        throw new Exception(AppResources.NetworkConnectivityIssue);
                                    }
                                }
                                else
                                {
                                    throw new Exception(AppResources.NetworkConnectivityIssue);
                                }
                            }
                        }
                    }
                    else
                        throw new Exception(AppResources.NetworkConnectivityIssue);
                    return Message;
                }
                catch (Exception ex)
                {
                    if (string.Equals(ex.Message, "User does not exist"))
                    {
                        throw new Exception(AppResources.UserDoesNotExist);
                    }
                    else if (string.Equals(ex.Message, "User authentication failed"))
                    {
                        throw new Exception(AppResources.UserAuthenticationFailed);
                    }
                    else if (string.Equals(ex.Message, "Authentication failed. Password locked"))
                    {
                        throw new Exception(AppResources.ZPasswordLocked);
                    }
                    else if (string.Equals(ex.Message, "User is not currently valid"))
                    {
                        throw new Exception(AppResources.ZUserNotValid);
                    }
                    else if (string.Equals(ex.Message, "User account locked"))
                    {
                        throw new Exception(AppResources.UserAccountLocked);
                    }
                    else if ((0 == String.Compare(Token, "Password is locked. Invalid attempts")))
                    {
                        throw new Exception(AppResources.ZZPasswordislockedInvalidattempts);
                    }
                    else if ((0 == String.Compare(Token, "Taxpayer's account is not active with GAZT.")))
                    {
                        throw new Exception(Token);
                    }
                    else if ((0 == String.Compare(ex.Message, "Wrong entering for the TIN or the Email")))
                    {
                        if (App.IsArabic)
                        {
                            throw new Exception("خطأ في إدخال الرقم المميز أو البريد الإلكتروني");
                        }
                        else
                        {
                            throw new Exception("Wrong entering for the TIN or the Email");
                        }
                    }
                    else if ((0 == String.Compare(ex.Message, "Wrong password")))
                    {
                        if (App.IsArabic)
                        {
                            throw new Exception("خطا في  كلمة  المرور");
                        }
                        else
                        {
                            throw new Exception("Wrong password");
                        }
                    }
                    else if ((0 == String.Compare(ex.Message, "The account is locked for 60 minutes after the last login attempt")))
                    {
                        if (App.IsArabic)
                        {
                            throw new Exception("الحساب معلق لمدة 60 دقيقة من أخر محاولة للدخول");
                        }
                        else
                        {
                            throw new Exception("The account is locked for 60 minutes after the last login attempt");
                        }
                    }
                    else if ((0 == String.Compare(ex.Message, "User Deactive")))
                    {
                        if (App.IsArabic)
                        {
                            throw new Exception("حساب المكلف غير مفعل في الهيئة العامة للزكاة والدخل");
                        }
                        else
                        {
                            throw new Exception("Taxpayer's account is not active with GAZT.");
                        }
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
        //done internet exception handling
        /// <summary>
        /// After GAZTAuthenticateTIN, this is the 1st mandatory call and that too without OTP
        /// </summary>
        /// <param name="Lang"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        /// 
        public static async Task<List<TIN>> GAZTGetAllTins(String Username)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                String GAZTGetTINsResponseResult = String.Empty;
                List<TIN> TINs = null;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.GetAllTin + Username;
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTGetTINsResponse = await client.GetAsync(uri);
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
        //done internet exception handling
        public static async Task<String> GAZTSendAndReceiveOTP(String Lang, String UserId, string currentAttempts)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                String OTPSentConfirmation = String.Empty;
                string NewToken = string.Empty;
                try
                {
                    string otp = "";
                    string _language = "EN";
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.GAZTSendAndReceiveOTP + Lang + "',Userid='" + UserId + "',Otp='" + "',CurrAttmps=" + currentAttempts + ")?&saml2=enabled&sap-" + "language='" + _language + "" + "'" + "&$format=json"; //)?&saml2=disabled&$format=json";
                                                                                                                                                                                                                                   //  https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/ZDP_USRLOGIN_OTP_SRV/HEADERSet(Langz='E',Userid='',Otp='34455',CurrAttmps=1)?&saml2=disabled&sap-language='EN'&$format=xml
                    var uri = new Uri(url);
                    ////client.DefaultRequestHeaders.Add("Token", App.Token);
                    HttpResponseMessage GAZTSendAndReceiveOTPResponse = await client.GetAsync(uri);


                    if (GAZTSendAndReceiveOTPResponse != null)
                    {
                        HttpHeaders headers = GAZTSendAndReceiveOTPResponse.Headers;
                        IEnumerable<string> values;
                    }
                    OTPSentConfirmation = GAZTSendAndReceiveOTPResponse.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(OTPSentConfirmation))
                    {
                        OTPSentConfirmation = JObject.Parse(OTPSentConfirmation)["d"].ToString();
                        JToken OTPSentConfirmationJToken = JObject.Parse(OTPSentConfirmation)["Result"];
                        if (OTPSentConfirmationJToken != null)
                            OTPSentConfirmation = OTPSentConfirmationJToken.Value<String>();
                    }
                    return OTPSentConfirmation;
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
        //done internet exception handling
        /// <summary>
        /// The user received OTP and provided for authorization
        /// </summary>
        /// <param name="Lang"></param>
        /// <param name="UserId"></param>
        /// <param name="OTP"></param>
        /// <returns></returns>
        public static async Task<TaxPayerProfile> GAZTValidateOTP(String Lang, String UserId, String OTP, string currentAttempts)
        {
            TaxPayerProfile TP = null;
            String NewToken = string.Empty;
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    string lang = "";
                    if (App.IsArabic)
                    {
                        lang = "A";
                    }
                    else
                    {
                        lang = "E";
                    }

                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.GAZTSendAndReceiveOTP + lang + "',Userid='" + UserId + "',Otp='" + OTP + "',CurrAttmps=" + currentAttempts + ")?&saml2=enabled&sap-" + "language='" + Lang + "" + "'" + "&$format=json"; //)?&saml2=disabled&$format=json";Constants.GAZTValidateOTP + Lang + "',Userid='" + UserId + "',Otp='" + OTP + "')?&saml2=disabled&$format=json";
                    var uri = new Uri(url);
                    ////client.DefaultRequestHeaders.Add("Token", App.Token);
                    HttpResponseMessage GAZTValidateOTPResponse = await client.GetAsync(uri);
                    if (GAZTValidateOTPResponse != null)
                    {
                        if (GAZTValidateOTPResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        if (GAZTValidateOTPResponse.Headers != null)
                        {

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
                            String GAZTValidateOTPResponseJSON = GAZTValidateOTPResponse.Content.ReadAsStringAsync().Result;
                            if (!string.IsNullOrEmpty(GAZTValidateOTPResponseJSON))
                            {
                                GAZTValidateOTPResponseJSON = JObject.Parse(GAZTValidateOTPResponseJSON)["d"].ToString();
                                JToken GAZTValidateOTPResponseJToken = JObject.Parse(GAZTValidateOTPResponseJSON)["Result"];
                                TP = JsonConvert.DeserializeObject<TaxPayerProfile>(GAZTValidateOTPResponseJSON);
                            }
                        }
                    }
                    else
                    {
                        throw new Exception(AppResources.NetworkConnectivityIssue);
                    }
                    return TP;
                }
                catch (Exception ex)
                {
                    if (string.Equals(ex.Message, AppResources.InvalidOTP))
                    {
                        throw new Exception(AppResources.InvalidOTP);
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
        //done internet exception handling
        public static async Task<TaxPayerProfile> GAZTValidateOTPForMobileNumber(String Lang, String OTP, String Tin, string CurrentMobileNumber, string NewMobileNumber,string mobileCountry)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                TaxPayerProfile TP = null;
                string NewToken = string.Empty;
                try
                {
                    NewMobileNumber = NewMobileNumber.Replace("+", "");
                    CurrentMobileNumber = CurrentMobileNumber.Replace("+", "");
                    NewMobileNumber = "00" + NewMobileNumber;
                    //CurrentMobileNumber = "00" + CurrentMobileNumber;
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.GAZTValidateOTPForMobile + "Langz='" + Lang + "',Tin='" + Tin + "',Otp='" + OTP + "',CurrEmail='" + "" + "',NewEmail='" + "" + "',CurrMobile='" + CurrentMobileNumber +"',MobileCountry='"+mobileCountry+ "',NewMobile='" + NewMobileNumber + "',CurrPwd='" + "" + "',NewPwd='" + "')?$format=json&saml2=enabled&sap-language=" + Lang;
                    var uri = new Uri(url);
                    ////client.DefaultRequestHeaders.Add("Token", App.Token);
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
                        String GAZTValidateOTPResponseJSON = GAZTValidateOTPResponse.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(GAZTValidateOTPResponseJSON))
                        {
                            GAZTValidateOTPResponseJSON = JObject.Parse(GAZTValidateOTPResponseJSON)["d"].ToString();
                            string GAZTValidateOTPResponseJToken = JObject.Parse(GAZTValidateOTPResponseJSON)["Result"].ToString();
                            if (GAZTValidateOTPResponseJToken == "Details Changed Successfully" || GAZTValidateOTPResponseJToken.ToString() == "تم تغيير التفاصيل بنجاح")
                            {
                                TP = JsonConvert.DeserializeObject<TaxPayerProfile>(GAZTValidateOTPResponseJSON);
                            }
                            else
                            {
                                throw new Exception(AppResources.InvalidOTP);
                            }
                        }
                    }
                    return TP;
                }
                catch (Exception ex)
                {
                    if (string.Equals(ex.Message, AppResources.InvalidOTP))
                    {
                        throw new Exception(AppResources.InvalidOTP);
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
        //done internet exception handling
        public static async Task<bool> GAZTValidateMobileNumber(String Lang, String Tin, string CurrentMobileNumber, string NewMobileNumber, string mobileCountry)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                bool result = false;
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    NewMobileNumber = NewMobileNumber.Replace("+", "");
                    CurrentMobileNumber = CurrentMobileNumber.Replace("+", "");
                    NewMobileNumber = "00" + NewMobileNumber;
                   // CurrentMobileNumber = "00" + CurrentMobileNumber;
                    String url = Constants.GaZTVerifyMobileNumber + "Langz='" + Lang + "',Tin='" + Tin + "',Otp='" + "" + "',CurrEmail='" + "" + "',NewEmail='" + "" +"',MobileCountry='" + mobileCountry + "',CurrMobile='" + CurrentMobileNumber +
                        "',NewMobile='" + NewMobileNumber + "',CurrPwd='" + "" + "',NewPwd='" + "')?$format=json&saml2=enabled&sap-language=" + Lang;
                    var uri = new Uri(url);

                    //client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    //client.DefaultRequestHeaders.Add("Accept", "application/json");

                    client.DefaultRequestHeaders.Add("Token", "123");

                    HttpResponseMessage GAZTValidateMobileNumberResponse = await client.GetAsync(uri);
                    if (GAZTValidateMobileNumberResponse != null)
                    {
                        if (GAZTValidateMobileNumberResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return false;
                        }

                        HttpHeaders headers = GAZTValidateMobileNumberResponse.Headers;
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
                                return false;
                            }
                            App.Token = NewToken;
                        }
                        String GAZTValidateMobileNumberResponseJSON = GAZTValidateMobileNumberResponse.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(GAZTValidateMobileNumberResponseJSON))
                        {
                            if (!string.IsNullOrEmpty(GAZTValidateMobileNumberResponseJSON))
                            {
                                GAZTValidateMobileNumberResponseJSON = JObject.Parse(GAZTValidateMobileNumberResponseJSON)["d"].ToString();
                                JToken GAZTValidateMobileNumberResponseJToken = JObject.Parse(GAZTValidateMobileNumberResponseJSON)["Result"];
                                if ((0 == String.Compare(GAZTValidateMobileNumberResponseJToken.Value<String>(), "Email and Mobile login code has been sent successfully")) || (GAZTValidateMobileNumberResponseJToken.ToString() == "رمز تحقق الدخول للبريد الالكتروني والهاتف الجوال تم ارسالها بنجاح"))
                                {
                                    result = true;
                                }
                                else
                                {
                                    throw new Exception(AppResources.EnterValidMobileNumber);
                                }
                            }
                            else
                            {
                                throw new Exception(AppResources.NetworkConnectivityIssue);
                            }
                        }
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    if (string.Equals(ex.Message, AppResources.EnterValidMobileNumber))
                    {
                        throw new Exception(AppResources.EnterValidMobileNumber);
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
        //done internet exception handling
        public static async Task<bool> GAZTValidateAndChangePassword(String Lang, String Tin, string CurrentPassword, string NewPassword)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                bool result = false;
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.GAZTValidateAndChangePassword + "Langz='" + Lang + "',Tin='" + Tin + "',Otp='" + "" + "',CurrEmail='" + "" + "',NewEmail='" + ""+"',MobileCountry='"+"" + "',CurrMobile='" + "" + "',NewMobile='" + "" + "',CurrPwd='" + CurrentPassword + "',NewPwd='" + NewPassword + "')?$format=json&saml2=enabled&sap-language=" + Lang;
                    var uri = new Uri(url);
                    ////client.DefaultRequestHeaders.Add("Token", App.Token);
                    HttpResponseMessage GAZTValidateAndChangePasswordResponse = await client.GetAsync(uri);
                    if (GAZTValidateAndChangePasswordResponse != null)
                    {
                        if (GAZTValidateAndChangePasswordResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return false;
                        }

                        HttpHeaders headers = GAZTValidateAndChangePasswordResponse.Headers;
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
                                return false;
                            }
                            App.Token = NewToken;
                        }
                        String GAZTValidateAndChangePasswordResponseJSON = GAZTValidateAndChangePasswordResponse.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(GAZTValidateAndChangePasswordResponseJSON))
                        {
                            GAZTValidateAndChangePasswordResponseJSON = JObject.Parse(GAZTValidateAndChangePasswordResponseJSON)["d"].ToString();
                            JToken GAZTValidateAndChangePasswordResponseJToken = JObject.Parse(GAZTValidateAndChangePasswordResponseJSON)["Result"];
                            if ((0 == String.Compare(GAZTValidateAndChangePasswordResponseJToken.Value<String>(), "Password Changed Successfully")) || (0 == String.Compare(GAZTValidateAndChangePasswordResponseJToken.Value<String>(), "تم تغيير كلمة المرور بنجاح")))
                            {
                                result = true;
                            }
                            else if ((0 == String.Compare(GAZTValidateAndChangePasswordResponseJToken.Value<String>(), "Please enter the correct old Password.")) || (0 == String.Compare(GAZTValidateAndChangePasswordResponseJToken.Value<String>(), "كلمة المرور القديمة المدخلة غير صحيحة") || GAZTValidateAndChangePasswordResponseJToken.Value<String>().Contains("كلمة المرور القديمة المدخلة غير صحيحة")))
                            {
                                ErrorMessage = GAZTValidateAndChangePasswordResponseJToken.Value<String>();
                            }
                            else
                            {
                                throw new ArgumentException(AppResources.ZZPasswordGuideLineTextNew);
                            }
                        }
                    }
                    else
                    {
                        throw new ArgumentException(AppResources.NetworkConnectivityIssue);
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    if (string.Equals(ex.Message, AppResources.NetworkConnectivityIssue))
                    {
                        throw new Exception(AppResources.NetworkConnectivityIssue);
                    }
                    else
                    {
                        throw new Exception(AppResources.ZZPasswordGuideLineTextNew);
                    }
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }
        //done internet exception handling
        public static async Task<string> GAZTGetPdfUrl(String Lang, String Tin)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                TaxPayerProfile TP = null;
                bool result = false;
                string PdfUrl = string.Empty;
                DateTime dt = DateTime.Now;
                string currentDate = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString() + "T" + dt.Hour.ToString() + ":" + dt.Minute.ToString();
                // 2007 - 01 - 01T00: 00
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    // String url = "https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/ZDP_IT_CORRES_MOB_NEW_SRV/Corr_detSet?$filter=Gpartz  eq  '3300057436'  and Langz   eq 'EN'  and  Begdaz eq   datetime'2007-01-01T00:00'  and Enddaz eq datetime'2019-10-13T11:12'  and  ObligFlagz eq 'I'  and  Auditor  eq  ''   and  TaxtpFg  eq  'VAT'  and  UserTin   eq  ''&$format=json";
                    String url = Constants.GAZTGetPdf + "Gpartz eq'" + Tin + "'and Langz eq'" + Lang + "'and  Begdaz eq datetime'" + "2007-01-01T00:00" + "'and Enddaz eq datetime'" + currentDate + "'and  ObligFlagz eq'" + "I" + "'and  Auditor  eq'" + "" + "'and  TaxtpFg  eq '" + "VAT" + "'and  UserTin   eq'" + "" + "'&saml2=enabled&$format=json";
                    var uri = new Uri(url);
                    ////client.DefaultRequestHeaders.Add("Token", App.Token);
                    HttpResponseMessage GAZTValidateAndChangePasswordResponse = await client.GetAsync(uri);
                    if (GAZTValidateAndChangePasswordResponse != null)
                    {
                        String GAZTValidateAndChangePasswordResponseJSON = GAZTValidateAndChangePasswordResponse.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(GAZTValidateAndChangePasswordResponseJSON))
                        {
                            GAZTValidateAndChangePasswordResponseJSON = JObject.Parse(GAZTValidateAndChangePasswordResponseJSON)["d"].ToString();
                            JObject jObject = JObject.Parse(GAZTValidateAndChangePasswordResponseJSON);
                            if (jObject != null)
                            {
                                if (GAZTValidateAndChangePasswordResponseJSON.Contains("Pdfurl"))
                                {
                                    JToken memberName = jObject["results"].First["Pdfurl"];
                                    result = true;
                                    PdfUrl = memberName.ToString();
                                }
                                else
                                {
                                    return null;
                                }
                            }
                        }
                    }
                    return PdfUrl;
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
        //done internet exception handling
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
                    String url = Constants.GetMyBills + "Fbguid eq '" + "'and Euser eq '" + Tin + "'" + "&saml2=enabled&$format=json&sap-language=" + lang;
                    ////client.DefaultRequestHeaders.Add("Token", App.Token);
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
        //done internet exception handling
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
                catch (Exception ex)
                {
                    App.IsSessionExpired = true;
                    return null;
                    if (string.Equals(ex.Message, AppResources.ZNoICRAvailable))
                    {
                        throw new Exception(AppResources.ZNoICRAvailable);
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
        //done internet exception handling
        public static async Task<string> GAZTGetTaxPayerProfile(String Tin, String Lang)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                String MobileNumber = string.Empty;
                string PdfUrl = string.Empty;
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.GAZTGetTP + "='" + Tin + "',Langz='" + Lang + "')" + "?&$expand=TPOC_LIST&saml2=enabled&$format=json";

                    client.DefaultRequestHeaders.Add("Token", App.Token);

                    var uri = new Uri(url);
                    HttpResponseMessage GAZTValidateAndChangePasswordResponse = await client.GetAsync(uri);
                    if (GAZTValidateAndChangePasswordResponse != null)
                    {
                        if (GAZTValidateAndChangePasswordResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTValidateAndChangePasswordResponse.Headers;
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
                        String GAZTValidateAndChangePasswordResponseJSON = GAZTValidateAndChangePasswordResponse.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(GAZTValidateAndChangePasswordResponseJSON))
                        {
                            GAZTValidateAndChangePasswordResponseJSON = JObject.Parse(GAZTValidateAndChangePasswordResponseJSON)["d"].ToString();
                            string GAZTValidateOTPResponseJToken = JObject.Parse(GAZTValidateAndChangePasswordResponseJSON)["Mobile"].ToString();
                            if (string.IsNullOrEmpty(GAZTValidateOTPResponseJToken) != true)
                            {
                                MobileNumber = GAZTValidateOTPResponseJToken;
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
                    return MobileNumber;
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
        //done internet exception handling
        public static async Task<bool> GAZTGetOTPForEmail(String Lang, String Tin, string CurrentEmail, string NewEmail)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                bool result = false;
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.GAZTGetOTPForEmail + "Langz='" + Lang + "',Tin='" + Tin + "',Otp='" + "" + "',CurrEmail='" + CurrentEmail + "',NewEmail='" + NewEmail +"',MobileCountry='"+""+ "',CurrMobile='" + "" + "',NewMobile='" + "" + "',CurrPwd='" + "" + "',NewPwd='" + "')?$format=json&saml2=enabled&sap-language=" + Lang;
                    var uri = new Uri(url);
                    //client.DefaultRequestHeaders.Add("Token", "123");
                    HttpResponseMessage GAZTValidateOTPResponse = await client.GetAsync(uri);
                    if (GAZTValidateOTPResponse != null)
                    {
                        if (GAZTValidateOTPResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return false;
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
                                return false;
                            }
                            App.Token = NewToken;
                        }
                        String GAZTValidateOTPResponseJSON = GAZTValidateOTPResponse.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(GAZTValidateOTPResponseJSON))
                        {
                            GAZTValidateOTPResponseJSON = JObject.Parse(GAZTValidateOTPResponseJSON)["d"].ToString();
                            string GAZTValidateOTPResponseJToken = JObject.Parse(GAZTValidateOTPResponseJSON)["Result"].ToString();
                            if (GAZTValidateOTPResponseJToken == "Email and Mobile login code has been sent successfully" || GAZTValidateOTPResponseJToken.ToString() == "رمز تحقق الدخول للبريد الالكتروني والهاتف الجوال تم ارسالها بنجاح")
                            {
                                result = true;
                                //TP = JsonConvert.DeserializeObject<TaxPayerProfile>(GAZTValidateOTPResponseJSON);
                            }
                            else
                            {
                                throw new Exception(AppResources.NDNewEmailCannotBeSameAsOldEmail);
                            }
                        }
                        else
                        {
                            throw new Exception(AppResources.NetworkConnectivityIssue);
                        }
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    if (string.Equals(ex.Message, AppResources.InvalidEmail))
                    {
                        throw new Exception(AppResources.InvalidEmail);
                    }
                    else if(string.Equals(ex.Message, AppResources.NDNewEmailCannotBeSameAsOldEmail))
                    {
                        throw new Exception(AppResources.NDNewEmailCannotBeSameAsOldEmail);
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
        //done internet exception handling
        public static async Task<TaxPayerProfile> GAZTValidateOTPForEmail(String Lang, String OTP, String Tin, string CurrentEmail, string NewEmail, string CurrentPassword, string NewPassword)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                TaxPayerProfile TP = null;
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    ////client.DefaultRequestHeaders.Add("Token", App.Token);
                    String url = Constants.GAZTValidateOTPForEmail + "Langz='" + Lang + "',Tin='" + Tin + "',Otp='" + OTP + "',CurrEmail='" + CurrentEmail +"',MobileCountry='"+""+ "',NewEmail='" + NewEmail + "',CurrMobile='" + "" + "',NewMobile='" + "" + "',CurrPwd='" + CurrentPassword + "',NewPwd='" + NewPassword + "')?$format=json&saml2=enabled&sap-language=" + Lang;
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
                        String GAZTValidateOTPResponseJSON = GAZTValidateOTPResponse.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(GAZTValidateOTPResponseJSON))
                        {
                            GAZTValidateOTPResponseJSON = JObject.Parse(GAZTValidateOTPResponseJSON)["d"].ToString();
                            string GAZTValidateOTPResponseJToken = JObject.Parse(GAZTValidateOTPResponseJSON)["Result"].ToString();
                            if (GAZTValidateOTPResponseJToken == "Details Changed Successfully" || GAZTValidateOTPResponseJToken.ToString() == "تم تغيير التفاصيل بنجاح")
                            {
                                TP = JsonConvert.DeserializeObject<TaxPayerProfile>(GAZTValidateOTPResponseJSON);
                            }
                            else
                            {
                                throw new Exception(AppResources.Invalidverificationcodeentered);
                            }
                        }
                        else
                        {
                        }
                    }
                    return TP;
                }
                catch (Exception ex)
                {
                    if (string.Equals(ex.Message, AppResources.Invalidverificationcodeentered))
                    {
                        throw new Exception(AppResources.Invalidverificationcodeentered);
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
        //done internet exception handling
        public static async Task<string> GAZTZakatGetPdfUrl(String Lang, String Tin)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                TaxPayerProfile TP = null;
                bool result = false;
                string PdfUrl = string.Empty;
                DateTime dt = DateTime.Now;
                string currentDate = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString() + "T" + dt.Hour.ToString() + ":" + dt.Minute.ToString();
                // 2007 - 01 - 01T00: 00
                try
                {
                    // Tin = "3300014611";
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    //String url = Constants.GAZTGetPdf + "Gpartz eq'" + Tin + "'and Langz eq'" + Lang + "'and  Begdaz eq datetime'" + "2007-01-01T00:00" + "'and Enddaz eq datetime'" + currentDate + "'and  ObligFlagz eq'" + "I" + "'and  Auditor  eq'" + "" + "'and  TaxtpFg  eq '" + "VAT" + "'and  UserTin   eq'" + "" + "'&$format=json";
                    string ZakatURL = Constants.GAZTZakatGetPdf + Tin + "'and Langz eq'" + Lang + "'and UserTin eq'" + "" + "'and Begdaz eq datetime'" + "2007-01-01T00:00" + "'and Enddaz eq datetime'" + currentDate + "'and ObligFlagz eq'" + "" + "'and Auditor eq '" + "" + "'&sap-client=100&sap-language='" + Lang + "'&saml2=enabled&$format=json";
                    var uri = new Uri(ZakatURL);
                    ////client.DefaultRequestHeaders.Add("Token", App.Token);
                    HttpResponseMessage GAZTValidateAndChangePasswordResponse = await client.GetAsync(uri);
                    if (GAZTValidateAndChangePasswordResponse != null)
                    {
                        String GAZTValidateAndChangePasswordResponseJSON = GAZTValidateAndChangePasswordResponse.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(GAZTValidateAndChangePasswordResponseJSON))
                        {
                            ZakatCertificate zakatCertificate = new ZakatCertificate();
                            GAZTValidateAndChangePasswordResponseJSON = JObject.Parse(GAZTValidateAndChangePasswordResponseJSON)["d"].ToString();
                            zakatCertificate = JsonConvert.DeserializeObject<ZakatCertificate>(GAZTValidateAndChangePasswordResponseJSON);
                            JObject jObject = JObject.Parse(GAZTValidateAndChangePasswordResponseJSON);
                            // string s =
                            //jObject JsonConvert.DeserializeObject<string>(jObject);
                            if (jObject != null)
                            {
                                if (GAZTValidateAndChangePasswordResponseJSON.Contains("Pdfurl"))
                                {
                                    JToken memberName = jObject["results"].First["Pdfurl"];
                                    result = true;
                                    PdfUrl = memberName.ToString();
                                }
                                else
                                {
                                    return null;
                                }
                            }
                        }
                    }
                    return PdfUrl;
                }
                catch (Exception ex)
                {
                    //  throw new Exception(ex.Message);
                    return null;
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }
        //done internet exception handling
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
                    // string uri = "https://10.50.15.51:8080/sap/opu/odata/SAP/ZDP_IT_CORR_MOOB_SRV/headerSet(Gpartz='" + Tin +"'" + ",Langz='"+ Lang + "'" + ",Begdaz=datetime'2007-01-01T00%3A00%3A00',Enddaz=datetime'2019-11-19T00%3A00%3A00')?&$expand=ZakatSet,VATSet,ExciseSet&saml2=disabled&$format=json";
                    string uri = Constants.GetAllCertificate + Tin + "'" + ",Langz='" + Lang + "'" + ",Begdaz=datetime'" + "2007-01-01T00%3A00%3A00'" + ",Enddaz=datetime'" + currentDate + "'" + ")?&$expand=ZakatSet,VATSet,ExciseSet&saml2=enabled&$format=json";
                    ////client.DefaultRequestHeaders.Add("Token", App.Token);
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
        public static async Task<ForgotPasswordOTP> GAZTFogotPasswordSendOTP(String Lang, String Tin)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                DateTime dt = DateTime.Now;
                ForgotPasswordOTP forgotPasswordOTP = new ForgotPasswordOTP();
                char lang = GetLangZParameter();
                string NewToken = string.Empty;
                try
                {
                    try
                    {
                        App.httpClientHandler.CookieContainer = null;
                    }
                    catch (Exception ex)
                    {

                    }

                    HttpClient client = new HttpClient(App.httpClientHandler);
                    // string uri = "https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/ZDP_FRGT_USRNM_PWD_SRV/HeaderSet(Tin='3102164652',EmailId='',TpType='',MobileNo='',SubType='',Idnumber='',Otp='',NewPwd='',RdBt='P',Dob=datetime'2015-07-05T15:13:49',Langu='E')?Saml2=disabled&$format=json";
                    string uri = Constants.FogotPasswordSendOTP + Tin + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "" + "'" + ",MobileNo='" + "" + "'" + ",SubType='" + "" + "'" + ",Idnumber='" + "" + "'" + ",Otp='" + "" + "'" + ",NewPwd='" + "" + "'" + ",RdBt='" + "P'" + ",Dob=datetime'" + "2015-07-05T15:13:49" + "'" + ",Langu='" + lang + "'" + ")?saml2=enabled&$format=json";
                    HttpResponseMessage GAZTFogotPasswordSendOTPResponse = await client.GetAsync(uri);
                    if (GAZTFogotPasswordSendOTPResponse != null)
                    {
                        HttpHeaders headers = GAZTFogotPasswordSendOTPResponse.Headers;
                        String GAZTGetSendOTPResponseJSON = GAZTFogotPasswordSendOTPResponse.Content.ReadAsStringAsync().Result;
                        forgotPasswordOTP = JsonConvert.DeserializeObject<ForgotPasswordOTP>(GAZTGetSendOTPResponseJSON);
                    }
                    return forgotPasswordOTP;
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
        //done internet exception handling
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
                    catch (Exception ex)
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
        //done internet exception handling
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
        //done internet exception handling
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
                    catch (Exception ex)
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
        //done internet exception handling
        public static async Task<TINStatus> GAZTGetTinStatus(string lang, string Tin)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                TINStatus tINStatus = new TINStatus();
                string NewToken = string.Empty;
                try
                {
                    string _language = null;
                    if (App.IsArabic)
                        _language = "A";
                    else
                        _language = "E";
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.GetTinStatus + _language + "',Tin='" + Tin + "" + "'" + ")?saml2=enabled&sap-language=’" + lang + "" + "'" + "&$expand=ItemSet&$format=json";

                    ////client.DefaultRequestHeaders.Add("Token", App.Token);

                    // client.DefaultRequestHeaders.Add("Token", App.Token);

                    var uri = new Uri(url);
                    HttpResponseMessage GAZTTinStatus = await client.GetAsync(uri);
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
        //done internet exception handling
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
                    HttpResponseMessage GAZTVATLookUp = await client.GetAsync(uri);
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
        }
        //done internet exception handling
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
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    // String url = "https://sapgatewayqa.gazt.gov.sa:443/sap/opu/odata/SAP/    ZDP_VATR_M_SRV/HDRSet(Periodkeyz='',Fbnumz='',Langz='E',Officerz='',Gpartz='3100032587',Euser='3100032587',Fbguid='005056B1F8FB1EEA8EEEAA379984A7B3')?saml2=disabled&$expand=ADRSet,ATTACHSet,CFSet,IBANSet,NOTESSet,VATR_MSGSet";
                    // String url = Constants.GAZTGetAllVATDeclarationReturnData + "" + "'" + ",Fbnumz='" + "" + "'" + ",Langz='" + Lang + "'" + ",Officerz='" + "MB" + "'" + ",Gpartz='" + App.TP.Tin + "'" + ",Euser='" + EUser + "'" + ",Fbguid='" + Fbguid + "',SrcAppz='MB'" + ")?saml2=disabled&sap-language=" + Lang + "&$expand=ADRSet,ATTACHSet,CFSet,IBANSet,NOTESSet,VATR_MSGSet&$format=json";
                    // String url = Constants.GAZTGetAllVATDeclarationReturnData + "" + "'" + ",Fbnumz='" + "" + "'" + ",Langz='" + Lang + "'" + ",Officerz='" + "MB" + "'" + ",Gpartz='" + App.TP.Tin + "'" + ",Euser='" + EUser + "'" + ",Fbguid='" + Fbguid + "')?saml2=disabled&sap-language=" + Lang + "&$expand=ADRSet,ATTACHSet,CFSet,IBANSet,NOTESSet,VATR_MSGSet&$format=json";

                    String url = Constants.GAZTGetAllVATDeclarationReturnData + "" + "'" + ",Fbnumz='" + "" + "'" + ",Langz='" + Lang + "'" + ",Officerz='" + "" + "'" + ",Gpartz='" + App.TP.Tin + "'" + ",Euser='" + EUser + "'" + ",Fbguid='" + Fbguid + "'" + ")?saml2=enabled&sap-language=" + Lang + "&$expand=ADRSet,ATTACHSet,CFSet,IBANSet,NOTESSet,VATR_MSGSet,VATPERITEMSet&$format=json";


                    //String url = Constants.GAZTGetAllVATDeclarationReturnData + "" + "'" + ",Fbnumz='" + "" + "'" + ",Langz='" + Lang + "'"  + "'" + ",Gpartz='" + App.TP.Tin + "'" + ",Euser='" + EUser + "'" + ",Fbguid='" + Fbguid + "'" + ")?saml2=enabled&sap-language=" + Lang + "&$expand=ADRSet,ATTACHSet,CFSet,IBANSet,NOTESSet,VATR_MSGSet&$format=json";
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
        private static char GetLangZParameter()
        {
            if (App.IsArabic)
                return 'A';
            else
                return 'E';
        }
        private static String GetLangZParameterAREN()
        {
            if (App.IsArabic)
                return "AR";
            else
                return "EN";
        }
        //done internet exception handling
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
                            //RequestVATDeclaration.d.SubmitFg = "";
                            ATTACHSet aTTACHSet = new ATTACHSet();
                            aTTACHSet.results = new List<Attachment>();
                            RequestVATDeclaration.d.ATTACHSet = aTTACHSet;
                        }
                        char LangZ = GetLangZParameter();
                        string lang = UtilityManager.GetLanguageParameter();
                        String url = Constants.SaveVATDeclarationData;
                        vATDeclaration.d.Langz = lang;
                        // String url = "https://sapgatewayqa.gazt.gov.sa:443/sap/opu/odata/SAP/ZDP_VATR_M_SRV/HDRSet?&saml2=disabled";// Constants.SaveVATDeclarationData;
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
                                ErrorMessageForVAT += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = ErrorMessageForVAT.Replace("An exception was raised", string.Empty);
                                ErrorMessageForVAT = WithReplacedString;
                                //ErrorMessageForVAT
                            }
                        }
                        return _vATDeclarationD;
                    }
                    return _vATDeclarationD;
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
        //done internet exception handling
        public static async Task<VATDeclarationD> GAZTGetSavedData(string lang, string IdType, String IdNumber)
        {
            string NewToken = string.Empty;
            try
            {
                VATDeclarationD vATLookUp = new VATDeclarationD();
                HttpClient client = new HttpClient(App.httpClientHandler);
                String url = Constants.SaveVATDeclarationData;
                ////client.DefaultRequestHeaders.Add("Token", App.Token);
                var uri = new Uri(url);
                HttpResponseMessage GAZTVATLookUp = await client.GetAsync(uri);
                if (GAZTVATLookUp != null)
                {
                    if (GAZTVATLookUp.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        App.IsSessionExpired = true;
                        return null;
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
                            App.IsSessionExpired = true;
                            return null;
                        }
                        App.Token = NewToken;
                    }
                    String TINStatusResponse = GAZTVATLookUp.Content.ReadAsStringAsync().Result;
                    vATLookUp = JsonConvert.DeserializeObject<VATDeclarationD>(TINStatusResponse);
                }
                return vATLookUp;
            }
            catch (Exception ex)
            {
                return null;
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
                    string url = "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/Z_REG_DROPDOWN_SRV/CountryCodeSet?$filter=Spras" + " eq " + "'" + lang + "'" + " &$format=json";
                    //String url = "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/Z_REG_DROPDOWN_SRV/CountryCodeSet?$filter=Spras%20eq%20%27AR%27&$format=json";
                    //String url = Constants.GAZTGetVATRegistrationData + "',PortalUsrz='" + "',Langz='" + lang + "',Officerz='" + "',Gpartz='" + App.LoginDataRetrieved.TIN + "',TxnTpz='" + "04" + "',Euser='" + "" + "',Fbguid='" + "" + "'" + ")?&$expand=ADDRESSSet,IBANSet,ATTDETSet,CONTACT_PERSONSet,CONTACTDTSet,NOTESSet,QUESTIONSSet,QUESLISTSet,QUESCONFIG_MSet,ELGBL_DOCSet&$format=json";
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
                        // String InternationalMobileData = GAZTVATRegistrationDataResponse.Content.ReadAsStringAsync().Result;

                        //internationalCodes = JsonConvert.DeserializeObject<InternationalMobileData>(InternationalMobileData);

                        String GAZTInternationalNumberResponseJSON = GAZTInternationalMobileNumDataResponse.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(GAZTInternationalNumberResponseJSON))
                        {
                            GAZTInternationalNumberResponseJSON = JObject.Parse(GAZTInternationalNumberResponseJSON)["d"].ToString();
                            string GAZTInternationalNumberResponseJSONJToken = JObject.Parse(GAZTInternationalNumberResponseJSON)["results"].ToString();
                            if (string.IsNullOrEmpty(GAZTInternationalNumberResponseJSONJToken) != true)
                            {
                                internationalCodes = JsonConvert.DeserializeObject<ObservableCollection<InternationalMobileData>>(GAZTInternationalNumberResponseJSONJToken);
                                //internationalCodes = JsonConvert.DeserializeObject<InternationalMobileData>(GAZTMyBillsResponseJSONJToken);
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
        //done internet exception handling
        public static async Task<List<IBANIDNumber>> GAZTGetIBANIdNumber(string IBANType)
        {
            List<IBANIDNumber> iBANIDNumbers = new List<IBANIDNumber>();
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    string lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    ////client.DefaultRequestHeaders.Add("Token", App.Token);
                    //String url = Constants.GAZTValidateOTPForEmail + "Langz='" + Lang + "',Tin='" + Tin + "',Otp='" + OTP + "',CurrEmail='" + CurrentEmail + "',NewEmail='" + NewEmail + "',CurrMobile='" + "" + "',NewMobile='" + "" + "',CurrPwd='" + CurrentPassword + "',NewPwd='" + NewPassword + "')?$format=json&saml2=disabled&sap-language=" + Lang;
                    String url = Constants.GAZTGetIdNumber + App.TP.Tin + "'" + "and Type eq '" + IBANType + "'" + "&saml2=enabled&sap-langauge='" + lang + "'&$format=json";//https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VATR_UH_SRV/UI_HDRSet(Fbnum='',Lang='E',Operation='',Gpart='3100032587',Status='E0001',TxnTp='VTR_ASMT',Formproc='',Periodkey='18JU')?saml2=disabled&$expand=IBANSet,IGRTSet,ITUDSet,UI_BTNSet,VATRSet,VTTHSet&$format=json";
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
                            IBANIdNumber = JObject.Parse(IBANIdNumber)["d"].ToString();
                            IBANIdNumber = JObject.Parse(IBANIdNumber)["results"].ToString();
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
        //done internet exception handling
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
                    ////client.DefaultRequestHeaders.Add("Token", App.Token);
                    //String url = Constants.GAZTValidateOTPForEmail + "Langz='" + Lang + "',Tin='" + Tin + "',Otp='" + OTP + "',CurrEmail='" + CurrentEmail + "',NewEmail='" + NewEmail + "',CurrMobile='" + "" + "',NewMobile='" + "" + "',CurrPwd='" + CurrentPassword + "',NewPwd='" + NewPassword + "')?$format=json&saml2=disabled&sap-language=" + Lang;
                    String url = Constants.GAZTCheckIBANNumber + IBAN + "')" + "?saml2=enabled&sap-langauge=" + lang + "&$format=json";//https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VATR_UH_SRV/UI_HDRSet(Fbnum='',Lang='E',Operation='',Gpart='3100032587',Status='E0001',TxnTp='VTR_ASMT',Formproc='',Periodkey='18JU')?saml2=disabled&$expand=IBANSet,IGRTSet,ITUDSet,UI_BTNSet,VATRSet,VTTHSet&$format=json";
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
                        catch (Exception e)
                        {
                            return null;
                        }
                    }
                    return IbanNumber;
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
        //done internet exception handling
        public static async Task<VATCalculationData> GAZTGetVATDeclaratinCalculationData(string periodKey, string TxnTp, string status, string FormBundleNumber, string Gpart)
        {
            VATCalculationData vATCalculationData = new VATCalculationData();
            if (CrossConnectivity.Current.IsConnected)
            {
                TaxPayerProfile TP = null;
                string NewToken = string.Empty;
                try
                {
                    char lang = GetLangZParameter();// "E";
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    ////client.DefaultRequestHeaders.Add("Token", App.Token);
                    //String url = Constants.GAZTValidateOTPForEmail + "Langz='" + Lang + "',Tin='" + Tin + "',Otp='" + OTP + "',CurrEmail='" + CurrentEmail + "',NewEmail='" + NewEmail + "',CurrMobile='" + "" + "',NewMobile='" + "" + "',CurrPwd='" + CurrentPassword + "',NewPwd='" + NewPassword + "')?$format=json&saml2=disabled&sap-language=" + Lang;
                    String url = Constants.GAZTGetVATDeclarationCalculationDataUrl + "'" + FormBundleNumber + "'" + ",Lang='" + lang + "'" + ",Operation='" + "'" + ",Gpart='" + Gpart + "'" + ",Status='" + status + "'" + ",TxnTp='" + TxnTp + "'" + ",Formproc='" + "'" + ",Periodkey='" + periodKey + "'" + ")?saml2=enabled&$expand=IBANSet,IGRTSet,ITUDSet,UI_BTNSet,VATRSet,VTTHSet&$format=json";//https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VATR_UH_SRV/UI_HDRSet(Fbnum='',Lang='E',Operation='',Gpart='3100032587',Status='E0001',TxnTp='VTR_ASMT',Formproc='',Periodkey='18JU')?saml2=disabled&$expand=IBANSet,IGRTSet,ITUDSet,UI_BTNSet,VATRSet,VTTHSet&$format=json";
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
                    ByteArrayContent baContent = new ByteArrayContent(AttachmentByte);
                    if (!string.IsNullOrEmpty(contentType))
                        baContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                    var response = await client.PostAsync(url, baContent);
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
        //Seesion expired handled  
        //done internet exception handling
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
                    // String url = "https://sapgatewayqa.gazt.gov.sa:443/sap/opu/odata/SAP/ZDP_INDTAX_ATT_SRV/AttachSet(OutletRef='',RetGuid='005056B1F8FB1EDA8FF041CFF05E83A9',Flag='N',Dotyp='VTA0',SchGuid='',Srno=1,Doguid='',AttBy='TP')/AttachMedSet";// Constants.SaveVATDeclarationData;
                    String url = Constants.GAZTDeteleAttachment + "'" + "'" + ",RetGuid='undefined'" + ",Flag='" + "N" + "'" + ",Dotyp='" + Dotyp + "'" + ",SchGuid='" + "'" + ",Srno=" + "1" + ",Doguid='" + RetGuid + "'" + ",AttBy='" + AttBy + "'" + ")/$value?saml2=enabled"; //",RetGuid='005056B1F8FB1EDA8FF041CFF05E83A9',Flag='N',Dotyp='VTA0',SchGuid='',Srno=1,Doguid='',AttBy='TP')/AttachMedSet";// Constants.SaveVATDeclarationData;
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
        //Seesion expired handled  
        //done internet exception handling
        public static async Task<SadadNumber> GAZTGetVATDeclarationSADADNumber(string FormBundleID)//, string returnedFguid
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    SadadNumber sadadNumber = new SadadNumber();
                    char LangZ = GetLangZParameter();
                    string lang = UtilityManager.GetLanguageParameter();
                    String url = Constants.GAZTGetSADADNumber + lang + "'" + "&$format=json&$filter=Langu eq'" + LangZ + "'and Fbnum eq '" + FormBundleID + "'" + "";
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var uri = new Uri(url);
                    client.DefaultRequestHeaders.Add("Token", "123");
                    var response = await client.GetAsync(url);
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
        //Seesion expired handled  
        //done internet exception handling
        public static async Task<EstimatedZakatReturns> GAZTGetEstimateZakatReturnList()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                EstimatedZakatReturns zAKATICRList = new EstimatedZakatReturns();
                string NewToken = string.Empty;
                try
                {
                    string _language = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.GAZTGetZakatReturnList + App.TP.Userid + "'" + ",Auditor='" + "'" + ",Lang='" + _language + "'" + ",UserTin='" + App.TP.Userid + "'" + ")?saml2=enabled&sap-language='" + _language + "'" + "&$expand=listSet&$format=json";
                    ////client.DefaultRequestHeaders.Add("Token", App.Token);
                    client.DefaultRequestHeaders.Add("Token", "123");

                    var uri = new Uri(url);
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
                        zAKATICRList = JsonConvert.DeserializeObject<EstimatedZakatReturns>(EstimateZakatReturnList);
                    }
                    return zAKATICRList;// tINStatus;
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

        //Seesion expired handled  
        //done internet exception handling
        public static async Task<VATDeclaration> GAZTSetVATReturnVoid(VATDeclaration vATDeclaration)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                VATDeclaration RequestVATDeclaration = null;
                try
                {
                    RequestVATDeclaration = await SaveVATDeclarationData(vATDeclaration);
                }
                catch (Exception ex)
                {
                }
                return RequestVATDeclaration;
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }
        //Seesion expired handled  
        //done internet exception handling
        public static async Task<VATDeclaration> GAZTSetVATReturnReset(VATDeclaration vATDeclaration)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                VATDeclaration RequestVATDeclaration = null;
                try
                {
                    RequestVATDeclaration = await SaveVATDeclarationData(vATDeclaration);
                }
                catch (Exception ex)
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
            catch (Exception ex)
            {
            }
            return RequestVATDeclaration;
        }
        //Seesion expired handled 
        //done internet exception handling
        public static async Task<ZakatReturnDetails> GAZTGetZAKATReturn(string fbguid)
        {
            ZakatReturnDetails zakatReturnDetails = new ZakatReturnDetails();
            if (CrossConnectivity.Current.IsConnected)
            {
                TaxPayerProfile TP = null;
                string NewToken = string.Empty;
                try
                {
                    char lang = GetLangZParameter();// "E";
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    client.DefaultRequestHeaders.Add("Token", "123");

                    // String url = Constants.GAZTGetZakatReturn + "'" + ",Langz='" + lang + "'" + ",Gpartz='" + App.TP.Userid + "'" + ",Euser='" + App.TP.Userid + "'" + ",Fbguid='" + fbguid + "'" + ",Invflg='" + "'" + ",Fsource='" + "TP" + "'" + ")?saml2=enabled&sap-language='" + lang + "'&$expand=ReasonSet,AttachSet,ThresholdSet,InvoiceSet&$format=json";
                    String url = "";

                    //When not in production we need to comment the code
                    //This logic is added:
                    // For Production as per Vinay
                    if (App.IsZakatLoadingFromMyReturns == true)
                    {
                        url = Constants.GAZTGetZakatReturn + "'" + ",Langz='" + lang + "'" + ",Gpartz='" + App.TP.Tin + "'" + ",Euser='" + App.TP.Tin + "'" + ",Fbguid='" + fbguid + "'" + ",Invflg='" + "'" + ",Fsource='" + "TP" + "'" + ")?saml2=enabled&sap-language='" + lang + "'&$expand=ReasonSet,AttachSet,ThresholdSet,InvoiceSet&$format=json";
                    }
                    else
                    {
                        url = Constants.GAZTGetZakatReturn + "'" + ",Langz='" + lang + "'" + ",Gpartz='" + App.TP.Tin + "'" + ",Euser='" + "'" + ",Fbguid='" + fbguid + "'" + ",Invflg='" + "'" + ",Fsource='" + "TP" + "'" + ")?saml2=enabled&sap-language='" + lang + "'&$expand=ReasonSet,AttachSet,ThresholdSet,InvoiceSet&$format=json";
                    }

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
        //Seesion expired handled 
        //done internet exception handling
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
                    // String url = "https://sapgatewayqa.gazt.gov.sa:443/sap/opu/odata/SAP/ZDP_INDTAX_ATT_SRV/AttachSet(OutletRef='',RetGuid='005056B1F8FB1EDA8FF041CFF05E83A9',Flag='N',Dotyp='VTA0',SchGuid='',Srno=1,Doguid='',AttBy='TP')/AttachMedSet";// Constants.SaveVATDeclarationData;
                    String url = Constants.GAZTSaveEstimatedZaktReturn + LangZ;
                    // lang + "'" + "&$filter=Idtype eq " + IdType + ",RetGuid='" + RetGuid + "'" +
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var serilized = JsonConvert.SerializeObject(zakatReturnDetailsD);

                    client.DefaultRequestHeaders.Add("Token", App.Token);
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    HttpResponseMessage res = client.PostAsync(uri, contentPost).Result;
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
        //Seesion expired handled  
        //done internet exception handling
        public static async Task<List<ApplicableButton>> GAZTVATReturnGetApplicableButtons(string Fbnum, string Lang, string Operation, string Gpart, string Status, string TxnTp, string PeriodKey)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                //Fbnum = '65000004030',Lang = 'E',Operation = '',Gpart = '3000493862',Status = 'E0045',TxnTp = 'VTR_AMDT'
                List<ApplicableButton> VATApplicableButtons = new List<ApplicableButton>();
                try
                {
                    char LangZ = GetLangZParameter();
                    // https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VATR_UH_SRV/UI_HDRSet(Fbnum='65000004030',Lang='E',Operation='',Gpart='3000493862',Status='E0045',TxnTp='VTR_AMDT',Formproc='',Periodkey='18JA')?&$expand=UI_BTNSet&$format=json	
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.GAZTVATReturnGetApplicableButtons + "'" + Fbnum + "'" + ",Lang='" + LangZ + "'" + ",Operation='" + Operation + "'," + "Gpart=" + "'" + Gpart + "',Status='" + Status + "',TxnTp='" + TxnTp + "',Formproc='',Periodkey='" + PeriodKey + "'" + ")?saml2=enabled&$expand=UI_BTNSet,IGRTSet&$format=json";
                    ////client.DefaultRequestHeaders.Add("Token", App.Token);
                    var uri = new Uri(url);
                    HttpResponseMessage ApplicableButtonsResponse = await client.GetAsync(uri);
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
        public static async Task<EsimatedZAKATReturnsButtonSets> GAZTGetZAKATReturnButtonSet()
        {
            EsimatedZAKATReturnsButtonSets ZAKATReturnApplicableButtons = new EsimatedZAKATReturnsButtonSets();
            if (CrossConnectivity.Current.IsConnected)
            {
                TaxPayerProfile TP = null;
                string NewToken = string.Empty;
                try
                {
                    char lang = GetLangZParameter();// "E";
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    // ////client.DefaultRequestHeaders.Add("Token", App.Token);
                    //client.DefaultRequestHeaders.Add("Accept", "application/json");
                    //  String url = "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_FZ12_BS_SRV/UI_HdrSet(Fbtypz='FZ12',UserTypz='TP',Fbnum='94000001174',Gpart='',Status='E0003',TxnTp='',Formproc='FZ12',Lang='EN',Officer='')?&$expand=UI_BtnSet&$format=json&saml2=disabled";
                    String url = "";
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
                        String _esimatedZAKATReturnsButtonSets = GAZTValidateOTPResponse.Content.ReadAsStringAsync().Result;
                        ZAKATReturnApplicableButtons = JsonConvert.DeserializeObject<EsimatedZAKATReturnsButtonSets>(_esimatedZAKATReturnsButtonSets);
                    }
                    return ZAKATReturnApplicableButtons;
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
        //Seesion expired handled  
        //done internet exception handling
        public static async Task<AttachmentRootOject> GAZTSaveEstimatedZAKATAttachment(byte[] AttachmentByte, string fileName, string RetGuid, string Dotyp, string ContentType)//, string returnedFguid
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    AttachmentRootOject _attachment = new AttachmentRootOject();
                    char LangZ = GetLangZParameter();
                    //string Dotyp = "VTA0";
                    string AttBy = "TP";
                    // String url = "https://sapgatewayqa.gazt.gov.sa:443/sap/opu/odata/SAP/ZDP_INDTAX_ATT_SRV/AttachSet(OutletRef='',RetGuid='005056B1F8FB1EDA8FF041CFF05E83A9',Flag='N',Dotyp='VTA0',SchGuid='',Srno=1,Doguid='',AttBy='TP')/AttachMedSet";// Constants.SaveVATDeclarationData;
                    string url = Constants.GAZTSaveEstimatedZAKATAttachement + RetGuid + "',Flag='N',Dotyp='Z12L',SchGuid='',Srno=1,Doguid='',AttBy='TP',OutletRef='')/AttachMedSet?saml2=enabled";
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("slug", fileName);
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", ContentType);

                    //MultipartFormDataContent content = new MultipartFormDataContent();
                    ByteArrayContent baContent = new ByteArrayContent(AttachmentByte);
                    if (!string.IsNullOrEmpty(ContentType))
                        baContent.Headers.ContentType = new MediaTypeHeaderValue(ContentType);
                    //  content.Add(baContent, "File", fileName);
                    var response = await client.PostAsync(url, baContent);
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
        // Not Completed
        //Seesion expired handled 
        //done internet exception handling
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

                    //euser parameter removed from code to accomodate changes not made in prod from the backend
                    //other two environments are in sync
                    //after go-live this code will be removed frmo pre-prod and qa as security changes didnt go live


                    if (App.IsZakatLoadingFromMyReturns == true)
                    {
                        url = Constants.GAZTGetEstimatedZAKATSADADNumber + FBNumber + "'" + ",Langz='" + lang + "'" + ",Gpartz='" + App.TP.Tin + "'" + ",Euser='" + App.TP.Tin + "'" + ",Fbguid='" + FBGuid + "'" + ",Invflg='',Fsource='TP')?saml2=enabled&$expand=InvoiceSet&$format=json";
                    }
                    else
                    {
                        url = Constants.GAZTGetEstimatedZAKATSADADNumber + FBNumber + "'" + ",Langz='" + lang + "'" + ",Gpartz='" + App.TP.Tin + "'" + ",Euser='" + "'" + ",Fbguid='" + FBGuid + "'" + ",Invflg='',Fsource='TP')?saml2=enabled&$expand=InvoiceSet&$format=json";
                    }

                    //}
                    //else
                    //{
                    //    url = Constants.GAZTGetEstimatedZAKATSADADNumber + FBNumber + "'" + ",Langz='" + lang + "'" + ",Gpartz='" + "'" + ",Euser='" + "0000000000" + App.TP.Userid + "'" + ",Fbguid='" + FBGuid + "'" + ",Invflg='S',Fsource='TP')?saml2=disabled&$expand=InvoiceSet&$format=json";
                    //}
                    client.DefaultRequestHeaders.Add("Token", "123");
                    var uri = new Uri(url);
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
        // Not Completed
        public static async Task<EstimatedZakatReturns> GAZTDowmloadEstimatedZakatReturnInvoice(string FBNumber, string FBGuid)
        {
            //EstimatedZakatReturns zAKATICRList = new EstimatedZakatReturns();
            string NewToken = string.Empty;
            try
            {
                string lang = UtilityManager.GetLanguageParameter();
                HttpClient client = new HttpClient(App.httpClientHandler);
                String url = Constants.GAZTGetEstimatedZAKATSADADNumber + FBNumber + "'" + ",Langz='" + lang + "'" + ",Gpartz='" + "'" + ",Euser='00000000000000000000'" + ",Fbguid='" + FBGuid + "'" + ",Invflg='I',Fsource='TP')?saml2=enabled&$expand=InvoiceSet";
                ////client.DefaultRequestHeaders.Add("Token", App.Token);
                var uri = new Uri(url);
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
                    // zAKATICRList = JsonConvert.DeserializeObject<EstimatedZakatReturns>(EstimateZakatReturnList);
                }
                return null;// tINStatus;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //Seesion expired handled 
        //done internet exception handling
        public static string GAZTDeleteEstimatedZAKATRAttachment(string fileName, string DocumentID)//, string returnedFguid
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
                    // String url = "https://sapgatewayqa.gazt.gov.sa:443/sap/opu/odata/SAP/ZDP_INDTAX_ATT_SRV/AttachSet(OutletRef='',RetGuid='005056B1F8FB1EDA8FF041CFF05E83A9',Flag='N',Dotyp='VTA0',SchGuid='',Srno=1,Doguid='',AttBy='TP')/AttachMedSet";// Constants.SaveVATDeclarationData;
                    String url = Constants.GAZTDeteleAttachment + "'" + "'" + ",RetGuid='undefined'" + ",Flag='" + "N" + "'" + ",Dotyp='" + Dotyp + "'" + ",SchGuid='" + "'" + ",Srno=" + "1" + ",Doguid='" + DocumentID + "'" + ",AttBy='" + AttBy + "'" + ")/$value?saml2=enabled"; //",RetGuid='005056B1F8FB1EDA8FF041CFF05E83A9',Flag='N',Dotyp='VTA0',SchGuid='',Srno=1,Doguid='',AttBy='TP')/AttachMedSet";// Constants.SaveVATDeclarationData;

                    //string url = "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_INDTAX_ATT_SRV/AttachMedSet(RetGuid='',Flag='N',Dotyp='',SchGuid='',Srno=1,Doguid='" + DocumentID + "',AttBy='TP',OutletRef='')/$value?saml2=disbaled";                                                                                                                                                                                                                                                          // lang + "'" + "&$filter=Idtype eq " + IdType + ",RetGuid='" + RetGuid + "'" +
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
        public static async Task<string> GAZTEstimatedZAKATReturnInvoicePdf(string Cokey)//, string returnedFguid
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
                    // String url = "https://sapgatewayqa.gazt.gov.sa:443/sap/opu/odata/SAP/ZDP_INDTAX_ATT_SRV/AttachSet(OutletRef='',RetGuid='005056B1F8FB1EDA8FF041CFF05E83A9',Flag='N',Dotyp='VTA0',SchGuid='',Srno=1,Doguid='',AttBy='TP')/AttachMedSet";// Constants.SaveVATDeclarationData;
                    String url = Constants.GAZTGetEstimatedZAKATReturnInvoicePdf + Cokey + "',Cotyp='FZ01')/$value?saml2=enabled";
                    ////client.DefaultRequestHeaders.Add("Token", App.Token);
                    var uri = new Uri(url);
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
                        string PdfUrl = JsonConvert.DeserializeObject<string>(EstimateZakatReturnList);
                    }
                    return null;// tINStatus;
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
                        ZakatCorrespondenceList = JsonConvert.DeserializeObject<CorrespondenceRootObject>(VAtCorrespondenceList);
                    }
                    return ZakatCorrespondenceList;// tINStatus;
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
                    ////client.DefaultRequestHeaders.Add("Token", App.Token);
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
                    return ETReturnCorrespondenceList;// tINStatus;
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
                    ////client.DefaultRequestHeaders.Add("Token", App.Token);
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
                    return CorrespondenceDetailsList;// tINStatus;
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
            if (CrossConnectivity.Current.IsConnected)
            {
                FormBundleModel ReturnFormBundleList = new FormBundleModel();
                string NewToken = string.Empty;
                try
                {
                    char lang = GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.GAZTGetFormBundleModel + " '" + lang + "' and Gpart eq '" + App.TP.Tin + "'";
                    ////client.DefaultRequestHeaders.Add("Token", App.Token);
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
                    return ReturnFormBundleList;// tINStatus;
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
            if (CrossConnectivity.Current.IsConnected)
            {
                FormBundleApplicationNumberModel ReturnFormBundleList = new FormBundleApplicationNumberModel();
                string NewToken = string.Empty; string ApplicationNumber = Fbtyp;
                try
                {
                    char lang = GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.GAZTGetFormBunleAccountNumberModel + "'" + lang + "' and Gpart eq '" + App.TP.Tin + "' and Fbtyp eq '" + ApplicationNumber + "'";
                    //String url = Constants.GAZTGetFormBunleAccountNumberModel;E' and Gpart eq '3300088513' and Fbtyp eq 'ZI10'&saml2=disabled
                    ////client.DefaultRequestHeaders.Add("Token", App.Token);
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
                        ReturnFormBundleList = JsonConvert.DeserializeObject<FormBundleApplicationNumberModel>(FormBundleList);
                    }
                    return ReturnFormBundleList;// tINStatus;
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
                    //String url = Constants.GAZTGetFormBunleAccountNumberModel;E' and Gpart eq '3300088513' and Fbtyp eq 'ZI10'&saml2=disabled
                    // //client.DefaultRequestHeaders.Add("Token", App.Token);
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
                    return SignupCityList;// tINStatus;
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


                //catch (Exception ex)
                //{
                //    return null;
                //}
            }
            else
            {
                //throw new GAZTNetworkConnectivityIssueException(AppResources.ZZInternetConnectionMessage);
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
                    //String url = Constants.GAZTGetFormBunleAccountNumberModel;E' and Gpart eq '3300088513' and Fbtyp eq 'ZI10'&saml2=disabled


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
                        catch (Exception ex)
                        {
                            Console.WriteLine("Unable to Find Other Value");
                        }

                        SignupIssuedByList = new List<IssuedByResponse>(sortedIssuedByList);
                    }
                    return SignupIssuedByList;// tINStatus;
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


                //catch (Exception ex)
                //{
                //    return null;
                //}
            }
            else
            {
                //  throw new InternetException(AppResources.ZZInternetConnectionMessage);
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
                    //String url = Constants.GAZTGetFormBunleAccountNumberModel;E' and Gpart eq '3300088513' and Fbtyp eq 'ZI10'&saml2=disabled
                    // //client.DefaultRequestHeaders.Add("Token", App.Token);
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
                    return IsIDTypeValidList;// tINStatus;
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

                //catch (Exception ex)
                //{
                //    return null;
                //}
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
                    //String url = Constants.GAZTGetFormBunleAccountNumberModel;E' and Gpart eq '3300088513' and Fbtyp eq 'ZI10'&saml2=disabled
                    // //client.DefaultRequestHeaders.Add("Token", App.Token);
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
                    return CRValidationModelValid;// tINStatus;
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

                    //String url = Constants.GAZTGetFormBunleAccountNumberModel;E' and Gpart eq '3300088513' and Fbtyp eq 'ZI10'&saml2=disabled
                    //client.DefaultRequestHeaders.Add("Token", App.Token);

                    if (!string.IsNullOrEmpty(crNum))
                    {

                        // hard coded string for CRNumber in the Institute parameter;
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
                    return ValidateDuplicate;// tINStatus;
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
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    string LangZ = GetLangZParameterAREN();
                    string FirstSignupSubmit = string.Empty;
                    string url = Constants.GAZTSignUpFirstSubmit + LangZ;
                    //string FirstSignupSubmit = string.Empty;
                    //string url = Constants.GAZTSignUpFirstSubmit;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

                    var serilized = JsonConvert.SerializeObject(SignUpModel);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    FirstSignupSubmit = await res.Content.ReadAsStringAsync();
                    // FirstSignupSubmit = JsonConvert.DeserializeObject<SignUpModelRootObject>(detailJson);
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
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {


                    //string FirstSignupSubmit = string.Empty;
                    //string url = Constants.GAZTSignUpFirstSubmit;
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
                    // FirstSignupSubmit = JsonConvert.DeserializeObject<SignUpModelRootObject>(detailJson);
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
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                    string Langz = UtilityManager.GetLanguageParameter();
                    string FirstSignupSubmit = string.Empty;
                    //string url = Constants.GAZTSignUpFirstSubmit + "?sap-language=" + Langz;
                    string url = Constants.GAZTSignUpFirstSubmit + Langz;
                    var uri = new Uri(url);
                    //HttpClient client = new HttpClient(crmSignUphttpClientHandler);
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
                    //String url = Constants.GAZTGetFormBunleAccountNumberModel;E' and Gpart eq '3300088513' and Fbtyp eq 'ZI10'&saml2=disabled
                    //client.DefaultRequestHeaders.Add("Token", App.Token);
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
                    return GaztGuidModel;// tINStatus;
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
                //catch (Exception ex)
                //{
                //    return null;
                //}
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
                    //client.DefaultRequestHeaders.Add("Accept", "application/json");//
                    var serilized = JsonConvert.SerializeObject(Cred);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    var response = res.Content.ReadAsStringAsync().Result;
                    terfregion = JsonConvert.DeserializeObject<TERFRegionRootObject>(response);
                    return terfregion;
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
                    //client.DefaultRequestHeaders.Add("Accept", "application/json");//
                    var serilized = JsonConvert.SerializeObject(Cred);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    var response = res.Content.ReadAsStringAsync().Result;
                    terfcity = JsonConvert.DeserializeObject<TERFCityRetrieveRootObject>(response);
                    return terfcity;
                }
                catch (Exception ex)
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
                    //client.DefaultRequestHeaders.Add("Accept", "application/json");//
                    var serilized = JsonConvert.SerializeObject(Cred);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    var response = await res.Content.ReadAsStringAsync();
                    terffaq = JsonConvert.DeserializeObject<TERFAQs>(response);
                    return terffaq;
                }
                catch (Exception ex)
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
                    //client.DefaultRequestHeaders.Add("Accept", "application/json");//
                    var serilized = JsonConvert.SerializeObject(Cred);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    var response = res.Content.ReadAsStringAsync().Result;
                    terfcity = JsonConvert.DeserializeObject<TEReportResponsePostRootObject>(response);
                    return terfcity;
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
        public static async Task<ReportRetriveByMobNoRootObject> GAZTTESReportByMobNo(string TPmobno)
        {
            ReportRetriveByMobileNumberPost Cred = new ReportRetriveByMobileNumberPost();
            Cred.WSUserName = "GAZT@CRM";
            Cred.WSPassword = "gazt@123";
            string trimedmob = TPmobno;
            //string trimedmob1 = trimedmob.Substring(5);
            //Cred.MobileNumber = "0"+ trimedmob1;
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
                    //client.DefaultRequestHeaders.Add("Accept", "application/json");//
                    var serilized = JsonConvert.SerializeObject(Cred);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    var response = await res.Content.ReadAsStringAsync();
                    terfreport = JsonConvert.DeserializeObject<ReportRetriveByMobNoRootObject>(response);
                    return terfreport;
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
        #region SYNFUSION INTEGRATION
        public static Dashboard GAZTGetDashboardData(string lang, string TIN)
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
                    //client.DefaultRequestHeaders.Add("Token", App.Token);
                    string uri = Constants.GetDashboardData + TIN + "'" + "&saml2=enabled" + "&$format=json";
                    HttpResponseMessage GAZTGetDashboardResponse = client.GetAsync(uri).Result;
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
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    //client.DefaultRequestHeaders.Add("Token", App.Token);
                    string uri = Constants.GAZTGetReturnList + TIN + "' and Lang eq '" + lang + "'&saml2=enabled&$format=json";
                    HttpResponseMessage GAZTGetDashboardResponse = await client.GetAsync(uri);
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
                    //client.DefaultRequestHeaders.Add("Token", App.Token);
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
            //lang = "E";
            //TIN = "3311620297";
            //Token = "051MiJPS7jgPsOOq374UiG!MjAyMDAzMTUxNzM2MTc";
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
                    //client.DefaultRequestHeaders.Add("Token", App.Token);
                    client.DefaultRequestHeaders.Add("Token", "123");
                    string uri = Constants.GAZTGetUnSubmittedReturnSetForDashboard + lang + "'" + " and Gpartz eq '" + TIN + "'" + "&sap-language=" + lang + "&saml2=enabled&$format=json";
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
                            GAZTGetUnSubmittedReturnSetResponseJSON = JObject.Parse(GAZTGetUnSubmittedReturnSetResponseJSON)["d"].ToString();
                            GAZTGetUnSubmittedReturnSetResponseJSON = JObject.Parse(GAZTGetUnSubmittedReturnSetResponseJSON)["results"].ToString();
                            overduePayments = JsonConvert.DeserializeObject<List<OverduePaymentAndUnSubmittedReturn>>(GAZTGetUnSubmittedReturnSetResponseJSON);
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
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    // client.DefaultRequestHeaders.Add("Token", App.Token);

                    client.DefaultRequestHeaders.Add("Token", "123");
                    string uri = Constants.GAZTGetPaymentOverdueSetForDashboard + lang + "'" + " and Gpartz eq '" + TIN + "'" + "&sap-language=" + lang + "&saml2=enabled&$format=json";
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
                            GAZTGetPaymentOverdueSetResponseJSON = JObject.Parse(GAZTGetPaymentOverdueSetResponseJSON)["d"].ToString();
                            GAZTGetPaymentOverdueSetResponseJSON = JObject.Parse(GAZTGetPaymentOverdueSetResponseJSON)["results"].ToString();
                            paymentOverdueSet = JsonConvert.DeserializeObject<List<OverduePaymentAndUnSubmittedReturn>>(GAZTGetPaymentOverdueSetResponseJSON);
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
                catch (XPathException xex)
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
                List<TIN> TINs = null;
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
                            //Dev
                            cookie.Domain = Constants.PartialDomainUrlForCookies;

                            //QA, Pre-prod and Prod
                            //cookie.Domain = ".gazt.gov.sa";

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
                List<TIN> TINs = null;
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

                            //Dev
                            //cookie.Domain = Constants.PartialDomainUrlForCookies;

                            //QA, Pre-prod and Prod
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
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.GAZTSAMLLogoutService;

                    var uri = new Uri(url);

                    HttpResponseMessage GAZTLogOffResponse = await client.GetAsync(uri);
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

                    // String url = Constants.GAZTGetTP + "='" + TIN + "',Langz='" + Lang + "')" + "?&$expand=TPOC_LIST&saml2=enabled&$format=json";
                    ////client.DefaultRequestHeaders.Add("Token", App.Token);
                    ///
                    String url = Constants.GAZTGetTP + "='" + TIN + "',Langz='" + Lang + "')" + "?&$expand=TPOC_LIST&saml2=enabled&$format=json";

                    ////client.DefaultRequestHeaders.Add("Token", App.LoginDataRetrieved.DeviceToken);

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
                catch (JsonReaderException ex)
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
            string sendDateTime = "0";
            if (CrossConnectivity.Current.IsConnected)
            {
                //ReportRetriveByMobNoRootObject terfreport = new ReportRetriveByMobNoRootObject();
                try
                {
                    ///---New API---///
                    ///https://prdsmswrapper.gazt.gov.sa/GAZTServiceREST.svc/SendBulkSMS?userName=*******&password=*********&tagName=GAZT.gov.sa&recepientNumbers=966********;966*****&message=*********&sendDateTime=0

                    //Replace the below API with the new one
                    string url = "http://10.50.11.203/ZPService/SMSAPI.asmx/SendSingleSMS?userName=" + userName + "&password=" + password + "&tagName=" + tagName + "&recepientNumber=" + recepientNumber + "&message=" + message + "&sendDateTime=0";
                    // string url = "http://10.50.11.203/ZPService/SMSAPI.asmx/SendSingleSMS?userName=GaztApp&password=Gazt@2020&tagName=Gazt.gov.sa&recepientNumber=966571006494&message=Test123onkar13:40&sendDateTime=0";
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    HttpResponseMessage res = await client.GetAsync(uri);
                    var response = await res.Content.ReadAsStringAsync();
                    XElement xmlroot = XElement.Parse(response);
                    string statuscode = xmlroot.Value;
                    return statuscode;
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
        }
        public static async Task<AttachmentDocumentModel> GAZTGetAllAttachments(String retGuid, String fbNum)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                String GAZTAttachmentsResponseResult = String.Empty;
                AttachmentDocumentModel attachmentDocumentModel = null;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    client.DefaultRequestHeaders.Add("Token", "123");

                    //string url = "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/Z_GET_DOCUMENT_SRV/AttachSet?$filter=ByPusr%20eq%20%27065000210711%27%20and%20RetGuid%20eq%20%27005056B1F8FB1EEAA9D0D6E708285146%27&saml2=enabled&$format=json";
                    //string url = "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/Z_GET_DOCUMENT_SRV/AttachSet?$filter=ByPusr eq '" +fbNum + "'" + " and RetGuid eq '" + retGuid + "'" + "&saml2=enabled&$format=json";

                    string url = Constants.GAZTGetAllAttachments + fbNum + "'" + " and RetGuid eq '" + retGuid + "'" + "&saml2=enabled&$format=json";

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

        #region Tax Evasion

        public static async Task<TaxEvasionCategoriesModel> GAZTTaxEvasionGetCategories()
        {
            TaxEvasionCategoriesModel categoriesModel = new TaxEvasionCategoriesModel();

            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    string url = Constants.GAZTTaxEvasionGetCategories;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);

                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    string langVal = "en";
                    if (App.IsArabic == true)
                    {
                        langVal = "ar";
                    }

                    client.DefaultRequestHeaders.Add("Accept-Language", langVal);
                    HttpResponseMessage res = await client.GetAsync(uri);
                    var response = res.Content.ReadAsStringAsync().Result;
                    categoriesModel = JsonConvert.DeserializeObject<TaxEvasionCategoriesModel>(response);
                    return categoriesModel;
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
                throw new GAZTNetworkConnectivityIssueException();
            }
        }


        public static async Task<TaxEvasionSendSmsResponseModel> GAZTTaxEvasionSendSms(TaxEvasionSendSmsModel sendSmsModel)
        {
            TaxEvasionSendSmsResponseModel sendSmsResponse = new TaxEvasionSendSmsResponseModel();
            TaxEvasionErrorReponseModel errorReponseModel = new TaxEvasionErrorReponseModel();
            string response = string.Empty;

            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    string url = Constants.GAZTTaxEvasionSendSms;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);

                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    var serilized = JsonConvert.SerializeObject(sendSmsModel);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    response = res.Content.ReadAsStringAsync().Result;
                    sendSmsResponse = JsonConvert.DeserializeObject<TaxEvasionSendSmsResponseModel>(response);
                    return sendSmsResponse;
                }
                catch (Exception)
                {
                    errorReponseModel = JsonConvert.DeserializeObject<TaxEvasionErrorReponseModel>(response);
                    throw new Exception(errorReponseModel.Data);
                }
            }
            else
            {
                throw new GAZTNetworkConnectivityIssueException();
            }
        }

        public static async Task<TaxEvasionVerifySmsResponseModel> GAZTTaxEvasionVerifySms(TaxEvasionVerifySmsModel verifySmsModel, string mobileNumber)
        {
            TaxEvasionVerifySmsResponseModel verifySmsResponse = new TaxEvasionVerifySmsResponseModel();
            TaxEvasionErrorReponseModel errorReponseModel = new TaxEvasionErrorReponseModel();
            string response = string.Empty;

            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    string url = Constants.GAZTTaxEvasionVerifySms;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);
                    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
                    requestMessage.Headers.Add("Accept", "application/json");
                    requestMessage.Headers.Add("mobile", mobileNumber);
                    var serilized = JsonConvert.SerializeObject(verifySmsModel);

                    requestMessage.Content = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);

                    string langVal = "en";
                    if (App.IsArabic == true)
                    {
                        langVal = "ar";
                    }

                    requestMessage.Headers.Add("Accept-Language", langVal);

                    HttpResponseMessage res = await client.SendAsync(requestMessage);

                    response = res.Content.ReadAsStringAsync().Result;
                    verifySmsResponse = JsonConvert.DeserializeObject<TaxEvasionVerifySmsResponseModel>(response);

                    App.TaxEvasionToken = verifySmsResponse.SmsResponse.Token;

                    return verifySmsResponse;
                }
                catch (Exception)
                {
                    errorReponseModel = JsonConvert.DeserializeObject<TaxEvasionErrorReponseModel>(response);
                    if (errorReponseModel.Data.Contains("Invalid code") || errorReponseModel.Data.Contains("الرمز غير صحيح"))
                    {

                        throw new Exception(AppResources.InvalidOTP);


                    }
                    else
                    {
                        throw new Exception(errorReponseModel.Data);
                    }

                }
            }
            else
            {
                throw new GAZTNetworkConnectivityIssueException();
            }
        }

        public static async Task<TaxEvasionReportsModel> GAZTTaxEvasionGetAllReportsByMobileNumber(TaxEvasionSendSmsModel mobileNumberModel)
        {
            TaxEvasionReportsModel verifySmsResponse = new TaxEvasionReportsModel();
            TaxEvasionErrorReponseModel errorReponseModel = new TaxEvasionErrorReponseModel();
            string response = string.Empty;

            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    string url = Constants.GAZTTaxEvasionGetAllReports;
                    var uri = new Uri(url);

                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", App.TaxEvasionToken);

                    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
                    requestMessage.Headers.Add("Accept", "application/json");

                    string langVal = "en";
                    if (App.IsArabic == true)
                    {
                        langVal = "ar";
                    }

                    requestMessage.Headers.Add("Accept-Language", langVal);
                    var serilized = JsonConvert.SerializeObject(mobileNumberModel);
                    requestMessage.Content = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    HttpResponseMessage res = await client.SendAsync(requestMessage);
                    response = res.Content.ReadAsStringAsync().Result;

                    verifySmsResponse = JsonConvert.DeserializeObject<TaxEvasionReportsModel>(response);

                    return verifySmsResponse;
                }
                catch (Exception)
                {
                    errorReponseModel = JsonConvert.DeserializeObject<TaxEvasionErrorReponseModel>(response);
                    throw new Exception(errorReponseModel.Data);
                }
            }
            else
            {
                throw new GAZTNetworkConnectivityIssueException();
            }
        }

        public static async Task<TaxEvasionUserRegistrationResponseModel> GAZTTaxEvasionGetUserByMobile(TaxEvasionSendSmsModel mobileNumberModel)
        {
            TaxEvasionUserRegistrationResponseModel sendSmsResponse = new TaxEvasionUserRegistrationResponseModel();
            TaxEvasionErrorReponseModel errorReponseModel = new TaxEvasionErrorReponseModel();
            string response = string.Empty;

            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    string url = Constants.GAZTTaxEvasionGetUserByMobile;
                    var uri = new Uri(url);

                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", App.TaxEvasionToken);

                    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
                    requestMessage.Headers.Add("Accept", "application/json");
                    requestMessage.Headers.Add("mobile", mobileNumberModel.mobile);

                    string langVal = "en";
                    if (App.IsArabic == true)
                    {
                        langVal = "ar";
                    }

                    requestMessage.Headers.Add("Accept-Language", langVal);
                    var serilized = JsonConvert.SerializeObject(mobileNumberModel);
                    requestMessage.Content = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    HttpResponseMessage res = await client.SendAsync(requestMessage);
                    response = res.Content.ReadAsStringAsync().Result;

                    sendSmsResponse = JsonConvert.DeserializeObject<TaxEvasionUserRegistrationResponseModel>(response);

                    return sendSmsResponse;
                }
                catch (Exception)
                {
                    errorReponseModel = JsonConvert.DeserializeObject<TaxEvasionErrorReponseModel>(response);
                    throw new Exception(errorReponseModel.Data);
                }
            }
            else
            {
                throw new GAZTNetworkConnectivityIssueException();
            }
        }

        public static async Task<TaxEvasionUserRegistrationResponseModel> GAZTTaxEvasionRegisterUser(TaxEvasionRegisterUserModel registerUserModel)
        {
            TaxEvasionUserRegistrationResponseModel registrationResponseModel = new TaxEvasionUserRegistrationResponseModel();
            TaxEvasionErrorReponseModel errorReponseModel = new TaxEvasionErrorReponseModel();
            string response = string.Empty;

            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    string url = Constants.GAZTTaxEvasionRegisterUser;
                    var uri = new Uri(url);

                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", App.TaxEvasionToken);

                    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
                    requestMessage.Headers.Add("Accept", "application/json");

                    var serilized = JsonConvert.SerializeObject(registerUserModel);
                    requestMessage.Content = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    HttpResponseMessage res = await client.SendAsync(requestMessage);
                    response = res.Content.ReadAsStringAsync().Result;

                    registrationResponseModel = JsonConvert.DeserializeObject<TaxEvasionUserRegistrationResponseModel>(response);
                    App.TaxEvasionToken = registrationResponseModel.Data.ApiToken;

                    return registrationResponseModel;
                }
                catch (Exception)
                {
                    errorReponseModel = JsonConvert.DeserializeObject<TaxEvasionErrorReponseModel>(response);
                    throw new Exception(errorReponseModel.Data);
                }
            }
            else
            {
                throw new GAZTNetworkConnectivityIssueException();
            }
        }


        public static async Task<TaxEvasionRegionsCityModel> GAZTTaxEvasionGetAllRegions()
        {
            TaxEvasionRegionsCityModel regionsModel = new TaxEvasionRegionsCityModel();

            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    string url = Constants.GAZTTaxEvasionGetAllRegions;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    string langVal = "en";
                    if (App.IsArabic == true)
                    {
                        langVal = "ar";
                    }

                    client.DefaultRequestHeaders.Add("Accept-Language", langVal);

                    HttpResponseMessage res = await client.GetAsync(uri);
                    var response = res.Content.ReadAsStringAsync().Result;
                    regionsModel = JsonConvert.DeserializeObject<TaxEvasionRegionsCityModel>(response);
                    return regionsModel;
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
                throw new GAZTNetworkConnectivityIssueException();
            }
        }

        public static async Task<TaxEvasionRegionsCityModel> GAZTTaxEvasionGetAllCitiesByRegion(long regionId)
        {
            TaxEvasionRegionsCityModel regionsModel = new TaxEvasionRegionsCityModel();

            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    string url = Constants.GAZTTaxEvasionGetAllCities + Convert.ToString(regionId);
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);

                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    string langVal = "en";
                    if (App.IsArabic == true)
                    {
                        langVal = "ar";
                    }

                    client.DefaultRequestHeaders.Add("Accept-Language", langVal);

                    HttpResponseMessage res = await client.GetAsync(uri);
                    var response = res.Content.ReadAsStringAsync().Result;
                    regionsModel = JsonConvert.DeserializeObject<TaxEvasionRegionsCityModel>(response);
                    return regionsModel;
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
                throw new GAZTNetworkConnectivityIssueException();
            }
        }

        public static async Task<TaxEvasionCreateReportResponseModel> GAZTTaxEvasionCreateReport(TaxEvasionReportDetails evasionReportDetails, List<UploadedDocumentsList> documentsLists)
        {
            TaxEvasionCreateReportResponseModel responseModel = new TaxEvasionCreateReportResponseModel();
            TaxEvasionErrorReponseModel errorReponseModel = new TaxEvasionErrorReponseModel();
            string response = string.Empty;

            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    string url = Constants.GAZTTaxEvasionCreateReport;
                    var uri = new Uri(url);

                    using (var client = new HttpClient())
                    {
                        using (var multipartFormDataContent = new MultipartFormDataContent())
                        {
                            var values = new[]
                            {
                                new KeyValuePair<string, string>("RegionCode", evasionReportDetails.RegionCode),
                                new KeyValuePair<string, string>("category", evasionReportDetails.Category),
                                new KeyValuePair<string, string>("category_id", "1"),
                                new KeyValuePair<string, string>("city", evasionReportDetails.City),
                                new KeyValuePair<string, string>("content", evasionReportDetails.Content),
                                new KeyValuePair<string, string>("district", evasionReportDetails.District),
                                new KeyValuePair<string, string>("facilities", evasionReportDetails.Facilities),
                                new KeyValuePair<string, string>("facility_work_type", evasionReportDetails.WorkType),
                                new KeyValuePair<string, string>("location", "https://www.google.com/maps/search/?api=1&query=" + evasionReportDetails.Latitude + "," + evasionReportDetails.Longitude),
                                new KeyValuePair<string, string>("phone_number", evasionReportDetails.PhoneNumber),
                                new KeyValuePair<string, string>("street", evasionReportDetails.Street),
                                new KeyValuePair<string, string>("subject", ""),
                                new KeyValuePair<string, string>("vat_number", evasionReportDetails.VatNumber),
                                new KeyValuePair<string, string>("TIN", evasionReportDetails.Tin)
                            };

                            foreach (var keyValuePair in values)
                            {
                                multipartFormDataContent.Add(new StringContent(keyValuePair.Value),
                                    String.Format("\"{0}\"", keyValuePair.Key));
                            }

                            foreach (UploadedDocumentsList uploadedDocumentsList in documentsLists)
                            {
                                multipartFormDataContent.Add(new ByteArrayContent(uploadedDocumentsList.DocBinaryInBase64),
                               '"' + "File" + '"',
                               '"' + uploadedDocumentsList.FileNameWithExtension + '"');
                            }

                            string langVal = "en";
                            if (App.IsArabic == true)
                            {
                                langVal = "ar";
                            }

                            client.DefaultRequestHeaders.Add("Accept-Language", langVal);

                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", App.TaxEvasionToken);
                            var result = await client.PostAsync(uri, multipartFormDataContent);
                            HttpContent responseContent = result.Content;
                            response = responseContent.ReadAsStringAsync().Result;
                            responseModel = JsonConvert.DeserializeObject<TaxEvasionCreateReportResponseModel>(response);
                            return responseModel;
                        }
                    }
                }
                catch (Exception)
                {
                    errorReponseModel = JsonConvert.DeserializeObject<TaxEvasionErrorReponseModel>(response);
                    throw new Exception(errorReponseModel.Data);
                }
            }
            else
            {
                throw new GAZTNetworkConnectivityIssueException();
            }
        }



        public static async Task<VATSignUpCaseId> GAZTGetVATSignUpCaseId()
        {
            VATSignUpCaseId vATSignUpCaseId = new VATSignUpCaseId();

            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    string url = Constants.GAZTGetVATSignUpCaseId;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);

                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    //string langVal = "en";
                    //if (App.IsArabic == true)
                    //{
                    //    langVal = "ar";
                    //}

                    //client.DefaultRequestHeaders.Add("Accept-Language", langVal);

                    HttpResponseMessage res = client.GetAsync(uri).Result;
                    var response = res.Content.ReadAsStringAsync().Result;
                    vATSignUpCaseId = JsonConvert.DeserializeObject<VATSignUpCaseId>(response);
                    return vATSignUpCaseId;
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
                throw new GAZTNetworkConnectivityIssueException();
            }
        }

        public async static Task<String> GAZTVATSignUpValidateTinNumberStringResp(string Tin)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                VATSignUp vATSignUp = new VATSignUp();
                string IsIDTypeValidList = string.Empty;
                string NewToken = string.Empty;
                String SignUpCityList = string.Empty;
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    string IDType = string.Empty;
                    string IDNumber = string.Empty;
                    string DBO = string.Empty;
                    char lang = GetLangZParameter();
                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);
                    String url = Constants.GAZTVATSignUpValidateId + "(Tin='" + Tin + "',Idtype='" + IDType + "',Idnum='" + IDNumber + "',Country='',PassExpDt='',TaxpDob='" + DBO + "')?sap-language=" + lang + "&$format=json&saml2=enabled";
                    //                     (Tin='',Idtype='ZS0015',Idnum='1048089609',Country='',PassExpDt='',TaxpDob='19650224')?sap-language=A&$format=json&saml2=enabled

                    var uri = new Uri(url);
                    HttpResponseMessage VATSignUpIdValidateObject = await client.GetAsync(uri);
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
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        SignUpCityList = await VATSignUpIdValidateObject.Content.ReadAsStringAsync();
                        //vATSignUp = JsonConvert.DeserializeObject<VATSignUp>(SignUpCityList);
                        //if (vATSignUp != null)
                        //{
                        //    if (vATSignUp.d == null)
                        //    {

                        //    }
                        //}



                        //IsIDTypeValidList = await SignupIsIDTypeValidList.Content.ReadAsStringAsync();
                    }
                    return SignUpCityList;// tINStatus;
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

                //catch (Exception ex)
                //{
                //    return null;
                //}
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        public async static Task<String> GAZTVATSignUpValidateIDTypesStringResp(string IDType, string IDNumber, string DBO)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                VATSignUp vATSignUp = new VATSignUp();
                string IsIDTypeValidList = string.Empty;
                string NewToken = string.Empty;
                String SignUpCityList = string.Empty;
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                    char lang = GetLangZParameter();
                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);
                    String url = Constants.GAZTVATSignUpValidateId + "(Tin='',Idtype='" + IDType + "',Idnum='" + IDNumber + "',Country='',PassExpDt='',TaxpDob='" + DBO + "')?sap-language=" + lang + "&$format=json&saml2=enabled";
                    //                     (Tin='',Idtype='ZS0015',Idnum='1048089609',Country='',PassExpDt='',TaxpDob='19650224')?sap-language=A&$format=json&saml2=enabled

                    var uri = new Uri(url);
                    HttpResponseMessage VATSignUpIdValidateObject = await client.GetAsync(uri);
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
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        SignUpCityList = await VATSignUpIdValidateObject.Content.ReadAsStringAsync();
                        //vATSignUp = JsonConvert.DeserializeObject<VATSignUp>(SignUpCityList);
                        //if (vATSignUp != null)
                        //{
                        //    if (vATSignUp.d == null)
                        //    {

                        //    }
                        //}



                        //IsIDTypeValidList = await SignupIsIDTypeValidList.Content.ReadAsStringAsync();
                    }
                    return SignUpCityList;// tINStatus;
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

                //catch (Exception ex)
                //{
                //    return null;
                //}
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        public async static Task<VATSignUp> GAZTVATSignUpValidateIDTypes(string IDType, string IDNumber, string DBO)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                VATSignUp vATSignUp = new VATSignUp();
                string IsIDTypeValidList = string.Empty;
                string NewToken = string.Empty;
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                    char lang = GetLangZParameter();
                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);
                    String url = Constants.GAZTVATSignUpValidateId + "(Tin='',Idtype='" + IDType + "',Idnum='" + IDNumber + "',Country='',PassExpDt='',TaxpDob='" + DBO + "')?sap-language=" + lang + "&$format=json&saml2=enabled";
                    //                     (Tin='',Idtype='ZS0015',Idnum='1048089609',Country='',PassExpDt='',TaxpDob='19650224')?sap-language=A&$format=json&saml2=enabled

                    var uri = new Uri(url);
                    HttpResponseMessage VATSignUpIdValidateObject = await client.GetAsync(uri);
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
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        String SignUpCityList = await VATSignUpIdValidateObject.Content.ReadAsStringAsync();
                        vATSignUp = JsonConvert.DeserializeObject<VATSignUp>(SignUpCityList);
                        if (vATSignUp != null)
                        {
                            if (vATSignUp.d == null)
                            {

                            }
                        }



                        //IsIDTypeValidList = await SignupIsIDTypeValidList.Content.ReadAsStringAsync();
                    }
                    return vATSignUp;// tINStatus;
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

                //catch (Exception ex)
                //{
                //    return null;
                //}
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }


        public static async Task<VATSignUpData> GAZTGetVATSignUpCityListForSignup()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                VATSignUpData vATSignUpData = new VATSignUpData();
                string NewToken = string.Empty;
                try
                {
                    char lang = GetLangZParameter();

                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);

                    String url = Constants.GAZTGetVATSignUpCityAndRegionList + "dropdown_headerSet(Spras='" + lang + "',Land1='',Bland='',Cityc='')?&$expand=city_dropdownSet,country_dropdownSet,State_dropdownSet&saml2=enabled&$format=json";
                    // dropdown_headerSet(Spras='A',Land1='',Bland='',Cityc='')?&$expand=city_dropdownSet,country_dropdownSet,State_dropdownSet&saml2=enabled&$format=json
                    var uri = new Uri(url);
                    HttpResponseMessage VATSignUpCountryRegionCityList = await client.GetAsync(uri);
                    if (VATSignUpCountryRegionCityList != null)
                    {
                        HttpHeaders headers = VATSignUpCountryRegionCityList.Headers;
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
                        String signUpData = await VATSignUpCountryRegionCityList.Content.ReadAsStringAsync();
                        vATSignUpData = JsonConvert.DeserializeObject<VATSignUpData>(signUpData);
                    }
                    return vATSignUpData;// tINStatus;
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


                //catch (Exception ex)
                //{
                //    return null;
                //}
            }
            else
            {
                //throw new GAZTNetworkConnectivityIssueException(AppResources.ZZInternetConnectionMessage);
                throw new GAZTInternetException();
            }
        }

        public static async Task<VATSignUpSubmit> GAZTCreateVATSignUp(VATSignUpSubmit vATSignUpSubmit)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    VATSignUpSubmit vatSignUpSubmit = new VATSignUpSubmit();
                    string url = Constants.GAZTGetCreateVATSignUp;
                    var uri = new Uri(url);

                    try
                    {
                        App.httpClientHandler.CookieContainer = null;
                    }
                    catch (Exception ex)
                    {

                    }
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    var serilized = JsonConvert.SerializeObject(vATSignUpSubmit);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    var detailJson = res.Content.ReadAsStringAsync().Result;

                    vatSignUpSubmit = JsonConvert.DeserializeObject<VATSignUpSubmit>(detailJson);
                    return vatSignUpSubmit;
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
        public static async Task<string> GAZTCreateVATSignUpFirst(VATSignUpSubmit vATSignUpSubmit)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    string LangZ = GetLangZParameterAREN();



                    VATSignUpSubmit vatSignUpSubmit = new VATSignUpSubmit();
                    string url = Constants.GAZTGetCreateVATSignUp + LangZ;
                    var uri = new Uri(url);

                    try
                    {
                        App.httpClientHandler.CookieContainer = null;
                    }
                    catch (Exception ex)
                    {

                    }
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

                    var serilized = JsonConvert.SerializeObject(vATSignUpSubmit);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    var detailJson = res.Content.ReadAsStringAsync().Result;
                    //VATSignUpSubmit vatSignUpSubmit = new VATSignUpSubmit();
                    //vatSignUpSubmit = JsonConvert.DeserializeObject<VATSignUpSubmit>(detailJson);
                    return detailJson;
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

        #endregion


        #region VatRegistration

        public async static Task<VATRegistrationDetails> GAZTGetVATRegistrationData()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                VATRegistrationDetails vATRegistrationDetails = new VATRegistrationDetails();
                string NewToken = string.Empty;
                try
                {
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.GAZTGetVATRegistrationData + "',PortalUsrz='" + "',Langz='" + lang + "',Officerz='" + "',Gpartz='" + App.LoginDataRetrieved.TIN + "',TxnTpz='" + "04" + "',Euser='" + "" + "',Fbguid='" + "" + "'" + ")?&$expand=ADDRESSSet,IBANSet,ATTDETSet,CONTACT_PERSONSet,CONTACTDTSet,NOTESSet,QUESTIONSSet,QUESLISTSet,QUESCONFIG_MSet,ELGBL_DOCSet&$format=json";
                    client.DefaultRequestHeaders.Add("Token", "123");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
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
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")))
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
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
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

        public async static Task<VATDeRegistrationDetails> GAZTGetVATDeRegistrationData()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                VATDeRegistrationDetails vATDeRegistrationDetails = new VATDeRegistrationDetails();
                string NewToken = string.Empty;
                try
                {
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.GAZTGetVATDeRegistrationData + "',PortalUsrx='" + "',Langx='" + lang + "',Officerx='" + "',Gpartx='" + App.LoginDataRetrieved.TIN + "',Euser='" + "" + "',FormGuid='" + "" + "',ReviewFg=false)?&$expand=AddressSet,AttdetSet,NotesSet,QuesListSet&$format=json";
                    client.DefaultRequestHeaders.Add("Token", "123");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
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
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        String VatRegistrationData = GAZTVATDeRegistrationDataResponse.Content.ReadAsStringAsync().Result;
                        vATDeRegistrationDetails = JsonConvert.DeserializeObject<VATDeRegistrationDetails>(VatRegistrationData);
                        if (!string.IsNullOrEmpty(VatRegistrationData) && vATDeRegistrationDetails.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(VatRegistrationData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message+" ";
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
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


        public async static Task<VATRegistrationOtherDetails> GAZTGetVATRegistrationDataWithButtons(string Fbnumz, string Officerz, string Status, string TxnTp, string Formproc)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                VATRegistrationOtherDetails vATRegistrationOtherDetails = new VATRegistrationOtherDetails();
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    Char lang = WebServiceManager.GetLangZParameter();
                    String url = Constants.GAZTGetVATRegistrationOtherDetails + Fbnumz + "',Lang='" + lang + "',Officer='" + Officerz + "',Gpart='" + App.LoginDataRetrieved.TIN + "',Status='" + Status + "',TxnTp='" + "CRE_RGVT" + "',Formproc='" + "ZTAX_VT_REG" + "')?&$expand=VR_UI_BTNSet,ELGBL_DOCSet&$format=json";
                    client.DefaultRequestHeaders.Add("Token", "123");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
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
                        vATRegistrationOtherDetails = JsonConvert.DeserializeObject<VATRegistrationOtherDetails>(VatRegistrationOtherData);

                        if (!string.IsNullOrEmpty(VatRegistrationOtherData) && vATRegistrationOtherDetails == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(VatRegistrationOtherData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new Exception(errorMessage);
                            }
                        }

                    }
                    return vATRegistrationOtherDetails;
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
        public static async Task<VATRegistrationDetails> SaveVATRegistrationData(VATRegistrationDetails vATRegistration)
        {
            VATRegistrationDetails RequestVATRegistration = new VATRegistrationDetails();
            VATRegistrationDetails _vATRegistration = new VATRegistrationDetails();
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    if (vATRegistration != null && vATRegistration.d != null)
                    {
                        if (vATRegistration.d != null)
                        {
                            //if (vATRegistration.d.ELGBL_DOCSet != null)
                            //{


                            //    //foreach (var item in vATRegistration.d.ELGBL_DOCSet.results)
                            //    //{
                            //    //    if (item.Txt50 == null)
                            //    //    {
                            //    //        item.Txt50 = string.Empty;
                            //    //    }
                            //    //}
                            //}
                            RequestVATRegistration = vATRegistration;

                            ELGBL_DOCSet eLGBL_DOCSet = new ELGBL_DOCSet();
                            eLGBL_DOCSet.results = new List<ResultsItemForElgblDocSet>();
                            RequestVATRegistration.d.ELGBL_DOCSet = eLGBL_DOCSet;
                            //RequestVATDeclaration.d.SubmitFg = "";
                            ATTDETSet aTTACHSet = new ATTDETSet();
                            aTTACHSet.results = new List<Attachment>();
                            RequestVATRegistration.d.ATTDETSet = aTTACHSet;
                            //if (vATRegistration.d.VatTaxDt == null)
                            //{
                            //    vATRegistration.d.VatTaxDt = "";
                            //}


                        }

                        //string url = Constants.GAZTSignUpFirstSubmit;
                        string LangZAREN = GetLangZParameterAREN();

                        char LangZ = GetLangZParameter();
                        string lang = UtilityManager.GetLanguageParameter();
                        String url = Constants.SaveVATRegistration + LangZAREN;
                        vATRegistration.d.Langz = lang;
                        var uri = new Uri(url);
                        HttpClient client = new HttpClient(App.httpClientHandler);

                        client.DefaultRequestHeaders.Add("Token", "123");
                        client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

                        client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                        client.DefaultRequestHeaders.Add("Accept", "application/json");

                        var serilized = JsonConvert.SerializeObject(RequestVATRegistration);
                        HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                        HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                        var detailJson = res.Content.ReadAsStringAsync().Result;
                        _vATRegistration = JsonConvert.DeserializeObject<VATRegistrationDetails>(detailJson);
                        if (_vATRegistration != null)
                        {
                            if (_vATRegistration.d != null)
                            {
                                if (_vATRegistration.d.NOTESSet == null)
                                {
                                    NOTESSet nOTEs = new NOTESSet();
                                    nOTEs.results = new List<Note>();
                                    _vATRegistration.d.NOTESSet = nOTEs;
                                }
                                if (_vATRegistration.d.IBANSet == null)
                                {
                                    IBANSet iBANSet = new IBANSet();
                                    iBANSet.results = new List<Result2>();
                                    _vATRegistration.d.IBANSet = iBANSet;
                                }
                                if (_vATRegistration.d.ADDRESSSet == null)
                                {
                                    ADDRESSSet aDDRESSSet = new ADDRESSSet();
                                    aDDRESSSet.results = new List<ResultsItem>();
                                    _vATRegistration.d.ADDRESSSet = aDDRESSSet;
                                }
                                if (_vATRegistration.d.ATTDETSet == null)
                                {
                                    ATTDETSet aTTDETSet = new ATTDETSet();
                                    aTTDETSet.results = new List<Attachment>();
                                    _vATRegistration.d.ATTDETSet = aTTDETSet;
                                }
                                if (_vATRegistration.d.CONTACTDTSet == null)
                                {
                                    CONTACTDTSet cONTACTDT = new CONTACTDTSet();
                                    cONTACTDT.results = new List<ResultsItemForContact>();
                                    _vATRegistration.d.CONTACTDTSet = cONTACTDT;
                                }
                                if (_vATRegistration.d.CONTACT_PERSONSet == null)
                                {
                                    CONTACT_PERSONSet cONTACT_PERSONSet = new CONTACT_PERSONSet();
                                    cONTACT_PERSONSet.results = new List<ResultsItemForContactPerson>();
                                    _vATRegistration.d.CONTACT_PERSONSet = cONTACT_PERSONSet;
                                }
                                if (_vATRegistration.d.ELGBL_DOCSet == null)
                                {
                                    ELGBL_DOCSet eLGBL_DOC = new ELGBL_DOCSet();
                                    eLGBL_DOC.results = new List<ResultsItemForElgblDocSet>();
                                    _vATRegistration.d.ELGBL_DOCSet = eLGBL_DOC;
                                }
                                if (_vATRegistration.d.QUESTIONSSet == null)
                                {
                                    QUESTIONSSet qUESTIONSSet = new QUESTIONSSet();
                                    qUESTIONSSet.results = new List<ResultsItemForQuestion>();
                                    _vATRegistration.d.QUESTIONSSet = qUESTIONSSet;
                                }
                                if (_vATRegistration.d.QUESCONFIG_MSet == null)
                                {
                                    QUESCONFIG_MSet qUESCONFIG = new QUESCONFIG_MSet();
                                    qUESCONFIG.results = new List<QuestionsetWithMinMax>();
                                    _vATRegistration.d.QUESCONFIG_MSet = qUESCONFIG;
                                }
                                if (_vATRegistration.d.QUESLISTSet == null)
                                {
                                    QUESLISTSet qUESLIST = new QUESLISTSet();
                                    qUESLIST.results = new List<string>();
                                    _vATRegistration.d.QUESLISTSet = qUESLIST;
                                }




                            }
                        }
                        if (_vATRegistration == null || _vATRegistration.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                ErrorMessageForVAT = errorMesg.error.innererror.errordetails[0].message;
                                ErrorMessageForVAT += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = ErrorMessageForVAT.Replace("An exception was raised", string.Empty);
                                ErrorMessageForVAT = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(ErrorMessageForVAT);
                            }
                        }
                        return _vATRegistration;
                    }
                    return _vATRegistration;
                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
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

        public static async Task<VATDeRegistrationDetails> SaveVATDeRegistrationData(VATDeRegistrationDetails vATDeRegistration)
        {
            VATDeRegistrationDetails RequestVATDeRegistration = new VATDeRegistrationDetails();
            VATDeRegistrationDetails _vATDeRegistration = new VATDeRegistrationDetails();
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    if (vATDeRegistration != null && vATDeRegistration.d != null)
                    {
                        if (vATDeRegistration.d != null)
                        {
                            //if (vATRegistration.d.ELGBL_DOCSet != null)
                            //{


                            //    //foreach (var item in vATRegistration.d.ELGBL_DOCSet.results)
                            //    //{
                            //    //    if (item.Txt50 == null)
                            //    //    {
                            //    //        item.Txt50 = string.Empty;
                            //    //    }
                            //    //}
                            //}
                            RequestVATDeRegistration = vATDeRegistration;

                            //RequestVATDeclaration.d.SubmitFg = "";
                            AttdetSet aTTACHSet = new AttdetSet();
                            aTTACHSet.results = new List<Attachment>();
                            RequestVATDeRegistration.d.AttdetSet = aTTACHSet;
                            //if (vATRegistration.d.VatTaxDt == null)
                            //{
                            //    vATRegistration.d.VatTaxDt = "";
                            //}


                        }

                        //string url = Constants.GAZTSignUpFirstSubmit;
                        string LangZAREN = GetLangZParameterAREN();

                        char LangZ = GetLangZParameter();
                        string lang = UtilityManager.GetLanguageParameter();
                        String url = Constants.SaveVATDeRegistration;
                        vATDeRegistration.d.Langx = lang;
                        var uri = new Uri(url);
                        HttpClient client = new HttpClient(App.httpClientHandler);

                        client.DefaultRequestHeaders.Add("Token", "123");
                        client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

                        client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                        client.DefaultRequestHeaders.Add("Accept", "application/json");

                        var serilized = JsonConvert.SerializeObject(RequestVATDeRegistration);
                        HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                        HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                        var detailJson = res.Content.ReadAsStringAsync().Result;
                        _vATDeRegistration = JsonConvert.DeserializeObject<VATDeRegistrationDetails>(detailJson);
                        if (_vATDeRegistration != null)
                        {
                            if (_vATDeRegistration.d != null)
                            {
                                if (_vATDeRegistration.d.NotesSet == null)
                                {
                                    NotesSet nOTEs = new NotesSet();
                                    nOTEs.results = new List<VATDeregNote>();
                                    _vATDeRegistration.d.NotesSet = nOTEs;
                                }


                                if (_vATDeRegistration.d.AttdetSet == null)
                                {
                                    AttdetSet aTTDETSet = new AttdetSet();
                                    aTTDETSet.results = new List<Attachment>();
                                    _vATDeRegistration.d.AttdetSet = aTTDETSet;
                                }

                                if (_vATDeRegistration.d.AddressSet == null)
                                {
                                    AddressSet addressSet = new AddressSet();
                                    addressSet.results = new List<ResultsItemSet>();
                                    _vATDeRegistration.d.AddressSet = addressSet;
                                }

                                if (_vATDeRegistration.d.QuesListSet == null)
                                {
                                    QuesListSet quesList = new QuesListSet();
                                    quesList.results = new List<string>();
                                    _vATDeRegistration.d.QuesListSet = quesList;
                                }




                            }
                        }
                        if (_vATDeRegistration == null || _vATDeRegistration.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                ErrorMessageForVAT = errorMesg.error.innererror.errordetails[0].message;
                                ErrorMessageForVAT += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = ErrorMessageForVAT.Replace("An exception was raised", string.Empty);
                                ErrorMessageForVAT = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(ErrorMessageForVAT);
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

        public static async Task<UnlockAccountResponseModel> GaztUnlockAccount(UnlockAccountModel unlockAccountModel)
        {
            UnlockAccountResponseModel _unlockResponseModel = new UnlockAccountResponseModel();

            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    if (unlockAccountModel != null)
                    {
                        //string url = Constants.GAZTSignUpFirstSubmit;
                        string LangZAREN = GetLangZParameterAREN();

                        char LangZ = GetLangZParameter();
                        string lang = UtilityManager.GetLanguageParameter();
                        String url = Constants.GAZTUnlockAccountAllOperations;

                        unlockAccountModel.Language = LangZ.ToString();
                        unlockAccountModel.UserLocked = "L";

                        var uri = new Uri(url);
                        HttpClient client = new HttpClient(App.httpClientHandler);

                        client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                        client.DefaultRequestHeaders.Add("Accept", "application/json");

                        var serilized = JsonConvert.SerializeObject(unlockAccountModel);
                        HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                        HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                        var detailJson = res.Content.ReadAsStringAsync().Result;
                        _unlockResponseModel = JsonConvert.DeserializeObject<UnlockAccountResponseModel>(detailJson);

                        if (_unlockResponseModel == null || _unlockResponseModel.D == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                ErrorMessageForUnlockAccount = errorMesg.error.innererror.errordetails[0].message;
                                ErrorMessageForUnlockAccount += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = ErrorMessageForUnlockAccount.Replace("An exception was raised", string.Empty);
                                ErrorMessageForUnlockAccount = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTUnlockAccountException(ErrorMessageForUnlockAccount);
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

        public static async Task<UnlockAccountResponseModel> GaztUnlockAccountOtp(UnlockAccountModelOtp unlockAccountModel)
        {
            UnlockAccountResponseModel _unlockResponseModel = new UnlockAccountResponseModel();

            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    if (unlockAccountModel != null)
                    {
                        //string url = Constants.GAZTSignUpFirstSubmit;
                        string LangZAREN = GetLangZParameterAREN();

                        char LangZ = GetLangZParameter();
                        string lang = UtilityManager.GetLanguageParameter();
                        String url = Constants.GAZTUnlockAccountAllOperations;

                        unlockAccountModel.Language = LangZ.ToString();
                        unlockAccountModel.UserLocked = "L";

                        var uri = new Uri(url);
                        HttpClient client = new HttpClient(App.httpClientHandler);

                        client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                        client.DefaultRequestHeaders.Add("Accept", "application/json");

                        var serilized = JsonConvert.SerializeObject(unlockAccountModel);
                        HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                        HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                        var detailJson = res.Content.ReadAsStringAsync().Result;
                        _unlockResponseModel = JsonConvert.DeserializeObject<UnlockAccountResponseModel>(detailJson);

                        if (_unlockResponseModel == null || _unlockResponseModel.D == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                ErrorMessageForUnlockAccount = errorMesg.error.innererror.errordetails[0].message;
                                ErrorMessageForUnlockAccount += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = ErrorMessageForUnlockAccount.Replace("An exception was raised", string.Empty);
                                ErrorMessageForUnlockAccount = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTUnlockAccountException(ErrorMessageForUnlockAccount);
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

        public static async Task<UnlockAccountResponseModel> GaztUnlockAccountChangePassword(UnlockAccountModelChangePassword unlockAccountModel)
        {
            UnlockAccountResponseModel _unlockResponseModel = new UnlockAccountResponseModel();

            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    if (unlockAccountModel != null)
                    {
                        //string url = Constants.GAZTSignUpFirstSubmit;
                        string LangZAREN = GetLangZParameterAREN();

                        char LangZ = GetLangZParameter();
                        string lang = UtilityManager.GetLanguageParameter();
                        String url = Constants.GAZTUnlockAccountAllOperations;

                        unlockAccountModel.Language = LangZ.ToString();
                        unlockAccountModel.UserLocked = "L";

                        var uri = new Uri(url);
                        HttpClient client = new HttpClient(App.httpClientHandler);

                        client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                        client.DefaultRequestHeaders.Add("Accept", "application/json");

                        var serilized = JsonConvert.SerializeObject(unlockAccountModel);
                        HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                        HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                        var detailJson = res.Content.ReadAsStringAsync().Result;
                        _unlockResponseModel = JsonConvert.DeserializeObject<UnlockAccountResponseModel>(detailJson);

                        if (_unlockResponseModel == null || _unlockResponseModel.D == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                ErrorMessageForUnlockAccount = errorMesg.error.innererror.errordetails[0].message;
                                ErrorMessageForUnlockAccount += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = ErrorMessageForUnlockAccount.Replace("An exception was raised", string.Empty);
                                ErrorMessageForUnlockAccount = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTUnlockAccountException(ErrorMessageForUnlockAccount);
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

        //public static async Task<VATRegistrationDetails> SaveVATRegistrationData(VATRegistrationDetails vATRegistrationDetails)
        //{
        //    VATRegistrationDetails RequestVATDeclaration = new VATRegistrationDetails();
        //    VATRegistrationDetails _vATDeclarationD = new VATRegistrationDetails();
        //    if (CrossConnectivity.Current.IsConnected)
        //    {
        //        try
        //        {
        //            if (vATRegistrationDetails != null && vATRegistrationDetails.d != null)
        //            {
        //                if (vATRegistrationDetails.d != null)
        //                {
        //                    RequestVATDeclaration = vATRegistrationDetails;


        //                }
        //                char LangZ = GetLangZParameter();
        //                string lang = UtilityManager.GetLanguageParameter();
        //                String url = Constants.SaveVATRegistrationData;
        //                var uri = new Uri(url);
        //                HttpClient client = new HttpClient(App.httpClientHandler);

        //                client.DefaultRequestHeaders.Add("Token", "123");
        //                client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

        //                client.DefaultRequestHeaders.Add("X-Requested-With", "X");
        //                client.DefaultRequestHeaders.Add("Accept", "application/json");

        //                var serilized = JsonConvert.SerializeObject(RequestVATDeclaration);
        //                HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
        //                HttpResponseMessage res = await client.PostAsync(uri, contentPost);
        //                var detailJson = res.Content.ReadAsStringAsync().Result;
        //                _vATDeclarationD = JsonConvert.DeserializeObject<VATDeclaration>(detailJson);
        //                if (_vATDeclarationD != null)
        //                {
        //                    if (_vATDeclarationD.d != null)
        //                    {
        //                        if (_vATDeclarationD.d.NOTESSet == null)
        //                        {
        //                            NOTESSet nOTEs = new NOTESSet();
        //                            nOTEs.results = new List<Note>();
        //                            _vATDeclarationD.d.NOTESSet = nOTEs;
        //                        }
        //                        if (_vATDeclarationD.d.IBANSet == null)
        //                        {
        //                            IBANSet iBANSet = new IBANSet();
        //                            iBANSet.results = new List<Result2>();
        //                            _vATDeclarationD.d.IBANSet = iBANSet;
        //                        }
        //                        if (_vATDeclarationD.d.CFSet == null)
        //                        {
        //                            CFSet cFSet = new CFSet();
        //                            cFSet.results = new List<Result3>();
        //                            _vATDeclarationD.d.CFSet = cFSet;
        //                        }
        //                        if (_vATDeclarationD.d.ATTACHSet == null)
        //                        {
        //                            ATTACHSet aTTACHSet = new ATTACHSet();
        //                            aTTACHSet.results = new List<Attachment>();
        //                            _vATDeclarationD.d.ATTACHSet = aTTACHSet;
        //                        }
        //                        if (_vATDeclarationD.d.ADRSet == null)
        //                        {
        //                            ADRSet aDRSet = new ADRSet();
        //                            aDRSet.results = new List<Result5>();
        //                            _vATDeclarationD.d.ADRSet = aDRSet;
        //                        }
        //                        if (_vATDeclarationD.d.VATR_MSGSet == null)
        //                        {
        //                            VATRMSGSet vATRMSGSet = new VATRMSGSet();
        //                            vATRMSGSet.results = new List<object>();
        //                            _vATDeclarationD.d.VATR_MSGSet = vATRMSGSet;
        //                        }
        //                        if (_vATDeclarationD.d.VATPERITEMSet == null)
        //                        {
        //                            VATPERITEMSet vATPERITEMSet = new VATPERITEMSet();
        //                            vATPERITEMSet.results = new List<Result6>();
        //                            _vATDeclarationD.d.VATPERITEMSet = vATPERITEMSet;
        //                        }
        //                    }
        //                }
        //                if (_vATDeclarationD == null || _vATDeclarationD.d == null)
        //                {
        //                    ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
        //                    if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
        //                    {
        //                        ErrorMessageForVAT = errorMesg.error.innererror.errordetails[0].message;
        //                        ErrorMessageForVAT += errorMesg.error.innererror.errordetails[1].message;
        //                        String WithReplacedString = ErrorMessageForVAT.Replace("An exception was raised", string.Empty);
        //                        ErrorMessageForVAT = WithReplacedString;
        //                        //ErrorMessageForVAT
        //                    }
        //                }
        //                return _vATDeclarationD;
        //            }
        //            return _vATDeclarationD;
        //        }
        //        catch (Exception ex)
        //        {
        //            return null;
        //        }
        //    }
        //    else
        //    {
        //        throw new InternetException(AppResources.ZZInternetConnectionMessage);
        //    }
        //}
        #endregion


        #region Form5
        #region ZakatForm5

        /// <summary>
        /// Basic Information and Financial Information
        /// </summary>
        /// <param name="Fbguid"></param>
        /// <returns></returns>
         #region Basic and Financial Information
        public static async Task<ZakatForm5DataResult> GAZTZakatForm5Data(string Fbguid)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                ZakatForm5DataResult ZakatForm5DataResultSet = new ZakatForm5DataResult();
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    char lang = GetLangZParameter();
                    string Lang = "";
                    if (App.IsArabic == true)
                    {
                        Lang = "A";
                    }
                    else
                    {
                        Lang = "E";
                    }

                    //string url = Constants.Z_RET_F05_ZKTE+"(Auditorz='',Taxpayerz='',RegIdz='',PeriodKeyz='',Submitz='',Savez='',Fbnumz='',Langz='E',OfficerUidz='',ObjSubmitz='',Approvez='',Rejectz='',CreateTxAssesz='',Euser='00000000000000000000',Fbguid='005056B1F8FB1EEAB680B9ED5E358961')?&$expand=GEN_SUB_SCH,GP03_2Set,GP03_3Set,GP03_4Set,GP03_5Set,GP03_6Set,GP03_7Set,GP03_8Set,GP06_1Set,GP06_2Set,GP06_3Set,MAIN_ACTIVITYSet,SCH_GP01,SCH_GP02,SCH_GP03,SCH_GP04,SCH_GP05,SCH_GP06,SCH_GP07,SCH_GP08,SCH_GP09,SCH_GP10,SCH_GP11,SCH_GP12,SUB_SCH_CAPITALSet,SCH_200Set,SCH_800Set,SCH_GP3S1Set,SCH_GP3S2Set,AttDetSet,LONG_TEXTSet";
                    string url = Constants.Z_RET_F05_ZKTE + "(Auditorz='',Taxpayerz='',RegIdz='',PeriodKeyz='',Submitz='',Savez='',Fbnumz='',Langz='" + Lang + "',OfficerUidz='',ObjSubmitz='',Approvez='',Rejectz='',CreateTxAssesz='',Euser='" + App.TP.Userid + "',Fbguid='" + Fbguid + "')?&$expand=GEN_SUB_SCH,GP03_2Set,GP03_3Set,GP03_4Set,GP03_5Set,GP03_6Set,GP03_7Set,GP03_8Set,GP06_1Set,GP06_2Set,GP06_3Set,MAIN_ACTIVITYSet,SCH_GP01,SCH_GP02,SCH_GP03,SCH_GP04,SCH_GP05,SCH_GP06,SCH_GP07,SCH_GP08,SCH_GP09,SCH_GP10,SCH_GP11,SCH_GP12,SUB_SCH_CAPITALSet,SCH_200Set,SCH_800Set,SCH_GP3S1Set,SCH_GP3S2Set,AttDetSet,LONG_TEXTSet";

                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    var uri = new Uri(url);
                    HttpResponseMessage GAZTZakatForm5Response = await client.GetAsync(uri);
                    if (GAZTZakatForm5Response != null)
                    {
                        if (GAZTZakatForm5Response.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTZakatForm5Response.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                            App.IsSessionExpired = false;
                        }
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expired")) || (0 == String.Compare(NewToken, "Invalid Token")))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }

                        String GAZTZakatForm5ResponseJSON = GAZTZakatForm5Response.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(GAZTZakatForm5ResponseJSON))
                        {
                            GAZTZakatForm5ResponseJSON = JObject.Parse(GAZTZakatForm5ResponseJSON)["d"].ToString();

                            ZakatForm5DataResultSet = JsonConvert.DeserializeObject<ZakatForm5DataResult>(GAZTZakatForm5ResponseJSON);
                            if (ZakatForm5DataResultSet == null)
                            {
                                throw new Exception(AppResources.NoTINsAvailable);
                            }
                        }
                        else
                        {
                            throw new Exception(AppResources.ZNoICRAvailable);
                        }
                    }
                    return ZakatForm5DataResultSet;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
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

        /// <summary>
        /// Form5 City API
        /// </summary>
        /// <returns></returns>

        #region City
        public static async Task<ZakatForm5CityDataResult> GAZTZakatForm5CityData()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                ZakatForm5CityDataResult ZakatForm5CityDataResultSet = new ZakatForm5CityDataResult();
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    char lang = GetLangZParameter();
                    string Lang = "";
                    if (App.IsArabic == true)
                    {
                        Lang = "AR";
                    }
                    else
                    {
                        Lang = "EN";
                    }



                    //string url = Constants.Z_RET_F05_ZKTE+"(Auditorz='',Taxpayerz='',RegIdz='',PeriodKeyz='',Submitz='',Savez='',Fbnumz='',Langz='E',OfficerUidz='',ObjSubmitz='',Approvez='',Rejectz='',CreateTxAssesz='',Euser='00000000000000000000',Fbguid='005056B1F8FB1EEAB680B9ED5E358961')?&$expand=GEN_SUB_SCH,GP03_2Set,GP03_3Set,GP03_4Set,GP03_5Set,GP03_6Set,GP03_7Set,GP03_8Set,GP06_1Set,GP06_2Set,GP06_3Set,MAIN_ACTIVITYSet,SCH_GP01,SCH_GP02,SCH_GP03,SCH_GP04,SCH_GP05,SCH_GP06,SCH_GP07,SCH_GP08,SCH_GP09,SCH_GP10,SCH_GP11,SCH_GP12,SUB_SCH_CAPITALSet,SCH_200Set,SCH_800Set,SCH_GP3S1Set,SCH_GP3S2Set,AttDetSet,LONG_TEXTSet";
                    //string url = Constants.Z_RET_F05_ZKTE + "(Auditorz='',Taxpayerz='',RegIdz='',PeriodKeyz='',Submitz='',Savez='',Fbnumz='',Langz='E',OfficerUidz='',ObjSubmitz='',Approvez='',Rejectz='',CreateTxAssesz='',Euser='" + App.TP.Userid + "',Fbguid='" + Fbguid + "')?&$expand=GEN_SUB_SCH,GP03_2Set,GP03_3Set,GP03_4Set,GP03_5Set,GP03_6Set,GP03_7Set,GP03_8Set,GP06_1Set,GP06_2Set,GP06_3Set,MAIN_ACTIVITYSet,SCH_GP01,SCH_GP02,SCH_GP03,SCH_GP04,SCH_GP05,SCH_GP06,SCH_GP07,SCH_GP08,SCH_GP09,SCH_GP10,SCH_GP11,SCH_GP12,SUB_SCH_CAPITALSet,SCH_200Set,SCH_800Set,SCH_GP3S1Set,SCH_GP3S2Set,AttDetSet,LONG_TEXTSet";
                    // https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/Z_F05_DROPDOWN_SRV/HeaderSet(Langu='AR',Country='SA')?&$expand=zcitySet,zmain_descSet,zsub_desc_A60Set,zsub_desc_A61Set,zsub_desc_A62Set,URLSet,MSGSet,GOVCODESet
                    string url = Constants.Z_RET_F05_City + "(Langu='" + Lang + "',Country='SA')?&$expand=zcitySet,zmain_descSet,zsub_desc_A60Set,zsub_desc_A61Set,zsub_desc_A62Set,URLSet,MSGSet,GOVCODESet";

                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    var uri = new Uri(url);
                    HttpResponseMessage GAZTZakatForm5CityResponse = await client.GetAsync(uri);
                    if (GAZTZakatForm5CityResponse != null)
                    {
                        if (GAZTZakatForm5CityResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTZakatForm5CityResponse.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                            App.IsSessionExpired = false;
                        }
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expired")) || (0 == String.Compare(NewToken, "Invalid Token")))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }

                        String GAZTZakatForm5ResponseJSON = GAZTZakatForm5CityResponse.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(GAZTZakatForm5ResponseJSON))
                        {
                            GAZTZakatForm5ResponseJSON = JObject.Parse(GAZTZakatForm5ResponseJSON)["d"].ToString();

                            ZakatForm5CityDataResultSet = JsonConvert.DeserializeObject<ZakatForm5CityDataResult>(GAZTZakatForm5ResponseJSON);
                            if (ZakatForm5CityDataResultSet == null)
                            {
                                throw new Exception(AppResources.NoTINsAvailable);
                            }
                        }
                        else
                        {
                            throw new Exception(AppResources.ZNoICRAvailable);
                        }
                    }
                    return ZakatForm5CityDataResultSet;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
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


        /// <summary>
        /// Zakat Estimation Summary API
        /// </summary>
        /// <param name="Fbnum"></param>
        /// <returns></returns>

        #region Zakat Estimation Summary
        public static async Task<ZakatForm5SummaryResult> GAZTZakatForm5DataSummary(string Fbnum)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                ZakatForm5SummaryResult ZakatForm5SummaryResultSet = new ZakatForm5SummaryResult();
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string url = Constants.Z_ZKTE_SUMMARY + "(Fbnum='" + Fbnum + "',Flag='X')?$expand=headsumSet,SadadSet,SchGP01Set,SchGP02Set,SchGP03Set,SchGP04Set,SchGP05Set,SchGP06Set,SchGP07Set,SchGP08Set,SchGP09Set,SchGP10Set,SchGP11Set,SchGP12Set";
                    // string url = "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/Z_ZKTE_SUMMARY_SRV/HeadSet(Fbnum='23001410241',Flag='X')?$expand=headsumSet,SadadSet,SchGP01Set,SchGP02Set,SchGP03Set,SchGP04Set,SchGP05Set,SchGP06Set,SchGP07Set,SchGP08Set,SchGP09Set,SchGP10Set,SchGP11Set,SchGP12Set";
                    // string url = Constants.Z_ZKTE_SUMMARY + "(Fbnum='"+Fbnum+"'',Flag='X')?$expand=headsumSet,SadadSet,SchGP01Set,SchGP02Set,SchGP03Set,SchGP04Set,SchGP05Set,SchGP06Set,SchGP07Set,SchGP08Set,SchGP09Set,SchGP10Set,SchGP11Set,SchGP12Set";

                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    var uri = new Uri(url);
                    HttpResponseMessage GAZTZakatForm5SummaryResponse = await client.GetAsync(uri);
                    if (GAZTZakatForm5SummaryResponse != null)
                    {
                        if (GAZTZakatForm5SummaryResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTZakatForm5SummaryResponse.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                            App.IsSessionExpired = false;
                        }
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expired")) || (0 == String.Compare(NewToken, "Invalid Token")))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }

                        String GAZTZakatForm5SummaryResponseJSON = GAZTZakatForm5SummaryResponse.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(GAZTZakatForm5SummaryResponseJSON))
                        {
                            GAZTZakatForm5SummaryResponseJSON = JObject.Parse(GAZTZakatForm5SummaryResponseJSON)["d"].ToString();

                            ZakatForm5SummaryResultSet = JsonConvert.DeserializeObject<ZakatForm5SummaryResult>(GAZTZakatForm5SummaryResponseJSON);
                            if (ZakatForm5SummaryResultSet == null)
                            {
                                throw new Exception(AppResources.NoTINsAvailable);
                            }
                        }
                        else
                        {
                            throw new Exception(AppResources.ZNoICRAvailable);
                        }
                    }
                    return ZakatForm5SummaryResultSet;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
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
        #endregion
        #endregion

        #region VAT Deregistration
        public static async Task<AttachmentRootOject> GAZTSaveVATDeregAttachment(byte[] AttachmentByte, string fileName, string RetGuid, string Dotyp, string contentType)//, string returnedFguid
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    AttachmentRootOject _attachment = new AttachmentRootOject();
                    char LangZ = GetLangZParameter();
                    string AttBy = "TP";
                    String url = Constants.GAZTSaveAttachment + "'" + "'" + ",RetGuid='" + RetGuid + "'" + ",Flag='" + "N" + "'" + ",Dotyp='" + Dotyp + "'" + ",SchGuid='" + "'" + ",Srno=" + "1" + ",Doguid='" + "'" + ",AttBy='" + AttBy + "'" + ")/AttachMedSet";

                    // String url = Constants.GAZTSaveVATDeregAttachment + "'" + "'" + ",RetGuid='" + RetGuid + "'" + ",Flag='" + "N" + "'" + ",Dotyp='" + Dotyp + "'" + ",SchGuid='" + "'" + ",Srno=" + "1" + ",Doguid='" + "'" + ",AttBy='" + AttBy + "'" + ")/AttachMedSet";
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
        public static string GAZTDeleteVATDeRegistrationAttachment(string fileName, string RetGuid, string Dotyp)//, string returnedFguid
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                string DeleteToken = string.Empty;
                try
                {
                    AttachmentRootOject _attachment = new AttachmentRootOject();
                    char LangZ = GetLangZParameter();
                    //string Dotyp = "VTA0";
                    string AttBy = "TP";
                    // String url = "https://sapgatewayqa.gazt.gov.sa:443/sap/opu/odata/SAP/ZDP_INDTAX_ATT_SRV/AttachSet(OutletRef='',RetGuid='005056B1F8FB1EDA8FF041CFF05E83A9',Flag='N',Dotyp='VTA0',SchGuid='',Srno=1,Doguid='',AttBy='TP')/AttachMedSet";// Constants.SaveVATDeclarationData;
                    String url = Constants.GAZTDeteleAttachment + "'" + "'" + ",RetGuid='undefined'" + ",Flag='" + "N" + "'" + ",Dotyp='" + Dotyp + "'" + ",SchGuid='" + "'" + ",Srno=" + "1" + ",Doguid='" + RetGuid + "'" + ",AttBy='" + AttBy + "'" + ")/$value?saml2=enabled"; //",RetGuid='005056B1F8FB1EDA8FF041CFF05E83A9',Flag='N',Dotyp='VTA0',SchGuid='',Srno=1,Doguid='',AttBy='TP')/AttachMedSet";// Constants.SaveVATDeclarationData;
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

        #region VATDeregistration Reason

        public static VATDeregistrationModelRootObject GAZTGETVATDeregReasonDropdownList(string selectedType)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                VATDeregistrationModelRootObject reasonData = new VATDeregistrationModelRootObject();
                // ObservableCollection<VATDeregistrationReasonModel> reasonDropdownlist = new ObservableCollection<VATDeregistrationReasonModel>();
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    char lang = GetLangZParameter();
                    string url = Constants.GAZTGETVATDeregReasonDropdownList + " eq " + "'" + selectedType + "'" + " and " + "Lang" + " eq " + "'" + lang + "'" + "&$format=json";

                    client.DefaultRequestHeaders.Add("Accept", "application/json");

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
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }

                        String GAZTVATDeregreasonDataResponseJSON = GAZTVATDeregreasonDataResponse.Content.ReadAsStringAsync().Result;
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

        #region VATDeregistration Attachment DocumentType

        public static async Task<VATDeRegistrationAttachmentDropdownDetails> GAZTGETVATDeregAttachmentsDropdownList(string selectedType)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                VATDeRegistrationAttachmentDropdownDetails vATDeregAttDetails = new VATDeRegistrationAttachmentDropdownDetails();
                string NewToken = string.Empty;
                try
                {
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    //  https://sapgatewayqa.gazt.gov.sa:443/sap/opu/odata/SAP/ZDP_VDRUH_SRV/VR_UI_HDRSet(Fbnum='',Lang='E',Officer='',Gpart='3101937624',Status='E0001',TxnTp='VT_DREG',Formproc='ZTAX_VT_REG')?&$expand=VR_UI_BTNSet,ELGBL_DOCSet&$format=json

                    string status = "E0001";

                    String url = Constants.GAZTGETVATDeregAttachmentsDropdownList + "',Lang='" + lang + "',Officer='" + "',Gpart='" + App.LoginDataRetrieved.TIN + "',Status='" + status + "',TxnTp='" + selectedType + "',Formproc='ZTAX_VT_REG'" + ")?&$expand=VR_UI_BTNSet,ELGBL_DOCSet&$format=json";
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
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        String VatDeregAttListResultModelSetResponseJson = GAZTVATDeregAttDataResponse.Content.ReadAsStringAsync().Result;
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
        public async static Task<VATRegistrationOtherDetails> GAZTGetVATDeRegistrationDataWithButtons(string Fbnumz, string Officerz, string Status, string TxnTp, string Formproc)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                VATRegistrationOtherDetails vATRegistrationOtherDetails = new VATRegistrationOtherDetails();
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    Char lang = WebServiceManager.GetLangZParameter();
                    String url = Constants.GAZTGetVATRegistrationOtherDetails + Fbnumz + "',Lang='" + lang + "',Officer='" + Officerz + "',Gpart='" + App.LoginDataRetrieved.TIN + "',Status='" + Status + "',TxnTp='" + "CRE_RGVT" + "',Formproc='" + "ZTAX_VT_REG" + "')?&$expand=VR_UI_BTNSet,ELGBL_DOCSet&$format=json";
                    client.DefaultRequestHeaders.Add("Token", "123");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
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
                        vATRegistrationOtherDetails = JsonConvert.DeserializeObject<VATRegistrationOtherDetails>(VatRegistrationOtherData);

                        if (!string.IsNullOrEmpty(VatRegistrationOtherData) && vATRegistrationOtherDetails == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(VatRegistrationOtherData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new Exception(errorMessage);
                            }
                        }

                    }
                    return vATRegistrationOtherDetails;
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

        #region VATDeregistration Reason

        public async static Task<VATDeregistrationLastICRDateRootObject> GAZTGETVATDeregSuspensionDate(string selectedType)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                VATDeregistrationLastICRDateRootObject reasonData = new VATDeregistrationLastICRDateRootObject();
                // ObservableCollection<VATDeregistrationReasonModel> reasonDropdownlist = new ObservableCollection<VATDeregistrationReasonModel>();
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    char lang = GetLangZParameter();
            //    https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_DREG_SRV/GetLastICRDtSet?$filter=Gpartx%20eq%20%273001105571%27%20and%20UserTypx%20eq%20%27TP%27%20and%20TxnTpx%20eq%20%27ZVAT_SUSP%27%20and%20Reqtp%20eq%20%27S%27&$format=json

                    string url = Constants.GAZTGETVATDeregSuspensionDate +"Gpartx"+ " eq " +"'"+ App.LoginDataRetrieved.TIN +"'"+" and "+ "UserTypx" + " eq " +"'TP'"+ " and "+"TxnTpx" + " eq " + "'" + "ZVAT_SUSP"+ "'" + " and " + "Reqtp"+ " eq "  + "'S'"+ "&$format=json";

                    client.DefaultRequestHeaders.Add("Accept", "application/json");

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
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }

                        String GAZTVATDeregreasonDataResponseJSON = GAZTVATDeregreasonDataResponse.Content.ReadAsStringAsync().Result;
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

        #region VATDeregistration Reason

        public static VATDeregistrationSuspendedDateRootObject GAZTGETVATDeregReturnFilingDateList(DateTime StartDate, DateTime EndDate)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                VATDeregistrationSuspendedDateRootObject reasonData = new VATDeregistrationSuspendedDateRootObject();
                // ObservableCollection<VATDeregistrationReasonModel> reasonDropdownlist = new ObservableCollection<VATDeregistrationReasonModel>();
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    char lang = GetLangZParameter();

                    string startDate = StartDate.Year.ToString() + "-" + StartDate.Month.ToString() + "-" + StartDate.Day.ToString() + "T" + StartDate.Hour.ToString() + ":" + StartDate.Minute.ToString();
                    string endDate = EndDate.Year.ToString() + "-" + EndDate.Month.ToString() + "-" + EndDate.Day.ToString() + "T" + EndDate.Hour.ToString() + ":" + EndDate.Minute.ToString();

                    string url = Constants.GAZTGETVATDeregReturnFilingDateList + "Gpart" + " eq " + "'" + App.LoginDataRetrieved.TIN + "'" + " and " + "StartDate" + " eq datetime" + "'" + startDate + "'" + " and " + "EndDate" + " eq datetime" + "'" + endDate + "'" + "&$format=json";

                    client.DefaultRequestHeaders.Add("Accept", "application/json");

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
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }

                        String GAZTVATDeregreasonDataResponseJSON = GAZTVATDeregreasonDataResponse.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(GAZTVATDeregreasonDataResponseJSON))
                        {
                            reasonData = JsonConvert.DeserializeObject<VATDeregistrationSuspendedDateRootObject>(GAZTVATDeregreasonDataResponseJSON);
                            if (reasonData.d == null)
                            {
                                throw new Exception(AppResources.VatDeregSuspendedDateMismatchException);
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


        public static async Task<VatRefundsListResultModel> GAZTGetVAtRefundList()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                VatRefundsListResultModel VatRefundsListResultModelSet = new VatRefundsListResultModel();
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    char lang = GetLangZParameter();

                    //https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_ETRF_WI_SRV/WISet(TaxType='VT',Lang='E',
                    //Gpart='3102435227',Euser='',Flag='W',Fbguid='')?&$expand=STATUSSet,WI_DTLSet,VatRef_HeaderSet,
                    //VatRef_SubItemsSet&$format=json

                    string url = Constants.VatRefundList + "(TaxType='VT',Lang='" + lang + "',Gpart='" + App.LoginDataRetrieved.TIN + "',Euser='',Flag='W',Fbguid='')?&$expand=STATUSSet,WI_DTLSet,VatRef_HeaderSet,VatRef_SubItemsSet&$format=json";
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

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
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expired")) || (0 == String.Compare(NewToken, "Invalid Token")))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }

                            String VatRefundsListResultModelSetResponseJson = VatRefundsResponse.Content.ReadAsStringAsync().Result;
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
                    Console.WriteLine(ex);
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
            if (CrossConnectivity.Current.IsConnected)
            {
                VatRefundDisplayDataModel VatRefundDisplayDataModel = new VatRefundDisplayDataModel();
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string lang = GetLangZParameterAREN();

                    //https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RF_SRV/HeaderSet(Euser='00000000000000000000',Fbnumx='87000001021',
                    //FormGuid='005056B1F8FB1EDAAEBDD2D56B2AB92F',Formprocx='ZTAX_VAT_MAISC_PROC',Gpartx='3102435227',Langx='EN',Officerx='',TxnTpx='')
                    ///sap/opu/odata/SAP/ZDP_VAT_NW_RF_SRV/HeaderSet(Euser='00000000001000083299',Fbnumx='',FormGuid='005056B1365C1EEAB6E0F7222AF68000',
                    ///Formprocx='ZTAX_VAT_MAISC_PROC',Gpartx='',Langx='EN',Officerx='',TxnTpx='')?$expand=AttdetSet,BankDtlSet,NotesSet


                    //HeaderSet(Euser='',FormGuid='',Formprocx='ZTAX_VAT_MAISC_PROC',";
                    string url = Constants.VatRefundDisplayData + "FormGuid='" + formguid + "',Formprocx='ZTAX_VAT_MAISC_PROC',Gpartx='" + App.LoginDataRetrieved.TIN + "',Langx='" + lang + "',Officerx='',TxnTpx='')?$expand=AttdetSet,BankDtlSet,NotesSet&$format=json";
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

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
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expired")) || (0 == String.Compare(NewToken, "Invalid Token")))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }

                        String VatRefundsListResultModelSetResponseJson = VatRefundsResponse.Content.ReadAsStringAsync().Result;

                        if (VatRefundsResponse.StatusCode == HttpStatusCode.BadRequest)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(VatRefundsListResultModelSetResponseJson);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                ErrorMessageForUnlockAccount = errorMesg.error.innererror.errordetails[0].message;
                                String WithReplacedString = ErrorMessageForUnlockAccount.Replace("An exception was raised", string.Empty);
                                ErrorMessageForUnlockAccount = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTErrorException(ErrorMessageForUnlockAccount);
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
                    Console.WriteLine(ex);
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
            if (CrossConnectivity.Current.IsConnected)
            {
                VarRefundIbanDataModel VarRefundIbanDataModel = new VarRefundIbanDataModel();
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string lang = GetLangZParameterAREN();

                    //https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_RF_SRV/HeaderSet(Euser='00000000000000000000',Fbnumx='87000001021',
                    //FormGuid='005056B1F8FB1EDAAEBDD2D56B2AB92F',Formprocx='ZTAX_VAT_MAISC_PROC',Gpartx='3102435227',Langx='EN',Officerx='',TxnTpx='')

                    string url = Constants.VatRefundGetIbanData + "Gpart='" + App.LoginDataRetrieved.TIN + "',Status='',TxnTp='',Formproc='')?&$expand=VR_UI_BTNSet,IBANSet&$format=json";
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

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
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expired")) || (0 == String.Compare(NewToken, "Invalid Token")))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }

                        String VatRefundsListResultModelSetResponseJson = VatRefundsResponse.Content.ReadAsStringAsync().Result;
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
                catch (GAZTErrorException ex)
                {
                    Console.WriteLine(ex);
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

            if (CrossConnectivity.Current.IsConnected)
            {
                VatRefundDisplayDataModel _newRequestSummaryDataResponse = new VatRefundDisplayDataModel();
                string NewToken = string.Empty;
                _newRequestSummaryData.Agrfg = "X";
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string lang = GetLangZParameterAREN();
                    string url = Constants.VatRefundSubmitData;

                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");  

                    var uri = new Uri(url);
                    var serilized = JsonConvert.SerializeObject(_newRequestSummaryData);

                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
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
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expired")) || (0 == String.Compare(NewToken, "Invalid Token")))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }

                        String VatRefundsListResultModelSetResponseJson = VatRefundsResponse.Content.ReadAsStringAsync().Result;

                        if (VatRefundsResponse.StatusCode == HttpStatusCode.BadRequest)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(VatRefundsListResultModelSetResponseJson);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                ErrorMessageForUnlockAccount = errorMesg.error.innererror.errordetails[0].message;
                                String WithReplacedString = ErrorMessageForUnlockAccount.Replace("An exception was raised", string.Empty);
                                ErrorMessageForUnlockAccount = WithReplacedString;
                                //ErrorMessageForVAT
                                //throw new GAZTUnlockAccountException(ErrorMessageForUnlockAccount);
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
                    Console.WriteLine(ex);
                    throw new GAZTUnlockAccountException(ex.Message);
                }

                catch (Exception ex)
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

        #region Upload Attachements
        public static async Task<AttachmentRootOject> GAZTSaveAttachment(byte[] AttachmentByte, string fileName, string RetGuid, string Dotyp, string contentType)//, string returnedFguid
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    AttachmentRootOject _attachment = new AttachmentRootOject();
                    char LangZ = GetLangZParameter();
                    string AttBy = "TP";
                    String url = Constants.GAZTSaveAttachmentGeneric + "'" + "'" + ",RetGuid='" + RetGuid + "'" + ",Flag='" + "N" + "'" + ",Dotyp='" + Dotyp + "'" + ",SchGuid='" + "'" + ",Srno=" + "1" + ",Doguid='" + "'" + ",AttBy='" + AttBy + "'" + ")/AttachMedSet";
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


        public static string GAZTDeleteAttachment(string fileName, string RetGuid)//, string returnedFguid
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
                    // String url = "https://sapgatewayqa.gazt.gov.sa:443/sap/opu/odata/SAP/ZDP_INDTAX_ATT_SRV/AttachSet(OutletRef='',RetGuid='005056B1F8FB1EDA8FF041CFF05E83A9',Flag='N',Dotyp='VTA0',SchGuid='',Srno=1,Doguid='',AttBy='TP')/AttachMedSet";// Constants.SaveVATDeclarationData;
                    String url = Constants.GAZTDeteleAttachment + "'" + "'" + ",RetGuid='undefined'" + ",Flag='" + "N" + "'" + ",Dotyp='" + Dotyp + "'" + ",SchGuid='" + "'" + ",Srno=" + "1" + ",Doguid='" + RetGuid + "'" + ",AttBy='" + AttBy + "'" + ")/$value?saml2=enabled"; //",RetGuid='005056B1F8FB1EDA8FF041CFF05E83A9',Flag='N',Dotyp='VTA0',SchGuid='',Srno=1,Doguid='',AttBy='TP')/AttachMedSet";// Constants.SaveVATDeclarationData;
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


        public static async Task<AttachmentRootOject> GAZTGenericSaveAttachment(byte[] AttachmentByte, string fileName, string RetGuid, string Dotyp, string contentType, string apiServiceUrl)//, string returnedFguid
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    AttachmentRootOject _attachment = new AttachmentRootOject();
                    char LangZ = GetLangZParameter();
                    string AttBy = "TP";
                   if(Dotyp == null)
                    {
                        Dotyp = string.Empty;
                    }
                    String url = Constants.GAZTSaveAttachmentGeneric + "'" + "'" + ",RetGuid='" + RetGuid + "'" + ",Flag='" + "N" + "'" + ",Dotyp='" + Dotyp + "'" + ",SchGuid='" + "'" + ",Srno=" + "1" + ",Doguid='" + "'" + ",AttBy='" + AttBy + "'" + ")/AttachMedSet";
                    url = url.Replace("attachmentServiceurl", apiServiceUrl);
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


        public static string GAZTGenericDeleteAttachment(string fileName, string RetGuid, string aPiMethod)//, string returnedFguid
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
                    // String url = "https://sapgatewayqa.gazt.gov.sa:443/sap/opu/odata/SAP/ZDP_INDTAX_ATT_SRV/AttachSet(OutletRef='',RetGuid='005056B1F8FB1EDA8FF041CFF05E83A9',Flag='N',Dotyp='VTA0',SchGuid='',Srno=1,Doguid='',AttBy='TP')/AttachMedSet";// Constants.SaveVATDeclarationData;
                    String url = Constants.GAZTDeteleAttachment + "'" + "'" + ",RetGuid='undefined'" + ",Flag='" + "N" + "'" + ",Dotyp='" + Dotyp + "'" + ",SchGuid='" + "'" + ",Srno=" + "1" + ",Doguid='" + RetGuid + "'" + ",AttBy='" + AttBy + "'" + ")/$value?saml2=enabled"; //",RetGuid='005056B1F8FB1EDA8FF041CFF05E83A9',Flag='N',Dotyp='VTA0',SchGuid='',Srno=1,Doguid='',AttBy='TP')/AttachMedSet";// Constants.SaveVATDeclarationData;
                    url = url.Replace("attachmentServiceurl", aPiMethod);
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
        #endregion

        #region VATInstalationPlan

        //Request To VAT Installment Plan
        public static async Task<ReqVatInstalmentPlanResponse> GetRequestToVATInstalmentData()
        {
            ReqVatInstalmentPlanResponse _requestVATInstalmentPlan = new ReqVatInstalmentPlanResponse();

            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    string LangZAREN = GetLangZParameterAREN();
                    char LangZ = GetLangZParameter();
                    string lang = UtilityManager.GetLanguageParameter();
                    string taxType = "VT";
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.GetReqVATInstalmentdata + "TaxType='" + taxType + "',AudTin='" + "',Gpart='" + App.LoginDataRetrieved.TIN + "',Lang='" + lang + "',UserTin='" + "')?&$expand=ASSLISTSet,STATUSSet,REQTYPSet&$format=json";
                    var uri = new Uri(url);
                    HttpResponseMessage _requestVATInstalmentPlanresponse = await client.GetAsync(uri);

                    //var serilized = JsonConvert.SerializeObject(_requestVATInstalmentPlanresponse);
                    //HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    //HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    //var detailJson = res.Content.ReadAsStringAsync().Result;

                    var detailJson = _requestVATInstalmentPlanresponse.Content.ReadAsStringAsync().Result;
                    _requestVATInstalmentPlan = JsonConvert.DeserializeObject<ReqVatInstalmentPlanResponse>(detailJson);

                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
                }

                catch (Exception ex)
                {
                    return null;
                }

            }
            return _requestVATInstalmentPlan;
        }

        //Get FBGuid for VAT plan details
        public async static Task<VATInstalmentDetailsInputModel> GAZTGetFbGuidDetailsInputData(string fbguid, string fbnum, string gpart, string Status, string type)
        {
            VATInstalmentDetailsInputModel _InstalmentDetailsInputs = new VATInstalmentDetailsInputModel();

            if (CrossConnectivity.Current.IsConnected)
            {

                string NewToken = string.Empty;
                try
                {
                    string fbtyp = type;

                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);


                    //https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_ITAP_SRV/TPFILLSet
                    //    (Euser1 = '', Fbguid = '005056B1F8FB1EEAB8D6D063DAB19A0C', Fbnum = '86000000664', Fbtyp = 'VTIA', Gpart = '3102288776', Lang = 'EN', Persl = '', Status = 'E0045', Dispflag = '')
                    //    ?=&$format = json

                    String url = Constants.VATGetFormGUIDURL + "Euser1='" + "',Fbguid='" + fbguid + "',Fbnum='" + fbnum + "',Fbtyp='" + fbtyp + "'," +
                     "Gpart='" + gpart + "',Lang='" + lang + "',Persl='" + "',Status='" + Status + "',Dispflag='" + "')?=&$format=json";
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
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        String _VATRefillingRequestData = _vATRefillingGetDropdownResponse.Content.ReadAsStringAsync().Result;
                        _InstalmentDetailsInputs = JsonConvert.DeserializeObject<VATInstalmentDetailsInputModel>(_VATRefillingRequestData);



                        if (!string.IsNullOrEmpty(_VATRefillingRequestData) && _InstalmentDetailsInputs.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_VATRefillingRequestData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
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


        //Request To VAT Installment Plan Details
        public static async Task<RequestToVATInstallmentPlanDetails> GetRequestToVATInstalmentPlanDetails(string euser, string formGuid)
        {
            RequestToVATInstallmentPlanDetails _requestToVATInstallmentPlanDetails = new EGAZT.Models.VATInstalmentModels.RequestToVATInstallmentPlanDetails();

            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    string LangZAREN = GetLangZParameterAREN();
                    char LangZ = GetLangZParameter();
                    string lang = UtilityManager.GetLanguageParameter();
                    //Input Parameters
                    string Euser = euser;
                    string FormGuid = formGuid;

                    //End
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    //FormGuid = '005056B1F8FB1EDAB7B1293EB7E27171',Euser = '00000000001008317083',Gpartz = '',Langz = 'E',Officerz = '',PortalUsrz = '',TxnTpz = '')
                    //?$expand = VTIASet,VTISSet,NOTESSet,ATTACHMENTSet,VTADSet &$format = json

                    String url = Constants.GetRequestToVATInstalmentPlanById
                        + "FormGuid='" + FormGuid + "',Euser='" + Euser + "',Gpartz=''," +
                        "Langz='" + lang + "',Officerz='" + "',PortalUsrz='" + "',TxnTpz='" + "')?&$expand=VTIASet,VTISSet,NOTESSet,ATTACHMENTSet,VTADSet&$format=json";
                    var uri = new Uri(url);
                    HttpResponseMessage _requestVATInstalmentPlanresponse = await client.GetAsync(uri);

                    //var serilized = JsonConvert.SerializeObject(_requestVATInstalmentPlanresponse);
                    //HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    //HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    //var detailJson = res.Content.ReadAsStringAsync().Result;

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
                            String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                            errorMessage = WithReplacedString;
                            //ErrorMessageForVAT
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
                    return null;
                }

            }
            return _requestToVATInstallmentPlanDetails;
        }


        //Display Installment Agreement Schedule Plan
        public static async Task<DisplayInstallmentAgreementSchedulePlan> GetDisplayInstallmentAgreementSchedulePlan(string formGuid)
        {
            DisplayInstallmentAgreementSchedulePlan _displayInstallmentAgreementSchedulePlan = new DisplayInstallmentAgreementSchedulePlan();

            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    string LangZAREN = GetLangZParameterAREN();
                    char LangZ = GetLangZParameter();
                    string lang = UtilityManager.GetLanguageParameter();
                    //Input Parameters
                    string Euser = "";
                    string FormGuid = formGuid;

                    //End
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    //  FormGuid = '005056B1F8FB1EDAB7B1669470197223',Euser = '00000010000008316968',Gpart = '',Langz = 'E',Opbel = '')
                    //?$expand = VTIA_IADTSet,VTIA_IAHDSet &$format = json

                    String url = Constants.GetDisplayInstallmentAgreementSchedule
                        + "FormGuid='" + FormGuid + "',Euser='" + Euser + "',Gpart=''," +
                        "Langz='" + lang + "',Opbel='" + "')?&$expand=VTIA_IADTSet,VTIA_IAHDSet&$format=json";
                    var uri = new Uri(url);
                    HttpResponseMessage _requestVATInstalmentPlanresponse = await client.GetAsync(uri);

                    //var serilized = JsonConvert.SerializeObject(_requestVATInstalmentPlanresponse);
                    //HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    //HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    //var detailJson = res.Content.ReadAsStringAsync().Result;

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
                            String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                            errorMessage = WithReplacedString;
                            //ErrorMessageForVAT
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
                    return null;
                }

            }
            return _displayInstallmentAgreementSchedulePlan;
        }


        //Instalment Schedule details
        public static async Task<VATInstalmentScheduleDetailsModel> GetDisplayInstallmentScheduleDetails(string opbel, string fromGuid, string eUser)
        {
            VATInstalmentScheduleDetailsModel _vATInstalmentScheduleDetailsModel = new VATInstalmentScheduleDetailsModel();

            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    string LangZAREN = GetLangZParameterAREN();
                    char LangZ = GetLangZParameter();
                    string lang = UtilityManager.GetLanguageParameter();
                    //Input Parameters
                    string Euser = eUser;
                    string FormGuid = fromGuid;
                    string Opbel = opbel;

                    //End
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    //(FormGuid = '005056B1F8FB1EDAB7B1669470197223', Euser = '00000010000008316968', Gpart = '', Langz = 'E', Opbel = '8000003828') 
                    //  ?$expand = VTIA_IADTSet,VTIA_IAHDSet &$format = json

                    String url = Constants.GetInstallmentSchedule
                        + "FormGuid='" + FormGuid + "',Euser='" + Euser + "',Gpart=''," +
                        "Langz='E',Opbel='" + Opbel + "')?$expand=VTIA_IADTSet,VTIA_IAHDSet&$format=json";
                    var uri = new Uri(url);
                    HttpResponseMessage _requestVATInstalmentPlanresponse = await client.GetAsync(uri);

                    //var serilized = JsonConvert.SerializeObject(_requestVATInstalmentPlanresponse);
                    //HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    //HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    //var detailJson = res.Content.ReadAsStringAsync().Result;


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
                            String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                            errorMessage = WithReplacedString;
                            //ErrorMessageForVAT
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
                    return null;
                }

            }
            return _vATInstalmentScheduleDetailsModel;
        }


        public async static Task<VatInstalmentPlanResponse> GAZTGetVATInstalmentData()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                VatInstalmentPlanResponse vATInstalmentDetails = new VatInstalmentPlanResponse();
                string NewToken = string.Empty;
                try
                {
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.GetVATInstalmentdata + "FormGuid='" + "',Euser='" + "',Gpartz='" + App.LoginDataRetrieved.TIN + "',Langz='" + lang + "',Officerz='" + "',PortalUsrz='" + "',TxnTpz='" + "')?$expand=VTIASet,VTISSet,NOTESSet,ATTACHMENTSet,VTADSet&$format=json";
                    //client.DefaultRequestHeaders.Add("Token", "123");
                    //client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
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
                        vATInstalmentDetails = JsonConvert.DeserializeObject<VatInstalmentPlanResponse>(vatInstalmentData);
                        if (!string.IsNullOrEmpty(vatInstalmentData) && vATInstalmentDetails.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(vatInstalmentData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return vATInstalmentDetails;
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

        public static async Task<VatInstalmentPlanResponse> SaveVATInstalmentData(VatInstalmentPlanRequest _vATInstalment)
        {

            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {

                    VatInstalmentPlanResponse _vatResponseObject = new VatInstalmentPlanResponse();
                    string LangZ = GetLangZParameterAREN();
                    String url = "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VTIA_SRV/VTIA_HEADERSet";
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    if(_vATInstalment.d.Operationz == "01") {

                        var serilized = JsonConvert.SerializeObject(_vATInstalment.d);
                        client.DefaultRequestHeaders.Add("Token", "123");
                        client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                        client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                        client.DefaultRequestHeaders.Add("Accept", "application/json");

                        HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                        HttpResponseMessage res = client.PostAsync(uri, contentPost).Result;
                        var _zakatReturnDetailsDesponsestr = res.Content.ReadAsStringAsync().Result;
                        _vatResponseObject = JsonConvert.DeserializeObject<VatInstalmentPlanResponse>(_zakatReturnDetailsDesponsestr);
                        if (_vatResponseObject == null || _vatResponseObject.d == null)
                        {
                            ErrorMessage = string.Empty;
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatReturnDetailsDesponsestr);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                ErrorMessage = errorMesg.error.innererror.errordetails[0].message;
                            }
                        }
                        return _vatResponseObject;
                    }
                    else {
                        var serilized = JsonConvert.SerializeObject(_vATInstalment);
                        client.DefaultRequestHeaders.Add("Token", "123");
                        client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                        client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                        client.DefaultRequestHeaders.Add("Accept", "application/json");

                        HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                        HttpResponseMessage res = client.PostAsync(uri, contentPost).Result;
                        var _zakatReturnDetailsDesponsestr = res.Content.ReadAsStringAsync().Result;
                        _vatResponseObject = JsonConvert.DeserializeObject<VatInstalmentPlanResponse>(_zakatReturnDetailsDesponsestr);
                        if (_vatResponseObject == null || _vatResponseObject.d == null)
                        {
                            ErrorMessage = string.Empty;
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatReturnDetailsDesponsestr);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                ErrorMessage = errorMesg.error.innererror.errordetails[0].message;
                            }
                        }
                        return _vatResponseObject;
                    }


                   
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
        #endregion

        #region Zakat Instalment Plan

        public async static Task<ZakatInstalmentInvListModel> GAZTGetZakatInstalmentInvData(string fbNum)
        {
            ZakatInstalmentInvListModel _zakatInstalmentInvListModel = new ZakatInstalmentInvListModel();



            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);



                    //https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_IPRF_M_SRV/invDtlsSet?$filter=Tin eq '3102434622'and Fbnum eq '85000000701' and Langz eq 'EN' and InstReqFor  eq '01'&$format=json




                    String url = Constants.ZakatInstalmentInvoiceURL + "Tin eq'" + App.LoginDataRetrieved.TIN + "' " +
                        "and Fbnum eq '" + fbNum + "' and Langz eq '" + lang + "' and InstReqFor eq '01'&$format=json";



                    var uri = new Uri(url);
                    HttpResponseMessage _zakatInstalmentInvListResponse = await client.GetAsync(uri);
                    if (_zakatInstalmentInvListResponse != null)
                    {
                        if (_zakatInstalmentInvListResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _zakatInstalmentInvListResponse.Headers;
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
                        String __zakatInstalmentInvListData = _zakatInstalmentInvListResponse.Content.ReadAsStringAsync().Result;
                        _zakatInstalmentInvListModel = JsonConvert.DeserializeObject<ZakatInstalmentInvListModel>(__zakatInstalmentInvListData);
                        if (!string.IsNullOrEmpty(__zakatInstalmentInvListData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__zakatInstalmentInvListData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _zakatInstalmentInvListModel;
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


        public async static Task<ZakatInstalmentValidateNewRequestModel> GAZTGetZakatInstalmentValidateNewReq()
        {
            ZakatInstalmentValidateNewRequestModel _zakatInstalmentValidateNewRequestModel = new ZakatInstalmentValidateNewRequestModel();



            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {



                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);



                    //  https://sapgatewayqa.gazt.gov.sa/sap/opu/odata//SAP/ZDP_IPRF_WI_SRV/HdrSet(CallServ='IPRA',HostName='',Zuser='3102227536',Bpnum='',
                    //     Auditor = 'null',Lang = 'E',Euser1 = 'null',Euser2 = 'null',Euser3 = 'null',Euser4 = 'null',Euser5 = 'null',
                    //Fbguid = '005056B1F8FB1EEABCCF9C96A93FD2E0',UserTin = '',Fbnum = '',UserTyp = 'TP')?$expand = WorklistSet,AuthServSet,EvtNotif12Set,EvtNotif1Set,RevokeListSet &$format = json




                    String url = Constants.ZakatInstalmentValidateNewRequestURL + "CallServ='IPRA',HostName='" + "',Zuser='" + App.LoginDataRetrieved.TIN + "',Bpnum='" + App.LoginDataRetrieved.TIN + "'," +
                     "Auditor='null',Lang='" + lang + "',Euser1='" + "',Euser2='null',Euser3='null',Euser4='null',Euser5='null'" +
                     ",Fbguid='" + "',UserTin='" + "',Fbnum='" + "',UserTyp='TP')?$expand=WorklistSet,AuthServSet,EvtNotif12Set,EvtNotif1Set,RevokeListSet&$format=json";
                    var uri = new Uri(url);
                    HttpResponseMessage zakatInstalmentValidateNewRequestResponse = await client.GetAsync(uri);
                    if (zakatInstalmentValidateNewRequestResponse != null)
                    {
                        if (zakatInstalmentValidateNewRequestResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = zakatInstalmentValidateNewRequestResponse.Headers;
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
                        String _zakatInstalmentValidateNewRequestData = zakatInstalmentValidateNewRequestResponse.Content.ReadAsStringAsync().Result;
                        _zakatInstalmentValidateNewRequestModel = JsonConvert.DeserializeObject<ZakatInstalmentValidateNewRequestModel>(_zakatInstalmentValidateNewRequestData);
                        if (!string.IsNullOrEmpty(_zakatInstalmentValidateNewRequestData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatInstalmentValidateNewRequestData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _zakatInstalmentValidateNewRequestModel;
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
        public async static Task<ZakatInstalmentPlanResponse> GetZakatInstalmentPostData()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                ZakatInstalmentPlanResponse zakatInstalmentDetails = new ZakatInstalmentPlanResponse();
                string NewToken = string.Empty;
                try
                {
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    string formMode = "N";
                    // https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_IPRF_M_SRV/iprfhdrSet(Tin='3102206579',Euser='',Fbguid='',Fbnum='',FormMode='N',Langz='EN')?&$format=json
                    String url = Constants.GetZAKATInstalmentdata + "Tin='" + App.LoginDataRetrieved.TIN + "',Euser='" + "',Fbguid='" + "',Fbnum='" + "',FormMode='" + formMode + "',Langz='" + lang + "')?&$format=json";

                    //client.DefaultRequestHeaders.Add("Token", "123");
                    //client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTZakatInstalmentDataResponse = await client.GetAsync(uri);
                    if (GAZTZakatInstalmentDataResponse != null)
                    {
                        if (GAZTZakatInstalmentDataResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTZakatInstalmentDataResponse.Headers;
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
                        String zakatInstalmentData = GAZTZakatInstalmentDataResponse.Content.ReadAsStringAsync().Result;
                        zakatInstalmentDetails = JsonConvert.DeserializeObject<ZakatInstalmentPlanResponse>(zakatInstalmentData);
                        if (!string.IsNullOrEmpty(zakatInstalmentData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(zakatInstalmentData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return zakatInstalmentDetails;
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

       
        public async static Task<ZakatInvoiceList> GetZakatInvoicesList(bool IsZakat)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                ZakatInvoiceList invoicesList = new ZakatInvoiceList();
                string NewToken = string.Empty;
                try
                {
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);


                    String url = "";
                    if (IsZakat)
                    {
                        url = Constants.GetZAKATInvoices + "Tin eq '" + App.LoginDataRetrieved.TIN + "'and " + "Fbnum eq '" + "' and " + "Langz eq '" + lang + "' and " + "InstReqFor eq '" + "01" + "'&$format=json";
                    }
                    else
                    {
                        url = Constants.GetZAKATInvoices + "Tin eq '" + App.LoginDataRetrieved.TIN + "'and " + "Fbnum eq '" + "' and " + "Langz eq '" + lang + "' and " + "InstReqFor eq '" + "02" + "'&$format=json";
                    }
                    url = System.Web.HttpUtility.UrlPathEncode(url);

                    //client.DefaultRequestHeaders.Add("Token", "123");
                    //client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTZakatInvoiceDataResponse = await client.GetAsync(uri);
                    if (GAZTZakatInvoiceDataResponse != null)
                    {
                        if (GAZTZakatInvoiceDataResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTZakatInvoiceDataResponse.Headers;
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
                        String zakatInvoicesData = GAZTZakatInvoiceDataResponse.Content.ReadAsStringAsync().Result;
                        invoicesList = JsonConvert.DeserializeObject<ZakatInvoiceList>(zakatInvoicesData);
                        if (!string.IsNullOrEmpty(zakatInvoicesData) && invoicesList.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(zakatInvoicesData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return invoicesList;
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




        public async static Task<ZakatInstalmentPlanResponse> SaveZakatInstalmentData(ZakatInstalmentPlanRequest _zakatInstalmentDetails)
        {


            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {

                    ZakatInstalmentPlanResponse _zakatResponseObject = new ZakatInstalmentPlanResponse();
                    string LangZ = GetLangZParameterAREN();
                    String url = Constants.GetZAKATPostdata;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);

                        var serilized = JsonConvert.SerializeObject(_zakatInstalmentDetails);
                        client.DefaultRequestHeaders.Add("Token", "123");
                        client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                        client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                        client.DefaultRequestHeaders.Add("Accept", "application/json");

                        HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                        HttpResponseMessage res = client.PostAsync(uri, contentPost).Result;
                        var _zakatReturnDetailsDesponsestr = res.Content.ReadAsStringAsync().Result;
                        _zakatResponseObject = JsonConvert.DeserializeObject<ZakatInstalmentPlanResponse>(_zakatReturnDetailsDesponsestr);
                    if (!string.IsNullOrEmpty(_zakatReturnDetailsDesponsestr) && _zakatResponseObject.d == null)
                    {
                        ErrorMessage = string.Empty;
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatReturnDetailsDesponsestr);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                ErrorMessage = errorMesg.error.innererror.errordetails[0].message;
                            throw new GAZTVATRegistrationInProcessException(ErrorMessage);

                        }

                    }
                        return _zakatResponseObject;
                  //  }



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


        public async static Task<ZakatInstalmentPlanRevokeResponse> GetZakatInstalmentRevokePostData(string FBnum)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                ZakatInstalmentPlanRevokeResponse zakatInstalmentDetails = new ZakatInstalmentPlanRevokeResponse();
                string NewToken = string.Empty;
                try
                {
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    string formMode = "S";
                    // https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_IPRF_M_SRV/iprfhdrSet(Tin='3102206579',Euser='',Fbguid='',Fbnum='',FormMode='N',Langz='EN')?&$format=json
                    String url = Constants.GetZAKATInstalmentdata + "Tin='" + App.LoginDataRetrieved.TIN + "',Euser='" + "',Fbguid='"  + "',Fbnum='" + FBnum + "',FormMode='" + formMode + "',Langz='" + lang + "')?&$format=json";

                    //client.DefaultRequestHeaders.Add("Token", "123");
                    //client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTZakatInstalmentDataResponse = await client.GetAsync(uri);
                    if (GAZTZakatInstalmentDataResponse != null)
                    {
                        if (GAZTZakatInstalmentDataResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTZakatInstalmentDataResponse.Headers;
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
                        String zakatInstalmentData = GAZTZakatInstalmentDataResponse.Content.ReadAsStringAsync().Result;
                        zakatInstalmentDetails = JsonConvert.DeserializeObject<ZakatInstalmentPlanRevokeResponse>(zakatInstalmentData);
                        if (!string.IsNullOrEmpty(zakatInstalmentData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(zakatInstalmentData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return zakatInstalmentDetails;
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

        public async static Task<ZakatInstalmentPlanRevokeResponse> SaveZakatInstalmentRevokeData(ZakatInstalmentPlanRevokeRequest _zakatInstalmentDetails)
        {


            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {

                    ZakatInstalmentPlanRevokeResponse _zakatResponseObject = new ZakatInstalmentPlanRevokeResponse();
                    string LangZ = GetLangZParameterAREN();
                    String url = Constants.GetZAKATPostdata;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    //if (_zakatInstalmentDetails.d.Savez == "X")
                    //{

                    //    var serilized = JsonConvert.SerializeObject(_zakatInstalmentDetails.d);
                    //    client.DefaultRequestHeaders.Add("Token", "123");
                    //    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    //    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    //    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    //    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    //    HttpResponseMessage res = client.PostAsync(uri, contentPost).Result;
                    //    var _zakatReturnDetailsDesponsestr = res.Content.ReadAsStringAsync().Result;
                    //    _zakatResponseObject = JsonConvert.DeserializeObject<ZakatInstalmentPlanResponse>(_zakatReturnDetailsDesponsestr);
                    //    if (_zakatResponseObject == null || _zakatResponseObject.d == null)
                    //    {
                    //        ErrorMessage = string.Empty;
                    //        ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatReturnDetailsDesponsestr);
                    //        if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                    //        {
                    //            ErrorMessage = errorMesg.error.innererror.errordetails[0].message;
                    //        }
                    //    }
                    //    return _zakatResponseObject;
                    //}
                    //else
                    //{
                    var serilized = JsonConvert.SerializeObject(_zakatInstalmentDetails);
                    client.DefaultRequestHeaders.Add("Token", "123");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    HttpResponseMessage res = client.PostAsync(uri, contentPost).Result;
                    var _zakatReturnDetailsDesponsestr = res.Content.ReadAsStringAsync().Result;
                    _zakatResponseObject = JsonConvert.DeserializeObject<ZakatInstalmentPlanRevokeResponse>(_zakatReturnDetailsDesponsestr);
                    if (!string.IsNullOrEmpty(_zakatReturnDetailsDesponsestr) && _zakatResponseObject.d == null)
                    {
                        ErrorMessage = string.Empty;
                        ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatReturnDetailsDesponsestr);
                        if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                        {
                            ErrorMessage = errorMesg.error.innererror.errordetails[0].message;
                            throw new GAZTVATRegistrationInProcessException(ErrorMessage);

                        }

                    }
                    return _zakatResponseObject;
                    //  }



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

        #region Establishment Registration API Calls
        public static async Task<List<BranchesDropDownModel>> ESTBranchesDropDown()
        {
            List<BranchesDropDownModel> dropDownModels = new List<BranchesDropDownModel>();
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    char lang = GetLangZParameter();
                    if (false == CrossConnectivity.Current.IsConnected)
                    {
                        throw new GAZTInternetException();
                    }
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    //client.DefaultRequestHeaders.Add("Token", "123");
                    var uri = new Uri(string.Format("{0}?&$format=json&$filter=Spras eq '{1}'", Constants.ESTBranchesDropDown, lang));
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
                    char lang = GetLangZParameter();
                    if (false == CrossConnectivity.Current.IsConnected)
                    {
                        throw new GAZTInternetException();
                    }
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    //client.DefaultRequestHeaders.Add("Token", "123");
                    var uri = new Uri(string.Format("{0}(Euser='',Fbguid='',Gpartx='{1}',Langx='{2}',Operationx='',PortalUsrx='{3}',Srcidentifyx='{4}',StepNumberx='{5}',Fbnumx='{6}',Fbstax='',Fbustx='')?&$expand=Nreg_ActivitySet,Nreg_AddressSet,Nreg_ContactSet,Nreg_CpersonSet,Nreg_IdSet,Nreg_OutletSet,Nreg_ShareholderSet,Nreg_FormEdit,Nreg_BtnSet,off_notesSet,AttDetSet,Nreg_MSGSet&$format=json",
                        Constants.ESTTaxPayerDetails, TIN, lang, emailID, srcidentify, step, Fbnum));
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
                            taxPayer = JsonConvert.DeserializeObject<TaxPayerDetails>(ESTBranchesDropDownResponseJSON);
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
        public static async Task<TaxPayerDetails> ESTTaxPayerDetailPostService(TaxPayerDetails taxPayer) //Rentatt =X , Passatt=X
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    char lang = GetLangZParameter();
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
                            }else if (errorMesg != null && errorMesg.error != null && errorMesg.error.message != null && !string.IsNullOrEmpty(errorMesg.error.message.value))
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
                    char lang = GetLangZParameter();
                    if (false == CrossConnectivity.Current.IsConnected)
                    {
                        throw new GAZTInternetException();
                    }
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    //client.DefaultRequestHeaders.Add("Token", "123");
                    var uri = new Uri(string.Format("{0}?&$format=json&$filter=ANationality eq '{1}' and Spras eq '{2}'",
                        Constants.ESTTaxPayerNationality, nationality, lang));
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
            char lang = GetLangZParameter();
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
                    client.DefaultRequestHeaders.Add("slug", fileName);
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
                    client.DefaultRequestHeaders.Add("slug", fileName);
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "multipart/form-data");

                    HttpResponseMessage response = client.DeleteAsync(uri).Result;
                    var responsestr = response.Content.ReadAsStringAsync().Result;
                    //responsestr = JObject.Parse(responsestr)["d"].ToString();
                    //Attachment _attachment = JsonConvert.DeserializeObject<Attachment>(responsestr);
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
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    //client.DefaultRequestHeaders.Add("Token", "123");
                    var uri = new Uri(string.Format("{0}(Fbnum='{1}',Gpart='')?&$format=json",
                        Constants.ESTOutletNumber, Fbnum));
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
                    char lang = GetLangZParameter();
                    if (false == CrossConnectivity.Current.IsConnected)
                    {
                        throw new GAZTInternetException();
                    }
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    //client.DefaultRequestHeaders.Add("Token", "123");
                    var uri = new Uri(string.Format("{0}(Spras='{1}',Land1='',Bland='',Cityc='')?&$expand=country_dropdownSet,State_dropdownSet,city_dropdownSet&$format=json",
                        Constants.ESTOutletCityStateCountryDropDown, lang));
                    
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
                    char lang = GetLangZParameter();
                    indSector = (string.IsNullOrEmpty(indSector) || string.IsNullOrWhiteSpace(indSector)) ? string.Empty : indSector;
                    if (false == CrossConnectivity.Current.IsConnected)
                    {
                        throw new GAZTInternetException();
                    }
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    //client.DefaultRequestHeaders.Add("Token", "123");
                    var uri = new Uri(string.Format("{0}(Spras='{1}',IndSector='{2}')?&$expand=act_groupSet,act_subgroupSet,activitySet&$format=json",
                        Constants.ESTActiivtyGroupSubGroupList, lang, indSector));
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
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    //client.DefaultRequestHeaders.Add("Token", "123");
                    var uri = new Uri(string.Format("{0}(Crnum='{1}')?$format=json",
                        Constants.ESTValidateCRNum, cr));
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

                    //client.DefaultRequestHeaders.Add("Token", "123");
                    var uri = new Uri(string.Format("{0}/?&$format=json&$filter=PortalUsrx eq '{1}' and Gpartx eq '{2}' and Fbnumx eq '{3}' and Actno eq ''",
                        Constants.ESTOutletList, email, gpart, fbnum));
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
                            outlets = JsonConvert.DeserializeObject<List<OutletItem>>(ESTBranchesDropDownResponseJSON);
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
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    //client.DefaultRequestHeaders.Add("Token", "123");
                    var uri = new Uri(string.Format("{0}?$format=json&$filter=IdType eq '{1}' and IdNumber eq '{2}' and Tin eq '{3}' and TpType eq 'I'",
                        Constants.ESTOutletAddressFetch, idType, IdNumber, tin));
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
                    //client.DefaultRequestHeaders.Add("slug", fileName);
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "multipart/form-data");

                    HttpResponseMessage response = client.DeleteAsync(uri).Result;
                    var responsestr = response.Content.ReadAsStringAsync().Result;
                    //responsestr = JObject.Parse(responsestr)["d"].ToString();
                    //Attachment _attachment = JsonConvert.DeserializeObject<Attachment>(responsestr);
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
                    var financeData = JsonConvert.SerializeObject(financialDetailRequest, new JsonSerializerSettings {
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


        #region Contract Release



        public async static Task<ContractReLeaseApplicationFormModel> GetContractReleaseList()
        {

            if (CrossConnectivity.Current.IsConnected)
            {
                ContractReLeaseApplicationFormModel _ContractReLeaseApplicationFormDetails = new ContractReLeaseApplicationFormModel();
                string NewToken = string.Empty;
                try
                {

                    string euser2 = "null";
                    string euser3 = "null";
                    string euser4 = "null";
                    string euser5 = "null";
                   

                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    //(CallServ = 'DCON', HostName = '', Zuser = 'MALRUZAYQI@GAZT.GOV.SA', Bpnum = '', Auditor = '', Lang = 'E',
                    // Euser1 = '00001000000008317878', Euser2 = 'null', Euser3 = 'null', Euser4 = 'null', Euser5 = 'null',
                    // Fbguid = '005056B1F8FB1EEAB88BF2E3F6A794B0') ?$expand=ListSet,AuthServSet Bpnum

                    String url = Constants.ContractReleaseApplicationFormUrl + "CallServ='DCON',HostName='" + "',Bpnum='" + App.LoginDataRetrieved.TIN + "',Zuser='" + "'," +
                       "Auditor='" + "'," +
                     "Lang='" + lang + "',Euser1='" + "''" + "',Euser2='" + euser2 + "',Euser3='" + euser3 + "'," +
                     "Euser4='" + euser4 + "',Euser5='" + euser5 + "',Fbguid='"  + "')?$expand=ListSet,AuthServSet&$format=json";
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTzakatInstalmentDataResponse = await client.GetAsync(uri);



                    if (GAZTzakatInstalmentDataResponse != null)
                    {
                        if (GAZTzakatInstalmentDataResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTzakatInstalmentDataResponse.Headers;
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
                        String _crApplicationFormData = GAZTzakatInstalmentDataResponse.Content.ReadAsStringAsync().Result;
                        _ContractReLeaseApplicationFormDetails = JsonConvert.DeserializeObject<ContractReLeaseApplicationFormModel>(_crApplicationFormData);

                        if (!string.IsNullOrEmpty(_crApplicationFormData) && _ContractReLeaseApplicationFormDetails.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_crApplicationFormData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _ContractReLeaseApplicationFormDetails;
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


        public async static Task<ContractReleaseFormResponse> GAZTGetContractReleaseRequestData()
        {
            ContractReleaseFormResponse _contractReleaseRequestModel = new ContractReleaseFormResponse();

            if (CrossConnectivity.Current.IsConnected)
            {

                string NewToken = string.Empty;
                try
                {
                    //taxpayerz = "3102224202";
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    //(Auditorz = '', Taxpayerz = '3102224202', RegIdz = '', Submitz = '', Savez = '', Fbnumz = '', Langz = 'E', PeriodKeyz = '', UserTin = '') 
                    // ?$expand = znotesSet,AttDetSet
                    String url = Constants.ContractReleaseRequestUrl + "Auditorz='" + "',Taxpayerz='" + App.LoginDataRetrieved.TIN + "',RegIdz='" + "',Submitz='" + "'," +
                      "Savez='" + "',Fbnumz='" + "',Langz='" + lang + "',PeriodKeyz='" + "'," +
                      "UserTin='" + "')?$expand=znotesSet,AttDetSet&$format=json";
                    var uri = new Uri(url);
                    HttpResponseMessage _contractReleaseRequestResponse = await client.GetAsync(uri);



                    if (_contractReleaseRequestResponse != null)
                    {
                        if (_contractReleaseRequestResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _contractReleaseRequestResponse.Headers;
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
                        String _contractReleaseRequestData = _contractReleaseRequestResponse.Content.ReadAsStringAsync().Result;
                        _contractReleaseRequestModel = JsonConvert.DeserializeObject<ContractReleaseFormResponse>(_contractReleaseRequestData);

                        if (!string.IsNullOrEmpty(_contractReleaseRequestData) && _contractReleaseRequestModel.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_contractReleaseRequestData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _contractReleaseRequestModel;
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
        //Request Model and Submit models are same here based on provided api list
        public async static Task<ContractReleaseFormResponse> GAZTSubmitContractReleaseRequestData(ContractReleaseFormRequest contractReleaseFormData)
        {
            ContractReleaseFormResponse _submitRequestData = new ContractReleaseFormResponse();
            try
            {

                //_submitRequestData = await GAZTGetContractReleaseRequestData();
                //_submitRequestData.d.AContDt = aContDt;
                //_submitRequestData.d.AContEndDt = aContEndDt;
                //_submitRequestData.d.AReceiveDt = aReceiveDt;
                //  _submitRequestData.d.CurrDatumz = null;

                string LangZ = GetLangZParameterAREN();
                String url = Constants.ContractReleaseSubmitUrl;
                var uri = new Uri(url);
                HttpClient client = new HttpClient(App.httpClientHandler);
                var serilized = JsonConvert.SerializeObject(contractReleaseFormData.d);
                client.DefaultRequestHeaders.Add("Token", App.Token);
                client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                HttpResponseMessage res = client.PostAsync(uri, contentPost).Result;
                var _contractReleasesubmitResponse = res.Content.ReadAsStringAsync().Result;
                _submitRequestData = JsonConvert.DeserializeObject<ContractReleaseFormResponse>(_contractReleasesubmitResponse);

                if (!string.IsNullOrEmpty(_contractReleasesubmitResponse) && _submitRequestData.d == null)
                {
                    ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_contractReleasesubmitResponse);
                    if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                    {
                        string errorMessage = string.Empty;
                        errorMessage = errorMesg.error.innererror.errordetails[0].message;
                        errorMessage += errorMesg.error.innererror.errordetails[1].message;
                        String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                        errorMessage = WithReplacedString;
                        //ErrorMessageForVAT

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

                App.IsSessionExpired = true;
                return null;
            }
            return _submitRequestData;


        }

        public async static Task<ContractReleaseSummaryModel> GAZTGetContractReleaseSummaryData(string taxpayerz, string fbnumz)
        {
            ContractReleaseSummaryModel _contractReleaseSummaryModel = new ContractReleaseSummaryModel();
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    
                    //fbnumz = "035001347905";
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    // https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/Z_TP_NOTES_TP11_SRV/znotes_tp11Set(Auditorz='',Taxpayerz='3102224202',RegIdz='',Submitz='',Savez='',
                    //Fbnumz='035001347905',Langz='E',PeriodKeyz='',UserTin='')?$expand=znotesSet,AttDetSet&$format=json
                    String url = Constants.ContractReleaseSummaryData + "Auditorz='" + "',Taxpayerz='" + App.LoginDataRetrieved.TIN + "',RegIdz='" + "',Submitz='" + "'," +
                      "Savez='" + "',Fbnumz='" + fbnumz + "',Langz='" + lang + "',PeriodKeyz='" + "'," +
                      "UserTin='" + "')?$expand=znotesSet,AttDetSet&$format=json";
                    var uri = new Uri(url);
                    HttpResponseMessage _contractReleasesummaryResponse = await client.GetAsync(uri);

                    if (_contractReleasesummaryResponse != null)
                    {
                        if (_contractReleasesummaryResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _contractReleasesummaryResponse.Headers;
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
                        String _contractReleaseRequestData = _contractReleasesummaryResponse.Content.ReadAsStringAsync().Result;
                        _contractReleaseSummaryModel = JsonConvert.DeserializeObject<ContractReleaseSummaryModel>(_contractReleaseRequestData);
                        if (!string.IsNullOrEmpty(_contractReleaseRequestData) && _contractReleaseSummaryModel.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_contractReleaseRequestData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _contractReleaseSummaryModel;
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

        #region VAT Filling Change
        public async static Task<VATChangeFillingPeriodRequestModel> GAZTGetVATChangeFillingPeriodRequestData()
        {
            VATChangeFillingPeriodRequestModel _vATChangeFillingPeriodRequestModel = new VATChangeFillingPeriodRequestModel();

            if (CrossConnectivity.Current.IsConnected)
            {

                string NewToken = string.Empty;
                try
                {
                   

                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    ///// sap / opu / odata / SAP / ZDP_VAT_TPCV_SRV / UI_HDRSet
                    ////(Fbnumz = '', PortalUsrz = '', Langz = 'E', Operationz = '', Gpartz = '', Euser = '00000001000000088130', UserTypz = '', Fbguid = '005056B1365C1EDAB5B1CBE1861076A3')
                    ////? &$expand = EffDateSet,UI_BTNSet,NOTESSet,ATTACHSet,ATT_TYPSet,QuesListSet

                    String url = Constants.VATChangeFillingPeriodGetURL + "Fbnumz='" + "',PortalUsrz='" + "',Langz='" + lang + "',Operationz='" + "'," +
                   "Gpartz='" + App.LoginDataRetrieved.TIN + "',Euser='" + "',UserTypz='" + "',Fbguid='" + "')?$expand=EffDateSet,UI_BTNSet,NOTESSet,ATTACHSet,ATT_TYPSet,QuesListSet&$format=json";
                    var uri = new Uri(url);
                    HttpResponseMessage _vATChangeFillingPeriodGetResponse = await client.GetAsync(uri);



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
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        String _VATChangeFillingPeriodRequestData = _vATChangeFillingPeriodGetResponse.Content.ReadAsStringAsync().Result;
                        _vATChangeFillingPeriodRequestModel = JsonConvert.DeserializeObject<VATChangeFillingPeriodRequestModel>(_VATChangeFillingPeriodRequestData);

                        if (!string.IsNullOrEmpty(_VATChangeFillingPeriodRequestData) && _vATChangeFillingPeriodRequestModel.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_VATChangeFillingPeriodRequestData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATChangeFillingPeriodException(errorMessage);
                            }
                        }
                    }
                    return _vATChangeFillingPeriodRequestModel;
                }
                catch (GAZTVATChangeFillingPeriodException ex)
                {
                    throw new GAZTVATChangeFillingPeriodException(ex.Message);
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

        public async static Task<VATChangeFillingPeriodRequestModel> GAZTPostVATChangeFillingPeriodData(VATchangeFillingPeriodPostModel request)
        {
            VATChangeFillingPeriodRequestModel _vATChangeFillingPeriodRequestModel = new VATChangeFillingPeriodRequestModel();

            if (CrossConnectivity.Current.IsConnected)
            {

                string NewToken = string.Empty;
                try
                {
                    VATchangeFillingPeriodPostModel _VATchangeFillingPeriodPostModel = new VATchangeFillingPeriodPostModel();
                    _VATchangeFillingPeriodPostModel = request;
                    Char lang = WebServiceManager.GetLangZParameter();
                    //_vATChangeFillingPeriodRequestModel = await GAZTGetVATChangeFillingPeriodRequestData();

                    string LangZ = GetLangZParameterAREN();
                    String url = Constants.VATChangeFillingPeriodPostURL;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var serilized = JsonConvert.SerializeObject(_VATchangeFillingPeriodPostModel.d);
                    client.DefaultRequestHeaders.Add("Token", App.Token);
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    HttpResponseMessage res = client.PostAsync(uri, contentPost).Result;
                    var _contractReleasesubmitResponse = res.Content.ReadAsStringAsync().Result;
                    _vATChangeFillingPeriodRequestModel = JsonConvert.DeserializeObject<VATChangeFillingPeriodRequestModel>(_contractReleasesubmitResponse);
                    return _vATChangeFillingPeriodRequestModel;
                }
                catch (GAZTVATChangeFillingPeriodException ex)
                {
                    throw new GAZTVATChangeFillingPeriodException(ex.Message);
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

        public async static Task<VATRefillingDropdownModel> GAZTGetVATChangeFillingPeriodDropdownData(string gpart)
        {
            VATRefillingDropdownModel _vATRefillingDropdownModel = new VATRefillingDropdownModel();

            if (CrossConnectivity.Current.IsConnected)
            {

                string NewToken = string.Empty;
                try
                {

                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    //  https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/ZDP_TPCV_UH_SRV/UI_HDRSet
                    //(Fbtypz = '', UserTypz = '', TransactionTypez = '', Lang = 'E', Gpart = '3300067427', Status = '') ? &$expand = UI_BTNSet,ATT_TYPSet,EffDateSet

                    String url = Constants.VATChangeFillingPeriodGetDropdownURL + "Fbtypz='" + "',UserTypz='" + "',TransactionTypez='" + "',Lang='" + lang + "'," +
                     "Gpart='" + gpart + "',Status='" + "')?$expand=UI_BTNSet,ATT_TYPSet,EffDateSet&$format=json";
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
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        String _VATRefillingRequestData = _vATRefillingGetDropdownResponse.Content.ReadAsStringAsync().Result;
                        _vATRefillingDropdownModel = JsonConvert.DeserializeObject<VATRefillingDropdownModel>(_VATRefillingRequestData);

                        if (!string.IsNullOrEmpty(_VATRefillingRequestData) && _vATRefillingDropdownModel.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_VATRefillingRequestData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATChangeFillingPeriodException(errorMessage);
                            }
                        }
                    }
                    return _vATRefillingDropdownModel;
                }
                catch (GAZTVATChangeFillingPeriodException ex)
                {
                    throw new GAZTVATChangeFillingPeriodException(ex.Message);
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

        public async static Task<VATRefillingWorkItemsModel> GAZTGetVATChangeFillingPeriodWorkItemsData(string taxType, string gpart)
        {
            VATRefillingWorkItemsModel _vATRefillingWorkItemsModel = new VATRefillingWorkItemsModel();

            if (CrossConnectivity.Current.IsConnected)
            {

                string NewToken = string.Empty;
                try
                {

                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    //https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/ZDP_ITAP_SRV/HEADERSet
                    //(TaxType = 'VT', AudTin = '', Gpart = '3300067427', Lang = 'E', UserTin = '') ? &$expand = ASSLISTSet,STATUSSet,REQTYPSet

                    String url = Constants.VATChangeFillingPeriodWorkItemsURL + "TaxType='" + taxType + "',AudTin='" + "',Gpart='" + gpart + "',Lang='" + lang + "'," +
                     "UserTin='" + "')?$expand=ASSLISTSet,STATUSSet,REQTYPSet&$format=json";
                    var uri = new Uri(url);
                    HttpResponseMessage _vATChangeFillingGetDropdownResponse = await client.GetAsync(uri);



                    if (_vATChangeFillingGetDropdownResponse != null)
                    {
                        if (_vATChangeFillingGetDropdownResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _vATChangeFillingGetDropdownResponse.Headers;
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
                        String _VATRefillingRequestData = _vATChangeFillingGetDropdownResponse.Content.ReadAsStringAsync().Result;
                        _vATRefillingWorkItemsModel = JsonConvert.DeserializeObject<VATRefillingWorkItemsModel>(_VATRefillingRequestData);

                        if (!string.IsNullOrEmpty(_VATRefillingRequestData) && _vATRefillingWorkItemsModel.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_VATRefillingRequestData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATChangeFillingPeriodException(errorMessage);
                            }
                        }
                    }
                    return _vATRefillingWorkItemsModel;
                }
                catch (GAZTVATChangeFillingPeriodException ex)
                {
                    throw new GAZTVATChangeFillingPeriodException(ex.Message);
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

        public async static Task<VATSignUp> GAZTGetTInNumberData(string tin)
        {

            VATSignUp _validateIDResponse = new VATSignUp();
            if (CrossConnectivity.Current.IsConnected)
            {
                VATSignUp vATSignUp = new VATSignUp();
                string IsIDTypeValidList = string.Empty;
                string NewToken = string.Empty;
                String SignUpCityList = string.Empty;
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    char lang = GetLangZParameter();
                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);

                    String Url = Constants.GAZTVATSignUpValidateId + "(Tin='"+ tin + "',Idtype='" + string.Empty + "',Idnum='" + string.Empty + "',Country='',PassExpDt='',TaxpDob='" + string.Empty + "')?sap-language=" + lang + "&$format=json&saml2=enabled";
                    //                     (Tin='',Idtype='ZS0015',Idnum='1048089609',Country='',PassExpDt='',TaxpDob='19650224')?sap-language=A&$format=json&saml2=enabled

                    /*String Url = string.Empty;
                    Url = Constants.VATChangeFillingPeriodValidateIDnumberURL + "Tin='" + tin + "',Idtype='" + idType + "',Idnum='" + idnum + "',Country='" + country + "'" +
                          ",PassExpDt = '" + passExpdt + "', TaxpDob = '" + taxpDOB + "')";*/
                    var uri = new Uri(Url);
                    HttpResponseMessage VATSignUpIdValidateObject = await client.GetAsync(uri);
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
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        SignUpCityList = await VATSignUpIdValidateObject.Content.ReadAsStringAsync();
                        _validateIDResponse = JsonConvert.DeserializeObject<VATSignUp>(SignUpCityList);
                        if (!string.IsNullOrEmpty(SignUpCityList) && _validateIDResponse.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(SignUpCityList);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //_validateIDResponse.errorMessage = errorMessage;
                                //ErrorMessageForVAT
                                //throw new GAZTVATChangeFillingPeriodException(errorMessage);
                            }
                        }
                    }
                    return _validateIDResponse;// tINStatus;
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
                //catch (Exception ex)
                //{
                //    return null;
                //}
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        public async static Task<ValidateIDResponse> GAZTVATChangeFillingPeriodValidateIDnumber(string tin, string idType, string idnum, string country, string passExpdt, string taxpDOB)
        {

            ValidateIDResponse _validateIDResponse = new ValidateIDResponse();
            if (CrossConnectivity.Current.IsConnected)
            {
                VATSignUp vATSignUp = new VATSignUp();
                string IsIDTypeValidList = string.Empty;
                string NewToken = string.Empty;
                String SignUpCityList = string.Empty;
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    char lang = GetLangZParameter();
                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);

                    String Url = Constants.GAZTVATSignUpValidateId + "(Tin='',Idtype='" + idType + "',Idnum='" + idnum + "',Country='',PassExpDt='',TaxpDob='" + taxpDOB + "')?sap-language=" + lang + "&$format=json&saml2=enabled";
                    //                     (Tin='',Idtype='ZS0015',Idnum='1048089609',Country='',PassExpDt='',TaxpDob='19650224')?sap-language=A&$format=json&saml2=enabled

                    /*String Url = string.Empty;
                    Url = Constants.VATChangeFillingPeriodValidateIDnumberURL + "Tin='" + tin + "',Idtype='" + idType + "',Idnum='" + idnum + "',Country='" + country + "'" +
                          ",PassExpDt = '" + passExpdt + "', TaxpDob = '" + taxpDOB + "')";*/
                    var uri = new Uri(Url);
                    HttpResponseMessage VATSignUpIdValidateObject = await client.GetAsync(uri);
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
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")))
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
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                _validateIDResponse.errorMessage = errorMessage;
                                //ErrorMessageForVAT
                                //throw new GAZTVATChangeFillingPeriodException(errorMessage);
                            }
                        }
                    }
                    return _validateIDResponse;// tINStatus;
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
                //catch (Exception ex)
                //{
                //    return null;
                //}
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        public static string GAZTVATChangeFillingPeriodAckDownload(string fbnum)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {

                    //  / sap / opu / odata / SAP / Z_GET_ACK_LETTER_SRV / Ack_letterSet(Fbnum = '81000000501') /$value
                    String Url = string.Empty;
                    Url = Constants.VATChangeFillingPeriodAcknowledgementdownloadURL + "Fbnum='" + fbnum + "')/$value";

                    return Url;
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


        public async static Task<VATChangeFillingListModel> GAZTGetVATChangeFillingList(string gpart)
        {
            VATChangeFillingListModel _vATChangeFillingListModel = new VATChangeFillingListModel();

            if (CrossConnectivity.Current.IsConnected)
            {

                string NewToken = string.Empty;
                try
                {
                    string taxType = "VT";

                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    //  https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_ITAP_SRV/HEADERSet(TaxType='VT',AudTin='',
                    //Gpart = '3100088087',Lang = 'E',UserTin = '')?= &$expand = ASSLISTSet,STATUSSet,REQTYPSet &$format = json

                    String url = Constants.VATChangeFillingListURL + "TaxType='" + taxType + "',AudTin='" + "',Gpart='" + gpart + "',Lang='" + lang + "'," +
                      "UserTin='" + "')?&$expand=ASSLISTSet,STATUSSet,REQTYPSet&$format=json";
                    var uri = new Uri(url);
                    HttpResponseMessage _vatChangeFillingListResponse = await client.GetAsync(uri);



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
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        String _vatChangeFillingListData = _vatChangeFillingListResponse.Content.ReadAsStringAsync().Result;
                        _vATChangeFillingListModel = JsonConvert.DeserializeObject<VATChangeFillingListModel>(_vatChangeFillingListData);

                        if (!string.IsNullOrEmpty(_vatChangeFillingListData) && _vATChangeFillingListModel.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_vatChangeFillingListData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATChangeFillingPeriodException(errorMessage);
                            }
                        }
                    }
                    return _vATChangeFillingListModel;
                }
                catch (GAZTVATChangeFillingPeriodException ex)
                {
                    throw new GAZTVATChangeFillingPeriodException(ex.Message);
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


        public async static Task<VATChangeFillingSummaryModel> GAZTGetVATChangeFillingSummary(string fbnum, string status)
        {
            VATChangeFillingSummaryModel _vATChangeFillingSummaryModel = new VATChangeFillingSummaryModel();

            if (CrossConnectivity.Current.IsConnected)
            {

                try
                {
                    string NewToken = string.Empty;
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    //https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_TPCV_SRV/UI_HDRSet
                    //  (Fbnumz = '', PortalUsrz = '', Langz = 'E', Operationz = '', Gpartz = '', Euser = '00001000000008322132',
                    //UserTypz = '', Fbguid = '005056B1F8FB1EDAB9F82A3E64053352')
                    //   ?&$expand=EffDateSet,UI_BTNSet,NOTESSet,ATTACHSet,ATT_TYPSet,QuesListSet&$format=json

                    String url = Constants.VATChangeFillingSummaryURL + "Fbnumz='" + fbnum + "',PortalUsrz='" + "',Langz='" + lang + "'," +
                      "Operationz='" + "',Euser='" + "',Gpartz='" + App.LoginDataRetrieved.TIN + "',UserTypz='" + "',Fbguid='" + "')?&$expand=EffDateSet,UI_BTNSet,NOTESSet,ATTACHSet,ATT_TYPSet,QuesListSet&$format=json";
                    var uri = new Uri(url);
                     HttpResponseMessage _vatChangeFillingSumamryResponse = await client.GetAsync(uri);



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
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        String _vatChangeFillingSummaryData = _vatChangeFillingSumamryResponse.Content.ReadAsStringAsync().Result;
                        _vATChangeFillingSummaryModel = JsonConvert.DeserializeObject<VATChangeFillingSummaryModel>(_vatChangeFillingSummaryData);

                        if (!string.IsNullOrEmpty(_vatChangeFillingSummaryData) && _vATChangeFillingSummaryModel.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_vatChangeFillingSummaryData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
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

        public async static Task<VATChangeFillingSummaryModel> GAZTGetVATChangeFillingSummaryInputs(string fbnum,string status)
        {
            VATChangeFillingSummaryModel _vATChangeFillingSummaryInputsModel = new VATChangeFillingSummaryModel();

            if (CrossConnectivity.Current.IsConnected)
            {

                string NewToken = string.Empty;
                try
                {
                  
                    string fbtyp = "TPCV";
                    // eUser = "00001000000008322132";

                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    /// sap / opu / odata / SAP / ZDP_ITAP_SRV / TPFILLSet(Euser1 = '00000001000008323131',
                    //Fbguid = 'undefined', Fbnum = '81000003264', Fbtyp = 'TPCV', Gpart = '3100088087', Lang = 'EN', Persl = '', Status = 'E0013', Dispflag = '')

                    String url = Constants.VATChangeFillingSummaryInputsURL + "Euser1='',Fbguid='',Fbnum='" + fbnum + "'," +
                      "Fbtyp='" + fbtyp + "',Gpart='" + App.LoginDataRetrieved.TIN + "',Lang='" + lang + "',Persl='" + "',Status='" + status + "',Dispflag='" + "')?&$format=json";
                    var uri = new Uri(url);
                    HttpResponseMessage _vatChangeFillingSumamryInputResponse = await client.GetAsync(uri);



                    if (_vatChangeFillingSumamryInputResponse != null)
                    {
                        if (_vatChangeFillingSumamryInputResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _vatChangeFillingSumamryInputResponse.Headers;
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
                        String _vatChangeFillingSummaryInputData = _vatChangeFillingSumamryInputResponse.Content.ReadAsStringAsync().Result;
                        _vATChangeFillingSummaryInputsModel = JsonConvert.DeserializeObject<VATChangeFillingSummaryModel>(_vatChangeFillingSummaryInputData);

                        if (!string.IsNullOrEmpty(_vatChangeFillingSummaryInputData) && _vATChangeFillingSummaryInputsModel.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_vatChangeFillingSummaryInputData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _vATChangeFillingSummaryInputsModel;
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

        #region TIN Deregistration
        public async static Task<TinDeregistrationResponseModel> GaztTinDeregistrationNewRequestData(TinDeregistrationResponseModel tinDeregistrationResponseModel)
        {
            TinDeregistrationResponseModel _tinDeregistrationResponseModel = new TinDeregistrationResponseModel();

            if (CrossConnectivity.Current.IsConnected)
            {

                string NewToken = string.Empty;
                try
                {

                    string fbtyp = "TPCV";
                    // eUser = "00001000000008322132";

                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    /// sap / opu / odata / SAP / ZDP_ITAP_SRV / TPFILLSet(Euser1 = '00000001000008323131',
                    //Fbguid = 'undefined', Fbnum = '81000003264', Fbtyp = 'TPCV', Gpart = '3100088087', Lang = 'EN', Persl = '', Status = 'E0013', Dispflag = '')

                    String url = Constants.TinDeregistrationNewRequestUrl + "(Auditorz='',ADegister='1',Taxpayerz='"+ App.LoginDataRetrieved.TIN +"',FormGuid='',RegIdz='',PeriodKeyz='',Submitz='',Savez='',Fbnumz='',Langz='',OfficerUidz='',Approvez='"+ tinDeregistrationResponseModel.Approvez+"',Rejectz='"+tinDeregistrationResponseModel.Rejectz+"',CreateTxAssesz='')?&$expand=AttDetSet,Off_notesSet,OutletSet,PermitSet,returnSet,Permit_TableSet&$format=json";
                    var uri = new Uri(url);

                    HttpResponseMessage _tinDeregNewRequestResponse = await client.GetAsync(uri);

                    if (_tinDeregNewRequestResponse != null)
                    {
                        if (_tinDeregNewRequestResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _tinDeregNewRequestResponse.Headers;
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

                        String _responseData = _tinDeregNewRequestResponse.Content.ReadAsStringAsync().Result;
                        if (_tinDeregNewRequestResponse.StatusCode == HttpStatusCode.BadRequest)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_responseData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorCode = errorMesg.error.innererror.errordetails[0].code;

                                ErrorMessageForUnlockAccount = errorMesg.error.innererror.errordetails[0].message;

                                if(errorCode.Contains("206"))
                                {
                                    ErrorMessageForUnlockAccount = "206";
                                }
                                else if (errorCode.Contains("112"))
                                {
                                    ErrorMessageForUnlockAccount = "112";
                                }

                                String WithReplacedString = ErrorMessageForUnlockAccount.Replace("An exception was raised", string.Empty);
                                ErrorMessageForUnlockAccount = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTErrorException(ErrorMessageForUnlockAccount);
                            }
                        }
                        else if (!string.IsNullOrEmpty(_responseData))
                        {
                            _responseData = JObject.Parse(_responseData)["d"].ToString();
                            _tinDeregistrationResponseModel = JsonConvert.DeserializeObject<TinDeregistrationResponseModel>(_responseData);
                            if (_tinDeregistrationResponseModel == null)
                            {
                                throw new GAZTErrorException(AppResources.ZZSomethingwentwrong);
                            }
                        }
                        else
                        {
                            throw new GAZTErrorException(AppResources.ZZSomethingwentwrong);
                        }
                    }

                    return _tinDeregistrationResponseModel;
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

        private static String ConvertDateFormat(DateTime newDate)
        {
            string ConvertedDate = string.Empty;
            //DateTime newDate = Convert.ToDateTime(date);
            //DateTime currentDate = DateTime.Now.ToLocalTime();
            long ticks = newDate.Ticks - new DateTime(1970, 1, 1).Ticks;

            //_estimateZakatAttachment.UploadedDate = currentDate.ToString();
            TimeSpan span = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
            string unixTime = span.TotalSeconds.ToString("N0");
            unixTime = unixTime.Replace(",", "");
            ConvertedDate = "" + "/Date(" + unixTime + ")/";

            long unixTimestamp = ((long)(newDate.Subtract(new DateTime(1970, 1, 1))).TotalSeconds);

            unixTimestamp = unixTimestamp * 1000;

            ConvertedDate = "" + "/Date(" + unixTimestamp + ")/";

            //var dateTime = new DateTime(newDate.Year, newDate.Month, newDate.Day, newDate.Hour, newDate.Minute, newDate.Second, DateTimeKind.Local);
            //var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            //var unixDateTime = (dateTime.ToUniversalTime() - epoch).TotalSeconds;
            //string unixTimeNEw = span.TotalSeconds.ToString("N0");

            //ConvertedDate = "" + "/Date(" + unixDateTime + ")/";

            return ConvertedDate;
        }

        public static async Task<TinDeregistrationResponseModel> GaztTinDeregistrationSubmitRequestData(TinDeregistrationResponseModel tinDeregistrationResponseModel)
        {
            TinDeregistrationSendResponseModel tinDeregistrationSendResponseModel = new TinDeregistrationSendResponseModel();

            tinDeregistrationResponseModel.AEffectiveDtC = "G";
            tinDeregistrationResponseModel.ASubmissionDateC = "G";
            tinDeregistrationResponseModel.ADecDateC = "G";
            tinDeregistrationResponseModel.ADobC = "G";

            try
            {
                ObservableCollection<OutletSetResult> AllOutlets = new ObservableCollection<OutletSetResult>(tinDeregistrationResponseModel.OutletSet.Results);
                List<PermitSetResult> allPermitTypes = new List<PermitSetResult>(tinDeregistrationResponseModel.PermitSet.Results);

                foreach (OutletSetResult outletInfo in AllOutlets)
                {
                    outletInfo.AOutletEffDtTb = ConvertDateFormat(Convert.ToDateTime(outletInfo.AOutletEffDtTb));
                    outletInfo.AOutletEffDtCTb = "G";
                }
                foreach (PermitSetResult permitInfo in allPermitTypes)
                {
                    permitInfo.APermitDobTb = null;
                    permitInfo.APermitEffDtTb = ConvertDateFormat(Convert.ToDateTime(permitInfo.APermitEffDtTb));
                    permitInfo.APermitEffDtCTb = "G";
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            tinDeregistrationSendResponseModel.Metadata = tinDeregistrationResponseModel.Metadata;
            tinDeregistrationSendResponseModel.Assignme = tinDeregistrationResponseModel.Assignme;

            tinDeregistrationSendResponseModel.Caseid = tinDeregistrationResponseModel.Caseid;
            tinDeregistrationSendResponseModel.Xvoidz = tinDeregistrationResponseModel.Xvoidz;
            tinDeregistrationSendResponseModel.TinInPrcFg = tinDeregistrationResponseModel.TinInPrcFg;
            tinDeregistrationSendResponseModel.Taxpayerz = tinDeregistrationResponseModel.Taxpayerz;
            tinDeregistrationSendResponseModel.Submitz = tinDeregistrationResponseModel.Submitz;
            tinDeregistrationSendResponseModel.Status = tinDeregistrationResponseModel.Status;
            tinDeregistrationSendResponseModel.Savez = tinDeregistrationResponseModel.Savez;
            tinDeregistrationSendResponseModel.Rejectz = tinDeregistrationResponseModel.Rejectz;
            tinDeregistrationSendResponseModel.RegIdz = tinDeregistrationResponseModel.RegIdz;
            tinDeregistrationSendResponseModel.PortalUsrz = tinDeregistrationResponseModel.PortalUsrz;
            tinDeregistrationSendResponseModel.PeriodKeyz = tinDeregistrationResponseModel.PeriodKeyz;
            tinDeregistrationSendResponseModel.Operation = tinDeregistrationResponseModel.Operation;
            tinDeregistrationSendResponseModel.OfficerUidz = tinDeregistrationResponseModel.OfficerUidz;
            tinDeregistrationSendResponseModel.Monthz = tinDeregistrationResponseModel.Monthz;
            tinDeregistrationSendResponseModel.LegacyDocNo = tinDeregistrationResponseModel.LegacyDocNo;
            tinDeregistrationSendResponseModel.Langz = tinDeregistrationResponseModel.Langz;
            tinDeregistrationSendResponseModel.FormGuid = tinDeregistrationResponseModel.FormGuid;
            tinDeregistrationSendResponseModel.Fbnumz = tinDeregistrationResponseModel.Fbnumz;
            tinDeregistrationSendResponseModel.Fbnum = tinDeregistrationResponseModel.Fbnum;
            tinDeregistrationSendResponseModel.Dflag = tinDeregistrationResponseModel.Dflag;
            tinDeregistrationSendResponseModel.CreateTxAssesz = tinDeregistrationResponseModel.CreateTxAssesz;
            tinDeregistrationSendResponseModel.Cflag = tinDeregistrationResponseModel.Cflag;
            tinDeregistrationSendResponseModel.CaseGuid = tinDeregistrationResponseModel.CaseGuid;
            tinDeregistrationSendResponseModel.Auditorz = tinDeregistrationResponseModel.Auditorz;
            tinDeregistrationSendResponseModel.ATransTin = tinDeregistrationResponseModel.ATransTin;
            tinDeregistrationSendResponseModel.ATitle = tinDeregistrationResponseModel.ATitle;
            tinDeregistrationSendResponseModel.ATinType = tinDeregistrationResponseModel.ATinType;
            tinDeregistrationSendResponseModel.ATin = tinDeregistrationResponseModel.ATin;
            tinDeregistrationSendResponseModel.ATaxpayerName = tinDeregistrationResponseModel.ATaxpayerName;
            tinDeregistrationSendResponseModel.ASubmissionDateH = tinDeregistrationResponseModel.ASubmissionDateH;
            tinDeregistrationSendResponseModel.ASubmissionDateC = tinDeregistrationResponseModel.ASubmissionDateC;
            tinDeregistrationSendResponseModel.ASubmissionDate = tinDeregistrationResponseModel.ASubmissionDate;
            tinDeregistrationSendResponseModel.AStep = tinDeregistrationResponseModel.AStep;
            tinDeregistrationSendResponseModel.Approvez = tinDeregistrationResponseModel.Approvez;
            tinDeregistrationSendResponseModel.AOffOrigin = tinDeregistrationResponseModel.AOffOrigin;
            tinDeregistrationSendResponseModel.AOffAppNo = tinDeregistrationResponseModel.AOffAppNo;
            tinDeregistrationSendResponseModel.ANm7 = tinDeregistrationResponseModel.ANm7;
            tinDeregistrationSendResponseModel.ANm6 = tinDeregistrationResponseModel.ANm6;
            tinDeregistrationSendResponseModel.ANm5 = tinDeregistrationResponseModel.ANm5;
            tinDeregistrationSendResponseModel.ANm4 = tinDeregistrationResponseModel.ANm4;
            tinDeregistrationSendResponseModel.ANm3 = tinDeregistrationResponseModel.ANm3;
            tinDeregistrationSendResponseModel.ANm2 = tinDeregistrationResponseModel.ANm2;
            tinDeregistrationSendResponseModel.ANm1 = tinDeregistrationResponseModel.ANm1;
            tinDeregistrationSendResponseModel.AmdRsnz = tinDeregistrationResponseModel.AmdRsnz;
            tinDeregistrationSendResponseModel.AIdType = tinDeregistrationResponseModel.AIdType;
            tinDeregistrationSendResponseModel.AIdNo = tinDeregistrationResponseModel.AIdNo;
            tinDeregistrationSendResponseModel.AFormStatus = tinDeregistrationResponseModel.AFormStatus;
            tinDeregistrationSendResponseModel.AExpdtH = tinDeregistrationResponseModel.AExpdtH;
            tinDeregistrationSendResponseModel.AExpdtC = tinDeregistrationResponseModel.AExpdtC;
            tinDeregistrationSendResponseModel.AExpdt = tinDeregistrationResponseModel.AExpdt;
            tinDeregistrationSendResponseModel.AEffectiveDtH = tinDeregistrationResponseModel.AEffectiveDtH;
            tinDeregistrationSendResponseModel.AEffectiveDtC = tinDeregistrationResponseModel.AEffectiveDtC;
            tinDeregistrationSendResponseModel.AEffectiveDt = tinDeregistrationResponseModel.AEffectiveDt;
            tinDeregistrationSendResponseModel.ADregReason = tinDeregistrationResponseModel.ADregReason;
            tinDeregistrationSendResponseModel.ADregOpt = tinDeregistrationResponseModel.ADregOpt;
            tinDeregistrationSendResponseModel.ADocumnt9 = tinDeregistrationResponseModel.ADocumnt9;
            tinDeregistrationSendResponseModel.ADocumnt8 = tinDeregistrationResponseModel.ADocumnt8;
            tinDeregistrationSendResponseModel.ADocumnt7 = tinDeregistrationResponseModel.ADocumnt7;
            tinDeregistrationSendResponseModel.ADocumnt6 = tinDeregistrationResponseModel.ADocumnt6;
            tinDeregistrationSendResponseModel.ADocumnt5Txt = tinDeregistrationResponseModel.ADocumnt5Txt;
            tinDeregistrationSendResponseModel.ADocumnt5 = tinDeregistrationResponseModel.ADocumnt5;
            tinDeregistrationSendResponseModel.ADocumnt4Txt = tinDeregistrationResponseModel.ADocumnt4Txt;
            tinDeregistrationSendResponseModel.ADocumnt4 = tinDeregistrationResponseModel.ADocumnt4;
            tinDeregistrationSendResponseModel.ADocumnt3 = tinDeregistrationResponseModel.ADocumnt3;
            tinDeregistrationSendResponseModel.ADocumnt2 = tinDeregistrationResponseModel.ADocumnt2;
            tinDeregistrationSendResponseModel.ADocumnt14 = tinDeregistrationResponseModel.ADocumnt14;
            tinDeregistrationSendResponseModel.ADocumnt13 = tinDeregistrationResponseModel.ADocumnt13;
            tinDeregistrationSendResponseModel.ADocumnt12 = tinDeregistrationResponseModel.ADocumnt12;
            tinDeregistrationSendResponseModel.ADocumnt11 = tinDeregistrationResponseModel.ADocumnt11;
            tinDeregistrationSendResponseModel.ADocumnt10 = tinDeregistrationResponseModel.ADocumnt10;
            tinDeregistrationSendResponseModel.ADocumnt1 = tinDeregistrationResponseModel.ADocumnt1;
            tinDeregistrationSendResponseModel.ADobH = tinDeregistrationResponseModel.ADobH;
            tinDeregistrationSendResponseModel.ADobC = tinDeregistrationResponseModel.ADobC;
            tinDeregistrationSendResponseModel.ADob = tinDeregistrationResponseModel.ADob;
            tinDeregistrationSendResponseModel.ADegister = "1";
            tinDeregistrationSendResponseModel.ADecTitle = tinDeregistrationResponseModel.ADecTitle;
            tinDeregistrationSendResponseModel.ADecTelNo = tinDeregistrationResponseModel.ADecTelNo;
            tinDeregistrationSendResponseModel.ADeclarationChkbox = "1";
            tinDeregistrationSendResponseModel.ADecDesig = tinDeregistrationResponseModel.ADecDesig;
            tinDeregistrationSendResponseModel.ADecDateH = tinDeregistrationResponseModel.ADecDateH;
            tinDeregistrationSendResponseModel.ADecDateC = tinDeregistrationResponseModel.ADecDateC;
            tinDeregistrationSendResponseModel.ADecDate = tinDeregistrationResponseModel.ADecDate;
            tinDeregistrationSendResponseModel.ADateFormat = "";
            tinDeregistrationSendResponseModel.ABranchTxt = tinDeregistrationResponseModel.ABranchTxt;
            tinDeregistrationSendResponseModel.ABpKind = tinDeregistrationResponseModel.ABpKind;
            tinDeregistrationSendResponseModel.PermitSet = tinDeregistrationResponseModel.PermitSet.Results;
            tinDeregistrationSendResponseModel.OutletSet = tinDeregistrationResponseModel.OutletSet.Results;
            tinDeregistrationSendResponseModel.OffNotesSet = new List<string>();
            tinDeregistrationSendResponseModel.AttDetSet = new List<string>();
            tinDeregistrationSendResponseModel.ReturnSet = new List<string>();
            tinDeregistrationSendResponseModel.ADecName = tinDeregistrationResponseModel.ADecName;
            tinDeregistrationSendResponseModel.PermitTableSet = new List<string>();

            List<PermitSetResult> temp = new List<PermitSetResult>();

            foreach (PermitSetResult permitInfo in tinDeregistrationSendResponseModel.PermitSet)
            {
                if (permitInfo.APermitIdNoTb == null)
                    permitInfo.APermitIdNoTb = "";

                if (String.IsNullOrEmpty(permitInfo.APermitTransTinTb))
                    permitInfo.APermitTransTinTb = " ";
                
                if (permitInfo.APermitDregRsnTb == null)
                    permitInfo.APermitDregRsnTb = string.Empty;

                temp.Add(permitInfo);
            }

            tinDeregistrationSendResponseModel.PermitSet = temp.ToArray();

            if (CrossConnectivity.Current.IsConnected)
            {
                TinDeregistrationResponseModel _newRequestSummaryDataResponse = new TinDeregistrationResponseModel();
                string NewToken = string.Empty;

                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string lang = GetLangZParameterAREN();
                    string url = Constants.TinDeregistrationNewRequestUrl;

                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");

                    var uri = new Uri(url);
                    var serilized = JsonConvert.SerializeObject(tinDeregistrationSendResponseModel);

                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    HttpResponseMessage tinDeregResponse = await client.PostAsync(uri, contentPost);

                    if (tinDeregResponse != null)
                    {
                        if (tinDeregResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = tinDeregResponse.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                            App.IsSessionExpired = false;
                        }
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expired")) || (0 == String.Compare(NewToken, "Invalid Token")))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }

                        String TinDeregResponseJson = tinDeregResponse.Content.ReadAsStringAsync().Result;

                        if (tinDeregResponse.StatusCode == HttpStatusCode.BadRequest)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(TinDeregResponseJson);
                           try
                            {
                                if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                                {
                                    ErrorMessageForUnlockAccount = errorMesg.error.innererror.errordetails[0].message;
                                    String WithReplacedString = ErrorMessageForUnlockAccount.Replace("An exception was raised", string.Empty);
                                    ErrorMessageForUnlockAccount = WithReplacedString;
                                    //ErrorMessageForVAT
                                    throw new GAZTErrorException(ErrorMessageForUnlockAccount);
                                }
                               
                            }
                            catch(Exception ex)
                            {
                                if (errorMesg != null && errorMesg.error != null)
                                {
                                    throw new GAZTErrorException(errorMesg.error.message.value);
                                }
                            }
                        }
                        else if (!string.IsNullOrEmpty(TinDeregResponseJson))
                        {
                            TinDeregResponseJson = JObject.Parse(TinDeregResponseJson)["d"].ToString();
                            _newRequestSummaryDataResponse = JsonConvert.DeserializeObject<TinDeregistrationResponseModel>(TinDeregResponseJson);
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
                catch (GAZTErrorException ex)
                {
                    Console.WriteLine(ex);
                    throw new GAZTErrorException(ex.Message);
                }

                catch (Exception ex)
                {
                    throw new GAZTErrorException(AppResources.Somethingwentwrong);
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }



        public async static Task<TinDeregistrationReasonSetDataModel> GaztTinDeregistrationReasonData()
        {
            TinDeregistrationReasonSetDataModel _tinDeregistrationReasonSetDataModel = new TinDeregistrationReasonSetDataModel();

            if (CrossConnectivity.Current.IsConnected)
            {

                string NewToken = string.Empty;
                try
                {
                    // eUser = "00001000000008322132";

                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    /// sap / opu / odata / SAP / ZDP_ITAP_SRV / TPFILLSet(Euser1 = '00000001000008323131',
                    //Fbguid = 'undefined', Fbnum = '81000003264', Fbtyp = 'TPCV', Gpart = '3100088087', Lang = 'EN', Persl = '', Status = 'E0013', Dispflag = '')
                    ////https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/Z_DREGRESN_SRV/ZDS_DETSet(Partner='3000363416',Spars='E')?&$expand=REASONSet
                    String url = Constants.TinDeregistrationReasonSetUrl + "Partner='"+App.LoginDataRetrieved.TIN+"',Spars='"+ lang + "')?&$expand=REASONSet&$format=json";
                    var uri = new Uri(url);

                    HttpResponseMessage _tinDeregReasonRequestResponse = await client.GetAsync(uri);

                    if (_tinDeregReasonRequestResponse != null)
                    {
                        if (_tinDeregReasonRequestResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _tinDeregReasonRequestResponse.Headers;
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

                        String _responseData = _tinDeregReasonRequestResponse.Content.ReadAsStringAsync().Result;
                        if (_tinDeregReasonRequestResponse.StatusCode == HttpStatusCode.BadRequest)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_responseData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorCode = errorMesg.error.innererror.errordetails[0].code;
                                ErrorMessageForUnlockAccount = errorMesg.error.innererror.errordetails[0].message;

                                String WithReplacedString = ErrorMessageForUnlockAccount.Replace("An exception was raised", string.Empty);
                                ErrorMessageForUnlockAccount = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTErrorException(ErrorMessageForUnlockAccount);
                            }
                        }
                        else if (!string.IsNullOrEmpty(_responseData))
                        {
                            _responseData = JObject.Parse(_responseData)["d"].ToString();
                            _tinDeregistrationReasonSetDataModel = JsonConvert.DeserializeObject<TinDeregistrationReasonSetDataModel>(_responseData);
                            if (_tinDeregistrationReasonSetDataModel == null)
                            {
                                throw new GAZTErrorException(AppResources.ZZSomethingwentwrong);
                            }
                        }
                        else
                        {
                            throw new GAZTErrorException(AppResources.ZZSomethingwentwrong);
                        }
                    }

                    return _tinDeregistrationReasonSetDataModel;
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
        #endregion

        #region Zakat
        public async static Task<ZakatInstalmentPlanRequestListModel> GAZTGetZakatInstalmentPlanRequestList(string zuser, string fbguid, string euser1)
        {

            if (CrossConnectivity.Current.IsConnected)
            {
                ZakatInstalmentPlanRequestListModel _ZakatInstalmentPlanRequestList = new ZakatInstalmentPlanRequestListModel();
                string NewToken = string.Empty;
                try
                {

                    string callServ = "IPRA";
                    zuser = "";
                    euser1 = "null";
                    string auditor = "null";
                    string euser2 = "null";
                    string euser3 = "null";
                    string euser4 = "null";
                    string euser5 = "null";
                    fbguid = "";
                    string userTyp = "TP";

                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);



                    /// sap / opu / odata / SAP / ZDP_IPRF_WI_SRV / HdrSet(CallServ = 'IPRA', HostName = '', Zuser = 'ABURAKAN1385@GMAIL.COM', Bpnum = '', 
                    //Auditor = 'null', Lang = 'E', Euser1 = '00000000001008320632', Euser2 = 'null', Euser3 = 'null', Euser4 = 'null', Euser5 = 'null',
                    //Fbguid = '005056B1F8FB1EEAB9BB446F7D6CB7D2', 
                    //UserTin = '', Fbnum = '', UserTyp = 'TP') ?$expand = WorklistSet,AuthServSet,EvtNotif12Set,EvtNotif1Set,RevokeListSet &$format = json


                    String url = Constants.ZakatListOfInstalmentplanRequestUrl + "CallServ='" + callServ + "',HostName='" + "',Bpnum='" + App.LoginDataRetrieved.TIN + "',Zuser='" + "'," +
                       "Auditor='" + auditor + "'," +
                     "Lang='" + lang + "',Euser1='" + euser1 + "',Euser2='" + euser2 + "',Euser3='" + euser3 + "'," +
                     "Euser4='" + euser4 + "',Euser5='" + euser5 + "',Fbguid='" + fbguid + "',UserTin='" + "',Fbnum='" + "',UserTyp='" + userTyp + "')?$expand=WorklistSet,AuthServSet,EvtNotif12Set,EvtNotif1Set,RevokeListSet&$format=json";
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTzakatInstalmentDataResponse = await client.GetAsync(uri);



                    if (GAZTzakatInstalmentDataResponse != null)
                    {
                        if (GAZTzakatInstalmentDataResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTzakatInstalmentDataResponse.Headers;
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
                        var _zakatInstalmentRequestData = GAZTzakatInstalmentDataResponse.Content.ReadAsStringAsync().Result;
                        _ZakatInstalmentPlanRequestList = JsonConvert.DeserializeObject<ZakatInstalmentPlanRequestListModel>(_zakatInstalmentRequestData);

                        if (!string.IsNullOrEmpty(_zakatInstalmentRequestData) && _ZakatInstalmentPlanRequestList.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatInstalmentRequestData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _ZakatInstalmentPlanRequestList;
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


        public async static Task<ZakatInstalmentPlanRequestListModel> GAZTGetZakatRevokeList(string zuser, string fbguid, string euser1)
        {

            if (CrossConnectivity.Current.IsConnected)
            {
                ZakatInstalmentPlanRequestListModel _zakatRevokeList = new ZakatInstalmentPlanRequestListModel();
                string NewToken = string.Empty;
                try
                {

                    string callServ = "IPRR";
                    zuser = "";
                    euser1 = "null";
                    string auditor = "null";
                    string euser2 = "null";
                    string euser3 = "null";
                    string euser4 = "null";
                    string euser5 = "null";
                    fbguid = "";
                    string userTyp = "TP";


                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);


                    //     / sap / opu / odata / SAP / ZDP_IPRF_WI_SRV / HdrSet(CallServ = 'IPRR', HostName = '',
                    //Zuser = 'ABURAKAN1385@GMAIL.COM', Bpnum = '', Auditor = 'null', Lang = 'E', Euser1 = '00001000000008320614', Euser2 = 'null', Euser3 = 'null',
                    //Euser4 = 'null', Euser5 = 'null', Fbguid = '005056B1F8FB1EEAB9BBAD0725EE38FF', UserTin = '', Fbnum = '', UserTyp = 'TP')
                    // ?$expand = WorklistSet,AuthServSet,EvtNotif12Set,EvtNotif1Set,RevokeListSet &$format = json

                    //
                    String url = Constants.ZakatRevokeRequestListUrl + "CallServ='" + callServ + "',HostName='" + "',Bpnum='" + App.LoginDataRetrieved.TIN + "',Zuser='" + "'," +
                       "Auditor='" + auditor + "'," +
                     "Lang='" + lang + "',Euser1='" + euser1 + "',Euser2='" + euser2 + "',Euser3='" + euser3 + "'," +
                     "Euser4='" + euser4 + "',Euser5='" + euser5 + "',Fbguid='" + fbguid + "',UserTin='" + "',Fbnum='" + "',UserTyp='" + userTyp + "')?$expand=WorklistSet,AuthServSet,EvtNotif12Set,EvtNotif1Set,RevokeListSet&$format=json";

                    var uri = new Uri(url);
                    HttpResponseMessage GAZTzakatRevokeInstalmentDataResponse = await client.GetAsync(uri);


                    if (GAZTzakatRevokeInstalmentDataResponse != null)
                    {
                        if (GAZTzakatRevokeInstalmentDataResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTzakatRevokeInstalmentDataResponse.Headers;
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

                        String _zakatRevokeInstalmentRequestData = GAZTzakatRevokeInstalmentDataResponse.Content.ReadAsStringAsync().Result;
                        _zakatRevokeList = JsonConvert.DeserializeObject<ZakatInstalmentPlanRequestListModel>(_zakatRevokeInstalmentRequestData);

                        if (!string.IsNullOrEmpty(_zakatRevokeInstalmentRequestData) && _zakatRevokeList.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatRevokeInstalmentRequestData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _zakatRevokeList;
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



        public async static Task<SummaryDisplayModel> GAZTGetZakatRequestDisplayData(string fbnum, string status)
        {



            if (CrossConnectivity.Current.IsConnected)
            {
                SummaryDisplayModel _zakatRequestDisplayModel = new SummaryDisplayModel();
                string NewToken = string.Empty;
                try
                {



                    var summaryInputs = await GAZTGetZakatSummaryInputData(fbnum, status);



                    string euser = "00000000000000000000";
                    string fbguid = summaryInputs.d.Fbguid;
                    // string fbguid = "005056B1F8FB1EDABAF675538BB331C8";



                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);



                    //https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/Z_INSTALLMENT_PLAN_SRV/z_installmentSet
                    //    (Auditorz = '', Taxpayerz = '', Fbnumz = '', PeriodKeyz = '', Langz = 'E', Euser = '00000000000000000000',
                    //Fbguid = '005056B1F8FB1EEAB9D3137F3DD4F7DA', Submitz = '', Savez = '', UserTin = '')
                    //    ?&$expand = Off_notesSet,AttDetSet,Z_INVOICE_UI5Set,z_invoiceSet,z_proposedinsSet



                    /* String url = Constants.ZakatRequestDisplayUrl + "Auditorz='" + "'," +
                 "Taxpayerz='" + "',Fbnumz='" + "',PeriodKeyz='" + "',Langz='" + lang + "'," +
                 "Euser='" + euser + "',Fbguid='" + fbguid + "',Submitz='" + "',Savez='" + "',UserTin='" + "')?&$expand=Off_notesSet,AttDetSet,Z_INVOICE_UI5Set,z_invoiceSet,z_proposedinsSet&$format=json";*/




                    String url = Constants.ZakatRequestDisplayUrl + "Tin='',Euser='00000000000000000000',Langz='EN',Fbguid='" + fbguid + "'," +
                        "Fbnum='" + fbnum + "',FormMode='S')?$expand=AttachSet,NotesSet,FnDtlSet&$format=json";
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTzakatDisplayResponse = await client.GetAsync(uri);




                    if (GAZTzakatDisplayResponse != null)
                    {
                        if (GAZTzakatDisplayResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTzakatDisplayResponse.Headers;
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
                        String _zakatDisplayRequestData = GAZTzakatDisplayResponse.Content.ReadAsStringAsync().Result;
                        _zakatRequestDisplayModel = JsonConvert.DeserializeObject<SummaryDisplayModel>(_zakatDisplayRequestData);



                        if (!string.IsNullOrEmpty(_zakatDisplayRequestData) && _zakatRequestDisplayModel.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatDisplayRequestData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _zakatRequestDisplayModel;
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

        // public static string GetZAKATSummaryInputURL = BaseUrlOfODataServices + "/sap/opu/odata/SAP/ZDP_IPRF_WI_SRV/UserFillSet(";


        public async static Task<ZakatSummaryInputModel> GAZTGetZakatSummaryInputData(string fbnum, string status)
        {
            ZakatSummaryInputModel _zakatSummaryInputModel = new ZakatSummaryInputModel();



            if (CrossConnectivity.Current.IsConnected)
            {



                string NewToken = string.Empty;
                try
                {
                    string fbtyp = "IPRF";



                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);



                    /*https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_IPRF_WI_SRV/UserFillSet(Euser1='00000000000000000000',Fbguid='',Fbnum='010001119386',Fbtyp='IPRF',
                     Gpart = '3100000567',Lang = 'EN',Persl = '',Status = 'IP014',TaxOffUid = '')?$format = json*/





                    String url = Constants.GetZAKATSummaryInputURL + "Euser1='00000000000000000000',Fbguid='" + "',Fbnum='" + fbnum + "',Fbtyp='" + fbtyp + "'," +
                     "Gpart='" + App.LoginDataRetrieved.TIN + "',Lang='" + lang + "',Persl='" + "',Status='" + status + "',TaxOffUid='" + "')?$format=json";
                    var uri = new Uri(url);
                    HttpResponseMessage _zakatSumamryInputResponse = await client.GetAsync(uri);




                    if (_zakatSumamryInputResponse != null)
                    {
                        if (_zakatSumamryInputResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _zakatSumamryInputResponse.Headers;
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
                        String _ZakatSummaryInputData = _zakatSumamryInputResponse.Content.ReadAsStringAsync().Result;
                        _zakatSummaryInputModel = JsonConvert.DeserializeObject<ZakatSummaryInputModel>(_ZakatSummaryInputData);





                        if (!string.IsNullOrEmpty(_ZakatSummaryInputData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_ZakatSummaryInputData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _zakatSummaryInputModel;
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



        public async static Task<ZakatRevokeValidateModel> GAZTGetZakatRevokeValidate(string fbnum)
        {
            ZakatRevokeValidateModel _zakatRevokeValidateModel = new ZakatRevokeValidateModel();

            if (CrossConnectivity.Current.IsConnected)
            {

                string NewToken = string.Empty;
                try
                {
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    //   / sap / opu / odata / SAP / ZDP_IPRF_M_SRV / RevChkSet(Fbnum = '85000000679')
                    String url = Constants.ZakatValidateRevokeListUrl + "Fbnum='" + fbnum + "')?&$format=json";
                    var uri = new Uri(url);
                    HttpResponseMessage _zakatRevokeValidateResponse = await client.GetAsync(uri);
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
                                //ErrorMessageForVAT
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
        //Send OTP and Validate OTP both are same.return model also same but different values.
        public static async Task<ZakatRevokeSendSMSModel> GAZTZakatRevokeSendOTP(string fbNum, string code)
        {
            ZakatRevokeSendSMSModel _zakatRevokeSendSMSModel = new ZakatRevokeSendSMSModel();
            if (CrossConnectivity.Current.IsConnected)
            {

                string NewToken = string.Empty;
                try
                {
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.ZakateRevokeSendOTPUrl + "Fbnum eq'" + fbNum + "'and Code eq'" + code + "'and  Tin eq '" + App.LoginDataRetrieved.TIN + "'&$format=json";

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
                                //ErrorMessageForVAT
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

        #region VATObjection
        public async static Task<VATObjectionListModel> GAZTGetVATObjectionList()
        {
            VATObjectionListModel _vATObjectionListModel = new VATObjectionListModel();

            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    string taxType = "VT";
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    // https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_ITAP_SRV/HEADERSet(TaxType='VT',AudTin='',Gpart='3060200042',Lang='E',UserTin='')
                    //  ?= &$expand = ASSLISTSet,STATUSSet,REQTYPSet &$format = json

                   // String url = Constants.GetVATObjectionListURL + "TaxType='" + taxType + "',AudTin='" + "',Gpart='" + App.LoginDataRetrieved.TIN + "',Lang='" + lang + "'," +"UserTin='" + "')?$expand=ASSLISTSet,STATUSSet,REQTYPSet&$format=json";

                    String url = Constants.GetVATObjectionListURL + "TaxType='" + taxType + "',AudTin='" + "',Gpart='" + App.LoginDataRetrieved.TIN + "',Lang='" + lang + "'," + "UserTin='" + "')?$expand=ASSLISTSet,STATUSSet,REQTYPSet&$format=json";

                    var uri = new Uri(url);
                    HttpResponseMessage vATObjectionListResponse = await client.GetAsync(uri);
                    if (vATObjectionListResponse != null)
                    {
                        if (vATObjectionListResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = vATObjectionListResponse.Headers;
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
                        String __vATObjectionListData = vATObjectionListResponse.Content.ReadAsStringAsync().Result;
                        _vATObjectionListModel = JsonConvert.DeserializeObject<VATObjectionListModel>(__vATObjectionListData);
                        if (!string.IsNullOrEmpty(__vATObjectionListData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__vATObjectionListData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _vATObjectionListModel;
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

        public async static Task<VATObjectionSummaryModel> GAZTGetVATObjectionSummary(string FBGuid)
        {
            VATObjectionSummaryModel _vATObjectionSummaryModel = new VATObjectionSummaryModel();

            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                   
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    //   https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_REV_SRV/HeaderSet
                    //(FormGuid = '005056B1F8FB1EDABAF453EBDAB0ACB7', Fbnumx = '', Gpartx = '', Langx = 'E', Officerx = '', PortalUsrx = '', Euserx = '00000000001008324001', Appfg = 'N')
                    //?$expand = AddressSet,AttdetSet,NotesSet,QuesListSet,ReasonSet,IdDetailSet,MainReasonSet,SecurityDtl &$format = json
                    String url = "";
                    if (string.IsNullOrEmpty(FBGuid)) {

                        url = Constants.GetVATObjectionSummaryURL + "FormGuid='" + "',Fbnumx='" + "',Gpartx='" + App.LoginDataRetrieved.TIN + "',Langx='" + lang + "'," +
                     "Officerx='" + "',PortalUsrx='" + "',Euserx='" + "',Appfg='" + "')?$expand=AddressSet,AttdetSet,NotesSet,QuesListSet,ReasonSet,IdDetailSet,MainReasonSet,SecurityDtl&$format=json";
                    }
                    else {
                         url = Constants.GetVATObjectionSummaryURL + "FormGuid='" + "',Fbnumx='" + FBGuid + "',Gpartx='" + "',Langx='" + lang + "'," +
                     "Officerx='" + "',PortalUsrx='" + "',Euserx='00000000000000000000',Appfg='" + "')?$expand=AddressSet,AttdetSet,NotesSet,QuesListSet,ReasonSet,IdDetailSet,MainReasonSet,SecurityDtl&$format=json";
                    }

                    
                    var uri = new Uri(url);
                    HttpResponseMessage vATObjectionSummaryResponse = await client.GetAsync(uri);
                    if (vATObjectionSummaryResponse != null)
                    {
                        if (vATObjectionSummaryResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = vATObjectionSummaryResponse.Headers;
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
                        String __vATObjectionSummaryData = vATObjectionSummaryResponse.Content.ReadAsStringAsync().Result;
                        _vATObjectionSummaryModel = JsonConvert.DeserializeObject<VATObjectionSummaryModel>(__vATObjectionSummaryData);
                        if (!string.IsNullOrEmpty(__vATObjectionSummaryData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__vATObjectionSummaryData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _vATObjectionSummaryModel;
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

        //Excel sheet API2 Called to get the on screen button codes and form mode(editable/uneditable). 
        public async static Task<VATObjectionButtonFormModeModel> GAZTGetVATObjectionFormMode(string status)
        {
            VATObjectionButtonFormModeModel _vATObjectionButtonFormModeModel = new VATObjectionButtonFormModeModel();

            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    status = "E0001";
                    string formProc = "ZTAX_VT_REV";

                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    ///sap/opu/odata/SAP/ZDP_REVIEW_UI_SRV/VR_UI_HDRSet(Fbnum='',Lang='E',Officer='',Gpart='',Status='E0001',TxnTp='',Formproc='ZTAX_VT_REV')
                    ///?&$expand=VR_UI_BTNSet,ELGBL_DOCSet,ReasonSet

                    String url = Constants.GetVATObjectionButtonFormModeURL + "Fbnum='" + "',Lang='" + lang + "',Officer='" + "',Gpart='" + "'," +
                     "Status='" + status + "',TxnTp='" + "',Formproc='" + formProc + "')?&$expand=VR_UI_BTNSet,ELGBL_DOCSet,ReasonSet&$format=json";

                    var uri = new Uri(url);
                    HttpResponseMessage vATObjectionButtonFormResponse = await client.GetAsync(uri);
                    if (vATObjectionButtonFormResponse != null)
                    {
                        if (vATObjectionButtonFormResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = vATObjectionButtonFormResponse.Headers;
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
                        String __vATObjectionButtonFormData = vATObjectionButtonFormResponse.Content.ReadAsStringAsync().Result;
                        _vATObjectionButtonFormModeModel = JsonConvert.DeserializeObject<VATObjectionButtonFormModeModel>(__vATObjectionButtonFormData);
                        if (!string.IsNullOrEmpty(__vATObjectionButtonFormData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__vATObjectionButtonFormData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _vATObjectionButtonFormModeModel;
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


        //Excel sheet API3 Called to get the list of rejected forms
        public async static Task<VATObjectionRejectedFormModel> GAZTGetVATObjectionFormRejected(string fbustx, string RvRsn, string rvSubRsn, string UserTypx)
        {
            VATObjectionRejectedFormModel _vATObjectionRejectedFormModel = new VATObjectionRejectedFormModel();

            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    // fbustx = "E0001";
                    UserTypx = "TP";
                    // RvRsn = "VTAS";
                    // rvSubRsn = "0090";
                    string formprocx = "ZTAX_VT_REV";

                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    /// sap / opu / odata / SAP / ZDP_VAT_GET_REJFRM_SRV / HeaderSet(Langx = 'EN', Gpartx = '3102407123', TxnTpx = '', Fbustx = 'E0001', Fbstax = '',
                    //UserTypx = 'TP', RvRsn = 'VTAS', RvSubRsn = '0090', Fbnumx = '', Sopbel = '', Formprocx = 'ZTAX_VT_REV') ?$expand=RejectedFormSet

                    String url = Constants.GetVATObjectionRejectedFormURL + "Langx='" + lang + "',Gpartx='" + App.LoginDataRetrieved.TIN + "',TxnTpx='" + "',Fbustx='" + fbustx + "'," +
                     "Fbstax='" + "',UserTypx='" + UserTypx + "',RvRsn='" + RvRsn + "',RvSubRsn='" + rvSubRsn + "',Fbnumx='" + "',Sopbel='" + "',Formprocx='" + formprocx + "')?$expand=RejectedFormSet&$format=json";

                    var uri = new Uri(url);
                    HttpResponseMessage vATObjectionRejectedFormResponse = await client.GetAsync(uri);
                    if (vATObjectionRejectedFormResponse != null)
                    {
                        if (vATObjectionRejectedFormResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = vATObjectionRejectedFormResponse.Headers;
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
                        String __vATObjectionRejectedFormData = vATObjectionRejectedFormResponse.Content.ReadAsStringAsync().Result;
                        _vATObjectionRejectedFormModel = JsonConvert.DeserializeObject<VATObjectionRejectedFormModel>(__vATObjectionRejectedFormData);
                        if (!string.IsNullOrEmpty(__vATObjectionRejectedFormData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__vATObjectionRejectedFormData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _vATObjectionRejectedFormModel;
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

        ////Excel sheet API4 for Attachment API
        public static async Task<AttachmentRootOject> GAZTSaveVATObjectionAttachment(byte[] AttachmentByte, string fileName, string retGuid, string dotyp, string contentType)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    // / sap / opu / odata / SAP / ZDP_INDTAX_ATT_SRV / AttachSet(OutletRef = '',
                    // RetGuid = '005056B1CB171EEAB4FD713DB2A24D4A', Flag = 'N', Dotyp = 'RAGA', SchGuid = '', Srno = 1, Doguid = '', AttBy = 'TP') / AttachMedSet

                    string flag = "N";
                    dotyp = "RAGA";
                    string attBy = "TP";
                    string srNo = "1";
                    AttachmentRootOject _attachment = new AttachmentRootOject();
                    char LangZ = GetLangZParameter();

                    String url = Constants.GetVATObjectionAttachmentURL + "OutletRef='" + "',RetGuid='" + retGuid + "',Flag='" + flag + "'" + ",Dotyp='" + dotyp + "',SchGuid='" + "',Srno='" + srNo + "',Doguid='" + "',AttBy='" + attBy + "')/AttachMedSet";
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


        //Excel sheet API5 Called to get the list of rejected forms
        public async static Task<VATObjectionSecurityAmountModel> GAZTGetVATObjectionSecurityAmount(Decimal disamt, Decimal liaamt, Decimal clramt)
        {
            VATObjectionSecurityAmountModel _vATObjectionSecurityAmountModel = new VATObjectionSecurityAmountModel();

            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    string amttp = "P";
                    ///    disamt = "-76000m";
                    ///   liaamt = "-76000m";
                    //   clramt = "-76000m";


                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    /// sap / opu / odata / SAP / ZDP_VAT_NW_REV_SRV / GetSecurityAmountSet(Disamt = -76000m, Liaamt = -76000m, Clramt = -76000m, Amttp = 'P')
                    String url = Constants.GetVATObjectionSecurityURL + "Disamt=" + disamt + "m,Liaamt=" + liaamt + "m,Clramt=" + clramt + "m,Amttp='" + amttp + "')?$format=json";
                    //   String url = Constants.GetVATObjectionRejectedFormURL + "Disamt=" + disamt + ",Liaamt=" + liaamt + ",Clramt=" + clramt+ ",Amttp='" + amttp + "')";

                    var uri = new Uri(url);
                    HttpResponseMessage vATObjectionSecurityAmountResponse = await client.GetAsync(uri);
                    if (vATObjectionSecurityAmountResponse != null)
                    {
                        if (vATObjectionSecurityAmountResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = vATObjectionSecurityAmountResponse.Headers;
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
                        String __vATObjectionSecurityAmountData = vATObjectionSecurityAmountResponse.Content.ReadAsStringAsync().Result;
                        _vATObjectionSecurityAmountModel = JsonConvert.DeserializeObject<VATObjectionSecurityAmountModel>(__vATObjectionSecurityAmountData);
                        if (!string.IsNullOrEmpty(__vATObjectionSecurityAmountData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__vATObjectionSecurityAmountData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _vATObjectionSecurityAmountModel;
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

        //Excel sheet API6  To decide whether to enable to submit button or not
        public async static Task<VATObjectionEnableSubmitModel> GAZTGetVATObjectionEnableSubmit(string statusx, string rvRsn, string rvSubRsn, string rejFb)
        {
            VATObjectionEnableSubmitModel _vATObjectionEnableSubmitModel = new VATObjectionEnableSubmitModel();

            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    //string amttp = "P";
                    //  statusx = "E0001";
                    // rvRsn = "-76000m";
                    //  rvSubRsn = "-76000m";
                    // rejFb = "";


                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    /// sap / opu / odata / SAP / ZDP_VAT_NW_REV_SRV / GetSubmitEnabledSet(Fbnumx = '', Gpartx = '3102407123', Statusx = 'E0001', RvRsn = 'VTAS', RvSubRsn = '0090', RejFb = '65000558139') ?$format = json


                    String url = Constants.GetVATObjectionEnableSubmitURL + "Fbnumx='" + "',Gpartx='" + App.LoginDataRetrieved.TIN + "',Statusx='" + statusx + "'" +
                        ",RvRsn='" + rvRsn + "',RvSubRsn='" + rvSubRsn + "',RejFb='" + rejFb + "')?$format=json";

                    var uri = new Uri(url);
                    HttpResponseMessage _vATObjectionEnableSubmitResponse = await client.GetAsync(uri);
                    if (_vATObjectionEnableSubmitResponse != null)
                    {
                        if (_vATObjectionEnableSubmitResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _vATObjectionEnableSubmitResponse.Headers;
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
                        String _vATObjectionEnableSubmitData = _vATObjectionEnableSubmitResponse.Content.ReadAsStringAsync().Result;
                        _vATObjectionEnableSubmitModel = JsonConvert.DeserializeObject<VATObjectionEnableSubmitModel>(_vATObjectionEnableSubmitData);
                        if (!string.IsNullOrEmpty(_vATObjectionEnableSubmitData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_vATObjectionEnableSubmitData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _vATObjectionEnableSubmitModel;
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

        ////Excel sheet API7 To validate ID and fetch taxpayer name
        public async static Task<VATObjectionValidateTaxpayerModel> GAZTGetVATObjectionValidateTaxPayer(string idnum, string idtype, string passExpDt, string taxpDob)
        {
            VATObjectionValidateTaxpayerModel _vATObjectionValidateTaxpayerModel = new VATObjectionValidateTaxpayerModel();

            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {


                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    /// sap / opu / odata / SAP / Z_REG_GET_TAXPAYER_SRV / taxpayer_nameSet(Tin = '', Idtype = 'ZS0003', Idnum = '674634544544', Country = '', PassExpDt = '20200702', TaxpDob = '20200702')


                    String url = Constants.GetVATObjectionValidateTaxPayerNameURL + "Tin='" + "',Idtype='" + idtype + "',Idnum='" + idnum + "'" +
                        ",Country='" + "',PassExpDt='" + passExpDt + "',TaxpDob='" + taxpDob + "')?$format=json";

                    var uri = new Uri(url);
                    HttpResponseMessage _vATObjectionValidateTaxpayerResponse = await client.GetAsync(uri);
                    if (_vATObjectionValidateTaxpayerResponse != null)
                    {
                        if (_vATObjectionValidateTaxpayerResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _vATObjectionValidateTaxpayerResponse.Headers;
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
                        String _VATObjectionValidateTaxpayerData = _vATObjectionValidateTaxpayerResponse.Content.ReadAsStringAsync().Result;
                        _vATObjectionValidateTaxpayerModel = JsonConvert.DeserializeObject<VATObjectionValidateTaxpayerModel>(_VATObjectionValidateTaxpayerData);
                        if (!string.IsNullOrEmpty(_VATObjectionValidateTaxpayerData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_VATObjectionValidateTaxpayerData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _vATObjectionValidateTaxpayerModel;
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
        //Excel sheet API9 To Called on the press of Download Acknowledgement Button
        public static string GetVATObjectionDownloadAck(string fbnum)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {

                    /// sap / opu / odata / SAP / Z_GET_ACK_LETTER_SRV / Ack_letterSet(Fbnum = '90000000231') /$value
                    String Url = string.Empty;
                    Url = Constants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_ACK_LETTER_SRV/Ack_letterSet(Fbnum='" + fbnum + "')/$value";

                    return Url;
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
        ////Excel sheet API10  To generate SADAD  && API11 On press of refresh button for SADAD
        public async static Task<VATObjectionGenrateSadadModel> GAZTGetVATObjectionGenrateorRefreshSADAD(string fbnum, decimal Disamt,
            decimal Liaamt, string Abrzu, string Abrzo, decimal Secamt, string Security, string Persl, bool isFlagenable)
        {
            VATObjectionGenrateSadadModel _vATObjectionGenrateSadadModel = new VATObjectionGenrateSadadModel();

            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {

                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    /// sap / opu / odata / SAP / ZDP_GEN_SADAD_SRV / GetSADADSet(Fbnum = '90000003100', Gpart = '3102285153', Disamt = 125000m, Liaamt = 125000m, Sectp = 'C',
                    /// Abrzu = datetime'2018-04-01T00:00:00', Abrzo = datetime'2018-04-30T00:00:00', Flag = true, Secamt = 125000m, Security = '100000002498', Persl = '18AP')

                    // https://sapgatewayqa.gazt.gov.sa:443/sap/opu/odata/SAP/ZDP_GEN_SADAD_SRV/GetSADADSet(Fbnum='65000212928',Gpart='3100000567',Disamt='0.00',Liaamt='541750.00',Sectp='C',Abrzu=datetime'5/1/2019 12:00:00 AM',Abrzo=datetime'5/31/2019 12:00:00 AM',Flag='true',Secamt='',Security='',Persl='19MY')?$format=json

                    String url = "";
                    if (isFlagenable) {

                         url = Constants.GetVATObjectionGenrateorRefreshSADADURL + "Fbnum='" + fbnum + "',Gpart='" + App.LoginDataRetrieved.TIN + "',Disamt=" + Disamt + "" +
                        "m,Liaamt=" + Liaamt + "m,Sectp='C',Abrzu=datetime'" + Abrzu + "',Abrzo=datetime'" + Abrzo + "',Flag=true,Secamt=" + Secamt + "m,Security='" + Security + "',Persl='" + Persl + "')?$format=json";
                    }
                    else {
                         url = Constants.GetVATObjectionGenrateorRefreshSADADURL + "Fbnum='" + fbnum + "',Gpart='" + App.LoginDataRetrieved.TIN + "',Disamt=" + Disamt + "" +
                        "m,Liaamt=" + Liaamt + "m,Sectp='C',Abrzu=datetime'" + Abrzu + "',Abrzo=datetime'" + Abrzo + "',Flag=false,Secamt=" + Secamt + "m,Security='" + Security + "',Persl='" + Persl + "')?$format=json";
                    }


                    

                    var uri = new Uri(url);
                    HttpResponseMessage _vATObjectionGenrateSadadResponse = await client.GetAsync(uri);
                    if (_vATObjectionGenrateSadadResponse != null)
                    {
                        if (_vATObjectionGenrateSadadResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _vATObjectionGenrateSadadResponse.Headers;
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
                        String __vATObjectionGenrateSadadData = _vATObjectionGenrateSadadResponse.Content.ReadAsStringAsync().Result;
                        _vATObjectionGenrateSadadModel = JsonConvert.DeserializeObject<VATObjectionGenrateSadadModel>(__vATObjectionGenrateSadadData);
                        if (!string.IsNullOrEmpty(__vATObjectionGenrateSadadData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__vATObjectionGenrateSadadData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _vATObjectionGenrateSadadModel;
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

        public async static Task<VATObjectionViewbillModel> GAZTGetVATObjectionViewBill(string opbel, string vtre2)
        {
            VATObjectionViewbillModel _vATObjectionViewbillModel = new VATObjectionViewbillModel();

            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    //https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VAT_NW_REV_SRV/BillDtlSet?$filter=Opbel eq '5000218062'and Vtre2 eq '3100000567193004'&$format=json

                    vtre2 = App.LoginDataRetrieved.TIN;
                    String url = Constants.GetVATObjectionViewBillURL + "Opbel eq'" + opbel + "' and Vtre2 eq'" + vtre2 + "'&$format=json";

                    var uri = new Uri(url);
                    HttpResponseMessage _vATObjectionViewbillResponse = await client.GetAsync(uri);
                    if (_vATObjectionViewbillResponse != null)
                    {
                        if (_vATObjectionViewbillResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _vATObjectionViewbillResponse.Headers;
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
                        String _vATObjectionViewbillData = _vATObjectionViewbillResponse.Content.ReadAsStringAsync().Result;
                        _vATObjectionViewbillModel = JsonConvert.DeserializeObject<VATObjectionViewbillModel>(_vATObjectionViewbillData);
                        if (!string.IsNullOrEmpty(_vATObjectionViewbillData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_vATObjectionViewbillData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _vATObjectionViewbillModel;
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


        public async static Task<VATObjectionSummaryModel> SaveVatReviewObjection(VatObjectionsRequest _vatObjectionDetails)
        {


            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {

                    VATObjectionSummaryModel _vatObjectionResponseObject = new VATObjectionSummaryModel();
                    string LangZ = GetLangZParameterAREN();
                    String url = Constants.PostVATObjectionURL;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    var serilized = JsonConvert.SerializeObject(_vatObjectionDetails);
                    client.DefaultRequestHeaders.Add("Token", "123");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    HttpResponseMessage res = client.PostAsync(uri, contentPost).Result;
                    var _vatObjectionsResponsestr = res.Content.ReadAsStringAsync().Result;
                    _vatObjectionResponseObject = JsonConvert.DeserializeObject<VATObjectionSummaryModel>(_vatObjectionsResponsestr);
                    if (!string.IsNullOrEmpty(_vatObjectionsResponsestr) && _vatObjectionResponseObject.d == null)
                    {
                        ErrorMessage = string.Empty;
                        ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_vatObjectionsResponsestr);
                        if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                        {
                            ErrorMessage = errorMesg.error.innererror.errordetails[0].message;
                            throw new GAZTVATRegistrationInProcessException(ErrorMessage);

                        }

                    }
                    return _vatObjectionResponseObject;
                    //  }



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


        public static async Task<VATObjectionSummaryInputModel> GAZTVATObjectionSummaryInputData(string fbNum, string status)
        {
            VATObjectionSummaryInputModel _vATObjectionSummaryInputModel = new VATObjectionSummaryInputModel();
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);


                    // https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/ZDP_ITAP_SRV/TPFILLSet(Euser1='00001000000000000000',
                    //Fbguid='undefined',Fbnum='70000001515',Fbtyp='RAVT',Gpart='3300105114',Lang='EN',Persl='',Status='E0013',Dispflag='')



                    String url = Constants.VATObjectionSummaryInputURL + "Euser1='00000000000000000000',Fbguid='undefined',Fbnum='" + fbNum + "'," +
                        "Fbtyp='RAVT',Gpart='" + App.LoginDataRetrieved.TIN + "',Lang='" + lang + "',Persl='" + "',Status='" + status + "',Dispflag='" + "')?&$format=json";
                    var uri = new Uri(url);


                    HttpResponseMessage _vATObjectionSummaryInputResponse = await client.GetAsync(uri);
                    if (_vATObjectionSummaryInputResponse != null)
                    {
                        if (_vATObjectionSummaryInputResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _vATObjectionSummaryInputResponse.Headers;
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
                        String _vATObjectionSummaryInputData = _vATObjectionSummaryInputResponse.Content.ReadAsStringAsync().Result;
                        _vATObjectionSummaryInputModel = JsonConvert.DeserializeObject<VATObjectionSummaryInputModel>(_vATObjectionSummaryInputData);
                        if (!string.IsNullOrEmpty(_vATObjectionSummaryInputData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_vATObjectionSummaryInputData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _vATObjectionSummaryInputModel;
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

        #region ZAKATObjections
        public async static Task<ZakatObjectionListModel> GAZTGetZAKATObjectionList()
        {
            ZakatObjectionListModel _ZAKATObjectionList = new ZakatObjectionListModel();

            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    string strCallService = "OBJECTION";
                    string strZuser = "";
                    string strBpnum = App.LoginDataRetrieved.TIN;
                    string strEuser2 = "";
                    string strEuser3 = "";
                    string strEuser4 = "";
                    string strEuser5 = "";
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    //https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_TP_DASHBOARD_SRV/HeaderSet(CallServ='OBJECTION',HostName='',Zuser='DALMUHISEN@GAZT.GOV.SA',
                    //Bpnum='3102236227',Auditor='',Lang='E',Euser1='',Euser2='00000000001008328414',Euser3='00000001000008328673',Euser4='00000010000008328298',
                    //Euser5='00001000000008328330',Fbguid='')?$expand=ListSet&$format=json

                    String url = Constants.GetZAKATObjectionListURL + "" +
                        "CallServ='" + strCallService + "'," +
                        "HostName='" + "'," +
                        "Zuser='" + strZuser + "'," +
                        "Bpnum='" + strBpnum + "'," +
                        "Auditor='" + "'," +
                        "Lang='" + lang + "'," +
                        "Euser1='" + "'," +
                        "Euser2='" + strEuser2 + "'," +
                        "Euser3='" + strEuser3 + "'," +
                        "Euser4='" + strEuser4 + "'," +
                        "Euser5='" + strEuser5 + "'," +
                     "Fbguid='" + "')?$expand=ListSet&$format=json";
                    var uri = new Uri(url);
                    HttpResponseMessage _ZAKATObjectionListResponse = await client.GetAsync(uri);
                    if (_ZAKATObjectionListResponse != null)
                    {
                        if (_ZAKATObjectionListResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _ZAKATObjectionListResponse.Headers;
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
                        String __ZAKATObjectionListData = _ZAKATObjectionListResponse.Content.ReadAsStringAsync().Result;
                        _ZAKATObjectionList = JsonConvert.DeserializeObject<ZakatObjectionListModel>(__ZAKATObjectionListData);
                        if (!string.IsNullOrEmpty(__ZAKATObjectionListData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__ZAKATObjectionListData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _ZAKATObjectionList;
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

        public static async Task<ZAKATObjectionDataModel> GAZTGetZakatObjectionData(string fbnum)
        {
            ZAKATObjectionDataModel _ZAKATObjectionData = new ZAKATObjectionDataModel();
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    //ZAKAT Objection Summary
                    //https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/Z_TP_NOTES_TP09_SRV/znotes_tp09Set(Auditorz='',Taxpayerz='3102236227',RegIdz='',Submitz='',Savez='',
                    //Fbnumz='40000161190',Langz='E',UserTin='')?$expand=znotesSet,AttDetSet,zobj_itemsSet&$format=json



                    String url = Constants.GetZakatObjectionDataURL + "Auditorz='" + "',Taxpayerz='" + App.LoginDataRetrieved.TIN + "',RegIdz='" + "',Submitz='" + "'," +
                        "Savez='" + "',Fbnumz='" + fbnum + "',Langz='" + lang + "',UserTin='" + "')?$expand=znotesSet,AttDetSet,zobj_itemsSet&$format=json";
                    var uri = new Uri(url);


                    HttpResponseMessage _ZAKATObjectionDataResponse = await client.GetAsync(uri);
                    if (_ZAKATObjectionDataResponse != null)
                    {
                        if (_ZAKATObjectionDataResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _ZAKATObjectionDataResponse.Headers;
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
                        String _zakatObjectionSummaryData = _ZAKATObjectionDataResponse.Content.ReadAsStringAsync().Result;
                        _ZAKATObjectionData = JsonConvert.DeserializeObject<ZAKATObjectionDataModel>(_zakatObjectionSummaryData);
                        if (!string.IsNullOrEmpty(_zakatObjectionSummaryData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatObjectionSummaryData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _ZAKATObjectionData;
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
        //API-2
        public async static Task<ZAKATObjectionCreateNewModel> GAZTGetZAKATObjectionCreateNew()
        {
            ZAKATObjectionCreateNewModel _ZAKATObjectionCreateNew = new ZAKATObjectionCreateNewModel();

            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    string strEuser1 = "00000000000000000000";
                    string strFbtyp = "ZNOB";
                    string strGpart = "3311647874";
                    string strLang = "EN";
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);


                    //https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_ITAP_SRV/TPFILLSet(Euser1='00000000000000000000',Fbguid='',Fbnum='',Fbtyp='ZNOB',
                    //Gpart='3102236227',Lang='EN',Persl='',Status='',Dispflag='')?$format=json

                    String url = Constants.GetZAKATObjectionCreateNewURL + "" +
                        "Euser1='" + strEuser1 + "'," +
                        "Fbguid='" + "'," +
                        "Fbnum='" + "'," +
                        "Fbtyp='" + strFbtyp + "'," +
                        "Gpart='" + strGpart + "'," +
                        "Lang='" + strLang + "'," +
                        "Persl='" + "'," +
                        "Status='" + "'," +
                     "Dispflag='" + "')?$format=json";
                    var uri = new Uri(url);
                    HttpResponseMessage _ZAKATObjectionCreateNewResponse = await client.GetAsync(uri);
                    if (_ZAKATObjectionCreateNewResponse != null)
                    {
                        if (_ZAKATObjectionCreateNewResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _ZAKATObjectionCreateNewResponse.Headers;
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
                        String __ZAKATObjectionCreateNewData = _ZAKATObjectionCreateNewResponse.Content.ReadAsStringAsync().Result;
                        _ZAKATObjectionCreateNew = JsonConvert.DeserializeObject<ZAKATObjectionCreateNewModel>(__ZAKATObjectionCreateNewData);
                        if (!string.IsNullOrEmpty(__ZAKATObjectionCreateNewData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__ZAKATObjectionCreateNewData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _ZAKATObjectionCreateNew;
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

        //API-5
        public async static Task<ZAKATObjectionDetailsByReferenceNumberModel> GAZTGetZAKATObjectionDetailsByReferenceNumber(string inputSearch)
        {
            ZAKATObjectionDetailsByReferenceNumberModel _ZAKATObjectionDetailsByReferenceNumber = new ZAKATObjectionDetailsByReferenceNumberModel();

            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    string strTaxPayer = App.LoginDataRetrieved.TIN;
                    string strEuser = "00000000000000000000";
                    string strFbnumz = inputSearch;
                    string strSectp = "C";
                    string strAud = "X";
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    //https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/Z_OBJ_ZNOB_AMT_SRV/ZNOB_AmtSet(Taxpayerz='3311633156',Fbnumz='26000004671',
                    //Euser='00000000000000000000',Aud='X',Sectp='C')


                    String url = Constants.GetZAKATObjectionDetailsByReferenceNumberURL + "" +
                        "Taxpayerz='" + strTaxPayer + "'," +
                        "Fbnumz='" + strFbnumz + "'," +
                        "Euser='" + strEuser + "'," +
                        "Aud='" + strAud + "'," +
                        "Sectp='" + strSectp + "')";
                    var uri = new Uri(url);
                    HttpResponseMessage _ZAKATObjectionDetailsByReferenceNumberResponse = await client.GetAsync(uri);
                    if (_ZAKATObjectionDetailsByReferenceNumberResponse != null)
                    {
                        if (_ZAKATObjectionDetailsByReferenceNumberResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _ZAKATObjectionDetailsByReferenceNumberResponse.Headers;
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
                        String __ZAKATObjectionDetailsByReferenceNumberData = _ZAKATObjectionDetailsByReferenceNumberResponse.Content.ReadAsStringAsync().Result;
                        _ZAKATObjectionDetailsByReferenceNumber = JsonConvert.DeserializeObject<ZAKATObjectionDetailsByReferenceNumberModel>(__ZAKATObjectionDetailsByReferenceNumberData);
                        if (!string.IsNullOrEmpty(__ZAKATObjectionDetailsByReferenceNumberData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__ZAKATObjectionDetailsByReferenceNumberData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _ZAKATObjectionDetailsByReferenceNumber;
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

        //API-6
        public async static Task<ZAKATObjectionDetailsToAmendReturnModel> GAZTGetZAKATObjectionDetailsToAmendReturn()
        {
            ZAKATObjectionDetailsToAmendReturnModel _ZAKATObjectionDetailsToAmendReturn = new ZAKATObjectionDetailsToAmendReturnModel();

            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    string strFbnum = "27000008586";
                    string strRefnum = "26000004637";
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    //https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/Z_ZNOBREF_SRV/Ref_NoSet(Fbnum='27000008586',Refnum='26000004637')


                    String url = Constants.GetZAKATObjectionDetailsToAmendReturnURL + "" +
                        "Fbnum='" + strFbnum + "'," +
                        "Refnum='" + strRefnum + "')";
                    var uri = new Uri(url);
                    HttpResponseMessage _ZAKATObjectionDetailsToAmendReturnResponse = await client.GetAsync(uri);
                    if (_ZAKATObjectionDetailsToAmendReturnResponse != null)
                    {
                        if (_ZAKATObjectionDetailsToAmendReturnResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _ZAKATObjectionDetailsToAmendReturnResponse.Headers;
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
                        String __ZAKATObjectionDetailsToAmendReturnData = _ZAKATObjectionDetailsToAmendReturnResponse.Content.ReadAsStringAsync().Result;
                        _ZAKATObjectionDetailsToAmendReturn = JsonConvert.DeserializeObject<ZAKATObjectionDetailsToAmendReturnModel>(__ZAKATObjectionDetailsToAmendReturnData);
                        if (!string.IsNullOrEmpty(__ZAKATObjectionDetailsToAmendReturnData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__ZAKATObjectionDetailsToAmendReturnData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _ZAKATObjectionDetailsToAmendReturn;
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

        //API-7
        public async static Task<ZAKATObjectionAmendReturnAndCloseModel> GAZTGetZAKATObjectionAmendReturnAndClose()
        {
            ZAKATObjectionAmendReturnAndCloseModel _ZAKATObjectionAmendReturnAndClose = new ZAKATObjectionAmendReturnAndCloseModel();

            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    string strFbnum = "26000004525";
                    string strSectp = "C";
                    string strBetrw = "65000.00d";
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    //https://goela:invenio@tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/Z_OBJ_ZNOB_REV_AMT_SRV/ZNOB_RevAmtSet(Fbnum='26000004525',Sectp='C',Betrw=65000.00d)


                    String url = Constants.GetZAKATObjectionAmendReturnAndCloseURL + "" +
                        "Fbnum='" + strFbnum + "'," +
                        "Sectp='" + strSectp + "'," +
                        "Betrw='" + strBetrw + "')";
                    var uri = new Uri(url);
                    HttpResponseMessage _ZAKATObjectionAmendReturnAndCloseResponse = await client.GetAsync(uri);
                    if (_ZAKATObjectionAmendReturnAndCloseResponse != null)
                    {
                        if (_ZAKATObjectionAmendReturnAndCloseResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _ZAKATObjectionAmendReturnAndCloseResponse.Headers;
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
                        String __ZAKATObjectionAmendReturnAndCloseData = _ZAKATObjectionAmendReturnAndCloseResponse.Content.ReadAsStringAsync().Result;
                        _ZAKATObjectionAmendReturnAndClose = JsonConvert.DeserializeObject<ZAKATObjectionAmendReturnAndCloseModel>(__ZAKATObjectionAmendReturnAndCloseData);
                        if (!string.IsNullOrEmpty(__ZAKATObjectionAmendReturnAndCloseData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__ZAKATObjectionAmendReturnAndCloseData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _ZAKATObjectionAmendReturnAndClose;
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

        //API-8
        public async static Task<ZAKATObjectionOnPaymentMethodSelectionModel> GAZTGetZAKATObjectionOnPaymentMethodSelection()
        {
            ZAKATObjectionOnPaymentMethodSelectionModel _ZAKATObjectionOnPaymentMethodSelection = new ZAKATObjectionOnPaymentMethodSelectionModel();

            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    string strFbnum = "26000004533";
                    string strSectp = "C";
                    string strRevam = "20000.00d";
                    string strDisam = "30000.00d";
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    //https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/Z_OBJ_ZNOB_REV_AMT_SRV/secamtSet(Revam=20000.00d,Fbnum='26000004533',Disam=30000.00d,Sectp='C')

                    String url = Constants.GetZAKATObjectionOnPaymentMethodSelectionURL + "" +
                        "Revam='" + strRevam + "'," +
                        "Fbnum='" + strFbnum + "'," +
                        "Disam='" + strDisam + "'," +
                        "Sectp='" + strSectp + "')";
                    var uri = new Uri(url);
                    HttpResponseMessage _ZAKATObjectionOnPaymentMethodSelectionResponse = await client.GetAsync(uri);
                    if (_ZAKATObjectionOnPaymentMethodSelectionResponse != null)
                    {
                        if (_ZAKATObjectionOnPaymentMethodSelectionResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _ZAKATObjectionOnPaymentMethodSelectionResponse.Headers;
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
                        String __ZAKATObjectionOnPaymentMethodSelectionData = _ZAKATObjectionOnPaymentMethodSelectionResponse.Content.ReadAsStringAsync().Result;
                        _ZAKATObjectionOnPaymentMethodSelection = JsonConvert.DeserializeObject<ZAKATObjectionOnPaymentMethodSelectionModel>(__ZAKATObjectionOnPaymentMethodSelectionData);
                        if (!string.IsNullOrEmpty(__ZAKATObjectionOnPaymentMethodSelectionData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__ZAKATObjectionOnPaymentMethodSelectionData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _ZAKATObjectionOnPaymentMethodSelection;
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

        //API-9
        public async static Task<ZAKATObjectionApplicationDetailsIfStatusIP017Model> GAZTGetZAKATObjectionApplicationDetailsIfStatusIP017()
        {
            ZAKATObjectionApplicationDetailsIfStatusIP017Model _ZAKATObjectionApplicationDetailsIfStatusIP017 = new ZAKATObjectionApplicationDetailsIfStatusIP017Model();

            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    string strFbnum = "26000004533";
                    string strEuser = "00000000000000000000";
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    //https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/Z_ZNOB_FB_DETAILS_SRV/ZFBSet(Fbnum='26000004533',Euser='00000000000000000000')

                    String url = Constants.GetZAKATObjectionApplicationDetailsIfStatusIP017URL + "" +
                        "Fbnum='" + strFbnum + "'," +
                        "Euser='" + strEuser + "')";
                    var uri = new Uri(url);
                    HttpResponseMessage _ZAKATObjectionApplicationDetailsIfStatusIP017Response = await client.GetAsync(uri);
                    if (_ZAKATObjectionApplicationDetailsIfStatusIP017Response != null)
                    {
                        if (_ZAKATObjectionApplicationDetailsIfStatusIP017Response.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _ZAKATObjectionApplicationDetailsIfStatusIP017Response.Headers;
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
                        String __ZAKATObjectionApplicationDetailsIfStatusIP017Data = _ZAKATObjectionApplicationDetailsIfStatusIP017Response.Content.ReadAsStringAsync().Result;
                        _ZAKATObjectionApplicationDetailsIfStatusIP017 = JsonConvert.DeserializeObject<ZAKATObjectionApplicationDetailsIfStatusIP017Model>(__ZAKATObjectionApplicationDetailsIfStatusIP017Data);
                        if (!string.IsNullOrEmpty(__ZAKATObjectionApplicationDetailsIfStatusIP017Data))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__ZAKATObjectionApplicationDetailsIfStatusIP017Data);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _ZAKATObjectionApplicationDetailsIfStatusIP017;
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

        //API-10
        public async static Task<ZAKATObjectionGenerateSADADNumberModel> GAZTGetZAKATObjectionGenerateSADADNumber()
        {
            ZAKATObjectionGenerateSADADNumberModel _ZAKATObjectionGenerateSADADNumber = new ZAKATObjectionGenerateSADADNumberModel();

            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    string strFbnum = "26000004533";
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    //https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/Z_OBJ_ZNOB_REV_AMT_SRV/sadadnumberSet(Fbnum='26000004533')

                    String url = Constants.GetZAKATObjectionGenerateSADADNumberURL + "" +
                        "Fbnum='" + strFbnum + "')";
                    var uri = new Uri(url);
                    HttpResponseMessage _ZAKATObjectionGenerateSADADNumberResponse = await client.GetAsync(uri);
                    if (_ZAKATObjectionGenerateSADADNumberResponse != null)
                    {
                        if (_ZAKATObjectionGenerateSADADNumberResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _ZAKATObjectionGenerateSADADNumberResponse.Headers;
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
                        String __ZAKATObjectionGenerateSADADNumberData = _ZAKATObjectionGenerateSADADNumberResponse.Content.ReadAsStringAsync().Result;
                        _ZAKATObjectionGenerateSADADNumber = JsonConvert.DeserializeObject<ZAKATObjectionGenerateSADADNumberModel>(__ZAKATObjectionGenerateSADADNumberData);
                        if (!string.IsNullOrEmpty(__ZAKATObjectionGenerateSADADNumberData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__ZAKATObjectionGenerateSADADNumberData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _ZAKATObjectionGenerateSADADNumber;
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

        //API-12
        public async static Task<ZAKATObjectionBusyIndicatorModel> GAZTGetZAKATObjectionBusyIndicator()
        {
            ZAKATObjectionBusyIndicatorModel _ZAKATObjectionBusyIndicator = new ZAKATObjectionBusyIndicatorModel();

            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    string strPartner = "3311626033";
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    //https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/Z_BUSY_INDICATOR_SRV/ZBUSYINDSet(Partner='3311626033')

                    String url = Constants.GetZAKATObjectionBusyIndicatorURL + "" +
                        "Partner='" + strPartner + "')";
                    var uri = new Uri(url);
                    HttpResponseMessage _ZAKATObjectionBusyIndicatorResponse = await client.GetAsync(uri);
                    if (_ZAKATObjectionBusyIndicatorResponse != null)
                    {
                        if (_ZAKATObjectionBusyIndicatorResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _ZAKATObjectionBusyIndicatorResponse.Headers;
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
                        String __ZAKATObjectionBusyIndicatorData = _ZAKATObjectionBusyIndicatorResponse.Content.ReadAsStringAsync().Result;
                        _ZAKATObjectionBusyIndicator = JsonConvert.DeserializeObject<ZAKATObjectionBusyIndicatorModel>(__ZAKATObjectionBusyIndicatorData);
                        if (!string.IsNullOrEmpty(__ZAKATObjectionBusyIndicatorData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__ZAKATObjectionBusyIndicatorData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _ZAKATObjectionBusyIndicator;
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

        public static async Task<ZakatBankListModel> GAZTGetBankList()
        {
            ZakatBankListModel zakatBankList = new ZakatBankListModel();
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    String url = Constants.ZakatObjectionLoadBankListURL;
                    var uri = new Uri(url);


                    HttpResponseMessage _zakatBankListResponse = await client.GetAsync(uri);
                    if (_zakatBankListResponse != null)
                    {
                        if (_zakatBankListResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _zakatBankListResponse.Headers;
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
                        String __zakatBankListData = _zakatBankListResponse.Content.ReadAsStringAsync().Result;
                        zakatBankList = JsonConvert.DeserializeObject<ZakatBankListModel>(__zakatBankListData);
                        if (!string.IsNullOrEmpty(__zakatBankListData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__zakatBankListData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return zakatBankList;
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

        public static async Task<ZakatBankListModel> GAZTGetIntialLoadData(string fbguid)
        {
            ZakatBankListModel zakatBankList = new ZakatBankListModel();
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);


                    // String url = Constants.GetVATObjectionViewBillURL + "Opbel eq'" + opbel + "' and Vtre2 eq'" + vtre2 + "')?$format=json";

                    //sap/opu/odata/SAP/Z_OBJ_ZNOB_SRV/ZNOB_HeaderSet(Taxpayerz='',
                    //Fbnumz = '',Langz = '',Auditorz = '',Euser = '00000000000000000000',
                    //Fbguid = '005056B1365C1EEAB5931176E56092C6')?&$expand = ZNOB_ObjSet,Off_notesSet,AttDetSet

                    String url = Constants.ZakatObjectionIntialLoadURL + "Taxpayerz='" + "',Fbnumz='" + "',Langz='" + "'," +
"Auditorz='" + "',Euser='" + 00000000000000000000 + "',Fbguid='" + fbguid + "')?&$expand=ZNOB_ObjSet,Off_notesSet,AttDetSet&$format=json";
                    var uri = new Uri(url);


                    HttpResponseMessage _zakatBankListResponse = await client.GetAsync(uri);
                    if (_zakatBankListResponse != null)
                    {
                        if (_zakatBankListResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _zakatBankListResponse.Headers;
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
                        String __zakatBankListData = _zakatBankListResponse.Content.ReadAsStringAsync().Result;
                        zakatBankList = JsonConvert.DeserializeObject<ZakatBankListModel>(__zakatBankListData);
                        if (!string.IsNullOrEmpty(__zakatBankListData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__zakatBankListData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return zakatBankList;
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

        public static async Task<ZakatBankListModel> GAZTGetZakatRemoveObjection(string retFbnum, string objFbnum)
        {
            ZakatBankListModel zakatBankList = new ZakatBankListModel();
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);


                    //https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/Z_RMV_OBJ_SRV/HeaderSet(RetFbnum='93000003544',ObjFbnum='27000008600')



                    String url = Constants.ZakatObjectionRemoveobjectionURL + "RetFbnum='" + retFbnum + "',ObjFbnum='" + objFbnum + "')?$format=json";
                    var uri = new Uri(url);


                    HttpResponseMessage _zakatBankListResponse = await client.GetAsync(uri);
                    if (_zakatBankListResponse != null)
                    {
                        if (_zakatBankListResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _zakatBankListResponse.Headers;
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
                        String __zakatBankListData = _zakatBankListResponse.Content.ReadAsStringAsync().Result;
                        zakatBankList = JsonConvert.DeserializeObject<ZakatBankListModel>(__zakatBankListData);
                        if (!string.IsNullOrEmpty(__zakatBankListData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__zakatBankListData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return zakatBankList;
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

        public static async Task<ZakatBankListModel> GAZTGetZakatRemoveObjectionACK(string retFbnum, string objFbnum)
        {
            ZakatBankListModel zakatBankList = new ZakatBankListModel();
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);


                    //https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/Z_RMV_OBJ_SRV/HeaderSet(RetFbnum='93000003544',ObjFbnum='27000008600')



                    String url = Constants.ZakatObjectionRemoveObjAckURL + "RetFbnum='" + retFbnum + "',ObjFbnum='" + objFbnum + "')?$format=json";
                    var uri = new Uri(url);


                    HttpResponseMessage _zakatBankListResponse = await client.GetAsync(uri);
                    if (_zakatBankListResponse != null)
                    {
                        if (_zakatBankListResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _zakatBankListResponse.Headers;
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
                        String __zakatBankListData = _zakatBankListResponse.Content.ReadAsStringAsync().Result;
                        zakatBankList = JsonConvert.DeserializeObject<ZakatBankListModel>(__zakatBankListData);
                        if (!string.IsNullOrEmpty(__zakatBankListData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__zakatBankListData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return zakatBankList;
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

        #region Zakat Withdraw Objection
        public static async Task<ZakatWithdrawMainDataModel> GAZTGetZakatWithDrawMainData()
        {
            ZakatWithdrawMainDataModel _zakatWithdrawMainDataModel = new ZakatWithdrawMainDataModel();
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);


                    ///sap/opu/odata/SAP/Z_TP_NOTES_TP09_SRV/znotes_tp09Set
                    /////(Auditorz='',Taxpayerz='3102435657',RegIdz='',Submitz='',Savez='',Fbnumz='',Langz='E',UserTin='')?$expand=znotesSet,AttDetSet,zobj_itemsSet



                    String url = Constants.ZakatObjectionWDMaindataRL + "Auditorz='" + "',Taxpayerz='" + App.LoginDataRetrieved.TIN + "',RegIdz='" + "'," +
                        "Submitz='" + "',Fbnumz='" + "',Langz='" + lang + "',UserTin='" + "')?$expand=znotesSet,AttDetSet,zobj_itemsSet&$format=json";
                    var uri = new Uri(url);


                    HttpResponseMessage _zakatWithdrawMainDataResponse = await client.GetAsync(uri);
                    if (_zakatWithdrawMainDataResponse != null)
                    {
                        if (_zakatWithdrawMainDataResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _zakatWithdrawMainDataResponse.Headers;
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
                        String _zakatWithdrawMainData = _zakatWithdrawMainDataResponse.Content.ReadAsStringAsync().Result;
                        _zakatWithdrawMainDataModel = JsonConvert.DeserializeObject<ZakatWithdrawMainDataModel>(_zakatWithdrawMainData);
                        if (!string.IsNullOrEmpty(_zakatWithdrawMainData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatWithdrawMainData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _zakatWithdrawMainDataModel;
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

        public static async Task<ZakatObjectionWithDrawListModel> GAZTGetZakatWithDrawList()
        {
            ZakatObjectionWithDrawListModel _zakatObjectionWithDrawListModel = new ZakatObjectionWithDrawListModel();
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);


                    //   / sap / opu / odata / SAP / Z_TP_NOTES_TP09_SRV / zvaluesSet ?$filter = Taxpy % 20eq % 20 % 20 % 273102435657 % 27



                    String url = Constants.ZakatObjectionWDListRL + "Taxpy eq '" + App.LoginDataRetrieved.TIN + "'&$format=json";
                    var uri = new Uri(url);


                    HttpResponseMessage __zakatObjectionWithDrawListResponse = await client.GetAsync(uri);
                    if (__zakatObjectionWithDrawListResponse != null)
                    {
                        if (__zakatObjectionWithDrawListResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = __zakatObjectionWithDrawListResponse.Headers;
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
                        String ___zakatObjectionWithDrawListData = __zakatObjectionWithDrawListResponse.Content.ReadAsStringAsync().Result;
                        _zakatObjectionWithDrawListModel = JsonConvert.DeserializeObject<ZakatObjectionWithDrawListModel>(___zakatObjectionWithDrawListData);
                        if (!string.IsNullOrEmpty(___zakatObjectionWithDrawListData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(___zakatObjectionWithDrawListData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _zakatObjectionWithDrawListModel;
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
        //ZakatObjectionWDDropdownModel

        public static async Task<ZakatObjectionWDDropdownModel> GAZTGetZakatWithDrawDDData(string objFbnum)
        {
            ZakatObjectionWDDropdownModel _zakatObjectionWDDropdownModel = new ZakatObjectionWDDropdownModel();
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);


                    // /sap/opu/odata/SAP/Z_TP_NOTES_TP09_SRV/object_itmsSet?$filter=ObjFbnum%20eq%20%20%2735001347883%27



                    String url = Constants.ZakatObjectionWDSelectedDDURL + "ObjFbnum eq '" + objFbnum + "'&$format=json";
                    var uri = new Uri(url);


                    HttpResponseMessage __zakatObjectionWDDropdownResponse = await client.GetAsync(uri);
                    if (__zakatObjectionWDDropdownResponse != null)
                    {
                        if (__zakatObjectionWDDropdownResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = __zakatObjectionWDDropdownResponse.Headers;
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
                        String __zakatObjectionWDDropdownData = __zakatObjectionWDDropdownResponse.Content.ReadAsStringAsync().Result;
                        _zakatObjectionWDDropdownModel = JsonConvert.DeserializeObject<ZakatObjectionWDDropdownModel>(__zakatObjectionWDDropdownData);
                        if (!string.IsNullOrEmpty(__zakatObjectionWDDropdownData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__zakatObjectionWDDropdownData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _zakatObjectionWDDropdownModel;
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

        public static async Task<AttachmentRootOject> GAZTSaveZakatObjectionWDAttachment(byte[] AttachmentByte, string fileName, string RetGuid, string Dotyp, string contentType)//, string returnedFguid
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    AttachmentRootOject _attachment = new AttachmentRootOject();
                    char LangZ = GetLangZParameter();
                    string AttBy = "TP";
                    //  / sap / opu / odata / SAP / Z_SAVE_ATTACH_SRV / AttachSet
                    //(RetGuid = '005056B1F8FB1EEABC81342B71834259', Flag = 'N', Dotyp = 'N03A', SchGuid = '', Srno = 1, Doguid = '', AttBy = 'TP', OutletRef = '') / AttachMedSet

                    String url = Constants.ZakatObjectionWDAttachmentURL + "RetGuid='" + RetGuid + "'" + ",Flag='" + "N" + "'" + ",Dotyp='" + Dotyp + "'" + ",SchGuid='" + "'" + ",Srno=" + "1" + ",Doguid='" + "'" + ",AttBy='" + AttBy + "'" + ",OutletRef='" + "'" + ")/AttachMedSet";
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

        public static string GAZTZakatObjectonWDDownloadacknowledgement(string fbnum)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {

                    /// /sap/opu/odata/SAP/Z_GET_ACK_LETTER_SRV/Ack_letterSet(Fbnum='40000161183')/$value
                    String Url = string.Empty;
                    Url = Constants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_ACK_LETTER_SRV/Ack_letterSet(Fbnum='" + fbnum + "')/$value";

                    return Url;
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

        public static string GAZTZakatObjectionWDDownloadForm(string fbnum)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {

                    // /sap/opu/odata/SAP/Z_GET_COVER_FORM_SRV/cover_formSet(Fbnum='40000161183')/$value
                    String Url = string.Empty;
                    Url = Constants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_COVER_FORM_SRV/cover_formSet(Fbnum='" + fbnum + "')/$value";

                    return Url;
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


        public static async Task<ZakatObjectionRequestSummaryModel> GAZTGetZakatRequestObjectionSummary(string fbnum)
        {
            ZakatObjectionRequestSummaryModel _zakatObjectionRequestSummaryModel = new ZakatObjectionRequestSummaryModel();
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);


                    // https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/Z_OBJ_ZNOB_SRV/ZNOB_HeaderSet(Taxpayerz='',Fbnumz='35001348258',Langz='',Auditorz='',
                    //   Euser = '00000000000000000000',Fbguid = '')?= &$expand = ZNOB_ObjSet,Off_notesSet,AttDetSet &$format = json



                    String url = Constants.ZakatObjectionRequestSummaryURL + "Taxpayerz='" + "',Fbnumz='" + fbnum + "',Langz='" + "',Auditorz='" + "'," +
                        "Euser='00000000000000000000',Fbguid='" + "')?=&$expand=ZNOB_ObjSet,Off_notesSet,AttDetSet&$format=json";
                    var uri = new Uri(url);


                    HttpResponseMessage _zakatObjectionRequestSummaryResponse = await client.GetAsync(uri);
                    if (_zakatObjectionRequestSummaryResponse != null)
                    {
                        if (_zakatObjectionRequestSummaryResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _zakatObjectionRequestSummaryResponse.Headers;
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
                        String _zakatObjectionSummaryData = _zakatObjectionRequestSummaryResponse.Content.ReadAsStringAsync().Result;
                        _zakatObjectionRequestSummaryModel = JsonConvert.DeserializeObject<ZakatObjectionRequestSummaryModel>(_zakatObjectionSummaryData);
                        if (!string.IsNullOrEmpty(_zakatObjectionSummaryData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatObjectionSummaryData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _zakatObjectionRequestSummaryModel;
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

        public static async Task<ZakatObjectionWithdrawPostResponceModel> GAZTSaveZakatObjectionWithDrawData(ZakatObjectionWithdrawPostModel.Root postData)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    ZakatObjectionWithdrawPostResponceModel responseData = new ZakatObjectionWithdrawPostResponceModel();
                   

                   


                    string LangZ = GetLangZParameterAREN();
                    String url = Constants.ZakatObjectionWDPostURL;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var serilized = JsonConvert.SerializeObject(postData);

                    client.DefaultRequestHeaders.Add("Token", App.Token);
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    HttpResponseMessage res = client.PostAsync(uri, contentPost).Result;
                    var _ZakatWithDrawresult = res.Content.ReadAsStringAsync().Result;
                    responseData = JsonConvert.DeserializeObject<ZakatObjectionWithdrawPostResponceModel>(_ZakatWithDrawresult);
                    if (_ZakatWithDrawresult == null || responseData.d == null)
                    {
                        ErrorMessage = string.Empty;
                        ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_ZakatWithDrawresult);
                        if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                        {
                            ErrorMessage = errorMesg.error.innererror.errordetails[0].message;
                        }
                    }
                    return responseData;
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

        public static async Task<ZakatObjectionSummaryModel> GAZTGetZakatObjectionSummary(string fbnum)
        {
            ZakatObjectionSummaryModel _zakatObjectionSummaryModel = new ZakatObjectionSummaryModel();
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    //ZAKAT Objection Summary
                    //https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/Z_TP_NOTES_TP09_SRV/znotes_tp09Set(Auditorz='',Taxpayerz='3102236227',RegIdz='',Submitz='',Savez='',
                    //Fbnumz='40000161190',Langz='E',UserTin='')?$expand=znotesSet,AttDetSet,zobj_itemsSet&$format=json



                    String url = Constants.ZakatObjectionSummaryURL + "Auditorz='" + "',Taxpayerz='" + App.LoginDataRetrieved.TIN + "',RegIdz='" + "',Submitz='" + "'," +
                        "Savez='" + "',Fbnumz='" + fbnum + "',Langz='" + lang + "',UserTin='" + "')?$expand=znotesSet,AttDetSet,zobj_itemsSet&$format=json";
                    var uri = new Uri(url);


                    HttpResponseMessage _zakatObjectionSummaryResponse = await client.GetAsync(uri);
                    if (_zakatObjectionSummaryResponse != null)
                    {
                        if (_zakatObjectionSummaryResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _zakatObjectionSummaryResponse.Headers;
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
                        String _zakatObjectionSummaryData = _zakatObjectionSummaryResponse.Content.ReadAsStringAsync().Result;
                        _zakatObjectionSummaryModel = JsonConvert.DeserializeObject<ZakatObjectionSummaryModel>(_zakatObjectionSummaryData);
                        if (!string.IsNullOrEmpty(_zakatObjectionSummaryData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatObjectionSummaryData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return _zakatObjectionSummaryModel;
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

        #region Download File

        public async static System.Threading.Tasks.Task<bool> FileDownload(string url, string fileExtension)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    string NewToken = string.Empty;
                    Char lang = WebServiceManager.GetLangZParameter();
                    //  HttpClient client = new HttpClient(App.httpClientHandler);
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
                catch (Exception ex)
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

        // * TP PROFILE WEB API MANAGER
        public static Task<TaxPayerProfile> GetTPProfileDataAPICall(string TIN)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                string lang = string.Empty;
                if (App.IsArabic) { lang = "A"; }
                else { lang = "E"; }

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
                if (App.IsArabic) { lang = "A"; }
                else { lang = "E"; }

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
                            + ",Euser5=" + "''" + ")?$format=json";

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

                        string GAZTTPProfileResponseJSON = UpdatePWDResponse.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(GAZTTPProfileResponseJSON))
                        {
                            GAZTTPProfileResponseJSON = JObject.Parse(GAZTTPProfileResponseJSON)["d"].ToString();
                            TPProfileData = JsonConvert.DeserializeObject<TaxPayerProfile>(GAZTTPProfileResponseJSON);
                        }
                    }
                }
                else
                    throw new Exception(AppResources.NetworkConnectivityIssue);

                //return tPProfileData;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("API RESPONSE ERROR : {0}", ex.Message);
                throw new Exception(AppResources.InvalidPassword);
            }

            return TPProfileData;
        }

        public static async Task<TaxPayerProfile> POSTTPProfileAPICalls(TPProfileAPIRequest TPProfileAPIRequestPOSTData, string APIType)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                TaxPayerProfile TP = null;

                try
                {
                    string url = Constants.TPProfileURL;
                    var uri = new Uri(url);

                    try { App.httpClientHandler.CookieContainer = null; }
                    catch (Exception ex) { }

                    HttpClient client = new HttpClient(App.httpClientHandler);

                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    var serilized = JsonConvert.SerializeObject(TPProfileAPIRequestPOSTData);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);

                    string GAZTTPProfileResponseJSON = res.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(GAZTTPProfileResponseJSON))
                    {
                        GAZTTPProfileResponseJSON = JObject.Parse(GAZTTPProfileResponseJSON)["d"].ToString();
                        TP = JsonConvert.DeserializeObject<TaxPayerProfile>(GAZTTPProfileResponseJSON);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("API RESPONSE ERROR : {0}", ex.Message);
                    if (APIType.Equals("GETOTPMOBILE"))
                        throw new Exception(AppResources.EnterValidMobileNumber);
                    else if(APIType.Equals("VERIFYOTPMOBILE"))
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
    }
}
