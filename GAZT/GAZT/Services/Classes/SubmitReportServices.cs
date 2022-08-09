using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using EGAZT.Helper;
using EGAZT.Models.SubmitReportModel;
using EGAZT.Services.Interface;

namespace EGAZT.Services.Classes
{
    public class SubmitReportServices : ISubmitReportServices
    {
        public async Task<List<CategoryDataResponse>> GetReportCategories(string typeId)
        {
            var response = await NewHTTPManger.Get<BaseVatReport<List<CategoryDataResponse>>>($"{App.VatBaseUrl}/SMS/GetCategories?type={typeId}") as BaseVatReport<List<CategoryDataResponse>>;
            return response?.Result?.Data;
        }
        public async Task<List<BaseRegionAndCity>> GetCities(string regionId)
        {
            var response = await NewHTTPManger.Get<BaseVatReport<List<BaseRegionAndCity>>>($"{App.VatBaseUrl}/SMS/GetCities?region={regionId}") as BaseVatReport<List<BaseRegionAndCity>>;
            return response?.Result?.Data;
        } 
        public async Task<List<BaseRegionAndCity>> GetRegions()
        {
            var response = await NewHTTPManger.Get<BaseVatReport<List<BaseRegionAndCity>>>($"{App.VatBaseUrl}/SMS/GetRegions") as BaseVatReport<List<BaseRegionAndCity>>;
            return response?.Result?.Data;
        }
        public async Task<BaseVatReport<string>> CreateZatcaNewReport(Dictionary<string, string> submitReport,ObservableCollection<ReportFileModel> reportFiles)
        {
            var response = await NewHTTPManger.PostFile<BaseVatReport<string>>($"{App.VatBaseUrl}/Report/CreateZatcaNewReport",submitReport, reportFiles) as BaseVatReport<string>;
            return response;
        }
    }
}

