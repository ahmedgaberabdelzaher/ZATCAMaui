using System.Net;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Models.AccountStatements;

namespace ZATCAMAUI.Core.Mangers
{

    public static class GetAccountStatementWebServiceManager
    {
        #region Account Statements
        public static async Task<ASTabIdentification> GAZTGetAccountStatementsTabIdentification()
        {
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                string FbGuid = App.LoginDataRetrieved.FbGuid;
                try
                {
                    ASTabIdentification _asTabIdentification = new ASTabIdentification();
                    char LangZ = WebServiceManager.GetLangZParameter();
                    string Lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string url = ZATCAConstants.AccountStatementTabIdentification + "Euser=''," + "Fbguid=" + "'" + App.LoginDataRetrieved.FbGuid + "')?$format=json";

                    client.DefaultRequestHeaders.Add("Token", "123");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

                    var uri = new Uri(url);
                    HttpResponseMessage GAZTASTabIdentificationStatus = await client.GetAsync(uri);
                    if (GAZTASTabIdentificationStatus != null)
                    {
                        if (GAZTASTabIdentificationStatus.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            throw new GAZTSessionExpiredException();
                        }
                        HttpHeaders headers = GAZTASTabIdentificationStatus.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                        }
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token") || 0 == string.Compare(NewToken, ""))
                            {
                                App.IsSessionExpired = true;
                                throw new GAZTSessionExpiredException();
                            }
                            App.Token = NewToken;
                        }
                        string data = GAZTASTabIdentificationStatus.Content.ReadAsStringAsync().Result;
                        _asTabIdentification = JsonConvert.DeserializeObject<ASTabIdentification>(data);
                    }
                    return _asTabIdentification;
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

        public static async Task<ASRevenueDropDownSet> GAZTGetAccountStatementsRevenueDropDownSet(string taxType)
        {
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                string FbGuid = App.LoginDataRetrieved.FbGuid;
                try
                {
                    ASRevenueDropDownSet _asTabIdentification = new ASRevenueDropDownSet();
                    char LangZ = WebServiceManager.GetLangZParameter();
                    string Lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string url = ZATCAConstants.AccountStatementRevenueDropDownSet + "Euser eq ''" + " and Fbguid eq '" + App.LoginDataRetrieved.FbGuid + "'" + " and TaxType eq '" + taxType + "'" + " and Langz eq '" + LangZ + "'&$format=json";

                    client.DefaultRequestHeaders.Add("Token", "123");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

                    var uri = new Uri(url);
                    HttpResponseMessage GAZTASTabIdentificationStatus = await client.GetAsync(uri);
                    if (GAZTASTabIdentificationStatus != null)
                    {
                        if (GAZTASTabIdentificationStatus.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            throw new GAZTSessionExpiredException();
                        }
                        HttpHeaders headers = GAZTASTabIdentificationStatus.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                        }
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token") || 0 == string.Compare(NewToken, ""))
                            {
                                App.IsSessionExpired = true;
                                throw new GAZTSessionExpiredException();
                            }
                            App.Token = NewToken;
                        }
                        string data = GAZTASTabIdentificationStatus.Content.ReadAsStringAsync().Result;
                        _asTabIdentification = JsonConvert.DeserializeObject<ASRevenueDropDownSet>(data);
                    }

                    return _asTabIdentification;
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

        public static async Task<ASStatementHeaderSet> GAZTGetAccountStatementHeaderSet(string statementFilter, string fiscalYear, string taxType)
        {
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                string FbGuid = App.LoginDataRetrieved.FbGuid;
                try
                {
                    ASStatementHeaderSet _asTabIdentification = new ASStatementHeaderSet();
                    char LangZ = WebServiceManager.GetLangZParameter();
                    string Lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string url = ZATCAConstants.AccountStatementGetHeaderSet + "Fbguid=" + "'" + App.LoginDataRetrieved.FbGuid + "',StatementFilter='" + statementFilter + "',FiscalYear='" + fiscalYear + "',TaxType='" + taxType + "',Lang='" + LangZ + "')?&$expand=StatmenetLineItemsSet,TaxRelationSet&$format=json";

                    client.DefaultRequestHeaders.Add("Token", "123");
                    client.DefaultRequestHeaders.Add("ichannel", App.IncomingChannel);

                    var uri = new Uri(url);
                    HttpResponseMessage GAZTASTabIdentificationStatus = await client.GetAsync(uri);

                    if (GAZTASTabIdentificationStatus != null)
                    {
                        if (GAZTASTabIdentificationStatus.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            throw new GAZTSessionExpiredException();
                        }
                        HttpHeaders headers = GAZTASTabIdentificationStatus.Headers;
                        IEnumerable<string> values;

                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                        }
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token") || 0 == string.Compare(NewToken, ""))
                            {
                                App.IsSessionExpired = true;
                                throw new GAZTSessionExpiredException();
                            }
                            App.Token = NewToken;
                        }

                        string data = GAZTASTabIdentificationStatus.Content.ReadAsStringAsync().Result;
                        _asTabIdentification = JsonConvert.DeserializeObject<ASStatementHeaderSet>(data);
                    }

                    return _asTabIdentification;
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

        public static async Task<ASYearValuesHeader> GAZTGetAccountStatementYearValuesHeaderSet(string statementFilter, string taxType)
        {
            if (NetworkCheck.IsInternet())
            {
                string NewToken = string.Empty;
                string FbGuid = App.LoginDataRetrieved.FbGuid;
                try
                {
                    ASYearValuesHeader _asTabIdentification = new ASYearValuesHeader();
                    char LangZ = WebServiceManager.GetLangZParameter();
                    string Lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    string url = ZATCAConstants.AccountStatementGetYearValues + "Fguid eq '" + App.LoginDataRetrieved.FbGuid + "'" + " and TaxType eq '" + taxType + "'" + " and StatementFilter eq '" + statementFilter + "'" + "&$format=json";

                    client.DefaultRequestHeaders.Add("Token", "123");
                    var uri = new Uri(url);
                    HttpResponseMessage GAZTASTabIdentificationStatus = await client.GetAsync(uri);
                    if (GAZTASTabIdentificationStatus != null)
                    {
                        if (GAZTASTabIdentificationStatus.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            throw new GAZTSessionExpiredException();
                        }
                        HttpHeaders headers = GAZTASTabIdentificationStatus.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                        }
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expaired") || 0 == string.Compare(NewToken, "Invalid Token") || 0 == string.Compare(NewToken, ""))
                            {
                                App.IsSessionExpired = true;
                                throw new GAZTSessionExpiredException();
                            }
                            App.Token = NewToken;
                        }
                        string data = GAZTASTabIdentificationStatus.Content.ReadAsStringAsync().Result;
                        _asTabIdentification = JsonConvert.DeserializeObject<ASYearValuesHeader>(data);
                    }

                    return _asTabIdentification;
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
