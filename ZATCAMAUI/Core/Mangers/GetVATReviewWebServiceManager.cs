using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Models;
using static ZATCAMAUI.Models.ErrorMessage;
using static ZATCAMAUI.Models.VATReviewModel.VATObjectionSummaryInputModel;

namespace ZATCAMAUI.Core.Mangers
{
    
    public static class GetVATReviewWebServiceManager
    {
        #region VATObjections
        public async static Task<VATReviewRequestTPFVModel> GAZTGetVATReviewRequestTPFV(string strOfficerz, string strGpartz, string strEuser, string strFbguid, string strReviewFg)
        {
            VATReviewRequestTPFVModel _VATReviewRequestTPFV = new VATReviewRequestTPFVModel();

            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string url = ZATCAConstants.GetVATReviewRequestTPFVURL
                        + "Fbnumz='"
                        + "',PortalUsrz='"
                        + "',Langz='" + lang
                        + "',Officerz='" + strOfficerz
                        + "',Gpartz='" + strGpartz
                        + "',UserTypz='"
                        + "',TxnTpz='"
                        + "',Euser='" + strEuser
                        + "',Fbguid='" + strFbguid
                        + "'," + "ReviewFg=" + strReviewFg + ")?&$expand=AttdetSet,NotesSet,QUESTIONSSet,QUESLISTSet&$format=json";

                    var uri = new Uri(url);
                    HttpResponseMessage VATReviewRequestTPFVResponse = await client.GetAsync(uri);
                    if (VATReviewRequestTPFVResponse != null)
                    {
                        if (VATReviewRequestTPFVResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = VATReviewRequestTPFVResponse.Headers;
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
                        string __VATReviewRequestTPFVData = VATReviewRequestTPFVResponse.Content.ReadAsStringAsync().Result;
                        _VATReviewRequestTPFV = JsonConvert.DeserializeObject<VATReviewRequestTPFVModel>(__VATReviewRequestTPFVData);
                        if (!string.IsNullOrEmpty(__VATReviewRequestTPFVData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__VATReviewRequestTPFVData);
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
                    return _VATReviewRequestTPFV;
                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
                }
                catch (Exception)

                {


                    // App.IsSessionExpired = true;
                    return null;
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        public async static Task<VATReviewRequestVTGRModel> GAZTGetVATReviewRequestVTGR(string strEuser, string strFbguid, string strGpart, string strTxnTpz, string strFBNum)
        {
            VATReviewRequestVTGRModel _VATReviewRequestVTGR = new VATReviewRequestVTGRModel();

            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string url = ZATCAConstants.GetVATReviewRequestVTGRURL
                        + "Euser='" + strEuser
                        + "',Fbguid='" + strFbguid
                        + "',Fbnumz='" + strFBNum
                        + "',Gpart='" + strGpart
                        + "',Langz='" + lang
                        + "',Officerz='"
                        + "',PortalUsrz='"
                        + "',TxnTpz='" + strTxnTpz
                        + "'," + "ReviewFg=true)?&$expand=ATTDETSet,NOTESSet,TABLESet,EFFDATESet,ELGBL_DOCSet,QUESLISTSet&$format=json";

                    var uri = new Uri(url);
                    HttpResponseMessage VATReviewRequestVTGRResponse = await client.GetAsync(uri);
                    if (VATReviewRequestVTGRResponse != null)
                    {
                        if (VATReviewRequestVTGRResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = VATReviewRequestVTGRResponse.Headers;
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
                        string __VATReviewRequestVTGRData = VATReviewRequestVTGRResponse.Content.ReadAsStringAsync().Result;
                        _VATReviewRequestVTGR = JsonConvert.DeserializeObject<VATReviewRequestVTGRModel>(__VATReviewRequestVTGRData);
                        if (!string.IsNullOrEmpty(__VATReviewRequestVTGRData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(__VATReviewRequestVTGRData);
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
                    return _VATReviewRequestVTGR;
                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
                }
                catch (Exception)

                {


                    //App.IsSessionExpired = true;
                    return null;
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        public async static Task<VATReviewDREGViewApplicationModel> GAZTGetVATReviewDREGViewApplication(string fbGUID)
        {
            VATReviewDREGViewApplicationModel _VATReviewDREGResult = new VATReviewDREGViewApplicationModel();

            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string Euser = "00000000000000000000";
                    string url = ZATCAConstants.GetVATObjViewApplicationDREGURL
                        + "FormGuid='" + fbGUID
                        + "',Fbnumx='"
                        + "',Gpartx='" + App.LoginDataRetrieved.TIN
                        + "',Langx='" + lang
                        + "',Officerx='"
                        + "',PortalUsrx='"
                        + "',Euser='" + Euser
                        + "'," + "ReviewFg=true)?&$expand=AddressSet,AttdetSet,NotesSet,QuesListSet&$format=json";

                    var uri = new Uri(url);
                    HttpResponseMessage VATDREGResponse = await client.GetAsync(uri);
                    if (VATDREGResponse != null)
                    {
                        if (VATDREGResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = VATDREGResponse.Headers;
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
                        string _VATReviewDREGData = VATDREGResponse.Content.ReadAsStringAsync().Result;
                        _VATReviewDREGResult = JsonConvert.DeserializeObject<VATReviewDREGViewApplicationModel>(_VATReviewDREGData);
                        if (!string.IsNullOrEmpty(_VATReviewDREGData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_VATReviewDREGData);
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
                    return _VATReviewDREGResult;
                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
                }
                catch (Exception)
                {


                    //App.IsSessionExpired = true;
                    return null;
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }


        public async static Task<VATObjectionDREGReasonModel> GAZTGetVATReviewDREGReasonSet(string deregType)
        {
            VATObjectionDREGReasonModel _vATObjectionDREGReasonModel = new VATObjectionDREGReasonModel();

            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    string url = string.Empty;
                    if (deregType == "VT_DREG")
                    {
                        url = ZATCAConstants.GetVATObjViewApplicationDREGReasonSetURL + "'" + lang + "'&$format=json";
                    }
                    else
                    {
                        url = ZATCAConstants.GetVATObjViewApplicationDREGSuspensionReasonSetURL;
                    }

                    var uri = new Uri(url);
                    HttpResponseMessage VATDREGReasonResponse = await client.GetAsync(uri);
                    if (VATDREGReasonResponse != null)
                    {
                        if (VATDREGReasonResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = VATDREGReasonResponse.Headers;
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
                        string _vATObjectionDREGReasonData = VATDREGReasonResponse.Content.ReadAsStringAsync().Result;
                        _vATObjectionDREGReasonModel = JsonConvert.DeserializeObject<VATObjectionDREGReasonModel>(_vATObjectionDREGReasonData);
                        if (!string.IsNullOrEmpty(_vATObjectionDREGReasonData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_vATObjectionDREGReasonData);
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
                    return _vATObjectionDREGReasonModel;
                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
                }
                catch (Exception)
                {


                    // App.IsSessionExpired = true;
                    return null;
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        public async static Task<VATReviewDREGSuspensionListModel> GAZTGetVATReviewDREGSuspensionDetailSet(string startDate, string endDate)
        {
            VATReviewDREGSuspensionListModel _vATReviewDREGSuspensionListModel = new VATReviewDREGSuspensionListModel();

            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string url = ZATCAConstants.GetVATObjSuspensionDetailSetURL + "Gpart eq'" + App.LoginDataRetrieved.TIN + "'and StartDate eq datetime'" + startDate + "' and  EndDate eq datetime'" + endDate + "'&$format=json";
                    var uri = new Uri(url);
                    HttpResponseMessage vATReviewDREGSuspension = await client.GetAsync(uri);
                    if (vATReviewDREGSuspension != null)
                    {
                        if (vATReviewDREGSuspension.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = vATReviewDREGSuspension.Headers;
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
                        string _vATReviewDREGSuspensionData = vATReviewDREGSuspension.Content.ReadAsStringAsync().Result;
                        _vATReviewDREGSuspensionListModel = JsonConvert.DeserializeObject<VATReviewDREGSuspensionListModel>(_vATReviewDREGSuspensionData);
                        if (!string.IsNullOrEmpty(_vATReviewDREGSuspensionData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_vATReviewDREGSuspensionData);
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
                    return _vATReviewDREGSuspensionListModel;
                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
                }
                catch (Exception)
                {


                    //App.IsSessionExpired = true;
                    return null;
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        public async static Task email(string doguid, Attachment attachment)
        {

            await Task.Run(async () =>
            {
                try
                {


                    string attachmentURL = attachment.DocUrl;// "https://sapgatewayqa.gazt.gov.sa/sap/opu/odata/SAP/ZDP_IT_CORR_MOOB_SRV/corr_dataSet(Cokey='" + doguid + "',Cotyp='VTA0')/$value?saml2=disabled";

                    MemoryStream pdfStream = new MemoryStream();


                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var uri = new Uri(attachmentURL);
                    HttpResponseMessage _fileDownloadResponse = await client.GetAsync(uri);

                    var fileName = Guid.NewGuid().ToString();

                    _fileDownloadResponse.EnsureSuccessStatusCode();
                    await _fileDownloadResponse.Content.CopyToAsync(pdfStream);
                    var message = new EmailMessage
                    {
                        Subject = "Attached Form :",
                    };
                    var fn = attachment.Filename;
                    var file = Path.Combine(FileSystem.CacheDirectory, fn);
                    File.WriteAllBytes(file, pdfStream.ToArray());

                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await Share.RequestAsync(new ShareFileRequest
                        {
                            Title = "",
                            File = new ShareFile(file)
                        });
                    });



                }
                catch (Exception)
                {


                }
            });

        }


        #endregion
    }
}
