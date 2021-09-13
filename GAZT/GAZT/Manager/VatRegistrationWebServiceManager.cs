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
    public static class VatRegistrationWebServiceManager
    {
        #region VatRegistration

        public async static Task<VatCommencementDateFormat> GAZTGetVATEligibilityDate(string vatEligibleStartDate,string txntpz)
        {
           
            if (CrossConnectivity.Current.IsConnected)
            {
                VatCommencementDateFormat vATcommencementDateResponse = new VatCommencementDateFormat();
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    Char lang = WebServiceManager.GetLangZParameter();
                    String url = Constants.GetVatEligilibilityDate;
                    client.DefaultRequestHeaders.Add("Token", "123");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    var uri = new Uri(url+ "/taxDateSet(VatTaxDt=datetime%27"+ vatEligibleStartDate + "%27,TxnTpz=%27"+ txntpz + "%27,Gpartz=%27"+App.LoginDataRetrieved.TIN+"%27)?&$format=json");
                    HttpResponseMessage GAZTVATRegistrationDataOtherResponse = await client.GetAsync(uri);
                    if (GAZTVATRegistrationDataOtherResponse != null)
                    {
                        if (GAZTVATRegistrationDataOtherResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTVATRegistrationDataOtherResponse.Headers;
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
                        String VatRegistrationOtherData = GAZTVATRegistrationDataOtherResponse.Content.ReadAsStringAsync().Result;
                        vATcommencementDateResponse = JsonConvert.DeserializeObject<VatCommencementDateFormat>(VatRegistrationOtherData);

                        if (!string.IsNullOrEmpty(VatRegistrationOtherData))
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(VatRegistrationOtherData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                WebServiceManager.ErrorMessageForVAT = errorMesg.error.innererror.errordetails[0].message;
                                WebServiceManager.ErrorMessageForVAT += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = WebServiceManager.ErrorMessageForVAT.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                //throw new Exception(errorMessage);
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        } 
                       
                    }
                    return vATcommencementDateResponse;
                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
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

        public async static Task<VATRegistrationDetails> GAZTGetVATRegistrationData()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                VATRegistrationDetails vATRegistrationDetails = new VATRegistrationDetails();
                string NewToken = string.Empty;
                try
                {
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.GAZTGetVATRegistrationData + "',PortalUsrz='" + "',Langz='" + lang + "',Officerz='" + "',Gpartz='" + App.LoginDataRetrieved.TIN + "',TxnTpz='" + "04" + "',Euser='" + "" + "',Fbguid='" + "" + "'" + ")?&$expand=ADDRESSSet,IBANSet,ATTDETSet,CONTACT_PERSONSet,CONTACTDTSet,NOTESSet,QUESTIONSSet,QUESLISTSet,QUESCONFIG_MSet,ELGBL_DOCSet&$format=json";
                    client.DefaultRequestHeaders.Add("Token", "123");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTVATRegistrationDataResponse = await client.GetAsync(uri);
                    if (GAZTVATRegistrationDataResponse != null)
                    {
                        if (GAZTVATRegistrationDataResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTVATRegistrationDataResponse.Headers;
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
                        String VatRegistrationData = GAZTVATRegistrationDataResponse.Content.ReadAsStringAsync().Result;
                        vATRegistrationDetails = JsonConvert.DeserializeObject<VATRegistrationDetails>(VatRegistrationData);
                        if (!string.IsNullOrEmpty(VatRegistrationData) && vATRegistrationDetails.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(VatRegistrationData);
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
                    return vATRegistrationDetails;
                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
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
        public async static Task<VATRegistrationDetails> GAZTGetVATRegistrationData(string pageType)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                VATRegistrationDetails vATRegistrationDetails = new VATRegistrationDetails();
                string NewToken = string.Empty;
                try
                {
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.GAZTGetVATRegistrationData + "',PortalUsrz='" + "',Langz='" + lang + "',Officerz='" + "',Gpartz='" + App.LoginDataRetrieved.TIN + "',TxnTpz='" + pageType + "',Euser='" + "" + "',Fbguid='" + "" + "'" + ")?&$expand=ADDRESSSet,IBANSet,ATTDETSet,CONTACT_PERSONSet,CONTACTDTSet,NOTESSet,QUESTIONSSet,QUESLISTSet,QUESCONFIG_MSet,ELGBL_DOCSet&$format=json";
                    client.DefaultRequestHeaders.Add("Token", "123");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTVATRegistrationDataResponse = await client.GetAsync(uri);
                    if (GAZTVATRegistrationDataResponse != null)
                    {
                        if (GAZTVATRegistrationDataResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTVATRegistrationDataResponse.Headers;
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
                        String VatRegistrationData = GAZTVATRegistrationDataResponse.Content.ReadAsStringAsync().Result;
                        vATRegistrationDetails = JsonConvert.DeserializeObject<VATRegistrationDetails>(VatRegistrationData);
                        if (!string.IsNullOrEmpty(VatRegistrationData) && vATRegistrationDetails.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(VatRegistrationData);
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
                    return vATRegistrationDetails;
                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
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

        public async static Task<VATRegistrationDetails> GAZTGetVATRegistrationDisplayDetailsData()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                VATRegistrationDetails vATRegistrationDetails = new VATRegistrationDetails();
                string NewToken = string.Empty;
                try
                {
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.GAZTGetVATRegistrationData + "',PortalUsrz='" + "',Langz='" + lang + "',Officerz='" + "',Gpartz='" + App.LoginDataRetrieved.TIN + "',TxnTpz='" + "06" + "',Euser='" + "" + "',Fbguid='" + "" + "'" + ")?&$expand=ADDRESSSet,IBANSet,ATTDETSet,CONTACT_PERSONSet,CONTACTDTSet,NOTESSet,QUESTIONSSet,QUESLISTSet,QUESCONFIG_MSet,ELGBL_DOCSet&$format=json";
                    client.DefaultRequestHeaders.Add("Token", "123");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTVATRegistrationDataResponse = await client.GetAsync(uri);
                    if (GAZTVATRegistrationDataResponse != null)
                    {
                        if (GAZTVATRegistrationDataResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTVATRegistrationDataResponse.Headers;
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
                        String VatRegistrationData = GAZTVATRegistrationDataResponse.Content.ReadAsStringAsync().Result;
                        vATRegistrationDetails = JsonConvert.DeserializeObject<VATRegistrationDetails>(VatRegistrationData);
                        if (!string.IsNullOrEmpty(VatRegistrationData) && vATRegistrationDetails.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(VatRegistrationData);
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
                    return vATRegistrationDetails;
                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
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

        public async static Task<VATDeRegistrationDetails> GAZTGetVATDeRegistrationData()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                VATDeRegistrationDetails vATDeRegistrationDetails = new VATDeRegistrationDetails();
                string NewToken = string.Empty;
                try
                {
                    Char lang = WebServiceManager.GetLangZParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.GAZTGetVATDeRegistrationData + "',PortalUsrx='" + "',Langx='" + lang + "',Officerx='" + "',Gpartx='" + App.LoginDataRetrieved.TIN + "',Euser='" + "" + "',FormGuid='" + "" + "',ReviewFg=false)?&$expand=AddressSet,AttdetSet,NotesSet,QuesListSet&$format=json";
                    client.DefaultRequestHeaders.Add("Token", "123");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTVATDeRegistrationDataResponse = await client.GetAsync(uri);
                    if (GAZTVATDeRegistrationDataResponse != null)
                    {
                        if (GAZTVATDeRegistrationDataResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTVATDeRegistrationDataResponse.Headers;
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
                        String VatRegistrationData = GAZTVATDeRegistrationDataResponse.Content.ReadAsStringAsync().Result;
                        vATDeRegistrationDetails = JsonConvert.DeserializeObject<VATDeRegistrationDetails>(VatRegistrationData);
                        if (!string.IsNullOrEmpty(VatRegistrationData) && vATDeRegistrationDetails.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(VatRegistrationData);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                string errorMessage = string.Empty;
                                errorMessage = errorMesg.error.innererror.errordetails[0].message + " ";
                                errorMessage += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = errorMessage.Replace("An exception was raised", string.Empty);
                                errorMessage = WithReplacedString;
                                throw new GAZTVATRegistrationInProcessException(errorMessage);
                            }
                        }
                    }
                    return vATDeRegistrationDetails;
                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
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


        public async static Task<VATRegistrationOtherDetails> GAZTGetVATRegistrationDataWithButtons(string Fbnumz, string Officerz, string Status, string TxnTp, string Formproc)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                VATRegistrationOtherDetails vATRegistrationOtherDetails = new VATRegistrationOtherDetails();
                string NewToken = string.Empty;
                try
                {
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    Char lang = WebServiceManager.GetLangZParameter();
                    String url = Constants.GAZTGetVATRegistrationOtherDetails + Fbnumz + "',Lang='" + lang + "',Officer='" + Officerz + "',Gpart='" + App.LoginDataRetrieved.TIN + "',Status='" + Status + "',TxnTp='" + "CRE_RGVT" + "',Formproc='" + "ZTAX_VT_REG" + "')?&$expand=VR_UI_BTNSet,ELGBL_DOCSet&$format=json";
                    client.DefaultRequestHeaders.Add("Token", "123");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTVATRegistrationDataOtherResponse = await client.GetAsync(uri);
                    if (GAZTVATRegistrationDataOtherResponse != null)
                    {
                        if (GAZTVATRegistrationDataOtherResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTVATRegistrationDataOtherResponse.Headers;
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
                        String VatRegistrationOtherData = GAZTVATRegistrationDataOtherResponse.Content.ReadAsStringAsync().Result;
                        vATRegistrationOtherDetails = JsonConvert.DeserializeObject<VATRegistrationOtherDetails>(VatRegistrationOtherData);

                        if (!string.IsNullOrEmpty(VatRegistrationOtherData) && vATRegistrationOtherDetails == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(VatRegistrationOtherData);
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
                    return vATRegistrationOtherDetails;
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
        public static async Task<VATRegistrationDetails> SaveVATRegistrationData(VATRegistrationDetails vATRegistration)
        {
            VATRegistrationDetails RequestVATRegistration = new VATRegistrationDetails();
            VATRegistrationDetails _vATRegistration = new VATRegistrationDetails();
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    if (vATRegistration != null && vATRegistration.d != null)
                    {
                        if (vATRegistration.d != null)
                        {
                            RequestVATRegistration = vATRegistration;
                            if (vATRegistration.d.ELGBL_DOCSet == null || vATRegistration.d.ELGBL_DOCSet.results == null)
                            {
                                ELGBL_DOCSetforsubmit eLGBL_DOCSet = new ELGBL_DOCSetforsubmit();
                                eLGBL_DOCSet.results = new List<ResultsItemForDOCSetforsubmit>();
                                RequestVATRegistration.d.ELGBL_DOCSet = eLGBL_DOCSet;

                            }
                            ATTDETSet aTTACHSet = new ATTDETSet();
                            aTTACHSet.results = new List<Attachment>();
                            RequestVATRegistration.d.ATTDETSet = aTTACHSet;
                        }
                        string LangZAREN = WebServiceManager.GetLangZParameterAREN();

                        char LangZ = WebServiceManager.GetLangZParameter();
                        string lang = UtilityManager.GetLanguageParameter();
                        String url = Constants.SaveVATRegistration + LangZAREN;
                        vATRegistration.d.Langz = lang;
                        var uri = new Uri(url);
                        HttpClient client = new HttpClient(App.httpClientHandler);

                        client.DefaultRequestHeaders.Add("Token", "123");
                        client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

                        client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                        client.DefaultRequestHeaders.Add("Accept", "application/json");

                        var serilized = JsonConvert.SerializeObject(RequestVATRegistration);
                        HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                        HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                        var detailJson = res.Content.ReadAsStringAsync().Result;
                        _vATRegistration = JsonConvert.DeserializeObject<VATRegistrationDetails>(detailJson);
                        if (_vATRegistration != null)
                        {
                            if (_vATRegistration.d != null)
                            {
                                if (_vATRegistration.d.NOTESSet == null)
                                {
                                    NOTESSet nOTEs = new NOTESSet();
                                    nOTEs.results = new List<Note>();
                                    _vATRegistration.d.NOTESSet = nOTEs;
                                }
                                if (_vATRegistration.d.IBANSet == null)
                                {
                                    IBANSet iBANSet = new IBANSet();
                                    iBANSet.results = new List<Result2>();
                                    _vATRegistration.d.IBANSet = iBANSet;
                                }
                                if (_vATRegistration.d.ADDRESSSet == null)
                                {
                                    ADDRESSSet aDDRESSSet = new ADDRESSSet();
                                    aDDRESSSet.results = new List<ResultsItem>();
                                    _vATRegistration.d.ADDRESSSet = aDDRESSSet;
                                }
                                if (_vATRegistration.d.ATTDETSet == null)
                                {
                                    ATTDETSet aTTDETSet = new ATTDETSet();
                                    aTTDETSet.results = new List<Attachment>();
                                    _vATRegistration.d.ATTDETSet = aTTDETSet;
                                }
                                if (_vATRegistration.d.CONTACTDTSet == null)
                                {
                                    CONTACTDTSet cONTACTDT = new CONTACTDTSet();
                                    cONTACTDT.results = new List<ResultsItemForContact>();
                                    _vATRegistration.d.CONTACTDTSet = cONTACTDT;
                                }
                                if (_vATRegistration.d.CONTACT_PERSONSet == null)
                                {
                                    CONTACT_PERSONSet cONTACT_PERSONSet = new CONTACT_PERSONSet();
                                    cONTACT_PERSONSet.results = new List<ResultsItemForContactPerson>();
                                    _vATRegistration.d.CONTACT_PERSONSet = cONTACT_PERSONSet;
                                }
                                if (_vATRegistration.d.ELGBL_DOCSet == null)
                                {
                                    ELGBL_DOCSetforsubmit eLGBL_DOC = new ELGBL_DOCSetforsubmit();
                                    eLGBL_DOC.results = new List<ResultsItemForDOCSetforsubmit>();
                                    _vATRegistration.d.ELGBL_DOCSet = eLGBL_DOC;
                                }
                                if (_vATRegistration.d.QUESTIONSSet == null)
                                {
                                    QUESTIONSSet qUESTIONSSet = new QUESTIONSSet();
                                    qUESTIONSSet.results = new List<ResultsItemForQuestion>();
                                    _vATRegistration.d.QUESTIONSSet = qUESTIONSSet;
                                }
                                if (_vATRegistration.d.QUESCONFIG_MSet == null)
                                {
                                    QUESCONFIG_MSet qUESCONFIG = new QUESCONFIG_MSet();
                                    qUESCONFIG.results = new List<QuestionsetWithMinMax>();
                                    _vATRegistration.d.QUESCONFIG_MSet = qUESCONFIG;
                                }
                                if (_vATRegistration.d.QUESLISTSet == null)
                                {
                                    QUESLISTSet qUESLIST = new QUESLISTSet();
                                    qUESLIST.results = new List<string>();
                                    _vATRegistration.d.QUESLISTSet = qUESLIST;
                                }




                            }
                        }
                        if (_vATRegistration == null || _vATRegistration.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                WebServiceManager.ErrorMessageForVAT = errorMesg.error.innererror.errordetails[0].message;
                                WebServiceManager.ErrorMessageForVAT += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = WebServiceManager.ErrorMessageForVAT.Replace("An exception was raised", string.Empty);
                                WebServiceManager.ErrorMessageForVAT = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(WebServiceManager.ErrorMessageForVAT);
                            }
                        }
                        return _vATRegistration;
                    }
                    return _vATRegistration;
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

        public static async Task<VATDeRegistrationDetails> SaveVATDeRegistrationData(VATDeRegistrationDetails vATDeRegistration)
        {
            VATDeRegistrationDetails RequestVATDeRegistration = new VATDeRegistrationDetails();
            VATDeRegistrationDetails _vATDeRegistration = new VATDeRegistrationDetails();
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    if (vATDeRegistration != null && vATDeRegistration.d != null)
                    {
                        if (vATDeRegistration.d != null)
                        {
                            RequestVATDeRegistration = vATDeRegistration;

                            AttdetSet aTTACHSet = new AttdetSet();
                            aTTACHSet.results = new List<Attachment>();
                            RequestVATDeRegistration.d.AttdetSet = aTTACHSet;
                        }
                        string LangZAREN = WebServiceManager.GetLangZParameterAREN();

                        char LangZ = WebServiceManager.GetLangZParameter();
                        string lang = UtilityManager.GetLanguageParameter();
                        String url = Constants.SaveVATDeRegistration;
                        vATDeRegistration.d.Langx = lang;
                        var uri = new Uri(url);
                        HttpClient client = new HttpClient(App.httpClientHandler);

                        client.DefaultRequestHeaders.Add("Token", "123");
                        client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

                        client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                        client.DefaultRequestHeaders.Add("Accept", "application/json");

                        var serilized = JsonConvert.SerializeObject(RequestVATDeRegistration);
                        HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                        HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                        var detailJson = res.Content.ReadAsStringAsync().Result;
                        _vATDeRegistration = JsonConvert.DeserializeObject<VATDeRegistrationDetails>(detailJson);
                        if (_vATDeRegistration != null)
                        {
                            if (_vATDeRegistration.d != null)
                            {
                                if (_vATDeRegistration.d.NotesSet == null)
                                {
                                    NotesSet nOTEs = new NotesSet();
                                    nOTEs.results = new List<VATDeregNote>();
                                    _vATDeRegistration.d.NotesSet = nOTEs;
                                }


                                if (_vATDeRegistration.d.AttdetSet == null)
                                {
                                    AttdetSet aTTDETSet = new AttdetSet();
                                    aTTDETSet.results = new List<Attachment>();
                                    _vATDeRegistration.d.AttdetSet = aTTDETSet;
                                }

                                if (_vATDeRegistration.d.AddressSet == null)
                                {
                                    AddressSet addressSet = new AddressSet();
                                    addressSet.results = new List<ResultsItemSet>();
                                    _vATDeRegistration.d.AddressSet = addressSet;
                                }

                                if (_vATDeRegistration.d.QuesListSet == null)
                                {
                                    QuesListSet quesList = new QuesListSet();
                                    quesList.results = new List<string>();
                                    _vATDeRegistration.d.QuesListSet = quesList;
                                }




                            }
                        }
                        if (_vATDeRegistration == null || _vATDeRegistration.d == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                WebServiceManager.ErrorMessageForVAT = errorMesg.error.innererror.errordetails[0].message;
                                WebServiceManager.ErrorMessageForVAT += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = WebServiceManager.ErrorMessageForVAT.Replace("An exception was raised", string.Empty);
                                WebServiceManager.ErrorMessageForVAT = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTVATRegistrationInProcessException(WebServiceManager.ErrorMessageForVAT);
                            }
                        }
                        return _vATDeRegistration;
                    }
                    return _vATDeRegistration;
                }
                catch (GAZTVATRegistrationInProcessException ex)
                {
                    throw new GAZTVATRegistrationInProcessException(ex.Message);
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

        public static async Task<UnlockAccountResponseModel> GaztUnlockAccount(UnlockAccountModel unlockAccountModel)
        {
            UnlockAccountResponseModel _unlockResponseModel = new UnlockAccountResponseModel();

            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    if (unlockAccountModel != null)
                    {
                        string LangZAREN = WebServiceManager.GetLangZParameterAREN();

                        char LangZ = WebServiceManager.GetLangZParameter();
                        string lang = UtilityManager.GetLanguageParameter();
                        String url = Constants.GAZTUnlockAccountAllOperations;

                        unlockAccountModel.Language = LangZ.ToString();
                        unlockAccountModel.UserLocked = "L";

                        var uri = new Uri(url);
                        HttpClient client = new HttpClient(App.httpClientHandler);

                        client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                        client.DefaultRequestHeaders.Add("Accept", "application/json");

                        var serilized = JsonConvert.SerializeObject(unlockAccountModel);
                        HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                        HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                        var detailJson = res.Content.ReadAsStringAsync().Result;
                        _unlockResponseModel = JsonConvert.DeserializeObject<UnlockAccountResponseModel>(detailJson);

                        if (_unlockResponseModel == null || _unlockResponseModel.D == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                WebServiceManager.ErrorMessageForUnlockAccount = errorMesg.error.innererror.errordetails[0].message;
                                WebServiceManager.ErrorMessageForUnlockAccount += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = WebServiceManager.ErrorMessageForUnlockAccount.Replace("An exception was raised", string.Empty);
                                WebServiceManager.ErrorMessageForUnlockAccount = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTUnlockAccountException(WebServiceManager.ErrorMessageForUnlockAccount);
                            }
                        }

                        return _unlockResponseModel;
                    }
                    return _unlockResponseModel;
                }
                catch (GAZTUnlockAccountException ex)
                {
                    throw new GAZTUnlockAccountException(ex.Message);
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

        public static async Task<UnlockAccountResponseModel> GaztUnlockAccountOtp(UnlockAccountModelOtp unlockAccountModel)
        {
            UnlockAccountResponseModel _unlockResponseModel = new UnlockAccountResponseModel();

            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    if (unlockAccountModel != null)
                    {
                        //string url = Constants.GAZTSignUpFirstSubmit;
                        string LangZAREN = WebServiceManager.GetLangZParameterAREN();

                        char LangZ = WebServiceManager.GetLangZParameter();
                        string lang = UtilityManager.GetLanguageParameter();
                        String url = Constants.GAZTUnlockAccountAllOperations;

                        unlockAccountModel.Language = LangZ.ToString();
                        unlockAccountModel.UserLocked = "L";

                        var uri = new Uri(url);
                        HttpClient client = new HttpClient(App.httpClientHandler);

                        client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                        client.DefaultRequestHeaders.Add("Accept", "application/json");

                        var serilized = JsonConvert.SerializeObject(unlockAccountModel);
                        HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                        HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                        var detailJson = res.Content.ReadAsStringAsync().Result;
                        _unlockResponseModel = JsonConvert.DeserializeObject<UnlockAccountResponseModel>(detailJson);

                        if (_unlockResponseModel == null || _unlockResponseModel.D == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                WebServiceManager.ErrorMessageForUnlockAccount = errorMesg.error.innererror.errordetails[0].message;
                                WebServiceManager.ErrorMessageForUnlockAccount += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = WebServiceManager.ErrorMessageForUnlockAccount.Replace("An exception was raised", string.Empty);
                                WebServiceManager.ErrorMessageForUnlockAccount = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTUnlockAccountException(WebServiceManager.ErrorMessageForUnlockAccount);
                            }
                        }

                        return _unlockResponseModel;
                    }
                    return _unlockResponseModel;
                }
                catch (GAZTUnlockAccountException ex)
                {
                    throw new GAZTUnlockAccountException(ex.Message);
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

        public static async Task<UnlockAccountResponseModel> GaztUnlockAccountChangePassword(UnlockAccountModelChangePassword unlockAccountModel)
        {
            UnlockAccountResponseModel _unlockResponseModel = new UnlockAccountResponseModel();

            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    if (unlockAccountModel != null)
                    {
                        //string url = Constants.GAZTSignUpFirstSubmit;
                        string LangZAREN = WebServiceManager.GetLangZParameterAREN();

                        char LangZ = WebServiceManager.GetLangZParameter();
                        string lang = UtilityManager.GetLanguageParameter();
                        String url = Constants.GAZTUnlockAccountAllOperations;

                        unlockAccountModel.Language = LangZ.ToString();
                        unlockAccountModel.UserLocked = "L";

                        var uri = new Uri(url);
                        HttpClient client = new HttpClient(App.httpClientHandler);

                        client.DefaultRequestHeaders.Add("X-Requested-With", "X");
                        client.DefaultRequestHeaders.Add("Accept", "application/json");

                        var serilized = JsonConvert.SerializeObject(unlockAccountModel);
                        HttpContent contentPost = new StringContent(serilized, Encoding.UTF8, Constants.ContentType);
                        HttpResponseMessage res = await client.PostAsync(uri, contentPost);
                        var detailJson = res.Content.ReadAsStringAsync().Result;
                        _unlockResponseModel = JsonConvert.DeserializeObject<UnlockAccountResponseModel>(detailJson);

                        if (_unlockResponseModel == null || _unlockResponseModel.D == null)
                        {
                            ErrorObj errorMesg = JsonConvert.DeserializeObject<ErrorObj>(detailJson);
                            if (errorMesg != null && errorMesg.error != null && errorMesg.error.innererror != null && errorMesg.error.innererror.errordetails != null && errorMesg.error.innererror.errordetails[0].message != null)
                            {
                                WebServiceManager.ErrorMessageForUnlockAccount = errorMesg.error.innererror.errordetails[0].message;
                                WebServiceManager.ErrorMessageForUnlockAccount += errorMesg.error.innererror.errordetails[1].message;
                                String WithReplacedString = WebServiceManager.ErrorMessageForUnlockAccount.Replace("An exception was raised", string.Empty);
                                WebServiceManager.ErrorMessageForUnlockAccount = WithReplacedString;
                                //ErrorMessageForVAT
                                throw new GAZTUnlockAccountException(WebServiceManager.ErrorMessageForUnlockAccount);
                            }
                        }

                        return _unlockResponseModel;
                    }
                    return _unlockResponseModel;
                }
                catch (GAZTUnlockAccountException ex)
                {
                    throw new GAZTUnlockAccountException(ex.Message);
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


        #endregion
    }
}
