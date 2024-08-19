using System;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using ZATCAMAUI.Models;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Exceptions;
using static ZATCAMAUI.Models.ErrorMessage;


namespace ZATCAMAUI.Core.Mangers
{

    public static class EscalatedCasesWebserviceManager
    {
        public static async Task<EscalatedGstcModel> GAZTGetCaseDetailSet()
        {
            EscalatedGstcModel _requestZakatExemtionReq = new EscalatedGstcModel();

            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    string LangZAREN = WebServiceManager.GetLangZParameterAREN();
                    char LangZ = WebServiceManager.GetLangZParameter();
                    string lang = UtilityManager.GetLanguageParameter();

                    String url = ZATCAConstants.GetGstcCaseDetailsApi + "%27" + App.LoginDataRetrieved.TIN + "%27)?$expand=CaseDetailSet&$format=json";
                    HttpResponseMessage _requestZakatExemtionReqResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
                    if (_requestZakatExemtionReqResponse != null)
                    {
                        if (_requestZakatExemtionReqResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _requestZakatExemtionReqResponse.Headers;
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
                        var detailJson = _requestZakatExemtionReqResponse.Content.ReadAsStringAsync().Result;
                        _requestZakatExemtionReq = JsonConvert.DeserializeObject<EscalatedGstcModel>(detailJson);
                        if (!string.IsNullOrEmpty(detailJson))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
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

                    return _requestZakatExemtionReq;

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
    }
}
