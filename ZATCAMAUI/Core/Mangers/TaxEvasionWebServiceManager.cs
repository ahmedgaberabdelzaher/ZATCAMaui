using System.Net;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using Newtonsoft.Json;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Models;

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
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

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
                catch (JsonReaderException )
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
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

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
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

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

            if (NetworkCheck.IsInternet())
            {
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

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

            if (NetworkCheck.IsInternet())
            {
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

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

            if (NetworkCheck.IsInternet())
            {
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

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

            if (NetworkCheck.IsInternet())
            {
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

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
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

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
                catch (JsonReaderException )
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

            if (NetworkCheck.IsInternet())
            {
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

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
                                    string.Format("\"{0}\"", keyValuePair.Key));
                            }
                            foreach (UploadedDocumentsList uploadedDocumentsList in documentsLists)
                            {
                                string base64Encoded = Convert.ToBase64String(uploadedDocumentsList.DocBinaryInBase64);


                                var valuesTmp = new[]
                                {
                                    new KeyValuePair<string, string>("file[]", base64Encoded)
                                };


                                foreach (var keyValuePair in valuesTmp)
                                {
                                    multipartFormDataContent.Add(new StringContent(keyValuePair.Value),
                                        string.Format("\"{0}\"", keyValuePair.Key));
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

            if (NetworkCheck.IsInternet())
            {
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                    string url = ZATCAConstants.GAZTGetVATSignUpCaseId;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);

                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    HttpResponseMessage res = client.GetAsync(uri).Result;
                    var response = res.Content.ReadAsStringAsync().Result;
                    vATSignUpCaseId = JsonConvert.DeserializeObject<VATSignUpCaseId>(response);
                    return vATSignUpCaseId;
                }
                catch (JsonReaderException )
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

        public async static Task<string> GAZTVATSignUpValidateTinNumberStringResp(string Tin)
        {
            if (NetworkCheck.IsInternet())
            {
                VATSignUp vATSignUp = new VATSignUp();
                string IsIDTypeValidList = string.Empty;
                string NewToken = string.Empty;
                string SignUpCityList = string.Empty;
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    string IDType = string.Empty;
                    string IDNumber = string.Empty;
                    string DBO = string.Empty;
                    char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);
                    string url = ZATCAConstants.GAZTVATSignUpValidateId + "(Tin='" + Tin + "',Idtype='" + IDType + "',Idnum='" + IDNumber + "',Country='',PassExpDt='',TaxpDob='" + DBO + "')?sap-language=" + lang + "&$format=json&saml2=enabled";
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
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
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

                catch (JsonReaderException )
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

        public async static Task<string> GAZTVATSignUpValidateIDTypesStringResp(string IDType, string IDNumber, string DBO)
        {
            if (NetworkCheck.IsInternet())
            {
                VATSignUp vATSignUp = new VATSignUp();
                string IsIDTypeValidList = string.Empty;
                string NewToken = string.Empty;
                string SignUpCityList = string.Empty;
                try
                {
                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                    char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);
                    string url = ZATCAConstants.GAZTVATSignUpValidateId + "(Tin='',Idtype='" + IDType + "',Idnum='" + IDNumber + "',Country='',PassExpDt='',TaxpDob='" + DBO + "')?sap-language=" + lang + "&$format=json&saml2=enabled";
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
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
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

                catch (JsonReaderException )
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

                    char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);
                    string url = ZATCAConstants.GAZTVATSignUpValidateId + "(Tin='',Idtype='" + IDType + "',Idnum='" + IDNumber + "',Country='',PassExpDt='',TaxpDob='" + DBO + "')?sap-language=" + lang + "&$format=json&saml2=enabled";
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
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        string SignUpCityList = await VATSignUpIdValidateObject.Content.ReadAsStringAsync();
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


        public static async Task<VATSignUpData> GAZTGetVATSignUpCityListForSignup()
        {
            if (NetworkCheck.IsInternet())
            {
                VATSignUpData vATSignUpData = new VATSignUpData();
                string NewToken = string.Empty;
                try
                {
                    char lang = WebServiceManager.GetLangZParameter();

                    HttpClientHandler crmSignUphttpClientHandler = new HttpClientHandler();
                    crmSignUphttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                    HttpClient client = new HttpClient(crmSignUphttpClientHandler);

                    string url = ZATCAConstants.GAZTGetVATSignUpCityAndRegionList + "dropdown_headerSet(Spras='" + lang + "',Land1='',Bland='',Cityc='')?&$expand=city_dropdownSet,country_dropdownSet,State_dropdownSet&saml2=enabled&$format=json";
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

                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }

                        string signUpData = await VATSignUpCountryRegionCityList.Content.ReadAsStringAsync();

                        DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(VATSignUpData));
                        MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(signUpData));
                        stream.Position = 0;
                        vATSignUpData = (VATSignUpData)jsonSerializer.ReadObject(stream);

                        return vATSignUpData;

                    }
                    else
                    {
                        return null;
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
        }

        public static async Task<string> GAZTCreateVATSignUpFirst(VATSignUpSubmit vATSignUpSubmit)
        {
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    string LangZ = WebServiceManager.GetLangZParameterAREN();



                    VATSignUpSubmit vatSignUpSubmit = new VATSignUpSubmit();
                    string url = ZATCAConstants.GAZTGetCreateVATSignUp + LangZ;
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
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

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

        #endregion
    }
}
