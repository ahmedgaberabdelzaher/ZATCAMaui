using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using ZATCAMAUI;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Mangers;
using ZATCAMAUI.Models.UpdateEffDateModel;
using static ZATCAMAUI.Models.ErrorMessage;

namespace ZATCAMAUI.Core.Manager
{
   public static class VatEffectiveDateWebServiceManager
    {
        public async static Task<UpdateVatEffectiveDateModel> GAZTGetIBanAccounts()
        {
            if (NetworkCheck.IsInternet())
            {
                UpdateVatEffectiveDateModel response = new UpdateVatEffectiveDateModel();
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient();
                    var lang = UtilityManager.GetLanguageParameter();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", "android-20013fbc500");
                    client.DefaultRequestHeaders.Add("X-Device-Name", "Samsung-s20+");
                    client.DefaultRequestHeaders.Add("X-Device-Platform", "android");
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);


                    String url = ZATCAConstants.GetRequestedUpdateVatEffDates+App.LoginDataRetrieved.TIN;

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
                catch (Exception ex)
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
