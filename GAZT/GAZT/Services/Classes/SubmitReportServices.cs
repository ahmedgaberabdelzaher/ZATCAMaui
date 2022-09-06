using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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
        public async Task<BaseResponseModel<string>> CreateZatcaNewReport(Dictionary<string, string> submitReport,ObservableCollection<ReportFileModel> reportFiles)
        {
            var response = await NewHTTPManger.PostFile<BaseResponseModel<string>>($"{App.VatBaseUrl}/Report/CreateZatcaNewReport",submitReport, reportFiles) as BaseResponseModel<string>;
            return response;
        }
    }
}

