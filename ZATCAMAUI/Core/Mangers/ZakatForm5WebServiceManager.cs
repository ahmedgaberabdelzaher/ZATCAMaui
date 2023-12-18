using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZATCAMAUI.Core.Exceptions;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Models.Form5Models;

namespace ZATCAMAUI.Core.Mangers
{

    public static class ZakatForm5WebServiceManager
    {
        #region ZakatForm5
        #region Basic and Financial Information
        public static async Task<ZakatForm5DataResult> GAZTZakatForm5Data(string Fbguid)
        {
            if (NetworkCheck.IsInternet())
            {
                ZakatForm5DataResult ZakatForm5DataResultSet = new ZakatForm5DataResult();
                string NewToken = string.Empty;
                try
                {
                    char Lang = WebServiceManager.GetLangZParameter();
                    string url = ZATCAConstants.Z_RET_F05_ZKTE + "(Auditorz='',Taxpayerz='',RegIdz='',PeriodKeyz='',Submitz='',Savez='',Fbnumz='',Langz='" + Lang + "',OfficerUidz='',ObjSubmitz='',Approvez='',Rejectz='',CreateTxAssesz='',Euser='" + App.TP.Userid + "',Fbguid='" + Fbguid + "')?&$expand=GEN_SUB_SCH,GP03_2Set,GP03_3Set,GP03_4Set,GP03_5Set,GP03_6Set,GP03_7Set,GP03_8Set,GP06_1Set,GP06_2Set,GP06_3Set,MAIN_ACTIVITYSet,SCH_GP01,SCH_GP02,SCH_GP03,SCH_GP04,SCH_GP05,SCH_GP06,SCH_GP07,SCH_GP08,SCH_GP09,SCH_GP10,SCH_GP11,SCH_GP12,SUB_SCH_CAPITALSet,SCH_200Set,SCH_800Set,SCH_GP3S1Set,SCH_GP3S2Set,AttDetSet,LONG_TEXTSet&$format=json";
                    HttpResponseMessage GAZTZakatForm5Response = await GetServiceManager.MakeGetAPICallForZakatForm5Response(url, false, "");
                    if (GAZTZakatForm5Response != null)
                    {
                        if (GAZTZakatForm5Response.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTZakatForm5Response.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                            App.IsSessionExpired = false;
                        }
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }

                        string GAZTZakatForm5ResponseJSON = GAZTZakatForm5Response.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(GAZTZakatForm5ResponseJSON))
                        {
                            GAZTZakatForm5ResponseJSON = JObject.Parse(GAZTZakatForm5ResponseJSON)["d"].ToString();

                            ZakatForm5DataResultSet = JsonConvert.DeserializeObject<ZakatForm5DataResult>(GAZTZakatForm5ResponseJSON);
                            if (ZakatForm5DataResultSet == null)
                            {
                                throw new Exception(AppResources.NoTINsAvailable);
                            }
                        }
                        else
                        {
                            throw new Exception(AppResources.ZNoICRAvailable);
                        }
                    }
                    return ZakatForm5DataResultSet;
                }
                catch (Exception)
                {
                    App.IsSessionExpired = true;
                    throw;
                }
            }
            else
            {
                throw new InternetException(AppResources.ZZInternetConnectionMessage);
            }
        }
        #endregion
        #region City
        public static async Task<ZakatForm5CityDataResult> GAZTZakatForm5CityData()
        {
            if (NetworkCheck.IsInternet())
            {
                ZakatForm5CityDataResult ZakatForm5CityDataResultSet = new ZakatForm5CityDataResult();
                string NewToken = string.Empty;
                try
                {
                    char lang = WebServiceManager.GetLangZParameter();
                    string Lang = WebServiceManager.GetLangZParameterAREN();
                    string url = ZATCAConstants.Z_RET_F05_City + "(Langu='" + Lang + "',Country='SA')?&$expand=zcitySet,zmain_descSet,zsub_desc_A60Set,zsub_desc_A61Set,zsub_desc_A62Set,URLSet,MSGSet,GOVCODESet&$format=json";
                    HttpResponseMessage GAZTZakatForm5CityResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
                    if (GAZTZakatForm5CityResponse != null)
                    {
                        if (GAZTZakatForm5CityResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTZakatForm5CityResponse.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                            App.IsSessionExpired = false;
                        }
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }

                        string GAZTZakatForm5ResponseJSON = GAZTZakatForm5CityResponse.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(GAZTZakatForm5ResponseJSON))
                        {
                            GAZTZakatForm5ResponseJSON = JObject.Parse(GAZTZakatForm5ResponseJSON)["d"].ToString();

                            ZakatForm5CityDataResultSet = JsonConvert.DeserializeObject<ZakatForm5CityDataResult>(GAZTZakatForm5ResponseJSON);
                            if (ZakatForm5CityDataResultSet == null)
                            {
                                throw new Exception(AppResources.NoTINsAvailable);
                            }
                        }
                        else
                        {
                            throw new Exception(AppResources.ZNoICRAvailable);
                        }
                    }
                    return ZakatForm5CityDataResultSet;
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
        #endregion

        #region Zakat Estimation Summary
        public static async Task<ZakatForm5SummaryResult> GAZTZakatForm5DataSummary(string Fbnum)
        {
            if (NetworkCheck.IsInternet())
            {
                ZakatForm5SummaryResult ZakatForm5SummaryResultSet = new ZakatForm5SummaryResult();
                string NewToken = string.Empty;
                try
                {
                    string url = ZATCAConstants.Z_ZKTE_SUMMARY + "(Fbnum='" + Fbnum + "',Flag='X')?$expand=headsumSet,SadadSet,SchGP01Set,SchGP02Set,SchGP03Set,SchGP04Set,SchGP05Set,SchGP06Set,SchGP07Set,SchGP08Set,SchGP09Set,SchGP10Set,SchGP11Set,SchGP12Set&$format=json";
                    HttpResponseMessage GAZTZakatForm5SummaryResponse = await GetServiceManager.MakeGetAPICall(url, false, "");
                    if (GAZTZakatForm5SummaryResponse != null)
                    {
                        if (GAZTZakatForm5SummaryResponse.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            App.IsSessionExpired = true;
                            return null;
                        }
                        HttpHeaders headers = GAZTZakatForm5SummaryResponse.Headers;
                        IEnumerable<string> values;
                        if (headers.TryGetValues("token", out values))
                        {
                            NewToken = values.First();
                            App.IsSessionExpired = false;
                        }
                        if (!string.IsNullOrEmpty(NewToken))
                        {
                            if (0 == string.Compare(NewToken, "Token has expired") || 0 == string.Compare(NewToken, "Invalid Token"))
                            {
                                App.IsSessionExpired = true;
                                return null;
                            }
                            App.Token = NewToken;
                        }

                        string GAZTZakatForm5SummaryResponseJSON = GAZTZakatForm5SummaryResponse.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(GAZTZakatForm5SummaryResponseJSON))
                        {
                            GAZTZakatForm5SummaryResponseJSON = JObject.Parse(GAZTZakatForm5SummaryResponseJSON)["d"].ToString();

                            ZakatForm5SummaryResultSet = JsonConvert.DeserializeObject<ZakatForm5SummaryResult>(GAZTZakatForm5SummaryResponseJSON);
                            if (ZakatForm5SummaryResultSet == null)
                            {
                                throw new Exception(AppResources.NoTINsAvailable);
                            }
                        }
                        else
                        {
                            throw new Exception(AppResources.ZNoICRAvailable);
                        }
                    }
                    return ZakatForm5SummaryResultSet;
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
        #endregion
        #endregion
    }
}
