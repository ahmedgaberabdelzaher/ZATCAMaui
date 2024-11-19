using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Models.VATReviewModel;
using static ZATCAMAUI.Models.ErrorMessage;

namespace ZATCAMAUI.Core.Mangers
{

    public static class VATObjectionWebServiceManager
    {
        #region VATObjection
        public async static Task<VATObjectionListModel> GAZTGetVATObjectionList()
        {
            VATObjectionListModel _vATObjectionListModel = new VATObjectionListModel();

            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    var lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    string taxType = "VT";
                    string url = ZATCAConstants.GAZTGetVATObjectionURL + App.TP.TIN + "&taxType=" + taxType + "&language=" + lang;
                    HttpResponseMessage vATObjectionListResponse = await client.GetAsync(url);
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
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
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
                                string WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
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

        public async static Task<VATObjectionSummaryModel> GAZTGetVATObjectionSummary(string FBGuid)
        {
            VATObjectionSummaryModel _vATObjectionSummaryModel = new VATObjectionSummaryModel();

            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    var lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);


                    String url = "";
                    if (string.IsNullOrEmpty(FBGuid))
                    {
                        url = ZATCAConstants.GetVATObjectionSummaryURL + App.LoginDataRetrieved.TIN + "&language=" + lang + "&application=N";

                    }
                    else
                    {

                        url = ZATCAConstants.GetVATObjectionSummaryURL + App.LoginDataRetrieved.TIN + "&language=" + lang + "&application=N" + "&formBundleNumber=" + FBGuid;
                    }
                    HttpResponseMessage vATObjectionSummaryResponse = await client.GetAsync(url);
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
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        string __vATObjectionSummaryData = await vATObjectionSummaryResponse.Content.ReadAsStringAsync();
                        _vATObjectionSummaryModel = JsonConvert.DeserializeObject<VATObjectionSummaryModel>(__vATObjectionSummaryData);
                        if (!string.IsNullOrEmpty(__vATObjectionSummaryData) && _vATObjectionSummaryModel.d == null)
                        {
                            string errorMessage = WebServiceManager.PrepareErrorMessageByJson(__vATObjectionSummaryData);
                            throw new GAZTVATRegistrationInProcessException(errorMessage);
                        }
                    }
                    return _vATObjectionSummaryModel;
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

        public async static Task<VATObjectionRejectedFormModel> GAZTGetVATObjectionFormRejected(string fbustx, string RvRsn, string rvSubRsn, string UserTypx, string fbnumx, string sopbel)
        {
            VATObjectionRejectedFormModel _vATObjectionRejectedFormModel = new VATObjectionRejectedFormModel();
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    UserTypx = "TP";
                    string formprocx = "ZTAX_VT_REV";
                    var lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);

                    string url = ZATCAConstants.GetVATObjectionRejectedFormURL + App.LoginDataRetrieved.TIN + "&language=" + lang + "&userStatus=" + fbustx + "&reviewReason=" + RvRsn + "&reviewSubReason=" + rvSubRsn + "&sadadBillNumber=" + sopbel + "&formProcess=" + formprocx + "&userType=" + UserTypx + "&formBundleNumber=" + fbnumx;


