using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.BaseModels;
using ZATCAMAUI.Models.ContractReleas;
using ZATCAMAUI.Models.ContractRelease;
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
                    string deviceOs = DeviceInfo.Platform.ToString();
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DeviceInfo.Model;
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
                    String url = ZATCAConstants.ContractReleaseApplicationFormUrl + App.TP.TIN + "&language=" + lang;
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTzakatInstalmentDataResponse = await client.GetAsync(uri);
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
                        ErrorObj statusHeader = JsonConvert.DeserializeObject<ErrorObj>(_crApplicationFormData);

                        if (statusHeader?.header?.status?.code != "E999999")
                        {
                            _ContractReLeaseApplicationFormDetails = JsonConvert.DeserializeObject<ContractReLeaseApplicationFormModel>(_crApplicationFormData);

                            if (!string.IsNullOrEmpty(_crApplicationFormData) && _ContractReLeaseApplicationFormDetails.d == null)
                            {

                                string errorMessage = WebServiceManager.PrepareErrorMessageByJson(_crApplicationFormData);
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                        else
                        {
                            throw new GAZTNetworkConnectivityIssueException();
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
                    throw new GAZTNetworkConnectivityIssueException();
                }
            }
            else
            {
                throw new InternetException();
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
                    string deviceOs = DeviceInfo.Platform.ToString();
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DeviceInfo.Model;
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

                    String url = ZATCAConstants.ContractReleaseRequestUrl + App.TP.TIN + "&language=" + lang;
                    var uri = new Uri(url);
                    HttpResponseMessage _contractReleaseRequestResponse = await client.GetAsync(uri);
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
                        ErrorObj statusHeader = JsonConvert.DeserializeObject<ErrorObj>(_contractReleaseRequestData);

                        if (statusHeader?.header?.status?.code != "E999999")
                        {
                            _contractReleaseRequestModel = JsonConvert.DeserializeObject<ContractReleaseFormResponse>(_contractReleaseRequestData);
                            if (!string.IsNullOrEmpty(_contractReleaseRequestData) && _contractReleaseRequestModel.d == null)
                            {
                                string errorMessage = WebServiceManager.PrepareErrorMessageByJson(_contractReleaseRequestData);
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                        else
                        {
                            throw new GAZTNetworkConnectivityIssueException();
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
                    throw new GAZTNetworkConnectivityIssueException();
                }
            }
            else
            {
                throw new InternetException();
            }
        }

        public async static Task<DATAPowerBaseResponseResult<CotractResponse>> GAZTSubmitContractReleaseRequestData(ContractReleaseFormRequest contractReleaseFormData)
        {
            if (NetworkCheck.IsInternet())
            {
                string _contractReleasesubmitResponse = string.Empty;
                try
                {
                    string LangZ = WebServiceManager.GetLangZParameterAREN();
                    string url = ZATCAConstants.ContractReleaseSubmitUrl;
                    var uri = new Uri(url);

                    string deviceOs = DeviceInfo.Platform.ToString();
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DeviceInfo.Model;
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", LangZ);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    var serilized = JsonConvert.SerializeObject(contractReleaseFormData.d);
                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                    _contractReleasesubmitResponse = await res.Content.ReadAsStringAsync();
                    ErrorObj statusHeader = JsonConvert.DeserializeObject<ErrorObj>(_contractReleasesubmitResponse);

                    if (statusHeader?.header?.status?.code != "E999999")
                    {
                        var result = JsonConvert.DeserializeObject<DATAPowerBaseResponseResult<CotractResponse>>(_contractReleasesubmitResponse);
                        if (result.result == null)
                        {
                            string errorMessage = WebServiceManager.PrepareErrorMessageByJson(_contractReleasesubmitResponse);
                            throw new GAZTVATRegistrationInProcessException(errorMessage);
                        }
                        return result;
                    }
                    else
                    {
                        throw new GAZTNetworkConnectivityIssueException();
                    }

                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
                }
                catch (Exception)
                {
                    throw new GAZTNetworkConnectivityIssueException();
                }
            }
            else
            {
                throw new InternetException();
            }

        }

        public async static Task<ContractReleaseSummaryModel> GAZTGetContractReleaseSummaryData(string taxpayerz, string fbnumz)
        {
            ContractReleaseSummaryModel _contractReleaseSummaryModel = new ContractReleaseSummaryModel();
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                try
                {
                    string deviceOs = DeviceInfo.Platform.ToString();
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfoZATCA>().GetDeviceUdid();
                    string deviceModel = DeviceInfo.Model;
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
                    String url = ZATCAConstants.ContractReleaseSummaryData + App.TP.TIN + "&language=" + lang + "&formBundleNumber=" + fbnumz;
                    var uri = new Uri(url);
                    HttpResponseMessage _contractReleasesummaryResponse = await client.GetAsync(uri);

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
                        ErrorObj statusHeader = JsonConvert.DeserializeObject<ErrorObj>(_contractReleaseRequestData);

                        if (statusHeader?.header?.status?.code != "E999999")
                        {
                            _contractReleaseSummaryModel = JsonConvert.DeserializeObject<ContractReleaseSummaryModel>(_contractReleaseRequestData);
                            if (!string.IsNullOrEmpty(_contractReleaseRequestData) && _contractReleaseSummaryModel.d == null)
                            {
                                string errorMessage = WebServiceManager.PrepareErrorMessageByJson(_contractReleaseRequestData);
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                        else
                        {
                            throw new GAZTNetworkConnectivityIssueException();
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
                    throw new GAZTNetworkConnectivityIssueException();
                }
            }
            else
            {
                throw new InternetException();
            }
        }
        #endregion
    }
}
