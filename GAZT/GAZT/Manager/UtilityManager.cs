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
                    if (0 == String.Compare(GAZTValidateOTPResponseJToken.Value<String>(), "Valid OTP"))
                        TP = JsonConvert.DeserializeObject<TaxPayerProfile>(GAZTValidateOTPResponseJSON);
                    else
                        throw new Exception("Invalid OTP / OTP expired");
                }

                return TP;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
