using EGAZT.Models.BaseModels;
using EGAZT.Models.SubmitReportModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace EGAZT.Services.Interface
{
    public interface ISubmitReportServices
    {
        Task<ReportTypeList> GetReportType();
        Task<LookUpsListModel> GetLookUps();
        Task<List<CategoryDataResponse>> GetReportCategories(string typeId);
        Task<List<BaseRegionAndCity>> GetCities(string regionId);
        Task<List<BaseRegionAndCity>> GetRegions();
        Task<BaseResponseModel<string>> CreateZatcaNewReport(SubmitReportModel submitReport);
    }
}

