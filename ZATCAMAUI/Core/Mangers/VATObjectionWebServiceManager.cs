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
                    string taxType = "VT";
                    char lang = WebServiceManager.GetLangZParameter();
                    string url = ZATCAConstants.GetVATObjectionListURL + "TaxType='" + taxType + "',AudTin='" + "',Gpart='" + App.LoginDataRetrieved.TIN + "',Lang='" + lang + "'," + "UserTin='" + "')?$expand=ASSLISTSet,STATUSSet,REQTYPSet&$format=json";
                    HttpResponseMessage vATObjectionListResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
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
                        string __vATObjectionListData = vATObjectionListResponse.Content.ReadAsStringAsync().Result;
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

                    char lang = WebServiceManager.GetLangZParameter();
                    string url = "";
                    if (string.IsNullOrEmpty(FBGuid))
                    {

                        url = ZATCAConstants.GetVATObjectionSummaryURL + "FormGuid='" + "',Fbnumx='" + "',Gpartx='" + App.LoginDataRetrieved.TIN + "',Langx='" + lang + "'," +
                     "Officerx='" + "',PortalUsrx='" + "',Euserx='" + "',Appfg='N')?$expand=AddressSet,AttdetSet,NotesSet,QuesListSet,ReasonSet,IdDetailSet,MainReasonSet,SecurityDtl&$format=json";
                    }
                    else
                    {
                        url = ZATCAConstants.GetVATObjectionSummaryURL + "FormGuid='" + "',Fbnumx='" + FBGuid + "',Gpartx='" + "',Langx='" + lang + "'," +
                    "Officerx='" + "',PortalUsrx='" + "',Euserx='00000000000000000000',Appfg='N')?$expand=AddressSet,AttdetSet,NotesSet,QuesListSet,ReasonSet,IdDetailSet,MainReasonSet,SecurityDtl&$format=json";
                    }
                    HttpResponseMessage vATObjectionSummaryResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
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
                        string __vATObjectionSummaryData = vATObjectionSummaryResponse.Content.ReadAsStringAsync().Result;
                        _vATObjectionSummaryModel = JsonConvert.DeserializeObject<VATObjectionSummaryModel>(__vATObjectionSummaryData);
                        if (!string.IsNullOrEmpty(__vATObjectionSummaryData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__vATObjectionSummaryData);
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
                    char lang = WebServiceManager.GetLangZParameter();
                    string url = ZATCAConstants.GetVATObjectionRejectedFormURL + "Langx='" + lang + "',Gpartx='" + App.LoginDataRetrieved.TIN + "',TxnTpx='" + "',Fbustx='" + fbustx + "'," +
                     "Fbstax='" + "',UserTypx='" + UserTypx + "',RvRsn='" + RvRsn + "',RvSubRsn='" + rvSubRsn + "',Fbnumx='" + fbnumx + "',Sopbel='" + sopbel + "',Formprocx='" + formprocx + "')?$expand=RejectedFormSet&$format=json";
                    HttpResponseMessage vATObjectionRejectedFormResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
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

        public async static Task<VATObjectionSecurityAmountModel> GAZTGetVATObjectionSecurityAmount(decimal disamt, decimal liaamt, decimal clramt)
        {
            VATObjectionSecurityAmountModel _vATObjectionSecurityAmountModel = new VATObjectionSecurityAmountModel();

            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    string amttp = "P";
                    char lang = WebServiceManager.GetLangZParameter();
                    string url = ZATCAConstants.GetVATObjectionSecurityURL + "Disamt=" + disamt + "m,Liaamt=" + liaamt + "m,Clramt=" + clramt + "m,Amttp='" + amttp + "')?$format=json";
                    HttpResponseMessage vATObjectionSecurityAmountResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
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

                    string Url = string.Empty;
                    Url = ZATCAConstants.BaseUrlOfODataServices + "/sap/opu/odata/SAP/Z_GET_ACK_LETTER_SRV/Ack_letterSet(Fbnum='" + fbnum + "')/$value";

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
        public async static Task<VATObjectionGenrateSadadModel> GAZTGetVATObjectionGenrateorRefreshSADAD(string fbnum, decimal Disamt,
            decimal Liaamt, string Abrzu, string Abrzo, decimal Secamt, string Security, string Persl, bool isFlagenable)
        {
            VATObjectionGenrateSadadModel _vATObjectionGenrateSadadModel = new VATObjectionGenrateSadadModel();

            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {

                    char lang = WebServiceManager.GetLangZParameter();
                    string url = "";
                    if (isFlagenable)
                    {

                        url = ZATCAConstants.GetVATObjectionGenrateorRefreshSADADURL + "Fbnum='" + fbnum + "',Gpart='" + App.LoginDataRetrieved.TIN + "',Disamt=" + Disamt + "" +
                       "m,Liaamt=" + Liaamt + "m,Sectp='C',Abrzu=datetime'" + Abrzu + "',Abrzo=datetime'" + Abrzo + "',Flag=true,Secamt=" + Secamt + "m,Security='" + Security + "',Persl='" + Persl + "')?$format=json";
                    }
                    else
                    {
                        url = ZATCAConstants.GetVATObjectionGenrateorRefreshSADADURL + "Fbnum='" + fbnum + "',Gpart='" + App.LoginDataRetrieved.TIN + "',Disamt=" + Disamt + "" +
                       "m,Liaamt=" + Liaamt + "m,Sectp='C',Abrzu=datetime'" + Abrzu + "',Abrzo=datetime'" + Abrzo + "',Flag=false,Secamt=" + Secamt + "m,Security='" + Security + "',Persl='" + Persl + "')?$format=json";
                    }
                    HttpResponseMessage _vATObjectionGenrateSadadResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
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
                    char lang = WebServiceManager.GetLangZParameter();
                    vtre2 = App.LoginDataRetrieved.TIN;
                    string url = ZATCAConstants.GetVATObjectionViewBillURL + "Opbel eq'" + opbel + "' and Vtre2 eq'" + vtre2 + "'&$format=json";
                    HttpResponseMessage _vATObjectionViewbillResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
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
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    var serilized = JsonConvert.SerializeObject(_vatObjectionDetails);
                    client.DefaultRequestHeaders.Add("Token", "123");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = client.PostAsync(uri, contentPost).Result;
                    var _vatObjectionsResponsestr = res.Content.ReadAsStringAsync().Result;
                    _vatObjectionResponseObject = JsonConvert.DeserializeObject<VATObjectionSummaryModel>(_vatObjectionsResponsestr);
                    if (!string.IsNullOrEmpty(_vatObjectionsResponsestr) && _vatObjectionResponseObject.d == null)
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
                    char lang = WebServiceManager.GetLangZParameter();
                    string url = ZATCAConstants.VATObjectionSummaryInputURL + "Euser1='00000000000000000000',Fbguid='undefined',Fbnum='" + fbNum + "'," +
                        "Fbtyp='" + fbtyp + "',Gpart='" + App.LoginDataRetrieved.TIN + "',Lang='" + lang + "',Persl='" + "',Status='" + status + "',Dispflag='" + "')?&$format=json";
                    HttpResponseMessage _vATObjectionSummaryInputResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
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
