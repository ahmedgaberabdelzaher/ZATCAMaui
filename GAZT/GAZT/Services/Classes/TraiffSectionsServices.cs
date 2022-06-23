using System;
using System.Threading.Tasks;
using EGAZT.Helper;
using EGAZT.Models.CustomServices.TraiffSection;
using EGAZT.Services.Interface;

namespace EGAZT.Services.Classes
{
    public class TraiffSectionsServices : ITraiffSectionsServices
    {

        public async Task<Tuple<SectionsModel, bool, string>> GetTraiffSections()
        {
            var response = await HttpManager.GetAsync<SectionsModel>(App.CustomBaseUrl + $"Tariff/GetTariffSections", true).ConfigureAwait(false);

            return response;
        }

        public async Task<Tuple<ChaptersModel, bool, string>> GetTariffChapters(string secCode)
        {
            var response = await HttpManager.GetAsync<ChaptersModel>(App.CustomBaseUrl + $"Tariff/GetTariffChapters?SECT_CODE={secCode}", true).ConfigureAwait(false);

            return response;
        }

        public async Task<Tuple<MainHarmonizedTariffModel, bool, string>> GetMainHarmonizedTariffs(string chptCode)
        {
            var response = await HttpManager.GetAsync<MainHarmonizedTariffModel>(App.CustomBaseUrl + $"Tariff/GetMainHarmonizedTariffs/{chptCode}", true).ConfigureAwait(false);

            return response;
        }
        public async Task<Tuple<SubHarmonizedTraiffsModel, bool, string>> GetSubHarmonizedTariffs(string chptCode,string mainItemCode)
        {
            var response = await HttpManager.GetAsync<SubHarmonizedTraiffsModel>(App.CustomBaseUrl + $"Tariff/GetSubHarmonizedTariffs/{chptCode}/{mainItemCode}", true).ConfigureAwait(false);

            return response;
        }
        public async Task<Tuple<HarmonizedTarrifResponse, bool, string>> GetHarmonizedTarrifs(string parentItemCode)
        {
            var response = await HttpManager.GetAsync<HarmonizedTarrifResponse>(App.CustomBaseUrl + $"Tariff/GetHarmonizedTariffs/{parentItemCode}", true).ConfigureAwait(false);

            return response;
        }

        public async Task<Tuple<TariffSearchResponse, bool, string>> Search(int searchCreteria,string key)
        {
            var response = await HttpManager.GetAsync<TariffSearchResponse>(App.CustomBaseUrl + $"Tariff/GetSubHarmonizedTariffs/{searchCreteria}/{key}", true).ConfigureAwait(false);

            return response;
        }
    }
}
