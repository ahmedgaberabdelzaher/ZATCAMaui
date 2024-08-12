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
        public static async Task<string> TaxpayerSubsidyPostRequestAsync(string source)
        {
            string _requestResponse = string.Empty;
            if (NetworkCheck.IsInternet())
            {
                try
                {
                    var lang = UtilityManager.GetLanguageParameter();
                    String url = ZATCAConstants.TaxPayerSubsidyPost;

                    TaxPayerSubsidyModel taxPayerSubsidyModel = new TaxPayerSubsidyModel
                    {
                        authenticationUser = App.LoginDataRetrieved.Euser,
                        formBundleGUID = App.LoginDataRetrieved.FbGuid,
                        language = lang,
                        source = source,
                        TIN = App.LoginDataRetrieved.TIN
                    };


                    string deviceOs = DependencyService.Get<Core.Interfaces.IDeviceInfo>().OperatingSystem;
                    string deviceUdid = DependencyService.Get<Core.Interfaces.IDeviceInfo>().GetDeviceUdid();
                    string deviceModel = DependencyService.Get<Core.Interfaces.IDeviceInfo>().Model;
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient();
                    var serilized = JsonConvert.SerializeObject(taxPayerSubsidyModel);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", deviceUdid);
                    client.DefaultRequestHeaders.Add("X-Device-Name", deviceModel);
                    client.DefaultRequestHeaders.Add("X-Device-Platform", deviceOs);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
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
