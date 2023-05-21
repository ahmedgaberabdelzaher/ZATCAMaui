using System;
using System.Threading.Tasks;
using EGAZT.Models.CustomServices;
using EGAZT.Models.CustomServices.TraiffSection;

namespace EGAZT.Services.Interface
{
    public interface IlaboratoryInsuranseFeesServices
    {
        Task<Tuple<InsuranceCheckListModel, bool, string>> CheckLetterSample(int portCode,int RequestNo,string RequestDate);

    }
}
