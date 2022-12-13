using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using EGAZT.AppConfigurations;
using EGAZT.Helper;
using EGAZT.Models.BaseModels;
using EGAZT.Models.SubmitReportModel;
using EGAZT.Services.Interface;

namespace EGAZT.Services.Classes
{
    public class SubmitReportServices : ISubmitReportServices
    {
        public async Task<ReportTypeList> GetReportType()
        {
            var response = await NewHTTPManger.Get<BaseResponseModel<ReportTypeList>>($"{App.VatBaseUrl}/Report/GetReportTaxType") as BaseResponseModel<ReportTypeList>;
            return response?.Result?.Data;
        }
        public async Task<LookUpsListModel> GetLookUps()
        {
            var response = await NewHTTPManger.Get<BaseResponseModel<LookUpsListModel>>($"{App.VatBaseUrl}/Report/GetLookups") as BaseResponseModel<LookUpsListModel>;
            return response?.Result?.Data;
        }
        public async Task<List<CategoryDataResponse>> GetReportCategories(string typeId)
        {
            var response = await NewHTTPManger.Get<BaseResponseModel<List<CategoryDataResponse>>>($"{App.VatBaseUrl}/SMS/GetCategories?type={typeId}") as BaseResponseModel<List<CategoryDataResponse>>;
            return response?.Result?.Data;
        }
        public async Task<List<BaseRegionAndCity>> GetCities(string regionId)
        {
            var response = await NewHTTPManger.Get<BaseResponseModel<List<BaseRegionAndCity>>>($"{App.VatBaseUrl}/SMS/GetCities?region={regionId}") as BaseResponseModel<List<BaseRegionAndCity>>;
            return response?.Result?.Data;
        } 
        public async Task<List<BaseRegionAndCity>> GetRegions()
        {
            var response = await NewHTTPManger.Get<BaseResponseModel<List<BaseRegionAndCity>>>($"{App.VatBaseUrl}/SMS/GetRegions") as BaseResponseModel<List<BaseRegionAndCity>>;
            return response?.Result?.Data;
        }
        public async Task<DATAPowerBaseResponseResult<SubmitDataPowerResult>> CreateZatcaNewReport(SubmitReportDataPowerModel submitReport)
        {
            /* var response = await NewHTTPManger.Post<BaseResponseModel<string>>($"{App.VatBaseUrl}/Report/CreateZatcaNewReport",submitReport) as BaseResponseModel<string>;
           */
            //   var res = await HttpManager.PostAsync($"{App.VatBaseUrl}/Report/CreateZatcaNewReport", submitReport);
//DataPower
            var res = await HttpManager.PostAsync($"{PageSettings.ZATCABaseURL}v4/zatca/new-report/submit-form", submitReport);

            var cont = await res.Content.ReadAsStringAsync();
            var response = NewHTTPManger.DeserializeObject<DATAPowerBaseResponseResult<SubmitDataPowerResult>>(cont);
            return response;
        }
    }
}

