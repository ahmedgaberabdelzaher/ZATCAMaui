using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EGAZT.Models.AccountStatements;
using GAZT.Helper;
using GAZT.Manager;
using GAZTeServicesBusinessLibrary.GAZTExceptions;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Xamarin.Forms.Internals;

namespace EGAZT.Manager
{
    [Preserve(AllMembers = true)]
    public static class GetAccountStatementWebServiceManager
    {
        #region Account Statements
        public static async Task<ASTabIdentification> GAZTGetAccountStatementsTabIdentification()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                string FbGuid = App.LoginDataRetrieved.FbGuid;
                try
                {
                    ASTabIdentification _asTabIdentification = new ASTabIdentification();
                    char LangZ = WebServiceManager.GetLangZParameter();
                    String Lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.AccountStatementTabIdentification + "Euser=''," + "Fbguid=" + "'" + App.LoginDataRetrieved.FbGuid + "')?$format=json";

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
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")) || (0 == String.Compare(NewToken, "")))
                            {
                                App.IsSessionExpired = true;
                                throw new GAZTSessionExpiredException();
                            }
                            App.Token = NewToken;
                        }
                        String data = GAZTASTabIdentificationStatus.Content.ReadAsStringAsync().Result;
                        _asTabIdentification = JsonConvert.DeserializeObject<ASTabIdentification>(data);
                    }
                    return _asTabIdentification;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                string FbGuid = App.LoginDataRetrieved.FbGuid;
                try
                {
                    ASRevenueDropDownSet _asTabIdentification = new ASRevenueDropDownSet();
                    char LangZ = WebServiceManager.GetLangZParameter();
                    String Lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.AccountStatementRevenueDropDownSet + "Euser eq ''" + " and Fbguid eq '" + App.LoginDataRetrieved.FbGuid + "'" + " and TaxType eq '" + taxType + "'" + " and Langz eq '" + LangZ + "'&$format=json";

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
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")) || (0 == String.Compare(NewToken, "")))
                            {
                                App.IsSessionExpired = true;
                                throw new GAZTSessionExpiredException();
                            }
                            App.Token = NewToken;
                        }
                        String data = GAZTASTabIdentificationStatus.Content.ReadAsStringAsync().Result;
                        _asTabIdentification = JsonConvert.DeserializeObject<ASRevenueDropDownSet>(data);
                    }

                    return _asTabIdentification;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                string FbGuid = App.LoginDataRetrieved.FbGuid;
                try
                {
                    ASStatementHeaderSet _asTabIdentification = new ASStatementHeaderSet();
                    char LangZ = WebServiceManager.GetLangZParameter();
                    String Lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.AccountStatementGetHeaderSet + "Fbguid=" + "'" + App.LoginDataRetrieved.FbGuid + "',StatementFilter='" + statementFilter + "',FiscalYear='" + fiscalYear + "',TaxType='" + taxType + "',Lang='" + LangZ + "')?&$expand=StatmenetLineItemsSet,TaxRelationSet&$format=json";

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
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")) || (0 == String.Compare(NewToken, "")))
                            {
                                App.IsSessionExpired = true;
                                throw new GAZTSessionExpiredException();
                            }
                            App.Token = NewToken;
                        }

                        String data = GAZTASTabIdentificationStatus.Content.ReadAsStringAsync().Result;
                        _asTabIdentification = JsonConvert.DeserializeObject<ASStatementHeaderSet>(data);
                    }

                    return _asTabIdentification;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
            if (CrossConnectivity.Current.IsConnected)
            {
                string NewToken = string.Empty;
                string FbGuid = App.LoginDataRetrieved.FbGuid;
                try
                {
                    ASYearValuesHeader _asTabIdentification = new ASYearValuesHeader();
                    char LangZ = WebServiceManager.GetLangZParameter();
                    String Lang = UtilityManager.GetLanguageParameter();
                    HttpClient client = new HttpClient(App.httpClientHandler);
                    String url = Constants.AccountStatementGetYearValues + "Fguid eq '" + App.LoginDataRetrieved.FbGuid + "'" + " and TaxType eq '" + taxType + "'" + " and StatementFilter eq '" + statementFilter + "'" + "&$format=json";

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
                        if ((!string.IsNullOrEmpty(NewToken)))
                        {
                            if ((0 == String.Compare(NewToken, "Token has expaired")) || (0 == String.Compare(NewToken, "Invalid Token")) || (0 == String.Compare(NewToken, "")))
                            {
                                App.IsSessionExpired = true;
                                throw new GAZTSessionExpiredException();
                            }
                            App.Token = NewToken;
                        }
                        String data = GAZTASTabIdentificationStatus.Content.ReadAsStringAsync().Result;
                        _asTabIdentification = JsonConvert.DeserializeObject<ASYearValuesHeader>(data);
                    }

                    return _asTabIdentification;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write(ex.StackTrace.ToString());
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
