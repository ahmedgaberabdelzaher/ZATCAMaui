using GAZT.Helper;
using GAZT.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        /// <summary>
        /// Method used for authenticaating TP
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static String GAZTAuthenticateTIN(String UserName, String Password)
        {
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
                                <password>" + Password + @"</password>
                                 <deviceId>" + Password + @"</deviceId>
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
                                // AuthenticationResult = node.InnerText;



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


                                if (!string.IsNullOrEmpty(Token))
                                {
                                    App.Token = Token;
                                }

                                Message = node.ChildNodes[1].InnerText;

                            }
                            else
                                throw new Exception("SOAP request failed");
                        }
                    }
                }
                else
                    throw new Exception("SOAP request not created");

                return Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// After GAZTAuthenticateTIN, this is the 1st mandatory call and that too without OTP
        /// </summary>
        /// <param name="Lang"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        /// 

        public static async Task<List<TIN>> GAZTGetAllTins(String Username)
        {
            String OTPSentConfirmation = String.Empty;

            List<TIN> tinIds = new List<TIN>();
            //string NewToken = string.Empty;
            try
            {
                HttpClient client = new HttpClient(App.httpClientHandler);
                String url = Constants.GetAllTin + Username;
                var uri = new Uri(url);
                HttpResponseMessage GAZTSendAndReceiveOTPResponse = await client.GetAsync(uri);


                if (GAZTSendAndReceiveOTPResponse != null)
                {
                    OTPSentConfirmation = GAZTSendAndReceiveOTPResponse.Content.ReadAsStringAsync().Result;
                }
                if (!string.IsNullOrEmpty(OTPSentConfirmation))
                {
                    OTPSentConfirmation = JObject.Parse(OTPSentConfirmation)["tinData"].ToString();
                    tinIds = JsonConvert.DeserializeObject<List<TIN>>(OTPSentConfirmation);

                    // OTPSentConfirmationJToken = JObject.Parse(OTPSentConfirmation)["Result"];
                }

                return tinIds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public static async Task<String> GAZTSendAndReceiveOTP(String Lang, String UserId)
        {
            String OTPSentConfirmation = String.Empty;
            string NewToken = string.Empty;
            try
            {
                HttpClient client = new HttpClient(App.httpClientHandler);
                String url = Constants.GAZTSendAndReceiveOTP + Lang + "',Userid='" + UserId + "',Otp='')?&saml2=disabled&$format=json";
                var uri = new Uri(url);
                client.DefaultRequestHeaders.Add("Token", App.Token);
                HttpResponseMessage GAZTSendAndReceiveOTPResponse = await client.GetAsync(uri);


                if (GAZTSendAndReceiveOTPResponse != null)
                {
                    HttpHeaders headers = GAZTSendAndReceiveOTPResponse.Headers;
                    IEnumerable<string> values;
                    if (headers.TryGetValues("token", out values))
                    {
                        NewToken = values.First();
                    }

                    if ((0 == String.Compare(NewToken, "Invalid Token")))
                    {
                        throw new Exception("Invalid Token");
                    }
                    if ((!string.IsNullOrEmpty(NewToken)))
                    {
                        App.Token = NewToken;
                    }


                    OTPSentConfirmation = GAZTSendAndReceiveOTPResponse.Content.ReadAsStringAsync().Result;
                }

                OTPSentConfirmation = JObject.Parse(OTPSentConfirmation)["d"].ToString();
                JToken OTPSentConfirmationJToken = JObject.Parse(OTPSentConfirmation)["Result"];

                if (OTPSentConfirmationJToken != null)
                    OTPSentConfirmation = OTPSentConfirmationJToken.Value<String>();

                return OTPSentConfirmation;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// The user received OTP and provided for authorization
        /// </summary>
        /// <param name="Lang"></param>
        /// <param name="UserId"></param>
        /// <param name="OTP"></param>
        /// <returns></returns>
        public static async Task<TaxPayerProfile> GAZTValidateOTP(String Lang, String UserId, String OTP)
        {
            TaxPayerProfile TP = null;
            String NewToken = string.Empty;
            try
            {
                HttpClient client = new HttpClient(App.httpClientHandler);

                String url = Constants.GAZTValidateOTP + Lang + "',Userid='" + UserId + "',Otp='" + OTP + "')?&saml2=disabled&$format=json";
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

                    if ((0 == String.Compare(NewToken, "Invalid Token")))
                    {
                        throw new Exception("Invalid Token");
                    }
                    if ((!string.IsNullOrEmpty(NewToken)))
                    {
                        App.Token = NewToken;
                    }


                    String GAZTValidateOTPResponseJSON = GAZTValidateOTPResponse.Content.ReadAsStringAsync().Result;

                    GAZTValidateOTPResponseJSON = JObject.Parse(GAZTValidateOTPResponseJSON)["d"].ToString();

                    JToken GAZTValidateOTPResponseJToken = JObject.Parse(GAZTValidateOTPResponseJSON)["Result"];
                    if ((0 == String.Compare(GAZTValidateOTPResponseJToken.Value<String>(), "Valid OTP")) || (0 == String.Compare(GAZTValidateOTPResponseJToken.Value<String>(), "كلمة مرور صالحة لمرة واحدة")))
                        TP = JsonConvert.DeserializeObject<TaxPayerProfile>(GAZTValidateOTPResponseJSON);
                    else
                        throw new Exception(AppResources.InvalidOTP);
                }

                return TP;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public static async Task<TaxPayerProfile> GAZTValidateOTPForMobileNumber(String Lang, String OTP, String Tin, string CurrentMobileNumber, string NewMobileNumber)
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

                    if ((0 == String.Compare(NewToken, "Invalid Token")))
                    {
                        throw new Exception("Invalid Token");
                    }
                    if ((!string.IsNullOrEmpty(NewToken)))
                    {
                        App.Token = NewToken;
                    }




                    String GAZTValidateOTPResponseJSON = GAZTValidateOTPResponse.Content.ReadAsStringAsync().Result;

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
                    //if ((0 == String.Compare(GAZTValidateOTPResponseJToken.Value<String>(), "Details changed successfully")) || (0 == String.Compare(GAZTValidateOTPResponseJToken.Value<String>(), "تم تغيير التفاصيل بنجاح")))
                    //    TP = JsonConvert.DeserializeObject<TaxPayerProfile>(GAZTValidateOTPResponseJSON);
                    //else
                    //    throw new Exception("Invalid OTP / OTP expired");
                }

                return TP;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<bool> GAZTValidateMobileNumber(String Lang, String Tin, string CurrentMobileNumber, string NewMobileNumber)
        {
            TaxPayerProfile TP = null;
            bool result = false;
            string NewToken = string.Empty;
            try
            {
                HttpClient client = new HttpClient(App.httpClientHandler);

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

                    if ((0 == String.Compare(NewToken, "Invalid Token")))
                    {
                        throw new Exception("Invalid Token");
                    }
                    if ((!string.IsNullOrEmpty(NewToken)))
                    {
                        App.Token = NewToken;
                    }


                    String GAZTValidateMobileNumberResponseJSON = GAZTValidateMobileNumberResponse.Content.ReadAsStringAsync().Result;

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

                    //if (true == GAZTValidateOTPResponseJToken.Value<bool>())
                    ////    TP = JsonConvert.DeserializeObject<TaxPayerProfile>(GAZTValidateOTPResponseJSON);
                    ////else
                    ////    throw new Exception("Invalid OTP / OTP expired");
                }

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public static async Task<bool> GAZTValidateAndChangePassword(String Lang, String Tin, string CurrentPassword, string NewPassword)
        {
            TaxPayerProfile TP = null;
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

                    if ((0 == String.Compare(NewToken, "Invalid Token")))
                    {
                        throw new Exception("Invalid Token");
                    }
                    if ((!string.IsNullOrEmpty(NewToken)))
                    {
                        App.Token = NewToken;
                    }



                    String GAZTValidateAndChangePasswordResponseJSON = GAZTValidateAndChangePasswordResponse.Content.ReadAsStringAsync().Result;

                    GAZTValidateAndChangePasswordResponseJSON = JObject.Parse(GAZTValidateAndChangePasswordResponseJSON)["d"].ToString();

                    JToken GAZTValidateAndChangePasswordResponseJToken = JObject.Parse(GAZTValidateAndChangePasswordResponseJSON)["Result"];

                    if ((0 == String.Compare(GAZTValidateAndChangePasswordResponseJToken.Value<String>(), "Password Changed Successfully")) || (0 == String.Compare(GAZTValidateAndChangePasswordResponseJToken.Value<String>(), "تم تغيير كلمة المرور بنجاح")))
                    {
                        result = true;
                    }
                    else
                    {
                        throw new ArgumentException(AppResources.InvalidPassword);
                    }

                    //if (true == GAZTValidateOTPResponseJToken.Value<bool>())
                    ////    TP = JsonConvert.DeserializeObject<TaxPayerProfile>(GAZTValidateOTPResponseJSON);
                    ////else
                    ////    throw new Exception("Invalid OTP / OTP expired");
                }

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public static async Task<string> GAZTGetPdfUrl(String Lang, String Tin)
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

                return PdfUrl;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public static async Task<List<MyBills>> GAZTGetMyBills(String Tin)
        {
            List<MyBills> myBills = new List<MyBills>();
            String MobileNumber = string.Empty;
            string PdfUrl = string.Empty;
            string NewToken = string.Empty;
            try
            {
                HttpClient client = new HttpClient(App.httpClientHandler);
                String url = Constants.GetMyBills + "Fbguid eq '" + "'and Euser eq '" + Tin + "'" + "&saml2=disabled&$format=json";
                client.DefaultRequestHeaders.Add("Token", App.Token);
                var uri = new Uri(url);

                HttpResponseMessage GAZTMyBillsResponse = await client.GetAsync(uri);

                if (GAZTMyBillsResponse != null)
                {

                    HttpHeaders headers = GAZTMyBillsResponse.Headers;
                    IEnumerable<string> values;
                    if (headers.TryGetValues("token", out values))
                    {
                        NewToken = values.First();
                    }

                    if ((0 == String.Compare(NewToken, "Invalid Token")))
                    {
                        throw new Exception("Invalid Token");
                    }
                    if ((!string.IsNullOrEmpty(NewToken)))
                    {
                        App.Token = NewToken;
                    }

                    String GAZTMyBillsResponseJSON = GAZTMyBillsResponse.Content.ReadAsStringAsync().Result;

                    GAZTMyBillsResponseJSON = JObject.Parse(GAZTMyBillsResponseJSON)["d"].ToString();


                    string GAZTMyBillsResponseJSONJToken = JObject.Parse(GAZTMyBillsResponseJSON)["results"].ToString();
                    if (string.IsNullOrEmpty(GAZTMyBillsResponseJSONJToken) != true)
                    {
                        myBills = JsonConvert.DeserializeObject<List<MyBills>>(GAZTMyBillsResponseJSONJToken);
                    }
                    else
                    {
                        throw new Exception("Invalid Response");
                    }
                }

                return myBills;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public static async Task<string> GAZTGetTaxPayerProfile(String Tin, String Lang)
        {
            TaxPayerProfile TP = null;
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

                    if ((0 == String.Compare(NewToken, "Invalid Token")))
                    {
                        throw new Exception("Invalid Token");
                    }
                    if ((!string.IsNullOrEmpty(NewToken)))
                    {
                        App.Token = NewToken;
                    }

                    String GAZTValidateAndChangePasswordResponseJSON = GAZTValidateAndChangePasswordResponse.Content.ReadAsStringAsync().Result;

                    GAZTValidateAndChangePasswordResponseJSON = JObject.Parse(GAZTValidateAndChangePasswordResponseJSON)["d"].ToString();

                    string GAZTValidateOTPResponseJToken = JObject.Parse(GAZTValidateAndChangePasswordResponseJSON)["Mobile"].ToString();
                    if (string.IsNullOrEmpty(GAZTValidateOTPResponseJToken) != true)
                    {
                        MobileNumber = GAZTValidateOTPResponseJToken;
                    }
                    else
                    {
                        throw new Exception("Invalid Response");
                    }
                }

                return MobileNumber;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public static async Task<bool> GAZTGetOTPForEmail(String Lang, String Tin, string CurrentEmail, string NewEmail)
        {
            TaxPayerProfile TP = null;
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

                    if ((0 == String.Compare(NewToken, "Invalid Token")))
                    {
                        throw new Exception("Invalid Token");
                    }
                    if ((!string.IsNullOrEmpty(NewToken)))
                    {
                        App.Token = NewToken;
                    }


                    String GAZTValidateOTPResponseJSON = GAZTValidateOTPResponse.Content.ReadAsStringAsync().Result;

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
                    //if ((0 == String.Compare(GAZTValidateOTPResponseJToken.Value<String>(), "Details changed successfully")) || (0 == String.Compare(GAZTValidateOTPResponseJToken.Value<String>(), "تم تغيير التفاصيل بنجاح")))
                    //    TP = JsonConvert.DeserializeObject<TaxPayerProfile>(GAZTValidateOTPResponseJSON);
                    //else
                    //    throw new Exception("Invalid OTP / OTP expired");
                }

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public static async Task<TaxPayerProfile> GAZTValidateOTPForEmail(String Lang, String OTP, String Tin, string CurrentEmail, string NewEmail, string CurrentPassword, string NewPassword)
        {
            TaxPayerProfile TP = null;
            string NewToken = string.Empty;
            try
            {
                HttpClient client = new HttpClient(App.httpClientHandler);

                String url = Constants.GAZTValidateOTPForEmail + "Langz='" + Lang + "',Tin='" + Tin + "',Otp='" + OTP + "',CurrEmail='" + CurrentEmail + "',NewEmail='" + NewEmail + "',CurrMobile='" + "" + "',NewMobile='" + "" + "',CurrPwd='" + CurrentPassword + "',NewPwd='" + NewPassword + "')?$format=json&saml2=disabled&sap-language=" + Lang;
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

                    if ((0 == String.Compare(NewToken, "Invalid Token")))
                    {
                        throw new Exception("Invalid Token");
                    }
                    if ((!string.IsNullOrEmpty(NewToken)))
                    {
                        App.Token = NewToken;
                    }


                    String GAZTValidateOTPResponseJSON = GAZTValidateOTPResponse.Content.ReadAsStringAsync().Result;

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
                    //if ((0 == String.Compare(GAZTValidateOTPResponseJToken.Value<String>(), "Details changed successfully")) || (0 == String.Compare(GAZTValidateOTPResponseJToken.Value<String>(), "تم تغيير التفاصيل بنجاح")))
                    //    TP = JsonConvert.DeserializeObject<TaxPayerProfile>(GAZTValidateOTPResponseJSON);
                    //else
                    //    throw new Exception("Invalid OTP / OTP expired");
                }

                return TP;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<string> GAZTZakatGetPdfUrl(String Lang, String Tin)
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

                return PdfUrl;

            }
            catch (Exception ex)
            {
                //  throw new Exception(ex.Message);
                return null;
            }
        }

        public static async Task<AllCertificate> GAZTGetAllCertificate(String Lang, String Tin)
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
                HttpResponseMessage GAZTGetAllCertificateResponse = await client.GetAsync(uri);

                if (GAZTGetAllCertificateResponse != null)
                {
                    HttpHeaders headers = GAZTGetAllCertificateResponse.Headers;
                    IEnumerable<string> values;
                    if (headers.TryGetValues("token", out values))
                    {
                        NewToken = values.First();
                    }

                    if ((0 == String.Compare(NewToken, "Invalid Token")))
                    {
                        throw new Exception("Invalid Token");
                    }
                    if ((!string.IsNullOrEmpty(NewToken)))
                    {
                        App.Token = NewToken;
                    }
                    String GAZTGetAllCertificateResponseJSON = GAZTGetAllCertificateResponse.Content.ReadAsStringAsync().Result;

                    GAZTGetAllCertificateResponseJSON = JObject.Parse(GAZTGetAllCertificateResponseJSON)["d"].ToString();
                    allCertificate = JsonConvert.DeserializeObject<AllCertificate>(GAZTGetAllCertificateResponseJSON);
                }
                return allCertificate;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static async Task<Dashboard> GAZTGetDashboardData(String Lang, String Tin)
        {
            DateTime dt = DateTime.Now;
            Dashboard dashboardData = new Dashboard();
            // string currentDate = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString() + "T" + dt.Hour.ToString() + ":" + dt.Minute.ToString();

            string NewToken = string.Empty;
            try
            {
                HttpClient client = new HttpClient(App.httpClientHandler);
                // string uri = "https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/ZDSM_TAXPAYER_SRV/HEADERSet?$filter=Tin eq '3300036062'&saml2=disabled  ";
                string uri = Constants.GetDashboardData + Tin + "'" + "&saml2=disabled" + "&$format=json";

                client.DefaultRequestHeaders.Add("Token", App.Token);
                HttpResponseMessage GAZTGetDashboardResponse = await client.GetAsync(uri);

                if (GAZTGetDashboardResponse != null)
                {
                    HttpHeaders headers = GAZTGetDashboardResponse.Headers;
                    IEnumerable<string> values;
                    if (headers.TryGetValues("token", out values))
                    {
                        NewToken = values.First();
                    }

                    if ((0 == String.Compare(NewToken, "Invalid Token")))
                    {
                        throw new Exception("Invalid Token");
                    }
                    if ((!string.IsNullOrEmpty(NewToken)))
                    {
                        App.Token = NewToken;
                    }
                    String GAZTGetDashboardResponseJSON = GAZTGetDashboardResponse.Content.ReadAsStringAsync().Result;

                    GAZTGetDashboardResponseJSON = JObject.Parse(GAZTGetDashboardResponseJSON)["d"].ToString();
                    dashboardData = JsonConvert.DeserializeObject<Dashboard>(GAZTGetDashboardResponseJSON);
                }
                return dashboardData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static async Task<ForgotPasswordOTP> GAZTFogotPasswordSendOTP(String Lang, String Tin)
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


        public static async Task GAZTForgotPasswordValidateOTP(ForgotPasswordOTP forgotPasswordOTP)
        {
            string url = Constants.ValidateOTP;
            var uri = new Uri(url);
            HttpClient client = new HttpClient(App.httpClientHandler);
            client.DefaultRequestHeaders.Add("X-Requested-With", "X");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            var serilized = JsonConvert.SerializeObject(forgotPasswordOTP);
            HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
            HttpResponseMessage res = await client.PostAsync(uri, contentPost);
            // var detailJson = res.Result.Content.ReadAsStringAsync().Result;
        }

        public static async Task GAZTSendUserNameToEmail(D forgotUserOTP)
        {
            try
            {
                string url = Constants.SendUserNameToEmail;
                var uri = new Uri(url);
                HttpClient client = new HttpClient(App.httpClientHandler);
                client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var serilized = JsonConvert.SerializeObject(forgotUserOTP);
                HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                // var detailJson = res.Result.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {

            }
        }

        public static async Task GAZTChangePassword(ForgotPasswordOTP forgotUserOTP)
        {
            try
            {
                string url = Constants.ChangePassword;
                var uri = new Uri(url);
                HttpClient client = new HttpClient(App.httpClientHandler);
                client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var serilized = JsonConvert.SerializeObject(forgotUserOTP);
                HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                // var detailJson = res.Result.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {

            }
        }

    }
}
