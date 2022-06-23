using System;
using System.Threading.Tasks;
using EGAZT.Helper;
using EGAZT.Models.CustomServices;
using EGAZT.Services.Interface;

namespace EGAZT.Services.Classes
{
    public class laboratoryInsuranseFeesServices: IlaboratoryInsuranseFeesServices
    {
        public async Task<Tuple<InsuranceCheckListModel, bool, string>> CheckLetterSample(int portCode, int RequestNo, string RequestDate)
        {
            var response = await HttpManager.GetAsync<InsuranceCheckListModel>(App.CustomBaseUrl + $"Insurance/CheckLetterSample/{portCode}/{RequestNo}/{RequestDate}", true,portCode.ToString()).ConfigureAwait(false);
            return response;
        }
    }
}
