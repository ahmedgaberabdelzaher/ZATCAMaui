using ZATCAMAUI.Core.AppConfigurations;
using ZATCAMAUI.Core.Helper;
using ZATCAMAUI.Core.Services.Interface;
using ZATCAMAUI.Models.BaseModels;
using ZATCAMAUI.Models.MyReportsModel;

namespace ZATCAMAUI.Core.Services.Classes
{
    public class MyReportsServices : IMyReportsServices
    {

        public async Task<ReportsResult> GetMyReports(string mobile,int? reportStatus = null, string search = "", int pageNumber = 1, int pageSize = 10)
        {
            var body = new
            {
                reportType = reportStatus,
                pageNumber=pageNumber,
                pageSize=pageSize,
                mobile=mobile,
                languageCode=App.IsArabic?"ar":"en",
                search = search
            };
            var response = await NewHTTPManger.Post<DATAPowerBaseResponseResult<ReportsResult>>($"{PageSettings.ZATCABaseURL}v1/vat/reports/tax-types", body) as DATAPowerBaseResponseResult<ReportsResult>;
            return response.result;
        }

        public async Task<DATAPowerBaseResponseResult<KeyModel>> SendOTP(string mobile)
        {
            var lang = App.IsArabic ? "ar" : "en";
            var body = new
            {

                languageCode= lang,
                mobile = mobile,
            };
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

