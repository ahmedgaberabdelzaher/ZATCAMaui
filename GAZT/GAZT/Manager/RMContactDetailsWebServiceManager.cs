using EGAZT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GAZT.Helper;
using GAZT.Manager;
using Newtonsoft.Json;
using Plugin.Connectivity;
using static GAZT.ErrorMessage;

namespace EGAZT.Manager
{
    public class RMContactDetailsWebServiceManager
    {
        public async static Task<RMContactDetailsBaseModel> GetGAZTRMContactDetails(string TinNumber)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                RMContactDetailsBaseModel RmContactsdetailsBaseModel = new RMContactDetailsBaseModel();
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                   // Char lang = WebServiceManager.GetLangZParameter();
                    string lang = WebServiceManager.GetLangZParameterAREN();
                    String url = Constants.GetRMContactDetails+ "=%27"+TinNumber+"%27,Lang=%27"+ lang + "%27)";
                    client.DefaultRequestHeaders.Add("Token", "123");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    var uri = new Uri(url + "?&$format=json");
                    HttpResponseMessage GAZTRMInfo = await client.GetAsync(uri);
                    if (GAZTRMInfo != null)
                    {
                        if (GAZTRMInfo.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTRMInfo.Headers;
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
                        String RmContactInfo = GAZTRMInfo.Content.ReadAsStringAsync().Result;
                        RmContactsdetailsBaseModel = JsonConvert.DeserializeObject<RMContactDetailsBaseModel>(RmContactInfo);

                        if (!string.IsNullOrEmpty(RmContactInfo) && GAZTRMInfo == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(RmContactInfo);
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

                    }
                    return RmContactsdetailsBaseModel;
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
