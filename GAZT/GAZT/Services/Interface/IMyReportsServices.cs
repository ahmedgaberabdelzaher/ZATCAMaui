using System;
using EGAZT.Models.MyReportsModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EGAZT.Services.Interface
{
    public interface IMyReportsServices
    {
        Task<List<MyReportsModel>> GetMyReports(string mobile, int pageNumber = 1, int pageSize = 10);

        Task<List<MyReportsModel>> GetMyReports(string mobile, int reportStatus, int pageNumber = 1, int pageSize = 10);
    }
}

