using ZATCAMAUI.Models.BaseModels;
using ZATCAMAUI.Models.SubmitReportModel;

namespace ZATCAMAUI.Core.Services.Interface
{
    public interface ISubmitReportServices
    {
        Task<List<ReportTypeModel>> GetReportType();
        Task<List<BaseRegionAndCity>> GetLookUps();
        Task<List<CategoryDataResponse>> GetReportCategories(string typeId);
        Task<List<BaseRegionAndCity>> GetCities(string regionId);
        Task<List<BaseRegionAndCity>> GetRegions();

         Task<DATAPowerBaseResponseResult<SubmitDataPowerResult>> CreateZatcaNewReport(SubmitReportDataPowerModel submitReport);
         Task<BaseResponseModel<string>> CreateZatcaReport(SubmitReportModel submitReport);
        Task<List<CategoryDataResponse>> GetReportSubCategories(string CatId);


    }
}

