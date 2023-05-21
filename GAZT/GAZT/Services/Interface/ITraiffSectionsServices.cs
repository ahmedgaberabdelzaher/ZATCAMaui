using System;
using System.Threading.Tasks;
using EGAZT.Models.CustomServices.TraiffSection;

namespace EGAZT.Services.Interface
{
    public interface ITraiffSectionsServices
    {
        Task<Tuple<SectionsModel, bool, string>> GetTraiffSections();
        Task<Tuple<ChaptersModel, bool, string>> GetTariffChapters(string secCode);
        Task<Tuple<MainHarmonizedTariffModel, bool, string>> GetMainHarmonizedTariffs(string chptCode);
        Task<Tuple<SubHarmonizedTraiffsModel, bool, string>> GetSubHarmonizedTariffs(string chptCode, string mainItemCode);
        Task<Tuple<HarmonizedTarrifResponse, bool, string>> GetHarmonizedTarrifs(string parentItemCode);
        Task<Tuple<TariffSearchResponse, bool, string>> Search(int searchCreteria, string key);
    }
}
