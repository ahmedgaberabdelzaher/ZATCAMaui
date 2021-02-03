using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EGAZT.Models.ZakatInstalationModels;
using GAZT.Helper;
using GAZT.Manager;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Xamarin.Forms.Internals;
using static GAZT.ErrorMessage;

namespace EGAZT.Manager
{
    [Preserve(AllMembers = true)]
    public static class OldZakatInstallmentWebServiceManager
    {
        #region Old Zakat Instalment

        public async static Task<OldZakatInstalmentPlanRequestListModel> GAZTGetOldZakatInstalmentPlanRequestList(string callServ, string fbguid, string euser1)
        {

            if (CrossConnectivity.Current.IsConnected)
            {
                OldZakatInstalmentPlanRequestListModel _ZakatInstalmentPlanRequestList = new OldZakatInstalmentPlanRequestListModel();
                string NewToken = string.Empty;
                try
                {


                    euser1 = "null";
                    string auditor = "null";
                    string euser2 = "null";
                    string euser3 = "null";
                    string euser4 = "null";
                    string euser5 = "null";
                    fbguid = "";
                    Char lang = WebServiceManager.GetLangZParameter();
                    String url = Constants.ZakatOldInstalmentsListUrl + "CallServ='" + callServ + "',HostName='" + "',Bpnum='" + App.LoginDataRetrieved.TIN + "',Zuser='" + "'," +
                       "Auditor='" + auditor + "'," +
                     "Lang='" + lang + "',Euser1='" + euser1 + "',Euser2='" + euser2 + "',Euser3='" + euser3 + "'," +
                     "Euser4='" + euser4 + "',Euser5='" + euser5 + "',Fbguid='" + fbguid + "')?$expand=ListSet,AuthServSet&$format=json";
                    HttpResponseMessage GAZTzakatInstalmentDataResponse = await GetServiceManager.MakeGetAPICall(url, false, "");



                    if (GAZTzakatInstalmentDataResponse != null)
                    {
                        if (GAZTzakatInstalmentDataResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            return null;
                        }

                        var _zakatInstalmentRequestData = GAZTzakatInstalmentDataResponse.Content.ReadAsStringAsync().Result;
                        _ZakatInstalmentPlanRequestList = JsonConvert.DeserializeObject<OldZakatInstalmentPlanRequestListModel>(_zakatInstalmentRequestData);

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

        public async static Task<OldZakatRequestDisplayModel> GAZTGetOldZakatRequestDisplayData(string fbnum, string status)
        {



            if (CrossConnectivity.Current.IsConnected)
            {
                OldZakatRequestDisplayModel _zakatRequestDisplayModel = new OldZakatRequestDisplayModel();
                string NewToken = string.Empty;
                try
                {
                    var summaryInputs = await GAZTGetOldZakatSummaryInputData(fbnum, status);
                    //string euser = "00000000000000000000";
                    string fbguid = summaryInputs.d.Fbguid;
                    Char lang = WebServiceManager.GetLangZParameter();
                    String url = Constants.ZakatOldInstalmentsSummarytUrl + "Auditorz='',Taxpayerz='',PeriodKeyz='',Euser='00000000000000000000',Langz='" + lang + "',Fbguid='" + fbguid + "'," +
                        "Fbnumz='',Submitz='',Savez='',UserTin='')?$expand=Off_notesSet,AttDetSet,Z_INVOICE_UI5Set,z_invoiceSet,z_proposedinsSet&$format=json";
                    HttpResponseMessage GAZTzakatDisplayResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
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
                        _zakatRequestDisplayModel = JsonConvert.DeserializeObject<OldZakatRequestDisplayModel>(_zakatDisplayRequestData);



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


        private async static Task<ZakatSummaryInputModel> GAZTGetOldZakatSummaryInputData(string fbnum, string status)
        {
            ZakatSummaryInputModel _zakatSummaryInputModel = new ZakatSummaryInputModel();



            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;

                string Newfbnum = fbnum;


                if (fbnum != "")
                {

                    Newfbnum = "0" + fbnum;
                }

                try
                {
                    string fbtyp = "IPRF";



                    string lang = WebServiceManager.GetLangZParameterAREN();
                    String url = Constants.GetOldZAKATSummaryInputURL + "Euser1='00000000000000000000',Fbguid='" + "',Fbnum='" + Newfbnum + "',Fbtyp='" + fbtyp + "'," +
                     "Gpart='" + App.LoginDataRetrieved.TIN + "',Lang='" + lang + "',Persl='" + "',Status='" + status + "',Dispflag='" + "')?$format=json";
                    HttpResponseMessage _zakatSumamryInputResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
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

        public async static Task<OldZakatRequestDisplayModel> SaveOldZakatInstalmentData(OldZakatInstalmentPlanRequest _zakatInstalmentDetails)
        {


            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {

                    OldZakatRequestDisplayModel _zakatResponseObject = new OldZakatRequestDisplayModel();
                    string LangZ = WebServiceManager.GetLangZParameterAREN();
                    String url = Constants.GetOldZAKATPostdata;
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
                    _zakatResponseObject = JsonConvert.DeserializeObject<OldZakatRequestDisplayModel>(_zakatReturnDetailsDesponsestr);
                    if (!string.IsNullOrEmpty(_zakatReturnDetailsDesponsestr) && _zakatResponseObject.d == null)
                    {
                        WebServiceManager.ErrorMessage = string.Empty;
                        ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatReturnDetailsDesponsestr);

                        if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                        {
                            string errorMessage = string.Empty;
                            errorMessage = errorMesg.error.innererror.errordetails[0].message;
                            errorMessage += errorMesg.error.innererror.errordetails[1].message;
                            String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                            errorMessage = WithReplacedString;
                            throw new GAZTVATRegistrationInProcessException(errorMessage);
                        }

                    }
                    return _zakatResponseObject;
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
    }
}
