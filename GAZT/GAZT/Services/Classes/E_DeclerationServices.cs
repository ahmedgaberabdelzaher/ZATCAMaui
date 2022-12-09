using System;
using EGAZT.Helper;
using EGAZT.Models.CustomServices;
using System.Threading.Tasks;
using EGAZT.Services.Interface;
using EGAZT.Models.BaseModels;
using System.Collections.ObjectModel;
using EGAZT.Models.EDeclerationsModel;
using EGAZT.AppConfigurations;
using System.Net.Http;
using EGAZT.Models.EDeclerationsModel.FeesCalculators;

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

        public async Task<Tuple<DATAPowerBaseResponse<ObservableCollection<ProductTypesModel>>, bool, string>> GetProductTypes()
        {
            var response = await HttpManager.GetAsync<DATAPowerBaseResponse<ObservableCollection<ProductTypesModel>>>($"{PageSettings.ZATCABaseURL}v1/references/customs/nibras/good-types").ConfigureAwait(false);
            return response;
        }
        public async Task<Tuple<DATAPowerBaseResponse<ObservableCollection<ProductTypesModel>>, bool, string>> GetProductSubTypes(string productTypeId)
        {
            var response = await HttpManager.GetAsync<DATAPowerBaseResponse<ObservableCollection<ProductTypesModel>>>($"{PageSettings.ZATCABaseURL}v1/references/customs/nibras/good-sub-types?goodTypeID={productTypeId}").ConfigureAwait(false);
            return response;
        }
        public async Task<HttpResponseMessage> FeesCalculator(FeesCalculatorBody body)
        {
            var response = await HttpManager.PostAsync<FeesCalculatorBody>($"{PageSettings.ZATCABaseURL}v1/customs/calculate-fees",body).ConfigureAwait(false);
            return response;
        }
    }
}

