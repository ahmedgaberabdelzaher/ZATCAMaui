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
        public async Task<List<MyReportsModel>> GetMyReports(string mobile, int pageNumber = 1, int pageSize = 10)
        {
            var body = new
            {
                reportType = "1",
                search = ""
            };
        var response = await NewHTTPManger.Post<BaseResponseModel<List<MyReportsModel>>>($"{App.VatBaseUrl}/Report/GetReportTaxByMobile?PageNumber={pageNumber}&PageSize={pageSize}&mobile={mobile}", body) as BaseResponseModel<List<MyReportsModel>>;
            return response?.Result?.Data;
        }

        public async Task<List<MyReportsModel>> GetMyReports(string mobile,int reportStatus, int pageNumber = 1, int pageSize = 10)
        {
            var body = new
            {
                reportType = reportStatus,
                search = ""
            };
            var response = await NewHTTPManger.Post<BaseResponseModel<List<MyReportsModel>>>($"{App.VatBaseUrl}/Report/GetReportTaxByMobile?PageNumber={pageNumber}&PageSize={pageSize}&mobile={mobile}", body) as BaseResponseModel<List<MyReportsModel>>;
            return response?.Result?.Data;
        }
    }
}

