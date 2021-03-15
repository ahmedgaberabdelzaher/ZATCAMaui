using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EGAZT.Models;
using GAZT.Helper;
using GAZT.Manager;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Connectivity;
using Xamarin.Forms.Internals;
using static GAZT.ErrorMessage;

namespace EGAZT.Manager
{
    [Preserve(AllMembers = true)]
    public static class TINDeregistrationWebServiceManager
    {
        #region TIN Deregistration
        public async static Task<TinDeregistrationResponseModel> GaztTinDeregistrationNewRequestData(TinDeregistrationResponseModel tinDeregistrationResponseModel)
        {
            TinDeregistrationResponseModel _tinDeregistrationResponseModel = new TinDeregistrationResponseModel();

            if (CrossConnectivity.Current.IsConnected)
            {

                string NewToken = string.Empty;
                try
                {
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.TinDeregistrationNewRequestUrl + "(Auditorz='',ADegister='1',Taxpayerz='" + App.LoginDataRetrieved.TIN + "',FormGuid='',RegIdz='',PeriodKeyz='',Submitz='',Savez='',Fbnumz='',Langz='',OfficerUidz='',Approvez='" + tinDeregistrationResponseModel.Approvez + "',Rejectz='" + tinDeregistrationResponseModel.Rejectz + "',CreateTxAssesz='')?&$expand=AttDetSet,Off_notesSet,OutletSet,PermitSet,returnSet,Permit_TableSet&$format=json";
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

                    var uri = new Uri(url);

                    HttpResponseMessage _tinDeregNewRequestResponse = await client.GetAsync(uri);

                    if (_tinDeregNewRequestResponse != null)
                    {
                        if (_tinDeregNewRequestResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _tinDeregNewRequestResponse.Headers;
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

                        String _responseData = _tinDeregNewRequestResponse.Content.ReadAsStringAsync().Result;
                        if (_tinDeregNewRequestResponse.StatusCode == HttpStatusCode.BadRequest)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_responseData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorCode = errorMesg.error.innererror.errordetails[0].code;

                                WebServiceManager.ErrorMessageForUnlockAccount = errorMesg.error.innererror.errordetails[0].message;

                                if (errorCode.Contains("206"))
                                {
                                    WebServiceManager.ErrorMessageForUnlockAccount = "206";
                                }
                                else if (errorCode.Contains("112"))
                                {
                                    WebServiceManager.ErrorMessageForUnlockAccount = "112";
                                }

                                String WithReplacedString = WebServiceManager.ErrorMessageForUnlockAccount.Replace("An exception was raised", string.Empty);
                                WebServiceManager.ErrorMessageForUnlockAccount = WithReplacedString;
                                throw new GAZTErrorException(WebServiceManager.ErrorMessageForUnlockAccount);
                            }
                        }
                        else if (!string.IsNullOrEmpty(_responseData))
                        {
                            _responseData = JObject.Parse(_responseData)["d"].ToString();
                            _tinDeregistrationResponseModel = JsonConvert.DeserializeObject<TinDeregistrationResponseModel>(_responseData);
                            if (_tinDeregistrationResponseModel == null)
                            {
                                throw new GAZTErrorException(AppResources.ZZSomethingwentwrong);
                            }
                        }
                        else
                        {
                            throw new GAZTErrorException(AppResources.ZZSomethingwentwrong);
                        }
                    }

                    return _tinDeregistrationResponseModel;
                }
                catch (GAZTErrorException ex)
                {
                    throw new GAZTErrorException(ex.Message);
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

        private static String ConvertDateFormat(DateTime newDate)
        {
            string ConvertedDate = string.Empty;
            long ticks = newDate.Ticks - new DateTime(1970, 1, 1).Ticks;
            TimeSpan span = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
            string unixTime = span.TotalSeconds.ToString("N0");
            unixTime = unixTime.Replace(",", "");
            ConvertedDate = "" + "/Date(" + unixTime + ")/";

            long unixTimestamp = ((long)(newDate.Subtract(new DateTime(1970, 1, 1))).TotalSeconds);

            unixTimestamp = unixTimestamp * 1000;

            ConvertedDate = "" + "/Date(" + unixTimestamp + ")/";
            return ConvertedDate;
        }


        public static async Task<string> GaztTinDeregistrationSubmitRequestData(TinDeregistrationResponseModel tinDeregistrationResponseModel)
        {
            string TinDeregResponseJson = string.Empty;
            TinDeregistrationSendResponseModel tinDeregistrationSendResponseModel = new TinDeregistrationSendResponseModel();
            try
            {
                ObservableCollection<OutletSetResult> AllOutlets = new ObservableCollection<OutletSetResult>(tinDeregistrationResponseModel.OutletSet.Results);
                List<PermitSetResult> allPermitTypes = new List<PermitSetResult>(tinDeregistrationResponseModel.PermitSet.Results);

                foreach (OutletSetResult outletInfo in AllOutlets)
                {
                    if (outletInfo.AOutletEffDtTb != null && !outletInfo.AOutletEffDtTb.Contains("/Date("))
                        outletInfo.AOutletEffDtTb = ConvertDateFormat(Convert.ToDateTime(outletInfo.AOutletEffDtTb));
                    outletInfo.AOutletEffDtCTb = "G";
                }
                foreach (PermitSetResult permitInfo in allPermitTypes)
                {
                    permitInfo.APermitDobTb = null;
                    if ((!string.IsNullOrEmpty(permitInfo.APermitEffDtTb) && !permitInfo.APermitEffDtTb.Contains("/Date(")))
                        permitInfo.APermitEffDtTb = ConvertDateFormat(Convert.ToDateTime(permitInfo.APermitEffDtTb));
                    permitInfo.APermitEffDtCTb = "G";
                    if (!permitInfo.APermitValfrDtTb.Contains("/Date("))
                        permitInfo.APermitValfrDtTb = ConvertDateFormat(Convert.ToDateTime(permitInfo.APermitValfrDtTb));
                    permitInfo.APermitValfrDtCTb = "G";

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            tinDeregistrationSendResponseModel.Metadata = tinDeregistrationResponseModel.Metadata;
            tinDeregistrationSendResponseModel.Assignme = tinDeregistrationResponseModel.Assignme;

            tinDeregistrationSendResponseModel.Caseid = tinDeregistrationResponseModel.Caseid;
            tinDeregistrationSendResponseModel.Xvoidz = tinDeregistrationResponseModel.Xvoidz;
            tinDeregistrationSendResponseModel.TinInPrcFg = tinDeregistrationResponseModel.TinInPrcFg;
            tinDeregistrationSendResponseModel.Taxpayerz = tinDeregistrationResponseModel.Taxpayerz;
            tinDeregistrationSendResponseModel.Submitz = tinDeregistrationResponseModel.Submitz;
            tinDeregistrationSendResponseModel.Status = tinDeregistrationResponseModel.Status;
            tinDeregistrationSendResponseModel.Savez = tinDeregistrationResponseModel.Savez;
            tinDeregistrationSendResponseModel.Rejectz = tinDeregistrationResponseModel.Rejectz;
            tinDeregistrationSendResponseModel.RegIdz = tinDeregistrationResponseModel.RegIdz;
            tinDeregistrationSendResponseModel.PortalUsrz = tinDeregistrationResponseModel.PortalUsrz;
            tinDeregistrationSendResponseModel.PeriodKeyz = tinDeregistrationResponseModel.PeriodKeyz;
            tinDeregistrationSendResponseModel.Operation = tinDeregistrationResponseModel.Operation;
            tinDeregistrationSendResponseModel.OfficerUidz = tinDeregistrationResponseModel.OfficerUidz;
            tinDeregistrationSendResponseModel.Monthz = tinDeregistrationResponseModel.Monthz;
            tinDeregistrationSendResponseModel.LegacyDocNo = tinDeregistrationResponseModel.LegacyDocNo;
            tinDeregistrationSendResponseModel.Langz = tinDeregistrationResponseModel.Langz;
            tinDeregistrationSendResponseModel.FormGuid = tinDeregistrationResponseModel.FormGuid;
            tinDeregistrationSendResponseModel.Fbnumz = tinDeregistrationResponseModel.Fbnumz;
            tinDeregistrationSendResponseModel.Fbnum = tinDeregistrationResponseModel.Fbnum;
            tinDeregistrationSendResponseModel.Dflag = tinDeregistrationResponseModel.Dflag;
            tinDeregistrationSendResponseModel.CreateTxAssesz = tinDeregistrationResponseModel.CreateTxAssesz;
            tinDeregistrationSendResponseModel.Cflag = tinDeregistrationResponseModel.Cflag;
            tinDeregistrationSendResponseModel.CaseGuid = tinDeregistrationResponseModel.CaseGuid;
            tinDeregistrationSendResponseModel.Auditorz = tinDeregistrationResponseModel.Auditorz;
            tinDeregistrationSendResponseModel.ATransTin = tinDeregistrationResponseModel.ATransTin;
            tinDeregistrationSendResponseModel.ATitle = tinDeregistrationResponseModel.ATitle;
            tinDeregistrationSendResponseModel.ATinType = tinDeregistrationResponseModel.ATinType;
            tinDeregistrationSendResponseModel.ATin = tinDeregistrationResponseModel.ATin;
            tinDeregistrationSendResponseModel.ATaxpayerName = tinDeregistrationResponseModel.ATaxpayerName;
            tinDeregistrationSendResponseModel.ASubmissionDateH = tinDeregistrationResponseModel.ASubmissionDateH;
            tinDeregistrationSendResponseModel.ASubmissionDateC = tinDeregistrationResponseModel.ASubmissionDateC;
            tinDeregistrationSendResponseModel.ASubmissionDate = tinDeregistrationResponseModel.ASubmissionDate;
            tinDeregistrationSendResponseModel.AStep = tinDeregistrationResponseModel.AStep;
            tinDeregistrationSendResponseModel.Approvez = tinDeregistrationResponseModel.Approvez;
            tinDeregistrationSendResponseModel.AOffOrigin = tinDeregistrationResponseModel.AOffOrigin;
            tinDeregistrationSendResponseModel.AOffAppNo = tinDeregistrationResponseModel.AOffAppNo;
            tinDeregistrationSendResponseModel.ANm7 = tinDeregistrationResponseModel.ANm7 ?? string.Empty;
            tinDeregistrationSendResponseModel.ANm6 = tinDeregistrationResponseModel?.ANm6 ?? string.Empty;
            tinDeregistrationSendResponseModel.ANm5 = tinDeregistrationResponseModel?.ANm5 ?? string.Empty;
            tinDeregistrationSendResponseModel.ANm4 = tinDeregistrationResponseModel?.ANm4 ?? string.Empty;
            tinDeregistrationSendResponseModel.ANm3 = tinDeregistrationResponseModel?.ANm3 ?? string.Empty;
            tinDeregistrationSendResponseModel.ANm2 = tinDeregistrationResponseModel?.ANm2 ?? string.Empty;
            tinDeregistrationSendResponseModel.ANm1 = tinDeregistrationResponseModel?.ANm1 ?? string.Empty;
            tinDeregistrationSendResponseModel.AmdRsnz = tinDeregistrationResponseModel.AmdRsnz;
            tinDeregistrationSendResponseModel.AIdType = tinDeregistrationResponseModel.AIdType ?? string.Empty;
            tinDeregistrationSendResponseModel.AIdNo = tinDeregistrationResponseModel.AIdNo;
            tinDeregistrationSendResponseModel.AFormStatus = tinDeregistrationResponseModel.AFormStatus;
            tinDeregistrationSendResponseModel.AExpdtH = tinDeregistrationResponseModel.AExpdtH;
            tinDeregistrationSendResponseModel.AExpdtC = tinDeregistrationResponseModel.AExpdtC;
            tinDeregistrationSendResponseModel.AExpdt = tinDeregistrationResponseModel.AExpdt;
            tinDeregistrationSendResponseModel.AEffectiveDtH = tinDeregistrationResponseModel.AEffectiveDtH;
            tinDeregistrationSendResponseModel.AEffectiveDtC = tinDeregistrationResponseModel.AEffectiveDtC;
            tinDeregistrationSendResponseModel.AEffectiveDt = tinDeregistrationResponseModel.AEffectiveDt;
            tinDeregistrationSendResponseModel.ADregReason = tinDeregistrationResponseModel.ADregReason;
            tinDeregistrationSendResponseModel.ADregOpt = tinDeregistrationResponseModel.ADregOpt;
            tinDeregistrationSendResponseModel.ADocumnt9 = tinDeregistrationResponseModel.ADocumnt9;
            tinDeregistrationSendResponseModel.ADocumnt8 = tinDeregistrationResponseModel.ADocumnt8;
            tinDeregistrationSendResponseModel.ADocumnt7 = tinDeregistrationResponseModel.ADocumnt7;
            tinDeregistrationSendResponseModel.ADocumnt6 = tinDeregistrationResponseModel.ADocumnt6;
            tinDeregistrationSendResponseModel.ADocumnt5Txt = tinDeregistrationResponseModel.ADocumnt5Txt;
            tinDeregistrationSendResponseModel.ADocumnt5 = tinDeregistrationResponseModel.ADocumnt5;
            tinDeregistrationSendResponseModel.ADocumnt4Txt = tinDeregistrationResponseModel.ADocumnt4Txt;
            tinDeregistrationSendResponseModel.ADocumnt4 = tinDeregistrationResponseModel.ADocumnt4;
            tinDeregistrationSendResponseModel.ADocumnt3 = tinDeregistrationResponseModel.ADocumnt3;
            tinDeregistrationSendResponseModel.ADocumnt2 = tinDeregistrationResponseModel.ADocumnt2;
            tinDeregistrationSendResponseModel.ADocumnt14 = tinDeregistrationResponseModel.ADocumnt14;
            tinDeregistrationSendResponseModel.ADocumnt13 = tinDeregistrationResponseModel.ADocumnt13;
            tinDeregistrationSendResponseModel.ADocumnt12 = tinDeregistrationResponseModel.ADocumnt12;
            tinDeregistrationSendResponseModel.ADocumnt11 = tinDeregistrationResponseModel.ADocumnt11;
            tinDeregistrationSendResponseModel.ADocumnt10 = tinDeregistrationResponseModel.ADocumnt10;
            tinDeregistrationSendResponseModel.ADocumnt1 = tinDeregistrationResponseModel.ADocumnt1;
            tinDeregistrationSendResponseModel.ADobH = tinDeregistrationResponseModel.ADobH;
            tinDeregistrationSendResponseModel.ADobC = tinDeregistrationResponseModel.ADobC;
            tinDeregistrationSendResponseModel.ADob = tinDeregistrationResponseModel.ADob;
            tinDeregistrationSendResponseModel.ADegister = "1";
            tinDeregistrationSendResponseModel.ADecTitle = tinDeregistrationResponseModel.ADecTitle;
            tinDeregistrationSendResponseModel.ADecTelNo = tinDeregistrationResponseModel.ADecTelNo;
            tinDeregistrationSendResponseModel.ADeclarationChkbox = "1";
            tinDeregistrationSendResponseModel.ADecDesig = tinDeregistrationResponseModel.ADecDesig;
            tinDeregistrationSendResponseModel.ADecDateH = tinDeregistrationResponseModel.ADecDateH;
            tinDeregistrationSendResponseModel.ADecDateC = tinDeregistrationResponseModel.ADecDateC;
            tinDeregistrationSendResponseModel.ADecDate = tinDeregistrationResponseModel.ADecDate;
            tinDeregistrationSendResponseModel.ADateFormat = "";
            tinDeregistrationSendResponseModel.ABranchTxt = tinDeregistrationResponseModel.ABranchTxt;
            tinDeregistrationSendResponseModel.ABpKind = tinDeregistrationResponseModel.ABpKind;
            tinDeregistrationSendResponseModel.PermitSet = tinDeregistrationResponseModel.PermitSet.Results;
            tinDeregistrationSendResponseModel.OutletSet = tinDeregistrationResponseModel.OutletSet.Results;
            tinDeregistrationSendResponseModel.OffNotesSet = new List<string>();
            tinDeregistrationSendResponseModel.AttDetSet = new List<string>();
            tinDeregistrationSendResponseModel.ReturnSet = new List<string>();
            tinDeregistrationSendResponseModel.ADecName = tinDeregistrationResponseModel.ADecName;
            tinDeregistrationSendResponseModel.PermitTableSet = new List<string>();

            List<PermitSetResult> temp = new List<PermitSetResult>();

            foreach (PermitSetResult permitInfo in tinDeregistrationSendResponseModel.PermitSet)
            {
                if (permitInfo.APermitIdNoTb == null)
                    permitInfo.APermitIdNoTb = "";

                if (String.IsNullOrEmpty(permitInfo.APermitTransTinTb))
                    permitInfo.APermitTransTinTb = " ";

                if (permitInfo.APermitDregRsnTb == null)
                    permitInfo.APermitDregRsnTb = string.Empty;

                temp.Add(permitInfo);
            }

            tinDeregistrationSendResponseModel.PermitSet = temp.ToArray();

            if (CrossConnectivity.Current.IsConnected)
            {
                TinDeregistrationResponseModel _newRequestSummaryDataResponse = new TinDeregistrationResponseModel();
                string NewToken = string.Empty;

                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string lang = WebServiceManager.GetLangZParameterAREN();
                    string url = Constants.TinDeregistrationNewRequestUrl;

                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

                    var uri = new Uri(url);
                    var serilized = JsonConvert.SerializeObject(tinDeregistrationSendResponseModel);

                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    HttpResponseMessage tinDeregResponse = await client.PostAsync(uri, contentPost);

                    if (tinDeregResponse != null)
                    {
                        if (tinDeregResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        String _responseData = tinDeregResponse.Content.ReadAsStringAsync().Result;
                        if (tinDeregResponse.StatusCode == HttpStatusCode.BadRequest)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_responseData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorCode = errorMesg.error.innererror.errordetails[0].code;
                                WebServiceManager.ErrorMessageForUnlockAccount = errorMesg.error.innererror.errordetails[0].message;

                                String WithReplacedString = WebServiceManager.ErrorMessageForUnlockAccount.Replace("An exception was raised", string.Empty);
                                WebServiceManager.ErrorMessageForUnlockAccount = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTErrorException(WebServiceManager.ErrorMessageForUnlockAccount);
                            }
                        }
                        HttpHeaders headers = tinDeregResponse.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                            App.IsSessionExpired = false;
                        }
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expired")) || (0 == String.Compare(NewToken, "Invalid Token")))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }

                        TinDeregResponseJson = tinDeregResponse.Content.ReadAsStringAsync().Result;

                    }
                    return TinDeregResponseJson;
                }
                catch (GAZTErrorException ex)
                {
                    Console.WriteLine(ex);
                    throw new GAZTErrorException(ex.Message);
                }

                catch (Exception)
                {
                    throw new GAZTErrorException(AppResources.Somethingwentwrong);
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }


        public async static Task<TinDeregistrationReasonSetDataModel> GaztTinDeregistrationReasonData()
        {
            TinDeregistrationReasonSetDataModel _tinDeregistrationReasonSetDataModel = new TinDeregistrationReasonSetDataModel();

            if (CrossConnectivity.Current.IsConnected)
            {

                string NewToken = string.Empty;
                try
                {
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    String url = Constants.TinDeregistrationReasonSetUrl + "Partner='" + App.LoginDataRetrieved.TIN + "',Spars='" + lang + "')?&$expand=REASONSet&$format=json";
                    var uri = new Uri(url);

                    HttpResponseMessage _tinDeregReasonRequestResponse = await client.GetAsync(uri);

                    if (_tinDeregReasonRequestResponse != null)
                    {
                        if (_tinDeregReasonRequestResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _tinDeregReasonRequestResponse.Headers;
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

                        String _responseData = _tinDeregReasonRequestResponse.Content.ReadAsStringAsync().Result;
                        if (_tinDeregReasonRequestResponse.StatusCode == HttpStatusCode.BadRequest)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_responseData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorCode = errorMesg.error.innererror.errordetails[0].code;
                                WebServiceManager.ErrorMessageForUnlockAccount = errorMesg.error.innererror.errordetails[0].message;

                                String WithReplacedString = WebServiceManager.ErrorMessageForUnlockAccount.Replace("An exception was raised", string.Empty);
                                WebServiceManager.ErrorMessageForUnlockAccount = WithReplacedString;
                                throw new GAZTErrorException(WebServiceManager.ErrorMessageForUnlockAccount);
                            }
                        }
                        else if (!string.IsNullOrEmpty(_responseData))
                        {
                            _responseData = JObject.Parse(_responseData)["d"].ToString();
                            _tinDeregistrationReasonSetDataModel = JsonConvert.DeserializeObject<TinDeregistrationReasonSetDataModel>(_responseData);
                            if (_tinDeregistrationReasonSetDataModel == null)
                            {
                                throw new GAZTErrorException(AppResources.ZZSomethingwentwrong);
                            }
                        }
                        else
                        {
                            throw new GAZTErrorException(AppResources.ZZSomethingwentwrong);
                        }
                    }

                    return _tinDeregistrationReasonSetDataModel;
                }
                catch (GAZTErrorException ex)
                {
                    throw new GAZTErrorException(ex.Message);
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
