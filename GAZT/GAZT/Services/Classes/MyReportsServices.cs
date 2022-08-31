using System;
using EGAZT.Helper;
using EGAZT.Models.BaseModels;
using EGAZT.Models.SubmitReportModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using EGAZT.Services.Interface;
using EGAZT.Models.MyReportsModel;

namespace EGAZT.Services.Classes
{
    public class MyReportsServices : IMyReportsServices
    {
        public async Task<List<MyReportsModel>> GetSearcedMyReports(string mobile,string search = "", int pageNumber = 1, int pageSize = 30)
        {
            int? reportType = null;
            var body = new
            {
                reportType = reportType,
                search = search
            };
            var response = await NewHTTPManger.Post<BaseResponseModel<List<MyReportsModel>>>($"{App.VatBaseUrl}/Report/GetReportTaxByMobile?PageNumber={pageNumber}&PageSize={pageSize}&mobile={mobile}", body) as BaseResponseModel<List<MyReportsModel>>;
                return response?.Result?.Data;
        }

        public async Task<List<MyReportsModel>> GetMyReports(string mobile,int? reportStatus = null, int pageNumber = 1, int pageSize = 30)
        {
            var body = new
            {
                reportType = reportStatus,
                search = ""
            };
            var response = await NewHTTPManger.Post<BaseResponseModel<List<MyReportsModel>>>($"{App.VatBaseUrl}/Report/GetReportTaxByMobile?PageNumber={pageNumber}&PageSize={pageSize}&mobile={mobile}", body) as BaseResponseModel<List<MyReportsModel>>;
            return response?.Result?.Data;
        }

        public async Task<BaseResponseModel<SendOTPModel>> SendOTP(string mobile)
        {
            var body = new
            {
                mobile = mobile,
            };
            var response = await NewHTTPManger.Post<BaseResponseModel<SendOTPModel>> ($"{App.VatBaseUrl}/SMS/SendOTP", body) as BaseResponseModel<SendOTPModel>;
            return response;
        }

        public async Task<VerifyCodeModel> VerifyCode(string mobile, string key, string otpCode)
        {
            var body = new
            {
                mobile = mobile,
                key = key,
                code = otpCode
            };
            var response = await NewHTTPManger.Post<VerifyCodeModel>($"{App.VatBaseUrl}/SMS/VerifyCode", body) as VerifyCodeModel;
            return response;
        }
    }
}

