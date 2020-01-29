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
                                    if((0 == String.Compare(Token, "Taxpayer's account is not active with GAZT.")))
                                    {
                                        throw new Exception(Token);
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
                    HttpResponseMessage GAZTMyBillsResponse =  client.GetAsync(uri).Result;
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
        public static async Task<ICR> GAZTGetICRs(String Tin, string lang)
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
                    HttpResponseMessage GAZTMyICRsResponse = await client.GetAsync(uri);
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
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    // string uri = "https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/ZDP_FRGT_USRNM_PWD_SRV/HeaderSet(Tin='3102164652',EmailId='',TpType='',MobileNo='',SubType='',Idnumber='',Otp='',NewPwd='',RdBt='P',Dob=datetime'2015-07-05T15:13:49',Langu='E')?Saml2=disabled&$format=json";
                    string uri = Constants.FogotPasswordSendOTP + Tin + "'" + ",EmailId='" + "" + "'" + ",TpType='" + "" + "'" + ",MobileNo='" + "" + "'" + ",SubType='" + "" + "'" + ",Idnumber='" + "" + "'" + ",Otp='" + "" + "'" + ",NewPwd='" + "" + "'" + ",RdBt='" + "P'" + ",Dob=datetime'" + "2015-07-05T15:13:49" + "'" + ",Langu='" + "E" + "'" + ")?Saml2=disabled&$format=json";
                    HttpResponseMessage GAZTFogotPasswordSendOTPResponse = await client.GetAsync(uri);
                    if (GAZTFogotPasswordSendOTPResponse != null)
                    {
                        HttpHeaders headers = GAZTFogotPasswordSendOTPResponse.Headers;
                        String GAZTGetSendOTPResponseJSON = GAZTFogotPasswordSendOTPResponse.Content.ReadAsStringAsync().Result;
                        /// GAZTGetSendOTPResponseJSON = JObject.Parse(GAZTGetSendOTPResponseJSON)["d"].ToString();
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
        public static async Task<VATDeclaration> GAZTGetVATReturns(string Fbguid)
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
                    String url = Constants.GAZTGetAllVATDeclarationReturnData + "'" + ",Fbnumz='" + "" + "'" + ",Langz='" + LangZ + "'" + ",Officerz='" + "" + "'" + ",Gpartz='" + App.TP.Userid + "'" + ",Euser='" + App.TP.Userid + "'" + ",Fbguid='" + Fbguid + "')?saml2=disabled&$expand=ADRSet,ATTACHSet,CFSet,IBANSet,NOTESSet,VATR_MSGSet&$format=json";
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
                            if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")))
                            {
                                App.IsSessionExpired = true;
                                return null;
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
        public static async Task<VATDeclaration> SaveVATDeclarationData(VATDeclaration vATDeclaration)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
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
                    var serilized = JsonConvert.SerializeObject(vATDeclaration);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    var detailJson = res.Content.ReadAsStringAsync().Result;
                    _vATDeclarationD = JsonConvert.DeserializeObject<VATDeclaration>(detailJson);
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
                    String url = Constants.GAZTGetVATDeclarationCalculationDataUrl + "'" + FormBundleNumber  + "'" + ",Lang='" + lang + "'" + ",Operation='" + "'" + ",Gpart='" + Gpart + "'" + ",Status='" + status + "'" + ",TxnTp='" + TxnTp + "'" + ",Formproc='" + "'" + ",Periodkey='" + periodKey + "'" + ")?saml2=disabled&$expand=IBANSet,IGRTSet,ITUDSet,UI_BTNSet,VATRSet,VTTHSet&$format=json";//https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_VATR_UH_SRV/UI_HDRSet(Fbnum='',Lang='E',Operation='',Gpart='3100032587',Status='E0001',TxnTp='VTR_ASMT',Formproc='',Periodkey='18JU')?saml2=disabled&$expand=IBANSet,IGRTSet,ITUDSet,UI_BTNSet,VATRSet,VTTHSet&$format=json";
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


        public static async Task<TINStatus> GAZTGetEstimateZakatReturnList()
        {
          //  TINStatus tINStatus = new TINStatus();
            string NewToken = string.Empty;
            try
            {
                string _language = null;
                if (App.IsArabic)
                    _language = "A";
                else
                    _language = "E";
                HttpClient client = new HttpClient(App.httpClientHandler);
                //String url = Constants.GetTinStatus + _language + "',Tin='" + Tin + "" + "'" + ")?saml2=disabled&sap-language=’" + lang + "" + "'" + "&$expand=ItemSet&$format=json";
                String url = "https://sapgatewayqa.gazt.gov.sa:443/sap/opu/odata/SAP/Z_TAX01RET_WI_SRV/HeaderSet(Bpnum='3102226654',Auditor='',Lang='EN',UserTin='3102226654')?saml2=disabled&sap-language='EN'&$expand=listSet&$format=json";

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


                  //  tINStatus = JsonConvert.DeserializeObject<TINStatus>(TINStatusResponse);


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


        public static async Task<AttachmentRootOject> GAZTSaveVATDeclarationAttachment(byte[] AttachmentByte, string fileName, string RetGuid)//, string returnedFguid
        {
            try
            {
                AttachmentRootOject _attachment = new AttachmentRootOject();
                char LangZ = GetLangZParameter();
                string Dotyp = "VTA0";
                string AttBy = "TP";
                // String url = "https://sapgatewayqa.gazt.gov.sa:443/sap/opu/odata/SAP/ZDP_INDTAX_ATT_SRV/AttachSet(OutletRef='',RetGuid='005056B1F8FB1EDA8FF041CFF05E83A9',Flag='N',Dotyp='VTA0',SchGuid='',Srno=1,Doguid='',AttBy='TP')/AttachMedSet";// Constants.SaveVATDeclarationData;
                String url = Constants.GAZTSaveAttachment + "'" + "'" + ",RetGuid='" + RetGuid + "'" + ",Flag='" + "N" + "'" + ",Dotyp='" + Dotyp + "'" + ",SchGuid='" + "'" + ",Srno=" + "1"  + ",Doguid='" + "'" + ",AttBy='" + AttBy + "'" + ")/AttachMedSet"; //",RetGuid='005056B1F8FB1EDA8FF041CFF05E83A9',Flag='N',Dotyp='VTA0',SchGuid='',Srno=1,Doguid='',AttBy='TP')/AttachMedSet";// Constants.SaveVATDeclarationData;
               // lang + "'" + "&$filter=Idtype eq " + IdType + ",RetGuid='" + RetGuid + "'" +
                var uri = new Uri(url);
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Token", App.Token);
                client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
               // client.DefaultRequestHeaders.Add("content-type", "application/pdf");
                client.DefaultRequestHeaders.Add("slug", fileName);
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


        public static async Task<AttachmentRootOject> GAZTDeleteVATDeclarationAttachment(string fileName, string RetGuid)//, string returnedFguid
        {
            try
            {
                AttachmentRootOject _attachment = new AttachmentRootOject();
                char LangZ = GetLangZParameter();
                string Dotyp = "VTA0";
                string AttBy = "TP";
                // String url = "https://sapgatewayqa.gazt.gov.sa:443/sap/opu/odata/SAP/ZDP_INDTAX_ATT_SRV/AttachSet(OutletRef='',RetGuid='005056B1F8FB1EDA8FF041CFF05E83A9',Flag='N',Dotyp='VTA0',SchGuid='',Srno=1,Doguid='',AttBy='TP')/AttachMedSet";// Constants.SaveVATDeclarationData;
                String url = Constants.GAZTSaveAttachment + "'" + "'" + ",RetGuid='" + RetGuid + "'" + ",Flag='" + "N" + "'" + ",Dotyp='" + Dotyp + "'" + ",SchGuid='" + "'" + ",Srno=" + "1" + ",Doguid='" + "'" + ",AttBy='" + AttBy + "'" + ")/AttachMedSet"; //",RetGuid='005056B1F8FB1EDA8FF041CFF05E83A9',Flag='N',Dotyp='VTA0',SchGuid='',Srno=1,Doguid='',AttBy='TP')/AttachMedSet";// Constants.SaveVATDeclarationData;
                                                                                                                                                                                                                                                                 // lang + "'" + "&$filter=Idtype eq " + IdType + ",RetGuid='" + RetGuid + "'" +
                var uri = new Uri(url);
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Token", App.Token);
                client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                // client.DefaultRequestHeaders.Add("content-type", "application/pdf");
                client.DefaultRequestHeaders.Add("slug", fileName);
                //MultipartFormDataContent content = new MultipartFormDataContent();
                //ByteArrayContent baContent = new ByteArrayContent(AttachmentByte);
               // content.Add(baContent, "File", fileName);
                var response = await client.DeleteAsync(url);
                var responsestr = response.Content.ReadAsStringAsync().Result;
                _attachment = JsonConvert.DeserializeObject<AttachmentRootOject>(responsestr);


                return _attachment;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static async Task<SadadNumber> GAZTGetVATDeclarationSADADNumber(string FormBundleID)//, string returnedFguid
        {
            try
            {
                SadadNumber sadadNumber = new SadadNumber();
                char LangZ = GetLangZParameter();
                string lang = UtilityManager.GetLanguageParameter();
                // String url = "https://sapgatewayqa.gazt.gov.sa:443/sap/opu/odata/SAP/Z_GET_SADAD_SRV/SadadSet?&saml2=disabled&sap-langauge=’EN’&$filter=Langu eq'E'and Fbnum eq '65000178680' ";
                // String url = "/sap/opu/odata/SAP/Z_GET_SADAD_SRV/SadadSet?&saml2=disabled&sap-langauge=’EN’&$filter=Langu eq'E'and Fbnum eq '65000178680' ";
                String url = Constants.GAZTGetSADADNumber + lang + "'" +"&$format=json&$filter=Langu eq'" + LangZ  + "'and Fbnum eq '" + FormBundleID + "'" + "";
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

    }

}