                    HttpResponseMessage vATObjectionRejectedFormResponse = await client.GetAsync(url);
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
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        string __vATObjectionRejectedFormData = vATObjectionRejectedFormResponse.Content.ReadAsStringAsync().Result;
                        _vATObjectionRejectedFormModel = JsonConvert.DeserializeObject<VATObjectionRejectedFormModel>(__vATObjectionRejectedFormData);
                        if (!string.IsNullOrEmpty(__vATObjectionRejectedFormData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__vATObjectionRejectedFormData);
                            if (errorMesg != null && errorMesg.header != null && errorMesg.header.moreInformation != null && errorMesg.header.moreInformation.errorDetails != null && errorMesg.header.moreInformation.errorDetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.header.moreInformation.errorDetails[0].message;
                                errorMessage += errorMesg.header.moreInformation.errorDetails[1].message;
                                string WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
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

        public async static Task<VATObjectionSecurityAmountModel> GAZTGetVATObjectionSecurityAmount(Decimal disamt, Decimal liaamt, Decimal clramt, string fbnum, string rvrsn, string rvsbrsn)

        {
            VATObjectionSecurityAmountModel _vATObjectionSecurityAmountModel = new VATObjectionSecurityAmountModel();

            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    string amttp = "Partial";
                    var lang = UtilityManager.GetLanguageParameter();
                    Decimal disamtRes = UtilityManager.CleanAndConvertToDecimal(disamt);
                    Decimal liaamtRes = UtilityManager.CleanAndConvertToDecimal(liaamt);
                    Decimal clramtRes = UtilityManager.CleanAndConvertToDecimal(clramt);
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    //String url = ZATCAConstants.GetVATObjectionSecurityURL + App.TP.TIN + "&amountType=" + amttp + "&clearedAmount=" + clramtRes + "&disputedAmount=" + disamtRes + "&totalTaxLiability=" + liaamtRes + "',formBundleNumber='" + fbnum + "',reviewReason='" + rvrsn + "',reviewSubReason='" + rvsbrsn + "',businessPartner = '" + App.LoginDataRetrieved.TIN + "', securityType = '', documentNumber = ''";
                    String url = ZATCAConstants.GetVATObjectionSecurityURL + App.TP.TIN + "&amountType=" + amttp + "&clearedAmount=" + clramtRes + "&disputedAmount=" + disamtRes + "&totalTaxLiability=" + liaamtRes + "&formBundleNumber=" + fbnum + "&reviewReason=" + rvrsn + "&reviewSubReason=" + rvsbrsn + "&businessPartner=" + App.LoginDataRetrieved.TIN + "&securityType=" + "&documentNumber=";
                    HttpResponseMessage vATObjectionSecurityAmountResponse = await client.GetAsync(url);

                    //HttpResponseMessage vATObjectionSecurityAmountResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
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
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        string __vATObjectionSecurityAmountData = vATObjectionSecurityAmountResponse.Content.ReadAsStringAsync().Result;
                        _vATObjectionSecurityAmountModel = JsonConvert.DeserializeObject<VATObjectionSecurityAmountModel>(__vATObjectionSecurityAmountData);
                        if (!string.IsNullOrEmpty(__vATObjectionSecurityAmountData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__vATObjectionSecurityAmountData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                string WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
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

        public async static Task<VATObjectionEnableSubmitModel> GAZTGetVATObjectionEnableSubmit(string statusx, string rvRsn, string rvSubRsn, string rejFb)
        {
            VATObjectionEnableSubmitModel _vATObjectionEnableSubmitModel = new VATObjectionEnableSubmitModel();

            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    char lang = WebServiceManager.GetLangZParameter();
                    string url = ZATCAConstants.GetVATObjectionEnableSubmitURL + "Fbnumx='" + "',Gpartx='" + App.LoginDataRetrieved.TIN + "',Statusx='" + statusx + "'" +
                        ",RvRsn='" + rvRsn + "',RvSubRsn='" + rvSubRsn + "',RejFb='" + rejFb + "')?$format=json";
                    HttpResponseMessage _vATObjectionEnableSubmitResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
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
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        string _vATObjectionEnableSubmitData = _vATObjectionEnableSubmitResponse.Content.ReadAsStringAsync().Result;
                        _vATObjectionEnableSubmitModel = JsonConvert.DeserializeObject<VATObjectionEnableSubmitModel>(_vATObjectionEnableSubmitData);
                        if (!string.IsNullOrEmpty(_vATObjectionEnableSubmitData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_vATObjectionEnableSubmitData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                string WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
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

        public async static Task<VATObjectionValidateTaxpayerModel> GAZTGetVATObjectionValidateTaxPayer(string idnum, string idtype, string passExpDt, string taxpDob)
        {
            VATObjectionValidateTaxpayerModel _vATObjectionValidateTaxpayerModel = new VATObjectionValidateTaxpayerModel();

            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {


                    char lang = WebServiceManager.GetLangZParameter();
                    string url = ZATCAConstants.GetVATObjectionValidateTaxPayerNameURL + "Tin='" + "',Idtype='" + idtype + "',Idnum='" + idnum + "'" +
                        ",Country='" + "',PassExpDt='" + passExpDt + "',TaxpDob='" + taxpDob + "')?$format=json";
                    HttpResponseMessage _vATObjectionValidateTaxpayerResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
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
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        string _VATObjectionValidateTaxpayerData = _vATObjectionValidateTaxpayerResponse.Content.ReadAsStringAsync().Result;
                        _vATObjectionValidateTaxpayerModel = JsonConvert.DeserializeObject<VATObjectionValidateTaxpayerModel>(_VATObjectionValidateTaxpayerData);
                        if (!string.IsNullOrEmpty(_VATObjectionValidateTaxpayerData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_VATObjectionValidateTaxpayerData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                string WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
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
        public static string GetVATObjectionDownloadAck(string fbnum)
        {
            if (NetworkCheck.IsInternet())
            {
                try
                {

                    String Url = string.Empty;
                    Url = ZATCAConstants.GetVATObjectionDownloadAckURL + "&formBundleNumber=" + fbnum;

                    return Url;
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
        public async static Task<VATObjectionGenrateSadadModel> GAZTGetVATObjectionGenrateorRefreshSADAD(SadadGenerationObject sadadGenerationObject)
        {
            VATObjectionGenrateSadadModel _vATObjectionGenrateSadadModel = new VATObjectionGenrateSadadModel();

            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {

                    Char lang = WebServiceManager.GetLangZParameter();
                    String url = ZATCAConstants.GetVATObjectionGenrateorRefreshSADADURL;

                    /*if (sadadGenerationObject.flag)
                    {

                        url = Constants.GetVATObjectionGenrateorRefreshSADADURL + "Fbnum='" + fbnum + "',Gpart='" + App.LoginDataRetrieved.TIN + "',Disamt=" + Disamt + "" +
                       "m,Liaamt=" + Liaamt + "m,Sectp='Sadad',Abrzu=datetime'" + Abrzu + "',Abrzo=datetime'" + Abrzo + "',Flag=true,Secamt=" + Secamt + "m,Security='" + Security + "',Persl='" + Persl + "')?$format=json";
                    }
                    else
                    {
                        url = Constants.GetVATObjectionGenrateorRefreshSADADURL + "Fbnum='" + fbnum + "',Gpart='" + App.LoginDataRetrieved.TIN + "',Disamt=" + Disamt + "" +
                       "m,Liaamt=" + Liaamt + "m,Sectp='Sadad',Abrzu=datetime'" + Abrzu + "',Abrzo=datetime'" + Abrzo + "',Flag=false,Secamt=" + Secamt + "m,Security='" + Security + "',Persl='" + Persl + "')?$format=json";
                    }*/
                    string LangZ = WebServiceManager.GetLangZParameterAREN();
                    HttpClient client = new HttpClient();
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", LangZ);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    var serilized = JsonConvert.SerializeObject(sadadGenerationObject);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    var uri = new Uri(url);
                    HttpResponseMessage _vATObjectionGenrateSadadResponse = await client.PostAsync(uri, contentPost);


                    //HttpResponseMessage _vATObjectionGenrateSadadResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
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
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        string __vATObjectionGenrateSadadData = _vATObjectionGenrateSadadResponse.Content.ReadAsStringAsync().Result;
                        _vATObjectionGenrateSadadModel = JsonConvert.DeserializeObject<VATObjectionGenrateSadadModel>(__vATObjectionGenrateSadadData);
                        if (!string.IsNullOrEmpty(__vATObjectionGenrateSadadData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__vATObjectionGenrateSadadData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                string WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
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

        public async static Task<VATObjectionViewbillModel> GAZTGetVATObjectionViewBill(string opbel, string vtre2)
        {
            VATObjectionViewbillModel _vATObjectionViewbillModel = new VATObjectionViewbillModel();

            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    var lang = UtilityManager.GetLanguageParameter();
                    vtre2 = App.LoginDataRetrieved.TIN;
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    // String url = Constants.GetVATObjectionViewBillURL + "Opbel eq'" + opbel + "' and Vtre2 eq'" + vtre2 + "'&$format=json";
                    string url = ZATCAConstants.GetVATObjectionViewBillURL + App.TP.TIN + "&language=" + lang + "&documentNumber=" + opbel;
                    // HttpResponseMessage _vATObjectionViewbillResponse = await GetServiceManager.MakeGetAPICall(url, false, "");

                    HttpResponseMessage _vATObjectionViewbillResponse = await client.GetAsync(url);
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
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        string _vATObjectionViewbillData = _vATObjectionViewbillResponse.Content.ReadAsStringAsync().Result;
                        _vATObjectionViewbillModel = JsonConvert.DeserializeObject<VATObjectionViewbillModel>(_vATObjectionViewbillData);
                        if (!string.IsNullOrEmpty(_vATObjectionViewbillData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_vATObjectionViewbillData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                string WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
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


        public async static Task<VATObjectionSummaryModel> SaveVatReviewObjection(VatObjectionsRequest _vatObjectionDetails)
        {


            if (NetworkCheck.IsInternet())
            {
                try
                {

                    VATObjectionSummaryModel _vatObjectionResponseObject = new VATObjectionSummaryModel();
                    string LangZ = WebServiceManager.GetLangZParameterAREN();
                    string url = ZATCAConstants.PostVATObjectionURL;
                    var uri = new Uri(url);
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    //HttpClient client = new HttpClient(App.httpClientHandler);
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var lang = UtilityManager.GetLanguageParameter();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);

                    //client.DefaultRequestHeaders.Add("Token", "123");
                    //client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    //client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    //client.DefaultRequestHeaders.Add("Accept", "application/json");
                    var serilized = JsonConvert.SerializeObject(_vatObjectionDetails);

                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = client.PostAsync(uri, contentPost).Result;
                    var _vatObjectionsResponsestr = res.Content.ReadAsStringAsync().Result;
                    _vatObjectionResponseObject = JsonConvert.DeserializeObject<VATObjectionSummaryModel>(_vatObjectionsResponsestr);
                    if (!string.IsNullOrEmpty(_vatObjectionsResponsestr) && _vatObjectionResponseObject.result == null)
                    {
                        WebServiceManager.ErrorMessage = string.Empty;

                        ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_vatObjectionsResponsestr);
                        if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                        {
                            WebServiceManager.ErrorMessage = errorMesg.error.innererror.errordetails[0].message;

                            WebServiceManager.ErrorMessageForVAT = WebServiceManager.ErrorMessage;
                            throw new GAZTVATRegistrationInProcessException(WebServiceManager.ErrorMessage);

                        }

                    }
                    return _vatObjectionResponseObject;

                }
                catch (GAZTVATRegistrationInProcessException ex)
                {

                    throw new GAZTVATRegistrationInProcessException(ex.Message);
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

        public static async Task<VATObjectionSummaryInputModel> GAZTVATObjectionSummaryInputData(string fbNum, string status, string fbtyp)
        {
            VATObjectionSummaryInputModel _vATObjectionSummaryInputModel = new VATObjectionSummaryInputModel();
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var lang = UtilityManager.GetLanguageParameter();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    String url = ZATCAConstants.VATObjectionSummaryInputURL + App.LoginDataRetrieved.TIN + "&authenticationUser1=00000000000000000000&formBundleGUID=undefined&formBundleNumber=" + fbNum +
                        "&formBundleType=" + fbtyp + "&language=" + lang + "&status=" + status;
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
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        string _vATObjectionSummaryInputData = _vATObjectionSummaryInputResponse.Content.ReadAsStringAsync().Result;
                        _vATObjectionSummaryInputModel = JsonConvert.DeserializeObject<VATObjectionSummaryInputModel>(_vATObjectionSummaryInputData);
                        if (!string.IsNullOrEmpty(_vATObjectionSummaryInputData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_vATObjectionSummaryInputData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                string WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
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
    }
}
