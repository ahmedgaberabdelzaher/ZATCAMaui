using GAZT.Helper;
using GAZT.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
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

                                XmlNode node = xmlDoc.SelectSingleNode("/soap:Envelope/soap:Body/ns2:loginValidationResponse/return", xmlnsManager);
                                AuthenticationResult = node.InnerText;
                                if(App.IsArabic)
                                {
                                    string tin = App.TP.Tin;
                                    tin = tin + " - " + "User does not exist";
                                    if (AuthenticationResult.Equals("User authentication failed"))
                                    {
                                        AuthenticationResult = AppResources.UserAuthenticationFailed;
                                    }
                                    else if (AuthenticationResult.Equals(tin))
                                    {
                                        AuthenticationResult = AppResources.UserDoesNotExist;
                                    }
                                    else
                                    {
                                        AuthenticationResult = AppResources.UserAccountLocked;
                                    }
                                }

                               
                            }
                            else
                                throw new Exception("SOAP request failed");
                        }
                    }
                }
                else
                    throw new Exception("SOAP request not created");

                return AuthenticationResult;
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
        public static async Task<String> GAZTSendAndReceiveOTP(String Lang, String UserId)
        {
            String OTPSentConfirmation = String.Empty;
            try
            {
                HttpClient client = new HttpClient(new System.Net.Http.HttpClientHandler());
                String url = Constants.GAZTSendAndReceiveOTP + Lang + "',Userid='" + UserId + "',Otp='')?$format=json";
                var uri = new Uri(url);
                HttpResponseMessage GAZTSendAndReceiveOTPResponse = await client.GetAsync(uri);

                if(GAZTSendAndReceiveOTPResponse != null)
                {
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
            try
            {
                HttpClient client = new HttpClient(new System.Net.Http.HttpClientHandler());

                String url = Constants.GAZTValidateOTP + Lang + "',Userid='" + UserId + "',Otp='" + OTP + "')?$format=json";
                var uri = new Uri(url);

                HttpResponseMessage GAZTValidateOTPResponse = await client.GetAsync(uri);

                if(GAZTValidateOTPResponse!=null)
                {
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


        public static async Task<TaxPayerProfile> GAZTValidateOTPForMobileNumber(String Lang,String OTP, String Tin, string CurrentMobileNumber, string NewMobileNumber)
        {
            TaxPayerProfile TP = null;
            try
            {
                HttpClient client = new HttpClient(new System.Net.Http.HttpClientHandler());

                String url = Constants.GAZTValidateOTPForMobile +"Langz='" + Lang + "',Tin='" + Tin + "',Otp='" +OTP+ "',CurrEmail='" + "" + "',NewEmail='" + "" + "',CurrMobile='" + CurrentMobileNumber + "',NewMobile='" + NewMobileNumber + "',CurrPwd='" + "" + "',NewPwd='" + "')?$format=json&sap-language=" + Lang;
                var uri = new Uri(url);

                HttpResponseMessage GAZTValidateOTPResponse = await client.GetAsync(uri);

                if (GAZTValidateOTPResponse != null)
                {
                    String GAZTValidateOTPResponseJSON = GAZTValidateOTPResponse.Content.ReadAsStringAsync().Result;

                    GAZTValidateOTPResponseJSON = JObject.Parse(GAZTValidateOTPResponseJSON)["d"].ToString();

                    string GAZTValidateOTPResponseJToken = JObject.Parse(GAZTValidateOTPResponseJSON)["Result"].ToString();
                    if(GAZTValidateOTPResponseJToken== "Details Changed Successfully" || GAZTValidateOTPResponseJToken.ToString() == "تم تغيير التفاصيل بنجاح")
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

        public static async Task<bool> GAZTValidateMobileNumber(String Lang, String Tin,string CurrentMobileNumber,string NewMobileNumber)
        {
            TaxPayerProfile TP = null;
            bool result = false;
            try
            {
                HttpClient client = new HttpClient(new System.Net.Http.HttpClientHandler());

                String url = Constants.GaZTVerifyMobileNumber + "Langz='" + Lang + "',Tin='" + Tin + "',Otp='" +""+ "',CurrEmail='" +""+ "',NewEmail='" +""+ "',CurrMobile='" + CurrentMobileNumber + "',NewMobile='" + NewMobileNumber + "',CurrPwd='" +""+"',NewPwd='" + "')?$format=json&sap-language=" + Lang;
                var uri = new Uri(url);

                HttpResponseMessage GAZTValidateMobileNumberResponse = await client.GetAsync(uri);

                if (GAZTValidateMobileNumberResponse != null)
                {
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
            try
            {
                HttpClient client = new HttpClient(new System.Net.Http.HttpClientHandler());

                String url = Constants.GAZTValidateAndChangePassword + "Langz='" + Lang + "',Tin='" + Tin + "',Otp='" + "" + "',CurrEmail='" + "" + "',NewEmail='" + "" + "',CurrMobile='" +""+ "',NewMobile='" +""+ "',CurrPwd='" + CurrentPassword + "',NewPwd='" +NewPassword+"')?$format=json&sap-language=" + Lang;
                var uri = new Uri(url);

                HttpResponseMessage GAZTValidateAndChangePasswordResponse = await client.GetAsync(uri);

                if (GAZTValidateAndChangePasswordResponse != null)
                {
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
            string currentDate=dt.Year.ToString() +"-"+dt.Month.ToString()+"-"+dt.Day.ToString()+"T"+dt.Hour.ToString()+":"+dt.Minute.ToString();
           // 2007 - 01 - 01T00: 00
            try
            {
                HttpClient client = new HttpClient(new System.Net.Http.HttpClientHandler());
               // String url = "https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/ZDP_IT_CORRES_MOB_NEW_SRV/Corr_detSet?$filter=Gpartz  eq  '3300057436'  and Langz   eq 'EN'  and  Begdaz eq   datetime'2007-01-01T00:00'  and Enddaz eq datetime'2019-10-13T11:12'  and  ObligFlagz eq 'I'  and  Auditor  eq  ''   and  TaxtpFg  eq  'VAT'  and  UserTin   eq  ''&$format=json";
                String url = Constants.GAZTGetPdf + "Gpartz eq'" + Tin + "'and Langz eq'" + Lang + "'and  Begdaz eq datetime'" + "2007-01-01T00:00" + "'and Enddaz eq datetime'" + currentDate + "'and  ObligFlagz eq'" + "I" + "'and  Auditor  eq'" + "" + "'and  TaxtpFg  eq '" + "VAT" + "'and  UserTin   eq'"+""+"'&$format=json";
                var uri = new Uri(url);

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


        public static async Task<string> GAZTGetTaxPayerProfile(String Tin,String Lang)
        {
            TaxPayerProfile TP = null;
            String MobileNumber = string.Empty;
            string PdfUrl = string.Empty;
            try
            {
                HttpClient client = new HttpClient(new System.Net.Http.HttpClientHandler());
                String url = Constants.GAZTGetTP + "='" + Tin + "',Langz='" + Lang + "')" + "?&$expand=TPOC_LIST&$format=json";
                var uri = new Uri(url);

                HttpResponseMessage GAZTValidateAndChangePasswordResponse = await client.GetAsync(uri);

                if (GAZTValidateAndChangePasswordResponse != null)
                {
                    String GAZTValidateAndChangePasswordResponseJSON = GAZTValidateAndChangePasswordResponse.Content.ReadAsStringAsync().Result;

                    GAZTValidateAndChangePasswordResponseJSON = JObject.Parse(GAZTValidateAndChangePasswordResponseJSON)["d"].ToString();

                    string GAZTValidateOTPResponseJToken = JObject.Parse(GAZTValidateAndChangePasswordResponseJSON)["Mobile"].ToString();
                    if (string.IsNullOrEmpty(GAZTValidateOTPResponseJToken)!=true)
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


        public static async Task<bool> GAZTGetOTPForEmail(String Lang,String Tin, string CurrentEmail, string NewEmail)
        {
            TaxPayerProfile TP = null;
            bool result = false;
            try
            {
                HttpClient client = new HttpClient(new System.Net.Http.HttpClientHandler());

                String url = Constants.GAZTGetOTPForEmail + "Langz='" + Lang + "',Tin='" + Tin + "',Otp='" + "" + "',CurrEmail='" + CurrentEmail + "',NewEmail='" + NewEmail + "',CurrMobile='" + "" + "',NewMobile='" + "" + "',CurrPwd='" + "" + "',NewPwd='" + "')?$format=json&sap-language=" + Lang;
                var uri = new Uri(url);

                HttpResponseMessage GAZTValidateOTPResponse = await client.GetAsync(uri);

                if (GAZTValidateOTPResponse != null)
                {
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



        public static async Task<TaxPayerProfile> GAZTValidateOTPForEmail(String Lang, String OTP, String Tin, string CurrentEmail, string NewEmail,string CurrentPassword,string NewPassword)
        {
            TaxPayerProfile TP = null;
            try
            {
                HttpClient client = new HttpClient(new System.Net.Http.HttpClientHandler());

                String url = Constants.GAZTValidateOTPForEmail + "Langz='" + Lang + "',Tin='" + Tin + "',Otp='" + OTP + "',CurrEmail='" + CurrentEmail + "',NewEmail='" + NewEmail + "',CurrMobile='" + "" + "',NewMobile='" + "" + "',CurrPwd='" + CurrentPassword + "',NewPwd='" + NewPassword+"')?$format=json&sap-language=" + Lang;
                var uri = new Uri(url);

                HttpResponseMessage GAZTValidateOTPResponse = await client.GetAsync(uri);

                if (GAZTValidateOTPResponse != null)
                {
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
                HttpClient client = new HttpClient(new System.Net.Http.HttpClientHandler());
                //String url = Constants.GAZTGetPdf + "Gpartz eq'" + Tin + "'and Langz eq'" + Lang + "'and  Begdaz eq datetime'" + "2007-01-01T00:00" + "'and Enddaz eq datetime'" + currentDate + "'and  ObligFlagz eq'" + "I" + "'and  Auditor  eq'" + "" + "'and  TaxtpFg  eq '" + "VAT" + "'and  UserTin   eq'" + "" + "'&$format=json";
                string ZakatURL = Constants.GAZTZakatGetPdf + Tin + "'and Langz eq'" + Lang + "'and UserTin eq'" + "" + "'and Begdaz eq datetime'" + "2007-01-01T00:00" + "'and Enddaz eq datetime'" + currentDate + "'and ObligFlagz eq'" + "" + "'and Auditor eq '" + "" + "'&sap-client=100&sap-language='" + Lang + "'&$format=json";

                 var uri = new Uri(ZakatURL);

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
    }
}
