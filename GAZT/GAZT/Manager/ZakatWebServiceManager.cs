using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
    public static class ZakatWebServiceManager
    {
        #region Zakat
        public async static Task<ZakatInstalmentPlanRequestListModel> GAZTGetZakatInstalmentPlanRequestList(string zuser, string fbguid, string euser1)
        {

            if (CrossConnectivity.Current.IsConnected)
            {
                ZakatInstalmentPlanRequestListModel _ZakatInstalmentPlanRequestList = new ZakatInstalmentPlanRequestListModel();
                string NewToken = string.Empty;
                try
                {

                    string callServ = "IPRA";
                    zuser = "";
                    euser1 = "null";
                    string auditor = "null";
                    string euser2 = "null";
                    string euser3 = "null";
                    string euser4 = "null";
                    string euser5 = "null";
                    fbguid = "";
                    string userTyp = "TP";

                    Char lang = WebServiceManager.GetLangZParameter();
                    String url = Constants.ZakatListOfInstalmentplanRequestUrl + "CallServ='" + callServ + "',HostName='" + "',Bpnum='" + App.LoginDataRetrieved.TIN + "',Zuser='" + "'," +
                       "Auditor='" + auditor + "'," +
                     "Lang='" + lang + "',Euser1='" + euser1 + "',Euser2='" + euser2 + "',Euser3='" + euser3 + "'," +
                     "Euser4='" + euser4 + "',Euser5='" + euser5 + "',Fbguid='" + fbguid + "',UserTin='" + "',Fbnum='" + "',UserTyp='" + userTyp + "')?$expand=WorklistSet,AuthServSet,EvtNotif12Set,EvtNotif1Set,RevokeListSet&$format=json";
                    HttpResponseMessage GAZTzakatInstalmentDataResponse = await GetServiceManager.MakeGetAPICall(url, false, "");



                    if (GAZTzakatInstalmentDataResponse != null)
                    {
                        if (GAZTzakatInstalmentDataResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTzakatInstalmentDataResponse.Headers;
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
                        var _zakatInstalmentRequestData = GAZTzakatInstalmentDataResponse.Content.ReadAsStringAsync().Result;
                        _ZakatInstalmentPlanRequestList = JsonConvert.DeserializeObject<ZakatInstalmentPlanRequestListModel>(_zakatInstalmentRequestData);

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
                    
                    
                    App.IsSessionExpired = true;
                    return null;
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }


        public async static Task<ZakatInstalmentPlanRequestListModel> GAZTGetZakatRevokeList(string zuser, string fbguid, string euser1)
        {

            if (CrossConnectivity.Current.IsConnected)
            {
                ZakatInstalmentPlanRequestListModel _zakatRevokeList = new ZakatInstalmentPlanRequestListModel();
                string NewToken = string.Empty;
                try
                {

                    string callServ = "IPRR";
                    zuser = "";
                    euser1 = "null";
                    string auditor = "null";
                    string euser2 = "null";
                    string euser3 = "null";
                    string euser4 = "null";
                    string euser5 = "null";
                    fbguid = "";
                    string userTyp = "TP";


                    Char lang = WebServiceManager.GetLangZParameter();
                    String url = Constants.ZakatRevokeRequestListUrl + "CallServ='" + callServ + "',HostName='" + "',Bpnum='" + App.LoginDataRetrieved.TIN + "',Zuser='" + "'," +
                       "Auditor='" + auditor + "'," +
                     "Lang='" + lang + "',Euser1='" + euser1 + "',Euser2='" + euser2 + "',Euser3='" + euser3 + "'," +
                     "Euser4='" + euser4 + "',Euser5='" + euser5 + "',Fbguid='" + fbguid + "',UserTin='" + "',Fbnum='" + "',UserTyp='" + userTyp + "')?$expand=WorklistSet,AuthServSet,EvtNotif12Set,EvtNotif1Set,RevokeListSet&$format=json";
                    HttpResponseMessage GAZTzakatRevokeInstalmentDataResponse = await GetServiceManager.MakeGetAPICall(url, false, "");


                    if (GAZTzakatRevokeInstalmentDataResponse != null)
                    {
                        if (GAZTzakatRevokeInstalmentDataResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTzakatRevokeInstalmentDataResponse.Headers;
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

                        String _zakatRevokeInstalmentRequestData = GAZTzakatRevokeInstalmentDataResponse.Content.ReadAsStringAsync().Result;
                        _zakatRevokeList = JsonConvert.DeserializeObject<ZakatInstalmentPlanRequestListModel>(_zakatRevokeInstalmentRequestData);

                        if (!string.IsNullOrEmpty(_zakatRevokeInstalmentRequestData) && _zakatRevokeList.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_zakatRevokeInstalmentRequestData);
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
                    return _zakatRevokeList;
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



        public async static Task<SummaryDisplayModel> GAZTGetZakatRequestDisplayData(string fbnum, string status)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                SummaryDisplayModel _zakatRequestDisplayModel = new SummaryDisplayModel();
                string NewToken = string.Empty;
                try
                {
                    var summaryInputs = await GAZTGetZakatSummaryInputData(fbnum, status);
                    string euser = "00000000000000000000";
                    string fbguid = summaryInputs.d.Fbguid;
                    Char lang = WebServiceManager.GetLangZParameter();
                    String url = Constants.ZakatRequestDisplayUrl + "Tin='',Euser='00000000000000000000',Langz='EN',Fbguid='" + fbguid + "'," +
                        "Fbnum='" + fbnum + "',FormMode='S')?$expand=AttachSet,NotesSet,FnDtlSet&$format=json";
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
                        _zakatRequestDisplayModel = JsonConvert.DeserializeObject<SummaryDisplayModel>(_zakatDisplayRequestData);



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
        private async static Task<ZakatSummaryInputModel> GAZTGetZakatSummaryInputData(string fbnum, string status)
        {
            ZakatSummaryInputModel _zakatSummaryInputModel = new ZakatSummaryInputModel();
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                try
                {
                    string fbtyp = "IPRF";
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.GetZAKATSummaryInputURL + "Euser1='00000000000000000000',Fbguid='" + "',Fbnum='" + fbnum + "',Fbtyp='" + fbtyp + "'," +
                     "Gpart='" + App.LoginDataRetrieved.TIN + "',Lang='" + lang + "',Persl='" + "',Status='" + status + "',TaxOffUid='" + "')?$format=json";
                    var uri = new Uri(url);
                    HttpResponseMessage _zakatSumamryInputResponse = await client.GetAsync(uri);




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
