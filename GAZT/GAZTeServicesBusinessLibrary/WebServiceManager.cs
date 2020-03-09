using GAZTeServicesBusinessLibrary.GAZTExceptions;
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
using System.Xml;
using System.Xml.XPath;

namespace GAZTeServicesBusinessLibrary
{
    public static class WebServiceManager
    {
        private static HttpClientHandler httpClientHandler = null;
        public static bool IsArabic = true;
        public static string Token = string.Empty;
       
        public static void InitialiseWebServiceManager()
        {
            try
            {
                httpClientHandler = new HttpClientHandler();
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            }
            catch (Exception ex)
            {
            }
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

        public static string GAZTAuthenticateTIN(string UserName, string Password, string DeviceId, string CurrentAttempt, string lang)
        {
            InitialiseWebServiceManager();

            if (CrossConnectivity.Current.IsConnected)
            {
                string AuthenticationResult = String.Empty;
                string Message = string.Empty;
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
                                    Token = node.ChildNodes[0].InnerText;
                                    
                                    if ((0 == String.Compare(Token, "User does not exist")))
                                    {
                                        throw new GAZTUserDoesNotExistException();
                                    }
                                    if ((0 == String.Compare(Token, "User authentication failed")))
                                    {
                                        throw new GAZTUserAuthenticationFailedException();
                                    }
                                    if ((0 == String.Compare(Token, "Authentication failed. Password locked")))
                                    {
                                        throw new GAZTPasswordLockedException();
                                    }
                                    if ((0 == String.Compare(Token, "User is not currently valid")))
                                    {
                                        throw new GAZTUserCurrentlyInvalidException();
                                    }
                                    if ((0 == String.Compare(Token, "User account locked")))
                                    {
                                        throw new GAZTUserAccountLockedException();
                                    }
                                    if ((0 == String.Compare(Token, "Password is locked. Invalid attempts")))
                                    {
                                        throw new GAZTPasswordIsLockedDueToInvalidAttemptsException();
                                    }
                                    if ((0 == String.Compare(Token, "Taxpayer's account is not active with GAZT.")))
                                    {
                                        throw new GAZTTaxpayersAccountInActiveWithGAZTException();
                                    }
                                    if ((0 == String.Compare(Token, "Wrong entering for the TIN or the Email")))
                                    {
                                        throw new GAZTWrongTINOrEmailException();
                                    }
                                    if ((0 == String.Compare(Token, "Wrong password")))
                                    {
                                        throw new GAZTWrongPasswordException();
                                    }
                                    if ((0 == String.Compare(Token, "The account is locked for 60 minutes after the last login attempt")))
                                    {
                                        throw new GAZTAccountLockedFor60MinutesAfterLastLoginAttemptException("test");
                                    }

                                    if ((0 == String.Compare(Token, "Incomplete")) || (0 == String.Compare(Token, "Deregister - Death")) || (0 == String.Compare(Token, "Deregister - Bankruptcy")) || (0 == String.Compare(Token, "Deregister - Liquidation")) || (0 == String.Compare(Token, "Deregister - Merger")) || (0 == String.Compare(Token, "Deregister - Acquisition")) || (0 == String.Compare(Token, "Suspension - Bankruptcy")) || (0 == String.Compare(Token, "Suspension - Liquidation/Close")) || (0 == String.Compare(Token, "Deregister - Close")) || (0 == String.Compare(Token, "Deregister - Company-Establish")) || (0 == String.Compare(Token, "Suspension - Est. to Company")))
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
                throw new GAZTInternetException("Network Issue");
            }
        }

        private static List<string> GAZTGetAllTINs(string UserName)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                String GAZTGetTINsResponseResult = String.Empty;
                List<String> TINs = null;
                try
                {
                    HttpClient client = new HttpClient(httpClientHandler);
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
                        TINs = JsonConvert.DeserializeObject<List<String>>(GAZTGetTINsResponseResult);

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

        public static TaxPayerProfile GAZTGetTaxPayerProfile(string TIN, string Lang)
        {
            TaxPayerProfile profile = null;

            if (CrossConnectivity.Current.IsConnected)
            {
                string MobileNumber = string.Empty;
                string PdfUrl = string.Empty;
                string NewToken = string.Empty;

                try
                {
                    HttpClient client = new HttpClient(httpClientHandler);
                    String url = Constants.GAZTGetTP + "='" + TIN + "',Langz='" + Lang + "')" + "?&$expand=TPOC_LIST&saml2=disabled&$format=json";
                    client.DefaultRequestHeaders.Add("Token", Token);
                    Uri uri = new Uri(url);

                    HttpResponseMessage GAZTGetTaxPayerProfileResponseJSON = client.GetAsync(uri).Result;

                    if (GAZTGetTaxPayerProfileResponseJSON != null)
                    {
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

                            Token = NewToken;
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

                    HttpClient client = new HttpClient(httpClientHandler);
                    client.DefaultRequestHeaders.Add("Token", Token);

                    string uri = Constants.GetDashboardData + TIN + "'" + "&saml2=disabled" + "&$format=json";
                    HttpResponseMessage GAZTGetDashboardResponse = client.GetAsync(uri).Result;

                    if (GAZTGetDashboardResponse != null)
                    {
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
                            Token = NewToken;
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
    }
}
