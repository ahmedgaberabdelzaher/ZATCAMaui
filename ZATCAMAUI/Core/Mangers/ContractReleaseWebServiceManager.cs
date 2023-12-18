using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Models.ContractRelease;
using static ZATCAMAUI.Models.ContractRelease.ContractReleaseFormResponse;
using static ZATCAMAUI.Models.ErrorMessage;

namespace ZATCAMAUI.Core.Mangers
{

    public static class ContractReleaseWebServiceManager
    {
        #region Contract Release
        public async static Task<ContractReLeaseApplicationFormModel> GetContractReleaseList()
        {

            if (NetworkCheck.IsInternet())
            {
                ContractReLeaseApplicationFormModel _ContractReLeaseApplicationFormDetails = new ContractReLeaseApplicationFormModel();
                string NewToken = string.Empty;
                try
                {
                    string euser2 = "null";
                    string euser3 = "null";
                    string euser4 = "null";
                    string euser5 = "null";


                    char lang = WebServiceManager.GetLangZParameter();
                    string url = ZATCAConstants.ContractReleaseApplicationFormUrl + "CallServ='DCON',HostName='" + "',Bpnum='" + App.LoginDataRetrieved.TIN + "',Zuser='" + "'," +
                       "Auditor='" + "'," +
                     "Lang='" + lang + "',Euser1='" + "''" + "',Euser2='" + euser2 + "',Euser3='" + euser3 + "'," +
                     "Euser4='" + euser4 + "',Euser5='" + euser5 + "',Fbguid='" + "')?$expand=ListSet,AuthServSet&$format=json";
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
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }
                        string _crApplicationFormData = GAZTzakatInstalmentDataResponse.Content.ReadAsStringAsync().Result;
                        _ContractReLeaseApplicationFormDetails = JsonConvert.DeserializeObject<ContractReLeaseApplicationFormModel>(_crApplicationFormData);

                        if (!string.IsNullOrEmpty(_crApplicationFormData) && _ContractReLeaseApplicationFormDetails.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_crApplicationFormData);
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
                    return _ContractReLeaseApplicationFormDetails;
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


        public async static Task<ContractReleaseFormResponse> GAZTGetContractReleaseRequestData()
        {
            ContractReleaseFormResponse _contractReleaseRequestModel = new ContractReleaseFormResponse();

            if (NetworkCheck.IsInternet())
            {

                string NewToken = string.Empty;
                try
                {
                    char lang = WebServiceManager.GetLangZParameter();
                    string url = ZATCAConstants.ContractReleaseRequestUrl + "Auditorz='" + "',Taxpayerz='" + App.LoginDataRetrieved.TIN + "',RegIdz='" + "',Submitz='" + "'," +
                      "Savez='" + "',Fbnumz='" + "',Langz='" + lang + "',PeriodKeyz='" + "'," +
                      "UserTin='" + "')?$expand=znotesSet,AttDetSet&$format=json";
                    HttpResponseMessage _contractReleaseRequestResponse = await GetServiceManager.MakeGetAPICall(url, false, "");



                    if (_contractReleaseRequestResponse != null)
                    {
                        if (_contractReleaseRequestResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _contractReleaseRequestResponse.Headers;
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
                        string _contractReleaseRequestData = _contractReleaseRequestResponse.Content.ReadAsStringAsync().Result;
                        _contractReleaseRequestModel = JsonConvert.DeserializeObject<ContractReleaseFormResponse>(_contractReleaseRequestData);

                        if (!string.IsNullOrEmpty(_contractReleaseRequestData) && _contractReleaseRequestModel.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_contractReleaseRequestData);
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
                    return _contractReleaseRequestModel;
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

        public async static Task<string> GAZTSubmitContractReleaseRequestData(ContractReleaseFormRequest contractReleaseFormData)
        {
            string _contractReleasesubmitResponse = string.Empty;
            try
            {
                string LangZ = WebServiceManager.GetLangZParameterAREN();
                string url = ZATCAConstants.ContractReleaseSubmitUrl;
                var uri = new Uri(url);
                HttpClient client = new HttpClient(App.httpClientHandler);
                var serilized = JsonConvert.SerializeObject(contractReleaseFormData.d);
                client.DefaultRequestHeaders.Add("Token", App.Token);
                client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                _contractReleasesubmitResponse = await res.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(_contractReleasesubmitResponse))
                {
                    ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_contractReleasesubmitResponse);
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
            catch (GAZTVATRegistrationInProcessException ex)
            {
                throw new GAZTVATRegistrationInProcessException(ex.Message);
            }
            catch (Exception)
            {


                App.IsSessionExpired = true;
                return null;
            }
            return _contractReleasesubmitResponse;
        }

        public async static Task<ContractReleaseSummaryModel> GAZTGetContractReleaseSummaryData(string taxpayerz, string fbnumz)
        {
            ContractReleaseSummaryModel _contractReleaseSummaryModel = new ContractReleaseSummaryModel();
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    char lang = WebServiceManager.GetLangZParameter();
                    string url = ZATCAConstants.ContractReleaseSummaryData + "Auditorz='" + "',Taxpayerz='" + App.LoginDataRetrieved.TIN + "',RegIdz='" + "',Submitz='" + "'," +
                      "Savez='" + "',Fbnumz='" + fbnumz + "',Langz='" + lang + "',PeriodKeyz='" + "'," +
                      "UserTin='" + "')?$expand=znotesSet,AttDetSet&$format=json";
                    HttpResponseMessage _contractReleasesummaryResponse = await GetServiceManager.MakeGetAPICall(url, false, "");

                    if (_contractReleasesummaryResponse != null)
                    {
                        if (_contractReleasesummaryResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = _contractReleasesummaryResponse.Headers;
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
                        string _contractReleaseRequestData = _contractReleasesummaryResponse.Content.ReadAsStringAsync().Result;
                        _contractReleaseSummaryModel = JsonConvert.DeserializeObject<ContractReleaseSummaryModel>(_contractReleaseRequestData);
                        if (!string.IsNullOrEmpty(_contractReleaseRequestData) && _contractReleaseSummaryModel.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_contractReleaseRequestData);
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
                    return _contractReleaseSummaryModel;
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
