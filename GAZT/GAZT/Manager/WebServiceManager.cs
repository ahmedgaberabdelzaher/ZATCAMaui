using GAZT.Helper;
using GAZT.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Xamarin.Forms;

namespace GAZT.Manager
{
    public static class WebServiceManager
    {
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

                                }
                                else
                                    throw new Exception(AppResources.NetworkConnectivityIssue);
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
                            throw new Exception("خطأ في كلمة سر");
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
                    String url = Constants.GAZTSendAndReceiveOTP + Lang + "',Userid='" + UserId + "',Otp='" + "',CurrAttmps=" + currentAttempts + ")?&saml2=disabled&sap-" + "language='" + _language + "" + "'" + "&$format=json"; //)?&saml2=disabled&$format=json";
                                                                                                                                                                                                                                    //  https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/ZDP_USRLOGIN_OTP_SRV/HEADERSet(Langz='E',Userid='',Otp='34455',CurrAttmps=1)?&saml2=disabled&sap-language='EN'&$format=xml
                    var uri = new Uri(url);
                    client.DefaultRequestHeaders.Add("Token", App.Token);
                    HttpResponseMessage GAZTSendAndReceiveOTPResponse = await client.GetAsync(uri);
                    if (GAZTSendAndReceiveOTPResponse != null)
                    {
                        HttpHeaders headers = GAZTSendAndReceiveOTPResponse.Headers;
                        IEnumerable<string> values;
                        //if (headers.TryGetValues("token", out values))
                        //{
                        //    NewToken = values.First();
                        //}

                        //if ((!string.IsNullOrEmpty(NewToken)))
                        //{
                        //    if ((0 == String.Compare(NewToken, "Token has expaired"))|| (0 == String.Compare(NewToken, "Invalid Token")))
                        //    {
                        //        App.IsSessionExpired = true;
                        //        return null;
                        //    }
                        //    App.Token = NewToken;
                        //}
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
                    String url = Constants.GAZTSendAndReceiveOTP + lang + "',Userid='" + UserId + "',Otp='" + OTP + "',CurrAttmps=" + currentAttempts + ")?&saml2=disabled&sap-" + "language='" + Lang + "" + "'" + "&$format=json"; //)?&saml2=disabled&$format=json";Constants.GAZTValidateOTP + Lang + "',Userid='" + UserId + "',Otp='" + OTP + "')?&saml2=disabled&$format=json";
                    var uri = new Uri(url);
                    client.DefaultRequestHeaders.Add("Token", App.Token);
                    HttpResponseMessage GAZTValidateOTPResponse = await client.GetAsync(uri);
                    if (GAZTValidateOTPResponse != null)
                    {
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
                                //   string result = GAZTValidateOTPResponseJToken.Value.ToString();

                                //   if ((0 == String.Compare(GAZTValidateOTPResponseJToken.Value<String>(), "Valid OTP")) || (0 == String.Compare(GAZTValidateOTPResponseJToken.Value<String>(), "كلمة مرور صالحة لمرة واحدة")))
                                TP = JsonConvert.DeserializeObject<TaxPayerProfile>(GAZTValidateOTPResponseJSON);
                                //else
                                //    throw new Exception(AppResources.InvalidOTP);
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
        public static async Task<TaxPayerProfile> GAZTValidateOTPForMobileNumber(String Lang, String OTP, String Tin, string CurrentMobileNumber, string NewMobileNumber)
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
                    CurrentMobileNumber = "00" + CurrentMobileNumber;
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.GAZTValidateOTPForMobile + "Langz='" + Lang + "',Tin='" + Tin + "',Otp='" + OTP + "',CurrEmail='" + "" + "',NewEmail='" + "" + "',CurrMobile='" + CurrentMobileNumber + "',NewMobile='" + NewMobileNumber + "',CurrPwd='" + "" + "',NewPwd='" + "')?$format=json&saml2=disabled&sap-language=" + Lang;
                    var uri = new Uri(url);
                    client.DefaultRequestHeaders.Add("Token", App.Token);
                    HttpResponseMessage GAZTValidateOTPResponse = await client.GetAsync(uri);
                    if (GAZTValidateOTPResponse != null)
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
        public static async Task<bool> GAZTValidateMobileNumber(String Lang, String Tin, string CurrentMobileNumber, string NewMobileNumber)
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
                    CurrentMobileNumber = "00" + CurrentMobileNumber;
                    String url = Constants.GaZTVerifyMobileNumber + "Langz='" + Lang + "',Tin='" + Tin + "',Otp='" + "" + "',CurrEmail='" + "" + "',NewEmail='" + "" + "',CurrMobile='" + CurrentMobileNumber + "',NewMobile='" + NewMobileNumber + "',CurrPwd='" + "" + "',NewPwd='" + "')?$format=json&saml2=disabled&sap-language=" + Lang;
                    var uri = new Uri(url);
                    client.DefaultRequestHeaders.Add("Token", App.Token);
                    HttpResponseMessage GAZTValidateMobileNumberResponse = await client.GetAsync(uri);
                    if (GAZTValidateMobileNumberResponse != null)
                    {
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
                    String url = Constants.GAZTValidateAndChangePassword + "Langz='" + Lang + "',Tin='" + Tin + "',Otp='" + "" + "',CurrEmail='" + "" + "',NewEmail='" + "" + "',CurrMobile='" + "" + "',NewMobile='" + "" + "',CurrPwd='" + CurrentPassword + "',NewPwd='" + NewPassword + "')?$format=json&saml2=disabled&sap-language=" + Lang;
                    var uri = new Uri(url);
                    client.DefaultRequestHeaders.Add("Token", App.Token);
                    HttpResponseMessage GAZTValidateAndChangePasswordResponse = await client.GetAsync(uri);
                    if (GAZTValidateAndChangePasswordResponse != null)
                    {
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
                            else
                            {
                                throw new ArgumentException(AppResources.PasswordGuidelineText);
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
                        throw new Exception(AppResources.PasswordGuidelineText);
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
                    String url = Constants.GAZTGetPdf + "Gpartz eq'" + Tin + "'and Langz eq'" + Lang + "'and  Begdaz eq datetime'" + "2007-01-01T00:00" + "'and Enddaz eq datetime'" + currentDate + "'and  ObligFlagz eq'" + "I" + "'and  Auditor  eq'" + "" + "'and  TaxtpFg  eq '" + "VAT" + "'and  UserTin   eq'" + "" + "'&saml2=disabled&$format=json";
                    var uri = new Uri(url);
                    client.DefaultRequestHeaders.Add("Token", App.Token);
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
        public static List<MyBills> GAZTGetMyBills(String Tin, string lang)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                List<MyBills> myBills = new List<MyBills>();
                String MobileNumber = string.Empty;
                string PdfUrl = string.Empty;
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.GetMyBills + "Fbguid eq '" + "'and Euser eq '" + Tin + "'" + "&saml2=disabled&$format=json&sap-language=" + lang;
                    client.DefaultRequestHeaders.Add("Token", App.Token);
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTMyBillsResponse = client.GetAsync(uri).Result;
                    if (GAZTMyBillsResponse != null)
                    {
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
                                myBills = JsonConvert.DeserializeObject<List<MyBills>>(GAZTMyBillsResponseJSONJToken);
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

                    String url = Constants.GetMyICRs + lang + "',Gpart='',Euser='" + Tin + "',Fbguid='" + "',UserTin='" + "'" + ")?&saml2=disabled" + "&$expand=ICR_LISTSet,ICR_STATUSSet&$format=json";
                    client.DefaultRequestHeaders.Add("Token", App.Token);
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTMyICRsResponse = client.GetAsync(uri).Result;
                    if (GAZTMyICRsResponse != null)
                    {
                        HttpHeaders headers = GAZTMyICRsResponse.Headers;

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
                    String url = Constants.GAZTGetTP + "='" + Tin + "',Langz='" + Lang + "')" + "?&$expand=TPOC_LIST&saml2=disabled&$format=json";
                    client.DefaultRequestHeaders.Add("Token", App.Token);
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTValidateAndChangePasswordResponse = await client.GetAsync(uri);

                    if (GAZTValidateAndChangePasswordResponse != null)
                    {

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
                    String url = Constants.GAZTGetOTPForEmail + "Langz='" + Lang + "',Tin='" + Tin + "',Otp='" + "" + "',CurrEmail='" + CurrentEmail + "',NewEmail='" + NewEmail + "',CurrMobile='" + "" + "',NewMobile='" + "" + "',CurrPwd='" + "" + "',NewPwd='" + "')?$format=json&saml2=disabled&sap-language=" + Lang;
                    var uri = new Uri(url);
                    client.DefaultRequestHeaders.Add("Token", App.Token);
                    HttpResponseMessage GAZTValidateOTPResponse = await client.GetAsync(uri);
                    if (GAZTValidateOTPResponse != null)
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
                                throw new Exception(AppResources.InvalidEmail);
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
                    client.DefaultRequestHeaders.Add("Token", App.Token);
                    String url = Constants.GAZTValidateOTPForEmail + "Langz='" + Lang + "',Tin='" + Tin + "',Otp='" + OTP + "',CurrEmail='" + CurrentEmail + "',NewEmail='" + NewEmail + "',CurrMobile='" + "" + "',NewMobile='" + "" + "',CurrPwd='" + CurrentPassword + "',NewPwd='" + NewPassword + "')?$format=json&saml2=disabled&sap-language=" + Lang;
                    var uri = new Uri(url);

                    HttpResponseMessage GAZTValidateOTPResponse = await client.GetAsync(uri);
                    if (GAZTValidateOTPResponse != null)
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
                        else
                        {

                        }

                    }

                    return TP;

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
                    string ZakatURL = Constants.GAZTZakatGetPdf + Tin + "'and Langz eq'" + Lang + "'and UserTin eq'" + "" + "'and Begdaz eq datetime'" + "2007-01-01T00:00" + "'and Enddaz eq datetime'" + currentDate + "'and ObligFlagz eq'" + "" + "'and Auditor eq '" + "" + "'&sap-client=100&sap-language='" + Lang + "'&saml2=disabled&$format=json";

                    var uri = new Uri(ZakatURL);
                    client.DefaultRequestHeaders.Add("Token", App.Token);
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
                    string uri = Constants.GetAllCertificate + Tin + "'" + ",Langz='" + Lang + "'" + ",Begdaz=datetime'" + "2007-01-01T00%3A00%3A00'" + ",Enddaz=datetime'" + currentDate + "'" + ")?&$expand=ZakatSet,VATSet,ExciseSet&saml2=disabled&$format=json";

                    client.DefaultRequestHeaders.Add("Token", App.Token);
                    HttpResponseMessage GAZTGetAllCertificateResponse = client.GetAsync(uri).Result;

                    if (GAZTGetAllCertificateResponse != null)
                    {
                        HttpHeaders headers = GAZTGetAllCertificateResponse.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                        }

                        //if ((0 == String.Compare(NewToken, "Invalid Token")))
                        //{
                        //    App.IsSessionExpired = true;
                        //    throw new Exception("Invalid Token");
                        //}
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
        //done internet exception handling
        public static Dashboard GAZTGetDashboardData(String Lang, String Tin)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                DateTime dt = DateTime.Now;
                Dashboard dashboardData = null;

                string NewToken = string.Empty;
                try
                {
                    if (false == CrossConnectivity.Current.IsConnected)
                    {
                        throw new WebException();
                    }

                    HttpClient client = new HttpClient(App.httpClientHandler);

                    // string uri = "https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/ZDSM_TAXPAYER_SRV/HEADERSet?$filter=Tin eq '3300036062'&saml2=disabled  ";
                    string uri = Constants.GetDashboardData + Tin + "'" + "&saml2=disabled" + "&$format=json";

                    client.DefaultRequestHeaders.Add("Token", App.Token);
                    HttpResponseMessage GAZTGetDashboardResponse = null;

                    GAZTGetDashboardResponse = client.GetAsync(uri).Result;


                    if (GAZTGetDashboardResponse != null)
                    {
                        HttpHeaders headers = GAZTGetDashboardResponse.Headers;
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
                        String GAZTGetDashboardResponseJSON = GAZTGetDashboardResponse.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(GAZTGetDashboardResponseJSON))
                        {
                            GAZTGetDashboardResponseJSON = JObject.Parse(GAZTGetDashboardResponseJSON)["d"].ToString();
                            dashboardData = JsonConvert.DeserializeObject<Dashboard>(GAZTGetDashboardResponseJSON);
                        }
                    }
                    return dashboardData;


                }
                catch (WebException webException)
                {
                    return null;
                }
                catch (Exception exception)
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
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    // string uri = "https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/ZDP_FRGT_USRNM_PWD_SRV/HeaderSet(Tin='3102164652',EmailId='',TpType='',MobileNo='',SubType='',Idnumber='',Otp='',NewPwd='',RdBt='P',Dob=datetime'2015-07-05T15:13:49',Langu='E')?Saml2=disabled&$format=json";
                    string uri = Constants.FogotPasswordSendOTP + Tin + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "" + "'" + ",MobileNo='" + "" + "'" + ",SubType='" + "" + "'" + ",Idnumber='" + "" + "'" + ",Otp='" + "" + "'" + ",NewPwd='" + "" + "'" + ",RdBt='" + "P'" + ",Dob=datetime'" + "2015-07-05T15:13:49" + "'" + ",Langu='" + lang + "'" + ")?Saml2=disabled&$format=json";
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
                    ForgotPasswordOTP forgotPasswordOTP = new ForgotPasswordOTP();
                    string url = Constants.SendUserNameToEmail;
                    var uri = new Uri(url);
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
        public static async Task<ForgotPasswordOTP> GAZTChangePassword(ForgotPasswordOTP forgotUserOTP)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    ForgotPasswordOTP forgotPasswordOTP = new ForgotPasswordOTP();
                    string url = Constants.ChangePassword;
                    var uri = new Uri(url);
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
                    String url = Constants.GetTinStatus + _language + "',Tin='" + Tin + "" + "'" + ")?saml2=disabled&sap-language=’" + lang + "" + "'" + "&$expand=ItemSet&$format=json";
                    client.DefaultRequestHeaders.Add("Token", App.Token);
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTTinStatus = await client.GetAsync(uri);

                    if (GAZTTinStatus != null)
                    {
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
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.GetVATLookUpDetails + lang + "'" + "&$filter=Idtype eq " + IdType + "  and Idnumber eq '" + IdNumber + "'&$format=json";
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTVATLookUp = await client.GetAsync(uri);

                    if (GAZTVATLookUp != null)
                    {

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


                        vATLookUp = JsonConvert.DeserializeObject<VATLookUp>(TINStatusResponse);


                    }
                    return vATLookUp;
                }
                catch (Exception ex)
                {
                    //if (string.Equals(ex.Message, AppResources.Nodataavailable))
                    //{
                    //    throw new Exception(AppResources.Nodataavailable);
                    //}
                    //else
                    //{
                    //    throw new Exception(AppResources.NetworkConnectivityIssue);
                    //}
                    return null;
                }
            }
            else
            {

                throw new InternetException(AppResources.ZZInternetConnectionMessage);

            }
        }

        //done internet exception handling
        public static async Task<VATDeclaration> GAZTGetVATReturns(string Fbguid, string Fbnumz, string EUser, string PeriodCode)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {

                    VATDeclaration _vATDeclaration = new VATDeclaration();
                    char LangZ = GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    // String url = "https://sapgatewayqa.gazt.gov.sa:443/sap/opu/odata/SAP/ZDP_VATR_M_SRV/HDRSet(Periodkeyz='',Fbnumz='',Langz='E',Officerz='',Gpartz='3100032587',Euser='3100032587',Fbguid='005056B1F8FB1EEA8EEEAA379984A7B3')?saml2=disabled&$expand=ADRSet,ATTACHSet,CFSet,IBANSet,NOTESSet,VATR_MSGSet";
                    String url = Constants.GAZTGetAllVATDeclarationReturnData + "" + "'" + ",Fbnumz='" + "" + "'" + ",Langz='" + LangZ + "'" + ",Officerz='" + "" + "'" + ",Gpartz='" + App.TP.Tin + "'" + ",Euser='" + EUser + "'" + ",Fbguid='" + Fbguid + "')?saml2=disabled&$expand=ADRSet,ATTACHSet,CFSet,IBANSet,NOTESSet,VATR_MSGSet&$format=json";
                    client.DefaultRequestHeaders.Add("Token", App.Token);
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTVATReturnStatus = await client.GetAsync(uri);

                    if (GAZTVATReturnStatus != null)
                    {

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
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        if(NewToken=="")
                        {
                            App.IsSessionExpired = true;
                            return null;
                            App.Token = NewToken;
                        }

                        String VATReturn = GAZTVATReturnStatus.Content.ReadAsStringAsync().Result;


                        _vATDeclaration = JsonConvert.DeserializeObject<VATDeclaration>(VATReturn);


                    }
                    return _vATDeclaration;
                }
                catch (Exception ex)
                {
                    //if (string.Equals(ex.Message, AppResources.Nodataavailable))
                    //{
                    //    throw new Exception(AppResources.Nodataavailable);
                    //}
                    //else
                    //{
                    //    throw new Exception(AppResources.NetworkConnectivityIssue);
                    //}
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

        //done internet exception handling
        public static VATDeclaration SaveVATDeclarationData(VATDeclaration vATDeclaration)
        {
            VATDeclaration RequestVATDeclaration = new VATDeclaration();
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {

                    if (vATDeclaration.d != null)
                    {
                        RequestVATDeclaration = vATDeclaration;
                        RequestVATDeclaration.d.SubmitFg = "X";
                        
                    }

                    VATDeclaration _vATDeclarationD = new VATDeclaration();
                    char LangZ = GetLangZParameter();
                    string lang = UtilityManager.GetLanguageParameter();
                    String url = Constants.SaveVATDeclarationData;
                    vATDeclaration.d.Langz = lang;

                    // String url = "https://sapgatewayqa.gazt.gov.sa:443/sap/opu/odata/SAP/ZDP_VATR_M_SRV/HDRSet?&saml2=disabled";// Constants.SaveVATDeclarationData;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    client.DefaultRequestHeaders.Add("Token", App.Token);
                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    var serilized = JsonConvert.SerializeObject(RequestVATDeclaration);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    HttpResponseMessage res = client.PostAsync(uri, contentPost).Result;
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
                        }

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
                client.DefaultRequestHeaders.Add("Token", App.Token);
                var uri = new Uri(url);
                HttpResponseMessage GAZTVATLookUp = await client.GetAsync(uri);
                if (GAZTVATLookUp != null)
                {
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
                //if (string.Equals(ex.Message, AppResources.Nodataavailable))
                //{
                //    throw new Exception(AppResources.Nodataavailable);
                //}
                //else
                //{
                //    throw new Exception(AppResources.NetworkConnectivityIssue);
                //}
                return null;
            }
        }
        //done internet exception handling
      
        public static List<IBANIDNumber> GAZTGetIBANIdNumber(string IBANType)
        {
            List<IBANIDNumber> iBANIDNumbers = new List<IBANIDNumber>();
            if (CrossConnectivity.Current.IsConnected)
            {

                string NewToken = string.Empty;
                try
                {

                    string lang = UtilityManager.GetLanguageParameter();

                    HttpClient client = new HttpClient(App.httpClientHandler);
                    client.DefaultRequestHeaders.Add("Token", App.Token);
                    //String url = Constants.GAZTValidateOTPForEmail + "Langz='" + Lang + "',Tin='" + Tin + "',Otp='" + OTP + "',CurrEmail='" + CurrentEmail + "',NewEmail='" + NewEmail + "',CurrMobile='" + "" + "',NewMobile='" + "" + "',CurrPwd='" + CurrentPassword + "',NewPwd='" + NewPassword + "')?$format=json&saml2=disabled&sap-language=" + Lang;
                    String url = Constants.GAZTGetIdNumber + App.TP.Tin + "'" + "and Type eq '" + IBANType + "'" + "&saml2=disabled&sap-langauge='" + lang + "'&$format=json";//https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VATR_UH_SRV/UI_HDRSet(Fbnum='',Lang='E',Operation='',Gpart='3100032587',Status='E0001',TxnTp='VTR_ASMT',Formproc='',Periodkey='18JU')?saml2=disabled&$expand=IBANSet,IGRTSet,ITUDSet,UI_BTNSet,VATRSet,VTTHSet&$format=json";
                    var uri = new Uri(url);

                    HttpResponseMessage GAZTValidateOTPResponse = client.GetAsync(uri).Result;
                    if (GAZTValidateOTPResponse != null)
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
                    client.DefaultRequestHeaders.Add("Token", App.Token);
                    //String url = Constants.GAZTValidateOTPForEmail + "Langz='" + Lang + "',Tin='" + Tin + "',Otp='" + OTP + "',CurrEmail='" + CurrentEmail + "',NewEmail='" + NewEmail + "',CurrMobile='" + "" + "',NewMobile='" + "" + "',CurrPwd='" + CurrentPassword + "',NewPwd='" + NewPassword + "')?$format=json&saml2=disabled&sap-language=" + Lang;
                    String url = Constants.GAZTCheckIBANNumber + IBAN + "')"+"?saml2=disabled&sap-langauge=" + lang + "&$format=json";//https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VATR_UH_SRV/UI_HDRSet(Fbnum='',Lang='E',Operation='',Gpart='3100032587',Status='E0001',TxnTp='VTR_ASMT',Formproc='',Periodkey='18JU')?saml2=disabled&$expand=IBANSet,IGRTSet,ITUDSet,UI_BTNSet,VATRSet,VTTHSet&$format=json";
                    var uri = new Uri(url);

                    HttpResponseMessage GAZTValidateOTPResponse = client.GetAsync(uri).Result;
                    if (GAZTValidateOTPResponse != null)
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
                        catch(Exception e)
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
                    client.DefaultRequestHeaders.Add("Token", App.Token);
                    //String url = Constants.GAZTValidateOTPForEmail + "Langz='" + Lang + "',Tin='" + Tin + "',Otp='" + OTP + "',CurrEmail='" + CurrentEmail + "',NewEmail='" + NewEmail + "',CurrMobile='" + "" + "',NewMobile='" + "" + "',CurrPwd='" + CurrentPassword + "',NewPwd='" + NewPassword + "')?$format=json&saml2=disabled&sap-language=" + Lang;
                    String url = Constants.GAZTGetVATDeclarationCalculationDataUrl + "'" + FormBundleNumber + "'" + ",Lang='" + lang + "'" + ",Operation='" + "'" + ",Gpart='" + Gpart + "'" + ",Status='" + status + "'" + ",TxnTp='" + TxnTp + "'" + ",Formproc='" + "'" + ",Periodkey='" + periodKey + "'" + ")?saml2=disabled&$expand=IBANSet,IGRTSet,ITUDSet,UI_BTNSet,VATRSet,VTTHSet&$format=json";//https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VATR_UH_SRV/UI_HDRSet(Fbnum='',Lang='E',Operation='',Gpart='3100032587',Status='E0001',TxnTp='VTR_ASMT',Formproc='',Periodkey='18JU')?saml2=disabled&$expand=IBANSet,IGRTSet,ITUDSet,UI_BTNSet,VATRSet,VTTHSet&$format=json";
                    var uri = new Uri(url);

                    HttpResponseMessage GAZTValidateOTPResponse = await client.GetAsync(uri);
                    if (GAZTValidateOTPResponse != null)
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


        //public static async Task<TINStatus> GAZTGetEstimateZakatReturnList()
        //{
        //    //  TINStatus tINStatus = new TINStatus();
        //    string NewToken = string.Empty;
        //    try
        //    {
        //        string _language = null;
        //        if (App.IsArabic)
        //            _language = "A";
        //        else
        //            _language = "E";
        //        HttpClient client = new HttpClient(App.httpClientHandler);
        //        //String url = Constants.GetTinStatus + _language + "',Tin='" + Tin + "" + "'" + ")?saml2=disabled&sap-language=’" + lang + "" + "'" + "&$expand=ItemSet&$format=json";
        //        String url = "https://sapgatewayqa.gazt.gov.sa:443/sap/opu/odata/SAP/Z_TAX01RET_WI_SRV/HeaderSet(Bpnum='3102226654',Auditor='',Lang='EN',UserTin='3102226654')?saml2=disabled&sap-language='EN'&$expand=listSet&$format=json";

        //        client.DefaultRequestHeaders.Add("Token", App.Token);
        //        var uri = new Uri(url);
        //        HttpResponseMessage GAZTEstimateZakatReturnList = await client.GetAsync(uri);

        //        if (GAZTEstimateZakatReturnList != null)
        //        {
        //            HttpHeaders headers = GAZTEstimateZakatReturnList.Headers;
        //            IEnumerable<string> values;
        //            if (headers.TryGetValues("token", out values))
        //            {
        //                NewToken = values.First();
        //            }

        //            if ((!string.IsNullOrEmpty(NewToken)))
        //            {
        //                if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")))
        //                {
        //                    App.IsSessionExpired = true;
        //                    return null;
        //                }
        //                App.Token = NewToken;
        //            }

        //            String EstimateZakatReturnList = GAZTEstimateZakatReturnList.Content.ReadAsStringAsync().Result;


        //            //  tINStatus = JsonConvert.DeserializeObject<TINStatus>(TINStatusResponse);


        //        }
        //        return null;// tINStatus;
        //    }
        //    catch (Exception ex)
        //    {
        //        //if (string.Equals(ex.Message, AppResources.Nodataavailable))
        //        //{
        //        //    throw new Exception(AppResources.Nodataavailable);
        //        //}
        //        //else
        //        //{
        //        //    throw new Exception(AppResources.NetworkConnectivityIssue);
        //        //}
        //        return null;
        //    }
        //}

        //Seesion expired handled       
        //done internet exception handling
        public static async Task<AttachmentRootOject> GAZTSaveVATDeclarationAttachment(byte[] AttachmentByte, string fileName, string RetGuid, string Dotyp)//, string returnedFguid
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
                    //    String url = Constants.GAZTSaveAttachment + "'" + "'" + ",RetGuid='" + RetGuid + "'" + ",Flag='" + "N" + "'" + ",Dotyp='" + Dotyp + "'" + ",SchGuid='" + "'" + ",Srno=" + "1" + ",Doguid='" + "'" + ",AttBy='" + AttBy + "'" + ")/AttachMedSet"; //",RetGuid='005056B1F8FB1EDA8FF041CFF05E83A9',Flag='N',Dotyp='VTA0',SchGuid='',Srno=1,Doguid='',AttBy='TP')/AttachMedSet";// Constants.SaveVATDeclarationData;
                    String url = Constants.GAZTSaveAttachment + "'" + "'" + ",RetGuid='" + RetGuid + "'" + ",Flag='" + "N" + "'" + ",Dotyp='" + Dotyp + "'" + ",SchGuid='" + "'" + ",Srno=" + "1" + ",Doguid='" + "'" + ",AttBy='" + AttBy + "'" + ")/AttachMedSet"; //",RetGuid='005056B1F8FB1EDA8FF041CFF05E83A9',Flag='N',Dotyp='VTA0',SchGuid='',Srno=1,Doguid='',AttBy='TP')/AttachMedSet";// Constants.SaveVATDeclarationData;
                                                                                                                                                                                                                                                                     // lang + "'" + "&$filter=Idtype eq " + IdType + ",RetGuid='" + RetGuid + "'" +
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient();
                    //  client.DefaultRequestHeaders.Add("Token", App.Token);
                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    // client.DefaultRequestHeaders.Add("content-type", "multipart/form-data");
                    client.DefaultRequestHeaders.Add("slug", fileName);
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "multipart/form-data");

                    MultipartFormDataContent content = new MultipartFormDataContent();
                    ByteArrayContent baContent = new ByteArrayContent(AttachmentByte);
                    content.Add(baContent, "File", fileName);
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
                    String url = Constants.GAZTDeteleAttachment + "'" + "'" + ",RetGuid='undefined'" + ",Flag='" + "N" + "'" + ",Dotyp='" + Dotyp + "'" + ",SchGuid='" + "'" + ",Srno=" + "1" + ",Doguid='" + RetGuid + "'" + ",AttBy='" + AttBy + "'" + ")/$value?saml2=disabled"; //",RetGuid='005056B1F8FB1EDA8FF041CFF05E83A9',Flag='N',Dotyp='VTA0',SchGuid='',Srno=1,Doguid='',AttBy='TP')/AttachMedSet";// Constants.SaveVATDeclarationData;
                                                                                                                                                                                                                                                                                    // lang + "'" + "&$filter=Idtype eq " + IdType + ",RetGuid='" + RetGuid + "'" +
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient();
                    //  client.DefaultRequestHeaders.Add("Token", App.Token);
                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    // client.DefaultRequestHeaders.Add("content-type", "multipart/form-data");
                    client.DefaultRequestHeaders.Add("slug", fileName);
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "multipart/form-data");
                    //MultipartFormDataContent content = new MultipartFormDataContent();
                    //ByteArrayContent baContent = new ByteArrayContent(AttachmentByte);
                    // content.Add(baContent, "File", fileName);
                    // var response = await client.DeleteAsync(url);
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
                    // String url = "https://sapgatewayqa.gazt.gov.sa:443/sap/opu/odata/SAP/Z_GET_SADAD_SRV/SadadSet?&saml2=disabled&sap-langauge=’EN’&$filter=Langu eq'E'and Fbnum eq '65000178680' ";
                    // String url = "/sap/opu/odata/SAP/Z_GET_SADAD_SRV/SadadSet?&saml2=disabled&sap-langauge=’EN’&$filter=Langu eq'E'and Fbnum eq '65000178680' ";
                    String url = Constants.GAZTGetSADADNumber + lang + "'" + "&$format=json&$filter=Langu eq'" + LangZ + "'and Fbnum eq '" + FormBundleID + "'" + "";
                    HttpClient client = new HttpClient();                                                                                                                                                                                                                                       // lang + "'" + "&$filter=Idtype eq " + IdType + ",RetGuid='" + RetGuid + "'" +
                    var uri = new Uri(url);
                    client.DefaultRequestHeaders.Add("Token", App.Token);
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
                    //String url = Constants.GetTinStatus + _language + "',Tin='" + Tin + "" + "'" + ")?saml2=disabled&sap-language=’" + lang + "" + "'" + "&$expand=ItemSet&$format=json";
                    // String url = "https://sapgatewayqa.gazt.gov.sa:443/sap/opu/odata/SAP/Z_TAX01RET_WI_SRV/HeaderSet(Bpnum='3101965624',Auditor='',Lang='EN',UserTin='3101965624')?saml2=disabled&sap-language='EN'&$expand=listSet&$format=json";
                    String url = Constants.GAZTGetZakatReturnList + App.TP.Userid + "'" + ",Auditor='" + "'" + ",Lang='" + _language + "'" + ",UserTin='" + App.TP.Userid + "'" + ")?saml2=disabled&sap-language='" + _language + "'" + "&$expand=listSet&$format=json";
                    client.DefaultRequestHeaders.Add("Token", App.Token);

                    var uri = new Uri(url);
                    HttpResponseMessage GAZTEstimateZakatReturnList = await client.GetAsync(uri);

                    if (GAZTEstimateZakatReturnList != null)
                    {
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
                    //if (string.Equals(ex.Message, AppResources.Nodataavailable))
                    //{
                    //    throw new Exception(AppResources.Nodataavailable);
                    //}
                    //else
                    //{
                    //    throw new Exception(AppResources.NetworkConnectivityIssue);
                    //}
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
        public static VATDeclaration GAZTSetVATReturnVoid(VATDeclaration vATDeclaration)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                VATDeclaration RequestVATDeclaration = null;
            try
            {
                RequestVATDeclaration = SaveVATDeclarationData(vATDeclaration);
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
        public static VATDeclaration GAZTSetVATReturnReset(VATDeclaration vATDeclaration)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                VATDeclaration RequestVATDeclaration = null;
                try
                {
                    RequestVATDeclaration = SaveVATDeclarationData(vATDeclaration);
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

        public static VATDeclaration GAZTSetVATReturnAmend(VATDeclaration vATDeclaration)
        {
            VATDeclaration RequestVATDeclaration = null;
            try
            {
                RequestVATDeclaration = SaveVATDeclarationData(vATDeclaration);
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
                    client.DefaultRequestHeaders.Add("Token", App.Token);
                    //  https://sapgatewayqa.gazt.gov.sa:443/sap/opu/odata/sap/ZDP_FZ12_SRV/HeaderSet(Fbnumz='',Langz='EN',Gpartz='3102226654',Euser='3102226654',Fbguid='005056B1F8FB1EEA8FBC3AD61DEA5627',Invflg='',Fsource='TP')?saml2=disabled&sap-language='EN'&$expand=ReasonSet,AttachSet,ThresholdSet,InvoiceSet&$format=json
                    String url = Constants.GAZTGetZakatReturn + "'" + ",Langz='" + lang + "'" + ",Gpartz='" + App.TP.Userid + "'" + ",Euser='" + App.TP.Userid + "'" + ",Fbguid='" + fbguid + "'" + ",Invflg='" + "'" + ",Fsource='" + "TP" + "'" + ")?saml2=disabled&sap-language='" + lang + "'&$expand=ReasonSet,AttachSet,ThresholdSet,InvoiceSet&$format=json";
                    var uri = new Uri(url);

                    HttpResponseMessage GAZTValidateOTPResponse = await client.GetAsync(uri);
                    if (GAZTValidateOTPResponse != null)
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
                        String _zakatReturnDetailsJSON = GAZTValidateOTPResponse.Content.ReadAsStringAsync().Result;
                        zakatReturnDetails = JsonConvert.DeserializeObject<ZakatReturnDetails>(_zakatReturnDetailsJSON);
                    }

                    return zakatReturnDetails;

                }
                catch (Exception ex)
                {
                    return null;
                    //if (string.Equals(ex.Message, AppResources.InvalidOTP))
                    //{
                    //    throw new Exception(AppResources.InvalidEmail);
                    //}
                    //else
                    //{
                    //    throw new Exception(AppResources.NetworkConnectivityIssue);
                    //}
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
                char LangZ = GetLangZParameter();
                // String url = "https://sapgatewayqa.gazt.gov.sa:443/sap/opu/odata/SAP/ZDP_INDTAX_ATT_SRV/AttachSet(OutletRef='',RetGuid='005056B1F8FB1EDA8FF041CFF05E83A9',Flag='N',Dotyp='VTA0',SchGuid='',Srno=1,Doguid='',AttBy='TP')/AttachMedSet";// Constants.SaveVATDeclarationData;
                String url = Constants.GAZTSaveEstimatedZaktReturn;
                // lang + "'" + "&$filter=Idtype eq " + IdType + ",RetGuid='" + RetGuid + "'" +
                var uri = new Uri(url);
                HttpClient client = new HttpClient();
                var serilized = JsonConvert.SerializeObject(zakatReturnDetailsD);

                client.DefaultRequestHeaders.Add("Token", App.Token);
                client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                HttpResponseMessage res = client.PostAsync(uri, contentPost).Result;

                var _zakatReturnDetailsDesponsestr = res.Content.ReadAsStringAsync().Result;
                _zakatReturnDetailsD = JsonConvert.DeserializeObject<ZakatReturnDetails>(_zakatReturnDetailsDesponsestr);
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

                String url = Constants.GAZTVATReturnGetApplicableButtons + "'" + Fbnum + "'" + ",Lang='" + LangZ + "'" + ",Operation='" + Operation + "'," + "Gpart=" + "'" + Gpart + "',Status='" + Status + "',TxnTp='" + TxnTp + "',Formproc='',Periodkey='" + PeriodKey + "'" + ")?saml2=disabled&$expand=UI_BTNSet&$format=json";

                client.DefaultRequestHeaders.Add("Token", App.Token);

                var uri = new Uri(url);

                HttpResponseMessage ApplicableButtonsResponse = await client.GetAsync(uri);


                if (ApplicableButtonsResponse != null)
                {
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
                    // client.DefaultRequestHeaders.Add("Token", App.Token);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    String url = "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_FZ12_BS_SRV/UI_HdrSet(Fbtypz='FZ12',UserTypz='TP',Fbnum='94000001174',Gpart='',Status='E0003',TxnTp='',Formproc='FZ12',Lang='EN',Officer='')?&$expand=UI_BtnSet&$format=json&saml2=disabled";
                    var uri = new Uri(url);

                    HttpResponseMessage GAZTValidateOTPResponse = await client.GetAsync(uri);
                    if (GAZTValidateOTPResponse != null)
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
                        String _esimatedZAKATReturnsButtonSets = GAZTValidateOTPResponse.Content.ReadAsStringAsync().Result;
                        ZAKATReturnApplicableButtons = JsonConvert.DeserializeObject<EsimatedZAKATReturnsButtonSets>(_esimatedZAKATReturnsButtonSets);
                    }

                    return ZAKATReturnApplicableButtons;

                }
                catch (Exception ex)
                {
                    //if (string.Equals(ex.Message, AppResources.InvalidOTP))
                    //{
                    //    throw new Exception(AppResources.InvalidEmail);
                    //}
                    //else
                    //{
                    //    throw new Exception(AppResources.NetworkConnectivityIssue);
                    //}
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
        public static async Task<AttachmentRootOject> GAZTSaveEstimatedZAKATAttachment(byte[] AttachmentByte, string fileName, string RetGuid, string Dotyp)//, string returnedFguid
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
                string url = Constants.GAZTSaveEstimatedZAKATAttachement + RetGuid + "',Flag='N',Dotyp='Z12L',SchGuid='',Srno=1,Doguid='',AttBy='TP',OutletRef='')/AttachMedSet?saml2=disabled";

                var uri = new Uri(url);
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("slug", fileName);
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "multipart/form-data");

                MultipartFormDataContent content = new MultipartFormDataContent();
                ByteArrayContent baContent = new ByteArrayContent(AttachmentByte);
                content.Add(baContent, "File", fileName);
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
                    //if (InvFlag.Equals("I"))
                    //{
                        url = Constants.GAZTGetEstimatedZAKATSADADNumber + FBNumber + "'" + ",Langz='" + lang + "'" + ",Gpartz='" + "'" + ",Euser='" + "0000000000" + App.TP.Userid + "'" + ",Fbguid='" + FBGuid + "'" + ",Invflg='I',Fsource='TP')?saml2=disabled&$expand=InvoiceSet&$format=json";
                    //}
                    //else
                    //{
                    //    url = Constants.GAZTGetEstimatedZAKATSADADNumber + FBNumber + "'" + ",Langz='" + lang + "'" + ",Gpartz='" + "'" + ",Euser='" + "0000000000" + App.TP.Userid + "'" + ",Fbguid='" + FBGuid + "'" + ",Invflg='S',Fsource='TP')?saml2=disabled&$expand=InvoiceSet&$format=json";
                    //}
                    client.DefaultRequestHeaders.Add("Token", App.Token);

                    var uri = new Uri(url);
                    HttpResponseMessage GAZTEstimateZakatReturnList = await client.GetAsync(uri);

                    if (GAZTEstimateZakatReturnList != null)
                    {
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
                    //if (string.Equals(ex.Message, AppResources.Nodataavailable))
                    //{
                    //    throw new Exception(AppResources.Nodataavailable);
                    //}
                    //else
                    //{
                    //    throw new Exception(AppResources.NetworkConnectivityIssue);
                    //}
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
                String url = Constants.GAZTGetEstimatedZAKATSADADNumber + FBNumber + "'" + ",Langz='" + lang + "'" + ",Gpartz='" + "'" + ",Euser='00000000000000000000'" + ",Fbguid='" + FBGuid + "'" + ",Invflg='I',Fsource='TP')?saml2=disabled&$expand=InvoiceSet";
                client.DefaultRequestHeaders.Add("Token", App.Token);

                var uri = new Uri(url);
                HttpResponseMessage GAZTEstimateZakatReturnList = await client.GetAsync(uri);

                if (GAZTEstimateZakatReturnList != null)
                {
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
                //if (string.Equals(ex.Message, AppResources.Nodataavailable))
                //{
                //    throw new Exception(AppResources.Nodataavailable);
                //}
                //else
                //{
                //    throw new Exception(AppResources.NetworkConnectivityIssue);
                //}
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
                    String url1 = Constants.GAZTDeteleAttachment + "'" + "'" + ",RetGuid='undefined'" + ",Flag='" + "N" + "'" + ",Dotyp='" + Dotyp + "'" + ",SchGuid='" + "'" + ",Srno=" + "1" + ",Doguid='" + DocumentID + "'" + ",AttBy='" + AttBy + "'" + ")/$value?saml2=disabled"; //",RetGuid='005056B1F8FB1EDA8FF041CFF05E83A9',Flag='N',Dotyp='VTA0',SchGuid='',Srno=1,Doguid='',AttBy='TP')/AttachMedSet";// Constants.SaveVATDeclarationData;
                    string url = "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_INDTAX_ATT_SRV/AttachMedSet(RetGuid='',Flag='N',Dotyp='',SchGuid='',Srno=1,Doguid='" + DocumentID + "',AttBy='TP',OutletRef='')/$value?saml2=disbaled";                                                                                                                                                                                                                                                          // lang + "'" + "&$filter=Idtype eq " + IdType + ",RetGuid='" + RetGuid + "'" +
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
    }

}


