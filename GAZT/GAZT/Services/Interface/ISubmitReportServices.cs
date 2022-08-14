using EGAZT.Models.SubmitReportModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace EGAZT.Services.Interface
{
    public interface ISubmitReportServices
    {
        Task<List<CategoryDataResponse>> GetReportCategories(string typeId);
        Task<List<BaseRegionAndCity>> GetCities(string regionId);
        Task<List<BaseRegionAndCity>> GetRegions();
        Task<BaseVatReport<string>> CreateZatcaNewReport(Dictionary<string, string> submitReport, ObservableCollection<ReportFileModel> reportFiles);
    }
}

