using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EGAZT.Models;
using GAZT.Helper;
using GAZT.Manager;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Xamarin.Forms.Internals;
using static GAZT.ErrorMessage;

namespace EGAZT.Manager
{
    [Preserve(AllMembers = true)]
    public static class TaxpayerSubsidyWebServiceManager
    {
        public static async Task<string> TaxpayerSubsidyPostRequestAsync(string source)
        {
            string _requestResponse = string.Empty;
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    char LangZ = WebServiceManager.GetLangZParameter();
                    String url = Constants.TaxPayerSubsidyPost;

                    TaxPayerSubsidyModel taxPayerSubsidyModel = new TaxPayerSubsidyModel
                    {
                        Euser = App.LoginDataRetrieved.Euser,
                        Fbguid = App.LoginDataRetrieved.FbGuid,
                        Langz = "" + LangZ,
                        Source = source,//"VSUB" or 6741
                        Partner = App.LoginDataRetrieved.TIN
                    };



                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var serilized = JsonConvert.SerializeObject(taxPayerSubsidyModel);
                    client.DefaultRequestHeaders.Add("Token", App.Token);
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                    HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                     
                    _requestResponse = await res.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(_requestResponse))
                    {
                        ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(_requestResponse);
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
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
                    
                }
                catch (Exception)
                {
                    
                    
                    //App.IsSessionExpired = true;
                    return null;
                }
                return _requestResponse;
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }
    }
}
