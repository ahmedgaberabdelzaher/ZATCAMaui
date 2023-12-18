using System.Text;
using Newtonsoft.Json;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Models;
using static ZATCAMAUI.Models.ErrorMessage;

namespace ZATCAMAUI.Core.Mangers
{
   
    public static class TaxpayerSubsidyWebServiceManager
    {
        public static async Task<string> TaxpayerSubsidyPostRequestAsync()
        {
            string _requestResponse = string.Empty;
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    char LangZ = WebServiceManager.GetLangZParameter();
                    string url = ZATCAConstants.TaxPayerSubsidyPost;

                    TaxPayerSubsidyModel taxPayerSubsidyModel = new TaxPayerSubsidyModel
                    {
                        Euser = App.LoginDataRetrieved.Euser,
                        Fbguid = App.LoginDataRetrieved.FbGuid,
                        Langz = "" + LangZ,
                        Source = "MSUB",//"VSUB"
                        Partner = App.LoginDataRetrieved.TIN
                    };



                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    var serilized = JsonConvert.SerializeObject(taxPayerSubsidyModel);
                    client.DefaultRequestHeaders.Add("Token", App.Token);
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, ZATCAConstants.ContentType);
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
