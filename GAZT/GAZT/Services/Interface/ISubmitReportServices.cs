using EGAZT.Models.BaseModels;
using EGAZT.Models.SubmitReportModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace EGAZT.Services.Interface
{
    public interface ISubmitReportServices
    {
        Task<List<ReportTypeModel>> GetReportType();
        Task<List<BaseRegionAndCity>> GetLookUps();
        Task<List<CategoryDataResponse>> GetReportCategories(string typeId);
        Task<List<BaseRegionAndCity>> GetCities(string regionId);
        Task<List<BaseRegionAndCity>> GetRegions();
        // Task<DATAPowerBaseResponseResult<SubmitDataPowerResult>> CreateZatcaNewReport(SubmitReportModel submitReport);
        ///Data Power
         Task<DATAPowerBaseResponseResult<SubmitDataPowerResult>> CreateZatcaNewReport(SubmitReportDataPowerModel submitReport);
         Task<BaseResponseModel<string>> CreateZatcaReport(SubmitReportModel submitReport);

    }
}

