using ZATCAMAUI.Models.BaseModels;
using ZATCAMAUI.Models.MyReportsModel;

namespace ZATCAMAUI.Core.Services.Interface
{
    public interface IMyReportsServices
    {

        Task<DataModel<List<MyReportsModel>>> GetMyReports(string mobile, int? reportStatus = null, string search = "", int pageNumber = 1, int pageSize = 10);

        Task<BaseResponseModel<SendOTPModel>> SendOTP(string mobile);

        Task<VerifyCodeModel> VerifyCode(string mobile, string key, string otpCode);
    }
}

