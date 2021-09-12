using EGAZT.Models.UpdateEffDateModel;
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
using static GAZT.ErrorMessage;

namespace EGAZT.Manager
{
   public static class VatEffectiveDateWebServiceManager
    {
        public async static Task<UpdateVatEffectiveDateModel> GAZTGetIBanAccounts()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                UpdateVatEffectiveDateModel response = new UpdateVatEffectiveDateModel();
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    client.DefaultRequestHeaders.Add("Token", "123");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    string LangZAREN = WebServiceManager.GetLangZParameterAREN();


                    String url = String.Format(Constants.GetRequestedUpdateVatEffDates,App.LoginDataRetrieved.TIN);

                    var uri = new Uri(url);
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
                        String ResponseData = GAZTIBanAccountsResponse.Content.ReadAsStringAsync().Result;
                        response = JsonConvert.DeserializeObject<UpdateVatEffectiveDateModel>(ResponseData);

                        if (!string.IsNullOrEmpty(ResponseData) && response == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(ResponseData);
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
                        return response;

                    }
                    return response;
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
