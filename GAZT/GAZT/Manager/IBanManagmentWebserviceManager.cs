using EGAZT.Models;
using GAZT.Helper;
using GAZT.Manager;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static EGAZT.Models.IBanManagementListModel;
using static GAZT.ErrorMessage;

namespace EGAZT.Manager
{
    public static class IBanManagmentWebserviceManager
    {

        public async static Task<IBanAccountManagementResponseModel> GAZTGetIBanAccounts()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                IBanAccountManagementResponseModel IBanModelResponse = new IBanAccountManagementResponseModel();
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    Char lang = WebServiceManager.GetLangZParameter();
                    String url = Constants.GetVatEligilibilityDate;
                    client.DefaultRequestHeaders.Add("Token", "123");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    var uri = new Uri("https://tstdg1as1.mygazt.gov.sa:8080/sap/opu/odata/SAP/ZDGW_BANK_MGMT_SRV/HeaderSet(Tin=%273000004986%27,Fbguid=%27%27,Euser=%27%27)?sap-language=EN&$expand=BankListSet,IbanListSet,IdNumberListSet,IdTypeListSet" + "&$format=json");
                    HttpResponseMessage GAZTIBanAccountsResponse = await client.GetAsync(uri);
                    if (GAZTIBanAccountsResponse != null)
                    {
                        if (GAZTIBanAccountsResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTIBanAccountsResponse.Headers;
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
                        String IBanData = GAZTIBanAccountsResponse.Content.ReadAsStringAsync().Result;
                        IBanModelResponse = JsonConvert.DeserializeObject<IBanAccountManagementResponseModel>(IBanData);

                        if (!string.IsNullOrEmpty(IBanData) && IBanModelResponse == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(IBanData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message;
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                throw new Exception(errorMessage);
                            }
                        }
                        return IBanModelResponse;

                    }
                    return IBanModelResponse;
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
