using System;
using EGAZT.Helper;
using EGAZT.Models.BaseModels;
using EGAZT.Models.SubmitReportModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using EGAZT.Services.Interface;
using EGAZT.Models.MyReportsModel;
using EGAZT.Controls;
using EGAZT.AppConfigurations;

namespace EGAZT.Services.Classes
{
    public class MyReportsServices : IMyReportsServices
    {
        public async Task<DataModel<List<MyReportsModel>>> GetMyReports(string mobile,int? reportStatus = null, string search = "", int pageNumber = 1, int pageSize = 10)
        {
            var body = new
            {
                reportType = reportStatus,
                search = search
            };
            var response = await NewHTTPManger.Post<BaseResponseModel<List<MyReportsModel>>>($"{App.VatBaseUrl}/Report/GetReportTaxByMobile?PageNumber={pageNumber}&PageSize={pageSize}&mobile={mobile}", body) as BaseResponseModel<List<MyReportsModel>>;
            return response.Result;
        }

        public async Task<DATAPowerBaseResponseResult<KeyModel>> SendOTP(string mobile)
        {
            var lang = App.IsArabic ? "ar" : "en";
            var body = new
            {
                languageCode= lang,
                mobile = mobile,
            };
            /* var response = await NewHTTPManger.Post<BaseResponseModel<SendOTPModel>> ($"{App.VatBaseUrl}/SMS/SendOTP", body) as BaseResponseModel<SendOTPModel>;*/
          var response = await NewHTTPManger.Post<DATAPowerBaseResponseResult<KeyModel>> ($"{PageSettings.ZATCABaseURL}v1/vat/sms/otp/sending", body) as DATAPowerBaseResponseResult<KeyModel>;

            return response;
        }

        public async Task<DATAPowerBaseResponse<SendOTPModel>> VerifyCode(string mobile, string key, string otpCode)
        {
            var lang = App.IsArabic ? "ar" : "en";
            var body = new
            {
                languageCode = lang,
                mobile = mobile,
                key = key,
                code = otpCode
            };
            var response = await NewHTTPManger.Post <DATAPowerBaseResponse<SendOTPModel>>($"{PageSettings.ZATCABaseURL}v1/vat/sms/otp/verification", body) as DATAPowerBaseResponse<SendOTPModel >;
            return response;
        }
    }
}

