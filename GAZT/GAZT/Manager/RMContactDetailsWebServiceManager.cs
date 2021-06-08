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
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using System.Text;

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


        public static async Task<string> GetVocSurveyCheckAvailability(CheckVocAvailabilityModel checkAvailabilityModel)
        {
          
            if (CrossConnectivity.Current.IsConnected)
            {
                TinDeregistrationResponseModel _newRequestSummaryDataResponse = new TinDeregistrationResponseModel();
                string NewToken = string.Empty;

                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string url = Constants.CheckVocAvailability;

                   


                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "https://tstdg1as1.mygazt.gov.sa");
                  

                    var uri = new Uri(url);
                    var serilized = JsonConvert.SerializeObject(checkAvailabilityModel);

                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    HttpResponseMessage tinDeregResponse = await client.PostAsync(uri, contentPost);

                    if (tinDeregResponse != null)
                    {
                        if (tinDeregResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        String _responseData = tinDeregResponse.Content.ReadAsStringAsync().Result;
                        if (tinDeregResponse.StatusCode == HttpStatusCode.BadRequest)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_responseData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorCode = errorMesg.error.innererror.errordetails[0].code;
                                WebServiceManager.ErrorMessageForUnlockAccount = errorMesg.error.innererror.errordetails[0].message;

                                String WithReplacedString = WebServiceManager.ErrorMessageForUnlockAccount.Replace("An exception was raised", string.Empty);
                                WebServiceManager.ErrorMessageForUnlockAccount = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTErrorException(WebServiceManager.ErrorMessageForUnlockAccount);
                            }
                        }
                        HttpHeaders headers = tinDeregResponse.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                            App.IsSessionExpired = false;
                        }
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expired")) || (0 == String.Compare(NewToken, "Invalid Token")))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }

                        //TinDeregResponseJson = tinDeregResponse.Content.ReadAsStringAsync().Result;

                    }
                    return "";
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


    }
}
