using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Services.Interface;
using ZATCAMAUI.Models.BaseModels;
using ZATCAMAUI.Models.MyReportsModel;

namespace ZATCAMAUI.Core.Services.Classes
{
    public class MyReportsServices : IMyReportsServices
    {
        public async Task<DataModel<List<MyReportsModel>>> GetMyReports(string mobile, int? reportStatus = null, string search = "", int pageNumber = 1, int pageSize = 10)
        {
            var body = new
            {
                reportType = reportStatus,
                search
            };
            var response = await NewHTTPManger.Post<BaseResponseModel<List<MyReportsModel>>>($"{App.VatBaseUrl}/Report/GetReportTaxByMobile?PageNumber={pageNumber}&PageSize={pageSize}&mobile={mobile}", body) as BaseResponseModel<List<MyReportsModel>>;
            return response.Result;
        }

        public async Task<BaseResponseModel<SendOTPModel>> SendOTP(string mobile)
        {
            var body = new
            {
                mobile,
            };
            var response = await NewHTTPManger.Post<BaseResponseModel<SendOTPModel>>($"{App.VatBaseUrl}/SMS/SendOTP", body) as BaseResponseModel<SendOTPModel>;
            return response;
        }

        public async Task<VerifyCodeModel> VerifyCode(string mobile, string key, string otpCode)
        {
            var body = new
            {
                mobile,
                key,
                code = otpCode
            };
            var response = await NewHTTPManger.Post<VerifyCodeModel>($"{App.VatBaseUrl}/SMS/VerifyCode", body) as VerifyCodeModel;
            return response;
        }
    }
}

