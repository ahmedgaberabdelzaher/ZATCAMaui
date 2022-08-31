using System;
using EGAZT.Models.MyReportsModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using EGAZT.Models.BaseModels;

namespace EGAZT.Services.Interface
{
    public interface IMyReportsServices
    {
        Task<List<MyReportsModel>> GetSearcedMyReports(string mobile, string search = "", int pageNumber = 1, int pageSize = 30);

        Task<List<MyReportsModel>> GetMyReports(string mobile, int? reportStatus = null, int pageNumber = 1, int pageSize = 30);

        Task<BaseResponseModel<SendOTPModel>> SendOTP(string mobile);

        Task<VerifyCodeModel> VerifyCode(string mobile, string key, string otpCode);
    }
}

