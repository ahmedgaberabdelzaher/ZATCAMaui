using ZATCAMAUI.Models.BaseModels;
using ZATCAMAUI.Models.SubmitReportModel;

namespace ZATCAMAUI.Core.Services.Interface
{
    public interface ISubmitReportServices
    {
        Task<ReportTypeList> GetReportType();
        Task<LookUpsListModel> GetLookUps();
        Task<List<CategoryDataResponse>> GetReportCategories(string typeId);
        Task<List<BaseRegionAndCity>> GetCities(string regionId);
        Task<List<BaseRegionAndCity>> GetRegions();
        // Task<DATAPowerBaseResponseResult<SubmitDataPowerResult>> CreateZatcaNewReport(SubmitReportModel submitReport);
        ///Data Power
        Task<DATAPowerBaseResponseResult<SubmitDataPowerResult>> CreateZatcaNewReport(SubmitReportDataPowerModel submitReport);
        Task<BaseResponseModel<string>> CreateZatcaReport(SubmitReportModel submitReport);

    }
}

