using ZATCAMAUI.Models.BaseModels;
using ZATCAMAUI.Models.MyReportsModel;

namespace ZATCAMAUI.Core.Services.Interface
{
    public interface IMyReportsServices
    {

        Task<ReportsResult> GetMyReports(string mobile, int? reportStatus = null, string search = "", int pageNumber = 1, int pageSize = 10);

        Task<DATAPowerBaseResponseResult<KeyModel>> SendOTP(string mobile);

        Task<DATAPowerBaseResponse<SendOTPModel>> VerifyCode(string mobile, string key, string otpCode);
    }
}

