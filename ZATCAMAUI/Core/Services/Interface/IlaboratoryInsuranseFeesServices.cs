using ZATCAMAUI.Models.CustomServices;

namespace ZATCAMAUI.Core.Services.Interface
{
    public interface IlaboratoryInsuranseFeesServices
    {
        Task<Tuple<InsuranceCheckListModel, bool, string>> CheckLetterSample(int portCode, int RequestNo, string RequestDate);

    }
}
