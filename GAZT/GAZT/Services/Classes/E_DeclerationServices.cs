using System;
using EGAZT.Helper;
using EGAZT.Models.CustomServices;
using System.Threading.Tasks;
using EGAZT.Services.Interface;
using EGAZT.Models.BaseModels;
using System.Collections.ObjectModel;
using EGAZT.Models.EDeclerationsModel;
using EGAZT.AppConfigurations;

namespace EGAZT.Services.Classes
{
    public class E_DeclerationServices: IE_DeclerationServices
    {
        public async Task<Tuple<DATAPowerBaseResponse<ObservableCollection<TobacoTypesModel>>, bool, string>> GetTobacoTypes()
        {
            var response = await HttpManager.GetAsync<DATAPowerBaseResponse<ObservableCollection<TobacoTypesModel>>>($"{PageSettings.ZATCABaseURL}v1/references/customs/nibras/tobacco-category").ConfigureAwait(false);
            return response;
        }
        public async Task<Tuple<DATAPowerBaseResponse<ObservableCollection<TobaccoItemsModel>>, bool, string>> GetTobacoItem(int TobacoTypeID)
        {
            var response = await HttpManager.GetAsync<DATAPowerBaseResponse<ObservableCollection<TobaccoItemsModel>>>($"{PageSettings.ZATCABaseURL}v1/references/customs/nibras/tobacco-items?tobaccoTypeID={TobacoTypeID}").ConfigureAwait(false);
            return response;
        }
    }
}

