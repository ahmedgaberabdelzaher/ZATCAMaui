using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Models;
using ZATCAMAUI.Models.Attachments;
using ZATCAMAUI.Models.EstablishmentRegistration;

namespace ZATCAMAUI.Core.Mangers
{

    public class UploadAttachementsWebServiceManager
    {
        #region Upload Attachements

        public static async Task<AttachmentRootOject> GAZTGenericSaveAttachment(byte[] AttachmentByte, string fileName, string RetGuid, string Dotyp, string contentType, string apiServiceUrl, string outletRef)//, string returnedFguid
        {
            if (NetworkCheck.IsInternet())
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
                    String url = ZATCAConstants.GAZTSaveAttachmentGeneric + outletRef + "&returnGUID=" + RetGuid + "&attachmentFlag=New" + "&documentCategory=" + Dotyp + "&serialNumber=1" + "&attachedByPerson=" + AttBy + /*"&fileName=" + fileName +*/ "&documentId=" + "&fileName=" + fileName;
       
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    client.Timeout = TimeSpan.FromMinutes(5);
                    var lang = UtilityManager.GetLanguageParameter();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("X-Device-Id", "android-20013fbc500");
                    client.DefaultRequestHeaders.Add("X-Device-Name", "Samsung-s20+");
                    client.DefaultRequestHeaders.Add("X-Device-Platform", "android");
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    //client.DefaultRequestHeaders.Add("Content-Type", "multipart/form-data");
                    client.DefaultRequestHeaders.Add("X-Message-Id", "58");
                    Regex regex = new Regex("[\u0600-\u06ff]|[\u0750-\u077f]|[\ufb50-\ufc3f]|[\ufe70-\ufefc]");
                    var fileNameRemovedSpace = fileName;
                    //Checking if file name is Arabic/Persian
                    if (fileName.Contains("SpaceAdded") && regex.IsMatch(fileName))
                    {
                        fileNameRemovedSpace = WebUtility.UrlEncode(fileName);
                    }
                    ByteArrayContent baContent = new ByteArrayContent(AttachmentByte);
                    /*if (!string.IsNullOrEmpty(contentType))
                        baContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);*/

                    var content = new MultipartFormDataContent();
                    var fileContent = new StreamContent(new MemoryStream(AttachmentByte));
                    fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = "attachmentFile",
                        FileName = fileName
                    };
                    content.Add(fileContent, "attachmentFile");

                    var response = await client.PostAsync(url, content);
                    var responsestr = response.Content.ReadAsStringAsync().Result;
                    _attachment = JsonConvert.DeserializeObject<AttachmentRootOject>(responsestr);
                    return _attachment;
                }
                catch (Exception)
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
            if (NetworkCheck.IsInternet())
            {
                string DeleteToken = string.Empty;
                try
                {
                    Models.Attachments.DeleteAttachmentRequest _attachment = new Models.Attachments.DeleteAttachmentRequest()
                    {
                        fileName = fileName,
                        returnGUID = RetGuid,
                        documentCategory = doType,
                        documentId = doGuid,
                        serialNumber = "0"
                    };

                    var lang = UtilityManager.GetLanguageParameter();
                    // String url = Constants.GAZTDeteleAttachmentNew + aPiMethod + "/AttachMedSet(" + "RetGuid='" + RetGuid + "',Flag='N',Dotyp='"+ doType +"',SchGuid='',Srno=1,Doguid='" + doGuid + "',AttBy='TP',OutletRef='')/$value";
                    //String url = Constants.GAZTDeteleAttachmentNew + aPiMethod + "&returnGUID="+RetGuid+ "&attachment=New"+ "&documentCategory="+doType+ "&serialNumber=1"+ "&attachedByPerson=TP"+ "&outletReference=" + "&documentId="+doGuid + "&fileName=" + fileName;
                    String url = ZATCAConstants.GAZTDeteleAttachmentNew;
                    // url = url.Replace("attachmentServiceurl", aPiMethod);
                    var uri = new Uri(url);
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-Session-Language", lang);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Id", ZATCAConstants.ClientId);
                    client.DefaultRequestHeaders.Add("X-ZATCA-Client-Secret", ZATCAConstants.ClientSecret);
                    client.DefaultRequestHeaders.Add("Authorization", App.Token);
                    //client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                    //client.DefaultRequestHeaders.Add("Accept", "application/json");
                    //client.DefaultRequestHeaders.Add("slug", WebUtility.UrlEncode(fileName));
                    var serializeOptions = new JsonSerializerSettings
                    {
                        DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                        DateTimeZoneHandling = DateTimeZoneHandling.Utc
                    };
                    serializeOptions.Converters.Add(new JsonFieldListConverter());
                    var serialized = JsonConvert.SerializeObject(_attachment, serializeOptions);

                    HttpContent contentPost = new StringContent(serialized, Encoding.UTF8, ZATCAConstants.ContentType);
                    //      client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "multipart/form-data");
                    HttpResponseMessage res = client.PostAsync(uri, contentPost).Result;
                    var responsestr = res.Content.ReadAsStringAsync().Result;
                    // _attachment = JsonConvert.DeserializeObject<DeleteAttachmentRequest>(responsestr);
                    if (res != null)
                    {
                        //HttpHeaders headers = res.Headers;
                        //IEnumerable<string> values;
                        //if (headers.TryGetValues("delete", out values))
                        //{
                        //    DeleteToken = values.First();
                        //}
                        if (res.StatusCode == HttpStatusCode.NoContent || res.StatusCode == HttpStatusCode.OK)
                            DeleteToken = "X";
                    }
                    return DeleteToken;
                }
                catch (Exception)
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
