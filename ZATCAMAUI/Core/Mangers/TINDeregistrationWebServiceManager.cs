using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.TINOutletDeregister;
using static ZATCAMAUI.Models.ErrorMessage;

namespace ZATCAMAUI.Core.Mangers
{

    public static class TINDeregistrationWebServiceManager
    {
        #region TIN Deregistration
        public async static Task<TinDeregistrationResponseModel> GaztTinDeregistrationNewRequestData(TinDeregistrationResponseModel tinDeregistrationResponseModel)
        {
            TinDeregistrationResponseModel _tinDeregistrationResponseModel = new TinDeregistrationResponseModel();

            if (NetworkCheck.IsInternet())
            {

                string NewToken = string.Empty;
                try
                {
                    var lang = UtilityManager.GetLanguageParameter();
               
                    String url = ZATCAConstants.OutletDeregistrationNewRequestUrl + App.LoginDataRetrieved.TIN + "&deregister=1" + "&language=" + lang + "&approve=" + tinDeregistrationResponseModel.Approvez + "&reject=" + tinDeregistrationResponseModel.Rejectz;
                    HttpClient client = new HttpClient(App.httpClientHandler);
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
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
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
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }

                        string _responseData = _tinDeregNewRequestResponse.Content.ReadAsStringAsync().Result;
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
                                string line1 = "";
                                /* if (errorMesg.error.innererror.errordetails.Count > 2)
                                 {*/
                                for (int i = 0; i < errorMesg.error.innererror.errordetails.Count; i++)
                                {
                                    if (i == 0)
                                    {
                                        line1 = line1 + errorMesg.error.innererror.errordetails[i].message + "\n";
                                    }
                                    else
                                    {
                                        if (i == errorMesg.error.innererror.errordetails.Count - 2)
                                        {
                                            line1 = line1 + "\n" + "\n" + errorMesg.error.innererror.errordetails[i].message;

                                        }
                                        else
                                        {
                                            line1 = line1 + "\u2022" + errorMesg.error.innererror.errordetails[i].message + "\n";

                                        }
                                    }

                                }
                                WebServiceManager.ErrorMessageForUnlockAccount = line1;

                                String WithReplacedString = WebServiceManager.ErrorMessageForUnlockAccount.Replace("\u2022An exception was raised", string.Empty);
                                throw new GAZTErrorException(WithReplacedString);
                                /* }
                                 else
                                 {
                                     String WithReplacedString = WebServiceManager.ErrorMessageForUnlockAccount.Replace("An exception was raised", string.Empty);
                                     WebServiceManager.ErrorMessageForUnlockAccount = WithReplacedString;
                                     throw new GAZTErrorException(WebServiceManager.ErrorMessageForUnlockAccount);
                                 }*/


                            }
                        }
                        else if (!string.IsNullOrEmpty(_responseData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_responseData);
                            if (errorMesg != null && errorMesg.header != null && errorMesg.header.moreInformation != null && errorMesg.header.moreInformation?.errorDetails != null && errorMesg.header.moreInformation.errorDetails[0].message != null)
                            {
                                string errorCode = errorMesg.header.moreInformation.errorDetails[0].code;
                                WebServiceManager.ErrorMessageForUnlockAccount = errorMesg.header.moreInformation.errorDetails[0].message;

                                string WithReplacedString = WebServiceManager.ErrorMessageForUnlockAccount.Replace("An exception was raised", string.Empty);
                                WebServiceManager.ErrorMessageForUnlockAccount = WithReplacedString;
                                throw new GAZTErrorException(WebServiceManager.ErrorMessageForUnlockAccount);
                            }
                            else
                            {
                                _responseData = JObject.Parse(_responseData)["data"].ToString();
                                _tinDeregistrationResponseModel = JsonConvert.DeserializeObject<TinDeregistrationResponseModel>(_responseData);
                                //AttachmentSet attachments = new AttachmentSet();
                                List<Attachment> attachments = _tinDeregistrationResponseModel.AttDetSet;
                                _tinDeregistrationResponseModel.AttDetSet = attachments;
                                // Set set = new Set();
                                OutletSetResult[] set = _tinDeregistrationResponseModel.OutletSet;
                                _tinDeregistrationResponseModel.OutletSet = set;
                                PermitSetResult[] permits = _tinDeregistrationResponseModel.PermitSet;
                                _tinDeregistrationResponseModel.PermitSet = permits;

                                if (_tinDeregistrationResponseModel == null)
                                {
                                    throw new GAZTErrorException(AppResources.ZZSomethingwentwrong);
                                }
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
                catch (Exception ex)
                {
                    
                    
                    throw new GAZTErrorException(ex.Message);
                }
                
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        private static string ConvertDateFormat(DateTime newDate)
        {
            string ConvertedDate = string.Empty;
            long ticks = newDate.Ticks - new DateTime(1970, 1, 1).Ticks;
            TimeSpan span = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            string unixTime = span.TotalSeconds.ToString("N0");
            unixTime = unixTime.Replace(",", "");
            ConvertedDate = "" + "/Date(" + unixTime + ")/";

            long unixTimestamp = (long)newDate.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

            unixTimestamp = unixTimestamp * 1000;

            ConvertedDate = "" + "/Date(" + unixTimestamp + ")/";
            return ConvertedDate;
        }


        public static async Task<string> GaztTinDeregistrationSubmitRequestData(TinDeregistrationResponseModel tinDeregistrationResponseModel)
        {
            string TinDeregResponseJson = string.Empty;
            TinDeregistrationSendResponseModel tinDeregistrationSendResponseModel = new TinDeregistrationSendResponseModel();
            ObservableCollection<OutletSetResult> AllOutlets = new ObservableCollection<OutletSetResult>(tinDeregistrationResponseModel.OutletSet);
            List<PermitSetResult> allPermitTypes = new List<PermitSetResult>(tinDeregistrationResponseModel.PermitSet);
            try
            {


                foreach (OutletSetResult outletInfo in AllOutlets)
                {
                    if (outletInfo.AOutletEffDtTb != null && outletInfo.AOutletEffDtTb.Contains("/Date("))
                        outletInfo.AOutletEffDtTb = UtilityManager.ConvertToStringFromDate(outletInfo.AOutletEffDtTb);
                    else if (outletInfo.AOutletEffDtTb != null && !outletInfo.AOutletEffDtTb.Contains("/Date("))
                    {
                        outletInfo.AOutletEffDtTb = UtilityManager.ConvertToStringFromDate(ConvertDateFormat(Convert.ToDateTime(outletInfo.AOutletEffDtTb)));
                    }
                    outletInfo.AOutletEffDtCTb = "Gregorian";
                    if (outletInfo.AOutletDobTb != null && outletInfo.AOutletDobTb.Contains("/Date"))
                        outletInfo.AOutletDobTb = UtilityManager.ConvertToStringFromDate(outletInfo.AOutletDobTb);
                    else if (outletInfo.AOutletDobTb != null && !outletInfo.AOutletDobTb.Contains("/Date"))
                    {
                        outletInfo.AOutletDobTb = UtilityManager.ConvertToStringFromDate(ConvertDateFormat(Convert.ToDateTime(outletInfo.AOutletDobTb)));
                    }
                    if (outletInfo != null && outletInfo.AOutletEffDtHTb.Contains("/Date"))
                        outletInfo.AOutletEffDtHTb = UtilityManager.ConvertDateFormat(outletInfo.AOutletEffDtHTb);
                    else if (outletInfo != null && !outletInfo.AOutletEffDtHTb.Contains("/Date"))
                    {
                        outletInfo.AOutletEffDtHTb = UtilityManager.stringToIFormat(UtilityManager.ConvertToStringFromDate(ConvertDateFormat(Convert.ToDateTime(outletInfo.AOutletEffDtHTb))));
                    }


                }
                foreach (PermitSetResult permitInfo in allPermitTypes)
                {
                    permitInfo.APermitDobTb = null;
                    if (!string.IsNullOrEmpty(permitInfo.APermitEffDtTb) && !permitInfo.APermitEffDtTb.Contains("/Date("))
                        permitInfo.APermitEffDtTb = ConvertDateFormat(Convert.ToDateTime(permitInfo.APermitEffDtTb));
                    permitInfo.APermitEffDtCTb = "Gregorian";
                    if (!permitInfo.APermitValfrDtTb.Contains("/Date("))
                    {
                        var dateValue = ConvertDateFormat(Convert.ToDateTime(permitInfo.APermitValfrDtTb));
                        permitInfo.APermitValfrDtTb = UtilityManager.ConvertToStringFromDate(dateValue);

                    }
                    else
                    {
                        //var dateValue = ConvertDateFormat(Convert.ToDateTime(permitInfo.APermitValfrDtTb));
                        permitInfo.APermitValfrDtTb = UtilityManager.ConvertToStringFromDate(permitInfo.APermitValfrDtTb);

                    }

                    permitInfo.APermitValfrDtCTb = "Gregorian";

                }
            }
            catch (Exception)
            {
            }

            var lang = UtilityManager.GetLanguageParameter();

            tinDeregistrationSendResponseModel.Metadata = tinDeregistrationResponseModel.Metadata;
            tinDeregistrationSendResponseModel.Assignme = tinDeregistrationResponseModel.Assignme;

            tinDeregistrationSendResponseModel.Caseid = tinDeregistrationResponseModel.Caseid;
            tinDeregistrationSendResponseModel.Xvoidz = tinDeregistrationResponseModel.Xvoidz;
            tinDeregistrationSendResponseModel.BgDregFlg = tinDeregistrationResponseModel.BgDregFlg;
            tinDeregistrationSendResponseModel.SezTpFlag = tinDeregistrationResponseModel.SezTpFlag;
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
            tinDeregistrationSendResponseModel.Langz = lang;
            tinDeregistrationSendResponseModel.FormGuid = tinDeregistrationResponseModel.FormGuid;
            tinDeregistrationSendResponseModel.Fbnumz = tinDeregistrationResponseModel.Fbnumz??string.Empty;
            tinDeregistrationSendResponseModel.Fbnum = tinDeregistrationResponseModel.Fbnum ?? string.Empty;
            tinDeregistrationSendResponseModel.Fbust = tinDeregistrationResponseModel.Fbust;
            tinDeregistrationSendResponseModel.Dflag = tinDeregistrationResponseModel.Dflag;
            tinDeregistrationSendResponseModel.CreateTxAssesz = tinDeregistrationResponseModel.CreateTxAssesz;
            tinDeregistrationSendResponseModel.Cflag = tinDeregistrationResponseModel.Cflag;
            tinDeregistrationSendResponseModel.CaseGuid = tinDeregistrationResponseModel.CaseGuid;
            tinDeregistrationSendResponseModel.Auditorz = tinDeregistrationResponseModel.Auditorz;
            tinDeregistrationSendResponseModel.ATransTin = tinDeregistrationResponseModel.ATransTin;
            tinDeregistrationSendResponseModel.ATitle = tinDeregistrationResponseModel.ATitle;
            tinDeregistrationSendResponseModel.ATinType = tinDeregistrationResponseModel.ATinType;
            tinDeregistrationSendResponseModel.ATin = tinDeregistrationResponseModel.Taxpayerz; ;// tinDeregistrationResponseModel.ATin;
            tinDeregistrationSendResponseModel.ATaxpayerName = tinDeregistrationResponseModel.ATaxpayerName;
            tinDeregistrationSendResponseModel.ASubmissionDateH = tinDeregistrationResponseModel.ASubmissionDateH;
            tinDeregistrationSendResponseModel.ASubmissionDateC = tinDeregistrationResponseModel.ASubmissionDateC;
            tinDeregistrationSendResponseModel.ASubmissionDate = UtilityManager.ConvertToStringFromDate(tinDeregistrationResponseModel.ASubmissionDate);
            tinDeregistrationSendResponseModel.AStep = tinDeregistrationResponseModel.AStep.ToString();
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
            tinDeregistrationSendResponseModel.AExpdt = UtilityManager.ConvertToStringFromDate(tinDeregistrationResponseModel.AExpdt);
            tinDeregistrationSendResponseModel.AEffectiveDtH = tinDeregistrationResponseModel.AEffectiveDtH;
            tinDeregistrationSendResponseModel.AEffectiveDtC = tinDeregistrationResponseModel.AEffectiveDtC;
            tinDeregistrationSendResponseModel.AEffectiveDt = UtilityManager.ConvertToStringFromDate(tinDeregistrationResponseModel.AEffectiveDt);
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
            tinDeregistrationSendResponseModel.ADob = UtilityManager.ConvertToStringFromDate(tinDeregistrationResponseModel.ADob);
            tinDeregistrationSendResponseModel.ADegister = "1";
            tinDeregistrationSendResponseModel.ADecTitle = tinDeregistrationResponseModel.ADecTitle;
            tinDeregistrationSendResponseModel.ADecTelNo = tinDeregistrationResponseModel.ADecTelNo;
            tinDeregistrationSendResponseModel.ADeclarationChkbox = "Checked";
            tinDeregistrationSendResponseModel.ADecDesig = tinDeregistrationResponseModel.ADecDesig;
            tinDeregistrationSendResponseModel.ADecDateH = tinDeregistrationResponseModel.ADecDateH;
            tinDeregistrationSendResponseModel.ADecDateC = tinDeregistrationResponseModel.ADecDateC;
            tinDeregistrationSendResponseModel.ADecDate = UtilityManager.ConvertToStringFromDate(tinDeregistrationResponseModel.ADecDate);
            tinDeregistrationSendResponseModel.ADateFormat = "";
            tinDeregistrationSendResponseModel.ABranchTxt = tinDeregistrationResponseModel.ABranchTxt;
            tinDeregistrationSendResponseModel.ABpKind = tinDeregistrationResponseModel.ABpKind;
            //tinDeregistrationSendResponseModel.PermitSet = tinDeregistrationResponseModel.PermitSet;
            tinDeregistrationSendResponseModel.PermitSet = allPermitTypes.ToArray();
            //tinDeregistrationSendResponseModel.OutletSet = tinDeregistrationResponseModel.OutletSet;
            tinDeregistrationSendResponseModel.OutletSet = AllOutlets.ToArray();
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

                if (string.IsNullOrEmpty(permitInfo.APermitTransTinTb))
                    permitInfo.APermitTransTinTb = " ";

                if (permitInfo.APermitDregRsnTb == null)
                    permitInfo.APermitDregRsnTb = string.Empty;

                temp.Add(permitInfo);
            }

            tinDeregistrationSendResponseModel.PermitSet = temp.ToArray();

            if (NetworkCheck.IsInternet())
            {
                TinDeregistrationResponseModel _newRequestSummaryDataResponse = new TinDeregistrationResponseModel();
                string NewToken = string.Empty;

                try
                {


                    string url = ZATCAConstants.TinDeregistrationNewRequestUrl;

                    /*client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);*/


                    HttpClient client = new HttpClient(App.httpClientHandler);
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
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);


                    var uri = new Uri(url);
                    var serilized = JsonConvert.SerializeObject(tinDeregistrationSendResponseModel);

                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage tinDeregResponse = await client.PostAsync(uri, contentPost);
                    TinDeregResponseJson = tinDeregResponse.Content.ReadAsStringAsync().Result;
                    TinDeregistrationParentResponseModel obj = JsonConvert.DeserializeObject<TinDeregistrationParentResponseModel>(TinDeregResponseJson);
                    if (obj.D == null)
                    {
                        string errorMessage = WebServiceManager.PrepareErrorMessageByJson(TinDeregResponseJson);
                        throw new GAZTVATRegistrationInProcessException(errorMessage);
                    }
                    return TinDeregResponseJson;
                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
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

            if (NetworkCheck.IsInternet())
            {

                string NewToken = string.Empty;
                try
                {
                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;
                    var lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    String url = ZATCAConstants.TinDeregistrationReasonSetUrl + App.LoginDataRetrieved.TIN + "&language=" + lang;
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
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }

                        string _responseData = _tinDeregReasonRequestResponse.Content.ReadAsStringAsync().Result;
                        if (_tinDeregReasonRequestResponse.StatusCode == HttpStatusCode.BadRequest)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_responseData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorCode = errorMesg.error.innererror.errordetails[0].code;
                                WebServiceManager.ErrorMessageForUnlockAccount = errorMesg.error.innererror.errordetails[0].message;

                                string WithReplacedString = WebServiceManager.ErrorMessageForUnlockAccount.Replace("An exception was raised", string.Empty);
                                WebServiceManager.ErrorMessageForUnlockAccount = WithReplacedString;
                                throw new GAZTErrorException(WebServiceManager.ErrorMessageForUnlockAccount);
                            }
                        }
                        else if (!string.IsNullOrEmpty(_responseData))
                        {
                            _responseData = JObject.Parse(_responseData)["data"].ToString();
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
                    return null;
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }
        #endregion

        #region TIN/Outlet Deregistration

        public static async Task<TinOutletPrevousRequestsModel> GetTinOutletDeRegisterPreviousRequests()
        {
            TinOutletPrevousRequestsModel _tinOutletPrevousRequestsModel = new TinOutletPrevousRequestsModel();

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
                    String url = ZATCAConstants.TinOutletDeregistrationPreousRequestsUrl + "?TIN=" + App.LoginDataRetrieved.TIN + "&language=" + lang;
                    var uri = new Uri(url);
                    HttpResponseMessage _tinDeregNewRequestPrevousResponse = await client.GetAsync(uri);

                    if (_tinDeregNewRequestPrevousResponse != null)
                    {
                        if (_tinDeregNewRequestPrevousResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _tinDeregNewRequestPrevousResponse.Headers;
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

                        String _responseData = _tinDeregNewRequestPrevousResponse.Content.ReadAsStringAsync().Result;
                        if (_tinDeregNewRequestPrevousResponse.StatusCode == HttpStatusCode.BadRequest)
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
                                string line1 = "";
                                /* if (errorMesg.error.innererror.errordetails.Count > 2)
                                 {*/
                                for (int i = 0; i < errorMesg.error.innererror.errordetails.Count; i++)
                                {
                                    if (i == 0)
                                    {
                                        line1 = line1 + errorMesg.error.innererror.errordetails[i].message + "\n";
                                    }
                                    else
                                    {
                                        if (i == errorMesg.error.innererror.errordetails.Count - 2)
                                        {
                                            line1 = line1 + "\n" + "\n" + errorMesg.error.innererror.errordetails[i].message;

                                        }
                                        else
                                        {
                                            line1 = line1 + "\u2022" + errorMesg.error.innererror.errordetails[i].message + "\n";

                                        }
                                    }

                                }
                                WebServiceManager.ErrorMessageForUnlockAccount = line1;

                                String WithReplacedString = WebServiceManager.ErrorMessageForUnlockAccount.Replace("\u2022An exception was raised", string.Empty);
                                throw new GAZTErrorException(WithReplacedString);



                            }
                        }
                        else if (!string.IsNullOrEmpty(_responseData))
                        {
                            _tinOutletPrevousRequestsModel = JsonConvert.DeserializeObject<TinOutletPrevousRequestsModel>(_responseData);
                            if (_tinOutletPrevousRequestsModel == null)
                            {
                                throw new GAZTErrorException(AppResources.ZZSomethingwentwrong);
                            }
                        }
                        else
                        {
                            throw new GAZTErrorException(AppResources.ZZSomethingwentwrong);
                        }
                    }

                    return _tinOutletPrevousRequestsModel;
                }
                catch (GAZTErrorException ex)
                {
                    throw new GAZTErrorException(ex.Message);
                }
                catch (Exception ex)
                {
                    throw new GAZTErrorException(ex.Message);
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        public static async Task<string> GetOutletDeRegisterNewRequest(int DeregTypeCode, string fbGuid = "")
        {

            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                string _responseData = "";
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

                    String url = string.Empty;
                    /*if(DeregTypeCode == 1)
                    {
                        url = Constants.OutletDeregistrationNewRequestUrl + "(Auditorz='',ADegister=%27" + DeregTypeCode + "%27,Taxpayerz='" + App.LoginDataRetrieved.TIN + "',FormGuid='',RegIdz='',PeriodKeyz='',Submitz='',Savez='',Fbnumz='',Langz='" + lang + "',OfficerUidz='',Approvez='',Rejectz='',CreateTxAssesz='')?saml2=enabled&$format=json&sap-language='" + lang + "'&$expand=AttDetSet,Off_notesSet,OutletSet,PermitSet,returnSet,Permit_TableSet";

                    }
                    else
                    {
                        url = Constants.OutletDeregistrationNewRequestUrl + "(Auditorz='',ADegister=%27" + DeregTypeCode + "%27,Taxpayerz='" + App.LoginDataRetrieved.TIN + "',FormGuid='',RegIdz='',PeriodKeyz='',Submitz='',Savez='',Fbnumz='',Langz='" + lang + "',OfficerUidz='',Approvez='',Rejectz='',CreateTxAssesz='')?saml2=enabled&$format=json&sap-language='" + lang + "'&$expand=AttDetSet,Off_notesSet,OutletSet,PermitSet,returnSet,Permit_TableSet,ErrMsgSet";

                    }*/

                    url = ZATCAConstants.OutletDeregistrationNewRequestUrl + App.LoginDataRetrieved.TIN + "&deregister=" + DeregTypeCode + "&language=" + lang;



                    var uri = new Uri(url);
                    HttpResponseMessage _tinDeregNewRequestPrevousResponse = await client.GetAsync(uri);

                    if (_tinDeregNewRequestPrevousResponse != null)
                    {
                        if (_tinDeregNewRequestPrevousResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _tinDeregNewRequestPrevousResponse.Headers;
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

                        _responseData = _tinDeregNewRequestPrevousResponse.Content.ReadAsStringAsync().Result;
                    }

                    return _responseData;
                }
                catch (GAZTErrorException ex)
                {
                    throw new GAZTErrorException(ex.Message);
                }
                catch (Exception ex)
                {
                    throw new GAZTErrorException(ex.Message);
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        private static double _timeoutMinutes = 3;
        internal static async Task<TinOutletDeregisterListModel> PostSubmitOrSaveDraft(TinOutletDeregisterListModel.OutletDeregisterListResponse tinOutletPrevousRequestsModel)
        {
            TinOutletDeregisterListModel _outletRequestsModel = new TinOutletDeregisterListModel();
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    string LangZ = WebServiceManager.GetLangZParameterAREN();
                    String url = ZATCAConstants.OutletDeregistrationNewRequestUrl;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    var serilized = JsonConvert.SerializeObject(tinOutletPrevousRequestsModel);
                    string lang = WebServiceManager.GetLangZParameterAREN();
                    //client.DefaultRequestHeaders.Add("Token", App.Token);
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);



                    client.Timeout = TimeSpan.FromMinutes(_timeoutMinutes);

                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = client.PostAsync(uri, contentPost).Result;
                    var _responseData = res.Content.ReadAsStringAsync().Result;

                    if (res.StatusCode == HttpStatusCode.OK || res.StatusCode == HttpStatusCode.Created)
                    {
                        if (!string.IsNullOrEmpty(_responseData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_responseData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                            else
                            {
                                _outletRequestsModel = JsonConvert.DeserializeObject<TinOutletDeregisterListModel>(_responseData);
                                _outletRequestsModel.D = _outletRequestsModel.result;
                                if (_outletRequestsModel == null)
                                {
                                    throw new GAZTErrorException(AppResources.ZZSomethingwentwrong);
                                }
                            }
                        }
                    }
                    else
                    {
                        throw new GAZTErrorException(AppResources.ZZSomethingwentwrong);
                    }
                    return _outletRequestsModel;

                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
                }
                catch (Exception ex)
                {
                    return null;
                }

            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }
        internal static async Task<bool> PostCancelExistingRequest(string fbnum, string status1, string status2, int btncode)
        {
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    var Jfbnum = new JProperty("formBundleNumber", fbnum);
                    JProperty JOperation = null;

                    if (btncode == 1)// Cancel
                    {
                        JOperation = new JProperty("operation", "04");//cancel

                    }
                    else // Delete Draft
                    {
                        JOperation = new JProperty("operation", "10");
                    }

                    JObject obj = new JObject(Jfbnum, JOperation);

                    var reqiestData = obj.ToString();

                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().Model;

                    // String url = Constants.TinOutletDeregistrationPreousRequestsUrl;
                    String url = ZATCAConstants.TinOutletDeregistrationPreousRequestsPostUrl;
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

                    var uri = new Uri(url);

                    client.Timeout = TimeSpan.FromMinutes(_timeoutMinutes);

                    HttpContent contentPost = new StringContent(reqiestData, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    var _responseData = res.Content.ReadAsStringAsync().Result;

                    if (res.StatusCode == HttpStatusCode.OK || res.StatusCode == HttpStatusCode.Created)
                    {
                        if (!string.IsNullOrEmpty(_responseData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_responseData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        throw new GAZTErrorException(AppResources.ZZSomethingwentwrong);
                    }
                    //return _outletRequestsModel;

                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
                }
                catch (Exception)
                {
                    return false;
                }

            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }



            return false;
        }

        #endregion
    }
}
