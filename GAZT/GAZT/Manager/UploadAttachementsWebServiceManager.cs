using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EGAZT.Models;
using GAZT.Helper;
using GAZT.Manager;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Xamarin.Forms.Internals;

namespace EGAZT.Manager
{
    [Preserve(AllMembers = true)]
    public class UploadAttachementsWebServiceManager
    {
        #region Upload Attachements

        public static async Task<AttachmentRootOject> GAZTGenericSaveAttachment(byte[] AttachmentByte, string fileName, string RetGuid, string Dotyp, string contentType, string apiServiceUrl)//, string returnedFguid
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    AttachmentRootOject _attachment = new AttachmentRootOject();
                    char LangZ = WebServiceManager.GetLangZParameter();
                    string AttBy = "TP";
                    if (Dotyp == null)
                    {
                        Dotyp = string.Empty;
                    }
                    String url = Constants.GAZTSaveAttachmentGeneric + "'" + "'" + ",RetGuid='" + RetGuid + "'" + ",Flag='" + "N" + "'" + ",Dotyp='" + Dotyp + "'" + ",SchGuid='" + "'" + ",Srno=" + "1" + ",Doguid='" + "'" + ",AttBy='" + AttBy + "'" + ")/AttachMedSet";
                    url = url.Replace("attachmentServiceurl", apiServiceUrl);
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                 
                    Regex regex = new Regex("[\u0600-\u06ff]|[\u0750-\u077f]|[\ufb50-\ufc3f]|[\ufe70-\ufefc]");
                    var fileNameRemovedSpace = fileName;
                    //Checking if file name is Arabic/Persian
                    if (fileName.Contains("SpaceAdded") && regex.IsMatch(fileName))
                    {
                        fileNameRemovedSpace= WebUtility.UrlEncode(fileName);
                    }
                    client.DefaultRequestHeaders.Add("slug",fileName.Contains("SpaceAdded") ? fileNameRemovedSpace.Replace("SpaceAdded", " ") : WebUtility.UrlEncode(fileName));
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

                    ByteArrayContent baContent = new ByteArrayContent(AttachmentByte);
                    if (!string.IsNullOrEmpty(contentType))
                        baContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                    var response = await client.PostAsync(url, baContent);
                    var responsestr = response.Content.ReadAsStringAsync().Result;
                    _attachment = JsonConvert.DeserializeObject<AttachmentRootOject>(responsestr);
                    return _attachment;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }

        private bool checkIsArabicText(string text)
        {
            Regex regex = new Regex("[\u0600-\u06ff]|[\u0750-\u077f]|[\ufb50-\ufc3f]|[\ufe70-\ufefc]");
            return regex.IsMatch(text);
        }


        public static string GAZTGenericDeleteAttachment(string fileName, string RetGuid, string aPiMethod, string doGuid = "", string doType = "")//, string returnedFguid
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                string DeleteToken = string.Empty;
                try
                {
                    AttachmentRootOject _attachment = new AttachmentRootOject();
                    char LangZ = WebServiceManager.GetLangZParameter();
                    String url = Constants.GAZTDeteleAttachmentNew + aPiMethod + "/AttachMedSet(" + "RetGuid='" + RetGuid + "',Flag='N',Dotyp='"+ doType +"',SchGuid='',Srno=1,Doguid='" + doGuid + "',AttBy='TP',OutletRef='')/$value";
                    url = url.Replace("attachmentServiceurl", aPiMethod);
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);

                    client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("slug", WebUtility.UrlEncode(fileName));

                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "multipart/form-data");
                    HttpResponseMessage res = client.DeleteAsync(url).Result;
                    var responsestr = res.Content.ReadAsStringAsync().Result;
                    _attachment = JsonConvert.DeserializeObject<AttachmentRootOject>(responsestr);
                    if (res != null)
                    {
                        HttpHeaders headers = res.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("delete", out values))
                        {
                            DeleteToken = values.First();
                        }
                        if (res.StatusCode == HttpStatusCode.NoContent)
                            DeleteToken = "X";
                    }
                    return DeleteToken;
                }
                catch (Exception ex)
                {
                    return DeleteToken;
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
