using System.Net;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using Newtonsoft.Json;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.NewModelAPI;
using ZATCAMAUI.Models.SignUP;

namespace ZATCAMAUI.Core.Mangers
{
    public static class TaxEvasionWebServiceManager
    {
        #region Tax Evasion

        public static async Task<TaxEvasionCategoriesModel> GAZTTaxEvasionGetCategories()
        {
            TaxEvasionCategoriesModel categoriesModel = new TaxEvasionCategoriesModel();

            if (NetworkCheck.IsInternet())
            {
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    string url = ZATCAConstants.GAZTTaxEvasionGetCategories;
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

            if (NetworkCheck.IsInternet())
            {
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    string url = ZATCAConstants.GAZTTaxEvasionSendSms;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);

                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    string langVal = "en";
                    if (App.IsArabic == true)
                    {
                        langVal = "ar";
                    }

                    client.DefaultRequestHeaders.Add("Accept-Language", langVal);

                    var serilized = JsonConvert.SerializeObject(sendSmsModel);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
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

            if (NetworkCheck.IsInternet())
            {
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    string url = ZATCAConstants.GAZTTaxEvasionVerifySms;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);
                    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
                    requestMessage.Headers.Add("Accept", "application/json");
                    requestMessage.Headers.Add("mobile", mobileNumber);
                    var serilized = JsonConvert.SerializeObject(verifySmsModel);

                    requestMessage.Content = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);

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
                catch (Exception ex)
                {


                    errorReponseModel = JsonConvert.DeserializeObject<TaxEvasionErrorReponseModel>(response);
                    if (errorReponseModel.Data.Contains("Invalid code") || errorReponseModel.Data.Contains("«·—„“ €Ì— ’ÕÌÕ"))
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

            if (NetworkCheck.IsInternet())
            {
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    string url = ZATCAConstants.GAZTTaxEvasionGetAllReports;
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
                    requestMessage.Content = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.SendAsync(requestMessage);
                    response = res.Content.ReadAsStringAsync().Result;

                    verifySmsResponse = JsonConvert.DeserializeObject<TaxEvasionReportsModel>(response);

                    return verifySmsResponse;
                }
                catch (Exception ex)
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

            if (NetworkCheck.IsInternet())
            {
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    string url = ZATCAConstants.GAZTTaxEvasionGetUserByMobile;
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
                    requestMessage.Content = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.SendAsync(requestMessage);
                    response = res.Content.ReadAsStringAsync().Result;

                    sendSmsResponse = JsonConvert.DeserializeObject<TaxEvasionUserRegistrationResponseModel>(response);

                    return sendSmsResponse;
                }
                catch (Exception ex)
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

            if (NetworkCheck.IsInternet())
            {
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    string url = ZATCAConstants.GAZTTaxEvasionRegisterUser;
                    var uri = new Uri(url);

                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", App.TaxEvasionToken);

                    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
                    requestMessage.Headers.Add("Accept", "application/json");

                    var serilized = JsonConvert.SerializeObject(registerUserModel);
                    requestMessage.Content = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.SendAsync(requestMessage);
                    response = res.Content.ReadAsStringAsync().Result;

                    registrationResponseModel = JsonConvert.DeserializeObject<TaxEvasionUserRegistrationResponseModel>(response);
                    App.TaxEvasionToken = registrationResponseModel.Data.ApiToken;

                    return registrationResponseModel;
                }
                catch (Exception ex)
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

            if (NetworkCheck.IsInternet())
            {
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    string url = ZATCAConstants.GAZTTaxEvasionGetAllRegions;
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
                catch (Exception ex)
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

            if (NetworkCheck.IsInternet())
            {
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    string url = ZATCAConstants.GAZTTaxEvasionGetAllCities + Convert.ToString(regionId);
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
                catch (Exception ex)
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

            if (NetworkCheck.IsInternet())
            {
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    string url = ZATCAConstants.GAZTTaxEvasionCreateReport;
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
                                string base64Encoded = System.Convert.ToBase64String(uploadedDocumentsList.DocBinaryInBase64);


                                var valuesTmp = new[]
                                {
                                 new KeyValuePair<string, string>("file[]", base64Encoded)
                             };


                                foreach (var keyValuePair in valuesTmp)
                                {
                                    multipartFormDataContent.Add(new StringContent(keyValuePair.Value),
                                        String.Format("\"{0}\"", keyValuePair.Key));
                                }
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
                catch (Exception ex)
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

            if (NetworkCheck.IsInternet())
            {
                try
                {

                    string lang = WebServiceManager.GetLangZParameterAREN();
                    string url = ZATCAConstants.GAZTGetVATSignUpCaseIdURL;  // "https://test-api.zatca.gov.sa/test/third-party/v1/vat-signup/cases?type=1";
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);


                    HttpResponseMessage res = client.GetAsync(uri).Result;
                    var response = res.Content.ReadAsStringAsync().Result;
                    vATSignUpCaseId = JsonConvert.DeserializeObject<VATSignUpCaseId>(response);
                    // return vATSignUpCaseId;
                    //HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    //crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    //string url = "https://test-api.zatca.gov.sa/test/third-party/v1/vat-signup/cases?type=1";
                    //var uri = new Uri(url);
                    //HttpClient client = new HttpClient(crmSignUphttpClientHandler);

                    //client.DefaultRequestHeaders.Add("Accept", "application/json");
                    //HttpResponseMessage res = client.GetAsync(uri).Result;
                    //var response = res.Content.ReadAsStringAsync().Result;
                    //vATSignUpCaseId = JsonConvert.DeserializeObject<VATSignUpCaseId>(response);
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
                catch (Exception ex)
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
            if (NetworkCheck.IsInternet())
            {
                VATSignUp vATSignUp = new VATSignUp();
                string IsIDTypeValidList = string.Empty;
                string NewToken = string.Empty;
                String SignUpCityList = string.Empty;
                try
                {
                    string IDType = string.Empty;
                    string IDNumber = string.Empty;
                    string DBO = string.Empty;

                    string lang = WebServiceManager.GetLangZParameterAREN();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string url = ZATCAConstants.GAZTVATSignUpValidateId;
                    ValidationRequest validationRequest = new ValidationRequest();
                    validationRequest.TIN = Tin;
                    validationRequest.idType = IDType;
                    validationRequest.idNumber = IDNumber;
                    validationRequest.passExpiryDate = DBO;
                    validationRequest.taxpayerBirthDate = DBO;
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;

                    // String url = Constants.GAZTVATSignUpValidateId + "(Tin='',Idtype='" + IDType + "',Idnum='" + IDNumber + "',Country='',PassExpDt='" + DBO + "',TaxpDob='" + DBO + "')?sap-language=" + lang + "&$format=json&saml2=enabled";
                    // var uri = new Uri(url);
                    //  HttpClient client = new HttpClient(crmSignUphttpClientHandler);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    var serilized = JsonConvert.SerializeObject(validationRequest);
                    Console.WriteLine("API for readCaptcha+ ----------------" + serilized);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage VATSignUpIdValidateObject = await client.PostAsync(url, contentPost);
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
                    }
                    return SignUpCityList;
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
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        public async static Task<String> GAZTVATSignUpValidateIDTypesStringResp(string IDType, string IDNumber, string DBO)
        {
            if (NetworkCheck.IsInternet())
            {
                VATSignUp vATSignUp = new VATSignUp();
                string IsIDTypeValidList = string.Empty;
                string NewToken = string.Empty;
                String SignUpCityList = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string lang = WebServiceManager.GetLangZParameterAREN();
                    string url = ZATCAConstants.GAZTVATSignUpValidateId;
                    ValidationRequest validationRequest = new ValidationRequest();
                    //validationRequest.TIN = Tin
                    validationRequest.idType = IDType;
                    validationRequest.idNumber = IDNumber;
                    validationRequest.passExpiryDate = DBO;
                    validationRequest.country = string.Empty;
                    validationRequest.taxpayerBirthDate = DBO;
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;

                    // String url = Constants.GAZTVATSignUpValidateId + "(Tin='',Idtype='" + IDType + "',Idnum='" + IDNumber + "',Country='',PassExpDt='" + DBO + "',TaxpDob='" + DBO + "')?sap-language=" + lang + "&$format=json&saml2=enabled";
                    var uri = new Uri(url);
                    //  HttpClient client = new HttpClient(crmSignUphttpClientHandler);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    var serilized = JsonConvert.SerializeObject(validationRequest);
                    Console.WriteLine("API for readCaptcha+ ----------------" + serilized);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage VATSignUpIdValidateObject = await client.PostAsync(url, contentPost);
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
                        SignUpCityList = VATSignUpIdValidateObject.Content.ReadAsStringAsync().Result;

                        // SignUpCityList = await VATSignUpIdValidateObject.Content.ReadAsStringAsync();
                    }
                    return SignUpCityList;
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
                catch (TimeoutException gex)
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

        public async static Task<String> GAZTVATSignUpValidateIDDeclaration(string IDType, string IDNumber, string DBO)
        {

            if (NetworkCheck.IsInternet())
            {
                VATSignUp vATSignUp = new VATSignUp();
                string IsIDTypeValidList = string.Empty;
                string NewToken = string.Empty;
                String SignUpCityList = string.Empty;
                try
                {

                    string lang = WebServiceManager.GetLangZParameterAREN();
                    TaxpayerInfo TpInfo = new TaxpayerInfo();
                    TpInfo.idNumber = IDNumber;
                    TpInfo.idType = IDType;
                    TpInfo.taxpayerBirthDate = DBO;
                    String url = ZATCAConstants.GAZTVATSignUpValidateIdDeclaration;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    var serilized = JsonConvert.SerializeObject(TpInfo);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage VATSignUpIdValidateObject = await client.PostAsync(url, contentPost);
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
                    }
                    return SignUpCityList;
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
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }


        public async static Task<VATSignUp> GAZTVATSignUpValidateIDTypes(string IDType, string IDNumber, string DBO)
        {
            if (NetworkCheck.IsInternet())
            {
                VATSignUp vATSignUp = new VATSignUp();
                string IsIDTypeValidList = string.Empty;
                string NewToken = string.Empty;
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                    ValidationRequest validationRequest = new ValidationRequest();
                    validationRequest.TIN = App.LoginDataRetrieved.TIN;
                    validationRequest.idType = IDType;
                    validationRequest.idNumber = IDNumber;
                    validationRequest.passExpiryDate = string.Empty;
                    validationRequest.country = string.Empty;
                    validationRequest.taxpayerBirthDate = DBO;


                    char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);
                    String url = ZATCAConstants.GAZTVATSignUpValidateId;
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
                    }
                    return vATSignUp;
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


        public static async Task<VATSignUpData> GAZTGetVATSignUpCityListForSignup()
        {
            if (NetworkCheck.IsInternet())
            {
                VATSignUpData vATSignUpData = new VATSignUpData();
                string NewToken = string.Empty;
                try
                {
                    string lang = WebServiceManager.GetLangZParameterAREN();

                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                    //HttpClient client = new HttpClient(crmSignUphttpClientHandler);
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    String url = ZATCAConstants.GAZTGetVATSignUpCityAndRegionList + lang + "&country=SA";
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
                        //String signUpData = await VATSignUpCountryRegionCityList.Content.ReadAsStringAsync();

                        //DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(VATSignUpData));
                        //MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(signUpData));
                        //stream.Position = 0;
                        //vATSignUpData = (VATSignUpData)jsonSerializer.ReadObject(stream);
                    }

                    return vATSignUpData;
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
                //throw new GAZTNetworkConnectivityIssueException(AppResources.ZZInternetConnectionMessage);
                throw new GAZTInternetException();
            }
        }

        public static async Task<string> GAZTCreateVATSignUpFirst(VATSignUpSubmit vATSignUpSubmit)
        {
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    VATSignUpSubmit vatSignUpSubmit = new VATSignUpSubmit();

                    string url = ZATCAConstants.GAZTGetCreateVATSignUp;
                    var uri = new Uri(url);

                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var lang = UtilityManager.GetLanguageParameter();
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    //client.DefaultRequestHeaders.Add("Authorization", App.Token);

                    var serilized = JsonConvert.SerializeObject(vATSignUpSubmit);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    var detailJson = res.Content.ReadAsStringAsync().Result;
                    return detailJson;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        public static async Task<String> VatSignUP(CreateVatSignUPRequest createVatSignUPRequest)
        {
            if (NetworkCheck.IsInternet())
            {

                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string lang = WebServiceManager.GetLangZParameterAREN();
                    string url = ZATCAConstants.VatSignUPURL; // "https://test-api.zatca.gov.sa/test/third-party/v1/vat-signup/cases";

                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);

                    var serilized = JsonConvert.SerializeObject(createVatSignUPRequest);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(url, contentPost);
                    var detailJson = res.Content.ReadAsStringAsync().Result;
                    return detailJson;

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
    }
}
